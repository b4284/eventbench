# D8-kafkaosflush_natskv — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.104s, closing 0.489s, drain 1.682s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814636 | 814636 | 12855.073 | 12854.394 | 21175.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642912 | 23.957 | 18.415 | 47.199 | 80.767 | 134.271 | 255.231 |
| e2e_from_send | 642912 | 22.147 | 16.527 | 45.247 | 78.207 | 130.943 | 252.415 |
| producer_lag | 642912 | 1.81 | 1.608 | 3.441 | 7.175 | 14.983 | 39.967 |
| publish_call | 421515 | 3.723 | 3.283 | 4.831 | 9.591 | 28.431 | 276.479 |
| queue_wait | 642912 | 15.44 | 10.191 | 34.751 | 54.111 | 80.127 | 199.295 |
| broker_dwell | 642912 | 12.139 | 6.759 | 31.343 | 50.687 | 76.095 | 194.047 |
| receive_call | 592902 | 0.021 | 0.011 | 0.023 | 0.083 | 1.527 | 34.975 |
| ctx_load | 642912 | 1.718 | 1.125 | 3.063 | 10.903 | 28.479 | 67.071 |
| process | 642912 | 0.002 | 0.001 | 0.002 | 0.004 | 0.04 | 5.543 |
| ctx_commit | 642912 | 3.127 | 2.185 | 5.851 | 18.031 | 35.007 | 141.183 |
| ack | 642675 | 0.006 | 0.004 | 0.009 | 0.018 | 0.082 | 4.479 |
| service | 642912 | 6.708 | 4.111 | 12.583 | 47.487 | 90.879 | 222.335 |
| e2e_large | 331 | 72.245 | 70.335 | 102.783 | 161.151 | 219.775 | 219.775 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.394 | 13669.9 |
| applied_total | 462723.082 | 771652 |
| bench_cpu_cores | 7.224 | 7.903 |
| bench_cpu_pct | 30.101 | 32.93 |
| bench_gc_gen0 | 12.041 | 18 |
| bench_gc_gen1 | 1.776 | 6 |
| bench_gc_gen2 | 0.367 | 2 |
| bench_gc_heap_mb | 722.065 | 985.2 |
| bench_mem_mb | 1601.104 | 1788.1 |
| bench_threadpool_queue | 17.714 | 132 |
| bench_threadpool_threads | 40.347 | 44 |
| broker_backlog | 29.347 | 184 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 448848.878 | 765636 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 448023.878 | 764809 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 294.675 | 308.02 |
| ctr_eb_kafka_mem_mb | 2852.984 | 3271.1 |
| ctr_eb_mongo_cpu_pct | 1.976 | 10.86 |
| ctr_eb_mongo_mem_mb | 3076.178 | 3164.3 |
| ctr_eb_nats_cpu_pct | 216.933 | 252.36 |
| ctr_eb_nats_mem_mb | 2291.657 | 4235.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.4 | 9.4 |
| ctr_eb_postgres_cpu_pct | 0.37 | 1.63 |
| ctr_eb_postgres_mem_mb | 4154.929 | 4179 |
| ctr_eb_rabbitmq_cpu_pct | 15.102 | 17.42 |
| ctr_eb_rabbitmq_mem_mb | 793.716 | 921.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.468 | 64.29 |
| in_flight | 267.286 | 1079 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12855.073 | 13180 |
| produced_total | 462990.367 | 771862 |
| publish_mb_per_s | 44.409 | 68.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,636 events applied by workers, 814,636 generated |
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
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 59.391 | 152.575 | 168.959 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 59.391 | 151.551 | 220.159 |
| t-224 | 9 | 1 | 30.606 | 4.698 | 1098 | 0 | 303 | 303 | 0 | 30.719 | 149.503 | 167.935 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 52.991 | 129.535 | 176.127 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 390 | 390 | 0 | 29.439 | 122.367 | 138.239 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 15.679 | 117.247 | 164.863 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 16.255 | 110.591 | 175.103 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 15.935 | 109.567 | 216.063 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 24.831 | 105.983 | 140.287 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 15.807 | 103.935 | 173.055 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 56.831 | 100.351 | 100.351 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 0 | 17.279 | 98.815 | 200.703 |
| t-356 | 6 | 1 | 85.291 | 8.355 | 303 | 0 | 527 | 527 | 0 | 27.135 | 98.815 | 103.935 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 15.359 | 97.791 | 205.823 |
| t-362 | 47 | 1 | 47.764 | 36.599 | 3235 | 0 | 2327 | 2327 | 0 | 16.383 | 97.791 | 155.647 |
| t-435 | 8 | 1 | 39.599 | 5.882 | 3763 | 0 | 370 | 370 | 0 | 31.487 | 97.279 | 113.151 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 4980 | 4980 | 0 | 16.767 | 96.767 | 192.511 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 15.999 | 96.767 | 217.087 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 16.191 | 96.255 | 144.383 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 8031 | 8031 | 0 | 17.023 | 96.255 | 221.183 |
| t-022 | 63 | 1 | 30.227 | 31.845 | 162 | 0 | 2122 | 2122 | 0 | 17.919 | 95.743 | 163.839 |
| t-205 | 18 | 1 | 52.795 | 15.701 | 449 | 0 | 967 | 967 | 0 | 24.191 | 95.743 | 169.983 |
| t-231 | 43 | 1 | 50.375 | 34.767 | 156 | 0 | 2213 | 2213 | 0 | 18.175 | 95.743 | 156.671 |
| t-268 | 41 | 1 | 36.983 | 24.264 | 1430 | 0 | 1686 | 1686 | 0 | 18.303 | 95.743 | 166.911 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 16.255 | 95.231 | 169.983 |
