namespace EventBench.RabbitMq;

/// <summary>Bound from the <c>Broker</c> config section when <c>Type</c> is <c>rabbitmq</c>.</summary>
public sealed class RabbitMqOptions
{
    /// <summary>amqp://user:pass@host:port/vhost — when set, overrides Host/Port/User/Password/VirtualHost.</summary>
    public string? Uri { get; set; }
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 5672;
    public string User { get; set; } = "bench";
    public string Password { get; set; } = "bench";
    public string VirtualHost { get; set; } = "/";

    /// <summary>Management HTTP API (ready + unacked backlog, time series). Empty disables it (passive declare fallback).</summary>
    public string ManagementUrl { get; set; } = "http://127.0.0.1:15672";
    /// <summary>Defaults to the AMQP credentials.</summary>
    public string? ManagementUser { get; set; }
    public string? ManagementPassword { get; set; }

    /// <summary>Queues are named {QueuePrefix}.g{group}.p{partition}.</summary>
    public string QueuePrefix { get; set; } = "eb";
    /// <summary>Partition queues per group (0 = one per worker). Each queue is consumed by exactly one worker.</summary>
    public int PartitionsPerGroup { get; set; } = 0;
    /// <summary>basic.qos prefetch per consumer channel (0 = 4 x Worker:BatchSize).</summary>
    public int Prefetch { get; set; } = 0;
    /// <summary>x-single-active-consumer on every queue: a safety net against two consumers on one partition.</summary>
    public bool SingleActiveConsumer { get; set; } = true;
    /// <summary>
    /// x-delivery-limit. RabbitMQ 4 defaults quorum queues to 20 and then DROPS the message (no DLX configured);
    /// a consumer channel closing also counts. -1 = unlimited (never lose a message); 0 = leave the broker default.
    /// </summary>
    public int DeliveryLimit { get; set; } = -1;

    public int ConsumerConnections { get; set; } = 4;
    public int PublisherConnections { get; set; } = 2;
    /// <summary>How long ReceiveAsync waits for a delivery before returning an empty batch.</summary>
    public int ReceiveWaitMs { get; set; } = 200;
    public double PublishConfirmTimeoutSeconds { get; set; } = 60;
    /// <summary>Client-side max inbound body size (the broker's max_message_size is set in infra/rabbitmq/rabbitmq.conf).</summary>
    public long MaxMessageBytes { get; set; } = 64L * 1024 * 1024;

    /// <summary>
    /// TEST ONLY — keep 0 for benchmarks. Probability that the consumer treats a delivery as failed by the worker
    /// (nack with <see cref="TestNackDelayMs"/>) instead of handing it out. Exercises the nack/retry ordering path.
    /// </summary>
    public double TestNackProbability { get; set; } = 0;
    /// <summary>TEST ONLY: retry delay of injected nacks (-1 = Worker:RetryDelayMs).</summary>
    public int TestNackDelayMs { get; set; } = -1;
}
