using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Util;

namespace EventBench.Core.Workload;

/// <summary>All source scripts plus shared generation state (large-payload budget).</summary>
public sealed class WorkloadPlan
{
    private int _largeIssued;

    public WorkloadOptions Options { get; }
    public Topology Topology { get; }
    /// <summary>Indexed by global source id.</summary>
    public SourceScript[] Scripts { get; }
    public int LargeIssued => Volatile.Read(ref _largeIssued);

    public WorkloadPlan(WorkloadOptions options, Topology topology)
    {
        Options = options;
        Topology = topology;
        Scripts = new SourceScript[topology.TotalSources];
        foreach (var g in topology.Groups)
            for (int i = 0; i < g.SourceCount; i++)
                Scripts[g.FirstSourceId + i] = new SourceScript(g.FirstSourceId + i, g.Id, options.Seed);
    }

    public bool TryTakeLarge(ref FastRandom rng)
    {
        var lp = Options.LargePayload;
        if (lp.MaxCount <= 0 || lp.Probability <= 0) return false;
        if (rng.NextDouble() >= lp.Probability) return false;
        while (true)
        {
            int cur = Volatile.Read(ref _largeIssued);
            if (cur >= lp.MaxCount) return false;
            if (Interlocked.CompareExchange(ref _largeIssued, cur + 1, cur) == cur) return true;
        }
    }

    public long ExpectedTotalEvents => Scripts.Sum(s => s.LastSeq);
}
