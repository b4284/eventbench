# 2-fifo-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.285s, drain 1.814s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276549 | 276549 | 4008.855 | 4009.053 | 16307.9 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200495 | 9.238 | 8.215 | 11.351 | 18.079 | 279.295 | 671.743 |
| e2e_from_send | 200495 | 7.745 | 7.151 | 10.343 | 16.719 | 69.119 | 655.871 |
| producer_lag | 200495 | 1.493 | 0.701 | 2.365 | 3.553 | 227.967 | 473.087 |
| publish_call | 201553 | 1.55 | 1.447 | 2.011 | 3.125 | 6.847 | 462.079 |
| queue_wait | 200495 | 5.027 | 4.843 | 6.523 | 13.319 | 52.191 | 550.399 |
| broker_dwell | 200495 | 4.081 | 3.857 | 5.571 | 12.303 | 51.199 | 515.583 |
| receive_call | 147601 | 1.977 | 1.66 | 2.233 | 9.031 | 46.719 | 120.191 |
| ctx_load | 200495 | 0.35 | 0.301 | 0.495 | 1.016 | 2.679 | 7.859 |
| process | 200495 | 0.002 | 0.001 | 0.002 | 0.004 | 0.016 | 2.969 |
| ctx_commit | 200495 | 1.276 | 1.171 | 1.695 | 2.631 | 4.787 | 71.679 |
| service | 200495 | 2.718 | 1.9 | 4.935 | 9.095 | 20.351 | 119.231 |
| e2e_large | 7 | 437.605 | 432.127 | 623.615 | 671.743 | 671.743 | 671.743 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4009.053 | 4184.1 |
| applied_total | 151717.898 | 247862 |
| bench_cpu_cores | 1.865 | 2.008 |
| bench_cpu_pct | 7.769 | 8.37 |
| bench_gc_gen0 | 3.898 | 6 |
| bench_gc_gen1 | 2.49 | 5 |
| bench_gc_gen2 | 0.102 | 1 |
| bench_gc_heap_mb | 958.904 | 1133.1 |
| bench_mem_mb | 729.878 | 862 |
| bench_threadpool_queue | 2.163 | 27 |
| bench_threadpool_threads | 18.959 | 25 |
| broker_backlog | 23.286 | 43 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 7.837 | 16 |
| ctr_eb_pgbouncer_cpu_pct | 4.269 | 4.61 |
| ctr_eb_pgbouncer_mem_mb | 13.237 | 15 |
| ctr_eb_postgres_cpu_pct | 49.505 | 53.27 |
| ctr_eb_postgres_mem_mb | 1443.437 | 1452.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 24.019 | 31 |
| in_flight | 21.735 | 34 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 12.592 | 31 |
| pg_conn_idle | 52.531 | 63 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.061 | 1 |
| pg_conn_total | 65.122 | 66 |
| pg_db_size_mb | 222.424 | 284.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21237.233 | 25567.8 |
| pg_tup_del_per_s | 5120.635 | 14664.5 |
| pg_tup_fetch_per_s | 72085.708 | 98712.7 |
| pg_tup_ins_per_s | 6718.276 | 23176 |
| pg_tup_upd_per_s | 10638.327 | 12805.3 |
| pg_wal_mb_per_s | 14.62 | 48.28 |
| pgb_cl_active | 231 | 231 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 15.551 | 48 |
| pgb_sv_idle | 48.449 | 63 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.855 | 4180.1 |
| produced_total | 151739.633 | 247879 |
| publish_mb_per_s | 3.941 | 33.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 8.131 | 16.831 | 356.607 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 8.103 | 14.983 | 28.031 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 8.167 | 15.663 | 27.727 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 8.131 | 136.063 | 439.295 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.123 | 15.199 | 42.303 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.111 | 82.687 | 671.743 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 8.179 | 175.231 | 623.615 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 8.027 | 14.919 | 26.431 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 8.063 | 14.799 | 31.391 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 8.075 | 14.647 | 27.359 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.463 | 20.735 | 35.967 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.455 | 20.319 | 26.399 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.487 | 19.631 | 196.863 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.407 | 19.583 | 33.439 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.503 | 19.711 | 30.287 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.519 | 19.839 | 33.311 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.495 | 19.471 | 30.671 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.479 | 19.599 | 33.503 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.591 | 20.079 | 34.815 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.527 | 20.127 | 31.279 |
