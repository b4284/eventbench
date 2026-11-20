# M2-mtsql-24k-pub1-conc — FAIL ❌

- Broker: `MassTransit SQL transport (Postgres 127.0.0.1:5432/bench, schema transport), queue per group, partition key = source, PartitionedOrdered limit 1, connection limit 50, unlock delay 250 ms, max delivery 1000000`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.696s, closing 4.614s, drain 55.316s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 314904 | 314904 | 5552.947 | 2911.514 | 13207.8 | 0 | 0 | 0 | 117 | 1491 | 29872 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 37870 | 93297.265 | 100073.471 | 103022.591 | 106364.927 | 108068.863 | 109641.727 |
| e2e_from_send | 37870 | 46706.912 | 53182.463 | 56655.871 | 59867.135 | 61800.447 | 64454.655 |
| producer_lag | 37870 | 46590.407 | 46628.863 | 48070.655 | 48562.175 | 48857.087 | 49217.535 |
| publish_call | 314904 | 371.058 | 230.399 | 263.423 | 401.663 | 24575.999 | 25018.367 |
| queue_wait | 37870 | 46658.238 | 53182.463 | 56623.103 | 59834.367 | 61702.143 | 64421.887 |
| broker_dwell | 37870 | 46657.457 | 53182.463 | 56623.103 | 59834.367 | 61702.143 | 64421.887 |
| receive_call | 270730 | 81.695 | 12.007 | 237.823 | 421.631 | 495.871 | 14254.079 |
| ctx_load | 37870 | 1.336 | 0.898 | 2.965 | 6.459 | 13.143 | 46.687 |
| process | 37870 | 12.262 | 0.001 | 0.002 | 0.007 | 2631.679 | 16523.263 |
| ctx_commit | 37870 | 34.629 | 1.642 | 5.067 | 204.799 | 10526.719 | 23625.727 |
| ack | 26229 | 0.002 | 0.001 | 0.002 | 0.004 | 0.054 | 1.332 |
| service | 37870 | 48.675 | 3.321 | 7.967 | 1010.175 | 11321.343 | 25001.983 |
| e2e_large | 16 | 49672.192 | 49741.823 | 51118.079 | 51838.975 | 51838.975 | 51838.975 |
| ready_wait | 37870 | 1965.628 | 335.871 | 2818.047 | 41746.431 | 52199.423 | 61210.623 |
| blocked | 37870 | 1964.811 | 335.871 | 2818.047 | 41713.663 | 52199.423 | 61210.623 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2911.514 | 4627.2 |
| applied_total | 54309.216 | 148175 |
| bench_cpu_cores | 3.043 | 5.011 |
| bench_cpu_pct | 12.679 | 20.88 |
| bench_gc_gen0 | 2.333 | 6 |
| bench_gc_gen1 | 2.137 | 6 |
| bench_gc_gen2 | 0.569 | 3 |
| bench_gc_heap_mb | 895.747 | 1810.2 |
| bench_mem_mb | 1303.782 | 2368.4 |
| bench_threadpool_queue | 860.412 | 4370 |
| bench_threadpool_threads | 93.529 | 108 |
| broker_backlog | 51204.902 | 136185 |
| ctr_eb_pgbouncer_2_cpu_pct | 5.365 | 8.95 |
| ctr_eb_pgbouncer_2_mem_mb | 4.224 | 5.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 6.11 | 10.38 |
| ctr_eb_pgbouncer_3_mem_mb | 4.314 | 4.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 5.617 | 9.46 |
| ctr_eb_pgbouncer_4_mem_mb | 5.437 | 5.5 |
| ctr_eb_pgbouncer_cpu_pct | 4.733 | 8.01 |
| ctr_eb_pgbouncer_mem_mb | 4.392 | 4.7 |
| ctr_eb_postgres_cpu_pct | 801.351 | 1281.98 |
| ctr_eb_postgres_mem_mb | 11955.886 | 12235.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 51.701 | 74.2 |
| in_flight | 53833.961 | 137680 |
| mt_error_or_dead_letter_rows | 0 | 0 |
| mt_faulted_total | 0 | 0 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.765 | 3 |
| pg_active_loads | 0.176 | 1 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.775 | 100 |
| pg_conn_active | 32.765 | 54 |
| pg_conn_idle | 203.608 | 218 |
| pg_conn_idle_in_tx | 8.686 | 23 |
| pg_conn_lock_wait | 22.157 | 44 |
| pg_conn_total | 245.059 | 246 |
| pg_db_size_mb | 3435.929 | 4083.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22382.114 | 38395.6 |
| pg_tup_del_per_s | 14247.637 | 29373.3 |
| pg_tup_fetch_per_s | 250629.382 | 623976.8 |
| pg_tup_ins_per_s | 27832.386 | 48724.1 |
| pg_tup_upd_per_s | 6775.82 | 11511.7 |
| pg_wal_mb_per_s | 39.625 | 73.19 |
| pgb_cl_active | 35.02 | 45 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0.176 | 2 |
| pgb_sv_idle | 22.882 | 41 |
| pgb_sv_used | 24.941 | 40 |
| produced_per_s | 5552.947 | 9556 |
| produced_total | 108143.176 | 285855 |
| publish_mb_per_s | 17.415 | 37.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 57,151 records are Completed |
| record_counts | pass | 57,151 records found, 57,151 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 117 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 314,904 events applied by workers, 314,904 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 29,872 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 41714 ms, max 61208 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4476 seq 9: ready 168 ms before start, 168 ms of it with an idle slot in group 154
- [cross_source_blocking] source 7260 seq 9: ready 175 ms before start, 175 ms of it with an idle slot in group 248
- [cross_source_blocking] source 13212 seq 31: ready 305 ms before start, 305 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9212 seq 6: ready 104 ms before start, 104 ms of it with an idle slot in group 316
- [cross_source_blocking] source 4796 seq 13: ready 242 ms before start, 242 ms of it with an idle slot in group 167
- [cross_source_blocking] source 5052 seq 13: ready 243 ms before start, 243 ms of it with an idle slot in group 176
- [cross_source_blocking] source 2556 seq 8: ready 150 ms before start, 150 ms of it with an idle slot in group 76
- [cross_source_blocking] source 3292 seq 7: ready 142 ms before start, 142 ms of it with an idle slot in group 110
- [cross_source_blocking] source 4188 seq 15: ready 328 ms before start, 328 ms of it with an idle slot in group 138
- [cross_source_blocking] source 15612 seq 7: ready 511 ms before start, 511 ms of it with an idle slot in group 485
- [cross_source_blocking] source 13212 seq 32: ready 426 ms before start, 426 ms of it with an idle slot in group 421
- [cross_source_blocking] source 4284 seq 8: ready 216 ms before start, 216 ms of it with an idle slot in group 146
- [cross_source_blocking] source 4540 seq 18: ready 238 ms before start, 238 ms of it with an idle slot in group 159
- [cross_source_blocking] source 5763 seq 6: ready 159 ms before start, 159 ms of it with an idle slot in group 199
- [cross_source_blocking] source 2979 seq 8: ready 258 ms before start, 258 ms of it with an idle slot in group 98
- [cross_source_blocking] source 2570 seq 9: ready 156 ms before start, 156 ms of it with an idle slot in group 78
- [cross_source_blocking] source 4195 seq 7: ready 158 ms before start, 158 ms of it with an idle slot in group 139
- [cross_source_blocking] source 451 seq 21: ready 172 ms before start, 172 ms of it with an idle slot in group 15
- [cross_source_blocking] source 6659 seq 8: ready 206 ms before start, 206 ms of it with an idle slot in group 225
- [cross_source_blocking] source 8266 seq 9: ready 270 ms before start, 270 ms of it with an idle slot in group 280

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 1449 | 1449 | 0 | 107479.039 | 109576.191 | 110100.479 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 1914 | 1914 | 0 | 105381.887 | 109051.903 | 109051.903 |
| t-104 | 46 | 1 | 59.619 | 45.723 | 2163 | 0 | 591 | 591 | 0 | 108003.327 | 108527.615 | 108527.615 |
| t-005 | 47 | 2 | 113.249 | 88.011 | 2385 | 0 | 1178 | 1178 | 0 | 106430.463 | 108003.327 | 108003.327 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 684 | 684 | 0 | 106954.751 | 108003.327 | 108003.327 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 2381 | 2381 | 0 | 105906.175 | 106954.751 | 106954.751 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 3750 | 3750 | 0 | 103284.735 | 106954.751 | 107479.039 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 6136 | 6136 | 0 | 103284.735 | 106430.463 | 106430.463 |
| t-020 | 22 | 1 | 118.979 | 41.92 | 981 | 0 | 542 | 542 | 0 | 104857.599 | 105906.175 | 105906.175 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 2847 | 2847 | 0 | 104333.311 | 105906.175 | 106430.463 |
| t-222 | 138 | 6 | 115.927 | 269.807 | 2846 | 0 | 3489 | 3489 | 0 | 103284.735 | 105906.175 | 106430.463 |
| t-235 | 54 | 3 | 148.429 | 133.613 | 527 | 0 | 1611 | 1611 | 0 | 103284.735 | 105906.175 | 105906.175 |
| t-237 | 74 | 2 | 67.368 | 84.407 | 2944 | 0 | 1158 | 1158 | 0 | 104857.599 | 105906.175 | 105906.175 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 1880 | 1880 | 0 | 103284.735 | 105906.175 | 105906.175 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 3901 | 3901 | 0 | 103284.735 | 105381.887 | 105906.175 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 1246 | 1246 | 0 | 104333.311 | 105381.887 | 105906.175 |
| t-473 | 32 | 1 | 78.937 | 42.88 | 4074 | 0 | 628 | 628 | 0 | 102236.159 | 105381.887 | 105381.887 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 595 | 595 | 0 | 104333.311 | 104857.599 | 104857.599 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 3086 | 3086 | 0 | 102236.159 | 104857.599 | 105381.887 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 1338 | 1338 | 0 | 103284.735 | 104857.599 | 104857.599 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 3205 | 3205 | 0 | 103284.735 | 104857.599 | 105906.175 |
| t-075 | 37 | 2 | 141.157 | 87.188 | 232 | 0 | 1093 | 1093 | 0 | 103809.023 | 104333.311 | 104333.311 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 1176 | 1176 | 0 | 102236.159 | 104333.311 | 104857.599 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 4552 | 4552 | 0 | 101187.583 | 104333.311 | 108003.327 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 630 | 630 | 0 | 103284.735 | 104333.311 | 104333.311 |
