using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EventBench.SqlServer;

public sealed class SqlServerStoreOptions
{
    /// <summary>Data path connection string (set Max Pool Size high enough for all workers).</summary>
    public string ConnectionString { get; set; } = "";
    /// <summary>Connection for DDL, oracle scans and server stats (defaults to ConnectionString).</summary>
    public string? AdminConnectionString { get; set; }
    public string TablePrefix { get; set; } = "eb";
    /// <summary>
    /// Database-level commit durability applied at setup: null = leave as configured on the server
    /// (infra/mssql/durability.sh), "Forced" = delayed durable (best effort), "Disabled" = fully durable.
    /// </summary>
    public string? DelayedDurability { get; set; }
    /// <summary>
    /// Group commit, as the Postgres store's: commits (without an upload or a transactional ack) queue up and up to
    /// BatchMaxSize of them run in one transaction (one EXEC of the commit procedure each, sources in order, one log
    /// flush), on BatchExecutors connections.
    /// </summary>
    public bool BatchCommits { get; set; } = false;
    public int BatchMaxSize { get; set; } = 32;
    public int BatchExecutors { get; set; } = 16;
}

/// <summary>
/// SQL Server state store. Load = one SELECT (cursor + record + context; non-blocking under RCSI).
/// Commit = one round trip: a stored procedure that locks the source cursor (UPDLOCK), applies the
/// version-guarded mutation, stores the upload and advances the cursor — wrapped, in
/// AckMode=InTransaction, in a batch that also deletes the queue message in the same transaction.
/// </summary>
public sealed class SqlServerStore : IStateStore
{
    private readonly SqlServerStoreOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly string _src, _rec, _upl, _blob, _proc;
    private readonly string _loadSql, _commitSql;
    private readonly Dictionary<string, string> _ackCommitSql = new();
    private readonly string _admin;

    private readonly Channel<(CommitRequest Req, TaskCompletionSource<CommitStatus> Tcs)>? _batchQueue;
    private readonly List<Task> _executors = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly Dictionary<int, string> _batchSql = new();
    private long _batches, _batchedCommits, _batchFallbacks;

    public string Name => "sqlserver";
    public IStatsProvider? Stats { get; }

    public SqlServerStore(SqlServerStoreOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Store:ConnectionString is required for sqlserver");
        _o = options;
        _topology = topology;
        _log = log;
        _admin = options.AdminConnectionString ?? options.ConnectionString;
        var p = options.TablePrefix;
        (_src, _rec, _upl, _blob, _proc) = ($"{p}_source", $"{p}_record", $"{p}_upload", $"{p}_blob", $"{p}_commit");
        _loadSql = $"SELECT s.last_seq, r.version, r.hash, r.status, r.ctx FROM {_src} s " +
                   $"LEFT JOIN {_rec} r ON r.record_id = @r AND r.source_id = s.source_id WHERE s.source_id = @s";
        _commitSql = $"EXEC @rc = {_proc} @source, @seq, @kind, @record, @prev_version, @version, @hash, @status, @ctx, @upload_size, @upload_checksum, @upload_data";
        Stats = new SqlServerStats(_admin, "mssql", log);
        if (options.BatchCommits)
        {
            _batchQueue = Channel.CreateUnbounded<(CommitRequest, TaskCompletionSource<CommitStatus>)>(new UnboundedChannelOptions { SingleReader = false });
            for (int i = 0; i < Math.Max(1, options.BatchExecutors); i++) _executors.Add(Task.Run(() => BatchLoopAsync(_cts.Token)));
        }
    }

    private string BatchSql(int n)
    {
        lock (_batchSql)
        {
            if (_batchSql.TryGetValue(n, out var sql)) return sql;
            var b = new StringBuilder("SET XACT_ABORT ON; BEGIN TRAN; DECLARE ");
            for (int j = 0; j < n; j++) b.Append(j == 0 ? "" : ", ").Append("@rc").Append(j).Append(" int");
            b.Append("; ");
            for (int j = 0; j < n; j++)
                b.Append($"EXEC @rc{j} = {_proc} @s{j}, @q{j}, @k{j}, @r{j}, @pv{j}, @v{j}, @h{j}, @st{j}, @c{j}, NULL, NULL, NULL; ");
            b.Append("COMMIT; SELECT ");
            for (int j = 0; j < n; j++) b.Append(j == 0 ? "" : ", ").Append("@rc").Append(j);
            b.Append(';');
            return _batchSql[n] = b.ToString();
        }
    }

