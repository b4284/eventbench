using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using EventBench.Dispatch.Proto;
using EventBench.Postgres;
using Google.Protobuf;
using Npgsql;

namespace EventBench.Dispatch;

/// <summary>
/// One dispatcher instance: a separate process between Postgres and the workers. It owns a share of the groups
/// (see <see cref="LeaseManager"/>), reads their heads (pgqueue: LISTEN, batched head reads, preloaded state),
/// pushes them to workers over TCP against credits, and runs the workers' commits as group commits (state change
/// + queue delete in one transaction). Workers hold no database connection.
/// </summary>
public sealed class DispatcherNode : IAsyncDisposable
{
    private readonly DispatcherOptions _o;
    private readonly Action<string> _log;
    private readonly ConcurrentDictionary<ServerSession, byte> _sessions = new();
    private readonly LeaseManager _leases;
    private readonly string _addr;
    private TcpListener? _listener;
    internal long Commits, CommitErrors, Delivered, Loads;

    internal PgQueueBroker Broker { get; }
    internal PostgresStore Store { get; }
    internal Action<string> Log => _log;
    public string Id => _o.Id;

    public DispatcherNode(DispatcherOptions o, PgQueueOptions queue, PostgresStoreOptions store, Topology topology, Action<string> log)
    {
        _o = o;
        _log = log;
        queue.Dispatch = true;
        queue.SampleBacklog = false;
        Broker = new PgQueueBroker(queue, topology, log);
        Broker.EnableOwnership();
        store.BatchCommits = true;
        Store = new PostgresStore(store, topology, log);
        _addr = o.Advertise ?? $"127.0.0.1:{o.Port}";
        _leases = new LeaseManager(o, Broker, o.LeaseConnectionString ?? queue.AdminConnectionString ?? queue.ConnectionString, _addr, log);
    }

