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

### B1 — Dispatcher as a separate 2–3 instance cluster · **done** (Octopus; MQTT instead of gRPC; kill/stop/scale-out drills pass: matrix-16 R1–R3, P1)
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

### B2 — Producers publish one event at a time · **done** (every Octopus run publishes one event per call through the dispatchers)
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

### B3 — Dispatcher-kill drill · **done** (matrix-16 D1, R1, P1)
- **From:** the failover discussion. Drill D2 killed Postgres; the dispatcher survived with its memory.
- **Why:** this checks the claim that in-memory state is only a cache. After a restart, re-fetched heads may
  be processed twice, but must be applied once (cursor check), in order.
- **Test:** part of B1. Until then, kill the in-process dispatcher (restart the worker host) mid-run.

### B4 — pgqueue blocking at 24k with a standby · parked (Octopus chosen; pgqueue stays the in-process baseline)
- **From:** matrix-13. 106 violations on the primary alone, 11k with an async standby, 55k with sync
  `remote_write`; 0 at 12.9k.
- **Why:** the no-cross-source-blocking rule fails at the target rate.
- **Test:** tune `FetchParallelism` and `MaxGroupsPerFetch`, wake on completion without the 20 ms loop tick,
  more slots per worker, and the batched-commit executor count.

### B5 — Cross-data-center replication · **partly done** (2026-11-21, matrix-23: three machines, Macs since retired)
- **From:** the user's goal of one cluster with 2–3 copies in another data center.
- **Why:** sync `remote_write` was free on one host. Over a WAN, every commit waits a round trip. Quorum
  (`ANY 1 (dc1_standby, dc2_standby)`) changes which failures lose data.
- **Test:** add network delay to the standby link (`tc netem`, 1/5/20 ms), and run quorum sync with two
  standbys. Measure throughput, commit latency and blocking, and repeat the failover drill.
- 2026-11-21, matrix-23 (`infra/patroni-geo`: DC1 laptop, DC2 m4mini, witness m1mini, 0.56 ms apart on one LAN):
  - **Bandwidth, not distance, set the cost.** WAL was 12 KiB/event with max_wal_size 4GB (a checkpoint every
    ~15 s, page-image storms), 7.1 KiB with 12GB + zstd. At 4GB, waiting for 2 of 3 standbys was 4–6× slower,
    capped by m4mini's 1 GbE link carrying two copies. At 12GB it cost nothing measurable.
  - At 24k ev/s: ~1.4 Gbit/s per standby, ~2.8 Gbit/s between DCs with two DC2 standbys. Floor for any store
    ~0.6 Gbit/s per copy (~3 KB of real data per event).
  - **remote_write doesn't bound replay lag:** DC2's two standbys on one nearly full Mac SSD fell 15–17 min
    behind in applying, so a failover to them would take that long.
- **Not explored yet** (the user retired the Macs on 2026-11-21; needs other hosts, or `tc netem` on one host):
  - `remote_apply`: what a bounded recovery time costs;
  - a replay-lag alert and Patroni `maximum_lag_on_failover` with a non-zero limit;
  - DC kill and partition drills on real machines (the single-host versions pass: matrix-19, matrix-22);
  - one WAN copy (DC2b cascades from DC2a) and its quorum cost;
  - compressing payloads at the source, or a compressing tunnel between the DCs;
  - added latency (1/5/20 ms) on the DC link.

### B6 — Automated failover and fencing · **done** (Patroni 2+2, etcd 3.5.17, HAProxy endpoint: matrix-18, matrix-19)
- **From:** matrix-13. Failover is a manual `pg_ctl promote`.
- **Why:** production needs automatic promotion, and the old primary must be fenced so two primaries never
  accept writes.
- **Test:** Patroni (with etcd, which is already in `infra/`) or pg_auto_failover. Drill: kill the primary,
  measure time to a new primary, and check that the oracle passes. Also partition the old primary instead of
  killing it.

### B7 — Separate machines · **partly done** (matrix-17: app on a Mac mini; the 1 GbE link saturates at 16k with inline large events; matrix-23: Postgres DC2 on a second Mac; a real cluster comparison still needs 10 GbE hosts; Macs retired 2026-11-21)
- **From:** every cluster round. Everything shares one 24-core host, so a cluster pays its replication cost
  without any scale-out benefit.
