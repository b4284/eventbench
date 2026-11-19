using System.Globalization;
using EventBench.Core.Abstractions;
using StackExchange.Redis;

namespace EventBench.Redis;

/// <summary>
/// A small pool of multiplexed connections. One multiplexer pipelines every command over a single
/// socket, so a few MB-sized values would block everyone behind them; spreading callers over N
/// connections bounds that head-of-line blocking.
/// </summary>
internal sealed class RedisPool : IAsyncDisposable
{
    private readonly ConnectionMultiplexer[] _muxes;

    public RedisPool(string configuration, int connections)
    {
        var opts = ConfigurationOptions.Parse(configuration);
        opts.AbortOnConnectFail = false;
        opts.AllowAdmin = true; // CONFIG GET / INFO for the report
        if (opts.SyncTimeout < 30_000) opts.SyncTimeout = 30_000;
        if (opts.AsyncTimeout < 30_000) opts.AsyncTimeout = 30_000;
        _muxes = Enumerable.Range(0, Math.Max(1, connections)).Select(_ => ConnectionMultiplexer.Connect(opts)).ToArray();
        Configuration = configuration;
    }

    public string Configuration { get; }
    public int Count => _muxes.Length;
    public IDatabase Db(int key) => _muxes[(int)((uint)key % (uint)_muxes.Length)].GetDatabase();
    /// <summary>The current primary (with Sentinel: whichever node is primary now).</summary>
    public IServer Server => _muxes[0].GetServers().FirstOrDefault(s => s.IsConnected && !s.IsReplica) ?? _muxes[0].GetServers()[0];

    /// <summary>WAIT numreplicas timeout: returns once that many replicas acknowledged this connection's writes.</summary>
    public static async Task WaitReplicasAsync(IDatabase db, int replicas, int timeoutMs)
    {
        long acked = (long)await db.ExecuteAsync("WAIT", replicas, timeoutMs);
        if (acked < replicas) throw new RedisException($"WAIT: only {acked}/{replicas} replicas acknowledged within {timeoutMs} ms");
    }

    /// <summary>Deletes every key matching the pattern (SCAN + UNLINK).</summary>
    public async Task<long> DeleteByPatternAsync(string pattern)
    {
        long n = 0;
        var db = Db(0);
        var batch = new List<RedisKey>(1000);
        await foreach (var key in Server.KeysAsync(pattern: pattern, pageSize: 1000))
        {
            batch.Add(key);
            if (batch.Count == 1000) { n += await db.KeyDeleteAsync(batch.ToArray()); batch.Clear(); }
        }
        if (batch.Count > 0) n += await db.KeyDeleteAsync(batch.ToArray());
        return n;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var m in _muxes) await m.DisposeAsync();
    }
}

/// <summary>INFO-based server stats (ops, memory, AOF, clients), cumulative counters turned into rates.</summary>
internal sealed class RedisStats(RedisPool pool, string prefix, Action<string> log) : IStatsProvider
{
    private static readonly string[] Gauges =
        ["connected_clients", "used_memory", "aof_current_size", "aof_buffer_length", "aof_pending_bio_fsync", "instantaneous_ops_per_sec", "blocked_clients"];
    private static readonly string[] Counters =
        ["total_commands_processed", "aof_delayed_fsync", "total_net_input_bytes", "total_net_output_bytes", "used_cpu_sys", "used_cpu_user", "used_cpu_sys_main_thread", "used_cpu_user_main_thread"];

    private Dictionary<string, double>? _prev;
    private long _prevTicks;
    private bool _failed;

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, double>();
        var info = new Dictionary<string, string>();
        try
        {
            var text = (string?)await pool.Db(0).ExecuteAsync("INFO", "everything") ?? "";
            foreach (var line in text.Split('\n'))
            {
                int i = line.IndexOf(':');
                if (i > 0) info[line[..i]] = line[(i + 1)..].Trim();
            }
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            if (!_failed) log($"redis stats unavailable: {e.Message}");
            _failed = true;
            return result;
        }

        double Get(string k) => info.TryGetValue(k, out var v) && double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) ? d : 0;
        foreach (var g in Gauges) result[$"{prefix}_{g}"] = g.Contains("memory") || g.Contains("size") ? Math.Round(Get(g) / 1048576.0, 1) : Get(g);
        var cur = Counters.ToDictionary(c => c, Get);
        long now = Environment.TickCount64;
        if (_prev != null)
        {
            double secs = Math.Max(0.001, (now - _prevTicks) / 1000.0);
            foreach (var (k, v) in cur)
            {
                double rate = (v - _prev[k]) / secs;
                // used_cpu_* are seconds of CPU -> cores busy.
                if (k.StartsWith("used_cpu")) result[$"{prefix}_{k}_cores"] = Math.Round(rate, 2);
                else result[$"{prefix}_{k}_per_s"] = Math.Round(rate, 1);
            }
        }
        _prev = cur;
        _prevTicks = now;
        return result;
    }
}
