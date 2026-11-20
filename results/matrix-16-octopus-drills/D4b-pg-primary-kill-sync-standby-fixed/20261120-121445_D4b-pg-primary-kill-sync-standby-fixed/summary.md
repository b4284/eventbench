# D4b-pg-primary-kill-sync-standby-fixed — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.27s, closing 2.941s, drain 180.038s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 998894 | 998860 | 15871.565 | 13421.21 | 26123.1 | 0 | 7282 | 7282 | 457 | 5156 | 97270 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 795477 | 6512.952 | 3100.671 | 17580.031 | 30392.319 | 49840.127 | 185335.807 |
| e2e_from_send | 795477 | 6148.447 | 2736.127 | 17170.431 | 29671.423 | 49741.823 | 184680.447 |
| producer_lag | 795508 | 364.497 | 39.647 | 1264.639 | 1681.407 | 1843.199 | 2230.271 |
| publish_call | 998894 | 87.013 | 70.527 | 155.135 | 307.711 | 1225.727 | 1965.055 |
| queue_wait | 795477 | 6038.427 | 2600.959 | 17104.895 | 29605.887 | 49381.375 | 184549.375 |
| broker_dwell | 795477 | 5962.278 | 2527.231 | 17022.975 | 29507.583 | 49348.607 | 184418.303 |
| receive_call | 981682 | 16.98 | 0.005 | 49.407 | 299.263 | 470.015 | 539.647 |
| ctx_load | 795477 | 8.533 | 4.731 | 15.783 | 43.359 | 73.599 | 15007.743 |
| process | 795477 | 10.743 | 0.001 | 0.003 | 0.028 | 2619.391 | 3014.655 |
| ctx_commit | 795477 | 90.716 | 87.039 | 150.143 | 258.559 | 509.695 | 15015.935 |
| ack | 794177 | 0.007 | 0.003 | 0.005 | 0.031 | 0.722 | 60.863 |
| service | 795477 | 110.022 | 95.615 | 161.151 | 383.999 | 2752.511 | 32931.839 |
| e2e_large | 355 | 3174.814 | 2996.223 | 4358.143 | 7409.663 | 9560.063 | 9560.063 |
| ready_wait | 795477 | 454.733 | 262.399 | 1144.831 | 2402.303 | 3813.375 | 35225.599 |
| blocked | 795477 | 58.945 | 3.133 | 132.863 | 1047.551 | 1941.503 | 16105.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13421.21 | 18249.6 |
| applied_total | 489378.592 | 810019 |
| bench_cpu_cores | 4.13 | 5.475 |
| bench_cpu_pct | 17.207 | 22.81 |
| bench_gc_gen0 | 2.959 | 4 |
| bench_gc_gen1 | 2.388 | 4 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1053.492 | 1335.3 |
| bench_mem_mb | 1610.078 | 1746.5 |
| bench_threadpool_queue | 91.796 | 588 |
| bench_threadpool_threads | 42.571 | 44 |
| broker_backlog | 60062.796 | 147667 |
| ctr_eb_mosquitto_cpu_pct | 56.245 | 65.55 |
| ctr_eb_mosquitto_mem_mb | 11.245 | 21 |
| ctr_eb_pg_standby_cpu_pct | 568.714 | 1086.43 |
| ctr_eb_pg_standby_mem_mb | 3868.492 | 5800.2 |
| ctr_eb_postgres_cpu_pct | 1047.981 | 1096.85 |
| ctr_eb_postgres_mem_mb | 4179.564 | 4467 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 151.718 | 6676.7 |
| host_cpu_pct | 73.218 | 84.58 |
| in_flight | 82989 | 148359 |
| nacks_per_s | 151.718 | 6676.7 |
| octopus_dispatcher_cores | 1.579 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 767891.633 | 1791511 |
| octopus_mqtt_received_total | 115163.571 | 252359 |
| octopus_mqtt_sent_total | 201479.367 | 471209 |
| octopus_publish_retries_total | 4116.122 | 7470 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.414 | 18 |
| pg_active_loads | 7.276 | 23 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.991 | 100 |
| pg_conn_active | 46.69 | 68 |
| pg_conn_idle | 227.31 | 256 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.483 | 17 |
| pg_conn_total | 274 | 294 |
| pg_db_size_mb | 4630.1 | 5834.4 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 0 | 0 |
| pg_replica_replay_lag_ms | 1.4 | 1.4 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 29.366 | 63.84 |
| pg_tps | 27754.37 | 42807 |
| pg_tup_del_per_s | 33383.796 | 53273.9 |
| pg_tup_fetch_per_s | 395924.593 | 484013.6 |
| pg_tup_ins_per_s | 46942.841 | 67116.1 |
| pg_tup_upd_per_s | 33086.785 | 45498.4 |
| pg_wal_mb_per_s | 183.953 | 252.9 |
| produced_per_s | 15871.565 | 20088.4 |
| produced_total | 572367.592 | 958378 |
| publish_mb_per_s | 51.58 | 74.32 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15997/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 3 records not Completed |
| record_counts | **FAIL** | 138,689 records found, 138,692 expected; 3 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 998,860 events applied by workers, 998,894 generated |
| broker_drained | **FAIL** | 34 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 97,270 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1048 ms, max 16098 ms) |

