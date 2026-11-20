# B2-pgqueue-24k-pub1-conc — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:6432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.217s, closing 2.316s, drain 180.018s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1037686 | 903243 | 16615.451 | 2538.833 | 41421.8 | 0 | 0 | 0 | 318 | 5243 | 599835 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 638517 | 157381.883 | 171704.319 | 209321.983 | 214040.575 | 215351.295 | 217186.303 |
| e2e_from_send | 638517 | 146522.155 | 161087.487 | 195952.639 | 201981.951 | 203161.599 | 205651.967 |
| producer_lag | 753347 | 11753.101 | 11862.015 | 17317.887 | 19005.439 | 19546.111 | 20021.247 |
| publish_call | 1037686 | 108.777 | 107.903 | 124.479 | 145.023 | 232.959 | 280.319 |
| queue_wait | 638517 | 146500.453 | 161087.487 | 195952.639 | 201981.951 | 203161.599 | 205389.823 |
| broker_dwell | 638517 | 146399.922 | 160956.415 | 195821.567 | 201981.951 | 203030.527 | 205258.751 |
| receive_call | 438148 | 253.985 | 290.815 | 470.527 | 496.383 | 499.967 | 502.015 |
| ctx_load | 638517 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.339 |
| process | 638517 | 9.79 | 0.001 | 0.001 | 0.004 | 2576.383 | 3002.367 |
| ctx_commit | 638517 | 5.807 | 3.761 | 5.223 | 89.151 | 114.879 | 148.095 |
| service | 638517 | 21.739 | 8.471 | 11.143 | 158.591 | 2590.719 | 3160.063 |
| e2e_large | 217 | 13952.477 | 14065.663 | 19841.023 | 21807.103 | 23838.719 | 23838.719 |
| ready_wait | 638517 | 3342.421 | 1429.503 | 8327.167 | 26492.927 | 57671.679 | 193200.127 |
| blocked | 638517 | 3342.255 | 1429.503 | 8323.071 | 26492.927 | 57671.679 | 193200.127 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2538.833 | 4187.9 |
| applied_total | 115135 | 165547 |
| bench_cpu_cores | 3.03 | 3.323 |
| bench_cpu_pct | 12.627 | 13.85 |
| bench_gc_gen0 | 1.367 | 3 |
| bench_gc_gen1 | 1.143 | 3 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 745.722 | 929.9 |
| bench_mem_mb | 1101.833 | 1179.2 |
| bench_threadpool_queue | 0.265 | 4 |
| bench_threadpool_threads | 33.592 | 36 |
| broker_backlog | 464820.224 | 823011 |
| ctr_eb_pgbouncer_2_cpu_pct | 24.802 | 25.78 |
| ctr_eb_pgbouncer_2_mem_mb | 4.612 | 5.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 25.133 | 26.3 |
| ctr_eb_pgbouncer_3_mem_mb | 4.765 | 5.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 25.118 | 26.52 |
| ctr_eb_pgbouncer_4_mem_mb | 5.976 | 6.5 |
| ctr_eb_pgbouncer_cpu_pct | 25.173 | 26.23 |
| ctr_eb_pgbouncer_mem_mb | 5.022 | 5.6 |
| ctr_eb_postgres_cpu_pct | 701.893 | 803.71 |
| ctr_eb_postgres_mem_mb | 7921.253 | 9870.5 |
| dispatch_empty_reads_per_s | 9.371 | 21.4 |
| dispatch_fetch_calls_per_s | 14.91 | 22.1 |
| dispatch_group_reads_per_s | 853.388 | 1330.8 |
| dispatch_in_flight | 197.49 | 668 |
| dispatch_notifies_per_s | 16652.922 | 17687.5 |
| dispatch_preloaded_per_s | 2636.749 | 4000 |
| dispatch_rows_per_s | 2638.165 | 4000 |
| dispatch_waiting_receives | 770.388 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.903 | 51.21 |
| in_flight | 482087.857 | 832020 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.327 | 1 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.791 | 100 |
| pg_conn_active | 190.592 | 192 |
| pg_conn_idle | 3.51 | 5 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 184.837 | 191 |
| pg_conn_total | 194.102 | 195 |
| pg_db_size_mb | 2821.659 | 3760.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19679.829 | 61563.8 |
| pg_tup_del_per_s | 14486.667 | 23839.4 |
| pg_tup_fetch_per_s | 135466.016 | 186373.3 |
| pg_tup_ins_per_s | 48450.549 | 67316 |
| pg_tup_upd_per_s | 4293.855 | 7263.4 |
| pg_wal_mb_per_s | 85.617 | 230.64 |
| pgb_cl_active | 50.163 | 53 |
| pgb_cl_waiting | 204.204 | 215 |
| pgb_maxwait_ms | 53.242 | 82.34 |
| pgb_sv_active | 48 | 48 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16615.451 | 18209 |
| produced_total | 597222.857 | 997567 |
| publish_mb_per_s | 45.724 | 70.97 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 7467/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 19,625 records not Completed |
| record_counts | **FAIL** | 130,243 records found, 143,017 expected; 8533 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 318 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 903,243 events applied by workers, 1,037,686 generated |
| broker_drained | **FAIL** | 134,156 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 599,835 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27050 ms, max 193129 ms) |

Sample mismatches:

