# C3c-nats3R3_pg-standby-KILL-NODE2 — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222,nats://127.0.0.1:4223,nats://127.0.0.1:4224: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=1000, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 61.977s, closing 5.066s, drain 72.998s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 258076 | 258076 | 2949.934 | 3184.108 | 11465.6 | 861 | 0 | 0 | 95 | 1176 | 3014 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 7037 | 28648.336 | 22052.863 | 55050.239 | 60293.119 | 62586.879 | 63864.831 |
| e2e_from_send | 7037 | 9875.468 | 3424.255 | 35979.263 | 41517.055 | 44695.551 | 46104.575 |
| producer_lag | 7037 | 18772.69 | 18825.215 | 20873.215 | 21282.815 | 21348.351 | 21348.351 |
| publish_call | 605 | 3503.737 | 2074.623 | 3547.135 | 31932.415 | 32702.463 | 33456.127 |
| queue_wait | 7037 | 9771.881 | 3225.599 | 35979.263 | 41418.751 | 44695.551 | 46104.575 |
| broker_dwell | 7037 | 7684.985 | 2250.751 | 34832.383 | 41222.143 | 44466.175 | 46039.039 |
| receive_call | 140401 | 215.36 | 146.559 | 418.815 | 1081.343 | 6860.799 | 35225.599 |
| ctx_load | 7037 | 1.268 | 0.405 | 3.123 | 10.967 | 42.271 | 52.383 |
| process | 7037 | 9.851 | 0.001 | 0.002 | 0.008 | 2394.111 | 2836.479 |
| ctx_commit | 7037 | 1.529 | 0.601 | 3.179 | 12.679 | 46.975 | 61.887 |
| ack | 6790 | 0.027 | 0.003 | 0.011 | 0.274 | 4.991 | 22.911 |
| service | 7037 | 103.616 | 1.628 | 16.095 | 1986.559 | 29704.191 | 30015.487 |
| e2e_large | 3 | 20575.573 | 20758.527 | 21692.415 | 21692.415 | 21692.415 | 21692.415 |
| ready_wait | 7037 | 1408.004 | 42.815 | 1284.095 | 30310.399 | 37126.143 | 39845.887 |
| blocked | 7037 | 1407.9 | 42.111 | 1284.095 | 30310.399 | 37126.143 | 39845.887 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3184.108 | 11465.6 |
| applied_total | 180626.38 | 219047 |
| bench_cpu_cores | 1.057 | 2.491 |
| bench_cpu_pct | 4.404 | 10.38 |
| bench_gc_gen0 | 0.56 | 2 |
| bench_gc_gen1 | 0.18 | 2 |
| bench_gc_gen2 | 0.1 | 1 |
| bench_gc_heap_mb | 900.982 | 1029.6 |
| bench_mem_mb | 1328.316 | 1404.4 |
| bench_threadpool_queue | 13.82 | 562 |
| bench_threadpool_threads | 36.74 | 44 |
| broker_backlog | 22278.62 | 35229 |
| broker_backlog_mb | 66.893 | 106.639 |
| broker_consumer_resets | 80.02 | 146 |
| broker_delivery_gaps | 81.04 | 146 |
| ctr_eb_nats_1_cpu_pct | 192.67 | 363.79 |
| ctr_eb_nats_1_mem_mb | 6213.49 | 6316.2 |
| ctr_eb_nats_2_cpu_pct | 326.445 | 375.08 |
| ctr_eb_nats_2_mem_mb | 6515.414 | 6693.8 |
| ctr_eb_nats_3_cpu_pct | 184.65 | 359.59 |
| ctr_eb_nats_3_mem_mb | 6576.308 | 6668.1 |
| ctr_eb_pg_standby_cpu_pct | 4.734 | 14.62 |
| ctr_eb_pg_standby_mem_mb | 719.226 | 726.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 5.55 | 11.24 |
| ctr_eb_pgbouncer_2_mem_mb | 11.82 | 12.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 4.822 | 9.96 |
| ctr_eb_pgbouncer_3_mem_mb | 11.848 | 12.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 6.016 | 12.31 |
| ctr_eb_pgbouncer_4_mem_mb | 10.206 | 10.3 |
| ctr_eb_pgbouncer_cpu_pct | 5.185 | 10.06 |
| ctr_eb_pgbouncer_mem_mb | 12.494 | 12.9 |
| ctr_eb_postgres_cpu_pct | 80.007 | 177.31 |
| ctr_eb_postgres_mem_mb | 3550.222 | 3617.3 |
| duplicates_per_s | 8.236 | 229.8 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 30.436 | 71.24 |
| in_flight | 4386.62 | 22264 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.48 | 8 |
| pg_active_loads | 0.48 | 3 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 0.52 | 4 |
| pg_conn_idle | 191.48 | 192 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 192 | 192 |
| pg_db_size_mb | 1135.424 | 1213.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 2.582 | 12.69 |
| pg_replica_replay_lag_ms | 153.56 | 529.3 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 5384.378 | 9108 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 19522.176 | 36865.5 |
| pg_tup_ins_per_s | 3183.71 | 7208.6 |
| pg_tup_upd_per_s | 6690.144 | 11362.1 |
| pg_wal_mb_per_s | 9.505 | 19.41 |
| pgb_cl_active | 249.2 | 284 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 1 | 10 |
| pgb_sv_idle | 47 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 2949.934 | 10996.3 |
| produced_total | 185013 | 215023 |
| publish_mb_per_s | 9.672 | 41.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 49,516 records are Completed |
| record_counts | pass | 49,516 records found, 49,516 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 95 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 258,076 events applied by workers, 258,076 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 3,014 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 30310 ms, max 39836 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4181 seq 11: ready 322 ms before start, 322 ms of it with an idle slot in group 137
- [cross_source_blocking] source 2645 seq 4: ready 352 ms before start, 352 ms of it with an idle slot in group 82
- [cross_source_blocking] source 10773 seq 12: ready 358 ms before start, 358 ms of it with an idle slot in group 353
- [cross_source_blocking] source 10261 seq 29: ready 357 ms before start, 357 ms of it with an idle slot in group 344
- [cross_source_blocking] source 1141 seq 10: ready 368 ms before start, 368 ms of it with an idle slot in group 38
- [cross_source_blocking] source 3573 seq 28: ready 377 ms before start, 377 ms of it with an idle slot in group 121
- [cross_source_blocking] source 13205 seq 7: ready 377 ms before start, 377 ms of it with an idle slot in group 420
- [cross_source_blocking] source 13173 seq 39: ready 391 ms before start, 391 ms of it with an idle slot in group 418
- [cross_source_blocking] source 12437 seq 12: ready 389 ms before start, 389 ms of it with an idle slot in group 396
- [cross_source_blocking] source 12117 seq 11: ready 388 ms before start, 388 ms of it with an idle slot in group 387
- [cross_source_blocking] source 12341 seq 7: ready 388 ms before start, 388 ms of it with an idle slot in group 392
- [cross_source_blocking] source 4373 seq 12: ready 395 ms before start, 395 ms of it with an idle slot in group 149
- [cross_source_blocking] source 3061 seq 20: ready 391 ms before start, 391 ms of it with an idle slot in group 103
- [cross_source_blocking] source 14261 seq 11: ready 390 ms before start, 390 ms of it with an idle slot in group 457
- [cross_source_blocking] source 9013 seq 9: ready 390 ms before start, 390 ms of it with an idle slot in group 311
- [cross_source_blocking] source 7381 seq 17: ready 390 ms before start, 390 ms of it with an idle slot in group 253
- [cross_source_blocking] source 7989 seq 29: ready 411 ms before start, 411 ms of it with an idle slot in group 269
- [cross_source_blocking] source 8949 seq 4: ready 409 ms before start, 409 ms of it with an idle slot in group 309
- [cross_source_blocking] source 6357 seq 8: ready 412 ms before start, 412 ms of it with an idle slot in group 217
- [cross_source_blocking] source 8277 seq 19: ready 412 ms before start, 412 ms of it with an idle slot in group 281

