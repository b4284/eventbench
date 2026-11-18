# T1-pgmq_pg-txack-pgb4x48 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.254s, closing 0.558s, drain 19.451s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814347 | 814347 | 12867.359 | 9395.516 | 16277.4 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642630 | 14267.523 | 14909.439 | 28409.855 | 33308.671 | 34406.399 | 34832.383 |
| e2e_from_send | 642630 | 14246.016 | 14893.055 | 28393.471 | 33292.287 | 34373.631 | 34832.383 |
| producer_lag | 642630 | 21.502 | 16.863 | 39.967 | 125.311 | 243.071 | 431.103 |
| publish_call | 52524 | 36.736 | 30.463 | 50.623 | 149.247 | 870.399 | 1682.431 |
| queue_wait | 642630 | 14105.296 | 14704.639 | 28278.783 | 33177.599 | 34275.327 | 34832.383 |
| broker_dwell | 642630 | 14092.039 | 14688.255 | 28262.399 | 33161.215 | 34275.327 | 34799.615 |
| receive_call | 173475 | 25.863 | 24.383 | 38.879 | 61.663 | 106.367 | 1133.567 |
| ctx_load | 642630 | 11.46 | 10.935 | 20.303 | 33.343 | 54.815 | 129.599 |
| process | 642630 | 0.003 | 0.002 | 0.003 | 0.006 | 0.103 | 44.511 |
| ctx_commit | 642630 | 19.494 | 19.119 | 33.247 | 51.487 | 77.055 | 185.983 |
| service | 642630 | 140.729 | 98.751 | 332.543 | 448.767 | 523.775 | 899.071 |
| e2e_large | 331 | 346.114 | 337.407 | 476.159 | 669.183 | 859.647 | 859.647 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9395.516 | 10882.2 |
| applied_total | 320349.388 | 550671 |
| bench_cpu_cores | 2.232 | 2.845 |
| bench_cpu_pct | 9.301 | 11.86 |
| bench_gc_gen0 | 1.98 | 3 |
| bench_gc_gen1 | 1.469 | 3 |
| bench_gc_gen2 | 0.469 | 1 |
| bench_gc_heap_mb | 1109.369 | 1453.7 |
| bench_mem_mb | 1517.627 | 1690 |
| bench_threadpool_queue | 1.531 | 16 |
| bench_threadpool_threads | 36.633 | 44 |
| broker_backlog | 141957.816 | 222288 |
| broker_oldest_age_s | 19.959 | 33 |
| broker_visible | 138941.102 | 219500 |
| ctr_eb_kafka_cpu_pct | 1.99 | 2.54 |
| ctr_eb_kafka_mem_mb | 2387.945 | 2539.6 |
| ctr_eb_mongo_cpu_pct | 1.383 | 4.53 |
| ctr_eb_mongo_mem_mb | 2651.476 | 2688.4 |
| ctr_eb_nats_cpu_pct | 0.052 | 0.09 |
| ctr_eb_nats_mem_mb | 6.686 | 8 |
| ctr_eb_pgbouncer_2_cpu_pct | 29.439 | 31.28 |
| ctr_eb_pgbouncer_2_mem_mb | 10.996 | 11.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.429 | 33.53 |
| ctr_eb_pgbouncer_3_mem_mb | 11.163 | 11.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 29.681 | 31.41 |
| ctr_eb_pgbouncer_4_mem_mb | 11.202 | 11.8 |
| ctr_eb_pgbouncer_cpu_pct | 29.478 | 31.56 |
| ctr_eb_pgbouncer_mem_mb | 11.771 | 12.8 |
| ctr_eb_postgres_cpu_pct | 1408.229 | 1435.45 |
| ctr_eb_postgres_mem_mb | 7560.929 | 7926 |
| ctr_eb_rabbitmq_cpu_pct | 15.711 | 18.07 |
| ctr_eb_rabbitmq_mem_mb | 715.702 | 723.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.314 | 81 |
| in_flight | 147840.306 | 226429 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.947 | 100 |
| pg_conn_active | 167.061 | 192 |
| pg_conn_idle | 27.551 | 69 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.082 | 1 |
| pg_conn_total | 194.612 | 196 |
| pg_db_size_mb | 2393.612 | 2740.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26429.196 | 28914.6 |
| pg_tup_del_per_s | 30093.414 | 39829.7 |
| pg_tup_fetch_per_s | 434950.596 | 479815.2 |
| pg_tup_ins_per_s | 47455.02 | 63752.2 |
| pg_tup_upd_per_s | 24798.386 | 27084.5 |
| pg_wal_mb_per_s | 101.761 | 163.89 |
| pgb_cl_active | 248.571 | 300 |
| pgb_cl_waiting | 42 | 89 |
| pgb_maxwait_ms | 5.991 | 13.25 |
| pgb_sv_active | 46.367 | 48 |
| pgb_sv_idle | 1.633 | 14 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12867.359 | 14051.7 |
| produced_total | 468189.694 | 777100 |
| publish_mb_per_s | 44.36 | 71.97 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,562 records are Completed |
| record_counts | pass | 117,562 records found, 117,562 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,347 events applied by workers, 814,347 generated |
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
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 28573.695 | 34603.007 | 34865.151 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12248 | 12248 | 0 | 28835.839 | 34603.007 | 34865.151 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 28704.767 | 34340.863 | 34603.007 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12126 | 12126 | 0 | 28966.911 | 34340.863 | 34603.007 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 28180.479 | 34340.863 | 34603.007 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6273 | 6273 | 0 | 28966.911 | 34340.863 | 34340.863 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 27131.903 | 33423.359 | 33554.431 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 27787.263 | 33292.287 | 33423.359 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9185 | 9185 | 0 | 27787.263 | 33161.215 | 33423.359 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 27394.047 | 33030.143 | 33161.215 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3005 | 3005 | 0 | 27525.119 | 33030.143 | 33554.431 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8788 | 8788 | 0 | 27131.903 | 32636.927 | 32899.071 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 26345.471 | 32374.783 | 32636.927 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 26738.687 | 32243.711 | 32505.855 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 26607.615 | 32112.639 | 32505.855 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 26345.471 | 31981.567 | 32112.639 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 26345.471 | 31850.495 | 32374.783 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2897 | 2897 | 0 | 25821.183 | 31850.495 | 31981.567 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 25952.255 | 31588.351 | 31719.423 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13787 | 13787 | 0 | 25690.111 | 31326.207 | 31719.423 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 25296.895 | 31326.207 | 31457.279 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5489 | 5489 | 0 | 25427.967 | 31326.207 | 31457.279 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16756 | 16756 | 0 | 25559.039 | 31195.135 | 31457.279 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 25034.751 | 31064.063 | 31195.135 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 25427.967 | 31064.063 | 31195.135 |