    public async Task RunAsync(CancellationToken ct)
    {
        await _leases.EnsureSchemaAsync(ct);
        await Broker.StartDispatchAsync(ct);
        _listener = new TcpListener(IPAddress.Any, _o.Port);
        _listener.Start(512);
        _log($"dispatcher {_o.Id} listening on {_addr}; lease ttl {_o.LeaseTtlMs} ms, tick {_o.TickMs} ms, drain {_o.DrainMs} ms");
        var leaseTask = Task.Run(() => _leases.RunAsync(ct), CancellationToken.None);
        var sessionTasks = new List<Task>();
        try
        {
            while (!ct.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync(ct);
                client.NoDelay = true;
                var s = new ServerSession(this, client);
                _sessions[s] = 0;
                sessionTasks.Add(Task.Run(async () =>
                {
                    try { await s.RunAsync(ct); }
                    finally { _sessions.TryRemove(s, out _); }
                }, CancellationToken.None));
                sessionTasks.RemoveAll(t => t.IsCompleted);
            }
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
        finally
        {
            _listener.Stop();
            await _leases.LeaveAsync();
            try { await leaseTask; } catch { /* stopping */ }
            foreach (var s in _sessions.Keys) s.Abort();
            try { await Task.WhenAll(sessionTasks).WaitAsync(TimeSpan.FromSeconds(5)); } catch { /* stopping */ }
        }
    }

    internal Task<List<(int Group, string Member, string Addr)>> RoutesAsync(CancellationToken ct) => _leases.RoutesAsync(ct);

    internal void Reset()
    {
        Broker.ResetAll();
        foreach (var s in _sessions.Keys) s.ForgetHanded();
        _log("reset: queue state cleared for a new run");
    }

    internal Dictionary<string, double> Stats()
    {
        var p = Process.GetCurrentProcess();
        var c = Broker.Counters;
        var l = _leases.Counters;
        int inflight = 0;
        for (int g = 0; g < Broker.GroupCount; g++) inflight += Broker.InFlightCount(g);
        return new Dictionary<string, double>
        {
            ["cpu_seconds"] = p.TotalProcessorTime.TotalSeconds,
            ["mem_mb"] = Math.Round(p.WorkingSet64 / 1048576.0, 1),
            ["groups"] = _leases.OwnedCount,
            ["draining"] = _leases.DrainingCount,
            ["members"] = _leases.LiveMembers,
            ["sessions"] = _sessions.Count,
            ["in_flight"] = inflight,
            ["fetch_calls"] = c.FetchCalls,
            ["rows"] = c.Rows,
            ["notifies"] = c.Notifies,
            ["delivered"] = Interlocked.Read(ref Delivered),
            ["commits"] = Interlocked.Read(ref Commits),
            ["commit_errors"] = Interlocked.Read(ref CommitErrors),
            ["commit_batches"] = Store.Batches,
            ["batched_commits"] = Store.BatchedCommits,
            ["loads"] = Interlocked.Read(ref Loads),
            ["claims"] = l.Claims,
            ["releases"] = l.Releases,
            ["leases_lost"] = l.Lost,
            ["lease_errors"] = l.TickErrors,
        };
    }

    public async ValueTask DisposeAsync()
    {
        await Broker.DisposeAsync();
        await Store.DisposeAsync();
        await _leases.DisposeAsync();
    }
}

/// <summary>Stand-in delivery for a commit arriving over the wire: identifies the queue row to delete in the transaction.</summary>
internal sealed class WireAck(int group, long msgId) : IDelivery, IPgQueueDelivery
{
    public int GroupId => group;
    public long MsgId => msgId;
    public byte[] Body => [];
    public EventEnvelope Event => throw new NotSupportedException();
    public long ReceivedAtUs => 0;
    public long EnqueuedAtUs => 0;
    public int DeliveryCount => 1;
    public ValueTask AckAsync(CancellationToken ct) => throw new NotSupportedException();
    public ValueTask NackAsync(TimeSpan delay, CancellationToken ct) => throw new NotSupportedException();
}

/// <summary>One worker-host connection: credits per group, pushes, commits, loads and calls.</summary>
internal sealed class ServerSession
{
    private readonly DispatcherNode _node;
    private readonly TcpClient _client;
    private readonly NetworkStream _stream;
    private readonly Channel<object> _out = Channel.CreateUnbounded<object>(new UnboundedChannelOptions { SingleReader = true });
    private readonly CancellationTokenSource _cts = new();
    private readonly int[] _credit;
    private readonly int[] _pumping;
    private readonly ConcurrentDictionary<long, int> _handed = new();   // msg id -> group, not yet released
    private readonly ConcurrentDictionary<Task, byte> _pumps = new();

    public ServerSession(DispatcherNode node, TcpClient client)
    {
        _node = node;
        _client = client;
        _stream = client.GetStream();
        _credit = new int[node.Broker.GroupCount];
        _pumping = new int[node.Broker.GroupCount];
    }

    public void Abort() { try { _cts.Cancel(); _client.Close(); } catch { /* closing */ } }

    public void ForgetHanded() => _handed.Clear();

    public async Task RunAsync(CancellationToken outer)
    {
        using var link = CancellationTokenSource.CreateLinkedTokenSource(outer, _cts.Token);
        var ct = link.Token;
        var writer = Task.Run(() => Framing.WriteLoopAsync<object, ServerMsg>(_out.Reader, _stream, () => new ServerMsg(), Add, 1 << 20, ct), CancellationToken.None);
        var header = new byte[4];
        try
        {
            while (!ct.IsCancellationRequested)
            {
                var m = await Framing.ReadAsync(_stream, ClientMsg.Parser, header, ct);
                if (m is null) break;
                Handle(m, ct);
            }
        }
        catch (Exception e) when (e is OperationCanceledException or IOException or SocketException or ObjectDisposedException) { }
        catch (Exception e) { _node.Log($"session error: {e.GetType().Name}: {e.Message}"); }
        finally
        {
            _cts.Cancel();
            _out.Writer.TryComplete();
            try { await Task.WhenAll(_pumps.Keys); } catch { /* cancelled */ }
            try { await writer; } catch { /* closed */ }
            // Whatever this worker host held and did not finish goes back to the queue at once.
            int n = 0;
            foreach (var (msgId, g) in _handed) { _node.Broker.Release(g, msgId); n++; }
            _handed.Clear();
            if (n > 0) _node.Log($"session closed: released {n} unfinished messages");
            _client.Dispose();
        }
    }

