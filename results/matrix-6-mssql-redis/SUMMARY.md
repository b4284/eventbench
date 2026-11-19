# Matrix 6 — SQL Server and Redis (best-effort durability)

The workload is the same as in matrix-5: `configs/pgmq-500groups.json`.
- 500 groups and 16k sources, with an offered load of about 12,856 ev/s.
- 4 groups send large messages, inline in the broker, at 1–5 ev/min.
- Each combination ran 60 s plus drain, 3 times. Every value is the median of the 3 reps.
- The table comes from `python3 scripts/cross-table.py results/matrix-6-mssql-redis`.
- All 42 runs passed the oracle: final state matched, and no ordering gaps or lost events.

## Durability (new components)

| component | setting | single-node loss window on crash |
|---|---|---|
| SQL Server 2025 (table queue + store) | `DELAYED_DURABILITY = FORCED`, SIMPLE recovery, RCSI | log blocks not yet flushed. A flush happens when the 60 KB log buffer fills or about every 1 s, so the loss is under 1 s of commits. Never corruption |
| Redis 8 (Streams + store) | AOF `appendfsync everysec`, no RDB snapshots | about 1 s of writes on OS or power loss. None on a process crash. A Lua commit (state + XACK) is logged as one unit |
| others | as in matrix-5: PG `synchronous_commit=off`, Kafka OS flush, NATS default sync, RabbitMQ quorum queues | see `../matrix-5-besteffort/SUMMARY.md` |

Other environment settings:
- SQL Server memory limit: 8 GB. At 4 GB its buffer pool thrashed, with 50M physical reads and 7.9k/s.
- MongoDB was stopped to free memory.

## Results

| combo | oracle | produced/s | applied/s | e2e p50 | e2e p99 | e2e p99.9 | large p50 | large p99 | drain s | CPU cores: pg / pgb / mongo / kafka / rabbit / nats / mssql / redis / bench | host % |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---:|
| 01-sqlq_mssql-txack | PPP | 11,731 | 10,548 | 4.7s | 48.7s | 58.5s | 4.3s | 12.3s | 59 | 0.0 / 0.0 / - / 0.0 / 0.2 / 0.1 / 16.5 / 0.0 / 2.5 | 86 |
| 02-sqlq_mssql | PPP | 12,870 | 8,210 | 15.1s | 50.9s | 67.8s | 9.9s | 21.7s | 81 | 0.0 / 0.0 / - / 0.0 / 0.2 / 0.0 / 13.2 / 0.0 / 2.5 | 72 |
| 03-redis_redis-txack | PPP | 12,852 | 12,853 | 11 | 55 | 74 | 43 | 92 | 1 | 0.0 / 0.0 / - / 0.0 / 0.1 / 0.0 / 0.0 / 2.1 / 4.4 | 13 |
| 04-redis_redis | PPP | 12,852 | 12,853 | 11 | 59 | 100 | 45 | 97 | 1 | 0.0 / 0.0 / - / 0.0 / 0.1 / 0.0 / 0.0 / 2.2 / 4.6 | 16 |
| 05-redis_pg | PPP | 12,852 | 12,853 | 11 | 54 | 65 | 53 | 117 | 2 | 2.3 / 0.5 / - / 0.0 / 0.1 / 0.0 / 0.0 / 1.6 / 5.4 | 34 |
| 06-redis_mssql | PPP | 12,852 | 12,852 | 11 | 55 | 65 | 58 | 116 | 1 | 0.0 / 0.0 / - / 0.0 / 0.1 / 0.0 / 5.3 / 1.6 / 5.2 | 50 |
| 07-rabbit_mssql | PPP | 13,131 | 13,350 | 21 | 1.4s | 2.1s | 71 | 1.2s | 5 | 0.0 / 0.0 / - / 0.0 / 5.2 / 0.0 / 5.9 / 0.0 / 2.7 | 61 |
| 08-kafka_mssql | PPP | 12,865 | 13,053 | 27 | 531 | 1.2s | 90 | 638 | 1 | 0.0 / 0.0 / - / 2.6 / 0.2 / 0.0 / 6.2 / 0.0 / 6.2 | 69 |
| 09-nats_mssql | PPP | 12,857 | 12,857 | 20 | 82 | 121 | 60 | 153 | 0 | 0.0 / 0.0 / - / 0.0 / 0.1 / 3.9 / 6.0 / 0.0 / 3.9 | 64 |
| 10-rabbit_redis | PPP | 12,852 | 12,852 | 9 | 221 | 321 | 39 | 194 | 4 | 0.0 / 0.0 / - / 0.0 / 5.6 / 0.0 / 0.0 / 1.2 / 2.2 | 39 |
| 11-kafka_redis | PPP | 12,855 | 12,856 | 13 | 56 | 69 | 52 | 95 | 0 | 0.0 / 0.0 / - / 3.0 / 0.1 / 0.1 / 0.0 / 1.3 / 6.3 | 47 |
| 12-nats_redis | PPP | 12,853 | 12,853 | 4 | 26 | 44 | 31 | 80 | 0 | 0.0 / 0.0 / - / 0.0 / 0.1 / 4.2 / 0.0 / 1.5 / 5.8 | 51 |
| 13-nats_pg | PPP | 12,855 | 12,855 | 8 | 34 | 50 | 42 | 107 | 1 | 2.6 / 0.6 / - / 0.0 / 0.1 / 4.2 / 0.0 / 0.0 / 4.8 | 57 |
| 14-kafka_pg | PPP | 12,854 | 12,854 | 18 | 63 | 82 | 64 | 123 | 1 | 3.0 / 0.6 / - / 2.9 / 0.2 / 0.0 / 0.0 / 0.0 / 6.2 | 59 |

