# Q0-pgqueue-12k — PASS ✅

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:6432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.118s, closing 0.136s, drain 3.1s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814601 | 814601 | 12853.014 | 12853.143 | 36917 | 0 | 0 | 0 | 431 | 4254 | 0 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642886 | 29.165 | 4.207 | 8.423 | 1170.431 | 2725.887 | 5722.111 |
| e2e_from_send | 642886 | 27.878 | 3.321 | 6.891 | 1169.407 | 2723.839 | 5693.439 |
| producer_lag | 642886 | 1.287 | 0.454 | 2.341 | 17.983 | 75.455 | 117.311 |
| publish_call | 585876 | 0.67 | 0.456 | 1.126 | 2.683 | 21.823 | 118.527 |
| queue_wait | 642886 | 15.223 | 1.839 | 3.869 | 307.455 | 2215.935 | 4034.559 |
| broker_dwell | 642886 | 14.168 | 0.801 | 2.743 | 306.431 | 2215.935 | 4032.511 |
| receive_call | 729153 | 45.906 | 27.887 | 99.071 | 324.095 | 468.991 | 500.479 |
| ctx_load | 642886 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 1.299 |
| process | 642886 | 10.898 | 0.001 | 0.002 | 0.007 | 2623.487 | 3000.319 |
| ctx_commit | 642886 | 0.996 | 0.631 | 2.079 | 4.555 | 8.511 | 61.631 |
| service | 642886 | 12.656 | 1.303 | 3.325 | 9.303 | 2625.535 | 3026.943 |
| e2e_large | 331 | 2267.818 | 2291.711 | 3051.519 | 4562.943 | 5722.111 | 5722.111 |
| ready_wait | 642886 | 2.344 | 1.845 | 3.959 | 9.959 | 20.767 | 1727.487 |
| blocked | 642886 | 2.316 | 1.845 | 3.959 | 9.943 | 20.639 | 54.335 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.143 | 13146.1 |
| applied_total | 468574.224 | 777361 |
| bench_cpu_cores | 4.444 | 4.663 |
| bench_cpu_pct | 18.516 | 19.43 |
| bench_gc_gen0 | 5.918 | 8 |
| bench_gc_gen1 | 1.143 | 4 |
| bench_gc_gen2 | 0.429 | 2 |
| bench_gc_heap_mb | 571.471 | 681 |
| bench_mem_mb | 905.731 | 988.4 |
| bench_threadpool_queue | 2.612 | 17 |
| bench_threadpool_threads | 29.714 | 31 |
| broker_backlog | 349.163 | 512 |
| ctr_eb_pgbouncer_2_cpu_pct | 27.567 | 29.78 |
| ctr_eb_pgbouncer_2_mem_mb | 11.59 | 12.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 26.17 | 28.22 |
| ctr_eb_pgbouncer_3_mem_mb | 11.751 | 12.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 19.946 | 21.65 |
| ctr_eb_pgbouncer_4_mem_mb | 9.878 | 10.3 |
| ctr_eb_pgbouncer_cpu_pct | 21.569 | 22.77 |
| ctr_eb_pgbouncer_mem_mb | 12.351 | 13 |
| ctr_eb_postgres_cpu_pct | 670.88 | 749.56 |
| ctr_eb_postgres_mem_mb | 6248.424 | 6344.8 |
| dispatch_empty_reads_per_s | 12262.606 | 12525.8 |
| dispatch_fetch_calls_per_s | 3529.273 | 3917.2 |
| dispatch_group_reads_per_s | 23879.329 | 24326.1 |
| dispatch_in_flight | 151.653 | 200 |
| dispatch_notifies_per_s | 12813.488 | 12972.2 |
| dispatch_preloaded_per_s | 12856.767 | 13013 |
| dispatch_rows_per_s | 12856.767 | 13013 |
| dispatch_waiting_receives | 599.143 | 601 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 45.368 | 56.59 |
| in_flight | 337.592 | 477 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 1.633 | 7 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 5.469 | 19 |
| pg_conn_idle | 63.735 | 69 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.857 | 15 |
| pg_conn_total | 69.204 | 70 |
| pg_db_size_mb | 2156.655 | 2687.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22415.069 | 44750.3 |
| pg_tup_del_per_s | 36212.386 | 46939.2 |
| pg_tup_fetch_per_s | 217200.555 | 244590 |
| pg_tup_ins_per_s | 46683.567 | 63057 |
| pg_tup_upd_per_s | 21115.48 | 22531.8 |
| pg_wal_mb_per_s | 89.835 | 119.15 |
| pgb_cl_active | 11.918 | 14 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 2.694 | 7 |
| pgb_sv_idle | 9.224 | 13 |
| pgb_sv_used | 4.592 | 8 |
| produced_per_s | 12853.014 | 13132.1 |
| produced_total | 468911.816 | 777702 |
| publish_mb_per_s | 44.331 | 71.11 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,589 records are Completed |
| record_counts | pass | 117,589 records found, 117,589 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,601 events applied by workers, 814,601 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | pass | none waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 10 ms, max 54 ms) |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 1622.015 | 4390.911 | 5734.399 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 1851.391 | 4292.607 | 4947.967 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 1998.847 | 3948.543 | 3948.543 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 1400.831 | 3047.423 | 3522.559 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 4.415 | 2850.815 | 3850.239 |
| t-418 | 6 | 1 | 121.075 | 12.228 | 201 | 0 | 759 | 759 | 0 | 4.319 | 2572.287 | 2916.351 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 4.383 | 2555.903 | 3735.551 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 4.255 | 2490.367 | 2703.359 |
| t-447 | 17 | 1 | 45.301 | 12.225 | 558 | 0 | 749 | 749 | 0 | 4.383 | 2490.367 | 2899.967 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 4.223 | 2392.063 | 2785.279 |
| t-349 | 14 | 1 | 44.443 | 10.033 | 379 | 0 | 603 | 603 | 0 | 4.287 | 2342.911 | 2850.815 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1790 | 1790 | 0 | 4.479 | 2326.527 | 2834.431 |
| t-299 | 21 | 1 | 42.355 | 14.484 | 646 | 0 | 907 | 907 | 0 | 4.351 | 2228.223 | 2965.503 |
| t-320 | 14 | 1 | 86.153 | 19.097 | 574 | 0 | 1206 | 1206 | 0 | 4.447 | 2162.687 | 2899.967 |
| t-338 | 6 | 1 | 21.73 | 2.22 | 156 | 0 | 140 | 140 | 0 | 3.903 | 2162.687 | 2539.519 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 3.775 | 2146.303 | 2850.815 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 4.191 | 2146.303 | 3948.543 |
| t-381 | 11 | 1 | 47.024 | 8.839 | 134 | 0 | 603 | 603 | 0 | 4.223 | 2146.303 | 2785.279 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 3.791 | 2113.535 | 2736.127 |
| t-420 | 13 | 1 | 49.618 | 10.967 | 270 | 0 | 712 | 712 | 0 | 4.223 | 2113.535 | 2916.351 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 4.063 | 2064.383 | 2424.831 |
| t-481 | 12 | 1 | 121.412 | 22.891 | 1993 | 0 | 1428 | 1428 | 0 | 4.223 | 2031.615 | 2801.663 |
| t-054 | 17 | 1 | 35.84 | 10.264 | 161 | 0 | 682 | 682 | 0 | 3.983 | 2007.039 | 2949.119 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 2110 | 2110 | 0 | 4.255 | 1998.847 | 2867.199 |
| t-243 | 16 | 1 | 59.376 | 15.905 | 3143 | 0 | 984 | 984 | 0 | 4.415 | 1990.655 | 2981.887 |
