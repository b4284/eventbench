# Kb-mysql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.241s, closing 1.548s, drain 180.007s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 473137 | 336972 | 6412.28 | 4569.161 | 17538.3 | 0 | 0 | 0 | 260 | 2389 | 8714 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 196303 | 22258.556 | 22790.143 | 34275.327 | 35586.047 | 36405.247 | 38731.775 |
| e2e_from_send | 196303 | 308.693 | 237.695 | 666.111 | 1210.367 | 2961.407 | 4825.087 |
| producer_lag | 274019 | 22062.554 | 22478.847 | 34111.487 | 35356.671 | 35913.727 | 36896.767 |
| publish_call | 473137 | 229.351 | 158.591 | 556.543 | 916.991 | 1199.103 | 1208.319 |
| queue_wait | 196303 | 297.297 | 235.007 | 656.383 | 1137.663 | 2260.991 | 4820.991 |
| broker_dwell | 196303 | 19.746 | 5.271 | 23.567 | 205.695 | 2049.023 | 4812.799 |
| receive_call | 280935 | 90.551 | 25.967 | 298.751 | 472.831 | 496.895 | 556.031 |
| ctx_load | 196303 | 0.002 | 0.001 | 0.003 | 0.007 | 0.045 | 12.655 |
| process | 196303 | 11.374 | 0.001 | 0.002 | 0.013 | 2646.015 | 3018.751 |
| ctx_commit | 196303 | 0.002 | 0.001 | 0.001 | 0.004 | 0.021 | 45.151 |
| ack | 190877 | 0.004 | 0.002 | 0.004 | 0.013 | 0.067 | 45.119 |
| service | 196303 | 11.396 | 0.011 | 0.032 | 0.131 | 2646.015 | 3018.751 |
| e2e_large | 137 | 23103.966 | 22167.551 | 36306.943 | 37617.663 | 37912.575 | 37912.575 |
| ready_wait | 196303 | 22.481 | 3.297 | 23.711 | 511.487 | 970.751 | 1111.039 |
| blocked | 196303 | 22.481 | 3.297 | 23.711 | 511.487 | 970.751 | 1111.039 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4569.161 | 17538.3 |
| applied_total | 194879.49 | 311880 |
| bench_cpu_cores | 1.082 | 3.426 |
| bench_cpu_pct | 4.507 | 14.28 |
| bench_gc_gen0 | 1.51 | 6 |
| bench_gc_gen1 | 0.918 | 4 |
| bench_gc_gen2 | 0.571 | 3 |
| bench_gc_heap_mb | 710.247 | 954.2 |
| bench_mem_mb | 1266.647 | 1389 |
| bench_threadpool_queue | 6.204 | 119 |
| bench_threadpool_threads | 44.714 | 45 |
| broker_backlog | 65852.469 | 101706 |
| ctr_eb_mosquitto_cpu_pct | 14.642 | 25.3 |
| ctr_eb_mosquitto_mem_mb | 6.278 | 7.1 |
| ctr_eb_mssql_cpu_pct | 1.483 | 1.92 |
| ctr_eb_mssql_mem_mb | 7352.451 | 7352.8 |
| ctr_eb_mysql_cpu_pct | 236.39 | 326.78 |
| ctr_eb_mysql_mem_mb | 9904 | 9975.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.5 | 14.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 14.5 | 14.5 |
| ctr_eb_postgres_cpu_pct | 0.447 | 1.59 |
| ctr_eb_postgres_mem_mb | 2354.378 | 2365.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 20.838 | 53.33 |
| in_flight | 77727.51 | 125083 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.488 | 0.85 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 481446.755 | 713361 |
| octopus_missing_reports_total | 5085.816 | 5402 |
| octopus_mqtt_received_total | 62392.735 | 89134 |
| octopus_mqtt_sent_total | 106532.653 | 161373 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 6412.28 | 25097.1 |
| produced_total | 272607 | 436963 |
| publish_mb_per_s | 23.812 | 86.46 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 10569/16000 source cursors at the expected last sequence |
| records_complete | pass | all 54,055 records are Completed |
| record_counts | **FAIL** | 54,055 records found, 77,179 expected; 5431 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | **FAIL** | 216 uploads verified, 24 sources mismatched |
| applied_equals_generated | **FAIL** | 336,972 events applied by workers, 473,137 generated |
| broker_drained | **FAIL** | 123,289 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 8,714 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 511 ms, max 1110 ms) |

