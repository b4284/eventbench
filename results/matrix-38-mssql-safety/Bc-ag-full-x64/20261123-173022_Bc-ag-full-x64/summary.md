# Bc-ag-full-x64 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 64/tx, 64 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.556s, closing 6.033s, drain 19.73s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 658159 | 658159 | 10190.447 | 7180.353 | 16103.4 | 0 | 0 | 0 | 329 | 3348 | 344886 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 456801 | 33329.798 | 35454.975 | 47349.759 | 53477.375 | 57376.767 | 62685.183 |
| e2e_from_send | 456801 | 20676.875 | 21676.031 | 36241.407 | 45514.751 | 50954.239 | 58032.127 |
| producer_lag | 456801 | 12652.934 | 12083.199 | 20447.231 | 23658.495 | 24182.783 | 24854.527 |
| publish_call | 658159 | 173.942 | 144.767 | 308.223 | 616.447 | 1611.775 | 2170.879 |
| queue_wait | 456801 | 20529.006 | 21561.343 | 36143.103 | 45416.447 | 50888.703 | 57999.359 |
| broker_dwell | 456801 | 20363.617 | 21413.887 | 35979.263 | 45252.607 | 50724.863 | 57933.823 |
| receive_call | 601436 | 24.629 | 9.287 | 50.303 | 319.999 | 479.743 | 565.759 |
| ctx_load | 456801 | 52.917 | 13.319 | 62.399 | 1115.135 | 1774.591 | 2830.335 |
| process | 456801 | 10.774 | 0.002 | 0.004 | 0.044 | 2613.247 | 3018.751 |
| ctx_commit | 456801 | 84.004 | 54.047 | 125.055 | 293.119 | 3450.879 | 3608.575 |
| ack | 456801 | 0.016 | 0.005 | 0.011 | 0.052 | 1.95 | 70.719 |
| service | 456801 | 147.878 | 75.647 | 190.847 | 1848.319 | 3481.599 | 6180.863 |
| e2e_large | 227 | 14689.032 | 13459.455 | 22724.607 | 26230.783 | 26722.303 | 26722.303 |
| ready_wait | 456801 | 864.37 | 525.311 | 1990.655 | 5419.007 | 7630.847 | 10805.247 |
| blocked | 456801 | 343.315 | 241.535 | 799.231 | 1597.439 | 2367.487 | 3164.159 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7180.353 | 12765.9 |
| applied_total | 197673.612 | 381858 |
| bench_cpu_cores | 3.313 | 4.387 |
| bench_cpu_pct | 13.806 | 18.28 |
| bench_gc_gen0 | 2.184 | 3 |
| bench_gc_gen1 | 1.224 | 3 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 1059.304 | 1344.3 |
| bench_mem_mb | 1787.257 | 1907.4 |
| bench_threadpool_queue | 170.551 | 1659 |
| bench_threadpool_threads | 43.122 | 44 |
| broker_backlog | 184127.959 | 221563 |
| ctr_eb_ag1_cpu_pct | 1014.44 | 1074.78 |
| ctr_eb_ag1_mem_mb | 7211.122 | 7457.7 |
| ctr_eb_ag2_cpu_pct | 218.4 | 232.51 |
| ctr_eb_ag2_mem_mb | 7004.767 | 7025.7 |
| ctr_eb_ag_haproxy_cpu_pct | 71.421 | 86.44 |
| ctr_eb_ag_haproxy_mem_mb | 32.916 | 36.2 |
| ctr_eb_mysql_cpu_pct | 0.784 | 0.85 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.4 | 7.4 |
| ctr_eb_postgres_cpu_pct | 0.114 | 0.34 |
| ctr_eb_postgres_mem_mb | 2270.108 | 2270.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.72 | 84.54 |
| in_flight | 184037.184 | 239391 |
| mssql_batch_requests_per_s | 8805.371 | 13390.4 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 50.153 | 168.8 |
| mssql_lock_waits_per_s | 3.973 | 7.2 |
| mssql_log_bytes_flushed_per_s | 31524075.455 | 40838881.7 |
| mssql_log_flush_waits_per_s | 175.982 | 383.2 |
| mssql_log_flushes_per_s | 564.743 | 841.8 |
| mssql_processes_blocked | 0.102 | 1 |
| mssql_sql_compilations_per_s | 7.7 | 10.6 |
| mssql_transactions_per_s | 1067.892 | 1569.1 |
| mssql_user_connections | 375.02 | 468 |
| mssql_wait_async_network_io_ms_per_s | 126.461 | 229.2 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 19290.071 | 27621.6 |
| mssql_wait_lck_m_s_ms_per_s | 1.002 | 22 |
| mssql_wait_lck_m_u_ms_per_s | 65.994 | 177.3 |
| mssql_wait_lck_m_x_ms_per_s | 0.192 | 2.2 |
| mssql_wait_logbuffer_ms_per_s | 4.339 | 42.6 |
| mssql_wait_pageiolatch_sh_ms_per_s | 15.604 | 78.6 |
| mssql_wait_pagelatch_ex_ms_per_s | 270 | 936.7 |
| mssql_wait_pagelatch_sh_ms_per_s | 8113.612 | 165229.2 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 5270.784 | 9088.3 |
| mssql_wait_threadpool_ms_per_s | 0.233 | 3 |
| mssql_wait_writelog_ms_per_s | 1305.541 | 2528.3 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.605 | 2.04 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 587852.306 | 1010410 |
| octopus_missing_reports_total | 370 | 841 |
| octopus_mqtt_received_total | 212070.245 | 335914 |
| octopus_mqtt_sent_total | 148979.041 | 272719 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10190.447 | 14467.4 |
| produced_total | 381710.796 | 621249 |
| publish_mb_per_s | 35.536 | 56.28 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 99,427 records are Completed |
| record_counts | pass | 99,427 records found, 99,427 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 329 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 658,159 events applied by workers, 658,159 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 344,886 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1597 ms, max 3163 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6396 seq 9: ready 140 ms before start, 140 ms of it with an idle slot in group 219
- [cross_source_blocking] source 2795 seq 7: ready 113 ms before start, 113 ms of it with an idle slot in group 87
- [cross_source_blocking] source 1163 seq 7: ready 141 ms before start, 141 ms of it with an idle slot in group 39
- [cross_source_blocking] source 13215 seq 6: ready 142 ms before start, 142 ms of it with an idle slot in group 422
- [cross_source_blocking] source 13183 seq 5: ready 140 ms before start, 140 ms of it with an idle slot in group 419
- [cross_source_blocking] source 7171 seq 4: ready 131 ms before start, 131 ms of it with an idle slot in group 244
- [cross_source_blocking] source 860 seq 4: ready 597 ms before start, 178 ms of it with an idle slot in group 24
- [cross_source_blocking] source 5567 seq 4: ready 244 ms before start, 244 ms of it with an idle slot in group 192
- [cross_source_blocking] source 15747 seq 2: ready 243 ms before start, 243 ms of it with an idle slot in group 491
- [cross_source_blocking] source 13980 seq 9: ready 198 ms before start, 198 ms of it with an idle slot in group 444
- [cross_source_blocking] source 4540 seq 12: ready 218 ms before start, 218 ms of it with an idle slot in group 159
- [cross_source_blocking] source 1155 seq 8: ready 167 ms before start, 167 ms of it with an idle slot in group 39
- [cross_source_blocking] source 12811 seq 4: ready 168 ms before start, 168 ms of it with an idle slot in group 404
- [cross_source_blocking] source 4395 seq 7: ready 166 ms before start, 166 ms of it with an idle slot in group 150
- [cross_source_blocking] source 6403 seq 11: ready 168 ms before start, 168 ms of it with an idle slot in group 219
- [cross_source_blocking] source 15743 seq 9: ready 126 ms before start, 118 ms of it with an idle slot in group 491
- [cross_source_blocking] source 5550 seq 23: ready 242 ms before start, 242 ms of it with an idle slot in group 190
- [cross_source_blocking] source 8255 seq 6: ready 402 ms before start, 141 ms of it with an idle slot in group 279
- [cross_source_blocking] source 4380 seq 13: ready 132 ms before start, 102 ms of it with an idle slot in group 149
- [cross_source_blocking] source 5 seq 9: ready 177 ms before start, 177 ms of it with an idle slot in group 0

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4072 | 4072 | 0 | 46137.343 | 59768.831 | 62914.559 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7170 | 7170 | 0 | 43515.903 | 58720.255 | 62128.127 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2020 | 2020 | 0 | 43515.903 | 58458.111 | 61603.839 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1487 | 1487 | 0 | 38797.311 | 57933.823 | 59506.687 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1854 | 1854 | 0 | 44826.623 | 57933.823 | 59768.831 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1921 | 1921 | 0 | 42467.327 | 57933.823 | 58982.399 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 5937 | 5937 | 0 | 46399.487 | 57409.535 | 60555.263 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1921 | 1921 | 0 | 42205.183 | 57409.535 | 58720.255 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1861 | 1861 | 0 | 42729.471 | 57409.535 | 58982.399 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6114 | 6114 | 0 | 45613.055 | 57147.391 | 60293.119 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1901 | 1901 | 0 | 42729.471 | 57147.391 | 60030.975 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3928 | 3928 | 0 | 45350.911 | 57147.391 | 60817.407 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1895 | 1895 | 0 | 38535.167 | 57147.391 | 59768.831 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 1790 | 1790 | 0 | 45088.767 | 56885.247 | 58982.399 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2034 | 2034 | 0 | 45088.767 | 56885.247 | 58982.399 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2085 | 2085 | 0 | 40894.463 | 56623.103 | 60293.119 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 1991 | 1991 | 0 | 45613.055 | 56623.103 | 59506.687 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3721 | 3721 | 0 | 42205.183 | 56360.959 | 59768.831 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 7284 | 7284 | 0 | 45350.911 | 56098.815 | 58458.111 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1756 | 1756 | 0 | 40108.031 | 55836.671 | 60030.975 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3442 | 3442 | 0 | 37486.591 | 55836.671 | 61603.839 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 1777 | 1777 | 0 | 41943.039 | 55574.527 | 58458.111 |
| t-080 | 56 | 1 | 40.902 | 38.54 | 822 | 0 | 1668 | 1668 | 0 | 40894.463 | 55574.527 | 59244.543 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 3628 | 3628 | 0 | 44302.335 | 55574.527 | 57147.391 |
| t-306 | 134 | 5 | 93.183 | 208.376 | 2978 | 0 | 8582 | 8582 | 0 | 44302.335 | 55312.383 | 57933.823 |
