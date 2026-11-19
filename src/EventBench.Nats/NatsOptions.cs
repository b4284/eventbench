namespace EventBench.Nats;

/// <summary>Bound from the <c>Broker</c> config section when <c>Type = nats</c>.</summary>
public sealed class NatsBrokerOptions
{
    public string Url { get; set; } = "nats://127.0.0.1:4222";
    /// <summary>Stream per group: <c>{StreamPrefix}_G{g}</c>.</summary>
    public string StreamPrefix { get; set; } = "EB";
    /// <summary>Partition subjects: <c>{SubjectPrefix}.g{g}.p{p}</c>.</summary>
    public string SubjectPrefix { get; set; } = "eb";
    /// <summary>Partitions (subjects + durable pull consumers) per group; 0 = one per worker.</summary>
    public int PartitionsPerGroup { get; set; } = 0;
    /// <summary>Consumer AckWait. Ordering does not depend on it (see README), it only bounds redelivery of lost acks.</summary>
    public int AckWaitSeconds { get; set; } = 30;
    /// <summary>Consumer MaxAckPending; 0 = 2 x Worker:BatchSize.</summary>
    public int MaxAckPending { get; set; } = 0;
    /// <summary>Stream replicas (1 = single node; 3 = R3 Raft group in a 3-node cluster).</summary>
    public int Replicas { get; set; } = 1;
    /// <summary>Stream duplicate window used with Nats-Msg-Id = "{source}:{seq}" (makes producer retries idempotent).</summary>
    public int DuplicateWindowSeconds { get; set; } = 120;
    /// <summary>Number of client connections (publishers/consumers are spread round-robin).</summary>
    public int Connections { get; set; } = 4;
    /// <summary>Timeout for JetStream API requests and PubAcks (large messages need headroom).</summary>
    public int RequestTimeoutSeconds { get; set; } = 30;
    /// <summary>Stream storage: File (durable, default) or Memory.</summary>
    public string Storage { get; set; } = "File";
    /// <summary>
    /// When a worker owns a single partition and a no-wait fetch is empty, block up to this long for the
    /// next message (no idle-backoff latency). 0 = plain polling.
    /// </summary>
    public int LongPollMs { get; set; } = 1000;
    /// <summary>
    /// Test knob: probability that a fetched message is silently dropped client-side (a "lost delivery").
    /// Exercises delivery-gap detection + consumer rewind; a correct run still passes.
    /// </summary>
    public double ChaosLoseDeliveryProbability { get; set; }
}

/// <summary>Bound from the <c>Store</c> config section when <c>Type = natskv</c>.</summary>
public sealed class NatsKvStoreOptions
{
    public string Url { get; set; } = "nats://127.0.0.1:4222";
    /// <summary>Buckets: KV <c>{p}_cursor</c>, <c>{p}_record</c>; object stores <c>{p}_upload</c>, <c>{p}_blob</c>.</summary>
    public string BucketPrefix { get; set; } = "eb";
    public int Connections { get; set; } = 4;
    public int RequestTimeoutSeconds { get; set; } = 30;
    /// <summary>Storage for all buckets: File (durable, default) or Memory.</summary>
    public string Storage { get; set; } = "File";
    /// <summary>Bound on CAS retry loops before an operation gives up with an error (the worker then nacks and retries).</summary>
    public int MaxCasAttempts { get; set; } = 16;
    /// <summary>
    /// Test knob: probability that LoadAsync throws a transient error, so the worker nacks the event and the
    /// rest of its lane (exercises the broker's nack/redelivery ordering). A correct run still passes.
    /// </summary>
    public double ChaosFailProbability { get; set; }
}
