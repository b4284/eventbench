# 04-rabbit_pg — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.833s, closing 2.206s, drain 6.694s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 804755 | 804755 | 12456.01 | 12366.637 | 17834.5 | 0 | 0 | 0 | 424 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 633073 | 248.971 | 22.607 | 679.423 | 3172.351 | 3708.927 | 4009.983 |
| e2e_from_send | 633073 | 128.258 | 17.647 | 389.887 | 1592.319 | 2260.991 | 2551.807 |
| producer_lag | 633073 | 120.714 | 4.711 | 263.679 | 2228.223 | 2801.663 | 3004.415 |
| publish_call | 188431 | 10.254 | 5.655 | 11.871 | 73.983 | 701.951 | 2815.999 |
| queue_wait | 633073 | 114.974 | 8.967 | 375.551 | 1585.151 | 2256.895 | 2543.615 |
| broker_dwell | 633073 | 111.301 | 8.959 | 372.223 | 1372.159 | 2177.023 | 2451.455 |
| receive_call | 616036 | 34.898 | 19.055 | 96.639 | 182.015 | 199.423 | 250.367 |
| ctx_load | 633073 | 3.099 | 1.77 | 7.583 | 16.671 | 30.415 | 99.327 |
| process | 633073 | 0.002 | 0.001 | 0.002 | 0.005 | 0.077 | 6.079 |
| ctx_commit | 633073 | 4.476 | 3.067 | 9.271 | 19.167 | 37.663 | 173.055 |
| ack | 625520 | 0.002 | 0.002 | 0.003 | 0.005 | 0.055 | 4.291 |
| service | 633073 | 13.285 | 5.571 | 26.895 | 133.759 | 321.023 | 494.591 |
| e2e_large | 324 | 291.699 | 64.639 | 637.439 | 3440.639 | 3710.975 | 3710.975 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12366.637 | 14687.4 |
| applied_total | 465529.347 | 753730 |
| bench_cpu_cores | 2.379 | 2.656 |
| bench_cpu_pct | 9.913 | 11.07 |
| bench_gc_gen0 | 2 | 3 |
| bench_gc_gen1 | 0.592 | 2 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 512.355 | 668.5 |
| bench_mem_mb | 850.984 | 914 |
| bench_threadpool_queue | 6.143 | 67 |
| bench_threadpool_threads | 32.49 | 44 |
| broker_backlog | 357.306 | 1671 |
| broker_consumers | 598 | 598 |
| broker_ready | 0 | 0 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 357.306 | 1671 |
| ctr_eb_kafka_cpu_pct | 1.793 | 2.57 |
| ctr_eb_kafka_mem_mb | 1808.178 | 1831.1 |
| ctr_eb_mongo_cpu_pct | 1.926 | 22.16 |
| ctr_eb_mongo_mem_mb | 3774.502 | 3907.2 |
| ctr_eb_nats_cpu_pct | 0.427 | 0.57 |
| ctr_eb_nats_mem_mb | 361.741 | 370.3 |
| ctr_eb_pgbouncer_cpu_pct | 56.841 | 61.98 |
| ctr_eb_pgbouncer_mem_mb | 16.692 | 19.8 |
| ctr_eb_postgres_cpu_pct | 364.92 | 403.13 |
| ctr_eb_postgres_mem_mb | 5074.68 | 5103.1 |
| ctr_eb_rabbitmq_cpu_pct | 524.895 | 588.45 |
| ctr_eb_rabbitmq_mem_mb | 1195.827 | 1754.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 53.34 | 60.31 |
| in_flight | -296.612 | 4432 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 21.245 | 64 |
| pg_conn_idle | 42.816 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.061 | 65 |
| pg_db_size_mb | 1448.52 | 1837.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25154.602 | 29917.9 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 57735.61 | 68129.3 |
| pg_tup_ins_per_s | 10326.935 | 19877.3 |
| pg_tup_upd_per_s | 20780.024 | 24592.3 |
| pg_wal_mb_per_s | 32.436 | 52.05 |
| pgb_cl_active | 507.98 | 510 |
| pgb_cl_waiting | 2.02 | 14 |
| pgb_maxwait_ms | 0.138 | 1.38 |
| pgb_sv_active | 23.878 | 64 |
| pgb_sv_idle | 40.122 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12456.01 | 16048.5 |
| produced_total | 465232.735 | 758162 |
| publish_mb_per_s | 43.234 | 71.16 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 116,453 records are Completed |
| record_counts | pass | 116,453 records found, 116,453 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 424 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 804,755 events applied by workers, 804,755 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-073 | 7 | 1 | 19.439 | 2.447 | 1128 | 0 | 154 | 154 | 0 | 23.423 | 3735.551 | 3751.935 |
| t-098 | 6 | 1 | 27.347 | 2.525 | 192 | 0 | 188 | 188 | 0 | 22.143 | 3653.631 | 3784.703 |
| t-151 | 6 | 1 | 23.83 | 2.494 | 577 | 0 | 154 | 154 | 0 | 21.887 | 3653.631 | 3768.319 |
| t-198 | 6 | 1 | 13.16 | 1.151 | 180 | 0 | 79 | 79 | 0 | 19.327 | 3637.247 | 3833.855 |
| t-060 | 7 | 1 | 47.726 | 5.892 | 1546 | 0 | 393 | 393 | 0 | 23.551 | 3555.327 | 3751.935 |
| t-063 | 8 | 1 | 19.658 | 2.724 | 1296 | 0 | 197 | 197 | 0 | 22.911 | 3555.327 | 3784.703 |
| t-114 | 14 | 1 | 48.994 | 11.296 | 1025 | 0 | 688 | 688 | 0 | 21.247 | 3489.791 | 3735.551 |
| t-143 | 21 | 1 | 23.607 | 8.455 | 1094 | 0 | 569 | 569 | 0 | 22.271 | 3489.791 | 3833.855 |
| t-459 | 25 | 1 | 16.76 | 6.839 | 2174 | 0 | 463 | 463 | 0 | 23.551 | 3457.023 | 3866.623 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 408 | 408 | 0 | 55.551 | 3440.639 | 3719.167 |
| t-370 | 12 | 1 | 20.006 | 4.067 | 568 | 0 | 252 | 252 | 0 | 17.535 | 3424.255 | 3555.327 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 2088 | 2088 | 0 | 22.015 | 3424.255 | 3866.623 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1823 | 1823 | 0 | 22.527 | 3407.871 | 3866.623 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 540 | 540 | 0 | 22.911 | 3407.871 | 3719.167 |
| t-455 | 16 | 1 | 50.441 | 13.579 | 3551 | 0 | 837 | 837 | 0 | 21.631 | 3407.871 | 3883.007 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2976 | 2976 | 0 | 23.807 | 3407.871 | 3801.087 |
| t-046 | 24 | 1 | 84.724 | 34.162 | 3235 | 0 | 2181 | 2181 | 0 | 21.631 | 3391.487 | 3801.087 |
| t-047 | 52 | 1 | 34.438 | 30.81 | 343 | 0 | 1983 | 1983 | 0 | 22.911 | 3391.487 | 3833.855 |
| t-188 | 27 | 1 | 39.714 | 18.344 | 599 | 0 | 1256 | 1256 | 0 | 21.631 | 3391.487 | 3866.623 |
| t-254 | 9 | 1 | 21.121 | 3.28 | 3623 | 0 | 201 | 201 | 0 | 23.935 | 3391.487 | 3407.871 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 7335 | 7335 | 0 | 23.039 | 3391.487 | 3899.391 |
| t-340 | 184 | 2 | 24.502 | 75.472 | 141 | 0 | 4907 | 4907 | 0 | 24.319 | 3391.487 | 3915.775 |
| t-368 | 36 | 1 | 18.505 | 10.972 | 2666 | 0 | 724 | 724 | 0 | 23.807 | 3391.487 | 3817.471 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 391 | 391 | 0 | 20.607 | 3391.487 | 3768.319 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2712 | 2712 | 0 | 23.039 | 3375.103 | 3817.471 |
