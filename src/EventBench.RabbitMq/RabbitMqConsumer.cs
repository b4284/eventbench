using System.Threading.Channels;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBench.RabbitMq;

/// <summary>
/// Pull adapter over RabbitMQ push consumers for one worker: one channel (prefetch = basic.qos) that consumes
/// all partition queues this worker owns. Deliveries are pushed into a <see cref="Channel{T}"/> by the client's
/// dispatcher, moved into an <see cref="OrderedRedeliveryBuffer{T}"/> and handed out from there.
///
/// Ordering: a partition queue has exactly one consumer, so the broker delivers its messages in queue order.
/// Nacks never go to the broker (a broker requeue would let already-prefetched later messages of the same
/// source overtake the retried one); the message stays unacked and is retried from the local buffer in its
/// original position, with its source blocked until then. If the channel dies, the broker returns all unacked
/// messages to the head of the queue in delivery order (= queue order, since we never requeue); the local
/// buffer is dropped and the channel re-created.
/// </summary>
internal sealed class RabbitMqConsumer : IEventConsumer
{
    internal sealed record Incoming(int Generation, ulong Tag, bool Redelivered, long EnqueuedUs, EventEnvelope Event);

    private readonly RabbitMqBroker _broker;
    private readonly string _name;
    private readonly string[] _queues;
    private readonly int _connectionIndex;
    private readonly Action<string> _log;
    private readonly Channel<Incoming> _inbox = Channel.CreateUnbounded<Incoming>(new UnboundedChannelOptions { SingleReader = true });
    private readonly SemaphoreSlim _doorbell = new(0);
    private readonly SemaphoreSlim _ackLock = new(1, 1);
    private readonly object _gate = new();
    private readonly OrderedRedeliveryBuffer<Incoming> _buffer = new();
    private readonly List<OrderedRedeliveryBuffer<Incoming>.Entry> _taken = new();
    private readonly Func<OrderedRedeliveryBuffer<Incoming>.Entry, bool>? _injectNack;
    private readonly long _injectDelayUs;
    private IChannel? _channel;
    private int _generation;
    private bool _disposed;

    public RabbitMqConsumer(RabbitMqBroker broker, string name, string[] queues, int connectionIndex, Action<string> log)
    {
        _broker = broker;
        _name = name;
        _queues = queues;
        _connectionIndex = connectionIndex;
        _log = log;
        if (broker.Options.TestNackProbability > 0)
        {
            var rng = new Random(HashCode.Combine(name, 4711));
            double p = broker.Options.TestNackProbability;
            _injectNack = _ => rng.NextDouble() < p;
            _injectDelayUs = (long)broker.TestNackDelay.TotalMicroseconds;
        }
    }

