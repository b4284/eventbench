# U3-dc-kill-other-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.476s, closing 2.446s, drain 56.64s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814176 | 814176 | 12907.402 | 7981.265 | 20919.7 | 0 | 0 | 0 | 430 | 4252 | 418495 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642450 | 19538.256 | 19775.487 | 37748.735 | 46170.111 | 62455.807 | 77070.335 |
| e2e_from_send | 642450 | 19009.62 | 19218.431 | 37355.519 | 45678.591 | 62357.503 | 76087.295 |
| producer_lag | 642450 | 528.643 | 259.711 | 1606.655 | 2652.159 | 2967.551 | 3653.631 |
| publish_call | 814176 | 112.845 | 89.087 | 209.663 | 468.991 | 1135.615 | 1145.855 |
| queue_wait | 642450 | 18879.724 | 19120.127 | 37289.983 | 45547.519 | 62029.823 | 75694.079 |
| broker_dwell | 642450 | 18775.765 | 19021.823 | 37191.679 | 45449.215 | 61997.055 | 75628.543 |
| receive_call | 730758 | 25.799 | 6.163 | 65.983 | 327.679 | 474.623 | 543.231 |
| ctx_load | 642450 | 22.181 | 3.035 | 14.031 | 48.351 | 193.023 | 30015.487 |
| process | 642450 | 10.909 | 0.002 | 0.004 | 0.046 | 2625.535 | 3020.799 |
| ctx_commit | 642450 | 96.762 | 44.191 | 261.119 | 666.111 | 1515.519 | 1576.959 |
| ack | 641012 | 0.006 | 0.003 | 0.005 | 0.028 | 0.344 | 37.887 |
| service | 642450 | 129.893 | 51.999 | 280.319 | 821.759 | 3174.399 | 30031.871 |
| e2e_large | 330 | 3236.51 | 3131.391 | 4616.191 | 6471.679 | 7839.743 | 7839.743 |
| ready_wait | 642450 | 857.929 | 414.975 | 2213.887 | 5775.359 | 10076.159 | 30769.151 |
| blocked | 642450 | 334.466 | 180.607 | 836.095 | 2041.855 | 4886.527 | 18661.375 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7981.265 | 16539.6 |
| applied_total | 271539 | 486891 |
| bench_cpu_cores | 2.951 | 4.669 |
| bench_cpu_pct | 12.296 | 19.45 |
| bench_gc_gen0 | 2.551 | 5 |
| bench_gc_gen1 | 2.388 | 5 |
| bench_gc_gen2 | 0.551 | 2 |
| bench_gc_heap_mb | 1044.906 | 1243.5 |
| bench_mem_mb | 1556.857 | 1681.4 |
| bench_threadpool_queue | 32.857 | 310 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 182869.776 | 288309 |
| ctr_eb_dc1a_cpu_pct | 739.786 | 1046.34 |
| ctr_eb_dc1a_mem_mb | 11647.284 | 12178.4 |
| ctr_eb_dc1b_cpu_pct | 47.484 | 60.34 |
| ctr_eb_dc1b_mem_mb | 3178.267 | 3275.9 |
| ctr_eb_dc2a_cpu_pct | 46.833 | 60.36 |
| ctr_eb_dc2a_mem_mb | 4143.548 | 4168.5 |
| ctr_eb_dc2b_cpu_pct | 44.659 | 57.54 |
| ctr_eb_dc2b_mem_mb | 7921.771 | 8016.6 |
| ctr_eb_etcd_dc1_cpu_pct | 2.107 | 3.04 |
| ctr_eb_etcd_dc1_mem_mb | 38.024 | 39.5 |
| ctr_eb_etcd_dc2_cpu_pct | 1.245 | 1.32 |
| ctr_eb_etcd_dc2_mem_mb | 30.792 | 35.5 |
| ctr_eb_etcd_w_cpu_pct | 1.829 | 2.72 |
| ctr_eb_etcd_w_mem_mb | 36.049 | 38 |
| ctr_eb_mosquitto_cpu_pct | 38.001 | 54.32 |
| ctr_eb_mosquitto_mem_mb | 6.673 | 16.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.026 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.394 | 9.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.027 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 10.018 | 10.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.023 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 1.432 | 2.27 |
| ctr_eb_postgres_mem_mb | 2211.504 | 2212.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 60.282 | 80.35 |
| in_flight | 189046.592 | 289936 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.472 | 1.84 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 722111.245 | 1238708 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 195806.816 | 326175 |
| octopus_mqtt_sent_total | 162981.184 | 295614 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.347 | 21 |
| pg_active_loads | 1.898 | 13 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.976 | 100 |
| pg_conn_active | 51.224 | 76 |
| pg_conn_idle | 216.041 | 237 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.306 | 3 |
| pg_conn_total | 267.265 | 268 |
| pg_db_size_mb | 1767.976 | 2513.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 11.105 | 48.01 |
| pg_replica_replay_lag_ms | 107.684 | 473.2 |
| pg_replicas | 1.898 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19415.878 | 29605.2 |
| pg_tup_del_per_s | 26495.984 | 35808.8 |
| pg_tup_fetch_per_s | 589666.076 | 778454.1 |
| pg_tup_ins_per_s | 46263.806 | 59322.2 |
| pg_tup_upd_per_s | 21428.433 | 35093.5 |
| pg_wal_mb_per_s | 102.641 | 274.93 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12907.402 | 22966 |
| produced_total | 460585.592 | 776827 |
| publish_mb_per_s | 44.599 | 75.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,544 records are Completed |
| record_counts | pass | 117,544 records found, 117,544 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,176 events applied by workers, 814,176 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 418,495 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2042 ms, max 18648 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12443 seq 10: ready 135 ms before start, 135 ms of it with an idle slot in group 396
- [cross_source_blocking] source 12377 seq 4: ready 140 ms before start, 140 ms of it with an idle slot in group 393
- [cross_source_blocking] source 11209 seq 9: ready 135 ms before start, 126 ms of it with an idle slot in group 365
- [cross_source_blocking] source 1989 seq 7: ready 140 ms before start, 132 ms of it with an idle slot in group 58
- [cross_source_blocking] source 12739 seq 6: ready 141 ms before start, 141 ms of it with an idle slot in group 402
- [cross_source_blocking] source 5062 seq 9: ready 121 ms before start, 121 ms of it with an idle slot in group 176
- [cross_source_blocking] source 1020 seq 12: ready 138 ms before start, 138 ms of it with an idle slot in group 31
- [cross_source_blocking] source 6681 seq 5: ready 128 ms before start, 128 ms of it with an idle slot in group 227
- [cross_source_blocking] source 9519 seq 14: ready 136 ms before start, 136 ms of it with an idle slot in group 330
- [cross_source_blocking] source 10627 seq 8: ready 140 ms before start, 126 ms of it with an idle slot in group 349
- [cross_source_blocking] source 1785 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 51
- [cross_source_blocking] source 9685 seq 10: ready 169 ms before start, 169 ms of it with an idle slot in group 336
- [cross_source_blocking] source 13205 seq 9: ready 170 ms before start, 170 ms of it with an idle slot in group 420
- [cross_source_blocking] source 1141 seq 10: ready 167 ms before start, 120 ms of it with an idle slot in group 38
- [cross_source_blocking] source 7913 seq 23: ready 129 ms before start, 129 ms of it with an idle slot in group 266
- [cross_source_blocking] source 871 seq 16: ready 135 ms before start, 135 ms of it with an idle slot in group 26
- [cross_source_blocking] source 2950 seq 8: ready 121 ms before start, 121 ms of it with an idle slot in group 95
- [cross_source_blocking] source 5917 seq 11: ready 140 ms before start, 120 ms of it with an idle slot in group 205
- [cross_source_blocking] source 13213 seq 12: ready 137 ms before start, 137 ms of it with an idle slot in group 421
- [cross_source_blocking] source 15628 seq 15: ready 113 ms before start, 113 ms of it with an idle slot in group 486
- [cross_source_blocking] source 9254 seq 10: ready 126 ms before start, 126 ms of it with an idle slot in group 318

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13785 | 13785 | 0 | 40108.031 | 65011.711 | 76546.047 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4559 | 4559 | 0 | 30932.991 | 62914.559 | 67633.151 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12247 | 12247 | 0 | 39059.455 | 62128.127 | 77070.335 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2908 | 2908 | 0 | 38010.879 | 61865.983 | 71827.455 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12121 | 12121 | 0 | 38535.167 | 60555.263 | 75497.471 |
| t-121 | 76 | 3 | 97.816 | 119.994 | 149 | 0 | 7234 | 7234 | 0 | 27656.191 | 59768.831 | 70778.879 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5906 | 5906 | 0 | 32374.783 | 58458.111 | 71303.167 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4968 | 4968 | 0 | 29229.055 | 58458.111 | 64749.567 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2682 | 2682 | 0 | 34603.007 | 57147.391 | 66322.431 |
| t-088 | 46 | 1 | 43.405 | 32.75 | 203 | 0 | 2225 | 2225 | 0 | 24903.679 | 56885.247 | 66060.287 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4616 | 4616 | 0 | 24117.247 | 56623.103 | 62914.559 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5305 | 5305 | 0 | 28573.695 | 55836.671 | 64487.423 |
| t-080 | 56 | 1 | 32.985 | 31.08 | 822 | 0 | 2164 | 2164 | 0 | 26083.327 | 54788.095 | 67633.151 |
| t-350 | 85 | 2 | 53.604 | 73.675 | 285 | 0 | 4669 | 4669 | 0 | 26607.615 | 54525.951 | 63700.991 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 33816.575 | 53739.519 | 57409.535 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 10466 | 10466 | 0 | 31588.351 | 53215.231 | 72351.743 |
| t-045 | 47 | 2 | 82.615 | 64.377 | 1398 | 0 | 3962 | 3962 | 0 | 24117.247 | 52953.087 | 64487.423 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 4364 | 4364 | 0 | 23330.815 | 52953.087 | 64487.423 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2977 | 2977 | 0 | 27394.047 | 51904.511 | 55836.671 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 1876 | 1876 | 0 | 17825.791 | 51118.079 | 59768.831 |
| t-169 | 33 | 1 | 47.03 | 26.039 | 260 | 0 | 1640 | 1640 | 0 | 12910.591 | 51118.079 | 59244.543 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2678 | 2678 | 0 | 30146.559 | 51118.079 | 54788.095 |
| t-488 | 26 | 1 | 57.68 | 25.54 | 1017 | 0 | 1556 | 1556 | 0 | 15269.887 | 50331.647 | 53215.231 |
| t-129 | 19 | 1 | 90.638 | 29.84 | 623 | 0 | 1862 | 1862 | 0 | 17563.647 | 49807.359 | 56885.247 |
| t-066 | 38 | 1 | 41.933 | 26.44 | 277 | 0 | 1727 | 1727 | 0 | 13959.167 | 49545.215 | 60030.975 |
