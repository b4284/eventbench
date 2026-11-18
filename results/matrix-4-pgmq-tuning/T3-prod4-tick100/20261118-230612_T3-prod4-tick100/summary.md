# T3-prod4-tick100 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.232s, closing 0.703s, drain 12.518s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 811233 | 811233 | 12824.776 | 10564.935 | 15759.7 | 0 | 0 | 0 | 435 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 640583 | 7594.894 | 5718.015 | 19283.967 | 25247.743 | 27033.599 | 27934.719 |
| e2e_from_send | 640583 | 7551.247 | 5681.151 | 19251.199 | 25214.975 | 27000.831 | 27869.183 |
| producer_lag | 640583 | 43.649 | 22.463 | 114.239 | 329.471 | 524.799 | 606.719 |
| publish_call | 5408 | 44.403 | 32.927 | 71.615 | 200.447 | 312.831 | 446.975 |
| queue_wait | 640583 | 7431.253 | 5505.023 | 19136.511 | 25100.287 | 26886.143 | 27852.799 |
| broker_dwell | 640583 | 7415.081 | 5488.639 | 19120.127 | 25083.903 | 26869.759 | 27836.415 |
| receive_call | 202971 | 18.396 | 17.247 | 22.431 | 39.327 | 182.527 | 1075.199 |
| ctx_load | 640583 | 13.316 | 13.951 | 18.271 | 28.895 | 126.399 | 308.991 |
| process | 640583 | 0.002 | 0.001 | 0.003 | 0.005 | 0.055 | 18.511 |
| ctx_commit | 640583 | 15.645 | 16.039 | 21.135 | 33.279 | 151.679 | 377.855 |
| service | 640583 | 119.996 | 82.111 | 275.967 | 388.095 | 977.919 | 1178.623 |
| e2e_large | 331 | 335.466 | 322.815 | 467.199 | 664.063 | 899.071 | 899.071 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10564.935 | 12132.9 |
| applied_total | 385178.245 | 632214 |
| bench_cpu_cores | 2.322 | 2.72 |
| bench_cpu_pct | 9.674 | 11.33 |
| bench_gc_gen0 | 2 | 3 |
| bench_gc_gen1 | 1.796 | 3 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 1038.871 | 1282.8 |
| bench_mem_mb | 1229.088 | 1368.9 |
| bench_threadpool_queue | 1.122 | 13 |
| bench_threadpool_threads | 24.347 | 27 |
| broker_backlog | 78475.184 | 141945 |
| broker_oldest_age_s | 15.184 | 26 |
| broker_visible | 76508.714 | 139738 |
| ctr_eb_kafka_cpu_pct | 1.853 | 2.29 |
| ctr_eb_kafka_mem_mb | 2114.635 | 2116.2 |
| ctr_eb_mongo_cpu_pct | 1.807 | 9.25 |
| ctr_eb_mongo_mem_mb | 2569.731 | 2577.8 |
| ctr_eb_nats_cpu_pct | 0.046 | 0.07 |
| ctr_eb_nats_mem_mb | 12.557 | 12.6 |
| ctr_eb_pgbouncer_cpu_pct | 90.506 | 95.21 |
| ctr_eb_pgbouncer_mem_mb | 23.727 | 27.2 |
| ctr_eb_postgres_cpu_pct | 1197.44 | 1272.71 |
| ctr_eb_postgres_mem_mb | 4655.341 | 4798.9 |
| ctr_eb_rabbitmq_cpu_pct | 14.037 | 14.77 |
| ctr_eb_rabbitmq_mem_mb | 708.141 | 712.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 68.735 | 74.32 |
| in_flight | 81262.388 | 141928 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 38.367 | 65 |
| pg_conn_idle | 27.286 | 52 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.653 | 66 |
| pg_db_size_mb | 1952.427 | 2462.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 30238.827 | 34819.3 |
| pg_tup_del_per_s | 32586.082 | 43816.1 |
| pg_tup_fetch_per_s | 299879.176 | 414501.9 |
| pg_tup_ins_per_s | 47483.949 | 63771.4 |
| pg_tup_upd_per_s | 28110.506 | 31991.8 |
| pg_wal_mb_per_s | 102.618 | 149.62 |
| pgb_cl_active | 693.898 | 816 |
| pgb_cl_waiting | 390.531 | 522 |
| pgb_maxwait_ms | 14.072 | 45.95 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12824.776 | 18255.4 |
| produced_total | 466440.633 | 774142 |
| publish_mb_per_s | 45.189 | 78.23 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,050 records are Completed |
| record_counts | pass | 117,050 records found, 117,050 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 435 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 811,233 events applied by workers, 811,233 generated |
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
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3113 | 3113 | 0 | 20054.015 | 27656.191 | 28049.407 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12098 | 12098 | 0 | 19529.727 | 27394.047 | 27656.191 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6226 | 6226 | 0 | 19660.799 | 27000.831 | 27262.975 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2995 | 2995 | 0 | 19398.655 | 26869.759 | 27131.903 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2926 | 2926 | 0 | 19136.511 | 26738.687 | 26869.759 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3010 | 3010 | 0 | 18481.151 | 26214.399 | 26476.543 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5916 | 5916 | 0 | 18612.223 | 26083.327 | 26214.399 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8975 | 8975 | 0 | 18874.367 | 25952.255 | 26214.399 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5903 | 5903 | 0 | 17956.863 | 25690.111 | 25821.183 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8952 | 8952 | 0 | 18350.079 | 25559.039 | 25821.183 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11664 | 11664 | 0 | 17956.863 | 25559.039 | 25690.111 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6061 | 6061 | 0 | 18350.079 | 25559.039 | 25821.183 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8946 | 8946 | 0 | 18087.935 | 25559.039 | 25690.111 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5934 | 5934 | 0 | 18219.007 | 25427.967 | 25690.111 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2929 | 2929 | 0 | 17694.719 | 24772.607 | 25165.823 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5792 | 5792 | 0 | 17301.503 | 24772.607 | 25034.751 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2919 | 2919 | 0 | 17301.503 | 24641.535 | 24772.607 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2955 | 2955 | 0 | 17039.359 | 24248.319 | 24379.391 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8336 | 8336 | 0 | 16646.143 | 23986.175 | 24248.319 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2863 | 2863 | 0 | 16908.287 | 23855.103 | 24248.319 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13695 | 13695 | 0 | 16383.999 | 23461.887 | 23592.959 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2823 | 2823 | 0 | 15794.175 | 23330.815 | 23461.887 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5518 | 5518 | 0 | 16056.319 | 23068.671 | 23330.815 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16481 | 16481 | 0 | 15728.639 | 22806.527 | 23068.671 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2866 | 2866 | 0 | 15400.959 | 22544.383 | 22675.455 |
