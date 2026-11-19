# 07-redis_redis-txack — FAIL ❌

- Broker: `redis streams @ 127.0.0.1:6379, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False, wait replicas=0`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False, wait replicas=0`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.102s, closing 0.943s, drain 3.714s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487616 | 1487616 | 24052.176 | 24128.873 | 28508.5 | 0 | 0 | 0 | 425 | 7514 | 39698 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1202191 | 88.446 | 22.767 | 148.223 | 1614.847 | 2824.191 | 6369.279 |
| e2e_from_send | 1202191 | 83.836 | 20.463 | 132.735 | 1609.727 | 2822.143 | 6369.279 |
| producer_lag | 1202191 | 4.61 | 1.434 | 11.407 | 43.231 | 149.887 | 351.487 |
| publish_call | 546820 | 2.688 | 1.07 | 6.583 | 24.783 | 54.495 | 386.559 |
| queue_wait | 1202191 | 35.116 | 12.631 | 58.303 | 474.879 | 910.847 | 1724.415 |
| broker_dwell | 1202191 | 28.932 | 7.771 | 47.295 | 455.167 | 890.367 | 1702.911 |
| receive_call | 1155983 | 7.357 | 3.111 | 17.615 | 50.271 | 159.487 | 386.559 |
| ctx_load | 1202191 | 5.891 | 1.907 | 14.023 | 53.983 | 125.951 | 199.295 |
| process | 1202191 | 10.388 | 0.001 | 0.002 | 0.004 | 2613.247 | 3008.511 |
| ctx_commit | 1202191 | 6.441 | 2.229 | 15.671 | 53.503 | 123.199 | 202.367 |
| service | 1202191 | 48.72 | 4.699 | 37.279 | 1558.527 | 2775.039 | 6328.319 |
| e2e_large | 324 | 2290.73 | 2224.127 | 3086.335 | 4939.775 | 6369.279 | 6369.279 |
| ready_wait | 1202191 | 20.093 | 6.407 | 39.007 | 278.783 | 643.583 | 1650.687 |
| blocked | 1202191 | 19.577 | 6.391 | 38.527 | 266.239 | 611.839 | 1581.055 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24128.873 | 28508.5 |
| applied_total | 868296.673 | 1445321 |
| bench_cpu_cores | 4.595 | 6.269 |
| bench_cpu_pct | 19.146 | 26.12 |
| bench_gc_gen0 | 3.265 | 5 |
| bench_gc_gen1 | 1.776 | 3 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 1044.055 | 1360.5 |
| bench_mem_mb | 1545.492 | 1700.4 |
| bench_threadpool_queue | 26.633 | 180 |
| bench_threadpool_threads | 40.041 | 44 |
| broker_backlog | 1889.102 | 4906 |
| ctr_eb_redis_cpu_pct | 384.623 | 472.93 |
| ctr_eb_redis_mem_mb | 1114.443 | 1539.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 31.302 | 35.75 |
| in_flight | 1754.531 | 5688 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 24052.176 | 25193.9 |
| produced_total | 870051.204 | 1446708 |
| publish_mb_per_s | 64.069 | 89.52 |
| redis_aof_buffer_length | 207982.429 | 701935 |
| redis_aof_current_size | 1647.782 | 2847.2 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 32 | 32 |
| redis_instantaneous_ops_per_sec | 378769.082 | 408452 |
| redis_total_commands_processed_per_s | 378928.32 | 408773 |
| redis_total_net_input_bytes_per_s | 124822640.422 | 150980884.5 |
| redis_total_net_output_bytes_per_s | 93805717.461 | 117995667 |
| redis_used_cpu_sys_cores | 1.8 | 2.06 |
| redis_used_cpu_sys_main_thread_cores | 0.159 | 0.22 |
| redis_used_cpu_user_cores | 1.724 | 1.87 |
| redis_used_cpu_user_main_thread_cores | 0.764 | 0.82 |
| redis_used_memory | 999.416 | 1495.6 |
| redisq_aof_buffer_length | 42471.265 | 269164 |
| redisq_aof_current_size | 1646.184 | 2847.1 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0 | 0 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 32 | 32 |
| redisq_instantaneous_ops_per_sec | 379515.49 | 408452 |
| redisq_total_commands_processed_per_s | 378809.688 | 410193.7 |
| redisq_total_net_input_bytes_per_s | 124905001.386 | 148647485.4 |
| redisq_total_net_output_bytes_per_s | 93793486.931 | 118962926.2 |
| redisq_used_cpu_sys_cores | 1.801 | 2.06 |
| redisq_used_cpu_sys_main_thread_cores | 0.158 | 0.22 |
| redisq_used_cpu_user_cores | 1.724 | 1.87 |
| redisq_used_cpu_user_main_thread_cores | 0.764 | 0.82 |
| redisq_used_memory | 998.886 | 1495.7 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,921 records are Completed |
| record_counts | pass | 193,921 records found, 193,921 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,616 events applied by workers, 1,487,616 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 39,698 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 266 ms, max 1581 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2564 seq 46: ready 101 ms before start, 101 ms of it with an idle slot in group 77
- [cross_source_blocking] source 8676 seq 18: ready 101 ms before start, 101 ms of it with an idle slot in group 301
- [cross_source_blocking] source 10684 seq 17: ready 118 ms before start, 118 ms of it with an idle slot in group 350
- [cross_source_blocking] source 7260 seq 9: ready 110 ms before start, 110 ms of it with an idle slot in group 248
- [cross_source_blocking] source 4559 seq 20: ready 105 ms before start, 105 ms of it with an idle slot in group 160
- [cross_source_blocking] source 10500 seq 28: ready 101 ms before start, 101 ms of it with an idle slot in group 348
- [cross_source_blocking] source 1519 seq 17: ready 109 ms before start, 109 ms of it with an idle slot in group 45
- [cross_source_blocking] source 7247 seq 64: ready 105 ms before start, 105 ms of it with an idle slot in group 246
- [cross_source_blocking] source 14935 seq 32: ready 112 ms before start, 112 ms of it with an idle slot in group 468
- [cross_source_blocking] source 9319 seq 25: ready 109 ms before start, 109 ms of it with an idle slot in group 322
- [cross_source_blocking] source 13508 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 429
- [cross_source_blocking] source 15463 seq 19: ready 109 ms before start, 109 ms of it with an idle slot in group 483
- [cross_source_blocking] source 9503 seq 28: ready 102 ms before start, 102 ms of it with an idle slot in group 329
- [cross_source_blocking] source 11509 seq 63: ready 119 ms before start, 119 ms of it with an idle slot in group 374
- [cross_source_blocking] source 14941 seq 29: ready 114 ms before start, 114 ms of it with an idle slot in group 468
- [cross_source_blocking] source 3981 seq 57: ready 108 ms before start, 108 ms of it with an idle slot in group 132
- [cross_source_blocking] source 4771 seq 15: ready 107 ms before start, 107 ms of it with an idle slot in group 165
- [cross_source_blocking] source 6339 seq 30: ready 107 ms before start, 107 ms of it with an idle slot in group 216
- [cross_source_blocking] source 8608 seq 15: ready 108 ms before start, 108 ms of it with an idle slot in group 297
- [cross_source_blocking] source 14036 seq 11: ready 108 ms before start, 108 ms of it with an idle slot in group 446

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 1925.119 | 4947.967 | 6389.759 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2047.999 | 4358.143 | 4358.143 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 1753.087 | 4259.839 | 4390.911 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1449.983 | 4128.767 | 5406.719 |
| t-295 | 12 | 1 | 103.993 | 20.658 | 3396 | 0 | 1309 | 1309 | 0 | 27.775 | 2686.975 | 4489.215 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8093 | 8093 | 0 | 22.015 | 2506.751 | 4849.663 |
| t-115 | 10 | 1 | 62.309 | 9.965 | 3658 | 0 | 641 | 641 | 0 | 34.559 | 2473.983 | 2801.663 |
| t-288 | 6 | 1 | 108.569 | 10.982 | 214 | 0 | 675 | 675 | 0 | 34.815 | 2457.599 | 3014.655 |
| t-079 | 6 | 1 | 180.755 | 18.586 | 1522 | 0 | 1136 | 1136 | 0 | 30.463 | 2441.215 | 3670.015 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5769 | 5769 | 0 | 22.015 | 2424.831 | 4161.535 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 1384 | 1384 | 0 | 26.879 | 2392.063 | 2949.119 |
| t-082 | 42 | 1 | 33.494 | 23.642 | 150 | 0 | 1579 | 1579 | 0 | 26.111 | 2375.679 | 2998.271 |
| t-090 | 7 | 1 | 74.668 | 8.784 | 242 | 0 | 527 | 527 | 0 | 31.743 | 2375.679 | 3063.807 |
| t-346 | 12 | 1 | 52.432 | 10.16 | 1247 | 0 | 648 | 648 | 0 | 33.535 | 2359.295 | 2949.119 |
| t-067 | 11 | 1 | 162.679 | 29.921 | 3490 | 0 | 1763 | 1763 | 0 | 23.295 | 2326.527 | 4620.287 |
| t-355 | 10 | 1 | 56.521 | 9.909 | 349 | 0 | 667 | 667 | 0 | 34.815 | 2244.607 | 2981.887 |
| t-302 | 12 | 1 | 172.999 | 37 | 1189 | 0 | 2259 | 2259 | 0 | 25.215 | 2228.223 | 2932.735 |
| t-427 | 16 | 2 | 207.863 | 55.174 | 1711 | 0 | 3315 | 3315 | 0 | 26.751 | 2211.839 | 2916.351 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22483 | 22483 | 0 | 21.503 | 2195.455 | 3915.775 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 7775 | 7775 | 0 | 21.759 | 2179.071 | 2998.271 |
| t-447 | 17 | 1 | 84.714 | 22.861 | 558 | 0 | 1451 | 1451 | 0 | 27.903 | 2179.071 | 2981.887 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 12405 | 12405 | 0 | 23.423 | 2162.687 | 3309.567 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 2763 | 2763 | 0 | 20.991 | 2162.687 | 2719.743 |
| t-056 | 7 | 1 | 90.183 | 10.895 | 347 | 0 | 664 | 664 | 0 | 33.023 | 2146.303 | 2555.903 |
| t-454 | 7 | 1 | 251.84 | 29.074 | 266 | 0 | 1721 | 1721 | 0 | 27.135 | 2146.303 | 2932.735 |