    private async Task BatchLoopAsync(CancellationToken ct)
    {
        var reader = _batchQueue!.Reader;
        var items = new List<(CommitRequest Req, TaskCompletionSource<CommitStatus> Tcs)>(_o.BatchMaxSize);
        while (!ct.IsCancellationRequested)
        {
            items.Clear();
            try { if (!await reader.WaitToReadAsync(ct)) return; }
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
                // Isolate: one bad event must not fail its batchmates; the batch may have committed before the error
                // reached us, so its events are in doubt (the commit procedure is idempotent: a replay is a duplicate)
                Interlocked.Increment(ref _batchFallbacks);
                foreach (var (req, _) in items) EventBench.Core.Processing.InDoubt.Add(req.Event.SourceId, req.Event.Seq);
                foreach (var (req, tcs) in items)
                {
                    try { tcs.TrySetResult(await CommitOneAsync(req, ct)); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }
            }
        }
    }

    private async Task<int[]> CommitManyAsync(List<CommitRequest> reqs, CancellationToken ct)
    {
        // rows locked in source order (no deadlocks between concurrent batches); results mapped back
        var order = Enumerable.Range(0, reqs.Count).OrderBy(i => reqs[i].Event.SourceId).ThenBy(i => reqs[i].Event.Seq).ToArray();
        int n = reqs.Count;
        await using var conn = new SqlConnection(_o.ConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand(BatchSql(n), conn);
        var ps = cmd.Parameters;
        for (int j = 0; j < n; j++)
        {
            var r = reqs[order[j]];
            ps.Add($"@s{j}", SqlDbType.Int).Value = r.Event.SourceId;
            ps.Add($"@q{j}", SqlDbType.BigInt).Value = r.Event.Seq;
            ps.Add($"@k{j}", SqlDbType.TinyInt).Value = (byte)r.Kind;
            ps.Add($"@r{j}", SqlDbType.BigInt).Value = r.Event.RecordId;
            ps.Add($"@pv{j}", SqlDbType.Int).Value = r.PrevVersion;
            ps.Add($"@v{j}", SqlDbType.Int).Value = r.NewVersion;
            ps.Add($"@h{j}", SqlDbType.BigInt).Value = r.NewHash;
            ps.Add($"@st{j}", SqlDbType.SmallInt).Value = (short)r.NewStatus;
            ps.Add($"@c{j}", SqlDbType.VarBinary, -1).Value = (object?)r.NewContext ?? DBNull.Value;
        }
        var codes = new int[n];
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        if (!await rd.ReadAsync(ct)) throw new InvalidOperationException("commit batch returned no row");
        for (int j = 0; j < n; j++) codes[order[j]] = rd.GetInt32(j);
        return codes;
    }

    private static CommitStatus ToStatus(int rc) => rc switch
    {
        0 => CommitStatus.Applied,
        1 => CommitStatus.Duplicate,
        2 => CommitStatus.Gap,
        _ => CommitStatus.RecordConflict,
    };

    private string AckCommitSql(string queueTable)
    {
        lock (_ackCommitSql)
        {
            if (!_ackCommitSql.TryGetValue(queueTable, out var sql))
                _ackCommitSql[queueTable] = sql =
                    "SET XACT_ABORT ON; BEGIN TRAN; " +
                    $"EXEC @rc = {_proc} @source, @seq, @kind, @record, @prev_version, @version, @hash, @status, @ctx, @upload_size, @upload_checksum, @upload_data; " +
                    $"IF @rc IN (0, 1) DELETE FROM {queueTable} WHERE group_id = @ack_group AND msg_id = @ack_msg; " +
                    "COMMIT;";
            return sql;
        }
    }

    public bool SupportsTransactionalAck(IBroker broker)
    {
        if (broker is not SqlQueueBroker q) return false;
        var mine = new SqlConnectionStringBuilder(_o.ConnectionString);
        var theirs = new SqlConnectionStringBuilder(q.Options.ConnectionString);
        if (!string.Equals(mine.DataSource, theirs.DataSource, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(mine.InitialCatalog, theirs.InitialCatalog, StringComparison.OrdinalIgnoreCase))
        {
            _log($"transactional ack unavailable: store {mine.DataSource}/{mine.InitialCatalog} != broker {theirs.DataSource}/{theirs.InitialCatalog}");
            return false;
        }
        return true;
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        string ddl = $"""
            DROP PROCEDURE IF EXISTS {_proc};
            DROP TABLE IF EXISTS {_upl}, {_blob}, {_rec}, {_src};
            CREATE TABLE {_src} (
                source_id  int NOT NULL CONSTRAINT pk_{_src} PRIMARY KEY,
                group_id   int NOT NULL,
                last_seq   bigint NOT NULL DEFAULT 0,
                updated_at datetime2(3) NULL
            );
            CREATE TABLE {_rec} (
                record_id     bigint NOT NULL CONSTRAINT pk_{_rec} PRIMARY KEY,
                source_id     int NOT NULL,
                status        smallint NOT NULL,
                version       int NOT NULL,
                hash          bigint NOT NULL,
                created_seq   bigint NOT NULL,
                updated_seq   bigint NOT NULL,
                completed_seq bigint NULL,
                ctx           varbinary(max) NULL,
                updated_at    datetime2(3) NOT NULL DEFAULT SYSUTCDATETIME()
            );
            CREATE TABLE {_upl} (
                source_id int NOT NULL,
                seq       bigint NOT NULL,
                record_id bigint NOT NULL,
                size      int NOT NULL,
                checksum  bigint NOT NULL,
                data      varbinary(max) NULL,
                CONSTRAINT pk_{_upl} PRIMARY KEY (source_id, seq)
            );
            EXEC sp_tableoption '{_upl}', 'large value types out of row', 1;
            CREATE TABLE {_blob} (
                blob_id   bigint NOT NULL CONSTRAINT pk_{_blob} PRIMARY KEY,
                source_id int NOT NULL,
                size      int NOT NULL,
                data      varbinary(max) NOT NULL
            );
            EXEC sp_tableoption '{_blob}', 'large value types out of row', 1;
            """;
        // Returns 0 applied, 1 duplicate, 2 gap, 3 record conflict.
        string proc = $"""
            CREATE PROCEDURE {_proc}
                @source int, @seq bigint, @kind tinyint, @record bigint, @prev_version int,
                @version int, @hash bigint, @status smallint, @ctx varbinary(max),
                @upload_size int, @upload_checksum bigint, @upload_data varbinary(max)
            AS
            BEGIN
                SET NOCOUNT ON; SET XACT_ABORT ON;
                DECLARE @last bigint;
                BEGIN TRAN;
                SELECT @last = last_seq FROM {_src} WITH (UPDLOCK, ROWLOCK) WHERE source_id = @source;
                IF @last IS NULL BEGIN ROLLBACK; THROW 50001, 'unknown source', 1; END;
                IF @last >= @seq BEGIN COMMIT; RETURN 1; END;
                IF @last <> @seq - 1 BEGIN COMMIT; RETURN 2; END;

                IF @kind = 1
                BEGIN
                    IF EXISTS (SELECT 1 FROM {_rec} WITH (UPDLOCK, HOLDLOCK) WHERE record_id = @record)
                    BEGIN COMMIT; RETURN 3; END;
                    INSERT INTO {_rec} (record_id, source_id, status, version, hash, created_seq, updated_seq, ctx)
                    VALUES (@record, @source, @status, @version, @hash, @seq, @seq, @ctx);
                END
                ELSE IF @kind = 2
                BEGIN
                    UPDATE {_rec}
                       SET version = @version, hash = @hash, status = @status,
                           ctx = COALESCE(@ctx, ctx), updated_seq = @seq, updated_at = SYSUTCDATETIME(),
                           completed_seq = CASE WHEN @status = 2 THEN @seq ELSE completed_seq END
                     WHERE record_id = @record AND source_id = @source AND version = @prev_version;
                    IF @@ROWCOUNT = 0 BEGIN COMMIT; RETURN 3; END;
                END;

                IF @upload_size IS NOT NULL
                    INSERT INTO {_upl} (source_id, seq, record_id, size, checksum, data)
                    VALUES (@source, @seq, @record, @upload_size, @upload_checksum, @upload_data);

                UPDATE {_src} SET last_seq = @seq, updated_at = SYSUTCDATETIME() WHERE source_id = @source;
                COMMIT;
                RETURN 0;
            END
            """;

        await using var conn = new SqlConnection(_admin);
        await conn.OpenAsync(ct);
        await Exec(conn, ddl, ct);
        await Exec(conn, proc, ct);
        if (!string.IsNullOrWhiteSpace(_o.DelayedDurability))
            await Exec(conn, $"ALTER DATABASE CURRENT SET DELAYED_DURABILITY = {_o.DelayedDurability.ToUpperInvariant()} WITH NO_WAIT", ct);

        var table = new DataTable();
        table.Columns.Add("source_id", typeof(int));
        table.Columns.Add("group_id", typeof(int));
        table.Columns.Add("last_seq", typeof(long));
        for (int i = 0; i < _topology.TotalSources; i++) table.Rows.Add(i, _topology.GroupOf(i).Id, 0L);
        using (var bulk = new SqlBulkCopy(conn) { DestinationTableName = _src, BatchSize = 10_000 })
        {
            foreach (DataColumn c in table.Columns) bulk.ColumnMappings.Add(c.ColumnName, c.ColumnName);
            await bulk.WriteToServerAsync(table, ct);
        }
        await Exec(conn, $"UPDATE STATISTICS {_src}", ct);

        await using var cmd = new SqlCommand(
            "SELECT CAST(SERVERPROPERTY('ProductVersion') AS nvarchar(64)), CAST(SERVERPROPERTY('Edition') AS nvarchar(128)), " +
            "is_read_committed_snapshot_on, delayed_durability_desc FROM sys.databases WHERE database_id = DB_ID()", conn);
        string server;
        await using (var rd = await cmd.ExecuteReaderAsync(ct))
        {
            await rd.ReadAsync(ct);
            server = $"{rd.GetString(0)} {rd.GetString(1)}, rcsi={rd.GetBoolean(2)}, delayed_durability={rd.GetString(3)}";
        }
        _log($"sqlserver store: schema created, {table.Rows.Count:N0} source cursors seeded; server {server}");
    }

    internal static async Task Exec(SqlConnection conn, string sql, CancellationToken ct)
    {
        await using var cmd = new SqlCommand(sql, conn) { CommandTimeout = 600 };
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        await using var conn = new SqlConnection(_o.ConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand(_loadSql, conn);
        cmd.Parameters.Add("@s", SqlDbType.Int).Value = ev.SourceId;
        cmd.Parameters.Add("@r", SqlDbType.BigInt).Value = ev.RecordId;
        await using var rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, ct);
        if (!await rd.ReadAsync(ct)) throw new InvalidOperationException($"unknown source {ev.SourceId}");
        if (rd.IsDBNull(1)) return new WorkContext { LastSeq = rd.GetInt64(0) };
        return new WorkContext
        {
            LastSeq = rd.GetInt64(0),
            RecordExists = true,
            Version = rd.GetInt32(1),
            Hash = rd.GetInt64(2),
            Status = (RecordStatus)rd.GetInt16(3),
            Context = rd.IsDBNull(4) ? null : (byte[])rd.GetValue(4),
        };
    }

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        if (_batchQueue != null && req.Upload is null && req.TransactionalAck is null)
        {
            var tcs = new TaskCompletionSource<CommitStatus>(TaskCreationOptions.RunContinuationsAsynchronously);
            _batchQueue.Writer.TryWrite((req, tcs));
            return await tcs.Task.WaitAsync(ct);
        }
        return await CommitOneAsync(req, ct);
    }

    private async Task<CommitStatus> CommitOneAsync(CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        var ack = req.TransactionalAck as ISqlQueueDelivery;
        if (req.TransactionalAck != null && ack is null)
            throw new InvalidOperationException("transactional ack requires a sqlserver queue delivery");

        await using var conn = new SqlConnection(_o.ConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand(ack is null ? _commitSql : AckCommitSql(ack.Table), conn);
        var ps = cmd.Parameters;
        ps.Add("@source", SqlDbType.Int).Value = ev.SourceId;
        ps.Add("@seq", SqlDbType.BigInt).Value = ev.Seq;
        ps.Add("@kind", SqlDbType.TinyInt).Value = (byte)req.Kind;
        ps.Add("@record", SqlDbType.BigInt).Value = ev.RecordId;
        ps.Add("@prev_version", SqlDbType.Int).Value = req.PrevVersion;
        ps.Add("@version", SqlDbType.Int).Value = req.NewVersion;
        ps.Add("@hash", SqlDbType.BigInt).Value = req.NewHash;
        ps.Add("@status", SqlDbType.SmallInt).Value = (short)req.NewStatus;
        ps.Add("@ctx", SqlDbType.VarBinary, -1).Value = (object?)req.NewContext ?? DBNull.Value;
        ps.Add("@upload_size", SqlDbType.Int).Value = (object?)req.Upload?.Size ?? DBNull.Value;
        ps.Add("@upload_checksum", SqlDbType.BigInt).Value = (object?)req.Upload?.Checksum ?? DBNull.Value;
        ps.Add("@upload_data", SqlDbType.VarBinary, -1).Value = (object?)req.Upload?.Data ?? DBNull.Value;
        if (ack != null)
        {
            ps.Add("@ack_group", SqlDbType.Int).Value = ack.GroupId;
            ps.Add("@ack_msg", SqlDbType.BigInt).Value = ack.MsgId;
        }
        var rc = ps.Add("@rc", SqlDbType.Int);
        rc.Direction = ParameterDirection.Output;
        await cmd.ExecuteNonQueryAsync(ct);
        return ToStatus((int)rc.Value);
    }

    public async ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        await using var conn = new SqlConnection(_o.ConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand(
            $"IF NOT EXISTS (SELECT 1 FROM {_blob} WHERE blob_id = @id) INSERT INTO {_blob} (blob_id, source_id, size, data) VALUES (@id, @s, @n, @d)", conn);
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = blobId;
        cmd.Parameters.Add("@s", SqlDbType.Int).Value = sourceId;
        cmd.Parameters.Add("@n", SqlDbType.Int).Value = data.Length;
        cmd.Parameters.Add("@d", SqlDbType.VarBinary, -1).Value = data.ToArray();
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
    {
        await using var conn = new SqlConnection(_o.ConnectionString);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand($"SELECT data FROM {_blob} WHERE blob_id = @id", conn);
        cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = blobId;
        return await cmd.ExecuteScalarAsync(ct) as byte[];
    }

    private async IAsyncEnumerable<T> Scan<T>(string sql, Func<SqlDataReader, T> map, [EnumeratorCancellation] CancellationToken ct)
    {
        await using var conn = new SqlConnection(_admin);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand(sql, conn) { CommandTimeout = 600 };
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct)) yield return map(rd);
    }

    public IAsyncEnumerable<SourceCursor> ScanCursorsAsync(CancellationToken ct)
        => Scan($"SELECT source_id, last_seq FROM {_src}", r => new SourceCursor(r.GetInt32(0), r.GetInt64(1)), ct);

    public IAsyncEnumerable<RecordRow> ScanRecordsAsync(CancellationToken ct)
        => Scan($"SELECT source_id, record_id, status, version, hash FROM {_rec}",
            r => new RecordRow(r.GetInt32(0), r.GetInt64(1), (RecordStatus)r.GetInt16(2), r.GetInt32(3), r.GetInt64(4)), ct);

    public IAsyncEnumerable<UploadRow> ScanUploadsAsync(CancellationToken ct)
        => Scan($"SELECT source_id, seq, size, checksum FROM {_upl}",
            r => new UploadRow(r.GetInt32(0), r.GetInt64(1), r.GetInt32(2), r.GetInt64(3)), ct);

    public string Describe()
    {
        var csb = new SqlConnectionStringBuilder(_o.ConnectionString);
        return $"sqlserver @ {csb.DataSource}/{csb.InitialCatalog} (pool max {csb.MaxPoolSize}), commit = proc with UPDLOCK cursor, " +
               $"delayed durability = {_o.DelayedDurability ?? "server setting"}" +
               (_o.BatchCommits ? $", group commit (max {_o.BatchMaxSize}/tx, {_o.BatchExecutors} executors)" : "");
    }

    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        _batchQueue?.Writer.TryComplete();
        try { await Task.WhenAll(_executors); } catch { /* shutting down */ }
        if (_o.BatchCommits)
        {
            long b = Interlocked.Read(ref _batches), c = Interlocked.Read(ref _batchedCommits);
            _log($"sqlserver store: {b:N0} commit batches, {c:N0} batched commits (avg {(b > 0 ? (double)c / b : 0):F1}/tx), {_batchFallbacks} fallbacks");
        }
        SqlConnection.ClearAllPools();
    }
}

public sealed class SqlServerStoreFactory : IStateStoreFactory
{
    public string Type => "sqlserver";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
        => new SqlServerStore(section.Get<SqlServerStoreOptions>() ?? new(), context.Topology, context.Log);
}
