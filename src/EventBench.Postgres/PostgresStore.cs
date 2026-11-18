using System.Runtime.CompilerServices;
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
    }

    public bool SupportsTransactionalAck(IBroker broker)
    {
        if (broker is not PgmqBroker pgmq) return false;
        var mine = new NpgsqlConnectionStringBuilder(_o.ConnectionString);
        if (!string.Equals(mine.Database, pgmq.DatabaseName, StringComparison.Ordinal))
        {
            _log($"transactional ack unavailable: store database '{mine.Database}' != broker database '{pgmq.DatabaseName}'");
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
        var ev = req.Event;
        var ack = req.TransactionalAck as IPgmqDelivery;
        if (req.TransactionalAck != null && ack is null)
            throw new InvalidOperationException("transactional ack requires a pgmq delivery");

        await using var conn = await _data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(_commitSql, conn);
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
        var code = (short)(await cmd.ExecuteScalarAsync(ct))!;
        return code switch
        {
            0 => CommitStatus.Applied,
            1 => CommitStatus.Duplicate,
            2 => CommitStatus.Gap,
            _ => CommitStatus.RecordConflict,
        };
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
        return $"postgres @ {csb.Host}:{csb.Port}/{csb.Database} (pool max {csb.MaxPoolSize}), unlogged={_o.UnloggedTables}, fillfactor={_o.FillFactor}";
    }

    public async ValueTask DisposeAsync()
    {
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
