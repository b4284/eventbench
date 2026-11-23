# Wb-pgsync-full — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.273s, closing 1.911s, drain 19.523s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 994914 | 994914 | 15780.155 | 10659.343 | 28129.8 | 0 | 0 | 0 | 457 | 5143 | 190335 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 791521 | 13900.316 | 9469.951 | 34897.919 | 46694.399 | 51150.847 | 54951.935 |
| e2e_from_send | 791521 | 12392.772 | 8241.151 | 31719.423 | 46661.631 | 51052.543 | 54755.327 |
| producer_lag | 791521 | 1507.552 | 992.255 | 4255.743 | 5099.519 | 5267.455 | 5623.807 |
| publish_call | 994914 | 93.001 | 70.975 | 143.487 | 545.791 | 2314.239 | 2463.743 |
| queue_wait | 791521 | 12271.695 | 8110.079 | 31653.887 | 46628.863 | 51019.775 | 54755.327 |
| broker_dwell | 791521 | 12187.551 | 8024.063 | 31539.199 | 46563.327 | 50987.007 | 54689.791 |
| receive_call | 903252 | 20.15 | 6.739 | 43.327 | 265.215 | 452.607 | 577.535 |
| ctx_load | 791521 | 6.465 | 4.619 | 12.783 | 33.823 | 67.071 | 157.951 |
| process | 791521 | 10.761 | 0.002 | 0.004 | 0.048 | 2621.439 | 3006.463 |
| ctx_commit | 791521 | 103.757 | 70.783 | 186.879 | 831.487 | 4329.471 | 4382.719 |
| ack | 791516 | 0.009 | 0.004 | 0.009 | 0.051 | 0.585 | 82.879 |
| service | 791521 | 121.079 | 78.783 | 198.399 | 1230.847 | 4382.719 | 7278.591 |
| e2e_large | 355 | 3984.46 | 3477.503 | 6631.423 | 8011.775 | 8806.399 | 8806.399 |
| ready_wait | 791521 | 563.081 | 49.055 | 772.095 | 9658.367 | 42663.935 | 48332.799 |
| blocked | 791521 | 202.498 | 26.527 | 285.439 | 2695.167 | 18055.167 | 22069.247 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10659.343 | 18018.8 |
| applied_total | 399086.347 | 652115 |
| bench_cpu_cores | 4.261 | 6.706 |
| bench_cpu_pct | 17.754 | 27.94 |
| bench_gc_gen0 | 2.061 | 5 |
| bench_gc_gen1 | 0.878 | 2 |
| bench_gc_gen2 | 0.694 | 2 |
| bench_gc_heap_mb | 1356.229 | 1825.1 |
| bench_mem_mb | 2098.945 | 2473.7 |
| bench_threadpool_queue | 73.939 | 1088 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 147510.082 | 294332 |
| ctr_eb_pg_standby_cpu_pct | 55.607 | 74.46 |
| ctr_eb_pg_standby_mem_mb | 3050.633 | 3140.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 11.913 | 17.14 |
| ctr_eb_pgbouncer_2_mem_mb | 15.329 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 12.315 | 17.34 |
| ctr_eb_pgbouncer_3_mem_mb | 15.496 | 16.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 11.656 | 16.49 |
| ctr_eb_pgbouncer_4_mem_mb | 15.2 | 16.3 |
| ctr_eb_pgbouncer_cpu_pct | 12.049 | 17.33 |
| ctr_eb_pgbouncer_mem_mb | 15.239 | 16.2 |
| ctr_eb_postgres_cpu_pct | 509.531 | 751.88 |
| ctr_eb_postgres_mem_mb | 4865.804 | 5289.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 51.336 | 72.2 |
| in_flight | 154310.286 | 301724 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.141 | 1.54 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 934332.327 | 1608748 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 197452.755 | 327774 |
| octopus_mqtt_sent_total | 237280.327 | 391791 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.939 | 34 |
| pg_active_loads | 1.592 | 12 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.964 | 100 |
| pg_conn_active | 42.898 | 65 |
| pg_conn_idle | 212 | 236 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.245 | 4 |
| pg_conn_total | 254.898 | 256 |
| pg_db_size_mb | 3699.359 | 4534.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 19.082 | 130.95 |
| pg_replica_replay_lag_ms | 281.035 | 3041 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 11729.276 | 17863.2 |
| pg_tup_del_per_s | 24062.043 | 39779.9 |
| pg_tup_fetch_per_s | 222047.145 | 538898.4 |
| pg_tup_ins_per_s | 39629.565 | 54604.5 |
| pg_tup_upd_per_s | 29071.745 | 44739.6 |
| pg_wal_mb_per_s | 105.833 | 178.07 |
| pgb_cl_active | 250.122 | 260 |
| pgb_cl_waiting | 0.735 | 12 |
| pgb_maxwait_ms | 0.016 | 0.26 |
| pgb_sv_active | 12.286 | 48 |
| pgb_sv_idle | 35.714 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15780.155 | 25259.9 |
| produced_total | 553396.633 | 953839 |
| publish_mb_per_s | 51.454 | 89.96 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,267 records are Completed |
| record_counts | pass | 138,267 records found, 138,267 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 994,914 events applied by workers, 994,914 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 190,335 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2695 ms, max 22067 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2009 seq 18: ready 139 ms before start, 139 ms of it with an idle slot in group 59
- [cross_source_blocking] source 15249 seq 11: ready 149 ms before start, 149 ms of it with an idle slot in group 476
- [cross_source_blocking] source 8565 seq 22: ready 153 ms before start, 153 ms of it with an idle slot in group 294
- [cross_source_blocking] source 14867 seq 6: ready 154 ms before start, 154 ms of it with an idle slot in group 466
- [cross_source_blocking] source 4745 seq 21: ready 123 ms before start, 123 ms of it with an idle slot in group 164
- [cross_source_blocking] source 8449 seq 4: ready 125 ms before start, 125 ms of it with an idle slot in group 286
- [cross_source_blocking] source 12719 seq 9: ready 150 ms before start, 150 ms of it with an idle slot in group 401
- [cross_source_blocking] source 11611 seq 8: ready 150 ms before start, 150 ms of it with an idle slot in group 377
- [cross_source_blocking] source 5565 seq 8: ready 149 ms before start, 149 ms of it with an idle slot in group 191
- [cross_source_blocking] source 14234 seq 21: ready 220 ms before start, 120 ms of it with an idle slot in group 456
- [cross_source_blocking] source 3180 seq 13: ready 229 ms before start, 141 ms of it with an idle slot in group 105
- [cross_source_blocking] source 8682 seq 8: ready 245 ms before start, 126 ms of it with an idle slot in group 301
- [cross_source_blocking] source 7559 seq 18: ready 254 ms before start, 152 ms of it with an idle slot in group 260
- [cross_source_blocking] source 7555 seq 15: ready 253 ms before start, 151 ms of it with an idle slot in group 260
- [cross_source_blocking] source 3781 seq 13: ready 225 ms before start, 116 ms of it with an idle slot in group 127
- [cross_source_blocking] source 3774 seq 16: ready 285 ms before start, 133 ms of it with an idle slot in group 127
- [cross_source_blocking] source 7557 seq 17: ready 284 ms before start, 167 ms of it with an idle slot in group 260
- [cross_source_blocking] source 562 seq 3: ready 298 ms before start, 124 ms of it with an idle slot in group 16
- [cross_source_blocking] source 3164 seq 10: ready 237 ms before start, 127 ms of it with an idle slot in group 105
- [cross_source_blocking] source 461 seq 4: ready 302 ms before start, 128 ms of it with an idle slot in group 16

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9167 | 9167 | 0 | 24117.247 | 52953.087 | 55050.239 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3166 | 3166 | 0 | 24510.463 | 52953.087 | 54001.663 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9047 | 9047 | 0 | 22937.599 | 52166.655 | 54525.951 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5895 | 5895 | 0 | 22413.311 | 52166.655 | 53477.375 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6060 | 6060 | 0 | 22675.455 | 51380.223 | 52690.943 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3080 | 3080 | 0 | 22675.455 | 51380.223 | 52690.943 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2925 | 2925 | 0 | 21364.735 | 51118.079 | 53477.375 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2943 | 2943 | 0 | 21889.023 | 51118.079 | 52690.943 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5948 | 5948 | 0 | 23461.887 | 51118.079 | 52953.087 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3085 | 3085 | 0 | 21889.023 | 50855.935 | 52166.655 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5603 | 5603 | 0 | 22544.383 | 50855.935 | 53215.231 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5796 | 5796 | 0 | 21233.663 | 50855.935 | 52428.799 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2840 | 2840 | 0 | 22544.383 | 50331.647 | 51904.511 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11783 | 11783 | 0 | 22806.527 | 50331.647 | 51904.511 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5532 | 5532 | 0 | 20840.447 | 49807.359 | 51642.367 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2900 | 2900 | 0 | 21364.735 | 49807.359 | 51380.223 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11063 | 11063 | 0 | 21889.023 | 49545.215 | 50855.935 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2767 | 2767 | 0 | 19922.943 | 49283.071 | 50855.935 |
| t-197 | 42 | 2 | 128.441 | 87.485 | 725 | 0 | 5319 | 5319 | 0 | 19005.439 | 49020.927 | 51380.223 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 2759 | 2759 | 0 | 19922.943 | 49020.927 | 49807.359 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2811 | 2811 | 0 | 21233.663 | 49020.927 | 51904.511 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5494 | 5494 | 0 | 22020.095 | 48758.783 | 51642.367 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20480 | 20480 | 0 | 22151.167 | 48758.783 | 50855.935 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2705 | 2705 | 0 | 20185.087 | 48496.639 | 49807.359 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2855 | 2855 | 0 | 18743.295 | 48496.639 | 49545.215 |
