# U0-none — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.562s, closing 3.725s, drain 33.086s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 736622 | 736622 | 11367.259 | 7665.847 | 21479.8 | 0 | 0 | 0 | 389 | 3811 | 372325 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 565379 | 17243.512 | 16588.799 | 36438.015 | 44990.463 | 49840.127 | 57409.535 |
| e2e_from_send | 565379 | 16855.556 | 15859.711 | 36175.871 | 44892.159 | 49643.519 | 57180.159 |
| producer_lag | 565379 | 387.952 | 25.615 | 928.767 | 4923.391 | 6180.863 | 6938.623 |
| publish_call | 736622 | 121.412 | 91.903 | 220.031 | 527.871 | 1853.439 | 1969.151 |
| queue_wait | 565379 | 16718.502 | 15720.447 | 36110.335 | 44826.623 | 49545.215 | 57147.391 |
| broker_dwell | 565379 | 16608.755 | 15564.799 | 36012.031 | 44761.087 | 49446.911 | 57081.855 |
| receive_call | 644966 | 30.928 | 7.331 | 86.527 | 345.343 | 479.487 | 573.439 |
| ctx_load | 565379 | 8.244 | 3.947 | 19.999 | 62.463 | 101.055 | 180.735 |
| process | 565379 | 10.837 | 0.002 | 0.004 | 0.047 | 2625.535 | 3012.607 |
| ctx_commit | 565379 | 117.931 | 71.423 | 218.111 | 819.711 | 2482.175 | 2641.919 |
| ack | 564033 | 0.006 | 0.003 | 0.005 | 0.028 | 0.342 | 55.263 |
| service | 565379 | 137.064 | 80.703 | 235.903 | 1380.351 | 2764.799 | 5279.743 |
| e2e_large | 289 | 3216.465 | 3074.047 | 4812.799 | 7786.495 | 8634.367 | 8634.367 |
| ready_wait | 565379 | 937.092 | 446.207 | 2297.855 | 8114.175 | 13590.527 | 35094.527 |
| blocked | 565379 | 375.608 | 190.463 | 979.967 | 2328.575 | 4007.935 | 16891.903 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7665.847 | 13064.1 |
| applied_total | 308216.98 | 464399 |
| bench_cpu_cores | 3.141 | 4.802 |
| bench_cpu_pct | 13.086 | 20.01 |
| bench_gc_gen0 | 2.347 | 4 |
| bench_gc_gen1 | 2.286 | 4 |
| bench_gc_gen2 | 0.531 | 1 |
| bench_gc_heap_mb | 1042.059 | 1220.6 |
| bench_mem_mb | 1573.51 | 1675 |
| bench_threadpool_queue | 35.714 | 243 |
| bench_threadpool_threads | 43.918 | 44 |
| broker_backlog | 145603.898 | 235604 |
| ctr_eb_dc1a_cpu_pct | 793.535 | 941.19 |
| ctr_eb_dc1a_mem_mb | 5523.245 | 7155.4 |
| ctr_eb_dc1b_cpu_pct | 63.399 | 82.55 |
| ctr_eb_dc1b_mem_mb | 2371.851 | 2914.4 |
| ctr_eb_dc2a_cpu_pct | 62.557 | 81.21 |
| ctr_eb_dc2a_mem_mb | 2377.039 | 2921.1 |
| ctr_eb_dc2b_cpu_pct | 61.6 | 81.78 |
| ctr_eb_dc2b_mem_mb | 2404.908 | 2939.2 |
| ctr_eb_etcd_dc1_cpu_pct | 1.874 | 2.7 |
| ctr_eb_etcd_dc1_mem_mb | 28.355 | 30.9 |
| ctr_eb_etcd_dc2_cpu_pct | 1.561 | 2.15 |
| ctr_eb_etcd_dc2_mem_mb | 25.929 | 26.9 |
| ctr_eb_etcd_w_cpu_pct | 1.428 | 1.73 |
| ctr_eb_etcd_w_mem_mb | 22.963 | 23.8 |
| ctr_eb_mosquitto_cpu_pct | 43.595 | 53.65 |
| ctr_eb_mosquitto_mem_mb | 10.416 | 28.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.025 | 0.06 |
| ctr_eb_pgbouncer_2_mem_mb | 9.3 | 9.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.023 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.029 | 0.06 |
| ctr_eb_pgbouncer_4_mem_mb | 10.916 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.03 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 1.914 | 4.74 |
| ctr_eb_postgres_mem_mb | 2210.698 | 2214.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 65.216 | 78.85 |
| in_flight | 149334.939 | 234342 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.473 | 1.69 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 761294.755 | 1201160 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 188018.878 | 319778 |
| octopus_mqtt_sent_total | 188172.755 | 301901 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.286 | 21 |
| pg_active_loads | 13.408 | 164 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 67.714 | 215 |
| pg_conn_idle | 199.49 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.898 | 11 |
| pg_conn_total | 267.204 | 268 |
| pg_db_size_mb | 1613.088 | 2082.9 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 9.569 | 24.56 |
| pg_replica_replay_lag_ms | 166.971 | 1639.2 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19679.045 | 28491.7 |
| pg_tup_del_per_s | 26981.931 | 35887.2 |
| pg_tup_fetch_per_s | 497354.882 | 644877.4 |
| pg_tup_ins_per_s | 43961.422 | 58272.3 |
| pg_tup_upd_per_s | 22253.596 | 32257.5 |
| pg_wal_mb_per_s | 86.25 | 119.96 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11367.259 | 16922.9 |
| produced_total | 457551.918 | 698741 |
| publish_mb_per_s | 39.181 | 69.73 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 108,597 records are Completed |
| record_counts | pass | 108,597 records found, 108,597 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 389 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 736,622 events applied by workers, 736,622 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 372,325 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2329 ms, max 16879 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8130 seq 10: ready 171 ms before start, 171 ms of it with an idle slot in group 275
- [cross_source_blocking] source 2332 seq 18: ready 171 ms before start, 171 ms of it with an idle slot in group 67
- [cross_source_blocking] source 14877 seq 7: ready 122 ms before start, 122 ms of it with an idle slot in group 467
- [cross_source_blocking] source 6398 seq 7: ready 148 ms before start, 148 ms of it with an idle slot in group 219
- [cross_source_blocking] source 1141 seq 10: ready 129 ms before start, 129 ms of it with an idle slot in group 38
- [cross_source_blocking] source 9512 seq 14: ready 129 ms before start, 129 ms of it with an idle slot in group 329
- [cross_source_blocking] source 5550 seq 16: ready 121 ms before start, 121 ms of it with an idle slot in group 190
- [cross_source_blocking] source 9254 seq 10: ready 121 ms before start, 121 ms of it with an idle slot in group 318
- [cross_source_blocking] source 9206 seq 3: ready 163 ms before start, 163 ms of it with an idle slot in group 316
- [cross_source_blocking] source 4235 seq 5: ready 129 ms before start, 129 ms of it with an idle slot in group 142
- [cross_source_blocking] source 8766 seq 13: ready 160 ms before start, 160 ms of it with an idle slot in group 305
- [cross_source_blocking] source 1002 seq 6: ready 169 ms before start, 169 ms of it with an idle slot in group 30
- [cross_source_blocking] source 4528 seq 5: ready 127 ms before start, 127 ms of it with an idle slot in group 157
- [cross_source_blocking] source 7316 seq 8: ready 178 ms before start, 178 ms of it with an idle slot in group 251
- [cross_source_blocking] source 7318 seq 9: ready 151 ms before start, 151 ms of it with an idle slot in group 251
- [cross_source_blocking] source 14401 seq 5: ready 120 ms before start, 120 ms of it with an idle slot in group 459
- [cross_source_blocking] source 14894 seq 6: ready 187 ms before start, 187 ms of it with an idle slot in group 467
- [cross_source_blocking] source 5917 seq 11: ready 129 ms before start, 129 ms of it with an idle slot in group 205
- [cross_source_blocking] source 15272 seq 6: ready 167 ms before start, 167 ms of it with an idle slot in group 477
- [cross_source_blocking] source 13020 seq 9: ready 185 ms before start, 185 ms of it with an idle slot in group 414

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2695 | 2695 | 0 | 28180.479 | 53477.375 | 56360.959 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2518 | 2518 | 0 | 26607.615 | 51642.367 | 57409.535 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8249 | 8249 | 0 | 30932.991 | 51118.079 | 56360.959 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2620 | 2620 | 0 | 29229.055 | 50331.647 | 52690.943 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2596 | 2596 | 0 | 31326.207 | 50069.503 | 52428.799 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2439 | 2439 | 0 | 28049.407 | 50069.503 | 54001.663 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2620 | 2620 | 0 | 30277.631 | 50069.503 | 54525.951 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5336 | 5336 | 0 | 30015.487 | 50069.503 | 52166.655 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5721 | 5721 | 0 | 31981.567 | 49807.359 | 54788.095 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2576 | 2576 | 0 | 28311.551 | 49545.215 | 52953.087 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5739 | 5739 | 0 | 32243.711 | 49283.071 | 53215.231 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5378 | 5378 | 0 | 30670.847 | 49283.071 | 52428.799 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 7818 | 7818 | 0 | 28704.767 | 49283.071 | 53215.231 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2692 | 2692 | 0 | 28966.911 | 48758.783 | 50593.791 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 2348 | 2348 | 0 | 25690.111 | 48496.639 | 50331.647 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2632 | 2632 | 0 | 29097.983 | 48234.495 | 50593.791 |
| t-350 | 85 | 2 | 53.604 | 73.675 | 285 | 0 | 4288 | 4288 | 0 | 23986.175 | 47972.351 | 52166.655 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 7988 | 7988 | 0 | 29884.415 | 47710.207 | 51118.079 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2393 | 2393 | 0 | 27525.119 | 47710.207 | 49807.359 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5143 | 5143 | 0 | 28966.911 | 47710.207 | 50331.647 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 7594 | 7594 | 0 | 27787.263 | 47710.207 | 51380.223 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2745 | 2745 | 0 | 30932.991 | 47448.063 | 49020.927 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5440 | 5440 | 0 | 31064.063 | 47448.063 | 49283.071 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2618 | 2618 | 0 | 27918.335 | 47448.063 | 50069.503 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2486 | 2486 | 0 | 26607.615 | 47448.063 | 49545.215 |