## Error samples

- publish p28 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p18 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p8 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p24 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p12 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p20 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p22 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p14 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p26 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p10 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p30 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p16 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p6 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p27 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p3 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p4 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p0 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p2 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p11 attempt 1: NatsJSPublishNoResponseException: No response received from the server
- publish p25 attempt 1: NatsJSPublishNoResponseException: No response received from the server

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-460 | 71 | 1 | 38.387 | 45.799 | 141 | 0 | 557 | 557 | 1 | 62128.127 | 63963.135 | 63963.135 |
| t-101 | 6 | 1 | 59.322 | 5.337 | 198 | 0 | 75 | 75 | 0 | 19922.943 | 63176.703 | 63176.703 |
| t-223 | 13 | 1 | 201.174 | 42.551 | 2372 | 0 | 426 | 426 | 1 | 61865.983 | 62390.271 | 62390.271 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 1963 | 1963 | 6 | 60555.263 | 61603.839 | 61603.839 |
| t-001 | 118 | 3 | 55.376 | 107.593 | 154 | 0 | 1308 | 1308 | 1 | 22806.527 | 61341.695 | 61341.695 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 1238 | 1238 | 3 | 24379.391 | 60293.119 | 60293.119 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 3665 | 3665 | 11 | 50593.791 | 59506.687 | 59506.687 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 2308 | 2308 | 3 | 22413.311 | 59506.687 | 59768.831 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 2535 | 2535 | 8 | 26869.759 | 59506.687 | 59768.831 |
| t-357 | 21 | 1 | 100.557 | 34.889 | 2215 | 0 | 355 | 355 | 1 | 58720.255 | 59506.687 | 59506.687 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 1768 | 1768 | 7 | 57147.391 | 59244.543 | 59244.543 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 578 | 578 | 1 | 58195.967 | 58720.255 | 58720.255 |
| t-111 | 18 | 1 | 148.016 | 44.353 | 3401 | 0 | 478 | 478 | 2 | 57671.679 | 58720.255 | 58720.255 |
| t-161 | 41 | 2 | 114.059 | 77.269 | 339 | 0 | 836 | 836 | 5 | 57409.535 | 58720.255 | 58720.255 |
| t-359 | 52 | 1 | 47.313 | 40.634 | 132 | 0 | 457 | 457 | 2 | 56885.247 | 58720.255 | 58720.255 |
| t-061 | 148 | 3 | 41.981 | 102.713 | 1274 | 0 | 1178 | 1178 | 4 | 21757.951 | 58458.111 | 58458.111 |
| t-053 | 11 | 1 | 21.603 | 4.062 | 3589 | 0 | 63 | 63 | 0 | 58195.967 | 58195.967 | 58195.967 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 3178 | 3178 | 9 | 22544.383 | 58195.967 | 59506.687 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 3542 | 3542 | 8 | 55312.383 | 58195.967 | 58458.111 |
| t-190 | 6 | 1 | 129.585 | 13.528 | 241 | 0 | 138 | 138 | 0 | 18481.151 | 57933.823 | 57933.823 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 1225 | 1225 | 1 | 57147.391 | 57671.679 | 57671.679 |
| t-235 | 54 | 3 | 148.429 | 133.613 | 527 | 0 | 1336 | 1336 | 7 | 50331.647 | 57671.679 | 57671.679 |
| t-245 | 43 | 2 | 133.219 | 91.504 | 2232 | 0 | 906 | 906 | 2 | 26214.399 | 57671.679 | 57671.679 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 573 | 573 | 0 | 56623.103 | 57671.679 | 57671.679 |
| t-402 | 31 | 1 | 63.94 | 33.628 | 1454 | 0 | 375 | 375 | 2 | 57147.391 | 57671.679 | 57671.679 |
