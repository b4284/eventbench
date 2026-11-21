# Na-pg — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.103s, closing 0.562s, drain 2.753s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000509 | 1000509 | 15942.359 | 15944.804 | 38397.4 | 0 | 0 | 0 | 458 | 5163 | 1366 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797141 | 35.34 | 6.839 | 11.615 | 1287.167 | 2727.935 | 4919.295 |
| e2e_from_send | 797141 | 34.121 | 6.131 | 9.719 | 1286.143 | 2725.887 | 4919.295 |
| producer_lag | 797141 | 1.219 | 0.793 | 2.709 | 6.727 | 29.967 | 221.823 |
| publish_call | 1000509 | 7.101 | 5.811 | 9.527 | 56.511 | 111.551 | 236.031 |
| queue_wait | 797141 | 23.38 | 6.103 | 9.495 | 635.391 | 2269.183 | 4677.631 |
| broker_dwell | 797141 | 17.603 | 0.879 | 2.044 | 630.271 | 2263.039 | 4673.535 |
| receive_call | 963537 | 35.722 | 14.215 | 90.751 | 318.719 | 469.503 | 500.479 |
| ctx_load | 797141 | 0.002 | 0.002 | 0.003 | 0.008 | 0.033 | 31.247 |
| process | 797141 | 10.725 | 0.001 | 0.002 | 0.013 | 2617.343 | 3000.319 |
| ctx_commit | 797141 | 0.002 | 0.001 | 0.001 | 0.003 | 0.019 | 26.239 |
| ack | 795073 | 0.003 | 0.002 | 0.004 | 0.014 | 0.045 | 14.063 |
| service | 797141 | 10.741 | 0.013 | 0.026 | 0.075 | 2617.343 | 3000.319 |
| e2e_large | 356 | 2266.961 | 2256.895 | 3037.183 | 4341.759 | 4771.839 | 4771.839 |
| ready_wait | 797141 | 1.642 | 0.515 | 2.169 | 14.743 | 121.343 | 1869.823 |
| blocked | 797141 | 1.569 | 0.515 | 2.169 | 14.655 | 118.911 | 268.287 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15944.804 | 16163.5 |
| applied_total | 578778.469 | 961749 |
| bench_cpu_cores | 4.723 | 5.159 |
| bench_cpu_pct | 19.679 | 21.5 |
| bench_gc_gen0 | 2.429 | 4 |
| bench_gc_gen1 | 0.98 | 2 |
| bench_gc_gen2 | 0.551 | 1 |
| bench_gc_heap_mb | 1015.522 | 1301.3 |
| bench_mem_mb | 1682.378 | 1746.9 |
| bench_threadpool_queue | 3.224 | 27 |
| bench_threadpool_threads | 28.816 | 31 |
| broker_backlog | 1912.163 | 4043 |
| ctr_eb_mssql_cpu_pct | 2.027 | 2.58 |
| ctr_eb_mssql_mem_mb | 7506.847 | 7508.3 |
| ctr_eb_mysql_cpu_pct | 0.651 | 0.72 |
| ctr_eb_mysql_mem_mb | 10719.1 | 10767 |
| ctr_eb_nats_octopus_cpu_pct | 83.631 | 85.52 |
| ctr_eb_nats_octopus_mem_mb | 45.124 | 58.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.4 | 7.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 8.5 | 8.5 |
| ctr_eb_postgres_cpu_pct | 196.53 | 332.67 |
| ctr_eb_postgres_mem_mb | 2775.124 | 2899.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 35.372 | 52.49 |
| in_flight | 445.98 | 573 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.193 | 1.27 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1148434.837 | 1963903 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 216178.306 | 369332 |
| octopus_mqtt_sent_total | 390478.102 | 671977 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.359 | 16147.6 |
| produced_total | 579224.449 | 962109 |
| publish_mb_per_s | 51.792 | 74.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,509 events applied by workers, 1,000,509 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,366 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 15 ms, max 268 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15638 seq 23: ready 127 ms before start, 127 ms of it with an idle slot in group 487
- [cross_source_blocking] source 12351 seq 4: ready 127 ms before start, 127 ms of it with an idle slot in group 392
- [cross_source_blocking] source 4216 seq 14: ready 128 ms before start, 128 ms of it with an idle slot in group 140
- [cross_source_blocking] source 3543 seq 23: ready 134 ms before start, 134 ms of it with an idle slot in group 121
- [cross_source_blocking] source 15898 seq 8: ready 127 ms before start, 127 ms of it with an idle slot in group 497
- [cross_source_blocking] source 8055 seq 29: ready 133 ms before start, 133 ms of it with an idle slot in group 271
- [cross_source_blocking] source 868 seq 12: ready 124 ms before start, 124 ms of it with an idle slot in group 25
- [cross_source_blocking] source 11199 seq 13: ready 133 ms before start, 133 ms of it with an idle slot in group 364
- [cross_source_blocking] source 3304 seq 11: ready 128 ms before start, 128 ms of it with an idle slot in group 111
- [cross_source_blocking] source 13021 seq 12: ready 124 ms before start, 124 ms of it with an idle slot in group 414
- [cross_source_blocking] source 8666 seq 3: ready 131 ms before start, 131 ms of it with an idle slot in group 300
- [cross_source_blocking] source 8870 seq 9: ready 131 ms before start, 131 ms of it with an idle slot in group 306
- [cross_source_blocking] source 11194 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 364
- [cross_source_blocking] source 3003 seq 10: ready 124 ms before start, 124 ms of it with an idle slot in group 100
- [cross_source_blocking] source 5281 seq 22: ready 124 ms before start, 124 ms of it with an idle slot in group 182
- [cross_source_blocking] source 12347 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 392
- [cross_source_blocking] source 3990 seq 30: ready 127 ms before start, 127 ms of it with an idle slot in group 132
- [cross_source_blocking] source 11168 seq 3: ready 130 ms before start, 130 ms of it with an idle slot in group 364
- [cross_source_blocking] source 7227 seq 47: ready 124 ms before start, 124 ms of it with an idle slot in group 246
- [cross_source_blocking] source 2184 seq 6: ready 119 ms before start, 119 ms of it with an idle slot in group 62

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1851.391 | 4456.447 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1884.159 | 4128.767 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1777.663 | 3522.559 | 4079.615 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1499.135 | 3063.807 | 4145.151 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 6.879 | 2555.903 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.007 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.007 | 2342.911 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 6.655 | 2244.607 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 6.911 | 2244.607 | 3080.191 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 6.815 | 2244.607 | 2899.967 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 6.687 | 2228.223 | 2965.503 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 6.975 | 2228.223 | 2883.583 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.103 | 2211.839 | 3489.791 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 6.975 | 2211.839 | 2916.351 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 6.943 | 2162.687 | 2867.199 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 6.879 | 2129.919 | 3129.343 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 6.911 | 2129.919 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 6.687 | 2129.919 | 2850.815 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 6.911 | 2097.151 | 2736.127 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 6.975 | 2072.575 | 2736.127 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 6.975 | 2064.383 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.135 | 2047.999 | 2965.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 6.815 | 2047.999 | 2539.519 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 6.943 | 2023.423 | 2703.359 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 6.847 | 1998.847 | 2883.583 |
