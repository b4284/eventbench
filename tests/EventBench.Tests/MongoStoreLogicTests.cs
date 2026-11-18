using EventBench.Core.Abstractions;
using EventBench.Mongo;

namespace EventBench.Tests;

public class MongoStoreLogicTests
{
    [Theory]
    [InlineData(5, 5, CommitStatus.Duplicate)]
    [InlineData(9, 5, CommitStatus.Duplicate)]
    [InlineData(3, 5, CommitStatus.Gap)]
    [InlineData(0, 5, CommitStatus.Gap)]
    public void CursorMissIsDuplicateOrGap(long lastSeq, long seq, CommitStatus expected)
        => Assert.Equal(expected, MongoStoreLogic.ClassifyCursorMiss(lastSeq, seq));

    [Fact]
    public void CursorAtSeqMinusOneIsNotClassified()
        => Assert.Null(MongoStoreLogic.ClassifyCursorMiss(4, 5));

    [Fact]
    public void EmbeddedMissFollowsCommitFunctionPrecedence()
    {
        // Duplicate wins over everything, then gap, then record conflict.
        Assert.Equal(CommitStatus.Duplicate, MongoStoreLogic.ClassifyEmbeddedMiss(5, 5, MutationKind.Update, false, 0, 3));
        Assert.Equal(CommitStatus.Gap, MongoStoreLogic.ClassifyEmbeddedMiss(2, 5, MutationKind.Update, false, 0, 3));
        Assert.Equal(CommitStatus.RecordConflict, MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.Update, true, 4, 3));
        Assert.Equal(CommitStatus.RecordConflict, MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.Update, false, 0, 3));
        Assert.Equal(CommitStatus.RecordConflict, MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.Insert, true, 1, 0));
        // Everything matches now: the miss was transient, caller retries.
        Assert.Null(MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.Update, true, 3, 3));
        Assert.Null(MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.Insert, false, 0, 0));
        Assert.Null(MongoStoreLogic.ClassifyEmbeddedMiss(4, 5, MutationKind.None, true, 7, 0));
    }

    [Theory]
    [InlineData(10, null, false)]
    [InlineData(10, 10L, false)]
    [InlineData(10, 3L, false)]
    [InlineData(10, 11L, true)]
    public void TornLoadDetection(long cursor, long? recordUpdatedSeq, bool torn)
        => Assert.Equal(torn, MongoStoreLogic.IsTornLoad(cursor, recordUpdatedSeq));

    [Fact]
    public void KeysAreDeterministicAndDistinct()
    {
        Assert.Equal("u:12:345", MongoStoreLogic.UploadOwner(12, 345));
        Assert.NotEqual(MongoStoreLogic.UploadOwner(1, 23), MongoStoreLogic.UploadOwner(12, 3));
        Assert.Equal("b:77", MongoStoreLogic.BlobOwner(77));
        Assert.Equal("u:1:2/3", MongoStoreLogic.ChunkId(MongoStoreLogic.UploadOwner(1, 2), 3));
    }

    [Theory]
    [InlineData(0, 4, 0)]
    [InlineData(1, 4, 1)]
    [InlineData(4, 4, 1)]
    [InlineData(5, 4, 2)]
    [InlineData(16 * 1048576, 1048576, 16)]
    [InlineData(16 * 1048576 + 1, 1048576, 17)]
    public void ChunkCount(int length, int chunk, int expected)
        => Assert.Equal(expected, MongoStoreLogic.ChunkCount(length, chunk));

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(1000)]
    [InlineData(1024)]
    [InlineData(5000)]
    public void SplitJoinRoundTrips(int size)
    {
        var data = new byte[size];
        new Random(size).NextBytes(data);
        var chunks = MongoStoreLogic.Split(data, 1024).Select((c, i) => (i, c.ToArray())).ToList();
        Assert.Equal(MongoStoreLogic.ChunkCount(size, 1024), chunks.Count);
        Assert.All(chunks, c => Assert.True(c.Item2.Length is > 0 and <= 1024));
        chunks.Reverse(); // order must not matter
        Assert.Equal(data, MongoStoreLogic.Join(chunks, chunks.Count, size));
    }

    [Fact]
    public void JoinRejectsIncompleteOrCorruptSets()
    {
        var data = new byte[3000];
        var chunks = MongoStoreLogic.Split(data, 1024).Select((c, i) => (i, c.ToArray())).ToList();
        Assert.Null(MongoStoreLogic.Join(chunks.Take(2).ToList(), 3, 3000));             // missing chunk
        Assert.Null(MongoStoreLogic.Join([chunks[0], chunks[0], chunks[2]], 3, 3000));   // duplicated chunk
        Assert.Null(MongoStoreLogic.Join(chunks, 3, 2999));                              // size mismatch
    }

    [Fact]
    public void BackoffIsBoundedAndPositive()
    {
        var rng = new Random(1);
        for (int a = 0; a < 50; a++)
        {
            int ms = MongoStoreLogic.BackoffMs(a, rng);
            Assert.InRange(ms, 1, 100);
        }
    }
}
