using System.Globalization;
using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace EventBench.Postgres;

public sealed class PgQueueOptions
{
    /// <summary>Data path (typically PgBouncer).</summary>
    public string ConnectionString { get; set; } = "";
    /// <summary>Direct connection: DDL, LISTEN, backlog (defaults to ConnectionString).</summary>
    public string? AdminConnectionString { get; set; }
    public string Table { get; set; } = "ebq_msg";
    /// <summary>Hash partitions of the queue table (spreads insert/delete hot spots).</summary>
    public int Partitions { get; set; } = 16;
    public bool Notify { get; set; } = true;
    public int FallbackPollMs { get; set; } = 200;
    public int MaxGroupsPerFetch { get; set; } = 64;
    public int FetchParallelism { get; set; } = 4;
    public bool PreloadContext { get; set; } = true;
    public string StoreTablePrefix { get; set; } = "eb";
    public int ReceiveWaitMs { get; set; } = 500;
    /// <summary>
    /// false: this instance only creates the schema, publishes and counts the backlog; no LISTEN and no
    /// dispatch loop (the remote dispatcher instances read the queue).
    /// </summary>
    public bool Dispatch { get; set; } = true;
    /// <summary>Include the backlog count (a full count(*)) in the periodic stats.</summary>
    public bool SampleBacklog { get; set; } = true;
}

/// <summary>Lets the Postgres store delete a pgqueue message inside its commit transaction.</summary>
public interface IPgQueueDelivery
{
    int GroupId { get; }
    long MsgId { get; }
    /// <summary>The message as stored (UTF-8 JSON envelope).</summary>
    byte[] Body { get; }
}

/// <summary>
/// "pgqueue": a dispatcher-native queue on Postgres — one hash-partitioned table for all groups instead of
/// one PGMQ table per queue.
/// <list type="bullet">
/// <item>Publish: one multi-row INSERT per producer batch, across all its groups.</item>
/// <item>Read: the dispatcher (sole reader of its groups) asks for source <b>heads</b>: rows with no older row
/// of the same source, minus the ones it already handed out (tracked in memory). No vt column and no write on
/// read. A source's next message becomes a head only when the previous one is deleted, which happens inside
/// the worker's commit transaction (store group commit), so per-source order needs no cursor and concurrent
/// producers committing ids out of order cannot cause a miss.</item>
/// <item>Retry: a failed delivery leaves the in-flight set after the retry delay and is read again as a head.</item>
/// <item>Wake-ups: a statement trigger (transition table) notifies the groups an INSERT touched; a completed
/// event marks its group; a slow fallback poll covers the rest.</item>
/// </list>
/// Crash safety: rows stay until committed and deleted; a restarted dispatcher re-reads every head.
/// </summary>
public sealed class PgQueueBroker : IBroker, IStatsProvider
{
    private sealed class Waiter(int max)
    {
        public readonly int Max = max;
        public readonly TaskCompletionSource<List<IDelivery>> Tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    private sealed class GroupState(int id)
    {
        public readonly int Id = id;
        public readonly object Gate = new();
        public readonly LinkedList<Waiter> Waiters = new();
        public readonly Queue<IDelivery> Ready = new();
        public readonly HashSet<long> InFlight = new();   // handed out or ready, not yet deleted
        public int Demand;
        public bool Dirty = true;
        public bool Fetching;
        public long LastFetchUs;
    }

    private readonly PgQueueOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    internal readonly NpgsqlDataSource Data;
    private readonly NpgsqlDataSource _admin;
    private readonly GroupState[] _groups;
    private readonly SemaphoreSlim _wake = new(0);
    private readonly SemaphoreSlim _fetchSlots;
    private readonly CancellationTokenSource _cts = new();
    private readonly string _fetchSql, _loadSql, _insertSql;
    private Task? _loop, _listenTask;
    private NpgsqlConnection? _listen;
    /// <summary>Groups this instance serves; null = all (single dispatcher). Set by a lease manager.</summary>
    private bool[]? _serving;

