# D4-nats_mongo — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.1s, closing 0.801s, drain 61.906s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814634 | 814634 | 12847.858 | 6299.998 | 7795.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642911 | 37693.311 | 41713.663 | 62586.879 | 68485.119 | 71368.703 | 72548.351 |
| e2e_from_send | 642911 | 37689.795 | 41713.663 | 62586.879 | 68419.583 | 71368.703 | 72548.351 |
| producer_lag | 642911 | 3.518 | 1.805 | 8.647 | 26.863 | 50.591 | 92.223 |
| publish_call | 345415 | 4.145 | 2.297 | 8.575 | 30.495 | 89.151 | 692.735 |
| queue_wait | 642911 | 37447.294 | 41451.519 | 62488.575 | 68354.047 | 71303.167 | 72482.815 |
| broker_dwell | 642911 | 37444.657 | 41451.519 | 62488.575 | 68354.047 | 71303.167 | 72482.815 |
| receive_call | 129200 | 49.268 | 5.563 | 159.999 | 622.079 | 949.247 | 1119.231 |
| ctx_load | 642911 | 10.527 | 7.627 | 23.167 | 44.831 | 66.751 | 128.895 |
| process | 642911 | 0.003 | 0.002 | 0.002 | 0.004 | 0.153 | 19.039 |
| ctx_commit | 642911 | 39.096 | 32.591 | 75.007 | 120.063 | 159.487 | 377.855 |
| ack | 642909 | 0.06 | 0.003 | 0.008 | 1.538 | 7.059 | 42.399 |
| service | 642911 | 242.515 | 189.311 | 527.871 | 821.247 | 958.463 | 1207.295 |
| e2e_large | 331 | 253.769 | 218.111 | 415.743 | 689.663 | 874.495 | 874.495 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6299.998 | 6871.3 |
| applied_total | 209860.292 | 358645 |
| bench_cpu_cores | 4.772 | 5.228 |
| bench_cpu_pct | 19.885 | 21.78 |
| bench_gc_gen0 | 5.667 | 7 |
| bench_gc_gen1 | 1.479 | 3 |
| bench_gc_gen2 | 0.417 | 2 |
| bench_gc_heap_mb | 943.988 | 1359.5 |
| bench_mem_mb | 1460.354 | 1638.9 |
| bench_threadpool_queue | 9.812 | 79 |
| bench_threadpool_threads | 39.417 | 44 |
| broker_backlog | 246466.833 | 396072 |
| broker_backlog_mb | 464.677 | 739.433 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.914 | 2.39 |
| ctr_eb_kafka_mem_mb | 2118.373 | 2353.7 |
| ctr_eb_mongo_cpu_pct | 967.84 | 1013.86 |
| ctr_eb_mongo_mem_mb | 3836.762 | 3946.3 |
| ctr_eb_nats_cpu_pct | 225.703 | 243.42 |
| ctr_eb_nats_mem_mb | 3918.96 | 4899 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 14.756 | 15.4 |
| ctr_eb_postgres_cpu_pct | 0.336 | 0.55 |
| ctr_eb_postgres_mem_mb | 4899.398 | 4979.1 |
| ctr_eb_rabbitmq_cpu_pct | 15.615 | 16.27 |
| ctr_eb_rabbitmq_mem_mb | 868.6 | 889.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.17 | 78.87 |
| in_flight | 252512.188 | 405865 |
| mongo_conn_current | 818.312 | 825 |
| mongo_op_command_per_s | 6281.394 | 6672.7 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 948.667 | 1252 |
| mongo_op_query_per_s | 12561.308 | 13301.6 |
| mongo_op_update_per_s | 10300.302 | 11026.3 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 37.792 | 80 |
| mongo_txn_committed_per_s | 6279.542 | 6682 |
| mongo_wt_cache_mb | 1636.688 | 1704.9 |
| mongo_wt_dirty_mb | 120.01 | 176.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12847.858 | 13227.4 |
| produced_total | 462372.479 | 764510 |
| publish_mb_per_s | 44.4 | 71.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,634 events applied by workers, 814,634 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 63963.135 | 72351.743 | 72876.031 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 63700.991 | 71827.455 | 71827.455 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 65273.855 | 70254.591 | 70254.591 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 65273.855 | 69730.303 | 69730.303 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 65011.711 | 69206.015 | 69206.015 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 63963.135 | 69206.015 | 69206.015 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 63438.847 | 68157.439 | 68157.439 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 63700.991 | 68157.439 | 68157.439 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 60293.119 | 67633.151 | 67633.151 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 62652.415 | 67633.151 | 67633.151 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 62652.415 | 67633.151 | 67633.151 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 63963.135 | 67633.151 | 68157.439 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 62914.559 | 66846.719 | 66846.719 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 62914.559 | 66322.431 | 66322.431 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 62390.271 | 66060.287 | 66060.287 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 62128.127 | 65535.999 | 65798.143 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 62128.127 | 65535.999 | 65798.143 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 61341.695 | 65535.999 | 65535.999 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 61341.695 | 65535.999 | 65535.999 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 60817.407 | 65535.999 | 65535.999 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 61865.983 | 65273.855 | 65535.999 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 60555.263 | 65273.855 | 65798.143 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 62128.127 | 65011.711 | 65273.855 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16118 | 16118 | 0 | 59244.543 | 64749.567 | 64749.567 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 58982.399 | 64487.423 | 64749.567 |
