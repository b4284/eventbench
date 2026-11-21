# T5-db-freeze-120s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 157.126s, closing 1.344s, drain 19.401s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 437383 | 437383 | 8855.559 | 9733.607 | 18025.6 | 580 | 0 | 0 | 266 | 2213 | 86774 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 269495 | 6087.127 | 199.807 | 4677.631 | 128974.847 | 141295.615 | 147324.927 |
| e2e_from_send | 269495 | 5982.743 | 192.767 | 4624.383 | 128974.847 | 141295.615 | 147324.927 |
| producer_lag | 269495 | 104.364 | 1.738 | 11.119 | 67.903 | 250.367 | 126156.799 |
| publish_call | 437383 | 589.422 | 44.415 | 97.215 | 253.439 | 125501.439 | 126156.799 |
| queue_wait | 269495 | 5366.427 | 123.839 | 4329.471 | 128843.775 | 141295.615 | 147324.927 |
| broker_dwell | 269495 | 4813.668 | 78.719 | 4089.855 | 128843.775 | 141295.615 | 147324.927 |
| receive_call | 376033 | 36.313 | 11.679 | 98.751 | 342.783 | 478.207 | 531.967 |
| ctx_load | 269495 | 8.873 | 6.067 | 19.231 | 46.399 | 77.375 | 133.119 |
| process | 269495 | 18.233 | 0.002 | 0.004 | 0.05 | 2648.063 | 112590.847 |
| ctx_commit | 269495 | 589.167 | 40.159 | 78.847 | 133.247 | 120979.455 | 120979.455 |
| ack | 268998 | 0.007 | 0.003 | 0.005 | 0.029 | 0.623 | 39.775 |
| service | 269495 | 616.338 | 49.311 | 93.631 | 221.695 | 120979.455 | 123797.503 |
| e2e_large | 166 | 15685.283 | 2775.039 | 121044.991 | 123600.895 | 124715.007 | 124715.007 |
| ready_wait | 269495 | 1537.476 | 52.959 | 574.975 | 121044.991 | 123797.503 | 126222.335 |
| blocked | 269495 | 463.778 | 40.159 | 366.335 | 1124.351 | 121307.135 | 123535.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9733.607 | 16864.3 |
| applied_total | 304396.655 | 405155 |
| bench_cpu_cores | 3.376 | 4.996 |
| bench_cpu_pct | 14.064 | 20.82 |
| bench_gc_gen0 | 2.379 | 4 |
| bench_gc_gen1 | 2.138 | 4 |
| bench_gc_gen2 | 0.448 | 1 |
| bench_gc_heap_mb | 970.462 | 1245.1 |
| bench_mem_mb | 1516.666 | 1663.3 |
| bench_threadpool_queue | 998.724 | 27751 |
| bench_threadpool_threads | 79.483 | 181 |
| broker_backlog | 18431.069 | 27495 |
| ctr_eb_etcd_cpu_pct | 1.162 | 7.07 |
| ctr_eb_etcd_mem_mb | 30.259 | 31.2 |
| ctr_eb_mosquitto_cpu_pct | 42.857 | 58.21 |
| ctr_eb_mosquitto_mem_mb | 7.162 | 9 |
| ctr_eb_pat1_cpu_pct | 57.66 | 72.79 |
| ctr_eb_pat1_mem_mb | 4485.008 | 5332 |
| ctr_eb_pat2_cpu_pct | 935.004 | 1048.57 |
| ctr_eb_pat2_mem_mb | 8971.967 | 9251.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.04 | 0.3 |
| ctr_eb_pgbouncer_2_mem_mb | 9.441 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.041 | 0.31 |
| ctr_eb_pgbouncer_3_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.047 | 0.33 |
| ctr_eb_pgbouncer_4_mem_mb | 10.948 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.044 | 0.3 |
| ctr_eb_pgbouncer_mem_mb | 11.631 | 11.9 |
| ctr_eb_postgres_cpu_pct | 1.458 | 4.8 |
| ctr_eb_postgres_mem_mb | 2394.397 | 2397.5 |
| duplicates_per_s | 21.052 | 460.4 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 62.121 | 89.24 |
| in_flight | 15401.448 | 28119 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.401 | 1.69 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 628778.793 | 925845 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 124094.31 | 171257 |
| octopus_mqtt_sent_total | 180636.966 | 258731 |
| octopus_publish_retries_total | 395.379 | 1912 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.759 | 18 |
| pg_active_loads | 9.621 | 47 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 54.759 | 98 |
| pg_conn_idle | 196.241 | 267 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.345 | 2 |
| pg_conn_total | 251 | 270 |
| pg_db_size_mb | 1369.645 | 1618.6 |
| pg_deadlocks | 2 | 2 |
| pg_replica_lag_mb | 1.865 | 5.14 |
| pg_replica_replay_lag_ms | 5.831 | 15.2 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26351.49 | 34648.5 |
| pg_tup_del_per_s | 34493.107 | 45389.1 |
| pg_tup_fetch_per_s | 493650.917 | 999653.2 |
| pg_tup_ins_per_s | 43406.403 | 59422.6 |
| pg_tup_upd_per_s | 29770.138 | 38449.4 |
| pg_wal_mb_per_s | 83.401 | 117.69 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 8855.559 | 14418 |
| produced_total | 319798.103 | 403880 |
| publish_mb_per_s | 33.494 | 71.32 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 72,790 records are Completed |
| record_counts | pass | 72,790 records found, 72,790 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 266 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 437,383 events applied by workers, 437,383 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 86,774 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1124 ms, max 123500 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1293 seq 33: ready 132 ms before start, 101 ms of it with an idle slot in group 41
- [cross_source_blocking] source 1300 seq 26: ready 132 ms before start, 100 ms of it with an idle slot in group 41
- [cross_source_blocking] source 13849 seq 9: ready 206 ms before start, 175 ms of it with an idle slot in group 438
- [cross_source_blocking] source 13831 seq 19: ready 189 ms before start, 158 ms of it with an idle slot in group 438
- [cross_source_blocking] source 1957 seq 15: ready 188 ms before start, 149 ms of it with an idle slot in group 55
- [cross_source_blocking] source 6888 seq 18: ready 119 ms before start, 119 ms of it with an idle slot in group 234
- [cross_source_blocking] source 12398 seq 8: ready 118 ms before start, 114 ms of it with an idle slot in group 394
- [cross_source_blocking] source 13759 seq 15: ready 203 ms before start, 172 ms of it with an idle slot in group 438
- [cross_source_blocking] source 4298 seq 24: ready 137 ms before start, 122 ms of it with an idle slot in group 147
- [cross_source_blocking] source 8452 seq 23: ready 104 ms before start, 104 ms of it with an idle slot in group 287
- [cross_source_blocking] source 10924 seq 14: ready 119 ms before start, 119 ms of it with an idle slot in group 356
- [cross_source_blocking] source 5900 seq 4: ready 102 ms before start, 102 ms of it with an idle slot in group 204
- [cross_source_blocking] source 13783 seq 12: ready 143 ms before start, 126 ms of it with an idle slot in group 438
- [cross_source_blocking] source 780 seq 2: ready 182 ms before start, 100 ms of it with an idle slot in group 23
- [cross_source_blocking] source 4287 seq 19: ready 129 ms before start, 114 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4339 seq 26: ready 128 ms before start, 113 ms of it with an idle slot in group 147
- [cross_source_blocking] source 1411 seq 3: ready 210 ms before start, 138 ms of it with an idle slot in group 44
- [cross_source_blocking] source 13855 seq 16: ready 191 ms before start, 126 ms of it with an idle slot in group 438
- [cross_source_blocking] source 13850 seq 11: ready 191 ms before start, 126 ms of it with an idle slot in group 438
- [cross_source_blocking] source 1912 seq 23: ready 184 ms before start, 129 ms of it with an idle slot in group 55

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1542 | 1542 | 0 | 2719.743 | 142606.335 | 145752.063 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4824 | 4824 | 1 | 1662.975 | 142606.335 | 144703.487 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1637 | 1637 | 0 | 1122.303 | 142606.335 | 145752.063 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4488 | 4488 | 4 | 1384.447 | 142606.335 | 147849.215 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1571 | 1571 | 4 | 1269.759 | 141557.759 | 143654.911 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3415 | 3415 | 4 | 2195.455 | 141557.759 | 144703.487 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6354 | 6354 | 10 | 659.455 | 141557.759 | 143654.911 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3269 | 3269 | 3 | 1638.399 | 141557.759 | 143654.911 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3201 | 3201 | 0 | 839.679 | 141557.759 | 143654.911 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3194 | 3194 | 3 | 2588.671 | 140509.183 | 143654.911 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4670 | 4670 | 2 | 1286.143 | 140509.183 | 144703.487 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1565 | 1565 | 2 | 2768.895 | 139460.607 | 143654.911 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1382 | 1382 | 1 | 602.111 | 136314.879 | 140509.183 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1473 | 1473 | 4 | 4882.431 | 135266.303 | 142606.335 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3041 | 3041 | 4 | 1040.383 | 134217.727 | 143654.911 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 2948 | 2948 | 0 | 409.599 | 133693.439 | 138412.031 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7239 | 7239 | 10 | 335.871 | 133169.151 | 142606.335 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1605 | 1605 | 3 | 6029.311 | 133169.151 | 138412.031 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1561 | 1561 | 0 | 3112.959 | 130547.711 | 133169.151 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1572 | 1572 | 2 | 4718.591 | 130547.711 | 135266.303 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3391 | 3391 | 2 | 4554.751 | 128974.847 | 135266.303 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6460 | 6460 | 3 | 5144.575 | 128974.847 | 132644.863 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 2823 | 2823 | 4 | 464.895 | 128450.559 | 133169.151 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4413 | 4413 | 6 | 737.279 | 127926.271 | 135266.303 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1704 | 1704 | 1 | 3784.703 | 127401.983 | 134217.727 |
