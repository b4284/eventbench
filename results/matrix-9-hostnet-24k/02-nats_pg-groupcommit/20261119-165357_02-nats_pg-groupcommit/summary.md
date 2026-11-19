# 02-nats_pg-groupcommit — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.224s, closing 1.636s, drain 8.209s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1485236 | 1485236 | 24048.841 | 23000.965 | 32047.7 | 0 | 0 | 0 | 424 | 7501 | 645620 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1199812 | 2772.525 | 1404.927 | 7340.031 | 12271.615 | 16130.047 | 21135.359 |
| e2e_from_send | 1199812 | 2677.8 | 1307.647 | 7249.919 | 12173.311 | 16039.935 | 21037.055 |
| producer_lag | 1199812 | 94.727 | 82.239 | 185.087 | 333.823 | 488.191 | 744.959 |
| publish_call | 11335 | 173.07 | 154.239 | 267.007 | 477.439 | 680.959 | 858.111 |
| queue_wait | 1199812 | 2641.935 | 1212.415 | 7204.863 | 12107.775 | 15949.823 | 19202.047 |
| broker_dwell | 1199812 | 2584.353 | 1151.999 | 7139.327 | 12050.431 | 15892.479 | 19054.591 |
| receive_call | 861059 | 55.698 | 44.319 | 87.615 | 281.599 | 661.503 | 2592.767 |
| ctx_load | 1199812 | 0.752 | 0.423 | 1.458 | 5.359 | 16.303 | 33.311 |
| process | 1199812 | 10.39 | 0.001 | 0.002 | 0.005 | 2613.247 | 3002.367 |
| ctx_commit | 1199812 | 1.201 | 0.721 | 2.209 | 8.351 | 27.135 | 238.591 |
| ack | 1148881 | 0.057 | 0.002 | 0.018 | 0.675 | 6.243 | 177.919 |
| service | 1199812 | 35.864 | 1.427 | 9.343 | 1443.839 | 2746.367 | 6918.143 |
| e2e_large | 323 | 2441.23 | 2349.055 | 3291.135 | 5263.359 | 7000.063 | 7000.063 |
| ready_wait | 1199812 | 300.858 | 109.823 | 790.527 | 2443.263 | 4710.399 | 12287.999 |
| blocked | 1199812 | 300.722 | 109.759 | 790.527 | 2441.215 | 4706.303 | 12279.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 23000.965 | 24217.7 |
| applied_total | 805890.816 | 1358699 |
| bench_cpu_cores | 4.646 | 4.987 |
| bench_cpu_pct | 19.358 | 20.78 |
| bench_gc_gen0 | 3.041 | 4 |
| bench_gc_gen1 | 3.02 | 4 |
| bench_gc_gen2 | 0.224 | 1 |
| bench_gc_heap_mb | 1133.941 | 1399.2 |
| bench_mem_mb | 1827.176 | 1964 |
| bench_threadpool_queue | 18.224 | 249 |
| bench_threadpool_threads | 37.163 | 44 |
| broker_backlog | 60252.898 | 84001 |
| broker_backlog_mb | 162.46 | 210.457 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_nats_cpu_pct | 417.837 | 439.28 |
| ctr_eb_nats_mem_mb | 5824.751 | 6416.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 24.126 | 24.99 |
| ctr_eb_pgbouncer_2_mem_mb | 13.045 | 13.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 21.966 | 22.4 |
| ctr_eb_pgbouncer_3_mem_mb | 13.02 | 14 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.136 | 27.58 |
| ctr_eb_pgbouncer_4_mem_mb | 13.4 | 14.3 |
| ctr_eb_pgbouncer_cpu_pct | 23.425 | 24.52 |
| ctr_eb_pgbouncer_mem_mb | 12.745 | 13.6 |
| ctr_eb_postgres_cpu_pct | 378.925 | 386.07 |
| ctr_eb_postgres_mem_mb | 3661.465 | 3796.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 58.036 | 59.81 |
| in_flight | 57787.633 | 82157 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.469 | 10 |
| pg_conn_idle | 189.531 | 192 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 192 | 192 |
| pg_db_size_mb | 857.973 | 1327.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 31317.782 | 33767.4 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 106151.894 | 115349.1 |
| pg_tup_ins_per_s | 11997 | 20265.4 |
| pg_tup_upd_per_s | 38086.941 | 40850.8 |
| pg_wal_mb_per_s | 46.202 | 61.05 |
| pgb_cl_active | 253.102 | 276 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0.531 | 3 |
| pgb_sv_idle | 47.469 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24048.841 | 26711.8 |
| produced_total | 863678.449 | 1440856 |
| publish_mb_per_s | 63.924 | 84.01 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,657 records are Completed |
| record_counts | pass | 193,657 records found, 193,657 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 424 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,485,236 events applied by workers, 1,485,236 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 645,620 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2441 ms, max 12274 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3224 seq 3: ready 104 ms before start, 104 ms of it with an idle slot in group 107
- [cross_source_blocking] source 12888 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 408
- [cross_source_blocking] source 440 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 14
- [cross_source_blocking] source 12891 seq 12: ready 105 ms before start, 105 ms of it with an idle slot in group 408
- [cross_source_blocking] source 2459 seq 11: ready 110 ms before start, 110 ms of it with an idle slot in group 72
- [cross_source_blocking] source 7163 seq 20: ready 112 ms before start, 112 ms of it with an idle slot in group 243
- [cross_source_blocking] source 7370 seq 31: ready 105 ms before start, 105 ms of it with an idle slot in group 253
- [cross_source_blocking] source 8362 seq 18: ready 107 ms before start, 107 ms of it with an idle slot in group 283
- [cross_source_blocking] source 8558 seq 21: ready 107 ms before start, 107 ms of it with an idle slot in group 293
- [cross_source_blocking] source 7914 seq 33: ready 109 ms before start, 109 ms of it with an idle slot in group 266
- [cross_source_blocking] source 5066 seq 23: ready 113 ms before start, 113 ms of it with an idle slot in group 177
- [cross_source_blocking] source 4214 seq 14: ready 114 ms before start, 114 ms of it with an idle slot in group 140
- [cross_source_blocking] source 2640 seq 6: ready 101 ms before start, 101 ms of it with an idle slot in group 81
- [cross_source_blocking] source 13390 seq 22: ready 118 ms before start, 118 ms of it with an idle slot in group 428
- [cross_source_blocking] source 7468 seq 21: ready 125 ms before start, 125 ms of it with an idle slot in group 258
- [cross_source_blocking] source 6382 seq 11: ready 120 ms before start, 120 ms of it with an idle slot in group 218
- [cross_source_blocking] source 400 seq 26: ready 107 ms before start, 107 ms of it with an idle slot in group 12
- [cross_source_blocking] source 9200 seq 9: ready 115 ms before start, 115 ms of it with an idle slot in group 316
- [cross_source_blocking] source 12703 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 400
- [cross_source_blocking] source 13150 seq 10: ready 100 ms before start, 100 ms of it with an idle slot in group 417
- [cross_source_blocking] source 8914 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 307

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15079 | 15079 | 0 | 6488.063 | 18874.367 | 21233.663 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6481 | 6481 | 0 | 9371.647 | 17563.647 | 20054.015 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11925 | 11925 | 0 | 5570.559 | 16580.607 | 17563.647 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5757 | 5757 | 0 | 7962.623 | 15728.639 | 17563.647 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25381 | 25381 | 0 | 4112.383 | 14745.599 | 17039.359 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9117 | 9117 | 0 | 6815.743 | 14548.991 | 16187.391 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22457 | 22457 | 0 | 3866.623 | 14548.991 | 16449.535 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15202 | 15202 | 0 | 7176.191 | 14417.919 | 15532.031 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17972 | 17972 | 0 | 6946.815 | 14221.311 | 15990.783 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 5869 | 5869 | 0 | 5767.167 | 13828.095 | 14745.599 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 15015 | 15015 | 0 | 6979.583 | 13828.095 | 14614.527 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9081 | 9081 | 0 | 7241.727 | 13631.487 | 15269.887 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3091 | 3091 | 0 | 10223.615 | 13565.951 | 14483.455 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 3054 | 3054 | 0 | 10092.543 | 13107.199 | 14680.063 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5513 | 5513 | 0 | 3309.567 | 12713.983 | 14024.703 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14322 | 14322 | 0 | 4358.143 | 12517.375 | 15073.279 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8815 | 8815 | 0 | 6389.759 | 12451.839 | 14745.599 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29417 | 29417 | 0 | 6815.743 | 12386.303 | 13762.559 |
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 3154 | 3154 | 0 | 8519.679 | 12320.767 | 13828.095 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9119 | 9119 | 0 | 7045.119 | 12255.231 | 13107.199 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3182 | 3182 | 0 | 7438.335 | 11862.015 | 13041.663 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2926 | 2926 | 0 | 7798.783 | 11599.871 | 14221.311 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 3056 | 3056 | 0 | 7438.335 | 11337.727 | 12189.695 |
| t-235 | 54 | 3 | 148.429 | 133.613 | 527 | 0 | 8033 | 8033 | 0 | 3293.183 | 11206.655 | 12582.911 |
| t-131 | 140 | 6 | 113.781 | 268.058 | 961 | 0 | 16517 | 16517 | 0 | 4014.079 | 11010.047 | 13565.951 |
