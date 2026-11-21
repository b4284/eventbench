# H08-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.552s, closing 2.204s, drain 16.016s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 516206 | 516206 | 6897.396 | 4836.782 | 18957.8 | 0 | 1900 | 1900 | 290 | 2596 | 108028 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 346979 | 24887.894 | 29376.511 | 44859.391 | 52363.263 | 54984.703 | 57573.375 |
| e2e_from_send | 346979 | 18684.1 | 10821.631 | 43778.047 | 50954.239 | 53706.751 | 56819.711 |
| producer_lag | 347199 | 6200.558 | 1320.959 | 23232.511 | 24526.847 | 25493.503 | 25903.103 |
| publish_call | 516206 | 208.171 | 104.319 | 238.591 | 583.679 | 22593.535 | 22773.759 |
| queue_wait | 346979 | 18573.228 | 10723.327 | 43745.279 | 50921.471 | 53673.983 | 55705.599 |
| broker_dwell | 346979 | 18402.711 | 10534.911 | 43614.207 | 50790.399 | 53542.911 | 55705.599 |
| receive_call | 458731 | 30.056 | 8.095 | 75.007 | 358.399 | 485.119 | 547.327 |
| ctx_load | 346979 | 10.002 | 6.095 | 22.559 | 56.703 | 87.487 | 186.495 |
| process | 346979 | 10.659 | 0.002 | 0.004 | 0.057 | 2623.487 | 3008.511 |
| ctx_commit | 346979 | 90.091 | 59.007 | 202.623 | 515.583 | 838.655 | 892.927 |
| ack | 346967 | 0.009 | 0.004 | 0.007 | 0.043 | 0.658 | 92.031 |
| service | 346979 | 110.853 | 69.247 | 221.439 | 603.647 | 2729.983 | 3743.743 |
| e2e_large | 189 | 9972.359 | 5103.615 | 26705.919 | 29736.959 | 33046.527 | 33046.527 |
| ready_wait | 346979 | 1411.996 | 42.943 | 1734.655 | 36208.639 | 42958.847 | 50921.471 |
| blocked | 346979 | 977.421 | 27.791 | 893.951 | 25509.887 | 30556.159 | 33423.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4836.782 | 11810.9 |
| applied_total | 234633.388 | 328350 |
| bench_cpu_cores | 1.936 | 4.005 |
| bench_cpu_pct | 8.066 | 16.69 |
| bench_gc_gen0 | 1.224 | 3 |
| bench_gc_gen1 | 0.571 | 2 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 1244.696 | 1611.4 |
| bench_mem_mb | 1916.539 | 2117.7 |
| bench_threadpool_queue | 33.878 | 672 |
| bench_threadpool_threads | 43.551 | 44 |
| broker_backlog | 115115.98 | 150457 |
| ctr_eb_dc1a_cpu_pct | 819.984 | 853.36 |
| ctr_eb_dc1a_mem_mb | 10506.873 | 11415.5 |
| ctr_eb_dc1b_cpu_pct | 47.685 | 52.86 |
| ctr_eb_dc1b_mem_mb | 3461.9 | 3639.2 |
| ctr_eb_dc2a_cpu_pct | 28.106 | 68.88 |
| ctr_eb_dc2a_mem_mb | 6016.476 | 7269.9 |
| ctr_eb_dc2b_cpu_pct | 107.647 | 1029.74 |
| ctr_eb_dc2b_mem_mb | 3298.961 | 4630.1 |
| ctr_eb_etcd_dc1_cpu_pct | 1.53 | 1.72 |
| ctr_eb_etcd_dc1_mem_mb | 37.268 | 37.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.314 | 2.52 |
| ctr_eb_etcd_dc2_mem_mb | 36.627 | 38 |
| ctr_eb_etcd_w_cpu_pct | 1.735 | 3.09 |
| ctr_eb_etcd_w_mem_mb | 66.704 | 68 |
| ctr_eb_haproxy_cpu_pct | 64.178 | 111.12 |
| ctr_eb_haproxy_mem_mb | 34.182 | 36.7 |
| ctr_eb_mosquitto_cpu_pct | 26.419 | 55 |
| ctr_eb_mosquitto_mem_mb | 5.159 | 11.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.841 | 8.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 8.831 | 8.9 |
| ctr_eb_postgres_cpu_pct | 0.733 | 2.67 |
| ctr_eb_postgres_mem_mb | 1787.637 | 1803.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 38.735 | 1779.1 |
| host_cpu_pct | 42.527 | 81.59 |
| in_flight | 117043.918 | 151487 |
| nacks_per_s | 38.735 | 1779.1 |
| octopus_dispatcher_cores | 0.856 | 1.57 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 605862.122 | 861256 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 157233.041 | 219528 |
| octopus_mqtt_sent_total | 150153.245 | 220106 |
| octopus_publish_retries_total | 9848.571 | 25592 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.388 | 19 |
| pg_active_loads | 6.837 | 49 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 55.633 | 74 |
| pg_conn_idle | 213.204 | 240 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 268.837 | 269 |
| pg_db_size_mb | 1420.237 | 1596.5 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 1.924 | 17.42 |
| pg_replica_replay_lag_ms | 33.951 | 131.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16226.38 | 26414.2 |
| pg_tup_del_per_s | 25357.849 | 37655.4 |
| pg_tup_fetch_per_s | 479669.922 | 544732.6 |
| pg_tup_ins_per_s | 43804.314 | 61464.9 |
| pg_tup_upd_per_s | 18094.698 | 31151.3 |
| pg_wal_mb_per_s | 106.439 | 115.47 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6897.396 | 18308.2 |
| produced_total | 351677.306 | 479837 |
| publish_mb_per_s | 24.441 | 66.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,395 records are Completed |
| record_counts | pass | 82,395 records found, 82,395 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 290 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 516,206 events applied by workers, 516,206 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 108,028 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 25510 ms, max 33418 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8704 seq 10: ready 141 ms before start, 141 ms of it with an idle slot in group 302
- [cross_source_blocking] source 6687 seq 8: ready 176 ms before start, 161 ms of it with an idle slot in group 228
- [cross_source_blocking] source 13020 seq 9: ready 179 ms before start, 179 ms of it with an idle slot in group 414
- [cross_source_blocking] source 13984 seq 10: ready 228 ms before start, 228 ms of it with an idle slot in group 444
- [cross_source_blocking] source 14877 seq 7: ready 213 ms before start, 213 ms of it with an idle slot in group 467
- [cross_source_blocking] source 1002 seq 6: ready 255 ms before start, 255 ms of it with an idle slot in group 30
- [cross_source_blocking] source 353 seq 4: ready 218 ms before start, 218 ms of it with an idle slot in group 10
- [cross_source_blocking] source 8096 seq 18: ready 183 ms before start, 183 ms of it with an idle slot in group 273
- [cross_source_blocking] source 1964 seq 10: ready 188 ms before start, 188 ms of it with an idle slot in group 56
- [cross_source_blocking] source 12108 seq 8: ready 254 ms before start, 254 ms of it with an idle slot in group 387
- [cross_source_blocking] source 12392 seq 6: ready 254 ms before start, 254 ms of it with an idle slot in group 393
- [cross_source_blocking] source 13014 seq 14: ready 262 ms before start, 262 ms of it with an idle slot in group 414
- [cross_source_blocking] source 7394 seq 5: ready 266 ms before start, 266 ms of it with an idle slot in group 255
- [cross_source_blocking] source 9533 seq 5: ready 254 ms before start, 254 ms of it with an idle slot in group 331
- [cross_source_blocking] source 8098 seq 25: ready 218 ms before start, 218 ms of it with an idle slot in group 273
- [cross_source_blocking] source 8475 seq 16: ready 263 ms before start, 263 ms of it with an idle slot in group 289
- [cross_source_blocking] source 12723 seq 7: ready 235 ms before start, 235 ms of it with an idle slot in group 401
- [cross_source_blocking] source 11212 seq 8: ready 183 ms before start, 183 ms of it with an idle slot in group 365
- [cross_source_blocking] source 7424 seq 19: ready 117 ms before start, 117 ms of it with an idle slot in group 257
- [cross_source_blocking] source 4709 seq 3: ready 164 ms before start, 164 ms of it with an idle slot in group 162