Sample mismatches:

- source 11: cursor at seq 0, expected 28 (28 events not applied)
- source 11: 0 records (0 completed), expected 5 (5 completed)
- source 16: cursor at seq 0, expected 13 (13 events not applied)
- source 16: 0 records (0 completed), expected 4 (4 completed)
- source 18: cursor at seq 0, expected 18 (18 events not applied)
- source 18: 0 records (0 completed), expected 3 (3 completed)
- source 19: cursor at seq 0, expected 13 (13 events not applied)
- source 19: 0 records (0 completed), expected 4 (4 completed)
- source 21: cursor at seq 0, expected 17 (17 events not applied)
- source 21: 0 records (0 completed), expected 2 (2 completed)
- source 22: cursor at seq 0, expected 20 (20 events not applied)
- source 22: 0 records (0 completed), expected 4 (4 completed)
- source 25: cursor at seq 0, expected 27 (27 events not applied)
- source 25: 0 records (0 completed), expected 5 (5 completed)
- source 30: cursor at seq 0, expected 25 (25 events not applied)
- source 30: 0 records (0 completed), expected 3 (3 completed)
- source 35: cursor at seq 0, expected 16 (16 events not applied)
- source 35: 0 records (0 completed), expected 3 (3 completed)
- source 37: cursor at seq 0, expected 16 (16 events not applied)
- source 37: 0 records (0 completed), expected 5 (5 completed)

## Cross-source blocking samples

