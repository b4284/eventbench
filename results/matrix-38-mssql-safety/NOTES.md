# matrix-38: SQL Server In-Memory OLTP vs Postgres: durable commits, crash drills, full stack, availability group (B16, B33)

Why: the organisation has a SQL Server team and no Postgres experience (2026-11-23), so SQL Server's data safety and
cost need the same evidence Postgres has. Laptop only (the Macs are gone): 500 groups, 16k sources, ~16k ev/s offered
(RateMultiplier 1.24), 3 dispatchers, ZeroMQ, 60 s. SQL Server 2025 CU11 Developer (8 GB), Octopus's event table
memory-optimized (`OCTOPUS_MSSQL_INMEMORY=1`, SCHEMA_AND_DATA), everything else as matrix-33.
`scripts/mssql-safety.sh <phase>`; "best effort" = `DELAYED_DURABILITY = FORCED` / `synchronous_commit=off`,
"durable" = `DISABLED` / `on`.

## Octopus alone (in-memory store), best effort vs durable (perf: F, D, P, Q)

| | applied/s | e2e p50 | e2e p99 | database CPU | dispatchers |
|---|---:|---:|---:|---:|---:|
| Postgres, best effort (Pa, Pb) | 15.9k | 6.2–6.4 ms | 1.29 s | 1.4–1.5 cores | 1.2–1.3 |
| Postgres, durable (Qa, Qb) | 15.9k | 7.2–7.4 ms | 1.29 s | 1.4–1.5 cores | 1.2–1.3 |
| SQL Server In-Memory, best effort (Fa, Fb) | 17.5k*, 15.9k | 18.4*, 11.7 ms | 6.2 s*, 1.30 s | 7.3*, 5.8 cores | 2.4–2.6 |
| SQL Server In-Memory, durable (Da, Db) | 15.4k*, 15.9k | 15.7, 17.9 ms | 1.32 s | 6.3, 6.1 cores | 2.3–2.4 |

\* Fa and Da: after the database was recreated SQL Server took ~25 s to reach the offered rate (a backlog built,
then caught up), which raised Fa's p99; the repeats were clean. All 8 runs correct.

- **Durable commits cost little on both:** Postgres +1 ms p50, SQL Server +4–6 ms, no CPU difference. SQL Server's
  WRITELOG waits average ~1.3 ms per commit (Da: 56.6k waits, 73 s).
- **SQL Server needs ~4x Postgres's CPU** for Octopus here (6 vs 1.5 cores; matrix-33 with Mosquitto: ~3.2x, as
  Postgres got cheaper with ZeroMQ: 1.4–1.5 cores vs 2.1). Its dispatchers also work twice as hard (2.4 vs 1.25).
- Where SQL Server's time goes (Da, per call): publish 7.7 ms (253 s CPU over the run), advance 48.5 ms, settle
  16.8 ms, move_cursors 10.4 ms.

## Crash drills: kill -9 of the database 30 s into the run, restarted 5 s later (crash: K*)

| run | engine, durability | back (start -> accepts queries) | oracle | gaps reported, all healed | publish retries |
|---|---|---:|---|---:|---:|
| Ka | SQL Server In-Memory, durable | 11.5 s | **pass** | 3,897 | 20.3k |
| Ke | SQL Server In-Memory, durable | 10.7 s | **pass** | 5,180 | 18.4k |
| Kc | SQL Server In-Memory, best effort | 10.5 s | **pass** | 2,733 | 18.5k |
| Kb | Postgres, durable | 14.0 s | **pass** | 0 | 26.0k |
| Kd | Postgres, best effort | 12.8 s | **pass** | 0 | 25.6k |

Every event applied exactly once, in order, after both engines' crash recovery; the In-Memory table came back from its
checkpoint files and log. Throughput was back ~16–22 s after the kill on both, then caught up at 2–3x the offered rate.

**What this drill cannot show:** a SIGKILL ends the process, not the machine; log blocks already written to the OS
survive in its page cache. So it tests crash recovery and the clients' retries, not durable vs best-effort commits
(the best-effort runs pass too). Only a power loss or OS crash separates those, which the laptop cannot do to itself
(WSL2 shuts down with it). The commit modes' guarantees are documented: durable = flushed before the reply.

