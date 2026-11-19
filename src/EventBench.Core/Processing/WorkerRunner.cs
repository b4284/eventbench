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
    }

    public async Task RunAsync(CancellationToken ct)
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

    /// <returns>true when the event was applied or safely skipped (duplicate).</returns>
    private async Task<bool> ProcessOneAsync(IDelivery d, CancellationToken ct)
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
                return true;
            }
            if (ctx.LastSeq != ev.Seq - 1)
            {
                _metrics.OnGap(ev, ctx.LastSeq);
                await NackAsync(d, ct);
                return false;
            }

            var request = await ComputeAsync(ev, ctx, d, ct);
            long t2 = Clock.NowUs();

            if (_options.ChaosFailProbability > 0 && Random.Shared.NextDouble() < _options.ChaosFailProbability)
            {
                // Injected worker failure before commit: exercises the broker's nack/redelivery path,
                // which must not let later events of the source overtake this one.
                await NackAsync(d, ct);
                return false;
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
                            return true;
                        }
                        ackUs = Clock.NowUs() - t3;
                    }
                    long done = Clock.NowUs();
                    _metrics.RecordLatencies(ev, d.ReceivedAtUs, d.EnqueuedAtUs, done, t1 - t0, t2 - t1, t3 - t2, ackUs);
                    return true;
                }
                case CommitStatus.Duplicate:
                    _metrics.OnDuplicate(ev.Group);
                    if (!_txAck) await d.AckAsync(ct);
                    return true;
                case CommitStatus.Gap:
                    _metrics.OnGap(ev, -1);
                    await NackAsync(d, ct);
                    return false;
                default:
                    _metrics.OnError(ev.Group);
                    await NackAsync(d, ct);
                    return false;
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
            return false;
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
        try { await d.NackAsync(_retryDelay, ct); }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
        catch (Exception ex)
        {
            // The visibility timeout will eventually redeliver it anyway.
            _metrics.OnError(d.Event.Group);
            _metrics.ErrorSample($"nack failed: {ex.Message}");
        }
    }

    private static async Task SafeDelay(double ms, CancellationToken ct)
    {
        try { await Task.Delay(TimeSpan.FromMilliseconds(Math.Max(ms, 0.5)), ct); }
        catch (OperationCanceledException) { }
    }
}
