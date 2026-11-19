# 07-rabbit_mssql — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.119s, closing 1.432s, drain 4.358s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814051 | 814051 | 13577.678 | 13492.151 | 24868.3 | 0 | 0 | 0 | 430 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642333 | 2746.003 | 3035.135 | 5038.079 | 6365.183 | 7290.879 | 8773.631 |
| e2e_from_send | 642333 | 1032.098 | 1009.663 | 2091.007 | 3129.343 | 3481.599 | 3944.447 |
| producer_lag | 642333 | 1713.905 | 1638.399 | 3753.983 | 4853.759 | 5763.071 | 6443.007 |
| publish_call | 30746 | 63.544 | 9.327 | 48.607 | 1566.719 | 2162.687 | 3002.367 |
| queue_wait | 642333 | 995.608 | 973.311 | 2032.639 | 3094.527 | 3454.975 | 3893.247 |
| broker_dwell | 642333 | 954.886 | 909.823 | 2025.471 | 3088.383 | 3448.831 | 3864.575 |
| receive_call | 440338 | 39.689 | 23.055 | 108.671 | 186.751 | 200.831 | 271.359 |
| ctx_load | 642333 | 8.149 | 1.818 | 6.435 | 133.119 | 850.431 | 1465.343 |
| process | 642333 | 0.003 | 0.001 | 0.002 | 0.005 | 0.129 | 31.743 |
| ctx_commit | 642333 | 3.129 | 1.849 | 6.167 | 15.815 | 70.463 | 1448.959 |
| ack | 636780 | 0.003 | 0.002 | 0.003 | 0.006 | 0.129 | 16.927 |
| service | 642333 | 36.492 | 8.055 | 70.911 | 672.255 | 1325.055 | 2209.791 |
| e2e_large | 330 | 2574.548 | 2856.959 | 4841.471 | 5627.903 | 7131.135 | 7131.135 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13492.151 | 24868.3 |
| applied_total | 433044.612 | 777396 |
| bench_cpu_cores | 2.9 | 4.268 |
| bench_cpu_pct | 12.084 | 17.78 |
| bench_gc_gen0 | 4.388 | 9 |
| bench_gc_gen1 | 2.408 | 6 |
| bench_gc_gen2 | 1.918 | 5 |
| bench_gc_heap_mb | 289.359 | 494.6 |
| bench_mem_mb | 608.733 | 797.2 |
| bench_threadpool_queue | 14.918 | 309 |
| bench_threadpool_threads | 31.776 | 33 |
| broker_backlog | 8253.51 | 40566 |
| broker_consumers | 598 | 598 |
| broker_ready | 5103.755 | 27548 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 3149.755 | 13018 |
| ctr_eb_kafka_cpu_pct | 1.816 | 2.35 |
| ctr_eb_kafka_mem_mb | 1887.596 | 1916.9 |
| ctr_eb_mssql_cpu_pct | 581.542 | 878.94 |
| ctr_eb_mssql_mem_mb | 7406.514 | 7411 |
| ctr_eb_nats_cpu_pct | 0.353 | 0.55 |
| ctr_eb_nats_mem_mb | 360.427 | 361 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.684 | 10.9 |
| ctr_eb_postgres_cpu_pct | 0.279 | 0.53 |
| ctr_eb_postgres_mem_mb | 3194.773 | 3206.3 |
| ctr_eb_rabbitmq_cpu_pct | 468.149 | 557.82 |
| ctr_eb_rabbitmq_mem_mb | 2029.678 | 2382.3 |
| ctr_eb_redis_cpu_pct | 0.273 | 0.3 |
| ctr_eb_redis_mem_mb | 1348.5 | 1348.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 60.097 | 80.21 |
| in_flight | 2531.816 | 43370 |
| mssql_batch_requests_per_s | 26869.139 | 42021.3 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 6.304 | 10.9 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 49676511.543 | 97545349.6 |
| mssql_log_flush_waits_per_s | 2.527 | 56.2 |
| mssql_log_flushes_per_s | 958.557 | 1743.5 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0.167 | 4.1 |
| mssql_transactions_per_s | 13436.769 | 21009.1 |
| mssql_user_connections | 203.367 | 308 |
| mssql_wait_async_network_io_ms_per_s | 1.188 | 9.1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.02 | 0.5 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 20.133 | 289.2 |
| mssql_wait_pagelatch_ex_ms_per_s | 37.398 | 333.6 |
| mssql_wait_pagelatch_sh_ms_per_s | 78.471 | 1097.3 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 48.62 | 277.9 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 1.118 | 23.5 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 13577.678 | 38007.1 |
| produced_total | 435576.429 | 777481 |
| publish_mb_per_s | 47.508 | 129.88 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,529 records are Completed |
| record_counts | pass | 117,529 records found, 117,529 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,051 events applied by workers, 814,051 generated |
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
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 3178.495 | 7340.031 | 8781.823 |
| t-128 | 11 | 1 | 101.581 | 18.855 | 260 | 0 | 1146 | 1146 | 0 | 3276.799 | 7077.887 | 7864.319 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6272 | 6272 | 0 | 3293.183 | 7077.887 | 8159.231 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5699 | 5699 | 0 | 3309.567 | 7045.119 | 7995.391 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12117 | 12117 | 0 | 3375.103 | 6979.583 | 8290.303 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4968 | 4968 | 0 | 3325.951 | 6979.583 | 7831.551 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8782 | 8782 | 0 | 3424.255 | 6946.815 | 8323.071 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2906 | 2906 | 0 | 3358.719 | 6881.279 | 7962.623 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2678 | 2678 | 0 | 3244.031 | 6848.511 | 7634.943 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5304 | 5304 | 0 | 3375.103 | 6848.511 | 7995.391 |
| t-375 | 6 | 1 | 74.947 | 7.146 | 275 | 0 | 434 | 434 | 0 | 2850.815 | 6848.511 | 6946.815 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2535 | 2535 | 0 | 3244.031 | 6815.743 | 8159.231 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6074 | 6074 | 0 | 3325.951 | 6815.743 | 7962.623 |
| t-258 | 52 | 1 | 41.292 | 35.498 | 2470 | 0 | 2371 | 2371 | 0 | 3112.959 | 6815.743 | 7929.855 |
| t-265 | 75 | 1 | 30.187 | 37.639 | 250 | 0 | 2368 | 2368 | 0 | 3194.879 | 6815.743 | 7602.175 |
| t-362 | 47 | 1 | 47.764 | 36.599 | 3235 | 0 | 2327 | 2327 | 0 | 3375.103 | 6815.743 | 8060.927 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2869 | 2869 | 0 | 3342.335 | 6782.975 | 7667.711 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 2507 | 2507 | 0 | 3293.183 | 6750.207 | 8126.463 |
| t-293 | 46 | 1 | 42.231 | 32.409 | 2741 | 0 | 2156 | 2156 | 0 | 3227.647 | 6750.207 | 7569.407 |
| t-494 | 35 | 1 | 33.067 | 18.747 | 2036 | 0 | 1177 | 1177 | 0 | 2621.439 | 6750.207 | 8454.143 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12241 | 12241 | 0 | 3325.951 | 6750.207 | 8290.303 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9179 | 9179 | 0 | 3260.415 | 6717.439 | 8028.159 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2737 | 2737 | 0 | 3358.719 | 6717.439 | 7438.335 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16747 | 16747 | 0 | 3276.799 | 6684.671 | 8224.767 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8384 | 8384 | 0 | 3276.799 | 6684.671 | 7864.319 |
