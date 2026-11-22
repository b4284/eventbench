# W28-mosq-2-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.653s, closing 1.549s, drain 66.399s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 295072 | 295072 | 4707.958 | 4660.678 | 7359.5 | 0 | 0 | 0 | 278 | 1689 | 58707 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 222012 | 10943.286 | 10698.751 | 16228.351 | 19152.895 | 21692.415 | 23838.719 |
| e2e_from_send | 222012 | 850.567 | 649.215 | 1563.647 | 3887.103 | 6459.391 | 8675.327 |
| producer_lag | 222012 | 10092.718 | 9945.087 | 15171.583 | 17743.871 | 21037.055 | 21413.887 |
| publish_call | 295072 | 413.213 | 340.735 | 852.991 | 1489.919 | 2082.815 | 2390.015 |
| queue_wait | 222012 | 777.5 | 585.727 | 1453.055 | 3733.503 | 6369.279 | 8658.943 |
| broker_dwell | 222012 | 364.087 | 114.623 | 1011.711 | 3407.871 | 6057.983 | 8323.071 |
| receive_call | 260450 | 38.061 | 15.631 | 99.007 | 322.047 | 468.223 | 514.303 |
| ctx_load | 222012 | 16.842 | 11.495 | 32.767 | 133.119 | 161.791 | 704.511 |
| process | 222012 | 11.753 | 0.001 | 0.002 | 0.012 | 2629.631 | 3000.319 |
| ctx_commit | 222012 | 44.438 | 26.895 | 87.743 | 310.527 | 566.783 | 2044.927 |
| ack | 205073 | 0.003 | 0.001 | 0.004 | 0.012 | 0.037 | 42.847 |
| service | 222012 | 73.067 | 43.807 | 118.207 | 382.719 | 2752.511 | 4796.415 |
| e2e_large | 198 | 12865.391 | 12820.479 | 18022.399 | 20496.383 | 23117.823 | 23117.823 |
| ready_wait | 222012 | 91.16 | 42.143 | 247.935 | 524.031 | 899.583 | 1268.735 |
| blocked | 222012 | 75.805 | 37.311 | 207.231 | 407.807 | 708.095 | 1243.135 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4660.678 | 6816.4 |
| applied_total | 161723.3 | 282053 |
| bench_cpu_cores | 1.693 | 2.167 |
| bench_cpu_pct | 21.167 | 27.09 |
| bench_gc_gen0 | 0.48 | 2 |
| bench_gc_gen1 | 0.38 | 1 |
| bench_gc_gen2 | 0.38 | 1 |
| bench_gc_heap_mb | 1033.182 | 1377.4 |
| bench_mem_mb | 1775.238 | 1918.8 |
| bench_threadpool_queue | 3.16 | 56 |
| bench_threadpool_threads | 14.08 | 15 |
| broker_backlog | 54689.08 | 117913 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1929.92 | 7071 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 330066.48 | 569164 |
| octopus_missing_reports_total | 15828.4 | 24818 |
| octopus_mqtt_received_total | 97826.24 | 154513 |
| octopus_mqtt_sent_total | 122146.96 | 206414 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 21.68 | 45 |
| pg_active_loads | 10.76 | 24 |
| pg_active_polls | 0.04 | 1 |
| pg_active_sends | 0.04 | 1 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 56.18 | 109 |
| pg_conn_idle | 207.5 | 242 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.54 | 31 |
| pg_conn_total | 263.68 | 264 |
| pg_db_size_mb | 3664.032 | 4232.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24091.696 | 30132.2 |
| pg_tup_del_per_s | 31181.716 | 43344 |
| pg_tup_fetch_per_s | 317604.854 | 387857.1 |
| pg_tup_ins_per_s | 43060.312 | 52793.4 |
| pg_tup_upd_per_s | 51010.678 | 61563.2 |
| pg_wal_mb_per_s | 117.042 | 181.99 |
| produced_per_s | 4707.958 | 8574.3 |
| produced_total | 163653.22 | 284938 |
| publish_mb_per_s | 23.973 | 48.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 180,618 records are Completed |
| record_counts | pass | 180,618 records found, 38,963 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 348 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 295,072 events applied by workers, 295,072 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 58,707 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 408 ms, max 1242 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6619 seq 28: ready 125 ms before start, 108 ms of it with an idle slot in group 222
- [cross_source_blocking] source 5957 seq 27: ready 104 ms before start, 104 ms of it with an idle slot in group 206
- [cross_source_blocking] source 5280 seq 23: ready 104 ms before start, 104 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6619 seq 29: ready 103 ms before start, 101 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6808 seq 17: ready 136 ms before start, 128 ms of it with an idle slot in group 231
- [cross_source_blocking] source 3742 seq 12: ready 190 ms before start, 154 ms of it with an idle slot in group 126
- [cross_source_blocking] source 3080 seq 15: ready 172 ms before start, 172 ms of it with an idle slot in group 103
- [cross_source_blocking] source 13859 seq 49: ready 118 ms before start, 118 ms of it with an idle slot in group 438
- [cross_source_blocking] source 5280 seq 25: ready 119 ms before start, 113 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6587 seq 31: ready 131 ms before start, 131 ms of it with an idle slot in group 222
- [cross_source_blocking] source 5312 seq 26: ready 118 ms before start, 118 ms of it with an idle slot in group 182
- [cross_source_blocking] source 13763 seq 37: ready 142 ms before start, 110 ms of it with an idle slot in group 438
- [cross_source_blocking] source 6555 seq 18: ready 149 ms before start, 148 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6619 seq 30: ready 151 ms before start, 150 ms of it with an idle slot in group 222
- [cross_source_blocking] source 13827 seq 35: ready 129 ms before start, 104 ms of it with an idle slot in group 438
- [cross_source_blocking] source 12285 seq 10: ready 146 ms before start, 132 ms of it with an idle slot in group 391
- [cross_source_blocking] source 8725 seq 11: ready 160 ms before start, 130 ms of it with an idle slot in group 303
- [cross_source_blocking] source 6643 seq 42: ready 132 ms before start, 106 ms of it with an idle slot in group 223
- [cross_source_blocking] source 738 seq 18: ready 982 ms before start, 955 ms of it with an idle slot in group 22
- [cross_source_blocking] source 8718 seq 19: ready 220 ms before start, 111 ms of it with an idle slot in group 303

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2270 | 2270 | 0 | 11927.551 | 22020.095 | 22806.527 |
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 924 | 924 | 0 | 11206.655 | 21626.879 | 22020.095 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 690 | 690 | 0 | 12517.375 | 21626.879 | 21757.951 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 6738 | 6738 | 0 | 11665.407 | 20709.375 | 22151.167 |
| t-462 | 10 | 1 | 55.668 | 9.033 | 2006 | 0 | 386 | 386 | 0 | 10551.295 | 20709.375 | 21757.951 |
| t-398 | 15 | 1 | 84.303 | 22.108 | 482 | 0 | 1007 | 1007 | 0 | 10420.223 | 20578.303 | 21626.879 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 332 | 332 | 0 | 12648.447 | 20316.159 | 21626.879 |
| t-454 | 7 | 1 | 292.242 | 33.739 | 266 | 0 | 1616 | 1616 | 0 | 11010.047 | 20054.015 | 21757.951 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 1969 | 1969 | 0 | 10551.295 | 19922.943 | 22151.167 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 15784 | 15784 | 0 | 11075.583 | 19791.871 | 22151.167 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1264 | 1264 | 0 | 11010.047 | 19660.799 | 22151.167 |
| t-166 | 6 | 1 | 56.693 | 5.447 | 292 | 0 | 249 | 249 | 0 | 9699.327 | 19529.727 | 20185.087 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 134 | 134 | 0 | 11141.119 | 19529.727 | 23199.743 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 8194 | 8194 | 0 | 11141.119 | 19529.727 | 21889.023 |
| t-470 | 16 | 1 | 103.193 | 27.186 | 140 | 0 | 1309 | 1309 | 0 | 10944.511 | 19529.727 | 21757.951 |
| t-479 | 34 | 1 | 80.711 | 45.517 | 3383 | 0 | 2131 | 2131 | 0 | 11075.583 | 19529.727 | 22020.095 |
| t-039 | 28 | 1 | 39.722 | 18.585 | 1459 | 0 | 907 | 907 | 0 | 10354.687 | 19398.655 | 21626.879 |
| t-046 | 24 | 2 | 183.851 | 74.131 | 3235 | 0 | 3316 | 3316 | 0 | 10747.903 | 19398.655 | 21889.023 |
| t-055 | 65 | 5 | 220.155 | 227.855 | 359 | 0 | 10389 | 10389 | 0 | 10878.975 | 19398.655 | 22020.095 |
| t-063 | 8 | 1 | 42.658 | 5.912 | 1296 | 0 | 281 | 281 | 0 | 10682.367 | 19398.655 | 21889.023 |
| t-150 | 15 | 1 | 67.298 | 17.194 | 990 | 0 | 787 | 787 | 0 | 10289.151 | 19398.655 | 21364.735 |
| t-246 | 32 | 5 | 407.356 | 215.226 | 494 | 0 | 9615 | 9615 | 0 | 11206.655 | 19398.655 | 21889.023 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 11882 | 11882 | 0 | 11141.119 | 19398.655 | 22151.167 |
| t-414 | 21 | 1 | 71.312 | 24.948 | 347 | 0 | 1203 | 1203 | 0 | 10289.151 | 19398.655 | 21889.023 |
| t-423 | 11 | 1 | 79.469 | 14.335 | 726 | 0 | 656 | 656 | 0 | 10616.831 | 19398.655 | 21495.807 |
