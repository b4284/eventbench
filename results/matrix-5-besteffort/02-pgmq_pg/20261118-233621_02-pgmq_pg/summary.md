# 02-pgmq_pg — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.246s, closing 0.296s, drain 19.326s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814472 | 814472 | 12853.251 | 9412.118 | 14737.8 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642754 | 13634.183 | 13893.631 | 28164.095 | 33243.135 | 34275.327 | 34701.311 |
| e2e_from_send | 642754 | 13621.894 | 13885.439 | 28164.095 | 33226.751 | 34275.327 | 34701.311 |
| producer_lag | 642754 | 12.291 | 7.959 | 15.783 | 166.143 | 286.463 | 366.591 |
| publish_call | 117550 | 16.345 | 15.079 | 19.551 | 34.751 | 242.815 | 369.663 |
| queue_wait | 642754 | 13481.847 | 13688.831 | 28049.407 | 33128.447 | 34144.255 | 34701.311 |
| broker_dwell | 642754 | 13469.853 | 13680.639 | 28033.023 | 33128.447 | 34144.255 | 34668.543 |
| receive_call | 181965 | 14.082 | 13.959 | 18.271 | 31.503 | 133.247 | 1050.623 |
| ctx_load | 642754 | 10.682 | 11.935 | 16.031 | 22.191 | 33.727 | 60.319 |
| process | 642754 | 0.002 | 0.002 | 0.003 | 0.006 | 0.04 | 29.359 |
| ctx_commit | 642754 | 10.778 | 11.983 | 16.087 | 22.463 | 37.567 | 368.639 |
| ack | 642754 | 10.771 | 12.023 | 16.135 | 22.335 | 34.047 | 73.599 |
| service | 642754 | 140.039 | 96.127 | 335.103 | 445.183 | 506.111 | 954.367 |
| e2e_large | 331 | 566.872 | 528.895 | 837.631 | 1196.031 | 1371.135 | 1371.135 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9412.118 | 10842.4 |
| applied_total | 329294.204 | 556564 |
| bench_cpu_cores | 2.82 | 3.275 |
| bench_cpu_pct | 11.748 | 13.65 |
| bench_gc_gen0 | 2.735 | 5 |
| bench_gc_gen1 | 2.245 | 4 |
| bench_gc_gen2 | 1.082 | 3 |
| bench_gc_heap_mb | 714.31 | 1199.2 |
| bench_mem_mb | 918.19 | 1250.8 |
| bench_threadpool_queue | 1.98 | 25 |
| bench_threadpool_threads | 28.878 | 30 |
| broker_backlog | 134526.755 | 213316 |
| broker_oldest_age_s | 19.796 | 32 |
| broker_visible | 132094.755 | 211034 |
| ctr_eb_kafka_cpu_pct | 1.887 | 2.27 |
| ctr_eb_kafka_mem_mb | 1617.533 | 1651.8 |
| ctr_eb_mongo_cpu_pct | 2.754 | 15.07 |
| ctr_eb_mongo_mem_mb | 3384.802 | 3425.6 |
| ctr_eb_nats_cpu_pct | 5.663 | 21.21 |
| ctr_eb_nats_mem_mb | 7545.141 | 7710.5 |
| ctr_eb_pgbouncer_cpu_pct | 97.192 | 98.06 |
| ctr_eb_pgbouncer_mem_mb | 21.947 | 29.1 |
| ctr_eb_postgres_cpu_pct | 1174.14 | 1315.16 |
| ctr_eb_postgres_mem_mb | 4560.265 | 4660.6 |
| ctr_eb_rabbitmq_cpu_pct | 14.216 | 15.05 |
| ctr_eb_rabbitmq_mem_mb | 525.147 | 537.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.433 | 80.19 |
| in_flight | 139254.388 | 220845 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 11.551 | 19 |
| pg_conn_idle | 53.857 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 65.408 | 66 |
| pg_db_size_mb | 1986.396 | 2607 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 38321.659 | 42028.7 |
| pg_tup_del_per_s | 30388.167 | 39213.3 |
| pg_tup_fetch_per_s | 293963.933 | 436861 |
| pg_tup_ins_per_s | 47598.637 | 66431.4 |
| pg_tup_upd_per_s | 25134.306 | 27756.7 |
| pg_wal_mb_per_s | 96.878 | 126.26 |
| pgb_cl_active | 885.265 | 1166 |
| pgb_cl_waiting | 280.735 | 406 |
| pgb_maxwait_ms | 8.187 | 14.36 |
| pgb_sv_active | 62.592 | 64 |
| pgb_sv_idle | 1.408 | 23 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.251 | 13348.8 |
| produced_total | 468548.592 | 777409 |
| publish_mb_per_s | 44.334 | 66.42 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,578 records are Completed |
| record_counts | pass | 117,578 records found, 117,578 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,472 events applied by workers, 814,472 generated |
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
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 28442.623 | 34603.007 | 34603.007 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 28704.767 | 34340.863 | 34603.007 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 28180.479 | 34340.863 | 34865.151 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3063 | 3063 | 0 | 27656.191 | 34078.719 | 34340.863 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 28704.767 | 34078.719 | 34340.863 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12129 | 12129 | 0 | 28311.551 | 33816.575 | 34078.719 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 27656.191 | 33161.215 | 33161.215 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 27394.047 | 33161.215 | 33423.359 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 26869.759 | 33030.143 | 33292.287 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9183 | 9183 | 0 | 27262.975 | 32767.999 | 33030.143 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 26738.687 | 32767.999 | 33030.143 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 26738.687 | 32505.855 | 32636.927 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 26345.471 | 32243.711 | 32767.999 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 26476.543 | 31981.567 | 32505.855 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2898 | 2898 | 0 | 25821.183 | 31981.567 | 32243.711 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 26345.471 | 31850.495 | 32243.711 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 26214.399 | 31850.495 | 32243.711 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2988 | 2988 | 0 | 26083.327 | 31719.423 | 32112.639 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 25559.039 | 31195.135 | 31326.207 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 25165.823 | 31195.135 | 31457.279 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13790 | 13790 | 0 | 25296.895 | 31064.063 | 31326.207 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 25165.823 | 30932.991 | 31326.207 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16755 | 16755 | 0 | 25165.823 | 30801.919 | 31195.135 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 24641.535 | 30801.919 | 30932.991 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5490 | 5490 | 0 | 24903.679 | 30801.919 | 31064.063 |