    private long _fetchCalls, _groupReads, _emptyGroupReads, _rows, _notifies, _preloaded;
    private long _prevTicks;
    private (long Calls, long Reads, long Empty, long Rows, long Notifies, long Preloaded) _prev;

    public string Name => "pgqueue";
    public IStatsProvider Stats => this;
    public string DatabaseName { get; }
    public string Table => _o.Table;

    public PgQueueBroker(PgQueueOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Broker:ConnectionString is required for pgqueue");
        _o = options;
        _topology = topology;
        _log = log;
        Data = NpgsqlDataSource.Create(options.ConnectionString);
        _admin = NpgsqlDataSource.Create(options.AdminConnectionString ?? options.ConnectionString);
        DatabaseName = new NpgsqlConnectionStringBuilder(options.ConnectionString).Database ?? "";
        _groups = topology.Groups.Select(g => new GroupState(g.Id)).ToArray();
        _fetchSlots = new SemaphoreSlim(Math.Max(1, options.FetchParallelism));
        var t = options.Table;
        _insertSql = $"INSERT INTO {t} (group_id, source_id, body) " +
                     $"SELECT g, s, b FROM unnest($1::int[], $2::int[], $3::bytea[]) WITH ORDINALITY AS x(g, s, b, o) ORDER BY o";
        _fetchSql = $"SELECT d.g, x.msg_id, x.source_id, x.enqueued_at, x.body " +
                    $"FROM unnest($1::int[], $2::int[]) AS d(g, n) CROSS JOIN LATERAL (" +
                    $"  SELECT q.msg_id, q.source_id, q.enqueued_at, q.body FROM {t} q " +
                    $"  WHERE q.group_id = d.g AND q.msg_id <> ALL($3::bigint[]) " +
                    $"    AND NOT EXISTS (SELECT 1 FROM {t} p WHERE p.group_id = d.g AND p.source_id = q.source_id AND p.msg_id < q.msg_id) " +
                    $"  ORDER BY q.msg_id LIMIT d.n) x";
        var p = options.StoreTablePrefix;
        _loadSql = $"SELECT k.i, s.last_seq, r.version, r.hash, r.status, r.ctx " +
                   $"FROM unnest($1::int[], $2::bigint[]) WITH ORDINALITY AS k(src, rec, i) " +
                   $"JOIN {p}_source s ON s.source_id = k.src " +
                   $"LEFT JOIN {p}_record r ON r.record_id = k.rec AND r.source_id = k.src";
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        PreloadedContexts.Clear();
        var t = _o.Table;
        await using var conn = await _admin.OpenConnectionAsync(ct);
        var ddl = new System.Text.StringBuilder();
        ddl.Append($"""
            DROP TABLE IF EXISTS {t} CASCADE;
            CREATE TABLE {t} (
                group_id    int NOT NULL,
                msg_id      bigint GENERATED ALWAYS AS IDENTITY,
                source_id   int NOT NULL,
                enqueued_at timestamptz NOT NULL DEFAULT clock_timestamp(),
                body        bytea NOT NULL,
                PRIMARY KEY (group_id, msg_id)
            ) PARTITION BY HASH (group_id);
            """);
        int n = Math.Max(1, _o.Partitions);
        for (int i = 0; i < n; i++)
            ddl.Append($"CREATE TABLE {t}_p{i} PARTITION OF {t} FOR VALUES WITH (MODULUS {n}, REMAINDER {i}) " +
                       "WITH (autovacuum_vacuum_scale_factor = 0, autovacuum_vacuum_threshold = 2000, autovacuum_vacuum_cost_delay = 0, " +
                       "autovacuum_vacuum_insert_scale_factor = 0, autovacuum_vacuum_insert_threshold = 10000);\n");
        ddl.Append($"CREATE INDEX {t}_src ON {t} (group_id, source_id, msg_id);\n");
        if (_o.Notify)
            ddl.Append($"""
                CREATE OR REPLACE FUNCTION {t}_notify() RETURNS trigger LANGUAGE plpgsql AS $$
                BEGIN
                  PERFORM pg_notify('{t}', g::text) FROM (SELECT DISTINCT group_id AS g FROM new_rows) x;
                  RETURN NULL;
                END $$;
                CREATE TRIGGER {t}_notify AFTER INSERT ON {t} REFERENCING NEW TABLE AS new_rows
                  FOR EACH STATEMENT EXECUTE FUNCTION {t}_notify();
                """);
        await using (var cmd = new NpgsqlCommand(ddl.ToString(), conn)) await cmd.ExecuteNonQueryAsync(ct);

        if (_o.Dispatch) await StartDispatchAsync(ct);
        _log($"pgqueue: table {t} ({n} hash partitions), notify={_o.Notify}, preload={_o.PreloadContext}, " +
             (_o.Dispatch ? $"{_o.MaxGroupsPerFetch} groups/fetch x {_o.FetchParallelism}" : "remote dispatchers"));
    }

