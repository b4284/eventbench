using System.Text.Json;
using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;
using Temporalio.Activities;
using Temporalio.Api.Enums.V1;
using Temporalio.Client;
using Temporalio.Common;
using Temporalio.Exceptions;
using Temporalio.Worker;
using Temporalio.Workflows;

namespace EventBench.Temporal;

public sealed class TemporalOptions
{
    public string Target { get; set; } = "127.0.0.1:7233";
    public string Namespace { get; set; } = "default";
    public string WorkflowTaskQueue { get; set; } = "eb-wf";
    public string ActivityTaskQueuePrefix { get; set; } = "eb-g";
    public int MaxConcurrentWorkflowTasks { get; set; } = 500;
    public int WorkflowTaskPollers { get; set; } = 32;
    public int ActivityTaskPollersPerGroup { get; set; } = 2;
    /// <summary>Events per workflow run before continue-as-new (bounds the history).</summary>
    public int ContinueAsNewAfter { get; set; } = 500;
    public int RetryInitialMs { get; set; } = 250;
    public int RetryMaxSeconds { get; set; } = 60;
    public int ReceiveWaitMs { get; set; } = 500;
}

/// <summary>One event as carried by Temporal (signal input, activity input).</summary>
public sealed class EbEvent
{
    public int Group { get; set; }
    public int SourceId { get; set; }
    public long Seq { get; set; }
    public byte[] Body { get; set; } = [];
}

/// <summary>
/// One long-running workflow per source ("entity" pattern). Events arrive as signals (signal-with-start) and are
/// processed strictly one after another: each is an activity on the group's task queue, retried for ever with
/// capped backoff, so a failing event holds back only its own source. Continue-as-new every N events carries the
/// not-yet-processed events over, in order.
/// </summary>
[Workflow]
public sealed class SourceWorkflow
{
    private readonly Queue<EbEvent> _carried = new();
    private readonly Queue<EbEvent> _pending = new();

    [WorkflowSignal]
    public Task EventAsync(EbEvent e)
    {
        _pending.Enqueue(e);
        return Task.CompletedTask;
    }

    [WorkflowRun]
    public async Task RunAsync(WorkflowArgs args)
    {
        foreach (var e in args.Carried) _carried.Enqueue(e);
        var options = new ActivityOptions
        {
            StartToCloseTimeout = TimeSpan.FromMinutes(10),
            RetryPolicy = new RetryPolicy
            {
                InitialInterval = TimeSpan.FromMilliseconds(args.RetryInitialMs),
                BackoffCoefficient = 2,
                MaximumInterval = TimeSpan.FromSeconds(args.RetryMaxSeconds),
                MaximumAttempts = 0, // for ever
            },
        };
        int done = 0;
        while (true)
        {
            await Workflow.WaitConditionAsync(() => _carried.Count + _pending.Count > 0);
            var e = _carried.Count > 0 ? _carried.Dequeue() : _pending.Dequeue();
            options.TaskQueue = $"{args.ActivityTaskQueuePrefix}{e.Group}";
            await Workflow.ExecuteActivityAsync((EbActivities a) => a.ProcessAsync(e), options);
            if (++done >= args.ContinueAsNewAfter)
            {
                var next = args with { Carried = _carried.Concat(_pending).ToList() };
                throw Workflow.CreateContinueAsNewException((SourceWorkflow wf) => wf.RunAsync(next));
            }
        }
    }
}

public sealed record WorkflowArgs(List<EbEvent> Carried, string ActivityTaskQueuePrefix, int ContinueAsNewAfter, int RetryInitialMs, int RetryMaxSeconds);

public sealed class EbActivities(TemporalBroker broker)
{
    [Activity]
    public async Task ProcessAsync(EbEvent e)
    {
        var ctx = ActivityExecutionContext.Current;
        var ev = EventEnvelope.FromJson(e.Body);
        var d = new TemporalDelivery(ev, Clock.ToUs(ctx.Info.CurrentAttemptScheduledTime), Clock.NowUs(), ctx.Info.Attempt);
        broker.GroupChannel(e.Group).Writer.TryWrite(d);
        bool ok = await d.Done.Task.WaitAsync(ctx.CancellationToken);
        if (!ok) throw new ApplicationFailureException("worker asked for a retry");
        Interlocked.Increment(ref broker.Acked);
    }
}

public sealed class TemporalBroker : IBroker, IStatsProvider
{
    private readonly TemporalOptions _o;
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly Channel<TemporalDelivery>[] _channels;
    private readonly CancellationTokenSource _cts = new();
    private readonly List<Task> _workers = new();
    private TemporalClient? _client;
    internal long Published, Acked;
    internal int SlotsPerWorker = 4;

    public string Name => "temporal";
    public IStatsProvider Stats => this;

    public TemporalBroker(TemporalOptions o, Topology topology, Action<string> log)
    {
        _o = o;
        _topology = topology;
        _log = log;
        _channels = topology.Groups.Select(_ => Channel.CreateUnbounded<TemporalDelivery>()).ToArray();
    }

    internal Channel<TemporalDelivery> GroupChannel(int group) => _channels[group];

