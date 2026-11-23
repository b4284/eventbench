# Bd-ag-full-x64-pool64 — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `sqlserver @ 127.0.0.1,14330/bench (pool max 400), commit = proc with UPDLOCK cursor, delayed durability = server setting, group commit (max 64/tx, 64 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.6s, closing 5.703s, drain 19.047s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 658543 | 658543 | 9937.735 | 7135.047 | 17558.2 | 0 | 0 | 0 | 331 | 3346 | 351480 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 457190 | 34336.053 | 37158.911 | 47742.975 | 53641.215 | 57606.143 | 63406.079 |
| e2e_from_send | 457190 | 23239.21 | 24526.847 | 39354.367 | 47284.223 | 52658.175 | 59932.671 |
| producer_lag | 457190 | 11096.868 | 10174.463 | 18890.751 | 21495.807 | 22708.223 | 24018.943 |
| publish_call | 658543 | 172.512 | 146.559 | 297.215 | 614.399 | 1528.831 | 1773.567 |
| queue_wait | 457190 | 23098.122 | 24412.159 | 39256.063 | 47218.687 | 52592.639 | 59899.903 |
| broker_dwell | 457190 | 22929.216 | 24248.319 | 39092.223 | 47087.615 | 52428.799 | 59867.135 |
| receive_call | 604125 | 23.421 | 9.247 | 40.831 | 322.047 | 481.535 | 585.215 |
| ctx_load | 457190 | 49.342 | 13.303 | 57.663 | 1122.303 | 2036.735 | 3264.511 |
| process | 457190 | 10.764 | 0.002 | 0.004 | 0.044 | 2611.199 | 3059.711 |
| ctx_commit | 457190 | 80.774 | 49.663 | 118.079 | 315.135 | 3815.423 | 3876.863 |
| ack | 457190 | 0.015 | 0.005 | 0.011 | 0.054 | 1.861 | 54.975 |
| service | 457190 | 141.06 | 70.527 | 174.335 | 1908.735 | 3846.143 | 6291.455 |
| e2e_large | 229 | 13368.423 | 12402.687 | 21987.327 | 24215.551 | 25198.591 | 25198.591 |
| ready_wait | 457190 | 836.273 | 501.759 | 1882.111 | 5505.023 | 8110.079 | 13852.671 |
| blocked | 457190 | 353.199 | 249.343 | 826.879 | 1604.607 | 2412.543 | 2899.967 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7135.047 | 13064 |
| applied_total | 183179.918 | 373974 |
| bench_cpu_cores | 3.332 | 4.671 |
| bench_cpu_pct | 13.882 | 19.46 |
| bench_gc_gen0 | 2.204 | 4 |
| bench_gc_gen1 | 1.184 | 3 |
| bench_gc_gen2 | 0.633 | 1 |
| bench_gc_heap_mb | 1044.114 | 1413 |
| bench_mem_mb | 1713.702 | 1827.2 |
| bench_threadpool_queue | 110.939 | 886 |
| bench_threadpool_threads | 43.612 | 44 |
| broker_backlog | 213654.51 | 270691 |
| ctr_eb_ag1_cpu_pct | 986.48 | 1081.23 |
| ctr_eb_ag1_mem_mb | 7121.241 | 7246 |
| ctr_eb_ag2_cpu_pct | 214.061 | 244.57 |
| ctr_eb_ag2_mem_mb | 7016.665 | 7093.4 |
| ctr_eb_ag_haproxy_cpu_pct | 67.055 | 86.25 |
| ctr_eb_ag_haproxy_mem_mb | 32.316 | 35.2 |
| ctr_eb_mysql_cpu_pct | 0.768 | 0.81 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 8.1 | 8.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.12 | 0.38 |
| ctr_eb_postgres_mem_mb | 2269.7 | 2269.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.622 | 84.18 |
| in_flight | 213987.265 | 266666 |
| mssql_batch_requests_per_s | 8258.998 | 12494.6 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 52.839 | 162.9 |
| mssql_lock_waits_per_s | 8.702 | 16.9 |
| mssql_log_bytes_flushed_per_s | 30534723.618 | 42431201.4 |
| mssql_log_flush_waits_per_s | 127.08 | 188.1 |
| mssql_log_flushes_per_s | 507.306 | 705.4 |
| mssql_processes_blocked | 0.51 | 2 |
| mssql_sql_compilations_per_s | 6.282 | 9.7 |
| mssql_transactions_per_s | 999.018 | 1500.4 |
| mssql_user_connections | 372.061 | 469 |
| mssql_wait_async_network_io_ms_per_s | 129.049 | 202.1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 21832.19 | 58329.6 |
| mssql_wait_lck_m_s_ms_per_s | 0.347 | 11.2 |
| mssql_wait_lck_m_u_ms_per_s | 307.886 | 1101.1 |
| mssql_wait_lck_m_x_ms_per_s | 0.018 | 0.9 |
| mssql_wait_logbuffer_ms_per_s | 4.555 | 36.9 |
| mssql_wait_pageiolatch_sh_ms_per_s | 35.882 | 308.3 |
| mssql_wait_pagelatch_ex_ms_per_s | 286.7 | 1046.1 |
| mssql_wait_pagelatch_sh_ms_per_s | 8615.829 | 184316.8 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 5067.835 | 10381.9 |
| mssql_wait_threadpool_ms_per_s | 0.208 | 3.4 |
| mssql_wait_writelog_ms_per_s | 1012.608 | 1612.9 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.564 | 2.02 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 588789.02 | 1002369 |
| octopus_missing_reports_total | 259.367 | 540 |
| octopus_mqtt_received_total | 215887.286 | 348059 |
| octopus_mqtt_sent_total | 138769.653 | 257741 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 9937.735 | 16331.4 |
| produced_total | 397167.184 | 621733 |
| publish_mb_per_s | 34.171 | 66.74 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 99,457 records are Completed |
| record_counts | pass | 99,457 records found, 99,457 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 331 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 658,543 events applied by workers, 658,543 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 351,480 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1605 ms, max 2900 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6396 seq 9: ready 120 ms before start, 120 ms of it with an idle slot in group 219
- [cross_source_blocking] source 6658 seq 7: ready 150 ms before start, 150 ms of it with an idle slot in group 225
- [cross_source_blocking] source 408 seq 8: ready 151 ms before start, 151 ms of it with an idle slot in group 13
- [cross_source_blocking] source 8500 seq 14: ready 153 ms before start, 153 ms of it with an idle slot in group 291
- [cross_source_blocking] source 4500 seq 10: ready 191 ms before start, 191 ms of it with an idle slot in group 156
- [cross_source_blocking] source 14620 seq 8: ready 190 ms before start, 190 ms of it with an idle slot in group 462
- [cross_source_blocking] source 8470 seq 9: ready 128 ms before start, 128 ms of it with an idle slot in group 288
- [cross_source_blocking] source 7310 seq 4: ready 127 ms before start, 127 ms of it with an idle slot in group 251
- [cross_source_blocking] source 13215 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 422
- [cross_source_blocking] source 14634 seq 4: ready 164 ms before start, 164 ms of it with an idle slot in group 463
- [cross_source_blocking] source 4482 seq 4: ready 163 ms before start, 163 ms of it with an idle slot in group 155
- [cross_source_blocking] source 8500 seq 15: ready 184 ms before start, 184 ms of it with an idle slot in group 291
- [cross_source_blocking] source 852 seq 15: ready 104 ms before start, 104 ms of it with an idle slot in group 24
- [cross_source_blocking] source 5568 seq 4: ready 209 ms before start, 209 ms of it with an idle slot in group 192
- [cross_source_blocking] source 4400 seq 6: ready 209 ms before start, 209 ms of it with an idle slot in group 151
- [cross_source_blocking] source 5867 seq 4: ready 121 ms before start, 121 ms of it with an idle slot in group 203
- [cross_source_blocking] source 402 seq 21: ready 185 ms before start, 185 ms of it with an idle slot in group 12
- [cross_source_blocking] source 7914 seq 25: ready 113 ms before start, 113 ms of it with an idle slot in group 266
- [cross_source_blocking] source 13666 seq 7: ready 122 ms before start, 122 ms of it with an idle slot in group 435
- [cross_source_blocking] source 13607 seq 9: ready 158 ms before start, 158 ms of it with an idle slot in group 431

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2027 | 2027 | 0 | 47185.919 | 60293.119 | 63438.847 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 7174 | 7174 | 0 | 45088.767 | 58720.255 | 62914.559 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2092 | 2092 | 0 | 44040.191 | 58458.111 | 61079.551 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 4064 | 4064 | 0 | 45613.055 | 58458.111 | 61341.695 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 1930 | 1930 | 0 | 44826.623 | 58195.967 | 59506.687 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 3916 | 3916 | 0 | 48234.495 | 58195.967 | 62652.415 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 1886 | 1886 | 0 | 42205.183 | 58195.967 | 61079.551 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 3431 | 3431 | 0 | 41680.895 | 57933.823 | 63438.847 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 1496 | 1496 | 0 | 39059.455 | 57671.679 | 58720.255 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 6116 | 6116 | 0 | 47185.919 | 57409.535 | 60293.119 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2037 | 2037 | 0 | 45875.199 | 57409.535 | 58982.399 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 3711 | 3711 | 0 | 44302.335 | 57147.391 | 61079.551 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1861 | 1861 | 0 | 44040.191 | 56623.103 | 58458.111 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 1983 | 1983 | 0 | 45613.055 | 56623.103 | 57933.823 |
| t-088 | 46 | 1 | 53.823 | 40.61 | 203 | 0 | 1697 | 1697 | 0 | 42991.615 | 56360.959 | 60555.263 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 5942 | 5942 | 0 | 45350.911 | 56360.959 | 59768.831 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 7273 | 7273 | 0 | 46137.343 | 56360.959 | 59244.543 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 1856 | 1856 | 0 | 42205.183 | 56360.959 | 57409.535 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 1800 | 1800 | 0 | 45613.055 | 56098.815 | 59244.543 |
| t-293 | 46 | 1 | 52.367 | 40.187 | 2741 | 0 | 1690 | 1690 | 0 | 44564.479 | 56098.815 | 57933.823 |
| t-341 | 96 | 1 | 25.054 | 40.263 | 2651 | 0 | 1732 | 1732 | 0 | 44040.191 | 56098.815 | 58982.399 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 1781 | 1781 | 0 | 44302.335 | 55836.671 | 58720.255 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 1750 | 1750 | 0 | 41418.751 | 55836.671 | 58982.399 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 1916 | 1916 | 0 | 41156.607 | 55836.671 | 56885.247 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 10828 | 10828 | 0 | 46399.487 | 55312.383 | 56885.247 |
