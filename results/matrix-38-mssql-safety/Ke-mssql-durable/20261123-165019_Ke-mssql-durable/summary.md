# Ke-mssql-durable — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.177s, closing 1.417s, drain 5.366s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 860682 | 860682 | 13059.282 | 13051.894 | 27986.4 | 0 | 0 | 0 | 402 | 4435 | 4423 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 658058 | 6298.849 | 7045.119 | 14893.055 | 16367.615 | 17154.047 | 23199.743 |
| e2e_from_send | 658058 | 123.108 | 29.727 | 103.871 | 1807.359 | 16105.471 | 23199.743 |
| producer_lag | 658058 | 6175.745 | 5758.975 | 14729.215 | 16277.503 | 16457.727 | 16777.215 |
| publish_call | 860682 | 80.089 | 25.263 | 90.239 | 286.463 | 16556.031 | 16859.135 |
| queue_wait | 658058 | 112.309 | 29.231 | 101.631 | 1315.839 | 16097.279 | 21168.127 |
| broker_dwell | 658058 | 26.21 | 2.447 | 13.047 | 946.687 | 2387.967 | 21037.055 |
| receive_call | 740407 | 35.942 | 17.007 | 86.719 | 302.335 | 469.759 | 529.407 |
| ctx_load | 658058 | 0.003 | 0.002 | 0.003 | 0.009 | 0.04 | 31.103 |
| process | 658058 | 10.773 | 0.001 | 0.002 | 0.014 | 2615.295 | 3012.607 |
| ctx_commit | 658058 | 0.002 | 0.001 | 0.001 | 0.004 | 0.024 | 27.503 |
| ack | 656239 | 0.005 | 0.003 | 0.007 | 0.02 | 0.074 | 33.151 |
| service | 658058 | 10.799 | 0.014 | 0.037 | 0.127 | 2615.295 | 3012.607 |
| e2e_large | 300 | 8219.293 | 3600.383 | 17727.487 | 21135.359 | 23199.743 | 23199.743 |
| ready_wait | 658058 | 50.228 | 1.203 | 12.087 | 80.255 | 16097.279 | 17022.975 |
| blocked | 658058 | 50.119 | 1.203 | 12.079 | 79.039 | 16097.279 | 17022.975 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13051.894 | 27986.4 |
| applied_total | 481068.184 | 819844 |
| bench_cpu_cores | 2.981 | 5.426 |
| bench_cpu_pct | 12.423 | 22.61 |
| bench_gc_gen0 | 2.449 | 5 |
| bench_gc_gen1 | 1.122 | 3 |
| bench_gc_gen2 | 0.776 | 2 |
| bench_gc_heap_mb | 981.367 | 1226 |
| bench_mem_mb | 1695.084 | 1860.9 |
| bench_threadpool_queue | 15.102 | 321 |
| bench_threadpool_threads | 39.122 | 44 |
| broker_backlog | 2839.714 | 6268 |
| ctr_eb_mssql_cpu_pct | 537.623 | 647.61 |
| ctr_eb_mssql_mem_mb | 4315.348 | 5026.8 |
| ctr_eb_mysql_cpu_pct | 0.53 | 0.68 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.074 | 0.22 |
| ctr_eb_postgres_mem_mb | 2155.382 | 2156.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 39.492 | 61.9 |
| in_flight | 445.633 | 1666 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.804 | 2.71 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 964718.327 | 1632336 |
| octopus_missing_reports_total | 2422.653 | 5180 |
| octopus_mqtt_received_total | 139009.755 | 196972 |
| octopus_mqtt_sent_total | 262758.98 | 412172 |
| octopus_publish_retries_total | 8580.245 | 18440 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 13059.282 | 27911.1 |
| produced_total | 481513.816 | 820723 |
| publish_mb_per_s | 42.791 | 85.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 122,839 records are Completed |
| record_counts | pass | 122,839 records found, 122,839 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 402 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 860,682 events applied by workers, 860,682 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 4,423 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 79 ms, max 17012 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15328 seq 8: ready 111 ms before start, 111 ms of it with an idle slot in group 479
- [cross_source_blocking] source 2204 seq 16: ready 111 ms before start, 111 ms of it with an idle slot in group 64
- [cross_source_blocking] source 11520 seq 38: ready 111 ms before start, 111 ms of it with an idle slot in group 374
- [cross_source_blocking] source 6376 seq 12: ready 114 ms before start, 114 ms of it with an idle slot in group 218
- [cross_source_blocking] source 2966 seq 8: ready 173 ms before start, 173 ms of it with an idle slot in group 97
- [cross_source_blocking] source 146 seq 20: ready 111 ms before start, 111 ms of it with an idle slot in group 2
- [cross_source_blocking] source 134 seq 27: ready 111 ms before start, 111 ms of it with an idle slot in group 2
- [cross_source_blocking] source 5222 seq 38: ready 103 ms before start, 103 ms of it with an idle slot in group 181
- [cross_source_blocking] source 15026 seq 20: ready 103 ms before start, 103 ms of it with an idle slot in group 469
- [cross_source_blocking] source 5714 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 197
- [cross_source_blocking] source 10438 seq 18: ready 103 ms before start, 103 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3190 seq 32: ready 108 ms before start, 108 ms of it with an idle slot in group 106
- [cross_source_blocking] source 4054 seq 5: ready 103 ms before start, 103 ms of it with an idle slot in group 136
- [cross_source_blocking] source 10998 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 359
- [cross_source_blocking] source 4326 seq 40: ready 103 ms before start, 103 ms of it with an idle slot in group 147
- [cross_source_blocking] source 9510 seq 16: ready 111 ms before start, 111 ms of it with an idle slot in group 329
- [cross_source_blocking] source 3534 seq 16: ready 103 ms before start, 103 ms of it with an idle slot in group 121
- [cross_source_blocking] source 2250 seq 36: ready 112 ms before start, 112 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8055 seq 33: ready 209 ms before start, 209 ms of it with an idle slot in group 271
- [cross_source_blocking] source 10583 seq 13: ready 205 ms before start, 205 ms of it with an idle slot in group 348
- [cross_source_blocking] source 7207 seq 23: ready 209 ms before start, 209 ms of it with an idle slot in group 245
- [cross_source_blocking] source 5427 seq 21: ready 203 ms before start, 203 ms of it with an idle slot in group 186

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 354 | 354 | 0 | 3850.239 | 21495.807 | 23199.743 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 79 | 79 | 0 | 3276.799 | 19267.583 | 19267.583 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 804 | 804 | 0 | 9240.575 | 18087.935 | 19136.511 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 88 | 88 | 0 | 2883.583 | 17956.863 | 18612.223 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 157 | 157 | 0 | 3145.727 | 17825.791 | 18219.007 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 794 | 794 | 0 | 831.487 | 17301.503 | 19398.655 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 870 | 870 | 0 | 8716.287 | 17301.503 | 18874.367 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 328 | 328 | 0 | 6848.511 | 17170.431 | 18087.935 |
| t-294 | 7 | 1 | 133.744 | 15.435 | 667 | 0 | 853 | 853 | 0 | 97.279 | 17039.359 | 17825.791 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1252 | 1252 | 0 | 8978.431 | 16908.287 | 19267.583 |
| t-470 | 16 | 1 | 58.967 | 15.535 | 140 | 0 | 856 | 856 | 0 | 7340.031 | 16908.287 | 18481.151 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 347 | 347 | 0 | 44.543 | 16646.143 | 17956.863 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 581 | 581 | 0 | 6094.847 | 16646.143 | 18219.007 |
| t-038 | 9 | 1 | 55.117 | 8.286 | 2056 | 0 | 415 | 415 | 0 | 7864.319 | 16580.607 | 18481.151 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 7115 | 7115 | 0 | 7962.623 | 16580.607 | 19529.727 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1569 | 1569 | 0 | 7045.119 | 16580.607 | 18874.367 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 357 | 357 | 0 | 30.591 | 16580.607 | 16908.287 |
| t-240 | 8 | 1 | 80.78 | 11.925 | 361 | 0 | 615 | 615 | 0 | 9175.039 | 16580.607 | 17039.359 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 516 | 516 | 0 | 93.183 | 16580.607 | 18087.935 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 700 | 700 | 0 | 7602.175 | 16515.071 | 18350.079 |
| t-139 | 16 | 1 | 21.826 | 6.166 | 1367 | 0 | 409 | 409 | 0 | 8585.215 | 16515.071 | 16515.071 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 6551 | 6551 | 0 | 1417.215 | 16515.071 | 19529.727 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 912 | 912 | 0 | 7831.551 | 16515.071 | 17825.791 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 774 | 774 | 0 | 99.839 | 16515.071 | 16777.215 |
| t-289 | 6 | 1 | 83.389 | 8.934 | 1711 | 0 | 504 | 504 | 0 | 133.119 | 16515.071 | 16711.679 |
