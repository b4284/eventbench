# D1-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 60.108s, closing 1.327s, drain 3.499s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1479819 | 1479819 | 23911.792 | 23914.988 | 36180.5 | 0 | 0 | 0 | 424 | 7462 | 20350 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1195496 | 63.276 | 14.879 | 75.135 | 1591.295 | 2826.239 | 5865.471 |
| e2e_from_send | 1195496 | 60.441 | 13.463 | 66.751 | 1588.223 | 2822.143 | 5865.471 |
| producer_lag | 1195496 | 2.835 | 0.789 | 3.547 | 49.567 | 118.463 | 333.311 |
| publish_call | 1479819 | 19.442 | 12.535 | 44.255 | 89.087 | 118.911 | 185.215 |
| queue_wait | 1195496 | 50.062 | 13.399 | 64.895 | 1204.223 | 2547.711 | 5689.343 |
| broker_dwell | 1195496 | 33.281 | 1.367 | 13.855 | 1186.815 | 2529.279 | 5640.191 |
| receive_call | 1356265 | 32.095 | 15.879 | 73.023 | 270.847 | 453.119 | 509.183 |
| ctx_load | 1195496 | 0.003 | 0.002 | 0.004 | 0.008 | 0.045 | 47.359 |
| process | 1195496 | 10.363 | 0.001 | 0.002 | 0.01 | 2609.151 | 3002.367 |
| ctx_commit | 1195496 | 0.002 | 0.001 | 0.001 | 0.003 | 0.021 | 22.815 |
| ack | 1183590 | 0.003 | 0.002 | 0.004 | 0.01 | 0.054 | 47.487 |
| service | 1195496 | 10.379 | 0.011 | 0.026 | 0.095 | 2609.151 | 3002.367 |
| e2e_large | 327 | 2335.678 | 2293.759 | 3129.343 | 4636.671 | 5042.175 | 5042.175 |
| ready_wait | 1195496 | 6.437 | 0.903 | 8.535 | 124.543 | 211.327 | 1352.703 |
| blocked | 1195496 | 6.413 | 0.903 | 8.535 | 124.223 | 211.327 | 383.231 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 23914.988 | 27028.3 |
| applied_total | 863625.02 | 1438130 |
| bench_cpu_cores | 6.623 | 7.408 |
| bench_cpu_pct | 27.595 | 30.87 |
| bench_gc_gen0 | 3.265 | 6 |
| bench_gc_gen1 | 1.265 | 3 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 1085.237 | 1330 |
| bench_mem_mb | 1776.784 | 1930.7 |
| bench_threadpool_queue | 12.592 | 141 |
| bench_threadpool_threads | 38.469 | 43 |
| broker_backlog | 13376.837 | 48116 |
| ctr_eb_mosquitto_cpu_pct | 43.941 | 47.95 |
| ctr_eb_mosquitto_mem_mb | 6.849 | 11.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 11.3 | 11.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 11.3 | 11.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.7 | 9.7 |
| ctr_eb_postgres_cpu_pct | 212.269 | 307.3 |
| ctr_eb_postgres_mem_mb | 5220.176 | 5311 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 39.061 | 46.04 |
| in_flight | 1284.551 | 4037 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.25 | 1.36 |
| octopus_dispatchers_alive | 1 | 1 |
| octopus_items_sent_total | 1703843.918 | 2906331 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 129443.245 | 218906 |
| octopus_mqtt_sent_total | 233972.367 | 406809 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 23911.792 | 25382.3 |
| produced_total | 864909.571 | 1438985 |
| publish_mb_per_s | 63.984 | 99.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,241 records are Completed |
| record_counts | pass | 193,241 records found, 193,241 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 424 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,479,819 events applied by workers, 1,479,819 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 20,350 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 124 ms, max 383 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3506 seq 54: ready 138 ms before start, 138 ms of it with an idle slot in group 120
- [cross_source_blocking] source 3789 seq 41: ready 138 ms before start, 138 ms of it with an idle slot in group 127
- [cross_source_blocking] source 6954 seq 16: ready 139 ms before start, 139 ms of it with an idle slot in group 236
- [cross_source_blocking] source 6512 seq 26: ready 104 ms before start, 104 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3826 seq 14: ready 119 ms before start, 119 ms of it with an idle slot in group 130
- [cross_source_blocking] source 6572 seq 30: ready 110 ms before start, 110 ms of it with an idle slot in group 222
- [cross_source_blocking] source 14521 seq 29: ready 122 ms before start, 122 ms of it with an idle slot in group 461
- [cross_source_blocking] source 3823 seq 16: ready 120 ms before start, 120 ms of it with an idle slot in group 130
- [cross_source_blocking] source 15537 seq 20: ready 103 ms before start, 103 ms of it with an idle slot in group 483
- [cross_source_blocking] source 6602 seq 14: ready 140 ms before start, 140 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3831 seq 15: ready 139 ms before start, 139 ms of it with an idle slot in group 130
- [cross_source_blocking] source 15456 seq 19: ready 130 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 1366 seq 11: ready 118 ms before start, 118 ms of it with an idle slot in group 43
- [cross_source_blocking] source 14603 seq 23: ready 139 ms before start, 139 ms of it with an idle slot in group 461
- [cross_source_blocking] source 15418 seq 15: ready 138 ms before start, 138 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11381 seq 29: ready 102 ms before start, 102 ms of it with an idle slot in group 371
- [cross_source_blocking] source 3052 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 102
- [cross_source_blocking] source 1372 seq 13: ready 139 ms before start, 139 ms of it with an idle slot in group 43
- [cross_source_blocking] source 15544 seq 21: ready 139 ms before start, 139 ms of it with an idle slot in group 483
- [cross_source_blocking] source 3053 seq 17: ready 131 ms before start, 131 ms of it with an idle slot in group 102
- [cross_source_blocking] source 4225 seq 28: ready 139 ms before start, 139 ms of it with an idle slot in group 141
- [cross_source_blocking] source 199 seq 25: ready 114 ms before start, 114 ms of it with an idle slot in group 4
- [cross_source_blocking] source 14813 seq 34: ready 118 ms before start, 118 ms of it with an idle slot in group 465

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 382 | 382 | 0 | 1933.311 | 4685.823 | 5046.271 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 152 | 152 | 0 | 1777.663 | 3817.471 | 4587.519 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 81 | 81 | 0 | 2211.839 | 3801.087 | 3801.087 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 89 | 89 | 0 | 1613.823 | 3375.103 | 4358.143 |
| t-437 | 9 | 1 | 107.839 | 16.191 | 1067 | 0 | 985 | 985 | 0 | 14.207 | 2850.815 | 4456.447 |
| t-299 | 21 | 1 | 78.78 | 26.94 | 646 | 0 | 1743 | 1743 | 0 | 15.871 | 2605.055 | 3309.567 |
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8217 | 8217 | 0 | 15.871 | 2539.519 | 5111.807 |
| t-355 | 10 | 1 | 56.219 | 9.856 | 349 | 0 | 593 | 593 | 0 | 15.231 | 2473.983 | 2916.351 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 7756 | 7756 | 0 | 15.295 | 2392.063 | 4489.215 |
| t-090 | 7 | 1 | 74.269 | 8.737 | 242 | 0 | 555 | 555 | 0 | 14.911 | 2375.679 | 2998.271 |
| t-418 | 6 | 1 | 225.2 | 22.744 | 201 | 0 | 1339 | 1339 | 0 | 15.807 | 2375.679 | 2932.735 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5440 | 5440 | 0 | 15.679 | 2375.679 | 2998.271 |
| t-295 | 12 | 1 | 103.437 | 20.547 | 3396 | 0 | 1311 | 1311 | 0 | 15.615 | 2359.295 | 3997.695 |
| t-132 | 22 | 2 | 280.528 | 95.394 | 2676 | 0 | 5711 | 5711 | 0 | 15.679 | 2342.911 | 3833.855 |
| t-447 | 17 | 1 | 84.261 | 22.739 | 558 | 0 | 1433 | 1433 | 0 | 15.231 | 2342.911 | 2981.887 |
| t-454 | 7 | 1 | 250.493 | 28.919 | 266 | 0 | 1713 | 1713 | 0 | 15.551 | 2342.911 | 2932.735 |
| t-031 | 11 | 1 | 128.922 | 23.474 | 463 | 0 | 1462 | 1462 | 0 | 14.783 | 2310.143 | 2932.735 |
| t-246 | 32 | 4 | 349.163 | 184.479 | 494 | 0 | 11066 | 11066 | 0 | 15.871 | 2310.143 | 4816.895 |
| t-115 | 10 | 1 | 61.976 | 9.912 | 3658 | 0 | 677 | 677 | 0 | 14.655 | 2244.607 | 2801.663 |
| t-302 | 12 | 1 | 172.074 | 36.802 | 1189 | 0 | 2316 | 2316 | 0 | 15.231 | 2244.607 | 2949.119 |
| t-041 | 34 | 5 | 361.724 | 203.757 | 2732 | 0 | 12471 | 12471 | 0 | 16.255 | 2211.839 | 3391.487 |
| t-499 | 36 | 2 | 105.553 | 62.215 | 1538 | 0 | 3915 | 3915 | 0 | 15.295 | 2195.455 | 2883.583 |
| t-079 | 6 | 1 | 179.789 | 18.486 | 1522 | 0 | 1105 | 1105 | 0 | 15.103 | 2162.687 | 3588.095 |
| t-314 | 24 | 3 | 348.915 | 139.029 | 525 | 0 | 8347 | 8347 | 0 | 15.615 | 2162.687 | 3686.399 |
| t-496 | 81 | 8 | 270.852 | 368.2 | 278 | 0 | 22326 | 22326 | 0 | 15.359 | 2146.303 | 3915.775 |
