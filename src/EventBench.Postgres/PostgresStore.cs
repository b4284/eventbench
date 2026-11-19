using System.Runtime.CompilerServices;
using System.Threading.Channels;
using EventBench.Core.Util;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace EventBench.Postgres;

public sealed class PostgresStoreOptions
{
    /// <summary>Data path connection string (typically through PgBouncer).</summary>
    public string ConnectionString { get; set; } = "";
    /// <summary>Direct connection for DDL, oracle scans and server stats (defaults to ConnectionString).</summary>
    public string? AdminConnectionString { get; set; }
    /// <summary>
    /// Optional shell command printing PgBouncer's SHOW POOLS as CSV (the admin console needs the simple
    /// query protocol, which Npgsql does not speak), e.g. podman exec eb-postgres psql "...dbname=pgbouncer" --csv -c "SHOW POOLS".
    /// </summary>
    public string? PgBouncerStatsCommand { get; set; }
    public string TablePrefix { get; set; } = "eb";
    public bool UnloggedTables { get; set; } = false;
    /// <summary>Fillfactor for the hot cursor/record tables (lower = more HOT updates).</summary>
    public int FillFactor { get; set; } = 70;
    /// <summary>
    /// Group commit: commits from all workers are queued and executed many per transaction
    /// (eb_commit_many, one round trip), with their in-transaction pgmq deletes. Events with an upload
    /// still commit alone. Batches form naturally while the previous ones execute.
    /// </summary>
    public bool BatchCommits { get; set; } = false;
    public int BatchMaxSize { get; set; } = 64;
    /// <summary>Concurrent batch executors (connections busy with batches).</summary>
    public int BatchExecutors { get; set; } = 8;
    /// <summary>pgqueue table deleted from inside the commit (AckMode=InTransaction with the pgqueue broker).</summary>
    public string PgQueueTable { get; set; } = "ebq_msg";
}

/// <summary>
/// Postgres state store. Load = one SELECT (cursor + record + context). Commit = one round trip to a
/// plpgsql function that checks the cursor under a row lock, applies the version-guarded mutation,
/// stores uploads, advances the cursor and — in AckMode=InTransaction — deletes the pgmq message in
/// the same transaction.
/// </summary>
public sealed class PostgresStore : IStateStore
{
    private readonly PostgresStoreOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly NpgsqlDataSource _data;
    private readonly NpgsqlDataSource _admin;
    private readonly string _src, _rec, _upl, _blob, _commitFn;
    private readonly string _loadSql, _commitSql;

    private readonly Channel<(CommitRequest Req, TaskCompletionSource<CommitStatus> Tcs)>? _batchQueue;
    private readonly List<Task> _executors = new();
    private readonly CancellationTokenSource _cts = new();
    private long _batches, _batchedCommits, _batchFallbacks, _preloadHits;

    public string Name => "postgres";
    public IStatsProvider? Stats { get; }

    public PostgresStore(PostgresStoreOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Store:ConnectionString is required for postgres");
        _o = options;
        _topology = topology;
        _log = log;
        _data = NpgsqlDataSource.Create(options.ConnectionString);
        _admin = NpgsqlDataSource.Create(options.AdminConnectionString ?? options.ConnectionString);
        var p = options.TablePrefix;
        (_src, _rec, _upl, _blob, _commitFn) = ($"{p}_source", $"{p}_record", $"{p}_upload", $"{p}_blob", $"{p}_commit");
        _loadSql = $"SELECT s.last_seq, r.version, r.hash, r.status, r.ctx FROM {_src} s " +
                   $"LEFT JOIN {_rec} r ON r.record_id = $2 AND r.source_id = s.source_id WHERE s.source_id = $1";
        _commitSql = $"SELECT {_commitFn}($1,$2,$3,$4,$5,$6,$7,$8,$9,$10,$11,$12,$13,$14,$15)";
        Stats = new PostgresServerStats(_admin, options.PgBouncerStatsCommand, log);
        if (options.BatchCommits)
        {
            _batchQueue = Channel.CreateUnbounded<(CommitRequest, TaskCompletionSource<CommitStatus>)>(new UnboundedChannelOptions { SingleReader = false });
            for (int i = 0; i < Math.Max(1, options.BatchExecutors); i++) _executors.Add(Task.Run(() => BatchLoopAsync(_cts.Token)));
        }
    }

