# F1-dc-freeze:leader:60 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 115.957s, closing 2.908s, drain 9.548s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 447193 | 447193 | 2566.699 | 1992.189 | 18260.5 | 0 | 1654 | 1654 | 268 | 2269 | 124830 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 277724 | 40383.771 | 9445.375 | 99680.255 | 105644.031 | 108003.327 | 111149.055 |
| e2e_from_send | 277724 | 40292.493 | 9363.455 | 99680.255 | 105644.031 | 107937.791 | 110755.839 |
| producer_lag | 279164 | 90.932 | 2.629 | 79.807 | 365.567 | 624.639 | 84279.295 |
| publish_call | 447193 | 428.55 | 65.535 | 148.351 | 347.391 | 83623.935 | 84279.295 |
| queue_wait | 277724 | 40204.518 | 9248.767 | 99614.719 | 105578.495 | 107872.255 | 110559.231 |
| broker_dwell | 277724 | 39585.719 | 8937.471 | 99549.183 | 105512.959 | 107806.719 | 110428.159 |
| receive_call | 396579 | 32.03 | 8.711 | 84.991 | 351.487 | 483.071 | 562.175 |
| ctx_load | 277724 | 12.83 | 7.991 | 30.335 | 65.919 | 95.359 | 148.095 |
| process | 277724 | 10.279 | 0.002 | 0.004 | 0.056 | 2594.815 | 3037.183 |
| ctx_commit | 277724 | 64.781 | 55.583 | 121.215 | 247.551 | 630.783 | 941.055 |
| ack | 277715 | 0.01 | 0.004 | 0.007 | 0.048 | 1.087 | 41.183 |
| service | 277724 | 87.968 | 68.927 | 146.047 | 283.903 | 2686.975 | 3289.087 |
| e2e_large | 168 | 10590.787 | 3012.607 | 6082.559 | 88145.919 | 90636.287 | 90636.287 |
| ready_wait | 277724 | 3157.13 | 90.239 | 1680.383 | 91291.647 | 94765.055 | 98959.359 |
| blocked | 277724 | 1158.832 | 58.751 | 912.383 | 27557.887 | 72548.351 | 84213.759 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 1992.189 | 14125 |
| applied_total | 283929.481 | 304753 |
| bench_cpu_cores | 1.077 | 4.991 |
| bench_cpu_pct | 4.488 | 20.8 |
| bench_gc_gen0 | 0.481 | 3 |
| bench_gc_gen1 | 0.192 | 2 |
| bench_gc_gen2 | 0.115 | 1 |
| bench_gc_heap_mb | 1297.747 | 1477.5 |
| bench_mem_mb | 1886.315 | 1941.4 |
| bench_threadpool_queue | 16.538 | 314 |
| bench_threadpool_threads | 36.817 | 44 |
| broker_backlog | 94547.298 | 97690 |
| ctr_eb_dc1a_cpu_pct | 779.041 | 846.12 |
| ctr_eb_dc1a_mem_mb | 5883.073 | 6158.8 |
| ctr_eb_dc1b_cpu_pct | 52.616 | 66.12 |
| ctr_eb_dc1b_mem_mb | 3837.527 | 4682.5 |
| ctr_eb_dc2a_cpu_pct | 11.856 | 64.88 |
| ctr_eb_dc2a_mem_mb | 2500.205 | 2727.5 |
| ctr_eb_dc2b_cpu_pct | 13.693 | 59.95 |
| ctr_eb_dc2b_mem_mb | 4780.068 | 6090.2 |
| ctr_eb_etcd_dc1_cpu_pct | 1.984 | 2.43 |
| ctr_eb_etcd_dc1_mem_mb | 29.991 | 31 |
| ctr_eb_etcd_dc2_cpu_pct | 0.766 | 1.83 |
| ctr_eb_etcd_dc2_mem_mb | 24.52 | 25.3 |
| ctr_eb_etcd_w_cpu_pct | 1.131 | 2.84 |
| ctr_eb_etcd_w_mem_mb | 58.693 | 60.3 |
| ctr_eb_haproxy_cpu_pct | 21.351 | 103.65 |
| ctr_eb_haproxy_mem_mb | 28.156 | 32.4 |
| ctr_eb_mosquitto_cpu_pct | 13.433 | 62.73 |
| ctr_eb_mosquitto_mem_mb | 4.562 | 20.7 |
| ctr_eb_mssql_cpu_pct | 1.494 | 3.65 |
| ctr_eb_mssql_mem_mb | 4772.535 | 4818 |
| ctr_eb_mysql_cpu_pct | 0.404 | 0.95 |
| ctr_eb_mysql_mem_mb | 6037.051 | 6081.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 3.104 | 3.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 2.408 | 2.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 3.019 | 3.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.08 |
| ctr_eb_pgbouncer_mem_mb | 3.104 | 3.2 |
| ctr_eb_postgres_cpu_pct | 0.576 | 2.78 |
| ctr_eb_postgres_mem_mb | 1539.238 | 1566.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 15.92 | 1553.8 |
| host_cpu_pct | 16.425 | 80.65 |
| in_flight | 104168.087 | 108881 |
| nacks_per_s | 15.92 | 1553.8 |
| octopus_dispatcher_cores | 0.717 | 1.4 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 686025.615 | 779614 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 175577.087 | 193732 |
| octopus_mqtt_sent_total | 182519.163 | 211422 |
| octopus_publish_retries_total | 4847.019 | 9335 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.644 | 19 |
| pg_active_loads | 2.106 | 44 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 67.058 | 86 |
| pg_conn_idle | 198.029 | 229 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.25 | 22 |
| pg_conn_total | 265.087 | 268 |
| pg_db_size_mb | 1402.233 | 1530.1 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 11.747 | 67.83 |
| pg_replica_replay_lag_ms | 851.726 | 82466.4 |
| pg_replicas | 2.981 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22566.252 | 25402.4 |
| pg_tup_del_per_s | 22806.787 | 31880.7 |
| pg_tup_fetch_per_s | 178003.166 | 205664.8 |
| pg_tup_ins_per_s | 34202.143 | 47419.7 |
| pg_tup_upd_per_s | 28583.744 | 31150.1 |
| pg_wal_mb_per_s | 79.986 | 110.28 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.154 | 1 |
| produced_per_s | 2566.699 | 16581.7 |
| produced_total | 388097.567 | 413634 |
| publish_mb_per_s | 9.574 | 78.86 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,017 records are Completed |
| record_counts | pass | 74,017 records found, 74,017 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 268 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 447,193 events applied by workers, 447,193 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 124,830 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27558 ms, max 84150 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8131 seq 10: ready 104 ms before start, 104 ms of it with an idle slot in group 275
- [cross_source_blocking] source 2880 seq 4: ready 160 ms before start, 148 ms of it with an idle slot in group 91
- [cross_source_blocking] source 7316 seq 8: ready 161 ms before start, 161 ms of it with an idle slot in group 251
- [cross_source_blocking] source 12886 seq 7: ready 117 ms before start, 117 ms of it with an idle slot in group 408
- [cross_source_blocking] source 13014 seq 14: ready 119 ms before start, 119 ms of it with an idle slot in group 414
- [cross_source_blocking] source 10627 seq 8: ready 119 ms before start, 119 ms of it with an idle slot in group 349
- [cross_source_blocking] source 4455 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 153
- [cross_source_blocking] source 2935 seq 4: ready 111 ms before start, 111 ms of it with an idle slot in group 94
- [cross_source_blocking] source 1975 seq 11: ready 111 ms before start, 111 ms of it with an idle slot in group 57
- [cross_source_blocking] source 15247 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 476
- [cross_source_blocking] source 12662 seq 8: ready 167 ms before start, 167 ms of it with an idle slot in group 398
- [cross_source_blocking] source 14894 seq 6: ready 173 ms before start, 173 ms of it with an idle slot in group 467
- [cross_source_blocking] source 15635 seq 16: ready 119 ms before start, 119 ms of it with an idle slot in group 487
- [cross_source_blocking] source 13212 seq 13: ready 122 ms before start, 122 ms of it with an idle slot in group 421
- [cross_source_blocking] source 404 seq 12: ready 156 ms before start, 156 ms of it with an idle slot in group 12
- [cross_source_blocking] source 314 seq 2: ready 169 ms before start, 169 ms of it with an idle slot in group 9
- [cross_source_blocking] source 6854 seq 4: ready 169 ms before start, 130 ms of it with an idle slot in group 233
- [cross_source_blocking] source 12022 seq 8: ready 130 ms before start, 130 ms of it with an idle slot in group 384
- [cross_source_blocking] source 4210 seq 3: ready 170 ms before start, 170 ms of it with an idle slot in group 139
- [cross_source_blocking] source 8704 seq 10: ready 163 ms before start, 163 ms of it with an idle slot in group 302

