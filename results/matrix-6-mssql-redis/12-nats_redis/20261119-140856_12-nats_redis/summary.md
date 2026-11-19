# 12-nats_redis — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.106s, closing 0.857s, drain 0.209s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814634 | 814634 | 12853.047 | 12853.384 | 35981.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642908 | 5.355 | 3.749 | 11.007 | 25.503 | 44.351 | 85.247 |
| e2e_from_send | 642908 | 4.292 | 2.707 | 9.151 | 22.127 | 39.903 | 79.231 |
| producer_lag | 642908 | 1.063 | 0.638 | 2.609 | 6.163 | 14.767 | 44.447 |
| publish_call | 544448 | 1.392 | 0.842 | 2.769 | 8.631 | 19.743 | 634.879 |
| queue_wait | 642908 | 1.9 | 1.097 | 4.179 | 12.943 | 23.951 | 73.855 |
| broker_dwell | 642908 | 1.177 | 0.799 | 1.926 | 9.743 | 20.959 | 70.335 |
| receive_call | 732371 | 46.801 | 24.303 | 104.383 | 382.463 | 786.943 | 1080.319 |
| ctx_load | 642908 | 0.747 | 0.285 | 0.738 | 11.223 | 18.399 | 37.759 |
| process | 642908 | 0.002 | 0.001 | 0.002 | 0.004 | 0.025 | 5.119 |
| ctx_commit | 642908 | 0.462 | 0.294 | 0.665 | 3.353 | 12.495 | 34.943 |
| ack | 634403 | 0.011 | 0.002 | 0.006 | 0.043 | 2.543 | 10.455 |
| service | 642908 | 2.392 | 1.438 | 4.811 | 16.367 | 27.311 | 67.455 |
| e2e_large | 331 | 32.389 | 30.943 | 51.199 | 69.183 | 85.247 | 85.247 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.384 | 13131.8 |
| applied_total | 468860.898 | 777645 |
| bench_cpu_cores | 5.943 | 6.409 |
| bench_cpu_pct | 24.763 | 26.71 |
| bench_gc_gen0 | 2.878 | 4 |
| bench_gc_gen1 | 1.388 | 3 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 759.737 | 961.4 |
| bench_mem_mb | 1133.747 | 1238.3 |
| bench_threadpool_queue | 3.673 | 24 |
| bench_threadpool_threads | 32.776 | 44 |
| broker_backlog | 50.592 | 175 |
| broker_backlog_mb | 0.805 | 7.528 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.547 | 1.87 |
| ctr_eb_kafka_mem_mb | 3219.6 | 3219.6 |
| ctr_eb_mssql_cpu_pct | 1.456 | 1.56 |
| ctr_eb_mssql_mem_mb | 7415.8 | 7415.8 |
| ctr_eb_nats_cpu_pct | 428.482 | 443.58 |
| ctr_eb_nats_mem_mb | 2095.02 | 2719.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.8 | 9.8 |
| ctr_eb_postgres_cpu_pct | 0.397 | 0.54 |
| ctr_eb_postgres_mem_mb | 3021.145 | 3022.2 |
| ctr_eb_rabbitmq_cpu_pct | 11.05 | 12.51 |
| ctr_eb_rabbitmq_mem_mb | 925.82 | 929 |
| ctr_eb_redis_cpu_pct | 151.478 | 213.5 |
| ctr_eb_redis_mem_mb | 911.069 | 1341.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 51.617 | 57.49 |
| in_flight | 23.408 | 179 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.047 | 13129.8 |
| produced_total | 468884.306 | 777660 |
| publish_mb_per_s | 44.332 | 71.04 |
| redis_aof_buffer_length | 342.837 | 2611 |
| redis_aof_current_size | 896.961 | 1374.1 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 16 | 16 |
| redis_instantaneous_ops_per_sec | 108801.939 | 109809 |
| redis_total_commands_processed_per_s | 108658.106 | 109473.6 |
| redis_total_net_input_bytes_per_s | 33488227.706 | 49032421.3 |
| redis_total_net_output_bytes_per_s | 12233180.78 | 12514071.7 |
| redis_used_cpu_sys_cores | 0.607 | 0.81 |
| redis_used_cpu_sys_main_thread_cores | 0.113 | 0.2 |
| redis_used_cpu_user_cores | 0.763 | 0.9 |
| redis_used_cpu_user_main_thread_cores | 0.441 | 0.54 |
| redis_used_memory | 873.578 | 1322.5 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,634 events applied by workers, 814,634 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 24.447 | 69.631 | 85.503 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 23.935 | 67.071 | 67.071 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 20.095 | 60.415 | 69.119 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 17.023 | 52.223 | 68.095 |
| t-435 | 8 | 1 | 39.599 | 5.882 | 3763 | 0 | 370 | 370 | 0 | 3.727 | 34.559 | 42.751 |
| t-254 | 9 | 1 | 21.121 | 3.28 | 3623 | 0 | 202 | 202 | 0 | 3.679 | 34.303 | 40.447 |
| t-217 | 8 | 1 | 17.717 | 2.196 | 1936 | 0 | 153 | 153 | 0 | 3.615 | 33.791 | 40.191 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 3.727 | 33.279 | 46.335 |
| t-150 | 15 | 1 | 31.013 | 7.923 | 990 | 0 | 521 | 521 | 0 | 3.631 | 32.767 | 55.039 |
| t-448 | 6 | 1 | 47.757 | 5.012 | 2313 | 0 | 298 | 298 | 0 | 3.759 | 32.127 | 52.223 |
| t-028 | 27 | 1 | 20.607 | 9.156 | 454 | 0 | 599 | 599 | 0 | 3.935 | 31.615 | 42.751 |
| t-331 | 12 | 1 | 35.895 | 7.376 | 499 | 0 | 486 | 486 | 0 | 3.423 | 31.487 | 51.967 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 3.903 | 30.591 | 47.359 |
| t-479 | 34 | 1 | 37.194 | 20.976 | 3383 | 0 | 1329 | 1329 | 0 | 3.599 | 30.207 | 46.335 |
| t-233 | 56 | 1 | 13.701 | 12.685 | 208 | 0 | 834 | 834 | 0 | 3.759 | 30.079 | 62.719 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 3.871 | 29.823 | 70.655 |
| t-384 | 12 | 1 | 22.317 | 4.426 | 844 | 0 | 292 | 292 | 0 | 3.823 | 29.823 | 33.023 |
| t-179 | 25 | 1 | 23.549 | 10.084 | 989 | 0 | 658 | 658 | 0 | 3.679 | 29.567 | 55.039 |
| t-302 | 12 | 1 | 92.513 | 19.786 | 1189 | 0 | 1248 | 1248 | 0 | 3.743 | 29.311 | 74.751 |
| t-092 | 29 | 1 | 33.741 | 16.54 | 519 | 0 | 1056 | 1056 | 0 | 3.615 | 29.183 | 43.775 |
| t-189 | 59 | 1 | 44.753 | 43.324 | 1601 | 0 | 2674 | 2674 | 0 | 3.871 | 29.055 | 48.127 |
| t-401 | 11 | 1 | 30.173 | 5.668 | 360 | 0 | 371 | 371 | 0 | 3.839 | 29.055 | 37.375 |
| t-011 | 38 | 1 | 43.137 | 28.792 | 1788 | 0 | 1832 | 1832 | 0 | 3.807 | 28.799 | 64.767 |
| t-014 | 34 | 1 | 31.511 | 17.12 | 214 | 0 | 1067 | 1067 | 0 | 3.759 | 28.799 | 53.503 |
| t-033 | 6 | 1 | 70.419 | 7.677 | 859 | 0 | 505 | 505 | 0 | 3.887 | 28.799 | 39.167 |