    /// <summary>Starts LISTEN and the dispatch loop without touching the schema (remote dispatcher instances).</summary>
    public async Task StartDispatchAsync(CancellationToken ct)
    {
        if (_o.Notify)
        {
            await OpenListenAsync(ct);
            _listenTask = Task.Run(() => ListenLoopAsync(_cts.Token));
        }
        _loop = Task.Run(() => DispatchLoopAsync(_cts.Token));
    }

    // ---- group ownership (remote dispatcher instances; null _serving = this instance serves every group) ----

    public int GroupCount => _groups.Length;

    public void EnableOwnership() => _serving = new bool[_groups.Length];

    public bool IsServing(int group) => _serving is not { } s || Volatile.Read(ref s[group]);

    /// <summary>
    /// Start serving a group just acquired: forget any earlier state of it (another instance may have handed
    /// out and finished its messages meanwhile) and read its heads.
    /// </summary>
    public void StartServing(int group)
    {
        var g = _groups[group];
        lock (g.Gate)
        {
            g.InFlight.Clear();
            g.Ready.Clear();
            g.Dirty = true;
            Volatile.Write(ref _serving![group], true);
        }
        _wake.Release();
    }

    /// <summary>
    /// Stop handing out the group's messages. Messages already handed out stay in the in-flight set until
    /// their commits arrive (drain); fetched but not handed ones are dropped; waiting receives return empty.
    /// </summary>
    public void StopServing(int group)
    {
        var g = _groups[group];
        List<Waiter> waiters;
        lock (g.Gate)
        {
            Volatile.Write(ref _serving![group], false);
            while (g.Ready.TryDequeue(out var d)) g.InFlight.Remove(((Delivery)d).MsgId);
            waiters = g.Waiters.ToList();
            g.Waiters.Clear();
            g.Demand = 0;
        }
        foreach (var w in waiters) w.Tcs.TrySetResult([]);
    }

    /// <summary>Serve a draining group again (it was not handed over after all); its in-flight set is still valid.</summary>
    public void ResumeServing(int group)
    {
        var g = _groups[group];
        lock (g.Gate)
        {
            g.Dirty = true;
            Volatile.Write(ref _serving![group], true);
        }
        _wake.Release();
    }

    /// <summary>Forget a group entirely (after its lease was handed over).</summary>
    public void DropGroup(int group)
    {
        var g = _groups[group];
        lock (g.Gate) { g.InFlight.Clear(); g.Ready.Clear(); }
    }

    public int InFlightCount(int group)
    {
        var g = _groups[group];
        lock (g.Gate) return g.InFlight.Count;
    }

    /// <summary>Release a handed-out message (completed, or failed: then offered again after the delay).</summary>
    public void Release(int group, long msgId, int delayMs = 0)
    {
        var g = _groups[group];
        if (delayMs <= 0)
        {
            lock (g.Gate) g.InFlight.Remove(msgId);
            MarkDirty(g);
            return;
        }
        _ = Task.Delay(delayMs, CancellationToken.None).ContinueWith(_ =>
        {
            lock (g.Gate) g.InFlight.Remove(msgId);
            MarkDirty(g);
        }, TaskScheduler.Default);
    }

