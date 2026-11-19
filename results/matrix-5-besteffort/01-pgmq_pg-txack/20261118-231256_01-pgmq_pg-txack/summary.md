# 01-pgmq_pg-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.404s, closing 0.384s, drain 10.7s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814417 | 814417 | 12854.706 | 10696.812 | 17796.2 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642697 | 8515.46 | 7929.855 | 19922.943 | 25542.655 | 27344.895 | 27967.487 |
| e2e_from_send | 642697 | 8500.549 | 7913.471 | 19906.559 | 25526.271 | 27328.511 | 27967.487 |
| producer_lag | 642697 | 14.911 | 9.599 | 20.959 | 178.303 | 315.391 | 457.727 |
| publish_call | 98210 | 19.592 | 17.599 | 24.927 | 48.863 | 263.679 | 459.775 |
| queue_wait | 642697 | 8383.567 | 7749.631 | 19791.871 | 25444.351 | 27230.207 | 27885.567 |
| broker_dwell | 642697 | 8368.948 | 7737.343 | 19775.487 | 25427.967 | 27230.207 | 27869.183 |
| receive_call | 204458 | 17.71 | 16.351 | 23.151 | 42.367 | 282.111 | 1396.735 |
| ctx_load | 642697 | 13.997 | 14.439 | 20.927 | 33.087 | 64.991 | 139.263 |
| process | 642697 | 0.003 | 0.001 | 0.002 | 0.005 | 0.046 | 54.079 |
| ctx_commit | 642697 | 14.285 | 14.703 | 21.215 | 33.983 | 70.207 | 267.519 |
| service | 642697 | 116.987 | 76.863 | 271.871 | 412.415 | 519.679 | 966.655 |
| e2e_large | 331 | 574.055 | 538.111 | 906.239 | 1222.655 | 1303.551 | 1303.551 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10696.812 | 12884.5 |
| applied_total | 372059.224 | 633814 |
| bench_cpu_cores | 2.622 | 3.074 |
| bench_cpu_pct | 10.924 | 12.81 |
| bench_gc_gen0 | 2.184 | 3 |
| bench_gc_gen1 | 1.816 | 3 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 1004.941 | 1249.2 |
| bench_mem_mb | 1209.08 | 1450.9 |
| bench_threadpool_queue | 4.082 | 83 |
| bench_threadpool_threads | 27.857 | 41 |
| broker_backlog | 92815.51 | 138309 |
| broker_oldest_age_s | 16.694 | 26 |
| broker_visible | 90593.082 | 136407 |
| ctr_eb_kafka_cpu_pct | 1.983 | 2.44 |
| ctr_eb_kafka_mem_mb | 1976.394 | 1994.1 |
| ctr_eb_mongo_cpu_pct | 1.286 | 2.25 |
| ctr_eb_mongo_mem_mb | 3595.769 | 3680.2 |
| ctr_eb_nats_cpu_pct | 0.047 | 0.11 |
| ctr_eb_nats_mem_mb | 10.418 | 10.5 |
| ctr_eb_pgbouncer_cpu_pct | 92.418 | 95.43 |
| ctr_eb_pgbouncer_mem_mb | 27.847 | 32.8 |
| ctr_eb_postgres_cpu_pct | 1239.687 | 1386.26 |
| ctr_eb_postgres_mem_mb | 4638.31 | 4727.4 |
| ctr_eb_rabbitmq_cpu_pct | 15.324 | 16.49 |
| ctr_eb_rabbitmq_mem_mb | 705.316 | 709.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.423 | 81.49 |
| in_flight | 96439.286 | 143574 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.997 | 100 |
| pg_conn_active | 12.388 | 21 |
| pg_conn_idle | 53.082 | 60 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.122 | 2 |
| pg_conn_total | 65.469 | 66 |
| pg_db_size_mb | 1988.029 | 2419.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 31405.61 | 37519.8 |
| pg_tup_del_per_s | 32346.29 | 40531 |
| pg_tup_fetch_per_s | 315725.155 | 477785 |
| pg_tup_ins_per_s | 47393.006 | 64932.6 |
| pg_tup_upd_per_s | 28042.473 | 33275.1 |
| pg_wal_mb_per_s | 102.884 | 176.81 |
| pgb_cl_active | 805.245 | 1115 |
| pgb_cl_waiting | 309.755 | 429 |
| pgb_maxwait_ms | 11.984 | 25.97 |
| pgb_sv_active | 60.143 | 64 |
| pgb_sv_idle | 3.857 | 63 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12854.706 | 13569.8 |
| produced_total | 468498.51 | 777388 |
| publish_mb_per_s | 44.336 | 71.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,576 records are Completed |
| record_counts | pass | 117,576 records found, 117,576 expected; 0 sources with count mismatch |
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
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 20840.447 | 27918.335 | 28049.407 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12251 | 12251 | 0 | 20316.159 | 27656.191 | 27787.263 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 20185.087 | 27394.047 | 27787.263 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12129 | 12129 | 0 | 20578.303 | 27262.975 | 27394.047 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 19922.943 | 26869.759 | 27131.903 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 19529.727 | 26345.471 | 26869.759 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 19267.583 | 26214.399 | 26607.615 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 19005.439 | 25821.183 | 26083.327 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3024 | 3024 | 0 | 19398.655 | 25821.183 | 26083.327 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 18743.295 | 25559.039 | 25559.039 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 18874.367 | 25427.967 | 25559.039 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 18874.367 | 25427.967 | 25559.039 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 18087.935 | 25034.751 | 25296.895 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 18481.151 | 24903.679 | 25296.895 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 18481.151 | 24510.463 | 24641.535 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 17956.863 | 24379.391 | 25034.751 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 18219.007 | 24379.391 | 24510.463 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 17956.863 | 23855.103 | 24117.247 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 17563.647 | 23724.031 | 23986.175 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 17170.431 | 23724.031 | 23986.175 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13788 | 13788 | 0 | 17301.503 | 23592.959 | 23855.103 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16755 | 16755 | 0 | 17170.431 | 23592.959 | 23855.103 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 17170.431 | 23461.887 | 23592.959 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 17039.359 | 23199.743 | 23330.815 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 16908.287 | 23068.671 | 23199.743 |
