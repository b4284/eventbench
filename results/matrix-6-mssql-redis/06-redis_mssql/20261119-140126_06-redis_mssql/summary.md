# 06-redis_mssql — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.101s, closing 0.281s, drain 1.404s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814651 | 814651 | 12852.588 | 12852.867 | 20411.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642928 | 16.23 | 11.295 | 35.775 | 54.591 | 64.863 | 602.623 |
| e2e_from_send | 642928 | 15.333 | 10.351 | 34.687 | 53.823 | 62.495 | 602.623 |
| producer_lag | 642928 | 0.897 | 0.496 | 2.359 | 4.303 | 11.719 | 29.967 |
| publish_call | 581166 | 0.842 | 0.586 | 1.515 | 5.027 | 13.071 | 168.447 |
| queue_wait | 642928 | 12.552 | 7.575 | 32.063 | 51.295 | 55.039 | 520.703 |
| broker_dwell | 642928 | 11.91 | 6.847 | 31.391 | 50.655 | 53.791 | 519.423 |
| receive_call | 548137 | 1.102 | 0.763 | 1.786 | 6.527 | 24.831 | 154.495 |
| ctx_load | 642928 | 0.754 | 0.567 | 1.36 | 3.105 | 6.027 | 365.311 |
| process | 642928 | 0.002 | 0.001 | 0.002 | 0.004 | 0.036 | 5.383 |
| ctx_commit | 642928 | 0.749 | 0.58 | 1.271 | 3.027 | 6.859 | 230.783 |
| ack | 642928 | 0.596 | 0.357 | 1.064 | 4.327 | 11.639 | 141.439 |
| service | 642928 | 2.782 | 2.079 | 5.039 | 11.759 | 23.983 | 523.007 |
| e2e_large | 331 | 56.898 | 57.599 | 82.495 | 110.079 | 157.567 | 157.567 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12852.867 | 13107.7 |
| applied_total | 468707.143 | 777434 |
| bench_cpu_cores | 5.241 | 5.497 |
| bench_cpu_pct | 21.837 | 22.9 |
| bench_gc_gen0 | 11.796 | 17 |
| bench_gc_gen1 | 4.755 | 8 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 821.81 | 1072.3 |
| bench_mem_mb | 1171.169 | 1306.4 |
| bench_threadpool_queue | 19.51 | 107 |
| bench_threadpool_threads | 36 | 44 |
| broker_backlog | 192.306 | 247 |
| ctr_eb_kafka_cpu_pct | 1.79 | 2.18 |
| ctr_eb_kafka_mem_mb | 1917.127 | 1919.7 |
| ctr_eb_mssql_cpu_pct | 530.115 | 613.29 |
| ctr_eb_mssql_mem_mb | 7400.692 | 7407.2 |
| ctr_eb_nats_cpu_pct | 0.497 | 0.6 |
| ctr_eb_nats_mem_mb | 360.524 | 360.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.7 | 10.7 |
| ctr_eb_postgres_cpu_pct | 0.273 | 0.39 |
| ctr_eb_postgres_mem_mb | 3223.871 | 3228.2 |
| ctr_eb_rabbitmq_cpu_pct | 12.264 | 13.43 |
| ctr_eb_rabbitmq_mem_mb | 613.271 | 620.2 |
| ctr_eb_redis_cpu_pct | 164.875 | 243.78 |
| ctr_eb_redis_mem_mb | 1430.518 | 1517.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 49.66 | 57.74 |
| in_flight | 166.837 | 235 |
| mssql_batch_requests_per_s | 25746.704 | 27564.5 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 4.237 | 8.8 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 47865632.578 | 69496944.9 |
| mssql_log_flush_waits_per_s | 0.347 | 2.5 |
| mssql_log_flushes_per_s | 972.802 | 1320.9 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 1.496 | 3 |
| mssql_transactions_per_s | 12877.086 | 13790.4 |
| mssql_user_connections | 132.245 | 134 |
| mssql_wait_async_network_io_ms_per_s | 0.316 | 1.5 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.082 | 2 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 10.994 | 169.3 |
| mssql_wait_pagelatch_ex_ms_per_s | 10.89 | 113.7 |
| mssql_wait_pagelatch_sh_ms_per_s | 8.733 | 34.9 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4.635 | 15.2 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.265 | 2 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12852.588 | 13123.7 |
| produced_total | 468873.98 | 777643 |
| publish_mb_per_s | 44.253 | 67.21 |
| redisq_aof_buffer_length | 9989.592 | 53877 |
| redisq_aof_current_size | 2566.753 | 3337 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 16 | 16 |
| redisq_instantaneous_ops_per_sec | 120941.531 | 122633 |
| redisq_total_commands_processed_per_s | 121337.145 | 123140.7 |
| redisq_total_net_input_bytes_per_s | 55079824.916 | 77196058.3 |
| redisq_total_net_output_bytes_per_s | 48506685.255 | 72163163.7 |
| redisq_used_cpu_sys_cores | 0.803 | 0.93 |
| redisq_used_cpu_sys_main_thread_cores | 0.178 | 0.22 |
| redisq_used_cpu_user_cores | 0.682 | 0.78 |
| redisq_used_cpu_user_main_thread_cores | 0.323 | 0.39 |
| redisq_used_memory | 1361.092 | 1380.4 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,651 events applied by workers, 814,651 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 48.639 | 107.007 | 157.695 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 47.615 | 95.231 | 95.231 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 44.031 | 87.551 | 110.079 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 54.527 | 82.943 | 88.063 |
| t-217 | 8 | 1 | 17.717 | 2.196 | 1936 | 0 | 153 | 153 | 0 | 26.879 | 78.335 | 102.911 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 198 | 198 | 0 | 28.287 | 69.119 | 69.631 |
| t-144 | 6 | 1 | 24.576 | 2.554 | 816 | 0 | 174 | 174 | 0 | 23.679 | 64.767 | 66.047 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 27.775 | 63.999 | 88.063 |
| t-076 | 23 | 1 | 17.778 | 6.719 | 4045 | 0 | 461 | 461 | 0 | 20.607 | 63.743 | 73.727 |
| t-422 | 9 | 1 | 33.219 | 4.937 | 2679 | 0 | 312 | 312 | 0 | 23.167 | 62.719 | 385.023 |
| t-370 | 12 | 1 | 20.006 | 4.067 | 568 | 0 | 254 | 254 | 0 | 24.575 | 62.463 | 66.047 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 30.207 | 61.951 | 76.287 |
| t-445 | 15 | 1 | 19.282 | 4.639 | 896 | 0 | 340 | 340 | 0 | 23.423 | 61.951 | 247.807 |
| t-069 | 31 | 1 | 14.175 | 7.061 | 2002 | 0 | 454 | 454 | 0 | 23.551 | 61.695 | 63.231 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 25.727 | 61.695 | 62.719 |
| t-204 | 37 | 1 | 18.243 | 11.261 | 1822 | 0 | 801 | 801 | 0 | 20.095 | 61.695 | 80.383 |
| t-318 | 7 | 1 | 31.766 | 3.874 | 686 | 0 | 252 | 252 | 0 | 25.343 | 61.439 | 74.751 |
| t-284 | 12 | 1 | 27.902 | 5.305 | 217 | 0 | 362 | 362 | 0 | 23.167 | 60.927 | 75.263 |
| t-162 | 69 | 1 | 10.553 | 12.151 | 353 | 0 | 829 | 829 | 0 | 18.559 | 60.671 | 104.959 |
| t-248 | 6 | 1 | 33.413 | 3.259 | 1190 | 0 | 223 | 223 | 0 | 24.319 | 60.671 | 61.951 |
| t-335 | 6 | 1 | 28.977 | 2.726 | 450 | 0 | 179 | 179 | 0 | 27.519 | 60.671 | 66.559 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 498 | 498 | 0 | 20.095 | 60.415 | 252.927 |
| t-177 | 19 | 1 | 38.393 | 12.238 | 615 | 0 | 793 | 793 | 0 | 17.791 | 60.159 | 75.263 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 23.423 | 59.903 | 73.215 |
| t-151 | 6 | 1 | 23.83 | 2.494 | 577 | 0 | 156 | 156 | 0 | 23.935 | 59.647 | 71.167 |
