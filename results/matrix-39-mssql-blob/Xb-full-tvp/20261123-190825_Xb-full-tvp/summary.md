# Xb-full-tvp — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (tvp, max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.358s, closing 2.683s, drain 20.276s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 986185 | 986185 | 15602.318 | 11366.961 | 25453.8 | 0 | 0 | 0 | 449 | 5090 | 551894 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 782882 | 18569.637 | 19726.335 | 33062.911 | 40992.767 | 45776.895 | 55738.367 |
| e2e_from_send | 782882 | 18342.7 | 19464.191 | 32866.303 | 40796.159 | 45678.591 | 55640.063 |
| producer_lag | 782882 | 226.945 | 14.663 | 678.911 | 2197.503 | 3379.199 | 3839.999 |
| publish_call | 986185 | 85.386 | 69.055 | 155.263 | 286.719 | 1413.119 | 2003.967 |
| queue_wait | 782882 | 18240.238 | 19382.271 | 32784.383 | 40730.623 | 45613.055 | 55607.295 |
| broker_dwell | 782882 | 18168.882 | 19300.351 | 32718.847 | 40632.319 | 45514.751 | 55312.383 |
| receive_call | 918276 | 20.719 | 8.155 | 39.679 | 267.263 | 460.031 | 560.127 |
| ctx_load | 782882 | 19.075 | 6.167 | 24.607 | 344.831 | 1007.103 | 1683.455 |
| process | 782882 | 10.755 | 0.002 | 0.004 | 0.026 | 2617.343 | 3024.895 |
| ctx_commit | 782882 | 72.507 | 57.471 | 155.135 | 251.519 | 365.055 | 1491.967 |
| ack | 782880 | 0.01 | 0.005 | 0.009 | 0.035 | 0.489 | 58.143 |
| service | 782882 | 102.459 | 70.975 | 182.911 | 709.631 | 2723.839 | 4358.143 |
| e2e_large | 347 | 3064.71 | 2916.351 | 4395.007 | 6840.319 | 8019.967 | 8019.967 |
| ready_wait | 782882 | 624.062 | 383.999 | 1510.399 | 3313.663 | 4898.815 | 7778.303 |
| blocked | 782882 | 303.411 | 207.999 | 742.911 | 1354.751 | 2072.575 | 2390.015 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11366.961 | 14495.5 |
| applied_total | 323386.041 | 602810 |
| bench_cpu_cores | 5.041 | 5.877 |
| bench_cpu_pct | 21.006 | 24.49 |
| bench_gc_gen0 | 2.796 | 5 |
| bench_gc_gen1 | 1.571 | 4 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 1304.106 | 1656 |
| bench_mem_mb | 2014.38 | 2338.6 |
| bench_threadpool_queue | 96.469 | 1123 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 249341.388 | 342127 |
| ctr_eb_mssql_cpu_pct | 1075.762 | 1189.81 |
| ctr_eb_mssql_mem_mb | 7091.608 | 7280.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.699 | 0.93 |
| ctr_eb_postgres_mem_mb | 2499.273 | 2499.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.88 | 85.57 |
| in_flight | 250682.367 | 341999 |
| mssql_batch_requests_per_s | 12432.965 | 14618.8 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 132.392 | 170.9 |
| mssql_lock_waits_per_s | 2.408 | 11.7 |
| mssql_log_bytes_flushed_per_s | 44850231.02 | 61715248.1 |
| mssql_log_flush_waits_per_s | 396.327 | 526.6 |
| mssql_log_flushes_per_s | 823.896 | 1196 |
| mssql_processes_blocked | 0.449 | 16 |
| mssql_sql_compilations_per_s | 396.604 | 531.3 |
| mssql_transactions_per_s | 1174.12 | 1562.6 |
| mssql_user_connections | 407.265 | 470 |
| mssql_wait_async_network_io_ms_per_s | 85.124 | 124.1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.202 | 3.3 |
| mssql_wait_lck_m_u_ms_per_s | 45.118 | 195.4 |
| mssql_wait_lck_m_x_ms_per_s | 0.02 | 0.5 |
| mssql_wait_logbuffer_ms_per_s | 18.443 | 139.4 |
| mssql_wait_pageiolatch_sh_ms_per_s | 55.031 | 414.8 |
| mssql_wait_pagelatch_ex_ms_per_s | 194.465 | 394.3 |
| mssql_wait_pagelatch_sh_ms_per_s | 695.353 | 1650.7 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 2984.271 | 6373.4 |
| mssql_wait_threadpool_ms_per_s | 0.086 | 2.1 |
| mssql_wait_writelog_ms_per_s | 3911.035 | 5856.6 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.571 | 1.85 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 897749.02 | 1561914 |
| octopus_missing_reports_total | 472.265 | 1087 |
| octopus_mqtt_received_total | 231094.592 | 378502 |
| octopus_mqtt_sent_total | 227750.633 | 419362 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15602.318 | 17721.5 |
| produced_total | 574068.408 | 944809 |
| publish_mb_per_s | 50.816 | 76.46 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,241 records are Completed |
| record_counts | pass | 137,241 records found, 137,241 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 449 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 986,185 events applied by workers, 986,185 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 551,894 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1355 ms, max 2389 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13344 seq 6: ready 374 ms before start, 115 ms of it with an idle slot in group 426
- [cross_source_blocking] source 14395 seq 4: ready 678 ms before start, 114 ms of it with an idle slot in group 459
- [cross_source_blocking] source 2940 seq 9: ready 702 ms before start, 101 ms of it with an idle slot in group 94
- [cross_source_blocking] source 12383 seq 4: ready 334 ms before start, 120 ms of it with an idle slot in group 393
- [cross_source_blocking] source 14416 seq 5: ready 581 ms before start, 120 ms of it with an idle slot in group 459
- [cross_source_blocking] source 11214 seq 8: ready 156 ms before start, 156 ms of it with an idle slot in group 365
- [cross_source_blocking] source 15754 seq 5: ready 156 ms before start, 156 ms of it with an idle slot in group 492
- [cross_source_blocking] source 5853 seq 4: ready 573 ms before start, 162 ms of it with an idle slot in group 201
- [cross_source_blocking] source 857 seq 6: ready 201 ms before start, 100 ms of it with an idle slot in group 24
- [cross_source_blocking] source 12806 seq 2: ready 362 ms before start, 111 ms of it with an idle slot in group 404
- [cross_source_blocking] source 6054 seq 4: ready 534 ms before start, 150 ms of it with an idle slot in group 207
- [cross_source_blocking] source 6409 seq 5: ready 307 ms before start, 125 ms of it with an idle slot in group 220
- [cross_source_blocking] source 6062 seq 4: ready 448 ms before start, 145 ms of it with an idle slot in group 207
- [cross_source_blocking] source 5770 seq 2: ready 558 ms before start, 123 ms of it with an idle slot in group 199
- [cross_source_blocking] source 12804 seq 5: ready 282 ms before start, 109 ms of it with an idle slot in group 404
- [cross_source_blocking] source 15283 seq 4: ready 897 ms before start, 168 ms of it with an idle slot in group 477
- [cross_source_blocking] source 6047 seq 3: ready 377 ms before start, 137 ms of it with an idle slot in group 207
- [cross_source_blocking] source 6368 seq 6: ready 479 ms before start, 167 ms of it with an idle slot in group 218
- [cross_source_blocking] source 6413 seq 5: ready 225 ms before start, 119 ms of it with an idle slot in group 220
- [cross_source_blocking] source 12812 seq 4: ready 298 ms before start, 116 ms of it with an idle slot in group 404

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2823 | 2823 | 0 | 26345.471 | 48234.495 | 55836.671 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6017 | 6017 | 0 | 30539.775 | 47972.351 | 51904.511 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 5041 | 5041 | 0 | 23461.887 | 47448.063 | 55312.383 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2808 | 2808 | 0 | 27656.191 | 47185.919 | 48758.783 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3152 | 3152 | 0 | 27131.903 | 46661.631 | 50331.647 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2873 | 2873 | 0 | 27918.335 | 46661.631 | 49283.071 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 2202 | 2202 | 0 | 23068.671 | 46137.343 | 49020.927 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8967 | 8967 | 0 | 31850.495 | 45875.199 | 50593.791 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5558 | 5558 | 0 | 28573.695 | 45875.199 | 50855.935 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 2543 | 2543 | 0 | 25296.895 | 45613.055 | 48234.495 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9086 | 9086 | 0 | 30146.559 | 45088.767 | 49807.359 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 2624 | 2624 | 0 | 26476.543 | 45088.767 | 49020.927 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2744 | 2744 | 0 | 26083.327 | 44826.623 | 46661.631 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5893 | 5893 | 0 | 28573.695 | 44826.623 | 49283.071 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2682 | 2682 | 0 | 28442.623 | 44564.479 | 48496.639 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2645 | 2645 | 0 | 27000.831 | 44302.335 | 47972.351 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3054 | 3054 | 0 | 28442.623 | 44302.335 | 49020.927 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 10646 | 10646 | 0 | 25296.895 | 44302.335 | 52166.655 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2788 | 2788 | 0 | 25296.895 | 43778.047 | 46923.775 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3068 | 3068 | 0 | 26607.615 | 43515.903 | 51642.367 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2798 | 2798 | 0 | 24379.391 | 43515.903 | 45613.055 |
| t-391 | 61 | 1 | 39.352 | 39.5 | 225 | 0 | 2473 | 2473 | 0 | 23592.959 | 43515.903 | 47185.919 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2934 | 2934 | 0 | 27131.903 | 43515.903 | 46661.631 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11692 | 11692 | 0 | 29622.271 | 43515.903 | 47185.919 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 2728 | 2728 | 0 | 27262.975 | 43253.759 | 46923.775 |
