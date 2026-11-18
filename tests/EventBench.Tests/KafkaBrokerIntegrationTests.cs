using System.Collections.Concurrent;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Kafka;

namespace EventBench.Tests;

/// <summary>
/// Exercises the Kafka nack/rewind and contiguous-commit paths against a live broker with random
/// failures, out-of-order (parallel) acks and a consumer restart. Opt-in: runs only when
/// EB_KAFKA_BOOTSTRAP is set (e.g. 127.0.0.1:9092 after infra/kafka/up.sh); otherwise it is a no-op.
/// </summary>
public class KafkaBrokerIntegrationTests
{
    [Fact]
    public async Task RandomNacksAndRestartKeepPerSourceOrder()
    {
        var bootstrap = Environment.GetEnvironmentVariable("EB_KAFKA_BOOTSTRAP");
        if (string.IsNullOrEmpty(bootstrap)) return;

        const int sources = 40, perSource = 60, workers = 2;
        var group = new GroupInfo(0, "A", sources, 1, workers, 0);
        var topology = new Topology([group]);
        var options = new KafkaOptions
        {
            BootstrapServers = bootstrap, TopicPrefix = "ebtest", PartitionsPerGroup = 4,
            FlushMessages = 0, CommitIntervalMs = 20,
        };
        await using var broker = new KafkaBroker(options, topology, _ => { });
        await broker.SetupAsync(CancellationToken.None);

        await using (var pub = broker.CreatePublisher(0))
        {
            var events = new List<EventEnvelope>();
            for (int seq = 1; seq <= perSource; seq++)
                for (int s = 0; s < sources; s++)
                    events.Add(new EventEnvelope { Group = 0, SourceId = s, Seq = seq, RecordId = s });
            foreach (var chunk in events.Chunk(300)) await pub.PublishAsync(chunk, CancellationToken.None);
        }

        var cursor = new ConcurrentDictionary<int, long>();
        long gaps = 0, nacks = 0, dups = 0, applied = 0;
        var rng = new ThreadLocal<Random>(() => new Random(Environment.CurrentManagedThreadId));
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(90));

        async Task<bool> Process(IDelivery d)
        {
            var ev = d.Event;
            await Task.Delay(rng.Value!.Next(0, 3));
            long cur = cursor.GetOrAdd(ev.SourceId, 0);
            if (ev.Seq <= cur) { Interlocked.Increment(ref dups); await d.AckAsync(default); return true; }
            if (ev.Seq != cur + 1) { Interlocked.Increment(ref gaps); await d.NackAsync(TimeSpan.FromMilliseconds(5), default); return false; }
            if (rng.Value!.NextDouble() < 0.05) { Interlocked.Increment(ref nacks); await d.NackAsync(TimeSpan.FromMilliseconds(10), default); return false; }
            cursor[ev.SourceId] = ev.Seq;
            Interlocked.Increment(ref applied);
            await d.AckAsync(default);
            return true;
        }

        async Task Worker(int index)
        {
            var consumer = broker.CreateConsumer(0, index);
            bool restarted = false;
            try
            {
                while (!cts.IsCancellationRequested && cursor.Values.Sum() < (long)sources * perSource)
                {
                    if (index == 0 && !restarted && Interlocked.Read(ref applied) > sources * perSource / 3)
                    {
                        // Simulated crash/restart: must resume from the committed offsets.
                        await consumer.DisposeAsync();
                        consumer = broker.CreateConsumer(0, index);
                        restarted = true;
                    }
                    var batch = await consumer.ReceiveAsync(10, cts.Token);
                    if (batch.Count == 0) { await Task.Delay(2); continue; }
                    // Same lane semantics as WorkerRunner: per source sequential, sources in parallel.
                    await Task.WhenAll(batch.GroupBy(d => d.Event.SourceId).Select(async lane =>
                    {
                        var items = lane.ToList();
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (await Process(items[i])) continue;
                            for (int j = i + 1; j < items.Count; j++) await items[j].NackAsync(TimeSpan.FromMilliseconds(10), default);
                            return;
                        }
                    }));
                }
                // Keep receiving briefly so the final commits go out.
                for (int i = 0; i < 10; i++) { await consumer.ReceiveAsync(10, cts.Token); await Task.Delay(30); }
            }
            finally { await consumer.DisposeAsync(); }
        }

        await Task.WhenAll(Enumerable.Range(0, workers).Select(i => Task.Run(() => Worker(i))));

        Assert.Equal(0, gaps);
        Assert.All(Enumerable.Range(0, sources), s => Assert.Equal(perSource, cursor.GetValueOrDefault(s)));
        Assert.True(nacks > 0);
        Assert.True(dups > 0); // redelivered after rewinds / restart, dedup'd by the cursor
        var stats = await broker.SampleAsync(CancellationToken.None);
        Assert.True(stats["broker_rewinds"] > 0);
        Assert.Equal(0, stats["broker_backlog"]);
        Assert.Equal((double)sources * perSource, stats["broker_end_offsets"]);
    }
}
