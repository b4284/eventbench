# 01-sqlq_mssql-txack — PASS ✅

- Broker: `sqlqueue table eb_q, per-source FIFO head dequeue (UPDLOCK+READPAST), vt=30s, delete on ack @ 127.0.0.1,1433/bench (pool max 1000)`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.928s, closing 2.523s, drain 60.715s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 779698 | 779698 | 12413.384 | 11024.871 | 17922.7 | 0 | 0 | 0 | 415 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 608145 | 8711.544 | 4730.879 | 23019.519 | 48693.247 | 58490.879 | 64552.959 |
| e2e_from_send | 608145 | 7735.744 | 3289.087 | 22249.471 | 47611.903 | 56426.495 | 61407.231 |
| producer_lag | 608145 | 975.802 | 286.463 | 3026.943 | 5955.583 | 8359.935 | 9510.911 |
| publish_call | 11884 | 166.786 | 17.903 | 382.719 | 3184.639 | 4222.975 | 6348.799 |
| queue_wait | 608145 | 7597.796 | 3153.919 | 22102.015 | 47448.063 | 56262.655 | 61341.695 |
| broker_dwell | 608145 | 7508.749 | 3024.895 | 22036.479 | 47382.527 | 56197.119 | 61276.159 |
| receive_call | 137547 | 106.751 | 17.039 | 93.695 | 3409.919 | 4036.607 | 4616.191 |
| ctx_load | 608145 | 13.713 | 10.623 | 23.711 | 53.983 | 108.287 | 4493.311 |
| process | 608145 | 0.003 | 0.002 | 0.003 | 0.006 | 0.117 | 51.487 |
| ctx_commit | 608145 | 15.594 | 11.463 | 27.231 | 64.991 | 169.855 | 4280.319 |
| service | 608145 | 137.948 | 106.239 | 271.615 | 450.303 | 3336.191 | 10502.143 |
| e2e_large | 315 | 4917.648 | 4255.743 | 9625.599 | 12279.807 | 13901.823 | 13901.823 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11024.871 | 16495.2 |
| applied_total | 390198.204 | 630228 |
| bench_cpu_cores | 2.813 | 3.866 |
| bench_cpu_pct | 11.719 | 16.11 |
| bench_gc_gen0 | 3.571 | 5 |
| bench_gc_gen1 | 1.898 | 4 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 776.057 | 1139.9 |
| bench_mem_mb | 1277.68 | 1485 |
| bench_threadpool_queue | 18.898 | 155 |
| bench_threadpool_threads | 35.653 | 41 |
| broker_backlog | 60120.857 | 108723 |
| ctr_eb_kafka_cpu_pct | 2.039 | 3.55 |
| ctr_eb_kafka_mem_mb | 1937.9 | 1937.9 |
| ctr_eb_mssql_cpu_pct | 1642.749 | 1844.27 |
| ctr_eb_mssql_mem_mb | 7375.831 | 7545.5 |
| ctr_eb_nats_cpu_pct | 0.584 | 0.66 |
| ctr_eb_nats_mem_mb | 360.6 | 360.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.3 | 10.3 |
| ctr_eb_postgres_cpu_pct | 0.244 | 0.38 |
| ctr_eb_postgres_mem_mb | 2193.5 | 2193.5 |
| ctr_eb_rabbitmq_cpu_pct | 16.609 | 17.62 |
| ctr_eb_rabbitmq_mem_mb | 629.041 | 633 |
| ctr_eb_redis_cpu_pct | 0.336 | 0.37 |
| ctr_eb_redis_mem_mb | 822.7 | 822.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 86.654 | 93.6 |
| in_flight | 58248.49 | 106473 |
| mssql_batch_requests_per_s | 27144.908 | 37398.7 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 83844 | 240148.1 |
| mssql_lock_waits_per_s | 2.557 | 25.9 |
| mssql_log_bytes_flushed_per_s | 157646780.998 | 240409843.8 |
| mssql_log_flush_waits_per_s | 110.12 | 185.4 |
| mssql_log_flushes_per_s | 2708.194 | 4057.6 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 204.347 | 824.9 |
| mssql_transactions_per_s | 16309.78 | 21513.4 |
| mssql_user_connections | 451.816 | 604 |
| mssql_wait_async_network_io_ms_per_s | 51.698 | 147.9 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 101.339 | 1624.5 |
| mssql_wait_lck_m_u_ms_per_s | 0.727 | 15.5 |
| mssql_wait_lck_m_x_ms_per_s | 0.424 | 10.4 |
| mssql_wait_logbuffer_ms_per_s | 110.588 | 1579.8 |
| mssql_wait_pageiolatch_sh_ms_per_s | 1636.571 | 7765.4 |
| mssql_wait_pagelatch_ex_ms_per_s | 8052.569 | 17939.3 |
| mssql_wait_pagelatch_sh_ms_per_s | 29530.49 | 223078.7 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 9848.947 | 22500 |
| mssql_wait_threadpool_ms_per_s | 0.939 | 16.9 |
| mssql_wait_writelog_ms_per_s | 266.051 | 411.3 |
| mssqlq_batch_requests_per_s | 27120.841 | 37021.9 |
| mssqlq_deadlocks_per_s | 0 | 0 |
| mssqlq_lock_timeouts_per_s | 83115.561 | 238524.1 |
| mssqlq_lock_waits_per_s | 2.502 | 26 |
| mssqlq_log_bytes_flushed_per_s | 157575811.32 | 239345912.7 |
| mssqlq_log_flush_waits_per_s | 110.549 | 190 |
| mssqlq_log_flushes_per_s | 2707.2 | 4036.1 |
| mssqlq_processes_blocked | 0 | 0 |
| mssqlq_sql_compilations_per_s | 204.853 | 813.7 |
| mssqlq_transactions_per_s | 16300.494 | 21341.9 |
| mssqlq_user_connections | 451.531 | 604 |
| mssqlq_wait_async_network_io_ms_per_s | 52.367 | 147.3 |
| mssqlq_wait_cxpacket_ms_per_s | 0 | 0 |
| mssqlq_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssqlq_wait_lck_m_s_ms_per_s | 101.386 | 1632.2 |
| mssqlq_wait_lck_m_u_ms_per_s | 0.722 | 15.3 |
| mssqlq_wait_lck_m_x_ms_per_s | 0.147 | 3.6 |
| mssqlq_wait_logbuffer_ms_per_s | 107.533 | 1573.8 |
| mssqlq_wait_pageiolatch_sh_ms_per_s | 1628.843 | 7929.9 |
| mssqlq_wait_pagelatch_ex_ms_per_s | 8135.29 | 17996 |
| mssqlq_wait_pagelatch_sh_ms_per_s | 29973.033 | 233536.4 |
| mssqlq_wait_sos_scheduler_yield_ms_per_s | 9810.512 | 21504.9 |
| mssqlq_wait_threadpool_ms_per_s | 0.906 | 15.9 |
| mssqlq_wait_writelog_ms_per_s | 266.306 | 412.9 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12413.384 | 23965.5 |
| produced_total | 448446.694 | 727602 |
| publish_mb_per_s | 44.038 | 95.08 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 113,570 records are Completed |
| record_counts | pass | 113,570 records found, 113,570 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 415 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 779,698 events applied by workers, 779,698 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8775 | 8775 | 0 | 29884.415 | 62128.127 | 64749.567 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11646 | 11646 | 0 | 24772.607 | 58982.399 | 62390.271 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 15418 | 15418 | 0 | 21233.663 | 56885.247 | 60293.119 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 10024 | 10024 | 0 | 18219.007 | 52166.655 | 55574.527 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 11748 | 11748 | 0 | 22151.167 | 48234.495 | 51118.079 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16029 | 16029 | 0 | 15400.959 | 44564.479 | 47448.063 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5796 | 5796 | 0 | 23068.671 | 42729.471 | 45088.767 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5437 | 5437 | 0 | 23330.815 | 42729.471 | 45088.767 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13155 | 13155 | 0 | 12124.159 | 39583.743 | 42467.327 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2766 | 2766 | 0 | 27131.903 | 39583.743 | 41943.039 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8269 | 8269 | 0 | 15400.959 | 39583.743 | 42205.183 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6029 | 6029 | 0 | 15007.743 | 37486.591 | 40108.031 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2874 | 2874 | 0 | 23461.887 | 35389.439 | 37486.591 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8044 | 8044 | 0 | 11534.335 | 34340.863 | 37224.447 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2772 | 2772 | 0 | 18612.223 | 33816.575 | 35389.439 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8443 | 8443 | 0 | 16646.143 | 32636.927 | 35127.295 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2915 | 2915 | 0 | 18874.367 | 30932.991 | 33161.215 |
| t-438 | 121 | 4 | 74.684 | 156.372 | 1839 | 0 | 9251 | 9251 | 0 | 10027.007 | 30932.991 | 34078.719 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2764 | 2764 | 0 | 12713.983 | 29753.343 | 31326.207 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5660 | 5660 | 0 | 18743.295 | 29097.983 | 31326.207 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5667 | 5667 | 0 | 15400.959 | 28573.695 | 30801.919 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4746 | 4746 | 0 | 12910.591 | 28311.551 | 30015.487 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7344 | 7344 | 0 | 10092.543 | 27525.119 | 29753.343 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6063 | 6063 | 0 | 14614.527 | 27131.903 | 28966.911 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5269 | 5269 | 0 | 17956.863 | 27131.903 | 29097.983 |