    /// <summary>Delete a message outside a commit (the worker skipped it as already applied).</summary>
    public async Task DeleteAsync(int group, long msgId, CancellationToken ct)
    {
        await using var conn = await Data.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"DELETE FROM {_o.Table} WHERE group_id = $1 AND msg_id = $2", conn);
        cmd.Parameters.Add(new NpgsqlParameter { Value = group, NpgsqlDbType = NpgsqlDbType.Integer });
        cmd.Parameters.Add(new NpgsqlParameter { Value = msgId, NpgsqlDbType = NpgsqlDbType.Bigint });
        await cmd.ExecuteNonQueryAsync(ct);
    }

    /// <summary>Start of a new run (the queue table was recreated, ids restart): forget every group's state.</summary>
    public void ResetAll()
    {
        PreloadedContexts.Clear();
        foreach (var g in _groups)
            lock (g.Gate) { g.InFlight.Clear(); g.Ready.Clear(); g.Dirty = true; }
        _wake.Release();
    }

    public (long FetchCalls, long GroupReads, long EmptyReads, long Rows, long Notifies, long Preloaded) Counters =>
        (Interlocked.Read(ref _fetchCalls), Interlocked.Read(ref _groupReads), Interlocked.Read(ref _emptyGroupReads),
         Interlocked.Read(ref _rows), Interlocked.Read(ref _notifies), Interlocked.Read(ref _preloaded));

    private async Task OpenListenAsync(CancellationToken ct)
    {
        var admin = _o.AdminConnectionString ?? _o.ConnectionString;
        var conn = new NpgsqlConnection(admin + ";Keepalive=5");
        await conn.OpenAsync(ct);
        conn.Notification += (_, e) =>
        {
            Interlocked.Increment(ref _notifies);
            if (int.TryParse(e.Payload, NumberStyles.Integer, CultureInfo.InvariantCulture, out int g) && (uint)g < (uint)_groups.Length)
                MarkDirty(_groups[g]);
        };
        await using (var cmd = new NpgsqlCommand($"LISTEN {_o.Table}", conn)) await cmd.ExecuteNonQueryAsync(ct);
        _listen = conn;
    }

