# matrix-23: three-site cluster, cost of synchronous standbys (B5, B20)

Cluster: `infra/patroni-geo`. DC1 is the laptop (leader + `geo-dc1b`), DC2 is m4mini (`dc2a`, `dc2b`, native macOS
Postgres 18.7), and the witness is m1mini (etcd only). Quorum ANY 2 of 3, strict, `synchronous_commit=remote_write`.
The sites share one gigabit LAN (0.56 ms round trip). Both DC2 standbys sit behind m4mini's single 1 GbE port.
Workload: 500 groups at 12.9k ev/s (RateMultiplier 1.0), Octopus, workers commit their own writes. Every run lost
nothing; the cross-source blocking check failed in all of them, because the laptop is at its limit at this rate.
Scripts: `scripts/geo-ab.sh`, `scripts/geo-wal.sh`; raw output in `ab-G.txt`, `wal-W*.txt`.

## 1. Checkpoints decided the cost of synchronous replication

| runs | settings | applied/s, waiting for 2 of 3 | applied/s, not waiting |
|---|---|---:|---:|
| G1–G4 | max_wal_size 4GB, wal_compression lz4 | 1,785 / 1,323 | 8,407 / 10,624 |
| W4–W6 | max_wal_size 12GB, wal_compression zstd | 8,536 / 9,366 | 8,208 |

At 4GB, every checkpoint was triggered by WAL volume (16 requested, 0 timed): one every ~15 s under load, each
followed by a storm of full-page images. That came to ~12 KB of WAL per event. Both DC2 standbys receive a copy over
m4mini's one link, so the waiting runs were capped at ~113 MB/s = the gigabit link. With checkpoints spaced out, WAL
is **7.1 KiB per event (exact, from the LSN)**, and waiting for 2 of 3 standbys costs nothing measurable here.

Data per event that any synchronously replicated store must ship: ~3 KB (payload 128 B–4 KB, about 2 KB on
average, plus the ~1.1 KB worker record). Postgres ships ~2.4× that: index changes, tuple headers, page images.

At 24k ev/s that is ~170 MB/s (1.4 Gbit/s) per standby, so ~2.8 Gbit/s between the DCs with two standbys in
DC2. The floor for any store is ~0.6 Gbit/s per copy. Levers: compress payloads at the source; one WAN copy
(cascading, at a quorum cost); a compressing tunnel. MySQL: `binlog_row_image=minimal`,
`binlog_transaction_compression`.

## 2. remote_write doesn't bound replay lag

After W4–W6, the DC2 standbys were **15–17 minutes behind in replay**, with 6.6–7.6 GB of WAL received but not
applied, while `geo-dc1b` on the laptop was caught up. Replay on the Macs is limited by disk: the startup process
at 3% CPU, both standbys together ~3.5k IOPS / ~107 MB/s of ~30 KB reads, on one SSD that was 93% full. Their
`pg_wal` grew to 18–19 GB each, past max_wal_size, because a standby only removes WAL it has applied. m4mini's
disk fell to 5 GB free. A `CHECKPOINT` on each standby freed 8 GB.

Partly a lab artifact (two standbys on one nearly full Mac SSD), but the production lesson stands:
`remote_write` (and `on`) make a commit wait for the standby to have the WAL, not to have applied it. A standby
can fall arbitrarily far behind in replay, and failing over to it then takes that long before the new leader
accepts writes. Production needs: an alert on replay lag (`pg_stat_replication.replay_lag`); standbys whose
disks keep up with the primary's write rate; and if the recovery time must be bounded, `remote_apply` (each commit
waits for replay; to be measured) or Patroni's `maximum_lag_on_failover` with a non-zero limit for a lagging
candidate.
