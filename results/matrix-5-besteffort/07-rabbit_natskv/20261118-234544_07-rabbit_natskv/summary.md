# 07-rabbit_natskv — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.128s, closing 1.797s, drain 3.771s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814457 | 814457 | 12848.692 | 12854.406 | 19792 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642729 | 28.389 | 12.807 | 58.911 | 244.351 | 439.295 | 787.455 |
| e2e_from_send | 642729 | 21.419 | 9.935 | 44.447 | 192.639 | 434.175 | 784.895 |
| producer_lag | 642729 | 6.97 | 2.675 | 11.503 | 102.591 | 178.303 | 227.583 |
| publish_call | 257780 | 7.106 | 4.439 | 8.639 | 37.311 | 445.183 | 1462.271 |
| queue_wait | 642729 | 13.636 | 5.275 | 27.055 | 149.375 | 373.759 | 762.367 |
| broker_dwell | 642729 | 13.59 | 5.267 | 26.863 | 149.375 | 373.759 | 762.367 |
| receive_call | 656858 | 34.715 | 19.599 | 93.951 | 181.119 | 199.295 | 256.639 |
| ctx_load | 642729 | 1.993 | 1.315 | 3.849 | 10.943 | 25.199 | 59.199 |
| process | 642729 | 0.002 | 0.001 | 0.002 | 0.004 | 0.084 | 3.703 |
| ctx_commit | 642729 | 3.702 | 2.523 | 7.559 | 19.247 | 33.663 | 110.271 |
| ack | 629020 | 0.002 | 0.002 | 0.003 | 0.006 | 0.089 | 3.145 |
| service | 642729 | 7.783 | 4.183 | 15.007 | 68.671 | 131.199 | 211.711 |
| e2e_large | 331 | 63.359 | 55.263 | 100.095 | 226.303 | 245.631 | 245.631 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.406 | 14868.9 |
| applied_total | 468467.755 | 777248 |
| bench_cpu_cores | 3.402 | 3.8 |
| bench_cpu_pct | 14.176 | 15.83 |
| bench_gc_gen0 | 9.796 | 15 |
| bench_gc_gen1 | 4.755 | 7 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 495.045 | 657.7 |
| bench_mem_mb | 719.978 | 801.1 |
| bench_threadpool_queue | 11.286 | 152 |
| bench_threadpool_threads | 34.755 | 39 |
| broker_backlog | 1053.041 | 6716 |
| broker_consumers | 598 | 598 |
| broker_ready | 12.98 | 106 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 1040.061 | 6610 |
| ctr_eb_kafka_cpu_pct | 1.607 | 2.06 |
| ctr_eb_kafka_mem_mb | 1314.573 | 1315.1 |
| ctr_eb_mongo_cpu_pct | 2.376 | 17.75 |
| ctr_eb_mongo_mem_mb | 4488.443 | 4489.9 |
| ctr_eb_nats_cpu_pct | 200.144 | 212.08 |
| ctr_eb_nats_mem_mb | 2006.378 | 2954.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 12.8 | 12.8 |
| ctr_eb_postgres_cpu_pct | 0.22 | 0.46 |
| ctr_eb_postgres_mem_mb | 4904.663 | 4904.7 |
| ctr_eb_rabbitmq_cpu_pct | 566.218 | 606.13 |
| ctr_eb_rabbitmq_mem_mb | 1326.927 | 1395.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 48.877 | 52.79 |
| in_flight | 141.633 | 1782 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12848.692 | 15187.8 |
| produced_total | 468609.388 | 777248 |
| publish_mb_per_s | 44.325 | 71.22 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,580 records are Completed |
| record_counts | pass | 117,580 records found, 117,580 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,457 events applied by workers, 814,457 generated |
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
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2613 | 2613 | 0 | 12.927 | 442.367 | 561.151 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6334 | 6334 | 0 | 13.055 | 407.551 | 688.127 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 12.927 | 374.783 | 528.383 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 12.927 | 372.735 | 536.575 |
| t-269 | 24 | 1 | 71.069 | 29.94 | 531 | 0 | 1888 | 1888 | 0 | 12.735 | 358.399 | 395.263 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 12.991 | 348.159 | 528.383 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12248 | 12248 | 0 | 13.119 | 348.159 | 790.527 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 12.799 | 346.111 | 598.015 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6075 | 6075 | 0 | 12.991 | 344.063 | 643.071 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13788 | 13788 | 0 | 13.183 | 337.919 | 598.015 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12125 | 12125 | 0 | 12.799 | 319.487 | 733.183 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9185 | 9185 | 0 | 13.247 | 319.487 | 569.343 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 13.183 | 319.487 | 532.479 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2988 | 2988 | 0 | 12.991 | 315.391 | 577.535 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 12.991 | 307.199 | 638.975 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 13.247 | 303.103 | 589.823 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8033 | 8033 | 0 | 13.119 | 301.055 | 573.439 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 12.799 | 301.055 | 520.191 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2738 | 2738 | 0 | 12.607 | 299.007 | 423.935 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8403 | 8403 | 0 | 12.991 | 299.007 | 585.727 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 2563 | 2563 | 0 | 12.671 | 296.959 | 614.399 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8785 | 8785 | 0 | 13.183 | 290.815 | 602.111 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7673 | 7673 | 0 | 12.927 | 288.767 | 430.079 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 2660 | 2660 | 0 | 13.439 | 284.671 | 382.975 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5307 | 5307 | 0 | 12.863 | 282.623 | 593.919 |