- [cross_source_blocking] source 7714 seq 15: ready 387 ms before start, 387 ms of it with an idle slot in group 263
- [cross_source_blocking] source 13410 seq 6: ready 388 ms before start, 388 ms of it with an idle slot in group 429
- [cross_source_blocking] source 6178 seq 17: ready 199 ms before start, 199 ms of it with an idle slot in group 210
- [cross_source_blocking] source 3202 seq 42: ready 181 ms before start, 181 ms of it with an idle slot in group 106
- [cross_source_blocking] source 14018 seq 4: ready 224 ms before start, 224 ms of it with an idle slot in group 446
- [cross_source_blocking] source 194 seq 26: ready 245 ms before start, 245 ms of it with an idle slot in group 4
- [cross_source_blocking] source 34 seq 8: ready 175 ms before start, 175 ms of it with an idle slot in group 1
- [cross_source_blocking] source 11522 seq 51: ready 174 ms before start, 174 ms of it with an idle slot in group 374
- [cross_source_blocking] source 4674 seq 2: ready 137 ms before start, 137 ms of it with an idle slot in group 162
- [cross_source_blocking] source 4994 seq 19: ready 137 ms before start, 137 ms of it with an idle slot in group 172
- [cross_source_blocking] source 11298 seq 2: ready 137 ms before start, 137 ms of it with an idle slot in group 368
- [cross_source_blocking] source 5442 seq 41: ready 137 ms before start, 137 ms of it with an idle slot in group 186
- [cross_source_blocking] source 2530 seq 22: ready 136 ms before start, 136 ms of it with an idle slot in group 75
- [cross_source_blocking] source 14050 seq 8: ready 137 ms before start, 137 ms of it with an idle slot in group 446
- [cross_source_blocking] source 10050 seq 11: ready 137 ms before start, 137 ms of it with an idle slot in group 343
- [cross_source_blocking] source 11620 seq 23: ready 331 ms before start, 331 ms of it with an idle slot in group 378
- [cross_source_blocking] source 7740 seq 11: ready 331 ms before start, 331 ms of it with an idle slot in group 263
- [cross_source_blocking] source 9116 seq 12: ready 331 ms before start, 331 ms of it with an idle slot in group 315
- [cross_source_blocking] source 5596 seq 19: ready 331 ms before start, 331 ms of it with an idle slot in group 194
- [cross_source_blocking] source 10204 seq 20: ready 329 ms before start, 329 ms of it with an idle slot in group 344
- [cross_source_blocking] source 6116 seq 12: ready 321 ms before start, 321 ms of it with an idle slot in group 210

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 240 | 207 | 0 | 22282.239 | 37748.735 | 38010.879 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 409 | 356 | 0 | 23068.671 | 37486.591 | 38797.311 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 1068 | 1068 | 0 | 23068.671 | 37486.591 | 38797.311 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 444 | 379 | 0 | 26607.615 | 36962.303 | 37224.447 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 53 | 39 | 0 | 29753.343 | 36962.303 | 36962.303 |
| t-017 | 19 | 1 | 36.957 | 11.447 | 830 | 0 | 386 | 184 | 0 | 28835.839 | 36700.159 | 36700.159 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 63 | 57 | 0 | 23330.815 | 36700.159 | 36700.159 |
| t-224 | 9 | 1 | 37.952 | 5.826 | 1098 | 0 | 183 | 65 | 0 | 25034.751 | 36700.159 | 36700.159 |
| t-298 | 6 | 1 | 79.466 | 8.739 | 3700 | 0 | 246 | 176 | 0 | 23986.175 | 36700.159 | 36962.303 |
| t-020 | 22 | 1 | 78.895 | 27.797 | 981 | 0 | 825 | 483 | 0 | 23199.743 | 36438.015 | 37486.591 |
| t-051 | 31 | 1 | 52.054 | 26.084 | 200 | 0 | 762 | 550 | 0 | 21233.663 | 36438.015 | 37224.447 |
| t-057 | 17 | 1 | 79.959 | 23.695 | 290 | 0 | 654 | 434 | 0 | 25165.823 | 36438.015 | 36962.303 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 107 | 79 | 0 | 18612.223 | 36438.015 | 36438.015 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 418 | 342 | 0 | 22544.383 | 36438.015 | 36700.159 |
| t-449 | 14 | 1 | 52.807 | 12.496 | 290 | 0 | 377 | 335 | 0 | 18874.367 | 36438.015 | 36962.303 |
| t-038 | 9 | 1 | 55.117 | 8.286 | 2056 | 0 | 228 | 205 | 0 | 26738.687 | 36175.871 | 36175.871 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 338 | 273 | 0 | 27262.975 | 36175.871 | 36175.871 |
| t-081 | 6 | 1 | 16.193 | 1.614 | 2170 | 0 | 60 | 39 | 0 | 27787.263 | 36175.871 | 36175.871 |
| t-086 | 6 | 1 | 53.883 | 5.925 | 464 | 0 | 193 | 95 | 0 | 17170.431 | 36175.871 | 36175.871 |
| t-108 | 32 | 1 | 72.27 | 39.424 | 694 | 0 | 1135 | 945 | 0 | 23724.031 | 36175.871 | 37748.735 |
| t-116 | 24 | 1 | 65.702 | 25.166 | 3734 | 0 | 709 | 432 | 0 | 21889.023 | 36175.871 | 36175.871 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 1776 | 1162 | 0 | 22544.383 | 36175.871 | 37224.447 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 446 | 173 | 0 | 25034.751 | 36175.871 | 36175.871 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 1477 | 849 | 0 | 23724.031 | 36175.871 | 36175.871 |
| t-184 | 20 | 1 | 98.647 | 32.052 | 161 | 0 | 889 | 593 | 0 | 19267.583 | 36175.871 | 36700.159 |
