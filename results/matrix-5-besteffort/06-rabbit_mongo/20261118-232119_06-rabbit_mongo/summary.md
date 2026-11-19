# 06-rabbit_mongo — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.615s, closing 1.168s, drain 79.385s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 803297 | 803297 | 12784.614 | 5503.071 | 7283.3 | 0 | 0 | 0 | 422 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 631629 | 47911.879 | 52232.191 | 78643.199 | 84869.119 | 88014.847 | 89456.639 |
| e2e_from_send | 631629 | 47407.076 | 51740.671 | 78315.519 | 84672.511 | 87818.239 | 88932.351 |
| producer_lag | 631629 | 504.836 | 144.383 | 1640.447 | 2529.279 | 2994.175 | 3371.007 |
| publish_call | 38582 | 50.691 | 11.135 | 56.927 | 1088.511 | 2418.687 | 3751.935 |
| queue_wait | 631629 | 47100.292 | 51412.991 | 78184.447 | 84541.439 | 87687.167 | 88866.815 |
| broker_dwell | 631629 | 47077.825 | 51380.223 | 78184.447 | 84541.439 | 87687.167 | 88866.815 |
| receive_call | 102791 | 12.156 | 0.008 | 41.439 | 181.119 | 200.191 | 242.943 |
| ctx_load | 631629 | 13.069 | 9.495 | 29.279 | 61.919 | 96.895 | 186.879 |
| process | 631629 | 0.003 | 0.001 | 0.002 | 0.004 | 0.191 | 20.015 |
| ctx_commit | 631629 | 46.002 | 36.831 | 92.095 | 172.031 | 244.735 | 450.815 |
| ack | 631628 | 0.004 | 0.003 | 0.004 | 0.009 | 0.284 | 11.543 |
| service | 631629 | 306.827 | 245.375 | 645.631 | 1091.583 | 1351.679 | 1928.191 |
| e2e_large | 322 | 1417.088 | 1110.015 | 3090.431 | 4147.199 | 4497.407 | 4497.407 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5503.071 | 7283.3 |
| applied_total | 186701.633 | 317397 |
| bench_cpu_cores | 3.413 | 4.179 |
| bench_cpu_pct | 14.221 | 17.41 |
| bench_gc_gen0 | 4.429 | 6 |
| bench_gc_gen1 | 1.204 | 3 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 658.798 | 867.9 |
| bench_mem_mb | 1114 | 1268.2 |
| bench_threadpool_queue | 6.571 | 120 |
| bench_threadpool_threads | 39.429 | 44 |
| broker_backlog | 233516.796 | 425049 |
| broker_consumers | 598 | 598 |
| broker_ready | 216911.694 | 407970 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 16605.102 | 17968 |
| ctr_eb_kafka_cpu_pct | 1.82 | 2.32 |
| ctr_eb_kafka_mem_mb | 1702.859 | 1744.3 |
| ctr_eb_mongo_cpu_pct | 886.252 | 980.1 |
| ctr_eb_mongo_mem_mb | 3831.963 | 3984.5 |
| ctr_eb_nats_cpu_pct | 0.634 | 5.3 |
| ctr_eb_nats_mem_mb | 2878.947 | 3224.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 17.686 | 17.8 |
| ctr_eb_postgres_cpu_pct | 0.501 | 0.71 |
| ctr_eb_postgres_mem_mb | 4918.857 | 4969.6 |
| ctr_eb_rabbitmq_cpu_pct | 413.143 | 494.5 |
| ctr_eb_rabbitmq_mem_mb | 1821.573 | 2156.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.242 | 76.66 |
| in_flight | 269117.735 | 447421 |
| mongo_conn_current | 786.837 | 794 |
| mongo_op_command_per_s | 5442.539 | 6523 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 852.822 | 1166.8 |
| mongo_op_query_per_s | 10880.308 | 13102.4 |
| mongo_op_update_per_s | 8935.884 | 10677.1 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 40.49 | 63 |
| mongo_txn_committed_per_s | 5441.133 | 6500.7 |
| mongo_wt_cache_mb | 1628.171 | 1705.3 |
| mongo_wt_dirty_mb | 105.061 | 155.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12784.614 | 39575.4 |
| produced_total | 455819.367 | 759892 |
| publish_mb_per_s | 44.7 | 160.27 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 116,278 records are Completed |
| record_counts | pass | 116,278 records found, 116,278 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 422 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 803,297 events applied by workers, 803,297 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11964 | 11964 | 0 | 80216.063 | 89128.959 | 89653.247 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5988 | 5988 | 0 | 79167.487 | 88604.671 | 88604.671 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6258 | 6258 | 0 | 80740.351 | 85983.231 | 85983.231 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6205 | 6205 | 0 | 80740.351 | 85983.231 | 85983.231 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12084 | 12084 | 0 | 80740.351 | 85983.231 | 86507.519 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9043 | 9043 | 0 | 79691.775 | 85458.943 | 85983.231 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13585 | 13585 | 0 | 75497.471 | 84410.367 | 84410.367 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5849 | 5849 | 0 | 78643.199 | 84410.367 | 84410.367 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3020 | 3020 | 0 | 80216.063 | 84410.367 | 84934.655 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2979 | 2979 | 0 | 79167.487 | 84410.367 | 84410.367 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2974 | 2974 | 0 | 80216.063 | 84410.367 | 84934.655 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8671 | 8671 | 0 | 77594.623 | 83886.079 | 84410.367 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5822 | 5822 | 0 | 79167.487 | 83361.791 | 83361.791 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2859 | 2859 | 0 | 78118.911 | 82837.503 | 83361.791 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16532 | 16532 | 0 | 75497.471 | 82837.503 | 82837.503 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8503 | 8503 | 0 | 75497.471 | 82837.503 | 82837.503 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2951 | 2951 | 0 | 77594.623 | 82313.215 | 82313.215 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2842 | 2842 | 0 | 77594.623 | 82313.215 | 82313.215 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2869 | 2869 | 0 | 77594.623 | 82313.215 | 82313.215 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2856 | 2856 | 0 | 77594.623 | 81788.927 | 82313.215 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2863 | 2863 | 0 | 78118.911 | 81788.927 | 82313.215 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 15873 | 15873 | 0 | 73400.319 | 81788.927 | 82313.215 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8285 | 8285 | 0 | 76021.759 | 81788.927 | 82313.215 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5628 | 5628 | 0 | 75497.471 | 81264.639 | 81788.927 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8261 | 8261 | 0 | 75497.471 | 81264.639 | 81264.639 |
