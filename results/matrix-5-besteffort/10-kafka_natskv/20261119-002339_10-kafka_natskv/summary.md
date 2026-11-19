# 10-kafka_natskv — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.455s, drain 1.684s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814637 | 814637 | 12855.31 | 12855.42 | 20892.2 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642914 | 27.94 | 19.663 | 52.511 | 150.527 | 312.319 | 598.527 |
| e2e_from_send | 642914 | 24.598 | 17.695 | 49.087 | 113.471 | 210.047 | 394.495 |
| producer_lag | 642914 | 3.342 | 1.726 | 3.929 | 56.415 | 163.455 | 327.679 |
| publish_call | 405359 | 3.935 | 3.397 | 5.039 | 10.895 | 51.519 | 327.679 |
| queue_wait | 642914 | 17.401 | 10.927 | 37.919 | 88.959 | 197.119 | 365.311 |
| broker_dwell | 642914 | 13.217 | 7.415 | 32.719 | 55.935 | 129.215 | 285.183 |
| receive_call | 582822 | 0.021 | 0.011 | 0.023 | 0.103 | 1.796 | 36.607 |
| ctx_load | 642914 | 1.818 | 1.207 | 3.325 | 10.071 | 29.983 | 112.063 |
| process | 642914 | 0.002 | 0.001 | 0.002 | 0.004 | 0.058 | 5.219 |
| ctx_commit | 642914 | 3.283 | 2.319 | 6.355 | 17.487 | 31.631 | 116.287 |
| ack | 642712 | 0.006 | 0.004 | 0.009 | 0.019 | 0.109 | 5.291 |
| service | 642914 | 7.197 | 4.443 | 13.943 | 48.383 | 97.343 | 193.535 |
| e2e_large | 331 | 88.5 | 74.815 | 131.839 | 355.071 | 493.311 | 493.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12855.42 | 14462.5 |
| applied_total | 463264.163 | 772289 |
| bench_cpu_cores | 7.271 | 7.497 |
| bench_cpu_pct | 30.296 | 31.24 |
| bench_gc_gen0 | 17.245 | 23 |
| bench_gc_gen1 | 4.408 | 10 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 651.08 | 835.6 |
| bench_mem_mb | 1498.239 | 1614.6 |
| bench_threadpool_queue | 23.918 | 217 |
| bench_threadpool_threads | 39.551 | 44 |
| broker_backlog | 27.837 | 137 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 449162.469 | 766854 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 448284.102 | 765983 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 289.564 | 303.79 |
| ctr_eb_kafka_mem_mb | 3371.673 | 3611.6 |
| ctr_eb_mongo_cpu_pct | 1.141 | 2.48 |
| ctr_eb_mongo_mem_mb | 4414.81 | 4497.5 |
| ctr_eb_nats_cpu_pct | 209.175 | 216.33 |
| ctr_eb_nats_mem_mb | 1931.363 | 2928.7 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 11.976 | 12 |
| ctr_eb_postgres_cpu_pct | 0.57 | 1.77 |
| ctr_eb_postgres_mem_mb | 4379.112 | 4427.9 |
| ctr_eb_rabbitmq_cpu_pct | 13.105 | 13.54 |
| ctr_eb_rabbitmq_mem_mb | 795.131 | 799.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 56.803 | 61.08 |
| in_flight | 218.959 | 649 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12855.31 | 15180.6 |
| produced_total | 463483.122 | 772482 |
| publish_mb_per_s | 44.355 | 75.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,597 records are Completed |
| record_counts | pass | 117,597 records found, 117,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,637 events applied by workers, 814,637 generated |
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
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 61.439 | 356.351 | 493.567 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 68.607 | 296.959 | 296.959 |
| t-098 | 6 | 1 | 27.347 | 2.525 | 192 | 0 | 193 | 193 | 0 | 33.279 | 282.623 | 296.959 |
| t-430 | 6 | 1 | 23.835 | 2.584 | 1163 | 0 | 168 | 168 | 0 | 33.023 | 280.575 | 352.255 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 63.999 | 261.119 | 428.031 |
| t-412 | 6 | 1 | 28.435 | 2.632 | 468 | 0 | 164 | 164 | 0 | 31.743 | 253.951 | 276.479 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 66.047 | 246.783 | 397.311 |
| t-144 | 6 | 1 | 24.576 | 2.554 | 816 | 0 | 174 | 174 | 0 | 39.935 | 242.687 | 325.631 |
| t-060 | 7 | 1 | 47.726 | 5.892 | 1546 | 0 | 398 | 398 | 0 | 29.055 | 234.495 | 475.135 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 411 | 411 | 0 | 33.023 | 228.351 | 374.783 |
| t-292 | 16 | 1 | 44.698 | 12.892 | 916 | 0 | 794 | 794 | 0 | 27.391 | 225.279 | 372.735 |
| t-432 | 9 | 1 | 58.559 | 8.62 | 747 | 0 | 529 | 529 | 0 | 27.391 | 218.111 | 352.255 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 390 | 390 | 0 | 27.903 | 217.087 | 274.431 |
| t-415 | 14 | 1 | 69.296 | 16.018 | 2972 | 0 | 1007 | 1007 | 0 | 23.039 | 217.087 | 522.239 |
| t-027 | 62 | 1 | 27.852 | 27.964 | 935 | 0 | 1867 | 1867 | 0 | 20.095 | 216.063 | 409.599 |
| t-091 | 27 | 1 | 37.806 | 16.188 | 952 | 0 | 1100 | 1100 | 0 | 23.679 | 216.063 | 499.711 |
| t-316 | 19 | 1 | 26.847 | 8.545 | 169 | 0 | 607 | 607 | 0 | 28.543 | 216.063 | 321.535 |
| t-148 | 6 | 1 | 62.881 | 5.748 | 715 | 0 | 407 | 407 | 0 | 31.615 | 214.015 | 329.727 |
| t-255 | 11 | 1 | 35.499 | 6.413 | 2029 | 0 | 386 | 386 | 0 | 33.279 | 212.991 | 350.207 |
| t-056 | 7 | 1 | 48.226 | 5.826 | 347 | 0 | 372 | 372 | 0 | 31.103 | 211.967 | 387.071 |
| t-381 | 11 | 1 | 47.024 | 8.839 | 134 | 0 | 603 | 603 | 0 | 30.335 | 210.943 | 282.623 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 36.863 | 210.943 | 225.279 |
| t-252 | 38 | 1 | 16.218 | 10.553 | 2736 | 0 | 705 | 705 | 0 | 29.823 | 209.919 | 331.775 |
| t-207 | 27 | 1 | 20.763 | 9.519 | 595 | 0 | 622 | 622 | 0 | 26.111 | 203.775 | 294.911 |
| t-015 | 9 | 1 | 40.857 | 6.053 | 3462 | 0 | 393 | 393 | 0 | 33.535 | 202.751 | 362.495 |
