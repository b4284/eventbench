# 06-pgmq-tuned — FAIL ❌

- Broker: `pgmq fifo via eb_read_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 61.085s, closing 1.675s, drain 18.595s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 920356 | 920356 | 14873.378 | 11043.696 | 16294.4 | 0 | 0 | 0 | 296 | 4628 | 296066 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 636400 | 27436.878 | 26148.863 | 41615.359 | 44531.711 | 45940.735 | 48463.871 |
| e2e_from_send | 636400 | 11343.106 | 11067.391 | 21954.559 | 24870.911 | 26050.559 | 28459.007 |
| producer_lag | 636400 | 16093.802 | 16605.183 | 22233.087 | 24756.223 | 25837.567 | 26476.543 |
| publish_call | 1913 | 1031.145 | 973.311 | 1643.519 | 2439.167 | 2791.423 | 3033.087 |
| queue_wait | 636400 | 11200.784 | 10903.551 | 21856.255 | 24805.375 | 25935.871 | 28377.087 |
| broker_dwell | 636400 | 11155.289 | 10862.591 | 21823.487 | 24756.223 | 25886.719 | 28344.319 |
| receive_call | 786486 | 41.021 | 39.263 | 60.543 | 104.127 | 219.007 | 658.431 |
| ctx_load | 636400 | 64.266 | 69.631 | 98.431 | 134.527 | 195.071 | 472.063 |
| process | 636400 | 10.539 | 0.002 | 0.003 | 0.016 | 2611.199 | 3006.463 |
| ctx_commit | 636400 | 65.852 | 69.759 | 100.415 | 150.655 | 319.743 | 5693.439 |
| service | 636400 | 142.321 | 144.895 | 195.583 | 288.255 | 2781.183 | 8163.327 |
| e2e_large | 195 | 20910.29 | 22069.247 | 27033.599 | 29622.271 | 31277.055 | 31277.055 |
| ready_wait | 636400 | 774.529 | 69.887 | 2308.095 | 6578.175 | 12230.655 | 23805.951 |
| blocked | 636400 | 708.878 | 68.671 | 2121.727 | 5894.143 | 10764.287 | 22216.703 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11043.696 | 12545.6 |
| applied_total | 398889.184 | 660139 |
| bench_cpu_cores | 2.969 | 3.532 |
| bench_cpu_pct | 12.371 | 14.72 |
| bench_gc_gen0 | 1.898 | 3 |
| bench_gc_gen1 | 0.776 | 2 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 1603.482 | 1974.3 |
| bench_mem_mb | 1963.159 | 2235.3 |
| bench_threadpool_queue | 55.429 | 578 |
| bench_threadpool_threads | 38.082 | 44 |
| broker_backlog | 80666.429 | 178935 |
| broker_oldest_age_s | 12.51 | 20 |
| broker_visible | 67778.694 | 134902 |
| ctr_eb_pgbouncer_2_cpu_pct | 40.702 | 44.87 |
| ctr_eb_pgbouncer_2_mem_mb | 18.063 | 25.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 39.515 | 47.95 |
| ctr_eb_pgbouncer_3_mem_mb | 13.294 | 14.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 41.681 | 49.89 |
| ctr_eb_pgbouncer_4_mem_mb | 13.824 | 17.9 |
| ctr_eb_pgbouncer_cpu_pct | 40.607 | 47.41 |
| ctr_eb_pgbouncer_mem_mb | 16.667 | 18.2 |
| ctr_eb_postgres_cpu_pct | 1683.526 | 1733.78 |
| ctr_eb_postgres_mem_mb | 8742.243 | 9422.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 90.524 | 93.83 |
| in_flight | 99788.265 | 207416 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.959 | 100 |
| pg_conn_active | 124.816 | 159 |
| pg_conn_idle | 69.49 | 127 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 9.714 | 50 |
| pg_conn_total | 194.306 | 196 |
| pg_db_size_mb | 2157.267 | 3676.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36519.68 | 43238.3 |
| pg_tup_del_per_s | 31002.68 | 33322.5 |
| pg_tup_fetch_per_s | 586388.035 | 719793.2 |
| pg_tup_ins_per_s | 47456.665 | 62220.5 |
| pg_tup_upd_per_s | 30536.327 | 35433.6 |
| pg_wal_mb_per_s | 96.431 | 140.45 |
| pgb_cl_active | 132.51 | 246 |
| pgb_cl_waiting | 319.918 | 356 |
| pgb_maxwait_ms | 37.217 | 52.7 |
| pgb_sv_active | 48 | 48 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14873.378 | 22348.4 |
| produced_total | 498677.449 | 867555 |
| publish_mb_per_s | 42.903 | 71.2 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 129,651 records are Completed |
| record_counts | pass | 129,651 records found, 129,651 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 296 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 920,356 events applied by workers, 920,356 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 296,066 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 5894 ms, max 22207 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13161 seq 24: ready 109 ms before start, 109 ms of it with an idle slot in group 417
- [cross_source_blocking] source 2345 seq 7: ready 117 ms before start, 117 ms of it with an idle slot in group 68
- [cross_source_blocking] source 4777 seq 27: ready 132 ms before start, 132 ms of it with an idle slot in group 165
- [cross_source_blocking] source 9 seq 17: ready 206 ms before start, 125 ms of it with an idle slot in group 0
- [cross_source_blocking] source 8425 seq 16: ready 184 ms before start, 162 ms of it with an idle slot in group 285
- [cross_source_blocking] source 1385 seq 5: ready 196 ms before start, 169 ms of it with an idle slot in group 44
- [cross_source_blocking] source 8393 seq 12: ready 205 ms before start, 183 ms of it with an idle slot in group 285
- [cross_source_blocking] source 2025 seq 8: ready 229 ms before start, 213 ms of it with an idle slot in group 61
- [cross_source_blocking] source 2409 seq 16: ready 272 ms before start, 197 ms of it with an idle slot in group 70
- [cross_source_blocking] source 9545 seq 26: ready 272 ms before start, 272 ms of it with an idle slot in group 332
- [cross_source_blocking] source 6697 seq 10: ready 266 ms before start, 232 ms of it with an idle slot in group 228
- [cross_source_blocking] source 6313 seq 19: ready 117 ms before start, 117 ms of it with an idle slot in group 215
- [cross_source_blocking] source 8745 seq 34: ready 147 ms before start, 147 ms of it with an idle slot in group 304
- [cross_source_blocking] source 11465 seq 11: ready 343 ms before start, 342 ms of it with an idle slot in group 372
- [cross_source_blocking] source 8553 seq 18: ready 322 ms before start, 322 ms of it with an idle slot in group 293
- [cross_source_blocking] source 1985 seq 21: ready 459 ms before start, 417 ms of it with an idle slot in group 57
- [cross_source_blocking] source 4257 seq 5: ready 492 ms before start, 492 ms of it with an idle slot in group 143
- [cross_source_blocking] source 8609 seq 8: ready 508 ms before start, 436 ms of it with an idle slot in group 297
- [cross_source_blocking] source 2017 seq 17: ready 492 ms before start, 492 ms of it with an idle slot in group 60
- [cross_source_blocking] source 2977 seq 12: ready 503 ms before start, 503 ms of it with an idle slot in group 97

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 1870 | 1870 | 0 | 41156.607 | 46923.775 | 48234.495 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 4004 | 4004 | 0 | 42205.183 | 46661.631 | 48496.639 |
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 1977 | 1977 | 0 | 40894.463 | 46661.631 | 47710.207 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 3528 | 3528 | 0 | 40370.175 | 45875.199 | 47185.919 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 2029 | 2029 | 0 | 40894.463 | 45875.199 | 46661.631 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 5653 | 5653 | 0 | 41156.607 | 45875.199 | 47710.207 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 18180 | 18180 | 0 | 40894.463 | 45875.199 | 47972.351 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 11114 | 11114 | 0 | 40894.463 | 45875.199 | 47185.919 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 9372 | 9372 | 0 | 40632.319 | 45875.199 | 46661.631 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 1955 | 1955 | 0 | 40894.463 | 45613.055 | 46923.775 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 1825 | 1825 | 0 | 39583.743 | 45613.055 | 48234.495 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 7316 | 7316 | 0 | 39845.887 | 45613.055 | 47710.207 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 3576 | 3576 | 0 | 40370.175 | 45613.055 | 47448.063 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 9205 | 9205 | 0 | 40632.319 | 45613.055 | 48496.639 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 9291 | 9291 | 0 | 39845.887 | 45613.055 | 46923.775 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 5605 | 5605 | 0 | 40108.031 | 45613.055 | 46661.631 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 1885 | 1885 | 0 | 38535.167 | 45350.911 | 46137.343 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 3695 | 3695 | 0 | 40108.031 | 45350.911 | 47185.919 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 5713 | 5713 | 0 | 39583.743 | 45088.767 | 46399.487 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 3788 | 3788 | 0 | 39059.455 | 45088.767 | 46923.775 |
| t-032 | 52 | 1 | 53.987 | 45.646 | 1041 | 0 | 1829 | 1829 | 0 | 38273.023 | 44826.623 | 47710.207 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 5444 | 5444 | 0 | 38535.167 | 44826.623 | 45875.199 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 15421 | 15421 | 0 | 39059.455 | 44564.479 | 47448.063 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 13706 | 13706 | 0 | 39059.455 | 44564.479 | 46661.631 |
| t-172 | 20 | 1 | 137.561 | 48.341 | 2589 | 0 | 1825 | 1825 | 0 | 39321.599 | 44564.479 | 46137.343 |
