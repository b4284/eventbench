# U1b-unwritten — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.366s, closing 2.249s, drain 3.388s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 989046 | 989046 | 15712.092 | 15885.198 | 22617.5 | 0 | 0 | 0 | 454 | 5108 | 295466 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 785689 | 946.144 | 233.343 | 2877.439 | 6975.487 | 9969.663 | 14753.791 |
| e2e_from_send | 785689 | 798.899 | 193.919 | 2519.039 | 6782.975 | 9920.511 | 14598.143 |
| producer_lag | 785689 | 147.245 | 3.447 | 556.543 | 1819.647 | 2205.695 | 2691.071 |
| publish_call | 989046 | 61.273 | 42.495 | 137.983 | 269.823 | 394.751 | 489.983 |
| queue_wait | 785689 | 739.943 | 133.759 | 2422.783 | 6729.727 | 9863.167 | 14581.759 |
| broker_dwell | 785689 | 696.909 | 77.951 | 2365.439 | 6680.575 | 9838.591 | 14540.799 |
| receive_call | 837409 | 36.339 | 15.127 | 90.815 | 317.951 | 473.087 | 620.543 |
| ctx_load | 785689 | 13.167 | 9.439 | 26.543 | 62.623 | 129.407 | 232.575 |
| process | 785689 | 10.765 | 0.002 | 0.004 | 0.067 | 2619.391 | 3035.135 |
| ctx_commit | 785689 | 34.867 | 26.943 | 72.063 | 136.447 | 252.415 | 301.567 |
| ack | 785658 | 0.011 | 0.004 | 0.007 | 0.048 | 1.034 | 64.255 |
| service | 785689 | 58.957 | 40.095 | 92.223 | 197.759 | 2672.639 | 3221.503 |
| e2e_large | 352 | 3140.899 | 3086.335 | 4263.935 | 6815.743 | 7266.303 | 7266.303 |
| ready_wait | 785689 | 174.833 | 59.519 | 507.391 | 1156.095 | 1930.239 | 2957.311 |
| blocked | 785689 | 136.365 | 49.823 | 401.919 | 843.775 | 1343.487 | 1865.727 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15885.198 | 19807.7 |
| applied_total | 563918.122 | 927382 |
| bench_cpu_cores | 5.903 | 7.267 |
| bench_cpu_pct | 24.594 | 30.28 |
| bench_gc_gen0 | 2.184 | 5 |
| bench_gc_gen1 | 0.816 | 3 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1444.665 | 1915.8 |
| bench_mem_mb | 2211.782 | 2488.2 |
| bench_threadpool_queue | 91 | 583 |
| bench_threadpool_threads | 42.306 | 44 |
| broker_backlog | 17007.571 | 34166 |
| ctr_eb_mosquitto_cpu_pct | 66.752 | 71.84 |
| ctr_eb_mosquitto_mem_mb | 6.018 | 14 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.393 | 23.05 |
| ctr_eb_pgbouncer_2_mem_mb | 15.661 | 17.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.908 | 22.06 |
| ctr_eb_pgbouncer_3_mem_mb | 15.294 | 15.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.898 | 24.34 |
| ctr_eb_pgbouncer_4_mem_mb | 15.68 | 17.7 |
| ctr_eb_pgbouncer_cpu_pct | 21.214 | 24.53 |
| ctr_eb_pgbouncer_mem_mb | 15.857 | 19.4 |
| ctr_eb_postgres_cpu_pct | 825.247 | 1043.59 |
| ctr_eb_postgres_mem_mb | 5794.608 | 6060.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.083 | 82.22 |
| in_flight | 12158.612 | 31750 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.614 | 1.79 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1118428.122 | 1847790 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 232658.592 | 380376 |
| octopus_mqtt_sent_total | 311745.531 | 532014 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.061 | 15 |
| pg_active_loads | 4.51 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.981 | 100 |
| pg_conn_active | 22.551 | 66 |
| pg_conn_idle | 235.878 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.673 | 57 |
| pg_conn_total | 258.429 | 260 |
| pg_db_size_mb | 3779.276 | 4973.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18072.529 | 21113.2 |
| pg_tup_del_per_s | 28870.949 | 35091.8 |
| pg_tup_fetch_per_s | 217337.357 | 264902.3 |
| pg_tup_ins_per_s | 40738.831 | 55392.7 |
| pg_tup_upd_per_s | 41598.357 | 47646.6 |
| pg_wal_mb_per_s | 109.554 | 231.94 |
| pgb_cl_active | 250.082 | 263 |
| pgb_cl_waiting | 1.714 | 42 |
| pgb_maxwait_ms | 0.424 | 10.38 |
| pgb_sv_active | 9.816 | 48 |
| pgb_sv_idle | 38.184 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15712.092 | 18421.3 |
| produced_total | 576076.735 | 949698 |
| publish_mb_per_s | 51.232 | 75.79 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,543 records are Completed |
| record_counts | pass | 137,543 records found, 137,543 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 454 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 989,046 events applied by workers, 989,046 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 295,466 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 844 ms, max 1866 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13180 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 419
- [cross_source_blocking] source 9228 seq 6: ready 158 ms before start, 124 ms of it with an idle slot in group 317
- [cross_source_blocking] source 2184 seq 6: ready 155 ms before start, 155 ms of it with an idle slot in group 62
- [cross_source_blocking] source 2968 seq 13: ready 153 ms before start, 153 ms of it with an idle slot in group 97
- [cross_source_blocking] source 15248 seq 9: ready 155 ms before start, 125 ms of it with an idle slot in group 476
- [cross_source_blocking] source 2012 seq 12: ready 158 ms before start, 158 ms of it with an idle slot in group 59
- [cross_source_blocking] source 13036 seq 8: ready 155 ms before start, 155 ms of it with an idle slot in group 415
- [cross_source_blocking] source 14188 seq 4: ready 158 ms before start, 158 ms of it with an idle slot in group 452
- [cross_source_blocking] source 4216 seq 14: ready 153 ms before start, 153 ms of it with an idle slot in group 140
- [cross_source_blocking] source 1988 seq 11: ready 157 ms before start, 157 ms of it with an idle slot in group 58
- [cross_source_blocking] source 15136 seq 5: ready 155 ms before start, 155 ms of it with an idle slot in group 472
- [cross_source_blocking] source 15636 seq 23: ready 154 ms before start, 154 ms of it with an idle slot in group 487
- [cross_source_blocking] source 9000 seq 11: ready 154 ms before start, 154 ms of it with an idle slot in group 310
- [cross_source_blocking] source 14180 seq 4: ready 157 ms before start, 157 ms of it with an idle slot in group 451
- [cross_source_blocking] source 6884 seq 15: ready 158 ms before start, 141 ms of it with an idle slot in group 234
- [cross_source_blocking] source 6888 seq 20: ready 116 ms before start, 116 ms of it with an idle slot in group 234
- [cross_source_blocking] source 7588 seq 19: ready 157 ms before start, 157 ms of it with an idle slot in group 261
- [cross_source_blocking] source 868 seq 12: ready 157 ms before start, 157 ms of it with an idle slot in group 25
- [cross_source_blocking] source 5076 seq 11: ready 154 ms before start, 154 ms of it with an idle slot in group 177
- [cross_source_blocking] source 15128 seq 5: ready 153 ms before start, 153 ms of it with an idle slot in group 472

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3161 | 3161 | 0 | 2072.575 | 11993.087 | 14483.455 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9104 | 9104 | 0 | 2424.831 | 11599.871 | 14811.135 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2823 | 2823 | 0 | 1171.455 | 11075.583 | 14286.847 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8982 | 8982 | 0 | 2129.919 | 10878.975 | 13697.023 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5853 | 5853 | 0 | 1884.159 | 10223.615 | 12386.303 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2881 | 2881 | 0 | 958.463 | 9830.399 | 12845.055 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5909 | 5909 | 0 | 1572.863 | 9830.399 | 12517.375 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6024 | 6024 | 0 | 1638.399 | 9699.327 | 13828.095 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3077 | 3077 | 0 | 1589.247 | 9633.791 | 12189.695 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5572 | 5572 | 0 | 880.639 | 9633.791 | 12058.623 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5479 | 5479 | 0 | 1003.519 | 9371.647 | 10551.295 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11708 | 11708 | 0 | 1269.759 | 9306.111 | 12845.055 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5464 | 5464 | 0 | 1220.607 | 8978.431 | 11337.727 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3057 | 3057 | 0 | 1277.951 | 8978.431 | 10747.903 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11009 | 11009 | 0 | 1040.383 | 8716.287 | 13434.879 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20377 | 20377 | 0 | 1073.151 | 8716.287 | 12451.839 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14523 | 14523 | 0 | 1490.943 | 8585.215 | 10354.687 |
| t-483 | 200 | 4 | 51.026 | 171.809 | 2470 | 0 | 10654 | 10654 | 0 | 663.551 | 8323.071 | 11468.799 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16519 | 16519 | 0 | 901.119 | 8224.767 | 11337.727 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2835 | 2835 | 0 | 1114.111 | 8191.999 | 10747.903 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5760 | 5760 | 0 | 1064.959 | 8191.999 | 10485.759 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14967 | 14967 | 0 | 1359.871 | 8191.999 | 10551.295 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2807 | 2807 | 0 | 1028.095 | 8159.231 | 11272.191 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2641 | 2641 | 0 | 700.415 | 7798.783 | 11927.551 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2749 | 2749 | 0 | 1032.191 | 7766.015 | 9437.183 |
