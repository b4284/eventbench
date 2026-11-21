using EventBench.Core.Abstractions;
using EventBench.Core.Config;
using EventBench.Core.Metrics;
using EventBench.Core.Model;
using EventBench.Core.Processing;
using EventBench.Core.Util;

namespace EventBench.Core.Workload;

/// <summary>
/// Open-loop producer for a disjoint subset of sources (of one group, or spread over many groups).
/// Each source has its own arrival process at its own (jittered) rate; due events are batched and
/// published in one call. Because a source is owned by exactly one producer and every publish is
/// awaited, per-source publish order equals sequence order. If the broker is slow the producer falls
/// behind its schedule — that lag is measured (producer_lag) and counted in e2e, instead of silently
/// lowering the offered load.
/// </summary>
public sealed class ProducerRunner
{
    private const int MaxPublishAttempts = 8;

    private readonly string _name;
    private readonly SourceScript[] _scripts;
    private readonly double[] _meanIntervalUs;
    private readonly IEventPublisher _publisher;
    private readonly IStateStore _store;
    private readonly WorkloadPlan _plan;
    private readonly MetricsHub _metrics;
    private readonly Action<string> _log;
    private FastRandom _rng;
    private readonly SemaphoreSlim? _slots;
    /// <summary>Per source, its last SourcePipeline publishes (oldest first).</summary>
    private readonly Dictionary<int, Queue<Task>> _lastBySource = new();
    private readonly HashSet<Task> _inFlight = new();

    public string Name => _name;
    public int SourceCount => _scripts.Length;

    public ProducerRunner(string name, int salt, SourceScript[] scripts, IEventPublisher publisher,
        IStateStore store, WorkloadPlan plan, MetricsHub metrics, Action<string> log)
    {
        _name = name;
        _scripts = scripts;
        _publisher = publisher;
        _store = store;
        _plan = plan;
        _metrics = metrics;
        _log = log;
        _meanIntervalUs = scripts.Select(s => 1_000_000.0 / plan.Topology.SourceRate(s.SourceId)).ToArray();
        _rng = new FastRandom((ulong)plan.Options.Seed * 31 + (ulong)salt * 7919 + 17);
        if (plan.Options.PublishConcurrency > 1) _slots = new SemaphoreSlim(plan.Options.PublishConcurrency);
    }

    /// <summary>
    /// Sends the batch: awaited (PublishConcurrency 1), or started in the background behind the previous publish
    /// of each of its sources (PublishConcurrency above 1). Takes ownership of the list.
    /// </summary>
    private async Task DispatchAsync(List<EventEnvelope> batch, CancellationToken ct)
    {
        if (_slots is null)
        {
            await SendAsync(batch, ct);
            return;
        }
        await _slots.WaitAsync(ct);
        int k = Math.Max(1, _plan.Options.SourcePipeline);
        // each event waits for its source's publish k places back (k = 1: the previous one)
        Task[] deps = batch.Select(e => _lastBySource.TryGetValue(e.SourceId, out var q) && q.Count >= k ? q.Peek() : null)
            .Where(t => t is { IsCompleted: false }).Distinct().ToArray()!;
        var task = SendAfterAsync(deps, batch, ct);
        foreach (var e in batch)
        {
            if (!_lastBySource.TryGetValue(e.SourceId, out var q)) _lastBySource[e.SourceId] = q = new Queue<Task>(k + 1);
            q.Enqueue(task);
            while (q.Count > k) q.Dequeue();
        }
        lock (_inFlight) _inFlight.Add(task);
        _ = task.ContinueWith(t =>
        {
            lock (_inFlight) _inFlight.Remove(t);
            _slots.Release();
        }, TaskScheduler.Default);
    }

    private async Task SendAfterAsync(Task[] deps, List<EventEnvelope> batch, CancellationToken ct)
    {
        if (deps.Length > 0) await Task.WhenAll(deps);
        await SendAsync(batch, ct);
    }

    /// <summary>Waits for every publish still in flight (PublishConcurrency above 1).</summary>
    private async Task FlushAsync()
    {
        Task[] pending;
        lock (_inFlight) pending = _inFlight.ToArray();
        await Task.WhenAll(pending);
        _lastBySource.Clear();
    }

