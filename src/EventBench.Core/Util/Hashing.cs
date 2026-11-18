using System.Buffers.Binary;
using System.IO.Hashing;
using System.Runtime.CompilerServices;

namespace EventBench.Core.Util;

/// <summary>Deterministic, order-sensitive hashing helpers shared by the workload generator and the workers.</summary>
public static class Hashing
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong SplitMix(ulong x)
    {
        x += 0x9E3779B97F4A7C15UL;
        x = (x ^ (x >> 30)) * 0xBF58476D1CE4E5B9UL;
        x = (x ^ (x >> 27)) * 0x94D049BB133111EBUL;
        return x ^ (x >> 31);
    }

    public static long Mix(long a, long b) => (long)SplitMix((ulong)a ^ SplitMix((ulong)b + 0x632BE59BD9B4E019UL));
    public static long Mix(long a, long b, long c) => Mix(Mix(a, b), c);
    public static long Mix(long a, long b, long c, long d) => Mix(Mix(Mix(a, b), c), d);

    public static long Checksum(ReadOnlySpan<byte> data) => data.IsEmpty ? 0 : (long)XxHash3.HashToUInt64(data);

    /// <summary>
    /// Fills an execution-context blob deterministically from the record hash.
    /// The first 8 bytes are the hash itself so a reader can validate the blob cheaply.
    /// </summary>
    public static void FillContext(Span<byte> ctx, long hash)
    {
        if (ctx.Length == 0) return;
        if (ctx.Length >= 8) BinaryPrimitives.WriteInt64LittleEndian(ctx, hash);
        var rng = new FastRandom((ulong)hash);
        if (ctx.Length > 8) rng.NextBytes(ctx[8..]);
        else if (ctx.Length < 8) rng.NextBytes(ctx);
    }

    public static bool ContextMatches(ReadOnlySpan<byte> ctx, long hash)
        => ctx.Length < 8 || BinaryPrimitives.ReadInt64LittleEndian(ctx) == hash;
}

/// <summary>Small, allocation-free xoshiro256** PRNG. Deterministic per seed.</summary>
public struct FastRandom
{
    private ulong _s0, _s1, _s2, _s3;

    public FastRandom(ulong seed)
    {
        _s0 = Hashing.SplitMix(seed);
        _s1 = Hashing.SplitMix(_s0);
        _s2 = Hashing.SplitMix(_s1);
        _s3 = Hashing.SplitMix(_s2);
        if ((_s0 | _s1 | _s2 | _s3) == 0) _s0 = 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong NextU64()
    {
        ulong result = BitOperations_RotateLeft(_s1 * 5, 7) * 9;
        ulong t = _s1 << 17;
        _s2 ^= _s0; _s3 ^= _s1; _s1 ^= _s2; _s0 ^= _s3; _s2 ^= t;
        _s3 = BitOperations_RotateLeft(_s3, 45);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong BitOperations_RotateLeft(ulong x, int k) => (x << k) | (x >> (64 - k));

    /// <summary>Uniform double in [0,1).</summary>
    public double NextDouble() => (NextU64() >> 11) * (1.0 / (1UL << 53));

    public int NextInt(int minInclusive, int maxExclusive)
        => maxExclusive <= minInclusive ? minInclusive : minInclusive + (int)(NextU64() % (ulong)(maxExclusive - minInclusive));

    public long NextLong() => (long)NextU64();

    public void NextBytes(Span<byte> buffer)
    {
        while (buffer.Length >= 8)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(buffer, NextU64());
            buffer = buffer[8..];
        }
        if (buffer.Length > 0)
        {
            ulong v = NextU64();
            for (int i = 0; i < buffer.Length; i++) { buffer[i] = (byte)v; v >>= 8; }
        }
    }
}
