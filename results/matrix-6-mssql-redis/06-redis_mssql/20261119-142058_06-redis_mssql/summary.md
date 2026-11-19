# 06-redis_mssql — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.1s, closing 0.266s, drain 1.402s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814631 | 814631 | 12850.912 | 12851.661 | 21512.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642908 | 16.375 | 11.471 | 35.967 | 54.847 | 65.247 | 745.471 |
| e2e_from_send | 642908 | 15.466 | 10.495 | 34.879 | 54.015 | 62.783 | 745.471 |
| producer_lag | 642908 | 0.91 | 0.508 | 2.391 | 4.519 | 10.927 | 31.167 |
| publish_call | 577457 | 0.898 | 0.608 | 1.69 | 5.603 | 12.687 | 159.743 |
| queue_wait | 642908 | 12.578 | 7.587 | 32.079 | 51.391 | 55.391 | 225.407 |
| broker_dwell | 642908 | 11.624 | 6.531 | 31.087 | 50.463 | 53.663 | 224.383 |
| receive_call | 543516 | 1.156 | 0.787 | 1.929 | 6.623 | 23.951 | 157.951 |
| ctx_load | 642908 | 0.773 | 0.576 | 1.412 | 3.181 | 6.299 | 297.215 |
| process | 642908 | 0.002 | 0.001 | 0.002 | 0.004 | 0.037 | 8.207 |
| ctx_commit | 642908 | 0.765 | 0.588 | 1.316 | 3.133 | 6.939 | 384.767 |
| ack | 642908 | 0.64 | 0.378 | 1.175 | 4.823 | 11.103 | 152.575 |
| service | 642908 | 2.888 | 2.151 | 5.287 | 12.151 | 23.839 | 741.375 |
| e2e_large | 331 | 57.877 | 57.823 | 82.751 | 115.647 | 217.727 | 217.727 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12851.661 | 13130.9 |
| applied_total | 468776.531 | 777454 |
| bench_cpu_cores | 5.165 | 5.337 |
| bench_cpu_pct | 21.522 | 22.24 |
| bench_gc_gen0 | 11.857 | 16 |
| bench_gc_gen1 | 4.918 | 9 |
| bench_gc_gen2 | 0.286 | 2 |
| bench_gc_heap_mb | 822.71 | 1132.1 |
| bench_mem_mb | 1135.99 | 1319.7 |
| bench_threadpool_queue | 10.571 | 109 |
| bench_threadpool_threads | 36.735 | 44 |
| broker_backlog | 205.163 | 314 |
| ctr_eb_kafka_cpu_pct | 1.702 | 2.08 |
| ctr_eb_kafka_mem_mb | 2514.48 | 2530.1 |
| ctr_eb_mssql_cpu_pct | 526.822 | 683.09 |
| ctr_eb_mssql_mem_mb | 6821.4 | 6823.5 |
| ctr_eb_nats_cpu_pct | 0.679 | 3.2 |
| ctr_eb_nats_mem_mb | 346.8 | 346.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.9 | 9.9 |
| ctr_eb_postgres_cpu_pct | 0.224 | 0.38 |
| ctr_eb_postgres_mem_mb | 3225.52 | 3228.2 |
| ctr_eb_rabbitmq_cpu_pct | 12.511 | 13.29 |
| ctr_eb_rabbitmq_mem_mb | 877.429 | 883.8 |
| ctr_eb_redis_cpu_pct | 159.411 | 234.99 |
| ctr_eb_redis_mem_mb | 1420.059 | 1465.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 48.743 | 54.86 |
| in_flight | 170.245 | 295 |
| mssql_batch_requests_per_s | 25830.722 | 31797.6 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 3.894 | 6.9 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 48050436.816 | 73494632.6 |
| mssql_log_flush_waits_per_s | 0.367 | 2 |
| mssql_log_flushes_per_s | 971.116 | 1370.3 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 12915.563 | 15908.6 |
| mssql_user_connections | 150.694 | 151 |
| mssql_wait_async_network_io_ms_per_s | 0.112 | 1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 2.196 | 11.8 |
| mssql_wait_pagelatch_ex_ms_per_s | 6.224 | 12.3 |
| mssql_wait_pagelatch_sh_ms_per_s | 7.861 | 30 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4.296 | 16.7 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.204 | 1.5 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12850.912 | 13120 |
| produced_total | 468946.776 | 777628 |
| publish_mb_per_s | 44.356 | 67.23 |
| redisq_aof_buffer_length | 10590.429 | 38388 |
| redisq_aof_current_size | 2403.169 | 3220 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 16 | 16 |
| redisq_instantaneous_ops_per_sec | 120667.204 | 122362 |
| redisq_total_commands_processed_per_s | 120930.047 | 122725.8 |
| redisq_total_net_input_bytes_per_s | 55087885.245 | 75543751 |
| redisq_total_net_output_bytes_per_s | 48720883.924 | 75269505.9 |
| redisq_used_cpu_sys_cores | 0.801 | 0.94 |
| redisq_used_cpu_sys_main_thread_cores | 0.169 | 0.22 |
| redisq_used_cpu_user_cores | 0.691 | 0.83 |
| redisq_used_cpu_user_main_thread_cores | 0.329 | 0.37 |
| redisq_used_memory | 1361.198 | 1382.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,631 events applied by workers, 814,631 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 50.687 | 115.711 | 218.111 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 46.079 | 114.687 | 114.687 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 41.471 | 95.743 | 102.399 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 50.687 | 80.383 | 93.695 |
| t-225 | 6 | 1 | 31.888 | 3.143 | 3030 | 0 | 186 | 186 | 0 | 26.239 | 66.559 | 92.671 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 23.935 | 66.047 | 136.191 |
| t-139 | 16 | 1 | 17.601 | 4.973 | 1367 | 0 | 372 | 372 | 0 | 24.831 | 65.535 | 105.471 |
| t-408 | 25 | 1 | 31.949 | 12.944 | 1601 | 0 | 805 | 805 | 0 | 17.279 | 63.999 | 274.431 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 411 | 411 | 0 | 23.679 | 62.719 | 119.295 |
| t-392 | 33 | 1 | 23.031 | 12.876 | 2495 | 0 | 867 | 867 | 0 | 17.535 | 62.207 | 255.999 |
| t-173 | 12 | 1 | 42.144 | 8.877 | 3493 | 0 | 533 | 533 | 0 | 20.607 | 61.183 | 409.599 |
| t-462 | 10 | 1 | 25.654 | 4.163 | 2006 | 0 | 264 | 264 | 0 | 22.271 | 61.183 | 67.583 |
| t-335 | 6 | 1 | 28.977 | 2.726 | 450 | 0 | 179 | 179 | 0 | 28.799 | 60.927 | 62.463 |
| t-028 | 27 | 1 | 20.607 | 9.156 | 454 | 0 | 599 | 599 | 0 | 19.455 | 60.671 | 67.583 |
| t-030 | 19 | 1 | 24.985 | 8.002 | 748 | 0 | 511 | 511 | 0 | 23.935 | 60.671 | 176.127 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 22.527 | 60.671 | 85.503 |
| t-115 | 10 | 1 | 33.32 | 5.329 | 3658 | 0 | 383 | 383 | 0 | 21.631 | 60.671 | 71.679 |
| t-130 | 13 | 1 | 31.643 | 7.058 | 136 | 0 | 451 | 451 | 0 | 24.575 | 60.671 | 115.199 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 26.879 | 60.671 | 87.039 |
| t-185 | 12 | 1 | 30.823 | 6.062 | 438 | 0 | 415 | 415 | 0 | 20.863 | 60.671 | 64.255 |
| t-297 | 14 | 1 | 38.488 | 8.638 | 1702 | 0 | 543 | 543 | 0 | 19.839 | 60.671 | 73.215 |
| t-451 | 35 | 1 | 10.395 | 5.991 | 815 | 0 | 417 | 417 | 0 | 21.247 | 60.671 | 97.791 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 380 | 380 | 0 | 23.295 | 60.159 | 79.871 |
| t-233 | 56 | 1 | 13.701 | 12.685 | 208 | 0 | 834 | 834 | 0 | 17.407 | 59.903 | 67.583 |
| t-162 | 69 | 1 | 10.553 | 12.151 | 353 | 0 | 829 | 829 | 0 | 18.431 | 59.647 | 107.007 |
