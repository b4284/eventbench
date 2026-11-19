# batch-04-nats_pg — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.103s, closing 0.7s, drain 6.305s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814584 | 814584 | 12853.682 | 12825.353 | 19815.5 | 0 | 0 | 0 | 431 | 4254 | 74089 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642861 | 530.938 | 6.695 | 2001.919 | 4661.247 | 7016.447 | 10027.007 |
| e2e_from_send | 642861 | 529.807 | 5.371 | 2000.895 | 4661.247 | 7016.447 | 10027.007 |
| producer_lag | 642861 | 1.13 | 0.714 | 2.689 | 6.567 | 15.175 | 40.575 |
| publish_call | 529798 | 1.63 | 1.114 | 2.985 | 8.031 | 33.407 | 523.263 |
| queue_wait | 642861 | 515.271 | 2.269 | 1960.959 | 4612.095 | 6971.391 | 9650.175 |
| broker_dwell | 642861 | 514.532 | 1.383 | 1959.935 | 4612.095 | 6971.391 | 9650.175 |
| receive_call | 525861 | 50.504 | 24.575 | 117.631 | 424.703 | 819.199 | 1077.247 |
| ctx_load | 642861 | 0.767 | 0.494 | 1.288 | 4.479 | 20.191 | 45.279 |
| process | 642861 | 10.899 | 0.001 | 0.002 | 0.005 | 2623.487 | 3000.319 |
| ctx_commit | 642861 | 0.789 | 0.52 | 1.274 | 4.731 | 20.639 | 158.335 |
| ack | 625696 | 0.016 | 0.002 | 0.005 | 0.141 | 3.235 | 11.935 |
| service | 642861 | 14.536 | 2.149 | 4.911 | 30.191 | 2672.639 | 6955.007 |
| e2e_large | 331 | 4037.19 | 3776.511 | 6414.335 | 8626.175 | 9535.487 | 9535.487 |
| ready_wait | 642861 | 199.408 | 1.653 | 691.711 | 2836.479 | 5009.407 | 8191.999 |
| blocked | 642861 | 198.406 | 1.636 | 686.079 | 2832.383 | 5001.215 | 8183.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12825.353 | 14224.7 |
| applied_total | 462183.531 | 770506 |
| bench_cpu_cores | 4.982 | 5.335 |
| bench_cpu_pct | 20.756 | 22.23 |
| bench_gc_gen0 | 2.633 | 5 |
| bench_gc_gen1 | 1.327 | 3 |
| bench_gc_gen2 | 0.429 | 2 |
| bench_gc_heap_mb | 821.8 | 1408.3 |
| bench_mem_mb | 1351.243 | 1725.3 |
| bench_threadpool_queue | 10.918 | 95 |
| bench_threadpool_threads | 33.755 | 44 |
| broker_backlog | 6587.224 | 7558 |
| broker_backlog_mb | 107.546 | 195.943 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.781 | 2.27 |
| ctr_eb_kafka_mem_mb | 2010.231 | 2087.6 |
| ctr_eb_mssql_cpu_pct | 1.985 | 4.59 |
| ctr_eb_mssql_mem_mb | 4369.067 | 4386.9 |
| ctr_eb_nats_cpu_pct | 356.781 | 366.63 |
| ctr_eb_nats_mem_mb | 2329.067 | 2648.4 |
| ctr_eb_pgbouncer_cpu_pct | 60.192 | 62.23 |
| ctr_eb_pgbouncer_mem_mb | 22.698 | 24.7 |
| ctr_eb_postgres_cpu_pct | 262.204 | 279.51 |
| ctr_eb_postgres_mem_mb | 4803.165 | 4867.6 |
| ctr_eb_rabbitmq_cpu_pct | 13.661 | 14.58 |
| ctr_eb_rabbitmq_mem_mb | 906.284 | 934.6 |
| ctr_eb_redis_cpu_pct | 0.269 | 0.29 |
| ctr_eb_redis_mem_mb | 1353.514 | 1357.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 55.095 | 59.23 |
| in_flight | 6690.429 | 8237 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 1.714 | 6 |
| pg_conn_idle | 62.286 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1253.941 | 1690.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25622.522 | 28653.6 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 58782.086 | 67806.9 |
| pg_tup_ins_per_s | 10986.092 | 16678.4 |
| pg_tup_upd_per_s | 21154.484 | 23701 |
| pg_wal_mb_per_s | 33.428 | 45.34 |
| pgb_cl_active | 1000 | 1000 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.959 | 25 |
| pgb_sv_idle | 57.041 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.682 | 13124.5 |
| produced_total | 468873.959 | 777628 |
| publish_mb_per_s | 44.333 | 71.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,588 records are Completed |
| record_counts | pass | 117,588 records found, 117,588 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,584 events applied by workers, 814,584 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 74,089 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2830 ms, max 8182 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14413 seq 3: ready 109 ms before start, 106 ms of it with an idle slot in group 459
- [cross_source_blocking] source 13212 seq 13: ready 217 ms before start, 215 ms of it with an idle slot in group 421
- [cross_source_blocking] source 3513 seq 20: ready 157 ms before start, 157 ms of it with an idle slot in group 120
- [cross_source_blocking] source 4564 seq 7: ready 247 ms before start, 247 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4598 seq 11: ready 279 ms before start, 279 ms of it with an idle slot in group 160
- [cross_source_blocking] source 192 seq 20: ready 164 ms before start, 164 ms of it with an idle slot in group 4
- [cross_source_blocking] source 206 seq 13: ready 143 ms before start, 143 ms of it with an idle slot in group 4
- [cross_source_blocking] source 10395 seq 5: ready 349 ms before start, 349 ms of it with an idle slot in group 347
- [cross_source_blocking] source 2993 seq 7: ready 308 ms before start, 305 ms of it with an idle slot in group 99
- [cross_source_blocking] source 2984 seq 9: ready 167 ms before start, 165 ms of it with an idle slot in group 99
- [cross_source_blocking] source 6377 seq 2: ready 409 ms before start, 402 ms of it with an idle slot in group 218
- [cross_source_blocking] source 6367 seq 4: ready 154 ms before start, 142 ms of it with an idle slot in group 218
- [cross_source_blocking] source 7794 seq 7: ready 411 ms before start, 389 ms of it with an idle slot in group 264
- [cross_source_blocking] source 4886 seq 5: ready 451 ms before start, 438 ms of it with an idle slot in group 169
- [cross_source_blocking] source 4905 seq 7: ready 410 ms before start, 396 ms of it with an idle slot in group 169
- [cross_source_blocking] source 3896 seq 7: ready 311 ms before start, 311 ms of it with an idle slot in group 131
- [cross_source_blocking] source 7816 seq 12: ready 191 ms before start, 166 ms of it with an idle slot in group 264
- [cross_source_blocking] source 4894 seq 13: ready 256 ms before start, 241 ms of it with an idle slot in group 169
- [cross_source_blocking] source 4899 seq 7: ready 138 ms before start, 123 ms of it with an idle slot in group 169
- [cross_source_blocking] source 5049 seq 10: ready 434 ms before start, 433 ms of it with an idle slot in group 175

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 3555.327 | 8650.751 | 9568.255 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 0 | 7.967 | 8060.927 | 10027.007 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 1081.343 | 7831.551 | 8519.679 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 11.263 | 7733.247 | 8781.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 3293.183 | 7700.479 | 7700.479 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 497.663 | 7372.799 | 7897.087 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 790.527 | 7274.495 | 8323.071 |
| t-313 | 35 | 1 | 35.066 | 20.997 | 510 | 0 | 1315 | 1315 | 0 | 6.527 | 6914.047 | 7405.567 |
| t-040 | 119 | 2 | 26.219 | 52.801 | 1254 | 0 | 3453 | 3453 | 0 | 7.103 | 6750.207 | 7569.407 |
| t-429 | 200 | 2 | 18.117 | 61.748 | 1395 | 0 | 4071 | 4071 | 0 | 80.895 | 6651.903 | 7667.711 |
| t-253 | 26 | 1 | 73.629 | 32.505 | 1433 | 0 | 2037 | 2037 | 0 | 6.367 | 6488.063 | 7143.423 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 1028.095 | 6389.759 | 7405.567 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2670.591 | 6389.759 | 6946.815 |
| t-104 | 46 | 1 | 31.882 | 24.451 | 2163 | 0 | 1619 | 1619 | 0 | 7.935 | 6324.223 | 6979.583 |
| t-002 | 17 | 1 | 82.711 | 23.551 | 377 | 0 | 1507 | 1507 | 0 | 14.207 | 6225.919 | 6619.135 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13791 | 13791 | 0 | 7.935 | 6193.151 | 7503.871 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 0 | 9.279 | 6160.383 | 7143.423 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 135.167 | 6127.615 | 6815.743 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 5.727 | 6094.847 | 6782.975 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 2110 | 2110 | 0 | 454.655 | 6062.079 | 6422.527 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 15.039 | 6029.311 | 7307.263 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9184 | 9184 | 0 | 667.647 | 5537.791 | 6127.615 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1850 | 1850 | 0 | 15.231 | 5406.719 | 5865.471 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 438.271 | 5373.951 | 6258.687 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1901 | 1901 | 0 | 13.311 | 5341.183 | 5799.935 |
