using System.Text.Json;
using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace EventBench.MassTransit;

public sealed class MtSqlOptions
{
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "bench";
    public string Username { get; set; } = "bench";
    public string Password { get; set; } = "bench";
    public string Schema { get; set; } = "transport";
    public string Role { get; set; } = "transport";
    /// <summary>Connections the transport may open (shared by all receive endpoints and sends).</summary>
    public int ConnectionLimit { get; set; } = 50;
    public SqlReceiveMode ReceiveMode { get; set; } = SqlReceiveMode.PartitionedOrdered;
    /// <summary>Messages per partition key in flight (1 = strict order).</summary>
    public int ConcurrentDeliveryLimit { get; set; } = 1;
    public int LockDurationSeconds { get; set; } = 60;
    /// <summary>Redelivery delay of a failed message (the transport keeps it at the head of its partition).</summary>
    public int UnlockDelayMs { get; set; } = 250;
    /// <summary>Strict order: a message must never leave the queue because it failed too often.</summary>
    public int MaxDeliveryCount { get; set; } = 1_000_000;
    public string QueuePrefix { get; set; } = "eb-g";
    public int ReceiveWaitMs { get; set; } = 500;
}

/// <summary>The message as stored by the transport: the event envelope as UTF-8 JSON.</summary>
public sealed class EbMessage
{
    public byte[] Body { get; set; } = [];
}

/// <summary>
/// MassTransit v8 SQL transport (Postgres, Apache-2.0), one queue per group, partition key = source id, receive
/// mode PartitionedOrdered (FIFO, one message per partition key at a time). MassTransit pushes messages to a
/// handler; the handler hands them to the bench's workers through a per-group channel and completes when the
/// worker acks (the transport then deletes the message) or nacks (the handler throws, faults are rethrown to the
/// transport, which unlocks the message after UnlockDelay; it stays at the head of its partition).
/// </summary>
public sealed class MtSqlBroker : IBroker, IStatsProvider
{
    private readonly MtSqlOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly Channel<MtDelivery>[] _channels;
    private readonly string _admin;
    private ServiceProvider? _sp;
    private List<IHostedService> _hosted = new();
    private IBus? _bus;
    private readonly ISendEndpoint?[] _endpoints;
    private long _handled, _faulted;

    public string Name => "mtsql";
    public IStatsProvider Stats => this;

