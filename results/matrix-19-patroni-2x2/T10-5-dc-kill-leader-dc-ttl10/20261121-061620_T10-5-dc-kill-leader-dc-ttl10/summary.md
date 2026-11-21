# T10-5-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.555s, closing 2.564s, drain 23.623s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 477540 | 477540 | 6359.569 | 3293.771 | 18676.1 | 0 | 2069 | 2069 | 278 | 2413 | 62242 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 308600 | 35582.623 | 41254.911 | 53182.463 | 60063.743 | 62980.095 | 66519.039 |
| e2e_from_send | 308600 | 28190.127 | 35848.191 | 50626.559 | 57802.751 | 60981.247 | 64225.279 |
| producer_lag | 309068 | 7385.127 | 2850.815 | 26099.711 | 26918.911 | 27164.671 | 27770.879 |
| publish_call | 477540 | 232.566 | 134.399 | 234.111 | 489.983 | 23396.351 | 23396.351 |
| queue_wait | 308600 | 28061.733 | 35749.887 | 50593.791 | 57737.215 | 60948.479 | 63373.311 |
| broker_dwell | 308600 | 27791.782 | 35422.207 | 50429.951 | 57638.911 | 60850.175 | 63078.399 |
| receive_call | 431247 | 23.619 | 7.615 | 45.535 | 337.663 | 475.647 | 523.519 |
| ctx_load | 308600 | 9.299 | 6.191 | 18.527 | 49.279 | 110.719 | 936.447 |
| process | 308600 | 10.798 | 0.002 | 0.004 | 0.063 | 2631.679 | 3000.319 |
| ctx_commit | 308600 | 108.166 | 42.527 | 284.159 | 593.919 | 2777.087 | 2836.479 |
| ack | 308591 | 0.009 | 0.004 | 0.007 | 0.042 | 0.601 | 52.959 |
| service | 308600 | 128.349 | 52.127 | 301.567 | 783.871 | 2897.919 | 3749.887 |
| e2e_large | 178 | 10583.172 | 5595.135 | 28573.695 | 30916.607 | 33292.287 | 33292.287 |
| ready_wait | 308600 | 1273.871 | 25.135 | 1185.791 | 39452.671 | 46301.183 | 55771.135 |
| blocked | 308600 | 736.972 | 18.447 | 333.055 | 24084.479 | 29130.751 | 31145.983 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3293.771 | 11023.8 |
| applied_total | 154227.306 | 216174 |
| bench_cpu_cores | 1.585 | 3.327 |
| bench_cpu_pct | 6.602 | 13.86 |
| bench_gc_gen0 | 1.163 | 4 |
| bench_gc_gen1 | 0.816 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1131.596 | 1454.3 |
| bench_mem_mb | 1730.059 | 1933.6 |
| bench_threadpool_queue | 35.837 | 543 |
| bench_threadpool_threads | 42.041 | 44 |
| broker_backlog | 168328.755 | 217640 |
| ctr_eb_dc1a_cpu_pct | 62.281 | 1050.23 |
| ctr_eb_dc1a_mem_mb | 7925.322 | 9594 |
| ctr_eb_dc1b_cpu_pct | 24.424 | 80.96 |
| ctr_eb_dc1b_mem_mb | 3663.918 | 3756.5 |
| ctr_eb_dc2a_cpu_pct | 46.601 | 53.98 |
| ctr_eb_dc2a_mem_mb | 6595.533 | 7271.8 |
| ctr_eb_dc2b_cpu_pct | 656.993 | 768.27 |
| ctr_eb_dc2b_mem_mb | 7370.048 | 7918.4 |
| ctr_eb_etcd_dc1_cpu_pct | 1.663 | 3.18 |
| ctr_eb_etcd_dc1_mem_mb | 32.284 | 35.9 |
| ctr_eb_etcd_dc2_cpu_pct | 1.962 | 2.36 |
| ctr_eb_etcd_dc2_mem_mb | 29.192 | 33.7 |
| ctr_eb_etcd_w_cpu_pct | 2.022 | 3.88 |
| ctr_eb_etcd_w_mem_mb | 56.982 | 58.6 |
| ctr_eb_haproxy_cpu_pct | 50.223 | 106.93 |
| ctr_eb_haproxy_mem_mb | 36.19 | 39.1 |
| ctr_eb_mosquitto_cpu_pct | 20.475 | 41.91 |
| ctr_eb_mosquitto_mem_mb | 4.478 | 9.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.529 | 7.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 7.5 | 7.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.792 | 2.47 |
| ctr_eb_postgres_mem_mb | 1669.443 | 1670.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 42.28 | 2053.7 |
| host_cpu_pct | 36.585 | 78.78 |
| in_flight | 175659.469 | 225556 |
| nacks_per_s | 42.28 | 2053.7 |
| octopus_dispatcher_cores | 0.745 | 1.43 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 504085.102 | 692594 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 155774.878 | 207709 |
| octopus_mqtt_sent_total | 97072.02 | 141437 |
| octopus_publish_retries_total | 9367.735 | 23751 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.224 | 23 |
| pg_active_loads | 10.531 | 33 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 52.939 | 90 |
| pg_conn_idle | 213.898 | 232 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.429 | 3 |
| pg_conn_total | 266.837 | 268 |
| pg_db_size_mb | 1351.933 | 1517 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 11.69 | 17.24 |
| pg_replica_replay_lag_ms | 80.745 | 171.5 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 15313.294 | 17595.5 |
| pg_tup_del_per_s | 24346.241 | 33037.8 |
| pg_tup_fetch_per_s | 458595.859 | 539986.4 |
| pg_tup_ins_per_s | 46956.533 | 51279.5 |
| pg_tup_upd_per_s | 17010.969 | 19551.4 |
| pg_wal_mb_per_s | 84.119 | 93.73 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6359.569 | 15851.5 |
| produced_total | 329886.776 | 441730 |
| publish_mb_per_s | 23.631 | 63.61 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 77,662 records are Completed |
| record_counts | pass | 77,662 records found, 77,662 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 278 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 477,540 events applied by workers, 477,540 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 62,242 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 24084 ms, max 31136 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13212 seq 13: ready 111 ms before start, 111 ms of it with an idle slot in group 421
- [cross_source_blocking] source 3826 seq 5: ready 187 ms before start, 184 ms of it with an idle slot in group 130
- [cross_source_blocking] source 4389 seq 11: ready 166 ms before start, 166 ms of it with an idle slot in group 150
- [cross_source_blocking] source 3420 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 118
- [cross_source_blocking] source 14410 seq 2: ready 322 ms before start, 161 ms of it with an idle slot in group 459
- [cross_source_blocking] source 15802 seq 9: ready 322 ms before start, 322 ms of it with an idle slot in group 495
- [cross_source_blocking] source 7092 seq 14: ready 240 ms before start, 240 ms of it with an idle slot in group 240
- [cross_source_blocking] source 2462 seq 4: ready 322 ms before start, 322 ms of it with an idle slot in group 73
- [cross_source_blocking] source 8274 seq 10: ready 292 ms before start, 292 ms of it with an idle slot in group 281
- [cross_source_blocking] source 7404 seq 12: ready 234 ms before start, 234 ms of it with an idle slot in group 256
- [cross_source_blocking] source 12446 seq 6: ready 773 ms before start, 116 ms of it with an idle slot in group 396
- [cross_source_blocking] source 14062 seq 7: ready 774 ms before start, 119 ms of it with an idle slot in group 447
- [cross_source_blocking] source 9532 seq 5: ready 224 ms before start, 224 ms of it with an idle slot in group 331
- [cross_source_blocking] source 5840 seq 6: ready 222 ms before start, 222 ms of it with an idle slot in group 201
- [cross_source_blocking] source 13340 seq 5: ready 222 ms before start, 222 ms of it with an idle slot in group 426
- [cross_source_blocking] source 9469 seq 12: ready 230 ms before start, 230 ms of it with an idle slot in group 327
- [cross_source_blocking] source 13983 seq 12: ready 211 ms before start, 211 ms of it with an idle slot in group 444
- [cross_source_blocking] source 5413 seq 5: ready 230 ms before start, 230 ms of it with an idle slot in group 185
- [cross_source_blocking] source 13341 seq 4: ready 223 ms before start, 223 ms of it with an idle slot in group 426
- [cross_source_blocking] source 5673 seq 17: ready 160 ms before start, 160 ms of it with an idle slot in group 195

