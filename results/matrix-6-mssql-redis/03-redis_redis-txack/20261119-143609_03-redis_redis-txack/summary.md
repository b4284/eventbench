# 03-redis_redis-txack — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.1s, closing 0.352s, drain 1.201s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814648 | 814648 | 12852.039 | 12851.724 | 21953 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642926 | 15.714 | 10.711 | 36.095 | 55.551 | 81.919 | 280.831 |
| e2e_from_send | 642926 | 14.67 | 9.535 | 34.303 | 53.695 | 74.111 | 280.575 |
| producer_lag | 642926 | 1.044 | 0.507 | 2.525 | 7.887 | 18.639 | 36.639 |
| publish_call | 572335 | 0.781 | 0.448 | 1.046 | 7.983 | 19.151 | 205.183 |
| queue_wait | 642926 | 13.044 | 8.187 | 32.479 | 51.999 | 59.775 | 135.039 |
| broker_dwell | 642926 | 12.534 | 7.715 | 32.047 | 51.455 | 56.639 | 132.607 |
| receive_call | 595312 | 1.168 | 0.635 | 1.864 | 10.727 | 28.735 | 131.007 |
| ctx_load | 642926 | 0.596 | 0.393 | 0.819 | 5.279 | 13.855 | 65.375 |
| process | 642926 | 0.002 | 0.001 | 0.002 | 0.004 | 0.019 | 6.023 |
| ctx_commit | 642926 | 0.592 | 0.377 | 0.827 | 5.475 | 13.863 | 68.479 |
| service | 642926 | 1.626 | 0.967 | 2.373 | 14.447 | 35.711 | 226.047 |
| e2e_large | 331 | 45.034 | 43.231 | 67.583 | 97.663 | 118.271 | 118.271 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12851.724 | 13113.7 |
| applied_total | 468731.571 | 777404 |
| bench_cpu_cores | 4.407 | 4.712 |
| bench_cpu_pct | 18.36 | 19.63 |
| bench_gc_gen0 | 4.49 | 10 |
| bench_gc_gen1 | 2.469 | 8 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 787.049 | 993.1 |
| bench_mem_mb | 1069.198 | 1195 |
| bench_threadpool_queue | 16.816 | 163 |
| bench_threadpool_threads | 35.265 | 40 |
| broker_backlog | 208.408 | 323 |
| ctr_eb_kafka_cpu_pct | 1.444 | 1.86 |
| ctr_eb_kafka_mem_mb | 3316.9 | 3316.9 |
| ctr_eb_mssql_cpu_pct | 2.645 | 17.66 |
| ctr_eb_mssql_mem_mb | 5633.537 | 5838.4 |
| ctr_eb_nats_cpu_pct | 0.547 | 2.93 |
| ctr_eb_nats_mem_mb | 419.945 | 526 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 0.221 | 0.4 |
| ctr_eb_postgres_mem_mb | 3114.7 | 3114.7 |
| ctr_eb_rabbitmq_cpu_pct | 7.831 | 8.47 |
| ctr_eb_rabbitmq_mem_mb | 716.704 | 720.8 |
| ctr_eb_redis_cpu_pct | 210.771 | 275.86 |
| ctr_eb_redis_mem_mb | 954.663 | 1412.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 12.66 | 26.4 |
| in_flight | 168.408 | 357 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12852.039 | 13130.7 |
| produced_total | 468899.98 | 777622 |
| publish_mb_per_s | 44.331 | 71.11 |
| redis_aof_buffer_length | 12929.367 | 52223 |
| redis_aof_current_size | 1332.029 | 2243.2 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 230044 | 233594 |
| redis_total_commands_processed_per_s | 230376.804 | 234516 |
| redis_total_net_input_bytes_per_s | 87105878.518 | 120345163.3 |
| redis_total_net_output_bytes_per_s | 60552648.492 | 80378885.5 |
| redis_used_cpu_sys_cores | 1.006 | 1.43 |
| redis_used_cpu_sys_main_thread_cores | 0.195 | 0.28 |
| redis_used_cpu_user_cores | 0.897 | 1.03 |
| redis_used_cpu_user_main_thread_cores | 0.44 | 0.47 |
| redis_used_memory | 899.955 | 1382.8 |
| redisq_aof_buffer_length | 11430.694 | 36516 |
| redisq_aof_current_size | 1331.91 | 2243 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 229994.286 | 233594 |
| redisq_total_commands_processed_per_s | 230381.545 | 234505.5 |
| redisq_total_net_input_bytes_per_s | 87112378.669 | 120474943.1 |
| redisq_total_net_output_bytes_per_s | 60555965.014 | 78260351.3 |
| redisq_used_cpu_sys_cores | 1.006 | 1.43 |
| redisq_used_cpu_sys_main_thread_cores | 0.195 | 0.28 |
| redisq_used_cpu_user_cores | 0.896 | 1.03 |
| redisq_used_cpu_user_main_thread_cores | 0.44 | 0.47 |
| redisq_used_memory | 900.739 | 1376.8 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,648 events applied by workers, 814,648 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 39.167 | 92.671 | 112.127 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 38.143 | 88.575 | 118.271 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 41.215 | 84.479 | 84.479 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 25.343 | 79.871 | 103.423 |
| t-069 | 31 | 1 | 14.175 | 7.061 | 2002 | 0 | 454 | 454 | 0 | 20.863 | 79.359 | 88.063 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 38.655 | 76.799 | 96.255 |
| t-442 | 6 | 1 | 18.007 | 1.885 | 577 | 0 | 120 | 120 | 0 | 24.703 | 76.287 | 83.967 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 23.935 | 74.751 | 85.503 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 299 | 299 | 0 | 27.007 | 74.239 | 81.407 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 198 | 198 | 0 | 24.063 | 71.679 | 76.287 |
| t-114 | 14 | 1 | 48.994 | 11.296 | 1025 | 0 | 694 | 694 | 0 | 21.375 | 71.679 | 122.879 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 21.247 | 71.167 | 94.207 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 498 | 498 | 0 | 20.863 | 70.655 | 218.111 |
| t-266 | 6 | 1 | 113.437 | 11.095 | 714 | 0 | 684 | 684 | 0 | 16.895 | 69.631 | 110.079 |
| t-087 | 6 | 1 | 34.936 | 3.77 | 1614 | 0 | 212 | 212 | 0 | 24.191 | 69.119 | 91.135 |
| t-138 | 9 | 1 | 30.7 | 4.513 | 228 | 0 | 286 | 286 | 0 | 26.239 | 69.119 | 80.895 |
| t-157 | 28 | 1 | 24.111 | 10.755 | 2458 | 0 | 744 | 744 | 0 | 18.047 | 69.119 | 91.135 |
| t-139 | 16 | 1 | 17.601 | 4.973 | 1367 | 0 | 372 | 372 | 0 | 24.575 | 68.095 | 82.943 |
| t-329 | 10 | 1 | 81.883 | 13.227 | 132 | 0 | 807 | 807 | 0 | 17.791 | 68.095 | 76.287 |
| t-419 | 17 | 1 | 20.486 | 6.067 | 3214 | 0 | 426 | 426 | 0 | 20.479 | 68.095 | 90.111 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 380 | 380 | 0 | 24.063 | 67.583 | 75.775 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 8.511 | 67.071 | 133.119 |
| t-028 | 27 | 1 | 20.607 | 9.156 | 454 | 0 | 599 | 599 | 0 | 19.199 | 67.071 | 94.719 |
| t-405 | 6 | 1 | 24.442 | 2.424 | 874 | 0 | 176 | 176 | 0 | 28.159 | 67.071 | 70.143 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 395 | 395 | 0 | 21.503 | 67.071 | 75.775 |
