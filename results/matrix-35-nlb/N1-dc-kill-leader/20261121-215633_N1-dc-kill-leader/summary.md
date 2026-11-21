# N1-dc-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.224s, closing 2.772s, drain 15.294s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 490282 | 490282 | 6495.206 | 4039.214 | 20217.4 | 0 | 1814 | 1814 | 280 | 2485 | 98267 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 321569 | 28662.844 | 34668.543 | 48922.623 | 55902.207 | 58982.399 | 63897.599 |
| e2e_from_send | 321569 | 24201.47 | 28573.695 | 48267.263 | 55050.239 | 57999.359 | 62619.647 |
| producer_lag | 321569 | 4461.413 | 662.015 | 25182.207 | 26247.167 | 26492.927 | 26787.839 |
| publish_call | 490282 | 215.833 | 103.359 | 216.959 | 516.095 | 25804.799 | 25853.951 |
| queue_wait | 321569 | 24112.231 | 28442.623 | 48201.727 | 55017.471 | 57901.055 | 62488.575 |
| broker_dwell | 321569 | 23862.848 | 25903.103 | 48103.423 | 54919.167 | 57802.751 | 62455.807 |
| receive_call | 429600 | 31.614 | 7.599 | 83.327 | 365.567 | 484.351 | 541.183 |
| ctx_load | 321569 | 10.14 | 6.335 | 22.847 | 57.983 | 92.095 | 164.095 |
| process | 321569 | 10.83 | 0.002 | 0.004 | 0.063 | 2627.583 | 3006.463 |
| ctx_commit | 321569 | 68.162 | 40.895 | 163.967 | 322.303 | 404.735 | 424.447 |
| ack | 321545 | 0.008 | 0.004 | 0.007 | 0.046 | 0.662 | 57.119 |
| service | 321569 | 89.212 | 50.111 | 186.367 | 359.423 | 2705.407 | 3289.087 |
| e2e_large | 180 | 8282.086 | 3909.631 | 28327.935 | 30965.759 | 32538.623 | 32538.623 |
| ready_wait | 321569 | 1443.525 | 47.391 | 1670.143 | 37847.039 | 42860.543 | 49545.215 |
| blocked | 321569 | 1060.252 | 29.183 | 1123.327 | 27148.287 | 33751.039 | 36339.711 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4039.214 | 11862.1 |
| applied_total | 208338.612 | 276996 |
| bench_cpu_cores | 2.233 | 5.891 |
| bench_cpu_pct | 9.305 | 24.55 |
| bench_gc_gen0 | 1.673 | 5 |
| bench_gc_gen1 | 1.041 | 4 |
| bench_gc_gen2 | 0.796 | 3 |
| bench_gc_heap_mb | 573.822 | 1320.2 |
| bench_mem_mb | 1064.808 | 1772.6 |
| bench_threadpool_queue | 27.347 | 357 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 141275.837 | 177142 |
| ctr_eb_dc1a_cpu_pct | 678.031 | 723.79 |
| ctr_eb_dc1a_mem_mb | 4204.195 | 5079.3 |
| ctr_eb_dc1b_cpu_pct | 57.299 | 70.68 |
| ctr_eb_dc1b_mem_mb | 1577.914 | 1964.6 |
| ctr_eb_dc2a_cpu_pct | 28.28 | 68.45 |
| ctr_eb_dc2a_mem_mb | 1814.337 | 2262.1 |
| ctr_eb_dc2b_cpu_pct | 48.051 | 679.03 |
| ctr_eb_dc2b_mem_mb | 1919.818 | 3566.7 |
| ctr_eb_etcd_dc1_cpu_pct | 1.601 | 1.78 |
| ctr_eb_etcd_dc1_mem_mb | 40.509 | 47.5 |
| ctr_eb_etcd_dc2_cpu_pct | 1.779 | 3.08 |
| ctr_eb_etcd_dc2_mem_mb | 39.465 | 41.3 |
| ctr_eb_etcd_w_cpu_pct | 1.962 | 2.97 |
| ctr_eb_etcd_w_mem_mb | 40.582 | 42.7 |
| ctr_eb_haproxy_cpu_pct | 47.5 | 91.65 |
| ctr_eb_haproxy_mem_mb | 31.824 | 35.3 |
| ctr_eb_mosquitto_cpu_pct | 24.039 | 50.35 |
| ctr_eb_mosquitto_mem_mb | 15.384 | 34.8 |
| ctr_eb_mssql_cpu_pct | 2.194 | 3.78 |
| ctr_eb_mssql_mem_mb | 7062.239 | 7062.9 |
| ctr_eb_mysql_cpu_pct | 0.619 | 1.07 |
| ctr_eb_mysql_mem_mb | 9037 | 9037 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 8 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 8.1 | 8.1 |
| ctr_eb_postgres_cpu_pct | 1.044 | 2.79 |
| ctr_eb_postgres_mem_mb | 2215.396 | 2218.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 37.067 | 1632.3 |
| host_cpu_pct | 36.573 | 76.56 |
| in_flight | 145562.531 | 177053 |
| nacks_per_s | 37.067 | 1632.3 |
| octopus_dispatcher_cores | 0.625 | 1.33 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 581576.367 | 796108 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 150577.184 | 206010 |
| octopus_mqtt_sent_total | 126575.878 | 177820 |
| octopus_publish_retries_total | 8607.612 | 25228 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 12.245 | 17 |
| pg_active_loads | 11 | 37 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 42.571 | 78 |
| pg_conn_idle | 222.163 | 243 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.02 | 1 |
| pg_conn_total | 264.735 | 265 |
| pg_db_size_mb | 1515.888 | 1711 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 3.199 | 19.2 |
| pg_replica_replay_lag_ms | 28.049 | 124.8 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17471.72 | 19818.5 |
| pg_tup_del_per_s | 22414.473 | 28456.2 |
| pg_tup_fetch_per_s | 234704.778 | 278873.3 |
| pg_tup_ins_per_s | 36100.143 | 49573.9 |
| pg_tup_upd_per_s | 22019.918 | 25926.6 |
| pg_wal_mb_per_s | 81.888 | 108.08 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.939 | 1 |
| produced_per_s | 6495.206 | 18031.5 |
| produced_total | 353901.143 | 454049 |
| publish_mb_per_s | 23.733 | 65.06 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 79,229 records are Completed |
| record_counts | pass | 79,229 records found, 79,229 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 280 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 490,282 events applied by workers, 490,282 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 98,267 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27148 ms, max 36308 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12117 seq 4: ready 104 ms before start, 104 ms of it with an idle slot in group 387
- [cross_source_blocking] source 15637 seq 19: ready 228 ms before start, 228 ms of it with an idle slot in group 487
- [cross_source_blocking] source 7164 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 243
- [cross_source_blocking] source 5016 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 173
- [cross_source_blocking] source 15628 seq 15: ready 101 ms before start, 101 ms of it with an idle slot in group 486
- [cross_source_blocking] source 7157 seq 10: ready 102 ms before start, 102 ms of it with an idle slot in group 243
- [cross_source_blocking] source 12706 seq 10: ready 131 ms before start, 131 ms of it with an idle slot in group 400
- [cross_source_blocking] source 14200 seq 25: ready 102 ms before start, 102 ms of it with an idle slot in group 454
- [cross_source_blocking] source 15253 seq 8: ready 125 ms before start, 125 ms of it with an idle slot in group 476
- [cross_source_blocking] source 4381 seq 5: ready 196 ms before start, 196 ms of it with an idle slot in group 149
- [cross_source_blocking] source 9469 seq 12: ready 195 ms before start, 195 ms of it with an idle slot in group 327
- [cross_source_blocking] source 9346 seq 2: ready 207 ms before start, 207 ms of it with an idle slot in group 324
- [cross_source_blocking] source 7352 seq 9: ready 199 ms before start, 199 ms of it with an idle slot in group 252
- [cross_source_blocking] source 7112 seq 2: ready 178 ms before start, 178 ms of it with an idle slot in group 241
- [cross_source_blocking] source 15252 seq 9: ready 178 ms before start, 178 ms of it with an idle slot in group 476
- [cross_source_blocking] source 7104 seq 6: ready 179 ms before start, 179 ms of it with an idle slot in group 241
- [cross_source_blocking] source 1341 seq 4: ready 316 ms before start, 115 ms of it with an idle slot in group 42
- [cross_source_blocking] source 15296 seq 4: ready 181 ms before start, 181 ms of it with an idle slot in group 478
- [cross_source_blocking] source 6872 seq 5: ready 183 ms before start, 183 ms of it with an idle slot in group 233
- [cross_source_blocking] source 4536 seq 10: ready 178 ms before start, 178 ms of it with an idle slot in group 158

