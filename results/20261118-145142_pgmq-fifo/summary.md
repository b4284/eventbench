# pgmq-fifo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 20 groups, 16000 sources, 160 workers, offered 4000 ev/s
- Phases: run 20.218s, closing 0.156s, drain 1.712s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 108515 | 108515 | 4150.093 | 4150.736 | 13850.5 | 0 | 0 | 0 | 10 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 62519 | 13.539 | 10.511 | 16.655 | 41.087 | 420.095 | 877.055 |
| e2e_from_send | 62519 | 11.103 | 9.351 | 15.551 | 36.031 | 116.543 | 864.767 |
| producer_lag | 62519 | 2.436 | 0.759 | 2.469 | 12.447 | 354.047 | 456.191 |
| publish_call | 68508 | 1.872 | 1.588 | 2.271 | 6.431 | 35.551 | 490.751 |
| queue_wait | 62519 | 5.685 | 5.031 | 7.131 | 23.263 | 82.879 | 551.935 |
| broker_dwell | 62519 | 4.587 | 3.889 | 6.047 | 22.143 | 81.471 | 522.239 |
| receive_call | 52231 | 2.444 | 1.817 | 2.601 | 18.447 | 67.903 | 121.407 |
| ctx_load | 62519 | 0.383 | 0.314 | 0.608 | 1.174 | 2.333 | 20.607 |
| process | 62519 | 0.002 | 0.001 | 0.002 | 0.004 | 0.018 | 5.243 |
| ctx_commit | 62519 | 1.379 | 1.211 | 1.822 | 2.945 | 17.151 | 167.551 |
| ack | 62519 | 1.383 | 1.273 | 1.947 | 2.935 | 15.087 | 24.863 |
| service | 62519 | 5.419 | 3.783 | 9.647 | 23.087 | 48.127 | 313.087 |
| e2e_large | 7 | 560.283 | 550.911 | 668.671 | 877.055 | 877.055 | 877.055 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4150.736 | 4898.3 |
| applied_total | 61476.143 | 87613 |
| bench_cpu_cores | 2.012 | 2.322 |
| bench_cpu_pct | 8.383 | 9.67 |
| bench_gc_gen0 | 4.286 | 5 |
| bench_gc_gen1 | 0.214 | 1 |
| bench_gc_gen2 | 0.214 | 1 |
| bench_gc_heap_mb | 682.25 | 844.9 |
| bench_mem_mb | 616.093 | 718.7 |
| bench_threadpool_queue | 2.286 | 32 |
| bench_threadpool_threads | 27.929 | 28 |
| broker_backlog | 66.571 | 166 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 22.429 | 101 |
| ctr_6d7d5f992e4d_infra_cpu_pct | 0 | 0 |
| ctr_6d7d5f992e4d_infra_mem_mb | 0.2 | 0.2 |
| ctr_eb_pgbouncer_cpu_pct | 2.996 | 4.26 |
| ctr_eb_pgbouncer_mem_mb | 12.443 | 13.7 |
| ctr_eb_postgres_cpu_pct | 33.817 | 47.76 |
| ctr_eb_postgres_mem_mb | 736.014 | 861.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.014 | 32.34 |
| in_flight | 24.429 | 35 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 18.571 | 34 |
| pg_conn_idle | 46.714 | 58 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.286 | 66 |
| pg_db_size_mb | 164.171 | 225.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26275.186 | 28171.1 |
| pg_tup_del_per_s | 8003.9 | 13356.9 |
| pg_tup_fetch_per_s | 93191.614 | 119783.7 |
| pg_tup_ins_per_s | 11138.714 | 16804.6 |
| pg_tup_upd_per_s | 11081.843 | 11823.2 |
| pg_wal_mb_per_s | 23.48 | 33.83 |
| produced_per_s | 4150.093 | 4892.3 |
| produced_total | 61500.571 | 87634 |
| publish_mb_per_s | 6.829 | 25.28 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 28,008 records are Completed |
| record_counts | pass | 28,008 records found, 28,008 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 10 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 108,515 events applied by workers, 108,515 generated |
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
| A-0 | 800 | 8 | 300 | 7468 | 7468 | 0 | 10.407 | 180.351 | 423.167 |
| A-1 | 800 | 8 | 300 | 7467 | 7467 | 0 | 10.559 | 30.367 | 44.415 |
| A-2 | 800 | 8 | 300 | 7671 | 7671 | 0 | 10.519 | 35.647 | 63.039 |
| A-3 | 800 | 8 | 300 | 7643 | 7643 | 0 | 10.503 | 281.087 | 550.911 |
| A-4 | 800 | 8 | 300 | 7618 | 7618 | 0 | 10.503 | 31.231 | 39.935 |
| A-5 | 800 | 8 | 300 | 7322 | 7322 | 0 | 10.535 | 31.663 | 877.055 |
| A-6 | 800 | 8 | 300 | 7498 | 7498 | 0 | 10.663 | 382.975 | 669.183 |
| A-7 | 800 | 8 | 300 | 7570 | 7570 | 0 | 10.535 | 33.055 | 43.359 |
| A-8 | 800 | 8 | 300 | 7695 | 7695 | 0 | 10.551 | 28.911 | 68.095 |
| A-9 | 800 | 8 | 300 | 7559 | 7559 | 0 | 10.487 | 32.351 | 49.759 |
| B-0 | 800 | 8 | 100 | 3262 | 3262 | 0 | 10.423 | 28.639 | 38.271 |
| B-1 | 800 | 8 | 100 | 3313 | 3313 | 0 | 10.719 | 29.775 | 39.295 |
| B-2 | 800 | 8 | 100 | 3268 | 3268 | 0 | 10.503 | 29.903 | 47.167 |
| B-3 | 800 | 8 | 100 | 3278 | 3278 | 0 | 10.351 | 29.007 | 44.447 |
| B-4 | 800 | 8 | 100 | 3253 | 3253 | 0 | 10.383 | 26.271 | 40.351 |
| B-5 | 800 | 8 | 100 | 3288 | 3288 | 0 | 10.535 | 25.519 | 47.071 |
| B-6 | 800 | 8 | 100 | 3407 | 3407 | 0 | 10.295 | 27.263 | 35.039 |
| B-7 | 800 | 8 | 100 | 3370 | 3370 | 0 | 10.463 | 25.215 | 35.327 |
| B-8 | 800 | 8 | 100 | 3254 | 3254 | 0 | 10.687 | 438.783 | 658.943 |
| B-9 | 800 | 8 | 100 | 3311 | 3311 | 0 | 10.487 | 28.271 | 37.983 |
