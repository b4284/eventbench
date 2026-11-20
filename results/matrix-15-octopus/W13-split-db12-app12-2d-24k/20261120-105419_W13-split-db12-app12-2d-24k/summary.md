# W13-split-db12-app12-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.146s, closing 1.499s, drain 18.053s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487568 | 1487568 | 24624.304 | 24187.059 | 28536.2 | 0 | 0 | 0 | 425 | 7514 | 171957 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1202143 | 943.237 | 93.951 | 1762.303 | 15572.991 | 22921.215 | 29458.431 |
| e2e_from_send | 1202143 | 765.186 | 77.375 | 1072.127 | 15564.799 | 22921.215 | 29425.663 |
| producer_lag | 1202143 | 178.051 | 2.681 | 818.687 | 1716.223 | 1927.167 | 2318.335 |
| publish_call | 1487568 | 44.591 | 32.175 | 90.943 | 193.535 | 281.087 | 503.295 |
| queue_wait | 1202143 | 735.237 | 54.527 | 920.575 | 15548.415 | 22888.447 | 29425.663 |
| broker_dwell | 1202143 | 708.901 | 25.263 | 892.415 | 15532.031 | 22872.063 | 29409.279 |
| receive_call | 1350601 | 23.312 | 1.915 | 60.991 | 275.967 | 472.831 | 561.663 |
| ctx_load | 1202143 | 7.173 | 5.943 | 13.495 | 26.079 | 38.239 | 82.303 |
| process | 1202143 | 10.399 | 0.001 | 0.002 | 0.028 | 2615.295 | 3018.751 |
| ctx_commit | 1202143 | 12.337 | 10.071 | 23.711 | 44.575 | 69.311 | 184.191 |
| ack | 1200401 | 0.007 | 0.003 | 0.006 | 0.038 | 0.335 | 35.391 |
| service | 1202143 | 29.951 | 17.263 | 34.751 | 65.599 | 2633.727 | 3071.999 |
| e2e_large | 324 | 2841.758 | 2719.743 | 4179.967 | 6459.391 | 8437.759 | 8437.759 |
| ready_wait | 1202143 | 65.99 | 18.047 | 167.039 | 880.639 | 1712.127 | 2981.887 |
| blocked | 1202143 | 65.048 | 17.423 | 163.967 | 879.615 | 1712.127 | 2981.887 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24187.059 | 28536.2 |
| applied_total | 849821.429 | 1419893 |
| bench_cpu_cores | 5.891 | 6.477 |
| bench_cpu_pct | 49.092 | 53.98 |
| bench_gc_gen0 | 4.347 | 6 |
| bench_gc_gen1 | 4.265 | 6 |
| bench_gc_gen2 | 0.878 | 2 |
| bench_gc_heap_mb | 753.318 | 1064.6 |
| bench_mem_mb | 1167.908 | 1357.9 |
| bench_threadpool_queue | 162.388 | 945 |
| bench_threadpool_threads | 24.429 | 31 |
| broker_backlog | 19031.306 | 27886 |
| ctr_eb_mosquitto_cpu_pct | 69.132 | 76.5 |
| ctr_eb_mosquitto_mem_mb | 11.461 | 17.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 27.83 | 32.9 |
| ctr_eb_pgbouncer_2_mem_mb | 16.159 | 17.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 25.739 | 30.15 |
| ctr_eb_pgbouncer_3_mem_mb | 16.008 | 18 |
| ctr_eb_pgbouncer_4_cpu_pct | 28.296 | 33.67 |
| ctr_eb_pgbouncer_4_mem_mb | 16.108 | 16.8 |
| ctr_eb_pgbouncer_cpu_pct | 25.299 | 30.03 |
| ctr_eb_pgbouncer_mem_mb | 16.749 | 17.2 |
| ctr_eb_postgres_cpu_pct | 834.838 | 943.28 |
| ctr_eb_postgres_mem_mb | 5421.12 | 5510.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.725 | 81.84 |
| in_flight | 15672.122 | 26816 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.538 | 1.75 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1672734.939 | 2806295 |
| octopus_mqtt_received_total | 117142.735 | 195604 |
| octopus_mqtt_sent_total | 387098.633 | 678052 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.041 | 10 |
| pg_active_loads | 2.531 | 10 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.986 | 100 |
| pg_conn_active | 13.143 | 30 |
| pg_conn_idle | 220.306 | 228 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.184 | 14 |
| pg_conn_total | 233.449 | 235 |
| pg_db_size_mb | 3780.392 | 4154.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27556.616 | 36129 |
| pg_tup_del_per_s | 58313.769 | 68199.3 |
| pg_tup_fetch_per_s | 353851.402 | 441320.1 |
| pg_tup_ins_per_s | 70787.788 | 90816.5 |
| pg_tup_upd_per_s | 61716.694 | 75075.7 |
| pg_wal_mb_per_s | 140.584 | 195.75 |
| pgb_cl_active | 243.429 | 270 |
| pgb_cl_waiting | 7.51 | 62 |
| pgb_maxwait_ms | 0.429 | 3.13 |
| pgb_sv_active | 11.408 | 48 |
| pgb_sv_idle | 36.592 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24624.304 | 28451.3 |
| produced_total | 865493.551 | 1446709 |
| publish_mb_per_s | 65.926 | 91.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,917 records are Completed |
| record_counts | pass | 193,917 records found, 193,917 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,568 events applied by workers, 1,487,568 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 171,957 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 880 ms, max 2982 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12252 seq 7: ready 138 ms before start, 138 ms of it with an idle slot in group 389
- [cross_source_blocking] source 15740 seq 10: ready 135 ms before start, 135 ms of it with an idle slot in group 491
- [cross_source_blocking] source 5916 seq 17: ready 144 ms before start, 115 ms of it with an idle slot in group 205
- [cross_source_blocking] source 4476 seq 9: ready 139 ms before start, 139 ms of it with an idle slot in group 154
- [cross_source_blocking] source 1020 seq 19: ready 164 ms before start, 164 ms of it with an idle slot in group 31
- [cross_source_blocking] source 5052 seq 13: ready 131 ms before start, 131 ms of it with an idle slot in group 176
- [cross_source_blocking] source 7132 seq 16: ready 165 ms before start, 165 ms of it with an idle slot in group 242
- [cross_source_blocking] source 8508 seq 21: ready 164 ms before start, 164 ms of it with an idle slot in group 292
- [cross_source_blocking] source 10940 seq 18: ready 131 ms before start, 114 ms of it with an idle slot in group 357
- [cross_source_blocking] source 9212 seq 6: ready 131 ms before start, 131 ms of it with an idle slot in group 316
- [cross_source_blocking] source 15612 seq 7: ready 135 ms before start, 135 ms of it with an idle slot in group 485
- [cross_source_blocking] source 13276 seq 24: ready 103 ms before start, 103 ms of it with an idle slot in group 425
- [cross_source_blocking] source 10684 seq 17: ready 164 ms before start, 164 ms of it with an idle slot in group 350
- [cross_source_blocking] source 12892 seq 12: ready 136 ms before start, 113 ms of it with an idle slot in group 408
- [cross_source_blocking] source 2172 seq 9: ready 135 ms before start, 135 ms of it with an idle slot in group 62
- [cross_source_blocking] source 4796 seq 13: ready 132 ms before start, 132 ms of it with an idle slot in group 167
- [cross_source_blocking] source 11612 seq 13: ready 104 ms before start, 104 ms of it with an idle slot in group 377
- [cross_source_blocking] source 7260 seq 9: ready 164 ms before start, 164 ms of it with an idle slot in group 248
- [cross_source_blocking] source 9052 seq 7: ready 173 ms before start, 126 ms of it with an idle slot in group 313
- [cross_source_blocking] source 14492 seq 15: ready 205 ms before start, 167 ms of it with an idle slot in group 461

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30764 | 30764 | 0 | 7110.655 | 25559.039 | 29491.199 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22484 | 22484 | 0 | 7733.247 | 23592.959 | 26607.615 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 19025 | 19025 | 0 | 6750.207 | 22806.527 | 28180.479 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25414 | 25414 | 0 | 7569.407 | 21364.735 | 26345.471 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29460 | 29460 | 0 | 1826.815 | 13959.167 | 22413.311 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 13667 | 13667 | 0 | 933.887 | 9240.575 | 12058.623 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 2342.911 | 7077.887 | 8454.143 |
| t-483 | 200 | 6 | 76.95 | 259.099 | 2470 | 0 | 16101 | 16101 | 0 | 806.911 | 6225.919 | 8847.359 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9093 | 9093 | 0 | 516.095 | 5308.415 | 7602.175 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 2457.599 | 5275.647 | 5275.647 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 2039.807 | 4882.431 | 6062.079 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1785.855 | 4587.519 | 5799.935 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 11310 | 11310 | 0 | 303.103 | 3850.239 | 5013.503 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 1384 | 1384 | 0 | 71.167 | 3784.703 | 4587.519 |
| t-079 | 6 | 1 | 180.755 | 18.586 | 1522 | 0 | 1136 | 1136 | 0 | 79.359 | 3653.631 | 5767.167 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5769 | 5769 | 0 | 221.183 | 3637.247 | 4718.591 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 10879 | 10879 | 0 | 651.263 | 3555.327 | 6062.079 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8093 | 8093 | 0 | 91.647 | 3276.799 | 5079.039 |
| t-365 | 13 | 1 | 65.624 | 14.161 | 3428 | 0 | 925 | 925 | 0 | 67.071 | 3227.647 | 4325.375 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 9502 | 9502 | 0 | 139.263 | 3047.423 | 4620.287 |
| t-131 | 140 | 6 | 113.781 | 268.058 | 961 | 0 | 16541 | 16541 | 0 | 370.687 | 3031.039 | 6291.455 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 8523 | 8523 | 0 | 129.535 | 3014.655 | 4423.679 |
| t-295 | 12 | 1 | 103.993 | 20.658 | 3396 | 0 | 1309 | 1309 | 0 | 71.679 | 2998.271 | 5341.183 |
| t-090 | 7 | 1 | 74.668 | 8.784 | 242 | 0 | 527 | 527 | 0 | 71.167 | 2932.735 | 3751.935 |
| t-369 | 14 | 2 | 248.129 | 60.928 | 1313 | 0 | 3637 | 3637 | 0 | 76.287 | 2932.735 | 3915.775 |
