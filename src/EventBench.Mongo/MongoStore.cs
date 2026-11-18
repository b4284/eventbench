using System.Diagnostics;
using System.Runtime.CompilerServices;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EventBench.Mongo;

public sealed class MongoStoreOptions
{
    /// <summary>Must point at a replica-set member (transactions need one), e.g. a single-node rs0.</summary>
    public string ConnectionString { get; set; } = "mongodb://127.0.0.1:27017/?directConnection=true";
    public string Database { get; set; } = "eventbench";
    public string CollectionPrefix { get; set; } = "eb";
    /// <summary>
    /// true: separate cursor/record/upload collections, commit = multi-document transaction.
    /// false: records and upload metadata embedded in the source document, commit = one conditional
    /// single-document update (atomic without transactions).
    /// </summary>
    public bool UseTransactions { get; set; } = true;
    /// <summary>Chunk size for upload / claim-check data (kept well below the 16 MB BSON document limit).</summary>
    public int ChunkSizeBytes { get; set; } = 1 << 20;
    /// <summary>Upper bound for retrying one commit (write conflicts, unknown commit results).</summary>
    public int CommitTimeoutSeconds { get; set; } = 60;
    /// <summary>
    /// Journaled (default): w:majority, j:true, readConcern majority — acknowledged writes survive a crash.
    /// BestEffort: w:majority, j:false, readConcern majority — no wait for the journal fsync; a crash can lose
    /// the last acknowledged writes. REQUIRES the replica set config writeConcernMajorityJournalDefault=false
    /// (infra/mongo/journal-default.sh false); otherwise majority writes still wait for the journal.
    /// (w:1 is not used: an acknowledged w:1 commit is not immediately visible to other sessions' local
    /// reads on the primary — they read at the no-overlap timestamp — which shows up as ordering gaps.)
    /// </summary>
    public MongoDurability Durability { get; set; } = MongoDurability.Journaled;
}

public enum MongoDurability { Journaled, BestEffort }

/// <summary>
/// MongoDB state store. Durability (Store:Durability): Journaled = every write w:majority, j:true (forced,
/// whatever the connection string says), reads readConcern majority; BestEffort = w:majority, j:false (with rs writeConcernMajorityJournalDefault=false).
/// Transactions always use readConcern snapshot.
///
/// Transaction mode commit (one transaction, retried on TransientTransactionError):
///   1. updateOne cursors {_id: src, lastSeq: seq-1} → {lastSeq: seq}; matched 0 → read cursor in the same
///      snapshot → Duplicate (lastSeq ≥ seq) or Gap; abort.
///   2. insert record (duplicate key → RecordConflict) or version-guarded updateOne (0 matched → RecordConflict); abort.
///   3. insert upload metadata; commit.
/// Two workers committing the same seq collide on the cursor document (write conflict): one commits
/// (Applied), the other retries, sees lastSeq = seq and returns Duplicate.
///
/// Upload / blob bytes (up to ~16 MB) are written before the transaction as idempotent chunk upserts keyed by
/// (source, seq) / blob id, so redelivered or concurrent duplicates rewrite identical bytes; only the metadata
/// that makes the upload visible to the oracle is written inside the commit.
/// </summary>
public sealed class MongoStore : IStateStore
{
    private readonly MongoStoreOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly MongoClient _client;
    private readonly IMongoDatabase _db;
    private readonly string _curName, _recName, _uplName, _chunkName, _blobName;
    private readonly IMongoCollection<BsonDocument> _cursors, _records, _uploads, _chunks, _blobs;
    private readonly TransactionOptions _txnOptions;
    private readonly TimeSpan _commitTimeout;
    private readonly int _chunkSize;

    internal long TxnRetries, CommitRetries, TornLoads, EmbeddedRetries;

    public string Name => "mongo";
    public IStatsProvider? Stats { get; }

