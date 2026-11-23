# Ra-pgsync-mem — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.187s, closing 1.487s, drain 1.702s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000422 | 1000422 | 15947 | 15948.422 | 21396 | 0 | 0 | 0 | 458 | 5163 | 1718 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797054 | 56.409 | 14.447 | 86.079 | 1312.767 | 2750.463 | 5042.175 |
| e2e_from_send | 797054 | 53.114 | 13.079 | 76.415 | 1306.623 | 2748.415 | 4993.023 |
| producer_lag | 797054 | 3.295 | 0.908 | 3.203 | 67.839 | 151.935 | 497.407 |
| publish_call | 1000422 | 25.089 | 12.847 | 60.767 | 140.543 | 209.407 | 322.559 |
| queue_wait | 797054 | 42.365 | 12.983 | 73.855 | 654.335 | 2289.663 | 4673.535 |
| broker_dwell | 797054 | 18.796 | 1.081 | 6.095 | 631.295 | 2267.135 | 4665.343 |
| receive_call | 904859 | 40.005 | 16.671 | 103.615 | 323.327 | 469.247 | 531.967 |
| ctx_load | 797054 | 0.002 | 0.002 | 0.003 | 0.01 | 0.039 | 12.831 |
| process | 797054 | 10.729 | 0.001 | 0.002 | 0.014 | 2619.391 | 3002.367 |
| ctx_commit | 797054 | 0.002 | 0.001 | 0.001 | 0.004 | 0.021 | 19.903 |
| ack | 795821 | 0.005 | 0.003 | 0.007 | 0.02 | 0.071 | 30.975 |
| service | 797054 | 10.749 | 0.013 | 0.032 | 0.107 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2304.406 | 2277.375 | 3051.519 | 4247.551 | 4784.127 | 4784.127 |
| ready_wait | 797054 | 3.299 | 0.591 | 6.559 | 54.175 | 128.639 | 1880.063 |
| blocked | 797054 | 3.227 | 0.591 | 6.559 | 53.855 | 122.495 | 293.119 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15948.422 | 17968.6 |
| applied_total | 578362.918 | 961670 |
| bench_cpu_cores | 5.098 | 6.213 |
| bench_cpu_pct | 21.243 | 25.89 |
| bench_gc_gen0 | 2.959 | 5 |
| bench_gc_gen1 | 1.327 | 3 |
| bench_gc_gen2 | 0.776 | 2 |
| bench_gc_heap_mb | 929.576 | 1217.1 |
| bench_mem_mb | 1559.533 | 1708.6 |
| bench_threadpool_queue | 26 | 607 |
| bench_threadpool_threads | 31.286 | 32 |
| broker_backlog | 2025.306 | 5073 |
| ctr_eb_pg_standby_cpu_pct | 30.555 | 46.03 |
| ctr_eb_pg_standby_mem_mb | 1406.553 | 1545 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 176.847 | 247.97 |
| ctr_eb_postgres_mem_mb | 3651.265 | 3795.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 31.065 | 40.95 |
| in_flight | 490.347 | 1252 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.258 | 1.47 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1147741.735 | 1923999 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 181454.878 | 297116 |
| octopus_mqtt_sent_total | 325989.878 | 540760 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15947 | 17984.6 |
| produced_total | 578853.265 | 962033 |
| publish_mb_per_s | 51.857 | 74.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,863 records are Completed |
| record_counts | pass | 138,863 records found, 138,863 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,422 events applied by workers, 1,000,422 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,718 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 54 ms, max 293 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5685 seq 29: ready 135 ms before start, 135 ms of it with an idle slot in group 196
- [cross_source_blocking] source 15877 seq 41: ready 105 ms before start, 105 ms of it with an idle slot in group 496
- [cross_source_blocking] source 8243 seq 14: ready 110 ms before start, 110 ms of it with an idle slot in group 279
- [cross_source_blocking] source 9051 seq 11: ready 110 ms before start, 110 ms of it with an idle slot in group 313
- [cross_source_blocking] source 7123 seq 16: ready 110 ms before start, 110 ms of it with an idle slot in group 242
- [cross_source_blocking] source 13347 seq 41: ready 110 ms before start, 110 ms of it with an idle slot in group 427
- [cross_source_blocking] source 11631 seq 21: ready 110 ms before start, 110 ms of it with an idle slot in group 378
- [cross_source_blocking] source 1573 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 47
- [cross_source_blocking] source 10681 seq 14: ready 111 ms before start, 111 ms of it with an idle slot in group 350
- [cross_source_blocking] source 13793 seq 20: ready 112 ms before start, 112 ms of it with an idle slot in group 438
- [cross_source_blocking] source 7697 seq 15: ready 112 ms before start, 112 ms of it with an idle slot in group 263
- [cross_source_blocking] source 9261 seq 1: ready 120 ms before start, 120 ms of it with an idle slot in group 319
- [cross_source_blocking] source 1306 seq 64: ready 117 ms before start, 117 ms of it with an idle slot in group 41
- [cross_source_blocking] source 8402 seq 17: ready 117 ms before start, 117 ms of it with an idle slot in group 285
- [cross_source_blocking] source 11334 seq 45: ready 117 ms before start, 117 ms of it with an idle slot in group 369
- [cross_source_blocking] source 11986 seq 25: ready 121 ms before start, 121 ms of it with an idle slot in group 383
- [cross_source_blocking] source 5018 seq 15: ready 117 ms before start, 117 ms of it with an idle slot in group 173
- [cross_source_blocking] source 10228 seq 31: ready 103 ms before start, 103 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10234 seq 36: ready 104 ms before start, 104 ms of it with an idle slot in group 344
- [cross_source_blocking] source 400 seq 16: ready 100 ms before start, 100 ms of it with an idle slot in group 12
- [cross_source_blocking] source 12916 seq 21: ready 104 ms before start, 104 ms of it with an idle slot in group 409
- [cross_source_blocking] source 8006 seq 28: ready 100 ms before start, 100 ms of it with an idle slot in group 270

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1884.159 | 4489.215 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1933.311 | 4145.151 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1818.623 | 3522.559 | 4079.615 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1515.519 | 3080.191 | 4145.151 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 14.463 | 2588.671 | 4227.071 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 15.359 | 2375.679 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 16.191 | 2359.295 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 15.423 | 2260.991 | 2752.511 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 14.271 | 2260.991 | 2899.967 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 15.295 | 2244.607 | 3080.191 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 15.871 | 2244.607 | 2998.271 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5539 | 5539 | 0 | 15.039 | 2228.223 | 3440.639 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 15.423 | 2228.223 | 2949.119 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 14.783 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 14.079 | 2195.455 | 2883.583 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 14.207 | 2129.919 | 2965.503 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 15.359 | 2129.919 | 3145.727 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 15.807 | 2129.919 | 2932.735 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 13.759 | 2129.919 | 2850.815 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 720 | 720 | 0 | 14.527 | 2113.535 | 2736.127 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 14.847 | 2072.575 | 2752.511 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 14.079 | 2056.191 | 2965.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 14.335 | 2056.191 | 2555.903 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1016 | 1016 | 0 | 15.167 | 2039.807 | 2719.743 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 15.679 | 2015.231 | 2899.967 |
