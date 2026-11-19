# Matrix 5: best-effort durability (no forced local fsync)

Workload: `configs/pgmq-500groups.json`. That's 500 groups, 16k sources, ~12,856 ev/s offered, 4 large-upload groups with inline 1–4 MiB payloads, 60 s runs with 10 s warmup, 3 interleaved reps each. Every row passed the oracle on all 3 reps.

Durability per platform:

| component | setting | single-node loss window on crash |
|---|---|---|
| Postgres (PGMQ + store) | `synchronous_commit=off`, set per database | last ≤ 3×`wal_writer_delay` (~600 ms) of commits on a server/OS crash; never corruption |
| MongoDB | `w:majority, j:false`, rs `writeConcernMajorityJournalDefault=false` | up to the journal flush interval (~100 ms) |
| Kafka | OS page-cache flush (no `flush.messages=1`) | process crash: none. OS/power loss: unflushed page cache (replication covers this in production) |
| NATS JetStream (broker + KV) | default `sync_interval` (2 min) | process crash: none. OS/power loss: up to 2 min (replication covers this in production) |
| RabbitMQ | quorum queues (always fsync, no off switch) | none |

Rep 3 of `04-pgmq_natskv` hit a ~67 s whole-host freeze, with host CPU at 0.5% and every process idle. That's a WSL/VM stall, not this combination. The end state still verified, but 66 NATS commits that timed out client-side were counted as duplicates, so the drain counter never closed. The run is kept in `_excluded/` and was re-run.

## Results (medians of 3)

| combo | oracle | produced/s | applied/s | e2e p50 | e2e p99 | e2e p99.9 | large p50 | large p99 | drain s | CPU cores: pg / pgb / mongo / kafka / rabbit / nats / bench | host % | pgb waiting |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---:|---:|
| 01-pgmq_pg-txack | PPP | 12,854 | 11,611 | 3.7s | 20.1s | 21.9s | 426 | 934 | 7 | 13.7 / 0.9 / 0.0 / 0.0 / 0.2 / 0.1 / 3.1 | 80 | 267 |
| 02-pgmq_pg | PPP | 12,856 | 9,269 | 14.0s | 33.4s | 34.4s | 529 | 1.3s | 19 | 11.7 / 1.0 / 0.0 / 0.0 / 0.1 / 0.1 / 2.8 | 71 | 281 |
| 03-pgmq_mongo | PPP | 12,854 | 4,284 | 64.6s | 99.7s | 100.5s | 618 | 2.6s | 96 | 7.7 / 0.5 / 6.4 / 0.0 / 0.2 / 0.0 / 3.8 | 83 | - |
| 04-pgmq_natskv | PPP | 12,854 | 11,192 | 4.8s | 22.4s | 24.4s | 334 | 641 | 11 | 12.8 / 0.8 / 0.0 / 0.0 / 0.2 / 1.4 / 3.7 | 83 | - |
| 05-rabbit_pg | PPP | 12,852 | 12,852 | 14 | 778 | 1.2s | 55 | 443 | 5 | 3.1 / 0.5 / 0.0 / 0.0 / 5.8 / 0.0 / 2.5 | 54 | 13 |
| 06-rabbit_mongo | PPP | 12,827 | 5,672 | 50.2s | 84.1s | 88.0s | 1.6s | 4.1s | 78 | 0.0 / 0.0 / 9.1 / 0.0 / 3.9 / 0.0 / 3.5 | 75 | - |
| 07-rabbit_natskv | PPP | 12,849 | 12,852 | 13 | 211 | 328 | 55 | 226 | 4 | 0.0 / 0.0 / 0.0 / 0.0 / 5.7 / 2.0 / 3.4 | 49 | - |
| 08-kafka_pg | PPP | 12,852 | 12,851 | 17 | 61 | 97 | 59 | 129 | 1 | 3.0 / 0.6 / 0.0 / 2.8 / 0.1 / 0.0 / 6.2 | 57 | 0 |
| 09-kafka_mongo | PPP | 12,862 | 4,942 | 52.9s | 79.4s | 82.9s | 335 | 1.5s | 73 | 0.0 / 0.0 / 7.7 / 2.4 / 0.2 / 0.0 / 6.8 | 78 | - |
| 10-kafka_natskv | PPP | 12,855 | 12,855 | 19 | 90 | 140 | 70 | 206 | 2 | 0.0 / 0.0 / 0.0 / 2.9 / 0.1 / 2.1 / 7.3 | 57 | - |
| 11-nats_pg | PPP | 12,853 | 12,854 | 7 | 33 | 49 | 39 | 84 | 1 | 2.7 / 0.6 / 0.0 / 0.0 / 0.1 / 4.2 / 5.0 | 59 | 0 |
| 12-nats_mongo | PPP | 12,852 | 6,045 | 44.4s | 71.4s | 74.9s | 233 | 741 | 64 | 0.0 / 0.0 / 9.4 / 0.0 / 0.2 / 2.3 / 5.0 | 76 | - |
| 13-nats_natskv | PPP | 12,853 | 12,044 | 424 | 16.2s | 19.3s | 71 | 173 | 5 | 0.0 / 0.0 / 0.0 / 0.0 / 0.1 / 4.3 / 5.5 | 43 | - |
| _excluded | F | 3,850 | 4,028 | 5.7s | 86.3s | 87.8s | 331 | 680 | 600 | 8.6 / 0.6 / 0.0 / 0.0 / 0.1 / 0.9 / 1.3 | 30 | - |

