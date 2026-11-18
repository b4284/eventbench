# 05-rabbit_mongo — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 61.203s, closing 2.33s, drain 77.436s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 796381 | 796381 | 12138.018 | 5668.566 | 6779.6 | 0 | 0 | 0 | 420 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 624773 | 46262.043 | 49774.591 | 76873.727 | 83165.183 | 86507.519 | 87949.311 |
| e2e_from_send | 624773 | 45950.109 | 49414.143 | 76546.047 | 82837.503 | 86179.839 | 87687.167 |
| producer_lag | 624773 | 311.914 | 121.343 | 940.031 | 1631.231 | 1966.079 | 2238.463 |
| publish_call | 49384 | 40.497 | 9.911 | 31.375 | 876.031 | 2043.903 | 3049.471 |
| queue_wait | 624773 | 45649.93 | 49086.463 | 76414.975 | 82771.967 | 86114.303 | 87621.631 |
| broker_dwell | 624773 | 45642.44 | 49086.463 | 76414.975 | 82771.967 | 86114.303 | 87621.631 |
| receive_call | 105277 | 13.795 | 0.008 | 55.263 | 185.599 | 211.327 | 242.559 |
| ctx_load | 624773 | 12.576 | 9.743 | 26.431 | 52.095 | 83.327 | 243.455 |
| process | 624773 | 0.003 | 0.002 | 0.003 | 0.005 | 0.213 | 20.079 |
| ctx_commit | 624773 | 45.406 | 38.399 | 84.031 | 146.687 | 212.223 | 379.391 |
| ack | 624767 | 0.005 | 0.003 | 0.004 | 0.009 | 0.374 | 17.743 |
| service | 624773 | 300.22 | 246.527 | 618.495 | 1006.591 | 1232.895 | 2385.919 |
| e2e_large | 320 | 1199.662 | 1077.247 | 2340.863 | 3420.159 | 3450.879 | 3450.879 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5668.566 | 6779.6 |
| applied_total | 191362.64 | 331223 |
| bench_cpu_cores | 3.465 | 4.072 |
| bench_cpu_pct | 14.439 | 16.97 |
| bench_gc_gen0 | 4.74 | 7 |
| bench_gc_gen1 | 1.22 | 3 |
| bench_gc_gen2 | 0.6 | 2 |
| bench_gc_heap_mb | 656.82 | 889.4 |
| bench_mem_mb | 1097.15 | 1160.4 |
| bench_threadpool_queue | 7.56 | 77 |
| bench_threadpool_threads | 38.24 | 41 |
| broker_backlog | 243715.38 | 415348 |
| broker_consumers | 598 | 598 |
| broker_ready | 226402.86 | 398120 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 17312.52 | 19189 |
| ctr_eb_kafka_cpu_pct | 1.846 | 2.25 |
| ctr_eb_kafka_mem_mb | 1749.622 | 1778.6 |
| ctr_eb_mongo_cpu_pct | 909.9 | 999.05 |
| ctr_eb_mongo_mem_mb | 3815.2 | 3989.2 |
| ctr_eb_nats_cpu_pct | 0.72 | 3.79 |
| ctr_eb_nats_mem_mb | 333.112 | 347.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 12.2 | 12.2 |
| ctr_eb_postgres_cpu_pct | 0.43 | 0.77 |
| ctr_eb_postgres_mem_mb | 5032.996 | 5078.3 |
| ctr_eb_rabbitmq_cpu_pct | 408.295 | 487.78 |
| ctr_eb_rabbitmq_mem_mb | 1741.392 | 2615.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.952 | 77.8 |
| in_flight | 273197.5 | 423941 |
| mongo_conn_current | 905.44 | 925 |
| mongo_op_command_per_s | 5602.892 | 6543.5 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 867.514 | 1154.2 |
| mongo_op_query_per_s | 11205.694 | 13142.8 |
| mongo_op_update_per_s | 9174.918 | 10582 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 45.8 | 103 |
| mongo_txn_committed_per_s | 5599.338 | 6564.7 |
| mongo_wt_cache_mb | 1648.052 | 1730.8 |
| mongo_wt_dirty_mb | 119.82 | 175.2 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12138.018 | 34303.5 |
| produced_total | 464560.14 | 754822 |
| publish_mb_per_s | 42.22 | 131.97 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 115,491 records are Completed |
| record_counts | pass | 115,491 records found, 115,491 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 420 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 796,381 events applied by workers, 796,381 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11854 | 11854 | 0 | 78643.199 | 87556.095 | 88080.383 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5946 | 5946 | 0 | 78118.911 | 87031.807 | 87556.095 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6163 | 6163 | 0 | 79167.487 | 84934.655 | 85458.943 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 11988 | 11988 | 0 | 79167.487 | 84934.655 | 85458.943 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6198 | 6198 | 0 | 79167.487 | 84410.367 | 84934.655 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8952 | 8952 | 0 | 77070.335 | 84410.367 | 85458.943 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2988 | 2988 | 0 | 78643.199 | 83886.079 | 84410.367 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2955 | 2955 | 0 | 77070.335 | 83361.791 | 83886.079 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5792 | 5792 | 0 | 76021.759 | 82837.503 | 83361.791 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2954 | 2954 | 0 | 77070.335 | 82837.503 | 83361.791 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13485 | 13485 | 0 | 72876.031 | 82313.215 | 82837.503 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8609 | 8609 | 0 | 76021.759 | 82313.215 | 82837.503 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5770 | 5770 | 0 | 76546.047 | 81788.927 | 82313.215 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2833 | 2833 | 0 | 75497.471 | 81264.639 | 81788.927 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2917 | 2917 | 0 | 75497.471 | 81264.639 | 81788.927 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2815 | 2815 | 0 | 76021.759 | 81264.639 | 81788.927 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5562 | 5562 | 0 | 73924.607 | 81264.639 | 81788.927 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8433 | 8433 | 0 | 73400.319 | 81264.639 | 82313.215 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16377 | 16377 | 0 | 72876.031 | 80740.351 | 81264.639 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2831 | 2831 | 0 | 74973.183 | 80740.351 | 81264.639 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8220 | 8220 | 0 | 73400.319 | 80740.351 | 81264.639 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2841 | 2841 | 0 | 74448.895 | 80216.063 | 81264.639 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2836 | 2836 | 0 | 74448.895 | 79691.775 | 80216.063 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8196 | 8196 | 0 | 72351.743 | 79691.775 | 80216.063 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 15749 | 15749 | 0 | 71303.167 | 79691.775 | 80216.063 |
