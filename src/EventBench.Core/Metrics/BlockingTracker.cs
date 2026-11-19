using EventBench.Core.Model;
using EventBench.Core.Util;

namespace EventBench.Core.Metrics;

/// <summary>
/// Measures cross-source blocking: for every event, how long it waited after becoming ready while
/// at least one worker slot of its group was idle.
///
/// An event is ready when it is in the broker (its publish call returned) and the previous event of its
/// source is done (or its own retry delay has passed). A slot is busy while it processes an event.
/// If an event waits ready while a slot of its group sits idle, something other than capacity held it
/// back: typically another source's slow event ahead of it in a partition or batch.
///
/// Per group, the tracker keeps a cumulative function F(t) = total time up to t with at least one idle
/// slot. It records a breakpoint only when that idle state flips. Idle overlap of [ready, start] is then
/// F(start) - F(ready).
/// </summary>
public sealed class BlockingTracker
{
    private const int PublishRing = 64;
    private const long HistoryHorizonUs = 180_000_000; // keep 3 min of idle-state breakpoints

    private sealed class GroupSlots(int slots, long t0)
    {
        public readonly int Slots = slots;
        public int Busy;
        public bool Idle = slots > 0;
        public readonly List<long> T = [t0];
        public readonly List<long> Acc = [0];
        public readonly List<bool> IdleAfter = [slots > 0];

        public long F(long t)
        {
            int n = T.Count;
            int i;
            if (t >= T[n - 1]) i = n - 1;
            else
            {
                int lo = 0, hi = n - 1;
                if (t < T[0]) return Acc[0]; // older than retained history: undercounts
                while (lo < hi)
                {
                    int mid = (lo + hi + 1) >> 1;
                    if (T[mid] <= t) lo = mid; else hi = mid - 1;
                }
                i = lo;
            }
            return Acc[i] + (IdleAfter[i] ? t - T[i] : 0);
        }

        public void Flip(long now, bool idle)
        {
            long acc = F(now);
            T.Add(now); Acc.Add(acc); IdleAfter.Add(idle);
            Idle = idle;
            if (T.Count > 8192 && now - T[0] > HistoryHorizonUs)
            {
                int cut = T.BinarySearch(now - HistoryHorizonUs);
                if (cut < 0) cut = ~cut;
                cut = Math.Max(0, Math.Min(cut - 1, T.Count - 2));
                if (cut > 0) { T.RemoveRange(0, cut); Acc.RemoveRange(0, cut); IdleAfter.RemoveRange(0, cut); }
            }
        }
    }

    private readonly GroupSlots[] _groups;
    private readonly long[] _sourceReadyUs;   // long.MaxValue while an event of the source is in flight
    private readonly long[] _inFlightSeq;
    private readonly long[] _pubSeq;
    private readonly long[] _pubUs;

    public BlockingTracker(Topology topology, int slotsPerWorker)
    {
        long now = Clock.NowUs();
        SlotsPerWorker = Math.Max(1, slotsPerWorker);
        _groups = topology.Groups.Select(g => new GroupSlots(g.Workers * SlotsPerWorker, now)).ToArray();
        _sourceReadyUs = new long[topology.TotalSources];
        _inFlightSeq = new long[topology.TotalSources];
        _pubSeq = new long[topology.TotalSources * PublishRing];
        _pubUs = new long[topology.TotalSources * PublishRing];
    }

    public int SlotsPerWorker { get; }

    /// <summary>Producer: the batch is in the broker as of now.</summary>
    public void OnPublished(IReadOnlyList<EventEnvelope> batch, long nowUs)
    {
        foreach (var ev in batch)
        {
            int i = ev.SourceId * PublishRing + (int)(ev.Seq % PublishRing);
            Volatile.Write(ref _pubUs[i], nowUs);
            Volatile.Write(ref _pubSeq[i], ev.Seq);
        }
    }

    /// <summary>When the event became ready to be processed.</summary>
    public long ReadyAt(EventEnvelope ev)
    {
        int i = ev.SourceId * PublishRing + (int)(ev.Seq % PublishRing);
        long pub = Volatile.Read(ref _pubSeq[i]) == ev.Seq ? Volatile.Read(ref _pubUs[i]) : 0;
        // Not recorded yet (consumer saw it before the publish call returned) or overwritten: send time.
        if (pub == 0 || pub < ev.SentAtUs) pub = ev.SentAtUs;
        long prev = Volatile.Read(ref _sourceReadyUs[ev.SourceId]);
        // Predecessor still marked in flight: it was acked a moment ago and is finishing its bookkeeping.
        if (prev == long.MaxValue) return long.MaxValue;
        return Math.Max(pub, prev);
    }

    /// <summary>An event of the source starts: the source's next event is not ready until this one is done.</summary>
    public void SourceStart(EventEnvelope ev)
    {
        Volatile.Write(ref _inFlightSeq[ev.SourceId], ev.Seq);
        Volatile.Write(ref _sourceReadyUs[ev.SourceId], long.MaxValue);
    }

    /// <summary>The event finished (applied, duplicate): the source's next event is ready from now.</summary>
    public void SourceDone(EventEnvelope ev, long nowUs)
    {
        // A later event of the source may already have started (the ack released it): leave its marker alone.
        if (Volatile.Read(ref _inFlightSeq[ev.SourceId]) == ev.Seq) Volatile.Write(ref _sourceReadyUs[ev.SourceId], nowUs);
    }

    /// <summary>The event failed and will be retried after the delay.</summary>
    public void SourceRetry(EventEnvelope ev, long readyUs)
    {
        if (Volatile.Read(ref _inFlightSeq[ev.SourceId]) == ev.Seq) Volatile.Write(ref _sourceReadyUs[ev.SourceId], readyUs);
    }

    public void SlotBusy(int group)
    {
        var g = _groups[group];
        lock (g)
        {
            g.Busy++;
            bool idle = g.Busy < g.Slots;
            if (idle != g.Idle) g.Flip(Clock.NowUs(), idle);
        }
    }

    public void SlotFree(int group)
    {
        var g = _groups[group];
        lock (g)
        {
            g.Busy--;
            bool idle = g.Busy < g.Slots;
            if (idle != g.Idle) g.Flip(Clock.NowUs(), idle);
        }
    }

    /// <summary>Time within [fromUs, toUs] during which at least one slot of the group was idle.</summary>
    public long IdleOverlap(int group, long fromUs, long toUs)
    {
        if (toUs <= fromUs) return 0;
        var g = _groups[group];
        lock (g) return Math.Max(0, g.F(toUs) - g.F(fromUs));
    }
}
