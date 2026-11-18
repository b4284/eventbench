# D4-nats_mongo — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.104s, closing 0.659s, drain 64.588s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814575 | 814575 | 12854.965 | 6195.104 | 7856.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642851 | 38566.493 | 42696.703 | 64290.815 | 70123.519 | 73269.247 | 73990.143 |
| e2e_from_send | 642851 | 38562.702 | 42696.703 | 64290.815 | 70123.519 | 73269.247 | 73990.143 |
| producer_lag | 642851 | 3.799 | 1.999 | 9.503 | 26.015 | 50.751 | 96.767 |
| publish_call | 330524 | 4.415 | 2.405 | 9.535 | 31.151 | 88.063 | 519.679 |
| queue_wait | 642851 | 38314.206 | 42434.559 | 64159.743 | 69992.447 | 73138.175 | 73990.143 |
| broker_dwell | 642851 | 38311.349 | 42434.559 | 64159.743 | 69992.447 | 73138.175 | 73990.143 |
| receive_call | 128162 | 48.879 | 5.919 | 154.879 | 623.615 | 947.199 | 1094.655 |
| ctx_load | 642851 | 10.66 | 7.867 | 23.359 | 44.447 | 65.119 | 136.319 |
| process | 642851 | 0.003 | 0.002 | 0.003 | 0.004 | 0.15 | 16.607 |
| ctx_commit | 642851 | 40.061 | 33.055 | 76.607 | 125.631 | 214.911 | 430.847 |
| ack | 642840 | 0.071 | 0.003 | 0.008 | 1.7 | 9.599 | 41.247 |
| service | 642851 | 248.508 | 193.279 | 542.207 | 848.383 | 1004.543 | 1369.087 |
| e2e_large | 331 | 249.894 | 221.311 | 391.935 | 621.055 | 745.471 | 745.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6195.104 | 6768 |
| applied_total | 212068.551 | 359281 |
| bench_cpu_cores | 4.554 | 4.946 |
| bench_cpu_pct | 18.976 | 20.61 |
| bench_gc_gen0 | 5.388 | 7 |
| bench_gc_gen1 | 1.531 | 3 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 948.973 | 1194.3 |
| bench_mem_mb | 1467.339 | 1557.9 |
| bench_threadpool_queue | 16.796 | 142 |
| bench_threadpool_threads | 39.469 | 44 |
| broker_backlog | 250781.143 | 415052 |
| broker_backlog_mb | 471.389 | 779.665 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.834 | 2.3 |
| ctr_eb_kafka_mem_mb | 2357.42 | 2588.3 |
| ctr_eb_mongo_cpu_pct | 968.475 | 994.85 |
| ctr_eb_mongo_mem_mb | 3905.473 | 4004.5 |
| ctr_eb_nats_cpu_pct | 233.406 | 244.71 |
| ctr_eb_nats_mem_mb | 4182.127 | 5249 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 10.757 | 10.8 |
| ctr_eb_postgres_cpu_pct | 0.418 | 1.53 |
| ctr_eb_postgres_mem_mb | 4429.622 | 4528 |
| ctr_eb_rabbitmq_cpu_pct | 14.492 | 15.8 |
| ctr_eb_rabbitmq_mem_mb | 821.429 | 862.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.532 | 78.9 |
| in_flight | 256732.878 | 418369 |
| mongo_conn_current | 793.306 | 817 |
| mongo_op_command_per_s | 6195.967 | 6516.1 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 933.702 | 1213 |
| mongo_op_query_per_s | 12398.151 | 13022.9 |
| mongo_op_update_per_s | 10164.051 | 10719.7 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 46.592 | 100 |
| mongo_txn_committed_per_s | 6194.037 | 6511.7 |
| mongo_wt_cache_mb | 1651.367 | 1709.2 |
| mongo_wt_dirty_mb | 130.763 | 185.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12854.965 | 13145.6 |
| produced_total | 468801.429 | 777650 |
| publish_mb_per_s | 44.339 | 71.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,590 records are Completed |
| record_counts | pass | 117,590 records found, 117,590 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,575 events applied by workers, 814,575 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 66060.287 | 73924.607 | 74448.895 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 65798.143 | 73924.607 | 73924.607 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 66846.719 | 71827.455 | 71827.455 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 66846.719 | 71303.167 | 71303.167 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12255 | 12255 | 0 | 66846.719 | 71303.167 | 71827.455 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9184 | 9184 | 0 | 65273.855 | 70778.879 | 70778.879 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13791 | 13791 | 0 | 61865.983 | 69730.303 | 69730.303 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 64749.567 | 69730.303 | 69730.303 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 64749.567 | 69730.303 | 69730.303 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 66060.287 | 69730.303 | 69730.303 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 65273.855 | 69730.303 | 69730.303 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 65535.999 | 69730.303 | 69730.303 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 64749.567 | 68157.439 | 68681.727 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 63963.135 | 67633.151 | 67633.151 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 64487.423 | 67633.151 | 68157.439 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 64225.279 | 67633.151 | 68157.439 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 63176.703 | 67633.151 | 68157.439 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16762 | 16762 | 0 | 62390.271 | 67633.151 | 68157.439 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 62390.271 | 67633.151 | 68157.439 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 63963.135 | 67108.863 | 67108.863 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 63963.135 | 67108.863 | 67108.863 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 63963.135 | 66846.719 | 66846.719 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 60555.263 | 66584.575 | 66846.719 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16116 | 16116 | 0 | 60817.407 | 66584.575 | 66846.719 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 62914.559 | 66584.575 | 66846.719 |
