# 04-redis_redis — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.466s, drain 0.802s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814640 | 814640 | 12852.555 | 12853.045 | 29326.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642916 | 16.6 | 11.295 | 37.727 | 61.439 | 100.351 | 539.647 |
| e2e_from_send | 642916 | 15.495 | 9.975 | 36.287 | 57.503 | 92.671 | 533.503 |
| producer_lag | 642916 | 1.105 | 0.528 | 2.581 | 9.087 | 21.039 | 39.487 |
| publish_call | 571630 | 0.79 | 0.447 | 1.075 | 8.383 | 17.327 | 274.175 |
| queue_wait | 642916 | 13.192 | 8.231 | 32.607 | 52.223 | 63.615 | 460.799 |
| broker_dwell | 642916 | 12.299 | 7.383 | 31.791 | 51.295 | 59.167 | 459.263 |
| receive_call | 594970 | 1.182 | 0.636 | 1.811 | 10.767 | 32.863 | 257.663 |
| ctx_load | 642916 | 0.593 | 0.37 | 0.841 | 5.411 | 15.831 | 39.039 |
| process | 642916 | 0.001 | 0.001 | 0.002 | 0.004 | 0.019 | 5.187 |
| ctx_commit | 642916 | 0.57 | 0.344 | 0.815 | 5.311 | 15.407 | 41.919 |
| ack | 642916 | 0.5 | 0.257 | 0.681 | 4.683 | 16.975 | 267.007 |
| service | 642916 | 2.303 | 1.242 | 3.493 | 21.727 | 56.351 | 467.455 |
| e2e_large | 331 | 49.175 | 48.479 | 73.343 | 96.639 | 121.471 | 121.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.045 | 13153.9 |
| applied_total | 468720.388 | 777448 |
| bench_cpu_cores | 4.632 | 4.986 |
| bench_cpu_pct | 19.301 | 20.78 |
| bench_gc_gen0 | 4.082 | 10 |
| bench_gc_gen1 | 2.367 | 8 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 825.141 | 1032.5 |
| bench_mem_mb | 1118.257 | 1209.8 |
| bench_threadpool_queue | 14.918 | 191 |
| bench_threadpool_threads | 33.612 | 36 |
| broker_backlog | 224 | 388 |
| ctr_eb_kafka_cpu_pct | 1.5 | 2 |
| ctr_eb_kafka_mem_mb | 1937.9 | 1937.9 |
| ctr_eb_mssql_cpu_pct | 2.281 | 3.38 |
| ctr_eb_mssql_mem_mb | 7139.096 | 7140.2 |
| ctr_eb_nats_cpu_pct | 0.466 | 0.58 |
| ctr_eb_nats_mem_mb | 360.6 | 360.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_mem_mb | 10.3 | 10.3 |
| ctr_eb_postgres_cpu_pct | 0.183 | 0.32 |
| ctr_eb_postgres_mem_mb | 2193.5 | 2193.5 |
| ctr_eb_rabbitmq_cpu_pct | 7.889 | 8.82 |
| ctr_eb_rabbitmq_mem_mb | 630.439 | 635.1 |
| ctr_eb_redis_cpu_pct | 222.963 | 284.59 |
| ctr_eb_redis_mem_mb | 968.606 | 1371.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 16.295 | 27.34 |
| in_flight | 161.673 | 215 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12852.555 | 13129.9 |
| produced_total | 468882.061 | 777621 |
| publish_mb_per_s | 44.331 | 71.16 |
| redis_aof_buffer_length | 15551.388 | 62010 |
| redis_aof_current_size | 1395.173 | 2487.8 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 229699.245 | 232569 |
| redis_total_commands_processed_per_s | 229273.439 | 232010 |
| redis_total_net_input_bytes_per_s | 88693165.165 | 121141233.6 |
| redis_total_net_output_bytes_per_s | 60913554.39 | 77393081.7 |
| redis_used_cpu_sys_cores | 1.086 | 1.35 |
| redis_used_cpu_sys_main_thread_cores | 0.216 | 0.29 |
| redis_used_cpu_user_cores | 0.94 | 1.13 |
| redis_used_cpu_user_main_thread_cores | 0.448 | 0.52 |
| redis_used_memory | 899.065 | 1340.6 |
| redisq_aof_buffer_length | 9491.633 | 60915 |
| redisq_aof_current_size | 1394.81 | 2487.5 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 229891.184 | 232569 |
| redisq_total_commands_processed_per_s | 229359.516 | 231918 |
| redisq_total_net_input_bytes_per_s | 88616591.584 | 121067612.5 |
| redisq_total_net_output_bytes_per_s | 60942590.069 | 77353745.5 |
| redisq_used_cpu_sys_cores | 1.086 | 1.36 |
| redisq_used_cpu_sys_main_thread_cores | 0.215 | 0.29 |
| redisq_used_cpu_user_cores | 0.94 | 1.14 |
| redisq_used_cpu_user_main_thread_cores | 0.447 | 0.52 |
| redisq_used_memory | 899.204 | 1337 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,640 events applied by workers, 814,640 generated |
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
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 42.751 | 121.855 | 121.855 |
| t-432 | 9 | 1 | 58.559 | 8.62 | 747 | 0 | 529 | 529 | 0 | 20.095 | 93.183 | 109.567 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 335 | 335 | 0 | 25.599 | 90.111 | 101.375 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 43.519 | 89.599 | 102.399 |
| t-472 | 32 | 1 | 25.243 | 12.984 | 1124 | 0 | 841 | 841 | 0 | 19.199 | 88.575 | 112.639 |
| t-358 | 22 | 1 | 26.953 | 9.697 | 310 | 0 | 634 | 634 | 0 | 19.839 | 85.503 | 120.319 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 29.695 | 84.479 | 87.039 |
| t-422 | 9 | 1 | 33.219 | 4.937 | 2679 | 0 | 312 | 312 | 0 | 23.551 | 83.967 | 495.615 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 22.271 | 83.455 | 177.151 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 411 | 411 | 0 | 24.575 | 82.431 | 168.959 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 26.111 | 81.919 | 103.935 |
| t-240 | 8 | 1 | 65.145 | 9.617 | 361 | 0 | 611 | 611 | 0 | 19.839 | 81.919 | 167.935 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 299 | 299 | 0 | 22.271 | 81.407 | 90.111 |
| t-173 | 12 | 1 | 42.144 | 8.877 | 3493 | 0 | 533 | 533 | 0 | 22.143 | 81.407 | 436.223 |
| t-204 | 37 | 1 | 18.243 | 11.261 | 1822 | 0 | 801 | 801 | 0 | 18.943 | 80.895 | 118.783 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 48.383 | 79.871 | 103.935 |
| t-359 | 52 | 1 | 25.301 | 21.73 | 132 | 0 | 1363 | 1363 | 0 | 15.039 | 79.871 | 346.111 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 347 | 347 | 0 | 25.087 | 78.847 | 97.279 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 41.471 | 78.335 | 96.767 |
| t-408 | 25 | 1 | 31.949 | 12.944 | 1601 | 0 | 805 | 805 | 0 | 17.535 | 78.335 | 199.679 |
| t-164 | 7 | 1 | 102.067 | 10.973 | 236 | 0 | 730 | 730 | 0 | 19.839 | 77.823 | 207.871 |
| t-244 | 11 | 1 | 32 | 5.972 | 1151 | 0 | 379 | 379 | 0 | 22.143 | 77.823 | 87.551 |
| t-357 | 21 | 1 | 53.774 | 18.657 | 2215 | 0 | 1197 | 1197 | 0 | 14.079 | 77.311 | 112.127 |
| t-087 | 6 | 1 | 34.936 | 3.77 | 1614 | 0 | 212 | 212 | 0 | 27.391 | 76.287 | 93.695 |
| t-229 | 28 | 1 | 23.904 | 11.164 | 1675 | 0 | 754 | 754 | 0 | 18.303 | 75.775 | 103.935 |