    public long Batches => Interlocked.Read(ref _batches);
    public long BatchedCommits => Interlocked.Read(ref _batchedCommits);

    public bool SupportsTransactionalAck(IBroker broker)
    {
        string? db = broker switch
        {
            PgmqBroker p => p.DatabaseName,
            PgDispatchBroker d => d.DatabaseName,
            PgQueueBroker q when q.Table == _o.PgQueueTable => q.DatabaseName,
            _ => null,
        };
        if (db is null) return false;
        var mine = new NpgsqlConnectionStringBuilder(_o.ConnectionString);
        if (!string.Equals(mine.Database, db, StringComparison.Ordinal))
        {
            _log($"transactional ack unavailable: store database '{mine.Database}' != broker database '{db}'");
            return false;
        }
        return true;
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        string unlogged = _o.UnloggedTables ? "UNLOGGED" : "";
        int ff = Math.Clamp(_o.FillFactor, 10, 100);
        string ddl = $"""
            DROP TABLE IF EXISTS {_upl}, {_blob}, {_rec}, {_src} CASCADE;
            CREATE {unlogged} TABLE {_src} (
                source_id  int PRIMARY KEY,
                group_id   int NOT NULL,
                last_seq   bigint NOT NULL DEFAULT 0,
                updated_at timestamptz
            ) WITH (fillfactor = {ff});
            CREATE {unlogged} TABLE {_rec} (
                record_id     bigint PRIMARY KEY,
                source_id     int NOT NULL,
                status        smallint NOT NULL,
                version       int NOT NULL,
                hash          bigint NOT NULL,
                created_seq   bigint NOT NULL,
                updated_seq   bigint NOT NULL,
                completed_seq bigint,
                ctx           bytea,
                updated_at    timestamptz NOT NULL DEFAULT now()
            ) WITH (fillfactor = {ff});
            CREATE {unlogged} TABLE {_upl} (
                source_id int NOT NULL,
                seq       bigint NOT NULL,
                record_id bigint NOT NULL,
                size      int NOT NULL,
                checksum  bigint NOT NULL,
                data      bytea,
                PRIMARY KEY (source_id, seq)
            );
            ALTER TABLE {_upl} ALTER COLUMN data SET STORAGE EXTERNAL;
            CREATE {unlogged} TABLE {_blob} (
                blob_id   bigint PRIMARY KEY,
                source_id int NOT NULL,
                size      int NOT NULL,
                data      bytea NOT NULL
            );
            ALTER TABLE {_blob} ALTER COLUMN data SET STORAGE EXTERNAL;

            -- Returns 0 applied, 1 duplicate, 2 gap, 3 record conflict.
            CREATE OR REPLACE FUNCTION {_commitFn}(
                p_source int, p_seq bigint, p_kind smallint, p_record bigint, p_prev_version int,
                p_version int, p_hash bigint, p_status smallint, p_ctx bytea,
                p_upload_size int, p_upload_checksum bigint, p_upload_data bytea,
                p_ack_queue text, p_ack_msg bigint, p_ack_archive boolean)
            RETURNS smallint LANGUAGE plpgsql AS $fn$
            DECLARE
                v_last bigint;
                n int;
            BEGIN
                SELECT last_seq INTO v_last FROM {_src} WHERE source_id = p_source FOR UPDATE;
                IF NOT FOUND THEN RAISE EXCEPTION 'unknown source %', p_source; END IF;

                IF v_last >= p_seq THEN
                    IF p_ack_queue IS NOT NULL THEN
                        IF p_ack_archive THEN PERFORM pgmq.archive(p_ack_queue, p_ack_msg);
                        ELSE PERFORM pgmq.delete(p_ack_queue, p_ack_msg); END IF;
                    END IF;
                    RETURN 1;
                END IF;
                IF v_last <> p_seq - 1 THEN RETURN 2; END IF;

                IF p_kind = 1 THEN
                    INSERT INTO {_rec} (record_id, source_id, status, version, hash, created_seq, updated_seq, ctx)
                    VALUES (p_record, p_source, p_status, p_version, p_hash, p_seq, p_seq, p_ctx)
                    ON CONFLICT (record_id) DO NOTHING;
                    GET DIAGNOSTICS n = ROW_COUNT;
                    IF n = 0 THEN RETURN 3; END IF;
                ELSIF p_kind = 2 THEN
                    UPDATE {_rec}
                       SET version = p_version, hash = p_hash, status = p_status,
                           ctx = COALESCE(p_ctx, ctx), updated_seq = p_seq, updated_at = now(),
                           completed_seq = CASE WHEN p_status = 2 THEN p_seq ELSE completed_seq END
                     WHERE record_id = p_record AND source_id = p_source AND version = p_prev_version;
                    GET DIAGNOSTICS n = ROW_COUNT;
                    IF n = 0 THEN RETURN 3; END IF;
                END IF;

                IF p_upload_size IS NOT NULL THEN
                    INSERT INTO {_upl} (source_id, seq, record_id, size, checksum, data)
                    VALUES (p_source, p_seq, p_record, p_upload_size, p_upload_checksum, p_upload_data);
                END IF;

                UPDATE {_src} SET last_seq = p_seq, updated_at = now() WHERE source_id = p_source;

                IF p_ack_queue IS NOT NULL THEN
                    IF p_ack_archive THEN PERFORM pgmq.archive(p_ack_queue, p_ack_msg);
                    ELSE PERFORM pgmq.delete(p_ack_queue, p_ack_msg); END IF;
                END IF;
                RETURN 0;
            END
            $fn$;

            -- Group commit: many events in one transaction; statuses in input order.
            CREATE OR REPLACE FUNCTION {_commitFn}_many(
                p_source int[], p_seq bigint[], p_kind smallint[], p_record bigint[], p_prev_version int[],
                p_version int[], p_hash bigint[], p_status smallint[], p_ctx bytea[],
                p_ack_queue text[], p_ack_msg bigint[], p_q_group int[], p_q_msg bigint[])
            RETURNS smallint[] LANGUAGE plpgsql AS $fn$
            DECLARE
                r smallint[] := ARRAY[]::smallint[];
                rc smallint;
                i int;
            BEGIN
                FOR i IN 1 .. coalesce(array_length(p_source, 1), 0) LOOP
                    rc := {_commitFn}(p_source[i], p_seq[i], p_kind[i], p_record[i], p_prev_version[i],
                                      p_version[i], p_hash[i], p_status[i], p_ctx[i], NULL, NULL, NULL,
                                      p_ack_queue[i], p_ack_msg[i], false);
                    -- pgqueue: delete the message in the same transaction (applied or duplicate).
                    IF p_q_msg[i] IS NOT NULL AND rc IN (0, 1) THEN
                        DELETE FROM {_o.PgQueueTable} WHERE group_id = p_q_group[i] AND msg_id = p_q_msg[i];
                    END IF;
                    r := r || rc;
                END LOOP;
                RETURN r;
            END
            $fn$;
            """;

        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using (var cmd = new NpgsqlCommand(ddl, conn)) await cmd.ExecuteNonQueryAsync(ct);

        var ids = new int[_topology.TotalSources];
        var groups = new int[_topology.TotalSources];
        for (int i = 0; i < ids.Length; i++) { ids[i] = i; groups[i] = _topology.GroupOf(i).Id; }
        await using (var cmd = new NpgsqlCommand(
            $"INSERT INTO {_src} (source_id, group_id) SELECT * FROM unnest($1::int[], $2::int[])", conn))
        {
            cmd.Parameters.AddWithValue(ids);
            cmd.Parameters.AddWithValue(groups);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        await using (var cmd = new NpgsqlCommand($"ANALYZE {_src}", conn)) await cmd.ExecuteNonQueryAsync(ct);
        _log($"postgres store: schema created, {ids.Length:N0} source cursors seeded");
    }

    public async ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        // Context loaded by a dispatcher together with the message (pgdispatch PreloadContext).
        if (PreloadedContexts.TryTake(ev.SourceId, ev.Seq, out var pre))
        {
            Interlocked.Increment(ref _preloadHits);
            return pre;
        }
        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(_loadSql, conn);
        cmd.Parameters.Add(new NpgsqlParameter { Value = ev.SourceId, NpgsqlDbType = NpgsqlDbType.Integer });
        cmd.Parameters.Add(new NpgsqlParameter { Value = ev.RecordId, NpgsqlDbType = NpgsqlDbType.Bigint });
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        if (!await rd.ReadAsync(ct)) throw new InvalidOperationException($"unknown source {ev.SourceId}");
        if (rd.IsDBNull(1)) return new WorkContext { LastSeq = rd.GetInt64(0) };
        return new WorkContext
        {
            LastSeq = rd.GetInt64(0),
            RecordExists = true,
            Version = rd.GetInt32(1),
            Hash = rd.GetInt64(2),
            Status = (RecordStatus)rd.GetInt16(3),
            Context = rd.IsDBNull(4) ? null : rd.GetFieldValue<byte[]>(4),
        };
    }

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        if (_batchQueue != null && req.Upload is null && (req.TransactionalAck is null or IPgmqDelivery { Archive: false } or IPgQueueDelivery))
        {
            var tcs = new TaskCompletionSource<CommitStatus>(TaskCreationOptions.RunContinuationsAsynchronously);
            _batchQueue.Writer.TryWrite((req, tcs));
            return await tcs.Task.WaitAsync(ct);
        }
        return await CommitOneAsync(req, ct);
    }

    private async Task BatchLoopAsync(CancellationToken ct)
    {
        var reader = _batchQueue!.Reader;
        var items = new List<(CommitRequest Req, TaskCompletionSource<CommitStatus> Tcs)>(_o.BatchMaxSize);
        while (!ct.IsCancellationRequested)
        {
            items.Clear();
            try
            {
                if (!await reader.WaitToReadAsync(ct)) return;
            }
            catch (OperationCanceledException) { return; }
            while (items.Count < _o.BatchMaxSize && reader.TryRead(out var it)) items.Add(it);
            if (items.Count == 0) continue;
            try
            {
                var codes = await CommitManyAsync(items.Select(x => x.Req).ToList(), ct);
                Interlocked.Increment(ref _batches);
                Interlocked.Add(ref _batchedCommits, items.Count);
                for (int i = 0; i < items.Count; i++) items[i].Tcs.TrySetResult(ToStatus(codes[i]));
            }
            catch (Exception e) when (e is not OperationCanceledException || !ct.IsCancellationRequested)
            {
                // Isolate: one bad event must not fail its batchmates.
                Interlocked.Increment(ref _batchFallbacks);
                foreach (var (req, tcs) in items)
                {
                    try { tcs.TrySetResult(await CommitOneAsync(req, ct)); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }
            }
        }
    }

    private async Task<short[]> CommitManyAsync(List<CommitRequest> reqs, CancellationToken ct)
    {
        // Lock rows in source order (deadlock-free across concurrent batches); map results back.
        var order = Enumerable.Range(0, reqs.Count).OrderBy(i => reqs[i].Event.SourceId).ToArray();
        int n = reqs.Count;
        var src = new int[n]; var seq = new long[n]; var kind = new short[n]; var rec = new long[n];
        var prev = new int[n]; var ver = new int[n]; var hash = new long[n]; var st = new short[n];
        var ctx = new byte[]?[n]; var aq = new string?[n]; var am = new long?[n]; var qg = new int?[n]; var qm = new long?[n];
        for (int j = 0; j < n; j++)
        {
            var r = reqs[order[j]];
            var ack = r.TransactionalAck as IPgmqDelivery;
            src[j] = r.Event.SourceId; seq[j] = r.Event.Seq; kind[j] = (short)r.Kind; rec[j] = r.Event.RecordId;
            prev[j] = r.PrevVersion; ver[j] = r.NewVersion; hash[j] = r.NewHash; st[j] = (short)r.NewStatus;
            ctx[j] = r.NewContext; aq[j] = ack?.Queue; am[j] = ack?.MsgId;
            if (r.TransactionalAck is IPgQueueDelivery qd) { qg[j] = qd.GroupId; qm[j] = qd.MsgId; }
        }
        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT {_commitFn}_many($1,$2,$3,$4,$5,$6,$7,$8,$9,$10,$11,$12,$13)", conn);
        var ps = cmd.Parameters;
        ps.Add(P(src, NpgsqlDbType.Array | NpgsqlDbType.Integer));
        ps.Add(P(seq, NpgsqlDbType.Array | NpgsqlDbType.Bigint));
        ps.Add(P(kind, NpgsqlDbType.Array | NpgsqlDbType.Smallint));
        ps.Add(P(rec, NpgsqlDbType.Array | NpgsqlDbType.Bigint));
        ps.Add(P(prev, NpgsqlDbType.Array | NpgsqlDbType.Integer));
        ps.Add(P(ver, NpgsqlDbType.Array | NpgsqlDbType.Integer));
        ps.Add(P(hash, NpgsqlDbType.Array | NpgsqlDbType.Bigint));
        ps.Add(P(st, NpgsqlDbType.Array | NpgsqlDbType.Smallint));
        ps.Add(P(ctx, NpgsqlDbType.Array | NpgsqlDbType.Bytea));
        ps.Add(P(aq, NpgsqlDbType.Array | NpgsqlDbType.Text));
        ps.Add(P(am, NpgsqlDbType.Array | NpgsqlDbType.Bigint));
        ps.Add(P(qg, NpgsqlDbType.Array | NpgsqlDbType.Integer));
        ps.Add(P(qm, NpgsqlDbType.Array | NpgsqlDbType.Bigint));
        var sorted = (short[])(await cmd.ExecuteScalarAsync(ct))!;
        var codes = new short[n];
        for (int j = 0; j < n; j++) codes[order[j]] = sorted[j];
        return codes;
    }

    private static CommitStatus ToStatus(short code) => code switch
    {
        0 => CommitStatus.Applied,
        1 => CommitStatus.Duplicate,
        2 => CommitStatus.Gap,
        _ => CommitStatus.RecordConflict,
    };

    private async Task<CommitStatus> CommitOneAsync(CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        var ack = req.TransactionalAck as IPgmqDelivery;
        var qack = req.TransactionalAck as IPgQueueDelivery;
        if (req.TransactionalAck != null && ack is null && qack is null)
            throw new InvalidOperationException("transactional ack requires a pgmq or pgqueue delivery");

        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(qack is null ? _commitSql :
            $"WITH c AS ({_commitSql} AS rc), d AS (DELETE FROM {_o.PgQueueTable} WHERE group_id = $16 AND msg_id = $17 " +
            "AND (SELECT rc FROM c) IN (0, 1)) SELECT rc FROM c", conn);
        var ps = cmd.Parameters;
        ps.Add(P(ev.SourceId, NpgsqlDbType.Integer));
        ps.Add(P(ev.Seq, NpgsqlDbType.Bigint));
        ps.Add(P((short)req.Kind, NpgsqlDbType.Smallint));
        ps.Add(P(ev.RecordId, NpgsqlDbType.Bigint));
        ps.Add(P(req.PrevVersion, NpgsqlDbType.Integer));
        ps.Add(P(req.NewVersion, NpgsqlDbType.Integer));
        ps.Add(P(req.NewHash, NpgsqlDbType.Bigint));
        ps.Add(P((short)req.NewStatus, NpgsqlDbType.Smallint));
        ps.Add(P(req.NewContext, NpgsqlDbType.Bytea));
        ps.Add(P(req.Upload?.Size, NpgsqlDbType.Integer));
        ps.Add(P(req.Upload?.Checksum, NpgsqlDbType.Bigint));
        ps.Add(P(req.Upload?.Data, NpgsqlDbType.Bytea));
        ps.Add(P(ack?.Queue, NpgsqlDbType.Text));
        ps.Add(P(ack?.MsgId, NpgsqlDbType.Bigint));
        ps.Add(P(ack?.Archive ?? false, NpgsqlDbType.Boolean));
        if (qack != null)
        {
            ps.Add(P(qack.GroupId, NpgsqlDbType.Integer));
            ps.Add(P(qack.MsgId, NpgsqlDbType.Bigint));
        }
        return ToStatus((short)(await cmd.ExecuteScalarAsync(ct))!);
    }

    private static NpgsqlParameter P(object? value, NpgsqlDbType type)
        => new() { Value = value ?? DBNull.Value, NpgsqlDbType = type };

    public async ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(
            $"INSERT INTO {_blob} (blob_id, source_id, size, data) VALUES ($1, $2, $3, $4) ON CONFLICT (blob_id) DO NOTHING", conn);
        cmd.Parameters.Add(P(blobId, NpgsqlDbType.Bigint));
        cmd.Parameters.Add(P(sourceId, NpgsqlDbType.Integer));
        cmd.Parameters.Add(P(data.Length, NpgsqlDbType.Integer));
        cmd.Parameters.Add(new NpgsqlParameter<ReadOnlyMemory<byte>> { TypedValue = data, NpgsqlDbType = NpgsqlDbType.Bytea });
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
    {
        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT data FROM {_blob} WHERE blob_id = $1", conn);
        cmd.Parameters.Add(P(blobId, NpgsqlDbType.Bigint));
        return await cmd.ExecuteScalarAsync(ct) as byte[];
    }

    public async IAsyncEnumerable<SourceCursor> ScanCursorsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT source_id, last_seq FROM {_src}", conn);
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct)) yield return new SourceCursor(rd.GetInt32(0), rd.GetInt64(1));
    }

    public async IAsyncEnumerable<RecordRow> ScanRecordsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT source_id, record_id, status, version, hash FROM {_rec}", conn);
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct))
            yield return new RecordRow(rd.GetInt32(0), rd.GetInt64(1), (RecordStatus)rd.GetInt16(2), rd.GetInt32(3), rd.GetInt64(4));
    }

    public async IAsyncEnumerable<UploadRow> ScanUploadsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT source_id, seq, size, checksum FROM {_upl}", conn);
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct)) yield return new UploadRow(rd.GetInt32(0), rd.GetInt64(1), rd.GetInt32(2), rd.GetInt64(3));
    }

    public string Describe()
    {
        var csb = new NpgsqlConnectionStringBuilder(_o.ConnectionString);
        return $"postgres @ {csb.Host}:{csb.Port}/{csb.Database} (pool max {csb.MaxPoolSize}), unlogged={_o.UnloggedTables}, fillfactor={_o.FillFactor}" +
               (_o.BatchCommits ? $", group commit (max {_o.BatchMaxSize}/tx, {_o.BatchExecutors} executors)" : "");
    }

    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        _batchQueue?.Writer.TryComplete();
        try { await Task.WhenAll(_executors); } catch { /* shutting down */ }
        if (_o.BatchCommits)
            _log($"postgres store: {Batches:N0} commit batches, {BatchedCommits:N0} batched commits " +
                 $"(avg {(Batches > 0 ? (double)BatchedCommits / Batches : 0):F1}/tx), {_batchFallbacks} fallbacks, {_preloadHits:N0} preloaded contexts used");
        await _data.DisposeAsync();
        await _admin.DisposeAsync();
    }
}

public sealed class PostgresStoreFactory : IStateStoreFactory
{
    public string Type => "postgres";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
        => new PostgresStore(section.Get<PostgresStoreOptions>() ?? new(), context.Topology, context.Log);
}
