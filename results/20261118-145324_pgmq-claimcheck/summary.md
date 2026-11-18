# pgmq-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 12.101s, closing 0.173s, drain 1.608s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 74473 | 74473 | 4401.85 | 4403.85 | 12113.4 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 39554 | 11.686 | 10.879 | 16.543 | 26.207 | 37.087 | 54.175 |
| e2e_from_send | 39554 | 10.689 | 9.703 | 15.503 | 24.655 | 35.743 | 54.111 |
| producer_lag | 39554 | 0.997 | 0.772 | 2.375 | 3.597 | 11.495 | 14.991 |
| publish_call | 42563 | 1.998 | 1.749 | 2.533 | 6.019 | 29.167 | 78.463 |
| queue_wait | 39554 | 5.147 | 5.031 | 6.911 | 13.255 | 20.127 | 35.039 |
| broker_dwell | 39554 | 3.96 | 3.775 | 5.739 | 11.911 | 19.007 | 34.015 |
| receive_call | 33441 | 2.978 | 1.947 | 3.129 | 25.663 | 121.983 | 157.183 |
| ctx_load | 39554 | 0.444 | 0.365 | 0.712 | 1.398 | 2.385 | 5.547 |
| process | 39554 | 0.002 | 0.001 | 0.003 | 0.005 | 0.024 | 5.223 |
| ctx_commit | 39554 | 1.437 | 1.302 | 1.95 | 2.949 | 14.487 | 19.407 |
| ack | 39554 | 1.493 | 1.422 | 2.089 | 2.891 | 5.171 | 22.879 |
| service | 39554 | 5.542 | 4.053 | 10.015 | 18.783 | 29.023 | 38.879 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4403.85 | 4846.7 |
| applied_total | 40736 | 55563 |
| bench_cpu_cores | 2.102 | 2.37 |
| bench_cpu_pct | 8.76 | 9.88 |
| bench_gc_gen0 | 5.375 | 6 |
| bench_gc_gen1 | 0.625 | 1 |
| bench_gc_gen2 | 0.625 | 1 |
| bench_gc_heap_mb | 45.512 | 54.3 |
| bench_mem_mb | 185.025 | 207.8 |
| bench_threadpool_queue | 0.375 | 1 |
| bench_threadpool_threads | 25 | 25 |
| broker_backlog | 33.875 | 43 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 8.125 | 10 |
| ctr_eb_pgbouncer_cpu_pct | 12.685 | 13.24 |
| ctr_eb_pgbouncer_mem_mb | 12.075 | 13.2 |
| ctr_eb_postgres_cpu_pct | 144.006 | 150.01 |
| ctr_eb_postgres_mem_mb | 1663.575 | 1665.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 30.828 | 36.5 |
| in_flight | 26.5 | 43 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 32 | 63 |
| pg_conn_idle | 33 | 46 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 162.838 | 176 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27325.425 | 33345 |
| pg_tup_del_per_s | 4987.775 | 7619 |
| pg_tup_fetch_per_s | 189506.437 | 832011 |
| pg_tup_ins_per_s | 13813.625 | 24927.2 |
| pg_tup_upd_per_s | 11924.412 | 15298.6 |
| pg_wal_mb_per_s | 26.086 | 39.43 |
| pgb_cl_active | 206 | 206 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 20.375 | 26 |
| pgb_sv_idle | 43.625 | 49 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4401.85 | 4861.9 |
| produced_total | 40762.5 | 55582 |
| publish_mb_per_s | 2.272 | 2.51 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 22,596 records are Completed |
| record_counts | pass | 22,596 records found, 22,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 74,473 events applied by workers, 74,473 generated |
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
| A-0 | 800 | 8 | 300 | 4953 | 4953 | 0 | 11.063 | 25.567 | 35.199 |
| A-1 | 800 | 8 | 300 | 4944 | 4944 | 0 | 10.999 | 26.415 | 42.079 |
| A-2 | 800 | 8 | 300 | 5110 | 5110 | 0 | 10.775 | 28.319 | 41.727 |
| A-3 | 800 | 8 | 300 | 5112 | 5112 | 0 | 11.039 | 25.839 | 38.079 |
| A-4 | 800 | 8 | 300 | 5061 | 5061 | 0 | 10.919 | 27.039 | 43.071 |
| A-5 | 800 | 8 | 300 | 4978 | 4978 | 0 | 10.967 | 27.007 | 41.247 |
| A-6 | 800 | 8 | 300 | 4970 | 4970 | 0 | 10.671 | 26.255 | 38.591 |
| A-7 | 800 | 8 | 300 | 5080 | 5080 | 0 | 11.007 | 27.935 | 44.383 |
| A-8 | 800 | 8 | 300 | 5005 | 5005 | 0 | 10.807 | 25.743 | 38.815 |
| A-9 | 800 | 8 | 300 | 4985 | 4985 | 0 | 10.951 | 29.247 | 43.039 |
| B-0 | 800 | 8 | 100 | 2406 | 2406 | 0 | 10.775 | 24.895 | 37.695 |
| B-1 | 800 | 8 | 100 | 2436 | 2436 | 0 | 10.871 | 25.439 | 31.935 |
| B-2 | 800 | 8 | 100 | 2410 | 2410 | 0 | 10.855 | 27.423 | 46.047 |
| B-3 | 800 | 8 | 100 | 2459 | 2459 | 0 | 10.783 | 23.407 | 30.031 |
| B-4 | 800 | 8 | 100 | 2419 | 2419 | 0 | 11.015 | 22.335 | 29.167 |
| B-5 | 800 | 8 | 100 | 2377 | 2377 | 0 | 10.671 | 22.847 | 54.175 |
| B-6 | 800 | 8 | 100 | 2491 | 2491 | 0 | 10.831 | 21.791 | 27.023 |
| B-7 | 800 | 8 | 100 | 2453 | 2453 | 0 | 10.799 | 27.807 | 43.519 |
| B-8 | 800 | 8 | 100 | 2383 | 2383 | 0 | 10.791 | 24.495 | 29.519 |
| B-9 | 800 | 8 | 100 | 2441 | 2441 | 0 | 10.767 | 24.863 | 32.095 |
