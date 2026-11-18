namespace EventBench.RabbitMq;

/// <summary>
/// Client-side holding area for the unacknowledged deliveries of one consumer channel, kept in broker
/// delivery order. This is what makes nack/retry order-preserving without any broker-side requeue:
/// <list type="bullet">
/// <item>A nack does not go to the broker. The message stays unacked there (holding a prefetch slot) and is
/// marked <see cref="EntryState.Delayed"/> here, in its original position.</item>
/// <item><see cref="Take"/> walks the entries in order and hands out an entry only if no earlier entry of
/// the same source is still handed out or delayed ("blocked source"). So a source's later messages can never
/// overtake an unsettled earlier one, while other sources of the same partition keep flowing.</item>
/// <item>An ack removes the entry. A retried entry is handed out again from its original position.</item>
/// </list>
/// Not thread-safe; the consumer serialises access.
/// </summary>
public sealed class OrderedRedeliveryBuffer<T>
{
    public enum EntryState { Ready, HandedOut, Delayed }

    public sealed class Entry
    {
        internal LinkedListNode<Entry>? Node;
        internal Entry(T item, int source) { Item = item; Source = source; }
        public T Item { get; }
        public int Source { get; }
        public EntryState State { get; internal set; }
        public long NotBeforeUs { get; internal set; }
        /// <summary>How many times this entry was handed out (1 on first delivery).</summary>
        public int HandOuts { get; internal set; }
        public bool IsLive => Node != null;
    }

    private readonly LinkedList<Entry> _entries = new();
    private readonly HashSet<int> _blocked = new();

    public int Count => _entries.Count;

    /// <summary>Appends a delivery (must be called in broker delivery order).</summary>
    public Entry Add(T item, int source)
    {
        var e = new Entry(item, source);
        e.Node = _entries.AddLast(e);
        return e;
    }

    /// <summary>
    /// Hands out up to <paramref name="max"/> entries in order, skipping sources that have an earlier entry
    /// still handed out or delayed. Several consecutive entries of one source may be handed out together.
    /// </summary>
    /// <param name="injectNack">Test hook: when it returns true for a candidate, that entry is nacked
    /// (delayed by <paramref name="injectDelayUs"/>) instead of being handed out, exactly as if a worker had failed it.</param>
    /// <returns>Number of entries injected-nacked.</returns>
    public int Take(int max, long nowUs, List<Entry> output, Func<Entry, bool>? injectNack = null, long injectDelayUs = 0)
    {
        _blocked.Clear();
        int injected = 0;
        for (var node = _entries.First; node != null && output.Count < max; node = node.Next)
        {
            var e = node.Value;
            if (_blocked.Contains(e.Source)) continue;
            bool eligible = e.State == EntryState.Ready || (e.State == EntryState.Delayed && nowUs >= e.NotBeforeUs);
            if (!eligible)
            {
                _blocked.Add(e.Source);
                continue;
            }
            if (injectNack != null && injectNack(e))
            {
                e.State = EntryState.Delayed;
                e.NotBeforeUs = nowUs + injectDelayUs;
                _blocked.Add(e.Source);
                injected++;
                continue;
            }
            e.State = EntryState.HandedOut;
            e.HandOuts++;
            output.Add(e);
        }
        return injected;
    }

    /// <summary>Removes a settled entry. Returns false if it was no longer held (e.g. after <see cref="Clear"/>).</summary>
    public bool Ack(Entry e)
    {
        if (e.Node is null) return false;
        _entries.Remove(e.Node);
        e.Node = null;
        return true;
    }

    /// <summary>Keeps the entry in place but makes it (and therefore its source) unavailable until <paramref name="notBeforeUs"/>.</summary>
    public bool Nack(Entry e, long notBeforeUs)
    {
        if (e.Node is null) return false;
        e.State = EntryState.Delayed;
        e.NotBeforeUs = notBeforeUs;
        return true;
    }

    /// <summary>Earliest future retry time among delayed entries, or <see cref="long.MaxValue"/>.</summary>
    public long NextRetryUs(long nowUs)
    {
        long min = long.MaxValue;
        foreach (var e in _entries)
            if (e.State == EntryState.Delayed && e.NotBeforeUs > nowUs && e.NotBeforeUs < min)
                min = e.NotBeforeUs;
        return min;
    }

    /// <summary>Forgets everything (the channel is gone; the broker requeues its unacked messages itself).</summary>
    public void Clear()
    {
        foreach (var e in _entries) e.Node = null;
        _entries.Clear();
    }
}
