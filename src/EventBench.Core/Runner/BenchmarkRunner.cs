using System.Diagnostics;
using EventBench.Core.Abstractions;
using EventBench.Core.Config;
using EventBench.Core.Metrics;
using EventBench.Core.Model;
using EventBench.Core.Oracle;
using EventBench.Core.Processing;
using EventBench.Core.Util;
using EventBench.Core.Workload;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.Runner;

public sealed class RunResult
{
    public required BenchConfig Config { get; init; }
    public required IConfiguration RawConfig { get; init; }
    public required Topology Topology { get; init; }
    public required MetricsHub Metrics { get; init; }
    public required DateTime StartedUtc { get; init; }
    public string BrokerDescription { get; set; } = "";
    public string StoreDescription { get; set; } = "";
    public List<TimeSeriesRow> TimeSeries { get; set; } = new();
    public OracleReport? Oracle { get; set; }
    public double SetupSeconds { get; set; }
    public double RunSeconds { get; set; }
    public double ClosingSeconds { get; set; }
    public double DrainSeconds { get; set; }
    public bool Drained { get; set; }
    public long BrokerBacklog { get; set; }
    public long LargeEventsIssued { get; set; }
    /// <summary>Sampler clock (seconds) at which the run phase started; used to cut warmup from time series stats.</summary>
    public double RunStartT { get; set; }
    public string? Error { get; set; }
    public bool Passed => Error is null && Oracle is { Passed: true };
}

/// <summary>Orchestrates one benchmark run: setup -> run -> closing -> drain -> verify.</summary>
public sealed class BenchmarkRunner(ComponentRegistry registry, Action<string> log)
{
    private volatile string _phase = "init";

