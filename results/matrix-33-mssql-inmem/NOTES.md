# matrix-33: SQL Server with a memory-optimized event table (B33)

The matrix-27/31 local setup: 500 groups, 16k sources, 15.9k ev/s offered, 3 dispatchers, Mosquitto, the in-memory
store (Octopus alone hits the database), 60 s; SQL Server 2025 CU11, delayed durability, READ_COMMITTED_SNAPSHOT.
`OCTOPUSD_ENV=OCTOPUS_MSSQL_INMEMORY=1 OCTOPUS_BACKEND=mssql infra/octopus/up.sh` (Octopus 9251c45 and the seek fix
below). The backend adds the MEMORY_OPTIMIZED_DATA filegroup and MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT itself and
creates `octopus.event` memory-optimized (SCHEMA_AND_DATA); every other table stays on disk.

## Result

| runs | applied ev/s | e2e p50 | e2e p99 | SQL Server CPU | advance / settle per call | publish per call |
|---|---:|---:|---:|---:|---:|---:|
| in-memory, Ie Ig Ih If (after the seek fix) | 15.9–16.1k, 4 of 4 | 13.6–14.7 ms | 1.31–1.39 s | 6.6–7.1 cores | 37–46 / 14–18 ms | 4.5–5.3 ms |
| disk, Ma Mb Me Mf Mg Mh (same code as matrix-27) | 15.9, 15.4, 4.7, 8.5, 9.3, 15.9k | 20–61 ms when holding | 1.35–9.1 s | 8.6–8.9 cores when holding | 300–770 / 250–710 ms | 7.7–70 ms |
| Postgres (matrix-31 Lb-pg) | 15.9k | 7 ms | 1.29 s | 2.1 cores | | |

**Verdict:** the memory-optimized event table makes SQL Server steady at 16k, where disk-based it swings between 5k
and 16k from run to run: the cursor writes' deletes (`advance` + `settle`) get ~15× cheaper (no locks, latches or
ghost rows) and the lock waits behind them are gone. CPU per event drops from ~4–5× Postgres to ~3.2×; latency
p99 matches Postgres, p50 is ~2× (14 vs 7 ms). Postgres stays the engine of choice; SQL Server is now viable for a
site that must run it, given the In-Memory OLTP filegroup (memory to hold the backlog of stored events).

## Found on the way

- Ia, Ic (8.8k, 9.7k): without IGNORE_DUP_KEY the publish skips stored rows itself (`NOT EXISTS`); compiled while
  the table held ~10 rows, that check became a **table scan of the event table on every publish** (8.8 ms CPU per
  call). Ib, compiled later, held 16k: bimodal runs. Fixed with forced seeks on publish's event lookups (in-memory
  variant only; on disk they are seeks anyway). After the fix the insert costs ~0.5 ms CPU per call.
- Memory-optimized tables reject UTF-8 collations (the event table's `grp` is UTF-16 binary), lock hints
  (ROWLOCK, READPAST, UPDLOCK), IGNORE_DUP_KEY and DDL inside a transaction; the variant is derived from the same
  script by checked text edits.

## Where publish's CPU goes now (Ig, per call)

Unpacking the JSON batch into `@u` (OPENJSON + BASE64_DECODE of the bodies): ~1.9 ms of ~3.5 ms. Next candidates:
memory-optimized table types for the table variables (bodies are copied into tempdb today), and a cheaper batch
format than JSON with base64 (B16 note).
