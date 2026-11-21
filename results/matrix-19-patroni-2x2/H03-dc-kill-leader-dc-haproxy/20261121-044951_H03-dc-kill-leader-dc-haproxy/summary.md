# H03-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.398s, closing 2.872s, drain 21.582s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 526196 | 526196 | 7272.555 | 4463.408 | 19006 | 0 | 1868 | 1868 | 295 | 2675 | 64593 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 357119 | 32975.572 | 36372.479 | 51118.079 | 59146.239 | 62193.663 | 65634.303 |
| e2e_from_send | 357119 | 23628.06 | 19136.511 | 49315.839 | 57507.839 | 60850.175 | 64684.031 |
| producer_lag | 357119 | 9347.513 | 2265.087 | 23494.655 | 24690.687 | 25149.439 | 25559.039 |
| publish_call | 526196 | 212.655 | 126.079 | 242.431 | 738.303 | 19480.575 | 19791.871 |
| queue_wait | 357119 | 23510.807 | 19103.743 | 49283.071 | 57507.839 | 60817.407 | 64520.191 |
| broker_dwell | 357119 | 23276.458 | 18808.831 | 49151.999 | 57343.999 | 60686.335 | 64454.655 |
| receive_call | 475255 | 26.058 | 7.235 | 59.935 | 327.423 | 478.463 | 563.711 |
| ctx_load | 357119 | 9.599 | 6.091 | 20.671 | 58.175 | 104.703 | 202.239 |
| process | 357119 | 10.798 | 0.002 | 0.004 | 0.055 | 2625.535 | 3043.327 |
| ctx_commit | 357119 | 96.784 | 54.559 | 213.887 | 757.759 | 1148.927 | 1321.983 |
| ack | 357102 | 0.008 | 0.004 | 0.007 | 0.043 | 0.626 | 37.823 |
| service | 357119 | 117.269 | 64.927 | 238.719 | 1007.615 | 2750.463 | 4112.383 |
| e2e_large | 195 | 11087.074 | 5410.815 | 25837.567 | 27557.887 | 29032.447 | 29032.447 |
| ready_wait | 357119 | 1046.573 | 25.935 | 780.287 | 38764.543 | 47480.831 | 57049.087 |
| blocked | 357119 | 598.134 | 18.319 | 288.511 | 21610.495 | 28196.863 | 32505.855 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4463.408 | 12842.2 |
| applied_total | 174495.041 | 282017 |
| bench_cpu_cores | 1.877 | 3.961 |
| bench_cpu_pct | 7.821 | 16.5 |
| bench_gc_gen0 | 1.306 | 3 |
| bench_gc_gen1 | 0.959 | 3 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 1139.563 | 1422.9 |
| bench_mem_mb | 1731.159 | 1973.4 |
| bench_threadpool_queue | 31.592 | 699 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 155785.878 | 200566 |
| ctr_eb_dc1a_cpu_pct | 156.553 | 1030.63 |
| ctr_eb_dc1a_mem_mb | 6319.349 | 8253.1 |
| ctr_eb_dc1b_cpu_pct | 30.135 | 90.98 |
| ctr_eb_dc1b_mem_mb | 3717.224 | 4289.6 |
| ctr_eb_dc2a_cpu_pct | 44.071 | 50.92 |
| ctr_eb_dc2a_mem_mb | 6567.252 | 7302.3 |
| ctr_eb_dc2b_cpu_pct | 711.101 | 747.18 |
| ctr_eb_dc2b_mem_mb | 8002.648 | 8407.6 |
| ctr_eb_etcd_dc1_cpu_pct | 1.439 | 2.37 |
| ctr_eb_etcd_dc1_mem_mb | 33.912 | 36.5 |
| ctr_eb_etcd_dc2_cpu_pct | 1.574 | 1.9 |
| ctr_eb_etcd_dc2_mem_mb | 34.548 | 36.1 |
| ctr_eb_etcd_w_cpu_pct | 1.772 | 3.05 |
| ctr_eb_etcd_w_mem_mb | 64.79 | 66.7 |
| ctr_eb_haproxy_cpu_pct | 58.784 | 114.37 |
| ctr_eb_haproxy_mem_mb | 30.963 | 32.6 |
| ctr_eb_mosquitto_cpu_pct | 24.122 | 45.15 |
| ctr_eb_mosquitto_mem_mb | 7.051 | 23.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.024 | 0.1 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 8.9 | 8.9 |
| ctr_eb_postgres_cpu_pct | 0.802 | 2.34 |
| ctr_eb_postgres_mem_mb | 1816.745 | 1833 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 38.143 | 1104.2 |
| host_cpu_pct | 42.349 | 80.69 |
| in_flight | 157589.878 | 207428 |
| nacks_per_s | 38.143 | 1104.2 |
| octopus_dispatcher_cores | 0.833 | 1.52 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 526680.327 | 798749 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 150920.959 | 221149 |
| octopus_mqtt_sent_total | 108569.306 | 180193 |
| octopus_publish_retries_total | 9137.878 | 21840 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.837 | 19 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 54.939 | 68 |
| pg_conn_idle | 213.755 | 240 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 268.694 | 269 |
| pg_db_size_mb | 1443.873 | 1641.6 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 14.074 | 18.09 |
| pg_replica_replay_lag_ms | 125.359 | 170.4 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 12583.967 | 20171 |
| pg_tup_del_per_s | 17567.522 | 29440.9 |
| pg_tup_fetch_per_s | 521112.647 | 604246.3 |
| pg_tup_ins_per_s | 35305.5 | 56797.8 |
| pg_tup_upd_per_s | 13270.69 | 23272.9 |
| pg_wal_mb_per_s | 77.287 | 102.14 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7272.555 | 20122.1 |
| produced_total | 332084.918 | 489445 |
| publish_mb_per_s | 25.731 | 63.4 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 83,579 records are Completed |
| record_counts | pass | 83,579 records found, 83,579 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 295 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 526,196 events applied by workers, 526,196 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 64,593 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 21610 ms, max 32493 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13020 seq 9: ready 314 ms before start, 259 ms of it with an idle slot in group 414
- [cross_source_blocking] source 7404 seq 12: ready 191 ms before start, 191 ms of it with an idle slot in group 256
- [cross_source_blocking] source 10924 seq 14: ready 191 ms before start, 191 ms of it with an idle slot in group 356
- [cross_source_blocking] source 12022 seq 8: ready 142 ms before start, 142 ms of it with an idle slot in group 384
- [cross_source_blocking] source 6396 seq 8: ready 154 ms before start, 154 ms of it with an idle slot in group 219
- [cross_source_blocking] source 3326 seq 4: ready 109 ms before start, 109 ms of it with an idle slot in group 113
- [cross_source_blocking] source 871 seq 16: ready 178 ms before start, 178 ms of it with an idle slot in group 26
- [cross_source_blocking] source 856 seq 7: ready 153 ms before start, 153 ms of it with an idle slot in group 24
- [cross_source_blocking] source 7118 seq 4: ready 173 ms before start, 173 ms of it with an idle slot in group 241
- [cross_source_blocking] source 11600 seq 7: ready 154 ms before start, 154 ms of it with an idle slot in group 377
- [cross_source_blocking] source 9498 seq 4: ready 175 ms before start, 175 ms of it with an idle slot in group 328
- [cross_source_blocking] source 4394 seq 9: ready 172 ms before start, 172 ms of it with an idle slot in group 150
- [cross_source_blocking] source 4028 seq 2: ready 119 ms before start, 119 ms of it with an idle slot in group 135
- [cross_source_blocking] source 9008 seq 5: ready 153 ms before start, 153 ms of it with an idle slot in group 311
- [cross_source_blocking] source 14168 seq 4: ready 155 ms before start, 155 ms of it with an idle slot in group 451
- [cross_source_blocking] source 4536 seq 10: ready 155 ms before start, 155 ms of it with an idle slot in group 158
- [cross_source_blocking] source 14631 seq 5: ready 178 ms before start, 178 ms of it with an idle slot in group 463
- [cross_source_blocking] source 2954 seq 8: ready 174 ms before start, 141 ms of it with an idle slot in group 96
- [cross_source_blocking] source 15958 seq 6: ready 178 ms before start, 178 ms of it with an idle slot in group 498
- [cross_source_blocking] source 314 seq 2: ready 177 ms before start, 177 ms of it with an idle slot in group 9

