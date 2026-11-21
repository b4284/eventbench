# D2-morning-octopus-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.258s, closing 2.451s, drain 7.794s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000405 | 1000405 | 16762.839 | 15031.769 | 21854.1 | 0 | 0 | 0 | 458 | 5162 | 537689 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797042 | 4659.206 | 3358.719 | 11108.351 | 19300.351 | 24281.087 | 30834.687 |
| e2e_from_send | 797042 | 3487.722 | 1330.175 | 10305.535 | 18677.759 | 23281.663 | 29540.351 |
| producer_lag | 797042 | 1171.482 | 930.815 | 2924.543 | 3790.847 | 4292.607 | 4780.031 |
| publish_call | 1000405 | 93.135 | 75.199 | 180.223 | 340.735 | 647.167 | 925.183 |
| queue_wait | 797042 | 3422.738 | 1244.159 | 10256.383 | 18628.607 | 23232.511 | 29458.431 |
| broker_dwell | 797042 | 3360.04 | 1176.575 | 10199.039 | 18563.071 | 23166.975 | 29409.279 |
| receive_call | 823022 | 37.584 | 15.119 | 92.927 | 331.007 | 496.639 | 729.599 |
| ctx_load | 797042 | 16.012 | 11.303 | 33.471 | 73.535 | 115.967 | 282.111 |
| process | 797042 | 10.765 | 0.002 | 0.004 | 0.076 | 2625.535 | 3170.303 |
| ctx_commit | 797042 | 37.984 | 31.151 | 76.735 | 132.735 | 243.711 | 484.095 |
| ack | 797015 | 0.012 | 0.004 | 0.007 | 0.053 | 1.06 | 202.239 |
| service | 797042 | 64.989 | 48.223 | 100.927 | 184.575 | 2680.831 | 3233.791 |
| e2e_large | 356 | 4123.671 | 4063.231 | 6352.895 | 7544.831 | 9543.679 | 9543.679 |
| ready_wait | 797042 | 406.042 | 275.967 | 937.471 | 2035.711 | 2764.799 | 7852.031 |
| blocked | 797042 | 332.71 | 232.191 | 781.823 | 1498.111 | 2092.031 | 7483.391 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15031.769 | 19574 |
| applied_total | 509333.408 | 873902 |
| bench_cpu_cores | 4.998 | 6.896 |
| bench_cpu_pct | 20.826 | 28.73 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 0.673 | 2 |
| bench_gc_gen2 | 0.531 | 1 |
| bench_gc_heap_mb | 1511.976 | 1882 |
| bench_mem_mb | 2260.435 | 2588.8 |
| bench_threadpool_queue | 178.347 | 981 |
| bench_threadpool_threads | 42.184 | 44 |
| broker_backlog | 50788.898 | 88049 |
| ctr_eb_mosquitto_cpu_pct | 65.636 | 74.29 |
| ctr_eb_mosquitto_mem_mb | 8.89 | 18.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.51 | 24.97 |
| ctr_eb_pgbouncer_2_mem_mb | 15.278 | 16.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 21.155 | 24.77 |
| ctr_eb_pgbouncer_3_mem_mb | 15.133 | 17.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.946 | 25.76 |
| ctr_eb_pgbouncer_4_mem_mb | 15.392 | 16 |
| ctr_eb_pgbouncer_cpu_pct | 22.27 | 25.31 |
| ctr_eb_pgbouncer_mem_mb | 16.153 | 17 |
| ctr_eb_postgres_cpu_pct | 935.042 | 1005.72 |
| ctr_eb_postgres_mem_mb | 5756.704 | 5968.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.476 | 81.84 |
| in_flight | 49664.041 | 87030 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.834 | 1.95 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1044154.184 | 1839054 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 163194.224 | 290086 |
| octopus_mqtt_sent_total | 270967.02 | 485854 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.408 | 16 |
| pg_active_loads | 6.143 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.994 | 100 |
| pg_conn_active | 28.98 | 68 |
| pg_conn_idle | 229.714 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.531 | 15 |
| pg_conn_total | 258.694 | 260 |
| pg_db_size_mb | 3617.527 | 4250.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17876.796 | 21663.7 |
| pg_tup_del_per_s | 41716.092 | 50134.1 |
| pg_tup_fetch_per_s | 539618.967 | 650380.9 |
| pg_tup_ins_per_s | 56535.894 | 78201.6 |
| pg_tup_upd_per_s | 39633.29 | 47624 |
| pg_wal_mb_per_s | 114.207 | 163.01 |
| pgb_cl_active | 245.571 | 261 |
| pgb_cl_waiting | 0.571 | 9 |
| pgb_maxwait_ms | 0.289 | 7.45 |
| pgb_sv_active | 13.02 | 48 |
| pgb_sv_idle | 34.98 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16762.839 | 21552.7 |
| produced_total | 558997.449 | 960932 |
| publish_mb_per_s | 55.155 | 90.34 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,859 records are Completed |
| record_counts | pass | 138,859 records found, 138,859 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,405 events applied by workers, 1,000,405 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 537,689 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1498 ms, max 7479 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14781 seq 10: ready 137 ms before start, 121 ms of it with an idle slot in group 464
- [cross_source_blocking] source 989 seq 51: ready 126 ms before start, 125 ms of it with an idle slot in group 29
- [cross_source_blocking] source 3101 seq 11: ready 150 ms before start, 150 ms of it with an idle slot in group 103
- [cross_source_blocking] source 13021 seq 12: ready 139 ms before start, 139 ms of it with an idle slot in group 414
- [cross_source_blocking] source 12957 seq 8: ready 145 ms before start, 106 ms of it with an idle slot in group 409
- [cross_source_blocking] source 3990 seq 30: ready 124 ms before start, 105 ms of it with an idle slot in group 132
- [cross_source_blocking] source 3549 seq 25: ready 122 ms before start, 122 ms of it with an idle slot in group 121
- [cross_source_blocking] source 11645 seq 10: ready 170 ms before start, 144 ms of it with an idle slot in group 378
- [cross_source_blocking] source 11453 seq 9: ready 127 ms before start, 113 ms of it with an idle slot in group 372
- [cross_source_blocking] source 6454 seq 6: ready 124 ms before start, 124 ms of it with an idle slot in group 221
- [cross_source_blocking] source 3573 seq 23: ready 135 ms before start, 117 ms of it with an idle slot in group 121
- [cross_source_blocking] source 3541 seq 20: ready 135 ms before start, 118 ms of it with an idle slot in group 121
- [cross_source_blocking] source 13749 seq 12: ready 134 ms before start, 110 ms of it with an idle slot in group 438
- [cross_source_blocking] source 13781 seq 18: ready 134 ms before start, 110 ms of it with an idle slot in group 438
- [cross_source_blocking] source 11810 seq 6: ready 120 ms before start, 120 ms of it with an idle slot in group 382
- [cross_source_blocking] source 2518 seq 11: ready 153 ms before start, 153 ms of it with an idle slot in group 75
- [cross_source_blocking] source 15810 seq 5: ready 124 ms before start, 124 ms of it with an idle slot in group 495
- [cross_source_blocking] source 1890 seq 7: ready 120 ms before start, 120 ms of it with an idle slot in group 54
- [cross_source_blocking] source 1910 seq 25: ready 151 ms before start, 151 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1058 seq 5: ready 123 ms before start, 123 ms of it with an idle slot in group 32

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 8585.215 | 25296.895 | 30015.487 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 8650.751 | 25034.751 | 29884.415 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 9633.791 | 24903.679 | 29622.271 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 9895.935 | 24772.607 | 30932.991 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15138 | 15138 | 0 | 9502.719 | 23986.175 | 27394.047 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2725 | 2725 | 0 | 9240.575 | 23855.103 | 25952.255 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 7766.015 | 23855.103 | 27525.119 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9220 | 9220 | 0 | 8781.823 | 23724.031 | 27656.191 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 7864.319 | 23724.031 | 27262.975 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 8650.751 | 23330.815 | 30277.631 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14676 | 14676 | 0 | 9306.111 | 23068.671 | 27787.263 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 8650.751 | 23068.671 | 26083.327 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 8781.823 | 23068.671 | 26083.327 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 8355.839 | 22937.599 | 28573.695 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6099 | 6099 | 0 | 7864.319 | 22806.527 | 29229.055 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19762 | 19762 | 0 | 7831.551 | 22413.311 | 27262.975 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 8650.751 | 22282.239 | 26345.471 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20595 | 20595 | 0 | 8126.463 | 22282.239 | 26869.759 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 7340.031 | 21757.951 | 24379.391 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 8847.359 | 21626.879 | 24903.679 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 7831.551 | 21626.879 | 23986.175 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2675 | 2675 | 0 | 5996.543 | 21364.735 | 28180.479 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11133 | 11133 | 0 | 8093.695 | 21364.735 | 28049.407 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4965 | 4965 | 0 | 5537.791 | 21364.735 | 25952.255 |
| t-122 | 33 | 1 | 82.365 | 44.385 | 550 | 0 | 2756 | 2756 | 0 | 7929.855 | 21102.591 | 23330.815 |
