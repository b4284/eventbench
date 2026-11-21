# H02-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.499s, closing 2.802s, drain 22.018s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 498120 | 498120 | 6703.682 | 3541.49 | 19509.4 | 0 | 1829 | 1829 | 283 | 2514 | 71237 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 329277 | 33115.144 | 38502.399 | 51838.975 | 59310.079 | 61898.751 | 65142.783 |
| e2e_from_send | 329277 | 27078.581 | 34308.095 | 50003.967 | 57901.055 | 60817.407 | 63045.631 |
| producer_lag | 329277 | 6036.591 | 1742.847 | 24117.247 | 25346.047 | 26460.159 | 26836.991 |
| publish_call | 498120 | 225.81 | 124.543 | 234.879 | 454.143 | 23199.743 | 23248.895 |
| queue_wait | 329277 | 26957.467 | 34209.791 | 49971.199 | 57868.287 | 60784.639 | 62750.719 |
| broker_dwell | 329277 | 26700.592 | 33849.343 | 49840.127 | 57737.215 | 60686.335 | 62717.951 |
| receive_call | 450121 | 26.359 | 7.259 | 59.487 | 340.479 | 477.951 | 545.791 |
| ctx_load | 329277 | 9.106 | 5.603 | 18.607 | 62.079 | 134.527 | 241.151 |
| process | 329277 | 10.752 | 0.002 | 0.004 | 0.061 | 2625.535 | 3039.231 |
| ctx_commit | 329277 | 101.153 | 50.335 | 271.615 | 479.487 | 1103.871 | 1205.247 |
| ack | 329259 | 0.008 | 0.004 | 0.007 | 0.045 | 0.606 | 61.023 |
| service | 329277 | 121.094 | 59.103 | 288.511 | 562.687 | 2748.415 | 4042.751 |
| e2e_large | 183 | 9836.471 | 4886.527 | 27934.719 | 30113.791 | 32866.303 | 32866.303 |
| ready_wait | 329277 | 1368.791 | 27.135 | 1458.175 | 39223.295 | 45449.215 | 56688.639 |
| blocked | 329277 | 868.615 | 18.895 | 521.983 | 26148.863 | 30998.527 | 34504.703 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3541.49 | 10582.1 |
| applied_total | 184203.918 | 252076 |
| bench_cpu_cores | 1.7 | 3.713 |
| bench_cpu_pct | 7.083 | 15.47 |
| bench_gc_gen0 | 1.163 | 3 |
| bench_gc_gen1 | 0.918 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1098.333 | 1510 |
| bench_mem_mb | 1774.653 | 2077.6 |
| bench_threadpool_queue | 36.469 | 409 |
| bench_threadpool_threads | 43.776 | 44 |
| broker_backlog | 157424.429 | 202392 |
| ctr_eb_dc1a_cpu_pct | 682.287 | 794.06 |
| ctr_eb_dc1a_mem_mb | 7386.214 | 7873.5 |
| ctr_eb_dc1b_cpu_pct | 45.12 | 53.34 |
| ctr_eb_dc1b_mem_mb | 7437.71 | 8212.1 |
| ctr_eb_dc2a_cpu_pct | 25.775 | 106.37 |
| ctr_eb_dc2a_mem_mb | 7362.398 | 8523.9 |
| ctr_eb_dc2b_cpu_pct | 73.501 | 990.74 |
| ctr_eb_dc2b_mem_mb | 3698.922 | 5143.2 |
| ctr_eb_etcd_dc1_cpu_pct | 1.508 | 1.78 |
| ctr_eb_etcd_dc1_mem_mb | 35.538 | 41.2 |
| ctr_eb_etcd_dc2_cpu_pct | 1.471 | 2.26 |
| ctr_eb_etcd_dc2_mem_mb | 28.573 | 32.3 |
| ctr_eb_etcd_w_cpu_pct | 1.587 | 3.09 |
| ctr_eb_etcd_w_mem_mb | 63.608 | 65.1 |
| ctr_eb_haproxy_cpu_pct | 50.093 | 103.46 |
| ctr_eb_haproxy_mem_mb | 30.161 | 32.1 |
| ctr_eb_mosquitto_cpu_pct | 21.489 | 43.99 |
| ctr_eb_mosquitto_mem_mb | 5.694 | 16.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 8.908 | 9 |
| ctr_eb_postgres_cpu_pct | 0.788 | 2.11 |
| ctr_eb_postgres_mem_mb | 1835.361 | 1853.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 37.38 | 990.4 |
| host_cpu_pct | 37.303 | 81.02 |
| in_flight | 162199.837 | 209722 |
| nacks_per_s | 37.38 | 990.4 |
| octopus_dispatcher_cores | 0.756 | 1.46 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 551242.531 | 756376 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 148546.163 | 200681 |
| octopus_mqtt_sent_total | 111933.633 | 160961 |
| octopus_publish_retries_total | 9472.98 | 23972 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.837 | 24 |
| pg_active_loads | 14.408 | 21 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 35.408 | 73 |
| pg_conn_idle | 232.449 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.02 | 1 |
| pg_conn_total | 267.857 | 269 |
| pg_db_size_mb | 1424.006 | 1591 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 8.65 | 60.87 |
| pg_replica_replay_lag_ms | 85.296 | 792 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16602.747 | 19644.8 |
| pg_tup_del_per_s | 26865.753 | 29454.1 |
| pg_tup_fetch_per_s | 539897.782 | 653940.4 |
| pg_tup_ins_per_s | 51032.478 | 57066.4 |
| pg_tup_upd_per_s | 19017.771 | 21463 |
| pg_wal_mb_per_s | 87.03 | 105.77 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6703.682 | 17042 |
| produced_total | 346403.755 | 461798 |
| publish_mb_per_s | 24.028 | 71.99 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 80,224 records are Completed |
| record_counts | pass | 80,224 records found, 80,224 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 283 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 498,120 events applied by workers, 498,120 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 71,237 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26149 ms, max 34474 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3331 seq 5: ready 121 ms before start, 121 ms of it with an idle slot in group 113
- [cross_source_blocking] source 2979 seq 5: ready 121 ms before start, 121 ms of it with an idle slot in group 98
- [cross_source_blocking] source 5067 seq 4: ready 106 ms before start, 106 ms of it with an idle slot in group 177
- [cross_source_blocking] source 13020 seq 9: ready 136 ms before start, 136 ms of it with an idle slot in group 414
- [cross_source_blocking] source 13731 seq 7: ready 124 ms before start, 124 ms of it with an idle slot in group 437
- [cross_source_blocking] source 15811 seq 7: ready 158 ms before start, 158 ms of it with an idle slot in group 495
- [cross_source_blocking] source 14877 seq 7: ready 136 ms before start, 136 ms of it with an idle slot in group 467
- [cross_source_blocking] source 9528 seq 7: ready 146 ms before start, 146 ms of it with an idle slot in group 331
- [cross_source_blocking] source 14072 seq 7: ready 146 ms before start, 146 ms of it with an idle slot in group 448
- [cross_source_blocking] source 4523 seq 6: ready 150 ms before start, 106 ms of it with an idle slot in group 157
- [cross_source_blocking] source 6357 seq 4: ready 106 ms before start, 106 ms of it with an idle slot in group 217
- [cross_source_blocking] source 8475 seq 16: ready 136 ms before start, 136 ms of it with an idle slot in group 289
- [cross_source_blocking] source 13213 seq 12: ready 149 ms before start, 149 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9533 seq 5: ready 152 ms before start, 152 ms of it with an idle slot in group 331
- [cross_source_blocking] source 14629 seq 5: ready 150 ms before start, 150 ms of it with an idle slot in group 462
- [cross_source_blocking] source 9685 seq 10: ready 153 ms before start, 153 ms of it with an idle slot in group 336
- [cross_source_blocking] source 5868 seq 4: ready 146 ms before start, 146 ms of it with an idle slot in group 203
- [cross_source_blocking] source 6812 seq 11: ready 146 ms before start, 146 ms of it with an idle slot in group 232
- [cross_source_blocking] source 14156 seq 6: ready 146 ms before start, 145 ms of it with an idle slot in group 451
- [cross_source_blocking] source 14883 seq 4: ready 136 ms before start, 136 ms of it with an idle slot in group 467