    public MtSqlBroker(MtSqlOptions o, Topology topology, Action<string> log)
    {
        _o = o;
        _topology = topology;
        _log = log;
        _channels = topology.Groups.Select(_ => Channel.CreateUnbounded<MtDelivery>()).ToArray();
        _endpoints = new ISendEndpoint?[topology.Groups.Count];
        _admin = $"Host={o.Host};Port={o.Port};Database={o.Database};Username={o.Username};Password={o.Password};Maximum Pool Size=4;Command Timeout=600";
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        await using (var conn = new NpgsqlConnection(_admin))
        {
            await conn.OpenAsync(ct);
            await using var cmd = new NpgsqlCommand($"DROP SCHEMA IF EXISTS \"{_o.Schema}\" CASCADE", conn);
            await cmd.ExecuteNonQueryAsync(ct);
        }

        var services = new ServiceCollection();
        services.AddLogging(b => b.SetMinimumLevel(LogLevel.Warning).AddProvider(new ForwardingLoggerProvider(_log)));
        services.AddOptions<SqlTransportOptions>().Configure(t =>
        {
            t.Host = _o.Host;
            t.Port = _o.Port;
            t.Database = _o.Database;
            t.Schema = _o.Schema;
            t.Role = _o.Role;
            t.Username = _o.Username;
            t.Password = _o.Password;
            t.AdminUsername = _o.Username;
            t.AdminPassword = _o.Password;
            t.ConnectionLimit = _o.ConnectionLimit;
        });
        services.AddPostgresMigrationHostedService(create: true, delete: false);
        services.AddMassTransit(x =>
        {
            x.UsingPostgres((context, cfg) =>
            {
                foreach (var g in _topology.Groups)
                {
                    int slots = Math.Max(1, g.Workers * Math.Max(1, _slotsPerWorker));
                    var channel = _channels[g.Id];
                    cfg.ReceiveEndpoint($"{_o.QueuePrefix}{g.Id}", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.SetReceiveMode(_o.ReceiveMode, _o.ConcurrentDeliveryLimit);
                        e.PrefetchCount = slots;
                        e.ConcurrentMessageLimit = slots;
                        e.LockDuration = TimeSpan.FromSeconds(_o.LockDurationSeconds);
                        e.UnlockDelay = TimeSpan.FromMilliseconds(_o.UnlockDelayMs);
                        e.MaxDeliveryCount = _o.MaxDeliveryCount;
                        e.RethrowFaultedMessages();
                        e.Handler<EbMessage>(ctx => HandleAsync(channel, ctx));
                    });
                }
            });
        });
        _sp = services.BuildServiceProvider();
        _hosted = _sp.GetServices<IHostedService>().ToList();
        foreach (var h in _hosted) await h.StartAsync(ct);
        _bus = _sp.GetRequiredService<IBus>();
        for (int g = 0; g < _endpoints.Length; g++)
            _endpoints[g] = await _bus.GetSendEndpoint(new Uri($"queue:{_o.QueuePrefix}{g}"));
        _log($"mtsql: MassTransit {typeof(IBus).Assembly.GetName().Version} SQL transport, {_endpoints.Length} queues, " +
             $"mode {_o.ReceiveMode} (limit {_o.ConcurrentDeliveryLimit}/key), connection limit {_o.ConnectionLimit}");
    }

    internal int _slotsPerWorker = 4;

    private async Task HandleAsync(Channel<MtDelivery> channel, ConsumeContext<EbMessage> ctx)
    {
        var ev = EventEnvelope.FromJson(ctx.Message.Body);
        long enq = ctx.SentTime is { } st ? Clock.ToUs(st) : 0;
        var d = new MtDelivery(ev, enq, Clock.NowUs(), (ctx.ReceiveContext.Redelivered ? 2 : 1));
        channel.Writer.TryWrite(d);
        bool ok = await d.Done.Task.WaitAsync(ctx.CancellationToken);
        Interlocked.Increment(ref _handled);
        if (!ok)
        {
            Interlocked.Increment(ref _faulted);
            // Rethrown to the transport: the message is unlocked after UnlockDelay and stays first in its partition.
            throw new RetryLaterException();
        }
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, _channels[groupId]);

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        await using var conn = new NpgsqlConnection(_admin);
        await conn.OpenAsync(ct);
        await using var cmd = new NpgsqlCommand(
            $"SELECT count(*) FROM \"{_o.Schema}\".message_delivery md JOIN \"{_o.Schema}\".queue q ON q.id = md.queue_id " +
            $"WHERE q.name LIKE '{_o.QueuePrefix}%'", conn);
        return (long)(await cmd.ExecuteScalarAsync(ct))!;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var r = new Dictionary<string, double>();
        await using var conn = new NpgsqlConnection(_admin);
        await conn.OpenAsync(ct);
        // type 1 = the queue itself; 2 = _error, 3 = _skipped (dead letter). Anything there broke strict order.
        await using var cmd = new NpgsqlCommand(
            $"SELECT q.type, count(*) FROM \"{_o.Schema}\".message_delivery md JOIN \"{_o.Schema}\".queue q ON q.id = md.queue_id " +
            $"WHERE q.name LIKE '{_o.QueuePrefix}%' GROUP BY q.type", conn);
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        double backlog = 0, side = 0;
        while (await rd.ReadAsync(ct))
        {
            if (rd.GetInt32(0) == 1) backlog += rd.GetInt64(1); else side += rd.GetInt64(1);
        }
        r["broker_backlog"] = backlog;
        r["mt_error_or_dead_letter_rows"] = side;
        r["mt_faulted_total"] = Interlocked.Read(ref _faulted);
        return r;
    }

    public string Describe()
        => $"MassTransit SQL transport (Postgres {_o.Host}:{_o.Port}/{_o.Database}, schema {_o.Schema}), queue per group, " +
           $"partition key = source, {_o.ReceiveMode} limit {_o.ConcurrentDeliveryLimit}, connection limit {_o.ConnectionLimit}, " +
           $"unlock delay {_o.UnlockDelayMs} ms, max delivery {_o.MaxDeliveryCount}";

    public async ValueTask DisposeAsync()
    {
        foreach (var ch in _channels) ch.Writer.TryComplete();
        for (int i = _hosted.Count - 1; i >= 0; i--)
        {
            try { await _hosted[i].StopAsync(CancellationToken.None).WaitAsync(TimeSpan.FromSeconds(30)); }
            catch (Exception e) { _log($"mtsql stop: {e.Message}"); }
        }
        if (_sp != null) await _sp.DisposeAsync();
    }

    private sealed class RetryLaterException() : Exception("worker asked for a retry");

    private sealed class Publisher(MtSqlBroker b) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            // One send per event, in order (the transport has no multi-row insert).
            foreach (var ev in events)
            {
                var body = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope);
                string key = ev.SourceId.ToString(System.Globalization.CultureInfo.InvariantCulture);
                await b._endpoints[ev.Group]!.Send(new EbMessage { Body = body }, ctx => ctx.SetPartitionKey(key), ct);
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(MtSqlBroker b, Channel<MtDelivery> channel) : IEventConsumer
    {
        public bool SharedSources => true;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            var list = new List<IDelivery>(maxCount);
            while (list.Count < maxCount && channel.Reader.TryRead(out var d)) list.Add(d);
            if (list.Count > 0) return list;
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(b._o.ReceiveWaitMs);
            try
            {
                if (!await channel.Reader.WaitToReadAsync(timeout.Token)) return list;
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested) { return list; }
            while (list.Count < maxCount && channel.Reader.TryRead(out var d)) list.Add(d);
            return list;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

/// <summary>MassTransit warnings and errors go to the run log.</summary>
internal sealed class ForwardingLoggerProvider(Action<string> log) : ILoggerProvider
{
    private int _lines;
    public ILogger CreateLogger(string categoryName) => new L(this, log, categoryName);
    public void Dispose() { }

    private sealed class L(ForwardingLoggerProvider p, Action<string> log, string cat) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Warning;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel) || Interlocked.Increment(ref p._lines) > 200) return; // cap the noise
            log($"mt {logLevel} {cat}: {formatter(state, exception)}{(exception != null ? $" ({exception.GetType().Name}: {exception.Message})" : "")}");
        }
    }
}

internal sealed class MtDelivery(EventEnvelope ev, long enqueuedUs, long receivedUs, int deliveryCount) : IDelivery
{
    public readonly TaskCompletionSource<bool> Done = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public EventEnvelope Event => ev;
    public long ReceivedAtUs => receivedUs;
    public long EnqueuedAtUs => enqueuedUs;
    public int DeliveryCount => deliveryCount;

    /// <summary>The handler returns; the transport deletes the message.</summary>
    public ValueTask AckAsync(CancellationToken ct)
    {
        Done.TrySetResult(true);
        return ValueTask.CompletedTask;
    }

    public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
    {
        Done.TrySetResult(false);
        return ValueTask.CompletedTask;
    }
}

public sealed class MtSqlBrokerFactory : IBrokerFactory
{
    public string Type => "mtsql";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new MtSqlBroker(section.Get<MtSqlOptions>() ?? new(), context.Topology, context.Log)
        {
            _slotsPerWorker = Math.Max(1, context.Config.Worker.MaxParallelPerWorker),
        };
}
