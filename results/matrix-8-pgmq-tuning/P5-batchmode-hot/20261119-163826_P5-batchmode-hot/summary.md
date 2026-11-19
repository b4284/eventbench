# P5-batchmode-hot — FAIL ❌

- Broker: `pgmq fifo via eb_read_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 45.12s, closing 0.534s, drain 12.21s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 619498 | 619498 | 12838.235 | 12727.526 | 16608.4 | 0 | 0 | 0 | 332 | 3150 | 189931 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 449318 | 2281.386 | 1251.327 | 6025.215 | 11444.223 | 15343.615 | 17825.791 |
| e2e_from_send | 449318 | 2262.687 | 1230.847 | 6008.831 | 11427.839 | 15343.615 | 17825.791 |
| producer_lag | 449318 | 18.701 | 12.695 | 32.335 | 154.751 | 297.727 | 505.855 |
| publish_call | 47950 | 30.237 | 22.319 | 37.215 | 203.647 | 777.727 | 1999.871 |
| queue_wait | 449318 | 2190.758 | 1129.471 | 5922.815 | 11370.495 | 15245.311 | 17825.791 |
| broker_dwell | 449318 | 2179.678 | 1118.207 | 5914.623 | 11362.303 | 15237.119 | 17809.407 |
| receive_call | 169782 | 22.416 | 20.655 | 31.055 | 54.207 | 114.047 | 1159.167 |
| ctx_load | 449318 | 19.784 | 19.087 | 27.007 | 41.567 | 65.663 | 164.735 |
| process | 449318 | 10.702 | 0.001 | 0.003 | 0.008 | 2627.583 | 3016.703 |
| ctx_commit | 449318 | 20.599 | 19.615 | 28.463 | 47.135 | 79.999 | 996.863 |
| service | 449318 | 71.928 | 47.519 | 111.103 | 184.959 | 2732.031 | 7086.079 |
| e2e_large | 232 | 4546.712 | 4130.815 | 7667.711 | 10395.647 | 10969.087 | 10969.087 |
| ready_wait | 449318 | 475.248 | 89.855 | 1492.991 | 4239.359 | 7356.415 | 13320.191 |
| blocked | 449318 | 407.13 | 67.839 | 1321.983 | 3700.735 | 6311.935 | 10985.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12727.526 | 13892.4 |
| applied_total | 343882.5 | 557273 |
| bench_cpu_cores | 2.784 | 3.136 |
| bench_cpu_pct | 11.598 | 13.07 |
| bench_gc_gen0 | 2.059 | 3 |
| bench_gc_gen1 | 1.441 | 3 |
| bench_gc_gen2 | 0.353 | 1 |
| bench_gc_heap_mb | 1422.012 | 1822.9 |
| bench_mem_mb | 1821.862 | 2021.4 |
| bench_threadpool_queue | 4.706 | 23 |
| bench_threadpool_threads | 40.294 | 44 |
| broker_backlog | 27636.059 | 30132 |
| broker_oldest_age_s | 12 | 16 |
| broker_visible | 25019.706 | 28088 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.966 | 31.55 |
| ctr_eb_pgbouncer_2_mem_mb | 14.888 | 15.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.79 | 31.81 |
| ctr_eb_pgbouncer_3_mem_mb | 15.215 | 18.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 31.048 | 32.2 |
| ctr_eb_pgbouncer_4_mem_mb | 17.974 | 19.1 |
| ctr_eb_pgbouncer_cpu_pct | 30.28 | 31.04 |
| ctr_eb_pgbouncer_mem_mb | 14.771 | 15.2 |
| ctr_eb_postgres_cpu_pct | 1431.199 | 1556.05 |
| ctr_eb_postgres_mem_mb | 10108.109 | 10213.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.629 | 89.62 |
| in_flight | 27992.088 | 30478 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.903 | 100 |
| pg_conn_active | 24.588 | 90 |
| pg_conn_idle | 180.882 | 198 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.353 | 4 |
| pg_conn_total | 205.471 | 206 |
| pg_db_size_mb | 2263.238 | 2672.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 34864.9 | 39961.9 |
| pg_tup_del_per_s | 38254.497 | 42927.7 |
| pg_tup_fetch_per_s | 557549.903 | 694097.2 |
| pg_tup_ins_per_s | 50052.726 | 59974 |
| pg_tup_upd_per_s | 33849.25 | 36735 |
| pg_wal_mb_per_s | 113.978 | 171.8 |
| pgb_cl_active | 396.147 | 410 |
| pgb_cl_waiting | 12.824 | 109 |
| pgb_maxwait_ms | 1.752 | 14.89 |
| pgb_sv_active | 25.647 | 51 |
| pgb_sv_idle | 25.559 | 39 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12838.235 | 13960.3 |
| produced_total | 371874.588 | 583939 |
| publish_mb_per_s | 44.409 | 71.21 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 94,797 records are Completed |
| record_counts | pass | 94,797 records found, 94,797 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 332 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 619,498 events applied by workers, 619,498 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 189,931 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 3701 ms, max 10981 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4581 seq 8: ready 127 ms before start, 127 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4587 seq 7: ready 117 ms before start, 117 ms of it with an idle slot in group 160
- [cross_source_blocking] source 13905 seq 28: ready 102 ms before start, 102 ms of it with an idle slot in group 441
- [cross_source_blocking] source 10814 seq 11: ready 124 ms before start, 124 ms of it with an idle slot in group 353
- [cross_source_blocking] source 13892 seq 31: ready 132 ms before start, 132 ms of it with an idle slot in group 441
- [cross_source_blocking] source 10781 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 353
- [cross_source_blocking] source 10813 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 353
- [cross_source_blocking] source 4557 seq 7: ready 110 ms before start, 110 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4575 seq 6: ready 107 ms before start, 107 ms of it with an idle slot in group 160
- [cross_source_blocking] source 1264 seq 5: ready 183 ms before start, 131 ms of it with an idle slot in group 40
- [cross_source_blocking] source 1203 seq 9: ready 177 ms before start, 132 ms of it with an idle slot in group 40
- [cross_source_blocking] source 1220 seq 6: ready 171 ms before start, 133 ms of it with an idle slot in group 40
- [cross_source_blocking] source 1269 seq 5: ready 162 ms before start, 134 ms of it with an idle slot in group 40
- [cross_source_blocking] source 13913 seq 22: ready 151 ms before start, 151 ms of it with an idle slot in group 441
- [cross_source_blocking] source 13889 seq 23: ready 157 ms before start, 157 ms of it with an idle slot in group 441
- [cross_source_blocking] source 13904 seq 25: ready 155 ms before start, 155 ms of it with an idle slot in group 441
- [cross_source_blocking] source 13899 seq 17: ready 154 ms before start, 154 ms of it with an idle slot in group 441
- [cross_source_blocking] source 4598 seq 11: ready 164 ms before start, 164 ms of it with an idle slot in group 160
- [cross_source_blocking] source 1232 seq 5: ready 177 ms before start, 172 ms of it with an idle slot in group 40
- [cross_source_blocking] source 8704 seq 10: ready 246 ms before start, 106 ms of it with an idle slot in group 302

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2224 | 2224 | 0 | 14286.847 | 17301.503 | 17825.791 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2049 | 2049 | 0 | 9764.863 | 13434.879 | 13697.023 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6835 | 6835 | 0 | 11206.655 | 13369.343 | 15597.567 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4528 | 4528 | 0 | 6488.063 | 12582.911 | 14876.671 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2212 | 2212 | 0 | 8585.215 | 12058.623 | 12189.695 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2323 | 2323 | 0 | 8912.895 | 11796.479 | 11927.551 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2205 | 2205 | 0 | 6848.511 | 11141.119 | 11272.191 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 73 | 73 | 0 | 4882.431 | 11010.047 | 11010.047 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4807 | 4807 | 0 | 4718.591 | 11010.047 | 11993.087 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2145 | 2145 | 0 | 9043.967 | 10878.975 | 11141.119 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4563 | 4563 | 0 | 7471.103 | 10747.903 | 12779.519 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 328 | 328 | 0 | 3457.023 | 10420.223 | 10485.759 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 9120 | 9120 | 0 | 8716.287 | 10289.151 | 11993.087 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1972 | 1972 | 0 | 3604.479 | 10158.079 | 10682.367 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2014 | 2014 | 0 | 7471.103 | 10027.007 | 10223.615 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2251 | 2251 | 0 | 6979.583 | 9830.399 | 10027.007 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2051 | 2051 | 0 | 5177.343 | 9568.255 | 10158.079 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4885 | 4885 | 0 | 6619.135 | 9502.719 | 11272.191 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1913 | 1913 | 0 | 4358.143 | 9502.719 | 9699.327 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 119 | 119 | 0 | 2588.671 | 9306.111 | 9437.183 |
| t-362 | 47 | 1 | 47.764 | 36.599 | 3235 | 0 | 1756 | 1756 | 0 | 4718.591 | 8978.431 | 9502.719 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1387 | 1387 | 0 | 4325.375 | 8781.823 | 9043.967 |
| t-104 | 46 | 1 | 31.882 | 24.451 | 2163 | 0 | 1215 | 1215 | 0 | 1417.215 | 8585.215 | 9306.111 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2291 | 2291 | 0 | 5373.951 | 8585.215 | 8847.359 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4530 | 4530 | 0 | 6029.311 | 8388.607 | 9306.111 |
