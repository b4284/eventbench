using System.Globalization;
using System.Runtime.CompilerServices;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.KeyValueStore;
using NATS.Client.ObjectStore;
using NATS.Client.ObjectStore.Models;

namespace EventBench.Nats;

/// <summary>
/// State store on NATS JetStream KV (+ Object Store for MB-sized uploads and claim-check blobs).
///
/// JetStream KV has no multi-key transactions, so a commit is built from single-key compare-and-set:
/// <list type="number">
/// <item>Cursor key <c>c{source}</c> (absent = 0) is read with its revision: ≥ seq → Duplicate, ≠ seq-1 → Gap.</item>
/// <item>The record key <c>r{record}</c> is written with a revision CAS (create-only for inserts). The value
///   carries <c>AppliedSeq</c> and the pre-image, which makes the write idempotent (AppliedSeq = seq means
///   "already applied by an earlier attempt or a concurrent duplicate") and lets readers hide it until it commits.</item>
/// <item>Upload data/metadata go to the object store under <c>{source}-{seq}</c> (idempotent overwrite).</item>
/// <item><b>Commit point:</b> the cursor CAS seq-1 → seq on the revision read in step 1. Exactly one of any
///   concurrent duplicates wins it; the others re-read the cursor and report Duplicate.</item>
/// </list>
/// Readers (Load, oracle scans) read the cursor first, then the record, and resolve visibility against the
/// cursor (<see cref="KvRecordCodec.Resolve"/>), so a record or upload written by an attempt that has not
/// (yet) advanced the cursor is never observed. A record precondition failure is only reported as
/// RecordConflict if the cursor is still at seq-1 afterwards; otherwise a duplicate already committed.
/// </summary>
public sealed class NatsKvStore : IStateStore
{
    private readonly NatsKvStoreOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly NatsConnectionPool _pool;
    private readonly string _cursorBucket, _recordBucket, _uploadBucket, _blobBucket;
    private Handles[] _handles = [];
    private int _next = -1;

    private sealed record Handles(INatsKVStore Cursors, INatsKVStore Records, INatsObjStore Uploads, INatsObjStore Blobs);

    public string Name => "natskv";

    public NatsKvStore(NatsKvStoreOptions options, Topology topology, Action<string> log)
    {
        _o = options;
        _topology = topology;
        _log = log;
        _pool = new NatsConnectionPool(options.Url, "eventbench-store", options.Connections,
            TimeSpan.FromSeconds(Math.Max(1, options.RequestTimeoutSeconds)));
        var p = options.BucketPrefix;
        (_cursorBucket, _recordBucket, _uploadBucket, _blobBucket) = ($"{p}_cursor", $"{p}_record", $"{p}_upload", $"{p}_blob");
    }

    private bool Memory => string.Equals(_o.Storage, "Memory", StringComparison.OrdinalIgnoreCase);

    private Handles H() => _handles[(int)((uint)Interlocked.Increment(ref _next) % (uint)_handles.Length)];

    private static string CursorKey(int sourceId) => "c" + sourceId.ToString(CultureInfo.InvariantCulture);
    private static string RecordKey(long recordId) => "r" + recordId.ToString(CultureInfo.InvariantCulture);
    private static string UploadName(int sourceId, long seq) => $"{sourceId}-{seq}";
    private static string BlobName(long blobId) => blobId.ToString(CultureInfo.InvariantCulture);

