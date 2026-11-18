# 01-pgmq_pg-txack — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.193s, closing 0.289s, drain 14.049s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814397 | 814397 | 12851.863 | 10235.006 | 17191 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642680 | 10123.398 | 9797.631 | 22806.527 | 28295.167 | 29868.031 | 30359.551 |
| e2e_from_send | 642680 | 10111.031 | 9781.247 | 22790.143 | 28278.783 | 29851.647 | 30343.167 |
| producer_lag | 642680 | 12.361 | 10.415 | 20.095 | 83.263 | 169.343 | 288.255 |
| publish_call | 89853 | 21.392 | 19.647 | 26.063 | 52.223 | 168.831 | 287.999 |
| queue_wait | 642680 | 9986.545 | 9609.215 | 22675.455 | 28164.095 | 29753.343 | 30326.783 |
| broker_dwell | 642680 | 9971.153 | 9601.023 | 22659.071 | 28147.711 | 29736.959 | 30310.399 |
| receive_call | 193178 | 18.816 | 18.495 | 24.511 | 44.991 | 70.079 | 233.215 |
| ctx_load | 642680 | 13.582 | 15.095 | 19.311 | 31.503 | 49.183 | 72.575 |
| process | 642680 | 0.002 | 0.002 | 0.003 | 0.005 | 0.044 | 14.951 |
| ctx_commit | 642680 | 15.672 | 17.055 | 21.967 | 36.095 | 58.303 | 142.847 |
| service | 642680 | 124.488 | 82.687 | 295.423 | 392.959 | 455.423 | 503.295 |
| e2e_large | 331 | 287.04 | 281.087 | 398.079 | 565.247 | 626.175 | 626.175 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10235.006 | 11322.5 |
| applied_total | 357715.592 | 605262 |
| bench_cpu_cores | 2.544 | 3.163 |
| bench_cpu_pct | 10.602 | 13.18 |
| bench_gc_gen0 | 2.163 | 4 |
| bench_gc_gen1 | 1.735 | 3 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 981.722 | 1236.7 |
| bench_mem_mb | 1180.678 | 1354.9 |
| bench_threadpool_queue | 1.714 | 57 |
| bench_threadpool_threads | 31.633 | 44 |
| broker_backlog | 107242.51 | 167522 |
| broker_oldest_age_s | 17.816 | 29 |
| broker_visible | 104982.796 | 165596 |
| ctr_eb_kafka_cpu_pct | 1.935 | 3.27 |
| ctr_eb_kafka_mem_mb | 2651.024 | 2819.1 |
| ctr_eb_mongo_cpu_pct | 1.074 | 1.34 |
| ctr_eb_mongo_mem_mb | 2476.018 | 2495.8 |
| ctr_eb_nats_cpu_pct | 1.733 | 20.46 |
| ctr_eb_nats_mem_mb | 4124.508 | 4363.1 |
| ctr_eb_pgbouncer_cpu_pct | 92.361 | 94.74 |
| ctr_eb_pgbouncer_mem_mb | 17.02 | 21.5 |
| ctr_eb_postgres_cpu_pct | 1221.572 | 1289.45 |
| ctr_eb_postgres_mem_mb | 4746.782 | 4802.7 |
| ctr_eb_rabbitmq_cpu_pct | 13.6 | 14.3 |
| ctr_eb_rabbitmq_mem_mb | 738.267 | 746.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.384 | 75.55 |
| in_flight | 110825.49 | 172064 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.989 | 100 |
| pg_conn_active | 40.592 | 64 |
| pg_conn_idle | 24.98 | 49 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.041 | 1 |
| pg_conn_total | 65.571 | 66 |
| pg_db_size_mb | 1939.837 | 2523.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 29755.212 | 35431.9 |
| pg_tup_del_per_s | 31267.298 | 42509 |
| pg_tup_fetch_per_s | 300046.651 | 439423.2 |
| pg_tup_ins_per_s | 46639.863 | 69720.9 |
| pg_tup_upd_per_s | 26857.647 | 31569.5 |
| pg_wal_mb_per_s | 97.96 | 136.29 |
| pgb_cl_active | 710.245 | 803 |
| pgb_cl_waiting | 419.755 | 472 |
| pgb_maxwait_ms | 14.239 | 23.08 |
| pgb_sv_active | 64 | 64 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12851.863 | 13287.8 |
| produced_total | 468541.082 | 777326 |
| publish_mb_per_s | 44.331 | 66.48 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,570 records are Completed |
| record_counts | pass | 117,570 records found, 117,570 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,397 events applied by workers, 814,397 generated |
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
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12250 | 12250 | 0 | 23068.671 | 30146.559 | 30408.703 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6335 | 6335 | 0 | 23199.743 | 30015.487 | 30146.559 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 6274 | 0 | 23068.671 | 29884.415 | 30146.559 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 22675.455 | 29753.343 | 30146.559 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12128 | 12128 | 0 | 22806.527 | 29360.127 | 29491.199 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 22282.239 | 29097.983 | 29360.127 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 22151.167 | 28704.767 | 28966.911 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 21889.023 | 28442.623 | 29229.055 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 21495.807 | 28311.551 | 28442.623 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 21757.951 | 28311.551 | 28442.623 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 21364.735 | 28180.479 | 28442.623 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 21233.663 | 27787.263 | 27918.335 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 20709.375 | 27656.191 | 27787.263 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 21233.663 | 27525.119 | 27787.263 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 20971.519 | 27525.119 | 28311.551 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 20709.375 | 27394.047 | 27787.263 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 20840.447 | 27394.047 | 27656.191 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 20578.303 | 27131.903 | 27262.975 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13789 | 13789 | 0 | 19922.943 | 26607.615 | 27262.975 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 19791.871 | 26607.615 | 26869.759 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 20185.087 | 26476.543 | 26607.615 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8601 | 8601 | 0 | 19791.871 | 26345.471 | 26476.543 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16756 | 16756 | 0 | 19660.799 | 26214.399 | 26345.471 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 19660.799 | 26083.327 | 26214.399 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 19398.655 | 25821.183 | 26083.327 |
