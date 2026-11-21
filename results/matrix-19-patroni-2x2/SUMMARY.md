# Matrix 19: the 2 + 2 layout (B28)

## Final: through a leader-following endpoint (HAProxy), etcd 3.5.17, Octopus d8de3c4

Clients (dispatchers and workers) connect to HAProxy (`infra/patroni/haproxy.sh`: health checks on Patroni's
`/primary`, `on-marked-down shutdown-sessions`), `configs/overlay-patroni-2x2-haproxy.json`. Workers abandon (never
cancel) a database call after 60 s.

| run | failure | correctness | new leader | quorum afterwards | drained |
|---|---|---|---|---|---|
| X0 | none | pass | — | ANY 2 (dc1a,dc1b,dc2a) | 39 s |
| X1 | leader node killed, not restarted | pass | 19 s | ANY 2 (dc1a,dc2a) | 22 s |
| H01-H10 | the leader's DC killed (2 nodes + its etcd), 10 times | **10 of 10 pass** | 17-21 s | ANY 1 (same-DC survivor) | 16-42 s |
| X3 | the other DC killed | pass | — | ANY 1 (dc1a) | 44 s |
| X4 | the leader's DC frozen 60 s | pass | 24 s; it stays after the freeze | ANY 1, then ANY 2 as DC1 rejoins | 16 s |

"Correctness" = every oracle check except cross-source blocking. Latency is not meaningful here: four Postgres
nodes, three of them replaying WAL, share one 24-core host (baseline e2e p50 20-40 s).

## Earlier runs (multi-host connection strings)


Patroni 4.1.5 quorum mode, `synchronous_node_count: 2` (ANY 2 of the 3 standbys: every commit reaches the other
DC), strict; etcd members in DC1, DC2 and a witness; 12.9k ev/s; 3 dispatchers; workers commit their own writes.

## Final setup (etcd 3.5.17, no client command timeout on commits)

| run | failure | correctness | failover | quorum afterwards |
|---|---|---|---|---|
| V0 | none | pass | — | ANY 2 (dc1a,dc1b,dc2a) |
| V1 | leader node killed (not restarted) | pass | 20 s | ANY 2 of the 2 survivors |
| V2 | the leader's DC killed (2 nodes + its etcd) | **stalled**: 8 events not applied | 22 s | ANY 1 (same-DC survivor) |
| V2b, V2c, V2g | same | pass | 19-23 s | ANY 1 |
| V2d, V2h | same | **stalled**: thousands of events not applied, all still stored | 20-21 s | ANY 1 |
| V3 | the other DC killed | pass | none needed | ANY 1 |
| U4b | the leader's DC frozen 60 s | pass | 24 s; the new leader stays after the freeze | ANY 2 again |

In every stalled run, nothing was lost: unapplied = produced - applied = events still stored in Octopus, and no
source's Octopus cursor is ahead of the application's.

Two different stalls:
1. **V2d, V2h:** benchmark workers stuck in their own database client (Npgsql) after the failover; the primary had no
   active session at all. With the earlier 60 s command timeout these calls failed and were retried; without it they
   hang. A harness problem; but a command timeout cancels a commit waiting for the standby, which Postgres then
   acknowledges without replication (unsafe). To be solved without cancelling commits.
2. **V2:** 8 events of one source stored, no worker holding anything, after the groups were reshuffled three times
   in 8 s (all leases expired during the 22 s outage; one dispatcher briefly claimed all 500 groups). Suspected:
   a new owner that never receives credit for the group never reads its heads. An Octopus liveness gap.

## etcd 3.5.9 (subdirectory etcd-3.5.9): a split brain
Freezing the leader's DC for 60 s: the resumed etcd member's stale lease revocations were applied in the new term
(fixed upstream in 3.5.13), the new primary's leader key vanished, the old primary took the lead again and the new
primary's timeline was rewound away. The oracle passed by timing; acknowledged writes could have been lost.
