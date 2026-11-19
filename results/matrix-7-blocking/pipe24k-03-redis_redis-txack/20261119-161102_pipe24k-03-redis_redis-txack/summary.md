# pipe24k-03-redis_redis-txack — FAIL ❌

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.101s, closing 0.805s, drain 3.899s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487631 | 1487631 | 24049.414 | 24163.702 | 28665.9 | 0 | 0 | 0 | 425 | 7514 | 40984 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1202207 | 91.162 | 23.935 | 147.455 | 1601.535 | 2824.191 | 6488.063 |
| e2e_from_send | 1202207 | 86.103 | 21.471 | 128.831 | 1597.439 | 2818.047 | 6483.967 |
| producer_lag | 1202207 | 5.059 | 1.555 | 10.479 | 63.391 | 187.135 | 346.623 |
| publish_call | 549252 | 2.77 | 1.244 | 6.559 | 21.359 | 57.119 | 415.487 |
| queue_wait | 1202207 | 38.894 | 12.551 | 57.215 | 603.647 | 1007.103 | 1747.967 |
| broker_dwell | 1202207 | 30.743 | 7.139 | 43.071 | 575.487 | 965.631 | 1702.911 |
| receive_call | 1154433 | 7.186 | 3.163 | 15.799 | 54.623 | 191.103 | 416.511 |
| ctx_load | 1202207 | 5.287 | 2.335 | 12.039 | 37.119 | 147.455 | 248.191 |
| process | 1202207 | 10.386 | 0.001 | 0.002 | 0.005 | 2611.199 | 3002.367 |
| ctx_commit | 1202207 | 5.855 | 2.781 | 13.671 | 37.503 | 122.751 | 233.855 |
| service | 1202207 | 47.21 | 5.707 | 30.815 | 1553.407 | 2770.943 | 6426.623 |
| e2e_large | 324 | 2290.626 | 2228.223 | 3065.855 | 4939.775 | 6488.063 | 6488.063 |
| ready_wait | 1202207 | 21.001 | 6.115 | 36.063 | 337.151 | 813.055 | 1608.703 |
| blocked | 1202207 | 20.626 | 6.099 | 35.775 | 324.863 | 801.791 | 1604.607 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24163.702 | 28298.7 |
| applied_total | 868238.673 | 1445813 |
| bench_cpu_cores | 4.21 | 5.996 |
| bench_cpu_pct | 17.54 | 24.98 |
| bench_gc_gen0 | 3.347 | 5 |
| bench_gc_gen1 | 2.204 | 4 |
| bench_gc_gen2 | 0.265 | 2 |
| bench_gc_heap_mb | 1002.416 | 1273 |
| bench_mem_mb | 1378.953 | 1487.2 |
| bench_threadpool_queue | 40.878 | 342 |
| bench_threadpool_threads | 42.408 | 44 |
| broker_backlog | 2023.571 | 4873 |
| ctr_eb_kafka_cpu_pct | 1.501 | 1.88 |
| ctr_eb_kafka_mem_mb | 1842.112 | 1842.5 |
| ctr_eb_mssql_cpu_pct | 1.139 | 1.23 |
| ctr_eb_mssql_mem_mb | 3868.949 | 3871 |
| ctr_eb_nats_cpu_pct | 0.446 | 0.57 |
| ctr_eb_nats_mem_mb | 521.5 | 521.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 28.8 | 28.8 |
| ctr_eb_postgres_cpu_pct | 0.282 | 0.49 |
| ctr_eb_postgres_mem_mb | 4859 | 4859 |
| ctr_eb_rabbitmq_cpu_pct | 8.789 | 9.43 |
| ctr_eb_rabbitmq_mem_mb | 1145.002 | 1148.2 |
| ctr_eb_redis_cpu_pct | 392.212 | 465.77 |
| ctr_eb_redis_mem_mb | 1121.686 | 1614.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 32.713 | 38.62 |
| in_flight | 1735.469 | 4959 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 24049.414 | 25868 |
| produced_total | 869974.143 | 1447209 |
| publish_mb_per_s | 64.064 | 89.55 |
| redis_aof_buffer_length | 566393.327 | 5595219 |
| redis_aof_current_size | 1729.214 | 3124.2 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 377656.49 | 411814 |
| redis_total_commands_processed_per_s | 377294.847 | 407521 |
| redis_total_net_input_bytes_per_s | 124824700.378 | 152533660.9 |
| redis_total_net_output_bytes_per_s | 93872326.284 | 114434109.6 |
| redis_used_cpu_sys_cores | 1.837 | 2.41 |
| redis_used_cpu_sys_main_thread_cores | 0.097 | 0.17 |
| redis_used_cpu_user_cores | 1.721 | 1.88 |
| redis_used_cpu_user_main_thread_cores | 0.852 | 0.91 |
| redis_used_memory | 1002.461 | 1486.6 |
| redisq_aof_buffer_length | 156140.653 | 2521545 |
| redisq_aof_current_size | 1727.455 | 3121.7 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.041 | 1 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 378121.918 | 411814 |
| redisq_total_commands_processed_per_s | 377180.418 | 408848.8 |
| redisq_total_net_input_bytes_per_s | 124884284.19 | 154400330.7 |
| redisq_total_net_output_bytes_per_s | 93822601.145 | 114475839.6 |
| redisq_used_cpu_sys_cores | 1.837 | 2.39 |
| redisq_used_cpu_sys_main_thread_cores | 0.097 | 0.18 |
| redisq_used_cpu_user_cores | 1.723 | 1.87 |
| redisq_used_cpu_user_main_thread_cores | 0.852 | 0.91 |
| redisq_used_memory | 1001.533 | 1486.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,924 records are Completed |
| record_counts | pass | 193,924 records found, 193,924 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,631 events applied by workers, 1,487,631 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 40,984 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 325 ms, max 1604 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12956 seq 18: ready 105 ms before start, 101 ms of it with an idle slot in group 409
- [cross_source_blocking] source 13993 seq 7: ready 109 ms before start, 104 ms of it with an idle slot in group 445
- [cross_source_blocking] source 4969 seq 7: ready 143 ms before start, 143 ms of it with an idle slot in group 170
- [cross_source_blocking] source 9116 seq 17: ready 108 ms before start, 108 ms of it with an idle slot in group 315
- [cross_source_blocking] source 5407 seq 29: ready 132 ms before start, 114 ms of it with an idle slot in group 184
- [cross_source_blocking] source 13143 seq 18: ready 104 ms before start, 104 ms of it with an idle slot in group 417
- [cross_source_blocking] source 13832 seq 22: ready 153 ms before start, 153 ms of it with an idle slot in group 438
- [cross_source_blocking] source 10976 seq 8: ready 116 ms before start, 116 ms of it with an idle slot in group 359
- [cross_source_blocking] source 4304 seq 41: ready 101 ms before start, 101 ms of it with an idle slot in group 147
- [cross_source_blocking] source 10344 seq 30: ready 114 ms before start, 114 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7368 seq 25: ready 128 ms before start, 128 ms of it with an idle slot in group 253
- [cross_source_blocking] source 2640 seq 6: ready 104 ms before start, 104 ms of it with an idle slot in group 81
- [cross_source_blocking] source 15560 seq 14: ready 128 ms before start, 128 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11011 seq 9: ready 116 ms before start, 116 ms of it with an idle slot in group 359
- [cross_source_blocking] source 963 seq 8: ready 116 ms before start, 116 ms of it with an idle slot in group 28
- [cross_source_blocking] source 2979 seq 8: ready 114 ms before start, 114 ms of it with an idle slot in group 98
- [cross_source_blocking] source 8150 seq 7: ready 116 ms before start, 111 ms of it with an idle slot in group 276
- [cross_source_blocking] source 8070 seq 31: ready 100 ms before start, 100 ms of it with an idle slot in group 271
- [cross_source_blocking] source 13284 seq 18: ready 140 ms before start, 140 ms of it with an idle slot in group 425
- [cross_source_blocking] source 10196 seq 21: ready 130 ms before start, 130 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 1933.311 | 4947.967 | 6488.063 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2007.039 | 4390.911 | 4390.911 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 1695.743 | 4259.839 | 4423.679 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1499.135 | 4128.767 | 5439.487 |
| t-295 | 12 | 1 | 103.993 | 20.658 | 3396 | 0 | 1309 | 1309 | 0 | 30.079 | 2572.287 | 4554.751 |
| t-115 | 10 | 1 | 62.309 | 9.965 | 3658 | 0 | 641 | 641 | 0 | 33.535 | 2506.751 | 2834.431 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8093 | 8093 | 0 | 24.063 | 2490.367 | 4882.431 |
| t-288 | 6 | 1 | 108.569 | 10.982 | 214 | 0 | 675 | 675 | 0 | 35.583 | 2473.983 | 3014.655 |
| t-079 | 6 | 1 | 180.755 | 18.586 | 1522 | 0 | 1136 | 1136 | 0 | 32.639 | 2441.215 | 3686.399 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5769 | 5769 | 0 | 23.295 | 2424.831 | 4177.919 |
| t-090 | 7 | 1 | 74.668 | 8.784 | 242 | 0 | 527 | 527 | 0 | 34.815 | 2375.679 | 3014.655 |
| t-346 | 12 | 1 | 52.432 | 10.16 | 1247 | 0 | 648 | 648 | 0 | 36.863 | 2375.679 | 2965.503 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 1384 | 1384 | 0 | 29.311 | 2375.679 | 2965.503 |
| t-082 | 42 | 1 | 33.494 | 23.642 | 150 | 0 | 1579 | 1579 | 0 | 26.751 | 2342.911 | 2981.887 |
| t-299 | 21 | 1 | 79.203 | 27.085 | 646 | 0 | 1726 | 1726 | 0 | 25.983 | 2326.527 | 3686.399 |
| t-067 | 11 | 1 | 162.679 | 29.921 | 3490 | 0 | 1763 | 1763 | 0 | 25.727 | 2310.143 | 4521.983 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 9502 | 9502 | 0 | 24.831 | 2277.375 | 3457.023 |
| t-302 | 12 | 1 | 172.999 | 37 | 1189 | 0 | 2259 | 2259 | 0 | 25.855 | 2228.223 | 2981.887 |
| t-365 | 13 | 1 | 65.624 | 14.161 | 3428 | 0 | 925 | 925 | 0 | 30.463 | 2211.839 | 2981.887 |
| t-427 | 16 | 2 | 207.863 | 55.174 | 1711 | 0 | 3315 | 3315 | 0 | 26.111 | 2211.839 | 3162.111 |
| t-462 | 10 | 1 | 47.972 | 7.784 | 2006 | 0 | 470 | 470 | 0 | 36.095 | 2211.839 | 2965.503 |
| t-355 | 10 | 1 | 56.521 | 9.909 | 349 | 0 | 667 | 667 | 0 | 34.815 | 2195.455 | 2932.735 |
| t-447 | 17 | 1 | 84.714 | 22.861 | 558 | 0 | 1451 | 1451 | 0 | 29.439 | 2179.071 | 2998.271 |
| t-056 | 7 | 1 | 90.183 | 10.895 | 347 | 0 | 664 | 664 | 0 | 33.535 | 2162.687 | 2539.519 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 7775 | 7775 | 0 | 23.807 | 2162.687 | 3014.655 |
