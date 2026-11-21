# Results index

One folder per experiment; each has a NOTES.md (or SUMMARY.md) with the setup, the runs and the verdict. Backlog
items (B*) are in ../BACKLOG.md. Newest first.

## Octopus: limits, engines and production questions (2026-11-22)

| matrix | question | headline |
|---|---|---|
| [36](matrix-36-nats-ha/NOTES.md) | 3-node NATS cluster, broker-node kills (B31) | node every client used killed: no outage, all correct; tail = 30 s delivery timeout |
| [35](matrix-35-nlb/NOTES.md) | network load balancer instead of HAProxy (B30) | frozen leader: 83–84 s outage without cutting sessions, 27–30 s with; 5 s checks +~5 s per failover |
| [34](matrix-34-durable/NOTES.md) | `synchronous_commit=on` (B12) | ~1 ms per publish at 16k; ~4% at the ceiling |
| [33](matrix-33-mssql-inmem/NOTES.md) | SQL Server In-Memory OLTP (B33) | steady 16k (4/4), p50 14 ms, ~3.2× Postgres CPU; disk-based erratic 4.7–15.9k |
| [32](matrix-32-nats/NOTES.md) | NATS instead of Mosquitto (B32) | +4.7% applied at the ceiling (24.3k vs 23.2k, 4 pairs); best 25.5k ev/s; m1mini link at 100 Mbit |
| [31](matrix-31-mysql/NOTES.md) | Octopus on MySQL 8.4 (B16) | correct after 4 fixes (one: acked-but-lost, all backends); ~2.2× Postgres CPU; binary log = latency |
| [30](matrix-30-emqx/NOTES.md) | EMQX instead of Mosquitto (B31) | no gain on the shared laptop |
| [29](matrix-29-limit/NOTES.md) | the ceiling with the load split | ~22–23.5k ev/s; Mosquitto's one thread first, then Postgres |
| [28](matrix-28-shards/NOTES.md) | producers/workers on the Macs | sharded runs (scripts/shard-run.sh) |
| [27](matrix-27-sqlserver/NOTES.md) | Octopus on SQL Server 2025 (B16) | correct; ~4–5× Postgres CPU; not steady at 16k |

## Earlier

- [matrix-26-yugabyte](matrix-26-yugabyte/NOTES.md): Octopus on YugabyteDB (B17)
- [matrix-25-dispatcher-peak](matrix-25-dispatcher-peak/NOTES.md): peak capacity of one dispatcher (B21)
- [matrix-24-b25](matrix-24-b25/NOTES.md): B25, Octopus's own database cost
- [matrix-23-geo](matrix-23-geo/NOTES.md): three-site cluster, cost of synchronous standbys (B5, B20)
- [matrix-22-partition](matrix-22-partition/NOTES.md): a real network partition between the DCs (B27)
- matrix-21-worker-crash (no notes)
- matrix-20-db-cost (no notes)
- [matrix-19-patroni-2x2](matrix-19-patroni-2x2/SUMMARY.md): the 2 + 2 layout (B28)
- [matrix-18-patroni-drills](matrix-18-patroni-drills/SUMMARY.md): drills on a Patroni pair (B6, B27)
- [matrix-17-two-machines](matrix-17-two-machines/SUMMARY.md): two machines
- [matrix-16-octopus-drills](matrix-16-octopus-drills/SUMMARY.md): Octopus failure drills
- matrix-15-octopus (no notes)
- [matrix-14-bakeoff](matrix-14-bakeoff/SUMMARY.md): bake-off of existing products, with one-event publishes (backlog B0, B2)
- [matrix-13-pgqueue](matrix-13-pgqueue/SUMMARY.md): pgqueue: Postgres-only platform (dispatcher-native queue) at 24k, with HA and failover drills
- [matrix-12-cluster-r3](matrix-12-cluster-r3/SUMMARY.md): cluster round 3: NATS JetStream as a 3-node R3 cluster (+ Postgres primary/standby, group commit)
- [matrix-11-cluster-r2](matrix-11-cluster-r2/SUMMARY.md): cluster round 2: Redis Streams + Redis as a fail-safe cluster; failover drills; tuned Kafka cluster
- [matrix-10-cluster-r1](matrix-10-cluster-r1/SUMMARY.md): cluster round 1: the #1 combination (Kafka + Postgres, group commit) as a fail-safe cluster
- [matrix-9-hostnet-24k](matrix-9-hostnet-24k/SUMMARY.md): 24k ev/s on host networking (and matrix 8: PGMQ tuning)
- matrix-8-pgmq-tuning (no notes)
- [matrix-7-blocking](matrix-7-blocking/SUMMARY.md): cross-source blocking rule, batch vs pipelined worker, 12.9k and 24k ev/s
- [matrix-6-mssql-redis](matrix-6-mssql-redis/SUMMARY.md): SQL Server and Redis (best-effort durability)
- [matrix-5-besteffort](matrix-5-besteffort/SUMMARY.md): best-effort durability (no forced local fsync)
- matrix-4-pgmq-tuning (no notes)
- matrix-3-probe (no notes)
- matrix-3-cross (no notes)
- matrix-2-pool (no notes)
- matrix-1 (no notes)
