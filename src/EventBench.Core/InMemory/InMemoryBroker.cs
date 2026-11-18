using System.Text;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.InMemory;

public sealed class InMemoryBrokerOptions
{
    /// <summary>Probability of handing out the 2nd queued message of a source instead of the head (breaks ordering).</summary>
    public double ChaosReorderProbability { get; set; }
    /// <summary>Probability that an ack is ignored, causing a redelivery (tests idempotency).</summary>
    public double ChaosDuplicateProbability { get; set; }
    /// <summary>Probability that a published message is silently lost.</summary>
    public double ChaosDropProbability { get; set; }
}

/// <summary>
/// Reference broker: per-group FIFO lanes per source; a source's next message is only delivered once
/// the previous one is acked. Messages are serialized to JSON so payload handling matches real brokers.
/// Useful as a baseline (measures the harness + store without broker cost) and for oracle self-tests.
/// </summary>
public sealed class InMemoryBroker : IBroker
{
    private sealed class Message
    {
        public required long Id;
        public required byte[] Body;
        public required long EnqueuedUs;
        public int Deliveries;
    }

    private sealed class Lane
    {
        public readonly LinkedList<Message> Messages = new();
        public bool InFlight;
        public bool Queued;
    }

    private sealed class GroupQueue
    {
        public readonly object Gate = new();
        public readonly Dictionary<int, Lane> Lanes = new();
        public readonly Queue<int> Ready = new();
        public long Count;
        public FastRandom Rng = new(42);
    }

    private readonly GroupQueue[] _groups;
    private readonly InMemoryBrokerOptions _options;
    private long _nextId;

    public string Name => "inmemory";

    public InMemoryBroker(Topology topology, InMemoryBrokerOptions options)
    {
        _options = options;
        _groups = topology.Groups.Select(_ => new GroupQueue()).ToArray();
    }

    public Task SetupAsync(CancellationToken ct) => Task.CompletedTask;

    public IEventPublisher CreatePublisher(int groupId) => new Publisher(this);
    public IEventConsumer CreateConsumer(int groupId, int workerIndex) => new Consumer(this, _groups[groupId]);

    public Task<long> GetBacklogAsync(CancellationToken ct)
        => Task.FromResult(_groups.Sum(g => { lock (g.Gate) return g.Count; }));

    public string Describe() =>
        $"inmemory (reorder={_options.ChaosReorderProbability}, duplicate={_options.ChaosDuplicateProbability}, drop={_options.ChaosDropProbability})";

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    private void Publish(IReadOnlyList<EventEnvelope> events)
    {
        foreach (var ev in events)
        {
            var g = _groups[ev.Group];
            var body = Encoding.UTF8.GetBytes(ev.ToJson());
            lock (g.Gate)
            {
                if (_options.ChaosDropProbability > 0 && g.Rng.NextDouble() < _options.ChaosDropProbability) continue;
                if (!g.Lanes.TryGetValue(ev.SourceId, out var lane)) g.Lanes[ev.SourceId] = lane = new Lane();
                lane.Messages.AddLast(new Message { Id = Interlocked.Increment(ref _nextId), Body = body, EnqueuedUs = Clock.NowUs() });
                g.Count++;
                MarkReady(g, ev.SourceId, lane);
            }
        }
    }

    private static void MarkReady(GroupQueue g, int sourceId, Lane lane)
    {
        if (!lane.InFlight && !lane.Queued && lane.Messages.Count > 0)
        {
            lane.Queued = true;
            g.Ready.Enqueue(sourceId);
        }
    }

    private sealed class Publisher(InMemoryBroker broker) : IEventPublisher
    {
        public ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            broker.Publish(events);
            return ValueTask.CompletedTask;
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Consumer(InMemoryBroker broker, GroupQueue g) : IEventConsumer
    {
        public ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
        {
            var taken = new List<(int Source, Message Msg)>();
            lock (g.Gate)
            {
                while (taken.Count < maxCount && g.Ready.TryDequeue(out int sid))
                {
                    var lane = g.Lanes[sid];
                    lane.Queued = false;
                    if (lane.InFlight || lane.Messages.Count == 0) continue;
                    var node = lane.Messages.First!;
                    if (broker._options.ChaosReorderProbability > 0 && node.Next != null
                        && g.Rng.NextDouble() < broker._options.ChaosReorderProbability)
                        node = node.Next;
                    lane.InFlight = true;
                    node.Value.Deliveries++;
                    taken.Add((sid, node.Value));
                }
            }
            long now = Clock.NowUs();
            IReadOnlyList<IDelivery> result = taken
                .Select(t => (IDelivery)new Delivery(broker, g, t.Source, t.Msg, EventEnvelope.FromJson(t.Msg.Body), now))
                .ToList();
            return ValueTask.FromResult(result);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class Delivery(InMemoryBroker broker, GroupQueue g, int sourceId, Message msg, EventEnvelope ev, long receivedUs) : IDelivery
    {
        public EventEnvelope Event => ev;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => msg.EnqueuedUs;
        public int DeliveryCount => msg.Deliveries;

        public ValueTask AckAsync(CancellationToken ct)
        {
            lock (g.Gate)
            {
                var lane = g.Lanes[sourceId];
                bool skip = broker._options.ChaosDuplicateProbability > 0 && g.Rng.NextDouble() < broker._options.ChaosDuplicateProbability;
                if (!skip && lane.Messages.Remove(msg)) g.Count--;
                lane.InFlight = false;
                MarkReady(g, sourceId, lane);
            }
            return ValueTask.CompletedTask;
        }

        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            _ = Task.Delay(delay, CancellationToken.None).ContinueWith(_ =>
            {
                lock (g.Gate)
                {
                    var lane = g.Lanes[sourceId];
                    lane.InFlight = false;
                    MarkReady(g, sourceId, lane);
                }
            }, TaskScheduler.Default);
            return ValueTask.CompletedTask;
        }
    }
}

public sealed class InMemoryBrokerFactory : IBrokerFactory
{
    public string Type => "inmemory";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new InMemoryBroker(context.Topology, section.Get<InMemoryBrokerOptions>() ?? new());
}
