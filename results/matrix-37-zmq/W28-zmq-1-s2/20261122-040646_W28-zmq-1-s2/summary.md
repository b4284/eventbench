# W28-zmq-1-s2 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.521s, closing 1.23s, drain 4.081s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 353961 | 353961 | 5834.512 | 5793.396 | 8582.9 | 0 | 0 | 0 | 315 | 2019 | 20255 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 280741 | 5085.831 | 3579.903 | 12894.207 | 18366.463 | 19136.511 | 21790.719 |
| e2e_from_send | 280741 | 573.859 | 463.615 | 1116.159 | 2672.639 | 4089.855 | 9142.271 |
| producer_lag | 280741 | 4511.973 | 2965.503 | 12140.543 | 17793.023 | 18153.471 | 18595.839 |
| publish_call | 353961 | 301.672 | 200.703 | 691.199 | 1549.311 | 2625.535 | 3723.263 |
| queue_wait | 280741 | 501.888 | 398.847 | 1019.391 | 2408.447 | 3719.167 | 6852.607 |
| broker_dwell | 280741 | 204.763 | 51.199 | 561.663 | 2142.207 | 3500.031 | 6696.959 |
| receive_call | 328359 | 28.279 | 10.247 | 71.359 | 272.383 | 456.959 | 726.527 |
| ctx_load | 280741 | 14.569 | 8.439 | 25.823 | 130.815 | 347.135 | 946.175 |
| process | 280741 | 11.724 | 0.001 | 0.003 | 0.016 | 2646.015 | 3004.415 |
| ctx_commit | 280741 | 45.646 | 27.039 | 85.823 | 326.143 | 589.823 | 1730.559 |
| ack | 267209 | 0.003 | 0.002 | 0.005 | 0.014 | 0.032 | 41.951 |
| service | 280741 | 71.971 | 40.063 | 119.551 | 440.319 | 2760.703 | 4415.487 |
| e2e_large | 235 | 8621.315 | 7704.575 | 16146.431 | 20299.775 | 20873.215 | 20873.215 |
| ready_wait | 280741 | 46.176 | 12.311 | 124.991 | 398.591 | 1101.823 | 2144.255 |
| blocked | 280741 | 33.02 | 11.007 | 82.879 | 273.663 | 937.471 | 2063.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5793.396 | 8582.9 |
| applied_total | 195032.816 | 340790 |
| bench_cpu_cores | 4.773 | 5.899 |
| bench_cpu_pct | 59.657 | 73.74 |
| bench_gc_gen0 | 0.571 | 2 |
| bench_gc_gen1 | 0.469 | 1 |
| bench_gc_gen2 | 0.449 | 1 |
| bench_gc_heap_mb | 1113.602 | 1574.2 |
| bench_mem_mb | 1962.81 | 2078.7 |
| bench_threadpool_queue | 4.939 | 164 |
| bench_threadpool_threads | 13.918 | 17 |
| broker_backlog | 20599.061 | 38802 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1610.02 | 3444 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 393633.653 | 699724 |
| octopus_missing_reports_total | 31386.673 | 52161 |
| octopus_mqtt_received_total | 131087.388 | 215592 |
| octopus_mqtt_sent_total | 138824.939 | 239396 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 23.224 | 38 |
| pg_active_loads | 11.837 | 33 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.949 | 100 |
| pg_conn_active | 69.347 | 107 |
| pg_conn_idle | 195.694 | 229 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4 | 29 |
| pg_conn_total | 265.041 | 266 |
| pg_db_size_mb | 3914.049 | 4410.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25335.545 | 28570.4 |
| pg_tup_del_per_s | 34084.949 | 43306 |
| pg_tup_fetch_per_s | 298780.798 | 358039 |
| pg_tup_ins_per_s | 44810.969 | 55926.3 |
| pg_tup_upd_per_s | 51728.678 | 62053.5 |
| pg_wal_mb_per_s | 119.303 | 159.72 |
| produced_per_s | 5834.512 | 8953.7 |
| produced_total | 196642.837 | 344134 |
| publish_mb_per_s | 28.232 | 55.86 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 177,440 records are Completed |
| record_counts | pass | 177,440 records found, 45,637 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 386 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 353,961 events applied by workers, 353,961 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 20,255 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 274 ms, max 2063 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6579 seq 25: ready 145 ms before start, 124 ms of it with an idle slot in group 222
- [cross_source_blocking] source 11849 seq 25: ready 133 ms before start, 124 ms of it with an idle slot in group 383
- [cross_source_blocking] source 10041 seq 66: ready 170 ms before start, 142 ms of it with an idle slot in group 342
- [cross_source_blocking] source 2187 seq 15: ready 121 ms before start, 121 ms of it with an idle slot in group 62
- [cross_source_blocking] source 13755 seq 23: ready 159 ms before start, 139 ms of it with an idle slot in group 438
- [cross_source_blocking] source 6800 seq 13: ready 331 ms before start, 331 ms of it with an idle slot in group 231
- [cross_source_blocking] source 6752 seq 34: ready 253 ms before start, 226 ms of it with an idle slot in group 230
- [cross_source_blocking] source 6776 seq 21: ready 231 ms before start, 231 ms of it with an idle slot in group 231
- [cross_source_blocking] source 8038 seq 41: ready 285 ms before start, 208 ms of it with an idle slot in group 271
- [cross_source_blocking] source 15795 seq 9: ready 308 ms before start, 169 ms of it with an idle slot in group 494
- [cross_source_blocking] source 6768 seq 18: ready 230 ms before start, 230 ms of it with an idle slot in group 231
- [cross_source_blocking] source 3036 seq 16: ready 145 ms before start, 145 ms of it with an idle slot in group 102
- [cross_source_blocking] source 10105 seq 18: ready 160 ms before start, 134 ms of it with an idle slot in group 343
- [cross_source_blocking] source 5985 seq 20: ready 231 ms before start, 231 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7079 seq 47: ready 223 ms before start, 223 ms of it with an idle slot in group 239
- [cross_source_blocking] source 1905 seq 24: ready 294 ms before start, 294 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1012 seq 27: ready 403 ms before start, 403 ms of it with an idle slot in group 31
- [cross_source_blocking] source 11761 seq 7: ready 361 ms before start, 361 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11829 seq 33: ready 402 ms before start, 402 ms of it with an idle slot in group 383
- [cross_source_blocking] source 5550 seq 32: ready 640 ms before start, 640 ms of it with an idle slot in group 190

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-454 | 7 | 1 | 292.242 | 33.739 | 266 | 0 | 1824 | 1824 | 0 | 5570.559 | 20578.303 | 21889.023 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 372 | 372 | 0 | 6750.207 | 19791.871 | 20971.519 |
| t-390 | 10 | 1 | 78.314 | 12.785 | 167 | 0 | 680 | 680 | 0 | 3751.935 | 19791.871 | 20447.231 |
| t-479 | 34 | 1 | 80.711 | 45.517 | 3383 | 0 | 2579 | 2579 | 0 | 4259.839 | 19136.511 | 21364.735 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2705 | 2705 | 0 | 4915.199 | 19005.439 | 20054.015 |
| t-118 | 15 | 1 | 52.306 | 13.232 | 138 | 0 | 730 | 730 | 0 | 3063.807 | 18743.295 | 19791.871 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 14326 | 14326 | 0 | 3801.087 | 18743.295 | 20840.447 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 2362 | 2362 | 0 | 3309.567 | 18743.295 | 20185.087 |
| t-310 | 6 | 1 | 152.947 | 16.027 | 1910 | 0 | 905 | 905 | 0 | 3047.423 | 18743.295 | 20447.231 |
| t-486 | 17 | 1 | 116.681 | 31.091 | 1259 | 0 | 1738 | 1738 | 0 | 3244.031 | 18743.295 | 20840.447 |
| t-110 | 9 | 1 | 40.981 | 6.184 | 2746 | 0 | 382 | 382 | 0 | 3145.727 | 18612.223 | 18743.295 |
| t-191 | 11 | 1 | 82.903 | 14.742 | 3529 | 0 | 798 | 798 | 0 | 2670.591 | 18612.223 | 19660.799 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 8074 | 8074 | 0 | 3719.167 | 18612.223 | 19267.583 |
| t-246 | 32 | 5 | 407.356 | 215.226 | 494 | 0 | 11666 | 11666 | 0 | 3883.007 | 18612.223 | 19136.511 |
| t-247 | 8 | 1 | 53.926 | 7.182 | 506 | 0 | 453 | 453 | 0 | 5570.559 | 18612.223 | 18874.367 |
| t-270 | 21 | 1 | 93.764 | 31.715 | 556 | 0 | 1790 | 1790 | 0 | 3325.951 | 18612.223 | 18874.367 |
| t-287 | 17 | 1 | 155.915 | 45.751 | 158 | 0 | 2508 | 2508 | 0 | 3063.807 | 18612.223 | 19136.511 |
| t-318 | 7 | 1 | 68.933 | 8.407 | 686 | 0 | 517 | 517 | 0 | 4685.823 | 18612.223 | 19005.439 |
| t-326 | 10 | 2 | 352.999 | 60.479 | 1837 | 0 | 3286 | 3286 | 0 | 4112.383 | 18612.223 | 20054.015 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2378 | 2378 | 0 | 4063.231 | 18612.223 | 19005.439 |
| t-414 | 21 | 1 | 71.312 | 24.948 | 347 | 0 | 1422 | 1422 | 0 | 3768.319 | 18612.223 | 20447.231 |
| t-462 | 10 | 1 | 55.668 | 9.033 | 2006 | 0 | 472 | 472 | 0 | 3391.487 | 18612.223 | 20185.087 |
| t-007 | 17 | 1 | 110.62 | 30.423 | 140 | 0 | 1657 | 1657 | 0 | 3620.863 | 18481.151 | 19136.511 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1548 | 1548 | 0 | 2883.583 | 18481.151 | 21102.591 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2516 | 2516 | 0 | 4259.839 | 18481.151 | 19005.439 |
