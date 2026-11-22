# W28-mosq-3-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.561s, closing 0.753s, drain 19.714s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 321181 | 321181 | 5243.526 | 5232.202 | 8191.8 | 0 | 0 | 0 | 294 | 1805 | 45912 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 248106 | 9039.458 | 9183.231 | 13156.351 | 16596.991 | 17924.095 | 21037.055 |
| e2e_from_send | 248106 | 674.668 | 532.479 | 1257.471 | 2863.103 | 4550.655 | 7618.559 |
| producer_lag | 248106 | 8364.787 | 8536.063 | 12402.687 | 15990.783 | 17252.351 | 17956.863 |
| publish_call | 321181 | 377.979 | 278.527 | 822.271 | 1492.991 | 1853.439 | 2018.303 |
| queue_wait | 248106 | 608.12 | 476.159 | 1168.383 | 2623.487 | 4366.335 | 6844.415 |
| broker_dwell | 248106 | 232.148 | 97.151 | 550.399 | 2283.519 | 4073.471 | 6688.767 |
| receive_call | 283248 | 34.766 | 13.535 | 90.879 | 320.255 | 473.599 | 525.311 |
| ctx_load | 248106 | 16.206 | 10.791 | 29.855 | 133.759 | 164.735 | 1042.943 |
| process | 248106 | 11.482 | 0.001 | 0.002 | 0.012 | 2627.583 | 3004.415 |
| ctx_commit | 248106 | 38.826 | 23.695 | 71.359 | 307.199 | 545.791 | 1324.031 |
| ack | 229721 | 0.002 | 0.001 | 0.004 | 0.012 | 0.038 | 1.151 |
| service | 248106 | 66.548 | 39.391 | 101.503 | 371.967 | 2736.127 | 3948.543 |
| e2e_large | 214 | 11510.832 | 11444.223 | 15638.527 | 19677.183 | 21037.055 | 21037.055 |
| ready_wait | 248106 | 69.558 | 38.655 | 166.271 | 419.583 | 890.367 | 1921.023 |
| blocked | 248106 | 58.183 | 34.495 | 139.903 | 294.655 | 766.463 | 1921.023 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5232.202 | 8191.8 |
| applied_total | 172197 | 310320 |
| bench_cpu_cores | 1.828 | 2.597 |
| bench_cpu_pct | 22.845 | 32.46 |
| bench_gc_gen0 | 0.48 | 2 |
| bench_gc_gen1 | 0.38 | 1 |
| bench_gc_gen2 | 0.38 | 1 |
| bench_gc_heap_mb | 1084.928 | 1707.6 |
| bench_mem_mb | 1819.122 | 1959.3 |
| bench_threadpool_queue | 0.52 | 11 |
| bench_threadpool_threads | 14.8 | 17 |
| broker_backlog | 33486.28 | 69383 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1543.92 | 4164 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 349419.04 | 625555 |
| octopus_missing_reports_total | 14371.4 | 23605 |
| octopus_mqtt_received_total | 98589.16 | 158544 |
| octopus_mqtt_sent_total | 125512.56 | 219163 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.86 | 44 |
| pg_active_loads | 6.08 | 24 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.987 | 100 |
| pg_conn_active | 46.16 | 95 |
| pg_conn_idle | 217.2 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.48 | 8 |
| pg_conn_total | 263.36 | 265 |
| pg_db_size_mb | 3740.424 | 4227.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26417.354 | 30458 |
| pg_tup_del_per_s | 33754.52 | 39363.9 |
| pg_tup_fetch_per_s | 328315.962 | 440025.3 |
| pg_tup_ins_per_s | 45387.734 | 50794.8 |
| pg_tup_upd_per_s | 56780.092 | 64358.8 |
| pg_wal_mb_per_s | 126.04 | 206.07 |
| produced_per_s | 5243.526 | 8594.5 |
| produced_total | 173740.92 | 311297 |
| publish_mb_per_s | 25.863 | 45.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 190,269 records are Completed |
| record_counts | pass | 190,269 records found, 41,963 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 368 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 321,181 events applied by workers, 321,181 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 45,912 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 295 ms, max 1921 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9672 seq 7: ready 320 ms before start, 320 ms of it with an idle slot in group 335
- [cross_source_blocking] source 6508 seq 24: ready 114 ms before start, 101 ms of it with an idle slot in group 222
- [cross_source_blocking] source 13780 seq 30: ready 117 ms before start, 103 ms of it with an idle slot in group 438
- [cross_source_blocking] source 5942 seq 18: ready 118 ms before start, 102 ms of it with an idle slot in group 206
- [cross_source_blocking] source 13844 seq 26: ready 129 ms before start, 114 ms of it with an idle slot in group 438
- [cross_source_blocking] source 774 seq 11: ready 186 ms before start, 152 ms of it with an idle slot in group 22
- [cross_source_blocking] source 13748 seq 40: ready 146 ms before start, 131 ms of it with an idle slot in group 438
- [cross_source_blocking] source 10091 seq 14: ready 252 ms before start, 139 ms of it with an idle slot in group 343
- [cross_source_blocking] source 10979 seq 15: ready 190 ms before start, 123 ms of it with an idle slot in group 359
- [cross_source_blocking] source 10046 seq 18: ready 250 ms before start, 141 ms of it with an idle slot in group 343
- [cross_source_blocking] source 5310 seq 23: ready 295 ms before start, 295 ms of it with an idle slot in group 182
- [cross_source_blocking] source 13858 seq 37: ready 114 ms before start, 114 ms of it with an idle slot in group 438
- [cross_source_blocking] source 8045 seq 38: ready 122 ms before start, 122 ms of it with an idle slot in group 271
- [cross_source_blocking] source 1932 seq 45: ready 121 ms before start, 121 ms of it with an idle slot in group 55
- [cross_source_blocking] source 8243 seq 17: ready 245 ms before start, 245 ms of it with an idle slot in group 279
- [cross_source_blocking] source 5342 seq 8: ready 303 ms before start, 270 ms of it with an idle slot in group 183
- [cross_source_blocking] source 11820 seq 33: ready 105 ms before start, 105 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11878 seq 21: ready 105 ms before start, 105 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11886 seq 31: ready 104 ms before start, 104 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11968 seq 34: ready 105 ms before start, 105 ms of it with an idle slot in group 383

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 351 | 351 | 0 | 10682.367 | 19791.871 | 21102.591 |
| t-142 | 15 | 1 | 58.416 | 14.227 | 413 | 0 | 714 | 714 | 0 | 9568.255 | 17825.791 | 19005.439 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2442 | 2442 | 0 | 10223.615 | 17825.791 | 19267.583 |
| t-287 | 17 | 1 | 155.915 | 45.751 | 158 | 0 | 2241 | 2241 | 0 | 9502.719 | 17825.791 | 18350.079 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 142 | 142 | 0 | 10289.151 | 17825.791 | 18087.935 |
| t-462 | 10 | 1 | 55.668 | 9.033 | 2006 | 0 | 417 | 417 | 0 | 10092.543 | 17825.791 | 18874.367 |
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 1004 | 1004 | 0 | 11010.047 | 17694.719 | 18481.151 |
| t-159 | 6 | 1 | 101.348 | 9.698 | 295 | 0 | 456 | 456 | 0 | 10027.007 | 17694.719 | 18350.079 |
| t-295 | 12 | 1 | 120.676 | 23.972 | 3396 | 0 | 1225 | 1225 | 0 | 9502.719 | 17694.719 | 18874.367 |
| t-423 | 11 | 1 | 79.469 | 14.335 | 726 | 0 | 694 | 694 | 0 | 9502.719 | 17694.719 | 18350.079 |
| t-454 | 7 | 1 | 292.242 | 33.739 | 266 | 0 | 1728 | 1728 | 0 | 9895.935 | 17694.719 | 19529.727 |
| t-479 | 34 | 1 | 80.711 | 45.517 | 3383 | 0 | 2339 | 2339 | 0 | 9437.183 | 17694.719 | 18874.367 |
| t-095 | 6 | 1 | 118.583 | 11.97 | 925 | 0 | 584 | 584 | 0 | 9764.863 | 17432.575 | 18219.007 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1333 | 1333 | 0 | 10289.151 | 17301.503 | 18219.007 |
| t-150 | 15 | 1 | 67.298 | 17.194 | 990 | 0 | 846 | 846 | 0 | 9371.647 | 17301.503 | 17825.791 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 751 | 751 | 0 | 11206.655 | 17301.503 | 18219.007 |
| t-006 | 14 | 1 | 54.493 | 12.159 | 1170 | 0 | 625 | 625 | 0 | 10027.007 | 17170.431 | 17956.863 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 7358 | 7358 | 0 | 9699.327 | 17170.431 | 18350.079 |
| t-486 | 17 | 1 | 116.681 | 31.091 | 1259 | 0 | 1569 | 1569 | 0 | 9175.039 | 17170.431 | 18481.151 |
| t-063 | 8 | 1 | 42.658 | 5.912 | 1296 | 0 | 297 | 297 | 0 | 10420.223 | 17039.359 | 17170.431 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2126 | 2126 | 0 | 9699.327 | 17039.359 | 18481.151 |
| t-414 | 21 | 1 | 71.312 | 24.948 | 347 | 0 | 1273 | 1273 | 0 | 9699.327 | 17039.359 | 18219.007 |
| t-118 | 15 | 1 | 52.306 | 13.232 | 138 | 0 | 649 | 649 | 0 | 9699.327 | 16908.287 | 17825.791 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 13005 | 13005 | 0 | 9502.719 | 16908.287 | 18743.295 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 2184 | 2184 | 0 | 9699.327 | 16908.287 | 18350.079 |
