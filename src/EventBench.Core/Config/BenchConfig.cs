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

public sealed class WorkerOptions
{
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
}
