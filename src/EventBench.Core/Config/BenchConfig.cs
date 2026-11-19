namespace EventBench.Core.Config;

/// <summary>Root configuration. Bound from JSON files + command line overrides (e.g. --Workload:DurationSeconds=30).</summary>
public sealed class BenchConfig
{
    public string Name { get; set; } = "bench";
    public string ResultsDirectory { get; set; } = "results";
    public WorkloadOptions Workload { get; set; } = new();
    public WorkerOptions Worker { get; set; } = new();
    public MetricsOptions Metrics { get; set; } = new();

    /// <summary>Broker section. "Type" selects the implementation; the remaining keys are implementation specific.</summary>
    public ComponentOptions Broker { get; set; } = new() { Type = "inmemory" };

    /// <summary>State store section. "Type" selects the implementation; the remaining keys are implementation specific.</summary>
    public ComponentOptions Store { get; set; } = new() { Type = "inmemory" };
}

public sealed class ComponentOptions
{
    public string Type { get; set; } = "";
}

public enum ArrivalProcess { Poisson, Constant }

public enum LargePayloadMode
{
    /// <summary>The large payload travels inside the broker message (base64 in JSON).</summary>
    Inline,
    /// <summary>The producer stores the payload in the state store and the message only carries a reference.</summary>
    ClaimCheck,
}

public sealed class WorkloadOptions
{
    public int Seed { get; set; } = 12345;

    /// <summary>Length of the steady-state event generation phase.</summary>
    public double DurationSeconds { get; set; } = 60;

    /// <summary>Events scheduled before this point are excluded from latency histograms.</summary>
    public double WarmupSeconds { get; set; } = 5;

    /// <summary>After generation stops, how long to wait for workers to drain all queues.</summary>
    public double DrainTimeoutSeconds { get; set; } = 120;

    public ArrivalProcess Arrival { get; set; } = ArrivalProcess.Poisson;

    /// <summary>Producer tasks per group. Each owns a disjoint subset of the group's sources (keeps per-source order).</summary>
    public int ProducersPerGroup { get; set; } = 1;

    /// <summary>
    /// When &gt; 0: a fixed pool of producers shared by all groups (sources are spread round-robin), each
    /// publishing to many queues per round trip. Overrides ProducersPerGroup. Use with many small groups.
    /// </summary>
    public int Producers { get; set; } = 0;

    /// <summary>Upper bound of events per publish call.</summary>
    public int MaxPublishBatch { get; set; } = 500;

    /// <summary>Max sleep between producer scheduling passes when nothing is due.</summary>
    public double ProducerTickMs { get; set; } = 5;

    public List<GroupOptions> Groups { get; set; } = new();

    /// <summary>Randomly generated groups (appended after the explicit Groups). Disabled when Groups = 0.</summary>
    public GeneratedGroupsOptions GeneratedGroups { get; set; } = new();

    public OpMixOptions OpMix { get; set; } = new();

    /// <summary>Size of the random payload attached to regular events.</summary>
    public int PayloadBytes { get; set; } = 256;

    /// <summary>Size of the execution-context blob stored per record (read + rewritten by workers).</summary>
    public int ContextBytes { get; set; } = 1024;

    public LargePayloadOptions LargePayload { get; set; } = new();

    /// <summary>Events whose processing waits on a slow external call (no CPU), to test cross-source blocking.</summary>
    public SlowEventOptions SlowEvents { get; set; } = new();

    /// <summary>
    /// Scales every small-message group's per-source rate (load ramps on the same group shape). Groups with
    /// large messages keep their configured rate. Workers sized by EventsPerSecondPerWorker scale along.
    /// </summary>
    public double RateMultiplier { get; set; } = 1.0;
}

public sealed class SlowEventOptions
{
    /// <summary>Fraction of (non-closing) events, in every group, that take MinMs..MaxMs to process. 0 disables.</summary>
    public double Fraction { get; set; } = 0;
    public int MinMs { get; set; } = 1000;
    public int MaxMs { get; set; } = 3000;
    /// <summary>Large-payload events are always slow (MinMs..MaxMs) when true.</summary>
    public bool IncludeLarge { get; set; } = false;
}

public sealed class GroupOptions
{
    /// <summary>Name prefix. When Count &gt; 1 the groups are named Name-0, Name-1 ...</summary>
    public string Name { get; set; } = "g";

    /// <summary>Replicates this group definition N times.</summary>
    public int Count { get; set; } = 1;

    /// <summary>Number of event sources in the group.</summary>
    public int Sources { get; set; } = 800;

