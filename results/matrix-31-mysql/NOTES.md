# matrix-31: Octopus on MySQL 8.4 vs Postgres (B16)

Local runs (no shards) at the matrix-27 load: 500 groups, 16k sources, 15.9k ev/s offered, 3 dispatchers, Mosquitto,
the in-memory store (workers' writes excluded: only Octopus's own work hits the database), 60 s. MySQL 8.4.13
(`infra/mysql/up.sh`): READ COMMITTED, redo flushed once a second (`innodb_flush_log_at_trx_commit=2`), binary log
on without sync (GTIDs, for the replication drill), 4 GB buffer pool. Postgres as in matrix-27.
`OCTOPUS_BACKEND=mysql infra/octopus/up.sh`, `--Broker:AdminDatabase=mysql`.

## Result (Octopus 186eaea; Kc, Kd)

| run | applied ev/s | e2e p50 | e2e p99 | database CPU | dispatchers | correct |
|---|---:|---:|---:|---:|---:|---|
| Kc-mysql | 16.7k | 648 ms | 5.4 s | 4.7 cores | 1.4 cores | yes: drained, every cursor and event |
| Kd-mysql | 16.9k | 332 ms | 5.9 s | 4.5 cores | 1.4 cores | yes |
| Lb-pg (Postgres) | 15.9k | 7 ms | 1.3 s | 2.1 cores | 1.3 cores | yes |

(Applied above the offered 15.9k: catching up a backlog built in the first seconds.)

**Verdict:** MySQL carries the 16k load correctly but at ~2.2× Postgres's CPU per event (SQL Server: ~4–5×,
matrix-27), and it does not keep up smoothly: the broker backlog averages ~4.5–5k messages and e2e p50 sits in the
hundreds of milliseconds instead of single digits. Most sessions wait in "waiting for handler commit" (the binary log's
group commit; the redo flush is already lazy). Postgres remains the engine for Octopus; MySQL is viable where it
is the only database on offer, with more headroom (cores) than Postgres needs.

## Bugs found on the way (all fixed in Octopus; the regression tests run on all three backends)

1. **Connection churn** (`pool-min1/` and earlier): mysql_async reset every connection returned to the pool and
   closed idle ones down to the pool minimum of 1: ~16.8k connection setups per run. Fixed: no reset, min = max.
2. **The gaps query** ran for minutes: first an aggregate with OR'ed range conditions, then (Ka) a lateral form that
   reads every stored event of each source, because MySQL cannot range-seek on outer-row values. It ran 19 minutes
   after Ka, held the metadata lock that the next run's `DROP DATABASE` waited for, and kept 2 cores busy into La-pg
   (La-pg's MySQL 2.0 cores, and Ka's 7.8k ev/s, are that). Fixed: sources whose head is stored are ruled out first
   by a point lookup, the seeks use the primary key, `MAX_EXECUTION_TIME(30 s)` bounds any bad plan; up.sh kills
   leftover statements before recreating the database.
3. **Acknowledged but never stored** (`acked-loss/`, critical): a publish whose source was not registered
   reported its events as duplicates, so they were acknowledged and dropped: 4,987 sources lost their first event
   and the runs could not drain. The same fallback existed on SQL Server; on Postgres the acknowledgements would have
   been misaligned. Fixed in all backends (Octopus 9003a62): such a batch aborts and is retried with registration.
4. **Registration race** (Ka, Kb: ~270 aborted batches per run, ~100 failing their retry too, all in the first
   seconds): with registration inside the batch's transaction, a publisher's `INSERT IGNORE` could skip a source
   another transaction had inserted, and its next read did not see it (that transaction rolled back, or its commit
   was not visible yet). Nothing was acknowledged wrongly (fix 3 held), but batches failed back to the producers.
   Fixed (186eaea): registration commits on its own before the batch's transaction (also on SQL Server), and an
   abort is retried with registration and a short backoff (8 attempts, ~0.4 s; with 5, Kc still lost 3 batches to
   it). A new test (24 publishers, the first events of 600 new sources at once) failed 8–11 batches per run on the
   old MySQL code; Postgres and SQL Server passed it before and after.

## Runs kept

- `Ka-mysql`, `Kb-mysql`, `La-pg`, `Lb-pg`: Octopus 9003a62 (fix 3, not yet 2's final form or 4). Ka is slowed by
  the runaway gaps query, La-pg shares the machine with it; Lb-pg is the clean Postgres reference.
- `Kc-mysql`, `Kd-mysql`: all fixes (Kc before the retry budget went from 5 to 8 attempts).
- `acked-loss/`, `pool-min1/`: the runs that exposed bugs 3 and 1.

## The binary log's cost (Ke, Kf: `MYSQL_BINLOG=0 infra/mysql/up.sh`)

| run | applied ev/s | e2e p50 | e2e p99 | MySQL CPU |
|---|---:|---:|---:|---:|
| Ke-mysql-nobinlog | 16.5k | 19 ms | 2.8 s | 4.9 cores |
| Kf-mysql-nobinlog | 16.3k | 13 ms | 1.7 s | 4.6 cores |
| Kc, Kd (binary log on) | 16.7–16.9k | 332–648 ms | 5.4–5.9 s | 4.5–4.7 cores |

The binary log's commit path (two logs per commit, ordered group commit) is what made MySQL slow, not CPU: without
it MySQL's latency comes close to Postgres's (p50 13–19 ms vs 7), at the same ~2.2× CPU. Replication needs the
binary log, though (semi-sync and Group Replication alike), so an HA MySQL pays this.

With the binary log on (both set globally for two runs each):

| change | runs | e2e p50 | e2e p99 |
|---|---|---:|---:|
| `binlog_order_commits=OFF` | Kg, Kh | 3.2 s, 557 ms | 6.5, 4.9 s |
| `binlog_row_image=MINIMAL` | Ki, Kj | 250 ms, 74 ms | 4.1, 5.8 s |

Commit ordering is not the cost; volume is. With full row images every deleted event is logged again with its
body; MINIMAL logs only its key and helps (p50 74–250 ms), but every body still passes through the binary log once
(~1 GB binary log file every few minutes at 16k). Recommendation for Octopus on MySQL: `binlog_row_image=MINIMAL`.

## Not explored yet (B16)

- Semi-sync replication (`AFTER_SYNC`) or Group Replication for the
  failover drill.
- Cheaper batch passing than JSON with base64 (`JSON_TABLE` + `FROM_BASE64` on every publish).
