# 4-fifo-txack-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.162s, drain 2.112s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276552 | 276552 | 4008.939 | 4008.767 | 14780.7 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200497 | 8.624 | 8.247 | 11.519 | 18.991 | 33.887 | 142.463 |
| e2e_from_send | 200497 | 7.654 | 7.179 | 10.463 | 17.167 | 27.023 | 128.831 |
| producer_lag | 200497 | 0.97 | 0.704 | 2.359 | 3.549 | 13.119 | 91.711 |
| publish_call | 202020 | 1.56 | 1.456 | 2.067 | 3.189 | 13.375 | 99.327 |
| queue_wait | 200497 | 4.939 | 4.851 | 6.575 | 13.079 | 21.375 | 100.223 |
| broker_dwell | 200497 | 4.682 | 4.555 | 6.315 | 12.735 | 20.991 | 41.631 |
| receive_call | 148255 | 1.993 | 1.676 | 2.317 | 10.487 | 43.871 | 84.095 |
| ctx_load | 200497 | 0.351 | 0.302 | 0.496 | 1.023 | 2.579 | 9.351 |
| process | 200497 | 0.002 | 0.001 | 0.002 | 0.004 | 0.018 | 26.383 |
| ctx_commit | 200497 | 1.29 | 1.17 | 1.73 | 2.761 | 4.783 | 27.311 |
| service | 200497 | 2.715 | 1.933 | 4.967 | 9.039 | 17.135 | 43.423 |
| e2e_large | 7 | 90.489 | 79.231 | 120.575 | 142.463 | 142.463 | 142.463 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.767 | 4138 |
| applied_total | 151722.612 | 247863 |
| bench_cpu_cores | 1.854 | 1.942 |
| bench_cpu_pct | 7.723 | 8.09 |
| bench_gc_gen0 | 4.347 | 6 |
| bench_gc_gen1 | 2.51 | 5 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 48.745 | 66.2 |
| bench_mem_mb | 163.802 | 203.7 |
| bench_threadpool_queue | 1.082 | 30 |
| bench_threadpool_threads | 19.49 | 25 |
| broker_backlog | 23.367 | 34 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 8.755 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 6.163 | 6.47 |
| ctr_eb_pgbouncer_mem_mb | 13.449 | 15.5 |
| ctr_eb_postgres_cpu_pct | 70.982 | 74.35 |
| ctr_eb_postgres_mem_mb | 1700.918 | 1709.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 23.559 | 27.14 |
| in_flight | 24.551 | 96 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 10.755 | 25 |
| pg_conn_idle | 54.245 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 205.647 | 239.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21190.208 | 24284.1 |
| pg_tup_del_per_s | 4031.524 | 4663.3 |
| pg_tup_fetch_per_s | 69596.476 | 91314.2 |
| pg_tup_ins_per_s | 5596.724 | 12487.1 |
| pg_tup_upd_per_s | 10609.094 | 12199.8 |
| pg_wal_mb_per_s | 12.202 | 25.86 |
| pgb_cl_active | 235 | 235 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 16.531 | 31 |
| pgb_sv_idle | 47.469 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.939 | 4125.3 |
| produced_total | 151747.163 | 247880 |
| publish_mb_per_s | 2.068 | 2.13 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,676 records are Completed |
| record_counts | pass | 52,676 records found, 52,676 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,552 events applied by workers, 276,552 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 8.159 | 18.159 | 57.471 |
| A-1 | 800 | 8 | 300 | 20008 | 20008 | 0 | 8.099 | 17.455 | 36.479 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 8.163 | 16.703 | 43.551 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 8.123 | 18.159 | 90.047 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.175 | 16.847 | 48.767 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.143 | 17.375 | 120.575 |
| A-6 | 800 | 8 | 300 | 19947 | 19947 | 0 | 8.199 | 18.719 | 142.463 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 8.071 | 16.831 | 38.623 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 8.123 | 16.783 | 52.991 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 8.199 | 15.823 | 43.807 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.575 | 20.575 | 36.383 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.471 | 20.303 | 37.727 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.567 | 21.599 | 73.919 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.583 | 21.327 | 38.335 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.551 | 21.199 | 47.871 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.527 | 20.623 | 44.095 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.543 | 20.575 | 29.903 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.503 | 21.071 | 33.151 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.559 | 22.351 | 38.175 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.551 | 22.015 | 38.303 |
