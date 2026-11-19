# 13-nats_natskv — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.489s, drain 5.425s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814648 | 814648 | 12853 | 12044.141 | 20796.1 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642926 | 3498.747 | 424.447 | 10395.647 | 16154.623 | 19267.583 | 21495.807 |
| e2e_from_send | 642926 | 3496.96 | 421.887 | 10387.455 | 16154.623 | 19267.583 | 21495.807 |
| producer_lag | 642926 | 1.787 | 0.768 | 2.847 | 10.335 | 154.751 | 256.639 |
| publish_call | 532931 | 1.571 | 1.002 | 3.347 | 7.299 | 18.527 | 449.279 |
| queue_wait | 642926 | 3409.172 | 280.575 | 10264.575 | 16048.127 | 19169.279 | 21446.655 |
| broker_dwell | 642926 | 3408.292 | 279.807 | 10264.575 | 16048.127 | 19169.279 | 21446.655 |
| receive_call | 303916 | 50.24 | 5.655 | 148.351 | 516.607 | 881.663 | 1072.127 |
| ctx_load | 642926 | 7.787 | 7.759 | 11.575 | 16.831 | 28.831 | 97.855 |
| process | 642926 | 0.002 | 0.001 | 0.002 | 0.004 | 0.023 | 8.063 |
| ctx_commit | 642926 | 17.434 | 18.655 | 26.575 | 36.639 | 60.159 | 111.487 |
| ack | 642612 | 0.02 | 0.002 | 0.022 | 0.151 | 2.741 | 24.431 |
| service | 642926 | 87.79 | 50.399 | 220.415 | 298.751 | 377.343 | 614.399 |
| e2e_large | 331 | 76.734 | 71.295 | 110.719 | 169.727 | 232.575 | 232.575 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12044.141 | 12814 |
| applied_total | 426476.551 | 714764 |
| bench_cpu_cores | 5.538 | 5.893 |
| bench_cpu_pct | 23.073 | 24.55 |
| bench_gc_gen0 | 4.184 | 5 |
| bench_gc_gen1 | 4.061 | 5 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 842.298 | 1130.2 |
| bench_mem_mb | 1266.737 | 1454.4 |
| bench_threadpool_queue | 8.143 | 165 |
| bench_threadpool_threads | 38.041 | 44 |
| broker_backlog | 41423.98 | 61363 |
| broker_backlog_mb | 79.689 | 116.386 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.816 | 4.03 |
| ctr_eb_kafka_mem_mb | 2003.516 | 2009.4 |
| ctr_eb_mongo_cpu_pct | 2.075 | 17.84 |
| ctr_eb_mongo_mem_mb | 4572.137 | 4612 |
| ctr_eb_nats_cpu_pct | 433.442 | 445.76 |
| ctr_eb_nats_mem_mb | 5451.573 | 6935.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 13.757 | 13.8 |
| ctr_eb_postgres_cpu_pct | 0.449 | 0.59 |
| ctr_eb_postgres_mem_mb | 4630.757 | 4634.9 |
| ctr_eb_rabbitmq_cpu_pct | 11.271 | 12.03 |
| ctr_eb_rabbitmq_mem_mb | 673.339 | 678.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.159 | 46.24 |
| in_flight | 42382.816 | 62904 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853 | 13916.1 |
| produced_total | 468859.367 | 777668 |
| publish_mb_per_s | 44.331 | 71.08 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,648 events applied by workers, 814,648 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 11337.727 | 20840.447 | 21495.807 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 10747.903 | 20709.375 | 20840.447 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 11993.087 | 18612.223 | 18874.367 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 12255.231 | 18481.151 | 18612.223 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12257 | 12257 | 0 | 11862.015 | 18219.007 | 18612.223 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 10682.367 | 17825.791 | 18219.007 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3009 | 3009 | 0 | 11010.047 | 17039.359 | 17301.503 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 11010.047 | 16777.215 | 16908.287 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 10485.759 | 16711.679 | 17039.359 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 11534.335 | 16711.679 | 16908.287 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 8323.071 | 16252.927 | 17039.359 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 10420.223 | 16056.319 | 16252.927 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 10420.223 | 15335.423 | 15466.495 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 10158.079 | 15073.279 | 15204.351 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 8978.431 | 14680.063 | 15138.815 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 8978.431 | 14614.527 | 14811.135 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 9633.791 | 14548.991 | 14811.135 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 10158.079 | 14483.455 | 14614.527 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 9633.791 | 14352.383 | 14483.455 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 8290.303 | 14221.311 | 14745.599 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 10092.543 | 14024.703 | 14090.239 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 9699.327 | 13959.167 | 14155.775 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 8716.287 | 13762.559 | 13959.167 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16118 | 16118 | 0 | 7569.407 | 13238.271 | 14024.703 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 8290.303 | 13172.735 | 13631.487 |
