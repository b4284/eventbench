using Confluent.Kafka;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;

namespace EventBench.Kafka;

/// <summary>
/// Consumer of one worker: owns a fixed set of partitions of its group's topic (manual assign).
/// All librdkafka calls happen under <see cref="_gate"/> and never block for long: <c>Consume(0)</c>
/// only drains the local prefetch queue (librdkafka fetches in the background), the caller's idle backoff
/// does the waiting. Acks/nacks update in-memory trackers; an ack that extends a partition's contiguous
/// acked prefix stores that offset, and librdkafka auto-commits stored offsets every <c>CommitIntervalMs</c>
/// in the background (plus a synchronous commit on dispose).
/// </summary>
internal sealed class KafkaEventConsumer : IEventConsumer
{
    internal sealed class Part(TopicPartition tp)
    {
        public readonly TopicPartition Tp = tp;
        public readonly PartitionOffsetTracker Tracker = new();
        /// <summary>Fetched but not yet handed out, in offset order.</summary>
        public readonly Queue<ConsumeResult<Ignore, byte[]>> Buffer = new();
        /// <summary>Messages below this offset were already accepted: drop them (refetch after pause/seek).</summary>
        public long NextExpected = -1;
        /// <summary>After a seek: the next accepted message must have exactly this offset.</summary>
        public long MustStartAt = -1;
        /// <summary>Highest offset ever handed out (redeliveries at or below it report DeliveryCount 2).</summary>
        public long MaxHandedOut = -1;
        public bool Paused;
    }

    private readonly KafkaBroker _broker;
    private readonly IConsumer<Ignore, byte[]> _consumer;
    private readonly Part[] _parts;
    private readonly Dictionary<int, Part> _byPartition;
    private readonly object _gate = new();
    private long _nextToken;
    private int _rr;
    private bool _disposed;

    public KafkaEventConsumer(KafkaBroker broker, int groupId, int workerIndex, int[] partitions)
    {
        _broker = broker;
        _parts = partitions.Select(p => new Part(new TopicPartition(broker.Topic(groupId), p))).ToArray();
        _byPartition = _parts.ToDictionary(p => p.Tp.Partition.Value);
        _consumer = broker.BuildConsumer(groupId, workerIndex);
        // Offset.Stored = resume from the group's committed offset (earliest when nothing is committed).
        _consumer.Assign(_parts.Select(p => new TopicPartitionOffset(p.Tp, Offset.Stored)));
    }

    public ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
    {
        List<(Part Part, ConsumeResult<Ignore, byte[]> Msg, long Token, bool Redelivery)>? taken = null;
        long now;
        lock (_gate)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            now = Clock.NowUs();
            ApplyRewinds(now);
            Fill(maxCount);

            for (int i = 0; i < _parts.Length && (taken?.Count ?? 0) < maxCount; i++)
            {
                var p = _parts[(_rr + i) % _parts.Length];
                // Never hand out more of a partition while any of its messages is unresolved: keeps the
                // per-source contract even if a caller asks again before acking.
                if (!p.Tracker.CanHandOut) continue;
                while ((taken?.Count ?? 0) < maxCount && p.Buffer.TryDequeue(out var cr))
                {
                    long offset = cr.Offset.Value;
                    long token = ++_nextToken;
                    p.Tracker.Register(offset, token);
                    bool redelivery = offset <= p.MaxHandedOut;
                    if (offset > p.MaxHandedOut) p.MaxHandedOut = offset;
                    (taken ??= new()).Add((p, cr, token, redelivery));
                }
            }
            _rr = (_rr + 1) % _parts.Length;
        }

