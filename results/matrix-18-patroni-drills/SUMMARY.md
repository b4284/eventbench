# Matrix 18: drills on a Patroni pair (B6, B27)

Patroni 4.1.5 (`infra/patroni`): etcd + two Postgres 18 nodes, strict synchronous mode, `synchronous_commit =
remote_write`, leader lease 20 s / loop 5 s. 12.9k ev/s, 3 dispatchers, workers commit their own writes, direct
multi-host connections (`configs/overlay-patroni.json`). Failure 30 s into the run (`scripts/octopus-drill.sh`).

| run | failure | correctness | e2e p50 / p99 | blocking violations | notes |
|---|---|---|---|---|---|
| T0, T0b | none | pass | 2.1 s / 33 s; 319 ms / 52 s | 354k; — | WAL-bound: WALWrite / WALInsert / SyncRep waits (local flush per commit comes with synchronous replication) |
| T1 (1st) | leader killed, not restarted | — (hung) | — | — | new leader in 19 s, then **no commit for hours**: strict mode with no standby left (B28) |
| T1 | leader killed, restarted after 30 s | pass | 1.2 s / 89 s | 152k | new leader in 13 s; old leader back as standby 32 s after its restart; drained 69 s |
| T2 | leader frozen 30 s (hang, not crash) | pass | 326 ms / 110 s | 108k | new leader in 22 s; 122 redeliveries flagged |
| T3 | MQTT broker restarted | **FAIL**: processing stopped | — | — | workers' view of the dispatchers lost (retained announcements gone, "gone" wills); fixed in Octopus |
| T3c | MQTT broker restarted (fixed) | pass | 12.5 s / 56 s | 360k | drained 40 s; every dispatcher kept its workers |
| T4 | dispatcher 1 cut from the database 30 s (still running) | pass | 4.1 s / 29 s | 158k | 884 redeliveries flagged |
| T5 | both database nodes frozen 120 s | pass | 200 ms / 141 s | 87k | everything waited and resumed; drained 19 s |

Produced counts are lower in the failure runs (about 440k of 815k): producers wait while the database is gone.

## Found
- Octopus did not survive a broker restart: fixed (announcements repeated every 5 s).
- Two data nodes in strict synchronous mode cannot commit after losing one until it returns: owner decision B28.
- A cancelled commit (client command timeout) is acknowledged by PostgreSQL without replication: documented in
  the Octopus design (§5.5); the benchmark's store uses a 60 s command timeout on commits.
- The drill script had no time limit: a blocked database hung it for 7 hours. Now 15-20 min.
