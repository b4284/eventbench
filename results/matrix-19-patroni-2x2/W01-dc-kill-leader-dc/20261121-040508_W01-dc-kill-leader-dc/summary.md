# W01-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 61.705s, closing 2.459s, drain 24.853s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 432663 | 432663 | 5862.978 | 4183.272 | 18359.5 | 0 | 2846 | 2846 | 262 | 2182 | 104847 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 264822 | 29097.344 | 34078.719 | 49119.231 | 57933.823 | 67239.935 | 73662.463 |
| e2e_from_send | 264822 | 19688.569 | 12541.951 | 43909.119 | 53084.159 | 63307.775 | 70516.735 |
| producer_lag | 264822 | 9408.814 | 5103.615 | 27803.647 | 29917.183 | 30867.455 | 33112.063 |
| publish_call | 432663 | 261.164 | 129.663 | 311.039 | 1097.727 | 21921.791 | 24002.559 |
| queue_wait | 264822 | 19556.48 | 12451.839 | 43810.815 | 52854.783 | 62554.111 | 70451.199 |
| broker_dwell | 264822 | 19260.297 | 12091.391 | 43646.975 | 52723.711 | 62357.503 | 70189.055 |
| receive_call | 378260 | 36.568 | 7.307 | 113.983 | 402.431 | 491.519 | 585.727 |
| ctx_load | 264822 | 25.769 | 3.947 | 18.319 | 60.447 | 15007.743 | 15032.319 |
| process | 264822 | 10.845 | 0.002 | 0.004 | 0.058 | 2631.679 | 3061.759 |
| ctx_commit | 264822 | 95.36 | 55.519 | 194.431 | 481.791 | 2562.047 | 2678.783 |
| ack | 264201 | 0.008 | 0.003 | 0.005 | 0.03 | 0.437 | 72.575 |
| service | 264822 | 132.07 | 63.871 | 211.071 | 1750.015 | 15007.743 | 17989.631 |
| e2e_large | 162 | 14858.202 | 9043.967 | 32194.559 | 34963.455 | 35717.119 | 35717.119 |
| ready_wait | 264822 | 1628.533 | 68.159 | 2046.975 | 36274.175 | 43286.527 | 55541.759 |
| blocked | 264822 | 1192.591 | 38.143 | 1138.687 | 27099.135 | 30310.399 | 32997.375 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4183.272 | 12330.2 |
| applied_total | 204429.02 | 286407 |
| bench_cpu_cores | 1.917 | 4.015 |
| bench_cpu_pct | 7.989 | 16.73 |
| bench_gc_gen0 | 1.12 | 4 |
| bench_gc_gen1 | 0.46 | 2 |
| bench_gc_gen2 | 0.38 | 2 |
| bench_gc_heap_mb | 1198.14 | 1473.5 |
| bench_mem_mb | 1974.22 | 2196.2 |
| bench_threadpool_queue | 44 | 561 |
| bench_threadpool_threads | 43.12 | 44 |
| broker_backlog | 93578.8 | 116347 |
| ctr_eb_dc1a_cpu_pct | 177.943 | 1098.77 |
| ctr_eb_dc1a_mem_mb | 10066.222 | 10881.4 |
| ctr_eb_dc1b_cpu_pct | 67.875 | 401.11 |
| ctr_eb_dc1b_mem_mb | 2692.472 | 2744.7 |
| ctr_eb_dc2a_cpu_pct | 105.301 | 109.67 |
| ctr_eb_dc2a_mem_mb | 2670.065 | 2853.4 |
| ctr_eb_dc2b_cpu_pct | 899.482 | 945.9 |
| ctr_eb_dc2b_mem_mb | 7247.478 | 7832.5 |
| ctr_eb_etcd_dc1_cpu_pct | 1.697 | 2.35 |
| ctr_eb_etcd_dc1_mem_mb | 52.822 | 56.1 |
| ctr_eb_etcd_dc2_cpu_pct | 2.027 | 2.22 |
| ctr_eb_etcd_dc2_mem_mb | 26.717 | 27 |
| ctr_eb_etcd_w_cpu_pct | 2.209 | 3 |
| ctr_eb_etcd_w_mem_mb | 47.692 | 48.7 |
| ctr_eb_mosquitto_cpu_pct | 31.826 | 65.78 |
| ctr_eb_mosquitto_mem_mb | 12.32 | 51.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.022 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.024 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 7.812 | 8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.035 | 0.19 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.028 | 0.13 |
| ctr_eb_pgbouncer_mem_mb | 8.892 | 9.1 |
| ctr_eb_postgres_cpu_pct | 0.919 | 1.9 |
| ctr_eb_postgres_mem_mb | 2027.06 | 2028.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 56.922 | 1700.6 |
| host_cpu_pct | 46.598 | 88.51 |
| in_flight | 97130.44 | 116295 |
| nacks_per_s | 56.922 | 1700.6 |
| octopus_dispatcher_cores | 1.282 | 1.61 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 520956.62 | 751112 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 149453.16 | 214333 |
| octopus_mqtt_sent_total | 121732.42 | 188784 |
| octopus_publish_retries_total | 6430.52 | 18230 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.66 | 21 |
| pg_active_loads | 2.28 | 14 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 51.06 | 57 |
| pg_conn_idle | 216 | 239 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.42 | 7 |
| pg_conn_total | 267.06 | 268 |
| pg_db_size_mb | 1241.272 | 1405.1 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 1100.175 | 1352.5 |
| pg_replica_replay_lag_ms | 12790.69 | 15858.2 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16699.49 | 22991.2 |
| pg_tup_del_per_s | 25579.83 | 30633.2 |
| pg_tup_fetch_per_s | 425055.458 | 507666.3 |
| pg_tup_ins_per_s | 44996.488 | 61218.6 |
| pg_tup_upd_per_s | 19704.448 | 25937.7 |
| pg_wal_mb_per_s | 77.926 | 106.3 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 5862.978 | 15596.9 |
| produced_total | 301559.46 | 399586 |
| publish_mb_per_s | 22.942 | 72.67 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 72,197 records are Completed |
| record_counts | pass | 72,197 records found, 72,197 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 262 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 432,663 events applied by workers, 432,663 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 104,847 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27099 ms, max 32986 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13605 seq 7: ready 195 ms before start, 195 ms of it with an idle slot in group 431
- [cross_source_blocking] source 9685 seq 10: ready 137 ms before start, 137 ms of it with an idle slot in group 336
- [cross_source_blocking] source 14629 seq 5: ready 135 ms before start, 135 ms of it with an idle slot in group 462
- [cross_source_blocking] source 4709 seq 3: ready 242 ms before start, 242 ms of it with an idle slot in group 162
- [cross_source_blocking] source 9029 seq 4: ready 257 ms before start, 257 ms of it with an idle slot in group 312
- [cross_source_blocking] source 15941 seq 4: ready 277 ms before start, 247 ms of it with an idle slot in group 497
- [cross_source_blocking] source 2885 seq 7: ready 231 ms before start, 111 ms of it with an idle slot in group 92
- [cross_source_blocking] source 1989 seq 7: ready 229 ms before start, 171 ms of it with an idle slot in group 58
- [cross_source_blocking] source 10629 seq 6: ready 182 ms before start, 182 ms of it with an idle slot in group 349
- [cross_source_blocking] source 5081 seq 6: ready 180 ms before start, 129 ms of it with an idle slot in group 177
- [cross_source_blocking] source 14201 seq 22: ready 247 ms before start, 161 ms of it with an idle slot in group 454
- [cross_source_blocking] source 873 seq 16: ready 191 ms before start, 191 ms of it with an idle slot in group 26
- [cross_source_blocking] source 4241 seq 3: ready 143 ms before start, 135 ms of it with an idle slot in group 142
- [cross_source_blocking] source 13976 seq 8: ready 215 ms before start, 215 ms of it with an idle slot in group 444
- [cross_source_blocking] source 7401 seq 4: ready 251 ms before start, 251 ms of it with an idle slot in group 255
- [cross_source_blocking] source 1017 seq 13: ready 162 ms before start, 162 ms of it with an idle slot in group 31
- [cross_source_blocking] source 14401 seq 5: ready 251 ms before start, 220 ms of it with an idle slot in group 459
- [cross_source_blocking] source 6889 seq 9: ready 250 ms before start, 108 ms of it with an idle slot in group 234
- [cross_source_blocking] source 7913 seq 23: ready 244 ms before start, 244 ms of it with an idle slot in group 266
- [cross_source_blocking] source 13201 seq 7: ready 138 ms before start, 138 ms of it with an idle slot in group 420

