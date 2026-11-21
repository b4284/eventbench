# C24-332-conn16-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (16 producer / 8 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.582s, closing 1.029s, drain 85.397s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 331744 | 331744 | 5374.82 | 3296.871 | 7421.7 | 0 | 0 | 0 | 366 | 1957 | 201284 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 267612 | 20938.738 | 21151.743 | 37093.375 | 59277.311 | 74186.751 | 88997.887 |
| e2e_from_send | 267612 | 20458.8 | 20594.687 | 36765.695 | 58458.111 | 73269.247 | 87162.879 |
| producer_lag | 267612 | 479.932 | 31.023 | 1565.695 | 3172.351 | 3684.351 | 4130.815 |
| publish_call | 331744 | 288.354 | 215.679 | 522.495 | 1424.383 | 2299.903 | 2920.447 |
| queue_wait | 267612 | 20406.318 | 20545.535 | 36732.927 | 58458.111 | 73269.247 | 87162.879 |
| broker_dwell | 267612 | 20240.83 | 20365.311 | 36601.855 | 58359.807 | 73007.103 | 87162.879 |
| receive_call | 296058 | 38.52 | 14.639 | 106.687 | 278.271 | 492.543 | 513.535 |
| ctx_load | 267612 | 12.079 | 6.747 | 23.279 | 124.863 | 166.143 | 694.271 |
| process | 267612 | 11.994 | 0.001 | 0.003 | 0.016 | 2656.255 | 3024.895 |
| ctx_commit | 267612 | 28.363 | 14.231 | 56.831 | 284.927 | 528.895 | 2011.135 |
| ack | 253566 | 0.003 | 0.002 | 0.005 | 0.015 | 0.058 | 0.362 |
| service | 267612 | 52.464 | 24.911 | 82.623 | 359.935 | 2754.559 | 4427.775 |
| e2e_large | 282 | 5061.158 | 4648.959 | 7192.575 | 11640.831 | 14073.855 | 14073.855 |
| ready_wait | 267612 | 542.888 | 320.511 | 1285.119 | 2781.183 | 4456.447 | 21594.111 |
| blocked | 267612 | 503.725 | 268.287 | 1243.135 | 2613.247 | 4145.151 | 21495.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3296.871 | 5705.7 |
| applied_total | 111773.571 | 194681 |
| bench_cpu_cores | 3.177 | 4.173 |
| bench_cpu_pct | 39.715 | 52.17 |
| bench_gc_gen0 | 0.49 | 2 |
| bench_gc_gen1 | 0.367 | 1 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 1049.2 | 1537.2 |
| bench_mem_mb | 1867.443 | 1983.4 |
| bench_threadpool_queue | 0.163 | 6 |
| bench_threadpool_threads | 16.531 | 22 |
| broker_backlog | 296733.204 | 477906 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 79391.837 | 125203 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 303634.571 | 521708 |
| octopus_missing_reports_total | 9495.755 | 18833 |
| octopus_mqtt_received_total | 347173.735 | 525051 |
| octopus_mqtt_sent_total | 190479.735 | 326912 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.714 | 35 |
| pg_active_loads | 5.633 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.837 | 100 |
| pg_conn_active | 50.571 | 83 |
| pg_conn_idle | 213.327 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 15.796 | 53 |
| pg_conn_total | 263.898 | 268 |
| pg_db_size_mb | 4289.751 | 5511.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19033.524 | 22516.6 |
| pg_tup_del_per_s | 27251.816 | 46363.2 |
| pg_tup_fetch_per_s | 414031.657 | 621669.5 |
| pg_tup_ins_per_s | 47299.339 | 59097.8 |
| pg_tup_upd_per_s | 37952.633 | 48788.9 |
| pg_wal_mb_per_s | 136.194 | 230.07 |
| produced_per_s | 5374.82 | 6872.8 |
| produced_total | 191165.408 | 319884 |
| publish_mb_per_s | 29.839 | 46.92 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 179,884 records are Completed |
| record_counts | pass | 179,884 records found, 43,115 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 450 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 331,744 events applied by workers, 331,744 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 201,284 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2613 ms, max 21481 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9010 seq 7: ready 219 ms before start, 219 ms of it with an idle slot in group 311
- [cross_source_blocking] source 5770 seq 3: ready 127 ms before start, 127 ms of it with an idle slot in group 199
- [cross_source_blocking] source 2787 seq 23: ready 149 ms before start, 149 ms of it with an idle slot in group 86
- [cross_source_blocking] source 4540 seq 12: ready 193 ms before start, 193 ms of it with an idle slot in group 159
- [cross_source_blocking] source 996 seq 10: ready 187 ms before start, 187 ms of it with an idle slot in group 30
- [cross_source_blocking] source 4801 seq 9: ready 198 ms before start, 198 ms of it with an idle slot in group 167
- [cross_source_blocking] source 5559 seq 15: ready 119 ms before start, 119 ms of it with an idle slot in group 191
- [cross_source_blocking] source 3292 seq 8: ready 118 ms before start, 118 ms of it with an idle slot in group 110
- [cross_source_blocking] source 8238 seq 16: ready 226 ms before start, 226 ms of it with an idle slot in group 278
- [cross_source_blocking] source 1137 seq 15: ready 107 ms before start, 107 ms of it with an idle slot in group 38
- [cross_source_blocking] source 8229 seq 27: ready 177 ms before start, 177 ms of it with an idle slot in group 278
- [cross_source_blocking] source 13215 seq 11: ready 104 ms before start, 104 ms of it with an idle slot in group 422
- [cross_source_blocking] source 2793 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 87
- [cross_source_blocking] source 11533 seq 33: ready 109 ms before start, 109 ms of it with an idle slot in group 375
- [cross_source_blocking] source 15643 seq 18: ready 220 ms before start, 220 ms of it with an idle slot in group 487
- [cross_source_blocking] source 8447 seq 9: ready 139 ms before start, 139 ms of it with an idle slot in group 286
- [cross_source_blocking] source 15089 seq 7: ready 128 ms before start, 128 ms of it with an idle slot in group 471
- [cross_source_blocking] source 4538 seq 19: ready 114 ms before start, 114 ms of it with an idle slot in group 158
- [cross_source_blocking] source 9009 seq 7: ready 118 ms before start, 118 ms of it with an idle slot in group 311
- [cross_source_blocking] source 10719 seq 8: ready 120 ms before start, 120 ms of it with an idle slot in group 351

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-383 | 200 | 10 | 144.204 | 479.619 | 2521 | 0 | 28870 | 28870 | 0 | 42205.183 | 74973.183 | 89128.959 |
| t-055 | 65 | 4 | 188.704 | 195.304 | 359 | 0 | 11754 | 11754 | 0 | 36962.303 | 62128.127 | 70254.591 |
| t-023 | 72 | 1 | 38.898 | 46.836 | 647 | 0 | 3108 | 3108 | 0 | 28311.551 | 47185.919 | 52428.799 |
| t-446 | 49 | 1 | 55.848 | 44.043 | 196 | 0 | 2713 | 2713 | 0 | 27131.903 | 46661.631 | 50855.935 |
| t-382 | 59 | 1 | 43.802 | 43.536 | 2368 | 0 | 2779 | 2779 | 0 | 28049.407 | 45350.911 | 48496.639 |
| t-175 | 27 | 1 | 107.829 | 49.991 | 184 | 0 | 2973 | 2973 | 0 | 26738.687 | 42205.183 | 43515.903 |
| t-350 | 85 | 3 | 99.704 | 137.035 | 285 | 0 | 8326 | 8326 | 0 | 26869.759 | 42205.183 | 44564.479 |
| t-438 | 121 | 6 | 138.912 | 290.853 | 1839 | 0 | 17616 | 17616 | 0 | 29229.055 | 42205.183 | 49545.215 |
| t-406 | 36 | 1 | 76.647 | 43.103 | 183 | 0 | 2675 | 2675 | 0 | 26214.399 | 41680.895 | 45088.767 |
| t-439 | 22 | 1 | 126.886 | 47.314 | 3339 | 0 | 2856 | 2856 | 0 | 29360.127 | 41680.895 | 43515.903 |
| t-222 | 138 | 6 | 115.307 | 268.364 | 2846 | 0 | 16271 | 16271 | 0 | 27131.903 | 41156.607 | 45350.911 |
| t-263 | 93 | 4 | 111.165 | 174.85 | 374 | 0 | 10802 | 10802 | 0 | 28573.695 | 41156.607 | 47448.063 |
| t-230 | 30 | 2 | 194.092 | 96.091 | 1033 | 0 | 5760 | 5760 | 0 | 25690.111 | 40632.319 | 42729.471 |
| t-182 | 89 | 5 | 152.41 | 221.744 | 2406 | 0 | 13497 | 13497 | 0 | 23461.887 | 40108.031 | 44302.335 |
| t-206 | 118 | 4 | 90.451 | 174.277 | 1969 | 0 | 10730 | 10730 | 0 | 23461.887 | 39845.887 | 43778.047 |
| t-239 | 39 | 3 | 195.601 | 123.82 | 225 | 0 | 7438 | 7438 | 0 | 24903.679 | 39845.887 | 42991.615 |
| t-359 | 52 | 1 | 47.06 | 40.417 | 132 | 0 | 2472 | 2472 | 0 | 22937.599 | 39845.887 | 45613.055 |
| t-070 | 24 | 1 | 97.125 | 38.964 | 2303 | 0 | 2404 | 2404 | 0 | 20709.375 | 38797.311 | 42205.183 |
| t-246 | 32 | 4 | 349.163 | 184.479 | 494 | 0 | 11079 | 11079 | 0 | 25690.111 | 38535.167 | 40632.319 |
| t-271 | 59 | 5 | 229.56 | 225.144 | 334 | 0 | 13673 | 13673 | 0 | 25165.823 | 38273.023 | 41156.607 |
| t-127 | 19 | 1 | 146.611 | 44.138 | 2026 | 0 | 2722 | 2722 | 0 | 22937.599 | 37224.447 | 38797.311 |
| t-126 | 93 | 2 | 51.735 | 80.985 | 816 | 0 | 5015 | 5015 | 0 | 19660.799 | 36700.159 | 44040.191 |
| t-303 | 32 | 1 | 73.952 | 39.81 | 659 | 0 | 2502 | 2502 | 0 | 20971.519 | 36700.159 | 38010.879 |
| t-111 | 18 | 1 | 147.225 | 44.116 | 3401 | 0 | 2668 | 2668 | 0 | 23330.815 | 36175.871 | 37486.591 |
| t-103 | 50 | 2 | 90.566 | 75.162 | 1025 | 0 | 4537 | 4537 | 0 | 17825.791 | 35389.439 | 37748.735 |
