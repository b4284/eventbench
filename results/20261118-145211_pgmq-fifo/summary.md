# pgmq-fifo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 3.263s, closing 0.047s, drain 0.802s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 27397 | 27397 | 6238.4 | 6304.4 | 10182.9 | 0 | 0 | 0 | 2 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 11819 | 17.28 | 12.343 | 24.495 | 146.175 | 317.951 | 755.199 |
| e2e_from_send | 11819 | 14.597 | 11.087 | 22.735 | 84.031 | 178.047 | 743.935 |
| producer_lag | 11819 | 2.683 | 0.927 | 2.565 | 21.007 | 261.887 | 303.359 |
| publish_call | 11398 | 2.255 | 1.814 | 2.695 | 10.727 | 64.255 | 455.679 |
| queue_wait | 11819 | 6.252 | 4.827 | 7.727 | 51.743 | 111.679 | 537.599 |
| broker_dwell | 11819 | 5.329 | 3.891 | 6.767 | 51.103 | 110.911 | 477.183 |
| receive_call | 9662 | 2.488 | 2.083 | 3.543 | 13.951 | 18.815 | 132.991 |
| ctx_load | 11819 | 0.594 | 0.476 | 1.002 | 2.149 | 5.419 | 11.551 |
| process | 11819 | 0.006 | 0.004 | 0.006 | 0.01 | 0.057 | 3.505 |
| ctx_commit | 11819 | 1.744 | 1.543 | 2.291 | 4.475 | 17.887 | 137.215 |
| ack | 11819 | 1.767 | 1.638 | 2.333 | 4.287 | 17.599 | 19.135 |
| service | 11819 | 8.346 | 5.607 | 16.263 | 40.511 | 61.887 | 206.463 |
| e2e_large | 2 | 616.64 | 478.463 | 755.199 | 755.199 | 755.199 | 755.199 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6304.4 | 6304.4 |
| applied_total | 15953 | 15953 |
| bench_cpu_cores | 4.14 | 4.14 |
| bench_cpu_pct | 17.25 | 17.25 |
| bench_gc_gen0 | 7 | 7 |
| bench_gc_gen1 | 1 | 1 |
| bench_gc_gen2 | 1 | 1 |
| bench_gc_heap_mb | 240.8 | 240.8 |
| bench_mem_mb | 347.5 | 347.5 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 25 | 25 |
| broker_backlog | 76 | 76 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 1 | 1 |
| ctr_6d7d5f992e4d_infra_cpu_pct | 0 | 0 |
| ctr_6d7d5f992e4d_infra_mem_mb | 0.2 | 0.2 |
| ctr_eb_pgbouncer_cpu_pct | 5.29 | 5.29 |
| ctr_eb_pgbouncer_mem_mb | 11.5 | 11.5 |
| ctr_eb_postgres_cpu_pct | 61.48 | 61.48 |
| ctr_eb_postgres_mem_mb | 965.1 | 965.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 49.81 | 49.81 |
| in_flight | 22 | 22 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 22 | 22 |
| pg_conn_idle | 43 | 43 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65 | 65 |
| pg_db_size_mb | 54.9 | 54.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 39601.9 | 39601.9 |
| pg_tup_del_per_s | 13241.1 | 13241.1 |
| pg_tup_fetch_per_s | 590549.7 | 590549.7 |
| pg_tup_ins_per_s | 23573.4 | 23573.4 |
| pg_tup_upd_per_s | 21072.1 | 21072.1 |
| pg_wal_mb_per_s | 34.72 | 34.72 |
| produced_per_s | 6238.4 | 6238.4 |
| produced_total | 15975 | 15975 |
| publish_mb_per_s | 3.22 | 3.22 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 11,599 records are Completed |
| record_counts | pass | 11,599 records found, 11,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 2 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 27,397 events applied by workers, 27,397 generated |
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
| A-0 | 800 | 8 | 300 | 2098 | 2098 | 0 | 13.983 | 410.111 | 488.447 |
| A-1 | 800 | 8 | 300 | 2079 | 2079 | 0 | 12.431 | 42.239 | 55.519 |
| A-2 | 800 | 8 | 300 | 2159 | 2159 | 0 | 12.303 | 42.303 | 50.911 |
| A-3 | 800 | 8 | 300 | 2122 | 2122 | 0 | 12.079 | 40.415 | 66.047 |
| A-4 | 800 | 8 | 300 | 2136 | 2136 | 0 | 12.567 | 43.327 | 50.815 |
| A-5 | 800 | 8 | 300 | 2121 | 2121 | 0 | 11.727 | 40.895 | 47.103 |
| A-6 | 800 | 8 | 300 | 2050 | 2050 | 0 | 12.287 | 43.039 | 53.439 |
| A-7 | 800 | 8 | 300 | 2104 | 2104 | 0 | 12.071 | 37.151 | 49.663 |
| A-8 | 800 | 8 | 300 | 2145 | 2145 | 0 | 12.671 | 44.767 | 62.751 |
| A-9 | 800 | 8 | 300 | 2126 | 2126 | 0 | 12.767 | 44.575 | 54.271 |
| B-0 | 800 | 8 | 100 | 597 | 597 | 0 | 11.919 | 29.791 | 755.199 |
| B-1 | 800 | 8 | 100 | 589 | 589 | 0 | 11.631 | 26.639 | 30.271 |
| B-2 | 800 | 8 | 100 | 672 | 672 | 0 | 13.151 | 31.311 | 36.223 |
| B-3 | 800 | 8 | 100 | 618 | 618 | 0 | 12.151 | 33.503 | 37.567 |
| B-4 | 800 | 8 | 100 | 674 | 674 | 0 | 11.719 | 29.311 | 30.815 |
| B-5 | 800 | 8 | 100 | 614 | 614 | 0 | 12.023 | 29.439 | 30.767 |
| B-6 | 800 | 8 | 100 | 645 | 645 | 0 | 11.871 | 30.639 | 33.311 |
| B-7 | 800 | 8 | 100 | 583 | 583 | 0 | 12.239 | 27.647 | 28.127 |
| B-8 | 800 | 8 | 100 | 656 | 656 | 0 | 11.415 | 24.815 | 36.031 |
| B-9 | 800 | 8 | 100 | 609 | 609 | 0 | 12.431 | 32.239 | 37.503 |
