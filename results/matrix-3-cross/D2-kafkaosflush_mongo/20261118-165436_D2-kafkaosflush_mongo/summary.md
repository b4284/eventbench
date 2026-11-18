# D2-kafkaosflush_mongo — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.111s, closing 0.37s, drain 72.95s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814492 | 814492 | 12864.79 | 5286.184 | 7686.4 | 0 | 0 | 0 | 430 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642767 | 46413.081 | 51380.223 | 72810.495 | 78512.127 | 81788.927 | 82378.751 |
| e2e_from_send | 642767 | 46404.637 | 51347.455 | 72810.495 | 78512.127 | 81788.927 | 82378.751 |
| producer_lag | 642767 | 8.452 | 4.731 | 21.119 | 50.687 | 75.839 | 118.655 |
| publish_call | 188890 | 9.856 | 6.719 | 19.279 | 50.463 | 96.255 | 251.007 |
| queue_wait | 642767 | 46131.536 | 51085.311 | 72679.423 | 78381.055 | 81723.391 | 82247.679 |
| broker_dwell | 642767 | 46122.263 | 51085.311 | 72679.423 | 78381.055 | 81657.855 | 82247.679 |
| receive_call | 111987 | 0.152 | 0.064 | 0.127 | 1.824 | 15.663 | 75.519 |
| ctx_load | 642767 | 11.46 | 8.075 | 25.983 | 52.703 | 81.535 | 186.239 |
| process | 642767 | 0.002 | 0.002 | 0.003 | 0.004 | 0.167 | 10.607 |
| ctx_commit | 642767 | 42.986 | 33.983 | 86.527 | 151.807 | 237.055 | 466.943 |
| ack | 641744 | 0.009 | 0.006 | 0.013 | 0.03 | 0.545 | 25.055 |
| service | 642767 | 273.102 | 209.791 | 582.143 | 1039.359 | 1239.039 | 1596.415 |
| e2e_large | 330 | 350.704 | 311.551 | 518.911 | 906.239 | 1138.687 | 1138.687 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5286.184 | 5929.9 |
| applied_total | 175167.714 | 299963 |
| bench_cpu_cores | 6.756 | 7.347 |
| bench_cpu_pct | 28.151 | 30.61 |
| bench_gc_gen0 | 5.041 | 6 |
| bench_gc_gen1 | 2.571 | 4 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 755.461 | 993.2 |
| bench_mem_mb | 2357.653 | 2601.2 |
| bench_threadpool_queue | 4.633 | 62 |
| bench_threadpool_threads | 40.082 | 44 |
| broker_backlog | 274096.122 | 453457 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 167643.776 | 288664 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 441653.163 | 742039 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 230.167 | 247.4 |
| ctr_eb_kafka_mem_mb | 3023.937 | 3150.9 |
| ctr_eb_mongo_cpu_pct | 786.919 | 818.7 |
| ctr_eb_mongo_mem_mb | 3687.445 | 3709.8 |
| ctr_eb_nats_cpu_pct | 0.332 | 0.53 |
| ctr_eb_nats_mem_mb | 165.561 | 168.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.243 | 0.38 |
| ctr_eb_postgres_mem_mb | 4964.447 | 4995.3 |
| ctr_eb_rabbitmq_cpu_pct | 15.905 | 16.35 |
| ctr_eb_rabbitmq_mem_mb | 929.812 | 959.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.119 | 80.18 |
| in_flight | 287748.816 | 472048 |
| mongo_conn_current | 957.082 | 963 |
| mongo_op_command_per_s | 5265.622 | 5715.9 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 852.729 | 1352.7 |
| mongo_op_query_per_s | 10534.8 | 11403.4 |
| mongo_op_update_per_s | 8612.055 | 9219.3 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 42.939 | 103 |
| mongo_txn_committed_per_s | 5261.157 | 5708.7 |
| mongo_wt_cache_mb | 1649.127 | 1697.9 |
| mongo_wt_dirty_mb | 120.794 | 167.7 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12864.79 | 13647.4 |
| produced_total | 462916.531 | 772011 |
| publish_mb_per_s | 44.428 | 68.71 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,577 records are Completed |
| record_counts | pass | 117,577 records found, 117,577 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,492 events applied by workers, 814,492 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12128 | 12128 | 0 | 74448.895 | 82313.215 | 82837.503 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6075 | 6075 | 0 | 73924.607 | 82313.215 | 82313.215 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6334 | 6334 | 0 | 75497.471 | 79691.775 | 79691.775 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 74973.183 | 79691.775 | 79691.775 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 75497.471 | 79691.775 | 79691.775 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9185 | 9185 | 0 | 73924.607 | 79167.487 | 79691.775 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 73924.607 | 78643.199 | 78643.199 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13788 | 13788 | 0 | 70254.591 | 78118.911 | 78118.911 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 72876.031 | 78118.911 | 78118.911 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 72876.031 | 77594.623 | 77594.623 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 73924.607 | 77594.623 | 77594.623 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 73400.319 | 77594.623 | 77594.623 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 73400.319 | 77070.335 | 77070.335 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 72876.031 | 76546.047 | 76546.047 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 73400.319 | 76546.047 | 77070.335 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 72876.031 | 76021.759 | 76021.759 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 72351.743 | 76021.759 | 76021.759 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 71827.455 | 76021.759 | 76021.759 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16762 | 16762 | 0 | 70778.879 | 76021.759 | 76021.759 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8601 | 8601 | 0 | 71303.167 | 76021.759 | 76021.759 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 72351.743 | 75497.471 | 76021.759 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 72351.743 | 75497.471 | 75497.471 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8403 | 8403 | 0 | 71303.167 | 75497.471 | 75497.471 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 70254.591 | 74973.183 | 75497.471 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 7790 | 7790 | 0 | 68157.439 | 74973.183 | 74973.183 |
