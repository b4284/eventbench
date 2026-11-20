namespace EventBench.Dispatch;

/// <summary>One dispatcher instance (section "Dispatcher"; the queue and store settings come from Broker/Store).</summary>
public sealed class DispatcherOptions
{
    /// <summary>Unique member id (e.g. d1, or the pod name).</summary>
    public string Id { get; set; } = Environment.MachineName;
    public int Port { get; set; } = 7101;
    /// <summary>host:port that workers use to reach this instance (default 127.0.0.1:Port).</summary>
    public string? Advertise { get; set; }
    /// <summary>Membership and group leases (default: the broker's admin connection, i.e. direct to Postgres).</summary>
    public string? LeaseConnectionString { get; set; }
    /// <summary>A member is dead, and its leases free, this long after its last heartbeat/renewal.</summary>
    public int LeaseTtlMs { get; set; } = 3000;
    /// <summary>Heartbeat, renew, rebalance period.</summary>
    public int TickMs { get; set; } = 500;
    /// <summary>On rebalance, wait up to this long for a group's handed-out messages to finish before handing it over.</summary>
    public int DrainMs { get; set; } = 5000;
}

/// <summary>Worker side ("pgremote" broker; the same section also binds the pgqueue settings).</summary>
public sealed class RemoteDispatchOptions
{
    /// <summary>Seed list "host:port,host:port": any live instance tells the client who owns which group.</summary>
    public string Endpoints { get; set; } = "127.0.0.1:7101";
    /// <summary>TCP connections per dispatcher instance (a group always uses the same one).</summary>
    public int ConnectionsPerDispatcher { get; set; } = 4;
    public int ReceiveWaitMs { get; set; } = 500;
    /// <summary>Periodic re-read of the group → instance routes (rebalances also push NotOwner).</summary>
    public int RouteRefreshMs { get; set; } = 1000;
    /// <summary>At setup, wait until every group has an owner.</summary>
    public int SetupWaitSeconds { get; set; } = 60;
}