    public async ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct)
    {
        var channel = await EnsureChannelAsync(ct);
        long deadline = Clock.NowUs() + _broker.Options.ReceiveWaitMs * 1000L;
        while (true)
        {
            long now = Clock.NowUs();
            long wake;
            List<IDelivery>? result = null;
            lock (_gate)
            {
                DrainInbox();
                _taken.Clear();
                int injected = _buffer.Take(maxCount, now, _taken, _injectNack, _injectDelayUs);
                if (injected > 0) Interlocked.Add(ref _broker.TestNacks, injected);
                if (_taken.Count > 0)
                {
                    result = new List<IDelivery>(_taken.Count);
                    foreach (var e in _taken) result.Add(new Delivery(this, e, _generation, now));
                }
                wake = _buffer.NextRetryUs(now);
            }
            if (result != null) return result;
            if (now >= deadline || !channel.IsOpen) return [];
            long waitUs = Math.Min(deadline, wake) - now;
            await _doorbell.WaitAsync(TimeSpan.FromMicroseconds(Math.Max(waitUs, 100)), ct);
        }
    }

    /// <summary>Moves pushed deliveries into the ordered buffer; drops those of a previous channel generation.</summary>
    private void DrainInbox()
    {
        while (_inbox.Reader.TryRead(out var m))
            if (m.Generation == _generation) _buffer.Add(m, m.Event.SourceId);
    }

    private async ValueTask<IChannel> EnsureChannelAsync(CancellationToken ct)
    {
        var current = _channel;
        if (current is { IsOpen: true }) return current;
        ObjectDisposedException.ThrowIf(_disposed, this);

        int generation;
        lock (_gate)
        {
            // Everything unacked on the old channel is requeued by the broker; forget it locally.
            generation = ++_generation;
            _buffer.Clear();
            while (_inbox.Reader.TryRead(out _)) { }
        }
        if (current != null)
        {
            _log($"rabbitmq consumer {_name}: channel closed ({current.CloseReason?.ReplyText ?? "?"}), re-subscribing");
            await CloseQuietlyAsync(current);
        }

        var conn = await _broker.ConsumerConnectionAsync(_connectionIndex, ct);
        var ch = await conn.CreateChannelAsync(new CreateChannelOptions(false, false, consumerDispatchConcurrency: 1), ct);
        try
        {
            await ch.BasicQosAsync(0, (ushort)_broker.Prefetch, global: false, ct);
            var consumer = new AsyncEventingBasicConsumer(ch);
            consumer.ReceivedAsync += (_, ea) =>
            {
                // The body buffer is only valid inside this callback: deserialize here.
                var ev = EventEnvelope.FromJson(ea.Body.Span);
                long enqueued = 0;
                if (ea.BasicProperties.Headers is { } h && h.TryGetValue(RabbitMqBroker.PublishTimestampHeader, out var ts) && ts is long l)
                    enqueued = l;
                _inbox.Writer.TryWrite(new Incoming(generation, ea.DeliveryTag, ea.Redelivered, enqueued, ev));
                _doorbell.Release();
                return Task.CompletedTask;
            };
            foreach (var q in _queues)
                await ch.BasicConsumeAsync(q, autoAck: false, consumerTag: $"{_name}/{q}", noLocal: false, exclusive: false,
                    arguments: null, consumer: consumer, cancellationToken: ct);
        }
        catch
        {
            await CloseQuietlyAsync(ch);
            throw;
        }
        _channel = ch;
        return ch;
    }

    private async ValueTask AckAsync(OrderedRedeliveryBuffer<Incoming>.Entry e, int generation, CancellationToken ct)
    {
        IChannel? ch;
        lock (_gate)
        {
            ch = generation == _generation ? _channel : null;
        }
        if (ch is null || !ch.IsOpen)
            throw new InvalidOperationException("rabbitmq: delivery belongs to a closed channel; the broker will redeliver it");
        await _ackLock.WaitAsync(ct);
        try
        {
            await ch.BasicAckAsync(e.Item.Tag, multiple: false, ct);
        }
        finally
        {
            _ackLock.Release();
        }
        // Only now may the source's next message be handed out.
        lock (_gate)
        {
            if (generation == _generation) _buffer.Ack(e);
        }
    }

    private void Nack(OrderedRedeliveryBuffer<Incoming>.Entry e, int generation, TimeSpan delay)
    {
        lock (_gate)
        {
            if (generation == _generation) _buffer.Nack(e, Clock.NowUs() + (long)delay.TotalMicroseconds);
        }
    }

    private static async ValueTask CloseQuietlyAsync(IChannel ch)
    {
        try
        {
            if (ch.IsOpen) await ch.CloseAsync();
            await ch.DisposeAsync();
        }
        catch { /* best effort */ }
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;
        var ch = _channel;
        _channel = null;
        // Closing the channel returns every unacked message (including locally delayed ones) to its queue, in order.
        if (ch != null) await CloseQuietlyAsync(ch);
        lock (_gate) _buffer.Clear();
    }

    private sealed class Delivery(RabbitMqConsumer owner, OrderedRedeliveryBuffer<Incoming>.Entry entry, int generation, long receivedUs)
        : IDelivery
    {
        public EventEnvelope Event => entry.Item.Event;
        public long ReceivedAtUs => receivedUs;
        public long EnqueuedAtUs => entry.Item.EnqueuedUs;
        public int DeliveryCount => entry.HandOuts + (entry.Item.Redelivered ? 1 : 0);
        public ValueTask AckAsync(CancellationToken ct) => owner.AckAsync(entry, generation, ct);

        public ValueTask NackAsync(TimeSpan delay, CancellationToken ct)
        {
            owner.Nack(entry, generation, delay);
            return ValueTask.CompletedTask;
        }
    }
}
