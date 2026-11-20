# W6-early-next-urgent-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.205s, closing 1.546s, drain 5.405s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1486919 | 1486919 | 24794.055 | 24316.808 | 26944.4 | 0 | 0 | 0 | 425 | 7513 | 314217 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1201494 | 1153.409 | 630.783 | 2502.655 | 8863.743 | 14630.911 | 26460.159 |
| e2e_from_send | 1201494 | 666.977 | 154.367 | 1608.703 | 8716.287 | 14483.455 | 26460.159 |
| producer_lag | 1201494 | 486.432 | 20.223 | 1561.599 | 2471.935 | 2691.071 | 2934.783 |
| publish_call | 1486919 | 59.859 | 50.847 | 109.439 | 201.855 | 303.359 | 473.855 |
| queue_wait | 1201494 | 617.474 | 109.119 | 1484.799 | 8683.519 | 14450.687 | 26083.327 |
| broker_dwell | 1201494 | 572.347 | 46.975 | 1441.791 | 8642.559 | 14417.919 | 26050.559 |
| receive_call | 1401085 | 21.815 | 0.021 | 61.599 | 267.263 | 447.231 | 540.159 |
| ctx_load | 1201494 | 8.339 | 6.447 | 16.703 | 33.279 | 48.191 | 135.551 |
| process | 1201494 | 10.404 | 0.001 | 0.002 | 0.023 | 2613.247 | 3018.751 |
| ctx_commit | 1201494 | 30.715 | 28.303 | 54.143 | 82.175 | 104.767 | 275.199 |
| ack | 1200655 | 0.008 | 0.003 | 0.006 | 0.035 | 0.675 | 34.623 |
| service | 1201494 | 49.504 | 37.151 | 65.791 | 102.527 | 2652.159 | 3100.671 |
| e2e_large | 324 | 3096.266 | 2988.031 | 4624.383 | 6557.695 | 8216.575 | 8216.575 |
| ready_wait | 1201494 | 95.624 | 34.367 | 268.799 | 715.775 | 1227.775 | 2461.695 |
| blocked | 1201494 | 90.82 | 29.375 | 262.399 | 710.655 | 1220.607 | 2461.695 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24316.808 | 26680.2 |
| applied_total | 843008.163 | 1420710 |
| bench_cpu_cores | 5.44 | 6.571 |
| bench_cpu_pct | 22.669 | 27.38 |
| bench_gc_gen0 | 4.102 | 6 |
| bench_gc_gen1 | 4.061 | 6 |
| bench_gc_gen2 | 0.714 | 2 |
| bench_gc_heap_mb | 1032.743 | 1283.8 |
| bench_mem_mb | 1566.278 | 1689.6 |
| bench_threadpool_queue | 156.49 | 1064 |
| bench_threadpool_threads | 42.286 | 44 |
| broker_backlog | 13933.776 | 24962 |
| ctr_eb_mosquitto_cpu_pct | 67.484 | 70.19 |
| ctr_eb_mosquitto_mem_mb | 7.308 | 12.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 23.704 | 25.92 |
| ctr_eb_pgbouncer_2_mem_mb | 15.796 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 24.347 | 27.29 |
| ctr_eb_pgbouncer_3_mem_mb | 16.345 | 17.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.361 | 28.59 |
| ctr_eb_pgbouncer_4_mem_mb | 16.122 | 17.1 |
| ctr_eb_pgbouncer_cpu_pct | 25.466 | 28.47 |
| ctr_eb_pgbouncer_mem_mb | 16.082 | 16.5 |
| ctr_eb_postgres_cpu_pct | 922.77 | 988.92 |
| ctr_eb_postgres_mem_mb | 4887.667 | 5362.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.752 | 81.36 |
| in_flight | 14592.204 | 25706 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.544 | 1.6 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1657319.429 | 2790252 |
| octopus_mqtt_received_total | 162257.184 | 271858 |
| octopus_mqtt_sent_total | 363292.143 | 610514 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.612 | 9 |
| pg_active_loads | 5.49 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.995 | 100 |
| pg_conn_active | 21.163 | 37 |
| pg_conn_idle | 205.204 | 220 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.837 | 8 |
| pg_conn_total | 226.367 | 228 |
| pg_db_size_mb | 3755.557 | 4241.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27043.52 | 31181.6 |
| pg_tup_del_per_s | 58128.033 | 66461 |
| pg_tup_fetch_per_s | 373670.286 | 408564.9 |
| pg_tup_ins_per_s | 71251.661 | 84521.7 |
| pg_tup_upd_per_s | 65765.653 | 72429 |
| pg_wal_mb_per_s | 140.756 | 219.55 |
| pgb_cl_active | 242.388 | 273 |
| pgb_cl_waiting | 2.653 | 38 |
| pgb_maxwait_ms | 0.457 | 7.46 |
| pgb_sv_active | 13.224 | 48 |
| pgb_sv_idle | 34.776 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24794.055 | 28980.5 |
| produced_total | 857600.367 | 1446117 |
| publish_mb_per_s | 66.216 | 89.83 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,841 records are Completed |
| record_counts | pass | 193,841 records found, 193,841 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,486,919 events applied by workers, 1,486,919 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 314,217 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 711 ms, max 2461 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10076 seq 11: ready 106 ms before start, 106 ms of it with an idle slot in group 343
- [cross_source_blocking] source 12828 seq 7: ready 105 ms before start, 105 ms of it with an idle slot in group 406
- [cross_source_blocking] source 15036 seq 11: ready 104 ms before start, 104 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15420 seq 15: ready 104 ms before start, 104 ms of it with an idle slot in group 483
- [cross_source_blocking] source 9212 seq 6: ready 106 ms before start, 106 ms of it with an idle slot in group 316
- [cross_source_blocking] source 11612 seq 13: ready 104 ms before start, 104 ms of it with an idle slot in group 377
- [cross_source_blocking] source 1212 seq 6: ready 125 ms before start, 125 ms of it with an idle slot in group 40
- [cross_source_blocking] source 11644 seq 20: ready 196 ms before start, 196 ms of it with an idle slot in group 378
- [cross_source_blocking] source 8892 seq 28: ready 115 ms before start, 115 ms of it with an idle slot in group 306
- [cross_source_blocking] source 2979 seq 8: ready 106 ms before start, 106 ms of it with an idle slot in group 98
- [cross_source_blocking] source 11011 seq 9: ready 106 ms before start, 106 ms of it with an idle slot in group 359
- [cross_source_blocking] source 1315 seq 54: ready 105 ms before start, 105 ms of it with an idle slot in group 41
- [cross_source_blocking] source 6339 seq 30: ready 106 ms before start, 106 ms of it with an idle slot in group 216
- [cross_source_blocking] source 11075 seq 19: ready 106 ms before start, 106 ms of it with an idle slot in group 362
- [cross_source_blocking] source 4771 seq 15: ready 106 ms before start, 106 ms of it with an idle slot in group 165
- [cross_source_blocking] source 963 seq 8: ready 106 ms before start, 106 ms of it with an idle slot in group 28
- [cross_source_blocking] source 6115 seq 24: ready 106 ms before start, 106 ms of it with an idle slot in group 210
- [cross_source_blocking] source 5955 seq 16: ready 106 ms before start, 106 ms of it with an idle slot in group 206
- [cross_source_blocking] source 9891 seq 7: ready 107 ms before start, 107 ms of it with an idle slot in group 340
- [cross_source_blocking] source 14115 seq 22: ready 106 ms before start, 106 ms of it with an idle slot in group 450

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15090 | 15090 | 0 | 4489.215 | 18481.151 | 26476.543 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 9502 | 9502 | 0 | 4554.751 | 15925.247 | 18874.367 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 9820 | 9820 | 0 | 3801.087 | 14090.239 | 16908.287 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14334 | 14334 | 0 | 2555.903 | 14024.703 | 17170.431 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 11306 | 11306 | 0 | 2572.287 | 13959.167 | 16383.999 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 12401 | 12401 | 0 | 2441.215 | 13303.807 | 14811.135 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 22203 | 22203 | 0 | 2260.991 | 12582.911 | 15925.247 |
| t-045 | 47 | 3 | 154.49 | 120.386 | 1398 | 0 | 7133 | 7133 | 0 | 2768.895 | 11927.551 | 13434.879 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25397 | 25397 | 0 | 1646.591 | 11665.407 | 16449.535 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 13662 | 13662 | 0 | 1728.511 | 11403.263 | 15204.351 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 8520 | 8520 | 0 | 2342.911 | 10878.975 | 12451.839 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30750 | 30750 | 0 | 1556.479 | 10813.439 | 17301.503 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9091 | 9091 | 0 | 1695.743 | 10616.831 | 13697.023 |
| t-075 | 37 | 2 | 141.157 | 87.188 | 232 | 0 | 5390 | 5390 | 0 | 1507.327 | 10420.223 | 11337.727 |
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8821 | 8821 | 0 | 1638.399 | 8355.839 | 13041.663 |
| t-490 | 56 | 4 | 175.559 | 164.988 | 245 | 0 | 9948 | 9948 | 0 | 1564.671 | 8126.463 | 12976.127 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 5854 | 5854 | 0 | 1523.711 | 7864.319 | 9633.791 |
| t-186 | 34 | 3 | 194.316 | 109.602 | 1259 | 0 | 6480 | 6480 | 0 | 1400.831 | 7798.783 | 9043.967 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 15028 | 15028 | 0 | 1433.599 | 7634.943 | 10289.151 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 5941 | 5941 | 0 | 1351.679 | 7569.407 | 8781.823 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 2588.671 | 6619.135 | 8224.767 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 19019 | 19019 | 0 | 1351.679 | 6619.135 | 10682.367 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5765 | 5765 | 0 | 1171.455 | 6488.063 | 7503.871 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2949.119 | 6029.311 | 6029.311 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 7625 | 7625 | 0 | 1007.615 | 5963.775 | 6815.743 |
