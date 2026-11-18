using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using Microsoft.Extensions.Configuration;

namespace EventBench.Kafka;

/// <summary>
/// Kafka broker: one topic per group with <c>PartitionsPerGroup</c> partitions (0 = one per worker).
/// A source maps to a fixed partition (<c>sourceIndexInGroup % partitions</c>, set explicitly on produce),
/// and each worker exclusively owns the partitions <c>p % workers == workerIndex</c> via manual
/// <c>Assign</c> (no consumer-group rebalancing). Per-partition order therefore equals per-source order.
/// <para>
/// Acks are offsets: a consumer commits (to the group <c>{prefix}-g{group}</c>) only the contiguous prefix
/// of acked offsets per partition. A nack rewinds the partition: after the nack delay the consumer seeks back
/// to the lowest nacked offset and redelivers everything from there in order (later messages of other sources
/// come back too and are dedup'd by the store cursor).
/// </para>
/// </summary>
public sealed class KafkaBroker : IBroker, IStatsProvider
{
    private readonly Topology _topology;
    private readonly Action<string> _log;
    private readonly string[] _topics;
    private readonly int[] _partitions;
    private const int MaxAutoProducers = 32;
    private readonly IProducer<int, byte[]>?[] _producers;
    private readonly IAdminClient _admin;
    private int _nextProducer;
    private int _clientLogBudget = 50;

    // Counters aggregated over all consumers (exposed as time-series columns).
    internal long Rewinds;
    internal long DiscardedMessages;
    internal long CommitErrors;

    public KafkaOptions Options { get; }
    public string Name => "kafka";
    public IStatsProvider Stats => this;

