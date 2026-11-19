using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace EventBench.Postgres;

public enum PgmqOrdering
{
    /// <summary>
    /// One queue per group; the source id goes into the "x-pgmq-group" header and workers read with
    /// pgmq.read_grouped* — any worker of the group may take any source, but a source's next message
    /// stays blocked while its head is in flight (requires pgmq >= 1.8).
    /// </summary>
    Fifo,

    /// <summary>
    /// N queues per group (source -> queue by hash). Each queue is read by exactly one worker using
    /// plain pgmq.read, so order is preserved by single-consumer ownership (Kafka-style partitions).
    /// </summary>
    Partitioned,
}

public enum PgmqFifoIndex { None, Gin, Btree }

public sealed class PgmqOptions
{
    /// <summary>Data path connection string (typically through PgBouncer, transaction pooling).</summary>
    public string ConnectionString { get; set; } = "";
    /// <summary>Direct connection for DDL and metrics (defaults to ConnectionString).</summary>
    public string? AdminConnectionString { get; set; }
    public PgmqOrdering Ordering { get; set; } = PgmqOrdering.Fifo;
    /// <summary>read_grouped_head | read_grouped | read_grouped_rr</summary>
    public string FifoReadFunction { get; set; } = "read_grouped_head";
    public PgmqFifoIndex FifoIndex { get; set; } = PgmqFifoIndex.Btree;
    /// <summary>Queues per group in Partitioned mode (0 = one per worker).</summary>
    public int PartitionsPerGroup { get; set; } = 0;
    public string QueuePrefix { get; set; } = "eb";
    public bool Unlogged { get; set; } = false;
    public int VisibilityTimeoutSeconds { get; set; } = 30;
    /// <summary>pgmq.archive instead of pgmq.delete on ack.</summary>
    public bool ArchiveOnAck { get; set; } = false;
}

/// <summary>Exposes the pgmq identity of a delivery so the Postgres store can delete it inside its own transaction.</summary>
public interface IPgmqDelivery
{
    string Queue { get; }
    long MsgId { get; }
    bool Archive { get; }
}

public sealed class PgmqBroker : IBroker, IStatsProvider
{
    private const string FifoHeader = "x-pgmq-group";
    private static readonly HashSet<string> FifoFunctions = ["read_grouped_head", "read_grouped", "read_grouped_rr", "eb_read_head"];
    private const string FifoHeaderConst = "x-pgmq-group";

    /// <summary>
    /// Head-only FIFO read. pgmq.read_grouped_head computes MIN(msg_id) per group over the whole queue table on
    /// every call (cost grows with the backlog). This walks the queue in msg_id order and keeps a row only if
    /// no older message of its group exists (index probe on the FIFO btree), regardless of visibility: an
    /// in-flight head blocks its group. Row locking + EvalPlanQual re-check vt, as in pgmq.
    /// </summary>
    private const string EbReadHeadSql = """
        CREATE OR REPLACE FUNCTION public.eb_read_head(queue_name text, vt integer, qty integer)
        RETURNS SETOF pgmq.message_record LANGUAGE plpgsql AS $fn$
        DECLARE qtable text := pgmq.format_table_name(queue_name, 'q');
        BEGIN
          RETURN QUERY EXECUTE format($q$
            WITH c AS (
              SELECT q.msg_id FROM pgmq.%1$I q
              WHERE q.vt <= clock_timestamp()
                AND NOT EXISTS (
                  SELECT 1 FROM pgmq.%1$I p
                  WHERE COALESCE(p.headers->>'x-pgmq-group', '_default_fifo_group')
                      = COALESCE(q.headers->>'x-pgmq-group', '_default_fifo_group')
                    AND p.msg_id < q.msg_id)
              ORDER BY q.msg_id
              LIMIT $1
              FOR UPDATE SKIP LOCKED)
            UPDATE pgmq.%1$I m
               SET vt = clock_timestamp() + %2$L, read_ct = m.read_ct + 1, last_read_at = clock_timestamp()
              FROM c WHERE m.msg_id = c.msg_id
            RETURNING m.msg_id, m.read_ct, m.enqueued_at, m.last_read_at, m.vt, m.message, m.headers
          $q$, qtable, make_interval(secs => vt)) USING qty;
        END $fn$;
        """;

    private readonly Topology _topology;
    private readonly Action<string> _log;
    internal readonly NpgsqlDataSource Data;
    private readonly NpgsqlDataSource _admin;

    public PgmqOptions Options { get; }
    public string Name => "pgmq";
    public string DatabaseName { get; }
    public IStatsProvider Stats => this;

