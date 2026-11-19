# 05-rabbit_pg — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.11s, closing 1.869s, drain 4.609s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814543 | 814543 | 12850.837 | 12822.963 | 16539.5 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642825 | 54.952 | 13.591 | 108.863 | 778.239 | 1155.071 | 1435.647 |
| e2e_from_send | 642825 | 48.781 | 10.407 | 83.199 | 769.023 | 1148.927 | 1426.431 |
| producer_lag | 642825 | 6.171 | 2.905 | 10.111 | 86.655 | 202.111 | 229.631 |
| publish_call | 265803 | 6.96 | 5.003 | 10.207 | 29.263 | 221.439 | 1030.143 |
| queue_wait | 642825 | 33.182 | 5.843 | 41.311 | 655.871 | 1042.943 | 1366.015 |
| broker_dwell | 642825 | 33.137 | 5.835 | 40.607 | 655.871 | 1042.431 | 1366.015 |
| receive_call | 657733 | 33.851 | 19.343 | 90.495 | 179.327 | 199.167 | 239.999 |
| ctx_load | 642825 | 3.727 | 1.81 | 10.543 | 20.623 | 30.095 | 48.799 |
| process | 642825 | 0.002 | 0.001 | 0.002 | 0.005 | 0.068 | 12.375 |
| ctx_commit | 642825 | 3.708 | 1.738 | 10.615 | 21.007 | 30.191 | 276.991 |
| ack | 622292 | 0.002 | 0.002 | 0.003 | 0.005 | 0.058 | 4.755 |
| service | 642825 | 15.6 | 3.849 | 31.423 | 207.615 | 333.055 | 511.487 |
| e2e_large | 331 | 82.192 | 54.559 | 181.631 | 442.879 | 564.735 | 564.735 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12822.963 | 14156.6 |
| applied_total | 468202.449 | 776140 |
| bench_cpu_cores | 2.491 | 2.661 |
| bench_cpu_pct | 10.381 | 11.09 |
| bench_gc_gen0 | 2.327 | 4 |
| bench_gc_gen1 | 0.449 | 2 |
| bench_gc_gen2 | 0.429 | 2 |
| bench_gc_heap_mb | 488.439 | 619.1 |
| bench_mem_mb | 768.471 | 829 |
| bench_threadpool_queue | 7.163 | 150 |
| bench_threadpool_threads | 27.408 | 39 |
| broker_backlog | 593.367 | 5429 |
| broker_consumers | 598 | 598 |
| broker_ready | 29.306 | 359 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 564.061 | 5070 |
| ctr_eb_kafka_cpu_pct | 1.757 | 2.77 |
| ctr_eb_kafka_mem_mb | 1792.473 | 1811 |
| ctr_eb_mongo_cpu_pct | 1.181 | 2.4 |
| ctr_eb_mongo_mem_mb | 3696.578 | 3838.5 |
| ctr_eb_nats_cpu_pct | 1.224 | 9.47 |
| ctr_eb_nats_mem_mb | 3497.731 | 3868.1 |
| ctr_eb_pgbouncer_cpu_pct | 53.649 | 69.53 |
| ctr_eb_pgbouncer_mem_mb | 22.58 | 24.1 |
| ctr_eb_postgres_cpu_pct | 315.712 | 398.18 |
| ctr_eb_postgres_mem_mb | 4952.606 | 5009.7 |
| ctr_eb_rabbitmq_cpu_pct | 578.948 | 598.96 |
| ctr_eb_rabbitmq_mem_mb | 1133.229 | 1192.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 54.432 | 61.98 |
| in_flight | 552.122 | 5536 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 3.286 | 8 |
| pg_conn_idle | 60.776 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.061 | 65 |
| pg_db_size_mb | 1316.176 | 1769.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25707.688 | 29329.8 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 59066.849 | 65669.8 |
| pg_tup_ins_per_s | 10634.639 | 19502.2 |
| pg_tup_upd_per_s | 21237.202 | 23870.9 |
| pg_wal_mb_per_s | 33.054 | 51.39 |
| pgb_cl_active | 458.429 | 472 |
| pgb_cl_waiting | 12.796 | 187 |
| pgb_maxwait_ms | 0.779 | 11.65 |
| pgb_sv_active | 16.714 | 64 |
| pgb_sv_idle | 47.286 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12850.837 | 14228.7 |
| produced_total | 468754.571 | 777494 |
| publish_mb_per_s | 44.327 | 71.22 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,581 records are Completed |
| record_counts | pass | 117,581 records found, 117,581 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,543 events applied by workers, 814,543 generated |
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
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9183 | 9183 | 0 | 13.823 | 1261.567 | 1425.407 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5911 | 5911 | 0 | 14.207 | 1204.223 | 1441.791 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 13.759 | 1171.455 | 1294.335 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 14.079 | 1146.879 | 1245.183 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12131 | 12131 | 0 | 13.759 | 1105.919 | 1384.447 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 2978 | 0 | 13.631 | 1105.919 | 1196.031 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 14.207 | 1097.727 | 1187.839 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12253 | 12253 | 0 | 13.823 | 1097.727 | 1310.719 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 13.375 | 1081.343 | 1351.679 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2988 | 2988 | 0 | 13.759 | 1048.575 | 1146.879 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3026 | 3026 | 0 | 13.695 | 1028.095 | 1138.687 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 13.631 | 1019.903 | 1335.295 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 0 | 13.631 | 1019.903 | 1130.495 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 14.015 | 999.423 | 1114.111 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13790 | 13790 | 0 | 13.631 | 995.327 | 1400.831 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16764 | 16764 | 0 | 13.567 | 987.135 | 1302.527 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 13.823 | 987.135 | 1294.335 |
| t-121 | 76 | 3 | 97.816 | 119.994 | 149 | 0 | 7236 | 7236 | 0 | 13.759 | 970.751 | 1130.495 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 0 | 13.887 | 966.655 | 1122.303 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8035 | 8035 | 0 | 13.887 | 937.983 | 1122.303 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 0 | 13.887 | 913.407 | 995.327 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 2660 | 2660 | 0 | 13.951 | 913.407 | 1044.479 |
| t-285 | 43 | 1 | 52.317 | 37.29 | 233 | 0 | 2308 | 2308 | 0 | 13.567 | 909.311 | 970.751 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2833 | 2833 | 0 | 14.143 | 905.215 | 974.847 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 10467 | 10467 | 0 | 13.567 | 905.215 | 1146.879 |
