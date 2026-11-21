# N24-332-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 86.04s, closing 0.684s, drain 5.85s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 589477 | 589477 | 9609.233 | 9657.735 | 12633 | 0 | 0 | 0 | 0 | 2813 | 11068 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 476681 | 141.549 | 61.375 | 232.191 | 1847.295 | 2959.359 | 5578.751 |
| e2e_from_send | 476681 | 126.935 | 57.759 | 193.535 | 1811.455 | 2924.543 | 5578.751 |
| producer_lag | 476681 | 14.615 | 1.931 | 10.023 | 381.695 | 990.719 | 1370.111 |
| publish_call | 589477 | 66.324 | 27.199 | 157.567 | 581.119 | 980.479 | 1989.631 |
| queue_wait | 476681 | 90.886 | 30.431 | 149.119 | 1484.799 | 2707.455 | 4800.511 |
| broker_dwell | 476681 | 57.585 | 7.875 | 65.855 | 1447.935 | 2670.591 | 4775.935 |
| receive_call | 541819 | 30.771 | 17.567 | 68.863 | 231.295 | 444.159 | 602.623 |
| ctx_load | 476681 | 7.798 | 5.523 | 15.583 | 37.855 | 68.799 | 376.575 |
| process | 476681 | 9.907 | 0.002 | 0.004 | 0.072 | 2615.295 | 3178.495 |
| ctx_commit | 476681 | 18.167 | 12.551 | 38.687 | 86.143 | 146.687 | 416.511 |
| ack | 476680 | 0.011 | 0.004 | 0.009 | 0.07 | 1.293 | 55.135 |
| service | 476681 | 36.049 | 20.415 | 51.519 | 115.711 | 2633.727 | 3356.671 |
| ready_wait | 476681 | 15.367 | 4.211 | 37.215 | 181.247 | 452.095 | 799.743 |
| blocked | 476681 | 14.088 | 4.191 | 33.535 | 160.895 | 435.967 | 766.463 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9657.735 | 12529.2 |
| applied_total | 340256.812 | 564301 |
| bench_cpu_cores | 3.645 | 4.661 |
| bench_cpu_pct | 15.189 | 19.42 |
| bench_gc_gen0 | 1.167 | 2 |
| bench_gc_gen1 | 0.271 | 1 |
| bench_gc_gen2 | 0.083 | 1 |
| bench_gc_heap_mb | 522.629 | 767.9 |
| bench_mem_mb | 884.894 | 1051.8 |
| bench_threadpool_queue | 75.042 | 326 |
| bench_threadpool_threads | 38.104 | 44 |
| broker_backlog | 5470.729 | 12816 |
| ctr_eb_mssql_cpu_pct | 3.833 | 4.27 |
| ctr_eb_mssql_mem_mb | 7508.281 | 7511.1 |
| ctr_eb_mysql_cpu_pct | 1.094 | 1.22 |
| ctr_eb_mysql_mem_mb | 10634.467 | 10662.1 |
| ctr_eb_nats_octopus_cpu_pct | 126.225 | 131.4 |
| ctr_eb_nats_octopus_mem_mb | 75.219 | 89.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 26.693 | 28.94 |
| ctr_eb_pgbouncer_2_mem_mb | 18.09 | 19 |
| ctr_eb_pgbouncer_3_cpu_pct | 27.972 | 29.78 |
| ctr_eb_pgbouncer_3_mem_mb | 18.069 | 19 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.953 | 29.48 |
| ctr_eb_pgbouncer_4_mem_mb | 18.427 | 19.1 |
| ctr_eb_pgbouncer_cpu_pct | 26.254 | 28.17 |
| ctr_eb_pgbouncer_mem_mb | 19.167 | 20 |
| ctr_eb_postgres_cpu_pct | 902.532 | 986.57 |
| ctr_eb_postgres_mem_mb | 4611.365 | 4888.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.19 | 82.18 |
| in_flight | 779.354 | 2090 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.107 | 2.2 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 673422.896 | 1147832 |
| octopus_missing_reports_total | 24272.938 | 30290 |
| octopus_mqtt_received_total | 172711.625 | 284378 |
| octopus_mqtt_sent_total | 290712.125 | 496140 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.25 | 34 |
| pg_active_loads | 7.062 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 39.312 | 137 |
| pg_conn_idle | 224.083 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.083 | 1 |
| pg_conn_total | 263.396 | 264 |
| pg_db_size_mb | 3054.44 | 3306.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25374.873 | 27208.5 |
| pg_tup_del_per_s | 24745.731 | 31760 |
| pg_tup_fetch_per_s | 229729.865 | 274669.5 |
| pg_tup_ins_per_s | 30270.015 | 36197.3 |
| pg_tup_upd_per_s | 49076.194 | 55649.3 |
| pg_wal_mb_per_s | 81.55 | 99.13 |
| pgb_cl_active | 554.604 | 576 |
| pgb_cl_waiting | 0.312 | 5 |
| pgb_maxwait_ms | 0.06 | 0.96 |
| pgb_sv_active | 18.625 | 48 |
| pgb_sv_idle | 29.375 | 44 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 9609.233 | 11821.9 |
| produced_total | 341036.167 | 565077 |
| publish_mb_per_s | 15.139 | 18.52 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 159,133 records are Completed |
| record_counts | pass | 159,133 records found, 76,055 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 172 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 589,477 events applied by workers, 589,477 generated |
| broker_drained | **FAIL** | 2,159 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 11,068 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 161 ms, max 766 ms) |

