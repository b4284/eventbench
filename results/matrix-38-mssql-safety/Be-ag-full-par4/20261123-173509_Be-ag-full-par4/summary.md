# Be-ag-full-par4 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.593s, closing 4.137s, drain 21.701s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 664033 | 664033 | 10516.659 | 6785.884 | 18089 | 0 | 0 | 0 | 334 | 3375 | 343315 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 462572 | 34007.3 | 37552.127 | 48300.031 | 54132.735 | 58261.503 | 65044.479 |
| e2e_from_send | 462572 | 20835.594 | 21872.639 | 36962.303 | 45416.447 | 50429.951 | 57180.159 |
| producer_lag | 462572 | 13171.705 | 13238.271 | 19382.271 | 22069.247 | 23314.431 | 23920.639 |
| publish_call | 664033 | 169.293 | 138.879 | 296.703 | 646.655 | 1625.087 | 2545.663 |
| queue_wait | 462572 | 20670.502 | 21757.951 | 36863.999 | 45318.143 | 50331.647 | 57147.391 |
| broker_dwell | 462572 | 20510.821 | 21594.111 | 36700.159 | 45154.303 | 50200.575 | 56983.551 |
| receive_call | 625894 | 22.56 | 8.295 | 41.311 | 313.087 | 476.159 | 566.271 |
| ctx_load | 462572 | 28.133 | 8.559 | 29.359 | 571.391 | 1586.175 | 2373.631 |
| process | 462572 | 10.757 | 0.002 | 0.004 | 0.042 | 2609.151 | 3008.511 |
| ctx_commit | 462572 | 126.031 | 72.319 | 224.127 | 414.207 | 4841.471 | 4947.967 |
| ack | 462572 | 0.012 | 0.005 | 0.011 | 0.053 | 1.147 | 59.327 |
| service | 462572 | 165.075 | 86.847 | 265.215 | 1640.447 | 4857.855 | 7757.823 |
| e2e_large | 232 | 15733.292 | 14934.015 | 22773.759 | 24969.215 | 26623.999 | 26623.999 |
| ready_wait | 462572 | 931.075 | 515.327 | 2119.679 | 6893.567 | 10174.463 | 12763.135 |
| blocked | 462572 | 309.279 | 219.519 | 741.375 | 1398.783 | 2193.407 | 3151.871 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6785.884 | 9823.7 |
| applied_total | 202246.571 | 369307 |
| bench_cpu_cores | 3.254 | 4.122 |
| bench_cpu_pct | 13.558 | 17.18 |
| bench_gc_gen0 | 2.204 | 4 |
| bench_gc_gen1 | 1.265 | 3 |
| bench_gc_gen2 | 0.694 | 2 |
| bench_gc_heap_mb | 1062.19 | 1403.7 |
| bench_mem_mb | 1710.78 | 1801.5 |
| bench_threadpool_queue | 85.694 | 597 |
| bench_threadpool_threads | 43.102 | 44 |
| broker_backlog | 173221.653 | 257374 |
| ctr_eb_ag1_cpu_pct | 949.595 | 1084.25 |
| ctr_eb_ag1_mem_mb | 7241.027 | 7416.7 |
| ctr_eb_ag2_cpu_pct | 215.041 | 248.24 |
| ctr_eb_ag2_mem_mb | 7005.496 | 7037.2 |
| ctr_eb_ag_haproxy_cpu_pct | 62.422 | 75.59 |
| ctr_eb_ag_haproxy_mem_mb | 32.6 | 38.2 |
| ctr_eb_mysql_cpu_pct | 0.771 | 0.85 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.133 | 0.43 |
| ctr_eb_postgres_mem_mb | 2270.68 | 2270.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.88 | 84.81 |
| in_flight | 174717.959 | 257689 |
| mssql_batch_requests_per_s | 7399.612 | 10140.9 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 58.161 | 241 |
| mssql_lock_waits_per_s | 3.569 | 7.6 |
| mssql_log_bytes_flushed_per_s | 30514215.133 | 39443858.3 |
| mssql_log_flush_waits_per_s | 81.31 | 176.2 |
| mssql_log_flushes_per_s | 510.414 | 669.5 |
| mssql_processes_blocked | 0.082 | 1 |
| mssql_sql_compilations_per_s | 9.22 | 15 |
| mssql_transactions_per_s | 255.041 | 331 |
| mssql_user_connections | 366.429 | 468 |
| mssql_wait_async_network_io_ms_per_s | 137.965 | 220.2 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 8613.169 | 40442.4 |
| mssql_wait_lck_m_s_ms_per_s | 0.147 | 3.6 |
| mssql_wait_lck_m_u_ms_per_s | 57.878 | 236.8 |
| mssql_wait_lck_m_x_ms_per_s | 0.384 | 8.9 |
| mssql_wait_logbuffer_ms_per_s | 12 | 89.2 |
| mssql_wait_pageiolatch_sh_ms_per_s | 46.859 | 189.3 |
| mssql_wait_pagelatch_ex_ms_per_s | 163.849 | 331.8 |
| mssql_wait_pagelatch_sh_ms_per_s | 1431.133 | 11321.3 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4501.176 | 7248.7 |
| mssql_wait_threadpool_ms_per_s | 0.135 | 2.2 |
| mssql_wait_writelog_ms_per_s | 889.949 | 1389.5 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.648 | 2.04 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 588254.02 | 1001883 |
| octopus_missing_reports_total | 428.408 | 815 |
| octopus_mqtt_received_total | 204796.531 | 337963 |
| octopus_mqtt_sent_total | 150780.918 | 262898 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10516.659 | 14567.4 |
| produced_total | 376964.531 | 626272 |
| publish_mb_per_s | 37.071 | 65.64 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 100,085 records are Completed |
| record_counts | pass | 100,085 records found, 100,085 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 334 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 664,033 events applied by workers, 664,033 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 343,315 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1399 ms, max 3151 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8508 seq 9: ready 485 ms before start, 214 ms of it with an idle slot in group 292
- [cross_source_blocking] source 7100 seq 5: ready 138 ms before start, 138 ms of it with an idle slot in group 241
- [cross_source_blocking] source 7388 seq 5: ready 138 ms before start, 138 ms of it with an idle slot in group 254
- [cross_source_blocking] source 9021 seq 2: ready 422 ms before start, 250 ms of it with an idle slot in group 312
- [cross_source_blocking] source 5853 seq 4: ready 699 ms before start, 269 ms of it with an idle slot in group 201
- [cross_source_blocking] source 9532 seq 8: ready 412 ms before start, 204 ms of it with an idle slot in group 331
- [cross_source_blocking] source 5052 seq 8: ready 340 ms before start, 186 ms of it with an idle slot in group 176
- [cross_source_blocking] source 12109 seq 7: ready 156 ms before start, 156 ms of it with an idle slot in group 387
- [cross_source_blocking] source 5053 seq 7: ready 168 ms before start, 168 ms of it with an idle slot in group 176
- [cross_source_blocking] source 14865 seq 11: ready 161 ms before start, 160 ms of it with an idle slot in group 466
- [cross_source_blocking] source 9005 seq 23: ready 177 ms before start, 161 ms of it with an idle slot in group 310
- [cross_source_blocking] source 4797 seq 12: ready 191 ms before start, 191 ms of it with an idle slot in group 167
- [cross_source_blocking] source 9732 seq 3: ready 112 ms before start, 112 ms of it with an idle slot in group 338
- [cross_source_blocking] source 4540 seq 12: ready 174 ms before start, 174 ms of it with an idle slot in group 159
- [cross_source_blocking] source 15108 seq 4: ready 111 ms before start, 111 ms of it with an idle slot in group 471
- [cross_source_blocking] source 10717 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 351
- [cross_source_blocking] source 4273 seq 12: ready 194 ms before start, 194 ms of it with an idle slot in group 145
- [cross_source_blocking] source 2972 seq 8: ready 580 ms before start, 311 ms of it with an idle slot in group 97
- [cross_source_blocking] source 5677 seq 15: ready 361 ms before start, 224 ms of it with an idle slot in group 195
- [cross_source_blocking] source 8269 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 281

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1942 | 1942 | 0 | 46399.487 | 60817.407 | 62128.127 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3463 | 3463 | 0 | 44302.335 | 60555.263 | 65273.855 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2040 | 2040 | 0 | 45875.199 | 59506.687 | 62652.415 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4093 | 4093 | 0 | 46399.487 | 59506.687 | 62390.271 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1903 | 1903 | 0 | 43253.759 | 59506.687 | 61865.983 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1780 | 1780 | 0 | 44564.479 | 58458.111 | 61865.983 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3753 | 3753 | 0 | 45613.055 | 58458.111 | 61865.983 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1870 | 1870 | 0 | 44826.623 | 58458.111 | 59768.831 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7231 | 7231 | 0 | 43778.047 | 58458.111 | 62128.127 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6169 | 6169 | 0 | 47185.919 | 57933.823 | 61079.551 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 6005 | 6005 | 0 | 47185.919 | 57671.679 | 60555.263 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2058 | 2058 | 0 | 45350.911 | 57409.535 | 59506.687 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2100 | 2100 | 0 | 41156.607 | 57147.391 | 59768.831 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1498 | 1498 | 0 | 39059.455 | 57147.391 | 58458.111 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1868 | 1868 | 0 | 44302.335 | 57147.391 | 58982.399 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1916 | 1916 | 0 | 42991.615 | 57147.391 | 59506.687 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 1804 | 1804 | 0 | 45350.911 | 56885.247 | 58982.399 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 3277 | 3277 | 0 | 43778.047 | 56885.247 | 58982.399 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 1998 | 1998 | 0 | 46137.343 | 56885.247 | 59506.687 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3960 | 3960 | 0 | 45613.055 | 56623.103 | 61079.551 |
| t-080 | 56 | 1 | 40.902 | 38.54 | 822 | 0 | 1684 | 1684 | 0 | 43253.759 | 56360.959 | 61079.551 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 1809 | 1809 | 0 | 43778.047 | 56098.815 | 58982.399 |
| t-253 | 26 | 1 | 91.3 | 40.306 | 1433 | 0 | 1672 | 1672 | 0 | 44302.335 | 56098.815 | 57409.535 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 7792 | 7792 | 0 | 46399.487 | 56098.815 | 59244.543 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 1731 | 1731 | 0 | 40894.463 | 55836.671 | 57147.391 |
