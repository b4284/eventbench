# X0a-no-loop — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.28s, closing 1.794s, drain 3.532s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 999908 | 999908 | 15913.563 | 15834.714 | 24102.9 | 0 | 0 | 0 | 458 | 5160 | 294103 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796531 | 832.106 | 201.343 | 2705.407 | 6778.879 | 9355.263 | 12951.551 |
| e2e_from_send | 796531 | 809.34 | 180.991 | 2670.591 | 6725.631 | 9314.303 | 12951.551 |
| producer_lag | 796531 | 22.765 | 2.679 | 62.815 | 310.271 | 479.231 | 977.407 |
| publish_call | 999908 | 53.36 | 37.407 | 112.447 | 243.455 | 364.799 | 580.095 |
| queue_wait | 796531 | 750.847 | 118.207 | 2580.479 | 6668.287 | 9256.959 | 12935.167 |
| broker_dwell | 796531 | 715.098 | 73.215 | 2535.423 | 6619.135 | 9199.615 | 12894.207 |
| receive_call | 851027 | 36.56 | 15.759 | 90.495 | 322.559 | 472.831 | 567.807 |
| ctx_load | 796531 | 12.853 | 9.367 | 26.015 | 58.143 | 121.599 | 215.039 |
| process | 796531 | 10.752 | 0.002 | 0.004 | 0.077 | 2623.487 | 3078.143 |
| ctx_commit | 796531 | 34.751 | 26.063 | 73.983 | 137.855 | 228.863 | 377.855 |
| ack | 796513 | 0.01 | 0.004 | 0.007 | 0.051 | 1.023 | 57.471 |
| service | 796531 | 58.493 | 40.031 | 92.479 | 199.935 | 2674.687 | 3233.791 |
| e2e_large | 356 | 2612.926 | 2559.999 | 3489.791 | 4882.431 | 6004.735 | 6004.735 |
| ready_wait | 796531 | 176.825 | 53.119 | 528.383 | 1219.583 | 1944.575 | 2838.527 |
| blocked | 796531 | 136.669 | 45.663 | 408.831 | 861.183 | 1352.703 | 1984.511 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15834.714 | 20049.8 |
| applied_total | 566170.347 | 928146 |
| bench_cpu_cores | 5.973 | 7.786 |
| bench_cpu_pct | 24.886 | 32.44 |
| bench_gc_gen0 | 2.122 | 4 |
| bench_gc_gen1 | 0.653 | 2 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1454.41 | 1894.3 |
| bench_mem_mb | 2197.537 | 2516 |
| bench_threadpool_queue | 175.204 | 861 |
| bench_threadpool_threads | 41.49 | 44 |
| broker_backlog | 15818.653 | 33368 |
| ctr_eb_mosquitto_cpu_pct | 68.371 | 73.92 |
| ctr_eb_mosquitto_mem_mb | 7.214 | 14.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.486 | 24.64 |
| ctr_eb_pgbouncer_2_mem_mb | 15.243 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 22.423 | 27 |
| ctr_eb_pgbouncer_3_mem_mb | 15.618 | 16.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.39 | 25.14 |
| ctr_eb_pgbouncer_4_mem_mb | 15.447 | 16.3 |
| ctr_eb_pgbouncer_cpu_pct | 22.487 | 26.24 |
| ctr_eb_pgbouncer_mem_mb | 15.327 | 16 |
| ctr_eb_postgres_cpu_pct | 811.128 | 973.47 |
| ctr_eb_postgres_mem_mb | 5728.294 | 6115.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.634 | 81.56 |
| in_flight | 12035 | 31259 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.637 | 1.79 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1120816.061 | 1917650 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 231592.082 | 396100 |
| octopus_mqtt_sent_total | 311638 | 547722 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 8.694 | 17 |
| pg_active_loads | 4.592 | 12 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.995 | 100 |
| pg_conn_active | 24.571 | 60 |
| pg_conn_idle | 231.755 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.224 | 45 |
| pg_conn_total | 256.327 | 260 |
| pg_db_size_mb | 3779.388 | 4872.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18224.961 | 23770.5 |
| pg_tup_del_per_s | 29299.008 | 36664.5 |
| pg_tup_fetch_per_s | 215428.361 | 263294.9 |
| pg_tup_ins_per_s | 41658.988 | 56028.6 |
| pg_tup_upd_per_s | 41732.047 | 53711.2 |
| pg_wal_mb_per_s | 109.539 | 166.18 |
| pgb_cl_active | 246.918 | 265 |
| pgb_cl_waiting | 3.857 | 39 |
| pgb_maxwait_ms | 1.874 | 16.18 |
| pgb_sv_active | 17.633 | 48 |
| pgb_sv_idle | 30.367 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15913.563 | 20951.3 |
| produced_total | 578205.347 | 959405 |
| publish_mb_per_s | 51.645 | 77.68 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,809 records are Completed |
| record_counts | pass | 138,809 records found, 138,809 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 999,908 events applied by workers, 999,908 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 294,103 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 861 ms, max 1984 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8706 seq 23: ready 100 ms before start, 100 ms of it with an idle slot in group 302
- [cross_source_blocking] source 12423 seq 10: ready 103 ms before start, 103 ms of it with an idle slot in group 394
- [cross_source_blocking] source 3799 seq 27: ready 102 ms before start, 102 ms of it with an idle slot in group 128
- [cross_source_blocking] source 255 seq 14: ready 132 ms before start, 132 ms of it with an idle slot in group 5
- [cross_source_blocking] source 610 seq 13: ready 166 ms before start, 134 ms of it with an idle slot in group 18
- [cross_source_blocking] source 6984 seq 7: ready 164 ms before start, 164 ms of it with an idle slot in group 237
- [cross_source_blocking] source 6997 seq 7: ready 176 ms before start, 176 ms of it with an idle slot in group 237
- [cross_source_blocking] source 3207 seq 13: ready 184 ms before start, 125 ms of it with an idle slot in group 107
- [cross_source_blocking] source 12783 seq 4: ready 118 ms before start, 118 ms of it with an idle slot in group 403
- [cross_source_blocking] source 15128 seq 5: ready 179 ms before start, 159 ms of it with an idle slot in group 472
- [cross_source_blocking] source 573 seq 9: ready 151 ms before start, 151 ms of it with an idle slot in group 16
- [cross_source_blocking] source 4977 seq 24: ready 115 ms before start, 115 ms of it with an idle slot in group 171
- [cross_source_blocking] source 7798 seq 9: ready 103 ms before start, 103 ms of it with an idle slot in group 264
- [cross_source_blocking] source 7481 seq 4: ready 167 ms before start, 128 ms of it with an idle slot in group 259
- [cross_source_blocking] source 6888 seq 20: ready 212 ms before start, 183 ms of it with an idle slot in group 234
- [cross_source_blocking] source 8449 seq 4: ready 103 ms before start, 103 ms of it with an idle slot in group 286
- [cross_source_blocking] source 4642 seq 15: ready 184 ms before start, 184 ms of it with an idle slot in group 161
- [cross_source_blocking] source 15110 seq 6: ready 113 ms before start, 102 ms of it with an idle slot in group 472
- [cross_source_blocking] source 15136 seq 5: ready 180 ms before start, 160 ms of it with an idle slot in group 472
- [cross_source_blocking] source 1122 seq 16: ready 189 ms before start, 150 ms of it with an idle slot in group 37

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3193 | 3193 | 0 | 1466.367 | 10485.759 | 12189.695 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3097 | 3097 | 0 | 1695.743 | 10289.151 | 12517.375 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9088 | 9088 | 0 | 2015.231 | 10158.079 | 12517.375 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9217 | 9217 | 0 | 1884.159 | 9568.255 | 12517.375 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5915 | 5915 | 0 | 1482.751 | 9240.575 | 10878.975 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 622.591 | 8978.431 | 11665.407 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15131 | 15131 | 0 | 1171.455 | 8978.431 | 10944.511 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 1482.751 | 8912.895 | 11730.943 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2908 | 2908 | 0 | 1036.287 | 8847.359 | 11993.087 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5971 | 5971 | 0 | 962.559 | 8847.359 | 10813.439 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5549 | 5549 | 0 | 831.487 | 8781.823 | 10289.151 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3101 | 3101 | 0 | 1335.295 | 8781.823 | 11141.119 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6091 | 6091 | 0 | 1187.839 | 8716.287 | 11665.407 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 1187.839 | 8650.751 | 11730.943 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11842 | 11842 | 0 | 1310.719 | 8585.215 | 11403.263 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14667 | 14667 | 0 | 1589.247 | 8454.143 | 9895.935 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5629 | 5629 | 0 | 811.007 | 8355.839 | 10551.295 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5535 | 5535 | 0 | 1245.183 | 8323.071 | 10223.615 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2673 | 2673 | 0 | 1011.711 | 8093.695 | 10813.439 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20587 | 20587 | 0 | 811.007 | 7995.391 | 10813.439 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 880.639 | 7831.551 | 10027.007 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19755 | 19755 | 0 | 815.103 | 7700.479 | 12976.127 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16718 | 16718 | 0 | 806.911 | 7602.175 | 9568.255 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2629 | 2629 | 0 | 501.759 | 7602.175 | 9961.471 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11130 | 11130 | 0 | 892.927 | 7569.407 | 12189.695 |
