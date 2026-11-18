using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EventBench.Core.Metrics;

/// <summary>Samples this process' CPU/memory/GC/thread-pool plus host-wide CPU from /proc/stat.</summary>
public sealed class ProcessSampler
{
    private readonly Process _proc = Process.GetCurrentProcess();
    private TimeSpan _lastCpu;
    private long _lastWallTicks;
    private (ulong busy, ulong total)? _lastHost;
    private int _lastGen0, _lastGen1, _lastGen2;

    public ProcessSampler()
    {
        _proc.Refresh();
        _lastCpu = _proc.TotalProcessorTime;
        _lastWallTicks = Stopwatch.GetTimestamp();
        _lastHost = ReadHostCpu();
    }

    public void Sample(IDictionary<string, double> row)
    {
        _proc.Refresh();
        var cpu = _proc.TotalProcessorTime;
        long now = Stopwatch.GetTimestamp();
        double wallSec = (now - _lastWallTicks) / (double)Stopwatch.Frequency;
        double cores = wallSec > 0 ? (cpu - _lastCpu).TotalSeconds / wallSec : 0;
        _lastCpu = cpu;
        _lastWallTicks = now;

        row["bench_cpu_cores"] = Math.Round(cores, 3);
        row["bench_cpu_pct"] = Math.Round(cores / Environment.ProcessorCount * 100, 2);
        row["bench_mem_mb"] = Math.Round(_proc.WorkingSet64 / 1048576.0, 1);
        row["bench_gc_heap_mb"] = Math.Round(GC.GetTotalMemory(false) / 1048576.0, 1);

        int g0 = GC.CollectionCount(0), g1 = GC.CollectionCount(1), g2 = GC.CollectionCount(2);
        row["bench_gc_gen0"] = g0 - _lastGen0;
        row["bench_gc_gen1"] = g1 - _lastGen1;
        row["bench_gc_gen2"] = g2 - _lastGen2;
        (_lastGen0, _lastGen1, _lastGen2) = (g0, g1, g2);

        row["bench_threadpool_threads"] = ThreadPool.ThreadCount;
        row["bench_threadpool_queue"] = ThreadPool.PendingWorkItemCount;

        var host = ReadHostCpu();
        if (host is { } h && _lastHost is { } l && h.total > l.total)
            row["host_cpu_pct"] = Math.Round((h.busy - l.busy) * 100.0 / (h.total - l.total), 2);
        _lastHost = host;
    }

    private static (ulong busy, ulong total)? ReadHostCpu()
    {
        try
        {
            if (!File.Exists("/proc/stat")) return null;
            using var reader = new StreamReader("/proc/stat");
            var line = reader.ReadLine();
            if (line is null || !line.StartsWith("cpu ")) return null;
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(ulong.Parse).ToArray();
            ulong total = 0;
            for (int i = 0; i < Math.Min(parts.Length, 8); i++) total += parts[i]; // user..steal (excl. guest)
            ulong idle = parts[3] + (parts.Length > 4 ? parts[4] : 0); // idle + iowait
            return (total - idle, total);
        }
        catch
        {
            return null;
        }
    }
}

/// <summary>
/// Periodically runs an external command (default: `podman stats --no-stream --format json`) and
/// exposes per-container CPU% and memory as time-series columns.
/// </summary>
public sealed partial class ContainerStatsSampler
{
    private readonly string _command;
    private readonly TimeSpan _interval;
    private readonly Action<string> _log;
    private volatile IReadOnlyDictionary<string, double> _latest = new Dictionary<string, double>();
    private bool _warned;

    public ContainerStatsSampler(string command, TimeSpan interval, Action<string> log)
    {
        _command = command;
        _interval = interval;
        _log = log;
    }

    public IReadOnlyDictionary<string, double> Latest => _latest;

    public async Task RunAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var output = await RunCommandAsync(ct);
                if (output != null) _latest = Parse(output);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { break; }
            catch (Exception ex)
            {
                if (!_warned) { _log($"container stats disabled after error: {ex.Message}"); _warned = true; }
                return;
            }
            try { await Task.Delay(_interval, ct); } catch (OperationCanceledException) { break; }
        }
    }

    private async Task<string?> RunCommandAsync(CancellationToken ct)
    {
        var psi = new ProcessStartInfo("/bin/sh", ["-c", _command])
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        using var p = Process.Start(psi) ?? throw new InvalidOperationException("cannot start container stats command");
        var stdout = p.StandardOutput.ReadToEndAsync(ct);
        var stderr = p.StandardError.ReadToEndAsync(ct);
        await p.WaitForExitAsync(ct);
        if (p.ExitCode != 0) throw new InvalidOperationException($"'{_command}' exited {p.ExitCode}: {(await stderr).Trim()}");
        return await stdout;
    }

    public static IReadOnlyDictionary<string, double> Parse(string json)
    {
        var result = new Dictionary<string, double>();
        json = json.Trim();
        if (json.Length == 0) return result;
        using var doc = JsonDocument.Parse(json);
        IEnumerable<JsonElement> items = doc.RootElement.ValueKind == JsonValueKind.Array
            ? doc.RootElement.EnumerateArray()
            : [doc.RootElement];
        foreach (var item in items)
        {
            string? name = Str(item, "name", "Name");
            if (name is null || name.EndsWith("-infra", StringComparison.Ordinal)) continue; // podman pod infra container
            name = SanitizeRegex().Replace(name, "_");
            var cpu = Str(item, "cpu_percent", "CPU", "CPUPerc", "cpu");
            if (cpu != null && TryParseNumber(cpu.TrimEnd('%'), out var c)) result[$"ctr_{name}_cpu_pct"] = c;
            var mem = Str(item, "mem_usage", "MemUsage", "mem");
            if (mem != null && TryParseBytes(mem.Split('/')[0].Trim(), out var bytes)) result[$"ctr_{name}_mem_mb"] = Math.Round(bytes / 1048576.0, 1);
        }
        return result;
    }

    private static string? Str(JsonElement e, params string[] names)
    {
        foreach (var n in names)
            if (e.TryGetProperty(n, out var v))
                return v.ValueKind == JsonValueKind.String ? v.GetString() : v.GetRawText();
        return null;
    }

    private static bool TryParseNumber(string s, out double v)
        => double.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out v);

    private static bool TryParseBytes(string s, out double bytes)
    {
        bytes = 0;
        var m = BytesRegex().Match(s);
        if (!m.Success || !TryParseNumber(m.Groups[1].Value, out var n)) return false;
        double mult = m.Groups[2].Value.ToUpperInvariant() switch
        {
            "" or "B" => 1,
            "KB" => 1e3, "KIB" => 1024,
            "MB" => 1e6, "MIB" => 1048576,
            "GB" => 1e9, "GIB" => 1073741824,
            "TB" => 1e12, "TIB" => 1099511627776,
            _ => 1,
        };
        bytes = n * mult;
        return true;
    }

    [GeneratedRegex(@"^([\d.]+)\s*([A-Za-z]*)")]
    private static partial Regex BytesRegex();

    [GeneratedRegex(@"[^A-Za-z0-9_]")]
    private static partial Regex SanitizeRegex();
}