**Gaps reported on SQL Server, none on Postgres, in every run (not a crash effect):** SQL Server's publish replies
report positions missing below an event in normal runs too (Fa–Db: 0.9–7.1k per run, of ~1M events; the drills 2.7–5.2k);
Postgres never does. The count is a hint (how many positions directly below are neither stored nor applied), computed
against what the publish's transaction can see. A source has up to 4 events in flight (SourcePipeline 4), so its
neighbours may be stored by concurrent publishes; SQL Server's publish calls take ~8 ms (Postgres's ~1 ms), so more
of them overlap, and its memory-optimized table is read at a transaction snapshot. Likely harmless: correctness held
everywhere, and a client's `RepairAsync` leaves positions still in flight alone. To confirm by reading the backend.

## The full stack: Octopus and the application's writes on the same engine, durable (full: T, S, U)

The workers commit their own writes (records, cursors, contexts of 1 KB) to the same server (database `bench`), with
durable commits on both databases.

| run | engine | store's commits | applied/s | e2e p50 | database CPU | laptop CPU |
|---|---|---|---:|---:|---:|---:|
| Ta, Tb | Postgres | group commit (16 executors, <= 32/tx, avg 10.5) | 15.9k | 34–35 ms | 6.6 cores | 70% |
| Ua | Postgres | one commit per event | 10.1k | 23 s (backlog) | 7.8 | 72% |
| Sc, Sd | SQL Server | group commit (as Postgres's, avg 21/tx) | 13.9k, 13.2k | 12–15 s (backlog) | 11.8–11.9 | 79% |
| Sa, Sb | SQL Server | one commit per event | 7.8k, 7.3k | 36 s (backlog) | 10.9–12.3 | 72–79% |

- **With durable commits, the store needs group commit on either engine.** One commit per event means one log
  flush per event: SQL Server showed 122k `FLUSHFILEBUFFERS` for 131k commits (in a diagnostic run), almost no
  sharing, and ~3 ms of each commit procedure's "CPU" was the flush. Postgres without it held only 10.1k too.
  (The SQL Server store got group commit for this: `Store:BatchCommits` as in the Postgres store, one transaction
  with one `EXEC eb_commit` per event in source order; it ignored the option before.)
- **With group commit, SQL Server tops out at ~13.5k on this laptop**, CPU-bound (scheduler yields, publish 24 ms
  per call; the laptop at 79%), at ~0.87 SQL Server cores per 1k ev/s; Postgres holds 16k at ~0.41. So for the
  whole stack SQL Server needs **~2.1x Postgres's CPU** (Octopus alone: ~4x; the application's writes cost SQL Server
  ~1.5x what they cost Postgres).
- Correct in every run, backlog or not.

## Availability group: synchronous commit to a second replica (agperf: A, B)

`infra/mssql-ag/up.sh`: two SQL Server 2025 containers (eb-ag1 :1433, eb-ag2 :1436, 8 GB each) on the host network,
`CLUSTER_TYPE = NONE` (no Pacemaker: manual failover, `infra/mssql-ag/failover.sh`), `SYNCHRONOUS_COMMIT`,
`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1` (no commit without the secondary), certificate-authenticated
endpoints, automatic seeding, FULL recovery, durable commits. `octopus` (In-Memory) is recreated inside the AG for
every run (`recreate-db.sh`: ~10 s including seeding); `bench` lives in it. Clients use a listener: HAProxy on
127.0.0.1,14330 following the replica whose role check (`rolecheck.py`, http /primary) says PRIMARY.

| run | | applied/s | e2e p50 | e2e p99 | primary | secondary |
|---|---|---:|---:|---:|---:|---:|
| Aa, Ab | Octopus alone | 16.1k, 16.0k | 32, 34 ms | 1.65, 1.35 s | 7.4 cores | 1.7 cores |
| Ba, Bb | full stack (group commit, 16 executors) | 7.2k, 8.1k | 31–38 s (backlog) | | 8.9–10.2 | 2.1–2.2 |
| Da, Db (single node, above) | Octopus alone | 15.9k | 16–18 ms | 1.32 s | 6.1–6.3 | — |

