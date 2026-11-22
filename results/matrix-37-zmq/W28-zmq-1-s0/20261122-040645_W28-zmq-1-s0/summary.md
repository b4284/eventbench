# W28-zmq-1-s0 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.386s, closing 1.381s, drain 3.881s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 570619 | 570619 | 9625.048 | 9501.436 | 12925.6 | 0 | 0 | 0 | 0 | 2747 | 115362 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 442062 | 8242.069 | 7974.911 | 12050.431 | 15081.471 | 17072.127 | 20807.679 |
| e2e_from_send | 442062 | 584.794 | 397.567 | 1200.127 | 3231.743 | 4886.527 | 7012.351 |
| producer_lag | 442062 | 7657.272 | 7421.951 | 11231.231 | 14286.847 | 14770.175 | 15007.743 |
| publish_call | 570619 | 217.337 | 166.911 | 450.559 | 823.295 | 1523.711 | 2473.983 |
| queue_wait | 442062 | 514.197 | 333.567 | 1097.727 | 3110.911 | 4812.799 | 7008.255 |
| broker_dwell | 442062 | 329.235 | 99.519 | 930.815 | 2951.167 | 4640.767 | 6922.239 |
| receive_call | 495357 | 30.197 | 12.767 | 77.375 | 261.119 | 457.983 | 802.303 |
| ctx_load | 442062 | 14.798 | 9.967 | 30.975 | 79.103 | 143.999 | 458.751 |
| process | 442062 | 10.096 | 0.002 | 0.004 | 0.082 | 2617.343 | 3168.255 |
| ctx_commit | 442062 | 45.365 | 35.135 | 93.247 | 180.479 | 287.743 | 511.487 |
| ack | 442049 | 0.013 | 0.004 | 0.008 | 0.068 | 1.788 | 67.967 |
| service | 442062 | 70.596 | 50.271 | 117.247 | 239.743 | 2682.879 | 3514.367 |
| ready_wait | 442062 | 95.599 | 43.647 | 262.911 | 591.359 | 912.383 | 1456.127 |
| blocked | 442062 | 75.488 | 33.471 | 207.999 | 464.639 | 710.143 | 1451.007 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9501.436 | 12925.6 |
| applied_total | 310322.1 | 545712 |
| bench_cpu_cores | 3.504 | 4.573 |
| bench_cpu_pct | 14.6 | 19.05 |
| bench_gc_gen0 | 1.48 | 3 |
| bench_gc_gen1 | 0.48 | 1 |
| bench_gc_gen2 | 0.36 | 1 |
| bench_gc_heap_mb | 499.402 | 806.9 |
| bench_mem_mb | 895.228 | 1182.6 |
| bench_threadpool_queue | 106.2 | 1101 |
| bench_threadpool_threads | 41.26 | 44 |
| broker_backlog | 20781.4 | 40191 |
| ctr_eb_mssql_cpu_pct | 4.348 | 4.75 |
| ctr_eb_mssql_mem_mb | 4894.912 | 4895.2 |
| ctr_eb_mysql_cpu_pct | 1.105 | 1.22 |
| ctr_eb_mysql_mem_mb | 5950.98 | 5951.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 32.878 | 34.89 |
| ctr_eb_pgbouncer_2_mem_mb | 13.178 | 13.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 34.724 | 38.08 |
| ctr_eb_pgbouncer_3_mem_mb | 16.226 | 17 |
| ctr_eb_pgbouncer_4_cpu_pct | 32.084 | 34.32 |
| ctr_eb_pgbouncer_4_mem_mb | 16.728 | 17.8 |
| ctr_eb_pgbouncer_cpu_pct | 33.768 | 36.12 |
| ctr_eb_pgbouncer_mem_mb | 17.472 | 18.4 |
| ctr_eb_postgres_cpu_pct | 1112.72 | 1185.66 |
| ctr_eb_postgres_mem_mb | 5364.13 | 5893.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.327 | 85.32 |
| in_flight | 3429.32 | 10323 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.433 | 2.5 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 619697.9 | 1107708 |
| octopus_missing_reports_total | 39364.76 | 68588 |
| octopus_mqtt_received_total | 131644.86 | 217247 |
| octopus_mqtt_sent_total | 242702.4 | 421952 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 25.96 | 45 |
| pg_active_loads | 12.76 | 33 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.959 | 100 |
| pg_conn_active | 78.02 | 102 |
| pg_conn_idle | 186.78 | 223 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.02 | 35 |
| pg_conn_total | 264.8 | 266 |
| pg_db_size_mb | 3846.562 | 4401.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25013.794 | 29101.6 |
| pg_tup_del_per_s | 34119.342 | 44855.1 |
| pg_tup_fetch_per_s | 295556.678 | 341774.4 |
| pg_tup_ins_per_s | 44658.78 | 50533.6 |
| pg_tup_upd_per_s | 50867.348 | 61817 |
| pg_wal_mb_per_s | 118.326 | 147.89 |
| pgb_cl_active | 625.64 | 668 |
| pgb_cl_waiting | 10.12 | 58 |
| pgb_maxwait_ms | 0.66 | 5.86 |
| pgb_sv_active | 27.66 | 48 |
| pgb_sv_idle | 20.34 | 44 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 9625.048 | 18663.5 |
| produced_total | 313751.42 | 555132 |
| publish_mb_per_s | 15.145 | 29.34 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 177,440 records are Completed |
| record_counts | pass | 177,440 records found, 74,045 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 386 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 570,619 events applied by workers, 570,619 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 115,362 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 465 ms, max 1451 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8595 seq 17: ready 150 ms before start, 150 ms of it with an idle slot in group 296
- [cross_source_blocking] source 7128 seq 31: ready 152 ms before start, 152 ms of it with an idle slot in group 242
- [cross_source_blocking] source 2240 seq 47: ready 114 ms before start, 102 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2304 seq 11: ready 150 ms before start, 150 ms of it with an idle slot in group 66
- [cross_source_blocking] source 11589 seq 11: ready 150 ms before start, 150 ms of it with an idle slot in group 376
- [cross_source_blocking] source 7104 seq 5: ready 163 ms before start, 163 ms of it with an idle slot in group 241
- [cross_source_blocking] source 13262 seq 38: ready 205 ms before start, 199 ms of it with an idle slot in group 425
- [cross_source_blocking] source 12742 seq 15: ready 198 ms before start, 198 ms of it with an idle slot in group 402
- [cross_source_blocking] source 2290 seq 22: ready 142 ms before start, 142 ms of it with an idle slot in group 66
- [cross_source_blocking] source 6645 seq 14: ready 178 ms before start, 178 ms of it with an idle slot in group 224
- [cross_source_blocking] source 11575 seq 14: ready 318 ms before start, 318 ms of it with an idle slot in group 376
- [cross_source_blocking] source 15842 seq 51: ready 199 ms before start, 199 ms of it with an idle slot in group 496
- [cross_source_blocking] source 10235 seq 38: ready 203 ms before start, 202 ms of it with an idle slot in group 344
- [cross_source_blocking] source 8761 seq 21: ready 199 ms before start, 199 ms of it with an idle slot in group 305
- [cross_source_blocking] source 3342 seq 12: ready 150 ms before start, 150 ms of it with an idle slot in group 114
- [cross_source_blocking] source 7441 seq 9: ready 200 ms before start, 174 ms of it with an idle slot in group 258
- [cross_source_blocking] source 144 seq 25: ready 149 ms before start, 149 ms of it with an idle slot in group 2
- [cross_source_blocking] source 4125 seq 15: ready 204 ms before start, 193 ms of it with an idle slot in group 136
- [cross_source_blocking] source 11077 seq 15: ready 147 ms before start, 147 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4136 seq 14: ready 133 ms before start, 133 ms of it with an idle slot in group 136

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-473 | 32 | 1 | 91.6 | 49.759 | 4074 | 0 | 2548 | 2548 | 0 | 8585.215 | 17170.431 | 19660.799 |
| t-232 | 6 | 1 | 123.071 | 12.511 | 805 | 0 | 613 | 613 | 0 | 7634.943 | 17039.359 | 17563.647 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 21743 | 21743 | 0 | 8454.143 | 17039.359 | 20840.447 |
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 7473 | 7473 | 0 | 8323.071 | 16580.607 | 17432.575 |
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 24311 | 24311 | 0 | 8454.143 | 16318.463 | 18743.295 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 14363 | 14363 | 0 | 8191.999 | 16121.855 | 19660.799 |
| t-313 | 35 | 1 | 76.092 | 45.564 | 510 | 0 | 2348 | 2348 | 0 | 8454.143 | 16121.855 | 17039.359 |
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 11843 | 11843 | 0 | 8388.607 | 16056.319 | 17956.863 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 9787 | 9787 | 0 | 8093.695 | 15859.711 | 17563.647 |
| t-105 | 33 | 2 | 159.618 | 91.774 | 851 | 0 | 4765 | 4765 | 0 | 8093.695 | 15728.639 | 17432.575 |
| t-290 | 15 | 1 | 185.897 | 44.82 | 212 | 0 | 2297 | 2297 | 0 | 8126.463 | 15728.639 | 17039.359 |
| t-417 | 75 | 3 | 95.64 | 118.871 | 3449 | 0 | 6232 | 6232 | 0 | 8126.463 | 15532.031 | 17432.575 |
| t-488 | 26 | 2 | 125.165 | 55.423 | 1017 | 0 | 2872 | 2872 | 0 | 7929.855 | 15532.031 | 17694.719 |
| t-497 | 57 | 1 | 49.416 | 45.807 | 912 | 0 | 2542 | 2542 | 0 | 8257.535 | 15532.031 | 17039.359 |
| t-137 | 10 | 1 | 47.308 | 7.961 | 1569 | 0 | 429 | 429 | 0 | 7733.247 | 15466.495 | 15597.567 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2446 | 2446 | 0 | 8159.231 | 15400.959 | 16580.607 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 29733 | 29733 | 0 | 8126.463 | 15400.959 | 17694.719 |
| t-106 | 24 | 4 | 386.38 | 157.097 | 824 | 0 | 7681 | 7681 | 0 | 8126.463 | 15269.887 | 16252.927 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 7333 | 7333 | 0 | 8355.839 | 15269.887 | 16908.287 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 18408 | 18408 | 0 | 8093.695 | 15269.887 | 17694.719 |
| t-425 | 89 | 6 | 180.363 | 263.66 | 2911 | 0 | 13530 | 13530 | 0 | 8093.695 | 15269.887 | 17301.503 |
| t-450 | 63 | 4 | 165.005 | 170.875 | 165 | 0 | 8647 | 8647 | 0 | 8093.695 | 15269.887 | 18219.007 |
| t-121 | 76 | 6 | 212.261 | 260.387 | 149 | 0 | 13164 | 13164 | 0 | 8159.231 | 15204.351 | 16515.071 |
| t-314 | 24 | 4 | 407.068 | 162.201 | 525 | 0 | 8167 | 8167 | 0 | 7962.623 | 15204.351 | 15859.711 |
| t-160 | 57 | 3 | 124.381 | 118.041 | 548 | 0 | 6054 | 6054 | 0 | 7962.623 | 15138.815 | 16711.679 |
