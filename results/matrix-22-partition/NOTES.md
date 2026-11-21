# matrix-22: a real network partition between the DCs (B27)

Lab: `infra/patroni-bridged`. The 2 + 2 Patroni cluster (quorum ANY 2 of 3, strict; etcd 3.5.17 with one member per
DC plus a witness) runs on per-DC podman networks: `eb-b-dc1`, `eb-b-dc2`, and `eb-b-wan` between the DCs and the
witness. The drill disconnects DC1's three containers (dc1a, dc1b, etcd-dc1) from the WAN. They keep running and still
see each other, but they can't reach DC2, the witness, or the clients (the WAN-side HAProxy on 127.0.0.1:6440).
Workload: 500 groups, 0.7× rate, Octopus with workers committing their own writes.

## N1: cut DC1, which held the leader (dc1a)

| time | event |
|---|---|
| 15:16:49 | DC1 cut from the WAN |
| 15:17:01 | dc1a demotes itself: "DCS is not accessible and I was a leader" (12 s; its etcd member lost quorum) |
| 15:17:22 | dc2b is the leader (33 s after the cut) |
| 15:17:53 | the run drained in 14.4 s |

- **Nothing lost:** produced 288,953 = applied 288,953 (399 duplicates, all from redelivery), drained.
- **No cancelled commits:** 0 "canceling wait for synchronous replication" on the isolated leader. Its commits had at
  most one acknowledgement (dc1b), never two, so none was acknowledged before the cut and lost after it.
- `runtime_cross_source_blocking` FAIL: expected, every event waits during an outage, as in every failover drill.

## Rejoin after the heal: two lab findings, both fixed

1. **pg_rewind failed** on both DC1 nodes (they had WAL past the divergence point that the new leader never got). The
   WAL segment holding the last common checkpoint was already recycled, so they fell back to a full copy
   (`remove_data_directory_on_rewind_failure`). That is safe and automatic.
2. **The full copy waited ~14 minutes before copying a byte:** `pg_basebackup` asks for a *spread* checkpoint by
   default, and with checkpoint_timeout 15min it takes ~13.5 min (`pg_stat_progress_basebackup`: "waiting for
   checkpoint to finish"). The DC1 nodes were streaming again at 15:45, 18 min after the heal; the copy itself
   (5.3 GB) took under a minute. **Fix:** `basebackup: [checkpoint: fast]` in all three Patroni configs. Production
   needs it too: a rebuilt replica is otherwise a lost quorum member for up to checkpoint_timeout.

Also fixed: the drill held the partition for 10 minutes instead of 60 s (the loop counted 5-second sleeps as
half-seconds). The outcome doesn't depend on it: failover finished 33 s in, and the run had drained long before.
