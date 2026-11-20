# M1-mtsql-24k-pub1 — PASS ✅

- Broker: `MassTransit SQL transport (Postgres 127.0.0.1:5432/bench, schema transport), queue per group, partition key = source, PartitionedOrdered limit 1, connection limit 50, unlock delay 250 ms, max delivery 1000000`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.109s, closing 4.567s, drain 1.002s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 250112 | 250112 | 3748.631 | 3749.167 | 6153.8 | 0 | 0 | 0 | 93 | 1141 | 0 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| publish_call | 250112 | 8.239 | 7.923 | 11.487 | 16.311 | 27.823 | 253.311 |
| receive_call | 236097 | 84.451 | 40.927 | 240.767 | 456.703 | 494.847 | 503.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3749.167 | 4347.7 |
| applied_total | 133473.653 | 222588 |
| bench_cpu_cores | 3.392 | 3.636 |
| bench_cpu_pct | 14.132 | 15.15 |
| bench_gc_gen0 | 2.98 | 5 |
| bench_gc_gen1 | 2.102 | 4 |
| bench_gc_gen2 | 0.388 | 2 |
| bench_gc_heap_mb | 754.998 | 1092.2 |
| bench_mem_mb | 1140.357 | 1401 |
| bench_threadpool_queue | 13.388 | 72 |
| bench_threadpool_threads | 37.204 | 44 |
| broker_backlog | 191.673 | 254 |
| ctr_eb_pgbouncer_2_cpu_pct | 7.233 | 7.59 |
| ctr_eb_pgbouncer_2_mem_mb | 1.949 | 2.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 7.271 | 7.68 |
| ctr_eb_pgbouncer_3_mem_mb | 2.145 | 2.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 9.624 | 10.23 |
| ctr_eb_pgbouncer_4_mem_mb | 2.186 | 2.4 |
| ctr_eb_pgbouncer_cpu_pct | 8.292 | 8.68 |
| ctr_eb_pgbouncer_mem_mb | 2.333 | 2.5 |
| ctr_eb_postgres_cpu_pct | 1567.395 | 1598.52 |
| ctr_eb_postgres_mem_mb | 4230.192 | 4344.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.618 | 84.94 |
| in_flight | 161.306 | 206 |
| mt_error_or_dead_letter_rows | 0 | 0 |
| mt_faulted_total | 0 | 0 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.673 | 3 |
| pg_active_loads | 0.265 | 2 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 22.327 | 32 |
| pg_conn_idle | 87.653 | 98 |
| pg_conn_idle_in_tx | 15.408 | 24 |
| pg_conn_lock_wait | 2.122 | 10 |
| pg_conn_total | 125.388 | 126 |
| pg_db_size_mb | 1350.284 | 1531.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 38358.437 | 47336.4 |
| pg_tup_del_per_s | 17944.349 | 24107.3 |
| pg_tup_fetch_per_s | 143666.429 | 156210.7 |
| pg_tup_ins_per_s | 22721.016 | 26428.1 |
| pg_tup_upd_per_s | 9921.543 | 10809.4 |
| pg_wal_mb_per_s | 33.522 | 40.36 |
| pgb_cl_active | 18.714 | 23 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 1.388 | 4 |
| pgb_sv_idle | 13.898 | 18 |
| pgb_sv_used | 2.776 | 5 |
| produced_per_s | 3748.631 | 4354.7 |
| produced_total | 133634.959 | 222755 |
| publish_mb_per_s | 12.065 | 21.47 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 48,461 records are Completed |
| record_counts | pass | 48,461 records found, 48,461 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 93 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 250,112 events applied by workers, 250,112 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | pass | none waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 0 ms, max 0 ms) |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-000 | 14 | 1 | 82.228 | 19.615 | 427 | 0 | 219 | 219 | 0 | 0 | 0 | 0 |
| t-001 | 118 | 3 | 55.376 | 107.593 | 154 | 0 | 1270 | 1270 | 0 | 0 | 0 | 0 |
| t-002 | 17 | 1 | 154.669 | 44.04 | 377 | 0 | 445 | 445 | 0 | 0 | 0 | 0 |
| t-003 | 30 | 1 | 68.204 | 33.895 | 1633 | 0 | 350 | 350 | 0 | 0 | 0 | 0 |
| t-004 | 38 | 3 | 163.013 | 106.748 | 2178 | 0 | 1072 | 1072 | 0 | 0 | 0 | 0 |
| t-005 | 47 | 2 | 113.249 | 88.011 | 2385 | 0 | 913 | 913 | 0 | 0 | 0 | 0 |
| t-006 | 14 | 1 | 46.959 | 10.478 | 1170 | 0 | 119 | 119 | 0 | 0 | 0 | 0 |
| t-007 | 17 | 1 | 95.327 | 26.217 | 140 | 0 | 270 | 270 | 0 | 0 | 0 | 0 |
| t-008 | 11 | 1 | 138.353 | 24.947 | 279 | 0 | 262 | 262 | 0 | 0 | 0 | 0 |
| t-009 | 29 | 1 | 36.034 | 17.293 | 2407 | 0 | 218 | 218 | 0 | 0 | 0 | 0 |
| t-010 | 26 | 1 | 80.199 | 33.93 | 183 | 0 | 372 | 372 | 0 | 0 | 0 | 0 |
| t-011 | 38 | 2 | 80.666 | 53.84 | 1788 | 0 | 577 | 577 | 0 | 0 | 0 | 0 |
| t-012 | 6 | 1 | 115.634 | 11.982 | 556 | 0 | 121 | 121 | 0 | 0 | 0 | 0 |
| t-013 | 10 | 1 | 68.642 | 10.415 | 134 | 0 | 135 | 135 | 0 | 0 | 0 | 0 |
| t-014 | 34 | 1 | 58.925 | 32.015 | 214 | 0 | 359 | 359 | 0 | 0 | 0 | 0 |
| t-015 | 9 | 1 | 76.403 | 11.319 | 3462 | 0 | 140 | 140 | 0 | 0 | 0 | 0 |
| t-016 | 132 | 2 | 35.437 | 77.042 | 137 | 0 | 942 | 942 | 0 | 0 | 0 | 0 |
| t-017 | 19 | 1 | 55.734 | 17.263 | 830 | 0 | 192 | 192 | 0 | 0 | 0 | 0 |
| t-018 | 26 | 1 | 95.202 | 41.855 | 315 | 0 | 447 | 447 | 0 | 0 | 0 | 0 |
| t-019 | 38 | 3 | 165.768 | 104.717 | 208 | 0 | 1068 | 1068 | 0 | 0 | 0 | 0 |
| t-020 | 22 | 1 | 118.979 | 41.92 | 981 | 0 | 435 | 435 | 0 | 0 | 0 | 0 |
| t-021 | 19 | 1 | 30.401 | 10.112 | 3252 | 0 | 126 | 126 | 0 | 0 | 0 | 0 |
| t-022 | 63 | 2 | 56.525 | 59.55 | 162 | 0 | 685 | 685 | 0 | 0 | 0 | 0 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 573 | 573 | 0 | 0 | 0 | 0 |
| t-024 | 14 | 1 | 62.419 | 14.25 | 1743 | 0 | 167 | 167 | 0 | 0 | 0 | 0 |
