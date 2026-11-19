# Matrix 11 — cluster round 2: Redis Streams + Redis as a fail-safe cluster; failover drills; tuned Kafka cluster

**Topology:** Redis 8 with one primary and two replicas (async replication), plus 3 Sentinels (quorum 2,
`down-after` 5 s), from `infra/redis-ha`.
- Every node runs AOF `everysec`.
- `min-replicas-to-write 1`.
- Clients find the primary through Sentinel (StackExchange.Redis, `serviceName=ebmaster`).
- `WaitReplicas=1` sends `WAIT 1` after every commit script and every publish batch, so an acknowledged
  write is on at least one replica.

**Workload:** the same as matrix-9/10: 24,033 ev/s offered, 0.5% slow events of 1–3 s, pipelined worker,
host network, one host. Drills kill the current primary (`infra/redis-ha/failover.sh`) 30 s into the run
phase; Sentinel promotes a replica.

| run | applied/s | e2e p50 / p99 | commit p50 / p99 | errors | duplicates | correctness failures (besides blocking) | Redis cores |
|---|---:|---|---|---:|---:|---|---:|
| single Redis, from matrix-9 | 24,129 | 23 ms / — | 1.9 / — ms | 0 | 0 | none | 3.8 |
| C2a primary + 2 replicas, async | 20,905 | 4.1 s / 21.3 s | 21 / 61 ms | 0 | 0 | none | 6.8 |
| C2b + `WAIT 1` per write | **3,742** | 67.6 s / 97.1 s | 7 / 791 ms | 0 | 0 | none | 3.7 |
| C2c async, **primary killed** | 2,827 | 8.9 s / 136.7 s | | 3,790 | 55,162 | **3,450 ordering gaps; 194 events applied twice (791,423 applied vs 791,229 generated); 5 messages stranded** | 5.1 |
| C2d `WAIT 1`, **primary killed** | 220 | 161.7 s / 174.5 s | | 7,103 | 5,980 | **974 events lost (785,639 applied vs 786,613 generated); 12 ordering gaps** | 2.4 |
| C1e Kafka 3× + PG + standby, linger 10 ms + lz4 | 16,895 | 16.6 s / 23.0 s | 8.9 / 150 ms | 0 | 0 | none | Kafka 7.9 |

## Findings

1. **Redis loses or duplicates acknowledged work on failover.**
   - **Async replication:** writes the old primary had acknowledged were missing on the promoted replica.
     The broker's stream and the store's cursors rolled back to different points, so events were re-applied
     (194 applied twice) and 3,450 arrived out of order.
   - **With `WAIT 1`:** 974 events vanished. WAIT guarantees that *one* replica has the write; Sentinel
     may then promote the *other* one.

   This is Redis's documented behaviour: Sentinel and asynchronous replication do not guarantee acknowledged
   writes survive. It disqualifies Redis as the single source of truth for this system, however fast it is
   on one node.
2. **Recovery is slow too.** After the failover, throughput collapsed to a trickle for the rest of the run
   (e2e p99 137–175 s), because consumers had to work through reset stream state and pending entries.
3. **Replication costs Redis's single main thread.** Async replicas take the cluster from 24.1k to 20.9k
   (commit p50 1.9 → 21 ms). `WAIT` after every write serialises the multiplexed connections and drops it
   to 3.7k. Dedicated connections or per-batch `WAIT` would help throughput, but not the failover
   guarantee.
4. **Kafka producer tuning (linger 10 ms, lz4) does not help the 3-node Kafka cluster** (16.9k vs 17.6k).
   - Publish calls stay at about 900 ms p50 (7 ms on a single node); producers fall 15 s behind.
   - The latency is in `acks=all` replication: 780 partitions × 3 replicas on a host at 76–80% CPU.
   - Broker-side replication tuning (`replica.fetch.wait.max.ms`, fetchers) and real separate machines are
     the open items.

## Ranking after two cluster rounds (correctness under failure first)

1. **Kafka (3 brokers, RF 3, min ISR 2) + Postgres (primary + standby) with group commit.** Correct under a
   broker kill. 16.5–18.8k on this shared host, where Kafka is the cost: about 8 cores, against about 3.5 for
   Postgres.
2. Redis Streams + Redis: **out**. It loses or duplicates acknowledged work on failover.
3. Next to cluster: NATS JetStream (3 nodes, R3) + Postgres; and a Postgres-only dispatcher queue
   (a single table, so a Postgres-only platform needs no broker cluster at all).
