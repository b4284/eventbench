# 1-fifo-base — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.14s, drain 2.61s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276548 | 276548 | 4008.722 | 4008.89 | 11477.4 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200494 | 11.425 | 9.991 | 14.719 | 23.471 | 296.959 | 735.231 |
| e2e_from_send | 200494 | 9.91 | 8.839 | 13.791 | 22.527 | 92.095 | 714.239 |
| producer_lag | 200494 | 1.515 | 0.701 | 2.347 | 3.539 | 232.319 | 474.623 |
| publish_call | 201046 | 1.606 | 1.483 | 2.091 | 3.373 | 7.267 | 465.407 |
| queue_wait | 200494 | 5.138 | 4.883 | 6.667 | 13.567 | 65.343 | 553.471 |
| broker_dwell | 200494 | 4.063 | 3.761 | 5.595 | 12.431 | 64.223 | 520.703 |
| receive_call | 148145 | 2.002 | 1.672 | 2.343 | 8.091 | 44.575 | 162.431 |
| ctx_load | 200494 | 0.369 | 0.309 | 0.561 | 1.097 | 2.147 | 43.295 |
| process | 200494 | 0.002 | 0.001 | 0.002 | 0.004 | 0.016 | 7.443 |
| ctx_commit | 200494 | 1.247 | 1.142 | 1.648 | 2.547 | 4.847 | 73.791 |
| ack | 200494 | 1.25 | 1.166 | 1.784 | 2.645 | 4.791 | 20.607 |
| service | 200494 | 4.772 | 3.381 | 8.591 | 16.031 | 34.495 | 190.975 |
| e2e_large | 7 | 467.575 | 452.095 | 649.215 | 728.575 | 728.575 | 728.575 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.89 | 4176.1 |
| applied_total | 151742.98 | 247887 |
| bench_cpu_cores | 1.971 | 2.108 |
| bench_cpu_pct | 8.213 | 8.78 |
| bench_gc_gen0 | 4.163 | 5 |
| bench_gc_gen1 | 2.367 | 5 |
| bench_gc_gen2 | 0.082 | 1 |
| bench_gc_heap_mb | 923.029 | 1142.3 |
| bench_mem_mb | 827.218 | 984.9 |
| bench_threadpool_queue | 0.612 | 16 |
| bench_threadpool_threads | 23.551 | 27 |
| broker_backlog | 34.204 | 48 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 10.082 | 22 |
| ctr_eb_pgbouncer_cpu_pct | 3.247 | 3.64 |
| ctr_eb_pgbouncer_mem_mb | 12.514 | 14 |
| ctr_eb_postgres_cpu_pct | 37.858 | 42.12 |
| ctr_eb_postgres_mem_mb | 1295.027 | 1331.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 26.348 | 34.38 |
| in_flight | 26.367 | 41 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 17.143 | 52 |
| pg_conn_idle | 47.857 | 59 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 219.041 | 281.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24956.776 | 26219.7 |
| pg_tup_del_per_s | 4846.141 | 14428.7 |
| pg_tup_fetch_per_s | 77971.151 | 108398.6 |
| pg_tup_ins_per_s | 6167.067 | 22903.2 |
| pg_tup_upd_per_s | 10551.12 | 11006.2 |
| pg_wal_mb_per_s | 13.651 | 47.89 |
| pgb_cl_active | 240 | 240 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 18.653 | 34 |
| pgb_sv_idle | 45.347 | 57 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.722 | 4192.1 |
| produced_total | 151769.347 | 247898 |
| publish_mb_per_s | 3.94 | 33.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,548 events applied by workers, 276,548 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 10.055 | 25.775 | 363.775 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 9.935 | 21.439 | 46.399 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 9.967 | 21.759 | 74.687 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 9.943 | 150.271 | 458.239 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 10.031 | 22.511 | 164.095 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 10.039 | 123.071 | 735.231 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 9.943 | 212.607 | 651.775 |
| A-7 | 800 | 8 | 300 | 19820 | 19820 | 0 | 9.991 | 20.623 | 35.807 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 9.919 | 21.967 | 145.023 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 9.919 | 21.295 | 35.647 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 10.071 | 20.655 | 37.663 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 10.071 | 23.695 | 35.135 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 10.007 | 23.311 | 238.975 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 10.031 | 22.447 | 41.951 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 10.135 | 22.767 | 35.999 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 9.975 | 22.015 | 36.895 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 9.943 | 21.263 | 34.079 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 10.071 | 21.279 | 35.295 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.071 | 22.943 | 37.951 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.023 | 22.927 | 38.527 |
