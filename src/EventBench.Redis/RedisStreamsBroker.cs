using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace EventBench.Redis;

public sealed class RedisStreamsOptions
{
    /// <summary>StackExchange.Redis configuration string, e.g. "127.0.0.1:6379".</summary>
    public string Configuration { get; set; } = "127.0.0.1:6379";
    public string KeyPrefix { get; set; } = "ebq";
    /// <summary>Streams per group (0 = one per worker). Each stream is owned by exactly one worker.</summary>
    public int PartitionsPerGroup { get; set; } = 0;
    /// <summary>Multiplexed connections shared by all publishers and consumers.</summary>
    public int Connections { get; set; } = 8;
    /// <summary>XDEL acknowledged entries (keeps memory bounded and XLEN equal to the backlog).</summary>
    public bool DeleteOnAck { get; set; } = true;
    /// <summary>
    /// After each publish batch, WAITAOF 1 0: return only once the batch is fsynced to the local AOF
    /// (fsync-parity mode; with appendfsync everysec this waits for the next once-a-second fsync).
    /// </summary>
    public bool WaitAof { get; set; } = false;
    /// <summary>After each publish batch, WAIT this many replicas (0 = async replication).</summary>
    public int WaitReplicas { get; set; } = 0;
    public int WaitTimeoutMs { get; set; } = 1000;
}

/// <summary>Exposes the stream identity of a delivery so the Redis store can XACK it inside its commit script.</summary>
public interface IRedisStreamDelivery
{
    string Stream { get; }
    string ConsumerGroup { get; }
    string EntryId { get; }
    string Configuration { get; }
    bool DeleteOnAck { get; }
}

/// <summary>
/// Redis Streams broker. Per-source order with a single consumer per stream (Kafka-style partitions):
/// source -> stream by (source - first source of group) % partitions, each stream read by exactly one
/// worker through a consumer group (XREADGROUP; entries stay in the PEL until XACK).
/// A nack pauses the stream: after the retry delay the consumer re-reads its pending entries (ID 0)
/// in stream order before taking new ones (ID &gt;), so a failed event is never overtaken.
/// </summary>
public sealed class RedisStreamsBroker : IBroker, IStatsProvider
{
    internal const string Group = "w";
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly RedisStats _stats;
    internal readonly RedisPool Pool;

    public RedisStreamsOptions Options { get; }
    public string Name => "redis-streams";
    public IStatsProvider Stats => this;

    public RedisStreamsBroker(RedisStreamsOptions options, Topology topology, Action<string> log)
    {
        Options = options;
        _topology = topology;
        _log = log;
        Pool = new RedisPool(options.Configuration, options.Connections);
        _stats = new RedisStats(Pool, "redisq", log);
    }

    private int Partitions(GroupInfo g) => Options.PartitionsPerGroup > 0 ? Math.Max(Options.PartitionsPerGroup, g.Workers) : g.Workers;
    internal string StreamKey(int group, int partition) => $"{Options.KeyPrefix}:s:{group}:{partition}";

    internal string StreamFor(EventEnvelope ev)
    {
        var g = _topology.Groups[ev.Group];
        return StreamKey(ev.Group, (ev.SourceId - g.FirstSourceId) % Partitions(g));
    }

