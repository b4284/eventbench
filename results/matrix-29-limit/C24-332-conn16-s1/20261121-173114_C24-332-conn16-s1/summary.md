# C24-332-conn16-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (16 producer / 8 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.715s, closing 2.092s, drain 84.436s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 475665 | 475665 | 7653.52 | 5263.865 | 12442.7 | 0 | 0 | 0 | 84 | 2301 | 295625 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 368112 | 21030.428 | 21823.487 | 34766.847 | 42860.543 | 49283.071 | 59047.935 |
| e2e_from_send | 368112 | 16508.207 | 16842.751 | 30490.623 | 38862.847 | 43548.671 | 49643.519 |
| producer_lag | 368112 | 4522.232 | 4202.495 | 7925.759 | 10960.895 | 12820.479 | 13230.079 |
| publish_call | 475665 | 264.963 | 228.351 | 427.775 | 978.943 | 1422.335 | 2846.719 |
| queue_wait | 368112 | 16449.853 | 16793.599 | 30441.471 | 38797.311 | 43515.903 | 49577.983 |
| broker_dwell | 368112 | 16330.742 | 16654.335 | 30343.167 | 38699.007 | 43417.599 | 49512.447 |
| receive_call | 435656 | 40.283 | 18.975 | 106.815 | 245.119 | 449.023 | 503.807 |
| ctx_load | 368112 | 13.12 | 7.819 | 25.599 | 125.311 | 160.511 | 692.735 |
| process | 368112 | 9.995 | 0.001 | 0.002 | 0.007 | 2609.151 | 3069.951 |
| ctx_commit | 368112 | 35.211 | 20.175 | 70.015 | 296.447 | 554.495 | 1398.783 |
| ack | 307661 | 0.002 | 0.001 | 0.004 | 0.012 | 0.035 | 0.198 |
| service | 368112 | 58.349 | 31.711 | 99.263 | 360.191 | 2662.399 | 3819.519 |
| e2e_large | 61 | 7454.888 | 7159.807 | 10698.751 | 14458.879 | 14573.567 | 14573.567 |
| ready_wait | 368112 | 670.074 | 438.015 | 1516.543 | 3532.799 | 4804.607 | 19120.127 |
| blocked | 368112 | 616.985 | 368.895 | 1457.151 | 3366.911 | 4685.823 | 18104.319 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5263.865 | 8061.7 |
| applied_total | 176590.878 | 308147 |
| bench_cpu_cores | 1.742 | 2.31 |
| bench_cpu_pct | 17.425 | 23.1 |
| bench_gc_gen0 | 0.286 | 2 |
| bench_gc_gen1 | 0.163 | 1 |
| bench_gc_gen2 | 0.163 | 1 |
| bench_gc_heap_mb | 1317.38 | 2413.2 |
| bench_mem_mb | 2295.531 | 2594.5 |
| bench_threadpool_queue | 0.837 | 10 |
| bench_threadpool_threads | 22.061 | 29 |
| broker_backlog | 295462.878 | 466775 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 100180.245 | 149447 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 456714.469 | 788421 |
| octopus_missing_reports_total | 10883.653 | 21633 |
| octopus_mqtt_received_total | 370942.143 | 579521 |
| octopus_mqtt_sent_total | 219043.857 | 381864 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 10.633 | 40 |
| pg_active_loads | 5.51 | 20 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.843 | 100 |
| pg_conn_active | 47.122 | 152 |
| pg_conn_idle | 216.694 | 261 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 11.469 | 45 |
| pg_conn_total | 263.816 | 266 |
| pg_db_size_mb | 4264.957 | 5590.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19169.371 | 22479.1 |
| pg_tup_del_per_s | 27911.512 | 41763.5 |
| pg_tup_fetch_per_s | 415056.659 | 648703.2 |
| pg_tup_ins_per_s | 47531.649 | 57672.7 |
| pg_tup_upd_per_s | 38235.176 | 47910.1 |
| pg_wal_mb_per_s | 134.866 | 238.12 |
| produced_per_s | 7653.52 | 10944.9 |
| produced_total | 276771.122 | 454491 |
| publish_mb_per_s | 19.063 | 27.61 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 179,884 records are Completed |
| record_counts | pass | 179,884 records found, 64,379 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 450 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 475,665 events applied by workers, 475,665 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 295,625 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 3367 ms, max 18097 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9523 seq 15: ready 150 ms before start, 150 ms of it with an idle slot in group 331
- [cross_source_blocking] source 9523 seq 16: ready 111 ms before start, 111 ms of it with an idle slot in group 331
- [cross_source_blocking] source 8670 seq 11: ready 299 ms before start, 296 ms of it with an idle slot in group 300
- [cross_source_blocking] source 6401 seq 9: ready 122 ms before start, 122 ms of it with an idle slot in group 219
- [cross_source_blocking] source 15241 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 475
- [cross_source_blocking] source 8497 seq 12: ready 106 ms before start, 106 ms of it with an idle slot in group 291
- [cross_source_blocking] source 8215 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 277
- [cross_source_blocking] source 2346 seq 6: ready 104 ms before start, 104 ms of it with an idle slot in group 68
- [cross_source_blocking] source 14418 seq 8: ready 126 ms before start, 126 ms of it with an idle slot in group 459
- [cross_source_blocking] source 954 seq 10: ready 279 ms before start, 219 ms of it with an idle slot in group 28
- [cross_source_blocking] source 7351 seq 5: ready 101 ms before start, 101 ms of it with an idle slot in group 252
- [cross_source_blocking] source 13197 seq 19: ready 253 ms before start, 218 ms of it with an idle slot in group 420
- [cross_source_blocking] source 1113 seq 15: ready 101 ms before start, 101 ms of it with an idle slot in group 36
- [cross_source_blocking] source 12438 seq 14: ready 232 ms before start, 134 ms of it with an idle slot in group 396
- [cross_source_blocking] source 13213 seq 21: ready 116 ms before start, 116 ms of it with an idle slot in group 421
- [cross_source_blocking] source 14894 seq 8: ready 121 ms before start, 121 ms of it with an idle slot in group 467
- [cross_source_blocking] source 5474 seq 12: ready 938 ms before start, 803 ms of it with an idle slot in group 188
- [cross_source_blocking] source 5674 seq 30: ready 116 ms before start, 116 ms of it with an idle slot in group 195
- [cross_source_blocking] source 13360 seq 28: ready 220 ms before start, 220 ms of it with an idle slot in group 427
- [cross_source_blocking] source 5458 seq 24: ready 101 ms before start, 101 ms of it with an idle slot in group 187

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-469 | 105 | 5 | 142.583 | 248.092 | 278 | 0 | 12864 | 12864 | 0 | 38010.879 | 52428.799 | 59244.543 |
| t-333 | 79 | 1 | 36.943 | 47.58 | 192 | 0 | 2580 | 2580 | 0 | 31064.063 | 48234.495 | 52690.943 |
| t-268 | 41 | 1 | 68.788 | 45.131 | 1430 | 0 | 2518 | 2518 | 0 | 27394.047 | 46399.487 | 50593.791 |
| t-221 | 70 | 1 | 37.148 | 42.548 | 2834 | 0 | 2350 | 2350 | 0 | 24248.319 | 46137.343 | 48758.783 |
| t-460 | 71 | 1 | 38.182 | 45.554 | 141 | 0 | 2576 | 2576 | 0 | 28049.407 | 45350.911 | 50331.647 |
| t-037 | 21 | 1 | 137.486 | 48.25 | 272 | 0 | 2521 | 2521 | 0 | 31850.495 | 45088.767 | 46137.343 |
| t-212 | 29 | 1 | 94.107 | 45.528 | 296 | 0 | 2455 | 2455 | 0 | 25034.751 | 45088.767 | 45875.199 |
| t-020 | 22 | 1 | 118.343 | 41.696 | 981 | 0 | 2212 | 2212 | 0 | 27394.047 | 44826.623 | 46137.343 |
| t-083 | 58 | 1 | 43.489 | 42.985 | 417 | 0 | 2348 | 2348 | 0 | 27918.335 | 44826.623 | 48758.783 |
| t-109 | 33 | 1 | 88.394 | 49.274 | 1467 | 0 | 2647 | 2647 | 0 | 32243.711 | 44564.479 | 46137.343 |
| t-235 | 54 | 3 | 147.636 | 132.899 | 527 | 0 | 6836 | 6836 | 0 | 28049.407 | 44302.335 | 49283.071 |
| t-340 | 184 | 3 | 45.575 | 140.377 | 141 | 0 | 7674 | 7674 | 0 | 27656.191 | 44040.191 | 49807.359 |
| t-245 | 43 | 2 | 132.507 | 91.014 | 2232 | 0 | 4805 | 4805 | 0 | 27262.975 | 43778.047 | 46923.775 |
| t-189 | 59 | 2 | 83.241 | 80.583 | 1601 | 0 | 4320 | 4320 | 0 | 26083.327 | 43515.903 | 47185.919 |
| t-259 | 70 | 1 | 33.545 | 38.405 | 160 | 0 | 2132 | 2132 | 0 | 22413.311 | 42991.615 | 50593.791 |
| t-429 | 200 | 3 | 33.697 | 114.852 | 1395 | 0 | 6436 | 6436 | 0 | 26607.615 | 42991.615 | 46399.487 |
| t-132 | 22 | 2 | 280.528 | 95.394 | 2676 | 0 | 4859 | 4859 | 0 | 29360.127 | 42729.471 | 44040.191 |
| t-147 | 79 | 8 | 270.698 | 363.5 | 1078 | 0 | 18654 | 18654 | 0 | 29884.415 | 42467.327 | 44040.191 |
| t-347 | 162 | 7 | 115.531 | 313.91 | 833 | 0 | 16643 | 16643 | 0 | 29491.199 | 42467.327 | 45088.767 |
| t-348 | 125 | 5 | 117.187 | 245.621 | 1340 | 0 | 12855 | 12855 | 0 | 29097.983 | 42467.327 | 44826.623 |
| t-052 | 62 | 2 | 81.907 | 85.941 | 1837 | 0 | 4690 | 4690 | 0 | 25821.183 | 42205.183 | 46137.343 |
| t-379 | 57 | 2 | 88.403 | 83.654 | 591 | 0 | 4627 | 4627 | 0 | 28442.623 | 41943.039 | 44302.335 |
| t-468 | 61 | 5 | 236.273 | 236.318 | 1645 | 0 | 12163 | 12163 | 0 | 27656.191 | 41943.039 | 44040.191 |
| t-125 | 20 | 1 | 120.256 | 41.985 | 1900 | 0 | 2194 | 2194 | 0 | 26738.687 | 41680.895 | 42991.615 |
| t-388 | 78 | 2 | 61.224 | 77.71 | 1205 | 0 | 4199 | 4199 | 0 | 25427.967 | 41680.895 | 46923.775 |
