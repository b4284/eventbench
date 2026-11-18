# pgmq-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 12.101s, closing 0.082s, drain 1.11s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 74471 | 74471 | 4401.525 | 4405.888 | 14500.7 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 39552 | 9.172 | 8.503 | 12.095 | 23.391 | 92.351 | 257.151 |
| e2e_from_send | 39552 | 8.038 | 7.407 | 10.895 | 20.943 | 40.319 | 63.583 |
| producer_lag | 39552 | 1.134 | 0.736 | 2.403 | 4.651 | 50.527 | 214.399 |
| publish_call | 40746 | 2.163 | 1.626 | 2.579 | 14.447 | 40.799 | 500.223 |
| queue_wait | 39552 | 5.112 | 4.975 | 6.659 | 15.143 | 25.983 | 48.671 |
| broker_dwell | 39552 | 4.08 | 3.889 | 5.619 | 14.095 | 24.911 | 47.519 |
| receive_call | 32505 | 2.688 | 1.897 | 3.961 | 19.151 | 36.991 | 140.159 |
| ctx_load | 39552 | 0.37 | 0.311 | 0.552 | 1.152 | 2.199 | 5.467 |
| process | 39552 | 0.002 | 0.001 | 0.003 | 0.007 | 0.022 | 1.851 |
| ctx_commit | 39552 | 1.405 | 1.252 | 1.885 | 2.953 | 14.399 | 19.023 |
| service | 39552 | 2.926 | 2.087 | 5.271 | 11.183 | 21.359 | 35.327 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4405.888 | 4859.7 |
| applied_total | 40739.75 | 55568 |
| bench_cpu_cores | 2.056 | 2.448 |
| bench_cpu_pct | 8.566 | 10.2 |
| bench_gc_gen0 | 3.625 | 5 |
| bench_gc_gen1 | 0.125 | 1 |
| bench_gc_gen2 | 0.125 | 1 |
| bench_gc_heap_mb | 992.162 | 1020.4 |
| bench_mem_mb | 896.725 | 937.6 |
| bench_threadpool_queue | 4.25 | 32 |
| bench_threadpool_threads | 25 | 25 |
| broker_backlog | 128.625 | 821 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 63.5 | 450 |
| ctr_eb_pgbouncer_cpu_pct | 10.691 | 11.16 |
| ctr_eb_pgbouncer_mem_mb | 12.925 | 14.8 |
| ctr_eb_postgres_cpu_pct | 120.798 | 126.46 |
| ctr_eb_postgres_mem_mb | 1415.15 | 1421 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.538 | 32.46 |
| in_flight | 24.125 | 35 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 21.5 | 56 |
| pg_conn_idle | 43.75 | 54 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.375 | 1 |
| pg_conn_total | 65.25 | 66 |
| pg_db_size_mb | 280.175 | 342.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23280.325 | 23652 |
| pg_tup_del_per_s | 15654.85 | 27928.6 |
| pg_tup_fetch_per_s | 100601.3 | 154647.8 |
| pg_tup_ins_per_s | 23203.65 | 43082.5 |
| pg_tup_upd_per_s | 12127.962 | 12985.6 |
| pg_wal_mb_per_s | 45.915 | 87.48 |
| pgb_cl_active | 257 | 257 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 28.875 | 56 |
| pgb_sv_idle | 35.125 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4401.525 | 4861.7 |
| produced_total | 40763.875 | 55582 |
| publish_mb_per_s | 2.272 | 2.51 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 22,596 records are Completed |
| record_counts | pass | 22,596 records found, 22,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 74,471 events applied by workers, 74,471 generated |
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
| A-0 | 800 | 8 | 300 | 4953 | 4953 | 0 | 8.551 | 22.383 | 63.903 |
| A-1 | 800 | 8 | 300 | 4944 | 4944 | 0 | 8.527 | 24.799 | 41.791 |
| A-2 | 800 | 8 | 300 | 5108 | 5108 | 0 | 8.479 | 20.943 | 37.471 |
| A-3 | 800 | 8 | 300 | 5112 | 5112 | 0 | 8.575 | 45.375 | 136.959 |
| A-4 | 800 | 8 | 300 | 5061 | 5061 | 0 | 8.343 | 22.255 | 31.183 |
| A-5 | 800 | 8 | 300 | 4978 | 4978 | 0 | 8.383 | 22.655 | 31.039 |
| A-6 | 800 | 8 | 300 | 4970 | 4970 | 0 | 8.407 | 22.719 | 32.863 |
| A-7 | 800 | 8 | 300 | 5080 | 5080 | 0 | 8.391 | 20.767 | 32.367 |
| A-8 | 800 | 8 | 300 | 5005 | 5005 | 0 | 8.295 | 22.415 | 28.879 |
| A-9 | 800 | 8 | 300 | 4985 | 4985 | 0 | 8.631 | 22.335 | 41.631 |
| B-0 | 800 | 8 | 100 | 2406 | 2406 | 0 | 8.839 | 179.967 | 257.151 |
| B-1 | 800 | 8 | 100 | 2436 | 2436 | 0 | 8.783 | 27.215 | 37.983 |
| B-2 | 800 | 8 | 100 | 2410 | 2410 | 0 | 8.543 | 18.991 | 28.575 |
| B-3 | 800 | 8 | 100 | 2459 | 2459 | 0 | 8.431 | 20.975 | 34.783 |
| B-4 | 800 | 8 | 100 | 2419 | 2419 | 0 | 8.463 | 23.887 | 29.631 |
| B-5 | 800 | 8 | 100 | 2377 | 2377 | 0 | 8.519 | 22.799 | 32.319 |
| B-6 | 800 | 8 | 100 | 2491 | 2491 | 0 | 8.679 | 22.239 | 29.407 |
| B-7 | 800 | 8 | 100 | 2453 | 2453 | 0 | 8.487 | 20.847 | 26.255 |
| B-8 | 800 | 8 | 100 | 2383 | 2383 | 0 | 8.727 | 22.351 | 28.687 |
| B-9 | 800 | 8 | 100 | 2441 | 2441 | 0 | 8.623 | 21.423 | 32.895 |
