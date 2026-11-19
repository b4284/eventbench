# 01-pgmq_pg-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.263s, closing 0.41s, drain 7.214s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814417 | 814417 | 12854.237 | 11611.384 | 18970.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642705 | 5471.313 | 3661.823 | 14245.887 | 20070.399 | 21889.023 | 22446.079 |
| e2e_from_send | 642705 | 5459.275 | 3649.535 | 14229.503 | 20054.015 | 21889.023 | 22396.927 |
| producer_lag | 642705 | 12.035 | 8.199 | 17.359 | 138.367 | 266.239 | 412.671 |
| publish_call | 112126 | 17.161 | 15.183 | 21.455 | 43.455 | 261.247 | 467.967 |
| queue_wait | 642705 | 5362.386 | 3500.031 | 14123.007 | 19939.327 | 21774.335 | 22364.159 |
| broker_dwell | 642705 | 5352.173 | 3489.791 | 14114.815 | 19922.943 | 21774.335 | 22364.159 |
| receive_call | 234306 | 15.212 | 14.183 | 19.951 | 38.655 | 151.935 | 313.599 |
| ctx_load | 642705 | 12.532 | 12.767 | 17.151 | 30.639 | 46.783 | 69.183 |
| process | 642705 | 0.003 | 0.001 | 0.003 | 0.005 | 0.076 | 36.671 |
| ctx_commit | 642705 | 12.844 | 13.039 | 17.551 | 31.007 | 49.343 | 255.487 |
| service | 642705 | 96.891 | 60.991 | 232.319 | 314.367 | 373.247 | 620.543 |
| e2e_large | 331 | 442.752 | 425.983 | 674.303 | 934.399 | 1089.535 | 1089.535 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11611.384 | 12971.2 |
| applied_total | 403338.469 | 682275 |
| bench_cpu_cores | 3.097 | 3.729 |
| bench_cpu_pct | 12.906 | 15.54 |
| bench_gc_gen0 | 3.857 | 6 |
| bench_gc_gen1 | 3.408 | 6 |
| bench_gc_gen2 | 2.041 | 4 |
| bench_gc_heap_mb | 330.522 | 516 |
| bench_mem_mb | 588.627 | 827.6 |
| bench_threadpool_queue | 3.714 | 36 |
| bench_threadpool_threads | 37.102 | 44 |
| broker_backlog | 63185.163 | 95164 |
| broker_oldest_age_s | 13.429 | 22 |
| broker_visible | 61266.633 | 92837 |
| ctr_eb_kafka_cpu_pct | 1.902 | 2.21 |
| ctr_eb_kafka_mem_mb | 1895.68 | 2019.9 |
| ctr_eb_mongo_cpu_pct | 1.857 | 9.97 |
| ctr_eb_mongo_mem_mb | 3455.49 | 3572.5 |
| ctr_eb_nats_cpu_pct | 5.189 | 19.57 |
| ctr_eb_nats_mem_mb | 8077.4 | 8270.3 |
| ctr_eb_pgbouncer_cpu_pct | 94.772 | 96.58 |
| ctr_eb_pgbouncer_mem_mb | 19.643 | 22 |
| ctr_eb_postgres_cpu_pct | 1365.408 | 1476.58 |
| ctr_eb_postgres_mem_mb | 4484.135 | 4549 |
| ctr_eb_rabbitmq_cpu_pct | 15.083 | 15.78 |
| ctr_eb_rabbitmq_mem_mb | 615.953 | 656.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 80.342 | 86.77 |
| in_flight | 65217.347 | 95167 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.857 | 29 |
| pg_conn_idle | 48.612 | 61 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.469 | 66 |
| pg_db_size_mb | 1886.112 | 2334.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 35675.955 | 41576 |
| pg_tup_del_per_s | 34216.08 | 40975.4 |
| pg_tup_fetch_per_s | 329972.773 | 498767.6 |
| pg_tup_ins_per_s | 47446.339 | 65903 |
| pg_tup_upd_per_s | 30591.463 | 34082.3 |
| pg_wal_mb_per_s | 104.739 | 193.17 |
| pgb_cl_active | 935.163 | 1191 |
| pgb_cl_waiting | 255.837 | 401 |
| pgb_maxwait_ms | 7.627 | 13.86 |
| pgb_sv_active | 60.571 | 64 |
| pgb_sv_idle | 3.429 | 56 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.237 | 13375.6 |
| produced_total | 468555.816 | 777442 |
| publish_mb_per_s | 44.331 | 67.89 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,571 records are Completed |
| record_counts | pass | 117,571 records found, 117,571 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,417 events applied by workers, 814,417 generated |
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
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 15204.351 | 22282.239 | 22544.383 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12250 | 12250 | 0 | 15400.959 | 22151.167 | 22282.239 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 15400.959 | 22020.095 | 22282.239 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12126 | 12126 | 0 | 15138.815 | 21889.023 | 22151.167 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 14811.135 | 21364.735 | 21757.951 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 14680.063 | 20971.519 | 21102.591 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 14024.703 | 20840.447 | 21233.663 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 14090.239 | 20709.375 | 20840.447 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9181 | 9181 | 0 | 13959.167 | 20578.303 | 20840.447 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 13565.951 | 20054.015 | 20185.087 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 13565.951 | 19791.871 | 20316.159 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 13828.095 | 19660.799 | 19791.871 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 12976.127 | 19136.511 | 19267.583 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 13172.735 | 19005.439 | 19398.655 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 12779.519 | 18874.367 | 19267.583 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 13303.807 | 18874.367 | 19005.439 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 13041.663 | 18874.367 | 19005.439 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 12910.591 | 18219.007 | 18219.007 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16756 | 16756 | 0 | 12058.623 | 17956.863 | 18087.935 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13789 | 13789 | 0 | 12124.159 | 17825.791 | 18087.935 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 12320.767 | 17694.719 | 17956.863 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 12189.695 | 17694.719 | 17825.791 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 11993.087 | 17563.647 | 17825.791 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 11730.943 | 17301.503 | 17563.647 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 11468.799 | 17170.431 | 17301.503 |
