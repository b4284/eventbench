# Jb-json — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.248s, closing 1.955s, drain 1.004s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000520 | 1000520 | 15936.722 | 15939.8 | 23208.1 | 0 | 0 | 0 | 458 | 5162 | 2339 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797154 | 60.561 | 17.311 | 68.415 | 1321.983 | 2764.799 | 4927.487 |
| e2e_from_send | 797154 | 52.798 | 15.767 | 56.991 | 1314.815 | 2754.559 | 4927.487 |
| producer_lag | 797154 | 7.763 | 1.053 | 4.931 | 208.383 | 321.791 | 633.343 |
| publish_call | 1000520 | 30.46 | 15.831 | 61.791 | 230.271 | 558.591 | 1359.871 |
| queue_wait | 797154 | 42.046 | 15.695 | 54.495 | 665.087 | 2289.663 | 4685.823 |
| broker_dwell | 797154 | 19.929 | 1.734 | 7.759 | 639.487 | 2271.231 | 4681.727 |
| receive_call | 904348 | 39.289 | 17.023 | 99.327 | 323.071 | 471.551 | 513.791 |
| ctx_load | 797154 | 0.002 | 0.002 | 0.003 | 0.01 | 0.042 | 31.727 |
| process | 797154 | 10.728 | 0.001 | 0.002 | 0.014 | 2619.391 | 3002.367 |
| ctx_commit | 797154 | 0.002 | 0.001 | 0.001 | 0.004 | 0.025 | 31.711 |
| ack | 795851 | 0.005 | 0.003 | 0.008 | 0.022 | 0.075 | 32.399 |
| service | 797154 | 10.752 | 0.014 | 0.035 | 0.116 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2432.726 | 2418.687 | 3207.167 | 4485.119 | 4837.375 | 4837.375 |
| ready_wait | 797154 | 5.173 | 0.725 | 13.983 | 68.607 | 208.127 | 1869.823 |
| blocked | 797154 | 5.097 | 0.725 | 13.975 | 68.223 | 203.391 | 549.375 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15939.8 | 20439.1 |
| applied_total | 578196.224 | 961191 |
| bench_cpu_cores | 4.978 | 5.74 |
| bench_cpu_pct | 20.743 | 23.92 |
| bench_gc_gen0 | 2.857 | 5 |
| bench_gc_gen1 | 1.245 | 3 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 1036.639 | 1345.4 |
| bench_mem_mb | 1737.627 | 1883.3 |
| bench_threadpool_queue | 17.816 | 121 |
| bench_threadpool_threads | 41.694 | 44 |
| broker_backlog | 2777.714 | 5919 |
| ctr_eb_mssql_cpu_pct | 560.806 | 619.58 |
| ctr_eb_mssql_mem_mb | 6334.408 | 6352.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.201 | 0.33 |
| ctr_eb_postgres_mem_mb | 2498.886 | 2499.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 53.194 | 61.56 |
| in_flight | 512.837 | 1311 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.272 | 2.74 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1148872.592 | 1925045 |
| octopus_missing_reports_total | 4213.898 | 7480 |
| octopus_mqtt_received_total | 178234.776 | 302567 |
| octopus_mqtt_sent_total | 333503.102 | 563623 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15936.722 | 20496.2 |
| produced_total | 578709.061 | 961549 |
| publish_mb_per_s | 51.759 | 75.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,520 events applied by workers, 1,000,520 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 2,339 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 68 ms, max 549 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6819 seq 4: ready 184 ms before start, 184 ms of it with an idle slot in group 233
- [cross_source_blocking] source 7919 seq 23: ready 182 ms before start, 182 ms of it with an idle slot in group 267
- [cross_source_blocking] source 7595 seq 15: ready 183 ms before start, 183 ms of it with an idle slot in group 261
- [cross_source_blocking] source 8407 seq 11: ready 279 ms before start, 279 ms of it with an idle slot in group 285
- [cross_source_blocking] source 4851 seq 11: ready 106 ms before start, 106 ms of it with an idle slot in group 168
- [cross_source_blocking] source 14671 seq 11: ready 156 ms before start, 156 ms of it with an idle slot in group 464
- [cross_source_blocking] source 3561 seq 29: ready 189 ms before start, 189 ms of it with an idle slot in group 121
- [cross_source_blocking] source 7316 seq 6: ready 189 ms before start, 189 ms of it with an idle slot in group 251
- [cross_source_blocking] source 1489 seq 23: ready 188 ms before start, 188 ms of it with an idle slot in group 45
- [cross_source_blocking] source 5220 seq 25: ready 186 ms before start, 186 ms of it with an idle slot in group 181
- [cross_source_blocking] source 6677 seq 19: ready 189 ms before start, 189 ms of it with an idle slot in group 227
- [cross_source_blocking] source 3313 seq 16: ready 188 ms before start, 188 ms of it with an idle slot in group 111
- [cross_source_blocking] source 13265 seq 22: ready 186 ms before start, 186 ms of it with an idle slot in group 425
- [cross_source_blocking] source 2225 seq 41: ready 189 ms before start, 189 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8995 seq 9: ready 189 ms before start, 189 ms of it with an idle slot in group 309
- [cross_source_blocking] source 6955 seq 6: ready 184 ms before start, 184 ms of it with an idle slot in group 236
- [cross_source_blocking] source 5327 seq 22: ready 184 ms before start, 184 ms of it with an idle slot in group 182
- [cross_source_blocking] source 4623 seq 14: ready 184 ms before start, 184 ms of it with an idle slot in group 161
- [cross_source_blocking] source 5199 seq 20: ready 189 ms before start, 189 ms of it with an idle slot in group 180
- [cross_source_blocking] source 14203 seq 29: ready 190 ms before start, 190 ms of it with an idle slot in group 454

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1982.463 | 4653.055 | 4849.663 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2072.575 | 4423.679 | 4816.895 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1949.695 | 3604.479 | 4227.071 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1638.399 | 3309.567 | 4390.911 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 17.407 | 2637.823 | 4177.919 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 17.791 | 2359.295 | 2949.119 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 18.559 | 2342.911 | 4456.447 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 17.407 | 2326.527 | 3227.647 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 795 | 795 | 0 | 16.895 | 2293.759 | 3014.655 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 16.895 | 2260.991 | 2916.351 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 17.663 | 2260.991 | 3112.959 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 17.407 | 2260.991 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 18.175 | 2244.607 | 3473.407 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 18.687 | 2244.607 | 2965.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 18.175 | 2228.223 | 2932.735 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 17.663 | 2179.071 | 2998.271 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 16.767 | 2179.071 | 2883.583 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 411 | 411 | 0 | 18.047 | 2162.687 | 3194.879 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 18.175 | 2162.687 | 2883.583 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 17.407 | 2146.303 | 3162.111 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 17.535 | 2146.303 | 2752.511 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 16.895 | 2113.535 | 2949.119 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 16.639 | 2097.151 | 2752.511 |
| t-492 | 6 | 1 | 32.276 | 2.941 | 3046 | 0 | 197 | 197 | 0 | 15.999 | 2080.767 | 2457.599 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 17.279 | 2064.383 | 3751.935 |
