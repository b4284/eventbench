# Yb-pg-split-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.538s, closing 0.582s, drain 3.773s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 503641 | 503641 | 8024.127 | 8025.778 | 15626.7 | 0 | 0 | 0 | 373 | 2688 | 145 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 401125 | 38.101 | 5.063 | 32.959 | 1320.959 | 2740.223 | 6074.367 |
| e2e_from_send | 401125 | 37.817 | 4.931 | 32.127 | 1320.959 | 2738.175 | 6070.271 |
| producer_lag | 401125 | 0.284 | 0.003 | 0.475 | 4.055 | 38.143 | 169.727 |
| publish_call | 503641 | 9.35 | 4.527 | 19.471 | 73.215 | 150.015 | 254.079 |
| queue_wait | 401125 | 26.808 | 4.911 | 30.703 | 658.431 | 2275.327 | 3665.919 |
| broker_dwell | 401125 | 28.427 | 8.375 | 22.175 | 659.455 | 2277.375 | 3596.287 |
| receive_call | 488595 | 36.549 | 13.327 | 96.319 | 323.327 | 473.087 | 500.479 |
| ctx_load | 401125 | 0.001 | 0.001 | 0.001 | 0.004 | 0.02 | 0.33 |
| process | 401125 | 11.003 | 0.001 | 0.002 | 0.009 | 2613.247 | 3000.319 |
| ctx_commit | 401125 | 0.001 | 0.001 | 0.001 | 0.002 | 0.011 | 2.229 |
| ack | 292145 | 0.002 | 0.001 | 0.003 | 0.007 | 0.03 | 6.263 |
| service | 401125 | 11.009 | 0.005 | 0.013 | 0.041 | 2613.247 | 3000.319 |
| e2e_large | 282 | 2411.135 | 2365.439 | 3303.423 | 4333.567 | 6074.367 | 6074.367 |
| ready_wait | 401125 | 3.205 | 0.64 | 5.315 | 46.815 | 89.535 | 1686.527 |
| blocked | 401125 | 3.171 | 0.64 | 5.315 | 46.687 | 89.279 | 1035.775 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8025.778 | 8403 |
| applied_total | 288767.776 | 481154 |
| bench_cpu_cores | 4.705 | 4.922 |
| bench_cpu_pct | 47.052 | 49.22 |
| bench_gc_gen0 | 0.755 | 2 |
| bench_gc_gen1 | 0.429 | 1 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 926.21 | 1667.6 |
| bench_mem_mb | 1774.486 | 2039 |
| bench_threadpool_queue | 0.449 | 8 |
| bench_threadpool_threads | 16.204 | 19 |
| broker_backlog | 1573.49 | 2158 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 246.98 | 550 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 582102.408 | 987760 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 269896.571 | 450872 |
| octopus_mqtt_sent_total | 292290.163 | 498440 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 8024.127 | 8279.1 |
| produced_total | 289014.755 | 481351 |
| publish_mb_per_s | 33.48 | 51.8 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 69,626 records are Completed |
| record_counts | pass | 69,626 records found, 69,626 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 373 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 503,641 events applied by workers, 503,641 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 145 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 47 ms, max 1036 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12311 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 391
- [cross_source_blocking] source 11122 seq 21: ready 107 ms before start, 107 ms of it with an idle slot in group 363
- [cross_source_blocking] source 3877 seq 23: ready 108 ms before start, 108 ms of it with an idle slot in group 131
- [cross_source_blocking] source 1156 seq 2: ready 107 ms before start, 107 ms of it with an idle slot in group 39
- [cross_source_blocking] source 15636 seq 33: ready 107 ms before start, 107 ms of it with an idle slot in group 487
- [cross_source_blocking] source 8389 seq 12: ready 107 ms before start, 107 ms of it with an idle slot in group 285
- [cross_source_blocking] source 3309 seq 22: ready 124 ms before start, 124 ms of it with an idle slot in group 111
- [cross_source_blocking] source 3439 seq 2: ready 304 ms before start, 110 ms of it with an idle slot in group 119
- [cross_source_blocking] source 13287 seq 29: ready 195 ms before start, 195 ms of it with an idle slot in group 425
- [cross_source_blocking] source 2433 seq 9: ready 206 ms before start, 206 ms of it with an idle slot in group 71
- [cross_source_blocking] source 8737 seq 22: ready 201 ms before start, 201 ms of it with an idle slot in group 303
- [cross_source_blocking] source 1487 seq 20: ready 210 ms before start, 210 ms of it with an idle slot in group 45
- [cross_source_blocking] source 12200 seq 9: ready 215 ms before start, 215 ms of it with an idle slot in group 389
- [cross_source_blocking] source 592 seq 7: ready 206 ms before start, 206 ms of it with an idle slot in group 17
- [cross_source_blocking] source 2920 seq 1: ready 571 ms before start, 571 ms of it with an idle slot in group 93
- [cross_source_blocking] source 3455 seq 1: ready 198 ms before start, 198 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3477 seq 3: ready 446 ms before start, 132 ms of it with an idle slot in group 119
- [cross_source_blocking] source 11369 seq 16: ready 107 ms before start, 107 ms of it with an idle slot in group 371
- [cross_source_blocking] source 7058 seq 29: ready 117 ms before start, 117 ms of it with an idle slot in group 239
- [cross_source_blocking] source 969 seq 30: ready 109 ms before start, 109 ms of it with an idle slot in group 29
- [cross_source_blocking] source 13034 seq 20: ready 117 ms before start, 117 ms of it with an idle slot in group 415

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 1908.735 | 4358.143 | 5799.935 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 1867.775 | 4325.375 | 6094.847 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2621.439 | 3719.167 | 4292.607 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 707 | 707 | 0 | 4.959 | 2654.207 | 4325.375 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 759 | 759 | 0 | 5.375 | 2310.143 | 2850.815 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2274 | 2274 | 0 | 5.279 | 2293.759 | 2916.351 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 411 | 411 | 0 | 4.927 | 2277.375 | 3014.655 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 370 | 370 | 0 | 5.055 | 2228.223 | 2801.663 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 979 | 979 | 0 | 5.119 | 2162.687 | 2965.503 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 443 | 443 | 0 | 5.183 | 2129.919 | 2883.583 |
| t-205 | 18 | 1 | 65.466 | 19.469 | 449 | 0 | 1257 | 1257 | 0 | 5.087 | 1982.463 | 3014.655 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2574 | 2574 | 0 | 5.119 | 1974.271 | 2883.583 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 370 | 370 | 0 | 5.055 | 1949.695 | 2457.599 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5215 | 5215 | 0 | 5.215 | 1933.311 | 3014.655 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 683 | 683 | 0 | 5.183 | 1916.927 | 2899.967 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 927 | 927 | 0 | 5.087 | 1900.543 | 2965.503 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1008 | 1008 | 0 | 4.959 | 1884.159 | 2932.735 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3552 | 3552 | 0 | 5.023 | 1884.159 | 2703.359 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3271 | 3271 | 0 | 5.087 | 1884.159 | 3014.655 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2486 | 2486 | 0 | 5.023 | 1859.583 | 2998.271 |
| t-417 | 75 | 2 | 54.652 | 67.926 | 3449 | 0 | 4236 | 4236 | 0 | 5.247 | 1818.623 | 2981.887 |
| t-225 | 6 | 1 | 39.542 | 3.897 | 3030 | 0 | 243 | 243 | 0 | 5.535 | 1794.047 | 2850.815 |
| t-439 | 22 | 1 | 84.591 | 31.543 | 3339 | 0 | 1992 | 1992 | 0 | 5.151 | 1785.855 | 2899.967 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 431 | 431 | 0 | 5.503 | 1777.663 | 2752.511 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 744 | 744 | 0 | 5.183 | 1769.471 | 2867.199 |
