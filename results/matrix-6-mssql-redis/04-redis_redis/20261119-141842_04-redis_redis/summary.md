# 04-redis_redis — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.101s, closing 0.552s, drain 0.801s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814646 | 814646 | 12851.478 | 12853.151 | 27116.2 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642923 | 16.439 | 11.175 | 37.183 | 58.559 | 118.911 | 601.087 |
| e2e_from_send | 642923 | 15.397 | 9.935 | 35.743 | 55.615 | 109.439 | 599.039 |
| producer_lag | 642923 | 1.042 | 0.51 | 2.523 | 8.031 | 16.559 | 43.743 |
| publish_call | 575507 | 0.768 | 0.443 | 1.04 | 7.823 | 16.543 | 360.447 |
| queue_wait | 642923 | 13.14 | 8.191 | 32.543 | 52.159 | 65.535 | 466.175 |
| broker_dwell | 642923 | 12.203 | 7.295 | 31.695 | 51.167 | 59.295 | 464.895 |
| receive_call | 594677 | 1.185 | 0.634 | 1.713 | 10.071 | 28.671 | 343.807 |
| ctx_load | 642923 | 0.577 | 0.375 | 0.819 | 5.119 | 14.919 | 56.223 |
| process | 642923 | 0.002 | 0.001 | 0.002 | 0.004 | 0.022 | 5.931 |
| ctx_commit | 642923 | 0.562 | 0.348 | 0.801 | 5.355 | 15.167 | 64.127 |
| ack | 642923 | 0.485 | 0.259 | 0.653 | 4.735 | 15.679 | 306.431 |
| service | 642923 | 2.257 | 1.26 | 3.321 | 19.775 | 59.071 | 536.063 |
| e2e_large | 331 | 46.621 | 45.055 | 72.959 | 90.303 | 121.791 | 121.791 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.151 | 13121.7 |
| applied_total | 468732.98 | 777471 |
| bench_cpu_cores | 4.897 | 5.268 |
| bench_cpu_pct | 20.406 | 21.95 |
| bench_gc_gen0 | 4.735 | 10 |
| bench_gc_gen1 | 2.653 | 9 |
| bench_gc_gen2 | 0.327 | 2 |
| bench_gc_heap_mb | 763.004 | 964.3 |
| bench_mem_mb | 1025.306 | 1181.5 |
| bench_threadpool_queue | 11.306 | 212 |
| bench_threadpool_threads | 30.163 | 34 |
| broker_backlog | 224.714 | 337 |
| ctr_eb_kafka_cpu_pct | 1.417 | 1.89 |
| ctr_eb_kafka_mem_mb | 2595.9 | 2595.9 |
| ctr_eb_mssql_cpu_pct | 1.027 | 1.24 |
| ctr_eb_mssql_mem_mb | 7300.257 | 7300.4 |
| ctr_eb_nats_cpu_pct | 0.619 | 2.92 |
| ctr_eb_nats_mem_mb | 346.353 | 346.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 9.9 | 9.9 |
| ctr_eb_postgres_cpu_pct | 0.392 | 0.53 |
| ctr_eb_postgres_mem_mb | 3200.612 | 3201.5 |
| ctr_eb_rabbitmq_cpu_pct | 8.194 | 9.3 |
| ctr_eb_rabbitmq_mem_mb | 878.631 | 884 |
| ctr_eb_redis_cpu_pct | 225.434 | 306.66 |
| ctr_eb_redis_mem_mb | 977.833 | 1517.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 17.364 | 26.68 |
| in_flight | 167 | 311 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12851.478 | 13126.9 |
| produced_total | 468899.98 | 777626 |
| publish_mb_per_s | 44.25 | 67.23 |
| redis_aof_buffer_length | 15948.184 | 94189 |
| redis_aof_current_size | 1644.614 | 3133.9 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 229830.571 | 232610 |
| redis_total_commands_processed_per_s | 229951.663 | 233137 |
| redis_total_net_input_bytes_per_s | 88355327.58 | 121217002.5 |
| redis_total_net_output_bytes_per_s | 60705254.812 | 77345741 |
| redis_used_cpu_sys_cores | 1.058 | 1.31 |
| redis_used_cpu_sys_main_thread_cores | 0.203 | 0.27 |
| redis_used_cpu_user_cores | 0.974 | 1.12 |
| redis_used_cpu_user_main_thread_cores | 0.474 | 0.53 |
| redis_used_memory | 900.884 | 1383.3 |
| redisq_aof_buffer_length | 13326.082 | 77122 |
| redisq_aof_current_size | 1644.473 | 3133.9 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 229830.571 | 232610 |
| redisq_total_commands_processed_per_s | 229968.878 | 233207.1 |
| redisq_total_net_input_bytes_per_s | 88361041.014 | 121330817.9 |
| redisq_total_net_output_bytes_per_s | 60654617.949 | 77435058.4 |
| redisq_used_cpu_sys_cores | 1.058 | 1.31 |
| redisq_used_cpu_sys_main_thread_cores | 0.203 | 0.27 |
| redisq_used_cpu_user_cores | 0.972 | 1.12 |
| redisq_used_cpu_user_main_thread_cores | 0.474 | 0.53 |
| redisq_used_memory | 901.112 | 1383.1 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,646 events applied by workers, 814,646 generated |
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
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 24.959 | 95.743 | 179.199 |
| t-155 | 14 | 1 | 14.59 | 3.316 | 2759 | 0 | 208 | 208 | 0 | 23.167 | 89.599 | 112.127 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 44.031 | 89.599 | 94.719 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 39.935 | 89.087 | 89.087 |
| t-149 | 13 | 1 | 34.576 | 7.055 | 3034 | 0 | 456 | 456 | 0 | 22.271 | 89.087 | 518.143 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 41.983 | 88.575 | 121.855 |
| t-097 | 12 | 1 | 56.648 | 11.402 | 1865 | 0 | 703 | 703 | 0 | 19.711 | 82.943 | 159.743 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 347 | 347 | 0 | 26.623 | 79.871 | 161.791 |
| t-459 | 25 | 1 | 16.76 | 6.839 | 2174 | 0 | 467 | 467 | 0 | 22.911 | 77.823 | 118.783 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 29.823 | 76.799 | 86.015 |
| t-039 | 28 | 1 | 18.305 | 8.565 | 1459 | 0 | 607 | 607 | 0 | 20.991 | 76.287 | 126.463 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 36.607 | 75.775 | 84.479 |
| t-025 | 6 | 1 | 48.625 | 5.14 | 954 | 0 | 325 | 325 | 0 | 24.703 | 73.727 | 114.175 |
| t-449 | 14 | 1 | 42.587 | 10.077 | 290 | 0 | 672 | 672 | 0 | 18.687 | 73.727 | 163.839 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 19.327 | 73.215 | 91.135 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 18.943 | 73.215 | 83.967 |
| t-408 | 25 | 1 | 31.949 | 12.944 | 1601 | 0 | 805 | 805 | 0 | 17.535 | 72.703 | 171.007 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 24.319 | 71.679 | 79.871 |
| t-457 | 18 | 1 | 23.751 | 7.425 | 1557 | 0 | 484 | 484 | 0 | 21.375 | 71.679 | 111.103 |
| t-412 | 6 | 1 | 28.435 | 2.632 | 468 | 0 | 164 | 164 | 0 | 27.007 | 71.167 | 74.751 |
| t-160 | 57 | 2 | 57.318 | 54.397 | 548 | 0 | 3365 | 3365 | 0 | 11.711 | 70.655 | 193.535 |
| t-390 | 10 | 1 | 36.089 | 5.892 | 167 | 0 | 353 | 353 | 0 | 27.007 | 70.655 | 74.751 |
| t-419 | 17 | 1 | 20.486 | 6.067 | 3214 | 0 | 426 | 426 | 0 | 24.063 | 70.655 | 92.159 |
| t-230 | 30 | 2 | 104.35 | 51.662 | 1033 | 0 | 3112 | 3112 | 0 | 12.287 | 70.143 | 154.623 |
| t-241 | 27 | 1 | 11.444 | 5.213 | 2685 | 0 | 386 | 386 | 0 | 24.831 | 70.143 | 80.383 |
