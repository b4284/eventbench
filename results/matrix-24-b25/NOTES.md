# matrix-24: B25, Octopus's own database cost

Setup: 16k ev/s (`RateMultiplier=1.24`), 500 groups, 3 dispatchers, workers commit their own writes, the single
`eb-postgres` (no standby). `scripts/perf-pgss.sh` per run: `pg_stat_statements` from 30 s to 70 s; active
backends' wait events sampled every 100 ms from 35 s to 55 s; event-table size at 70 s; the dispatchers' counters,
summed by `scripts/octopus-stats.py`. Every change ran as interleaved pairs (A, B, A, B). This host's speed
drifts during the day, so single runs aren't comparable.

## Kept

| change | Octopus | result |
|---|---|---|
| Head reads leave out sources whose "done" cursor isn't written yet | `257cc83` | group head reads 260–284k -> 25–30k per run; key lookups 93–97k -> 0.3–1.6k; Postgres **9.5–9.6 -> 8.3–8.4 cores**; the head-read statement left the top 8 (was 135 s per 40 s) |
| Event bodies up to ~8 KB stay in the row (`toast_tuple_target` 8160) | `bd54c5d` | publish 5.7/6.4 vs 6.7/8.4 ms per call (~20% less publish time), cheaper head reads, e2e p50 0.49/0.41 vs 0.77/0.92 s |
| Look up a source's next event only when it is known to be stored | `7d1de4d` | key lookups 133–165k -> 87–92k (cheap ones: small) |

How the head-read waste showed up: counters of where each handed-out event came from (O1). Publish hints gave
305k, in-memory pending events 620k, **head reads 666**, key lookups and removals 2k. Yet head reads ran
260k times and "found" rows 99% of the time. The rows were events already done in memory: "done" cursors are
written lazily (every ~100 ms). Until then, the database still sees the done event as the head. A read that
filled its limit marked the group dirty, so it read again.

## Rejected

| tried | result |
|---|---|
| Fallback head reads less often | not the waste: 7% of head reads, two-thirds of them find work |
| `VACUUM octopus.event` every second (X1/X0) | publish slower (12.0/12.7 vs 8.6/9.6 ms), extension waits not lower; the table still reached ~880 MB |
| Event table hash-partitioned 8 ways by source (P1/P0) | publish extension waits halved (309/167 vs 595/544 samples), but publish CPU and `advance` rose: Postgres 9.0/8.8 vs 8.1/8.4 cores. Reverted |
| `autovacuum_naptime` 1 s (V1/V0) | not tested: the container's command line pins it at 10 s and overrides `ALTER SYSTEM`. The four runs serve as a baseline: Postgres 7.9–8.0 cores, p50 99–222 ms |

## Where the time goes now (current code, 16k)

- Wait samples: publish is split between CPU and `Lock:extend`. The event table is insert-at-the-end and
  delete-after-apply, and the freed space is reused only after vacuum, so inserts keep extending it (one lock per
  table). At 70 s the table holds 0.9–1.6 GB for a few thousand live rows. The benchmark's own `eb_commit_many`
  waits on the same lock for its tables.
- `pg_stat_statements` per 40 s: publish ~230–280 s (plus the same again for its inner statement under
  track=all); benchmark worker statements ~400 s; `octopus.advance` (cursor writes + deletes) 55–70 s at
  100–140 ms per call.
- A publish of 23 events costs 0.68 ms of executor time on a quiet database (auto_explain: all index lookups),
  ~8–13 ms under this load: CPU contention and extension waits, not plan shape.
