# ms-mem-12k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 591 workers, offered 11956.702 ev/s
- Phases: run 60.174s, closing 1.175s, drain 1.757s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 759882 | 759882 | 11949.678 | 11948.876 | 24818.1 | 0 | 0 | 0 | 445 | 445 | 851 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 597339 | 20.43 | 12.647 | 32.175 | 146.687 | 299.519 | 6586.367 |
| e2e_from_send | 597339 | 18.591 | 11.391 | 29.599 | 123.455 | 293.887 | 6586.367 |
| producer_lag | 597339 | 1.839 | 0.921 | 2.905 | 17.759 | 116.095 | 329.983 |
| publish_call | 759882 | 21.861 | 11.495 | 38.271 | 173.055 | 688.127 | 1320.959 |
| queue_wait | 597339 | 17.391 | 11.375 | 29.583 | 121.983 | 282.111 | 4972.543 |
| broker_dwell | 597339 | 2.861 | 1.032 | 3.523 | 39.615 | 72.127 | 4964.351 |
| receive_call | 702345 | 44.878 | 19.535 | 116.031 | 355.071 | 478.463 | 512.511 |
| ctx_load | 597339 | 0.002 | 0.002 | 0.003 | 0.011 | 0.039 | 14.439 |
| process | 597339 | 1.18 | 0.001 | 0.002 | 0.007 | 0.055 | 2998.271 |
| ctx_commit | 597339 | 0.001 | 0.001 | 0.001 | 0.003 | 0.022 | 17.903 |
| ack | 596790 | 0.005 | 0.003 | 0.008 | 0.022 | 0.065 | 17.871 |
| service | 597339 | 1.199 | 0.013 | 0.032 | 0.074 | 0.461 | 3000.319 |
| e2e_large | 345 | 2580.354 | 2492.415 | 3688.447 | 5787.647 | 6586.367 | 6586.367 |
| ready_wait | 597339 | 3.293 | 0.433 | 9.135 | 47.679 | 127.999 | 1801.215 |
| blocked | 597339 | 3.271 | 0.433 | 9.135 | 47.551 | 125.311 | 1243.135 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11948.876 | 13426.8 |
| applied_total | 436097.184 | 723162 |
| bench_cpu_cores | 4.244 | 4.879 |
| bench_cpu_pct | 17.684 | 20.33 |
| bench_gc_gen0 | 2.633 | 4 |
| bench_gc_gen1 | 1.245 | 2 |
| bench_gc_gen2 | 0.816 | 2 |
| bench_gc_heap_mb | 853.496 | 1087.3 |
| bench_mem_mb | 1547.084 | 1721.5 |
| bench_threadpool_queue | 4.041 | 62 |
| bench_threadpool_threads | 29.102 | 34 |
| broker_backlog | 1189.898 | 1803 |
| ctr_eb_mssql_cpu_pct | 369.543 | 452 |
| ctr_eb_mssql_mem_mb | 6897.884 | 7121.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.219 | 0.35 |
| ctr_eb_postgres_mem_mb | 2592.739 | 2594 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 35.978 | 45.96 |
| in_flight | 49.755 | 550 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.485 | 1.71 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 865576.122 | 1442124 |
| octopus_missing_reports_total | 1667.898 | 3023 |
| octopus_mqtt_received_total | 176257.531 | 296317 |
| octopus_mqtt_sent_total | 286287.735 | 480747 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 11949.678 | 13428.8 |
| produced_total | 436146.939 | 723224 |
| publish_mb_per_s | 44.038 | 71.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 111,261 records are Completed |
| record_counts | pass | 111,261 records found, 111,261 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 445 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 759,882 events applied by workers, 759,882 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 851 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 48 ms, max 1243 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9279 seq 1: ready 107 ms before start, 107 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3462 seq 2: ready 212 ms before start, 212 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3617 seq 17: ready 132 ms before start, 132 ms of it with an idle slot in group 122
- [cross_source_blocking] source 3458 seq 1: ready 112 ms before start, 112 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3473 seq 3: ready 167 ms before start, 167 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3461 seq 1: ready 137 ms before start, 137 ms of it with an idle slot in group 119
- [cross_source_blocking] source 15167 seq 7: ready 107 ms before start, 107 ms of it with an idle slot in group 473
- [cross_source_blocking] source 391 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 11
- [cross_source_blocking] source 9282 seq 2: ready 129 ms before start, 129 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3470 seq 1: ready 110 ms before start, 110 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9269 seq 1: ready 119 ms before start, 119 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3423 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 119
- [cross_source_blocking] source 8285 seq 15: ready 140 ms before start, 140 ms of it with an idle slot in group 282
- [cross_source_blocking] source 8295 seq 19: ready 132 ms before start, 132 ms of it with an idle slot in group 282
- [cross_source_blocking] source 9967 seq 9: ready 117 ms before start, 117 ms of it with an idle slot in group 341
- [cross_source_blocking] source 9946 seq 7: ready 148 ms before start, 148 ms of it with an idle slot in group 341
- [cross_source_blocking] source 5536 seq 13: ready 147 ms before start, 147 ms of it with an idle slot in group 189
- [cross_source_blocking] source 5544 seq 8: ready 149 ms before start, 149 ms of it with an idle slot in group 189
- [cross_source_blocking] source 5503 seq 16: ready 116 ms before start, 116 ms of it with an idle slot in group 189
- [cross_source_blocking] source 7081 seq 18: ready 129 ms before start, 129 ms of it with an idle slot in group 239

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 403 | 403 | 0 | 2375.679 | 5799.935 | 6586.367 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 174 | 174 | 0 | 1916.927 | 3997.695 | 5111.807 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 76 | 76 | 0 | 2129.919 | 3932.159 | 3932.159 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 80 | 80 | 0 | 1581.055 | 3031.039 | 3031.039 |
| t-063 | 8 | 1 | 18.282 | 2.534 | 1296 | 0 | 174 | 174 | 0 | 12.991 | 272.383 | 303.103 |
| t-298 | 6 | 1 | 59.599 | 6.554 | 3700 | 0 | 394 | 394 | 0 | 12.799 | 252.927 | 311.295 |
| t-445 | 15 | 1 | 17.932 | 4.315 | 896 | 0 | 316 | 316 | 0 | 12.607 | 221.183 | 276.479 |
| t-226 | 6 | 1 | 81.796 | 8.651 | 623 | 0 | 543 | 543 | 0 | 12.927 | 220.159 | 290.815 |
| t-410 | 6 | 1 | 29.079 | 2.837 | 195 | 0 | 174 | 174 | 0 | 12.223 | 220.159 | 236.543 |
| t-311 | 15 | 1 | 21.242 | 5.528 | 761 | 0 | 391 | 391 | 0 | 12.735 | 219.135 | 307.199 |
| t-323 | 6 | 1 | 27.439 | 2.755 | 3540 | 0 | 178 | 178 | 0 | 12.863 | 209.919 | 217.087 |
| t-431 | 6 | 1 | 34.945 | 3.481 | 596 | 0 | 235 | 235 | 0 | 12.351 | 206.847 | 333.823 |
| t-212 | 29 | 1 | 47.053 | 22.764 | 296 | 0 | 1430 | 1430 | 0 | 12.799 | 205.823 | 301.055 |
| t-301 | 26 | 1 | 41.427 | 18.593 | 3339 | 0 | 1189 | 1189 | 0 | 12.991 | 205.823 | 305.151 |
| t-448 | 6 | 1 | 44.414 | 4.661 | 2313 | 0 | 281 | 281 | 0 | 13.439 | 205.823 | 278.527 |
| t-191 | 11 | 1 | 35.53 | 6.318 | 3529 | 0 | 427 | 427 | 0 | 12.159 | 203.775 | 274.431 |
| t-364 | 66 | 1 | 16.659 | 18.803 | 436 | 0 | 1338 | 1338 | 0 | 12.671 | 200.703 | 301.055 |
| t-026 | 6 | 1 | 112.395 | 11.273 | 1596 | 0 | 676 | 676 | 0 | 13.119 | 199.679 | 342.015 |
| t-082 | 42 | 1 | 16.657 | 11.758 | 150 | 0 | 878 | 878 | 0 | 12.351 | 198.655 | 264.191 |
| t-294 | 7 | 1 | 100.308 | 11.576 | 667 | 0 | 721 | 721 | 0 | 12.415 | 197.631 | 260.095 |
| t-013 | 10 | 1 | 34.137 | 5.18 | 134 | 0 | 354 | 354 | 0 | 13.055 | 195.583 | 292.863 |
| t-072 | 22 | 1 | 35.283 | 12.757 | 388 | 0 | 820 | 820 | 0 | 12.927 | 195.583 | 227.327 |
| t-384 | 12 | 1 | 20.754 | 4.117 | 844 | 0 | 263 | 263 | 0 | 12.735 | 195.583 | 241.663 |
| t-493 | 8 | 1 | 79.622 | 11.383 | 1419 | 0 | 753 | 753 | 0 | 12.799 | 194.559 | 362.495 |
| t-076 | 23 | 1 | 16.533 | 6.249 | 4045 | 0 | 416 | 416 | 0 | 13.055 | 193.535 | 260.095 |
