# P0c-epochs-none-3d-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.119s, closing 1.38s, drain 2.055s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000510 | 1000510 | 15947.916 | 16088.453 | 21297 | 0 | 0 | 0 | 458 | 5162 | 52233 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797143 | 123.281 | 45.151 | 247.039 | 1609.727 | 2975.743 | 7856.127 |
| e2e_from_send | 797143 | 109.6 | 42.559 | 206.463 | 1580.031 | 2940.927 | 7856.127 |
| producer_lag | 797143 | 13.681 | 1.661 | 11.423 | 390.143 | 623.615 | 1092.607 |
| publish_call | 1000510 | 38.763 | 21.599 | 85.823 | 249.471 | 508.159 | 857.599 |
| queue_wait | 797143 | 78.096 | 24.559 | 159.999 | 1091.583 | 2617.343 | 7843.839 |
| broker_dwell | 797143 | 54.53 | 8.847 | 93.503 | 1057.791 | 2584.575 | 7831.551 |
| receive_call | 886891 | 39.056 | 19.487 | 90.943 | 316.415 | 485.119 | 574.975 |
| ctx_load | 797143 | 7.433 | 5.455 | 15.167 | 33.215 | 49.951 | 88.767 |
| process | 797143 | 10.736 | 0.002 | 0.004 | 0.049 | 2619.391 | 3008.511 |
| ctx_commit | 797143 | 13.285 | 8.815 | 29.807 | 64.671 | 95.359 | 380.671 |
| ack | 796582 | 0.007 | 0.003 | 0.005 | 0.031 | 0.495 | 57.247 |
| service | 797143 | 31.505 | 15.519 | 43.615 | 90.431 | 2643.967 | 3188.735 |
| e2e_large | 356 | 2606.918 | 2525.183 | 3438.591 | 5033.983 | 6696.959 | 6696.959 |
| ready_wait | 797143 | 25.364 | 5.815 | 63.327 | 273.407 | 534.015 | 1913.855 |
| blocked | 797143 | 22.947 | 5.791 | 56.479 | 236.031 | 426.751 | 856.575 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16088.453 | 18914.2 |
| applied_total | 577581.347 | 961223 |
| bench_cpu_cores | 5.003 | 5.732 |
| bench_cpu_pct | 20.846 | 23.88 |
| bench_gc_gen0 | 3.224 | 5 |
| bench_gc_gen1 | 2.898 | 4 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 1001.282 | 1250.1 |
| bench_mem_mb | 1557.09 | 1676.1 |
| bench_threadpool_queue | 63.551 | 367 |
| bench_threadpool_threads | 41.796 | 44 |
| broker_backlog | 2944.143 | 12557 |
| ctr_eb_mosquitto_cpu_pct | 67.195 | 71.21 |
| ctr_eb_mosquitto_mem_mb | 8.735 | 16.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.106 | 22.2 |
| ctr_eb_pgbouncer_2_mem_mb | 15.055 | 15.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.227 | 21.84 |
| ctr_eb_pgbouncer_3_mem_mb | 15.363 | 16.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.573 | 23.88 |
| ctr_eb_pgbouncer_4_mem_mb | 15.286 | 16.2 |
| ctr_eb_pgbouncer_cpu_pct | 19.963 | 23.02 |
| ctr_eb_pgbouncer_mem_mb | 15.876 | 16.6 |
| ctr_eb_postgres_cpu_pct | 850.416 | 988.27 |
| ctr_eb_postgres_mem_mb | 5838.927 | 5991.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.534 | 82.34 |
| in_flight | 1264.204 | 4434 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.834 | 1.9 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1132351.551 | 1915471 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 182274.082 | 305107 |
| octopus_mqtt_sent_total | 349936.143 | 625487 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.184 | 16 |
| pg_active_loads | 9.184 | 172 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 25.429 | 230 |
| pg_conn_idle | 231.735 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.776 | 11 |
| pg_conn_total | 257.163 | 258 |
| pg_db_size_mb | 3538.457 | 3995.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21377.718 | 25648.7 |
| pg_tup_del_per_s | 43951.431 | 55186.1 |
| pg_tup_fetch_per_s | 476005.627 | 658447.1 |
| pg_tup_ins_per_s | 55470.908 | 70243.9 |
| pg_tup_upd_per_s | 42333.216 | 51081.3 |
| pg_wal_mb_per_s | 109.196 | 167.34 |
| pgb_cl_active | 246.633 | 269 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.449 | 29 |
| pgb_sv_idle | 41.551 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15947.916 | 17586.3 |
| produced_total | 578845.551 | 962040 |
| publish_mb_per_s | 51.892 | 90.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,510 events applied by workers, 1,000,510 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 52,233 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 236 ms, max 856 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12632 seq 7: ready 100 ms before start, 100 ms of it with an idle slot in group 397
- [cross_source_blocking] source 642 seq 17: ready 135 ms before start, 135 ms of it with an idle slot in group 19
- [cross_source_blocking] source 8666 seq 3: ready 157 ms before start, 157 ms of it with an idle slot in group 300
- [cross_source_blocking] source 10338 seq 4: ready 136 ms before start, 136 ms of it with an idle slot in group 347
- [cross_source_blocking] source 6338 seq 16: ready 135 ms before start, 135 ms of it with an idle slot in group 216
- [cross_source_blocking] source 7338 seq 3: ready 127 ms before start, 127 ms of it with an idle slot in group 252
- [cross_source_blocking] source 13950 seq 12: ready 132 ms before start, 132 ms of it with an idle slot in group 443
- [cross_source_blocking] source 10434 seq 13: ready 136 ms before start, 136 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3798 seq 25: ready 129 ms before start, 129 ms of it with an idle slot in group 128
- [cross_source_blocking] source 7986 seq 22: ready 122 ms before start, 122 ms of it with an idle slot in group 269
- [cross_source_blocking] source 8262 seq 11: ready 125 ms before start, 125 ms of it with an idle slot in group 280
- [cross_source_blocking] source 5642 seq 28: ready 127 ms before start, 127 ms of it with an idle slot in group 194
- [cross_source_blocking] source 12110 seq 11: ready 153 ms before start, 153 ms of it with an idle slot in group 387
- [cross_source_blocking] source 11199 seq 13: ready 154 ms before start, 154 ms of it with an idle slot in group 364
- [cross_source_blocking] source 3670 seq 13: ready 124 ms before start, 124 ms of it with an idle slot in group 125
- [cross_source_blocking] source 4942 seq 6: ready 135 ms before start, 135 ms of it with an idle slot in group 170
- [cross_source_blocking] source 1034 seq 6: ready 158 ms before start, 158 ms of it with an idle slot in group 32
- [cross_source_blocking] source 7990 seq 21: ready 129 ms before start, 129 ms of it with an idle slot in group 269
- [cross_source_blocking] source 4602 seq 16: ready 156 ms before start, 156 ms of it with an idle slot in group 161
- [cross_source_blocking] source 11722 seq 15: ready 126 ms before start, 126 ms of it with an idle slot in group 379

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2228.223 | 6160.383 | 6717.439 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2195.455 | 4456.447 | 5636.095 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2162.687 | 3883.007 | 4554.751 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1925.119 | 3440.639 | 4358.143 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 46.847 | 3194.879 | 7864.319 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9221 | 9221 | 0 | 48.383 | 2965.503 | 6553.599 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 48.383 | 2768.895 | 6029.311 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 47.103 | 2588.671 | 4325.375 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 48.383 | 2539.519 | 6291.455 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 45.567 | 2490.367 | 3031.039 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 49.663 | 2473.983 | 4620.287 |
| t-104 | 46 | 1 | 39.533 | 30.319 | 2163 | 0 | 1915 | 1915 | 0 | 43.775 | 2441.215 | 3735.551 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 46.335 | 2441.215 | 2981.887 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 47.359 | 2408.447 | 3964.927 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 50.431 | 2408.447 | 3145.727 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 47.871 | 2392.063 | 3276.799 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 44.031 | 2326.527 | 3129.343 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 46.335 | 2310.143 | 2998.271 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 42.751 | 2277.375 | 2768.895 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 46.847 | 2277.375 | 2899.967 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14678 | 14678 | 0 | 46.335 | 2260.991 | 7110.655 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 44.031 | 2244.607 | 3325.951 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 50.687 | 2244.607 | 2965.503 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2400 | 2400 | 0 | 49.919 | 2228.223 | 3047.423 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 47.103 | 2211.839 | 3407.871 |
