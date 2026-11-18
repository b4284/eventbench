using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using EventBench.Core.Abstractions;
using EventBench.Core.Config;

namespace EventBench.Core.Metrics;

public sealed class TimeSeriesRow
{
    public required double T { get; init; }
    public required string Phase { get; init; }
    public required Dictionary<string, double> Values { get; init; }
}

/// <summary>Builds one time-series row per interval and prints live progress.</summary>
public sealed class MetricsSampler
{
    private readonly MetricsHub _hub;
    private readonly MetricsOptions _options;
    private readonly IReadOnlyList<(string Name, IStatsProvider Provider)> _providers;
    private readonly Func<string> _phase;
    private readonly Action<string> _log;
    private readonly ProcessSampler _process = new();
    private readonly ContainerStatsSampler? _containers;
    private readonly ConcurrentDictionary<string, double> _external = new();
    private readonly HashSet<string> _failedProviders = new();
    private readonly Stopwatch _sw = Stopwatch.StartNew();
    private readonly object _rowsLock = new();

    private long _lastProduced, _lastApplied, _lastDup, _lastErr, _lastNack, _lastBytes;

    public List<TimeSeriesRow> Rows { get; } = new();

    public MetricsSampler(MetricsHub hub, MetricsOptions options, IReadOnlyList<(string, IStatsProvider)> providers,
        Func<string> phase, Action<string> log)
    {
        _hub = hub;
        _options = options;
        _providers = providers;
        _phase = phase;
        _log = log;
        if (options.ContainerStats && !string.IsNullOrWhiteSpace(options.ContainerStatsCommand))
            _containers = new ContainerStatsSampler(options.ContainerStatsCommand,
                TimeSpan.FromSeconds(options.ContainerStatsIntervalSeconds), log);
    }

    public async Task RunAsync(CancellationToken ct)
    {
        var tasks = new List<Task> { SampleLoopAsync(ct), ExternalLoopAsync(ct) };
        if (_containers != null) tasks.Add(_containers.RunAsync(ct));
        try { await Task.WhenAll(tasks); }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
    }

    /// <summary>Takes one final sample (call after the loops are stopped) so the tail of the run is captured.</summary>
    public void FinalSample() => SampleOnce(print: false);

    private async Task SampleLoopAsync(CancellationToken ct)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_options.SampleIntervalSeconds));
        try
        {
            int failures = 0;
            while (await timer.WaitForNextTickAsync(ct))
            {
                try { SampleOnce(_options.ConsoleProgress); }
                catch (Exception ex)
                {
                    if (++failures <= 3) _log($"metrics sample failed: {ex}");
                }
            }
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
    }

    private async Task ExternalLoopAsync(CancellationToken ct)
    {
        if (_providers.Count == 0) return;
        while (!ct.IsCancellationRequested)
        {
            foreach (var (name, provider) in _providers)
            {
                if (_failedProviders.Contains(name)) continue;
                try
                {
                    var values = await provider.SampleAsync(ct);
                    foreach (var kv in values) _external[kv.Key] = kv.Value;
                }
                catch (OperationCanceledException) when (ct.IsCancellationRequested) { return; }
                catch (Exception ex)
                {
                    _failedProviders.Add(name);
                    _log($"stats provider '{name}' disabled: {ex.Message}");
                }
            }
            try { await Task.Delay(TimeSpan.FromSeconds(_options.ExternalStatsIntervalSeconds), ct); }
            catch (OperationCanceledException) { return; }
        }
    }

    private double _lastT;

    private void SampleOnce(bool print)
    {
        double t = _sw.Elapsed.TotalSeconds;
        double dt = Math.Max(1e-6, t - _lastT);
        _lastT = t;
        var total = _hub.Total;
        var row = new Dictionary<string, double>();

        long produced = Interlocked.Read(ref total.Produced), applied = Interlocked.Read(ref total.Applied);
        long dup = Interlocked.Read(ref total.Duplicates), err = Interlocked.Read(ref total.Errors);
        long nack = Interlocked.Read(ref total.Nacks), bytes = Interlocked.Read(ref total.BytesPublished);
        row["produced_per_s"] = Math.Round((produced - _lastProduced) / dt, 1);
        row["applied_per_s"] = Math.Round((applied - _lastApplied) / dt, 1);
        row["duplicates_per_s"] = Math.Round((dup - _lastDup) / dt, 1);
        row["errors_per_s"] = Math.Round((err - _lastErr) / dt, 1);
        row["nacks_per_s"] = Math.Round((nack - _lastNack) / dt, 1);
        row["publish_mb_per_s"] = Math.Round((bytes - _lastBytes) / dt / 1048576.0, 2);
        row["produced_total"] = produced;
        row["applied_total"] = applied;
        row["in_flight"] = produced - applied;
        (_lastProduced, _lastApplied, _lastDup, _lastErr, _lastNack, _lastBytes) = (produced, applied, dup, err, nack, bytes);

        IntervalSnapshot e2e = default, qwait = default;
        foreach (var m in _hub.All)
        {
            var snap = m.Rotate();
            if (m == _hub.E2E) e2e = snap;
            if (m == _hub.QueueWait) qwait = snap;
            if (snap.Count == 0) continue;
            row[$"{m.Name}_p50_ms"] = Math.Round(snap.P50Ms, 3);
            row[$"{m.Name}_p99_ms"] = Math.Round(snap.P99Ms, 3);
            row[$"{m.Name}_max_ms"] = Math.Round(snap.MaxMs, 3);
        }
        foreach (var g in _hub.GroupE2E) g.Rotate();

        _process.Sample(row);
        foreach (var kv in _external) row[kv.Key] = kv.Value;
        if (_containers != null) foreach (var kv in _containers.Latest) row[kv.Key] = kv.Value;

        string phase = _phase();
        lock (_rowsLock) Rows.Add(new TimeSeriesRow { T = Math.Round(t, 3), Phase = phase, Values = row });

        if (print)
        {
            var ci = CultureInfo.InvariantCulture;
            string backlog = row.TryGetValue("broker_backlog", out var b) ? $" broker_q {b,8:N0}" : "";
            _log(string.Format(ci,
                "[{0,7:F1}s {1,-8}] prod {2,8:N0}/s  done {3,8:N0}/s  in-flight {4,8:N0}{5}  e2e p50/p99 {6,8:F1}/{7,8:F1} ms  qwait p99 {8,8:F1} ms  cpu bench {9,5:F1}% host {10,5:F1}%",
                t, phase, row["produced_per_s"], row["applied_per_s"], row["in_flight"], backlog,
                e2e.P50Ms, e2e.P99Ms, qwait.P99Ms,
                row["bench_cpu_pct"], row.GetValueOrDefault("host_cpu_pct")));
        }
    }

    public List<TimeSeriesRow> Snapshot()
    {
        lock (_rowsLock) return Rows.ToList();
    }
}