## Error samples

- t-344#5 src=10229 seq=13 op=Get rec=43933220470785 ev=7: NpgsqlException: Exception while reading from stream
- t-222#0 src=6583 seq=9 op=Update rec=28273769709570 ev=1: NpgsqlException: Exception while reading from stream
- t-102#0 src=3043 seq=13 op=Update rec=13069585481730 ev=5: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3460 seq=4 op=Update rec=14860586844161 ev=3: NpgsqlException: Exception while reading from stream
- t-012#0 src=401 seq=35 op=Update rec=1722281885702 ev=2: NpgsqlException: Exception while reading from stream
- t-409#1 src=12919 seq=18 op=Update rec=55486682497029 ev=4: NpgsqlException: Exception while reading from stream
- t-300#0 src=8655 seq=8 op=Get rec=37172941946882 ev=4: NpgsqlException: Exception while reading from stream
- t-381#0 src=11757 seq=16 op=Update rec=50495930499073 ev=13: NpgsqlException: Exception while reading from stream
- t-302#0 src=8703 seq=34 op=Get rec=37379100377092 ev=6: NpgsqlException: Exception while reading from stream
- t-316#0 src=9209 seq=19 op=Update rec=39552353828866 ev=10: NpgsqlException: Exception while reading from stream
- t-255#0 src=7397 seq=13 op=Get rec=31769873088513 ev=4: NpgsqlException: Exception while reading from stream
- t-382#0 src=11791 seq=7 op=Get rec=50641959387137 ev=6: NpgsqlException: Exception while reading from stream
- t-045#1 src=1532 seq=17 op=Get rec=6579889897475 ev=2: NpgsqlException: Exception while reading from stream
- t-147#0 src=4302 seq=21 op=Update rec=18476949307396 ev=3: NpgsqlException: Exception while reading from stream
- t-265#0 src=7908 seq=6 op=Get rec=33964601376769 ev=3: NpgsqlException: Exception while reading from stream
- t-381#0 src=11755 seq=23 op=Update rec=50487340564485 ev=2: NpgsqlException: Exception while reading from stream
- t-465#1 src=14840 seq=13 op=Update rec=63737314672643 ev=4: NpgsqlException: Exception while reading from stream
- t-475#0 src=15237 seq=11 op=Get rec=65442416689154 ev=1: NpgsqlException: Exception while reading from stream
- t-476#0 src=15245 seq=24 op=Get rec=65476776427521 ev=7: NpgsqlException: Exception while reading from stream
- t-347#1 src=10446 seq=10 op=Update rec=44865228374019 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4121 | 4121 | 0 | 44564.479 | 63438.847 | 64225.279 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4123 | 4123 | 0 | 44302.335 | 63438.847 | 64487.423 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3839 | 3839 | 0 | 44302.335 | 63176.703 | 65798.143 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3884 | 3884 | 0 | 44826.623 | 62914.559 | 64487.423 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1852 | 1852 | 0 | 44040.191 | 62652.415 | 64225.279 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1959 | 1959 | 0 | 43778.047 | 62652.415 | 63963.135 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5818 | 5818 | 0 | 44302.335 | 62390.271 | 63700.991 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3829 | 3829 | 0 | 43253.759 | 62128.127 | 63176.703 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1899 | 1899 | 0 | 44564.479 | 61603.839 | 62652.415 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1891 | 1891 | 0 | 42729.471 | 61603.839 | 63438.847 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5614 | 5614 | 0 | 43515.903 | 61603.839 | 62652.415 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1894 | 1894 | 0 | 42991.615 | 61341.695 | 62390.271 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5689 | 5689 | 0 | 43778.047 | 61341.695 | 62914.559 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1700 | 1700 | 0 | 41680.895 | 61341.695 | 62390.271 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1878 | 1878 | 0 | 43515.903 | 61341.695 | 62128.127 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3656 | 3656 | 0 | 43253.759 | 61341.695 | 62652.415 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1861 | 1861 | 0 | 43515.903 | 61341.695 | 62128.127 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5411 | 5411 | 0 | 42991.615 | 61341.695 | 63176.703 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1896 | 1896 | 0 | 43778.047 | 61079.551 | 62390.271 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1852 | 1852 | 0 | 43515.903 | 61079.551 | 61603.839 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1763 | 1763 | 0 | 43515.903 | 61079.551 | 62914.559 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1739 | 1739 | 0 | 44040.191 | 61079.551 | 61865.983 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3553 | 3553 | 0 | 42729.471 | 61079.551 | 62390.271 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1660 | 1660 | 0 | 41680.895 | 60817.407 | 61865.983 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1891 | 1891 | 0 | 43253.759 | 60817.407 | 61603.839 |
