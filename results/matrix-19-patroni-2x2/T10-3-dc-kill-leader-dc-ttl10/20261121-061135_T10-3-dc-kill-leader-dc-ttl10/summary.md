# T10-3-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.356s, closing 2.539s, drain 22.883s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 423876 | 423876 | 5395.339 | 2516.165 | 18650.7 | 0 | 2031 | 2031 | 260 | 2154 | 42817 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 256220 | 38977.914 | 44761.087 | 54886.399 | 60784.639 | 63143.935 | 65896.447 |
| e2e_from_send | 256220 | 32471.071 | 40337.407 | 52199.423 | 58818.559 | 61472.767 | 63602.687 |
| producer_lag | 256220 | 6506.868 | 3522.559 | 28131.327 | 30081.023 | 30851.071 | 31637.503 |
| publish_call | 423876 | 265.718 | 146.431 | 276.991 | 481.535 | 24592.383 | 24756.223 |
| queue_wait | 256220 | 32342.146 | 40271.871 | 52133.887 | 58785.791 | 61439.999 | 63275.007 |
| broker_dwell | 256220 | 32013.799 | 40075.263 | 52002.815 | 58654.719 | 61308.927 | 63209.471 |
| receive_call | 386681 | 23.986 | 7.543 | 46.303 | 329.727 | 478.207 | 550.399 |
| ctx_load | 256220 | 8.285 | 5.711 | 16.831 | 47.007 | 92.287 | 1321.983 |
| process | 256220 | 10.994 | 0.002 | 0.004 | 0.062 | 2639.871 | 3014.655 |
| ctx_commit | 256220 | 109.544 | 38.559 | 318.463 | 781.311 | 1208.319 | 1586.175 |
| ack | 256199 | 0.008 | 0.004 | 0.007 | 0.045 | 0.645 | 28.735 |
| service | 256220 | 128.893 | 47.039 | 341.759 | 942.591 | 2748.415 | 4026.367 |
| e2e_large | 160 | 11262.138 | 6221.823 | 31637.503 | 33980.415 | 34177.023 | 34177.023 |
| ready_wait | 256220 | 1352.811 | 19.567 | 1128.447 | 39616.511 | 45973.503 | 58458.111 |
| blocked | 256220 | 862.505 | 15.615 | 324.863 | 26165.247 | 31145.983 | 33374.207 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2516.165 | 8432.8 |
| applied_total | 143468.367 | 179989 |
| bench_cpu_cores | 1.476 | 3.54 |
| bench_cpu_pct | 6.15 | 14.75 |
| bench_gc_gen0 | 1.041 | 4 |
| bench_gc_gen1 | 0.857 | 3 |
| bench_gc_gen2 | 0.306 | 2 |
| bench_gc_heap_mb | 1045.018 | 1386.7 |
| bench_mem_mb | 1742.224 | 1882.3 |
| bench_threadpool_queue | 36.163 | 912 |
| bench_threadpool_threads | 43.306 | 44 |
| broker_backlog | 161721.224 | 205284 |
| ctr_eb_dc1a_cpu_pct | 50.932 | 671.26 |
| ctr_eb_dc1a_mem_mb | 6239.282 | 7337.6 |
| ctr_eb_dc1b_cpu_pct | 24.652 | 69.03 |
| ctr_eb_dc1b_mem_mb | 4282.635 | 4546.2 |
| ctr_eb_dc2a_cpu_pct | 616.946 | 788.6 |
| ctr_eb_dc2a_mem_mb | 11865.086 | 12502.7 |
| ctr_eb_dc2b_cpu_pct | 45.59 | 53.53 |
| ctr_eb_dc2b_mem_mb | 3591.843 | 3791.8 |
| ctr_eb_etcd_dc1_cpu_pct | 1.636 | 2.88 |
| ctr_eb_etcd_dc1_mem_mb | 40.247 | 41.4 |
| ctr_eb_etcd_dc2_cpu_pct | 1.886 | 2.04 |
| ctr_eb_etcd_dc2_mem_mb | 53.048 | 54.4 |
| ctr_eb_etcd_w_cpu_pct | 2.054 | 3.2 |
| ctr_eb_etcd_w_mem_mb | 55.747 | 57.7 |
| ctr_eb_haproxy_cpu_pct | 46.41 | 101.03 |
| ctr_eb_haproxy_mem_mb | 33.588 | 36.5 |
| ctr_eb_mosquitto_cpu_pct | 19.407 | 42.98 |
| ctr_eb_mosquitto_mem_mb | 3.906 | 14.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.629 | 7.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.308 | 7.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.06 |
| ctr_eb_pgbouncer_4_mem_mb | 7.545 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.791 | 2.32 |
| ctr_eb_postgres_mem_mb | 1676.027 | 1683.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 41.433 | 1729.4 |
| host_cpu_pct | 32.601 | 74.91 |
| in_flight | 168216.286 | 209905 |
| nacks_per_s | 41.433 | 1729.4 |
| octopus_dispatcher_cores | 0.682 | 1.44 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 478204.429 | 625215 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 147324.776 | 192740 |
| octopus_mqtt_sent_total | 90578.735 | 124938 |
| octopus_publish_retries_total | 9569.327 | 24037 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.122 | 19 |
| pg_active_loads | 2.755 | 11 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 42.49 | 52 |
| pg_conn_idle | 224.51 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 267 | 267 |
| pg_db_size_mb | 1312.141 | 1453.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 6.64 | 25.44 |
| pg_replica_replay_lag_ms | 94.359 | 297 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 15881.427 | 18151 |
| pg_tup_del_per_s | 25778.896 | 28152.6 |
| pg_tup_fetch_per_s | 495945.263 | 610017.9 |
| pg_tup_ins_per_s | 46585.429 | 52912.5 |
| pg_tup_upd_per_s | 17986.914 | 21157.2 |
| pg_wal_mb_per_s | 81.366 | 95.26 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 5395.339 | 15013.1 |
| produced_total | 311684.653 | 389894 |
| publish_mb_per_s | 20.741 | 64.62 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 71,105 records are Completed |
| record_counts | pass | 71,105 records found, 71,105 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 260 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 423,876 events applied by workers, 423,876 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 42,817 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26165 ms, max 33373 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6396 seq 8: ready 194 ms before start, 194 ms of it with an idle slot in group 219
- [cross_source_blocking] source 3331 seq 5: ready 201 ms before start, 201 ms of it with an idle slot in group 113
- [cross_source_blocking] source 8475 seq 16: ready 104 ms before start, 104 ms of it with an idle slot in group 289
- [cross_source_blocking] source 15811 seq 7: ready 185 ms before start, 185 ms of it with an idle slot in group 495
- [cross_source_blocking] source 6187 seq 6: ready 106 ms before start, 106 ms of it with an idle slot in group 211
- [cross_source_blocking] source 8619 seq 13: ready 184 ms before start, 184 ms of it with an idle slot in group 298
- [cross_source_blocking] source 7388 seq 3: ready 103 ms before start, 103 ms of it with an idle slot in group 254
- [cross_source_blocking] source 4028 seq 2: ready 104 ms before start, 104 ms of it with an idle slot in group 135
- [cross_source_blocking] source 4235 seq 5: ready 106 ms before start, 106 ms of it with an idle slot in group 142
- [cross_source_blocking] source 13605 seq 7: ready 108 ms before start, 108 ms of it with an idle slot in group 431
- [cross_source_blocking] source 4383 seq 6: ready 185 ms before start, 185 ms of it with an idle slot in group 150
- [cross_source_blocking] source 2979 seq 5: ready 201 ms before start, 201 ms of it with an idle slot in group 98
- [cross_source_blocking] source 7391 seq 7: ready 185 ms before start, 185 ms of it with an idle slot in group 254
- [cross_source_blocking] source 5067 seq 4: ready 185 ms before start, 185 ms of it with an idle slot in group 177
- [cross_source_blocking] source 14631 seq 5: ready 106 ms before start, 106 ms of it with an idle slot in group 463
- [cross_source_blocking] source 7100 seq 4: ready 103 ms before start, 103 ms of it with an idle slot in group 241
- [cross_source_blocking] source 13989 seq 5: ready 140 ms before start, 140 ms of it with an idle slot in group 445
- [cross_source_blocking] source 1347 seq 7: ready 329 ms before start, 162 ms of it with an idle slot in group 43
- [cross_source_blocking] source 15363 seq 8: ready 203 ms before start, 111 ms of it with an idle slot in group 480
- [cross_source_blocking] source 13187 seq 7: ready 166 ms before start, 166 ms of it with an idle slot in group 419

