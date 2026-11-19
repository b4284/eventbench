# 09-nats_mssql — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.125s, closing 1.563s, drain 0.306s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814450 | 814450 | 12858.751 | 12859.331 | 19412.6 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642735 | 28.817 | 26.031 | 46.175 | 82.431 | 120.639 | 197.759 |
| e2e_from_send | 642735 | 22.639 | 20.415 | 35.903 | 66.047 | 103.807 | 186.495 |
| producer_lag | 642735 | 6.179 | 4.651 | 13.087 | 29.087 | 48.479 | 100.863 |
| publish_call | 232816 | 7.803 | 6.395 | 14.911 | 31.215 | 65.727 | 1179.647 |
| queue_wait | 642735 | 14.498 | 12.479 | 25.599 | 47.999 | 81.983 | 161.279 |
| broker_dwell | 642735 | 6.429 | 3.809 | 14.791 | 32.751 | 72.191 | 139.903 |
| receive_call | 537951 | 56.641 | 33.215 | 121.407 | 432.383 | 839.679 | 1097.727 |
| ctx_load | 642735 | 1.245 | 0.691 | 2.123 | 9.799 | 59.231 | 82.623 |
| process | 642735 | 0.002 | 0.001 | 0.002 | 0.004 | 0.04 | 13.447 |
| ctx_commit | 642735 | 1.162 | 0.735 | 2.087 | 8.767 | 17.711 | 106.239 |
| ack | 641558 | 0.046 | 0.002 | 0.005 | 0.36 | 8.975 | 42.687 |
| service | 642735 | 8.142 | 6.487 | 16.527 | 32.143 | 75.711 | 141.823 |
| e2e_large | 331 | 79.376 | 75.263 | 120.895 | 158.079 | 197.759 | 197.759 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12859.331 | 13311.6 |
| applied_total | 468511.163 | 777407 |
| bench_cpu_cores | 4.728 | 5.258 |
| bench_cpu_pct | 19.702 | 21.91 |
| bench_gc_gen0 | 6.878 | 10 |
| bench_gc_gen1 | 3.918 | 7 |
| bench_gc_gen2 | 3.184 | 6 |
| bench_gc_heap_mb | 202.514 | 315.4 |
| bench_mem_mb | 537.031 | 639.1 |
| bench_threadpool_queue | 11.245 | 77 |
| bench_threadpool_threads | 37.367 | 44 |
| broker_backlog | 309.49 | 643 |
| broker_backlog_mb | 1.763 | 9.53 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.765 | 2.13 |
| ctr_eb_kafka_mem_mb | 2374.624 | 2611.2 |
| ctr_eb_mssql_cpu_pct | 592.024 | 720.16 |
| ctr_eb_mssql_mem_mb | 7400.467 | 7402.4 |
| ctr_eb_nats_cpu_pct | 376.946 | 388.21 |
| ctr_eb_nats_mem_mb | 2788.89 | 3163.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.049 | 10.2 |
| ctr_eb_postgres_cpu_pct | 0.472 | 0.64 |
| ctr_eb_postgres_mem_mb | 3147.58 | 3166.2 |
| ctr_eb_rabbitmq_cpu_pct | 13.978 | 14.94 |
| ctr_eb_rabbitmq_mem_mb | 1078.865 | 1086.2 |
| ctr_eb_redis_cpu_pct | 0.273 | 0.29 |
| ctr_eb_redis_mem_mb | 1349.686 | 1350.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 65.626 | 68.15 |
| in_flight | 152.673 | 490 |
| mssql_batch_requests_per_s | 25804.449 | 30172.1 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 4.298 | 11.5 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 48192291.722 | 74152871.9 |
| mssql_log_flush_waits_per_s | 0.292 | 1.9 |
| mssql_log_flushes_per_s | 979.076 | 1385.8 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 12902.661 | 15098 |
| mssql_user_connections | 174.082 | 191 |
| mssql_wait_async_network_io_ms_per_s | 0.943 | 8.8 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 20.682 | 628.6 |
| mssql_wait_pagelatch_ex_ms_per_s | 33.578 | 595.6 |
| mssql_wait_pagelatch_sh_ms_per_s | 41.055 | 1153.9 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 9.308 | 57.8 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.239 | 1.9 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12858.751 | 13331.6 |
| produced_total | 468663.837 | 777523 |
| publish_mb_per_s | 44.342 | 71.34 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,574 records are Completed |
| record_counts | pass | 117,574 records found, 117,574 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,450 events applied by workers, 814,450 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 65.279 | 158.719 | 198.655 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 59.647 | 147.455 | 147.455 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 55.295 | 133.119 | 150.527 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 58.111 | 124.415 | 175.103 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 28.543 | 111.615 | 113.663 |
| t-090 | 7 | 1 | 39.93 | 4.697 | 242 | 0 | 286 | 286 | 0 | 26.751 | 109.055 | 118.271 |
| t-430 | 6 | 1 | 23.835 | 2.584 | 1163 | 0 | 168 | 168 | 0 | 28.671 | 109.055 | 117.247 |
| t-256 | 14 | 1 | 36.08 | 7.98 | 2927 | 0 | 533 | 533 | 0 | 25.855 | 106.495 | 124.927 |
| t-444 | 16 | 1 | 35.438 | 9.589 | 188 | 0 | 605 | 605 | 0 | 25.599 | 106.495 | 126.463 |
| t-272 | 14 | 1 | 100.429 | 24.962 | 394 | 0 | 1495 | 1495 | 0 | 25.215 | 104.447 | 131.071 |
| t-290 | 15 | 1 | 85.667 | 20.654 | 212 | 0 | 1289 | 1289 | 0 | 28.287 | 100.863 | 153.599 |
| t-298 | 6 | 1 | 64.085 | 7.048 | 3700 | 0 | 414 | 414 | 0 | 27.391 | 100.863 | 115.199 |
| t-405 | 6 | 1 | 24.442 | 2.424 | 874 | 0 | 176 | 176 | 0 | 27.903 | 100.863 | 118.783 |
| t-236 | 12 | 1 | 21.423 | 4.143 | 131 | 0 | 263 | 263 | 0 | 28.671 | 99.839 | 107.519 |
| t-195 | 9 | 1 | 74.097 | 11.821 | 2299 | 0 | 698 | 698 | 0 | 27.647 | 98.303 | 143.359 |
| t-318 | 7 | 1 | 31.766 | 3.874 | 686 | 0 | 252 | 252 | 0 | 25.855 | 98.303 | 115.199 |
| t-414 | 21 | 1 | 32.863 | 11.497 | 347 | 0 | 773 | 773 | 0 | 28.031 | 98.303 | 139.263 |
| t-122 | 33 | 1 | 66.424 | 35.794 | 550 | 0 | 2206 | 2206 | 0 | 24.575 | 97.791 | 139.263 |
| t-385 | 20 | 1 | 45.955 | 15.114 | 2007 | 0 | 1006 | 1006 | 0 | 26.111 | 97.791 | 143.359 |
| t-134 | 21 | 1 | 19.598 | 6.714 | 3368 | 0 | 453 | 453 | 0 | 29.695 | 96.767 | 113.151 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 395 | 395 | 0 | 26.111 | 96.255 | 121.855 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 1877 | 1877 | 0 | 25.471 | 95.743 | 144.383 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 380 | 380 | 0 | 27.391 | 95.743 | 109.567 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 27.135 | 95.743 | 140.287 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 25.855 | 95.231 | 101.887 |
