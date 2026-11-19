# 07-rabbit_natskv — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.11s, closing 0.914s, drain 4.515s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814357 | 814357 | 12851.767 | 12851.786 | 19103.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642639 | 25.676 | 13.823 | 49.343 | 211.455 | 328.191 | 549.887 |
| e2e_from_send | 642639 | 19.687 | 10.687 | 38.431 | 165.503 | 283.135 | 447.743 |
| producer_lag | 642639 | 5.989 | 2.877 | 10.983 | 72.511 | 163.199 | 219.647 |
| publish_call | 243146 | 7.512 | 4.803 | 9.583 | 37.471 | 413.695 | 1601.535 |
| queue_wait | 642639 | 11.812 | 5.787 | 22.527 | 104.511 | 221.951 | 416.255 |
| broker_dwell | 642639 | 11.783 | 5.779 | 22.415 | 104.319 | 221.823 | 416.255 |
| receive_call | 663661 | 34.403 | 19.567 | 92.799 | 180.607 | 199.295 | 235.007 |
| ctx_load | 642639 | 2.056 | 1.349 | 4.103 | 10.999 | 22.175 | 37.247 |
| process | 642639 | 0.002 | 0.001 | 0.002 | 0.004 | 0.098 | 8.135 |
| ctx_commit | 642639 | 3.803 | 2.569 | 7.795 | 19.919 | 35.231 | 97.343 |
| ack | 625008 | 0.002 | 0.002 | 0.003 | 0.006 | 0.081 | 4.351 |
| service | 642639 | 7.876 | 4.323 | 14.807 | 67.391 | 156.927 | 256.511 |
| e2e_large | 331 | 62.993 | 56.767 | 99.839 | 200.191 | 244.735 | 244.735 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12851.786 | 16243.2 |
| applied_total | 468618.612 | 777504 |
| bench_cpu_cores | 3.212 | 3.616 |
| bench_cpu_pct | 13.385 | 15.07 |
| bench_gc_gen0 | 10.041 | 13 |
| bench_gc_gen1 | 6.551 | 10 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 483.204 | 637.3 |
| bench_mem_mb | 741.065 | 876.3 |
| bench_threadpool_queue | 12.714 | 312 |
| bench_threadpool_threads | 34.551 | 41 |
| broker_backlog | 737.122 | 5348 |
| broker_consumers | 598 | 598 |
| broker_ready | 2.551 | 25 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 734.571 | 5323 |
| ctr_eb_kafka_cpu_pct | 1.597 | 2.05 |
| ctr_eb_kafka_mem_mb | 1659.253 | 1660.3 |
| ctr_eb_mongo_cpu_pct | 1.015 | 1.36 |
| ctr_eb_mongo_mem_mb | 4548.91 | 4554.7 |
| ctr_eb_nats_cpu_pct | 189.659 | 198.04 |
| ctr_eb_nats_mem_mb | 1824.496 | 2782.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 17.29 | 17.3 |
| ctr_eb_postgres_cpu_pct | 0.474 | 0.8 |
| ctr_eb_postgres_mem_mb | 4884.18 | 4885.7 |
| ctr_eb_rabbitmq_cpu_pct | 559.336 | 603.57 |
| ctr_eb_rabbitmq_mem_mb | 1315.759 | 1383.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.19 | 50.44 |
| in_flight | 98.551 | 1041 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12851.767 | 16203.2 |
| produced_total | 468717.163 | 777555 |
| publish_mb_per_s | 44.329 | 71.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,564 records are Completed |
| record_counts | pass | 117,564 records found, 117,564 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,357 events applied by workers, 814,357 generated |
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
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 14.335 | 362.495 | 477.183 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2895 | 2895 | 0 | 14.207 | 319.487 | 528.383 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13786 | 13786 | 0 | 14.207 | 311.295 | 552.959 |
| t-280 | 12 | 1 | 40.999 | 8.025 | 2814 | 0 | 532 | 532 | 0 | 12.543 | 288.767 | 329.727 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9183 | 9183 | 0 | 14.271 | 276.479 | 395.263 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 13.759 | 276.479 | 438.271 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 14.335 | 274.431 | 337.919 |
| t-137 | 10 | 1 | 21.801 | 3.669 | 1569 | 0 | 288 | 288 | 0 | 13.439 | 274.431 | 292.863 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12127 | 12127 | 0 | 14.079 | 272.383 | 430.079 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6272 | 6272 | 0 | 14.271 | 272.383 | 460.799 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16752 | 16752 | 0 | 13.951 | 268.287 | 505.855 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5308 | 5308 | 0 | 13.951 | 266.239 | 448.511 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2765 | 2765 | 0 | 13.759 | 262.143 | 452.607 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 198 | 198 | 0 | 13.439 | 262.143 | 301.055 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 14.207 | 262.143 | 499.711 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 14.079 | 261.119 | 425.983 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 14.079 | 260.095 | 503.807 |
| t-488 | 26 | 1 | 57.68 | 25.54 | 1017 | 0 | 1556 | 1556 | 0 | 14.143 | 260.095 | 385.023 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 8027 | 8027 | 0 | 13.951 | 259.071 | 401.407 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2682 | 2682 | 0 | 13.951 | 258.047 | 385.023 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12250 | 12250 | 0 | 14.207 | 253.951 | 491.519 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8388 | 8388 | 0 | 13.951 | 252.927 | 399.359 |
| t-353 | 77 | 2 | 42.158 | 54.021 | 977 | 0 | 3485 | 3485 | 0 | 13.823 | 252.927 | 405.503 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4970 | 4970 | 0 | 13.887 | 251.903 | 387.071 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2833 | 2833 | 0 | 13.759 | 250.879 | 368.639 |
