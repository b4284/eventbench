# 10-rabbit_redis — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.105s, closing 0.893s, drain 3.718s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814595 | 814595 | 12851.641 | 12851.592 | 32881.8 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642871 | 23.222 | 9.767 | 51.551 | 223.487 | 321.279 | 431.615 |
| e2e_from_send | 642871 | 15.068 | 6.595 | 31.871 | 143.359 | 269.567 | 294.911 |
| producer_lag | 642871 | 8.154 | 2.817 | 17.759 | 95.295 | 209.535 | 270.335 |
| publish_call | 283905 | 6.252 | 4.095 | 9.519 | 37.375 | 168.703 | 424.959 |
| queue_wait | 642871 | 12.196 | 5.159 | 25.119 | 132.223 | 251.903 | 286.463 |
| broker_dwell | 642871 | 12.137 | 5.151 | 25.039 | 130.815 | 249.343 | 284.671 |
| receive_call | 714227 | 36.521 | 22.383 | 93.311 | 179.327 | 199.039 | 247.167 |
| ctx_load | 642871 | 1.294 | 0.482 | 2.699 | 13.631 | 21.423 | 30.271 |
| process | 642871 | 0.002 | 0.001 | 0.002 | 0.005 | 0.066 | 4.411 |
| ctx_commit | 642871 | 1.003 | 0.489 | 2.269 | 7.475 | 18.479 | 29.215 |
| ack | 562773 | 0.002 | 0.001 | 0.002 | 0.005 | 0.037 | 0.895 |
| service | 642871 | 2.872 | 1.037 | 7.267 | 23.887 | 39.807 | 86.783 |
| e2e_large | 331 | 45.934 | 38.527 | 74.815 | 193.919 | 327.423 | 327.423 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12851.592 | 14811.8 |
| applied_total | 468668.857 | 777588 |
| bench_cpu_cores | 2.049 | 2.39 |
| bench_cpu_pct | 8.538 | 9.96 |
| bench_gc_gen0 | 1.653 | 4 |
| bench_gc_gen1 | 0.735 | 3 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 524.386 | 680.4 |
| bench_mem_mb | 838.927 | 927.6 |
| bench_threadpool_queue | 3.857 | 59 |
| bench_threadpool_threads | 30.592 | 34 |
| broker_backlog | 157.51 | 314 |
| broker_consumers | 598 | 598 |
| broker_ready | 0 | 0 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 157.51 | 314 |
| ctr_eb_kafka_cpu_pct | 1.477 | 1.88 |
| ctr_eb_kafka_mem_mb | 3106.606 | 3151.9 |
| ctr_eb_mssql_cpu_pct | 1.431 | 3.1 |
| ctr_eb_mssql_mem_mb | 5763.992 | 5766.9 |
| ctr_eb_nats_cpu_pct | 2.058 | 9.61 |
| ctr_eb_nats_mem_mb | 2924.096 | 2924.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.094 | 10.1 |
| ctr_eb_postgres_cpu_pct | 0.345 | 0.48 |
| ctr_eb_postgres_mem_mb | 3062.755 | 3066.1 |
| ctr_eb_rabbitmq_cpu_pct | 534.989 | 589.58 |
| ctr_eb_rabbitmq_mem_mb | 1166.239 | 1212.1 |
| ctr_eb_redis_cpu_pct | 114.422 | 197.25 |
| ctr_eb_redis_mem_mb | 923.257 | 1337.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 36.803 | 41.2 |
| in_flight | 57.776 | 724 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12851.641 | 15120.6 |
| produced_total | 468726.633 | 777587 |
| publish_mb_per_s | 44.327 | 71.23 |
| redis_aof_buffer_length | 38691.286 | 630282 |
| redis_aof_current_size | 2312.239 | 3107.6 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.122 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 16 | 16 |
| redis_instantaneous_ops_per_sec | 107898.694 | 121411 |
| redis_total_commands_processed_per_s | 108520.249 | 118890.9 |
| redis_total_net_input_bytes_per_s | 33416201.255 | 50800446.8 |
| redis_total_net_output_bytes_per_s | 12223948.557 | 13517587.5 |
| redis_used_cpu_sys_cores | 0.454 | 0.62 |
| redis_used_cpu_sys_main_thread_cores | 0.055 | 0.14 |
| redis_used_cpu_user_cores | 0.615 | 0.74 |
| redis_used_cpu_user_main_thread_cores | 0.326 | 0.37 |
| redis_used_memory | 880.067 | 1310.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,591 records are Completed |
| record_counts | pass | 117,591 records found, 117,591 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,595 events applied by workers, 814,595 generated |
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
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 28.031 | 327.679 | 327.679 |
| t-110 | 9 | 1 | 18.885 | 2.85 | 2746 | 0 | 177 | 177 | 0 | 9.983 | 303.103 | 327.679 |
| t-068 | 21 | 1 | 19.721 | 6.772 | 3531 | 0 | 459 | 459 | 0 | 10.559 | 290.815 | 321.535 |
| t-291 | 7 | 1 | 41.565 | 4.729 | 368 | 0 | 302 | 302 | 0 | 9.663 | 286.719 | 311.295 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 335 | 335 | 0 | 9.983 | 278.527 | 311.295 |
| t-155 | 14 | 1 | 14.59 | 3.316 | 2759 | 0 | 208 | 208 | 0 | 9.087 | 274.431 | 368.639 |
| t-472 | 32 | 1 | 25.243 | 12.984 | 1124 | 0 | 841 | 841 | 0 | 10.175 | 274.431 | 342.015 |
| t-021 | 19 | 1 | 16.257 | 5.407 | 3252 | 0 | 346 | 346 | 0 | 9.535 | 272.383 | 294.911 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 9.983 | 272.383 | 333.823 |
| t-228 | 27 | 1 | 36.757 | 16.166 | 2869 | 0 | 1061 | 1061 | 0 | 10.495 | 272.383 | 393.215 |
| t-419 | 17 | 1 | 20.486 | 6.067 | 3214 | 0 | 426 | 426 | 0 | 9.535 | 272.383 | 315.391 |
| t-137 | 10 | 1 | 21.801 | 3.669 | 1569 | 0 | 288 | 288 | 0 | 10.047 | 270.335 | 337.919 |
| t-458 | 132 | 1 | 15.687 | 34.029 | 2384 | 0 | 2377 | 2377 | 0 | 9.663 | 268.287 | 319.487 |
| t-159 | 6 | 1 | 46.704 | 4.469 | 295 | 0 | 288 | 288 | 0 | 10.879 | 266.239 | 323.583 |
| t-301 | 26 | 1 | 44.546 | 19.992 | 3339 | 0 | 1237 | 1237 | 0 | 9.663 | 266.239 | 380.927 |
| t-444 | 16 | 1 | 35.438 | 9.589 | 188 | 0 | 605 | 605 | 0 | 9.855 | 266.239 | 358.399 |
| t-066 | 38 | 1 | 41.933 | 26.44 | 277 | 0 | 1727 | 1727 | 0 | 9.791 | 264.191 | 372.735 |
| t-432 | 9 | 1 | 58.559 | 8.62 | 747 | 0 | 529 | 529 | 0 | 9.471 | 264.191 | 286.719 |
| t-481 | 12 | 1 | 121.412 | 22.891 | 1993 | 0 | 1428 | 1428 | 0 | 9.535 | 264.191 | 409.599 |
| t-332 | 39 | 1 | 58.718 | 38.195 | 812 | 0 | 2412 | 2412 | 0 | 9.983 | 261.119 | 370.687 |
| t-008 | 11 | 1 | 73.985 | 13.34 | 279 | 0 | 806 | 806 | 0 | 9.855 | 260.095 | 303.103 |
| t-125 | 20 | 1 | 64.654 | 22.573 | 1900 | 0 | 1387 | 1387 | 0 | 10.303 | 259.071 | 342.015 |
| t-140 | 13 | 1 | 63.375 | 13.868 | 429 | 0 | 847 | 847 | 0 | 9.663 | 259.071 | 335.871 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 390 | 390 | 0 | 9.535 | 259.071 | 305.151 |
| t-453 | 6 | 1 | 44.943 | 4.427 | 874 | 0 | 289 | 289 | 0 | 10.175 | 259.071 | 344.063 |
