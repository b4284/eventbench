# U4-dc-freeze-leader-dc-60s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 123.97s, closing 1.951s, drain 17.155s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 356391 | 356391 | 6321.185 | 4951.318 | 23672.1 | 514 | 0 | 0 | 225 | 1806 | 30845 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 190145 | 90134.158 | 106495.999 | 114098.175 | 118554.623 | 120127.487 | 122159.103 |
| e2e_from_send | 190145 | 86472.763 | 102629.375 | 111017.983 | 116064.255 | 117768.191 | 119799.807 |
| producer_lag | 190145 | 3661.363 | 3616.767 | 4468.735 | 5935.103 | 98303.999 | 100335.615 |
| publish_call | 356391 | 642.588 | 140.927 | 298.239 | 716.799 | 93257.727 | 93388.799 |
| queue_wait | 190145 | 85753.839 | 102563.839 | 110952.447 | 115998.719 | 117768.191 | 119734.271 |
| broker_dwell | 190145 | 85056.386 | 102432.767 | 110821.375 | 115933.183 | 117637.119 | 119668.735 |
| receive_call | 320925 | 20.335 | 5.635 | 35.807 | 321.279 | 477.183 | 521.983 |
| ctx_load | 190145 | 4.998 | 3.125 | 10.279 | 29.839 | 89.855 | 287.487 |
| process | 190145 | 11.069 | 0.002 | 0.004 | 0.047 | 2633.727 | 3000.319 |
| ctx_commit | 190145 | 702.934 | 29.743 | 397.823 | 1098.751 | 89522.175 | 89522.175 |
| ack | 189697 | 0.007 | 0.003 | 0.005 | 0.03 | 0.416 | 25.311 |
| service | 190145 | 719.053 | 35.071 | 417.023 | 1931.263 | 89522.175 | 92274.687 |
| e2e_large | 125 | 8958.321 | 6225.919 | 7524.351 | 94961.663 | 95027.199 | 95027.199 |
| ready_wait | 190145 | 3009.639 | 33.919 | 970.751 | 99287.039 | 107413.503 | 118226.943 |
| blocked | 190145 | 217.898 | 20.735 | 152.191 | 5054.463 | 9052.159 | 90374.143 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4951.318 | 20498.4 |
| applied_total | 120671.667 | 210614 |
| bench_cpu_cores | 1.909 | 7.94 |
| bench_cpu_pct | 7.956 | 33.08 |
| bench_gc_gen0 | 1.576 | 4 |
| bench_gc_gen1 | 1.273 | 4 |
| bench_gc_gen2 | 0.364 | 2 |
| bench_gc_heap_mb | 1035.482 | 1269.9 |
| bench_mem_mb | 1521.109 | 1601.1 |
| bench_threadpool_queue | 2341.152 | 47087 |
| bench_threadpool_threads | 73.97 | 143 |
| broker_backlog | 147877.758 | 188036 |
| ctr_eb_dc1a_cpu_pct | 557.46 | 806.95 |
| ctr_eb_dc1a_mem_mb | 11459.496 | 11978.1 |
| ctr_eb_dc1b_cpu_pct | 32.881 | 43.81 |
| ctr_eb_dc1b_mem_mb | 3103.704 | 3246.3 |
| ctr_eb_dc2a_cpu_pct | 33.533 | 75.66 |
| ctr_eb_dc2a_mem_mb | 3767.812 | 3912 |
| ctr_eb_dc2b_cpu_pct | 33.984 | 67.16 |
| ctr_eb_dc2b_mem_mb | 4044.197 | 4309.7 |
| ctr_eb_etcd_dc1_cpu_pct | 1.504 | 2.02 |
| ctr_eb_etcd_dc1_mem_mb | 42.715 | 44.4 |
| ctr_eb_etcd_dc2_cpu_pct | 1.185 | 1.71 |
| ctr_eb_etcd_dc2_mem_mb | 33.842 | 38.4 |
| ctr_eb_etcd_w_cpu_pct | 1.391 | 2.19 |
| ctr_eb_etcd_w_mem_mb | 40.388 | 42.8 |
| ctr_eb_mosquitto_cpu_pct | 23.433 | 40.72 |
| ctr_eb_mosquitto_mem_mb | 4.967 | 6.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 9.4 | 9.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.025 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.025 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 1.237 | 1.88 |
| ctr_eb_postgres_mem_mb | 2188.548 | 2188.7 |
| duplicates_per_s | 11.367 | 236.3 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 45.396 | 81.48 |
| in_flight | 148648.394 | 187372 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.061 | 1.51 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 407626.333 | 582749 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 133083.212 | 176157 |
| octopus_mqtt_sent_total | 71857.515 | 102183 |
| octopus_publish_retries_total | 395.364 | 1865 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.303 | 20 |
| pg_active_loads | 0.939 | 14 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 49.455 | 65 |
| pg_conn_idle | 201.273 | 233 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 250.727 | 268 |
| pg_db_size_mb | 1249.342 | 1514 |
| pg_deadlocks | 0.121 | 2 |
| pg_replica_lag_mb | 55.78 | 132.68 |
| pg_replica_replay_lag_ms | 263.23 | 525.4 |
| pg_replicas | 2.879 | 3 |
| pg_rollbacks_per_s | 0.048 | 0.8 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 11633.03 | 16729.4 |
| pg_tup_del_per_s | 20160.197 | 33297.3 |
| pg_tup_fetch_per_s | 456764.594 | 647852.5 |
| pg_tup_ins_per_s | 36351.609 | 60363.6 |
| pg_tup_upd_per_s | 11319.515 | 17964.3 |
| pg_wal_mb_per_s | 77.504 | 108.79 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6321.185 | 15686.6 |
| produced_total | 269320.061 | 325036 |
| publish_mb_per_s | 25.261 | 69.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 62,558 records are Completed |
| record_counts | pass | 62,558 records found, 62,558 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 225 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 356,391 events applied by workers, 356,391 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 30,845 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 5054 ms, max 90329 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13989 seq 5: ready 173 ms before start, 173 ms of it with an idle slot in group 445
- [cross_source_blocking] source 7388 seq 3: ready 260 ms before start, 260 ms of it with an idle slot in group 254
- [cross_source_blocking] source 14156 seq 6: ready 117 ms before start, 117 ms of it with an idle slot in group 451
- [cross_source_blocking] source 3292 seq 3: ready 112 ms before start, 112 ms of it with an idle slot in group 110
- [cross_source_blocking] source 2940 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 94
- [cross_source_blocking] source 2933 seq 7: ready 148 ms before start, 109 ms of it with an idle slot in group 94
- [cross_source_blocking] source 2357 seq 7: ready 149 ms before start, 149 ms of it with an idle slot in group 68
- [cross_source_blocking] source 7105 seq 3: ready 149 ms before start, 149 ms of it with an idle slot in group 241
- [cross_source_blocking] source 15253 seq 7: ready 149 ms before start, 117 ms of it with an idle slot in group 476
- [cross_source_blocking] source 4449 seq 9: ready 148 ms before start, 124 ms of it with an idle slot in group 153
- [cross_source_blocking] source 15957 seq 6: ready 148 ms before start, 148 ms of it with an idle slot in group 498
- [cross_source_blocking] source 4193 seq 10: ready 148 ms before start, 148 ms of it with an idle slot in group 138
- [cross_source_blocking] source 4277 seq 10: ready 148 ms before start, 148 ms of it with an idle slot in group 145
- [cross_source_blocking] source 861 seq 11: ready 148 ms before start, 148 ms of it with an idle slot in group 24
- [cross_source_blocking] source 10717 seq 3: ready 148 ms before start, 148 ms of it with an idle slot in group 351
- [cross_source_blocking] source 14877 seq 7: ready 148 ms before start, 148 ms of it with an idle slot in group 467
- [cross_source_blocking] source 8257 seq 8: ready 207 ms before start, 207 ms of it with an idle slot in group 280
- [cross_source_blocking] source 8269 seq 3: ready 207 ms before start, 207 ms of it with an idle slot in group 281
- [cross_source_blocking] source 4252 seq 6: ready 202 ms before start, 202 ms of it with an idle slot in group 143
- [cross_source_blocking] source 4528 seq 5: ready 202 ms before start, 202 ms of it with an idle slot in group 157

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1267 | 1267 | 0 | 119013.375 | 120586.239 | 121110.527 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 5217 | 5217 | 2 | 110624.767 | 120586.239 | 122159.103 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1227 | 1227 | 0 | 110624.767 | 120586.239 | 121110.527 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 3882 | 3882 | 2 | 110624.767 | 120586.239 | 121110.527 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1260 | 1260 | 1 | 110624.767 | 120061.951 | 120586.239 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 2802 | 2802 | 3 | 110624.767 | 120061.951 | 122159.103 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 2679 | 2679 | 4 | 109576.191 | 120061.951 | 122159.103 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1336 | 1336 | 1 | 111149.055 | 120061.951 | 121110.527 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1314 | 1314 | 0 | 109051.903 | 120061.951 | 121634.815 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 2610 | 2610 | 2 | 110100.479 | 120061.951 | 120586.239 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 2489 | 2489 | 2 | 109576.191 | 120061.951 | 121110.527 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1411 | 1411 | 0 | 111149.055 | 120061.951 | 121110.527 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1315 | 1315 | 0 | 110100.479 | 120061.951 | 120586.239 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 2705 | 2705 | 1 | 110624.767 | 120061.951 | 121634.815 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 3648 | 3648 | 2 | 110100.479 | 120061.951 | 120586.239 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1291 | 1291 | 3 | 110624.767 | 119537.663 | 120586.239 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1138 | 1138 | 2 | 110100.479 | 119537.663 | 120061.951 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 7198 | 7198 | 4 | 110624.767 | 119537.663 | 120586.239 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1283 | 1283 | 1 | 110100.479 | 119537.663 | 120586.239 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 3811 | 3811 | 2 | 109051.903 | 119537.663 | 120061.951 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 2401 | 2401 | 4 | 109576.191 | 119537.663 | 121110.527 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1274 | 1274 | 0 | 109051.903 | 119013.375 | 119537.663 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1259 | 1259 | 1 | 109576.191 | 119013.375 | 120061.951 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1128 | 1128 | 1 | 110100.479 | 119013.375 | 120061.951 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 3783 | 3783 | 4 | 110100.479 | 119013.375 | 120061.951 |
