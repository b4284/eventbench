# D3-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.106s, closing 0.793s, drain 1.106s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814636 | 814636 | 12854.657 | 12854.763 | 18657.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642914 | 10.838 | 9.359 | 17.439 | 34.463 | 69.055 | 153.087 |
| e2e_from_send | 642914 | 9.289 | 8.011 | 14.791 | 29.359 | 66.751 | 150.911 |
| producer_lag | 642914 | 1.549 | 1.107 | 3.441 | 8.135 | 16.479 | 42.719 |
| publish_call | 462054 | 2.736 | 2.097 | 4.979 | 12.079 | 33.247 | 633.855 |
| queue_wait | 642914 | 3.893 | 2.911 | 7.607 | 17.599 | 38.719 | 113.599 |
| broker_dwell | 642914 | 2.261 | 1.376 | 4.943 | 13.223 | 35.935 | 112.767 |
| receive_call | 679094 | 46.98 | 23.503 | 106.559 | 392.447 | 795.647 | 1027.583 |
| ctx_load | 642914 | 0.906 | 0.585 | 1.69 | 5.207 | 15.775 | 30.127 |
| process | 642914 | 0.002 | 0.001 | 0.002 | 0.004 | 0.039 | 8.663 |
| ctx_commit | 642914 | 2.179 | 1.777 | 3.471 | 7.771 | 20.655 | 53.695 |
| ack | 634444 | 0.021 | 0.002 | 0.007 | 0.282 | 3.351 | 26.703 |
| service | 642914 | 5.396 | 4.659 | 8.727 | 18.143 | 39.391 | 121.215 |
| e2e_large | 331 | 43.456 | 41.567 | 66.495 | 92.543 | 140.159 | 140.159 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.763 | 13136.6 |
| applied_total | 468748 | 777552 |
| bench_cpu_cores | 4.198 | 4.629 |
| bench_cpu_pct | 17.493 | 19.29 |
| bench_gc_gen0 | 2.878 | 4 |
| bench_gc_gen1 | 1.918 | 4 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 823.133 | 1034.3 |
| bench_mem_mb | 1310.551 | 1441.2 |
| bench_threadpool_queue | 16.796 | 153 |
| bench_threadpool_threads | 34.796 | 44 |
| broker_backlog | 89.531 | 137 |
| broker_backlog_mb | 0.164 | 0.261 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 2.033 | 4.34 |
| ctr_eb_kafka_mem_mb | 2746.08 | 2890.6 |
| ctr_eb_mongo_cpu_pct | 2.834 | 19.87 |
| ctr_eb_mongo_mem_mb | 4075.829 | 4197.1 |
| ctr_eb_nats_cpu_pct | 385.419 | 395.35 |
| ctr_eb_nats_mem_mb | 1920.722 | 2120 |
| ctr_eb_pgbouncer_cpu_pct | 61.34 | 61.99 |
| ctr_eb_pgbouncer_mem_mb | 19.878 | 22.4 |
| ctr_eb_postgres_cpu_pct | 352.11 | 364.04 |
| ctr_eb_postgres_mem_mb | 5009.398 | 5039.2 |
| ctr_eb_rabbitmq_cpu_pct | 13.643 | 14.32 |
| ctr_eb_rabbitmq_mem_mb | 905.963 | 915.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.047 | 59.85 |
| in_flight | 76.918 | 249 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 21 | 64 |
| pg_conn_idle | 43.102 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.102 | 65 |
| pg_db_size_mb | 1439.639 | 1872.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25612.524 | 30741.7 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 58897.006 | 72902.1 |
| pg_tup_ins_per_s | 10392.159 | 20094.5 |
| pg_tup_upd_per_s | 21161.539 | 25490.7 |
| pg_wal_mb_per_s | 36.441 | 78.74 |
| pgb_cl_active | 489 | 489 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 26.735 | 50 |
| pgb_sv_idle | 37.265 | 62 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.657 | 13155.1 |
| produced_total | 468824.918 | 777641 |
| publish_mb_per_s | 44.334 | 71.19 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,595 records are Completed |
| record_counts | pass | 117,595 records found, 117,595 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,636 events applied by workers, 814,636 generated |
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
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 31.743 | 140.287 | 140.287 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 35.071 | 88.575 | 104.447 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 32.255 | 73.727 | 77.823 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 28.159 | 68.095 | 72.703 |
| t-192 | 6 | 1 | 19.403 | 1.728 | 194 | 0 | 145 | 145 | 0 | 9.599 | 47.615 | 56.063 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 8.639 | 47.103 | 54.527 |
| t-053 | 11 | 1 | 11.553 | 2.172 | 3589 | 0 | 145 | 145 | 0 | 8.383 | 45.311 | 60.415 |
| t-122 | 33 | 1 | 66.424 | 35.794 | 550 | 0 | 2209 | 2209 | 0 | 9.279 | 43.519 | 112.639 |
| t-276 | 78 | 1 | 17.125 | 22.078 | 423 | 0 | 1453 | 1453 | 0 | 9.215 | 41.983 | 70.143 |
| t-390 | 10 | 1 | 36.089 | 5.892 | 167 | 0 | 353 | 353 | 0 | 8.639 | 41.983 | 56.319 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 9.663 | 41.983 | 105.983 |
| t-282 | 23 | 1 | 84.429 | 32.491 | 956 | 0 | 1944 | 1944 | 0 | 9.663 | 41.727 | 91.135 |
| t-133 | 6 | 1 | 27.688 | 2.68 | 273 | 0 | 182 | 182 | 0 | 8.639 | 41.471 | 60.671 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 9.727 | 41.215 | 75.775 |
| t-391 | 61 | 1 | 31.736 | 31.855 | 225 | 0 | 2176 | 2176 | 0 | 9.471 | 40.447 | 77.823 |
| t-416 | 57 | 1 | 38.884 | 37.248 | 3430 | 0 | 2352 | 2352 | 0 | 9.215 | 40.447 | 77.311 |
| t-022 | 63 | 1 | 30.227 | 31.845 | 162 | 0 | 2122 | 2122 | 0 | 9.599 | 40.191 | 91.647 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 2660 | 2660 | 0 | 9.535 | 40.191 | 96.767 |
| t-191 | 11 | 1 | 38.204 | 6.794 | 3529 | 0 | 442 | 442 | 0 | 9.151 | 39.935 | 44.799 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 9.599 | 39.935 | 79.871 |
| t-320 | 14 | 1 | 86.153 | 19.097 | 574 | 0 | 1206 | 1206 | 0 | 9.343 | 39.679 | 89.087 |
| t-020 | 22 | 1 | 63.625 | 22.417 | 981 | 0 | 1432 | 1432 | 0 | 9.407 | 39.423 | 58.367 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 335 | 335 | 0 | 9.023 | 39.423 | 47.359 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 9.663 | 39.423 | 109.567 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 9.471 | 39.423 | 87.039 |
