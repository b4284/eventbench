# D3-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.105s, closing 0.86s, drain 1.113s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814619 | 814619 | 12853.59 | 12854.212 | 19349.1 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642895 | 14.837 | 12.455 | 26.175 | 47.039 | 80.063 | 143.871 |
| e2e_from_send | 642895 | 12.422 | 10.511 | 21.407 | 39.647 | 76.607 | 132.735 |
| producer_lag | 642895 | 2.415 | 1.624 | 5.587 | 13.383 | 23.711 | 47.679 |
| publish_call | 387289 | 3.866 | 2.897 | 7.471 | 16.943 | 37.215 | 705.023 |
| queue_wait | 642895 | 6.097 | 4.551 | 12.495 | 25.887 | 46.719 | 118.719 |
| broker_dwell | 642895 | 3.172 | 1.777 | 7.259 | 18.655 | 42.911 | 119.039 |
| receive_call | 655316 | 48.887 | 25.391 | 109.631 | 398.847 | 805.887 | 1056.767 |
| ctx_load | 642895 | 0.923 | 0.559 | 1.643 | 6.951 | 20.495 | 33.567 |
| process | 642895 | 0.002 | 0.001 | 0.002 | 0.004 | 0.038 | 16.103 |
| ctx_commit | 642895 | 2.148 | 1.686 | 3.331 | 9.367 | 26.639 | 50.591 |
| ack | 638728 | 0.019 | 0.002 | 0.006 | 0.234 | 3.069 | 15.279 |
| service | 642895 | 6.325 | 5.219 | 11.007 | 23.695 | 50.015 | 119.295 |
| e2e_large | 331 | 49.326 | 46.783 | 76.031 | 100.799 | 125.247 | 125.247 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.212 | 13442.1 |
| applied_total | 468661.327 | 777522 |
| bench_cpu_cores | 4.544 | 5.023 |
| bench_cpu_pct | 18.936 | 20.93 |
| bench_gc_gen0 | 2.959 | 4 |
| bench_gc_gen1 | 1.327 | 3 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 725.343 | 1043.7 |
| bench_mem_mb | 1210.814 | 1452.2 |
| bench_threadpool_queue | 17.429 | 116 |
| bench_threadpool_threads | 33.245 | 44 |
| broker_backlog | 139.306 | 288 |
| broker_backlog_mb | 1.362 | 6.767 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.886 | 3.38 |
| ctr_eb_kafka_mem_mb | 2804.112 | 2953.5 |
| ctr_eb_mongo_cpu_pct | 2.834 | 22.05 |
| ctr_eb_mongo_mem_mb | 4157.069 | 4299.2 |
| ctr_eb_nats_cpu_pct | 409.076 | 419.79 |
| ctr_eb_nats_mem_mb | 2178.929 | 2441.4 |
| ctr_eb_pgbouncer_cpu_pct | 63.826 | 65.81 |
| ctr_eb_pgbouncer_mem_mb | 15.837 | 17.8 |
| ctr_eb_postgres_cpu_pct | 363.484 | 375.91 |
| ctr_eb_postgres_mem_mb | 4699.053 | 4717.8 |
| ctr_eb_rabbitmq_cpu_pct | 12.665 | 13.59 |
| ctr_eb_rabbitmq_mem_mb | 825.753 | 831.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 60.437 | 62.58 |
| in_flight | 91.694 | 590 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 21.408 | 45 |
| pg_conn_idle | 42.592 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1453.424 | 1896.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26043.114 | 29492.5 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59879.398 | 68460.8 |
| pg_tup_ins_per_s | 10726.269 | 19573.2 |
| pg_tup_upd_per_s | 21512.361 | 24022.9 |
| pg_wal_mb_per_s | 37.278 | 77.87 |
| pgb_cl_active | 450.98 | 458 |
| pgb_cl_waiting | 7.02 | 102 |
| pgb_maxwait_ms | 0.309 | 4.69 |
| pgb_sv_active | 31.918 | 64 |
| pgb_sv_idle | 32.082 | 60 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.59 | 13339.2 |
| produced_total | 468753.02 | 777559 |
| publish_mb_per_s | 44.333 | 71.21 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,594 records are Completed |
| record_counts | pass | 117,594 records found, 117,594 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,619 events applied by workers, 814,619 generated |
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
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 34.559 | 96.767 | 101.375 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 37.887 | 95.743 | 95.743 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 38.911 | 95.231 | 125.439 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 31.615 | 83.967 | 97.279 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2679 | 2679 | 0 | 11.967 | 57.599 | 136.191 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 13.183 | 56.319 | 97.791 |
| t-108 | 32 | 1 | 58.283 | 31.794 | 694 | 0 | 2040 | 2040 | 0 | 12.351 | 56.063 | 93.183 |
| t-124 | 11 | 1 | 14.909 | 2.842 | 1576 | 0 | 201 | 201 | 0 | 13.119 | 55.807 | 77.311 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 12.223 | 55.039 | 114.687 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 12.543 | 54.783 | 116.223 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2738 | 2738 | 0 | 12.159 | 54.527 | 108.543 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 12.415 | 54.271 | 110.079 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 12.415 | 54.015 | 108.543 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 13.183 | 54.015 | 88.575 |
| t-216 | 19 | 1 | 60.988 | 19.724 | 3889 | 0 | 1234 | 1234 | 0 | 13.375 | 53.503 | 72.703 |
| t-090 | 7 | 1 | 39.93 | 4.697 | 242 | 0 | 286 | 286 | 0 | 14.335 | 52.991 | 60.927 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7676 | 7676 | 0 | 12.607 | 52.735 | 140.287 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 12.479 | 52.735 | 96.767 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4616 | 4616 | 0 | 12.671 | 52.479 | 108.543 |
| t-393 | 30 | 1 | 27.649 | 13.621 | 134 | 0 | 890 | 890 | 0 | 11.967 | 52.479 | 89.599 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 12.095 | 52.223 | 140.287 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 0 | 12.543 | 52.223 | 92.159 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 12.095 | 52.223 | 102.911 |
| t-196 | 21 | 1 | 94.353 | 33.348 | 2244 | 0 | 2079 | 2079 | 0 | 12.223 | 51.967 | 90.111 |
| t-272 | 14 | 1 | 100.429 | 24.962 | 394 | 0 | 1495 | 1495 | 0 | 12.351 | 51.967 | 111.615 |
