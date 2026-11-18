using EventBench.Kafka;

namespace EventBench.Tests;

public class KafkaOffsetTrackerTests
{
    private static PartitionOffsetTracker Handed(params long[] offsets)
    {
        var t = new PartitionOffsetTracker();
        foreach (var o in offsets) t.Register(o, Tok(o));
        return t;
    }

    private static long Tok(long offset, int generation = 0) => offset * 10 + generation;

    [Fact]
    public void InOrderAcksAdvanceCommitPosition()
    {
        var t = Handed(0, 1, 2);
        Assert.Equal(-1, t.CommitPosition);
        Assert.True(t.Ack(0, Tok(0)));
        Assert.Equal(1, t.CommitPosition);
        Assert.True(t.Ack(1, Tok(1)));
        Assert.True(t.Ack(2, Tok(2)));
        Assert.Equal(3, t.CommitPosition);
        Assert.True(t.CanHandOut);
    }

    [Fact]
    public void OutOfOrderAcksCommitOnlyTheContiguousPrefix()
    {
        var t = Handed(10, 11, 12, 15); // gaps in offsets are legal
        Assert.False(t.Ack(12, Tok(12)));
        Assert.False(t.Ack(15, Tok(15)));
        Assert.Equal(-1, t.CommitPosition);
        Assert.False(t.CanHandOut);
        Assert.False(t.Ack(11, Tok(11)));
        Assert.True(t.Ack(10, Tok(10)));
        Assert.Equal(16, t.CommitPosition);
        Assert.Equal(0, t.Outstanding);
    }

    [Fact]
    public void NackRewindsAndInvalidatesEverythingAfterIt()
    {
        var t = Handed(10, 11, 12, 13, 14);
        t.Ack(10, Tok(10));
        t.Ack(12, Tok(12));            // another source, acked before the nack
        t.Nack(11, Tok(11), resumeAtUs: 1000);
        Assert.True(t.RewindPending);
        Assert.Equal(11, t.RewindOffset);
        Assert.Equal(11, t.CommitPosition);
        Assert.False(t.CanHandOut);

        // 13 and 14 are still being processed: no rewind until they resolve, and their acks don't count.
        Assert.False(t.TryTakeRewind(2000, out _));
        Assert.False(t.Ack(13, Tok(13)));
        t.Nack(14, Tok(14), resumeAtUs: 1500);
        Assert.Equal(11, t.CommitPosition);

        Assert.False(t.TryTakeRewind(1400, out _)); // nack delay (max of both) not yet elapsed
        Assert.True(t.TryTakeRewind(1500, out long seek));
        Assert.Equal(11, seek);
        Assert.True(t.CanHandOut);

        // Redelivery from 11 with fresh tokens; stale tokens are ignored.
        t.Register(11, Tok(11, 1));
        t.Register(12, Tok(12, 1));
        Assert.False(t.Ack(11, Tok(11)));
        Assert.True(t.Ack(11, Tok(11, 1)));
        Assert.True(t.Ack(12, Tok(12, 1)));
        Assert.Equal(13, t.CommitPosition);
    }

    [Fact]
    public void LowerSecondNackLowersTheRewindOffset()
    {
        var t = Handed(1, 2, 3, 4);
        t.Nack(3, Tok(3), 0);
        t.Nack(4, Tok(4), 0);          // stale (lane rest) – just resolved
        t.Nack(2, Tok(2), 0);          // a parallel lane failed earlier in the partition
        t.Ack(1, Tok(1));
        Assert.True(t.TryTakeRewind(0, out long seek));
        Assert.Equal(2, seek);
        Assert.Equal(2, t.CommitPosition);
    }

    [Fact]
    public void HandingOutOutOfOrderOrDuringRewindIsRejected()
    {
        var t = Handed(5);
        Assert.Throws<InvalidOperationException>(() => t.Register(5, 99));
        t.Nack(5, Tok(5), 0);
        Assert.Throws<InvalidOperationException>(() => t.Register(6, 99));
    }

    [Fact]
    public void RandomizedCommitNeverPassesAnUnfinishedOffset()
    {
        var rng = new Random(7);
        for (int run = 0; run < 200; run++)
        {
            var t = new PartitionOffsetTracker();
            var done = new HashSet<long>();   // offsets whose effect is final (acked in the current generation)
            long next = 0, token = 0, last = -1;
            const long total = 60;
            int guard = 0;
            while (t.CommitPosition < total && guard++ < 10_000)
            {
                if (t.TryTakeRewind(long.MaxValue, out long seek))
                {
                    next = seek;
                    foreach (var o in done.Where(o => o >= seek).ToList()) done.Remove(o);
                }
                if (!t.CanHandOut) Assert.Fail("tracker blocked with nothing outstanding");
                var batch = new List<(long Off, long Tok)>();
                int n = rng.Next(1, 8);
                for (int i = 0; i < n && next < total; i++)
                {
                    t.Register(next, ++token);
                    batch.Add((next++, token));
                }
                // Resolve in random order (parallel lanes); sometimes nack.
                foreach (var (off, tok) in batch.OrderBy(_ => rng.Next()))
                {
                    if (rng.NextDouble() < 0.1) t.Nack(off, tok, 0);
                    else
                    {
                        t.Ack(off, tok);
                        done.Add(off);
                    }
                    long pos = t.CommitPosition;
                    Assert.True(pos >= last, "commit position moved backwards");
                    last = pos;
                    // Everything below the commit position must have been acked in its latest delivery.
                    for (long o = 0; o < pos; o++) Assert.Contains(o, done);
                }
            }
            Assert.Equal(total, t.CommitPosition);
        }
    }
}