    /// <summary>
    /// Waits for notifications. If the connection breaks (e.g. the primary died and a standby was promoted),
    /// it reconnects (multi-host connection strings land on the new primary), re-LISTENs and marks every
    /// group dirty, since notifications sent meanwhile were lost.
    /// </summary>
    private async Task ListenLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try { await _listen!.WaitAsync(ct); }
            catch (OperationCanceledException) { return; }
            catch (Exception e)
            {
                _log($"pgqueue listen connection lost ({e.GetType().Name}: {e.Message}); reconnecting");
                try { await _listen!.DisposeAsync(); } catch { /* broken */ }
                while (!ct.IsCancellationRequested)
                {
                    try { await OpenListenAsync(ct); _log("pgqueue listen reconnected"); break; }
                    catch (OperationCanceledException) { return; }
                    catch (Exception e2)
                    {
                        _log($"pgqueue listen reconnect failed: {e2.Message}");
                        try { await Task.Delay(500, ct); } catch (OperationCanceledException) { return; }
                    }
                }
                foreach (var g in _groups) MarkDirty(g);
            }
        }
    }

    private void MarkDirty(GroupState g)
    {
        lock (g.Gate) g.Dirty = true;
        _wake.Release();
    }

    private async Task DispatchLoopAsync(CancellationToken ct)
    {
        long fallbackUs = _o.FallbackPollMs * 1000L;
        var due = new List<(GroupState G, int Qty, long[] Exclude)>();
        while (!ct.IsCancellationRequested)
        {
            try { await _wake.WaitAsync(TimeSpan.FromMilliseconds(Math.Min(20, _o.FallbackPollMs)), ct); }
            catch (OperationCanceledException) { return; }
            while (_wake.CurrentCount > 0 && _wake.Wait(0)) { }

            long now = Clock.NowUs();
            due.Clear();
            foreach (var g in _groups)
            {
                lock (g.Gate)
                {
                    if (!IsServing(g.Id)) continue;
                    int want = g.Demand - g.Ready.Count;
                    if (want <= 0 || g.Fetching) continue;
                    if (!g.Dirty && now - g.LastFetchUs < fallbackUs) continue;
                    g.Fetching = true;
                    g.Dirty = false;
                    due.Add((g, want, g.InFlight.ToArray()));
                }
            }
            for (int i = 0; i < due.Count; i += _o.MaxGroupsPerFetch)
            {
                var chunk = due.GetRange(i, Math.Min(_o.MaxGroupsPerFetch, due.Count - i));
                try { await _fetchSlots.WaitAsync(ct); }
                catch (OperationCanceledException) { return; }
                _ = Task.Run(async () =>
                {
                    try { await FetchAsync(chunk, ct); }
                    finally { _fetchSlots.Release(); }
                }, CancellationToken.None);
            }
        }
    }

    private async Task FetchAsync(List<(GroupState G, int Qty, long[] Exclude)> chunk, CancellationToken ct)
    {
        var got = new Dictionary<int, List<Delivery>>();
        try
        {
            Interlocked.Increment(ref _fetchCalls);
            await using var conn = await Data.OpenConnectionAsync(ct);
            await using (var cmd = new NpgsqlCommand(_fetchSql, conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter { Value = chunk.Select(c => c.G.Id).ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
                cmd.Parameters.Add(new NpgsqlParameter { Value = chunk.Select(c => c.Qty).ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
                cmd.Parameters.Add(new NpgsqlParameter { Value = chunk.SelectMany(c => c.Exclude).ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Bigint });
                await using var rd = await cmd.ExecuteReaderAsync(ct);
                long now = Clock.NowUs();
                while (await rd.ReadAsync(ct))
                {
                    int gid = rd.GetInt32(0);
                    if (!got.TryGetValue(gid, out var list)) got[gid] = list = new List<Delivery>();
                    var body = rd.GetFieldValue<byte[]>(4);
                    list.Add(new Delivery(this, _groups[gid], rd.GetInt64(1), Clock.ToUs(rd.GetFieldValue<DateTime>(3)), EventEnvelope.FromJson(body), body, now));
                }
            }
            if (_o.PreloadContext && got.Count > 0) await PreloadAsync(conn, got.Values.SelectMany(l => l).ToList(), ct);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _log($"pgqueue fetch error: {e.GetType().Name}: {e.Message}");
            got.Clear();
        }
        catch (OperationCanceledException) { got.Clear(); }

        long done = Clock.NowUs();
        foreach (var (g, qty, _) in chunk)
        {
            got.TryGetValue(g.Id, out var list);
            Interlocked.Increment(ref _groupReads);
            if (list is null || list.Count == 0) Interlocked.Increment(ref _emptyGroupReads);
            else Interlocked.Add(ref _rows, list.Count);
            List<(Waiter W, List<IDelivery> Items)>? fulfil = null;
            lock (g.Gate)
            {
                g.Fetching = false;
                g.LastFetchUs = done;
                if (list is { Count: > 0 } && IsServing(g.Id))
                {
                    foreach (var d in list.OrderBy(d => d.MsgId))
                        if (g.InFlight.Add(d.MsgId)) g.Ready.Enqueue(d);
                    if (list.Count >= qty) g.Dirty = true;
                }
                while (g.Ready.Count > 0 && g.Waiters.First is { } node)
                {
                    var w = node.Value;
                    g.Waiters.RemoveFirst();
                    g.Demand -= w.Max;
                    var items = new List<IDelivery>(Math.Min(w.Max, g.Ready.Count));
                    while (items.Count < w.Max && g.Ready.TryDequeue(out var x)) items.Add(x);
                    (fulfil ??= new()).Add((w, items));
                }
            }
            if (fulfil != null) foreach (var (w, items) in fulfil) w.Tcs.TrySetResult(items);
        }
        _wake.Release();
    }

    private async Task PreloadAsync(NpgsqlConnection conn, List<Delivery> all, CancellationToken ct)
    {
        await using var cmd = new NpgsqlCommand(_loadSql, conn);
        cmd.Parameters.Add(new NpgsqlParameter { Value = all.Select(d => d.Event.SourceId).ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
        cmd.Parameters.Add(new NpgsqlParameter { Value = all.Select(d => d.Event.RecordId).ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Bigint });
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct))
        {
            var ev = all[(int)rd.GetInt64(0) - 1].Event;
            var ctx = rd.IsDBNull(2)
                ? new WorkContext { LastSeq = rd.GetInt64(1) }
                : new WorkContext
                {
                    LastSeq = rd.GetInt64(1), RecordExists = true, Version = rd.GetInt32(2), Hash = rd.GetInt64(3),
                    Status = (RecordStatus)rd.GetInt16(4), Context = rd.IsDBNull(5) ? null : rd.GetFieldValue<byte[]>(5),
                };
            PreloadedContexts.Put(ev.SourceId, ev.Seq, ctx);
            Interlocked.Increment(ref _preloaded);
        }
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, _groups[groupId]);

    public async Task<long> GetBacklogAsync(CancellationToken ct)
    {
        await using var conn = await _admin.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand($"SELECT count(*) FROM {_o.Table}", conn);
        return (long)(await cmd.ExecuteScalarAsync(ct))!;
    }

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var r = new Dictionary<string, double>();
        try { if (_o.SampleBacklog) r["broker_backlog"] = await GetBacklogAsync(ct); }
        catch (Exception e) when (e is not OperationCanceledException) { _log($"pgqueue backlog sample failed: {e.Message}"); }
        long now = Environment.TickCount64;
        var cur = (Interlocked.Read(ref _fetchCalls), Interlocked.Read(ref _groupReads), Interlocked.Read(ref _emptyGroupReads),
            Interlocked.Read(ref _rows), Interlocked.Read(ref _notifies), Interlocked.Read(ref _preloaded));
        if (_prevTicks != 0)
        {
            double s = Math.Max(0.001, (now - _prevTicks) / 1000.0);
            r["dispatch_fetch_calls_per_s"] = Math.Round((cur.Item1 - _prev.Calls) / s, 1);
            r["dispatch_group_reads_per_s"] = Math.Round((cur.Item2 - _prev.Reads) / s, 1);
            r["dispatch_empty_reads_per_s"] = Math.Round((cur.Item3 - _prev.Empty) / s, 1);
            r["dispatch_rows_per_s"] = Math.Round((cur.Item4 - _prev.Rows) / s, 1);
            r["dispatch_notifies_per_s"] = Math.Round((cur.Item5 - _prev.Notifies) / s, 1);
            r["dispatch_preloaded_per_s"] = Math.Round((cur.Item6 - _prev.Preloaded) / s, 1);
        }
        _prev = cur;
        _prevTicks = now;
        int waiting = 0, inflight = 0;
        foreach (var g in _groups) lock (g.Gate) { waiting += g.Waiters.Count; inflight += g.InFlight.Count; }
        r["dispatch_waiting_receives"] = waiting;
        r["dispatch_in_flight"] = inflight;
        return r;
    }

    public string Describe()
        => $"pgqueue table {_o.Table} ({_o.Partitions} hash partitions) @ {new NpgsqlConnectionStringBuilder(_o.ConnectionString).Host}:" +
           $"{new NpgsqlConnectionStringBuilder(_o.ConnectionString).Port}/{DatabaseName}; head reads without vt writes, " +
           $"notify={_o.Notify}, fallback {_o.FallbackPollMs} ms, {_o.MaxGroupsPerFetch} groups/fetch x {_o.FetchParallelism}, preload={_o.PreloadContext}";

    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        try { if (_loop != null) await _loop; } catch { /* shutting down */ }
        try { if (_listenTask != null) await _listenTask; } catch { /* shutting down */ }
        if (_listen != null)
        {
            try { await _listen.CloseAsync(); } catch { /* best effort */ }
            try { await _listen.DisposeAsync(); } catch { /* best effort */ }
        }
        await Data.DisposeAsync();
        await _admin.DisposeAsync();
    }

    // ------------------------------------------------------------------------------------------------

    private sealed class Publisher(PgQueueBroker broker) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            if (events.Count == 0) return;
            var groups = new int[events.Count];
            var sources = new int[events.Count];
            var bodies = new byte[events.Count][];
            for (int i = 0; i < events.Count; i++)
            {
                groups[i] = events[i].Group;
                sources[i] = events[i].SourceId;
                bodies[i] = JsonSerializer.SerializeToUtf8Bytes(events[i], EventJsonContext.Default.EventEnvelope);
            }
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand(broker._insertSql, conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = groups, NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
            cmd.Parameters.Add(new NpgsqlParameter { Value = sources, NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
            cmd.Parameters.Add(new NpgsqlParameter { Value = bodies, NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Bytea });
            await cmd.ExecuteNonQueryAsync(ct);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(PgQueueBroker broker, GroupState g) : IEventConsumer
    {
        public bool SharedSources => true;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            Waiter w;
            if (!broker.IsServing(g.Id)) return [];
            lock (g.Gate)
            {
                if (g.Ready.Count > 0)
                {
                    var items = new List<IDelivery>(Math.Min(maxCount, g.Ready.Count));
                    while (items.Count < maxCount && g.Ready.TryDequeue(out var x)) items.Add(x);
                    return items;
                }
                w = new Waiter(maxCount);
                g.Waiters.AddLast(w);
                g.Demand += maxCount;
            }
            broker._wake.Release();
            using (var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                timeout.CancelAfter(broker._o.ReceiveWaitMs);
                try { return await w.Tcs.Task.WaitAsync(timeout.Token); }
                catch (OperationCanceledException) { /* timed out or shutting down */ }
            }
            lock (g.Gate)
            {
                if (w.Tcs.Task.IsCompletedSuccessfully) return w.Tcs.Task.Result;
                if (g.Waiters.Remove(w)) g.Demand -= w.Max;
            }
            ct.ThrowIfCancellationRequested();
            return [];
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Delivery(PgQueueBroker broker, GroupState g, long msgId, long enqueuedUs, EventEnvelope ev, byte[] body, long receivedUs)
        : IDelivery, IPgQueueDelivery, IDeliveryCompletion
    {
        private int _attempts = 1;
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => enqueuedUs;
        public int DeliveryCount => _attempts;
        public int GroupId => g.Id;
        public long MsgId => msgId;
        public byte[] Body => body;

        /// <summary>AfterCommit mode: delete the row (the in-flight entry goes on completion).</summary>
        public async ValueTask AckAsync(CancellationToken ct)
        {
            await using var conn = await broker.Data.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand($"DELETE FROM {broker._o.Table} WHERE group_id = $1 AND msg_id = $2", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = g.Id, NpgsqlDbType = NpgsqlDbType.Integer });
            cmd.Parameters.Add(new NpgsqlParameter { Value = msgId, NpgsqlDbType = NpgsqlDbType.Bigint });
            await cmd.ExecuteNonQueryAsync(ct);
        }

        /// <summary>No database write: the row stays; it becomes readable again when it leaves the in-flight set.</summary>
        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            Interlocked.Increment(ref _attempts);
            _ = Task.Delay(delay, CancellationToken.None).ContinueWith(_ =>
            {
                lock (g.Gate) g.InFlight.Remove(msgId);
                broker.MarkDirty(g);
            }, TaskScheduler.Default);
            return ValueTask.CompletedTask;
        }

        public void Completed(bool success)
        {
            if (!success) return; // the nack path releases it after the delay
            // Committed (row deleted in the same transaction) or a duplicate: forget it, look for the next head.
            lock (g.Gate) g.InFlight.Remove(msgId);
            broker.MarkDirty(g);
        }
    }
}

public sealed class PgQueueBrokerFactory : IBrokerFactory
{
    public string Type => "pgqueue";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new PgQueueBroker(section.Get<PgQueueOptions>() ?? new(), context.Topology, context.Log);
}
