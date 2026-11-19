# 08-kafka_pg — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.106s, closing 0.719s, drain 0.983s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814631 | 814631 | 12854.504 | 12853.727 | 25567.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642903 | 21.373 | 16.895 | 41.375 | 60.159 | 70.527 | 142.719 |
| e2e_from_send | 642903 | 19.096 | 14.383 | 38.655 | 57.279 | 64.703 | 138.879 |
| producer_lag | 642903 | 2.277 | 1.964 | 4.463 | 9.367 | 17.615 | 37.599 |
| publish_call | 364081 | 4.614 | 3.865 | 6.315 | 13.575 | 81.791 | 399.359 |
| queue_wait | 642903 | 15.833 | 10.831 | 35.103 | 54.079 | 58.879 | 85.119 |
| broker_dwell | 642903 | 12.776 | 7.519 | 31.887 | 50.911 | 54.175 | 75.967 |
| receive_call | 587298 | 0.023 | 0.011 | 0.023 | 0.109 | 2.903 | 27.903 |
| ctx_load | 642903 | 1.237 | 1.014 | 2.245 | 4.615 | 7.787 | 27.199 |
| process | 642903 | 0.002 | 0.001 | 0.002 | 0.004 | 0.05 | 6.495 |
| ctx_commit | 642903 | 1.222 | 1 | 2.191 | 4.611 | 8.083 | 54.943 |
| ack | 642445 | 0.005 | 0.003 | 0.009 | 0.016 | 0.122 | 6.167 |
| service | 642903 | 3.264 | 2.581 | 5.951 | 12.623 | 22.351 | 75.647 |
| e2e_large | 331 | 61.694 | 60.831 | 90.303 | 128.895 | 142.719 | 142.719 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.727 | 13149.9 |
| applied_total | 462404.694 | 771259 |
| bench_cpu_cores | 6.165 | 6.364 |
| bench_cpu_pct | 25.687 | 26.52 |
| bench_gc_gen0 | 3.837 | 6 |
| bench_gc_gen1 | 2.041 | 5 |
| bench_gc_gen2 | 0.408 | 2 |
| bench_gc_heap_mb | 592.455 | 826.1 |
| bench_mem_mb | 1425.557 | 1565.5 |
| bench_threadpool_queue | 13.306 | 217 |
| bench_threadpool_threads | 33.857 | 44 |
| broker_backlog | 7.551 | 39 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 443004.429 | 738871 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 441877.306 | 737855 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 273.914 | 279.38 |
| ctr_eb_kafka_mem_mb | 2484.006 | 2780.9 |
| ctr_eb_mongo_cpu_pct | 1.203 | 2.08 |
| ctr_eb_mongo_mem_mb | 3958.343 | 4197.1 |
| ctr_eb_nats_cpu_pct | 0.182 | 1.07 |
| ctr_eb_nats_mem_mb | 3133.09 | 3220.6 |
| ctr_eb_pgbouncer_cpu_pct | 57.863 | 59.01 |
| ctr_eb_pgbouncer_mem_mb | 21.104 | 22.6 |
| ctr_eb_postgres_cpu_pct | 294.18 | 307.72 |
| ctr_eb_postgres_mem_mb | 4916.869 | 4951.5 |
| ctr_eb_rabbitmq_cpu_pct | 14.99 | 16.6 |
| ctr_eb_rabbitmq_mem_mb | 792.829 | 929.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.018 | 62.39 |
| in_flight | 180.837 | 255 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.02 | 8 |
| pg_conn_idle | 62.02 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.041 | 65 |
| pg_db_size_mb | 1302.643 | 1737.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25781.963 | 27004.4 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59268.045 | 63332.8 |
| pg_tup_ins_per_s | 10892.841 | 19135.2 |
| pg_tup_upd_per_s | 21290.541 | 22428.7 |
| pg_wal_mb_per_s | 33.351 | 49.74 |
| pgb_cl_active | 495 | 495 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 11.367 | 60 |
| pgb_sv_idle | 52.633 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.504 | 13169.9 |
| produced_total | 462585.531 | 771437 |
| publish_mb_per_s | 44.406 | 68.18 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 51.967 | 121.343 | 143.359 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 46.591 | 110.079 | 110.079 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 48.895 | 109.055 | 129.535 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 46.079 | 108.543 | 142.335 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 28.159 | 72.191 | 83.967 |
| t-236 | 12 | 1 | 21.423 | 4.143 | 131 | 0 | 263 | 263 | 0 | 32.255 | 70.143 | 73.215 |
| t-390 | 10 | 1 | 36.089 | 5.892 | 167 | 0 | 353 | 353 | 0 | 28.159 | 68.607 | 76.799 |
| t-101 | 6 | 1 | 31.723 | 2.854 | 198 | 0 | 174 | 174 | 0 | 32.383 | 67.071 | 67.071 |
| t-190 | 6 | 1 | 69.297 | 7.234 | 241 | 0 | 436 | 436 | 0 | 26.879 | 67.071 | 71.679 |
| t-226 | 6 | 1 | 87.952 | 9.302 | 623 | 0 | 576 | 576 | 0 | 24.319 | 67.071 | 108.543 |
| t-370 | 12 | 1 | 20.006 | 4.067 | 568 | 0 | 254 | 254 | 0 | 28.671 | 67.071 | 71.167 |
| t-452 | 6 | 1 | 20.6 | 2.155 | 1797 | 0 | 137 | 137 | 0 | 32.511 | 67.071 | 68.607 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 299 | 299 | 0 | 28.799 | 66.559 | 69.631 |
| t-202 | 8 | 1 | 45.584 | 5.861 | 1480 | 0 | 382 | 382 | 0 | 28.927 | 66.559 | 73.215 |
| t-255 | 11 | 1 | 35.499 | 6.413 | 2029 | 0 | 386 | 386 | 0 | 27.903 | 66.559 | 69.631 |
| t-345 | 13 | 1 | 16.657 | 3.763 | 1052 | 0 | 278 | 278 | 0 | 26.367 | 66.559 | 69.119 |
| t-365 | 13 | 1 | 35.093 | 7.573 | 3428 | 0 | 468 | 468 | 0 | 24.831 | 66.559 | 72.191 |
| t-368 | 36 | 1 | 18.505 | 10.972 | 2666 | 0 | 731 | 731 | 0 | 24.191 | 66.559 | 72.703 |
| t-448 | 6 | 1 | 47.757 | 5.012 | 2313 | 0 | 298 | 298 | 0 | 27.135 | 66.559 | 67.071 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 28.543 | 66.047 | 72.703 |
| t-117 | 28 | 1 | 23.949 | 11.225 | 294 | 0 | 746 | 746 | 0 | 24.191 | 66.047 | 76.799 |
| t-134 | 21 | 1 | 19.598 | 6.714 | 3368 | 0 | 453 | 453 | 0 | 28.287 | 66.047 | 76.287 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 28.799 | 66.047 | 67.583 |
| t-000 | 14 | 1 | 43.972 | 10.489 | 427 | 0 | 663 | 663 | 0 | 26.239 | 65.535 | 71.679 |
| t-021 | 19 | 1 | 16.257 | 5.407 | 3252 | 0 | 346 | 346 | 0 | 30.207 | 65.535 | 76.799 |
