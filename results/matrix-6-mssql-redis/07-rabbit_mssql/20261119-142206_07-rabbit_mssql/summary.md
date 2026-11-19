# 07-rabbit_mssql — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.114s, closing 1.336s, drain 5.027s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814520 | 814520 | 12863.176 | 12986.543 | 19584.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642795 | 63.506 | 17.103 | 213.887 | 500.735 | 842.751 | 1518.591 |
| e2e_from_send | 642795 | 47.21 | 12.479 | 146.431 | 435.711 | 833.535 | 1513.471 |
| producer_lag | 642795 | 16.296 | 4.447 | 45.855 | 176.511 | 241.535 | 324.607 |
| publish_call | 187249 | 10.164 | 6.419 | 13.807 | 91.135 | 369.663 | 856.063 |
| queue_wait | 642795 | 36.591 | 8.151 | 116.607 | 367.871 | 790.527 | 1466.367 |
| broker_dwell | 642795 | 36.454 | 8.139 | 116.223 | 366.847 | 790.527 | 1466.367 |
| receive_call | 565600 | 35.812 | 20.959 | 95.103 | 181.759 | 199.807 | 247.423 |
| ctx_load | 642795 | 4.128 | 1.461 | 4.643 | 82.687 | 279.807 | 867.839 |
| process | 642795 | 0.002 | 0.001 | 0.002 | 0.005 | 0.193 | 16.071 |
| ctx_commit | 642795 | 2.211 | 1.494 | 4.431 | 8.855 | 59.231 | 830.463 |
| ack | 636876 | 0.002 | 0.002 | 0.003 | 0.005 | 0.127 | 29.439 |
| service | 642795 | 10.62 | 3.601 | 17.711 | 145.279 | 360.447 | 876.543 |
| e2e_large | 331 | 108.489 | 64.511 | 239.999 | 685.055 | 871.935 | 871.935 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12986.543 | 17174.8 |
| applied_total | 468246.959 | 777490 |
| bench_cpu_cores | 2.614 | 2.874 |
| bench_cpu_pct | 10.89 | 11.98 |
| bench_gc_gen0 | 4.959 | 8 |
| bench_gc_gen1 | 2.878 | 6 |
| bench_gc_gen2 | 0.367 | 2 |
| bench_gc_heap_mb | 482.696 | 667.1 |
| bench_mem_mb | 827.89 | 937.4 |
| bench_threadpool_queue | 21.102 | 162 |
| bench_threadpool_threads | 35.041 | 42 |
| broker_backlog | 1672.224 | 22706 |
| broker_consumers | 598 | 598 |
| broker_ready | 278.531 | 10233 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 1393.694 | 12473 |
| ctr_eb_kafka_cpu_pct | 1.807 | 2.29 |
| ctr_eb_kafka_mem_mb | 2442.112 | 2460.5 |
| ctr_eb_mssql_cpu_pct | 585.698 | 664.62 |
| ctr_eb_mssql_mem_mb | 6798.898 | 6810.2 |
| ctr_eb_nats_cpu_pct | 0.377 | 3.17 |
| ctr_eb_nats_mem_mb | 346.904 | 347.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 9.9 | 9.9 |
| ctr_eb_postgres_cpu_pct | 0.237 | 0.41 |
| ctr_eb_postgres_mem_mb | 3213.976 | 3216.7 |
| ctr_eb_rabbitmq_cpu_pct | 528.991 | 584.63 |
| ctr_eb_rabbitmq_mem_mb | 1351.998 | 1429.6 |
| ctr_eb_redis_cpu_pct | 0.283 | 0.3 |
| ctr_eb_redis_mem_mb | 1353.3 | 1353.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 61.163 | 68.22 |
| in_flight | 186.959 | 3420 |
| mssql_batch_requests_per_s | 26171.261 | 31558.1 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 5.463 | 13.6 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 49148544.845 | 78681168.5 |
| mssql_log_flush_waits_per_s | 0.276 | 1.9 |
| mssql_log_flushes_per_s | 968.549 | 1450.8 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 13085.824 | 15780.9 |
| mssql_user_connections | 199.633 | 256 |
| mssql_wait_async_network_io_ms_per_s | 0.916 | 3.7 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.02 | 0.5 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 8.304 | 33.4 |
| mssql_wait_pagelatch_ex_ms_per_s | 14.894 | 33.7 |
| mssql_wait_pagelatch_sh_ms_per_s | 24.92 | 59.2 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 25.72 | 62.7 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.182 | 1.4 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12863.176 | 16614.3 |
| produced_total | 468433.918 | 777585 |
| publish_mb_per_s | 44.352 | 69.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,583 records are Completed |
| record_counts | pass | 117,583 records found, 117,583 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,520 events applied by workers, 814,520 generated |
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
| t-393 | 30 | 1 | 27.649 | 13.621 | 134 | 0 | 889 | 889 | 0 | 16.127 | 1163.263 | 1261.567 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2738 | 2738 | 0 | 17.151 | 983.039 | 1277.951 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2613 | 2613 | 0 | 17.535 | 946.175 | 1163.263 |
| t-046 | 24 | 1 | 84.724 | 34.162 | 3235 | 0 | 2199 | 2199 | 0 | 15.743 | 929.791 | 1409.023 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 52.735 | 868.351 | 872.447 |
| t-019 | 38 | 2 | 88.646 | 55.998 | 208 | 0 | 3500 | 3500 | 0 | 17.151 | 806.911 | 1523.711 |
| t-494 | 35 | 1 | 33.067 | 18.747 | 2036 | 0 | 1179 | 1179 | 0 | 17.151 | 794.623 | 929.791 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 395 | 395 | 0 | 16.895 | 774.143 | 1056.767 |
| t-127 | 19 | 1 | 78.823 | 23.73 | 2026 | 0 | 1515 | 1515 | 0 | 17.023 | 753.663 | 958.463 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16762 | 16762 | 0 | 17.407 | 741.375 | 1499.135 |
| t-431 | 6 | 1 | 37.575 | 3.743 | 596 | 0 | 226 | 226 | 0 | 16.639 | 741.375 | 815.103 |
| t-111 | 18 | 1 | 79.153 | 23.718 | 3401 | 0 | 1504 | 1504 | 0 | 16.319 | 737.279 | 999.423 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 17.279 | 729.087 | 929.791 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 17.663 | 720.895 | 872.447 |
| t-387 | 13 | 1 | 26.447 | 5.945 | 138 | 0 | 400 | 400 | 0 | 17.151 | 704.511 | 839.679 |
| t-290 | 15 | 1 | 85.667 | 20.654 | 212 | 0 | 1289 | 1289 | 0 | 18.047 | 688.127 | 1032.191 |
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 17.023 | 671.743 | 856.063 |
| t-279 | 18 | 1 | 35.495 | 10.479 | 196 | 0 | 667 | 667 | 0 | 16.255 | 667.647 | 802.815 |
| t-472 | 32 | 1 | 25.243 | 12.984 | 1124 | 0 | 840 | 840 | 0 | 17.663 | 663.551 | 884.735 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1901 | 1901 | 0 | 17.151 | 659.455 | 1146.879 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 17.407 | 659.455 | 860.159 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 18.047 | 655.359 | 1105.919 |
| t-459 | 25 | 1 | 16.76 | 6.839 | 2174 | 0 | 467 | 467 | 0 | 16.319 | 651.263 | 692.223 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 60.159 | 647.167 | 647.167 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 17.663 | 647.167 | 1015.807 |
