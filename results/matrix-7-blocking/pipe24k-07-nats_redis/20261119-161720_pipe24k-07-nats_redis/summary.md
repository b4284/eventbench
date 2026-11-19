# pipe24k-07-nats_redis — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `redis @ 127.0.0.1:6379, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.191s, closing 1.222s, drain 3.304s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1486938 | 1486938 | 24051.363 | 24269.763 | 27818.8 | 0 | 0 | 0 | 425 | 7511 | 163658 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1201489 | 278.73 | 124.607 | 592.383 | 2656.255 | 4939.775 | 8257.535 |
| e2e_from_send | 1201489 | 235.121 | 76.159 | 546.815 | 2611.199 | 4890.623 | 8208.383 |
| producer_lag | 1201489 | 43.609 | 38.975 | 85.375 | 137.855 | 188.927 | 258.303 |
| publish_call | 34839 | 55.602 | 59.007 | 109.055 | 193.535 | 416.767 | 638.975 |
| queue_wait | 1201489 | 195.688 | 66.431 | 416.767 | 2285.567 | 4829.183 | 5832.703 |
| broker_dwell | 1201489 | 161.485 | 31.951 | 380.415 | 2248.703 | 4792.319 | 5804.031 |
| receive_call | 926855 | 50.142 | 33.343 | 98.559 | 265.727 | 630.783 | 1288.191 |
| ctx_load | 1201489 | 1.473 | 0.379 | 3.377 | 17.679 | 36.479 | 86.079 |
| process | 1201489 | 10.392 | 0.001 | 0.002 | 0.005 | 2613.247 | 3016.703 |
| ctx_commit | 1201489 | 1.315 | 0.39 | 2.981 | 16.543 | 41.215 | 86.143 |
| ack | 1155311 | 0.02 | 0.002 | 0.01 | 0.209 | 3.301 | 15.871 |
| service | 1201489 | 39.434 | 0.966 | 30.927 | 1480.703 | 2744.319 | 6340.607 |
| e2e_large | 324 | 2338.952 | 2275.327 | 3115.007 | 4997.119 | 6410.239 | 6410.239 |
| ready_wait | 1201489 | 80.182 | 49.247 | 134.783 | 740.351 | 1820.671 | 5345.279 |
| blocked | 1201489 | 80.106 | 49.247 | 134.655 | 738.815 | 1817.599 | 5345.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24269.763 | 26258 |
| applied_total | 863490.469 | 1442295 |
| bench_cpu_cores | 5.145 | 5.586 |
| bench_cpu_pct | 21.439 | 23.27 |
| bench_gc_gen0 | 3.347 | 4 |
| bench_gc_gen1 | 3.245 | 4 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 1075.673 | 1415.8 |
| bench_mem_mb | 1680.304 | 1831.2 |
| bench_threadpool_queue | 23.612 | 297 |
| bench_threadpool_threads | 38.429 | 44 |
| broker_backlog | 5736.286 | 15415 |
| broker_backlog_mb | 60.874 | 126.027 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.614 | 2.22 |
| ctr_eb_kafka_mem_mb | 1833.404 | 1833.9 |
| ctr_eb_mssql_cpu_pct | 1.708 | 2.13 |
| ctr_eb_mssql_mem_mb | 2198.084 | 2202 |
| ctr_eb_nats_cpu_pct | 437.296 | 457.13 |
| ctr_eb_nats_mem_mb | 3573.018 | 4162.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 26.2 | 26.2 |
| ctr_eb_postgres_cpu_pct | 0.278 | 0.48 |
| ctr_eb_postgres_mem_mb | 4937.2 | 4937.2 |
| ctr_eb_rabbitmq_cpu_pct | 15.295 | 17.3 |
| ctr_eb_rabbitmq_mem_mb | 1547.518 | 1707.1 |
| ctr_eb_redis_cpu_pct | 213.54 | 304.7 |
| ctr_eb_redis_mem_mb | 956.006 | 1414.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 54.506 | 58.59 |
| in_flight | 3643.816 | 11263 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 24051.363 | 25790.1 |
| produced_total | 867134.286 | 1444302 |
| publish_mb_per_s | 64.01 | 89.77 |
| redis_aof_buffer_length | 1116.653 | 7933 |
| redis_aof_current_size | 1810.316 | 3353.8 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0 | 0 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 16 | 16 |
| redis_instantaneous_ops_per_sec | 205239.082 | 219708 |
| redis_total_commands_processed_per_s | 205451.316 | 219138.9 |
| redis_total_net_input_bytes_per_s | 47667740.651 | 64007910.8 |
| redis_total_net_output_bytes_per_s | 23405340.339 | 24443441.4 |
| redis_used_cpu_sys_cores | 0.858 | 1.11 |
| redis_used_cpu_sys_main_thread_cores | 0.097 | 0.18 |
| redis_used_cpu_user_cores | 1.112 | 1.21 |
| redis_used_cpu_user_main_thread_cores | 0.616 | 0.66 |
| redis_used_memory | 915.402 | 1384.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,859 records are Completed |
| record_counts | pass | 193,859 records found, 193,859 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,486,938 events applied by workers, 1,486,938 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 163,658 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 739 ms, max 5343 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8289 seq 19: ready 101 ms before start, 101 ms of it with an idle slot in group 282
- [cross_source_blocking] source 4602 seq 20: ready 110 ms before start, 110 ms of it with an idle slot in group 161
- [cross_source_blocking] source 12726 seq 12: ready 102 ms before start, 102 ms of it with an idle slot in group 402
- [cross_source_blocking] source 2006 seq 7: ready 105 ms before start, 105 ms of it with an idle slot in group 58
- [cross_source_blocking] source 6882 seq 20: ready 101 ms before start, 101 ms of it with an idle slot in group 234
- [cross_source_blocking] source 10230 seq 30: ready 106 ms before start, 106 ms of it with an idle slot in group 344
- [cross_source_blocking] source 11746 seq 23: ready 102 ms before start, 102 ms of it with an idle slot in group 380
- [cross_source_blocking] source 5538 seq 10: ready 103 ms before start, 103 ms of it with an idle slot in group 189
- [cross_source_blocking] source 15954 seq 13: ready 100 ms before start, 100 ms of it with an idle slot in group 498
- [cross_source_blocking] source 9485 seq 7: ready 102 ms before start, 102 ms of it with an idle slot in group 328
- [cross_source_blocking] source 13154 seq 14: ready 114 ms before start, 114 ms of it with an idle slot in group 417
- [cross_source_blocking] source 13319 seq 26: ready 101 ms before start, 101 ms of it with an idle slot in group 425
- [cross_source_blocking] source 13309 seq 16: ready 134 ms before start, 134 ms of it with an idle slot in group 425
- [cross_source_blocking] source 1294 seq 52: ready 101 ms before start, 101 ms of it with an idle slot in group 41
- [cross_source_blocking] source 2990 seq 9: ready 106 ms before start, 106 ms of it with an idle slot in group 99
- [cross_source_blocking] source 8558 seq 21: ready 106 ms before start, 106 ms of it with an idle slot in group 293
- [cross_source_blocking] source 13102 seq 21: ready 105 ms before start, 105 ms of it with an idle slot in group 417
- [cross_source_blocking] source 10129 seq 22: ready 133 ms before start, 133 ms of it with an idle slot in group 344
- [cross_source_blocking] source 4813 seq 13: ready 118 ms before start, 116 ms of it with an idle slot in group 168
- [cross_source_blocking] source 3058 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 103
- [cross_source_blocking] source 5234 seq 48: ready 114 ms before start, 114 ms of it with an idle slot in group 181

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15090 | 15090 | 0 | 243.711 | 5701.631 | 8257.535 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6484 | 6484 | 0 | 2260.991 | 5472.255 | 7929.855 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 1966.079 | 5013.503 | 6422.527 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 1998.847 | 4423.679 | 4423.679 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 1753.087 | 4325.375 | 4358.143 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 3055 | 3055 | 0 | 1458.175 | 4227.071 | 6160.383 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1507.327 | 4194.303 | 5439.487 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17981 | 17981 | 0 | 241.663 | 3588.095 | 6324.223 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11940 | 11940 | 0 | 282.623 | 3489.791 | 5505.023 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5767 | 5767 | 0 | 995.327 | 3457.023 | 4915.199 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3098 | 3098 | 0 | 626.687 | 3457.023 | 5046.271 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8821 | 8821 | 0 | 228.351 | 3276.799 | 3981.311 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9092 | 9092 | 0 | 231.423 | 3112.959 | 5734.399 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8089 | 8089 | 0 | 173.055 | 3096.575 | 4980.735 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15220 | 15220 | 0 | 250.879 | 3047.423 | 5406.719 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2930 | 2930 | 0 | 475.135 | 2965.503 | 5013.503 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25400 | 25400 | 0 | 153.599 | 2899.967 | 5308.415 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 22203 | 22203 | 0 | 147.455 | 2785.279 | 4784.127 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 8498 | 8498 | 0 | 205.823 | 2752.511 | 5570.559 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 3087 | 3087 | 0 | 171.007 | 2719.743 | 4915.199 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 13663 | 13663 | 0 | 142.335 | 2703.359 | 4816.895 |
| t-295 | 12 | 1 | 103.993 | 20.658 | 3396 | 0 | 1308 | 1308 | 0 | 105.983 | 2670.591 | 4489.215 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9129 | 9129 | 0 | 197.631 | 2588.671 | 4227.071 |
| t-235 | 54 | 3 | 148.429 | 133.613 | 527 | 0 | 8040 | 8040 | 0 | 146.431 | 2539.519 | 5341.183 |
| t-288 | 6 | 1 | 108.569 | 10.982 | 214 | 0 | 674 | 674 | 0 | 108.031 | 2539.519 | 3096.575 |