    public PgmqBroker(PgmqOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Broker:ConnectionString is required for pgmq");
        if (options.Ordering == PgmqOrdering.Fifo && !FifoFunctions.Contains(options.FifoReadFunction))
            throw new InvalidOperationException($"Broker:FifoReadFunction must be one of {string.Join(", ", FifoFunctions)}");
        Options = options;
        _topology = topology;
        _log = log;
        Data = NpgsqlDataSource.Create(options.ConnectionString);
        _admin = NpgsqlDataSource.Create(options.AdminConnectionString ?? options.ConnectionString);
        DatabaseName = new NpgsqlConnectionStringBuilder(options.ConnectionString).Database ?? "";

        if (options.Ordering == PgmqOrdering.Partitioned)
            foreach (var g in topology.Groups)
                if (Partitions(g) < g.Workers)
                    throw new InvalidOperationException(
                        $"Partitioned mode: group {g.Name} has {g.Workers} workers but only {Partitions(g)} partitions (workers would idle).");
    }

    private int Partitions(GroupInfo g) => Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup : g.Workers;

    internal string QueueName(int groupId) => $"{Options.QueuePrefix}_g{groupId}";
    internal string QueueName(int groupId, int partition) => $"{Options.QueuePrefix}_g{groupId}_p{partition}";

    internal string QueueFor(EventEnvelope ev)
    {
        if (Options.Ordering == PgmqOrdering.Fifo) return QueueName(ev.Group);
        var g = _topology.Groups[ev.Group];
        return QueueName(ev.Group, (ev.SourceId - g.FirstSourceId) % Partitions(g));
    }

    private IEnumerable<string> AllQueues()
    {
        foreach (var g in _topology.Groups)
        {
            if (Options.Ordering == PgmqOrdering.Fifo) yield return QueueName(g.Id);
            else for (int p = 0; p < Partitions(g); p++) yield return QueueName(g.Id, p);
        }
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await Exec(conn, "CREATE EXTENSION IF NOT EXISTS pgmq", ct);
        if (Options.FifoReadFunction == "eb_read_head") await Exec(conn, EbReadHeadSql, ct);

        await using (var cmd = new NpgsqlCommand("SELECT extversion FROM pg_extension WHERE extname = 'pgmq'", conn))
            _log($"pgmq extension version {await cmd.ExecuteScalarAsync(ct)}");

        // Drop leftovers of previous runs with the same prefix.
        var existing = new List<string>();
        await using (var cmd = new NpgsqlCommand("SELECT queue_name FROM pgmq.list_queues() WHERE queue_name LIKE $1", conn))
        {
            cmd.Parameters.AddWithValue(Options.QueuePrefix + "\\_%");
            await using var rd = await cmd.ExecuteReaderAsync(ct);
            while (await rd.ReadAsync(ct)) existing.Add(rd.GetString(0));
        }
        foreach (var q in existing)
        {
            await using var cmd = new NpgsqlCommand("SELECT pgmq.drop_queue($1)", conn);
            cmd.Parameters.AddWithValue(q);
            await cmd.ExecuteNonQueryAsync(ct);
        }

        int created = 0;
        foreach (var q in AllQueues())
        {
            await using (var cmd = new NpgsqlCommand(Options.Unlogged ? "SELECT pgmq.create_unlogged($1)" : "SELECT pgmq.create($1)", conn))
            {
                cmd.Parameters.AddWithValue(q);
                await cmd.ExecuteNonQueryAsync(ct);
            }
            if (Options.Ordering == PgmqOrdering.Fifo)
            {
                switch (Options.FifoIndex)
                {
                    case PgmqFifoIndex.Gin:
                        await using (var cmd = new NpgsqlCommand("SELECT pgmq.create_fifo_index($1)", conn))
                        {
                            cmd.Parameters.AddWithValue(q);
                            await cmd.ExecuteNonQueryAsync(ct);
                        }
                        break;
                    case PgmqFifoIndex.Btree:
                        // Matches the grouping expression used by pgmq.read_grouped*.
                        await Exec(conn, $"CREATE INDEX IF NOT EXISTS q_{q}_fifo_btree ON pgmq.q_{q} " +
                                         $"((COALESCE(headers->>'{FifoHeader}', '_default_fifo_group')), msg_id)", ct);
                        break;
                }
            }
            created++;
        }
        _log($"pgmq: dropped {existing.Count} old queues, created {created} queues ({Options.Ordering})");
    }

