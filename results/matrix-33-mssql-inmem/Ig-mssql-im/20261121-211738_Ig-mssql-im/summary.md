# Ig-mssql-im — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.23s, closing 1.477s, drain 1.503s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000516 | 1000516 | 15941.378 | 15944.12 | 29584.1 | 0 | 0 | 0 | 458 | 5162 | 4595 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797150 | 49.853 | 14.351 | 42.143 | 1311.743 | 2762.751 | 6295.551 |
| e2e_from_send | 797150 | 47.222 | 12.847 | 36.607 | 1308.671 | 2758.655 | 6098.943 |
| producer_lag | 797150 | 2.631 | 1.036 | 3.961 | 24.543 | 225.791 | 730.111 |
| publish_call | 1000516 | 29.414 | 12.695 | 61.215 | 298.239 | 797.183 | 1428.479 |
| queue_wait | 797150 | 36.47 | 12.791 | 35.423 | 668.671 | 2295.807 | 4894.719 |
| broker_dwell | 797150 | 21.921 | 1.826 | 14.023 | 646.655 | 2279.423 | 4718.591 |
| receive_call | 908120 | 38.712 | 16.023 | 98.495 | 329.215 | 472.575 | 593.407 |
| ctx_load | 797150 | 0.003 | 0.002 | 0.004 | 0.012 | 0.052 | 40.351 |
| process | 797150 | 10.728 | 0.001 | 0.003 | 0.017 | 2619.391 | 3002.367 |
| ctx_commit | 797150 | 0.002 | 0.001 | 0.001 | 0.004 | 0.028 | 28.527 |
| ack | 792367 | 0.004 | 0.003 | 0.005 | 0.018 | 0.072 | 43.391 |
| service | 797150 | 10.752 | 0.014 | 0.033 | 0.125 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2572.911 | 2574.335 | 3399.679 | 4517.887 | 6295.551 | 6295.551 |
| ready_wait | 797150 | 4.721 | 1.049 | 8.639 | 72.063 | 212.479 | 1869.823 |
| blocked | 797150 | 4.633 | 1.049 | 8.639 | 71.679 | 207.231 | 1154.047 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15944.12 | 18342.2 |
| applied_total | 578545.041 | 961450 |
| bench_cpu_cores | 4.776 | 5.183 |
| bench_cpu_pct | 19.9 | 21.6 |
| bench_gc_gen0 | 2.735 | 4 |
| bench_gc_gen1 | 1.163 | 2 |
| bench_gc_gen2 | 0.633 | 1 |
| bench_gc_heap_mb | 985.741 | 1295.3 |
| bench_mem_mb | 1714.314 | 1871.8 |
| bench_threadpool_queue | 10.857 | 128 |
| bench_threadpool_threads | 40.776 | 44 |
| broker_backlog | 2281.51 | 3751 |
| ctr_eb_mosquitto_cpu_pct | 60.594 | 66.33 |
| ctr_eb_mosquitto_mem_mb | 7.02 | 12.7 |
| ctr_eb_mssql_cpu_pct | 692.852 | 887.22 |
| ctr_eb_mssql_mem_mb | 7459.227 | 7708.5 |
| ctr_eb_mysql_cpu_pct | 0.739 | 0.79 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.639 | 1.69 |
| ctr_eb_postgres_mem_mb | 3120.92 | 3124.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 62.583 | 70.82 |
| in_flight | 535.286 | 1684 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.547 | 2.85 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146723.204 | 1920983 |
| octopus_missing_reports_total | 814.837 | 1169 |
| octopus_mqtt_received_total | 195780.714 | 340035 |
| octopus_mqtt_sent_total | 325751.143 | 570918 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15941.378 | 17126.2 |
| produced_total | 579080.327 | 961856 |
| publish_mb_per_s | 51.763 | 80.26 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,516 events applied by workers, 1,000,516 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 4,595 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 72 ms, max 1153 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5679 seq 26: ready 111 ms before start, 111 ms of it with an idle slot in group 196
- [cross_source_blocking] source 7055 seq 32: ready 109 ms before start, 109 ms of it with an idle slot in group 239
- [cross_source_blocking] source 14147 seq 16: ready 112 ms before start, 112 ms of it with an idle slot in group 450
- [cross_source_blocking] source 135 seq 16: ready 112 ms before start, 112 ms of it with an idle slot in group 2
- [cross_source_blocking] source 6935 seq 18: ready 112 ms before start, 112 ms of it with an idle slot in group 235
- [cross_source_blocking] source 6043 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 207
- [cross_source_blocking] source 4135 seq 12: ready 112 ms before start, 112 ms of it with an idle slot in group 136
- [cross_source_blocking] source 1991 seq 8: ready 112 ms before start, 112 ms of it with an idle slot in group 58
- [cross_source_blocking] source 14403 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 459
- [cross_source_blocking] source 1955 seq 18: ready 109 ms before start, 109 ms of it with an idle slot in group 55
- [cross_source_blocking] source 2335 seq 20: ready 113 ms before start, 113 ms of it with an idle slot in group 67
- [cross_source_blocking] source 1122 seq 16: ready 110 ms before start, 110 ms of it with an idle slot in group 37
- [cross_source_blocking] source 8407 seq 11: ready 414 ms before start, 414 ms of it with an idle slot in group 285
- [cross_source_blocking] source 10393 seq 9: ready 224 ms before start, 224 ms of it with an idle slot in group 347
- [cross_source_blocking] source 5393 seq 22: ready 230 ms before start, 230 ms of it with an idle slot in group 184
- [cross_source_blocking] source 4582 seq 14: ready 209 ms before start, 209 ms of it with an idle slot in group 160
- [cross_source_blocking] source 4734 seq 16: ready 122 ms before start, 122 ms of it with an idle slot in group 163
- [cross_source_blocking] source 4578 seq 14: ready 131 ms before start, 131 ms of it with an idle slot in group 160
- [cross_source_blocking] source 12998 seq 17: ready 123 ms before start, 123 ms of it with an idle slot in group 413
- [cross_source_blocking] source 8710 seq 15: ready 131 ms before start, 131 ms of it with an idle slot in group 303

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2031.615 | 4718.591 | 5013.503 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2211.839 | 4521.983 | 5341.183 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2162.687 | 4063.231 | 6324.223 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1990.655 | 3735.551 | 4325.375 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 14.719 | 2637.823 | 4177.919 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 14.719 | 2359.295 | 2932.735 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 15.167 | 2342.911 | 4423.679 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 14.847 | 2277.375 | 2850.815 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 13.823 | 2260.991 | 2736.127 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 15.359 | 2260.991 | 2932.735 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 14.975 | 2260.991 | 2899.967 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 14.911 | 2244.607 | 3096.575 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 14.847 | 2244.607 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 14.783 | 2228.223 | 3489.791 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 14.911 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 14.143 | 2179.071 | 2883.583 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 13.951 | 2146.303 | 2768.895 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 14.527 | 2129.919 | 3145.727 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 14.207 | 2129.919 | 2932.735 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 13.887 | 2113.535 | 2768.895 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 13.759 | 2113.535 | 2752.511 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 14.911 | 2113.535 | 2932.735 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 14.335 | 2072.575 | 2949.119 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 13.823 | 2072.575 | 2555.903 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 14.079 | 2056.191 | 2965.503 |
