# W28-mosq-1-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.242s, closing 1.423s, drain 129.121s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 680041 | 680041 | 11626.492 | 9573.38 | 22799.9 | 0 | 0 | 0 | 0 | 3246 | 352785 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 551340 | 7711.554 | 4960.255 | 16408.575 | 45449.215 | 90439.679 | 127598.591 |
| e2e_from_send | 551340 | 6430.78 | 3049.471 | 15745.023 | 45187.071 | 90177.535 | 127533.055 |
| producer_lag | 551340 | 1280.775 | 617.983 | 3430.399 | 5828.607 | 6946.815 | 7368.703 |
| publish_call | 680041 | 162.342 | 125.759 | 321.023 | 660.479 | 1523.711 | 2222.079 |
| queue_wait | 551340 | 6362.448 | 2961.407 | 15679.487 | 45187.071 | 90177.535 | 127533.055 |
| broker_dwell | 551340 | 6249.763 | 2850.815 | 15589.375 | 45121.535 | 90046.463 | 127533.055 |
| receive_call | 572007 | 27.89 | 13.439 | 66.623 | 239.231 | 498.175 | 542.207 |
| ctx_load | 551340 | 16.465 | 11.119 | 36.511 | 82.367 | 126.655 | 245.631 |
| process | 551340 | 9.909 | 0.002 | 0.004 | 0.078 | 2607.103 | 3084.287 |
| ctx_commit | 551340 | 41.651 | 32.047 | 90.751 | 183.551 | 287.487 | 557.055 |
| ack | 551136 | 0.015 | 0.004 | 0.009 | 0.07 | 1.743 | 118.463 |
| service | 551340 | 68.329 | 49.663 | 119.487 | 243.455 | 2672.639 | 3209.215 |
| ready_wait | 551340 | 322.924 | 212.991 | 800.767 | 1702.911 | 2566.143 | 11943.935 |
| blocked | 551340 | 272.834 | 178.943 | 658.431 | 1326.079 | 2117.631 | 11059.199 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9573.38 | 11577.9 |
| applied_total | 335501.571 | 558300 |
| bench_cpu_cores | 3.601 | 4.656 |
| bench_cpu_pct | 15.006 | 19.4 |
| bench_gc_gen0 | 1.388 | 3 |
| bench_gc_gen1 | 0.347 | 2 |
| bench_gc_gen2 | 0.224 | 1 |
| bench_gc_heap_mb | 498.067 | 737.6 |
| bench_mem_mb | 926.518 | 1147.5 |
| bench_threadpool_queue | 132.429 | 995 |
| bench_threadpool_threads | 40.184 | 44 |
| broker_backlog | 61868.755 | 125240 |
| ctr_eb_mosquitto_cpu_pct | 86.529 | 89.19 |
| ctr_eb_mosquitto_mem_mb | 11.48 | 20.8 |
| ctr_eb_mssql_cpu_pct | 4.563 | 4.94 |
| ctr_eb_mssql_mem_mb | 4897.1 | 4897.1 |
| ctr_eb_mysql_cpu_pct | 1.222 | 1.47 |
| ctr_eb_mysql_mem_mb | 5950.9 | 5950.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 31.923 | 33.24 |
| ctr_eb_pgbouncer_2_mem_mb | 15.498 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 33.22 | 35.45 |
| ctr_eb_pgbouncer_3_mem_mb | 15.549 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 35.547 | 38.81 |
| ctr_eb_pgbouncer_4_mem_mb | 16.824 | 17.8 |
| ctr_eb_pgbouncer_cpu_pct | 31.887 | 33.49 |
| ctr_eb_pgbouncer_mem_mb | 16.763 | 17.5 |
| ctr_eb_postgres_cpu_pct | 1072.676 | 1145.12 |
| ctr_eb_postgres_mem_mb | 5758.098 | 5963.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 83.706 | 86.64 |
| in_flight | 51771.02 | 105599 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.247 | 2.37 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 710158.163 | 1242864 |
| octopus_missing_reports_total | 47911.612 | 81984 |
| octopus_mqtt_received_total | 157360.347 | 271566 |
| octopus_mqtt_sent_total | 246324.796 | 420175 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.082 | 26 |
| pg_active_loads | 10.204 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 53.061 | 100 |
| pg_conn_idle | 211.837 | 249 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 10.898 | 38 |
| pg_conn_total | 264.898 | 266 |
| pg_db_size_mb | 3614.727 | 4126.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23795.1 | 27466.6 |
| pg_tup_del_per_s | 31337.867 | 35069.7 |
| pg_tup_fetch_per_s | 322412.32 | 385913.2 |
| pg_tup_ins_per_s | 43611.818 | 47605.9 |
| pg_tup_upd_per_s | 50640.416 | 57460.6 |
| pg_wal_mb_per_s | 116.511 | 159.17 |
| pgb_cl_active | 543.265 | 625 |
| pgb_cl_waiting | 27.265 | 317 |
| pgb_maxwait_ms | 2.833 | 29.03 |
| pgb_sv_active | 22.02 | 48 |
| pgb_sv_idle | 25.98 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11626.492 | 16286.6 |
| produced_total | 387272.592 | 663899 |
| publish_mb_per_s | 18.291 | 25.58 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 179,815 records are Completed |
| record_counts | pass | 179,815 records found, 86,222 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 345 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 680,041 events applied by workers, 680,041 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 352,785 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1326 ms, max 11055 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14782 seq 17: ready 121 ms before start, 109 ms of it with an idle slot in group 464
- [cross_source_blocking] source 14080 seq 18: ready 267 ms before start, 267 ms of it with an idle slot in group 449
- [cross_source_blocking] source 8793 seq 22: ready 151 ms before start, 119 ms of it with an idle slot in group 306
- [cross_source_blocking] source 12020 seq 9: ready 211 ms before start, 211 ms of it with an idle slot in group 384
- [cross_source_blocking] source 7441 seq 9: ready 425 ms before start, 276 ms of it with an idle slot in group 258
- [cross_source_blocking] source 2484 seq 29: ready 141 ms before start, 141 ms of it with an idle slot in group 74
- [cross_source_blocking] source 6645 seq 14: ready 425 ms before start, 425 ms of it with an idle slot in group 224
- [cross_source_blocking] source 8613 seq 16: ready 426 ms before start, 370 ms of it with an idle slot in group 297
- [cross_source_blocking] source 14674 seq 16: ready 155 ms before start, 117 ms of it with an idle slot in group 464
- [cross_source_blocking] source 1659 seq 12: ready 422 ms before start, 323 ms of it with an idle slot in group 49
- [cross_source_blocking] source 2613 seq 14: ready 421 ms before start, 372 ms of it with an idle slot in group 80
- [cross_source_blocking] source 11342 seq 16: ready 269 ms before start, 269 ms of it with an idle slot in group 370
- [cross_source_blocking] source 12032 seq 13: ready 274 ms before start, 274 ms of it with an idle slot in group 385
- [cross_source_blocking] source 9040 seq 10: ready 290 ms before start, 290 ms of it with an idle slot in group 312
- [cross_source_blocking] source 7409 seq 17: ready 290 ms before start, 290 ms of it with an idle slot in group 256
- [cross_source_blocking] source 13652 seq 19: ready 261 ms before start, 261 ms of it with an idle slot in group 434
- [cross_source_blocking] source 14327 seq 4: ready 247 ms before start, 247 ms of it with an idle slot in group 458
- [cross_source_blocking] source 12354 seq 7: ready 295 ms before start, 295 ms of it with an idle slot in group 392
- [cross_source_blocking] source 8295 seq 38: ready 269 ms before start, 186 ms of it with an idle slot in group 282
- [cross_source_blocking] source 3497 seq 32: ready 271 ms before start, 271 ms of it with an idle slot in group 120

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 29091 | 29091 | 0 | 16383.999 | 100139.007 | 127926.271 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 26042 | 26042 | 0 | 13172.735 | 57671.679 | 73924.607 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 35357 | 35357 | 0 | 13959.167 | 40894.463 | 58195.967 |
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 8902 | 8902 | 0 | 11862.015 | 27656.191 | 30277.631 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8675 | 8675 | 0 | 9306.111 | 26607.615 | 31588.351 |
| t-473 | 32 | 1 | 91.6 | 49.759 | 4074 | 0 | 3049 | 3049 | 0 | 11730.943 | 26607.615 | 30277.631 |
| t-481 | 12 | 1 | 263.464 | 49.674 | 1993 | 0 | 2995 | 2995 | 0 | 11206.655 | 26476.543 | 27656.191 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2967 | 2967 | 0 | 10092.543 | 25559.039 | 27525.119 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2818 | 2818 | 0 | 8847.359 | 25296.895 | 28311.551 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11658 | 11658 | 0 | 10027.007 | 24379.391 | 26738.687 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5631 | 5631 | 0 | 8454.143 | 24117.247 | 28049.407 |
| t-105 | 33 | 2 | 159.618 | 91.774 | 851 | 0 | 5647 | 5647 | 0 | 11403.263 | 23986.175 | 26083.327 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17201 | 17201 | 0 | 9895.935 | 23592.959 | 29622.271 |
| t-497 | 57 | 1 | 49.416 | 45.807 | 912 | 0 | 3018 | 3018 | 0 | 8454.143 | 23592.959 | 26476.543 |
| t-361 | 36 | 1 | 76.363 | 45.398 | 1404 | 0 | 2849 | 2849 | 0 | 8454.143 | 23330.815 | 30015.487 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5617 | 5617 | 0 | 6586.367 | 22937.599 | 27787.263 |
| t-161 | 41 | 2 | 132.357 | 89.666 | 339 | 0 | 5480 | 5480 | 0 | 8716.287 | 22675.455 | 27394.047 |
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 14263 | 14263 | 0 | 9437.183 | 22282.239 | 24379.391 |
| t-168 | 71 | 2 | 75.117 | 87.222 | 155 | 0 | 5390 | 5390 | 0 | 7438.335 | 21889.023 | 24248.319 |
| t-313 | 35 | 1 | 76.092 | 45.564 | 510 | 0 | 2843 | 2843 | 0 | 8355.839 | 21757.951 | 26869.759 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16594 | 16594 | 0 | 7208.959 | 21757.951 | 26345.471 |
| t-290 | 15 | 1 | 185.897 | 44.82 | 212 | 0 | 2768 | 2768 | 0 | 7798.783 | 21626.879 | 22937.599 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 21917 | 21917 | 0 | 8388.607 | 21233.663 | 26738.687 |
| t-234 | 21 | 1 | 127.636 | 43.478 | 141 | 0 | 2589 | 2589 | 0 | 6488.063 | 21102.591 | 23724.031 |
| t-320 | 14 | 1 | 186.953 | 41.44 | 574 | 0 | 2574 | 2574 | 0 | 8159.231 | 20971.519 | 22282.239 |
