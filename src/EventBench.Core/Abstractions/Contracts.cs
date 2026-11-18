using EventBench.Core.Config;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.Abstractions;

/// <summary>Shared context handed to component factories.</summary>
public sealed class BenchContext
{
    public required BenchConfig Config { get; init; }
    public required Topology Topology { get; init; }
    public required Action<string> Log { get; init; }
}

/// <summary>
/// A message broker. Ordering contract every implementation must honour:
/// for a given source, a message is never handed to any consumer while an earlier message of the
/// same source is still unacknowledged, and messages of one source are delivered in publish order
/// (a single consumer may receive several consecutive messages of one source in one batch).
/// The oracle verifies this contract; it is not assumed.
/// </summary>
public interface IBroker : IAsyncDisposable
{
    string Name { get; }

    /// <summary>Create (and purge) the queues/streams/topics required by the topology.</summary>
    Task SetupAsync(CancellationToken ct);

    /// <summary>
    /// Creates a publisher. <paramref name="groupId"/> is a hint (the first group it will serve): a publisher
    /// must accept events of any group, because shared producers (Workload:Producers) span many groups.
    /// </summary>
    IEventPublisher CreatePublisher(int groupId);

    /// <summary>Create the consumer for worker <paramref name="workerIndex"/> of the group (workers are typed by group).</summary>
    IEventConsumer CreateConsumer(int groupId, int workerIndex);

    /// <summary>Number of messages still held by the broker (used for drain detection and the oracle).</summary>
    Task<long> GetBacklogAsync(CancellationToken ct);

    /// <summary>Optional periodic metrics (queue depth, etc.).</summary>
    IStatsProvider? Stats => null;

    /// <summary>One-line description of the effective broker settings for the report.</summary>
    string Describe();
}

public interface IEventPublisher : IAsyncDisposable
{
    /// <summary>Publishes in list order. Must not return before the events are durably accepted.</summary>
    ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct);
}

public interface IEventConsumer : IAsyncDisposable
{
    /// <summary>Returns up to <paramref name="maxCount"/> messages; may return an empty list when idle.</summary>
    ValueTask<IReadOnlyList<IDelivery>> ReceiveAsync(int maxCount, CancellationToken ct);
}

public interface IDelivery
{
    EventEnvelope Event { get; }
    long ReceivedAtUs { get; }
    /// <summary>Broker-side enqueue time if known (0 otherwise).</summary>
    long EnqueuedAtUs { get; }
    int DeliveryCount { get; }
    ValueTask AckAsync(CancellationToken ct);
    /// <summary>Make the message visible again after <paramref name="delay"/>.</summary>
    ValueTask NackAsync(TimeSpan delay, CancellationToken ct);
}

/// <summary>Contextual state a stateless worker loads before handling an event.</summary>
public sealed class WorkContext
{
    /// <summary>Highest sequence already applied for the source.</summary>
    public long LastSeq { get; init; }
    public bool RecordExists { get; init; }
    public int Version { get; init; }
    public long Hash { get; init; }
    public RecordStatus Status { get; init; }
    public byte[]? Context { get; init; }
}

public enum MutationKind : byte { None = 0, Insert = 1, Update = 2 }

/// <summary>What the worker wants to persist for one event.</summary>
public sealed class CommitRequest
{
    public required EventEnvelope Event { get; init; }
    public MutationKind Kind { get; init; }
    public int PrevVersion { get; init; }
    public int NewVersion { get; init; }
    public long NewHash { get; init; }
    public RecordStatus NewStatus { get; init; }
    public byte[]? NewContext { get; init; }

    /// <summary>Set when the event carried a large upload that must be persisted.</summary>
    public UploadInfo? Upload { get; init; }

    /// <summary>When AckMode=InTransaction: the delivery to acknowledge atomically with the state change.</summary>
    public IDelivery? TransactionalAck { get; init; }
}

public sealed record UploadInfo(int Size, long Checksum, byte[]? Data);

public enum CommitStatus
{
    Applied,
    /// <summary>The source cursor is already at or past this sequence (redelivery) — safe to ack and skip.</summary>
    Duplicate,
    /// <summary>The source cursor is behind seq-1: an earlier event has not been applied (ordering violation).</summary>
    Gap,
    /// <summary>The record row did not match the expected previous version (lost update / corruption).</summary>
    RecordConflict,
}

public readonly record struct SourceCursor(int SourceId, long LastSeq);
public readonly record struct RecordRow(int SourceId, long RecordId, RecordStatus Status, int Version, long Hash);
public readonly record struct UploadRow(int SourceId, long Seq, int Size, long Checksum);

/// <summary>The data store used to hand execution context between stateless workers.</summary>
public interface IStateStore : IAsyncDisposable
{
    string Name { get; }

    /// <summary>Create schema and seed one cursor row per source. Always starts from an empty state.</summary>
    Task SetupAsync(CancellationToken ct);

    ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct);

    /// <summary>
    /// Atomically: verify source cursor == seq-1, apply the record mutation (guarded by PrevVersion),
    /// persist upload, advance cursor, and (optionally) ack the delivery.
    /// </summary>
    ValueTask<CommitStatus> CommitAsync(CommitRequest request, CancellationToken ct);

    /// <summary>True if this store can ack the given delivery inside its own transaction.</summary>
    bool SupportsTransactionalAck(IBroker broker) => false;

    // Claim-check support for large payloads.
    ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct);
    ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct);

    // Oracle scans.
    IAsyncEnumerable<SourceCursor> ScanCursorsAsync(CancellationToken ct);
    IAsyncEnumerable<RecordRow> ScanRecordsAsync(CancellationToken ct);
    IAsyncEnumerable<UploadRow> ScanUploadsAsync(CancellationToken ct);

    IStatsProvider? Stats => null;
    string Describe();
}

/// <summary>Periodic metrics source. Returned values are written as time-series columns (prefix them).</summary>
public interface IStatsProvider
{
    Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct);
}

public interface IBrokerFactory
{
    string Type { get; }
    IBroker Create(IConfigurationSection section, BenchContext context);
}

public interface IStateStoreFactory
{
    string Type { get; }
    IStateStore Create(IConfigurationSection section, BenchContext context);
}
