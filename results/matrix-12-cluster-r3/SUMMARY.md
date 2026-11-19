# Matrix 12 — cluster round 3: NATS JetStream as a 3-node R3 cluster (+ Postgres primary/standby, group commit)

**Topology:** `infra/nats-cluster`, 3 nats-server 2.12 nodes on host network, default `sync_interval`.
- Streams use `Broker:Replicas=3`: one Raft group per stream, 500 streams.
- `MaxAckPending` is 1000.
- Postgres runs as primary plus async standby, with group commit.

**Workload:** 24,033 ev/s offered, slow-event overlay, pipelined worker, one host.

| run | applied/s | produced/s | e2e p50 / p99 | publish p50 / p99 | errors / dups | NATS cores | correctness (besides blocking) |
|---|---:|---:|---|---|---|---:|---|
| single NATS + PG, MaxAckPending 20 (matrix-9) | 23,001 | | 1.4 s / — | | 0 / 0 | 4.2 | passes |
| C3b single NATS + PG, MaxAckPending 1000 | 21,817 | | 3.3 s / 16.2 s | 200 ms / 650 ms | 0 / 0 | 4.4 | passes |
| C3a 3× NATS R3 + PG + standby | 5,820 | 6,674 | 39.3 s / 55.0 s | **2.0 s** / 4.2 s | 0 / 0 | **11.3** | passes |
| C3c = C3a, **node 2 killed** 30 s into the run | 3,184 | 2,950 | 22.1 s / 60.3 s | 2.1 s / **31.9 s** | 0 / 861 | 7.0 | **passes** |

## Findings

1. **NATS R3 is fail-safe.** A node killed mid-run caused publish timeouts, so producers retried. Retries came
   back as duplicates through `Nats-Msg-Id`, which is correct: the message is stored once. Nothing was lost,
   applied twice or reordered.
   - Two client fixes were needed: a duplicate PubAck now counts as success, and JetStream setup calls retry
     while a new cluster elects leaders.
2. **R3 throughput collapses on one host: 5.8k/s.**
   - Each of the 500 streams is its own Raft group replicating to the other two servers.
   - The cluster uses 11.3 cores, and PubAcks take 2 s p50.
   - The publisher keeps per-source order by publishing each subject's events strictly in sequence and
     awaiting each PubAck, so 2 s per ack caps producers at about 6.7k/s.
   - Pipelined publishing with `Nats-Expected-Last-Subject-Sequence`, and fewer, larger streams, are the
     open tuning items.
3. **Raising `MaxAckPending` does not help single-node NATS.** It stays at about 22k, limited by publish
   latency (200 ms p50).

## Ranking after three cluster rounds (correctness under failure first)

1. Kafka (3 brokers) + Postgres (primary + standby), group commit: correct under failure, 16.5–18.8k on this
   host.
2. NATS JetStream R3 + Postgres: correct under failure, 3–6k on this host.
3. Redis Sentinel: out (loses or duplicates acknowledged work on failover).
