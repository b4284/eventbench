# eventbench

A .NET 10 benchmark framework for **ordered, grouped event processing** with a swappable broker and a
swappable state store. A **correctness oracle** checks every run: it compares the state the workers
produced with the state the deterministic workload says they should have produced.

Implemented today:

| Component | Types |
|---|---|
| Broker | `pgmq` (FIFO-grouped or partitioned), `rabbitmq` (quorum-queue partitions), `inmemory` (baseline plus chaos injection) |
| Store  | `postgres`, `inmemory` |

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
src/EventBench.RabbitMq   RabbitMQ broker (quorum-queue partitions, publisher confirms, client-side ordered retry)
src/EventBench.Cli        `eventbench` CLI (run / validate / compare)
tests/EventBench.Tests    oracle self-tests + generator determinism
configs/                  base configs and overlays
infra/                    podman scripts + PgBouncer config; infra/rabbitmq/ for RabbitMQ
```
