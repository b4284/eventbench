# O1-origins — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.568s, closing 2.236s, drain 8.651s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 927789 | 927789 | 14577.278 | 13650.808 | 21026.6 | 0 | 0 | 0 | 433 | 4802 | 492283 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 724800 | 6565.971 | 4118.527 | 17203.199 | 26918.911 | 32145.407 | 41058.303 |
| e2e_from_send | 724800 | 5973.322 | 3016.703 | 16793.599 | 26902.527 | 32129.023 | 41058.303 |
| producer_lag | 724800 | 592.652 | 13.735 | 2699.263 | 4800.511 | 5435.391 | 6324.223 |
| publish_call | 927789 | 88.664 | 65.407 | 188.159 | 379.391 | 578.559 | 934.911 |
| queue_wait | 724800 | 5896.715 | 2928.639 | 16728.063 | 26853.375 | 32063.487 | 41025.535 |
| broker_dwell | 724800 | 5832.091 | 2869.247 | 16662.527 | 26820.607 | 32030.719 | 40927.231 |
| receive_call | 762783 | 37.778 | 14.879 | 97.279 | 342.015 | 478.975 | 675.839 |
| ctx_load | 724800 | 15.84 | 11.399 | 32.959 | 68.095 | 152.447 | 350.975 |
| process | 724800 | 10.843 | 0.002 | 0.004 | 0.08 | 2623.487 | 3231.743 |
| ctx_commit | 724800 | 49.615 | 40.223 | 100.671 | 187.007 | 357.375 | 564.735 |
| ack | 724772 | 0.012 | 0.004 | 0.007 | 0.056 | 1.483 | 54.111 |
| service | 724800 | 76.605 | 57.823 | 123.903 | 244.863 | 2693.119 | 3393.535 |
| e2e_large | 331 | 3387.663 | 3055.615 | 5898.239 | 7778.303 | 9576.447 | 9576.447 |
| ready_wait | 724800 | 469.892 | 310.271 | 1118.207 | 2430.975 | 3690.495 | 4739.071 |
| blocked | 724800 | 356.019 | 244.351 | 846.847 | 1630.207 | 2439.167 | 2967.551 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13650.808 | 18767 |
| applied_total | 482397.347 | 795581 |
| bench_cpu_cores | 5.109 | 6.228 |
| bench_cpu_pct | 21.286 | 25.95 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 0.673 | 2 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1402.329 | 1859 |
| bench_mem_mb | 2184.314 | 2413.8 |
| bench_threadpool_queue | 193.367 | 1049 |
| bench_threadpool_threads | 42.714 | 44 |
| broker_backlog | 85835.041 | 116972 |
| ctr_eb_mosquitto_cpu_pct | 61.142 | 72.1 |
| ctr_eb_mosquitto_mem_mb | 9.694 | 31 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.992 | 23.18 |
| ctr_eb_pgbouncer_2_mem_mb | 15.198 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.832 | 24.2 |
| ctr_eb_pgbouncer_3_mem_mb | 15.014 | 15.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.652 | 24.11 |
| ctr_eb_pgbouncer_4_mem_mb | 15.157 | 16.2 |
| ctr_eb_pgbouncer_cpu_pct | 19.991 | 23.14 |
| ctr_eb_pgbouncer_mem_mb | 15.259 | 16.4 |
| ctr_eb_postgres_cpu_pct | 952.438 | 1167.8 |
| ctr_eb_postgres_mem_mb | 5908.273 | 6173.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.396 | 82.7 |
| in_flight | 83119.163 | 110178 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.87 | 1.98 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1028697.571 | 1672098 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 210774.939 | 337550 |
| octopus_mqtt_sent_total | 249291.776 | 426963 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.367 | 14 |
| pg_active_loads | 9.02 | 39 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.982 | 100 |
| pg_conn_active | 36.286 | 96 |
| pg_conn_idle | 223.694 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.531 | 11 |
| pg_conn_total | 259.98 | 262 |
| pg_db_size_mb | 3782.31 | 4832.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16155.98 | 22523.3 |
| pg_tup_del_per_s | 26180.247 | 36948.5 |
| pg_tup_fetch_per_s | 376824.659 | 451601.9 |
| pg_tup_ins_per_s | 38698.841 | 53461.6 |
| pg_tup_upd_per_s | 36147.39 | 48459.1 |
| pg_wal_mb_per_s | 105.446 | 224.49 |
| pgb_cl_active | 245.816 | 262 |
| pgb_cl_waiting | 9.265 | 106 |
| pgb_maxwait_ms | 1.256 | 15.12 |
| pgb_sv_active | 16.592 | 48 |
| pgb_sv_idle | 31.408 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14577.278 | 18942 |
| produced_total | 565516.51 | 887789 |
| publish_mb_per_s | 47.781 | 88.72 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 130,518 records are Completed |
| record_counts | pass | 130,518 records found, 130,518 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 433 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 927,789 events applied by workers, 927,789 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 492,283 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1630 ms, max 2967 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3102 seq 15: ready 123 ms before start, 103 ms of it with an idle slot in group 103
- [cross_source_blocking] source 15375 seq 8: ready 165 ms before start, 165 ms of it with an idle slot in group 480
- [cross_source_blocking] source 8479 seq 10: ready 207 ms before start, 207 ms of it with an idle slot in group 289
- [cross_source_blocking] source 10623 seq 9: ready 167 ms before start, 167 ms of it with an idle slot in group 349
- [cross_source_blocking] source 8261 seq 15: ready 208 ms before start, 208 ms of it with an idle slot in group 280
- [cross_source_blocking] source 6681 seq 10: ready 208 ms before start, 208 ms of it with an idle slot in group 227
- [cross_source_blocking] source 7337 seq 5: ready 207 ms before start, 207 ms of it with an idle slot in group 252
- [cross_source_blocking] source 2329 seq 26: ready 208 ms before start, 208 ms of it with an idle slot in group 67
- [cross_source_blocking] source 8763 seq 17: ready 206 ms before start, 206 ms of it with an idle slot in group 305
- [cross_source_blocking] source 4399 seq 7: ready 207 ms before start, 207 ms of it with an idle slot in group 151
- [cross_source_blocking] source 9295 seq 19: ready 210 ms before start, 205 ms of it with an idle slot in group 320
- [cross_source_blocking] source 13605 seq 10: ready 208 ms before start, 208 ms of it with an idle slot in group 431
- [cross_source_blocking] source 2543 seq 4: ready 166 ms before start, 166 ms of it with an idle slot in group 76
- [cross_source_blocking] source 6057 seq 8: ready 208 ms before start, 208 ms of it with an idle slot in group 207
- [cross_source_blocking] source 13183 seq 5: ready 166 ms before start, 166 ms of it with an idle slot in group 419
- [cross_source_blocking] source 5551 seq 14: ready 181 ms before start, 181 ms of it with an idle slot in group 190
- [cross_source_blocking] source 11279 seq 11: ready 195 ms before start, 109 ms of it with an idle slot in group 367
- [cross_source_blocking] source 14875 seq 7: ready 207 ms before start, 207 ms of it with an idle slot in group 467
- [cross_source_blocking] source 1007 seq 5: ready 209 ms before start, 209 ms of it with an idle slot in group 30
- [cross_source_blocking] source 7407 seq 7: ready 142 ms before start, 142 ms of it with an idle slot in group 256

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 2957 | 2957 | 0 | 14024.703 | 34603.007 | 38273.023 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2655 | 2655 | 0 | 13500.415 | 34078.719 | 38010.879 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 8551 | 8551 | 0 | 15007.743 | 33423.359 | 37224.447 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 5674 | 5674 | 0 | 13303.807 | 33030.143 | 37224.447 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2685 | 2685 | 0 | 13172.735 | 32505.855 | 36175.871 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5249 | 5249 | 0 | 13041.663 | 32505.855 | 35389.439 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 8452 | 8452 | 0 | 15269.887 | 32243.711 | 36438.015 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 2082 | 2082 | 0 | 7143.423 | 32112.639 | 33816.575 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 2894 | 2894 | 0 | 15269.887 | 31850.495 | 37224.447 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2655 | 2655 | 0 | 11206.655 | 31719.423 | 35651.583 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5550 | 5550 | 0 | 13565.951 | 31719.423 | 35651.583 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 10953 | 10953 | 0 | 14548.991 | 31064.063 | 35651.583 |
| t-429 | 200 | 2 | 22.464 | 76.568 | 1395 | 0 | 4765 | 4765 | 0 | 9961.471 | 30932.991 | 41156.607 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2484 | 2484 | 0 | 11730.943 | 30670.847 | 33423.359 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5464 | 5464 | 0 | 14286.847 | 30670.847 | 34340.863 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5165 | 5165 | 0 | 12779.519 | 30539.775 | 34865.151 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 2893 | 2893 | 0 | 12189.695 | 30539.775 | 34078.719 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2766 | 2766 | 0 | 12189.695 | 30408.703 | 32636.927 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 10351 | 10351 | 0 | 13565.951 | 30146.559 | 33554.431 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2598 | 2598 | 0 | 10616.831 | 30146.559 | 32112.639 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2574 | 2574 | 0 | 12582.911 | 30146.559 | 31588.351 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 13610 | 13610 | 0 | 14680.063 | 30015.487 | 31981.567 |
| t-283 | 74 | 1 | 33.797 | 40.793 | 240 | 0 | 2475 | 2475 | 0 | 10747.903 | 29884.415 | 34078.719 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5373 | 5373 | 0 | 12845.055 | 29753.343 | 33816.575 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 19107 | 19107 | 0 | 13172.735 | 29622.271 | 35127.295 |
