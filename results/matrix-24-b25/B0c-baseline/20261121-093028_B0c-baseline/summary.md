# B0c-baseline — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.241s, closing 2.537s, drain 6.899s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 999676 | 999676 | 16246.663 | 15099.222 | 19609.1 | 0 | 0 | 0 | 458 | 5159 | 457925 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796299 | 1927.201 | 670.719 | 5664.767 | 14131.199 | 20824.063 | 31473.663 |
| e2e_from_send | 796299 | 1811.696 | 454.911 | 5603.327 | 14073.855 | 20791.295 | 31473.663 |
| producer_lag | 796299 | 115.504 | 5.211 | 303.615 | 1558.527 | 2040.831 | 2811.903 |
| publish_call | 999676 | 74.336 | 57.407 | 149.503 | 294.143 | 536.063 | 706.047 |
| queue_wait | 796299 | 1747.166 | 391.423 | 5541.887 | 14024.703 | 20725.759 | 31457.279 |
| broker_dwell | 796299 | 1703.273 | 342.783 | 5496.831 | 13983.743 | 20692.991 | 31440.895 |
| receive_call | 819333 | 37.699 | 14.999 | 98.239 | 335.359 | 493.055 | 608.767 |
| ctx_load | 796299 | 15.694 | 11.271 | 32.399 | 74.623 | 113.855 | 326.143 |
| process | 796299 | 10.763 | 0.002 | 0.004 | 0.075 | 2623.487 | 3026.943 |
| ctx_commit | 796299 | 37.871 | 30.751 | 77.375 | 137.215 | 183.807 | 392.191 |
| ack | 796270 | 0.012 | 0.004 | 0.007 | 0.054 | 1.584 | 81.919 |
| service | 796299 | 64.531 | 46.591 | 102.463 | 188.031 | 2678.783 | 3385.343 |
| e2e_large | 356 | 3333.413 | 3168.255 | 4632.575 | 7200.767 | 7540.735 | 7540.735 |
| ready_wait | 796299 | 308.245 | 174.463 | 775.679 | 1702.911 | 2732.031 | 3809.279 |
| blocked | 796299 | 249.835 | 146.047 | 634.879 | 1254.399 | 1983.487 | 2723.839 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15099.222 | 18997.9 |
| applied_total | 551594.551 | 896573 |
| bench_cpu_cores | 5.805 | 7.276 |
| bench_cpu_pct | 24.187 | 30.32 |
| bench_gc_gen0 | 2.163 | 4 |
| bench_gc_gen1 | 0.776 | 2 |
| bench_gc_gen2 | 0.633 | 1 |
| bench_gc_heap_mb | 1412.612 | 1832.9 |
| bench_mem_mb | 2210.412 | 2527.9 |
| bench_threadpool_queue | 276.571 | 1215 |
| bench_threadpool_threads | 41.204 | 44 |
| broker_backlog | 26064.735 | 58278 |
| ctr_eb_mosquitto_cpu_pct | 66.744 | 71.59 |
| ctr_eb_mosquitto_mem_mb | 8.314 | 18.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.626 | 23.4 |
| ctr_eb_pgbouncer_2_mem_mb | 15.345 | 16.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 21.359 | 22.68 |
| ctr_eb_pgbouncer_3_mem_mb | 15.465 | 16.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.108 | 23.05 |
| ctr_eb_pgbouncer_4_mem_mb | 15.453 | 16.5 |
| ctr_eb_pgbouncer_cpu_pct | 21.03 | 23.09 |
| ctr_eb_pgbouncer_mem_mb | 15.794 | 17.8 |
| ctr_eb_postgres_cpu_pct | 869.089 | 942.79 |
| ctr_eb_postgres_mem_mb | 5732.59 | 5869.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.666 | 82.07 |
| in_flight | 25106.347 | 63914 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.844 | 1.95 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1106680.551 | 1828502 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 202502.449 | 335491 |
| octopus_mqtt_sent_total | 297282.878 | 496569 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.612 | 14 |
| pg_active_loads | 7.857 | 32 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 25.429 | 58 |
| pg_conn_idle | 232.878 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.776 | 15 |
| pg_conn_total | 258.306 | 260 |
| pg_db_size_mb | 3611.282 | 4011.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18724.749 | 24053.5 |
| pg_tup_del_per_s | 42500.845 | 56608 |
| pg_tup_fetch_per_s | 421284.971 | 494608.8 |
| pg_tup_ins_per_s | 55495.878 | 72853.5 |
| pg_tup_upd_per_s | 41083.937 | 51028 |
| pg_wal_mb_per_s | 108.268 | 135.22 |
| pgb_cl_active | 247.816 | 255 |
| pgb_cl_waiting | 3.551 | 45 |
| pgb_maxwait_ms | 0.696 | 8.77 |
| pgb_sv_active | 12.449 | 48 |
| pgb_sv_idle | 35.551 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16246.663 | 23553.3 |
| produced_total | 576700.898 | 960487 |
| publish_mb_per_s | 52.973 | 86.26 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,780 records are Completed |
| record_counts | pass | 138,780 records found, 138,780 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 999,676 events applied by workers, 999,676 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 457,925 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1254 ms, max 2724 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7235 seq 35: ready 168 ms before start, 168 ms of it with an idle slot in group 246
- [cross_source_blocking] source 2533 seq 21: ready 116 ms before start, 116 ms of it with an idle slot in group 75
- [cross_source_blocking] source 1253 seq 9: ready 121 ms before start, 121 ms of it with an idle slot in group 40
- [cross_source_blocking] source 9333 seq 16: ready 267 ms before start, 185 ms of it with an idle slot in group 322
- [cross_source_blocking] source 9532 seq 8: ready 110 ms before start, 110 ms of it with an idle slot in group 331
- [cross_source_blocking] source 5667 seq 13: ready 127 ms before start, 106 ms of it with an idle slot in group 194
- [cross_source_blocking] source 2524 seq 15: ready 104 ms before start, 104 ms of it with an idle slot in group 75
- [cross_source_blocking] source 2940 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 94
- [cross_source_blocking] source 6117 seq 6: ready 208 ms before start, 124 ms of it with an idle slot in group 210
- [cross_source_blocking] source 8549 seq 8: ready 335 ms before start, 241 ms of it with an idle slot in group 293
- [cross_source_blocking] source 8773 seq 12: ready 303 ms before start, 278 ms of it with an idle slot in group 306
- [cross_source_blocking] source 10405 seq 12: ready 323 ms before start, 298 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3861 seq 13: ready 293 ms before start, 234 ms of it with an idle slot in group 131
- [cross_source_blocking] source 1493 seq 13: ready 352 ms before start, 268 ms of it with an idle slot in group 45
- [cross_source_blocking] source 7781 seq 14: ready 199 ms before start, 157 ms of it with an idle slot in group 263
- [cross_source_blocking] source 9091 seq 28: ready 173 ms before start, 121 ms of it with an idle slot in group 314
- [cross_source_blocking] source 1925 seq 19: ready 241 ms before start, 196 ms of it with an idle slot in group 55
- [cross_source_blocking] source 2245 seq 26: ready 120 ms before start, 111 ms of it with an idle slot in group 65
- [cross_source_blocking] source 1923 seq 22: ready 160 ms before start, 131 ms of it with an idle slot in group 55
- [cross_source_blocking] source 10389 seq 7: ready 338 ms before start, 313 ms of it with an idle slot in group 347

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9211 | 9211 | 0 | 6881.279 | 25165.823 | 29753.343 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3190 | 3190 | 0 | 6291.455 | 22544.383 | 31588.351 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9090 | 9090 | 0 | 6324.223 | 22413.311 | 30015.487 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5971 | 5971 | 0 | 5406.719 | 22282.239 | 26345.471 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11840 | 11840 | 0 | 4947.967 | 20316.159 | 25427.967 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6088 | 6088 | 0 | 4784.127 | 19660.799 | 26214.399 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5629 | 5629 | 0 | 4489.215 | 19398.655 | 25821.183 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2849 | 2849 | 0 | 4128.767 | 19398.655 | 25690.111 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2958 | 2958 | 0 | 4128.767 | 18743.295 | 25821.183 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5532 | 5532 | 0 | 4784.127 | 18481.151 | 20316.159 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14667 | 14667 | 0 | 4620.287 | 18350.079 | 23068.671 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15127 | 15127 | 0 | 3670.015 | 18219.007 | 24641.535 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3096 | 3096 | 0 | 3506.175 | 17563.647 | 19660.799 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5915 | 5915 | 0 | 4456.447 | 17301.503 | 22675.455 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2720 | 2720 | 0 | 4063.231 | 17039.359 | 19922.943 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5552 | 5552 | 0 | 3407.871 | 17039.359 | 21757.951 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5717 | 5717 | 0 | 3506.175 | 17039.359 | 19529.727 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20582 | 20582 | 0 | 3358.719 | 17039.359 | 23592.959 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11121 | 11121 | 0 | 3981.311 | 16449.535 | 25690.111 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16717 | 16717 | 0 | 2981.887 | 15532.031 | 19660.799 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19754 | 19754 | 0 | 2195.455 | 13959.167 | 19529.727 |
| t-428 | 37 | 1 | 74.414 | 46.006 | 159 | 0 | 2901 | 2901 | 0 | 2818.047 | 13959.167 | 17563.647 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7640 | 7640 | 0 | 2850.815 | 13762.559 | 15663.103 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2834 | 2834 | 0 | 2949.119 | 13762.559 | 18219.007 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 2056.191 | 13762.559 | 15532.031 |
