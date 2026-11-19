using EventBench.Core.Abstractions;
using EventBench.Core.Config;
using EventBench.Core.Metrics;
using EventBench.Core.Model;
using EventBench.Core.Util;

namespace EventBench.Core.Processing;

/// <summary>
/// One stateless worker of a given group (worker type). Loop: receive -> for each event:
/// load context from the store -> process -> commit result -> ack. Events of the same source in a
/// batch are processed sequentially; different sources may run in parallel (MaxParallelPerWorker).
/// </summary>
public sealed class WorkerRunner
{
    private readonly GroupInfo _group;
    private readonly int _index;
    private readonly IEventConsumer _consumer;
    private readonly IStateStore _store;
    private readonly WorkerOptions _options;
    private readonly WorkloadOptions _workload;
    private readonly MetricsHub _metrics;
    private readonly bool _txAck;
    private readonly Action<string> _log;
    private readonly TimeSpan _retryDelay;
    private readonly bool _pipelined;

    public WorkerRunner(GroupInfo group, int index, IEventConsumer consumer, IStateStore store,
        BenchConfig config, MetricsHub metrics, bool txAck, Action<string> log)
    {
        _group = group;
        _index = index;
        _consumer = consumer;
        _store = store;
        _options = config.Worker;
        _workload = config.Workload;
        _metrics = metrics;
        _txAck = txAck;
        _log = log;
        _retryDelay = TimeSpan.FromMilliseconds(_options.RetryDelayMs);
        _pipelined = _options.Mode == WorkerMode.Pipelined;
    }

    public Task RunAsync(CancellationToken ct) => _pipelined ? RunPipelinedAsync(ct) : RunBatchAsync(ct);

