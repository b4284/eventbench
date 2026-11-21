# P0b-base — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.406s, closing 3.177s, drain 8.669s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 984087 | 984087 | 15649.916 | 13719.402 | 22547.1 | 0 | 0 | 0 | 451 | 5089 | 533203 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 780785 | 5693.581 | 2605.055 | 16007.167 | 26345.471 | 31948.799 | 38436.863 |
| e2e_from_send | 780785 | 5553.029 | 2465.791 | 15892.479 | 26312.703 | 31899.647 | 38436.863 |
| producer_lag | 780785 | 140.553 | 11.871 | 482.303 | 1203.199 | 1572.863 | 1895.423 |
| publish_call | 984087 | 81.896 | 65.183 | 165.119 | 310.527 | 423.679 | 639.487 |
| queue_wait | 780785 | 5479.474 | 2369.535 | 15826.943 | 26247.167 | 31866.879 | 38436.863 |
| broker_dwell | 780785 | 5422.226 | 2310.143 | 15769.599 | 26214.399 | 31817.727 | 38404.095 |
| receive_call | 812417 | 36.83 | 13.975 | 92.223 | 342.271 | 499.967 | 586.239 |
| ctx_load | 780785 | 16.324 | 11.607 | 35.743 | 74.559 | 107.007 | 299.775 |
| process | 780785 | 10.794 | 0.003 | 0.005 | 0.078 | 2623.487 | 3020.799 |
| ctx_commit | 780785 | 46.189 | 38.303 | 94.591 | 163.199 | 293.375 | 395.263 |
| ack | 780758 | 0.012 | 0.004 | 0.008 | 0.058 | 1.301 | 76.479 |
| service | 780785 | 73.557 | 56.191 | 118.079 | 229.119 | 2689.023 | 3371.007 |
| e2e_large | 349 | 2872.294 | 2809.855 | 3975.167 | 5464.063 | 6172.671 | 6172.671 |
| ready_wait | 780785 | 470.406 | 297.215 | 1124.351 | 2494.463 | 3815.423 | 5181.439 |
| blocked | 780785 | 366.713 | 235.135 | 888.319 | 1729.535 | 2678.783 | 3246.079 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13719.402 | 17825.5 |
| applied_total | 498528 | 813896 |
| bench_cpu_cores | 5.682 | 7.51 |
| bench_cpu_pct | 23.673 | 31.29 |
| bench_gc_gen0 | 2 | 5 |
| bench_gc_gen1 | 0.633 | 2 |
| bench_gc_gen2 | 0.49 | 2 |
| bench_gc_heap_mb | 1445.863 | 1846.6 |
| bench_mem_mb | 2204.19 | 2475.4 |
| bench_threadpool_queue | 196.959 | 1200 |
| bench_threadpool_threads | 43.592 | 44 |
| broker_backlog | 77316.714 | 134826 |
| ctr_eb_mosquitto_cpu_pct | 66.88 | 73.92 |
| ctr_eb_mosquitto_mem_mb | 8.841 | 24.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.077 | 23.91 |
| ctr_eb_pgbouncer_2_mem_mb | 15.533 | 18.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.074 | 22.79 |
| ctr_eb_pgbouncer_3_mem_mb | 13.645 | 14.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.683 | 24.61 |
| ctr_eb_pgbouncer_4_mem_mb | 15.688 | 16.2 |
| ctr_eb_pgbouncer_cpu_pct | 20.728 | 23.41 |
| ctr_eb_pgbouncer_mem_mb | 13.802 | 14.5 |
| ctr_eb_postgres_cpu_pct | 843.468 | 992.09 |
| ctr_eb_postgres_mem_mb | 5840.535 | 6112.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.752 | 80.81 |
| in_flight | 77235.449 | 132157 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.752 | 1.84 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1052370.959 | 1790991 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 222090.898 | 378904 |
| octopus_mqtt_sent_total | 263125.429 | 457004 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 8.306 | 16 |
| pg_active_loads | 5.082 | 18 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.981 | 100 |
| pg_conn_active | 18.612 | 49 |
| pg_conn_idle | 237.184 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.653 | 16 |
| pg_conn_total | 255.796 | 258 |
| pg_db_size_mb | 3794.771 | 4944.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 15638.549 | 20672.9 |
| pg_tup_del_per_s | 27352.735 | 36875.5 |
| pg_tup_fetch_per_s | 225888.996 | 279815.6 |
| pg_tup_ins_per_s | 41371.788 | 59296.2 |
| pg_tup_upd_per_s | 37391.053 | 46137.1 |
| pg_wal_mb_per_s | 108.11 | 158.51 |
| pgb_cl_active | 242.224 | 263 |
| pgb_cl_waiting | 3.367 | 30 |
| pgb_maxwait_ms | 2.027 | 27.58 |
| pgb_sv_active | 13 | 48 |
| pgb_sv_idle | 35 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15649.916 | 18691.6 |
| produced_total | 575763.449 | 943765 |
| publish_mb_per_s | 50.9 | 88.4 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 136,989 records are Completed |
| record_counts | pass | 136,989 records found, 136,989 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 451 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 984,087 events applied by workers, 984,087 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 533,203 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1730 ms, max 3245 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9533 seq 9: ready 103 ms before start, 103 ms of it with an idle slot in group 331
- [cross_source_blocking] source 3101 seq 11: ready 116 ms before start, 116 ms of it with an idle slot in group 103
- [cross_source_blocking] source 14209 seq 15: ready 119 ms before start, 119 ms of it with an idle slot in group 455
- [cross_source_blocking] source 8700 seq 24: ready 102 ms before start, 102 ms of it with an idle slot in group 302
- [cross_source_blocking] source 13732 seq 18: ready 102 ms before start, 102 ms of it with an idle slot in group 437
- [cross_source_blocking] source 8077 seq 31: ready 100 ms before start, 100 ms of it with an idle slot in group 272
- [cross_source_blocking] source 10960 seq 7: ready 102 ms before start, 102 ms of it with an idle slot in group 358
- [cross_source_blocking] source 15136 seq 5: ready 102 ms before start, 102 ms of it with an idle slot in group 472
- [cross_source_blocking] source 4538 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 158
- [cross_source_blocking] source 14072 seq 14: ready 103 ms before start, 103 ms of it with an idle slot in group 448
- [cross_source_blocking] source 2940 seq 9: ready 104 ms before start, 104 ms of it with an idle slot in group 94
- [cross_source_blocking] source 3312 seq 18: ready 100 ms before start, 100 ms of it with an idle slot in group 111
- [cross_source_blocking] source 3317 seq 14: ready 103 ms before start, 103 ms of it with an idle slot in group 111
- [cross_source_blocking] source 13344 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 426
- [cross_source_blocking] source 2332 seq 21: ready 100 ms before start, 100 ms of it with an idle slot in group 67
- [cross_source_blocking] source 14620 seq 7: ready 100 ms before start, 100 ms of it with an idle slot in group 462
- [cross_source_blocking] source 2330 seq 18: ready 112 ms before start, 112 ms of it with an idle slot in group 67
- [cross_source_blocking] source 3801 seq 20: ready 103 ms before start, 103 ms of it with an idle slot in group 128
- [cross_source_blocking] source 11213 seq 8: ready 127 ms before start, 127 ms of it with an idle slot in group 365
- [cross_source_blocking] source 5551 seq 14: ready 126 ms before start, 126 ms of it with an idle slot in group 190

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3144 | 3144 | 0 | 14417.919 | 35127.295 | 38535.167 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2814 | 2814 | 0 | 12779.519 | 34340.863 | 37748.735 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9049 | 9049 | 0 | 14745.599 | 34078.719 | 38535.167 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2784 | 2784 | 0 | 10485.759 | 32767.999 | 37748.735 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5884 | 5884 | 0 | 13041.663 | 32505.855 | 36438.015 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 5998 | 5998 | 0 | 12386.303 | 32374.783 | 37224.447 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8941 | 8941 | 0 | 13959.167 | 31981.567 | 36700.159 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2916 | 2916 | 0 | 12845.055 | 31457.279 | 34340.863 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5554 | 5554 | 0 | 11993.087 | 31195.135 | 35389.439 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2864 | 2864 | 0 | 10158.079 | 30932.991 | 34603.007 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3059 | 3059 | 0 | 12648.447 | 30801.919 | 34078.719 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11663 | 11663 | 0 | 12779.519 | 30277.631 | 35651.583 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5836 | 5836 | 0 | 13172.735 | 30146.559 | 33554.431 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5730 | 5730 | 0 | 11730.943 | 30146.559 | 34865.151 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3045 | 3045 | 0 | 12189.695 | 30015.487 | 34340.863 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2820 | 2820 | 0 | 9437.183 | 29753.343 | 38273.023 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20255 | 20255 | 0 | 12189.695 | 29622.271 | 35913.727 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14444 | 14444 | 0 | 13172.735 | 29491.199 | 32505.855 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2790 | 2790 | 0 | 10420.223 | 29491.199 | 31195.135 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5433 | 5433 | 0 | 12713.983 | 29229.055 | 31326.207 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 5059 | 5059 | 0 | 8716.287 | 29229.055 | 38535.167 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14906 | 14906 | 0 | 12517.375 | 29097.983 | 35389.439 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5466 | 5466 | 0 | 11141.119 | 28966.911 | 33816.575 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4876 | 4876 | 0 | 7864.319 | 28966.911 | 32767.999 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 10947 | 10947 | 0 | 11599.871 | 28573.695 | 32636.927 |
