# 02-sqlq_mssql — PASS ✅

- Broker: `sqlqueue table eb_q, per-source FIFO head dequeue (UPDLOCK+READPAST), vt=30s, delete on ack @ 127.0.0.1,1433/bench (pool max 1000)`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 61.41s, closing 1.989s, drain 81.477s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 812486 | 812486 | 12912.084 | 8918.688 | 15295.4 | 0 | 0 | 0 | 429 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 640762 | 17490.332 | 14688.255 | 35323.903 | 46268.415 | 67764.223 | 78381.055 |
| e2e_from_send | 640762 | 13854.057 | 10526.719 | 30752.767 | 44793.855 | 66813.951 | 78381.055 |
| producer_lag | 640762 | 3636.278 | 3078.143 | 8511.487 | 12550.143 | 14262.271 | 15327.231 |
| publish_call | 15460 | 127.48 | 15.135 | 176.895 | 3115.007 | 6189.055 | 10649.599 |
| queue_wait | 640762 | 13665.68 | 10280.959 | 30605.311 | 44597.247 | 66584.575 | 78053.375 |
| broker_dwell | 640762 | 13554.28 | 10117.119 | 30490.623 | 44531.711 | 66584.575 | 77004.799 |
| receive_call | 114286 | 101.991 | 13.271 | 48.255 | 3518.463 | 5910.527 | 8937.471 |
| ctx_load | 640762 | 11.386 | 8.463 | 18.799 | 42.015 | 123.455 | 6156.287 |
| process | 640762 | 0.003 | 0.001 | 0.003 | 0.006 | 0.081 | 36.191 |
| ctx_commit | 640762 | 11.261 | 8.599 | 19.487 | 43.839 | 126.527 | 6021.119 |
| ack | 640762 | 14.826 | 8.879 | 21.455 | 56.415 | 290.559 | 6373.375 |
| service | 640762 | 188.378 | 133.759 | 350.719 | 648.191 | 5074.943 | 13099.007 |
| e2e_large | 329 | 9201.006 | 9338.879 | 13926.399 | 21708.799 | 24690.687 | 24690.687 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8918.688 | 13749.3 |
| applied_total | 317358.74 | 538691 |
| bench_cpu_cores | 2.769 | 4.023 |
| bench_cpu_pct | 11.539 | 16.76 |
| bench_gc_gen0 | 3.26 | 7 |
| bench_gc_gen1 | 1.62 | 4 |
| bench_gc_gen2 | 0.58 | 2 |
| bench_gc_heap_mb | 866.704 | 1210 |
| bench_mem_mb | 1360.158 | 1603.4 |
| bench_threadpool_queue | 26.14 | 163 |
| bench_threadpool_threads | 37.62 | 41 |
| broker_backlog | 101804.08 | 245056 |
| ctr_eb_kafka_cpu_pct | 1.933 | 2.37 |
| ctr_eb_kafka_mem_mb | 1937.9 | 1937.9 |
| ctr_eb_mssql_cpu_pct | 1324.5 | 1544.17 |
| ctr_eb_mssql_mem_mb | 7187.198 | 7367.1 |
| ctr_eb_nats_cpu_pct | 0.624 | 0.8 |
| ctr_eb_nats_mem_mb | 360.616 | 360.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.3 | 10.3 |
| ctr_eb_postgres_cpu_pct | 0.246 | 0.38 |
| ctr_eb_postgres_mem_mb | 2193.5 | 2193.5 |
| ctr_eb_rabbitmq_cpu_pct | 16.606 | 18.73 |
| ctr_eb_rabbitmq_mem_mb | 629.712 | 637.8 |
| ctr_eb_redis_cpu_pct | 0.318 | 0.36 |
| ctr_eb_redis_mem_mb | 822.7 | 822.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.616 | 85.79 |
| in_flight | 101841.56 | 247004 |
| mssql_batch_requests_per_s | 30170.5 | 44553.6 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 34577.982 | 196339.9 |
| mssql_lock_waits_per_s | 0.496 | 2.3 |
| mssql_log_bytes_flushed_per_s | 153713479.638 | 238392290.6 |
| mssql_log_flush_waits_per_s | 108.866 | 228 |
| mssql_log_flushes_per_s | 2656.106 | 4028.7 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 235.882 | 853.4 |
| mssql_transactions_per_s | 21583.35 | 31497.1 |
| mssql_user_connections | 470.1 | 623 |
| mssql_wait_async_network_io_ms_per_s | 33.332 | 216.7 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 59.822 | 1114.5 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0.156 | 2.4 |
| mssql_wait_logbuffer_ms_per_s | 100.27 | 1019.8 |
| mssql_wait_pageiolatch_sh_ms_per_s | 1666.04 | 9117.7 |
| mssql_wait_pagelatch_ex_ms_per_s | 8704.198 | 45391.6 |
| mssql_wait_pagelatch_sh_ms_per_s | 92550.24 | 861440.4 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 2600.526 | 6878.5 |
| mssql_wait_threadpool_ms_per_s | 534.64 | 8798 |
| mssql_wait_writelog_ms_per_s | 238.46 | 378.6 |
| mssqlq_batch_requests_per_s | 30290.454 | 44117 |
| mssqlq_deadlocks_per_s | 0 | 0 |
| mssqlq_lock_timeouts_per_s | 33007.562 | 200670 |
| mssqlq_lock_waits_per_s | 0.47 | 2.3 |
| mssqlq_log_bytes_flushed_per_s | 152087105.256 | 239576841.3 |
| mssqlq_log_flush_waits_per_s | 110.51 | 228.7 |
| mssqlq_log_flushes_per_s | 2629.968 | 4047.8 |
| mssqlq_processes_blocked | 0 | 0 |
| mssqlq_sql_compilations_per_s | 243.952 | 849.8 |
| mssqlq_transactions_per_s | 21659.01 | 31179.5 |
| mssqlq_user_connections | 470.42 | 623 |
| mssqlq_wait_async_network_io_ms_per_s | 33.8 | 223.9 |
| mssqlq_wait_cxpacket_ms_per_s | 0 | 0 |
| mssqlq_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_s_ms_per_s | 60.112 | 1127.4 |
| mssqlq_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_x_ms_per_s | 0.156 | 2.4 |
| mssqlq_wait_logbuffer_ms_per_s | 99.494 | 1006.5 |
| mssqlq_wait_pageiolatch_sh_ms_per_s | 1688.194 | 9113.5 |
| mssqlq_wait_pagelatch_ex_ms_per_s | 8295.448 | 45776 |
| mssqlq_wait_pagelatch_sh_ms_per_s | 81848.376 | 886515.6 |
| mssqlq_wait_sos_scheduler_yield_ms_per_s | 2604.05 | 7028.6 |
| mssqlq_wait_threadpool_ms_per_s | 537.584 | 8847.4 |
| mssqlq_wait_writelog_ms_per_s | 241.908 | 381.9 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12912.084 | 39678.3 |
| produced_total | 419200.3 | 777036 |
| publish_mb_per_s | 45.157 | 122.49 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,358 records are Completed |
| record_counts | pass | 117,358 records found, 117,358 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 429 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 812,486 events applied by workers, 812,486 generated |
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
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6260 | 6260 | 0 | 49807.359 | 77070.335 | 78643.199 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 16076 | 16076 | 0 | 37224.447 | 51642.367 | 52690.943 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7658 | 7658 | 0 | 38797.311 | 51380.223 | 52953.087 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2993 | 2993 | 0 | 38797.311 | 49545.215 | 51380.223 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3051 | 3051 | 0 | 36438.015 | 48758.783 | 50855.935 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8367 | 8367 | 0 | 37486.591 | 47972.351 | 50855.935 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12217 | 12217 | 0 | 36962.303 | 47448.063 | 50069.503 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2827 | 2827 | 0 | 40108.031 | 46661.631 | 50331.647 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3018 | 3018 | 0 | 38010.879 | 45350.911 | 47710.207 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 10444 | 10444 | 0 | 32767.999 | 44564.479 | 46661.631 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8393 | 8393 | 0 | 35389.439 | 43778.047 | 46661.631 |
| t-438 | 121 | 4 | 74.684 | 156.372 | 1839 | 0 | 9666 | 9666 | 0 | 34603.007 | 43253.759 | 46399.487 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9162 | 9162 | 0 | 25296.895 | 42729.471 | 44302.335 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 7775 | 7775 | 0 | 36175.871 | 42729.471 | 45613.055 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8582 | 8582 | 0 | 34078.719 | 42467.327 | 45875.199 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5686 | 5686 | 0 | 21495.807 | 41680.895 | 43778.047 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2888 | 2888 | 0 | 35651.583 | 41680.895 | 44040.191 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5902 | 5902 | 0 | 32112.639 | 41156.607 | 43778.047 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 8005 | 8005 | 0 | 33292.287 | 40894.463 | 42205.183 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4960 | 4960 | 0 | 31588.351 | 40632.319 | 42205.183 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6319 | 6319 | 0 | 32243.711 | 40370.175 | 43778.047 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5482 | 5482 | 0 | 33423.359 | 40108.031 | 40894.463 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8769 | 8769 | 0 | 31588.351 | 39845.887 | 43253.759 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12095 | 12095 | 0 | 24772.607 | 39845.887 | 42729.471 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2893 | 2893 | 0 | 32112.639 | 39583.743 | 40894.463 |
