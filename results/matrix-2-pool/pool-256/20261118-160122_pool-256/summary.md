# pool-256 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 60.104s, closing 0.897s, drain 9.147s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 551670 | 551670 | 7989.512 | 7989.514 | 13600.7 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 399375 | 11.503 | 10.623 | 16.783 | 26.623 | 38.751 | 83.199 |
| e2e_from_send | 399375 | 10.363 | 9.367 | 15.615 | 25.327 | 37.119 | 83.135 |
| producer_lag | 399375 | 1.141 | 0.996 | 2.419 | 3.847 | 6.151 | 20.287 |
| publish_call | 288316 | 2.573 | 2.229 | 3.735 | 8.191 | 19.567 | 570.879 |
| queue_wait | 399375 | 4.951 | 4.667 | 6.907 | 9.983 | 14.295 | 41.343 |
| broker_dwell | 399375 | 4.319 | 4.037 | 6.207 | 9.119 | 13.335 | 39.487 |
| receive_call | 250556 | 5.955 | 2.503 | 5.079 | 68.671 | 592.383 | 1708.031 |
| ctx_load | 399375 | 0.799 | 0.651 | 1.452 | 2.569 | 4.111 | 15.071 |
| process | 399375 | 0.002 | 0.001 | 0.002 | 0.004 | 0.018 | 5.215 |
| ctx_commit | 399375 | 2.104 | 2.005 | 2.957 | 4.431 | 6.247 | 22.047 |
| service | 399375 | 5.413 | 4.083 | 10.487 | 19.919 | 31.071 | 78.207 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7989.514 | 8233 |
| applied_total | 302570.776 | 494430 |
| bench_cpu_cores | 2.544 | 2.652 |
| bench_cpu_pct | 10.601 | 11.05 |
| bench_gc_gen0 | 3.653 | 5 |
| bench_gc_gen1 | 3 | 4 |
| bench_gc_gen2 | 0.041 | 1 |
| bench_gc_heap_mb | 768.227 | 851 |
| bench_mem_mb | 707.082 | 765.1 |
| bench_threadpool_queue | 2.286 | 43 |
| bench_threadpool_threads | 22.939 | 26 |
| broker_backlog | 60.592 | 90 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 6.571 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 77.136 | 78.47 |
| ctr_eb_pgbouncer_mem_mb | 14.706 | 16.3 |
| ctr_eb_postgres_cpu_pct | 1041.004 | 1239.08 |
| ctr_eb_postgres_mem_mb | 2193.471 | 2205.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 56.009 | 62.86 |
| in_flight | 67.735 | 116 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 24.49 | 32 |
| pg_conn_idle | 172.776 | 184 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 197.265 | 198 |
| pg_db_size_mb | 309.306 | 345.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36549.269 | 41609.5 |
| pg_tup_del_per_s | 8097.541 | 9753.3 |
| pg_tup_fetch_per_s | 189701.437 | 220382.8 |
| pg_tup_ins_per_s | 9461.198 | 13116 |
| pg_tup_upd_per_s | 21068.069 | 23917.4 |
| pg_wal_mb_per_s | 23.754 | 80.53 |
| pgb_cl_active | 413 | 413 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 38.02 | 63 |
| pgb_sv_idle | 129.245 | 179 |
| pgb_sv_used | 28.735 | 63 |
| produced_per_s | 7989.512 | 8243 |
| produced_total | 302638.51 | 494505 |
| publish_mb_per_s | 4.123 | 4.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 105,163 records are Completed |
| record_counts | pass | 105,163 records found, 105,163 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 551,670 events applied by workers, 551,670 generated |
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
| A-0 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40008 | 40008 | 0 | 10.751 | 27.519 | 54.527 |
| A-1 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40119 | 40119 | 0 | 10.751 | 27.775 | 57.343 |
| A-2 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39306 | 39306 | 0 | 10.751 | 27.647 | 57.855 |
| A-3 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39873 | 39873 | 0 | 10.687 | 27.903 | 64.511 |
| A-4 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40172 | 40172 | 0 | 10.687 | 27.135 | 54.783 |
| A-5 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40076 | 40076 | 0 | 10.815 | 27.775 | 58.623 |
| A-6 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40013 | 40013 | 0 | 10.815 | 27.519 | 83.455 |
| A-7 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40065 | 40065 | 0 | 10.687 | 27.903 | 53.247 |
| A-8 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40127 | 40127 | 0 | 10.751 | 28.543 | 68.095 |
| A-9 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40434 | 40434 | 0 | 10.751 | 27.647 | 56.319 |
| B-0 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15052 | 15052 | 0 | 10.367 | 22.015 | 41.983 |
| B-1 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.431 | 22.143 | 54.783 |
| B-2 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15083 | 15083 | 0 | 10.495 | 22.015 | 46.079 |
| B-3 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15232 | 15232 | 0 | 10.495 | 21.631 | 42.751 |
| B-4 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15207 | 15207 | 0 | 10.431 | 22.399 | 47.359 |
| B-5 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.431 | 21.631 | 34.047 |
| B-6 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15074 | 15074 | 0 | 10.431 | 22.015 | 35.327 |
| B-7 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15345 | 15345 | 0 | 10.495 | 21.247 | 48.895 |
| B-8 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15167 | 15167 | 0 | 10.367 | 21.503 | 45.055 |
| B-9 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15107 | 15107 | 0 | 10.367 | 21.503 | 40.959 |
