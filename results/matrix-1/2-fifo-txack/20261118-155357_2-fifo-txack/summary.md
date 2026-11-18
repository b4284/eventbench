# 2-fifo-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.149s, drain 2.31s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276549 | 276549 | 4008.908 | 4008.906 | 13138.4 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200495 | 9.117 | 8.163 | 11.327 | 18.351 | 251.263 | 657.919 |
| e2e_from_send | 200495 | 7.705 | 7.123 | 10.303 | 17.087 | 55.807 | 647.167 |
| producer_lag | 200495 | 1.412 | 0.697 | 2.345 | 3.541 | 203.263 | 446.975 |
| publish_call | 201393 | 1.567 | 1.451 | 2.081 | 3.185 | 7.631 | 441.599 |
| queue_wait | 200495 | 4.995 | 4.827 | 6.523 | 13.415 | 40.671 | 493.823 |
| broker_dwell | 200495 | 4.623 | 4.407 | 6.135 | 12.959 | 40.351 | 459.775 |
| receive_call | 147581 | 2.071 | 1.66 | 2.339 | 12.871 | 57.119 | 132.351 |
| ctx_load | 200495 | 0.351 | 0.302 | 0.494 | 1.019 | 2.113 | 7.751 |
| process | 200495 | 0.001 | 0.001 | 0.002 | 0.004 | 0.018 | 2.267 |
| ctx_commit | 200495 | 1.266 | 1.157 | 1.688 | 2.637 | 4.515 | 78.655 |
| service | 200495 | 2.71 | 1.894 | 4.903 | 8.991 | 19.103 | 169.727 |
| e2e_large | 7 | 426.981 | 392.703 | 625.151 | 657.919 | 657.919 | 657.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.906 | 4191.5 |
| applied_total | 151734.714 | 247876 |
| bench_cpu_cores | 1.9 | 2.067 |
| bench_cpu_pct | 7.915 | 8.61 |
| bench_gc_gen0 | 3.959 | 5 |
| bench_gc_gen1 | 2.327 | 4 |
| bench_gc_gen2 | 0.082 | 1 |
| bench_gc_heap_mb | 931.086 | 1064.8 |
| bench_mem_mb | 792.594 | 892.3 |
| bench_threadpool_queue | 0.469 | 13 |
| bench_threadpool_threads | 20.265 | 25 |
| broker_backlog | 22.714 | 37 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 9.245 | 18 |
| ctr_eb_pgbouncer_cpu_pct | 15.57 | 15.75 |
| ctr_eb_pgbouncer_mem_mb | 13.437 | 16 |
| ctr_eb_postgres_cpu_pct | 178.343 | 180.38 |
| ctr_eb_postgres_mem_mb | 2062.659 | 2152.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 24.504 | 31.66 |
| in_flight | 24.102 | 110 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 10.388 | 25 |
| pg_conn_idle | 54.612 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 221.359 | 281.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21272.557 | 25359.7 |
| pg_tup_del_per_s | 5265.433 | 14450.3 |
| pg_tup_fetch_per_s | 73368.053 | 106444.7 |
| pg_tup_ins_per_s | 6830.606 | 22971.1 |
| pg_tup_upd_per_s | 10645.031 | 12682.4 |
| pg_wal_mb_per_s | 14.823 | 47.81 |
| pgb_cl_active | 230 | 230 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 15.837 | 57 |
| pgb_sv_idle | 48.163 | 60 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.908 | 4185.5 |
| produced_total | 151758.816 | 247892 |
| publish_mb_per_s | 3.941 | 44.69 |

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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 8.071 | 17.231 | 324.863 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 8.031 | 14.927 | 27.375 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 8.083 | 15.279 | 36.223 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 8.091 | 105.983 | 434.687 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 8.067 | 15.151 | 37.119 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 8.107 | 50.335 | 657.919 |
| A-6 | 800 | 8 | 300 | 19946 | 19946 | 0 | 8.079 | 155.263 | 625.151 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 7.979 | 15.223 | 31.807 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 8.059 | 15.007 | 37.663 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 8.063 | 14.679 | 36.255 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 8.463 | 20.239 | 32.799 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 8.495 | 19.567 | 31.711 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 8.375 | 21.151 | 212.863 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 8.375 | 19.583 | 31.711 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 8.495 | 20.063 | 37.951 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 8.455 | 20.383 | 34.047 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 8.519 | 20.431 | 47.135 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 8.439 | 19.263 | 29.471 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.463 | 19.967 | 33.663 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 8.367 | 20.895 | 34.207 |
