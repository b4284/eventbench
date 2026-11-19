# 10-kafka_natskv — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.516s, drain 1.68s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814631 | 814631 | 12855.743 | 12854.351 | 22292.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642906 | 23.357 | 18.319 | 45.727 | 70.335 | 102.719 | 171.775 |
| e2e_from_send | 642906 | 21.492 | 16.343 | 43.583 | 67.263 | 98.751 | 169.855 |
| producer_lag | 642906 | 1.865 | 1.643 | 3.519 | 7.543 | 17.119 | 73.279 |
| publish_call | 414478 | 3.826 | 3.365 | 5.011 | 10.623 | 25.919 | 296.447 |
| queue_wait | 642906 | 15.287 | 10.175 | 34.431 | 53.663 | 63.199 | 131.839 |
| broker_dwell | 642906 | 12.156 | 6.943 | 31.183 | 50.463 | 58.143 | 128.895 |
| receive_call | 594304 | 0.021 | 0.011 | 0.023 | 0.083 | 1.725 | 25.567 |
| ctx_load | 642906 | 1.631 | 1.115 | 2.893 | 9.895 | 20.111 | 36.127 |
| process | 642906 | 0.002 | 0.001 | 0.002 | 0.004 | 0.045 | 4.907 |
| ctx_commit | 642906 | 2.941 | 2.133 | 5.379 | 16.295 | 28.927 | 93.311 |
| ack | 642660 | 0.006 | 0.004 | 0.01 | 0.018 | 0.101 | 5.763 |
| service | 642906 | 6.205 | 4.011 | 11.831 | 38.463 | 69.951 | 136.703 |
| e2e_large | 331 | 72.4 | 69.247 | 105.279 | 147.455 | 171.775 | 171.775 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.351 | 13173.8 |
| applied_total | 463049.184 | 771896 |
| bench_cpu_cores | 7.506 | 7.885 |
| bench_cpu_pct | 31.277 | 32.85 |
| bench_gc_gen0 | 11.429 | 21 |
| bench_gc_gen1 | 0.49 | 2 |
| bench_gc_gen2 | 0.469 | 1 |
| bench_gc_heap_mb | 613.42 | 849 |
| bench_mem_mb | 1486.669 | 1681.8 |
| bench_threadpool_queue | 17.408 | 152 |
| bench_threadpool_threads | 37.02 | 44 |
| broker_backlog | 19.02 | 54 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 448780.653 | 765190 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 447961.449 | 764192 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 289.341 | 299.97 |
| ctr_eb_kafka_mem_mb | 3207.173 | 3430.4 |
| ctr_eb_mongo_cpu_pct | 1.176 | 2.44 |
| ctr_eb_mongo_mem_mb | 4582.943 | 4663.5 |
| ctr_eb_nats_cpu_pct | 202.405 | 206.54 |
| ctr_eb_nats_mem_mb | 1720.461 | 2714.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.188 | 16.3 |
| ctr_eb_postgres_cpu_pct | 0.241 | 0.36 |
| ctr_eb_postgres_mem_mb | 4693.712 | 4712.1 |
| ctr_eb_rabbitmq_cpu_pct | 13.045 | 13.91 |
| ctr_eb_rabbitmq_mem_mb | 746.837 | 750.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.633 | 61.33 |
| in_flight | 234.02 | 654 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12855.743 | 13187.2 |
| produced_total | 463283.204 | 772157 |
| publish_mb_per_s | 44.409 | 68.48 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,631 events applied by workers, 814,631 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 58.367 | 145.407 | 172.031 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 55.807 | 133.119 | 133.119 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 60.159 | 117.247 | 129.535 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 57.599 | 116.223 | 147.455 |
| t-101 | 6 | 1 | 31.723 | 2.854 | 198 | 0 | 174 | 174 | 0 | 33.791 | 95.743 | 112.639 |
| t-368 | 36 | 1 | 18.505 | 10.972 | 2666 | 0 | 731 | 731 | 0 | 25.343 | 87.551 | 118.783 |
| t-221 | 70 | 1 | 19.972 | 22.875 | 2834 | 0 | 1495 | 1495 | 0 | 20.863 | 86.527 | 103.423 |
| t-405 | 6 | 1 | 24.442 | 2.424 | 874 | 0 | 176 | 176 | 0 | 35.327 | 86.527 | 103.423 |
| t-125 | 20 | 1 | 64.654 | 22.573 | 1900 | 0 | 1387 | 1387 | 0 | 19.839 | 86.015 | 150.527 |
| t-304 | 19 | 1 | 104.74 | 32.518 | 238 | 0 | 1942 | 1942 | 0 | 17.279 | 85.503 | 107.007 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 15.807 | 84.479 | 123.391 |
| t-076 | 23 | 1 | 17.778 | 6.719 | 4045 | 0 | 461 | 461 | 0 | 29.951 | 83.967 | 95.743 |
| t-283 | 74 | 1 | 27.256 | 32.897 | 240 | 0 | 2143 | 2143 | 0 | 18.047 | 83.455 | 117.247 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 15.871 | 83.455 | 137.215 |
| t-292 | 16 | 1 | 44.698 | 12.892 | 916 | 0 | 794 | 794 | 0 | 23.679 | 82.431 | 95.231 |
| t-445 | 15 | 1 | 19.282 | 4.639 | 896 | 0 | 340 | 340 | 0 | 31.359 | 82.431 | 84.991 |
| t-210 | 71 | 2 | 57.482 | 65.449 | 136 | 0 | 4177 | 4177 | 0 | 17.663 | 81.919 | 134.143 |
| t-128 | 11 | 1 | 101.581 | 18.855 | 260 | 0 | 1148 | 1148 | 0 | 20.479 | 81.407 | 119.295 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 28.415 | 80.895 | 100.351 |
| t-335 | 6 | 1 | 28.977 | 2.726 | 450 | 0 | 179 | 179 | 0 | 35.071 | 80.895 | 88.575 |
| t-021 | 19 | 1 | 16.257 | 5.407 | 3252 | 0 | 346 | 346 | 0 | 33.023 | 80.383 | 98.815 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 15.807 | 80.383 | 119.807 |
| t-152 | 43 | 1 | 37.673 | 26.595 | 882 | 0 | 1701 | 1701 | 0 | 18.815 | 79.359 | 129.023 |
| t-416 | 57 | 1 | 38.884 | 37.248 | 3430 | 0 | 2352 | 2352 | 0 | 18.303 | 79.359 | 107.007 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 15.359 | 79.359 | 123.903 |
