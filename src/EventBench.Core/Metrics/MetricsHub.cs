using System.Collections.Concurrent;
using EventBench.Core.Model;

namespace EventBench.Core.Metrics;

/// <summary>Per-group counters. Plain fields updated with Interlocked.</summary>
public sealed class GroupCounters
{
    public long Produced;
    public long ClosingProduced;
    public long Applied;
    public long Duplicates;
    public long Gaps;
    public long StateMismatches;
    public long Errors;
    public long Nacks;
    public long LargeEvents;
    public long PublishErrors;
    public long BytesPublished;
}

/// <summary>Central sink for all benchmark measurements.</summary>
public sealed class MetricsHub
{
    private const int MaxViolationSamples = 200;

    public GroupCounters[] Groups { get; }
    public GroupCounters Total { get; } = new();

    public LatencyMetric E2E { get; } = new("e2e", "scheduled -> acked (includes producer lag; coordinated-omission safe)");
    public LatencyMetric E2ESent { get; } = new("e2e_from_send", "handed to broker -> acked");
    public LatencyMetric ProducerLag { get; } = new("producer_lag", "scheduled -> handed to broker");
    public LatencyMetric Publish { get; } = new("publish_call", "duration of one publish (batch) call");
    public LatencyMetric QueueWait { get; } = new("queue_wait", "handed to broker -> received by a worker");
    public LatencyMetric BrokerDwell { get; } = new("broker_dwell", "broker enqueue timestamp -> received (broker clock)");
    public LatencyMetric Receive { get; } = new("receive_call", "duration of a non-empty receive call");
    public LatencyMetric Load { get; } = new("ctx_load", "load execution context from store");
    public LatencyMetric Process { get; } = new("process", "worker compute (incl. payload verification + simulated work)");
    public LatencyMetric Commit { get; } = new("ctx_commit", "persist result + advance cursor (+ tx ack)");
    public LatencyMetric Ack { get; } = new("ack", "broker ack call (AfterCommit mode)");
    public LatencyMetric Service { get; } = new("service", "received -> acked");
    public LatencyMetric E2ELarge { get; } = new("e2e_large", "e2e of large-payload events only");

    public IReadOnlyList<LatencyMetric> All { get; }
    public LatencyMetric[] GroupE2E { get; }

    public ConcurrentDictionary<string, long> ViolationCounts { get; } = new();
    public ConcurrentQueue<string> ViolationSamples { get; } = new();
    private int _violationSamples;

    /// <summary>Events scheduled before this instant are not recorded in latency histograms.</summary>
    public long MeasureFromUs { get; set; }

    public MetricsHub(Topology topology)
    {
        Groups = topology.Groups.Select(_ => new GroupCounters()).ToArray();
        GroupE2E = topology.Groups.Select(g => new LatencyMetric($"e2e[{g.Name}]", $"e2e for group {g.Name}")).ToArray();
        All = [E2E, E2ESent, ProducerLag, Publish, QueueWait, BrokerDwell, Receive, Load, Process, Commit, Ack, Service, E2ELarge];
    }

    public bool ShouldMeasure(EventEnvelope ev) => !ev.IsClosing && ev.ScheduledAtUs >= MeasureFromUs;

    public void OnPublished(int group, IReadOnlyList<EventEnvelope> batch, long callUs, long approxBytes)
    {
        var g = Groups[group];
        long closing = 0, large = 0;
        foreach (var ev in batch)
        {
            if (ev.IsClosing) closing++;
            if (ev.IsLarge) large++;
            if (ShouldMeasure(ev)) ProducerLag.Record(ev.SentAtUs - ev.ScheduledAtUs);
        }
        Add(ref g.Produced, ref Total.Produced, batch.Count);
        if (closing > 0) Add(ref g.ClosingProduced, ref Total.ClosingProduced, closing);
        if (large > 0) Add(ref g.LargeEvents, ref Total.LargeEvents, large);
        Interlocked.Add(ref g.BytesPublished, approxBytes);
        Interlocked.Add(ref Total.BytesPublished, approxBytes);
        Publish.Record(callUs);
    }

    /// <summary>Counts an event whose state change was committed (exactly once per event, guarded by the cursor).</summary>
    public void OnApplied(EventEnvelope ev)
    {
        Interlocked.Increment(ref Groups[ev.Group].Applied);
        Interlocked.Increment(ref Total.Applied);
    }

    public void RecordLatencies(EventEnvelope ev, long receivedUs, long enqueuedUs, long ackedUs,
        long loadUs, long processUs, long commitUs, long ackUs)
    {
        if (!ShouldMeasure(ev)) return;

        E2E.Record(ackedUs - ev.ScheduledAtUs);
        GroupE2E[ev.Group].Record(ackedUs - ev.ScheduledAtUs);
        E2ESent.Record(ackedUs - ev.SentAtUs);
        QueueWait.Record(receivedUs - ev.SentAtUs);
        if (enqueuedUs > 0) BrokerDwell.Record(receivedUs - enqueuedUs);
        Load.Record(loadUs);
        Process.Record(processUs);
        Commit.Record(commitUs);
        if (ackUs > 0) Ack.Record(ackUs);
        Service.Record(ackedUs - receivedUs);
        if (ev.IsLarge) E2ELarge.Record(ackedUs - ev.ScheduledAtUs);
    }

    public void OnDuplicate(int group) => Inc(group, static c => ref c.Duplicates);
    public void OnNack(int group) => Inc(group, static c => ref c.Nacks);
    public void OnError(int group) => Inc(group, static c => ref c.Errors);
    public void OnPublishError(int group) => Inc(group, static c => ref c.PublishErrors);

    public void OnGap(EventEnvelope ev, long lastSeq)
    {
        Inc(ev.Group, static c => ref c.Gaps);
        Violation("ordering_gap", $"source {ev.SourceId}: received seq {ev.Seq} while store cursor is {lastSeq}");
    }

    public void OnStateMismatch(EventEnvelope ev, string kind, string detail)
    {
        Inc(ev.Group, static c => ref c.StateMismatches);
        Violation(kind, $"source {ev.SourceId} seq {ev.Seq} {ev.Op} record {ev.RecordId}: {detail}");
    }

    public ConcurrentQueue<string> ErrorSamples { get; } = new();
    private int _errorSamples;

    /// <summary>Transient/operational error (not a correctness violation by itself).</summary>
    public void ErrorSample(string detail)
    {
        if (Interlocked.Increment(ref _errorSamples) <= MaxViolationSamples) ErrorSamples.Enqueue(detail);
    }

    public void Violation(string kind, string detail)
    {
        ViolationCounts.AddOrUpdate(kind, 1, static (_, v) => v + 1);
        if (Interlocked.Increment(ref _violationSamples) <= MaxViolationSamples)
            ViolationSamples.Enqueue($"[{kind}] {detail}");
    }

    private delegate ref long FieldSelector(GroupCounters c);

    private void Inc(int group, FieldSelector field)
    {
        Interlocked.Increment(ref field(Groups[group]));
        Interlocked.Increment(ref field(Total));
    }

    private static void Add(ref long a, ref long b, long n)
    {
        Interlocked.Add(ref a, n);
        Interlocked.Add(ref b, n);
    }
}
