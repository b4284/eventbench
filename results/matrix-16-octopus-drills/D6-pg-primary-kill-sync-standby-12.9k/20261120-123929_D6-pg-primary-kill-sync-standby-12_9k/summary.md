# D6-pg-primary-kill-sync-standby-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.274s, closing 2.74s, drain 9.207s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 813419 | 813419 | 12818.169 | 11924.153 | 20225.2 | 0 | 4470 | 4470 | 429 | 4251 | 289390 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 641191 | 3307.797 | 1311.743 | 9109.503 | 24707.071 | 33095.679 | 43876.351 |
| e2e_from_send | 641191 | 3045.956 | 772.607 | 8921.087 | 23805.951 | 32522.239 | 43876.351 |
| producer_lag | 641672 | 261.651 | 3.621 | 1038.847 | 1948.671 | 2418.687 | 2910.207 |
| publish_call | 813419 | 84.436 | 62.559 | 156.671 | 467.711 | 1262.591 | 1895.423 |
| queue_wait | 641191 | 2957.719 | 660.479 | 8847.359 | 23756.799 | 32473.087 | 43646.975 |
| broker_dwell | 641191 | 2880.859 | 561.151 | 8773.631 | 23642.111 | 32342.015 | 43614.207 |
| receive_call | 742609 | 29.248 | 6.035 | 86.399 | 336.639 | 473.855 | 523.007 |
| ctx_load | 641191 | 6.549 | 3.959 | 14.783 | 39.647 | 62.335 | 492.799 |
| process | 641191 | 10.919 | 0.002 | 0.004 | 0.044 | 2625.535 | 3006.463 |
| ctx_commit | 641191 | 70.722 | 57.599 | 123.903 | 327.167 | 891.391 | 1067.007 |
| ack | 641099 | 0.01 | 0.004 | 0.007 | 0.05 | 0.89 | 36.351 |
| service | 641191 | 88.236 | 64.863 | 134.527 | 591.359 | 2729.983 | 3987.455 |
| e2e_large | 328 | 3017.054 | 2826.239 | 4419.583 | 7114.751 | 8134.655 | 8134.655 |
| ready_wait | 641191 | 397.857 | 166.015 | 1071.103 | 2443.263 | 4399.103 | 35520.511 |
| blocked | 641191 | 167.928 | 79.615 | 427.007 | 987.135 | 2398.207 | 17317.887 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11924.153 | 18316.6 |
| applied_total | 429651.163 | 706416 |
| bench_cpu_cores | 3.797 | 4.852 |
| bench_cpu_pct | 15.822 | 20.22 |
| bench_gc_gen0 | 2.898 | 6 |
| bench_gc_gen1 | 2.735 | 4 |
| bench_gc_gen2 | 0.571 | 3 |
| bench_gc_heap_mb | 1019.11 | 1306.5 |
| bench_mem_mb | 1531.698 | 1639.8 |
| bench_threadpool_queue | 50.837 | 455 |
| bench_threadpool_threads | 42.918 | 44 |
| broker_backlog | 25473.857 | 71016 |
| ctr_eb_mosquitto_cpu_pct | 49.584 | 63.71 |
| ctr_eb_mosquitto_mem_mb | 9.808 | 28 |
| ctr_eb_pg_standby_cpu_pct | 538.522 | 1083.79 |
| ctr_eb_pg_standby_mem_mb | 3595.178 | 5349.2 |
| ctr_eb_postgres_cpu_pct | 997.052 | 1013.59 |
| ctr_eb_postgres_mem_mb | 5013.164 | 5181.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 91.439 | 4113.1 |
| host_cpu_pct | 69.903 | 78.59 |
| in_flight | 34769.082 | 68587 |
| nacks_per_s | 91.439 | 4113.1 |
| octopus_dispatcher_cores | 1.622 | 1.76 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1192502.082 | 2770509 |
| octopus_mqtt_received_total | 130178.061 | 300453 |
| octopus_mqtt_sent_total | 113279.49 | 259238 |
| octopus_publish_retries_total | 4234.041 | 7684 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.31 | 20 |
| pg_active_loads | 17.517 | 152 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 59.966 | 81 |
| pg_conn_idle | 206.724 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.345 | 13 |
| pg_conn_total | 266.69 | 297 |
| pg_db_size_mb | 4232.29 | 5380.5 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 0 | 0 |
| pg_replica_replay_lag_ms | 1.5 | 1.5 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 28.273 | 56.08 |
| pg_tps | 20733.623 | 36997.7 |
| pg_tup_del_per_s | 24511.788 | 41310.2 |
| pg_tup_fetch_per_s | 492886.731 | 866194.8 |
| pg_tup_ins_per_s | 35461.454 | 52515.1 |
| pg_tup_upd_per_s | 23606.396 | 42764.2 |
| pg_wal_mb_per_s | 121.13 | 191.73 |
| produced_per_s | 12818.169 | 19174.8 |
| produced_total | 464420.245 | 775003 |
| publish_mb_per_s | 44.095 | 73.07 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,478 records are Completed |
| record_counts | pass | 117,478 records found, 117,478 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 429 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 813,419 events applied by workers, 813,419 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 289,390 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 987 ms, max 17315 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4557 seq 7: ready 107 ms before start, 107 ms of it with an idle slot in group 160
- [cross_source_blocking] source 6377 seq 2: ready 102 ms before start, 102 ms of it with an idle slot in group 218
- [cross_source_blocking] source 3285 seq 7: ready 101 ms before start, 101 ms of it with an idle slot in group 109
- [cross_source_blocking] source 15660 seq 8: ready 196 ms before start, 115 ms of it with an idle slot in group 488
- [cross_source_blocking] source 5406 seq 12: ready 211 ms before start, 138 ms of it with an idle slot in group 184
- [cross_source_blocking] source 2223 seq 20: ready 214 ms before start, 214 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4886 seq 5: ready 252 ms before start, 125 ms of it with an idle slot in group 169
- [cross_source_blocking] source 2476 seq 12: ready 268 ms before start, 134 ms of it with an idle slot in group 74
- [cross_source_blocking] source 4888 seq 13: ready 266 ms before start, 123 ms of it with an idle slot in group 169
- [cross_source_blocking] source 4881 seq 7: ready 278 ms before start, 129 ms of it with an idle slot in group 169
- [cross_source_blocking] source 1957 seq 15: ready 286 ms before start, 283 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1932 seq 18: ready 291 ms before start, 289 ms of it with an idle slot in group 55
- [cross_source_blocking] source 4993 seq 12: ready 287 ms before start, 287 ms of it with an idle slot in group 172
- [cross_source_blocking] source 1203 seq 9: ready 289 ms before start, 289 ms of it with an idle slot in group 40
- [cross_source_blocking] source 11132 seq 11: ready 286 ms before start, 220 ms of it with an idle slot in group 363
- [cross_source_blocking] source 9533 seq 5: ready 292 ms before start, 292 ms of it with an idle slot in group 331
- [cross_source_blocking] source 10813 seq 11: ready 287 ms before start, 287 ms of it with an idle slot in group 353
- [cross_source_blocking] source 12669 seq 4: ready 287 ms before start, 287 ms of it with an idle slot in group 399
- [cross_source_blocking] source 3518 seq 16: ready 294 ms before start, 294 ms of it with an idle slot in group 120
- [cross_source_blocking] source 9311 seq 12: ready 291 ms before start, 129 ms of it with an idle slot in group 322

