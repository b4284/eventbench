# D28-sync-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 85.808s, closing 1.157s, drain 82.298s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 852275 | 852275 | 14088.059 | 11377.586 | 24734.3 | 0 | 0 | 0 | 51 | 4112 | 407534 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 691310 | 6942.869 | 4632.575 | 16973.823 | 30375.935 | 52658.175 | 83689.471 |
| e2e_from_send | 691310 | 6858.925 | 4546.559 | 16941.055 | 30359.551 | 52658.175 | 83623.935 |
| producer_lag | 691310 | 83.941 | 4.879 | 265.727 | 1169.407 | 1597.439 | 1799.167 |
| publish_call | 852275 | 99.032 | 76.863 | 190.975 | 388.863 | 581.631 | 1267.711 |
| queue_wait | 691310 | 6768.289 | 4444.159 | 16859.135 | 30343.167 | 52625.407 | 83623.935 |
| broker_dwell | 691310 | 6699.036 | 4370.431 | 16809.983 | 30294.015 | 52592.639 | 83623.935 |
| receive_call | 758971 | 20.992 | 9.583 | 48.895 | 198.399 | 489.727 | 653.311 |
| ctx_load | 691310 | 15.086 | 10.007 | 34.207 | 73.087 | 109.503 | 273.407 |
| process | 691310 | 9.932 | 0.002 | 0.004 | 0.068 | 2609.151 | 3041.279 |
| ctx_commit | 691310 | 65.354 | 60.607 | 122.623 | 195.199 | 331.775 | 390.399 |
| ack | 691221 | 0.012 | 0.004 | 0.007 | 0.056 | 1.604 | 59.359 |
| service | 691310 | 90.639 | 78.655 | 144.127 | 234.495 | 2691.071 | 3217.407 |
| e2e_large | 44 | 2739.665 | 2559.999 | 3973.119 | 5136.383 | 5136.383 | 5136.383 |
| ready_wait | 691310 | 342.919 | 220.159 | 819.199 | 1900.543 | 2988.031 | 18890.751 |
| blocked | 691310 | 229.459 | 145.535 | 538.111 | 1084.415 | 2285.567 | 15523.839 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11377.586 | 14919.3 |
| applied_total | 411911.571 | 683081 |
| bench_cpu_cores | 4.261 | 5.478 |
| bench_cpu_pct | 17.753 | 22.83 |
| bench_gc_gen0 | 1.551 | 3 |
| bench_gc_gen1 | 0.551 | 2 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 922.963 | 1323.2 |
| bench_mem_mb | 1524.851 | 1839.5 |
| bench_threadpool_queue | 231.694 | 1139 |
| bench_threadpool_threads | 41.531 | 44 |
| broker_backlog | 85979.571 | 158841 |
| ctr_eb_mssql_cpu_pct | 4.267 | 11.22 |
| ctr_eb_mssql_mem_mb | 7309.829 | 7311.8 |
| ctr_eb_mysql_cpu_pct | 1.074 | 1.17 |
| ctr_eb_mysql_mem_mb | 9072.59 | 9077.1 |
| ctr_eb_nats_octopus_cpu_pct | 111.736 | 116.62 |
| ctr_eb_nats_octopus_mem_mb | 44.088 | 59.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 29.859 | 32.29 |
| ctr_eb_pgbouncer_2_mem_mb | 17.676 | 19.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 28.527 | 30.97 |
| ctr_eb_pgbouncer_3_mem_mb | 18.19 | 19.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 28.69 | 31.14 |
| ctr_eb_pgbouncer_4_mem_mb | 17.778 | 18.9 |
| ctr_eb_pgbouncer_cpu_pct | 27.956 | 30.34 |
| ctr_eb_pgbouncer_mem_mb | 17.606 | 19.9 |
| ctr_eb_postgres_cpu_pct | 975.287 | 1021.59 |
| ctr_eb_postgres_mem_mb | 5594.149 | 5874.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.543 | 86.78 |
| in_flight | 76580.041 | 139088 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.867 | 1.91 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 884487.776 | 1540419 |
| octopus_missing_reports_total | 44392.939 | 71022 |
| octopus_mqtt_received_total | 181031.469 | 310637 |
| octopus_mqtt_sent_total | 290877.592 | 490387 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 22.02 | 34 |
| pg_active_loads | 15.347 | 71 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.955 | 100 |
| pg_conn_active | 61.816 | 104 |
| pg_conn_idle | 198.898 | 234 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.469 | 23 |
| pg_conn_total | 260.714 | 262 |
| pg_db_size_mb | 3768.247 | 4402.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25444.571 | 31117.9 |
| pg_tup_del_per_s | 34816.524 | 41654.4 |
| pg_tup_fetch_per_s | 364289.512 | 428738.9 |
| pg_tup_ins_per_s | 49135.598 | 56754.8 |
| pg_tup_upd_per_s | 59944.38 | 68131.2 |
| pg_wal_mb_per_s | 140.289 | 170.86 |
| pgb_cl_active | 493.51 | 517 |
| pgb_cl_waiting | 8.102 | 111 |
| pgb_maxwait_ms | 1.285 | 7.62 |
| pgb_sv_active | 26.796 | 48 |
| pgb_sv_idle | 21.204 | 39 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14088.059 | 18355.2 |
| produced_total | 488491.612 | 822169 |
| publish_mb_per_s | 26.716 | 34.26 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 204,916 records are Completed |
| record_counts | pass | 204,916 records found, 109,077 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 391 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 852,275 events applied by workers, 852,275 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 407,534 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1084 ms, max 15522 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1445 seq 8: ready 206 ms before start, 162 ms of it with an idle slot in group 44
- [cross_source_blocking] source 4908 seq 11: ready 203 ms before start, 203 ms of it with an idle slot in group 170
- [cross_source_blocking] source 8077 seq 42: ready 104 ms before start, 104 ms of it with an idle slot in group 272
- [cross_source_blocking] source 1549 seq 25: ready 102 ms before start, 102 ms of it with an idle slot in group 46
- [cross_source_blocking] source 4117 seq 16: ready 171 ms before start, 129 ms of it with an idle slot in group 136
- [cross_source_blocking] source 9768 seq 8: ready 120 ms before start, 113 ms of it with an idle slot in group 340
- [cross_source_blocking] source 4174 seq 17: ready 176 ms before start, 135 ms of it with an idle slot in group 136
- [cross_source_blocking] source 5295 seq 34: ready 140 ms before start, 128 ms of it with an idle slot in group 182
- [cross_source_blocking] source 11248 seq 7: ready 177 ms before start, 108 ms of it with an idle slot in group 366
- [cross_source_blocking] source 8861 seq 32: ready 224 ms before start, 186 ms of it with an idle slot in group 306
- [cross_source_blocking] source 1551 seq 27: ready 103 ms before start, 101 ms of it with an idle slot in group 46
- [cross_source_blocking] source 4595 seq 20: ready 142 ms before start, 135 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4110 seq 19: ready 141 ms before start, 139 ms of it with an idle slot in group 136
- [cross_source_blocking] source 14921 seq 37: ready 171 ms before start, 129 ms of it with an idle slot in group 468
- [cross_source_blocking] source 7802 seq 26: ready 151 ms before start, 145 ms of it with an idle slot in group 264
- [cross_source_blocking] source 10113 seq 30: ready 251 ms before start, 195 ms of it with an idle slot in group 344
- [cross_source_blocking] source 15248 seq 15: ready 150 ms before start, 115 ms of it with an idle slot in group 476
- [cross_source_blocking] source 10550 seq 23: ready 246 ms before start, 202 ms of it with an idle slot in group 348
- [cross_source_blocking] source 15849 seq 44: ready 143 ms before start, 141 ms of it with an idle slot in group 496
- [cross_source_blocking] source 4116 seq 21: ready 157 ms before start, 155 ms of it with an idle slot in group 136

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 25934 | 25934 | 0 | 15990.783 | 61865.983 | 83886.079 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 35605 | 35605 | 0 | 17301.503 | 38797.311 | 56885.247 |
| t-364 | 66 | 1 | 38.872 | 43.874 | 436 | 0 | 2863 | 2863 | 0 | 10223.615 | 31326.207 | 36175.871 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 2970 | 2970 | 0 | 12648.447 | 30015.487 | 33423.359 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2901 | 2901 | 0 | 10878.975 | 29753.343 | 33816.575 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8820 | 8820 | 0 | 10616.831 | 28966.911 | 32636.927 |
| t-260 | 30 | 2 | 180.858 | 93.625 | 499 | 0 | 5615 | 5615 | 0 | 10878.975 | 27918.335 | 29491.199 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5930 | 5930 | 0 | 11272.191 | 27656.191 | 31326.207 |
| t-276 | 78 | 1 | 37.161 | 47.91 | 423 | 0 | 3076 | 3076 | 0 | 9371.647 | 27394.047 | 35651.583 |
| t-348 | 125 | 6 | 136.718 | 286.558 | 1340 | 0 | 17675 | 17675 | 0 | 11403.263 | 27262.975 | 34078.719 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5736 | 5736 | 0 | 9437.183 | 27131.903 | 30539.775 |
| t-388 | 78 | 2 | 71.428 | 90.661 | 1205 | 0 | 5736 | 5736 | 0 | 9633.791 | 27000.831 | 32112.639 |
| t-234 | 21 | 1 | 127.636 | 43.478 | 141 | 0 | 2756 | 2756 | 0 | 8290.303 | 26476.543 | 29360.127 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 16603 | 16603 | 0 | 11337.727 | 26476.543 | 30146.559 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5630 | 5630 | 0 | 10027.007 | 26083.327 | 30408.703 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2856 | 2856 | 0 | 11927.551 | 25952.255 | 29097.983 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2935 | 2935 | 0 | 11272.191 | 25690.111 | 27262.975 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2844 | 2844 | 0 | 10027.007 | 25559.039 | 27787.263 |
| t-168 | 71 | 2 | 75.117 | 87.222 | 155 | 0 | 5586 | 5586 | 0 | 9895.935 | 25296.895 | 29097.983 |
| t-474 | 63 | 1 | 39.975 | 41.235 | 375 | 0 | 2639 | 2639 | 0 | 7602.175 | 25296.895 | 29753.343 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 20896 | 20896 | 0 | 12910.591 | 24903.679 | 28704.767 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 10842 | 10842 | 0 | 9699.327 | 24510.463 | 29097.983 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17277 | 17277 | 0 | 11468.799 | 24379.391 | 28442.623 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11567 | 11567 | 0 | 10878.975 | 24379.391 | 25821.183 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 19130 | 19130 | 0 | 10616.831 | 24248.319 | 28311.551 |