Latencies are in ms unless marked `s`. CPU is container cores from cpu_time deltas. Where `applied/s` is above `produced/s`, the median rep had a short backlog that drained after the producer stopped.

Rows 13 and 14 are the matrix-5 reference pairs. Their matrix-5 values were 7/33 ms and 17/61 ms, so run-to-run noise is about ±1 ms at p50.

## Ranking

The ranking is by e2e p99, among combinations that keep up with the offered load.

| # | combo | e2e p50 / p99 / p99.9 | CPU cores (containers + bench) |
|---|---|---|---|
| 1 | **NATS JetStream + Redis** | 4 / 26 / 44 ms | ~11.6 |
| 2 | NATS JetStream + Postgres | 8 / 34 / 50 ms | ~12.3 |
| 3 | Redis Streams + Postgres | 11 / 54 / 65 ms | ~9.9 |
| 4 | **Redis Streams + Redis (txack)** | 11 / 55 / 74 ms | **~6.6** |
| 5 | Redis Streams + SQL Server | 11 / 55 / 65 ms | ~12.2 |
| 6 | Kafka + Redis | 13 / 56 / 69 ms | ~10.8 |
| 7 | Redis Streams + Redis (ack after commit) | 11 / 59 / 100 ms | ~6.9 |
| 8 | Kafka + Postgres | 18 / 63 / 82 ms | ~12.9 |
| 9 | NATS JetStream + SQL Server | 20 / 82 / 121 ms | ~13.9 |
| 10 | RabbitMQ + Redis | 9 / 221 / 321 ms | ~9.0 |
| 11 | Kafka + SQL Server | 27 / 531 ms / 1.2 s | ~15.2 |
| 12 | RabbitMQ + SQL Server | 21 ms / 1.4 s / 2.1 s | ~13.8 |
| — | SQL queue + SQL Server (txack) | 4.7 s / 49 s, **10.5k/s, falls behind** | ~19.3 |
| — | SQL queue + SQL Server (ack after commit) | 15 s / 51 s, **8.2k/s, falls behind** | ~15.9 |

From matrix-5, for comparison: PGMQ + Postgres (txack) reached 11.6k/s, with p50 3.7 s and p99 20 s, on ~17.9 cores.

## Takeaways

1. **Redis is the cheapest store, and Redis Streams is the cheapest broker.**
   - Redis Streams + Redis carries the full load on ~2.1 Redis cores. Host CPU is 13%, against 50–60% for the other combinations that keep up.
   - Redis's single main thread sat at ~0.65 core of user + sys time while serving ~230k commands/s. Most of those commands are workers polling streams.
   - Having broker and store on one server also gives the same in-transaction ack PGMQ gives you: the commit Lua script XACKs the entry. This came out slightly better at the tail than acking after commit (p99.9 74 vs 100 ms).
2. **The lowest latency is NATS JetStream + Redis, at 4 / 26 ms.** That is about half the latency of NATS + Postgres.
   - The Redis store needs ~1.5 cores, against ~3.2 for Postgres + PgBouncer.
   - The trade-off is durability. Redis keeps its data in memory with an AOF flushed once a second, and it has no MVCC or SQL to query state.
3. **SQL Server works well as a store, but not as a queue.**
   - As a store behind a proper broker it keeps up: 11–27 ms p50.
   - It uses ~5–6 cores, about twice what Postgres needs for the same commits.
   - The SQL table queue behaves like PGMQ, only worse: 10.5k/s with txack and 8.2k/s with ack after commit.
     - It pins ~16 cores and spends 0.4–1.8 s per second waiting on exclusive page latches.
     - The waits are on the hot last page of `eb_q`'s clustered index and on the `eb_source` cursor pages: 600 polling workers race inserts and deletes for the same pages.
     - WRITELOG waits stay around 290 ms/s even with delayed durability, because the 60 KB log buffer fills and flushes constantly.
   - The likely fix is In-Memory OLTP (latch-free, SCHEMA_ONLY or SCHEMA_AND_DATA tables), which needs a different queue design.
4. **Queues built on tables are the bottleneck, whichever database.** PGMQ on Postgres and the SQL queue on SQL Server both fall behind. Any streaming broker in front of the same database keeps up, at a fraction of the database CPU:
   - Postgres: 2.3 cores (Redis Streams in front) vs 13.7 cores (PGMQ).
   - SQL Server: 5.3 cores vs 16.5 cores.
5. **RabbitMQ still has the worst p99 among brokers that keep up**: 221 ms to 1.4 s, depending on how fast the store acks. With SQL Server's slower commits, RabbitMQ's tail grows the most.

## Redis Streams design (summary)

The full description is in the README section "Redis (Streams broker and store)".

- **Partitions:** each group has one stream per worker, and a source always hashes to the same stream. One consumer reads each stream through `XREADGROUP`, which keeps per-source order.
- **Retries:** a nack pauses the stream. After the retry delay, the consumer re-reads its pending entries with ID `0` before taking new ones with `>`.
- **Duplicates:** the store's cursor check turns any re-delivery into a duplicate, which is acked.
- **Chaos test:** with 1% of events failed on purpose, there were about 4.6k nacks per run, zero ordering gaps, and the oracle passed.
- **Ack:** `XACK + XDEL`, so `XLEN` equals the backlog.
- **Limits:** single node only, because a Redis Cluster would need hash tags so that cursor and stream keys share a slot. There is no XAUTOCLAIM-based takeover of a crashed consumer's pending entries.
