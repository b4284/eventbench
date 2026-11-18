# 1-fifo-base — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.164s, drain 2.811s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276547 | 276547 | 4008.788 | 4008.806 | 12637.5 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200493 | 12.214 | 10.415 | 16.055 | 30.895 | 315.903 | 727.039 |
| e2e_from_send | 200493 | 10.622 | 9.263 | 14.991 | 28.447 | 117.951 | 700.927 |
| producer_lag | 200493 | 1.592 | 0.743 | 2.403 | 5.163 | 230.271 | 500.735 |
| publish_call | 198605 | 1.738 | 1.558 | 2.231 | 3.861 | 17.295 | 489.215 |
| queue_wait | 200493 | 5.396 | 4.999 | 6.979 | 17.119 | 76.287 | 577.535 |
| broker_dwell | 200493 | 4.426 | 3.985 | 6.015 | 16.111 | 74.623 | 519.935 |
| receive_call | 147819 | 2.208 | 1.766 | 2.449 | 14.095 | 59.519 | 153.087 |
| ctx_load | 200493 | 0.385 | 0.32 | 0.595 | 1.129 | 2.367 | 19.727 |
| process | 200493 | 0.001 | 0.001 | 0.002 | 0.004 | 0.015 | 2.275 |
| ctx_commit | 200493 | 1.345 | 1.196 | 1.804 | 2.811 | 13.767 | 72.447 |
| ack | 200493 | 1.371 | 1.269 | 1.972 | 2.863 | 12.983 | 43.583 |
| service | 200493 | 5.226 | 3.743 | 9.335 | 20.143 | 47.167 | 162.431 |
| e2e_large | 7 | 455.433 | 433.151 | 662.527 | 717.311 | 717.311 | 717.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.806 | 4179.6 |
| applied_total | 151743.51 | 247884 |
| bench_cpu_cores | 1.969 | 2.125 |
| bench_cpu_pct | 8.202 | 8.86 |
| bench_gc_gen0 | 3.98 | 5 |
| bench_gc_gen1 | 3.163 | 5 |
| bench_gc_gen2 | 0.082 | 1 |
| bench_gc_heap_mb | 984.884 | 1137.2 |
| bench_mem_mb | 804.439 | 898.3 |
| bench_threadpool_queue | 0.571 | 13 |
| bench_threadpool_threads | 21.449 | 25 |
| broker_backlog | 35.061 | 55 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 10.143 | 23 |
| ctr_eb_pgbouncer_cpu_pct | 8.001 | 8.32 |
| ctr_eb_pgbouncer_mem_mb | 12.614 | 15.3 |
| ctr_eb_postgres_cpu_pct | 91.551 | 95.02 |
| ctr_eb_postgres_mem_mb | 2010.588 | 2040.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 26.571 | 34.15 |
| in_flight | 27.143 | 42 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 18.714 | 33 |
| pg_conn_idle | 46.347 | 59 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.061 | 66 |
| pg_db_size_mb | 220.163 | 265.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24817.369 | 31121.6 |
| pg_tup_del_per_s | 4838.737 | 14598.9 |
| pg_tup_fetch_per_s | 79792.571 | 127582 |
| pg_tup_ins_per_s | 6265.933 | 23028.9 |
| pg_tup_upd_per_s | 10506.571 | 13004.1 |
| pg_wal_mb_per_s | 13.839 | 48 |
| pgb_cl_active | 247 | 247 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 20.408 | 33 |
| pgb_sv_idle | 43.592 | 60 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.788 | 4184.6 |
| produced_total | 151770.653 | 247901 |
| publish_mb_per_s | 3.939 | 33.07 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,547 events applied by workers, 276,547 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 10.479 | 35.551 | 366.079 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 10.327 | 29.519 | 95.231 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 10.471 | 29.887 | 121.663 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 10.479 | 163.071 | 465.663 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 10.447 | 28.303 | 91.327 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 10.383 | 201.087 | 727.039 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 10.447 | 202.879 | 671.743 |
| A-7 | 800 | 8 | 300 | 19820 | 19820 | 0 | 10.319 | 27.743 | 89.919 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 10.431 | 29.743 | 169.727 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 10.287 | 26.703 | 66.879 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 10.391 | 25.599 | 47.679 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 10.455 | 24.847 | 44.063 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 10.439 | 28.559 | 227.583 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 10.503 | 26.159 | 43.519 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 10.559 | 26.495 | 38.175 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 10.399 | 26.543 | 94.975 |
| B-6 | 800 | 8 | 100 | 7723 | 7723 | 0 | 10.431 | 26.735 | 49.023 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 10.415 | 28.351 | 55.231 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.503 | 26.047 | 53.983 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.415 | 25.199 | 59.231 |
