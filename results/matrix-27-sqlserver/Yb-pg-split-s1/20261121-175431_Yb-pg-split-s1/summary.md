# Yb-pg-split-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.54s, closing 0.469s, drain 3.873s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 503643 | 503643 | 8023.267 | 8026.388 | 21876.2 | 0 | 0 | 0 | 373 | 2688 | 319 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 401127 | 37.513 | 4.663 | 32.735 | 1313.791 | 2729.983 | 6070.271 |
| e2e_from_send | 401127 | 37.218 | 4.531 | 31.967 | 1313.791 | 2729.983 | 6066.175 |
| producer_lag | 401127 | 0.296 | 0.002 | 0.482 | 3.815 | 42.271 | 165.759 |
| publish_call | 503643 | 8.65 | 4.223 | 17.247 | 75.071 | 144.639 | 316.415 |
| queue_wait | 401127 | 26.21 | 4.515 | 30.543 | 647.679 | 2273.279 | 3661.823 |
| broker_dwell | 401127 | 27.732 | 8.055 | 20.223 | 649.215 | 2277.375 | 3596.287 |
| receive_call | 487987 | 36.485 | 13.239 | 95.743 | 322.559 | 470.783 | 503.551 |
| ctx_load | 401127 | 0.001 | 0.001 | 0.001 | 0.004 | 0.015 | 5.563 |
| process | 401127 | 11.003 | 0.001 | 0.002 | 0.008 | 2613.247 | 3000.319 |
| ctx_commit | 401127 | 0.001 | 0.001 | 0.001 | 0.002 | 0.01 | 1.853 |
| ack | 294681 | 0.002 | 0.001 | 0.002 | 0.007 | 0.02 | 0.257 |
| service | 401127 | 11.008 | 0.005 | 0.012 | 0.032 | 2613.247 | 3000.319 |
| e2e_large | 282 | 2331.003 | 2306.047 | 3094.527 | 4333.567 | 6070.271 | 6070.271 |
| ready_wait | 401127 | 2.833 | 0.5 | 4.567 | 43.135 | 94.271 | 1315.839 |
| blocked | 401127 | 2.8 | 0.5 | 4.563 | 43.071 | 92.095 | 258.815 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8026.388 | 8391 |
| applied_total | 288843.735 | 481175 |
| bench_cpu_cores | 4.549 | 4.699 |
| bench_cpu_pct | 45.486 | 46.99 |
| bench_gc_gen0 | 0.776 | 2 |
| bench_gc_gen1 | 0.449 | 2 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 924.42 | 1724.5 |
| bench_mem_mb | 1840.524 | 2018.3 |
| bench_threadpool_queue | 0.531 | 12 |
| bench_threadpool_threads | 16.082 | 19 |
| broker_backlog | 759.061 | 1102 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 235.041 | 417 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 579810.469 | 983435 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 269491.592 | 450078 |
| octopus_mqtt_sent_total | 292542.571 | 498760 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 8023.267 | 8227.7 |
| produced_total | 289078.776 | 481415 |
| publish_mb_per_s | 33.479 | 52.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 69,626 records are Completed |
| record_counts | pass | 69,626 records found, 69,626 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 373 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 503,643 events applied by workers, 503,643 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 319 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 43 ms, max 259 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3459 seq 1: ready 144 ms before start, 144 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3455 seq 1: ready 167 ms before start, 167 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3477 seq 3: ready 401 ms before start, 121 ms of it with an idle slot in group 119
- [cross_source_blocking] source 8133 seq 19: ready 116 ms before start, 116 ms of it with an idle slot in group 275
- [cross_source_blocking] source 13120 seq 15: ready 110 ms before start, 110 ms of it with an idle slot in group 417
- [cross_source_blocking] source 13180 seq 6: ready 110 ms before start, 110 ms of it with an idle slot in group 419
- [cross_source_blocking] source 14219 seq 6: ready 110 ms before start, 110 ms of it with an idle slot in group 455
- [cross_source_blocking] source 5040 seq 13: ready 110 ms before start, 110 ms of it with an idle slot in group 175
- [cross_source_blocking] source 6307 seq 17: ready 110 ms before start, 110 ms of it with an idle slot in group 215
- [cross_source_blocking] source 4518 seq 9: ready 110 ms before start, 110 ms of it with an idle slot in group 157
- [cross_source_blocking] source 7979 seq 18: ready 111 ms before start, 111 ms of it with an idle slot in group 269
- [cross_source_blocking] source 6924 seq 20: ready 111 ms before start, 111 ms of it with an idle slot in group 235
- [cross_source_blocking] source 8629 seq 9: ready 110 ms before start, 110 ms of it with an idle slot in group 299
- [cross_source_blocking] source 6639 seq 31: ready 110 ms before start, 110 ms of it with an idle slot in group 223
- [cross_source_blocking] source 908 seq 5: ready 108 ms before start, 108 ms of it with an idle slot in group 27
- [cross_source_blocking] source 13261 seq 32: ready 107 ms before start, 107 ms of it with an idle slot in group 425
- [cross_source_blocking] source 7058 seq 28: ready 107 ms before start, 107 ms of it with an idle slot in group 239
- [cross_source_blocking] source 406 seq 16: ready 107 ms before start, 107 ms of it with an idle slot in group 13
- [cross_source_blocking] source 5504 seq 9: ready 106 ms before start, 106 ms of it with an idle slot in group 189
- [cross_source_blocking] source 1133 seq 25: ready 107 ms before start, 107 ms of it with an idle slot in group 37

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 1908.735 | 4358.143 | 5799.935 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 1859.583 | 4325.375 | 6094.847 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2244.607 | 3112.959 | 3899.391 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 707 | 707 | 0 | 4.735 | 2654.207 | 4325.375 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 759 | 759 | 0 | 4.959 | 2310.143 | 2850.815 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 411 | 411 | 0 | 4.575 | 2277.375 | 3031.039 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2274 | 2274 | 0 | 4.799 | 2277.375 | 2916.351 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 370 | 370 | 0 | 4.703 | 2244.607 | 2801.663 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 979 | 979 | 0 | 4.991 | 2228.223 | 2965.503 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 443 | 443 | 0 | 4.639 | 2129.919 | 2883.583 |
| t-205 | 18 | 1 | 65.466 | 19.469 | 449 | 0 | 1257 | 1257 | 0 | 4.639 | 1990.655 | 2998.271 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2574 | 2574 | 0 | 4.895 | 1982.463 | 2883.583 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5215 | 5215 | 0 | 4.831 | 1933.311 | 3014.655 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 370 | 370 | 0 | 4.735 | 1925.119 | 2457.599 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 683 | 683 | 0 | 4.863 | 1916.927 | 2899.967 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 927 | 927 | 0 | 4.543 | 1900.543 | 2965.503 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1008 | 1008 | 0 | 4.831 | 1884.159 | 2932.735 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3552 | 3552 | 0 | 4.671 | 1884.159 | 2703.359 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3272 | 3272 | 0 | 4.671 | 1884.159 | 3031.039 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2486 | 2486 | 0 | 4.575 | 1851.391 | 2981.887 |
| t-417 | 75 | 2 | 54.652 | 67.926 | 3449 | 0 | 4236 | 4236 | 0 | 4.767 | 1826.815 | 2965.503 |
| t-225 | 6 | 1 | 39.542 | 3.897 | 3030 | 0 | 243 | 243 | 0 | 5.151 | 1794.047 | 2850.815 |
| t-439 | 22 | 1 | 84.591 | 31.543 | 3339 | 0 | 1992 | 1992 | 0 | 4.767 | 1785.855 | 2899.967 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 431 | 431 | 0 | 5.087 | 1769.471 | 2752.511 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 744 | 744 | 0 | 4.767 | 1769.471 | 2883.583 |
