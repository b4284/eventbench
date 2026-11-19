# D1-pgqueue-24k-async-KILL-PRIMARY — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.1s, closing 0.305s, drain 600.016s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487617 | 1443177 | 24045.341 | 16334.002 | 40985.3 | 0 | 150 | 1536288 | 425 | 7514 | 697667 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1159377 | 14712.558 | 1878.015 | 53018.623 | 69337.087 | 75169.791 | 79560.703 |
| e2e_from_send | 1159377 | 14663.886 | 1672.191 | 53018.623 | 69337.087 | 75169.791 | 79560.703 |
| producer_lag | 1202193 | 49.964 | 1.054 | 34.911 | 1188.863 | 1591.295 | 1736.703 |
| publish_call | 667113 | 2.04 | 1.166 | 3.735 | 9.127 | 71.679 | 1734.655 |
| queue_wait | 1159377 | 14616.829 | 1627.135 | 52985.855 | 69337.087 | 75169.791 | 78446.591 |
| broker_dwell | 1159377 | 14604.335 | 1605.631 | 52985.855 | 69337.087 | 75169.791 | 78446.591 |
| receive_call | 2057425 | 165.69 | 175.231 | 280.319 | 448.767 | 495.103 | 504.575 |
| ctx_load | 1159377 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 1.342 |
| process | 1159377 | 10.409 | 0.001 | 0.002 | 0.004 | 2613.247 | 3002.367 |
| ctx_commit | 1159377 | 5.933 | 5.103 | 8.327 | 30.431 | 116.031 | 1598.463 |
| service | 1159377 | 47.062 | 18.687 | 94.911 | 207.359 | 2666.495 | 3897.343 |
| e2e_large | 324 | 2539.22 | 2430.975 | 3667.967 | 5742.591 | 7540.735 | 7540.735 |
| ready_wait | 1159377 | 921.643 | 205.439 | 2430.975 | 9887.743 | 23379.967 | 65273.855 |
| blocked | 1159377 | 920.998 | 205.439 | 2426.879 | 9887.743 | 23379.967 | 65273.855 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16334.002 | 40985.3 |
| applied_total | 759805.265 | 1049570 |
| bench_cpu_cores | 3.595 | 4.957 |
| bench_cpu_pct | 14.978 | 20.65 |
| bench_gc_gen0 | 2.633 | 6 |
| bench_gc_gen1 | 1.776 | 6 |
| bench_gc_gen2 | 0.265 | 1 |
| bench_gc_heap_mb | 848.651 | 1025.5 |
| bench_mem_mb | 1143.602 | 1276 |
| bench_threadpool_queue | 9.571 | 153 |
| bench_threadpool_threads | 41.143 | 44 |
| broker_backlog | 98865.408 | 367647 |
| ctr_eb_pg_standby_cpu_pct | 508.233 | 1063.55 |
| ctr_eb_pg_standby_mem_mb | 2932.586 | 3402.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.06 |
| ctr_eb_pgbouncer_2_mem_mb | 10.982 | 11.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 10.982 | 11.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.08 |
| ctr_eb_pgbouncer_4_mem_mb | 9.382 | 9.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.022 | 0.07 |
| ctr_eb_pgbouncer_mem_mb | 11.882 | 12 |
| ctr_eb_postgres_cpu_pct | 1176.739 | 1232.94 |
| ctr_eb_postgres_mem_mb | 10529.525 | 10643 |
| dispatch_empty_reads_per_s | 1832.971 | 4682.3 |
| dispatch_fetch_calls_per_s | 178.037 | 318.4 |
| dispatch_group_reads_per_s | 8790.782 | 15838.4 |
| dispatch_in_flight | 497.959 | 1017 |
| dispatch_notifies_per_s | 22844.755 | 28704.3 |
| dispatch_preloaded_per_s | 18639.886 | 34941 |
| dispatch_rows_per_s | 18637.755 | 34941 |
| dispatch_waiting_receives | 775.612 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 3.061 | 92 |
| host_cpu_pct | 58.671 | 69.13 |
| in_flight | 108824.306 | 397858 |
| nacks_per_s | 833.996 | 2109.9 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 2 | 9 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.996 | 100 |
| pg_conn_active | 21.735 | 45 |
| pg_conn_idle | 57.388 | 109 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 11.735 | 32 |
| pg_conn_total | 79.122 | 116 |
| pg_db_size_mb | 3018.563 | 4458.3 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 3.4 | 27.05 |
| pg_replica_replay_lag_ms | 14.792 | 92.5 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | -0.029 | 0 |
| pg_temp_mb_per_s | 9.873 | 40.26 |
| pg_tps | -423777.296 | 44442 |
| pg_tup_del_per_s | -324700.186 | 82482.3 |
| pg_tup_fetch_per_s | -3127635.571 | 2293189.4 |
| pg_tup_ins_per_s | -496997.749 | 89868.7 |
| pg_tup_upd_per_s | -578283.112 | 57283 |
| pg_wal_mb_per_s | -1160.666 | 195.52 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 1.041 | 11 |
| pgb_sv_used | 15.388 | 18 |
| produced_per_s | 24045.341 | 58761.2 |
| produced_total | 868629.571 | 1447428 |
| publish_mb_per_s | 64.033 | 151.47 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15312/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 1,598 records not Completed |
| record_counts | **FAIL** | 189,036 records found, 193,922 expected; 688 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 1,443,177 events applied by workers, 1,487,617 generated |
| broker_drained | **FAIL** | 44,195 messages left in the broker |
| runtime_ordering_gap | **FAIL** | 1,536,138 observed by workers |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 697,667 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 9888 ms, max 65269 ms) |

