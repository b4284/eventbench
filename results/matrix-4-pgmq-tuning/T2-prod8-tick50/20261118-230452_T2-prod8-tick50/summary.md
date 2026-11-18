# T2-prod8-tick50 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.184s, closing 0.573s, drain 13.86s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 813800 | 813800 | 12853.514 | 10353.322 | 15609.9 | 0 | 0 | 0 | 448 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642316 | 9879.298 | 9330.687 | 22315.007 | 27967.487 | 29196.287 | 30130.175 |
| e2e_from_send | 642316 | 9857.508 | 9314.303 | 22298.623 | 27951.103 | 29163.519 | 30048.255 |
| producer_lag | 642316 | 21.79 | 14.375 | 39.903 | 160.127 | 245.119 | 321.791 |
| publish_call | 15970 | 30.144 | 24.799 | 39.039 | 157.439 | 251.519 | 372.991 |
| queue_wait | 642316 | 9734.205 | 9150.463 | 22183.935 | 27852.799 | 29065.215 | 30031.871 |
| broker_dwell | 642316 | 9718.491 | 9134.079 | 22167.551 | 27836.415 | 29048.831 | 30015.487 |
| receive_call | 193138 | 19.386 | 18.239 | 24.943 | 43.423 | 146.943 | 1132.543 |
| ctx_load | 642316 | 13.389 | 14.543 | 19.119 | 32.191 | 59.487 | 150.527 |
| process | 642316 | 0.002 | 0.001 | 0.003 | 0.005 | 0.049 | 17.615 |
| ctx_commit | 642316 | 15.595 | 16.607 | 21.951 | 36.575 | 77.119 | 155.007 |
| service | 642316 | 123.302 | 83.647 | 289.023 | 399.871 | 526.847 | 639.487 |
| e2e_large | 339 | 318.52 | 301.823 | 438.783 | 651.775 | 700.415 | 700.415 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10353.322 | 12172.9 |
| applied_total | 360272.551 | 608724 |
| bench_cpu_cores | 2.341 | 2.712 |
| bench_cpu_pct | 9.752 | 11.3 |
| bench_gc_gen0 | 1.878 | 3 |
| bench_gc_gen1 | 1.714 | 3 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 1252.157 | 1639.2 |
| bench_mem_mb | 1457.896 | 1656.9 |
| bench_threadpool_queue | 1.49 | 17 |
| bench_threadpool_threads | 30.673 | 37 |
| broker_backlog | 104117.653 | 167839 |
| broker_oldest_age_s | 17.633 | 29 |
| broker_visible | 101952.837 | 165477 |
| ctr_eb_kafka_cpu_pct | 1.83 | 2.25 |
| ctr_eb_kafka_mem_mb | 2135.145 | 2136.2 |
| ctr_eb_mongo_cpu_pct | 1.102 | 2.11 |
| ctr_eb_mongo_mem_mb | 2576.378 | 2576.8 |
| ctr_eb_nats_cpu_pct | 0.054 | 0.23 |
| ctr_eb_nats_mem_mb | 9.614 | 12.4 |
| ctr_eb_pgbouncer_cpu_pct | 91.201 | 94.81 |
| ctr_eb_pgbouncer_mem_mb | 22.963 | 26 |
| ctr_eb_postgres_cpu_pct | 1214.414 | 1297.62 |
| ctr_eb_postgres_mem_mb | 4532.204 | 4612 |
| ctr_eb_rabbitmq_cpu_pct | 14.452 | 15.24 |
| ctr_eb_rabbitmq_mem_mb | 709.576 | 713.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 69.876 | 73.81 |
| in_flight | 107581.653 | 167937 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 43.939 | 65 |
| pg_conn_idle | 21.776 | 43 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.714 | 66 |
| pg_db_size_mb | 1986.567 | 2544.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 29336.278 | 34796 |
| pg_tup_del_per_s | 32157.704 | 40154.1 |
| pg_tup_fetch_per_s | 289422.894 | 410572.8 |
| pg_tup_ins_per_s | 47582.643 | 65882.2 |
| pg_tup_upd_per_s | 27549.335 | 32559.7 |
| pg_wal_mb_per_s | 97.92 | 142.01 |
| pgb_cl_active | 697.857 | 905 |
| pgb_cl_waiting | 395.143 | 499 |
| pgb_maxwait_ms | 16.216 | 31.11 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.514 | 13695.5 |
| produced_total | 467854.204 | 776661 |
| publish_mb_per_s | 45.385 | 64.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,483 records are Completed |
| record_counts | pass | 117,483 records found, 117,483 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 448 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 813,800 events applied by workers, 813,800 generated |
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
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3089 | 3089 | 0 | 22675.455 | 29884.415 | 30146.559 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6276 | 6276 | 0 | 22151.167 | 29622.271 | 29753.343 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3100 | 3100 | 0 | 22413.311 | 29491.199 | 29622.271 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 3001 | 3001 | 0 | 21889.023 | 29360.127 | 29491.199 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12076 | 12076 | 0 | 22413.311 | 29229.055 | 29360.127 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12121 | 12121 | 0 | 22151.167 | 29229.055 | 29360.127 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6016 | 6016 | 0 | 22020.095 | 28835.839 | 29229.055 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6189 | 6189 | 0 | 21757.951 | 28573.695 | 28704.767 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8974 | 8974 | 0 | 21889.023 | 28442.623 | 28704.767 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 9080 | 9080 | 0 | 21364.735 | 28311.551 | 28573.695 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5858 | 5858 | 0 | 20840.447 | 28180.479 | 28442.623 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2913 | 2913 | 0 | 20971.519 | 27787.263 | 27918.335 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 6103 | 6103 | 0 | 20840.447 | 27656.191 | 27918.335 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2848 | 2848 | 0 | 19922.943 | 27394.047 | 27656.191 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2931 | 2931 | 0 | 20316.159 | 27394.047 | 27656.191 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5743 | 5743 | 0 | 20316.159 | 27131.903 | 27262.975 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2874 | 2874 | 0 | 20316.159 | 26607.615 | 26738.687 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8746 | 8746 | 0 | 19791.871 | 26607.615 | 26869.759 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2876 | 2876 | 0 | 19398.655 | 26476.543 | 26869.759 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16802 | 16802 | 0 | 19529.727 | 26345.471 | 26607.615 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8332 | 8332 | 0 | 19267.583 | 26083.327 | 26214.399 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13698 | 13698 | 0 | 19398.655 | 25952.255 | 26214.399 |
| t-189 | 59 | 1 | 44.753 | 43.324 | 1601 | 0 | 2753 | 2753 | 0 | 19398.655 | 25952.255 | 26083.327 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8038 | 8038 | 0 | 19005.439 | 25559.039 | 25821.183 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2822 | 2822 | 0 | 18612.223 | 25296.895 | 25690.111 |
