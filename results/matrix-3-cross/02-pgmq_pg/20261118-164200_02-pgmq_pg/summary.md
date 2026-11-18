# 02-pgmq_pg — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.202s, closing 0.318s, drain 31.89s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814467 | 814467 | 12853.718 | 7889.792 | 11972.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642751 | 21709.403 | 23674.879 | 38993.919 | 43057.151 | 43843.583 | 44335.103 |
| e2e_from_send | 642751 | 21696.879 | 23658.495 | 38961.151 | 43024.383 | 43843.583 | 44302.335 |
| producer_lag | 642751 | 12.54 | 10.271 | 20.271 | 88.447 | 166.399 | 264.191 |
| publish_call | 90582 | 21.25 | 19.103 | 26.735 | 54.751 | 163.199 | 315.647 |
| queue_wait | 642751 | 21527.219 | 23429.119 | 38862.847 | 42926.079 | 43745.279 | 44302.335 |
| broker_dwell | 642751 | 21511.096 | 23429.119 | 38830.079 | 42926.079 | 43745.279 | 44269.567 |
| receive_call | 154082 | 17.564 | 17.391 | 23.919 | 49.599 | 102.655 | 320.511 |
| ctx_load | 642751 | 11.375 | 12.407 | 18.239 | 32.335 | 69.951 | 124.927 |
| process | 642751 | 0.002 | 0.001 | 0.002 | 0.005 | 0.036 | 17.999 |
| ctx_commit | 642751 | 12.969 | 13.831 | 20.191 | 35.615 | 89.663 | 141.439 |
| ack | 642751 | 12.993 | 13.895 | 20.191 | 35.007 | 85.055 | 127.615 |
| service | 642751 | 169.629 | 119.743 | 407.551 | 593.919 | 685.055 | 949.759 |
| e2e_large | 331 | 315.788 | 312.831 | 438.271 | 542.719 | 603.647 | 603.647 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7889.792 | 9212 |
| applied_total | 274773.755 | 461979 |
| bench_cpu_cores | 2.527 | 3.061 |
| bench_cpu_pct | 10.531 | 12.75 |
| bench_gc_gen0 | 1.939 | 4 |
| bench_gc_gen1 | 1.49 | 3 |
| bench_gc_gen2 | 0.449 | 2 |
| bench_gc_heap_mb | 990.904 | 1231 |
| bench_mem_mb | 1240.657 | 1412.8 |
| bench_threadpool_queue | 1.265 | 13 |
| bench_threadpool_threads | 24 | 24 |
| broker_backlog | 187120.204 | 310058 |
| broker_oldest_age_s | 23.51 | 39 |
| broker_visible | 184477.98 | 307443 |
| ctr_eb_kafka_cpu_pct | 2.672 | 3.92 |
| ctr_eb_kafka_mem_mb | 1751.133 | 1757.6 |
| ctr_eb_mongo_cpu_pct | 1.048 | 1.54 |
| ctr_eb_mongo_mem_mb | 1985.251 | 2003.7 |
| ctr_eb_nats_cpu_pct | 0.412 | 0.53 |
| ctr_eb_nats_mem_mb | 1015.467 | 1028.1 |
| ctr_eb_pgbouncer_cpu_pct | 91.568 | 96.19 |
| ctr_eb_pgbouncer_mem_mb | 24.788 | 33 |
| ctr_eb_postgres_cpu_pct | 1066.896 | 1166.35 |
| ctr_eb_postgres_mem_mb | 4887.722 | 4945.8 |
| ctr_eb_rabbitmq_cpu_pct | 12.929 | 13.65 |
| ctr_eb_rabbitmq_mem_mb | 1068.357 | 1072.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 64.64 | 70.29 |
| in_flight | 193793 | 315501 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.962 | 100 |
| pg_conn_active | 45.857 | 65 |
| pg_conn_idle | 19.592 | 51 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.449 | 66 |
| pg_db_size_mb | 2113.318 | 2771.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 30854.157 | 36536.8 |
| pg_tup_del_per_s | 26978.994 | 37649.3 |
| pg_tup_fetch_per_s | 278790.106 | 429595.4 |
| pg_tup_ins_per_s | 46873.865 | 66316 |
| pg_tup_upd_per_s | 20882.886 | 24687.6 |
| pg_wal_mb_per_s | 94.189 | 140.48 |
| pgb_cl_active | 636.551 | 751 |
| pgb_cl_waiting | 458.449 | 564 |
| pgb_maxwait_ms | 16.286 | 48.07 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.718 | 13298.9 |
| produced_total | 468566.755 | 777480 |
| publish_mb_per_s | 44.331 | 70.31 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,577 records are Completed |
| record_counts | pass | 117,577 records found, 117,577 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,467 events applied by workers, 814,467 generated |
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
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 41156.607 | 44040.191 | 44040.191 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12250 | 12250 | 0 | 40894.463 | 44040.191 | 44564.479 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 40370.175 | 43778.047 | 44040.191 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 40894.463 | 43778.047 | 44040.191 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12127 | 12127 | 0 | 40894.463 | 43253.759 | 43778.047 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 40108.031 | 43253.759 | 43515.903 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 40108.031 | 42467.327 | 42729.471 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 39845.887 | 42467.327 | 42991.615 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 39321.599 | 42205.183 | 42467.327 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 39583.743 | 42205.183 | 42467.327 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 39059.455 | 42205.183 | 42467.327 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 39059.455 | 41680.895 | 41680.895 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 38797.311 | 41418.751 | 41680.895 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 38797.311 | 41418.751 | 41943.039 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 38273.023 | 41418.751 | 41680.895 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 38535.167 | 41418.751 | 41680.895 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2898 | 2898 | 0 | 38010.879 | 41418.751 | 41418.751 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 38273.023 | 41156.607 | 41943.039 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 37224.447 | 40632.319 | 40632.319 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13789 | 13789 | 0 | 37486.591 | 40370.175 | 40632.319 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 37748.735 | 40370.175 | 40632.319 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 37486.591 | 40370.175 | 40370.175 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16757 | 16757 | 0 | 37224.447 | 40108.031 | 40370.175 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 36438.015 | 40108.031 | 40108.031 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 37224.447 | 40108.031 | 40370.175 |
