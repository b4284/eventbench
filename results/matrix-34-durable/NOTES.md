# matrix-34: local durability (`synchronous_commit=on`) for Octopus and at the ceiling (B12)

The lab compares at best effort (`synchronous_commit=off`: commits return before the WAL fsync). Production would
run crash-durable commits. `infra/pg-sync-commit.sh on|off` switches the bench database (Octopus's tables and the
benchmark store live there); WSL2 on the laptop's NVMe, `wal_sync_method=fdatasync`.

## Octopus alone, local 16k (S*; as matrix-31 Lb-pg)

| | e2e p50 | e2e p99 | publish p50 | Postgres CPU |
|---|---:|---:|---:|---:|
| durable, Sa / Sb | 8.5 / 8.4 ms | 1.29 / 1.30 s | 6.9 / 6.8 ms | 2.4 / 2.1 cores |
| best effort (Lb-pg) | 7.1 ms | 1.29 s | 5.8 ms | 2.1 cores |

Sampled waits: WALWrite and WalSync behind publish, next to CPU. About 1 ms more per publish; no throughput cost.

## The ceiling: Octopus and the store's writes, NATS, laptop + m4mini (D*; as matrix-32 T*)

| offered | durable produced / applied | best effort produced / applied |
|---|---:|---:|
| 24k | 24.5k / 23.2k | 23.6k / 23.6k (T24-nats) |
| 28k | 26.3k / 23.3k | 25.8k / 24.7k (T28-nats); 22.8k / 21.1k (P28-nats, the same setup) |

Postgres ~9.7 cores, laptop 81% either way. Against the best-effort repeats (matrix-32 V28: NATS 24.3k ±0.4k
applied at 28k), durable commits cost ~4% at the ceiling (23.3k), and ~2% at 24k. Group commit absorbs the fsyncs: many sessions commit per WAL flush.

**Verdict:** local durability is affordable for Octopus: ~1 ms per publish, a few percent at the ceiling. The
remaining cost of production durability is the synchronous standby (`remote_write`, measured on the Patroni pair,
matrix-18/19).