offered load ≈ 12,856 ev/s; medians of 3 reps; latencies ms unless 's'; CPU = container cores (cpu_time deltas)

## Ranking (all keep up with offered load unless noted)

| # | combo | e2e p50 / p99 / p99.9 | CPU cores total (containers + bench) |
|---|---|---|---|
| 1 | NATS JetStream + Postgres | 7 / 33 / 49 ms | ~12.6 |
| 2 | Kafka + Postgres | 17 / 61 / 97 ms | ~12.6 |
| 3 | Kafka + NATS KV | 19 / 90 / 140 ms | ~12.4 |
| 4 | RabbitMQ + NATS KV | 13 / 211 / 328 ms | ~11.2 |
| 5 | RabbitMQ + Postgres | 14 / 778 ms / 1.2 s | ~12.0 |
| 6 | NATS + NATS KV | 424 ms / 16 s, 12.0k/s, *just below offered* | ~9.9 |
| 7 | PGMQ + Postgres (txack) | 3.7 s / 20 s, **11.6k/s, falls behind** | ~17.9 |
| 8 | PGMQ + NATS KV | 4.8 s / 22 s, 11.2k/s, falls behind | ~18.9 |
| 9 | PGMQ + Postgres (ack after commit) | 14 s / 33 s, 9.3k/s | ~15.7 |
| 10–13 | any broker + MongoDB | 44–65 s p50, 4.3–6.0k/s | Mongo pinned at 6.4–9.4 cores |

## Compared with matrix-3 (fsync-durable, `results/matrix-3-cross`)

| combo | fsync-durable applied/s, p50 / p99 | best-effort applied/s, p50 / p99 | change |
|---|---|---|---|
| PGMQ + PG txack | 10,199, 10.1 s / 28 s | 11,611, 3.7 s / 20 s | +14% throughput, still below offered |
| PGMQ + PG after-commit | 7,738, 24 s / 43 s | 9,269, 14 s / 33 s | +20% |
| PGMQ + NATS KV | 10,376, 9.0 s / 28 s | 11,192, 4.8 s / 22 s | +8% |
| RabbitMQ + PG | 12,878, 23 ms / 3.2 s | 12,852, 14 ms / 778 ms | p99 4× better, all from the Postgres side |
| Kafka + PG | (fsync: 3.3k/s) / osflush: 24 / 83 ms | 17 / 61 ms | modest |
| NATS + PG | 11 / 45 ms | 7 / 33 ms | modest |
| any + Mongo | 4.7–6.2k/s | 4.3–6.0k/s | **none**: Mongo is not journal-bound |

Two rows are identical in both matrices: `rabbit_natskv` and `kafka_natskv` (no setting changed). They show the run-to-run noise on tails: p99 116 vs 211 ms, and 83 vs 90 ms.

## Takeaways

- **PGMQ is the bottleneck, not Postgres as a store.** With PGMQ as broker, Postgres burns 12.8–13.7 cores and still can't keep up. As a pure state store behind NATS or Kafka, the same Postgres serves the full 12.9k ev/s on ~3 cores. Queue operations cost roughly 0.8 ms of DB CPU per event: `send_batch` once per event across 500 queues, `read_grouped_head` polling, and delete. Turning fsync off removes WAL-flush waits but none of that CPU.
- **MongoDB tops out at ~6k ev/s** with the transactional commit (cursor CAS + record + upload metadata), even without journaling. This is transaction/WiredTiger contention, not durability.
- **NATS KV as store:** fine behind Kafka or RabbitMQ. Paired with the NATS broker, both share one single-node server (4.3 cores), and the tail grows.
- **Large inline messages:** large-event e2e stays sub-second for every combo that keeps up. The broker side (RabbitMQ/NATS/Kafka) handles 1–4 MiB inline payloads without hurting small-event tails.