    /// <summary>Events/sec per individual source. Takes precedence over RatePerGroup.</summary>
    public double? RatePerSource { get; set; }

    /// <summary>Aggregate events/sec for the whole group (spread evenly across its sources).</summary>
    public double? RatePerGroup { get; set; }

    /// <summary>Worker count for this group (overrides Worker:WorkersPerGroup / load-based sizing).</summary>
    public int? Workers { get; set; }

    /// <summary>Each source's rate = group rate x U[1-j, 1+j].</summary>
    public double SourceRateJitter { get; set; } = 0;

    /// <summary>Regular payload size for this group (default Workload:PayloadBytes).</summary>
    public int? PayloadBytes { get; set; }

    /// <summary>Fraction of this group's Create/Update events that carry a large upload.</summary>
    public double LargeEventFraction { get; set; } = 0;
    public int? LargeMinBytes { get; set; }
    public int? LargeMaxBytes { get; set; }
}

/// <summary>
/// Generates many heterogeneous groups. Sizes follow a log-normal clamped to [MinSources, MaxSources]
/// and rescaled so they sum exactly to TotalSources. Per-source base rates per group follow a log-normal
/// (median RateMedianPerMinute) truncated to [MinRatePerMinute, MaxRatePerMinute], so mid/low rates are
/// common and high rates rare. A few "large" groups send MB-sized uploads at a low rate.
/// </summary>
public sealed class GeneratedGroupsOptions
{
    public int Groups { get; set; } = 0;
    public string Name { get; set; } = "t";

    /// <summary>Exact total number of sources across generated groups (0 = unconstrained, use MedianSources).</summary>
    public int TotalSources { get; set; } = 16000;
    public int MedianSources { get; set; } = 20;
    public int MinSources { get; set; } = 6;
    public int MaxSources { get; set; } = 200;
    /// <summary>Log-normal sigma of group sizes; larger = more very small and very large groups.</summary>
    public double SizeSigma { get; set; } = 1.0;

    /// <summary>Per-source base rate of a group, events per minute.</summary>
    public double MinRatePerMinute { get; set; } = 10;
    public double MaxRatePerMinute { get; set; } = 200;
    public double RateMedianPerMinute { get; set; } = 40;
    public double RateSigma { get; set; } = 0.7;
    /// <summary>Each source's rate = group base rate x U[1-j, 1+j].</summary>
    public double SourceRateJitter { get; set; } = 0.25;

    /// <summary>Regular payload size is chosen per group, log-uniform in this range.</summary>
    public int SmallPayloadMinBytes { get; set; } = 128;
    public int SmallPayloadMaxBytes { get; set; } = 4096;

    /// <summary>Number of groups (picked at random) that carry large uploads.</summary>
    public int LargeGroups { get; set; } = 0;
    /// <summary>Per-source rate of large groups: low, since clients need time to produce MB-sized uploads.</summary>
    public double LargeGroupMinRatePerMinute { get; set; } = 1;
    public double LargeGroupMaxRatePerMinute { get; set; } = 5;
    /// <summary>Fraction of a large group's Create/Update events that carry an upload (Get/Complete stay small).</summary>
    public double LargeEventFraction { get; set; } = 1.0;
    public int LargeMinBytes { get; set; } = 1 * 1024 * 1024;
    public int LargeMaxBytes { get; set; } = 4 * 1024 * 1024;
}

public sealed class OpMixOptions
{
    public double CreateWeight { get; set; } = 1;
    public double GetWeight { get; set; } = 2;
    public double UpdateWeight { get; set; } = 5;
    public double CompleteWeight { get; set; } = 1;

    /// <summary>Max concurrently open (created, not yet completed) records per source.</summary>
    public int MaxOpenRecordsPerSource { get; set; } = 4;
}

public sealed class LargePayloadOptions
{
    /// <summary>Probability that any given event carries a large payload.</summary>
    public double Probability { get; set; } = 0.0001;

    /// <summary>Hard cap on the number of large events over the run (0 disables).</summary>
    public int MaxCount { get; set; } = 12;

    public int MinBytes { get; set; } = 2 * 1024 * 1024;
    public int MaxBytes { get; set; } = 16 * 1024 * 1024;
    public LargePayloadMode Mode { get; set; } = LargePayloadMode.Inline;
}

public enum AckMode
{
    /// <summary>Commit state, then ack via the broker in a separate call (at-least-once; dedup via the source cursor).</summary>
    AfterCommit,

