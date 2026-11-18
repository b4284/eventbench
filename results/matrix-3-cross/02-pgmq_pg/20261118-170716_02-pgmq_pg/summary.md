# 02-pgmq_pg — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.22s, closing 0.285s, drain 32.286s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814461 | 814461 | 12854.573 | 7737.945 | 12054.5 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642743 | 22095.506 | 24313.855 | 39452.671 | 43384.831 | 44171.263 | 44433.407 |
| e2e_from_send | 642743 | 22082.677 | 24313.855 | 39419.903 | 43384.831 | 44138.495 | 44433.407 |
| producer_lag | 642743 | 12.823 | 10.543 | 20.719 | 92.543 | 173.311 | 268.287 |
| publish_call | 88511 | 21.715 | 19.711 | 27.039 | 55.199 | 169.215 | 292.095 |
| queue_wait | 642743 | 21909.676 | 24084.479 | 39321.599 | 43286.527 | 44040.191 | 44400.639 |
| broker_dwell | 642743 | 21892.397 | 24068.095 | 39288.831 | 43253.759 | 44040.191 | 44400.639 |
| receive_call | 152760 | 18.537 | 17.887 | 24.559 | 46.687 | 186.495 | 1084.415 |
| ctx_load | 642743 | 11.553 | 11.975 | 19.087 | 33.375 | 53.311 | 188.287 |
| process | 642743 | 0.002 | 0.001 | 0.003 | 0.005 | 0.039 | 16.751 |
| ctx_commit | 642743 | 13.178 | 13.399 | 21.151 | 37.119 | 60.255 | 193.791 |
| ack | 642743 | 13.204 | 13.455 | 21.135 | 36.415 | 58.623 | 194.303 |
| service | 642743 | 173.009 | 120.255 | 418.047 | 624.639 | 874.495 | 1026.559 |
| e2e_large | 331 | 307.605 | 299.007 | 417.023 | 609.791 | 723.967 | 723.967 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7737.945 | 9008.7 |
| applied_total | 272899.245 | 453593 |
| bench_cpu_cores | 2.542 | 3.001 |
| bench_cpu_pct | 10.593 | 12.51 |
| bench_gc_gen0 | 1.898 | 4 |
| bench_gc_gen1 | 1.531 | 3 |
| bench_gc_gen2 | 0.429 | 2 |
| bench_gc_heap_mb | 1033.698 | 1420.2 |
| bench_mem_mb | 1284.514 | 1443.7 |
| bench_threadpool_queue | 0.918 | 15 |
| bench_threadpool_threads | 29.653 | 36 |
| broker_backlog | 188810.816 | 317891 |
| broker_oldest_age_s | 23.571 | 40 |
| broker_visible | 186162.796 | 314963 |
| ctr_eb_kafka_cpu_pct | 1.884 | 2.52 |
| ctr_eb_kafka_mem_mb | 2250.38 | 2333.6 |
| ctr_eb_mongo_cpu_pct | 1.155 | 2.41 |
| ctr_eb_mongo_mem_mb | 2423.167 | 2436.6 |
| ctr_eb_nats_cpu_pct | 1.322 | 9.85 |
| ctr_eb_nats_mem_mb | 3248.731 | 3860.5 |
| ctr_eb_pgbouncer_cpu_pct | 90.84 | 94.75 |
| ctr_eb_pgbouncer_mem_mb | 18.257 | 25.6 |
| ctr_eb_postgres_cpu_pct | 1063.844 | 1156.66 |
| ctr_eb_postgres_mem_mb | 4821.29 | 4869.5 |
| ctr_eb_rabbitmq_cpu_pct | 13.349 | 14.44 |
| ctr_eb_rabbitmq_mem_mb | 719.027 | 726.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 64.452 | 69.18 |
| in_flight | 195662.653 | 323768 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.959 | 100 |
| pg_conn_active | 38.633 | 65 |
| pg_conn_idle | 26.755 | 49 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.388 | 66 |
| pg_db_size_mb | 2091.529 | 2970.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 30241.733 | 36483.1 |
| pg_tup_del_per_s | 26629.114 | 37388.1 |
| pg_tup_fetch_per_s | 265880.543 | 432903.4 |
| pg_tup_ins_per_s | 46935.4 | 68006.5 |
| pg_tup_upd_per_s | 20473.69 | 24221.1 |
| pg_wal_mb_per_s | 92.923 | 169.54 |
| pgb_cl_active | 548.857 | 736 |
| pgb_cl_waiting | 458.143 | 539 |
| pgb_maxwait_ms | 19.001 | 37.68 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.573 | 13244 |
| produced_total | 468561.898 | 777361 |
| publish_mb_per_s | 44.338 | 71.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,577 records are Completed |
| record_counts | pass | 117,577 records found, 117,577 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,461 events applied by workers, 814,461 generated |
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
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 41680.895 | 44302.335 | 44302.335 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12252 | 12252 | 0 | 41418.751 | 44302.335 | 44564.479 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 40894.463 | 44040.191 | 44302.335 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 41418.751 | 44040.191 | 44302.335 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12128 | 12128 | 0 | 41418.751 | 43515.903 | 44040.191 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 40370.175 | 43515.903 | 43778.047 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 40632.319 | 42991.615 | 42991.615 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3008 | 3008 | 0 | 40370.175 | 42991.615 | 43253.759 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 39583.743 | 42729.471 | 42729.471 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 39583.743 | 42729.471 | 42729.471 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 40108.031 | 42467.327 | 42729.471 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 39583.743 | 41943.039 | 42205.183 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 39321.599 | 41680.895 | 41943.039 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 39321.599 | 41680.895 | 42205.183 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 38535.167 | 41680.895 | 42205.183 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 39059.455 | 41680.895 | 41943.039 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2898 | 2898 | 0 | 38535.167 | 41680.895 | 41680.895 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 38797.311 | 41418.751 | 42205.183 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13789 | 13789 | 0 | 38010.879 | 40894.463 | 40894.463 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 37748.735 | 40894.463 | 41156.607 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 38273.023 | 40632.319 | 41156.607 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 38010.879 | 40632.319 | 40632.319 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16760 | 16760 | 0 | 37748.735 | 40370.175 | 40632.319 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 37224.447 | 40370.175 | 40632.319 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5490 | 5490 | 0 | 37748.735 | 40370.175 | 40632.319 |
