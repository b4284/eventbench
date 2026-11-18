# pgmq-fifo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 6.105s, closing 0.059s, drain 1.108s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 44879 | 44879 | 4959.775 | 4987.55 | 12830.1 | 0 | 0 | 0 | 4 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 25495 | 17.48 | 11.527 | 22.879 | 205.183 | 544.767 | 695.807 |
| e2e_from_send | 25495 | 13.47 | 10.295 | 20.543 | 67.967 | 201.343 | 681.471 |
| producer_lag | 25495 | 4.011 | 0.859 | 2.639 | 101.823 | 367.615 | 462.079 |
| publish_call | 21904 | 2.168 | 1.757 | 2.553 | 13.103 | 60.255 | 450.303 |
| queue_wait | 25495 | 6.306 | 4.991 | 7.731 | 39.999 | 113.151 | 528.895 |
| broker_dwell | 25495 | 5.112 | 3.741 | 6.503 | 38.463 | 112.191 | 466.175 |
| receive_call | 18136 | 2.576 | 2.001 | 3.651 | 16.023 | 24.111 | 127.743 |
| ctx_load | 25495 | 0.499 | 0.391 | 0.86 | 1.684 | 4.279 | 7.667 |
| process | 25495 | 0.004 | 0.003 | 0.005 | 0.008 | 0.033 | 4.919 |
| ctx_commit | 25495 | 1.619 | 1.401 | 2.159 | 4.119 | 20.511 | 90.111 |
| ack | 25495 | 1.659 | 1.509 | 2.227 | 4.151 | 17.871 | 22.895 |
| service | 25495 | 7.165 | 4.723 | 13.495 | 34.847 | 123.647 | 184.447 |
| e2e_large | 4 | 601.6 | 585.215 | 695.807 | 695.807 | 695.807 | 695.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4987.55 | 6384.8 |
| applied_total | 22659.75 | 29509 |
| bench_cpu_cores | 2.681 | 3.482 |
| bench_cpu_pct | 11.172 | 14.51 |
| bench_gc_gen0 | 5.25 | 6 |
| bench_gc_gen1 | 0.75 | 1 |
| bench_gc_gen2 | 0.75 | 1 |
| bench_gc_heap_mb | 348.65 | 439.4 |
| bench_mem_mb | 438.175 | 486.3 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 25 | 25 |
| broker_backlog | 47.5 | 63 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 13.5 | 18 |
| ctr_eb_pgbouncer_cpu_pct | 6.202 | 6.39 |
| ctr_eb_pgbouncer_mem_mb | 13.225 | 13.6 |
| ctr_eb_postgres_cpu_pct | 70.905 | 73.01 |
| ctr_eb_postgres_mem_mb | 1060.95 | 1068.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 38.218 | 47.62 |
| in_flight | 30.75 | 50 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.992 | 100 |
| pg_conn_active | 21.75 | 23 |
| pg_conn_idle | 43.25 | 47 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 70.525 | 105.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24219.925 | 24428.6 |
| pg_tup_del_per_s | 8212.775 | 8364.6 |
| pg_tup_fetch_per_s | 98017.05 | 109135.3 |
| pg_tup_ins_per_s | 18594.475 | 20640 |
| pg_tup_upd_per_s | 9544.675 | 9730 |
| pg_wal_mb_per_s | 23.878 | 24.2 |
| pgb_cl_active | 228 | 228 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 17.75 | 23 |
| pgb_sv_idle | 46.25 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4959.775 | 6256.7 |
| produced_total | 22690.5 | 29542 |
| publish_mb_per_s | 10.88 | 20.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 16,474 records are Completed |
| record_counts | pass | 16,474 records found, 16,474 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 4 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 44,879 events applied by workers, 44,879 generated |
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
| A-0 | 800 | 8 | 300 | 2997 | 2997 | 0 | 12.255 | 362.239 | 553.471 |
| A-1 | 800 | 8 | 300 | 3000 | 3000 | 0 | 11.735 | 37.279 | 60.639 |
| A-2 | 800 | 8 | 300 | 3138 | 3138 | 0 | 11.399 | 37.951 | 56.287 |
| A-3 | 800 | 8 | 300 | 3077 | 3077 | 0 | 11.775 | 39.231 | 60.895 |
| A-4 | 800 | 8 | 300 | 3096 | 3096 | 0 | 11.679 | 37.183 | 62.111 |
| A-5 | 800 | 8 | 300 | 3093 | 3093 | 0 | 11.319 | 44.319 | 60.735 |
| A-6 | 800 | 8 | 300 | 2990 | 2990 | 0 | 11.479 | 36.415 | 54.783 |
| A-7 | 800 | 8 | 300 | 3003 | 3003 | 0 | 11.615 | 38.879 | 53.567 |
| A-8 | 800 | 8 | 300 | 3126 | 3126 | 0 | 11.383 | 37.439 | 62.335 |
| A-9 | 800 | 8 | 300 | 3069 | 3069 | 0 | 11.215 | 33.375 | 54.399 |
| B-0 | 800 | 8 | 100 | 1453 | 1453 | 0 | 11.791 | 622.591 | 695.807 |
| B-1 | 800 | 8 | 100 | 1401 | 1401 | 0 | 11.415 | 35.487 | 39.359 |
| B-2 | 800 | 8 | 100 | 1446 | 1446 | 0 | 11.223 | 36.319 | 42.239 |
| B-3 | 800 | 8 | 100 | 1425 | 1425 | 0 | 11.023 | 29.311 | 33.535 |
| B-4 | 800 | 8 | 100 | 1424 | 1424 | 0 | 11.903 | 549.375 | 585.215 |
| B-5 | 800 | 8 | 100 | 1348 | 1348 | 0 | 11.759 | 32.863 | 39.487 |
| B-6 | 800 | 8 | 100 | 1448 | 1448 | 0 | 11.343 | 32.303 | 42.655 |
| B-7 | 800 | 8 | 100 | 1381 | 1381 | 0 | 11.111 | 32.159 | 41.759 |
| B-8 | 800 | 8 | 100 | 1459 | 1459 | 0 | 11.671 | 419.327 | 587.775 |
| B-9 | 800 | 8 | 100 | 1505 | 1505 | 0 | 11.479 | 35.359 | 44.031 |
