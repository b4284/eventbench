# W28-zmq-3-s2 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.816s, closing 0.713s, drain 4.882s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 379405 | 379405 | 6282.378 | 6187.292 | 8910.4 | 0 | 0 | 0 | 335 | 2166 | 32710 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 306267 | 3782.001 | 4018.175 | 6819.839 | 8568.831 | 10059.775 | 14262.271 |
| e2e_from_send | 306267 | 609.998 | 449.535 | 1241.087 | 2938.879 | 4411.391 | 8765.439 |
| producer_lag | 306267 | 3172 | 3364.863 | 6111.231 | 7647.231 | 8708.095 | 9289.727 |
| publish_call | 379405 | 276.885 | 174.335 | 699.391 | 1386.495 | 2869.247 | 3991.551 |
| queue_wait | 306267 | 539.45 | 385.023 | 1147.903 | 2721.791 | 4093.951 | 8577.023 |
| broker_dwell | 306267 | 250.458 | 63.551 | 701.951 | 2482.175 | 3852.287 | 8437.759 |
| receive_call | 353556 | 26.073 | 9.559 | 65.183 | 254.591 | 450.047 | 765.951 |
| ctx_load | 306267 | 14.308 | 8.003 | 23.119 | 130.879 | 349.951 | 642.559 |
| process | 306267 | 11.732 | 0.001 | 0.003 | 0.017 | 2650.111 | 3041.279 |
| ctx_commit | 306267 | 44.476 | 26.591 | 76.415 | 323.839 | 570.367 | 1496.063 |
| ack | 289435 | 0.002 | 0.002 | 0.005 | 0.014 | 0.032 | 1.531 |
| service | 306267 | 70.549 | 38.847 | 115.071 | 412.671 | 2764.799 | 4263.935 |
| e2e_large | 255 | 6936.076 | 7024.639 | 10002.431 | 12337.151 | 14262.271 | 14262.271 |
| ready_wait | 306267 | 56.341 | 17.423 | 151.423 | 473.343 | 941.567 | 2285.567 |
| blocked | 306267 | 39.278 | 14.239 | 104.127 | 293.375 | 813.055 | 2285.567 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6187.292 | 8087.8 |
| applied_total | 204793.347 | 363160 |
| bench_cpu_cores | 4.443 | 5.69 |
| bench_cpu_pct | 55.533 | 71.13 |
| bench_gc_gen0 | 0.612 | 2 |
| bench_gc_gen1 | 0.49 | 2 |
| bench_gc_gen2 | 0.469 | 1 |
| bench_gc_heap_mb | 1106.124 | 1520.5 |
| bench_mem_mb | 1939.831 | 2087.6 |
| bench_threadpool_queue | 0.367 | 6 |
| bench_threadpool_threads | 14.347 | 20 |
| broker_backlog | 14203.816 | 22043 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 2066.102 | 5815 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 413223.408 | 723571 |
| octopus_missing_reports_total | 31717.878 | 50762 |
| octopus_mqtt_received_total | 130058.224 | 209239 |
| octopus_mqtt_sent_total | 140074.939 | 234136 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.939 | 35 |
| pg_active_loads | 8.714 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.988 | 100 |
| pg_conn_active | 55.551 | 102 |
| pg_conn_idle | 209.102 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.408 | 28 |
| pg_conn_total | 264.653 | 265 |
| pg_db_size_mb | 3881.353 | 4376.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 28315.218 | 33095.6 |
| pg_tup_del_per_s | 36774.529 | 48410.6 |
| pg_tup_fetch_per_s | 315317.955 | 406223 |
| pg_tup_ins_per_s | 48949.157 | 59126.3 |
| pg_tup_upd_per_s | 60526.935 | 75081.7 |
| pg_wal_mb_per_s | 139.165 | 238.67 |
| produced_per_s | 6282.378 | 9462.9 |
| produced_total | 206859.449 | 368975 |
| publish_mb_per_s | 30.114 | 42.45 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 198,750 records are Completed |
| record_counts | pass | 198,750 records found, 48,471 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 413 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 379,405 events applied by workers, 379,405 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 32,710 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 293 ms, max 2285 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5038 seq 22: ready 137 ms before start, 137 ms of it with an idle slot in group 175
- [cross_source_blocking] source 13785 seq 40: ready 179 ms before start, 164 ms of it with an idle slot in group 438
- [cross_source_blocking] source 11767 seq 8: ready 418 ms before start, 418 ms of it with an idle slot in group 382
- [cross_source_blocking] source 775 seq 11: ready 392 ms before start, 272 ms of it with an idle slot in group 22
- [cross_source_blocking] source 3414 seq 16: ready 388 ms before start, 388 ms of it with an idle slot in group 118
- [cross_source_blocking] source 8734 seq 13: ready 388 ms before start, 354 ms of it with an idle slot in group 303
- [cross_source_blocking] source 12874 seq 8: ready 388 ms before start, 388 ms of it with an idle slot in group 407
- [cross_source_blocking] source 14637 seq 10: ready 390 ms before start, 390 ms of it with an idle slot in group 463
- [cross_source_blocking] source 12882 seq 9: ready 390 ms before start, 390 ms of it with an idle slot in group 407
- [cross_source_blocking] source 8043 seq 37: ready 289 ms before start, 289 ms of it with an idle slot in group 271
- [cross_source_blocking] source 2176 seq 13: ready 390 ms before start, 342 ms of it with an idle slot in group 62
- [cross_source_blocking] source 6773 seq 15: ready 390 ms before start, 390 ms of it with an idle slot in group 231
- [cross_source_blocking] source 8738 seq 15: ready 277 ms before start, 277 ms of it with an idle slot in group 303
- [cross_source_blocking] source 5329 seq 25: ready 390 ms before start, 387 ms of it with an idle slot in group 182
- [cross_source_blocking] source 15078 seq 18: ready 101 ms before start, 101 ms of it with an idle slot in group 470
- [cross_source_blocking] source 5310 seq 21: ready 102 ms before start, 102 ms of it with an idle slot in group 182
- [cross_source_blocking] source 8032 seq 56: ready 101 ms before start, 101 ms of it with an idle slot in group 271
- [cross_source_blocking] source 5298 seq 26: ready 102 ms before start, 102 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5326 seq 25: ready 101 ms before start, 101 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6312 seq 19: ready 571 ms before start, 553 ms of it with an idle slot in group 215
- [cross_source_blocking] source 3784 seq 30: ready 605 ms before start, 605 ms of it with an idle slot in group 127

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 391 | 391 | 0 | 6258.687 | 12386.303 | 14286.847 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 159 | 159 | 0 | 5898.239 | 11599.871 | 13828.095 |
| t-359 | 52 | 1 | 54.904 | 47.153 | 132 | 0 | 2829 | 2829 | 0 | 4620.287 | 10747.903 | 13893.631 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2918 | 2918 | 0 | 5046.271 | 9699.327 | 10747.903 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5824 | 5824 | 0 | 4227.071 | 9633.791 | 11862.015 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 8682 | 8682 | 0 | 4521.983 | 9633.791 | 11534.335 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 931 | 931 | 0 | 5079.039 | 9502.719 | 10027.007 |
| t-462 | 10 | 1 | 55.668 | 9.033 | 2006 | 0 | 494 | 494 | 0 | 4587.519 | 9371.647 | 10485.759 |
| t-287 | 17 | 1 | 155.915 | 45.751 | 158 | 0 | 2661 | 2661 | 0 | 4063.231 | 9306.111 | 9764.863 |
| t-343 | 63 | 2 | 84.032 | 85.035 | 518 | 0 | 5266 | 5266 | 0 | 4079.615 | 9240.575 | 11075.583 |
| t-015 | 9 | 1 | 88.66 | 13.135 | 3462 | 0 | 778 | 778 | 0 | 5111.807 | 9175.039 | 10682.367 |
| t-095 | 6 | 1 | 118.583 | 11.97 | 925 | 0 | 676 | 676 | 0 | 4325.375 | 9175.039 | 9961.471 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1629 | 1629 | 0 | 4816.895 | 9109.503 | 10223.615 |
| t-159 | 6 | 1 | 101.348 | 9.698 | 295 | 0 | 547 | 547 | 0 | 4980.735 | 9109.503 | 9437.183 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2543 | 2543 | 0 | 4784.127 | 9109.503 | 11010.047 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 10503 | 10503 | 0 | 4587.519 | 9109.503 | 11337.727 |
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 1226 | 1226 | 0 | 6029.311 | 9043.967 | 10027.007 |
| t-246 | 32 | 5 | 407.356 | 215.226 | 494 | 0 | 12529 | 12529 | 0 | 4227.071 | 8978.431 | 11534.335 |
| t-295 | 12 | 1 | 120.676 | 23.972 | 3396 | 0 | 1437 | 1437 | 0 | 4423.679 | 8978.431 | 9961.471 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 20243 | 20243 | 0 | 4161.535 | 8978.431 | 13434.879 |
| t-390 | 10 | 1 | 78.314 | 12.785 | 167 | 0 | 711 | 711 | 0 | 4947.967 | 8912.895 | 9175.039 |
| t-486 | 17 | 1 | 116.681 | 31.091 | 1259 | 0 | 1834 | 1834 | 0 | 3768.319 | 8912.895 | 9568.255 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 2562 | 2562 | 0 | 4095.999 | 8781.823 | 10551.295 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2734 | 2734 | 0 | 3817.471 | 8716.287 | 11403.263 |
| t-142 | 15 | 1 | 58.416 | 14.227 | 413 | 0 | 854 | 854 | 0 | 4554.751 | 8716.287 | 9437.183 |
