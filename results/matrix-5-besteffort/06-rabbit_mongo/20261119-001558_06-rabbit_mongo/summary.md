# 06-rabbit_mongo — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.344s, closing 2.767s, drain 77.607s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 813075 | 813075 | 12827.392 | 5672.143 | 6917.9 | 0 | 0 | 0 | 428 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 641374 | 46114.038 | 50167.807 | 77660.159 | 84148.223 | 88342.527 | 89391.103 |
| e2e_from_send | 641374 | 45539.187 | 49643.519 | 77070.335 | 83558.399 | 87949.311 | 88866.815 |
| producer_lag | 641374 | 574.847 | 430.335 | 1377.279 | 2054.143 | 2344.959 | 2555.903 |
| publish_call | 22118 | 90.602 | 9.831 | 199.295 | 1518.591 | 2328.575 | 2578.431 |
| queue_wait | 641374 | 45233.922 | 49315.839 | 76939.263 | 83427.327 | 87818.239 | 88735.743 |
| broker_dwell | 641374 | 45224.571 | 49283.071 | 76873.727 | 83427.327 | 87818.239 | 88735.743 |
| receive_call | 102951 | 12.07 | 0.008 | 39.711 | 182.271 | 202.495 | 243.839 |
| ctx_load | 641374 | 12.937 | 9.399 | 29.327 | 61.471 | 96.383 | 205.951 |
| process | 641374 | 0.003 | 0.002 | 0.003 | 0.005 | 0.21 | 15.295 |
| ctx_commit | 641374 | 46.079 | 37.055 | 91.839 | 173.439 | 246.015 | 444.415 |
| ack | 641369 | 0.005 | 0.003 | 0.005 | 0.01 | 0.293 | 18.015 |
| service | 641374 | 305.231 | 247.295 | 637.439 | 1069.055 | 1357.823 | 2057.215 |
| e2e_large | 328 | 1595.987 | 1595.391 | 2516.991 | 3166.207 | 3723.263 | 3723.263 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5672.143 | 6884.3 |
| applied_total | 198857.592 | 331999 |
| bench_cpu_cores | 3.525 | 4.037 |
| bench_cpu_pct | 14.685 | 16.82 |
| bench_gc_gen0 | 4.694 | 7 |
| bench_gc_gen1 | 1.245 | 3 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 629.296 | 836.2 |
| bench_mem_mb | 1125.467 | 1216 |
| bench_threadpool_queue | 5.959 | 43 |
| bench_threadpool_threads | 37.184 | 44 |
| broker_backlog | 220269.306 | 379185 |
| broker_consumers | 598 | 598 |
| broker_ready | 204438.918 | 363420 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 15830.388 | 17711 |
| ctr_eb_kafka_cpu_pct | 1.509 | 1.76 |
| ctr_eb_kafka_mem_mb | 1873.382 | 1915.9 |
| ctr_eb_mongo_cpu_pct | 924.112 | 978.52 |
| ctr_eb_mongo_mem_mb | 3785.227 | 3975.9 |
| ctr_eb_nats_cpu_pct | 0.704 | 3.74 |
| ctr_eb_nats_mem_mb | 1374.965 | 1601.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 12.2 | 12.3 |
| ctr_eb_postgres_cpu_pct | 0.993 | 4.49 |
| ctr_eb_postgres_mem_mb | 4839 | 4901.9 |
| ctr_eb_rabbitmq_cpu_pct | 385.536 | 464.06 |
| ctr_eb_rabbitmq_mem_mb | 1963.492 | 2423.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.981 | 77.73 |
| in_flight | 252959.551 | 443856 |
| mongo_conn_current | 828 | 828 |
| mongo_op_command_per_s | 5689.18 | 6832.8 |
| mongo_op_delete_per_s | 36.357 | 436.7 |
| mongo_op_getmore_per_s | 0.031 | 0.5 |
| mongo_op_insert_per_s | 869.865 | 1239.6 |
| mongo_op_query_per_s | 11372.771 | 13631.8 |
| mongo_op_update_per_s | 9333.033 | 11047.9 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 44.551 | 84 |
| mongo_txn_committed_per_s | 5686.586 | 6830 |
| mongo_wt_cache_mb | 1630.114 | 1669.3 |
| mongo_wt_dirty_mb | 102.247 | 152.3 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12827.392 | 37701.3 |
| produced_total | 451817.143 | 775855 |
| publish_mb_per_s | 44.284 | 138.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,415 records are Completed |
| record_counts | pass | 117,415 records found, 117,415 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 428 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 813,075 events applied by workers, 813,075 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12103 | 12103 | 0 | 79167.487 | 89128.959 | 89653.247 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6067 | 6067 | 0 | 79167.487 | 89128.959 | 89653.247 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6327 | 6327 | 0 | 80216.063 | 85983.231 | 86507.519 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6269 | 6269 | 0 | 80216.063 | 85983.231 | 86507.519 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12230 | 12230 | 0 | 79691.775 | 85983.231 | 87031.807 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9171 | 9171 | 0 | 78643.199 | 85458.943 | 85983.231 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13769 | 13769 | 0 | 73924.607 | 83886.079 | 84934.655 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8774 | 8774 | 0 | 76546.047 | 83886.079 | 84410.367 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3056 | 3056 | 0 | 78643.199 | 83886.079 | 84410.367 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3021 | 3021 | 0 | 78118.911 | 83886.079 | 84410.367 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3001 | 3001 | 0 | 78118.911 | 83886.079 | 84410.367 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5915 | 5915 | 0 | 76546.047 | 83361.791 | 83886.079 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5896 | 5896 | 0 | 77594.623 | 82837.503 | 83886.079 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2900 | 2900 | 0 | 76546.047 | 82313.215 | 82837.503 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2985 | 2985 | 0 | 76546.047 | 82313.215 | 82837.503 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2872 | 2872 | 0 | 76021.759 | 82313.215 | 82837.503 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2903 | 2903 | 0 | 76546.047 | 82313.215 | 82837.503 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2892 | 2892 | 0 | 76546.047 | 81788.927 | 82313.215 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5691 | 5691 | 0 | 74973.183 | 81788.927 | 82313.215 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8391 | 8391 | 0 | 74973.183 | 81788.927 | 82837.503 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8594 | 8594 | 0 | 74448.895 | 81788.927 | 82837.503 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2894 | 2894 | 0 | 75497.471 | 81264.639 | 81788.927 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 73924.607 | 80740.351 | 81264.639 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16731 | 16731 | 0 | 73924.607 | 80740.351 | 81788.927 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16079 | 16079 | 0 | 71827.455 | 80740.351 | 81788.927 |
