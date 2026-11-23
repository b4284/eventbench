# Bb-ag-full — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.95s, closing 4.596s, drain 33.054s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 655893 | 655893 | 9819.406 | 4592.6 | 16687.1 | 0 | 24316 | 24316 | 328 | 3325 | 333002 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 454364 | 45271.483 | 49577.983 | 59179.007 | 65437.695 | 74252.287 | 85721.087 |
| e2e_from_send | 454364 | 32478.201 | 35225.599 | 49709.055 | 57999.359 | 67764.223 | 80936.959 |
| producer_lag | 454574 | 12791.026 | 14671.871 | 20103.167 | 22167.551 | 23035.903 | 24018.943 |
| publish_call | 655893 | 173.807 | 143.999 | 277.503 | 642.047 | 3506.175 | 3555.327 |
| queue_wait | 454364 | 32306.392 | 35094.527 | 49610.751 | 57901.055 | 67698.687 | 80936.959 |
| broker_dwell | 454364 | 32146.85 | 34930.687 | 49446.911 | 57737.215 | 67502.079 | 80871.423 |
| receive_call | 630538 | 17.72 | 7.291 | 28.495 | 264.447 | 465.663 | 553.471 |
| ctx_load | 454364 | 81.203 | 7.527 | 52.575 | 1690.623 | 3878.911 | 4202.495 |
| process | 454364 | 10.713 | 0.002 | 0.004 | 0.038 | 2609.151 | 3006.463 |
| ctx_commit | 454364 | 79.741 | 52.415 | 146.559 | 554.495 | 1593.343 | 4073.471 |
| ack | 454363 | 0.011 | 0.005 | 0.01 | 0.049 | 0.98 | 61.343 |
| service | 454364 | 171.783 | 65.663 | 286.719 | 2359.295 | 4022.271 | 8699.903 |
| e2e_large | 225 | 15730.847 | 17924.095 | 23773.183 | 27230.207 | 28409.855 | 28409.855 |
| ready_wait | 454364 | 948.072 | 506.879 | 2043.903 | 7663.615 | 16023.551 | 63700.991 |
| blocked | 454364 | 309.431 | 197.119 | 556.031 | 2525.183 | 7835.647 | 22937.599 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4592.6 | 9583.2 |
| applied_total | 146864.367 | 249539 |
| bench_cpu_cores | 2.739 | 3.79 |
| bench_cpu_pct | 11.411 | 15.79 |
| bench_gc_gen0 | 2.02 | 4 |
| bench_gc_gen1 | 1.082 | 3 |
| bench_gc_gen2 | 0.653 | 1 |
| bench_gc_heap_mb | 1018.555 | 1346.9 |
| bench_mem_mb | 1682.843 | 1838.9 |
| bench_threadpool_queue | 81.041 | 1555 |
| bench_threadpool_threads | 43.592 | 44 |
| broker_backlog | 228562.98 | 366607 |
| ctr_eb_ag1_cpu_pct | 781.148 | 943 |
| ctr_eb_ag1_mem_mb | 6487.684 | 6971.4 |
| ctr_eb_ag2_cpu_pct | 203.655 | 283.87 |
| ctr_eb_ag2_mem_mb | 5527.261 | 6220.8 |
| ctr_eb_ag_haproxy_cpu_pct | 59.982 | 161.05 |
| ctr_eb_ag_haproxy_mem_mb | 23.72 | 25.8 |
| ctr_eb_mysql_cpu_pct | 0.744 | 0.83 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.07 |
| ctr_eb_pgbouncer_2_mem_mb | 7.831 | 8.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.724 | 8.3 |
| ctr_eb_postgres_cpu_pct | 0.746 | 1.17 |
| ctr_eb_postgres_mem_mb | 2327.878 | 2328.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 495.32 | 7244.5 |
| host_cpu_pct | 66.658 | 81.13 |
| in_flight | 234157.02 | 368394 |
| mssql_batch_requests_per_s | 5885.51 | 8655.6 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 43.061 | 82.9 |
| mssql_lock_waits_per_s | 6.412 | 17.6 |
| mssql_log_bytes_flushed_per_s | 26972552.259 | 39989797.1 |
| mssql_log_flush_waits_per_s | 72.957 | 94.7 |
| mssql_log_flushes_per_s | 448.698 | 665.3 |
| mssql_processes_blocked | 0.633 | 5 |
| mssql_sql_compilations_per_s | 11.024 | 21.9 |
| mssql_transactions_per_s | 215.433 | 299.2 |
| mssql_user_connections | 284.02 | 390 |
| mssql_wait_async_network_io_ms_per_s | 128.467 | 222.8 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 8962.527 | 13500 |
| mssql_wait_lck_m_s_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_u_ms_per_s | 175.112 | 398.5 |
| mssql_wait_lck_m_x_ms_per_s | 0.159 | 3.7 |
| mssql_wait_logbuffer_ms_per_s | 17.406 | 163.7 |
| mssql_wait_pageiolatch_sh_ms_per_s | 68.255 | 1308.2 |
| mssql_wait_pagelatch_ex_ms_per_s | 163.088 | 432.4 |
| mssql_wait_pagelatch_sh_ms_per_s | 959.371 | 2509.2 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 2808.32 | 5906.4 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 1254.476 | 2457.5 |
| nacks_per_s | 495.32 | 7244.5 |
| octopus_dispatcher_cores | 1.56 | 2.3 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 580441.776 | 971072 |
| octopus_missing_reports_total | 224.714 | 384 |
| octopus_mqtt_received_total | 212365.694 | 356529 |
| octopus_mqtt_sent_total | 116072.02 | 197225 |
| octopus_publish_retries_total | 5692.245 | 11010 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 9819.406 | 16240.3 |
| produced_total | 381021.388 | 617933 |
| publish_mb_per_s | 33.669 | 57.93 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 99,131 records are Completed |
| record_counts | pass | 99,131 records found, 99,131 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 328 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 655,893 events applied by workers, 655,893 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 333,002 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2525 ms, max 22936 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3327 seq 11: ready 266 ms before start, 266 ms of it with an idle slot in group 113
- [cross_source_blocking] source 8501 seq 11: ready 254 ms before start, 254 ms of it with an idle slot in group 291
- [cross_source_blocking] source 14403 seq 4: ready 1982 ms before start, 167 ms of it with an idle slot in group 459
- [cross_source_blocking] source 7388 seq 5: ready 296 ms before start, 296 ms of it with an idle slot in group 254
- [cross_source_blocking] source 15755 seq 3: ready 107 ms before start, 107 ms of it with an idle slot in group 492
- [cross_source_blocking] source 12963 seq 6: ready 278 ms before start, 278 ms of it with an idle slot in group 410
- [cross_source_blocking] source 15290 seq 6: ready 138 ms before start, 138 ms of it with an idle slot in group 478
- [cross_source_blocking] source 12430 seq 9: ready 137 ms before start, 137 ms of it with an idle slot in group 395
- [cross_source_blocking] source 7394 seq 7: ready 137 ms before start, 137 ms of it with an idle slot in group 255
- [cross_source_blocking] source 9346 seq 6: ready 137 ms before start, 137 ms of it with an idle slot in group 324
- [cross_source_blocking] source 13666 seq 7: ready 137 ms before start, 137 ms of it with an idle slot in group 435
- [cross_source_blocking] source 13335 seq 7: ready 403 ms before start, 132 ms of it with an idle slot in group 426
- [cross_source_blocking] source 4387 seq 4: ready 874 ms before start, 102 ms of it with an idle slot in group 150
- [cross_source_blocking] source 4276 seq 14: ready 336 ms before start, 336 ms of it with an idle slot in group 145
- [cross_source_blocking] source 10316 seq 4: ready 338 ms before start, 163 ms of it with an idle slot in group 345
- [cross_source_blocking] source 4793 seq 8: ready 103 ms before start, 103 ms of it with an idle slot in group 167
- [cross_source_blocking] source 11348 seq 6: ready 336 ms before start, 336 ms of it with an idle slot in group 370
- [cross_source_blocking] source 9347 seq 7: ready 192 ms before start, 192 ms of it with an idle slot in group 324
- [cross_source_blocking] source 3033 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 101
- [cross_source_blocking] source 9253 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 318

