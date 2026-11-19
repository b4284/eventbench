# 13-nats_natskv — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.486s, drain 5.206s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814653 | 814653 | 12853.259 | 12073.29 | 20578.5 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642929 | 3303.273 | 356.607 | 9904.127 | 15761.407 | 18841.599 | 20758.527 |
| e2e_from_send | 642929 | 3302.038 | 355.327 | 9904.127 | 15761.407 | 18841.599 | 20758.527 |
| producer_lag | 642929 | 1.238 | 0.748 | 2.739 | 6.415 | 44.063 | 145.407 |
| publish_call | 540826 | 1.55 | 1.01 | 3.377 | 7.255 | 14.527 | 381.951 |
| queue_wait | 642929 | 3215.61 | 218.239 | 9789.439 | 15654.911 | 18743.295 | 20692.991 |
| broker_dwell | 642929 | 3214.066 | 216.447 | 9781.247 | 15654.911 | 18743.295 | 20692.991 |
| receive_call | 309461 | 49.842 | 5.763 | 146.175 | 514.047 | 877.055 | 1071.103 |
| ctx_load | 642929 | 7.658 | 7.643 | 11.399 | 16.039 | 30.543 | 59.775 |
| process | 642929 | 0.001 | 0.001 | 0.002 | 0.004 | 0.022 | 4.915 |
| ctx_commit | 642929 | 17.35 | 18.591 | 26.367 | 35.519 | 57.855 | 91.455 |
| ack | 642513 | 0.019 | 0.002 | 0.023 | 0.161 | 2.767 | 8.567 |
| service | 642929 | 86.426 | 48.863 | 217.599 | 296.959 | 392.191 | 610.303 |
| e2e_large | 331 | 76.08 | 72.511 | 104.383 | 173.311 | 247.295 | 247.295 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12073.29 | 12688.8 |
| applied_total | 428670.286 | 716834 |
| bench_cpu_cores | 5.667 | 6.043 |
| bench_cpu_pct | 23.612 | 25.18 |
| bench_gc_gen0 | 4.122 | 5 |
| bench_gc_gen1 | 4.102 | 5 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 867.773 | 1190.7 |
| bench_mem_mb | 1283.151 | 1461.5 |
| bench_threadpool_queue | 20.449 | 365 |
| bench_threadpool_threads | 37.939 | 44 |
| broker_backlog | 39239.122 | 58674 |
| broker_backlog_mb | 75.953 | 116.976 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.688 | 2.08 |
| ctr_eb_kafka_mem_mb | 2310.459 | 2340.3 |
| ctr_eb_mongo_cpu_pct | 2.052 | 18.47 |
| ctr_eb_mongo_mem_mb | 4408.151 | 4510.9 |
| ctr_eb_nats_cpu_pct | 433.83 | 452.54 |
| ctr_eb_nats_mem_mb | 5633.837 | 6952.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 13.353 | 13.4 |
| ctr_eb_postgres_cpu_pct | 0.201 | 0.3 |
| ctr_eb_postgres_mem_mb | 4510.265 | 4524.2 |
| ctr_eb_rabbitmq_cpu_pct | 10.8 | 11.43 |
| ctr_eb_rabbitmq_mem_mb | 767.014 | 775.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.732 | 45.84 |
| in_flight | 40199.918 | 60774 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.259 | 13128.6 |
| produced_total | 468870.204 | 777608 |
| publish_mb_per_s | 44.333 | 71.2 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,653 events applied by workers, 814,653 generated |
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
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 10223.615 | 20578.303 | 20709.375 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 10878.975 | 20185.087 | 20840.447 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 11730.943 | 18087.935 | 18219.007 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 11272.191 | 17825.791 | 18087.935 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12257 | 12257 | 0 | 11534.335 | 17825.791 | 18219.007 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 10158.079 | 17432.575 | 17694.719 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 9961.471 | 16515.071 | 16711.679 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 10420.223 | 16449.535 | 16580.607 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 10420.223 | 16383.999 | 16515.071 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 10878.975 | 16187.391 | 16318.463 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 7864.319 | 15728.639 | 16383.999 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 9699.327 | 15728.639 | 16121.855 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 9961.471 | 15007.743 | 15204.351 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 9306.111 | 14417.919 | 14483.455 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 8585.215 | 14352.383 | 14483.455 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 9109.503 | 14286.847 | 14483.455 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 8257.535 | 14286.847 | 14680.063 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 8912.895 | 14024.703 | 14155.775 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 9371.647 | 14024.703 | 14155.775 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 9830.399 | 13697.023 | 13762.559 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 9240.575 | 13631.487 | 13762.559 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 7897.087 | 13565.951 | 14024.703 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 8224.767 | 13041.663 | 13238.271 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 8159.231 | 13041.663 | 13107.199 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 7733.247 | 12976.127 | 13369.343 |
