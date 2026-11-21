# P3-pg-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.103s, closing 0.619s, drain 2.756s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000526 | 1000526 | 15944.367 | 15945.798 | 38342.2 | 0 | 0 | 0 | 458 | 5162 | 730 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797159 | 36.203 | 7.355 | 16.399 | 1290.239 | 2727.935 | 4915.199 |
| e2e_from_send | 797159 | 35.023 | 6.475 | 14.639 | 1289.215 | 2725.887 | 4915.199 |
| producer_lag | 797159 | 1.18 | 0.8 | 2.731 | 6.267 | 18.671 | 131.071 |
| publish_call | 1000526 | 7.554 | 6.015 | 13.295 | 42.143 | 90.623 | 141.183 |
| queue_wait | 797159 | 24.281 | 6.447 | 14.159 | 634.879 | 2273.279 | 4669.439 |
| broker_dwell | 797159 | 18.055 | 0.461 | 4.683 | 630.271 | 2267.135 | 4665.343 |
| receive_call | 950610 | 36.244 | 14.527 | 92.607 | 320.767 | 471.039 | 509.183 |
| ctx_load | 797159 | 0.002 | 0.002 | 0.004 | 0.013 | 0.048 | 14.079 |
| process | 797159 | 10.723 | 0.001 | 0.003 | 0.017 | 2617.343 | 3002.367 |
| ctx_commit | 797159 | 0.002 | 0.001 | 0.001 | 0.004 | 0.027 | 25.055 |
| ack | 794724 | 0.003 | 0.002 | 0.005 | 0.018 | 0.064 | 21.887 |
| service | 797159 | 10.741 | 0.014 | 0.029 | 0.101 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2273.581 | 2254.847 | 3031.039 | 4345.855 | 4763.647 | 4763.647 |
| ready_wait | 797159 | 2.1 | 0.685 | 2.327 | 38.143 | 102.655 | 1862.655 |
| blocked | 797159 | 2.029 | 0.685 | 2.327 | 37.951 | 86.783 | 233.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15945.798 | 16265.9 |
| applied_total | 578855.531 | 961832 |
| bench_cpu_cores | 4.976 | 5.434 |
| bench_cpu_pct | 20.733 | 22.64 |
| bench_gc_gen0 | 2.796 | 6 |
| bench_gc_gen1 | 1.143 | 3 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 936.261 | 1162.6 |
| bench_mem_mb | 1512.939 | 1599.6 |
| bench_threadpool_queue | 5.122 | 66 |
| bench_threadpool_threads | 32.02 | 37 |
| broker_backlog | 2092.735 | 4519 |
| ctr_eb_mosquitto_cpu_pct | 55.968 | 63.08 |
| ctr_eb_mosquitto_mem_mb | 7.655 | 10.1 |
| ctr_eb_mssql_cpu_pct | 12.141 | 104.35 |
| ctr_eb_mssql_mem_mb | 7578.935 | 7581.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 224.219 | 385.91 |
| ctr_eb_postgres_mem_mb | 2974.3 | 3100.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 39.976 | 50.44 |
| in_flight | 455.163 | 602 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.472 | 1.75 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1144760.673 | 1908925 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 225819.714 | 379830 |
| octopus_mqtt_sent_total | 354006.531 | 600268 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15944.367 | 16243.9 |
| produced_total | 579310.694 | 962211 |
| publish_mb_per_s | 51.901 | 74.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,526 events applied by workers, 1,000,526 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 730 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 38 ms, max 233 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1909 seq 30: ready 114 ms before start, 114 ms of it with an idle slot in group 55
- [cross_source_blocking] source 4341 seq 43: ready 114 ms before start, 114 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4401 seq 5: ready 115 ms before start, 115 ms of it with an idle slot in group 151
- [cross_source_blocking] source 1141 seq 13: ready 113 ms before start, 113 ms of it with an idle slot in group 38
- [cross_source_blocking] source 9173 seq 15: ready 114 ms before start, 114 ms of it with an idle slot in group 315
- [cross_source_blocking] source 3605 seq 18: ready 115 ms before start, 115 ms of it with an idle slot in group 122
- [cross_source_blocking] source 11633 seq 14: ready 115 ms before start, 115 ms of it with an idle slot in group 378
- [cross_source_blocking] source 3441 seq 2: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 2: ready 102 ms before start, 102 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 108 ms before start, 108 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3484 seq 2: ready 148 ms before start, 148 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9268 seq 2: ready 105 ms before start, 105 ms of it with an idle slot in group 319
- [cross_source_blocking] source 9269 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3467 seq 5: ready 114 ms before start, 114 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 4: ready 135 ms before start, 135 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3486 seq 5: ready 128 ms before start, 128 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3427 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 119
- [cross_source_blocking] source 13900 seq 165: ready 112 ms before start, 112 ms of it with an idle slot in group 441
- [cross_source_blocking] source 7060 seq 114: ready 112 ms before start, 112 ms of it with an idle slot in group 239
- [cross_source_blocking] source 9156 seq 55: ready 113 ms before start, 113 ms of it with an idle slot in group 315

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1818.623 | 4489.215 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1884.159 | 4030.463 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1794.047 | 3538.943 | 4112.383 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1499.135 | 3047.423 | 4177.919 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.519 | 2555.903 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.583 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.807 | 2342.911 | 2932.735 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.807 | 2260.991 | 2899.967 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.295 | 2244.607 | 2736.127 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.615 | 2244.607 | 3096.575 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.583 | 2228.223 | 2932.735 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.487 | 2228.223 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.519 | 2211.839 | 3473.407 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.327 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.199 | 2162.687 | 2867.199 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 6.911 | 2129.919 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.487 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.391 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.455 | 2113.535 | 2736.127 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 7.199 | 2088.959 | 2752.511 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.327 | 2056.191 | 2949.119 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 7.423 | 2056.191 | 2555.903 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.487 | 2047.999 | 2965.503 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 7.455 | 2039.807 | 2883.583 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 7.359 | 2031.615 | 2719.743 |
