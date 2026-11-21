# V0b-naptime-10s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.201s, closing 2.1s, drain 3.705s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 996930 | 996930 | 15842.453 | 15871.58 | 20683.6 | 0 | 0 | 0 | 457 | 5148 | 296737 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 793573 | 914.897 | 222.207 | 2926.591 | 6971.391 | 9551.871 | 13942.783 |
| e2e_from_send | 793573 | 824.334 | 181.759 | 2746.367 | 6795.263 | 9396.223 | 13910.015 |
| producer_lag | 793573 | 90.564 | 3.263 | 378.623 | 944.127 | 1180.671 | 1708.031 |
| publish_call | 996930 | 57.636 | 38.559 | 131.967 | 275.711 | 414.719 | 602.111 |
| queue_wait | 793573 | 768.965 | 122.815 | 2656.255 | 6742.015 | 9330.687 | 13901.823 |
| broker_dwell | 793573 | 729.873 | 68.543 | 2603.007 | 6684.671 | 9289.727 | 13869.055 |
| receive_call | 842030 | 37.123 | 15.543 | 92.287 | 323.839 | 474.367 | 578.559 |
| ctx_load | 793573 | 12.326 | 8.767 | 25.183 | 61.023 | 115.199 | 262.015 |
| process | 793573 | 10.759 | 0.002 | 0.004 | 0.064 | 2619.391 | 3049.471 |
| ctx_commit | 793573 | 32.113 | 23.423 | 69.951 | 136.703 | 283.647 | 420.607 |
| ack | 793544 | 0.01 | 0.004 | 0.007 | 0.047 | 0.927 | 54.975 |
| service | 793573 | 55.37 | 35.615 | 91.199 | 186.111 | 2666.495 | 3381.247 |
| e2e_large | 355 | 2702.973 | 2650.111 | 3663.871 | 5013.503 | 6045.695 | 6045.695 |
| ready_wait | 793573 | 177.7 | 50.815 | 530.431 | 1208.319 | 2181.119 | 3264.511 |
| blocked | 793573 | 140.412 | 43.871 | 424.703 | 866.815 | 1457.151 | 2385.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15871.58 | 20683.6 |
| applied_total | 564603.939 | 930857 |
| bench_cpu_cores | 5.8 | 6.95 |
| bench_cpu_pct | 24.166 | 28.96 |
| bench_gc_gen0 | 2.082 | 4 |
| bench_gc_gen1 | 0.653 | 2 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1461.08 | 1908.4 |
| bench_mem_mb | 2195.196 | 2516 |
| bench_threadpool_queue | 139.286 | 1003 |
| bench_threadpool_threads | 41.571 | 44 |
| broker_backlog | 16077.367 | 32506 |
| ctr_eb_mosquitto_cpu_pct | 68.279 | 72.97 |
| ctr_eb_mosquitto_mem_mb | 6.867 | 15.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.19 | 23.58 |
| ctr_eb_pgbouncer_2_mem_mb | 15.386 | 16.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.731 | 24.44 |
| ctr_eb_pgbouncer_3_mem_mb | 15.104 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.594 | 23.46 |
| ctr_eb_pgbouncer_4_mem_mb | 15.122 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 20.507 | 23.31 |
| ctr_eb_pgbouncer_mem_mb | 15.498 | 16.6 |
| ctr_eb_postgres_cpu_pct | 800.961 | 962.72 |
| ctr_eb_postgres_mem_mb | 5772.214 | 6080.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 75.254 | 81.35 |
| in_flight | 12448 | 31826 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.625 | 1.74 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1122160.469 | 1833617 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 231944.816 | 375500 |
| octopus_mqtt_sent_total | 310168.041 | 524814 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.694 | 15 |
| pg_active_loads | 3.224 | 13 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.983 | 100 |
| pg_conn_active | 15.653 | 28 |
| pg_conn_idle | 240.939 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.265 | 9 |
| pg_conn_total | 256.592 | 258 |
| pg_db_size_mb | 3780.259 | 4935.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18183.951 | 22917.1 |
| pg_tup_del_per_s | 29222.641 | 41349.8 |
| pg_tup_fetch_per_s | 215405.837 | 250529 |
| pg_tup_ins_per_s | 41021.376 | 55955.8 |
| pg_tup_upd_per_s | 41705.365 | 52953.4 |
| pg_wal_mb_per_s | 113.568 | 206.81 |
| pgb_cl_active | 250.306 | 254 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.306 | 22 |
| pgb_sv_idle | 41.694 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15842.453 | 17667.1 |
| produced_total | 577051.939 | 956312 |
| publish_mb_per_s | 51.575 | 76.92 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,467 records are Completed |
| record_counts | pass | 138,467 records found, 138,467 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 996,930 events applied by workers, 996,930 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 296,737 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 867 ms, max 2384 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3976 seq 20: ready 105 ms before start, 105 ms of it with an idle slot in group 132
- [cross_source_blocking] source 15728 seq 19: ready 101 ms before start, 101 ms of it with an idle slot in group 490
- [cross_source_blocking] source 15681 seq 17: ready 106 ms before start, 106 ms of it with an idle slot in group 490
- [cross_source_blocking] source 12749 seq 6: ready 113 ms before start, 113 ms of it with an idle slot in group 402
- [cross_source_blocking] source 6742 seq 28: ready 132 ms before start, 132 ms of it with an idle slot in group 230
- [cross_source_blocking] source 5796 seq 4: ready 151 ms before start, 101 ms of it with an idle slot in group 200
- [cross_source_blocking] source 4772 seq 13: ready 197 ms before start, 112 ms of it with an idle slot in group 165
- [cross_source_blocking] source 15720 seq 15: ready 124 ms before start, 124 ms of it with an idle slot in group 490
- [cross_source_blocking] source 7239 seq 27: ready 146 ms before start, 146 ms of it with an idle slot in group 246
- [cross_source_blocking] source 5244 seq 27: ready 129 ms before start, 129 ms of it with an idle slot in group 181
- [cross_source_blocking] source 3521 seq 14: ready 137 ms before start, 137 ms of it with an idle slot in group 120
- [cross_source_blocking] source 6760 seq 26: ready 101 ms before start, 101 ms of it with an idle slot in group 230
- [cross_source_blocking] source 11381 seq 15: ready 154 ms before start, 154 ms of it with an idle slot in group 371
- [cross_source_blocking] source 15816 seq 19: ready 199 ms before start, 199 ms of it with an idle slot in group 496
- [cross_source_blocking] source 4885 seq 12: ready 204 ms before start, 127 ms of it with an idle slot in group 169
- [cross_source_blocking] source 10817 seq 6: ready 128 ms before start, 128 ms of it with an idle slot in group 353
- [cross_source_blocking] source 6744 seq 24: ready 129 ms before start, 129 ms of it with an idle slot in group 230
- [cross_source_blocking] source 3611 seq 16: ready 226 ms before start, 188 ms of it with an idle slot in group 122
- [cross_source_blocking] source 10775 seq 7: ready 195 ms before start, 169 ms of it with an idle slot in group 353
- [cross_source_blocking] source 8074 seq 24: ready 202 ms before start, 201 ms of it with an idle slot in group 271

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3183 | 3183 | 0 | 2088.959 | 11993.087 | 13959.167 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3089 | 3089 | 0 | 1753.087 | 10747.903 | 12124.159 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2840 | 2840 | 0 | 1028.095 | 10158.079 | 13369.343 |
| t-428 | 37 | 1 | 74.414 | 46.006 | 159 | 0 | 2890 | 2890 | 0 | 1196.031 | 10027.007 | 12386.303 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5896 | 5896 | 0 | 1777.663 | 9437.183 | 11862.015 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9176 | 9176 | 0 | 1802.239 | 9306.111 | 12910.591 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9060 | 9060 | 0 | 1490.943 | 9175.039 | 12189.695 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6078 | 6078 | 0 | 1433.599 | 9175.039 | 12124.159 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11810 | 11810 | 0 | 1433.599 | 9109.503 | 12058.623 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5537 | 5537 | 0 | 1011.711 | 8978.431 | 10551.295 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5615 | 5615 | 0 | 884.735 | 8978.431 | 11272.191 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2860 | 2860 | 0 | 1597.439 | 8912.895 | 10813.439 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5955 | 5955 | 0 | 974.847 | 8716.287 | 10813.439 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3085 | 3085 | 0 | 1204.223 | 8585.215 | 11665.407 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2949 | 2949 | 0 | 1187.839 | 8585.215 | 10092.543 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5516 | 5516 | 0 | 1433.599 | 8519.679 | 10813.439 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5795 | 5795 | 0 | 1196.031 | 8519.679 | 10485.759 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15096 | 15096 | 0 | 1269.759 | 8519.679 | 10747.903 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20531 | 20531 | 0 | 1138.687 | 8454.143 | 11403.263 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2619 | 2619 | 0 | 651.263 | 8454.143 | 11141.119 |
| t-282 | 23 | 1 | 104.692 | 40.289 | 956 | 0 | 2499 | 2499 | 0 | 757.759 | 8323.071 | 11796.479 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11089 | 11089 | 0 | 1122.303 | 8257.535 | 12713.983 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14643 | 14643 | 0 | 1425.407 | 8191.999 | 10354.687 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19707 | 19707 | 0 | 995.327 | 8060.927 | 12845.055 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 2767 | 2767 | 0 | 1155.071 | 7766.015 | 10354.687 |