## Error samples

- t-205#0 src=5907 seq=24 op=Update rec=25370371817478 ev=1: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-301#0 src=8687 seq=21 op=Update rec=37310380900354 ev=12: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-377#0 src=11611 seq=17 op=Get rec=49868865273859 ev=3: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-434#0 src=13627 seq=19 op=Update rec=58527519342595 ev=4: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-119L#3 src=3429 seq=4 op=Update rec=14727442857985 ev=3: NpgsqlException: No suitable host was found.
- t-082#0 src=2679 seq=8 op=Get rec=11506217385986 ev=2: NpgsqlException: No suitable host was found.
- t-290#0 src=8487 seq=41 op=Create rec=36451387441161 ev=0: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-290#0 src=8485 seq=44 op=Update rec=36442797506562 ev=8: NpgsqlException: No suitable host was found.
- t-239#1 src=7066 seq=69 op=Create rec=30348238913547 ev=0: NpgsqlException: No suitable host was found.
- t-348#1 src=10497 seq=46 op=Update rec=45084271706118 ev=6: NpgsqlException: No suitable host was found.
- t-121#1 src=3549 seq=61 op=Update rec=15242838933517 ev=2: NpgsqlException: No suitable host was found.
- t-415#0 src=13039 seq=37 op=Get rec=56002078572545 ev=9: NpgsqlException: No suitable host was found.
- t-402#0 src=12736 seq=11 op=Complete rec=54700703481858 ev=3: NpgsqlException: No suitable host was found.
- t-102#0 src=3040 seq=22 op=Get rec=13056700579843 ev=1: NpgsqlException: No suitable host was found.
- t-304#0 src=8747 seq=53 op=Update rec=37568078938118 ev=7: NpgsqlException: No suitable host was found.
- t-241#0 src=7098 seq=9 op=Update rec=30485677867011 ev=1: NpgsqlException: No suitable host was found.
- t-314#0 src=9099 seq=95 op=Update rec=39079907426316 ev=1: NpgsqlException: No suitable host was found.
- t-211#0 src=6192 seq=26 op=Complete rec=26594437496836 ev=1: NpgsqlException: No suitable host was found.
- t-160#1 src=4592 seq=23 op=Update rec=19722489823234 ev=7: NpgsqlException: No suitable host was found.
- t-106#1 src=3205 seq=92 op=Update rec=13765370183694 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2874 | 2874 | 0 | 5603.327 | 38010.879 | 44040.191 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2901 | 2901 | 0 | 7471.103 | 34865.151 | 36175.871 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3058 | 3058 | 0 | 7274.495 | 34865.151 | 38273.023 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2905 | 2905 | 0 | 5079.039 | 33816.575 | 34865.151 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12241 | 12241 | 0 | 7438.335 | 33816.575 | 35389.439 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3024 | 3024 | 0 | 4112.383 | 33030.143 | 34603.007 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2610 | 2610 | 0 | 3211.263 | 32767.999 | 34340.863 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 6619.135 | 32636.927 | 36438.015 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12118 | 12118 | 0 | 9240.575 | 32243.711 | 36438.015 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5483 | 5483 | 0 | 3686.399 | 31981.567 | 34340.863 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6066 | 6066 | 0 | 9306.111 | 31719.423 | 35913.727 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2536 | 2536 | 0 | 3080.191 | 31588.351 | 32899.071 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8024 | 8024 | 0 | 3129.343 | 31195.135 | 33554.431 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2679 | 2679 | 0 | 3981.311 | 31064.063 | 33816.575 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 7784 | 7784 | 0 | 2064.383 | 30146.559 | 31850.495 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9172 | 9172 | 0 | 9502.719 | 29753.343 | 36175.871 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13781 | 13781 | 0 | 3751.935 | 28180.479 | 32899.071 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4966 | 4966 | 0 | 2555.903 | 27525.119 | 29097.983 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5299 | 5299 | 0 | 4112.383 | 27394.047 | 28573.695 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5920 | 5920 | 0 | 8454.143 | 26738.687 | 32899.071 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6327 | 6327 | 0 | 8454.143 | 26214.399 | 31326.207 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 7407 | 7407 | 0 | 2654.207 | 26083.327 | 32636.927 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4608 | 4608 | 0 | 1941.503 | 25821.183 | 28573.695 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4553 | 4553 | 0 | 3719.167 | 25427.967 | 27262.975 |
| t-441 | 30 | 2 | 141.287 | 68.521 | 2203 | 0 | 4253 | 4253 | 0 | 1466.367 | 25034.751 | 27918.335 |
