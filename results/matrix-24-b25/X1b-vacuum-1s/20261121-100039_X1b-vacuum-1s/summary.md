# X1b-vacuum-1s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.265s, closing 2.818s, drain 8.308s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 982771 | 982771 | 15597.147 | 14465.243 | 23430 | 0 | 0 | 0 | 449 | 5082 | 482307 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 779477 | 3500.084 | 1511.423 | 9961.471 | 20840.447 | 27377.663 | 35291.135 |
| e2e_from_send | 779477 | 3163.923 | 926.719 | 9347.071 | 20594.687 | 27361.279 | 35291.135 |
| producer_lag | 779477 | 336.159 | 7.891 | 1353.727 | 2193.407 | 2559.999 | 3049.471 |
| publish_call | 982771 | 75.909 | 56.927 | 157.055 | 328.191 | 625.663 | 811.519 |
| queue_wait | 779477 | 3092.763 | 837.119 | 9281.535 | 20529.151 | 27295.743 | 35258.367 |
| broker_dwell | 779477 | 3039.151 | 779.263 | 9224.191 | 20463.615 | 27279.359 | 35192.831 |
| receive_call | 818136 | 36.597 | 13.775 | 92.671 | 336.127 | 477.439 | 792.575 |
| ctx_load | 779477 | 15.893 | 11.015 | 32.575 | 73.343 | 203.519 | 551.423 |
| process | 779477 | 10.793 | 0.002 | 0.004 | 0.078 | 2619.391 | 3180.543 |
| ctx_commit | 779477 | 44.283 | 35.359 | 89.279 | 162.303 | 561.663 | 747.519 |
| ack | 779448 | 0.012 | 0.004 | 0.008 | 0.059 | 1.314 | 75.583 |
| service | 779477 | 71.161 | 51.167 | 114.175 | 275.967 | 2682.879 | 3420.159 |
| e2e_large | 347 | 3044.811 | 2961.407 | 4354.047 | 5865.471 | 6459.391 | 6459.391 |
| ready_wait | 779477 | 384.336 | 228.095 | 955.903 | 2089.983 | 3627.007 | 6090.751 |
| blocked | 779477 | 297.419 | 181.887 | 738.303 | 1485.823 | 2523.135 | 4001.791 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 14465.243 | 18669.1 |
| applied_total | 529401 | 852212 |
| bench_cpu_cores | 5.677 | 7.535 |
| bench_cpu_pct | 23.655 | 31.39 |
| bench_gc_gen0 | 2.061 | 4 |
| bench_gc_gen1 | 0.592 | 2 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 1469.524 | 1934.7 |
| bench_mem_mb | 2249.314 | 2586.4 |
| bench_threadpool_queue | 134.122 | 983 |
| bench_threadpool_threads | 43.898 | 44 |
| broker_backlog | 46036.898 | 71026 |
| ctr_eb_mosquitto_cpu_pct | 65.615 | 74.91 |
| ctr_eb_mosquitto_mem_mb | 8.31 | 20.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.328 | 25.54 |
| ctr_eb_pgbouncer_2_mem_mb | 15.19 | 16.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.627 | 22.35 |
| ctr_eb_pgbouncer_3_mem_mb | 15.384 | 19.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 22.268 | 25.64 |
| ctr_eb_pgbouncer_4_mem_mb | 15.394 | 16.9 |
| ctr_eb_pgbouncer_cpu_pct | 21.081 | 24.38 |
| ctr_eb_pgbouncer_mem_mb | 15.245 | 16.9 |
| ctr_eb_postgres_cpu_pct | 823.828 | 973.34 |
| ctr_eb_postgres_mem_mb | 5729.52 | 5979.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.007 | 80.95 |
| in_flight | 43431.939 | 91492 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.687 | 1.86 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1082387.694 | 1744038 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 226107.735 | 364527 |
| octopus_mqtt_sent_total | 280971.51 | 462116 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.898 | 16 |
| pg_active_loads | 5.531 | 11 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.975 | 100 |
| pg_conn_active | 27.857 | 44 |
| pg_conn_idle | 228.959 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.837 | 25 |
| pg_conn_total | 256.816 | 259 |
| pg_db_size_mb | 3488.853 | 4871.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16386.355 | 21306.9 |
| pg_tup_del_per_s | 27870.829 | 36430.4 |
| pg_tup_fetch_per_s | 219599.72 | 289344.1 |
| pg_tup_ins_per_s | 40307.014 | 55432.4 |
| pg_tup_upd_per_s | 38901.89 | 50718.2 |
| pg_wal_mb_per_s | 106.636 | 211.32 |
| pgb_cl_active | 245.673 | 271 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 10.673 | 44 |
| pgb_sv_idle | 37.327 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15597.147 | 24296.2 |
| produced_total | 572832.939 | 943704 |
| publish_mb_per_s | 50.814 | 82.57 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 136,843 records are Completed |
| record_counts | pass | 136,843 records found, 136,843 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 449 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 982,771 events applied by workers, 982,771 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 482,307 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1486 ms, max 4001 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11512 seq 35: ready 107 ms before start, 107 ms of it with an idle slot in group 374
- [cross_source_blocking] source 7816 seq 13: ready 108 ms before start, 108 ms of it with an idle slot in group 264
- [cross_source_blocking] source 13950 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 443
- [cross_source_blocking] source 4642 seq 15: ready 103 ms before start, 103 ms of it with an idle slot in group 161
- [cross_source_blocking] source 12957 seq 8: ready 157 ms before start, 157 ms of it with an idle slot in group 409
- [cross_source_blocking] source 12632 seq 7: ready 123 ms before start, 123 ms of it with an idle slot in group 397
- [cross_source_blocking] source 4638 seq 14: ready 106 ms before start, 106 ms of it with an idle slot in group 161
- [cross_source_blocking] source 7555 seq 15: ready 146 ms before start, 146 ms of it with an idle slot in group 260
- [cross_source_blocking] source 11510 seq 34: ready 111 ms before start, 111 ms of it with an idle slot in group 374
- [cross_source_blocking] source 6993 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 237
- [cross_source_blocking] source 7009 seq 10: ready 112 ms before start, 112 ms of it with an idle slot in group 237
- [cross_source_blocking] source 12359 seq 7: ready 229 ms before start, 229 ms of it with an idle slot in group 392
- [cross_source_blocking] source 15128 seq 5: ready 229 ms before start, 229 ms of it with an idle slot in group 472
- [cross_source_blocking] source 1071 seq 5: ready 243 ms before start, 243 ms of it with an idle slot in group 32
- [cross_source_blocking] source 3304 seq 11: ready 244 ms before start, 244 ms of it with an idle slot in group 111
- [cross_source_blocking] source 698 seq 6: ready 234 ms before start, 234 ms of it with an idle slot in group 21
- [cross_source_blocking] source 11722 seq 15: ready 220 ms before start, 220 ms of it with an idle slot in group 379
- [cross_source_blocking] source 3072 seq 12: ready 211 ms before start, 211 ms of it with an idle slot in group 103
- [cross_source_blocking] source 15136 seq 5: ready 233 ms before start, 233 ms of it with an idle slot in group 472
- [cross_source_blocking] source 3798 seq 25: ready 237 ms before start, 237 ms of it with an idle slot in group 128

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9043 | 9043 | 0 | 10223.615 | 30277.631 | 34603.007 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5872 | 5872 | 0 | 8912.895 | 28573.695 | 32505.855 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3141 | 3141 | 0 | 8847.359 | 28311.551 | 35389.439 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8933 | 8933 | 0 | 10485.759 | 28180.479 | 34865.151 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2780 | 2780 | 0 | 7897.087 | 27656.191 | 32899.071 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 5980 | 5980 | 0 | 8290.303 | 26607.615 | 32374.783 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5541 | 5541 | 0 | 8355.839 | 26345.471 | 33423.359 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11656 | 11656 | 0 | 8912.895 | 26345.471 | 34340.863 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3039 | 3039 | 0 | 8912.895 | 26214.399 | 29097.983 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2804 | 2804 | 0 | 6651.903 | 25821.183 | 31850.495 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14871 | 14871 | 0 | 8847.359 | 25821.183 | 32899.071 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2861 | 2861 | 0 | 7667.711 | 25690.111 | 30277.631 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5817 | 5817 | 0 | 8781.823 | 25165.823 | 30015.487 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14427 | 14427 | 0 | 9109.503 | 25034.751 | 29753.343 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5431 | 5431 | 0 | 8716.287 | 24641.535 | 27131.903 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5725 | 5725 | 0 | 7307.263 | 24641.535 | 28966.911 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20245 | 20245 | 0 | 7962.623 | 24379.391 | 32505.855 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2912 | 2912 | 0 | 6881.279 | 24248.319 | 29753.343 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 10931 | 10931 | 0 | 8454.143 | 23986.175 | 30670.847 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5458 | 5458 | 0 | 7536.639 | 23461.887 | 29097.983 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2891 | 2891 | 0 | 7897.087 | 23068.671 | 27131.903 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5613 | 5613 | 0 | 7798.783 | 22806.527 | 25821.183 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19411 | 19411 | 0 | 6553.599 | 22806.527 | 27262.975 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2729 | 2729 | 0 | 7012.351 | 22544.383 | 25165.823 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2684 | 2684 | 0 | 7405.567 | 21889.023 | 24248.319 |
