using EventBench.Core.Abstractions;
using Microsoft.Data.SqlClient;

namespace EventBench.SqlServer;

/// <summary>
/// Samples SQL Server performance counters (sys.dm_os_performance_counters) and wait statistics,
/// converting the cumulative ones into per-second rates.
/// </summary>
public sealed class SqlServerStats(string connectionString, string prefix, Action<string> log) : IStatsProvider
{
    // Cumulative counters (cntr_type 272696576) -> rate; the rest are point-in-time values.
    private const string CounterSql = """
        SELECT RTRIM(counter_name), RTRIM(instance_name), cntr_type, cntr_value
        FROM sys.dm_os_performance_counters
        WHERE (counter_name IN ('Batch Requests/sec', 'SQL Compilations/sec', 'User Connections', 'Processes blocked')
               AND instance_name = '')
           OR (counter_name IN ('Transactions/sec', 'Log Flushes/sec', 'Log Bytes Flushed/sec', 'Log Flush Waits/sec')
               AND instance_name = DB_NAME())
           OR (counter_name IN ('Lock Waits/sec', 'Number of Deadlocks/sec', 'Lock Timeouts/sec') AND instance_name = '_Total')
        """;
    // Top waits by accumulated wait time; reported as ms of waiting per second.
    private const string WaitSql = """
        SELECT wait_type, wait_time_ms FROM sys.dm_os_wait_stats
        WHERE wait_type IN ('WRITELOG', 'LCK_M_U', 'LCK_M_X', 'LCK_M_S', 'PAGELATCH_EX', 'PAGELATCH_SH', 'PAGEIOLATCH_SH',
                            'SOS_SCHEDULER_YIELD', 'THREADPOOL', 'ASYNC_NETWORK_IO', 'CXPACKET', 'LOGBUFFER', 'HADR_SYNC_COMMIT')
        """;

    private Dictionary<string, double>? _prev;
    private long _prevTicks;
    private bool _failed;

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, double>();
        var cumulative = new Dictionary<string, double>();
        try
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync(ct);
            await using (var cmd = new SqlCommand(CounterSql, conn))
            await using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                while (await rd.ReadAsync(ct))
                {
                    var key = Key(rd.GetString(0));
                    double v = rd.GetInt64(3);
                    if (rd.GetInt32(2) == 272696576) cumulative[key] = v; else result[key] = v;
                }
            }
            await using (var cmd = new SqlCommand(WaitSql, conn))
            await using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                while (await rd.ReadAsync(ct)) cumulative[prefix + "_wait_" + rd.GetString(0).ToLowerInvariant() + "_ms"] = rd.GetInt64(1);
            }
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            if (!_failed) log($"sqlserver stats unavailable: {e.Message}");
            _failed = true;
            return result;
        }

        long now = Environment.TickCount64;
        if (_prev != null)
        {
            double secs = Math.Max(0.001, (now - _prevTicks) / 1000.0);
            foreach (var (k, v) in cumulative)
                if (_prev.TryGetValue(k, out var p)) result[k.EndsWith("_ms") ? k[..^3] + "_ms_per_s" : k + "_per_s"] = Math.Round((v - p) / secs, 1);
        }
        _prev = cumulative;
        _prevTicks = now;
        return result;
    }

    private string Key(string counter) => prefix + "_" + counter.ToLowerInvariant()
        .Replace("/sec", "").Replace(' ', '_').Replace("number_of_", "");
}
