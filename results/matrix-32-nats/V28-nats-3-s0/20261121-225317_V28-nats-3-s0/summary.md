# V28-nats-3-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 85.897s, closing 1.267s, drain 23.05s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 851819 | 851819 | 14165.604 | 12812.286 | 24056.6 | 0 | 0 | 0 | 51 | 4110 | 360670 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 690862 | 3436.461 | 2033.663 | 8912.895 | 15171.583 | 19693.567 | 27230.207 |
| e2e_from_send | 690862 | 2924.726 | 1035.775 | 8740.863 | 15056.895 | 19480.575 | 27230.207 |
| producer_lag | 690862 | 511.736 | 122.303 | 1528.831 | 2508.799 | 2957.311 | 3102.719 |
| publish_call | 851819 | 122.19 | 95.231 | 258.047 | 452.863 | 698.879 | 1134.591 |
| queue_wait | 690862 | 2845.519 | 939.007 | 8675.327 | 14991.359 | 19415.039 | 26968.063 |
| broker_dwell | 690862 | 2753.883 | 844.287 | 8609.791 | 14909.439 | 19349.503 | 26836.991 |
| receive_call | 744499 | 23.408 | 11.063 | 56.671 | 201.471 | 417.535 | 573.439 |
| ctx_load | 690862 | 18.39 | 11.919 | 39.679 | 99.647 | 184.959 | 358.143 |
| process | 690862 | 9.948 | 0.002 | 0.004 | 0.066 | 2611.199 | 3149.823 |
| ctx_commit | 690862 | 50.579 | 40.351 | 104.383 | 191.871 | 322.303 | 529.919 |
| ack | 690810 | 0.014 | 0.004 | 0.007 | 0.057 | 2.028 | 65.791 |
| service | 690862 | 79.207 | 60.223 | 132.991 | 265.215 | 2680.831 | 3368.959 |
| e2e_large | 44 | 3226.624 | 3112.959 | 4714.495 | 6021.119 | 6021.119 | 6021.119 |
| ready_wait | 690862 | 241.716 | 145.279 | 576.511 | 1351.679 | 2150.399 | 11460.607 |
| blocked | 690862 | 173.042 | 109.247 | 414.719 | 868.351 | 1242.111 | 10223.615 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12812.286 | 16637.3 |
| applied_total | 445663.388 | 749795 |
| bench_cpu_cores | 4.025 | 5.159 |
| bench_cpu_pct | 16.77 | 21.5 |
| bench_gc_gen0 | 1.592 | 3 |
| bench_gc_gen1 | 0.449 | 2 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 1047.306 | 1431.2 |
| bench_mem_mb | 1584.58 | 2019.8 |
| bench_threadpool_queue | 229.837 | 1720 |
| bench_threadpool_threads | 39.959 | 44 |
| broker_backlog | 43614.265 | 83972 |
| ctr_eb_mssql_cpu_pct | 4.109 | 5.29 |
| ctr_eb_mssql_mem_mb | 4885.733 | 4888.5 |
| ctr_eb_mysql_cpu_pct | 1.091 | 1.26 |
| ctr_eb_mysql_mem_mb | 5964.533 | 5968.1 |
| ctr_eb_nats_octopus_cpu_pct | 118.599 | 123.57 |
| ctr_eb_nats_octopus_mem_mb | 45.496 | 66.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 31.378 | 33.31 |
| ctr_eb_pgbouncer_2_mem_mb | 12.845 | 13.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.604 | 32.61 |
| ctr_eb_pgbouncer_3_mem_mb | 15.584 | 16.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 31.546 | 33.89 |
| ctr_eb_pgbouncer_4_mem_mb | 16.263 | 17.1 |
| ctr_eb_pgbouncer_cpu_pct | 31.438 | 34.19 |
| ctr_eb_pgbouncer_mem_mb | 16.345 | 17.3 |
| ctr_eb_postgres_cpu_pct | 1008.385 | 1096.71 |
| ctr_eb_postgres_mem_mb | 5667.614 | 5940.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.827 | 86.21 |
| in_flight | 35249.735 | 69528 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.03 | 2.12 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 911744 | 1577659 |
| octopus_missing_reports_total | 45488.571 | 74293 |
| octopus_mqtt_received_total | 174770.367 | 297243 |
| octopus_mqtt_sent_total | 315795.755 | 527453 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13.735 | 26 |
| pg_active_loads | 12.02 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 45.122 | 82 |
| pg_conn_idle | 215.469 | 244 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.041 | 28 |
| pg_conn_total | 260.592 | 262 |
| pg_db_size_mb | 3669.484 | 4291.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27288.776 | 32267.2 |
| pg_tup_del_per_s | 35300.88 | 40650.6 |
| pg_tup_fetch_per_s | 317820.818 | 358955.9 |
| pg_tup_ins_per_s | 48048.033 | 56266.7 |
| pg_tup_upd_per_s | 60465.49 | 69657.8 |
| pg_wal_mb_per_s | 134.197 | 263.18 |
| pgb_cl_active | 481.633 | 509 |
| pgb_cl_waiting | 20.02 | 254 |
| pgb_maxwait_ms | 2.309 | 21.96 |
| pgb_sv_active | 28.286 | 48 |
| pgb_sv_idle | 19.714 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14165.604 | 21157.3 |
| produced_total | 480913.122 | 819323 |
| publish_mb_per_s | 26.885 | 40.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 200,562 records are Completed |
| record_counts | pass | 200,562 records found, 109,017 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 373 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 851,819 events applied by workers, 851,819 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 360,670 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 868 ms, max 10218 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14025 seq 12: ready 237 ms before start, 237 ms of it with an idle slot in group 446
- [cross_source_blocking] source 445 seq 14: ready 276 ms before start, 249 ms of it with an idle slot in group 14
- [cross_source_blocking] source 4217 seq 26: ready 235 ms before start, 224 ms of it with an idle slot in group 140
- [cross_source_blocking] source 14241 seq 67: ready 158 ms before start, 158 ms of it with an idle slot in group 456
- [cross_source_blocking] source 6347 seq 27: ready 195 ms before start, 162 ms of it with an idle slot in group 216
- [cross_source_blocking] source 5434 seq 34: ready 172 ms before start, 129 ms of it with an idle slot in group 186
- [cross_source_blocking] source 11507 seq 64: ready 122 ms before start, 105 ms of it with an idle slot in group 374
- [cross_source_blocking] source 5274 seq 28: ready 178 ms before start, 167 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6179 seq 19: ready 168 ms before start, 137 ms of it with an idle slot in group 210
- [cross_source_blocking] source 13813 seq 36: ready 192 ms before start, 184 ms of it with an idle slot in group 438
- [cross_source_blocking] source 8808 seq 20: ready 168 ms before start, 159 ms of it with an idle slot in group 306
- [cross_source_blocking] source 13845 seq 32: ready 169 ms before start, 160 ms of it with an idle slot in group 438
- [cross_source_blocking] source 10226 seq 28: ready 228 ms before start, 206 ms of it with an idle slot in group 344
- [cross_source_blocking] source 8840 seq 26: ready 206 ms before start, 196 ms of it with an idle slot in group 306
- [cross_source_blocking] source 3505 seq 26: ready 152 ms before start, 128 ms of it with an idle slot in group 120
- [cross_source_blocking] source 3613 seq 25: ready 165 ms before start, 126 ms of it with an idle slot in group 122
- [cross_source_blocking] source 10639 seq 30: ready 154 ms before start, 132 ms of it with an idle slot in group 350
- [cross_source_blocking] source 9541 seq 24: ready 179 ms before start, 131 ms of it with an idle slot in group 332
- [cross_source_blocking] source 14120 seq 28: ready 195 ms before start, 135 ms of it with an idle slot in group 450
- [cross_source_blocking] source 10561 seq 29: ready 168 ms before start, 159 ms of it with an idle slot in group 348

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2901 | 2901 | 0 | 6815.743 | 20185.087 | 21757.951 |
| t-348 | 125 | 6 | 136.718 | 286.558 | 1340 | 0 | 17664 | 17664 | 0 | 5242.879 | 20185.087 | 27262.975 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 2969 | 2969 | 0 | 5603.327 | 19922.943 | 24510.463 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2856 | 2856 | 0 | 5013.503 | 19398.655 | 20709.375 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8812 | 8812 | 0 | 5373.951 | 18874.367 | 21102.591 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2931 | 2931 | 0 | 6258.687 | 18743.295 | 21495.807 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17262 | 17262 | 0 | 5734.399 | 18481.151 | 23986.175 |
| t-388 | 78 | 2 | 71.428 | 90.661 | 1205 | 0 | 5733 | 5733 | 0 | 4718.591 | 18087.935 | 21626.879 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 25921 | 25921 | 0 | 5832.703 | 18087.935 | 20971.519 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2843 | 2843 | 0 | 5472.255 | 17956.863 | 20578.303 |
| t-276 | 78 | 1 | 37.161 | 47.91 | 423 | 0 | 3077 | 3077 | 0 | 4095.999 | 17301.503 | 20578.303 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5927 | 5927 | 0 | 4292.607 | 17170.431 | 22675.455 |
| t-168 | 71 | 2 | 75.117 | 87.222 | 155 | 0 | 5584 | 5584 | 0 | 4046.847 | 17170.431 | 21233.663 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 20880 | 20880 | 0 | 5079.039 | 17170.431 | 22806.527 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 16586 | 16586 | 0 | 4816.895 | 16908.287 | 21626.879 |
| t-260 | 30 | 2 | 180.858 | 93.625 | 499 | 0 | 5614 | 5614 | 0 | 5832.703 | 16646.143 | 20971.519 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 19115 | 19115 | 0 | 4325.375 | 16515.071 | 21757.951 |
| t-364 | 66 | 1 | 38.872 | 43.874 | 436 | 0 | 2860 | 2860 | 0 | 4145.151 | 16515.071 | 19791.871 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5733 | 5733 | 0 | 4161.535 | 16449.535 | 20971.519 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11561 | 11561 | 0 | 5210.111 | 16383.999 | 19267.583 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 22434 | 22434 | 0 | 4423.679 | 16121.855 | 19136.511 |
| t-474 | 63 | 1 | 39.975 | 41.235 | 375 | 0 | 2639 | 2639 | 0 | 3571.711 | 16056.319 | 19005.439 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 10835 | 10835 | 0 | 4145.151 | 15728.639 | 17170.431 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16798 | 16798 | 0 | 3932.159 | 15663.103 | 18874.367 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2606 | 2606 | 0 | 3473.407 | 15335.423 | 15990.783 |