- **Why:** sizing, and a fair Kafka/NATS cluster comparison.
- **Test:** 3+ VMs or hosts. Rerun pgqueue (with sync standby), the Kafka cluster and the NATS cluster.

### B8 — Dynamic slots: one slot per active key · parked (for partitioned brokers, not chosen)
- **From:** user idea (slots = number of unique keys in the partition).
- **Why:** on a partitioned broker, a slow event cannot block others if each active source has its own slot.
- **Test:** set slots per worker to K (8/16/32) or adapt them to the parked sources. Measure blocking and memory
  on Kafka + PG and NATS + PG.

### B9 — Kafka cluster replication tuning · parked (Kafka not chosen)
- **From:** matrix-10/11. With 3 brokers, publish takes about 900 ms p50 and throughput is 16.5–18.8k.
- **Test:** `replica.fetch.wait.max.ms`, `num.replica.fetchers`, `replica.fetch.min.bytes`, fewer partitions
  (780 × RF3). Best done together with B7.

### B10 — NATS R3 tuning · parked (NATS not chosen)
- **From:** matrix-12. R3 reaches 3–6k because producers wait about 2 s for each PubAck.
- **Test:**
  - Pipelined publishing with `Nats-Expected-Last-Subject-Sequence`.
  - Fewer, larger streams than one stream per group: 500 Raft groups is heavy.
  - Single-node NATS + PG is stuck at about 22k, limited by publish latency.

### B11 — pgqueue under a large backlog · parked; the same question applies to Octopus (catch-up after a long consumer outage), see B27
- **From:** the pgqueue design review. The `NOT EXISTS` head probe runs once per candidate, so a large
  backlog makes fetches slower.
- **Test:**
  - Stop consumers for 5–10 min at 24k, then resume. Measure catch-up time and fetch latency.
  - Also measure standby catch-up after an outage: in Q2, a standby that started 11 GB behind dragged the
    primary to 10.8k.

### B12 — Local durability with a synchronous standby · open (now for Octopus: `synchronous_commit=on` locally plus the sync standby)
- **From:** the best-effort comparison rule (`synchronous_commit=off` locally).
- **Why:** production would likely run `synchronous_commit=on` locally plus the sync standby. The cost at 24k
  with batched commits is unknown.
- **Test:** rerun pgqueue Q3 with local fsync on.

### B16 — The dispatcher queue on SQL Server and MySQL · **SQL Server ported and measured** (2026-11-22, matrix-27: correct, ~4–5× Postgres CPU per event, not steady at 16k); MySQL open
- **From:** user, 2026-11-20. Once the dispatcher owns the queue logic, the database only needs transactions,
  batched inserts, an index and row locks for the commit, so other SQL engines may have the same property.
- **Test:**
  - Put the queue table and batched-commit procedure behind a backend interface. Ports: SQL Server (table +
    procedure, `READPAST` not needed) and MySQL/InnoDB.
  - Same 24k runs with one-event publishes.
  - Failover drills with each engine's synchronous replication: an SQL Server availability group with
    synchronous commit; MySQL semi-sync `AFTER_SYNC` or Group Replication.
- 2026-11-22, matrix-27: Octopus `crates/octopus-mssql` (SQL Server 2025). The 16-test backend suite passes;
  `octopusd` selects it with `OCTOPUS_BACKEND=mssql`; the benchmark runs it (`OCTOPUS_BACKEND=mssql infra/octopus/up.sh`,
  `--Broker:AdminDatabase=sqlserver`). At 16k it needs ~4–5× Postgres's CPU per event and swings between 11.6k and
  16.1k ev/s applied. Fixed so far: lock waits, deadlocks, tempdb metadata, a blocking purge (see the NOTES).
  - **Not explored yet:** memory-optimized tables (In-Memory OLTP) for the event table; cheaper batch passing than
    JSON with base64; settle's delete cost; the application's writes on SQL Server; the availability-group failover
    drill.

### B17 — Distributed SQL under the dispatcher · **measured: not viable for 24k** (2026-11-21, matrix-26: YugabyteDB runs Octopus unchanged but needs 25–45× Postgres's CPU per event)
- **From:** dispatcher design §4.1. The dispatchers are active-active, but SQL is single-writer with synchronous
  replicas.
