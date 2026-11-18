# 5-partitioned-txack-claimcheck — PASS ✅

- Broker: `pgmq partitioned (=workers per group), vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.102s, closing 0.087s, drain 1.326s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276550 | 276550 | 4008.859 | 4008.714 | 20223.4 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200496 | 17.591 | 12.463 | 36.671 | 55.935 | 64.223 | 190.207 |
| e2e_from_send | 200496 | 16.559 | 11.311 | 35.423 | 54.943 | 59.167 | 179.839 |
| producer_lag | 200496 | 1.032 | 0.748 | 2.413 | 3.821 | 14.263 | 106.111 |
| publish_call | 198727 | 1.733 | 1.591 | 2.295 | 4.211 | 16.039 | 129.727 |
| queue_wait | 200496 | 14.239 | 9.135 | 33.471 | 53.183 | 55.359 | 143.743 |
| broker_dwell | 200496 | 13.191 | 7.971 | 32.287 | 51.967 | 54.239 | 68.863 |
| receive_call | 190805 | 1.985 | 1.811 | 2.499 | 5.219 | 18.447 | 25.551 |
| ctx_load | 200496 | 0.376 | 0.318 | 0.515 | 1.279 | 2.961 | 8.823 |
| process | 200496 | 0.002 | 0.001 | 0.002 | 0.004 | 0.014 | 41.311 |
| ctx_commit | 200496 | 1.358 | 1.212 | 1.842 | 2.905 | 13.759 | 24.223 |
| service | 200496 | 2.32 | 1.668 | 4.107 | 8.255 | 16.911 | 53.151 |
| e2e_large | 7 | 126.167 | 109.119 | 181.631 | 190.207 | 190.207 | 190.207 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.714 | 4127.4 |
| applied_total | 151722.633 | 247857 |
| bench_cpu_cores | 2.068 | 2.174 |
| bench_cpu_pct | 8.618 | 9.06 |
| bench_gc_gen0 | 3.98 | 6 |
| bench_gc_gen1 | 3.388 | 5 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 56.62 | 80.5 |
| bench_mem_mb | 174.163 | 206.7 |
| bench_threadpool_queue | 4.878 | 80 |
| bench_threadpool_threads | 24.776 | 26 |
| broker_backlog | 58.551 | 94 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 27.857 | 40 |
| ctr_eb_pgbouncer_cpu_pct | 7.092 | 7.45 |
| ctr_eb_pgbouncer_mem_mb | 13.337 | 14 |
| ctr_eb_postgres_cpu_pct | 81.505 | 85.44 |
| ctr_eb_postgres_mem_mb | 1971.818 | 1996 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.064 | 31.69 |
| in_flight | 59.02 | 87 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 15.082 | 41 |
| pg_conn_idle | 49.959 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.041 | 66 |
| pg_db_size_mb | 218.463 | 253.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 28043.006 | 33910.5 |
| pg_tup_del_per_s | 4146.61 | 5072.5 |
| pg_tup_fetch_per_s | 70883.839 | 153148.9 |
| pg_tup_ins_per_s | 5635.661 | 10339 |
| pg_tup_upd_per_s | 10908.867 | 13046.1 |
| pg_wal_mb_per_s | 12.189 | 20.91 |
| pgb_cl_active | 313 | 313 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 17.633 | 32 |
| pgb_sv_idle | 46.367 | 62 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.859 | 4134.4 |
| produced_total | 151781.653 | 247906 |
| publish_mb_per_s | 2.067 | 2.13 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,675 records are Completed |
| record_counts | pass | 52,675 records found, 52,675 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,550 events applied by workers, 276,550 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 11.095 | 54.271 | 90.431 |
| A-1 | 800 | 8 | 300 | 20006 | 20006 | 0 | 11.039 | 53.791 | 67.391 |
| A-2 | 800 | 8 | 300 | 20144 | 20144 | 0 | 11.047 | 53.055 | 64.031 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 10.991 | 54.431 | 119.103 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 11.231 | 53.599 | 81.983 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 11.087 | 54.591 | 181.631 |
| A-6 | 800 | 8 | 300 | 19947 | 19947 | 0 | 11.039 | 55.583 | 190.207 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 11.287 | 54.399 | 71.871 |
| A-8 | 800 | 8 | 300 | 20124 | 20124 | 0 | 11.055 | 53.887 | 67.007 |
| A-9 | 800 | 8 | 300 | 19952 | 19952 | 0 | 11.087 | 54.047 | 71.103 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 19.407 | 57.375 | 68.671 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 19.151 | 57.663 | 99.839 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 19.823 | 57.471 | 97.343 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 19.247 | 57.439 | 73.983 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 19.743 | 57.919 | 71.103 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 19.375 | 57.823 | 66.751 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 19.583 | 57.503 | 65.663 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 18.703 | 57.695 | 68.351 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 18.847 | 57.311 | 84.543 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 19.391 | 57.471 | 69.311 |
