# eventbench

A .NET 10 benchmark framework for **ordered, grouped event processing** with a swappable broker and a
swappable state store. A **correctness oracle** checks every run: it compares the state the workers
produced with the state the deterministic workload says they should have produced.

Implemented today:

| Component | Types |
|---|---|
| Broker | `pgmq` (FIFO-grouped or partitioned), `inmemory` (baseline plus chaos injection) |
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
- RabbitMQ: single-active-consumer queues per partition, or consistent-hash exchange.
- Pulsar: `Key_Shared` subscription.

## Layout

```
src/EventBench.Core       abstractions, config, workload generator, worker runtime, metrics, oracle, reports, in-memory impls
src/EventBench.Postgres   pgmq broker, Postgres store (schema + commit function), Postgres/PgBouncer stats
src/EventBench.Cli        `eventbench` CLI (run / validate / compare)
tests/EventBench.Tests    oracle self-tests + generator determinism
configs/                  base configs and overlays
infra/                    podman scripts + PgBouncer config
```