    private static async Task Exec(NpgsqlConnection conn, string sql, CancellationToken ct)
    {
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public IEventPublisher CreatePublisher(int groupId) => new PgmqPublisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        if (Options.Ordering == PgmqOrdering.Fifo)
            return new PgmqConsumer(this, [QueueName(groupId)],
                Options.FifoReadFunction == "eb_read_head" ? "public.eb_read_head" : $"pgmq.{Options.FifoReadFunction}");
        var g = _topology.Groups[groupId];
        var owned = Enumerable.Range(0, Partitions(g)).Where(p => p % g.Workers == workerIndex).Select(p => QueueName(groupId, p)).ToArray();
        return new PgmqConsumer(this, owned, "pgmq.read");
    }

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(
            "SELECT COALESCE(sum(queue_length), 0)::bigint FROM pgmq.metrics_all() WHERE queue_name LIKE $1", conn);
        cmd.Parameters.AddWithValue(Options.QueuePrefix + "\\_%");
        return (long)(await cmd.ExecuteScalarAsync(ct))!;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(
            "SELECT COALESCE(sum(queue_length),0)::float8, COALESCE(sum(queue_visible_length),0)::float8, " +
            "COALESCE(max(oldest_msg_age_sec),0)::float8, COALESCE(max(queue_length),0)::float8 " +
            "FROM pgmq.metrics_all() WHERE queue_name LIKE $1", conn);
        cmd.Parameters.AddWithValue(Options.QueuePrefix + "\\_%");
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        await rd.ReadAsync(ct);
        return new Dictionary<string, double>
        {
            ["broker_backlog"] = rd.GetDouble(0),
            ["broker_visible"] = rd.GetDouble(1),
            ["broker_oldest_age_s"] = rd.GetDouble(2),
            ["broker_max_queue_len"] = rd.GetDouble(3),
        };
    }

    public string Describe()
    {
        var csb = new NpgsqlConnectionStringBuilder(Options.ConnectionString);
        var mode = Options.Ordering == PgmqOrdering.Fifo
            ? $"fifo via {Options.FifoReadFunction}, index={Options.FifoIndex}"
            : $"partitioned ({(Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup.ToString() : "=workers")} per group)";
        return $"pgmq {mode}, vt={Options.VisibilityTimeoutSeconds}s, unlogged={Options.Unlogged}, " +
               $"{(Options.ArchiveOnAck ? "archive" : "delete")} on ack @ {csb.Host}:{csb.Port}/{csb.Database} (pool max {csb.MaxPoolSize})";
    }

    public async ValueTask DisposeAsync()
    {
        await Data.DisposeAsync();
        await _admin.DisposeAsync();
    }

    // ------------------------------------------------------------------------------------------------

    private sealed class PgmqPublisher(PgmqBroker broker) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            bool fifo = broker.Options.Ordering == PgmqOrdering.Fifo;
            // Preserve per-queue order (and therefore per-source order).
            var byQueue = new Dictionary<string, (List<string> Msgs, List<string> Headers)>();
            foreach (var ev in events)
            {
                var q = broker.QueueFor(ev);
                if (!byQueue.TryGetValue(q, out var lists)) byQueue[q] = lists = (new List<string>(), new List<string>());
                lists.Msgs.Add(ev.ToJson());
                if (fifo) lists.Headers.Add($"{{\"{FifoHeader}\":\"{ev.SourceId}\"}}");
            }

            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var batch = new NpgsqlBatch(conn);
            foreach (var (queue, (msgs, headers)) in byQueue)
            {
                var cmd = new NpgsqlBatchCommand(fifo
                    ? "SELECT pgmq.send_batch($1, $2, $3)"
                    : "SELECT pgmq.send_batch($1, $2)");
                cmd.Parameters.Add(new NpgsqlParameter { Value = queue, NpgsqlDbType = NpgsqlDbType.Text });
                cmd.Parameters.Add(new NpgsqlParameter { Value = msgs.ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Jsonb });
                if (fifo)
                    cmd.Parameters.Add(new NpgsqlParameter { Value = headers.ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Jsonb });
                batch.BatchCommands.Add(cmd);
            }
            // A multi-statement batch runs as one implicit transaction: all-or-nothing.
            await batch.ExecuteNonQueryAsync(ct);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class PgmqConsumer(PgmqBroker broker, string[] queues, string readFunction) : IEventConsumer, ISourceLinger
    {
        /// <summary>
        /// Linger (FIFO only): take the source's oldest message if it is visible. The oldest is selected without
        /// a vt filter, so an in-flight older message makes this return nothing instead of skipping ahead.
        /// </summary>
        public async ValueTask<IDelivery?> NextForSourceAsync(IDelivery done, CancellationToken ct)
        {
            if (broker.Options.Ordering != PgmqOrdering.Fifo || done is not PgmqDelivery pd) return null;
            string t = "pgmq.\"q_" + pd.Queue + "\"";
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand(
                $"WITH h AS (SELECT msg_id FROM {t} WHERE COALESCE(headers->>'{FifoHeaderConst}', '_default_fifo_group') = $1 ORDER BY msg_id LIMIT 1), " +
                $"c AS (SELECT q.msg_id FROM {t} q JOIN h ON q.msg_id = h.msg_id WHERE q.vt <= clock_timestamp() FOR UPDATE OF q SKIP LOCKED) " +
                $"UPDATE {t} m SET vt = clock_timestamp() + make_interval(secs => $2), read_ct = m.read_ct + 1, last_read_at = clock_timestamp() " +
                "FROM c WHERE m.msg_id = c.msg_id RETURNING m.msg_id, m.read_ct, m.enqueued_at, m.message::text", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = done.Event.SourceId.ToString(System.Globalization.CultureInfo.InvariantCulture), NpgsqlDbType = NpgsqlDbType.Text });
            cmd.Parameters.Add(new NpgsqlParameter { Value = (double)broker.Options.VisibilityTimeoutSeconds, NpgsqlDbType = NpgsqlDbType.Double });
            await using var rd = await cmd.ExecuteReaderAsync(ct);
            if (!await rd.ReadAsync(ct)) return null;
            return new PgmqDelivery(broker, pd.Queue, rd.GetInt64(0), rd.GetInt32(1),
                Clock.ToUs(rd.GetFieldValue<DateTime>(2)), EventEnvelope.FromJson(rd.GetString(3)), Clock.NowUs());
        }

        private int _next;

        public bool SharedSources => broker.Options.Ordering == PgmqOrdering.Fifo;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            // Round-robin over owned queues; stop at the first non-empty one.
            for (int attempt = 0; attempt < queues.Length; attempt++)
            {
                var queue = queues[_next];
                _next = (_next + 1) % queues.Length;
                var result = await ReadAsync(queue, maxCount, ct);
                if (result.Count > 0) return result;
            }
            return [];
        }

        private async Task<List<IDelivery>> ReadAsync(string queue, int maxCount, CancellationToken ct)
        {
            var list = new List<IDelivery>();
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand(
                $"SELECT msg_id, read_ct, enqueued_at, message::text FROM {readFunction}($1, $2, $3)", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = queue, NpgsqlDbType = NpgsqlDbType.Text });
            cmd.Parameters.Add(new NpgsqlParameter { Value = broker.Options.VisibilityTimeoutSeconds, NpgsqlDbType = NpgsqlDbType.Integer });
            cmd.Parameters.Add(new NpgsqlParameter { Value = maxCount, NpgsqlDbType = NpgsqlDbType.Integer });
            await using var rd = await cmd.ExecuteReaderAsync(ct);
            long now = 0;
            while (await rd.ReadAsync(ct))
            {
                if (now == 0) now = Clock.NowUs();
                list.Add(new PgmqDelivery(broker, queue, rd.GetInt64(0), rd.GetInt32(1),
                    Clock.ToUs(rd.GetFieldValue<DateTime>(2)), EventEnvelope.FromJson(rd.GetString(3)), now));
            }
            // pgmq.read returns rows in msg_id order within the queue; keep it explicit.
            list.Sort(static (a, b) => ((PgmqDelivery)a).MsgId.CompareTo(((PgmqDelivery)b).MsgId));
            return list;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class PgmqDelivery(PgmqBroker broker, string queue, long msgId, int readCount, long enqueuedUs,
        EventEnvelope ev, long receivedUs) : IDelivery, IPgmqDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => readCount;
        public string Queue => queue;
        public long MsgId => msgId;
        public bool Archive => broker.Options.ArchiveOnAck;

        public async ValueTask AckAsync(CancellationToken ct)
        {
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand(Archive ? "SELECT pgmq.archive($1, $2)" : "SELECT pgmq.delete($1, $2)", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = queue, NpgsqlDbType = NpgsqlDbType.Text });
            cmd.Parameters.Add(new NpgsqlParameter { Value = msgId, NpgsqlDbType = NpgsqlDbType.Bigint });
            await cmd.ExecuteNonQueryAsync(ct);
        }

        public async ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            // Partitioned mode must retry immediately: a delayed head would let later messages of the
            // same source (read by the same single consumer) overtake it.
            int vt = broker.Options.Ordering == PgmqOrdering.Partitioned ? 0 : (int)Math.Ceiling(delay.TotalSeconds);
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand("SELECT msg_id FROM pgmq.set_vt($1, $2, $3)", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = queue, NpgsqlDbType = NpgsqlDbType.Text });
            cmd.Parameters.Add(new NpgsqlParameter { Value = msgId, NpgsqlDbType = NpgsqlDbType.Bigint });
            cmd.Parameters.Add(new NpgsqlParameter { Value = vt, NpgsqlDbType = NpgsqlDbType.Integer });
            await cmd.ExecuteNonQueryAsync(ct);
        }
    }
}

public sealed class PgmqBrokerFactory : IBrokerFactory
{
    public string Type => "pgmq";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new PgmqBroker(section.Get<PgmqOptions>() ?? new(), context.Topology, context.Log);
}
