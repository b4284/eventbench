# matrix-26: Octopus on YugabyteDB (B17)

YugabyteDB 2026.1.3.0-b100 (PostgreSQL 15.12 compatible), one node in podman (`eb-yb`, YSQL on 5433, RF 1,
advisory locks enabled with `ysql_yb_enable_advisory_locks=true`). Workers commit to the benchmark's in-memory store,
so the database carries only Octopus (as in matrix-25). 16k ev/s, 3 dispatchers, Octopus `c3f960b`.

## Compatibility: Octopus runs unchanged

The schema loads as is. YugabyteDB ignores the heap storage parameters (fillfactor, autovacuum_*) with a warning.
**All 23 Octopus tests pass** once two things retry serialization failures (40001): the schema migration and the
tests' hook setup. Concurrent DDL on YugabyteDB fails where Postgres would wait, and a migrator that waited for the
advisory lock can still hold an older snapshot. Pushed as Octopus `c3f960b`, harmless on Postgres. The tests take
~5× longer.

## Performance: 25–45× Postgres's CPU per event

| run | applied/s | e2e p50 | database CPU | CPU per event |
|---|---:|---:|---:|---:|
| Y1 YugabyteDB, defaults | 2,214 | 45.7 s | 15.3 cores | ~6.9 ms |
| Y2 + `yb_enable_cbo = off` | 1,742 | 42.0 s | 11.6 cores | ~6.7 ms |
| Y3 + colocated database (`colocation = true`) | 3,012 | 43.2 s | 11.6 cores | ~3.9 ms |
| R1 PostgreSQL 18, same run | 15,945 | 8.5 ms | 2.5 cores | ~0.15 ms |

Nothing was lost in any run (the cross-source blocking check failed only because of the backlog).

What makes it slow:
1. **The cost-based optimizer** (on by default in 2026.1) takes the event table's statistics, usually "empty" for a
   queue table, at face value. It joined key lookups to the event table on (epoch, seq) without the source id, a
   scan of the whole index per lookup (2.1 s per call under load). `yb_enable_cbo = off` restores per-key
   lookups. A barrier (`OFFSET 0`) or a rewrite didn't help.
2. **Tombstones:** LSM storage keeps a marker for every deleted row until compaction. After a run, a
   sequential scan of the *empty* event table took 57 ms. Octopus's event table is insert-then-delete at the
   event rate, the worst case for that.
3. **Distributed transactions:** every multi-row write that spans tablets (publish, cursor writes, deletes) is a
   distributed transaction, with provisional records, a status tablet, and an apply step. Publish took 27–29 ms per
   call (~7 events). Colocating all tables in one tablet makes transactions single-shard: ~1.7× better, still ~25×
   Postgres.

Verdict: on this workload, distributed SQL costs far more CPU per event than the single-writer Postgres +
Patroni design. At 24k ev/s, one node would need ~90 cores before replication; RF 3 adds Raft on every write.
What it would offer is fast automatic failover (Raft, seconds) and no Patroni/etcd/HAProxy to run.

**Not explored yet:**
- 3 nodes at RF 3, and node-loss drills: failover time vs Patroni's ~20–25 s;
- compaction tuning for the tombstones;
- `pg_hint_plan` hints instead of turning off the cost-based optimizer;
- bigger publish batches;
- the application's own writes on YugabyteDB;
- CockroachDB (similar design, likely similar costs).
