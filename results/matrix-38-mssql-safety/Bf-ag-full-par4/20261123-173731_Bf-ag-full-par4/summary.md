# Bf-ag-full-par4 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.946s, closing 4.794s, drain 25.82s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 675605 | 675605 | 10387.09 | 6959.235 | 16926.9 | 0 | 0 | 0 | 339 | 3449 | 350828 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 474078 | 35551.218 | 38600.703 | 50659.327 | 57081.855 | 61669.375 | 69861.375 |
| e2e_from_send | 474078 | 23818.927 | 25985.023 | 40370.175 | 48955.391 | 54657.023 | 63012.863 |
| producer_lag | 474078 | 11732.311 | 11943.935 | 18563.071 | 21004.287 | 22298.623 | 23183.359 |
| publish_call | 675605 | 167.868 | 142.207 | 285.183 | 661.503 | 1810.431 | 1863.679 |
| queue_wait | 474078 | 23655.334 | 25853.951 | 40239.103 | 48889.855 | 54558.719 | 63012.863 |
| broker_dwell | 474078 | 23495.035 | 25706.495 | 40108.031 | 48726.015 | 54394.879 | 62849.023 |
| receive_call | 635064 | 22.512 | 8.155 | 40.799 | 310.783 | 478.975 | 607.231 |
| ctx_load | 474078 | 32.428 | 8.503 | 30.943 | 744.447 | 1781.759 | 2668.543 |
| process | 474078 | 10.833 | 0.002 | 0.005 | 0.043 | 2615.295 | 3041.279 |
| ctx_commit | 474078 | 120.17 | 78.271 | 203.903 | 395.263 | 3850.239 | 3919.871 |
| ack | 474077 | 0.013 | 0.005 | 0.011 | 0.053 | 1.241 | 49.855 |
| service | 474078 | 163.599 | 92.799 | 248.191 | 1786.879 | 3880.959 | 7139.327 |
| e2e_large | 237 | 14150.643 | 13647.871 | 20873.215 | 24379.391 | 25001.983 | 25001.983 |
| ready_wait | 474078 | 936.872 | 545.791 | 2234.367 | 5611.519 | 8765.439 | 12525.567 |
| blocked | 474078 | 333.944 | 224.767 | 818.175 | 1591.295 | 2387.967 | 3338.239 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6959.235 | 10692.6 |
| applied_total | 194150.082 | 370800 |
| bench_cpu_cores | 3.399 | 4.269 |
| bench_cpu_pct | 14.161 | 17.79 |
| bench_gc_gen0 | 2.184 | 4 |
| bench_gc_gen1 | 1.224 | 3 |
| bench_gc_gen2 | 0.653 | 1 |
| bench_gc_heap_mb | 1016.949 | 1315.8 |
| bench_mem_mb | 1662.963 | 1844.5 |
| bench_threadpool_queue | 115.163 | 1085 |
| bench_threadpool_threads | 41.265 | 44 |
| broker_backlog | 199117.51 | 266412 |
| ctr_eb_ag1_cpu_pct | 968.493 | 1055.66 |
| ctr_eb_ag1_mem_mb | 7088.331 | 7228.9 |
| ctr_eb_ag2_cpu_pct | 225.823 | 252.44 |
| ctr_eb_ag2_mem_mb | 7008.561 | 7086.8 |
| ctr_eb_ag_haproxy_cpu_pct | 65.591 | 74.22 |
| ctr_eb_ag_haproxy_mem_mb | 33.233 | 39.1 |
| ctr_eb_mysql_cpu_pct | 0.788 | 0.84 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.5 | 7.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 8 | 8 |
| ctr_eb_postgres_cpu_pct | 0.505 | 0.73 |
| ctr_eb_postgres_mem_mb | 2271.967 | 2272.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.349 | 85.43 |
| in_flight | 200069.347 | 266972 |
| mssql_batch_requests_per_s | 7606.506 | 9843.4 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 58.971 | 291.9 |
| mssql_lock_waits_per_s | 3.804 | 13.3 |
| mssql_log_bytes_flushed_per_s | 31168683.845 | 41652864.7 |
| mssql_log_flush_waits_per_s | 76.567 | 104.8 |
| mssql_log_flushes_per_s | 516.135 | 688.9 |
| mssql_processes_blocked | 0.735 | 3 |
| mssql_sql_compilations_per_s | 8.82 | 13.9 |
| mssql_transactions_per_s | 256.465 | 346.1 |
| mssql_user_connections | 356.918 | 470 |
| mssql_wait_async_network_io_ms_per_s | 152.72 | 279.9 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 8420.763 | 34179.4 |
| mssql_wait_lck_m_s_ms_per_s | 0.21 | 10.3 |
| mssql_wait_lck_m_u_ms_per_s | 52.441 | 166.5 |
| mssql_wait_lck_m_x_ms_per_s | 0.073 | 3.6 |
| mssql_wait_logbuffer_ms_per_s | 20.076 | 377.6 |
| mssql_wait_pageiolatch_sh_ms_per_s | 45.296 | 197.8 |
| mssql_wait_pagelatch_ex_ms_per_s | 164.216 | 271.1 |
| mssql_wait_pagelatch_sh_ms_per_s | 1181.253 | 16967.5 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4673.451 | 9731 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 812.282 | 1462.9 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.632 | 1.93 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 598436.694 | 1035781 |
| octopus_missing_reports_total | 308.531 | 676 |
| octopus_mqtt_received_total | 210277.408 | 346478 |
| octopus_mqtt_sent_total | 145065.653 | 266840 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10387.09 | 15420.3 |
| produced_total | 394219.429 | 637772 |
| publish_mb_per_s | 35.843 | 64.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 101,447 records are Completed |
| record_counts | pass | 101,447 records found, 101,447 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 339 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 675,605 events applied by workers, 675,605 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 350,828 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1591 ms, max 3338 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5863 seq 8: ready 169 ms before start, 169 ms of it with an idle slot in group 203
- [cross_source_blocking] source 9675 seq 4: ready 103 ms before start, 103 ms of it with an idle slot in group 335
- [cross_source_blocking] source 3655 seq 7: ready 152 ms before start, 152 ms of it with an idle slot in group 124
- [cross_source_blocking] source 9251 seq 13: ready 151 ms before start, 151 ms of it with an idle slot in group 318
- [cross_source_blocking] source 15755 seq 3: ready 135 ms before start, 135 ms of it with an idle slot in group 492
- [cross_source_blocking] source 15603 seq 13: ready 135 ms before start, 135 ms of it with an idle slot in group 484
- [cross_source_blocking] source 4387 seq 4: ready 136 ms before start, 136 ms of it with an idle slot in group 150
- [cross_source_blocking] source 7171 seq 5: ready 136 ms before start, 136 ms of it with an idle slot in group 244
- [cross_source_blocking] source 13603 seq 4: ready 136 ms before start, 136 ms of it with an idle slot in group 430
- [cross_source_blocking] source 4235 seq 5: ready 135 ms before start, 135 ms of it with an idle slot in group 142
- [cross_source_blocking] source 4371 seq 7: ready 135 ms before start, 135 ms of it with an idle slot in group 149
- [cross_source_blocking] source 8383 seq 7: ready 144 ms before start, 144 ms of it with an idle slot in group 284
- [cross_source_blocking] source 9735 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 339
- [cross_source_blocking] source 8003 seq 11: ready 462 ms before start, 166 ms of it with an idle slot in group 270
- [cross_source_blocking] source 14627 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 462
- [cross_source_blocking] source 5555 seq 15: ready 115 ms before start, 115 ms of it with an idle slot in group 190
- [cross_source_blocking] source 6396 seq 9: ready 608 ms before start, 608 ms of it with an idle slot in group 219
- [cross_source_blocking] source 1891 seq 10: ready 713 ms before start, 147 ms of it with an idle slot in group 54
- [cross_source_blocking] source 8647 seq 6: ready 404 ms before start, 102 ms of it with an idle slot in group 300
- [cross_source_blocking] source 2795 seq 8: ready 130 ms before start, 130 ms of it with an idle slot in group 87

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3510 | 3510 | 0 | 46923.775 | 63963.135 | 70254.591 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6287 | 6287 | 0 | 51642.367 | 63176.703 | 67108.863 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1909 | 1909 | 0 | 47972.351 | 62914.559 | 65011.711 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1932 | 1932 | 0 | 45875.199 | 62914.559 | 67633.151 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1532 | 1532 | 0 | 44826.623 | 62652.415 | 66060.287 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4163 | 4163 | 0 | 48496.639 | 62128.127 | 65273.855 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1982 | 1982 | 0 | 47710.207 | 62128.127 | 64749.567 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1967 | 1967 | 0 | 47448.063 | 61865.983 | 65798.143 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 4055 | 4055 | 0 | 49020.927 | 61603.839 | 66322.431 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2145 | 2145 | 0 | 45875.199 | 61341.695 | 63438.847 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 6113 | 6113 | 0 | 50069.503 | 61079.551 | 65273.855 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2052 | 2052 | 0 | 47972.351 | 61079.551 | 63438.847 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7358 | 7358 | 0 | 46137.343 | 61079.551 | 66846.719 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1910 | 1910 | 0 | 46399.487 | 60817.407 | 62128.127 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1786 | 1786 | 0 | 46399.487 | 60817.407 | 66060.287 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1945 | 1945 | 0 | 46137.343 | 60817.407 | 63963.135 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2098 | 2098 | 0 | 48234.495 | 60030.975 | 62914.559 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3822 | 3822 | 0 | 46399.487 | 60030.975 | 65798.143 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 1772 | 1772 | 0 | 44302.335 | 59768.831 | 62128.127 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2080 | 2080 | 0 | 45613.055 | 59768.831 | 66322.431 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 9827 | 9827 | 0 | 50069.503 | 59506.687 | 61341.695 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 1840 | 1840 | 0 | 47448.063 | 59506.687 | 63176.703 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 3827 | 3827 | 0 | 49545.215 | 59506.687 | 60817.407 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 3965 | 3965 | 0 | 48234.495 | 59506.687 | 61865.983 |
| t-047 | 52 | 1 | 42.703 | 38.205 | 343 | 0 | 1583 | 1583 | 0 | 44302.335 | 59244.543 | 61865.983 |
