# 02-sqlq_mssql — PASS ✅

- Broker: `sqlqueue table eb_q, per-source FIFO head dequeue (UPDLOCK+READPAST), vt=30s, delete on ack @ 127.0.0.1,1433/bench (pool max 1000)`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.325s, closing 2.614s, drain 121.433s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 805879 | 805879 | 12869.712 | 7630.037 | 14096.1 | 0 | 0 | 0 | 426 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 634191 | 29369.128 | 23789.567 | 63373.311 | 93519.871 | 117702.655 | 130678.783 |
| e2e_from_send | 634191 | 27002.271 | 21266.431 | 61800.447 | 92733.439 | 117309.439 | 123076.607 |
| producer_lag | 634191 | 2366.865 | 1704.959 | 5746.687 | 10174.463 | 12042.239 | 12877.823 |
| publish_call | 10674 | 184.912 | 8.983 | 422.911 | 3407.871 | 6774.783 | 7729.151 |
| queue_wait | 634191 | 26764.83 | 21004.287 | 61603.839 | 92536.831 | 117047.295 | 123011.071 |
| broker_dwell | 634191 | 26616.52 | 20824.063 | 61571.071 | 92471.295 | 117047.295 | 122879.999 |
| receive_call | 116312 | 121.043 | 21.391 | 136.703 | 2973.695 | 7458.815 | 7962.623 |
| ctx_load | 634191 | 16.842 | 11.439 | 23.919 | 51.071 | 516.095 | 7589.887 |
| process | 634191 | 0.003 | 0.002 | 0.003 | 0.006 | 0.1 | 66.431 |
| ctx_commit | 634191 | 14.536 | 11.543 | 24.367 | 52.095 | 118.847 | 7979.007 |
| ack | 634191 | 14.513 | 11.911 | 26.095 | 57.567 | 109.759 | 6021.119 |
| service | 634191 | 237.433 | 190.207 | 417.023 | 677.375 | 4333.567 | 8716.287 |
| e2e_large | 326 | 9168.984 | 9887.743 | 14147.583 | 19005.439 | 21020.671 | 21020.671 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7630.037 | 13331.7 |
| applied_total | 275916.837 | 461036 |
| bench_cpu_cores | 2.525 | 3.791 |
| bench_cpu_pct | 10.52 | 15.8 |
| bench_gc_gen0 | 3.082 | 5 |
| bench_gc_gen1 | 1.327 | 4 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 871.298 | 1164.8 |
| bench_mem_mb | 1323.702 | 1586.7 |
| bench_threadpool_queue | 36.469 | 301 |
| bench_threadpool_threads | 37.714 | 41 |
| broker_backlog | 151019.571 | 308886 |
| ctr_eb_kafka_cpu_pct | 1.857 | 2.49 |
| ctr_eb_kafka_mem_mb | 2638.661 | 2671.2 |
| ctr_eb_mssql_cpu_pct | 1293.529 | 1824.23 |
| ctr_eb_mssql_mem_mb | 7366.367 | 7603.6 |
| ctr_eb_nats_cpu_pct | 1.361 | 8.67 |
| ctr_eb_nats_mem_mb | 1625.612 | 2049.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 9.9 | 9.9 |
| ctr_eb_postgres_cpu_pct | 0.346 | 0.53 |
| ctr_eb_postgres_mem_mb | 3196.222 | 3198.6 |
| ctr_eb_rabbitmq_cpu_pct | 15.405 | 17.95 |
| ctr_eb_rabbitmq_mem_mb | 884.237 | 889.9 |
| ctr_eb_redis_cpu_pct | 0.314 | 0.36 |
| ctr_eb_redis_mem_mb | 1323.7 | 1323.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 70.888 | 92.48 |
| in_flight | 154007.531 | 304764 |
| mssql_batch_requests_per_s | 26264.349 | 37760 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 107189.692 | 303462.2 |
| mssql_lock_waits_per_s | 0.569 | 1.8 |
| mssql_log_bytes_flushed_per_s | 146956763.171 | 217611770.1 |
| mssql_log_flush_waits_per_s | 216.696 | 1107.9 |
| mssql_log_flushes_per_s | 2637.206 | 3643.2 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 180.441 | 1067 |
| mssql_transactions_per_s | 18906.812 | 26131 |
| mssql_user_connections | 493.265 | 632 |
| mssql_wait_async_network_io_ms_per_s | 41.586 | 246.4 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 30.716 | 213.6 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0.147 | 3.6 |
| mssql_wait_logbuffer_ms_per_s | 176.151 | 2041.5 |
| mssql_wait_pageiolatch_sh_ms_per_s | 2293.782 | 11227.6 |
| mssql_wait_pagelatch_ex_ms_per_s | 4993.753 | 15720 |
| mssql_wait_pagelatch_sh_ms_per_s | 20976.647 | 59150.9 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 7231.147 | 28368.5 |
| mssql_wait_threadpool_ms_per_s | 0.414 | 9 |
| mssql_wait_writelog_ms_per_s | 258.253 | 501.9 |
| mssqlq_batch_requests_per_s | 26218.48 | 37565.2 |
| mssqlq_deadlocks_per_s | 0 | 0 |
| mssqlq_lock_timeouts_per_s | 108376.522 | 301185.3 |
| mssqlq_lock_waits_per_s | 0.567 | 1.8 |
| mssqlq_log_bytes_flushed_per_s | 146615645.629 | 226538338.5 |
| mssqlq_log_flush_waits_per_s | 216.659 | 1090.9 |
| mssqlq_log_flushes_per_s | 2631.855 | 3790.9 |
| mssqlq_processes_blocked | 0.02 | 1 |
| mssqlq_sql_compilations_per_s | 179.882 | 1071.6 |
| mssqlq_transactions_per_s | 18876.457 | 25949.9 |
| mssqlq_user_connections | 492.694 | 632 |
| mssqlq_wait_async_network_io_ms_per_s | 40.861 | 247.3 |
| mssqlq_wait_cxpacket_ms_per_s | 0 | 0 |
| mssqlq_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_s_ms_per_s | 27.929 | 207 |
| mssqlq_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_x_ms_per_s | 0.227 | 3.7 |
| mssqlq_wait_logbuffer_ms_per_s | 166.161 | 2010.5 |
| mssqlq_wait_pageiolatch_sh_ms_per_s | 2322.306 | 10995.9 |
| mssqlq_wait_pagelatch_ex_ms_per_s | 4938.22 | 15764.2 |
| mssqlq_wait_pagelatch_sh_ms_per_s | 21048.561 | 56106 |
| mssqlq_wait_sos_scheduler_yield_ms_per_s | 7215.473 | 28065 |
| mssqlq_wait_threadpool_ms_per_s | 0.61 | 9.2 |
| mssqlq_wait_writelog_ms_per_s | 257.433 | 507.6 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12869.712 | 34269.2 |
| produced_total | 429924.367 | 765800 |
| publish_mb_per_s | 44.92 | 124.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 116,605 records are Completed |
| record_counts | pass | 116,605 records found, 116,605 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 426 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 805,879 events applied by workers, 805,879 generated |
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
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12000 | 12000 | 0 | 75497.471 | 122159.103 | 131071.999 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9064 | 9064 | 0 | 61079.551 | 103284.735 | 111673.343 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 15933 | 15933 | 0 | 59768.831 | 100139.007 | 102236.159 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16579 | 16579 | 0 | 60030.975 | 95944.703 | 99090.431 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12127 | 12127 | 0 | 57409.535 | 93323.263 | 101711.871 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13624 | 13624 | 0 | 56623.103 | 89653.247 | 97517.567 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6225 | 6225 | 0 | 52690.943 | 85983.231 | 94371.839 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5855 | 5855 | 0 | 59244.543 | 82837.503 | 90701.823 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8693 | 8693 | 0 | 56885.247 | 82313.215 | 90701.823 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 10373 | 10373 | 0 | 45613.055 | 82313.215 | 91226.111 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6008 | 6008 | 0 | 52428.799 | 80740.351 | 89653.247 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7579 | 7579 | 0 | 52690.943 | 80216.063 | 88604.671 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3012 | 3012 | 0 | 52690.943 | 76546.047 | 84934.655 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5653 | 5653 | 0 | 48234.495 | 76546.047 | 84934.655 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6258 | 6258 | 0 | 51642.367 | 76021.759 | 83886.079 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8515 | 8515 | 0 | 46137.343 | 76021.759 | 83886.079 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 7951 | 7951 | 0 | 49545.215 | 75497.471 | 83886.079 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 7714 | 7714 | 0 | 48758.783 | 74973.183 | 82837.503 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5264 | 5264 | 0 | 49283.071 | 72876.031 | 80740.351 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4931 | 4931 | 0 | 52428.799 | 72876.031 | 74973.183 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2989 | 2989 | 0 | 48234.495 | 71827.455 | 74448.895 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8328 | 8328 | 0 | 46661.631 | 71827.455 | 80216.063 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2865 | 2865 | 0 | 50069.503 | 71303.167 | 78643.199 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8291 | 8291 | 0 | 41680.895 | 71303.167 | 79167.487 |
| t-438 | 121 | 4 | 74.684 | 156.372 | 1839 | 0 | 9574 | 9574 | 0 | 42991.615 | 70778.879 | 78643.199 |
