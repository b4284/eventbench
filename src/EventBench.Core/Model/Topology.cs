using EventBench.Core.Config;

namespace EventBench.Core.Model;

/// <param name="Id">0-based group index (the "source type" / worker type).</param>
/// <param name="FirstSourceId">Sources of the group have contiguous global ids [FirstSourceId, FirstSourceId + SourceCount).</param>
public sealed record GroupInfo(int Id, string Name, int SourceCount, double RatePerSource, int Workers, int FirstSourceId)
{
    public double RatePerGroup => RatePerSource * SourceCount;
    public bool ContainsSource(int sourceId) => sourceId >= FirstSourceId && sourceId < FirstSourceId + SourceCount;
}

/// <summary>Resolved, immutable view of groups and sources.</summary>
public sealed class Topology
{
    private readonly int[] _groupOfSource;

    public IReadOnlyList<GroupInfo> Groups { get; }
    public int TotalSources { get; }
    public double TotalRate => Groups.Sum(g => g.RatePerGroup);

    public Topology(IReadOnlyList<GroupInfo> groups)
    {
        Groups = groups;
        TotalSources = groups.Sum(g => g.SourceCount);
        _groupOfSource = new int[TotalSources];
        foreach (var g in groups)
            for (int i = 0; i < g.SourceCount; i++) _groupOfSource[g.FirstSourceId + i] = g.Id;
    }

    public GroupInfo GroupOf(int sourceId) => Groups[_groupOfSource[sourceId]];
    public int IndexInGroup(int sourceId) => sourceId - GroupOf(sourceId).FirstSourceId;

    public static Topology FromConfig(BenchConfig cfg)
    {
        var groupDefs = cfg.Workload.Groups;
        if (groupDefs.Count == 0)
            throw new InvalidOperationException("Workload:Groups must define at least one group.");

        var groups = new List<GroupInfo>();
        int nextSource = 0;
        foreach (var def in groupDefs)
        {
            if (def.Count < 1) throw new InvalidOperationException($"Group '{def.Name}': Count must be >= 1");
            if (def.Sources < 1) throw new InvalidOperationException($"Group '{def.Name}': Sources must be >= 1");
            double perSource = def.RatePerSource
                ?? (def.RatePerGroup.HasValue ? def.RatePerGroup.Value / def.Sources
                    : throw new InvalidOperationException($"Group '{def.Name}': set RatePerSource or RatePerGroup"));
            if (perSource <= 0) throw new InvalidOperationException($"Group '{def.Name}': rate must be > 0");
            int workers = def.Workers ?? cfg.Worker.WorkersPerGroup;
            if (workers < 1) throw new InvalidOperationException($"Group '{def.Name}': Workers must be >= 1");

            for (int i = 0; i < def.Count; i++)
            {
                string name = def.Count == 1 ? def.Name : $"{def.Name}-{i}";
                groups.Add(new GroupInfo(groups.Count, name, def.Sources, perSource, workers, nextSource));
                nextSource += def.Sources;
            }
        }

        if (groups.Select(g => g.Name).Distinct(StringComparer.Ordinal).Count() != groups.Count)
            throw new InvalidOperationException("Group names must be unique.");
        return new Topology(groups);
    }
}
