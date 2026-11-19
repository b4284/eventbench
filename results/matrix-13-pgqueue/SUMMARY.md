# Matrix 13 — pgqueue: Postgres-only platform (dispatcher-native queue) at 24k, with HA and failover drills

## Design (`Broker:Type=pgqueue`, `src/EventBench.Postgres/PgQueueBroker.cs`)

**The queue table:** one hash-partitioned table (`ebq_msg`, 16 partitions) for all 500 groups, keyed
`(group_id, msg_id)`, plus an index `(group_id, source_id, msg_id)`.

**Producers** insert each batch with one multi-row `INSERT … unnest`, across all of the batch's groups.

**The dispatcher** is in process here, and is the sole reader of its groups:
- Workers wait in memory for it; they never poll the database.
- It reads source **heads** (rows with no older row of the same source), minus what it already has in flight,
  which it tracks in memory.
- Reads write nothing: there is no `vt` column.
- A source's next message becomes a head when the previous one is deleted, so concurrent producers that
  commit ids out of order cannot cause a miss.
- It wakes on a statement-level insert trigger (transition table → `pg_notify` per touched group), on completed
  events, or on a 200 ms fallback.
- It loads every fetched event's context in the same pass (preload).

**Commits** use the store's group commit: many events per transaction (7–12 at 24k). The queue row's delete is
inside the same transaction (exactly once).

**Retries** stay in memory: no database write; the row is offered again after the delay.

**Workload:** 24,033 ev/s offered (`RateMultiplier=1.87`), 0.5% slow calls of 1–3 s plus every large event,
pipelined worker with 4 slots, host networking, one host. Q0 runs at 12,856 ev/s.

| run | verdict | applied/s | e2e p50 / p90 / p99 | ready→start p50 / p99 | blocking violations | PG cores | standby cores | standby lag mean / max |
|---|---|---:|---|---|---:|---:|---:|---|
| Q0 12.9k, primary | **PASS** (all checks) | 12,853 | 4 / 8 / 1,170 ms | 2 / 10 ms | **0** | 6.7 | — | — |
| Q1 24k, primary | FAIL (blocking only) | **24,042** | **14 / 32** / 1,566 ms | 7 / **34 ms** | **106** | **8.7** | — | — |
| Q2b 24k + async standby (caught up) | FAIL (blocking only) | 24,042 | 21 / — / 1,591 ms | — / 96 ms | 11,050 | 8.8 | 0.6 | 4.4 / 16 MB |
| Q3 24k + **sync** standby (`remote_write`) | FAIL (blocking only) | 23,973 | 25 / 114 / 1,624 ms | 15 / 248 ms | 54,516 | 8.9 | 0.7 | 15 / 66 MB |
| Q2 24k + async standby that started 11 GB behind | FAIL (blocking only) | 10,817 | 30.6 s | 421 ms / 12.2 s | 880,183 | 7.6 | 0.9 | 578 / 1,174 MB |
| **D2** 24k, sync standby, **primary killed + standby promoted** | **all correctness checks pass** | 21,512 | 712 ms / — / 12.9 s | — / 2.7 s | 412,423 | 8.0 | 3.8 | 0.3 / 0.5 MB |
| **D1** 24k, async standby, **primary killed + standby promoted** | **FAIL: data loss** | 16,334 | 1.9 s / — / 69.3 s | — / 9.9 s | 697,667 | 11.8 | 5.1 | 3.4 / 27 MB |

e2e p99 includes the 1–3 s slow calls themselves.

**The failover drill** (`infra/pg-standby/failover.sh`):
1. `podman kill eb-postgres`, then `pg_ctl promote` on the standby. The standby was promoted 1.8 s after the
   kill.
2. Clients use `configs/overlay-pg-failover.json`: `Host=127.0.0.1:5432,127.0.0.1:5433;Target Session
   Attributes=read-write`, direct connections without PgBouncer.
3. The dispatcher's LISTEN connection reconnected 2.9 s after the kill and re-checked every group.

**D2, synchronous `remote_write`:**
- 648 operations failed during the kill and were retried.
- No event was lost, applied twice or reordered: cursors, records, uploads and exactly-once all pass, and the
  queue drained.

**D1, async:** the promoted standby lacked the last acknowledged commits.
- 44,440 events were never applied and 44,195 messages were left stranded.
- 1.5M ordering-gap retries followed: a lost producer insert leaves its successors waiting forever.

## Findings

1. **One Postgres primary carries the whole system at 24k ev/s** — queue, state and uploads — on **about 9 cores**
   (0.37 cores per 1k ev/s), at 14–25 ms e2e p50. For comparison:
   - PGMQ needed 15–17 cores and still fell behind at 11k.
   - Kafka + Postgres needs 3.9 Postgres cores plus 3.6 Kafka cores on a single node, or about 8 Kafka cores
     as a 3-node cluster.
2. **It meets the no-blocking rule at 12.9k (0 violations).** At 24k, 106 violations remain on the primary
   alone, and 11k–55k with a standby. Those come from dispatcher fetch latency when commits slow down with
   replication. Tuning is still open: fetch parallelism, wake-up path, slots per worker.
3. **Fail-safe needs a synchronous standby.** With `remote_write`, a primary kill loses nothing, and the
   synchronous standby costs nothing measurable at 24k (Q3 vs Q2b). Async replication loses acknowledged
   work on failover. That is the same failure class as Redis Sentinel, and true of any async replica.
4. **A standby left behind must catch up before it is measured.** Q2 started 11 GB behind: replaying that
   and competing for the WSL disk dragged the primary down to 10.8k.

## Ranking after four rounds (correctness under failure first, then 24k on this host, then blocking)

| # | platform | node-failure drill | at 24k on one host | blocking at 24k | one platform / SQL data |
|---|---|---|---|---|---|
| 1 | **pgqueue + Postgres primary + sync standby** | **passes** (primary kill) | **24.0k, 25 ms p50, PG about 9 cores** | 55k violations (0 at 12.9k) | **yes** |
| 2 | Kafka (3 brokers) + Postgres + standby, group commit | passes (broker kill) | 16.5–18.8k (Kafka about 8 cores, publish about 900 ms) | 316k–438k | state in PG, queue in Kafka |
| 3 | NATS JetStream R3 + Postgres | passes (node kill) | 3–6k | — | state in PG, queue in NATS |
| — | Redis Sentinel; any async-replicated store | **loses or duplicates acknowledged work** | — | — | — |
