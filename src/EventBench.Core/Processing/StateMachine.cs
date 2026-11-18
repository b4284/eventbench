using EventBench.Core.Util;

namespace EventBench.Core.Processing;

/// <summary>
/// The pure record state transition. Used both by the generator (to compute the expected state)
/// and by the workers (to compute the actual state from whatever they loaded). Hashes are
/// order-sensitive, so any reordering, skipped or doubly-applied event changes the final hash.
/// </summary>
public static class StateMachine
{
    private const long CompleteTag = 0x434F_4D50_4C45_5445; // "COMPLETE"

    public static (int Version, long Hash) Create(long recordId, long seq, long delta, long payloadChecksum)
        => (1, Hashing.Mix(recordId, seq, delta, payloadChecksum));

    public static (int Version, long Hash) Update(int version, long hash, long seq, long delta, long payloadChecksum)
        => (version + 1, Hashing.Mix(hash, seq, delta, payloadChecksum));

    public static (int Version, long Hash) Complete(int version, long hash, long seq)
        => (version + 1, Hashing.Mix(hash, seq, CompleteTag));

    /// <summary>Order-independent per-record contribution to a source-level fold (summed with wrap-around).</summary>
    public static long RecordFold(long recordId, int version, long hash) => Hashing.Mix(recordId, version, hash);

    public static long UploadFold(long seq, int size, long checksum) => Hashing.Mix(seq, size, checksum);

    /// <summary>Record ids are globally unique: source id in the high bits, per-source counter in the low bits.</summary>
    public static long MakeRecordId(int sourceId, int counter) => ((long)sourceId << 32) | (uint)counter;

    public static long MakeBlobId(int sourceId, long seq) => ((long)sourceId << 40) | (seq & 0xFF_FFFF_FFFF);
}
