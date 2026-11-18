using System.Buffers.Binary;
using EventBench.Core.Model;

namespace EventBench.Nats;

/// <summary>One version of a record's state.</summary>
public readonly record struct KvRecordState(RecordStatus Status, int Version, long Hash, byte[]? Context);

/// <summary>
/// Value of a record key. <see cref="AppliedSeq"/> is the source sequence whose mutation produced
/// <see cref="Current"/>; <see cref="Previous"/> is the pre-image (null = the record did not exist before,
/// i.e. AppliedSeq was its Create). The write becomes visible only once the source cursor reaches
/// AppliedSeq (the cursor CAS is the commit point); until then readers see <see cref="Previous"/>.
/// </summary>
public sealed record KvRecordValue(int SourceId, long AppliedSeq, KvRecordState Current, KvRecordState? Previous);

public enum KvVisibility
{
    /// <summary>The record does not exist as of the cursor.</summary>
    Absent,
    /// <summary><c>State</c> holds the committed state as of the cursor.</summary>
    Visible,
    /// <summary>The cursor read is stale (the record is more than one committed step ahead): read again.</summary>
    Retry,
}

public static class KvRecordCodec
{
    private const byte Format = 1;

    public static byte[] Encode(KvRecordValue v)
    {
        int size = 1 + 4 + 8 + StateSize(v.Current) + 1 + (v.Previous is { } p ? StateSize(p) : 0);
        var buf = new byte[size];
        var span = buf.AsSpan();
        span[0] = Format;
        BinaryPrimitives.WriteInt32LittleEndian(span[1..], v.SourceId);
        BinaryPrimitives.WriteInt64LittleEndian(span[5..], v.AppliedSeq);
        int off = 13;
        off += WriteState(span[off..], v.Current);
        span[off++] = v.Previous is null ? (byte)0 : (byte)1;
        if (v.Previous is { } prev) off += WriteState(span[off..], prev);
        return buf;
    }

    public static KvRecordValue Decode(ReadOnlySpan<byte> span)
    {
        if (span.Length < 14 || span[0] != Format) throw new FormatException("bad record value");
        int src = BinaryPrimitives.ReadInt32LittleEndian(span[1..]);
        long applied = BinaryPrimitives.ReadInt64LittleEndian(span[5..]);
        int off = 13;
        var cur = ReadState(span[off..], out int n);
        off += n;
        KvRecordState? prev = null;
        if (span[off++] == 1) prev = ReadState(span[off..], out _);
        return new KvRecordValue(src, applied, cur, prev);
    }

    public static byte[] EncodeCursor(long lastSeq)
    {
        var b = new byte[8];
        BinaryPrimitives.WriteInt64LittleEndian(b, lastSeq);
        return b;
    }

    public static long DecodeCursor(ReadOnlySpan<byte> span)
        => span.Length == 8 ? BinaryPrimitives.ReadInt64LittleEndian(span) : throw new FormatException("bad cursor value");

    /// <summary>
    /// Snapshot rule used by Load and the oracle scans. The cursor must be read <b>before</b> the record.
    /// Because a record write for seq s only happens while the cursor is s-1 and the cursor only advances
    /// after that write, a record with AppliedSeq ≤ cursor is committed and current as of the cursor; one
    /// with AppliedSeq = cursor+1 is an uncommitted (or just-committed-after-our-read) write whose
    /// pre-image is the state as of the cursor; anything further ahead means the cursor read is stale.
    /// </summary>
    public static KvVisibility Resolve(long cursor, int sourceId, KvRecordValue? rec, out KvRecordState state)
    {
        state = default;
        if (rec is null || rec.SourceId != sourceId) return KvVisibility.Absent;
        if (rec.AppliedSeq <= cursor)
        {
            state = rec.Current;
            return KvVisibility.Visible;
        }
        if (rec.AppliedSeq == cursor + 1)
        {
            if (rec.Previous is not { } prev) return KvVisibility.Absent;
            state = prev;
            return KvVisibility.Visible;
        }
        return KvVisibility.Retry;
    }

    private static int StateSize(KvRecordState s) => 2 + 4 + 8 + 4 + (s.Context?.Length ?? 0);

    private static int WriteState(Span<byte> span, KvRecordState s)
    {
        BinaryPrimitives.WriteInt16LittleEndian(span, (short)s.Status);
        BinaryPrimitives.WriteInt32LittleEndian(span[2..], s.Version);
        BinaryPrimitives.WriteInt64LittleEndian(span[6..], s.Hash);
        BinaryPrimitives.WriteInt32LittleEndian(span[14..], s.Context?.Length ?? -1);
        s.Context?.CopyTo(span[18..]);
        return StateSize(s);
    }

    private static KvRecordState ReadState(ReadOnlySpan<byte> span, out int read)
    {
        var status = (RecordStatus)BinaryPrimitives.ReadInt16LittleEndian(span);
        int version = BinaryPrimitives.ReadInt32LittleEndian(span[2..]);
        long hash = BinaryPrimitives.ReadInt64LittleEndian(span[6..]);
        int len = BinaryPrimitives.ReadInt32LittleEndian(span[14..]);
        byte[]? ctx = len < 0 ? null : span.Slice(18, len).ToArray();
        read = 18 + Math.Max(0, len);
        return new KvRecordState(status, version, hash, ctx);
    }
}
