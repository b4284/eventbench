# pipe24k-05-kafka_pg — FAIL ❌

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=default), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.39s, closing 0.885s, drain 18.773s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1459152 | 1459152 | 23692.857 | 18902.057 | 27006.2 | 0 | 0 | 0 | 416 | 7343 | 133534 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1173790 | 12262.409 | 12746.751 | 22249.471 | 27049.983 | 30326.783 | 34537.471 |
| e2e_from_send | 1173790 | 11134.639 | 11878.399 | 20725.759 | 25427.967 | 28901.375 | 33439.743 |
| producer_lag | 1173790 | 1127.771 | 926.719 | 2260.991 | 3829.759 | 4317.183 | 4567.039 |
| publish_call | 3294 | 589.816 | 629.247 | 773.119 | 934.399 | 1058.815 | 1121.279 |
| queue_wait | 1173790 | 10939.039 | 11673.599 | 20545.535 | 25247.743 | 28639.231 | 31031.295 |
| broker_dwell | 1173790 | 10603.927 | 11337.727 | 20201.471 | 24887.295 | 28278.783 | 30392.319 |
| receive_call | 1371200 | 0.028 | 0.009 | 0.021 | 0.084 | 2.355 | 284.415 |
| ctx_load | 1173790 | 50.251 | 53.919 | 72.127 | 103.615 | 147.711 | 239.359 |
| process | 1173790 | 10.353 | 0.001 | 0.002 | 0.007 | 2611.199 | 3033.087 |
| ctx_commit | 1173790 | 50.076 | 53.631 | 71.615 | 105.215 | 150.527 | 452.863 |
| ack | 1108656 | 0.004 | 0.002 | 0.008 | 0.015 | 0.136 | 67.327 |
| service | 1173790 | 195.602 | 122.111 | 332.287 | 1905.663 | 3094.527 | 7712.767 |
| e2e_large | 315 | 3914.867 | 3772.415 | 5464.063 | 6815.743 | 8593.407 | 8593.407 |
| ready_wait | 1173790 | 463.631 | 54.239 | 1310.719 | 4059.135 | 8159.231 | 22675.455 |
| blocked | 1173790 | 45.366 | 1.418 | 121.215 | 571.903 | 807.423 | 2844.671 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 18902.057 | 21539.9 |
| applied_total | 608349.918 | 1069458 |
| bench_cpu_cores | 6.492 | 6.893 |
| bench_cpu_pct | 27.048 | 28.72 |
| bench_gc_gen0 | 3.816 | 7 |
| bench_gc_gen1 | 3 | 7 |
| bench_gc_gen2 | 2.082 | 3 |
| bench_gc_heap_mb | 362.11 | 504.1 |
| bench_mem_mb | 1800.39 | 2135.5 |
| bench_threadpool_queue | 37.224 | 589 |
| bench_threadpool_threads | 39.551 | 44 |
| broker_backlog | 207166.98 | 318308 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 571566 | 1031316 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 778671.837 | 1349581 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 283.462 | 338.2 |
| ctr_eb_kafka_mem_mb | 1951.214 | 2018.9 |
| ctr_eb_mssql_cpu_pct | 10.931 | 38.33 |
| ctr_eb_mssql_mem_mb | 2914.798 | 3405.6 |
| ctr_eb_nats_cpu_pct | 8.231 | 13.87 |
| ctr_eb_nats_mem_mb | 6404.014 | 6406.8 |
| ctr_eb_pgbouncer_cpu_pct | 91.227 | 94.86 |
| ctr_eb_pgbouncer_mem_mb | 33.41 | 36.6 |
| ctr_eb_postgres_cpu_pct | 496.212 | 519.33 |
| ctr_eb_postgres_mem_mb | 4851.294 | 4874.2 |
| ctr_eb_rabbitmq_cpu_pct | 15.97 | 16.99 |
| ctr_eb_rabbitmq_mem_mb | 1099.235 | 1114.8 |
| ctr_eb_redis_cpu_pct | 0.308 | 0.33 |
| ctr_eb_redis_mem_mb | 1450.769 | 1462 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 73.794 | 77.21 |
| in_flight | 205482.796 | 319514 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 4.612 | 38 |
| pg_conn_idle | 59.388 | 62 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1789.261 | 2244.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 37003.949 | 49576.4 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 84839.465 | 112870 |
| pg_tup_ins_per_s | 11609.704 | 19616 |
| pg_tup_upd_per_s | 30601.855 | 41219.5 |
| pg_wal_mb_per_s | 43.666 | 85.7 |
| pgb_cl_active | 340.286 | 570 |
| pgb_cl_waiting | 659.714 | 818 |
| pgb_maxwait_ms | 17.685 | 25.51 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 23692.857 | 28886.1 |
| produced_total | 813832.714 | 1388972 |
| publish_mb_per_s | 63.315 | 91.27 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 190,724 records are Completed |
| record_counts | pass | 190,724 records found, 190,724 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 416 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,459,152 events applied by workers, 1,459,152 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 133,534 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 572 ms, max 2844 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 866 seq 24: ready 141 ms before start, 141 ms of it with an idle slot in group 25
- [cross_source_blocking] source 9346 seq 7: ready 229 ms before start, 100 ms of it with an idle slot in group 324
- [cross_source_blocking] source 2556 seq 8: ready 118 ms before start, 118 ms of it with an idle slot in group 76
- [cross_source_blocking] source 10626 seq 13: ready 274 ms before start, 274 ms of it with an idle slot in group 349
- [cross_source_blocking] source 2978 seq 11: ready 318 ms before start, 318 ms of it with an idle slot in group 98
- [cross_source_blocking] source 10306 seq 8: ready 325 ms before start, 325 ms of it with an idle slot in group 345
- [cross_source_blocking] source 4209 seq 6: ready 145 ms before start, 145 ms of it with an idle slot in group 139
- [cross_source_blocking] source 2846 seq 9: ready 100 ms before start, 100 ms of it with an idle slot in group 89
- [cross_source_blocking] source 8222 seq 4: ready 109 ms before start, 102 ms of it with an idle slot in group 277
- [cross_source_blocking] source 4208 seq 6: ready 150 ms before start, 150 ms of it with an idle slot in group 139
- [cross_source_blocking] source 1076 seq 23: ready 106 ms before start, 106 ms of it with an idle slot in group 33
- [cross_source_blocking] source 9677 seq 9: ready 525 ms before start, 439 ms of it with an idle slot in group 336
- [cross_source_blocking] source 2570 seq 9: ready 105 ms before start, 105 ms of it with an idle slot in group 78
- [cross_source_blocking] source 5022 seq 8: ready 151 ms before start, 151 ms of it with an idle slot in group 174
- [cross_source_blocking] source 15240 seq 5: ready 155 ms before start, 155 ms of it with an idle slot in group 475
- [cross_source_blocking] source 13667 seq 19: ready 198 ms before start, 147 ms of it with an idle slot in group 435
- [cross_source_blocking] source 8765 seq 21: ready 101 ms before start, 101 ms of it with an idle slot in group 305
- [cross_source_blocking] source 8254 seq 10: ready 167 ms before start, 160 ms of it with an idle slot in group 279
- [cross_source_blocking] source 13923 seq 10: ready 199 ms before start, 199 ms of it with an idle slot in group 442
- [cross_source_blocking] source 8760 seq 14: ready 286 ms before start, 273 ms of it with an idle slot in group 305

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 14802 | 14802 | 0 | 20316.159 | 32243.711 | 34603.007 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5645 | 5645 | 0 | 21102.591 | 31326.207 | 32899.071 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11707 | 11707 | 0 | 19791.871 | 31064.063 | 34340.863 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6368 | 6368 | 0 | 23855.103 | 30408.703 | 32112.639 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 5760 | 5760 | 0 | 20185.087 | 29622.271 | 31719.423 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17679 | 17679 | 0 | 20447.231 | 29360.127 | 32636.927 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 3019 | 3019 | 0 | 22282.239 | 29229.055 | 30408.703 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 8947 | 8947 | 0 | 21626.879 | 29097.983 | 30539.775 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22068 | 22068 | 0 | 18087.935 | 29097.983 | 31064.063 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3132 | 3132 | 0 | 21757.951 | 28835.839 | 30015.487 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 14906 | 14906 | 0 | 21102.591 | 28573.695 | 31981.567 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 8958 | 8958 | 0 | 20971.519 | 28442.623 | 30015.487 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3033 | 3033 | 0 | 22413.311 | 28049.407 | 29229.055 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 14739 | 14739 | 0 | 21102.591 | 28049.407 | 30408.703 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 28892 | 28892 | 0 | 20185.087 | 28049.407 | 30539.775 |
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 3086 | 3086 | 0 | 22020.095 | 27918.335 | 28835.839 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14072 | 14072 | 0 | 18874.367 | 27918.335 | 29622.271 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 2985 | 2985 | 0 | 20709.375 | 27656.191 | 28442.623 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 24934 | 24934 | 0 | 18350.079 | 27525.119 | 32243.711 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 8979 | 8979 | 0 | 20185.087 | 27525.119 | 29229.055 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5420 | 5420 | 0 | 17956.863 | 27394.047 | 29753.343 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 2817 | 2817 | 0 | 19398.655 | 27131.903 | 28180.479 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 5850 | 5850 | 0 | 20316.159 | 27000.831 | 28180.479 |
| t-425 | 89 | 5 | 155.428 | 227.21 | 2911 | 0 | 13435 | 13435 | 0 | 17301.503 | 27000.831 | 28573.695 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 11097 | 11097 | 0 | 19005.439 | 26869.759 | 28180.479 |
