# ms-full-16k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (exec, max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.339s, closing 2.911s, drain 12.219s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 968819 | 968819 | 15280.996 | 12652.331 | 30000.2 | 0 | 0 | 0 | 445 | 445 | 545999 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 765549 | 15374.219 | 16064.511 | 28344.319 | 36241.407 | 41156.607 | 50397.183 |
| e2e_from_send | 765549 | 14734.417 | 15269.887 | 27983.871 | 36077.567 | 40992.767 | 50397.183 |
| producer_lag | 765549 | 639.802 | 77.951 | 2142.207 | 3839.999 | 4534.271 | 5017.599 |
| publish_call | 968819 | 90.007 | 69.503 | 171.391 | 335.871 | 944.127 | 1984.511 |
| queue_wait | 765549 | 14645.431 | 15196.159 | 27918.335 | 36012.031 | 40894.463 | 50364.415 |
| broker_dwell | 765549 | 14566.518 | 15114.239 | 27836.415 | 35946.495 | 40828.927 | 50266.111 |
| receive_call | 877720 | 23.179 | 9.207 | 49.695 | 282.623 | 463.615 | 582.655 |
| ctx_load | 765549 | 27.253 | 7.491 | 31.663 | 601.087 | 1247.231 | 1898.495 |
| process | 765549 | 0.902 | 0.002 | 0.004 | 0.016 | 0.251 | 3002.367 |
| ctx_commit | 765549 | 60.687 | 50.431 | 116.607 | 242.047 | 478.207 | 1767.423 |
| ack | 765548 | 0.011 | 0.005 | 0.009 | 0.037 | 0.675 | 65.279 |
| service | 765549 | 88.993 | 66.111 | 148.735 | 667.647 | 1403.903 | 3708.927 |
| e2e_large | 343 | 3266.727 | 3151.871 | 4820.991 | 6737.919 | 7712.767 | 7712.767 |
| ready_wait | 765549 | 564.203 | 366.847 | 1348.607 | 2871.295 | 4292.607 | 8921.087 |
| blocked | 765549 | 316.192 | 224.255 | 742.399 | 1356.799 | 1970.175 | 2564.095 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12652.331 | 17345.8 |
| applied_total | 351584.898 | 676220 |
| bench_cpu_cores | 5.134 | 5.951 |
| bench_cpu_pct | 21.392 | 24.8 |
| bench_gc_gen0 | 2.735 | 4 |
| bench_gc_gen1 | 1.163 | 2 |
| bench_gc_gen2 | 0.673 | 1 |
| bench_gc_heap_mb | 1318.602 | 1694.4 |
| bench_mem_mb | 2058.773 | 2338 |
| bench_threadpool_queue | 157.367 | 1144 |
| bench_threadpool_threads | 42.633 | 44 |
| broker_backlog | 216767.939 | 254987 |
| ctr_eb_mssql_cpu_pct | 1052.684 | 1172.56 |
| ctr_eb_mssql_mem_mb | 7108.116 | 7270.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 11.3 | 11.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 12 | 12 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 11.2 | 11.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 11.506 | 12.2 |
| ctr_eb_postgres_cpu_pct | 0.576 | 0.83 |
| ctr_eb_postgres_mem_mb | 4366.2 | 4368.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.974 | 80.84 |
| in_flight | 215069.224 | 252442 |
| mssql_batch_requests_per_s | 13660.58 | 16935.7 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 38.629 | 62.4 |
| mssql_lock_waits_per_s | 1.831 | 8.9 |
| mssql_log_bytes_flushed_per_s | 47757856.335 | 64204230.1 |
| mssql_log_flush_waits_per_s | 467.751 | 581.5 |
| mssql_log_flushes_per_s | 899.412 | 1245.4 |
| mssql_processes_blocked | 0.143 | 1 |
| mssql_sql_compilations_per_s | 8.91 | 14.8 |
| mssql_transactions_per_s | 468.296 | 585.1 |
| mssql_user_connections | 407.286 | 470 |
| mssql_wait_async_network_io_ms_per_s | 103.645 | 153.7 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.943 | 13.5 |
| mssql_wait_lck_m_u_ms_per_s | 24.655 | 103.6 |
| mssql_wait_lck_m_x_ms_per_s | 0.143 | 2.3 |
| mssql_wait_logbuffer_ms_per_s | 159.161 | 3515.5 |
| mssql_wait_pageiolatch_sh_ms_per_s | 69.992 | 728.2 |
| mssql_wait_pagelatch_ex_ms_per_s | 354.329 | 626.1 |
| mssql_wait_pagelatch_sh_ms_per_s | 1068.473 | 5637.1 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 2652.622 | 3805.7 |
| mssql_wait_threadpool_ms_per_s | 0.894 | 19.8 |
| mssql_wait_writelog_ms_per_s | 3993.035 | 8138.9 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.573 | 1.81 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 916249.898 | 1618216 |
| octopus_missing_reports_total | 297.755 | 625 |
| octopus_mqtt_received_total | 230026.49 | 370140 |
| octopus_mqtt_sent_total | 244885.408 | 457454 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15280.996 | 18836.1 |
| produced_total | 566654.122 | 928662 |
| publish_mb_per_s | 49.766 | 75.95 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 135,246 records are Completed |
| record_counts | pass | 135,246 records found, 135,246 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 445 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 968,819 events applied by workers, 968,819 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 545,999 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1357 ms, max 2562 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9006 seq 7: ready 112 ms before start, 101 ms of it with an idle slot in group 311
- [cross_source_blocking] source 13669 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 435
- [cross_source_blocking] source 1616 seq 4: ready 267 ms before start, 122 ms of it with an idle slot in group 48
- [cross_source_blocking] source 4527 seq 4: ready 273 ms before start, 142 ms of it with an idle slot in group 157
- [cross_source_blocking] source 1007 seq 5: ready 382 ms before start, 160 ms of it with an idle slot in group 30
- [cross_source_blocking] source 1000 seq 5: ready 375 ms before start, 158 ms of it with an idle slot in group 30
- [cross_source_blocking] source 993 seq 2: ready 307 ms before start, 137 ms of it with an idle slot in group 30
- [cross_source_blocking] source 1611 seq 5: ready 309 ms before start, 151 ms of it with an idle slot in group 48
- [cross_source_blocking] source 13183 seq 5: ready 356 ms before start, 125 ms of it with an idle slot in group 419
- [cross_source_blocking] source 4791 seq 3: ready 125 ms before start, 125 ms of it with an idle slot in group 166
- [cross_source_blocking] source 13334 seq 6: ready 305 ms before start, 112 ms of it with an idle slot in group 426
- [cross_source_blocking] source 13344 seq 6: ready 573 ms before start, 109 ms of it with an idle slot in group 426
- [cross_source_blocking] source 2184 seq 6: ready 634 ms before start, 144 ms of it with an idle slot in group 62
- [cross_source_blocking] source 6042 seq 8: ready 265 ms before start, 102 ms of it with an idle slot in group 207
- [cross_source_blocking] source 1612 seq 7: ready 354 ms before start, 106 ms of it with an idle slot in group 48
- [cross_source_blocking] source 6057 seq 8: ready 272 ms before start, 109 ms of it with an idle slot in group 207
- [cross_source_blocking] source 8516 seq 5: ready 256 ms before start, 147 ms of it with an idle slot in group 292
- [cross_source_blocking] source 13335 seq 7: ready 231 ms before start, 107 ms of it with an idle slot in group 426
- [cross_source_blocking] source 6110 seq 5: ready 637 ms before start, 144 ms of it with an idle slot in group 209
- [cross_source_blocking] source 13332 seq 9: ready 222 ms before start, 103 ms of it with an idle slot in group 426

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3011 | 3011 | 0 | 26214.399 | 44302.335 | 50331.647 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2783 | 2783 | 0 | 22282.239 | 43253.759 | 50593.791 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2994 | 2994 | 0 | 26607.615 | 42991.615 | 46923.775 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5794 | 5794 | 0 | 26214.399 | 42467.327 | 45613.055 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3100 | 3100 | 0 | 22937.599 | 42205.183 | 46137.343 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 5891 | 5891 | 0 | 23855.103 | 41943.039 | 46399.487 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2767 | 2767 | 0 | 21626.879 | 41943.039 | 44302.335 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 8916 | 8916 | 0 | 26476.543 | 41418.751 | 47185.919 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2724 | 2724 | 0 | 23068.671 | 41156.607 | 44040.191 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2827 | 2827 | 0 | 21233.663 | 40894.463 | 44040.191 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 2496 | 2496 | 0 | 19922.943 | 40632.319 | 43778.047 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8811 | 8811 | 0 | 25690.111 | 40632.319 | 46137.343 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 2168 | 2168 | 0 | 19136.511 | 40632.319 | 42991.615 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 4948 | 4948 | 0 | 17956.863 | 40370.175 | 49807.359 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 2590 | 2590 | 0 | 22413.311 | 40108.031 | 43515.903 |
| t-389 | 64 | 1 | 34.804 | 37.251 | 281 | 0 | 2360 | 2360 | 0 | 22413.311 | 39845.887 | 42729.471 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 10447 | 10447 | 0 | 21102.591 | 39583.743 | 48234.495 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2752 | 2752 | 0 | 19660.799 | 39321.599 | 41943.039 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2874 | 2874 | 0 | 23724.031 | 39321.599 | 42205.183 |
| t-080 | 56 | 1 | 40.902 | 38.54 | 822 | 0 | 2357 | 2357 | 0 | 19529.727 | 38797.311 | 44040.191 |
| t-206 | 118 | 3 | 60.3 | 116.185 | 1969 | 0 | 7111 | 7111 | 0 | 21495.807 | 38797.311 | 41680.895 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2641 | 2641 | 0 | 23199.743 | 38797.311 | 43253.759 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11468 | 11468 | 0 | 24117.247 | 38797.311 | 41680.895 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2601 | 2601 | 0 | 20709.375 | 38535.167 | 43515.903 |
| t-293 | 46 | 1 | 52.367 | 40.187 | 2741 | 0 | 2464 | 2464 | 0 | 22544.383 | 38535.167 | 40632.319 |
