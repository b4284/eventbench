# W24-332-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.674s, closing 0.562s, drain 3.896s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 283777 | 283777 | 4567.142 | 4569.116 | 6906.2 | 0 | 0 | 0 | 322 | 1683 | 46271 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 219864 | 7254.72 | 7016.447 | 11378.687 | 14352.383 | 15507.455 | 18235.391 |
| e2e_from_send | 219864 | 728.889 | 568.319 | 1417.215 | 2947.071 | 4313.087 | 7000.063 |
| producer_lag | 219864 | 6525.832 | 6275.071 | 10477.567 | 13475.839 | 14360.575 | 15187.967 |
| publish_call | 283777 | 423.194 | 283.135 | 983.551 | 1737.727 | 2215.935 | 2762.751 |
| queue_wait | 219864 | 666.833 | 516.607 | 1332.223 | 2699.263 | 4077.567 | 5722.111 |
| broker_dwell | 219864 | 243.708 | 103.231 | 570.367 | 2349.055 | 3799.039 | 5660.671 |
| receive_call | 248723 | 35.854 | 13.391 | 92.991 | 318.207 | 473.087 | 515.583 |
| ctx_load | 219864 | 14.149 | 8.999 | 24.495 | 128.639 | 156.927 | 422.143 |
| process | 219864 | 12.124 | 0.001 | 0.003 | 0.015 | 2660.351 | 3000.319 |
| ctx_commit | 219864 | 35.753 | 20.639 | 60.831 | 293.631 | 547.839 | 1082.367 |
| ack | 203655 | 0.003 | 0.001 | 0.005 | 0.015 | 0.065 | 51.615 |
| service | 219864 | 62.057 | 33.375 | 89.983 | 363.007 | 2738.175 | 3923.967 |
| e2e_large | 238 | 9628.767 | 9322.495 | 13844.479 | 17268.735 | 18153.471 | 18153.471 |
| ready_wait | 219864 | 76.947 | 39.103 | 190.847 | 483.327 | 1039.871 | 2762.751 |
| blocked | 219864 | 63.677 | 34.687 | 157.695 | 371.455 | 786.943 | 2762.751 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4569.116 | 6261.9 |
| applied_total | 156393.92 | 272594 |
| bench_cpu_cores | 1.826 | 2.351 |
| bench_cpu_pct | 22.825 | 29.39 |
| bench_gc_gen0 | 0.52 | 2 |
| bench_gc_gen1 | 0.42 | 1 |
| bench_gc_gen2 | 0.42 | 1 |
| bench_gc_heap_mb | 1023.018 | 1462.6 |
| bench_mem_mb | 1784.686 | 1894.2 |
| bench_threadpool_queue | 0.7 | 24 |
| bench_threadpool_threads | 14.46 | 15 |
| broker_backlog | 12852.88 | 23578 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1225.5 | 4641 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 319055.98 | 561835 |
| octopus_missing_reports_total | 12120.88 | 19693 |
| octopus_mqtt_received_total | 98475.88 | 157127 |
| octopus_mqtt_sent_total | 121519.9 | 207730 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.34 | 39 |
| pg_active_loads | 6.6 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.976 | 100 |
| pg_conn_active | 38.28 | 95 |
| pg_conn_idle | 224.28 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.12 | 38 |
| pg_conn_total | 262.56 | 264 |
| pg_db_size_mb | 3655.58 | 4177.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25248.938 | 31645 |
| pg_tup_del_per_s | 33413.646 | 39168.2 |
| pg_tup_fetch_per_s | 277486.458 | 326783.3 |
| pg_tup_ins_per_s | 44373.666 | 52732.1 |
| pg_tup_upd_per_s | 54104.336 | 61848.1 |
| pg_wal_mb_per_s | 120.46 | 228.53 |
| produced_per_s | 4567.142 | 7463.2 |
| produced_total | 157619.42 | 273968 |
| publish_mb_per_s | 25.958 | 43.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 174,910 records are Completed |
| record_counts | pass | 174,910 records found, 37,748 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 402 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 283,777 events applied by workers, 283,777 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 46,271 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 371 ms, max 2762 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1164 seq 7: ready 244 ms before start, 239 ms of it with an idle slot in group 39
- [cross_source_blocking] source 843 seq 4: ready 411 ms before start, 189 ms of it with an idle slot in group 23
- [cross_source_blocking] source 835 seq 7: ready 428 ms before start, 206 ms of it with an idle slot in group 23
- [cross_source_blocking] source 815 seq 3: ready 297 ms before start, 234 ms of it with an idle slot in group 23
- [cross_source_blocking] source 10700 seq 27: ready 111 ms before start, 110 ms of it with an idle slot in group 350
- [cross_source_blocking] source 783 seq 11: ready 361 ms before start, 262 ms of it with an idle slot in group 23
- [cross_source_blocking] source 4401 seq 11: ready 302 ms before start, 302 ms of it with an idle slot in group 151
- [cross_source_blocking] source 14210 seq 21: ready 568 ms before start, 568 ms of it with an idle slot in group 455
- [cross_source_blocking] source 10032 seq 47: ready 187 ms before start, 187 ms of it with an idle slot in group 342
- [cross_source_blocking] source 3083 seq 18: ready 509 ms before start, 509 ms of it with an idle slot in group 103
- [cross_source_blocking] source 999 seq 12: ready 610 ms before start, 610 ms of it with an idle slot in group 30
- [cross_source_blocking] source 7034 seq 14: ready 258 ms before start, 258 ms of it with an idle slot in group 238
- [cross_source_blocking] source 15302 seq 11: ready 611 ms before start, 547 ms of it with an idle slot in group 478
- [cross_source_blocking] source 9465 seq 14: ready 611 ms before start, 611 ms of it with an idle slot in group 327
- [cross_source_blocking] source 13857 seq 28: ready 102 ms before start, 102 ms of it with an idle slot in group 438
- [cross_source_blocking] source 1569 seq 8: ready 125 ms before start, 122 ms of it with an idle slot in group 47
- [cross_source_blocking] source 6617 seq 24: ready 101 ms before start, 101 ms of it with an idle slot in group 222
- [cross_source_blocking] source 13789 seq 33: ready 103 ms before start, 103 ms of it with an idle slot in group 438
- [cross_source_blocking] source 6633 seq 27: ready 102 ms before start, 102 ms of it with an idle slot in group 223
- [cross_source_blocking] source 1018 seq 23: ready 102 ms before start, 102 ms of it with an idle slot in group 31

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 361 | 361 | 0 | 8847.359 | 17039.359 | 18219.007 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 163 | 163 | 0 | 8585.215 | 16646.143 | 17301.503 |
| t-135 | 6 | 1 | 28.268 | 2.883 | 791 | 0 | 150 | 150 | 0 | 6586.367 | 15400.959 | 15532.031 |
| t-023 | 72 | 1 | 38.898 | 46.836 | 647 | 0 | 2686 | 2686 | 0 | 7176.191 | 15335.423 | 17170.431 |
| t-223 | 13 | 1 | 200.098 | 42.324 | 2372 | 0 | 2133 | 2133 | 0 | 7143.423 | 15204.351 | 16908.287 |
| t-406 | 36 | 1 | 76.647 | 43.103 | 183 | 0 | 2291 | 2291 | 0 | 7077.887 | 15138.815 | 15794.175 |
| t-230 | 30 | 2 | 194.092 | 96.091 | 1033 | 0 | 4936 | 4936 | 0 | 7077.887 | 15007.743 | 16252.927 |
| t-359 | 52 | 1 | 47.06 | 40.417 | 132 | 0 | 2087 | 2087 | 0 | 7307.263 | 15007.743 | 16252.927 |
| t-438 | 121 | 6 | 138.912 | 290.853 | 1839 | 0 | 15093 | 15093 | 0 | 7176.191 | 14876.671 | 16711.679 |
| t-439 | 22 | 1 | 126.886 | 47.314 | 3339 | 0 | 2466 | 2466 | 0 | 7503.871 | 14876.671 | 15335.423 |
| t-031 | 11 | 1 | 128.922 | 23.474 | 463 | 0 | 1227 | 1227 | 0 | 7110.655 | 14745.599 | 15925.247 |
| t-302 | 12 | 1 | 172.074 | 36.802 | 1189 | 0 | 1897 | 1897 | 0 | 6324.223 | 14745.599 | 15204.351 |
| t-422 | 9 | 1 | 61.788 | 9.183 | 2679 | 0 | 447 | 447 | 0 | 7503.871 | 14745.599 | 15138.815 |
| t-079 | 6 | 1 | 179.789 | 18.486 | 1522 | 0 | 914 | 914 | 0 | 8224.767 | 14680.063 | 15663.103 |
| t-175 | 27 | 1 | 107.829 | 49.991 | 184 | 0 | 2510 | 2510 | 0 | 7372.799 | 14680.063 | 15466.495 |
| t-311 | 15 | 1 | 42.484 | 11.055 | 761 | 0 | 560 | 560 | 0 | 7143.423 | 14680.063 | 15335.423 |
| t-462 | 10 | 1 | 47.716 | 7.742 | 2006 | 0 | 397 | 397 | 0 | 6750.207 | 14680.063 | 15400.959 |
| t-055 | 65 | 4 | 188.704 | 195.304 | 359 | 0 | 10041 | 10041 | 0 | 7241.727 | 14614.527 | 15532.031 |
| t-190 | 6 | 1 | 128.892 | 13.456 | 241 | 0 | 659 | 659 | 0 | 8323.071 | 14614.527 | 14876.671 |
| t-295 | 12 | 1 | 103.437 | 20.547 | 3396 | 0 | 1063 | 1063 | 0 | 7602.175 | 14614.527 | 15728.639 |
| t-382 | 59 | 1 | 43.802 | 43.536 | 2368 | 0 | 2429 | 2429 | 0 | 7340.031 | 14614.527 | 15597.567 |
| t-271 | 59 | 5 | 229.56 | 225.144 | 334 | 0 | 11707 | 11707 | 0 | 7274.495 | 14548.991 | 18350.079 |
| t-046 | 24 | 2 | 157.586 | 63.541 | 3235 | 0 | 3336 | 3336 | 0 | 7012.351 | 14483.455 | 15204.351 |
| t-118 | 15 | 1 | 44.833 | 11.342 | 138 | 0 | 590 | 590 | 0 | 6488.063 | 14483.455 | 16056.319 |
| t-246 | 32 | 4 | 349.163 | 184.479 | 494 | 0 | 9448 | 9448 | 0 | 7405.567 | 14483.455 | 16580.607 |
