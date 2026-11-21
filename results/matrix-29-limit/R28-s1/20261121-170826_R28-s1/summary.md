# R28-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 90.148s, closing 1.187s, drain 3.943s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 459528 | 459528 | 7486.77 | 7490.498 | 9689.6 | 0 | 0 | 0 | 94 | 2319 | 24841 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 359304 | 5699.86 | 5451.775 | 10878.975 | 13557.759 | 15835.135 | 19234.815 |
| e2e_from_send | 359304 | 317.49 | 242.687 | 499.711 | 2187.263 | 3323.903 | 5382.143 |
| producer_lag | 359304 | 5382.372 | 5128.191 | 10502.143 | 13082.623 | 15499.263 | 19021.823 |
| publish_call | 459528 | 162.579 | 137.343 | 305.663 | 613.375 | 927.743 | 1403.903 |
| queue_wait | 359304 | 255.451 | 191.615 | 409.855 | 1871.871 | 3110.911 | 5349.375 |
| broker_dwell | 359304 | 131.016 | 59.711 | 212.607 | 1757.183 | 3018.751 | 5201.919 |
| receive_call | 405907 | 33.209 | 14.863 | 87.743 | 243.711 | 436.735 | 505.343 |
| ctx_load | 359304 | 14.086 | 9.487 | 26.031 | 126.399 | 154.623 | 434.943 |
| process | 359304 | 10.608 | 0.001 | 0.002 | 0.008 | 2633.727 | 3004.415 |
| ctx_commit | 359304 | 37.318 | 23.407 | 68.735 | 303.359 | 546.303 | 851.967 |
| ack | 286636 | 0.002 | 0.001 | 0.003 | 0.01 | 0.034 | 31.743 |
| service | 359304 | 62.04 | 36.959 | 93.247 | 349.951 | 2691.071 | 3497.983 |
| e2e_large | 68 | 7866.383 | 7016.447 | 13336.575 | 15679.487 | 16809.983 | 16809.983 |
| ready_wait | 359304 | 41.343 | 18.319 | 105.471 | 291.327 | 665.599 | 1163.263 |
| blocked | 359304 | 33.427 | 17.039 | 82.815 | 210.431 | 445.951 | 837.119 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7490.498 | 9689.6 |
| applied_total | 259417.72 | 446730 |
| bench_cpu_cores | 1.946 | 4.143 |
| bench_cpu_pct | 19.456 | 41.43 |
| bench_gc_gen0 | 0.3 | 2 |
| bench_gc_gen1 | 0.2 | 1 |
| bench_gc_gen2 | 0.18 | 1 |
| bench_gc_heap_mb | 1281.676 | 2314 |
| bench_mem_mb | 2191.61 | 2472.8 |
| bench_threadpool_queue | 11.22 | 320 |
| bench_threadpool_threads | 17.12 | 19 |
| broker_backlog | 12537.94 | 16191 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1079.36 | 2406 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 522765.2 | 896281 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 134856.48 | 210233 |
| octopus_mqtt_sent_total | 193349.58 | 325278 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.88 | 36 |
| pg_active_loads | 8.9 | 40 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 48.68 | 88 |
| pg_conn_idle | 214.54 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.54 | 29 |
| pg_conn_total | 263.22 | 264 |
| pg_db_size_mb | 3553.194 | 4033.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26101.552 | 33154.7 |
| pg_tup_del_per_s | 32526.318 | 39812.9 |
| pg_tup_fetch_per_s | 288802.812 | 353523.6 |
| pg_tup_ins_per_s | 42534.232 | 53084.3 |
| pg_tup_upd_per_s | 56335.204 | 70348 |
| pg_wal_mb_per_s | 114.112 | 161.37 |
| produced_per_s | 7486.77 | 10740.3 |
| produced_total | 260497.08 | 447092 |
| publish_mb_per_s | 15.176 | 29.45 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5359/5359 source cursors at the expected last sequence |
| records_complete | pass | all 182,985 records are Completed |
| record_counts | pass | 182,985 records found, 60,557 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 344 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 459,528 events applied by workers, 459,528 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 24,841 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 210 ms, max 837 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9838 seq 6: ready 106 ms before start, 106 ms of it with an idle slot in group 340
- [cross_source_blocking] source 13992 seq 5: ready 122 ms before start, 122 ms of it with an idle slot in group 445
- [cross_source_blocking] source 9405 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 325
- [cross_source_blocking] source 15001 seq 20: ready 160 ms before start, 160 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15005 seq 27: ready 119 ms before start, 103 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15009 seq 25: ready 123 ms before start, 106 ms of it with an idle slot in group 469
- [cross_source_blocking] source 197 seq 29: ready 154 ms before start, 116 ms of it with an idle slot in group 4
- [cross_source_blocking] source 14973 seq 28: ready 113 ms before start, 100 ms of it with an idle slot in group 469
- [cross_source_blocking] source 181 seq 37: ready 156 ms before start, 114 ms of it with an idle slot in group 4
- [cross_source_blocking] source 193 seq 20: ready 142 ms before start, 105 ms of it with an idle slot in group 4
- [cross_source_blocking] source 15890 seq 53: ready 193 ms before start, 193 ms of it with an idle slot in group 496
- [cross_source_blocking] source 26 seq 11: ready 205 ms before start, 205 ms of it with an idle slot in group 1
- [cross_source_blocking] source 9413 seq 15: ready 141 ms before start, 116 ms of it with an idle slot in group 325
- [cross_source_blocking] source 9453 seq 14: ready 139 ms before start, 118 ms of it with an idle slot in group 325
- [cross_source_blocking] source 9367 seq 18: ready 125 ms before start, 101 ms of it with an idle slot in group 325
- [cross_source_blocking] source 9880 seq 10: ready 125 ms before start, 104 ms of it with an idle slot in group 340
- [cross_source_blocking] source 9442 seq 13: ready 155 ms before start, 103 ms of it with an idle slot in group 325
- [cross_source_blocking] source 13699 seq 15: ready 117 ms before start, 117 ms of it with an idle slot in group 436
- [cross_source_blocking] source 939 seq 12: ready 117 ms before start, 117 ms of it with an idle slot in group 28
- [cross_source_blocking] source 14995 seq 43: ready 117 ms before start, 117 ms of it with an idle slot in group 469
- [cross_source_blocking] source 6908 seq 32: ready 117 ms before start, 117 ms of it with an idle slot in group 235

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-106 | 24 | 4 | 386.38 | 157.097 | 824 | 0 | 8224 | 8224 | 0 | 6455.295 | 16383.999 | 19267.583 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 156 | 156 | 0 | 6520.831 | 15728.639 | 16908.287 |
| t-025 | 6 | 1 | 105.516 | 11.154 | 954 | 0 | 555 | 555 | 0 | 8650.751 | 15204.351 | 16383.999 |
| t-232 | 6 | 1 | 123.071 | 12.511 | 805 | 0 | 614 | 614 | 0 | 8323.071 | 15138.815 | 16449.535 |
| t-415 | 14 | 1 | 150.373 | 34.759 | 2972 | 0 | 1777 | 1777 | 0 | 6619.135 | 15138.815 | 15990.783 |
| t-454 | 7 | 1 | 292.242 | 33.739 | 266 | 0 | 1740 | 1740 | 0 | 7569.407 | 15138.815 | 16515.071 |
| t-427 | 16 | 2 | 241.21 | 64.026 | 1711 | 0 | 3338 | 3338 | 0 | 5537.791 | 15073.279 | 16711.679 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1486 | 1486 | 0 | 6127.615 | 15007.743 | 16449.535 |
| t-244 | 11 | 1 | 69.439 | 12.96 | 1151 | 0 | 654 | 654 | 0 | 6488.063 | 15007.743 | 15728.639 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 818 | 818 | 0 | 7307.263 | 14876.671 | 16383.999 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2561 | 2561 | 0 | 5111.807 | 14614.527 | 17825.791 |
| t-181 | 35 | 4 | 322.682 | 186.423 | 1367 | 0 | 9734 | 9734 | 0 | 6389.759 | 14286.847 | 17170.431 |
| t-238 | 13 | 1 | 123.749 | 26.683 | 2335 | 0 | 1373 | 1373 | 0 | 6062.079 | 14286.847 | 16383.999 |
| t-067 | 11 | 1 | 188.777 | 34.722 | 3490 | 0 | 1843 | 1843 | 0 | 5931.007 | 14221.311 | 17694.719 |
| t-148 | 6 | 1 | 136.453 | 12.472 | 715 | 0 | 638 | 638 | 0 | 7340.031 | 14221.311 | 16121.855 |
| t-370 | 12 | 1 | 43.412 | 8.825 | 568 | 0 | 464 | 464 | 0 | 6651.903 | 14221.311 | 15138.815 |
| t-133 | 6 | 1 | 60.083 | 5.815 | 273 | 0 | 299 | 299 | 0 | 7798.783 | 14155.775 | 15073.279 |
| t-280 | 12 | 1 | 88.969 | 17.414 | 2814 | 0 | 884 | 884 | 0 | 5832.703 | 14155.775 | 15990.783 |
| t-487 | 9 | 1 | 211.444 | 30.916 | 755 | 0 | 1527 | 1527 | 0 | 5341.183 | 14090.239 | 14811.135 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 22562 | 22562 | 0 | 5996.543 | 14090.239 | 16711.679 |
| t-037 | 21 | 2 | 160.4 | 56.292 | 272 | 0 | 2946 | 2946 | 0 | 5636.095 | 14024.703 | 16056.319 |
| t-346 | 12 | 1 | 60.843 | 11.79 | 1247 | 0 | 652 | 652 | 0 | 6979.583 | 14024.703 | 15663.103 |
| t-073 | 7 | 1 | 42.182 | 5.31 | 1128 | 0 | 299 | 299 | 0 | 7667.711 | 13959.167 | 15794.175 |
| t-352 | 16 | 2 | 226.575 | 59.558 | 2088 | 0 | 3074 | 3074 | 0 | 6193.151 | 13959.167 | 15859.711 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 13675 | 13675 | 0 | 5832.703 | 13828.095 | 16515.071 |
