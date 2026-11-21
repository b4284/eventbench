# Ma-mssql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.4s, closing 2.355s, drain 14.859s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000528 | 1000528 | 15942.167 | 15945.267 | 23019.3 | 0 | 0 | 0 | 458 | 5162 | 6614 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797163 | 76.914 | 19.567 | 94.527 | 1347.583 | 2795.519 | 5357.567 |
| e2e_from_send | 797163 | 58.131 | 17.535 | 65.791 | 1328.127 | 2772.991 | 5357.567 |
| producer_lag | 797163 | 18.783 | 1.301 | 9.359 | 490.495 | 687.103 | 1538.047 |
| publish_call | 1000528 | 38.88 | 17.055 | 82.879 | 349.951 | 743.423 | 1554.431 |
| queue_wait | 797163 | 47.373 | 17.455 | 63.199 | 740.351 | 2310.143 | 4689.919 |
| broker_dwell | 797163 | 23.822 | 2.729 | 19.215 | 664.575 | 2289.663 | 4677.631 |
| receive_call | 892835 | 39.79 | 17.359 | 100.223 | 326.911 | 472.063 | 542.719 |
| ctx_load | 797163 | 0.003 | 0.002 | 0.004 | 0.013 | 0.065 | 37.535 |
| process | 797163 | 10.731 | 0.001 | 0.003 | 0.018 | 2617.343 | 3008.511 |
| ctx_commit | 797163 | 0.002 | 0.001 | 0.001 | 0.004 | 0.03 | 27.071 |
| ack | 788541 | 0.004 | 0.002 | 0.005 | 0.018 | 0.087 | 41.983 |
| service | 797163 | 10.759 | 0.013 | 0.033 | 0.163 | 2617.343 | 3008.511 |
| e2e_large | 356 | 2639.307 | 2643.967 | 3469.311 | 4497.407 | 5357.567 | 5357.567 |
| ready_wait | 797163 | 7.105 | 1.656 | 13.663 | 91.647 | 326.399 | 1874.943 |
| blocked | 797163 | 7.025 | 1.656 | 13.663 | 91.327 | 308.991 | 753.151 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15945.267 | 19635.7 |
| applied_total | 577969.898 | 961557 |
| bench_cpu_cores | 4.246 | 4.838 |
| bench_cpu_pct | 17.694 | 20.16 |
| bench_gc_gen0 | 2.735 | 4 |
| bench_gc_gen1 | 1.224 | 2 |
| bench_gc_gen2 | 0.653 | 1 |
| bench_gc_heap_mb | 1007.341 | 1372.2 |
| bench_mem_mb | 1756.765 | 1911.9 |
| bench_threadpool_queue | 27.714 | 220 |
| bench_threadpool_threads | 41.551 | 44 |
| broker_backlog | 23235.327 | 78851 |
| ctr_eb_mosquitto_cpu_pct | 59.007 | 65.01 |
| ctr_eb_mosquitto_mem_mb | 6.68 | 9.1 |
| ctr_eb_mssql_cpu_pct | 862.133 | 992.58 |
| ctr_eb_mssql_mem_mb | 7614.957 | 7641.8 |
| ctr_eb_mysql_cpu_pct | 0.762 | 0.79 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 14.7 | 14.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.733 | 14.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 15.043 | 15.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.194 | 16.4 |
| ctr_eb_postgres_cpu_pct | 5.552 | 27.07 |
| ctr_eb_postgres_mem_mb | 3276.276 | 3463.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 67.804 | 76.84 |
| in_flight | 646.878 | 2222 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.419 | 2.68 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1082022 | 1877272 |
| octopus_missing_reports_total | 2920.204 | 3450 |
| octopus_mqtt_received_total | 167797.939 | 302512 |
| octopus_mqtt_sent_total | 300410.571 | 541920 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.167 | 20082.3 |
| produced_total | 578616.776 | 961953 |
| publish_mb_per_s | 51.768 | 79.99 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,528 events applied by workers, 1,000,528 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 6,614 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 91 ms, max 753 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13172 seq 22: ready 194 ms before start, 194 ms of it with an idle slot in group 418
- [cross_source_blocking] source 8041 seq 19: ready 201 ms before start, 201 ms of it with an idle slot in group 271
- [cross_source_blocking] source 8074 seq 25: ready 242 ms before start, 242 ms of it with an idle slot in group 271
- [cross_source_blocking] source 13175 seq 34: ready 206 ms before start, 206 ms of it with an idle slot in group 418
- [cross_source_blocking] source 12554 seq 2: ready 201 ms before start, 201 ms of it with an idle slot in group 397
- [cross_source_blocking] source 13177 seq 29: ready 201 ms before start, 201 ms of it with an idle slot in group 418
- [cross_source_blocking] source 14224 seq 21: ready 201 ms before start, 201 ms of it with an idle slot in group 456
- [cross_source_blocking] source 12644 seq 6: ready 236 ms before start, 236 ms of it with an idle slot in group 397
- [cross_source_blocking] source 1107 seq 15: ready 207 ms before start, 207 ms of it with an idle slot in group 35
- [cross_source_blocking] source 14223 seq 29: ready 207 ms before start, 207 ms of it with an idle slot in group 456
- [cross_source_blocking] source 5634 seq 22: ready 209 ms before start, 209 ms of it with an idle slot in group 194
- [cross_source_blocking] source 14234 seq 21: ready 242 ms before start, 242 ms of it with an idle slot in group 456
- [cross_source_blocking] source 9923 seq 6: ready 243 ms before start, 243 ms of it with an idle slot in group 340
- [cross_source_blocking] source 5600 seq 19: ready 242 ms before start, 242 ms of it with an idle slot in group 194
- [cross_source_blocking] source 5642 seq 28: ready 242 ms before start, 242 ms of it with an idle slot in group 194
- [cross_source_blocking] source 1095 seq 9: ready 207 ms before start, 207 ms of it with an idle slot in group 35
- [cross_source_blocking] source 4902 seq 10: ready 206 ms before start, 206 ms of it with an idle slot in group 169
- [cross_source_blocking] source 1096 seq 12: ready 211 ms before start, 211 ms of it with an idle slot in group 35
- [cross_source_blocking] source 1092 seq 13: ready 226 ms before start, 226 ms of it with an idle slot in group 35
- [cross_source_blocking] source 9456 seq 34: ready 129 ms before start, 129 ms of it with an idle slot in group 326

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2162.687 | 5079.039 | 5373.951 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2342.911 | 4423.679 | 5275.647 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2056.191 | 3948.543 | 4423.679 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1810.431 | 3719.167 | 4554.751 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 20.095 | 2670.591 | 4177.919 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 20.863 | 2506.751 | 3194.879 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 20.863 | 2375.679 | 4554.751 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 20.991 | 2293.759 | 3489.791 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 19.455 | 2277.375 | 3145.727 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 19.199 | 2260.991 | 2736.127 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 20.735 | 2260.991 | 2916.351 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 20.223 | 2260.991 | 3129.343 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 19.967 | 2260.991 | 2981.887 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1027 | 1027 | 0 | 19.327 | 2228.223 | 3227.647 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 19.839 | 2228.223 | 2949.119 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 19.583 | 2195.455 | 2883.583 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 408 | 408 | 0 | 19.071 | 2162.687 | 2654.207 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 20.863 | 2162.687 | 2867.199 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 469 | 469 | 0 | 18.815 | 2146.303 | 3473.407 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 19.199 | 2146.303 | 2932.735 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 19.455 | 2129.919 | 2752.511 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 19.711 | 2129.919 | 3145.727 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 19.199 | 2113.535 | 2752.511 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 20.223 | 2113.535 | 3522.559 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 19.711 | 2088.959 | 2719.743 |
