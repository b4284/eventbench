# Da-mssql — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 62.743s, closing 1.563s, drain 0.276s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000481 | 1000481 | 15367.396 | 15376.231 | 24229.7 | 0 | 0 | 0 | 458 | 5161 | 1803 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797115 | 48.205 | 15.679 | 35.519 | 1315.839 | 2768.895 | 5640.191 |
| e2e_from_send | 797115 | 45.816 | 14.279 | 30.591 | 1307.647 | 2754.559 | 5636.095 |
| producer_lag | 797115 | 2.389 | 0.984 | 3.623 | 21.151 | 154.879 | 4423.679 |
| publish_call | 1000481 | 25.02 | 14.319 | 43.999 | 205.951 | 547.839 | 4472.831 |
| queue_wait | 797115 | 35.068 | 14.231 | 29.791 | 658.943 | 2297.855 | 4681.727 |
| broker_dwell | 797115 | 19.07 | 0.941 | 5.551 | 641.535 | 2273.279 | 4673.535 |
| receive_call | 920305 | 38.298 | 16.559 | 96.639 | 321.279 | 472.063 | 532.479 |
| ctx_load | 797115 | 0.003 | 0.002 | 0.004 | 0.011 | 0.045 | 30.991 |
| process | 797115 | 10.726 | 0.001 | 0.003 | 0.015 | 2619.391 | 3002.367 |
| ctx_commit | 797115 | 0.002 | 0.001 | 0.001 | 0.004 | 0.026 | 25.247 |
| ack | 796455 | 0.005 | 0.003 | 0.008 | 0.023 | 0.077 | 30.191 |
| service | 797115 | 10.748 | 0.014 | 0.035 | 0.114 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2489.321 | 2484.223 | 3293.183 | 4677.631 | 5640.191 | 5640.191 |
| ready_wait | 797115 | 4.641 | 0.668 | 13.599 | 57.855 | 166.527 | 1864.703 |
| blocked | 797115 | 4.568 | 0.668 | 13.599 | 57.631 | 159.103 | 1055.743 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15376.231 | 17818.7 |
| applied_total | 593542.941 | 964173 |
| bench_cpu_cores | 5.018 | 5.605 |
| bench_cpu_pct | 20.906 | 23.35 |
| bench_gc_gen0 | 2.745 | 5 |
| bench_gc_gen1 | 1.176 | 3 |
| bench_gc_gen2 | 0.608 | 2 |
| bench_gc_heap_mb | 1057.949 | 1329.3 |
| bench_mem_mb | 1768.784 | 1940 |
| bench_threadpool_queue | 31.176 | 401 |
| bench_threadpool_threads | 38.275 | 43 |
| broker_backlog | 2468.745 | 3815 |
| ctr_eb_mssql_cpu_pct | 630.924 | 700.14 |
| ctr_eb_mssql_mem_mb | 6616.098 | 6733.9 |
| ctr_eb_mysql_cpu_pct | 0.646 | 0.67 |
| ctr_eb_mysql_mem_mb | 6149.861 | 6153.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 9.688 | 9.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.147 | 9.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 10.2 | 10.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.516 | 10.7 |
| ctr_eb_postgres_cpu_pct | 0.328 | 0.61 |
| ctr_eb_postgres_mem_mb | 2202.078 | 2203 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 56.795 | 67.49 |
| in_flight | 514.902 | 1884 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.396 | 2.72 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1178859.216 | 1967580 |
| octopus_missing_reports_total | 3130.627 | 5160 |
| octopus_mqtt_received_total | 190173.235 | 328325 |
| octopus_mqtt_sent_total | 349574.059 | 592137 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15367.396 | 17254.4 |
| produced_total | 594057.843 | 964222 |
| publish_mb_per_s | 49.996 | 79.74 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,869 records are Completed |
| record_counts | pass | 138,869 records found, 138,869 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,481 events applied by workers, 1,000,481 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,803 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 58 ms, max 1056 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12934 seq 23: ready 324 ms before start, 324 ms of it with an idle slot in group 409
- [cross_source_blocking] source 7303 seq 4: ready 341 ms before start, 341 ms of it with an idle slot in group 250
- [cross_source_blocking] source 9343 seq 5: ready 346 ms before start, 346 ms of it with an idle slot in group 324
- [cross_source_blocking] source 10887 seq 7: ready 342 ms before start, 342 ms of it with an idle slot in group 354
- [cross_source_blocking] source 12007 seq 19: ready 342 ms before start, 342 ms of it with an idle slot in group 383
- [cross_source_blocking] source 9087 seq 31: ready 348 ms before start, 348 ms of it with an idle slot in group 314
- [cross_source_blocking] source 1951 seq 21: ready 346 ms before start, 346 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1575 seq 11: ready 346 ms before start, 346 ms of it with an idle slot in group 47
- [cross_source_blocking] source 14967 seq 15: ready 347 ms before start, 347 ms of it with an idle slot in group 469
- [cross_source_blocking] source 3327 seq 11: ready 346 ms before start, 346 ms of it with an idle slot in group 113
- [cross_source_blocking] source 3215 seq 12: ready 346 ms before start, 346 ms of it with an idle slot in group 107
- [cross_source_blocking] source 10735 seq 29: ready 351 ms before start, 351 ms of it with an idle slot in group 352
- [cross_source_blocking] source 4335 seq 40: ready 346 ms before start, 346 ms of it with an idle slot in group 147
- [cross_source_blocking] source 9295 seq 20: ready 346 ms before start, 346 ms of it with an idle slot in group 320
- [cross_source_blocking] source 3691 seq 6: ready 347 ms before start, 347 ms of it with an idle slot in group 126
- [cross_source_blocking] source 2775 seq 4: ready 347 ms before start, 347 ms of it with an idle slot in group 85
- [cross_source_blocking] source 12299 seq 7: ready 351 ms before start, 351 ms of it with an idle slot in group 391
- [cross_source_blocking] source 4127 seq 11: ready 167 ms before start, 167 ms of it with an idle slot in group 136
- [cross_source_blocking] source 5731 seq 22: ready 173 ms before start, 173 ms of it with an idle slot in group 197
- [cross_source_blocking] source 191 seq 20: ready 173 ms before start, 173 ms of it with an idle slot in group 4

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2031.615 | 4980.735 | 5668.863 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2056.191 | 4259.839 | 4816.895 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2129.919 | 4161.535 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1916.927 | 3702.783 | 4292.607 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 15.871 | 2637.823 | 4194.303 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 15.295 | 2392.063 | 4489.215 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 15.935 | 2359.295 | 2932.735 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 16.511 | 2342.911 | 4456.447 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 15.615 | 2260.991 | 2736.127 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 942 | 942 | 0 | 15.615 | 2260.991 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5541 | 5541 | 0 | 16.063 | 2244.607 | 3473.407 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 16.063 | 2244.607 | 2949.119 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 16.191 | 2244.607 | 3112.959 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 15.551 | 2244.607 | 2965.503 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 15.935 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 15.999 | 2195.455 | 2883.583 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 16.255 | 2146.303 | 2752.511 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 14.847 | 2146.303 | 2932.735 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 15.423 | 2146.303 | 2867.199 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 15.423 | 2129.919 | 3162.111 |
| t-492 | 6 | 1 | 32.276 | 2.941 | 3046 | 0 | 197 | 197 | 0 | 16.063 | 2113.535 | 2441.215 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 15.743 | 2097.151 | 2752.511 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 15.551 | 2080.767 | 2572.287 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 15.487 | 2064.383 | 2981.887 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 14.783 | 2047.999 | 2719.743 |
