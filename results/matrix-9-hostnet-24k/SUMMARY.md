# Matrix 9 — 24k ev/s on host networking (and matrix 8: PGMQ tuning)

## Environment change: no userspace port proxy

Until this matrix, every container published its ports through rootless podman's **pasta** proxy. pasta is
a single-threaded userspace process per container. The one in front of Postgres and PgBouncer averaged 40%
of a core over its whole lifetime, idle time included, and it capped every Postgres-store combination at
about 19k ev/s:
- Postgres load and commit took 8.6 ms each, although Postgres was mostly idle (5.5 cores, 10 active
  backends).
- Running 4 PgBouncer processes instead of 1 changed nothing (19.0k vs 19.1k).

All infra scripts now default to `--network host` (`EB_NETWORK=pasta` restores published ports). On host
networking the same round trips take 0.6 ms. **Results from matrix 1–8 for Postgres, NATS, Kafka and
RabbitMQ paths were measured behind the proxy.** Redis paths were affected less, because Redis pipelines
over 8 connections.

## Setup

- `configs/pgmq-500groups.json` plus `configs/overlay-slow.json`: 0.5% slow calls of 1–3 s, plus every
  large event; 4 slots per worker.
- Pipelined worker. `RateMultiplier=1.87` gives 24,033 ev/s with 780 workers.
- Best effort, as in matrix-5 to matrix-7. 4 PgBouncer processes. One rep each.

## Results at 24k

| combo | applied/s | e2e p50 | ready→start p50 / p99 | blocking violations | CPU (cores) |
|---|---:|---:|---|---:|---|
| 04 Kafka + PG, **group commit** | **24,046** | **27 ms** | 8 / **54 ms** | **1,646** | pg 3.9, pgb 0.9, kafka 3.6, bench 9.0 |
| 07 Redis Streams + Redis (txack) | 24,129 | 23 ms | 6 / 279 ms | 39,698 | redis 3.8, bench 4.6 |
| 03 Kafka + PG | 25,207 (draining) | 89 ms | 21 / 730 ms | 84,217 | pg 6.1, pgb 1.7, kafka 3.2, bench 9.2 |
| 08 NATS + Redis | 24,016 | 115 ms | 45 / 486 ms | 133,853 | nats 4.7, redis 2.5 |
| 02 NATS + PG, group commit | 23,001 | 1.4 s | 110 / 2,443 ms | 645,620 | pg 3.8, nats 4.2 |
| 01 NATS + PG | 21,548 | 7.0 s | 142 / 3,414 ms | 759,429 | pg 5.0, nats 4.1 |
| 05 pgdispatch mode B (preload + group commit) | 20,839 | 4.3 s | 139 / 3,301 ms | 684,818 | **pg 12.7** |
| 06 PGMQ tuned (eb_read_head, no vt index, ff 70) | 11,044 | 26 s | 70 / 6,578 ms | 296,066 | pg 16.8 (pinned) |

Every run fails only `runtime_cross_source_blocking`. Ordering, end state, exactly-once and drain pass.

## Findings

1. **Group commit (`Store:BatchCommits`) is the biggest Postgres win.** Commits from all workers execute many
   per transaction. Kafka + PG reached 7.3 commits per transaction and used 3.9 Postgres cores at 24k. It
   also cut blocking 50× (84k → 1.6k violations), because commits finish sooner and slots free up faster.
2. **Kafka + Postgres + group commit is the first combination that meets 24k with near-zero blocking**, with
   all state in Postgres. Postgres needs about **0.16 cores per 1k ev/s** for the store path.
3. **NATS falls behind with a Postgres store.** It stays at about 4 cores. The suspect is `MaxAckPending` (20
   per partition consumer): slow events and their parked followers hold ack credits, so a partition stalls.
   It still needs tuning.
4. **PGMQ as the queue costs about 9 Postgres cores at 24k, even behind the dispatcher.** Dispatcher B
   removes polling (595 of 601 receives wait in memory) and batches loads and commits (13.9 per
   transaction), but Postgres still pins at 12.7 cores. The remaining cost is PGMQ's table per queue:
   - Producers insert 1.36 messages per statement into 500 different tables, each with 3 indexes.
   - Each message is updated once on read (vt) and deleted once.

   Next step: a dispatcher-native queue (one table, inserts batched across groups, heads of many groups per
   query, deletes inside the group commit).
5. **PGMQ alone stays pinned at about 11k.**

## Matrix 8: PGMQ tuning at 12.9k (behind the proxy; relative comparison only)

| variant | applied/s | e2e p50 | PG cores | WAL MB/s | violations |
|---|---:|---:|---:|---:|---:|
| P0 baseline (pipelined, read_grouped_head) | 8,427 | 11.2 s | 15.9 | 89 | 209k |
| P1 eb_read_head | 9,557 | 12.1 s | 15.1 | 107 | 287k |
| P2 + no vt index, fillfactor 70, aggressive vacuum | 9,822 | 10.5 s | 14.2 | 100 | 279k |
| P3 + linger | 8,209 | 16.9 s | 14.4 | 98 | 46k (77% linger hits) |
| P4 + unlogged queues | 10,715 | 6.5 s | 14.9 | 34 | 258k |
| P5 batch worker | 12,728 | 1.3 s | 14.3 | 114 | 190k |

`pg_stat_statements` breaks the Postgres time down as:
- `send_batch`: 310k calls for 423k messages, 1.6 ms each under load.
- The commit function: 1.1 ms.
- The read: 0.8 ms.
- On an idle server, a `send_batch` call costs about 30 µs, so most of the time under load is contention.
