# 4-fifo-txack-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.309s, drain 1.918s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276552 | 276552 | 4008.92 | 4008.818 | 15973.1 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200497 | 8.646 | 8.271 | 11.567 | 18.495 | 30.463 | 147.327 |
| e2e_from_send | 200497 | 7.67 | 7.191 | 10.503 | 16.719 | 27.263 | 131.327 |
| producer_lag | 200497 | 0.976 | 0.705 | 2.373 | 3.563 | 12.975 | 99.391 |
| publish_call | 200936 | 1.583 | 1.474 | 2.079 | 3.413 | 15.263 | 221.055 |
| queue_wait | 200497 | 4.939 | 4.851 | 6.567 | 12.831 | 21.999 | 109.503 |
| broker_dwell | 200497 | 4.072 | 3.947 | 5.687 | 11.887 | 20.927 | 49.471 |
| receive_call | 147507 | 2.009 | 1.687 | 2.331 | 10.535 | 42.495 | 107.519 |
| ctx_load | 200497 | 0.354 | 0.304 | 0.499 | 1.047 | 2.841 | 8.091 |
| process | 200497 | 0.002 | 0.001 | 0.002 | 0.004 | 0.013 | 31.119 |
| ctx_commit | 200497 | 1.293 | 1.18 | 1.718 | 2.797 | 5.003 | 22.959 |
| service | 200497 | 2.732 | 1.924 | 5.007 | 9.151 | 16.991 | 48.319 |
| e2e_large | 7 | 109.289 | 105.023 | 139.519 | 147.327 | 147.327 | 147.327 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.818 | 4119.1 |
| applied_total | 151741.857 | 247880 |
| bench_cpu_cores | 1.857 | 1.953 |
| bench_cpu_pct | 7.737 | 8.14 |
| bench_gc_gen0 | 4.551 | 6 |
| bench_gc_gen1 | 3.469 | 5 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 47.898 | 65.2 |
| bench_mem_mb | 161.522 | 201.8 |
| bench_threadpool_queue | 1.184 | 36 |
| bench_threadpool_threads | 20.98 | 27 |
| broker_backlog | 28.898 | 77 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 11.551 | 43 |
| ctr_eb_pgbouncer_cpu_pct | 10.341 | 10.59 |
| ctr_eb_pgbouncer_mem_mb | 13.235 | 15.2 |
| ctr_eb_postgres_cpu_pct | 118.708 | 121.49 |
| ctr_eb_postgres_mem_mb | 2362.712 | 2428.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 24.245 | 27.98 |
| in_flight | 23.551 | 38 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.102 | 30 |
| pg_conn_idle | 48.98 | 59 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.082 | 66 |
| pg_db_size_mb | 204.861 | 240.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21261.533 | 24013 |
| pg_tup_del_per_s | 4051.692 | 4593.6 |
| pg_tup_fetch_per_s | 69344.455 | 84357 |
| pg_tup_ins_per_s | 5619.137 | 12333.1 |
| pg_tup_upd_per_s | 10660.694 | 12042.7 |
| pg_wal_mb_per_s | 12.26 | 25.7 |
| pgb_cl_active | 224 | 224 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 13.612 | 39 |
| pgb_sv_idle | 50.388 | 62 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.92 | 4124.1 |
| produced_total | 151765.408 | 247894 |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 8.171 | 16.767 | 105.023 |
| A-1 | 800 | 8 | 300 | 20008 | 20008 | 0 | 8.223 | 16.655 | 32.735 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 8.167 | 15.823 | 29.055 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 8.155 | 17.887 | 114.239 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.255 | 16.735 | 32.399 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.175 | 17.391 | 147.327 |
| A-6 | 800 | 8 | 300 | 19947 | 19947 | 0 | 8.139 | 19.279 | 139.519 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 8.123 | 16.191 | 29.391 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 8.179 | 16.223 | 32.799 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 8.127 | 16.375 | 29.119 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.567 | 21.135 | 35.487 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.551 | 20.143 | 35.839 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.599 | 21.007 | 90.303 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.575 | 21.743 | 32.863 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.567 | 20.847 | 42.527 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.551 | 21.407 | 31.311 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.631 | 20.927 | 34.207 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.439 | 20.623 | 31.919 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.535 | 20.031 | 33.567 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.479 | 21.215 | 36.255 |
