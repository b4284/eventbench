# Lb-pg — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.105s, closing 0.599s, drain 2.754s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000536 | 1000536 | 15942.961 | 15944.441 | 38481.4 | 0 | 0 | 0 | 458 | 5163 | 4812 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797173 | 38.028 | 7.151 | 20.495 | 1295.359 | 2752.511 | 6569.983 |
| e2e_from_send | 797173 | 36.854 | 6.323 | 18.383 | 1294.335 | 2750.463 | 6569.983 |
| producer_lag | 797173 | 1.173 | 0.791 | 2.715 | 6.043 | 24.367 | 100.799 |
| publish_call | 1000536 | 7.109 | 5.775 | 12.095 | 43.039 | 87.167 | 137.727 |
| queue_wait | 797173 | 26.113 | 6.295 | 17.311 | 661.503 | 2285.567 | 4673.535 |
| broker_dwell | 797173 | 20.226 | 0.665 | 7.631 | 654.335 | 2279.423 | 4665.343 |
| receive_call | 953199 | 36.229 | 13.807 | 93.631 | 322.047 | 472.575 | 507.391 |
| ctx_load | 797173 | 0.002 | 0.002 | 0.003 | 0.011 | 0.035 | 9.351 |
| process | 797173 | 10.725 | 0.001 | 0.002 | 0.015 | 2617.343 | 3006.463 |
| ctx_commit | 797173 | 0.002 | 0.001 | 0.001 | 0.004 | 0.022 | 20.783 |
| ack | 794196 | 0.003 | 0.002 | 0.004 | 0.015 | 0.047 | 33.823 |
| service | 797173 | 10.741 | 0.013 | 0.027 | 0.08 | 2617.343 | 3006.463 |
| e2e_large | 356 | 2608.597 | 2566.143 | 3602.431 | 5267.455 | 6569.983 | 6569.983 |
| ready_wait | 797173 | 3.675 | 0.646 | 3.483 | 73.535 | 224.127 | 1870.847 |
| blocked | 797173 | 3.596 | 0.646 | 3.481 | 73.151 | 217.343 | 1350.655 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15944.441 | 17724 |
| applied_total | 578595.265 | 961702 |
| bench_cpu_cores | 4.779 | 5.37 |
| bench_cpu_pct | 19.914 | 22.37 |
| bench_gc_gen0 | 2.837 | 5 |
| bench_gc_gen1 | 1.204 | 3 |
| bench_gc_gen2 | 0.694 | 2 |
| bench_gc_heap_mb | 862.155 | 1104.4 |
| bench_mem_mb | 1470.786 | 1586.9 |
| bench_threadpool_queue | 2.633 | 23 |
| bench_threadpool_threads | 33.959 | 44 |
| broker_backlog | 1892.735 | 3487 |
| ctr_eb_mosquitto_cpu_pct | 53.218 | 57.11 |
| ctr_eb_mosquitto_mem_mb | 9.073 | 36.1 |
| ctr_eb_mssql_cpu_pct | 1.996 | 2.61 |
| ctr_eb_mssql_mem_mb | 7353.067 | 7355.7 |
| ctr_eb_mysql_cpu_pct | 0.612 | 0.71 |
| ctr_eb_mysql_mem_mb | 9808.439 | 9841.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.5 | 14.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 14.5 | 14.5 |
| ctr_eb_postgres_cpu_pct | 212.133 | 384.5 |
| ctr_eb_postgres_mem_mb | 2903.071 | 2960.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 36.786 | 48.37 |
| in_flight | 593.735 | 2110 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.363 | 1.41 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146053.347 | 1903758 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 235814.041 | 390510 |
| octopus_mqtt_sent_total | 364661.714 | 612923 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.961 | 16196.1 |
| produced_total | 579189 | 962064 |
| publish_mb_per_s | 51.855 | 73.8 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,536 events applied by workers, 1,000,536 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 4,812 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 73 ms, max 1350 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2394 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 70
- [cross_source_blocking] source 9088 seq 55: ready 101 ms before start, 101 ms of it with an idle slot in group 314
- [cross_source_blocking] source 7379 seq 16: ready 101 ms before start, 101 ms of it with an idle slot in group 253
- [cross_source_blocking] source 13304 seq 21: ready 106 ms before start, 106 ms of it with an idle slot in group 425
- [cross_source_blocking] source 9375 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 325
- [cross_source_blocking] source 14754 seq 21: ready 102 ms before start, 102 ms of it with an idle slot in group 464
- [cross_source_blocking] source 7230 seq 52: ready 105 ms before start, 105 ms of it with an idle slot in group 246
- [cross_source_blocking] source 3227 seq 15: ready 101 ms before start, 101 ms of it with an idle slot in group 108
- [cross_source_blocking] source 8132 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 275
- [cross_source_blocking] source 2288 seq 47: ready 100 ms before start, 100 ms of it with an idle slot in group 65
- [cross_source_blocking] source 7148 seq 15: ready 100 ms before start, 100 ms of it with an idle slot in group 242
- [cross_source_blocking] source 6751 seq 27: ready 102 ms before start, 102 ms of it with an idle slot in group 230
- [cross_source_blocking] source 10575 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 348
- [cross_source_blocking] source 3820 seq 35: ready 106 ms before start, 106 ms of it with an idle slot in group 129
- [cross_source_blocking] source 8476 seq 20: ready 100 ms before start, 100 ms of it with an idle slot in group 289
- [cross_source_blocking] source 15695 seq 21: ready 103 ms before start, 103 ms of it with an idle slot in group 490
- [cross_source_blocking] source 3586 seq 28: ready 105 ms before start, 105 ms of it with an idle slot in group 121
- [cross_source_blocking] source 12205 seq 13: ready 100 ms before start, 100 ms of it with an idle slot in group 389
- [cross_source_blocking] source 11128 seq 21: ready 101 ms before start, 101 ms of it with an idle slot in group 363
- [cross_source_blocking] source 13279 seq 17: ready 101 ms before start, 101 ms of it with an idle slot in group 425

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2473.983 | 5308.415 | 6586.367 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1859.583 | 4620.287 | 4784.127 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1810.431 | 3571.711 | 4177.919 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1499.135 | 3031.039 | 4128.767 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.423 | 2588.671 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.487 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.487 | 2342.911 | 2932.735 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.359 | 2310.143 | 3014.655 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.359 | 2244.607 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.263 | 2244.607 | 3096.575 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.263 | 2244.607 | 2899.967 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.423 | 2228.223 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.231 | 2211.839 | 3457.023 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.327 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.231 | 2162.687 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.103 | 2129.919 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.103 | 2129.919 | 2850.815 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 7.071 | 2113.535 | 2752.511 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.327 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.263 | 2097.151 | 2736.127 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 7.551 | 2097.151 | 2899.967 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 6.879 | 2080.767 | 2588.671 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.359 | 2056.191 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.295 | 2056.191 | 2965.503 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 7.007 | 2023.423 | 2719.743 |
