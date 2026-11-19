# 05-redis_pg — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.101s, closing 0.212s, drain 1.407s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814654 | 814654 | 12853.92 | 12854.631 | 20494 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642931 | 15.692 | 10.759 | 35.167 | 53.951 | 64.287 | 350.719 |
| e2e_from_send | 642931 | 14.83 | 9.919 | 34.175 | 53.343 | 61.983 | 348.927 |
| producer_lag | 642931 | 0.861 | 0.463 | 2.303 | 3.817 | 9.567 | 34.815 |
| publish_call | 585644 | 0.646 | 0.415 | 1.034 | 4.415 | 12.415 | 150.655 |
| queue_wait | 642931 | 12.656 | 7.847 | 32.159 | 51.423 | 54.591 | 189.695 |
| broker_dwell | 642931 | 11.882 | 7.023 | 31.375 | 50.623 | 53.247 | 188.671 |
| receive_call | 594553 | 0.954 | 0.659 | 1.313 | 6.039 | 38.143 | 149.375 |
| ctx_load | 642931 | 0.618 | 0.512 | 1.048 | 2.489 | 4.967 | 34.303 |
| process | 642931 | 0.002 | 0.001 | 0.002 | 0.004 | 0.02 | 6.015 |
| ctx_commit | 642931 | 0.607 | 0.508 | 0.955 | 2.451 | 5.955 | 224.383 |
| ack | 642931 | 0.403 | 0.254 | 0.57 | 3.413 | 9.663 | 150.143 |
| service | 642931 | 2.175 | 1.661 | 3.579 | 9.983 | 22.927 | 255.103 |
| e2e_large | 331 | 54.707 | 53.631 | 80.703 | 117.055 | 277.759 | 277.759 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.631 | 13099.6 |
| applied_total | 468729.898 | 777456 |
| bench_cpu_cores | 5.432 | 5.665 |
| bench_cpu_pct | 22.634 | 23.6 |
| bench_gc_gen0 | 3.306 | 6 |
| bench_gc_gen1 | 2.204 | 6 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 787.296 | 1051.6 |
| bench_mem_mb | 1187.59 | 1283.8 |
| bench_threadpool_queue | 20.51 | 177 |
| bench_threadpool_threads | 35.98 | 44 |
| broker_backlog | 191.551 | 254 |
| ctr_eb_kafka_cpu_pct | 1.678 | 2.07 |
| ctr_eb_kafka_mem_mb | 1932.502 | 1937.9 |
| ctr_eb_mssql_cpu_pct | 2.504 | 3.84 |
| ctr_eb_mssql_mem_mb | 7140.016 | 7140.2 |
| ctr_eb_nats_cpu_pct | 0.447 | 2.8 |
| ctr_eb_nats_mem_mb | 360.802 | 361 |
| ctr_eb_pgbouncer_cpu_pct | 54.021 | 55.73 |
| ctr_eb_pgbouncer_mem_mb | 14.941 | 16 |
| ctr_eb_postgres_cpu_pct | 235.568 | 255.91 |
| ctr_eb_postgres_mem_mb | 3050.018 | 3222.5 |
| ctr_eb_rabbitmq_cpu_pct | 10.723 | 11.55 |
| ctr_eb_rabbitmq_mem_mb | 623.886 | 633.9 |
| ctr_eb_redis_cpu_pct | 157.88 | 239 |
| ctr_eb_redis_mem_mb | 1423.006 | 1498.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 33.059 | 43.7 |
| in_flight | 163.898 | 230 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.163 | 5 |
| pg_conn_idle | 61.878 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.041 | 65 |
| pg_db_size_mb | 1317.873 | 1749.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25713.102 | 27730.6 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 58855.161 | 65043.1 |
| pg_tup_ins_per_s | 10782.58 | 20148.1 |
| pg_tup_upd_per_s | 21240.541 | 22987 |
| pg_wal_mb_per_s | 35.936 | 60.11 |
| pgb_cl_active | 457 | 457 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.061 | 19 |
| pgb_sv_idle | 57.939 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.92 | 13120.6 |
| produced_total | 468893.796 | 777617 |
| publish_mb_per_s | 44.262 | 67.79 |
| redisq_aof_buffer_length | 8517.612 | 29207 |
| redisq_aof_current_size | 2373.286 | 3298.5 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 16 | 16 |
| redisq_instantaneous_ops_per_sec | 122502.327 | 124161 |
| redisq_total_commands_processed_per_s | 122499.365 | 123854.6 |
| redisq_total_net_input_bytes_per_s | 55710705.706 | 75357131.7 |
| redisq_total_net_output_bytes_per_s | 48968595.786 | 67972615.4 |
| redisq_used_cpu_sys_cores | 0.75 | 0.91 |
| redisq_used_cpu_sys_main_thread_cores | 0.18 | 0.25 |
| redisq_used_cpu_user_cores | 0.607 | 0.67 |
| redisq_used_cpu_user_main_thread_cores | 0.302 | 0.35 |
| redisq_used_memory | 1360.58 | 1373.9 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,654 events applied by workers, 814,654 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 47.615 | 117.247 | 278.527 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 46.591 | 91.647 | 96.767 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 46.591 | 88.575 | 88.575 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 41.727 | 84.991 | 84.991 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 20.095 | 71.167 | 108.031 |
| t-462 | 10 | 1 | 25.654 | 4.163 | 2006 | 0 | 264 | 264 | 0 | 23.423 | 66.047 | 75.775 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 28.799 | 62.207 | 78.335 |
| t-255 | 11 | 1 | 35.499 | 6.413 | 2029 | 0 | 386 | 386 | 0 | 24.063 | 61.439 | 198.655 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 20.479 | 60.415 | 90.623 |
| t-291 | 7 | 1 | 41.565 | 4.729 | 368 | 0 | 302 | 302 | 0 | 20.607 | 60.415 | 61.951 |
| t-300 | 25 | 1 | 20.722 | 8.771 | 372 | 0 | 617 | 617 | 0 | 21.759 | 59.903 | 90.623 |
| t-113 | 8 | 1 | 32.487 | 4.328 | 143 | 0 | 283 | 283 | 0 | 24.959 | 59.647 | 96.255 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 347 | 347 | 0 | 26.239 | 59.647 | 68.095 |
| t-166 | 6 | 1 | 26.126 | 2.51 | 292 | 0 | 147 | 147 | 0 | 21.503 | 59.391 | 66.559 |
| t-345 | 13 | 1 | 16.657 | 3.763 | 1052 | 0 | 278 | 278 | 0 | 21.119 | 59.391 | 66.047 |
| t-284 | 12 | 1 | 27.902 | 5.305 | 217 | 0 | 362 | 362 | 0 | 25.727 | 58.879 | 69.119 |
| t-390 | 10 | 1 | 36.089 | 5.892 | 167 | 0 | 353 | 353 | 0 | 23.039 | 58.623 | 81.919 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 28.031 | 58.367 | 72.191 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 25.343 | 58.367 | 85.503 |
| t-028 | 27 | 1 | 20.607 | 9.156 | 454 | 0 | 599 | 599 | 0 | 19.583 | 58.111 | 120.831 |
| t-150 | 15 | 1 | 31.013 | 7.923 | 990 | 0 | 521 | 521 | 0 | 19.839 | 58.111 | 67.583 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 23.807 | 58.111 | 63.487 |
| t-478 | 28 | 1 | 16.799 | 7.603 | 416 | 0 | 535 | 535 | 0 | 19.199 | 58.111 | 194.559 |
| t-068 | 21 | 1 | 19.721 | 6.772 | 3531 | 0 | 459 | 459 | 0 | 22.911 | 57.855 | 61.951 |
| t-193 | 19 | 1 | 12.672 | 4.096 | 1805 | 0 | 282 | 282 | 0 | 27.903 | 57.855 | 62.207 |
