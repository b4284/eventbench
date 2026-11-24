# ms-full-8k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (exec, max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 544 workers, offered 7974.007 ev/s
- Phases: run 60.174s, closing 2.816s, drain 0.619s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 518105 | 518105 | 7943.765 | 8402.063 | 12274.2 | 0 | 0 | 0 | 430 | 430 | 18183 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 397699 | 158.304 | 37.087 | 144.639 | 3229.695 | 5464.063 | 7761.919 |
| e2e_from_send | 397699 | 155.781 | 35.231 | 139.775 | 3227.647 | 5451.775 | 7757.823 |
| producer_lag | 397699 | 2.523 | 1.277 | 4.447 | 24.735 | 105.023 | 412.927 |
| publish_call | 518105 | 46.773 | 20.159 | 86.911 | 484.863 | 1369.087 | 2111.487 |
| queue_wait | 397699 | 131.493 | 18.879 | 83.199 | 3186.687 | 5427.199 | 7753.727 |
| broker_dwell | 397699 | 110.292 | 2.383 | 30.623 | 3160.063 | 5402.623 | 7663.615 |
| receive_call | 481478 | 51.225 | 20.639 | 139.647 | 394.751 | 487.679 | 535.039 |
| ctx_load | 397699 | 7.262 | 2.447 | 8.131 | 130.879 | 478.463 | 787.967 |
| process | 397699 | 1.711 | 0.002 | 0.004 | 0.018 | 0.568 | 3000.319 |
| ctx_commit | 397699 | 15.253 | 11.255 | 29.135 | 72.831 | 138.751 | 603.135 |
| ack | 397698 | 0.009 | 0.005 | 0.013 | 0.04 | 0.202 | 44.831 |
| service | 397699 | 24.288 | 14.623 | 38.751 | 168.959 | 663.039 | 3702.783 |
| e2e_large | 333 | 2442.411 | 2377.727 | 3203.071 | 5251.071 | 6623.231 | 6623.231 |
| ready_wait | 397699 | 30.348 | 1.671 | 39.391 | 632.831 | 1257.471 | 3893.247 |
| blocked | 397699 | 19.865 | 1.669 | 35.359 | 355.583 | 657.919 | 1335.295 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8402.063 | 12274.2 |
| applied_total | 292086.857 | 483432 |
| bench_cpu_cores | 4.708 | 5.875 |
| bench_cpu_pct | 19.617 | 24.48 |
| bench_gc_gen0 | 2.102 | 4 |
| bench_gc_gen1 | 0.776 | 2 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 1158.112 | 1499.3 |
| bench_mem_mb | 1876.969 | 2103 |
| bench_threadpool_queue | 19.837 | 159 |
| bench_threadpool_threads | 41.531 | 44 |
| broker_backlog | 3048.918 | 26803 |
| ctr_eb_mssql_cpu_pct | 838.253 | 1068.84 |
| ctr_eb_mssql_mem_mb | 7132.157 | 7190.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.7 | 9.7 |
| ctr_eb_postgres_cpu_pct | 0.273 | 0.61 |
| ctr_eb_postgres_mem_mb | 4045.314 | 4047.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 63.253 | 76.5 |
| in_flight | 1533.082 | 19814 |
| mssql_batch_requests_per_s | 11018.155 | 14159.1 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 19.647 | 42.1 |
| mssql_lock_waits_per_s | 1.22 | 4.3 |
| mssql_log_bytes_flushed_per_s | 39467140.135 | 65417697 |
| mssql_log_flush_waits_per_s | 1443.51 | 2244.7 |
| mssql_log_flushes_per_s | 1031.541 | 1401.2 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 34.68 | 51.7 |
| mssql_transactions_per_s | 1443.971 | 2241.8 |
| mssql_user_connections | 305.327 | 346 |
| mssql_wait_async_network_io_ms_per_s | 46.424 | 75.3 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 4.429 | 51.7 |
| mssql_wait_lck_m_u_ms_per_s | 9.851 | 44 |
| mssql_wait_lck_m_x_ms_per_s | 1.91 | 18.4 |
| mssql_wait_logbuffer_ms_per_s | 12.071 | 114 |
| mssql_wait_pageiolatch_sh_ms_per_s | 5.078 | 27.5 |
| mssql_wait_pagelatch_ex_ms_per_s | 40.802 | 160.5 |
| mssql_wait_pagelatch_sh_ms_per_s | 80.808 | 404.9 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 412.065 | 1415.2 |
| mssql_wait_threadpool_ms_per_s | 1.747 | 12.7 |
| mssql_wait_writelog_ms_per_s | 4165.849 | 7299.2 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.508 | 1.72 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 588659.163 | 979809 |
| octopus_missing_reports_total | 879.122 | 1683 |
| octopus_mqtt_received_total | 176419.449 | 283143 |
| octopus_mqtt_sent_total | 243614.633 | 413489 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 7943.765 | 8336.8 |
| produced_total | 293619.939 | 483865 |
| publish_mb_per_s | 36.114 | 57.84 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,665 records are Completed |
| record_counts | pass | 82,665 records found, 82,665 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 518,105 events applied by workers, 518,105 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,183 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 356 ms, max 1335 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7407 seq 2: ready 118 ms before start, 118 ms of it with an idle slot in group 256
- [cross_source_blocking] source 1793 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 51
- [cross_source_blocking] source 1329 seq 6: ready 113 ms before start, 113 ms of it with an idle slot in group 42
- [cross_source_blocking] source 7297 seq 4: ready 115 ms before start, 115 ms of it with an idle slot in group 250
- [cross_source_blocking] source 9349 seq 3: ready 105 ms before start, 105 ms of it with an idle slot in group 324
- [cross_source_blocking] source 4529 seq 3: ready 113 ms before start, 113 ms of it with an idle slot in group 157
- [cross_source_blocking] source 5401 seq 10: ready 194 ms before start, 110 ms of it with an idle slot in group 184
- [cross_source_blocking] source 13353 seq 14: ready 113 ms before start, 113 ms of it with an idle slot in group 427
- [cross_source_blocking] source 13895 seq 9: ready 244 ms before start, 125 ms of it with an idle slot in group 441
- [cross_source_blocking] source 3522 seq 9: ready 238 ms before start, 111 ms of it with an idle slot in group 120
- [cross_source_blocking] source 3773 seq 7: ready 251 ms before start, 100 ms of it with an idle slot in group 127
- [cross_source_blocking] source 8173 seq 2: ready 306 ms before start, 146 ms of it with an idle slot in group 276
- [cross_source_blocking] source 1766 seq 1: ready 348 ms before start, 157 ms of it with an idle slot in group 50
- [cross_source_blocking] source 15699 seq 11: ready 251 ms before start, 118 ms of it with an idle slot in group 490
- [cross_source_blocking] source 9143 seq 8: ready 333 ms before start, 251 ms of it with an idle slot in group 315
- [cross_source_blocking] source 11552 seq 4: ready 248 ms before start, 105 ms of it with an idle slot in group 376
- [cross_source_blocking] source 12206 seq 3: ready 369 ms before start, 167 ms of it with an idle slot in group 389
- [cross_source_blocking] source 9185 seq 8: ready 334 ms before start, 246 ms of it with an idle slot in group 315
- [cross_source_blocking] source 12237 seq 2: ready 291 ms before start, 155 ms of it with an idle slot in group 389
- [cross_source_blocking] source 1408 seq 3: ready 272 ms before start, 116 ms of it with an idle slot in group 44

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-450 | 63 | 1 | 47.144 | 48.822 | 165 | 0 | 3013 | 3013 | 0 | 38.399 | 6258.687 | 7766.015 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 384 | 384 | 0 | 2047.999 | 6029.311 | 6651.903 |
| t-429 | 200 | 1 | 11.232 | 38.284 | 1395 | 0 | 2733 | 2733 | 0 | 38.143 | 5996.543 | 7700.479 |
| t-340 | 184 | 1 | 15.192 | 46.792 | 141 | 0 | 3205 | 3205 | 0 | 38.911 | 5963.775 | 7536.639 |
| t-314 | 24 | 1 | 116.305 | 46.343 | 525 | 0 | 2926 | 2926 | 0 | 39.167 | 5603.327 | 6750.207 |
| t-106 | 24 | 1 | 110.394 | 44.885 | 824 | 0 | 2762 | 2762 | 0 | 38.911 | 5275.647 | 6389.759 |
| t-350 | 85 | 1 | 33.235 | 45.678 | 285 | 0 | 2958 | 2958 | 0 | 36.607 | 5111.807 | 7372.799 |
| t-348 | 125 | 2 | 39.062 | 81.874 | 1340 | 0 | 5314 | 5314 | 0 | 37.887 | 5013.503 | 6717.439 |
| t-465 | 66 | 1 | 43.859 | 47.323 | 667 | 0 | 2979 | 2979 | 0 | 39.679 | 4980.735 | 6258.687 |
| t-197 | 42 | 1 | 64.221 | 43.742 | 725 | 0 | 2709 | 2709 | 0 | 39.423 | 4947.967 | 6160.383 |
| t-438 | 121 | 2 | 46.304 | 96.951 | 1839 | 0 | 6051 | 6051 | 0 | 38.143 | 4915.199 | 6619.135 |
| t-483 | 200 | 2 | 25.513 | 85.904 | 2470 | 0 | 5635 | 5635 | 0 | 38.143 | 4849.663 | 6324.223 |
| t-160 | 57 | 1 | 35.537 | 33.726 | 548 | 0 | 2162 | 2162 | 0 | 38.911 | 4816.895 | 5931.007 |
| t-194 | 78 | 2 | 60.955 | 81.357 | 1184 | 0 | 5140 | 5140 | 0 | 38.399 | 4751.359 | 6356.991 |
| t-235 | 54 | 1 | 49.212 | 44.3 | 527 | 0 | 2757 | 2757 | 0 | 37.375 | 4718.591 | 6389.759 |
| t-029 | 28 | 1 | 88.83 | 41.191 | 1732 | 0 | 2584 | 2584 | 0 | 39.423 | 4653.055 | 6258.687 |
| t-222 | 138 | 2 | 38.436 | 89.455 | 2846 | 0 | 5681 | 5681 | 0 | 38.911 | 4653.055 | 6684.671 |
| t-378 | 55 | 1 | 53.238 | 47.699 | 3659 | 0 | 3045 | 3045 | 0 | 38.655 | 4620.287 | 5931.007 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 167 | 167 | 0 | 1941.503 | 4587.519 | 5275.647 |
| t-182 | 89 | 2 | 50.803 | 73.915 | 2406 | 0 | 4624 | 4624 | 0 | 38.399 | 4489.215 | 5865.471 |
| t-239 | 39 | 1 | 65.2 | 41.273 | 225 | 0 | 2627 | 2627 | 0 | 36.607 | 4456.447 | 6127.615 |
| t-065 | 80 | 3 | 105.193 | 138.857 | 263 | 0 | 8586 | 8586 | 0 | 38.399 | 4423.679 | 6324.223 |
| t-416 | 57 | 1 | 24.108 | 23.094 | 3430 | 0 | 1534 | 1534 | 0 | 37.631 | 4423.679 | 5341.183 |
| t-210 | 71 | 1 | 35.639 | 40.578 | 136 | 0 | 2671 | 2671 | 0 | 39.167 | 4325.375 | 6160.383 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 81 | 81 | 0 | 2211.839 | 4292.607 | 4292.607 |
