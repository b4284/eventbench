# B3-pgqueue-24k-pub1-conc-nonotify — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:6432/bench; head reads without vt writes, notify=False, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.102s, closing 0.738s, drain 69.139s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487632 | 1487632 | 24044.022 | 12879.11 | 47176.2 | 0 | 0 | 0 | 425 | 7514 | 976880 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1202209 | 33043.436 | 30179.327 | 64618.495 | 70254.591 | 72548.351 | 76087.295 |
| e2e_from_send | 1202209 | 33041.525 | 30179.327 | 64618.495 | 70254.591 | 72548.351 | 76087.295 |
| producer_lag | 1202209 | 1.91 | 0.749 | 3.367 | 23.583 | 93.503 | 141.439 |
| publish_call | 1487632 | 3.19 | 1.276 | 5.227 | 35.039 | 180.607 | 282.111 |
| queue_wait | 1202209 | 33020.146 | 30162.943 | 64618.495 | 70189.055 | 72482.815 | 75759.615 |
| broker_dwell | 1202209 | 33018.487 | 30162.943 | 64618.495 | 70189.055 | 72482.815 | 75759.615 |
| receive_call | 808218 | 91.261 | 71.679 | 159.999 | 406.271 | 488.447 | 520.703 |
| ctx_load | 1202209 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 11.535 |
| process | 1202209 | 10.391 | 0.001 | 0.002 | 0.004 | 2613.247 | 3002.367 |
| ctx_commit | 1202209 | 4.92 | 4.311 | 8.255 | 18.143 | 41.055 | 142.207 |
| service | 1202209 | 21.393 | 9.807 | 17.263 | 47.839 | 2623.487 | 3110.911 |
| e2e_large | 324 | 2524.117 | 2435.071 | 3325.951 | 5144.575 | 6324.223 | 6324.223 |
| ready_wait | 1202209 | 1070.335 | 453.375 | 2652.159 | 8765.439 | 18612.223 | 53116.927 |
| blocked | 1202209 | 1070.164 | 453.119 | 2652.159 | 8765.439 | 18612.223 | 53116.927 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12879.11 | 21041.8 |
| applied_total | 537063.184 | 802949 |
| bench_cpu_cores | 5.751 | 6.077 |
| bench_cpu_pct | 23.962 | 25.32 |
| bench_gc_gen0 | 2.082 | 3 |
| bench_gc_gen1 | 1.408 | 3 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 876.508 | 1059.9 |
| bench_mem_mb | 1349.778 | 1435.3 |
| bench_threadpool_queue | 8.429 | 80 |
| bench_threadpool_threads | 39 | 41 |
| broker_backlog | 316985.286 | 610085 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.299 | 27.18 |
| ctr_eb_pgbouncer_2_mem_mb | 5.635 | 7.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 23.181 | 27.7 |
| ctr_eb_pgbouncer_3_mem_mb | 5.737 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 24.519 | 28.65 |
| ctr_eb_pgbouncer_4_mem_mb | 6.922 | 7.9 |
| ctr_eb_pgbouncer_cpu_pct | 23.631 | 28.21 |
| ctr_eb_pgbouncer_mem_mb | 5.608 | 6.6 |
| ctr_eb_postgres_cpu_pct | 897.624 | 995.04 |
| ctr_eb_postgres_mem_mb | 11592.79 | 12130.7 |
| dispatch_empty_reads_per_s | 270.598 | 517.9 |
| dispatch_fetch_calls_per_s | 82.704 | 125.4 |
| dispatch_group_reads_per_s | 4476.298 | 6660.5 |
| dispatch_in_flight | 239.98 | 525 |
| dispatch_notifies_per_s | 0 | 0 |
| dispatch_preloaded_per_s | 13013.41 | 19286.9 |
| dispatch_rows_per_s | 13014.112 | 19286.9 |
| dispatch_waiting_receives | 778.51 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 58.253 | 67.4 |
| in_flight | 333159.041 | 644428 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 2.082 | 5 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.837 | 100 |
| pg_conn_active | 26.592 | 182 |
| pg_conn_idle | 166.571 | 189 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 193.163 | 194 |
| pg_db_size_mb | 3705.341 | 4524.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24864.798 | 26995.4 |
| pg_tup_del_per_s | 36477.629 | 55168.4 |
| pg_tup_fetch_per_s | 249719.837 | 343008.2 |
| pg_tup_ins_per_s | 67002.906 | 83080.7 |
| pg_tup_upd_per_s | 21878.414 | 32346 |
| pg_wal_mb_per_s | 129.029 | 221.24 |
| pgb_cl_active | 250.776 | 272 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.939 | 17 |
| pgb_sv_idle | 43.061 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24044.022 | 24838.7 |
| produced_total | 870222.224 | 1447377 |
| publish_mb_per_s | 64.03 | 89.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,924 records are Completed |
| record_counts | pass | 193,924 records found, 193,924 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,632 events applied by workers, 1,487,632 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 976,880 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 8765 ms, max 53103 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13993 seq 7: ready 105 ms before start, 105 ms of it with an idle slot in group 445
- [cross_source_blocking] source 14869 seq 11: ready 108 ms before start, 108 ms of it with an idle slot in group 466
- [cross_source_blocking] source 14872 seq 9: ready 109 ms before start, 109 ms of it with an idle slot in group 467
- [cross_source_blocking] source 14864 seq 8: ready 119 ms before start, 119 ms of it with an idle slot in group 466
- [cross_source_blocking] source 13181 seq 11: ready 109 ms before start, 109 ms of it with an idle slot in group 419
- [cross_source_blocking] source 9681 seq 18: ready 113 ms before start, 113 ms of it with an idle slot in group 336
- [cross_source_blocking] source 12270 seq 15: ready 103 ms before start, 103 ms of it with an idle slot in group 390
- [cross_source_blocking] source 993 seq 15: ready 137 ms before start, 137 ms of it with an idle slot in group 30
- [cross_source_blocking] source 1007 seq 7: ready 148 ms before start, 148 ms of it with an idle slot in group 30
- [cross_source_blocking] source 8906 seq 12: ready 179 ms before start, 179 ms of it with an idle slot in group 307
- [cross_source_blocking] source 4259 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 143
- [cross_source_blocking] source 4208 seq 6: ready 193 ms before start, 193 ms of it with an idle slot in group 139
- [cross_source_blocking] source 4195 seq 7: ready 136 ms before start, 136 ms of it with an idle slot in group 139
- [cross_source_blocking] source 5552 seq 12: ready 110 ms before start, 110 ms of it with an idle slot in group 190
- [cross_source_blocking] source 6659 seq 8: ready 148 ms before start, 148 ms of it with an idle slot in group 225
- [cross_source_blocking] source 5459 seq 27: ready 110 ms before start, 110 ms of it with an idle slot in group 187
- [cross_source_blocking] source 5458 seq 19: ready 123 ms before start, 123 ms of it with an idle slot in group 187
- [cross_source_blocking] source 2937 seq 14: ready 166 ms before start, 166 ms of it with an idle slot in group 94
- [cross_source_blocking] source 10327 seq 9: ready 127 ms before start, 127 ms of it with an idle slot in group 346
- [cross_source_blocking] source 6398 seq 11: ready 142 ms before start, 142 ms of it with an idle slot in group 219

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3190 | 3190 | 0 | 65011.711 | 73924.607 | 74973.183 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15095 | 15095 | 0 | 64749.567 | 73400.319 | 75497.471 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9094 | 9094 | 0 | 65011.711 | 72876.031 | 74448.895 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9131 | 9131 | 0 | 63963.135 | 72351.743 | 73924.607 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 5857 | 5857 | 0 | 63176.703 | 71827.455 | 73924.607 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17986 | 17986 | 0 | 63963.135 | 71827.455 | 76546.047 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15228 | 15228 | 0 | 63963.135 | 71827.455 | 74448.895 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 3091 | 3091 | 0 | 63963.135 | 70778.879 | 72876.031 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9134 | 9134 | 0 | 63700.991 | 70778.879 | 72876.031 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 6153 | 6153 | 0 | 63700.991 | 70778.879 | 73400.319 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29463 | 29463 | 0 | 64225.279 | 70778.879 | 73400.319 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6485 | 6485 | 0 | 60555.263 | 70254.591 | 71303.167 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 8501 | 8501 | 0 | 63176.703 | 70254.591 | 72351.743 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8828 | 8828 | 0 | 62390.271 | 70254.591 | 72351.743 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 15040 | 15040 | 0 | 61079.551 | 69730.303 | 72351.743 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 5948 | 5948 | 0 | 60555.263 | 69730.303 | 70254.591 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30765 | 30765 | 0 | 62390.271 | 69730.303 | 72876.031 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 14632 | 14632 | 0 | 61865.983 | 69730.303 | 72351.743 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2931 | 2931 | 0 | 61079.551 | 69730.303 | 72351.743 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3100 | 3100 | 0 | 60817.407 | 69206.015 | 71303.167 |
| t-230 | 30 | 2 | 195.135 | 96.607 | 1033 | 0 | 5827 | 5827 | 0 | 58982.399 | 69206.015 | 70778.879 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 19492 | 19492 | 0 | 62652.415 | 69206.015 | 71827.455 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5526 | 5526 | 0 | 59506.687 | 69206.015 | 71303.167 |
| t-460 | 71 | 1 | 38.387 | 45.799 | 141 | 0 | 3057 | 3057 | 0 | 62390.271 | 69206.015 | 70254.591 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14343 | 14343 | 0 | 60293.119 | 69206.015 | 71303.167 |
