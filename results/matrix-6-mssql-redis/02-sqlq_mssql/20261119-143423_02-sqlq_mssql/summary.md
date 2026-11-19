# 02-sqlq_mssql — PASS ✅

- Broker: `sqlqueue table eb_q, per-source FIFO head dequeue (UPDLOCK+READPAST), vt=30s, delete on ack @ 127.0.0.1,1433/bench (pool max 1000)`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.51s, closing 1.459s, drain 37.796s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 602227 | 602227 | 9122.278 | 8209.516 | 14147.8 | 0 | 0 | 0 | 331 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 432312 | 17632.08 | 15081.471 | 32096.255 | 50855.935 | 56983.551 | 63766.527 |
| e2e_from_send | 432312 | 8406.69 | 5365.759 | 20414.463 | 42205.183 | 46759.935 | 50724.863 |
| producer_lag | 432312 | 9225.368 | 8003.583 | 18399.231 | 24297.471 | 25247.743 | 25624.575 |
| publish_call | 4462 | 438.805 | 7.103 | 1417.215 | 4952.063 | 9060.351 | 10190.847 |
| queue_wait | 432312 | 8252.722 | 5206.015 | 20250.623 | 42074.111 | 46563.327 | 50561.023 |
| broker_dwell | 432312 | 8090.797 | 5009.407 | 20054.015 | 41910.271 | 46071.807 | 49643.519 |
| receive_call | 102750 | 127.403 | 13.759 | 89.663 | 3762.175 | 5361.663 | 45645.823 |
| ctx_load | 432312 | 10.152 | 7.951 | 17.567 | 38.943 | 76.991 | 4935.679 |
| process | 432312 | 0.003 | 0.002 | 0.003 | 0.006 | 0.107 | 39.231 |
| ctx_commit | 432312 | 10.091 | 7.971 | 17.679 | 39.007 | 84.415 | 5140.479 |
| ack | 432312 | 12.278 | 8.455 | 20.943 | 57.087 | 146.815 | 5312.511 |
| service | 432312 | 153.974 | 117.247 | 296.447 | 483.839 | 3942.399 | 5791.743 |
| e2e_large | 231 | 13388.235 | 12787.711 | 20578.303 | 26591.231 | 28065.791 | 28065.791 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8209.516 | 11831.4 |
| applied_total | 282371.041 | 485844 |
| bench_cpu_cores | 2.44 | 3.577 |
| bench_cpu_pct | 10.165 | 14.91 |
| bench_gc_gen0 | 2.796 | 5 |
| bench_gc_gen1 | 0.837 | 3 |
| bench_gc_gen2 | 0.469 | 2 |
| bench_gc_heap_mb | 915.431 | 1327.5 |
| bench_mem_mb | 1436.29 | 1638.5 |
| bench_threadpool_queue | 20.041 | 181 |
| bench_threadpool_threads | 42.755 | 44 |
| broker_backlog | 54435.041 | 91590 |
| ctr_eb_kafka_cpu_pct | 1.899 | 2.34 |
| ctr_eb_kafka_mem_mb | 3316.9 | 3316.9 |
| ctr_eb_mssql_cpu_pct | 1402.975 | 1781 |
| ctr_eb_mssql_mem_mb | 5958.443 | 5999.6 |
| ctr_eb_nats_cpu_pct | 1.37 | 10.87 |
| ctr_eb_nats_mem_mb | 700.333 | 1975.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 0.317 | 0.67 |
| ctr_eb_postgres_mem_mb | 3114.7 | 3114.7 |
| ctr_eb_rabbitmq_cpu_pct | 15.069 | 17.11 |
| ctr_eb_rabbitmq_mem_mb | 720.486 | 723.2 |
| ctr_eb_redis_cpu_pct | 0.319 | 0.46 |
| ctr_eb_redis_mem_mb | 1326.6 | 1326.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 73.526 | 90.02 |
| in_flight | 48666.939 | 88121 |
| mssql_batch_requests_per_s | 29737.598 | 40910.7 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 91669.804 | 230902.4 |
| mssql_lock_waits_per_s | 40.355 | 126.9 |
| mssql_log_bytes_flushed_per_s | 125336740.759 | 221088969.7 |
| mssql_log_flush_waits_per_s | 80.061 | 239.2 |
| mssql_log_flushes_per_s | 2175.68 | 3725.9 |
| mssql_processes_blocked | 23.286 | 43 |
| mssql_sql_compilations_per_s | 67.445 | 418.9 |
| mssql_transactions_per_s | 21617.318 | 28699.6 |
| mssql_user_connections | 448.735 | 609 |
| mssql_wait_async_network_io_ms_per_s | 26.818 | 133.9 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 24890.067 | 79332.2 |
| mssql_wait_lck_m_u_ms_per_s | 0.398 | 6.5 |
| mssql_wait_lck_m_x_ms_per_s | 0.02 | 0.5 |
| mssql_wait_logbuffer_ms_per_s | 133.339 | 1515.9 |
| mssql_wait_pageiolatch_sh_ms_per_s | 1629.327 | 4304.6 |
| mssql_wait_pagelatch_ex_ms_per_s | 7749.808 | 60651.3 |
| mssql_wait_pagelatch_sh_ms_per_s | 33368.967 | 254552.4 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4444.086 | 12358.6 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 192.484 | 302.7 |
| mssqlq_batch_requests_per_s | 29798.01 | 40995.7 |
| mssqlq_deadlocks_per_s | 0 | 0 |
| mssqlq_lock_timeouts_per_s | 91645.629 | 228823.4 |
| mssqlq_lock_waits_per_s | 39.949 | 122.4 |
| mssqlq_log_bytes_flushed_per_s | 124681037.48 | 220244514.4 |
| mssqlq_log_flush_waits_per_s | 80.445 | 243.2 |
| mssqlq_log_flushes_per_s | 2165.141 | 3710.7 |
| mssqlq_processes_blocked | 23.122 | 43 |
| mssqlq_sql_compilations_per_s | 68.5 | 468.8 |
| mssqlq_transactions_per_s | 21655.484 | 28814.3 |
| mssqlq_user_connections | 448.735 | 609 |
| mssqlq_wait_async_network_io_ms_per_s | 24.488 | 131.7 |
| mssqlq_wait_cxpacket_ms_per_s | 0 | 0 |
| mssqlq_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_s_ms_per_s | 24873.576 | 79796.5 |
| mssqlq_wait_lck_m_u_ms_per_s | 0.392 | 6.4 |
| mssqlq_wait_lck_m_x_ms_per_s | 0.02 | 0.5 |
| mssqlq_wait_logbuffer_ms_per_s | 133.514 | 1513 |
| mssqlq_wait_pageiolatch_sh_ms_per_s | 1627.294 | 4279.3 |
| mssqlq_wait_pagelatch_ex_ms_per_s | 7788.476 | 60534.1 |
| mssqlq_wait_pagelatch_sh_ms_per_s | 33714.627 | 257459.6 |
| mssqlq_wait_sos_scheduler_yield_ms_per_s | 4453.098 | 12405.9 |
| mssqlq_wait_threadpool_ms_per_s | 0 | 0 |
| mssqlq_wait_writelog_ms_per_s | 193.859 | 301.4 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 9122.278 | 20888.5 |
| produced_total | 331037.98 | 552269 |
| publish_mb_per_s | 33.091 | 103.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 92,678 records are Completed |
| record_counts | pass | 92,678 records found, 92,678 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 331 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 602,227 events applied by workers, 602,227 generated |
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
| t-373 | 31 | 1 | 57.544 | 30.618 | 2878 | 0 | 1418 | 1418 | 0 | 51904.511 | 63438.847 | 63963.135 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1255 | 1255 | 0 | 50593.791 | 60293.119 | 61079.551 |
| t-274 | 29 | 1 | 56.437 | 27.244 | 1181 | 0 | 1264 | 1264 | 0 | 49545.215 | 58195.967 | 60293.119 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1337 | 1337 | 0 | 48496.639 | 57933.823 | 58720.255 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 8977 | 8977 | 0 | 33816.575 | 57933.823 | 59768.831 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 1380 | 1380 | 0 | 47972.351 | 56885.247 | 58720.255 |
| t-212 | 29 | 1 | 50.595 | 24.477 | 296 | 0 | 1166 | 1166 | 0 | 47185.919 | 56623.103 | 57409.535 |
| t-413 | 19 | 1 | 69.524 | 22.975 | 3585 | 0 | 1034 | 1034 | 0 | 48234.495 | 56360.959 | 57409.535 |
| t-406 | 36 | 1 | 41.208 | 23.174 | 183 | 0 | 1180 | 1180 | 0 | 46923.775 | 55836.671 | 56885.247 |
| t-172 | 20 | 1 | 73.562 | 25.851 | 2589 | 0 | 1135 | 1135 | 0 | 47185.919 | 55574.527 | 56623.103 |
| t-100 | 33 | 1 | 40.775 | 21.722 | 2626 | 0 | 1017 | 1017 | 0 | 45350.911 | 55312.383 | 56098.815 |
| t-083 | 58 | 1 | 23.381 | 23.11 | 417 | 0 | 1101 | 1101 | 0 | 44564.479 | 54788.095 | 55574.527 |
| t-359 | 52 | 1 | 25.301 | 21.73 | 132 | 0 | 995 | 995 | 0 | 45350.911 | 54001.663 | 55050.239 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 8865 | 8865 | 0 | 27787.263 | 53739.519 | 56360.959 |
| t-003 | 30 | 1 | 36.473 | 18.126 | 1633 | 0 | 895 | 895 | 0 | 42991.615 | 53215.231 | 53739.519 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3954 | 3954 | 0 | 32505.855 | 53215.231 | 54525.951 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 6186 | 6186 | 0 | 34603.007 | 52166.655 | 54001.663 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4697 | 4697 | 0 | 30277.631 | 51904.511 | 54788.095 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6699 | 6699 | 0 | 30277.631 | 51118.079 | 53739.519 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 12340 | 12340 | 0 | 29491.199 | 50593.791 | 52428.799 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 11938 | 11938 | 0 | 25952.255 | 47972.351 | 50331.647 |
| t-077 | 10 | 1 | 143.606 | 23.968 | 1566 | 0 | 1067 | 1067 | 0 | 38273.023 | 47185.919 | 47710.207 |
| t-301 | 26 | 1 | 44.546 | 19.992 | 3339 | 0 | 911 | 911 | 0 | 35389.439 | 45613.055 | 46399.487 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 6368 | 6368 | 0 | 26607.615 | 44826.623 | 47710.207 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2241 | 2241 | 0 | 25427.967 | 42991.615 | 44826.623 |
