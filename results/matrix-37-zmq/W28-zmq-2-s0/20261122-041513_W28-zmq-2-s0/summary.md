# W28-zmq-2-s0 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.113s, closing 1.567s, drain 35.932s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 672126 | 672126 | 11557.414 | 10538.865 | 25372 | 0 | 0 | 0 | 0 | 3199 | 278789 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 543409 | 4242.639 | 3280.895 | 9617.407 | 16793.599 | 22904.831 | 46366.719 |
| e2e_from_send | 543409 | 2867.624 | 986.623 | 8708.095 | 16007.167 | 21774.335 | 40697.855 |
| producer_lag | 543409 | 1375.017 | 637.439 | 3872.767 | 5795.839 | 6471.679 | 6758.399 |
| publish_call | 672126 | 158.08 | 112.383 | 345.343 | 741.375 | 1599.487 | 2181.119 |
| queue_wait | 543409 | 2795.082 | 903.679 | 8650.751 | 15949.823 | 21741.567 | 40697.855 |
| broker_dwell | 543409 | 2672.704 | 776.191 | 8527.871 | 15851.519 | 21594.111 | 40632.319 |
| receive_call | 568025 | 25.049 | 11.263 | 62.655 | 223.359 | 470.015 | 623.615 |
| ctx_load | 543409 | 16.98 | 11.663 | 35.711 | 89.279 | 137.855 | 354.303 |
| process | 543409 | 9.892 | 0.002 | 0.004 | 0.076 | 2619.391 | 3108.863 |
| ctx_commit | 543409 | 45.32 | 35.263 | 95.871 | 187.391 | 319.231 | 421.119 |
| ack | 543353 | 0.015 | 0.004 | 0.008 | 0.068 | 2.317 | 84.351 |
| service | 543409 | 72.543 | 53.311 | 120.831 | 256.639 | 2678.783 | 3262.463 |
| ready_wait | 543409 | 217.411 | 137.471 | 517.631 | 1214.463 | 1972.223 | 10346.495 |
| blocked | 543409 | 160.4 | 105.087 | 381.951 | 808.447 | 1148.927 | 8871.935 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10538.865 | 13804.2 |
| applied_total | 348666.653 | 596812 |
| bench_cpu_cores | 3.977 | 4.991 |
| bench_cpu_pct | 16.572 | 20.79 |
| bench_gc_gen0 | 1.531 | 3 |
| bench_gc_gen1 | 0.531 | 2 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 537.557 | 867 |
| bench_mem_mb | 922.2 | 1232.9 |
| bench_threadpool_queue | 187.449 | 578 |
| bench_threadpool_threads | 42.286 | 44 |
| broker_backlog | 38840.714 | 71468 |
| ctr_eb_mssql_cpu_pct | 4.188 | 4.98 |
| ctr_eb_mssql_mem_mb | 4897.978 | 4898.1 |
| ctr_eb_mysql_cpu_pct | 1.152 | 1.26 |
| ctr_eb_mysql_mem_mb | 5950.982 | 5951.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 35.179 | 37.8 |
| ctr_eb_pgbouncer_2_mem_mb | 16.12 | 17 |
| ctr_eb_pgbouncer_3_cpu_pct | 33.421 | 35.12 |
| ctr_eb_pgbouncer_3_mem_mb | 15.555 | 16.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 35.549 | 38.95 |
| ctr_eb_pgbouncer_4_mem_mb | 16.531 | 17.3 |
| ctr_eb_pgbouncer_cpu_pct | 34.14 | 36.11 |
| ctr_eb_pgbouncer_mem_mb | 16.727 | 17.3 |
| ctr_eb_postgres_cpu_pct | 1081.169 | 1156.34 |
| ctr_eb_postgres_mem_mb | 6065.478 | 6698.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.884 | 86.45 |
| in_flight | 26608.286 | 55226 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.419 | 2.54 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 713505.755 | 1225771 |
| octopus_missing_reports_total | 37412.694 | 63302 |
| octopus_mqtt_received_total | 140828.571 | 235963 |
| octopus_mqtt_sent_total | 255754.959 | 426086 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.939 | 42 |
| pg_active_loads | 8.653 | 20 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.958 | 100 |
| pg_conn_active | 55.245 | 90 |
| pg_conn_idle | 208.918 | 238 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.816 | 19 |
| pg_conn_total | 264.163 | 265 |
| pg_db_size_mb | 3756.945 | 4351.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26151.112 | 30351.8 |
| pg_tup_del_per_s | 35211.567 | 39504.3 |
| pg_tup_fetch_per_s | 308838.373 | 362897.8 |
| pg_tup_ins_per_s | 47760.333 | 55933.9 |
| pg_tup_upd_per_s | 55359.355 | 65410.5 |
| pg_wal_mb_per_s | 128.104 | 186.04 |
| pgb_cl_active | 528.347 | 569 |
| pgb_cl_waiting | 12.388 | 91 |
| pgb_maxwait_ms | 2.134 | 16.39 |
| pgb_sv_active | 25.898 | 48 |
| pgb_sv_idle | 22.102 | 42 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11557.414 | 16683.1 |
| produced_total | 375274.939 | 648438 |
| publish_mb_per_s | 18.181 | 26.51 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 190,542 records are Completed |
| record_counts | pass | 190,542 records found, 85,349 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 387 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 672,126 events applied by workers, 672,126 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 278,789 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 808 ms, max 8864 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1975 seq 24: ready 116 ms before start, 104 ms of it with an idle slot in group 57
- [cross_source_blocking] source 8763 seq 21: ready 117 ms before start, 117 ms of it with an idle slot in group 305
- [cross_source_blocking] source 5843 seq 12: ready 114 ms before start, 114 ms of it with an idle slot in group 201
- [cross_source_blocking] source 8104 seq 31: ready 104 ms before start, 104 ms of it with an idle slot in group 274
- [cross_source_blocking] source 14248 seq 11: ready 159 ms before start, 159 ms of it with an idle slot in group 457
- [cross_source_blocking] source 15920 seq 9: ready 161 ms before start, 139 ms of it with an idle slot in group 497
- [cross_source_blocking] source 3526 seq 34: ready 159 ms before start, 159 ms of it with an idle slot in group 121
- [cross_source_blocking] source 11589 seq 11: ready 161 ms before start, 154 ms of it with an idle slot in group 376
- [cross_source_blocking] source 342 seq 21: ready 260 ms before start, 260 ms of it with an idle slot in group 10
- [cross_source_blocking] source 14700 seq 16: ready 161 ms before start, 161 ms of it with an idle slot in group 464
- [cross_source_blocking] source 5408 seq 20: ready 160 ms before start, 160 ms of it with an idle slot in group 184
- [cross_source_blocking] source 4710 seq 7: ready 161 ms before start, 156 ms of it with an idle slot in group 162
- [cross_source_blocking] source 4188 seq 12: ready 275 ms before start, 275 ms of it with an idle slot in group 138
- [cross_source_blocking] source 2645 seq 8: ready 166 ms before start, 149 ms of it with an idle slot in group 82
- [cross_source_blocking] source 4 seq 22: ready 115 ms before start, 115 ms of it with an idle slot in group 0
- [cross_source_blocking] source 4039 seq 18: ready 186 ms before start, 186 ms of it with an idle slot in group 136
- [cross_source_blocking] source 8838 seq 41: ready 194 ms before start, 194 ms of it with an idle slot in group 306
- [cross_source_blocking] source 483 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 16
- [cross_source_blocking] source 13337 seq 11: ready 210 ms before start, 210 ms of it with an idle slot in group 426
- [cross_source_blocking] source 13057 seq 14: ready 214 ms before start, 141 ms of it with an idle slot in group 416
- [cross_source_blocking] source 10310 seq 10: ready 212 ms before start, 212 ms of it with an idle slot in group 345

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 28695 | 28695 | 0 | 6979.583 | 27525.119 | 46399.487 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8569 | 8569 | 0 | 5603.327 | 21233.663 | 26738.687 |
| t-481 | 12 | 1 | 263.464 | 49.674 | 1993 | 0 | 2975 | 2975 | 0 | 6488.063 | 20709.375 | 23068.671 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2922 | 2922 | 0 | 5570.559 | 20185.087 | 21626.879 |
| t-473 | 32 | 1 | 91.6 | 49.759 | 4074 | 0 | 3008 | 3008 | 0 | 7340.031 | 20185.087 | 23855.103 |
| t-497 | 57 | 1 | 49.416 | 45.807 | 912 | 0 | 2989 | 2989 | 0 | 6225.919 | 20054.015 | 23724.031 |
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 8836 | 8836 | 0 | 7471.103 | 19922.943 | 24379.391 |
| t-161 | 41 | 2 | 132.357 | 89.666 | 339 | 0 | 5423 | 5423 | 0 | 5898.239 | 19791.871 | 23855.103 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11514 | 11514 | 0 | 6258.687 | 18612.223 | 23592.959 |
| t-313 | 35 | 1 | 76.092 | 45.564 | 510 | 0 | 2812 | 2812 | 0 | 6193.151 | 18350.079 | 21889.023 |
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 14081 | 14081 | 0 | 5996.543 | 17825.791 | 20447.231 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 16957 | 16957 | 0 | 5734.399 | 17825.791 | 20054.015 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5571 | 5571 | 0 | 4784.127 | 17694.719 | 22151.167 |
| t-105 | 33 | 2 | 159.618 | 91.774 | 851 | 0 | 5588 | 5588 | 0 | 6258.687 | 17563.647 | 19791.871 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 25776 | 25776 | 0 | 5701.631 | 17563.647 | 22282.239 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2793 | 2793 | 0 | 5373.951 | 17432.575 | 19005.439 |
| t-290 | 15 | 1 | 185.897 | 44.82 | 212 | 0 | 2721 | 2721 | 0 | 4718.591 | 16777.215 | 19005.439 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 21691 | 21691 | 0 | 5079.039 | 16711.679 | 22544.383 |
| t-425 | 89 | 6 | 180.363 | 263.66 | 2911 | 0 | 15902 | 15902 | 0 | 5013.503 | 16711.679 | 19660.799 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16434 | 16434 | 0 | 4620.287 | 16580.607 | 21102.591 |
| t-168 | 71 | 2 | 75.117 | 87.222 | 155 | 0 | 5325 | 5325 | 0 | 3883.007 | 16121.855 | 20447.231 |
| t-121 | 76 | 6 | 212.261 | 260.387 | 149 | 0 | 15554 | 15554 | 0 | 4423.679 | 15925.247 | 20709.375 |
| t-361 | 36 | 1 | 76.363 | 45.398 | 1404 | 0 | 2818 | 2818 | 0 | 4816.895 | 15925.247 | 20316.159 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 34977 | 34977 | 0 | 5931.007 | 15597.567 | 19267.583 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5543 | 5543 | 0 | 3670.015 | 15466.495 | 20447.231 |
