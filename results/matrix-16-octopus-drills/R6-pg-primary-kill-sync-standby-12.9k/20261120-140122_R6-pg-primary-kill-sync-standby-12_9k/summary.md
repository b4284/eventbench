# R6-pg-primary-kill-sync-standby-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.283s, closing 2.754s, drain 11.198s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 809819 | 809819 | 12747.249 | 11894.904 | 18361.7 | 0 | 8021 | 8021 | 426 | 4229 | 294986 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 637963 | 2381.226 | 813.567 | 6725.631 | 16809.983 | 30588.927 | 36503.551 |
| e2e_from_send | 637963 | 2259.758 | 620.543 | 6553.599 | 16728.063 | 30408.703 | 36503.551 |
| producer_lag | 638093 | 121.444 | 2.903 | 498.943 | 1186.815 | 1505.279 | 1905.663 |
| publish_call | 809819 | 77.867 | 55.295 | 144.255 | 485.375 | 1060.863 | 1828.863 |
| queue_wait | 637963 | 2174.984 | 517.375 | 6471.679 | 16662.527 | 30375.935 | 35848.191 |
| broker_dwell | 637963 | 2102.388 | 433.407 | 6381.567 | 16580.607 | 30261.247 | 35454.975 |
| receive_call | 723919 | 32.557 | 9.375 | 87.679 | 337.663 | 479.231 | 517.887 |
| ctx_load | 637963 | 7.802 | 5.027 | 17.423 | 42.847 | 69.567 | 366.079 |
| process | 637963 | 10.919 | 0.002 | 0.004 | 0.048 | 2625.535 | 3004.415 |
| ctx_commit | 637963 | 66.006 | 49.183 | 120.895 | 384.511 | 986.623 | 1015.295 |
| ack | 636934 | 0.006 | 0.003 | 0.005 | 0.027 | 0.479 | 40.287 |
| service | 637963 | 84.776 | 57.631 | 133.119 | 648.703 | 2703.359 | 3792.895 |
| e2e_large | 326 | 3054.548 | 2889.727 | 4325.375 | 7520.255 | 8798.207 | 8798.207 |
| ready_wait | 637963 | 387.344 | 156.543 | 1092.607 | 2299.903 | 3600.383 | 34373.631 |
| blocked | 637963 | 173.196 | 79.935 | 470.527 | 907.263 | 1416.191 | 14917.631 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11894.904 | 17080.4 |
| applied_total | 440782.082 | 717234 |
| bench_cpu_cores | 3.824 | 5.023 |
| bench_cpu_pct | 15.933 | 20.93 |
| bench_gc_gen0 | 2.898 | 5 |
| bench_gc_gen1 | 2.612 | 5 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1001.114 | 1275.7 |
| bench_mem_mb | 1493.155 | 1625.6 |
| bench_threadpool_queue | 20.98 | 156 |
| bench_threadpool_threads | 43.102 | 44 |
| broker_backlog | 26552.959 | 56156 |
| ctr_eb_mosquitto_cpu_pct | 51.243 | 63.98 |
| ctr_eb_mosquitto_mem_mb | 7.916 | 13.8 |
| ctr_eb_pg_standby_cpu_pct | 515.573 | 1010.63 |
| ctr_eb_pg_standby_mem_mb | 3492.333 | 5272.9 |
| ctr_eb_postgres_cpu_pct | 1052.08 | 1117.5 |
| ctr_eb_postgres_mem_mb | 7015.918 | 7346.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 163.388 | 6368.1 |
| host_cpu_pct | 69.846 | 81.71 |
| in_flight | 25424.776 | 54391 |
| nacks_per_s | 163.388 | 6368.1 |
| octopus_dispatcher_cores | 1.59 | 1.73 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 923182.429 | 1533206 |
| octopus_mqtt_received_total | 186813 | 307166 |
| octopus_mqtt_sent_total | 275385.122 | 463554 |
| octopus_publish_retries_total | 4310.286 | 7543 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.245 | 19 |
| pg_active_loads | 12.388 | 184 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 64.102 | 244 |
| pg_conn_idle | 214.163 | 270 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.939 | 21 |
| pg_conn_total | 278.265 | 287 |
| pg_db_size_mb | 3958.863 | 5353.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 2.369 | 9.2 |
| pg_replica_replay_lag_ms | 16.171 | 66.6 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 15.844 | 53.75 |
| pg_tps | -128355.949 | 46734.6 |
| pg_tup_del_per_s | -280217.139 | 47921.8 |
| pg_tup_fetch_per_s | -2653867.524 | 647470 |
| pg_tup_ins_per_s | -361112.71 | 60378.8 |
| pg_tup_upd_per_s | -274645.496 | 51088.2 |
| pg_wal_mb_per_s | -692.653 | 191.48 |
| produced_per_s | 12747.249 | 18985.9 |
| produced_total | 466206.857 | 771341 |
| publish_mb_per_s | 43.94 | 69.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,057 records are Completed |
| record_counts | pass | 117,057 records found, 117,057 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 426 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 809,819 events applied by workers, 809,819 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 294,986 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 907 ms, max 14916 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3599 seq 18: ready 124 ms before start, 112 ms of it with an idle slot in group 121
- [cross_source_blocking] source 3531 seq 17: ready 115 ms before start, 102 ms of it with an idle slot in group 121
- [cross_source_blocking] source 14125 seq 12: ready 147 ms before start, 134 ms of it with an idle slot in group 450
- [cross_source_blocking] source 14093 seq 22: ready 141 ms before start, 128 ms of it with an idle slot in group 450
- [cross_source_blocking] source 3540 seq 14: ready 105 ms before start, 105 ms of it with an idle slot in group 121
- [cross_source_blocking] source 3545 seq 12: ready 137 ms before start, 125 ms of it with an idle slot in group 121
- [cross_source_blocking] source 11648 seq 16: ready 186 ms before start, 144 ms of it with an idle slot in group 378
- [cross_source_blocking] source 14102 seq 17: ready 172 ms before start, 141 ms of it with an idle slot in group 450
- [cross_source_blocking] source 11518 seq 26: ready 139 ms before start, 105 ms of it with an idle slot in group 374
- [cross_source_blocking] source 14098 seq 17: ready 119 ms before start, 101 ms of it with an idle slot in group 450
- [cross_source_blocking] source 11652 seq 16: ready 195 ms before start, 148 ms of it with an idle slot in group 378
- [cross_source_blocking] source 3536 seq 21: ready 120 ms before start, 120 ms of it with an idle slot in group 121
- [cross_source_blocking] source 3591 seq 10: ready 128 ms before start, 128 ms of it with an idle slot in group 121
- [cross_source_blocking] source 14106 seq 7: ready 168 ms before start, 136 ms of it with an idle slot in group 450
- [cross_source_blocking] source 14946 seq 25: ready 206 ms before start, 146 ms of it with an idle slot in group 468
- [cross_source_blocking] source 4215 seq 11: ready 135 ms before start, 135 ms of it with an idle slot in group 140
- [cross_source_blocking] source 11642 seq 15: ready 230 ms before start, 162 ms of it with an idle slot in group 378
- [cross_source_blocking] source 14129 seq 13: ready 158 ms before start, 126 ms of it with an idle slot in group 450
- [cross_source_blocking] source 8032 seq 21: ready 261 ms before start, 202 ms of it with an idle slot in group 271
- [cross_source_blocking] source 3566 seq 18: ready 155 ms before start, 143 ms of it with an idle slot in group 121