    private static int Add(ServerMsg msg, object item)
    {
        switch (item)
        {
            case Delivery d: msg.Deliveries.Add(d); return d.Body.Length + 48;
            case CommitRes c: msg.CommitResults.Add(c); return 16;
            case LoadRes l: msg.LoadResults.Add(l); return 64 + (l.Ctx?.Context?.Length ?? 0);
            case int g: msg.NotOwner.Add(g); return 4;
            case CallRes r: msg.CallResults.Add(r); return 64 + r.Routes.Count * 24;
            default: return 0;
        }
    }

    private void Send(object item) => _out.Writer.TryWrite(item);

    private void Handle(ClientMsg m, CancellationToken ct)
    {
        foreach (var d in m.Demands) AddCredit(d.Group, d.Count, ct);
        foreach (var c in m.Commits) _ = CommitAsync(c, ct);
        foreach (var a in m.Acks) _ = AckAsync(a, ct);
        foreach (var n in m.Nacks)
        {
            _handed.TryRemove(n.MsgId, out _);
            _node.Broker.Release(n.Group, n.MsgId, n.DelayMs);
        }
        foreach (var d in m.Dones)
        {
            _handed.TryRemove(d.MsgId, out _);
            _node.Broker.Release(d.Group, d.MsgId);
        }
        foreach (var l in m.Loads) _ = LoadAsync(l, ct);
        foreach (var c in m.Calls) _ = CallAsync(c, ct);
    }

    // ---- credits and pushes ----

    private void AddCredit(int group, int n, CancellationToken ct)
    {
        if ((uint)group >= (uint)_credit.Length || n <= 0) return;
        Interlocked.Add(ref _credit[group], n);
        if (Interlocked.CompareExchange(ref _pumping[group], 1, 0) == 0) StartPump(group, ct);
    }

    private void StartPump(int group, CancellationToken ct)
    {
        var t = Task.Run(() => PumpAsync(group, ct), CancellationToken.None);
        _pumps[t] = 0;
        t.ContinueWith(x => _pumps.TryRemove(x, out _), TaskScheduler.Default);
    }

