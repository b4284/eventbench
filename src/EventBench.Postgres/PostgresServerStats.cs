using System.Diagnostics;
using System.Globalization;
using EventBench.Core.Abstractions;
using Npgsql;

namespace EventBench.Postgres;

/// <summary>
/// Samples Postgres server counters (pg_stat_database, pg_stat_activity, pg_stat_wal) and, optionally,
/// PgBouncer pool state (SHOW POOLS) — converted to per-second rates where they are cumulative.
/// </summary>
public sealed class PostgresServerStats : IStatsProvider
{
    private readonly NpgsqlDataSource _admin;
    private readonly string? _bouncerCommand;
    private readonly Action<string> _log;
    private Dictionary<string, double>? _prev;
    private long _prevTicks;
    private bool _bouncerFailed;

    public PostgresServerStats(NpgsqlDataSource admin, string? pgBouncerStatsCommand, Action<string> log)
    {
        _admin = admin;
        _log = log;
        _bouncerCommand = string.IsNullOrWhiteSpace(pgBouncerStatsCommand) ? null : pgBouncerStatsCommand;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var raw = new Dictionary<string, double>();
        var result = new Dictionary<string, double>();
        await using (var conn = await _admin.OpenConnectionAsync(ct))
        {
            await using (var cmd = new NpgsqlCommand(
                "SELECT xact_commit, xact_rollback, tup_inserted, tup_updated, tup_deleted, tup_fetched, blks_read, blks_hit, deadlocks, temp_bytes, " +
                "pg_database_size(datname) FROM pg_stat_database WHERE datname = current_database()", conn))
            await using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                if (await rd.ReadAsync(ct))
                {
                    for (int i = 0; i < 10; i++) raw[rd.GetName(i)] = Convert.ToDouble(rd.GetValue(i));
                    result["pg_db_size_mb"] = Math.Round(Convert.ToDouble(rd.GetValue(10)) / 1048576.0, 1);
                }
            }
            await using (var cmd = new NpgsqlCommand("SELECT wal_bytes::float8 FROM pg_stat_wal", conn))
                raw["wal_bytes"] = Convert.ToDouble(await cmd.ExecuteScalarAsync(ct));
            await using (var cmd = new NpgsqlCommand(
                "SELECT count(*) FILTER (WHERE state = 'active'), count(*) FILTER (WHERE state = 'idle'), " +
                "count(*) FILTER (WHERE state LIKE 'idle in transaction%'), count(*) FILTER (WHERE wait_event_type = 'Lock'), count(*) " +
                "FROM pg_stat_activity WHERE datname = current_database() AND pid <> pg_backend_pid()", conn))
            await using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                if (await rd.ReadAsync(ct))
                {
                    result["pg_conn_active"] = rd.GetInt64(0);
                    result["pg_conn_idle"] = rd.GetInt64(1);
                    result["pg_conn_idle_in_tx"] = rd.GetInt64(2);
                    result["pg_conn_lock_wait"] = rd.GetInt64(3);
                    result["pg_conn_total"] = rd.GetInt64(4);
                }
            }
        }

        long now = Stopwatch.GetTimestamp();
        if (_prev != null)
        {
            double dt = (now - _prevTicks) / (double)Stopwatch.Frequency;
            double Rate(string k) => Math.Round((raw.GetValueOrDefault(k) - _prev.GetValueOrDefault(k)) / dt, 1);
            result["pg_tps"] = Rate("xact_commit") + Rate("xact_rollback");
            result["pg_rollbacks_per_s"] = Rate("xact_rollback");
            result["pg_tup_ins_per_s"] = Rate("tup_inserted");
            result["pg_tup_upd_per_s"] = Rate("tup_updated");
            result["pg_tup_del_per_s"] = Rate("tup_deleted");
            result["pg_tup_fetch_per_s"] = Rate("tup_fetched");
            result["pg_wal_mb_per_s"] = Math.Round(Rate("wal_bytes") / 1048576.0, 2);
            result["pg_temp_mb_per_s"] = Math.Round(Rate("temp_bytes") / 1048576.0, 2);
            result["pg_deadlocks"] = raw.GetValueOrDefault("deadlocks");
            double hit = Rate("blks_hit"), read = Rate("blks_read");
            if (hit + read > 0) result["pg_cache_hit_pct"] = Math.Round(hit * 100 / (hit + read), 2);
        }
        _prev = raw;
        _prevTicks = now;

        if (_bouncerCommand != null && !_bouncerFailed)
        {
            try
            {
                await SampleBouncerAsync(result, ct);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _bouncerFailed = true;
                _log($"pgbouncer stats disabled: {ex.Message}");
            }
        }
        return result;
    }

    /// <summary>
    /// The PgBouncer admin console only speaks the simple query protocol (Npgsql always uses the extended one),
    /// so SHOW POOLS is obtained by running an external command that prints CSV (psql --csv).
    /// </summary>
    private async Task SampleBouncerAsync(Dictionary<string, double> result, CancellationToken ct)
    {
        var psi = new ProcessStartInfo("/bin/sh", ["-c", _bouncerCommand!])
        {
            RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false,
        };
        using var p = Process.Start(psi) ?? throw new InvalidOperationException("cannot start pgbouncer stats command");
        var stdoutTask = p.StandardOutput.ReadToEndAsync(ct);
        var stderrTask = p.StandardError.ReadToEndAsync(ct);
        await p.WaitForExitAsync(ct);
        if (p.ExitCode != 0) throw new InvalidOperationException($"exit {p.ExitCode}: {(await stderrTask).Trim()}");
        var lines = (await stdoutTask).Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (lines.Length == 0) return;
        var header = lines[0].Split(',');
        int Col(string n) => Array.IndexOf(header, n);
        double clActive = 0, clWaiting = 0, svActive = 0, svIdle = 0, svUsed = 0, maxWaitMs = 0;
        foreach (var line in lines.Skip(1))
        {
            var f = line.Split(',');
            double Get(string n) => Col(n) is var i and >= 0 && i < f.Length && double.TryParse(f[i], CultureInfo.InvariantCulture, out var v) ? v : 0;
            if (Col("database") is var di and >= 0 && di < f.Length && f[di] == "pgbouncer") continue;
            clActive += Get("cl_active");
            clWaiting += Get("cl_waiting");
            svActive += Get("sv_active");
            svIdle += Get("sv_idle");
            svUsed += Get("sv_used");
            maxWaitMs = Math.Max(maxWaitMs, Get("maxwait") * 1000 + Get("maxwait_us") / 1000);
        }
        result["pgb_cl_active"] = clActive;
        result["pgb_cl_waiting"] = clWaiting;
        result["pgb_sv_active"] = svActive;
        result["pgb_sv_idle"] = svIdle;
        result["pgb_sv_used"] = svUsed;
        result["pgb_maxwait_ms"] = Math.Round(maxWaitMs, 2);
    }
}
