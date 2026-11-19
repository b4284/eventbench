# Matrix 10 — cluster round 1: the #1 combination (Kafka + Postgres, group commit) as a fail-safe cluster

The #1 from matrix-9 is Kafka + Postgres with group commit: 24.0k/s at 24k offered, 27 ms e2e p50 and 1,646
blocking violations on a single Kafka node. Here it is rebuilt in a production-style topology on **one host**
(24 cores, 39 GB):
- **Kafka:** 3 KRaft nodes (`infra/kafka-cluster`), replication factor 3 for topics and internal topics,
  `min.insync.replicas=2`, producers with `acks=all` and idempotence, flush left to the OS.
- **Postgres:** the primary plus a streaming hot standby (`infra/pg-standby`, port 5433, built with
  `pg_basebackup`). It is async by default; `sync.sh on` makes it the synchronous standby with
  `synchronous_commit=remote_write`.

Workload: `pgmq-500groups` + `overlay-slow`, pipelined worker, 24,033 ev/s offered, host networking,
4 PgBouncer processes.

| run | applied/s | e2e p50 / p99 | publish p50 / p99 | blocking violations | Kafka cores | PG cores (standby) | standby lag mean / max |
|---|---:|---|---|---:|---:|---|---|
| single Kafka, from matrix-9 | 24,046 | 27 ms / — | — | 1,646 | 3.6 | 3.9 | — |
| C1a 3× Kafka, PG primary | 17,621 | 15.7 s / 22.3 s | 898 ms / 3.2 s | 365,067 | 8.7 | 3.5 | — |
| C1b 3× Kafka, PG + async standby | 16,469 | 14.5 s / 21.2 s | 938 ms / 1.5 s | 372,451 | 8.1 | 3.3 (0.3) | 1.3 / 3.9 MB |
| C1c 3× Kafka, PG + sync standby (`remote_write`) | 18,777 | 8.8 s / 15.7 s | 837 ms / 1.2 s | 438,507 | 7.9 | 3.3 (0.3) | 0.8 / 2.8 MB |
| C1d = C1b, **broker 2 killed** 30 s into the run | 14,641 | 15.4 s / 23.9 s | 845 ms / **11.2 s** | 316,759 | 7.4 | 2.6 (0.2) | 2.3 / 25.8 MB |

Every run fails only `runtime_cross_source_blocking`. Ordering, end state, exactly-once and drain pass.

## Findings

1. **Fail-safe works.** Killing one of three brokers mid-run caused no errors, no duplicates and no lost or
   reordered events. The oracle verified every source cursor, record and upload. Partition leadership moved
   to the surviving brokers; producers stalled for up to about 11 s (publish p99) and then caught up.
2. **The Postgres standby is nearly free.** It costs 0.3 cores, lags 1–4 MB, and does not slow the primary.
   Synchronous `remote_write` cost nothing measurable either: the commits are already batched.
3. **The 3-node Kafka cluster drops the combination from 24k to about 17.6k on this host.**
   - Kafka uses 8.7 cores instead of 3.6.
   - Producer calls take about 900 ms, so producer lag, not the workers, limits throughput.
   - The host runs at 80% CPU, with three brokers, Postgres, PgBouncer and the benchmark (5.5 cores) all on
     24 cores.

   On three machines each broker would have its own CPU. The number to carry forward is the **cluster cost:
   about 0.5 Kafka cores per 1k ev/s, against about 0.15 on a single node.**
4. **Blocking rises with the backlog.** The violations here come from events waiting behind producer lag, not
   from slow events: slots sit idle because messages arrive late.

Next: Kafka producer tuning (linger 10 ms and lz4 compression cut replication requests) is in matrix 11,
together with the previous #2 (Redis Streams + Redis) as a Sentinel cluster.
