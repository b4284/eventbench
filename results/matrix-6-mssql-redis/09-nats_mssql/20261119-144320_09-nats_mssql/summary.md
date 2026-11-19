# 09-nats_mssql — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.123s, closing 1.417s, drain 0.304s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814531 | 814531 | 12856.788 | 12857.467 | 18779.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642810 | 22.737 | 19.583 | 36.255 | 83.071 | 137.087 | 252.415 |
| e2e_from_send | 642810 | 17.844 | 15.695 | 28.127 | 58.911 | 93.503 | 176.895 |
| producer_lag | 642810 | 4.893 | 3.363 | 10.103 | 29.679 | 66.559 | 139.007 |
| publish_call | 280213 | 6.3 | 5.119 | 11.463 | 26.399 | 70.079 | 995.327 |
| queue_wait | 642810 | 11.248 | 9.119 | 20.239 | 47.327 | 81.471 | 175.359 |
| broker_dwell | 642810 | 4.649 | 2.717 | 10.647 | 24.319 | 44.415 | 124.671 |
| receive_call | 564480 | 54.64 | 30.959 | 118.207 | 423.423 | 827.903 | 1126.399 |
| ctx_load | 642810 | 0.861 | 0.654 | 1.551 | 3.901 | 7.355 | 80.767 |
| process | 642810 | 0.002 | 0.001 | 0.002 | 0.004 | 0.042 | 6.291 |
| ctx_commit | 642810 | 0.915 | 0.7 | 1.585 | 3.961 | 9.151 | 56.863 |
| ack | 641484 | 0.016 | 0.003 | 0.005 | 0.078 | 3.401 | 11.975 |
| service | 642810 | 6.596 | 5.667 | 12.679 | 22.639 | 40.063 | 103.807 |
| e2e_large | 331 | 64.46 | 60.223 | 98.367 | 152.703 | 183.551 | 183.551 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12857.467 | 14017.2 |
| applied_total | 468641.143 | 777551 |
| bench_cpu_cores | 3.841 | 4.064 |
| bench_cpu_pct | 16.004 | 16.93 |
| bench_gc_gen0 | 3.673 | 5 |
| bench_gc_gen1 | 2.796 | 5 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 794.414 | 1056.6 |
| bench_mem_mb | 1247.367 | 1362.7 |
| bench_threadpool_queue | 10.02 | 73 |
| bench_threadpool_threads | 35.082 | 44 |
| broker_backlog | 214.163 | 687 |
| broker_backlog_mb | 0.812 | 4.953 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.785 | 2.29 |
| ctr_eb_kafka_mem_mb | 3224.371 | 3398.9 |
| ctr_eb_mssql_cpu_pct | 613.949 | 719.43 |
| ctr_eb_mssql_mem_mb | 5787.555 | 5841.3 |
| ctr_eb_nats_cpu_pct | 395.106 | 408.9 |
| ctr_eb_nats_mem_mb | 2740.986 | 3013.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.1 | 10.1 |
| ctr_eb_postgres_cpu_pct | 0.413 | 0.55 |
| ctr_eb_postgres_mem_mb | 3068.416 | 3079.4 |
| ctr_eb_rabbitmq_cpu_pct | 14.837 | 15.86 |
| ctr_eb_rabbitmq_mem_mb | 835.978 | 842.5 |
| ctr_eb_redis_cpu_pct | 0.282 | 0.3 |
| ctr_eb_redis_mem_mb | 1348.5 | 1348.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 63.89 | 68.45 |
| in_flight | 103.163 | 175 |
| mssql_batch_requests_per_s | 25808.51 | 30743.7 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 4.72 | 12.1 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 48178780.382 | 72140403.1 |
| mssql_log_flush_waits_per_s | 0.414 | 2 |
| mssql_log_flushes_per_s | 990.498 | 1368.2 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 12904.214 | 15380.3 |
| mssql_user_connections | 144.939 | 145 |
| mssql_wait_async_network_io_ms_per_s | 0.092 | 0.5 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0.061 | 1.5 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0.02 | 0.5 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 12.755 | 38.2 |
| mssql_wait_pagelatch_ex_ms_per_s | 17.014 | 46.5 |
| mssql_wait_pagelatch_sh_ms_per_s | 15.718 | 34 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 6.624 | 14.7 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.255 | 1.5 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12856.788 | 14109.2 |
| produced_total | 468744.306 | 777632 |
| publish_mb_per_s | 44.339 | 71.39 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,582 records are Completed |
| record_counts | pass | 117,582 records found, 117,582 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,531 events applied by workers, 814,531 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 53.247 | 153.599 | 184.319 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 44.543 | 146.431 | 148.479 |
| t-440 | 6 | 1 | 19.794 | 2.033 | 154 | 0 | 135 | 135 | 0 | 19.967 | 125.951 | 192.511 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 51.455 | 125.439 | 125.439 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 41.983 | 119.295 | 148.479 |
| t-095 | 6 | 1 | 54.646 | 5.516 | 925 | 0 | 308 | 308 | 0 | 18.303 | 116.735 | 172.031 |
| t-279 | 18 | 1 | 35.495 | 10.479 | 196 | 0 | 667 | 667 | 0 | 19.327 | 115.199 | 148.479 |
| t-229 | 28 | 1 | 23.904 | 11.164 | 1675 | 0 | 754 | 754 | 0 | 20.095 | 109.055 | 146.431 |
| t-301 | 26 | 1 | 44.546 | 19.992 | 3339 | 0 | 1237 | 1237 | 0 | 19.711 | 109.055 | 140.287 |
| t-424 | 8 | 1 | 76.354 | 10.019 | 1700 | 0 | 599 | 599 | 0 | 19.967 | 108.543 | 127.487 |
| t-021 | 19 | 1 | 16.257 | 5.407 | 3252 | 0 | 346 | 346 | 0 | 20.223 | 108.031 | 124.927 |
| t-356 | 6 | 1 | 85.291 | 8.355 | 303 | 0 | 527 | 527 | 0 | 18.431 | 108.031 | 212.991 |
| t-433 | 6 | 1 | 21.23 | 2.112 | 184 | 0 | 132 | 132 | 0 | 21.119 | 106.495 | 138.239 |
| t-404 | 17 | 1 | 22.748 | 5.962 | 1799 | 0 | 401 | 401 | 0 | 18.175 | 105.983 | 148.479 |
| t-366 | 55 | 1 | 17.464 | 16.244 | 2410 | 0 | 1102 | 1102 | 0 | 20.223 | 105.471 | 196.607 |
| t-097 | 12 | 1 | 56.648 | 11.402 | 1865 | 0 | 702 | 702 | 0 | 19.967 | 104.959 | 133.119 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 17.663 | 104.959 | 120.319 |
| t-345 | 13 | 1 | 16.657 | 3.763 | 1052 | 0 | 278 | 278 | 0 | 18.815 | 104.959 | 121.855 |
| t-031 | 11 | 1 | 69.313 | 12.621 | 463 | 0 | 781 | 781 | 0 | 19.711 | 104.447 | 135.167 |
| t-033 | 6 | 1 | 70.419 | 7.677 | 859 | 0 | 505 | 505 | 0 | 22.143 | 104.447 | 145.407 |
| t-115 | 10 | 1 | 33.32 | 5.329 | 3658 | 0 | 383 | 383 | 0 | 20.351 | 104.447 | 149.503 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 21.887 | 104.447 | 110.079 |
| t-420 | 13 | 1 | 49.618 | 10.967 | 270 | 0 | 712 | 712 | 0 | 20.095 | 104.447 | 163.839 |
| t-476 | 15 | 1 | 38.366 | 10.704 | 432 | 0 | 706 | 706 | 0 | 19.071 | 104.447 | 160.767 |
| t-190 | 6 | 1 | 69.297 | 7.234 | 241 | 0 | 436 | 436 | 0 | 20.095 | 103.423 | 172.031 |