- **Why:** a Raft-based, Postgres-compatible database (YugabyteDB, CockroachDB) makes storage multi-writer as
  well. The dispatcher's large batches suit its higher cost per write.
- **Test:** the dispatcher backend on it, at 24k, with node-loss and DC-loss drills.

- 2026-11-21, matrix-26 (YugabyteDB 2026.1, one node, RF 1):
  - All 23 Octopus tests pass unchanged; only the migration retries 40001 (Octopus c3f960b).
  - 16k offered: 2.2k ev/s on 15 cores by default; 1.7k with the cost-based optimizer off; 3.0k colocated
    (11.6 cores). Postgres: 16k on 2.5 cores.
  - Causes: optimizer plans from "empty" statistics, tombstones of deleted events, distributed transactions per
    write.
  - **Not explored yet:** RF 3 and node-loss drills (failover time), compaction tuning, pg_hint_plan, bigger
    batches, CockroachDB.

### B18 — Octopus: the Rust client keys running deliveries by source · **done** (Octopus `4e596b1`, see matrix-16 R0–R6)
- **From:** D4 failover drills, 2026-11-20. The .NET client aliased two deliveries of one source, so each such pair
  leaked one credit and workers ended up holding 5x their slots (fixed there: deliveries by identity, plus the
  invariant running + granted <= slots, enforced after each result and every second).
- **Next:** apply the same to `octopus-client` (`running: HashMap<String, Running>`), and add the invariant check
  to the dispatcher too (refuse to hand a worker more than its declared slots).

### B19 — Octopus: recovery after a database failover · **measured** (2026-11-21, matrix-19 H, T10)
- Leader's DC killed (2+2, HAProxy): writes stop ~2 s after the kill; a new leader 17-21 s after it; throughput back
  above half within **2-6 s** of the new leader (HAProxy marking it up, reconnects, Octopus resuming); one outlier
  31 s. The outage is Patroni's detection: its leader lease cannot go below 20 s (a 10 s setting is still granted
  as 20 s by etcd). Planned switchovers (`patronictl switchover`) avoid the detection time.
- 10-minute database freeze under load: no loss, no memory growth (dispatchers flat at 0.5-0.6 GB), producers'
  "publish stuck" gauge raised (1,804) and cleared, drained 10 s after the database returned.
- Left: the 31 s outlier (H10); the catch-up rate after long outages is bounded by this host's capacity.
- Earlier text:
- D6 (12.9k, synchronous standby): no loss, drained in 9 s, but blocking violations 289k vs 107k in the baseline
  and e2e p99 25 s vs 4 s. Find where the minutes of recovery go (publish retries backing off to 2 s; worker
  retries; ready heads dropped on the lapsed lease) and shorten them.

### B20 — Cost of the synchronous standby on one host · open
- D7: the same 12.9k with `remote_write` to a standby on the same host: e2e p50 164 ms (16 ms async), commit p50
  36 ms, Postgres 8.0 cores (5.8). Measure with the standby on its own cores (CPU pinning) and with an added
  network delay (tc netem) to estimate a real second DC.
- 2026-11-21, matrix-23: across machines, the cost came from checkpoints (max_wal_size), not from waiting itself.
  See B5. CPU pinning and netem on one host are still open.

### B21 — Peak capacity of one dispatcher · **done** (2026-11-21, matrix-25: one dispatcher carries 24k at a 15 ms median, saturates before 32k; three carry 32k)
- From the user, 2026-11-20. Not measured yet: in every run Postgres saturates first. Measure with one dispatcher,
  rising load, until its own CPU or its database round trips limit it.

- 2026-11-21, matrix-25 (workers commit to memory, so Postgres carries Octopus alone):
  - one dispatcher: 24k at p50 15 ms on 1.25 cores; at 32k still 31k/s but p50 1.5 s on 1.3 cores (a serial limit,
    not CPU);
  - three dispatchers: 32k at p50 15 ms;
  - Octopus costs ~0.1 Postgres core per 1k ev/s, so ~80% of Postgres in the full runs is the application's writes;
  - Mosquitto (single-threaded) at 74% of a core at 32k.
  - **Not explored yet:** profiling what serializes one dispatcher; Mosquitto's ceiling and multi-threaded brokers;
    2 dispatchers.

