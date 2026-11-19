# 10-rabbit_redis — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.142s, closing 1.246s, drain 3.77s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814480 | 814480 | 12854.4 | 12854.402 | 24900.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642761 | 14.689 | 7.659 | 28.367 | 127.743 | 222.463 | 274.431 |
| e2e_from_send | 642761 | 9.534 | 5.207 | 19.759 | 63.391 | 105.087 | 170.879 |
| producer_lag | 642761 | 5.155 | 2.213 | 8.687 | 73.023 | 184.191 | 222.719 |
| publish_call | 330981 | 5.223 | 3.695 | 7.695 | 22.175 | 169.471 | 733.695 |
| queue_wait | 642761 | 7.209 | 4.095 | 13.215 | 54.207 | 97.855 | 167.295 |
| broker_dwell | 642761 | 7.163 | 4.087 | 13.175 | 52.575 | 94.463 | 167.295 |
| receive_call | 734181 | 35.958 | 22.127 | 90.943 | 178.943 | 199.423 | 246.527 |
| ctx_load | 642761 | 1.166 | 0.44 | 2.233 | 14.303 | 22.287 | 42.463 |
| process | 642761 | 0.002 | 0.001 | 0.002 | 0.004 | 0.059 | 6.159 |
| ctx_commit | 642761 | 0.853 | 0.442 | 1.759 | 6.371 | 20.319 | 43.551 |
| ack | 559079 | 0.002 | 0.001 | 0.002 | 0.005 | 0.042 | 2.459 |
| service | 642761 | 2.325 | 0.922 | 5.343 | 20.671 | 43.935 | 107.199 |
| e2e_large | 331 | 38.181 | 33.119 | 60.319 | 130.431 | 211.071 | 211.071 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.402 | 14245.5 |
| applied_total | 468758.673 | 777613 |
| bench_cpu_cores | 2.251 | 2.551 |
| bench_cpu_pct | 9.377 | 10.63 |
| bench_gc_gen0 | 3.041 | 7 |
| bench_gc_gen1 | 2.347 | 7 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 502.014 | 686.7 |
| bench_mem_mb | 781.831 | 876.3 |
| bench_threadpool_queue | 1.694 | 21 |
| bench_threadpool_threads | 33.633 | 44 |
| broker_backlog | 103.694 | 183 |
| broker_consumers | 598 | 598 |
| broker_ready | 0 | 0 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 103.694 | 183 |
| ctr_eb_kafka_cpu_pct | 1.704 | 2.98 |
| ctr_eb_kafka_mem_mb | 3043.516 | 3046 |
| ctr_eb_mssql_cpu_pct | 1.476 | 3.33 |
| ctr_eb_mssql_mem_mb | 6184.339 | 6184.6 |
| ctr_eb_nats_cpu_pct | 2.044 | 9.7 |
| ctr_eb_nats_mem_mb | 2979.594 | 2980.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.5 | 10.5 |
| ctr_eb_postgres_cpu_pct | 0.171 | 0.3 |
| ctr_eb_postgres_mem_mb | 3113.663 | 3113.7 |
| ctr_eb_rabbitmq_cpu_pct | 569.364 | 607.28 |
| ctr_eb_rabbitmq_mem_mb | 1208.063 | 1250.3 |
| ctr_eb_redis_cpu_pct | 118.619 | 167.42 |
| ctr_eb_redis_mem_mb | 918.88 | 1360.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 39.819 | 43.99 |
| in_flight | 27.857 | 404 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12854.4 | 14263.5 |
| produced_total | 468786.531 | 777613 |
| publish_mb_per_s | 44.335 | 70.83 |
| redis_aof_buffer_length | 1373.388 | 20821 |
| redis_aof_current_size | 915.602 | 1443.6 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.041 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 16 | 16 |
| redis_instantaneous_ops_per_sec | 108579.653 | 111235 |
| redis_total_commands_processed_per_s | 108668.996 | 110668.8 |
| redis_total_net_input_bytes_per_s | 33448769.255 | 47852474.1 |
| redis_total_net_output_bytes_per_s | 12238017.747 | 12635172.6 |
| redis_used_cpu_sys_cores | 0.459 | 0.67 |
| redis_used_cpu_sys_main_thread_cores | 0.059 | 0.12 |
| redis_used_cpu_user_cores | 0.622 | 0.71 |
| redis_used_cpu_user_main_thread_cores | 0.337 | 0.4 |
| redis_used_memory | 879.731 | 1322.1 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,576 records are Completed |
| record_counts | pass | 117,576 records found, 117,576 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,480 events applied by workers, 814,480 generated |
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
| t-134 | 21 | 1 | 19.598 | 6.714 | 3368 | 0 | 453 | 453 | 0 | 7.935 | 225.279 | 246.783 |
| t-433 | 6 | 1 | 21.23 | 2.112 | 184 | 0 | 132 | 132 | 0 | 7.711 | 202.751 | 228.351 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 8.063 | 200.703 | 227.327 |
| t-249 | 15 | 1 | 23.883 | 6.172 | 691 | 0 | 413 | 413 | 0 | 7.839 | 194.559 | 237.567 |
| t-255 | 11 | 1 | 35.499 | 6.413 | 2029 | 0 | 385 | 385 | 0 | 7.807 | 192.511 | 216.063 |
| t-240 | 8 | 1 | 65.145 | 9.617 | 361 | 0 | 611 | 611 | 0 | 7.615 | 190.463 | 202.751 |
| t-424 | 8 | 1 | 76.354 | 10.019 | 1700 | 0 | 599 | 599 | 0 | 7.775 | 189.439 | 208.895 |
| t-256 | 14 | 1 | 36.08 | 7.98 | 2927 | 0 | 533 | 533 | 0 | 7.807 | 188.415 | 211.967 |
| t-431 | 6 | 1 | 37.575 | 3.743 | 596 | 0 | 226 | 226 | 0 | 7.839 | 186.367 | 214.015 |
| t-146 | 6 | 1 | 43.291 | 4.645 | 246 | 0 | 274 | 274 | 0 | 8.159 | 183.295 | 216.063 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 7.487 | 181.247 | 214.015 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 495 | 495 | 0 | 7.743 | 178.175 | 223.231 |
| t-355 | 10 | 1 | 30.225 | 5.299 | 349 | 0 | 354 | 354 | 0 | 7.711 | 178.175 | 221.183 |
| t-175 | 27 | 1 | 57.972 | 26.877 | 184 | 0 | 1648 | 1648 | 0 | 7.935 | 177.151 | 235.519 |
| t-115 | 10 | 1 | 33.32 | 5.329 | 3658 | 0 | 383 | 383 | 0 | 7.935 | 176.127 | 222.207 |
| t-251 | 9 | 1 | 37.874 | 5.69 | 1193 | 0 | 358 | 358 | 0 | 7.519 | 174.079 | 204.799 |
| t-310 | 6 | 1 | 70.482 | 7.386 | 1910 | 0 | 474 | 474 | 0 | 7.743 | 172.031 | 210.943 |
| t-026 | 6 | 1 | 120.855 | 12.121 | 1596 | 0 | 717 | 717 | 0 | 7.807 | 171.007 | 236.543 |
| t-227 | 17 | 1 | 53.386 | 14.56 | 850 | 0 | 920 | 920 | 0 | 7.743 | 171.007 | 223.231 |
| t-244 | 11 | 1 | 32 | 5.972 | 1151 | 0 | 379 | 379 | 0 | 7.615 | 171.007 | 224.255 |
| t-010 | 26 | 1 | 42.887 | 18.144 | 183 | 0 | 1183 | 1183 | 0 | 7.487 | 169.983 | 237.567 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 28.799 | 168.959 | 222.207 |
| t-477 | 27 | 1 | 37.292 | 17.248 | 2169 | 0 | 1077 | 1077 | 0 | 7.871 | 168.959 | 251.903 |
| t-406 | 36 | 1 | 41.208 | 23.174 | 183 | 0 | 1563 | 1563 | 0 | 7.519 | 167.935 | 229.375 |
| t-448 | 6 | 1 | 47.757 | 5.012 | 2313 | 0 | 298 | 298 | 0 | 8.255 | 166.911 | 237.567 |
