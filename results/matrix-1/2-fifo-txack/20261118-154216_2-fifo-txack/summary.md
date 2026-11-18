# 2-fifo-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.165s, drain 1.41s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276549 | 276549 | 4008.802 | 4008.986 | 17876.1 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200495 | 9.789 | 8.351 | 11.895 | 24.751 | 357.119 | 744.959 |
| e2e_from_send | 200495 | 8.103 | 7.263 | 10.775 | 22.287 | 81.471 | 724.479 |
| producer_lag | 200495 | 1.686 | 0.724 | 2.417 | 5.019 | 277.759 | 523.775 |
| publish_call | 199689 | 1.651 | 1.496 | 2.101 | 3.689 | 19.567 | 514.559 |
| queue_wait | 200495 | 5.212 | 4.903 | 6.647 | 17.263 | 59.519 | 579.583 |
| broker_dwell | 200495 | 4.344 | 3.989 | 5.783 | 16.287 | 58.495 | 547.839 |
| receive_call | 147082 | 1.961 | 1.719 | 2.339 | 6.159 | 36.191 | 169.983 |
| ctx_load | 200495 | 0.356 | 0.305 | 0.512 | 1.027 | 2.551 | 37.887 |
| process | 200495 | 0.002 | 0.001 | 0.002 | 0.004 | 0.019 | 3.153 |
| ctx_commit | 200495 | 1.341 | 1.207 | 1.761 | 2.793 | 14.679 | 82.303 |
| service | 200495 | 2.891 | 1.981 | 5.135 | 11.071 | 27.679 | 178.687 |
| e2e_large | 7 | 494.711 | 461.055 | 670.207 | 737.279 | 737.279 | 737.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.986 | 4265.6 |
| applied_total | 151747.592 | 247889 |
| bench_cpu_cores | 1.909 | 2.075 |
| bench_cpu_pct | 7.954 | 8.65 |
| bench_gc_gen0 | 4 | 6 |
| bench_gc_gen1 | 2.755 | 5 |
| bench_gc_gen2 | 0.143 | 2 |
| bench_gc_heap_mb | 839.671 | 1041.8 |
| bench_mem_mb | 719.541 | 864.4 |
| bench_threadpool_queue | 0.796 | 16 |
| bench_threadpool_threads | 23.122 | 35 |
| broker_backlog | 27.429 | 81 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 8.143 | 15 |
| ctr_eb_pgbouncer_cpu_pct | 8.816 | 9.09 |
| ctr_eb_pgbouncer_mem_mb | 13.353 | 15.2 |
| ctr_eb_postgres_cpu_pct | 101.269 | 104.31 |
| ctr_eb_postgres_mem_mb | 1972.82 | 2061.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 24.568 | 36.32 |
| in_flight | 26.776 | 66 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 13.367 | 26 |
| pg_conn_idle | 51.653 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.02 | 66 |
| pg_db_size_mb | 226.722 | 257.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21158.527 | 24625 |
| pg_tup_del_per_s | 5248.492 | 14764.9 |
| pg_tup_fetch_per_s | 77642.565 | 163187.8 |
| pg_tup_ins_per_s | 6807.314 | 23330.6 |
| pg_tup_upd_per_s | 10630.457 | 12361.7 |
| pg_wal_mb_per_s | 14.884 | 49.77 |
| pgb_cl_active | 231 | 231 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 12.51 | 27 |
| pgb_sv_idle | 51.49 | 63 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.802 | 4232.6 |
| produced_total | 151774.367 | 247904 |
| publish_mb_per_s | 3.939 | 33.12 |

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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 8.311 | 26.591 | 397.567 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 8.231 | 21.679 | 70.847 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 8.239 | 22.031 | 59.327 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 8.263 | 238.847 | 670.207 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.271 | 21.327 | 177.791 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.311 | 83.711 | 744.959 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 8.295 | 177.023 | 616.447 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 8.123 | 21.263 | 78.719 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 8.239 | 21.711 | 75.903 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 8.271 | 22.143 | 64.831 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.679 | 22.543 | 36.895 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.695 | 22.431 | 37.759 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.663 | 24.847 | 210.431 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.631 | 23.199 | 36.447 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.559 | 24.047 | 44.063 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.639 | 22.879 | 36.127 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.623 | 23.295 | 45.759 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.615 | 23.343 | 41.343 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.615 | 22.543 | 38.719 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.567 | 23.951 | 35.807 |
