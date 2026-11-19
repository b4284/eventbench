using System.Data;
using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Configuration;

namespace EventBench.SqlServer;

public sealed class SqlQueueOptions
{
    /// <summary>Data path connection string (set Max Pool Size high enough for workers + producers).</summary>
    public string ConnectionString { get; set; } = "";
    /// <summary>Connection for DDL and metrics (defaults to ConnectionString).</summary>
    public string? AdminConnectionString { get; set; }
    public string TablePrefix { get; set; } = "eb";
    public int VisibilityTimeoutSeconds { get; set; } = 30;
    /// <summary>Retries of a dequeue/publish chosen as a deadlock victim (error 1205) before surfacing it.</summary>
    public int DeadlockRetries { get; set; } = 5;
}

/// <summary>Exposes the queue identity of a delivery so the SQL Server store can delete it inside its own transaction.</summary>
public interface ISqlQueueDelivery
{
    string Table { get; }
    int GroupId { get; }
    long MsgId { get; }
}

/// <summary>
/// A table-backed queue inside SQL Server (the PGMQ analogue): one table for all groups, clustered on
/// (group_id, msg_id). FIFO per source with competing consumers: a dequeue only takes a source's head
/// message (no older message of that source still exists) whose visibility time has passed; the head
/// stays in the table while it is being processed, so the source's later messages stay blocked until it
/// is deleted (ack). READPAST + UPDLOCK lets concurrent workers of a group skip each other's rows.
/// Publishing inserts a whole batch spanning many groups in one statement (table-valued parameter).
/// </summary>
public sealed class SqlQueueBroker : IBroker, IStatsProvider
{
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly string _admin;
    private readonly string _dequeueSql, _insertSql;
    private readonly SqlServerStats _serverStats;

    public SqlQueueOptions Options { get; }
    public string Name => "sqlqueue";
    public string Table { get; }
    public string ItemType { get; }
    public IStatsProvider Stats => this;

