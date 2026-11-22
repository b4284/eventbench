# W28-zmq-1-s1 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.37s, closing 1.486s, drain 3.822s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 417168 | 417168 | 6943.9 | 6950.562 | 9183.5 | 0 | 0 | 0 | 71 | 2027 | 14871 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 294823 | 16649.592 | 17448.959 | 21643.263 | 25133.055 | 26492.927 | 29540.351 |
| e2e_from_send | 294823 | 470.204 | 393.727 | 816.639 | 2059.263 | 3342.335 | 6135.807 |
| producer_lag | 294823 | 16179.383 | 17039.359 | 21168.127 | 24494.079 | 26148.863 | 26296.319 |
| publish_call | 417168 | 298.251 | 236.543 | 600.575 | 1382.399 | 2488.319 | 3665.919 |
| queue_wait | 294823 | 390.171 | 323.839 | 713.727 | 1688.575 | 3028.991 | 5758.975 |
| broker_dwell | 294823 | 109.891 | 8.687 | 319.231 | 1382.399 | 2795.519 | 5640.191 |
| receive_call | 387064 | 40.194 | 17.407 | 102.911 | 312.063 | 462.335 | 603.135 |
| ctx_load | 294823 | 14.641 | 9.279 | 25.983 | 130.751 | 165.759 | 613.887 |
| process | 294823 | 10.199 | 0.001 | 0.002 | 0.01 | 2598.911 | 3145.727 |
| ctx_commit | 294823 | 55.165 | 36.191 | 102.463 | 332.799 | 609.279 | 1395.711 |
| ack | 245897 | 0.002 | 0.001 | 0.003 | 0.009 | 0.02 | 32.607 |
| service | 294823 | 80.035 | 50.559 | 133.247 | 457.727 | 2670.591 | 4284.415 |
| e2e_large | 47 | 18641.942 | 19709.951 | 23134.207 | 24838.143 | 24838.143 | 24838.143 |
| ready_wait | 294823 | 36.547 | 7.135 | 104.383 | 413.439 | 794.111 | 1736.703 |
| blocked | 294823 | 24.079 | 6.907 | 58.207 | 276.991 | 614.399 | 1382.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6950.562 | 9183.5 |
| applied_total | 217648.44 | 399666 |
| bench_cpu_cores | 3.265 | 5.452 |
| bench_cpu_pct | 32.655 | 54.52 |
| bench_gc_gen0 | 0.4 | 1 |
| bench_gc_gen1 | 0.3 | 1 |
| bench_gc_gen2 | 0.28 | 1 |
| bench_gc_heap_mb | 1090.834 | 1921.5 |
| bench_mem_mb | 2010.32 | 2417.8 |
| bench_threadpool_queue | 6.74 | 94 |
| bench_threadpool_threads | 17.44 | 25 |
| broker_backlog | 21113.7 | 37129 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1397.56 | 4582 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 442101.62 | 823795 |
| octopus_missing_reports_total | 27714.26 | 51276 |
| octopus_mqtt_received_total | 114442.28 | 193537 |
| octopus_mqtt_sent_total | 124381.92 | 218360 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.36 | 39 |
| pg_active_loads | 10.28 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.95 | 100 |
| pg_conn_active | 64.16 | 100 |
| pg_conn_idle | 200.88 | 234 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 8.44 | 39 |
| pg_conn_total | 265.04 | 266 |
| pg_db_size_mb | 3891.324 | 4407.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25103.098 | 30451.9 |
| pg_tup_del_per_s | 34404.518 | 44044.3 |
| pg_tup_fetch_per_s | 298572.462 | 357748.4 |
| pg_tup_ins_per_s | 44852.868 | 55666.9 |
| pg_tup_upd_per_s | 51349.224 | 61516.8 |
| pg_wal_mb_per_s | 119.019 | 163.53 |
| produced_per_s | 6943.9 | 11653.1 |
| produced_total | 219046 | 400415 |
| publish_mb_per_s | 16.995 | 26.03 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 177,440 records are Completed |
| record_counts | pass | 177,440 records found, 57,758 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 386 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 417,168 events applied by workers, 417,168 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 14,871 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 277 ms, max 1382 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3276 seq 19: ready 109 ms before start, 109 ms of it with an idle slot in group 109
- [cross_source_blocking] source 11358 seq 24: ready 344 ms before start, 344 ms of it with an idle slot in group 371
- [cross_source_blocking] source 10554 seq 21: ready 112 ms before start, 101 ms of it with an idle slot in group 348
- [cross_source_blocking] source 7588 seq 19: ready 132 ms before start, 101 ms of it with an idle slot in group 261
- [cross_source_blocking] source 10924 seq 50: ready 180 ms before start, 180 ms of it with an idle slot in group 356
- [cross_source_blocking] source 7963 seq 18: ready 227 ms before start, 165 ms of it with an idle slot in group 268
- [cross_source_blocking] source 13395 seq 33: ready 227 ms before start, 227 ms of it with an idle slot in group 428
- [cross_source_blocking] source 4532 seq 11: ready 228 ms before start, 228 ms of it with an idle slot in group 157
- [cross_source_blocking] source 259 seq 30: ready 228 ms before start, 228 ms of it with an idle slot in group 5
- [cross_source_blocking] source 10330 seq 25: ready 143 ms before start, 107 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10546 seq 21: ready 255 ms before start, 229 ms of it with an idle slot in group 348
- [cross_source_blocking] source 11135 seq 36: ready 105 ms before start, 105 ms of it with an idle slot in group 363
- [cross_source_blocking] source 2506 seq 42: ready 114 ms before start, 110 ms of it with an idle slot in group 75
- [cross_source_blocking] source 9558 seq 29: ready 316 ms before start, 122 ms of it with an idle slot in group 332
- [cross_source_blocking] source 8974 seq 9: ready 120 ms before start, 120 ms of it with an idle slot in group 309
- [cross_source_blocking] source 11730 seq 20: ready 391 ms before start, 391 ms of it with an idle slot in group 380
- [cross_source_blocking] source 384 seq 15: ready 115 ms before start, 115 ms of it with an idle slot in group 11
- [cross_source_blocking] source 7024 seq 23: ready 126 ms before start, 104 ms of it with an idle slot in group 237
- [cross_source_blocking] source 5246 seq 30: ready 121 ms before start, 104 ms of it with an idle slot in group 181
- [cross_source_blocking] source 10458 seq 34: ready 127 ms before start, 111 ms of it with an idle slot in group 347

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-299 | 21 | 1 | 91.91 | 31.431 | 646 | 0 | 1216 | 1216 | 0 | 17432.575 | 26607.615 | 27656.191 |
| t-284 | 12 | 1 | 60.548 | 11.512 | 217 | 0 | 479 | 479 | 0 | 18087.935 | 26476.543 | 26607.615 |
| t-363 | 19 | 2 | 202.118 | 65.584 | 639 | 0 | 2578 | 2578 | 0 | 17432.575 | 26476.543 | 27656.191 |
| t-405 | 6 | 1 | 53.039 | 5.259 | 874 | 0 | 211 | 211 | 0 | 16908.287 | 26476.543 | 26738.687 |
| t-493 | 8 | 1 | 185.785 | 26.56 | 1419 | 0 | 1051 | 1051 | 0 | 17170.431 | 26476.543 | 27262.975 |
| t-067 | 11 | 1 | 188.777 | 34.722 | 3490 | 0 | 1363 | 1363 | 0 | 17301.503 | 26345.471 | 26607.615 |
| t-101 | 6 | 1 | 68.839 | 6.193 | 198 | 0 | 260 | 260 | 0 | 16646.143 | 26345.471 | 26607.615 |
| t-115 | 10 | 1 | 72.305 | 11.564 | 3658 | 0 | 469 | 469 | 0 | 18612.223 | 26345.471 | 26476.543 |
| t-219 | 13 | 1 | 71.086 | 15.025 | 272 | 0 | 565 | 565 | 0 | 18350.079 | 26345.471 | 26476.543 |
| t-356 | 6 | 1 | 185.082 | 18.13 | 303 | 0 | 776 | 776 | 0 | 17039.359 | 26345.471 | 26607.615 |
| t-381 | 11 | 1 | 102.042 | 19.181 | 134 | 0 | 780 | 780 | 0 | 17170.431 | 26345.471 | 26607.615 |
| t-069 | 31 | 1 | 30.759 | 15.322 | 2002 | 0 | 685 | 685 | 0 | 17170.431 | 26214.399 | 26607.615 |
| t-076 | 23 | 1 | 38.578 | 14.58 | 4045 | 0 | 611 | 611 | 0 | 17432.575 | 26214.399 | 26476.543 |
| t-148 | 6 | 1 | 136.453 | 12.472 | 715 | 0 | 513 | 513 | 0 | 16908.287 | 26214.399 | 26607.615 |
| t-180 | 19 | 1 | 102.843 | 30.984 | 152 | 0 | 1222 | 1222 | 0 | 17432.575 | 26214.399 | 26869.759 |
| t-227 | 17 | 1 | 115.847 | 31.596 | 850 | 0 | 1240 | 1240 | 0 | 17170.431 | 26214.399 | 26607.615 |
| t-428 | 37 | 2 | 130.224 | 80.51 | 159 | 0 | 3260 | 3260 | 0 | 17432.575 | 26214.399 | 29360.127 |
| t-485 | 12 | 1 | 69.507 | 13.448 | 880 | 0 | 545 | 545 | 0 | 17694.719 | 26214.399 | 26476.543 |
| t-013 | 10 | 1 | 79.654 | 12.086 | 134 | 0 | 473 | 473 | 0 | 18219.007 | 26083.327 | 26476.543 |
| t-139 | 16 | 1 | 38.195 | 10.791 | 1367 | 0 | 472 | 472 | 0 | 18087.935 | 26083.327 | 26476.543 |
| t-292 | 16 | 1 | 96.995 | 27.975 | 916 | 0 | 1108 | 1108 | 0 | 17825.791 | 26083.327 | 26607.615 |
| t-476 | 15 | 1 | 83.253 | 23.227 | 432 | 0 | 971 | 971 | 0 | 16711.679 | 26083.327 | 26607.615 |
| t-004 | 38 | 3 | 189.165 | 123.873 | 2178 | 0 | 4903 | 4903 | 0 | 17694.719 | 25952.255 | 26738.687 |
| t-021 | 19 | 1 | 35.278 | 11.734 | 3252 | 0 | 513 | 513 | 0 | 17301.503 | 25952.255 | 26476.543 |
| t-029 | 28 | 3 | 310.905 | 144.17 | 1732 | 0 | 5520 | 5520 | 0 | 17694.719 | 25952.255 | 26607.615 |
