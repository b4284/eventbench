# R20-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 713 workers, offered 19922.093 ev/s
- Phases: run 89.432s, closing 0.645s, drain 2.97s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 396738 | 396738 | 6365.6 | 6361.541 | 10804.6 | 0 | 0 | 0 | 48 | 1944 | 6809 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 318920 | 131.813 | 81.151 | 190.847 | 1705.983 | 2924.543 | 5218.303 |
| e2e_from_send | 318920 | 124.017 | 75.903 | 172.543 | 1696.767 | 2910.207 | 5210.111 |
| producer_lag | 318920 | 7.796 | 2.191 | 17.871 | 92.223 | 275.967 | 947.711 |
| publish_call | 396738 | 50.683 | 35.679 | 98.495 | 296.703 | 491.263 | 987.135 |
| queue_wait | 318920 | 87.15 | 48.031 | 130.559 | 1251.327 | 2648.063 | 4575.231 |
| broker_dwell | 318920 | 62.405 | 24.127 | 92.223 | 1221.631 | 2621.439 | 4562.943 |
| receive_call | 354208 | 36.398 | 20.191 | 86.079 | 267.519 | 453.375 | 573.439 |
| ctx_load | 318920 | 9.13 | 6.383 | 18.543 | 49.087 | 89.343 | 140.671 |
| process | 318920 | 10.173 | 0.002 | 0.005 | 0.081 | 2615.295 | 3004.415 |
| ctx_commit | 318920 | 17.421 | 11.479 | 38.047 | 93.887 | 134.143 | 326.399 |
| ack | 318907 | 0.012 | 0.004 | 0.009 | 0.066 | 1.266 | 78.463 |
| service | 318920 | 36.868 | 20.543 | 54.335 | 128.383 | 2641.919 | 3139.583 |
| e2e_large | 37 | 3067.876 | 3055.615 | 3954.687 | 4624.383 | 4624.383 | 4624.383 |
| ready_wait | 318920 | 22.197 | 12.231 | 54.911 | 141.183 | 282.111 | 1224.703 |
| blocked | 318920 | 21.072 | 12.087 | 52.191 | 127.167 | 232.575 | 1142.783 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6361.541 | 7290.3 |
| applied_total | 228759 | 381387 |
| bench_cpu_cores | 3.257 | 3.998 |
| bench_cpu_pct | 13.569 | 16.66 |
| bench_gc_gen0 | 1.408 | 3 |
| bench_gc_gen1 | 0.633 | 2 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 558.38 | 744.6 |
| bench_mem_mb | 961.843 | 1179.6 |
| bench_threadpool_queue | 31.51 | 137 |
| bench_threadpool_threads | 38.673 | 44 |
| broker_backlog | 5457.857 | 7703 |
| ctr_eb_mosquitto_cpu_pct | 89.689 | 92.57 |
| ctr_eb_mosquitto_mem_mb | 12.535 | 20.4 |
| ctr_eb_mssql_cpu_pct | 4.152 | 4.97 |
| ctr_eb_mssql_mem_mb | 7473.9 | 7473.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 29.489 | 33.05 |
| ctr_eb_pgbouncer_2_mem_mb | 17.316 | 17.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 29.09 | 31.75 |
| ctr_eb_pgbouncer_3_mem_mb | 19.506 | 21.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 28.517 | 31.75 |
| ctr_eb_pgbouncer_4_mem_mb | 17.608 | 18.1 |
| ctr_eb_pgbouncer_cpu_pct | 31.367 | 33.79 |
| ctr_eb_pgbouncer_mem_mb | 17.822 | 18.3 |
| ctr_eb_postgres_cpu_pct | 964.81 | 1029.58 |
| ctr_eb_postgres_mem_mb | 5803.018 | 5933.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.992 | 82.62 |
| in_flight | 506.367 | 1116 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.319 | 2.45 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 453674.714 | 760485 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 165503.551 | 265101 |
| octopus_mqtt_sent_total | 211030.082 | 353821 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.306 | 36 |
| pg_active_loads | 7.612 | 23 |
| pg_active_polls | 0.061 | 1 |
| pg_active_sends | 0.061 | 1 |
| pg_cache_hit_pct | 99.997 | 100 |
| pg_conn_active | 24.857 | 71 |
| pg_conn_idle | 236.816 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.49 | 25 |
| pg_conn_total | 261.673 | 263 |
| pg_db_size_mb | 3580.414 | 4040.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24133.973 | 26314.4 |
| pg_tup_del_per_s | 31906.649 | 36121.4 |
| pg_tup_fetch_per_s | 247659.547 | 266078.6 |
| pg_tup_ins_per_s | 43744.045 | 49912.5 |
| pg_tup_upd_per_s | 48152 | 52223.2 |
| pg_wal_mb_per_s | 118.08 | 187.85 |
| pgb_cl_active | 466.49 | 508 |
| pgb_cl_waiting | 2.857 | 35 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 16.694 | 48 |
| pgb_sv_idle | 31.306 | 46 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6365.6 | 7002 |
| produced_total | 229265.367 | 381884 |
| publish_mb_per_s | 15.415 | 26.55 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5012/5012 source cursors at the expected last sequence |
| records_complete | pass | all 158,223 records are Completed |
| record_counts | pass | 158,223 records found, 53,150 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 441 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 396,738 events applied by workers, 396,738 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 6,809 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 127 ms, max 1143 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8429 seq 19: ready 100 ms before start, 100 ms of it with an idle slot in group 285
- [cross_source_blocking] source 14900 seq 28: ready 110 ms before start, 110 ms of it with an idle slot in group 468
- [cross_source_blocking] source 8469 seq 9: ready 106 ms before start, 106 ms of it with an idle slot in group 288
- [cross_source_blocking] source 712 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 21
- [cross_source_blocking] source 15548 seq 10: ready 106 ms before start, 106 ms of it with an idle slot in group 483
- [cross_source_blocking] source 6559 seq 20: ready 126 ms before start, 126 ms of it with an idle slot in group 222
- [cross_source_blocking] source 14905 seq 27: ready 105 ms before start, 105 ms of it with an idle slot in group 468
- [cross_source_blocking] source 9143 seq 10: ready 127 ms before start, 127 ms of it with an idle slot in group 315
- [cross_source_blocking] source 3313 seq 21: ready 129 ms before start, 129 ms of it with an idle slot in group 111
- [cross_source_blocking] source 2957 seq 10: ready 130 ms before start, 130 ms of it with an idle slot in group 96
- [cross_source_blocking] source 6343 seq 14: ready 137 ms before start, 134 ms of it with an idle slot in group 216
- [cross_source_blocking] source 4352 seq 44: ready 118 ms before start, 118 ms of it with an idle slot in group 147
- [cross_source_blocking] source 10032 seq 37: ready 113 ms before start, 113 ms of it with an idle slot in group 342
- [cross_source_blocking] source 8851 seq 29: ready 112 ms before start, 112 ms of it with an idle slot in group 306
- [cross_source_blocking] source 6062 seq 7: ready 110 ms before start, 110 ms of it with an idle slot in group 207
- [cross_source_blocking] source 3239 seq 22: ready 109 ms before start, 104 ms of it with an idle slot in group 108
- [cross_source_blocking] source 11752 seq 18: ready 128 ms before start, 128 ms of it with an idle slot in group 381
- [cross_source_blocking] source 8770 seq 24: ready 106 ms before start, 106 ms of it with an idle slot in group 306
- [cross_source_blocking] source 14912 seq 42: ready 107 ms before start, 107 ms of it with an idle slot in group 468
- [cross_source_blocking] source 4351 seq 46: ready 112 ms before start, 112 ms of it with an idle slot in group 147
- [cross_source_blocking] source 158 seq 12: ready 129 ms before start, 129 ms of it with an idle slot in group 3

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2473.983 | 4620.287 | 4653.055 |
| t-132 | 22 | 2 | 233.773 | 79.495 | 2676 | 0 | 4865 | 4865 | 0 | 96.767 | 2932.735 | 3981.311 |
| t-288 | 6 | 1 | 89.99 | 9.103 | 214 | 0 | 557 | 557 | 0 | 77.823 | 2768.895 | 3014.655 |
| t-381 | 11 | 1 | 72.887 | 13.701 | 134 | 0 | 860 | 860 | 0 | 79.359 | 2605.055 | 2932.735 |
| t-330 | 9 | 1 | 72.161 | 10.047 | 2607 | 0 | 665 | 665 | 0 | 77.311 | 2457.599 | 3670.015 |
| t-441 | 30 | 3 | 218.994 | 106.208 | 2203 | 0 | 6335 | 6335 | 0 | 89.087 | 2457.599 | 4358.143 |
| t-018 | 26 | 1 | 78.911 | 34.693 | 315 | 0 | 2127 | 2127 | 0 | 86.527 | 2293.759 | 3063.807 |
| t-447 | 17 | 1 | 70.217 | 18.949 | 558 | 0 | 1221 | 1221 | 0 | 79.871 | 2277.375 | 3080.191 |
| t-462 | 10 | 1 | 39.763 | 6.452 | 2006 | 0 | 438 | 438 | 0 | 68.607 | 2277.375 | 2998.271 |
| t-009 | 29 | 1 | 29.868 | 14.334 | 2407 | 0 | 953 | 953 | 0 | 76.287 | 2195.455 | 3178.495 |
| t-069 | 31 | 1 | 21.971 | 10.944 | 2002 | 0 | 734 | 734 | 0 | 72.191 | 2195.455 | 3063.807 |
| t-246 | 32 | 4 | 290.969 | 153.733 | 494 | 0 | 9356 | 9356 | 0 | 88.063 | 2162.687 | 4718.591 |
| t-456 | 21 | 2 | 222.967 | 75.931 | 512 | 0 | 4591 | 4591 | 0 | 84.479 | 2162.687 | 2998.271 |
| t-489 | 6 | 1 | 216.529 | 22.101 | 1030 | 0 | 1338 | 1338 | 0 | 78.847 | 2113.535 | 2867.199 |
| t-459 | 25 | 1 | 25.979 | 10.601 | 2174 | 0 | 694 | 694 | 0 | 73.727 | 2088.959 | 3047.423 |
| t-012 | 6 | 1 | 95.846 | 9.932 | 556 | 0 | 611 | 611 | 0 | 71.167 | 2080.767 | 2703.359 |
| t-105 | 33 | 2 | 114.013 | 65.553 | 851 | 0 | 4082 | 4082 | 0 | 80.383 | 2080.767 | 3932.159 |
| t-090 | 7 | 1 | 61.891 | 7.281 | 242 | 0 | 470 | 470 | 0 | 82.943 | 2072.575 | 3047.423 |
| t-369 | 14 | 2 | 205.669 | 50.502 | 1313 | 0 | 3137 | 3137 | 0 | 81.919 | 2064.383 | 3063.807 |
| t-270 | 21 | 1 | 66.974 | 22.653 | 556 | 0 | 1442 | 1442 | 0 | 73.727 | 2039.807 | 3178.495 |
| t-363 | 19 | 1 | 144.37 | 46.846 | 639 | 0 | 2837 | 2837 | 0 | 103.423 | 2031.615 | 3178.495 |
| t-045 | 47 | 2 | 128.053 | 99.785 | 1398 | 0 | 6069 | 6069 | 0 | 95.231 | 2015.231 | 3080.191 |
| t-024 | 14 | 1 | 51.738 | 11.811 | 1743 | 0 | 773 | 773 | 0 | 80.383 | 2007.039 | 2899.967 |
| t-084 | 23 | 1 | 126.5 | 45.888 | 204 | 0 | 2860 | 2860 | 0 | 93.695 | 1982.463 | 3342.335 |
| t-468 | 61 | 4 | 196.894 | 196.931 | 1645 | 0 | 12034 | 12034 | 0 | 84.479 | 1982.463 | 3096.575 |
