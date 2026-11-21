# X0b-no-loop — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.367s, closing 3.212s, drain 3.948s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 991921 | 991921 | 15823.955 | 15743.757 | 20118.6 | 0 | 0 | 0 | 455 | 5129 | 415912 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 788650 | 1835.159 | 600.575 | 5672.959 | 12836.863 | 19054.591 | 30523.391 |
| e2e_from_send | 788650 | 1693.391 | 378.623 | 5476.351 | 12673.023 | 19038.207 | 30523.391 |
| producer_lag | 788650 | 141.769 | 5.247 | 552.959 | 1215.487 | 1903.615 | 2424.831 |
| publish_call | 991921 | 70.405 | 51.167 | 153.855 | 274.175 | 463.359 | 683.007 |
| queue_wait | 788650 | 1631.226 | 310.527 | 5423.103 | 12623.871 | 19005.439 | 30507.007 |
| broker_dwell | 788650 | 1584.647 | 258.047 | 5373.951 | 12566.527 | 18956.287 | 30490.623 |
| receive_call | 820267 | 37.066 | 15.839 | 92.671 | 326.143 | 471.551 | 692.735 |
| ctx_load | 788650 | 14.314 | 10.583 | 29.103 | 65.247 | 134.911 | 251.263 |
| process | 788650 | 10.785 | 0.002 | 0.004 | 0.071 | 2621.439 | 3084.287 |
| ctx_commit | 788650 | 36.846 | 30.303 | 73.215 | 130.623 | 190.975 | 316.671 |
| ack | 788607 | 0.011 | 0.004 | 0.007 | 0.052 | 1.154 | 78.911 |
| service | 788650 | 62.164 | 45.279 | 95.231 | 177.791 | 2672.639 | 3217.407 |
| e2e_large | 353 | 2776 | 2717.695 | 3837.951 | 5246.975 | 6529.023 | 6529.023 |
| ready_wait | 788650 | 263.906 | 144.895 | 674.815 | 1524.735 | 2320.383 | 3448.831 |
| blocked | 788650 | 211.858 | 116.735 | 546.815 | 1131.519 | 1692.671 | 2664.447 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15743.757 | 20118.6 |
| applied_total | 550356.571 | 917254 |
| bench_cpu_cores | 5.687 | 6.756 |
| bench_cpu_pct | 23.694 | 28.15 |
| bench_gc_gen0 | 2.102 | 3 |
| bench_gc_gen1 | 0.633 | 2 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1465.869 | 1973.9 |
| bench_mem_mb | 2231.988 | 2604.3 |
| bench_threadpool_queue | 207.878 | 1270 |
| bench_threadpool_threads | 42.673 | 44 |
| broker_backlog | 29773.265 | 47370 |
| ctr_eb_mosquitto_cpu_pct | 68.964 | 75.26 |
| ctr_eb_mosquitto_mem_mb | 6.065 | 13.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.766 | 26.72 |
| ctr_eb_pgbouncer_2_mem_mb | 15.424 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 22.463 | 26.02 |
| ctr_eb_pgbouncer_3_mem_mb | 15.494 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 22.297 | 26.22 |
| ctr_eb_pgbouncer_4_mem_mb | 15.667 | 16.5 |
| ctr_eb_pgbouncer_cpu_pct | 20.134 | 23.51 |
| ctr_eb_pgbouncer_mem_mb | 15.443 | 16.4 |
| ctr_eb_postgres_cpu_pct | 829.341 | 991.72 |
| ctr_eb_postgres_mem_mb | 5777.953 | 6032.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.585 | 80 |
| in_flight | 25689 | 40568 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.664 | 1.86 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1103563.673 | 1844281 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 226282.306 | 374293 |
| octopus_mqtt_sent_total | 290918.245 | 502535 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.571 | 15 |
| pg_active_loads | 3.102 | 11 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.971 | 100 |
| pg_conn_active | 22.959 | 48 |
| pg_conn_idle | 233.714 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.694 | 16 |
| pg_conn_total | 256.673 | 258 |
| pg_db_size_mb | 3779.084 | 5000.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17820.088 | 21977.8 |
| pg_tup_del_per_s | 29105.335 | 36898.8 |
| pg_tup_fetch_per_s | 231754.396 | 260268.8 |
| pg_tup_ins_per_s | 41236.133 | 57440.3 |
| pg_tup_upd_per_s | 41787.118 | 48723 |
| pg_wal_mb_per_s | 109.647 | 223 |
| pgb_cl_active | 248.918 | 261 |
| pgb_cl_waiting | 3.653 | 41 |
| pgb_maxwait_ms | 2.122 | 19.93 |
| pgb_sv_active | 14.143 | 48 |
| pgb_sv_idle | 33.857 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15823.955 | 20078.8 |
| produced_total | 576045.571 | 952042 |
| publish_mb_per_s | 51.529 | 76.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,859 records are Completed |
| record_counts | pass | 137,859 records found, 137,859 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 455 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 991,921 events applied by workers, 991,921 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 415,912 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1132 ms, max 2664 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15762 seq 24: ready 130 ms before start, 130 ms of it with an idle slot in group 493
- [cross_source_blocking] source 562 seq 3: ready 136 ms before start, 136 ms of it with an idle slot in group 16
- [cross_source_blocking] source 5535 seq 10: ready 128 ms before start, 128 ms of it with an idle slot in group 189
- [cross_source_blocking] source 8215 seq 9: ready 104 ms before start, 104 ms of it with an idle slot in group 277
- [cross_source_blocking] source 9067 seq 10: ready 138 ms before start, 127 ms of it with an idle slot in group 313
- [cross_source_blocking] source 14065 seq 10: ready 114 ms before start, 114 ms of it with an idle slot in group 447
- [cross_source_blocking] source 11279 seq 11: ready 109 ms before start, 109 ms of it with an idle slot in group 367
- [cross_source_blocking] source 2513 seq 12: ready 113 ms before start, 106 ms of it with an idle slot in group 75
- [cross_source_blocking] source 14203 seq 26: ready 123 ms before start, 123 ms of it with an idle slot in group 454
- [cross_source_blocking] source 5049 seq 16: ready 117 ms before start, 117 ms of it with an idle slot in group 175
- [cross_source_blocking] source 418 seq 4: ready 127 ms before start, 127 ms of it with an idle slot in group 14
- [cross_source_blocking] source 6978 seq 8: ready 126 ms before start, 126 ms of it with an idle slot in group 237
- [cross_source_blocking] source 1854 seq 11: ready 132 ms before start, 132 ms of it with an idle slot in group 52
- [cross_source_blocking] source 7615 seq 14: ready 143 ms before start, 143 ms of it with an idle slot in group 261
- [cross_source_blocking] source 7582 seq 13: ready 125 ms before start, 125 ms of it with an idle slot in group 261
- [cross_source_blocking] source 7015 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 237
- [cross_source_blocking] source 14084 seq 6: ready 116 ms before start, 116 ms of it with an idle slot in group 449
- [cross_source_blocking] source 3791 seq 18: ready 107 ms before start, 107 ms of it with an idle slot in group 127
- [cross_source_blocking] source 8178 seq 2: ready 132 ms before start, 132 ms of it with an idle slot in group 276
- [cross_source_blocking] source 13215 seq 6: ready 140 ms before start, 140 ms of it with an idle slot in group 422

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9140 | 9140 | 0 | 6029.311 | 22020.095 | 26476.543 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3169 | 3169 | 0 | 5865.471 | 20840.447 | 30539.775 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9010 | 9010 | 0 | 6160.383 | 20185.087 | 29097.983 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5588 | 5588 | 0 | 5046.271 | 19529.727 | 27918.335 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5928 | 5928 | 0 | 4685.823 | 19136.511 | 24641.535 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11762 | 11762 | 0 | 5636.095 | 19005.439 | 24379.391 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15023 | 15023 | 0 | 5537.791 | 18087.935 | 25034.751 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5481 | 5481 | 0 | 5603.327 | 17956.863 | 20578.303 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5855 | 5855 | 0 | 5767.167 | 17956.863 | 21889.023 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14556 | 14556 | 0 | 6029.311 | 17432.575 | 21626.879 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20438 | 20438 | 0 | 4259.839 | 15925.247 | 23330.815 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11041 | 11041 | 0 | 4587.519 | 15794.175 | 24248.319 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2940 | 2940 | 0 | 3670.015 | 15597.567 | 19660.799 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3075 | 3075 | 0 | 4292.607 | 15335.423 | 20054.015 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5512 | 5512 | 0 | 4014.079 | 15269.887 | 22544.383 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2836 | 2836 | 0 | 3276.799 | 15269.887 | 19922.943 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2807 | 2807 | 0 | 3588.095 | 15204.351 | 19005.439 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2882 | 2882 | 0 | 3407.871 | 15138.815 | 17563.647 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5661 | 5661 | 0 | 3457.023 | 14942.207 | 17301.503 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6038 | 6038 | 0 | 4161.535 | 14417.919 | 18612.223 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2702 | 2702 | 0 | 3899.391 | 13631.487 | 16449.535 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19594 | 19594 | 0 | 3538.943 | 13369.343 | 18874.367 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2755 | 2755 | 0 | 3424.255 | 13303.807 | 15990.783 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3079 | 3079 | 0 | 3637.247 | 12976.127 | 16646.143 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5777 | 5777 | 0 | 3850.239 | 12976.127 | 18219.007 |
