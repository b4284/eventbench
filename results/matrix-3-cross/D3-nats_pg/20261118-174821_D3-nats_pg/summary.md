# D3-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.109s, closing 0.909s, drain 1.116s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814625 | 814625 | 12854.116 | 12855.569 | 19093.1 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642904 | 13.473 | 11.167 | 23.775 | 44.543 | 64.543 | 131.967 |
| e2e_from_send | 642904 | 11.292 | 9.495 | 19.519 | 36.415 | 55.391 | 125.311 |
| producer_lag | 642904 | 2.181 | 1.46 | 5.023 | 12.575 | 23.935 | 49.119 |
| publish_call | 400750 | 3.656 | 2.675 | 7.035 | 16.927 | 40.383 | 699.903 |
| queue_wait | 642904 | 5.471 | 3.977 | 11.319 | 24.655 | 39.391 | 81.663 |
| broker_dwell | 642904 | 2.47 | 1.303 | 6.031 | 15.951 | 31.439 | 76.543 |
| receive_call | 664231 | 48.758 | 25.327 | 108.799 | 397.567 | 801.791 | 1028.607 |
| ctx_load | 642904 | 0.828 | 0.542 | 1.504 | 4.583 | 16.655 | 35.903 |
| process | 642904 | 0.002 | 0.001 | 0.002 | 0.004 | 0.038 | 10.351 |
| ctx_commit | 642904 | 2.023 | 1.66 | 3.143 | 7.079 | 18.351 | 64.127 |
| ack | 637933 | 0.019 | 0.002 | 0.006 | 0.219 | 3.245 | 15.839 |
| service | 642904 | 5.821 | 4.883 | 10.087 | 19.871 | 37.951 | 96.767 |
| e2e_large | 331 | 47.742 | 44.895 | 72.639 | 101.183 | 131.967 | 131.967 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12855.569 | 13444.7 |
| applied_total | 468737.204 | 777585 |
| bench_cpu_cores | 4.35 | 4.997 |
| bench_cpu_pct | 18.123 | 20.82 |
| bench_gc_gen0 | 2.898 | 4 |
| bench_gc_gen1 | 2.429 | 4 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 783.794 | 963.2 |
| bench_mem_mb | 1215.196 | 1320.8 |
| bench_threadpool_queue | 9.612 | 93 |
| bench_threadpool_threads | 34.571 | 44 |
| broker_backlog | 139.163 | 346 |
| broker_backlog_mb | 0.71 | 5.185 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.673 | 2.14 |
| ctr_eb_kafka_mem_mb | 2941.143 | 3096.6 |
| ctr_eb_mongo_cpu_pct | 1.988 | 15.62 |
| ctr_eb_mongo_mem_mb | 4016.537 | 4165.6 |
| ctr_eb_nats_cpu_pct | 413.91 | 421.99 |
| ctr_eb_nats_mem_mb | 2131.216 | 2460.5 |
| ctr_eb_pgbouncer_cpu_pct | 63.818 | 67.64 |
| ctr_eb_pgbouncer_mem_mb | 14.235 | 15.5 |
| ctr_eb_postgres_cpu_pct | 362.497 | 399.54 |
| ctr_eb_postgres_mem_mb | 4529.955 | 4559.5 |
| ctr_eb_rabbitmq_cpu_pct | 13.257 | 14.86 |
| ctr_eb_rabbitmq_mem_mb | 877.965 | 890 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 59.642 | 61.82 |
| in_flight | 80.02 | 458 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.02 | 43 |
| pg_conn_idle | 48 | 63 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.02 | 65 |
| pg_db_size_mb | 1498.967 | 1942.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25722.602 | 29771.8 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59216.994 | 69719.3 |
| pg_tup_ins_per_s | 10563.157 | 19992.9 |
| pg_tup_upd_per_s | 21251.951 | 24430.4 |
| pg_wal_mb_per_s | 34.231 | 52.14 |
| pgb_cl_active | 455.633 | 456 |
| pgb_cl_waiting | 0.367 | 6 |
| pgb_maxwait_ms | 0.014 | 0.23 |
| pgb_sv_active | 20.959 | 64 |
| pgb_sv_idle | 43.041 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.116 | 13136.2 |
| produced_total | 468817.224 | 777633 |
| publish_mb_per_s | 44.336 | 71.21 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,595 records are Completed |
| record_counts | pass | 117,595 records found, 117,595 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,625 events applied by workers, 814,625 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 38.911 | 98.815 | 132.095 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 32.639 | 95.231 | 112.127 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 36.607 | 94.207 | 94.207 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 30.975 | 86.015 | 99.839 |
| t-053 | 11 | 1 | 11.553 | 2.172 | 3589 | 0 | 145 | 145 | 0 | 11.647 | 58.623 | 60.927 |
| t-139 | 16 | 1 | 17.601 | 4.973 | 1367 | 0 | 372 | 372 | 0 | 11.391 | 56.063 | 71.679 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 10.751 | 53.759 | 63.743 |
| t-042 | 20 | 1 | 43.187 | 15.348 | 209 | 0 | 939 | 939 | 0 | 11.647 | 53.247 | 82.431 |
| t-025 | 6 | 1 | 48.625 | 5.14 | 954 | 0 | 325 | 325 | 0 | 9.663 | 52.735 | 63.743 |
| t-138 | 9 | 1 | 30.7 | 4.513 | 228 | 0 | 286 | 286 | 0 | 10.815 | 52.223 | 55.039 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 11.199 | 51.711 | 58.879 |
| t-413 | 19 | 1 | 69.524 | 22.975 | 3585 | 0 | 1396 | 1396 | 0 | 11.263 | 51.199 | 60.415 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2679 | 2679 | 0 | 10.879 | 50.943 | 90.111 |
| t-494 | 35 | 1 | 33.067 | 18.747 | 2036 | 0 | 1179 | 1179 | 0 | 11.711 | 50.687 | 67.071 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 11.263 | 49.919 | 90.623 |
| t-258 | 52 | 1 | 41.292 | 35.498 | 2470 | 0 | 2371 | 2371 | 0 | 11.519 | 49.919 | 72.191 |
| t-014 | 34 | 1 | 31.511 | 17.12 | 214 | 0 | 1067 | 1067 | 0 | 10.495 | 49.407 | 74.751 |
| t-453 | 6 | 1 | 44.943 | 4.427 | 874 | 0 | 289 | 289 | 0 | 11.071 | 49.407 | 56.575 |
| t-426 | 14 | 1 | 31.182 | 7.183 | 830 | 0 | 452 | 452 | 0 | 11.007 | 49.151 | 56.831 |
| t-129 | 19 | 1 | 90.638 | 29.84 | 623 | 0 | 1862 | 1862 | 0 | 11.711 | 48.895 | 77.823 |
| t-193 | 19 | 1 | 12.672 | 4.096 | 1805 | 0 | 282 | 282 | 0 | 12.415 | 48.895 | 56.319 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 289 | 289 | 0 | 11.519 | 48.639 | 60.159 |
| t-467 | 28 | 1 | 18.189 | 8.297 | 250 | 0 | 595 | 595 | 0 | 11.071 | 48.639 | 62.207 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 11.583 | 48.639 | 77.823 |
| t-165 | 36 | 1 | 55.043 | 32.844 | 373 | 0 | 2053 | 2053 | 0 | 11.711 | 48.383 | 83.455 |
