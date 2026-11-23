# Aa-ag-mem — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.256s, closing 2.463s, drain 0.811s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 986740 | 986740 | 16645.32 | 16643.892 | 22635.7 | 0 | 0 | 0 | 454 | 5089 | 42297 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 783460 | 2088.241 | 2256.895 | 3606.527 | 4698.111 | 5804.031 | 10059.775 |
| e2e_from_send | 783460 | 139.488 | 100.543 | 201.215 | 1456.127 | 2871.295 | 8118.271 |
| producer_lag | 783460 | 1948.754 | 2119.679 | 3448.831 | 4317.183 | 5287.935 | 5795.839 |
| publish_call | 986740 | 107.482 | 94.143 | 178.303 | 389.631 | 1049.599 | 1804.287 |
| queue_wait | 783460 | 128.71 | 100.223 | 197.503 | 861.183 | 2435.071 | 6090.751 |
| broker_dwell | 783460 | 33.109 | 7.631 | 37.887 | 761.855 | 2344.959 | 5918.719 |
| receive_call | 754719 | 48.181 | 23.903 | 120.895 | 335.615 | 473.855 | 542.207 |
| ctx_load | 783460 | 0.003 | 0.002 | 0.003 | 0.011 | 0.079 | 61.183 |
| process | 783460 | 10.737 | 0.001 | 0.002 | 0.017 | 2619.391 | 3033.087 |
| ctx_commit | 783460 | 0.003 | 0.001 | 0.001 | 0.004 | 0.035 | 55.327 |
| ack | 779029 | 0.006 | 0.003 | 0.006 | 0.025 | 0.163 | 65.983 |
| service | 783460 | 10.778 | 0.016 | 0.049 | 0.452 | 2619.391 | 3033.087 |
| e2e_large | 352 | 4984.963 | 5087.231 | 6823.935 | 8962.047 | 10059.775 | 10059.775 |
| ready_wait | 783460 | 18.537 | 3.581 | 59.135 | 192.895 | 395.007 | 1804.287 |
| blocked | 783460 | 18.465 | 3.581 | 59.007 | 192.383 | 388.607 | 1804.287 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16643.892 | 22635.7 |
| applied_total | 545138.163 | 946900 |
| bench_cpu_cores | 2.993 | 4.158 |
| bench_cpu_pct | 12.473 | 17.32 |
| bench_gc_gen0 | 2.837 | 5 |
| bench_gc_gen1 | 1.327 | 3 |
| bench_gc_gen2 | 0.898 | 2 |
| bench_gc_heap_mb | 1084.567 | 1380.1 |
| bench_mem_mb | 1799.569 | 2004.6 |
| bench_threadpool_queue | 47.429 | 782 |
| bench_threadpool_threads | 43.776 | 44 |
| broker_backlog | 5421.367 | 7724 |
| ctr_eb_ag1_cpu_pct | 714.334 | 897.89 |
| ctr_eb_ag1_mem_mb | 3935.892 | 4182.8 |
| ctr_eb_ag2_cpu_pct | 163.88 | 211.13 |
| ctr_eb_ag2_mem_mb | 2416.978 | 2593 |
| ctr_eb_ag_haproxy_cpu_pct | 25.199 | 29.5 |
| ctr_eb_ag_haproxy_mem_mb | 19.984 | 23.2 |
| ctr_eb_mysql_cpu_pct | 0.718 | 0.79 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.492 | 0.97 |
| ctr_eb_postgres_mem_mb | 2323.788 | 2324.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 62.965 | 75.08 |
| in_flight | 567.857 | 1455 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.08 | 2.56 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1086601.102 | 1872703 |
| octopus_missing_reports_total | 10136.469 | 18871 |
| octopus_mqtt_received_total | 96498.735 | 167718 |
| octopus_mqtt_sent_total | 269539.143 | 464237 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16645.32 | 22728.7 |
| produced_total | 545706.02 | 947364 |
| publish_mb_per_s | 55.066 | 73.58 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,274 records are Completed |
| record_counts | pass | 137,274 records found, 137,274 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 454 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 986,740 events applied by workers, 986,740 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 42,297 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 192 ms, max 1804 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 135 seq 16: ready 123 ms before start, 123 ms of it with an idle slot in group 2
- [cross_source_blocking] source 10471 seq 23: ready 126 ms before start, 126 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3207 seq 13: ready 123 ms before start, 123 ms of it with an idle slot in group 107
- [cross_source_blocking] source 7783 seq 20: ready 126 ms before start, 126 ms of it with an idle slot in group 263
- [cross_source_blocking] source 3271 seq 17: ready 127 ms before start, 127 ms of it with an idle slot in group 109
- [cross_source_blocking] source 1991 seq 8: ready 127 ms before start, 127 ms of it with an idle slot in group 58
- [cross_source_blocking] source 13607 seq 9: ready 127 ms before start, 127 ms of it with an idle slot in group 431
- [cross_source_blocking] source 10695 seq 11: ready 126 ms before start, 126 ms of it with an idle slot in group 350
- [cross_source_blocking] source 7559 seq 18: ready 128 ms before start, 128 ms of it with an idle slot in group 260
- [cross_source_blocking] source 10055 seq 8: ready 128 ms before start, 128 ms of it with an idle slot in group 343
- [cross_source_blocking] source 1575 seq 11: ready 123 ms before start, 123 ms of it with an idle slot in group 47
- [cross_source_blocking] source 5639 seq 13: ready 122 ms before start, 122 ms of it with an idle slot in group 194
- [cross_source_blocking] source 5703 seq 24: ready 123 ms before start, 123 ms of it with an idle slot in group 197
- [cross_source_blocking] source 14759 seq 11: ready 128 ms before start, 128 ms of it with an idle slot in group 464
- [cross_source_blocking] source 5863 seq 8: ready 125 ms before start, 125 ms of it with an idle slot in group 203
- [cross_source_blocking] source 5607 seq 16: ready 124 ms before start, 124 ms of it with an idle slot in group 194
- [cross_source_blocking] source 4135 seq 12: ready 125 ms before start, 125 ms of it with an idle slot in group 136
- [cross_source_blocking] source 6503 seq 14: ready 125 ms before start, 125 ms of it with an idle slot in group 222
- [cross_source_blocking] source 935 seq 8: ready 124 ms before start, 124 ms of it with an idle slot in group 27
- [cross_source_blocking] source 1095 seq 9: ready 123 ms before start, 123 ms of it with an idle slot in group 35

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 393 | 393 | 0 | 4685.823 | 9306.111 | 10092.543 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 182 | 182 | 0 | 4030.463 | 7503.871 | 8454.143 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 3948.543 | 7110.655 | 8159.231 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 3850.239 | 6193.151 | 6619.135 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 388 | 388 | 0 | 2801.663 | 6029.311 | 7471.103 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 398 | 398 | 0 | 2785.279 | 5865.471 | 6553.599 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 934 | 934 | 0 | 2932.735 | 5832.703 | 6291.455 |
| t-006 | 14 | 1 | 31.139 | 6.948 | 1170 | 0 | 444 | 444 | 0 | 2621.439 | 5570.559 | 6356.991 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 603 | 603 | 0 | 2932.735 | 5537.791 | 5767.167 |
| t-453 | 6 | 1 | 55.729 | 5.489 | 874 | 0 | 351 | 351 | 0 | 2752.511 | 5505.023 | 5668.863 |
| t-433 | 6 | 1 | 26.326 | 2.618 | 184 | 0 | 179 | 179 | 0 | 2834.431 | 5406.719 | 5439.487 |
| t-360 | 14 | 1 | 82.459 | 19.32 | 274 | 0 | 1170 | 1170 | 0 | 2523.135 | 5373.951 | 6619.135 |
| t-142 | 15 | 1 | 33.381 | 8.13 | 413 | 0 | 525 | 525 | 0 | 2523.135 | 5341.183 | 5668.863 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 430 | 430 | 0 | 2654.207 | 5341.183 | 5668.863 |
| t-481 | 12 | 1 | 150.551 | 28.385 | 1993 | 0 | 1788 | 1788 | 0 | 2506.751 | 5341.183 | 7340.031 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1110 | 1110 | 0 | 2211.839 | 5275.647 | 6455.295 |
| t-322 | 27 | 1 | 80.689 | 35.397 | 2198 | 0 | 2191 | 2191 | 0 | 2408.447 | 5275.647 | 6455.295 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1012 | 1012 | 0 | 2375.679 | 5242.879 | 7208.959 |
| t-191 | 11 | 1 | 47.373 | 8.424 | 3529 | 0 | 551 | 551 | 0 | 2015.231 | 5242.879 | 5439.487 |
| t-331 | 12 | 1 | 44.509 | 9.146 | 499 | 0 | 539 | 539 | 0 | 2408.447 | 5242.879 | 5931.007 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1001 | 1001 | 0 | 2424.831 | 5210.111 | 5832.703 |
| t-398 | 15 | 1 | 48.173 | 12.633 | 482 | 0 | 810 | 810 | 0 | 2572.287 | 5210.111 | 6225.919 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5053 | 5053 | 0 | 2375.679 | 5210.111 | 7733.247 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 917 | 917 | 0 | 2211.839 | 5177.343 | 5799.935 |
| t-028 | 27 | 1 | 25.553 | 11.354 | 454 | 0 | 753 | 753 | 0 | 2310.143 | 5177.343 | 6488.063 |
