using EventBench.Core.Processing;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using EventBench.Postgres;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Octopus.Client;
using Octopus.V1;

namespace EventBench.Octopus;

public sealed class OctopusBrokerOptions
{
    public string MqttHost { get; set; } = "127.0.0.1";
    public int MqttPort { get; set; } = 1883;
    public string Namespace { get; set; } = "octopus";
    /// <summary>Direct connection for setup, backlog and stats (Octopus's own tables).</summary>
    public string AdminConnectionString { get; set; } = "";
    public string Schema { get; set; } = "octopus";
    /// <summary>MQTT connections shared by the producers, and by the workers.</summary>
    public int ProducerConnections { get; set; } = 4;
    public int WorkerConnections { get; set; } = 8;
    public int ReceiveWaitMs { get; set; } = 500;
    public int SetupWaitSeconds { get; set; } = 60;
    /// <summary>File listing the dispatcher process ids (one per line), for CPU stats.</summary>
    public string? DispatcherPidFile { get; set; }
}

/// <summary>
/// Octopus (https://github.com/b4284/octopus): events are published through the dispatcher cluster, which stores
/// them in Postgres, hands each source's next event to a free worker slot, and runs the commit (eb_commit through
/// the apply hook) in the same transaction as the cursor check. Groups are "g{id}", sources "{id}".
/// </summary>
public sealed class OctopusBroker : IBroker, IStatsProvider
{
    private readonly OctopusBrokerOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly int _slots;
    private readonly List<OctopusConnection> _producerConns = new();
    private readonly List<OctopusConnection> _workerConns = new();
    private readonly List<OctopusProducer> _producers = new();
    private readonly List<OctopusWorker> _workers = new();
    private int _nextProducer, _nextConn;
    private long _prevCpuTicks, _prevWall;

    public string Name => "octopus";
    public IStatsProvider Stats => this;
    /// <summary>Publish acknowledgements that reported missing positions below the item (a reordering hint).</summary>
    internal static long MissingReports;
    internal string Schema => _o.Schema;

