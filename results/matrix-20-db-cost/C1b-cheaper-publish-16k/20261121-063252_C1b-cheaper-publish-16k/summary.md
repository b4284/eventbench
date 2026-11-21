# C1b-cheaper-publish-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.196s, closing 2.064s, drain 5.817s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000445 | 1000445 | 16025.996 | 15599.767 | 21906.7 | 0 | 0 | 0 | 458 | 5162 | 475692 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797081 | 2705.901 | 1025.023 | 8110.079 | 16711.679 | 22364.159 | 31293.439 |
| e2e_from_send | 797081 | 2542.75 | 627.711 | 7991.295 | 16605.183 | 22282.239 | 30998.527 |
| producer_lag | 797081 | 163.148 | 7.771 | 660.479 | 1350.655 | 1584.127 | 2103.295 |
| publish_call | 1000445 | 74.107 | 53.919 | 155.519 | 340.479 | 538.111 | 903.167 |
| queue_wait | 797081 | 2479.593 | 560.639 | 7933.951 | 16547.839 | 22233.087 | 30982.143 |
| broker_dwell | 797081 | 2426.455 | 499.455 | 7884.799 | 16498.687 | 22183.935 | 30965.759 |
| receive_call | 825731 | 36.301 | 14.815 | 91.775 | 322.047 | 473.087 | 579.583 |
| ctx_load | 797081 | 14.882 | 10.511 | 30.959 | 71.039 | 179.199 | 291.327 |
| process | 797081 | 10.758 | 0.002 | 0.005 | 0.073 | 2621.439 | 3067.903 |
| ctx_commit | 797081 | 37.301 | 30.335 | 75.583 | 132.607 | 246.015 | 545.791 |
| ack | 797059 | 0.012 | 0.004 | 0.007 | 0.053 | 1.292 | 68.223 |
| service | 797081 | 63.157 | 45.567 | 98.623 | 189.823 | 2680.831 | 3459.071 |
| e2e_large | 356 | 2947.665 | 2887.679 | 4061.183 | 5615.615 | 6529.023 | 6529.023 |
| ready_wait | 797081 | 327.726 | 200.191 | 811.519 | 1732.607 | 2686.975 | 3989.503 |
| blocked | 797081 | 264.496 | 167.039 | 661.503 | 1236.991 | 2191.359 | 3004.415 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15599.767 | 19598.1 |
| applied_total | 536762.653 | 915807 |
| bench_cpu_cores | 5.47 | 6.409 |
| bench_cpu_pct | 22.791 | 26.7 |
| bench_gc_gen0 | 2.02 | 4 |
| bench_gc_gen1 | 0.673 | 2 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 1546.637 | 2031.2 |
| bench_mem_mb | 2303.773 | 2703.2 |
| bench_threadpool_queue | 208.367 | 1165 |
| bench_threadpool_threads | 42.122 | 44 |
| broker_backlog | 41100.49 | 60718 |
| ctr_eb_mosquitto_cpu_pct | 65.504 | 69.78 |
| ctr_eb_mosquitto_mem_mb | 9.237 | 34.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.09 | 24.09 |
| ctr_eb_pgbouncer_2_mem_mb | 15.457 | 16.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.056 | 21.52 |
| ctr_eb_pgbouncer_3_mem_mb | 15.02 | 16.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.146 | 23.19 |
| ctr_eb_pgbouncer_4_mem_mb | 15.331 | 16.4 |
| ctr_eb_pgbouncer_cpu_pct | 21.834 | 23.9 |
| ctr_eb_pgbouncer_mem_mb | 15.443 | 16.6 |
| ctr_eb_postgres_cpu_pct | 925.088 | 1003.82 |
| ctr_eb_postgres_mem_mb | 5664.48 | 5797.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.218 | 83.19 |
| in_flight | 38932.98 | 60512 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.847 | 1.97 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1090902.857 | 1861956 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 206860.184 | 348080 |
| octopus_mqtt_sent_total | 286508.429 | 507173 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 8.041 | 15 |
| pg_active_loads | 7.449 | 28 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.997 | 100 |
| pg_conn_active | 31.959 | 76 |
| pg_conn_idle | 227.163 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.51 | 11 |
| pg_conn_total | 259.122 | 260 |
| pg_db_size_mb | 3667.569 | 4199.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18843.133 | 23717.4 |
| pg_tup_del_per_s | 43290.29 | 58199.8 |
| pg_tup_fetch_per_s | 416665.069 | 509104.2 |
| pg_tup_ins_per_s | 55398.143 | 70279.6 |
| pg_tup_upd_per_s | 41629.673 | 51322.4 |
| pg_wal_mb_per_s | 117.584 | 267.73 |
| pgb_cl_active | 243.551 | 269 |
| pgb_cl_waiting | 8.347 | 73 |
| pgb_maxwait_ms | 0.425 | 8.3 |
| pgb_sv_active | 18.429 | 48 |
| pgb_sv_idle | 29.571 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16025.996 | 23264.1 |
| produced_total | 575695.633 | 961564 |
| publish_mb_per_s | 52.191 | 79.84 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,865 records are Completed |
| record_counts | pass | 138,865 records found, 138,865 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,445 events applied by workers, 1,000,445 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 475,692 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1237 ms, max 3002 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3774 seq 16: ready 127 ms before start, 127 ms of it with an idle slot in group 127
- [cross_source_blocking] source 15634 seq 7: ready 126 ms before start, 126 ms of it with an idle slot in group 486
- [cross_source_blocking] source 10 seq 10: ready 156 ms before start, 156 ms of it with an idle slot in group 0
- [cross_source_blocking] source 1082 seq 7: ready 151 ms before start, 151 ms of it with an idle slot in group 34
- [cross_source_blocking] source 7286 seq 8: ready 156 ms before start, 156 ms of it with an idle slot in group 250
- [cross_source_blocking] source 4499 seq 12: ready 147 ms before start, 147 ms of it with an idle slot in group 156
- [cross_source_blocking] source 7217 seq 12: ready 102 ms before start, 102 ms of it with an idle slot in group 245
- [cross_source_blocking] source 3976 seq 20: ready 101 ms before start, 101 ms of it with an idle slot in group 132
- [cross_source_blocking] source 414 seq 7: ready 104 ms before start, 104 ms of it with an idle slot in group 13
- [cross_source_blocking] source 13937 seq 15: ready 169 ms before start, 169 ms of it with an idle slot in group 443
- [cross_source_blocking] source 2869 seq 3: ready 170 ms before start, 170 ms of it with an idle slot in group 91
- [cross_source_blocking] source 9009 seq 9: ready 141 ms before start, 141 ms of it with an idle slot in group 311
- [cross_source_blocking] source 7277 seq 7: ready 168 ms before start, 168 ms of it with an idle slot in group 249
- [cross_source_blocking] source 10957 seq 5: ready 169 ms before start, 169 ms of it with an idle slot in group 358
- [cross_source_blocking] source 3213 seq 9: ready 136 ms before start, 136 ms of it with an idle slot in group 107
- [cross_source_blocking] source 1698 seq 3: ready 160 ms before start, 123 ms of it with an idle slot in group 50
- [cross_source_blocking] source 881 seq 6: ready 168 ms before start, 168 ms of it with an idle slot in group 27
- [cross_source_blocking] source 6349 seq 20: ready 169 ms before start, 169 ms of it with an idle slot in group 216
- [cross_source_blocking] source 10854 seq 6: ready 157 ms before start, 112 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10901 seq 10: ready 151 ms before start, 105 ms of it with an idle slot in group 354

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9220 | 9220 | 0 | 9043.967 | 26083.327 | 29622.271 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 9043.967 | 23855.103 | 30146.559 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3194 | 3194 | 0 | 7471.103 | 23592.959 | 31326.207 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 7045.119 | 22675.455 | 26869.759 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 6094.847 | 22282.239 | 25690.111 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 4390.911 | 21364.735 | 24510.463 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6099 | 6099 | 0 | 6586.367 | 21102.591 | 27131.903 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5917 | 5917 | 0 | 7307.263 | 20709.375 | 23724.031 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 5537.791 | 20578.303 | 22937.599 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11852 | 11852 | 0 | 6750.207 | 20578.303 | 25165.823 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15142 | 15142 | 0 | 6422.527 | 20447.231 | 26607.615 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 6356.991 | 20316.159 | 22544.383 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5542 | 5542 | 0 | 6488.063 | 20185.087 | 21233.663 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 5996.543 | 20054.015 | 26476.543 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20596 | 20596 | 0 | 5931.007 | 19922.943 | 26476.543 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14678 | 14678 | 0 | 7045.119 | 19529.727 | 23986.175 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 5341.183 | 19267.583 | 23986.175 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 4915.199 | 18874.367 | 22413.311 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 6619.135 | 18874.367 | 21757.951 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11134 | 11134 | 0 | 5931.007 | 17432.575 | 24379.391 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2674 | 2674 | 0 | 3801.087 | 17039.359 | 19660.799 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16726 | 16726 | 0 | 5341.183 | 17039.359 | 20447.231 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3103 | 3103 | 0 | 4915.199 | 17039.359 | 20840.447 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2724 | 2724 | 0 | 6324.223 | 16515.071 | 18481.151 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5724 | 5724 | 0 | 5767.167 | 16515.071 | 17432.575 |
