# R24-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.488s, closing 0.754s, drain 3.883s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 472931 | 472931 | 7647.159 | 7595.68 | 10341 | 0 | 0 | 0 | 56 | 2292 | 85979 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 382124 | 336.852 | 177.663 | 624.127 | 2912.255 | 8081.407 | 11329.535 |
| e2e_from_send | 382124 | 318.733 | 163.583 | 578.047 | 2887.679 | 8024.063 | 11255.807 |
| producer_lag | 382124 | 18.119 | 3.051 | 51.615 | 228.351 | 447.487 | 1057.791 |
| publish_call | 472931 | 76.207 | 56.223 | 156.799 | 336.383 | 482.559 | 815.615 |
| queue_wait | 382124 | 267.732 | 118.271 | 508.927 | 2736.127 | 7954.431 | 11231.231 |
| broker_dwell | 382124 | 222.446 | 68.415 | 456.703 | 2689.023 | 7905.279 | 11206.655 |
| receive_call | 405804 | 31.142 | 16.815 | 74.815 | 235.391 | 420.095 | 574.463 |
| ctx_load | 382124 | 12.97 | 9.151 | 26.335 | 69.887 | 128.767 | 228.223 |
| process | 382124 | 9.915 | 0.002 | 0.005 | 0.084 | 2603.007 | 3098.623 |
| ctx_commit | 382124 | 27.871 | 19.999 | 59.295 | 130.431 | 193.919 | 954.879 |
| ack | 382115 | 0.014 | 0.004 | 0.009 | 0.073 | 1.801 | 68.927 |
| service | 382124 | 51.002 | 32.959 | 81.407 | 170.495 | 2648.063 | 3323.903 |
| e2e_large | 46 | 3392.134 | 3018.751 | 5029.887 | 9715.711 | 9715.711 | 9715.711 |
| ready_wait | 382124 | 75.406 | 40.607 | 196.863 | 426.239 | 764.415 | 3059.711 |
| blocked | 382124 | 66.9 | 37.791 | 173.695 | 361.983 | 545.279 | 1116.159 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7595.68 | 9697.7 |
| applied_total | 272177.694 | 453502 |
| bench_cpu_cores | 3.523 | 4.509 |
| bench_cpu_pct | 14.679 | 18.79 |
| bench_gc_gen0 | 1.449 | 4 |
| bench_gc_gen1 | 0.551 | 2 |
| bench_gc_gen2 | 0.367 | 2 |
| bench_gc_heap_mb | 706.143 | 1037.2 |
| bench_mem_mb | 1101.68 | 1311.1 |
| bench_threadpool_queue | 47.388 | 400 |
| bench_threadpool_threads | 38.776 | 44 |
| broker_backlog | 9891.286 | 13435 |
| ctr_eb_mosquitto_cpu_pct | 89.801 | 91.87 |
| ctr_eb_mosquitto_mem_mb | 13.224 | 24.5 |
| ctr_eb_mssql_cpu_pct | 4.427 | 6.53 |
| ctr_eb_mssql_mem_mb | 7473.888 | 7474.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 32.029 | 34.15 |
| ctr_eb_pgbouncer_2_mem_mb | 21.339 | 22.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 31.937 | 35.85 |
| ctr_eb_pgbouncer_3_mem_mb | 21.873 | 22.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 31.983 | 34.55 |
| ctr_eb_pgbouncer_4_mem_mb | 18.829 | 20.2 |
| ctr_eb_pgbouncer_cpu_pct | 31.371 | 33.73 |
| ctr_eb_pgbouncer_mem_mb | 18.778 | 22.1 |
| ctr_eb_postgres_cpu_pct | 1015.486 | 1060.96 |
| ctr_eb_postgres_mem_mb | 5776.582 | 5960.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.917 | 85.26 |
| in_flight | 1891.837 | 4068 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.314 | 2.42 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 540670.959 | 923093 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 172253.061 | 278996 |
| octopus_mqtt_sent_total | 231834.449 | 391530 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.857 | 36 |
| pg_active_loads | 7.286 | 17 |
| pg_active_polls | 0.061 | 1 |
| pg_active_sends | 0.061 | 1 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 39.918 | 106 |
| pg_conn_idle | 222.857 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.98 | 16 |
| pg_conn_total | 262.776 | 264 |
| pg_db_size_mb | 3520.561 | 3906.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25281.561 | 27484.8 |
| pg_tup_del_per_s | 31967.806 | 36868.2 |
| pg_tup_fetch_per_s | 268309.61 | 297757.7 |
| pg_tup_ins_per_s | 42972.463 | 50210.8 |
| pg_tup_upd_per_s | 52482.792 | 56516.6 |
| pg_wal_mb_per_s | 113.266 | 149.34 |
| pgb_cl_active | 702.388 | 724 |
| pgb_cl_waiting | 15.306 | 46 |
| pgb_maxwait_ms | 2.333 | 12.07 |
| pgb_sv_active | 30.776 | 48 |
| pgb_sv_idle | 17.224 | 42 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7647.159 | 8971.6 |
| produced_total | 274069.531 | 457332 |
| publish_mb_per_s | 18.617 | 36.55 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5012/5012 source cursors at the expected last sequence |
| records_complete | pass | all 172,069 records are Completed |
| record_counts | pass | 172,069 records found, 61,815 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 383 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 472,931 events applied by workers, 472,931 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 85,979 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 362 ms, max 1116 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6518 seq 21: ready 135 ms before start, 135 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6544 seq 30: ready 144 ms before start, 144 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6565 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6548 seq 17: ready 147 ms before start, 147 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6621 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6527 seq 21: ready 136 ms before start, 136 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6591 seq 16: ready 151 ms before start, 151 ms of it with an idle slot in group 222
- [cross_source_blocking] source 11655 seq 29: ready 135 ms before start, 119 ms of it with an idle slot in group 378
- [cross_source_blocking] source 6505 seq 17: ready 174 ms before start, 174 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6607 seq 23: ready 140 ms before start, 140 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6506 seq 18: ready 142 ms before start, 142 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6524 seq 19: ready 143 ms before start, 143 ms of it with an idle slot in group 222
- [cross_source_blocking] source 11649 seq 30: ready 137 ms before start, 120 ms of it with an idle slot in group 378
- [cross_source_blocking] source 6610 seq 31: ready 146 ms before start, 146 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6602 seq 13: ready 128 ms before start, 128 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6501 seq 17: ready 153 ms before start, 153 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6522 seq 22: ready 155 ms before start, 155 ms of it with an idle slot in group 222
- [cross_source_blocking] source 13810 seq 20: ready 227 ms before start, 227 ms of it with an idle slot in group 438
- [cross_source_blocking] source 6550 seq 11: ready 113 ms before start, 113 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6570 seq 16: ready 143 ms before start, 143 ms of it with an idle slot in group 222

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-132 | 22 | 2 | 280.528 | 95.394 | 2676 | 0 | 5819 | 5819 | 0 | 1335.295 | 9895.935 | 11337.727 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 92 | 92 | 0 | 2867.199 | 6389.759 | 9764.863 |
| t-018 | 26 | 1 | 94.693 | 41.631 | 315 | 0 | 2569 | 2569 | 0 | 374.783 | 5832.703 | 7143.423 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5569 | 5569 | 0 | 385.023 | 4685.823 | 6389.759 |
| t-066 | 38 | 1 | 77.996 | 49.178 | 277 | 0 | 3077 | 3077 | 0 | 614.399 | 4325.375 | 6881.279 |
| t-348 | 125 | 5 | 117.187 | 245.621 | 1340 | 0 | 15214 | 15214 | 0 | 450.559 | 3473.407 | 6488.063 |
| t-468 | 61 | 5 | 236.273 | 236.318 | 1645 | 0 | 14242 | 14242 | 0 | 229.375 | 3358.719 | 7208.959 |
| t-120 | 37 | 2 | 154.992 | 94.103 | 1412 | 0 | 5688 | 5688 | 0 | 387.071 | 3342.335 | 5144.575 |
| t-246 | 32 | 4 | 349.163 | 184.479 | 494 | 0 | 11178 | 11178 | 0 | 264.191 | 3293.183 | 5996.543 |
| t-333 | 79 | 1 | 36.943 | 47.58 | 192 | 0 | 3099 | 3099 | 0 | 421.887 | 3162.111 | 4554.751 |
| t-075 | 37 | 2 | 140.402 | 86.721 | 232 | 0 | 5184 | 5184 | 0 | 231.423 | 2998.271 | 5275.647 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 7636 | 7636 | 0 | 232.447 | 2883.583 | 4751.359 |
| t-378 | 55 | 3 | 159.715 | 143.096 | 3659 | 0 | 8880 | 8880 | 0 | 260.095 | 2834.431 | 4980.735 |
| t-342 | 15 | 2 | 259.618 | 64.924 | 146 | 0 | 3900 | 3900 | 0 | 159.743 | 2768.895 | 4079.615 |
| t-450 | 63 | 3 | 141.433 | 146.465 | 165 | 0 | 8916 | 8916 | 0 | 292.863 | 2670.591 | 4292.607 |
| t-288 | 6 | 1 | 107.988 | 10.923 | 214 | 0 | 700 | 700 | 0 | 140.287 | 2654.207 | 3162.111 |
| t-462 | 10 | 1 | 47.716 | 7.742 | 2006 | 0 | 511 | 511 | 0 | 122.367 | 2621.439 | 3063.807 |
| t-474 | 63 | 1 | 34.265 | 35.344 | 375 | 0 | 2343 | 2343 | 0 | 158.719 | 2605.055 | 4292.607 |
| t-105 | 33 | 2 | 136.816 | 78.663 | 851 | 0 | 4817 | 4817 | 0 | 171.007 | 2588.671 | 3489.791 |
| t-447 | 17 | 1 | 84.261 | 22.739 | 558 | 0 | 1367 | 1367 | 0 | 141.311 | 2572.287 | 3096.575 |
| t-261 | 51 | 2 | 110.543 | 94.419 | 2342 | 0 | 5842 | 5842 | 0 | 246.783 | 2539.519 | 3407.871 |
| t-438 | 121 | 6 | 138.912 | 290.853 | 1839 | 0 | 17891 | 17891 | 0 | 278.527 | 2490.367 | 5013.503 |
| t-171 | 14 | 1 | 179.386 | 42.55 | 142 | 0 | 2615 | 2615 | 0 | 186.367 | 2473.983 | 3227.647 |
| t-369 | 14 | 2 | 246.802 | 60.602 | 1313 | 0 | 3593 | 3593 | 0 | 156.671 | 2457.599 | 4816.895 |
| t-186 | 34 | 3 | 193.276 | 109.016 | 1259 | 0 | 6630 | 6630 | 0 | 168.959 | 2441.215 | 3325.951 |