Sample mismatches:

- source 12822: record 55070070669315 left in status Open (version 3)
- source 12830: record 55104430407683 left in status Open (version 3)
- source 3713: record 15947213570049 left in status Open (version 6)
- source 6640: record 28518582845443 left in status Open (version 1)
- source 7758: record 33320356282371 left in status Open (version 2)
- source 13868: record 59562606460931 left in status Open (version 3)
- source 1534: record 6588479832065 left in status Open (version 10)
- source 3039: record 13052405612545 left in status Open (version 8)
- source 765: record 3285649981442 left in status Open (version 4)
- source 7742: record 33251636805634 left in status Open (version 4)
- source 3717: record 15964393439234 left in status Open (version 3)
- source 15626: record 67113158967298 left in status Open (version 6)
- source 13822: record 59365037965317 left in status Open (version 7)
- source 13743: record 59025735548931 left in status Open (version 3)
- source 6018: record 25847113187331 left in status Open (version 5)
- source 4017: record 17252883628033 left in status Open (version 2)
- source 11948: record 51316269252609 left in status Open (version 9)
- source 7686: record 33011118637057 left in status Open (version 2)
- source 433: record 1859720839169 left in status Open (version 1)
- source 8715: record 37430639984641 left in status Open (version 2)

## Cross-source blocking samples

