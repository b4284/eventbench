# Backlog: paths not yet explored

A record of every path we noticed but have not tested yet.

**Adding an item:** add it as soon as a path is left open, with:
- where it came from;
- why it matters;
- how to test it.

**Closing an item:** move it to *Done* or *Dropped* with a one-line result and a link to the results directory.
Never delete an item.

Ordering: correctness under failure first, then the 24k target on one platform, then blocking. Status is
`requested` (the user asked for it), `next`, `open` or `idea`.

## Open

### B0 — Bake-off: existing products before building our own dispatcher · **done**
- **From:** user, 2026-11-20: "are we re-creating something that's already out there?"
- **Why:** per-key ordered queues ("message groups", "sessions", "partition keys") are a known pattern. Building
  our own is a long-term operating commitment, so first test what exists, with the same oracle and drills.
- **Candidates:**
  - MassTransit SQL transport, `PartitionedOrdered` mode: .NET; Postgres and SQL Server. Check the license.
  - Temporal: .NET SDK; an entity workflow per source; persistence on SQL.
  - Hatchet: an engine plus gRPC workers on Postgres; concurrency keys; no .NET SDK.
  - DBOS partitioned queues: no .NET SDK.
  - For reference: Kafka with key-level parallel consumption, which is already our pipelined worker
    (matrix 10).
- **Test:** the 24k workload with one-event publishes; order, exactly-once, blocking and drain checks; and a
  node-kill drill.

### B1 — Dispatcher as a separate 2–3 instance cluster · **requested**
- **Now a standalone product: Octopus, https://github.com/b4284/octopus** (public, GPL-3.0). The design lives
  there; this repository will only hold the adapter and the runs.
- **From:** the dispatcher plan, step 2 ("sounds good"); the user asked for it 2026-11-20.
- **Why:** today the dispatcher runs inside the worker process, as one instance. Production needs:
  - surviving the loss of a dispatcher;
  - a fixed number of Postgres connections, independent of the worker count.
- **Design:** active-active instances; workers reach them over gRPC.
  - Groups are owned through leases (`dispatcher_lease(group_id, owner, epoch, expires_at)`), with an epoch
    fence on commit.
  - Workers stream "N free slots" to the dispatchers.
  - The dispatchers do the fetches, preloads and batched commits, so workers need no database connection.
- **Test:**
  - 24k with 2 and 3 instances.
  - A drill that kills one instance mid-run. The oracle must pass, and the pause should stay within the lease
    timeout.
  - Measure the cost of the extra hop.

### B2 — Producers publish one event at a time · **measured**
- **Result (matrix-14):**
  - Direct inserts collapse under concurrent one-event producers: 2.5k/s, because `NOTIFY` takes a global lock
    at commit.
  - With `NOTIFY` off, publishing reaches 24k, but consumption falls to 12.9k (200 ms polling, growing backlog).
  - The fix is publishes through the dispatcher (B1).
- **User direction, 2026-11-20:** if this is a bottleneck, the publishers go through the same dispatcher
  cluster. Any instance accepts a publish and coalesces concurrent ones into one INSERT; the call returns once
  the row is committed. It is idempotent on (source, seq), and when it owns the group it hands the event
  straight to a waiting worker. This makes the dispatcher a complete queue system backed by SQL.
- **From:** user correction, 2026-11-20. Real producers send each event as soon as it is generated.
  `ProducerRunner` collects up to 500 per publish (`MaxPublishBatch`).
- **Why:** this flatters every Postgres path. For pgqueue at 24k it means about 24k insert transactions/s and
  about 24k `pg_notify`/s. NOTIFY takes a global lock at commit, which may become the ceiling. The pgqueue
  24k result is optimistic until retested.
- **Test:**
  - Rerun pgqueue and Kafka + PG at 24k with `Workload:MaxPublishBatch=1`, with the sync standby.
  - If pgqueue drops, add a publish gateway or coalescing client: send immediately, share an INSERT with
    concurrent calls, no timer. The gateway also tells the dispatcher about new groups, which removes the
    trigger.
- **Related:** the publishing half of the user's "layer 1" idea (a layer between the event sources and PG).

### B3 — Dispatcher-kill drill
- **From:** the failover discussion. Drill D2 killed Postgres; the dispatcher survived with its memory.
- **Why:** this checks the claim that in-memory state is only a cache. After a restart, re-fetched heads may
  be processed twice, but must be applied once (cursor check), in order.
- **Test:** part of B1. Until then, kill the in-process dispatcher (restart the worker host) mid-run.

### B4 — pgqueue blocking at 24k with a standby
- **From:** matrix-13. 106 violations on the primary alone, 11k with an async standby, 55k with sync
  `remote_write`; 0 at 12.9k.
- **Why:** the no-cross-source-blocking rule fails at the target rate.
- **Test:** tune `FetchParallelism` and `MaxGroupsPerFetch`, wake on completion without the 20 ms loop tick,
  more slots per worker, and the batched-commit executor count.

