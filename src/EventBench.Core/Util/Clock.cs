using System.Diagnostics;

namespace EventBench.Core.Util;

/// <summary>
/// Monotonic, high resolution wall clock expressed as Unix epoch microseconds.
/// Anchored to UTC once at startup and advanced by <see cref="Stopwatch"/>, so it never jumps
/// backwards inside a process and stays roughly comparable across processes on the same host.
/// </summary>
public static class Clock
{
    private static readonly long AnchorUnixUs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 1000;
    private static readonly long AnchorTicks = Stopwatch.GetTimestamp();
    private static readonly double UsPerTick = 1_000_000.0 / Stopwatch.Frequency;

    public static long NowUs() => AnchorUnixUs + (long)((Stopwatch.GetTimestamp() - AnchorTicks) * UsPerTick);

    public static long ToUs(DateTime utc) => (utc.ToUniversalTime().Ticks - DateTime.UnixEpoch.Ticks) / 10;

    /// <summary>Busy-spins for the given number of microseconds (simulates CPU-bound work).</summary>
    public static void Spin(double micros)
    {
        if (micros <= 0) return;
        long end = Stopwatch.GetTimestamp() + (long)(micros * Stopwatch.Frequency / 1_000_000.0);
        while (Stopwatch.GetTimestamp() < end) Thread.SpinWait(20);
    }
}
