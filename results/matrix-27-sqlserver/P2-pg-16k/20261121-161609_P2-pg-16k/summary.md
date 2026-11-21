# P2-pg-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 0.572s, drain 2.752s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000538 | 1000538 | 15944.537 | 15946.192 | 38409.4 | 0 | 0 | 0 | 458 | 5163 | 1132 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797175 | 35.716 | 7.015 | 15.319 | 1289.215 | 2727.935 | 4919.295 |
| e2e_from_send | 797175 | 34.556 | 6.203 | 13.711 | 1288.191 | 2727.935 | 4919.295 |
| producer_lag | 797175 | 1.16 | 0.79 | 2.717 | 5.911 | 18.031 | 126.079 |
| publish_call | 1000538 | 7.104 | 5.763 | 12.303 | 41.887 | 92.927 | 151.167 |
| queue_wait | 797175 | 23.816 | 6.175 | 13.255 | 636.415 | 2271.231 | 4673.535 |
| broker_dwell | 797175 | 17.928 | 0.463 | 4.219 | 630.783 | 2265.087 | 4665.343 |
| receive_call | 954911 | 36.279 | 14.543 | 92.095 | 325.119 | 482.559 | 547.327 |
| ctx_load | 797175 | 0.002 | 0.002 | 0.003 | 0.01 | 0.034 | 7.135 |
| process | 797175 | 10.724 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797175 | 0.002 | 0.001 | 0.001 | 0.004 | 0.021 | 24.959 |
| ack | 794515 | 0.003 | 0.002 | 0.004 | 0.014 | 0.046 | 9.255 |
| service | 797175 | 10.74 | 0.013 | 0.025 | 0.077 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2272.112 | 2254.847 | 3039.231 | 4341.759 | 4763.647 | 4763.647 |
| ready_wait | 797175 | 1.955 | 0.623 | 1.926 | 33.215 | 147.711 | 1885.183 |
| blocked | 797175 | 1.882 | 0.623 | 1.926 | 32.863 | 139.519 | 236.031 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15946.192 | 16274.4 |
| applied_total | 578890.388 | 961893 |
| bench_cpu_cores | 4.703 | 5.173 |
| bench_cpu_pct | 19.597 | 21.55 |
| bench_gc_gen0 | 2.816 | 6 |
| bench_gc_gen1 | 1.204 | 2 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 925.041 | 1168.3 |
| bench_mem_mb | 1473.198 | 1585.1 |
| bench_threadpool_queue | 3.429 | 20 |
| bench_threadpool_threads | 29.755 | 34 |
| broker_backlog | 1815.265 | 4113 |
| ctr_eb_mosquitto_cpu_pct | 52.244 | 62.18 |
| ctr_eb_mosquitto_mem_mb | 7.635 | 15.6 |
| ctr_eb_mssql_cpu_pct | 17.732 | 89.5 |
| ctr_eb_mssql_mem_mb | 7570.361 | 7573.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 203.142 | 387.28 |
| ctr_eb_postgres_mem_mb | 3029.024 | 3149 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 34.801 | 47.02 |
| in_flight | 441.204 | 551 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.345 | 1.53 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146068.694 | 1954075 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 231012.347 | 395630 |
| octopus_mqtt_sent_total | 365794.776 | 633834 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15944.537 | 16252.4 |
| produced_total | 579331.592 | 962247 |
| publish_mb_per_s | 51.961 | 74.41 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,538 events applied by workers, 1,000,538 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,132 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 33 ms, max 236 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2789 seq 13: ready 125 ms before start, 125 ms of it with an idle slot in group 86
- [cross_source_blocking] source 6117 seq 6: ready 121 ms before start, 121 ms of it with an idle slot in group 210
- [cross_source_blocking] source 10177 seq 19: ready 125 ms before start, 125 ms of it with an idle slot in group 344
- [cross_source_blocking] source 369 seq 12: ready 125 ms before start, 125 ms of it with an idle slot in group 11
- [cross_source_blocking] source 7781 seq 14: ready 125 ms before start, 125 ms of it with an idle slot in group 263
- [cross_source_blocking] source 1297 seq 57: ready 121 ms before start, 121 ms of it with an idle slot in group 41
- [cross_source_blocking] source 15121 seq 3: ready 125 ms before start, 125 ms of it with an idle slot in group 472
- [cross_source_blocking] source 8337 seq 5: ready 124 ms before start, 124 ms of it with an idle slot in group 283
- [cross_source_blocking] source 2245 seq 26: ready 120 ms before start, 120 ms of it with an idle slot in group 65
- [cross_source_blocking] source 12565 seq 2: ready 123 ms before start, 123 ms of it with an idle slot in group 397
- [cross_source_blocking] source 1493 seq 14: ready 123 ms before start, 123 ms of it with an idle slot in group 45
- [cross_source_blocking] source 1925 seq 19: ready 125 ms before start, 125 ms of it with an idle slot in group 55
- [cross_source_blocking] source 5621 seq 24: ready 124 ms before start, 124 ms of it with an idle slot in group 194
- [cross_source_blocking] source 325 seq 5: ready 120 ms before start, 120 ms of it with an idle slot in group 9
- [cross_source_blocking] source 3093 seq 16: ready 124 ms before start, 124 ms of it with an idle slot in group 103
- [cross_source_blocking] source 225 seq 19: ready 125 ms before start, 125 ms of it with an idle slot in group 5
- [cross_source_blocking] source 1108 seq 9: ready 109 ms before start, 109 ms of it with an idle slot in group 35
- [cross_source_blocking] source 6068 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 207
- [cross_source_blocking] source 14016 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 446
- [cross_source_blocking] source 6088 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 208

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1851.391 | 4489.215 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1884.159 | 4128.767 | 4685.823 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1785.855 | 3522.559 | 4079.615 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1499.135 | 3031.039 | 4095.999 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.167 | 2588.671 | 4161.535 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.199 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.007 | 2342.911 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.039 | 2260.991 | 2719.743 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.071 | 2260.991 | 2899.967 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.135 | 2244.607 | 3080.191 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.103 | 2244.607 | 2965.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.103 | 2228.223 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.231 | 2211.839 | 3457.023 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.103 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.103 | 2162.687 | 2883.583 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 6.719 | 2129.919 | 2752.511 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.007 | 2129.919 | 2932.735 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.263 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.071 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 6.911 | 2113.535 | 2736.127 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.007 | 2064.383 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.007 | 2056.191 | 2965.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 7.039 | 2056.191 | 2555.903 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 7.455 | 2056.191 | 2899.967 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 6.879 | 2023.423 | 2719.743 |
