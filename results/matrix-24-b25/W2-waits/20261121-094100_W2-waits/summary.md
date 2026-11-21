# W2-waits — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.273s, closing 3.105s, drain 5.958s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 949329 | 949329 | 15106.124 | 14210.767 | 21217.5 | 0 | 0 | 0 | 436 | 4914 | 483641 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 746257 | 3992.464 | 1897.471 | 11493.375 | 21512.191 | 27262.975 | 35291.135 |
| e2e_from_send | 746257 | 3470.061 | 969.727 | 10788.863 | 21479.423 | 27262.975 | 35291.135 |
| producer_lag | 746257 | 522.407 | 18.815 | 2226.175 | 3764.223 | 4366.335 | 5005.311 |
| publish_call | 949329 | 88.78 | 66.751 | 184.319 | 364.543 | 600.575 | 727.551 |
| queue_wait | 746257 | 3397.688 | 887.807 | 10723.327 | 21413.887 | 27213.823 | 35258.367 |
| broker_dwell | 746257 | 3335.786 | 829.951 | 10657.791 | 21381.119 | 27181.055 | 35258.367 |
| receive_call | 778530 | 37.734 | 14.775 | 97.151 | 335.615 | 482.559 | 817.151 |
| ctx_load | 746257 | 14.906 | 10.943 | 31.055 | 66.623 | 101.055 | 349.695 |
| process | 746257 | 10.831 | 0.002 | 0.004 | 0.072 | 2621.439 | 3143.679 |
| ctx_commit | 746257 | 46.383 | 37.503 | 93.183 | 166.655 | 358.399 | 500.735 |
| ack | 746237 | 0.012 | 0.004 | 0.007 | 0.05 | 1.238 | 66.239 |
| service | 746257 | 72.369 | 53.663 | 113.855 | 239.231 | 2691.071 | 3342.335 |
| e2e_large | 334 | 3193.49 | 2971.647 | 4980.735 | 6639.615 | 7200.767 | 7200.767 |
| ready_wait | 746257 | 393.166 | 252.031 | 942.591 | 2071.551 | 3141.631 | 4206.591 |
| blocked | 746257 | 306.466 | 204.543 | 741.887 | 1406.975 | 2140.159 | 2957.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 14210.767 | 18608.7 |
| applied_total | 519391.061 | 847525 |
| bench_cpu_cores | 5.066 | 6.634 |
| bench_cpu_pct | 21.107 | 27.64 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 0.612 | 2 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 1504.337 | 1971.8 |
| bench_mem_mb | 2220.455 | 2520.1 |
| bench_threadpool_queue | 170.571 | 782 |
| bench_threadpool_threads | 42.51 | 44 |
| broker_backlog | 51769.755 | 83167 |
| ctr_eb_mosquitto_cpu_pct | 63.131 | 71.16 |
| ctr_eb_mosquitto_mem_mb | 7.082 | 12.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.422 | 25.09 |
| ctr_eb_pgbouncer_2_mem_mb | 15.563 | 16.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.802 | 24.21 |
| ctr_eb_pgbouncer_3_mem_mb | 15.198 | 16.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.85 | 24.54 |
| ctr_eb_pgbouncer_4_mem_mb | 15.029 | 15.8 |
| ctr_eb_pgbouncer_cpu_pct | 19.144 | 21.83 |
| ctr_eb_pgbouncer_mem_mb | 15.273 | 16.1 |
| ctr_eb_postgres_cpu_pct | 948.373 | 1179.39 |
| ctr_eb_postgres_mem_mb | 5778.663 | 5968.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.715 | 81.32 |
| in_flight | 49082.551 | 73411 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.761 | 1.93 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1067140.143 | 1783293 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 206630.02 | 341957 |
| octopus_mqtt_sent_total | 278934.571 | 485003 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.898 | 16 |
| pg_active_loads | 4.714 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.997 | 100 |
| pg_conn_active | 34 | 85 |
| pg_conn_idle | 224.347 | 249 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 8.082 | 26 |
| pg_conn_total | 258.347 | 261 |
| pg_db_size_mb | 3730.192 | 4734.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17299.371 | 23676 |
| pg_tup_del_per_s | 27406.076 | 37489.1 |
| pg_tup_fetch_per_s | 392959.88 | 473823.2 |
| pg_tup_ins_per_s | 39719.835 | 57362.5 |
| pg_tup_upd_per_s | 38546.543 | 51300.7 |
| pg_wal_mb_per_s | 107.334 | 156.95 |
| pgb_cl_active | 249.327 | 269 |
| pgb_cl_waiting | 5.551 | 68 |
| pgb_maxwait_ms | 0.349 | 4.28 |
| pgb_sv_active | 14.224 | 48 |
| pgb_sv_idle | 33.776 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15106.124 | 19906.7 |
| produced_total | 568473.612 | 909289 |
| publish_mb_per_s | 49.585 | 88.72 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 132,972 records are Completed |
| record_counts | pass | 132,972 records found, 132,972 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 436 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 949,329 events applied by workers, 949,329 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 483,641 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1407 ms, max 2957 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4882 seq 15: ready 139 ms before start, 101 ms of it with an idle slot in group 169
- [cross_source_blocking] source 13877 seq 10: ready 191 ms before start, 181 ms of it with an idle slot in group 439
- [cross_source_blocking] source 11733 seq 15: ready 215 ms before start, 212 ms of it with an idle slot in group 380
- [cross_source_blocking] source 12565 seq 2: ready 215 ms before start, 215 ms of it with an idle slot in group 397
- [cross_source_blocking] source 3093 seq 16: ready 215 ms before start, 215 ms of it with an idle slot in group 103
- [cross_source_blocking] source 15762 seq 25: ready 233 ms before start, 233 ms of it with an idle slot in group 493
- [cross_source_blocking] source 3794 seq 19: ready 234 ms before start, 234 ms of it with an idle slot in group 128
- [cross_source_blocking] source 1269 seq 7: ready 259 ms before start, 259 ms of it with an idle slot in group 40
- [cross_source_blocking] source 7666 seq 7: ready 214 ms before start, 214 ms of it with an idle slot in group 262
- [cross_source_blocking] source 15733 seq 8: ready 172 ms before start, 172 ms of it with an idle slot in group 491
- [cross_source_blocking] source 8754 seq 15: ready 287 ms before start, 260 ms of it with an idle slot in group 304
- [cross_source_blocking] source 5906 seq 15: ready 214 ms before start, 214 ms of it with an idle slot in group 205
- [cross_source_blocking] source 13010 seq 11: ready 108 ms before start, 108 ms of it with an idle slot in group 414
- [cross_source_blocking] source 11029 seq 18: ready 172 ms before start, 172 ms of it with an idle slot in group 360
- [cross_source_blocking] source 1362 seq 11: ready 233 ms before start, 233 ms of it with an idle slot in group 43
- [cross_source_blocking] source 3797 seq 16: ready 146 ms before start, 113 ms of it with an idle slot in group 128
- [cross_source_blocking] source 15698 seq 27: ready 125 ms before start, 125 ms of it with an idle slot in group 490
- [cross_source_blocking] source 8146 seq 3: ready 159 ms before start, 126 ms of it with an idle slot in group 276
- [cross_source_blocking] source 1205 seq 8: ready 180 ms before start, 150 ms of it with an idle slot in group 40
- [cross_source_blocking] source 885 seq 9: ready 135 ms before start, 109 ms of it with an idle slot in group 27

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3032 | 3032 | 0 | 11206.655 | 30539.775 | 35389.439 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 8738 | 8738 | 0 | 11796.479 | 30408.703 | 34340.863 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2707 | 2707 | 0 | 9437.183 | 29360.127 | 33816.575 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5669 | 5669 | 0 | 10354.687 | 27918.335 | 30801.919 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8645 | 8645 | 0 | 10289.151 | 26345.471 | 31981.567 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 5785 | 5785 | 0 | 8912.895 | 26214.399 | 29884.415 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2817 | 2817 | 0 | 9240.575 | 26214.399 | 27918.335 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5367 | 5367 | 0 | 9109.503 | 26083.327 | 31850.495 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 19545 | 19545 | 0 | 9175.039 | 25821.183 | 31326.207 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5612 | 5612 | 0 | 9961.471 | 25821.183 | 29622.271 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11208 | 11208 | 0 | 9502.719 | 25821.183 | 32112.639 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14387 | 14387 | 0 | 9568.255 | 25690.111 | 31064.063 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2963 | 2963 | 0 | 9437.183 | 25427.967 | 28966.911 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5513 | 5513 | 0 | 8585.215 | 25034.751 | 29097.983 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5267 | 5267 | 0 | 7962.623 | 24903.679 | 27394.047 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 13904 | 13904 | 0 | 9830.399 | 24641.535 | 28442.623 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2747 | 2747 | 0 | 6291.455 | 24379.391 | 27525.119 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5244 | 5244 | 0 | 9568.255 | 24248.319 | 26083.327 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2676 | 2676 | 0 | 6946.815 | 24117.247 | 26214.399 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2703 | 2703 | 0 | 6225.919 | 23986.175 | 27394.047 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 10588 | 10588 | 0 | 8519.679 | 23330.815 | 28311.551 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4701 | 4701 | 0 | 4227.071 | 23068.671 | 26738.687 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 18760 | 18760 | 0 | 7471.103 | 23068.671 | 26476.543 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 15800 | 15800 | 0 | 7634.943 | 22282.239 | 24903.679 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2945 | 2945 | 0 | 7012.351 | 22282.239 | 23724.031 |
