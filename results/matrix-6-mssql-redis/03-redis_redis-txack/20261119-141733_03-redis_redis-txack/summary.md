# 03-redis_redis-txack — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.697s, drain 0.6s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814656 | 814656 | 12852.014 | 12852.739 | 25816.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642932 | 15.729 | 10.791 | 36.031 | 55.167 | 73.663 | 260.863 |
| e2e_from_send | 642932 | 14.69 | 9.607 | 34.271 | 53.535 | 69.119 | 259.711 |
| producer_lag | 642932 | 1.039 | 0.515 | 2.531 | 7.775 | 16.327 | 27.327 |
| publish_call | 570377 | 0.82 | 0.462 | 1.148 | 8.015 | 17.679 | 415.487 |
| queue_wait | 642932 | 13.03 | 8.207 | 32.415 | 51.935 | 58.527 | 248.703 |
| broker_dwell | 642932 | 12.693 | 7.899 | 32.175 | 51.615 | 56.511 | 237.695 |
| receive_call | 595203 | 1.357 | 0.648 | 2.099 | 10.759 | 71.615 | 410.367 |
| ctx_load | 642932 | 0.607 | 0.402 | 0.868 | 5.211 | 13.719 | 46.207 |
| process | 642932 | 0.001 | 0.001 | 0.002 | 0.004 | 0.017 | 7.031 |
| ctx_commit | 642932 | 0.615 | 0.388 | 0.894 | 5.603 | 14.351 | 48.479 |
| service | 642932 | 1.661 | 0.998 | 2.511 | 13.887 | 32.767 | 148.223 |
| e2e_large | 331 | 45.063 | 44.895 | 68.223 | 91.903 | 98.431 | 98.431 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12852.739 | 13152.6 |
| applied_total | 468755.653 | 777440 |
| bench_cpu_cores | 4.356 | 4.643 |
| bench_cpu_pct | 18.15 | 19.35 |
| bench_gc_gen0 | 4.898 | 11 |
| bench_gc_gen1 | 3.02 | 8 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 811.339 | 1037.6 |
| bench_mem_mb | 1127.78 | 1269.8 |
| bench_threadpool_queue | 6.388 | 165 |
| bench_threadpool_threads | 36.143 | 38 |
| broker_backlog | 213.061 | 409 |
| ctr_eb_kafka_cpu_pct | 1.517 | 1.93 |
| ctr_eb_kafka_mem_mb | 2595.9 | 2595.9 |
| ctr_eb_mssql_cpu_pct | 19.523 | 98.02 |
| ctr_eb_mssql_mem_mb | 7299.408 | 7300.4 |
| ctr_eb_nats_cpu_pct | 0.368 | 0.55 |
| ctr_eb_nats_mem_mb | 345.2 | 345.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.9 | 9.9 |
| ctr_eb_postgres_cpu_pct | 0.357 | 0.45 |
| ctr_eb_postgres_mem_mb | 3198.522 | 3199.6 |
| ctr_eb_rabbitmq_cpu_pct | 8.22 | 9.44 |
| ctr_eb_rabbitmq_mem_mb | 878.986 | 885.7 |
| ctr_eb_redis_cpu_pct | 211.503 | 273.16 |
| ctr_eb_redis_mem_mb | 981.733 | 1437.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 14.45 | 30.16 |
| in_flight | 172.694 | 444 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12852.014 | 13128.3 |
| produced_total | 468928.347 | 777620 |
| publish_mb_per_s | 44.251 | 67.36 |
| redis_aof_buffer_length | 479131.592 | 8703595 |
| redis_aof_current_size | 1345.824 | 2282.7 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.082 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 230211.592 | 233547 |
| redis_total_commands_processed_per_s | 230235.343 | 232955 |
| redis_total_net_input_bytes_per_s | 87559007.918 | 120055066.9 |
| redis_total_net_output_bytes_per_s | 60846382.61 | 80683979 |
| redis_used_cpu_sys_cores | 1.016 | 1.32 |
| redis_used_cpu_sys_main_thread_cores | 0.194 | 0.25 |
| redis_used_cpu_user_cores | 0.923 | 1.12 |
| redis_used_cpu_user_main_thread_cores | 0.451 | 0.52 |
| redis_used_memory | 900.949 | 1339.5 |
| redisq_aof_buffer_length | 455415.224 | 8501477 |
| redisq_aof_current_size | 1352.19 | 2427.1 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.082 | 1 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 230200.857 | 233547 |
| redisq_total_commands_processed_per_s | 230250.382 | 233213.7 |
| redisq_total_net_input_bytes_per_s | 87207868.441 | 119932483.1 |
| redisq_total_net_output_bytes_per_s | 60668590.61 | 77529651.9 |
| redisq_used_cpu_sys_cores | 1.017 | 1.33 |
| redisq_used_cpu_sys_main_thread_cores | 0.195 | 0.25 |
| redisq_used_cpu_user_cores | 0.922 | 1.12 |
| redisq_used_cpu_user_main_thread_cores | 0.451 | 0.52 |
| redisq_used_memory | 902.939 | 1372.4 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,656 events applied by workers, 814,656 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 39.679 | 89.599 | 98.815 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 38.143 | 86.015 | 86.015 |
| t-192 | 6 | 1 | 19.403 | 1.728 | 194 | 0 | 145 | 145 | 0 | 27.263 | 82.943 | 83.455 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 198 | 198 | 0 | 27.775 | 79.359 | 81.407 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 40.191 | 78.335 | 94.719 |
| t-133 | 6 | 1 | 27.688 | 2.68 | 273 | 0 | 182 | 182 | 0 | 23.039 | 76.799 | 81.919 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 21.375 | 71.167 | 129.023 |
| t-068 | 21 | 1 | 19.721 | 6.772 | 3531 | 0 | 459 | 459 | 0 | 23.039 | 70.655 | 73.727 |
| t-025 | 6 | 1 | 48.625 | 5.14 | 954 | 0 | 325 | 325 | 0 | 22.015 | 70.143 | 89.087 |
| t-275 | 9 | 1 | 70.577 | 10.508 | 166 | 0 | 632 | 632 | 0 | 17.279 | 69.631 | 79.871 |
| t-318 | 7 | 1 | 31.766 | 3.874 | 686 | 0 | 252 | 252 | 0 | 26.623 | 69.119 | 81.407 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 40.703 | 68.607 | 74.239 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 411 | 411 | 0 | 23.679 | 68.095 | 113.151 |
| t-422 | 9 | 1 | 33.219 | 4.937 | 2679 | 0 | 312 | 312 | 0 | 24.447 | 67.583 | 72.191 |
| t-298 | 6 | 1 | 64.085 | 7.048 | 3700 | 0 | 415 | 415 | 0 | 20.991 | 67.071 | 75.775 |
| t-155 | 14 | 1 | 14.59 | 3.316 | 2759 | 0 | 208 | 208 | 0 | 22.527 | 66.559 | 68.607 |
| t-451 | 35 | 1 | 10.395 | 5.991 | 815 | 0 | 417 | 417 | 0 | 22.143 | 66.559 | 76.799 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 20.863 | 66.047 | 72.191 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 299 | 299 | 0 | 23.807 | 66.047 | 72.703 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 24.575 | 66.047 | 123.903 |
| t-395 | 6 | 1 | 35.102 | 3.762 | 212 | 0 | 257 | 257 | 0 | 25.727 | 66.047 | 75.775 |
| t-255 | 11 | 1 | 35.499 | 6.413 | 2029 | 0 | 386 | 386 | 0 | 21.503 | 65.535 | 226.303 |
| t-060 | 7 | 1 | 47.726 | 5.892 | 1546 | 0 | 398 | 398 | 0 | 18.303 | 65.279 | 76.799 |
| t-138 | 9 | 1 | 30.7 | 4.513 | 228 | 0 | 286 | 286 | 0 | 24.575 | 65.279 | 69.119 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 27.775 | 65.279 | 69.119 |