Sample mismatches:

- source 2306: record 9904194584583 left in status Open (version 7)
- source 9089: record 39036957753361 left in status Open (version 4)
- source 7771: record 33376190857231 left in status Open (version 6)
- source 5212: record 22385369546757 left in status Open (version 9)
- source 2856: record 12266426597380 left in status Open (version 4)
- source 8814: record 37855841746951 left in status Open (version 3)
- source 11188: record 48052094107649 left in status Open (version 4)
- source 4457: record 19142669238275 left in status Open (version 4)
- source 8429: record 36202279337990 left in status Open (version 5)
- source 5621: record 24142011170823 left in status Open (version 21)
- source 3990: record 17136919511055 left in status Open (version 4)
- source 8760: record 37623913512967 left in status Open (version 6)
- source 13216: record 56762287783937 left in status Open (version 9)
- source 3079: record 13224204304385 left in status Open (version 16)
- source 3727: record 16007343112198 left in status Open (version 1)
- source 2364: record 10153302687747 left in status Open (version 3)
- source 1525: record 6549825126409 left in status Open (version 4)
- source 15115: record 64918430679043 left in status Open (version 4)
- source 8903: record 38238093836289 left in status Open (version 15)
- source 10800: record 46385646796806 left in status Open (version 3)

## Cross-source blocking samples

- [cross_source_blocking] source 5800 seq 16: ready 103 ms before start, 103 ms of it with an idle slot in group 200
- [cross_source_blocking] source 14367 seq 8: ready 132 ms before start, 132 ms of it with an idle slot in group 458
- [cross_source_blocking] source 14361 seq 6: ready 121 ms before start, 121 ms of it with an idle slot in group 458
- [cross_source_blocking] source 4615 seq 25: ready 116 ms before start, 116 ms of it with an idle slot in group 161
- [cross_source_blocking] source 4607 seq 23: ready 135 ms before start, 135 ms of it with an idle slot in group 161
- [cross_source_blocking] source 9370 seq 6: ready 120 ms before start, 120 ms of it with an idle slot in group 325
- [cross_source_blocking] source 4637 seq 24: ready 104 ms before start, 104 ms of it with an idle slot in group 161
- [cross_source_blocking] source 4628 seq 21: ready 105 ms before start, 105 ms of it with an idle slot in group 161
- [cross_source_blocking] source 13033 seq 34: ready 110 ms before start, 110 ms of it with an idle slot in group 415
- [cross_source_blocking] source 4616 seq 19: ready 106 ms before start, 106 ms of it with an idle slot in group 161
- [cross_source_blocking] source 143 seq 25: ready 142 ms before start, 142 ms of it with an idle slot in group 2
- [cross_source_blocking] source 147 seq 34: ready 139 ms before start, 139 ms of it with an idle slot in group 2
- [cross_source_blocking] source 1736 seq 5: ready 169 ms before start, 169 ms of it with an idle slot in group 50
- [cross_source_blocking] source 3089 seq 12: ready 203 ms before start, 203 ms of it with an idle slot in group 103
- [cross_source_blocking] source 3080 seq 24: ready 207 ms before start, 207 ms of it with an idle slot in group 103
- [cross_source_blocking] source 13029 seq 25: ready 140 ms before start, 140 ms of it with an idle slot in group 415
- [cross_source_blocking] source 4559 seq 20: ready 126 ms before start, 126 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4550 seq 21: ready 217 ms before start, 217 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4576 seq 17: ready 191 ms before start, 191 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4595 seq 25: ready 126 ms before start, 126 ms of it with an idle slot in group 160

## Runtime violation samples

- [ordering_gap] source 9658: received seq 75 while store cursor is 73
- [ordering_gap] source 5050: received seq 68 while store cursor is 66
- [ordering_gap] source 8890: received seq 71 while store cursor is 69
- [ordering_gap] source 12250: received seq 31 while store cursor is 29
- [ordering_gap] source 14232: received seq 158 while store cursor is 156
- [ordering_gap] source 15864: received seq 160 while store cursor is 158
- [ordering_gap] source 2266: received seq 146 while store cursor is 144
- [ordering_gap] source 3226: received seq 65 while store cursor is 63
- [ordering_gap] source 3514: received seq 98 while store cursor is 96
- [ordering_gap] source 3930: received seq 72 while store cursor is 70
- [ordering_gap] source 3994: received seq 174 while store cursor is 172
- [ordering_gap] source 9016: received seq 32 while store cursor is 30
- [ordering_gap] source 15992: received seq 46 while store cursor is 44
- [ordering_gap] source 15224: received seq 21 while store cursor is 19
- [ordering_gap] source 14968: received seq 77 while store cursor is 75
- [ordering_gap] source 6413: received seq 40 while store cursor is 38
- [ordering_gap] source 3821: received seq 81 while store cursor is 79
- [ordering_gap] source 216: received seq 85 while store cursor is 83
- [ordering_gap] source 4184: received seq 28 while store cursor is 26
- [ordering_gap] source 5229: received seq 132 while store cursor is 130

