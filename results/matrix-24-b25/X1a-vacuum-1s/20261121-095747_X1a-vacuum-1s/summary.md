# X1a-vacuum-1s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.211s, closing 2.106s, drain 5.073s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000337 | 1000337 | 15935.386 | 15511.804 | 25200.3 | 0 | 0 | 0 | 458 | 5161 | 423150 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796968 | 1742.545 | 438.015 | 5304.319 | 13754.367 | 20447.231 | 30015.487 |
| e2e_from_send | 796968 | 1713.227 | 391.935 | 5263.359 | 13729.791 | 20414.463 | 30015.487 |
| producer_lag | 796968 | 29.318 | 3.033 | 65.599 | 463.615 | 746.495 | 1456.127 |
| publish_call | 1000337 | 58.028 | 43.071 | 119.999 | 238.975 | 393.471 | 632.319 |
| queue_wait | 796968 | 1648.201 | 324.351 | 5201.919 | 13680.639 | 20365.311 | 30015.487 |
| broker_dwell | 796968 | 1608.186 | 280.575 | 5160.959 | 13631.487 | 20316.159 | 29982.719 |
| receive_call | 840207 | 35.928 | 14.031 | 90.943 | 327.423 | 478.719 | 688.639 |
| ctx_load | 796968 | 14.813 | 10.951 | 30.303 | 68.287 | 107.007 | 244.095 |
| process | 796968 | 10.752 | 0.002 | 0.004 | 0.076 | 2619.391 | 3024.895 |
| ctx_commit | 796968 | 39.275 | 32.047 | 79.679 | 139.135 | 212.991 | 270.079 |
| ack | 796947 | 0.011 | 0.004 | 0.007 | 0.052 | 1.136 | 55.071 |
| service | 796968 | 65.025 | 47.487 | 101.503 | 190.591 | 2678.783 | 3219.455 |
| e2e_large | 356 | 2717.072 | 2656.255 | 3618.815 | 5091.327 | 6520.831 | 6520.831 |
| ready_wait | 796968 | 276.016 | 148.351 | 715.263 | 1547.263 | 2326.527 | 3158.015 |
| blocked | 796968 | 213.576 | 118.463 | 560.127 | 1041.407 | 1604.607 | 2164.735 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15511.804 | 20579 |
| applied_total | 552512.122 | 915614 |
| bench_cpu_cores | 5.866 | 7.137 |
| bench_cpu_pct | 24.44 | 29.74 |
| bench_gc_gen0 | 2.061 | 5 |
| bench_gc_gen1 | 0.694 | 3 |
| bench_gc_gen2 | 0.51 | 2 |
| bench_gc_heap_mb | 1483.857 | 2057 |
| bench_mem_mb | 2236.067 | 2585.6 |
| bench_threadpool_queue | 142.122 | 892 |
| bench_threadpool_threads | 43.469 | 44 |
| broker_backlog | 30823.347 | 52637 |
| ctr_eb_mosquitto_cpu_pct | 66.892 | 71.62 |
| ctr_eb_mosquitto_mem_mb | 6.4 | 14.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.802 | 26.32 |
| ctr_eb_pgbouncer_2_mem_mb | 15.639 | 16.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.384 | 22.54 |
| ctr_eb_pgbouncer_3_mem_mb | 15.192 | 15.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.371 | 23.01 |
| ctr_eb_pgbouncer_4_mem_mb | 15.384 | 16.2 |
| ctr_eb_pgbouncer_cpu_pct | 21.007 | 23.97 |
| ctr_eb_pgbouncer_mem_mb | 15.786 | 18.6 |
| ctr_eb_postgres_cpu_pct | 821.544 | 968.4 |
| ctr_eb_postgres_mem_mb | 5759.592 | 6030.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.777 | 80.65 |
| in_flight | 25550.531 | 49039 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.633 | 1.74 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1111542.306 | 1914614 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 232236.245 | 401291 |
| octopus_mqtt_sent_total | 303641.082 | 534771 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.551 | 17 |
| pg_active_loads | 4.388 | 14 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.986 | 100 |
| pg_conn_active | 20.592 | 69 |
| pg_conn_idle | 235.796 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.571 | 40 |
| pg_conn_total | 256.388 | 258 |
| pg_db_size_mb | 3485.114 | 4907.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17515.261 | 21123.1 |
| pg_tup_del_per_s | 29180.89 | 37898.9 |
| pg_tup_fetch_per_s | 282636.537 | 498485.7 |
| pg_tup_ins_per_s | 41379.004 | 55702.7 |
| pg_tup_upd_per_s | 40770.335 | 44979.9 |
| pg_wal_mb_per_s | 109.057 | 146.68 |
| pgb_cl_active | 247.673 | 277 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 5.429 | 15 |
| pgb_sv_idle | 42.571 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15935.386 | 17940.2 |
| produced_total | 578062.653 | 960955 |
| publish_mb_per_s | 51.76 | 76.16 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,854 records are Completed |
| record_counts | pass | 138,854 records found, 138,854 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,337 events applied by workers, 1,000,337 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 423,150 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1041 ms, max 2164 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3742 seq 7: ready 135 ms before start, 135 ms of it with an idle slot in group 126
- [cross_source_blocking] source 7392 seq 11: ready 151 ms before start, 136 ms of it with an idle slot in group 255
- [cross_source_blocking] source 2019 seq 14: ready 100 ms before start, 100 ms of it with an idle slot in group 60
- [cross_source_blocking] source 11210 seq 12: ready 133 ms before start, 133 ms of it with an idle slot in group 365
- [cross_source_blocking] source 10828 seq 11: ready 189 ms before start, 127 ms of it with an idle slot in group 354
- [cross_source_blocking] source 6083 seq 12: ready 101 ms before start, 101 ms of it with an idle slot in group 208
- [cross_source_blocking] source 7198 seq 15: ready 143 ms before start, 143 ms of it with an idle slot in group 245
- [cross_source_blocking] source 8134 seq 21: ready 104 ms before start, 104 ms of it with an idle slot in group 275
- [cross_source_blocking] source 1095 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 35
- [cross_source_blocking] source 6663 seq 26: ready 100 ms before start, 100 ms of it with an idle slot in group 226
- [cross_source_blocking] source 222 seq 17: ready 134 ms before start, 134 ms of it with an idle slot in group 5
- [cross_source_blocking] source 4942 seq 6: ready 193 ms before start, 154 ms of it with an idle slot in group 170
- [cross_source_blocking] source 4933 seq 4: ready 190 ms before start, 152 ms of it with an idle slot in group 170
- [cross_source_blocking] source 10830 seq 8: ready 132 ms before start, 124 ms of it with an idle slot in group 354
- [cross_source_blocking] source 1034 seq 6: ready 188 ms before start, 168 ms of it with an idle slot in group 32
- [cross_source_blocking] source 3981 seq 29: ready 128 ms before start, 115 ms of it with an idle slot in group 132
- [cross_source_blocking] source 11168 seq 3: ready 193 ms before start, 162 ms of it with an idle slot in group 364
- [cross_source_blocking] source 9312 seq 17: ready 189 ms before start, 108 ms of it with an idle slot in group 322
- [cross_source_blocking] source 12957 seq 8: ready 188 ms before start, 165 ms of it with an idle slot in group 409
- [cross_source_blocking] source 8756 seq 18: ready 132 ms before start, 117 ms of it with an idle slot in group 304

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9219 | 9219 | 0 | 6389.759 | 23855.103 | 27656.191 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 6455.295 | 22151.167 | 29884.415 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3193 | 3193 | 0 | 5505.023 | 21495.807 | 30015.487 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5975 | 5975 | 0 | 4882.431 | 20840.447 | 24772.607 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11851 | 11851 | 0 | 5079.039 | 19791.871 | 24772.607 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5917 | 5917 | 0 | 5472.255 | 19660.799 | 22806.527 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15137 | 15137 | 0 | 5013.503 | 19398.655 | 24903.679 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2849 | 2849 | 0 | 3670.015 | 19005.439 | 24510.463 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5629 | 5629 | 0 | 4292.607 | 18612.223 | 26738.687 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14677 | 14677 | 0 | 5373.951 | 18350.079 | 23199.743 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2911 | 2911 | 0 | 3948.543 | 18350.079 | 21102.591 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5539 | 5539 | 0 | 4980.735 | 18087.935 | 20447.231 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 4063.231 | 18087.935 | 22937.599 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20595 | 20595 | 0 | 4145.151 | 17563.647 | 24510.463 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 4521.983 | 17432.575 | 21233.663 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6098 | 6098 | 0 | 4390.911 | 17170.431 | 23461.887 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2725 | 2725 | 0 | 4816.895 | 16646.143 | 20054.015 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5724 | 5724 | 0 | 3637.247 | 16187.391 | 18219.007 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11129 | 11129 | 0 | 4227.071 | 16056.319 | 24248.319 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5556 | 5556 | 0 | 3719.167 | 15794.175 | 22151.167 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 3391.487 | 14483.455 | 19791.871 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19764 | 19764 | 0 | 3145.727 | 14352.383 | 19922.943 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 2818.047 | 14024.703 | 18219.007 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 3883.007 | 13697.023 | 16187.391 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 3604.479 | 13697.023 | 17563.647 |
