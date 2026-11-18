# pool-256 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 60.102s, closing 0.67s, drain 7.243s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 551673 | 551673 | 7988.831 | 7988.471 | 16658.9 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 399378 | 12.141 | 11.071 | 18.015 | 29.967 | 47.679 | 153.215 |
| e2e_from_send | 399378 | 10.943 | 9.759 | 16.751 | 28.431 | 44.863 | 152.959 |
| producer_lag | 399378 | 1.198 | 1.039 | 2.517 | 4.207 | 7.847 | 20.047 |
| publish_call | 279368 | 2.737 | 2.305 | 4.167 | 9.319 | 18.527 | 524.287 |
| queue_wait | 399378 | 5.083 | 4.791 | 7.103 | 10.447 | 17.055 | 49.343 |
| broker_dwell | 399378 | 3.656 | 3.363 | 5.587 | 8.727 | 15.159 | 48.063 |
| receive_call | 245037 | 5.799 | 2.583 | 5.659 | 23.231 | 679.423 | 1960.959 |
| ctx_load | 399378 | 0.884 | 0.712 | 1.629 | 2.997 | 4.671 | 23.151 |
| process | 399378 | 0.002 | 0.001 | 0.002 | 0.004 | 0.02 | 4.131 |
| ctx_commit | 399378 | 2.219 | 2.109 | 3.157 | 4.839 | 7.503 | 47.871 |
| service | 399378 | 5.86 | 4.379 | 11.455 | 22.479 | 36.479 | 149.119 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7988.471 | 8230.6 |
| applied_total | 302613.204 | 494445 |
| bench_cpu_cores | 2.499 | 2.666 |
| bench_cpu_pct | 10.414 | 11.11 |
| bench_gc_gen0 | 3.408 | 5 |
| bench_gc_gen1 | 2.776 | 4 |
| bench_gc_gen2 | 0.02 | 1 |
| bench_gc_heap_mb | 983.959 | 1204.9 |
| bench_mem_mb | 862.12 | 1013.7 |
| bench_threadpool_queue | 2.122 | 43 |
| bench_threadpool_threads | 24.02 | 25 |
| broker_backlog | 67.061 | 93 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 5.755 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 78.025 | 83.57 |
| ctr_eb_pgbouncer_mem_mb | 16.692 | 19 |
| ctr_eb_postgres_cpu_pct | 1052.944 | 1320.8 |
| ctr_eb_postgres_mem_mb | 2256.018 | 2263.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 56.736 | 62.87 |
| in_flight | 69.122 | 142 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 34.367 | 62 |
| pg_conn_idle | 161.878 | 181 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 196.245 | 197 |
| pg_db_size_mb | 312.353 | 354.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36172.518 | 39319.4 |
| pg_tup_del_per_s | 8133.822 | 9928.5 |
| pg_tup_fetch_per_s | 195046.39 | 222249.6 |
| pg_tup_ins_per_s | 9519.08 | 12363.7 |
| pg_tup_upd_per_s | 21064.469 | 22792.2 |
| pg_wal_mb_per_s | 21.302 | 26.43 |
| pgb_cl_active | 435 | 435 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 38.306 | 82 |
| pgb_sv_idle | 127.286 | 182 |
| pgb_sv_used | 29.408 | 74 |
| produced_per_s | 7988.831 | 8227.6 |
| produced_total | 302682.327 | 494531 |
| publish_mb_per_s | 4.121 | 4.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 105,163 records are Completed |
| record_counts | pass | 105,163 records found, 105,163 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 551,673 events applied by workers, 551,673 generated |
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
| A-0 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40008 | 40008 | 0 | 11.199 | 30.591 | 75.775 |
| A-1 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40119 | 40119 | 0 | 11.327 | 31.487 | 71.679 |
| A-2 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39307 | 39307 | 0 | 11.135 | 30.591 | 71.679 |
| A-3 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39872 | 39872 | 0 | 11.263 | 32.127 | 64.511 |
| A-4 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40171 | 40171 | 0 | 11.263 | 31.487 | 69.119 |
| A-5 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40079 | 40079 | 0 | 11.263 | 32.127 | 153.599 |
| A-6 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40014 | 40014 | 0 | 11.263 | 30.719 | 69.631 |
| A-7 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40065 | 40065 | 0 | 11.071 | 30.719 | 72.191 |
| A-8 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40127 | 40127 | 0 | 11.263 | 30.975 | 66.559 |
| A-9 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40434 | 40434 | 0 | 11.327 | 33.023 | 80.383 |
| B-0 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15052 | 15052 | 0 | 10.751 | 23.295 | 49.919 |
| B-1 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.815 | 22.271 | 46.335 |
| B-2 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15083 | 15083 | 0 | 10.815 | 23.039 | 47.359 |
| B-3 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15232 | 15232 | 0 | 10.815 | 23.679 | 45.311 |
| B-4 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15207 | 15207 | 0 | 10.815 | 23.039 | 58.879 |
| B-5 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.815 | 23.935 | 40.703 |
| B-6 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15074 | 15074 | 0 | 10.879 | 23.423 | 43.775 |
| B-7 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15345 | 15345 | 0 | 10.687 | 23.807 | 56.831 |
| B-8 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15167 | 15167 | 0 | 10.815 | 24.575 | 53.759 |
| B-9 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15107 | 15107 | 0 | 10.751 | 23.551 | 53.503 |
