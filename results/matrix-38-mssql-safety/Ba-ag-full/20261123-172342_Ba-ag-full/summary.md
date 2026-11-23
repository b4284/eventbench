# Ba-ag-full — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.429s, closing 5.122s, drain 26.979s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 665744 | 665744 | 10074.059 | 7177.169 | 17234 | 0 | 0 | 0 | 332 | 3387 | 333298 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 464334 | 35542.146 | 37879.807 | 52035.583 | 59146.239 | 63438.847 | 69402.623 |
| e2e_from_send | 464334 | 23927.104 | 26116.095 | 40730.623 | 50987.007 | 57343.999 | 65667.071 |
| producer_lag | 464334 | 11615.019 | 11558.911 | 19070.975 | 21823.487 | 23560.191 | 24346.623 |
| publish_call | 665744 | 170.581 | 147.199 | 296.191 | 581.119 | 1556.479 | 2590.719 |
| queue_wait | 464334 | 23754.801 | 25985.023 | 40632.319 | 50888.703 | 57278.463 | 65634.303 |
| broker_dwell | 464334 | 23586.694 | 25821.183 | 40468.479 | 50724.863 | 57114.623 | 65535.999 |
| receive_call | 629926 | 21.945 | 7.599 | 38.559 | 314.367 | 478.207 | 573.439 |
| ctx_load | 464334 | 32.634 | 7.891 | 29.663 | 746.495 | 1689.599 | 3569.663 |
| process | 464334 | 10.777 | 0.002 | 0.004 | 0.038 | 2613.247 | 3014.655 |
| ctx_commit | 464334 | 128.761 | 80.831 | 205.439 | 1075.199 | 4128.767 | 4288.511 |
| ack | 464334 | 0.013 | 0.005 | 0.01 | 0.049 | 0.944 | 61.343 |
| service | 464334 | 172.303 | 97.919 | 255.231 | 1816.575 | 4243.455 | 7110.655 |
| e2e_large | 230 | 13632.815 | 12861.439 | 21544.959 | 23691.263 | 25034.751 | 25034.751 |
| ready_wait | 464334 | 968.614 | 557.567 | 2246.655 | 6172.671 | 10035.199 | 14000.127 |
| blocked | 464334 | 298.821 | 206.719 | 733.695 | 1392.639 | 2164.735 | 2832.383 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7177.169 | 11477.1 |
| applied_total | 199645.612 | 387982 |
| bench_cpu_cores | 3.236 | 4.361 |
| bench_cpu_pct | 13.485 | 18.17 |
| bench_gc_gen0 | 2.143 | 4 |
| bench_gc_gen1 | 1.388 | 3 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 1062.339 | 1493.3 |
| bench_mem_mb | 1697.394 | 1870.1 |
| bench_threadpool_queue | 72.714 | 432 |
| bench_threadpool_threads | 43.041 | 44 |
| broker_backlog | 193186.429 | 242917 |
| ctr_eb_ag1_cpu_pct | 886.551 | 1019.94 |
| ctr_eb_ag1_mem_mb | 7245.953 | 7515 |
| ctr_eb_ag2_cpu_pct | 207.679 | 308.84 |
| ctr_eb_ag2_mem_mb | 6590.561 | 6769.2 |
| ctr_eb_ag_haproxy_cpu_pct | 64.091 | 82.53 |
| ctr_eb_ag_haproxy_mem_mb | 26.294 | 30.5 |
| ctr_eb_mysql_cpu_pct | 0.758 | 0.92 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.812 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.539 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.694 | 8.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.865 | 8.3 |
| ctr_eb_postgres_cpu_pct | 0.11 | 0.34 |
| ctr_eb_postgres_mem_mb | 2304.89 | 2312.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.581 | 82.56 |
| in_flight | 193721.306 | 239939 |
| mssql_batch_requests_per_s | 7713.327 | 10948.7 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 45.112 | 147.2 |
| mssql_lock_waits_per_s | 3.506 | 7.5 |
| mssql_log_bytes_flushed_per_s | 31028626.212 | 46990222.2 |
| mssql_log_flush_waits_per_s | 84.72 | 157.1 |
| mssql_log_flushes_per_s | 522.206 | 782.6 |
| mssql_processes_blocked | 0.102 | 1 |
| mssql_sql_compilations_per_s | 8.169 | 18.3 |
| mssql_transactions_per_s | 258.447 | 381 |
| mssql_user_connections | 354.918 | 469 |
| mssql_wait_async_network_io_ms_per_s | 143.508 | 279 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 7887.176 | 15716.1 |
| mssql_wait_lck_m_s_ms_per_s | 1.622 | 31.6 |
| mssql_wait_lck_m_u_ms_per_s | 60.09 | 324.9 |
| mssql_wait_lck_m_x_ms_per_s | 0.396 | 11.9 |
| mssql_wait_logbuffer_ms_per_s | 6.849 | 50.2 |
| mssql_wait_pageiolatch_sh_ms_per_s | 12.114 | 59 |
| mssql_wait_pagelatch_ex_ms_per_s | 167.637 | 458.8 |
| mssql_wait_pagelatch_sh_ms_per_s | 6257.061 | 136692.9 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 3295.331 | 6350.7 |
| mssql_wait_threadpool_ms_per_s | 2.231 | 36.1 |
| mssql_wait_writelog_ms_per_s | 828.278 | 1762.4 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.585 | 2.01 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 602340.408 | 1046675 |
| octopus_missing_reports_total | 347.408 | 797 |
| octopus_mqtt_received_total | 210801.714 | 345151 |
| octopus_mqtt_sent_total | 150498.327 | 282377 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10074.059 | 14046.6 |
| produced_total | 393366.918 | 627921 |
| publish_mb_per_s | 34.823 | 63.68 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 100,314 records are Completed |
| record_counts | pass | 100,314 records found, 100,314 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 332 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 665,744 events applied by workers, 665,744 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 333,298 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1393 ms, max 2832 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15240 seq 5: ready 196 ms before start, 196 ms of it with an idle slot in group 475
- [cross_source_blocking] source 13603 seq 3: ready 150 ms before start, 150 ms of it with an idle slot in group 430
- [cross_source_blocking] source 700 seq 4: ready 131 ms before start, 131 ms of it with an idle slot in group 21
- [cross_source_blocking] source 5416 seq 12: ready 130 ms before start, 130 ms of it with an idle slot in group 185
- [cross_source_blocking] source 8476 seq 16: ready 153 ms before start, 115 ms of it with an idle slot in group 289
- [cross_source_blocking] source 13607 seq 9: ready 513 ms before start, 513 ms of it with an idle slot in group 431
- [cross_source_blocking] source 14159 seq 3: ready 222 ms before start, 127 ms of it with an idle slot in group 451
- [cross_source_blocking] source 10316 seq 4: ready 174 ms before start, 174 ms of it with an idle slot in group 345
- [cross_source_blocking] source 5007 seq 8: ready 317 ms before start, 114 ms of it with an idle slot in group 173
- [cross_source_blocking] source 2792 seq 8: ready 175 ms before start, 175 ms of it with an idle slot in group 87
- [cross_source_blocking] source 15747 seq 2: ready 321 ms before start, 146 ms of it with an idle slot in group 491
- [cross_source_blocking] source 1896 seq 8: ready 551 ms before start, 155 ms of it with an idle slot in group 54
- [cross_source_blocking] source 2543 seq 4: ready 395 ms before start, 129 ms of it with an idle slot in group 76
- [cross_source_blocking] source 2972 seq 8: ready 418 ms before start, 145 ms of it with an idle slot in group 97
- [cross_source_blocking] source 860 seq 4: ready 630 ms before start, 104 ms of it with an idle slot in group 24
- [cross_source_blocking] source 8508 seq 9: ready 1440 ms before start, 128 ms of it with an idle slot in group 292
- [cross_source_blocking] source 4795 seq 7: ready 146 ms before start, 146 ms of it with an idle slot in group 167
- [cross_source_blocking] source 12387 seq 5: ready 711 ms before start, 183 ms of it with an idle slot in group 393
- [cross_source_blocking] source 1891 seq 10: ready 343 ms before start, 100 ms of it with an idle slot in group 54
- [cross_source_blocking] source 14403 seq 4: ready 886 ms before start, 104 ms of it with an idle slot in group 459

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2046 | 2046 | 0 | 49545.215 | 65535.999 | 68157.439 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1904 | 1904 | 0 | 46399.487 | 65273.855 | 68157.439 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4108 | 4108 | 0 | 49807.359 | 64749.567 | 68157.439 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1937 | 1937 | 0 | 48234.495 | 64487.423 | 65798.143 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1944 | 1944 | 0 | 47448.063 | 64487.423 | 65798.143 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3757 | 3757 | 0 | 49020.927 | 64225.279 | 66846.719 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3456 | 3456 | 0 | 45350.911 | 64225.279 | 69730.303 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1873 | 1873 | 0 | 48496.639 | 64225.279 | 65273.855 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7250 | 7250 | 0 | 47448.063 | 64225.279 | 67633.151 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2115 | 2115 | 0 | 46661.631 | 63963.135 | 68157.439 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 6024 | 6024 | 0 | 51642.367 | 63963.135 | 67108.863 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1508 | 1508 | 0 | 42729.471 | 63438.847 | 65535.999 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3972 | 3972 | 0 | 49545.215 | 62914.559 | 67633.151 |
| t-088 | 46 | 1 | 53.823 | 40.61 | 203 | 0 | 1710 | 1710 | 0 | 46399.487 | 62390.271 | 66060.287 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2062 | 2062 | 0 | 49283.071 | 62390.271 | 65273.855 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6199 | 6199 | 0 | 49807.359 | 62128.127 | 65798.143 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1880 | 1880 | 0 | 46923.775 | 62128.127 | 63963.135 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 1935 | 1935 | 0 | 45875.199 | 62128.127 | 65273.855 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 1745 | 1745 | 0 | 44826.623 | 61865.983 | 63176.703 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 7366 | 7366 | 0 | 49283.071 | 61865.983 | 64225.279 |
| t-047 | 52 | 1 | 42.703 | 38.205 | 343 | 0 | 1564 | 1564 | 0 | 45088.767 | 61603.839 | 63963.135 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 1805 | 1805 | 0 | 46661.631 | 61603.839 | 63963.135 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 1818 | 1818 | 0 | 48234.495 | 61603.839 | 63176.703 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1771 | 1771 | 0 | 45613.055 | 61603.839 | 65273.855 |
| t-293 | 46 | 1 | 52.367 | 40.187 | 2741 | 0 | 1712 | 1712 | 0 | 46137.343 | 61341.695 | 62914.559 |