    /// <summary>Ack inside the state store transaction (exactly-once). Only possible when broker and store share a database.</summary>
    InTransaction,
}

public enum WorkerMode
{
    /// <summary>Receive a batch, process it (per-source lanes, MaxParallelPerWorker at a time), then receive the next.</summary>
    Batch,
    /// <summary>
    /// MaxParallelPerWorker slots that refill independently: receive whenever a slot is free, park events of
    /// a source that is already in flight, retry failures locally. A slow event holds only its own slot and
    /// its own source's later events.
    /// </summary>
    Pipelined,
}

public sealed class WorkerOptions
{
    public WorkerMode Mode { get; set; } = WorkerMode.Batch;
    /// <summary>Pipelined mode: max events a worker holds parked behind in-flight events of the same source.</summary>
    public int ReadAheadLimit { get; set; } = 256;
    /// <summary>
    /// Pipelined mode: local retries of one event before the worker gives the source's events back to the
    /// broker (nack, oldest first). Guards against a broker that delivered the source out of order.
    /// </summary>
    public int MaxLocalRetries { get; set; } = 5;
    /// <summary>
    /// Pipelined mode with a shared-queue broker (ISourceLinger): after finishing a source's event, fetch
    /// that source's next event straight away and keep the slot, without a poll. Skipped while other
    /// sources wait for a slot.
    /// </summary>
    public bool Linger { get; set; } = false;
    /// <summary>Consecutive lingers on one source before the slot is released (fairness).</summary>
    public int LingerMax { get; set; } = 16;

    public int WorkersPerGroup { get; set; } = 8;

    /// <summary>
    /// When &gt; 0, size each group's worker pool by load: ceil(groupEventsPerSec / this), clamped to
    /// [MinWorkersPerGroup, MaxWorkersPerGroup]. An explicit GroupOptions.Workers still wins.
    /// </summary>
    public double EventsPerSecondPerWorker { get; set; } = 0;

    /// <summary>
    /// When &gt; 0, also size by expected large-upload bandwidth: ceil(groupUploadBytesPerSec / this).
    /// The larger of the event-based and byte-based counts wins (still clamped to Min/MaxWorkersPerGroup).
    /// </summary>
    public double UploadBytesPerSecondPerWorker { get; set; } = 0;
    public int MinWorkersPerGroup { get; set; } = 1;
    public int MaxWorkersPerGroup { get; set; } = 16;

    /// <summary>Max messages fetched per receive call.</summary>
    public int BatchSize { get; set; } = 10;

    /// <summary>How many different sources from one batch a worker may process concurrently (same-source events are always sequential).</summary>
    public int MaxParallelPerWorker { get; set; } = 1;

    public AckMode AckMode { get; set; } = AckMode.AfterCommit;

    /// <summary>Busy CPU work per event, in microseconds (simulates business logic).</summary>
    public double SimulatedWorkMicros { get; set; } = 0;

    /// <summary>Delay before a failed message becomes visible again.</summary>
    public double RetryDelayMs { get; set; } = 250;
    /// <summary>Probability that a worker fails an event just before commit and nacks it (tests ordered redelivery).</summary>
    public double ChaosFailProbability { get; set; }

    public double IdleBackoffMinMs { get; set; } = 1;
    public double IdleBackoffMaxMs { get; set; } = 50;
}

public sealed class MetricsOptions
{
    public double SampleIntervalSeconds { get; set; } = 1;
    public bool ConsoleProgress { get; set; } = true;

    /// <summary>Sample per-container CPU/memory via an external command (e.g. podman stats).</summary>
    public bool ContainerStats { get; set; } = true;
    public string ContainerStatsCommand { get; set; } = "podman stats --no-stream --format json";
    public double ContainerStatsIntervalSeconds { get; set; } = 3;

    /// <summary>How often broker/store stats providers are polled (they may run COUNT(*) queries).</summary>
    public double ExternalStatsIntervalSeconds { get; set; } = 2;

    /// <summary>Write HdrHistogram percentile distributions per metric.</summary>
    public bool WriteHistograms { get; set; } = true;

    /// <summary>
    /// Cross-source blocking: an event that is ready (sent, and its source's previous event done) but not
    /// started while some worker slot of its group sits idle for longer than this is a violation.
    /// Covers delivery latency and the idle-poll backoff (IdleBackoffMaxMs).
    /// </summary>
    public double BlockingThresholdMs { get; set; } = 100;
    /// <summary>Make cross-source blocking violations fail the oracle.</summary>
    public bool FailOnBlocking { get; set; } = true;
}
