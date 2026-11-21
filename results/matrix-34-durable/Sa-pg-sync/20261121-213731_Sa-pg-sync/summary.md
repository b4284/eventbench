# Sa-pg-sync — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.105s, closing 0.603s, drain 2.762s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000534 | 1000534 | 15941.051 | 15943.573 | 38549 | 0 | 0 | 0 | 458 | 5163 | 793 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797171 | 38.042 | 8.511 | 19.471 | 1292.287 | 2738.175 | 4919.295 |
| e2e_from_send | 797171 | 36.789 | 7.531 | 17.599 | 1292.287 | 2736.127 | 4919.295 |
| producer_lag | 797171 | 1.254 | 0.8 | 2.745 | 7.687 | 31.327 | 169.599 |
| publish_call | 1000534 | 8.881 | 6.943 | 14.607 | 55.711 | 129.151 | 176.895 |
| queue_wait | 797171 | 26.044 | 7.499 | 17.055 | 641.023 | 2277.375 | 4677.631 |
| broker_dwell | 797171 | 18.635 | 0.898 | 5.999 | 632.831 | 2267.135 | 4669.439 |
| receive_call | 951037 | 36.219 | 14.455 | 92.799 | 321.023 | 471.039 | 504.575 |
| ctx_load | 797171 | 0.002 | 0.002 | 0.004 | 0.011 | 0.041 | 31.391 |
| process | 797171 | 10.728 | 0.001 | 0.003 | 0.016 | 2619.391 | 3002.367 |
| ctx_commit | 797171 | 0.002 | 0.001 | 0.001 | 0.004 | 0.025 | 20.495 |
| ack | 794192 | 0.003 | 0.002 | 0.004 | 0.016 | 0.053 | 13.575 |
| service | 797171 | 10.745 | 0.014 | 0.028 | 0.089 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2286.56 | 2275.327 | 3055.615 | 4358.143 | 4775.935 | 4775.935 |
| ready_wait | 797171 | 2.216 | 0.698 | 2.745 | 36.831 | 114.943 | 1886.207 |
| blocked | 797171 | 2.144 | 0.698 | 2.743 | 36.607 | 98.815 | 327.935 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15943.573 | 17049.7 |
| applied_total | 578789.163 | 961602 |
| bench_cpu_cores | 4.915 | 5.86 |
| bench_cpu_pct | 20.478 | 24.42 |
| bench_gc_gen0 | 2.837 | 4 |
| bench_gc_gen1 | 1.265 | 2 |
| bench_gc_gen2 | 0.714 | 2 |
| bench_gc_heap_mb | 872.173 | 1073 |
| bench_mem_mb | 1447.567 | 1575.3 |
| bench_threadpool_queue | 5.388 | 65 |
| bench_threadpool_threads | 30.184 | 31 |
| broker_backlog | 1953.102 | 3556 |
| ctr_eb_mosquitto_cpu_pct | 54.438 | 69.11 |
| ctr_eb_mosquitto_mem_mb | 9.118 | 21.1 |
| ctr_eb_mssql_cpu_pct | 2.754 | 10.98 |
| ctr_eb_mssql_mem_mb | 7327.859 | 7330.9 |
| ctr_eb_mysql_cpu_pct | 0.577 | 0.68 |
| ctr_eb_mysql_mem_mb | 9229.051 | 9247.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.39 | 7.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.316 | 7.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 8.027 | 8.2 |
| ctr_eb_postgres_cpu_pct | 239.316 | 473.98 |
| ctr_eb_postgres_mem_mb | 3314.035 | 3487.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 42.288 | 51.98 |
| in_flight | 496.571 | 1703 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.419 | 1.72 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146017.592 | 1903661 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 231359.857 | 383984 |
| octopus_mqtt_sent_total | 354621.633 | 597744 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15941.051 | 16227.6 |
| produced_total | 579285.735 | 961952 |
| publish_mb_per_s | 51.858 | 73.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,534 events applied by workers, 1,000,534 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 793 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 37 ms, max 328 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10385 seq 23: ready 137 ms before start, 137 ms of it with an idle slot in group 347
- [cross_source_blocking] source 12761 seq 15: ready 133 ms before start, 133 ms of it with an idle slot in group 403
- [cross_source_blocking] source 6353 seq 19: ready 137 ms before start, 137 ms of it with an idle slot in group 216
- [cross_source_blocking] source 1265 seq 9: ready 133 ms before start, 133 ms of it with an idle slot in group 40
- [cross_source_blocking] source 14309 seq 9: ready 133 ms before start, 133 ms of it with an idle slot in group 458
- [cross_source_blocking] source 5613 seq 27: ready 136 ms before start, 136 ms of it with an idle slot in group 194
- [cross_source_blocking] source 11605 seq 10: ready 132 ms before start, 132 ms of it with an idle slot in group 377
- [cross_source_blocking] source 3458 seq 1: ready 110 ms before start, 110 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 119 ms before start, 119 ms of it with an idle slot in group 119
- [cross_source_blocking] source 13730 seq 22: ready 149 ms before start, 149 ms of it with an idle slot in group 437
- [cross_source_blocking] source 6300 seq 10: ready 149 ms before start, 149 ms of it with an idle slot in group 214
- [cross_source_blocking] source 10736 seq 44: ready 146 ms before start, 146 ms of it with an idle slot in group 352
- [cross_source_blocking] source 1624 seq 5: ready 146 ms before start, 146 ms of it with an idle slot in group 48
- [cross_source_blocking] source 14793 seq 22: ready 151 ms before start, 151 ms of it with an idle slot in group 464
- [cross_source_blocking] source 2217 seq 45: ready 151 ms before start, 151 ms of it with an idle slot in group 65
- [cross_source_blocking] source 3924 seq 27: ready 168 ms before start, 168 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3435 seq 2: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3593 seq 32: ready 156 ms before start, 156 ms of it with an idle slot in group 121
- [cross_source_blocking] source 11884 seq 42: ready 128 ms before start, 128 ms of it with an idle slot in group 383
- [cross_source_blocking] source 7984 seq 27: ready 146 ms before start, 146 ms of it with an idle slot in group 269

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1867.775 | 4620.287 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1900.543 | 4145.151 | 4784.127 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1794.047 | 3538.943 | 4095.999 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1523.711 | 3047.423 | 4145.151 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 8.703 | 2588.671 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 8.703 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 8.831 | 2342.911 | 2932.735 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 8.511 | 2260.991 | 2719.743 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 8.575 | 2260.991 | 2899.967 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 8.703 | 2244.607 | 3096.575 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 8.639 | 2228.223 | 2916.351 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 8.511 | 2228.223 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 8.767 | 2211.839 | 3457.023 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 8.639 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 8.703 | 2162.687 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 8.383 | 2129.919 | 2932.735 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 8.319 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 8.703 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 8.511 | 2113.535 | 2736.127 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 8.319 | 2080.767 | 2752.511 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 8.383 | 2056.191 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 8.255 | 2056.191 | 2965.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 8.447 | 2056.191 | 2555.903 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 8.319 | 2023.423 | 2719.743 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 8.703 | 2007.039 | 2899.967 |