- source 3131: record 13447542603777 left in status Open (version 12)
- source 1180: record 5068061409281 left in status Open (version 3)
- source 3366: record 14456859918337 left in status Open (version 13)
- source 5505: record 23643794964481 left in status Open (version 14)
- source 5529: record 23746874179585 left in status Open (version 8)
- source 3275: record 14066017894401 left in status Open (version 11)
- source 3740: record 16063177687041 left in status Open (version 10)
- source 1188: record 5102421147651 left in status Open (version 4)
- source 2726: record 11708080848897 left in status Open (version 18)
- source 5944: record 25529285607425 left in status Open (version 20)
- source 5992: record 25735444037633 left in status Open (version 7)
- source 469: record 2014339661825 left in status Open (version 6)
- source 4812: record 20667382628353 left in status Open (version 9)
- source 4097: record 17596481011713 left in status Open (version 9)
- source 12612: record 54168127537153 left in status Open (version 6)
- source 13777: record 59171764436993 left in status Open (version 17)
- source 12917: record 55478092562433 left in status Open (version 31)
- source 13166: record 56547539419137 left in status Open (version 12)
- source 13280: record 57037165690881 left in status Open (version 16)
- source 14331: record 61551176318977 left in status Open (version 5)

## Cross-source blocking samples

- [cross_source_blocking] source 7260 seq 9: ready 517 ms before start, 517 ms of it with an idle slot in group 248
- [cross_source_blocking] source 2979 seq 8: ready 321 ms before start, 321 ms of it with an idle slot in group 98
- [cross_source_blocking] source 2570 seq 9: ready 270 ms before start, 270 ms of it with an idle slot in group 78
- [cross_source_blocking] source 6358 seq 4: ready 131 ms before start, 131 ms of it with an idle slot in group 217
- [cross_source_blocking] source 2845 seq 7: ready 160 ms before start, 160 ms of it with an idle slot in group 89
- [cross_source_blocking] source 3032 seq 13: ready 149 ms before start, 149 ms of it with an idle slot in group 101
- [cross_source_blocking] source 3448 seq 4: ready 148 ms before start, 148 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3292 seq 7: ready 748 ms before start, 748 ms of it with an idle slot in group 110
- [cross_source_blocking] source 3998 seq 6: ready 501 ms before start, 501 ms of it with an idle slot in group 133
- [cross_source_blocking] source 13886 seq 9: ready 293 ms before start, 293 ms of it with an idle slot in group 440
- [cross_source_blocking] source 13885 seq 8: ready 298 ms before start, 298 ms of it with an idle slot in group 440
- [cross_source_blocking] source 4272 seq 11: ready 242 ms before start, 242 ms of it with an idle slot in group 144
- [cross_source_blocking] source 1086 seq 7: ready 338 ms before start, 338 ms of it with an idle slot in group 34
- [cross_source_blocking] source 1084 seq 10: ready 382 ms before start, 382 ms of it with an idle slot in group 34
- [cross_source_blocking] source 2466 seq 4: ready 150 ms before start, 150 ms of it with an idle slot in group 73
- [cross_source_blocking] source 2978 seq 11: ready 394 ms before start, 394 ms of it with an idle slot in group 98
- [cross_source_blocking] source 2640 seq 6: ready 420 ms before start, 420 ms of it with an idle slot in group 81
- [cross_source_blocking] source 2570 seq 10: ready 348 ms before start, 348 ms of it with an idle slot in group 78
- [cross_source_blocking] source 2981 seq 9: ready 379 ms before start, 379 ms of it with an idle slot in group 98
- [cross_source_blocking] source 1113 seq 7: ready 324 ms before start, 324 ms of it with an idle slot in group 36

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 2264 | 1428 | 0 | 206569.471 | 217055.231 | 217055.231 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 10499 | 7154 | 0 | 201326.591 | 216006.655 | 217055.231 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 6362 | 4408 | 0 | 206569.471 | 216006.655 | 216006.655 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 12542 | 8568 | 0 | 204472.319 | 216006.655 | 217055.231 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 6385 | 4279 | 0 | 204472.319 | 216006.655 | 218103.807 |
| t-460 | 71 | 1 | 38.387 | 45.799 | 141 | 0 | 2134 | 1423 | 0 | 206569.471 | 216006.655 | 216006.655 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 10532 | 7215 | 0 | 203423.743 | 216006.655 | 217055.231 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 2149 | 1522 | 0 | 197132.287 | 214958.079 | 214958.079 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 2144 | 1456 | 0 | 205520.895 | 214958.079 | 216006.655 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 6463 | 4332 | 0 | 202375.167 | 214958.079 | 216006.655 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 5910 | 4275 | 0 | 201326.591 | 214958.079 | 216006.655 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 4284 | 2861 | 0 | 202375.167 | 214958.079 | 216006.655 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 20525 | 14107 | 0 | 201326.591 | 214958.079 | 216006.655 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 4117 | 2929 | 0 | 204472.319 | 214958.079 | 216006.655 |
| t-439 | 22 | 1 | 127.568 | 47.568 | 3339 | 0 | 1943 | 1349 | 0 | 202375.167 | 214958.079 | 216006.655 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 10184 | 7120 | 0 | 201326.591 | 214958.079 | 216006.655 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 6146 | 4288 | 0 | 202375.167 | 214958.079 | 216006.655 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2048 | 1485 | 0 | 204472.319 | 214958.079 | 214958.079 |
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 2207 | 1612 | 0 | 196083.711 | 213909.503 | 214958.079 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 10480 | 7652 | 0 | 198180.863 | 213909.503 | 213909.503 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 4166 | 2949 | 0 | 199229.439 | 213909.503 | 216006.655 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 1986 | 1440 | 0 | 202375.167 | 213909.503 | 214958.079 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 5884 | 4464 | 0 | 201326.591 | 213909.503 | 213909.503 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 21301 | 15738 | 0 | 198180.863 | 213909.503 | 214958.079 |
| t-382 | 59 | 1 | 44.037 | 43.77 | 2368 | 0 | 1983 | 1426 | 0 | 203423.743 | 213909.503 | 213909.503 |
