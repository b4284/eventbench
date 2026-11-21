# V28-mosq-2-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.676s, closing 1.039s, drain 39.078s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 698723 | 698723 | 11674.257 | 11608.692 | 15761.2 | 0 | 0 | 0 | 328 | 3639 | 78465 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 535657 | 9033.837 | 8896.511 | 12705.791 | 14802.943 | 15998.975 | 21200.895 |
| e2e_from_send | 535657 | 376.185 | 284.671 | 626.175 | 2330.623 | 3553.279 | 7557.119 |
| producer_lag | 535657 | 8657.644 | 8503.295 | 12222.463 | 14311.423 | 14860.287 | 15515.647 |
| publish_call | 698723 | 177.76 | 138.879 | 360.191 | 663.551 | 895.487 | 1157.119 |
| queue_wait | 535657 | 319.203 | 234.879 | 565.759 | 2051.071 | 3401.727 | 7340.031 |
| broker_dwell | 535657 | 151.566 | 51.935 | 343.295 | 1896.447 | 3239.935 | 7290.879 |
| receive_call | 625726 | 35.033 | 15.391 | 88.511 | 282.111 | 471.807 | 539.135 |
| ctx_load | 535657 | 13.722 | 10.135 | 28.463 | 57.759 | 85.375 | 386.303 |
| process | 535657 | 10.777 | 0.001 | 0.002 | 0.009 | 2611.199 | 3172.351 |
| ctx_commit | 535657 | 32.451 | 25.903 | 65.407 | 121.471 | 202.879 | 651.775 |
| ack | 441234 | 0.002 | 0.001 | 0.003 | 0.008 | 0.027 | 9.023 |
| service | 535657 | 56.983 | 39.935 | 86.463 | 164.607 | 2666.495 | 3583.999 |
| e2e_large | 236 | 11471.568 | 11444.223 | 15147.007 | 16924.671 | 18104.319 | 18104.319 |
| ready_wait | 535657 | 59.869 | 29.471 | 145.791 | 448.511 | 702.975 | 1383.423 |
| blocked | 535657 | 53.058 | 27.391 | 126.015 | 424.447 | 638.463 | 935.423 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11608.692 | 15761.2 |
| applied_total | 383337.939 | 671094 |
| bench_cpu_cores | 2.75 | 5.674 |
| bench_cpu_pct | 27.502 | 56.74 |
| bench_gc_gen0 | 0.633 | 2 |
| bench_gc_gen1 | 0.469 | 2 |
| bench_gc_gen2 | 0.449 | 1 |
| bench_gc_heap_mb | 1474.163 | 2436 |
| bench_mem_mb | 2460.102 | 2760.8 |
| bench_threadpool_queue | 4.184 | 117 |
| bench_threadpool_threads | 18.163 | 22 |
| broker_backlog | 55923.857 | 115673 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 2265.918 | 4702 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 773654.306 | 1364485 |
| octopus_missing_reports_total | 25905.898 | 43474 |
| octopus_mqtt_received_total | 144901.082 | 239408 |
| octopus_mqtt_sent_total | 241067.694 | 408436 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.898 | 31 |
| pg_active_loads | 8.102 | 29 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.986 | 100 |
| pg_conn_active | 53.735 | 93 |
| pg_conn_idle | 207.612 | 238 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.245 | 23 |
| pg_conn_total | 261.347 | 262 |
| pg_db_size_mb | 3736.637 | 4347.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26601.361 | 32477.5 |
| pg_tup_del_per_s | 34717.453 | 40603.6 |
| pg_tup_fetch_per_s | 326004.682 | 410349.8 |
| pg_tup_ins_per_s | 47428.853 | 55766 |
| pg_tup_upd_per_s | 59437.669 | 71178.7 |
| pg_wal_mb_per_s | 138.471 | 268.9 |
| produced_per_s | 11674.257 | 16485.3 |
| produced_total | 385603.857 | 674895 |
| publish_mb_per_s | 39.346 | 54.69 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 200,658 records are Completed |
| record_counts | pass | 200,658 records found, 91,587 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 379 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 698,723 events applied by workers, 698,723 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 78,465 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 424 ms, max 935 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5498 seq 15: ready 102 ms before start, 102 ms of it with an idle slot in group 189
- [cross_source_blocking] source 3059 seq 20: ready 125 ms before start, 125 ms of it with an idle slot in group 103
- [cross_source_blocking] source 3091 seq 33: ready 125 ms before start, 125 ms of it with an idle slot in group 103
- [cross_source_blocking] source 13930 seq 20: ready 101 ms before start, 101 ms of it with an idle slot in group 443
- [cross_source_blocking] source 14976 seq 40: ready 135 ms before start, 135 ms of it with an idle slot in group 469
- [cross_source_blocking] source 5527 seq 16: ready 117 ms before start, 114 ms of it with an idle slot in group 189
- [cross_source_blocking] source 4358 seq 47: ready 116 ms before start, 116 ms of it with an idle slot in group 147
- [cross_source_blocking] source 8716 seq 13: ready 137 ms before start, 100 ms of it with an idle slot in group 303
- [cross_source_blocking] source 13514 seq 9: ready 102 ms before start, 102 ms of it with an idle slot in group 429
- [cross_source_blocking] source 7763 seq 19: ready 130 ms before start, 130 ms of it with an idle slot in group 263
- [cross_source_blocking] source 13951 seq 23: ready 111 ms before start, 111 ms of it with an idle slot in group 443
- [cross_source_blocking] source 15160 seq 15: ready 134 ms before start, 134 ms of it with an idle slot in group 473
- [cross_source_blocking] source 10063 seq 20: ready 107 ms before start, 107 ms of it with an idle slot in group 343
- [cross_source_blocking] source 6633 seq 51: ready 143 ms before start, 143 ms of it with an idle slot in group 223
- [cross_source_blocking] source 9984 seq 10: ready 130 ms before start, 130 ms of it with an idle slot in group 341
- [cross_source_blocking] source 14546 seq 14: ready 121 ms before start, 114 ms of it with an idle slot in group 461
- [cross_source_blocking] source 3667 seq 23: ready 166 ms before start, 117 ms of it with an idle slot in group 125
- [cross_source_blocking] source 10398 seq 17: ready 106 ms before start, 106 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7002 seq 14: ready 129 ms before start, 120 ms of it with an idle slot in group 237
- [cross_source_blocking] source 3163 seq 37: ready 109 ms before start, 109 ms of it with an idle slot in group 105

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 325 | 325 | 0 | 11075.583 | 17039.359 | 18219.007 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 80 | 80 | 0 | 10027.007 | 16646.143 | 16646.143 |
| t-369 | 14 | 2 | 287.936 | 70.703 | 1313 | 0 | 3456 | 3456 | 0 | 8912.895 | 16252.927 | 19136.511 |
| t-359 | 52 | 1 | 54.904 | 47.153 | 132 | 0 | 2488 | 2488 | 0 | 9043.967 | 15859.711 | 16908.287 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 132 | 132 | 0 | 11206.655 | 15794.175 | 15794.175 |
| t-065 | 80 | 10 | 368.175 | 485.998 | 263 | 0 | 23378 | 23378 | 0 | 9240.575 | 15728.639 | 21233.663 |
| t-187 | 6 | 1 | 122.008 | 12.138 | 685 | 0 | 569 | 569 | 0 | 9306.111 | 15663.103 | 15990.783 |
| t-413 | 19 | 1 | 150.868 | 49.856 | 3585 | 0 | 2490 | 2490 | 0 | 9109.503 | 15597.567 | 16908.287 |
| t-473 | 32 | 1 | 91.6 | 49.759 | 4074 | 0 | 2506 | 2506 | 0 | 9371.647 | 15532.031 | 16908.287 |
| t-489 | 6 | 1 | 303.141 | 30.942 | 1030 | 0 | 1490 | 1490 | 0 | 10616.831 | 15400.959 | 16252.927 |
| t-479 | 34 | 1 | 80.711 | 45.517 | 3383 | 0 | 2355 | 2355 | 0 | 9175.039 | 15335.423 | 16252.927 |
| t-041 | 34 | 5 | 422.011 | 237.716 | 2732 | 0 | 11580 | 11580 | 0 | 9175.039 | 15269.887 | 17825.791 |
| t-363 | 19 | 2 | 202.118 | 65.584 | 639 | 0 | 3289 | 3289 | 0 | 9175.039 | 15269.887 | 17039.359 |
| t-045 | 47 | 3 | 179.274 | 139.699 | 1398 | 0 | 6895 | 6895 | 0 | 8847.359 | 15204.351 | 16515.071 |
| t-287 | 17 | 1 | 155.915 | 45.751 | 158 | 0 | 2274 | 2274 | 0 | 8912.895 | 15204.351 | 15794.175 |
| t-379 | 57 | 2 | 103.137 | 97.597 | 591 | 0 | 4795 | 4795 | 0 | 8978.431 | 15204.351 | 17432.575 |
| t-105 | 33 | 2 | 159.618 | 91.774 | 851 | 0 | 4527 | 4527 | 0 | 9175.039 | 15138.815 | 16121.855 |
| t-313 | 35 | 1 | 76.092 | 45.564 | 510 | 0 | 2300 | 2300 | 0 | 8978.431 | 15138.815 | 16383.999 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 6920 | 6920 | 0 | 9306.111 | 15073.279 | 16056.319 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 12788 | 12788 | 0 | 8847.359 | 15073.279 | 16908.287 |
| t-441 | 30 | 3 | 306.592 | 148.691 | 2203 | 0 | 7221 | 7221 | 0 | 9043.967 | 15073.279 | 16515.071 |
| t-087 | 6 | 1 | 75.81 | 8.181 | 1614 | 0 | 397 | 397 | 0 | 10158.079 | 15007.743 | 15073.279 |
| t-161 | 41 | 2 | 132.357 | 89.666 | 339 | 0 | 4470 | 4470 | 0 | 9043.967 | 15007.743 | 16908.287 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2480 | 2480 | 0 | 9306.111 | 15007.743 | 17563.647 |
| t-251 | 9 | 1 | 82.187 | 12.347 | 1193 | 0 | 615 | 615 | 0 | 8257.535 | 15007.743 | 17170.431 |
