namespace EventBench.Kafka;

/// <summary>
/// Pure bookkeeping for one Kafka partition owned by one consumer (no Kafka types, unit-testable).
/// <list type="bullet">
/// <item>Offsets are handed out in increasing order (<see cref="Register"/>) and may be acked out of order
/// (different sources processed in parallel). <see cref="CommitPosition"/> only advances over the
/// <b>contiguous prefix</b> of acked offsets, so a committed offset never skips an unfinished message.</item>
/// <item>A nack at offset <c>o</c> invalidates every handed-out offset ≥ <c>o</c>: acked ones are forgotten
/// (they will be redelivered and dedup'd by the store cursor), outstanding ones become <i>stale</i>
/// (their eventual ack/nack is ignored but still awaited). The consumer must then re-seek the partition
/// to <see cref="RewindOffset"/> once nothing is outstanding and the nack delay has elapsed
/// (<see cref="TryTakeRewind"/>).</item>
/// <item>No new offsets may be handed out while anything is outstanding or a rewind is pending
/// (<see cref="CanHandOut"/>): this keeps the broker's per-source ordering contract even if the caller
/// asked for more before finishing the previous batch.</item>
/// </list>
/// Not thread-safe: callers serialize access.
/// </summary>
public sealed class PartitionOffsetTracker
{
    private enum State : byte { Outstanding, Acked, Stale, Dead }

    private struct Entry
    {
        public long Offset;
        public long Token;
        public State State;
    }

    // Entries in increasing offset order; [_head, _entries.Count) are live.
    private readonly List<Entry> _entries = new();
    private int _head;
    private long _lastRegistered = -1;

    /// <summary>Next offset to commit (= last contiguously acked offset + 1); -1 while nothing was acked.</summary>
    public long CommitPosition { get; private set; } = -1;

    /// <summary>Handed-out deliveries not yet acked/nacked (including stale ones invalidated by a nack).</summary>
    public int Outstanding { get; private set; }

    /// <summary>Lowest nacked offset still waiting for a re-seek, or -1.</summary>
    public long RewindOffset { get; private set; } = -1;

    /// <summary>Earliest time (µs) the rewind may be executed (nack delay).</summary>
    public long ResumeAtUs { get; private set; }

    public bool RewindPending => RewindOffset >= 0;

    public bool CanHandOut => Outstanding == 0 && !RewindPending;

    /// <summary>Count of nacks that triggered (or lowered) a rewind.</summary>
    public long Rewinds { get; private set; }

    /// <summary>Seed the commit position (e.g. the committed offset read at assignment); never moves it backwards.</summary>
    public void SeedCommitPosition(long offset)
    {
        if (offset > CommitPosition) CommitPosition = offset;
    }

    /// <summary>Record that <paramref name="offset"/> was handed out under <paramref name="token"/> (unique per delivery).</summary>
    public void Register(long offset, long token)
    {
        if (RewindPending) throw new InvalidOperationException("cannot hand out while a rewind is pending");
        if (offset <= _lastRegistered)
            throw new InvalidOperationException($"offsets must be handed out in increasing order ({offset} after {_lastRegistered})");
        _lastRegistered = offset;
        _entries.Add(new Entry { Offset = offset, Token = token, State = State.Outstanding });
        Outstanding++;
    }

    /// <returns>true if <see cref="CommitPosition"/> advanced.</returns>
    public bool Ack(long offset, long token)
    {
        int i = Find(offset, token);
        if (i < 0) return false;
        var e = _entries[i];
        switch (e.State)
        {
            case State.Outstanding:
                Outstanding--;
                e.State = State.Acked;
                _entries[i] = e;
                return Advance();
            case State.Stale:
                Outstanding--;
                e.State = State.Dead;
                _entries[i] = e;
                return false;
            default:
                return false; // double ack
        }
    }

    /// <summary>
    /// Nack <paramref name="offset"/>: it and everything handed out after it in this partition must be
    /// redelivered in order, not before <paramref name="resumeAtUs"/>.
    /// </summary>
    public void Nack(long offset, long token, long resumeAtUs)
    {
        int i = Find(offset, token);
        if (i < 0) return;
        var e = _entries[i];
        if (e.State == State.Stale)
        {
            // Already invalidated by an earlier nack at a lower offset: just resolve it.
            Outstanding--;
            e.State = State.Dead;
            _entries[i] = e;
            if (resumeAtUs > ResumeAtUs) ResumeAtUs = resumeAtUs;
            return;
        }
        if (e.State != State.Outstanding) return; // already acked: ignore a contradicting nack

        Outstanding--;
        e.State = State.Dead;
        _entries[i] = e;
        for (int j = i + 1; j < _entries.Count; j++)
        {
            var x = _entries[j];
            x.State = x.State switch
            {
                State.Outstanding => State.Stale,
                State.Acked => State.Dead,
                _ => x.State,
            };
            _entries[j] = x;
        }
        if (RewindOffset < 0 || offset < RewindOffset) RewindOffset = offset;
        if (resumeAtUs > ResumeAtUs) ResumeAtUs = resumeAtUs;
        Rewinds++;
    }

    /// <summary>
    /// If a rewind is pending, nothing is outstanding and the delay has elapsed: clears the invalidated
    /// entries and returns the offset to seek to. Afterwards offsets ≥ that one may be registered again.
    /// </summary>
    public bool TryTakeRewind(long nowUs, out long offset)
    {
        offset = RewindOffset;
        if (!RewindPending || Outstanding > 0 || nowUs < ResumeAtUs) return false;
        Advance();
        // Whatever remains is dead (≥ the rewind offset); the redelivered messages get fresh entries.
        _entries.Clear();
        _head = 0;
        _lastRegistered = offset - 1;
        RewindOffset = -1;
        return true;
    }

    private bool Advance()
    {
        bool moved = false;
        while (_head < _entries.Count && _entries[_head].State == State.Acked)
        {
            CommitPosition = _entries[_head].Offset + 1;
            _head++;
            moved = true;
        }
        if (_head > 64 && _head * 2 > _entries.Count)
        {
            _entries.RemoveRange(0, _head);
            _head = 0;
        }
        return moved;
    }

    private int Find(long offset, long token)
    {
        int lo = _head, hi = _entries.Count - 1;
        while (lo <= hi)
        {
            int mid = (lo + hi) >>> 1;
            long o = _entries[mid].Offset;
            if (o == offset) return _entries[mid].Token == token ? mid : -1;
            if (o < offset) lo = mid + 1; else hi = mid - 1;
        }
        return -1;
    }
}
