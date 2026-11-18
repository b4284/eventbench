using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Nats;

namespace EventBench.Tests;

/// <summary>
/// Concurrent duplicate processing against a live NATS server (the case the chaos runs cannot produce:
/// several workers handling the *same* seq at the same time). Runs only when EB_NATS_URL is set,
/// e.g. EB_NATS_URL=nats://127.0.0.1:4222 after infra/nats/up.sh.
/// </summary>
public class NatsKvConcurrencyTests
{
    [Fact]
    public async Task ConcurrentDuplicatesApplyExactlyOnce()
    {
        var url = Environment.GetEnvironmentVariable("EB_NATS_URL");
        if (string.IsNullOrEmpty(url)) return;

        var topology = new Topology([new GroupInfo(0, "a", 2, 1, 1, 0)]);
        await using var store = new NatsKvStore(new NatsKvStoreOptions { Url = url, BucketPrefix = "ebtest", Storage = "Memory" },
            topology, _ => { });
        await store.SetupAsync(CancellationToken.None);

        const int events = 150, racers = 6;
        long recordId = StateMachineIds.Record(0);
        for (long seq = 1; seq <= events; seq++)
        {
            var ev = new EventEnvelope { Group = 0, SourceId = 0, Seq = seq, RecordId = recordId, Op = seq == 1 ? OpType.Create : OpType.Update };
            var results = await Task.WhenAll(Enumerable.Range(0, racers).Select(async _ =>
            {
                var ctx = await store.LoadAsync(ev, CancellationToken.None);
                if (ctx.LastSeq >= seq) return CommitStatus.Duplicate;
                Assert.Equal(seq - 1, ctx.LastSeq);
                // Snapshot consistency: the record state must be exactly the one as of cursor seq-1.
                Assert.Equal(seq > 1, ctx.RecordExists);
                if (seq > 1) Assert.Equal((int)(seq - 1), ctx.Version);
                return await store.CommitAsync(new CommitRequest
                {
                    Event = ev,
                    Kind = seq == 1 ? MutationKind.Insert : MutationKind.Update,
                    PrevVersion = ctx.Version,
                    NewVersion = (int)seq,
                    NewHash = seq * 31,
                    NewStatus = seq == events ? RecordStatus.Completed : RecordStatus.Open,
                    NewContext = [(byte)seq],
                }, CancellationToken.None);
            }));
            Assert.Equal(1, results.Count(r => r == CommitStatus.Applied));
            Assert.All(results, r => Assert.True(r is CommitStatus.Applied or CommitStatus.Duplicate, $"seq {seq}: {r}"));
        }

        var cursors = new List<SourceCursor>();
        await foreach (var c in store.ScanCursorsAsync(CancellationToken.None)) cursors.Add(c);
        Assert.Equal([new SourceCursor(0, events), new SourceCursor(1, 0)], cursors);

        var records = new List<RecordRow>();
        await foreach (var r in store.ScanRecordsAsync(CancellationToken.None)) records.Add(r);
        var row = Assert.Single(records);
        Assert.Equal(new RecordRow(0, recordId, RecordStatus.Completed, events, events * 31), row);
    }

    private static class StateMachineIds
    {
        public static long Record(int sourceId) => ((long)sourceId << 32) | 1;
    }
}
