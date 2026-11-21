# La-pg — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.106s, closing 0.854s, drain 2.57s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000542 | 1000542 | 15960.51 | 15961.724 | 38486.8 | 0 | 0 | 0 | 458 | 5163 | 3638 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797179 | 37.987 | 7.291 | 20.703 | 1295.359 | 2750.463 | 6451.199 |
| e2e_from_send | 797179 | 36.634 | 6.423 | 17.935 | 1293.311 | 2748.415 | 6447.103 |
| producer_lag | 797179 | 1.353 | 0.812 | 2.789 | 10.431 | 44.831 | 159.103 |
| publish_call | 1000542 | 8.022 | 5.951 | 14.759 | 47.903 | 95.423 | 163.967 |
| queue_wait | 797179 | 25.892 | 6.395 | 17.119 | 655.871 | 2287.615 | 4669.439 |
| broker_dwell | 797179 | 19.841 | 0.862 | 6.715 | 649.215 | 2281.471 | 4665.343 |
| receive_call | 949618 | 36.504 | 14.255 | 93.247 | 326.143 | 472.575 | 509.183 |
| ctx_load | 797179 | 0.002 | 0.002 | 0.003 | 0.011 | 0.037 | 11.631 |
| process | 797179 | 10.726 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797179 | 0.002 | 0.001 | 0.001 | 0.004 | 0.022 | 17.679 |
| ack | 793566 | 0.003 | 0.002 | 0.004 | 0.015 | 0.049 | 16.655 |
| service | 797179 | 10.742 | 0.013 | 0.027 | 0.084 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2603.406 | 2553.855 | 3553.279 | 5255.167 | 6451.199 | 6451.199 |
| ready_wait | 797179 | 3.117 | 0.664 | 2.973 | 49.535 | 214.783 | 1864.703 |
| blocked | 797179 | 3.039 | 0.664 | 2.973 | 49.279 | 211.967 | 1356.799 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15961.724 | 16881.2 |
| applied_total | 578698.306 | 961700 |
| bench_cpu_cores | 5.011 | 5.872 |
| bench_cpu_pct | 20.881 | 24.47 |
| bench_gc_gen0 | 2.878 | 5 |
| bench_gc_gen1 | 1.245 | 2 |
| bench_gc_gen2 | 0.755 | 2 |
| bench_gc_heap_mb | 879.276 | 1139.3 |
| bench_mem_mb | 1520.578 | 1737.9 |
| bench_threadpool_queue | 2.837 | 38 |
| bench_threadpool_threads | 33.837 | 44 |
| broker_backlog | 2068.918 | 3839 |
| ctr_eb_mosquitto_cpu_pct | 53.548 | 57.48 |
| ctr_eb_mosquitto_mem_mb | 8.363 | 29.9 |
| ctr_eb_mssql_cpu_pct | 2.114 | 2.37 |
| ctr_eb_mssql_mem_mb | 7353.969 | 7354.7 |
| ctr_eb_mysql_cpu_pct | 0.677 | 0.85 |
| ctr_eb_mysql_mem_mb | 10312.155 | 10318.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.5 | 14.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.4 | 14.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 14.5 | 14.5 |
| ctr_eb_postgres_cpu_pct | 220.621 | 416.16 |
| ctr_eb_postgres_mem_mb | 3045.951 | 3149 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 38.676 | 51.7 |
| in_flight | 490.061 | 1202 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.402 | 1.52 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1144328.184 | 1909774 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 229058.49 | 383132 |
| octopus_mqtt_sent_total | 352928.776 | 599291 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15960.51 | 16920.6 |
| produced_total | 579188.367 | 962068 |
| publish_mb_per_s | 51.89 | 75 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,874 records are Completed |
| record_counts | pass | 138,874 records found, 138,874 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,542 events applied by workers, 1,000,542 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 3,638 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 49 ms, max 1356 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5297 seq 22: ready 104 ms before start, 104 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5401 seq 16: ready 112 ms before start, 112 ms of it with an idle slot in group 184
- [cross_source_blocking] source 7136 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 242
- [cross_source_blocking] source 8523 seq 11: ready 102 ms before start, 102 ms of it with an idle slot in group 293
- [cross_source_blocking] source 3254 seq 14: ready 102 ms before start, 102 ms of it with an idle slot in group 108
- [cross_source_blocking] source 3803 seq 31: ready 102 ms before start, 102 ms of it with an idle slot in group 129
- [cross_source_blocking] source 2580 seq 18: ready 106 ms before start, 106 ms of it with an idle slot in group 79
- [cross_source_blocking] source 8906 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 307
- [cross_source_blocking] source 872 seq 35: ready 108 ms before start, 108 ms of it with an idle slot in group 26
- [cross_source_blocking] source 5270 seq 18: ready 109 ms before start, 109 ms of it with an idle slot in group 182
- [cross_source_blocking] source 12900 seq 5: ready 108 ms before start, 108 ms of it with an idle slot in group 408
- [cross_source_blocking] source 10939 seq 17: ready 102 ms before start, 102 ms of it with an idle slot in group 357
- [cross_source_blocking] source 4586 seq 13: ready 109 ms before start, 109 ms of it with an idle slot in group 160
- [cross_source_blocking] source 1521 seq 19: ready 104 ms before start, 104 ms of it with an idle slot in group 45
- [cross_source_blocking] source 15785 seq 11: ready 104 ms before start, 104 ms of it with an idle slot in group 494
- [cross_source_blocking] source 2441 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 72
- [cross_source_blocking] source 13267 seq 19: ready 111 ms before start, 111 ms of it with an idle slot in group 425
- [cross_source_blocking] source 5969 seq 16: ready 112 ms before start, 112 ms of it with an idle slot in group 206
- [cross_source_blocking] source 5977 seq 13: ready 112 ms before start, 112 ms of it with an idle slot in group 206
- [cross_source_blocking] source 10928 seq 17: ready 110 ms before start, 110 ms of it with an idle slot in group 357
- [cross_source_blocking] source 2314 seq 9: ready 111 ms before start, 111 ms of it with an idle slot in group 66
- [cross_source_blocking] source 9433 seq 8: ready 102 ms before start, 102 ms of it with an idle slot in group 325

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2473.983 | 5341.183 | 6455.295 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1859.583 | 4489.215 | 4784.127 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1769.471 | 3522.559 | 4128.767 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1507.327 | 3047.423 | 4128.767 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.327 | 2588.671 | 4177.919 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.615 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.551 | 2342.911 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.231 | 2244.607 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.423 | 2244.607 | 3096.575 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.103 | 2244.607 | 2899.967 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.295 | 2244.607 | 2965.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.551 | 2228.223 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.551 | 2211.839 | 3440.639 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.423 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.327 | 2162.687 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.295 | 2146.303 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.071 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.679 | 2113.535 | 3129.343 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 7.103 | 2097.151 | 2588.671 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 7.167 | 2097.151 | 2752.511 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.135 | 2097.151 | 2736.127 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.167 | 2064.383 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.039 | 2056.191 | 2965.503 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 7.231 | 2023.423 | 2719.743 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 7.647 | 2007.039 | 2899.967 |
