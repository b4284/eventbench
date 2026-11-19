# 03-pgmq_mongo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), BEST-EFFORT w=majority j=false (needs writeConcernMajorityJournalDefault=false), load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.317s, closing 0.588s, drain 96.225s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814416 | 814416 | 12848.316 | 4283.724 | 6561.1 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642701 | 60538.092 | 65241.087 | 93978.623 | 100597.759 | 101384.191 | 101842.943 |
| e2e_from_send | 642701 | 60529.62 | 65241.087 | 93978.623 | 100597.759 | 101318.655 | 101842.943 |
| producer_lag | 642701 | 8.491 | 2.457 | 13.999 | 146.431 | 253.311 | 411.135 |
| publish_call | 301797 | 5.339 | 3.353 | 10.223 | 31.519 | 159.615 | 417.535 |
| queue_wait | 642701 | 60213.712 | 64913.407 | 93847.551 | 100466.687 | 101253.119 | 101777.407 |
| broker_dwell | 642701 | 60210.614 | 64913.407 | 93847.551 | 100466.687 | 101253.119 | 101777.407 |
| receive_call | 104945 | 7.275 | 5.039 | 13.031 | 32.959 | 95.039 | 1140.735 |
| ctx_load | 642701 | 13.534 | 8.335 | 33.119 | 72.575 | 114.495 | 237.951 |
| process | 642701 | 0.002 | 0.002 | 0.003 | 0.004 | 0.141 | 30.735 |
| ctx_commit | 642701 | 44.2 | 33.247 | 93.119 | 191.359 | 279.295 | 611.839 |
| ack | 642701 | 4.772 | 3.477 | 9.319 | 19.727 | 116.991 | 209.535 |
| service | 642701 | 315.951 | 248.575 | 649.215 | 1256.447 | 1588.223 | 2619.391 |
| e2e_large | 331 | 816.746 | 618.495 | 1669.119 | 3010.559 | 3430.399 | 3430.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4283.724 | 4906.8 |
| applied_total | 145876.918 | 248055 |
| bench_cpu_cores | 3.781 | 4.388 |
| bench_cpu_pct | 15.753 | 18.28 |
| bench_gc_gen0 | 4.163 | 6 |
| bench_gc_gen1 | 4 | 5 |
| bench_gc_gen2 | 0.469 | 2 |
| bench_gc_heap_mb | 1283.655 | 1634.3 |
| bench_mem_mb | 1483.945 | 1704.6 |
| bench_threadpool_queue | 1.612 | 13 |
| bench_threadpool_threads | 41.49 | 44 |
| broker_backlog | 313392.265 | 517319 |
| broker_oldest_age_s | 30.204 | 50 |
| broker_visible | 308723.061 | 513245 |
| ctr_eb_kafka_cpu_pct | 1.99 | 2.4 |
| ctr_eb_kafka_mem_mb | 1587.073 | 1620.3 |
| ctr_eb_mongo_cpu_pct | 635.968 | 671.7 |
| ctr_eb_mongo_mem_mb | 3481.531 | 3644 |
| ctr_eb_nats_cpu_pct | 0.387 | 0.52 |
| ctr_eb_nats_mem_mb | 5828.176 | 5944.3 |
| ctr_eb_pgbouncer_cpu_pct | 46.738 | 48.96 |
| ctr_eb_pgbouncer_mem_mb | 13.163 | 14.6 |
| ctr_eb_postgres_cpu_pct | 772.605 | 823.95 |
| ctr_eb_postgres_mem_mb | 4474.249 | 4554.7 |
| ctr_eb_rabbitmq_cpu_pct | 16.333 | 17.15 |
| ctr_eb_rabbitmq_mem_mb | 537.349 | 548.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 83.447 | 87.58 |
| in_flight | 322877.02 | 529445 |
| mongo_conn_current | 830.224 | 859 |
| mongo_op_command_per_s | 4313.351 | 4762.1 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 718.343 | 1139.5 |
| mongo_op_query_per_s | 8627.955 | 9692.7 |
| mongo_op_update_per_s | 7030.633 | 7779.5 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 41.429 | 86 |
| mongo_txn_committed_per_s | 4312.812 | 4779.4 |
| mongo_wt_cache_mb | 1639.055 | 1691.5 |
| mongo_wt_dirty_mb | 110.778 | 169.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12848.316 | 15339.7 |
| produced_total | 468753.939 | 777500 |
| publish_mb_per_s | 44.327 | 71.16 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,569 records are Completed |
| record_counts | pass | 117,569 records found, 117,569 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,416 events applied by workers, 814,416 generated |
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
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 96993.279 | 101711.871 | 102236.159 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12128 | 12128 | 0 | 95944.703 | 101711.871 | 101711.871 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 96468.991 | 101711.871 | 101711.871 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12251 | 12251 | 0 | 96993.279 | 101711.871 | 101711.871 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 96468.991 | 101187.583 | 101711.871 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9181 | 9181 | 0 | 94896.127 | 100139.007 | 100663.295 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 94896.127 | 100139.007 | 100139.007 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 94371.839 | 100139.007 | 100139.007 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 93847.551 | 100139.007 | 100663.295 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 92798.975 | 99090.431 | 99090.431 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 93323.263 | 98566.143 | 98566.143 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 93323.263 | 98566.143 | 99090.431 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2988 | 2988 | 0 | 92274.687 | 98041.855 | 98566.143 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 92798.975 | 98041.855 | 98566.143 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 91226.111 | 98041.855 | 98041.855 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 91750.399 | 97517.567 | 97517.567 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 92274.687 | 97517.567 | 98041.855 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13790 | 13790 | 0 | 90701.823 | 96993.279 | 97517.567 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16756 | 16756 | 0 | 90701.823 | 96993.279 | 96993.279 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 90701.823 | 96993.279 | 97517.567 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 89653.247 | 96468.991 | 96468.991 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 90177.535 | 96468.991 | 96468.991 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8387 | 8387 | 0 | 88604.671 | 95944.703 | 95944.703 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 90177.535 | 95944.703 | 95944.703 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5308 | 5308 | 0 | 88080.383 | 95420.415 | 95420.415 |
