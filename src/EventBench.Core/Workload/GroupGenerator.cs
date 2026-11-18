using EventBench.Core.Config;
using EventBench.Core.Util;

namespace EventBench.Core.Workload;

/// <summary>A group definition before worker sizing / per-source jitter is applied.</summary>
public sealed record GroupSpec(
    string Name,
    int Sources,
    double BaseRatePerSource,
    double SourceRateJitter,
    int? PayloadBytes,
    double LargeEventFraction,
    int? LargeMinBytes,
    int? LargeMaxBytes,
    int? Workers);

/// <summary>Deterministic generator for many heterogeneous groups (see <see cref="GeneratedGroupsOptions"/>).</summary>
public static class GroupGenerator
{
    public static List<GroupSpec> Generate(GeneratedGroupsOptions o, int seed)
    {
        if (o.Groups <= 0) return [];
        if (o.MinSources < 1 || o.MaxSources < o.MinSources)
            throw new InvalidOperationException("GeneratedGroups: need 1 <= MinSources <= MaxSources");
        if (o.MinRatePerMinute <= 0 || o.MaxRatePerMinute < o.MinRatePerMinute)
            throw new InvalidOperationException("GeneratedGroups: need 0 < MinRatePerMinute <= MaxRatePerMinute");
        if (o.LargeGroups > o.Groups)
            throw new InvalidOperationException("GeneratedGroups: LargeGroups > Groups");

        var rng = new FastRandom(Hashing.SplitMix((ulong)seed ^ 0x6E6E_6E6E));
        int[] sizes = GenerateSizes(o, ref rng);

        // Pick the large-payload groups.
        var large = new HashSet<int>();
        while (large.Count < o.LargeGroups) large.Add(rng.NextInt(0, o.Groups));

        int width = Math.Max(3, (o.Groups - 1).ToString().Length);
        var result = new List<GroupSpec>(o.Groups);
        for (int i = 0; i < o.Groups; i++)
        {
            bool isLarge = large.Contains(i);
            double perMinute = isLarge
                ? o.LargeGroupMinRatePerMinute + rng.NextDouble() * (o.LargeGroupMaxRatePerMinute - o.LargeGroupMinRatePerMinute)
                : TruncatedLogNormal(ref rng, o.RateMedianPerMinute, o.RateSigma, o.MinRatePerMinute, o.MaxRatePerMinute);
            int payload = (int)Math.Round(LogUniform(ref rng, Math.Max(0, o.SmallPayloadMinBytes), Math.Max(o.SmallPayloadMinBytes, o.SmallPayloadMaxBytes)));
            result.Add(new GroupSpec(
                Name: $"{o.Name}-{i.ToString().PadLeft(width, '0')}{(isLarge ? "L" : "")}",
                Sources: sizes[i],
                BaseRatePerSource: perMinute / 60.0,
                SourceRateJitter: o.SourceRateJitter,
                PayloadBytes: payload,
                LargeEventFraction: isLarge ? o.LargeEventFraction : 0,
                LargeMinBytes: isLarge ? o.LargeMinBytes : null,
                LargeMaxBytes: isLarge ? o.LargeMaxBytes : null,
                Workers: null));
        }
        return result;
    }

    private static int[] GenerateSizes(GeneratedGroupsOptions o, ref FastRandom rng)
    {
        int n = o.Groups;
        var raw = new double[n];
        for (int i = 0; i < n; i++) raw[i] = Math.Exp(o.SizeSigma * Normal(ref rng));

        int Clamp(double v) => Math.Clamp((int)Math.Round(v), o.MinSources, o.MaxSources);

        if (o.TotalSources <= 0)
            return raw.Select(r => Clamp(r * o.MedianSources)).ToArray();

        if ((long)n * o.MinSources > o.TotalSources || (long)n * o.MaxSources < o.TotalSources)
            throw new InvalidOperationException(
                $"GeneratedGroups: {o.TotalSources} sources cannot be split into {n} groups of {o.MinSources}..{o.MaxSources}");

        // Find the scale whose clamped/rounded sizes come closest to the total (sum is monotone in scale).
        double lo = 1e-9, hi = 1e9;
        long Sum(double scale) => raw.Sum(r => (long)Clamp(r * scale));
        for (int iter = 0; iter < 200; iter++)
        {
            double mid = Math.Sqrt(lo * hi);
            if (Sum(mid) <= o.TotalSources) lo = mid; else hi = mid;
        }
        var sizes = raw.Select(r => Clamp(r * lo)).ToArray();

        // Fix the rounding residual one source at a time on random groups that still have room.
        long diff = o.TotalSources - sizes.Sum(x => (long)x);
        while (diff != 0)
        {
            int i = rng.NextInt(0, n);
            if (diff > 0 && sizes[i] < o.MaxSources) { sizes[i]++; diff--; }
            else if (diff < 0 && sizes[i] > o.MinSources) { sizes[i]--; diff++; }
        }
        return sizes;
    }

    /// <summary>Per-source rates for one group: base x U[1-j, 1+j], deterministic per (seed, group index).</summary>
    public static double[] JitterRates(double baseRate, double jitter, int count, int seed, int groupIndex)
    {
        var rng = new FastRandom(Hashing.SplitMix((ulong)seed * 0x9E37 + (ulong)groupIndex + 0xA11CE));
        jitter = Math.Clamp(jitter, 0, 0.95);
        var rates = new double[count];
        for (int i = 0; i < count; i++) rates[i] = baseRate * (1 + jitter * (2 * rng.NextDouble() - 1));
        return rates;
    }

    private static double Normal(ref FastRandom rng)
    {
        double u1 = 1.0 - rng.NextDouble(); // (0,1]
        double u2 = rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
    }

    private static double TruncatedLogNormal(ref FastRandom rng, double median, double sigma, double min, double max)
    {
        for (int i = 0; i < 1000; i++)
        {
            double v = median * Math.Exp(sigma * Normal(ref rng));
            if (v >= min && v <= max) return v;
        }
        return Math.Clamp(median, min, max);
    }

    private static double LogUniform(ref FastRandom rng, double min, double max)
        => min <= 0 || max <= min ? max : min * Math.Pow(max / min, rng.NextDouble());
}