- [cross_source_blocking] source 5658 seq 28: ready 134 ms before start, 124 ms of it with an idle slot in group 194
- [cross_source_blocking] source 6838 seq 7: ready 138 ms before start, 123 ms of it with an idle slot in group 233
- [cross_source_blocking] source 4045 seq 7: ready 160 ms before start, 155 ms of it with an idle slot in group 136
- [cross_source_blocking] source 5623 seq 28: ready 140 ms before start, 140 ms of it with an idle slot in group 194
- [cross_source_blocking] source 11651 seq 21: ready 166 ms before start, 104 ms of it with an idle slot in group 378
- [cross_source_blocking] source 4610 seq 21: ready 148 ms before start, 144 ms of it with an idle slot in group 161
- [cross_source_blocking] source 13918 seq 7: ready 184 ms before start, 184 ms of it with an idle slot in group 442
- [cross_source_blocking] source 2625 seq 17: ready 186 ms before start, 160 ms of it with an idle slot in group 80
- [cross_source_blocking] source 4147 seq 16: ready 126 ms before start, 126 ms of it with an idle slot in group 136
- [cross_source_blocking] source 5652 seq 23: ready 167 ms before start, 167 ms of it with an idle slot in group 194
- [cross_source_blocking] source 5662 seq 30: ready 172 ms before start, 172 ms of it with an idle slot in group 194
- [cross_source_blocking] source 15379 seq 49: ready 188 ms before start, 156 ms of it with an idle slot in group 481
- [cross_source_blocking] source 13123 seq 21: ready 184 ms before start, 171 ms of it with an idle slot in group 417
- [cross_source_blocking] source 10109 seq 32: ready 161 ms before start, 161 ms of it with an idle slot in group 344
- [cross_source_blocking] source 79 seq 8: ready 162 ms before start, 162 ms of it with an idle slot in group 1
- [cross_source_blocking] source 3623 seq 24: ready 162 ms before start, 162 ms of it with an idle slot in group 122
- [cross_source_blocking] source 15861 seq 39: ready 150 ms before start, 150 ms of it with an idle slot in group 496
- [cross_source_blocking] source 10776 seq 15: ready 104 ms before start, 104 ms of it with an idle slot in group 353
- [cross_source_blocking] source 327 seq 14: ready 162 ms before start, 162 ms of it with an idle slot in group 9
- [cross_source_blocking] source 3342 seq 8: ready 156 ms before start, 156 ms of it with an idle slot in group 114

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8137 | 8137 | 0 | 72.191 | 3031.039 | 4849.663 |
| t-418 | 6 | 1 | 225.2 | 22.744 | 201 | 0 | 1432 | 1432 | 0 | 64.767 | 2818.047 | 4079.615 |
| t-314 | 24 | 3 | 348.915 | 139.029 | 525 | 0 | 8387 | 8387 | 0 | 68.607 | 2506.751 | 4177.919 |
| t-266 | 6 | 1 | 210.992 | 20.636 | 714 | 0 | 1275 | 1275 | 0 | 63.999 | 2490.367 | 3309.567 |
| t-369 | 14 | 2 | 246.802 | 60.602 | 1313 | 0 | 3796 | 3796 | 0 | 69.119 | 2473.983 | 4194.303 |
| t-146 | 6 | 1 | 80.521 | 8.639 | 246 | 0 | 521 | 521 | 0 | 62.207 | 2457.599 | 2834.431 |
| t-026 | 6 | 1 | 224.791 | 22.545 | 1596 | 0 | 1371 | 1371 | 0 | 62.207 | 2441.215 | 3342.335 |
| t-090 | 7 | 1 | 74.269 | 8.737 | 242 | 0 | 552 | 552 | 0 | 59.391 | 2392.063 | 3260.415 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 7634 | 7634 | 0 | 67.583 | 2342.911 | 3850.239 |
| t-041 | 34 | 5 | 361.724 | 203.757 | 2732 | 0 | 12205 | 12205 | 0 | 68.607 | 2326.527 | 3735.551 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5348 | 5348 | 0 | 67.071 | 2326.527 | 2965.503 |
| t-137 | 10 | 1 | 40.549 | 6.824 | 1569 | 0 | 428 | 428 | 0 | 56.319 | 2260.991 | 2932.735 |
| t-145 | 6 | 1 | 95.658 | 9.638 | 517 | 0 | 537 | 537 | 0 | 58.879 | 2260.991 | 3211.263 |
| t-298 | 6 | 1 | 119.198 | 13.108 | 3700 | 0 | 800 | 800 | 0 | 60.415 | 2260.991 | 3031.039 |
| t-424 | 8 | 1 | 142.018 | 18.635 | 1700 | 0 | 1071 | 1071 | 0 | 59.391 | 2228.223 | 2932.735 |
| t-401 | 11 | 1 | 56.122 | 10.542 | 360 | 0 | 664 | 664 | 0 | 57.343 | 2211.839 | 3293.183 |
| t-065 | 80 | 9 | 315.578 | 416.57 | 263 | 0 | 25173 | 25173 | 0 | 67.583 | 2162.687 | 4718.591 |
| t-496 | 81 | 8 | 270.852 | 368.2 | 278 | 0 | 22176 | 22176 | 0 | 65.279 | 2162.687 | 3817.471 |
| t-194 | 78 | 5 | 182.864 | 244.07 | 1184 | 0 | 14804 | 14804 | 0 | 62.207 | 2146.303 | 4849.663 |
| t-242 | 28 | 2 | 116.963 | 53.618 | 1662 | 0 | 3280 | 3280 | 0 | 60.159 | 2129.919 | 3653.631 |
| t-322 | 27 | 2 | 121.033 | 53.095 | 2198 | 0 | 3295 | 3295 | 0 | 59.391 | 2129.919 | 3047.423 |
| t-352 | 16 | 2 | 194.207 | 51.05 | 2088 | 0 | 3082 | 3082 | 0 | 62.975 | 2129.919 | 2965.503 |
| t-000 | 14 | 1 | 81.788 | 19.51 | 427 | 0 | 1130 | 1130 | 0 | 57.087 | 2080.767 | 2637.823 |
| t-018 | 26 | 1 | 94.693 | 41.631 | 315 | 0 | 2600 | 2600 | 0 | 62.463 | 2080.767 | 3424.255 |
| t-288 | 6 | 1 | 107.988 | 10.923 | 214 | 0 | 637 | 637 | 0 | 57.599 | 2072.575 | 3047.423 |
