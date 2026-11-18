using System.Diagnostics;
using EventBench.Core.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EventBench.Mongo;

/// <summary>
/// Samples <c>serverStatus</c> (opcounters, connections, transactions, WiredTiger cache) plus the store's
/// own retry counters; cumulative counters become per-second rates. Columns are prefixed <c>mongo_</c>.
/// </summary>
public sealed class MongoServerStats : IStatsProvider
{
    private static readonly BsonDocument Command = new()
    {
        { "serverStatus", 1 }, { "repl", 0 }, { "metrics", 0 }, { "locks", 0 }, { "tcmalloc", 0 },
        { "storageEngine", 0 }, { "network", 0 }, { "asserts", 0 },
    };

    private readonly IMongoDatabase _admin;
    private readonly MongoStore _store;
    private readonly Action<string> _log;
    private Dictionary<string, double>? _prev;
    private long _prevTicks;
    private bool _failed;

    public MongoServerStats(MongoClient client, MongoStore store, Action<string> log)
    {
        _admin = client.GetDatabase("admin");
        _store = store;
        _log = log;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, double>();
        if (_failed) return result;
        var raw = new Dictionary<string, double>();
        try
        {
            var s = await _admin.RunCommandAsync<BsonDocument>(Command, cancellationToken: ct);
            static double Num(BsonDocument d, string path)
            {
                BsonValue cur = d;
                foreach (var part in path.Split('.'))
                {
                    if (!cur.IsBsonDocument || !cur.AsBsonDocument.TryGetValue(part, out cur)) return 0;
                }
                return cur.IsNumeric ? cur.ToDouble() : 0;
            }
            foreach (var op in new[] { "insert", "query", "update", "delete", "getmore", "command" })
                raw[$"op_{op}"] = Num(s, $"opcounters.{op}");
            raw["txn_committed"] = Num(s, "transactions.totalCommitted");
            raw["txn_aborted"] = Num(s, "transactions.totalAborted");
            result["mongo_conn_current"] = Num(s, "connections.current");
            result["mongo_txn_active"] = Num(s, "transactions.currentActive");
            result["mongo_wt_cache_mb"] = Math.Round(Num(s, "wiredTiger.cache.bytes currently in the cache") / 1048576.0, 1);
            result["mongo_wt_dirty_mb"] = Math.Round(Num(s, "wiredTiger.cache.tracked dirty bytes in the cache") / 1048576.0, 1);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _failed = true;
            _log($"mongo server stats disabled: {ex.Message}");
            return result;
        }
        raw["store_txn_retries"] = Interlocked.Read(ref _store.TxnRetries);
        raw["store_commit_retries"] = Interlocked.Read(ref _store.CommitRetries);
        raw["store_torn_loads"] = Interlocked.Read(ref _store.TornLoads);
        raw["store_embedded_retries"] = Interlocked.Read(ref _store.EmbeddedRetries);

        long now = Stopwatch.GetTimestamp();
        if (_prev != null)
        {
            double dt = (now - _prevTicks) / (double)Stopwatch.Frequency;
            foreach (var (k, v) in raw)
                result[$"mongo_{k}_per_s"] = Math.Round((v - _prev.GetValueOrDefault(k)) / dt, 1);
        }
        _prev = raw;
        _prevTicks = now;
        return result;
    }
}
