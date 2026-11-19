using System.Globalization;
using System.Runtime.CompilerServices;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace EventBench.Redis;

public sealed class RedisStoreOptions
{
    public string Configuration { get; set; } = "127.0.0.1:6379";
    public string KeyPrefix { get; set; } = "ebs";
    public int Connections { get; set; } = 8;
    /// <summary>After each commit, WAITAOF 1 0 (fsync-parity mode). Off = best effort (appendfsync policy).</summary>
    public bool WaitAof { get; set; } = false;
}

/// <summary>
/// Redis state store. Keys: {p}:c:{source} (cursor, absent = 0), {p}:r:{record} (hash), {p}:u:{source}:{seq}
/// (upload hash), {p}:b:{blob}. Load and commit are Lua scripts, so each is one atomic round trip.
/// The commit script checks the cursor, applies the version-guarded mutation, stores the upload,
/// advances the cursor and — in AckMode=InTransaction with a Redis Streams broker on the same server —
/// XACKs (+XDELs) the stream entry inside the same script.
/// </summary>
public sealed class RedisStore : IStateStore
{
    private const string LoadScript = """
        local last = redis.call('GET', KEYS[1]) or '0'
        local r = redis.call('HMGET', KEYS[2], 's', 'v', 'h', 'st', 'ctx')
        if r[1] ~= ARGV[1] then return {last} end
        return {last, r[2], r[3], r[4], r[5] or ''}
        """;

    // ARGV: 1 seq, 2 kind, 3 source, 4 prev_version, 5 version, 6 hash, 7 status, 8 has_ctx, 9 ctx,
    //       10 has_upload, 11 size, 12 checksum, 13 data, 14 record id, 15 ack (0/1/2=ack+del), 16 group, 17 entry id
    // KEYS: 1 cursor, 2 record, 3 upload, 4 ack stream
    private const string CommitScript = """
        local function ack()
          if ARGV[15] ~= '0' then
            redis.call('XACK', KEYS[4], ARGV[16], ARGV[17])
            if ARGV[15] == '2' then redis.call('XDEL', KEYS[4], ARGV[17]) end
          end
        end
        local last = tonumber(redis.call('GET', KEYS[1]) or '0')
        local seq = tonumber(ARGV[1])
        if last >= seq then ack() return 1 end
        if last ~= seq - 1 then return 2 end
        local kind = ARGV[2]
        if kind == '1' then
          if redis.call('EXISTS', KEYS[2]) == 1 then return 3 end
          redis.call('HSET', KEYS[2], 's', ARGV[3], 'st', ARGV[7], 'v', ARGV[5], 'h', ARGV[6], 'cs', ARGV[1], 'us', ARGV[1])
          if ARGV[8] == '1' then redis.call('HSET', KEYS[2], 'ctx', ARGV[9]) end
        elseif kind == '2' then
          local cur = redis.call('HMGET', KEYS[2], 's', 'v')
          if cur[1] ~= ARGV[3] or cur[2] ~= ARGV[4] then return 3 end
          redis.call('HSET', KEYS[2], 'st', ARGV[7], 'v', ARGV[5], 'h', ARGV[6], 'us', ARGV[1])
          if ARGV[8] == '1' then redis.call('HSET', KEYS[2], 'ctx', ARGV[9]) end
          if ARGV[7] == '2' then redis.call('HSET', KEYS[2], 'cps', ARGV[1]) end
        end
        if ARGV[10] == '1' then
          redis.call('HSET', KEYS[3], 'rid', ARGV[14], 'n', ARGV[11], 'c', ARGV[12], 'd', ARGV[13])
        end
        redis.call('SET', KEYS[1], ARGV[1])
        ack()
        return 0
        """;

    private readonly RedisStoreOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly RedisPool _pool;
    private readonly string _p;

    public string Name => "redis";
    public IStatsProvider? Stats { get; }

    public RedisStore(RedisStoreOptions options, Topology topology, Action<string> log)
    {
        _o = options;
        _topology = topology;
        _log = log;
        _p = options.KeyPrefix;
        _pool = new RedisPool(options.Configuration, options.Connections);
        Stats = new RedisStats(_pool, "redis", log);
    }

    private RedisKey CursorKey(int source) => $"{_p}:c:{source}";
    private RedisKey RecordKey(long record) => $"{_p}:r:{record}";
    private RedisKey UploadKey(int source, long seq) => $"{_p}:u:{source}:{seq}";
    private RedisKey BlobKey(long blob) => $"{_p}:b:{blob}";

    public bool SupportsTransactionalAck(IBroker broker)
    {
        if (broker is not RedisStreamsBroker rs) return false;
        if (!SameEndpoint(rs.Options.Configuration, _o.Configuration))
        {
            _log($"transactional ack unavailable: store {_o.Configuration} != broker {rs.Options.Configuration}");
            return false;
        }
        return true;
    }

    private static bool SameEndpoint(string a, string b)
    {
        static string Ep(string c) => string.Join(",", ConfigurationOptions.Parse(c).EndPoints.Select(e => e.ToString()).Order());
        return Ep(a) == Ep(b);
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        long dropped = await _pool.DeleteByPatternAsync(_p + ":*");
        var db = _pool.Db(0);
        await db.ScriptEvaluateAsync(LoadScript, [CursorKey(0), RecordKey(0)], ["-1"]); // warm the script cache
        var fsync = (RedisResult[])(await db.ExecuteAsync("CONFIG", "GET", "appendfsync"))!;
        _log($"redis store: dropped {dropped} old keys; {_topology.TotalSources:N0} source cursors (absent key = 0); appendfsync={fsync[1]}, waitaof={_o.WaitAof}");
    }

