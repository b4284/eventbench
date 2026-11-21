# K2-crash-50pct-workers-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.172s, closing 1.183s, drain 14.513s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814635 | 814635 | 12855.878 | 12749.263 | 21325.2 | 5 | 0 | 0 | 431 | 4254 | 65901 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642896 | 509.053 | 52.415 | 314.111 | 18202.623 | 30081.023 | 36306.943 |
| e2e_from_send | 642896 | 504.836 | 48.927 | 305.663 | 18186.239 | 30081.023 | 36306.943 |
| producer_lag | 642913 | 4.217 | 1.81 | 8.895 | 40.127 | 143.487 | 540.671 |
| publish_call | 814635 | 36.404 | 20.367 | 79.679 | 249.983 | 475.647 | 1046.015 |
| queue_wait | 642896 | 472.476 | 27.791 | 252.799 | 18153.471 | 30064.639 | 36241.407 |
| broker_dwell | 642896 | 454.726 | 11.711 | 229.247 | 18137.087 | 30064.639 | 36208.639 |
| receive_call | 703206 | 41.628 | 18.031 | 106.367 | 347.135 | 475.903 | 546.815 |
| ctx_load | 642896 | 7.675 | 5.443 | 15.647 | 39.263 | 56.639 | 100.223 |
| process | 642896 | 10.916 | 0.002 | 0.004 | 0.057 | 2625.535 | 3028.991 |
| ctx_commit | 642896 | 13.624 | 9.039 | 30.303 | 68.927 | 123.519 | 201.087 |
| ack | 642884 | 0.009 | 0.004 | 0.007 | 0.044 | 0.678 | 39.199 |
| service | 642896 | 32.36 | 16.175 | 44.095 | 102.591 | 2648.063 | 3088.383 |
| e2e_large | 331 | 4717.624 | 3008.511 | 5656.575 | 34996.223 | 35160.063 | 35160.063 |
| ready_wait | 642896 | 57.592 | 8.551 | 116.287 | 586.751 | 1331.199 | 33275.903 |
| blocked | 642896 | 53.46 | 8.479 | 103.487 | 517.375 | 1178.623 | 33275.903 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12749.263 | 15247 |
| applied_total | 462732.898 | 763115 |
| bench_cpu_cores | 5.082 | 6.126 |
| bench_cpu_pct | 21.176 | 25.52 |
| bench_gc_gen0 | 1.918 | 4 |
| bench_gc_gen1 | 0.694 | 2 |
| bench_gc_gen2 | 0.469 | 1 |
| bench_gc_heap_mb | 1373.682 | 1721.3 |
| bench_mem_mb | 2080.757 | 2266.3 |
| bench_threadpool_queue | 55.837 | 485 |
| bench_threadpool_threads | 42.673 | 44 |
| broker_backlog | 6986.918 | 15500 |
| ctr_eb_mosquitto_cpu_pct | 62.414 | 67.37 |
| ctr_eb_mosquitto_mem_mb | 8.39 | 16.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 17.19 | 19.29 |
| ctr_eb_pgbouncer_2_mem_mb | 15.378 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 17.31 | 19.75 |
| ctr_eb_pgbouncer_3_mem_mb | 15.241 | 17.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 19.406 | 22.03 |
| ctr_eb_pgbouncer_4_mem_mb | 15.604 | 18.9 |
| ctr_eb_pgbouncer_cpu_pct | 18.615 | 20.61 |
| ctr_eb_pgbouncer_mem_mb | 16.665 | 17 |
| ctr_eb_postgres_cpu_pct | 803.287 | 902.2 |
| ctr_eb_postgres_mem_mb | 5449.918 | 5573.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.408 | 81.97 |
| in_flight | 5717.122 | 14360 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.78 | 1.83 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 917233.061 | 1556732 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 190917.98 | 329948 |
| octopus_mqtt_sent_total | 293725.408 | 522550 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.816 | 14 |
| pg_active_loads | 2.714 | 30 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 12.776 | 30 |
| pg_conn_idle | 246.347 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.367 | 6 |
| pg_conn_total | 259.122 | 260 |
| pg_db_size_mb | 3356.231 | 3717.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18085.639 | 19307.5 |
| pg_tup_del_per_s | 35625.204 | 46594.3 |
| pg_tup_fetch_per_s | 418482.312 | 463757.5 |
| pg_tup_ins_per_s | 46774.106 | 59634.1 |
| pg_tup_upd_per_s | 34070.951 | 37851.6 |
| pg_wal_mb_per_s | 93.731 | 160.09 |
| pgb_cl_active | 254.714 | 277 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 4.082 | 20 |
| pgb_sv_idle | 43.918 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12855.878 | 13408.1 |
| produced_total | 468450.02 | 777355 |
| publish_mb_per_s | 44.332 | 71.29 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,596 records are Completed |
| record_counts | pass | 117,596 records found, 117,596 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,635 events applied by workers, 814,635 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 65,901 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 517 ms, max 33273 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13565 seq 4: ready 121 ms before start, 101 ms of it with an idle slot in group 429
- [cross_source_blocking] source 13899 seq 17: ready 116 ms before start, 101 ms of it with an idle slot in group 441
- [cross_source_blocking] source 2801 seq 12: ready 140 ms before start, 115 ms of it with an idle slot in group 88
- [cross_source_blocking] source 12043 seq 5: ready 114 ms before start, 114 ms of it with an idle slot in group 385
- [cross_source_blocking] source 6755 seq 27: ready 125 ms before start, 125 ms of it with an idle slot in group 230
- [cross_source_blocking] source 6261 seq 5: ready 134 ms before start, 112 ms of it with an idle slot in group 214
- [cross_source_blocking] source 195 seq 14: ready 124 ms before start, 108 ms of it with an idle slot in group 4
- [cross_source_blocking] source 12039 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 385
- [cross_source_blocking] source 8032 seq 21: ready 177 ms before start, 141 ms of it with an idle slot in group 271
- [cross_source_blocking] source 81 seq 6: ready 175 ms before start, 138 ms of it with an idle slot in group 1
- [cross_source_blocking] source 14145 seq 9: ready 172 ms before start, 168 ms of it with an idle slot in group 450
- [cross_source_blocking] source 6091 seq 6: ready 121 ms before start, 121 ms of it with an idle slot in group 209
- [cross_source_blocking] source 3331 seq 5: ready 124 ms before start, 124 ms of it with an idle slot in group 113
- [cross_source_blocking] source 2719 seq 8: ready 115 ms before start, 115 ms of it with an idle slot in group 83
- [cross_source_blocking] source 12779 seq 4: ready 114 ms before start, 114 ms of it with an idle slot in group 403
- [cross_source_blocking] source 12231 seq 7: ready 189 ms before start, 159 ms of it with an idle slot in group 389
- [cross_source_blocking] source 6290 seq 7: ready 148 ms before start, 132 ms of it with an idle slot in group 214
- [cross_source_blocking] source 13541 seq 3: ready 131 ms before start, 110 ms of it with an idle slot in group 429
- [cross_source_blocking] source 13759 seq 15: ready 203 ms before start, 189 ms of it with an idle slot in group 438
- [cross_source_blocking] source 7934 seq 7: ready 169 ms before start, 115 ms of it with an idle slot in group 268

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 2670.591 | 35127.295 | 35389.439 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2752.511 | 33423.359 | 33554.431 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 89.087 | 30539.775 | 33816.575 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 154.623 | 29491.199 | 35127.295 |
| t-019 | 38 | 2 | 88.646 | 55.998 | 208 | 0 | 3501 | 3501 | 0 | 52.735 | 29229.055 | 32636.927 |
| t-041 | 34 | 3 | 194.475 | 109.547 | 2732 | 0 | 6577 | 6577 | 0 | 54.271 | 28573.695 | 32767.999 |
| t-120 | 37 | 2 | 83.329 | 50.593 | 1412 | 0 | 3112 | 3112 | 0 | 55.039 | 28442.623 | 32112.639 |
| t-029 | 28 | 2 | 143.274 | 66.438 | 1732 | 0 | 4238 | 4238 | 0 | 57.343 | 28180.479 | 30670.847 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 4971 | 4971 | 0 | 64.767 | 28180.479 | 31981.567 |
| t-230 | 30 | 2 | 104.35 | 51.662 | 1033 | 0 | 3112 | 3112 | 0 | 53.503 | 28049.407 | 30408.703 |
| t-371 | 51 | 2 | 63.629 | 55.833 | 3301 | 0 | 3426 | 3426 | 0 | 50.687 | 28049.407 | 31195.135 |
| t-045 | 47 | 2 | 82.615 | 64.377 | 1398 | 0 | 3964 | 3964 | 0 | 54.015 | 27787.263 | 31588.351 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 0 | 79.359 | 27656.191 | 33423.359 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 4365 | 4365 | 0 | 57.855 | 27656.191 | 32112.639 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 5309 | 5309 | 0 | 72.191 | 27525.119 | 31981.567 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 4616 | 4616 | 0 | 65.535 | 27525.119 | 33161.215 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 0 | 60.415 | 27262.975 | 33030.143 |
| t-132 | 22 | 2 | 150.822 | 51.287 | 2676 | 0 | 3045 | 3045 | 0 | 60.415 | 27131.903 | 30932.991 |
| t-378 | 55 | 2 | 85.868 | 76.933 | 3659 | 0 | 4701 | 4701 | 0 | 60.159 | 27131.903 | 32112.639 |
| t-441 | 30 | 2 | 141.287 | 68.521 | 2203 | 0 | 4264 | 4264 | 0 | 59.647 | 27131.903 | 31195.135 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 79.359 | 26738.687 | 31457.279 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 12254 | 0 | 88.575 | 26607.615 | 34078.719 |
| t-004 | 38 | 2 | 87.173 | 57.085 | 2178 | 0 | 3628 | 3628 | 0 | 55.039 | 26345.471 | 31588.351 |
| t-186 | 34 | 2 | 103.912 | 58.611 | 1259 | 0 | 3628 | 3628 | 0 | 54.527 | 26345.471 | 30408.703 |
| t-465 | 66 | 2 | 70.741 | 76.327 | 667 | 0 | 4733 | 4733 | 0 | 57.855 | 25952.255 | 33423.359 |