### B22 — Octopus: source epoch next to seq · **done** (2026-11-21; positions `(epoch, seq)`, integer source ids in event rows, epoch declarations, fencing)
- **From:** user, 2026-11-20: every event source will stamp a gapless seq plus an epoch (the origin of the sequence).
- **Plan:** key (source, epoch, seq); cursor (epoch, seq). A publish from an older epoch than the source's current one
  is rejected (fencing: a stale producer instance cannot inject events). The first event of a new epoch carries the
  last seq of the previous epoch (if known), so the old epoch can be drained before the new one starts; if not
  known, an operator or policy decides. Proto fields, schema column, TLA+ model update.

### B23 — Octopus: heal permanent gaps automatically · **done** (2026-11-21; high-water announcements and client repair helpers in Octopus 2026-11-21; Octopus-initiated resend requests not built: the acknowledgement report plus high-water cover the same ground)
- **Done:** "missing below" in every publish reply; skip markers; per-group gap policy (`grp.gap_skip_ms`,
  `OCTOPUS_GAP_SKIP_MS`) skipping within an epoch or closing an epoch; skips recorded and listed on the next
  delivery; `LATE` status and late table; TLA+ model `spec/WorkerCommit.tla`.
- **Not built yet:** resend requests from Octopus to the producer, and producers' periodic high-water
  announcements (catch a lost *last* event); a client helper that reacts to "missing below" (replay from an
  outbox or send a marker).
- **From:** user, 2026-11-20: at 24k ev/s nobody can chase single missing events, and not every event matters.
- **Proposal:** (1) explicit skip markers from the producer (it gives up on a seq, or a new epoch declares where the
  old one ended); (2) a per-group gap timeout: when cursor + 1 is missing and a later event has waited longer than
  T (database clock), the owner skips the gap in one transaction (cursor moves, a skip record is kept); `none` for
  strict groups; (3) the next delivery carries the skipped range so the application can compensate or resync;
  (4) a late arrival of a skipped seq is rejected with its own status and kept in a late table; (5) metrics and
  alerts on rates (skips, late arrivals, gap age), not on single events. TLA+: a Skip action; safety becomes
  "in order, minus declared skips; nothing applied after it was skipped".
- **Resend requests (user, 2026-11-20):** before skipping, Octopus asks the source to replay the missing range
  (a resend request to the producer; repeated until answered, rate-limited, deduplicated per range). The producer
  replays from durable storage (outbox or source DB) with the same epoch and seq, or answers "unavailable", which
  skips at once. No answer within T: skip. Only useful if the source can regenerate lost events: a producer that is
  still retrying needs no request. Producers also announce their high-water mark (epoch, last seq) now and then, so
  a lost *last* event (no later event shows the gap) is detected too.
- **Gap report in the publish acknowledgement (user, 2026-11-20; preferred):** each publish reply also says which
  seqs directly below it are missing (e.g. "N+2 stored, missing N+1"): one backward index probe for the highest
  stored or applied seq below it, inside the publish statement. The producer judges each missing seq: still pending
  in its own retries = ignore (in flight); never sent or lost after a restart = replay from durable storage, or
  send a skip marker; acknowledged earlier = a bug, alert. A report may be transient (the missing event is
  committing in another dispatcher), which this rule tolerates. Resend requests stay for sources that stop
  publishing (a lost last event).

### B24 — Octopus: producer retries forever, gap alarm, conflict check, out-of-order e2e test · **done** (matrix-16 R0–R6)
- The .NET producer gives up after 20 attempts (a permanent gap); it should retry forever and alert.
- Report sources whose next event is missing too long; store a body hash and report a reused seq with a different
  body as a conflict; an end-to-end test with out-of-order and lost publishes.

### B25 — Octopus: database cost of head scans and key lookups · **done** (2026-11-21, matrix-24: Octopus 7d1de4d, bd54c5d, 257cc83; ~1.2 Postgres cores less at 16k)
- Done: settle() skips epoch-end and marker work unless needed (partial index on markers); publish skips
  registration for known sources/groups and the "missing" lookup when it is obviously 0; key re-polling removed
  earlier. A/B at 16k (matrix-20): cursor writes out of the top statements (were 55-77 ms per call), publish 4.3 ms
  vs 4.8-12.0, Postgres 8.6 vs 8.8-9.0 cores.
