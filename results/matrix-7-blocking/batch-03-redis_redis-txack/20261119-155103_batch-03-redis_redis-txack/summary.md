# batch-03-redis_redis-txack — FAIL ❌

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.101s, closing 0.724s, drain 6.416s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814633 | 814633 | 12853.251 | 12825.218 | 24191.4 | 0 | 0 | 0 | 431 | 4254 | 73271 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642910 | 524.458 | 21.999 | 1960.959 | 4616.191 | 6926.335 | 10469.375 |
| e2e_from_send | 642910 | 523.316 | 20.847 | 1959.935 | 4616.191 | 6926.335 | 10461.183 |
| producer_lag | 642910 | 1.142 | 0.52 | 2.575 | 9.791 | 29.999 | 66.687 |
| publish_call | 569742 | 0.766 | 0.427 | 0.965 | 7.975 | 21.439 | 405.759 |
| queue_wait | 642910 | 510.166 | 18.879 | 1922.047 | 4567.039 | 6873.087 | 9420.799 |
| broker_dwell | 642910 | 509.981 | 18.031 | 1922.047 | 4567.039 | 6873.087 | 9420.799 |
| receive_call | 444335 | 1.272 | 0.559 | 1.511 | 11.487 | 58.335 | 397.567 |
| ctx_load | 642910 | 0.589 | 0.378 | 0.775 | 5.795 | 16.911 | 61.183 |
| process | 642910 | 10.899 | 0.001 | 0.002 | 0.004 | 2623.487 | 3000.319 |
| ctx_commit | 642910 | 0.6 | 0.38 | 0.829 | 6.059 | 15.311 | 61.151 |
| service | 642910 | 13.15 | 0.92 | 2.129 | 18.799 | 2672.639 | 5410.815 |
| e2e_large | 331 | 4116.964 | 3809.279 | 6512.639 | 8675.327 | 10469.375 | 10469.375 |
| ready_wait | 642910 | 203.541 | 7.423 | 670.207 | 2820.095 | 4984.831 | 8179.711 |
| blocked | 642910 | 203.046 | 7.239 | 667.135 | 2818.047 | 4976.639 | 8179.711 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12825.218 | 14400.7 |
| applied_total | 462211.857 | 770771 |
| bench_cpu_cores | 4.272 | 4.516 |
| bench_cpu_pct | 17.802 | 18.82 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 0.898 | 2 |
| bench_gc_gen2 | 0.204 | 1 |
| bench_gc_heap_mb | 1297.331 | 1668.6 |
| bench_mem_mb | 1610.633 | 1724.9 |
| bench_threadpool_queue | 20.245 | 160 |
| bench_threadpool_threads | 31.265 | 34 |
| broker_backlog | 6789 | 7646 |
| ctr_eb_kafka_cpu_pct | 1.498 | 1.84 |
| ctr_eb_kafka_mem_mb | 2087.6 | 2087.6 |
| ctr_eb_mssql_cpu_pct | 13.091 | 93.71 |
| ctr_eb_mssql_mem_mb | 4517.6 | 4588.1 |
| ctr_eb_nats_cpu_pct | 0.453 | 2.65 |
| ctr_eb_nats_mem_mb | 302.947 | 303.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.6 | 16.6 |
| ctr_eb_postgres_cpu_pct | 0.232 | 0.42 |
| ctr_eb_postgres_mem_mb | 4544.3 | 4544.3 |
| ctr_eb_rabbitmq_cpu_pct | 8.609 | 9.87 |
| ctr_eb_rabbitmq_mem_mb | 934.969 | 939.2 |
| ctr_eb_redis_cpu_pct | 206.415 | 293.61 |
| ctr_eb_redis_mem_mb | 1010.929 | 1421 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 15.339 | 23.83 |
| in_flight | 6631.531 | 8067 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.251 | 13453.8 |
| produced_total | 468843.388 | 777632 |
| publish_mb_per_s | 44.255 | 67.3 |
| redis_aof_buffer_length | 374304.469 | 3142184 |
| redis_aof_current_size | 1454.367 | 2315.1 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.163 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 215137.102 | 227799 |
| redis_total_commands_processed_per_s | 215396.222 | 224836.5 |
| redis_total_net_input_bytes_per_s | 87074245.073 | 114246803.5 |
| redis_total_net_output_bytes_per_s | 61066169.731 | 83678750 |
| redis_used_cpu_sys_cores | 0.964 | 1.3 |
| redis_used_cpu_sys_main_thread_cores | 0.182 | 0.31 |
| redis_used_cpu_user_cores | 0.923 | 1.05 |
| redis_used_cpu_user_main_thread_cores | 0.46 | 0.51 |
| redis_used_memory | 935.818 | 1374.8 |
| redisq_aof_buffer_length | 767007.122 | 5692979 |
| redisq_aof_current_size | 1453.553 | 2315 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.245 | 1 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 215116 | 227799 |
| redisq_total_commands_processed_per_s | 215397.227 | 224813 |
| redisq_total_net_input_bytes_per_s | 87075683.424 | 114211156 |
| redisq_total_net_output_bytes_per_s | 61080942.145 | 83990829.3 |
| redisq_used_cpu_sys_cores | 0.964 | 1.3 |
| redisq_used_cpu_sys_main_thread_cores | 0.182 | 0.31 |
| redisq_used_cpu_user_cores | 0.924 | 1.05 |
| redisq_used_cpu_user_main_thread_cores | 0.46 | 0.51 |
| redisq_used_memory | 936.616 | 1374.7 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,633 events applied by workers, 814,633 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 73,271 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2816 ms, max 8179 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14413 seq 3: ready 111 ms before start, 110 ms of it with an idle slot in group 459
- [cross_source_blocking] source 13212 seq 13: ready 223 ms before start, 221 ms of it with an idle slot in group 421
- [cross_source_blocking] source 3513 seq 20: ready 153 ms before start, 153 ms of it with an idle slot in group 120
- [cross_source_blocking] source 4564 seq 7: ready 224 ms before start, 224 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4598 seq 11: ready 246 ms before start, 246 ms of it with an idle slot in group 160
- [cross_source_blocking] source 10395 seq 5: ready 297 ms before start, 297 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3896 seq 7: ready 178 ms before start, 178 ms of it with an idle slot in group 131
- [cross_source_blocking] source 192 seq 20: ready 177 ms before start, 177 ms of it with an idle slot in group 4
- [cross_source_blocking] source 206 seq 13: ready 157 ms before start, 157 ms of it with an idle slot in group 4
- [cross_source_blocking] source 9613 seq 6: ready 165 ms before start, 149 ms of it with an idle slot in group 333
- [cross_source_blocking] source 9643 seq 5: ready 213 ms before start, 197 ms of it with an idle slot in group 333
- [cross_source_blocking] source 2984 seq 9: ready 172 ms before start, 170 ms of it with an idle slot in group 99
- [cross_source_blocking] source 2993 seq 7: ready 307 ms before start, 305 ms of it with an idle slot in group 99
- [cross_source_blocking] source 1487 seq 16: ready 302 ms before start, 302 ms of it with an idle slot in group 45
- [cross_source_blocking] source 4886 seq 5: ready 428 ms before start, 424 ms of it with an idle slot in group 169
- [cross_source_blocking] source 6377 seq 2: ready 406 ms before start, 402 ms of it with an idle slot in group 218
- [cross_source_blocking] source 4905 seq 7: ready 391 ms before start, 385 ms of it with an idle slot in group 169
- [cross_source_blocking] source 6367 seq 4: ready 149 ms before start, 144 ms of it with an idle slot in group 218
- [cross_source_blocking] source 4899 seq 7: ready 123 ms before start, 118 ms of it with an idle slot in group 169
- [cross_source_blocking] source 4894 seq 13: ready 234 ms before start, 229 ms of it with an idle slot in group 169

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 3702.783 | 8716.287 | 10485.759 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 19.839 | 7831.551 | 9830.399 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 3309.567 | 7667.711 | 7667.711 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 27.007 | 7634.943 | 8716.287 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 1028.095 | 7602.175 | 8355.839 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 411.647 | 7241.727 | 7766.015 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 720.895 | 7241.727 | 8290.303 |
| t-040 | 119 | 2 | 26.219 | 52.801 | 1254 | 0 | 3453 | 3453 | 0 | 24.191 | 6782.975 | 7569.407 |
| t-313 | 35 | 1 | 35.066 | 20.997 | 510 | 0 | 1315 | 1315 | 0 | 23.295 | 6619.135 | 6979.583 |
| t-429 | 200 | 2 | 18.117 | 61.748 | 1395 | 0 | 4071 | 4071 | 0 | 66.559 | 6586.367 | 7569.407 |
| t-253 | 26 | 1 | 73.629 | 32.505 | 1433 | 0 | 2037 | 2037 | 0 | 16.895 | 6422.527 | 7110.655 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 958.463 | 6356.991 | 7372.799 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2703.359 | 6324.223 | 6979.583 |
| t-104 | 46 | 1 | 31.882 | 24.451 | 2163 | 0 | 1619 | 1619 | 0 | 30.463 | 6291.455 | 6946.815 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 19.711 | 6193.151 | 7438.335 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 0 | 28.287 | 6193.151 | 7143.423 |
| t-002 | 17 | 1 | 82.711 | 23.551 | 377 | 0 | 1507 | 1507 | 0 | 44.287 | 6160.383 | 6553.599 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 25.855 | 6062.079 | 6848.511 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 85.503 | 6029.311 | 6717.439 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 2110 | 2110 | 0 | 401.407 | 6029.311 | 6389.759 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 33.279 | 5963.775 | 7274.495 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 626.687 | 5472.255 | 6062.079 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1850 | 1850 | 0 | 46.079 | 5373.951 | 5799.935 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 393.215 | 5308.415 | 6193.151 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1901 | 1901 | 0 | 40.959 | 5308.415 | 5767.167 |