    public async Task SetupAsync(CancellationToken ct)
    {
        _client = await TemporalClient.ConnectAsync(new(_o.Target) { Namespace = _o.Namespace });
        var acts = new EbActivities(this);
        var wf = new TemporalWorker(_client, new TemporalWorkerOptions(_o.WorkflowTaskQueue)
        {
            MaxConcurrentWorkflowTasks = _o.MaxConcurrentWorkflowTasks,
            MaxConcurrentWorkflowTaskPolls = _o.WorkflowTaskPollers,
        }.AddWorkflow<SourceWorkflow>());
        _workers.Add(wf.ExecuteAsync(_cts.Token));
        foreach (var g in _topology.Groups)
        {
            var w = new TemporalWorker(_client, new TemporalWorkerOptions($"{_o.ActivityTaskQueuePrefix}{g.Id}")
            {
                MaxConcurrentActivities = Math.Max(1, g.Workers * SlotsPerWorker),
                MaxConcurrentActivityTaskPolls = _o.ActivityTaskPollersPerGroup,
            }.AddAllActivities(acts));
            _workers.Add(w.ExecuteAsync(_cts.Token));
        }
        _log($"temporal: {_o.Target}/{_o.Namespace}, workflow per source on {_o.WorkflowTaskQueue}, activity task queue per group " +
             $"({_topology.Groups.Count}), continue-as-new after {_o.ContinueAsNewAfter}");
    }

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);

    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, _channels[groupId]);

    /// <summary>Signals accepted but not yet completed by a worker (Temporal has no queue-depth API for this).</summary>
    public Task<long> GetBacklogAsync(CancellationToken ct) => Task.FromResult(Math.Max(0, Interlocked.Read(ref Published) - Interlocked.Read(ref Acked)));

    public Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
        => Task.FromResult<IReadOnlyDictionary<string, double>>(new Dictionary<string, double>
        {
            ["broker_backlog"] = Math.Max(0, Interlocked.Read(ref Published) - Interlocked.Read(ref Acked)),
            ["temporal_published_total"] = Interlocked.Read(ref Published),
        });

    public string Describe()
        => $"Temporal {_o.Target}/{_o.Namespace}: workflow per source (signal-with-start), activity per event on a task queue per group, " +
           $"retry {_o.RetryInitialMs} ms..{_o.RetryMaxSeconds} s for ever, continue-as-new after {_o.ContinueAsNewAfter}";

    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        try { await Task.WhenAll(_workers).WaitAsync(TimeSpan.FromSeconds(30)); } catch { /* shutting down */ }
        foreach (var ch in _channels) ch.Writer.TryComplete();
    }

    private sealed class Publisher(TemporalBroker b) : IEventPublisher
    {
        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            foreach (var ev in events)
            {
                var e = new EbEvent
                {
                    Group = ev.Group, SourceId = ev.SourceId, Seq = ev.Seq,
                    Body = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope),
                };
                var args = new WorkflowArgs([], b._o.ActivityTaskQueuePrefix, b._o.ContinueAsNewAfter, b._o.RetryInitialMs, b._o.RetryMaxSeconds);
                var opts = new WorkflowOptions($"src-{ev.SourceId}", b._o.WorkflowTaskQueue)
                {
                    IdConflictPolicy = WorkflowIdConflictPolicy.UseExisting,
                    Rpc = new RpcOptions { CancellationToken = ct },
                };
                opts.SignalWithStart((SourceWorkflow wf) => wf.EventAsync(e));
                await b._client!.StartWorkflowAsync((SourceWorkflow wf) => wf.RunAsync(args), opts);
                Interlocked.Increment(ref b.Published);
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(TemporalBroker b, Channel<TemporalDelivery> channel) : IEventConsumer
    {
        public bool SharedSources => true;

        public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            var list = new List<IDelivery>(maxCount);
            while (list.Count < maxCount && channel.Reader.TryRead(out var d)) list.Add(d);
            if (list.Count > 0) return list;
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(b._o.ReceiveWaitMs);
            try
            {
                if (!await channel.Reader.WaitToReadAsync(timeout.Token)) return list;
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested) { return list; }
            while (list.Count < maxCount && channel.Reader.TryRead(out var d)) list.Add(d);
            return list;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

public sealed class TemporalDelivery(EventEnvelope ev, long enqueuedUs, long receivedUs, int attempt) : IDelivery
{
    public readonly TaskCompletionSource<bool> Done = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public EventEnvelope Event => ev;
    public long ReceivedAtUs => receivedUs;
    public long EnqueuedAtUs => enqueuedUs;
    public int DeliveryCount => attempt;

    public ValueTask AckAsync(CancellationToken ct)
    {
        Done.TrySetResult(true);
        return ValueTask.CompletedTask;
    }

    public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
    {
        Done.TrySetResult(false);
        return ValueTask.CompletedTask;
    }
}

public sealed class TemporalBrokerFactory : IBrokerFactory
{
    public string Type => "temporal";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new TemporalBroker(section.Get<TemporalOptions>() ?? new(), context.Topology, context.Log)
        {
            SlotsPerWorker = Math.Max(1, context.Config.Worker.MaxParallelPerWorker),
        };
}