- Caveat: by the end of the day the lab host ran 3-5x slower than in the morning for the same runs (thermal?),
  so absolute numbers need re-measuring on a cool or separate machine.
- Left: fewer fallback head scans (1 s per credited group); the publish statement itself (CTE + insert).
- R0 (16k, 3 dispatchers), pg_stat_statements over 40 s: fallback head scans 56k calls / 43 s, key lookups
  2.7k calls at 8 ms each / 22 s, publish 2.6 ms per call / 96 s. Fewer fallback scans when hints and "done"
  already say what is next; cheaper key lookups.

- 2026-11-21, matrix-24 (see its NOTES.md):
  - Kept: head reads leave out sources whose "done" cursor isn't written yet (260–284k reads per run -> 25–30k;
    Postgres 9.5 -> 8.3 cores); bodies up to 8 KB stay in the row (publish ~20% cheaper); next-event lookups only
    when known stored.
  - Rejected: fewer fallback reads (not the waste); a 1 s vacuum loop (slower); an 8-way partitioned event table
    (fewer extension waits, more CPU).
  - **Not explored yet:**
    - `autovacuum_naptime` 1 s, which needs the lab container recreated;
    - fillfactor 100 on the event table (inserts never update in place);
    - rotating event tables (truncate instead of delete);
    - `advance` at 100–140 ms per call (cursor writes plus deletes);
    - the benchmark's own `eb_commit_many` waiting on its tables' extension lock.

### B26 — Benchmark: no replica during performance runs; repeat runs for variance · open
- 2026-11-21 afternoon: the lab host's effective speed dropped ~25% over the day (Postgres needs ~25% more CPU time
  per event for the same statements; this morning's exact code, Octopus b78df62 + benchmark a14add94b, now gives
  1.4-3.2 s median at 16k instead of 36 ms). Not code, not the database (fresh tables, catalogs fine, WAL reset,
  restart), not other load (Windows idle). 16k sits at the knee, so -25% capacity explodes latency. Likely Windows
  power/scheduling of the WSL VM (heat soak; "Balanced" plan moving it to efficiency cores while the user is
  away). Before comparing configurations: Windows "Best performance", plugged in, a fresh baseline in the same
  session, and A/B runs interleaved.
- A running async standby on the same host raised e2e p50 at 16k from ~33 ms to 150-530 ms (WAL replay competes
  for the same cores). Performance runs must check that no standby is running. Two identical 16k runs gave
  55k and 20k blocking violations: compare configurations with 2-3 repeats, not single runs.

### B27 — Octopus: failure drills · **done** (2026-11-21; real DC partition matrix-22; DC drills on separate machines deferred, see B5)
- Done: MQTT broker restart (found and fixed a bug), a dispatcher cut from the database, database frozen 2 min,
  Patroni leader kill/freeze (1+1), node kill, either DC killed, DC frozen 60 s (2+2; found the etcd 3.5.9 split
  brain and the multi-host client stall). Done since: a 10-minute database outage under load (matrix-19 L1); half of
  every group's workers crashing at once (matrix-21: nothing lost; re-offering a forgotten worker's events took p99
  from 30 s to 10.6 s). Left: a real network partition between DCs (frozen containers approximate it).
- From the drill review, 2026-11-20. Passed so far: dispatcher kill/stop/scale-out (16k), Postgres primary
  failover with a synchronous standby (12.9k; correct, slow recovery: B19).
- Still to drill: MQTT broker loss and restart; a dispatcher cut off from the database while alive (its leases
  expire under it); network partition between DCs and loss of a DC (needs the two-DC lab, B5); a database outage
  of minutes; many workers dying at once; failover through Patroni (realistic detection time) rather than a script.

- 2026-11-21: **real partition done** (matrix-22, `infra/patroni-bridged`): DC1 cut from the WAN while holding the leader. It demoted itself in 12 s, DC2 took over in 33 s, nothing lost, no cancelled commits. Rejoin found that replica rebuilds waited for a spread checkpoint (~14 min). Fixed with `basebackup: checkpoint: fast`.