    public async Task SetupAsync(CancellationToken ct)
    {
        await _pool.ConnectAsync();
        var kv0 = new NatsKVContext(_pool.FirstJs);
        var obj0 = new NatsObjContext(_pool.FirstJs);
        foreach (var b in new[] { _cursorBucket, _recordBucket })
        {
            try { await kv0.DeleteStoreAsync(b, ct); }
            catch (NatsJSApiException e) when (e.Error.Code == 404) { }
        }
        foreach (var b in new[] { _uploadBucket, _blobBucket })
        {
            try { await obj0.DeleteObjectStore(b, ct); }
            catch (NatsJSApiException e) when (e.Error.Code == 404) { }
        }

        var kvStorage = Memory ? NatsKVStorageType.Memory : NatsKVStorageType.File;
        var objStorage = Memory ? NatsObjStorageType.Memory : NatsObjStorageType.File;
        await kv0.CreateStoreAsync(new NatsKVConfig(_cursorBucket) { Storage = kvStorage, History = 1 }, ct);
        await kv0.CreateStoreAsync(new NatsKVConfig(_recordBucket) { Storage = kvStorage, History = 1 }, ct);
        await obj0.CreateObjectStoreAsync(new NatsObjConfig(_uploadBucket) { Storage = objStorage }, ct);
        await obj0.CreateObjectStoreAsync(new NatsObjConfig(_blobBucket) { Storage = objStorage }, ct);

        var handles = new Handles[Math.Max(1, _o.Connections)];
        for (int i = 0; i < handles.Length; i++)
        {
            var js = _pool.Js(i);
            var kv = new NatsKVContext(js);
            var obj = new NatsObjContext(js);
            handles[i] = new Handles(
                await kv.GetStoreAsync(_cursorBucket, ct), await kv.GetStoreAsync(_recordBucket, ct),
                await obj.GetObjectStoreAsync(_uploadBucket, ct), await obj.GetObjectStoreAsync(_blobBucket, ct));
        }
        _handles = handles;
        _log($"natskv store: buckets {_cursorBucket}, {_recordBucket} (KV, {kvStorage}), {_uploadBucket}, {_blobBucket} (object store); " +
             $"{_topology.TotalSources:N0} source cursors (absent key = 0)");
    }

    // ---- primitive operations --------------------------------------------------------------------

    private static async ValueTask<(long Cursor, ulong Revision)> GetCursorAsync(Handles h, int sourceId, CancellationToken ct)
    {
        var r = await h.Cursors.TryGetEntryAsync<byte[]>(CursorKey(sourceId), cancellationToken: ct);
        if (r.Success) return (KvRecordCodec.DecodeCursor(r.Value.Value), r.Value.Revision);
        if (IsNotFound(r.Error)) return (0, 0);
        throw r.Error;
    }

    private static async ValueTask<(KvRecordValue? Value, ulong Revision)> GetRecordAsync(Handles h, long recordId, CancellationToken ct)
    {
        var r = await h.Records.TryGetEntryAsync<byte[]>(RecordKey(recordId), cancellationToken: ct);
        if (r.Success) return (KvRecordCodec.Decode(r.Value.Value), r.Value.Revision);
        if (IsNotFound(r.Error)) return (null, 0);
        throw r.Error;
    }

    /// <summary>CAS write: revision 0 = create-only. Returns false when the precondition failed.</summary>
    private static async ValueTask<bool> TryCasAsync(INatsKVStore kv, string key, byte[] value, ulong revision, CancellationToken ct)
    {
        var r = revision == 0
            ? await kv.TryCreateAsync(key, value, cancellationToken: ct)
            : await kv.TryUpdateAsync(key, value, revision, cancellationToken: ct);
        if (r.Success) return true;
        if (r.Error is NatsKVCreateException or NatsKVWrongLastRevisionException) return false;
        throw r.Error;
    }

    private static bool IsNotFound(Exception e) => e is NatsKVKeyNotFoundException or NatsKVKeyDeletedException;

    // ---- IStateStore ---------------------------------------------------------------------------------