    public OctopusBroker(OctopusBrokerOptions o, Topology topology, int slotsPerWorker, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(o.AdminConnectionString)) throw new InvalidOperationException("Broker:AdminConnectionString is required for octopus");
        _o = o;
        _topology = topology;
        _slots = slotsPerWorker;
        _log = log;
    }

    public static string Group(int id) => $"g{id}";

    public async Task SetupAsync(CancellationToken ct)
    {
        await using (var conn = new NpgsqlConnection(_o.AdminConnectionString))
        {
            await conn.OpenAsync(ct);
            await using (var cmd = new NpgsqlCommand($"SELECT count(*) FROM {_o.Schema}.member WHERE heartbeat > now() - interval '10 seconds'", conn))
            {
                long n = (long)(await cmd.ExecuteScalarAsync(ct))!;
                if (n == 0) throw new InvalidOperationException("no live Octopus dispatcher (start them with infra/octopus/up.sh)");
                _log($"octopus: {n} live dispatchers");
            }
            // Register every group now, so the dispatchers own them before the first event.
            await using (var cmd = new NpgsqlCommand($"INSERT INTO {_o.Schema}.grp (name) SELECT unnest($1::text[]) ON CONFLICT DO NOTHING", conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter { Value = _topology.Groups.Select(g => Group(g.Id)).ToArray() });
                await cmd.ExecuteNonQueryAsync(ct);
            }
        }
        string run = Guid.NewGuid().ToString("N")[..8];
        for (int i = 0; i < _o.ProducerConnections; i++)
        {
            var c = await OctopusConnection.ConnectAsync(Options($"eb-{run}-p{i}"), ct);
            _producerConns.Add(c);
            _producers.Add(new OctopusProducer(c));
        }
        for (int i = 0; i < _o.WorkerConnections; i++) _workerConns.Add(await OctopusConnection.ConnectAsync(Options($"eb-{run}-w{i}"), ct));

        // Wait until every group has a live owner.
        var until = DateTime.UtcNow.AddSeconds(_o.SetupWaitSeconds);
        var probe = _workerConns[0];
        while (true)
        {
            int owned = _topology.Groups.Count(g => probe.Owners.TryGetValue(Group(g.Id), out var m) && probe.Members.ContainsKey(m));
            if (owned == _topology.Groups.Count) break;
            if (DateTime.UtcNow > until) throw new TimeoutException($"only {owned} of {_topology.Groups.Count} groups have an owner");
            await Task.Delay(200, ct);
        }
        var perMember = probe.Owners.Values.GroupBy(m => m).Select(g => $"{g.Key}={g.Count()}");
        _log($"octopus: all {_topology.Groups.Count} groups owned ({string.Join(", ", perMember)}); " +
             $"{_o.ProducerConnections} producer + {_o.WorkerConnections} worker MQTT connections");
    }

    private OctopusOptions Options(string clientId) => new() { Host = _o.MqttHost, Port = _o.MqttPort, Namespace = _o.Namespace, ClientId = clientId };

    public IEventPublisher CreatePublisher(int groupId)
    {
        var p = _producers[Interlocked.Increment(ref _nextProducer) % _producers.Count];
        return new Publisher(p);
    }

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        var conn = _workerConns[Interlocked.Increment(ref _nextConn) % _workerConns.Count];
        var w = OctopusWorker.Create(conn, $"w{groupId}-{workerIndex}-{Guid.NewGuid().ToString("N")[..6]}", [Group(groupId)], _slots);
        lock (_workers)
        {
            if (_workers.Count == 0)
                WorkerRunner.ExtraDumps.Add(() => { lock (_workers) return _workers.Select(x => x.Describe()).Where(d => !d.Contains("running 0,")).ToList(); });
            _workers.Add(w);
        }
        return new Consumer(this, w);
    }

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        await using var conn = new NpgsqlConnection(_o.AdminConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new NpgsqlCommand(
            $"SELECT count(*) FROM {_o.Schema}.event e JOIN {_o.Schema}.source s ON s.id = e.sid WHERE (e.epoch, e.seq) > (s.epoch, s.last_seq)", conn);
        return (long)(await cmd.ExecuteScalarAsync(ct))!;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var r = new Dictionary<string, double>();
        try { r["broker_backlog"] = await GetBacklogAsync(ct); } catch (Exception e) when (e is not OperationCanceledException) { _log($"octopus backlog: {e.Message}"); }
        r["octopus_mqtt_sent_total"] = _producerConns.Concat(_workerConns).Sum(c => Interlocked.Read(ref c.Sent));
        r["octopus_items_sent_total"] = _producerConns.Concat(_workerConns).Sum(c => Interlocked.Read(ref c.ItemsSent));
        r["octopus_mqtt_received_total"] = _producerConns.Concat(_workerConns).Sum(c => Interlocked.Read(ref c.Received));
        r["octopus_publish_retries_total"] = _producers.Sum(p => Interlocked.Read(ref p.Retries));
        r["octopus_missing_reports_total"] = Interlocked.Read(ref MissingReports);
        // dispatcher processes' CPU (they run outside containers)
        string? f = _o.DispatcherPidFile?.Replace("~", Environment.GetEnvironmentVariable("HOME") ?? "~");
        if (f != null && File.Exists(f))
        {
            long ticks = 0; int alive = 0;
            foreach (var line in File.ReadAllLines(f))
            {
                if (!int.TryParse(line.Trim(), out int pid)) continue;
                try
                {
                    var stat = File.ReadAllText($"/proc/{pid}/stat");
                    var fields = stat[(stat.LastIndexOf(')') + 2)..].Split(' ');
                    ticks += long.Parse(fields[11]) + long.Parse(fields[12]); // utime + stime
                    alive++;
                }
                catch (IOException) { }
            }
            long wall = Environment.TickCount64;
            if (_prevWall > 0 && wall > _prevWall && ticks >= _prevCpuTicks)
                r["octopus_dispatcher_cores"] = Math.Round((ticks - _prevCpuTicks) / 100.0 / ((wall - _prevWall) / 1000.0), 2);
            r["octopus_dispatchers_alive"] = alive;
            (_prevCpuTicks, _prevWall) = (ticks, wall);
        }
        return r;
    }

    public string Describe()
        => $"Octopus dispatchers via MQTT {_o.MqttHost}:{_o.MqttPort} ns={_o.Namespace}, schema {_o.Schema}; one event per publish call " +
           $"({_o.ProducerConnections} producer / {_o.WorkerConnections} worker connections), commit through the apply hook";

    public async ValueTask DisposeAsync()
    {
        foreach (var w in _workers) { try { await w.DisposeAsync(); } catch { } }
        foreach (var c in _producerConns.Concat(_workerConns)) { try { await c.DisposeAsync(); } catch { } }
    }

    // ------------------------------------------------------------------------------------------------

    private sealed class Publisher(OctopusProducer p) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            if (events.Count == 0) return;
            var items = events.Select(e => new PublishItem
            {
                Group = Group(e.Group),
                Source = e.SourceId.ToString(System.Globalization.CultureInfo.InvariantCulture),
                Epoch = 1,              // the benchmark's sources never lose their counters
                Seq = e.Seq,
                Body = ByteString.CopyFrom(JsonSerializer.SerializeToUtf8Bytes(e, EventJsonContext.Default.EventEnvelope)),
            }).ToList();
            var acks = await p.PublishAsync(items, ct);
            foreach (var a in acks)
            {
                if (a.Status == PublishStatus.PublishGroupMismatch) throw new InvalidOperationException("octopus: source published to the wrong group");
                if (a.Status == PublishStatus.PublishConflict) throw new InvalidOperationException("octopus: a (source, epoch, seq) was published with a different body");
                if (a.Status is PublishStatus.PublishStaleEpoch or PublishStatus.PublishLate) throw new InvalidOperationException($"octopus: publish rejected ({a.Status})");
                if (a.MissingBelow > 0) Interlocked.Increment(ref MissingReports);
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(OctopusBroker b, OctopusWorker w) : IEventConsumer
    {
        public bool SharedSources => true;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            var list = new List<IDelivery>(maxCount);
            var reader = w.Deliveries;
            while (list.Count < maxCount && reader.TryRead(out var d)) list.Add(Wrap(d));
            if (list.Count > 0) return list;
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(b._o.ReceiveWaitMs);
            try { if (!await reader.WaitToReadAsync(timeout.Token)) return list; }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested) { return list; }
            while (list.Count < maxCount && reader.TryRead(out var d)) list.Add(Wrap(d));
            return list;
        }

        private IDelivery Wrap(OctopusDelivery d)
        {
            var ev = EventEnvelope.FromJson(d.Item.Body.Span);
            if (d.Item.HasContext) PreloadedContexts.Put(ev.SourceId, ev.Seq, OctopusStore.ParseContext(d.Item.Context.Span));
            return new BenchDelivery(w, d, ev);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

/// <summary>A delivery as the bench worker sees it. Commit goes through <see cref="OctopusStore"/>.</summary>
internal sealed class BenchDelivery(OctopusWorker worker, OctopusDelivery d, EventEnvelope ev) : IDelivery, IDeliveryCompletion
{
    public OctopusWorker Worker => worker;
    public OctopusDelivery Raw => d;
    public EventEnvelope Event => ev;
    public long ReceivedAtUs { get; } = Clock.NowUs();
    public long EnqueuedAtUs => d.Item.EnqueuedUs;
    public int DeliveryCount => d.Item.Attempts + 1;

    /// <summary>
    /// The bench worker has committed its own writes (AckMode=AfterCommit, store "postgres" through PgBouncer), or
    /// found the event already applied: report done, Octopus removes the event.
    /// </summary>
    public ValueTask AckAsync(CancellationToken ct)
    {
        worker.ReportDone(d);
        return ValueTask.CompletedTask;
    }

    public ValueTask NackAsync(TimeSpan delay, CancellationToken ct) => new(worker.Fail(d, delay, ct));

    public void Completed(bool success) => worker.Release(d);
}

public sealed class OctopusBrokerFactory : IBrokerFactory
{
    public string Type => "octopus";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new OctopusBroker(section.Get<OctopusBrokerOptions>() ?? new(), context.Topology,
            Math.Max(1, context.Config.Worker.MaxParallelPerWorker), context.Log);
}
