# 05-rabbit_mongo — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.413s, closing 3.125s, drain 78.325s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 807664 | 807664 | 13340.631 | 5840.408 | 7066.6 | 0 | 0 | 0 | 426 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 635956 | 44391.299 | 47808.511 | 75235.327 | 81788.927 | 85458.943 | 87425.023 |
| e2e_from_send | 635956 | 43579.905 | 46891.007 | 74842.111 | 81330.175 | 85131.263 | 86441.983 |
| producer_lag | 635956 | 811.422 | 407.039 | 2469.887 | 4902.911 | 6303.743 | 7110.655 |
| publish_call | 21481 | 93.901 | 11.463 | 171.391 | 1561.599 | 2691.071 | 3751.935 |
| queue_wait | 635956 | 43280.15 | 46596.095 | 74645.503 | 81199.103 | 85000.191 | 86376.447 |
| broker_dwell | 635956 | 43254.485 | 46563.327 | 74645.503 | 81133.567 | 85000.191 | 85983.231 |
| receive_call | 107160 | 13.803 | 0.008 | 55.647 | 184.703 | 204.415 | 239.231 |
| ctx_load | 635956 | 12.504 | 9.727 | 26.031 | 52.959 | 81.215 | 197.759 |
| process | 635956 | 0.003 | 0.002 | 0.003 | 0.005 | 0.228 | 13.023 |
| ctx_commit | 635956 | 45.32 | 38.239 | 83.647 | 150.655 | 210.431 | 621.055 |
| ack | 635953 | 0.005 | 0.003 | 0.004 | 0.009 | 0.37 | 12.807 |
| service | 635956 | 299.762 | 246.271 | 616.959 | 1031.679 | 1277.951 | 2062.335 |
| e2e_large | 326 | 1895.578 | 1489.919 | 4145.151 | 6017.023 | 6647.807 | 6647.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5840.408 | 7066.6 |
| applied_total | 199455 | 340411 |
| bench_cpu_cores | 3.559 | 4.093 |
| bench_cpu_pct | 14.829 | 17.05 |
| bench_gc_gen0 | 4.714 | 6 |
| bench_gc_gen1 | 1.367 | 3 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 678.247 | 821.7 |
| bench_mem_mb | 1125.814 | 1259.1 |
| bench_threadpool_queue | 4.796 | 46 |
| bench_threadpool_threads | 36.571 | 41 |
| broker_backlog | 221247.714 | 407139 |
| broker_consumers | 598 | 598 |
| broker_ready | 204702.245 | 389475 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 16545.469 | 19658 |
| ctr_eb_kafka_cpu_pct | 2.667 | 3.81 |
| ctr_eb_kafka_mem_mb | 1451.673 | 1504.9 |
| ctr_eb_mongo_cpu_pct | 919.859 | 1001.35 |
| ctr_eb_mongo_mem_mb | 3672.71 | 3707.9 |
| ctr_eb_nats_cpu_pct | 0.631 | 2.4 |
| ctr_eb_nats_mem_mb | 176.076 | 178.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.198 | 16.4 |
| ctr_eb_postgres_cpu_pct | 0.35 | 0.6 |
| ctr_eb_postgres_mem_mb | 5015.708 | 5067.8 |
| ctr_eb_rabbitmq_cpu_pct | 395.777 | 480.15 |
| ctr_eb_rabbitmq_mem_mb | 1854.086 | 2089.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 75.472 | 78.65 |
| in_flight | 249334.592 | 415379 |
| mongo_conn_current | 910.163 | 960 |
| mongo_op_command_per_s | 5776.435 | 6685.6 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 883.329 | 1233.1 |
| mongo_op_query_per_s | 11552.016 | 13307.4 |
| mongo_op_update_per_s | 9467.131 | 10992.8 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 46.776 | 129 |
| mongo_txn_committed_per_s | 5769.806 | 6671.7 |
| mongo_wt_cache_mb | 1646.731 | 1714.5 |
| mongo_wt_dirty_mb | 123.012 | 160.6 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 13340.631 | 37029.1 |
| produced_total | 448789.592 | 755790 |
| publish_mb_per_s | 47.09 | 135.96 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 116,806 records are Completed |
| record_counts | pass | 116,806 records found, 116,806 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 426 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 807,664 events applied by workers, 807,664 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12030 | 12030 | 0 | 76546.047 | 86507.519 | 87556.095 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6023 | 6023 | 0 | 76546.047 | 85983.231 | 86507.519 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6236 | 6236 | 0 | 78118.911 | 83886.079 | 84934.655 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12150 | 12150 | 0 | 77594.623 | 83361.791 | 84410.367 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6288 | 6288 | 0 | 78118.911 | 82837.503 | 83886.079 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9104 | 9104 | 0 | 76021.759 | 82837.503 | 84410.367 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3034 | 3034 | 0 | 76546.047 | 82313.215 | 82837.503 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2984 | 2984 | 0 | 76546.047 | 82313.215 | 83361.791 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13663 | 13663 | 0 | 71303.167 | 81788.927 | 82837.503 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8718 | 8718 | 0 | 74448.895 | 81788.927 | 82313.215 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2999 | 2999 | 0 | 76021.759 | 81788.927 | 82837.503 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5874 | 5874 | 0 | 73924.607 | 80740.351 | 81788.927 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5852 | 5852 | 0 | 74973.183 | 80740.351 | 80740.351 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2872 | 2872 | 0 | 73400.319 | 79691.775 | 80216.063 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2965 | 2965 | 0 | 73400.319 | 79691.775 | 80216.063 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2856 | 2856 | 0 | 73924.607 | 79691.775 | 80216.063 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2888 | 2888 | 0 | 73924.607 | 79691.775 | 80216.063 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5664 | 5664 | 0 | 72351.743 | 79691.775 | 80216.063 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16630 | 16630 | 0 | 71303.167 | 79167.487 | 80216.063 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8348 | 8348 | 0 | 72351.743 | 79167.487 | 80216.063 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8542 | 8542 | 0 | 71303.167 | 79167.487 | 80216.063 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2869 | 2869 | 0 | 72876.031 | 78643.199 | 79167.487 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2878 | 2878 | 0 | 72351.743 | 78643.199 | 79167.487 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 7973 | 7973 | 0 | 68681.727 | 78118.911 | 78643.199 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8307 | 8307 | 0 | 70254.591 | 78118.911 | 78643.199 |