## Error samples

- t-054#0 src=1884 seq=15 op=Create rec=8091718385670 ev=0: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-432#0 src=13617 seq=32 op=Update rec=58484569669633 ev=19: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-285#0 src=8417 seq=9 op=Complete rec=36150739730434 ev=1: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-189#0 src=5503 seq=10 op=Complete rec=23635205029890 ev=4: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-121#0 src=3597 seq=13 op=Update rec=15448997363713 ev=12: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-016#0 src=569 seq=6 op=Get rec=2443836391425 ev=3: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-391#0 src=12302 seq=8 op=Get rec=52836687675393 ev=3: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-270#0 src=8014 seq=16 op=Update rec=34419867910145 ev=7: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-286#0 src=8443 seq=8 op=Update rec=36262408880129 ev=2: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-055#1 src=1916 seq=15 op=Get rec=8229157339138 ev=3: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-193#0 src=5579 seq=10 op=Get rec=23961622544385 ev=6: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-065#1 src=2279 seq=23 op=Update rec=9788230467585 ev=12: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-215#0 src=6317 seq=10 op=Get rec=27131308408834 ev=6: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-461#0 src=14573 seq=9 op=Get rec=62590558404610 ev=1: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-374#2 src=11504 seq=33 op=Update rec=49409303773191 ev=5: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-131#0 src=3857 seq=9 op=Update rec=16565688860675 ev=2: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-344#5 src=10113 seq=12 op=Update rec=43435004264449 ev=9: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-147#2 src=4295 seq=22 op=Update rec=18446884536324 ev=4: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-044#0 src=1406 seq=4 op=Create rec=6038724018178 ev=0: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)
- t-233#0 src=6841 seq=8 op=Get rec=29381871271937 ev=5: SqlException: A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 2 - Connection was terminated)

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2075 | 2075 | 0 | 55312.383 | 82837.503 | 85983.231 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4051 | 4051 | 0 | 56885.247 | 81264.639 | 83361.791 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1886 | 1886 | 0 | 54001.663 | 79167.487 | 80740.351 |
| t-001 | 118 | 2 | 36.72 | 71.345 | 154 | 0 | 3134 | 3134 | 0 | 49545.215 | 74973.183 | 77594.623 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1903 | 1903 | 0 | 54525.951 | 74973.183 | 81264.639 |
| t-061 | 148 | 2 | 27.838 | 68.109 | 1274 | 0 | 2980 | 2980 | 0 | 47710.207 | 72876.031 | 74448.895 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2018 | 2018 | 0 | 57933.823 | 71827.455 | 74973.183 |
| t-023 | 72 | 1 | 25.932 | 31.224 | 647 | 0 | 1369 | 1369 | 0 | 46399.487 | 71303.167 | 72876.031 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1903 | 1903 | 0 | 57409.535 | 71303.167 | 73400.319 |
| t-044 | 113 | 1 | 18.942 | 34.8 | 243 | 0 | 1607 | 1607 | 0 | 46923.775 | 70778.879 | 72351.743 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3421 | 3421 | 0 | 54525.951 | 70778.879 | 76546.047 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1849 | 1849 | 0 | 56623.103 | 70778.879 | 72876.031 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1496 | 1496 | 0 | 52953.087 | 70254.591 | 72876.031 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3705 | 3705 | 0 | 57409.535 | 70254.591 | 73924.607 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6081 | 6081 | 0 | 58458.111 | 69730.303 | 73400.319 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1737 | 1737 | 0 | 53739.519 | 69206.015 | 79167.487 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 1978 | 1978 | 0 | 57409.535 | 69206.015 | 71827.455 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7144 | 7144 | 0 | 55050.239 | 69206.015 | 74448.895 |
| t-050 | 96 | 1 | 13.54 | 21.538 | 130 | 0 | 1012 | 1012 | 0 | 43778.047 | 68681.727 | 68681.727 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 5917 | 5917 | 0 | 58195.967 | 68681.727 | 72351.743 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 1789 | 1789 | 0 | 57409.535 | 68681.727 | 70778.879 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1906 | 1906 | 0 | 54001.663 | 68681.727 | 70778.879 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2020 | 2020 | 0 | 57409.535 | 68681.727 | 71827.455 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3911 | 3911 | 0 | 57147.391 | 68681.727 | 72351.743 |
| t-088 | 46 | 1 | 53.823 | 40.61 | 203 | 0 | 1684 | 1684 | 0 | 54788.095 | 68157.439 | 72876.031 |
