using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace EventBench.Postgres;

public sealed class PgDispatchOptions
{
    /// <summary>Statement-level insert trigger + LISTEN: wake the dispatcher when a group's queue gets messages.</summary>
    public bool Notify { get; set; } = true;
    /// <summary>Re-check a group that has waiting workers at least this often even without a wake-up.</summary>
    public int FallbackPollMs { get; set; } = 200;
    /// <summary>Groups read in one round trip (one batched statement per group).</summary>
    public int MaxGroupsPerFetch { get; set; } = 64;
    /// <summary>Concurrent fetch round trips.</summary>
    public int FetchParallelism { get; set; } = 4;
    /// <summary>Load the execution context of every fetched event in the same pass (mode B).</summary>
    public bool PreloadContext { get; set; } = false;
    /// <summary>Store table prefix (for PreloadContext).</summary>
    public string StoreTablePrefix { get; set; } = "eb";
    /// <summary>How long a worker's receive waits for the dispatcher before returning empty.</summary>
    public int ReceiveWaitMs { get; set; } = 500;
}

/// <summary>
/// "pgdispatch": a message dispatcher between Postgres (PGMQ FIFO queues) and the workers, in process.
/// Workers never poll the database: a receive registers demand (free slots) with the dispatcher and waits.
/// The dispatcher reads a group's queue only when the group has demand and something may be there:
/// an insert notification (statement trigger + LISTEN on one direct connection), a completed event of
/// the group (its source's next message may now be the head), or the fallback interval. Reads of many
/// groups go out in one round trip (head-only FIFO read, eb_read_head). With PreloadContext the
/// dispatcher also loads every fetched event's context in one query and hands it to the store
/// (PreloadedContexts). Postgres remains the source of truth: deliveries are ordinary PGMQ messages
/// (vt, in-transaction delete), so a dispatcher crash only delays them until vt expires.
/// </summary>
public sealed class PgDispatchBroker : IBroker, IStatsProvider
{
    private sealed class Waiter(int max)
    {
        public readonly int Max = max;
        public readonly TaskCompletionSource<List<IDelivery>> Tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    private sealed class GroupState(int id, string queue)
    {
        public readonly int Id = id;
        public readonly string Queue = queue;
        public readonly object Gate = new();
        public readonly LinkedList<Waiter> Waiters = new();
        public readonly Queue<IDelivery> Ready = new();
        public int Demand;
        public bool Dirty = true;
        public bool Fetching;
        public long LastFetchUs;
    }

    internal readonly PgmqBroker Inner;
    private readonly PgDispatchOptions _d;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly GroupState[] _groups;
    private readonly Dictionary<string, GroupState> _byGroupKey;
    private readonly SemaphoreSlim _wake = new(0);
    private readonly SemaphoreSlim _fetchSlots;
    private readonly CancellationTokenSource _cts = new();
    private readonly string _loadSql;
    private Task? _loop, _listenTask;
    private NpgsqlConnection? _listen;
    private readonly string _adminConnectionString;

    private long _fetchCalls, _groupReads, _emptyGroupReads, _rows, _notifies, _completions, _preloaded;
    private long _prevTicks;
    private (long Calls, long Reads, long Empty, long Rows, long Notifies, long Preloaded) _prev;

    public string Name => "pgdispatch";
    public IStatsProvider Stats => this;
    public string DatabaseName => Inner.DatabaseName;

    public PgDispatchBroker(PgmqOptions pgmq, PgDispatchOptions dispatch, Topology topology, Action<string> log)
    {
        pgmq.Ordering = PgmqOrdering.Fifo;
        pgmq.FifoReadFunction = "eb_read_head";
        Inner = new PgmqBroker(pgmq, topology, log);
        _d = dispatch;
        _topology = topology;
        _log = log;
        _adminConnectionString = pgmq.AdminConnectionString ?? pgmq.ConnectionString;
        _groups = topology.Groups.Select(g => new GroupState(g.Id, Inner.QueueName(g.Id))).ToArray();
        _byGroupKey = _groups.ToDictionary(g => g.Id.ToString(System.Globalization.CultureInfo.InvariantCulture));
        _fetchSlots = new SemaphoreSlim(Math.Max(1, dispatch.FetchParallelism));
        var p = dispatch.StoreTablePrefix;
        _loadSql = $"SELECT k.i, s.last_seq, r.version, r.hash, r.status, r.ctx " +
                   $"FROM unnest($1::int[], $2::bigint[]) WITH ORDINALITY AS k(src, rec, i) " +
                   $"JOIN {p}_source s ON s.source_id = k.src " +
                   $"LEFT JOIN {p}_record r ON r.record_id = k.rec AND r.source_id = k.src";
    }

