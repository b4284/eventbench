# P1-nats-sync_pg — PASS ✅

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 20.799s, closing 2.873s, drain 0.105s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 163193 | 163193 | 7213.843 | 7053.686 | 13619.8 | 0 | 0 | 0 | 105 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 67866 | 7659.065 | 7716.863 | 10002.431 | 11198.463 | 12001.279 | 12673.023 |
| e2e_from_send | 67866 | 168.047 | 101.439 | 258.431 | 1730.559 | 2883.583 | 4284.415 |
| producer_lag | 67866 | 7491.026 | 7548.927 | 9805.823 | 10993.663 | 11829.247 | 12001.279 |
| publish_call | 462 | 1601.845 | 1702.911 | 2965.503 | 3737.599 | 4276.223 | 4276.223 |
| queue_wait | 67866 | 148.07 | 78.591 | 243.199 | 1716.223 | 2861.055 | 4276.223 |
| broker_dwell | 67866 | 46.156 | 22.143 | 79.039 | 540.671 | 917.503 | 1335.295 |
| receive_call | 74662 | 138.314 | 60.351 | 354.815 | 891.903 | 1102.847 | 1549.311 |
| ctx_load | 67866 | 2.369 | 1.359 | 5.895 | 12.199 | 18.591 | 62.431 |
| process | 67866 | 0.002 | 0.001 | 0.002 | 0.005 | 0.202 | 11.511 |
| ctx_commit | 67866 | 3.956 | 2.893 | 7.959 | 15.799 | 53.823 | 75.583 |
| ack | 67634 | 0.034 | 0.002 | 0.006 | 0.565 | 4.559 | 15.471 |
| service | 67866 | 19.978 | 13.359 | 45.727 | 93.631 | 148.223 | 680.447 |
| e2e_large | 62 | 7447.453 | 7516.159 | 9912.319 | 10485.759 | 10797.055 | 10797.055 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7053.686 | 8973.6 |
| applied_total | 90861.5 | 136727 |
| bench_cpu_cores | 2.007 | 2.505 |
| bench_cpu_pct | 8.364 | 10.44 |
| bench_gc_gen0 | 1.786 | 4 |
| bench_gc_gen1 | 0.571 | 3 |
| bench_gc_gen2 | 0.5 | 2 |
| bench_gc_heap_mb | 713.321 | 1009 |
| bench_mem_mb | 1026.357 | 1218.9 |
| bench_threadpool_queue | 11.643 | 84 |
| bench_threadpool_threads | 40 | 40 |
| broker_backlog | 468.357 | 631 |
| broker_backlog_mb | 2.507 | 8.651 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.703 | 2.09 |
| ctr_eb_kafka_mem_mb | 3141.4 | 3141.4 |
| ctr_eb_mongo_cpu_pct | 0.938 | 1.12 |
| ctr_eb_mongo_mem_mb | 2766.6 | 2766.6 |
| ctr_eb_nats_cpu_pct | 454.186 | 472.73 |
| ctr_eb_nats_mem_mb | 987.893 | 1349.4 |
| ctr_eb_pgbouncer_cpu_pct | 37.688 | 38.95 |
| ctr_eb_pgbouncer_mem_mb | 11.886 | 12.7 |
| ctr_eb_postgres_cpu_pct | 220.074 | 235.07 |
| ctr_eb_postgres_mem_mb | 2691.007 | 2780 |
| ctr_eb_rabbitmq_cpu_pct | 12.851 | 13.49 |
| ctr_eb_rabbitmq_mem_mb | 747.864 | 749.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 45.334 | 52.93 |
| in_flight | -14544.929 | -12673 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 1.286 | 6 |
| pg_conn_idle | 62.714 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64 | 64 |
| pg_db_size_mb | 697.45 | 787.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 13051.343 | 16192.7 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 29537.186 | 37877.3 |
| pg_tup_ins_per_s | 8349.8 | 11785.5 |
| pg_tup_upd_per_s | 10379.036 | 13131.8 |
| pg_wal_mb_per_s | 21.731 | 30.89 |
| pgb_cl_active | 398 | 398 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.929 | 14 |
| pgb_sv_idle | 59.071 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7213.843 | 9003.7 |
| produced_total | 76316.571 | 123888 |
| publish_mb_per_s | 31.84 | 63.33 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 36,070 records are Completed |
| record_counts | pass | 36,070 records found, 36,070 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 105 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 163,193 events applied by workers, 163,193 generated |
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
| t-214 | 52 | 1 | 37.783 | 33.86 | 688 | 0 | 476 | 476 | 0 | 7634.943 | 12058.623 | 12124.159 |
| t-327 | 6 | 1 | 66.48 | 6.003 | 370 | 0 | 93 | 93 | 0 | 8126.463 | 12058.623 | 12058.623 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 3144 | 3144 | 0 | 9043.967 | 12058.623 | 12713.983 |
| t-384 | 12 | 1 | 22.317 | 4.426 | 844 | 0 | 74 | 74 | 0 | 6586.367 | 12058.623 | 12058.623 |
| t-241 | 27 | 1 | 11.444 | 5.213 | 2685 | 0 | 109 | 109 | 0 | 7536.639 | 11993.087 | 11993.087 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 3149 | 3149 | 0 | 8912.895 | 11993.087 | 12517.375 |
| t-430 | 6 | 1 | 23.835 | 2.584 | 1163 | 0 | 37 | 37 | 0 | 8323.071 | 11993.087 | 11993.087 |
| t-019 | 38 | 2 | 88.646 | 55.998 | 208 | 0 | 700 | 700 | 0 | 8028.159 | 11927.551 | 12124.159 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 122 | 122 | 0 | 7274.495 | 11927.551 | 11927.551 |
| t-070 | 24 | 1 | 52.218 | 20.948 | 2303 | 0 | 263 | 263 | 0 | 7634.943 | 11927.551 | 12124.159 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 68 | 68 | 0 | 8323.071 | 11927.551 | 11927.551 |
| t-462 | 10 | 1 | 25.654 | 4.163 | 2006 | 0 | 60 | 60 | 0 | 8355.839 | 11927.551 | 11927.551 |
| t-495 | 16 | 1 | 37.363 | 9.583 | 553 | 0 | 111 | 111 | 0 | 7831.551 | 11927.551 | 11927.551 |
| t-219 | 13 | 1 | 32.759 | 6.924 | 272 | 0 | 105 | 105 | 0 | 7372.799 | 11862.015 | 11862.015 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 569 | 569 | 0 | 7372.799 | 11862.015 | 12058.623 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 105 | 105 | 0 | 7766.015 | 11862.015 | 11862.015 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 382 | 382 | 0 | 7733.247 | 11796.479 | 12058.623 |
| t-363 | 19 | 1 | 93.142 | 30.223 | 639 | 0 | 343 | 343 | 0 | 7241.727 | 11796.479 | 11993.087 |
| t-297 | 14 | 1 | 38.488 | 8.638 | 1702 | 0 | 117 | 117 | 0 | 7929.855 | 11730.943 | 11730.943 |
| t-413 | 19 | 1 | 69.524 | 22.975 | 3585 | 0 | 262 | 262 | 0 | 7962.623 | 11730.943 | 12058.623 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 529 | 529 | 0 | 7471.103 | 11730.943 | 11993.087 |
| t-465 | 66 | 2 | 70.741 | 76.327 | 667 | 0 | 927 | 927 | 0 | 7634.943 | 11730.943 | 11993.087 |
| t-467 | 28 | 1 | 18.189 | 8.297 | 250 | 0 | 121 | 121 | 0 | 7634.943 | 11730.943 | 11730.943 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 425 | 425 | 0 | 7536.639 | 11730.943 | 11993.087 |
| t-083 | 58 | 1 | 23.381 | 23.11 | 417 | 0 | 301 | 301 | 0 | 7045.119 | 11665.407 | 12058.623 |
