# Ta-pg — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.18s, closing 1.67s, drain 1.425s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000529 | 1000529 | 15904.839 | 15938.18 | 21995.1 | 0 | 0 | 0 | 458 | 5162 | 9492 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797164 | 80.127 | 34.943 | 105.791 | 1421.311 | 2813.951 | 5349.375 |
| e2e_from_send | 797164 | 76.799 | 32.767 | 98.431 | 1417.215 | 2811.903 | 5349.375 |
| producer_lag | 797164 | 3.329 | 1.428 | 6.743 | 35.455 | 93.247 | 441.087 |
| publish_call | 1000529 | 29.096 | 17.455 | 62.719 | 160.127 | 243.583 | 368.127 |
| queue_wait | 797164 | 46.839 | 17.199 | 57.759 | 883.711 | 2381.823 | 5332.991 |
| broker_dwell | 797164 | 31.723 | 4.519 | 29.647 | 866.815 | 2365.439 | 5312.511 |
| receive_call | 906829 | 38.148 | 19.487 | 91.455 | 298.495 | 461.823 | 536.575 |
| ctx_load | 797164 | 5.224 | 3.781 | 9.871 | 26.991 | 68.031 | 109.695 |
| process | 797164 | 10.737 | 0.002 | 0.004 | 0.049 | 2619.391 | 3010.559 |
| ctx_commit | 797164 | 13.901 | 9.751 | 28.495 | 62.047 | 102.527 | 286.207 |
| ack | 797163 | 0.01 | 0.005 | 0.01 | 0.058 | 0.764 | 36.703 |
| service | 797164 | 29.96 | 14.399 | 38.367 | 94.463 | 2635.775 | 3102.719 |
| e2e_large | 356 | 2408.874 | 2394.111 | 3182.591 | 4485.119 | 4993.023 | 4993.023 |
| ready_wait | 797164 | 11.921 | 3.601 | 26.703 | 126.783 | 315.135 | 1912.831 |
| blocked | 797164 | 10.861 | 3.593 | 25.375 | 108.991 | 213.631 | 539.647 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15938.18 | 19302.1 |
| applied_total | 577887.98 | 957011 |
| bench_cpu_cores | 6.676 | 7.836 |
| bench_cpu_pct | 27.817 | 32.65 |
| bench_gc_gen0 | 2.224 | 4 |
| bench_gc_gen1 | 0.755 | 2 |
| bench_gc_gen2 | 0.551 | 1 |
| bench_gc_heap_mb | 1460.698 | 1970 |
| bench_mem_mb | 2259.139 | 2594.4 |
| bench_threadpool_queue | 70.694 | 570 |
| bench_threadpool_threads | 42.796 | 44 |
| broker_backlog | 2759.061 | 15349 |
| ctr_eb_mssql_cpu_pct | 2.901 | 4.09 |
| ctr_eb_mssql_mem_mb | 4454.259 | 4455.6 |
| ctr_eb_mysql_cpu_pct | 0.823 | 0.93 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.58 | 21 |
| ctr_eb_pgbouncer_2_mem_mb | 15.516 | 16.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.314 | 21.14 |
| ctr_eb_pgbouncer_3_mem_mb | 15.282 | 16.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 19.863 | 20.89 |
| ctr_eb_pgbouncer_4_mem_mb | 15.718 | 16.3 |
| ctr_eb_pgbouncer_cpu_pct | 19.183 | 20.46 |
| ctr_eb_pgbouncer_mem_mb | 15.139 | 16.2 |
| ctr_eb_postgres_cpu_pct | 656.549 | 815.55 |
| ctr_eb_postgres_mem_mb | 4679.584 | 5063.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 70.321 | 76.37 |
| in_flight | 1078.306 | 2911 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.57 | 1.62 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1134187.837 | 1929838 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 197673.816 | 329978 |
| octopus_mqtt_sent_total | 381229.143 | 663678 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 8.878 | 16 |
| pg_active_loads | 2.939 | 9 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 17.306 | 30 |
| pg_conn_idle | 236.143 | 246 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 253.449 | 255 |
| pg_db_size_mb | 3527.859 | 3938 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19117.053 | 21184.6 |
| pg_tup_del_per_s | 29752.251 | 39783.3 |
| pg_tup_fetch_per_s | 211520.371 | 268716.3 |
| pg_tup_ins_per_s | 41414.188 | 56479.9 |
| pg_tup_upd_per_s | 42447.086 | 50007.6 |
| pg_wal_mb_per_s | 105.153 | 131.93 |
| pgb_cl_active | 251.327 | 260 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 8.388 | 28 |
| pgb_sv_idle | 39.612 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15904.839 | 16716.3 |
| produced_total | 578966.286 | 959922 |
| publish_mb_per_s | 51.69 | 74.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,529 events applied by workers, 1,000,529 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 9,492 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 109 ms, max 539 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 347 seq 9: ready 104 ms before start, 104 ms of it with an idle slot in group 10
- [cross_source_blocking] source 5076 seq 11: ready 169 ms before start, 169 ms of it with an idle slot in group 177
- [cross_source_blocking] source 4299 seq 27: ready 162 ms before start, 133 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4772 seq 13: ready 146 ms before start, 104 ms of it with an idle slot in group 165
- [cross_source_blocking] source 2242 seq 31: ready 116 ms before start, 114 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4346 seq 24: ready 152 ms before start, 138 ms of it with an idle slot in group 147
- [cross_source_blocking] source 14978 seq 21: ready 181 ms before start, 156 ms of it with an idle slot in group 469
- [cross_source_blocking] source 12299 seq 7: ready 101 ms before start, 101 ms of it with an idle slot in group 391
- [cross_source_blocking] source 11580 seq 5: ready 171 ms before start, 147 ms of it with an idle slot in group 376
- [cross_source_blocking] source 3275 seq 11: ready 103 ms before start, 103 ms of it with an idle slot in group 109
- [cross_source_blocking] source 15874 seq 31: ready 157 ms before start, 122 ms of it with an idle slot in group 496
- [cross_source_blocking] source 15833 seq 30: ready 124 ms before start, 123 ms of it with an idle slot in group 496
- [cross_source_blocking] source 9187 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 315
- [cross_source_blocking] source 642 seq 17: ready 181 ms before start, 181 ms of it with an idle slot in group 19
- [cross_source_blocking] source 5398 seq 11: ready 182 ms before start, 182 ms of it with an idle slot in group 184
- [cross_source_blocking] source 3990 seq 30: ready 177 ms before start, 175 ms of it with an idle slot in group 132
- [cross_source_blocking] source 5718 seq 23: ready 181 ms before start, 149 ms of it with an idle slot in group 197
- [cross_source_blocking] source 41 seq 6: ready 143 ms before start, 104 ms of it with an idle slot in group 1
- [cross_source_blocking] source 2216 seq 35: ready 103 ms before start, 102 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4321 seq 25: ready 148 ms before start, 134 ms of it with an idle slot in group 147

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1957.887 | 4718.591 | 5013.503 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2039.807 | 4259.839 | 4947.967 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1875.967 | 3637.247 | 4292.607 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1597.439 | 3260.415 | 4456.447 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 36.863 | 2588.671 | 4292.607 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 37.119 | 2408.447 | 5373.951 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 37.119 | 2392.063 | 4554.751 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 36.351 | 2359.295 | 2932.735 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 36.607 | 2310.143 | 2949.119 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 36.095 | 2310.143 | 3325.951 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 35.583 | 2310.143 | 2981.887 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 38.399 | 2310.143 | 2899.967 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 33.791 | 2277.375 | 2768.895 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 34.559 | 2277.375 | 2981.887 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 35.583 | 2228.223 | 3211.263 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 34.303 | 2211.839 | 2949.119 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 35.327 | 2211.839 | 2867.199 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 33.023 | 2195.455 | 2932.735 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 33.791 | 2162.687 | 2736.127 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 39.167 | 2162.687 | 2965.503 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 34.559 | 2146.303 | 2768.895 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 35.327 | 2129.919 | 2768.895 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 36.863 | 2129.919 | 2949.119 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 36.095 | 2113.535 | 3653.631 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 36.095 | 2088.959 | 3801.087 |
