# 3-fifo-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.145s, drain 2.614s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276549 | 276549 | 4008.845 | 4008.941 | 11154.5 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200494 | 10.894 | 10.231 | 15.183 | 23.199 | 36.671 | 159.743 |
| e2e_from_send | 200494 | 9.937 | 9.079 | 14.215 | 22.143 | 33.727 | 142.975 |
| producer_lag | 200494 | 0.957 | 0.719 | 2.333 | 3.481 | 7.747 | 82.815 |
| publish_call | 201143 | 1.633 | 1.527 | 2.187 | 3.309 | 9.183 | 65.375 |
| queue_wait | 200494 | 5.042 | 4.947 | 6.799 | 12.487 | 21.631 | 101.631 |
| broker_dwell | 200494 | 3.887 | 3.743 | 5.647 | 11.231 | 20.447 | 88.767 |
| receive_call | 148812 | 2.05 | 1.722 | 2.413 | 9.759 | 41.279 | 102.271 |
| ctx_load | 200494 | 0.384 | 0.32 | 0.586 | 1.14 | 2.535 | 26.671 |
| process | 200494 | 0.002 | 0.001 | 0.002 | 0.004 | 0.015 | 32.751 |
| ctx_commit | 200494 | 1.28 | 1.16 | 1.736 | 2.645 | 4.835 | 21.647 |
| ack | 200494 | 1.321 | 1.237 | 1.904 | 2.777 | 4.883 | 21.743 |
| service | 200494 | 4.895 | 3.619 | 8.871 | 16.223 | 25.967 | 122.943 |
| e2e_large | 7 | 88.535 | 84.927 | 116.159 | 145.535 | 145.535 | 145.535 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.941 | 4116.7 |
| applied_total | 151750.592 | 247886 |
| bench_cpu_cores | 2.008 | 2.097 |
| bench_cpu_pct | 8.367 | 8.74 |
| bench_gc_gen0 | 4.776 | 6 |
| bench_gc_gen1 | 3.449 | 6 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 49.886 | 71 |
| bench_mem_mb | 159.096 | 198.9 |
| bench_threadpool_queue | 0.898 | 24 |
| bench_threadpool_threads | 22.367 | 25 |
| broker_backlog | 31.408 | 50 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 8.082 | 16 |
| ctr_eb_pgbouncer_cpu_pct | 16.102 | 16.31 |
| ctr_eb_pgbouncer_mem_mb | 12.99 | 15 |
| ctr_eb_postgres_cpu_pct | 184.677 | 186.91 |
| ctr_eb_postgres_mem_mb | 2129.969 | 2141 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 26.732 | 31.86 |
| in_flight | 26.98 | 49 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 17 | 34 |
| pg_conn_idle | 48.224 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.224 | 66 |
| pg_db_size_mb | 206.082 | 238.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25192.565 | 28513.6 |
| pg_tup_del_per_s | 4047.084 | 4583.4 |
| pg_tup_fetch_per_s | 77485.551 | 102493.2 |
| pg_tup_ins_per_s | 5482.506 | 10363 |
| pg_tup_upd_per_s | 10643.092 | 12055.9 |
| pg_wal_mb_per_s | 12.489 | 21.79 |
| pgb_cl_active | 238 | 238 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 21.367 | 48 |
| pgb_sv_idle | 42.633 | 59 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.845 | 4118.7 |
| produced_total | 151777.571 | 247899 |
| publish_mb_per_s | 2.068 | 2.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,676 records are Completed |
| record_counts | pass | 52,676 records found, 52,676 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,549 events applied by workers, 276,549 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 10.223 | 23.215 | 59.423 |
| A-1 | 800 | 8 | 300 | 20008 | 20008 | 0 | 10.199 | 22.431 | 42.495 |
| A-2 | 800 | 8 | 300 | 20143 | 20143 | 0 | 10.223 | 23.423 | 43.039 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 10.199 | 23.567 | 92.159 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 10.303 | 24.527 | 143.359 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 10.247 | 24.783 | 159.743 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 10.199 | 24.575 | 116.159 |
| A-7 | 800 | 8 | 300 | 19820 | 19820 | 0 | 10.151 | 22.351 | 41.247 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 10.255 | 23.295 | 51.743 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 10.223 | 22.383 | 37.631 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 10.255 | 22.191 | 40.095 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 10.239 | 22.927 | 33.951 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 10.279 | 23.503 | 48.479 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 10.263 | 22.959 | 35.423 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 10.327 | 23.567 | 38.207 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 10.247 | 21.839 | 40.159 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 10.263 | 22.991 | 38.783 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 10.215 | 23.983 | 46.367 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.319 | 21.903 | 36.607 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 10.207 | 22.943 | 34.687 |