## Error samples

- t-383#3 src=11959 seq=10 op=Complete rec=51363513892866 ev=4: NpgsqlException: Exception while reading from stream
- t-299#0 src=8642 seq=15 op=Update rec=37117107372036 ev=2: NpgsqlException: Exception while reading from stream
- t-003#0 src=161 seq=13 op=Create rec=691489734660 ev=0: NpgsqlException: Exception while reading from stream
- t-458#0 src=14377 seq=3 op=Complete rec=61748744814593 ev=2: NpgsqlException: Exception while reading from stream
- t-028#0 src=942 seq=18 op=Get rec=4045859192834 ev=2: NpgsqlException: Exception while reading from stream
- t-183#0 src=5374 seq=7 op=Update rec=23081154248705 ev=5: NpgsqlException: Exception while reading from stream
- t-314#1 src=9096 seq=27 op=Get rec=39067022524420 ev=3: NpgsqlException: Exception while reading from stream
- t-106#1 src=3200 seq=29 op=Update rec=13743895347204 ev=3: NpgsqlException: Exception while reading from stream
- t-212#0 src=6223 seq=12 op=Update rec=26727581483009 ev=8: NpgsqlException: Exception while reading from stream
- t-061#1 src=2070 seq=5 op=Update rec=8890582302721 ev=3: NpgsqlException: Exception while reading from stream
- t-317#0 src=9222 seq=13 op=Update rec=39608188403713 ev=9: NpgsqlException: Exception while reading from stream
- t-383#3 src=11977 seq=11 op=Update rec=51440823304194 ev=1: NpgsqlException: Exception while reading from stream
- t-390#0 src=12270 seq=11 op=Update rec=52699248721921 ev=7: NpgsqlException: Exception while reading from stream
- t-261#0 src=7609 seq=13 op=Complete rec=32680406155267 ev=6: NpgsqlException: Exception while reading from stream
- t-214#0 src=6289 seq=8 op=Update rec=27011049324546 ev=1: NpgsqlException: Exception while reading from stream
- t-029#0 src=988 seq=25 op=Update rec=4243427688451 ev=3: NpgsqlException: Exception while reading from stream
- t-444#0 src=13976 seq=15 op=Create rec=60026462928899 ev=0: NpgsqlException: Exception while reading from stream
- t-361#0 src=11041 seq=10 op=Update rec=47420733915137 ev=8: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8927 seq=1 op=Create rec=38341173051393 ev=0: NpgsqlException: Exception while reading from stream
- t-219#0 src=6403 seq=23 op=Complete rec=27500675596290 ev=12: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3303 | 3303 | 0 | 50069.503 | 63700.991 | 64487.423 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1600 | 1600 | 0 | 49545.215 | 63700.991 | 64487.423 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1496 | 1496 | 0 | 50593.791 | 63438.847 | 63963.135 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3293 | 3293 | 0 | 50069.503 | 63438.847 | 64749.567 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4531 | 4531 | 0 | 50069.503 | 63438.847 | 63963.135 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1526 | 1526 | 0 | 49807.359 | 63176.703 | 63963.135 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3170 | 3170 | 0 | 50069.503 | 63176.703 | 66060.287 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4661 | 4661 | 0 | 50331.647 | 63176.703 | 64487.423 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1442 | 1442 | 0 | 49807.359 | 62914.559 | 63963.135 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1537 | 1537 | 0 | 49545.215 | 62914.559 | 63963.135 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 4584 | 4584 | 0 | 49283.071 | 62652.415 | 63438.847 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6180 | 6180 | 0 | 49807.359 | 62652.415 | 64487.423 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1563 | 1563 | 0 | 50069.503 | 62652.415 | 63176.703 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3103 | 3103 | 0 | 50069.503 | 62652.415 | 63700.991 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3100 | 3100 | 0 | 49545.215 | 62652.415 | 64225.279 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 2943 | 2943 | 0 | 49545.215 | 62652.415 | 63700.991 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1566 | 1566 | 0 | 49545.215 | 62652.415 | 63700.991 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4341 | 4341 | 0 | 49545.215 | 62652.415 | 64225.279 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1523 | 1523 | 0 | 49545.215 | 62390.271 | 62914.559 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1434 | 1434 | 0 | 49807.359 | 62390.271 | 63176.703 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1520 | 1520 | 0 | 49545.215 | 62128.127 | 62914.559 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1504 | 1504 | 0 | 49283.071 | 62128.127 | 63176.703 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1500 | 1500 | 0 | 49283.071 | 62128.127 | 63438.847 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 2858 | 2858 | 0 | 49283.071 | 62128.127 | 63700.991 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1480 | 1480 | 0 | 49545.215 | 61865.983 | 62128.127 |
