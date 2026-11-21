# R24-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 90.163s, closing 1.261s, drain 2.815s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 446050 | 446050 | 7230.578 | 7228.732 | 8998.7 | 0 | 0 | 0 | 105 | 2235 | 44089 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 357823 | 1082.9 | 417.279 | 3010.559 | 8241.151 | 9568.255 | 14024.703 |
| e2e_from_send | 357823 | 298.142 | 202.367 | 530.943 | 2250.751 | 3524.607 | 6467.583 |
| producer_lag | 357823 | 784.758 | 28.911 | 2533.375 | 7942.143 | 8904.703 | 12894.207 |
| publish_call | 446050 | 121.492 | 85.439 | 267.775 | 555.007 | 820.223 | 1221.631 |
| queue_wait | 357823 | 242.155 | 156.799 | 454.655 | 1980.415 | 3332.095 | 6451.199 |
| broker_dwell | 357823 | 166.863 | 73.919 | 312.063 | 1911.807 | 3268.607 | 6385.663 |
| receive_call | 405356 | 30.377 | 14.207 | 75.135 | 247.039 | 444.415 | 662.527 |
| ctx_load | 357823 | 12.853 | 8.519 | 23.599 | 123.071 | 145.023 | 456.703 |
| process | 357823 | 10.525 | 0.001 | 0.002 | 0.009 | 2641.919 | 3008.511 |
| ctx_commit | 357823 | 32.585 | 19.551 | 60.383 | 288.767 | 461.055 | 848.895 |
| ack | 297098 | 0.002 | 0.001 | 0.004 | 0.01 | 0.034 | 0.376 |
| service | 357823 | 55.987 | 31.471 | 83.007 | 328.447 | 2697.215 | 3500.031 |
| e2e_large | 78 | 3603.298 | 3287.039 | 4898.815 | 8335.359 | 8937.471 | 8937.471 |
| ready_wait | 357823 | 57.179 | 25.663 | 144.511 | 430.079 | 907.775 | 1820.671 |
| blocked | 357823 | 46.074 | 23.167 | 113.407 | 313.599 | 702.463 | 1422.335 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7228.732 | 8998.7 |
| applied_total | 257023.46 | 432871 |
| bench_cpu_cores | 3.273 | 5.493 |
| bench_cpu_pct | 32.734 | 54.93 |
| bench_gc_gen0 | 0.28 | 2 |
| bench_gc_gen1 | 0.18 | 2 |
| bench_gc_gen2 | 0.16 | 1 |
| bench_gc_heap_mb | 1302.032 | 2177.4 |
| bench_mem_mb | 2305.106 | 2519.4 |
| bench_threadpool_queue | 0.44 | 8 |
| bench_threadpool_threads | 16.22 | 19 |
| broker_backlog | 10190.8 | 15396 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1354.68 | 2979 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 516769.3 | 880816 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 172527.32 | 285736 |
| octopus_mqtt_sent_total | 214415.2 | 365860 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.44 | 43 |
| pg_active_loads | 12.62 | 89 |
| pg_active_polls | 0.06 | 1 |
| pg_active_sends | 0.06 | 1 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 52.28 | 201 |
| pg_conn_idle | 210.62 | 244 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.74 | 15 |
| pg_conn_total | 262.94 | 264 |
| pg_db_size_mb | 3563.88 | 4020.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25414.61 | 28113 |
| pg_tup_del_per_s | 32157.262 | 39344.7 |
| pg_tup_fetch_per_s | 269472.022 | 303961.3 |
| pg_tup_ins_per_s | 42717.698 | 52702.1 |
| pg_tup_upd_per_s | 52810.288 | 58908.7 |
| pg_wal_mb_per_s | 112.858 | 152.99 |
| produced_per_s | 7230.578 | 8925.7 |
| produced_total | 258378.14 | 433495 |
| publish_mb_per_s | 15.055 | 25.84 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5359/5359 source cursors at the expected last sequence |
| records_complete | pass | all 172,069 records are Completed |
| record_counts | pass | 172,069 records found, 59,132 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 383 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 446,050 events applied by workers, 446,050 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 44,089 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 314 ms, max 1422 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5229 seq 34: ready 103 ms before start, 103 ms of it with an idle slot in group 181
- [cross_source_blocking] source 1289 seq 9: ready 111 ms before start, 111 ms of it with an idle slot in group 40
- [cross_source_blocking] source 4040 seq 12: ready 104 ms before start, 104 ms of it with an idle slot in group 136
- [cross_source_blocking] source 5410 seq 21: ready 154 ms before start, 110 ms of it with an idle slot in group 184
- [cross_source_blocking] source 13866 seq 30: ready 140 ms before start, 140 ms of it with an idle slot in group 439
- [cross_source_blocking] source 4149 seq 13: ready 115 ms before start, 115 ms of it with an idle slot in group 136
- [cross_source_blocking] source 4053 seq 15: ready 155 ms before start, 155 ms of it with an idle slot in group 136
- [cross_source_blocking] source 1944 seq 22: ready 103 ms before start, 103 ms of it with an idle slot in group 55
- [cross_source_blocking] source 4065 seq 12: ready 132 ms before start, 132 ms of it with an idle slot in group 136
- [cross_source_blocking] source 6640 seq 28: ready 177 ms before start, 166 ms of it with an idle slot in group 223
- [cross_source_blocking] source 11804 seq 2: ready 132 ms before start, 113 ms of it with an idle slot in group 382
- [cross_source_blocking] source 1133 seq 31: ready 152 ms before start, 150 ms of it with an idle slot in group 37
- [cross_source_blocking] source 4132 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 136
- [cross_source_blocking] source 13861 seq 16: ready 133 ms before start, 133 ms of it with an idle slot in group 439
- [cross_source_blocking] source 4097 seq 19: ready 126 ms before start, 126 ms of it with an idle slot in group 136
- [cross_source_blocking] source 4173 seq 14: ready 124 ms before start, 124 ms of it with an idle slot in group 136
- [cross_source_blocking] source 4036 seq 15: ready 115 ms before start, 115 ms of it with an idle slot in group 136
- [cross_source_blocking] source 6901 seq 15: ready 121 ms before start, 121 ms of it with an idle slot in group 235
- [cross_source_blocking] source 6905 seq 32: ready 124 ms before start, 124 ms of it with an idle slot in group 235
- [cross_source_blocking] source 6917 seq 29: ready 105 ms before start, 105 ms of it with an idle slot in group 235

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8008 | 8008 | 0 | 1040.383 | 11665.407 | 13172.735 |
| t-202 | 8 | 1 | 84.786 | 10.902 | 1480 | 0 | 636 | 636 | 0 | 415.743 | 9568.255 | 11468.799 |
| t-310 | 6 | 1 | 131.097 | 13.737 | 1910 | 0 | 866 | 866 | 0 | 389.119 | 9240.575 | 10420.223 |
| t-418 | 6 | 1 | 225.2 | 22.744 | 201 | 0 | 1350 | 1350 | 0 | 761.855 | 9175.039 | 10092.543 |
| t-355 | 10 | 1 | 56.219 | 9.856 | 349 | 0 | 627 | 627 | 0 | 284.671 | 9109.503 | 11141.119 |
| t-463 | 12 | 1 | 36.029 | 7.298 | 2195 | 0 | 450 | 450 | 0 | 450.559 | 9109.503 | 9175.039 |
| t-244 | 11 | 1 | 59.519 | 11.109 | 1151 | 0 | 700 | 700 | 0 | 344.063 | 9043.967 | 11206.655 |
| t-298 | 6 | 1 | 119.198 | 13.108 | 3700 | 0 | 757 | 757 | 0 | 356.351 | 9043.967 | 9502.719 |
| t-289 | 6 | 1 | 125.084 | 13.401 | 1711 | 0 | 748 | 748 | 0 | 749.567 | 8978.431 | 9175.039 |
| t-400 | 14 | 1 | 86.955 | 20.454 | 179 | 0 | 1239 | 1239 | 0 | 313.343 | 8912.895 | 9371.647 |
| t-007 | 17 | 1 | 94.817 | 26.077 | 140 | 0 | 1580 | 1580 | 0 | 364.543 | 8847.359 | 9240.575 |
| t-031 | 11 | 1 | 128.922 | 23.474 | 463 | 0 | 1425 | 1425 | 0 | 495.615 | 8847.359 | 9240.575 |
| t-151 | 6 | 1 | 44.324 | 4.639 | 577 | 0 | 289 | 289 | 0 | 246.783 | 8847.359 | 8912.895 |
| t-481 | 12 | 1 | 225.826 | 42.578 | 1993 | 0 | 2602 | 2602 | 0 | 765.951 | 8847.359 | 9568.255 |
| t-058 | 23 | 1 | 62.938 | 23.79 | 768 | 0 | 1481 | 1481 | 0 | 280.575 | 8781.823 | 9568.255 |
| t-094 | 13 | 1 | 59.907 | 12.925 | 1443 | 0 | 805 | 805 | 0 | 221.183 | 8781.823 | 9175.039 |
| t-184 | 20 | 1 | 147.97 | 48.078 | 161 | 0 | 2927 | 2927 | 0 | 630.783 | 8781.823 | 9633.791 |
| t-208 | 21 | 1 | 109.072 | 39.748 | 915 | 0 | 2474 | 2474 | 0 | 448.511 | 8781.823 | 9240.575 |
| t-304 | 19 | 2 | 194.817 | 60.484 | 238 | 0 | 3656 | 3656 | 0 | 315.391 | 8781.823 | 9502.719 |
| t-331 | 12 | 1 | 66.764 | 13.719 | 499 | 0 | 851 | 851 | 0 | 354.303 | 8781.823 | 9109.503 |
| t-349 | 14 | 1 | 82.665 | 18.662 | 379 | 0 | 1123 | 1123 | 0 | 274.431 | 8781.823 | 9371.647 |
| t-370 | 12 | 1 | 37.21 | 7.564 | 568 | 0 | 464 | 464 | 0 | 301.055 | 8781.823 | 8978.431 |
| t-376 | 65 | 1 | 43.345 | 48.199 | 566 | 0 | 3078 | 3078 | 0 | 880.639 | 8781.823 | 11010.047 |
| t-430 | 6 | 1 | 44.333 | 4.805 | 1163 | 0 | 328 | 328 | 0 | 630.783 | 8781.823 | 9175.039 |
| t-139 | 16 | 1 | 32.738 | 9.249 | 1367 | 0 | 624 | 624 | 0 | 352.255 | 8716.287 | 9175.039 |
