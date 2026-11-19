# pipe12k-06-rabbit_pg — FAIL ❌

- Broker: `rabbitmq quorum queues, =workers partitions/group (601 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.808s, closing 1.536s, drain 6.335s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 807695 | 807695 | 12518.737 | 12592.773 | 23556.9 | 0 | 0 | 0 | 425 | 4212 | 106736 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 636009 | 393.75 | 35.743 | 1156.095 | 3958.783 | 4579.327 | 8351.743 |
| e2e_from_send | 636009 | 220.401 | 27.823 | 701.951 | 2125.823 | 3012.607 | 6303.743 |
| producer_lag | 636009 | 173.35 | 5.243 | 440.831 | 2772.991 | 3477.503 | 3764.223 |
| publish_call | 174476 | 10.885 | 5.259 | 11.063 | 124.287 | 802.303 | 2879.487 |
| queue_wait | 636009 | 192.411 | 9.767 | 643.071 | 2002.943 | 2586.623 | 5091.327 |
| broker_dwell | 636009 | 190.953 | 9.751 | 633.343 | 2001.919 | 2584.575 | 5091.327 |
| receive_call | 729529 | 35.105 | 19.583 | 95.999 | 185.471 | 200.575 | 259.711 |
| ctx_load | 636009 | 8.32 | 2.145 | 28.111 | 58.303 | 69.631 | 87.615 |
| process | 636009 | 10.884 | 0.001 | 0.002 | 0.007 | 2623.487 | 3000.319 |
| ctx_commit | 636009 | 8.268 | 2.03 | 27.887 | 57.695 | 70.335 | 248.575 |
| ack | 610036 | 0.002 | 0.002 | 0.003 | 0.006 | 0.131 | 8.043 |
| service | 636009 | 27.99 | 4.323 | 58.463 | 127.423 | 2646.015 | 4169.727 |
| e2e_large | 325 | 2649.069 | 2535.423 | 4005.887 | 6197.247 | 6610.943 | 6610.943 |
| ready_wait | 636009 | 95.581 | 5.507 | 256.255 | 1514.495 | 2215.935 | 2699.263 |
| blocked | 636009 | 79.774 | 4.699 | 201.855 | 1500.159 | 2215.935 | 2699.263 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12592.773 | 23556.9 |
| applied_total | 463783.837 | 762176 |
| bench_cpu_cores | 2.687 | 4.038 |
| bench_cpu_pct | 11.194 | 16.82 |
| bench_gc_gen0 | 2.102 | 5 |
| bench_gc_gen1 | 1.184 | 4 |
| bench_gc_gen2 | 0.796 | 3 |
| bench_gc_heap_mb | 433.214 | 613.6 |
| bench_mem_mb | 774.38 | 882.8 |
| bench_threadpool_queue | 4.592 | 78 |
| bench_threadpool_threads | 33.367 | 44 |
| broker_backlog | 1080.612 | 17190 |
| broker_consumers | 601 | 601 |
| broker_ready | 97.49 | 4777 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 983.122 | 12413 |
| ctr_eb_kafka_cpu_pct | 1.664 | 2.04 |
| ctr_eb_kafka_mem_mb | 2309.214 | 2430 |
| ctr_eb_mssql_cpu_pct | 1.751 | 1.99 |
| ctr_eb_mssql_mem_mb | 4438.247 | 4462.2 |
| ctr_eb_nats_cpu_pct | 4.643 | 21.42 |
| ctr_eb_nats_mem_mb | 2064.904 | 2766.6 |
| ctr_eb_pgbouncer_cpu_pct | 54.596 | 63.31 |
| ctr_eb_pgbouncer_mem_mb | 22.688 | 24.8 |
| ctr_eb_postgres_cpu_pct | 296.919 | 325.31 |
| ctr_eb_postgres_mem_mb | 4735.276 | 4747.4 |
| ctr_eb_rabbitmq_cpu_pct | 497.463 | 579.11 |
| ctr_eb_rabbitmq_mem_mb | 1363.033 | 2059.9 |
| ctr_eb_redis_cpu_pct | 0.274 | 0.29 |
| ctr_eb_redis_mem_mb | 1268.071 | 1309.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 51.26 | 66.53 |
| in_flight | 288.347 | 18799 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.184 | 6 |
| pg_conn_idle | 61.878 | 64 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 64.061 | 65 |
| pg_db_size_mb | 1367.365 | 1755.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24454.553 | 29202.4 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 56231.843 | 67491.9 |
| pg_tup_ins_per_s | 10679.233 | 20684 |
| pg_tup_upd_per_s | 20196.369 | 23913.8 |
| pg_wal_mb_per_s | 38.774 | 118.08 |
| pgb_cl_active | 839.898 | 1000 |
| pgb_cl_waiting | 160.102 | 723 |
| pgb_maxwait_ms | 4.952 | 24.52 |
| pgb_sv_active | 33.571 | 64 |
| pgb_sv_idle | 30.429 | 64 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12518.737 | 33471 |
| produced_total | 464072.184 | 759969 |
| publish_mb_per_s | 43.259 | 117.46 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 116,793 records are Completed |
| record_counts | pass | 116,793 records found, 116,793 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 807,695 events applied by workers, 807,695 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 106,736 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1500 ms, max 2699 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6050 seq 3: ready 111 ms before start, 111 ms of it with an idle slot in group 207
- [cross_source_blocking] source 8130 seq 10: ready 113 ms before start, 113 ms of it with an idle slot in group 275
- [cross_source_blocking] source 8867 seq 16: ready 160 ms before start, 129 ms of it with an idle slot in group 306
- [cross_source_blocking] source 9187 seq 10: ready 161 ms before start, 108 ms of it with an idle slot in group 315
- [cross_source_blocking] source 5183 seq 3: ready 118 ms before start, 118 ms of it with an idle slot in group 179
- [cross_source_blocking] source 3331 seq 5: ready 163 ms before start, 163 ms of it with an idle slot in group 113
- [cross_source_blocking] source 2179 seq 4: ready 164 ms before start, 118 ms of it with an idle slot in group 62
- [cross_source_blocking] source 3195 seq 46: ready 110 ms before start, 110 ms of it with an idle slot in group 106
- [cross_source_blocking] source 10335 seq 8: ready 122 ms before start, 122 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3515 seq 15: ready 122 ms before start, 121 ms of it with an idle slot in group 120
- [cross_source_blocking] source 14647 seq 18: ready 106 ms before start, 106 ms of it with an idle slot in group 464
- [cross_source_blocking] source 13299 seq 12: ready 111 ms before start, 111 ms of it with an idle slot in group 425
- [cross_source_blocking] source 14936 seq 24: ready 107 ms before start, 107 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14752 seq 10: ready 107 ms before start, 107 ms of it with an idle slot in group 464
- [cross_source_blocking] source 10281 seq 11: ready 109 ms before start, 109 ms of it with an idle slot in group 344
- [cross_source_blocking] source 5299 seq 12: ready 142 ms before start, 106 ms of it with an idle slot in group 182
- [cross_source_blocking] source 10200 seq 15: ready 109 ms before start, 109 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10166 seq 10: ready 116 ms before start, 109 ms of it with an idle slot in group 344
- [cross_source_blocking] source 14644 seq 5: ready 116 ms before start, 116 ms of it with an idle slot in group 464
- [cross_source_blocking] source 10148 seq 13: ready 116 ms before start, 109 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-338 | 6 | 1 | 21.73 | 2.22 | 156 | 0 | 140 | 140 | 0 | 25.983 | 5996.543 | 6225.919 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 153 | 153 | 0 | 1826.815 | 5734.399 | 6225.919 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 407 | 407 | 0 | 2195.455 | 5570.559 | 6619.135 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 79 | 79 | 0 | 1884.159 | 5537.791 | 5537.791 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 2113.535 | 5210.111 | 5210.111 |
| t-192 | 6 | 1 | 19.403 | 1.728 | 194 | 0 | 144 | 144 | 0 | 21.247 | 4620.287 | 4685.823 |
| t-115 | 10 | 1 | 33.32 | 5.329 | 3658 | 0 | 381 | 381 | 0 | 35.839 | 4521.983 | 5242.879 |
| t-484 | 6 | 1 | 74.942 | 7.586 | 232 | 0 | 469 | 469 | 0 | 32.255 | 4423.679 | 4718.591 |
| t-216 | 19 | 1 | 60.988 | 19.724 | 3889 | 0 | 1221 | 1221 | 0 | 34.047 | 4390.911 | 4554.751 |
| t-079 | 6 | 1 | 96.661 | 9.939 | 1522 | 0 | 540 | 540 | 0 | 53.759 | 4358.143 | 4489.215 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5267 | 5267 | 0 | 34.815 | 4358.143 | 5931.007 |
| t-431 | 6 | 1 | 37.575 | 3.743 | 596 | 0 | 223 | 223 | 0 | 41.727 | 4358.143 | 4521.983 |
| t-493 | 8 | 1 | 85.615 | 12.24 | 1419 | 0 | 726 | 726 | 0 | 42.751 | 4358.143 | 6324.223 |
| t-127 | 19 | 1 | 78.823 | 23.73 | 2026 | 0 | 1502 | 1502 | 0 | 36.607 | 4325.375 | 5636.095 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 7961 | 7961 | 0 | 36.863 | 4325.375 | 6651.903 |
| t-226 | 6 | 1 | 87.952 | 9.302 | 623 | 0 | 568 | 568 | 0 | 28.671 | 4325.375 | 4620.287 |
| t-159 | 6 | 1 | 46.704 | 4.469 | 295 | 0 | 287 | 287 | 0 | 32.767 | 4292.607 | 4423.679 |
| t-193 | 19 | 1 | 12.672 | 4.096 | 1805 | 0 | 280 | 280 | 0 | 24.447 | 4292.607 | 4587.519 |
| t-012 | 6 | 1 | 61.836 | 6.408 | 556 | 0 | 377 | 377 | 0 | 32.639 | 4259.839 | 4390.911 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 391 | 391 | 0 | 31.231 | 4259.839 | 4653.055 |
| t-471 | 28 | 1 | 10.495 | 4.976 | 330 | 0 | 346 | 346 | 0 | 33.535 | 4259.839 | 4423.679 |
| t-002 | 17 | 1 | 82.711 | 23.551 | 377 | 0 | 1495 | 1495 | 0 | 48.639 | 4227.071 | 5898.239 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2967 | 2967 | 0 | 38.399 | 4227.071 | 6881.279 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 298 | 298 | 0 | 24.703 | 4227.071 | 4554.751 |
| t-201 | 21 | 1 | 28.959 | 9.91 | 811 | 0 | 664 | 664 | 0 | 40.959 | 4227.071 | 4915.199 |
