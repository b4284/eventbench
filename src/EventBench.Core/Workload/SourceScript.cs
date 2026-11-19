using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Processing;
using EventBench.Core.Util;

namespace EventBench.Core.Workload;

/// <summary>
/// Deterministic event script for one source plus the expected ("oracle") state it implies.
/// Owned by exactly one producer task, so it needs no synchronization while generating.
/// </summary>
public sealed class SourceScript
{
    private struct OpenRecord
    {
        public long Id;
        public int Version;
        public long Hash;
    }

    private readonly List<OpenRecord> _open = new(4);
    private FastRandom _rng;
    // Separate stream so enabling slow events does not change the rest of the script.
    private FastRandom _slowRng;
    private int _recordCounter;

    public int SourceId { get; }
    public int GroupId { get; }

    public long NextSeq { get; private set; } = 1;
    public long LastSeq => NextSeq - 1;

    // Expected end state.
    public long RecordsCreated { get; private set; }
    public long RecordsCompleted { get; private set; }
    public long CompletedFold { get; private set; }
    public long Uploads { get; private set; }
    public long UploadFold { get; private set; }
    public int OpenRecords => _open.Count;

    private readonly GroupInfo _group;

    public SourceScript(int sourceId, GroupInfo group, int seed)
    {
        SourceId = sourceId;
        GroupId = group.Id;
        _group = group;
        _rng = new FastRandom(Hashing.SplitMix((ulong)seed) ^ Hashing.SplitMix((ulong)sourceId + 0x5151));
        _slowRng = new FastRandom(Hashing.SplitMix((ulong)seed + 0x51077) ^ Hashing.SplitMix((ulong)sourceId + 0x7a7a));
    }

    public EventEnvelope Next(long scheduledAtUs, WorkloadPlan plan)
    {
        var mix = plan.Options.OpMix;
        OpType op = ChooseOp(mix);

        // Random sprinkled large events (Workload:LargePayload) ...
        bool large = plan.TryTakeLarge(ref _rng);
        if (large && op is OpType.Get or OpType.Complete)
            op = _open.Count > 0 ? OpType.Update : OpType.Create;
        // ... or the group's own profile: uploads ride on Create/Update, Get/Complete stay small.
        if (!large && _group.LargeEventFraction > 0 && op is OpType.Create or OpType.Update)
            large = _rng.NextDouble() < _group.LargeEventFraction;

        int size;
        if (large)
        {
            int min = _group.LargeMinBytes ?? plan.Options.LargePayload.MinBytes;
            int max = Math.Max(min, _group.LargeMaxBytes ?? plan.Options.LargePayload.MaxBytes);
            size = _rng.NextInt(min, max + 1);
        }
        else size = SmallPayload(plan);

        var ev = NewEvent(op, scheduledAtUs, size, large ? EventFlags.Large : EventFlags.None);
        var slow = plan.Options.SlowEvents;
        if ((slow.Fraction > 0 && _slowRng.NextDouble() < slow.Fraction) || (large && slow.IncludeLarge))
            ev.SlowMs = _slowRng.NextInt(slow.MinMs, Math.Max(slow.MinMs, slow.MaxMs) + 1);

        switch (op)
        {
            case OpType.Create:
            {
                long id = StateMachine.MakeRecordId(SourceId, ++_recordCounter);
                var (v, h) = StateMachine.Create(id, ev.Seq, ev.Delta, ev.PayloadChecksum);
                ev.RecordId = id;
                ev.ExpectedVersion = 0;
                ev.ExpectedHash = 0;
                _open.Add(new OpenRecord { Id = id, Version = v, Hash = h });
                RecordsCreated++;
                break;
            }
            case OpType.Get:
            {
                int idx = _rng.NextInt(0, _open.Count);
                var r = _open[idx];
                ev.RecordId = r.Id; ev.ExpectedVersion = r.Version; ev.ExpectedHash = r.Hash;
                break;
            }
            case OpType.Update:
            {
                int idx = _rng.NextInt(0, _open.Count);
                var r = _open[idx];
                ev.RecordId = r.Id; ev.ExpectedVersion = r.Version; ev.ExpectedHash = r.Hash;
                (r.Version, r.Hash) = StateMachine.Update(r.Version, r.Hash, ev.Seq, ev.Delta, ev.PayloadChecksum);
                _open[idx] = r;
                break;
            }
            case OpType.Complete:
            {
                int idx = _rng.NextInt(0, _open.Count);
                CompleteAt(idx, ev);
                break;
            }
        }

        if (large)
        {
            Uploads++;
            UploadFold += StateMachine.UploadFold(ev.Seq, ev.PayloadSize, ev.PayloadChecksum);
        }
        return ev;
    }

    /// <summary>Emits Complete events for every still-open record so the run ends with all records completed.</summary>
    public void Close(List<EventEnvelope> output, long scheduledAtUs, WorkloadPlan plan)
    {
        while (_open.Count > 0)
        {
            var ev = NewEvent(OpType.Complete, scheduledAtUs, SmallPayload(plan), EventFlags.Closing);
            CompleteAt(_open.Count - 1, ev);
            output.Add(ev);
        }
    }

    private int SmallPayload(WorkloadPlan plan) => _group.PayloadBytes ?? plan.Options.PayloadBytes;

    private void CompleteAt(int idx, EventEnvelope ev)
    {
        var r = _open[idx];
        ev.RecordId = r.Id; ev.ExpectedVersion = r.Version; ev.ExpectedHash = r.Hash;
        var (v, h) = StateMachine.Complete(r.Version, r.Hash, ev.Seq);
        _open.RemoveAt(idx);
        RecordsCompleted++;
        CompletedFold += StateMachine.RecordFold(r.Id, v, h);
    }

    private EventEnvelope NewEvent(OpType op, long scheduledAtUs, int payloadSize, EventFlags flags)
    {
        byte[]? payload = payloadSize > 0 ? GC.AllocateUninitializedArray<byte>(payloadSize) : null;
        if (payload != null) _rng.NextBytes(payload);
        return new EventEnvelope
        {
            Group = GroupId,
            SourceId = SourceId,
            Seq = NextSeq++,
            Op = op,
            Delta = _rng.NextLong(),
            ScheduledAtUs = scheduledAtUs,
            Flags = flags,
            PayloadSize = payloadSize,
            Payload = payload,
            PayloadChecksum = payload is null ? 0 : Hashing.Checksum(payload),
        };
    }

    private OpType ChooseOp(OpMixOptions mix)
    {
        if (_open.Count == 0) return OpType.Create;
        double c = _open.Count < mix.MaxOpenRecordsPerSource ? mix.CreateWeight : 0;
        double g = mix.GetWeight, u = mix.UpdateWeight, x = mix.CompleteWeight;
        double total = c + g + u + x;
        if (total <= 0) return OpType.Update;
        double pick = _rng.NextDouble() * total;
        if ((pick -= c) < 0) return OpType.Create;
        if ((pick -= g) < 0) return OpType.Get;
        if ((pick -= u) < 0) return OpType.Update;
        return OpType.Complete;
    }
}
