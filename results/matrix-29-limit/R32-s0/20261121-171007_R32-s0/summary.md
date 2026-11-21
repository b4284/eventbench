# R32-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 926 workers, offered 31870.178 ev/s
- Phases: run 90.061s, closing 1.449s, drain 7.747s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 612255 | 612255 | 10185.818 | 9408.896 | 18961.6 | 0 | 0 | 0 | 50 | 2973 | 261850 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 495579 | 3333.385 | 2381.823 | 7958.527 | 14704.639 | 19185.663 | 24248.319 |
| e2e_from_send | 495579 | 2088.719 | 644.095 | 6418.431 | 13058.047 | 16990.207 | 22331.391 |
| producer_lag | 495579 | 1244.663 | 556.543 | 3278.847 | 6295.551 | 7614.463 | 9994.239 |
| publish_call | 612255 | 110.818 | 90.367 | 213.759 | 398.079 | 507.903 | 859.135 |
| queue_wait | 495579 | 2024.155 | 577.023 | 6356.991 | 13000.703 | 16924.671 | 22216.703 |
| broker_dwell | 495579 | 1947.971 | 502.271 | 6283.263 | 12926.975 | 16859.135 | 22118.399 |
| receive_call | 517608 | 25.576 | 13.471 | 62.175 | 192.255 | 383.743 | 563.199 |
| ctx_load | 495579 | 16.58 | 11.471 | 34.879 | 88.639 | 157.183 | 207.487 |
| process | 495579 | 9.998 | 0.002 | 0.005 | 0.08 | 2605.055 | 3041.279 |
| ctx_commit | 495579 | 37.706 | 27.903 | 81.727 | 159.743 | 232.959 | 431.871 |
| ack | 495550 | 0.014 | 0.004 | 0.008 | 0.064 | 1.951 | 82.559 |
| service | 495579 | 64.565 | 44.895 | 110.271 | 214.655 | 2662.399 | 3254.271 |
| e2e_large | 35 | 4235.644 | 4288.511 | 6078.463 | 7622.655 | 7622.655 | 7622.655 |
| ready_wait | 495579 | 189.026 | 127.615 | 445.695 | 837.119 | 1437.695 | 2230.271 |
| blocked | 495579 | 159.595 | 110.719 | 375.807 | 659.967 | 910.335 | 1446.911 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9408.896 | 12150.1 |
| applied_total | 332535.102 | 552109 |
| bench_cpu_cores | 3.573 | 4.502 |
| bench_cpu_pct | 14.887 | 18.76 |
| bench_gc_gen0 | 1.694 | 4 |
| bench_gc_gen1 | 0.694 | 3 |
| bench_gc_gen2 | 0.49 | 2 |
| bench_gc_heap_mb | 682.9 | 1083.3 |
| bench_mem_mb | 1143.382 | 1390.3 |
| bench_threadpool_queue | 130.735 | 740 |
| bench_threadpool_threads | 40.061 | 44 |
| broker_backlog | 26248.98 | 44450 |
| ctr_eb_mosquitto_cpu_pct | 84.049 | 88.83 |
| ctr_eb_mosquitto_mem_mb | 11.957 | 24.1 |
| ctr_eb_mssql_cpu_pct | 4.268 | 4.64 |
| ctr_eb_mssql_mem_mb | 7474.9 | 7474.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 32.273 | 36.89 |
| ctr_eb_pgbouncer_2_mem_mb | 21.808 | 22.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 33.227 | 36.09 |
| ctr_eb_pgbouncer_3_mem_mb | 21.524 | 22.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 33.443 | 36.61 |
| ctr_eb_pgbouncer_4_mem_mb | 21.912 | 22.6 |
| ctr_eb_pgbouncer_cpu_pct | 33.826 | 36.53 |
| ctr_eb_pgbouncer_mem_mb | 21.808 | 23.1 |
| ctr_eb_postgres_cpu_pct | 1025.076 | 1060.46 |
| ctr_eb_postgres_mem_mb | 5912.229 | 6067.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.617 | 85.38 |
| in_flight | 18254.265 | 39185 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.12 | 2.24 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 671214.939 | 1125491 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 160576.429 | 261966 |
| octopus_mqtt_sent_total | 251193.673 | 413905 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 12.592 | 37 |
| pg_active_loads | 10.592 | 36 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 52.429 | 117 |
| pg_conn_idle | 211.02 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 7.265 | 33 |
| pg_conn_total | 263.449 | 264 |
| pg_db_size_mb | 3498.014 | 4026.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26087.529 | 30111.9 |
| pg_tup_del_per_s | 31642.7 | 36260.1 |
| pg_tup_fetch_per_s | 298655.173 | 326165.1 |
| pg_tup_ins_per_s | 41999.035 | 50037.6 |
| pg_tup_upd_per_s | 56490.418 | 64066.7 |
| pg_wal_mb_per_s | 121.879 | 189.63 |
| pgb_cl_active | 650.51 | 726 |
| pgb_cl_waiting | 45.408 | 396 |
| pgb_maxwait_ms | 2.414 | 8.57 |
| pgb_sv_active | 29.551 | 48 |
| pgb_sv_idle | 18.449 | 45 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 10185.818 | 13649.8 |
| produced_total | 350789.367 | 591294 |
| publish_mb_per_s | 23.305 | 34.14 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5012/5012 source cursors at the expected last sequence |
| records_complete | pass | all 187,233 records are Completed |
| record_counts | pass | 187,233 records found, 77,305 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 307 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 612,255 events applied by workers, 612,255 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 261,850 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 660 ms, max 1446 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2361 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 69
- [cross_source_blocking] source 6521 seq 23: ready 122 ms before start, 106 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3751 seq 13: ready 124 ms before start, 124 ms of it with an idle slot in group 126
- [cross_source_blocking] source 6588 seq 32: ready 107 ms before start, 107 ms of it with an idle slot in group 222
- [cross_source_blocking] source 13333 seq 10: ready 234 ms before start, 150 ms of it with an idle slot in group 426
- [cross_source_blocking] source 14936 seq 43: ready 234 ms before start, 220 ms of it with an idle slot in group 468
- [cross_source_blocking] source 8008 seq 9: ready 199 ms before start, 123 ms of it with an idle slot in group 270
- [cross_source_blocking] source 7152 seq 25: ready 221 ms before start, 166 ms of it with an idle slot in group 243
- [cross_source_blocking] source 6400 seq 15: ready 233 ms before start, 233 ms of it with an idle slot in group 219
- [cross_source_blocking] source 8411 seq 16: ready 198 ms before start, 167 ms of it with an idle slot in group 285
- [cross_source_blocking] source 913 seq 14: ready 234 ms before start, 234 ms of it with an idle slot in group 27
- [cross_source_blocking] source 6981 seq 19: ready 220 ms before start, 189 ms of it with an idle slot in group 237
- [cross_source_blocking] source 8785 seq 23: ready 112 ms before start, 112 ms of it with an idle slot in group 306
- [cross_source_blocking] source 13801 seq 43: ready 142 ms before start, 136 ms of it with an idle slot in group 438
- [cross_source_blocking] source 7005 seq 22: ready 122 ms before start, 118 ms of it with an idle slot in group 237
- [cross_source_blocking] source 13798 seq 34: ready 118 ms before start, 113 ms of it with an idle slot in group 438
- [cross_source_blocking] source 6524 seq 23: ready 113 ms before start, 107 ms of it with an idle slot in group 222
- [cross_source_blocking] source 5527 seq 25: ready 124 ms before start, 118 ms of it with an idle slot in group 189
- [cross_source_blocking] source 6173 seq 29: ready 208 ms before start, 208 ms of it with an idle slot in group 210
- [cross_source_blocking] source 4 seq 25: ready 208 ms before start, 160 ms of it with an idle slot in group 0

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 5 | 465.55 | 245.973 | 494 | 0 | 14480 | 14480 | 0 | 5275.647 | 21757.951 | 24248.319 |
| t-216 | 19 | 1 | 151.25 | 48.916 | 3889 | 0 | 2864 | 2864 | 0 | 6684.671 | 18481.151 | 22806.527 |
| t-186 | 34 | 3 | 257.702 | 145.354 | 1259 | 0 | 8554 | 8554 | 0 | 4489.215 | 18219.007 | 20578.303 |
| t-057 | 17 | 1 | 159.919 | 47.389 | 290 | 0 | 2939 | 2939 | 0 | 5144.575 | 17563.647 | 19136.511 |
| t-483 | 200 | 7 | 102.052 | 343.618 | 2470 | 0 | 20895 | 20895 | 0 | 4554.751 | 17301.503 | 22675.455 |
| t-147 | 79 | 10 | 360.93 | 484.666 | 1078 | 0 | 28519 | 28519 | 0 | 4489.215 | 16646.143 | 19005.439 |
| t-450 | 63 | 4 | 188.577 | 195.286 | 165 | 0 | 11552 | 11552 | 0 | 4521.983 | 16515.071 | 19398.655 |
| t-309 | 56 | 1 | 54.069 | 49.865 | 3528 | 0 | 3085 | 3085 | 0 | 3145.727 | 16252.927 | 20316.159 |
| t-438 | 121 | 8 | 185.216 | 387.804 | 1839 | 0 | 23188 | 23188 | 0 | 4947.967 | 16252.927 | 19398.655 |
| t-168 | 71 | 2 | 85.848 | 99.682 | 155 | 0 | 6073 | 6073 | 0 | 4718.591 | 16187.391 | 19136.511 |
| t-465 | 66 | 4 | 175.436 | 189.292 | 667 | 0 | 11291 | 11291 | 0 | 3637.247 | 16187.391 | 19660.799 |
| t-468 | 61 | 7 | 315.031 | 315.09 | 1645 | 0 | 18526 | 18526 | 0 | 3145.727 | 16121.855 | 22413.311 |
| t-417 | 75 | 3 | 109.303 | 135.853 | 3449 | 0 | 8311 | 8311 | 0 | 4292.607 | 15728.639 | 19005.439 |
| t-348 | 125 | 7 | 156.249 | 327.495 | 1340 | 0 | 19521 | 19521 | 0 | 3489.791 | 15466.495 | 19398.655 |
| t-315 | 88 | 5 | 155.915 | 228.351 | 2927 | 0 | 13499 | 13499 | 0 | 3276.799 | 15400.959 | 19791.871 |
| t-378 | 55 | 4 | 212.953 | 190.794 | 3659 | 0 | 11482 | 11482 | 0 | 3702.783 | 15269.887 | 17170.431 |
| t-474 | 63 | 1 | 45.686 | 47.126 | 375 | 0 | 2895 | 2895 | 0 | 3948.543 | 15073.279 | 18087.935 |
| t-132 | 22 | 3 | 374.037 | 127.192 | 2676 | 0 | 7371 | 7371 | 0 | 3735.551 | 15007.743 | 17301.503 |
| t-306 | 134 | 9 | 186.365 | 416.752 | 2978 | 0 | 24990 | 24990 | 0 | 3162.111 | 14745.599 | 17563.647 |
| t-441 | 30 | 4 | 350.391 | 169.932 | 2203 | 0 | 10262 | 10262 | 0 | 3702.783 | 14352.383 | 17039.359 |
| t-402 | 31 | 1 | 84.797 | 44.597 | 1454 | 0 | 2744 | 2744 | 0 | 3751.935 | 14024.703 | 15007.743 |
| t-231 | 43 | 2 | 124.93 | 86.223 | 156 | 0 | 5211 | 5211 | 0 | 2752.511 | 13959.167 | 20709.375 |
| t-285 | 43 | 2 | 129.746 | 92.478 | 233 | 0 | 5541 | 5541 | 0 | 3096.575 | 13762.559 | 15794.175 |
| t-234 | 21 | 1 | 145.87 | 49.69 | 141 | 0 | 2914 | 2914 | 0 | 2752.511 | 13697.023 | 17170.431 |
| t-222 | 138 | 8 | 153.743 | 357.819 | 2846 | 0 | 21373 | 21373 | 0 | 3047.423 | 13631.487 | 17563.647 |
