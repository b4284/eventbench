# W28-zmq-2-s1 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.263s, closing 1.342s, drain 36.162s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 428614 | 428614 | 6859.284 | 6879.098 | 11171.3 | 0 | 0 | 0 | 72 | 2073 | 11720 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 306239 | 14267.186 | 13877.247 | 21954.559 | 24248.319 | 25772.031 | 28049.407 |
| e2e_from_send | 306239 | 470.487 | 396.799 | 810.495 | 2136.063 | 3309.567 | 8855.551 |
| producer_lag | 306239 | 13796.687 | 13377.535 | 21544.959 | 23838.719 | 25247.743 | 25804.799 |
| publish_call | 428614 | 290.815 | 237.311 | 590.847 | 1220.607 | 1963.007 | 3432.447 |
| queue_wait | 306239 | 390.955 | 326.911 | 702.975 | 1917.951 | 2942.975 | 5316.607 |
| broker_dwell | 306239 | 93.89 | 10.055 | 237.311 | 1430.527 | 2697.215 | 5140.479 |
| receive_call | 389942 | 39.46 | 16.847 | 102.655 | 306.687 | 471.551 | 536.575 |
| ctx_load | 306239 | 15.584 | 10.071 | 29.183 | 134.143 | 162.943 | 666.623 |
| process | 306239 | 10.091 | 0.001 | 0.002 | 0.01 | 2584.575 | 3014.655 |
| ctx_commit | 306239 | 53.829 | 35.007 | 98.687 | 343.551 | 606.207 | 2494.463 |
| ack | 254328 | 0.002 | 0.001 | 0.003 | 0.009 | 0.02 | 33.599 |
| service | 306239 | 79.532 | 49.823 | 132.735 | 419.071 | 2668.543 | 5246.975 |
| e2e_large | 48 | 15942.699 | 16105.471 | 22953.983 | 25100.287 | 25100.287 | 25100.287 |
| ready_wait | 306239 | 31.462 | 8.155 | 86.655 | 332.799 | 718.335 | 2127.871 |
| blocked | 306239 | 22.618 | 7.879 | 48.799 | 265.215 | 693.247 | 2127.871 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6879.098 | 11171.3 |
| applied_total | 238949.02 | 409793 |
| bench_cpu_cores | 2.999 | 4.259 |
| bench_cpu_pct | 29.987 | 42.59 |
| bench_gc_gen0 | 0.367 | 2 |
| bench_gc_gen1 | 0.245 | 1 |
| bench_gc_gen2 | 0.245 | 1 |
| bench_gc_heap_mb | 1238.965 | 2151.1 |
| bench_mem_mb | 2233.933 | 2487.2 |
| bench_threadpool_queue | 1.429 | 20 |
| bench_threadpool_threads | 19.347 | 22 |
| broker_backlog | 39743.592 | 72673 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1175.571 | 3353 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 482924.857 | 844972 |
| octopus_missing_reports_total | 24419.469 | 40052 |
| octopus_mqtt_received_total | 117835.592 | 187807 |
| octopus_mqtt_sent_total | 128679.449 | 216563 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.49 | 34 |
| pg_active_loads | 9.347 | 30 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.957 | 100 |
| pg_conn_active | 56 | 88 |
| pg_conn_idle | 208.286 | 249 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 7.694 | 43 |
| pg_conn_total | 264.286 | 265 |
| pg_db_size_mb | 3834.586 | 4428.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26339.702 | 29883.1 |
| pg_tup_del_per_s | 34845.263 | 40462.4 |
| pg_tup_fetch_per_s | 312387.188 | 355749.1 |
| pg_tup_ins_per_s | 46774.959 | 56470.4 |
| pg_tup_upd_per_s | 55852.08 | 62379.6 |
| pg_wal_mb_per_s | 127.328 | 184.27 |
| produced_per_s | 6859.284 | 11707.9 |
| produced_total | 240124.592 | 410024 |
| publish_mb_per_s | 16.717 | 30.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 190,542 records are Completed |
| record_counts | pass | 190,542 records found, 59,078 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 387 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 428,614 events applied by workers, 428,614 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 11,720 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 265 ms, max 2126 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4491 seq 7: ready 140 ms before start, 140 ms of it with an idle slot in group 155
- [cross_source_blocking] source 3858 seq 22: ready 147 ms before start, 147 ms of it with an idle slot in group 131
- [cross_source_blocking] source 12484 seq 5: ready 147 ms before start, 146 ms of it with an idle slot in group 397
- [cross_source_blocking] source 9444 seq 13: ready 140 ms before start, 126 ms of it with an idle slot in group 325
- [cross_source_blocking] source 15590 seq 18: ready 123 ms before start, 111 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10513 seq 37: ready 139 ms before start, 116 ms of it with an idle slot in group 348
- [cross_source_blocking] source 9454 seq 14: ready 164 ms before start, 126 ms of it with an idle slot in group 325
- [cross_source_blocking] source 3008 seq 11: ready 162 ms before start, 107 ms of it with an idle slot in group 100
- [cross_source_blocking] source 10425 seq 24: ready 145 ms before start, 122 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10349 seq 33: ready 150 ms before start, 124 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3982 seq 40: ready 116 ms before start, 100 ms of it with an idle slot in group 132
- [cross_source_blocking] source 15482 seq 24: ready 116 ms before start, 100 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15036 seq 26: ready 109 ms before start, 107 ms of it with an idle slot in group 469
- [cross_source_blocking] source 4300 seq 53: ready 146 ms before start, 127 ms of it with an idle slot in group 147
- [cross_source_blocking] source 15032 seq 26: ready 177 ms before start, 168 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15494 seq 18: ready 166 ms before start, 149 ms of it with an idle slot in group 483
- [cross_source_blocking] source 14564 seq 17: ready 111 ms before start, 100 ms of it with an idle slot in group 461
- [cross_source_blocking] source 9165 seq 31: ready 124 ms before start, 118 ms of it with an idle slot in group 315
- [cross_source_blocking] source 988 seq 74: ready 115 ms before start, 114 ms of it with an idle slot in group 29
- [cross_source_blocking] source 4286 seq 39: ready 126 ms before start, 121 ms of it with an idle slot in group 147

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-484 | 6 | 1 | 162.625 | 16.461 | 232 | 0 | 661 | 661 | 0 | 15859.711 | 25821.183 | 25952.255 |
| t-132 | 22 | 3 | 327.283 | 111.293 | 2676 | 0 | 4403 | 4403 | 0 | 14811.135 | 25559.039 | 27262.975 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 70 | 70 | 0 | 15073.279 | 25165.823 | 25165.823 |
| t-077 | 10 | 2 | 311.624 | 52.01 | 1566 | 0 | 2014 | 2014 | 0 | 14811.135 | 25034.751 | 25952.255 |
| t-173 | 12 | 1 | 91.453 | 19.263 | 3493 | 0 | 858 | 858 | 0 | 14221.311 | 25034.751 | 26083.327 |
| t-299 | 21 | 1 | 91.91 | 31.431 | 646 | 0 | 1251 | 1251 | 0 | 14221.311 | 25034.751 | 26083.327 |
| t-324 | 15 | 1 | 47.791 | 11.104 | 131 | 0 | 469 | 469 | 0 | 14876.671 | 25034.751 | 25821.183 |
| t-069 | 31 | 1 | 30.759 | 15.322 | 2002 | 0 | 690 | 690 | 0 | 12910.591 | 24903.679 | 25821.183 |
| t-075 | 37 | 3 | 163.802 | 101.175 | 232 | 0 | 4179 | 4179 | 0 | 13303.807 | 24903.679 | 27131.903 |
| t-219 | 13 | 1 | 71.086 | 15.025 | 272 | 0 | 580 | 580 | 0 | 15400.959 | 24903.679 | 26083.327 |
| t-292 | 16 | 1 | 96.995 | 27.975 | 916 | 0 | 1154 | 1154 | 0 | 13565.951 | 24903.679 | 26083.327 |
| t-355 | 10 | 1 | 65.589 | 11.499 | 349 | 0 | 455 | 455 | 0 | 15138.815 | 24903.679 | 26476.543 |
| t-013 | 10 | 1 | 79.654 | 12.086 | 134 | 0 | 472 | 472 | 0 | 13893.631 | 24772.607 | 25821.183 |
| t-029 | 28 | 3 | 310.905 | 144.17 | 1732 | 0 | 5710 | 5710 | 0 | 14417.919 | 24772.607 | 26083.327 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 1999 | 1999 | 0 | 14417.919 | 24772.607 | 26083.327 |
| t-203 | 6 | 1 | 62.43 | 6.746 | 2258 | 0 | 265 | 265 | 0 | 17432.575 | 24772.607 | 25296.895 |
| t-211 | 12 | 1 | 82.324 | 16.827 | 1475 | 0 | 709 | 709 | 0 | 13959.167 | 24772.607 | 25165.823 |
| t-243 | 16 | 1 | 128.845 | 34.514 | 3143 | 0 | 1422 | 1422 | 0 | 14090.239 | 24772.607 | 25427.967 |
| t-251 | 9 | 1 | 82.187 | 12.347 | 1193 | 0 | 491 | 491 | 0 | 15597.567 | 24772.607 | 25034.751 |
| t-267 | 17 | 1 | 142.094 | 40.866 | 1525 | 0 | 1702 | 1702 | 0 | 14090.239 | 24772.607 | 26083.327 |
| t-412 | 6 | 1 | 61.704 | 5.712 | 468 | 0 | 239 | 239 | 0 | 14090.239 | 24772.607 | 25034.751 |
| t-420 | 13 | 1 | 107.671 | 23.798 | 270 | 0 | 1009 | 1009 | 0 | 12713.983 | 24772.607 | 25821.183 |
| t-427 | 16 | 2 | 241.21 | 64.026 | 1711 | 0 | 2445 | 2445 | 0 | 13959.167 | 24772.607 | 25952.255 |
| t-012 | 6 | 1 | 134.185 | 13.905 | 556 | 0 | 584 | 584 | 0 | 13369.343 | 24641.535 | 25427.967 |
| t-043 | 27 | 1 | 59.668 | 26.664 | 491 | 0 | 1151 | 1151 | 0 | 14352.383 | 24641.535 | 26083.327 |
