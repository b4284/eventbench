# T28-nats-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.0.217:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.222s, closing 1.147s, drain 11.407s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 704039 | 704039 | 11837.568 | 11804.586 | 15995.1 | 0 | 0 | 0 | 330 | 3660 | 54219 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 540967 | 9174.034 | 9461.759 | 11755.519 | 13901.823 | 15622.143 | 18956.287 |
| e2e_from_send | 540967 | 349.752 | 256.383 | 594.943 | 2244.607 | 3444.735 | 7135.231 |
| producer_lag | 540967 | 8824.28 | 9125.887 | 11296.767 | 13344.767 | 14811.135 | 15712.255 |
| publish_call | 704039 | 176.975 | 140.415 | 349.439 | 608.767 | 770.047 | 900.607 |
| queue_wait | 540967 | 289.771 | 206.847 | 523.263 | 1955.839 | 3262.463 | 7041.023 |
| broker_dwell | 540967 | 128.511 | 32.111 | 277.503 | 1801.215 | 3125.247 | 6991.871 |
| receive_call | 624828 | 34.57 | 14.519 | 88.447 | 284.415 | 455.935 | 748.031 |
| ctx_load | 540967 | 13.889 | 10.839 | 27.967 | 53.599 | 89.023 | 430.847 |
| process | 540967 | 10.753 | 0.001 | 0.002 | 0.009 | 2615.295 | 3057.663 |
| ctx_commit | 540967 | 35.295 | 28.047 | 69.503 | 132.863 | 414.207 | 697.855 |
| ack | 454085 | 0.003 | 0.001 | 0.003 | 0.008 | 0.027 | 395.775 |
| service | 540967 | 59.982 | 43.135 | 88.575 | 188.287 | 2672.639 | 3483.647 |
| e2e_large | 238 | 11593.663 | 11804.671 | 14245.887 | 16588.799 | 17465.343 | 17465.343 |
| ready_wait | 540967 | 49.628 | 17.567 | 124.543 | 455.167 | 742.399 | 2420.735 |
| blocked | 540967 | 41.887 | 16.367 | 100.223 | 398.079 | 664.575 | 964.607 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11804.586 | 15995.1 |
| applied_total | 381279.62 | 680080 |
| bench_cpu_cores | 2.574 | 5.34 |
| bench_cpu_pct | 25.739 | 53.4 |
| bench_gc_gen0 | 0.52 | 2 |
| bench_gc_gen1 | 0.36 | 1 |
| bench_gc_gen2 | 0.34 | 1 |
| bench_gc_heap_mb | 1710.468 | 2694.1 |
| bench_mem_mb | 2652.616 | 2968.6 |
| bench_threadpool_queue | 24.64 | 600 |
| bench_threadpool_threads | 20.06 | 26 |
| broker_backlog | 38481.68 | 66320 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 2226.84 | 5602 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 766435.12 | 1381586 |
| octopus_missing_reports_total | 23611.2 | 41079 |
| octopus_mqtt_received_total | 130077.06 | 216318 |
| octopus_mqtt_sent_total | 297396.58 | 519077 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 12.24 | 28 |
| pg_active_loads | 8.98 | 36 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.982 | 100 |
| pg_conn_active | 40.98 | 94 |
| pg_conn_idle | 218.42 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.24 | 54 |
| pg_conn_total | 259.4 | 260 |
| pg_db_size_mb | 3684.828 | 4300.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27526.794 | 34096.9 |
| pg_tup_del_per_s | 35977.192 | 41087.7 |
| pg_tup_fetch_per_s | 318796.086 | 359181.1 |
| pg_tup_ins_per_s | 48314.864 | 57416.2 |
| pg_tup_upd_per_s | 61743.39 | 70024.1 |
| pg_wal_mb_per_s | 131.46 | 285.52 |
| produced_per_s | 11837.568 | 18640.2 |
| produced_total | 383506.46 | 683711 |
| publish_mb_per_s | 40.177 | 56.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 201,286 records are Completed |
| record_counts | pass | 201,286 records found, 92,239 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 381 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 704,039 events applied by workers, 704,039 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 54,219 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 398 ms, max 964 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2423 seq 14: ready 230 ms before start, 230 ms of it with an idle slot in group 71
- [cross_source_blocking] source 11912 seq 26: ready 195 ms before start, 195 ms of it with an idle slot in group 383
- [cross_source_blocking] source 8551 seq 20: ready 196 ms before start, 196 ms of it with an idle slot in group 293
- [cross_source_blocking] source 2276 seq 66: ready 196 ms before start, 196 ms of it with an idle slot in group 65
- [cross_source_blocking] source 11282 seq 21: ready 196 ms before start, 196 ms of it with an idle slot in group 367
- [cross_source_blocking] source 9466 seq 22: ready 196 ms before start, 196 ms of it with an idle slot in group 327
- [cross_source_blocking] source 12565 seq 11: ready 196 ms before start, 196 ms of it with an idle slot in group 397
- [cross_source_blocking] source 8059 seq 49: ready 172 ms before start, 163 ms of it with an idle slot in group 271
- [cross_source_blocking] source 3838 seq 28: ready 196 ms before start, 196 ms of it with an idle slot in group 131
- [cross_source_blocking] source 7361 seq 26: ready 196 ms before start, 196 ms of it with an idle slot in group 253
- [cross_source_blocking] source 3086 seq 18: ready 279 ms before start, 225 ms of it with an idle slot in group 103
- [cross_source_blocking] source 8430 seq 17: ready 221 ms before start, 221 ms of it with an idle slot in group 285
- [cross_source_blocking] source 5184 seq 13: ready 280 ms before start, 243 ms of it with an idle slot in group 179
- [cross_source_blocking] source 11362 seq 24: ready 280 ms before start, 280 ms of it with an idle slot in group 371
- [cross_source_blocking] source 10372 seq 31: ready 129 ms before start, 129 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7622 seq 22: ready 168 ms before start, 168 ms of it with an idle slot in group 261
- [cross_source_blocking] source 4723 seq 17: ready 169 ms before start, 169 ms of it with an idle slot in group 163
- [cross_source_blocking] source 1795 seq 21: ready 168 ms before start, 168 ms of it with an idle slot in group 51
- [cross_source_blocking] source 11972 seq 23: ready 169 ms before start, 169 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11370 seq 26: ready 168 ms before start, 168 ms of it with an idle slot in group 371
- [cross_source_blocking] source 219 seq 22: ready 168 ms before start, 168 ms of it with an idle slot in group 5

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 131 | 131 | 0 | 10420.223 | 16646.143 | 16777.215 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 330 | 330 | 0 | 11337.727 | 16056.319 | 17563.647 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2502 | 2502 | 0 | 9764.863 | 15400.959 | 16056.319 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 81 | 81 | 0 | 11010.047 | 14942.207 | 14942.207 |
| t-489 | 6 | 1 | 303.141 | 30.942 | 1030 | 0 | 1509 | 1509 | 0 | 10158.079 | 14942.207 | 16056.319 |
| t-359 | 52 | 1 | 54.904 | 47.153 | 132 | 0 | 2487 | 2487 | 0 | 9502.719 | 14811.135 | 16515.071 |
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 7260 | 7260 | 0 | 9830.399 | 14811.135 | 15990.783 |
| t-053 | 11 | 1 | 25.069 | 4.714 | 3589 | 0 | 234 | 234 | 0 | 9830.399 | 14745.599 | 15204.351 |
| t-247 | 8 | 1 | 53.926 | 7.182 | 506 | 0 | 338 | 338 | 0 | 9830.399 | 14745.599 | 15269.887 |
| t-021 | 19 | 1 | 35.278 | 11.734 | 3252 | 0 | 603 | 603 | 0 | 9633.791 | 14680.063 | 15859.711 |
| t-211 | 12 | 1 | 82.324 | 16.827 | 1475 | 0 | 819 | 819 | 0 | 9895.935 | 14680.063 | 15728.639 |
| t-327 | 6 | 1 | 144.262 | 13.027 | 370 | 0 | 606 | 606 | 0 | 9961.471 | 14680.063 | 15794.175 |
| t-453 | 6 | 1 | 97.527 | 9.606 | 874 | 0 | 464 | 464 | 0 | 9830.399 | 14680.063 | 14811.135 |
| t-363 | 19 | 2 | 202.118 | 65.584 | 639 | 0 | 3306 | 3306 | 0 | 9830.399 | 14614.527 | 16515.071 |
| t-481 | 12 | 1 | 263.464 | 49.674 | 1993 | 0 | 2497 | 2497 | 0 | 9961.471 | 14614.527 | 16318.463 |
| t-077 | 10 | 2 | 311.624 | 52.01 | 1566 | 0 | 2606 | 2606 | 0 | 9699.327 | 14548.991 | 15859.711 |
| t-355 | 10 | 1 | 65.589 | 11.499 | 349 | 0 | 580 | 580 | 0 | 9830.399 | 14548.991 | 15204.351 |
| t-415 | 14 | 1 | 150.373 | 34.759 | 2972 | 0 | 1754 | 1754 | 0 | 9568.255 | 14417.919 | 15597.567 |
| t-037 | 21 | 2 | 160.4 | 56.292 | 272 | 0 | 2787 | 2787 | 0 | 9633.791 | 14352.383 | 15728.639 |
| t-123 | 16 | 1 | 145.733 | 36.226 | 4028 | 0 | 1778 | 1778 | 0 | 9699.327 | 14352.383 | 15204.351 |
| t-297 | 14 | 1 | 83.52 | 18.744 | 1702 | 0 | 931 | 931 | 0 | 9830.399 | 14352.383 | 15728.639 |
| t-427 | 16 | 2 | 241.21 | 64.026 | 1711 | 0 | 3055 | 3055 | 0 | 9961.471 | 14352.383 | 16121.855 |
| t-003 | 30 | 1 | 79.146 | 39.333 | 1633 | 0 | 2000 | 2000 | 0 | 9502.719 | 14286.847 | 16056.319 |
| t-029 | 28 | 3 | 310.905 | 144.17 | 1732 | 0 | 7082 | 7082 | 0 | 9764.863 | 14286.847 | 16056.319 |
| t-173 | 12 | 1 | 91.453 | 19.263 | 3493 | 0 | 992 | 992 | 0 | 9830.399 | 14286.847 | 16056.319 |