    private async Task RunBatchAsync(CancellationToken ct)
    {
        double backoff = _options.IdleBackoffMinMs;
        int consecutiveErrors = 0;
        while (!ct.IsCancellationRequested)
        {
            IReadOnlyList<IDelivery> batch;
            try
            {
                long t0 = Clock.NowUs();
                batch = await _consumer.ReceiveAsync(_options.BatchSize, ct);
                if (batch.Count > 0) _metrics.Receive.Record(Clock.NowUs() - t0);
                consecutiveErrors = 0;
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { break; }
            catch (Exception ex)
            {
                _metrics.OnError(_group.Id);
                _metrics.ErrorSample($"receive {_group.Name}#{_index}: {ex.GetType().Name}: {ex.Message}");
                if (++consecutiveErrors <= 3 || consecutiveErrors % 100 == 0)
                    _log($"worker {_group.Name}#{_index} receive error ({consecutiveErrors}): {ex.GetType().Name}: {ex.Message}");
                await SafeDelay(Math.Min(1000, 10 * consecutiveErrors), ct);
                continue;
            }

            if (batch.Count == 0)
            {
                await SafeDelay(backoff, ct);
                backoff = Math.Min(_options.IdleBackoffMaxMs, Math.Max(backoff * 2, 0.5));
                continue;
            }
            backoff = _options.IdleBackoffMinMs;

            try
            {
                await ProcessBatchAsync(batch, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { break; }
        }
    }

    private async Task ProcessBatchAsync(IReadOnlyList<IDelivery> batch, CancellationToken ct)
    {
        if (batch.Count == 1)
        {
            await ProcessOneAsync(batch[0], ct);
            return;
        }

        // Group by source preserving arrival order (the broker returns per-source messages in order).
        var lanes = new List<List<IDelivery>>();
        var bySource = new Dictionary<int, List<IDelivery>>();
        foreach (var d in batch)
        {
            if (!bySource.TryGetValue(d.Event.SourceId, out var lane))
            {
                lane = new List<IDelivery>(2);
                bySource[d.Event.SourceId] = lane;
                lanes.Add(lane);
            }
            lane.Add(d);
        }

        if (_options.MaxParallelPerWorker <= 1 || lanes.Count == 1)
        {
            foreach (var lane in lanes) await ProcessLaneAsync(lane, ct);
        }
        else
        {
            await Parallel.ForEachAsync(lanes,
                new ParallelOptions { MaxDegreeOfParallelism = _options.MaxParallelPerWorker, CancellationToken = ct },
                async (lane, token) => await ProcessLaneAsync(lane, token));
        }
    }

    private async Task ProcessLaneAsync(List<IDelivery> lane, CancellationToken ct)
    {
        for (int i = 0; i < lane.Count; i++)
        {
            if (await ProcessOneAsync(lane[i], ct)) continue;
            // A failure must not let later events of the same source overtake it.
            for (int j = i + 1; j < lane.Count; j++) await NackAsync(lane[j], ct);
            return;
        }
    }

    private enum Outcome { Applied, Skipped, Failed }

    /// <returns>true when the event was applied or safely skipped (duplicate).</returns>
    private async Task<bool> ProcessOneAsync(IDelivery d, CancellationToken ct)
    {
        var ev = d.Event;
        var blocking = _metrics.Blocking;
        long startUs = Clock.NowUs();
        long readyUs = Math.Min(blocking.ReadyAt(ev), startUs);
        long idleUs = blocking.IdleOverlap(ev.Group, readyUs, startUs);
        blocking.SourceStart(ev);
        blocking.SlotBusy(ev.Group);
        Outcome outcome = Outcome.Failed;
        try
        {
            outcome = await ProcessCoreAsync(d, ct);
            if (outcome == Outcome.Applied) _metrics.RecordBlocking(ev, readyUs, startUs, idleUs);
            return outcome != Outcome.Failed;
        }
        finally
        {
            blocking.SlotFree(ev.Group);
            long now = Clock.NowUs();
            if (outcome == Outcome.Failed) blocking.SourceRetry(ev, now + (long)(_retryDelay.TotalMilliseconds * 1000));
            else blocking.SourceDone(ev, now);
        }
    }

    private async Task<Outcome> ProcessCoreAsync(IDelivery d, CancellationToken ct)
    {
        var ev = d.Event;
        try
        {
            long t0 = Clock.NowUs();
            var ctx = await _store.LoadAsync(ev, ct);
            long t1 = Clock.NowUs();

            if (ctx.LastSeq >= ev.Seq)
            {
                _metrics.OnDuplicate(ev.Group);
                await d.AckAsync(ct);
                return Outcome.Skipped;
            }
            if (ctx.LastSeq != ev.Seq - 1)
            {
                _metrics.OnGap(ev, ctx.LastSeq);
                await NackAsync(d, ct);
                return Outcome.Failed;
            }

            var request = await ComputeAsync(ev, ctx, d, ct);
            long t2 = Clock.NowUs();

            if (_options.ChaosFailProbability > 0 && Random.Shared.NextDouble() < _options.ChaosFailProbability)
            {
                // Injected worker failure before commit: exercises the broker's nack/redelivery path,
                // which must not let later events of the source overtake this one.
                await NackAsync(d, ct);
                return Outcome.Failed;
            }

            var status = await _store.CommitAsync(request, ct);
            if (status == CommitStatus.RecordConflict)
            {
                // The record row changed underneath us although the cursor matched: lost update / corruption.
                _metrics.OnStateMismatch(ev, "record_conflict", "conditional record write matched 0 rows");
                status = await _store.CommitAsync(new CommitRequest
                {
                    Event = ev, Kind = MutationKind.None, TransactionalAck = request.TransactionalAck,
                }, ct);
            }
            long t3 = Clock.NowUs();

            switch (status)
            {
                case CommitStatus.Applied:
                {
                    _metrics.OnApplied(ev);
                    long ackUs = 0;
                    if (!_txAck)
                    {
                        try
                        {
                            await d.AckAsync(ct);
                        }
                        catch (Exception ex) when (ex is not OperationCanceledException)
                        {
                            // State is committed; the message will be redelivered and skipped as a duplicate.
                            _metrics.OnError(ev.Group);
                            _metrics.ErrorSample($"ack failed {ev}: {ex.Message}");
                            return Outcome.Applied;
                        }
                        ackUs = Clock.NowUs() - t3;
                    }
                    long done = Clock.NowUs();
                    _metrics.RecordLatencies(ev, d.ReceivedAtUs, d.EnqueuedAtUs, done, t1 - t0, t2 - t1, t3 - t2, ackUs);
                    return Outcome.Applied;
                }
                case CommitStatus.Duplicate:
                    _metrics.OnDuplicate(ev.Group);
                    if (!_txAck) await d.AckAsync(ct);
                    return Outcome.Skipped;
                case CommitStatus.Gap:
                    _metrics.OnGap(ev, -1);
                    await NackAsync(d, ct);
                    return Outcome.Failed;
                default:
                    _metrics.OnError(ev.Group);
                    await NackAsync(d, ct);
                    return Outcome.Failed;
            }
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _metrics.OnError(ev.Group);
            _metrics.ErrorSample($"{_group.Name}#{_index} {ev}: {ex.GetType().Name}: {ex.Message}");
            await NackAsync(d, ct);
            return Outcome.Failed;
        }
    }

    private async Task<CommitRequest> ComputeAsync(EventEnvelope ev, WorkContext ctx, IDelivery d, CancellationToken ct)
    {
        // Verify the payload actually arrived intact (inline or via claim check).
        byte[]? payload = ev.Payload;
        if (payload is null && ev.BlobId is { } blobId)
        {
            payload = await _store.GetBlobAsync(blobId, ct);
            if (payload is null)
                _metrics.OnStateMismatch(ev, "blob_missing", $"claim-check blob {blobId} not found");
        }
        long checksum = payload is null ? 0 : Hashing.Checksum(payload);
        if (checksum != ev.PayloadChecksum || (payload?.Length ?? 0) != ev.PayloadSize)
            _metrics.OnStateMismatch(ev, "payload_corrupt", $"payload checksum/size mismatch (size {payload?.Length ?? 0} vs {ev.PayloadSize})");

        // Verify the pre-state matches what the generator expected: detects reordering / lost updates immediately.
        if (ev.Op == OpType.Create)
        {
            if (ctx.RecordExists) _metrics.OnStateMismatch(ev, "pre_state", "record already exists before Create");
        }
        else if (!ctx.RecordExists)
        {
            _metrics.OnStateMismatch(ev, "pre_state", "record missing");
        }
        else
        {
            if (ctx.Version != ev.ExpectedVersion || ctx.Hash != ev.ExpectedHash)
                _metrics.OnStateMismatch(ev, "pre_state", $"version {ctx.Version}/{ev.ExpectedVersion} hash {ctx.Hash:x}/{ev.ExpectedHash:x}");
            if (ctx.Status != RecordStatus.Open)
                _metrics.OnStateMismatch(ev, "pre_state", $"record status {ctx.Status}");
            if (ctx.Context != null && !Hashing.ContextMatches(ctx.Context, ctx.Hash))
                _metrics.OnStateMismatch(ev, "context_corrupt", "context blob does not match record hash");
        }

        Clock.Spin(_options.SimulatedWorkMicros);
        // Slow external call (e.g. a downstream service): holds the slot, burns no CPU.
        if (ev.SlowMs > 0) await Task.Delay(ev.SlowMs, ct);

        // Compute the new state from what was *loaded* (not from the event's expectation).
        MutationKind kind = MutationKind.None;
        int newVersion = ctx.Version;
        long newHash = ctx.Hash;
        var newStatus = ctx.Status;
        switch (ev.Op)
        {
            case OpType.Create when !ctx.RecordExists:
                (newVersion, newHash) = StateMachine.Create(ev.RecordId, ev.Seq, ev.Delta, checksum);
                newStatus = RecordStatus.Open;
                kind = MutationKind.Insert;
                break;
            case OpType.Update when ctx.RecordExists:
                (newVersion, newHash) = StateMachine.Update(ctx.Version, ctx.Hash, ev.Seq, ev.Delta, checksum);
                kind = MutationKind.Update;
                break;
            case OpType.Complete when ctx.RecordExists:
                (newVersion, newHash) = StateMachine.Complete(ctx.Version, ctx.Hash, ev.Seq);
                newStatus = RecordStatus.Completed;
                kind = MutationKind.Update;
                break;
        }

        byte[]? newContext = null;
        if (kind != MutationKind.None && _workload.ContextBytes > 0)
        {
            newContext = new byte[_workload.ContextBytes];
            Hashing.FillContext(newContext, newHash);
        }

        UploadInfo? upload = null;
        if (ev.IsLarge)
            upload = new UploadInfo(payload?.Length ?? 0, checksum, ev.BlobId is null ? payload : null);

        return new CommitRequest
        {
            Event = ev,
            Kind = kind,
            PrevVersion = ctx.Version,
            NewVersion = newVersion,
            NewHash = newHash,
            NewStatus = newStatus,
            NewContext = newContext,
            Upload = upload,
            TransactionalAck = _txAck ? d : null,
        };
    }

    private async Task NackAsync(IDelivery d, CancellationToken ct)
    {
        _metrics.OnNack(d.Event.Group);
        // Pipelined mode retries locally (the lane keeps the delivery and its source's later events).
        if (_pipelined) return;
        try { await d.NackAsync(_retryDelay, ct); }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
        catch (Exception ex)
        {
            // The visibility timeout will eventually redeliver it anyway.
            _metrics.OnError(d.Event.Group);
            _metrics.ErrorSample($"nack failed: {ex.Message}");
        }
    }

    // ---------------------------------------------------------------------------------------------
    // Pipelined mode
    // ---------------------------------------------------------------------------------------------

    private readonly object _gate = new();
    private readonly Dictionary<int, Queue<IDelivery>> _lanes = new();   // source -> its events here, head first
    private readonly Queue<int> _ready = new();                          // sources waiting for a free slot
    private readonly Dictionary<int, int> _retries = new();              // source -> consecutive local retries
    private readonly SemaphoreSlim _slotFreed = new(0);
    private int _free;
    private int _parked;
    private int _running;
    private bool _sharedSources;
    private ISourceLinger? _linger;

    /// <summary>
    /// MaxParallelPerWorker slots. The receive loop asks for at most as many messages as there are free
    /// slots (never hoards work another worker could take). An event whose source already has an event
    /// here is parked behind it; everything else starts at once. When a slot finishes an event it continues
    /// with that source's next parked event, or hands the slot to the oldest waiting source. A failure frees
    /// the slot and re-queues the source after the retry delay, so one slow or failing event never holds up
    /// other sources.
    /// </summary>
    private async Task RunPipelinedAsync(CancellationToken ct)
    {
        _consumer.EnableReadAhead();
        _sharedSources = _consumer.SharedSources;
        _linger = _options.Linger ? _consumer as ISourceLinger : null;
        if (_options.Linger && _linger is null && _index == 0)
            _log($"worker {_group.Name}: Linger requested but the consumer does not support it (ignored)");
        _free = Math.Max(1, _options.MaxParallelPerWorker);
        double backoff = _options.IdleBackoffMinMs;
        int consecutiveErrors = 0;
        while (!ct.IsCancellationRequested)
        {
            int want;
            lock (_gate) want = _parked >= _options.ReadAheadLimit ? 0 : Math.Min(_free, _options.BatchSize);
            if (want <= 0)
            {
                try { await _slotFreed.WaitAsync(TimeSpan.FromMilliseconds(50), ct); }
                catch (OperationCanceledException) { break; }
                continue;
            }

            IReadOnlyList<IDelivery> batch;
            try
            {
                long t0 = Clock.NowUs();
                batch = await _consumer.ReceiveAsync(want, ct);
                if (batch.Count > 0) _metrics.Receive.Record(Clock.NowUs() - t0);
                consecutiveErrors = 0;
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { break; }
            catch (Exception ex)
            {
                _metrics.OnError(_group.Id);
                _metrics.ErrorSample($"receive {_group.Name}#{_index}: {ex.GetType().Name}: {ex.Message}");
                if (++consecutiveErrors <= 3 || consecutiveErrors % 100 == 0)
                    _log($"worker {_group.Name}#{_index} receive error ({consecutiveErrors}): {ex.GetType().Name}: {ex.Message}");
                await SafeDelay(Math.Min(1000, 10 * consecutiveErrors), ct);
                continue;
            }

            if (batch.Count == 0)
            {
                await SafeDelay(backoff, ct);
                backoff = Math.Min(_options.IdleBackoffMaxMs, Math.Max(backoff * 2, 0.5));
                continue;
            }
            backoff = _options.IdleBackoffMinMs;

            lock (_gate)
            {
                foreach (var d in batch)
                {
                    int src = d.Event.SourceId;
                    if (_lanes.TryGetValue(src, out var lane))
                    {
                        // An empty lane is a slot lingering on this source: the event becomes its head.
                        if (lane.Count > 0) _parked++;
                        lane.Enqueue(d);
                    }
                    else
                    {
                        lane = new Queue<IDelivery>(2);
                        lane.Enqueue(d);
                        _lanes[src] = lane;
                        _ready.Enqueue(src);
                    }
                }
                DispatchLocked(ct);
            }
        }

        // Let in-flight events finish (or observe the cancellation).
        for (int i = 0; i < 300 && Volatile.Read(ref _running) > 0; i++) await Task.Delay(100, CancellationToken.None);
    }

    private void DispatchLocked(CancellationToken ct)
    {
        while (_free > 0 && _ready.Count > 0)
        {
            int src = _ready.Dequeue();
            _free--;
            Interlocked.Increment(ref _running);
            _ = Task.Run(() => RunLaneAsync(src, ct), CancellationToken.None);
        }
    }

    private async Task RunLaneAsync(int src, CancellationToken ct)
    {
        int lingers = 0;
        try
        {
            while (!ct.IsCancellationRequested)
            {
                IDelivery d;
                lock (_gate) d = _lanes[src].Peek();
                bool ok;
                try { ok = await ProcessOneAsync(d, ct); }
                catch (OperationCanceledException) { return; }

                lock (_gate)
                {
                    var lane = _lanes[src];
                    if (!ok)
                    {
                        int n = _retries[src] = _retries.GetValueOrDefault(src) + 1;
                        if (_sharedSources || n > _options.MaxLocalRetries)
                        {
                            // Shared queue: any free worker should take the retry. Or give up locally after
                            // too many retries. Either way the whole lane goes back to the broker, oldest first.
                            _lanes.Remove(src);
                            _retries.Remove(src);
                            _parked -= lane.Count - 1;
                            var giveBack = lane.ToArray();
                            ReleaseSlotLocked(ct);
                            _ = GiveBackAsync(giveBack, ct);
                            return;
                        }
                        // Free the slot; the source rejoins the ready queue after the retry delay.
                        ReleaseSlotLocked(ct);
                        _ = RequeueLaterAsync(src, ct);
                        return;
                    }
                    _retries.Remove(src);
                    lane.Dequeue();
                    if (lane.Count == 0)
                    {
                        if (_linger is null || lingers >= _options.LingerMax || _ready.Count > 0)
                        {
                            _lanes.Remove(src);
                            ReleaseSlotLocked(ct);
                            return;
                        }
                        // Linger: keep the (empty) lane and the slot while asking for the source's next event.
                    }
                    else _parked--;
                    if (lane.Count > 0 && _ready.Count > 0)
                    {
                        // Others are waiting for a slot: take turns.
                        _ready.Enqueue(src);
                        ReleaseSlotLocked(ct);
                        return;
                    }
                    if (lane.Count > 0) continue;
                }

                IDelivery? next = null;
                try { next = await _linger!.NextForSourceAsync(d, ct); }
                catch (OperationCanceledException) { return; }
                catch (Exception ex)
                {
                    _metrics.OnError(_group.Id);
                    _metrics.ErrorSample($"linger {_group.Name}#{_index}: {ex.GetType().Name}: {ex.Message}");
                }
                lingers++;
                lock (_gate)
                {
                    var lane = _lanes[src];
                    if (next != null) { lane.Enqueue(next); Interlocked.Increment(ref _metrics.LingerHits); }
                    else Interlocked.Increment(ref _metrics.LingerMisses);
                    if (lane.Count == 0)
                    {
                        _lanes.Remove(src);
                        ReleaseSlotLocked(ct);
                        return;
                    }
                }
            }
        }
        finally
        {
            Interlocked.Decrement(ref _running);
        }
    }

    private void ReleaseSlotLocked(CancellationToken ct)
    {
        _free++;
        DispatchLocked(ct);
        if (_free > 0 && _slotFreed.CurrentCount == 0) _slotFreed.Release();
    }

    private async Task GiveBackAsync(IDelivery[] lane, CancellationToken ct)
    {
        foreach (var d in lane)
        {
            try { await d.NackAsync(_retryDelay, ct); }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _metrics.OnError(d.Event.Group);
                _metrics.ErrorSample($"nack failed: {ex.Message}");
            }
            catch (OperationCanceledException) { return; }
        }
    }

    private async Task RequeueLaterAsync(int src, CancellationToken ct)
    {
        try { await Task.Delay(_retryDelay, ct); }
        catch (OperationCanceledException) { return; }
        lock (_gate)
        {
            _ready.Enqueue(src);
            DispatchLocked(ct);
        }
    }

    private static async Task SafeDelay(double ms, CancellationToken ct)
    {
        try { await Task.Delay(TimeSpan.FromMilliseconds(Math.Max(ms, 0.5)), ct); }
        catch (OperationCanceledException) { }
    }
}
