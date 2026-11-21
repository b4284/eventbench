# V28-mosq-1-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.414s, closing 1.335s, drain 44.246s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 632831 | 632831 | 10774.5 | 10765.088 | 14292.1 | 0 | 0 | 0 | 296 | 3284 | 45620 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 469780 | 13020.814 | 13262.847 | 16687.103 | 18939.903 | 20627.455 | 23330.815 |
| e2e_from_send | 469780 | 363.085 | 285.183 | 598.527 | 2197.503 | 3311.615 | 6643.711 |
| producer_lag | 469780 | 12657.723 | 12902.399 | 16310.271 | 18579.455 | 20119.551 | 20725.759 |
| publish_call | 632831 | 197.177 | 156.799 | 385.023 | 814.079 | 1565.695 | 2211.839 |
| queue_wait | 469780 | 302.034 | 232.447 | 528.895 | 1858.559 | 3092.479 | 5881.855 |
| broker_dwell | 469780 | 124.236 | 39.967 | 265.471 | 1702.911 | 2938.879 | 5791.743 |
| receive_call | 570523 | 38.442 | 17.743 | 97.023 | 294.399 | 464.127 | 531.967 |
| ctx_load | 469780 | 13.393 | 10.039 | 28.447 | 53.343 | 76.735 | 410.111 |
| process | 469780 | 10.785 | 0.001 | 0.002 | 0.009 | 2611.199 | 3002.367 |
| ctx_commit | 469780 | 36.846 | 29.199 | 73.343 | 130.751 | 455.167 | 848.383 |
| ack | 382687 | 0.002 | 0.001 | 0.003 | 0.008 | 0.025 | 36.735 |
| service | 469780 | 61.052 | 43.487 | 92.799 | 176.383 | 2674.687 | 3735.551 |
| e2e_large | 204 | 15405.247 | 15720.447 | 18743.295 | 20807.679 | 22380.543 | 22380.543 |
| ready_wait | 469780 | 49.651 | 22.351 | 123.263 | 397.823 | 715.263 | 1463.295 |
| blocked | 469780 | 42.12 | 20.863 | 98.303 | 326.655 | 631.807 | 849.407 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10765.088 | 14292.1 |
| applied_total | 335164.796 | 606770 |
| bench_cpu_cores | 2.555 | 5.061 |
| bench_cpu_pct | 25.549 | 50.61 |
| bench_gc_gen0 | 0.633 | 2 |
| bench_gc_gen1 | 0.469 | 1 |
| bench_gc_gen2 | 0.449 | 1 |
| bench_gc_heap_mb | 1453.255 | 2225.1 |
| bench_mem_mb | 2381.776 | 2592.4 |
| bench_threadpool_queue | 1.571 | 36 |
| bench_threadpool_threads | 18.306 | 23 |
| broker_backlog | 51344.408 | 128059 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 2015.633 | 5223 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 679131.776 | 1247514 |
| octopus_missing_reports_total | 28607.939 | 47989 |
| octopus_mqtt_received_total | 136530.224 | 231165 |
| octopus_mqtt_sent_total | 227082.633 | 395188 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.469 | 35 |
| pg_active_loads | 13.388 | 53 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.983 | 100 |
| pg_conn_active | 44.755 | 113 |
| pg_conn_idle | 215.714 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.735 | 34 |
| pg_conn_total | 260.469 | 262 |
| pg_db_size_mb | 3625.469 | 4305.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25514.018 | 28633.9 |
| pg_tup_del_per_s | 32661.11 | 42103.1 |
| pg_tup_fetch_per_s | 311766.518 | 369274 |
| pg_tup_ins_per_s | 46224.182 | 51392 |
| pg_tup_upd_per_s | 55964.855 | 63587.7 |
| pg_wal_mb_per_s | 123.689 | 201.93 |
| produced_per_s | 10774.5 | 15213.4 |
| produced_total | 337180.429 | 607812 |
| publish_mb_per_s | 36.672 | 50.55 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 191,954 records are Completed |
| record_counts | pass | 191,954 records found, 84,299 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 347 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 632,831 events applied by workers, 632,831 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 45,620 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 327 ms, max 849 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15901 seq 9: ready 107 ms before start, 105 ms of it with an idle slot in group 497
- [cross_source_blocking] source 15933 seq 9: ready 112 ms before start, 110 ms of it with an idle slot in group 497
- [cross_source_blocking] source 7977 seq 26: ready 100 ms before start, 100 ms of it with an idle slot in group 269
- [cross_source_blocking] source 14968 seq 28: ready 288 ms before start, 288 ms of it with an idle slot in group 469
- [cross_source_blocking] source 9280 seq 2: ready 678 ms before start, 171 ms of it with an idle slot in group 319
- [cross_source_blocking] source 13477 seq 6: ready 160 ms before start, 110 ms of it with an idle slot in group 429
- [cross_source_blocking] source 1502 seq 35: ready 141 ms before start, 104 ms of it with an idle slot in group 45
- [cross_source_blocking] source 6974 seq 13: ready 180 ms before start, 125 ms of it with an idle slot in group 237
- [cross_source_blocking] source 15277 seq 17: ready 189 ms before start, 102 ms of it with an idle slot in group 477
- [cross_source_blocking] source 3153 seq 24: ready 149 ms before start, 122 ms of it with an idle slot in group 105
- [cross_source_blocking] source 3057 seq 28: ready 177 ms before start, 142 ms of it with an idle slot in group 103
- [cross_source_blocking] source 2515 seq 43: ready 233 ms before start, 233 ms of it with an idle slot in group 75
- [cross_source_blocking] source 11338 seq 60: ready 261 ms before start, 261 ms of it with an idle slot in group 369
- [cross_source_blocking] source 4727 seq 33: ready 262 ms before start, 237 ms of it with an idle slot in group 163
- [cross_source_blocking] source 2083 seq 10: ready 260 ms before start, 260 ms of it with an idle slot in group 61
- [cross_source_blocking] source 14212 seq 13: ready 260 ms before start, 240 ms of it with an idle slot in group 455
- [cross_source_blocking] source 4508 seq 12: ready 100 ms before start, 100 ms of it with an idle slot in group 157
- [cross_source_blocking] source 5676 seq 38: ready 100 ms before start, 100 ms of it with an idle slot in group 195
- [cross_source_blocking] source 14532 seq 14: ready 159 ms before start, 159 ms of it with an idle slot in group 461
- [cross_source_blocking] source 3846 seq 22: ready 260 ms before start, 260 ms of it with an idle slot in group 131

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 122 | 122 | 0 | 14548.991 | 21364.735 | 22413.311 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 300 | 300 | 0 | 15335.423 | 20840.447 | 20840.447 |
| t-135 | 6 | 1 | 32.979 | 3.364 | 791 | 0 | 150 | 150 | 0 | 13303.807 | 20185.087 | 20316.159 |
| t-015 | 9 | 1 | 88.66 | 13.135 | 3462 | 0 | 613 | 613 | 0 | 13303.807 | 20054.015 | 20971.519 |
| t-289 | 6 | 1 | 145.931 | 15.635 | 1711 | 0 | 702 | 702 | 0 | 13565.951 | 20054.015 | 20447.231 |
| t-195 | 9 | 1 | 160.79 | 25.652 | 2299 | 0 | 1077 | 1077 | 0 | 13893.631 | 19922.943 | 21626.879 |
| t-431 | 6 | 1 | 81.539 | 8.123 | 596 | 0 | 321 | 321 | 0 | 13369.343 | 19922.943 | 20054.015 |
| t-025 | 6 | 1 | 105.516 | 11.154 | 954 | 0 | 490 | 490 | 0 | 13828.095 | 19660.799 | 20447.231 |
| t-057 | 17 | 1 | 139.929 | 41.466 | 290 | 0 | 1903 | 1903 | 0 | 12976.127 | 19660.799 | 20447.231 |
| t-159 | 6 | 1 | 101.348 | 9.698 | 295 | 0 | 414 | 414 | 0 | 13238.271 | 19660.799 | 20316.159 |
| t-367 | 18 | 1 | 118.037 | 34.134 | 1001 | 0 | 1549 | 1549 | 0 | 13369.343 | 19660.799 | 20971.519 |
| t-369 | 14 | 2 | 287.936 | 70.703 | 1313 | 0 | 3137 | 3137 | 0 | 12845.055 | 19660.799 | 20316.159 |
| t-045 | 47 | 3 | 179.274 | 139.699 | 1398 | 0 | 6240 | 6240 | 0 | 13238.271 | 19529.727 | 20971.519 |
| t-133 | 6 | 1 | 60.083 | 5.815 | 273 | 0 | 283 | 283 | 0 | 11796.479 | 19529.727 | 19791.871 |
| t-285 | 43 | 2 | 113.527 | 80.919 | 233 | 0 | 3623 | 3623 | 0 | 13500.415 | 19529.727 | 20971.519 |
| t-321 | 10 | 1 | 138.632 | 24.859 | 204 | 0 | 1110 | 1110 | 0 | 13434.879 | 19529.727 | 20447.231 |
| t-439 | 22 | 2 | 148.034 | 55.2 | 3339 | 0 | 2452 | 2452 | 0 | 13369.343 | 19529.727 | 21102.591 |
| t-487 | 9 | 1 | 211.444 | 30.916 | 755 | 0 | 1334 | 1334 | 0 | 13959.167 | 19529.727 | 21102.591 |
| t-227 | 17 | 1 | 115.847 | 31.596 | 850 | 0 | 1453 | 1453 | 0 | 14024.703 | 19398.655 | 20840.447 |
| t-303 | 32 | 1 | 86.277 | 46.445 | 659 | 0 | 2127 | 2127 | 0 | 13172.735 | 19398.655 | 20971.519 |
| t-305 | 8 | 1 | 135.017 | 17.647 | 436 | 0 | 796 | 796 | 0 | 13565.951 | 19398.655 | 20840.447 |
| t-307 | 16 | 1 | 57.296 | 15.459 | 200 | 0 | 713 | 713 | 0 | 12976.127 | 19398.655 | 20578.303 |
| t-317 | 32 | 1 | 85.87 | 46.88 | 1288 | 0 | 2061 | 2061 | 0 | 13238.271 | 19398.655 | 20709.375 |
| t-419 | 17 | 1 | 44.454 | 13.165 | 3214 | 0 | 580 | 580 | 0 | 13303.807 | 19398.655 | 20840.447 |
| t-481 | 12 | 1 | 263.464 | 49.674 | 1993 | 0 | 2268 | 2268 | 0 | 14024.703 | 19398.655 | 20840.447 |
