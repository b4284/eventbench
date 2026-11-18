# D8-kafkaosflush_natskv — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.492s, drain 1.684s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814626 | 814626 | 12855.769 | 12855.416 | 21134.8 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642900 | 24.664 | 19.055 | 48.447 | 83.583 | 122.559 | 225.663 |
| e2e_from_send | 642900 | 22.817 | 17.167 | 46.399 | 80.831 | 120.319 | 221.183 |
| producer_lag | 642900 | 1.847 | 1.634 | 3.485 | 7.675 | 15.855 | 37.855 |
| publish_call | 418100 | 3.78 | 3.359 | 4.831 | 10.031 | 27.583 | 296.703 |
| queue_wait | 642900 | 15.513 | 10.311 | 34.911 | 54.271 | 77.311 | 152.063 |
| broker_dwell | 642900 | 12.935 | 7.595 | 32.191 | 51.551 | 74.687 | 149.887 |
| receive_call | 592732 | 0.022 | 0.012 | 0.025 | 0.091 | 1.49 | 24.303 |
| ctx_load | 642900 | 1.849 | 1.253 | 3.359 | 11.103 | 26.063 | 51.615 |
| process | 642900 | 0.002 | 0.001 | 0.002 | 0.004 | 0.046 | 5.407 |
| ctx_commit | 642900 | 3.416 | 2.425 | 6.347 | 19.183 | 36.703 | 149.887 |
| ack | 642699 | 0.006 | 0.004 | 0.01 | 0.022 | 0.101 | 5.675 |
| service | 642900 | 7.304 | 4.567 | 13.975 | 50.207 | 88.319 | 161.663 |
| e2e_large | 331 | 76.038 | 72.063 | 112.767 | 173.567 | 225.663 | 225.663 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12855.416 | 13638.8 |
| applied_total | 463010.102 | 771932 |
| bench_cpu_cores | 7.631 | 8.048 |
| bench_cpu_pct | 31.797 | 33.53 |
| bench_gc_gen0 | 11.592 | 19 |
| bench_gc_gen1 | 2.041 | 6 |
| bench_gc_gen2 | 0.347 | 2 |
| bench_gc_heap_mb | 747.18 | 957.6 |
| bench_mem_mb | 1574.688 | 1720.1 |
| bench_threadpool_queue | 22.408 | 184 |
| bench_threadpool_threads | 40.245 | 44 |
| broker_backlog | 21.571 | 150 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 448729.347 | 768967 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 447817.939 | 768176 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 294.388 | 304.65 |
| ctr_eb_kafka_mem_mb | 2618.914 | 3119.5 |
| ctr_eb_mongo_cpu_pct | 1.231 | 2.25 |
| ctr_eb_mongo_mem_mb | 2978.569 | 3125.2 |
| ctr_eb_nats_cpu_pct | 219.464 | 244.43 |
| ctr_eb_nats_mem_mb | 2355.908 | 4956.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 12.398 | 12.4 |
| ctr_eb_postgres_cpu_pct | 0.241 | 0.46 |
| ctr_eb_postgres_mem_mb | 4479.147 | 4480.4 |
| ctr_eb_rabbitmq_cpu_pct | 14.557 | 15.93 |
| ctr_eb_rabbitmq_mem_mb | 778.688 | 907.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 59.215 | 62.47 |
| in_flight | 248.306 | 941 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12855.769 | 13200.4 |
| produced_total | 463258.408 | 772127 |
| publish_mb_per_s | 44.411 | 68.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,626 events applied by workers, 814,626 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 60.671 | 174.079 | 226.303 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 61.439 | 161.791 | 164.863 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 61.439 | 151.551 | 151.551 |
| t-432 | 9 | 1 | 58.559 | 8.62 | 747 | 0 | 529 | 529 | 0 | 29.439 | 120.319 | 149.503 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 15.999 | 116.223 | 188.415 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 17.151 | 114.175 | 168.959 |
| t-032 | 52 | 1 | 28.87 | 24.41 | 1041 | 0 | 1620 | 1620 | 0 | 20.223 | 114.175 | 139.263 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 59.135 | 111.615 | 116.223 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 16.255 | 107.007 | 147.455 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 16.895 | 105.983 | 169.983 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 16.191 | 105.983 | 167.935 |
| t-080 | 56 | 1 | 32.985 | 31.08 | 822 | 0 | 2165 | 2165 | 0 | 18.559 | 104.959 | 155.647 |
| t-148 | 6 | 1 | 62.881 | 5.748 | 715 | 0 | 407 | 407 | 0 | 30.975 | 101.887 | 113.151 |
| t-294 | 7 | 1 | 107.858 | 12.447 | 667 | 0 | 765 | 765 | 0 | 23.935 | 101.887 | 139.263 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2766 | 2766 | 0 | 16.895 | 101.375 | 151.551 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 30.847 | 100.863 | 110.079 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2537 | 2537 | 0 | 16.895 | 99.327 | 143.359 |
| t-363 | 19 | 1 | 93.142 | 30.223 | 639 | 0 | 1855 | 1855 | 0 | 18.431 | 98.815 | 187.391 |
| t-428 | 37 | 1 | 60.011 | 37.101 | 159 | 0 | 2290 | 2290 | 0 | 17.151 | 98.303 | 147.455 |
| t-372 | 65 | 1 | 32.959 | 35.645 | 875 | 0 | 2355 | 2355 | 0 | 18.943 | 97.791 | 127.999 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 16.127 | 97.279 | 153.599 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 16.255 | 96.255 | 145.407 |
| t-159 | 6 | 1 | 46.704 | 4.469 | 295 | 0 | 288 | 288 | 0 | 35.071 | 95.743 | 103.935 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4616 | 4616 | 0 | 17.919 | 95.743 | 129.023 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16766 | 16766 | 0 | 16.383 | 95.743 | 146.431 |
