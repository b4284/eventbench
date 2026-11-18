using EventBench.Core.Abstractions;
using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Mongo;
using Microsoft.Extensions.Configuration;

namespace EventBench.Tests;

/// <summary>
/// Commit-semantics tests against a real MongoDB replica set. They only run when EB_MONGO_URL is set
/// (e.g. EB_MONGO_URL="mongodb://127.0.0.1:27017/?directConnection=true" after infra/mongo/up.sh);
/// otherwise they return immediately.
/// </summary>
public class MongoStoreIntegrationTests
{
    private static readonly string? Url = Environment.GetEnvironmentVariable("EB_MONGO_URL");

    private static MongoStore CreateStore(bool transactions)
    {
        var raw = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Workload:Groups:0:Name"] = "a",
            ["Workload:Groups:0:Count"] = "1",
            ["Workload:Groups:0:Sources"] = "4",
            ["Workload:Groups:0:RatePerSource"] = "1",
        }).Build();
        var topology = Topology.FromConfig(raw.Get<BenchConfig>()!);
        return new MongoStore(new MongoStoreOptions
        {
            ConnectionString = Url!, Database = "eventbench_test", CollectionPrefix = transactions ? "tx" : "emb",
            UseTransactions = transactions, ChunkSizeBytes = 256 * 1024,
        }, topology, _ => { });
    }

    private static CommitRequest Req(int src, long seq, long rec, MutationKind kind, int prev, int next,
        RecordStatus status = RecordStatus.Open, UploadInfo? upload = null)
        => new()
        {
            Event = new EventEnvelope { SourceId = src, Seq = seq, RecordId = rec, Op = OpType.Update },
            Kind = kind, PrevVersion = prev, NewVersion = next, NewHash = next * 31L, NewStatus = status,
            NewContext = kind == MutationKind.None ? null : new byte[] { (byte)next, 1, 2, 3 }, Upload = upload,
        };

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CommitOutcomesMatchPostgresSemantics(bool transactions)
    {
        if (string.IsNullOrEmpty(Url)) return;
        await using var store = CreateStore(transactions);
        await store.SetupAsync(CancellationToken.None);
        var ct = CancellationToken.None;

        Assert.Equal(CommitStatus.Applied, await store.CommitAsync(Req(0, 1, 100, MutationKind.Insert, 0, 1), ct));
        Assert.Equal(CommitStatus.Duplicate, await store.CommitAsync(Req(0, 1, 100, MutationKind.Insert, 0, 1), ct));
        Assert.Equal(CommitStatus.Gap, await store.CommitAsync(Req(0, 3, 100, MutationKind.Update, 1, 2), ct));
        // Version guard: nothing (not even the cursor) may change on a conflict.
        Assert.Equal(CommitStatus.RecordConflict, await store.CommitAsync(Req(0, 2, 100, MutationKind.Update, 7, 8), ct));
        Assert.Equal(CommitStatus.RecordConflict, await store.CommitAsync(Req(0, 2, 100, MutationKind.Insert, 0, 1), ct));
        var ctx = await store.LoadAsync(new EventEnvelope { SourceId = 0, Seq = 2, RecordId = 100 }, ct);
        Assert.Equal(1, ctx.LastSeq);
        Assert.True(ctx.RecordExists);
        Assert.Equal(1, ctx.Version);
        Assert.Equal(31L, ctx.Hash);
        Assert.Equal(new byte[] { 1, 1, 2, 3 }, ctx.Context);
        // The worker's re-commit after a conflict only advances the cursor.
        Assert.Equal(CommitStatus.Applied, await store.CommitAsync(Req(0, 2, 100, MutationKind.None, 0, 0), ct));
        Assert.Equal(CommitStatus.Applied, await store.CommitAsync(Req(0, 3, 100, MutationKind.Update, 1, 2, RecordStatus.Completed), ct));
        ctx = await store.LoadAsync(new EventEnvelope { SourceId = 0, Seq = 4, RecordId = 100 }, ct);
        Assert.Equal((3L, 2, RecordStatus.Completed), (ctx.LastSeq, ctx.Version, ctx.Status));
        ctx = await store.LoadAsync(new EventEnvelope { SourceId = 1, Seq = 1, RecordId = 100 }, ct);
        Assert.False(ctx.RecordExists); // record belongs to another source

        // Concurrent duplicate commits of the same seq: exactly one Applied, the rest Duplicate.
        var upload = new byte[600_000];
        new Random(3).NextBytes(upload);
        int version = 0;
        for (long seq = 1; seq <= 15; seq++)
        {
            var req = seq == 1
                ? Req(1, seq, 200, MutationKind.Insert, 0, 1)
                : Req(1, seq, 200, MutationKind.Update, version, version + 1,
                    upload: seq == 5 ? new UploadInfo(upload.Length, 42, upload) : null);
            var results = await Task.WhenAll(Enumerable.Range(0, 8).Select(_ => store.CommitAsync(req, ct).AsTask()));
            Assert.Equal(1, results.Count(r => r == CommitStatus.Applied));
            Assert.Equal(7, results.Count(r => r == CommitStatus.Duplicate));
            version++;
        }
        // The concurrent duplicates must actually have collided on the cursor (exercising the retry path).
        if (transactions) Assert.True(store.TxnRetries > 0, "expected write-conflict retries");

        var cursors = new Dictionary<int, long>();
        await foreach (var c in store.ScanCursorsAsync(ct)) cursors[c.SourceId] = c.LastSeq;
        Assert.Equal(new Dictionary<int, long> { [0] = 3, [1] = 15, [2] = 0, [3] = 0 }, cursors);
        var records = new List<RecordRow>();
        await foreach (var r in store.ScanRecordsAsync(ct)) records.Add(r);
        Assert.Equal(2, records.Count);
        Assert.Contains(new RecordRow(0, 100, RecordStatus.Completed, 2, 62), records);
        Assert.Contains(new RecordRow(1, 200, RecordStatus.Open, 15, 15 * 31L), records);
        var uploads = new List<UploadRow>();
        await foreach (var u in store.ScanUploadsAsync(ct)) uploads.Add(u);
        Assert.Equal([new UploadRow(1, 5, upload.Length, 42)], uploads);
    }

    [Fact]
    public async Task BlobsRoundTripThroughChunks()
    {
        if (string.IsNullOrEmpty(Url)) return;
        await using var store = CreateStore(transactions: true);
        await store.SetupAsync(CancellationToken.None);
        var data = new byte[3 * 1024 * 1024 + 17];
        new Random(5).NextBytes(data);
        await store.PutBlobAsync(9, 0, data, CancellationToken.None);
        await store.PutBlobAsync(9, 0, data, CancellationToken.None); // idempotent
        Assert.Equal(data, await store.GetBlobAsync(9, CancellationToken.None));
        Assert.Null(await store.GetBlobAsync(10, CancellationToken.None));
        await store.PutBlobAsync(11, 0, Array.Empty<byte>(), CancellationToken.None);
        Assert.Equal(Array.Empty<byte>(), await store.GetBlobAsync(11, CancellationToken.None));
    }
}