### B28 — Decision: write availability after losing one of two database nodes · **decided: 2 + 2** (2026-11-21; two nodes per DC, Patroni quorum mode ANY 2 of 3 standbys, 3 etcd members)
- **From:** the Patroni drill T1, 2026-11-21. Two data nodes in strict synchronous mode: after a failover the new
  primary has no synchronous standby, so no commit completes until the lost node is back.
- **Options:**
  1. Keep two nodes, strict: zero loss always; writes stop until the lost node (or a replacement) rejoins
     (minutes for a pod restart; longer for a lost DC).
  2. Three data-bearing nodes (e.g. two in DC1, one in DC2; `synchronous_node_count` / ANY 1 of 2): survives one
     node with writes and zero loss; a whole-DC loss still leaves one copy, then option 1 or 3 applies.
  3. Non-strict synchronous mode: after a failover, writes continue with a single copy until a standby rejoins;
     a second failure in that window loses those writes.
- Related: a cancelled commit is acknowledged without replication (documented in Octopus design §5.5); the
  benchmark's 60 s command timeout on commits does exactly that.

### B29 — Octopus: deadlocks on octopus.lease between dispatchers · **done** (Octopus d8de3c4: rows locked in group order)
- Seen in the Patroni drill logs (2026-11-21): "deadlock detected while updating tuple in relation lease". Lease
  renewal and claims update several rows without a common order; two dispatchers can deadlock. Harmless (the
  next tick retries) but it costs a lease round and log noise: lock rows in group order.

### B30 — A network load balancer in place of HAProxy · open (from the user, 2026-11-21: probably the default in production)
- Any L4 load balancer works if its health check is HTTP GET /primary on Patroni's port 8008 and traffic goes to
  TCP 5432 on all nodes. It differs from the lab's HAProxy in three ways:
  - detection: cloud health checks run every 5 s and need 2+ failures, against 1 s and 2 here;
  - established sessions to a dead leader: HAProxy cuts them (`shutdown-sessions`), while many NLBs keep the flow
    until an idle timeout (AWS: 350 s) unless "terminate connections on unhealthy targets" is on;
  - targets in both DCs.
- Test: HAProxy set up like an NLB (inter 5 s, fall 2, no shutdown-sessions) in the 2+2 lab; leader-DC kill drill
  x5. Measure the outage and whether clients recover on TCP keepalive and operation timeouts alone. Then the same
  with session cutting on, which is the setting to require from the NLB.

### B31 — MQTT broker: the first ceiling, and a single point of failure · open (2026-11-22, matrix-29)
- Mosquitto is single-threaded: 84–93% of its core at 16–24k with remote clients, 97% with more connections, when
  every shard's round trips rose 4×. The setup tops out at ~22–23.5k ev/s; Postgres is next. Mosquitto also has
  no clustering, the topology's one remaining single point of failure (docs/topology.md).
- Tried: NanoMQ 0.25.7, worse (650 ms round trips, ~3.7k ev/s applied).
- 2026-11-22, matrix-30: EMQX 6.3.2 (single node; clustering needs a commercial license from 5.9) did not lift
  the ceiling on the shared laptop: 2.6–2.7 cores against Mosquitto's ~0.9, taken from Postgres; longer round
  trips. The ceiling is the laptop's total CPU.
- Next: the broker on its own machine (lab), a broker-node kill drill, and NATS (B32). Separately, fewer
  MQTT messages per event in Octopus (batch publish replies, done reports and their replies).

### B32 — NATS as Octopus's transport · open (from the user, 2026-11-22)
- NATS (Apache 2.0, multi-threaded, clustering free) as a native transport next to MQTT. NATS's MQTT gateway speaks
  only 3.1.1 and Octopus's clients speak MQTT 5, so it needs a NATS implementation of the transport layer
  (crates/octopus-fabric, ~170 lines, and the .NET client's equivalent).
- Gaps to cover: retained messages (Octopus re-announces every 5 s already, or a JetStream KV bucket) and last will
  (lease expiry and worker timeouts already detect a dead dispatcher). Topics map `/`->`.`, `+`->`*`, `#`->`>`.
- Mainly for high availability (B31: Mosquitto cannot cluster, EMQX clustering is licensed). matrix-30 suggests
  it won't raise throughput on the shared lab laptop.

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
