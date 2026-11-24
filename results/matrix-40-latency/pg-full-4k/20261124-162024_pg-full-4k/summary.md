# pg-full-4k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 511 workers, offered 3991.312 ev/s
- Phases: run 60.128s, closing 1.089s, drain 4.853s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 275212 | 275212 | 4002.376 | 4002.596 | 15581.7 | 0 | 0 | 0 | 457 | 457 | 418 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200195 | 33.99 | 11.943 | 52.351 | 420.863 | 2162.687 | 6443.007 |
| e2e_from_send | 200195 | 32.2 | 10.463 | 49.887 | 406.783 | 2158.591 | 6438.911 |
| producer_lag | 200195 | 1.79 | 1.063 | 2.865 | 8.263 | 121.919 | 481.023 |
| publish_call | 275212 | 18.778 | 7.571 | 42.847 | 209.407 | 409.599 | 513.791 |
| queue_wait | 200195 | 19.623 | 7.347 | 30.655 | 304.127 | 536.575 | 5156.863 |
| broker_dwell | 200195 | 3.434 | 0.054 | 2.295 | 79.807 | 258.431 | 5115.903 |
| receive_call | 261115 | 80.63 | 38.335 | 229.887 | 451.327 | 493.823 | 629.247 |
| ctx_load | 200195 | 1.304 | 0.75 | 2.187 | 8.711 | 39.551 | 173.695 |
| process | 200195 | 3.56 | 0.002 | 0.004 | 0.019 | 1881.087 | 2998.271 |
| ctx_commit | 200195 | 7.689 | 2.217 | 15.543 | 103.679 | 286.463 | 535.039 |
| ack | 200194 | 0.009 | 0.006 | 0.013 | 0.034 | 0.104 | 23.455 |
| service | 200195 | 12.578 | 3.069 | 17.967 | 112.767 | 1932.287 | 3391.487 |
| e2e_large | 352 | 2415.786 | 2322.431 | 3205.119 | 5382.143 | 6443.007 | 6443.007 |
| ready_wait | 200195 | 4.112 | 0.36 | 7.047 | 77.951 | 276.735 | 2160.639 |
| blocked | 200195 | 3.171 | 0.36 | 6.939 | 51.519 | 171.391 | 510.463 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4002.596 | 4881.2 |
| applied_total | 150963.184 | 246951 |
| bench_cpu_cores | 3.137 | 3.762 |
| bench_cpu_pct | 13.068 | 15.68 |
| bench_gc_gen0 | 1.571 | 4 |
| bench_gc_gen1 | 0.755 | 2 |
| bench_gc_gen2 | 0.694 | 2 |
| bench_gc_heap_mb | 996.837 | 1310.7 |
| bench_mem_mb | 1537.853 | 1675.3 |
| bench_threadpool_queue | 1.694 | 29 |
| bench_threadpool_threads | 35.878 | 38 |
| broker_backlog | 322.857 | 555 |
| ctr_eb_mssql_cpu_pct | 3.676 | 25.61 |
| ctr_eb_mssql_mem_mb | 6825.243 | 6837.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 7.131 | 8.2 |
| ctr_eb_pgbouncer_2_mem_mb | 13.427 | 14.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 7.097 | 8.3 |
| ctr_eb_pgbouncer_3_mem_mb | 13.331 | 14.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 7.931 | 9.08 |
| ctr_eb_pgbouncer_4_mem_mb | 13.696 | 14.2 |
| ctr_eb_pgbouncer_cpu_pct | 6.931 | 7.87 |
| ctr_eb_pgbouncer_mem_mb | 13.512 | 14.3 |
| ctr_eb_postgres_cpu_pct | 302.526 | 499.93 |
| ctr_eb_postgres_mem_mb | 4380.31 | 4919.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 38.65 | 44.74 |
| in_flight | 66.551 | 637 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.856 | 1.07 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 310088.531 | 504793 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 138742.449 | 225746 |
| octopus_mqtt_sent_total | 157050.837 | 257450 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.796 | 17 |
| pg_active_loads | 0.122 | 3 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 15.204 | 71 |
| pg_conn_idle | 194.286 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 209.49 | 252 |
| pg_db_size_mb | 3128.663 | 3540.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 7175.971 | 8953.2 |
| pg_tup_del_per_s | 17129.571 | 27597.3 |
| pg_tup_fetch_per_s | 88718.073 | 113843.9 |
| pg_tup_ins_per_s | 27462.38 | 44535.4 |
| pg_tup_upd_per_s | 11425.19 | 13019.5 |
| pg_wal_mb_per_s | 65.497 | 154.23 |
| pgb_cl_active | 179.408 | 208 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 1.592 | 5 |
| pgb_sv_idle | 36.633 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4002.376 | 4877.3 |
| produced_total | 151029.735 | 246989 |
| publish_mb_per_s | 30.907 | 50.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 51,850 records are Completed |
| record_counts | pass | 51,850 records found, 51,850 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 275,212 events applied by workers, 275,212 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 418 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 52 ms, max 510 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15013 seq 5: ready 236 ms before start, 231 ms of it with an idle slot in group 469
- [cross_source_blocking] source 4359 seq 12: ready 151 ms before start, 151 ms of it with an idle slot in group 147
- [cross_source_blocking] source 13055 seq 3: ready 147 ms before start, 147 ms of it with an idle slot in group 416
- [cross_source_blocking] source 7071 seq 9: ready 148 ms before start, 148 ms of it with an idle slot in group 239
- [cross_source_blocking] source 7839 seq 3: ready 148 ms before start, 148 ms of it with an idle slot in group 265
- [cross_source_blocking] source 5119 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 178
- [cross_source_blocking] source 7733 seq 5: ready 108 ms before start, 108 ms of it with an idle slot in group 263
- [cross_source_blocking] source 11333 seq 11: ready 108 ms before start, 108 ms of it with an idle slot in group 369
- [cross_source_blocking] source 8489 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 290
- [cross_source_blocking] source 8705 seq 8: ready 108 ms before start, 108 ms of it with an idle slot in group 302
- [cross_source_blocking] source 3434 seq 1: ready 104 ms before start, 104 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3476 seq 3: ready 104 ms before start, 104 ms of it with an idle slot in group 119
- [cross_source_blocking] source 2266 seq 18: ready 109 ms before start, 109 ms of it with an idle slot in group 65
- [cross_source_blocking] source 9010 seq 4: ready 103 ms before start, 103 ms of it with an idle slot in group 311
- [cross_source_blocking] source 9458 seq 21: ready 103 ms before start, 103 ms of it with an idle slot in group 326
- [cross_source_blocking] source 11923 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 383
- [cross_source_blocking] source 10651 seq 6: ready 108 ms before start, 108 ms of it with an idle slot in group 350
- [cross_source_blocking] source 3779 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 127
- [cross_source_blocking] source 9512 seq 11: ready 107 ms before start, 107 ms of it with an idle slot in group 329

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 413 | 413 | 0 | 2023.423 | 5472.255 | 6455.295 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 182 | 182 | 0 | 1843.199 | 4046.847 | 4161.535 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 83 | 83 | 0 | 1654.783 | 3768.319 | 4227.071 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 1720.319 | 3604.479 | 3604.479 |
| t-442 | 6 | 1 | 5.582 | 0.584 | 577 | 0 | 40 | 40 | 0 | 10.879 | 659.455 | 659.455 |
| t-089 | 6 | 1 | 6.765 | 0.631 | 1545 | 0 | 46 | 46 | 0 | 11.327 | 622.591 | 622.591 |
| t-133 | 6 | 1 | 8.583 | 0.831 | 273 | 0 | 63 | 63 | 0 | 12.671 | 573.439 | 573.439 |
| t-425 | 89 | 1 | 25.766 | 37.666 | 2911 | 0 | 2496 | 2496 | 0 | 11.967 | 573.439 | 729.087 |
| t-248 | 6 | 1 | 10.358 | 1.01 | 1190 | 0 | 76 | 76 | 0 | 12.479 | 561.151 | 561.151 |
| t-282 | 23 | 1 | 26.173 | 10.072 | 956 | 0 | 648 | 648 | 0 | 11.967 | 561.151 | 761.855 |
| t-124 | 11 | 1 | 4.622 | 0.881 | 1576 | 0 | 79 | 79 | 0 | 11.583 | 557.055 | 659.455 |
| t-033 | 6 | 1 | 21.83 | 2.38 | 859 | 0 | 158 | 158 | 0 | 11.903 | 552.959 | 651.263 |
| t-450 | 63 | 1 | 23.572 | 24.411 | 165 | 0 | 1665 | 1665 | 0 | 11.839 | 548.863 | 786.431 |
| t-107 | 19 | 1 | 12.784 | 3.97 | 453 | 0 | 312 | 312 | 0 | 11.839 | 544.767 | 552.959 |
| t-244 | 11 | 1 | 9.92 | 1.851 | 1151 | 0 | 134 | 134 | 0 | 11.839 | 544.767 | 565.247 |
| t-411 | 15 | 1 | 15.697 | 3.6 | 2644 | 0 | 250 | 250 | 0 | 12.159 | 540.671 | 634.879 |
| t-070 | 24 | 1 | 16.188 | 6.494 | 2303 | 0 | 475 | 475 | 0 | 11.583 | 536.575 | 753.663 |
| t-289 | 6 | 1 | 20.847 | 2.234 | 1711 | 0 | 148 | 148 | 0 | 11.711 | 532.479 | 647.167 |
| t-350 | 85 | 1 | 16.617 | 22.839 | 285 | 0 | 1631 | 1631 | 0 | 12.223 | 532.479 | 671.743 |
| t-204 | 37 | 1 | 5.655 | 3.491 | 1822 | 0 | 286 | 286 | 0 | 12.031 | 528.383 | 552.959 |
| t-111 | 18 | 1 | 24.537 | 7.353 | 3401 | 0 | 501 | 501 | 0 | 11.903 | 524.287 | 622.591 |
| t-048 | 18 | 1 | 7.288 | 2.282 | 136 | 0 | 172 | 172 | 0 | 12.223 | 522.239 | 544.767 |
| t-406 | 36 | 1 | 12.774 | 7.184 | 183 | 0 | 525 | 525 | 0 | 11.903 | 516.095 | 557.055 |
| t-190 | 6 | 1 | 21.482 | 2.243 | 241 | 0 | 147 | 147 | 0 | 11.647 | 514.047 | 573.439 |
| t-344 | 195 | 2 | 25.395 | 83.569 | 571 | 0 | 5635 | 5635 | 0 | 12.159 | 511.999 | 798.719 |
