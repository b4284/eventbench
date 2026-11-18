using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace EventBench.RabbitMq;

/// <summary>
/// RabbitMQ broker: per group, N durable quorum queues ("partitions"); a source maps to one partition and each
/// partition is consumed by exactly one worker. Publishes are persistent and wait for publisher confirms.
/// Nack/retry is handled client-side (see <see cref="OrderedRedeliveryBuffer{T}"/>) because a broker-side
/// requeue does not preserve per-source order once later messages are prefetched.
/// </summary>
public sealed class RabbitMqBroker : IBroker, IStatsProvider
{
    internal const string PublishTimestampHeader = "eb-ts";

    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly ConnectionFactory _factory;
    private readonly ConnectionSlot _admin;
    private readonly ConnectionSlot[] _publishConns;
    private readonly ConnectionSlot[] _consumerConns;
    private readonly HttpClient? _http;
    private readonly string _vhost;
    private int _nextPublisherConn;
    private int _mgmtWarned;
    internal long TestNacks;

    public RabbitMqOptions Options { get; }
    public int Prefetch { get; }
    internal TimeSpan TestNackDelay { get; }
    public string Name => "rabbitmq";
    public IStatsProvider Stats => this;

    public RabbitMqBroker(RabbitMqOptions options, BenchContext context)
    {
        Options = options;
        _topology = context.Topology;
        _log = context.Log;
        Prefetch = Math.Clamp(options.Prefetch > 0 ? options.Prefetch : 4 * Math.Max(1, context.Config.Worker.BatchSize), 1, ushort.MaxValue);
        TestNackDelay = TimeSpan.FromMilliseconds(options.TestNackDelayMs >= 0 ? options.TestNackDelayMs : context.Config.Worker.RetryDelayMs);
        if (options.TestNackProbability is < 0 or >= 1)
            throw new InvalidOperationException("Broker:TestNackProbability must be in [0, 1)");

        _factory = new ConnectionFactory
        {
            AutomaticRecoveryEnabled = false, // the consumer re-creates its channel itself (it must reset its buffer)
            TopologyRecoveryEnabled = false,
            MaxInboundMessageBodySize = (uint)Math.Min(options.MaxMessageBytes, 512L * 1024 * 1024),
            ConsumerDispatchConcurrency = 1, // deliveries of a channel are dispatched in order
        };
        if (!string.IsNullOrWhiteSpace(options.Uri))
        {
            _factory.Uri = new Uri(options.Uri);
        }
        else
        {
            _factory.HostName = options.Host;
            _factory.Port = options.Port;
            _factory.UserName = options.User;
            _factory.Password = options.Password;
            _factory.VirtualHost = options.VirtualHost;
        }
        _vhost = _factory.VirtualHost;

        _admin = new ConnectionSlot(_factory, "eventbench-admin");
        _publishConns = Enumerable.Range(0, Math.Max(1, options.PublisherConnections))
            .Select(i => new ConnectionSlot(_factory, $"eventbench-pub-{i}")).ToArray();
        _consumerConns = Enumerable.Range(0, Math.Max(1, options.ConsumerConnections))
            .Select(i => new ConnectionSlot(_factory, $"eventbench-con-{i}")).ToArray();

        if (!string.IsNullOrWhiteSpace(options.ManagementUrl))
        {
            _http = new HttpClient { BaseAddress = new Uri(options.ManagementUrl.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(10) };
            var user = options.ManagementUser ?? _factory.UserName;
            var pass = options.ManagementPassword ?? _factory.Password;
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}")));
        }

        foreach (var g in _topology.Groups)
            if (Partitions(g) < g.Workers)
                throw new InvalidOperationException(
                    $"rabbitmq: group {g.Name} has {g.Workers} workers but only {Partitions(g)} partitions (workers would idle).");
    }

    private int Partitions(GroupInfo g) => Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup : g.Workers;

    internal string QueueName(int groupId, int partition) => $"{Options.QueuePrefix}.g{groupId}.p{partition}";

    /// <summary>Deterministic source -> partition mapping (same as PgmqBroker Partitioned).</summary>
    internal string QueueFor(EventEnvelope ev)
    {
        var g = _topology.Groups[ev.Group];
        return QueueName(ev.Group, (ev.SourceId - g.FirstSourceId) % Partitions(g));
    }

    private List<string> AllQueues()
    {
        var list = new List<string>();
        foreach (var g in _topology.Groups)
            for (int p = 0; p < Partitions(g); p++) list.Add(QueueName(g.Id, p));
        return list;
    }