Sample mismatches:

- source 2259: record 9702331121691 left in status Open (version 6)
- source 2229: record 9573482102811 left in status Open (version 8)
- source 2219: record 9530532429858 left in status Open (version 8)
- source 2219: cursor at seq 268, expected 271 (3 events not applied)
- source 2219: 34 records (33 completed), expected 34 (34 completed)
- source 2229: cursor at seq 243, expected 257 (14 events not applied)
- source 2229: 27 records (26 completed), expected 28 (28 completed)
- source 2259: cursor at seq 223, expected 240 (17 events not applied)
- source 2259: 28 records (27 completed), expected 30 (30 completed)

## Cross-source blocking samples

- [cross_source_blocking] source 12191 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 388
- [cross_source_blocking] source 1007 seq 5: ready 155 ms before start, 155 ms of it with an idle slot in group 30
- [cross_source_blocking] source 3798 seq 25: ready 150 ms before start, 150 ms of it with an idle slot in group 128
- [cross_source_blocking] source 935 seq 8: ready 134 ms before start, 134 ms of it with an idle slot in group 27
- [cross_source_blocking] source 2335 seq 20: ready 112 ms before start, 112 ms of it with an idle slot in group 67
- [cross_source_blocking] source 415 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 14
- [cross_source_blocking] source 6997 seq 7: ready 119 ms before start, 117 ms of it with an idle slot in group 237
- [cross_source_blocking] source 8763 seq 17: ready 162 ms before start, 162 ms of it with an idle slot in group 305
- [cross_source_blocking] source 14084 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 449
- [cross_source_blocking] source 12804 seq 5: ready 112 ms before start, 112 ms of it with an idle slot in group 404
- [cross_source_blocking] source 3801 seq 20: ready 107 ms before start, 107 ms of it with an idle slot in group 128
- [cross_source_blocking] source 15632 seq 13: ready 112 ms before start, 112 ms of it with an idle slot in group 486
- [cross_source_blocking] source 12185 seq 8: ready 107 ms before start, 107 ms of it with an idle slot in group 388
- [cross_source_blocking] source 10 seq 10: ready 150 ms before start, 150 ms of it with an idle slot in group 0
- [cross_source_blocking] source 5863 seq 8: ready 110 ms before start, 110 ms of it with an idle slot in group 203
- [cross_source_blocking] source 4399 seq 7: ready 161 ms before start, 161 ms of it with an idle slot in group 151
- [cross_source_blocking] source 8221 seq 6: ready 104 ms before start, 104 ms of it with an idle slot in group 277
- [cross_source_blocking] source 9675 seq 4: ready 111 ms before start, 111 ms of it with an idle slot in group 335
- [cross_source_blocking] source 1616 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 48
- [cross_source_blocking] source 6660 seq 21: ready 112 ms before start, 112 ms of it with an idle slot in group 226

## Error samples

