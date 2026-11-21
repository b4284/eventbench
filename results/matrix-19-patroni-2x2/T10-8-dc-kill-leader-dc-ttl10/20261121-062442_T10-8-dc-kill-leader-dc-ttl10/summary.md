# T10-8-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.339s, closing 3.595s, drain 17.78s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 432964 | 432964 | 5438.557 | 2816.586 | 18027.5 | 0 | 2007 | 2007 | 265 | 2196 | 55059 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 264701 | 36799.618 | 43122.687 | 53641.215 | 59637.759 | 62193.663 | 64815.103 |
| e2e_from_send | 264701 | 28758.906 | 37421.055 | 50855.935 | 57442.303 | 60522.495 | 63275.007 |
| producer_lag | 265150 | 8032.377 | 3356.671 | 29179.903 | 30162.943 | 30834.687 | 31129.599 |
| publish_call | 432964 | 259.251 | 141.695 | 262.655 | 562.175 | 24838.143 | 24838.143 |
| queue_wait | 264701 | 28627.249 | 37322.751 | 50790.399 | 57409.535 | 60456.959 | 62586.879 |
| broker_dwell | 264701 | 28436.192 | 37126.143 | 50659.327 | 57278.463 | 60358.655 | 62488.575 |
| receive_call | 393704 | 24.968 | 7.271 | 51.327 | 336.127 | 479.231 | 547.839 |
| ctx_load | 264701 | 9.306 | 6.043 | 18.751 | 62.175 | 122.495 | 235.519 |
| process | 264701 | 10.915 | 0.002 | 0.005 | 0.072 | 2633.727 | 3000.319 |
| ctx_commit | 264701 | 111.357 | 46.847 | 303.103 | 663.551 | 1085.439 | 1179.647 |
| ack | 264686 | 0.009 | 0.004 | 0.007 | 0.05 | 0.607 | 71.295 |
| service | 264701 | 131.655 | 56.191 | 319.487 | 970.239 | 2742.271 | 3954.687 |
| e2e_large | 164 | 11488.599 | 5881.855 | 32030.719 | 33816.575 | 35258.367 | 35258.367 |
| ready_wait | 264701 | 1463.531 | 23.951 | 1350.655 | 40763.391 | 46956.543 | 57245.695 |
| blocked | 264701 | 940.592 | 17.311 | 332.543 | 27983.871 | 32358.399 | 34373.631 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2816.586 | 10065.3 |
| applied_total | 150449.449 | 203940 |
| bench_cpu_cores | 1.48 | 3.42 |
| bench_cpu_pct | 6.166 | 14.25 |
| bench_gc_gen0 | 1.041 | 3 |
| bench_gc_gen1 | 0.776 | 3 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 1077.3 | 1389.6 |
| bench_mem_mb | 1735.259 | 1858.7 |
| bench_threadpool_queue | 22.633 | 508 |
| bench_threadpool_threads | 42.898 | 44 |
| broker_backlog | 153682.224 | 193825 |
| ctr_eb_dc1a_cpu_pct | 640.461 | 745.61 |
| ctr_eb_dc1a_mem_mb | 7651.619 | 8243.6 |
| ctr_eb_dc1b_cpu_pct | 48.751 | 57.44 |
| ctr_eb_dc1b_mem_mb | 4792.933 | 4813.2 |
| ctr_eb_dc2a_cpu_pct | 24.243 | 63.26 |
| ctr_eb_dc2a_mem_mb | 4196.702 | 4257.2 |
| ctr_eb_dc2b_cpu_pct | 45.309 | 889.36 |
| ctr_eb_dc2b_mem_mb | 7739.816 | 9147.6 |
| ctr_eb_etcd_dc1_cpu_pct | 2.063 | 2.31 |
| ctr_eb_etcd_dc1_mem_mb | 35.957 | 36.4 |
| ctr_eb_etcd_dc2_cpu_pct | 1.669 | 3.25 |
| ctr_eb_etcd_dc2_mem_mb | 33.059 | 35.8 |
| ctr_eb_etcd_w_cpu_pct | 1.829 | 3.25 |
| ctr_eb_etcd_w_mem_mb | 57.271 | 59.8 |
| ctr_eb_haproxy_cpu_pct | 43.846 | 93.75 |
| ctr_eb_haproxy_mem_mb | 35.178 | 37.1 |
| ctr_eb_mosquitto_cpu_pct | 18.062 | 42.34 |
| ctr_eb_mosquitto_mem_mb | 4.639 | 13.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.4 | 7.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.231 | 7.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.814 | 2.52 |
| ctr_eb_postgres_mem_mb | 1634.457 | 1635.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 40.937 | 1477.8 |
| host_cpu_pct | 33.926 | 79.1 |
| in_flight | 157348.735 | 193588 |
| nacks_per_s | 40.937 | 1477.8 |
| octopus_dispatcher_cores | 0.692 | 1.45 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 483579.102 | 645325 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 149403.347 | 194298 |
| octopus_mqtt_sent_total | 97857.755 | 137483 |
| octopus_publish_retries_total | 9484.776 | 23842 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.857 | 19 |
| pg_active_loads | 0.592 | 1 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 45.531 | 54 |
| pg_conn_idle | 221.551 | 228 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.122 | 2 |
| pg_conn_total | 267.082 | 268 |
| pg_db_size_mb | 1330.041 | 1470.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 24.374 | 32 |
| pg_replica_replay_lag_ms | 235.088 | 336.8 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 13789.043 | 15498.9 |
| pg_tup_del_per_s | 23495.718 | 29499.2 |
| pg_tup_fetch_per_s | 427457.078 | 484144.3 |
| pg_tup_ins_per_s | 42619.212 | 50868.9 |
| pg_tup_upd_per_s | 15612.406 | 17344.3 |
| pg_wal_mb_per_s | 79.354 | 89.37 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 5438.557 | 14166.7 |
| produced_total | 307798.184 | 397528 |
| publish_mb_per_s | 20.702 | 54.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 72,207 records are Completed |
| record_counts | pass | 72,207 records found, 72,207 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 265 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 432,964 events applied by workers, 432,964 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 55,059 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27984 ms, max 34346 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13205 seq 9: ready 197 ms before start, 165 ms of it with an idle slot in group 420
- [cross_source_blocking] source 9685 seq 10: ready 198 ms before start, 198 ms of it with an idle slot in group 336
- [cross_source_blocking] source 13605 seq 7: ready 156 ms before start, 156 ms of it with an idle slot in group 431
- [cross_source_blocking] source 5017 seq 7: ready 151 ms before start, 151 ms of it with an idle slot in group 173
- [cross_source_blocking] source 14883 seq 4: ready 147 ms before start, 147 ms of it with an idle slot in group 467
- [cross_source_blocking] source 13212 seq 13: ready 291 ms before start, 291 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9468 seq 12: ready 292 ms before start, 292 ms of it with an idle slot in group 327
- [cross_source_blocking] source 2933 seq 7: ready 452 ms before start, 452 ms of it with an idle slot in group 94
- [cross_source_blocking] source 15253 seq 7: ready 452 ms before start, 162 ms of it with an idle slot in group 476
- [cross_source_blocking] source 3033 seq 3: ready 223 ms before start, 223 ms of it with an idle slot in group 101
- [cross_source_blocking] source 4249 seq 7: ready 224 ms before start, 224 ms of it with an idle slot in group 143
- [cross_source_blocking] source 7093 seq 8: ready 160 ms before start, 160 ms of it with an idle slot in group 240
- [cross_source_blocking] source 605 seq 7: ready 162 ms before start, 162 ms of it with an idle slot in group 17
- [cross_source_blocking] source 8659 seq 6: ready 354 ms before start, 116 ms of it with an idle slot in group 300
- [cross_source_blocking] source 15115 seq 4: ready 353 ms before start, 102 ms of it with an idle slot in group 472
- [cross_source_blocking] source 8661 seq 4: ready 353 ms before start, 116 ms of it with an idle slot in group 300
- [cross_source_blocking] source 12351 seq 7: ready 550 ms before start, 113 ms of it with an idle slot in group 392
- [cross_source_blocking] source 12395 seq 5: ready 992 ms before start, 114 ms of it with an idle slot in group 393
- [cross_source_blocking] source 15267 seq 6: ready 406 ms before start, 112 ms of it with an idle slot in group 477
- [cross_source_blocking] source 6681 seq 5: ready 829 ms before start, 128 ms of it with an idle slot in group 227

