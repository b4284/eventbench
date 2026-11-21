# matrix-27: Octopus on SQL Server 2025 (B16)

SQL Server 2025 CU11 (17.0.5065) Developer edition in podman (`infra/mssql`): host network, 8 GB, MAXDOP 1, the
Octopus database recreated per run with READ_COMMITTED_SNAPSHOT, SIMPLE recovery and delayed durable commits (the same
best-effort durability as the Postgres runs). Backend: Octopus `crates/octopus-mssql` (stored procedures; batches as
one JSON parameter). Workers commit to the benchmark's in-memory store, so the database carries Octopus alone.
16k ev/s offered, 3 dispatchers.

## Results

| run | Octopus | produced/s | applied/s | e2e p50 / p99 | DB CPU | notes |
|---|---|---:|---:|---:|---:|---|
| M1 | first port | 14,205 | 14,203 | 4.9 s / 10.5 s | 6.4 cores | lock waits: cursor writes scanned `source` under UPDLOCK, page locks |
| M2 | row locks, forced seeks, deadlock retry | 16,099 | 16,104 | 37 ms / 1.9 s | 9.7 | |
| M3 | bodies in one table variable | 13,431 | 13,436 | 34 ms / 8.4 s | 7.1 | hot tempdb page: `sysallocunits` (table variable metadata) |
| M4 | same as M3 | 15,918 | 15,959 | 26 ms / 1.4 s | n/a | M3 was host noise |
| M5 | + memory-optimized tempdb metadata | 15,962 | 15,963 | 543 ms / 6.1 s | 9.5 | tempdb latches gone; `LCK_M_U` on event keys |
| M6 | + bounded range deletes in settle | 10,196 | 10,202 | 31 ms / 4.5 s | 6.7 | still `LCK_M_U` |
| M7 | same | 15,942 | 15,945 | 23 ms / 1.4 s | n/a | blocking chain sampled: the housekeeping purge vs settle |
| M8a | + lock-light purge | 14,486 | 14,496 | 38 ms / 1.8 s | 9.3 | |
| P8a | PostgreSQL, same run setup | 15,944 | 15,945 | 7 ms / 1.3 s | 2.2 | |
| M8b | same as M8a | 11,598 | 11,610 | 36 ms / 7.7 s | 6.4 | |
| P8b | PostgreSQL | 15,945 | 15,946 | 7 ms / 1.3 s | 2.2 | |

(p99 includes the workload's deliberately slow events, 1–3 s.)

**Verdict so far:** SQL Server runs Octopus correctly (the same 16-test backend suite passes) but uses ~4–5× Postgres's
CPU per event, and doesn't hold 16k steadily on this host. Producers are held back by slower publishes (15–18 ms
per call), and the cursor writes plus deletes take 600–800 ms per call (settle: ~200 ms CPU, ~43k logical reads per
call for ~3k deleted rows). Postgres holds 16k at p50 7 ms on 2.2 cores, run after run.

## What was found and fixed (Octopus 5787c53, f734b21, 4dcaefd)

1. Lock waits (`LCK_M_IX`, `LCK_M_U`, `LCK_M_X`):
   - key lookups under UPDLOCK compiled to scans, so cursor writes locked every row and queued behind each other
     (3 s per call) and deadlocked;
   - deletes took page locks, which blocked inserts.

   Fixed with row locks only, no lock escalation, and seeks driven by a keyed table variable in key order
   (FORCESEEK, LOOP JOIN, FORCE ORDER).
2. Deadlocks between publishes: SQL Server does not guarantee that `INSERT ... SELECT ... ORDER BY` inserts in that
   order. The backend retries deadlock victims; every operation is idempotent or monotonic.
3. tempdb metadata contention (`sysallocunits`): each publish declares several table variables, ~600 calls/s.
   Memory-optimized tempdb metadata (server setting, restart; now in `infra/mssql/up.sh`).
4. `settle` walked a source's newer rows and waited for publishes inserting them: bounded range deletes.
5. The housekeeping purge (a scanning `DELETE TOP`) blocked the cursor writes both ways: a versioned read for the
   candidates, then a delete by key with READPAST.
6. Correctness on the way: names read from JSON as nvarchar and converted through a UTF-8 binary collation (a default
   code page turns non-Latin names into '?'); a zero gap age takes every gap (SYSUTCDATETIME differs by
   milliseconds between sessions).

## Not explored yet

- **Memory-optimized (In-Memory OLTP) tables** for the event table and table types: SQL Server's design for hot
  queue-like tables, with no locks, no latches and no tempdb. Needs a MEMORY_OPTIMIZED_DATA filegroup, and
  natively compiled procedures for full effect. The most promising lever.
- Cheaper batch passing than JSON with base64 bodies: publish's unpacking costs ~2.5 ms CPU per call.
- `settle`'s delete cost (~14 logical reads per deleted row).
- The application's writes on SQL Server too (the benchmark's SqlServerStore), and the availability-group failover
  drill.

## With the load split across machines (2026-11-22)

Same comparison (Octopus alone, workers commit to memory, 16k ev/s, 3 dispatchers), with producers and workers
split into two shards: the laptop and m4mini (`scripts/shard-run.sh`, see matrix-28). Interleaved.

| run | backend | applied/s (total) | e2e p50 laptop / m4mini shard | DB CPU | laptop CPU |
|---|---|---:|---:|---:|---:|
| Xa | SQL Server | 15,614 | 29 ms / 2.6 s | 9.8 cores | 68% |
| Xb | SQL Server | 15,923 | 33 ms / 25 ms | 11.3 cores | 76% |
| Ya | PostgreSQL | 15,907 | 7 ms / 5 ms | 2.6 cores | 41% |
| Yb | PostgreSQL | 15,908 | 7 ms / 5 ms | 2.3 cores | 39% |

Taking the benchmark process off the laptop doesn't change SQL Server's picture: it can hold 16k (Xb), but
inconsistently, at ~4–5x Postgres's CPU per event; `advance`/`settle` still take 560–920 ms per call. Postgres holds
16k at p50 5–7 ms on ~2.5 cores, run after run.

(The first Postgres split attempts failed on the laptop shard's admin connection to Postgres through the laptop's
own LAN address, 192.168.0.217:5432: timeouts, while the Mac shard used the same address fine. Unresolved; the
laptop shard uses 127.0.0.1 now, as in matrix-28.)
