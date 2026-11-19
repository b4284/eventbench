# 14-kafka_pg — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.109s, closing 0.698s, drain 1.101s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814602 | 814602 | 12854.08 | 12853.935 | 24233.1 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642879 | 20.761 | 16.215 | 40.671 | 59.167 | 68.863 | 237.439 |
| e2e_from_send | 642879 | 18.635 | 13.879 | 38.143 | 56.575 | 63.295 | 234.623 |
| producer_lag | 642879 | 2.126 | 1.847 | 4.143 | 8.655 | 16.103 | 38.559 |
| publish_call | 388346 | 4.235 | 3.659 | 5.827 | 11.903 | 36.063 | 420.095 |
| queue_wait | 642879 | 15.704 | 10.687 | 35.007 | 53.727 | 58.399 | 87.871 |
| broker_dwell | 642879 | 11.875 | 6.619 | 31.023 | 49.823 | 52.895 | 73.023 |
| receive_call | 595777 | 0.022 | 0.011 | 0.023 | 0.084 | 2.245 | 31.039 |
| ctx_load | 642879 | 1.111 | 0.938 | 1.965 | 3.847 | 6.659 | 18.031 |
| process | 642879 | 0.002 | 0.001 | 0.002 | 0.004 | 0.045 | 6.899 |
| ctx_commit | 642879 | 1.097 | 0.914 | 1.912 | 4.013 | 8.143 | 167.295 |
| ack | 642183 | 0.005 | 0.003 | 0.009 | 0.016 | 0.11 | 5.655 |
| service | 642879 | 2.932 | 2.363 | 5.135 | 10.983 | 19.999 | 171.007 |
| e2e_large | 331 | 64.113 | 63.615 | 93.695 | 123.007 | 237.439 | 237.439 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.935 | 13104 |
| applied_total | 462779.612 | 771615 |
| bench_cpu_cores | 6.17 | 6.375 |
| bench_cpu_pct | 25.709 | 26.56 |
| bench_gc_gen0 | 3.959 | 5 |
| bench_gc_gen1 | 1.857 | 5 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 580.122 | 726 |
| bench_mem_mb | 1424.3 | 1505 |
| bench_threadpool_queue | 13.653 | 186 |
| bench_threadpool_threads | 30.796 | 37 |
| broker_backlog | 11.531 | 35 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 444851.245 | 762272 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 443887.612 | 761240 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 282.942 | 294.18 |
| ctr_eb_kafka_mem_mb | 3290.08 | 3362.7 |
| ctr_eb_mssql_cpu_pct | 1.928 | 2.07 |
| ctr_eb_mssql_mem_mb | 5784.816 | 5906.1 |
| ctr_eb_nats_cpu_pct | 2.616 | 10.76 |
| ctr_eb_nats_mem_mb | 2307.937 | 2351.8 |
| ctr_eb_pgbouncer_cpu_pct | 58.271 | 59.51 |
| ctr_eb_pgbouncer_mem_mb | 13.871 | 14.8 |
| ctr_eb_postgres_cpu_pct | 281.703 | 297.07 |
| ctr_eb_postgres_mem_mb | 3063.004 | 3119.5 |
| ctr_eb_rabbitmq_cpu_pct | 13.513 | 14.46 |
| ctr_eb_rabbitmq_mem_mb | 735.331 | 746.5 |
| ctr_eb_redis_cpu_pct | 0.282 | 0.31 |
| ctr_eb_redis_mem_mb | 1326.894 | 1327.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 56.816 | 60.92 |
| in_flight | 177.918 | 256 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.143 | 8 |
| pg_conn_idle | 61.878 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.02 | 65 |
| pg_db_size_mb | 1305.459 | 1766.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25742.465 | 27706 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59118.406 | 65886.8 |
| pg_tup_ins_per_s | 10756.608 | 17641.1 |
| pg_tup_upd_per_s | 21260.447 | 22975.1 |
| pg_wal_mb_per_s | 33.067 | 46.37 |
| pgb_cl_active | 482 | 482 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.571 | 28 |
| pgb_sv_idle | 57.429 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.08 | 13155.9 |
| produced_total | 462957.531 | 771807 |
| publish_mb_per_s | 44.407 | 68.32 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,593 records are Completed |
| record_counts | pass | 117,593 records found, 117,593 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,602 events applied by workers, 814,602 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 49.919 | 123.391 | 126.975 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 56.063 | 117.247 | 237.567 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 51.711 | 116.223 | 116.223 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 46.079 | 115.199 | 118.783 |
| t-452 | 6 | 1 | 20.6 | 2.155 | 1797 | 0 | 137 | 137 | 0 | 33.791 | 74.239 | 80.895 |
| t-110 | 9 | 1 | 18.885 | 2.85 | 2746 | 0 | 177 | 177 | 0 | 31.743 | 69.631 | 70.655 |
| t-053 | 11 | 1 | 11.553 | 2.172 | 3589 | 0 | 145 | 145 | 0 | 28.927 | 68.607 | 79.359 |
| t-006 | 14 | 1 | 25.112 | 5.603 | 1170 | 0 | 380 | 380 | 0 | 29.439 | 68.095 | 75.775 |
| t-056 | 7 | 1 | 48.226 | 5.826 | 347 | 0 | 372 | 372 | 0 | 27.647 | 68.095 | 76.287 |
| t-249 | 15 | 1 | 23.883 | 6.172 | 691 | 0 | 413 | 413 | 0 | 25.983 | 67.583 | 75.775 |
| t-324 | 15 | 1 | 22.024 | 5.117 | 131 | 0 | 374 | 374 | 0 | 26.879 | 67.583 | 80.383 |
| t-466 | 11 | 1 | 39.527 | 6.561 | 449 | 0 | 457 | 457 | 0 | 26.751 | 67.583 | 73.215 |
| t-098 | 6 | 1 | 27.347 | 2.525 | 192 | 0 | 193 | 193 | 0 | 27.263 | 67.071 | 69.119 |
| t-148 | 6 | 1 | 62.881 | 5.748 | 715 | 0 | 407 | 407 | 0 | 27.519 | 66.559 | 68.095 |
| t-174 | 6 | 1 | 34.772 | 3.9 | 3366 | 0 | 229 | 229 | 0 | 26.111 | 66.559 | 71.167 |
| t-336 | 11 | 1 | 40.566 | 7.616 | 1886 | 0 | 480 | 480 | 0 | 25.599 | 66.559 | 77.311 |
| t-370 | 12 | 1 | 20.006 | 4.067 | 568 | 0 | 254 | 254 | 0 | 30.719 | 66.559 | 69.119 |
| t-384 | 12 | 1 | 22.317 | 4.426 | 844 | 0 | 292 | 292 | 0 | 31.359 | 66.559 | 68.607 |
| t-135 | 6 | 1 | 15.198 | 1.55 | 791 | 0 | 85 | 85 | 0 | 31.231 | 66.047 | 72.703 |
| t-151 | 6 | 1 | 23.83 | 2.494 | 577 | 0 | 156 | 156 | 0 | 25.727 | 66.047 | 67.071 |
| t-435 | 8 | 1 | 39.599 | 5.882 | 3763 | 0 | 370 | 370 | 0 | 29.183 | 66.047 | 68.095 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 23.935 | 66.047 | 76.799 |
| t-068 | 21 | 1 | 19.721 | 6.772 | 3531 | 0 | 459 | 459 | 0 | 28.927 | 65.535 | 73.727 |
| t-177 | 19 | 1 | 38.393 | 12.238 | 615 | 0 | 793 | 793 | 0 | 22.527 | 65.535 | 71.679 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 31.615 | 65.535 | 69.119 |
