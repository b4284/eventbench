using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Runner;
using EventBench.Core.Workload;
using Microsoft.Extensions.Configuration;

namespace EventBench.Tests;

/// <summary>
/// End-to-end runs against the in-memory broker/store. The chaos knobs inject the faults a broken
/// broker would produce; the oracle must flag them (and must not flag benign redeliveries).
/// </summary>
public class OracleTests
{
    private static async Task<RunResult> RunAsync(Dictionary<string, string?> overrides)
    {
        var settings = new Dictionary<string, string?>
        {
            ["Name"] = "test",
            ["Workload:DurationSeconds"] = "2",
            ["Workload:WarmupSeconds"] = "0",
            ["Workload:DrainTimeoutSeconds"] = "4",
            ["Workload:Groups:0:Name"] = "a",
            ["Workload:Groups:0:Count"] = "2",
            ["Workload:Groups:0:Sources"] = "50",
            ["Workload:Groups:0:RatePerSource"] = "20",
            ["Workload:PayloadBytes"] = "64",
            ["Workload:ContextBytes"] = "64",
            ["Workload:LargePayload:Probability"] = "0.01",
            ["Workload:LargePayload:MaxCount"] = "2",
            ["Workload:LargePayload:MinBytes"] = "100000",
            ["Workload:LargePayload:MaxBytes"] = "200000",
            ["Worker:WorkersPerGroup"] = "3",
            ["Worker:RetryDelayMs"] = "20",
            ["Metrics:ConsoleProgress"] = "false",
            ["Metrics:ContainerStats"] = "false",
            ["Broker:Type"] = "inmemory",
            ["Store:Type"] = "inmemory",
        };
        foreach (var kv in overrides) settings[kv.Key] = kv.Value;
        var raw = new ConfigurationBuilder().AddInMemoryCollection(settings).Build();
        var cfg = raw.Get<BenchConfig>()!;
        return await new BenchmarkRunner(ComponentRegistry.WithBuiltIns(), _ => { }).RunAsync(cfg, raw, CancellationToken.None);
    }

    [Fact]
    public async Task CleanRunPasses()
    {
        var r = await RunAsync(new());
        Assert.True(r.Passed, string.Join("; ", r.Oracle!.Checks.Where(c => !c.Passed).Select(c => $"{c.Name}: {c.Detail}")));
        Assert.True(r.Metrics.Total.Applied > 1000);
        Assert.True(r.Oracle.RecordsVerified > 0);
    }

    [Fact]
    public async Task ReorderingIsDetected()
    {
        var r = await RunAsync(new() { ["Broker:ChaosReorderProbability"] = "0.05" });
        Assert.False(r.Passed);
        Assert.Contains(r.Oracle!.Checks, c => c.Name == "runtime_ordering_gap" && !c.Passed);
    }

    [Fact]
    public async Task DuplicatesAreToleratedIdempotently()
    {
        var r = await RunAsync(new() { ["Broker:ChaosDuplicateProbability"] = "0.1" });
        Assert.True(r.Passed, string.Join("; ", r.Oracle!.Checks.Where(c => !c.Passed).Select(c => $"{c.Name}: {c.Detail}")));
        Assert.True(r.Metrics.Total.Duplicates > 0);
    }

    [Fact]
    public async Task LostMessagesAreDetected()
    {
        var r = await RunAsync(new() { ["Broker:ChaosDropProbability"] = "0.005" });
        Assert.False(r.Passed);
        Assert.Contains(r.Oracle!.Checks, c => c.Name == "cursors" && !c.Passed);
        Assert.False(r.Drained);
    }

    private static Dictionary<string, string?> Slow(string mode) => new()
    {
        ["Worker:Mode"] = mode,
        ["Worker:MaxParallelPerWorker"] = "4",
        ["Workload:SlowEvents:Fraction"] = "0.02",
        ["Workload:SlowEvents:MinMs"] = "300",
        ["Workload:SlowEvents:MaxMs"] = "500",
    };

    [Fact]
    public async Task BatchModeCrossSourceBlockingIsDetected()
    {
        var r = await RunAsync(Slow("Batch"));
        Assert.Contains(r.Oracle!.Checks, c => c.Name == "runtime_cross_source_blocking" && !c.Passed);
        Assert.True(r.Metrics.Total.SlowEvents > 0);
    }

    [Fact]
    public async Task PipelinedModePassesWithSlowEventsAndFailures()
    {
        var o = Slow("Pipelined");
        o["Worker:ChaosFailProbability"] = "0.02";
        var r = await RunAsync(o);
        Assert.True(r.Passed, string.Join("; ", r.Oracle!.Checks.Where(c => !c.Passed).Select(c => $"{c.Name}: {c.Detail}")));
        Assert.True(r.Metrics.Total.Nacks > 0);
        Assert.Equal(0, r.Metrics.Total.BlockingViolations);
    }

    [Fact]
    public async Task PipelinedModeStillDetectsReordering()
    {
        var o = Slow("Pipelined");
        o["Broker:ChaosReorderProbability"] = "0.05";
        var r = await RunAsync(o);
        Assert.False(r.Passed);
        Assert.Contains(r.Oracle!.Checks, c => c.Name == "runtime_ordering_gap" && !c.Passed);
    }

    [Fact]
    public void ScriptsAreDeterministicPerSeed()
    {
        var opts = new WorkloadOptions { Seed = 7, LargePayload = new LargePayloadOptions { Probability = 0 } };
        var topo = new Topology([new GroupInfo(0, "g", 3, 1, 1, 0)]);
        string Trace()
        {
            var plan = new WorkloadPlan(opts, topo);
            var events = new List<EventEnvelope>();
            for (int i = 0; i < 200; i++) events.Add(plan.Scripts[i % 3].Next(i, plan));
            foreach (var s in plan.Scripts) s.Close(events, 0, plan);
            return string.Join("|", events.Select(e => $"{e.SourceId}:{e.Seq}:{e.Op}:{e.RecordId}:{e.Delta}:{e.PayloadChecksum}"));
        }
        Assert.Equal(Trace(), Trace());
    }

    [Fact]
    public void ClosingCompletesEveryRecord()
    {
        var opts = new WorkloadOptions { LargePayload = new LargePayloadOptions { Probability = 0 } };
        var topo = new Topology([new GroupInfo(0, "g", 1, 1, 1, 0)]);
        var plan = new WorkloadPlan(opts, topo);
        var s = plan.Scripts[0];
        for (int i = 0; i < 500; i++) s.Next(i, plan);
        s.Close(new List<EventEnvelope>(), 0, plan);
        Assert.Equal(0, s.OpenRecords);
        Assert.Equal(s.RecordsCreated, s.RecordsCompleted);
    }
}
