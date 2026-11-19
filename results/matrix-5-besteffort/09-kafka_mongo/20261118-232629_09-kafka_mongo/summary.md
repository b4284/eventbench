# 09-kafka_mongo — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.124s, closing 0.497s, drain 73.964s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814608 | 814608 | 12861.527 | 4906.184 | 7841.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642887 | 48698.956 | 53805.055 | 74842.111 | 80478.207 | 83755.007 | 84606.975 |
| e2e_from_send | 642887 | 48691.103 | 53805.055 | 74842.111 | 80478.207 | 83755.007 | 84606.975 |
| producer_lag | 642887 | 7.844 | 4.087 | 20.255 | 48.575 | 76.671 | 137.599 |
| publish_call | 203544 | 9.072 | 5.803 | 18.431 | 48.959 | 111.103 | 291.327 |
| queue_wait | 642887 | 48411.409 | 53542.911 | 74711.039 | 80347.135 | 83623.935 | 84541.439 |
| broker_dwell | 642887 | 48403.056 | 53542.911 | 74711.039 | 80347.135 | 83623.935 | 84541.439 |
| receive_call | 107667 | 0.136 | 0.067 | 0.12 | 1.222 | 14.415 | 77.823 |
| ctx_load | 642887 | 12.098 | 7.887 | 29.455 | 65.535 | 102.719 | 228.607 |
| process | 642887 | 0.002 | 0.002 | 0.003 | 0.004 | 0.151 | 9.863 |
| ctx_commit | 642887 | 43.388 | 32.271 | 93.119 | 184.063 | 257.919 | 484.351 |
| ack | 641816 | 0.01 | 0.008 | 0.013 | 0.031 | 0.352 | 23.983 |
| service | 642887 | 279.701 | 211.455 | 589.823 | 1165.311 | 1439.743 | 1918.975 |
| e2e_large | 331 | 406.306 | 334.591 | 665.599 | 1520.639 | 1791.999 | 1791.999 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4906.184 | 5876.3 |
| applied_total | 166616.653 | 280646 |
| bench_cpu_cores | 6.783 | 7.366 |
| bench_cpu_pct | 28.262 | 30.69 |
| bench_gc_gen0 | 4.898 | 6 |
| bench_gc_gen1 | 2.735 | 4 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 660.669 | 941.3 |
| bench_mem_mb | 2322.198 | 2629.7 |
| bench_threadpool_queue | 3.204 | 38 |
| bench_threadpool_threads | 38.51 | 44 |
| broker_backlog | 282443.429 | 482739 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 159828.857 | 276465 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 442224.837 | 759176 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 235.108 | 254.31 |
| ctr_eb_kafka_mem_mb | 2808.108 | 2891.5 |
| ctr_eb_mongo_cpu_pct | 768.471 | 805.97 |
| ctr_eb_mongo_mem_mb | 4053.033 | 4334.4 |
| ctr_eb_nats_cpu_pct | 0.712 | 6.79 |
| ctr_eb_nats_mem_mb | 2587.608 | 3044.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.906 | 17.1 |
| ctr_eb_postgres_cpu_pct | 0.273 | 0.6 |
| ctr_eb_postgres_mem_mb | 4791.61 | 4854.2 |
| ctr_eb_rabbitmq_cpu_pct | 15.344 | 16.59 |
| ctr_eb_rabbitmq_mem_mb | 750.002 | 758.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.418 | 80.06 |
| in_flight | 295849.306 | 490750 |
| mongo_conn_current | 853.898 | 876 |
| mongo_op_command_per_s | 4907.831 | 5621.4 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 800.094 | 1385.4 |
| mongo_op_query_per_s | 9815.014 | 11274 |
| mongo_op_update_per_s | 8034.71 | 9299.3 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 45.612 | 78 |
| mongo_txn_committed_per_s | 4906.373 | 5601.2 |
| mongo_wt_cache_mb | 1631.796 | 1702.5 |
| mongo_wt_dirty_mb | 99.629 | 157.6 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12861.527 | 13355.3 |
| produced_total | 462465.959 | 771396 |
| publish_mb_per_s | 44.419 | 68.64 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,592 records are Completed |
| record_counts | pass | 117,592 records found, 117,592 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,608 events applied by workers, 814,608 generated |
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
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 76546.047 | 84934.655 | 84934.655 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 76021.759 | 83886.079 | 84410.367 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12253 | 12253 | 0 | 77594.623 | 82313.215 | 82313.215 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 76546.047 | 81788.927 | 81788.927 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 77070.335 | 81264.639 | 81264.639 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 76021.759 | 81264.639 | 81264.639 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13791 | 13791 | 0 | 72351.743 | 80216.063 | 80216.063 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 74973.183 | 80216.063 | 80216.063 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 75497.471 | 80216.063 | 80216.063 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 76021.759 | 80216.063 | 80216.063 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 76021.759 | 80216.063 | 80216.063 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 75497.471 | 79691.775 | 80216.063 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 74448.895 | 78643.199 | 78643.199 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 74973.183 | 78643.199 | 79167.487 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 74973.183 | 78643.199 | 79167.487 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 73400.319 | 78643.199 | 78643.199 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16765 | 16765 | 0 | 72876.031 | 78643.199 | 78643.199 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 74448.895 | 78118.911 | 78118.911 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 74973.183 | 78118.911 | 78118.911 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 73924.607 | 77594.623 | 77594.623 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 71303.167 | 77594.623 | 77594.623 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 73400.319 | 77594.623 | 77594.623 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 72876.031 | 77594.623 | 77594.623 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 72351.743 | 77070.335 | 77594.623 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16118 | 16118 | 0 | 71827.455 | 77070.335 | 77070.335 |