    public async Task SetupAsync(CancellationToken ct)
    {
        PreloadedContexts.Clear();
        await Inner.SetupAsync(ct);
        if (_d.Notify)
        {
            await using var conn = new NpgsqlConnection(_adminConnectionString);
            await conn.OpenAsync(ct);
            await Exec(conn, """
                CREATE OR REPLACE FUNCTION public.eb_dispatch_notify() RETURNS trigger LANGUAGE plpgsql AS $$
                BEGIN PERFORM pg_notify('eb_dispatch', TG_ARGV[0]); RETURN NULL; END $$;
                """, ct);
            foreach (var g in _groups)
                await Exec(conn, $"CREATE TRIGGER eb_dispatch_notify AFTER INSERT ON pgmq.q_{g.Queue} " +
                                 $"FOR EACH STATEMENT EXECUTE FUNCTION public.eb_dispatch_notify('{g.Id}')", ct);

            _listen = new NpgsqlConnection(_adminConnectionString + ";Keepalive=10");
            await _listen.OpenAsync(ct);
            _listen.Notification += (_, e) =>
            {
                Interlocked.Increment(ref _notifies);
                if (_byGroupKey.TryGetValue(e.Payload, out var g)) MarkDirty(g);
            };
            await Exec(_listen, "LISTEN eb_dispatch", ct);
            _listenTask = Task.Run(() => ListenLoopAsync(_cts.Token));
        }
        _loop = Task.Run(() => DispatchLoopAsync(_cts.Token));
        _log($"pgdispatch: {_groups.Length} groups, notify={_d.Notify}, fallback {_d.FallbackPollMs} ms, " +
             $"{_d.MaxGroupsPerFetch} groups/fetch x {_d.FetchParallelism}, preload context={_d.PreloadContext}");
    }

