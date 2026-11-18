# pool-256 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 60.102s, closing 0.541s, drain 5.428s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 551677 | 551677 | 7989.292 | 7989.392 | 19622.7 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 399380 | 11.727 | 10.767 | 17.247 | 27.935 | 42.943 | 76.095 |
| e2e_from_send | 399380 | 10.565 | 9.495 | 16.007 | 26.527 | 41.119 | 73.855 |
| producer_lag | 399380 | 1.162 | 1.013 | 2.455 | 4.021 | 6.607 | 27.919 |
| publish_call | 284767 | 2.644 | 2.261 | 3.975 | 8.535 | 15.367 | 657.919 |
| queue_wait | 399380 | 5.03 | 4.735 | 7.031 | 10.463 | 15.247 | 53.151 |
| broker_dwell | 399380 | 3.77 | 3.477 | 5.707 | 8.951 | 13.535 | 51.967 |
| receive_call | 249172 | 4.581 | 2.539 | 5.447 | 13.359 | 526.847 | 1339.391 |
| ctx_load | 399380 | 0.814 | 0.658 | 1.49 | 2.683 | 4.447 | 31.023 |
| process | 399380 | 0.001 | 0.001 | 0.002 | 0.004 | 0.019 | 4.091 |
| ctx_commit | 399380 | 2.15 | 2.039 | 3.039 | 4.763 | 7.159 | 14.943 |
| service | 399380 | 5.536 | 4.163 | 10.759 | 20.671 | 33.503 | 66.559 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7989.392 | 8189.1 |
| applied_total | 302598.551 | 494445 |
| bench_cpu_cores | 2.542 | 2.698 |
| bench_cpu_pct | 10.59 | 11.24 |
| bench_gc_gen0 | 3.51 | 5 |
| bench_gc_gen1 | 2.388 | 4 |
| bench_gc_gen2 | 0.02 | 1 |
| bench_gc_heap_mb | 854.994 | 929.9 |
| bench_mem_mb | 794.555 | 868.9 |
| bench_threadpool_queue | 2.796 | 32 |
| bench_threadpool_threads | 24.184 | 27 |
| broker_backlog | 66.959 | 135 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 6.837 | 11 |
| ctr_eb_pgbouncer_cpu_pct | 77.312 | 79.3 |
| ctr_eb_pgbouncer_mem_mb | 15.943 | 17.1 |
| ctr_eb_postgres_cpu_pct | 1060.118 | 1398.74 |
| ctr_eb_postgres_mem_mb | 2457.773 | 2469.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.052 | 63.27 |
| in_flight | 63.755 | 97 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 28.184 | 46 |
| pg_conn_idle | 163.143 | 181 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 191.327 | 192 |
| pg_db_size_mb | 322.361 | 431.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36495.214 | 39951.1 |
| pg_tup_del_per_s | 8146.998 | 9939.2 |
| pg_tup_fetch_per_s | 190658.053 | 219325.4 |
| pg_tup_ins_per_s | 9545.09 | 13117 |
| pg_tup_upd_per_s | 21117.8 | 22812 |
| pg_wal_mb_per_s | 21.28 | 27.95 |
| pgb_cl_active | 425 | 425 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 48.755 | 88 |
| pgb_sv_idle | 113.184 | 167 |
| pgb_sv_used | 28.061 | 67 |
| produced_per_s | 7989.292 | 8214.1 |
| produced_total | 302662.306 | 494521 |
| publish_mb_per_s | 4.122 | 4.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 105,164 records are Completed |
| record_counts | pass | 105,164 records found, 105,164 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 551,677 events applied by workers, 551,677 generated |
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
| A-0 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40008 | 40008 | 0 | 10.943 | 28.799 | 62.719 |
| A-1 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40120 | 40120 | 0 | 10.943 | 29.439 | 62.463 |
| A-2 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39306 | 39306 | 0 | 10.943 | 28.927 | 66.047 |
| A-3 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39875 | 39875 | 0 | 10.879 | 29.439 | 68.607 |
| A-4 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40171 | 40171 | 0 | 10.815 | 29.439 | 66.559 |
| A-5 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40079 | 40079 | 0 | 10.879 | 28.799 | 70.655 |
| A-6 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40015 | 40015 | 0 | 10.943 | 29.439 | 72.191 |
| A-7 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40065 | 40065 | 0 | 10.815 | 29.311 | 66.047 |
| A-8 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40127 | 40127 | 0 | 10.879 | 28.671 | 76.287 |
| A-9 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40434 | 40434 | 0 | 10.879 | 29.055 | 74.239 |
| B-0 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15052 | 15052 | 0 | 10.495 | 23.423 | 39.679 |
| B-1 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.495 | 22.527 | 38.655 |
| B-2 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15083 | 15083 | 0 | 10.559 | 22.911 | 38.655 |
| B-3 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15232 | 15232 | 0 | 10.559 | 22.399 | 42.751 |
| B-4 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15207 | 15207 | 0 | 10.623 | 22.143 | 35.583 |
| B-5 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.559 | 22.527 | 40.191 |
| B-6 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15074 | 15074 | 0 | 10.559 | 22.015 | 41.727 |
| B-7 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15345 | 15345 | 0 | 10.623 | 22.783 | 41.471 |
| B-8 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15167 | 15167 | 0 | 10.495 | 22.271 | 38.911 |
| B-9 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15107 | 15107 | 0 | 10.559 | 22.527 | 44.799 |
