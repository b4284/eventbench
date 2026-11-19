# 03-redis_redis-txack — PASS ✅

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.586s, drain 0.899s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814655 | 814655 | 12852.745 | 12854.924 | 24178.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642932 | 15.678 | 10.783 | 35.967 | 55.135 | 72.319 | 380.159 |
| e2e_from_send | 642932 | 14.639 | 9.559 | 34.207 | 53.503 | 67.455 | 370.943 |
| producer_lag | 642932 | 1.039 | 0.516 | 2.543 | 7.715 | 15.175 | 33.503 |
| publish_call | 570708 | 0.815 | 0.459 | 1.139 | 8.383 | 16.655 | 302.847 |
| queue_wait | 642932 | 13.05 | 8.215 | 32.447 | 51.967 | 58.847 | 350.463 |
| broker_dwell | 642932 | 12.768 | 7.963 | 32.255 | 51.711 | 56.831 | 349.951 |
| receive_call | 596230 | 1.334 | 0.643 | 2.075 | 10.951 | 56.447 | 299.519 |
| ctx_load | 642932 | 0.589 | 0.393 | 0.83 | 5.179 | 12.871 | 39.231 |
| process | 642932 | 0.002 | 0.001 | 0.002 | 0.005 | 0.023 | 6.103 |
| ctx_commit | 642932 | 0.58 | 0.376 | 0.851 | 5.119 | 13.343 | 37.695 |
| service | 642932 | 1.589 | 0.973 | 2.379 | 13.983 | 28.815 | 87.103 |
| e2e_large | 331 | 44.093 | 43.391 | 65.919 | 81.663 | 104.767 | 104.767 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.924 | 13210.9 |
| applied_total | 468724 | 777458 |
| bench_cpu_cores | 4.413 | 4.651 |
| bench_cpu_pct | 18.385 | 19.38 |
| bench_gc_gen0 | 4.816 | 10 |
| bench_gc_gen1 | 2.857 | 9 |
| bench_gc_gen2 | 0.327 | 2 |
| bench_gc_heap_mb | 744.047 | 1031.8 |
| bench_mem_mb | 1088.576 | 1254.9 |
| bench_threadpool_queue | 13.673 | 183 |
| bench_threadpool_threads | 29.531 | 31 |
| broker_backlog | 203.735 | 325 |
| ctr_eb_kafka_cpu_pct | 1.419 | 1.81 |
| ctr_eb_kafka_mem_mb | 1937.9 | 1937.9 |
| ctr_eb_mssql_cpu_pct | 3.759 | 37.97 |
| ctr_eb_mssql_mem_mb | 7134.433 | 7135.4 |
| ctr_eb_nats_cpu_pct | 0.463 | 2.6 |
| ctr_eb_nats_mem_mb | 360.6 | 360.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.3 | 10.3 |
| ctr_eb_postgres_cpu_pct | 0.19 | 0.37 |
| ctr_eb_postgres_mem_mb | 2193.5 | 2193.5 |
| ctr_eb_rabbitmq_cpu_pct | 8.061 | 9.09 |
| ctr_eb_rabbitmq_mem_mb | 628.755 | 632.2 |
| ctr_eb_redis_cpu_pct | 213.586 | 295.53 |
| ctr_eb_redis_mem_mb | 948.831 | 1367.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 13.015 | 24.54 |
| in_flight | 159.041 | 268 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12852.745 | 13143.9 |
| produced_total | 468883.041 | 777622 |
| publish_mb_per_s | 44.33 | 70.95 |
| redis_aof_buffer_length | 211533.531 | 4847994 |
| redis_aof_current_size | 1346.659 | 2227.9 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.041 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 229950.673 | 233256 |
| redis_total_commands_processed_per_s | 230308.773 | 234681.1 |
| redis_total_net_input_bytes_per_s | 87097416.018 | 119619905.6 |
| redis_total_net_output_bytes_per_s | 60550077.663 | 80305467.2 |
| redis_used_cpu_sys_cores | 1.029 | 1.37 |
| redis_used_cpu_sys_main_thread_cores | 0.21 | 0.26 |
| redis_used_cpu_user_cores | 0.881 | 1.01 |
| redis_used_cpu_user_main_thread_cores | 0.424 | 0.47 |
| redis_used_memory | 900.4 | 1358.6 |
| redisq_aof_buffer_length | 200972 | 4713288 |
| redisq_aof_current_size | 1346.539 | 2227.8 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.041 | 1 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 229995.163 | 233256 |
| redisq_total_commands_processed_per_s | 230317.32 | 235043.9 |
| redisq_total_net_input_bytes_per_s | 87103649.708 | 119740056.8 |
| redisq_total_net_output_bytes_per_s | 60555528.118 | 77269666.3 |
| redisq_used_cpu_sys_cores | 1.03 | 1.37 |
| redisq_used_cpu_sys_main_thread_cores | 0.21 | 0.26 |
| redisq_used_cpu_user_cores | 0.881 | 1.01 |
| redisq_used_cpu_user_main_thread_cores | 0.423 | 0.47 |
| redisq_used_memory | 900.42 | 1358.8 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,655 events applied by workers, 814,655 generated |
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
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 38.911 | 81.919 | 81.919 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 40.447 | 81.407 | 104.959 |
| t-166 | 6 | 1 | 26.126 | 2.51 | 292 | 0 | 147 | 147 | 0 | 24.063 | 73.727 | 75.263 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 29.439 | 68.607 | 111.103 |
| t-155 | 14 | 1 | 14.59 | 3.316 | 2759 | 0 | 208 | 208 | 0 | 23.551 | 68.095 | 70.655 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 22.143 | 68.095 | 120.831 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 347 | 347 | 0 | 23.295 | 68.095 | 88.575 |
| t-085 | 19 | 1 | 28.267 | 8.788 | 1157 | 0 | 582 | 582 | 0 | 21.247 | 67.583 | 364.543 |
| t-412 | 6 | 1 | 28.435 | 2.632 | 468 | 0 | 164 | 164 | 0 | 31.359 | 67.583 | 71.167 |
| t-433 | 6 | 1 | 21.23 | 2.112 | 184 | 0 | 132 | 132 | 0 | 26.623 | 67.583 | 76.799 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 42.239 | 67.071 | 72.703 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 37.119 | 66.559 | 74.751 |
| t-442 | 6 | 1 | 18.007 | 1.885 | 577 | 0 | 120 | 120 | 0 | 28.543 | 66.047 | 66.559 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 299 | 299 | 0 | 23.295 | 65.535 | 69.119 |
| t-241 | 27 | 1 | 11.444 | 5.213 | 2685 | 0 | 386 | 386 | 0 | 24.063 | 65.535 | 73.727 |
| t-318 | 7 | 1 | 31.766 | 3.874 | 686 | 0 | 252 | 252 | 0 | 22.143 | 65.023 | 75.775 |
| t-335 | 6 | 1 | 28.977 | 2.726 | 450 | 0 | 179 | 179 | 0 | 25.983 | 65.023 | 69.119 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 20.479 | 64.767 | 68.607 |
| t-092 | 29 | 1 | 33.741 | 16.54 | 519 | 0 | 1056 | 1056 | 0 | 14.911 | 64.767 | 75.775 |
| t-142 | 15 | 1 | 26.92 | 6.556 | 413 | 0 | 428 | 428 | 0 | 20.735 | 64.767 | 77.823 |
| t-154 | 18 | 1 | 20.808 | 6.162 | 2158 | 0 | 403 | 403 | 0 | 23.423 | 64.767 | 75.263 |
| t-227 | 17 | 1 | 53.386 | 14.56 | 850 | 0 | 920 | 920 | 0 | 15.487 | 64.767 | 124.927 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 21.375 | 63.999 | 69.119 |
| t-134 | 21 | 1 | 19.598 | 6.714 | 3368 | 0 | 453 | 453 | 0 | 21.119 | 63.743 | 86.015 |
| t-190 | 6 | 1 | 69.297 | 7.234 | 241 | 0 | 436 | 436 | 0 | 22.655 | 63.743 | 258.047 |