- **Octopus alone keeps 16k with a synchronous secondary:** p50 ~33 ms (single node 16–18 ms); `HADR_SYNC_COMMIT`
  waits average ~10–13 ms per commit on this laptop (the secondary hardens the log before the primary's commit
  returns: the equivalent of Postgres's `synchronous_commit = on` with a synchronous standby, not `remote_write`).
- **The full stack does not** (7–8k), and neither does Postgres's with a synchronous standby (below): on one laptop,
  a commit that waits for another replica's flush makes the store's commits ~60–90 ms instead of ~10–30.
- First attempt discarded (`ag-listener-flaps/`): the role check started a `podman exec sqlcmd` per poll; under load
  it answered late, HAProxy marked the healthy primary down and cut 348 sessions mid-run. Rewritten to keep one
  sqlcmd session and tolerate 5 s; no cuts since. A real AG listener (WSFC/Pacemaker) does not depend on this.

### Tuning the full stack on the AG (agtune, agslots): no effect

| run | change | applied/s |
|---|---|---:|
| Bc | 64 store executors, <= 64 per batch (avg fell to 6.6/tx: commits waited for work) | 7.2k |
| Bd | + dispatcher pool 64 | 7.1k |
| Be, Bf | `MaxParallelPerWorker = 4` (**a no-op**: overlay-slow.json already sets 4 for every run) | 6.8k, 7.0k |

Mid-run (Bf): log send queue 60 KB, redo queue ~4 MB, all SYNCHRONIZED (the secondary keeps up), but 108 runnable
tasks on SQL Server's 24 schedulers with the laptop at 75–79%: three SQL Server processes, the benchmark, the
dispatchers and the listener share 24 threads. The limit here is the laptop, not the AG; the numbers to carry over
are costs (primary ~1.0 core per 1k ev/s with the full stack and a secondary, secondary ~0.25).

## Failover drills on the AG (agcrash: C)

### Two replicas: writes stop after the failover (`ag-2replicas-drill/`)

Primary killed, the secondary promoted 2 s later: **"Unable to access database 'octopus' because it lacks a quorum of
nodes for high availability"**. With `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1` the new primary has no
secondary, so no commit completes until one returns: the same as Postgres's 1+1 strict mode in the Patroni drill that
led to B28 (2+2). Zero loss and writes after losing a node need a third replica (or the setting lowered to 0 by an
operator, accepting a single copy for a while).

The same drill showed two `CLUSTER_TYPE = NONE` hazards:
- **The old primary comes back as PRIMARY** (both replicas said PRIMARY, and both role checks answered 200). Nothing
  tells it otherwise; demoting it failed (41104). A listener that trusts the replicas' own word would route writes to
  both. Fixed in the lab the way a cluster manager does it: `failover.sh` records the chosen primary
  (`~/.cache/eventbench/mssql-ag/primary`, as Patroni's etcd key), the role check answers 200 only for it, and the old
  primary is rebuilt as a secondary (`rejoin.sh`: drop its AG and databases, remove and re-add the replica, join,
  automatic seeding; ~45–85 s).
- The run hung until the 15-minute limit (its writes blocked for good).

### Three replicas (default since): correct, ~75 s without writes

`AG_REPLICAS=3` (6 GB each; MySQL stopped for the memory). kill -9 of the primary 30 s into the run, forced failover
2 s later, the old primary restarted 30 s after that and rebuilt as a secondary.

| run | | killed | failover | the other secondary attached | writes back | old primary rejoined | oracle |
|---|---|---|---:|---:|---:|---:|---|
| Ca | Octopus alone | ag1 | +2 s | +77 s (restarted at +47 s) | ~+76 s | +2:37 | **pass** |
| Cb | full stack | ag2 | +2 s | +78 s (restarted at +49 s) | | +4:42 | **pass** |

Every event applied exactly once, in order: nothing acknowledged was lost by the forced failover
(`FORCE_FAILOVER_ALLOW_DATA_LOSS` on a synchronous secondary with `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1`).
**The outage is the orchestration, not SQL Server:** after a failover the remaining secondary's databases kept
reconnecting to the old primary (NOT SYNCHRONIZING, not suspended); a database that does see it is suspended
("failover from partner") and needs one RESUME. Neither a plain RESUME nor SUSPEND + RESUME re-attached them
reliably; restarting that replica did (its databases come back suspended, one RESUME, SYNCHRONIZED in ~1 s).
`failover.sh` does that after 20 s (47–49 s under load: its own status queries were slow). Planned failovers needed it
in one direction only (ag1 -> ag2: all SYNCHRONIZED in 8 s; ag2 -> ag1: ag3 needed the restart, 46 s). With a cluster
manager (Pacemaker, `CLUSTER_TYPE = EXTERNAL`, or WSFC on Windows) failover is automatic and the secondaries are
re-pointed by the cluster; that is the production setting, and this drill does not measure it. To compare: Patroni's
failover (matrix-19) took 19–24 s, automatic, with the remaining standbys following on their own.

## Postgres with a synchronous standby, the same comparison (pgsync: R, W)

`infra/pg-standby/up.sh` (streaming standby on the laptop), `synchronous_standby_names = 'eb_standby'`,
`synchronous_commit = on`: a commit waits for the standby's flush, as the AG's synchronous commit waits for the
secondary to harden the log (the Patroni lab used `remote_write`, memory only).

| | applied/s | e2e p50 | primary | replica |
|---|---:|---:|---:|---:|
| Postgres + sync standby, Octopus alone (Ra, Rb) | 15.9k | 14, 57 ms | 1.75 cores | 0.3 |
| SQL Server AG, Octopus alone (Aa, Ab) | 16.1k, 16.0k | 32, 34 ms | 7.4 | 1.7 (each secondary) |
| Postgres + sync standby, full stack (Wa, Wb) | 7.0k, 10.7k | 28 s, 9.5 s (backlog) | 4.4–5.1 | 0.6–0.7 |
| SQL Server AG, full stack (Ba, Bb) | 7.2k, 8.1k | 31–38 s (backlog) | 8.9–10.2 | 2.1–2.2 |

**On one laptop, neither engine's full stack holds 16k with a replica that must flush**: the store's commits go from
~10 ms to ~94 ms on Postgres (Wa) and to 54–81 ms on SQL Server. A lab limit (one disk, one CPU for every replica),
the same for both; real replicas on their own hosts (B20 open). Octopus alone holds 16k on both, Postgres at p50
14–57 ms on 2 cores in all, SQL Server at ~33 ms on ~9 (primary + one secondary).

## Verdict

**Data safety:** SQL Server In-Memory OLTP matched Postgres in every check made here: durable commits cost little
(+4–6 ms p50, no CPU), kill -9 of the database lost and duplicated nothing, and a forced failover to a synchronous AG
secondary lost nothing acknowledged. What SQL Server needs, as Postgres did (B28): **three replicas** for writes to
continue after losing one with zero loss, and **a cluster manager** (WSFC or Pacemaker): without one
(`CLUSTER_TYPE = NONE`) failover is manual, a returning old primary still believes it leads, and the remaining
secondary had to be restarted to follow the new primary (~75 s without writes).

**Cost:** SQL Server needs ~4x Postgres's CPU for Octopus alone, ~2x for the whole stack (Octopus + the application's
writes; at most ~13.5k on this laptop, CPU-bound), and each synchronous secondary costs ~0.1 (Octopus alone) to
~0.3 (whole stack) cores per 1k ev/s, a Postgres standby ~0.02–0.07. Sizing for 24k (rough, from these laptop
numbers): SQL Server primary ~20–24 cores for the whole stack plus ~3–7 per secondary; Postgres ~10 plus ~0.5–1.5.

**For the SQL Server team:** the costs to look at, from SQL Server's own statistics: Octopus's publish procedure
(~5 ms CPU per call single-node, mostly unpacking the JSON batch with base64 bodies) and its cursor writes
(`advance` 48 ms, `settle` 17 ms per call); the store's commit procedure (cheap per statement; durable commits need
group commit). And the AG orchestration: Pacemaker/WSFC behaviour after a forced failover is the open question.

## Also changed

- `SqlServerStore`: group commit (`Store:BatchCommits`, `BatchMaxSize`, `BatchExecutors`), as the Postgres store's.
- `infra/mssql-ag/`: AG of 2 or 3 replicas, listener (HAProxy + role checks), recreate/failover/rejoin scripts.
- `scripts/octopus-drill.sh`: `mssql-kill`, `pg-kill`, `ag-kill`, `ag-switch`; `infra/octopus/up.sh`: `MSSQL_AG=1`.
- `scripts/mssql-safety.sh`: this matrix's phases.
