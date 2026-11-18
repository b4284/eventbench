using EventBench.RabbitMq;

namespace EventBench.Tests;

/// <summary>Pure-logic tests of the RabbitMQ broker's ordered client-side retry and publisher-confirm tracking.</summary>
public class RabbitMqBookkeepingTests
{
    private sealed record Msg(int Source, long Seq);

    private static OrderedRedeliveryBuffer<Msg> Buffer(params (int Src, long Seq)[] msgs)
    {
        var b = new OrderedRedeliveryBuffer<Msg>();
        foreach (var (s, q) in msgs) b.Add(new Msg(s, q), s);
        return b;
    }

    private static List<OrderedRedeliveryBuffer<Msg>.Entry> Take(OrderedRedeliveryBuffer<Msg> b, int max, long now)
    {
        var list = new List<OrderedRedeliveryBuffer<Msg>.Entry>();
        b.Take(max, now, list);
        return list;
    }

    [Fact]
    public void HandsOutInOrderIncludingConsecutiveSameSource()
    {
        var b = Buffer((1, 1), (1, 2), (2, 1), (1, 3));
        var got = Take(b, 10, 0);
        Assert.Equal([(1, 1L), (1, 2L), (2, 1L), (1, 3L)], got.Select(e => (e.Item.Source, e.Item.Seq)));
        Assert.Empty(Take(b, 10, 0)); // all handed out, nothing settled
    }

    [Fact]
    public void NackBlocksOnlyThatSourceUntilRetryTime()
    {
        var b = Buffer((1, 1), (2, 1), (1, 2), (2, 2));
        var first = Take(b, 2, 0); // (1,1) (2,1)
        b.Nack(first[0], notBeforeUs: 1000);
        b.Ack(first[1]);

        var second = Take(b, 10, 10); // source 1 blocked; source 2 continues
        Assert.Equal([(2, 2L)], second.Select(e => (e.Item.Source, e.Item.Seq)));
        b.Ack(second[0]);
        Assert.Equal(1000, b.NextRetryUs(10));

        Assert.Empty(Take(b, 10, 999));
        var third = Take(b, 10, 1000); // retried head first, then its successor
        Assert.Equal([(1, 1L), (1, 2L)], third.Select(e => (e.Item.Source, e.Item.Seq)));
        Assert.Equal(2, third[0].HandOuts);
    }

    [Fact]
    public void HandedOutEntryBlocksItsSourceAcrossOverlappingTakes()
    {
        var b = Buffer((1, 1));
        var first = Take(b, 10, 0);
        b.Add(new Msg(1, 2), 1);
        b.Add(new Msg(3, 1), 3);
        Assert.Equal([3], Take(b, 10, 0).Select(e => e.Item.Source)); // (1,2) waits for (1,1)'s ack
        b.Ack(first[0]);
        Assert.Equal([(1, 2L)], Take(b, 10, 0).Select(e => (e.Item.Source, e.Item.Seq)));
    }

    [Fact]
    public void ClearInvalidatesEntries()
    {
        var b = Buffer((1, 1), (1, 2));
        var got = Take(b, 1, 0);
        b.Clear();
        Assert.False(b.Ack(got[0]));
        Assert.False(b.Nack(got[0], 5));
        Assert.Equal(0, b.Count);
    }

    /// <summary>
    /// Simulates WorkerRunner semantics against the buffer: batches are split into per-source lanes; a lane stops
    /// at the first failure and nacks the rest of the lane. Random worker failures plus injected nacks. Every
    /// handed-out message must be exactly the source's next sequence (or an already-applied duplicate never occurs here).
    /// </summary>
    [Theory]
    [InlineData(1, 0.05, 0.0)]
    [InlineData(2, 0.20, 0.0)]
    [InlineData(3, 0.0, 0.10)]
    [InlineData(4, 0.15, 0.15)]
    public void RandomizedNackRetryPreservesPerSourceOrder(int seed, double failP, double injectP)
    {
        var rng = new Random(seed);
        const int sources = 7, perSource = 300, prefetch = 40, batch = 10;
        var pending = new Queue<Msg>(); // broker queue order (interleaved sources, per-source in order)
        var next = new long[sources];
        for (int i = 0; i < sources * perSource; i++)
        {
            int s;
            do s = rng.Next(sources); while (next[s] >= perSource);
            pending.Enqueue(new Msg(s, ++next[s]));
        }

        var b = new OrderedRedeliveryBuffer<Msg>();
        var applied = new long[sources];
        long now = 0;
        int rounds = 0;
        while (applied.Sum() < sources * perSource)
        {
            Assert.True(++rounds < 1_000_000, "no progress");
            while (b.Count < prefetch && pending.Count > 0) { var m = pending.Dequeue(); b.Add(m, m.Source); } // broker push within prefetch
            var got = new List<OrderedRedeliveryBuffer<Msg>.Entry>();
            b.Take(batch, now, got, injectP > 0 ? _ => rng.NextDouble() < injectP : null, injectDelayUs: 300);

            foreach (var lane in got.GroupBy(e => e.Source))
            {
                bool failed = false;
                foreach (var e in lane)
                {
                    if (failed) { b.Nack(e, now + 250); continue; }
                    Assert.Equal(applied[e.Source] + 1, e.Item.Seq); // never a gap, never a reorder
                    if (rng.NextDouble() < failP) { failed = true; b.Nack(e, now + 250); continue; }
                    applied[e.Source] = e.Item.Seq;
                    Assert.True(b.Ack(e));
                }
            }
            now += got.Count == 0 ? Math.Max(1, Math.Min(b.NextRetryUs(now), now + 1000) - now) : 10;
        }
        Assert.All(applied, a => Assert.Equal(perSource, a));
        Assert.Equal(0, b.Count);
    }

    [Fact]
    public async Task ConfirmTrackerSettlesOnSingleAndMultipleAcks()
    {
        var t = new ConfirmTracker();
        for (ulong i = 1; i <= 5; i++) t.Add(i);
        var settled = t.WhenSettledAsync();
        t.Ack(3, multiple: true);
        t.Ack(5, multiple: false);
        Assert.False(settled.IsCompleted);
        Assert.Equal(1, t.Outstanding);
        t.Ack(4, multiple: false);
        await settled.WaitAsync(TimeSpan.FromSeconds(1));
        Assert.Null(t.TakeFailure());
        Assert.True(t.WhenSettledAsync().IsCompleted);
    }

    [Fact]
    public async Task ConfirmTrackerReportsNackReturnAndAbort()
    {
        var t = new ConfirmTracker();
        t.Add(1); t.Add(2);
        t.Nack(1, false);
        t.Ack(2, false);
        await t.WhenSettledAsync();
        Assert.Contains("nacked", t.TakeFailure());
        Assert.Null(t.TakeFailure());

        t.Add(3);
        t.Fail("returned");
        t.Ack(3, false);
        Assert.Equal("returned", t.TakeFailure());

        t.Add(4);
        var w = t.WhenSettledAsync();
        t.Abort("channel closed");
        await w.WaitAsync(TimeSpan.FromSeconds(1));
        Assert.Equal("channel closed", t.TakeFailure());
    }
}
