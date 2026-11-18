using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

namespace EventBench.Nats;

/// <summary>
/// NATS JetStream broker. One work-queue stream per group (<c>{prefix}_G{g}</c>, file storage) with one
/// subject per partition (<c>{subj}.g{g}.p{p}</c>, source → partition by index). Each partition has one
/// durable pull consumer (filter = its subject) owned by exactly one worker, so per-source order is
/// stream order. Ordering on failure: a nack is never sent to the server (JetStream would redeliver the
/// nak'd message after later ones); the partition is paused for the retry delay and its consumer is then
/// deleted and recreated (DeliverPolicy.All on a work-queue stream = restart at the oldest unacked
/// message), so the failed message and everything after it are redelivered in order. Acked messages are
/// removed by the work-queue retention; already applied re-deliveries are skipped by the store cursor.
/// </summary>
public sealed class NatsBroker : IBroker, IStatsProvider
{
    private readonly NatsBrokerOptions _o;
    private readonly Topology _topology;
    private readonly BenchConfig _config;
    private readonly Action<string> _log;
    private readonly NatsConnectionPool _pool;
    private readonly int _maxAckPending;
    private readonly TimeSpan _longPoll;
    private readonly System.Collections.Concurrent.ConcurrentBag<Partition> _partitions = new();
    private bool _connected;
    private long _serverMaxPayload;

    public string Name => "nats";
    public IStatsProvider Stats => this;

    public NatsBroker(NatsBrokerOptions options, BenchContext ctx)
    {
        _o = options;
        _topology = ctx.Topology;
        _config = ctx.Config;
        _log = ctx.Log;
        _maxAckPending = options.MaxAckPending > 0 ? options.MaxAckPending : Math.Max(1, ctx.Config.Worker.BatchSize) * 2;
        _longPoll = TimeSpan.FromMilliseconds(Math.Max(0, options.LongPollMs));
        _pool = new NatsConnectionPool(options.Url, "eventbench-broker", options.Connections,
            TimeSpan.FromSeconds(Math.Max(1, options.RequestTimeoutSeconds)));
        foreach (var g in _topology.Groups)
            if (Partitions(g) < g.Workers)
                throw new InvalidOperationException(
                    $"nats: group {g.Name} has {g.Workers} workers but only {Partitions(g)} partitions (workers would idle).");
    }

    private int Partitions(GroupInfo g) => NatsPartitioning.Partitions(g, _o.PartitionsPerGroup);
    internal string StreamName(int groupId) => $"{_o.StreamPrefix}_G{groupId}";
    internal string Subject(int groupId, int partition) => $"{_o.SubjectPrefix}.g{groupId}.p{partition}";
    internal static string ConsumerName(int partition, int generation) => $"p{partition}-g{generation}";

    internal string SubjectFor(EventEnvelope ev)
    {
        var g = _topology.Groups[ev.Group];
        return Subject(ev.Group, NatsPartitioning.PartitionOf(g, ev.SourceId, Partitions(g)));
    }

    private StreamConfigStorage StorageType =>
        string.Equals(_o.Storage, "Memory", StringComparison.OrdinalIgnoreCase) ? StreamConfigStorage.Memory : StreamConfigStorage.File;

    private async Task EnsureConnectedAsync()
    {
        if (_connected) return;
        await _pool.ConnectAsync();
        _serverMaxPayload = _pool.First.ServerInfo?.MaxPayload ?? 0;
        _connected = true;
    }

    internal ConsumerConfig ConsumerConfigFor(int groupId, int partition, int generation) => new(ConsumerName(partition, generation))
    {
        DurableName = ConsumerName(partition, generation),
        FilterSubject = Subject(groupId, partition),
        AckPolicy = ConsumerConfigAckPolicy.Explicit,
        DeliverPolicy = ConsumerConfigDeliverPolicy.All,
        AckWait = TimeSpan.FromSeconds(Math.Max(1, _o.AckWaitSeconds)),
        MaxAckPending = _maxAckPending,
        MaxDeliver = -1,
    };

