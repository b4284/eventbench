# Sc-mssql-batch — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.344s, closing 2.683s, drain 3.371s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 785784 | 785784 | 12662.845 | 13891.051 | 19669.2 | 0 | 0 | 0 | 376 | 4041 | 337237 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 583577 | 12829.764 | 12230.655 | 19546.111 | 25853.951 | 30638.079 | 38076.415 |
| e2e_from_send | 583577 | 4497.296 | 1296.383 | 13180.927 | 20545.535 | 25264.127 | 34504.703 |
| producer_lag | 583577 | 8332.47 | 8339.455 | 12730.367 | 16023.551 | 19693.567 | 20856.831 |
| publish_call | 785784 | 139.847 | 115.455 | 255.231 | 569.855 | 2025.471 | 3041.279 |
| queue_wait | 583577 | 4418.035 | 1164.287 | 13099.007 | 20463.615 | 25214.975 | 34471.935 |
| broker_dwell | 583577 | 4288.423 | 1030.655 | 12976.127 | 20348.927 | 25083.903 | 34340.863 |
| receive_call | 708442 | 32.794 | 11.247 | 87.743 | 333.311 | 480.767 | 574.463 |
| ctx_load | 583577 | 32.351 | 9.199 | 37.055 | 549.375 | 1164.287 | 1598.463 |
| process | 583577 | 10.815 | 0.002 | 0.004 | 0.029 | 2613.247 | 3026.943 |
| ctx_commit | 583577 | 35.919 | 29.439 | 63.167 | 118.271 | 417.535 | 1435.647 |
| ack | 583577 | 0.011 | 0.005 | 0.009 | 0.037 | 1.007 | 63.743 |
| service | 583577 | 79.263 | 42.559 | 102.527 | 884.223 | 2693.119 | 4126.719 |
| e2e_large | 274 | 10524.911 | 10076.159 | 15187.967 | 17612.799 | 22265.855 | 22265.855 |
| ready_wait | 583577 | 342.758 | 189.439 | 880.639 | 2079.743 | 3260.415 | 5492.735 |
| blocked | 583577 | 220.721 | 143.359 | 564.223 | 1053.695 | 1774.591 | 2277.375 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13891.051 | 19669.2 |
| applied_total | 380382.102 | 736263 |
| bench_cpu_cores | 4.472 | 5.459 |
| bench_cpu_pct | 18.634 | 22.75 |
| bench_gc_gen0 | 2.633 | 4 |
| bench_gc_gen1 | 1.122 | 2 |
| bench_gc_gen2 | 0.633 | 1 |
| bench_gc_heap_mb | 1312.922 | 1760.7 |
| bench_mem_mb | 2011.127 | 2374.7 |
| bench_threadpool_queue | 118.469 | 1044 |
| bench_threadpool_threads | 43.224 | 44 |
| broker_backlog | 70747.837 | 110198 |
| ctr_eb_mssql_cpu_pct | 1190.059 | 1322.15 |
| ctr_eb_mssql_mem_mb | 7195.253 | 7413.9 |
| ctr_eb_mysql_cpu_pct | 0.76 | 0.85 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.162 | 0.41 |
| ctr_eb_postgres_mem_mb | 2215.318 | 2215.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.717 | 84.97 |
| in_flight | 65781.735 | 106025 |
| mssql_batch_requests_per_s | 14839.065 | 19562.5 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 45.863 | 61.3 |
| mssql_lock_waits_per_s | 1.469 | 4.1 |
| mssql_log_bytes_flushed_per_s | 46236131.114 | 57204617.1 |
| mssql_log_flush_waits_per_s | 587.21 | 755.1 |
| mssql_log_flushes_per_s | 857.227 | 1065.8 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 22.318 | 35.4 |
| mssql_transactions_per_s | 591.147 | 760.1 |
| mssql_user_connections | 395.367 | 464 |
| mssql_wait_async_network_io_ms_per_s | 109.396 | 184.1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.224 | 5.5 |
| mssql_wait_lck_m_u_ms_per_s | 18.059 | 78.7 |
| mssql_wait_lck_m_x_ms_per_s | 0.073 | 1.8 |
| mssql_wait_logbuffer_ms_per_s | 3.296 | 18.1 |
| mssql_wait_pageiolatch_sh_ms_per_s | 11.627 | 129.7 |
| mssql_wait_pagelatch_ex_ms_per_s | 323.904 | 759.4 |
| mssql_wait_pagelatch_sh_ms_per_s | 807.48 | 3454.5 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 3256.573 | 6101.4 |
| mssql_wait_threadpool_ms_per_s | 0.971 | 11.9 |
| mssql_wait_writelog_ms_per_s | 3400.012 | 4164.2 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.859 | 2.28 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 826425.122 | 1477392 |
| octopus_missing_reports_total | 805.735 | 2233 |
| octopus_mqtt_received_total | 198853.694 | 309250 |
| octopus_mqtt_sent_total | 262396.653 | 493320 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 12662.845 | 16713.5 |
| produced_total | 446163.837 | 746112 |
| publish_mb_per_s | 42.814 | 69.02 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 114,145 records are Completed |
| record_counts | pass | 114,145 records found, 114,145 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 376 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 785,784 events applied by workers, 785,784 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 337,237 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1054 ms, max 2277 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10324 seq 5: ready 159 ms before start, 159 ms of it with an idle slot in group 346
- [cross_source_blocking] source 2783 seq 6: ready 194 ms before start, 194 ms of it with an idle slot in group 85
- [cross_source_blocking] source 12724 seq 12: ready 142 ms before start, 142 ms of it with an idle slot in group 401
- [cross_source_blocking] source 3036 seq 14: ready 142 ms before start, 142 ms of it with an idle slot in group 102
- [cross_source_blocking] source 4020 seq 8: ready 141 ms before start, 141 ms of it with an idle slot in group 134
- [cross_source_blocking] source 6868 seq 4: ready 142 ms before start, 124 ms of it with an idle slot in group 233
- [cross_source_blocking] source 4793 seq 8: ready 215 ms before start, 215 ms of it with an idle slot in group 167
- [cross_source_blocking] source 7297 seq 10: ready 217 ms before start, 217 ms of it with an idle slot in group 250
- [cross_source_blocking] source 15093 seq 2: ready 214 ms before start, 187 ms of it with an idle slot in group 471
- [cross_source_blocking] source 857 seq 6: ready 215 ms before start, 215 ms of it with an idle slot in group 24
- [cross_source_blocking] source 2361 seq 3: ready 217 ms before start, 137 ms of it with an idle slot in group 69
- [cross_source_blocking] source 5759 seq 8: ready 148 ms before start, 148 ms of it with an idle slot in group 199
- [cross_source_blocking] source 6815 seq 16: ready 152 ms before start, 152 ms of it with an idle slot in group 232
- [cross_source_blocking] source 9305 seq 13: ready 216 ms before start, 216 ms of it with an idle slot in group 321
- [cross_source_blocking] source 4377 seq 4: ready 215 ms before start, 215 ms of it with an idle slot in group 149
- [cross_source_blocking] source 2773 seq 7: ready 215 ms before start, 215 ms of it with an idle slot in group 85
- [cross_source_blocking] source 12447 seq 4: ready 657 ms before start, 175 ms of it with an idle slot in group 396
- [cross_source_blocking] source 13217 seq 7: ready 131 ms before start, 131 ms of it with an idle slot in group 422
- [cross_source_blocking] source 7297 seq 11: ready 110 ms before start, 110 ms of it with an idle slot in group 250
- [cross_source_blocking] source 148 seq 10: ready 188 ms before start, 153 ms of it with an idle slot in group 2

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 7271 | 7271 | 0 | 21102.591 | 33161.215 | 36438.015 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4815 | 4815 | 0 | 19660.799 | 32899.071 | 36438.015 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2546 | 2546 | 0 | 18087.935 | 32505.855 | 35127.295 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2433 | 2433 | 0 | 19005.439 | 32112.639 | 38273.023 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 4438 | 4438 | 0 | 17825.791 | 30932.991 | 35913.727 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2363 | 2363 | 0 | 18743.295 | 30277.631 | 32636.927 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 7136 | 7136 | 0 | 19136.511 | 30146.559 | 33816.575 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2199 | 2199 | 0 | 16908.287 | 30015.487 | 31326.207 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 8535 | 8535 | 0 | 16252.927 | 30015.487 | 35389.439 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2253 | 2253 | 0 | 16449.535 | 29229.055 | 30932.991 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 12987 | 12987 | 0 | 20054.015 | 28704.767 | 30539.775 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 4703 | 4703 | 0 | 16580.607 | 28704.767 | 31981.567 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 9300 | 9300 | 0 | 18219.007 | 28311.551 | 32243.711 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 4032 | 4032 | 0 | 13631.487 | 28180.479 | 34078.719 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1765 | 1765 | 0 | 12845.055 | 27918.335 | 30277.631 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 2100 | 2100 | 0 | 14680.063 | 27918.335 | 32112.639 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2277 | 2277 | 0 | 14614.527 | 27787.263 | 29884.415 |
| t-306 | 134 | 5 | 93.183 | 208.376 | 2978 | 0 | 10239 | 10239 | 0 | 16908.287 | 27656.191 | 30539.775 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2453 | 2453 | 0 | 16515.071 | 27525.119 | 29622.271 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 4594 | 4594 | 0 | 16383.999 | 27394.047 | 29360.127 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2123 | 2123 | 0 | 17301.503 | 27262.975 | 30408.703 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2126 | 2126 | 0 | 15400.959 | 27131.903 | 30408.703 |
| t-108 | 32 | 1 | 72.27 | 39.424 | 694 | 0 | 1923 | 1923 | 0 | 15400.959 | 27131.903 | 28311.551 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2266 | 2266 | 0 | 13369.343 | 27131.903 | 30015.487 |
| t-253 | 26 | 1 | 91.3 | 40.306 | 1433 | 0 | 1958 | 1958 | 0 | 15663.103 | 27000.831 | 28311.551 |
