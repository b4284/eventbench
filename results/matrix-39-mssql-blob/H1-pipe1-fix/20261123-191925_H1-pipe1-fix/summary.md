# H1-pipe1-fix — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.152s, closing 1.322s, drain 2.003s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000502 | 1000502 | 15941.906 | 15943.227 | 23804.5 | 0 | 0 | 0 | 458 | 5163 | 4313 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797141 | 59.247 | 14.391 | 73.471 | 1312.767 | 2750.463 | 4923.391 |
| e2e_from_send | 797141 | 50.757 | 13.047 | 61.311 | 1304.575 | 2744.319 | 4923.391 |
| producer_lag | 797141 | 8.491 | 0.997 | 4.095 | 222.847 | 335.103 | 672.767 |
| publish_call | 1000502 | 26.318 | 12.815 | 59.039 | 195.071 | 496.383 | 1104.895 |
| queue_wait | 797141 | 40.007 | 12.991 | 58.975 | 657.407 | 2285.567 | 4681.727 |
| broker_dwell | 797141 | 19.566 | 1.141 | 7.115 | 637.439 | 2269.183 | 4661.247 |
| receive_call | 913575 | 38.607 | 16.143 | 99.647 | 320.255 | 474.879 | 527.871 |
| ctx_load | 797141 | 0.002 | 0.002 | 0.003 | 0.011 | 0.04 | 27.631 |
| process | 797141 | 10.728 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797141 | 0.002 | 0.001 | 0.001 | 0.004 | 0.024 | 27.679 |
| ack | 795757 | 0.005 | 0.003 | 0.008 | 0.021 | 0.069 | 27.039 |
| service | 797141 | 10.749 | 0.014 | 0.035 | 0.11 | 2617.343 | 3006.463 |
| e2e_large | 356 | 2314.223 | 2312.191 | 3069.951 | 4349.951 | 4792.319 | 4792.319 |
| ready_wait | 797141 | 4.575 | 0.599 | 11.031 | 74.175 | 147.839 | 1874.943 |
| blocked | 797141 | 4.502 | 0.599 | 11.023 | 73.791 | 144.895 | 432.127 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15943.227 | 20078.1 |
| applied_total | 578323.122 | 961530 |
| bench_cpu_cores | 5.178 | 5.91 |
| bench_cpu_pct | 21.577 | 24.62 |
| bench_gc_gen0 | 2.959 | 5 |
| bench_gc_gen1 | 1.265 | 3 |
| bench_gc_gen2 | 0.755 | 2 |
| bench_gc_heap_mb | 966.302 | 1234.1 |
| bench_mem_mb | 1649.294 | 1765.7 |
| bench_threadpool_queue | 8.857 | 178 |
| bench_threadpool_threads | 35.51 | 37 |
| broker_backlog | 2754.755 | 6163 |
| ctr_eb_mssql_cpu_pct | 472.918 | 560.71 |
| ctr_eb_mssql_mem_mb | 6833.635 | 7058.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.155 | 0.33 |
| ctr_eb_postgres_mem_mb | 2481.133 | 2481.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.239 | 54.97 |
| in_flight | 486.49 | 1549 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.737 | 2.03 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1147655.041 | 1924873 |
| octopus_missing_reports_total | 2646 | 5075 |
| octopus_mqtt_received_total | 185279.673 | 315951 |
| octopus_mqtt_sent_total | 337880.837 | 571749 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15941.906 | 20038.1 |
| produced_total | 578809.612 | 961893 |
| publish_mb_per_s | 51.85 | 74.77 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,868 records are Completed |
| record_counts | pass | 138,868 records found, 138,868 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,502 events applied by workers, 1,000,502 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 4,313 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 74 ms, max 432 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10825 seq 7: ready 111 ms before start, 111 ms of it with an idle slot in group 354
- [cross_source_blocking] source 5721 seq 18: ready 111 ms before start, 111 ms of it with an idle slot in group 197
- [cross_source_blocking] source 3233 seq 15: ready 117 ms before start, 117 ms of it with an idle slot in group 108
- [cross_source_blocking] source 6497 seq 18: ready 113 ms before start, 113 ms of it with an idle slot in group 222
- [cross_source_blocking] source 4861 seq 7: ready 117 ms before start, 117 ms of it with an idle slot in group 168
- [cross_source_blocking] source 13917 seq 29: ready 113 ms before start, 113 ms of it with an idle slot in group 441
- [cross_source_blocking] source 5501 seq 14: ready 116 ms before start, 116 ms of it with an idle slot in group 189
- [cross_source_blocking] source 6337 seq 8: ready 117 ms before start, 117 ms of it with an idle slot in group 216
- [cross_source_blocking] source 309 seq 3: ready 113 ms before start, 113 ms of it with an idle slot in group 9
- [cross_source_blocking] source 11325 seq 3: ready 112 ms before start, 112 ms of it with an idle slot in group 368
- [cross_source_blocking] source 14235 seq 39: ready 107 ms before start, 107 ms of it with an idle slot in group 456
- [cross_source_blocking] source 8704 seq 20: ready 114 ms before start, 114 ms of it with an idle slot in group 302
- [cross_source_blocking] source 7404 seq 12: ready 111 ms before start, 111 ms of it with an idle slot in group 256
- [cross_source_blocking] source 348 seq 12: ready 112 ms before start, 112 ms of it with an idle slot in group 10
- [cross_source_blocking] source 3592 seq 22: ready 117 ms before start, 117 ms of it with an idle slot in group 121
- [cross_source_blocking] source 4612 seq 25: ready 103 ms before start, 103 ms of it with an idle slot in group 161
- [cross_source_blocking] source 13000 seq 27: ready 117 ms before start, 117 ms of it with an idle slot in group 413
- [cross_source_blocking] source 14776 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 464
- [cross_source_blocking] source 9092 seq 66: ready 104 ms before start, 104 ms of it with an idle slot in group 314
- [cross_source_blocking] source 3232 seq 15: ready 102 ms before start, 102 ms of it with an idle slot in group 108

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1892.351 | 4521.983 | 4816.895 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1933.311 | 4161.535 | 4620.287 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1785.855 | 3522.559 | 4095.999 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1540.095 | 3145.727 | 4194.303 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 14.655 | 2555.903 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 15.167 | 2359.295 | 4456.447 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 14.655 | 2359.295 | 2932.735 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 15.103 | 2326.527 | 2932.735 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 14.015 | 2260.991 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 14.975 | 2244.607 | 3096.575 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 14.719 | 2244.607 | 3227.647 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 13.695 | 2244.607 | 2965.503 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 14.847 | 2244.607 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 15.295 | 2211.839 | 3473.407 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 14.719 | 2179.071 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 15.167 | 2146.303 | 2932.735 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 14.655 | 2129.919 | 2981.887 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 14.655 | 2129.919 | 3129.343 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 14.655 | 2129.919 | 2899.967 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 14.335 | 2113.535 | 2736.127 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 13.823 | 2097.151 | 2768.895 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 14.335 | 2088.959 | 3735.551 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 14.271 | 2072.575 | 2555.903 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 14.719 | 2056.191 | 2981.887 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 14.271 | 2031.615 | 2785.279 |