### B5 — Cross-data-center replication
- **From:** the user's goal of one cluster with 2–3 copies in another data center.
- **Why:** sync `remote_write` was free on one host. Over a WAN, every commit waits a round trip. Quorum
  (`ANY 1 (dc1_standby, dc2_standby)`) changes which failures lose data.
- **Test:** add network delay to the standby link (`tc netem`, 1/5/20 ms), and run quorum sync with two
  standbys. Measure throughput, commit latency and blocking, and repeat the failover drill.

### B6 — Automated failover and fencing
- **From:** matrix-13. Failover is a manual `pg_ctl promote`.
- **Why:** production needs automatic promotion, and the old primary must be fenced so two primaries never
  accept writes.
- **Test:** Patroni (with etcd, which is already in `infra/`) or pg_auto_failover. Drill: kill the primary,
  measure time to a new primary, and check that the oracle passes. Also partition the old primary instead of
  killing it.

### B7 — Separate machines
- **From:** every cluster round. Everything shares one 24-core host, so a cluster pays its replication cost
  without any scale-out benefit.
- **Why:** sizing, and a fair Kafka/NATS cluster comparison.
- **Test:** 3+ VMs or hosts. Rerun pgqueue (with sync standby), the Kafka cluster and the NATS cluster.

### B8 — Dynamic slots: one slot per active key
- **From:** user idea (slots = number of unique keys in the partition).
- **Why:** on a partitioned broker, a slow event cannot block others if each active source has its own slot.
- **Test:** set slots per worker to K (8/16/32) or adapt them to the parked sources. Measure blocking and memory
  on Kafka + PG and NATS + PG.

### B9 — Kafka cluster replication tuning
- **From:** matrix-10/11. With 3 brokers, publish takes about 900 ms p50 and throughput is 16.5–18.8k.
- **Test:** `replica.fetch.wait.max.ms`, `num.replica.fetchers`, `replica.fetch.min.bytes`, fewer partitions
  (780 × RF3). Best done together with B7.

### B10 — NATS R3 tuning
- **From:** matrix-12. R3 reaches 3–6k because producers wait about 2 s for each PubAck.
- **Test:**
  - Pipelined publishing with `Nats-Expected-Last-Subject-Sequence`.
  - Fewer, larger streams than one stream per group: 500 Raft groups is heavy.
  - Single-node NATS + PG is stuck at about 22k, limited by publish latency.

### B11 — pgqueue under a large backlog
- **From:** the pgqueue design review. The `NOT EXISTS` head probe runs once per candidate, so a large
  backlog makes fetches slower.
- **Test:**
  - Stop consumers for 5–10 min at 24k, then resume. Measure catch-up time and fetch latency.
  - Also measure standby catch-up after an outage: in Q2, a standby that started 11 GB behind dragged the
    primary to 10.8k.

### B12 — Local durability with a synchronous standby
- **From:** the best-effort comparison rule (`synchronous_commit=off` locally).
- **Why:** production would likely run `synchronous_commit=on` locally plus the sync standby. The cost at 24k
  with batched commits is unknown.
- **Test:** rerun pgqueue Q3 with local fsync on.

### B16 — The dispatcher queue on SQL Server and MySQL · **requested to consider**
- **From:** user, 2026-11-20. Once the dispatcher owns the queue logic, the database only needs transactions,
  batched inserts, an index and row locks for the commit, so other SQL engines may have the same property.
- **Test:**
  - Put the queue table and batched-commit procedure behind a backend interface. Ports: SQL Server (table +
    procedure, `READPAST` not needed) and MySQL/InnoDB.
  - Same 24k runs with one-event publishes.
  - Failover drills with each engine's synchronous replication: an SQL Server availability group with
    synchronous commit; MySQL semi-sync `AFTER_SYNC` or Group Replication.

### B17 — Distributed SQL under the dispatcher · idea
- **From:** dispatcher design §4.1. The dispatchers are active-active, but SQL is single-writer with synchronous
  replicas.
- **Why:** a Raft-based, Postgres-compatible database (YugabyteDB, CockroachDB) makes storage multi-writer as
  well. The dispatcher's large batches suit its higher cost per write.
- **Test:** the dispatcher backend on it, at 24k, with node-loss and DC-loss drills.

### B18 — Octopus: the Rust client keys running deliveries by source · open
- **From:** D4 failover drills, 2026-11-20. The .NET client aliased two deliveries of one source, so each such pair
  leaked one credit and workers ended up holding 5x their slots (fixed there: deliveries by identity, plus the
  invariant running + granted <= slots, enforced after each result and every second).
- **Next:** apply the same to `octopus-client` (`running: HashMap<String, Running>`), and add the invariant check
  to the dispatcher too (refuse to hand a worker more than its declared slots).