    public async Task SetupAsync(CancellationToken ct)
    {
        await EnsureConnectedAsync();
        var js = _pool.FirstJs;
        var info = _pool.First.ServerInfo;
        _log($"nats: server {info?.Version} @ {_o.Url}, max_payload {_serverMaxPayload / (1024.0 * 1024):F0} MiB, jetstream={info?.JetStreamAvailable}");

        var lp = _config.Workload.LargePayload;
        bool large = (lp.Probability > 0 && lp.MaxCount > 0) || _topology.Groups.Any(g => g.LargeEventFraction > 0);
        if (large && lp.Mode == LargePayloadMode.Inline && _serverMaxPayload > 0)
        {
            long maxRaw = Math.Max(lp.MaxBytes, _topology.Groups.Max(g => g.LargeMaxBytes ?? 0));
            long wire = maxRaw * 4 / 3 + 4096;
            if (wire > _serverMaxPayload)
                throw new InvalidOperationException(
                    $"nats: inline large payloads need ~{wire / 1048576.0:F1} MiB messages but server max_payload is " +
                    $"{_serverMaxPayload / 1048576.0:F1} MiB (start the server with infra/nats/up.sh, which raises it).");
        }

        var prefix = _o.StreamPrefix + "_G";
        var existing = new List<string>();
        await foreach (var name in js.ListStreamNamesAsync(cancellationToken: ct))
            if (name.StartsWith(prefix, StringComparison.Ordinal)) existing.Add(name);
        foreach (var name in existing) await js.DeleteStreamAsync(name, ct);

        int consumers = 0;
        foreach (var g in _topology.Groups)
        {
            await js.CreateStreamAsync(new StreamConfig(StreamName(g.Id), [$"{_o.SubjectPrefix}.g{g.Id}.>"])
            {
                Storage = StorageType,
                Retention = StreamConfigRetention.Workqueue,
                Discard = StreamConfigDiscard.Old,
                NumReplicas = 1,
                MaxMsgSize = -1,
                DuplicateWindow = TimeSpan.FromSeconds(Math.Max(1, _o.DuplicateWindowSeconds)),
            }, ct);
            for (int p = 0; p < Partitions(g); p++)
            {
                await js.CreateOrUpdateConsumerAsync(StreamName(g.Id), ConsumerConfigFor(g.Id, p, 0), ct);
                consumers++;
            }
        }
        _log($"nats: dropped {existing.Count} old streams, created {_topology.Groups.Count} work-queue streams ({StorageType}) and {consumers} durable pull consumers");
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this, _pool.NextJs());

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        var g = _topology.Groups[groupId];
        var js = _pool.NextJs();
        var parts = NatsPartitioning.Owned(Partitions(g), g.Workers, workerIndex)
            .Select(p => new Partition(this, js, groupId, p)).ToArray();
        foreach (var p in parts) _partitions.Add(p);
        return new Consumer(parts);
    }

    private async Task<List<StreamInfo>> ListOurStreamsAsync(CancellationToken ct)
    {
        await EnsureConnectedAsync();
        var prefix = _o.StreamPrefix + "_G";
        var list = new List<StreamInfo>();
        await foreach (var s in _pool.FirstJs.ListStreamsAsync(cancellationToken: ct))
            if (s.Info.Config.Name is { } n && n.StartsWith(prefix, StringComparison.Ordinal)) list.Add(s.Info);
        return list;
    }

    /// <summary>Work-queue retention deletes acked messages, so the stream message count is the backlog.</summary>
    public async Task<long> GetBacklogAsync(CancellationToken ct)
        => (await ListOurStreamsAsync(ct)).Sum(s => s.State.Messages);

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var streams = await ListOurStreamsAsync(ct);
        return new Dictionary<string, double>
        {
            ["broker_backlog"] = streams.Sum(s => (double)s.State.Messages),
            ["broker_backlog_mb"] = streams.Sum(s => (double)s.State.Bytes) / (1024 * 1024),
            ["broker_max_stream_len"] = streams.Count == 0 ? 0 : streams.Max(s => (double)s.State.Messages),
            ["broker_consumer_resets"] = _partitions.Sum(p => (double)Interlocked.Read(ref p.Resets)),
            ["broker_delivery_gaps"] = _partitions.Sum(p => (double)Interlocked.Read(ref p.DeliveryGaps)),
        };
    }

    public string Describe()
        => $"nats jetstream @ {_o.Url}: work-queue stream/group ({_o.Storage}), " +
           $"{(_o.PartitionsPerGroup > 0 ? _o.PartitionsPerGroup.ToString() : "=workers")} partitions/group, 1 pull consumer/partition, " +
           $"ackWait={_o.AckWaitSeconds}s, maxAckPending={_maxAckPending}, nack=pause+consumer rewind, msg-id dedup {_o.DuplicateWindowSeconds}s, long-poll {_o.LongPollMs}ms, conns={_o.Connections}" +
           (_o.ChaosLoseDeliveryProbability > 0 ? $", CHAOS lose-delivery={_o.ChaosLoseDeliveryProbability}" : "");

    public ValueTask DisposeAsync() => _pool.DisposeAsync();

    // ------------------------------------------------------------------------------------------------

    private sealed class Publisher(NatsBroker broker, NatsJSContext js) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            // Per subject (= partition) strictly sequential, each publish awaits its PubAck: a failure stops
            // that subject's chain, so the stream can never contain seq n+1 of a source without seq n.
            // Different subjects are independent and run concurrently. The producer retries the whole batch
            // on failure; Nats-Msg-Id makes the already stored ones duplicates (no double delivery).
            var bySubject = new Dictionary<string, List<EventEnvelope>>();
            foreach (var ev in events)
            {
                var s = broker.SubjectFor(ev);
                if (!bySubject.TryGetValue(s, out var list)) bySubject[s] = list = new List<EventEnvelope>();
                list.Add(ev);
            }
            if (bySubject.Count == 1)
            {
                foreach (var (s, list) in bySubject) await PublishChainAsync(s, list, ct);
                return;
            }
            await Task.WhenAll(bySubject.Select(kv => PublishChainAsync(kv.Key, kv.Value, ct)));
        }

        private async Task PublishChainAsync(string subject, List<EventEnvelope> list, CancellationToken ct)
        {
            foreach (var ev in list)
            {
                var body = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope);
                var ack = await js.PublishAsync(subject, body,
                    opts: new NatsJSPubOpts { MsgId = $"{ev.SourceId}:{ev.Seq}" }, cancellationToken: ct);
                ack.EnsureSuccess();
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Partition(NatsBroker broker, NatsJSContext js, int groupId, int partition)
    {
        public readonly PartitionGate Gate = new();
        public int Generation;
        public long Resets;
        public long DeliveryGaps;
        private INatsJSConsumer? _consumer;
        private ulong _lastConsumerSeq;   // consumer (delivery) sequence of the last message handed out, this generation

        private string Stream => broker.StreamName(groupId);

        public async Task<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, bool longPoll, CancellationToken ct)
        {
            switch (Gate.Check(Clock.NowUs(), out long mark))
            {
                case GateState.Paused:
                    return [];
                case GateState.ResetDue:
                    await ResetAsync(ct);
                    Gate.OnResetDone(mark);
                    break;
            }

            _consumer ??= await js.GetConsumerAsync(Stream, ConsumerName(partition, Generation), ct);
            var result = new List<IDelivery>();
            bool ok = true;
            await foreach (var msg in _consumer.FetchNoWaitAsync<byte[]>(new NatsJSFetchOpts { MaxMsgs = maxCount }, cancellationToken: ct))
                if (!(ok = Accept(msg, result))) break;

            if (ok && result.Count == 0 && longPoll && broker._longPoll > TimeSpan.Zero)
            {
                // Idle: block for the first message (no idle-backoff latency), then take what else is ready.
                var first = await _consumer.NextAsync<byte[]>(opts: new NatsJSNextOpts { Expires = broker._longPoll }, cancellationToken: ct);
                if (first is { } m && (ok = Accept(m, result)) && maxCount > 1)
                    await foreach (var msg in _consumer.FetchNoWaitAsync<byte[]>(new NatsJSFetchOpts { MaxMsgs = maxCount - 1 }, cancellationToken: ct))
                        if (!(ok = Accept(msg, result))) break;
            }
            return result;
        }

        /// <summary>
        /// Hands a message out only if it is the next delivery of this consumer generation. The consumer
        /// sequence grows by exactly one per delivery (redeliveries included), so a jump means a delivery
        /// was lost on the way (e.g. a pull that timed out client-side): JetStream would redeliver it after
        /// AckWait, i.e. after later messages. Treat it like a nack: hand out nothing more, rewind now.
        /// </summary>
        private bool Accept(INatsJSMsg<byte[]> msg, List<IDelivery> result)
        {
            if (msg.Metadata is not { } md)
                throw new InvalidOperationException($"nats: message without JetStream metadata on {msg.Subject}");
            if (md.Sequence.Consumer != _lastConsumerSeq + 1)
            {
                Interlocked.Increment(ref DeliveryGaps);
                Gate.OnNack(md.Sequence.Stream, Clock.NowUs(), 0);
                return false;
            }
            if (msg.Data is null)
                throw new InvalidOperationException($"nats: message without data on {msg.Subject} (stream seq {md.Sequence.Stream})");
            if (broker._o.ChaosLoseDeliveryProbability > 0 && Random.Shared.NextDouble() < broker._o.ChaosLoseDeliveryProbability)
                return true; // chaos: pretend this delivery never arrived (the next one shows the gap)
            _lastConsumerSeq = md.Sequence.Consumer;
            var ev = EventEnvelope.FromJson(msg.Data);
            result.Add(new Delivery(this, msg, ev, md.Sequence.Stream, (int)md.NumDelivered,
                Clock.ToUs(md.Timestamp.UtcDateTime), Clock.NowUs()));
            return true;
        }

        /// <summary>
        /// Rewinds delivery to the oldest unacked message of this partition: delete the consumer (its pending
        /// deliveries die with it) and create the next generation with DeliverPolicy.All. Work-queue
        /// retention has already removed every acked message, so delivery restarts exactly at the first
        /// message not yet acknowledged, in stream order. Only called between batches (the worker has
        /// resolved every delivery of the previous batch); safe to retry if it fails midway.
        /// </summary>
        private async Task ResetAsync(CancellationToken ct)
        {
            _consumer = null;
            try
            {
                await js.DeleteConsumerAsync(Stream, ConsumerName(partition, Generation), ct);
            }
            catch (NatsJSApiException e) when (e.Error.Code == 404) { /* already gone (earlier attempt) */ }
            _consumer = await js.CreateOrUpdateConsumerAsync(Stream, broker.ConsumerConfigFor(groupId, partition, Generation + 1), ct);
            Generation++;
            _lastConsumerSeq = 0;
            Interlocked.Increment(ref Resets);
        }

        public void Nack(ulong streamSeq, TimeSpan delay)
            => Gate.OnNack(streamSeq, Clock.NowUs(), (long)delay.TotalMicroseconds);
    }

    private sealed class Consumer(Partition[] partitions) : IEventConsumer
    {
        private int _next;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            // Round-robin over owned partitions; return the first non-empty fetch (like PgmqBroker Partitioned).
            // Long-poll only when this worker owns a single partition (blocking would starve the others).
            for (int attempt = 0; attempt < partitions.Length; attempt++)
            {
                var p = partitions[_next];
                _next = (_next + 1) % partitions.Length;
                var result = await p.ReceiveAsync(maxCount, partitions.Length == 1, ct);
                if (result.Count > 0) return result;
            }
            return [];
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Delivery(Partition partition, INatsJSMsg<byte[]> msg, EventEnvelope ev,
        ulong streamSeq, int deliveryCount, long enqueuedUs, long receivedUs) : IDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => deliveryCount;

        public ValueTask AckAsync(CancellationToken ct) => msg.AckAsync(cancellationToken: ct);

        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            partition.Nack(streamSeq, delay);
            return ValueTask.CompletedTask;
        }
    }
}

public sealed class NatsBrokerFactory : IBrokerFactory
{
    public string Type => "nats";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new NatsBroker(section.Get<NatsBrokerOptions>() ?? new(), context);
}
