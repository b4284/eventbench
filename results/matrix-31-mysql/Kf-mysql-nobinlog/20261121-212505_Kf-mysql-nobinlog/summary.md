# Kf-mysql-nobinlog — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 1.03s, drain 2.351s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000502 | 1000502 | 16333.688 | 16335.649 | 33517.6 | 0 | 0 | 0 | 458 | 5162 | 11210 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797135 | 121.627 | 12.719 | 258.303 | 1688.575 | 2891.775 | 7208.959 |
| e2e_from_send | 797135 | 62.242 | 10.919 | 108.351 | 1342.463 | 2785.279 | 7204.863 |
| producer_lag | 797135 | 59.384 | 1.231 | 76.799 | 1268.735 | 1696.767 | 2273.279 |
| publish_call | 1000502 | 43.643 | 12.231 | 120.639 | 323.839 | 457.727 | 520.447 |
| queue_wait | 797135 | 51.494 | 10.831 | 104.895 | 736.767 | 2328.575 | 5185.535 |
| broker_dwell | 797135 | 25.538 | 1.961 | 22.799 | 707.071 | 2308.095 | 5140.479 |
| receive_call | 892294 | 39.381 | 13.487 | 105.471 | 344.575 | 473.087 | 516.607 |
| ctx_load | 797135 | 0.002 | 0.002 | 0.003 | 0.011 | 0.045 | 16.399 |
| process | 797135 | 10.727 | 0.001 | 0.002 | 0.014 | 2617.343 | 3000.319 |
| ctx_commit | 797135 | 0.003 | 0.001 | 0.001 | 0.004 | 0.025 | 27.999 |
| ack | 787190 | 0.003 | 0.002 | 0.004 | 0.015 | 0.06 | 20.879 |
| service | 797135 | 10.749 | 0.013 | 0.03 | 0.112 | 2617.343 | 3000.319 |
| e2e_large | 356 | 2837.651 | 2764.799 | 3997.695 | 6143.999 | 7208.959 | 7208.959 |
| ready_wait | 797135 | 7.793 | 1.023 | 13.687 | 136.191 | 323.327 | 1754.111 |
| blocked | 797135 | 7.697 | 1.023 | 13.671 | 135.551 | 322.815 | 1489.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16335.649 | 22320.8 |
| applied_total | 577750.449 | 961634 |
| bench_cpu_cores | 4.39 | 5.006 |
| bench_cpu_pct | 18.29 | 20.86 |
| bench_gc_gen0 | 2.816 | 6 |
| bench_gc_gen1 | 1.204 | 3 |
| bench_gc_gen2 | 0.694 | 2 |
| bench_gc_heap_mb | 1006.706 | 1375.9 |
| bench_mem_mb | 1649.402 | 1857.7 |
| bench_threadpool_queue | 11.388 | 400 |
| bench_threadpool_threads | 42.959 | 44 |
| broker_backlog | 2814.571 | 7873 |
| ctr_eb_mosquitto_cpu_pct | 52.624 | 57.91 |
| ctr_eb_mosquitto_mem_mb | 9.567 | 14.8 |
| ctr_eb_mssql_cpu_pct | 2.426 | 5.66 |
| ctr_eb_mssql_mem_mb | 6994.32 | 6997.1 |
| ctr_eb_mysql_cpu_pct | 456.031 | 533.71 |
| ctr_eb_mysql_mem_mb | 8710.641 | 9425.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 15.1 | 15.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.3 | 16.3 |
| ctr_eb_postgres_cpu_pct | 0.675 | 1.97 |
| ctr_eb_postgres_mem_mb | 4302.759 | 4312.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.161 | 55.21 |
| in_flight | 628.408 | 1785 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.515 | 1.73 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1140368.408 | 1913284 |
| octopus_missing_reports_total | 398.837 | 977 |
| octopus_mqtt_received_total | 174359.306 | 314828 |
| octopus_mqtt_sent_total | 303053.755 | 537143 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16333.688 | 22519.3 |
| produced_total | 578378.857 | 961999 |
| publish_mb_per_s | 53.471 | 92.59 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,502 events applied by workers, 1,000,502 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 11,210 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 136 ms, max 1489 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5611 seq 26: ready 103 ms before start, 103 ms of it with an idle slot in group 194
- [cross_source_blocking] source 2859 seq 12: ready 102 ms before start, 102 ms of it with an idle slot in group 91
- [cross_source_blocking] source 687 seq 17: ready 102 ms before start, 102 ms of it with an idle slot in group 20
- [cross_source_blocking] source 3171 seq 16: ready 104 ms before start, 104 ms of it with an idle slot in group 105
- [cross_source_blocking] source 5263 seq 17: ready 105 ms before start, 105 ms of it with an idle slot in group 182
- [cross_source_blocking] source 3803 seq 30: ready 104 ms before start, 104 ms of it with an idle slot in group 129
- [cross_source_blocking] source 9563 seq 18: ready 102 ms before start, 102 ms of it with an idle slot in group 332
- [cross_source_blocking] source 8131 seq 19: ready 105 ms before start, 105 ms of it with an idle slot in group 275
- [cross_source_blocking] source 1091 seq 13: ready 103 ms before start, 103 ms of it with an idle slot in group 35
- [cross_source_blocking] source 12747 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 402
- [cross_source_blocking] source 11587 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 376
- [cross_source_blocking] source 11279 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 367
- [cross_source_blocking] source 14107 seq 22: ready 104 ms before start, 104 ms of it with an idle slot in group 450
- [cross_source_blocking] source 1835 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 52
- [cross_source_blocking] source 14699 seq 17: ready 104 ms before start, 104 ms of it with an idle slot in group 464
- [cross_source_blocking] source 15567 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 483
- [cross_source_blocking] source 9562 seq 14: ready 101 ms before start, 101 ms of it with an idle slot in group 332
- [cross_source_blocking] source 2698 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 83
- [cross_source_blocking] source 10474 seq 13: ready 101 ms before start, 101 ms of it with an idle slot in group 347
- [cross_source_blocking] source 4458 seq 7: ready 101 ms before start, 101 ms of it with an idle slot in group 153

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2670.591 | 6160.383 | 7208.959 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2007.039 | 5308.415 | 6389.759 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2015.231 | 3866.623 | 4112.383 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1589.247 | 3096.575 | 4112.383 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 12.799 | 2637.823 | 4194.303 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 13.311 | 2605.055 | 3620.863 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 12.351 | 2605.055 | 4177.919 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 13.631 | 2555.903 | 3063.807 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 12.991 | 2506.751 | 2916.351 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 911 | 911 | 0 | 12.735 | 2457.599 | 3162.111 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 12.223 | 2441.215 | 4358.143 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 13.887 | 2375.679 | 4456.447 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 11.711 | 2359.295 | 3571.711 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 13.695 | 2277.375 | 3751.935 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 13.503 | 2277.375 | 3080.191 |
| t-104 | 46 | 1 | 39.533 | 30.319 | 2163 | 0 | 1915 | 1915 | 0 | 12.735 | 2260.991 | 3883.007 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 12.991 | 2260.991 | 2719.743 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 881 | 881 | 0 | 12.991 | 2244.607 | 3309.567 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 12.863 | 2244.607 | 2965.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 13.567 | 2228.223 | 2949.119 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 12.543 | 2211.839 | 2932.735 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 14.143 | 2179.071 | 3850.239 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 12.287 | 2179.071 | 2736.127 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 12.863 | 2179.071 | 2932.735 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1027 | 1027 | 0 | 11.967 | 2146.303 | 3162.111 |
