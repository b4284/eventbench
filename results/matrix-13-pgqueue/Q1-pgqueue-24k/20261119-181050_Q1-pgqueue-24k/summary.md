# Q1-pgqueue-24k — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:6432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.103s, closing 0.149s, drain 4.504s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487611 | 1487611 | 24043.114 | 24042.469 | 40292.4 | 0 | 0 | 0 | 425 | 7514 | 106 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1202187 | 52.556 | 14.207 | 31.807 | 1565.695 | 2783.231 | 6266.879 |
| e2e_from_send | 1202187 | 50.941 | 13.031 | 29.503 | 1563.647 | 2781.183 | 6262.783 |
| producer_lag | 1202187 | 1.615 | 0.624 | 2.827 | 22.015 | 85.567 | 126.591 |
| publish_call | 854476 | 1.21 | 0.85 | 2.303 | 6.015 | 13.767 | 198.911 |
| queue_wait | 1202187 | 34.316 | 6.627 | 20.303 | 1160.191 | 2471.935 | 5926.911 |
| broker_dwell | 1202187 | 33.22 | 5.547 | 19.167 | 1159.167 | 2471.935 | 5926.911 |
| receive_call | 1096858 | 42.059 | 32.735 | 75.263 | 230.015 | 428.543 | 503.039 |
| ctx_load | 1202187 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 3.493 |
| process | 1202187 | 10.387 | 0.001 | 0.002 | 0.004 | 2613.247 | 3004.415 |
| ctx_commit | 1202187 | 3.62 | 3.477 | 6.399 | 11.151 | 18.191 | 55.103 |
| service | 1202187 | 16.625 | 5.883 | 10.511 | 21.983 | 2617.343 | 3035.135 |
| e2e_large | 324 | 2284.119 | 2226.175 | 3074.047 | 4976.639 | 5550.079 | 5550.079 |
| ready_wait | 1202187 | 9.796 | 7.295 | 20.303 | 33.567 | 52.415 | 1545.215 |
| blocked | 1202187 | 9.753 | 7.295 | 20.287 | 33.535 | 51.519 | 326.655 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24042.469 | 24509 |
| applied_total | 869185.327 | 1446084 |
| bench_cpu_cores | 4.463 | 4.95 |
| bench_cpu_pct | 18.594 | 20.63 |
| bench_gc_gen0 | 4.51 | 6 |
| bench_gc_gen1 | 2.163 | 4 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 696.708 | 877.4 |
| bench_mem_mb | 1011.737 | 1140.6 |
| bench_threadpool_queue | 7.122 | 42 |
| bench_threadpool_threads | 37.265 | 41 |
| broker_backlog | 1184.449 | 1589 |
| ctr_eb_pgbouncer_2_cpu_pct | 25.561 | 26.24 |
| ctr_eb_pgbouncer_2_mem_mb | 11.878 | 12.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 14.065 | 14.84 |
| ctr_eb_pgbouncer_3_mem_mb | 11.58 | 12.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 30.226 | 32.07 |
| ctr_eb_pgbouncer_4_mem_mb | 10.276 | 11.7 |
| ctr_eb_pgbouncer_cpu_pct | 23.97 | 25.38 |
| ctr_eb_pgbouncer_mem_mb | 12.42 | 13 |
| ctr_eb_postgres_cpu_pct | 870.586 | 921.97 |
| ctr_eb_postgres_mem_mb | 4513.461 | 5030.6 |
| dispatch_empty_reads_per_s | 10893.39 | 11684.1 |
| dispatch_fetch_calls_per_s | 795.822 | 900 |
| dispatch_group_reads_per_s | 25884.343 | 27183 |
| dispatch_in_flight | 341.755 | 649 |
| dispatch_notifies_per_s | 23878.655 | 24036.5 |
| dispatch_preloaded_per_s | 24044.996 | 24209.9 |
| dispatch_rows_per_s | 24043.839 | 24209.9 |
| dispatch_waiting_receives | 771.265 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 58.06 | 61.15 |
| in_flight | 1176.347 | 1630 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 1.939 | 8 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.306 | 34 |
| pg_conn_idle | 177.837 | 191 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 7.571 | 27 |
| pg_conn_total | 194.143 | 195 |
| pg_db_size_mb | 2425.714 | 2843.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20595.404 | 51816 |
| pg_tup_del_per_s | 56634.078 | 73137.4 |
| pg_tup_fetch_per_s | 414299.729 | 499784 |
| pg_tup_ins_per_s | 67954.355 | 88313.3 |
| pg_tup_upd_per_s | 39495.5 | 45580.8 |
| pg_wal_mb_per_s | 128.321 | 205.45 |
| pgb_cl_active | 11.633 | 16 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.776 | 9 |
| pgb_sv_idle | 6.857 | 11 |
| pgb_sv_used | 36.367 | 42 |
| produced_per_s | 24043.114 | 24371.1 |
| produced_total | 870361.673 | 1447438 |
| publish_mb_per_s | 64.026 | 89.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,922 records are Completed |
| record_counts | pass | 193,922 records found, 193,922 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,611 events applied by workers, 1,487,611 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 106 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 34 ms, max 326 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11814 seq 12: ready 772 ms before start, 102 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11818 seq 6: ready 778 ms before start, 110 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11799 seq 7: ready 720 ms before start, 116 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11796 seq 16: ready 703 ms before start, 130 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11795 seq 14: ready 710 ms before start, 135 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11767 seq 12: ready 695 ms before start, 160 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11811 seq 19: ready 704 ms before start, 164 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11789 seq 13: ready 687 ms before start, 171 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11801 seq 13: ready 684 ms before start, 173 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11788 seq 15: ready 685 ms before start, 176 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11770 seq 9: ready 689 ms before start, 178 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11804 seq 10: ready 687 ms before start, 186 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11773 seq 16: ready 639 ms before start, 212 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11799 seq 8: ready 182 ms before start, 140 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11792 seq 15: ready 652 ms before start, 250 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11800 seq 16: ready 476 ms before start, 271 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11810 seq 10: ready 482 ms before start, 278 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11813 seq 12: ready 468 ms before start, 288 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11784 seq 12: ready 452 ms before start, 318 ms of it with an idle slot in group 382
- [cross_source_blocking] source 11806 seq 24: ready 445 ms before start, 324 ms of it with an idle slot in group 382

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 1933.311 | 4980.735 | 5570.559 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 1990.655 | 4521.983 | 4521.983 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 1712.127 | 4292.607 | 4325.375 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1523.711 | 4128.767 | 5472.255 |
| t-295 | 12 | 1 | 103.993 | 20.658 | 3396 | 0 | 1309 | 1309 | 0 | 14.911 | 2572.287 | 4423.679 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8093 | 8093 | 0 | 15.807 | 2490.367 | 4915.199 |
| t-115 | 10 | 1 | 62.309 | 9.965 | 3658 | 0 | 641 | 641 | 0 | 13.375 | 2490.367 | 2801.663 |
| t-079 | 6 | 1 | 180.755 | 18.586 | 1522 | 0 | 1136 | 1136 | 0 | 13.951 | 2473.983 | 3653.631 |
| t-288 | 6 | 1 | 108.569 | 10.982 | 214 | 0 | 675 | 675 | 0 | 13.951 | 2457.599 | 2981.887 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5769 | 5769 | 0 | 16.767 | 2408.447 | 4194.303 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 1384 | 1384 | 0 | 13.631 | 2408.447 | 2932.735 |
| t-090 | 7 | 1 | 74.668 | 8.784 | 242 | 0 | 527 | 527 | 0 | 13.695 | 2359.295 | 2998.271 |
| t-346 | 12 | 1 | 52.432 | 10.16 | 1247 | 0 | 648 | 648 | 0 | 13.951 | 2342.911 | 2916.351 |
| t-082 | 42 | 1 | 33.494 | 23.642 | 150 | 0 | 1579 | 1579 | 0 | 13.695 | 2326.527 | 2981.887 |
| t-067 | 11 | 1 | 162.679 | 29.921 | 3490 | 0 | 1763 | 1763 | 0 | 13.311 | 2228.223 | 4423.679 |
| t-302 | 12 | 1 | 172.999 | 37 | 1189 | 0 | 2259 | 2259 | 0 | 14.527 | 2211.839 | 2932.735 |
| t-355 | 10 | 1 | 56.521 | 9.909 | 349 | 0 | 667 | 667 | 0 | 13.951 | 2195.455 | 2949.119 |
| t-427 | 16 | 2 | 207.863 | 55.174 | 1711 | 0 | 3315 | 3315 | 0 | 13.695 | 2195.455 | 2949.119 |
| t-447 | 17 | 1 | 84.714 | 22.861 | 558 | 0 | 1451 | 1451 | 0 | 13.887 | 2179.071 | 2965.503 |
| t-000 | 14 | 1 | 82.228 | 19.615 | 427 | 0 | 1238 | 1238 | 0 | 12.095 | 2129.919 | 2588.671 |
| t-012 | 6 | 1 | 115.634 | 11.982 | 556 | 0 | 779 | 779 | 0 | 12.415 | 2129.919 | 2637.823 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 7775 | 7775 | 0 | 14.527 | 2129.919 | 2998.271 |
| t-205 | 18 | 1 | 98.727 | 29.361 | 449 | 0 | 1762 | 1762 | 0 | 14.463 | 2113.535 | 3014.655 |
| t-454 | 7 | 1 | 251.84 | 29.074 | 266 | 0 | 1721 | 1721 | 0 | 13.439 | 2113.535 | 2949.119 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 2763 | 2763 | 0 | 14.271 | 2097.151 | 2719.743 |
