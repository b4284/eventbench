# 13-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.109s, closing 1.097s, drain 0.505s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814594 | 814594 | 12854.655 | 12855.073 | 20832 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642869 | 9.332 | 7.151 | 17.695 | 37.855 | 59.615 | 169.855 |
| e2e_from_send | 642869 | 7.533 | 5.847 | 14.159 | 28.991 | 47.423 | 167.423 |
| producer_lag | 642869 | 1.799 | 1.122 | 4.005 | 12.207 | 27.087 | 54.239 |
| publish_call | 449152 | 2.805 | 1.885 | 5.363 | 14.655 | 46.047 | 790.015 |
| queue_wait | 642869 | 3.933 | 2.661 | 8.327 | 20.175 | 35.103 | 70.207 |
| broker_dwell | 642869 | 1.59 | 0.821 | 3.861 | 12.319 | 23.871 | 63.199 |
| receive_call | 690267 | 49.246 | 26.319 | 108.607 | 396.543 | 801.279 | 1029.631 |
| ctx_load | 642869 | 0.634 | 0.426 | 1.148 | 3.407 | 8.831 | 33.919 |
| process | 642869 | 0.002 | 0.001 | 0.002 | 0.004 | 0.035 | 17.775 |
| ctx_commit | 642869 | 0.647 | 0.445 | 1.124 | 3.211 | 10.591 | 127.871 |
| ack | 634075 | 0.016 | 0.002 | 0.005 | 0.153 | 2.989 | 13.767 |
| service | 642869 | 3.6 | 2.773 | 6.839 | 15.391 | 27.263 | 133.631 |
| e2e_large | 331 | 45.112 | 41.503 | 69.631 | 107.199 | 169.855 | 169.855 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12855.073 | 13138.3 |
| applied_total | 468793.184 | 777587 |
| bench_cpu_cores | 4.968 | 5.638 |
| bench_cpu_pct | 20.698 | 23.49 |
| bench_gc_gen0 | 2.959 | 4 |
| bench_gc_gen1 | 1.388 | 3 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 746.129 | 1020.8 |
| bench_mem_mb | 1236.204 | 1394.2 |
| bench_threadpool_queue | 12.306 | 168 |
| bench_threadpool_threads | 33.347 | 44 |
| broker_backlog | 90.735 | 218 |
| broker_backlog_mb | 1.668 | 10.026 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.749 | 2.14 |
| ctr_eb_kafka_mem_mb | 3332.661 | 3476.1 |
| ctr_eb_mssql_cpu_pct | 1.761 | 2.06 |
| ctr_eb_mssql_mem_mb | 5938.937 | 5969 |
| ctr_eb_nats_cpu_pct | 410.567 | 427.22 |
| ctr_eb_nats_mem_mb | 2423.902 | 2627.4 |
| ctr_eb_pgbouncer_cpu_pct | 60.335 | 63.48 |
| ctr_eb_pgbouncer_mem_mb | 14.671 | 16.6 |
| ctr_eb_postgres_cpu_pct | 259.124 | 275.21 |
| ctr_eb_postgres_mem_mb | 3063.918 | 3159.5 |
| ctr_eb_rabbitmq_cpu_pct | 12.852 | 13.42 |
| ctr_eb_rabbitmq_mem_mb | 752.418 | 758.5 |
| ctr_eb_redis_cpu_pct | 0.262 | 0.27 |
| ctr_eb_redis_mem_mb | 1327.827 | 1328.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.337 | 61.96 |
| in_flight | 49.857 | 122 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.245 | 8 |
| pg_conn_idle | 61.755 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1319.478 | 1744.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25903.851 | 29377.8 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59329.498 | 69321.6 |
| pg_tup_ins_per_s | 10994.557 | 20314.1 |
| pg_tup_upd_per_s | 21395.373 | 24360.9 |
| pg_wal_mb_per_s | 36.821 | 64.61 |
| pgb_cl_active | 495 | 495 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.959 | 14 |
| pgb_sv_idle | 59.041 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.655 | 13115.3 |
| produced_total | 468843.041 | 777633 |
| publish_mb_per_s | 44.339 | 71.37 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,593 records are Completed |
| record_counts | pass | 117,593 records found, 117,593 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,594 events applied by workers, 814,594 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 35.071 | 104.959 | 169.983 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 28.671 | 95.231 | 119.807 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 37.375 | 90.111 | 90.111 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 27.647 | 86.527 | 101.375 |
| t-198 | 6 | 1 | 13.16 | 1.151 | 180 | 0 | 79 | 79 | 0 | 8.959 | 61.951 | 62.975 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 7.359 | 54.015 | 70.655 |
| t-015 | 9 | 1 | 40.857 | 6.053 | 3462 | 0 | 393 | 393 | 0 | 7.103 | 50.943 | 69.631 |
| t-467 | 28 | 1 | 18.189 | 8.297 | 250 | 0 | 595 | 595 | 0 | 7.455 | 50.943 | 63.231 |
| t-078 | 6 | 1 | 17.625 | 1.71 | 386 | 0 | 108 | 108 | 0 | 8.767 | 48.127 | 58.623 |
| t-092 | 29 | 1 | 33.741 | 16.54 | 519 | 0 | 1055 | 1055 | 0 | 7.039 | 47.615 | 74.239 |
| t-434 | 39 | 1 | 44.58 | 28.795 | 630 | 0 | 1832 | 1832 | 0 | 7.263 | 47.615 | 68.095 |
| t-145 | 6 | 1 | 51.429 | 5.182 | 517 | 0 | 297 | 297 | 0 | 6.783 | 47.359 | 68.095 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 7.487 | 47.103 | 56.063 |
| t-025 | 6 | 1 | 48.625 | 5.14 | 954 | 0 | 325 | 325 | 0 | 6.751 | 46.847 | 52.735 |
| t-085 | 19 | 1 | 28.267 | 8.788 | 1157 | 0 | 582 | 582 | 0 | 7.199 | 46.847 | 61.439 |
| t-487 | 9 | 1 | 97.44 | 14.247 | 755 | 0 | 867 | 867 | 0 | 7.455 | 46.591 | 60.159 |
| t-102 | 18 | 1 | 35.867 | 10.912 | 339 | 0 | 722 | 722 | 0 | 7.647 | 45.567 | 70.143 |
| t-226 | 6 | 1 | 87.952 | 9.302 | 623 | 0 | 575 | 575 | 0 | 7.679 | 45.567 | 59.903 |
| t-254 | 9 | 1 | 21.121 | 3.28 | 3623 | 0 | 202 | 202 | 0 | 8.447 | 45.567 | 56.575 |
| t-179 | 25 | 1 | 23.549 | 10.084 | 989 | 0 | 658 | 658 | 0 | 7.039 | 45.311 | 55.295 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1901 | 1901 | 0 | 6.847 | 45.055 | 62.975 |
| t-399 | 34 | 1 | 33.65 | 18.576 | 397 | 0 | 1254 | 1254 | 0 | 7.199 | 45.055 | 64.767 |
| t-012 | 6 | 1 | 61.836 | 6.408 | 556 | 0 | 381 | 381 | 0 | 7.231 | 44.799 | 62.463 |
| t-066 | 38 | 1 | 41.933 | 26.44 | 277 | 0 | 1727 | 1727 | 0 | 7.615 | 44.543 | 66.047 |
| t-081 | 6 | 1 | 13.059 | 1.301 | 2170 | 0 | 94 | 94 | 0 | 8.159 | 44.287 | 44.799 |