## Error samples

- publish p13 attempt 1: NpgsqlException: Exception while reading from stream
- publish p26 attempt 1: NpgsqlException: Exception while reading from stream
- publish p24 attempt 1: NpgsqlException: Exception while reading from stream
- publish p4 attempt 1: NpgsqlException: Exception while reading from stream
- publish p9 attempt 1: NpgsqlException: Exception while reading from stream
- publish p3 attempt 1: NpgsqlException: Exception while reading from stream
- t-040#1 src=1239 seq=19 op=Get rec=5321464479745 ev=12: NpgsqlException: No suitable host was found.
- publish p11 attempt 1: NpgsqlException: Exception while reading from stream
- publish p14 attempt 1: NpgsqlException: Exception while reading from stream
- publish p31 attempt 1: NpgsqlException: Exception while reading from stream
- t-080#1 src=2624 seq=26 op=Update rec=11269994184706 ev=3: NpgsqlException: No suitable host was found.
- publish p5 attempt 1: NpgsqlException: Exception while reading from stream
- t-347#6 src=10392 seq=82 op=Complete rec=44633300140044 ev=2: NpgsqlException: No suitable host was found.
- t-147#0 src=4331 seq=173 op=Complete rec=18601503358996 ev=2: NpgsqlException: No suitable host was found.
- t-372#1 src=11437 seq=42 op=Get rec=49121540964357 ev=2: NpgsqlException: No suitable host was found.
- t-499#0 src=15984 seq=63 op=Update rec=68650757259269 ev=12: NpgsqlException: No suitable host was found.
- publish p8 attempt 1: NpgsqlException: Exception while reading from stream
- t-182#1 src=5255 seq=70 op=Get rec=22570053140487 ev=9: NpgsqlException: No suitable host was found.
- publish p1 attempt 1: NpgsqlException: Exception while reading from stream
- t-464#0 src=14761 seq=40 op=Complete rec=63398012256260 ev=3: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 3159 | 3002 | 0 | 2703.359 | 77594.623 | 79691.775 |
| t-439 | 22 | 1 | 127.568 | 47.568 | 3339 | 0 | 2822 | 2589 | 0 | 3391.487 | 77070.335 | 77070.335 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 6153 | 5985 | 0 | 5832.703 | 76021.759 | 77070.335 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3190 | 3133 | 0 | 5603.327 | 74448.895 | 75497.471 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9134 | 9013 | 0 | 6258.687 | 73924.607 | 75497.471 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15095 | 14565 | 0 | 5144.575 | 73924.607 | 75497.471 |
| t-379 | 57 | 2 | 88.878 | 84.104 | 591 | 0 | 5123 | 4860 | 0 | 5636.095 | 73924.607 | 74448.895 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9094 | 8642 | 0 | 4816.895 | 73400.319 | 76021.759 |
| t-474 | 63 | 1 | 34.449 | 35.534 | 375 | 0 | 2288 | 2222 | 0 | 2506.751 | 73400.319 | 73924.607 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8828 | 8393 | 0 | 4390.911 | 72876.031 | 74448.895 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 3091 | 3049 | 0 | 4816.895 | 72351.743 | 73400.319 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 19492 | 18700 | 0 | 2981.887 | 72351.743 | 73924.607 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9131 | 8741 | 0 | 3571.711 | 72351.743 | 74973.183 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 14632 | 14170 | 0 | 3375.103 | 71827.455 | 73924.607 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 3061 | 2853 | 0 | 3440.639 | 70778.879 | 70778.879 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6485 | 6403 | 0 | 4390.911 | 70778.879 | 71827.455 |
| t-461 | 130 | 4 | 84.209 | 183.191 | 3025 | 0 | 11266 | 10926 | 0 | 5144.575 | 70778.879 | 70778.879 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3100 | 3022 | 0 | 3637.247 | 69730.303 | 71303.167 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30765 | 29622 | 0 | 3407.871 | 69730.303 | 72351.743 |
| t-104 | 46 | 1 | 59.619 | 45.723 | 2163 | 0 | 2803 | 2720 | 0 | 3817.471 | 69206.015 | 69730.303 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15228 | 14848 | 0 | 5242.879 | 68681.727 | 70254.591 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11943 | 11266 | 0 | 2539.519 | 68157.439 | 69730.303 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 15040 | 14292 | 0 | 2621.439 | 67633.151 | 69730.303 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29462 | 28612 | 0 | 4489.215 | 67633.151 | 69730.303 |
| t-443 | 49 | 2 | 106.945 | 88.851 | 261 | 0 | 5468 | 5231 | 0 | 2719.743 | 67633.151 | 69206.015 |
