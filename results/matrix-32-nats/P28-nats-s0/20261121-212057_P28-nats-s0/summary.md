# P28-nats-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 85.937s, closing 1.396s, drain 23.588s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 808805 | 808805 | 13297.902 | 11604.196 | 27814.2 | 0 | 0 | 0 | 50 | 3906 | 353990 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 647792 | 5768.172 | 4960.255 | 12296.191 | 18808.831 | 23183.359 | 29802.495 |
| e2e_from_send | 647792 | 2741.371 | 1085.439 | 8007.679 | 14204.927 | 19038.207 | 28606.463 |
| producer_lag | 647792 | 3026.803 | 2836.479 | 6275.071 | 8388.607 | 9494.527 | 10256.383 |
| publish_call | 808805 | 148.044 | 118.463 | 300.543 | 562.175 | 869.887 | 1782.783 |
| queue_wait | 647792 | 2655.425 | 985.599 | 7938.047 | 14155.775 | 18989.055 | 28360.703 |
| broker_dwell | 647792 | 2535.2 | 855.039 | 7839.743 | 14049.279 | 18874.367 | 28295.167 |
| receive_call | 712134 | 24.273 | 10.959 | 58.975 | 214.911 | 440.575 | 624.127 |
| ctx_load | 647792 | 16.928 | 11.071 | 36.447 | 91.903 | 176.895 | 376.575 |
| process | 647792 | 9.979 | 0.002 | 0.004 | 0.079 | 2615.295 | 3115.007 |
| ctx_commit | 647792 | 58.743 | 46.207 | 121.727 | 220.543 | 368.639 | 1178.623 |
| ack | 647738 | 0.015 | 0.004 | 0.007 | 0.067 | 2.085 | 81.727 |
| service | 647792 | 85.947 | 64.543 | 148.991 | 305.919 | 2691.071 | 3753.983 |
| e2e_large | 43 | 6032.836 | 5750.783 | 9912.319 | 10936.319 | 10936.319 | 10936.319 |
| ready_wait | 647792 | 258.281 | 164.991 | 613.887 | 1413.119 | 2250.751 | 10412.031 |
| blocked | 647792 | 183.835 | 119.359 | 434.175 | 994.815 | 1290.239 | 9191.423 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11604.196 | 16152.1 |
| applied_total | 418407.857 | 692655 |
| bench_cpu_cores | 3.889 | 4.999 |
| bench_cpu_pct | 16.203 | 20.83 |
| bench_gc_gen0 | 1.612 | 3 |
| bench_gc_gen1 | 0.571 | 1 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 919.976 | 1452.5 |
| bench_mem_mb | 1483.588 | 1875.9 |
| bench_threadpool_queue | 242.633 | 1601 |
| bench_threadpool_threads | 38.878 | 44 |
| broker_backlog | 35789.204 | 94230 |
| ctr_eb_mssql_cpu_pct | 4.3 | 4.87 |
| ctr_eb_mssql_mem_mb | 6987.631 | 6990.4 |
| ctr_eb_mysql_cpu_pct | 1.264 | 1.42 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_nats_octopus_cpu_pct | 116.406 | 124.05 |
| ctr_eb_nats_octopus_mem_mb | 45.01 | 63.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 28.353 | 31.03 |
| ctr_eb_pgbouncer_2_mem_mb | 20.351 | 20.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 29.156 | 31.97 |
| ctr_eb_pgbouncer_3_mem_mb | 21.169 | 23.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 30.434 | 33.02 |
| ctr_eb_pgbouncer_4_mem_mb | 21.165 | 22 |
| ctr_eb_pgbouncer_cpu_pct | 29.252 | 32.07 |
| ctr_eb_pgbouncer_mem_mb | 22.237 | 23.1 |
| ctr_eb_postgres_cpu_pct | 998.36 | 1097.08 |
| ctr_eb_postgres_mem_mb | 5206.935 | 5579 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.718 | 86.1 |
| in_flight | 28461.694 | 85764 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.898 | 1.99 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 855201.571 | 1484310 |
| octopus_missing_reports_total | 49016.98 | 83199 |
| octopus_mqtt_received_total | 170038.041 | 286195 |
| octopus_mqtt_sent_total | 301872.592 | 493897 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.306 | 30 |
| pg_active_loads | 26.673 | 162 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 65.694 | 216 |
| pg_conn_idle | 194.694 | 238 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.592 | 16 |
| pg_conn_total | 260.388 | 261 |
| pg_db_size_mb | 3478.304 | 3877.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23245.376 | 26176.2 |
| pg_tup_del_per_s | 29748.586 | 35441.9 |
| pg_tup_fetch_per_s | 266108.569 | 316700.5 |
| pg_tup_ins_per_s | 41498.286 | 50091 |
| pg_tup_upd_per_s | 50528.68 | 57239.3 |
| pg_wal_mb_per_s | 112.881 | 161.96 |
| pgb_cl_active | 468.612 | 526 |
| pgb_cl_waiting | 29.367 | 230 |
| pgb_maxwait_ms | 3.675 | 32.45 |
| pgb_sv_active | 22.898 | 48 |
| pgb_sv_idle | 25.102 | 45 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 13297.902 | 22974.2 |
| produced_total | 446869.551 | 777458 |
| publish_mb_per_s | 25.359 | 38.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 182,071 records are Completed |
| record_counts | pass | 182,071 records found, 104,267 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 320 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 808,805 events applied by workers, 808,805 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 353,990 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 995 ms, max 9186 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5625 seq 35: ready 162 ms before start, 147 ms of it with an idle slot in group 194
- [cross_source_blocking] source 9102 seq 80: ready 112 ms before start, 112 ms of it with an idle slot in group 314
- [cross_source_blocking] source 8748 seq 39: ready 112 ms before start, 112 ms of it with an idle slot in group 304
- [cross_source_blocking] source 7127 seq 31: ready 111 ms before start, 111 ms of it with an idle slot in group 242
- [cross_source_blocking] source 13062 seq 19: ready 117 ms before start, 102 ms of it with an idle slot in group 416
- [cross_source_blocking] source 13781 seq 24: ready 117 ms before start, 105 ms of it with an idle slot in group 438
- [cross_source_blocking] source 12652 seq 21: ready 110 ms before start, 110 ms of it with an idle slot in group 398
- [cross_source_blocking] source 6622 seq 33: ready 118 ms before start, 104 ms of it with an idle slot in group 222
- [cross_source_blocking] source 4005 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 134
- [cross_source_blocking] source 14205 seq 41: ready 112 ms before start, 112 ms of it with an idle slot in group 454
- [cross_source_blocking] source 5465 seq 22: ready 123 ms before start, 111 ms of it with an idle slot in group 188
- [cross_source_blocking] source 5299 seq 28: ready 124 ms before start, 119 ms of it with an idle slot in group 182
- [cross_source_blocking] source 4989 seq 27: ready 123 ms before start, 123 ms of it with an idle slot in group 172
- [cross_source_blocking] source 14643 seq 16: ready 116 ms before start, 116 ms of it with an idle slot in group 464
- [cross_source_blocking] source 14132 seq 30: ready 417 ms before start, 417 ms of it with an idle slot in group 450
- [cross_source_blocking] source 8511 seq 25: ready 136 ms before start, 136 ms of it with an idle slot in group 292
- [cross_source_blocking] source 4117 seq 16: ready 137 ms before start, 137 ms of it with an idle slot in group 136
- [cross_source_blocking] source 8468 seq 18: ready 136 ms before start, 136 ms of it with an idle slot in group 288
- [cross_source_blocking] source 13742 seq 23: ready 118 ms before start, 118 ms of it with an idle slot in group 438
- [cross_source_blocking] source 12058 seq 6: ready 130 ms before start, 130 ms of it with an idle slot in group 386

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 6 | 136.718 | 286.558 | 1340 | 0 | 16802 | 16802 | 0 | 8191.999 | 23855.103 | 28966.911 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2780 | 2780 | 0 | 8978.431 | 23724.031 | 26345.471 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 33794 | 33794 | 0 | 8257.535 | 22675.455 | 29884.415 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2757 | 2757 | 0 | 8388.607 | 22413.311 | 24641.535 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 2819 | 2819 | 0 | 7700.479 | 21889.023 | 24379.391 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2723 | 2723 | 0 | 7372.799 | 21757.951 | 23724.031 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2683 | 2683 | 0 | 8355.839 | 21626.879 | 24510.463 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 16325 | 16325 | 0 | 7077.887 | 21233.663 | 24641.535 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 24633 | 24633 | 0 | 7733.247 | 21233.663 | 27525.119 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8350 | 8350 | 0 | 7274.495 | 21102.591 | 24772.607 |
| t-364 | 66 | 1 | 38.872 | 43.874 | 436 | 0 | 2733 | 2733 | 0 | 6619.135 | 20971.519 | 25034.751 |
| t-260 | 30 | 2 | 180.858 | 93.625 | 499 | 0 | 5326 | 5326 | 0 | 7307.263 | 20316.159 | 25165.823 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5638 | 5638 | 0 | 6389.759 | 19922.943 | 24641.535 |
| t-276 | 78 | 1 | 37.161 | 47.91 | 423 | 0 | 2925 | 2925 | 0 | 6782.975 | 19922.943 | 24117.247 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 21217 | 21217 | 0 | 6979.583 | 19922.943 | 23724.031 |
| t-474 | 63 | 1 | 39.975 | 41.235 | 375 | 0 | 2517 | 2517 | 0 | 6455.295 | 19922.943 | 25952.255 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 10953 | 10953 | 0 | 7536.639 | 19922.943 | 21757.951 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 19809 | 19809 | 0 | 7274.495 | 19791.871 | 23986.175 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 15740 | 15740 | 0 | 6881.279 | 19791.871 | 24379.391 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16021 | 16021 | 0 | 6553.599 | 19529.727 | 23330.815 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5448 | 5448 | 0 | 6389.759 | 19398.655 | 23855.103 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2498 | 2498 | 0 | 5767.167 | 19267.583 | 20709.375 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5338 | 5338 | 0 | 6193.151 | 18874.367 | 23068.671 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 18174 | 18174 | 0 | 5963.775 | 18743.295 | 24510.463 |
| t-290 | 15 | 1 | 185.897 | 44.82 | 212 | 0 | 2593 | 2593 | 0 | 7077.887 | 18743.295 | 19922.943 |
