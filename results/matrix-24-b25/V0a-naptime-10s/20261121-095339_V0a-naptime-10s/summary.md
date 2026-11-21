# V0a-naptime-10s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.843s, closing 1.49s, drain 1.481s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 999501 | 999501 | 15959.447 | 16332.949 | 20613.4 | 0 | 0 | 0 | 458 | 5158 | 160347 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796122 | 348.731 | 98.559 | 816.639 | 4179.967 | 6832.127 | 11509.759 |
| e2e_from_send | 796122 | 341.045 | 92.671 | 804.351 | 4165.631 | 6803.455 | 11485.183 |
| producer_lag | 796122 | 7.686 | 2.525 | 16.463 | 94.143 | 245.247 | 923.647 |
| publish_call | 999501 | 44.763 | 30.303 | 92.479 | 227.071 | 766.975 | 921.599 |
| queue_wait | 796122 | 294.164 | 51.295 | 703.999 | 4114.431 | 6746.111 | 11460.607 |
| broker_dwell | 796122 | 266.041 | 23.679 | 643.071 | 4075.519 | 6705.151 | 11444.223 |
| receive_call | 854167 | 37.892 | 17.295 | 92.223 | 321.023 | 468.479 | 559.615 |
| ctx_load | 796122 | 11.014 | 7.883 | 22.255 | 50.015 | 87.871 | 779.775 |
| process | 796122 | 10.748 | 0.002 | 0.004 | 0.064 | 2619.391 | 3028.991 |
| ctx_commit | 796122 | 24.967 | 17.999 | 51.999 | 100.735 | 755.711 | 988.159 |
| ack | 796100 | 0.009 | 0.004 | 0.007 | 0.045 | 0.713 | 52.127 |
| service | 796122 | 46.882 | 28.895 | 70.975 | 140.927 | 2664.447 | 3696.639 |
| e2e_large | 356 | 2626.405 | 2572.287 | 3543.039 | 5128.191 | 5734.399 | 5734.399 |
| ready_wait | 796122 | 84.469 | 19.471 | 254.463 | 781.311 | 1388.543 | 1959.935 |
| blocked | 796122 | 69.899 | 18.591 | 213.503 | 601.599 | 1015.807 | 1561.599 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16332.949 | 20482.5 |
| applied_total | 573497.551 | 959375 |
| bench_cpu_cores | 5.856 | 6.803 |
| bench_cpu_pct | 24.401 | 28.35 |
| bench_gc_gen0 | 2.143 | 4 |
| bench_gc_gen1 | 0.714 | 2 |
| bench_gc_gen2 | 0.531 | 1 |
| bench_gc_heap_mb | 1464.161 | 1936.7 |
| bench_mem_mb | 2210.855 | 2492.3 |
| bench_threadpool_queue | 95.102 | 435 |
| bench_threadpool_threads | 42.265 | 44 |
| broker_backlog | 9818.918 | 27124 |
| ctr_eb_mosquitto_cpu_pct | 67.112 | 71.44 |
| ctr_eb_mosquitto_mem_mb | 7.131 | 25 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.844 | 23.79 |
| ctr_eb_pgbouncer_2_mem_mb | 15.424 | 16.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.717 | 23.68 |
| ctr_eb_pgbouncer_3_mem_mb | 15.69 | 17 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.129 | 23.89 |
| ctr_eb_pgbouncer_4_mem_mb | 15.484 | 16.1 |
| ctr_eb_pgbouncer_cpu_pct | 20.479 | 23.36 |
| ctr_eb_pgbouncer_mem_mb | 15.498 | 16.3 |
| ctr_eb_postgres_cpu_pct | 795.412 | 934.15 |
| ctr_eb_postgres_mem_mb | 5642.147 | 5847.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.432 | 81.15 |
| in_flight | 5172.755 | 21768 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.566 | 1.72 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1128012.204 | 1882739 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 222241.878 | 366169 |
| octopus_mqtt_sent_total | 313201.837 | 538515 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.204 | 16 |
| pg_active_loads | 4.857 | 20 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.997 | 100 |
| pg_conn_active | 21.51 | 47 |
| pg_conn_idle | 233.612 | 250 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.041 | 25 |
| pg_conn_total | 255.122 | 257 |
| pg_db_size_mb | 3742.453 | 4933.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18820.545 | 21900.9 |
| pg_tup_del_per_s | 29677.286 | 39549.8 |
| pg_tup_fetch_per_s | 217031.214 | 255737.2 |
| pg_tup_ins_per_s | 41199.849 | 57428.7 |
| pg_tup_upd_per_s | 42501.569 | 49369.5 |
| pg_wal_mb_per_s | 109.267 | 189.36 |
| pgb_cl_active | 242.98 | 259 |
| pgb_cl_waiting | 6.204 | 152 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 7.449 | 48 |
| pgb_sv_idle | 40.551 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15959.447 | 17584.1 |
| produced_total | 578670.306 | 961203 |
| publish_mb_per_s | 51.862 | 74.52 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,771 records are Completed |
| record_counts | pass | 138,771 records found, 138,771 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 999,501 events applied by workers, 999,501 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 160,347 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 602 ms, max 1561 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13183 seq 5: ready 127 ms before start, 127 ms of it with an idle slot in group 419
- [cross_source_blocking] source 2182 seq 9: ready 179 ms before start, 179 ms of it with an idle slot in group 62
- [cross_source_blocking] source 12934 seq 23: ready 148 ms before start, 148 ms of it with an idle slot in group 409
- [cross_source_blocking] source 11753 seq 9: ready 103 ms before start, 103 ms of it with an idle slot in group 381
- [cross_source_blocking] source 1699 seq 5: ready 143 ms before start, 143 ms of it with an idle slot in group 50
- [cross_source_blocking] source 14209 seq 15: ready 180 ms before start, 180 ms of it with an idle slot in group 455
- [cross_source_blocking] source 1034 seq 6: ready 175 ms before start, 175 ms of it with an idle slot in group 32
- [cross_source_blocking] source 4499 seq 12: ready 133 ms before start, 133 ms of it with an idle slot in group 156
- [cross_source_blocking] source 4602 seq 16: ready 176 ms before start, 176 ms of it with an idle slot in group 161
- [cross_source_blocking] source 11710 seq 13: ready 158 ms before start, 158 ms of it with an idle slot in group 379
- [cross_source_blocking] source 12447 seq 4: ready 147 ms before start, 147 ms of it with an idle slot in group 396
- [cross_source_blocking] source 7002 seq 4: ready 171 ms before start, 171 ms of it with an idle slot in group 237
- [cross_source_blocking] source 4214 seq 11: ready 145 ms before start, 145 ms of it with an idle slot in group 140
- [cross_source_blocking] source 6221 seq 9: ready 115 ms before start, 115 ms of it with an idle slot in group 212
- [cross_source_blocking] source 7337 seq 5: ready 128 ms before start, 128 ms of it with an idle slot in group 252
- [cross_source_blocking] source 2335 seq 20: ready 148 ms before start, 148 ms of it with an idle slot in group 67
- [cross_source_blocking] source 1428 seq 3: ready 153 ms before start, 110 ms of it with an idle slot in group 44
- [cross_source_blocking] source 2891 seq 10: ready 144 ms before start, 144 ms of it with an idle slot in group 92
- [cross_source_blocking] source 935 seq 8: ready 144 ms before start, 144 ms of it with an idle slot in group 27
- [cross_source_blocking] source 8262 seq 11: ready 149 ms before start, 149 ms of it with an idle slot in group 280

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3192 | 3192 | 0 | 446.463 | 9043.967 | 11403.263 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5913 | 5913 | 0 | 268.287 | 7602.175 | 9961.471 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2848 | 2848 | 0 | 178.175 | 7077.887 | 9895.935 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9087 | 9087 | 0 | 294.911 | 6946.815 | 11534.335 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9215 | 9215 | 0 | 251.903 | 6815.743 | 10878.975 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11841 | 11841 | 0 | 204.799 | 6586.367 | 9895.935 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5972 | 5972 | 0 | 220.159 | 6488.063 | 8454.143 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5532 | 5532 | 0 | 206.847 | 6455.295 | 9764.863 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5626 | 5626 | 0 | 162.815 | 6356.991 | 8519.679 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2906 | 2906 | 0 | 197.631 | 6291.455 | 10485.759 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2672 | 2672 | 0 | 146.431 | 6225.919 | 9109.503 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5550 | 5550 | 0 | 200.703 | 6193.151 | 7634.943 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14663 | 14663 | 0 | 201.727 | 6127.615 | 7798.783 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3097 | 3097 | 0 | 520.191 | 6094.847 | 8519.679 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20576 | 20576 | 0 | 175.103 | 5931.007 | 9109.503 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11118 | 11118 | 0 | 218.111 | 5832.703 | 10158.079 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2931 | 2931 | 0 | 224.255 | 5832.703 | 7471.103 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3096 | 3096 | 0 | 440.319 | 5767.167 | 7667.711 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 199.679 | 5668.863 | 7143.423 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6088 | 6088 | 0 | 200.703 | 5603.327 | 9175.039 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5823 | 5823 | 0 | 152.575 | 5603.327 | 9109.503 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 174.079 | 5537.791 | 7831.551 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16715 | 16715 | 0 | 137.215 | 5406.719 | 7766.015 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4957 | 4957 | 0 | 113.663 | 5308.415 | 6782.975 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2959 | 2959 | 0 | 165.887 | 5242.879 | 7110.655 |