    internal ValueTask<IConnection> PublisherConnectionAsync(CancellationToken ct)
        => _publishConns[(int)((uint)Interlocked.Increment(ref _nextPublisherConn) % _publishConns.Length)].GetAsync(ct);

    internal ValueTask<IConnection> ConsumerConnectionAsync(int index, CancellationToken ct)
        => _consumerConns[index % _consumerConns.Length].GetAsync(ct);

    private Dictionary<string, object?> QueueArguments()
    {
        var args = new Dictionary<string, object?> { ["x-queue-type"] = "quorum" };
        if (Options.SingleActiveConsumer) args["x-single-active-consumer"] = true;
        if (Options.DeliveryLimit != 0) args["x-delivery-limit"] = Options.DeliveryLimit;
        return args;
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        var conn = await _admin.GetAsync(ct);
        _log($"rabbitmq server {ServerVersion(conn)}");

        var wanted = AllQueues();
        var toDelete = new HashSet<string>(wanted);
        try
        {
            foreach (var q in await ListQueuesAsync(ct))
                if (q.Name.StartsWith(Options.QueuePrefix + ".", StringComparison.Ordinal)) toDelete.Add(q.Name);
        }
        catch (Exception ex)
        {
            _log($"rabbitmq: management API unavailable ({ex.Message}); deleting only this topology's queues");
        }

        var args = QueueArguments();
        await ForEachOnChannelsAsync(toDelete, (ch, q, token) => ch.QueueDeleteAsync(q, false, false, cancellationToken: token), ct);
        await ForEachOnChannelsAsync(wanted, (ch, q, token) =>
            ch.QueueDeclareAsync(q, durable: true, exclusive: false, autoDelete: false, arguments: args, cancellationToken: token), ct);
        _log($"rabbitmq: deleted {toDelete.Count} queues, declared {wanted.Count} quorum queues " +
             $"(SAC={Options.SingleActiveConsumer}, delivery-limit={Options.DeliveryLimit})");
    }

    /// <summary>Runs admin operations on a few channels in parallel (quorum queue declares take a while each).</summary>
    private async Task ForEachOnChannelsAsync(IEnumerable<string> queues, Func<IChannel, string, CancellationToken, Task> op, CancellationToken ct)
    {
        var conn = await _admin.GetAsync(ct);
        var work = new System.Collections.Concurrent.ConcurrentQueue<string>(queues);
        var tasks = Enumerable.Range(0, Math.Min(8, Math.Max(1, work.Count))).Select(async _ =>
        {
            await using var ch = await conn.CreateChannelAsync(cancellationToken: ct);
            while (work.TryDequeue(out var q)) await op(ch, q, ct);
            await ch.CloseAsync(ct);
        });
        await Task.WhenAll(tasks);
    }

    private static string ServerVersion(IConnection conn)
        => conn.ServerProperties is { } p && p.TryGetValue("version", out var v)
            ? v is byte[] b ? Encoding.UTF8.GetString(b) : v?.ToString() ?? "?"
            : "?";

