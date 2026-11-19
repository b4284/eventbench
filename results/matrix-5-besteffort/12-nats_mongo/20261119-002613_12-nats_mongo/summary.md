# 12-nats_mongo — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.106s, closing 0.823s, drain 61.76s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814633 | 814633 | 12851.602 | 5999.378 | 7996.8 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642909 | 39069.949 | 43941.887 | 63504.383 | 69074.943 | 72548.351 | 73531.391 |
| e2e_from_send | 642909 | 39066.975 | 43941.887 | 63504.383 | 69074.943 | 72548.351 | 73531.391 |
| producer_lag | 642909 | 2.972 | 1.284 | 6.303 | 34.207 | 65.183 | 107.455 |
| publish_call | 404660 | 3.054 | 1.713 | 5.647 | 25.983 | 78.463 | 650.751 |
| queue_wait | 642909 | 38816.07 | 43646.975 | 63406.079 | 69009.407 | 72417.279 | 73531.391 |
| broker_dwell | 642909 | 38814.079 | 43646.975 | 63406.079 | 69009.407 | 72417.279 | 73531.391 |
| receive_call | 125177 | 45.873 | 2.991 | 151.807 | 623.615 | 945.663 | 1148.927 |
| ctx_load | 642909 | 11.372 | 7.583 | 25.919 | 56.767 | 89.919 | 311.807 |
| process | 642909 | 0.002 | 0.001 | 0.002 | 0.004 | 0.115 | 37.503 |
| ctx_commit | 642909 | 39.689 | 30.351 | 83.967 | 148.095 | 199.679 | 383.231 |
| ack | 642909 | 0.03 | 0.004 | 0.007 | 0.545 | 4.283 | 61.375 |
| service | 642909 | 250.909 | 190.335 | 559.103 | 922.111 | 1115.135 | 1429.503 |
| e2e_large | 331 | 252.143 | 224.767 | 404.735 | 619.007 | 742.911 | 742.911 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5999.378 | 6808.8 |
| applied_total | 208216.224 | 349708 |
| bench_cpu_cores | 4.968 | 5.519 |
| bench_cpu_pct | 20.701 | 23 |
| bench_gc_gen0 | 5.286 | 7 |
| bench_gc_gen1 | 1.469 | 3 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 887.065 | 1165 |
| bench_mem_mb | 1468.231 | 1581 |
| bench_threadpool_queue | 10.612 | 187 |
| bench_threadpool_threads | 38.327 | 44 |
| broker_backlog | 255048.327 | 420857 |
| broker_backlog_mb | 477.488 | 782.026 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.827 | 2.25 |
| ctr_eb_kafka_mem_mb | 2397.198 | 2641.7 |
| ctr_eb_mongo_cpu_pct | 938.171 | 969.85 |
| ctr_eb_mongo_mem_mb | 4059.663 | 4238.1 |
| ctr_eb_nats_cpu_pct | 231.75 | 245.59 |
| ctr_eb_nats_mem_mb | 4663.986 | 5275.7 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 11.9 | 11.9 |
| ctr_eb_postgres_cpu_pct | 0.744 | 6.45 |
| ctr_eb_postgres_mem_mb | 4288.039 | 4351.6 |
| ctr_eb_rabbitmq_cpu_pct | 15.029 | 15.78 |
| ctr_eb_rabbitmq_mem_mb | 755.514 | 766.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.373 | 77.97 |
| in_flight | 260595.143 | 427803 |
| mongo_conn_current | 786.082 | 837 |
| mongo_op_command_per_s | 6008.294 | 6585.3 |
| mongo_op_delete_per_s | 33.069 | 669.6 |
| mongo_op_getmore_per_s | 0.02 | 0.5 |
| mongo_op_insert_per_s | 910.218 | 1201.5 |
| mongo_op_query_per_s | 12012.208 | 13228.7 |
| mongo_op_update_per_s | 9880.59 | 10682.8 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 41.449 | 99 |
| mongo_txn_committed_per_s | 6005.316 | 6606 |
| mongo_wt_cache_mb | 1625.016 | 1647.7 |
| mongo_wt_dirty_mb | 114.869 | 158.5 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12851.602 | 13241.6 |
| produced_total | 468811.367 | 777511 |
| publish_mb_per_s | 44.328 | 71.24 |

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

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 65011.711 | 73400.319 | 73924.607 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 64225.279 | 72351.743 | 72351.743 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 66060.287 | 70778.879 | 70778.879 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 65798.143 | 70254.591 | 70254.591 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 66060.287 | 70254.591 | 70254.591 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 64749.567 | 69206.015 | 69206.015 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 65535.999 | 69206.015 | 69730.303 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 64749.567 | 69206.015 | 69206.015 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 64225.279 | 68681.727 | 68681.727 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 61603.839 | 68157.439 | 68157.439 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 63438.847 | 67633.151 | 68157.439 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 63700.991 | 67633.151 | 68157.439 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 63176.703 | 67108.863 | 67108.863 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 63176.703 | 67108.863 | 67108.863 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 63700.991 | 67108.863 | 67633.151 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 63438.847 | 67108.863 | 67108.863 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 63438.847 | 66846.719 | 66846.719 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 61603.839 | 66846.719 | 66846.719 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 61603.839 | 66846.719 | 67108.863 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 62914.559 | 66584.575 | 66584.575 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 61865.983 | 66322.431 | 66322.431 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 61865.983 | 66322.431 | 66322.431 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 63438.847 | 66060.287 | 66322.431 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16118 | 16118 | 0 | 60555.263 | 66060.287 | 66322.431 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 61079.551 | 65798.143 | 66060.287 |
