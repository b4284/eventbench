# P1-epochs-kill-1-of-3-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.11s, closing 1.303s, drain 1.908s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000492 | 1000492 | 16044.727 | 16177.177 | 23987.1 | 877 | 0 | 0 | 458 | 5162 | 84634 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797128 | 332.273 | 47.903 | 1052.671 | 3104.767 | 4939.775 | 8732.671 |
| e2e_from_send | 797128 | 182.114 | 43.999 | 340.479 | 2650.111 | 4325.375 | 8593.407 |
| producer_lag | 797128 | 150.159 | 2.057 | 419.839 | 2607.103 | 3964.927 | 5074.943 |
| publish_call | 1000492 | 46.292 | 22.623 | 107.903 | 277.247 | 504.063 | 5095.423 |
| queue_wait | 797128 | 150.3 | 26.351 | 282.111 | 2523.135 | 4163.583 | 8544.255 |
| broker_dwell | 797128 | 119.851 | 8.447 | 224.127 | 2473.983 | 3813.375 | 8454.143 |
| receive_call | 883432 | 37.708 | 17.823 | 89.087 | 316.927 | 466.175 | 536.063 |
| ctx_load | 797128 | 7.218 | 4.999 | 15.103 | 35.935 | 67.967 | 109.695 |
| process | 797128 | 10.736 | 0.002 | 0.004 | 0.045 | 2617.343 | 3016.703 |
| ctx_commit | 797128 | 13.809 | 8.119 | 33.119 | 75.327 | 109.247 | 415.743 |
| ack | 796314 | 0.007 | 0.003 | 0.005 | 0.03 | 0.413 | 76.479 |
| service | 797128 | 31.815 | 14.407 | 47.647 | 106.687 | 2635.775 | 3149.823 |
| e2e_large | 356 | 2725.943 | 2635.775 | 3919.871 | 5419.007 | 6246.399 | 6246.399 |
| ready_wait | 797128 | 48.484 | 5.911 | 122.623 | 485.887 | 2969.599 | 5746.687 |
| blocked | 797128 | 43.797 | 5.891 | 107.135 | 397.055 | 2945.023 | 5079.039 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16177.177 | 21360.5 |
| applied_total | 566155.208 | 944087 |
| bench_cpu_cores | 4.894 | 5.823 |
| bench_cpu_pct | 20.389 | 24.26 |
| bench_gc_gen0 | 3.25 | 5 |
| bench_gc_gen1 | 2.583 | 4 |
| bench_gc_gen2 | 0.604 | 2 |
| bench_gc_heap_mb | 1023.598 | 1330.1 |
| bench_mem_mb | 1623.979 | 1793.2 |
| bench_threadpool_queue | 41.792 | 408 |
| bench_threadpool_threads | 41.229 | 44 |
| broker_backlog | 4981.896 | 17596 |
| ctr_eb_mosquitto_cpu_pct | 63.206 | 71.74 |
| ctr_eb_mosquitto_mem_mb | 7.875 | 14 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.675 | 22.01 |
| ctr_eb_pgbouncer_2_mem_mb | 15.623 | 16.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.656 | 21.9 |
| ctr_eb_pgbouncer_3_mem_mb | 15.796 | 16.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.261 | 21.99 |
| ctr_eb_pgbouncer_4_mem_mb | 15.792 | 16.4 |
| ctr_eb_pgbouncer_cpu_pct | 19.875 | 21.98 |
| ctr_eb_pgbouncer_mem_mb | 16.029 | 17.5 |
| ctr_eb_postgres_cpu_pct | 827.719 | 1014.55 |
| ctr_eb_postgres_mem_mb | 5496.317 | 5729.7 |
| duplicates_per_s | 18.24 | 868.5 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.577 | 82.5 |
| in_flight | 2386.792 | 11268 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.668 | 1.85 |
| octopus_dispatchers_alive | 2.396 | 3 |
| octopus_items_sent_total | 1111211.104 | 1884500 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 172873.792 | 269543 |
| octopus_mqtt_sent_total | 329715.5 | 580235 |
| octopus_publish_retries_total | 174.896 | 365 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.625 | 22 |
| pg_active_loads | 7.833 | 73 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 21.208 | 53 |
| pg_conn_idle | 225.021 | 244 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.646 | 25 |
| pg_conn_total | 246.229 | 259 |
| pg_db_size_mb | 3515.329 | 4030.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21153.625 | 23336.7 |
| pg_tup_del_per_s | 44349.471 | 56785.4 |
| pg_tup_fetch_per_s | 516939.375 | 777825.1 |
| pg_tup_ins_per_s | 55805.629 | 68120.6 |
| pg_tup_upd_per_s | 42276.842 | 50107.6 |
| pg_wal_mb_per_s | 115.818 | 268.34 |
| pgb_cl_active | 250 | 255 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.667 | 28 |
| pgb_sv_idle | 41.333 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16044.727 | 21260 |
| produced_total | 568542 | 945939 |
| publish_mb_per_s | 52.325 | 98.04 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,870 records are Completed |
| record_counts | pass | 138,870 records found, 138,870 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,492 events applied by workers, 1,000,492 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 84,634 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 397 ms, max 5076 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10332 seq 16: ready 150 ms before start, 145 ms of it with an idle slot in group 347
- [cross_source_blocking] source 14943 seq 27: ready 141 ms before start, 116 ms of it with an idle slot in group 468
- [cross_source_blocking] source 8074 seq 24: ready 158 ms before start, 158 ms of it with an idle slot in group 271
- [cross_source_blocking] source 11132 seq 15: ready 191 ms before start, 170 ms of it with an idle slot in group 363
- [cross_source_blocking] source 2478 seq 14: ready 158 ms before start, 108 ms of it with an idle slot in group 74
- [cross_source_blocking] source 10937 seq 19: ready 164 ms before start, 164 ms of it with an idle slot in group 357
- [cross_source_blocking] source 5049 seq 16: ready 190 ms before start, 171 ms of it with an idle slot in group 175
- [cross_source_blocking] source 4458 seq 7: ready 157 ms before start, 157 ms of it with an idle slot in group 153
- [cross_source_blocking] source 13177 seq 29: ready 163 ms before start, 163 ms of it with an idle slot in group 418
- [cross_source_blocking] source 2845 seq 6: ready 162 ms before start, 162 ms of it with an idle slot in group 89
- [cross_source_blocking] source 11122 seq 18: ready 156 ms before start, 135 ms of it with an idle slot in group 363
- [cross_source_blocking] source 2773 seq 7: ready 165 ms before start, 165 ms of it with an idle slot in group 85
- [cross_source_blocking] source 3358 seq 9: ready 156 ms before start, 155 ms of it with an idle slot in group 116
- [cross_source_blocking] source 4570 seq 11: ready 161 ms before start, 133 ms of it with an idle slot in group 160
- [cross_source_blocking] source 11037 seq 10: ready 162 ms before start, 137 ms of it with an idle slot in group 361
- [cross_source_blocking] source 12182 seq 9: ready 160 ms before start, 160 ms of it with an idle slot in group 388
- [cross_source_blocking] source 7286 seq 8: ready 165 ms before start, 165 ms of it with an idle slot in group 250
- [cross_source_blocking] source 3742 seq 7: ready 164 ms before start, 164 ms of it with an idle slot in group 126
- [cross_source_blocking] source 221 seq 14: ready 199 ms before start, 199 ms of it with an idle slot in group 5
- [cross_source_blocking] source 9470 seq 15: ready 162 ms before start, 162 ms of it with an idle slot in group 327

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2293.759 | 6062.079 | 6258.687 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 15 | 65.023 | 5767.167 | 7766.015 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9221 | 9221 | 0 | 49.407 | 5570.559 | 8781.823 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2228.223 | 4947.967 | 5963.775 |
| t-304 | 19 | 1 | 129.878 | 40.323 | 238 | 0 | 2459 | 2459 | 15 | 70.655 | 4849.663 | 5734.399 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2849 | 2849 | 0 | 48.127 | 4849.663 | 7405.567 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 50.175 | 4784.127 | 8028.159 |
| t-493 | 8 | 1 | 106.163 | 15.177 | 1419 | 0 | 982 | 982 | 0 | 44.799 | 4751.359 | 5570.559 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 51.455 | 4653.055 | 7536.639 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2195.455 | 4456.447 | 5013.503 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1998.847 | 4456.447 | 5439.487 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 11 | 63.743 | 4390.911 | 7274.495 |
| t-033 | 6 | 1 | 87.319 | 9.519 | 859 | 0 | 581 | 581 | 0 | 46.591 | 4325.375 | 5111.807 |
| t-298 | 6 | 1 | 79.466 | 8.739 | 3700 | 0 | 530 | 530 | 0 | 45.311 | 4292.607 | 5046.271 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 4 | 56.575 | 4030.463 | 5210.111 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 615 | 615 | 1 | 64.767 | 3981.311 | 5111.807 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14678 | 14678 | 0 | 47.103 | 3915.775 | 5832.703 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 47.359 | 3883.007 | 5439.487 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8301 | 8301 | 26 | 65.279 | 3833.855 | 6619.135 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 1 | 48.127 | 3817.471 | 5111.807 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 45.055 | 3817.471 | 6094.847 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2867 | 2867 | 13 | 51.455 | 3784.703 | 4456.447 |
| t-212 | 29 | 1 | 62.738 | 30.352 | 296 | 0 | 1930 | 1930 | 10 | 54.271 | 3751.935 | 5111.807 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5724 | 5724 | 0 | 51.455 | 3751.935 | 5701.631 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 46.847 | 3637.247 | 5505.023 |
