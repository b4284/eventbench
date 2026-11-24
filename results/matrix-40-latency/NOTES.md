# matrix-40: latency while the laptop keeps up, Postgres vs SQL Server (durable)

What was asked: real p99 numbers at loads the laptop can carry. Earlier runs all showed p99 ~1.3 s because the
workload makes 0.5% of events slow on purpose (1–3 s in the worker, overlay-slow.json) and events of the same source
wait behind them. Here those are off (`SlowEvents:Fraction=0`); everything else as matrix-38/39: laptop only, ZeroMQ,
3 dispatchers, durable commits (option B's local part; no replica, which on one laptop would distort it), SQL Server
In-Memory event table with the raw-blob publish. 60 s per run, one run per point.

"Octopus alone": the workers keep their records in memory, so the database does only Octopus's work.
"Whole stack": the workers also write their records to the same database (group commit).
"Kept up": applied at least 98% of the offered rate, with no growing backlog (p50 under 500 ms).
e2e = from the moment the source created the event to the worker's commit.
Caveat: ~350 large uploads (2–16 MB) per run at any load: 0.15% of events at 4k, 0.04% at 16k. They take ~2 s, so
they dominate p99.9 at low load and lift p99 at 4k.

## Octopus alone

| offered | Postgres p50 | p90 | **p99** | p99.9 | cores | SQL Server p50 | p90 | **p99** | p99.9 | cores |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 4k | 7.1 ms | 11 ms | **93 ms** | 2.1 s | 1.2 | 9.5 ms | 20 ms | **373 ms** | 2.1 s | 2.4 |
| 8k | 7.7 ms | 21 ms | **63 ms** | 140 ms | 1.2 | 12 ms | 30 ms | **80 ms** | 145 ms | 3.0 |
| 12k | 7.6 ms | 15 ms | **58 ms** | 99 ms | 1.3 | 13 ms | 32 ms | **147 ms** | 300 ms | 3.7 |
| 16k | 8.3 ms | 33 ms | **260 ms** | 493 ms | 1.5 | 15 ms | 52 ms | **166 ms** | 280 ms | 4.7 |
| 20k | 8.4 ms | 19 ms | **67 ms** | 110 ms | 1.6 | 18 ms | 66 ms | **205 ms** | 316 ms | 5.3 |

Both keep up to 20k (the most tried). Typical latency: Postgres 7–8 ms, SQL Server 10–18 ms. **p99: Postgres ~60–90
ms, SQL Server ~80–200 ms** (Postgres 260 ms at 16k is one run's outlier; one run per point). CPU: SQL Server 2–3.3x.

## Whole stack (the application's writes on the same database)

| offered | Postgres kept up | p50 | p90 | **p99** | cores | SQL Server kept up | p50 | p90 | **p99** | cores |
|---:|---|---:|---:|---:|---:|---|---:|---:|---:|---:|
| 4k | yes | 12 ms | 52 ms | **421 ms** | 3.0 | yes | 20 ms | 55 ms | **376 ms** | 6.2 |
| 8k | yes | 18 ms | 92 ms | **383 ms** | 4.0 | yes | 37 ms | 145 ms | **3.2 s** | 8.4 |
| 12k | yes | 30 ms | 262 ms | **1.5 s** | 5.1 | **no** | 2.6 s | 14.7 s | **22.8 s** | 10.5 |
| 16k | yes | 65 ms | 283 ms | **958 ms** | 6.2 | **no** | 16.1 s | 28.3 s | **36.2 s** | 10.5 |

Postgres keeps up to 16k (p99 0.4–1.5 s); SQL Server to 8k, and 8k is already at its edge (p99 3.2 s, 8.4 cores;
the laptop at 63%), not at 12k (2.6 s median: a backlog). The whole stack's p99 is high on both: each worker's
commit waits for its group commit (16 executors, up to 32 events each) and the large uploads are written to the
database too; Postgres's p99 is not monotone (one run per point).

## Reading it

- With Octopus alone the laptop is far from its limit on both engines; the latency difference is SQL Server's
  slower publish and cursor procedures (B37), not the laptop.
- For the whole stack the laptop's CPU is the limit for SQL Server (~10.5 cores busy at 12–16k): **on this laptop
  SQL Server's whole stack keeps up to ~8k ev/s, Postgres's to 16k+**. On a server with more cores the same ratio
  of CPU per event holds (~2x), not the same ceiling.
- All 18 runs correct.

Full per-run tables: `scripts/latency-table.py results/matrix-40-latency`.