## Error samples

- t-119L#2 src=3433 seq=4 op=Update rec=14744622727169 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3423 seq=4 op=Update rec=14701673054209 ev=2: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8923 seq=2 op=Update rec=38323993182209 ev=1: NpgsqlException: Exception while reading from stream
- t-468#2 src=14947 seq=31 op=Update rec=64196876173313 ev=20: NpgsqlException: Exception while reading from stream
- t-342#0 src=10041 seq=35 op=Update rec=43125766619139 ev=4: NpgsqlException: Exception while reading from stream
- t-380#0 src=11746 seq=20 op=Create rec=50448685858822 ev=0: NpgsqlException: Exception while reading from stream
- t-041#0 src=1304 seq=45 op=Update rec=5600637353990 ev=2: NpgsqlException: Exception while reading from stream
- t-215#0 src=6314 seq=12 op=Create rec=27118423506948 ev=0: NpgsqlException: Exception while reading from stream
- t-096#0 src=2957 seq=23 op=Complete rec=12700218294275 ev=7: NpgsqlException: Exception while reading from stream
- t-111#0 src=3307 seq=21 op=Update rec=14203456847874 ev=4: NpgsqlException: Exception while reading from stream
- t-285#0 src=8415 seq=12 op=Update rec=36142149795844 ev=2: NpgsqlException: Exception while reading from stream
- t-121#1 src=3576 seq=19 op=Get rec=15358803050501 ev=3: NpgsqlException: Exception while reading from stream
- t-152#0 src=4437 seq=9 op=Create rec=19056769892355 ev=0: NpgsqlException: Exception while reading from stream
- t-024#0 src=856 seq=15 op=Get rec=3676492005379 ev=1: NpgsqlException: Exception while reading from stream
- t-438#2 src=13787 seq=16 op=Get rec=59214714109953 ev=10: NpgsqlException: Exception while reading from stream
- t-486#0 src=15629 seq=21 op=Update rec=67126043869186 ev=3: NpgsqlException: Exception while reading from stream
- t-472#0 src=15112 seq=11 op=Complete rec=64905545777154 ev=3: NpgsqlException: Exception while reading from stream
- t-375#0 src=11533 seq=30 op=Update rec=49533857824772 ev=1: NpgsqlException: Exception while reading from stream
- t-473#0 src=15172 seq=10 op=Complete rec=65163243814914 ev=2: NpgsqlException: Exception while reading from stream
- t-112#0 src=3320 seq=24 op=Get rec=14259291422722 ev=2: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3381 | 3381 | 0 | 49545.215 | 62914.559 | 64487.423 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1624 | 1624 | 0 | 49020.927 | 62914.559 | 63438.847 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3371 | 3371 | 0 | 49020.927 | 62914.559 | 63700.991 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1555 | 1555 | 0 | 49020.927 | 62390.271 | 63176.703 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3240 | 3240 | 0 | 49283.071 | 62390.271 | 65011.711 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4758 | 4758 | 0 | 49283.071 | 62390.271 | 64487.423 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3180 | 3180 | 0 | 48496.639 | 62390.271 | 63963.135 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1524 | 1524 | 0 | 49283.071 | 62128.127 | 63438.847 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1578 | 1578 | 0 | 49020.927 | 62128.127 | 62914.559 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4632 | 4632 | 0 | 48758.783 | 62128.127 | 63438.847 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 4676 | 4676 | 0 | 48496.639 | 61865.983 | 62914.559 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1464 | 1464 | 0 | 48758.783 | 61865.983 | 62652.415 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1593 | 1593 | 0 | 49283.071 | 61865.983 | 63700.991 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1598 | 1598 | 0 | 49020.927 | 61865.983 | 63176.703 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1553 | 1553 | 0 | 48758.783 | 61603.839 | 62390.271 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1561 | 1561 | 0 | 48496.639 | 61603.839 | 61865.983 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1459 | 1459 | 0 | 48758.783 | 61603.839 | 62914.559 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1535 | 1535 | 0 | 48496.639 | 61603.839 | 62652.415 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1548 | 1548 | 0 | 48758.783 | 61603.839 | 62390.271 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3011 | 3011 | 0 | 48758.783 | 61341.695 | 63176.703 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1692 | 1692 | 0 | 49283.071 | 61341.695 | 62128.127 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1506 | 1506 | 0 | 48496.639 | 61079.551 | 61341.695 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3170 | 3170 | 0 | 48234.495 | 61079.551 | 64225.279 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4342 | 4342 | 0 | 47972.351 | 60555.263 | 61341.695 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 1431 | 1431 | 0 | 47972.351 | 60555.263 | 61603.839 |
