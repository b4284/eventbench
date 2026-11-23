# Ja-json — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.161s, closing 1.779s, drain 1.43s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000376 | 1000376 | 15942.729 | 15943.784 | 24190 | 0 | 0 | 0 | 458 | 5161 | 3431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797014 | 56.908 | 18.383 | 60.671 | 1315.839 | 2783.231 | 7446.527 |
| e2e_from_send | 797014 | 52.842 | 16.879 | 52.735 | 1311.743 | 2779.135 | 7442.431 |
| producer_lag | 797014 | 4.066 | 1.009 | 3.899 | 96.831 | 239.743 | 680.447 |
| publish_call | 1000376 | 31.912 | 17.247 | 64.607 | 244.095 | 531.967 | 1241.087 |
| queue_wait | 797014 | 42.094 | 16.799 | 50.911 | 684.543 | 2301.951 | 5423.103 |
| broker_dwell | 797014 | 19.901 | 1.33 | 7.627 | 655.871 | 2279.423 | 5308.415 |
| receive_call | 903737 | 39.583 | 17.327 | 100.799 | 322.047 | 469.503 | 509.695 |
| ctx_load | 797014 | 0.002 | 0.002 | 0.003 | 0.01 | 0.044 | 24.911 |
| process | 797014 | 10.726 | 0.001 | 0.002 | 0.015 | 2617.343 | 3016.703 |
| ctx_commit | 797014 | 0.002 | 0.001 | 0.001 | 0.004 | 0.026 | 24.127 |
| ack | 796128 | 0.005 | 0.003 | 0.008 | 0.023 | 0.075 | 23.663 |
| service | 797014 | 10.748 | 0.015 | 0.036 | 0.12 | 2617.343 | 3016.703 |
| e2e_large | 356 | 2810.415 | 2758.655 | 3852.287 | 5709.823 | 7446.527 | 7446.527 |
| ready_wait | 797014 | 5.555 | 0.731 | 15.575 | 68.479 | 226.047 | 1907.711 |
| blocked | 797014 | 5.472 | 0.731 | 15.567 | 67.903 | 208.895 | 1368.063 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15943.784 | 18695.9 |
| applied_total | 578341.837 | 961523 |
| bench_cpu_cores | 5.055 | 5.581 |
| bench_cpu_pct | 21.062 | 23.25 |
| bench_gc_gen0 | 2.898 | 5 |
| bench_gc_gen1 | 1.286 | 3 |
| bench_gc_gen2 | 0.714 | 2 |
| bench_gc_heap_mb | 1019.747 | 1351.6 |
| bench_mem_mb | 1732.89 | 1885.3 |
| bench_threadpool_queue | 13.959 | 98 |
| bench_threadpool_threads | 38.204 | 40 |
| broker_backlog | 2499.51 | 4369 |
| ctr_eb_mssql_cpu_pct | 567.166 | 683.98 |
| ctr_eb_mssql_mem_mb | 6313.112 | 6340 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.174 | 0.34 |
| ctr_eb_postgres_mem_mb | 2498.886 | 2499.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 53.862 | 61.24 |
| in_flight | 484.776 | 1109 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.294 | 2.62 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146928.245 | 1950981 |
| octopus_missing_reports_total | 5521.184 | 9193 |
| octopus_mqtt_received_total | 172978.612 | 303725 |
| octopus_mqtt_sent_total | 330376.245 | 571775 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.729 | 18686.9 |
| produced_total | 578826.612 | 961901 |
| publish_mb_per_s | 51.763 | 75.61 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,854 records are Completed |
| record_counts | pass | 138,854 records found, 138,854 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,376 events applied by workers, 1,000,376 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 3,431 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 68 ms, max 1367 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14683 seq 15: ready 102 ms before start, 102 ms of it with an idle slot in group 464
- [cross_source_blocking] source 5307 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 182
- [cross_source_blocking] source 14203 seq 27: ready 104 ms before start, 104 ms of it with an idle slot in group 454
- [cross_source_blocking] source 8604 seq 5: ready 103 ms before start, 103 ms of it with an idle slot in group 296
- [cross_source_blocking] source 5311 seq 18: ready 104 ms before start, 104 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6815 seq 18: ready 106 ms before start, 106 ms of it with an idle slot in group 232
- [cross_source_blocking] source 5252 seq 23: ready 106 ms before start, 106 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5284 seq 13: ready 100 ms before start, 100 ms of it with an idle slot in group 182
- [cross_source_blocking] source 9451 seq 9: ready 106 ms before start, 106 ms of it with an idle slot in group 325
- [cross_source_blocking] source 10527 seq 17: ready 104 ms before start, 104 ms of it with an idle slot in group 348
- [cross_source_blocking] source 15479 seq 6: ready 104 ms before start, 104 ms of it with an idle slot in group 483
- [cross_source_blocking] source 8071 seq 27: ready 103 ms before start, 103 ms of it with an idle slot in group 271
- [cross_source_blocking] source 6683 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 228
- [cross_source_blocking] source 8871 seq 16: ready 105 ms before start, 105 ms of it with an idle slot in group 306
- [cross_source_blocking] source 8568 seq 25: ready 100 ms before start, 100 ms of it with an idle slot in group 294
- [cross_source_blocking] source 2476 seq 5: ready 104 ms before start, 104 ms of it with an idle slot in group 74
- [cross_source_blocking] source 8779 seq 16: ready 104 ms before start, 104 ms of it with an idle slot in group 306
- [cross_source_blocking] source 5404 seq 12: ready 104 ms before start, 104 ms of it with an idle slot in group 184
- [cross_source_blocking] source 8075 seq 26: ready 100 ms before start, 100 ms of it with an idle slot in group 271
- [cross_source_blocking] source 2232 seq 40: ready 101 ms before start, 101 ms of it with an idle slot in group 65

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2654.207 | 5832.703 | 7471.103 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1974.271 | 4653.055 | 4915.199 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2031.615 | 4079.615 | 4587.519 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1916.927 | 3620.863 | 4227.071 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 18.431 | 2572.287 | 4194.303 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 19.327 | 2359.295 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 19.071 | 2359.295 | 2932.735 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 19.199 | 2293.759 | 2719.743 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 941 | 941 | 0 | 17.663 | 2277.375 | 2916.351 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 18.943 | 2244.607 | 2916.351 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 19.199 | 2244.607 | 3096.575 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 18.943 | 2244.607 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5540 | 5540 | 0 | 19.327 | 2228.223 | 3555.327 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 18.303 | 2228.223 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 17.791 | 2179.071 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 18.687 | 2162.687 | 2949.119 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 18.175 | 2146.303 | 2850.815 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 18.175 | 2129.919 | 2768.895 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 19.199 | 2129.919 | 3145.727 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 18.175 | 2113.535 | 2752.511 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 795 | 795 | 0 | 18.431 | 2113.535 | 2867.199 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 18.815 | 2097.151 | 2752.511 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 17.791 | 2080.767 | 2555.903 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 825 | 825 | 0 | 17.663 | 2072.575 | 2965.503 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 18.047 | 2072.575 | 2965.503 |
