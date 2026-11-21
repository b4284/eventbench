# S2a-split-16k-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.429s, closing 1.08s, drain 3.634s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 494902 | 494902 | 7889.533 | 7888.457 | 12309.9 | 0 | 0 | 0 | 47 | 2424 | 3519 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 393922 | 93.646 | 52.351 | 127.871 | 1449.983 | 2828.287 | 6279.167 |
| e2e_from_send | 393922 | 88.16 | 48.959 | 116.735 | 1443.839 | 2822.143 | 6189.055 |
| producer_lag | 393922 | 5.486 | 1.924 | 9.535 | 78.655 | 202.495 | 392.703 |
| publish_call | 494902 | 32.736 | 23.055 | 65.375 | 167.295 | 275.455 | 546.815 |
| queue_wait | 393922 | 56.872 | 28.239 | 80.191 | 894.975 | 2445.311 | 4644.863 |
| broker_dwell | 393922 | 38.918 | 12.367 | 47.999 | 876.031 | 2428.927 | 4632.575 |
| receive_call | 444789 | 39.317 | 21.279 | 92.031 | 299.263 | 469.503 | 579.071 |
| ctx_load | 393922 | 7.762 | 5.611 | 15.991 | 35.391 | 62.751 | 222.719 |
| process | 393922 | 10.216 | 0.002 | 0.005 | 0.067 | 2611.199 | 3004.415 |
| ctx_commit | 393922 | 13.181 | 9.191 | 27.807 | 62.463 | 112.895 | 259.071 |
| ack | 393913 | 0.01 | 0.004 | 0.009 | 0.06 | 0.855 | 43.391 |
| service | 393922 | 31.288 | 16.863 | 41.215 | 94.783 | 2631.679 | 3121.151 |
| e2e_large | 39 | 2927.603 | 3022.847 | 3608.575 | 6279.167 | 6279.167 | 6279.167 |
| ready_wait | 393922 | 14.022 | 6.819 | 33.887 | 103.423 | 228.351 | 2156.543 |
| blocked | 393922 | 13.344 | 6.779 | 32.447 | 94.783 | 214.143 | 1029.631 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7888.457 | 9478.9 |
| applied_total | 283898.816 | 472929 |
| bench_cpu_cores | 3.557 | 4.397 |
| bench_cpu_pct | 14.821 | 18.32 |
| bench_gc_gen0 | 1.388 | 3 |
| bench_gc_gen1 | 0.551 | 2 |
| bench_gc_gen2 | 0.367 | 2 |
| bench_gc_heap_mb | 611.286 | 903 |
| bench_mem_mb | 1033.835 | 1323 |
| bench_threadpool_queue | 38.204 | 202 |
| bench_threadpool_threads | 38.592 | 44 |
| broker_backlog | 3055.184 | 5402 |
| ctr_eb_mosquitto_cpu_pct | 83.697 | 86.73 |
| ctr_eb_mosquitto_mem_mb | 12.488 | 32.3 |
| ctr_eb_mssql_cpu_pct | 4.104 | 5.5 |
| ctr_eb_mssql_mem_mb | 7503.5 | 7503.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 27.604 | 29.39 |
| ctr_eb_pgbouncer_2_mem_mb | 17.6 | 18.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 26.039 | 28.27 |
| ctr_eb_pgbouncer_3_mem_mb | 16.965 | 18.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 27.422 | 29.22 |
| ctr_eb_pgbouncer_4_mem_mb | 17.39 | 18.2 |
| ctr_eb_pgbouncer_cpu_pct | 25.831 | 27.19 |
| ctr_eb_pgbouncer_mem_mb | 17.498 | 18.3 |
| ctr_eb_postgres_cpu_pct | 862.617 | 906.51 |
| ctr_eb_postgres_mem_mb | 5579.663 | 5668.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.997 | 79.85 |
| in_flight | 514.429 | 1508 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.226 | 2.34 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 565309.224 | 957610 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 175734.082 | 287230 |
| octopus_mqtt_sent_total | 246974.857 | 417712 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6 | 16 |
| pg_active_loads | 4.122 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 20.061 | 59 |
| pg_conn_idle | 240.592 | 261 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.306 | 3 |
| pg_conn_total | 260.653 | 262 |
| pg_db_size_mb | 3497.02 | 3886.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21613.59 | 23394.9 |
| pg_tup_del_per_s | 28531.747 | 35411.4 |
| pg_tup_fetch_per_s | 209480.247 | 225241.1 |
| pg_tup_ins_per_s | 39577.471 | 50923.1 |
| pg_tup_upd_per_s | 41318.245 | 45220 |
| pg_wal_mb_per_s | 100.962 | 124.67 |
| pgb_cl_active | 413.204 | 439 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.653 | 17 |
| pgb_sv_idle | 41.347 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7889.533 | 9403.7 |
| produced_total | 284413.245 | 473327 |
| publish_mb_per_s | 15.874 | 26.86 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 138,580 records are Completed |
| record_counts | pass | 138,580 records found, 68,954 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 420 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 494,902 events applied by workers, 494,902 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 3,519 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 95 ms, max 1029 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15865 seq 31: ready 122 ms before start, 122 ms of it with an idle slot in group 496
- [cross_source_blocking] source 8285 seq 22: ready 140 ms before start, 140 ms of it with an idle slot in group 282
- [cross_source_blocking] source 2357 seq 7: ready 140 ms before start, 140 ms of it with an idle slot in group 68
- [cross_source_blocking] source 6287 seq 6: ready 140 ms before start, 140 ms of it with an idle slot in group 214
- [cross_source_blocking] source 13197 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 420
- [cross_source_blocking] source 15710 seq 26: ready 108 ms before start, 108 ms of it with an idle slot in group 490
- [cross_source_blocking] source 8831 seq 22: ready 112 ms before start, 112 ms of it with an idle slot in group 306
- [cross_source_blocking] source 11435 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 372
- [cross_source_blocking] source 5457 seq 29: ready 107 ms before start, 107 ms of it with an idle slot in group 186
- [cross_source_blocking] source 15789 seq 9: ready 112 ms before start, 112 ms of it with an idle slot in group 494
- [cross_source_blocking] source 15845 seq 42: ready 108 ms before start, 108 ms of it with an idle slot in group 496
- [cross_source_blocking] source 10515 seq 12: ready 196 ms before start, 195 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10547 seq 17: ready 197 ms before start, 196 ms of it with an idle slot in group 348
- [cross_source_blocking] source 14954 seq 37: ready 194 ms before start, 194 ms of it with an idle slot in group 468
- [cross_source_blocking] source 10147 seq 23: ready 325 ms before start, 325 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10121 seq 21: ready 120 ms before start, 120 ms of it with an idle slot in group 344
- [cross_source_blocking] source 9288 seq 26: ready 325 ms before start, 325 ms of it with an idle slot in group 320
- [cross_source_blocking] source 13762 seq 18: ready 330 ms before start, 330 ms of it with an idle slot in group 438
- [cross_source_blocking] source 4911 seq 11: ready 330 ms before start, 330 ms of it with an idle slot in group 170
- [cross_source_blocking] source 6275 seq 14: ready 121 ms before start, 121 ms of it with an idle slot in group 214
- [cross_source_blocking] source 6621 seq 17: ready 120 ms before start, 120 ms of it with an idle slot in group 222
- [cross_source_blocking] source 6372 seq 7: ready 326 ms before start, 326 ms of it with an idle slot in group 218

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 88 | 88 | 0 | 2375.679 | 4816.895 | 6291.455 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 152 | 152 | 0 | 52.223 | 2719.743 | 2752.511 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 404 | 404 | 0 | 48.127 | 2572.287 | 2965.503 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3931 | 3931 | 0 | 55.295 | 2424.831 | 3948.543 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1432 | 1432 | 0 | 54.271 | 2342.911 | 2932.735 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 907 | 907 | 0 | 52.991 | 2310.143 | 3031.039 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5544 | 5544 | 0 | 58.111 | 2293.759 | 3686.399 |
| t-056 | 7 | 1 | 59.8 | 7.224 | 347 | 0 | 466 | 466 | 0 | 50.943 | 2162.687 | 2473.983 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5632 | 5632 | 0 | 57.087 | 2146.303 | 3112.959 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1057 | 1057 | 0 | 52.735 | 2097.151 | 2785.279 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 919 | 919 | 0 | 50.431 | 2072.575 | 2981.887 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7474 | 7474 | 0 | 55.295 | 2072.575 | 4751.359 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 446 | 446 | 0 | 50.431 | 2056.191 | 2998.271 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1711 | 1711 | 0 | 53.503 | 2039.807 | 3031.039 |
| t-090 | 7 | 1 | 49.513 | 5.825 | 242 | 0 | 386 | 386 | 0 | 53.247 | 1974.271 | 3014.655 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 382 | 382 | 0 | 54.527 | 1974.271 | 2719.743 |
| t-260 | 30 | 2 | 103.347 | 53.5 | 499 | 0 | 3303 | 3303 | 0 | 51.967 | 1966.079 | 4456.447 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 749 | 749 | 0 | 50.431 | 1966.079 | 2801.663 |
| t-482 | 10 | 1 | 83.28 | 14.168 | 2234 | 0 | 877 | 877 | 0 | 51.455 | 1966.079 | 2949.119 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 3869 | 3869 | 0 | 54.271 | 1925.119 | 2949.119 |
| t-380 | 22 | 1 | 96.015 | 34.358 | 132 | 0 | 2098 | 2098 | 0 | 55.039 | 1900.543 | 2949.119 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 2618 | 2618 | 0 | 53.247 | 1875.967 | 2883.583 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 575 | 575 | 0 | 49.663 | 1875.967 | 2818.047 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 6215 | 6215 | 0 | 54.783 | 1859.583 | 2965.503 |
| t-468 | 61 | 4 | 157.515 | 157.545 | 1645 | 0 | 9559 | 9559 | 0 | 52.479 | 1826.815 | 3047.423 |
