# R28-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.978s, closing 1.325s, drain 3.867s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 381178 | 381178 | 6078.296 | 6049.057 | 9473.8 | 0 | 0 | 0 | 201 | 1971 | 11876 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 261031 | 16774.861 | 16351.231 | 24838.143 | 29474.815 | 31195.135 | 33325.055 |
| e2e_from_send | 261031 | 334.318 | 267.775 | 553.983 | 1763.327 | 3117.055 | 6508.543 |
| producer_lag | 261031 | 16440.531 | 16015.359 | 24494.079 | 29196.287 | 30982.143 | 32489.471 |
| publish_call | 381178 | 213.003 | 180.735 | 393.471 | 774.655 | 1243.135 | 1375.231 |
| queue_wait | 261031 | 265.906 | 214.143 | 457.471 | 1243.135 | 2693.119 | 5431.295 |
| broker_dwell | 261031 | 66.756 | 26.479 | 112.191 | 1017.343 | 2521.087 | 5373.951 |
| receive_call | 325367 | 50.208 | 24.447 | 133.247 | 330.495 | 481.791 | 513.023 |
| ctx_load | 261031 | 15.579 | 10.567 | 29.647 | 127.871 | 162.431 | 622.591 |
| process | 261031 | 10.725 | 0.001 | 0.003 | 0.015 | 2611.199 | 3012.607 |
| ctx_commit | 261031 | 42.064 | 26.703 | 78.079 | 306.175 | 533.503 | 735.743 |
| ack | 246495 | 0.003 | 0.001 | 0.005 | 0.014 | 0.077 | 49.695 |
| service | 261031 | 68.413 | 42.431 | 105.087 | 389.119 | 2695.167 | 3485.695 |
| e2e_large | 136 | 19179.264 | 18694.143 | 27492.351 | 30162.943 | 31571.967 | 31571.967 |
| ready_wait | 261031 | 32.263 | 14.359 | 74.879 | 261.375 | 718.335 | 1262.591 |
| blocked | 261031 | 29.365 | 14.087 | 65.151 | 236.799 | 703.487 | 1254.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6049.057 | 8466.7 |
| applied_total | 214442.959 | 362373 |
| bench_cpu_cores | 2.119 | 3.814 |
| bench_cpu_pct | 26.489 | 47.67 |
| bench_gc_gen0 | 0.49 | 2 |
| bench_gc_gen1 | 0.388 | 1 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 1035.457 | 1515.2 |
| bench_mem_mb | 1839.867 | 1927.5 |
| bench_threadpool_queue | 9.673 | 224 |
| bench_threadpool_threads | 13.918 | 15 |
| broker_backlog | 12884.98 | 17430 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 810.224 | 1956 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 438411.98 | 741078 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 93932.408 | 142082 |
| octopus_mqtt_sent_total | 144355.469 | 239175 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.347 | 40 |
| pg_active_loads | 12.204 | 98 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 45.857 | 136 |
| pg_conn_idle | 217.306 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.327 | 25 |
| pg_conn_total | 263.163 | 264 |
| pg_db_size_mb | 3539.712 | 4021.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26192.645 | 31327.8 |
| pg_tup_del_per_s | 32401.018 | 41050.7 |
| pg_tup_fetch_per_s | 289852.09 | 355851.7 |
| pg_tup_ins_per_s | 42548.602 | 51338.7 |
| pg_tup_upd_per_s | 56484.682 | 65987 |
| pg_wal_mb_per_s | 113.92 | 167.47 |
| produced_per_s | 6078.296 | 9187.8 |
| produced_total | 215253.184 | 364321 |
| publish_mb_per_s | 22.66 | 37.43 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5629/5629 source cursors at the expected last sequence |
| records_complete | pass | all 182,985 records are Completed |
| record_counts | pass | 182,985 records found, 52,056 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 344 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 381,178 events applied by workers, 381,178 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 11,876 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 237 ms, max 1254 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9227 seq 19: ready 138 ms before start, 108 ms of it with an idle slot in group 317
- [cross_source_blocking] source 15157 seq 16: ready 117 ms before start, 101 ms of it with an idle slot in group 473
- [cross_source_blocking] source 13322 seq 36: ready 105 ms before start, 105 ms of it with an idle slot in group 425
- [cross_source_blocking] source 13266 seq 26: ready 110 ms before start, 110 ms of it with an idle slot in group 425
- [cross_source_blocking] source 1754 seq 5: ready 240 ms before start, 212 ms of it with an idle slot in group 50
- [cross_source_blocking] source 1742 seq 8: ready 241 ms before start, 212 ms of it with an idle slot in group 50
- [cross_source_blocking] source 6218 seq 18: ready 113 ms before start, 113 ms of it with an idle slot in group 212
- [cross_source_blocking] source 6099 seq 13: ready 113 ms before start, 113 ms of it with an idle slot in group 209
- [cross_source_blocking] source 10172 seq 40: ready 101 ms before start, 101 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10361 seq 26: ready 108 ms before start, 108 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7058 seq 29: ready 109 ms before start, 109 ms of it with an idle slot in group 239
- [cross_source_blocking] source 11920 seq 32: ready 108 ms before start, 108 ms of it with an idle slot in group 383
- [cross_source_blocking] source 5666 seq 53: ready 117 ms before start, 117 ms of it with an idle slot in group 194
- [cross_source_blocking] source 4216 seq 24: ready 117 ms before start, 109 ms of it with an idle slot in group 140
- [cross_source_blocking] source 15778 seq 11: ready 123 ms before start, 115 ms of it with an idle slot in group 494
- [cross_source_blocking] source 7553 seq 38: ready 105 ms before start, 101 ms of it with an idle slot in group 260
- [cross_source_blocking] source 2288 seq 62: ready 105 ms before start, 105 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2219 seq 79: ready 106 ms before start, 106 ms of it with an idle slot in group 65
- [cross_source_blocking] source 12256 seq 9: ready 115 ms before start, 105 ms of it with an idle slot in group 389
- [cross_source_blocking] source 5655 seq 41: ready 110 ms before start, 110 ms of it with an idle slot in group 194

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 8415 | 8415 | 0 | 17432.575 | 31326.207 | 32636.927 |
| t-095 | 6 | 1 | 118.583 | 11.97 | 925 | 0 | 442 | 442 | 0 | 16252.927 | 31195.135 | 31326.207 |
| t-248 | 6 | 1 | 72.506 | 7.071 | 1190 | 0 | 221 | 221 | 0 | 16711.679 | 31195.135 | 31195.135 |
| t-281 | 10 | 1 | 75.584 | 13.289 | 2160 | 0 | 447 | 447 | 0 | 17694.719 | 31064.063 | 31195.135 |
| t-335 | 6 | 1 | 62.881 | 5.915 | 450 | 0 | 218 | 218 | 0 | 16711.679 | 31064.063 | 31326.207 |
| t-266 | 6 | 1 | 246.158 | 24.076 | 714 | 0 | 812 | 812 | 0 | 17956.863 | 30932.991 | 31326.207 |
| t-437 | 9 | 1 | 125.813 | 18.889 | 1067 | 0 | 714 | 714 | 0 | 18481.151 | 30670.847 | 31195.135 |
| t-452 | 6 | 1 | 44.703 | 4.676 | 1797 | 0 | 152 | 152 | 0 | 17694.719 | 30408.703 | 30670.847 |
| t-038 | 9 | 1 | 96.455 | 14.5 | 2056 | 0 | 498 | 498 | 0 | 17694.719 | 30277.631 | 31326.207 |
| t-146 | 6 | 1 | 93.941 | 10.079 | 246 | 0 | 403 | 403 | 0 | 16318.463 | 30277.631 | 30801.919 |
| t-176 | 11 | 1 | 89.606 | 15.549 | 1581 | 0 | 565 | 565 | 0 | 16908.287 | 30277.631 | 31588.351 |
| t-305 | 8 | 1 | 135.017 | 17.647 | 436 | 0 | 606 | 606 | 0 | 17301.503 | 30277.631 | 31195.135 |
| t-158 | 6 | 1 | 94.449 | 9.988 | 165 | 0 | 369 | 369 | 0 | 17825.791 | 30146.559 | 30146.559 |
| t-326 | 10 | 2 | 352.999 | 60.479 | 1837 | 0 | 2031 | 2031 | 0 | 17432.575 | 30146.559 | 31850.495 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 6196 | 6196 | 0 | 17301.503 | 30146.559 | 31850.495 |
| t-029 | 28 | 3 | 310.905 | 144.17 | 1732 | 0 | 5069 | 5069 | 0 | 16515.071 | 30015.487 | 31588.351 |
| t-272 | 14 | 2 | 217.93 | 54.168 | 394 | 0 | 1948 | 1948 | 0 | 16777.215 | 30015.487 | 31457.279 |
| t-485 | 12 | 1 | 69.507 | 13.448 | 880 | 0 | 501 | 501 | 0 | 16646.143 | 30015.487 | 31326.207 |
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 17339 | 17339 | 0 | 16908.287 | 29884.415 | 32636.927 |
| t-077 | 10 | 2 | 311.624 | 52.01 | 1566 | 0 | 1874 | 1874 | 0 | 16449.535 | 29884.415 | 31195.135 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 287 | 287 | 0 | 18219.007 | 29884.415 | 31588.351 |
| t-299 | 21 | 1 | 91.91 | 31.431 | 646 | 0 | 1153 | 1153 | 0 | 16777.215 | 29884.415 | 32374.783 |
| t-356 | 6 | 1 | 185.082 | 18.13 | 303 | 0 | 673 | 673 | 0 | 16515.071 | 29884.415 | 30277.631 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 1797 | 1797 | 0 | 16777.215 | 29753.343 | 31719.423 |
| t-032 | 52 | 2 | 62.648 | 52.969 | 1041 | 0 | 1981 | 1981 | 0 | 16646.143 | 29753.343 | 31195.135 |
