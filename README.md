# eventbench

A .NET 10 benchmark framework for **ordered, grouped event processing** with a swappable broker and a
swappable state store. A **correctness oracle** checks every run: it compares the state the workers
produced with the state the deterministic workload says they should have produced.

Implemented today:

| Component | Types |
|---|---|
| Broker | `pgmq` (FIFO-grouped or partitioned), `kafka` (partition per worker), `rabbitmq` (quorum-queue partitions), `nats` (JetStream), `sqlqueue` (SQL Server table queue), `redis` (Redis Streams, stream per worker), `inmemory` (baseline plus chaos injection) |
| Store  | `postgres`, `mongo`, `natskv` (JetStream KV + Object Store), `sqlserver`, `redis` (keys + Lua), `inmemory` |

## Quick start (podman)

```bash
./infra/up.sh                      # Postgres 18 + pgmq 1.13 and PgBouncer 1.26 in one pod (:5432 direct, :6432 pooled)
dotnet build -c Release
B="dotnet src/EventBench.Cli/bin/Release/net10.0/eventbench.dll"

$B run -c configs/inmemory-smoke.json                                   # harness sanity check, no infra
$B run -c configs/pgmq-fifo.json                                        # 16k sources, 4k ev/s, 60s
$B run -c configs/pgmq-fifo.json -c configs/overlay-32k.json            # 32k sources, 8k ev/s
$B run -c configs/pgmq-fifo.json -c configs/overlay-txack.json          # ack inside the store transaction
$B run -c configs/pgmq-fifo.json -c configs/overlay-partitioned.json    # partition-per-worker ordering
$B run -c configs/pgmq-fifo.json --Worker:WorkersPerGroup=16 --Broker:FifoReadFunction=read_grouped
$B compare results/                                                     # one line per run
./infra/psql.sh            # psql into Postgres
./infra/psql.sh bouncer    # PgBouncer admin console (SHOW POOLS; SHOW STATS;)
./infra/rabbitmq/up.sh && $B run -c configs/rabbitmq-postgres.json      # RabbitMQ broker + Postgres store
./infra/down.sh
```

Config layering: later `-c` files override earlier ones. Then `EB_*` environment variables apply
(for example `EB_Broker__ConnectionString`), then `--Section:Key=value` on the command line.
`eventbench validate -c ...` prints the resolved config and the offered load.
The exit code is `0` for PASS, `1` for FAIL, and `2` for an error, so you can script sweeps.

> Overlays merge arrays **by index**. An overlay `Workload:Groups` with fewer entries than the base
> leaves the extra base entries in place.

## Model (maps to the requirements)

```
 sources (N per group)           broker                         workers (typed by group)        store
 ┌──────────────┐  publish   ┌─────────────────┐  receive  ┌─────────────────────────────┐   ┌──────────┐
 │ group A src… │──────────▶ │ queue(s) for A  │ ────────▶ │ A#0 … A#k  (stateless)      │◀─▶│ cursor   │
 │ group B src… │──────────▶ │ queue(s) for B  │ ────────▶ │ B#0 … B#k                   │◀─▶│ records  │
 └──────────────┘            └─────────────────┘   ack     │ load ctx → process → commit │   │ uploads  │
                                                           └─────────────────────────────┘   └──────────┘
```

