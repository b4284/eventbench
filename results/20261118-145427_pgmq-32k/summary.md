# pgmq-32k — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 30.104s, closing 0.368s, drain 4.217s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 302109 | 302109 | 8158.071 | 8190.987 | 14646.6 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 204933 | 34.461 | 19.439 | 57.791 | 393.471 | 588.799 | 840.191 |
| e2e_from_send | 204933 | 31.503 | 17.855 | 55.007 | 331.775 | 536.063 | 829.439 |
| producer_lag | 204933 | 2.958 | 1.4 | 3.799 | 13.823 | 382.463 | 555.007 |
| publish_call | 124757 | 3.502 | 2.769 | 5.511 | 13.703 | 26.079 | 534.015 |
| queue_wait | 204933 | 12.168 | 5.951 | 11.095 | 257.663 | 441.087 | 633.343 |
| broker_dwell | 204933 | 10.047 | 3.973 | 7.851 | 252.799 | 437.247 | 621.567 |
| receive_call | 111611 | 5.014 | 3.041 | 6.387 | 19.359 | 318.463 | 617.471 |
| ctx_load | 204933 | 1.756 | 1.226 | 3.737 | 5.487 | 17.775 | 98.367 |
| process | 204933 | 0.002 | 0.001 | 0.002 | 0.004 | 0.022 | 6.203 |
| ctx_commit | 204933 | 3.124 | 2.543 | 5.211 | 8.983 | 22.031 | 116.031 |
| ack | 204933 | 3.146 | 2.571 | 5.231 | 8.151 | 21.791 | 50.015 |
| service | 204933 | 19.336 | 11.647 | 42.751 | 123.967 | 166.783 | 330.751 |
| e2e_large | 4 | 739.84 | 756.735 | 818.175 | 818.175 | 818.175 | 818.175 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8190.987 | 10028.2 |
| applied_total | 162965.667 | 255197 |
| bench_cpu_cores | 2.533 | 2.698 |
| bench_cpu_pct | 10.553 | 11.24 |
| bench_gc_gen0 | 3.75 | 4 |
| bench_gc_gen1 | 0.375 | 4 |
| bench_gc_gen2 | 0.083 | 1 |
| bench_gc_heap_mb | 1041.154 | 1162.3 |
| bench_mem_mb | 954.683 | 1043.9 |
| bench_threadpool_queue | 1.5 | 18 |
| bench_threadpool_threads | 35.625 | 42 |
| broker_backlog | 282.792 | 1739 |
| broker_oldest_age_s | 0.167 | 1 |
| broker_visible | 82.75 | 1132 |
| ctr_eb_pgbouncer_cpu_pct | 15.124 | 17.17 |
| ctr_eb_pgbouncer_mem_mb | 13.871 | 19 |
| ctr_eb_postgres_cpu_pct | 173.376 | 197.68 |
| ctr_eb_postgres_mem_mb | 1890.283 | 1920.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 59.461 | 68.95 |
| in_flight | 161.75 | 747 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 46.833 | 59 |
| pg_conn_idle | 18.292 | 32 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.125 | 66 |
| pg_db_size_mb | 338.746 | 471.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 41489.983 | 51188.3 |
| pg_tup_del_per_s | 11428.737 | 27981.1 |
| pg_tup_fetch_per_s | 230773.117 | 305349.4 |
| pg_tup_ins_per_s | 14828.008 | 38847 |
| pg_tup_upd_per_s | 21766.496 | 28357.1 |
| pg_wal_mb_per_s | 32.164 | 78.27 |
| pgb_cl_active | 351.292 | 383 |
| pgb_cl_waiting | 31.375 | 120 |
| pgb_maxwait_ms | 1.301 | 5.22 |
| pgb_sv_active | 59.25 | 64 |
| pgb_sv_idle | 4.75 | 18 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 8158.071 | 9755.1 |
| produced_total | 163127.417 | 255257 |
| publish_mb_per_s | 6.927 | 39.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 69,469 records are Completed |
| record_counts | pass | 69,469 records found, 69,469 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 302,109 events applied by workers, 302,109 generated |
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
| A-0 | 1600 | 12 | 600 | 21351 | 21351 | 0 | 19.855 | 135.167 | 219.775 |
| A-1 | 1600 | 12 | 600 | 21363 | 21363 | 0 | 20.079 | 133.119 | 196.479 |
| A-2 | 1600 | 12 | 600 | 21569 | 21569 | 0 | 20.047 | 139.007 | 222.719 |
| A-3 | 1600 | 12 | 600 | 21747 | 21747 | 0 | 20.223 | 202.111 | 337.407 |
| A-4 | 1600 | 12 | 600 | 21584 | 21584 | 0 | 20.015 | 144.767 | 234.367 |
| A-5 | 1600 | 12 | 600 | 21372 | 21372 | 0 | 20.047 | 496.895 | 656.383 |
| A-6 | 1600 | 12 | 600 | 21470 | 21470 | 0 | 20.383 | 404.735 | 840.191 |
| A-7 | 1600 | 12 | 600 | 21245 | 21245 | 0 | 20.063 | 562.687 | 756.735 |
| A-8 | 1600 | 12 | 600 | 21619 | 21619 | 0 | 19.983 | 143.231 | 248.319 |
| A-9 | 1600 | 12 | 600 | 21267 | 21267 | 0 | 20.079 | 559.615 | 811.007 |
| B-0 | 1600 | 12 | 200 | 8720 | 8720 | 0 | 17.999 | 95.487 | 181.247 |
| B-1 | 1600 | 12 | 200 | 8777 | 8777 | 0 | 17.935 | 94.527 | 180.607 |
| B-2 | 1600 | 12 | 200 | 8723 | 8723 | 0 | 17.871 | 108.607 | 192.511 |
| B-3 | 1600 | 12 | 200 | 8757 | 8757 | 0 | 17.823 | 94.591 | 182.527 |
| B-4 | 1600 | 12 | 200 | 8738 | 8738 | 0 | 17.935 | 103.935 | 202.239 |
| B-5 | 1600 | 12 | 200 | 8770 | 8770 | 0 | 17.903 | 93.503 | 169.087 |
| B-6 | 1600 | 12 | 200 | 8762 | 8762 | 0 | 18.175 | 333.055 | 640.511 |
| B-7 | 1600 | 12 | 200 | 8693 | 8693 | 0 | 17.711 | 96.575 | 189.183 |
| B-8 | 1600 | 12 | 200 | 8777 | 8777 | 0 | 17.711 | 96.639 | 178.559 |
| B-9 | 1600 | 12 | 200 | 8805 | 8805 | 0 | 17.935 | 89.151 | 180.223 |
