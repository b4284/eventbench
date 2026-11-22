# W28-nats-3-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.007s, closing 0.707s, drain 8.093s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 683700 | 683700 | 11670.486 | 11047.357 | 18959.8 | 0 | 0 | 0 | 0 | 3264 | 243246 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 555015 | 1818.252 | 780.287 | 4767.743 | 12337.151 | 17858.559 | 23003.135 |
| e2e_from_send | 555015 | 1560.933 | 501.759 | 4517.887 | 12320.767 | 17842.175 | 23003.135 |
| producer_lag | 555015 | 257.32 | 5.159 | 1064.959 | 2742.271 | 3438.591 | 3891.199 |
| publish_call | 683700 | 117.857 | 76.095 | 254.335 | 633.343 | 1944.575 | 2365.439 |
| queue_wait | 555015 | 1489.459 | 431.871 | 4452.351 | 12263.423 | 17760.255 | 22904.831 |
| broker_dwell | 555015 | 1404.433 | 319.487 | 4374.527 | 12181.503 | 17711.103 | 22790.143 |
| receive_call | 603653 | 23.35 | 11.463 | 56.671 | 198.911 | 414.975 | 579.071 |
| ctx_load | 555015 | 16.485 | 11.535 | 34.463 | 84.223 | 144.127 | 299.263 |
| process | 555015 | 9.915 | 0.002 | 0.004 | 0.079 | 2617.343 | 3190.783 |
| ctx_commit | 555015 | 44.772 | 35.359 | 88.959 | 181.247 | 285.695 | 367.615 |
| ack | 554993 | 0.016 | 0.004 | 0.008 | 0.076 | 2.815 | 77.823 |
| service | 555015 | 71.475 | 52.703 | 115.455 | 238.719 | 2680.831 | 3258.367 |
| ready_wait | 555015 | 154.581 | 101.375 | 363.775 | 831.999 | 1360.895 | 3901.439 |
| blocked | 555015 | 116.191 | 80.191 | 276.223 | 519.423 | 755.199 | 3510.271 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11047.357 | 14095.7 |
| applied_total | 373443.531 | 624701 |
| bench_cpu_cores | 3.9 | 4.61 |
| bench_cpu_pct | 16.252 | 19.21 |
| bench_gc_gen0 | 1.347 | 3 |
| bench_gc_gen1 | 0.327 | 2 |
| bench_gc_gen2 | 0.163 | 2 |
| bench_gc_heap_mb | 611.537 | 846.4 |
| bench_mem_mb | 1002.041 | 1252.8 |
| bench_threadpool_queue | 175.245 | 1193 |
| bench_threadpool_threads | 40.102 | 44 |
| broker_backlog | 27709.184 | 45981 |
| ctr_eb_mssql_cpu_pct | 4.242 | 4.69 |
| ctr_eb_mssql_mem_mb | 4902.882 | 4903.8 |
| ctr_eb_mysql_cpu_pct | 1.164 | 1.31 |
| ctr_eb_mysql_mem_mb | 5951.9 | 5951.9 |
| ctr_eb_nats_octopus_cpu_pct | 114.448 | 117.93 |
| ctr_eb_nats_octopus_mem_mb | 54.567 | 92.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.832 | 32.57 |
| ctr_eb_pgbouncer_2_mem_mb | 16.076 | 16.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 32.912 | 35.69 |
| ctr_eb_pgbouncer_3_mem_mb | 15.935 | 17 |
| ctr_eb_pgbouncer_4_cpu_pct | 33.079 | 34.87 |
| ctr_eb_pgbouncer_4_mem_mb | 16.976 | 17.5 |
| ctr_eb_pgbouncer_cpu_pct | 31.813 | 33.44 |
| ctr_eb_pgbouncer_mem_mb | 17.076 | 17.8 |
| ctr_eb_postgres_cpu_pct | 1039.082 | 1102.75 |
| ctr_eb_postgres_mem_mb | 5940.949 | 6196 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 83.362 | 85.67 |
| in_flight | 15205.531 | 33901 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.025 | 2.13 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 749452.653 | 1280899 |
| octopus_missing_reports_total | 45329.449 | 72733 |
| octopus_mqtt_received_total | 160991.367 | 267273 |
| octopus_mqtt_sent_total | 281551.306 | 470232 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 19.673 | 37 |
| pg_active_loads | 11.755 | 36 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.995 | 100 |
| pg_conn_active | 47.735 | 109 |
| pg_conn_idle | 215.388 | 249 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.143 | 36 |
| pg_conn_total | 263.122 | 264 |
| pg_db_size_mb | 3698.165 | 4251 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26161.276 | 28347.7 |
| pg_tup_del_per_s | 34490.884 | 41028.6 |
| pg_tup_fetch_per_s | 297963.237 | 323882.9 |
| pg_tup_ins_per_s | 46385.392 | 53908.8 |
| pg_tup_upd_per_s | 55756.835 | 60621.3 |
| pg_wal_mb_per_s | 127.418 | 177.93 |
| pgb_cl_active | 586.224 | 618 |
| pgb_cl_waiting | 10.184 | 95 |
| pgb_maxwait_ms | 0.608 | 4.55 |
| pgb_sv_active | 32.551 | 48 |
| pgb_sv_idle | 15.449 | 37 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11670.486 | 17500.8 |
| produced_total | 388649.061 | 658602 |
| publish_mb_per_s | 18.358 | 27.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 190,019 records are Completed |
| record_counts | pass | 190,019 records found, 86,628 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 374 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 683,700 events applied by workers, 683,700 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 243,246 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 519 ms, max 3510 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15387 seq 45: ready 160 ms before start, 160 ms of it with an idle slot in group 481
- [cross_source_blocking] source 14654 seq 23: ready 118 ms before start, 111 ms of it with an idle slot in group 464
- [cross_source_blocking] source 1208 seq 11: ready 146 ms before start, 146 ms of it with an idle slot in group 40
- [cross_source_blocking] source 11547 seq 17: ready 141 ms before start, 141 ms of it with an idle slot in group 376
- [cross_source_blocking] source 15120 seq 5: ready 153 ms before start, 153 ms of it with an idle slot in group 472
- [cross_source_blocking] source 14719 seq 21: ready 141 ms before start, 118 ms of it with an idle slot in group 464
- [cross_source_blocking] source 4569 seq 31: ready 182 ms before start, 154 ms of it with an idle slot in group 160
- [cross_source_blocking] source 7284 seq 13: ready 202 ms before start, 162 ms of it with an idle slot in group 250
- [cross_source_blocking] source 13323 seq 46: ready 167 ms before start, 167 ms of it with an idle slot in group 425
- [cross_source_blocking] source 13652 seq 18: ready 210 ms before start, 210 ms of it with an idle slot in group 434
- [cross_source_blocking] source 1300 seq 46: ready 164 ms before start, 140 ms of it with an idle slot in group 41
- [cross_source_blocking] source 8826 seq 20: ready 129 ms before start, 103 ms of it with an idle slot in group 306
- [cross_source_blocking] source 10828 seq 16: ready 146 ms before start, 126 ms of it with an idle slot in group 354
- [cross_source_blocking] source 1054 seq 19: ready 209 ms before start, 209 ms of it with an idle slot in group 32
- [cross_source_blocking] source 6 seq 16: ready 140 ms before start, 140 ms of it with an idle slot in group 0
- [cross_source_blocking] source 15387 seq 46: ready 139 ms before start, 110 ms of it with an idle slot in group 481
- [cross_source_blocking] source 12742 seq 15: ready 271 ms before start, 144 ms of it with an idle slot in group 402
- [cross_source_blocking] source 10892 seq 22: ready 147 ms before start, 127 ms of it with an idle slot in group 354
- [cross_source_blocking] source 4852 seq 13: ready 152 ms before start, 121 ms of it with an idle slot in group 168
- [cross_source_blocking] source 15726 seq 24: ready 157 ms before start, 123 ms of it with an idle slot in group 490

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 8972 | 8972 | 0 | 5144.575 | 20316.159 | 23068.671 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2979 | 2979 | 0 | 3620.863 | 18481.151 | 20578.303 |
| t-473 | 32 | 1 | 91.6 | 49.759 | 4074 | 0 | 3063 | 3063 | 0 | 4915.199 | 17432.575 | 21233.663 |
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 29271 | 29271 | 0 | 3162.111 | 16908.287 | 22413.311 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8722 | 8722 | 0 | 2539.519 | 16646.143 | 21757.951 |
| t-481 | 12 | 1 | 263.464 | 49.674 | 1993 | 0 | 3003 | 3003 | 0 | 4128.767 | 16515.071 | 18743.295 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 35556 | 35556 | 0 | 3227.647 | 15794.175 | 20447.231 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 26207 | 26207 | 0 | 2998.271 | 15597.567 | 20447.231 |
| t-161 | 41 | 2 | 132.357 | 89.666 | 339 | 0 | 5510 | 5510 | 0 | 2719.743 | 14024.703 | 19005.439 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11736 | 11736 | 0 | 3522.559 | 14024.703 | 18743.295 |
| t-105 | 33 | 2 | 159.618 | 91.774 | 851 | 0 | 5678 | 5678 | 0 | 3719.167 | 13631.487 | 15400.959 |
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 14328 | 14328 | 0 | 2834.431 | 12779.519 | 14548.991 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17300 | 17300 | 0 | 2818.047 | 12779.519 | 15532.031 |
| t-497 | 57 | 1 | 49.416 | 45.807 | 912 | 0 | 3032 | 3032 | 0 | 2293.759 | 12517.375 | 16318.463 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2828 | 2828 | 0 | 2342.911 | 11665.407 | 13762.559 |
| t-313 | 35 | 1 | 76.092 | 45.564 | 510 | 0 | 2863 | 2863 | 0 | 2506.751 | 11141.119 | 14221.311 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5659 | 5659 | 0 | 1974.271 | 10158.079 | 13697.023 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 22027 | 22027 | 0 | 1638.399 | 9175.039 | 12648.447 |
| t-361 | 36 | 1 | 76.363 | 45.398 | 1404 | 0 | 2867 | 2867 | 0 | 2195.455 | 8912.895 | 11141.119 |
| t-425 | 89 | 6 | 180.363 | 263.66 | 2911 | 0 | 16189 | 16189 | 0 | 1392.639 | 8224.767 | 11927.551 |
| t-186 | 34 | 3 | 225.489 | 127.185 | 1259 | 0 | 7792 | 7792 | 0 | 1613.823 | 7831.551 | 8912.895 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16690 | 16690 | 0 | 1441.791 | 7536.639 | 10354.687 |
| t-216 | 19 | 1 | 132.344 | 42.802 | 3889 | 0 | 2588 | 2588 | 0 | 1269.759 | 7438.335 | 8585.215 |
| t-106 | 24 | 4 | 386.38 | 157.097 | 824 | 0 | 9374 | 9374 | 0 | 819.199 | 7012.351 | 8454.143 |
| t-128 | 11 | 1 | 220.431 | 40.915 | 260 | 0 | 2428 | 2428 | 0 | 729.087 | 6914.047 | 7733.247 |