    public MongoStore(MongoStoreOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Store:ConnectionString is required for mongo");
        _o = options;
        _topology = topology;
        _log = log;
        _chunkSize = Math.Clamp(options.ChunkSizeBytes, 64 * 1024, 8 << 20);
        _commitTimeout = TimeSpan.FromSeconds(Math.Max(1, options.CommitTimeoutSeconds));

        bool journaled = options.Durability == MongoDurability.Journaled;
        var wc = journaled ? WriteConcern.WMajority.With(journal: true) : WriteConcern.WMajority.With(journal: false);
        var rc = ReadConcern.Majority;
        var settings = MongoClientSettings.FromConnectionString(options.ConnectionString);
        settings.WriteConcern = wc;
        settings.ReadConcern = rc;
        settings.ReadPreference = ReadPreference.Primary;
        _client = new MongoClient(settings);
        _db = _client.GetDatabase(options.Database, new MongoDatabaseSettings
        {
            WriteConcern = wc, ReadConcern = rc, ReadPreference = ReadPreference.Primary,
        });
        _txnOptions = new TransactionOptions(readConcern: ReadConcern.Snapshot, readPreference: ReadPreference.Primary, writeConcern: wc);

        var p = options.CollectionPrefix;
        (_curName, _recName, _uplName, _chunkName, _blobName) = ($"{p}_cursors", $"{p}_records", $"{p}_uploads", $"{p}_chunks", $"{p}_blobs");
        _cursors = _db.GetCollection<BsonDocument>(_curName);
        _records = _db.GetCollection<BsonDocument>(_recName);
        _uploads = _db.GetCollection<BsonDocument>(_uplName);
        _chunks = _db.GetCollection<BsonDocument>(_chunkName);
        _blobs = _db.GetCollection<BsonDocument>(_blobName);
        Stats = new MongoServerStats(_client, this, log);
    }

    // ---------------------------------------------------------------- setup

    public async Task SetupAsync(CancellationToken ct)
    {
        foreach (var name in new[] { _curName, _recName, _uplName, _chunkName, _blobName })
            await _db.DropCollectionAsync(name, ct);

        var names = _o.UseTransactions
            ? new[] { _curName, _recName, _uplName, _chunkName, _blobName }
            : new[] { _curName, _chunkName, _blobName };
        foreach (var name in names) await _db.CreateCollectionAsync(name, cancellationToken: ct);

        int n = _topology.TotalSources;
        const int batch = 10_000;
        for (int start = 0; start < n; start += batch)
        {
            var docs = new List<BsonDocument>(Math.Min(batch, n - start));
            for (int i = start; i < Math.Min(n, start + batch); i++)
            {
                var d = new BsonDocument { { "_id", i }, { "group", _topology.GroupOf(i).Id }, { "lastSeq", 0L } };
                if (!_o.UseTransactions)
                {
                    d["recs"] = new BsonDocument();
                    d["ups"] = new BsonArray();
                }
                docs.Add(d);
            }
            await _cursors.InsertManyAsync(docs, new InsertManyOptions { IsOrdered = false }, ct);
        }
        _log($"mongo store: collections recreated in '{_o.Database}' ({(_o.UseTransactions ? "transactions" : "embedded")} mode), {n:N0} source cursors seeded");
    }

    // ---------------------------------------------------------------- load

    private static readonly ProjectionDefinition<BsonDocument> LastSeqOnly = new BsonDocument("lastSeq", 1);

    public async ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        if (!_o.UseTransactions) return await LoadEmbeddedAsync(ev, ct);

