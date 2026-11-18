# 06-kafkafsync_pg — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=1), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 62.653s, closing 7.253s, drain 0.19s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 222435 | 222435 | 3201.963 | 3214.84 | 3956.1 | 0 | 0 | 0 | 154 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 60131 | 40724.082 | 41058.303 | 46497.791 | 48037.887 | 48627.711 | 49053.695 |
| e2e_from_send | 60131 | 2484.282 | 2445.311 | 4509.695 | 5488.639 | 5857.279 | 6156.287 |
| producer_lag | 60131 | 38239.776 | 38371.327 | 44367.871 | 45809.663 | 46137.343 | 46268.415 |
| publish_call | 521 | 4213.689 | 4780.031 | 5414.911 | 5935.103 | 6156.287 | 6180.863 |
| queue_wait | 60131 | 2478.145 | 2439.167 | 4505.599 | 5484.543 | 5853.183 | 6148.095 |
| broker_dwell | 60131 | 29.489 | 28.175 | 52.383 | 64.255 | 77.567 | 120.447 |
| receive_call | 97823 | 0.033 | 0.018 | 0.031 | 0.081 | 4.471 | 20.095 |
| ctx_load | 60131 | 0.323 | 0.303 | 0.403 | 0.646 | 1.551 | 7.515 |
| process | 60131 | 0.002 | 0.001 | 0.002 | 0.004 | 0.045 | 8.807 |
| ctx_commit | 60131 | 2.327 | 1.997 | 3.579 | 7.703 | 18.927 | 34.623 |
| ack | 58857 | 0.006 | 0.007 | 0.009 | 0.018 | 0.039 | 0.457 |
| service | 60131 | 6.139 | 4.651 | 12.287 | 23.199 | 39.647 | 77.055 |
| e2e_large | 54 | 39179.302 | 38862.847 | 45481.983 | 46399.487 | 47054.847 | 47054.847 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3214.84 | 3634.7 |
| applied_total | 115332.615 | 196024 |
| bench_cpu_cores | 1.676 | 2.183 |
| bench_cpu_pct | 6.984 | 9.1 |
| bench_gc_gen0 | 0.788 | 2 |
| bench_gc_gen1 | 0.327 | 1 |
| bench_gc_gen2 | 0.308 | 1 |
| bench_gc_heap_mb | 422.138 | 602.9 |
| bench_mem_mb | 1227.204 | 1366.5 |
| bench_threadpool_queue | 2.404 | 64 |
| bench_threadpool_threads | 18.981 | 25 |
| broker_backlog | 0 | 0 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 76692.404 | 196055 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 44180.25 | 178525 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 96.727 | 110.83 |
| ctr_eb_kafka_mem_mb | 2000.294 | 2078.1 |
| ctr_eb_mongo_cpu_pct | 1.029 | 1.87 |
| ctr_eb_mongo_mem_mb | 4212.746 | 4269.6 |
| ctr_eb_nats_cpu_pct | 0.324 | 0.48 |
| ctr_eb_nats_mem_mb | 316.938 | 318.7 |
| ctr_eb_pgbouncer_cpu_pct | 17.833 | 20.37 |
| ctr_eb_pgbouncer_mem_mb | 13.017 | 14.2 |
| ctr_eb_postgres_cpu_pct | 81.65 | 94.95 |
| ctr_eb_postgres_mem_mb | 4679.281 | 4695.9 |
| ctr_eb_rabbitmq_cpu_pct | 9.958 | 12.04 |
| ctr_eb_rabbitmq_mem_mb | 1186.319 | 1275.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 16.996 | 21.68 |
| in_flight | -7715.769 | -4428 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 5.462 | 12 |
| pg_conn_idle | 55.385 | 61 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 60.846 | 61 |
| pg_db_size_mb | 868.204 | 1082.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 6097.832 | 6715.6 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 13441.632 | 15124.6 |
| pg_tup_ins_per_s | 3759.53 | 4169.4 |
| pg_tup_upd_per_s | 4708.33 | 5417.1 |
| pg_wal_mb_per_s | 9.589 | 11.05 |
| pgb_cl_active | 90.385 | 100 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 5.077 | 16 |
| pgb_sv_idle | 41.288 | 44 |
| pgb_sv_used | 14.481 | 19 |
| produced_per_s | 3201.963 | 9993 |
| produced_total | 107616.846 | 191588 |
| publish_mb_per_s | 14.325 | 42.18 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 44,600 records are Completed |
| record_counts | pass | 44,600 records found, 44,600 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 154 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 222,435 events applied by workers, 222,435 generated |
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
| t-059 | 8 | 1 | 63.812 | 8.437 | 1449 | 0 | 150 | 150 | 0 | 41943.039 | 49020.927 | 49020.927 |
| t-146 | 6 | 1 | 43.291 | 4.645 | 246 | 0 | 84 | 84 | 0 | 41943.039 | 49020.927 | 49020.927 |
| t-321 | 10 | 1 | 63.886 | 11.456 | 204 | 0 | 176 | 176 | 0 | 41418.751 | 49020.927 | 49020.927 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 109 | 109 | 0 | 40632.319 | 49020.927 | 49020.927 |
| t-424 | 8 | 1 | 76.354 | 10.019 | 1700 | 0 | 156 | 156 | 0 | 39321.599 | 49020.927 | 49020.927 |
| t-426 | 14 | 1 | 31.182 | 7.183 | 830 | 0 | 135 | 135 | 0 | 39845.887 | 49020.927 | 49020.927 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 157 | 157 | 0 | 41156.607 | 48758.783 | 48758.783 |
| t-036 | 6 | 1 | 29.546 | 2.968 | 284 | 0 | 52 | 52 | 0 | 40894.463 | 48758.783 | 48758.783 |
| t-096 | 15 | 1 | 48.42 | 12.09 | 1642 | 0 | 202 | 202 | 0 | 41680.895 | 48758.783 | 48758.783 |
| t-180 | 19 | 1 | 47.393 | 14.278 | 152 | 0 | 243 | 243 | 0 | 40632.319 | 48758.783 | 48758.783 |
| t-212 | 29 | 1 | 50.595 | 24.477 | 296 | 0 | 438 | 438 | 0 | 41418.751 | 48758.783 | 48758.783 |
| t-317 | 32 | 1 | 39.571 | 21.604 | 1288 | 0 | 374 | 374 | 0 | 40370.175 | 48758.783 | 49020.927 |
| t-334 | 19 | 1 | 62.361 | 19.957 | 391 | 0 | 348 | 348 | 0 | 41943.039 | 48758.783 | 48758.783 |
| t-376 | 65 | 1 | 23.304 | 25.914 | 566 | 0 | 490 | 490 | 0 | 41943.039 | 48758.783 | 48758.783 |
| t-409 | 48 | 2 | 64.626 | 50.933 | 671 | 0 | 837 | 837 | 0 | 40632.319 | 48758.783 | 49020.927 |
| t-428 | 37 | 1 | 60.011 | 37.101 | 159 | 0 | 619 | 619 | 0 | 41156.607 | 48758.783 | 49020.927 |
| t-453 | 6 | 1 | 44.943 | 4.427 | 874 | 0 | 59 | 59 | 0 | 43515.903 | 48758.783 | 48758.783 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 753 | 753 | 0 | 41156.607 | 48758.783 | 49020.927 |
| t-457 | 18 | 1 | 23.751 | 7.425 | 1557 | 0 | 133 | 133 | 0 | 42205.183 | 48758.783 | 48758.783 |
| t-022 | 63 | 1 | 30.227 | 31.845 | 162 | 0 | 619 | 619 | 0 | 41680.895 | 48496.639 | 48496.639 |
| t-030 | 19 | 1 | 24.985 | 8.002 | 748 | 0 | 144 | 144 | 0 | 41418.751 | 48496.639 | 48496.639 |
| t-047 | 52 | 1 | 34.438 | 30.81 | 343 | 0 | 559 | 559 | 0 | 41156.607 | 48496.639 | 48758.783 |
| t-061 | 148 | 2 | 22.45 | 54.927 | 1274 | 0 | 1111 | 1111 | 0 | 40894.463 | 48496.639 | 48758.783 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 168 | 168 | 0 | 42205.183 | 48496.639 | 48758.783 |
| t-074 | 30 | 1 | 59.845 | 28.959 | 1515 | 0 | 463 | 463 | 0 | 41943.039 | 48496.639 | 48758.783 |
