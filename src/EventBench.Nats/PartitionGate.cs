using EventBench.Core.Model;

namespace EventBench.Nats;

/// <summary>Deterministic source → partition → worker mapping (same scheme as PgmqBroker Partitioned).</summary>
public static class NatsPartitioning
{
    public static int Partitions(GroupInfo g, int partitionsPerGroup) => partitionsPerGroup > 0 ? partitionsPerGroup : g.Workers;

    public static int PartitionOf(GroupInfo g, int sourceId, int partitions) => (sourceId - g.FirstSourceId) % partitions;

    /// <summary>Partitions owned by one worker: each partition has exactly one owner.</summary>
    public static int[] Owned(int partitions, int workers, int workerIndex)
        => Enumerable.Range(0, partitions).Where(p => p % workers == workerIndex).ToArray();
}

public enum GateState
{
    /// <summary>Messages may be fetched from the partition.</summary>
    Open,
    /// <summary>A delivery was nacked and the retry delay has not elapsed: hand out nothing from this partition.</summary>
    Paused,
    /// <summary>The retry delay elapsed: the consumer must be rewound to the oldest unacked message before fetching.</summary>
    ResetDue,
}

/// <summary>
/// Nack bookkeeping of one partition. A nack never goes to the server (a JetStream NAK would let later
/// messages of the same subject overtake the nacked one). Instead the partition is paused and, once the
/// delay elapsed, its consumer is recreated so delivery restarts in stream order from the oldest unacked
/// message. Thread-safe: deliveries of one batch may be nacked from parallel lanes.
/// </summary>
public sealed class PartitionGate
{
    private readonly object _lock = new();
    private long _resumeAtUs;
    private long _nacks;
    private long _nacksAtReset;
    private ulong _failedSeq = ulong.MaxValue;

    /// <summary>Lowest stream sequence nacked since the last reset (diagnostics).</summary>
    public ulong FailedStreamSeq { get { lock (_lock) return _failedSeq; } }

    public long TotalNacks { get { lock (_lock) return _nacks; } }

    public void OnNack(ulong streamSeq, long nowUs, long delayUs)
    {
        lock (_lock)
        {
            _nacks++;
            _resumeAtUs = Math.Max(_resumeAtUs, nowUs + Math.Max(0, delayUs));
            if (streamSeq < _failedSeq) _failedSeq = streamSeq;
        }
    }

    public GateState Check(long nowUs, out long nackMark)
    {
        lock (_lock)
        {
            nackMark = _nacks;
            if (_nacks == _nacksAtReset) return GateState.Open;
            return nowUs < _resumeAtUs ? GateState.Paused : GateState.ResetDue;
        }
    }

    /// <summary>Marks the reset done for all nacks up to <paramref name="nackMark"/> (a later nack keeps the gate closed).</summary>
    public void OnResetDone(long nackMark)
    {
        lock (_lock)
        {
            if (nackMark > _nacksAtReset) _nacksAtReset = nackMark;
            if (_nacksAtReset == _nacks) _failedSeq = ulong.MaxValue;
        }
    }
}