    private IEnumerable<string> AllStreams()
    {
        foreach (var g in _topology.Groups)
            for (int p = 0; p < Partitions(g); p++) yield return StreamKey(g.Id, p);
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        long dropped = await Pool.DeleteByPatternAsync(Options.KeyPrefix + ":*");
        var db = Pool.Db(0);
        var tasks = AllStreams().Select(s => db.StreamCreateConsumerGroupAsync(s, Group, StreamPosition.NewMessages, createStream: true)).ToList();
        await Task.WhenAll(tasks);
        var fsync = await db.ExecuteAsync("CONFIG", "GET", "appendfsync");
        _log($"redis streams: dropped {dropped} old keys, created {tasks.Count} streams (consumer group '{Group}'); appendfsync={((RedisResult[])fsync!)[1]}");
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        var g = _topology.Groups[groupId];
        var owned = Enumerable.Range(0, Partitions(g)).Where(p => p % g.Workers == workerIndex).Select(p => StreamKey(groupId, p)).ToArray();
        return new Consumer(this, owned, $"c{workerIndex}", Pool.Db(groupId * 31 + workerIndex));
    }

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        var db = Pool.Db(0);
        if (Options.DeleteOnAck)
        {
            var lens = await Task.WhenAll(AllStreams().Select(s => db.StreamLengthAsync(s)));
            return lens.Sum();
        }
        // Without XDEL: entries not yet delivered + delivered but unacked.
        long total = 0;
        foreach (var s in AllStreams())
        {
            var info = await db.StreamGroupInfoAsync(s);
            total += info.Sum(i => i.PendingMessageCount + (i.Lag ?? 0));
        }
        return total;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, double>(await _stats.SampleAsync(ct));
        if (Options.DeleteOnAck)
        {
            try { result["broker_backlog"] = await GetBacklogAsync(ct); }
            catch (Exception e) when (e is not OperationCanceledException) { _log($"redis backlog sample failed: {e.Message}"); }
        }
        return result;
    }

    public string Describe()
        => $"redis streams @ {Options.Configuration}, {(Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup.ToString() : "=workers")} streams per group, " +
           $"single consumer per stream, {Options.Connections} connections, {(Options.DeleteOnAck ? "XACK+XDEL" : "XACK")} on ack, waitaof={Options.WaitAof}, wait replicas={Options.WaitReplicas}";

    public ValueTask DisposeAsync() => Pool.DisposeAsync();

    // ------------------------------------------------------------------------------------------------

    private sealed class Publisher(RedisStreamsBroker broker) : IEventPublisher
    {
        private static int _seed;
        private readonly IDatabase _db = broker.Pool.Db(Interlocked.Increment(ref _seed));

        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            if (events.Count == 0) return;
            // MULTI/EXEC: the batch is appended atomically and in list order (per-stream order follows).
            var tx = _db.CreateTransaction();
            var adds = new Task[events.Count];
            for (int i = 0; i < events.Count; i++)
            {
                var ev = events[i];
                adds[i] = tx.StreamAddAsync(broker.StreamFor(ev), "d", JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope));
            }
            if (!await tx.ExecuteAsync()) throw new InvalidOperationException("redis MULTI/EXEC aborted");
            await Task.WhenAll(adds);
            if (broker.Options.WaitAof) await _db.ExecuteAsync("WAITAOF", 1, 0, 0);
            if (broker.Options.WaitReplicas > 0) await RedisPool.WaitReplicasAsync(_db, broker.Options.WaitReplicas, broker.Options.WaitTimeoutMs);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class StreamState(string key)
    {
        public readonly string Key = key;
        public bool Replay;
        public long ReplayAtUs;
    }

    private sealed class Consumer(RedisStreamsBroker broker, string[] streams, string name, IDatabase db) : IEventConsumer
    {
        private readonly StreamState[] _streams = streams.Select(s => new StreamState(s)).ToArray();
        private int _next;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            for (int attempt = 0; attempt < _streams.Length; attempt++)
            {
                var st = _streams[_next];
                _next = (_next + 1) % _streams.Length;
                var result = await ReadAsync(st, maxCount);
                if (result.Count > 0) return result;
            }
            return [];
        }

        private async Task<List<IDelivery>> ReadAsync(StreamState st, int maxCount)
        {
            StreamEntry[] entries;
            if (st.Replay)
            {
                if (Clock.NowUs() < st.ReplayAtUs) return [];
                // Pending entries of this consumer, oldest first: the nacked head and anything after it.
                entries = await db.StreamReadGroupAsync(st.Key, Group, name, "0", maxCount);
                if (entries.Length == 0) st.Replay = false;
            }
            else entries = [];
            if (!st.Replay)
                entries = await db.StreamReadGroupAsync(st.Key, Group, name, StreamPosition.NewMessages, maxCount);

            var list = new List<IDelivery>(entries.Length);
            if (entries.Length == 0) return list;
            long now = Clock.NowUs();
            foreach (var e in entries)
            {
                if (e.IsNull) continue; // deleted while pending
                var body = (byte[]?)e["d"];
                if (body is null) continue;
                list.Add(new Delivery(this, st, e.Id!, EnqueuedUs(e.Id!), EventEnvelope.FromJson(body), now));
            }
            return list;
        }

        // Stream IDs are "<ms since epoch>-<seq>": the broker-side enqueue time for free.
        private static long EnqueuedUs(string id) => long.TryParse(id.AsSpan(0, id.IndexOf('-')), out var ms) ? ms * 1000 : 0;

        internal void Nack(StreamState st, TimeSpan delay)
        {
            st.Replay = true;
            st.ReplayAtUs = Math.Max(st.ReplayAtUs, Clock.NowUs() + (long)(delay.TotalMilliseconds * 1000));
        }

        internal async ValueTask AckAsync(string stream, string id)
        {
            if (broker.Options.DeleteOnAck)
            {
                var b = db.CreateBatch();
                var a = b.StreamAcknowledgeAsync(stream, Group, id);
                var d = b.StreamDeleteAsync(stream, [id]);
                b.Execute();
                await Task.WhenAll(a, d);
            }
            else await db.StreamAcknowledgeAsync(stream, Group, id);
        }

        internal RedisStreamsBroker Broker => broker;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Delivery(Consumer owner, StreamState st, string id, long enqueuedUs, EventEnvelope ev, long receivedUs)
        : IDelivery, IRedisStreamDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => st.Replay ? 2 : 1;
        public string Stream => st.Key;
        public string ConsumerGroup => Group;
        public string EntryId => id;
        public string Configuration => owner.Broker.Options.Configuration;
        public bool DeleteOnAck => owner.Broker.Options.DeleteOnAck;

        public ValueTask AckAsync(CancellationToken ct) => owner.AckAsync(st.Key, id);

        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            owner.Nack(st, delay);
            return ValueTask.CompletedTask;
        }
    }
}

public sealed class RedisStreamsBrokerFactory : IBrokerFactory
{
    public string Type => "redis";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new RedisStreamsBroker(section.Get<RedisStreamsOptions>() ?? new(), context.Topology, context.Log);
}