## Error samples

- t-163#0 src=4717 seq=22 op=Get rec=20259360735233 ev=8: NpgsqlException: Exception while reading from stream
- t-488#0 src=15668 seq=22 op=Get rec=67293547593734 ev=1: NpgsqlException: Exception while reading from stream
- t-302#0 src=8701 seq=47 op=Get rec=37370510442499 ev=8: NpgsqlException: Exception while reading from stream
- t-496#3 src=15872 seq=29 op=Complete rec=68169720922117 ev=1: NpgsqlException: Exception while reading from stream
- t-383#2 src=11837 seq=18 op=Get rec=50839527882753 ev=5: NpgsqlException: Exception while reading from stream
- t-264#0 src=7794 seq=13 op=Create rec=33474975105030 ev=0: NpgsqlException: Exception while reading from stream
- t-409#1 src=12956 seq=25 op=Update rec=55645596286982 ev=3: NpgsqlException: Exception while reading from stream
- t-127#0 src=3774 seq=32 op=Create rec=16209206575113 ev=0: NpgsqlException: Exception while reading from stream
- t-212#0 src=6198 seq=25 op=Update rec=26620207300612 ev=6: NpgsqlException: Exception while reading from stream
- t-278#0 src=8233 seq=32 op=Get rec=35360465747973 ev=3: NpgsqlException: Exception while reading from stream
- t-465#0 src=14840 seq=18 op=Get rec=63737314672643 ev=8: NpgsqlException: Exception while reading from stream
- t-131#0 src=3955 seq=15 op=Update rec=16986595655685 ev=1: NpgsqlException: Exception while reading from stream
- t-409#1 src=12942 seq=22 op=Update rec=55585466744836 ev=1: NpgsqlException: Exception while reading from stream
- t-194#1 src=5666 seq=19 op=Update rec=24335284699139 ev=2: NpgsqlException: Exception while reading from stream
- t-417#0 src=13125 seq=14 op=Complete rec=56371445760004 ev=1: NpgsqlException: Exception while reading from stream
- t-001#0 src=19 seq=10 op=Create rec=81604378627 ev=0: NpgsqlException: Exception while reading from stream
- t-177#0 src=5077 seq=17 op=Update rec=21805548961795 ev=6: NpgsqlException: Exception while reading from stream
- t-441#1 src=13891 seq=33 op=Get rec=59661390708741 ev=4: NpgsqlException: Exception while reading from stream
- t-354#1 src=10870 seq=15 op=Get rec=46686294507524 ev=1: NpgsqlException: Exception while reading from stream
- t-428#0 src=13392 seq=19 op=Create rec=57518202028036 ev=0: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3834 | 3834 | 0 | 45088.767 | 60817.407 | 63963.135 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1742 | 1742 | 0 | 45350.911 | 60030.975 | 62652.415 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7135 | 7135 | 0 | 44826.623 | 60030.975 | 62390.271 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3579 | 3579 | 0 | 44302.335 | 59244.543 | 60030.975 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5399 | 5399 | 0 | 44302.335 | 58982.399 | 61079.551 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3802 | 3802 | 0 | 43515.903 | 58458.111 | 61341.695 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3402 | 3402 | 0 | 43515.903 | 58195.967 | 58982.399 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1806 | 1806 | 0 | 43515.903 | 58195.967 | 58720.255 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1622 | 1622 | 0 | 44564.479 | 57933.823 | 60293.119 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1775 | 1775 | 0 | 44826.623 | 57933.823 | 59506.687 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1826 | 1826 | 0 | 42991.615 | 57933.823 | 58982.399 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1873 | 1873 | 0 | 45088.767 | 57933.823 | 58720.255 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5026 | 5026 | 0 | 43515.903 | 57933.823 | 60555.263 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 4738 | 4738 | 0 | 43253.759 | 57671.679 | 58720.255 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3637 | 3637 | 0 | 43778.047 | 57671.679 | 59768.831 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1754 | 1754 | 0 | 44040.191 | 57409.535 | 58458.111 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3587 | 3587 | 0 | 43778.047 | 57409.535 | 58720.255 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1760 | 1760 | 0 | 43253.759 | 57409.535 | 57933.823 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7298 | 7298 | 0 | 42991.615 | 57409.535 | 58458.111 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1730 | 1730 | 0 | 43515.903 | 57147.391 | 57671.679 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1749 | 1749 | 0 | 43253.759 | 57147.391 | 57933.823 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1581 | 1581 | 0 | 42729.471 | 56623.103 | 57671.679 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 6302 | 6302 | 0 | 42467.327 | 56623.103 | 57671.679 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3173 | 3173 | 0 | 42205.183 | 56360.959 | 60030.975 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5220 | 5220 | 0 | 42991.615 | 56360.959 | 57147.391 |
