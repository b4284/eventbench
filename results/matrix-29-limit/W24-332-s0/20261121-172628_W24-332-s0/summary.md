# W24-332-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.466s, closing 0.771s, drain 4.181s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 589513 | 589513 | 9563.231 | 9467.549 | 13081.3 | 0 | 0 | 0 | 0 | 2814 | 119723 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 476720 | 578.763 | 171.775 | 1729.535 | 5226.495 | 7675.903 | 11288.575 |
| e2e_from_send | 476720 | 567.16 | 162.815 | 1708.031 | 5214.207 | 7667.711 | 11288.575 |
| producer_lag | 476720 | 11.603 | 2.683 | 18.511 | 221.823 | 421.375 | 671.231 |
| publish_call | 589513 | 87.621 | 54.079 | 175.231 | 554.495 | 1557.503 | 2271.231 |
| queue_wait | 476720 | 514.441 | 115.263 | 1598.463 | 5152.767 | 7602.175 | 11280.383 |
| broker_dwell | 476720 | 468.934 | 65.599 | 1544.191 | 5103.615 | 7561.215 | 11247.615 |
| receive_call | 506098 | 30.4 | 17.183 | 70.463 | 225.791 | 438.015 | 587.263 |
| ctx_load | 476720 | 13.437 | 9.719 | 28.015 | 59.935 | 98.815 | 419.839 |
| process | 476720 | 9.914 | 0.002 | 0.005 | 0.083 | 2617.343 | 3022.847 |
| ctx_commit | 476720 | 29.13 | 21.439 | 59.807 | 129.279 | 279.807 | 469.759 |
| ack | 476701 | 0.015 | 0.004 | 0.008 | 0.07 | 1.675 | 350.975 |
| service | 476720 | 52.72 | 35.423 | 81.471 | 178.815 | 2658.303 | 3282.943 |
| ready_wait | 476720 | 100.674 | 36.127 | 291.327 | 759.807 | 1349.631 | 1945.599 |
| blocked | 476720 | 86.067 | 33.823 | 253.055 | 590.335 | 1038.847 | 1230.847 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9467.549 | 11669.5 |
| applied_total | 337445.612 | 560485 |
| bench_cpu_cores | 3.704 | 5.068 |
| bench_cpu_pct | 15.434 | 21.12 |
| bench_gc_gen0 | 1.224 | 3 |
| bench_gc_gen1 | 0.286 | 1 |
| bench_gc_gen2 | 0.082 | 1 |
| bench_gc_heap_mb | 480.822 | 777.7 |
| bench_mem_mb | 851.465 | 1061.8 |
| bench_threadpool_queue | 68.673 | 368 |
| bench_threadpool_threads | 41.939 | 44 |
| broker_backlog | 12421.388 | 26211 |
| ctr_eb_mosquitto_cpu_pct | 87.923 | 89.53 |
| ctr_eb_mosquitto_mem_mb | 15.259 | 21.8 |
| ctr_eb_mssql_cpu_pct | 4.432 | 6.12 |
| ctr_eb_mssql_mem_mb | 7444.478 | 7446.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 34.187 | 35.39 |
| ctr_eb_pgbouncer_2_mem_mb | 20.9 | 21.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.85 | 33.73 |
| ctr_eb_pgbouncer_3_mem_mb | 20.204 | 21.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 32.28 | 35 |
| ctr_eb_pgbouncer_4_mem_mb | 20.639 | 22 |
| ctr_eb_pgbouncer_cpu_pct | 31.572 | 33.35 |
| ctr_eb_pgbouncer_mem_mb | 20.688 | 21.3 |
| ctr_eb_postgres_cpu_pct | 1020.308 | 1091.6 |
| ctr_eb_postgres_mem_mb | 6026.48 | 6222.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.923 | 84.72 |
| in_flight | 4631.224 | 13924 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.132 | 2.19 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 672770.347 | 1139604 |
| octopus_missing_reports_total | 23167.102 | 34717 |
| octopus_mqtt_received_total | 176353.918 | 288228 |
| octopus_mqtt_sent_total | 263481.878 | 436346 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 19.306 | 49 |
| pg_active_loads | 12.306 | 34 |
| pg_active_polls | 0.082 | 1 |
| pg_active_sends | 0.082 | 1 |
| pg_cache_hit_pct | 99.979 | 100 |
| pg_conn_active | 44.837 | 103 |
| pg_conn_idle | 217.796 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.755 | 31 |
| pg_conn_total | 262.633 | 264 |
| pg_db_size_mb | 3611.086 | 4125.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24800.047 | 28850.7 |
| pg_tup_del_per_s | 33233.82 | 39261.2 |
| pg_tup_fetch_per_s | 274834.863 | 310402.5 |
| pg_tup_ins_per_s | 44623.012 | 51246.5 |
| pg_tup_upd_per_s | 53087.018 | 60256.8 |
| pg_wal_mb_per_s | 124.03 | 186.84 |
| pgb_cl_active | 499.429 | 567 |
| pgb_cl_waiting | 12.653 | 77 |
| pgb_maxwait_ms | 1.941 | 11.7 |
| pgb_sv_active | 26.286 | 48 |
| pgb_sv_idle | 21.714 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 9563.231 | 11693.8 |
| produced_total | 342076.837 | 571034 |
| publish_mb_per_s | 15.069 | 18.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 174,910 records are Completed |
| record_counts | pass | 174,910 records found, 76,058 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 402 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 589,513 events applied by workers, 589,513 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 119,723 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 590 ms, max 1230 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1310 seq 64: ready 115 ms before start, 115 ms of it with an idle slot in group 41
- [cross_source_blocking] source 869 seq 30: ready 139 ms before start, 139 ms of it with an idle slot in group 26
- [cross_source_blocking] source 13883 seq 8: ready 134 ms before start, 134 ms of it with an idle slot in group 440
- [cross_source_blocking] source 11300 seq 8: ready 117 ms before start, 117 ms of it with an idle slot in group 368
- [cross_source_blocking] source 5658 seq 28: ready 107 ms before start, 107 ms of it with an idle slot in group 194
- [cross_source_blocking] source 1322 seq 57: ready 112 ms before start, 112 ms of it with an idle slot in group 41
- [cross_source_blocking] source 8276 seq 11: ready 136 ms before start, 136 ms of it with an idle slot in group 281
- [cross_source_blocking] source 3574 seq 27: ready 133 ms before start, 123 ms of it with an idle slot in group 121
- [cross_source_blocking] source 5652 seq 23: ready 117 ms before start, 117 ms of it with an idle slot in group 194
- [cross_source_blocking] source 7797 seq 15: ready 130 ms before start, 106 ms of it with an idle slot in group 264
- [cross_source_blocking] source 5609 seq 30: ready 107 ms before start, 107 ms of it with an idle slot in group 194
- [cross_source_blocking] source 4545 seq 11: ready 124 ms before start, 122 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4578 seq 15: ready 121 ms before start, 120 ms of it with an idle slot in group 160
- [cross_source_blocking] source 8121 seq 21: ready 134 ms before start, 134 ms of it with an idle slot in group 274
- [cross_source_blocking] source 15919 seq 8: ready 139 ms before start, 107 ms of it with an idle slot in group 497
- [cross_source_blocking] source 15939 seq 6: ready 151 ms before start, 119 ms of it with an idle slot in group 497
- [cross_source_blocking] source 3594 seq 33: ready 138 ms before start, 123 ms of it with an idle slot in group 121
- [cross_source_blocking] source 15182 seq 3: ready 152 ms before start, 101 ms of it with an idle slot in group 474
- [cross_source_blocking] source 2245 seq 41: ready 108 ms before start, 100 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4818 seq 19: ready 221 ms before start, 166 ms of it with an idle slot in group 168

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-376 | 65 | 1 | 43.345 | 48.199 | 566 | 0 | 3233 | 3233 | 0 | 360.447 | 9043.967 | 11337.727 |
| t-488 | 26 | 1 | 107.285 | 47.505 | 1017 | 0 | 2915 | 2915 | 0 | 399.359 | 8454.143 | 10354.687 |
| t-040 | 119 | 2 | 48.767 | 98.209 | 1254 | 0 | 6226 | 6226 | 0 | 655.359 | 8093.695 | 9830.399 |
| t-465 | 66 | 3 | 131.577 | 141.969 | 667 | 0 | 8854 | 8854 | 0 | 428.031 | 7995.391 | 11272.191 |
| t-272 | 14 | 1 | 186.797 | 46.429 | 394 | 0 | 2805 | 2805 | 0 | 692.223 | 7110.655 | 7667.711 |
| t-194 | 78 | 5 | 182.864 | 244.07 | 1184 | 0 | 14804 | 14804 | 0 | 376.831 | 7045.119 | 9043.967 |
| t-104 | 46 | 1 | 59.3 | 45.479 | 2163 | 0 | 2845 | 2845 | 0 | 868.351 | 6782.975 | 8093.695 |
| t-354 | 90 | 2 | 64.766 | 96.929 | 157 | 0 | 6115 | 6115 | 0 | 294.911 | 6619.135 | 8978.431 |
| t-450 | 63 | 3 | 141.433 | 146.465 | 165 | 0 | 9026 | 9026 | 0 | 374.783 | 6586.367 | 7995.391 |
| t-065 | 80 | 9 | 315.578 | 416.57 | 263 | 0 | 25175 | 25175 | 0 | 262.143 | 6520.831 | 8519.679 |
| t-184 | 20 | 1 | 147.97 | 48.078 | 161 | 0 | 2977 | 2977 | 0 | 399.359 | 6422.527 | 9109.503 |
| t-378 | 55 | 3 | 159.715 | 143.096 | 3659 | 0 | 8784 | 8784 | 0 | 331.775 | 6422.527 | 8585.215 |
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8137 | 8137 | 0 | 335.871 | 6160.383 | 7208.959 |
| t-473 | 32 | 1 | 78.515 | 42.651 | 4074 | 0 | 2687 | 2687 | 0 | 299.007 | 6062.079 | 7700.479 |
| t-066 | 38 | 1 | 77.996 | 49.178 | 277 | 0 | 3032 | 3032 | 0 | 675.839 | 5963.775 | 7471.103 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5348 | 5348 | 0 | 276.479 | 5931.007 | 7208.959 |
| t-002 | 17 | 1 | 153.842 | 43.804 | 377 | 0 | 2680 | 2680 | 0 | 331.775 | 5799.935 | 7208.959 |
| t-409 | 48 | 2 | 120.205 | 94.736 | 671 | 0 | 5783 | 5783 | 0 | 348.159 | 5799.935 | 7798.783 |
| t-152 | 43 | 1 | 70.072 | 49.467 | 882 | 0 | 3034 | 3034 | 0 | 745.471 | 5603.327 | 8519.679 |
| t-120 | 37 | 2 | 154.992 | 94.103 | 1412 | 0 | 5814 | 5814 | 0 | 413.695 | 5570.559 | 7503.871 |
| t-425 | 89 | 5 | 154.597 | 225.995 | 2911 | 0 | 14031 | 14031 | 0 | 234.495 | 5439.487 | 7274.495 |
| t-169 | 33 | 1 | 87.477 | 48.432 | 260 | 0 | 3004 | 3004 | 0 | 228.351 | 5406.719 | 8028.159 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 7636 | 7636 | 0 | 220.159 | 5406.719 | 8191.999 |
| t-496 | 81 | 8 | 270.852 | 368.2 | 278 | 0 | 22177 | 22177 | 0 | 216.063 | 5406.719 | 8912.895 |
| t-306 | 134 | 7 | 139.774 | 312.564 | 2978 | 0 | 19319 | 19319 | 0 | 231.423 | 5144.575 | 8650.751 |
