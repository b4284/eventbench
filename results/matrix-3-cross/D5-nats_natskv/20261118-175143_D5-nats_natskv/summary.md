# D5-nats_natskv — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.102s, closing 0.499s, drain 5.307s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814640 | 814640 | 12853.21 | 12004.643 | 20259 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642918 | 3291.942 | 342.271 | 10067.967 | 16023.551 | 19038.207 | 20856.831 |
| e2e_from_send | 642918 | 3290.333 | 339.455 | 10059.775 | 16023.551 | 19038.207 | 20856.831 |
| producer_lag | 642918 | 1.609 | 0.764 | 2.807 | 8.695 | 139.391 | 225.279 |
| publish_call | 539537 | 1.543 | 0.998 | 3.333 | 7.115 | 15.711 | 429.567 |
| queue_wait | 642918 | 3202.764 | 204.671 | 9945.087 | 15900.671 | 18956.287 | 20791.295 |
| broker_dwell | 642918 | 3201.538 | 203.135 | 9945.087 | 15900.671 | 18956.287 | 20791.295 |
| receive_call | 308791 | 49.874 | 5.639 | 146.815 | 515.071 | 879.615 | 1065.983 |
| ctx_load | 642918 | 7.747 | 7.711 | 11.487 | 16.495 | 30.031 | 72.959 |
| process | 642918 | 0.002 | 0.001 | 0.002 | 0.004 | 0.021 | 3.899 |
| ctx_commit | 642918 | 17.548 | 18.751 | 26.687 | 37.151 | 65.343 | 109.567 |
| ack | 642545 | 0.019 | 0.002 | 0.021 | 0.158 | 2.763 | 20.127 |
| service | 642918 | 87.566 | 50.303 | 219.647 | 298.495 | 413.695 | 612.863 |
| e2e_large | 331 | 77.09 | 71.487 | 108.671 | 173.055 | 320.255 | 320.255 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12004.643 | 12566.2 |
| applied_total | 429072.49 | 715910 |
| bench_cpu_cores | 5.601 | 5.944 |
| bench_cpu_pct | 23.336 | 24.77 |
| bench_gc_gen0 | 4.061 | 5 |
| bench_gc_gen1 | 4.041 | 5 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 956.653 | 1223.2 |
| bench_mem_mb | 1404.071 | 1526.6 |
| bench_threadpool_queue | 14.143 | 268 |
| bench_threadpool_threads | 39.571 | 44 |
| broker_backlog | 38705.755 | 60435 |
| broker_backlog_mb | 74.679 | 114.664 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.61 | 2.07 |
| ctr_eb_kafka_mem_mb | 2175.865 | 2190.6 |
| ctr_eb_mongo_cpu_pct | 2.078 | 17.94 |
| ctr_eb_mongo_mem_mb | 4195.514 | 4292.5 |
| ctr_eb_nats_cpu_pct | 432.933 | 447.55 |
| ctr_eb_nats_mem_mb | 5648.733 | 6843.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.582 | 10.7 |
| ctr_eb_postgres_cpu_pct | 0.377 | 1.74 |
| ctr_eb_postgres_mem_mb | 4240.873 | 4260.1 |
| ctr_eb_rabbitmq_cpu_pct | 10.507 | 11.41 |
| ctr_eb_rabbitmq_mem_mb | 755.955 | 765.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.597 | 45.34 |
| in_flight | 39773.429 | 61730 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.21 | 13464.3 |
| produced_total | 468845.918 | 777640 |
| publish_mb_per_s | 44.332 | 71.11 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,640 events applied by workers, 814,640 generated |
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
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 10551.295 | 20578.303 | 20709.375 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 11075.583 | 20316.159 | 20971.519 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 11599.871 | 18612.223 | 18743.295 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 11862.015 | 18219.007 | 18219.007 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 11665.407 | 18087.935 | 18350.079 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 0 | 10354.687 | 17563.647 | 17825.791 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3009 | 3009 | 0 | 10813.439 | 17039.359 | 17170.431 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 10485.759 | 16580.607 | 16711.679 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 11206.655 | 16383.999 | 16449.535 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 9830.399 | 16252.927 | 16449.535 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 7897.087 | 15663.103 | 16383.999 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 9895.935 | 15597.567 | 15925.247 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 10158.079 | 15269.887 | 15466.495 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 8650.751 | 14614.527 | 14745.599 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 9502.719 | 14548.991 | 14680.063 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 8323.071 | 14548.991 | 14876.671 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 9306.111 | 14286.847 | 14483.455 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 8781.823 | 14090.239 | 14221.311 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 9437.183 | 13959.167 | 14090.239 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 8028.159 | 13893.631 | 14155.775 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 9830.399 | 13828.095 | 13959.167 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 9371.647 | 13762.559 | 13828.095 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 8454.143 | 13500.415 | 13762.559 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 8323.071 | 13238.271 | 13369.343 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 7798.783 | 12845.055 | 13303.807 |
