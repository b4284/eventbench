# D2-kafkaosflush_mongo — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.111s, closing 0.532s, drain 77.069s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814618 | 814618 | 12866.1 | 5005.78 | 7439.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642896 | 49358.953 | 54394.879 | 76677.119 | 82509.823 | 85852.159 | 87293.951 |
| e2e_from_send | 642896 | 49348.909 | 54362.111 | 76611.583 | 82509.823 | 85852.159 | 87293.951 |
| producer_lag | 642896 | 10.064 | 4.935 | 26.559 | 64.959 | 95.423 | 142.847 |
| publish_call | 179993 | 10.361 | 6.483 | 21.199 | 60.671 | 111.743 | 288.767 |
| queue_wait | 642896 | 49066.596 | 54099.967 | 76546.047 | 82378.751 | 85786.623 | 87228.415 |
| broker_dwell | 642896 | 49055.906 | 54099.967 | 76480.511 | 82378.751 | 85786.623 | 87228.415 |
| receive_call | 108803 | 0.152 | 0.066 | 0.122 | 1.827 | 15.535 | 63.359 |
| ctx_load | 642896 | 11.618 | 8.019 | 26.575 | 56.703 | 87.167 | 187.519 |
| process | 642896 | 0.002 | 0.002 | 0.002 | 0.004 | 0.177 | 11.535 |
| ctx_commit | 642896 | 44.322 | 34.719 | 90.111 | 160.767 | 222.719 | 403.455 |
| ack | 641842 | 0.01 | 0.006 | 0.013 | 0.031 | 0.636 | 27.567 |
| service | 642896 | 282.328 | 217.855 | 596.479 | 1103.871 | 1321.983 | 1659.903 |
| e2e_large | 331 | 395.857 | 329.727 | 593.407 | 1597.439 | 1793.023 | 1793.023 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5005.78 | 5773.4 |
| applied_total | 166096.673 | 284119 |
| bench_cpu_cores | 6.689 | 7.325 |
| bench_cpu_pct | 27.871 | 30.52 |
| bench_gc_gen0 | 4.918 | 7 |
| bench_gc_gen1 | 2.592 | 4 |
| bench_gc_gen2 | 0.551 | 2 |
| bench_gc_heap_mb | 735.2 | 978.7 |
| bench_mem_mb | 2352.929 | 2666.6 |
| bench_threadpool_queue | 2.755 | 35 |
| bench_threadpool_threads | 42.143 | 44 |
| broker_backlog | 283186.204 | 473241 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 159518.143 | 277073 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 442627.551 | 750236 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 232.439 | 251.33 |
| ctr_eb_kafka_mem_mb | 3086.553 | 3253 |
| ctr_eb_mongo_cpu_pct | 789.286 | 852.18 |
| ctr_eb_mongo_mem_mb | 3843.249 | 3892.9 |
| ctr_eb_nats_cpu_pct | 0.4 | 0.6 |
| ctr_eb_nats_mem_mb | 287.931 | 295.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 11.971 | 12.1 |
| ctr_eb_postgres_cpu_pct | 0.369 | 1.39 |
| ctr_eb_postgres_mem_mb | 4807.359 | 4867.6 |
| ctr_eb_rabbitmq_cpu_pct | 13.929 | 14.89 |
| ctr_eb_rabbitmq_mem_mb | 853.247 | 889.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.804 | 80.06 |
| in_flight | 296279.286 | 487372 |
| mongo_conn_current | 867.224 | 890 |
| mongo_op_command_per_s | 5012.488 | 5498.5 |
| mongo_op_delete_per_s | 14.602 | 238.5 |
| mongo_op_getmore_per_s | 0.024 | 0.4 |
| mongo_op_insert_per_s | 819.278 | 1381.4 |
| mongo_op_query_per_s | 10020.169 | 10975.2 |
| mongo_op_update_per_s | 8190.676 | 8941.5 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 45.878 | 88 |
| mongo_txn_committed_per_s | 5010.82 | 5483 |
| mongo_wt_cache_mb | 1640.329 | 1704.5 |
| mongo_wt_dirty_mb | 116.241 | 162 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12866.1 | 14230.1 |
| produced_total | 462375.959 | 771491 |
| publish_mb_per_s | 44.427 | 68.54 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,594 records are Completed |
| record_counts | pass | 117,594 records found, 117,594 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,618 events applied by workers, 814,618 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 78118.911 | 87556.095 | 87556.095 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 77594.623 | 85983.231 | 85983.231 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 79167.487 | 83886.079 | 83886.079 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 79167.487 | 83886.079 | 83886.079 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 79167.487 | 83886.079 | 83886.079 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 77594.623 | 83361.791 | 83361.791 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 78643.199 | 82837.503 | 82837.503 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 78118.911 | 82837.503 | 82837.503 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 76546.047 | 82313.215 | 82313.215 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 77070.335 | 82313.215 | 82313.215 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13791 | 13791 | 0 | 73924.607 | 81788.927 | 82313.215 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 76546.047 | 81788.927 | 81788.927 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 77070.335 | 81264.639 | 81264.639 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 74973.183 | 80740.351 | 80740.351 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 76021.759 | 80216.063 | 80216.063 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 76546.047 | 80216.063 | 80740.351 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 75497.471 | 80216.063 | 80216.063 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 76021.759 | 79691.775 | 80216.063 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 76021.759 | 79691.775 | 79691.775 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 76021.759 | 79691.775 | 79691.775 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 74448.895 | 79691.775 | 80216.063 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2898 | 2898 | 0 | 76021.759 | 79167.487 | 79167.487 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 71827.455 | 78643.199 | 79167.487 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 73924.607 | 78643.199 | 78643.199 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16117 | 16117 | 0 | 72876.031 | 78643.199 | 78643.199 |
