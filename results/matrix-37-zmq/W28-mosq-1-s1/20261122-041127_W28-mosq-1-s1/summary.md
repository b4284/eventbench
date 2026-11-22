# W28-mosq-1-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.304s, closing 1.42s, drain 128.94s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 387932 | 387932 | 6314.968 | 6308.58 | 10026.4 | 0 | 0 | 0 | 70 | 1891 | 22017 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 265719 | 17360.397 | 17858.559 | 25214.975 | 28590.079 | 30523.391 | 32866.303 |
| e2e_from_send | 265719 | 498.796 | 434.431 | 830.975 | 2003.967 | 3278.847 | 6524.927 |
| producer_lag | 265719 | 16861.606 | 17416.191 | 24690.687 | 27836.415 | 29786.111 | 31260.671 |
| publish_call | 387932 | 322.522 | 269.567 | 632.831 | 1125.375 | 1552.383 | 2197.503 |
| queue_wait | 265719 | 424.987 | 368.127 | 740.351 | 1579.007 | 2932.735 | 6520.831 |
| broker_dwell | 265719 | 118.58 | 49.087 | 274.687 | 1241.087 | 2664.447 | 5959.679 |
| receive_call | 344960 | 46.956 | 22.735 | 119.999 | 332.799 | 467.455 | 505.599 |
| ctx_load | 265719 | 16.837 | 11.815 | 32.175 | 130.943 | 161.535 | 454.911 |
| process | 265719 | 10.257 | 0.001 | 0.002 | 0.007 | 2584.575 | 2996.223 |
| ctx_commit | 265719 | 46.687 | 32.543 | 87.615 | 314.367 | 480.767 | 1298.431 |
| ack | 209739 | 0.002 | 0.001 | 0.003 | 0.008 | 0.025 | 0.115 |
| service | 265719 | 73.809 | 49.023 | 120.191 | 361.983 | 2660.351 | 3762.175 |
| e2e_large | 46 | 19254.138 | 18972.671 | 26394.623 | 30261.247 | 30261.247 | 30261.247 |
| ready_wait | 265719 | 44.396 | 22.975 | 109.439 | 305.151 | 537.087 | 1919.999 |
| blocked | 265719 | 38.464 | 22.031 | 89.407 | 248.959 | 474.879 | 1027.583 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6308.58 | 9431.6 |
| applied_total | 209723.94 | 370556 |
| bench_cpu_cores | 1.56 | 2.396 |
| bench_cpu_pct | 15.597 | 23.96 |
| bench_gc_gen0 | 0.32 | 2 |
| bench_gc_gen1 | 0.2 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 1169.514 | 2084.4 |
| bench_mem_mb | 2030.106 | 2412 |
| bench_threadpool_queue | 0.08 | 2 |
| bench_threadpool_threads | 20.18 | 25 |
| broker_backlog | 62093.44 | 120747 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1020.12 | 2529 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 428104.98 | 752888 |
| octopus_missing_reports_total | 17221.58 | 31157 |
| octopus_mqtt_received_total | 107999.98 | 175258 |
| octopus_mqtt_sent_total | 157536.1 | 273991 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.8 | 35 |
| pg_active_loads | 14.48 | 75 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 55.46 | 92 |
| pg_conn_idle | 209.58 | 238 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 9.2 | 55 |
| pg_conn_total | 265.04 | 266 |
| pg_db_size_mb | 3655.65 | 4287.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24038.596 | 28377.8 |
| pg_tup_del_per_s | 31523.842 | 44428.5 |
| pg_tup_fetch_per_s | 326431.222 | 404873.4 |
| pg_tup_ins_per_s | 43349.716 | 49188.5 |
| pg_tup_upd_per_s | 51099.934 | 59407.4 |
| pg_wal_mb_per_s | 118.764 | 186.68 |
| produced_per_s | 6314.968 | 9880.5 |
| produced_total | 210744.06 | 371338 |
| publish_mb_per_s | 15.673 | 26.81 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 179,815 records are Completed |
| record_counts | pass | 179,815 records found, 54,409 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 345 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 387,932 events applied by workers, 387,932 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 22,017 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 249 ms, max 1027 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9182 seq 24: ready 205 ms before start, 135 ms of it with an idle slot in group 315
- [cross_source_blocking] source 3003 seq 16: ready 227 ms before start, 104 ms of it with an idle slot in group 100
- [cross_source_blocking] source 10516 seq 27: ready 211 ms before start, 155 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10420 seq 21: ready 143 ms before start, 131 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10356 seq 17: ready 143 ms before start, 131 ms of it with an idle slot in group 347
- [cross_source_blocking] source 14995 seq 33: ready 141 ms before start, 129 ms of it with an idle slot in group 469
- [cross_source_blocking] source 1791 seq 16: ready 268 ms before start, 116 ms of it with an idle slot in group 51
- [cross_source_blocking] source 7518 seq 12: ready 301 ms before start, 119 ms of it with an idle slot in group 259
- [cross_source_blocking] source 10372 seq 25: ready 144 ms before start, 132 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10404 seq 30: ready 143 ms before start, 131 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10612 seq 26: ready 170 ms before start, 156 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10340 seq 25: ready 142 ms before start, 137 ms of it with an idle slot in group 347
- [cross_source_blocking] source 2508 seq 33: ready 152 ms before start, 132 ms of it with an idle slot in group 75
- [cross_source_blocking] source 8390 seq 16: ready 162 ms before start, 118 ms of it with an idle slot in group 285
- [cross_source_blocking] source 10608 seq 21: ready 171 ms before start, 156 ms of it with an idle slot in group 348
- [cross_source_blocking] source 1520 seq 30: ready 168 ms before start, 141 ms of it with an idle slot in group 45
- [cross_source_blocking] source 12555 seq 4: ready 200 ms before start, 148 ms of it with an idle slot in group 397
- [cross_source_blocking] source 15043 seq 30: ready 120 ms before start, 120 ms of it with an idle slot in group 469
- [cross_source_blocking] source 8976 seq 10: ready 166 ms before start, 112 ms of it with an idle slot in group 309
- [cross_source_blocking] source 1488 seq 39: ready 123 ms before start, 120 ms of it with an idle slot in group 45

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-133 | 6 | 1 | 60.083 | 5.815 | 273 | 0 | 216 | 216 | 0 | 18350.079 | 30539.775 | 31588.351 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 69 | 69 | 0 | 17563.647 | 30277.631 | 30277.631 |
| t-475 | 6 | 1 | 28.222 | 2.673 | 3771 | 0 | 116 | 116 | 0 | 17956.863 | 30146.559 | 30277.631 |
| t-492 | 6 | 1 | 56.484 | 5.146 | 3046 | 0 | 198 | 198 | 0 | 18087.935 | 29753.343 | 29884.415 |
| t-164 | 7 | 1 | 221.485 | 23.812 | 236 | 0 | 846 | 846 | 0 | 18612.223 | 29491.199 | 31588.351 |
| t-243 | 16 | 1 | 128.845 | 34.514 | 3143 | 0 | 1282 | 1282 | 0 | 18350.079 | 29229.055 | 30539.775 |
| t-299 | 21 | 1 | 91.91 | 31.431 | 646 | 0 | 1143 | 1143 | 0 | 17170.431 | 29229.055 | 30146.559 |
| t-413 | 19 | 1 | 150.868 | 49.856 | 3585 | 0 | 1793 | 1793 | 0 | 17694.719 | 29229.055 | 31588.351 |
| t-437 | 9 | 1 | 125.813 | 18.889 | 1067 | 0 | 699 | 699 | 0 | 18350.079 | 29229.055 | 30408.703 |
| t-141 | 7 | 1 | 239.018 | 28.012 | 134 | 0 | 994 | 994 | 0 | 19005.439 | 29097.983 | 31719.423 |
| t-148 | 6 | 1 | 136.453 | 12.472 | 715 | 0 | 471 | 471 | 0 | 15466.495 | 29097.983 | 30146.559 |
| t-195 | 9 | 1 | 160.79 | 25.652 | 2299 | 0 | 917 | 917 | 0 | 18743.295 | 29097.983 | 31195.135 |
| t-363 | 19 | 2 | 202.118 | 65.584 | 639 | 0 | 2392 | 2392 | 0 | 17956.863 | 29097.983 | 31588.351 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 9875 | 9875 | 0 | 17956.863 | 29097.983 | 31719.423 |
| t-059 | 8 | 1 | 138.471 | 18.307 | 1449 | 0 | 635 | 635 | 0 | 17825.791 | 28966.911 | 30146.559 |
| t-075 | 37 | 3 | 163.802 | 101.175 | 232 | 0 | 3776 | 3776 | 0 | 17825.791 | 28966.911 | 30670.847 |
| t-140 | 13 | 1 | 137.523 | 30.094 | 429 | 0 | 1134 | 1134 | 0 | 17039.359 | 28966.911 | 29229.055 |
| t-173 | 12 | 1 | 91.453 | 19.263 | 3493 | 0 | 784 | 784 | 0 | 18087.935 | 28966.911 | 30277.631 |
| t-196 | 21 | 2 | 204.746 | 72.365 | 2244 | 0 | 2657 | 2657 | 0 | 17694.719 | 28966.911 | 30277.631 |
| t-259 | 70 | 1 | 39.136 | 44.806 | 160 | 0 | 1754 | 1754 | 0 | 18087.935 | 28966.911 | 31064.063 |
| t-277 | 13 | 1 | 62.924 | 13.814 | 2307 | 0 | 518 | 518 | 0 | 17039.359 | 28966.911 | 31719.423 |
| t-284 | 12 | 1 | 60.548 | 11.512 | 217 | 0 | 449 | 449 | 0 | 18087.935 | 28966.911 | 29622.271 |
| t-396 | 17 | 1 | 89.956 | 25.946 | 210 | 0 | 900 | 900 | 0 | 18612.223 | 28966.911 | 31850.495 |
| t-421 | 6 | 1 | 141.184 | 14.431 | 758 | 0 | 512 | 512 | 0 | 17563.647 | 28966.911 | 31326.207 |
| t-445 | 15 | 1 | 41.842 | 10.067 | 896 | 0 | 371 | 371 | 0 | 17039.359 | 28966.911 | 30146.559 |
