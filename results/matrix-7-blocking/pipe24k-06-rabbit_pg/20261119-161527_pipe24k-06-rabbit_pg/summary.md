# pipe24k-06-rabbit_pg — FAIL ❌

- Broker: `rabbitmq quorum queues, =workers partitions/group (780 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.662s, closing 2.465s, drain 39.647s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1402760 | 1402760 | 24889.163 | 15938.633 | 22373 | 0 | 0 | 0 | 396 | 7077 | 107581 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1117451 | 21718.203 | 21692.415 | 37847.039 | 44171.263 | 48037.887 | 55410.687 |
| e2e_from_send | 1117451 | 17565.834 | 16924.671 | 33439.743 | 38797.311 | 43089.919 | 46628.863 |
| producer_lag | 1117451 | 4152.371 | 3131.391 | 8331.263 | 9969.663 | 10838.015 | 11386.879 |
| publish_call | 3025 | 661.64 | 445.695 | 1405.951 | 2709.503 | 3629.055 | 3844.095 |
| queue_wait | 1117451 | 17417.714 | 16760.831 | 33341.439 | 38699.007 | 43024.383 | 46563.327 |
| broker_dwell | 1117451 | 17405.712 | 16752.639 | 33341.439 | 38699.007 | 43024.383 | 46563.327 |
| receive_call | 1304905 | 5.88 | 0.008 | 0.016 | 166.399 | 201.215 | 273.663 |
| ctx_load | 1117451 | 67.288 | 66.687 | 97.343 | 125.759 | 153.983 | 185.471 |
| process | 1117451 | 10.409 | 0.001 | 0.003 | 0.011 | 2613.247 | 3002.367 |
| ctx_commit | 1117451 | 66.755 | 65.983 | 96.511 | 124.159 | 157.311 | 513.535 |
| ack | 1112433 | 0.005 | 0.002 | 0.003 | 0.008 | 0.335 | 37.535 |
| service | 1117451 | 148.123 | 134.655 | 195.583 | 363.775 | 2762.751 | 4081.663 |
| e2e_large | 295 | 7564.033 | 7241.727 | 12050.431 | 13787.135 | 14835.711 | 14835.711 |
| ready_wait | 1117451 | 636.937 | 235.391 | 1727.487 | 5210.111 | 10305.535 | 29638.655 |
| blocked | 1117451 | 44.741 | 3.943 | 94.847 | 688.127 | 1459.199 | 5160.959 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15938.633 | 22373 |
| applied_total | 509078.327 | 895054 |
| bench_cpu_cores | 3.855 | 4.877 |
| bench_cpu_pct | 16.062 | 20.32 |
| bench_gc_gen0 | 3.306 | 6 |
| bench_gc_gen1 | 1.878 | 4 |
| bench_gc_gen2 | 1.49 | 3 |
| bench_gc_heap_mb | 425.769 | 663 |
| bench_mem_mb | 859.471 | 1013 |
| bench_threadpool_queue | 18.327 | 282 |
| bench_threadpool_threads | 34.224 | 40 |
| broker_backlog | 210654.633 | 404862 |
| broker_consumers | 780 | 780 |
| broker_ready | 188129 | 379670 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 22525.633 | 25783 |
| ctr_eb_kafka_cpu_pct | 1.795 | 2.21 |
| ctr_eb_kafka_mem_mb | 1882.569 | 1909.3 |
| ctr_eb_mssql_cpu_pct | 2.013 | 2.17 |
| ctr_eb_mssql_mem_mb | 2306.51 | 2367 |
| ctr_eb_nats_cpu_pct | 0.522 | 0.7 |
| ctr_eb_nats_mem_mb | 4334.239 | 4337.3 |
| ctr_eb_pgbouncer_cpu_pct | 78.333 | 83.63 |
| ctr_eb_pgbouncer_mem_mb | 32.659 | 39.7 |
| ctr_eb_postgres_cpu_pct | 421.467 | 455.71 |
| ctr_eb_postgres_mem_mb | 4924.743 | 4941.9 |
| ctr_eb_rabbitmq_cpu_pct | 614.551 | 701.98 |
| ctr_eb_rabbitmq_mem_mb | 2407.729 | 2835.3 |
| ctr_eb_redis_cpu_pct | 0.298 | 0.31 |
| ctr_eb_redis_mem_mb | 1371.992 | 1416.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 68.702 | 73.86 |
| in_flight | 240038.49 | 450896 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 5.653 | 29 |
| pg_conn_idle | 58.347 | 63 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1717.739 | 2172.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 32221.694 | 39385.6 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 73841.153 | 89552.7 |
| pg_tup_ins_per_s | 11566.245 | 16105.7 |
| pg_tup_upd_per_s | 26595.62 | 32787.9 |
| pg_wal_mb_per_s | 37.268 | 47.49 |
| pgb_cl_active | 375.51 | 1000 |
| pgb_cl_waiting | 624.49 | 853 |
| pgb_maxwait_ms | 22.49 | 37.2 |
| pgb_sv_active | 55.102 | 64 |
| pgb_sv_idle | 8.898 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24889.163 | 42060 |
| produced_total | 749116.816 | 1345950 |
| publish_mb_per_s | 67.313 | 121.4 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 184,351 records are Completed |
| record_counts | pass | 184,351 records found, 184,351 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 396 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,402,760 events applied by workers, 1,402,760 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 107,581 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 688 ms, max 5161 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 272 seq 6: ready 207 ms before start, 207 ms of it with an idle slot in group 6
- [cross_source_blocking] source 4185 seq 10: ready 146 ms before start, 146 ms of it with an idle slot in group 137
- [cross_source_blocking] source 9529 seq 10: ready 146 ms before start, 146 ms of it with an idle slot in group 331
- [cross_source_blocking] source 2640 seq 6: ready 215 ms before start, 215 ms of it with an idle slot in group 81
- [cross_source_blocking] source 10617 seq 13: ready 146 ms before start, 146 ms of it with an idle slot in group 349
- [cross_source_blocking] source 8223 seq 10: ready 112 ms before start, 112 ms of it with an idle slot in group 277
- [cross_source_blocking] source 6394 seq 13: ready 183 ms before start, 183 ms of it with an idle slot in group 219
- [cross_source_blocking] source 13209 seq 20: ready 193 ms before start, 193 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9337 seq 12: ready 192 ms before start, 192 ms of it with an idle slot in group 323
- [cross_source_blocking] source 4272 seq 11: ready 437 ms before start, 437 ms of it with an idle slot in group 144
- [cross_source_blocking] source 10719 seq 8: ready 267 ms before start, 267 ms of it with an idle slot in group 351
- [cross_source_blocking] source 9465 seq 20: ready 128 ms before start, 128 ms of it with an idle slot in group 327
- [cross_source_blocking] source 4537 seq 10: ready 438 ms before start, 438 ms of it with an idle slot in group 158
- [cross_source_blocking] source 4025 seq 6: ready 438 ms before start, 438 ms of it with an idle slot in group 135
- [cross_source_blocking] source 6815 seq 27: ready 140 ms before start, 140 ms of it with an idle slot in group 232
- [cross_source_blocking] source 12031 seq 9: ready 442 ms before start, 292 ms of it with an idle slot in group 384
- [cross_source_blocking] source 7257 seq 6: ready 440 ms before start, 201 ms of it with an idle slot in group 247
- [cross_source_blocking] source 1136 seq 14: ready 341 ms before start, 341 ms of it with an idle slot in group 38
- [cross_source_blocking] source 1081 seq 12: ready 233 ms before start, 233 ms of it with an idle slot in group 34
- [cross_source_blocking] source 7263 seq 13: ready 486 ms before start, 486 ms of it with an idle slot in group 248

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5419 | 5419 | 0 | 35913.727 | 51118.079 | 51904.511 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 14204 | 14204 | 0 | 34340.863 | 50593.791 | 55574.527 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11224 | 11224 | 0 | 34865.151 | 48758.783 | 51380.223 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6138 | 6138 | 0 | 37486.591 | 48234.495 | 50855.935 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 8620 | 8620 | 0 | 35913.727 | 47710.207 | 50855.935 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 5553 | 5553 | 0 | 33816.575 | 47185.919 | 48758.783 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 2934 | 2934 | 0 | 37748.735 | 46923.775 | 49020.927 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 2995 | 2995 | 0 | 36438.015 | 46923.775 | 48758.783 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 8571 | 8571 | 0 | 36175.871 | 46923.775 | 48496.639 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17009 | 17009 | 0 | 35389.439 | 46923.775 | 49283.071 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 14148 | 14148 | 0 | 35913.727 | 46661.631 | 49545.215 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 2890 | 2890 | 0 | 36962.303 | 46137.343 | 47185.919 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 2840 | 2840 | 0 | 36700.159 | 46137.343 | 48496.639 |
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 2995 | 2995 | 0 | 36700.159 | 46137.343 | 47972.351 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5277 | 5277 | 0 | 32243.711 | 46137.343 | 48496.639 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 13443 | 13443 | 0 | 33423.359 | 46137.343 | 49545.215 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 23885 | 23885 | 0 | 32505.855 | 45875.199 | 51380.223 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 14332 | 14332 | 0 | 35389.439 | 45875.199 | 49283.071 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8330 | 8330 | 0 | 33423.359 | 45613.055 | 48234.495 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 21172 | 21172 | 0 | 32112.639 | 45613.055 | 48234.495 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 10639 | 10639 | 0 | 33554.431 | 45350.911 | 47710.207 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 27772 | 27772 | 0 | 34603.007 | 45350.911 | 47448.063 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 5569 | 5569 | 0 | 35389.439 | 45350.911 | 47710.207 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 2739 | 2739 | 0 | 34603.007 | 45088.767 | 45613.055 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 7995 | 7995 | 0 | 33161.215 | 45088.767 | 46661.631 |
