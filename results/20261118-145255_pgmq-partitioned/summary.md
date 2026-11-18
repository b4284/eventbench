# pgmq-partitioned — PASS ✅

- Broker: `pgmq partitioned (=workers per group), vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 12.103s, closing 0.05s, drain 1.317s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 74466 | 74466 | 4401.638 | 4404.138 | 13431.6 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 39548 | 20.154 | 14.679 | 38.751 | 59.615 | 228.351 | 447.487 |
| e2e_from_send | 39548 | 19.023 | 13.511 | 37.407 | 57.823 | 205.311 | 401.919 |
| producer_lag | 39548 | 1.131 | 0.803 | 2.433 | 4.679 | 22.735 | 156.543 |
| publish_call | 39985 | 2.355 | 1.797 | 3.029 | 13.831 | 33.119 | 524.799 |
| queue_wait | 39548 | 14.335 | 9.111 | 32.959 | 53.567 | 185.983 | 362.495 |
| broker_dwell | 39548 | 13.122 | 7.779 | 31.631 | 52.287 | 183.423 | 361.471 |
| receive_call | 40850 | 2.554 | 1.979 | 3.679 | 15.927 | 29.487 | 210.047 |
| ctx_load | 39548 | 0.448 | 0.348 | 0.719 | 1.805 | 3.263 | 24.559 |
| process | 39548 | 0.002 | 0.001 | 0.004 | 0.007 | 0.017 | 2.036 |
| ctx_commit | 39548 | 1.423 | 1.232 | 1.962 | 3.653 | 14.287 | 24.303 |
| ack | 39548 | 1.549 | 1.445 | 2.141 | 3.651 | 14.887 | 23.407 |
| service | 39548 | 4.689 | 3.467 | 7.891 | 18.991 | 62.783 | 97.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4404.138 | 4848 |
| applied_total | 40717 | 55537 |
| bench_cpu_cores | 2.344 | 2.892 |
| bench_cpu_pct | 9.769 | 12.05 |
| bench_gc_gen0 | 4.125 | 5 |
| bench_gc_gen1 | 0.125 | 1 |
| bench_gc_gen2 | 0.125 | 1 |
| bench_gc_heap_mb | 1079.6 | 1097.3 |
| bench_mem_mb | 972.275 | 987.7 |
| bench_threadpool_queue | 0.125 | 1 |
| bench_threadpool_threads | 25 | 25 |
| broker_backlog | 152.25 | 694 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 78.125 | 402 |
| ctr_eb_pgbouncer_cpu_pct | 8.421 | 9.13 |
| ctr_eb_pgbouncer_mem_mb | 13.237 | 13.8 |
| ctr_eb_postgres_cpu_pct | 95.311 | 102.53 |
| ctr_eb_postgres_mem_mb | 1356.7 | 1371.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 29.982 | 32.76 |
| in_flight | 54 | 65 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 29.25 | 42 |
| pg_conn_idle | 35.875 | 40 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.125 | 66 |
| pg_db_size_mb | 302.075 | 324.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 34009.362 | 38546.1 |
| pg_tup_del_per_s | 14766.525 | 28984.1 |
| pg_tup_fetch_per_s | 133818.062 | 343655.1 |
| pg_tup_ins_per_s | 24499.163 | 44753.3 |
| pg_tup_upd_per_s | 12463.787 | 14605.9 |
| pg_wal_mb_per_s | 45.425 | 89.52 |
| pgb_cl_active | 298 | 298 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 40.75 | 55 |
| pgb_sv_idle | 23.25 | 27 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4401.638 | 4855 |
| produced_total | 40771 | 55590 |
| publish_mb_per_s | 2.271 | 2.5 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 22,595 records are Completed |
| record_counts | pass | 22,595 records found, 22,595 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 74,466 events applied by workers, 74,466 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups

| group | sources | workers | offered/s | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| A-0 | 800 | 8 | 300 | 4951 | 4951 | 0 | 13.407 | 59.007 | 285.439 |
| A-1 | 800 | 8 | 300 | 4944 | 4944 | 0 | 13.407 | 56.607 | 90.431 |
| A-2 | 800 | 8 | 300 | 5108 | 5108 | 0 | 13.407 | 54.431 | 65.855 |
| A-3 | 800 | 8 | 300 | 5112 | 5112 | 0 | 13.375 | 99.647 | 194.175 |
| A-4 | 800 | 8 | 300 | 5061 | 5061 | 0 | 13.799 | 56.767 | 64.447 |
| A-5 | 800 | 8 | 300 | 4978 | 4978 | 0 | 13.855 | 56.927 | 65.791 |
| A-6 | 800 | 8 | 300 | 4970 | 4970 | 0 | 13.599 | 56.095 | 83.775 |
| A-7 | 800 | 8 | 300 | 5079 | 5079 | 0 | 13.263 | 68.479 | 148.607 |
| A-8 | 800 | 8 | 300 | 5005 | 5005 | 0 | 13.391 | 55.519 | 68.863 |
| A-9 | 800 | 8 | 300 | 4985 | 4985 | 0 | 13.919 | 226.943 | 402.431 |
| B-0 | 800 | 8 | 100 | 2404 | 2404 | 0 | 19.791 | 154.367 | 447.487 |
| B-1 | 800 | 8 | 100 | 2436 | 2436 | 0 | 17.935 | 59.551 | 74.175 |
| B-2 | 800 | 8 | 100 | 2410 | 2410 | 0 | 17.663 | 59.743 | 82.431 |
| B-3 | 800 | 8 | 100 | 2459 | 2459 | 0 | 18.495 | 58.463 | 76.159 |
| B-4 | 800 | 8 | 100 | 2419 | 2419 | 0 | 18.927 | 59.583 | 71.231 |
| B-5 | 800 | 8 | 100 | 2377 | 2377 | 0 | 19.199 | 60.191 | 76.351 |
| B-6 | 800 | 8 | 100 | 2491 | 2491 | 0 | 17.711 | 59.039 | 70.911 |
| B-7 | 800 | 8 | 100 | 2453 | 2453 | 0 | 17.855 | 58.719 | 76.735 |
| B-8 | 800 | 8 | 100 | 2383 | 2383 | 0 | 17.471 | 58.623 | 63.583 |
| B-9 | 800 | 8 | 100 | 2441 | 2441 | 0 | 18.415 | 59.423 | 70.591 |