    private IDatabase DbFor(int source) => _pool.Db(source);

    public async ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        var r = (RedisResult[])(await DbFor(ev.SourceId).ScriptEvaluateAsync(LoadScript,
            [CursorKey(ev.SourceId), RecordKey(ev.RecordId)], [ev.SourceId]))!;
        long last = (long)r[0];
        if (r.Length == 1) return new WorkContext { LastSeq = last };
        var ctx = (byte[]?)r[4];
        return new WorkContext
        {
            LastSeq = last,
            RecordExists = true,
            Version = (int)r[1],
            Hash = (long)r[2],
            Status = (RecordStatus)(int)r[3],
            Context = ctx is { Length: > 0 } ? ctx : null,
        };
    }

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        var ack = req.TransactionalAck as IRedisStreamDelivery;
        if (req.TransactionalAck != null && ack is null)
            throw new InvalidOperationException("transactional ack requires a redis streams delivery");
        var up = req.Upload;
        RedisKey[] keys = [CursorKey(ev.SourceId), RecordKey(ev.RecordId), UploadKey(ev.SourceId, ev.Seq), ack != null ? (RedisKey)ack.Stream : CursorKey(ev.SourceId)];
        RedisValue[] args =
        [
            ev.Seq, (int)req.Kind, ev.SourceId, req.PrevVersion, req.NewVersion, req.NewHash, (int)req.NewStatus,
            req.NewContext != null ? 1 : 0, req.NewContext ?? RedisValue.EmptyString,
            up != null ? 1 : 0, up?.Size ?? 0, up?.Checksum ?? 0, up?.Data ?? RedisValue.EmptyString,
            ev.RecordId,
            ack is null ? 0 : (ack.DeleteOnAck ? 2 : 1), ack?.ConsumerGroup ?? "", ack?.EntryId ?? "",
        ];
        var db = DbFor(ev.SourceId);
        int code = (int)await db.ScriptEvaluateAsync(CommitScript, keys, args);
        if (_o.WaitAof && code == 0) await db.ExecuteAsync("WAITAOF", 1, 0, 0);
        return code switch
        {
            0 => CommitStatus.Applied,
            1 => CommitStatus.Duplicate,
            2 => CommitStatus.Gap,
            _ => CommitStatus.RecordConflict,
        };
    }

    public async ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
        => await DbFor(sourceId).StringSetAsync(BlobKey(blobId), data, when: When.NotExists);

    public async ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
        => await _pool.Db((int)blobId).StringGetAsync(BlobKey(blobId));

    public async IAsyncEnumerable<SourceCursor> ScanCursorsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var db = _pool.Db(0);
        for (int start = 0; start < _topology.TotalSources; start += 1000)
        {
            int n = Math.Min(1000, _topology.TotalSources - start);
            var vals = await db.StringGetAsync(Enumerable.Range(start, n).Select(CursorKey).ToArray());
            for (int i = 0; i < n; i++)
                yield return new SourceCursor(start + i, vals[i].IsNull ? 0 : (long)vals[i]);
        }
    }

    private async IAsyncEnumerable<(RedisKey Key, RedisValue[] Fields)> ScanHashes(string pattern, RedisValue[] fields,
        [EnumeratorCancellation] CancellationToken ct)
    {
        var db = _pool.Db(0);
        var keys = new List<RedisKey>(1000);
        async Task<(RedisKey, RedisValue[])[]> Flush()
        {
            var ks = keys.ToArray();
            keys.Clear();
            var rows = await Task.WhenAll(ks.Select(k => db.HashGetAsync(k, fields)));
            return ks.Zip(rows).ToArray();
        }
        await foreach (var key in _pool.Server.KeysAsync(pattern: pattern, pageSize: 1000))
        {
            keys.Add(key);
            if (keys.Count == 1000) foreach (var row in await Flush()) yield return row;
        }
        if (keys.Count > 0) foreach (var row in await Flush()) yield return row;
    }

    public async IAsyncEnumerable<RecordRow> ScanRecordsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        int prefixLen = _p.Length + 3;
        await foreach (var (key, f) in ScanHashes($"{_p}:r:*", ["s", "st", "v", "h"], ct))
            yield return new RecordRow((int)f[0], long.Parse(((string)key!)[prefixLen..], CultureInfo.InvariantCulture),
                (RecordStatus)(int)f[1], (int)f[2], (long)f[3]);
    }

    public async IAsyncEnumerable<UploadRow> ScanUploadsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        int prefixLen = _p.Length + 3;
        await foreach (var (key, f) in ScanHashes($"{_p}:u:*", ["n", "c"], ct))
        {
            var parts = ((string)key!)[prefixLen..].Split(':');
            yield return new UploadRow(int.Parse(parts[0], CultureInfo.InvariantCulture), long.Parse(parts[1], CultureInfo.InvariantCulture), (int)f[0], (long)f[1]);
        }
    }

    public string Describe()
        => $"redis @ {_o.Configuration}, {_o.Connections} connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof={_o.WaitAof}";

    public ValueTask DisposeAsync() => _pool.DisposeAsync();
}

public sealed class RedisStoreFactory : IStateStoreFactory
{
    public string Type => "redis";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
        => new RedisStore(section.Get<RedisStoreOptions>() ?? new(), context.Topology, context.Log);
}
