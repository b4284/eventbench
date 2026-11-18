# 03-pgmq_mongo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.231s, closing 0.701s, drain 97.632s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814407 | 814407 | 12874.278 | 4748.065 | 6228.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642686 | 56948.565 | 60653.567 | 92405.759 | 100401.151 | 101449.727 | 101908.479 |
| e2e_from_send | 642686 | 56928.359 | 60653.567 | 92405.759 | 100335.615 | 101449.727 | 101711.871 |
| producer_lag | 642686 | 20.194 | 12.807 | 43.967 | 126.655 | 290.303 | 579.071 |
| publish_call | 87476 | 21.919 | 16.087 | 45.279 | 98.815 | 239.999 | 574.463 |
| queue_wait | 642686 | 56614.329 | 60325.887 | 92209.151 | 100270.079 | 101318.655 | 101711.871 |
| broker_dwell | 642686 | 56599.878 | 60325.887 | 92209.151 | 100204.543 | 101318.655 | 101711.871 |
| receive_call | 108364 | 20.572 | 14.383 | 41.919 | 75.711 | 1103.871 | 1169.407 |
| ctx_load | 642686 | 10.429 | 6.635 | 24.223 | 51.807 | 79.359 | 143.999 |
| process | 642686 | 0.003 | 0.002 | 0.003 | 0.005 | 0.152 | 17.855 |
| ctx_commit | 642686 | 38.963 | 31.519 | 76.415 | 139.263 | 194.943 | 423.935 |
| ack | 642686 | 12.497 | 8.943 | 25.471 | 58.015 | 101.375 | 359.935 |
| service | 642686 | 314.036 | 256.639 | 635.391 | 1090.559 | 1291.263 | 1607.679 |
| e2e_large | 331 | 541.858 | 502.271 | 807.423 | 1096.703 | 1282.047 | 1282.047 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4748.065 | 5373.9 |
| applied_total | 162276.265 | 275941 |
| bench_cpu_cores | 3.351 | 3.844 |
| bench_cpu_pct | 13.964 | 16.02 |
| bench_gc_gen0 | 4.469 | 6 |
| bench_gc_gen1 | 4.224 | 5 |
| bench_gc_gen2 | 0.449 | 2 |
| bench_gc_heap_mb | 1253.129 | 1674.2 |
| bench_mem_mb | 1517.818 | 1653.4 |
| bench_threadpool_queue | 1.469 | 8 |
| bench_threadpool_threads | 36.755 | 44 |
| broker_backlog | 296183.653 | 491594 |
| broker_oldest_age_s | 29.327 | 49 |
| broker_visible | 291446.224 | 487618 |
| ctr_eb_kafka_cpu_pct | 2.004 | 2.74 |
| ctr_eb_kafka_mem_mb | 1909.555 | 2000.8 |
| ctr_eb_mongo_cpu_pct | 684.79 | 709.72 |
| ctr_eb_mongo_mem_mb | 3081.865 | 3272.1 |
| ctr_eb_nats_cpu_pct | 1.176 | 7.64 |
| ctr_eb_nats_mem_mb | 2451.761 | 2933.5 |
| ctr_eb_pgbouncer_cpu_pct | 43.293 | 44.97 |
| ctr_eb_pgbouncer_mem_mb | 14.416 | 15.3 |
| ctr_eb_postgres_cpu_pct | 722.779 | 762.48 |
| ctr_eb_postgres_mem_mb | 4638.8 | 4758.8 |
| ctr_eb_rabbitmq_cpu_pct | 15.648 | 16.29 |
| ctr_eb_rabbitmq_mem_mb | 705.016 | 711.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.173 | 83.17 |
| in_flight | 306138.694 | 501706 |
| mongo_conn_current | 772 | 772 |
| mongo_op_command_per_s | 4762.002 | 5034.3 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 765.582 | 1120.7 |
| mongo_op_query_per_s | 9513.935 | 10027.2 |
| mongo_op_update_per_s | 7775.992 | 8179.8 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 36.02 | 110 |
| mongo_txn_committed_per_s | 4760.251 | 4988.1 |
| mongo_wt_cache_mb | 1637.492 | 1695.7 |
| mongo_wt_dirty_mb | 118.804 | 148.4 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12874.278 | 13923.6 |
| produced_total | 468414.959 | 777647 |
| publish_mb_per_s | 44.37 | 72.83 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,571 records are Completed |
| record_counts | pass | 117,571 records found, 117,571 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,407 events applied by workers, 814,407 generated |
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
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 94371.839 | 101711.871 | 101711.871 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12125 | 12125 | 0 | 93847.551 | 101711.871 | 101711.871 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12249 | 12249 | 0 | 94896.127 | 101711.871 | 102236.159 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6075 | 6075 | 0 | 93323.263 | 101187.583 | 101187.583 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 93847.551 | 101187.583 | 101187.583 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 92798.975 | 100663.295 | 101187.583 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9181 | 9181 | 0 | 92274.687 | 100139.007 | 100139.007 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 91750.399 | 100139.007 | 100139.007 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 91226.111 | 99614.719 | 100139.007 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8787 | 8787 | 0 | 89653.247 | 98566.143 | 98566.143 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 89128.959 | 98041.855 | 98041.855 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 90177.535 | 98041.855 | 98041.855 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 90701.823 | 98041.855 | 98566.143 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 88604.671 | 97517.567 | 98041.855 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 88604.671 | 97517.567 | 97517.567 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 89128.959 | 96993.279 | 97517.567 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 88604.671 | 96468.991 | 96993.279 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2898 | 2898 | 0 | 88080.383 | 96468.991 | 96993.279 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13787 | 13787 | 0 | 87031.807 | 95944.703 | 96468.991 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 87556.095 | 95944.703 | 96468.991 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16758 | 16758 | 0 | 86507.519 | 95944.703 | 96468.991 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 87031.807 | 95944.703 | 95944.703 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8601 | 8601 | 0 | 87031.807 | 95944.703 | 95944.703 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 85983.231 | 95420.415 | 95420.415 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8388 | 8388 | 0 | 85458.943 | 95420.415 | 95420.415 |