    public async ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        if (_o.ChaosFailProbability > 0 && Random.Shared.NextDouble() < _o.ChaosFailProbability)
            throw new IOException("chaos: injected transient load failure");
        var h = H();
        for (int attempt = 0; attempt < _o.MaxCasAttempts; attempt++)
        {
            var (cursor, _) = await GetCursorAsync(h, ev.SourceId, ct);   // cursor first, then record
            var (rec, _) = await GetRecordAsync(h, ev.RecordId, ct);
            switch (KvRecordCodec.Resolve(cursor, ev.SourceId, rec, out var s))
            {
                case KvVisibility.Absent:
                    return new WorkContext { LastSeq = cursor };
                case KvVisibility.Visible:
                    return new WorkContext
                    {
                        LastSeq = cursor, RecordExists = true, Version = s.Version, Hash = s.Hash, Status = s.Status, Context = s.Context,
                    };
            }
        }
        throw new InvalidOperationException($"natskv: no consistent snapshot of source {ev.SourceId} / record {ev.RecordId}");
    }

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        if (req.TransactionalAck != null) throw new InvalidOperationException("natskv does not support transactional acks");
        var ev = req.Event;
        var h = H();
        for (int attempt = 0; attempt < _o.MaxCasAttempts; attempt++)
        {
            var (cursor, cursorRev) = await GetCursorAsync(h, ev.SourceId, ct);
            if (cursor >= ev.Seq) return CommitStatus.Duplicate;
            if (cursor != ev.Seq - 1) return CommitStatus.Gap;

            if (req.Kind != MutationKind.None && !await ApplyRecordAsync(h, req, ct))
            {
                // Precondition failed. If a concurrent duplicate already committed this seq, that is not a conflict.
                var (now, _) = await GetCursorAsync(h, ev.SourceId, ct);
                return now >= ev.Seq ? CommitStatus.Duplicate : CommitStatus.RecordConflict;
            }

            if (req.Upload is { } up) await PutUploadAsync(h, ev, up, ct);

            // Commit point.
            if (await TryCasAsync(h.Cursors, CursorKey(ev.SourceId), KvRecordCodec.EncodeCursor(ev.Seq), cursorRev, ct))
                return CommitStatus.Applied;
            // Lost the race: re-read (normally Duplicate on the next iteration).
        }
        throw new InvalidOperationException($"natskv: commit of {ev} did not converge after {_o.MaxCasAttempts} attempts");
    }

    /// <summary>Idempotent, version-guarded record write. Returns false on a precondition failure.</summary>
    private async ValueTask<bool> ApplyRecordAsync(Handles h, CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        string key = RecordKey(ev.RecordId);
        for (int attempt = 0; attempt < _o.MaxCasAttempts; attempt++)
        {
            var (rec, rev) = await GetRecordAsync(h, ev.RecordId, ct);
            if (rec != null && rec.SourceId == ev.SourceId && rec.AppliedSeq == ev.Seq)
                return true; // this event's mutation is already there (earlier attempt / concurrent duplicate)

            byte[] value;
            if (req.Kind == MutationKind.Insert)
            {
                if (rec != null) return false;
                value = KvRecordCodec.Encode(new KvRecordValue(ev.SourceId, ev.Seq,
                    new KvRecordState(req.NewStatus, req.NewVersion, req.NewHash, req.NewContext), null));
            }
            else
            {
                // AppliedSeq < seq ≤ cursor+1 means Current is committed as of cursor = seq-1.
                if (rec == null || rec.SourceId != ev.SourceId || rec.AppliedSeq > ev.Seq) return false;
                if (rec.Current.Version != req.PrevVersion) return false;
                value = KvRecordCodec.Encode(new KvRecordValue(ev.SourceId, ev.Seq,
                    new KvRecordState(req.NewStatus, req.NewVersion, req.NewHash, req.NewContext ?? rec.Current.Context),
                    rec.Current));
            }
            if (await TryCasAsync(h.Records, key, value, rev, ct)) return true;
        }
        throw new InvalidOperationException($"natskv: record write of {ev} did not converge");
    }

    private static async ValueTask PutUploadAsync(Handles h, EventEnvelope ev, UploadInfo up, CancellationToken ct)
    {
        var meta = new ObjectMetadata
        {
            Name = UploadName(ev.SourceId, ev.Seq),
            Metadata = new Dictionary<string, string>
            {
                ["source"] = ev.SourceId.ToString(CultureInfo.InvariantCulture),
                ["seq"] = ev.Seq.ToString(CultureInfo.InvariantCulture),
                ["record"] = ev.RecordId.ToString(CultureInfo.InvariantCulture),
                ["size"] = up.Size.ToString(CultureInfo.InvariantCulture),
                ["checksum"] = up.Checksum.ToString(CultureInfo.InvariantCulture),
            },
        };
        // Claim-check uploads carry no data here (like the NULL data column in Postgres).
        await using var stream = new MemoryStream(up.Data ?? [], writable: false);
        await h.Uploads.PutAsync(meta, stream, leaveOpen: false, ct);
    }

    public async ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        var meta = new ObjectMetadata
        {
            Name = BlobName(blobId),
            Metadata = new Dictionary<string, string> { ["source"] = sourceId.ToString(CultureInfo.InvariantCulture) },
        };
        await using var stream = new MemoryStream(data.ToArray(), writable: false);
        await H().Blobs.PutAsync(meta, stream, leaveOpen: false, ct);
    }

    public async ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
    {
        try { return await H().Blobs.GetBytesAsync(BlobName(blobId), ct); }
        catch (NatsObjNotFoundException) { return null; }
    }

    // ---- oracle scans ------------------------------------------------------------------------------

    /// <summary>Snapshot of all keys of a KV bucket (watch the last value per key until no more pending).</summary>
    private static async Task<List<(string Key, byte[] Value)>> ReadAllAsync(INatsKVStore kv, CancellationToken ct)
    {
        var list = new List<(string, byte[])>();
        var opts = new NatsKVWatchOpts { IgnoreDeletes = true, OnNoData = _ => ValueTask.FromResult(true) };
        await foreach (var e in kv.WatchAsync<byte[]>(">", opts: opts, cancellationToken: ct))
        {
            if (e.Value != null) list.Add((e.Key, e.Value));
            if (e.Delta == 0) break;
        }
        return list;
    }

    private async Task<long[]> ReadCursorsAsync(CancellationToken ct)
    {
        var cursors = new long[_topology.TotalSources];
        foreach (var (key, value) in await ReadAllAsync(H().Cursors, ct))
        {
            int sid = int.Parse(key.AsSpan(1), CultureInfo.InvariantCulture);
            if ((uint)sid < (uint)cursors.Length) cursors[sid] = KvRecordCodec.DecodeCursor(value);
        }
        return cursors;
    }

    public async IAsyncEnumerable<SourceCursor> ScanCursorsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        // Every source has a cursor; an absent key is cursor 0 (no event committed yet).
        var cursors = await ReadCursorsAsync(ct);
        for (int i = 0; i < cursors.Length; i++) yield return new SourceCursor(i, cursors[i]);
    }

    public async IAsyncEnumerable<RecordRow> ScanRecordsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var cursors = await ReadCursorsAsync(ct);
        foreach (var (key, value) in await ReadAllAsync(H().Records, ct))
        {
            var rec = KvRecordCodec.Decode(value);
            long recordId = long.Parse(key.AsSpan(1), CultureInfo.InvariantCulture);
            long cursor = (uint)rec.SourceId < (uint)cursors.Length ? cursors[rec.SourceId] : long.MaxValue;
            // Only committed state is visible (an uncommitted write shows its pre-image, or nothing for an insert).
            KvRecordState? s = rec.AppliedSeq <= cursor ? rec.Current : rec.Previous;
            if (s is { } st) yield return new RecordRow(rec.SourceId, recordId, st.Status, st.Version, st.Hash);
        }
    }

    public async IAsyncEnumerable<UploadRow> ScanUploadsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var cursors = await ReadCursorsAsync(ct);
        var rows = new List<UploadRow>();
        await foreach (var m in H().Uploads.ListAsync(cancellationToken: ct))
        {
            if (m.Deleted || m.Metadata is not { } md) continue;
            int sid = int.Parse(md["source"], CultureInfo.InvariantCulture);
            long seq = long.Parse(md["seq"], CultureInfo.InvariantCulture);
            if ((uint)sid < (uint)cursors.Length && seq > cursors[sid]) continue; // written by an attempt that never committed
            rows.Add(new UploadRow(sid, seq, int.Parse(md["size"], CultureInfo.InvariantCulture),
                long.Parse(md["checksum"], CultureInfo.InvariantCulture)));
        }
        foreach (var r in rows) yield return r;
    }

    public string Describe()
        => $"natskv @ {_o.Url}: KV {_cursorBucket}/{_recordBucket} + object stores {_uploadBucket}/{_blobBucket} ({_o.Storage}), " +
           $"commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns={_o.Connections}" +
           (_o.ChaosFailProbability > 0 ? $", CHAOS load-fail={_o.ChaosFailProbability}" : "");

    public ValueTask DisposeAsync() => _pool.DisposeAsync();
}

public sealed class NatsKvStoreFactory : IStateStoreFactory
{
    public string Type => "natskv";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
        => new NatsKvStore(section.Get<NatsKvStoreOptions>() ?? new(), context.Topology, context.Log);
}
