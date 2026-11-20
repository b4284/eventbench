# Matrix 14: bake-off of existing products, with one-event publishes (backlog B0, B2)

**Question:** do existing products give per-source order, no cross-source blocking and 24k ev/s on one SQL
database, so that we don't have to build our own dispatcher?

**Rules for every run:**
- Each publish carries **one event** (`MaxPublishBatch=1`), as real producers send each event the moment it is
  generated.
- The 0.5% slow-event overlay, a pipelined worker with 4 slots, and the same oracle checks.
- One host, Postgres primary only, host networking.

`PublishConcurrency` is new:
- **1:** each of the 32 producers waits for every publish before sending the next.
- **64:** each producer keeps up to 64 publishes in flight. Only events of the same source wait for each other,
  so per-source order is kept. This models many independent sources firing at once.

| run | product | offered | produced/s | applied/s (steady) | Postgres cores | other cores | correctness | blocking violations |
|---|---|---:|---:|---:|---:|---:|---|---:|
| Q1 (matrix-13) | pgqueue, **batched** publishes | 24.0k | 24.0k | 24.0k | 8.7 | — | pass | 106 |
| B1 | pgqueue, 1 event, 32 waiting producers | 24.0k | 17.8k | 17.8k | 8.5 | — | **all pass** | 0 |
| B2 | pgqueue, 1 event, concurrent | 24.0k | 16.6k | **2.5k** (collapse) | 7.0 | — | not drained | 600k |
| B3 | = B2, `NOTIFY` off (200 ms poll) | 24.0k | **24.0k** | 12.9k | 9.0 | — | pass (drained in 69 s) | 977k |
| M0 | MassTransit v8.5.10 SQL transport, `PartitionedOrdered` | 3.9k | 3.0k | 3.1k | 12.5 | — | pass | 2.8k |
| M1 | MassTransit, 1 event, 32 waiting producers | 24.0k | 3.7k | 3.7k | **15.7** | — | all pass | 0 |
| M2 | MassTransit, 1 event, concurrent | 24.0k | 5.6k | 2.9k | 8–13 | — | pass | 30k |
| T1 | Temporal 1.29.3, workflow per source, activity per event (rate limits raised) | 1.3k | 1.6k | **0.28k** (peak 1.0k) | 8.7 | Temporal 6.7 | pass (drained in 69 s) | 22k |

"Correctness" covers per-source order, exactly-once (cursors, records, applied = generated) and drain. Every
product kept order and applied each event exactly once; the differences are only in cost and throughput.

## Findings

1. **MassTransit v8 SQL transport tops out around 4k ev/s on this host,** at about **3.8 Postgres cores per 1k
   ev/s**: ten times `pgqueue`. Per event, `pg_stat_statements` shows:
   - **fetch about 2 ms:** a `row_number()` window per partition key over the whole queue, and 1.9 fetch calls
     per message;
   - **send 1 ms:** two inserts plus a per-row notify trigger;
   - **delete 0.45 ms**, plus a metrics insert.

   This is the same scan-the-queue design that capped PGMQ, so it cannot reach 24k on one database. It is also
   Apache-2.0 only up to v8; v9 needs a commercial licence.
2. **Temporal is correct but about 100× too expensive per event for this workload.**
   - Each event costs a signal, a workflow task and an activity: about 7 history events and several database
     transactions.
   - At 1.6k/s produced it applied 0.28k/s on 15 cores (Postgres + Temporal).
   - It also has a 2 MB payload limit by default, so the 4 MB inline events had to be capped at 1.5 MB.

   It is built for long-running business workflows, not for a 24k ev/s event stream.
3. **pgqueue's direct-insert publish path does not survive real producers** (B2): with many concurrent single-event
   inserts it collapses to 2.5k/s.
   - The per-insert `NOTIFY` takes a global lock at commit, so every publishing transaction is serialised.
   - Turning `NOTIFY` off (B3) restores 24k/s publishing at 1.3 ms per call. But the dispatcher then only finds
     new rows by polling every 200 ms, and its head reads slow down as the backlog grows (610k): consumption
     fell to 12.9k/s.
4. **Both bottlenecks disappear if the dispatcher also receives the publishes,** as the user proposed:
   - inserts are batched across concurrent publishers (one statement, no `NOTIFY`);
   - the dispatcher knows the new rows without asking the database;
   - with hand-out from memory, the backlog stays small, so head reads stay cheap.

   This is the design in `docs/dispatcher-design.md`.

## Conclusion of the bake-off

No ready-made candidate comes near 24k ev/s with strict per-source order on one SQL database:
- MassTransit v8: about 4k ev/s, and later versions need a licence.
- Temporal: under 1k ev/s.
- Hatchet and DBOS were not run. They have no .NET SDK, and they use the same queue-table pattern whose scan
  costs are measured here and in DBOS's own blog.

Building the dispatcher (backlog B1) is justified. Its publish path is now required, not optional.
