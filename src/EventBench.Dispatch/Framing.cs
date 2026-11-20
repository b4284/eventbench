using System.Buffers;
using System.Buffers.Binary;
using System.Threading.Channels;
using Google.Protobuf;

namespace EventBench.Dispatch;

/// <summary>Length-prefixed Protobuf frames over a stream (4-byte little-endian length + message).</summary>
internal static class Framing
{
    public const int MaxFrame = 256 << 20;

    /// <summary>Reads one frame; null at a clean end of stream.</summary>
    public static async ValueTask<T?> ReadAsync<T>(Stream s, MessageParser<T> parser, byte[] header, CancellationToken ct)
        where T : class, IMessage<T>
    {
        int got = 0;
        while (got < 4)
        {
            int n = await s.ReadAsync(header.AsMemory(got, 4 - got), ct);
            if (n == 0)
            {
                if (got == 0) return null;
                throw new EndOfStreamException("truncated frame header");
            }
            got += n;
        }
        int len = BinaryPrimitives.ReadInt32LittleEndian(header);
        if (len < 0 || len > MaxFrame) throw new InvalidDataException($"bad frame length {len}");
        var buf = ArrayPool<byte>.Shared.Rent(Math.Max(1, len));
        try
        {
            await s.ReadExactlyAsync(buf.AsMemory(0, len), ct);
            return parser.ParseFrom(new ReadOnlySpan<byte>(buf, 0, len));
        }
        finally { ArrayPool<byte>.Shared.Return(buf); }
    }

    public static async ValueTask WriteAsync(Stream s, IMessage m, CancellationToken ct)
    {
        int len = m.CalculateSize();
        var buf = ArrayPool<byte>.Shared.Rent(len + 4);
        try
        {
            BinaryPrimitives.WriteInt32LittleEndian(buf, len);
            m.WriteTo(new Span<byte>(buf, 4, len));
            await s.WriteAsync(buf.AsMemory(0, len + 4), ct);
        }
        finally { ArrayPool<byte>.Shared.Return(buf); }
    }

    /// <summary>
    /// Drains an outbox into frames: each frame carries everything queued while the previous frame was
    /// being written (no timer), capped at about <paramref name="maxBytes"/>.
    /// </summary>
    public static async Task WriteLoopAsync<TItem, TMsg>(ChannelReader<TItem> outbox, Stream s, Func<TMsg> create,
        Func<TMsg, TItem, int> add, int maxBytes, CancellationToken ct) where TMsg : IMessage
    {
        while (await outbox.WaitToReadAsync(ct))
        {
            var msg = create();
            int bytes = 0;
            while (bytes < maxBytes && outbox.TryRead(out var item)) bytes += add(msg, item);
            if (bytes > 0) await WriteAsync(s, msg, ct);
        }
    }
}
