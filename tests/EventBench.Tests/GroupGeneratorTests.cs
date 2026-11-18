using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Workload;

namespace EventBench.Tests;

public class GroupGeneratorTests
{
    private static BenchConfig Config(Action<GeneratedGroupsOptions>? tweak = null)
    {
        var cfg = new BenchConfig();
        cfg.Workload.Seed = 12345;
        cfg.Workload.GeneratedGroups = new GeneratedGroupsOptions
        {
            Groups = 500, TotalSources = 16000, MinSources = 6, MaxSources = 200,
            MinRatePerMinute = 10, MaxRatePerMinute = 200, RateMedianPerMinute = 40, RateSigma = 0.7,
            SourceRateJitter = 0.25, LargeGroups = 4,
        };
        tweak?.Invoke(cfg.Workload.GeneratedGroups);
        cfg.Worker.EventsPerSecondPerWorker = 50;
        cfg.Worker.UploadBytesPerSecondPerWorker = 8 * 1024 * 1024;
        return cfg;
    }

    [Fact]
    public void SizesHitTheExactTotalWithinBounds()
    {
        var t = Topology.FromConfig(Config());
        Assert.Equal(500, t.Groups.Count);
        Assert.Equal(16000, t.TotalSources);
        Assert.All(t.Groups, g => Assert.InRange(g.SourceCount, 6, 200));
        // "a few (single digit) or hundreds": both ends must actually occur.
        Assert.Contains(t.Groups, g => g.SourceCount < 10);
        Assert.Contains(t.Groups, g => g.SourceCount >= 100);
    }

    [Fact]
    public void RatesAreBoundedAndSkewedTowardsLowMid()
    {
        var t = Topology.FromConfig(Config());
        var perMin = t.Groups.Where(g => g.LargeEventFraction == 0).Select(g => g.RatePerSource * 60).ToList();
        Assert.All(perMin, r => Assert.InRange(r, 10, 200));
        int lowMid = perMin.Count(r => r < 75), high = perMin.Count(r => r >= 125);
        Assert.True(lowMid > 5 * high, $"low/mid {lowMid} vs high {high}");
    }

    [Fact]
    public void SourceRatesAreJitteredAroundTheGroupRate()
    {
        var t = Topology.FromConfig(Config());
        foreach (var g in t.Groups.Take(50))
        {
            var rates = Enumerable.Range(g.FirstSourceId, g.SourceCount).Select(t.SourceRate).ToList();
            Assert.All(rates, r => Assert.InRange(r, g.RatePerSource * 0.75 - 1e-9, g.RatePerSource * 1.25 + 1e-9));
            Assert.Equal(g.RatePerGroup, rates.Sum(), 6);
            if (g.SourceCount > 5) Assert.True(rates.Distinct().Count() > 1);
        }
    }

    [Fact]
    public void LargeGroupsAreFewSlowAndGetMoreWorkers()
    {
        var t = Topology.FromConfig(Config());
        var large = t.Groups.Where(g => g.LargeEventFraction > 0).ToList();
        Assert.Equal(4, large.Count);
        Assert.All(large, g => Assert.InRange(g.RatePerSource * 60, 1, 5 * 1.0001));
        Assert.All(t.Groups.Where(g => g.LargeEventFraction == 0), g => Assert.InRange(g.PayloadBytes!.Value, 128, 4096));
        // Byte-based sizing kicks in once a large group's upload bandwidth exceeds one worker's budget.
        var t2 = Topology.FromConfig(Config(o => { o.LargeGroupMinRatePerMinute = 60; o.LargeGroupMaxRatePerMinute = 60; }));
        Assert.True(t2.Groups.Where(g => g.LargeEventFraction > 0).Max(g => g.Workers) > 1);
    }

    [Fact]
    public void GenerationIsDeterministic()
    {
        string Shape() => string.Join(";", Topology.FromConfig(Config()).Groups
            .Select(g => $"{g.Name}:{g.SourceCount}:{g.RatePerGroup:F6}:{g.PayloadBytes}:{g.Workers}"));
        Assert.Equal(Shape(), Shape());
    }

    [Fact]
    public void ImpossibleTotalsAreRejected()
    {
        Assert.Throws<InvalidOperationException>(() => Topology.FromConfig(Config(o => o.TotalSources = 500 * 201)));
        Assert.Throws<InvalidOperationException>(() => Topology.FromConfig(Config(o => o.TotalSources = 500 * 5)));
    }
}

public class ContainerStatsTests
{
    [Theory]
    [InlineData("2h7m40.6555s", 7660.6555)]
    [InlineData("11m6.949882s", 666.949882)]
    [InlineData("2.213ms", 0.002213)]
    [InlineData("850µs", 0.00085)]
    public void ParsesGoDurations(string text, double seconds)
    {
        Assert.True(EventBench.Core.Metrics.ContainerStatsSampler.TryParseGoDuration(text, out var v));
        Assert.Equal(seconds, v, 6);
    }
}
