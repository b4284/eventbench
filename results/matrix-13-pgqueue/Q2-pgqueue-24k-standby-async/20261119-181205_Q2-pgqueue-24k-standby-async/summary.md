# Q2-pgqueue-24k-standby-async — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:6432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.154s, closing 0.949s, drain 85.127s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1486875 | 1486875 | 24018.841 | 10816.598 | 27029.4 | 0 | 0 | 0 | 425 | 7511 | 880183 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1201449 | 34338.962 | 30638.079 | 81854.463 | 86376.447 | 88276.991 | 92405.759 |
| e2e_from_send | 1201449 | 34234.318 | 30408.703 | 81788.927 | 86310.911 | 88211.455 | 92405.759 |
| producer_lag | 1201449 | 104.605 | 2.547 | 369.407 | 1158.143 | 1764.351 | 2181.119 |
| publish_call | 511156 | 2.894 | 1.115 | 3.567 | 13.831 | 359.167 | 1446.911 |
| queue_wait | 1201449 | 34196.535 | 30375.935 | 81788.927 | 86310.911 | 88211.455 | 92078.079 |
| broker_dwell | 1201449 | 34131.701 | 30228.479 | 81723.391 | 86310.911 | 88211.455 | 92078.079 |
| receive_call | 851588 | 96.651 | 59.967 | 275.199 | 444.159 | 494.079 | 519.679 |
| ctx_load | 1201449 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 4.009 |
| process | 1201449 | 10.391 | 0.001 | 0.002 | 0.004 | 2613.247 | 3004.415 |
| ctx_commit | 1201449 | 9.246 | 4.727 | 10.335 | 136.191 | 266.495 | 2146.303 |
| service | 1201449 | 37.792 | 13.031 | 42.175 | 366.079 | 2654.207 | 4579.327 |
| e2e_large | 324 | 3060.286 | 2906.111 | 4562.943 | 7045.119 | 7938.047 | 7938.047 |
| ready_wait | 1201449 | 1298.045 | 421.119 | 3395.583 | 12189.695 | 25952.255 | 64094.207 |
| blocked | 1201449 | 1297.987 | 421.119 | 3395.583 | 12189.695 | 25952.255 | 64094.207 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10816.598 | 24198.4 |
| applied_total | 634906.245 | 797810 |
| bench_cpu_cores | 2.36 | 4.087 |
| bench_cpu_pct | 9.833 | 17.03 |
| bench_gc_gen0 | 2 | 5 |
| bench_gc_gen1 | 1.02 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 738.41 | 908.7 |
| bench_mem_mb | 1058.01 | 1170.9 |
| bench_threadpool_queue | 11.551 | 161 |
| bench_threadpool_threads | 34.49 | 37 |
| broker_backlog | 208583.857 | 590561 |
| ctr_eb_pg_standby_cpu_pct | 87.565 | 145.91 |
| ctr_eb_pg_standby_mem_mb | 2143.759 | 2685.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 18.106 | 29.02 |
| ctr_eb_pgbouncer_2_mem_mb | 11.888 | 13.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 13.99 | 23.44 |
| ctr_eb_pgbouncer_3_mem_mb | 11.645 | 13 |
| ctr_eb_pgbouncer_4_cpu_pct | 8.343 | 14.35 |
| ctr_eb_pgbouncer_4_mem_mb | 9.831 | 10.3 |
| ctr_eb_pgbouncer_cpu_pct | 15.874 | 27.54 |
| ctr_eb_pgbouncer_mem_mb | 12.459 | 13.3 |
| ctr_eb_postgres_cpu_pct | 756.841 | 1101.04 |
| ctr_eb_postgres_mem_mb | 6794.892 | 7732.4 |
| dispatch_empty_reads_per_s | 1183.176 | 9446.5 |
| dispatch_fetch_calls_per_s | 116.829 | 625 |
| dispatch_group_reads_per_s | 5738.198 | 23837.8 |
| dispatch_in_flight | 288.592 | 753 |
| dispatch_notifies_per_s | 20480.855 | 23900.1 |
| dispatch_preloaded_per_s | 11881.047 | 24081.5 |
| dispatch_rows_per_s | 11881.28 | 24081.5 |
| dispatch_waiting_receives | 764.959 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.585 | 66.83 |
| in_flight | 229857.449 | 648269 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.082 | 14 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.578 | 100 |
| pg_conn_active | 25.531 | 52 |
| pg_conn_idle | 169.592 | 191 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.143 | 28 |
| pg_conn_total | 195.122 | 197 |
| pg_db_size_mb | 3462.655 | 4526.3 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 577.64 | 1173.7 |
| pg_replica_replay_lag_ms | 4207.788 | 7369.5 |
| pg_replicas | 1.041 | 2 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 10608.786 | 45973.7 |
| pg_tup_del_per_s | 35368.143 | 73396.7 |
| pg_tup_fetch_per_s | 227927.055 | 613933.3 |
| pg_tup_ins_per_s | 66990.765 | 91553.1 |
| pg_tup_upd_per_s | 20597.778 | 43611.6 |
| pg_wal_mb_per_s | 119.299 | 165.59 |
| pgb_cl_active | 13.306 | 18 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 7.286 | 13 |
| pgb_sv_idle | 6.02 | 13 |
| pgb_sv_used | 34.694 | 40 |
| produced_per_s | 24018.841 | 33139.4 |
| produced_total | 864763.694 | 1446079 |
| publish_mb_per_s | 63.869 | 104.77 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,835 records are Completed |
| record_counts | pass | 193,835 records found, 193,835 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,486,875 events applied by workers, 1,486,875 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 880,183 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 12190 ms, max 64065 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 835 seq 15: ready 118 ms before start, 112 ms of it with an idle slot in group 23
- [cross_source_blocking] source 812 seq 7: ready 105 ms before start, 105 ms of it with an idle slot in group 23
- [cross_source_blocking] source 819 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 23
- [cross_source_blocking] source 844 seq 9: ready 114 ms before start, 114 ms of it with an idle slot in group 23
- [cross_source_blocking] source 6972 seq 11: ready 102 ms before start, 102 ms of it with an idle slot in group 237
- [cross_source_blocking] source 10881 seq 8: ready 109 ms before start, 109 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10846 seq 18: ready 113 ms before start, 113 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10824 seq 8: ready 128 ms before start, 128 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10834 seq 22: ready 135 ms before start, 135 ms of it with an idle slot in group 354
- [cross_source_blocking] source 9068 seq 11: ready 104 ms before start, 104 ms of it with an idle slot in group 313
- [cross_source_blocking] source 9293 seq 28: ready 150 ms before start, 150 ms of it with an idle slot in group 320
- [cross_source_blocking] source 10864 seq 17: ready 109 ms before start, 109 ms of it with an idle slot in group 354
- [cross_source_blocking] source 9291 seq 27: ready 167 ms before start, 167 ms of it with an idle slot in group 320
- [cross_source_blocking] source 9288 seq 30: ready 222 ms before start, 222 ms of it with an idle slot in group 320
- [cross_source_blocking] source 12913 seq 25: ready 106 ms before start, 106 ms of it with an idle slot in group 409
- [cross_source_blocking] source 1132 seq 48: ready 102 ms before start, 102 ms of it with an idle slot in group 37
- [cross_source_blocking] source 12911 seq 27: ready 106 ms before start, 106 ms of it with an idle slot in group 409
- [cross_source_blocking] source 9298 seq 35: ready 151 ms before start, 151 ms of it with an idle slot in group 320
- [cross_source_blocking] source 5300 seq 26: ready 102 ms before start, 102 ms of it with an idle slot in group 182
- [cross_source_blocking] source 9296 seq 28: ready 217 ms before start, 217 ms of it with an idle slot in group 320

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15088 | 15088 | 0 | 68681.727 | 89128.959 | 92274.687 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3187 | 3187 | 0 | 66584.575 | 89128.959 | 90177.535 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3100 | 3100 | 0 | 56623.103 | 88604.671 | 90701.823 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 15028 | 15028 | 0 | 58982.399 | 88604.671 | 91226.111 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9090 | 9090 | 0 | 64487.423 | 88080.383 | 89653.247 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9129 | 9129 | 0 | 64749.567 | 88080.383 | 89653.247 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5766 | 5766 | 0 | 55574.527 | 87556.095 | 89653.247 |
| t-172 | 20 | 1 | 137.561 | 48.341 | 2589 | 0 | 2972 | 2972 | 0 | 53477.375 | 87031.807 | 88080.383 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 11305 | 11305 | 0 | 55574.527 | 87031.807 | 89128.959 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9130 | 9130 | 0 | 64225.279 | 87031.807 | 88604.671 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 6149 | 6149 | 0 | 61341.695 | 87031.807 | 88604.671 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17980 | 17980 | 0 | 62914.559 | 87031.807 | 92798.975 |
| t-230 | 30 | 2 | 195.135 | 96.607 | 1033 | 0 | 5825 | 5825 | 0 | 51904.511 | 86507.519 | 88604.671 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 5942 | 5942 | 0 | 59244.543 | 86507.519 | 89128.959 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 3086 | 3086 | 0 | 60030.975 | 86507.519 | 88080.383 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30748 | 30748 | 0 | 61603.839 | 86507.519 | 89128.959 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 19487 | 19487 | 0 | 61865.983 | 86507.519 | 89128.959 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 8497 | 8497 | 0 | 59506.687 | 86507.519 | 88604.671 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29447 | 29447 | 0 | 62652.415 | 85983.231 | 88604.671 |
| t-460 | 71 | 1 | 38.387 | 45.799 | 141 | 0 | 3056 | 3056 | 0 | 62652.415 | 85983.231 | 87556.095 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 3059 | 3059 | 0 | 52690.943 | 85458.943 | 87556.095 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 8516 | 8516 | 0 | 56885.247 | 85458.943 | 87031.807 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8821 | 8821 | 0 | 60293.119 | 85458.943 | 87556.095 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15220 | 15220 | 0 | 60555.263 | 85458.943 | 88080.383 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6483 | 6483 | 0 | 50331.647 | 84934.655 | 87556.095 |