## Error samples

- t-246#0 src=7240 seq=132 op=Complete rec=31095563223058 ev=12: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-206#0 src=6009 seq=24 op=Complete rec=25808458481668 ev=1: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-121#2 src=3581 seq=62 op=Update rec=15380277886983 ev=5: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-065#4 src=2212 seq=73 op=Update rec=9500467658763 ev=4: NpgsqlException: No suitable host was found.
- t-197#0 src=5740 seq=47 op=Complete rec=24653112279048 ev=5: NpgsqlException: No suitable host was found.
- t-397#0 src=12508 seq=9 op=Update rec=53721450938370 ev=2: NpgsqlException: No suitable host was found.
- t-469#1 src=15012 seq=37 op=Update rec=64476049047555 ev=4: NpgsqlException: No suitable host was found.
- t-147#0 src=4316 seq=81 op=Get rec=18537078849546 ev=6: NpgsqlException: No suitable host was found.
- t-383#4 src=11822 seq=42 op=Create rec=50775103373320 ev=0: NpgsqlException: No suitable host was found.
- t-065#4 src=2240 seq=71 op=Get rec=9620726743048 ev=9: NpgsqlException: No suitable host was found.
- t-235#0 src=6940 seq=49 op=Get rec=29807073034246 ev=6: NpgsqlException: No suitable host was found.
- t-460#0 src=14440 seq=15 op=Update rec=62019327754242 ev=4: NpgsqlException: No suitable host was found.
- t-253#0 src=7380 seq=42 op=Update rec=31696858644487 ev=5: NpgsqlException: No suitable host was found.
- t-417#0 src=13168 seq=16 op=Get rec=56556129353730 ev=6: NpgsqlException: No suitable host was found.
- t-065#0 src=2268 seq=95 op=Update rec=9740985827342 ev=3: NpgsqlException: No suitable host was found.
- t-483#2 src=15544 seq=16 op=Update rec=66760971649028 ev=4: NpgsqlException: No suitable host was found.
- t-049#0 src=1628 seq=24 op=Update rec=6992206757890 ev=2: NpgsqlException: No suitable host was found.
- t-041#1 src=1302 seq=106 op=Update rec=5592047419406 ev=7: NpgsqlException: No suitable host was found.
- t-402#0 src=12728 seq=21 op=Complete rec=54666343743494 ev=3: NpgsqlException: No suitable host was found.
- t-347#0 src=10413 seq=25 op=Get rec=44723494453252 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2864 | 2864 | 0 | 6160.383 | 33554.431 | 34603.007 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2993 | 2993 | 0 | 3014.655 | 32374.783 | 34865.151 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3036 | 3036 | 0 | 3522.559 | 31981.567 | 36700.159 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6041 | 6041 | 0 | 3899.391 | 31588.351 | 35651.583 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12191 | 12191 | 0 | 3293.183 | 31588.351 | 33816.575 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12054 | 12054 | 0 | 3014.655 | 31457.279 | 34340.863 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13723 | 13723 | 0 | 2277.375 | 29360.127 | 34603.007 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4936 | 4936 | 0 | 1179.647 | 25821.183 | 31326.207 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5274 | 5274 | 0 | 1728.511 | 25296.895 | 32112.639 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9132 | 9132 | 0 | 6258.687 | 23330.815 | 29097.983 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4534 | 4534 | 0 | 1523.711 | 21626.879 | 24117.247 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6252 | 6252 | 0 | 5734.399 | 19791.871 | 24248.319 |
| t-196 | 21 | 1 | 94.353 | 33.348 | 2244 | 0 | 2068 | 2068 | 0 | 552.959 | 19005.439 | 20971.519 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4590 | 4590 | 0 | 647.167 | 18219.007 | 20578.303 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6300 | 6300 | 0 | 5472.255 | 18087.935 | 21364.735 |
| t-041 | 34 | 3 | 194.475 | 109.547 | 2732 | 0 | 6543 | 6543 | 0 | 610.303 | 17563.647 | 23068.671 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2888 | 2888 | 0 | 4177.919 | 17432.575 | 21102.591 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2975 | 2975 | 0 | 4521.983 | 17301.503 | 20971.519 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5889 | 5889 | 0 | 4751.359 | 17301.503 | 23068.671 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2667 | 2667 | 0 | 4014.079 | 17170.431 | 20578.303 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2528 | 2528 | 0 | 3686.399 | 16908.287 | 20447.231 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8738 | 8738 | 0 | 4014.079 | 16908.287 | 20709.375 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5865 | 5865 | 0 | 4259.839 | 16580.607 | 20185.087 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3008 | 3008 | 0 | 3653.631 | 16449.535 | 19660.799 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2883 | 2883 | 0 | 4227.071 | 16187.391 | 19791.871 |
