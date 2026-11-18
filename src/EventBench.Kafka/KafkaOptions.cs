namespace EventBench.Kafka;

/// <summary>Bound from the <c>Broker</c> config section when <c>Type = kafka</c>.</summary>
public sealed class KafkaOptions
{
    public string BootstrapServers { get; set; } = "127.0.0.1:9092";

    /// <summary>Topics are named <c>{TopicPrefix}.g{groupId}</c>; consumer groups <c>{TopicPrefix}-g{groupId}</c>.</summary>
    public string TopicPrefix { get; set; } = "eb";

    /// <summary>Partitions per group topic (0 = one per worker of the group). Must be ≥ the group's workers.</summary>
    public int PartitionsPerGroup { get; set; } = 0;

    public short ReplicationFactor { get; set; } = 1;

    /// <summary>
    /// Topic <c>flush.messages</c>: fsync the log after this many messages (1 = every append, comparable to
    /// Postgres synchronous_commit=on on a single node). 0 = Kafka default (rely on the OS page cache).
    /// </summary>
    public long FlushMessages { get; set; } = 1;

    /// <summary>Largest message (bytes) accepted end to end: topic max.message.bytes, producer message.max.bytes, consumer fetch.max.bytes.</summary>
    public int MaxMessageBytes { get; set; } = 64 * 1024 * 1024;

    // ---- producer ----
    /// <summary>
    /// librdkafka producer instances shared round-robin by the publishers (0 = one per publisher, max 32).
    /// Each instance is one broker connection, and Kafka serves a connection's requests one at a time.
    /// </summary>
    public int ProducerInstances { get; set; } = 0;
    public double LingerMs { get; set; } = 2;
    public int BatchSizeBytes { get; set; } = 1_000_000;
    /// <summary>none | gzip | snappy | lz4 | zstd</summary>
    public string Compression { get; set; } = "none";
    public int MessageTimeoutMs { get; set; } = 300_000;

    // ---- consumer ----
    /// <summary>Soft per-partition fetch limit (the broker still returns a single larger message, KIP-74).</summary>
    public int MaxPartitionFetchBytes { get; set; } = 4 * 1024 * 1024;
    public int FetchWaitMaxMs { get; set; } = 100;
    /// <summary>librdkafka prefetch queue limit per consumer (KB).</summary>
    public int QueuedMaxMessagesKbytes { get; set; } = 16 * 1024;
    public int QueuedMinMessages { get; set; } = 1000;
    /// <summary>auto.commit.interval.ms: how often stored (contiguous-prefix) offsets are committed to Kafka.</summary>
    public int CommitIntervalMs { get; set; } = 100;

    /// <summary>Timeout for topic deletion/creation in SetupAsync.</summary>
    public int SetupTimeoutSeconds { get; set; } = 120;
}
