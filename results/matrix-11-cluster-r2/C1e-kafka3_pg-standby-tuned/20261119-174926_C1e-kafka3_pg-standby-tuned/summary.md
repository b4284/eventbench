# C1e-kafka3_pg-standby-tuned — FAIL ❌

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=3, flush.messages=default), acks=all idempotent, linger=10ms, compression=lz4, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9192,127.0.0.1:9292,127.0.0.1:9392`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.663s, closing 1.428s, drain 2.783s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 994391 | 994391 | 16886.024 | 16895.137 | 26620.5 | 0 | 0 | 0 | 308 | 5017 | 328508 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 710149 | 16622.986 | 16556.031 | 20152.319 | 23019.519 | 24346.623 | 27377.663 |
| e2e_from_send | 710149 | 520.977 | 464.127 | 920.575 | 2046.975 | 3371.007 | 6926.335 |
| producer_lag | 710149 | 16102.018 | 16015.359 | 19677.183 | 22511.615 | 23838.719 | 24363.007 |
| publish_call | 2044 | 963.715 | 881.151 | 1275.903 | 3411.967 | 3688.447 | 3723.263 |
| queue_wait | 710149 | 448.114 | 418.303 | 807.935 | 1341.439 | 1835.007 | 3008.511 |
| broker_dwell | 710149 | 72.472 | 25.375 | 72.511 | 952.319 | 1466.367 | 2627.583 |
| receive_call | 595098 | 0.042 | 0.013 | 0.031 | 0.437 | 5.011 | 48.991 |
| ctx_load | 710149 | 4.147 | 2.345 | 8.287 | 23.695 | 158.591 | 431.615 |
| process | 710149 | 10.533 | 0.001 | 0.003 | 0.015 | 2607.103 | 3002.367 |
| ctx_commit | 710149 | 21.301 | 8.863 | 58.399 | 149.887 | 455.935 | 583.167 |
| ack | 684058 | 0.007 | 0.003 | 0.008 | 0.024 | 0.866 | 36.703 |
| service | 710149 | 72.863 | 21.359 | 102.399 | 1528.831 | 2861.055 | 5836.799 |
| e2e_large | 207 | 17975.425 | 17924.095 | 21463.039 | 24969.215 | 26017.791 | 26017.791 |
| ready_wait | 710149 | 221.463 | 92.991 | 623.615 | 972.287 | 1275.903 | 1975.295 |
| blocked | 710149 | 204.552 | 51.391 | 606.207 | 961.535 | 1259.519 | 1889.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16895.137 | 26620.5 |
| applied_total | 508460.673 | 941738 |
| bench_cpu_cores | 5.555 | 6.022 |
| bench_cpu_pct | 23.148 | 25.09 |
| bench_gc_gen0 | 1.837 | 3 |
| bench_gc_gen1 | 0.959 | 2 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 889.371 | 1155.2 |
| bench_mem_mb | 2215.092 | 2382.2 |
| bench_threadpool_queue | 61.49 | 276 |
| bench_threadpool_threads | 39.041 | 44 |
| broker_backlog | 3157 | 10307 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 459927.816 | 884330 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 453764.755 | 875619 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_1_cpu_pct | 254.997 | 297.58 |
| ctr_eb_kafka_1_mem_mb | 2010.498 | 2379.4 |
| ctr_eb_kafka_2_cpu_pct | 281.407 | 316.06 |
| ctr_eb_kafka_2_mem_mb | 2119.12 | 2542.5 |
| ctr_eb_kafka_3_cpu_pct | 258.142 | 303.21 |
| ctr_eb_kafka_3_mem_mb | 2607.194 | 2978.3 |
| ctr_eb_pg_standby_cpu_pct | 23.899 | 31.93 |
| ctr_eb_pg_standby_mem_mb | 781.898 | 1244.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.505 | 22.6 |
| ctr_eb_pgbouncer_2_mem_mb | 11.139 | 12.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 17.938 | 20.1 |
| ctr_eb_pgbouncer_3_mem_mb | 10.218 | 11.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 18.362 | 20.37 |
| ctr_eb_pgbouncer_4_mem_mb | 10.367 | 12.1 |
| ctr_eb_pgbouncer_cpu_pct | 20.743 | 23.2 |
| ctr_eb_pgbouncer_mem_mb | 11.429 | 14 |
| ctr_eb_postgres_cpu_pct | 337.212 | 405.09 |
| ctr_eb_postgres_mem_mb | 1813.694 | 2532 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 75.867 | 80.55 |
| in_flight | -6697.49 | 8163 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.122 | 8 |
| pg_active_loads | 2.449 | 5 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 7.122 | 13 |
| pg_conn_idle | 184.469 | 190 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.367 | 4 |
| pg_conn_total | 191.592 | 193 |
| pg_db_size_mb | 1445.5 | 1886.4 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 3.064 | 29.46 |
| pg_replica_replay_lag_ms | 89.739 | 503.6 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18197.747 | 22004.6 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 78445.927 | 102949.3 |
| pg_tup_ins_per_s | 9732.222 | 13012.3 |
| pg_tup_upd_per_s | 27671.751 | 35378.7 |
| pg_wal_mb_per_s | 32.783 | 42.86 |
| pgb_cl_active | 206.571 | 275 |
| pgb_cl_waiting | 12.571 | 154 |
| pgb_maxwait_ms | 5.251 | 64.32 |
| pgb_sv_active | 11.653 | 48 |
| pgb_sv_idle | 36.347 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16886.024 | 25989.4 |
| produced_total | 501763.184 | 932804 |
| publish_mb_per_s | 48.081 | 91.9 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,183 records are Completed |
| record_counts | pass | 138,183 records found, 138,183 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 308 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 994,391 events applied by workers, 994,391 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 328,508 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 962 ms, max 1889 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14601 seq 15: ready 126 ms before start, 126 ms of it with an idle slot in group 461
- [cross_source_blocking] source 11465 seq 11: ready 286 ms before start, 286 ms of it with an idle slot in group 372
- [cross_source_blocking] source 9161 seq 26: ready 547 ms before start, 547 ms of it with an idle slot in group 315
- [cross_source_blocking] source 13577 seq 3: ready 602 ms before start, 602 ms of it with an idle slot in group 429
- [cross_source_blocking] source 15740 seq 10: ready 310 ms before start, 310 ms of it with an idle slot in group 491
- [cross_source_blocking] source 7132 seq 16: ready 322 ms before start, 322 ms of it with an idle slot in group 242
- [cross_source_blocking] source 5052 seq 13: ready 321 ms before start, 321 ms of it with an idle slot in group 176
- [cross_source_blocking] source 3881 seq 15: ready 743 ms before start, 743 ms of it with an idle slot in group 131
- [cross_source_blocking] source 15356 seq 8: ready 350 ms before start, 350 ms of it with an idle slot in group 480
- [cross_source_blocking] source 6697 seq 10: ready 751 ms before start, 751 ms of it with an idle slot in group 228
- [cross_source_blocking] source 9052 seq 7: ready 358 ms before start, 358 ms of it with an idle slot in group 313
- [cross_source_blocking] source 8988 seq 10: ready 402 ms before start, 402 ms of it with an idle slot in group 309
- [cross_source_blocking] source 13993 seq 7: ready 842 ms before start, 842 ms of it with an idle slot in group 445
- [cross_source_blocking] source 14588 seq 17: ready 457 ms before start, 457 ms of it with an idle slot in group 461
- [cross_source_blocking] source 11228 seq 5: ready 467 ms before start, 467 ms of it with an idle slot in group 366
- [cross_source_blocking] source 14057 seq 16: ready 897 ms before start, 897 ms of it with an idle slot in group 447
- [cross_source_blocking] source 12843 seq 28: ready 128 ms before start, 128 ms of it with an idle slot in group 406
- [cross_source_blocking] source 10187 seq 30: ready 121 ms before start, 121 ms of it with an idle slot in group 344
- [cross_source_blocking] source 9675 seq 7: ready 126 ms before start, 126 ms of it with an idle slot in group 335
- [cross_source_blocking] source 14862 seq 9: ready 107 ms before start, 107 ms of it with an idle slot in group 466

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 1875 | 1875 | 0 | 17432.575 | 25427.967 | 25952.255 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 73 | 73 | 0 | 17694.719 | 25296.895 | 25296.895 |
| t-135 | 6 | 1 | 28.42 | 2.899 | 791 | 0 | 144 | 144 | 0 | 17170.431 | 24903.679 | 25034.751 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 281 | 281 | 0 | 17694.719 | 24772.607 | 26083.327 |
| t-331 | 12 | 1 | 67.123 | 13.792 | 499 | 0 | 515 | 515 | 0 | 17956.863 | 24641.535 | 26083.327 |
| t-118 | 15 | 1 | 45.074 | 11.403 | 138 | 0 | 460 | 460 | 0 | 17956.863 | 24379.391 | 24641.535 |
| t-089 | 6 | 1 | 40.807 | 3.807 | 1545 | 0 | 169 | 169 | 0 | 16515.071 | 24248.319 | 25034.751 |
| t-112 | 6 | 1 | 86.754 | 8.91 | 774 | 0 | 351 | 351 | 0 | 18481.151 | 24248.319 | 24510.463 |
| t-327 | 6 | 1 | 124.318 | 11.226 | 370 | 0 | 465 | 465 | 0 | 17170.431 | 24248.319 | 24772.607 |
| t-146 | 6 | 1 | 80.954 | 8.685 | 246 | 0 | 342 | 342 | 0 | 17694.719 | 24117.247 | 24379.391 |
| t-232 | 6 | 1 | 106.056 | 10.781 | 805 | 0 | 450 | 450 | 0 | 16580.607 | 24117.247 | 24379.391 |
| t-273 | 8 | 1 | 188.7 | 24.75 | 898 | 0 | 1015 | 1015 | 0 | 16646.143 | 24117.247 | 24641.535 |
| t-289 | 6 | 1 | 125.756 | 13.473 | 1711 | 0 | 592 | 592 | 0 | 17170.431 | 24117.247 | 24379.391 |
| t-342 | 15 | 2 | 261.014 | 65.273 | 146 | 0 | 2474 | 2474 | 0 | 17563.647 | 24117.247 | 26214.399 |
| t-440 | 6 | 1 | 37.016 | 3.802 | 154 | 0 | 156 | 156 | 0 | 16515.071 | 24117.247 | 24248.319 |
| t-489 | 6 | 1 | 261.232 | 26.664 | 1030 | 0 | 986 | 986 | 0 | 18087.935 | 24117.247 | 24903.679 |
| t-205 | 18 | 1 | 98.727 | 29.361 | 449 | 0 | 1172 | 1172 | 0 | 17432.575 | 23986.175 | 26214.399 |
| t-323 | 6 | 1 | 55.173 | 5.539 | 3540 | 0 | 236 | 236 | 0 | 17956.863 | 23986.175 | 24379.391 |
| t-396 | 17 | 1 | 77.52 | 22.359 | 210 | 0 | 910 | 910 | 0 | 17039.359 | 23986.175 | 24641.535 |
| t-454 | 7 | 1 | 251.84 | 29.074 | 266 | 0 | 1136 | 1136 | 0 | 17563.647 | 23986.175 | 25034.751 |
| t-481 | 12 | 1 | 227.041 | 42.807 | 1993 | 0 | 1582 | 1582 | 0 | 18087.935 | 23986.175 | 24772.607 |
| t-013 | 10 | 1 | 68.642 | 10.415 | 134 | 0 | 409 | 409 | 0 | 18350.079 | 23855.103 | 24510.463 |
| t-033 | 6 | 1 | 131.683 | 14.356 | 859 | 0 | 573 | 573 | 0 | 17563.647 | 23855.103 | 24117.247 |
| t-117 | 28 | 1 | 44.784 | 20.99 | 294 | 0 | 895 | 895 | 0 | 16580.607 | 23855.103 | 24772.607 |
| t-123 | 16 | 1 | 125.585 | 31.218 | 4028 | 0 | 1248 | 1248 | 0 | 17301.503 | 23855.103 | 24510.463 |
