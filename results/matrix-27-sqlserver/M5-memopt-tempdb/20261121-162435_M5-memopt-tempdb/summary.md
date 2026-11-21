# M5-memopt-tempdb — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.325s, closing 2.005s, drain 4.769s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 985773 | 985773 | 15961.68 | 15962.578 | 21368.4 | 0 | 0 | 0 | 454 | 5080 | 48940 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 782537 | 1168.751 | 543.231 | 3284.991 | 6148.095 | 7073.791 | 13541.375 |
| e2e_from_send | 782537 | 116.336 | 54.175 | 200.575 | 1593.343 | 2889.727 | 12697.599 |
| producer_lag | 782537 | 1052.416 | 374.783 | 3119.103 | 5996.543 | 6868.991 | 7880.703 |
| publish_call | 985773 | 81.727 | 50.847 | 165.759 | 526.335 | 1603.583 | 6574.079 |
| queue_wait | 782537 | 105.571 | 53.727 | 194.815 | 1166.335 | 2504.703 | 11501.567 |
| broker_dwell | 782537 | 35.174 | 6.223 | 43.263 | 775.679 | 2398.207 | 11026.431 |
| receive_call | 824257 | 43.096 | 20.095 | 108.095 | 334.335 | 472.575 | 547.839 |
| ctx_load | 782537 | 0.003 | 0.002 | 0.004 | 0.012 | 0.078 | 38.079 |
| process | 782537 | 10.729 | 0.001 | 0.002 | 0.018 | 2619.391 | 3016.703 |
| ctx_commit | 782537 | 0.003 | 0.001 | 0.001 | 0.005 | 0.04 | 42.975 |
| ack | 765774 | 0.005 | 0.002 | 0.005 | 0.019 | 0.115 | 51.615 |
| service | 782537 | 10.765 | 0.014 | 0.041 | 0.319 | 2619.391 | 3016.703 |
| e2e_large | 352 | 4434.995 | 3944.447 | 6914.047 | 10182.655 | 13541.375 | 13541.375 |
| ready_wait | 782537 | 21.988 | 4.013 | 62.463 | 238.335 | 670.207 | 6963.199 |
| blocked | 782537 | 21.905 | 4.013 | 62.399 | 237.695 | 652.799 | 6963.199 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15962.578 | 20329.1 |
| applied_total | 560214.286 | 945985 |
| bench_cpu_cores | 3.272 | 4.081 |
| bench_cpu_pct | 13.634 | 17.01 |
| bench_gc_gen0 | 2.612 | 5 |
| bench_gc_gen1 | 1.102 | 2 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1167.114 | 1479.3 |
| bench_mem_mb | 1886.347 | 2082.5 |
| bench_threadpool_queue | 38.163 | 373 |
| bench_threadpool_threads | 43 | 44 |
| broker_backlog | 23342.082 | 64062 |
| ctr_eb_mosquitto_cpu_pct | 58.402 | 65.75 |
| ctr_eb_mosquitto_mem_mb | 9.104 | 20.7 |
| ctr_eb_mssql_cpu_pct | 954.807 | 1173.86 |
| ctr_eb_mssql_mem_mb | 6466.924 | 7397.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.023 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.675 | 2.02 |
| ctr_eb_postgres_mem_mb | 2513.106 | 2516.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 68.46 | 84.72 |
| in_flight | 634.98 | 2034 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.363 | 2.65 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1099354.429 | 1887992 |
| octopus_missing_reports_total | 2352.041 | 3183 |
| octopus_mqtt_received_total | 133128.51 | 230990 |
| octopus_mqtt_sent_total | 284451.245 | 500562 |
| octopus_publish_retries_total | 6.041 | 22 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15961.68 | 20678.6 |
| produced_total | 560849.265 | 946541 |
| publish_mb_per_s | 52.49 | 83.49 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,149 records are Completed |
| record_counts | pass | 137,149 records found, 137,149 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 454 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 985,773 events applied by workers, 985,773 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 48,940 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 238 ms, max 6962 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6903 seq 12: ready 428 ms before start, 428 ms of it with an idle slot in group 235
- [cross_source_blocking] source 13671 seq 10: ready 413 ms before start, 413 ms of it with an idle slot in group 435
- [cross_source_blocking] source 4851 seq 10: ready 429 ms before start, 429 ms of it with an idle slot in group 168
- [cross_source_blocking] source 3275 seq 11: ready 412 ms before start, 412 ms of it with an idle slot in group 109
- [cross_source_blocking] source 7059 seq 20: ready 413 ms before start, 413 ms of it with an idle slot in group 239
- [cross_source_blocking] source 14543 seq 11: ready 430 ms before start, 430 ms of it with an idle slot in group 461
- [cross_source_blocking] source 11667 seq 23: ready 430 ms before start, 430 ms of it with an idle slot in group 378
- [cross_source_blocking] source 13335 seq 7: ready 414 ms before start, 414 ms of it with an idle slot in group 426
- [cross_source_blocking] source 7222 seq 54: ready 134 ms before start, 134 ms of it with an idle slot in group 246
- [cross_source_blocking] source 2242 seq 34: ready 133 ms before start, 133 ms of it with an idle slot in group 65
- [cross_source_blocking] source 185 seq 25: ready 147 ms before start, 147 ms of it with an idle slot in group 4
- [cross_source_blocking] source 13081 seq 10: ready 147 ms before start, 147 ms of it with an idle slot in group 416
- [cross_source_blocking] source 13849 seq 14: ready 142 ms before start, 142 ms of it with an idle slot in group 438
- [cross_source_blocking] source 2287 seq 50: ready 342 ms before start, 342 ms of it with an idle slot in group 65
- [cross_source_blocking] source 14658 seq 9: ready 107 ms before start, 107 ms of it with an idle slot in group 464
- [cross_source_blocking] source 4622 seq 12: ready 107 ms before start, 107 ms of it with an idle slot in group 161
- [cross_source_blocking] source 3579 seq 20: ready 342 ms before start, 342 ms of it with an idle slot in group 121
- [cross_source_blocking] source 9039 seq 9: ready 342 ms before start, 342 ms of it with an idle slot in group 312
- [cross_source_blocking] source 4291 seq 51: ready 342 ms before start, 342 ms of it with an idle slot in group 147
- [cross_source_blocking] source 5496 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 189

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 395 | 395 | 0 | 3784.703 | 10551.295 | 13565.951 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 180 | 180 | 0 | 3211.263 | 8781.823 | 9961.471 |
| t-171 | 14 | 1 | 119.591 | 28.366 | 142 | 0 | 1632 | 1632 | 0 | 511.999 | 7340.031 | 9109.503 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 386 | 386 | 0 | 1081.343 | 7143.423 | 8126.463 |
| t-311 | 15 | 1 | 28.322 | 7.37 | 761 | 0 | 503 | 503 | 0 | 226.303 | 7045.119 | 7405.567 |
| t-008 | 11 | 1 | 91.742 | 16.542 | 279 | 0 | 940 | 940 | 0 | 794.623 | 7012.351 | 8716.287 |
| t-081 | 6 | 1 | 16.193 | 1.614 | 2170 | 0 | 115 | 115 | 0 | 778.239 | 6979.583 | 7208.959 |
| t-024 | 14 | 1 | 41.39 | 9.449 | 1743 | 0 | 587 | 587 | 0 | 335.871 | 6946.815 | 7143.423 |
| t-423 | 11 | 1 | 45.411 | 8.191 | 726 | 0 | 493 | 493 | 0 | 798.719 | 6946.815 | 7077.887 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 722 | 722 | 0 | 667.647 | 6914.047 | 7471.103 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 863 | 863 | 0 | 1155.071 | 6914.047 | 9306.111 |
| t-432 | 9 | 1 | 72.613 | 10.689 | 747 | 0 | 640 | 640 | 0 | 179.199 | 6914.047 | 7110.655 |
| t-223 | 13 | 1 | 133.399 | 28.216 | 2372 | 0 | 1687 | 1687 | 0 | 872.447 | 6881.279 | 7274.495 |
| t-316 | 19 | 1 | 33.29 | 10.596 | 169 | 0 | 714 | 714 | 0 | 454.655 | 6881.279 | 7897.087 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 407 | 407 | 0 | 720.895 | 6881.279 | 7110.655 |
| t-405 | 6 | 1 | 30.308 | 3.005 | 874 | 0 | 183 | 183 | 0 | 163.839 | 6881.279 | 6914.047 |
| t-411 | 15 | 1 | 62.789 | 14.398 | 2644 | 0 | 900 | 900 | 0 | 704.511 | 6881.279 | 9568.255 |
| t-452 | 6 | 1 | 25.545 | 2.672 | 1797 | 0 | 137 | 137 | 0 | 1449.983 | 6881.279 | 7340.031 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 553 | 553 | 0 | 438.271 | 6848.511 | 7766.015 |
| t-339 | 17 | 1 | 15.431 | 4.427 | 134 | 0 | 319 | 319 | 0 | 514.047 | 6848.511 | 7110.655 |
| t-370 | 12 | 1 | 24.807 | 5.043 | 568 | 0 | 316 | 316 | 0 | 415.743 | 6848.511 | 7045.119 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 3679 | 3679 | 0 | 548.863 | 6848.511 | 7503.871 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1661 | 1661 | 0 | 497.663 | 6815.743 | 8978.431 |
| t-148 | 6 | 1 | 77.973 | 7.127 | 715 | 0 | 444 | 444 | 0 | 1286.143 | 6815.743 | 7143.423 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 2177 | 2177 | 0 | 573.439 | 6815.743 | 7634.943 |