    public KafkaBroker(KafkaOptions options, Topology topology, Action<string> log)
    {
        if (string.IsNullOrWhiteSpace(options.BootstrapServers))
            throw new InvalidOperationException("Broker:BootstrapServers is required for kafka");
        Options = options;
        _topology = topology;
        _log = log;
        _topics = topology.Groups.Select(g => $"{options.TopicPrefix}.g{g.Id}").ToArray();
        _partitions = topology.Groups.Select(g => options.PartitionsPerGroup > 0 ? options.PartitionsPerGroup : g.Workers).ToArray();
        foreach (var g in topology.Groups)
            if (_partitions[g.Id] < g.Workers)
                throw new InvalidOperationException(
                    $"kafka: group {g.Name} has {g.Workers} workers but only {_partitions[g.Id]} partitions (workers would idle).");

        _admin = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = options.BootstrapServers, ClientId = "eb-admin" })
            .SetLogHandler((_, m) => ClientLog("admin", m))
            .Build();
        // Created lazily: 0 = one per publisher (capped).
        _producers = new IProducer<int, byte[]>?[options.ProducerInstances > 0 ? options.ProducerInstances : MaxAutoProducers];
    }

    internal string Topic(int groupId) => _topics[groupId];
    internal int PartitionCount(int groupId) => _partitions[groupId];
    internal string GroupId(int groupId) => $"{Options.TopicPrefix}-g{groupId}";

    internal int PartitionOf(EventEnvelope ev)
    {
        var g = _topology.Groups[ev.Group];
        return (ev.SourceId - g.FirstSourceId) % _partitions[g.Id];
    }

    internal void ClientLog(string who, LogMessage m)
    {
        if (m.Level > SyslogLevel.Warning) return;
        if (Interlocked.Decrement(ref _clientLogBudget) < 0) return;
        _log($"kafka {who} [{m.Level}] {m.Facility}: {m.Message}");
    }

    private IProducer<int, byte[]> BuildProducer(int index)
    {
        var cfg = new ProducerConfig
        {
            BootstrapServers = Options.BootstrapServers,
            ClientId = $"eb-producer-{index}",
            Acks = Acks.All,
            EnableIdempotence = true,
            MaxInFlight = 5,
            LingerMs = Options.LingerMs,
            BatchSize = Options.BatchSizeBytes,
            CompressionType = Enum.Parse<CompressionType>(Options.Compression, ignoreCase: true),
            MessageMaxBytes = Options.MaxMessageBytes,
            MessageTimeoutMs = Options.MessageTimeoutMs,
            // The handler only needs the status; avoids marshalling large values back.
            DeliveryReportFields = "status",
        };
        return new ProducerBuilder<int, byte[]>(cfg)
            .SetLogHandler((_, m) => ClientLog("producer", m))
            .SetErrorHandler((_, e) => { if (e.IsFatal) _log($"kafka producer FATAL: {e.Reason}"); })
            .Build();
    }

    internal IConsumer<Ignore, byte[]> BuildConsumer(int groupId, int workerIndex)
    {
        var cfg = new ConsumerConfig
        {
            BootstrapServers = Options.BootstrapServers,
            GroupId = GroupId(groupId),
            ClientId = $"eb-g{groupId}-w{workerIndex}",
            // Background commit of explicitly stored offsets (only contiguous acked prefixes are stored).
            EnableAutoCommit = true,
            AutoCommitIntervalMs = Math.Max(1, Options.CommitIntervalMs),
            EnableAutoOffsetStore = false,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnablePartitionEof = false,
            FetchMaxBytes = Options.MaxMessageBytes,
            MaxPartitionFetchBytes = Math.Min(Options.MaxPartitionFetchBytes, Options.MaxMessageBytes),
            ReceiveMessageMaxBytes = Options.MaxMessageBytes + 1024 * 1024,
            FetchWaitMaxMs = Options.FetchWaitMaxMs,
            QueuedMaxMessagesKbytes = Options.QueuedMaxMessagesKbytes,
            QueuedMinMessages = Options.QueuedMinMessages,
            CheckCrcs = false,
        };
        return new ConsumerBuilder<Ignore, byte[]>(cfg)
            .SetLogHandler((_, m) => ClientLog("consumer", m))
            .SetErrorHandler((_, e) => { if (e.IsFatal) _log($"kafka consumer g{groupId}#{workerIndex} FATAL: {e.Reason}"); })
            .SetOffsetsCommittedHandler((_, c) =>
            {
                if (c.Error.IsError || c.Offsets.Any(o => o.Error.IsError && o.Error.Code != ErrorCode.Local_NoOffset))
                    Interlocked.Increment(ref CommitErrors);
            })
            .Build();
    }

    // ------------------------------------------------------------------------------------------------
    // Setup

    public async Task SetupAsync(CancellationToken ct)
    {
        var deadline = DateTime.UtcNow.AddSeconds(Options.SetupTimeoutSeconds);
        string prefix = Options.TopicPrefix + ".g";

        var existing = _admin.GetMetadata(TimeSpan.FromSeconds(30)).Topics
            .Select(t => t.Topic).Where(t => t.StartsWith(prefix, StringComparison.Ordinal)).ToList();
        if (existing.Count > 0)
        {
            try { await _admin.DeleteTopicsAsync(existing); }
            catch (DeleteTopicsException ex) when (ex.Results.All(r => !r.Error.IsError || r.Error.Code == ErrorCode.UnknownTopicOrPart)) { }
            // Deletion is asynchronous: wait until the metadata no longer lists them.
            var gone = new HashSet<string>(existing);
            while (true)
            {
                var still = _admin.GetMetadata(TimeSpan.FromSeconds(30)).Topics.Count(t => gone.Contains(t.Topic) && t.Error.Code != ErrorCode.UnknownTopicOrPart);
                if (still == 0) break;
                if (DateTime.UtcNow > deadline) throw new TimeoutException($"kafka: {still} topics still present after delete");
                await Task.Delay(200, ct);
            }
        }

        // Committed offsets of the previous run (topic deletion normally drops them too; be explicit).
        var groupIds = _topology.Groups.Select(g => GroupId(g.Id)).ToList();
        try { await _admin.DeleteGroupsAsync(groupIds); }
        catch (DeleteGroupsException) { /* GroupIdNotFound for fresh groups is expected */ }

        var configs = new Dictionary<string, string>
        {
            ["max.message.bytes"] = Options.MaxMessageBytes.ToString(),
            ["message.timestamp.type"] = "LogAppendTime",
            ["min.insync.replicas"] = "1",
            ["cleanup.policy"] = "delete",
            ["unclean.leader.election.enable"] = "false",
        };
        if (Options.FlushMessages > 0) configs["flush.messages"] = Options.FlushMessages.ToString();

        var pending = _topology.Groups.Select(g => new TopicSpecification
        {
            Name = Topic(g.Id), NumPartitions = _partitions[g.Id], ReplicationFactor = Options.ReplicationFactor,
            Configs = new Dictionary<string, string>(configs),
        }).ToList();
        while (pending.Count > 0)
        {
            try
            {
                await _admin.CreateTopicsAsync(pending);
                pending.Clear();
            }
            catch (CreateTopicsException ex)
            {
                var failed = ex.Results.Where(r => r.Error.IsError).ToList();
                var fatal = failed.FirstOrDefault(r => r.Error.Code != ErrorCode.TopicAlreadyExists);
                if (fatal is not null) throw new InvalidOperationException($"kafka: create topic {fatal.Topic}: {fatal.Error.Reason}");
                // "already exists" right after a delete = still being deleted: retry those.
                var retry = failed.Select(r => r.Topic).ToHashSet();
                pending = pending.Where(s => retry.Contains(s.Name)).ToList();
                if (DateTime.UtcNow > deadline) throw new TimeoutException("kafka: topics still marked for deletion");
                await Task.Delay(500, ct);
            }
        }

        // Wait until every partition has a leader.
        var want = _topology.Groups.ToDictionary(g => Topic(g.Id), g => _partitions[g.Id]);
        while (true)
        {
            var md = _admin.GetMetadata(TimeSpan.FromSeconds(30));
            int ready = md.Topics.Count(t => want.TryGetValue(t.Topic, out int n) && !t.Error.IsError
                                             && t.Partitions.Count == n && t.Partitions.All(p => p.Leader >= 0 && !p.Error.IsError));
            if (ready == want.Count) break;
            if (DateTime.UtcNow > deadline) throw new TimeoutException($"kafka: only {ready}/{want.Count} topics ready");
            await Task.Delay(200, ct);
        }

        // Warms up the group coordinator (__consumer_offsets) and validates the admin path used for drain detection.
        long backlog = await GetBacklogAsync(ct);
        _log($"kafka: deleted {existing.Count} old topics, created {want.Count} topics / {want.Values.Sum()} partitions, backlog {backlog}");
    }

    // ------------------------------------------------------------------------------------------------
    // Factories

    public IEventPublisher CreatePublisher(int groupId)
    {
        // One connection per producer instance: Kafka handles a connection's requests one at a time, so
        // several instances let fsync-bound appends (flush.messages) proceed in parallel.
        lock (_producers)
        {
            int i = _nextProducer++ % _producers.Length;
            return new KafkaPublisher(this, _producers[i] ??= BuildProducer(i));
        }
    }

    public IEventConsumer CreateConsumer(int groupId, int workerIndex)
    {
        var g = _topology.Groups[groupId];
        var owned = Enumerable.Range(0, _partitions[groupId]).Where(p => p % g.Workers == workerIndex).ToArray();
        return new KafkaEventConsumer(this, groupId, workerIndex, owned);
    }

    // ------------------------------------------------------------------------------------------------
    // Backlog / stats

    private readonly record struct Lag(long Backlog, long MaxPartitionLag, long EndOffsets, long Committed);

    private async Task<Lag> MeasureAsync(CancellationToken ct)
    {
        var tps = _topology.Groups
            .SelectMany(g => Enumerable.Range(0, _partitions[g.Id]).Select(p => new TopicPartition(Topic(g.Id), p)))
            .ToList();
        ListOffsetsResult ends;
        for (int attempt = 1; ; attempt++)
        {
            try
            {
                ends = await _admin.ListOffsetsAsync(
                    tps.Select(tp => new TopicPartitionOffsetSpec { TopicPartition = tp, OffsetSpec = OffsetSpec.Latest() }),
                    new ListOffsetsOptions { RequestTimeout = TimeSpan.FromSeconds(30) });
                break;
            }
            catch (ListOffsetsException ex)
            {
                // Transient right after (re)creating topics (leader metadata still propagating).
                var bad = ex.Result.ResultInfos.Select(r => r.TopicPartitionOffsetError).FirstOrDefault(e => e.Error.IsError);
                if (attempt >= 20) throw new KafkaException(bad?.Error ?? new Error(ErrorCode.Unknown, ex.Message));
                await Task.Delay(250, ct);
            }
        }
        var end = new Dictionary<TopicPartition, long>();
        foreach (var r in ends.ResultInfos)
            end[r.TopicPartitionOffsetError.TopicPartition] = r.TopicPartitionOffsetError.Offset.Value;

        // librdkafka lists one group per request; run them concurrently.
        var committedTasks = _topology.Groups.Select(g => _admin.ListConsumerGroupOffsetsAsync(
            [new ConsumerGroupTopicPartitions(GroupId(g.Id), null)],
            new ListConsumerGroupOffsetsOptions { RequestTimeout = TimeSpan.FromSeconds(30) })).ToList();
        var committed = new Dictionary<TopicPartition, long>();
        foreach (var res in await Task.WhenAll(committedTasks))
            foreach (var group in res)
                foreach (var p in group.Partitions)
                    if (!p.Error.IsError && p.Offset.Value >= 0) committed[p.TopicPartition] = p.Offset.Value;

        long backlog = 0, maxLag = 0, endSum = 0, committedSum = 0;
        foreach (var (tp, hw) in end)
        {
            long c = committed.GetValueOrDefault(tp, 0);
            long lag = Math.Max(0, hw - c);
            backlog += lag;
            maxLag = Math.Max(maxLag, lag);
            endSum += hw;
            committedSum += c;
        }
        return new Lag(backlog, maxLag, endSum, committedSum);
    }

    public async Task<long> GetBacklogAsync(CancellationToken ct) => (await MeasureAsync(ct)).Backlog;

    public async Task<IReadOnlyDictionary<string, double>> SampleAsync(CancellationToken ct)
    {
        var lag = await MeasureAsync(ct);
        return new Dictionary<string, double>
        {
            ["broker_backlog"] = lag.Backlog,
            ["broker_max_partition_lag"] = lag.MaxPartitionLag,
            ["broker_end_offsets"] = lag.EndOffsets,
            ["broker_committed_offsets"] = lag.Committed,
            ["broker_rewinds"] = Interlocked.Read(ref Rewinds),
            ["broker_discarded_msgs"] = Interlocked.Read(ref DiscardedMessages),
            ["broker_commit_errors"] = Interlocked.Read(ref CommitErrors),
        };
    }

    public string Describe()
    {
        var parts = Options.PartitionsPerGroup > 0 ? Options.PartitionsPerGroup.ToString() : "=workers";
        return $"kafka {_topics.Length} topics '{Options.TopicPrefix}.g*' ({parts} partitions/group, rf={Options.ReplicationFactor}, " +
               $"flush.messages={(Options.FlushMessages > 0 ? Options.FlushMessages.ToString() : "default")}), acks=all idempotent, " +
               $"linger={Options.LingerMs}ms, compression={Options.Compression}, max.message={Options.MaxMessageBytes / 1048576}MB, " +
               $"producers={(Options.ProducerInstances > 0 ? Options.ProducerInstances.ToString() : $"per publisher (max {MaxAutoProducers})")}, " +
               $"manual assign + contiguous commits every {Options.CommitIntervalMs}ms @ {Options.BootstrapServers}";
    }

    public ValueTask DisposeAsync()
    {
        foreach (var p in _producers)
        {
            if (p is null) continue;
            try { p.Flush(TimeSpan.FromSeconds(10)); } catch { /* best effort */ }
            p.Dispose();
        }
        _admin.Dispose();
        return ValueTask.CompletedTask;
    }

    // ------------------------------------------------------------------------------------------------

    private sealed class KafkaPublisher(KafkaBroker broker, IProducer<int, byte[]> producer) : IEventPublisher
    {
        private sealed class PendingBatch(int count)
        {
            private int _remaining = count;
            private Error? _error;
            public readonly TaskCompletionSource Done = new(TaskCreationOptions.RunContinuationsAsynchronously);

            public void OnReport(DeliveryReport<int, byte[]> r)
            {
                if (r.Error.IsError) Interlocked.CompareExchange(ref _error, r.Error, null);
                if (Interlocked.Decrement(ref _remaining) == 0)
                {
                    if (_error is { } e) Done.TrySetException(new KafkaException(e));
                    else Done.TrySetResult();
                }
            }
        }

        public async ValueTask PublishAsync(IReadOnlyList<EventEnvelope> events, CancellationToken ct)
        {
            if (events.Count == 0) return;
            var batch = new PendingBatch(events.Count);
            Action<DeliveryReport<int, byte[]>> handler = batch.OnReport;
            // Produce in list order: librdkafka keeps per-partition order (idempotent producer, so retries
            // cannot reorder or duplicate), and a source always maps to the same partition.
            foreach (var ev in events)
            {
                var tp = new TopicPartition(broker.Topic(ev.Group), new Partition(broker.PartitionOf(ev)));
                var msg = new Message<int, byte[]>
                {
                    Key = ev.SourceId,
                    Value = JsonSerializer.SerializeToUtf8Bytes(ev, EventJsonContext.Default.EventEnvelope),
                };
                while (true)
                {
                    try
                    {
                        producer.Produce(tp, msg, handler);
                        break;
                    }
                    catch (ProduceException<int, byte[]> ex) when (ex.Error.Code == ErrorCode.Local_QueueFull)
                    {
                        await Task.Delay(5, ct);
                    }
                }
            }
            // Must not return before every event is durably accepted (acks=all).
            await batch.Done.Task.WaitAsync(ct);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

public sealed class KafkaBrokerFactory : IBrokerFactory
{
    public string Type => "kafka";

    public IBroker Create(IConfigurationSection section, BenchContext context)
        => new KafkaBroker(section.Get<KafkaOptions>() ?? new(), context.Topology, context.Log);
}
