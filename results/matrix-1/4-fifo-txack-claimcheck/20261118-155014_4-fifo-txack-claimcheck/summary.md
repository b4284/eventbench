# 4-fifo-txack-claimcheck — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.286s, drain 1.821s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276545 | 276545 | 4008.943 | 4008.798 | 16723.6 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200491 | 8.433 | 8.123 | 11.231 | 16.879 | 32.687 | 117.311 |
| e2e_from_send | 200491 | 7.501 | 7.083 | 10.231 | 15.591 | 27.967 | 105.343 |
| producer_lag | 200491 | 0.932 | 0.69 | 2.331 | 3.459 | 5.947 | 75.391 |
| publish_call | 202431 | 1.509 | 1.428 | 1.955 | 3.021 | 7.027 | 235.775 |
| queue_wait | 200491 | 4.843 | 4.771 | 6.435 | 12.039 | 21.599 | 69.119 |
| broker_dwell | 200491 | 3.827 | 3.711 | 5.411 | 10.943 | 20.351 | 34.143 |
| receive_call | 148295 | 1.906 | 1.624 | 2.185 | 9.103 | 35.839 | 80.319 |
| ctx_load | 200491 | 0.357 | 0.308 | 0.498 | 1.042 | 2.929 | 11.991 |
| process | 200491 | 0.002 | 0.001 | 0.002 | 0.004 | 0.014 | 33.695 |
| ctx_commit | 200491 | 1.255 | 1.161 | 1.639 | 2.547 | 4.511 | 15.999 |
| service | 200491 | 2.658 | 1.874 | 4.895 | 8.655 | 13.671 | 49.791 |
| e2e_large | 7 | 83.774 | 77.311 | 104.703 | 117.311 | 117.311 | 117.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.798 | 4116.8 |
| applied_total | 151755.98 | 247887 |
| bench_cpu_cores | 1.933 | 2.009 |
| bench_cpu_pct | 8.056 | 8.37 |
| bench_gc_gen0 | 4.347 | 6 |
| bench_gc_gen1 | 3.592 | 5 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 50.976 | 79 |
| bench_mem_mb | 159.412 | 184.7 |
| bench_threadpool_queue | 1.939 | 27 |
| bench_threadpool_threads | 24.49 | 28 |
| broker_backlog | 24.163 | 42 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 7.633 | 14 |
| ctr_eb_pgbouncer_cpu_pct | 13.817 | 14.03 |
| ctr_eb_pgbouncer_mem_mb | 14.01 | 15.8 |
| ctr_eb_postgres_cpu_pct | 158.517 | 160.93 |
| ctr_eb_postgres_mem_mb | 2325.735 | 2338.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 24.87 | 28.23 |
| in_flight | 22.837 | 41 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 9.653 | 20 |
| pg_conn_idle | 55.408 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.061 | 66 |
| pg_db_size_mb | 205.116 | 236.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21143.09 | 23563.8 |
| pg_tup_del_per_s | 4022.955 | 4525.6 |
| pg_tup_fetch_per_s | 68978.441 | 83472.3 |
| pg_tup_ins_per_s | 5480.565 | 9297.1 |
| pg_tup_upd_per_s | 10584.824 | 11818.7 |
| pg_wal_mb_per_s | 12.795 | 27.18 |
| pgb_cl_active | 233 | 233 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 16.878 | 43 |
| pgb_sv_idle | 47.122 | 61 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.943 | 4124.8 |
| produced_total | 151778.816 | 247903 |
| publish_mb_per_s | 2.068 | 2.13 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,545 events applied by workers, 276,545 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 7.987 | 14.783 | 58.239 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 8.019 | 14.935 | 40.127 |
| A-2 | 800 | 8 | 300 | 20143 | 20143 | 0 | 8.059 | 14.759 | 37.119 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 7.979 | 15.527 | 77.311 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.091 | 15.263 | 39.487 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.075 | 15.183 | 104.703 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 8.007 | 15.383 | 117.311 |
| A-7 | 800 | 8 | 300 | 19820 | 19820 | 0 | 7.951 | 14.487 | 35.999 |
| A-8 | 800 | 8 | 300 | 20123 | 20123 | 0 | 8.007 | 14.951 | 43.935 |
| A-9 | 800 | 8 | 300 | 19951 | 19951 | 0 | 8.015 | 14.679 | 38.591 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.455 | 21.087 | 37.919 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.455 | 20.767 | 35.999 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.447 | 20.079 | 76.031 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.375 | 19.711 | 35.135 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.527 | 20.863 | 45.183 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.399 | 19.647 | 36.575 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.423 | 20.575 | 36.863 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.399 | 19.775 | 37.439 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.455 | 20.015 | 29.871 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.399 | 20.511 | 35.103 |
