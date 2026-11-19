# C1c-kafka3_pg-standby-sync — FAIL ❌

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=3, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9192,127.0.0.1:9292,127.0.0.1:9392`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.498s, closing 1.36s, drain 1.549s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1151789 | 1151789 | 18789.034 | 18777.266 | 22794.9 | 0 | 0 | 0 | 346 | 5808 | 438507 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 867097 | 9047.215 | 8847.359 | 13197.311 | 15671.295 | 16637.951 | 19431.423 |
| e2e_from_send | 867097 | 453.162 | 421.887 | 750.591 | 1849.343 | 3256.319 | 6565.887 |
| producer_lag | 867097 | 8594.055 | 8396.799 | 12697.599 | 15138.815 | 16130.047 | 16572.415 |
| publish_call | 2377 | 825.912 | 837.119 | 968.703 | 1164.287 | 1295.359 | 1321.983 |
| queue_wait | 867097 | 403.967 | 395.519 | 709.631 | 899.583 | 1060.863 | 2140.159 |
| broker_dwell | 867097 | 26.898 | 22.175 | 51.775 | 84.159 | 157.823 | 1656.831 |
| receive_call | 654228 | 0.045 | 0.013 | 0.03 | 0.46 | 4.723 | 166.527 |
| ctx_load | 867097 | 3.168 | 1.895 | 6.731 | 18.655 | 75.839 | 147.327 |
| process | 867097 | 10.463 | 0.001 | 0.003 | 0.011 | 2611.199 | 3024.895 |
| ctx_commit | 867097 | 7.785 | 4.799 | 16.543 | 47.359 | 118.335 | 274.431 |
| ack | 830715 | 0.007 | 0.003 | 0.009 | 0.022 | 0.839 | 90.815 |
| service | 867097 | 49.195 | 12.207 | 42.783 | 1465.343 | 2815.999 | 5808.127 |
| e2e_large | 245 | 10675.137 | 10231.807 | 15007.743 | 18006.015 | 18825.215 | 18825.215 |
| ready_wait | 867097 | 223.958 | 107.839 | 627.711 | 841.727 | 992.767 | 1360.895 |
| blocked | 867097 | 223.099 | 107.199 | 625.663 | 839.167 | 986.623 | 1360.895 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 18777.266 | 22794.9 |
| applied_total | 652948.02 | 1109986 |
| bench_cpu_cores | 5.879 | 6.39 |
| bench_cpu_pct | 24.494 | 26.62 |
| bench_gc_gen0 | 2 | 3 |
| bench_gc_gen1 | 1.06 | 3 |
| bench_gc_gen2 | 0.28 | 1 |
| bench_gc_heap_mb | 919.742 | 1213 |
| bench_mem_mb | 2346.802 | 2514 |
| bench_threadpool_queue | 40.74 | 288 |
| bench_threadpool_threads | 39.32 | 44 |
| broker_backlog | 5236.32 | 7298 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 614117.5 | 1092927 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 616797.86 | 1092776 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_1_cpu_pct | 264.956 | 271.76 |
| ctr_eb_kafka_1_mem_mb | 3124.398 | 3244.4 |
| ctr_eb_kafka_2_cpu_pct | 261.595 | 268.68 |
| ctr_eb_kafka_2_mem_mb | 2830.61 | 2988.8 |
| ctr_eb_kafka_3_cpu_pct | 265.528 | 272.79 |
| ctr_eb_kafka_3_mem_mb | 3033.178 | 3153.8 |
| ctr_eb_pg_standby_cpu_pct | 26.652 | 33.29 |
| ctr_eb_pg_standby_mem_mb | 1279.884 | 1341.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 18.346 | 19.19 |
| ctr_eb_pgbouncer_2_mem_mb | 10.926 | 13.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 17.392 | 18.02 |
| ctr_eb_pgbouncer_3_mem_mb | 11.118 | 13.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 17.962 | 18.98 |
| ctr_eb_pgbouncer_4_mem_mb | 11.082 | 12.1 |
| ctr_eb_pgbouncer_cpu_pct | 19.051 | 20.16 |
| ctr_eb_pgbouncer_mem_mb | 11.014 | 11.8 |
| ctr_eb_postgres_cpu_pct | 334.878 | 349.19 |
| ctr_eb_postgres_mem_mb | 2884.822 | 3003.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.037 | 82.2 |
| in_flight | -7286.02 | -4513 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.1 | 9 |
| pg_active_loads | 2.38 | 8 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 5.24 | 10 |
| pg_conn_idle | 186.76 | 192 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.04 | 7 |
| pg_conn_total | 192 | 192 |
| pg_db_size_mb | 1587.494 | 1969.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 0.753 | 2.78 |
| pg_replica_replay_lag_ms | 27.018 | 84.9 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21016.91 | 22374.8 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 88443.978 | 92909.9 |
| pg_tup_ins_per_s | 10408.648 | 15720.7 |
| pg_tup_upd_per_s | 31407.188 | 33717.8 |
| pg_wal_mb_per_s | 36.075 | 45.42 |
| pgb_cl_active | 248.32 | 262 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 10.42 | 46 |
| pgb_sv_idle | 37.58 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 18789.034 | 21465.5 |
| produced_total | 645662 | 1102752 |
| publish_mb_per_s | 52.3 | 81.83 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 155,998 records are Completed |
| record_counts | pass | 155,998 records found, 155,998 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 346 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,151,789 events applied by workers, 1,151,789 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 438,507 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 839 ms, max 1361 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3998 seq 6: ready 108 ms before start, 108 ms of it with an idle slot in group 133
- [cross_source_blocking] source 1694 seq 5: ready 110 ms before start, 110 ms of it with an idle slot in group 50
- [cross_source_blocking] source 15678 seq 28: ready 110 ms before start, 110 ms of it with an idle slot in group 490
- [cross_source_blocking] source 1854 seq 11: ready 133 ms before start, 133 ms of it with an idle slot in group 52
- [cross_source_blocking] source 9598 seq 4: ready 133 ms before start, 133 ms of it with an idle slot in group 333
- [cross_source_blocking] source 10238 seq 27: ready 138 ms before start, 138 ms of it with an idle slot in group 344
- [cross_source_blocking] source 7582 seq 15: ready 177 ms before start, 177 ms of it with an idle slot in group 261
- [cross_source_blocking] source 4510 seq 4: ready 177 ms before start, 177 ms of it with an idle slot in group 157
- [cross_source_blocking] source 8830 seq 27: ready 227 ms before start, 227 ms of it with an idle slot in group 306
- [cross_source_blocking] source 13214 seq 19: ready 258 ms before start, 258 ms of it with an idle slot in group 422
- [cross_source_blocking] source 15998 seq 21: ready 290 ms before start, 290 ms of it with an idle slot in group 499
- [cross_source_blocking] source 2430 seq 10: ready 370 ms before start, 370 ms of it with an idle slot in group 71
- [cross_source_blocking] source 13150 seq 10: ready 377 ms before start, 377 ms of it with an idle slot in group 417
- [cross_source_blocking] source 11102 seq 19: ready 434 ms before start, 434 ms of it with an idle slot in group 362
- [cross_source_blocking] source 6238 seq 10: ready 428 ms before start, 412 ms of it with an idle slot in group 213
- [cross_source_blocking] source 6366 seq 10: ready 467 ms before start, 467 ms of it with an idle slot in group 218
- [cross_source_blocking] source 2999 seq 8: ready 133 ms before start, 133 ms of it with an idle slot in group 100
- [cross_source_blocking] source 8823 seq 33: ready 122 ms before start, 122 ms of it with an idle slot in group 306
- [cross_source_blocking] source 8798 seq 20: ready 482 ms before start, 482 ms of it with an idle slot in group 306
- [cross_source_blocking] source 5943 seq 18: ready 137 ms before start, 137 ms of it with an idle slot in group 206

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 72 | 72 | 0 | 9830.399 | 18481.151 | 18481.151 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 314 | 314 | 0 | 9830.399 | 17956.863 | 18874.367 |
| t-266 | 6 | 1 | 212.127 | 20.747 | 714 | 0 | 991 | 991 | 0 | 9240.575 | 17039.359 | 18219.007 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 133 | 133 | 0 | 9568.255 | 17039.359 | 17301.503 |
| t-452 | 6 | 1 | 38.523 | 4.03 | 1797 | 0 | 197 | 197 | 0 | 10354.687 | 17039.359 | 17039.359 |
| t-298 | 6 | 1 | 119.839 | 13.179 | 3700 | 0 | 635 | 635 | 0 | 9568.255 | 16777.215 | 18087.935 |
| t-463 | 12 | 1 | 36.223 | 7.337 | 2195 | 0 | 412 | 412 | 0 | 9371.647 | 16711.679 | 17170.431 |
| t-087 | 6 | 1 | 65.33 | 7.05 | 1614 | 0 | 345 | 345 | 0 | 9306.111 | 16646.143 | 16711.679 |
| t-099 | 13 | 1 | 92.516 | 21.307 | 419 | 0 | 999 | 999 | 0 | 9175.039 | 16646.143 | 17825.791 |
| t-223 | 13 | 1 | 201.174 | 42.551 | 2372 | 0 | 1867 | 1867 | 0 | 9502.719 | 16646.143 | 19136.511 |
| t-144 | 6 | 1 | 45.956 | 4.775 | 816 | 0 | 220 | 220 | 0 | 10420.223 | 16580.607 | 16711.679 |
| t-195 | 9 | 1 | 138.561 | 22.106 | 2299 | 0 | 1070 | 1070 | 0 | 9109.503 | 16515.071 | 17039.359 |
| t-356 | 6 | 1 | 159.495 | 15.624 | 303 | 0 | 716 | 716 | 0 | 9371.647 | 16515.071 | 16711.679 |
| t-114 | 14 | 1 | 91.618 | 21.124 | 1025 | 0 | 950 | 950 | 0 | 9437.183 | 16449.535 | 17039.359 |
| t-287 | 17 | 1 | 134.36 | 39.426 | 158 | 0 | 1881 | 1881 | 0 | 9306.111 | 16449.535 | 17694.719 |
| t-028 | 27 | 1 | 38.536 | 17.123 | 454 | 0 | 883 | 883 | 0 | 8978.431 | 16383.999 | 17039.359 |
| t-078 | 6 | 1 | 32.959 | 3.199 | 386 | 0 | 163 | 163 | 0 | 8716.287 | 16383.999 | 16580.607 |
| t-164 | 7 | 1 | 190.865 | 20.52 | 236 | 0 | 946 | 946 | 0 | 9437.183 | 16383.999 | 16908.287 |
| t-008 | 11 | 1 | 138.353 | 24.947 | 279 | 0 | 1175 | 1175 | 0 | 9895.935 | 16318.463 | 16646.143 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 2197 | 2197 | 0 | 9371.647 | 16318.463 | 17039.359 |
| t-310 | 6 | 1 | 131.802 | 13.811 | 1910 | 0 | 689 | 689 | 0 | 8847.359 | 16318.463 | 16908.287 |
| t-405 | 6 | 1 | 45.706 | 4.532 | 874 | 0 | 226 | 226 | 0 | 10027.007 | 16318.463 | 16908.287 |
| t-432 | 9 | 1 | 109.506 | 16.119 | 747 | 0 | 780 | 780 | 0 | 9371.647 | 16318.463 | 16646.143 |
| t-486 | 17 | 1 | 100.55 | 26.793 | 1259 | 0 | 1270 | 1270 | 0 | 9306.111 | 16318.463 | 17170.431 |
| t-012 | 6 | 1 | 115.634 | 11.982 | 556 | 0 | 579 | 579 | 0 | 9961.471 | 16252.927 | 16908.287 |
