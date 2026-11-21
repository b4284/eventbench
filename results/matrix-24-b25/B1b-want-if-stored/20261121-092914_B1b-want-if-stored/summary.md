# B1b-want-if-stored — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.263s, closing 2.201s, drain 3.073s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000366 | 1000366 | 16045.298 | 16315.965 | 19302.1 | 0 | 0 | 0 | 458 | 5163 | 347932 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796996 | 1148.383 | 285.439 | 3332.095 | 10297.343 | 15515.647 | 26968.063 |
| e2e_from_send | 796996 | 1048.392 | 219.775 | 3170.303 | 10207.231 | 15441.919 | 26951.679 |
| producer_lag | 796996 | 99.991 | 3.651 | 318.463 | 1350.655 | 1937.407 | 2598.911 |
| publish_call | 1000366 | 60.752 | 42.175 | 125.887 | 301.823 | 575.999 | 999.423 |
| queue_wait | 796996 | 993.354 | 166.015 | 3104.767 | 10149.887 | 15376.383 | 26935.295 |
| broker_dwell | 796996 | 954.591 | 115.391 | 3055.615 | 10117.119 | 15335.423 | 26918.911 |
| receive_call | 822215 | 38.2 | 15.743 | 95.551 | 338.943 | 474.111 | 590.335 |
| ctx_load | 796996 | 13.741 | 10.303 | 27.423 | 62.655 | 119.615 | 248.703 |
| process | 796996 | 10.756 | 0.002 | 0.004 | 0.064 | 2623.487 | 3096.575 |
| ctx_commit | 796996 | 30.361 | 24.671 | 59.807 | 107.583 | 178.559 | 310.271 |
| ack | 796978 | 0.011 | 0.004 | 0.007 | 0.046 | 1.234 | 57.471 |
| service | 796996 | 55.038 | 38.783 | 80.191 | 154.239 | 2666.495 | 3309.567 |
| e2e_large | 356 | 2810.575 | 2707.455 | 4018.175 | 5455.871 | 6651.903 | 6651.903 |
| ready_wait | 796996 | 192.762 | 82.111 | 525.823 | 1185.791 | 1756.159 | 2506.751 |
| blocked | 796996 | 160.302 | 71.231 | 440.831 | 889.343 | 1381.375 | 1874.943 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16315.965 | 19302.1 |
| applied_total | 560626.143 | 948768 |
| bench_cpu_cores | 5.693 | 6.856 |
| bench_cpu_pct | 23.72 | 28.57 |
| bench_gc_gen0 | 2.122 | 4 |
| bench_gc_gen1 | 0.735 | 2 |
| bench_gc_gen2 | 0.531 | 1 |
| bench_gc_heap_mb | 1469.135 | 1943.3 |
| bench_mem_mb | 2211.91 | 2565.5 |
| bench_threadpool_queue | 159.204 | 606 |
| bench_threadpool_threads | 42.551 | 44 |
| broker_backlog | 19286.286 | 31822 |
| ctr_eb_mosquitto_cpu_pct | 65.255 | 68.54 |
| ctr_eb_mosquitto_mem_mb | 7.141 | 12.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.286 | 22.2 |
| ctr_eb_pgbouncer_2_mem_mb | 15.312 | 16 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.727 | 22.42 |
| ctr_eb_pgbouncer_3_mem_mb | 15.482 | 17 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.387 | 23.14 |
| ctr_eb_pgbouncer_4_mem_mb | 15.496 | 16.7 |
| ctr_eb_pgbouncer_cpu_pct | 21.765 | 23.64 |
| ctr_eb_pgbouncer_mem_mb | 15.6 | 16.3 |
| ctr_eb_postgres_cpu_pct | 867.626 | 971.83 |
| ctr_eb_postgres_mem_mb | 5687.886 | 5876.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.599 | 80.87 |
| in_flight | 16420.429 | 27847 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.756 | 1.82 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1118552.224 | 1943402 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 210304.939 | 358045 |
| octopus_mqtt_sent_total | 296945.796 | 536957 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.367 | 15 |
| pg_active_loads | 8.714 | 94 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 31.245 | 136 |
| pg_conn_idle | 227.939 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.755 | 35 |
| pg_conn_total | 259.184 | 260 |
| pg_db_size_mb | 3687.969 | 4216 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19862.473 | 22547.4 |
| pg_tup_del_per_s | 44332.208 | 54634.7 |
| pg_tup_fetch_per_s | 458213.847 | 541642.9 |
| pg_tup_ins_per_s | 55513.839 | 67645.4 |
| pg_tup_upd_per_s | 43374.312 | 49397.9 |
| pg_wal_mb_per_s | 115.245 | 183.65 |
| pgb_cl_active | 250.204 | 260 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 9.224 | 24 |
| pgb_sv_idle | 38.776 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16045.298 | 23097 |
| produced_total | 577046.571 | 959791 |
| publish_mb_per_s | 52.334 | 82.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,856 records are Completed |
| record_counts | pass | 138,856 records found, 138,856 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,366 events applied by workers, 1,000,366 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 347,932 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 889 ms, max 1874 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7235 seq 35: ready 135 ms before start, 135 ms of it with an idle slot in group 246
- [cross_source_blocking] source 9302 seq 18: ready 209 ms before start, 209 ms of it with an idle slot in group 321
- [cross_source_blocking] source 8463 seq 9: ready 152 ms before start, 103 ms of it with an idle slot in group 287
- [cross_source_blocking] source 7277 seq 7: ready 185 ms before start, 185 ms of it with an idle slot in group 249
- [cross_source_blocking] source 217 seq 11: ready 187 ms before start, 187 ms of it with an idle slot in group 5
- [cross_source_blocking] source 14641 seq 5: ready 181 ms before start, 181 ms of it with an idle slot in group 463
- [cross_source_blocking] source 6249 seq 10: ready 188 ms before start, 188 ms of it with an idle slot in group 213
- [cross_source_blocking] source 13177 seq 29: ready 187 ms before start, 187 ms of it with an idle slot in group 418
- [cross_source_blocking] source 4229 seq 28: ready 168 ms before start, 168 ms of it with an idle slot in group 141
- [cross_source_blocking] source 15093 seq 2: ready 188 ms before start, 188 ms of it with an idle slot in group 471
- [cross_source_blocking] source 5009 seq 12: ready 183 ms before start, 183 ms of it with an idle slot in group 173
- [cross_source_blocking] source 8129 seq 23: ready 184 ms before start, 184 ms of it with an idle slot in group 275
- [cross_source_blocking] source 8241 seq 10: ready 182 ms before start, 182 ms of it with an idle slot in group 279
- [cross_source_blocking] source 190 seq 13: ready 170 ms before start, 170 ms of it with an idle slot in group 4
- [cross_source_blocking] source 6989 seq 5: ready 186 ms before start, 186 ms of it with an idle slot in group 237
- [cross_source_blocking] source 297 seq 16: ready 166 ms before start, 166 ms of it with an idle slot in group 8
- [cross_source_blocking] source 10937 seq 19: ready 185 ms before start, 185 ms of it with an idle slot in group 357
- [cross_source_blocking] source 2773 seq 7: ready 189 ms before start, 189 ms of it with an idle slot in group 85
- [cross_source_blocking] source 1865 seq 12: ready 186 ms before start, 186 ms of it with an idle slot in group 52
- [cross_source_blocking] source 12745 seq 11: ready 183 ms before start, 177 ms of it with an idle slot in group 402
- [cross_source_blocking] source 437 seq 6: ready 187 ms before start, 187 ms of it with an idle slot in group 14

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9220 | 9220 | 0 | 5013.503 | 19529.727 | 21889.023 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 4456.447 | 18743.295 | 27000.831 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 4325.375 | 17825.791 | 24117.247 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 3293.183 | 16515.071 | 20316.159 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5630 | 5630 | 0 | 2981.887 | 15400.959 | 19267.583 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 1572.863 | 14811.135 | 16187.391 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 3522.559 | 14352.383 | 15663.103 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 3407.871 | 14155.775 | 17301.503 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15138 | 15138 | 0 | 3424.255 | 14024.703 | 19660.799 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5539 | 5539 | 0 | 4587.519 | 13631.487 | 15269.887 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14675 | 14675 | 0 | 4112.383 | 13238.271 | 15335.423 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 2080.767 | 12451.839 | 16187.391 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11131 | 11131 | 0 | 2539.519 | 12124.159 | 16121.855 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 3194.879 | 12124.159 | 16187.391 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 2375.679 | 12058.623 | 15269.887 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6099 | 6099 | 0 | 2719.743 | 11796.479 | 15663.103 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 1564.671 | 11730.943 | 12976.127 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20593 | 20593 | 0 | 1826.815 | 11272.191 | 16449.535 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 1835.007 | 11272.191 | 13172.735 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4961 | 4961 | 0 | 454.655 | 11206.655 | 13369.343 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 2473.983 | 10420.223 | 13631.487 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 1687.551 | 10420.223 | 13172.735 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 1728.511 | 9961.471 | 12058.623 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19762 | 19762 | 0 | 1466.367 | 9764.863 | 13959.167 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3103 | 3103 | 0 | 2228.223 | 9633.791 | 15728.639 |
