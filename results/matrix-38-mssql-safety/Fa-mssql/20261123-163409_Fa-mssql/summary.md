# Fa-mssql — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.178s, closing 1.589s, drain 1.504s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000521 | 1000521 | 17467.549 | 17461.214 | 24691.4 | 0 | 0 | 0 | 458 | 5162 | 16141 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797154 | 1008.041 | 18.367 | 3876.863 | 6180.863 | 6582.271 | 9969.663 |
| e2e_from_send | 797154 | 69.745 | 15.455 | 114.431 | 1411.071 | 2793.471 | 6332.415 |
| producer_lag | 797154 | 938.296 | 2.223 | 3751.935 | 6045.695 | 6369.279 | 6860.799 |
| publish_call | 1000521 | 55.824 | 18.015 | 125.055 | 521.215 | 1571.839 | 2643.967 |
| queue_wait | 797154 | 58.987 | 15.335 | 111.103 | 850.943 | 2385.919 | 4722.687 |
| broker_dwell | 797154 | 25.703 | 2.191 | 15.255 | 807.423 | 2349.055 | 4526.079 |
| receive_call | 876371 | 39.815 | 17.343 | 99.007 | 328.703 | 474.623 | 536.575 |
| ctx_load | 797154 | 0.003 | 0.002 | 0.004 | 0.01 | 0.05 | 23.983 |
| process | 797154 | 10.731 | 0.001 | 0.003 | 0.016 | 2619.391 | 3010.559 |
| ctx_commit | 797154 | 0.002 | 0.001 | 0.001 | 0.004 | 0.027 | 42.239 |
| ack | 795299 | 0.005 | 0.003 | 0.008 | 0.023 | 0.089 | 56.959 |
| service | 797154 | 10.757 | 0.015 | 0.039 | 0.149 | 2619.391 | 3010.559 |
| e2e_large | 356 | 3894.062 | 3092.479 | 7200.767 | 9207.807 | 9969.663 | 9969.663 |
| ready_wait | 797154 | 8.005 | 1.027 | 13.847 | 137.087 | 287.743 | 1848.319 |
| blocked | 797154 | 7.88 | 1.027 | 13.839 | 136.063 | 255.231 | 806.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 17461.214 | 24691.4 |
| applied_total | 560285.918 | 960379 |
| bench_cpu_cores | 4.881 | 5.894 |
| bench_cpu_pct | 20.34 | 24.56 |
| bench_gc_gen0 | 3.143 | 5 |
| bench_gc_gen1 | 1.388 | 3 |
| bench_gc_gen2 | 0.857 | 2 |
| bench_gc_heap_mb | 1045.808 | 1326 |
| bench_mem_mb | 1789.471 | 1991.9 |
| bench_threadpool_queue | 43.51 | 309 |
| bench_threadpool_threads | 37 | 39 |
| broker_backlog | 3049.469 | 9003 |
| ctr_eb_mssql_cpu_pct | 733.345 | 987.05 |
| ctr_eb_mssql_mem_mb | 6412 | 6609 |
| ctr_eb_mysql_cpu_pct | 0.666 | 0.72 |
| ctr_eb_mysql_mem_mb | 6115.727 | 6140.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 9.98 | 10.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.429 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 10.814 | 11.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.602 | 11 |
| ctr_eb_postgres_cpu_pct | 0.351 | 0.59 |
| ctr_eb_postgres_mem_mb | 2202.543 | 2203.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 62.437 | 74.06 |
| in_flight | 807.122 | 3703 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.561 | 2.91 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1116847.367 | 1928831 |
| octopus_missing_reports_total | 1595.857 | 2121 |
| octopus_mqtt_received_total | 139877.367 | 270308 |
| octopus_mqtt_sent_total | 313292.653 | 558277 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 17467.549 | 24386.1 |
| produced_total | 561093.041 | 961338 |
| publish_mb_per_s | 58.543 | 87.79 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,521 events applied by workers, 1,000,521 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 16,141 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 136 ms, max 806 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13304 seq 23: ready 198 ms before start, 198 ms of it with an idle slot in group 425
- [cross_source_blocking] source 13328 seq 21: ready 198 ms before start, 198 ms of it with an idle slot in group 425
- [cross_source_blocking] source 11664 seq 21: ready 142 ms before start, 142 ms of it with an idle slot in group 378
- [cross_source_blocking] source 11184 seq 8: ready 636 ms before start, 636 ms of it with an idle slot in group 364
- [cross_source_blocking] source 2812 seq 11: ready 636 ms before start, 636 ms of it with an idle slot in group 88
- [cross_source_blocking] source 2364 seq 4: ready 639 ms before start, 639 ms of it with an idle slot in group 69
- [cross_source_blocking] source 176 seq 7: ready 639 ms before start, 639 ms of it with an idle slot in group 3
- [cross_source_blocking] source 5052 seq 9: ready 639 ms before start, 639 ms of it with an idle slot in group 176
- [cross_source_blocking] source 14812 seq 11: ready 639 ms before start, 639 ms of it with an idle slot in group 465
- [cross_source_blocking] source 3100 seq 9: ready 639 ms before start, 639 ms of it with an idle slot in group 103
- [cross_source_blocking] source 3868 seq 20: ready 639 ms before start, 639 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3900 seq 16: ready 639 ms before start, 639 ms of it with an idle slot in group 131
- [cross_source_blocking] source 60 seq 8: ready 639 ms before start, 639 ms of it with an idle slot in group 1
- [cross_source_blocking] source 7964 seq 10: ready 640 ms before start, 640 ms of it with an idle slot in group 268
- [cross_source_blocking] source 14492 seq 8: ready 639 ms before start, 639 ms of it with an idle slot in group 461
- [cross_source_blocking] source 8060 seq 32: ready 641 ms before start, 641 ms of it with an idle slot in group 271
- [cross_source_blocking] source 312 seq 8: ready 103 ms before start, 103 ms of it with an idle slot in group 9
- [cross_source_blocking] source 10896 seq 5: ready 103 ms before start, 103 ms of it with an idle slot in group 354
- [cross_source_blocking] source 4720 seq 18: ready 132 ms before start, 132 ms of it with an idle slot in group 163
- [cross_source_blocking] source 8092 seq 30: ready 132 ms before start, 132 ms of it with an idle slot in group 273
- [cross_source_blocking] source 3100 seq 10: ready 132 ms before start, 132 ms of it with an idle slot in group 103

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2686.975 | 9240.575 | 10027.007 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2736.127 | 8978.431 | 9764.863 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2785.279 | 8323.071 | 8912.895 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2506.751 | 8028.159 | 9437.183 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 260 | 260 | 0 | 17.151 | 7077.887 | 7569.407 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 17.151 | 6684.671 | 8191.999 |
| t-203 | 6 | 1 | 35.674 | 3.855 | 2258 | 0 | 234 | 234 | 0 | 18.047 | 6488.063 | 6488.063 |
| t-311 | 15 | 1 | 28.322 | 7.37 | 761 | 0 | 509 | 509 | 0 | 19.455 | 6455.295 | 8224.767 |
| t-190 | 6 | 1 | 85.928 | 8.971 | 241 | 0 | 551 | 551 | 0 | 19.199 | 6422.527 | 7962.623 |
| t-401 | 11 | 1 | 37.414 | 7.028 | 360 | 0 | 469 | 469 | 0 | 19.839 | 6422.527 | 9175.039 |
| t-087 | 6 | 1 | 43.32 | 4.675 | 1614 | 0 | 287 | 287 | 0 | 20.991 | 6389.759 | 6553.599 |
| t-090 | 7 | 1 | 49.513 | 5.825 | 242 | 0 | 331 | 331 | 0 | 23.551 | 6389.759 | 7045.119 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 19.711 | 6389.759 | 6520.831 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 881 | 881 | 0 | 17.663 | 6389.759 | 8585.215 |
| t-226 | 6 | 1 | 109.061 | 11.534 | 623 | 0 | 716 | 716 | 0 | 19.199 | 6389.759 | 6586.367 |
| t-279 | 18 | 1 | 44.014 | 12.994 | 196 | 0 | 835 | 835 | 0 | 18.687 | 6389.759 | 7143.423 |
| t-481 | 12 | 1 | 150.551 | 28.385 | 1993 | 0 | 1822 | 1822 | 0 | 19.199 | 6389.759 | 7831.551 |
| t-487 | 9 | 1 | 120.825 | 17.666 | 755 | 0 | 1093 | 1093 | 0 | 17.535 | 6389.759 | 6553.599 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 19.839 | 6356.991 | 7077.887 |
| t-012 | 6 | 1 | 76.677 | 7.946 | 556 | 0 | 505 | 505 | 0 | 16.255 | 6356.991 | 6553.599 |
| t-033 | 6 | 1 | 87.319 | 9.519 | 859 | 0 | 581 | 581 | 0 | 17.535 | 6356.991 | 6488.063 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 22.143 | 6356.991 | 7274.495 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 400 | 400 | 0 | 20.607 | 6356.991 | 6488.063 |
| t-149 | 13 | 1 | 42.874 | 8.748 | 3034 | 0 | 530 | 530 | 0 | 15.999 | 6356.991 | 6553.599 |
| t-216 | 19 | 1 | 75.625 | 24.458 | 3889 | 0 | 1577 | 1577 | 0 | 18.815 | 6356.991 | 7307.263 |