        if (taken is null) return ValueTask.FromResult<IReadOnlyList<IDelivery>>([]);
        long received = Clock.NowUs();
        var list = new IDelivery[taken.Count];
        for (int i = 0; i < taken.Count; i++)
        {
            var (part, msg, token, redelivery) = taken[i];
            list[i] = new KafkaDelivery(this, part, msg.Offset.Value, token,
                EventEnvelope.FromJson(msg.Message.Value), received,
                Clock.ToUs(msg.Message.Timestamp.UtcDateTime), redelivery ? 2 : 1);
        }
        return ValueTask.FromResult<IReadOnlyList<IDelivery>>(list);
    }

    /// <summary>Pause partitions with a pending rewind; seek them back once their nack delay elapsed.</summary>
    private void ApplyRewinds(long now)
    {
        foreach (var p in _parts)
        {
            if (!p.Tracker.RewindPending) continue;
            p.Buffer.Clear();
            if (p.Tracker.TryTakeRewind(now, out long offset))
            {
                if (p.Paused)
                {
                    _consumer.Resume([p.Tp]);
                    p.Paused = false;
                }
                // Seek purges librdkafka's prefetched messages for the partition (op versioning); the
                // MustStartAt filter additionally guarantees nothing after `offset` slips in first.
                _consumer.Seek(new TopicPartitionOffset(p.Tp, offset));
                p.MustStartAt = offset;
                p.NextExpected = offset;
                Interlocked.Increment(ref _broker.Rewinds);
            }
            else if (!p.Paused)
            {
                // Stop fetching while waiting for the nack delay / outstanding stale deliveries.
                _consumer.Pause([p.Tp]);
                p.Paused = true;
            }
        }
    }

    /// <summary>Move messages from librdkafka's local queue into the per-partition buffers.</summary>
    private void Fill(int maxCount)
    {
        int ready = 0;
        foreach (var p in _parts)
            if (p.Tracker.CanHandOut) ready += p.Buffer.Count;

        int budget = maxCount * 4 + 16;
        while (ready < maxCount && budget-- > 0)
        {
            var cr = _consumer.Consume(TimeSpan.Zero);
            if (cr is null) break;
            if (cr.IsPartitionEOF || !_byPartition.TryGetValue(cr.Partition.Value, out var p)) continue;
            long offset = cr.Offset.Value;
            if (p.Tracker.RewindPending) { Discard(); continue; }
            if (p.MustStartAt >= 0)
            {
                if (offset != p.MustStartAt) { Discard(); continue; }
                p.MustStartAt = -1;
            }
            else if (offset < p.NextExpected) { Discard(); continue; }
            p.NextExpected = offset + 1;
            p.Buffer.Enqueue(cr);
            if (p.Tracker.CanHandOut) ready++;
        }
    }

    private void Discard() => Interlocked.Increment(ref _broker.DiscardedMessages);

    internal void Ack(Part part, long offset, long token)
    {
        lock (_gate)
        {
            if (_disposed || !part.Tracker.Ack(offset, token)) return;
            // Only the contiguous prefix is ever stored; librdkafka's auto-commit (enable.auto.offset.store
            // = false) sends the stored offsets to the group coordinator in the background.
            try { _consumer.StoreOffset(new TopicPartitionOffset(part.Tp, part.Tracker.CommitPosition)); }
            catch (KafkaException) { Interlocked.Increment(ref _broker.CommitErrors); }
        }
    }

    internal void Nack(Part part, long offset, long token, TimeSpan delay)
    {
        lock (_gate)
        {
            part.Tracker.Nack(offset, token, Clock.NowUs() + (long)(delay.TotalMilliseconds * 1000));
            if (part.Tracker.RewindPending) part.Buffer.Clear();
        }
    }

    public ValueTask DisposeAsync()
    {
        lock (_gate)
        {
            if (_disposed) return ValueTask.CompletedTask;
            _disposed = true;
            // Synchronously commit whatever was stored since the last auto-commit.
            var offsets = _parts.Where(p => p.Tracker.CommitPosition >= 0)
                .Select(p => new TopicPartitionOffset(p.Tp, p.Tracker.CommitPosition)).ToList();
            try { if (offsets.Count > 0) _consumer.Commit(offsets); }
            catch (KafkaException) { Interlocked.Increment(ref _broker.CommitErrors); }
            try { _consumer.Close(); } catch { /* best effort */ }
            _consumer.Dispose();
        }
        return ValueTask.CompletedTask;
    }

    private sealed class KafkaDelivery(KafkaEventConsumer owner, Part part, long offset, long token, EventEnvelope ev,
        long receivedUs, long enqueuedUs, int deliveryCount) : IDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => deliveryCount;

        public ValueTask AckAsync(CancellationToken ct)
        {
            owner.Ack(part, offset, token);
            return ValueTask.CompletedTask;
        }

        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            owner.Nack(part, offset, token, delay);
            return ValueTask.CompletedTask;
        }
    }
}
