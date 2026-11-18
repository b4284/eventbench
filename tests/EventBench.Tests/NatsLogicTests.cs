using EventBench.Core.Model;
using EventBench.Nats;

namespace EventBench.Tests;

/// <summary>Pure logic of the NATS plug-ins (no server needed).</summary>
public class NatsLogicTests
{
    private static readonly KvRecordState S1 = new(RecordStatus.Open, 1, 111, [1, 2, 3]);
    private static readonly KvRecordState S2 = new(RecordStatus.Completed, 2, 222, null);

    [Fact]
    public void RecordCodecRoundTrips()
    {
        foreach (var v in new[]
                 {
                     new KvRecordValue(7, 42, S1, null),
                     new KvRecordValue(int.MaxValue, long.MaxValue, S2, S1),
                     new KvRecordValue(0, 1, new KvRecordState(RecordStatus.Open, 1, -5, []), null),
                 })
        {
            var d = KvRecordCodec.Decode(KvRecordCodec.Encode(v));
            Assert.Equal(v.SourceId, d.SourceId);
            Assert.Equal(v.AppliedSeq, d.AppliedSeq);
            AssertState(v.Current, d.Current);
            Assert.Equal(v.Previous is null, d.Previous is null);
            if (v.Previous is { } p) AssertState(p, d.Previous!.Value);
        }
        Assert.Equal(123456789L, KvRecordCodec.DecodeCursor(KvRecordCodec.EncodeCursor(123456789L)));
    }

    private static void AssertState(KvRecordState e, KvRecordState a)
    {
        Assert.Equal(e.Status, a.Status);
        Assert.Equal(e.Version, a.Version);
        Assert.Equal(e.Hash, a.Hash);
        Assert.Equal(e.Context, a.Context);
    }

    [Fact]
    public void VisibilityHidesUncommittedWrites()
    {
        // Committed: applied ≤ cursor → current state.
        Assert.Equal(KvVisibility.Visible, KvRecordCodec.Resolve(10, 7, new KvRecordValue(7, 10, S2, S1), out var s));
        Assert.Equal(2, s.Version);
        Assert.Equal(KvVisibility.Visible, KvRecordCodec.Resolve(12, 7, new KvRecordValue(7, 10, S2, S1), out s));
        Assert.Equal(2, s.Version);

        // Pending update (applied = cursor + 1) → pre-image.
        Assert.Equal(KvVisibility.Visible, KvRecordCodec.Resolve(9, 7, new KvRecordValue(7, 10, S2, S1), out s));
        Assert.Equal(1, s.Version);

        // Pending insert → absent.
        Assert.Equal(KvVisibility.Absent, KvRecordCodec.Resolve(9, 7, new KvRecordValue(7, 10, S1, null), out _));

        // Cursor read is stale (record two steps ahead) → retry.
        Assert.Equal(KvVisibility.Retry, KvRecordCodec.Resolve(8, 7, new KvRecordValue(7, 10, S2, S1), out _));

        // Missing record or record of another source → absent.
        Assert.Equal(KvVisibility.Absent, KvRecordCodec.Resolve(8, 7, null, out _));
        Assert.Equal(KvVisibility.Absent, KvRecordCodec.Resolve(20, 8, new KvRecordValue(7, 10, S2, S1), out _));
    }

    [Fact]
    public void PartitionsHaveExactlyOneOwner()
    {
        var g = new GroupInfo(0, "a", 100, 1, 3, 1000);
        int parts = NatsPartitioning.Partitions(g, 0);
        Assert.Equal(3, parts);
        Assert.Equal(7, NatsPartitioning.Partitions(g, 7));

        var owners = new int[7];
        for (int w = 0; w < 3; w++)
            foreach (var p in NatsPartitioning.Owned(7, 3, w)) owners[p]++;
        Assert.All(owners, n => Assert.Equal(1, n));

        // Deterministic, stable mapping relative to the group's first source.
        Assert.Equal(0, NatsPartitioning.PartitionOf(g, 1000, 7));
        Assert.Equal(1, NatsPartitioning.PartitionOf(g, 1008, 7));
    }

    [Fact]
    public void GatePausesThenRequiresResetAfterNack()
    {
        var gate = new PartitionGate();
        Assert.Equal(GateState.Open, gate.Check(0, out _));

        gate.OnNack(streamSeq: 50, nowUs: 1_000, delayUs: 250_000);
        gate.OnNack(streamSeq: 40, nowUs: 1_100, delayUs: 250_000); // a second lane failed too
        Assert.Equal(40UL, gate.FailedStreamSeq);
        Assert.Equal(GateState.Paused, gate.Check(200_000, out _));
        Assert.Equal(GateState.ResetDue, gate.Check(251_100, out long mark));

        // A nack that arrives after the reset decision keeps the gate closed.
        gate.OnNack(60, 260_000, 0);
        gate.OnResetDone(mark);
        Assert.Equal(GateState.ResetDue, gate.Check(260_001, out mark));
        gate.OnResetDone(mark);
        Assert.Equal(GateState.Open, gate.Check(260_002, out _));
        Assert.Equal(ulong.MaxValue, gate.FailedStreamSeq);
    }
}