## Error samples

- t-092#0 src=2904 seq=21 op=Get rec=12472585027586 ev=3: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-342#0 src=10040 seq=56 op=Update rec=43121471651847 ev=8: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-136#0 src=4054 seq=11 op=Create rec=17411797417987 ev=0: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-350#1 src=10642 seq=19 op=Get rec=45707041964035 ev=7: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-342#0 src=10038 seq=54 op=Create rec=43112881717257 ev=0: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-469#2 src=14991 seq=23 op=Update rec=64385854734341 ev=1: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-315#1 src=9125 seq=19 op=Create rec=39191576576004 ev=0: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-363#0 src=11133 seq=43 op=Update rec=47815870906372 ev=13: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-116#0 src=3356 seq=22 op=Update rec=14413910245380 ev=5: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-361#0 src=11072 seq=15 op=Get rec=47553877901315 ev=4: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-319L#1 src=9259 seq=4 op=Update rec=39767102193665 ev=2: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#0 src=3458 seq=2 op=Update rec=14851996909569 ev=1: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#3 src=3432 seq=4 op=Update rec=14740327759873 ev=3: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#2 src=3485 seq=3 op=Update rec=14967961026561 ev=2: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#3 src=3439 seq=5 op=Update rec=14770392530946 ev=1: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#1 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#1 src=3436 seq=4 op=Update rec=14757507629057 ev=2: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#3 src=3474 seq=3 op=Update rec=14920716386305 ev=1: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#1 src=3433 seq=5 op=Update rec=14744622727169 ev=2: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.
- t-119L#0 src=3465 seq=4 op=Update rec=14882061680641 ev=1: NpgsqlException: The connection pool has been exhausted, either raise 'Max Pool Size' (currently 200) or 'Timeout' (currently 15 seconds) in your connection string.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1570 | 1570 | 0 | 97517.567 | 109576.191 | 111149.055 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3338 | 3338 | 0 | 96993.279 | 109051.903 | 109576.191 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1669 | 1669 | 0 | 97517.567 | 109051.903 | 109576.191 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1613 | 1613 | 0 | 95420.415 | 108527.615 | 109051.903 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3480 | 3480 | 0 | 96993.279 | 108527.615 | 110100.479 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4923 | 4923 | 0 | 96468.991 | 108527.615 | 110100.479 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3487 | 3487 | 0 | 96468.991 | 108003.327 | 108527.615 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6493 | 6493 | 0 | 96993.279 | 107479.039 | 109051.903 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3277 | 3277 | 0 | 96993.279 | 107479.039 | 109051.903 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3285 | 3285 | 0 | 95944.703 | 107479.039 | 108003.327 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4789 | 4789 | 0 | 95944.703 | 107479.039 | 107479.039 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1588 | 1588 | 0 | 95420.415 | 106954.751 | 108003.327 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1607 | 1607 | 0 | 94896.127 | 106954.751 | 108527.615 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1638 | 1638 | 0 | 95944.703 | 106954.751 | 108527.615 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1593 | 1593 | 0 | 95420.415 | 106954.751 | 107479.039 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1726 | 1726 | 0 | 94896.127 | 106954.751 | 109051.903 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1650 | 1650 | 0 | 95420.415 | 106954.751 | 108527.615 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1581 | 1581 | 0 | 95944.703 | 106954.751 | 107479.039 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6645 | 6645 | 0 | 95944.703 | 106954.751 | 108527.615 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1547 | 1547 | 0 | 93323.263 | 106430.463 | 106954.751 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1512 | 1512 | 0 | 95420.415 | 106430.463 | 107479.039 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4514 | 4514 | 0 | 94371.839 | 106430.463 | 107479.039 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 4398 | 4398 | 0 | 93847.551 | 106430.463 | 107479.039 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3017 | 3017 | 0 | 95420.415 | 106430.463 | 106954.751 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7414 | 7414 | 0 | 94896.127 | 105906.175 | 106954.751 |
