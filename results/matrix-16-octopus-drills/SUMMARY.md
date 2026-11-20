# Matrix 16: Octopus failure drills

Workers commit their own writes (`overlay-octopus-worker.json`), one event per publish, Mosquitto, 60 s runs, the
failure 30 s into the run (`scripts/octopus-drill.sh`). 3 dispatchers unless stated.

| run | load | failure | correctness | e2e p50 / p99 | blocking violations | notes |
|---|---|---|---|---|---|---|
| D1 | 16k | `kill -9` of 1 of 3 dispatchers | pass | 27 ms / 3.0 s | 13.9k | 474 duplicates (flagged redeliveries), drained 1.8 s |
| D2 | 16k | SIGTERM (planned stop) of 1 of 3 | pass | 25 ms / 2.5 s | 13.8k | 0 duplicates, drained 2.1 s |
| D3 | 16k | scale-out 2 → 3 | pass | 25 ms / 1.5 s | 13.9k | 0 duplicates, drained 1.5 s |
| D4 | 16k | Postgres primary killed, sync standby promoted | **FAIL**: 4 sources stuck | | | progress reports kept lost deliveries alive |
| D4b–D4d | 16k | same | FAIL (tracing runs) | | | found the credit leak in the .NET client |
| D4e | 16k | same, credit fix | pass (oracle counted in-doubt commits as duplicates) | 3.7 s / 33 s | 363k | over capacity even before the failure |
| D5 | 12.9k | none (baseline: sync standby, direct connections) | pass | 106 ms / 3.7 s | 107k | |
| D6 | 12.9k | Postgres primary killed, sync standby promoted | **pass** | 1.3 s / 25 s | 289k | drained 9.2 s, no loss, no double apply |
| D7 | 12.9k | none (sync standby, through PgBouncer) | pass | 164 ms / 8.5 s | 183k | PG 8.0 cores, commit p50 36 ms |

D1–D3 ran before the fixes below; their design paths are unchanged by them.

## Defects found and fixed (Octopus)
1. A worker's progress report extended any in-flight delivery of the source, whoever held it; a stale entry kept a
   lost delivery alive forever. In-flight deliveries now record their worker; only its reports count.
2. A failure report from a worker that no longer held the event could cancel the current delivery. Ignored now
   (`stale_failures` in the stats).
3. .NET client: `Fail` did not free the slot, and running deliveries were keyed by source, so two deliveries of one
   source leaked a credit. Each result re-granted it, so workers held up to 5x their slots and events queued
   inside a busy worker, kept alive by progress reports. Fixed: deliveries by identity, `Fail` releases, and the
   invariant running + granted <= slots is enforced after every result and every second.
4. Operational: a delivery outstanding for over 60 s is logged once with its source, seq and worker.

## Benchmark fixes
- In-doubt commits (the commit threw, e.g. the connection died while committing) that a retry finds applied
  count as applied, not as duplicates.
- On a drain timeout the benchmark prints events held over 30 s, the worker slots and each Octopus worker's
  credit state.
