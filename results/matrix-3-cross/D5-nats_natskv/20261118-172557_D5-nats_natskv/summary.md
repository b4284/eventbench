# D5-nats_natskv — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.102s, closing 0.47s, drain 5.504s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814655 | 814655 | 12853.663 | 11961.653 | 19989.2 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642932 | 3433.891 | 427.519 | 10346.495 | 16310.271 | 19333.119 | 21200.895 |
| e2e_from_send | 642932 | 3432.502 | 425.727 | 10346.495 | 16310.271 | 19316.735 | 21200.895 |
| producer_lag | 642932 | 1.39 | 0.749 | 2.759 | 7.111 | 89.855 | 191.743 |
| publish_call | 541720 | 1.522 | 0.988 | 3.311 | 7.091 | 13.751 | 347.647 |
| queue_wait | 642932 | 3343.89 | 282.367 | 10223.615 | 16203.775 | 19202.047 | 21151.743 |
| broker_dwell | 642932 | 3342.96 | 281.343 | 10223.615 | 16203.775 | 19202.047 | 21151.743 |
| receive_call | 306362 | 49.913 | 5.627 | 147.199 | 515.327 | 880.127 | 1044.479 |
| ctx_load | 642932 | 7.805 | 7.819 | 11.615 | 16.575 | 28.287 | 46.207 |
| process | 642932 | 0.002 | 0.001 | 0.002 | 0.004 | 0.022 | 5.075 |
| ctx_commit | 642932 | 17.628 | 18.911 | 26.991 | 37.087 | 58.047 | 91.071 |
| ack | 642616 | 0.018 | 0.002 | 0.023 | 0.16 | 2.593 | 9.519 |
| service | 642932 | 88.611 | 51.295 | 222.335 | 303.103 | 391.167 | 651.775 |
| e2e_large | 331 | 75.559 | 72.127 | 102.271 | 159.871 | 195.199 | 195.199 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11961.653 | 12489.9 |
| applied_total | 427425 | 712874 |
| bench_cpu_cores | 5.523 | 5.755 |
| bench_cpu_pct | 23.014 | 23.98 |
| bench_gc_gen0 | 4.102 | 5 |
| bench_gc_gen1 | 4.082 | 5 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 886.455 | 1151.4 |
| bench_mem_mb | 1259.216 | 1434.7 |
| bench_threadpool_queue | 9.204 | 95 |
| bench_threadpool_threads | 38.449 | 44 |
| broker_backlog | 40346.102 | 63473 |
| broker_backlog_mb | 77.92 | 123.593 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.698 | 2.18 |
| ctr_eb_kafka_mem_mb | 1982.431 | 1990.3 |
| ctr_eb_mongo_cpu_pct | 1.988 | 16.68 |
| ctr_eb_mongo_mem_mb | 4056.355 | 4110.3 |
| ctr_eb_nats_cpu_pct | 434.334 | 448 |
| ctr_eb_nats_mem_mb | 5590.645 | 7104.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 11.757 | 11.8 |
| ctr_eb_postgres_cpu_pct | 0.358 | 1.41 |
| ctr_eb_postgres_mem_mb | 4418.59 | 4458.4 |
| ctr_eb_rabbitmq_cpu_pct | 10.102 | 11 |
| ctr_eb_rabbitmq_mem_mb | 730.888 | 736 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.194 | 45.61 |
| in_flight | 41445.898 | 64794 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.663 | 13158.1 |
| produced_total | 468870.898 | 777668 |
| publish_mb_per_s | 44.331 | 71.1 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,655 events applied by workers, 814,655 generated |
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
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 10878.975 | 20971.519 | 20971.519 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 11141.119 | 20709.375 | 21233.663 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 11730.943 | 18743.295 | 18874.367 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12257 | 12257 | 0 | 11993.087 | 18612.223 | 18874.367 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 11993.087 | 18481.151 | 18612.223 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 10485.759 | 17956.863 | 18219.007 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3009 | 3009 | 0 | 10747.903 | 17039.359 | 17170.431 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 10158.079 | 16908.287 | 17039.359 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 10485.759 | 16908.287 | 16908.287 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 11272.191 | 16777.215 | 16908.287 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 10158.079 | 16383.999 | 16711.679 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 8093.695 | 16121.855 | 16908.287 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 10223.615 | 15663.103 | 15859.711 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 9961.471 | 15138.815 | 15204.351 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 8781.823 | 15138.815 | 15269.887 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 9502.719 | 14942.207 | 15007.743 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 9699.327 | 14876.671 | 14942.207 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 9306.111 | 14548.991 | 14680.063 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 8716.287 | 14483.455 | 14614.527 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 8290.303 | 14352.383 | 14811.135 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 9895.935 | 14090.239 | 14221.311 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 9240.575 | 14090.239 | 14155.775 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 8650.751 | 13959.167 | 14352.383 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 7962.623 | 13565.951 | 13828.095 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 6750.207 | 13238.271 | 13631.487 |