    public SqlQueueBroker(SqlQueueOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Broker:ConnectionString is required for sqlqueue");
        Options = options;
        _topology = topology;
        _log = log;
        _admin = options.AdminConnectionString ?? options.ConnectionString;
        Table = $"{options.TablePrefix}_q";
        ItemType = $"{options.TablePrefix}_q_item";
        _serverStats = new SqlServerStats(_admin, "mssqlq", log);

        _dequeueSql = $"""
            SET NOCOUNT ON;
            DECLARE @now datetime2(3) = SYSUTCDATETIME();
            WITH c AS (
                SELECT TOP (@n) q.msg_id, q.vt, q.read_ct, q.enqueued_at, q.body
                FROM {Table} q WITH (UPDLOCK, READPAST, ROWLOCK)
                WHERE q.group_id = @g AND q.vt <= @now
                  -- Locking read (not the RCSI statement snapshot): a predecessor inserted after the
                  -- snapshot was taken must still block its successors, or two consumers could hold
                  -- messages of the same source at once.
                  AND NOT EXISTS (SELECT 1 FROM {Table} p WITH (READCOMMITTEDLOCK)
                                  WHERE p.group_id = @g AND p.source_id = q.source_id AND p.msg_id < q.msg_id)
                ORDER BY q.msg_id)
            UPDATE c SET vt = DATEADD(second, @vt, @now), read_ct = read_ct + 1
            OUTPUT inserted.msg_id, inserted.read_ct, inserted.enqueued_at, inserted.body;
            """;
        _insertSql = $"""
            SET NOCOUNT ON;
            DECLARE @now datetime2(3) = SYSUTCDATETIME();
            INSERT INTO {Table} (group_id, source_id, vt, enqueued_at, body)
            SELECT group_id, source_id, @now, @now, body FROM @items ORDER BY ord;
            """;
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        await using var conn = new SqlConnection(_admin);
        await conn.OpenAsync(ct);
        await SqlServerStore.Exec(conn, $"""
            DROP TABLE IF EXISTS {Table};
            DROP TYPE IF EXISTS {ItemType};
            CREATE TYPE {ItemType} AS TABLE (ord int NOT NULL PRIMARY KEY, group_id int NOT NULL, source_id int NOT NULL, body varbinary(max) NOT NULL);
            CREATE TABLE {Table} (
                msg_id      bigint IDENTITY(1,1) NOT NULL,
                group_id    int NOT NULL,
                source_id   int NOT NULL,
                vt          datetime2(3) NOT NULL,
                read_ct     int NOT NULL DEFAULT 0,
                enqueued_at datetime2(3) NOT NULL,
                body        varbinary(max) NOT NULL,
                CONSTRAINT pk_{Table} PRIMARY KEY CLUSTERED (group_id, msg_id) WITH (OPTIMIZE_FOR_SEQUENTIAL_KEY = ON)
            );
            CREATE NONCLUSTERED INDEX ix_{Table}_source ON {Table} (group_id, source_id, msg_id);
            """, ct);
        _log($"sqlqueue: table {Table} (clustered group_id,msg_id + index group_id,source_id,msg_id) for {_topology.Groups.Count} groups");
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);
    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, groupId);

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        await using var conn = new SqlConnection(_admin);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand($"SELECT COUNT_BIG(*) FROM {Table}", conn) { CommandTimeout = 120 };
        return (long)(await cmd.ExecuteScalarAsync(ct))!;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, double>(await _serverStats.SampleAsync(ct));
        try
        {
            await using var conn = new SqlConnection(_admin);
            await conn.OpenAsync(ct);
            // Row count from partition metadata: cheap, approximate under concurrent change.
            await using var cmd = new SqlCommand(
                "SELECT COALESCE(SUM(row_count), 0) FROM sys.dm_db_partition_stats WHERE object_id = OBJECT_ID(@t) AND index_id IN (0, 1)", conn);
            cmd.Parameters.Add("@t", SqlDbType.NVarChar, 256).Value = Table;
            result["broker_backlog"] = Convert.ToDouble(await cmd.ExecuteScalarAsync(ct));
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _log($"sqlqueue backlog sample failed: {e.Message}");
        }
        return result;
    }

    public string Describe()
    {
        var csb = new SqlConnectionStringBuilder(Options.ConnectionString);
        return $"sqlqueue table {Table}, per-source FIFO head dequeue (UPDLOCK+READPAST), vt={Options.VisibilityTimeoutSeconds}s, " +
               $"delete on ack @ {csb.DataSource}/{csb.InitialCatalog} (pool max {csb.MaxPoolSize})";
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    internal async Task<T> WithDeadlockRetry<T>(Func<Task<T>> action)
    {
        for (int attempt = 0; ; attempt++)
        {
            try { return await action(); }
            catch (SqlException e) when (e.Number == 1205 && attempt < Options.DeadlockRetries)
            {
                await Task.Delay(Random.Shared.Next(1, 10 << attempt));
            }
        }
    }

    // ------------------------------------------------------------------------------------------------

    private static readonly SqlMetaData[] ItemMeta =
    [
        new("ord", SqlDbType.Int), new("group_id", SqlDbType.Int), new("source_id", SqlDbType.Int),
        new("body", SqlDbType.VarBinary, SqlMetaData.Max),
    ];

    private sealed class Publisher(SqlQueueBroker broker) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            if (events.Count == 0) return;
            var rows = new List<SqlDataRecord>(events.Count);
            for (int i = 0; i < events.Count; i++)
            {
                var ev = events[i];
                var r = new SqlDataRecord(ItemMeta);
                r.SetInt32(0, i);
                r.SetInt32(1, ev.Group);
                r.SetInt32(2, ev.SourceId);
                var body = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope);
                r.SetBytes(3, 0, body, 0, body.Length);
                rows.Add(r);
            }

            await broker.WithDeadlockRetry(async () =>
            {
                await using var conn = new SqlConnection(broker.Options.ConnectionString);
                await conn.OpenAsync(ct);
                await using var cmd = new SqlCommand(broker._insertSql, conn);
                var p = cmd.Parameters.Add("@items", SqlDbType.Structured);
                p.TypeName = broker.ItemType;
                p.Value = rows;
                // One statement = one (autocommit) transaction: the batch is all-or-nothing.
                return await cmd.ExecuteNonQueryAsync(ct);
            });
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(SqlQueueBroker broker, int groupId) : IEventConsumer
    {
        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            return await broker.WithDeadlockRetry(async () =>
            {
                var list = new List<IDelivery>(maxCount);
                await using var conn = new SqlConnection(broker.Options.ConnectionString);
                await conn.OpenAsync(ct);
                await using var cmd = new SqlCommand(broker._dequeueSql, conn);
                cmd.Parameters.Add("@n", SqlDbType.Int).Value = maxCount;
                cmd.Parameters.Add("@g", SqlDbType.Int).Value = groupId;
                cmd.Parameters.Add("@vt", SqlDbType.Int).Value = broker.Options.VisibilityTimeoutSeconds;
                await using var rd = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct);
                long now = 0;
                while (await rd.ReadAsync(ct))
                {
                    if (now == 0) now = Clock.NowUs();
                    long msgId = rd.GetInt64(0);
                    int readCt = rd.GetInt32(1);
                    var enq = DateTime.SpecifyKind(rd.GetDateTime(2), DateTimeKind.Utc);
                    var body = await rd.GetFieldValueAsync<byte[]>(3, ct);
                    list.Add(new Delivery(broker, groupId, msgId, readCt, Clock.ToUs(enq), EventEnvelope.FromJson(body), now));
                }
                list.Sort(static (a, b) => ((Delivery)a).MsgId.CompareTo(((Delivery)b).MsgId));
                return (IReadOnlyList<IDelivery>)list;
            });
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Delivery(SqlQueueBroker broker, int groupId, long msgId, int readCount, long enqueuedUs,
        EventEnvelope ev, long receivedUs) : IDelivery, ISqlQueueDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => readCount;
        public string Table => broker.Table;
        public int GroupId => groupId;
        public long MsgId => msgId;

        public async ValueTask AckAsync(CancellationToken ct)
        {
            await broker.WithDeadlockRetry(async () =>
            {
                await using var conn = new SqlConnection(broker.Options.ConnectionString);
                await conn.OpenAsync(ct);
                await using var cmd = new SqlCommand($"DELETE FROM {broker.Table} WHERE group_id = @g AND msg_id = @m", conn);
                cmd.Parameters.Add("@g", SqlDbType.Int).Value = groupId;
                cmd.Parameters.Add("@m", SqlDbType.BigInt).Value = msgId;
                return await cmd.ExecuteNonQueryAsync(ct);
            });
        }

        public async ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            // The head stays in place (still blocking its source's later messages) and becomes visible again later.
            await broker.WithDeadlockRetry(async () =>
            {
                await using var conn = new SqlConnection(broker.Options.ConnectionString);
                await conn.OpenAsync(ct);
                await using var cmd = new SqlCommand(
                    $"UPDATE {broker.Table} SET vt = DATEADD(millisecond, @d, SYSUTCDATETIME()) WHERE group_id = @g AND msg_id = @m", conn);
                cmd.Parameters.Add("@d", SqlDbType.Int).Value = (int)delay.TotalMilliseconds;
                cmd.Parameters.Add("@g", SqlDbType.Int).Value = groupId;
                cmd.Parameters.Add("@m", SqlDbType.BigInt).Value = msgId;
                return await cmd.ExecuteNonQueryAsync(ct);
            });
        }
    }
}

public sealed class SqlQueueBrokerFactory : IBrokerFactory
{
    public string Type => "sqlqueue";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new SqlQueueBroker(section.Get<SqlQueueOptions>() ?? new(), context.Topology, context.Log);
}
