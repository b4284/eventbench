# T0b-none — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.39s, closing 2.388s, drain 10.109s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 799985 | 799985 | 12483.904 | 12227.714 | 19443.9 | 0 | 0 | 0 | 421 | 4167 | 247772 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 628344 | 2288.515 | 319.231 | 5910.527 | 30834.687 | 51937.279 | 56623.103 |
| e2e_from_send | 628344 | 2214.571 | 300.543 | 5664.767 | 30834.687 | 51904.511 | 56623.103 |
| producer_lag | 628344 | 73.942 | 2.135 | 71.231 | 1489.919 | 1770.495 | 2201.599 |
| publish_call | 799985 | 65.365 | 46.527 | 117.695 | 368.639 | 977.407 | 1077.247 |
| queue_wait | 628344 | 2135.567 | 226.559 | 5578.751 | 30801.919 | 51904.511 | 56623.103 |
| broker_dwell | 628344 | 2083.101 | 177.279 | 5480.447 | 30752.767 | 51806.207 | 56623.103 |
| receive_call | 697652 | 34.916 | 11.135 | 94.719 | 347.135 | 483.071 | 558.079 |
| ctx_load | 628344 | 8.694 | 5.895 | 19.055 | 42.527 | 71.295 | 119.423 |
| process | 628344 | 10.887 | 0.002 | 0.004 | 0.054 | 2625.535 | 3004.415 |
| ctx_commit | 628344 | 59.36 | 45.119 | 91.071 | 363.007 | 1356.799 | 1560.575 |
| ack | 627723 | 0.009 | 0.003 | 0.005 | 0.032 | 0.769 | 76.991 |
| service | 628344 | 79.007 | 53.919 | 106.367 | 997.887 | 2715.647 | 4354.047 |
| e2e_large | 321 | 2806.867 | 2699.263 | 4022.271 | 5230.591 | 6230.015 | 6230.015 |
| ready_wait | 628344 | 258.29 | 91.391 | 699.903 | 2332.671 | 3684.351 | 5414.911 |
| blocked | 628344 | 139.043 | 60.287 | 394.751 | 835.071 | 1242.111 | 2273.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12227.714 | 15369.9 |
| applied_total | 434159.714 | 705270 |
| bench_cpu_cores | 3.994 | 4.986 |
| bench_cpu_pct | 16.644 | 20.77 |
| bench_gc_gen0 | 3.102 | 5 |
| bench_gc_gen1 | 2.673 | 5 |
| bench_gc_gen2 | 0.51 | 2 |
| bench_gc_heap_mb | 999.704 | 1271.7 |
| bench_mem_mb | 1551.265 | 1755.3 |
| bench_threadpool_queue | 78.653 | 580 |
| bench_threadpool_threads | 41.49 | 44 |
| broker_backlog | 34068.633 | 49076 |
| ctr_eb_etcd_cpu_pct | 0.847 | 1.01 |
| ctr_eb_etcd_mem_mb | 31.82 | 34 |
| ctr_eb_mosquitto_cpu_pct | 53.711 | 59.21 |
| ctr_eb_mosquitto_mem_mb | 14.853 | 27.5 |
| ctr_eb_pat1_cpu_pct | 59.567 | 70.88 |
| ctr_eb_pat1_mem_mb | 3371.163 | 3475.2 |
| ctr_eb_pat2_cpu_pct | 996.253 | 1057.04 |
| ctr_eb_pat2_mem_mb | 9230.635 | 9651.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.028 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 9.418 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.032 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 9.757 | 10.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.029 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 10.933 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.6 | 11.6 |
| ctr_eb_postgres_cpu_pct | 1.604 | 2.49 |
| ctr_eb_postgres_mem_mb | 2381.618 | 2384.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 73.223 | 79.93 |
| in_flight | 32499.102 | 53450 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.606 | 1.7 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 890522.98 | 1485833 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 206498.245 | 341603 |
| octopus_mqtt_sent_total | 265812.918 | 455829 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.959 | 21 |
| pg_active_loads | 6.776 | 21 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 42.714 | 73 |
| pg_conn_idle | 226.265 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.531 | 13 |
| pg_conn_total | 268.98 | 271 |
| pg_db_size_mb | 1643.751 | 2158.7 |
| pg_deadlocks | 2 | 2 |
| pg_replica_lag_mb | 5.257 | 68.18 |
| pg_replica_replay_lag_ms | 58.567 | 896.2 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0.018 | 0.3 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 29748.673 | 35268.6 |
| pg_tup_del_per_s | 35866.492 | 44292.2 |
| pg_tup_fetch_per_s | 507768.073 | 665247.5 |
| pg_tup_ins_per_s | 46573.353 | 61436.7 |
| pg_tup_upd_per_s | 33777.467 | 40287.3 |
| pg_wal_mb_per_s | 93.248 | 184.6 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12483.904 | 15492.8 |
| produced_total | 466658.816 | 758720 |
| publish_mb_per_s | 43.203 | 70.16 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 115,909 records are Completed |
| record_counts | pass | 115,909 records found, 115,909 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 421 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 799,985 events applied by workers, 799,985 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 247,772 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 835 ms, max 2272 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12662 seq 8: ready 128 ms before start, 128 ms of it with an idle slot in group 398
- [cross_source_blocking] source 15818 seq 26: ready 108 ms before start, 108 ms of it with an idle slot in group 496
- [cross_source_blocking] source 12706 seq 10: ready 116 ms before start, 116 ms of it with an idle slot in group 400
- [cross_source_blocking] source 10882 seq 5: ready 141 ms before start, 141 ms of it with an idle slot in group 354
- [cross_source_blocking] source 4287 seq 19: ready 125 ms before start, 102 ms of it with an idle slot in group 147
- [cross_source_blocking] source 15854 seq 16: ready 132 ms before start, 132 ms of it with an idle slot in group 496
- [cross_source_blocking] source 14862 seq 5: ready 116 ms before start, 116 ms of it with an idle slot in group 466
- [cross_source_blocking] source 4210 seq 3: ready 129 ms before start, 129 ms of it with an idle slot in group 139
- [cross_source_blocking] source 5441 seq 20: ready 130 ms before start, 123 ms of it with an idle slot in group 186
- [cross_source_blocking] source 10834 seq 13: ready 124 ms before start, 124 ms of it with an idle slot in group 354
- [cross_source_blocking] source 314 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 9
- [cross_source_blocking] source 15759 seq 19: ready 114 ms before start, 114 ms of it with an idle slot in group 493
- [cross_source_blocking] source 9511 seq 13: ready 114 ms before start, 114 ms of it with an idle slot in group 329
- [cross_source_blocking] source 12925 seq 14: ready 128 ms before start, 116 ms of it with an idle slot in group 409
- [cross_source_blocking] source 6083 seq 10: ready 114 ms before start, 114 ms of it with an idle slot in group 208
- [cross_source_blocking] source 10836 seq 6: ready 130 ms before start, 129 ms of it with an idle slot in group 354
- [cross_source_blocking] source 7259 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 248
- [cross_source_blocking] source 10848 seq 7: ready 154 ms before start, 123 ms of it with an idle slot in group 354
- [cross_source_blocking] source 15791 seq 3: ready 101 ms before start, 101 ms of it with an idle slot in group 494
- [cross_source_blocking] source 2979 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 98
- [cross_source_blocking] source 5175 seq 7: ready 115 ms before start, 115 ms of it with an idle slot in group 179

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6227 | 6227 | 0 | 2818.047 | 52166.655 | 56623.103 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9015 | 9015 | 0 | 3702.783 | 52166.655 | 56098.815 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6177 | 6177 | 0 | 2555.903 | 52166.655 | 54788.095 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2846 | 2846 | 0 | 5177.343 | 51904.511 | 54788.095 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3011 | 3011 | 0 | 5210.111 | 51904.511 | 55312.383 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11923 | 11923 | 0 | 1613.823 | 51118.079 | 56623.103 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5828 | 5828 | 0 | 3653.631 | 51118.079 | 56098.815 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12027 | 12027 | 0 | 1171.455 | 51118.079 | 55836.671 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2825 | 2825 | 0 | 3325.951 | 50593.791 | 54788.095 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8638 | 8638 | 0 | 2490.367 | 49545.215 | 54263.807 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5959 | 5959 | 0 | 1982.463 | 49283.071 | 54263.807 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2572 | 2572 | 0 | 2342.911 | 48496.639 | 54263.807 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2847 | 2847 | 0 | 3686.399 | 48496.639 | 54788.095 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2956 | 2956 | 0 | 3129.343 | 47972.351 | 53739.519 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2968 | 2968 | 0 | 4292.607 | 47448.063 | 54525.951 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2847 | 2847 | 0 | 3407.871 | 45613.055 | 54263.807 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2699 | 2699 | 0 | 2408.447 | 45088.767 | 54525.951 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8470 | 8470 | 0 | 1695.743 | 44564.479 | 54525.951 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2856 | 2856 | 0 | 3276.799 | 44040.191 | 54263.807 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8256 | 8256 | 0 | 974.847 | 43778.047 | 54001.663 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5798 | 5798 | 0 | 2916.351 | 43515.903 | 55574.527 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5395 | 5395 | 0 | 1261.567 | 43253.759 | 53739.519 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5607 | 5607 | 0 | 2506.751 | 42729.471 | 54525.951 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2627 | 2627 | 0 | 2965.503 | 41418.751 | 54001.663 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2934 | 2934 | 0 | 1654.783 | 38010.879 | 54263.807 |