    /// <summary>While the worker host has credit for the group, wait for its heads and push them.</summary>
    private async Task PumpAsync(int group, CancellationToken ct)
    {
        var consumer = _node.Broker.CreateConsumer(group, 0);
        try
        {
            while (!ct.IsCancellationRequested)
            {
                int credit = Volatile.Read(ref _credit[group]);
                if (credit <= 0)
                {
                    Volatile.Write(ref _pumping[group], 0);
                    // a credit may have arrived between the read and the flag reset
                    if (Volatile.Read(ref _credit[group]) > 0 && Interlocked.CompareExchange(ref _pumping[group], 1, 0) == 0) continue;
                    return;
                }
                if (!_node.Broker.IsServing(group))
                {
                    Interlocked.Exchange(ref _credit[group], 0);
                    Send(group); // NotOwner: the worker host re-routes its demand
                    continue;
                }
                var items = await consumer.ReceiveAsync(credit, ct);
                if (items.Count == 0) continue;
                Interlocked.Add(ref _credit[group], -items.Count);
                Interlocked.Add(ref _node.Delivered, items.Count);
                foreach (var d in items)
                {
                    var q = (IPgQueueDelivery)d;
                    _handed[q.MsgId] = group;
                    var wire = new Delivery { Group = group, MsgId = q.MsgId, EnqueuedUs = d.EnqueuedAtUs, Body = ByteString.CopyFrom(q.Body) };
                    if (PreloadedContexts.TryTake(d.Event.SourceId, d.Event.Seq, out var ctx)) wire.Ctx = ToWire(ctx);
                    Send(wire);
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception e) { _node.Log($"pump {group} failed: {e.Message}"); }
        finally { if (ct.IsCancellationRequested) Volatile.Write(ref _pumping[group], 0); }
    }

    internal static Ctx ToWire(WorkContext c)
    {
        var w = new Ctx { LastSeq = c.LastSeq, RecordExists = c.RecordExists, Version = c.Version, Hash = c.Hash, Status = (int)c.Status };
        if (c.Context != null) w.Context = ByteString.CopyFrom(c.Context);
        return w;
    }

    // ---- commits, acks, loads ----

    private async Task CommitAsync(CommitReq c, CancellationToken ct)
    {
        var req = new CommitRequest
        {
            Event = new EventEnvelope { Group = c.Group, SourceId = c.SourceId, Seq = c.Seq, RecordId = c.RecordId },
            Kind = (MutationKind)c.Kind,
            PrevVersion = c.PrevVersion,
            NewVersion = c.NewVersion,
            NewHash = c.NewHash,
            NewStatus = (RecordStatus)c.NewStatus,
            NewContext = c.HasNewContext ? c.NewContext.ToByteArray() : null,
            Upload = c.HasUpload ? new UploadInfo(c.UploadSize, c.UploadChecksum, c.HasUploadData ? c.UploadData.ToByteArray() : null) : null,
            TransactionalAck = c.HasAck ? new WireAck(c.Group, c.MsgId) : null,
        };
        try
        {
            var st = await _node.Store.CommitAsync(req, ct);
            Interlocked.Increment(ref _node.Commits);
            if (c.HasAck && st is CommitStatus.Applied or CommitStatus.Duplicate)
            {
                // The row is gone (same transaction): the source's next message is now its head.
                _handed.TryRemove(c.MsgId, out _);
                _node.Broker.Release(c.Group, c.MsgId);
            }
            Send(new CommitRes { ReqId = c.ReqId, Status = (int)st });
        }
        catch (Exception e)
        {
            Interlocked.Increment(ref _node.CommitErrors);
            Send(new CommitRes { ReqId = c.ReqId, Error = $"{e.GetType().Name}: {e.Message}" });
        }
    }

    private async Task AckAsync(MsgRef a, CancellationToken ct)
    {
        try { await _node.Broker.DeleteAsync(a.Group, a.MsgId, ct); }
        catch (Exception e) when (e is not OperationCanceledException) { _node.Log($"ack delete failed: {e.Message}"); }
        _handed.TryRemove(a.MsgId, out _);
        _node.Broker.Release(a.Group, a.MsgId);
    }

    private async Task LoadAsync(LoadReq l, CancellationToken ct)
    {
        Interlocked.Increment(ref _node.Loads);
        try
        {
            var ctx = await _node.Store.LoadAsync(new EventEnvelope { Group = l.Group, SourceId = l.SourceId, Seq = l.Seq, RecordId = l.RecordId }, ct);
            Send(new LoadRes { ReqId = l.ReqId, Ctx = ToWire(ctx) });
        }
        catch (Exception e) { Send(new LoadRes { ReqId = l.ReqId, Error = $"{e.GetType().Name}: {e.Message}" }); }
    }

    private async Task CallAsync(Call c, CancellationToken ct)
    {
        var r = new CallRes { ReqId = c.ReqId, MemberId = _node.Id };
        try
        {
            switch (c.Kind)
            {
                case CallKind.CallRoutes:
                    foreach (var (g, m, a) in await _node.RoutesAsync(ct)) r.Routes.Add(new Route { Group = g, MemberId = m, Addr = a });
                    break;
                case CallKind.CallReset:
                    _node.Reset();
                    break;
                case CallKind.CallStats:
                    foreach (var kv in _node.Stats()) r.Stats[kv.Key] = kv.Value;
                    break;
            }
        }
        catch (Exception e) { r.Error = $"{e.GetType().Name}: {e.Message}"; }
        Send(r);
    }
}