- t-056#0 src=1962 seq=17 op=Get rec=8426725834755 ev=5: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-019#1 src=665 seq=55 op=Create rec=2856153251849 ev=0: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-110#0 src=3295 seq=11 op=Get rec=14151917240323 ev=1: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-256#0 src=7403 seq=26 op=Update rec=31795642892290 ev=4: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-390#0 src=12271 seq=31 op=Complete rec=52703543689218 ev=8: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-052#0 src=1864 seq=42 op=Update rec=8005819039752 ev=5: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-361#0 src=11048 seq=30 op=Complete rec=47450798686210 ev=4: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-069#0 src=2376 seq=7 op=Create rec=10204842295298 ev=0: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-230#1 src=6738 seq=47 op=Update rec=28939489640454 ev=6: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-348#2 src=10500 seq=37 op=Update rec=45097156608004 ev=3: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-322#0 src=9327 seq=34 op=Update rec=40059159969798 ev=4: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-019#1 src=670 seq=68 op=Update rec=2877628088330 ev=1: NpgsqlException: No suitable host was found.
- t-019#1 src=648 seq=52 op=Update rec=2783138807817 ev=3: NpgsqlException: No suitable host was found.
- t-019#1 src=662 seq=45 op=Create rec=2843268349957 ev=0: NpgsqlException: No suitable host was found.
- t-052#0 src=1843 seq=27 op=Create rec=7915624726535 ev=0: NpgsqlException: No suitable host was found.
- t-019#1 src=659 seq=54 op=Update rec=2830383448072 ev=6: NpgsqlException: No suitable host was found.
- t-052#0 src=1841 seq=31 op=Update rec=7907034791940 ev=3: NpgsqlException: No suitable host was found.
- t-019#1 src=666 seq=58 op=Get rec=2860448219141 ev=1: NpgsqlException: No suitable host was found.
- t-019#1 src=655 seq=46 op=Update rec=2813203578890 ev=1: NpgsqlException: No suitable host was found.
- t-019#1 src=663 seq=51 op=Update rec=2847563317254 ev=8: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16705 | 16671 | 0 | 22282.239 | 115867.647 | 185597.951 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8283 | 8283 | 0 | 23199.743 | 42729.471 | 44826.623 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2847 | 2847 | 0 | 8781.823 | 40108.031 | 42991.615 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2621 | 2621 | 0 | 10158.079 | 40108.031 | 43253.759 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3188 | 3188 | 0 | 14417.919 | 37748.735 | 39059.455 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11836 | 11836 | 0 | 12386.303 | 37486.591 | 44564.479 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11113 | 11113 | 0 | 11993.087 | 37224.447 | 45613.055 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2928 | 2928 | 0 | 11075.583 | 36700.159 | 39321.599 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5908 | 5908 | 0 | 13041.663 | 35651.583 | 42205.183 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9080 | 9080 | 0 | 13893.631 | 35389.439 | 44564.479 |
| t-046 | 24 | 1 | 105.058 | 42.361 | 3235 | 0 | 2588 | 2588 | 0 | 9568.255 | 35127.295 | 38273.023 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9200 | 9200 | 0 | 14483.455 | 34603.007 | 40370.175 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14654 | 14654 | 0 | 16908.287 | 33292.287 | 51118.079 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5965 | 5965 | 0 | 12058.623 | 32899.071 | 38273.023 |
| t-080 | 56 | 1 | 40.902 | 38.54 | 822 | 0 | 2428 | 2428 | 0 | 6291.455 | 32505.855 | 42991.615 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5529 | 5529 | 0 | 15794.175 | 32505.855 | 35651.583 |
| t-197 | 42 | 2 | 128.441 | 87.485 | 725 | 0 | 5337 | 5337 | 0 | 9633.791 | 32374.783 | 39059.455 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5715 | 5715 | 0 | 13959.167 | 32374.783 | 37748.735 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5624 | 5624 | 0 | 12189.695 | 32112.639 | 34865.151 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 11796.479 | 31981.567 | 37486.591 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6087 | 6087 | 0 | 12189.695 | 31850.495 | 40632.319 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2669 | 2669 | 0 | 7864.319 | 31719.423 | 45350.911 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5543 | 5543 | 0 | 11993.087 | 31588.351 | 42205.183 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3095 | 3095 | 0 | 12124.159 | 31588.351 | 35389.439 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20569 | 20569 | 0 | 12779.519 | 31457.279 | 36175.871 |
