# 06-kafkafsync_pg — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=1), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 63.243s, closing 6.968s, drain 0.199s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 225037 | 225037 | 3240.265 | 3275.956 | 4046.9 | 0 | 0 | 0 | 155 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 62669 | 40579.298 | 41287.679 | 46891.007 | 48594.943 | 49119.231 | 49512.447 |
| e2e_from_send | 62669 | 2525.377 | 2467.839 | 4603.903 | 5906.431 | 6283.263 | 6500.351 |
| producer_lag | 62669 | 38053.943 | 37257.215 | 44695.551 | 45744.127 | 46071.807 | 46235.647 |
| publish_call | 528 | 4189.87 | 4689.919 | 5316.607 | 6262.783 | 6410.239 | 6443.007 |
| queue_wait | 62669 | 2519.143 | 2459.647 | 4599.807 | 5898.239 | 6279.167 | 6492.159 |
| broker_dwell | 62669 | 29.616 | 28.271 | 52.479 | 65.375 | 89.343 | 108.223 |
| receive_call | 99016 | 0.033 | 0.018 | 0.031 | 0.069 | 4.619 | 22.511 |
| ctx_load | 62669 | 0.332 | 0.306 | 0.416 | 0.683 | 2.359 | 16.623 |
| process | 62669 | 0.002 | 0.001 | 0.002 | 0.005 | 0.04 | 6.675 |
| ctx_commit | 62669 | 2.38 | 1.995 | 3.501 | 9.823 | 18.815 | 27.807 |
| ack | 61187 | 0.006 | 0.007 | 0.01 | 0.019 | 0.038 | 0.221 |
| service | 62669 | 6.231 | 4.691 | 12.591 | 24.479 | 42.719 | 74.175 |
| e2e_large | 55 | 39045.306 | 37814.271 | 45023.231 | 47546.367 | 48758.783 | 48758.783 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3275.956 | 3623.8 |
| applied_total | 114777.115 | 196652 |
| bench_cpu_cores | 1.741 | 1.967 |
| bench_cpu_pct | 7.252 | 8.2 |
| bench_gc_gen0 | 1.096 | 3 |
| bench_gc_gen1 | 0.404 | 1 |
| bench_gc_gen2 | 0.288 | 1 |
| bench_gc_heap_mb | 436.44 | 617.7 |
| bench_mem_mb | 1204.263 | 1337.4 |
| bench_threadpool_queue | 1.135 | 41 |
| bench_threadpool_threads | 20.596 | 24 |
| broker_backlog | 0 | 0 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 74580.769 | 155438 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 41714.673 | 115461 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 98.498 | 118.96 |
| ctr_eb_kafka_mem_mb | 1836.052 | 1942.6 |
| ctr_eb_mongo_cpu_pct | 3.131 | 17.24 |
| ctr_eb_mongo_mem_mb | 4154.219 | 4250.5 |
| ctr_eb_nats_cpu_pct | 0.264 | 0.38 |
| ctr_eb_nats_mem_mb | 173.773 | 174.6 |
| ctr_eb_pgbouncer_cpu_pct | 18.012 | 19.5 |
| ctr_eb_pgbouncer_mem_mb | 16.813 | 17.5 |
| ctr_eb_postgres_cpu_pct | 80.848 | 88.05 |
| ctr_eb_postgres_mem_mb | 4996.285 | 5014.4 |
| ctr_eb_rabbitmq_cpu_pct | 10.281 | 11.2 |
| ctr_eb_rabbitmq_mem_mb | 1140.413 | 1195 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 17.336 | 23.12 |
| in_flight | -7891.5 | -4512 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 10.692 | 14 |
| pg_conn_idle | 53.308 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 803.962 | 1019.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 6030.765 | 6833.4 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 13393.848 | 15464.4 |
| pg_tup_ins_per_s | 3744.43 | 4257 |
| pg_tup_upd_per_s | 4662.678 | 5500.3 |
| pg_wal_mb_per_s | 9.53 | 11.29 |
| pgb_cl_active | 74.308 | 84 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.885 | 11 |
| pgb_sv_idle | 45.962 | 59 |
| pgb_sv_used | 11.154 | 64 |
| produced_per_s | 3240.265 | 7999.1 |
| produced_total | 106885.615 | 188626 |
| publish_mb_per_s | 14.433 | 40.93 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 44,959 records are Completed |
| record_counts | pass | 44,959 records found, 44,959 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 155 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 225,037 events applied by workers, 225,037 generated |
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
| t-365 | 13 | 1 | 35.093 | 7.573 | 3428 | 0 | 133 | 133 | 0 | 41418.751 | 49545.215 | 49545.215 |
| t-039 | 28 | 1 | 18.305 | 8.565 | 1459 | 0 | 189 | 189 | 0 | 41156.607 | 49283.071 | 49283.071 |
| t-118 | 15 | 1 | 24.104 | 6.098 | 138 | 0 | 128 | 128 | 0 | 41943.039 | 49283.071 | 49283.071 |
| t-130 | 13 | 1 | 31.643 | 7.058 | 136 | 0 | 119 | 119 | 0 | 42991.615 | 49283.071 | 49283.071 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 101 | 101 | 0 | 42729.471 | 49283.071 | 49283.071 |
| t-190 | 6 | 1 | 69.297 | 7.234 | 241 | 0 | 112 | 112 | 0 | 40632.319 | 49283.071 | 49283.071 |
| t-256 | 14 | 1 | 36.08 | 7.98 | 2927 | 0 | 142 | 142 | 0 | 39845.887 | 49283.071 | 49283.071 |
| t-275 | 9 | 1 | 70.577 | 10.508 | 166 | 0 | 177 | 177 | 0 | 41418.751 | 49283.071 | 49283.071 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 102 | 102 | 0 | 44826.623 | 49283.071 | 49283.071 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 142 | 142 | 0 | 42729.471 | 49283.071 | 49283.071 |
| t-311 | 15 | 1 | 22.841 | 5.944 | 761 | 0 | 98 | 98 | 0 | 41680.895 | 49283.071 | 49283.071 |
| t-316 | 19 | 1 | 26.847 | 8.545 | 169 | 0 | 183 | 183 | 0 | 41680.895 | 49283.071 | 49283.071 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 125 | 125 | 0 | 36962.303 | 49283.071 | 49283.071 |
| t-357 | 21 | 1 | 53.774 | 18.657 | 2215 | 0 | 295 | 295 | 0 | 42467.327 | 49283.071 | 49283.071 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 111 | 111 | 0 | 42205.183 | 49283.071 | 49283.071 |
| t-428 | 37 | 1 | 60.011 | 37.101 | 159 | 0 | 623 | 623 | 0 | 42729.471 | 49283.071 | 49283.071 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 147 | 147 | 0 | 42205.183 | 49283.071 | 49283.071 |
| t-453 | 6 | 1 | 44.943 | 4.427 | 874 | 0 | 60 | 60 | 0 | 44040.191 | 49283.071 | 49283.071 |
| t-476 | 15 | 1 | 38.366 | 10.704 | 432 | 0 | 176 | 176 | 0 | 39321.599 | 49283.071 | 49283.071 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 128 | 128 | 0 | 40370.175 | 49283.071 | 49283.071 |
| t-001 | 118 | 2 | 29.613 | 57.536 | 154 | 0 | 1114 | 1114 | 0 | 40894.463 | 49020.927 | 49283.071 |
| t-002 | 17 | 1 | 82.711 | 23.551 | 377 | 0 | 414 | 414 | 0 | 41680.895 | 49020.927 | 49283.071 |
| t-012 | 6 | 1 | 61.836 | 6.408 | 556 | 0 | 107 | 107 | 0 | 41680.895 | 49020.927 | 49020.927 |
| t-022 | 63 | 1 | 30.227 | 31.845 | 162 | 0 | 623 | 623 | 0 | 42205.183 | 49020.927 | 49545.215 |
| t-033 | 6 | 1 | 70.419 | 7.677 | 859 | 0 | 143 | 143 | 0 | 42205.183 | 49020.927 | 49020.927 |
