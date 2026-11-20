# mt-smoke — FAIL ❌

- Broker: `MassTransit SQL transport (Postgres 127.0.0.1:5432/bench, schema transport), queue per group, partition key = source, PartitionedOrdered limit 1, connection limit 50, unlock delay 250 ms, max delivery 1000000`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 511 workers, offered 3862.838 ev/s
- Phases: run 20.108s, closing 4.659s, drain 5.602s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 68864 | 68864 | 3036.543 | 3072.714 | 5721.6 | 0 | 0 | 0 | 125 | 371 | 2817 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 26198 | 7434.597 | 7299.071 | 8953.855 | 11534.335 | 13656.063 | 21135.359 |
| e2e_from_send | 26198 | 223.979 | 20.991 | 479.231 | 3913.727 | 5955.583 | 11730.943 |
| producer_lag | 26198 | 7210.596 | 7131.135 | 8658.943 | 9904.127 | 10592.255 | 10625.023 |
| publish_call | 68864 | 11.394 | 7.087 | 11.959 | 89.215 | 850.943 | 1112.063 |
| queue_wait | 26198 | 201.979 | 16.575 | 399.359 | 3715.071 | 5795.839 | 9912.319 |
| broker_dwell | 26198 | 201.679 | 16.279 | 399.103 | 3715.071 | 5795.839 | 9912.319 |
| receive_call | 65429 | 79.442 | 30.031 | 240.127 | 459.775 | 495.871 | 1179.647 |
| ctx_load | 26198 | 1.479 | 1.227 | 2.771 | 5.755 | 10.007 | 19.135 |
| process | 26198 | 15.811 | 0.001 | 0.003 | 0.022 | 2846.719 | 3440.639 |
| ctx_commit | 26198 | 4.203 | 2.207 | 5.087 | 40.831 | 352.767 | 947.199 |
| ack | 19050 | 0.001 | 0.001 | 0.001 | 0.002 | 0.035 | 0.99 |
| service | 26198 | 22.003 | 3.785 | 7.891 | 81.791 | 2940.927 | 3928.063 |
| e2e_large | 81 | 11722.373 | 11395.071 | 15253.503 | 18956.287 | 21135.359 | 21135.359 |
| ready_wait | 26198 | 147.588 | 8.663 | 134.783 | 2842.623 | 5099.519 | 6905.855 |
| blocked | 26198 | 147.398 | 8.663 | 133.119 | 2840.575 | 5099.519 | 6905.855 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3072.714 | 4152.3 |
| applied_total | 31480 | 50281 |
| bench_cpu_cores | 3.16 | 4.733 |
| bench_cpu_pct | 13.17 | 19.72 |
| bench_gc_gen0 | 2.429 | 3 |
| bench_gc_gen1 | 0.786 | 2 |
| bench_gc_gen2 | 0.714 | 1 |
| bench_gc_heap_mb | 1412.171 | 1863.7 |
| bench_mem_mb | 1908.707 | 2397.6 |
| bench_threadpool_queue | 23.286 | 122 |
| bench_threadpool_threads | 44.857 | 45 |
| broker_backlog | 631.429 | 736 |
| ctr_eb_pgbouncer_2_cpu_pct | 3.532 | 3.86 |
| ctr_eb_pgbouncer_2_mem_mb | 1.5 | 1.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 6.224 | 6.89 |
| ctr_eb_pgbouncer_3_mem_mb | 1.7 | 1.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 7.865 | 9.27 |
| ctr_eb_pgbouncer_4_mem_mb | 1.679 | 1.8 |
| ctr_eb_pgbouncer_cpu_pct | 7.933 | 8.85 |
| ctr_eb_pgbouncer_mem_mb | 1.943 | 2 |
| ctr_eb_postgres_cpu_pct | 1247.912 | 1463.31 |
| ctr_eb_postgres_mem_mb | 1524.207 | 2170.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.295 | 83.08 |
| in_flight | 605.929 | 872 |
| mt_error_or_dead_letter_rows | 0 | 0 |
| mt_faulted_total | 0 | 0 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.357 | 9 |
| pg_active_loads | 1.071 | 2 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 23 | 35 |
| pg_conn_idle | 78.714 | 87 |
| pg_conn_idle_in_tx | 15.714 | 23 |
| pg_conn_lock_wait | 0.143 | 1 |
| pg_conn_total | 117.429 | 119 |
| pg_db_size_mb | 2562.671 | 2908.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 29305.807 | 32913.9 |
| pg_tup_del_per_s | 20256.043 | 32087.6 |
| pg_tup_fetch_per_s | 113307.25 | 155001 |
| pg_tup_ins_per_s | 34259.1 | 46832.2 |
| pg_tup_upd_per_s | 6884.714 | 8534.4 |
| pg_wal_mb_per_s | 58.603 | 83.77 |
| pgb_cl_active | 16.643 | 20 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0.429 | 2 |
| pgb_sv_idle | 15.786 | 17 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 3036.543 | 3924.7 |
| produced_total | 32085.929 | 50762 |
| publish_mb_per_s | 33.83 | 82.29 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 21,232 records are Completed |
| record_counts | pass | 21,232 records found, 21,232 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 125 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 68,864 events applied by workers, 68,864 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 2,817 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2841 ms, max 6906 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1547 seq 5: ready 126 ms before start, 126 ms of it with an idle slot in group 46
- [cross_source_blocking] source 6536 seq 2: ready 137 ms before start, 137 ms of it with an idle slot in group 222
- [cross_source_blocking] source 8836 seq 6: ready 212 ms before start, 212 ms of it with an idle slot in group 306
- [cross_source_blocking] source 8813 seq 2: ready 183 ms before start, 183 ms of it with an idle slot in group 306
- [cross_source_blocking] source 14920 seq 4: ready 721 ms before start, 721 ms of it with an idle slot in group 468
- [cross_source_blocking] source 2501 seq 3: ready 324 ms before start, 324 ms of it with an idle slot in group 75
- [cross_source_blocking] source 14952 seq 4: ready 733 ms before start, 733 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14917 seq 4: ready 670 ms before start, 670 ms of it with an idle slot in group 468
- [cross_source_blocking] source 9666 seq 5: ready 107 ms before start, 107 ms of it with an idle slot in group 334
- [cross_source_blocking] source 14938 seq 3: ready 467 ms before start, 467 ms of it with an idle slot in group 468
- [cross_source_blocking] source 8869 seq 4: ready 199 ms before start, 199 ms of it with an idle slot in group 306
- [cross_source_blocking] source 14906 seq 5: ready 273 ms before start, 273 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14949 seq 3: ready 316 ms before start, 316 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14925 seq 4: ready 385 ms before start, 385 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14907 seq 2: ready 175 ms before start, 175 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14939 seq 3: ready 203 ms before start, 203 ms of it with an idle slot in group 468
- [cross_source_blocking] source 8898 seq 2: ready 285 ms before start, 285 ms of it with an idle slot in group 306
- [cross_source_blocking] source 5144 seq 1: ready 112 ms before start, 112 ms of it with an idle slot in group 178
- [cross_source_blocking] source 5947 seq 2: ready 181 ms before start, 181 ms of it with an idle slot in group 206
- [cross_source_blocking] source 3896 seq 4: ready 198 ms before start, 198 ms of it with an idle slot in group 131

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 137 | 137 | 0 | 11796.479 | 19267.583 | 21233.663 |
| t-306 | 134 | 2 | 22.544 | 50.414 | 2978 | 0 | 757 | 757 | 0 | 11534.335 | 14221.311 | 14417.919 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 56 | 56 | 0 | 9830.399 | 14155.775 | 14155.775 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 24 | 24 | 0 | 10551.295 | 13238.271 | 13238.271 |
| t-131 | 140 | 1 | 18.254 | 43.004 | 961 | 0 | 691 | 691 | 0 | 9502.719 | 12976.127 | 13697.023 |
| t-425 | 89 | 1 | 24.935 | 36.451 | 2911 | 0 | 608 | 608 | 0 | 9175.039 | 12910.591 | 13565.951 |
| t-417 | 75 | 1 | 13.222 | 16.434 | 3449 | 0 | 306 | 306 | 0 | 7700.479 | 12255.231 | 12910.591 |
| t-461 | 130 | 1 | 13.509 | 29.389 | 3025 | 0 | 525 | 525 | 0 | 7438.335 | 12058.623 | 12648.447 |
| t-466 | 11 | 1 | 11.858 | 1.968 | 449 | 0 | 38 | 38 | 0 | 7340.031 | 11862.015 | 11862.015 |
| t-152 | 43 | 1 | 11.302 | 7.978 | 882 | 0 | 154 | 154 | 0 | 7307.263 | 11730.943 | 12189.695 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 16 | 16 | 0 | 9502.719 | 11665.407 | 11665.407 |
| t-128 | 11 | 1 | 30.474 | 5.656 | 260 | 0 | 79 | 79 | 0 | 8159.231 | 11599.871 | 11599.871 |
| t-333 | 79 | 1 | 5.959 | 7.674 | 192 | 0 | 212 | 212 | 0 | 9240.575 | 11599.871 | 11796.479 |
| t-162 | 69 | 1 | 3.166 | 3.645 | 353 | 0 | 107 | 107 | 0 | 7340.031 | 11534.335 | 11534.335 |
| t-200 | 62 | 1 | 8.519 | 8.706 | 348 | 0 | 172 | 172 | 0 | 6979.583 | 11534.335 | 12845.055 |
| t-045 | 47 | 1 | 24.784 | 19.313 | 1398 | 0 | 279 | 279 | 0 | 7733.247 | 11272.191 | 11272.191 |
| t-169 | 33 | 1 | 14.109 | 7.812 | 260 | 0 | 156 | 156 | 0 | 7340.031 | 11206.655 | 11403.263 |
| t-286 | 20 | 1 | 5.763 | 1.962 | 401 | 0 | 56 | 56 | 0 | 7307.263 | 11141.119 | 11141.119 |
| t-299 | 21 | 1 | 12.706 | 4.345 | 646 | 0 | 77 | 77 | 0 | 7340.031 | 11010.047 | 11010.047 |
| t-204 | 37 | 1 | 5.473 | 3.378 | 1822 | 0 | 92 | 92 | 0 | 7110.655 | 10813.439 | 10813.439 |
| t-107 | 19 | 1 | 12.371 | 3.842 | 453 | 0 | 73 | 73 | 0 | 8323.071 | 10682.367 | 10682.367 |
| t-154 | 18 | 1 | 6.242 | 1.849 | 2158 | 0 | 46 | 46 | 0 | 7110.655 | 10682.367 | 10682.367 |
| t-168 | 71 | 1 | 10.385 | 12.058 | 155 | 0 | 256 | 256 | 0 | 6914.047 | 10682.367 | 10878.975 |
| t-222 | 138 | 1 | 18.598 | 43.285 | 2846 | 0 | 689 | 689 | 0 | 7536.639 | 10682.367 | 11075.583 |
| t-258 | 52 | 1 | 12.388 | 10.649 | 2470 | 0 | 182 | 182 | 0 | 7045.119 | 10682.367 | 10682.367 |