* **Groups / sources / rates.** `Workload:Groups[]` sets `Count` × `Sources` with either
  `RatePerSource` or `RatePerGroup` (spread across the group's sources). Arrival is Poisson or
  constant per source, and the generator is open-loop. If the system falls behind, that lag shows up
  as `producer_lag` and inside `e2e`; the offered load is never silently reduced.
* **Per-source order.** Each source has a gapless `seq`. Every broker must honour this contract: a
  source's next message is never handed out while an earlier one is unacked. The oracle checks the
  contract rather than trusting it.
* **Typed, shared, stateless workers.** Workers of group A only consume A's queues. Any worker can
  take any source of its group. All context lives in the store.
* **Operations.** Each event is `Create`, `Get`, `Update` (get-then-update) or `Complete`. Every
  record that is created is eventually completed. At the end of the run a closing phase emits
  `Complete` for every still-open record, so a correct run ends with **all records Completed**.
* **Large uploads.** `Workload:LargePayload` adds a few 2–16 MB events. With `Mode: Inline` the
  payload travels base64 inside the message. With `Mode: ClaimCheck` the producer stores it in the
  store and the message carries only a reference.

### Many heterogeneous groups (`Workload:GeneratedGroups`)

`configs/pgmq-500groups.json` generates 500 groups and 16,000 sources:

* **Sizes** follow a log-normal (`SizeSigma`), clamped to `[MinSources, MaxSources]` and rescaled to
  sum *exactly* to `TotalSources`. Note that 16k sources over 500 groups averages only 32 per group,
  so the distribution must be skewed: most groups are small and a few have hundreds.
* **Rates.** Each group draws a per-source base rate from a log-normal (`RateMedianPerMinute`,
  `RateSigma`) truncated to `[MinRatePerMinute, MaxRatePerMinute]`. Mid and low rates are common;
  high rates are rare. Each source's rate is the base × U[1−j, 1+j] (`SourceRateJitter`).
* **Payloads.** Each regular group gets a small payload size, log-uniform in
  `[SmallPayloadMinBytes, SmallPayloadMaxBytes]`. `LargeGroups` groups (suffix `L`) run at a low rate
  and send uploads of `LargeMinBytes..LargeMaxBytes` on a `LargeEventFraction` of their Create/Update events.
* **Workers** are sized per group by load: `ceil(max(events/s ÷ EventsPerSecondPerWorker,
  uploadBytes/s ÷ UploadBytesPerSecondPerWorker))`, clamped to `[Min, MaxWorkersPerGroup]`.
* **Producers.** `Workload:Producers` sets up a shared producer pool that spans all groups (one round
  trip publishes to many queues) instead of one producer per group.

`eventbench validate -c configs/pgmq-500groups.json` prints the resulting size and rate
distributions, the worker count, and the expected payload bandwidth.

### Worker step (one event)

1. **Load**: one query reads the source cursor, the record, and the context blob.
2. Check `cursor ≥ seq`. If true it is a **duplicate**: ack and skip.
3. Check `cursor ≠ seq-1`. If true it is an **ordering gap**: record a violation and nack.
4. Verify the pre-state. The event carries the version and hash the generator expected. A mismatch
   is a violation: a lost update, a reorder, or corruption.
5. Verify the payload checksum (inline payload or claim-check blob).
6. Compute the new state from **what was loaded**. `Worker:SimulatedWorkMicros` adds CPU work here.
7. **Commit** in one round trip to a plpgsql function. Under a row lock it re-checks the cursor,
   applies a version-guarded write, stores the upload, and advances the cursor. With
   `AckMode=InTransaction` it also runs `pgmq.delete` in the same transaction (exactly-once).
8. Ack (only in `AckMode=AfterCommit`).

Record hashes are chained and order-sensitive (`hash' = mix(hash, seq, delta, payloadChecksum)`).
Any event that is applied out of order, twice, or with the wrong data therefore changes the final state.

## Oracle (verdict)

| check | meaning |
|---|---|
| `cursors` | every source's cursor equals the number of events generated for it |
| `records_complete` | no record is left Open |
| `record_counts` / `record_content` | per source: number of records, and an order-independent fold of each record's (id, version, chained hash) |
| `uploads` | every large upload persisted with the right size and checksum |
| `applied_equals_generated` | exactly-once application count |
| `broker_drained` | nothing left in the queues |
| `runtime_*` | gaps, pre-state mismatches, conflicts, and corrupt payloads/contexts seen by workers |

The oracle has been tested against itself: `tests/EventBench.Tests` runs the in-memory broker with
chaos injection.
- `ChaosReorderProbability` → FAIL (gaps)
- `ChaosDropProbability` → FAIL (cursors, open records, backlog)
- `ChaosDuplicateProbability` → PASS (idempotent dedup)

`Worker:ChaosFailProbability` works with every broker. The worker fails an event just before
commit and nacks it, which tests the broker's redelivery path: a retried event must not be overtaken
by later events of the same source. Use it with a short `Worker:RetryDelayMs` (e.g. 0.01 and 50).

## PGMQ specifics

`Broker:Ordering`:

* **`Fifo`** (default) uses one queue per group. The source id goes into the `x-pgmq-group` header,
  and workers read with `pgmq.read_grouped_head` (or `read_grouped` / `read_grouped_rr`,
  selected by `FifoReadFunction`). Any worker of the group can pick up any source, and a source's
  later messages stay blocked while its head is in flight. `FifoIndex: Btree` adds an index on the
  grouping expression. `Gin` uses pgmq's own `create_fifo_index`.
* **`Partitioned`** uses `PartitionsPerGroup` queues per group (0 means one per worker). Each queue
  is owned by exactly one worker and read with plain `pgmq.read`, which is the Kafka-style model.
  Nacks are immediate (`vt=0`); otherwise a later message could overtake.

Other knobs: `VisibilityTimeoutSeconds`, `Unlogged`, `ArchiveOnAck`, `QueuePrefix`. On the store
side: `UnloggedTables`, `FillFactor`. Pool sizes and prepared statements are set in the connection
strings (`Maximum Pool Size`, `Max Auto Prepare`). Server-side settings live in `infra/up.sh`
(env vars `PG_SYNC_COMMIT`, `PG_SHARED_BUFFERS`, `PG_CPUS`, …) and
`infra/pgbouncer/pgbouncer.ini` (`default_pool_size`, …). The data path goes through PgBouncer in
transaction mode. DDL, oracle scans, and stats use the direct port.

## MongoDB store

`Store:Type=mongo` (`src/EventBench.Mongo`, official `MongoDB.Driver` 3.x). Start and stop MongoDB 8.0 as a
single-node replica set (transactions need one) with `./infra/mongo/up.sh` and `./infra/mongo/down.sh`
(container `eb-mongo`, `127.0.0.1:27017`; env `MONGO_IMAGE`, `MONGO_CACHE_GB`, `MONGO_CPUS`, `MONGO_MEMORY`).
`configs/pgmq-mongo.json` keeps the PGMQ broker and uses `AckMode: AfterCommit`, because an ack cannot be
transactional across PGMQ and MongoDB.

```bash
./infra/mongo/up.sh
$B run -c configs/pgmq-mongo.json
$B run -c configs/pgmq-mongo.json --Store:UseTransactions=false     # embedded mode
EB_MONGO_URL="mongodb://127.0.0.1:27017/?directConnection=true" dotnet test tests/EventBench.Tests   # + live commit tests
```

* **Durability.** Every write uses `w: majority, j: true`, whatever the connection string says. Loads and
  scans read with `readConcern: majority`, so a worker never skips an event because of state that could
  still roll back. Transactions read with `readConcern: snapshot`.
* **Collections** (`CollectionPrefix`, default `eb`): `_cursors` (`_id` = source, `group`, `lastSeq`),
  `_records` (`_id` = record id, `src`, `status`, `version`, `hash`, `ctx` binary, created/updated/completed seq),
  `_uploads` (metadata: `src`, `seq`, `size`, `checksum`), `_blobs` (claim-check metadata), and `_chunks`
  (1 MiB slices of upload and blob bytes, `ChunkSizeBytes`).
* **Commit (`UseTransactions=true`, the default).** One multi-document transaction:
  1. a conditional `updateOne {_id: src, lastSeq: seq-1}`. If it matches nothing, the store re-reads the
     cursor in the same snapshot and returns Duplicate (`lastSeq >= seq`) or Gap;
  2. a record insert (duplicate key gives RecordConflict) or a version-guarded `updateOne` (0 matched
     gives RecordConflict). Either conflict aborts the transaction, so the cursor does not move;
  3. the upload metadata insert, then commit.

  Two workers that commit the same seq collide on the cursor document with a write conflict
  (`TransientTransactionError`). The loser retries with jittered backoff on a fresh snapshot, sees
  `lastSeq = seq`, and returns Duplicate. An `UnknownTransactionCommitResult` is retried by committing again.
* **Large uploads** (up to about 16 MB, the BSON document limit) are written **before** the transaction
  as idempotent chunk upserts keyed by (source, seq). A redelivered or concurrent duplicate rewrites
  identical bytes. Only the metadata that makes an upload visible to the oracle is written inside the commit.
  Claim-check blobs write their chunks first and their metadata document last.
* **Load** runs the cursor and record queries in parallel. Every commit at seq k sets
  `record.updatedSeq = k` together with the cursor, so a record newer than the cursor shows a torn read,
  and the store then re-reads record first, cursor second.
* **Embedded mode (`UseTransactions=false`).** Records and upload metadata live inside the source
  document, so commit is one conditional single-document `updateOne` and load is one `find`. Atomicity
  comes without transactions. The cost is that a source document grows with its records (about
  1.1 KB each with a 1 KB context), which caps it at about 14k records per source (16 MB).
* **Stats.** `serverStatus` opcounters, transactions, connections, and WiredTiger cache, plus the store's
  own retry counters, are written to the timeseries as `mongo_*` columns.

## Kafka specifics

```bash
./infra/kafka/up.sh       # single-node KRaft Kafka (apache/kafka image) on 127.0.0.1:9092, 64 MB max message
$B run -c configs/kafka-postgres.json                    # same workload as pgmq-fifo, Postgres store
$B run -c configs/kafka-smoke.json                       # small, in-memory store
./infra/kafka/down.sh     # removes the container and its data (does not touch the eventbench pod)
```

* **Ordering.** One topic per group (`{TopicPrefix}.g{id}`) with `PartitionsPerGroup` partitions
  (0 = one per worker). A source always goes to partition `indexInGroup % partitions`. The partition
  is set explicitly on produce and the key is the source id. Worker `w` owns partitions with
  `p % workers == w` through manual `Assign`. There is no consumer-group rebalancing, so each partition
  has exactly one reader and per-partition order is per-source order. A consumer never hands out more
  of a partition while an earlier batch from that partition is still unacked or unnacked.
* **Acks** are offsets. Each partition tracks the handed-out offsets. When an ack extends the
  **contiguous prefix** of acked offsets, the consumer stores that offset (`StoreOffset`). librdkafka
  auto-commits stored offsets every `CommitIntervalMs` to consumer group `{TopicPrefix}-g{id}`, and
  dispose adds a final synchronous commit. A restarted consumer resumes from the committed offset
  (`Offset.Stored`). Out-of-order acks from `MaxParallelPerWorker > 1` never commit past an
  unfinished message. The `ack` metric is therefore ~0, and the broker-side ack lags by up to
  `CommitIntervalMs`. If a crash happens in that window, the messages are redelivered and dedup'd as
  duplicates. `AckMode=InTransaction` is not supported.
* **Nacks.** Kafka has no per-message nack. A nack at offset `o` pauses the partition, discards
  everything fetched after `o`, and waits for the nack delay and for in-flight deliveries of that
  partition to resolve. It then `Seek`s back to `o`. Everything from `o` on is redelivered in order.
  Later messages of *other* sources come back too and count as duplicates. A nack only stalls its own
  partition.
* **Durability.** The producer uses `acks=all` and is idempotent (`max.in.flight=5`), and replication
  factor is 1 on a single node. Topics set `flush.messages=1` by default (`FlushMessages`), so the
  log is fsynced on each append, which is comparable to Postgres `synchronous_commit=on`. Set it to
  `0` to rely on the page cache like a default Kafka. Kafka fsyncs each partition append
  separately: there is no group commit across partitions, and it serves one request per connection
  at a time. That is why `ProducerInstances=0` (one producer connection per publisher) is the
  default. On the dev box, with `kafka-postgres.json` at 4k ev/s, e2e p50 was ~115 ms with fsync
  and ~20 ms with `FlushMessages=0`.
* **Large inline payloads** travel through the broker as is. Topic `max.message.bytes`, producer
  `message.max.bytes`, and consumer `fetch.max.bytes` default to 64 MB (`MaxMessageBytes`). The
  broker's `message.max.bytes`, `replica.fetch.max.bytes`, `fetch.max.bytes`, and
  `socket.request.max.bytes` are raised in `infra/kafka/up.sh`.
* **Backlog** = Σ over partitions of (high watermark − committed offset), read with the admin
  client. Time-series columns: `broker_backlog`, `broker_max_partition_lag`, `broker_rewinds`,
  `broker_discarded_msgs`, `broker_commit_errors`.
* Other knobs: `LingerMs`, `BatchSizeBytes`, `Compression`, `ProducerInstances`,
  `MaxPartitionFetchBytes`, `FetchWaitMaxMs`, `QueuedMaxMessagesKbytes`. There is one librdkafka
  consumer per worker. `SetupAsync` deletes and recreates the `{TopicPrefix}.g*` topics and the
  consumer groups.

## RabbitMQ specifics

`./infra/rabbitmq/up.sh` starts `rabbitmq:4.4.1-management` as the standalone podman container `eb-rabbitmq`
(AMQP `127.0.0.1:5672`, management `http://127.0.0.1:15672`, user `bench`/`bench`); `./infra/rabbitmq/down.sh`
removes it. It is independent of the Postgres pod; `configs/rabbitmq-postgres.json` uses RabbitMQ as the broker and
Postgres as the store (`AckMode: AfterCommit`: a transactional ack across RabbitMQ and Postgres is impossible).
`infra/rabbitmq/rabbitmq.conf` raises `max_message_size` to 64 MiB (the 4.x default of 16 MiB is too small for
~22 MB base64 inline uploads) and refreshes stats every second (`quorum_tick_interval=1000` via erl args).

* **Topology.** Per group, `PartitionsPerGroup` durable **quorum queues** `{QueuePrefix}.g{group}.p{partition}`
  (0 = one per worker). A source maps to `partition = indexInGroup % partitions`, and partition `p` is consumed only
  by worker `p % workers`, so each queue has exactly one consumer. `x-single-active-consumer` is a safety net.
  `SetupAsync` deletes and redeclares the queues. `x-delivery-limit` is `-1` (unlimited): RabbitMQ 4 defaults to
  20 and then drops the message, and a consumer channel closing counts as a delivery attempt.
* **Publish.** Persistent messages and the mandatory flag, on a confirm-mode channel. A batch is pipelined in list
  order on one channel (so per-queue order equals publish order), and `PublishAsync` returns only after every
  message is confirmed. A nack, a return or a timeout fails the batch; the producer retries it whole, and the
  copies that were already confirmed become duplicates that the store cursor skips. Header `eb-ts` carries the
  publish time (µs) used for `broker_dwell`.
* **Receive.** One channel per worker (`basic.qos` prefetch = `Prefetch`, default 4 × `BatchSize`). Pushed
  deliveries go into a `Channel<T>` and then into a local buffer kept in delivery order. `ReceiveAsync` hands out up
  to `max` of them, or returns empty after `ReceiveWaitMs`.
* **Ack.** `basic.ack` of the single delivery tag. Only after the ack has been sent can the source's next message
  be handed out.
* **Nack — why not `basic.nack(requeue)`.** We probed quorum queues on 4.4.1 with prefetch 10. After `nack(3,
  requeue)`, message 3 is redelivered, but 4..10 had already been delivered ahead of it. When a channel closes, its
  unacked messages return to the head of the queue in the order they were *last delivered* (1,2,4..10,3). So a
  broker requeue breaks per-source order, and so does a channel close after any requeue. Instead, **a nack never
  goes to the broker.** The message stays unacked and holds its prefetch slot, while the local buffer marks it
  delayed *in its original position*. The hand-out walk skips every source that has an earlier entry still handed
  out or delayed, so the failed message is retried after the delay before any later message of its source. Other
  sources in the same partition keep flowing. Because the client never requeues, the broker's view of delivery
  order always equals queue order. That makes the failure path safe too: if a channel dies, the broker requeues
  everything in order, the consumer drops its buffer, and it re-subscribes. Acks of deliveries from the dead
  channel fail; the state is already committed, so the redelivered copy is skipped as a duplicate.
  `OrderedRedeliveryBuffer` / `ConfirmTracker` are unit-tested in `tests/EventBench.Tests/RabbitMqBookkeepingTests.cs`.
* **Backlog.** Ready plus unacked comes from the management API (`ManagementUrl`), combined by `max` with an exact
  ready count from passive declares. The time series adds `broker_ready`, `broker_unacked`,
  `broker_max_queue_len` and `broker_consumers`.
* **Test-only knob.** `Broker:TestNackProbability` (default `0`, never use it for benchmarks) makes the consumer treat
  that fraction of deliveries as failed by the worker, nacking them with `TestNackDelayMs` (default
  `Worker:RetryDelayMs`). It exercises the retry path: `--Broker:TestNackProbability=0.02` must still PASS with
  `runtime_ordering_gap` = 0. Injected nacks are logged and reported as `broker_test_nacks`, not in the worker's `nacks`.

Limitations: a broker-side nack of a message *inside* a batch, while a later message of the same batch is
confirmed, would leave a gap in the queue. The producer's retry re-publishes the whole batch, but the queue then
holds `n+1` before the retried `n`, and the worker keeps nacking `n+1` as a gap. No such nack was observed in single-node
runs. Closing that hole would need idempotent, gap-free publishing, which AMQP 0-9-1 does not offer.

## NATS JetStream / KV

```bash
./infra/nats/up.sh        # nats:2.12-alpine as eb-nats, JetStream file store in volume eb-nats-data, :4222 / :8222
$B run -c configs/nats-postgres.json     # JetStream broker + Postgres store
$B run -c configs/nats-natskv.json       # JetStream broker + JetStream KV store
$B run -c configs/pgmq-natskv.json       # pgmq broker + JetStream KV store
./infra/nats/down.sh      # removes the container and its volume
```

`infra/nats/nats-server.conf` raises `max_payload` to 64 MB so inline 16 MB uploads (~22 MB of base64 JSON)
travel through the broker. `NATS_SYNC_INTERVAL` (default `2m`, the nats-server default) controls fsync.
With the default, a PubAck means the message was written to the file store but not yet fsynced. With
`NATS_SYNC_INTERVAL=always` every write is fsynced (comparable to `synchronous_commit=on`). On this host
that caps the KV store at roughly 450 events/s.

**Broker (`Broker:Type=nats`).**

* One **work-queue stream per group** (`EB_G{g}`, file storage), with one subject per partition
  (`eb.g{g}.p{p}`). A source maps to a partition by its index in the group. `PartitionsPerGroup: 0`
  means one partition per worker.
* Each partition has one durable **pull** consumer filtered on its subject. Exactly one worker owns it
  (`p % workers == worker`), so per-source order is stream order.
* **Publish** goes subject by subject, one message at a time, and awaits each PubAck. Subjects publish
  concurrently. A failure stops that subject's chain, so the stream never holds seq n+1 without seq n.
  `Nats-Msg-Id = source:seq` turns the producer's batch retry into server-side duplicates.
* **Ack** is a JetStream ack, and the work-queue retention deletes the message. So the backlog
  (`GetBacklogAsync`, `broker_backlog`) is the streams' message count.
* **Nack is never sent to the server.** A JetStream NAK would redeliver the message *after* later
  messages of the same subject. Instead the partition is **paused** for the retry delay, then its
  consumer is **deleted and recreated** as the next generation with `DeliverPolicy.All`. Acked messages
  are already gone, so delivery restarts at the oldest unacked message, in stream order. This covers
  the failed message, the rest of its lane, and anything fetched after it. Any already-applied
  redeliveries are skipped by the store cursor. The rewind only happens inside `ReceiveAsync`, when the
  worker has resolved every delivery of its previous batch.
* **Lost deliveries.** The consumer delivery sequence grows by exactly one per delivery, so a jump means
  a message was delivered but never reached the worker, for example a pull that timed out client-side.
  JetStream would redeliver that message after `AckWait`, which is after later ones. The partition
  therefore stops handing out messages at the jump and rewinds immediately (`broker_delivery_gaps`,
  `broker_consumer_resets`). As a result, ordering does not depend on `AckWait` or `MaxAckPending`
  (default 2 × `BatchSize`).
* **Receive:** a no-wait fetch of up to `BatchSize` messages. If it is empty and the worker owns a
  single partition, the worker long-polls for one message (`LongPollMs`) and then takes whatever else is
  ready. `EnqueuedAtUs` is the JetStream message timestamp.

**KV store (`Store:Type=natskv`).** Buckets `{p}_cursor` and `{p}_record` (KV, file, history 1). The object
stores `{p}_upload` and `{p}_blob` hold MB-sized uploads and claim-check blobs. JetStream KV has no
multi-key transactions, so a commit is built from single-key compare-and-set operations:

1. Read cursor key `c{source}` and its revision (an absent key means 0). If cursor ≥ seq, return
   **Duplicate**. If cursor ≠ seq-1, return **Gap**.
2. Write record key `r{record}` with a revision CAS (create-only for an insert). The value carries
   `AppliedSeq` plus the **pre-image**. If `AppliedSeq == seq` the mutation is already there, from an
   earlier attempt or a concurrent duplicate, so it is skipped (idempotent). A version mismatch returns
   **RecordConflict** only if a re-read cursor is still seq-1. Otherwise a duplicate already committed it,
   and the result is **Duplicate**.
3. Write the upload to the object store as `{source}-{seq}`, with size and checksum in its metadata.
   Writing it again is idempotent.
4. **Commit point:** cursor CAS seq-1 → seq on the revision from step 1. **Applied** if it wins. A lost
   race re-reads the cursor and yields Duplicate.

Readers (`LoadAsync` and the oracle scans) read the cursor *before* the record and resolve visibility
against it:
* `AppliedSeq ≤ cursor`: the record is current.
* `AppliedSeq = cursor+1`: the write is not committed yet, so readers see the pre-image (or "absent" for an insert).
* Further ahead: the cursor read is stale, so read again.

Writes and uploads from an attempt that never advanced the cursor are therefore never observed.
`SupportsTransactionalAck` is false (`AckMode: AfterCommit`). Test knobs:
`Broker:ChaosLoseDeliveryProbability` and `Store:ChaosFailProbability` (Load throws, so the worker nacks).
Both still PASS. `EB_NATS_URL=nats://127.0.0.1:4222 dotnet test` also runs a concurrent-duplicate
commit test against the server.

## SQL Server (store and table queue)

```bash
./infra/mssql/up.sh                 # SQL Server 2025 Developer, :1433, db "bench" (RCSI on, SIMPLE recovery)
./infra/mssql/durability.sh forced  # best effort: DELAYED_DURABILITY=FORCED (default) | disabled = fully durable
B run -c configs/pgmq-500groups.json -c configs/matrix/broker-sqlqueue.json -c configs/matrix/store-sqlserver.json
```

- **Store (`sqlserver`).** Load is one SELECT, which doesn't block under RCSI. Commit is one stored
  procedure `eb_commit`. It locks the source cursor with UPDLOCK, writes the record with a version
  guard, inserts the upload and advances the cursor.
- **Queue (`sqlqueue`), the PGMQ analogue.** One table `eb_q`, clustered on `(group_id, msg_id)`,
  with an index on `(group_id, source_id, msg_id)`. Publishing inserts the whole producer batch, across
  all groups, in one statement through a table-valued parameter. Dequeue takes up to N source *heads*:
  - Each picked message has no older message of its source left, and its visibility time has passed.
  - `UPDLOCK, READPAST` makes concurrent workers of a group skip each other's rows.
  - The "no older message" check must use a locking read (`READCOMMITTEDLOCK`). The RCSI statement
    snapshot misses predecessors inserted after the snapshot started. The oracle caught exactly that
    as ordering gaps.
- **Transactional ack.** With `AckMode: InTransaction` and the same database, the commit batch also
  deletes the queue row, so state and ack commit together (exactly once).
- **Memory.** `MSSQL_MEMORY_MB` defaults to 4096 in `up.sh`. Give it 8192 for the 500-group workload,
  otherwise SQL Server re-reads pages from disk and falls behind.
- **SqlClient and globalization.** Microsoft.Data.SqlClient does not run with invariant
  globalization, so the CLI project turns it off.

## Redis (Streams broker and store)

```bash
./infra/redis/up.sh                 # Redis 8, :6379, AOF appendfsync everysec (best effort), no RDB snapshots
./infra/redis/fsync.sh always       # fsync before every reply (fsync parity); back with: everysec
B run -c configs/pgmq-500groups.json -c configs/matrix/broker-redis.json -c configs/matrix/store-redis.json
```

- **Broker (`redis`).** One stream per (group, partition), with partitions equal to workers. Each
  stream is read by exactly one worker through a consumer group (`XREADGROUP`), so order comes from
  single ownership, like Kafka.
  - Publishing is one `MULTI/EXEC` per producer batch.
  - Ack is `XACK` + `XDEL`, which keeps `XLEN` equal to the backlog.
  - A nack pauses the stream. After the retry delay, the consumer re-reads its own pending entries
    (`ID 0`, oldest first) before taking new ones (`>`), so a failed event is never overtaken.
  - Stream IDs carry the enqueue time.
- **Store (`redis`).** Cursor keys (absent = 0), record hashes and upload hashes. Load and commit are
  Lua scripts, each one atomic round trip.
- **Transactional ack.** With the Streams broker on the same server and `AckMode: InTransaction`, the
  commit script also `XACK`s and `XDEL`s the entry (exactly once).
- **Fsync parity.** `WaitAof: true` on the broker or store adds `WAITAOF 1 0`.
- **Connections.** Commands share a few multiplexed connections (`Connections`, default 8), which
  bounds head-of-line blocking behind MB-sized values.

## Metrics & output

Each run writes `results/<utc-stamp>_<name>/`:

* `summary.md` / `summary.json`: verdict, oracle checks, throughput, latency percentiles, per-group
  stats, and steady-state resource mean/max.
* `timeseries.csv`: one row per second. It covers rates, in-flight count, interval p50/p99/max for
  every latency metric, bench CPU/GC/thread-pool, host CPU, container CPU/mem (`podman stats`),
  Postgres TPS / tuples / WAL MB/s / connections / lock waits, pgmq backlog, and PgBouncer
  `cl_waiting` / `sv_active` / `maxwait`.
* `histograms/*.hgrm`: full HdrHistogram distributions (ms).
* `config.json`: the effective config, with passwords masked.

Latency metrics (µs precision, recorded after warmup, excluding closing events):

| metric | span |
|---|---|
| `e2e` | scheduled → acked (includes producer lag, so it is coordinated-omission safe) |
| `e2e_from_send` | handed to broker → acked |
| `producer_lag` | scheduled → handed to broker |
| `queue_wait` | handed to broker → received |
| `broker_dwell` | pgmq `enqueued_at` → received |
| `ctx_load`, `process`, `ctx_commit`, `ack` | the worker steps |
| `service` | received → acked |
| `e2e_large` | e2e of large-payload events only |

## Adding a broker or store

Implement `IBroker` / `IEventPublisher` / `IEventConsumer` / `IDelivery` (or `IStateStore`) from
`src/EventBench.Core/Abstractions/Contracts.cs`, plus a factory. Put it in its own project so its
client library stays isolated, and register it in `CreateRegistry()` in
`src/EventBench.Cli/Program.cs`. The `Broker`/`Store` config section is bound to your options class
and `Type` selects the implementation. Ordering approaches for other brokers:

- Kafka / Redpanda: partitions keyed by source.
- NATS JetStream: a subject per source partition with `MaxAckPending=1` consumers, or ordered consumers.
- Redis Streams: a stream per partition with one consumer per stream.
- RabbitMQ: implemented as `rabbitmq` (see below).
- Pulsar: `Key_Shared` subscription.

## Layout

```
src/EventBench.Core       abstractions, config, workload generator, worker runtime, metrics, oracle, reports, in-memory impls
src/EventBench.Postgres   pgmq broker, Postgres store (schema + commit function), Postgres/PgBouncer stats
src/EventBench.Mongo      MongoDB store (transactional or embedded commit, chunked uploads), serverStatus stats
src/EventBench.Kafka      Kafka broker (Confluent.Kafka), contiguous-offset tracker
src/EventBench.RabbitMq   RabbitMQ broker (quorum-queue partitions, publisher confirms, client-side ordered retry)
src/EventBench.Nats       NATS JetStream broker, JetStream KV/Object Store state store
src/EventBench.SqlServer  SQL Server table-queue broker, SQL Server store (stored procedure commit), DMV stats
src/EventBench.Redis      Redis Streams broker, Redis store (Lua commit), INFO stats
src/EventBench.Cli        `eventbench` CLI (run / validate / compare)
tests/EventBench.Tests    oracle self-tests, generator determinism, broker/store bookkeeping tests
configs/                  base configs and overlays
infra/                    podman scripts + PgBouncer config; infra/{kafka,rabbitmq,nats,mongo,mssql,redis} per component
```