    public async Task RunAsync(long startUs, long endUs, CancellationToken ct)
    {
        var opts = _plan.Options;
        var due = new PriorityQueue<int, long>(_scripts.Length);
        for (int i = 0; i < _scripts.Length; i++)
            due.Enqueue(i, startUs + (long)(_rng.NextDouble() * _meanIntervalUs[i])); // de-synchronize sources

        var batch = new List<EventEnvelope>(opts.MaxPublishBatch);
        long tickUs = (long)(opts.ProducerTickMs * 1000);

        while (!ct.IsCancellationRequested)
        {
            long now = Clock.NowUs();
            if (now >= endUs) break;

            while (batch.Count < opts.MaxPublishBatch && due.TryPeek(out int idx, out long at) && at <= now)
            {
                due.Dequeue();
                batch.Add(_scripts[idx].Next(at, _plan));
                long next = at + NextIntervalUs(idx);
                if (next < endUs) due.Enqueue(idx, next);
            }

            if (batch.Count == 0)
            {
                long wait = Math.Min(tickUs, due.TryPeek(out _, out long nextAt) ? nextAt - now : endUs - now);
                try { await Task.Delay(TimeSpan.FromMicroseconds(Math.Max(wait, 500)), ct); }
                catch (OperationCanceledException) { break; }
                continue;
            }

            if (_slots is null)
            {
                await SendAsync(batch, ct);
                batch.Clear();
            }
            else
            {
                await DispatchAsync(batch, ct);
                batch = new List<EventEnvelope>(opts.MaxPublishBatch);
            }
        }
        await FlushAsync();
    }

    /// <summary>Completes every still-open record of this producer's sources.</summary>
    public async Task CloseAsync(CancellationToken ct)
    {
        var all = new List<EventEnvelope>();
        long now = Clock.NowUs();
        foreach (var s in _scripts) s.Close(all, now, _plan);
        var batch = new List<EventEnvelope>(_plan.Options.MaxPublishBatch);
        foreach (var ev in all)
        {
            batch.Add(ev);
            if (batch.Count >= _plan.Options.MaxPublishBatch)
            {
                await DispatchAsync(batch, ct);
                batch = new List<EventEnvelope>(_plan.Options.MaxPublishBatch);
            }
        }
        if (batch.Count > 0) await DispatchAsync(batch, ct);
        await FlushAsync();
    }

    private long NextIntervalUs(int idx) => _plan.Options.Arrival switch
    {
        ArrivalProcess.Constant => (long)_meanIntervalUs[idx],
        _ => (long)(-Math.Log(1.0 - _rng.NextDouble()) * _meanIntervalUs[idx]) + 1,
    };

    private async Task SendAsync(List<EventEnvelope> batch, CancellationToken ct)
    {
        long sent = Clock.NowUs();
        long bytes = 0;
        foreach (var ev in batch)
        {
            ev.SentAtUs = sent;
            if (ev.IsLarge && _plan.Options.LargePayload.Mode == LargePayloadMode.ClaimCheck && ev.Payload != null)
            {
                long blobId = StateMachine.MakeBlobId(ev.SourceId, ev.Seq);
                await RetryAsync(() => _store.PutBlobAsync(blobId, ev.SourceId, ev.Payload, ct).AsTask(), "put blob", ev.Group, ct);
                ev.BlobId = blobId;
                ev.Payload = null;
            }
            bytes += 200 + (ev.Payload?.Length ?? 0) * 4 / 3;
        }

        long t0 = Clock.NowUs();
        await RetryAsync(() => _publisher.PublishAsync(batch, ct).AsTask(), "publish", batch[0].Group, ct);
        long published = Clock.NowUs();
        _metrics.Blocking.OnPublished(batch, published);
        _metrics.OnPublished(batch, published - t0, bytes);

        // Drop payload references so large buffers can be collected.
        foreach (var ev in batch) ev.Payload = null;
    }

    private async Task RetryAsync(Func<Task> action, string what, int group, CancellationToken ct)
    {
        for (int attempt = 1; ; attempt++)
        {
            try
            {
                await action();
                return;
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
            catch (Exception ex) when (attempt < MaxPublishAttempts)
            {
                // Retrying can duplicate messages if the first attempt actually committed: workers dedup via the cursor.
                _metrics.OnPublishError(group);
                _metrics.ErrorSample($"{what} {_name} attempt {attempt}: {ex.GetType().Name}: {ex.Message}");
                if (attempt <= 2) _log($"producer {_name} {what} failed (attempt {attempt}): {ex.Message}");
                await Task.Delay(TimeSpan.FromMilliseconds(Math.Min(2000, 50 * (1 << attempt))), ct);
            }
        }
    }
}
