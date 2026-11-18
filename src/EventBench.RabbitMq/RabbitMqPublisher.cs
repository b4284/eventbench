using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBench.RabbitMq;

/// <summary>
/// One confirm-mode channel. A batch is published strictly in list order on that single channel (so per-queue,
/// and therefore per-source, order is the publish order), then the call waits until the broker has confirmed
/// every message (quorum queue: replicated + fsynced). Persistent delivery mode, mandatory flag (an unroutable
/// message is returned and fails the batch instead of being silently dropped).
/// A failed batch is retried whole by the producer; already-confirmed messages become duplicates, which the
/// store cursor skips.
/// </summary>
internal sealed class RabbitMqPublisher(RabbitMqBroker broker) : IEventPublisher
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private IChannel? _channel;
    private ConfirmTracker _tracker = new();

    public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var ch = await EnsureChannelAsync(ct);
            var tracker = _tracker;
            tracker.TakeFailure();
            try
            {
                foreach (var ev in events)
                {
                    var body = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope);
                    var props = new BasicProperties
                    {
                        DeliveryMode = DeliveryModes.Persistent,
                        ContentType = "application/json",
                        Headers = new Dictionary<string, object?> { [RabbitMqBroker.PublishTimestampHeader] = Clock.NowUs() },
                    };
                    tracker.Add(await ch.GetNextPublishSequenceNumberAsync(ct));
                    await ch.BasicPublishAsync(string.Empty, broker.QueueFor(ev), mandatory: true, props, body, ct);
                }
                await tracker.WhenSettledAsync().WaitAsync(TimeSpan.FromSeconds(broker.Options.PublishConfirmTimeoutSeconds), ct);
                if (tracker.TakeFailure() is { } failure) throw new InvalidOperationException($"rabbitmq publish failed: {failure}");
            }
            catch
            {
                // Unknown confirm state: start over on a fresh channel (fresh sequence numbers).
                await ResetChannelAsync();
                throw;
            }
        }
        finally
        {
            _gate.Release();
        }
    }

    private async ValueTask<IChannel> EnsureChannelAsync(CancellationToken ct)
    {
        if (_channel is { IsOpen: true }) return _channel;
        await ResetChannelAsync();
        var conn = await broker.PublisherConnectionAsync(ct);
        var tracker = new ConfirmTracker();
        // Confirms on, but tracked by us (ConfirmTracker) so a whole batch can be pipelined in strict order.
        var ch = await conn.CreateChannelAsync(new CreateChannelOptions(
            publisherConfirmationsEnabled: true, publisherConfirmationTrackingEnabled: false), ct);
        ch.BasicAcksAsync += (_, ea) => { tracker.Ack(ea.DeliveryTag, ea.Multiple); return Task.CompletedTask; };
        ch.BasicNacksAsync += (_, ea) => { tracker.Nack(ea.DeliveryTag, ea.Multiple); return Task.CompletedTask; };
        ch.BasicReturnAsync += (_, ea) =>
        {
            tracker.Fail($"message returned by broker: {ea.ReplyCode} {ea.ReplyText} (routing key {ea.RoutingKey})");
            return Task.CompletedTask;
        };
        ch.ChannelShutdownAsync += (_, ea) =>
        {
            tracker.Abort($"channel closed: {ea.ReplyCode} {ea.ReplyText}");
            return Task.CompletedTask;
        };
        _tracker = tracker;
        _channel = ch;
        return ch;
    }

    private async ValueTask ResetChannelAsync()
    {
        var ch = _channel;
        _channel = null;
        if (ch is null) return;
        try
        {
            if (ch.IsOpen) await ch.CloseAsync();
            await ch.DisposeAsync();
        }
        catch { /* already broken */ }
    }

    public async ValueTask DisposeAsync()
    {
        await _gate.WaitAsync();
        try { await ResetChannelAsync(); }
        finally { _gate.Release(); }
    }
}
