# Ba-ag-full — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.405s, closing 4.557s, drain 19.869s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 613397 | 613397 | 8941.99 | 6533.537 | 15516.6 | 0 | 0 | 0 | 313 | 3130 | 313279 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 412443 | 36811.274 | 39944.191 | 50266.111 | 55836.671 | 59572.223 | 64389.119 |
| e2e_from_send | 412443 | 22187.965 | 23494.655 | 38010.879 | 46858.239 | 52297.727 | 59146.239 |
| producer_lag | 412443 | 14623.288 | 14761.983 | 22446.079 | 24625.151 | 25772.031 | 26591.231 |
| publish_call | 613397 | 184.782 | 160.255 | 331.263 | 650.751 | 858.111 | 1469.439 |
| queue_wait | 412443 | 22036.009 | 23379.967 | 37879.807 | 46759.935 | 52232.191 | 59080.703 |
| broker_dwell | 412443 | 21849.107 | 23183.359 | 37683.199 | 46596.095 | 52002.815 | 58916.863 |
| receive_call | 573871 | 24.524 | 8.959 | 43.583 | 340.223 | 486.143 | 569.855 |
| ctx_load | 412443 | 29.197 | 9.767 | 34.207 | 583.167 | 1391.615 | 2000.895 |
| process | 412443 | 10.887 | 0.002 | 0.004 | 0.044 | 2613.247 | 3004.415 |
| ctx_commit | 412443 | 111.736 | 80.511 | 220.799 | 474.367 | 837.119 | 1805.311 |
| ack | 412443 | 0.013 | 0.005 | 0.01 | 0.054 | 1.463 | 58.815 |
| service | 412443 | 151.987 | 98.239 | 268.799 | 988.159 | 2775.039 | 4542.463 |
| e2e_large | 211 | 16546.209 | 16269.311 | 24920.063 | 27262.975 | 29114.367 | 29114.367 |
| ready_wait | 412443 | 890.279 | 547.327 | 2109.439 | 5009.407 | 7372.799 | 11124.735 |
| blocked | 412443 | 354.905 | 251.775 | 854.015 | 1626.111 | 2441.215 | 2998.271 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6533.537 | 9406 |
| applied_total | 172360.143 | 346348 |
| bench_cpu_cores | 3.132 | 4.007 |
| bench_cpu_pct | 13.048 | 16.69 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 1.49 | 3 |
| bench_gc_gen2 | 0.551 | 1 |
| bench_gc_heap_mb | 1071.096 | 1519.8 |
| bench_mem_mb | 1718.896 | 1969.9 |
| bench_threadpool_queue | 102.959 | 903 |
| bench_threadpool_threads | 43.857 | 44 |
| broker_backlog | 183421.837 | 226238 |
| ctr_eb_ag1_cpu_pct | 873.209 | 965.26 |
| ctr_eb_ag1_mem_mb | 4872.865 | 5683.9 |
| ctr_eb_ag2_cpu_pct | 229.712 | 273.17 |
| ctr_eb_ag2_mem_mb | 3966.512 | 4974.4 |
| ctr_eb_ag_haproxy_cpu_pct | 62.831 | 75.75 |
| ctr_eb_ag_haproxy_mem_mb | 24.582 | 34.1 |
| ctr_eb_mysql_cpu_pct | 0.789 | 0.91 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.711 | 1.18 |
| ctr_eb_postgres_mem_mb | 2326.667 | 2327.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 73.397 | 82.78 |
| in_flight | 184252.163 | 229672 |
| mssql_batch_requests_per_s | 7086.271 | 9313.8 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 43.978 | 67.1 |
| mssql_lock_waits_per_s | 5.89 | 16.8 |
| mssql_log_bytes_flushed_per_s | 27873560.037 | 33840917.2 |
| mssql_log_flush_waits_per_s | 76.696 | 94.7 |
| mssql_log_flushes_per_s | 467.598 | 559.3 |
| mssql_processes_blocked | 0.204 | 2 |
| mssql_sql_compilations_per_s | 6.243 | 9.4 |
| mssql_transactions_per_s | 229.6 | 288.5 |
| mssql_user_connections | 360.531 | 468 |
| mssql_wait_async_network_io_ms_per_s | 139.2 | 229.4 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 8843.333 | 14530.4 |
| mssql_wait_lck_m_s_ms_per_s | 1.384 | 33.9 |
| mssql_wait_lck_m_u_ms_per_s | 115.31 | 295.9 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 14.684 | 121.5 |
| mssql_wait_pageiolatch_sh_ms_per_s | 37.004 | 200.8 |
| mssql_wait_pagelatch_ex_ms_per_s | 190.914 | 505.6 |
| mssql_wait_pagelatch_sh_ms_per_s | 742.688 | 2636.8 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 4043.306 | 5996.9 |
| mssql_wait_threadpool_ms_per_s | 28 | 116.9 |
| mssql_wait_writelog_ms_per_s | 1066.092 | 2632.1 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.512 | 1.91 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 538889.694 | 932603 |
| octopus_missing_reports_total | 293.02 | 684 |
| octopus_mqtt_received_total | 207732.735 | 330844 |
| octopus_mqtt_sent_total | 127679.469 | 243587 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 8941.99 | 11267.4 |
| produced_total | 356612.306 | 576020 |
| publish_mb_per_s | 31.376 | 50.11 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 94,095 records are Completed |
| record_counts | pass | 94,095 records found, 94,095 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 313 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 613,397 events applied by workers, 613,397 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 313,279 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1626 ms, max 2997 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9253 seq 8: ready 210 ms before start, 210 ms of it with an idle slot in group 318
- [cross_source_blocking] source 8501 seq 11: ready 116 ms before start, 116 ms of it with an idle slot in group 291
- [cross_source_blocking] source 405 seq 4: ready 210 ms before start, 210 ms of it with an idle slot in group 13
- [cross_source_blocking] source 401 seq 12: ready 231 ms before start, 231 ms of it with an idle slot in group 12
- [cross_source_blocking] source 1877 seq 3: ready 117 ms before start, 117 ms of it with an idle slot in group 53
- [cross_source_blocking] source 15601 seq 17: ready 194 ms before start, 194 ms of it with an idle slot in group 484
- [cross_source_blocking] source 6652 seq 12: ready 148 ms before start, 148 ms of it with an idle slot in group 224
- [cross_source_blocking] source 8272 seq 10: ready 149 ms before start, 149 ms of it with an idle slot in group 281
- [cross_source_blocking] source 12433 seq 4: ready 269 ms before start, 269 ms of it with an idle slot in group 395
- [cross_source_blocking] source 7388 seq 5: ready 107 ms before start, 107 ms of it with an idle slot in group 254
- [cross_source_blocking] source 15093 seq 2: ready 1637 ms before start, 138 ms of it with an idle slot in group 471
- [cross_source_blocking] source 8469 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 288
- [cross_source_blocking] source 3649 seq 6: ready 336 ms before start, 336 ms of it with an idle slot in group 124
- [cross_source_blocking] source 868 seq 12: ready 177 ms before start, 177 ms of it with an idle slot in group 25
- [cross_source_blocking] source 993 seq 3: ready 120 ms before start, 120 ms of it with an idle slot in group 30
- [cross_source_blocking] source 1873 seq 6: ready 120 ms before start, 120 ms of it with an idle slot in group 53
- [cross_source_blocking] source 7260 seq 7: ready 178 ms before start, 178 ms of it with an idle slot in group 248
- [cross_source_blocking] source 12433 seq 5: ready 170 ms before start, 170 ms of it with an idle slot in group 395
- [cross_source_blocking] source 268 seq 10: ready 327 ms before start, 109 ms of it with an idle slot in group 6
- [cross_source_blocking] source 6656 seq 11: ready 227 ms before start, 227 ms of it with an idle slot in group 225

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 1882 | 1882 | 0 | 48758.783 | 61865.983 | 63963.135 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 1924 | 1924 | 0 | 47185.919 | 61341.695 | 63963.135 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1407 | 1407 | 0 | 43515.903 | 61079.551 | 61865.983 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1754 | 1754 | 0 | 47972.351 | 61079.551 | 62652.415 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 3819 | 3819 | 0 | 47710.207 | 60555.263 | 63700.991 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1802 | 1802 | 0 | 46661.631 | 60555.263 | 61865.983 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3227 | 3227 | 0 | 44564.479 | 60293.119 | 64487.423 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1797 | 1797 | 0 | 45350.911 | 60030.975 | 61603.839 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1773 | 1773 | 0 | 44040.191 | 60030.975 | 62390.271 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3475 | 3475 | 0 | 47448.063 | 59768.831 | 62390.271 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1622 | 1622 | 0 | 45613.055 | 59244.543 | 62652.415 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 6645 | 6645 | 0 | 44564.479 | 59244.543 | 61865.983 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1724 | 1724 | 0 | 47185.919 | 58982.399 | 60817.407 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 1850 | 1850 | 0 | 48496.639 | 58720.255 | 60817.407 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3642 | 3642 | 0 | 48234.495 | 58720.255 | 62652.415 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 5708 | 5708 | 0 | 48234.495 | 58458.111 | 61865.983 |
| t-136 | 146 | 3 | 50.214 | 121.036 | 450 | 0 | 4740 | 4740 | 0 | 46661.631 | 58458.111 | 60555.263 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 5577 | 5577 | 0 | 47710.207 | 58458.111 | 61341.695 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 6795 | 6795 | 0 | 47972.351 | 58458.111 | 60817.407 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1780 | 1780 | 0 | 45088.767 | 58458.111 | 61341.695 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 1898 | 1898 | 0 | 47185.919 | 58195.967 | 60555.263 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 1616 | 1616 | 0 | 44564.479 | 57933.823 | 59768.831 |
| t-055 | 65 | 3 | 125.803 | 130.203 | 359 | 0 | 4872 | 4872 | 0 | 46399.487 | 57933.823 | 58458.111 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 1672 | 1672 | 0 | 47972.351 | 57933.823 | 59244.543 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 1668 | 1668 | 0 | 45350.911 | 57933.823 | 58982.399 |