## Error samples

- t-096#0 src=2964 seq=22 op=Update rec=12730283065347 ev=4: NpgsqlException: Exception while reading from stream
- t-416#0 src=13052 seq=12 op=Update rec=56057913147395 ev=1: NpgsqlException: Exception while reading from stream
- t-446#0 src=14033 seq=13 op=Update rec=60271276064771 ev=2: NpgsqlException: Exception while reading from stream
- t-282#0 src=8290 seq=30 op=Update rec=35605278883845 ev=2: NpgsqlException: Exception while reading from stream
- t-088#0 src=2805 seq=16 op=Update rec=12047383265282 ev=4: NpgsqlException: Exception while reading from stream
- t-020#0 src=694 seq=26 op=Update rec=2980707303430 ev=1: NpgsqlException: Exception while reading from stream
- t-109#0 src=3283 seq=21 op=Update rec=14100377632771 ev=4: NpgsqlException: Exception while reading from stream
- t-498#0 src=15959 seq=20 op=Update rec=68543383076868 ev=2: NpgsqlException: Exception while reading from stream
- t-271#1 src=8046 seq=32 op=Create rec=34557306863621 ev=0: NpgsqlException: Exception while reading from stream
- t-292#0 src=8512 seq=26 op=Update rec=36558761623555 ev=4: NpgsqlException: Exception while reading from stream
- t-409#0 src=12946 seq=24 op=Create rec=55602646614023 ev=0: NpgsqlException: Exception while reading from stream
- t-028#0 src=939 seq=14 op=Update rec=4032974290947 ev=2: NpgsqlException: Exception while reading from stream
- t-353#0 src=10790 seq=19 op=Complete rec=46342697123843 ev=3: NpgsqlException: Exception while reading from stream
- t-049#0 src=1678 seq=12 op=Get rec=7206955122690 ev=3: NpgsqlException: Exception while reading from stream
- t-171#0 src=4978 seq=38 op=Update rec=21380347199493 ev=5: NpgsqlException: Exception while reading from stream
- t-415#0 src=13026 seq=34 op=Update rec=55946243997701 ev=5: NpgsqlException: Exception while reading from stream
- t-200#0 src=5819 seq=11 op=Update rec=24992414695426 ev=3: NpgsqlException: Exception while reading from stream
- t-092#0 src=2906 seq=13 op=Update rec=12481174962179 ev=2: NpgsqlException: Exception while reading from stream
- t-032#0 src=1028 seq=8 op=Get rec=4415226380291 ev=1: NpgsqlException: Exception while reading from stream
- t-060#0 src=2018 seq=25 op=Complete rec=8667244003331 ev=9: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4037 | 4037 | 0 | 41680.895 | 56360.959 | 57671.679 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1821 | 1821 | 0 | 41156.607 | 55836.671 | 56885.247 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1914 | 1914 | 0 | 40894.463 | 55836.671 | 57147.391 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1850 | 1850 | 0 | 40370.175 | 55574.527 | 57147.391 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4033 | 4033 | 0 | 41156.607 | 55312.383 | 56098.815 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7697 | 7697 | 0 | 40894.463 | 55050.239 | 56623.103 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3803 | 3803 | 0 | 41156.607 | 54525.951 | 56360.959 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1859 | 1859 | 0 | 41156.607 | 54525.951 | 57147.391 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1838 | 1838 | 0 | 40370.175 | 54525.951 | 55836.671 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1963 | 1963 | 0 | 41680.895 | 54525.951 | 54788.095 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5563 | 5563 | 0 | 40108.031 | 54263.807 | 55312.383 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7552 | 7552 | 0 | 40632.319 | 54263.807 | 56098.815 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5720 | 5720 | 0 | 40632.319 | 54263.807 | 55836.671 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1824 | 1824 | 0 | 40370.175 | 54001.663 | 54788.095 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3764 | 3764 | 0 | 39845.887 | 54001.663 | 54525.951 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1864 | 1864 | 0 | 39583.743 | 54001.663 | 54788.095 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1860 | 1860 | 0 | 39321.599 | 53739.519 | 54263.807 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1711 | 1711 | 0 | 39583.743 | 53739.519 | 55836.671 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1913 | 1913 | 0 | 39059.455 | 53739.519 | 55050.239 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3775 | 3775 | 0 | 41156.607 | 53477.375 | 55050.239 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5510 | 5510 | 0 | 39583.743 | 53477.375 | 54263.807 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1855 | 1855 | 0 | 39059.455 | 52953.087 | 53215.231 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1832 | 1832 | 0 | 38797.311 | 52953.087 | 54263.807 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3592 | 3592 | 0 | 39321.599 | 52690.943 | 53739.519 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5220 | 5220 | 0 | 39059.455 | 52690.943 | 55050.239 |