## Error samples

- t-220#0 src=6422 seq=22 op=Complete rec=27582279974913 ev=5: NpgsqlException: Exception while reading from stream
- t-085#0 src=2766 seq=17 op=Update rec=11879879540737 ev=8: NpgsqlException: Exception while reading from stream
- t-049#0 src=1652 seq=14 op=Update rec=7095285972994 ev=4: NpgsqlException: Exception while reading from stream
- t-263#0 src=7710 seq=13 op=Complete rec=33114197852161 ev=3: NpgsqlException: Exception while reading from stream
- t-241#0 src=7114 seq=6 op=Get rec=30554397343746 ev=2: NpgsqlException: Exception while reading from stream
- t-200#0 src=5792 seq=6 op=Update rec=24876450578433 ev=4: NpgsqlException: Exception while reading from stream
- t-031#0 src=1013 seq=24 op=Get rec=4350801870852 ev=3: NpgsqlException: Exception while reading from stream
- t-352#0 src=10731 seq=34 op=Update rec=46089294053381 ev=8: NpgsqlException: Exception while reading from stream
- t-447#0 src=14059 seq=21 op=Update rec=60382945214466 ev=7: NpgsqlException: Exception while reading from stream
- t-231#0 src=6799 seq=16 op=Get rec=29201482645508 ev=2: NpgsqlException: Exception while reading from stream
- t-449#0 src=14079 seq=24 op=Create rec=60468844560390 ev=0: NpgsqlException: Exception while reading from stream
- t-355#0 src=10921 seq=25 op=Create rec=46905337839622 ev=0: NpgsqlException: Exception while reading from stream
- t-199#0 src=5758 seq=10 op=Update rec=24730421690370 ev=7: NpgsqlException: Exception while reading from stream
- t-370#0 src=11344 seq=12 op=Update rec=48722109005827 ev=1: NpgsqlException: Exception while reading from stream
- t-306#0 src=8892 seq=17 op=Update rec=38190849196034 ev=2: NpgsqlException: No suitable host was found.
- t-136#0 src=4064 seq=8 op=Update rec=17454747090946 ev=2: NpgsqlException: Exception while reading from stream
- t-189#0 src=5526 seq=11 op=Update rec=23733989277698 ev=2: NpgsqlException: Exception while reading from stream
- t-182#0 src=5252 seq=21 op=Get rec=22557168238594 ev=12: NpgsqlException: Exception while reading from stream
- t-018#0 src=624 seq=17 op=Update rec=2680059592707 ev=1: NpgsqlException: Exception while reading from stream
- t-386#0 src=12070 seq=6 op=Get rec=51840255262721 ev=3: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-436 | 58 | 1 | 35.493 | 34.473 | 1128 | 0 | 1159 | 1159 | 0 | 58720.255 | 71827.455 | 73924.607 |
| t-196 | 21 | 1 | 94.353 | 33.348 | 2244 | 0 | 1041 | 1041 | 0 | 57409.535 | 70254.591 | 71303.167 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 2798 | 2798 | 0 | 45088.767 | 68157.439 | 71303.167 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 5531 | 5531 | 0 | 46923.775 | 68157.439 | 73924.607 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 2298 | 2298 | 0 | 41418.751 | 66322.431 | 70254.591 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 2316 | 2316 | 0 | 41680.895 | 66060.287 | 72351.743 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4764 | 4764 | 0 | 45875.199 | 64487.423 | 72351.743 |
| t-481 | 12 | 1 | 121.412 | 22.891 | 1993 | 0 | 766 | 766 | 0 | 11665.407 | 64487.423 | 65011.711 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1044 | 1044 | 0 | 37224.447 | 64225.279 | 65535.999 |
| t-417 | 75 | 2 | 44.074 | 54.779 | 3449 | 0 | 1869 | 1869 | 0 | 29097.983 | 63963.135 | 69730.303 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3360 | 3360 | 0 | 49283.071 | 63700.991 | 72876.031 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 2371 | 2371 | 0 | 42991.615 | 63438.847 | 66322.431 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6279 | 6279 | 0 | 44564.479 | 63176.703 | 71303.167 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 4183 | 4183 | 0 | 43778.047 | 62914.559 | 71303.167 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4086 | 4086 | 0 | 41680.895 | 62390.271 | 66846.719 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4346 | 4346 | 0 | 43253.759 | 62128.127 | 70254.591 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3018 | 3018 | 0 | 46137.343 | 61603.839 | 69206.015 |
| t-352 | 16 | 1 | 104.412 | 27.446 | 2088 | 0 | 861 | 861 | 0 | 12713.983 | 61341.695 | 63963.135 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1598 | 1598 | 0 | 42991.615 | 61079.551 | 64487.423 |
| t-210 | 71 | 2 | 57.482 | 65.449 | 136 | 0 | 2264 | 2264 | 0 | 40108.031 | 60817.407 | 66846.719 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3174 | 3174 | 0 | 44564.479 | 60817.407 | 64225.279 |
| t-165 | 36 | 1 | 55.043 | 32.844 | 373 | 0 | 1116 | 1116 | 0 | 40632.319 | 60030.975 | 65011.711 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1505 | 1505 | 0 | 46137.343 | 58458.111 | 59506.687 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 2613 | 2613 | 0 | 40108.031 | 58458.111 | 63176.703 |
| t-029 | 28 | 2 | 143.274 | 66.438 | 1732 | 0 | 2190 | 2190 | 0 | 40632.319 | 58195.967 | 61865.983 |