        var curTask = FindCursorAsync(ev.SourceId, ct);
        var recTask = FindRecordAsync(ev, ct);
        await Task.WhenAll((Task)curTask, recTask);
        long last = (await curTask)["lastSeq"].ToInt64();
        var rec = await recTask;
        if (MongoStoreLogic.IsTornLoad(last, rec?["updatedSeq"].ToInt64()))
        {
            // A commit of this source landed between the two reads. Re-read record first, then cursor:
            // the cursor can then only be newer than the record, never older (which looks like a duplicate).
            Interlocked.Increment(ref TornLoads);
            rec = await FindRecordAsync(ev, ct);
            last = (await FindCursorAsync(ev.SourceId, ct))["lastSeq"].ToInt64();
        }
        return ToContext(last, rec);
    }

    private async Task<BsonDocument> FindCursorAsync(int sourceId, CancellationToken ct)
        => await _cursors.Find(new BsonDocument("_id", sourceId)).Project(LastSeqOnly).FirstOrDefaultAsync(ct)
           ?? throw new InvalidOperationException($"unknown source {sourceId}");

    private Task<BsonDocument?> FindRecordAsync(EventEnvelope ev, CancellationToken ct)
        => _records.Find(new BsonDocument { { "_id", ev.RecordId }, { "src", ev.SourceId } }).FirstOrDefaultAsync(ct)!;

    private static WorkContext ToContext(long last, BsonDocument? rec)
    {
        if (rec is null) return new WorkContext { LastSeq = last };
        return new WorkContext
        {
            LastSeq = last,
            RecordExists = true,
            Version = rec["version"].ToInt32(),
            Hash = rec["hash"].ToInt64(),
            Status = (RecordStatus)rec["status"].ToInt32(),
            Context = rec.TryGetValue("ctx", out var c) && c.IsBsonBinaryData ? c.AsByteArray : null,
        };
    }

    private async ValueTask<WorkContext> LoadEmbeddedAsync(EventEnvelope ev, CancellationToken ct)
    {
        string key = ev.RecordId.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var doc = await _cursors.Find(new BsonDocument("_id", ev.SourceId))
                      .Project(new BsonDocument { { "lastSeq", 1 }, { $"recs.{key}", 1 } })
                      .FirstOrDefaultAsync(ct)
                  ?? throw new InvalidOperationException($"unknown source {ev.SourceId}");
        BsonDocument? rec = doc.TryGetValue("recs", out var recs) && recs.AsBsonDocument.TryGetValue(key, out var r)
            ? r.AsBsonDocument : null;
        return ToContext(doc["lastSeq"].ToInt64(), rec);
    }

    // ---------------------------------------------------------------- commit

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        if (req.TransactionalAck != null)
            throw new InvalidOperationException("mongo store cannot ack a broker delivery transactionally");
        var ev = req.Event;
        // Bulk bytes go first, idempotently, outside the transaction (see class remarks).
        if (req.Upload is { Data: { } data })
            await WriteChunksAsync(MongoStoreLogic.UploadOwner(ev.SourceId, ev.Seq), data, ct);
        return _o.UseTransactions ? await CommitTxnAsync(req, ct) : await CommitEmbeddedAsync(req, ct);
    }

    private sealed class RecordConflictSignal : Exception;

    private async Task<CommitStatus> CommitTxnAsync(CommitRequest req, CancellationToken ct)
    {
        var deadline = Stopwatch.GetTimestamp() + (long)(_commitTimeout.TotalSeconds * Stopwatch.Frequency);
        using var session = await _client.StartSessionAsync(new ClientSessionOptions { CausalConsistency = false }, ct);
        for (int attempt = 0; ; attempt++)
        {
            session.StartTransaction(_txnOptions);
            try
            {
                var status = await TxnBodyAsync(session, req, ct);
                if (status == CommitStatus.Applied) await CommitWithRetryAsync(session, deadline, ct);
                else await SafeAbortAsync(session);
                return status;
            }
            catch (RecordConflictSignal)
            {
                await SafeAbortAsync(session);
                return CommitStatus.RecordConflict;
            }
            catch (MongoException ex) when (ex.HasErrorLabel("TransientTransactionError") && Stopwatch.GetTimestamp() < deadline)
            {
                // Write conflict (typically a concurrent duplicate commit of the same source) or a transient
                // network error: the transaction did not commit — start over with a fresh snapshot.
                await SafeAbortAsync(session);
                Interlocked.Increment(ref TxnRetries);
                await Task.Delay(MongoStoreLogic.BackoffMs(attempt, Random.Shared), ct);
            }
            catch
            {
                await SafeAbortAsync(session);
                throw;
            }
        }
    }

    private async Task<CommitStatus> TxnBodyAsync(IClientSessionHandle s, CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        var now = DateTime.UtcNow;

        var cur = await _cursors.UpdateOneAsync(s,
            new BsonDocument { { "_id", ev.SourceId }, { "lastSeq", ev.Seq - 1 } },
            new BsonDocument("$set", new BsonDocument { { "lastSeq", ev.Seq }, { "updatedAt", now } }),
            cancellationToken: ct);
        if (cur.MatchedCount == 0)
        {
            var doc = await _cursors.Find(s, new BsonDocument("_id", ev.SourceId)).Project(LastSeqOnly).FirstOrDefaultAsync(ct)
                      ?? throw new InvalidOperationException($"unknown source {ev.SourceId}");
            return MongoStoreLogic.ClassifyCursorMiss(doc["lastSeq"].ToInt64(), ev.Seq)
                   ?? throw new InvalidOperationException($"source {ev.SourceId}: cursor unmatched but at seq-1 inside one snapshot");
        }

        switch (req.Kind)
        {
            case MutationKind.Insert:
            {
                var rec = new BsonDocument
                {
                    { "_id", ev.RecordId }, { "src", ev.SourceId }, { "status", (int)req.NewStatus },
                    { "version", req.NewVersion }, { "hash", req.NewHash },
                    { "createdSeq", ev.Seq }, { "updatedSeq", ev.Seq }, { "updatedAt", now },
                };
                if (req.NewStatus == RecordStatus.Completed) rec["completedSeq"] = ev.Seq;
                if (req.NewContext != null) rec["ctx"] = new BsonBinaryData(req.NewContext);
                try
                {
                    await _records.InsertOneAsync(s, rec, cancellationToken: ct);
                }
                catch (MongoWriteException wx) when (wx.WriteError?.Category == ServerErrorCategory.DuplicateKey)
                {
                    throw new RecordConflictSignal();
                }
                break;
            }
            case MutationKind.Update:
            {
                var set = new BsonDocument
                {
                    { "version", req.NewVersion }, { "hash", req.NewHash }, { "status", (int)req.NewStatus },
                    { "updatedSeq", ev.Seq }, { "updatedAt", now },
                };
                if (req.NewStatus == RecordStatus.Completed) set["completedSeq"] = ev.Seq;
                if (req.NewContext != null) set["ctx"] = new BsonBinaryData(req.NewContext);
                var r = await _records.UpdateOneAsync(s,
                    new BsonDocument { { "_id", ev.RecordId }, { "src", ev.SourceId }, { "version", req.PrevVersion } },
                    new BsonDocument("$set", set), cancellationToken: ct);
                if (r.MatchedCount == 0) throw new RecordConflictSignal();
                break;
            }
        }

        if (req.Upload is { } up)
            await _uploads.InsertOneAsync(s, UploadMeta(ev, up, includeId: true), cancellationToken: ct);
        return CommitStatus.Applied;
    }

    private BsonDocument UploadMeta(EventEnvelope ev, UploadInfo up, bool includeId)
    {
        var d = new BsonDocument();
        if (includeId) d["_id"] = MongoStoreLogic.UploadOwner(ev.SourceId, ev.Seq);
        d["src"] = ev.SourceId;
        d["seq"] = ev.Seq;
        d["rec"] = ev.RecordId;
        d["size"] = up.Size;
        d["checksum"] = up.Checksum;
        d["chunks"] = up.Data is null ? 0 : MongoStoreLogic.ChunkCount(up.Data.Length, _chunkSize);
        return d;
    }

    private async Task CommitWithRetryAsync(IClientSessionHandle session, long deadline, CancellationToken ct)
    {
        while (true)
        {
            try
            {
                await session.CommitTransactionAsync(ct);
                return;
            }
            catch (MongoException ex) when (ex.HasErrorLabel("UnknownTransactionCommitResult") && Stopwatch.GetTimestamp() < deadline)
            {
                // Commit outcome unknown (e.g. majority wait timed out / network): committing again is idempotent.
                Interlocked.Increment(ref CommitRetries);
                await Task.Delay(5, ct);
            }
        }
    }

    private static async Task SafeAbortAsync(IClientSessionHandle session)
    {
        if (!session.IsInTransaction) return;
        try { await session.AbortTransactionAsync(CancellationToken.None); }
        catch { /* best effort: the server aborts it anyway (error or transaction timeout) */ }
    }

    private async Task<CommitStatus> CommitEmbeddedAsync(CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        string key = ev.RecordId.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string recPath = $"recs.{key}";
        var now = DateTime.UtcNow;

        var filter = new BsonDocument { { "_id", ev.SourceId }, { "lastSeq", ev.Seq - 1 } };
        var set = new BsonDocument { { "lastSeq", ev.Seq }, { "updatedAt", now } };
        switch (req.Kind)
        {
            case MutationKind.Insert:
            {
                filter[recPath] = new BsonDocument("$exists", false);
                var rec = new BsonDocument
                {
                    { "status", (int)req.NewStatus }, { "version", req.NewVersion }, { "hash", req.NewHash },
                    { "createdSeq", ev.Seq }, { "updatedSeq", ev.Seq },
                };
                if (req.NewStatus == RecordStatus.Completed) rec["completedSeq"] = ev.Seq;
                if (req.NewContext != null) rec["ctx"] = new BsonBinaryData(req.NewContext);
                set[recPath] = rec;
                break;
            }
            case MutationKind.Update:
                filter[$"{recPath}.version"] = req.PrevVersion;
                set[$"{recPath}.version"] = req.NewVersion;
                set[$"{recPath}.hash"] = req.NewHash;
                set[$"{recPath}.status"] = (int)req.NewStatus;
                set[$"{recPath}.updatedSeq"] = ev.Seq;
                if (req.NewStatus == RecordStatus.Completed) set[$"{recPath}.completedSeq"] = ev.Seq;
                if (req.NewContext != null) set[$"{recPath}.ctx"] = new BsonBinaryData(req.NewContext);
                break;
        }
        var update = new BsonDocument("$set", set);
        if (req.Upload is { } up) update["$push"] = new BsonDocument("ups", UploadMeta(ev, up, includeId: false));

        var deadline = Stopwatch.GetTimestamp() + (long)(_commitTimeout.TotalSeconds * Stopwatch.Frequency);
        for (int attempt = 0; ; attempt++)
        {
            var r = await _cursors.UpdateOneAsync(filter, update, cancellationToken: ct);
            if (r.MatchedCount == 1) return CommitStatus.Applied;

            var doc = await _cursors.Find(new BsonDocument("_id", ev.SourceId))
                          .Project(new BsonDocument { { "lastSeq", 1 }, { $"{recPath}.version", 1 } })
                          .FirstOrDefaultAsync(ct)
                      ?? throw new InvalidOperationException($"unknown source {ev.SourceId}");
            bool exists = doc.TryGetValue("recs", out var recs) && recs.AsBsonDocument.Contains(key);
            int version = exists && recs.AsBsonDocument[key].AsBsonDocument.TryGetValue("version", out var v) ? v.ToInt32() : 0;
            var status = MongoStoreLogic.ClassifyEmbeddedMiss(doc["lastSeq"].ToInt64(), ev.Seq, req.Kind, exists, version, req.PrevVersion);
            if (status is { } st) return st;
            // The majority snapshot read lags the write that made the update miss: re-evaluate.
            if (Stopwatch.GetTimestamp() >= deadline)
                throw new TimeoutException($"source {ev.SourceId} seq {ev.Seq}: embedded commit could not be classified");
            Interlocked.Increment(ref EmbeddedRetries);
            await Task.Delay(MongoStoreLogic.BackoffMs(attempt, Random.Shared), ct);
        }
    }

    // ---------------------------------------------------------------- chunks / blobs

    private async Task WriteChunksAsync(string owner, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        var models = new List<WriteModel<BsonDocument>>();
        int n = 0;
        foreach (var seg in MongoStoreLogic.Split(data, _chunkSize))
        {
            string id = MongoStoreLogic.ChunkId(owner, n);
            models.Add(new ReplaceOneModel<BsonDocument>(new BsonDocument("_id", id),
                new BsonDocument { { "_id", id }, { "f", owner }, { "n", n }, { "d", new BsonBinaryData(seg.ToArray()) } })
            {
                IsUpsert = true,
            });
            n++;
        }
        if (models.Count > 0)
            await _chunks.BulkWriteAsync(models, new BulkWriteOptions { IsOrdered = false }, ct);
    }

    private async Task<byte[]?> ReadChunksAsync(string owner, int count, int size, CancellationToken ct)
    {
        if (count == 0) return size == 0 ? [] : null;
        var ids = new BsonArray(Enumerable.Range(0, count).Select(i => MongoStoreLogic.ChunkId(owner, i)));
        var docs = await _chunks.Find(new BsonDocument("_id", new BsonDocument("$in", ids))).ToListAsync(ct);
        return MongoStoreLogic.Join(docs.Select(d => (d["n"].ToInt32(), d["d"].AsByteArray)).ToList(), count, size);
    }

    public async ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        string owner = MongoStoreLogic.BlobOwner(blobId);
        await WriteChunksAsync(owner, data, ct);
        // The metadata document is written last: a blob is only visible once all its chunks are durable.
        var meta = new BsonDocument
        {
            { "_id", blobId }, { "src", sourceId }, { "size", data.Length },
            { "chunks", MongoStoreLogic.ChunkCount(data.Length, _chunkSize) },
        };
        await _blobs.ReplaceOneAsync(new BsonDocument("_id", blobId), meta, new ReplaceOptions { IsUpsert = true }, ct);
    }

    public async ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
    {
        var meta = await _blobs.Find(new BsonDocument("_id", blobId)).FirstOrDefaultAsync(ct);
        if (meta is null) return null;
        return await ReadChunksAsync(MongoStoreLogic.BlobOwner(blobId), meta["chunks"].ToInt32(), meta["size"].ToInt32(), ct);
    }

    // ---------------------------------------------------------------- oracle scans

    private static async IAsyncEnumerable<BsonDocument> Stream(Task<IAsyncCursor<BsonDocument>> open,
        [EnumeratorCancellation] CancellationToken ct)
    {
        using var cursor = await open;
        while (await cursor.MoveNextAsync(ct))
            foreach (var d in cursor.Current)
                yield return d;
    }

    private Task<IAsyncCursor<BsonDocument>> FindAll(IMongoCollection<BsonDocument> c, BsonDocument projection, CancellationToken ct)
        => c.FindAsync(FilterDefinition<BsonDocument>.Empty,
            new FindOptions<BsonDocument, BsonDocument> { Projection = projection, BatchSize = 10_000 }, ct);

    private Task<IAsyncCursor<BsonDocument>> Aggregate(BsonDocument[] stages, CancellationToken ct)
        => _cursors.AggregateAsync<BsonDocument>(stages, new AggregateOptions { BatchSize = 10_000, AllowDiskUse = true }, ct);

    public async IAsyncEnumerable<SourceCursor> ScanCursorsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var d in Stream(FindAll(_cursors, new BsonDocument("lastSeq", 1), ct), ct))
            yield return new SourceCursor(d["_id"].ToInt32(), d["lastSeq"].ToInt64());
    }

    public async IAsyncEnumerable<RecordRow> ScanRecordsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        if (_o.UseTransactions)
        {
            var proj = new BsonDocument { { "src", 1 }, { "status", 1 }, { "version", 1 }, { "hash", 1 } };
            await foreach (var d in Stream(FindAll(_records, proj, ct), ct))
                yield return new RecordRow(d["src"].ToInt32(), d["_id"].ToInt64(), (RecordStatus)d["status"].ToInt32(),
                    d["version"].ToInt32(), d["hash"].ToInt64());
            yield break;
        }
        BsonDocument[] stages =
        [
            new("$project", new BsonDocument("r", new BsonDocument("$objectToArray", "$recs"))),
            new("$unwind", "$r"),
            new("$project", new BsonDocument
            {
                { "_id", 0 }, { "src", "$_id" }, { "rid", "$r.k" },
                { "status", "$r.v.status" }, { "version", "$r.v.version" }, { "hash", "$r.v.hash" },
            }),
        ];
        await foreach (var d in Stream(Aggregate(stages, ct), ct))
            yield return new RecordRow(d["src"].ToInt32(), long.Parse(d["rid"].AsString, System.Globalization.CultureInfo.InvariantCulture),
                (RecordStatus)d["status"].ToInt32(), d["version"].ToInt32(), d["hash"].ToInt64());
    }

    public async IAsyncEnumerable<UploadRow> ScanUploadsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        IAsyncEnumerable<BsonDocument> rows = _o.UseTransactions
            ? Stream(FindAll(_uploads, new BsonDocument { { "src", 1 }, { "seq", 1 }, { "size", 1 }, { "checksum", 1 } }, ct), ct)
            : Stream(Aggregate(
            [
                new("$match", new BsonDocument("ups.0", new BsonDocument("$exists", true))),
                new("$project", new BsonDocument("ups", 1)),
                new("$unwind", "$ups"),
                new("$replaceRoot", new BsonDocument("newRoot", "$ups")),
            ], ct), ct);
        await foreach (var d in rows)
            yield return new UploadRow(d["src"].ToInt32(), d["seq"].ToInt64(), d["size"].ToInt32(), d["checksum"].ToInt64());
    }

    public string Describe()
    {
        var url = MongoUrl.Create(_o.ConnectionString);
        string hosts = string.Join(",", url.Servers.Select(s => $"{s.Host}:{s.Port}"));
        string mode = _o.UseTransactions ? "transactions (readConcern snapshot)" : "embedded (single-document commit)";
        return $"mongo @ {hosts}/{_o.Database} prefix={_o.CollectionPrefix}, mode={mode}, " +
               (_o.Durability == MongoDurability.Journaled ? "w=majority j=true, load readConcern majority" : "BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority") + $", chunk={_chunkSize / 1024}KiB, pool max {url.MaxConnectionPoolSize}";
    }

    public ValueTask DisposeAsync()
    {
        _client.Dispose();
        return ValueTask.CompletedTask;
    }
}

public sealed class MongoStoreFactory : IStateStoreFactory
{
    public string Type => "mongo";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
        => new MongoStore(section.Get<MongoStoreOptions>() ?? new(), context.Topology, context.Log);
}
