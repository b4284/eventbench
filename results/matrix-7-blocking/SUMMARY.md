# Matrix 7 — cross-source blocking rule, batch vs pipelined worker, 12.9k and 24k ev/s

**New rule** (`runtime_cross_source_blocking`): an event must not wait more than 100 ms after it is ready while
a worker slot of its group is idle. Slots are counted across all of the group's workers.
- **Ready** means its publish call returned and the source's previous event is done.
- The README section "Cross-source blocking" has the exact definition.

**Workload:** `configs/pgmq-500groups.json` plus `configs/overlay-slow.json`:
- 0.5% of events, and every large event, make a slow external call of 1–3 s.
- 4 slots per worker.
- Workers are sized so that slow calls fill at most half the slots (601 workers at 12.9k, 780 at 24k).
- `Workload:RateMultiplier=1.87` gives 24,033 ev/s. Large groups keep 1–5 ev/min.

**Durability:** best effort, the same settings as matrix-5 and matrix-6. One rep per combination.

**A note on e2e p99:** it now includes the 1–3 s slow calls themselves, and the same-source events queued
behind them. Compare p50 and the blocking columns instead.

## Results

| combo | applied/s | e2e p50 | ready→start p50 / p99 | blocking violations | blocked max | CPU: pg / pgb / kafka / rabbit / nats / mssql / redis / bench |
|---|---:|---:|---|---:|---:|---|
| batch-01-pgmq_pg-txack | 12,314 | 1.1 s | — | 275,779 | 14.2 s | 11.8 / 1.0 / … |
| batch-02-sqlq_mssql-txack | 8,526 | 19.3 s | — | 386,966 | 30.8 s | mssql 16.3 |
| batch-03-redis_redis-txack | 12,825 | 22 ms | — | 73,271 | 8.2 s | redis 2.2 |
| batch-04-nats_pg | 12,825 | 7 ms | — | 74,089 | 8.2 s | |
| batch-05-kafka_pg | 12,825 | 31 ms | — | 74,073 | 8.2 s | |
| batch-06-rabbit_pg | 12,894 | 28 ms | — | 104,513 | 9.0 s | |
| pipe12k-01-pgmq_pg-txack | **9,407** | 13.9 s | 295 ms / 7.7 s | 412,120 | 30.9 s | 15.4 / 1.0 / 0 / 0.2 / 0 / 0 / 0 / 2.3 |
| pipe12k-02-sqlq_mssql-txack | **4,638** | 41.7 s | | 227,049 | 74.0 s | mssql 14.7 |
| pipe12k-03-redis_redis-txack | 12,854 | 12 ms | | **292** | 2.4 s | redis 2.2, bench 4.1 |
| pipe12k-04-nats_pg | 12,885 | 9 ms | | 1,901 | 2.5 s | 2.9 / 0.6 / nats 4.0 / bench 5.0 |
| pipe12k-05-kafka_pg | 12,855 | 19 ms | | 6,758 | 2.5 s | 3.1 / 0.6 / kafka 2.8 / bench 6.3 |
| pipe12k-06-rabbit_pg | 12,593 | 36 ms | | 106,736 | 2.7 s | 3.0 / 0.5 / rabbit 5.0 |
| pipe12k-07-nats_redis | 12,856 | 4 ms | | **105** | 2.5 s | nats 4.1 / redis 1.5 / bench 5.9 |
| pipe24k-01-pgmq_pg-txack | **5,997** | 99 s | | 884,945 | 92.9 s | 16.9 / 0.8 |
| pipe24k-03-redis_redis-txack | **24,164** | 24 ms | 6 ms / 337 ms | 40,984 | 1.6 s | redis 3.9 (main thread 0.85) / bench 4.2 |
| pipe24k-04-nats_pg | 19,117 | 10.7 s | 169 ms / 4.1 s | 767,783 | 21.8 s | 4.5 / **0.8** / nats 4.0 / bench 5.0 |
| pipe24k-05-kafka_pg | 18,902 | 12.7 s | | 133,534 | 2.8 s | 5.0 / **0.9** / kafka 2.8 / bench 6.5 |
| pipe24k-06-rabbit_pg | 15,939 | 21.7 s | | 107,581 | 5.2 s | 4.2 / 0.8 / rabbit 6.1 |
| pipe24k-07-nats_redis | **24,270** | 125 ms | 49 ms / 740 ms | 163,658 | 5.3 s | nats 4.4 / redis 2.1 / bench 5.1 |

Every run fails only `runtime_cross_source_blocking`. Ordering, end state, exactly-once and drain pass everywhere.

## Findings

1. **The batch worker breaks the rule on every broker.** A worker finishes its whole batch before fetching
   again, so one 1–3 s event idles its other slots and holds up every source behind it. The result is
   74k–387k violations per run, with blocked times up to 8–31 s.
2. **The pipelined worker removes almost all of it on partitioned brokers.** At 12.9k:
   - Redis Streams: 73k → 292 violations.
   - NATS + Redis: 105.
   - NATS + PG: 1.9k.
   - Kafka: 6.8k.

   The rest is structural. A source is pinned to one worker, so when that worker's 4 slots are all busy
   with slow calls, its other sources wait (up to one slow call, about 2.5 s) while other workers have
   free slots. More slots per worker should close this; that is the next test.
3. **RabbitMQ stays at about 107k** even when pipelined. Parked events still hold prefetch credits (40 per
   channel), so a slow source's backlog fills the window.
4. **PGMQ gets worse when pipelined: 9.4k/s at 12.9k, and 6.0k/s at 24k.** `pgmq.read_grouped_head`
   computes `MIN(msg_id) GROUP BY group` over the **whole queue table** on every call. Pipelined workers
   read only as many messages as they have free slots (1–4), so there are more calls, each costing time
   proportional to the backlog. Postgres pins at 15–17 cores and the backlog spirals. The fixes, still
   to be measured, are a head-only read with an index probe (`FifoReadFunction=eb_read_head`) and
   `Worker:Linger`.
5. **At 24k, only the Redis-store combinations keep up.**
   - Redis Streams + Redis: 24.2k/s on 3.9 Redis cores, main thread at 0.85 of a core, so near its
     single-thread ceiling.
   - NATS + Redis: 24.3k/s.
   - The Postgres-store combinations stall at 16–19k. The cause is the **single PgBouncer process**
     (0.8–0.9 core, single-threaded). Postgres itself runs only 4.5–5 cores with 3.5 active
     connections. Several PgBouncer processes (`infra/pgbouncer-scale.sh`) is the fix to test.
6. **The SQL Server table queue is out:** 4.6k/s when pipelined.

## Ranking under the rules so far (pipelined)

1. **Redis Streams + Redis**: the only combination that keeps up at 24k with low blocking, on 3.9 cores.
   But Redis's single main thread is near its ceiling, and the state is not SQL-queryable.
2. **NATS + Redis**: keeps up at 24k, but with 4× more violations and 5× higher p50.
3. NATS + PG and Kafka + PG: limited by PgBouncer at 24k. To be re-run with scaled PgBouncer.
4. RabbitMQ + PG.
5. PGMQ + PG: to be re-run with `eb_read_head` and linger.