## Error samples

- t-314#0 src=9105 seq=38 op=Update rec=39105677230086 ev=4: NpgsqlException: Exception while reading from stream
- t-285#0 src=8411 seq=11 op=Get rec=36124969926658 ev=4: NpgsqlException: Exception while reading from stream
- t-314#0 src=9098 seq=38 op=Update rec=39075612459014 ev=1: NpgsqlException: Exception while reading from stream
- t-494#0 src=15797 seq=10 op=Get rec=67847598374913 ev=8: NpgsqlException: Exception while reading from stream
- t-242#0 src=7131 seq=16 op=Complete rec=30627411787779 ev=2: NpgsqlException: Exception while reading from stream
- t-493#0 src=15762 seq=25 op=Update rec=67697274519556 ev=3: NpgsqlException: Exception while reading from stream
- t-238#0 src=7046 seq=29 op=Update rec=30262339567621 ev=1: NpgsqlException: Exception while reading from stream
- t-369#0 src=11338 seq=33 op=Complete rec=48696339202049 ev=8: NpgsqlException: Exception while reading from stream
- t-319L#0 src=9283 seq=3 op=Update rec=39870181408769 ev=1: NpgsqlException: Exception while reading from stream
- t-354#0 src=10860 seq=10 op=Update rec=46643344834561 ev=3: NpgsqlException: Exception while reading from stream
- t-340#1 src=9825 seq=5 op=Get rec=42198053683201 ev=3: NpgsqlException: Exception while reading from stream
- t-052#0 src=1834 seq=8 op=Update rec=7876970020866 ev=4: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3440 seq=4 op=Update rec=14774687498241 ev=1: NpgsqlException: Exception while reading from stream
- t-348#2 src=10575 seq=11 op=Update rec=45419279155201 ev=8: NpgsqlException: Exception while reading from stream
- t-392#0 src=12349 seq=8 op=Get rec=53038551138305 ev=3: NpgsqlException: Exception while reading from stream
- t-170#0 src=4941 seq=6 op=Update rec=21221433409537 ev=5: NpgsqlException: Exception while reading from stream
- t-127#0 src=3780 seq=26 op=Update rec=16234976378883 ev=1: NpgsqlException: Exception while reading from stream
- t-334#0 src=9661 seq=30 op=Get rec=41493679046659 ev=4: NpgsqlException: Exception while reading from stream
- t-367#0 src=11284 seq=25 op=Complete rec=48464410968069 ev=3: NpgsqlException: Exception while reading from stream
- t-494#0 src=15776 seq=17 op=Update rec=67757404061699 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1780 | 1780 | 0 | 46661.631 | 63176.703 | 65273.855 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3889 | 3889 | 0 | 46661.631 | 63176.703 | 64487.423 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1858 | 1858 | 0 | 45875.199 | 62914.559 | 64225.279 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3879 | 3879 | 0 | 46661.631 | 62652.415 | 63700.991 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3676 | 3676 | 0 | 46923.775 | 62390.271 | 63963.135 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3643 | 3643 | 0 | 46661.631 | 61865.983 | 63963.135 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1789 | 1789 | 0 | 45875.199 | 61603.839 | 62652.415 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3625 | 3625 | 0 | 45613.055 | 61603.839 | 63700.991 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1907 | 1907 | 0 | 46923.775 | 61603.839 | 62128.127 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1787 | 1787 | 0 | 45350.911 | 61341.695 | 61865.983 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7263 | 7263 | 0 | 46137.343 | 61341.695 | 62914.559 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1649 | 1649 | 0 | 46137.343 | 61341.695 | 61603.839 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5505 | 5505 | 0 | 46137.343 | 61341.695 | 62652.415 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1790 | 1790 | 0 | 45875.199 | 61341.695 | 62128.127 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5317 | 5317 | 0 | 46137.343 | 61341.695 | 62914.559 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7430 | 7430 | 0 | 45613.055 | 61341.695 | 62652.415 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1798 | 1798 | 0 | 46923.775 | 61079.551 | 62652.415 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3458 | 3458 | 0 | 45350.911 | 61079.551 | 62128.127 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1793 | 1793 | 0 | 45613.055 | 61079.551 | 62128.127 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3352 | 3352 | 0 | 45088.767 | 60817.407 | 62128.127 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1584 | 1584 | 0 | 44826.623 | 60555.263 | 61865.983 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5035 | 5035 | 0 | 44564.479 | 60555.263 | 61341.695 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5120 | 5120 | 0 | 45350.911 | 60555.263 | 63438.847 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5363 | 5363 | 0 | 45350.911 | 60293.119 | 61603.839 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1674 | 1674 | 0 | 45613.055 | 60293.119 | 62390.271 |
