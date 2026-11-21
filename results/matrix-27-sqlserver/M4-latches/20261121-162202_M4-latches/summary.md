# M4-latches — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.41s, closing 2.527s, drain 8.45s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 999769 | 999769 | 15917.682 | 15958.994 | 26266.1 | 0 | 0 | 0 | 458 | 5158 | 19877 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796387 | 75.91 | 26.143 | 117.247 | 1356.799 | 2797.567 | 7094.271 |
| e2e_from_send | 796387 | 67.727 | 23.519 | 88.511 | 1347.583 | 2787.327 | 6733.823 |
| producer_lag | 796387 | 8.183 | 1.528 | 10.151 | 166.655 | 505.855 | 1069.055 |
| publish_call | 999769 | 41.331 | 21.023 | 81.919 | 358.143 | 995.839 | 1210.367 |
| queue_wait | 796387 | 56.958 | 23.375 | 84.991 | 744.447 | 2334.719 | 5533.695 |
| broker_dwell | 796387 | 31.2 | 5.771 | 34.111 | 683.519 | 2310.143 | 5308.415 |
| receive_call | 864611 | 41.514 | 18.703 | 103.935 | 335.871 | 475.391 | 545.279 |
| ctx_load | 796387 | 0.003 | 0.002 | 0.004 | 0.013 | 0.088 | 37.663 |
| process | 796387 | 10.736 | 0.001 | 0.003 | 0.019 | 2621.439 | 3018.751 |
| ctx_commit | 796387 | 0.003 | 0.001 | 0.002 | 0.005 | 0.043 | 34.495 |
| ack | 785461 | 0.005 | 0.002 | 0.005 | 0.02 | 0.125 | 39.807 |
| service | 796387 | 10.769 | 0.013 | 0.038 | 0.327 | 2621.439 | 3018.751 |
| e2e_large | 356 | 2683.657 | 2650.111 | 3586.047 | 4968.447 | 7094.271 | 7094.271 |
| ready_wait | 796387 | 12.28 | 2.897 | 26.895 | 178.175 | 315.647 | 2318.335 |
| blocked | 796387 | 12.198 | 2.897 | 26.879 | 177.535 | 302.079 | 2318.335 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15958.994 | 18368.6 |
| applied_total | 578062.694 | 959451 |
| bench_cpu_cores | 4.124 | 4.72 |
| bench_cpu_pct | 17.185 | 19.67 |
| bench_gc_gen0 | 2.633 | 4 |
| bench_gc_gen1 | 1.102 | 3 |
| bench_gc_gen2 | 0.571 | 1 |
| bench_gc_heap_mb | 1092.706 | 1382.6 |
| bench_mem_mb | 1849.565 | 2004.7 |
| bench_threadpool_queue | 34.122 | 228 |
| bench_threadpool_threads | 42.878 | 44 |
| broker_backlog | 14909.408 | 81618 |
| ctr_eb_mosquitto_cpu_pct | 59.464 | 65.39 |
| ctr_eb_mosquitto_mem_mb | 8.994 | 16.2 |
| ctr_eb_mssql_cpu_pct | 866.456 | 930.35 |
| ctr_eb_mssql_mem_mb | 7588.741 | 7598.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.023 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.023 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.644 | 1.78 |
| ctr_eb_postgres_mem_mb | 2512.947 | 2515.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 68.551 | 77.35 |
| in_flight | 772.49 | 2246 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.487 | 2.83 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1129658.163 | 1935425 |
| octopus_missing_reports_total | 1669.224 | 2107 |
| octopus_mqtt_received_total | 159984.245 | 286544 |
| octopus_mqtt_sent_total | 297871.735 | 527105 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15917.682 | 17654.7 |
| produced_total | 578835.184 | 959783 |
| publish_mb_per_s | 51.747 | 78.91 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,796 records are Completed |
| record_counts | pass | 138,796 records found, 138,796 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 999,769 events applied by workers, 999,769 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 19,877 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 178 ms, max 2317 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3891 seq 7: ready 273 ms before start, 273 ms of it with an idle slot in group 131
- [cross_source_blocking] source 13354 seq 19: ready 248 ms before start, 248 ms of it with an idle slot in group 427
- [cross_source_blocking] source 9659 seq 14: ready 365 ms before start, 365 ms of it with an idle slot in group 334
- [cross_source_blocking] source 2075 seq 2: ready 365 ms before start, 365 ms of it with an idle slot in group 61
- [cross_source_blocking] source 2967 seq 23: ready 365 ms before start, 365 ms of it with an idle slot in group 97
- [cross_source_blocking] source 11407 seq 15: ready 365 ms before start, 365 ms of it with an idle slot in group 372
- [cross_source_blocking] source 11451 seq 8: ready 365 ms before start, 365 ms of it with an idle slot in group 372
- [cross_source_blocking] source 10043 seq 47: ready 365 ms before start, 365 ms of it with an idle slot in group 342
- [cross_source_blocking] source 7931 seq 13: ready 365 ms before start, 365 ms of it with an idle slot in group 267
- [cross_source_blocking] source 7919 seq 23: ready 365 ms before start, 365 ms of it with an idle slot in group 267
- [cross_source_blocking] source 2895 seq 9: ready 365 ms before start, 365 ms of it with an idle slot in group 92
- [cross_source_blocking] source 8751 seq 21: ready 365 ms before start, 365 ms of it with an idle slot in group 304
- [cross_source_blocking] source 12231 seq 10: ready 223 ms before start, 223 ms of it with an idle slot in group 389
- [cross_source_blocking] source 15587 seq 13: ready 220 ms before start, 220 ms of it with an idle slot in group 483
- [cross_source_blocking] source 351 seq 10: ready 224 ms before start, 224 ms of it with an idle slot in group 10
- [cross_source_blocking] source 2317 seq 12: ready 469 ms before start, 469 ms of it with an idle slot in group 66
- [cross_source_blocking] source 2445 seq 8: ready 468 ms before start, 468 ms of it with an idle slot in group 72
- [cross_source_blocking] source 5315 seq 18: ready 220 ms before start, 220 ms of it with an idle slot in group 182
- [cross_source_blocking] source 8337 seq 5: ready 463 ms before start, 463 ms of it with an idle slot in group 283
- [cross_source_blocking] source 5621 seq 24: ready 464 ms before start, 464 ms of it with an idle slot in group 194

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2113.535 | 5046.271 | 5111.807 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2342.911 | 4358.143 | 7110.655 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2293.759 | 4325.375 | 4980.735 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2260.991 | 4227.071 | 5177.343 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 872 | 872 | 0 | 27.775 | 2572.287 | 4194.303 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3803 | 3803 | 0 | 28.031 | 2408.447 | 4456.447 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 27.391 | 2375.679 | 2932.735 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2626 | 2626 | 0 | 27.263 | 2342.911 | 2899.967 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 26.367 | 2277.375 | 2736.127 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 27.391 | 2277.375 | 3194.879 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5128 | 5128 | 0 | 27.007 | 2260.991 | 3145.727 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 27.775 | 2260.991 | 2981.887 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5536 | 5536 | 0 | 28.031 | 2244.607 | 3538.943 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 795 | 795 | 0 | 26.239 | 2244.607 | 3080.191 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 27.007 | 2244.607 | 2883.583 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2259 | 2259 | 0 | 28.671 | 2244.607 | 3014.655 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 25.471 | 2195.455 | 2850.815 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 876 | 876 | 0 | 25.471 | 2162.687 | 2654.207 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1131 | 1131 | 0 | 25.215 | 2162.687 | 3145.727 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 27.775 | 2162.687 | 2916.351 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 25.343 | 2146.303 | 2768.895 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 25.343 | 2146.303 | 2932.735 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 720 | 720 | 0 | 25.727 | 2113.535 | 2768.895 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3200 | 3200 | 0 | 27.007 | 2097.151 | 4325.375 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 25.343 | 2088.959 | 2965.503 |
