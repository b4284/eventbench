# pg-mem-8k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 544 workers, offered 7974.007 ev/s
- Phases: run 60.135s, closing 0.887s, drain 1.952s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 518104 | 518104 | 7951.704 | 7951.849 | 31721.8 | 0 | 0 | 0 | 430 | 430 | 41 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 397699 | 12.745 | 7.711 | 20.527 | 62.911 | 139.519 | 5922.815 |
| e2e_from_send | 397699 | 11.488 | 6.687 | 18.911 | 61.215 | 137.855 | 5894.143 |
| producer_lag | 397699 | 1.256 | 0.948 | 2.747 | 6.371 | 23.311 | 130.495 |
| publish_call | 518104 | 10.125 | 6.603 | 24.287 | 55.423 | 100.287 | 215.295 |
| queue_wait | 397699 | 9.763 | 6.671 | 18.895 | 60.799 | 112.127 | 4587.519 |
| broker_dwell | 397699 | 0.923 | 0.001 | 0.761 | 20.111 | 59.839 | 4567.039 |
| receive_call | 488233 | 56.026 | 24.687 | 151.167 | 399.103 | 487.167 | 502.271 |
| ctx_load | 397699 | 0.002 | 0.002 | 0.003 | 0.01 | 0.032 | 13.511 |
| process | 397699 | 1.708 | 0.001 | 0.002 | 0.009 | 0.071 | 2996.223 |
| ctx_commit | 397699 | 0.001 | 0.001 | 0.001 | 0.003 | 0.019 | 7.419 |
| ack | 397473 | 0.005 | 0.003 | 0.007 | 0.02 | 0.052 | 19.215 |
| service | 397699 | 1.726 | 0.014 | 0.029 | 0.061 | 4.207 | 2996.223 |
| e2e_large | 333 | 2288.995 | 2230.271 | 3055.615 | 5185.535 | 5922.815 | 5922.815 |
| ready_wait | 397699 | 1.558 | 0.311 | 2.757 | 28.223 | 63.231 | 1667.071 |
| blocked | 397699 | 1.536 | 0.311 | 2.757 | 28.191 | 63.135 | 157.951 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7951.849 | 8153 |
| applied_total | 293644.102 | 484321 |
| bench_cpu_cores | 2.927 | 3.277 |
| bench_cpu_pct | 12.197 | 13.66 |
| bench_gc_gen0 | 2.245 | 4 |
| bench_gc_gen1 | 1.163 | 3 |
| bench_gc_gen2 | 0.816 | 2 |
| bench_gc_heap_mb | 775.912 | 1028.8 |
| bench_mem_mb | 1199.443 | 1303.7 |
| bench_threadpool_queue | 1.327 | 17 |
| bench_threadpool_threads | 24.939 | 28 |
| broker_backlog | 530.898 | 854 |
| ctr_eb_mssql_cpu_pct | 4.975 | 41.48 |
| ctr_eb_mssql_mem_mb | 7051.676 | 7094.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 123.918 | 175.3 |
| ctr_eb_postgres_mem_mb | 3334.984 | 3460.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 23.176 | 30.52 |
| in_flight | 19.041 | 96 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.892 | 1.02 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 585661.673 | 974469 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 169419.51 | 280669 |
| octopus_mqtt_sent_total | 233194.857 | 387556 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 7951.704 | 8144 |
| produced_total | 293663.143 | 484331 |
| publish_mb_per_s | 36.223 | 57.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,664 records are Completed |
| record_counts | pass | 82,664 records found, 82,664 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 518,104 events applied by workers, 518,104 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 41 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 28 ms, max 158 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3461 seq 1: ready 115 ms before start, 115 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 118 ms before start, 118 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3425 seq 2: ready 138 ms before start, 138 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9961 seq 2: ready 112 ms before start, 112 ms of it with an idle slot in group 341
- [cross_source_blocking] source 6141 seq 10: ready 106 ms before start, 106 ms of it with an idle slot in group 210
- [cross_source_blocking] source 3435 seq 2: ready 108 ms before start, 108 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 3: ready 102 ms before start, 102 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3426 seq 1: ready 126 ms before start, 126 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 4: ready 115 ms before start, 115 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3461 seq 2: ready 108 ms before start, 108 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3474 seq 3: ready 100 ms before start, 100 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 2: ready 112 ms before start, 112 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3450 seq 2: ready 105 ms before start, 105 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3477 seq 5: ready 102 ms before start, 102 ms of it with an idle slot in group 119
- [cross_source_blocking] source 9279 seq 3: ready 108 ms before start, 108 ms of it with an idle slot in group 319
- [cross_source_blocking] source 13316 seq 37: ready 158 ms before start, 158 ms of it with an idle slot in group 425
- [cross_source_blocking] source 2256 seq 51: ready 157 ms before start, 157 ms of it with an idle slot in group 65
- [cross_source_blocking] source 10544 seq 28: ready 156 ms before start, 156 ms of it with an idle slot in group 348
- [cross_source_blocking] source 1512 seq 26: ready 154 ms before start, 154 ms of it with an idle slot in group 45
- [cross_source_blocking] source 9092 seq 112: ready 158 ms before start, 158 ms of it with an idle slot in group 314
- [cross_source_blocking] source 12816 seq 11: ready 157 ms before start, 157 ms of it with an idle slot in group 405
- [cross_source_blocking] source 2252 seq 45: ready 154 ms before start, 154 ms of it with an idle slot in group 65

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 384 | 384 | 0 | 1900.543 | 5701.631 | 5931.007 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 86 | 86 | 0 | 1654.783 | 4079.615 | 4259.839 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 81 | 81 | 0 | 2072.575 | 4063.231 | 4063.231 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 167 | 167 | 0 | 1785.855 | 3506.175 | 5111.807 |
| t-381 | 11 | 1 | 29.155 | 5.48 | 134 | 0 | 325 | 325 | 0 | 7.295 | 119.295 | 146.431 |
| t-145 | 6 | 1 | 31.886 | 3.213 | 517 | 0 | 190 | 190 | 0 | 7.775 | 92.671 | 126.463 |
| t-448 | 6 | 1 | 29.609 | 3.108 | 2313 | 0 | 187 | 187 | 0 | 7.775 | 86.015 | 91.135 |
| t-009 | 29 | 1 | 11.947 | 5.734 | 2407 | 0 | 422 | 422 | 0 | 7.711 | 85.503 | 115.199 |
| t-310 | 6 | 1 | 43.699 | 4.579 | 1910 | 0 | 297 | 297 | 0 | 7.871 | 85.503 | 100.351 |
| t-031 | 11 | 1 | 42.974 | 7.825 | 463 | 0 | 522 | 522 | 0 | 7.519 | 84.991 | 143.359 |
| t-224 | 9 | 1 | 18.976 | 2.913 | 1098 | 0 | 187 | 187 | 0 | 7.935 | 82.943 | 93.183 |
| t-254 | 9 | 1 | 13.095 | 2.033 | 3623 | 0 | 144 | 144 | 0 | 7.903 | 82.431 | 88.575 |
| t-419 | 17 | 1 | 12.701 | 3.761 | 3214 | 0 | 280 | 280 | 0 | 8.255 | 81.919 | 93.183 |
| t-238 | 13 | 1 | 35.357 | 7.624 | 2335 | 0 | 472 | 472 | 0 | 7.583 | 80.895 | 115.199 |
| t-275 | 9 | 1 | 43.758 | 6.515 | 166 | 0 | 421 | 421 | 0 | 7.711 | 79.359 | 119.295 |
| t-076 | 23 | 1 | 11.022 | 4.166 | 4045 | 0 | 304 | 304 | 0 | 8.447 | 78.335 | 87.551 |
| t-167 | 10 | 1 | 15.379 | 2.651 | 1266 | 0 | 200 | 200 | 0 | 7.743 | 78.335 | 79.871 |
| t-291 | 7 | 1 | 25.77 | 2.932 | 368 | 0 | 183 | 183 | 0 | 8.031 | 77.823 | 93.695 |
| t-303 | 32 | 1 | 24.651 | 13.27 | 659 | 0 | 851 | 851 | 0 | 7.775 | 77.823 | 129.023 |
| t-351 | 14 | 1 | 12.254 | 2.765 | 4034 | 0 | 197 | 197 | 0 | 7.295 | 77.823 | 83.455 |
| t-453 | 6 | 1 | 27.865 | 2.745 | 874 | 0 | 169 | 169 | 0 | 7.487 | 77.823 | 126.463 |
| t-405 | 6 | 1 | 15.154 | 1.503 | 874 | 0 | 117 | 117 | 0 | 8.319 | 77.311 | 159.743 |
| t-161 | 41 | 1 | 37.816 | 25.619 | 339 | 0 | 1689 | 1689 | 0 | 7.903 | 76.799 | 133.119 |
| t-166 | 6 | 1 | 16.198 | 1.556 | 292 | 0 | 99 | 99 | 0 | 7.871 | 76.799 | 82.943 |
| t-162 | 69 | 1 | 6.543 | 7.534 | 353 | 0 | 612 | 612 | 0 | 7.679 | 76.287 | 134.143 |
