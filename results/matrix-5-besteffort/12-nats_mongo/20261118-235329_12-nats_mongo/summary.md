# 12-nats_mongo — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.102s, closing 0.845s, drain 64.432s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814645 | 814645 | 12842.141 | 6045.286 | 8043.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642923 | 40112.685 | 44564.479 | 65699.839 | 71434.239 | 74907.647 | 75825.151 |
| e2e_from_send | 642923 | 40110.191 | 44564.479 | 65699.839 | 71434.239 | 74907.647 | 75825.151 |
| producer_lag | 642923 | 2.496 | 1.213 | 5.639 | 22.895 | 48.159 | 111.103 |
| publish_call | 419730 | 2.87 | 1.635 | 5.579 | 21.455 | 67.583 | 649.727 |
| queue_wait | 642923 | 39856.032 | 44302.335 | 65568.767 | 71303.167 | 74842.111 | 75759.615 |
| broker_dwell | 642923 | 39854.426 | 44302.335 | 65568.767 | 71303.167 | 74842.111 | 75759.615 |
| receive_call | 124346 | 45.586 | 3.007 | 151.295 | 622.079 | 939.519 | 1091.583 |
| ctx_load | 642923 | 11.517 | 7.851 | 26.159 | 55.583 | 84.095 | 196.095 |
| process | 642923 | 0.002 | 0.002 | 0.002 | 0.004 | 0.114 | 65.247 |
| ctx_commit | 642923 | 40.002 | 31.391 | 83.007 | 145.535 | 199.807 | 407.807 |
| ack | 642922 | 0.034 | 0.004 | 0.007 | 0.517 | 4.991 | 59.519 |
| service | 642923 | 254.196 | 196.991 | 554.495 | 913.407 | 1106.943 | 1478.655 |
| e2e_large | 331 | 261.695 | 233.471 | 414.463 | 740.863 | 813.567 | 813.567 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6045.286 | 6642.8 |
| applied_total | 204853.959 | 348811 |
| bench_cpu_cores | 5.126 | 5.851 |
| bench_cpu_pct | 21.361 | 24.38 |
| bench_gc_gen0 | 5.49 | 7 |
| bench_gc_gen1 | 1.429 | 3 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 909.476 | 1177.8 |
| bench_mem_mb | 1462.888 | 1567.4 |
| bench_threadpool_queue | 8.429 | 75 |
| bench_threadpool_threads | 39.959 | 44 |
| broker_backlog | 258080.571 | 422832 |
| broker_backlog_mb | 483.203 | 785.64 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.89 | 2.93 |
| ctr_eb_kafka_mem_mb | 2566.473 | 2840 |
| ctr_eb_mongo_cpu_pct | 925.847 | 968.09 |
| ctr_eb_mongo_mem_mb | 3997.845 | 4097.9 |
| ctr_eb_nats_cpu_pct | 235.477 | 248.31 |
| ctr_eb_nats_mem_mb | 4318.18 | 4962 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 13.537 | 13.7 |
| ctr_eb_postgres_cpu_pct | 0.226 | 0.45 |
| ctr_eb_postgres_mem_mb | 4574.973 | 4641.5 |
| ctr_eb_rabbitmq_cpu_pct | 14.96 | 16.12 |
| ctr_eb_rabbitmq_mem_mb | 803.735 | 823.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.885 | 78.23 |
| in_flight | 263934.347 | 428276 |
| mongo_conn_current | 785.878 | 809 |
| mongo_op_command_per_s | 6052.306 | 6443.5 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 922.661 | 1312.5 |
| mongo_op_query_per_s | 12100.224 | 12849.4 |
| mongo_op_update_per_s | 9915.171 | 10526 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 46.082 | 92 |
| mongo_txn_committed_per_s | 6048.645 | 6448.4 |
| mongo_wt_cache_mb | 1624.369 | 1658.7 |
| mongo_wt_dirty_mb | 98.378 | 142.4 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12842.141 | 13554 |
| produced_total | 468788.306 | 777087 |
| publish_mb_per_s | 44.318 | 71.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,645 events applied by workers, 814,645 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 66846.719 | 76021.759 | 76021.759 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 66846.719 | 75497.471 | 75497.471 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 68157.439 | 72876.031 | 73400.319 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 68157.439 | 72876.031 | 73400.319 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 67633.151 | 72351.743 | 72351.743 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 66846.719 | 72351.743 | 72351.743 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 67633.151 | 71827.455 | 71827.455 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 65535.999 | 71303.167 | 71303.167 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 66584.575 | 71303.167 | 71303.167 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3009 | 3009 | 0 | 66846.719 | 71303.167 | 71303.167 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 63438.847 | 70778.879 | 71303.167 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 65798.143 | 70778.879 | 70778.879 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 64225.279 | 70254.591 | 70254.591 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 66060.287 | 69730.303 | 70254.591 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 65273.855 | 69206.015 | 69206.015 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 65535.999 | 69206.015 | 69730.303 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 65273.855 | 69206.015 | 69206.015 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 65273.855 | 69206.015 | 69206.015 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 63700.991 | 69206.015 | 69730.303 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 64225.279 | 69206.015 | 69206.015 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 61865.983 | 68681.727 | 68681.727 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 64487.423 | 68681.727 | 68681.727 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 63963.135 | 68681.727 | 68681.727 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 64749.567 | 68157.439 | 68681.727 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 65273.855 | 68157.439 | 68681.727 |
