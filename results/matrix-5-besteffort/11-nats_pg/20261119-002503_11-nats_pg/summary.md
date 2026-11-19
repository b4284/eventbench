# 11-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.11s, closing 0.823s, drain 0.908s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814630 | 814630 | 12853.406 | 12854.002 | 19806.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642906 | 9.279 | 7.519 | 16.927 | 33.279 | 49.599 | 152.575 |
| e2e_from_send | 642906 | 7.556 | 6.179 | 13.671 | 26.351 | 38.815 | 145.407 |
| producer_lag | 642906 | 1.723 | 1.149 | 3.871 | 10.287 | 20.671 | 41.023 |
| publish_call | 445572 | 2.88 | 2.035 | 5.451 | 13.511 | 53.215 | 678.911 |
| queue_wait | 642906 | 3.904 | 2.805 | 8.059 | 18.239 | 29.823 | 63.199 |
| broker_dwell | 642906 | 1.717 | 1.016 | 4.039 | 10.967 | 19.743 | 42.719 |
| receive_call | 689167 | 49.123 | 26.271 | 108.543 | 392.191 | 796.671 | 1057.791 |
| ctx_load | 642906 | 0.654 | 0.434 | 1.253 | 3.371 | 7.439 | 17.535 |
| process | 642906 | 0.002 | 0.001 | 0.002 | 0.004 | 0.032 | 13.607 |
| ctx_commit | 642906 | 0.664 | 0.454 | 1.212 | 3.327 | 8.487 | 100.735 |
| ack | 633253 | 0.014 | 0.002 | 0.005 | 0.157 | 2.653 | 7.967 |
| service | 642906 | 3.652 | 2.927 | 6.903 | 13.831 | 22.831 | 111.167 |
| e2e_large | 331 | 41.739 | 38.847 | 64.383 | 101.119 | 152.575 | 152.575 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.002 | 13219.9 |
| applied_total | 468721.102 | 777516 |
| bench_cpu_cores | 4.616 | 5.04 |
| bench_cpu_pct | 19.234 | 21 |
| bench_gc_gen0 | 3 | 5 |
| bench_gc_gen1 | 1.653 | 4 |
| bench_gc_gen2 | 0.408 | 2 |
| bench_gc_heap_mb | 725.735 | 898.5 |
| bench_mem_mb | 1159.502 | 1315.2 |
| bench_threadpool_queue | 12.469 | 141 |
| bench_threadpool_threads | 31.939 | 44 |
| broker_backlog | 97.551 | 225 |
| broker_backlog_mb | 1.573 | 10.038 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.628 | 2.06 |
| ctr_eb_kafka_mem_mb | 2818.863 | 2976.4 |
| ctr_eb_mongo_cpu_pct | 1.151 | 2.23 |
| ctr_eb_mongo_mem_mb | 3938.986 | 4023.6 |
| ctr_eb_nats_cpu_pct | 405.142 | 419.7 |
| ctr_eb_nats_mem_mb | 3698.39 | 5650.5 |
| ctr_eb_pgbouncer_cpu_pct | 59.402 | 60.58 |
| ctr_eb_pgbouncer_mem_mb | 16.224 | 17.3 |
| ctr_eb_postgres_cpu_pct | 262.174 | 280.68 |
| ctr_eb_postgres_mem_mb | 4370.39 | 4403.1 |
| ctr_eb_rabbitmq_cpu_pct | 12.52 | 13.49 |
| ctr_eb_rabbitmq_mem_mb | 779.537 | 788.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 55.505 | 58.2 |
| in_flight | 53.02 | 143 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 1.531 | 5 |
| pg_conn_idle | 62.469 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 65 |
| pg_db_size_mb | 1310.339 | 1734.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25479.159 | 31077.8 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 58350.388 | 70825.4 |
| pg_tup_ins_per_s | 10608.71 | 19175.2 |
| pg_tup_upd_per_s | 21046.416 | 25851.5 |
| pg_wal_mb_per_s | 32.674 | 52.24 |
| pgb_cl_active | 522 | 522 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 9.469 | 40 |
| pgb_sv_idle | 54.49 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.406 | 13136.9 |
| produced_total | 468774.122 | 777543 |
| publish_mb_per_s | 44.331 | 71.21 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,630 events applied by workers, 814,630 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 33.023 | 95.231 | 152.575 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 29.695 | 84.991 | 84.991 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 26.367 | 77.311 | 101.887 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 27.391 | 73.727 | 77.311 |
| t-034 | 7 | 1 | 26.346 | 3.113 | 2831 | 0 | 196 | 196 | 0 | 7.487 | 50.687 | 56.831 |
| t-430 | 6 | 1 | 23.835 | 2.584 | 1163 | 0 | 168 | 168 | 0 | 7.903 | 47.615 | 50.687 |
| t-356 | 6 | 1 | 85.291 | 8.355 | 303 | 0 | 527 | 527 | 0 | 7.103 | 43.263 | 48.383 |
| t-192 | 6 | 1 | 19.403 | 1.728 | 194 | 0 | 145 | 145 | 0 | 7.871 | 41.983 | 62.975 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 198 | 198 | 0 | 8.095 | 41.471 | 57.087 |
| t-033 | 6 | 1 | 70.419 | 7.677 | 859 | 0 | 505 | 505 | 0 | 7.839 | 40.191 | 60.415 |
| t-494 | 35 | 1 | 33.067 | 18.747 | 2036 | 0 | 1179 | 1179 | 0 | 7.775 | 39.423 | 61.183 |
| t-216 | 19 | 1 | 60.988 | 19.724 | 3889 | 0 | 1234 | 1234 | 0 | 7.999 | 38.911 | 50.175 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 7.903 | 38.911 | 41.215 |
| t-167 | 10 | 1 | 24.805 | 4.276 | 1266 | 0 | 249 | 249 | 0 | 7.263 | 38.143 | 38.399 |
| t-212 | 29 | 1 | 50.595 | 24.477 | 296 | 0 | 1594 | 1594 | 0 | 7.935 | 38.143 | 56.831 |
| t-311 | 15 | 1 | 22.841 | 5.944 | 761 | 0 | 396 | 396 | 0 | 7.871 | 38.143 | 57.855 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2738 | 2738 | 0 | 7.231 | 38.143 | 53.759 |
| t-007 | 17 | 1 | 50.977 | 14.02 | 140 | 0 | 921 | 921 | 0 | 8.095 | 37.887 | 52.479 |
| t-079 | 6 | 1 | 96.661 | 9.939 | 1522 | 0 | 550 | 550 | 0 | 7.455 | 37.887 | 55.295 |
| t-095 | 6 | 1 | 54.646 | 5.516 | 925 | 0 | 308 | 308 | 0 | 7.327 | 37.631 | 41.727 |
| t-236 | 12 | 1 | 21.423 | 4.143 | 131 | 0 | 263 | 263 | 0 | 8.319 | 37.631 | 43.263 |
| t-026 | 6 | 1 | 120.855 | 12.121 | 1596 | 0 | 717 | 717 | 0 | 7.807 | 37.375 | 56.831 |
| t-054 | 17 | 1 | 35.84 | 10.264 | 161 | 0 | 682 | 682 | 0 | 7.231 | 37.375 | 73.215 |
| t-062 | 18 | 1 | 31.356 | 9.614 | 2061 | 0 | 649 | 649 | 0 | 7.999 | 37.375 | 46.335 |
| t-123 | 16 | 1 | 67.158 | 16.694 | 4028 | 0 | 1028 | 1028 | 0 | 7.487 | 37.375 | 48.127 |
