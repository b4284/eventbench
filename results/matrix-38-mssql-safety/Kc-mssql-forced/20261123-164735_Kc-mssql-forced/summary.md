# Kc-mssql-forced — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.257s, closing 1.2s, drain 3.761s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 859964 | 859964 | 13042.418 | 13025.147 | 31730.8 | 0 | 0 | 0 | 400 | 4426 | 10912 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 657402 | 6530.65 | 7569.407 | 15015.935 | 16416.767 | 17235.967 | 19841.023 |
| e2e_from_send | 657402 | 121.648 | 24.591 | 115.263 | 1807.359 | 15769.599 | 19628.031 |
| producer_lag | 657402 | 6409.003 | 46.591 | 14860.287 | 16318.463 | 16523.263 | 16891.903 |
| publish_call | 859964 | 79.238 | 21.247 | 97.791 | 272.383 | 16547.839 | 16891.903 |
| queue_wait | 657402 | 110.848 | 24.031 | 113.535 | 1314.815 | 15761.407 | 17612.799 |
| broker_dwell | 657402 | 27.821 | 2.661 | 17.871 | 942.591 | 2396.159 | 17448.959 |
| receive_call | 742428 | 35.558 | 15.479 | 87.167 | 309.247 | 473.855 | 526.847 |
| ctx_load | 657402 | 0.003 | 0.002 | 0.003 | 0.011 | 0.051 | 62.175 |
| process | 657402 | 10.766 | 0.001 | 0.002 | 0.017 | 2617.343 | 3014.655 |
| ctx_commit | 657402 | 0.002 | 0.001 | 0.001 | 0.004 | 0.025 | 37.599 |
| ack | 655459 | 0.006 | 0.003 | 0.008 | 0.023 | 0.087 | 62.367 |
| service | 657402 | 10.799 | 0.015 | 0.042 | 0.164 | 2617.343 | 3014.655 |
| e2e_large | 298 | 7720.336 | 3110.911 | 17186.815 | 19382.271 | 19628.031 | 19628.031 |
| ready_wait | 657402 | 51.522 | 1.329 | 12.367 | 137.855 | 15761.407 | 16048.127 |
| blocked | 657402 | 51.354 | 1.329 | 12.351 | 135.935 | 15761.407 | 16048.127 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13025.147 | 31730.8 |
| applied_total | 477892.02 | 818376 |
| bench_cpu_cores | 3.406 | 5.787 |
| bench_cpu_pct | 14.19 | 24.11 |
| bench_gc_gen0 | 2.408 | 6 |
| bench_gc_gen1 | 1.082 | 3 |
| bench_gc_gen2 | 0.714 | 2 |
| bench_gc_heap_mb | 997.68 | 1297.1 |
| bench_mem_mb | 1693.735 | 1851.9 |
| bench_threadpool_queue | 38.184 | 721 |
| bench_threadpool_threads | 37.286 | 43 |
| broker_backlog | 2666.653 | 5089 |
| ctr_eb_mssql_cpu_pct | 546.993 | 789.56 |
| ctr_eb_mssql_mem_mb | 4263.786 | 5050.7 |
| ctr_eb_mysql_cpu_pct | 0.544 | 0.74 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.066 | 0.24 |
| ctr_eb_postgres_mem_mb | 2174.4 | 2174.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 42.173 | 69.53 |
| in_flight | 647.469 | 5030 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.904 | 3.02 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 955995.122 | 1607309 |
| octopus_missing_reports_total | 821.429 | 2733 |
| octopus_mqtt_received_total | 152176.163 | 216930 |
| octopus_mqtt_sent_total | 281653.694 | 441937 |
| octopus_publish_retries_total | 8617.469 | 18520 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 13042.418 | 31562 |
| produced_total | 478539.49 | 819825 |
| publish_mb_per_s | 42.581 | 98.68 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 122,719 records are Completed |
| record_counts | pass | 122,719 records found, 122,719 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 400 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 859,964 events applied by workers, 859,964 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 10,912 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 136 ms, max 16041 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1943 seq 15: ready 251 ms before start, 251 ms of it with an idle slot in group 55
- [cross_source_blocking] source 15251 seq 10: ready 110 ms before start, 110 ms of it with an idle slot in group 476
- [cross_source_blocking] source 11851 seq 22: ready 110 ms before start, 110 ms of it with an idle slot in group 383
- [cross_source_blocking] source 1391 seq 6: ready 110 ms before start, 110 ms of it with an idle slot in group 44
- [cross_source_blocking] source 11991 seq 16: ready 110 ms before start, 110 ms of it with an idle slot in group 383
- [cross_source_blocking] source 15339 seq 9: ready 110 ms before start, 110 ms of it with an idle slot in group 479
- [cross_source_blocking] source 8823 seq 19: ready 111 ms before start, 111 ms of it with an idle slot in group 306
- [cross_source_blocking] source 6827 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 233
- [cross_source_blocking] source 2243 seq 38: ready 111 ms before start, 111 ms of it with an idle slot in group 65
- [cross_source_blocking] source 15851 seq 31: ready 110 ms before start, 110 ms of it with an idle slot in group 496
- [cross_source_blocking] source 5287 seq 13: ready 110 ms before start, 110 ms of it with an idle slot in group 182
- [cross_source_blocking] source 15911 seq 5: ready 112 ms before start, 112 ms of it with an idle slot in group 497
- [cross_source_blocking] source 15727 seq 26: ready 111 ms before start, 111 ms of it with an idle slot in group 490
- [cross_source_blocking] source 5487 seq 8: ready 111 ms before start, 111 ms of it with an idle slot in group 188
- [cross_source_blocking] source 3311 seq 20: ready 111 ms before start, 111 ms of it with an idle slot in group 111
- [cross_source_blocking] source 1495 seq 16: ready 111 ms before start, 111 ms of it with an idle slot in group 45
- [cross_source_blocking] source 5267 seq 15: ready 111 ms before start, 111 ms of it with an idle slot in group 182
- [cross_source_blocking] source 8855 seq 19: ready 111 ms before start, 111 ms of it with an idle slot in group 306
- [cross_source_blocking] source 11211 seq 3: ready 110 ms before start, 110 ms of it with an idle slot in group 365
- [cross_source_blocking] source 14803 seq 19: ready 110 ms before start, 110 ms of it with an idle slot in group 465
- [cross_source_blocking] source 15163 seq 18: ready 112 ms before start, 112 ms of it with an idle slot in group 473
- [cross_source_blocking] source 15703 seq 23: ready 111 ms before start, 111 ms of it with an idle slot in group 490

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 352 | 352 | 0 | 3080.191 | 19529.727 | 19660.799 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 79 | 79 | 0 | 3031.039 | 19398.655 | 19398.655 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 801 | 801 | 0 | 8978.431 | 18219.007 | 19267.583 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 157 | 157 | 0 | 3178.495 | 17825.791 | 18219.007 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 872 | 872 | 0 | 9109.503 | 17694.719 | 19005.439 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 88 | 88 | 0 | 2899.967 | 17694.719 | 18219.007 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 330 | 330 | 0 | 7766.015 | 17301.503 | 18087.935 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1263 | 1263 | 0 | 9109.503 | 17301.503 | 19398.655 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 796 | 796 | 0 | 1359.871 | 17039.359 | 19136.511 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 344 | 344 | 0 | 30.847 | 17039.359 | 18350.079 |
| t-294 | 7 | 1 | 133.744 | 15.435 | 667 | 0 | 851 | 851 | 0 | 60.671 | 17039.359 | 18219.007 |
| t-470 | 16 | 1 | 58.967 | 15.535 | 140 | 0 | 857 | 857 | 0 | 8585.215 | 16777.215 | 18219.007 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 910 | 910 | 0 | 8716.287 | 16711.679 | 17956.863 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 7089 | 7089 | 0 | 8323.071 | 16646.143 | 19922.943 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 693 | 693 | 0 | 8290.303 | 16646.143 | 18612.223 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 523 | 523 | 0 | 299.007 | 16646.143 | 18087.935 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 533 | 533 | 0 | 9830.399 | 16646.143 | 17825.791 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1564 | 1564 | 0 | 7536.639 | 16580.607 | 19005.439 |
| t-118 | 15 | 1 | 29.889 | 7.561 | 138 | 0 | 426 | 426 | 0 | 7667.711 | 16580.607 | 18219.007 |
| t-148 | 6 | 1 | 77.973 | 7.127 | 715 | 0 | 401 | 401 | 0 | 9764.863 | 16580.607 | 16711.679 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 6551 | 6551 | 0 | 1474.559 | 16580.607 | 19398.655 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2405 | 2405 | 0 | 135.167 | 16580.607 | 18612.223 |
| t-312 | 30 | 1 | 27.364 | 13.55 | 213 | 0 | 708 | 708 | 0 | 31.487 | 16580.607 | 17432.575 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 1891 | 1891 | 0 | 227.327 | 16580.607 | 18612.223 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 581 | 581 | 0 | 196.607 | 16580.607 | 17956.863 |
