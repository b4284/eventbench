using System.Collections.Concurrent;
using EventBench.Core.Abstractions;

namespace EventBench.Core.Util;

/// <summary>
/// Process-wide hand-off of execution contexts a dispatcher loaded together with the messages it fetched
/// (one batched query instead of one load per event). The store's LoadAsync takes the entry for
/// (source, seq) if present and falls back to its own query otherwise.
/// A context loaded at fetch time stays valid until the event is processed: an event is visible only after
/// its source's previous event committed, and only one event per source is in flight.
/// </summary>
public static class PreloadedContexts
{
    private static readonly ConcurrentDictionary<(int Source, long Seq), WorkContext> Map = new();

    public static void Put(int source, long seq, WorkContext ctx) => Map[(source, seq)] = ctx;
    public static bool TryTake(int source, long seq, out WorkContext ctx) => Map.TryRemove((source, seq), out ctx!);
    public static int Count => Map.Count;
    public static void Clear() => Map.Clear();
}