    public async Task<RunResult> RunAsync(BenchConfig cfg, IConfiguration raw, CancellationToken ct)
    {
        var topology = Topology.FromConfig(cfg);
        var metrics = new MetricsHub(topology, cfg.Worker.MaxParallelPerWorker, cfg.Metrics.BlockingThresholdMs, cfg.Metrics.FailOnBlocking);
        var result = new RunResult { Config = cfg, RawConfig = raw, Topology = topology, Metrics = metrics, StartedUtc = DateTime.UtcNow };
        var context = new BenchContext { Config = cfg, Topology = topology, Log = log };
        var sw = Stopwatch.StartNew();

        log($"topology: {topology.Groups.Count} groups, {topology.TotalSources:N0} sources, offered load {topology.TotalRate:N0} events/s, "
            + $"{topology.Groups.Sum(g => g.Workers):N0} workers");

        await using var broker = registry.CreateBroker(raw.GetSection("Broker"), context);
        await using var store = registry.CreateStore(raw.GetSection("Store"), context);
        result.BrokerDescription = broker.Describe();
        result.StoreDescription = store.Describe();
        log($"broker: {result.BrokerDescription}");
        log($"store:  {result.StoreDescription}");

        bool txAck = cfg.Worker.AckMode == AckMode.InTransaction;
        if (txAck && !store.SupportsTransactionalAck(broker))
            throw new InvalidOperationException($"AckMode=InTransaction is not supported by store '{store.Name}' with broker '{broker.Name}'.");

        _phase = "setup";
        await store.SetupAsync(ct);
        await broker.SetupAsync(ct);
        result.SetupSeconds = sw.Elapsed.TotalSeconds;
        log($"setup done in {result.SetupSeconds:F1}s");

        var plan = new WorkloadPlan(cfg.Workload, topology);

        var providers = new List<(string, IStatsProvider)>();
        if (broker.Stats is { } bs) providers.Add(("broker", bs));
        if (store.Stats is { } ss) providers.Add(("store", ss));
        var sampler = new MetricsSampler(metrics, cfg.Metrics, providers, () => _phase, log);
        using var samplerCts = new CancellationTokenSource();
        var samplerTask = sampler.RunAsync(samplerCts.Token);
        var samplerClock = Stopwatch.StartNew();

        using var workersCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var consumers = new List<IEventConsumer>();
        var workerTasks = new List<Task>();
        foreach (var g in topology.Groups)
        {
            for (int w = 0; w < g.Workers; w++)
            {
                var consumer = broker.CreateConsumer(g.Id, w);
                consumers.Add(consumer);
                var runner = new WorkerRunner(g, w, consumer, store, cfg, metrics, txAck, log);
                workerTasks.Add(Task.Run(() => runner.RunAsync(workersCts.Token)));
            }
        }

        var publishers = new List<IEventPublisher>();
        var producers = new List<ProducerRunner>();
        void AddProducer(string name, SourceScript[] owned)
        {
            if (owned.Length == 0) return;
            var publisher = broker.CreatePublisher(owned[0].GroupId);
            publishers.Add(publisher);
            producers.Add(new ProducerRunner(name, producers.Count, owned, publisher, store, plan, metrics, log));
        }
        if (cfg.Workload.Producers > 0)
        {
            // Shared pool: sources spread round-robin, so every producer serves many groups.
            int n = Math.Min(cfg.Workload.Producers, topology.TotalSources);
            for (int p = 0; p < n; p++)
                AddProducer($"p{p}", plan.Scripts.Where((_, sid) => sid % n == p).ToArray());
        }
        else
        {
            int shards = Math.Max(1, cfg.Workload.ProducersPerGroup);
            foreach (var g in topology.Groups)
                for (int shard = 0; shard < shards; shard++)
                    AddProducer($"{g.Name}/{shard}", Enumerable.Range(0, g.SourceCount).Where(i => i % shards == shard)
                        .Select(i => plan.Scripts[g.FirstSourceId + i]).ToArray());
        }

        try
        {
            // ---- run ----
            long startUs = Clock.NowUs() + 100_000;
            long endUs = startUs + (long)(cfg.Workload.DurationSeconds * 1_000_000);
            metrics.MeasureFromUs = startUs + (long)(cfg.Workload.WarmupSeconds * 1_000_000);
            result.RunStartT = samplerClock.Elapsed.TotalSeconds + 0.1;
            _phase = "run";
            log($"run: {cfg.Workload.DurationSeconds}s (warmup {cfg.Workload.WarmupSeconds}s), {producers.Count} producers, {workerTasks.Count} workers");
            var phaseSw = Stopwatch.StartNew();
            await Task.WhenAll(producers.Select(p => Task.Run(() => p.RunAsync(startUs, endUs, ct))));
            result.RunSeconds = phaseSw.Elapsed.TotalSeconds;

            // ---- closing: complete all open records ----
            _phase = "closing";
            phaseSw.Restart();
            await Task.WhenAll(producers.Select(p => Task.Run(() => p.CloseAsync(ct))));
            result.ClosingSeconds = phaseSw.Elapsed.TotalSeconds;
            log($"closing: {Interlocked.Read(ref metrics.Total.ClosingProduced):N0} completion events emitted");

            // ---- drain ----
            _phase = "drain";
            phaseSw.Restart();
            result.Drained = await DrainAsync(broker, metrics, TimeSpan.FromSeconds(cfg.Workload.DrainTimeoutSeconds), ct);
            result.DrainSeconds = phaseSw.Elapsed.TotalSeconds;
            log(result.Drained
                ? $"drained in {result.DrainSeconds:F1}s"
                : $"DRAIN TIMEOUT after {result.DrainSeconds:F1}s: {Interlocked.Read(ref metrics.Total.Produced) - Interlocked.Read(ref metrics.Total.Applied):N0} events not applied");
            if (!result.Drained)
                foreach (var line in Processing.WorkerRunner.DumpHeld(TimeSpan.FromSeconds(30)).Take(50)) log($"  held: {line}");
            if (!result.Drained)
                foreach (var line in Processing.WorkerRunner.DumpRunners().Take(60)) log($"  state: {line}");
            if (Interlocked.Read(ref Processing.WorkerRunner.AbandonedCalls) > 0)
                log($"worker database calls abandoned after Worker:OperationTimeoutMs: {Interlocked.Read(ref Processing.WorkerRunner.AbandonedCalls):N0}");
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            result.Error = "cancelled";
            log("cancelled — stopping workers and verifying partial state");
        }
        catch (Exception ex)
        {
            result.Error = $"{ex.GetType().Name}: {ex.Message}";
            log($"run aborted: {ex}");
        }
        finally
        {
            workersCts.Cancel();
            try { await Task.WhenAll(workerTasks).WaitAsync(TimeSpan.FromSeconds(30)); }
            catch (Exception ex) when (ex is OperationCanceledException or TimeoutException) { }
            foreach (var c in consumers) await SafeDispose(c);
            foreach (var p in publishers) await SafeDispose(p);
        }

        // ---- verify ----
        _phase = "verify";
        result.LargeEventsIssued = plan.LargeIssued;
        try
        {
            result.BrokerBacklog = await broker.GetBacklogAsync(CancellationToken.None);
            log("verifying end state against the oracle ...");
            result.Oracle = await StateOracle.VerifyAsync(plan, store, metrics, result.BrokerBacklog, CancellationToken.None);
        }
        catch (Exception ex)
        {
            result.Error ??= $"verification failed: {ex.GetType().Name}: {ex.Message}";
            log($"verification failed: {ex}");
        }

        samplerCts.Cancel();
        await samplerTask;
        try { sampler.FinalSample(); } catch (Exception ex) { log($"final metrics sample failed: {ex.Message}"); }
        result.TimeSeries = sampler.Snapshot();
        _phase = "done";
        return result;
    }

    private async Task<bool> DrainAsync(IBroker broker, MetricsHub metrics, TimeSpan timeout, CancellationToken ct)
    {
        var deadline = Stopwatch.StartNew();
        while (deadline.Elapsed < timeout)
        {
            ct.ThrowIfCancellationRequested();
            long produced = Interlocked.Read(ref metrics.Total.Produced);
            long applied = Interlocked.Read(ref metrics.Total.Applied);
            if (applied >= produced)
            {
                // All events applied; wait for leftover duplicates/acks to clear from the broker.
                long backlog = await broker.GetBacklogAsync(ct);
                if (backlog == 0) return true;
            }
            await Task.Delay(applied >= produced ? 250 : 100, ct);
        }
        return false;
    }

    private static async Task SafeDispose(IAsyncDisposable d)
    {
        try { await d.DisposeAsync(); } catch { /* best effort */ }
    }
}
