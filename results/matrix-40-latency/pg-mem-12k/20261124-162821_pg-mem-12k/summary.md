# pg-mem-12k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 591 workers, offered 11956.702 ev/s
- Phases: run 60.133s, closing 0.718s, drain 2.001s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 759885 | 759885 | 11948.912 | 11948.973 | 36609.9 | 0 | 0 | 0 | 445 | 445 | 169 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 597343 | 11.247 | 7.587 | 15.335 | 58.175 | 99.135 | 6672.383 |
| e2e_from_send | 597343 | 10.055 | 6.727 | 13.719 | 56.031 | 97.471 | 6668.287 |
| producer_lag | 597343 | 1.192 | 0.867 | 2.715 | 6.319 | 15.983 | 85.695 |
| publish_call | 759885 | 8.157 | 6.515 | 14.367 | 44.639 | 80.703 | 128.255 |
| queue_wait | 597343 | 8.858 | 6.707 | 13.695 | 55.871 | 93.311 | 4702.207 |
| broker_dwell | 597343 | 1.415 | 0.209 | 1.01 | 30.895 | 68.671 | 4694.015 |
| receive_call | 720318 | 43.455 | 18.191 | 112.319 | 354.815 | 478.463 | 511.743 |
| ctx_load | 597343 | 0.002 | 0.002 | 0.003 | 0.011 | 0.035 | 14.335 |
| process | 597343 | 1.18 | 0.001 | 0.002 | 0.009 | 0.047 | 2996.223 |
| ctx_commit | 597343 | 0.001 | 0.001 | 0.001 | 0.003 | 0.019 | 16.431 |
| ack | 597041 | 0.005 | 0.003 | 0.008 | 0.02 | 0.057 | 16.447 |
| service | 597343 | 1.197 | 0.013 | 0.03 | 0.064 | 0.201 | 2998.271 |
| e2e_large | 345 | 2545.188 | 2482.175 | 3545.087 | 5173.247 | 6672.383 | 6672.383 |
| ready_wait | 597343 | 1.884 | 0.346 | 3.411 | 31.855 | 68.671 | 1773.567 |
| blocked | 597343 | 1.863 | 0.346 | 3.411 | 31.791 | 68.415 | 1146.879 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11948.973 | 12844.1 |
| applied_total | 436244.796 | 723252 |
| bench_cpu_cores | 4.017 | 4.761 |
| bench_cpu_pct | 16.736 | 19.84 |
| bench_gc_gen0 | 2.592 | 4 |
| bench_gc_gen1 | 1.143 | 3 |
| bench_gc_gen2 | 0.755 | 2 |
| bench_gc_heap_mb | 858.343 | 1177.7 |
| bench_mem_mb | 1354.42 | 1524.9 |
| bench_threadpool_queue | 4.02 | 105 |
| bench_threadpool_threads | 28.245 | 29 |
| broker_backlog | 794.143 | 1515 |
| ctr_eb_mssql_cpu_pct | 4.066 | 33.83 |
| ctr_eb_mssql_mem_mb | 7053.278 | 7056.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 132.209 | 171.48 |
| ctr_eb_postgres_mem_mb | 3275.347 | 3461.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 29.92 | 37.54 |
| in_flight | 50.51 | 413 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.081 | 1.38 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 864488.408 | 1446153 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 190904.51 | 317185 |
| octopus_mqtt_sent_total | 297380.837 | 495983 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 11948.912 | 12846.1 |
| produced_total | 436295.306 | 723270 |
| publish_mb_per_s | 44.04 | 71.7 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 111,261 records are Completed |
| record_counts | pass | 111,261 records found, 111,261 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 445 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 759,885 events applied by workers, 759,885 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 169 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 32 ms, max 1147 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3458 seq 1: ready 118 ms before start, 118 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3461 seq 1: ready 116 ms before start, 116 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3423 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3460 seq 1: ready 355 ms before start, 355 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3431 seq 4: ready 787 ms before start, 787 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3445 seq 2: ready 497 ms before start, 497 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3465 seq 2: ready 928 ms before start, 928 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3483 seq 3: ready 457 ms before start, 457 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3476 seq 3: ready 383 ms before start, 383 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3479 seq 2: ready 681 ms before start, 681 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 572 ms before start, 572 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3480 seq 1: ready 208 ms before start, 208 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3426 seq 1: ready 552 ms before start, 552 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3424 seq 2: ready 939 ms before start, 939 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3424 seq 3: ready 124 ms before start, 124 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3479 seq 3: ready 244 ms before start, 244 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3459 seq 2: ready 702 ms before start, 702 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3471 seq 3: ready 639 ms before start, 639 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3450 seq 3: ready 356 ms before start, 356 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3432 seq 3: ready 889 ms before start, 889 ms of it with an idle slot in group 119

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 403 | 403 | 0 | 2392.063 | 5177.343 | 6684.671 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 76 | 76 | 0 | 2097.151 | 3883.007 | 3883.007 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 174 | 174 | 0 | 1867.775 | 3883.007 | 5111.807 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 80 | 80 | 0 | 1564.671 | 3014.655 | 3014.655 |
| t-167 | 10 | 1 | 23.069 | 3.977 | 1266 | 0 | 258 | 258 | 0 | 7.967 | 79.871 | 90.111 |
| t-157 | 28 | 1 | 22.423 | 10.002 | 2458 | 0 | 665 | 665 | 0 | 7.583 | 79.359 | 102.399 |
| t-284 | 12 | 1 | 25.949 | 4.934 | 217 | 0 | 310 | 310 | 0 | 8.095 | 78.335 | 97.279 |
| t-442 | 6 | 1 | 16.746 | 1.753 | 577 | 0 | 108 | 108 | 0 | 7.551 | 78.335 | 82.431 |
| t-471 | 28 | 1 | 9.761 | 4.628 | 330 | 0 | 360 | 360 | 0 | 7.487 | 78.335 | 99.839 |
| t-219 | 13 | 1 | 30.466 | 6.439 | 272 | 0 | 433 | 433 | 0 | 7.487 | 76.287 | 90.111 |
| t-087 | 6 | 1 | 32.49 | 3.506 | 1614 | 0 | 201 | 201 | 0 | 7.871 | 74.751 | 86.527 |
| t-492 | 6 | 1 | 24.207 | 2.206 | 3046 | 0 | 133 | 133 | 0 | 7.967 | 74.751 | 78.335 |
| t-148 | 6 | 1 | 58.48 | 5.345 | 715 | 0 | 325 | 325 | 0 | 7.775 | 73.727 | 75.775 |
| t-151 | 6 | 1 | 22.162 | 2.32 | 577 | 0 | 146 | 146 | 0 | 7.327 | 73.727 | 74.751 |
| t-405 | 6 | 1 | 22.731 | 2.254 | 874 | 0 | 159 | 159 | 0 | 7.839 | 73.215 | 79.359 |
| t-058 | 23 | 1 | 31.469 | 11.895 | 768 | 0 | 738 | 738 | 0 | 7.487 | 72.703 | 99.327 |
| t-085 | 19 | 1 | 26.288 | 8.173 | 1157 | 0 | 578 | 578 | 0 | 7.327 | 72.703 | 90.623 |
| t-139 | 16 | 1 | 16.369 | 4.625 | 1367 | 0 | 310 | 310 | 0 | 7.903 | 72.703 | 96.767 |
| t-156 | 9 | 1 | 34.296 | 5.054 | 968 | 0 | 305 | 305 | 0 | 7.583 | 72.191 | 72.703 |
| t-457 | 18 | 1 | 22.089 | 6.906 | 1557 | 0 | 500 | 500 | 0 | 7.263 | 72.191 | 99.327 |
| t-445 | 15 | 1 | 17.932 | 4.315 | 896 | 0 | 316 | 316 | 0 | 7.711 | 71.679 | 76.799 |
| t-180 | 19 | 1 | 44.075 | 13.279 | 152 | 0 | 862 | 862 | 0 | 7.615 | 71.167 | 98.815 |
| t-390 | 10 | 1 | 33.563 | 5.479 | 167 | 0 | 357 | 357 | 0 | 7.231 | 71.167 | 85.503 |
| t-096 | 15 | 1 | 45.031 | 11.244 | 1642 | 0 | 701 | 701 | 0 | 7.583 | 70.143 | 91.647 |
| t-281 | 10 | 1 | 32.393 | 5.695 | 2160 | 0 | 378 | 378 | 0 | 7.295 | 70.143 | 79.871 |
