using EventBench.Core.Config;
using EventBench.Core.Workload;

namespace EventBench.Core.Model;

/// <param name="Id">0-based group index (the "source type" / worker type).</param>
/// <param name="RatePerSource">Base (mean) events/sec per source; individual sources may be jittered around it.</param>
/// <param name="FirstSourceId">Sources of the group have contiguous global ids [FirstSourceId, FirstSourceId + SourceCount).</param>
public sealed record GroupInfo(int Id, string Name, int SourceCount, double RatePerSource, int Workers, int FirstSourceId)
{
    /// <summary>Sum of the group's per-source rates (events/sec).</summary>
    public double RatePerGroup { get; init; } = RatePerSource * SourceCount;

    /// <summary>Regular payload size for this group; null = Workload:PayloadBytes.</summary>
    public int? PayloadBytes { get; init; }

    /// <summary>Fraction of Create/Update events carrying a large upload (group profile).</summary>
    public double LargeEventFraction { get; init; }
    public int? LargeMinBytes { get; init; }
    public int? LargeMaxBytes { get; init; }

    public bool ContainsSource(int sourceId) => sourceId >= FirstSourceId && sourceId < FirstSourceId + SourceCount;
}

/// <summary>Resolved, immutable view of groups and sources.</summary>
public sealed class Topology
{
    private readonly int[] _groupOfSource;
    private readonly double[] _sourceRates;

    public IReadOnlyList<GroupInfo> Groups { get; }
    public int TotalSources { get; }
    public double TotalRate => Groups.Sum(g => g.RatePerGroup);

    public Topology(IReadOnlyList<GroupInfo> groups, double[]? sourceRates = null)
    {
        Groups = groups;
        TotalSources = groups.Sum(g => g.SourceCount);
        _groupOfSource = new int[TotalSources];
        _sourceRates = sourceRates ?? new double[TotalSources];
        if (_sourceRates.Length != TotalSources) throw new ArgumentException("sourceRates length must equal the number of sources");
        foreach (var g in groups)
            for (int i = 0; i < g.SourceCount; i++)
            {
                _groupOfSource[g.FirstSourceId + i] = g.Id;
                if (sourceRates is null) _sourceRates[g.FirstSourceId + i] = g.RatePerSource;
            }
    }

    public GroupInfo GroupOf(int sourceId) => Groups[_groupOfSource[sourceId]];
    public int IndexInGroup(int sourceId) => sourceId - GroupOf(sourceId).FirstSourceId;

    /// <summary>Mean events/sec of one source (after jitter).</summary>
    public double SourceRate(int sourceId) => _sourceRates[sourceId];

    public static Topology FromConfig(BenchConfig cfg)
    {
        var specs = new List<GroupSpec>();
        foreach (var def in cfg.Workload.Groups)
        {
            if (def.Count < 1) throw new InvalidOperationException($"Group '{def.Name}': Count must be >= 1");
            if (def.Sources < 1) throw new InvalidOperationException($"Group '{def.Name}': Sources must be >= 1");
            double perSource = def.RatePerSource
                ?? (def.RatePerGroup.HasValue ? def.RatePerGroup.Value / def.Sources
                    : throw new InvalidOperationException($"Group '{def.Name}': set RatePerSource or RatePerGroup"));
            for (int i = 0; i < def.Count; i++)
                specs.Add(new GroupSpec(def.Count == 1 ? def.Name : $"{def.Name}-{i}", def.Sources, perSource,
                    def.SourceRateJitter, def.PayloadBytes, def.LargeEventFraction, def.LargeMinBytes, def.LargeMaxBytes, def.Workers));
        }
        specs.AddRange(GroupGenerator.Generate(cfg.Workload.GeneratedGroups, cfg.Workload.Seed));

        if (specs.Count == 0)
            throw new InvalidOperationException("Define Workload:Groups and/or Workload:GeneratedGroups.");

        var groups = new List<GroupInfo>(specs.Count);
        var rates = new double[specs.Sum(s => s.Sources)];
        int nextSource = 0;
        foreach (var spec in specs)
        {
            if (spec.BaseRatePerSource <= 0) throw new InvalidOperationException($"Group '{spec.Name}': rate must be > 0");
            int id = groups.Count;
            var sourceRates = GroupGenerator.JitterRates(spec.BaseRatePerSource, spec.SourceRateJitter, spec.Sources, cfg.Workload.Seed, id);
            Array.Copy(sourceRates, 0, rates, nextSource, spec.Sources);
            double groupRate = sourceRates.Sum();
            int workers = spec.Workers ?? SizeWorkers(cfg, spec, groupRate);
            if (workers < 1) throw new InvalidOperationException($"Group '{spec.Name}': Workers must be >= 1");

            groups.Add(new GroupInfo(id, spec.Name, spec.Sources, spec.BaseRatePerSource, workers, nextSource)
            {
                RatePerGroup = groupRate,
                PayloadBytes = spec.PayloadBytes,
                LargeEventFraction = spec.LargeEventFraction,
                LargeMinBytes = spec.LargeMinBytes,
                LargeMaxBytes = spec.LargeMaxBytes,
            });
            nextSource += spec.Sources;
        }

        if (groups.Select(g => g.Name).Distinct(StringComparer.Ordinal).Count() != groups.Count)
            throw new InvalidOperationException("Group names must be unique.");
        return new Topology(groups, rates);
    }

    private static int SizeWorkers(BenchConfig cfg, GroupSpec spec, double groupRate)
    {
        var w = cfg.Worker;
        if (w.EventsPerSecondPerWorker <= 0 && w.UploadBytesPerSecondPerWorker <= 0) return w.WorkersPerGroup;
        int byEvents = w.EventsPerSecondPerWorker > 0 ? (int)Math.Ceiling(groupRate / w.EventsPerSecondPerWorker) : 1;
        int byBytes = w.UploadBytesPerSecondPerWorker > 0
            ? (int)Math.Ceiling(ExpectedUploadBytesPerSec(cfg.Workload, spec, groupRate) / w.UploadBytesPerSecondPerWorker)
            : 1;
        return Math.Clamp(Math.Max(byEvents, byBytes), Math.Max(1, w.MinWorkersPerGroup), Math.Max(1, w.MaxWorkersPerGroup));
    }

    /// <summary>Uploads ride on Create/Update events only, so scale by their share of the op mix.</summary>
    public static double ExpectedUploadBytesPerSec(WorkloadOptions wl, GroupSpec spec, double groupRate)
    {
        if (spec.LargeEventFraction <= 0) return 0;
        var mix = wl.OpMix;
        double cuShare = (mix.CreateWeight + mix.UpdateWeight) / Math.Max(1e-9, mix.CreateWeight + mix.GetWeight + mix.UpdateWeight + mix.CompleteWeight);
        double avg = ((spec.LargeMinBytes ?? wl.LargePayload.MinBytes) + (double)(spec.LargeMaxBytes ?? wl.LargePayload.MaxBytes)) / 2;
        return groupRate * spec.LargeEventFraction * cuShare * avg;
    }
}
