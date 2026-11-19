# 09-nats_mssql — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `sqlserver @ 127.0.0.1,1433/bench (pool max 1000), commit = proc with UPDLOCK cursor, delayed durability = server setting`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.124s, closing 1.246s, drain 0.506s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814525 | 814525 | 12855.222 | 12856.835 | 18783.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642808 | 21.051 | 19.103 | 33.919 | 55.103 | 78.271 | 138.111 |
| e2e_from_send | 642808 | 16.747 | 15.319 | 26.623 | 42.143 | 64.223 | 121.727 |
| producer_lag | 642808 | 4.305 | 3.203 | 9.263 | 20.031 | 33.823 | 68.031 |
| publish_call | 292283 | 5.955 | 4.935 | 11.031 | 22.623 | 53.375 | 946.687 |
| queue_wait | 642808 | 10.248 | 8.791 | 18.623 | 32.607 | 48.383 | 104.959 |
| broker_dwell | 642808 | 3.924 | 2.223 | 9.623 | 19.183 | 31.279 | 98.943 |
| receive_call | 567687 | 53.853 | 30.335 | 116.991 | 419.839 | 826.367 | 1095.679 |
| ctx_load | 642808 | 0.895 | 0.654 | 1.574 | 4.291 | 9.399 | 69.375 |
| process | 642808 | 0.002 | 0.001 | 0.002 | 0.004 | 0.045 | 7.447 |
| ctx_commit | 642808 | 0.93 | 0.699 | 1.613 | 4.223 | 10.343 | 86.655 |
| ack | 641419 | 0.018 | 0.002 | 0.005 | 0.083 | 3.805 | 14.407 |
| service | 642808 | 6.499 | 5.619 | 12.575 | 20.623 | 33.503 | 91.711 |
| e2e_large | 331 | 60.519 | 59.199 | 89.407 | 129.279 | 138.111 | 138.111 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12856.835 | 13302.9 |
| applied_total | 468649 | 777410 |
| bench_cpu_cores | 3.9 | 4.164 |
| bench_cpu_pct | 16.249 | 17.35 |
| bench_gc_gen0 | 4.143 | 5 |
| bench_gc_gen1 | 3.694 | 5 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 772.1 | 1169.5 |
| bench_mem_mb | 1200.631 | 1511.9 |
| bench_threadpool_queue | 12.041 | 61 |
| bench_threadpool_threads | 35.571 | 44 |
| broker_backlog | 203.51 | 323 |
| broker_backlog_mb | 1.222 | 10.195 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.765 | 2.2 |
| ctr_eb_kafka_mem_mb | 3121.849 | 3348.4 |
| ctr_eb_mssql_cpu_pct | 599.419 | 736.06 |
| ctr_eb_mssql_mem_mb | 6388.28 | 6444 |
| ctr_eb_nats_cpu_pct | 394.233 | 408.85 |
| ctr_eb_nats_mem_mb | 2719.104 | 3040.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.431 | 10.5 |
| ctr_eb_postgres_cpu_pct | 0.362 | 1.47 |
| ctr_eb_postgres_mem_mb | 3117.786 | 3135.7 |
| ctr_eb_rabbitmq_cpu_pct | 13.978 | 15.46 |
| ctr_eb_rabbitmq_mem_mb | 985.133 | 992.8 |
| ctr_eb_redis_cpu_pct | 0.282 | 0.3 |
| ctr_eb_redis_mem_mb | 1352.443 | 1353.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 63.697 | 65.62 |
| in_flight | 106.939 | 203 |
| mssql_batch_requests_per_s | 25849.333 | 32613.1 |
| mssql_deadlocks_per_s | 0 | 0 |
| mssql_lock_timeouts_per_s | 4.163 | 11.7 |
| mssql_lock_waits_per_s | 0 | 0 |
| mssql_log_bytes_flushed_per_s | 48203961.729 | 72918741.7 |
| mssql_log_flush_waits_per_s | 0.465 | 2.4 |
| mssql_log_flushes_per_s | 992.063 | 1373.3 |
| mssql_processes_blocked | 0 | 0 |
| mssql_sql_compilations_per_s | 0 | 0 |
| mssql_transactions_per_s | 12925.19 | 16316 |
| mssql_user_connections | 148.673 | 150 |
| mssql_wait_async_network_io_ms_per_s | 0.224 | 1 |
| mssql_wait_cxpacket_ms_per_s | 0 | 0 |
| mssql_wait_hadr_sync_commit_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_s_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_u_ms_per_s | 0 | 0 |
| mssql_wait_lck_m_x_ms_per_s | 0 | 0 |
| mssql_wait_logbuffer_ms_per_s | 0 | 0 |
| mssql_wait_pageiolatch_sh_ms_per_s | 11.716 | 28.8 |
| mssql_wait_pagelatch_ex_ms_per_s | 24.251 | 59.2 |
| mssql_wait_pagelatch_sh_ms_per_s | 21.92 | 152.4 |
| mssql_wait_sos_scheduler_yield_ms_per_s | 6.445 | 25.7 |
| mssql_wait_threadpool_ms_per_s | 0 | 0 |
| mssql_wait_writelog_ms_per_s | 0.437 | 2.4 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12855.222 | 13212.4 |
| produced_total | 468755.939 | 777509 |
| publish_mb_per_s | 44.334 | 71.49 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,580 records are Completed |
| record_counts | pass | 117,580 records found, 117,580 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,525 events applied by workers, 814,525 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 50.431 | 129.023 | 138.239 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 47.615 | 122.879 | 137.215 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 46.847 | 102.399 | 102.399 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 42.495 | 98.815 | 106.495 |
| t-410 | 6 | 1 | 31.268 | 3.051 | 195 | 0 | 188 | 188 | 0 | 20.479 | 75.263 | 78.335 |
| t-203 | 6 | 1 | 28.77 | 3.109 | 2258 | 0 | 180 | 180 | 0 | 17.791 | 74.751 | 75.263 |
| t-134 | 21 | 1 | 19.598 | 6.714 | 3368 | 0 | 453 | 453 | 0 | 20.991 | 71.679 | 85.503 |
| t-144 | 6 | 1 | 24.576 | 2.554 | 816 | 0 | 174 | 174 | 0 | 18.943 | 71.167 | 87.551 |
| t-407 | 30 | 1 | 28.601 | 13.807 | 222 | 0 | 907 | 907 | 0 | 20.607 | 68.607 | 84.479 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 19.839 | 68.095 | 79.359 |
| t-098 | 6 | 1 | 27.347 | 2.525 | 192 | 0 | 193 | 193 | 0 | 21.503 | 66.559 | 77.823 |
| t-412 | 6 | 1 | 28.435 | 2.632 | 468 | 0 | 164 | 164 | 0 | 18.303 | 65.535 | 69.631 |
| t-466 | 11 | 1 | 39.527 | 6.561 | 449 | 0 | 457 | 457 | 0 | 18.431 | 64.767 | 121.343 |
| t-335 | 6 | 1 | 28.977 | 2.726 | 450 | 0 | 179 | 179 | 0 | 17.535 | 64.255 | 66.559 |
| t-302 | 12 | 1 | 92.513 | 19.786 | 1189 | 0 | 1248 | 1248 | 0 | 21.119 | 63.487 | 97.791 |
| t-459 | 25 | 1 | 16.76 | 6.839 | 2174 | 0 | 467 | 467 | 0 | 19.071 | 63.231 | 91.647 |
| t-481 | 12 | 1 | 121.412 | 22.891 | 1993 | 0 | 1428 | 1428 | 0 | 20.735 | 63.231 | 88.063 |
| t-081 | 6 | 1 | 13.059 | 1.301 | 2170 | 0 | 94 | 94 | 0 | 18.431 | 62.975 | 87.551 |
| t-133 | 6 | 1 | 27.688 | 2.68 | 273 | 0 | 182 | 182 | 0 | 19.199 | 62.975 | 69.119 |
| t-411 | 15 | 1 | 50.636 | 11.611 | 2644 | 0 | 772 | 772 | 0 | 19.071 | 62.975 | 101.375 |
| t-030 | 19 | 1 | 24.985 | 8.002 | 748 | 0 | 511 | 511 | 0 | 18.303 | 62.719 | 117.759 |
| t-274 | 29 | 1 | 56.437 | 27.244 | 1181 | 0 | 1700 | 1700 | 0 | 20.351 | 62.207 | 98.815 |
| t-310 | 6 | 1 | 70.482 | 7.386 | 1910 | 0 | 474 | 474 | 0 | 19.071 | 62.207 | 72.703 |
| t-359 | 52 | 1 | 25.301 | 21.73 | 132 | 0 | 1363 | 1363 | 0 | 20.479 | 62.207 | 74.239 |
| t-059 | 8 | 1 | 63.812 | 8.437 | 1449 | 0 | 543 | 543 | 0 | 20.991 | 61.951 | 73.215 |