### B19 — Octopus: recovery after a database failover · open
- D6 (12.9k, synchronous standby): no loss, drained in 9 s, but blocking violations 289k vs 107k in the baseline
  and e2e p99 25 s vs 4 s. Find where the minutes of recovery go (publish retries backing off to 2 s; worker
  retries; ready heads dropped on the lapsed lease) and shorten them.

### B20 — Cost of the synchronous standby on one host · open
- D7: the same 12.9k with `remote_write` to a standby on the same host: e2e p50 164 ms (16 ms async), commit p50
  36 ms, Postgres 8.0 cores (5.8). Measure with the standby on its own cores (CPU pinning) and with an added
  network delay (tc netem) to estimate a real second DC.

### B21 — Peak capacity of one dispatcher · open
- From the user, 2026-11-20. Not measured yet: in every run Postgres saturates first. Measure with one dispatcher,
  rising load, until its own CPU or its database round trips limit it.

### B22 — Octopus: source epoch next to seq · open
- **From:** user, 2026-11-20: every event source will stamp a gapless seq plus an epoch (the origin of the sequence).
- **Plan:** key (source, epoch, seq); cursor (epoch, seq). A publish from an older epoch than the source's current one
  is rejected (fencing: a stale producer instance cannot inject events). The first event of a new epoch carries the
  last seq of the previous epoch (if known), so the old epoch can be drained before the new one starts; if not
  known, an operator or policy decides. Proto fields, schema column, TLA+ model update.

### B23 — Octopus: heal permanent gaps automatically · proposed
- **From:** user, 2026-11-20: at 24k ev/s nobody can chase single missing events, and not every event matters.
- **Proposal:** (1) explicit skip markers from the producer (it gives up on a seq, or a new epoch declares where the
  old one ended); (2) a per-group gap timeout: when cursor + 1 is missing and a later event has waited longer than
  T (database clock), the owner skips the gap in one transaction (cursor moves, a skip record is kept); `none` for
  strict groups; (3) the next delivery carries the skipped range so the application can compensate or resync;
  (4) a late arrival of a skipped seq is rejected with its own status and kept in a late table; (5) metrics and
  alerts on rates (skips, late arrivals, gap age), not on single events. TLA+: a Skip action; safety becomes
  "in order, minus declared skips; nothing applied after it was skipped".

### B24 — Octopus: producer retries forever, gap alarm, conflict check, out-of-order e2e test · open
- The .NET producer gives up after 20 attempts (a permanent gap); it should retry forever and alert.
- Report sources whose next event is missing too long; store a body hash and report a reused seq with a different
  body as a conflict; an end-to-end test with out-of-order and lost publishes.

### B13 — Citus (distributed Postgres) · idea
- **From:** "some tools are stronger when clustered", together with "one platform, not 20 Postgres instances".
- **Why:** one logical database with shards by group, if one primary is not enough.
- **Test:** pgqueue and the store sharded by `group_id` on 2–3 workers.

### B14 — Clustered SQL Server and MongoDB · idea
- **From:** matrix-6 ran single-node only.
- **Test:** SQL Server availability group (synchronous commit), and a MongoDB replica set with
  `w: majority`, each with a failover drill. Low priority: neither led on one node.

### B15 — Housekeeping
- Rerun the full test suite (65 tests) after the final runner fixes. It was last run in full before them.
- Matrices 1–8 were measured behind the rootless podman proxy, which caps the Postgres paths at about 19k.
  Rerun any of them that a decision depends on.

## Done

| item | result |
|---|---|
| Cross-source blocking measured; pipelined worker | `results/matrix-7-blocking` |
| PGMQ tuning (eb_read_head, no vt index, fill factor, unlogged, linger) | Stays at about 11k: PG CPU on per-queue tables, vt rewrites and polling (`matrix-8`, `matrix-9`) |
| In-process dispatcher over PGMQ (pgdispatch) | 20.8k, 12.7 PG cores (`matrix-9`) |
| Host networking instead of the podman proxy | Removed the 19k cap (`matrix-9`) |
| Kafka 3-node + PG standby, broker-kill drill | Passes; 16.5–18.8k (`matrix-10`, `matrix-11`) |
| Redis Sentinel, async and `WAIT 1`, failover drills | Out: loses or duplicates acknowledged work (`matrix-11`) |
| NATS R3, node-kill drill | Passes; 3–6k (`matrix-12`) |
| Bake-off (B0): MassTransit v8 SQL transport, Temporal | Both correct; about 4k and under 1k ev/s. Neither comes near 24k (`matrix-14`) |
| pgqueue + PG sync standby, primary-kill drill | #1: 24.0k on about 9 cores, no loss with sync; async loses data (`matrix-13`) |

## Dropped

| item | why |
|---|---|
| Solace | The user decided: one slow event blocks its whole partition queue. |
| Multiple PgBouncer processes | No gain (19.0k vs 19.1k); the cap was the podman proxy. |
| Unlogged queue tables | +9% only, and not durable. |
| ~20 separate Postgres instances (split by group) | The user wants one platform. |
