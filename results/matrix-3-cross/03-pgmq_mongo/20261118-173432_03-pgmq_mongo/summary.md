# 03-pgmq_mongo — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.272s, closing 0.812s, drain 100.456s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814376 | 814376 | 12826.455 | 4669.376 | 6383.9 | 0 | 0 | 0 | 430 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642660 | 58572.087 | 62095.359 | 95289.343 | 103219.199 | 104202.239 | 104660.991 |
| e2e_from_send | 642660 | 58551.374 | 62062.591 | 95289.343 | 103219.199 | 104202.239 | 104595.455 |
| producer_lag | 642660 | 20.721 | 13.407 | 45.919 | 121.727 | 264.447 | 475.647 |
| publish_call | 85544 | 22.419 | 15.759 | 47.999 | 103.551 | 227.967 | 507.903 |
| queue_wait | 642660 | 58230.745 | 61767.679 | 95092.735 | 103088.127 | 104071.167 | 104595.455 |
| broker_dwell | 642660 | 58215.074 | 61734.911 | 95092.735 | 103088.127 | 104071.167 | 104595.455 |
| receive_call | 107073 | 21.896 | 14.839 | 44.319 | 80.959 | 1151.999 | 1202.175 |
| ctx_load | 642660 | 10.547 | 6.747 | 24.399 | 52.095 | 80.895 | 170.111 |
| process | 642660 | 0.003 | 0.002 | 0.003 | 0.005 | 0.169 | 31.215 |
| ctx_commit | 642660 | 39.206 | 31.679 | 76.095 | 140.671 | 265.215 | 483.583 |
| ack | 642660 | 13.265 | 9.311 | 27.295 | 64.351 | 164.735 | 333.567 |
| service | 642660 | 320.647 | 262.015 | 645.631 | 1120.255 | 1372.159 | 1843.199 |
| e2e_large | 330 | 539.901 | 512.511 | 781.823 | 1052.671 | 1306.623 | 1306.623 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4669.376 | 5343 |
| applied_total | 159154.551 | 270271 |
| bench_cpu_cores | 3.348 | 3.968 |
| bench_cpu_pct | 13.952 | 16.53 |
| bench_gc_gen0 | 4.347 | 6 |
| bench_gc_gen1 | 4.061 | 6 |
| bench_gc_gen2 | 0.449 | 2 |
| bench_gc_heap_mb | 1344.986 | 1771.6 |
| bench_mem_mb | 1624.253 | 1790.2 |
| bench_threadpool_queue | 2.265 | 11 |
| bench_threadpool_threads | 39.061 | 44 |
| broker_backlog | 299749.204 | 494728 |
| broker_oldest_age_s | 29.571 | 49 |
| broker_visible | 295366.837 | 490349 |
| ctr_eb_kafka_cpu_pct | 2.628 | 12.54 |
| ctr_eb_kafka_mem_mb | 1949.047 | 2011.3 |
| ctr_eb_mongo_cpu_pct | 686.406 | 734.09 |
| ctr_eb_mongo_mem_mb | 3031.047 | 3229.1 |
| ctr_eb_nats_cpu_pct | 1.314 | 10.43 |
| ctr_eb_nats_mem_mb | 2084.416 | 2779 |
| ctr_eb_pgbouncer_cpu_pct | 43.352 | 47.02 |
| ctr_eb_pgbouncer_mem_mb | 14.073 | 19.6 |
| ctr_eb_postgres_cpu_pct | 723.454 | 770.92 |
| ctr_eb_postgres_mem_mb | 4402.863 | 4520.4 |
| ctr_eb_rabbitmq_cpu_pct | 16.472 | 17.33 |
| ctr_eb_rabbitmq_mem_mb | 693.749 | 701.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.462 | 84.42 |
| in_flight | 309213.673 | 506142 |
| mongo_conn_current | 864 | 864 |
| mongo_op_command_per_s | 4640.661 | 4984.4 |
| mongo_op_delete_per_s | 60.865 | 1491.2 |
| mongo_op_getmore_per_s | 0.016 | 0.4 |
| mongo_op_insert_per_s | 751.241 | 1031.8 |
| mongo_op_query_per_s | 9276.541 | 9999.7 |
| mongo_op_update_per_s | 7589.122 | 8187.7 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 19.51 | 59 |
| mongo_txn_committed_per_s | 4638.592 | 4999.3 |
| mongo_wt_cache_mb | 1642.604 | 1697.8 |
| mongo_wt_dirty_mb | 114.9 | 154.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12826.455 | 14436 |
| produced_total | 468368.224 | 776413 |
| publish_mb_per_s | 44.281 | 66.45 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,564 records are Completed |
| record_counts | pass | 117,564 records found, 117,564 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,376 events applied by workers, 814,376 generated |
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
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12251 | 12251 | 0 | 96993.279 | 104857.599 | 104857.599 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 96993.279 | 104333.311 | 104333.311 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12124 | 12124 | 0 | 95944.703 | 104333.311 | 104333.311 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 96468.991 | 104333.311 | 104333.311 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 95944.703 | 103809.023 | 103809.023 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6075 | 6075 | 0 | 96468.991 | 103809.023 | 104333.311 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9181 | 9181 | 0 | 94896.127 | 102760.447 | 103284.735 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 93323.263 | 102236.159 | 102236.159 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 93847.551 | 102236.159 | 102760.447 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 92274.687 | 101187.583 | 101711.871 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 93323.263 | 101187.583 | 101187.583 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 92274.687 | 100663.295 | 100663.295 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5910 | 5910 | 0 | 92274.687 | 100663.295 | 101187.583 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 90701.823 | 100139.007 | 100663.295 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 91750.399 | 100139.007 | 100663.295 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 91750.399 | 100139.007 | 100663.295 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 91226.111 | 99614.719 | 100139.007 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13787 | 13787 | 0 | 89128.959 | 99090.431 | 99090.431 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 90177.535 | 99090.431 | 99614.719 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 90177.535 | 99090.431 | 99614.719 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16752 | 16752 | 0 | 89653.247 | 99090.431 | 99614.719 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 89653.247 | 98566.143 | 99090.431 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8601 | 8601 | 0 | 89653.247 | 98566.143 | 99090.431 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8387 | 8387 | 0 | 87556.095 | 98041.855 | 98566.143 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 88604.671 | 98041.855 | 98566.143 |
