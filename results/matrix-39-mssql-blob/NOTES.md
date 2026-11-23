# matrix-39: SQL Server publish without base64 (B37)

Octopus's SQL Server publish took each batch as one `nvarchar(max)` JSON with the bodies base64-encoded: SQL Server
received ~2.7x the bodies' size (UTF-16 text of base64) as one large object, then parsed and decoded it. In
matrix-38's durable runs publish was ~75% of SQL Server's CPU, and its statements explained only ~1/3 of that: the
rest was outside them (receiving and handling the parameter; the log flush counted as worker time).

Change (Octopus, `OCTOPUS_MSSQL_BODIES=blob`, now the default): the bodies travel as one raw `varbinary(max)` parameter,
the JSON carries each body's offset and length, the procedure takes `SUBSTRING(@b, o, n)`; base64 JSON still accepted.
The backend suite passes (18/18 disk-based, 18/18 In-Memory).

Same setup as matrix-38 (laptop, ZeroMQ, ~16k ev/s, 3 dispatchers, In-Memory event table, durable commits).

| run | bodies | applied/s | e2e p50 | SQL Server | dispatchers | laptop |
|---|---|---:|---:|---:|---:|---:|
| Ja, Jb | base64 JSON | 15.9k | 18.4, 17.3 ms | 5.67, 5.61 cores | 2.29, 2.27 | 53–54% |
| Ja, Jb | **raw blob** | 15.9k | 15.4, 12.7 ms | **4.12, 4.12** | **1.66, 1.72** | 43–44% |
| (matrix-38 Qa, Qb) | Postgres, durable | 15.9k | 7.2–7.4 ms | 1.43–1.51 | 1.23–1.26 | 36% |

**SQL Server −27% CPU, dispatchers −26%** (no base64 encoding, smaller messages), p50 a little lower. Octopus on SQL
Server now needs ~2.8x Postgres's CPU (was ~4x). All runs correct.

Whole stack, blob (Sa-full-blob, one run): 12.6k applied (matrix-38 Sc, Sd with base64: 13.9k, 13.2k), SQL Server
10.7 cores (11.8–11.9): within noise; still CPU-bound on the laptop. The application's writes are now the larger part
of SQL Server's work (~7.5 of 10.7 cores, ~0.6 per 1k ev/s; the Postgres store ~0.3): the SQL Server store runs one
`EXEC eb_commit` per event even in a group commit. Next: a set-based batch commit with a table-valued parameter.

## The store's group commit: N EXECs vs one set-based call (storetvp: X)

`Store:BatchMode=tvp`: the batch as a table-valued parameter to `eb_commit_many`, which does the per-event checks in
a few set-based statements (cursors locked in source order, conflicts, inserts, version-guarded updates, cursors
advanced; at most one event per source per batch, a repeat commits alone). Whole stack, durable, blob publish:

| run | store's group commit | applied/s | SQL Server | store commit p50 |
|---|---|---:|---:|---:|
| Xa, Xb | one EXEC per event in one transaction | 13.3k, 13.2k | 10.6, 10.5 cores | 44 ms |
| Xa, Xb | one set-based call (TVP) | 11.8k, 11.4k | 10.9, 10.8 | 50 ms |

**No gain; ~12% less throughput.** Correct in all four. `exec` stays the default (`tvp` kept as an option).

## Where SQL Server's CPU goes (diagnostic runs, not kept)

Whole stack, 40 s window: all statements together (`sys.dm_exec_query_stats`) **~2.4 cores of the ~10.6** the
container used. Per call: publish's `@st` (the missing-position hint, a range seek per event) 1.5 ms and `@u` (unpack)
1.1 ms; the store's load SELECT 0.05 ms and its commit statements 0.01–0.04 ms each. So **~3/4 of SQL Server's CPU is
outside query execution**. By thread (`/proc`, SQL Server on Linux names threads loosely): the largest group (~4.8
cores, 11 threads) carries the In-Memory OLTP module path, then scheduler threads (~1.7), network I/O (~0.9), log
writer (~0.1). Not conclusive without a profiler (no `perf` here): a question for the SQL Server team's tools
(Extended Events, Windows Performance Analyzer) on a dedicated host. (In matrix-33, disk-based tables used more CPU in
total, so In-Memory is not a net loss.)

## The gap reports on SQL Server (B38: G, H)

SQL Server's publish replies report positions missing below an event in normal runs (5–7k per ~1M events); Postgres
never does. The hint does not affect correctness (every run correct); it would make a client's repair do extra work.

| run | SourcePipeline | Octopus | gap reports |
|---|---:|---|---:|
| G1 | 1 (one event in flight per source) | as matrix-39 | 7,150 |
| G4 | 4 | as matrix-39 | 7,188 |
| H1 | 1 | hint's cursor re-read in the same statement as the event table | 5,075 |
| H4 | 4 | same | 5,392 |

- **Not concurrent publishes of one source:** with one event in flight per source (G1) the count is the same.
- **Partly a stale cursor:** publish read the cursor (disk table, its statement's snapshot) before the event table's
  snapshot (memory-optimized: taken at the INSERT); a cursor move and its cleanup committing in between showed a
  deleted event below an unmoved cursor. Re-reading the cursor in the hint's own statement (Octopus, kept): −28%.
- The rest: not found (time-boxed). Postgres computes the hint in one statement with one snapshot.
