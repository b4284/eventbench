# pg-full-8k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 544 workers, offered 7974.007 ev/s
- Phases: run 60.133s, closing 1.183s, drain 1.763s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 518109 | 518109 | 7952.633 | 7952.867 | 18446.1 | 0 | 0 | 0 | 430 | 430 | 1249 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 397703 | 44.219 | 18.399 | 91.647 | 383.487 | 754.175 | 6508.543 |
| e2e_from_send | 397703 | 42.154 | 16.959 | 88.255 | 373.503 | 686.079 | 6508.543 |
| producer_lag | 397703 | 2.065 | 1.054 | 3.117 | 19.167 | 134.911 | 375.039 |
| publish_call | 518109 | 21.308 | 10.887 | 47.743 | 147.583 | 271.359 | 383.487 |
| queue_wait | 397703 | 24.942 | 9.967 | 50.783 | 263.423 | 486.911 | 4808.703 |
| broker_dwell | 397703 | 7.029 | 1.224 | 7.323 | 145.535 | 382.207 | 4784.127 |
| receive_call | 483183 | 56.271 | 25.855 | 148.991 | 396.543 | 487.167 | 522.751 |
| ctx_load | 397703 | 2.092 | 1.34 | 4.131 | 10.679 | 37.375 | 118.527 |
| process | 397703 | 1.71 | 0.002 | 0.004 | 0.021 | 0.409 | 2996.223 |
| ctx_commit | 397703 | 13.368 | 4.695 | 32.287 | 137.471 | 250.495 | 447.487 |
| ack | 397702 | 0.008 | 0.005 | 0.013 | 0.044 | 0.188 | 14.383 |
| service | 397703 | 17.213 | 6.459 | 36.095 | 150.655 | 286.463 | 3395.583 |
| e2e_large | 333 | 2399.067 | 2328.575 | 3184.639 | 5439.487 | 6508.543 | 6508.543 |
| ready_wait | 397703 | 6.79 | 0.71 | 12.079 | 128.127 | 288.767 | 1741.823 |
| blocked | 397703 | 4.785 | 0.71 | 11.151 | 66.367 | 130.623 | 344.831 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7952.867 | 11019.7 |
| applied_total | 293418.49 | 484346 |
| bench_cpu_cores | 4.55 | 5.245 |
| bench_cpu_pct | 18.956 | 21.85 |
| bench_gc_gen0 | 1.755 | 3 |
| bench_gc_gen1 | 0.714 | 2 |
| bench_gc_gen2 | 0.571 | 1 |
| bench_gc_heap_mb | 1213.367 | 1543.3 |
| bench_mem_mb | 1893.551 | 2020.9 |
| bench_threadpool_queue | 15.388 | 208 |
| bench_threadpool_threads | 43.102 | 44 |
| broker_backlog | 829.02 | 1584 |
| ctr_eb_mssql_cpu_pct | 5.305 | 26.75 |
| ctr_eb_mssql_mem_mb | 6505.061 | 6506 |
| ctr_eb_pgbouncer_2_cpu_pct | 12.084 | 13.67 |
| ctr_eb_pgbouncer_2_mem_mb | 14.057 | 14.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 12.125 | 14.21 |
| ctr_eb_pgbouncer_3_mem_mb | 14.076 | 16 |
| ctr_eb_pgbouncer_4_cpu_pct | 11.702 | 13.35 |
| ctr_eb_pgbouncer_4_mem_mb | 13.859 | 15.4 |
| ctr_eb_pgbouncer_cpu_pct | 11.355 | 13.01 |
| ctr_eb_pgbouncer_mem_mb | 13.682 | 14.5 |
| ctr_eb_postgres_cpu_pct | 400.864 | 467.61 |
| ctr_eb_postgres_mem_mb | 4650.58 | 5062.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.752 | 53.65 |
| in_flight | 230.796 | 2390 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.173 | 1.32 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 585775.735 | 1000978 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 154932.204 | 271055 |
| octopus_mqtt_sent_total | 235763.612 | 410816 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.143 | 19 |
| pg_active_loads | 0.347 | 4 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 27.245 | 71 |
| pg_conn_idle | 231.184 | 258 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 258.429 | 260 |
| pg_db_size_mb | 3212.467 | 3605.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 11311.612 | 14292.2 |
| pg_tup_del_per_s | 20783.51 | 31306.8 |
| pg_tup_fetch_per_s | 130933.069 | 154819.9 |
| pg_tup_ins_per_s | 30775.459 | 45435.7 |
| pg_tup_upd_per_s | 21737.753 | 24799.8 |
| pg_wal_mb_per_s | 74.781 | 99.1 |
| pgb_cl_active | 249.49 | 260 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.122 | 21 |
| pgb_sv_idle | 43.878 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7952.633 | 9199.8 |
| produced_total | 293649.286 | 484401 |
| publish_mb_per_s | 36.223 | 57.18 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,665 records are Completed |
| record_counts | pass | 82,665 records found, 82,665 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 518,109 events applied by workers, 518,109 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,249 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 66 ms, max 345 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15395 seq 12: ready 104 ms before start, 104 ms of it with an idle slot in group 482
- [cross_source_blocking] source 9972 seq 2: ready 104 ms before start, 104 ms of it with an idle slot in group 341
- [cross_source_blocking] source 2468 seq 4: ready 104 ms before start, 104 ms of it with an idle slot in group 73
- [cross_source_blocking] source 7616 seq 8: ready 102 ms before start, 102 ms of it with an idle slot in group 261
- [cross_source_blocking] source 11216 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 365
- [cross_source_blocking] source 13375 seq 10: ready 108 ms before start, 108 ms of it with an idle slot in group 428
- [cross_source_blocking] source 10200 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 344
- [cross_source_blocking] source 4496 seq 4: ready 102 ms before start, 102 ms of it with an idle slot in group 156
- [cross_source_blocking] source 11456 seq 5: ready 102 ms before start, 102 ms of it with an idle slot in group 372
- [cross_source_blocking] source 4428 seq 4: ready 102 ms before start, 102 ms of it with an idle slot in group 152
- [cross_source_blocking] source 4559 seq 5: ready 132 ms before start, 132 ms of it with an idle slot in group 160
- [cross_source_blocking] source 1984 seq 10: ready 123 ms before start, 123 ms of it with an idle slot in group 57
- [cross_source_blocking] source 5023 seq 6: ready 133 ms before start, 133 ms of it with an idle slot in group 174
- [cross_source_blocking] source 8179 seq 4: ready 132 ms before start, 132 ms of it with an idle slot in group 276
- [cross_source_blocking] source 3347 seq 9: ready 133 ms before start, 133 ms of it with an idle slot in group 115
- [cross_source_blocking] source 8745 seq 16: ready 123 ms before start, 123 ms of it with an idle slot in group 304
- [cross_source_blocking] source 12956 seq 11: ready 130 ms before start, 130 ms of it with an idle slot in group 409
- [cross_source_blocking] source 6232 seq 8: ready 190 ms before start, 190 ms of it with an idle slot in group 213
- [cross_source_blocking] source 2268 seq 19: ready 124 ms before start, 124 ms of it with an idle slot in group 65
- [cross_source_blocking] source 10784 seq 5: ready 120 ms before start, 120 ms of it with an idle slot in group 353

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 384 | 384 | 0 | 2007.039 | 5963.775 | 6520.831 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 81 | 81 | 0 | 2162.687 | 4390.911 | 4390.911 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 86 | 86 | 0 | 1785.855 | 4177.919 | 4489.215 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 167 | 167 | 0 | 1884.159 | 3883.007 | 5242.879 |
| t-210 | 71 | 1 | 35.639 | 40.578 | 136 | 0 | 2671 | 2671 | 0 | 18.815 | 532.479 | 991.231 |
| t-378 | 55 | 1 | 53.238 | 47.699 | 3659 | 0 | 3045 | 3045 | 0 | 17.791 | 503.807 | 1011.711 |
| t-004 | 38 | 1 | 54.047 | 35.392 | 2178 | 0 | 2185 | 2185 | 0 | 18.175 | 493.567 | 843.775 |
| t-441 | 30 | 1 | 87.598 | 42.483 | 2203 | 0 | 2632 | 2632 | 0 | 18.559 | 487.423 | 913.407 |
| t-120 | 37 | 1 | 51.664 | 31.368 | 1412 | 0 | 2020 | 2020 | 0 | 18.175 | 483.327 | 835.583 |
| t-429 | 200 | 1 | 11.232 | 38.284 | 1395 | 0 | 2733 | 2733 | 0 | 19.071 | 481.279 | 753.663 |
| t-186 | 34 | 1 | 64.425 | 36.339 | 1259 | 0 | 2346 | 2346 | 0 | 18.047 | 479.231 | 913.407 |
| t-197 | 42 | 1 | 64.221 | 43.742 | 725 | 0 | 2709 | 2709 | 0 | 18.815 | 477.183 | 716.799 |
| t-105 | 33 | 1 | 45.605 | 26.221 | 851 | 0 | 1677 | 1677 | 0 | 18.687 | 473.087 | 786.431 |
| t-029 | 28 | 1 | 88.83 | 41.191 | 1732 | 0 | 2585 | 2585 | 0 | 18.687 | 466.943 | 741.375 |
| t-106 | 24 | 1 | 110.394 | 44.885 | 824 | 0 | 2762 | 2762 | 0 | 18.303 | 460.799 | 823.295 |
| t-464 | 153 | 2 | 30.877 | 77.682 | 352 | 0 | 4990 | 4990 | 0 | 17.791 | 458.751 | 749.567 |
| t-055 | 65 | 2 | 62.901 | 65.101 | 359 | 0 | 3982 | 3982 | 0 | 18.175 | 456.703 | 811.007 |
| t-469 | 105 | 2 | 47.528 | 82.697 | 278 | 0 | 5255 | 5255 | 0 | 18.047 | 456.703 | 815.103 |
| t-406 | 36 | 1 | 25.549 | 14.368 | 183 | 0 | 994 | 994 | 0 | 17.919 | 454.655 | 598.015 |
| t-450 | 63 | 1 | 47.144 | 48.822 | 165 | 0 | 3013 | 3013 | 0 | 18.815 | 448.511 | 741.375 |
| t-045 | 47 | 1 | 51.221 | 39.914 | 1398 | 0 | 2526 | 2526 | 0 | 19.327 | 442.367 | 827.391 |
| t-147 | 79 | 3 | 90.233 | 121.167 | 1078 | 0 | 7429 | 7429 | 0 | 18.687 | 440.319 | 868.351 |
| t-412 | 6 | 1 | 17.63 | 1.632 | 468 | 0 | 116 | 116 | 0 | 16.319 | 440.319 | 505.855 |
| t-199 | 24 | 1 | 7.623 | 3.232 | 251 | 0 | 228 | 228 | 0 | 18.687 | 434.175 | 434.175 |
| t-417 | 75 | 1 | 27.326 | 33.963 | 3449 | 0 | 2154 | 2154 | 0 | 19.455 | 434.175 | 700.415 |
