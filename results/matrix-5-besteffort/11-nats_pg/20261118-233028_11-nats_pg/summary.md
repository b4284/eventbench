# 11-nats_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.109s, closing 0.948s, drain 0.71s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814598 | 814598 | 12853.135 | 12853.365 | 19671.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642875 | 7.69 | 6.251 | 13.511 | 29.391 | 45.247 | 116.863 |
| e2e_from_send | 642875 | 6.256 | 5.095 | 11.015 | 22.863 | 36.191 | 114.239 |
| producer_lag | 642875 | 1.434 | 0.954 | 3.197 | 8.671 | 18.735 | 46.815 |
| publish_call | 483736 | 2.379 | 1.684 | 4.411 | 11.551 | 39.071 | 634.367 |
| queue_wait | 642875 | 3.128 | 2.235 | 6.315 | 15.559 | 26.671 | 63.039 |
| broker_dwell | 642875 | 1.819 | 1.32 | 3.495 | 9.391 | 18.367 | 49.983 |
| receive_call | 706756 | 48.347 | 25.631 | 106.943 | 388.351 | 796.159 | 1069.055 |
| ctx_load | 642875 | 0.564 | 0.414 | 0.986 | 2.521 | 5.655 | 24.751 |
| process | 642875 | 0.002 | 0.001 | 0.002 | 0.004 | 0.029 | 6.867 |
| ctx_commit | 642875 | 0.584 | 0.439 | 0.974 | 2.517 | 6.819 | 44.543 |
| ack | 634346 | 0.015 | 0.002 | 0.005 | 0.12 | 3.081 | 8.839 |
| service | 642875 | 3.128 | 2.541 | 5.679 | 11.567 | 22.351 | 59.967 |
| e2e_large | 331 | 39.214 | 38.175 | 58.047 | 83.647 | 116.863 | 116.863 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12853.365 | 13180.2 |
| applied_total | 468857.551 | 777674 |
| bench_cpu_cores | 5.009 | 5.455 |
| bench_cpu_pct | 20.871 | 22.73 |
| bench_gc_gen0 | 2.98 | 4 |
| bench_gc_gen1 | 2.082 | 4 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 785.049 | 969.2 |
| bench_mem_mb | 1197.039 | 1281.5 |
| bench_threadpool_queue | 12.122 | 126 |
| bench_threadpool_threads | 34.224 | 44 |
| broker_backlog | 79.714 | 146 |
| broker_backlog_mb | 1.573 | 7.201 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.78 | 2.17 |
| ctr_eb_kafka_mem_mb | 2484.196 | 2693.2 |
| ctr_eb_mongo_cpu_pct | 1.126 | 2.12 |
| ctr_eb_mongo_mem_mb | 3974.847 | 4097 |
| ctr_eb_nats_cpu_pct | 416.844 | 429.44 |
| ctr_eb_nats_mem_mb | 3681.812 | 6443 |
| ctr_eb_pgbouncer_cpu_pct | 61.169 | 62.25 |
| ctr_eb_pgbouncer_mem_mb | 18.463 | 19.7 |
| ctr_eb_postgres_cpu_pct | 268.462 | 282.2 |
| ctr_eb_postgres_mem_mb | 4718.143 | 4759.8 |
| ctr_eb_rabbitmq_cpu_pct | 12.885 | 13.79 |
| ctr_eb_rabbitmq_mem_mb | 725.518 | 737 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 58.758 | 60.1 |
| in_flight | 39.51 | 93 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 1.224 | 4 |
| pg_conn_idle | 62.776 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 1316.745 | 1740.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25674.355 | 28724.9 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 58833.682 | 65276.6 |
| pg_tup_ins_per_s | 10792.573 | 19941.2 |
| pg_tup_upd_per_s | 21206.692 | 23829.1 |
| pg_wal_mb_per_s | 33.127 | 52.25 |
| pgb_cl_active | 450 | 450 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.98 | 14 |
| pgb_sv_idle | 59.02 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.135 | 13105.2 |
| produced_total | 468897.061 | 777727 |
| publish_mb_per_s | 44.331 | 71.15 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,589 records are Completed |
| record_counts | pass | 117,589 records found, 117,589 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,598 events applied by workers, 814,598 generated |
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
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 31.871 | 82.943 | 95.231 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 30.079 | 80.383 | 80.383 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 22.271 | 67.583 | 117.247 |
| t-319L | 29 | 1 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 25.599 | 66.047 | 70.655 |
| t-133 | 6 | 1 | 27.688 | 2.68 | 273 | 0 | 182 | 182 | 0 | 5.919 | 46.591 | 58.623 |
| t-277 | 13 | 1 | 28.997 | 6.366 | 2307 | 0 | 413 | 413 | 0 | 6.047 | 38.911 | 56.319 |
| t-144 | 6 | 1 | 24.576 | 2.554 | 816 | 0 | 174 | 174 | 0 | 6.271 | 37.887 | 42.239 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 6.047 | 37.375 | 43.519 |
| t-087 | 6 | 1 | 34.936 | 3.77 | 1614 | 0 | 212 | 212 | 0 | 6.015 | 36.095 | 44.799 |
| t-216 | 19 | 1 | 60.988 | 19.724 | 3889 | 0 | 1234 | 1234 | 0 | 6.815 | 36.095 | 54.527 |
| t-219 | 13 | 1 | 32.759 | 6.924 | 272 | 0 | 443 | 443 | 0 | 6.143 | 35.839 | 58.367 |
| t-191 | 11 | 1 | 38.204 | 6.794 | 3529 | 0 | 442 | 442 | 0 | 5.759 | 35.583 | 48.127 |
| t-229 | 28 | 1 | 23.904 | 11.164 | 1675 | 0 | 754 | 754 | 0 | 6.239 | 35.327 | 45.311 |
| t-143 | 21 | 1 | 23.607 | 8.455 | 1094 | 0 | 572 | 572 | 0 | 6.239 | 35.071 | 47.615 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 335 | 335 | 0 | 6.111 | 35.071 | 36.863 |
| t-225 | 6 | 1 | 31.888 | 3.143 | 3030 | 0 | 186 | 186 | 0 | 6.431 | 34.559 | 55.039 |
| t-317 | 32 | 1 | 39.571 | 21.604 | 1288 | 0 | 1345 | 1345 | 0 | 6.751 | 34.559 | 46.335 |
| t-377 | 17 | 1 | 41.297 | 11.162 | 200 | 0 | 739 | 739 | 0 | 6.463 | 34.559 | 40.703 |
| t-439 | 22 | 1 | 68.218 | 25.438 | 3339 | 0 | 1624 | 1624 | 0 | 6.527 | 34.559 | 54.783 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 6.079 | 34.303 | 56.319 |
| t-294 | 7 | 1 | 107.858 | 12.447 | 667 | 0 | 765 | 765 | 0 | 6.335 | 34.047 | 56.575 |
| t-029 | 28 | 2 | 143.274 | 66.438 | 1732 | 0 | 4238 | 4238 | 0 | 6.335 | 33.791 | 58.367 |
| t-114 | 14 | 1 | 48.994 | 11.296 | 1025 | 0 | 694 | 694 | 0 | 6.143 | 33.535 | 55.807 |
| t-423 | 11 | 1 | 36.622 | 6.606 | 726 | 0 | 441 | 441 | 0 | 6.047 | 33.535 | 53.503 |
| t-487 | 9 | 1 | 97.44 | 14.247 | 755 | 0 | 867 | 867 | 0 | 6.367 | 33.535 | 45.311 |
