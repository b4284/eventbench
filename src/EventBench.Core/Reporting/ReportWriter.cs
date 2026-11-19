using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using EventBench.Core.Metrics;
using EventBench.Core.Runner;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.Reporting;

public static class ReportWriter
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private static readonly CultureInfo Ci = CultureInfo.InvariantCulture;

    /// <summary>Writes all artifacts and returns the result directory.</summary>
    public static string Write(RunResult r)
    {
        var stamp = r.StartedUtc.ToString("yyyyMMdd-HHmmss", Ci);
        var safeName = string.Concat(r.Config.Name.Select(c => char.IsLetterOrDigit(c) || c is '-' or '_' ? c : '_'));
        var dir = Path.Combine(r.Config.ResultsDirectory, $"{stamp}_{safeName}");
        Directory.CreateDirectory(dir);

        File.WriteAllText(Path.Combine(dir, "config.json"), ConfigToJson(r.RawConfig).ToJsonString(JsonOpts));
        var summary = BuildSummary(r);
        File.WriteAllText(Path.Combine(dir, "summary.json"), summary.ToJsonString(JsonOpts));
        File.WriteAllText(Path.Combine(dir, "summary.md"), BuildMarkdown(r, summary));
        WriteTimeSeries(Path.Combine(dir, "timeseries.csv"), r.TimeSeries);

        if (r.Config.Metrics.WriteHistograms)
        {
            var hdir = Path.Combine(dir, "histograms");
            Directory.CreateDirectory(hdir);
            foreach (var m in r.Metrics.All.Concat(r.Metrics.GroupE2E))
            {
                if (m.Summarize().Count == 0) continue;
                using var w = new StreamWriter(Path.Combine(hdir, Sanitize(m.Name) + ".hgrm"));
                w.WriteLine($"# {m.Name}: {m.Description} (values in ms)");
                m.WritePercentileDistribution(w);
            }
        }
        return dir;
    }

    private static string Sanitize(string s) => string.Concat(s.Select(c => char.IsLetterOrDigit(c) || c is '-' or '_' ? c : '_'));

    public static JsonObject BuildSummary(RunResult r)
    {
        var m = r.Metrics;
        var t = m.Total;
        var cfg = r.Config;
        double warmupEndT = r.RunStartT + cfg.Workload.WarmupSeconds;
        var steadyRows = r.TimeSeries.Where(x => x.Phase == "run" && x.T > warmupEndT + cfg.Metrics.SampleIntervalSeconds).ToList();
        if (steadyRows.Count == 0) steadyRows = r.TimeSeries.Where(x => x.Phase == "run").ToList();

        var latencies = new JsonArray();
        foreach (var s in m.All.Select(x => x.Summarize()).Where(s => s.Count > 0))
            latencies.Add(LatencyJson(s));

        var groups = new JsonArray();
        for (int i = 0; i < r.Topology.Groups.Count; i++)
        {
            var g = r.Topology.Groups[i];
            var c = m.Groups[i];
            var e = m.GroupE2E[i].Summarize();
            groups.Add(new JsonObject
            {
                ["name"] = g.Name, ["sources"] = g.SourceCount, ["workers"] = g.Workers,
                ["offeredRatePerSec"] = Round(g.RatePerGroup),
                ["baseRatePerSourcePerMin"] = Round(g.RatePerSource * 60),
                ["payloadBytes"] = g.PayloadBytes ?? cfg.Workload.PayloadBytes,
                ["largeEventFraction"] = g.LargeEventFraction,
                ["produced"] = c.Produced, ["applied"] = c.Applied, ["duplicates"] = c.Duplicates,
                ["gaps"] = c.Gaps, ["stateMismatches"] = c.StateMismatches, ["errors"] = c.Errors, ["nacks"] = c.Nacks, ["slowEvents"] = c.SlowEvents, ["blockingViolations"] = c.BlockingViolations,
                ["largeEvents"] = c.LargeEvents,
                ["e2eP50Ms"] = Round(e.P50Ms), ["e2eP99Ms"] = Round(e.P99Ms), ["e2eMaxMs"] = Round(e.MaxMs),
            });
        }

        var resources = new JsonObject();
        foreach (var col in steadyRows.SelectMany(x => x.Values.Keys).Distinct().Order(StringComparer.Ordinal))
        {
            var vals = steadyRows.Where(x => x.Values.ContainsKey(col)).Select(x => x.Values[col]).ToList();
            if (vals.Count == 0) continue;
            resources[col] = new JsonObject { ["mean"] = Round(vals.Average()), ["max"] = Round(vals.Max()) };
        }

        var oracle = new JsonObject();
        if (r.Oracle is { } o)
        {
            oracle["passed"] = o.Passed;
            oracle["sourcesVerified"] = o.SourcesVerified;
            oracle["recordsVerified"] = o.RecordsVerified;
            oracle["uploadsVerified"] = o.UploadsVerified;
            oracle["expectedEvents"] = o.ExpectedEvents;
            oracle["verifySeconds"] = Round(o.VerifySeconds);
            oracle["checks"] = new JsonArray(o.Checks.Select(c => (JsonNode)new JsonObject
                { ["name"] = c.Name, ["passed"] = c.Passed, ["detail"] = c.Detail }).ToArray());
            oracle["mismatchSamples"] = new JsonArray(o.MismatchSamples.Select(s => (JsonNode)s!).ToArray());
        }

        double steadyApplied = steadyRows.Count > 0 ? steadyRows.Average(x => x.Values.GetValueOrDefault("applied_per_s")) : 0;
        double steadyProduced = steadyRows.Count > 0 ? steadyRows.Average(x => x.Values.GetValueOrDefault("produced_per_s")) : 0;

        return new JsonObject
        {
            ["name"] = cfg.Name,
            ["verdict"] = r.Passed ? "PASS" : "FAIL",
            ["error"] = r.Error,
            ["startedUtc"] = r.StartedUtc.ToString("O"),
            ["broker"] = r.BrokerDescription,
            ["store"] = r.StoreDescription,
            ["ackMode"] = cfg.Worker.AckMode.ToString(),
            ["topology"] = new JsonObject
            {
                ["groups"] = r.Topology.Groups.Count,
                ["sources"] = r.Topology.TotalSources,
                ["workers"] = r.Topology.Groups.Sum(g => g.Workers),
                ["offeredRatePerSec"] = Round(r.Topology.TotalRate),
            },
            ["phases"] = new JsonObject
            {
                ["setupSeconds"] = Round(r.SetupSeconds), ["runSeconds"] = Round(r.RunSeconds),
                ["closingSeconds"] = Round(r.ClosingSeconds), ["drainSeconds"] = Round(r.DrainSeconds),
                ["drained"] = r.Drained,
            },
            ["throughput"] = new JsonObject
            {
                ["produced"] = t.Produced, ["applied"] = t.Applied, ["duplicates"] = t.Duplicates,
                ["gaps"] = t.Gaps, ["stateMismatches"] = t.StateMismatches, ["errors"] = t.Errors,
                ["nacks"] = t.Nacks, ["publishErrors"] = t.PublishErrors, ["largeEvents"] = t.LargeEvents, ["slowEvents"] = t.SlowEvents, ["blockingViolations"] = t.BlockingViolations,
                ["lingerHits"] = Interlocked.Read(ref m.LingerHits), ["lingerMisses"] = Interlocked.Read(ref m.LingerMisses),
                ["publishedMB"] = Round(t.BytesPublished / 1048576.0),
                ["steadyProducedPerSec"] = Round(steadyProduced),
                ["steadyAppliedPerSec"] = Round(steadyApplied),
                ["peakAppliedPerSec"] = Round(r.TimeSeries.Select(x => x.Values.GetValueOrDefault("applied_per_s")).DefaultIfEmpty().Max()),
                ["overallAppliedPerSec"] = Round(t.Applied / Math.Max(1e-9, r.RunSeconds + r.ClosingSeconds + r.DrainSeconds)),
            },
            ["latencyMs"] = latencies,
            ["groups"] = groups,
            ["resourcesSteadyState"] = resources,
            ["oracle"] = oracle,
            ["violationCounts"] = new JsonObject(m.ViolationCounts.Select(kv => KeyValuePair.Create(kv.Key, (JsonNode?)kv.Value))),
            ["violationSamples"] = new JsonArray(m.ViolationSamples.Take(50).Select(s => (JsonNode)s!).ToArray()),
            ["errorSamples"] = new JsonArray(m.ErrorSamples.Take(50).Select(s => (JsonNode)s!).ToArray()),
        };
    }

    private static JsonObject LatencyJson(LatencySummary s) => new()
    {
        ["metric"] = s.Name, ["count"] = s.Count, ["mean"] = Round(s.MeanMs), ["p50"] = Round(s.P50Ms),
        ["p90"] = Round(s.P90Ms), ["p99"] = Round(s.P99Ms), ["p99_9"] = Round(s.P999Ms), ["p99_99"] = Round(s.P9999Ms),
        ["max"] = Round(s.MaxMs), ["description"] = s.Description,
    };

    private static double Round(double v) => double.IsFinite(v) ? Math.Round(v, 3) : 0;

    private static string BuildMarkdown(RunResult r, JsonObject s)
    {
        var sb = new StringBuilder();
        var tp = s["throughput"]!.AsObject();
        var topo = s["topology"]!.AsObject();
        var ph = s["phases"]!.AsObject();
        sb.AppendLine($"# {r.Config.Name} — {(r.Passed ? "PASS ✅" : "FAIL ❌")}");
        sb.AppendLine();
        if (r.Error != null) sb.AppendLine($"**Error:** {r.Error}\n");
        sb.AppendLine($"- Broker: `{r.BrokerDescription}`");
        sb.AppendLine($"- Store: `{r.StoreDescription}`");
        sb.AppendLine($"- Ack mode: `{r.Config.Worker.AckMode}`, batch {r.Config.Worker.BatchSize}, parallel/worker {r.Config.Worker.MaxParallelPerWorker}");
        sb.AppendLine($"- Topology: {topo["groups"]} groups, {topo["sources"]:N0} sources, {topo["workers"]} workers, offered {topo["offeredRatePerSec"]} ev/s");
        sb.AppendLine($"- Phases: run {ph["runSeconds"]}s, closing {ph["closingSeconds"]}s, drain {ph["drainSeconds"]}s (drained: {ph["drained"]})");
        sb.AppendLine();
        sb.AppendLine("## Throughput");
        sb.AppendLine();
        sb.AppendLine("| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |");
        sb.AppendLine("|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|");
        sb.AppendLine($"| {tp["produced"]} | {tp["applied"]} | {tp["steadyProducedPerSec"]} | {tp["steadyAppliedPerSec"]} | {tp["peakAppliedPerSec"]} | {tp["duplicates"]} | {tp["errors"]} | {tp["nacks"]} | {tp["largeEvents"]} | {tp["slowEvents"]} | {tp["blockingViolations"]} |");
        sb.AppendLine();
        sb.AppendLine("## Latency (ms, after warmup, excluding closing events)");
        sb.AppendLine();
        sb.AppendLine("| metric | count | mean | p50 | p90 | p99 | p99.9 | max |");
        sb.AppendLine("|---|---:|---:|---:|---:|---:|---:|---:|");
        foreach (var l in s["latencyMs"]!.AsArray())
            sb.AppendLine($"| {l!["metric"]} | {l["count"]} | {l["mean"]} | {l["p50"]} | {l["p90"]} | {l["p99"]} | {l["p99_9"]} | {l["max"]} |");
        sb.AppendLine();
        sb.AppendLine("## Resources (steady state mean / max)");
        sb.AppendLine();
        sb.AppendLine("| metric | mean | max |");
        sb.AppendLine("|---|---:|---:|");
        foreach (var (k, v) in s["resourcesSteadyState"]!.AsObject())
            if (!k.Contains("_p50_") && !k.Contains("_p99_") && !k.Contains("_max_"))
                sb.AppendLine($"| {k} | {v!["mean"]} | {v["max"]} |");
        sb.AppendLine();
        sb.AppendLine("## Oracle");
        sb.AppendLine();
        if (r.Oracle is { } o)
        {
            sb.AppendLine("| check | result | detail |");
            sb.AppendLine("|---|---|---|");
            foreach (var c in o.Checks) sb.AppendLine($"| {c.Name} | {(c.Passed ? "pass" : "**FAIL**")} | {c.Detail} |");
            if (o.MismatchSamples.Count > 0)
            {
                sb.AppendLine("\nSample mismatches:\n");
                foreach (var x in o.MismatchSamples.Take(20)) sb.AppendLine($"- {x}");
            }
        }
        else sb.AppendLine("_not run_");
        if (!r.Metrics.BlockingSamples.IsEmpty)
        {
            sb.AppendLine("\n## Cross-source blocking samples\n");
            foreach (var x in r.Metrics.BlockingSamples) sb.AppendLine($"- {x}");
        }
        if (!r.Metrics.ViolationSamples.IsEmpty)
        {
            sb.AppendLine("\n## Runtime violation samples\n");
            foreach (var x in r.Metrics.ViolationSamples.Take(20)) sb.AppendLine($"- {x}");
        }
        if (!r.Metrics.ErrorSamples.IsEmpty)
        {
            sb.AppendLine("\n## Error samples\n");
            foreach (var x in r.Metrics.ErrorSamples.Take(20)) sb.AppendLine($"- {x}");
        }
        sb.AppendLine();
        var groupRows = s["groups"]!.AsArray().Select(g => g!.AsObject()).ToList();
        const int maxRows = 25;
        if (groupRows.Count > maxRows)
        {
            sb.AppendLine($"## Groups ({groupRows.Count}; worst {maxRows} by e2e p99 — all groups in summary.json)");
            groupRows = groupRows.OrderByDescending(g => (double)g["e2eP99Ms"]!).Take(maxRows).ToList();
        }
        else sb.AppendLine("## Groups");
        sb.AppendLine();
        sb.AppendLine("| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |");
        sb.AppendLine("|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|");
        foreach (var g in groupRows)
            sb.AppendLine($"| {g["name"]} | {g["sources"]} | {g["workers"]} | {g["baseRatePerSourcePerMin"]} | {g["offeredRatePerSec"]} | {g["payloadBytes"]} | {g["largeEventFraction"]} | {g["produced"]} | {g["applied"]} | {g["duplicates"]} | {g["e2eP50Ms"]} | {g["e2eP99Ms"]} | {g["e2eMaxMs"]} |");
        return sb.ToString();
    }

    private static void WriteTimeSeries(string path, List<TimeSeriesRow> rows)
    {
        var cols = rows.SelectMany(r => r.Values.Keys).Distinct().ToList();
        using var w = new StreamWriter(path);
        w.WriteLine("t_s,phase," + string.Join(",", cols));
        foreach (var r in rows)
        {
            w.Write(r.T.ToString(Ci));
            w.Write(',');
            w.Write(r.Phase);
            foreach (var c in cols)
            {
                w.Write(',');
                if (r.Values.TryGetValue(c, out var v)) w.Write(v.ToString(Ci));
            }
            w.WriteLine();
        }
    }

    /// <summary>Turns the effective IConfiguration into a JSON tree (secrets in connection strings are masked).</summary>
    public static JsonObject ConfigToJson(IConfiguration config)
    {
        var obj = new JsonObject();
        foreach (var child in config.GetChildren())
        {
            if (child.GetChildren().Any())
                obj[child.Key] = ConfigToJson(child);
            else
                obj[child.Key] = MaskSecrets(child.Key, child.Value);
        }
        return obj;
    }

    private static string? MaskSecrets(string key, string? value)
    {
        if (value is null) return null;
        if (key.Contains("Password", StringComparison.OrdinalIgnoreCase)) return "***";
        return System.Text.RegularExpressions.Regex.Replace(value, @"(?i)(password|pwd)\s*=\s*[^;]*", "$1=***");
    }
}
