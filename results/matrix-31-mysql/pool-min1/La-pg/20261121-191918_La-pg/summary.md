# La-pg — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 0.703s, drain 2.601s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000545 | 1000545 | 15941.657 | 15938.061 | 38691.9 | 0 | 0 | 0 | 458 | 5163 | 1898 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797181 | 36.498 | 7.307 | 15.951 | 1290.239 | 2729.983 | 4915.199 |
| e2e_from_send | 797181 | 35.335 | 6.407 | 14.303 | 1289.215 | 2727.935 | 4911.103 |
| producer_lag | 797181 | 1.163 | 0.791 | 2.713 | 5.987 | 16.975 | 167.935 |
| publish_call | 1000545 | 7.44 | 5.943 | 12.335 | 40.639 | 108.863 | 182.783 |
| queue_wait | 797181 | 24.594 | 6.379 | 13.879 | 637.439 | 2275.327 | 4673.535 |
| broker_dwell | 797181 | 18.793 | 0.836 | 5.039 | 632.319 | 2267.135 | 4669.439 |
| receive_call | 955050 | 36.156 | 14.543 | 92.671 | 320.255 | 468.735 | 503.807 |
| ctx_load | 797181 | 0.002 | 0.002 | 0.003 | 0.011 | 0.037 | 12.199 |
| process | 797181 | 10.725 | 0.001 | 0.002 | 0.014 | 2617.343 | 3006.463 |
| ctx_commit | 797181 | 0.002 | 0.001 | 0.001 | 0.004 | 0.022 | 21.775 |
| ack | 794339 | 0.003 | 0.002 | 0.004 | 0.014 | 0.046 | 12.655 |
| service | 797181 | 10.741 | 0.013 | 0.026 | 0.081 | 2617.343 | 3006.463 |
| e2e_large | 356 | 2280.629 | 2269.183 | 3041.279 | 4218.879 | 4771.839 | 4771.839 |
| ready_wait | 797181 | 2.453 | 0.638 | 2.205 | 53.919 | 158.591 | 1880.063 |
| blocked | 797181 | 2.38 | 0.638 | 2.205 | 53.471 | 156.799 | 262.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15938.061 | 16166 |
| applied_total | 578899.51 | 961523 |
| bench_cpu_cores | 4.828 | 5.216 |
| bench_cpu_pct | 20.115 | 21.73 |
| bench_gc_gen0 | 2.837 | 4 |
| bench_gc_gen1 | 1.204 | 2 |
| bench_gc_gen2 | 0.694 | 1 |
| bench_gc_heap_mb | 898.18 | 1147.5 |
| bench_mem_mb | 1469.204 | 1576.2 |
| bench_threadpool_queue | 4.98 | 89 |
| bench_threadpool_threads | 30.408 | 34 |
| broker_backlog | 2091.653 | 4291 |
| ctr_eb_mosquitto_cpu_pct | 53.378 | 57.23 |
| ctr_eb_mosquitto_mem_mb | 7.576 | 16 |
| ctr_eb_mssql_cpu_pct | 2.023 | 2.59 |
| ctr_eb_mssql_mem_mb | 7352.267 | 7352.8 |
| ctr_eb_mysql_cpu_pct | 0.631 | 0.77 |
| ctr_eb_mysql_mem_mb | 9841.737 | 9851.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.5 | 14.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 14.5 | 14.5 |
| ctr_eb_postgres_cpu_pct | 236.393 | 477.77 |
| ctr_eb_postgres_mem_mb | 3047.416 | 3207.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 37.724 | 49.63 |
| in_flight | 452.327 | 624 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.38 | 1.46 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146541.571 | 1902139 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 234196.245 | 387417 |
| octopus_mqtt_sent_total | 365318.857 | 612222 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15941.657 | 16151.3 |
| produced_total | 579351.837 | 962147 |
| publish_mb_per_s | 51.79 | 74.38 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,874 records are Completed |
| record_counts | pass | 138,874 records found, 138,874 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,545 events applied by workers, 1,000,545 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,898 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 53 ms, max 262 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3461 seq 1: ready 135 ms before start, 107 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 2: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 108 ms before start, 108 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3484 seq 2: ready 118 ms before start, 118 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9265 seq 4: ready 120 ms before start, 120 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3483 seq 3: ready 102 ms before start, 102 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3458 seq 4: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 2220 seq 167: ready 230 ms before start, 230 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8520 seq 23: ready 230 ms before start, 230 ms of it with an idle slot in group 293
- [cross_source_blocking] source 3596 seq 61: ready 230 ms before start, 230 ms of it with an idle slot in group 121
- [cross_source_blocking] source 6767 seq 81: ready 222 ms before start, 222 ms of it with an idle slot in group 230
- [cross_source_blocking] source 4639 seq 45: ready 222 ms before start, 222 ms of it with an idle slot in group 161
- [cross_source_blocking] source 11559 seq 25: ready 222 ms before start, 222 ms of it with an idle slot in group 376
- [cross_source_blocking] source 2243 seq 117: ready 222 ms before start, 222 ms of it with an idle slot in group 65
- [cross_source_blocking] source 1135 seq 57: ready 222 ms before start, 222 ms of it with an idle slot in group 37
- [cross_source_blocking] source 13715 seq 29: ready 222 ms before start, 222 ms of it with an idle slot in group 436
- [cross_source_blocking] source 4317 seq 130: ready 233 ms before start, 233 ms of it with an idle slot in group 147
- [cross_source_blocking] source 13969 seq 33: ready 233 ms before start, 233 ms of it with an idle slot in group 443
- [cross_source_blocking] source 9145 seq 48: ready 233 ms before start, 233 ms of it with an idle slot in group 315

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1851.391 | 4489.215 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1916.927 | 4128.767 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1769.471 | 3522.559 | 4079.615 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1507.327 | 3080.191 | 4194.303 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.359 | 2588.671 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.519 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.615 | 2342.911 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.199 | 2260.991 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.551 | 2244.607 | 3096.575 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.487 | 2244.607 | 2899.967 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.551 | 2228.223 | 2932.735 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.135 | 2228.223 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.423 | 2211.839 | 3457.023 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.455 | 2211.839 | 2867.199 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 6.783 | 2162.687 | 2867.199 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.263 | 2129.919 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.519 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.391 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.263 | 2097.151 | 2752.511 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 6.975 | 2072.575 | 2752.511 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.391 | 2056.191 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.135 | 2056.191 | 2965.503 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 795 | 795 | 0 | 7.199 | 2056.191 | 2899.967 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 7.039 | 2047.999 | 2555.903 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 7.327 | 2023.423 | 2719.743 |