    private static async Task Exec(NpgsqlConnection conn, string sql, CancellationToken ct)
    {
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    private async Task ListenLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try { await _listen!.WaitAsync(ct); }
            catch (OperationCanceledException) { return; }
            catch (Exception e)
            {
                _log($"pgdispatch listen error: {e.Message}");
                await Task.Delay(500, CancellationToken.None);
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
        long fallbackUs = _d.FallbackPollMs * 1000L;
        var due = new List<(GroupState G, int Qty)>();
        while (!ct.IsCancellationRequested)
        {
            try { await _wake.WaitAsync(TimeSpan.FromMilliseconds(Math.Min(20, _d.FallbackPollMs)), ct); }
            catch (OperationCanceledException) { return; }
            while (_wake.CurrentCount > 0 && _wake.Wait(0)) { } // coalesce wake-ups

            long now = Clock.NowUs();
            due.Clear();
            foreach (var g in _groups)
            {
                lock (g.Gate)
                {
                    int want = g.Demand - g.Ready.Count;
                    if (want <= 0 || g.Fetching) continue;
                    if (!g.Dirty && now - g.LastFetchUs < fallbackUs) continue;
                    g.Fetching = true;
                    g.Dirty = false;
                    due.Add((g, want));
                }
            }
            for (int i = 0; i < due.Count; i += _d.MaxGroupsPerFetch)
            {
                var chunk = due.GetRange(i, Math.Min(_d.MaxGroupsPerFetch, due.Count - i));
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

    private async Task FetchAsync(List<(GroupState G, int Qty)> chunk, CancellationToken ct)
    {
        var got = new List<PgmqBroker.PgmqDelivery>[chunk.Count];
        try
        {
            Interlocked.Increment(ref _fetchCalls);
            await using var conn = await Inner.Data.OpenConnectionAsync(ct);
            await using (var batch = new NpgsqlBatch(conn))
            {
                foreach (var (g, qty) in chunk)
                {
                    var c = new NpgsqlBatchCommand("SELECT msg_id, read_ct, enqueued_at, message::text FROM public.eb_read_head($1, $2, $3)");
                    c.Parameters.Add(new NpgsqlParameter { Value = g.Queue, NpgsqlDbType = NpgsqlDbType.Text });
                    c.Parameters.Add(new NpgsqlParameter { Value = Inner.Options.VisibilityTimeoutSeconds, NpgsqlDbType = NpgsqlDbType.Integer });
                    c.Parameters.Add(new NpgsqlParameter { Value = qty, NpgsqlDbType = NpgsqlDbType.Integer });
                    batch.BatchCommands.Add(c);
                }
                await using var rd = await batch.ExecuteReaderAsync(ct);
                long now = Clock.NowUs();
                for (int i = 0; i < chunk.Count; i++)
                {
                    var list = got[i] = new List<PgmqBroker.PgmqDelivery>();
                    while (await rd.ReadAsync(ct))
                        list.Add(new PgmqBroker.PgmqDelivery(Inner, chunk[i].G.Queue, rd.GetInt64(0), rd.GetInt32(1),
                            Clock.ToUs(rd.GetFieldValue<DateTime>(2)), EventEnvelope.FromJson(rd.GetString(3)), now));
                    list.Sort(static (a, b) => a.MsgId.CompareTo(b.MsgId));
                    if (i < chunk.Count - 1) await rd.NextResultAsync(ct);
                }
            }
            if (_d.PreloadContext) await PreloadAsync(conn, got, ct);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            _log($"pgdispatch fetch error: {e.GetType().Name}: {e.Message}");
        }
        catch (OperationCanceledException) { }

        long done = Clock.NowUs();
        for (int i = 0; i < chunk.Count; i++)
        {
            var (g, qty) = chunk[i];
            var list = got[i];
            Interlocked.Increment(ref _groupReads);
            if (list is null || list.Count == 0) Interlocked.Increment(ref _emptyGroupReads);
            else Interlocked.Add(ref _rows, list.Count);
            List<(Waiter W, List<IDelivery> Items)>? fulfil = null;
            lock (g.Gate)
            {
                g.Fetching = false;
                g.LastFetchUs = done;
                if (list is { Count: > 0 })
                {
                    foreach (var d in list) g.Ready.Enqueue(new Delivery(this, g, d));
                    if (list.Count >= qty) g.Dirty = true; // probably more where these came from
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

    private async Task PreloadAsync(NpgsqlConnection conn, List<PgmqBroker.PgmqDelivery>[] got, CancellationToken ct)
    {
        var all = got.Where(l => l != null).SelectMany(l => l).ToList();
        if (all.Count == 0) return;
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

    public IEventPublisher CreatePublisher(int groupId) => Inner.CreatePublisher(groupId);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, _groups[groupId]);

    public Task<long> GetBacklogAsync(CancellationToken ct) => Inner.GetBacklogAsync(ct);

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var r = new Dictionary<string, double>(await ((IStatsProvider)Inner).SampleAsync(ct));
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
        int waiting = 0;
        foreach (var g in _groups) lock (g.Gate) waiting += g.Waiters.Count;
        r["dispatch_waiting_receives"] = waiting;
        r["dispatch_preload_cache"] = PreloadedContexts.Count;
        return r;
    }

    public string Describe()
        => $"pgdispatch over {Inner.Describe()}; notify={_d.Notify}, fallback {_d.FallbackPollMs} ms, {_d.MaxGroupsPerFetch} groups/fetch x {_d.FetchParallelism}, preload={_d.PreloadContext}";

    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        try { if (_loop != null) await _loop; } catch { /* shutting down */ }
        // Stop the LISTEN loop first: closing a connection inside WaitAsync throws.
        try { if (_listenTask != null) await _listenTask; } catch { /* shutting down */ }
        if (_listen != null)
        {
            try { await _listen.CloseAsync(); } catch { /* best effort */ }
            try { await _listen.DisposeAsync(); } catch { /* best effort */ }
        }
        await Inner.DisposeAsync();
    }

    // ------------------------------------------------------------------------------------------------

    private sealed class Consumer(PgDispatchBroker broker, GroupState g) : IEventConsumer
    {
        public bool SharedSources => true;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            Waiter w;
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
                timeout.CancelAfter(broker._d.ReceiveWaitMs);
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

    private sealed class Delivery(PgDispatchBroker broker, GroupState g, PgmqBroker.PgmqDelivery inner)
        : IDelivery, IPgmqDelivery, IDeliveryCompletion
    {
        public EventEnvelope Event => inner.Event;
        public long ReceivedAtUs => inner.ReceivedAtUs;
        public long EnqueuedAtUs => inner.EnqueuedAtUs;
        public int DeliveryCount => inner.DeliveryCount;
        public string Queue => inner.Queue;
        public long MsgId => inner.MsgId;
        public bool Archive => inner.Archive;

        public ValueTask AckAsync(CancellationToken ct) => inner.AckAsync(ct);

        public async ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            await inner.NackAsync(delay, ct);
            // The message reappears after vt: look again then.
            _ = Task.Delay(delay + TimeSpan.FromMilliseconds(5), CancellationToken.None).ContinueWith(_ => broker.MarkDirty(g), TaskScheduler.Default);
        }

        // The source's next message may now be its head: read the group again soon.
        public void Completed(bool success)
        {
            Interlocked.Increment(ref broker._completions);
            if (success) broker.MarkDirty(g);
        }
    }
}

public sealed class PgDispatchBrokerFactory : IBrokerFactory
{
    public string Type => "pgdispatch";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new PgDispatchBroker(section.Get<PgmqOptions>() ?? new(), section.GetSection("Dispatch").Get<PgDispatchOptions>() ?? new(),
            context.Topology, context.Log);
}
