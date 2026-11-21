# Ya-pg-split-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.52s, closing 0.406s, drain 3.967s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 503638 | 503638 | 8024.555 | 8028.38 | 22003 | 0 | 0 | 0 | 373 | 2688 | 187 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 401123 | 36.913 | 4.455 | 30.527 | 1314.815 | 2738.175 | 6057.983 |
| e2e_from_send | 401123 | 36.649 | 4.339 | 29.791 | 1314.815 | 2738.175 | 6057.983 |
| producer_lag | 401123 | 0.264 | 0.002 | 0.473 | 3.049 | 37.055 | 165.503 |
| publish_call | 503638 | 8.253 | 4.051 | 16.239 | 69.503 | 139.647 | 236.671 |
| queue_wait | 401123 | 25.641 | 4.323 | 28.319 | 652.799 | 2271.231 | 3653.631 |
| broker_dwell | 401123 | 33.7 | 14.175 | 24.799 | 660.479 | 2279.423 | 3602.431 |
| receive_call | 488421 | 36.369 | 13.311 | 95.487 | 322.815 | 472.575 | 500.479 |
| ctx_load | 401123 | 0.001 | 0.001 | 0.001 | 0.003 | 0.013 | 0.164 |
| process | 401123 | 11.003 | 0.001 | 0.002 | 0.007 | 2613.247 | 3000.319 |
| ctx_commit | 401123 | 0.001 | 0.001 | 0.001 | 0.002 | 0.008 | 5.027 |
| ack | 299155 | 0.001 | 0.001 | 0.002 | 0.007 | 0.019 | 0.38 |
| service | 401123 | 11.008 | 0.005 | 0.01 | 0.029 | 2613.247 | 3000.319 |
| e2e_large | 282 | 2390.297 | 2355.199 | 3178.495 | 4329.471 | 6057.983 | 6057.983 |
| ready_wait | 401123 | 2.674 | 0.477 | 3.193 | 43.039 | 84.735 | 1651.711 |
| blocked | 401123 | 2.638 | 0.477 | 3.193 | 42.943 | 83.967 | 1101.823 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8028.38 | 8352.6 |
| applied_total | 288898.531 | 481325 |
| bench_cpu_cores | 4.47 | 4.859 |
| bench_cpu_pct | 44.696 | 48.59 |
| bench_gc_gen0 | 0.796 | 2 |
| bench_gc_gen1 | 0.469 | 1 |
| bench_gc_gen2 | 0.449 | 1 |
| bench_gc_heap_mb | 896.29 | 1401.6 |
| bench_mem_mb | 1844.841 | 2037 |
| bench_threadpool_queue | 0.796 | 10 |
| bench_threadpool_threads | 16.51 | 21 |
| broker_backlog | 705.612 | 955 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 235.122 | 417 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 578846.429 | 981893 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 269900.449 | 450170 |
| octopus_mqtt_sent_total | 293520.714 | 499931 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 8024.555 | 8272.6 |
| produced_total | 289133.653 | 481514 |
| publish_mb_per_s | 33.481 | 52 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 69,625 records are Completed |
| record_counts | pass | 69,625 records found, 69,625 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 373 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 503,638 events applied by workers, 503,638 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 187 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 43 ms, max 1101 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10334 seq 18: ready 135 ms before start, 135 ms of it with an idle slot in group 347
- [cross_source_blocking] source 13872 seq 22: ready 115 ms before start, 115 ms of it with an idle slot in group 439
- [cross_source_blocking] source 655 seq 17: ready 103 ms before start, 103 ms of it with an idle slot in group 19
- [cross_source_blocking] source 10096 seq 8: ready 114 ms before start, 114 ms of it with an idle slot in group 343
- [cross_source_blocking] source 5703 seq 29: ready 104 ms before start, 104 ms of it with an idle slot in group 197
- [cross_source_blocking] source 2337 seq 28: ready 116 ms before start, 116 ms of it with an idle slot in group 67
- [cross_source_blocking] source 14166 seq 2: ready 112 ms before start, 112 ms of it with an idle slot in group 451
- [cross_source_blocking] source 7079 seq 32: ready 112 ms before start, 112 ms of it with an idle slot in group 239
- [cross_source_blocking] source 9177 seq 16: ready 118 ms before start, 118 ms of it with an idle slot in group 315
- [cross_source_blocking] source 4327 seq 35: ready 125 ms before start, 125 ms of it with an idle slot in group 147
- [cross_source_blocking] source 5912 seq 10: ready 122 ms before start, 122 ms of it with an idle slot in group 205
- [cross_source_blocking] source 5723 seq 29: ready 115 ms before start, 115 ms of it with an idle slot in group 197
- [cross_source_blocking] source 3283 seq 17: ready 107 ms before start, 107 ms of it with an idle slot in group 109
- [cross_source_blocking] source 15379 seq 35: ready 122 ms before start, 122 ms of it with an idle slot in group 481
- [cross_source_blocking] source 9085 seq 8: ready 119 ms before start, 119 ms of it with an idle slot in group 313
- [cross_source_blocking] source 13027 seq 14: ready 123 ms before start, 123 ms of it with an idle slot in group 415
- [cross_source_blocking] source 15607 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 485
- [cross_source_blocking] source 12756 seq 4: ready 123 ms before start, 123 ms of it with an idle slot in group 403
- [cross_source_blocking] source 13864 seq 7: ready 103 ms before start, 103 ms of it with an idle slot in group 439
- [cross_source_blocking] source 8242 seq 9: ready 102 ms before start, 102 ms of it with an idle slot in group 279

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 1908.735 | 4358.143 | 5767.167 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 1859.583 | 4325.375 | 6062.079 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2473.983 | 4145.151 | 4259.839 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 707 | 707 | 0 | 4.447 | 2654.207 | 4325.375 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 759 | 759 | 0 | 4.607 | 2310.143 | 2850.815 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 411 | 411 | 0 | 4.191 | 2277.375 | 2998.271 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2274 | 2274 | 0 | 4.575 | 2277.375 | 2916.351 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 370 | 370 | 0 | 4.511 | 2228.223 | 2801.663 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 979 | 979 | 0 | 4.703 | 2162.687 | 2965.503 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 443 | 443 | 0 | 4.511 | 2129.919 | 2883.583 |
| t-205 | 18 | 1 | 65.466 | 19.469 | 449 | 0 | 1257 | 1257 | 0 | 4.447 | 2056.191 | 2998.271 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2574 | 2574 | 0 | 4.671 | 1974.271 | 2867.199 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 370 | 370 | 0 | 4.383 | 1949.695 | 2473.983 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5215 | 5215 | 0 | 4.607 | 1933.311 | 2998.271 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 683 | 683 | 0 | 4.639 | 1916.927 | 2899.967 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 927 | 927 | 0 | 4.447 | 1900.543 | 2965.503 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1008 | 1008 | 0 | 4.511 | 1884.159 | 2949.119 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3552 | 3552 | 0 | 4.383 | 1884.159 | 2686.975 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3271 | 3271 | 0 | 4.479 | 1884.159 | 3014.655 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2486 | 2486 | 0 | 4.447 | 1859.583 | 2981.887 |
| t-417 | 75 | 2 | 54.652 | 67.926 | 3449 | 0 | 4236 | 4236 | 0 | 4.511 | 1851.391 | 2949.119 |
| t-439 | 22 | 1 | 84.591 | 31.543 | 3339 | 0 | 1992 | 1992 | 0 | 4.639 | 1810.431 | 2899.967 |
| t-225 | 6 | 1 | 39.542 | 3.897 | 3030 | 0 | 243 | 243 | 0 | 4.671 | 1794.047 | 2850.815 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 431 | 431 | 0 | 4.735 | 1769.471 | 2752.511 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 744 | 744 | 0 | 4.703 | 1769.471 | 2867.199 |
