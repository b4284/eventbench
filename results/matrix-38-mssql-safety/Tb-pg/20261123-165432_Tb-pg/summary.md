# Tb-pg — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.116s, closing 1.621s, drain 1.87s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000496 | 1000496 | 15943.231 | 15943.906 | 24743.5 | 0 | 0 | 0 | 458 | 5162 | 2855 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797129 | 75.086 | 34.175 | 102.399 | 1389.567 | 2797.567 | 5005.311 |
| e2e_from_send | 797129 | 71.631 | 31.967 | 94.463 | 1385.471 | 2791.423 | 5005.311 |
| producer_lag | 797129 | 3.455 | 1.402 | 6.603 | 38.591 | 128.959 | 305.407 |
| publish_call | 1000496 | 28.413 | 16.831 | 60.735 | 166.399 | 268.799 | 386.815 |
| queue_wait | 797129 | 41.944 | 16.735 | 53.151 | 753.151 | 2363.391 | 4702.207 |
| broker_dwell | 797129 | 26.49 | 3.655 | 24.655 | 737.791 | 2349.055 | 4698.111 |
| receive_call | 907723 | 38.417 | 19.711 | 91.455 | 299.775 | 463.103 | 618.495 |
| ctx_load | 797129 | 5.067 | 3.635 | 9.815 | 25.615 | 49.823 | 243.711 |
| process | 797129 | 10.741 | 0.002 | 0.004 | 0.048 | 2621.439 | 3061.759 |
| ctx_commit | 797129 | 13.799 | 9.495 | 28.303 | 66.431 | 110.847 | 266.495 |
| ack | 797127 | 0.01 | 0.005 | 0.011 | 0.059 | 0.702 | 145.535 |
| service | 797129 | 29.688 | 13.895 | 38.143 | 95.999 | 2639.871 | 3149.823 |
| e2e_large | 356 | 2401.25 | 2371.583 | 3194.879 | 4464.639 | 5005.311 | 5005.311 |
| ready_wait | 797129 | 9.783 | 3.305 | 24.367 | 86.015 | 161.151 | 1786.879 |
| blocked | 797129 | 9.2 | 3.297 | 23.279 | 77.823 | 131.583 | 660.991 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15943.906 | 17759.6 |
| applied_total | 570313.812 | 945348 |
| bench_cpu_cores | 6.634 | 7.76 |
| bench_cpu_pct | 27.643 | 32.33 |
| bench_gc_gen0 | 2.271 | 4 |
| bench_gc_gen1 | 0.688 | 2 |
| bench_gc_gen2 | 0.562 | 1 |
| bench_gc_heap_mb | 1454.71 | 1829.8 |
| bench_mem_mb | 2211.975 | 2550.1 |
| bench_threadpool_queue | 81.458 | 513 |
| bench_threadpool_threads | 42.542 | 44 |
| broker_backlog | 2234.188 | 4321 |
| ctr_eb_mssql_cpu_pct | 2.886 | 3.81 |
| ctr_eb_mssql_mem_mb | 4448.781 | 4449.8 |
| ctr_eb_mysql_cpu_pct | 0.835 | 0.96 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.158 | 20.18 |
| ctr_eb_pgbouncer_2_mem_mb | 15.467 | 17.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.618 | 20.79 |
| ctr_eb_pgbouncer_3_mem_mb | 15.45 | 16.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.117 | 21.39 |
| ctr_eb_pgbouncer_4_mem_mb | 15.531 | 16 |
| ctr_eb_pgbouncer_cpu_pct | 18.74 | 19.68 |
| ctr_eb_pgbouncer_mem_mb | 15.158 | 16.1 |
| ctr_eb_postgres_cpu_pct | 658.525 | 760.99 |
| ctr_eb_postgres_mem_mb | 4587.431 | 4956.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 69.866 | 77.23 |
| in_flight | 774.062 | 2130 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.561 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1117607.792 | 1913609 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 192410.396 | 324438 |
| octopus_mqtt_sent_total | 380278.729 | 661427 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.292 | 16 |
| pg_active_loads | 3.375 | 28 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 19.167 | 42 |
| pg_conn_idle | 232.938 | 248 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.062 | 1 |
| pg_conn_total | 252.104 | 253 |
| pg_db_size_mb | 3499.806 | 3910.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19023.192 | 20600.6 |
| pg_tup_del_per_s | 29609.069 | 37791 |
| pg_tup_fetch_per_s | 209792.196 | 250236.6 |
| pg_tup_ins_per_s | 41256.675 | 53980.7 |
| pg_tup_upd_per_s | 42149.49 | 49536.8 |
| pg_wal_mb_per_s | 105.641 | 129.82 |
| pgb_cl_active | 246.458 | 262 |
| pgb_cl_waiting | 0.75 | 12 |
| pgb_maxwait_ms | 0.066 | 1.05 |
| pgb_sv_active | 8.646 | 48 |
| pgb_sv_idle | 39.354 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15943.231 | 16676.1 |
| produced_total | 571087.875 | 946064 |
| publish_mb_per_s | 51.947 | 74.31 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,871 records are Completed |
| record_counts | pass | 138,871 records found, 138,871 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,496 events applied by workers, 1,000,496 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 2,855 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 78 ms, max 661 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11094 seq 5: ready 253 ms before start, 115 ms of it with an idle slot in group 362
- [cross_source_blocking] source 11092 seq 10: ready 299 ms before start, 140 ms of it with an idle slot in group 362
- [cross_source_blocking] source 11095 seq 10: ready 316 ms before start, 160 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4857 seq 10: ready 376 ms before start, 189 ms of it with an idle slot in group 168
- [cross_source_blocking] source 11074 seq 9: ready 303 ms before start, 150 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4808 seq 9: ready 360 ms before start, 179 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4837 seq 8: ready 310 ms before start, 146 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4865 seq 8: ready 318 ms before start, 147 ms of it with an idle slot in group 168
- [cross_source_blocking] source 11109 seq 11: ready 319 ms before start, 157 ms of it with an idle slot in group 362
- [cross_source_blocking] source 11073 seq 7: ready 312 ms before start, 152 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4868 seq 5: ready 305 ms before start, 143 ms of it with an idle slot in group 168
- [cross_source_blocking] source 11086 seq 6: ready 324 ms before start, 161 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4810 seq 5: ready 294 ms before start, 147 ms of it with an idle slot in group 168
- [cross_source_blocking] source 11098 seq 12: ready 315 ms before start, 156 ms of it with an idle slot in group 362
- [cross_source_blocking] source 11084 seq 12: ready 295 ms before start, 147 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4821 seq 6: ready 310 ms before start, 161 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4823 seq 8: ready 273 ms before start, 136 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4861 seq 6: ready 275 ms before start, 129 ms of it with an idle slot in group 168
- [cross_source_blocking] source 11083 seq 12: ready 281 ms before start, 137 ms of it with an idle slot in group 362
- [cross_source_blocking] source 3493 seq 17: ready 131 ms before start, 131 ms of it with an idle slot in group 120

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1966.079 | 4685.823 | 5013.503 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2023.423 | 4325.375 | 4816.895 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1916.927 | 3670.015 | 4325.375 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1613.823 | 3162.111 | 4227.071 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 35.327 | 2637.823 | 4259.839 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 35.839 | 2408.447 | 4587.519 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 36.863 | 2408.447 | 2932.735 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 36.351 | 2375.679 | 2981.887 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 36.863 | 2342.911 | 3751.935 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 35.327 | 2342.911 | 3031.039 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 36.351 | 2326.527 | 2932.735 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 35.071 | 2310.143 | 3211.263 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 34.559 | 2293.759 | 2932.735 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 31.359 | 2277.375 | 2883.583 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 34.047 | 2277.375 | 3260.415 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 35.327 | 2211.839 | 2867.199 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 33.535 | 2195.455 | 2916.351 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 34.815 | 2162.687 | 2686.975 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 34.559 | 2162.687 | 2998.271 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 32.255 | 2146.303 | 2752.511 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 32.767 | 2129.919 | 3096.575 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 33.791 | 2129.919 | 2785.279 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 35.839 | 2129.919 | 2932.735 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1027 | 1027 | 0 | 35.071 | 2113.535 | 2949.119 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 33.023 | 2113.535 | 2850.815 |
