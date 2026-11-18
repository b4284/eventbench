# 3-fifo-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.144s, drain 2.513s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276546 | 276546 | 4008.906 | 4008.888 | 12028.6 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200492 | 10.909 | 10.279 | 15.167 | 22.639 | 38.303 | 160.127 |
| e2e_from_send | 200492 | 9.952 | 9.135 | 14.191 | 21.727 | 34.975 | 135.167 |
| producer_lag | 200492 | 0.957 | 0.722 | 2.333 | 3.465 | 5.547 | 92.479 |
| publish_call | 201325 | 1.623 | 1.526 | 2.163 | 3.139 | 6.587 | 72.383 |
| queue_wait | 200492 | 5.051 | 4.963 | 6.787 | 12.135 | 20.255 | 91.007 |
| broker_dwell | 200492 | 3.942 | 3.809 | 5.683 | 10.983 | 18.383 | 47.967 |
| receive_call | 148322 | 2.016 | 1.729 | 2.391 | 7.867 | 43.391 | 104.895 |
| ctx_load | 200492 | 0.378 | 0.316 | 0.575 | 1.108 | 2.705 | 11.367 |
| process | 200492 | 0.002 | 0.001 | 0.002 | 0.004 | 0.016 | 33.375 |
| ctx_commit | 200492 | 1.286 | 1.166 | 1.75 | 2.665 | 4.427 | 19.135 |
| ack | 200492 | 1.321 | 1.235 | 1.91 | 2.795 | 4.619 | 20.239 |
| service | 200492 | 4.902 | 3.629 | 8.903 | 16.047 | 25.519 | 103.295 |
| e2e_large | 7 | 100.846 | 99.007 | 112.447 | 151.167 | 151.167 | 151.167 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.888 | 4112.7 |
| applied_total | 151744.816 | 247880 |
| bench_cpu_cores | 1.982 | 2.102 |
| bench_cpu_pct | 8.259 | 8.76 |
| bench_gc_gen0 | 4.939 | 6 |
| bench_gc_gen1 | 3.857 | 6 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 48.19 | 67.5 |
| bench_mem_mb | 159.012 | 186.2 |
| bench_threadpool_queue | 1.49 | 28 |
| bench_threadpool_threads | 22.388 | 26 |
| broker_backlog | 32.918 | 57 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 9.98 | 25 |
| ctr_eb_pgbouncer_cpu_pct | 5.24 | 5.59 |
| ctr_eb_pgbouncer_mem_mb | 12.943 | 14.2 |
| ctr_eb_postgres_cpu_pct | 60.535 | 64.36 |
| ctr_eb_postgres_mem_mb | 1599.347 | 1620.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 25.885 | 30.42 |
| in_flight | 26.388 | 45 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 14.388 | 25 |
| pg_conn_idle | 50.796 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.184 | 66 |
| pg_db_size_mb | 205.163 | 238.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25172.633 | 29680 |
| pg_tup_del_per_s | 4044.345 | 4763.1 |
| pg_tup_fetch_per_s | 79903.708 | 106018.8 |
| pg_tup_ins_per_s | 5577.073 | 12847.3 |
| pg_tup_upd_per_s | 10637.771 | 12556.4 |
| pg_wal_mb_per_s | 12.276 | 26.92 |
| pgb_cl_active | 229 | 229 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 21.98 | 40 |
| pgb_sv_idle | 42.02 | 59 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.906 | 4117.7 |
| produced_total | 151771.204 | 247897 |
| publish_mb_per_s | 2.068 | 2.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,546 events applied by workers, 276,546 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 10.303 | 23.727 | 99.007 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 10.271 | 22.255 | 55.455 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 10.319 | 23.055 | 42.079 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 10.239 | 24.303 | 108.799 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 10.319 | 22.911 | 106.879 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 10.263 | 23.583 | 160.127 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 10.271 | 24.591 | 118.015 |
| A-7 | 800 | 8 | 300 | 19820 | 19820 | 0 | 10.215 | 21.231 | 35.103 |
| A-8 | 800 | 8 | 300 | 20123 | 20123 | 0 | 10.215 | 21.743 | 41.823 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 10.231 | 21.471 | 76.479 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 10.447 | 23.503 | 40.511 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 10.327 | 22.703 | 35.487 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 10.247 | 22.399 | 68.543 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 10.383 | 22.127 | 35.231 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 10.343 | 21.759 | 36.383 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 10.287 | 22.671 | 35.455 |
| B-6 | 800 | 8 | 100 | 7723 | 7723 | 0 | 10.367 | 22.287 | 37.727 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 10.215 | 20.687 | 29.071 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.215 | 21.711 | 37.375 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.351 | 21.839 | 34.719 |
