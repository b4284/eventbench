# Kd-pg-forced — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 0.612s, drain 8.107s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000543 | 1000543 | 15957.62 | 15950.498 | 60033.7 | 0 | 0 | 0 | 458 | 5163 | 2570 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797179 | 5227.725 | 1748.991 | 15613.951 | 18743.295 | 19283.967 | 24133.631 |
| e2e_from_send | 797179 | 119.441 | 19.871 | 66.239 | 2038.783 | 19251.199 | 23445.503 |
| producer_lag | 797179 | 5108.28 | 1462.271 | 15417.343 | 18579.455 | 19038.207 | 19283.967 |
| publish_call | 1000543 | 53.573 | 8.671 | 41.279 | 93.375 | 19234.815 | 19300.351 |
| queue_wait | 797179 | 108.684 | 19.503 | 63.903 | 1747.967 | 19251.199 | 21692.415 |
| broker_dwell | 797179 | 45.906 | 1.575 | 25.359 | 1523.711 | 2711.551 | 21643.263 |
| receive_call | 884463 | 27.666 | 10.527 | 67.903 | 275.711 | 456.959 | 531.455 |
| ctx_load | 797179 | 0.002 | 0.002 | 0.003 | 0.008 | 0.036 | 37.215 |
| process | 797179 | 10.73 | 0.001 | 0.002 | 0.012 | 2617.343 | 3018.751 |
| ctx_commit | 797179 | 0.002 | 0.001 | 0.001 | 0.003 | 0.018 | 39.295 |
| ack | 794626 | 0.004 | 0.003 | 0.007 | 0.017 | 0.059 | 39.199 |
| service | 797179 | 10.756 | 0.014 | 0.036 | 0.117 | 2617.343 | 3018.751 |
| e2e_large | 356 | 7716.62 | 4044.799 | 18186.239 | 21970.943 | 24133.631 | 24133.631 |
| ready_wait | 797179 | 9.157 | 0.988 | 12.367 | 75.711 | 502.015 | 19300.351 |
| blocked | 797179 | 8.212 | 0.988 | 12.303 | 72.255 | 165.375 | 19300.351 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15950.498 | 60033.7 |
| applied_total | 494326.796 | 961416 |
| bench_cpu_cores | 3.663 | 6.921 |
| bench_cpu_pct | 15.263 | 28.84 |
| bench_gc_gen0 | 2.898 | 11 |
| bench_gc_gen1 | 1.327 | 5 |
| bench_gc_gen2 | 0.878 | 4 |
| bench_gc_heap_mb | 954.241 | 1302.1 |
| bench_mem_mb | 1612.676 | 2092.2 |
| bench_threadpool_queue | 20.367 | 520 |
| bench_threadpool_threads | 29.898 | 44 |
| broker_backlog | 3220.122 | 14123 |
| ctr_eb_mssql_cpu_pct | 25.474 | 122.28 |
| ctr_eb_mssql_mem_mb | 4757.8 | 5006.8 |
| ctr_eb_mysql_cpu_pct | 0.472 | 0.7 |
| ctr_eb_mysql_mem_mb | 6156 | 6156 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 194.589 | 458.7 |
| ctr_eb_postgres_mem_mb | 2306.993 | 3035.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 26.907 | 61.65 |
| in_flight | 927.612 | 5383 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.986 | 2.48 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 990420.449 | 1949131 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 170964.02 | 267307 |
| octopus_mqtt_sent_total | 303804.592 | 514564 |
| octopus_publish_retries_total | 11906.327 | 25577 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15957.62 | 61480.7 |
| produced_total | 495254.408 | 962227 |
| publish_mb_per_s | 51.854 | 189.94 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,874 records are Completed |
| record_counts | pass | 138,874 records found, 138,874 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,543 events applied by workers, 1,000,543 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 2,570 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 72 ms, max 19291 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3430 seq 1: ready 182 ms before start, 182 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3478 seq 3: ready 100 ms before start, 100 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 1: ready 302 ms before start, 302 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3477 seq 1: ready 612 ms before start, 612 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9279 seq 1: ready 101 ms before start, 101 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3427 seq 2: ready 194 ms before start, 194 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3462 seq 2: ready 726 ms before start, 726 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3439 seq 4: ready 406 ms before start, 406 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3423 seq 1: ready 930 ms before start, 930 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3465 seq 2: ready 990 ms before start, 990 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3452 seq 1: ready 821 ms before start, 789 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3467 seq 3: ready 374 ms before start, 374 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3445 seq 2: ready 187 ms before start, 187 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3458 seq 1: ready 677 ms before start, 677 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3461 seq 1: ready 691 ms before start, 691 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 387 ms before start, 125 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3470 seq 1: ready 514 ms before start, 514 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3440 seq 1: ready 685 ms before start, 685 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9257 seq 3: ready 121 ms before start, 121 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3458 seq 2: ready 912 ms before start, 912 ms of it with an idle slot in group 119

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 4947.967 | 22413.311 | 24248.319 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 3997.695 | 20578.303 | 20971.519 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2981.887 | 19791.871 | 21364.735 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 930 | 930 | 0 | 1908.735 | 19660.799 | 21889.023 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 2506.751 | 19398.655 | 21757.951 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1027 | 1027 | 0 | 2490.367 | 19267.583 | 21102.591 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 2326.527 | 19267.583 | 20447.231 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1812 | 1812 | 0 | 1687.551 | 19267.583 | 21495.807 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2883.583 | 19267.583 | 19791.871 |
| t-101 | 6 | 1 | 39.336 | 3.539 | 198 | 0 | 231 | 231 | 0 | 2555.903 | 19267.583 | 19398.655 |
| t-305 | 8 | 1 | 77.153 | 10.084 | 436 | 0 | 607 | 607 | 0 | 2007.039 | 19267.583 | 19398.655 |
| t-422 | 9 | 1 | 41.192 | 6.122 | 2679 | 0 | 393 | 393 | 0 | 995.327 | 19267.583 | 19267.583 |
| t-144 | 6 | 1 | 30.474 | 3.167 | 816 | 0 | 204 | 204 | 0 | 16.319 | 19136.511 | 19136.511 |
| t-281 | 10 | 1 | 43.191 | 7.593 | 2160 | 0 | 456 | 456 | 0 | 2342.911 | 19136.511 | 20578.303 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 2211.839 | 19136.511 | 21889.023 |
| t-405 | 6 | 1 | 30.308 | 3.005 | 874 | 0 | 187 | 187 | 0 | 1982.463 | 19136.511 | 19398.655 |
| t-470 | 16 | 1 | 58.967 | 15.535 | 140 | 0 | 996 | 996 | 0 | 1736.703 | 19136.511 | 20840.447 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 616 | 616 | 0 | 2916.351 | 19136.511 | 19398.655 |
| t-008 | 11 | 1 | 91.742 | 16.542 | 279 | 0 | 962 | 962 | 0 | 1753.087 | 19005.439 | 20709.375 |
| t-024 | 14 | 1 | 41.39 | 9.449 | 1743 | 0 | 596 | 596 | 0 | 1490.943 | 19005.439 | 19398.655 |
| t-035 | 22 | 1 | 58.172 | 22.212 | 264 | 0 | 1406 | 1406 | 0 | 716.799 | 19005.439 | 20840.447 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8301 | 8301 | 0 | 2211.839 | 19005.439 | 21757.951 |
| t-064 | 11 | 1 | 57.27 | 10.782 | 263 | 0 | 728 | 728 | 0 | 1859.583 | 19005.439 | 19398.655 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3533 | 3533 | 0 | 2179.071 | 19005.439 | 21233.663 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 408 | 408 | 0 | 983.039 | 19005.439 | 20185.087 |
