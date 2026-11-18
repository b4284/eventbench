# 5-partitioned-txack-claimcheck — PASS ✅

- Broker: `pgmq partitioned (=workers per group), vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.071s, drain 1.219s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276551 | 276551 | 4008.7 | 4008.99 | 21337.3 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200496 | 17.481 | 12.375 | 36.479 | 55.807 | 59.743 | 160.255 |
| e2e_from_send | 200496 | 16.497 | 11.295 | 35.327 | 54.879 | 57.599 | 151.423 |
| producer_lag | 200496 | 0.984 | 0.749 | 2.369 | 3.535 | 6.367 | 78.783 |
| publish_call | 199541 | 1.708 | 1.614 | 2.323 | 3.467 | 8.007 | 126.399 |
| queue_wait | 200496 | 14.17 | 9.071 | 33.375 | 53.087 | 54.975 | 121.855 |
| broker_dwell | 200496 | 13.589 | 8.399 | 32.671 | 52.383 | 54.335 | 65.599 |
| receive_call | 191419 | 1.96 | 1.848 | 2.513 | 4.323 | 10.399 | 28.799 |
| ctx_load | 200496 | 0.386 | 0.324 | 0.532 | 1.317 | 3.089 | 12.151 |
| process | 200496 | 0.002 | 0.001 | 0.002 | 0.004 | 0.015 | 30.127 |
| ctx_commit | 200496 | 1.354 | 1.224 | 1.915 | 2.883 | 5.135 | 22.415 |
| service | 200496 | 2.327 | 1.701 | 4.203 | 8.083 | 12.455 | 45.215 |
| e2e_large | 7 | 98.393 | 94.079 | 123.583 | 160.255 | 160.255 | 160.255 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.99 | 4110.9 |
| applied_total | 151729.367 | 247867 |
| bench_cpu_cores | 2.098 | 2.256 |
| bench_cpu_pct | 8.742 | 9.4 |
| bench_gc_gen0 | 3.918 | 5 |
| bench_gc_gen1 | 2.898 | 5 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 53.727 | 75.7 |
| bench_mem_mb | 175.165 | 219.1 |
| bench_threadpool_queue | 3.878 | 60 |
| bench_threadpool_threads | 23.959 | 27 |
| broker_backlog | 60.061 | 73 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 32.571 | 44 |
| ctr_eb_pgbouncer_cpu_pct | 14.46 | 14.71 |
| ctr_eb_pgbouncer_mem_mb | 13.471 | 14.5 |
| ctr_eb_postgres_cpu_pct | 165.799 | 168.52 |
| ctr_eb_postgres_mem_mb | 2348.514 | 2402.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.662 | 34.23 |
| in_flight | 58.143 | 77 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.673 | 33 |
| pg_conn_idle | 48.327 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 217.398 | 254.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 28028.929 | 33530.6 |
| pg_tup_del_per_s | 4140.994 | 4999.5 |
| pg_tup_fetch_per_s | 76345.71 | 165919.9 |
| pg_tup_ins_per_s | 5632.316 | 10295.3 |
| pg_tup_upd_per_s | 10899.11 | 12902 |
| pg_wal_mb_per_s | 11.524 | 21.26 |
| pgb_cl_active | 318 | 318 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 23.388 | 53 |
| pgb_sv_idle | 40.612 | 59 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.7 | 4111.1 |
| produced_total | 151787.51 | 247908 |
| publish_mb_per_s | 2.068 | 2.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,676 records are Completed |
| record_counts | pass | 52,676 records found, 52,676 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,551 events applied by workers, 276,551 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 11.135 | 54.047 | 70.911 |
| A-1 | 800 | 8 | 300 | 20008 | 20008 | 0 | 10.975 | 53.695 | 62.495 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 10.999 | 53.151 | 60.095 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 11.007 | 54.591 | 110.335 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 11.279 | 53.279 | 62.271 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 11.191 | 55.199 | 160.255 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 10.959 | 54.431 | 123.583 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 11.319 | 54.399 | 74.239 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 11.135 | 53.407 | 62.559 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 10.983 | 52.959 | 69.695 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 18.959 | 57.375 | 75.007 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 18.991 | 57.727 | 61.407 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 19.359 | 57.407 | 70.975 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 18.975 | 57.119 | 63.103 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 19.503 | 57.375 | 64.511 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 18.575 | 57.183 | 61.535 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 19.279 | 57.375 | 62.431 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 18.879 | 57.567 | 69.823 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 18.575 | 57.535 | 60.895 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 19.407 | 57.375 | 64.223 |
