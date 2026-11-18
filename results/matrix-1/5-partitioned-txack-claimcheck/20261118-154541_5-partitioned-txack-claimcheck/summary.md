# 5-partitioned-txack-claimcheck — PASS ✅

- Broker: `pgmq partitioned (=workers per group), vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 60.101s, closing 0.244s, drain 1.02s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 276548 | 276548 | 4008.786 | 4008.927 | 20947.5 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200493 | 17.402 | 12.303 | 36.415 | 55.679 | 59.519 | 176.255 |
| e2e_from_send | 200493 | 16.431 | 11.255 | 35.295 | 54.783 | 57.311 | 165.119 |
| producer_lag | 200493 | 0.971 | 0.735 | 2.343 | 3.485 | 5.727 | 95.487 |
| publish_call | 200227 | 1.674 | 1.584 | 2.259 | 3.289 | 7.667 | 213.887 |
| queue_wait | 200493 | 14.155 | 9.079 | 33.407 | 53.119 | 54.783 | 131.199 |
| broker_dwell | 200493 | 13.571 | 8.391 | 32.671 | 52.383 | 54.175 | 60.895 |
| receive_call | 191247 | 1.914 | 1.818 | 2.461 | 4.235 | 7.407 | 25.775 |
| ctx_load | 200493 | 0.376 | 0.32 | 0.512 | 1.27 | 2.351 | 10.383 |
| process | 200493 | 0.002 | 0.001 | 0.002 | 0.004 | 0.017 | 31.999 |
| ctx_commit | 200493 | 1.327 | 1.201 | 1.854 | 2.789 | 4.699 | 13.991 |
| service | 200493 | 2.276 | 1.671 | 4.107 | 7.751 | 11.967 | 42.303 |
| e2e_large | 7 | 109.173 | 90.623 | 172.799 | 176.255 | 176.255 | 176.255 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4008.927 | 4120.1 |
| applied_total | 151720.918 | 247859 |
| bench_cpu_cores | 2.103 | 2.242 |
| bench_cpu_pct | 8.76 | 9.34 |
| bench_gc_gen0 | 4.122 | 6 |
| bench_gc_gen1 | 2.98 | 5 |
| bench_gc_gen2 | 0.735 | 2 |
| bench_gc_heap_mb | 52.402 | 72.8 |
| bench_mem_mb | 171.624 | 198 |
| bench_threadpool_queue | 2.592 | 45 |
| bench_threadpool_threads | 23.347 | 25 |
| broker_backlog | 59.612 | 80 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 32.184 | 47 |
| ctr_eb_pgbouncer_cpu_pct | 11.1 | 11.4 |
| ctr_eb_pgbouncer_mem_mb | 13.941 | 16.2 |
| ctr_eb_postgres_cpu_pct | 127.465 | 130.74 |
| ctr_eb_postgres_mem_mb | 2395.89 | 2419.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.865 | 32.05 |
| in_flight | 58.653 | 83 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 14.571 | 29 |
| pg_conn_idle | 50.429 | 65 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 217.353 | 250.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26576.947 | 34118.4 |
| pg_tup_del_per_s | 3921.984 | 5104.1 |
| pg_tup_fetch_per_s | 68053.896 | 157803.1 |
| pg_tup_ins_per_s | 5196.08 | 13372.7 |
| pg_tup_upd_per_s | 10320.343 | 13088.4 |
| pg_wal_mb_per_s | 10.627 | 27.02 |
| pgb_cl_active | 310 | 310 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 16.776 | 44 |
| pgb_sv_idle | 47.224 | 61 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4008.786 | 4118.3 |
| produced_total | 151779.571 | 247903 |
| publish_mb_per_s | 2.068 | 2.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 52,676 records are Completed |
| record_counts | pass | 52,676 records found, 52,676 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 276,548 events applied by workers, 276,548 generated |
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
| A-0 | 800 | 8 | 300 | 19851 | 19851 | 0 | 11.087 | 53.535 | 74.303 |
| A-1 | 800 | 8 | 300 | 20008 | 20008 | 0 | 11.007 | 52.959 | 64.383 |
| A-2 | 800 | 8 | 300 | 20143 | 20143 | 0 | 10.943 | 53.087 | 60.095 |
| A-3 | 800 | 8 | 300 | 20307 | 20307 | 0 | 10.799 | 54.335 | 113.407 |
| A-4 | 800 | 8 | 300 | 20175 | 20175 | 0 | 10.999 | 53.887 | 68.607 |
| A-5 | 800 | 8 | 300 | 19967 | 19967 | 0 | 11.151 | 54.303 | 172.799 |
| A-6 | 800 | 8 | 300 | 19947 | 19947 | 0 | 10.959 | 54.719 | 176.255 |
| A-7 | 800 | 8 | 300 | 19821 | 19821 | 0 | 11.119 | 54.015 | 60.799 |
| A-8 | 800 | 8 | 300 | 20122 | 20122 | 0 | 10.903 | 53.695 | 61.759 |
| A-9 | 800 | 8 | 300 | 19951 | 19951 | 0 | 11.071 | 54.175 | 62.335 |
| B-0 | 800 | 8 | 100 | 7612 | 7612 | 0 | 19.151 | 57.471 | 62.815 |
| B-1 | 800 | 8 | 100 | 7610 | 7610 | 0 | 18.559 | 57.343 | 70.079 |
| B-2 | 800 | 8 | 100 | 7524 | 7524 | 0 | 19.711 | 57.471 | 61.247 |
| B-3 | 800 | 8 | 100 | 7567 | 7567 | 0 | 19.279 | 57.247 | 60.735 |
| B-4 | 800 | 8 | 100 | 7598 | 7598 | 0 | 19.471 | 57.151 | 63.423 |
| B-5 | 800 | 8 | 100 | 7680 | 7680 | 0 | 18.623 | 57.407 | 60.671 |
| B-6 | 800 | 8 | 100 | 7724 | 7724 | 0 | 18.751 | 57.183 | 67.135 |
| B-7 | 800 | 8 | 100 | 7651 | 7651 | 0 | 18.911 | 57.151 | 60.671 |
| B-8 | 800 | 8 | 100 | 7645 | 7645 | 0 | 19.039 | 57.567 | 65.599 |
| B-9 | 800 | 8 | 100 | 7645 | 7645 | 0 | 19.231 | 57.311 | 67.647 |
