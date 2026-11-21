# M1-mssql-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.265s, closing 4.683s, drain 31.356s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 907759 | 907759 | 14204.771 | 14203.265 | 21395.5 | 0 | 0 | 0 | 424 | 4705 | 18576 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 704935 | 4029.301 | 4911.103 | 8675.327 | 10469.375 | 11534.335 | 44892.159 |
| e2e_from_send | 704935 | 124.176 | 60.191 | 168.447 | 1992.703 | 5062.655 | 38273.023 |
| producer_lag | 704935 | 3905.13 | 4743.167 | 8527.871 | 10215.423 | 11288.575 | 12222.463 |
| publish_call | 907759 | 96.29 | 58.783 | 158.207 | 395.519 | 4956.159 | 12214.271 |
| queue_wait | 704935 | 113.306 | 59.519 | 163.327 | 1482.751 | 5062.655 | 36896.767 |
| broker_dwell | 704935 | 25.673 | 4.047 | 19.247 | 756.735 | 2355.199 | 36765.695 |
| receive_call | 765026 | 42.571 | 21.423 | 103.487 | 329.983 | 473.855 | 527.871 |
| ctx_load | 704935 | 0.003 | 0.002 | 0.003 | 0.01 | 0.058 | 40.831 |
| process | 704935 | 10.844 | 0.001 | 0.002 | 0.015 | 2623.487 | 3012.607 |
| ctx_commit | 704935 | 0.002 | 0.001 | 0.001 | 0.004 | 0.028 | 20.671 |
| ack | 688775 | 0.004 | 0.002 | 0.004 | 0.015 | 0.078 | 43.487 |
| service | 704935 | 10.869 | 0.013 | 0.035 | 0.168 | 2623.487 | 3012.607 |
| e2e_large | 322 | 6293.582 | 4071.423 | 10838.015 | 24575.999 | 44892.159 | 44892.159 |
| ready_wait | 704935 | 16.621 | 2.889 | 19.023 | 150.783 | 2030.591 | 21463.039 |
| blocked | 704935 | 16.528 | 2.889 | 19.007 | 150.143 | 2030.591 | 20479.999 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 14203.265 | 21395.5 |
| applied_total | 515031.98 | 867138 |
| bench_cpu_cores | 2.782 | 4.81 |
| bench_cpu_pct | 11.59 | 20.04 |
| bench_gc_gen0 | 2.388 | 5 |
| bench_gc_gen1 | 1.041 | 2 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1089.29 | 1364.1 |
| bench_mem_mb | 1789.506 | 1943.9 |
| bench_threadpool_queue | 14.041 | 351 |
| bench_threadpool_threads | 43.531 | 44 |
| broker_backlog | 326203.122 | 493349 |
| ctr_eb_mosquitto_cpu_pct | 44.708 | 65.96 |
| ctr_eb_mosquitto_mem_mb | 9.198 | 21.6 |
| ctr_eb_mssql_cpu_pct | 641.654 | 916.57 |
| ctr_eb_mssql_mem_mb | 4310.404 | 5970 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.764 | 2.85 |
| ctr_eb_postgres_mem_mb | 2215.451 | 2222.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 50.963 | 78.91 |
| in_flight | 470.98 | 1604 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.906 | 2.85 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1017300.898 | 1678280 |
| octopus_missing_reports_total | 5.265 | 8 |
| octopus_mqtt_received_total | 132489.347 | 194352 |
| octopus_mqtt_sent_total | 262003.98 | 418799 |
| octopus_publish_retries_total | 510.735 | 1072 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 14204.771 | 21901.7 |
| produced_total | 515502.959 | 867715 |
| publish_mb_per_s | 46.99 | 80.27 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 128,218 records are Completed |
| record_counts | pass | 128,218 records found, 128,218 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 424 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 907,759 events applied by workers, 907,759 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,576 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 150 ms, max 20473 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9324 seq 19: ready 121 ms before start, 121 ms of it with an idle slot in group 322
- [cross_source_blocking] source 10260 seq 20: ready 121 ms before start, 121 ms of it with an idle slot in group 344
- [cross_source_blocking] source 1800 seq 8: ready 121 ms before start, 121 ms of it with an idle slot in group 51
- [cross_source_blocking] source 3620 seq 15: ready 121 ms before start, 121 ms of it with an idle slot in group 122
- [cross_source_blocking] source 15828 seq 30: ready 121 ms before start, 121 ms of it with an idle slot in group 496
- [cross_source_blocking] source 11824 seq 13: ready 119 ms before start, 119 ms of it with an idle slot in group 383
- [cross_source_blocking] source 7720 seq 21: ready 120 ms before start, 120 ms of it with an idle slot in group 263
- [cross_source_blocking] source 1552 seq 18: ready 118 ms before start, 118 ms of it with an idle slot in group 46
- [cross_source_blocking] source 8300 seq 18: ready 121 ms before start, 121 ms of it with an idle slot in group 282
- [cross_source_blocking] source 9700 seq 13: ready 121 ms before start, 121 ms of it with an idle slot in group 337
- [cross_source_blocking] source 10192 seq 9: ready 119 ms before start, 119 ms of it with an idle slot in group 344
- [cross_source_blocking] source 4348 seq 27: ready 120 ms before start, 120 ms of it with an idle slot in group 147
- [cross_source_blocking] source 6916 seq 18: ready 121 ms before start, 121 ms of it with an idle slot in group 235
- [cross_source_blocking] source 980 seq 25: ready 121 ms before start, 121 ms of it with an idle slot in group 29
- [cross_source_blocking] source 1244 seq 6: ready 120 ms before start, 120 ms of it with an idle slot in group 40
- [cross_source_blocking] source 10037 seq 41: ready 167 ms before start, 167 ms of it with an idle slot in group 342
- [cross_source_blocking] source 7405 seq 11: ready 170 ms before start, 170 ms of it with an idle slot in group 256
- [cross_source_blocking] source 2843 seq 4: ready 169 ms before start, 169 ms of it with an idle slot in group 89
- [cross_source_blocking] source 13851 seq 13: ready 170 ms before start, 170 ms of it with an idle slot in group 438
- [cross_source_blocking] source 187 seq 22: ready 169 ms before start, 169 ms of it with an idle slot in group 4

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 83 | 83 | 0 | 5144.575 | 31457.279 | 45088.767 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 168 | 168 | 0 | 4620.287 | 12320.767 | 12713.983 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 346 | 346 | 0 | 5046.271 | 12255.231 | 12648.447 |
| t-405 | 6 | 1 | 30.308 | 3.005 | 874 | 0 | 164 | 164 | 0 | 6520.831 | 12189.695 | 12255.231 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 3407.871 | 11993.087 | 12976.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 369 | 369 | 0 | 3981.311 | 11993.087 | 12517.375 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 346 | 346 | 0 | 5472.255 | 11534.335 | 12189.695 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 372 | 372 | 0 | 1212.415 | 11468.799 | 14024.703 |
| t-448 | 6 | 1 | 59.218 | 6.215 | 2313 | 0 | 317 | 317 | 0 | 5406.719 | 11468.799 | 12451.839 |
| t-453 | 6 | 1 | 55.729 | 5.489 | 874 | 0 | 313 | 313 | 0 | 6127.615 | 11468.799 | 11730.943 |
| t-137 | 10 | 1 | 27.033 | 4.549 | 1569 | 0 | 285 | 285 | 0 | 1761.279 | 11403.263 | 11599.871 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 1980 | 1980 | 0 | 5406.719 | 11403.263 | 12058.623 |
| t-387 | 13 | 1 | 32.794 | 7.372 | 138 | 0 | 448 | 448 | 0 | 6127.615 | 11403.263 | 11468.799 |
| t-466 | 11 | 1 | 49.014 | 8.135 | 449 | 0 | 485 | 485 | 0 | 6029.311 | 11403.263 | 14090.239 |
| t-033 | 6 | 1 | 87.319 | 9.519 | 859 | 0 | 521 | 521 | 0 | 6094.847 | 11337.727 | 11665.407 |
| t-101 | 6 | 1 | 39.336 | 3.539 | 198 | 0 | 201 | 201 | 0 | 5963.775 | 11337.727 | 11534.335 |
| t-140 | 13 | 1 | 78.584 | 17.196 | 429 | 0 | 935 | 935 | 0 | 3932.159 | 11337.727 | 11599.871 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 362 | 362 | 0 | 4079.615 | 11337.727 | 11534.335 |
| t-243 | 16 | 1 | 73.626 | 19.722 | 3143 | 0 | 1080 | 1080 | 0 | 5996.543 | 11337.727 | 12124.159 |
| t-294 | 7 | 1 | 133.744 | 15.435 | 667 | 0 | 904 | 904 | 0 | 4718.591 | 11337.727 | 11599.871 |
| t-321 | 10 | 1 | 79.218 | 14.205 | 204 | 0 | 737 | 737 | 0 | 4980.735 | 11337.727 | 11665.407 |
| t-412 | 6 | 1 | 35.26 | 3.264 | 468 | 0 | 187 | 187 | 0 | 6488.063 | 11272.191 | 11337.727 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 851 | 851 | 0 | 5963.775 | 11272.191 | 11665.407 |
| t-424 | 8 | 1 | 94.679 | 12.424 | 1700 | 0 | 673 | 673 | 0 | 5767.167 | 11272.191 | 11599.871 |
| t-489 | 6 | 1 | 173.223 | 17.681 | 1030 | 0 | 968 | 968 | 0 | 4882.431 | 11272.191 | 11599.871 |
