# Kb-mysql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.148s, closing 1.626s, drain 1.655s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000520 | 1000520 | 16440.739 | 16439.551 | 26547.7 | 0 | 0 | 0 | 458 | 5163 | 33810 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797153 | 2501.461 | 1806.335 | 6397.951 | 7671.807 | 8568.831 | 11132.927 |
| e2e_from_send | 797153 | 121.646 | 55.327 | 240.767 | 1454.079 | 2836.479 | 4952.063 |
| producer_lag | 797153 | 2379.814 | 1638.399 | 6250.495 | 7479.295 | 8044.543 | 8757.247 |
| publish_call | 1000520 | 85.634 | 45.599 | 206.719 | 547.839 | 1180.671 | 1214.463 |
| queue_wait | 797153 | 110.894 | 54.943 | 234.623 | 1143.807 | 2435.071 | 4612.095 |
| broker_dwell | 797153 | 34.252 | 6.507 | 38.143 | 801.279 | 2355.199 | 4591.615 |
| receive_call | 834786 | 41.543 | 14.439 | 116.799 | 349.695 | 473.599 | 518.655 |
| ctx_load | 797153 | 0.003 | 0.002 | 0.003 | 0.009 | 0.045 | 32.831 |
| process | 797153 | 10.73 | 0.001 | 0.002 | 0.014 | 2617.343 | 3004.415 |
| ctx_commit | 797153 | 0.002 | 0.001 | 0.001 | 0.004 | 0.022 | 36.575 |
| ack | 775297 | 0.004 | 0.002 | 0.004 | 0.014 | 0.063 | 38.399 |
| service | 797153 | 10.752 | 0.013 | 0.033 | 0.131 | 2619.391 | 3004.415 |
| e2e_large | 356 | 4922.508 | 4435.967 | 8962.047 | 10403.839 | 10985.471 | 10985.471 |
| ready_wait | 797153 | 16.457 | 3.615 | 38.175 | 214.655 | 395.263 | 1312.767 |
| blocked | 797153 | 16.39 | 3.615 | 38.143 | 214.271 | 393.727 | 1210.367 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16439.551 | 26547.7 |
| applied_total | 537835.633 | 961569 |
| bench_cpu_cores | 2.951 | 4.81 |
| bench_cpu_pct | 12.295 | 20.04 |
| bench_gc_gen0 | 2.735 | 5 |
| bench_gc_gen1 | 1.265 | 3 |
| bench_gc_gen2 | 0.776 | 2 |
| bench_gc_heap_mb | 1059.633 | 1364.9 |
| bench_mem_mb | 1651.065 | 1827.4 |
| bench_threadpool_queue | 12.02 | 211 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 5538.347 | 11741 |
| ctr_eb_mosquitto_cpu_pct | 43.894 | 54.94 |
| ctr_eb_mosquitto_mem_mb | 7.331 | 17.3 |
| ctr_eb_mssql_cpu_pct | 2.218 | 2.52 |
| ctr_eb_mssql_mem_mb | 7420.292 | 7422.4 |
| ctr_eb_mysql_cpu_pct | 430.796 | 513.61 |
| ctr_eb_mysql_mem_mb | 10819.551 | 11043.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.4 | 7.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 8.033 | 8.2 |
| ctr_eb_postgres_cpu_pct | 0.323 | 0.54 |
| ctr_eb_postgres_mem_mb | 2345.959 | 2370.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 39.697 | 54.25 |
| in_flight | 545.939 | 1636 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.24 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1081093.469 | 1933342 |
| octopus_missing_reports_total | 295.633 | 1091 |
| octopus_mqtt_received_total | 122637.204 | 229691 |
| octopus_mqtt_sent_total | 250892.571 | 469830 |
| octopus_publish_retries_total | 16872 | 16872 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16440.739 | 26905 |
| produced_total | 538381.571 | 961973 |
| publish_mb_per_s | 53.827 | 81.01 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,520 events applied by workers, 1,000,520 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 33,810 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 214 ms, max 1209 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 242 seq 18: ready 102 ms before start, 102 ms of it with an idle slot in group 5
- [cross_source_blocking] source 8038 seq 21: ready 208 ms before start, 208 ms of it with an idle slot in group 271
- [cross_source_blocking] source 10506 seq 13: ready 208 ms before start, 208 ms of it with an idle slot in group 348
- [cross_source_blocking] source 8074 seq 24: ready 208 ms before start, 208 ms of it with an idle slot in group 271
- [cross_source_blocking] source 2242 seq 31: ready 208 ms before start, 208 ms of it with an idle slot in group 65
- [cross_source_blocking] source 5318 seq 15: ready 208 ms before start, 208 ms of it with an idle slot in group 182
- [cross_source_blocking] source 15686 seq 15: ready 208 ms before start, 208 ms of it with an idle slot in group 490
- [cross_source_blocking] source 14234 seq 21: ready 208 ms before start, 208 ms of it with an idle slot in group 456
- [cross_source_blocking] source 4340 seq 31: ready 272 ms before start, 272 ms of it with an idle slot in group 147
- [cross_source_blocking] source 14116 seq 28: ready 234 ms before start, 234 ms of it with an idle slot in group 450
- [cross_source_blocking] source 3316 seq 17: ready 234 ms before start, 234 ms of it with an idle slot in group 111
- [cross_source_blocking] source 5944 seq 18: ready 235 ms before start, 235 ms of it with an idle slot in group 206
- [cross_source_blocking] source 8856 seq 19: ready 234 ms before start, 234 ms of it with an idle slot in group 306
- [cross_source_blocking] source 4020 seq 8: ready 234 ms before start, 234 ms of it with an idle slot in group 134
- [cross_source_blocking] source 6744 seq 24: ready 235 ms before start, 235 ms of it with an idle slot in group 230
- [cross_source_blocking] source 15636 seq 25: ready 216 ms before start, 216 ms of it with an idle slot in group 487
- [cross_source_blocking] source 9248 seq 9: ready 234 ms before start, 234 ms of it with an idle slot in group 317
- [cross_source_blocking] source 13024 seq 8: ready 235 ms before start, 235 ms of it with an idle slot in group 414
- [cross_source_blocking] source 9088 seq 50: ready 235 ms before start, 235 ms of it with an idle slot in group 314
- [cross_source_blocking] source 8788 seq 22: ready 235 ms before start, 235 ms of it with an idle slot in group 306
- [cross_source_blocking] source 15528 seq 5: ready 235 ms before start, 235 ms of it with an idle slot in group 483

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 4112.383 | 10485.759 | 11010.047 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 3407.871 | 10027.007 | 10420.223 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 3571.711 | 9502.719 | 9633.791 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 3801.087 | 9043.967 | 9109.503 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 2572.287 | 9043.967 | 10092.543 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 739 | 739 | 0 | 2785.279 | 8847.359 | 10616.831 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 376 | 376 | 0 | 1933.311 | 8650.751 | 9699.327 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 260 | 260 | 0 | 1990.655 | 8585.215 | 9043.967 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 2200 | 2200 | 0 | 2441.215 | 8388.607 | 10092.543 |
| t-166 | 6 | 1 | 32.396 | 3.113 | 292 | 0 | 187 | 187 | 0 | 2244.607 | 8290.303 | 8716.287 |
| t-424 | 8 | 1 | 94.679 | 12.424 | 1700 | 0 | 746 | 746 | 0 | 2056.191 | 8159.231 | 10223.615 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 438 | 438 | 0 | 1916.927 | 8126.463 | 8650.751 |
| t-336 | 11 | 1 | 50.302 | 9.444 | 1886 | 0 | 624 | 624 | 0 | 2408.447 | 8126.463 | 10223.615 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 1484 | 1484 | 0 | 2023.423 | 8093.695 | 8585.215 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 2642 | 2642 | 0 | 2228.223 | 8060.927 | 9764.863 |
| t-433 | 6 | 1 | 26.326 | 2.618 | 184 | 0 | 182 | 182 | 0 | 2228.223 | 8060.927 | 8257.535 |
| t-156 | 9 | 1 | 45.728 | 6.738 | 968 | 0 | 397 | 397 | 0 | 2392.063 | 8028.159 | 8454.143 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 1736.703 | 8028.159 | 9633.791 |
| t-453 | 6 | 1 | 55.729 | 5.489 | 874 | 0 | 356 | 356 | 0 | 1507.327 | 8028.159 | 8355.839 |
| t-457 | 18 | 1 | 29.452 | 9.207 | 1557 | 0 | 608 | 608 | 0 | 1933.311 | 8028.159 | 9175.039 |
| t-216 | 19 | 1 | 75.625 | 24.458 | 3889 | 0 | 1577 | 1577 | 0 | 2113.535 | 7995.391 | 9895.935 |
| t-405 | 6 | 1 | 30.308 | 3.005 | 874 | 0 | 187 | 187 | 0 | 473.087 | 7995.391 | 8093.695 |
| t-481 | 12 | 1 | 150.551 | 28.385 | 1993 | 0 | 1822 | 1822 | 0 | 1941.503 | 7995.391 | 8716.287 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 2113.535 | 7962.623 | 9830.399 |
| t-063 | 8 | 1 | 24.376 | 3.378 | 1296 | 0 | 217 | 217 | 0 | 3047.423 | 7962.623 | 8224.767 |
