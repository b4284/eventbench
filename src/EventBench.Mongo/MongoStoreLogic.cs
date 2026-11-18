using EventBench.Core.Abstractions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventBench.Tests")]

namespace EventBench.Mongo;

/// <summary>Pure (driver-independent) decisions and key layouts used by <see cref="MongoStore"/>.</summary>
public static class MongoStoreLogic
{
    /// <summary>
    /// The conditional cursor write <c>{_id: source, lastSeq: seq-1}</c> matched nothing; classify from the
    /// cursor value read in the same snapshot. Returns null when the cursor is in fact at seq-1 (the caller
    /// must then retry: the state moved between the write and the read).
    /// </summary>
    public static CommitStatus? ClassifyCursorMiss(long lastSeq, long seq)
        => lastSeq >= seq ? CommitStatus.Duplicate
            : lastSeq != seq - 1 ? CommitStatus.Gap
            : null;

    /// <summary>
    /// Embedded mode: the single-document conditional update (cursor + record guard) matched nothing.
    /// Classify from a re-read of the source document. Same precedence as the Postgres commit function:
    /// duplicate, then gap, then record conflict. Returns null when everything matches now (retry).
    /// </summary>
    public static CommitStatus? ClassifyEmbeddedMiss(long lastSeq, long seq, MutationKind kind,
        bool recordExists, int recordVersion, int prevVersion)
    {
        if (ClassifyCursorMiss(lastSeq, seq) is { } s) return s;
        bool recordOk = kind switch
        {
            MutationKind.Insert => !recordExists,
            MutationKind.Update => recordExists && recordVersion == prevVersion,
            _ => true,
        };
        return recordOk ? null : CommitStatus.RecordConflict;
    }

    /// <summary>
    /// Load reads the cursor and the record with two parallel queries. Every commit at seq k sets the
    /// record's updatedSeq = k and the cursor to k atomically, so a consistent view always has
    /// record.updatedSeq &lt;= cursor.lastSeq. A larger value means the cursor was read before a commit the
    /// record read already saw (torn read): the caller re-reads record-then-cursor sequentially.
    /// </summary>
    public static bool IsTornLoad(long cursorLastSeq, long? recordUpdatedSeq)
        => recordUpdatedSeq is { } u && u > cursorLastSeq;

    /// <summary>Owner key of an upload's data (and the _id of its metadata document): unique per (source, seq).</summary>
    public static string UploadOwner(int sourceId, long seq) => $"u:{sourceId}:{seq}";

    /// <summary>Owner key of a claim-check blob's data.</summary>
    public static string BlobOwner(long blobId) => $"b:{blobId}";

    /// <summary>_id of chunk <paramref name="n"/> of an owner. Deterministic, so chunk writes are idempotent upserts.</summary>
    public static string ChunkId(string owner, int n) => $"{owner}/{n}";

    public static int ChunkCount(int length, int chunkSize)
    {
        if (chunkSize <= 0) throw new ArgumentOutOfRangeException(nameof(chunkSize));
        return length <= 0 ? 0 : (int)(((long)length + chunkSize - 1) / chunkSize);
    }

    /// <summary>Splits <paramref name="data"/> into consecutive slices of at most <paramref name="chunkSize"/> bytes.</summary>
    public static IEnumerable<ReadOnlyMemory<byte>> Split(ReadOnlyMemory<byte> data, int chunkSize)
    {
        int count = ChunkCount(data.Length, chunkSize);
        for (int i = 0; i < count; i++)
        {
            int start = i * chunkSize;
            yield return data.Slice(start, Math.Min(chunkSize, data.Length - start));
        }
    }

    /// <summary>
    /// Reassembles chunks (any order). Returns null if a chunk is missing or duplicated, or the total size
    /// differs from <paramref name="expectedSize"/> (a partially written or corrupt object).
    /// </summary>
    public static byte[]? Join(IReadOnlyList<(int N, byte[] Data)> chunks, int expectedCount, int expectedSize)
    {
        if (chunks.Count != expectedCount) return null;
        var ordered = chunks.OrderBy(c => c.N).ToArray();
        long total = 0;
        for (int i = 0; i < ordered.Length; i++)
        {
            if (ordered[i].N != i) return null;
            total += ordered[i].Data.Length;
        }
        if (total != expectedSize) return null;
        var result = new byte[expectedSize];
        int pos = 0;
        foreach (var c in ordered)
        {
            c.Data.CopyTo(result, pos);
            pos += c.Data.Length;
        }
        return result;
    }

    /// <summary>Jittered exponential backoff for transient transaction errors (write conflicts), in ms.</summary>
    public static int BackoffMs(int attempt, Random rng)
    {
        int cap = Math.Min(100, 1 << Math.Min(attempt, 7));
        return 1 + rng.Next(cap);
    }
}
