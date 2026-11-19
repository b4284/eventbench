# pipe12k-01-pgmq_pg-txack — FAIL ❌

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.319s, closing 0.576s, drain 28.784s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814137 | 814137 | 12857.163 | 9406.859 | 11310.1 | 0 | 0 | 0 | 430 | 4252 | 412120 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642418 | 14629.522 | 13934.591 | 30769.151 | 36143.103 | 37781.503 | 39944.191 |
| e2e_from_send | 642418 | 14596.298 | 13901.823 | 30736.383 | 36110.335 | 37748.735 | 39911.423 |
| producer_lag | 642418 | 33.225 | 28.959 | 55.807 | 190.207 | 332.031 | 495.359 |
| publish_call | 32796 | 58.866 | 55.807 | 69.247 | 181.247 | 349.695 | 478.207 |
| queue_wait | 642418 | 14503.057 | 13795.327 | 30687.231 | 36044.799 | 37715.967 | 39813.119 |
| broker_dwell | 642418 | 14455.718 | 13746.175 | 30638.079 | 36012.031 | 37650.431 | 39780.351 |
| receive_call | 665539 | 42.858 | 47.135 | 60.095 | 70.655 | 107.391 | 1098.751 |
| ctx_load | 642418 | 40.898 | 45.503 | 58.975 | 72.447 | 87.359 | 157.439 |
| process | 642418 | 10.906 | 0.001 | 0.003 | 0.007 | 2625.535 | 3004.415 |
| ctx_commit | 642418 | 40.658 | 44.959 | 58.943 | 72.895 | 91.775 | 333.823 |
| service | 642418 | 93.246 | 92.351 | 117.375 | 152.447 | 2719.743 | 3223.551 |
| e2e_large | 330 | 2914.135 | 2828.287 | 4114.431 | 5472.255 | 6508.543 | 6508.543 |
| ready_wait | 642418 | 895.975 | 295.167 | 2430.975 | 7733.247 | 14991.359 | 31948.799 |
| blocked | 642418 | 880.358 | 290.303 | 2387.967 | 7589.887 | 14606.335 | 30932.991 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9406.859 | 11298.5 |
| applied_total | 330580.714 | 555390 |
| bench_cpu_cores | 2.29 | 2.685 |
| bench_cpu_pct | 9.542 | 11.19 |
| bench_gc_gen0 | 1.796 | 3 |
| bench_gc_gen1 | 1.653 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1146.163 | 1450.5 |
| bench_mem_mb | 1463.973 | 1574.6 |
| bench_threadpool_queue | 14.184 | 106 |
| bench_threadpool_threads | 31.449 | 44 |
| broker_backlog | 131795.286 | 215965 |
| broker_oldest_age_s | 19.959 | 33 |
| broker_visible | 129739.469 | 213815 |
| ctr_eb_kafka_cpu_pct | 1.829 | 2.22 |
| ctr_eb_kafka_mem_mb | 1960.408 | 2020.8 |
| ctr_eb_mssql_cpu_pct | 2.244 | 2.67 |
| ctr_eb_mssql_mem_mb | 4271.49 | 4272.5 |
| ctr_eb_nats_cpu_pct | 1.464 | 13.17 |
| ctr_eb_nats_mem_mb | 2073.006 | 2256.4 |
| ctr_eb_pgbouncer_cpu_pct | 96.736 | 98.07 |
| ctr_eb_pgbouncer_mem_mb | 25.131 | 33.2 |
| ctr_eb_postgres_cpu_pct | 1540.746 | 1696.13 |
| ctr_eb_postgres_mem_mb | 4488.441 | 4614.8 |
| ctr_eb_rabbitmq_cpu_pct | 17.389 | 17.93 |
| ctr_eb_rabbitmq_mem_mb | 1089.322 | 1255 |
| ctr_eb_redis_cpu_pct | 0.322 | 0.37 |
| ctr_eb_redis_mem_mb | 1350.4 | 1350.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 84.834 | 90.72 |
| in_flight | 137164.041 | 221138 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.981 | 100 |
| pg_conn_active | 18.327 | 29 |
| pg_conn_idle | 47.184 | 63 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.51 | 66 |
| pg_db_size_mb | 2047.492 | 2670.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 29933.459 | 34448.1 |
| pg_tup_del_per_s | 30340.661 | 37301.2 |
| pg_tup_fetch_per_s | 347901.247 | 508118.9 |
| pg_tup_ins_per_s | 47131.673 | 62206.8 |
| pg_tup_upd_per_s | 24802.049 | 28057.1 |
| pg_wal_mb_per_s | 106.331 | 210.51 |
| pgb_cl_active | 314.429 | 664 |
| pgb_cl_waiting | 1314.571 | 1471 |
| pgb_maxwait_ms | 42.807 | 54.99 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12857.163 | 13832.8 |
| produced_total | 467744.755 | 776528 |
| publish_mb_per_s | 44.34 | 66.73 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,540 records are Completed |
| record_counts | pass | 117,540 records found, 117,540 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,137 events applied by workers, 814,137 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 412,120 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 7590 ms, max 30932 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8517 seq 9: ready 102 ms before start, 100 ms of it with an idle slot in group 292
- [cross_source_blocking] source 8619 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 298
- [cross_source_blocking] source 10945 seq 6: ready 133 ms before start, 132 ms of it with an idle slot in group 357
- [cross_source_blocking] source 12886 seq 7: ready 111 ms before start, 102 ms of it with an idle slot in group 408
- [cross_source_blocking] source 5472 seq 8: ready 112 ms before start, 112 ms of it with an idle slot in group 188
- [cross_source_blocking] source 15070 seq 5: ready 152 ms before start, 144 ms of it with an idle slot in group 470
- [cross_source_blocking] source 9296 seq 16: ready 125 ms before start, 124 ms of it with an idle slot in group 320
- [cross_source_blocking] source 12039 seq 12: ready 107 ms before start, 106 ms of it with an idle slot in group 385
- [cross_source_blocking] source 12909 seq 3: ready 118 ms before start, 118 ms of it with an idle slot in group 408
- [cross_source_blocking] source 3326 seq 4: ready 102 ms before start, 102 ms of it with an idle slot in group 113
- [cross_source_blocking] source 1337 seq 7: ready 205 ms before start, 132 ms of it with an idle slot in group 42
- [cross_source_blocking] source 1341 seq 4: ready 170 ms before start, 126 ms of it with an idle slot in group 42
- [cross_source_blocking] source 2455 seq 7: ready 196 ms before start, 189 ms of it with an idle slot in group 72
- [cross_source_blocking] source 5568 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 192
- [cross_source_blocking] source 15674 seq 13: ready 132 ms before start, 132 ms of it with an idle slot in group 489
- [cross_source_blocking] source 4273 seq 9: ready 102 ms before start, 102 ms of it with an idle slot in group 145
- [cross_source_blocking] source 12391 seq 10: ready 115 ms before start, 115 ms of it with an idle slot in group 393
- [cross_source_blocking] source 12358 seq 8: ready 124 ms before start, 124 ms of it with an idle slot in group 392
- [cross_source_blocking] source 13175 seq 22: ready 138 ms before start, 133 ms of it with an idle slot in group 418
- [cross_source_blocking] source 14407 seq 6: ready 133 ms before start, 124 ms of it with an idle slot in group 459

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12122 | 12122 | 0 | 34078.719 | 38010.879 | 40108.031 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6075 | 6075 | 0 | 31981.567 | 37486.591 | 39583.743 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6272 | 6272 | 0 | 32112.639 | 36962.303 | 39059.455 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6333 | 6333 | 0 | 32243.711 | 36438.015 | 38273.023 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12246 | 12246 | 0 | 32505.855 | 36438.015 | 38797.311 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 31850.495 | 35913.727 | 38535.167 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3058 | 3058 | 0 | 31981.567 | 35651.583 | 37486.591 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8786 | 8786 | 0 | 30932.991 | 35127.295 | 37748.735 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3005 | 3005 | 0 | 31326.207 | 35127.295 | 36962.303 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5925 | 5925 | 0 | 31195.135 | 34865.151 | 36962.303 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 29622.271 | 34603.007 | 35913.727 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 31064.063 | 34340.863 | 36700.159 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 30670.847 | 34078.719 | 35913.727 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3023 | 3023 | 0 | 29753.343 | 34078.719 | 35913.727 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 29229.055 | 33554.431 | 34865.151 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5906 | 5906 | 0 | 29753.343 | 33554.431 | 35651.583 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16746 | 16746 | 0 | 29622.271 | 33554.431 | 36700.159 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2906 | 2906 | 0 | 29097.983 | 33161.215 | 35913.727 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13782 | 13782 | 0 | 28573.695 | 32374.783 | 34865.151 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5699 | 5699 | 0 | 28704.767 | 32374.783 | 34078.719 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2895 | 2895 | 0 | 29229.055 | 32374.783 | 34340.863 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8400 | 8400 | 0 | 28311.551 | 32374.783 | 33816.575 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2975 | 2975 | 0 | 27000.831 | 32243.711 | 33030.143 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8598 | 8598 | 0 | 28180.479 | 31981.567 | 34340.863 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5488 | 5488 | 0 | 28049.407 | 31588.351 | 33816.575 |
