# pool-64 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 60.101s, closing 0.886s, drain 8.131s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 551675 | 551675 | 7989.337 | 7989.327 | 17350 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 399377 | 10.982 | 10.215 | 15.879 | 24.399 | 35.935 | 75.583 |
| e2e_from_send | 399377 | 9.887 | 9.007 | 14.759 | 23.311 | 34.015 | 75.519 |
| producer_lag | 399377 | 1.095 | 0.952 | 2.331 | 3.723 | 6.207 | 16.183 |
| publish_call | 296824 | 2.423 | 2.131 | 3.479 | 7.023 | 13.903 | 529.919 |
| queue_wait | 399377 | 4.8 | 4.519 | 6.699 | 9.599 | 14.335 | 48.575 |
| broker_dwell | 399377 | 4.195 | 3.933 | 6.003 | 8.767 | 13.511 | 47.327 |
| receive_call | 253167 | 4.149 | 2.391 | 4.515 | 43.935 | 232.319 | 680.447 |
| ctx_load | 399377 | 0.748 | 0.609 | 1.361 | 2.339 | 3.801 | 13.551 |
| process | 399377 | 0.002 | 0.001 | 0.002 | 0.004 | 0.017 | 5.187 |
| ctx_commit | 399377 | 1.986 | 1.894 | 2.765 | 4.037 | 6.267 | 18.079 |
| service | 399377 | 5.087 | 3.865 | 9.775 | 18.287 | 28.095 | 65.727 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7989.327 | 8216.9 |
| applied_total | 302619.041 | 494478 |
| bench_cpu_cores | 2.524 | 2.729 |
| bench_cpu_pct | 10.518 | 11.37 |
| bench_gc_gen0 | 3.551 | 4 |
| bench_gc_gen1 | 2.98 | 4 |
| bench_gc_gen2 | 0.02 | 1 |
| bench_gc_heap_mb | 847.312 | 969.7 |
| bench_mem_mb | 756.273 | 914.8 |
| bench_threadpool_queue | 3.837 | 35 |
| bench_threadpool_threads | 28.408 | 29 |
| broker_backlog | 61.327 | 85 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 8.857 | 24 |
| ctr_eb_pgbouncer_cpu_pct | 77.073 | 80.39 |
| ctr_eb_pgbouncer_mem_mb | 15.457 | 16 |
| ctr_eb_postgres_cpu_pct | 1006.624 | 1084.38 |
| ctr_eb_postgres_mem_mb | 1451.498 | 1460.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 54.106 | 58.53 |
| in_flight | 62.878 | 100 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 22.551 | 46 |
| pg_conn_idle | 42.592 | 55 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.143 | 66 |
| pg_db_size_mb | 303.771 | 344 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36220.898 | 42472.1 |
| pg_tup_del_per_s | 7975.569 | 11839.9 |
| pg_tup_fetch_per_s | 182344.714 | 254096.5 |
| pg_tup_ins_per_s | 9321.724 | 15719.6 |
| pg_tup_upd_per_s | 20748.222 | 25128.7 |
| pg_wal_mb_per_s | 20.747 | 35.1 |
| pgb_cl_active | 422.714 | 424 |
| pgb_cl_waiting | 1.286 | 8 |
| pgb_maxwait_ms | 0.07 | 0.63 |
| pgb_sv_active | 40.51 | 64 |
| pgb_sv_idle | 23.49 | 49 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7989.337 | 8245.9 |
| produced_total | 302681.918 | 494548 |
| publish_mb_per_s | 4.122 | 4.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 105,164 records are Completed |
| record_counts | pass | 105,164 records found, 105,164 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 551,675 events applied by workers, 551,675 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| A-0 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40008 | 40008 | 0 | 10.303 | 24.831 | 66.559 |
| A-1 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40120 | 40120 | 0 | 10.303 | 25.471 | 72.191 |
| A-2 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39307 | 39307 | 0 | 10.239 | 25.087 | 48.895 |
| A-3 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39875 | 39875 | 0 | 10.303 | 25.855 | 66.047 |
| A-4 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40171 | 40171 | 0 | 10.367 | 25.471 | 56.575 |
| A-5 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40079 | 40079 | 0 | 10.303 | 25.471 | 75.775 |
| A-6 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40012 | 40012 | 0 | 10.303 | 25.471 | 67.071 |
| A-7 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40065 | 40065 | 0 | 10.239 | 24.959 | 68.095 |
| A-8 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40127 | 40127 | 0 | 10.367 | 25.727 | 58.111 |
| A-9 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40434 | 40434 | 0 | 10.303 | 25.215 | 75.775 |
| B-0 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15052 | 15052 | 0 | 10.047 | 19.711 | 34.303 |
| B-1 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.047 | 19.967 | 37.375 |
| B-2 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15083 | 15083 | 0 | 10.111 | 20.351 | 40.959 |
| B-3 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15232 | 15232 | 0 | 10.111 | 20.607 | 42.751 |
| B-4 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15207 | 15207 | 0 | 10.111 | 20.223 | 34.815 |
| B-5 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.175 | 19.839 | 43.007 |
| B-6 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15074 | 15074 | 0 | 10.175 | 20.095 | 40.959 |
| B-7 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15345 | 15345 | 0 | 10.111 | 20.351 | 41.471 |
| B-8 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15167 | 15167 | 0 | 10.175 | 20.351 | 40.447 |
| B-9 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15107 | 15107 | 0 | 10.047 | 19.583 | 39.423 |
