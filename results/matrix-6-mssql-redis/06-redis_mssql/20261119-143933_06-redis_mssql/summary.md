# 06-redis_mssql — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.102s, closing 0.279s, drain 1.4s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814638 | 814638 | 12851.937 | 12852.398 | 20626.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642914 | 16.516 | 11.615 | 36.127 | 55.103 | 70.783 | 409.343 |
| e2e_from_send | 642914 | 15.592 | 10.591 | 35.007 | 54.175 | 67.199 | 408.063 |
| producer_lag | 642914 | 0.924 | 0.518 | 2.411 | 4.643 | 12.007 | 29.839 |
| publish_call | 575750 | 0.909 | 0.614 | 1.71 | 5.599 | 14.015 | 173.567 |
| queue_wait | 642914 | 12.584 | 7.591 | 32.095 | 51.487 | 56.031 | 398.847 |
| broker_dwell | 642914 | 11.995 | 6.911 | 31.487 | 50.879 | 54.399 | 398.335 |
| receive_call | 544018 | 1.188 | 0.795 | 1.985 | 7.167 | 25.823 | 173.055 |
| ctx_load | 642914 | 0.809 | 0.599 | 1.467 | 3.441 | 7.167 | 228.991 |
| process | 642914 | 0.002 | 0.001 | 0.002 | 0.004 | 0.055 | 7.467 |
| ctx_commit | 642914 | 0.799 | 0.611 | 1.378 | 3.387 | 7.395 | 73.919 |
| ack | 642914 | 0.66 | 0.384 | 1.201 | 4.915 | 11.327 | 159.487 |
| service | 642914 | 3.008 | 2.205 | 5.499 | 12.919 | 27.471 | 398.335 |
| e2e_large | 331 | 60.788 | 59.231 | 88.255 | 129.791 | 185.343 | 185.343 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12852.398 | 13129 |
| applied_total | 468733.204 | 777459 |
| bench_cpu_cores | 5.144 | 5.363 |
| bench_cpu_pct | 21.435 | 22.34 |
| bench_gc_gen0 | 13.49 | 20 |
| bench_gc_gen1 | 9.041 | 14 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 827.359 | 1172.2 |
| bench_mem_mb | 1168.106 | 1374.6 |
| bench_threadpool_queue | 23.714 | 198 |
| bench_threadpool_threads | 37.327 | 44 |
| broker_backlog | 207.918 | 253 |
| ctr_eb_kafka_cpu_pct | 1.743 | 2.09 |
| ctr_eb_kafka_mem_mb | 3196.002 | 3231 |
| ctr_eb_mssql_cpu_pct | 538.164 | 689.45 |
| ctr_eb_mssql_mem_mb | 5862.218 | 5863.2 |
| ctr_eb_nats_cpu_pct | 0.297 | 0.34 |
| ctr_eb_nats_mem_mb | 353.455 | 353.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 0.629 | 6.29 |
| ctr_eb_postgres_mem_mb | 3166.245 | 3173.8 |
| ctr_eb_rabbitmq_cpu_pct | 12.236 | 13.44 |
| ctr_eb_rabbitmq_mem_mb | 714.531 | 720 |
| ctr_eb_redis_cpu_pct | 167.979 | 239.01 |
| ctr_eb_redis_mem_mb | 1430.741 | 1503.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 50.236 | 55.47 |
| in_flight | 170.224 | 233 |
| mssql_batch_requests_per_s | 25842.853 | 32449.8 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 5.188 | 16.2 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 48050164.069 | 74147275.6 |
| mssql_log_flush_waits_per_s | 0.367 | 2.5 |
| mssql_log_flushes_per_s | 969.971 | 1369.6 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 12921.978 | 16234.3 |
| mssql_user_connections | 150 | 151 |
| mssql_wait_async_network_io_ms_per_s | 0.204 | 1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 4.386 | 21 |
| mssql_wait_pagelatch_ex_ms_per_s | 6.041 | 13.8 |
| mssql_wait_pagelatch_sh_ms_per_s | 7.696 | 28.5 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 5.259 | 19.7 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.163 | 1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12851.937 | 13138 |
| produced_total | 468903.429 | 777633 |
| publish_mb_per_s | 44.251 | 67.24 |
| redisq_aof_buffer_length | 8859.918 | 56202 |
| redisq_aof_current_size | 2598.865 | 3373.4 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 16 | 16 |
| redisq_instantaneous_ops_per_sec | 120658.857 | 122169 |
| redisq_total_commands_processed_per_s | 120695.865 | 122210.1 |
| redisq_total_net_input_bytes_per_s | 55053764.249 | 75469881.6 |
| redisq_total_net_output_bytes_per_s | 48676953.927 | 77973021 |
| redisq_used_cpu_sys_cores | 0.81 | 0.94 |
| redisq_used_cpu_sys_main_thread_cores | 0.172 | 0.22 |
| redisq_used_cpu_user_cores | 0.68 | 0.79 |
| redisq_used_cpu_user_main_thread_cores | 0.324 | 0.37 |
| redisq_used_memory | 1360.457 | 1379.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,638 events applied by workers, 814,638 generated |
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
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 49.407 | 130.047 | 133.119 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 51.967 | 122.367 | 185.343 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 45.823 | 117.759 | 117.759 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 48.895 | 102.911 | 106.495 |
| t-118 | 15 | 1 | 24.104 | 6.098 | 138 | 0 | 403 | 403 | 0 | 22.015 | 72.191 | 81.407 |
| t-069 | 31 | 1 | 14.175 | 7.061 | 2002 | 0 | 454 | 454 | 0 | 23.551 | 68.607 | 87.039 |
| t-217 | 8 | 1 | 17.717 | 2.196 | 1936 | 0 | 153 | 153 | 0 | 26.879 | 68.095 | 129.535 |
| t-262 | 63 | 1 | 13.176 | 13.326 | 130 | 0 | 948 | 948 | 0 | 16.511 | 66.559 | 116.735 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 22.783 | 65.279 | 87.039 |
| t-135 | 6 | 1 | 15.198 | 1.55 | 791 | 0 | 85 | 85 | 0 | 23.807 | 64.511 | 325.631 |
| t-155 | 14 | 1 | 14.59 | 3.316 | 2759 | 0 | 208 | 208 | 0 | 25.983 | 63.999 | 91.647 |
| t-307 | 16 | 1 | 26.404 | 7.124 | 200 | 0 | 466 | 466 | 0 | 23.423 | 63.487 | 70.655 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 25.983 | 63.231 | 63.999 |
| t-310 | 6 | 1 | 70.482 | 7.386 | 1910 | 0 | 474 | 474 | 0 | 20.863 | 62.975 | 90.111 |
| t-448 | 6 | 1 | 47.757 | 5.012 | 2313 | 0 | 298 | 298 | 0 | 21.631 | 62.975 | 63.487 |
| t-021 | 19 | 1 | 16.257 | 5.407 | 3252 | 0 | 346 | 346 | 0 | 25.471 | 62.207 | 192.511 |
| t-339 | 17 | 1 | 12.445 | 3.571 | 134 | 0 | 241 | 241 | 0 | 22.015 | 62.207 | 66.559 |
| t-412 | 6 | 1 | 28.435 | 2.632 | 468 | 0 | 164 | 164 | 0 | 31.999 | 62.207 | 69.119 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 28.031 | 62.207 | 66.047 |
| t-146 | 6 | 1 | 43.291 | 4.645 | 246 | 0 | 274 | 274 | 0 | 24.703 | 61.439 | 63.231 |
| t-177 | 19 | 1 | 38.393 | 12.238 | 615 | 0 | 793 | 793 | 0 | 17.791 | 61.183 | 80.383 |
| t-392 | 33 | 1 | 23.031 | 12.876 | 2495 | 0 | 867 | 867 | 0 | 18.303 | 61.183 | 342.015 |
| t-445 | 15 | 1 | 19.282 | 4.639 | 896 | 0 | 340 | 340 | 0 | 26.751 | 61.183 | 75.263 |
| t-006 | 14 | 1 | 25.112 | 5.603 | 1170 | 0 | 380 | 380 | 0 | 23.167 | 60.927 | 62.719 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 22.271 | 60.927 | 77.311 |