## Error samples

- t-147#1 src=4298 seq=38 op=Update rec=18459769438210 ev=9: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8923 seq=2 op=Update rec=38323993182209 ev=1: NpgsqlException: Exception while reading from stream
- t-312#0 src=9045 seq=9 op=Update rec=38847979192322 ev=3: NpgsqlException: Exception while reading from stream
- t-305#0 src=8760 seq=26 op=Create rec=37623913512968 ev=0: NpgsqlException: Exception while reading from stream
- t-387#0 src=12114 seq=14 op=Update rec=52029233823746 ev=1: NpgsqlException: Exception while reading from stream
- t-215#0 src=6311 seq=14 op=Get rec=27105538605058 ev=6: NpgsqlException: Exception while reading from stream
- t-496#2 src=15868 seq=33 op=Update rec=68152541052934 ev=2: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8918 seq=1 op=Create rec=38302518345729 ev=0: NpgsqlException: Exception while reading from stream
- t-496#2 src=15833 seq=36 op=Get rec=68002217197574 ev=1: NpgsqlException: Exception while reading from stream
- t-152#0 src=4420 seq=10 op=Update rec=18983755448321 ev=7: NpgsqlException: Exception while reading from stream
- t-199#0 src=5770 seq=3 op=Update rec=24781961297921 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3458 seq=2 op=Update rec=14851996909569 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3444 seq=3 op=Create rec=14791867367426 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3432 seq=3 op=Update rec=14740327759873 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3439 seq=5 op=Update rec=14770392530946 ev=1: NpgsqlException: Exception while reading from stream
- t-472#0 src=15113 seq=9 op=Get rec=64909840744450 ev=5: NpgsqlException: Exception while reading from stream
- t-436#0 src=13719 seq=6 op=Get rec=58922656333825 ev=4: NpgsqlException: Exception while reading from stream
- t-183#0 src=5347 seq=8 op=Create rec=22965190131714 ev=0: NpgsqlException: Exception while reading from stream
- t-347#3 src=10445 seq=12 op=Get rec=44860933406724 ev=1: NpgsqlException: Exception while reading from stream
- t-426#0 src=13337 seq=28 op=Update rec=57281978826756 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3744 | 3744 | 0 | 48496.639 | 64225.279 | 65011.711 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1781 | 1781 | 0 | 47972.351 | 64225.279 | 65011.711 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3722 | 3722 | 0 | 47972.351 | 64225.279 | 64749.567 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1700 | 1700 | 0 | 48234.495 | 63700.991 | 65273.855 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3537 | 3537 | 0 | 48758.783 | 63700.991 | 66584.575 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1717 | 1717 | 0 | 47972.351 | 63176.703 | 64225.279 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5247 | 5247 | 0 | 47972.351 | 62914.559 | 63963.135 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1751 | 1751 | 0 | 48758.783 | 62914.559 | 64749.567 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3492 | 3492 | 0 | 47710.207 | 62914.559 | 63700.991 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1718 | 1718 | 0 | 47972.351 | 62914.559 | 63176.703 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5086 | 5086 | 0 | 47972.351 | 62914.559 | 64225.279 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1689 | 1689 | 0 | 47710.207 | 62652.415 | 63176.703 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5142 | 5142 | 0 | 47448.063 | 62652.415 | 63438.847 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1607 | 1607 | 0 | 47972.351 | 62390.271 | 64225.279 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1710 | 1710 | 0 | 47185.919 | 62390.271 | 63176.703 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1706 | 1706 | 0 | 47448.063 | 62128.127 | 62914.559 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3322 | 3322 | 0 | 47710.207 | 62128.127 | 63176.703 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1840 | 1840 | 0 | 49020.927 | 62128.127 | 62652.415 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1764 | 1764 | 0 | 47710.207 | 62128.127 | 62652.415 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1710 | 1710 | 0 | 47448.063 | 61603.839 | 62652.415 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1588 | 1588 | 0 | 47972.351 | 61603.839 | 62390.271 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4822 | 4822 | 0 | 46923.775 | 61603.839 | 62652.415 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 1611 | 1611 | 0 | 46923.775 | 61603.839 | 62652.415 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1633 | 1633 | 0 | 47710.207 | 61341.695 | 61865.983 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 4699 | 4699 | 0 | 45875.199 | 61341.695 | 62128.127 |
