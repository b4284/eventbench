# K18-octopus-2d-18k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 675 workers, offered 17994.982 ev/s
- Phases: run 60.158s, closing 1.354s, drain 2.601s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1124861 | 1124861 | 18027.614 | 18035.088 | 24220 | 0 | 0 | 0 | 455 | 5772 | 62784 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 900501 | 105.977 | 35.231 | 172.927 | 1708.031 | 2924.543 | 5394.431 |
| e2e_from_send | 900501 | 102.943 | 33.215 | 163.839 | 1704.959 | 2920.447 | 5394.431 |
| producer_lag | 900501 | 3.034 | 1.217 | 4.919 | 29.055 | 247.423 | 638.975 |
| publish_call | 1124861 | 30.053 | 16.151 | 69.055 | 198.527 | 326.911 | 523.775 |
| queue_wait | 900501 | 79.995 | 20.607 | 137.599 | 1411.071 | 2639.871 | 5132.287 |
| broker_dwell | 900501 | 65.306 | 7.303 | 113.087 | 1395.711 | 2627.583 | 5083.135 |
| receive_call | 1046275 | 27.778 | 8.179 | 74.431 | 286.207 | 457.727 | 539.135 |
| ctx_load | 900501 | 4.903 | 3.757 | 9.591 | 20.671 | 34.143 | 70.015 |
| process | 900501 | 10.612 | 0.001 | 0.002 | 0.024 | 2621.439 | 3002.367 |
| ctx_commit | 900501 | 7.405 | 5.471 | 15.007 | 32.959 | 49.951 | 202.495 |
| ack | 899205 | 0.006 | 0.003 | 0.006 | 0.033 | 0.365 | 24.399 |
| service | 900501 | 22.948 | 9.935 | 23.551 | 51.295 | 2633.727 | 3063.807 |
| e2e_large | 350 | 2517.415 | 2502.655 | 3311.615 | 5005.311 | 5349.375 | 5349.375 |
| ready_wait | 900501 | 27.785 | 5.539 | 59.295 | 345.855 | 1126.399 | 1977.343 |
| blocked | 900501 | 27.378 | 5.371 | 57.759 | 345.087 | 1125.375 | 1977.343 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 18035.088 | 20067.5 |
| applied_total | 651517.49 | 1084326 |
| bench_cpu_cores | 5.707 | 6.107 |
| bench_cpu_pct | 23.781 | 25.44 |
| bench_gc_gen0 | 3.51 | 5 |
| bench_gc_gen1 | 3.143 | 4 |
| bench_gc_gen2 | 0.592 | 2 |
| bench_gc_heap_mb | 973.743 | 1288.4 |
| bench_mem_mb | 1529.959 | 1714.8 |
| bench_threadpool_queue | 45.02 | 711 |
| bench_threadpool_threads | 38.469 | 44 |
| broker_backlog | 3491.939 | 4963 |
| ctr_eb_mosquitto_cpu_pct | 62.397 | 67.46 |
| ctr_eb_mosquitto_mem_mb | 6.894 | 10.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.883 | 22.33 |
| ctr_eb_pgbouncer_2_mem_mb | 15.943 | 17.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.451 | 21.61 |
| ctr_eb_pgbouncer_3_mem_mb | 15.763 | 16.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 22.561 | 24.23 |
| ctr_eb_pgbouncer_4_mem_mb | 16.073 | 16.7 |
| ctr_eb_pgbouncer_cpu_pct | 21.213 | 22.08 |
| ctr_eb_pgbouncer_mem_mb | 16.851 | 18.1 |
| ctr_eb_postgres_cpu_pct | 729.274 | 856.07 |
| ctr_eb_postgres_mem_mb | 5200.549 | 5410.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.301 | 75.57 |
| in_flight | 1493.837 | 3825 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.442 | 1.51 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1280163.204 | 2128732 |
| octopus_mqtt_received_total | 139649.653 | 233763 |
| octopus_mqtt_sent_total | 324401.898 | 567525 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.265 | 10 |
| pg_active_loads | 2.163 | 7 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 9.857 | 38 |
| pg_conn_idle | 227.327 | 236 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.551 | 26 |
| pg_conn_total | 237.184 | 238 |
| pg_db_size_mb | 3563.749 | 3996.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22509.741 | 26476.2 |
| pg_tup_del_per_s | 47757.708 | 65397.7 |
| pg_tup_fetch_per_s | 281570.504 | 526345.5 |
| pg_tup_ins_per_s | 59671.182 | 78352.5 |
| pg_tup_upd_per_s | 47283.22 | 55616.2 |
| pg_wal_mb_per_s | 124.381 | 253.9 |
| pgb_cl_active | 254.265 | 267 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 5.265 | 16 |
| pgb_sv_idle | 42.735 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 18027.614 | 18851.5 |
| produced_total | 653011.327 | 1085991 |
| publish_mb_per_s | 55.023 | 88.29 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 152,951 records are Completed |
| record_counts | pass | 152,951 records found, 152,951 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 455 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,124,861 events applied by workers, 1,124,861 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 62,784 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 345 ms, max 1977 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2280 seq 33: ready 115 ms before start, 115 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2246 seq 47: ready 104 ms before start, 104 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2260 seq 40: ready 122 ms before start, 122 ms of it with an idle slot in group 65
- [cross_source_blocking] source 5327 seq 29: ready 106 ms before start, 106 ms of it with an idle slot in group 182
- [cross_source_blocking] source 10406 seq 12: ready 119 ms before start, 119 ms of it with an idle slot in group 347
- [cross_source_blocking] source 8410 seq 12: ready 114 ms before start, 114 ms of it with an idle slot in group 285
- [cross_source_blocking] source 14794 seq 13: ready 119 ms before start, 119 ms of it with an idle slot in group 464
- [cross_source_blocking] source 3986 seq 25: ready 120 ms before start, 120 ms of it with an idle slot in group 132
- [cross_source_blocking] source 10710 seq 18: ready 114 ms before start, 114 ms of it with an idle slot in group 350
- [cross_source_blocking] source 11850 seq 17: ready 120 ms before start, 120 ms of it with an idle slot in group 383
- [cross_source_blocking] source 3138 seq 11: ready 122 ms before start, 101 ms of it with an idle slot in group 104
- [cross_source_blocking] source 134 seq 19: ready 113 ms before start, 113 ms of it with an idle slot in group 2
- [cross_source_blocking] source 6750 seq 31: ready 117 ms before start, 117 ms of it with an idle slot in group 230
- [cross_source_blocking] source 2286 seq 37: ready 108 ms before start, 108 ms of it with an idle slot in group 65
- [cross_source_blocking] source 5267 seq 29: ready 140 ms before start, 140 ms of it with an idle slot in group 182
- [cross_source_blocking] source 7074 seq 19: ready 115 ms before start, 115 ms of it with an idle slot in group 239
- [cross_source_blocking] source 11973 seq 21: ready 177 ms before start, 177 ms of it with an idle slot in group 383
- [cross_source_blocking] source 5690 seq 18: ready 114 ms before start, 114 ms of it with an idle slot in group 196
- [cross_source_blocking] source 3798 seq 36: ready 114 ms before start, 114 ms of it with an idle slot in group 128
- [cross_source_blocking] source 10296 seq 19: ready 101 ms before start, 101 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 410 | 410 | 0 | 2113.535 | 4653.055 | 5046.271 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 94 | 94 | 0 | 1687.551 | 4046.847 | 4095.999 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 171 | 171 | 0 | 1933.311 | 4030.463 | 5373.951 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2244.607 | 3473.407 | 5013.503 |
| t-437 | 9 | 1 | 81.169 | 12.187 | 1067 | 0 | 781 | 781 | 0 | 33.279 | 3424.255 | 4358.143 |
| t-344 | 195 | 8 | 114.687 | 377.409 | 571 | 0 | 23185 | 23185 | 0 | 223.231 | 2818.047 | 5177.343 |
| t-418 | 6 | 1 | 169.506 | 17.119 | 201 | 0 | 1091 | 1091 | 0 | 32.383 | 2654.207 | 2981.887 |
| t-288 | 6 | 1 | 81.281 | 8.222 | 214 | 0 | 489 | 489 | 0 | 32.639 | 2637.823 | 2998.271 |
| t-132 | 22 | 2 | 211.15 | 71.802 | 2676 | 0 | 4321 | 4321 | 0 | 33.791 | 2572.287 | 4521.983 |
| t-348 | 125 | 4 | 88.205 | 184.876 | 1340 | 0 | 11403 | 11403 | 0 | 87.551 | 2555.903 | 4587.519 |
| t-065 | 80 | 7 | 237.532 | 313.547 | 263 | 0 | 18950 | 18950 | 0 | 124.927 | 2441.215 | 4816.895 |
| t-079 | 6 | 1 | 135.325 | 13.915 | 1522 | 0 | 892 | 892 | 0 | 36.095 | 2441.215 | 3145.727 |
| t-295 | 12 | 1 | 77.856 | 15.466 | 3396 | 0 | 956 | 956 | 0 | 31.615 | 2424.831 | 3702.783 |
| t-346 | 12 | 1 | 39.254 | 7.607 | 1247 | 0 | 476 | 476 | 0 | 33.279 | 2359.295 | 2949.119 |
| t-383 | 200 | 8 | 108.541 | 361.004 | 2521 | 0 | 22130 | 22130 | 0 | 146.431 | 2326.527 | 4554.751 |
| t-002 | 17 | 1 | 115.795 | 32.971 | 377 | 0 | 2033 | 2033 | 0 | 32.767 | 2293.759 | 3751.935 |
| t-441 | 30 | 2 | 197.801 | 95.93 | 2203 | 0 | 5780 | 5780 | 0 | 34.047 | 2260.991 | 3047.423 |
| t-468 | 61 | 4 | 177.84 | 177.874 | 1645 | 0 | 10883 | 10883 | 0 | 84.991 | 2228.223 | 3473.407 |
| t-102 | 18 | 1 | 50.214 | 15.276 | 339 | 0 | 1017 | 1017 | 0 | 29.439 | 2211.839 | 2981.887 |
| t-355 | 10 | 1 | 42.315 | 7.419 | 349 | 0 | 465 | 465 | 0 | 32.639 | 2195.455 | 2949.119 |
| t-369 | 14 | 1 | 185.765 | 45.615 | 1313 | 0 | 2694 | 2694 | 0 | 33.535 | 2195.455 | 3604.479 |
| t-395 | 6 | 1 | 49.143 | 5.267 | 212 | 0 | 338 | 338 | 0 | 31.359 | 2195.455 | 3031.039 |
| t-482 | 10 | 1 | 94.026 | 15.997 | 2234 | 0 | 978 | 978 | 0 | 31.743 | 2195.455 | 2981.887 |
| t-447 | 17 | 1 | 63.422 | 17.115 | 558 | 0 | 1114 | 1114 | 0 | 28.927 | 2162.687 | 2981.887 |
| t-299 | 21 | 1 | 59.297 | 20.278 | 646 | 0 | 1293 | 1293 | 0 | 31.231 | 2146.303 | 2981.887 |