    public IEventPublisher CreatePublisher(int groupId) => new RabbitMqPublisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        var g = _topology.Groups[groupId];
        var owned = Enumerable.Range(0, Partitions(g)).Where(p => p % g.Workers == workerIndex).Select(p => QueueName(groupId, p)).ToArray();
        // Spread consumer channels over the consumer connections.
        int index = g.FirstSourceId + workerIndex;
        return new RabbitMqConsumer(this, $"{g.Name}#{workerIndex}", owned, index, _log);
    }

    // ---------------------------------------------------------------------------------------------- backlog & stats

    private sealed record QueueStat(string Name, long Messages, long Ready, long Unacked, long Consumers);

    private async Task<List<QueueStat>> ListQueuesAsync(CancellationToken ct)
    {
        if (_http is null) throw new InvalidOperationException("Broker:ManagementUrl is not set");
        var url = $"api/queues/{Uri.EscapeDataString(_vhost)}?columns=name,messages,messages_ready,messages_unacknowledged,consumers";
        using var resp = await _http.GetAsync(url, ct);
        resp.EnsureSuccessStatusCode();
        await using var stream = await resp.Content.ReadAsStreamAsync(ct);
        using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
        static long L(JsonElement e, string name) => e.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.Number ? v.GetInt64() : 0;
        var list = new List<QueueStat>();
        foreach (var q in doc.RootElement.EnumerateArray())
            list.Add(new QueueStat(q.GetProperty("name").GetString()!, L(q, "messages"), L(q, "messages_ready"),
                L(q, "messages_unacknowledged"), L(q, "consumers")));
        return list;
    }

    private async Task<List<QueueStat>> OwnQueueStatsAsync(CancellationToken ct)
    {
        var own = new HashSet<string>(AllQueues());
        return (await ListQueuesAsync(ct)).Where(q => own.Contains(q.Name)).ToList();
    }

    /// <summary>
    /// Ready + unacked across this topology's queues. The management API is the only source of the unacked count
    /// (its stats lag by collect_statistics_interval); passive declares give an exact ready count. Take the max.
    /// </summary>
    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        long mgmt = 0;
        if (_http != null)
        {
            try { mgmt = (await OwnQueueStatsAsync(ct)).Sum(q => q.Messages); }
            catch (Exception ex) when (ex is not OperationCanceledException || !ct.IsCancellationRequested)
            {
                if (Interlocked.Exchange(ref _mgmtWarned, 1) == 0)
                    _log($"rabbitmq: management API failed ({ex.Message}); backlog falls back to ready count only");
            }
        }

        long ready = 0;
        var conn = await _admin.GetAsync(ct);
        await using (var ch = await conn.CreateChannelAsync(cancellationToken: ct))
        {
            foreach (var q in AllQueues()) ready += await ch.MessageCountAsync(q, ct);
            await ch.CloseAsync(ct);
        }
        return Math.Max(mgmt, ready);
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var stats = await OwnQueueStatsAsync(ct);
        return new Dictionary<string, double>
        {
            ["broker_backlog"] = stats.Sum(q => q.Messages),
            ["broker_ready"] = stats.Sum(q => q.Ready),
            ["broker_unacked"] = stats.Sum(q => q.Unacked),
            ["broker_max_queue_len"] = stats.Count == 0 ? 0 : stats.Max(q => q.Messages),
            ["broker_consumers"] = stats.Sum(q => q.Consumers),
            ["broker_test_nacks"] = Interlocked.Read(ref TestNacks),
        };
    }

    public string Describe()
    {
        var where = !string.IsNullOrWhiteSpace(Options.Uri)
            ? new Uri(Options.Uri).GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped)
            : $"{Options.Host}:{Options.Port}";
        var parts = Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup.ToString() : "=workers";
        return $"rabbitmq quorum queues, {parts} partitions/group ({AllQueues().Count} queues), prefetch {Prefetch}, " +
               $"SAC={Options.SingleActiveConsumer}, delivery-limit={Options.DeliveryLimit}, persistent + publisher confirms, " +
               $"client-side ordered retry, {_consumerConns.Length}+{_publishConns.Length} conns @ {where} vhost {_vhost}" +
               (Options.TestNackProbability > 0 ? $", TEST nack p={Options.TestNackProbability} delay={TestNackDelay.TotalMilliseconds}ms" : "");
    }

    public async ValueTask DisposeAsync()
    {
        long injected = Interlocked.Read(ref TestNacks);
        if (injected > 0) _log($"rabbitmq: {injected:N0} test nacks injected (Broker:TestNackProbability)");
        foreach (var slot in _consumerConns) await slot.DisposeAsync();
        foreach (var slot in _publishConns) await slot.DisposeAsync();
        await _admin.DisposeAsync();
        _http?.Dispose();
    }

    /// <summary>A lazily (re)created connection.</summary>
    private sealed class ConnectionSlot(ConnectionFactory factory, string name) : IAsyncDisposable
    {
        private readonly SemaphoreSlim _lock = new(1, 1);
        private IConnection? _conn;

        public async ValueTask<IConnection> GetAsync(CancellationToken ct)
        {
            var c = _conn;
            if (c is { IsOpen: true }) return c;
            await _lock.WaitAsync(ct);
            try
            {
                if (_conn is { IsOpen: true }) return _conn;
                if (_conn != null)
                {
                    try { await _conn.DisposeAsync(); } catch { /* already broken */ }
                }
                _conn = await factory.CreateConnectionAsync(name, ct);
                return _conn;
            }
            finally { _lock.Release(); }
        }

        public async ValueTask DisposeAsync()
        {
            var c = _conn;
            _conn = null;
            if (c is null) return;
            try
            {
                if (c.IsOpen) await c.CloseAsync();
                await c.DisposeAsync();
            }
            catch { /* best effort */ }
        }
    }
}

public sealed class RabbitMqBrokerFactory : IBrokerFactory
{
    public string Type => "rabbitmq";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new RabbitMqBroker(section.Get<RabbitMqOptions>() ?? new(), context);
}
