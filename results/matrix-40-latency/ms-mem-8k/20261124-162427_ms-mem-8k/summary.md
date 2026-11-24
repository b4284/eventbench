# ms-mem-8k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 544 workers, offered 7974.007 ev/s
- Phases: run 60.122s, closing 1.032s, drain 1.602s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 518101 | 518101 | 7952.316 | 7952.343 | 29940.7 | 0 | 0 | 0 | 430 | 430 | 169 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 397692 | 17.819 | 11.543 | 29.503 | 80.319 | 145.407 | 6062.079 |
| e2e_from_send | 397692 | 16.495 | 10.199 | 27.711 | 77.759 | 142.463 | 6017.023 |
| producer_lag | 397692 | 1.324 | 0.953 | 2.761 | 7.299 | 38.623 | 144.511 |
| publish_call | 518101 | 21.145 | 10.319 | 36.415 | 179.455 | 1077.247 | 1211.391 |
| queue_wait | 397692 | 14.768 | 10.183 | 27.679 | 77.183 | 129.407 | 4837.375 |
| broker_dwell | 397692 | 1.687 | 0.656 | 1.954 | 24.655 | 64.575 | 4804.607 |
| receive_call | 482038 | 56.405 | 25.039 | 150.143 | 397.823 | 487.167 | 507.391 |
| ctx_load | 397692 | 0.002 | 0.002 | 0.003 | 0.011 | 0.035 | 13.031 |
| process | 397692 | 1.708 | 0.001 | 0.002 | 0.009 | 0.076 | 2996.223 |
| ctx_commit | 397692 | 0.001 | 0.001 | 0.002 | 0.004 | 0.022 | 11.415 |
| ack | 397461 | 0.005 | 0.003 | 0.008 | 0.021 | 0.061 | 18.687 |
| service | 397692 | 1.727 | 0.014 | 0.032 | 0.071 | 5.727 | 2996.223 |
| e2e_large | 333 | 2304.44 | 2260.991 | 3065.855 | 5099.519 | 6062.079 | 6062.079 |
| ready_wait | 397692 | 2.425 | 0.368 | 7.375 | 40.863 | 80.191 | 1701.887 |
| blocked | 397692 | 2.403 | 0.368 | 7.375 | 40.799 | 80.127 | 171.903 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7952.343 | 8514.2 |
| applied_total | 293617.959 | 484359 |
| bench_cpu_cores | 2.96 | 3.313 |
| bench_cpu_pct | 12.335 | 13.81 |
| bench_gc_gen0 | 2.143 | 4 |
| bench_gc_gen1 | 1.061 | 2 |
| bench_gc_gen2 | 0.735 | 2 |
| bench_gc_heap_mb | 851.727 | 1086.9 |
| bench_mem_mb | 1347.531 | 1467.7 |
| bench_threadpool_queue | 3.184 | 42 |
| bench_threadpool_threads | 33.286 | 38 |
| broker_backlog | 691.592 | 1288 |
| ctr_eb_mssql_cpu_pct | 296.05 | 362.01 |
| ctr_eb_mssql_mem_mb | 6984.857 | 7088.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.216 | 0.33 |
| ctr_eb_postgres_mem_mb | 2526.461 | 2531.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 26.813 | 35.57 |
| in_flight | 39.122 | 334 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.214 | 1.4 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 587088.939 | 976700 |
| octopus_missing_reports_total | 627.061 | 1194 |
| octopus_mqtt_received_total | 160963.592 | 268201 |
| octopus_mqtt_sent_total | 226167.429 | 377523 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 7952.316 | 8476.1 |
| produced_total | 293657.082 | 484375 |
| publish_mb_per_s | 36.221 | 54.83 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,664 records are Completed |
| record_counts | pass | 82,664 records found, 82,664 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 518,101 events applied by workers, 518,101 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 169 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 41 ms, max 172 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2590 seq 4: ready 117 ms before start, 117 ms of it with an idle slot in group 80
- [cross_source_blocking] source 4606 seq 9: ready 113 ms before start, 113 ms of it with an idle slot in group 161
- [cross_source_blocking] source 6134 seq 5: ready 117 ms before start, 117 ms of it with an idle slot in group 210
- [cross_source_blocking] source 12234 seq 8: ready 117 ms before start, 117 ms of it with an idle slot in group 389
- [cross_source_blocking] source 4897 seq 6: ready 102 ms before start, 102 ms of it with an idle slot in group 169
- [cross_source_blocking] source 13886 seq 8: ready 117 ms before start, 117 ms of it with an idle slot in group 440
- [cross_source_blocking] source 3641 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 123
- [cross_source_blocking] source 14089 seq 4: ready 140 ms before start, 140 ms of it with an idle slot in group 449
- [cross_source_blocking] source 11665 seq 7: ready 141 ms before start, 141 ms of it with an idle slot in group 378
- [cross_source_blocking] source 14197 seq 9: ready 141 ms before start, 141 ms of it with an idle slot in group 453
- [cross_source_blocking] source 3431 seq 4: ready 110 ms before start, 110 ms of it with an idle slot in group 119
- [cross_source_blocking] source 5319 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 182
- [cross_source_blocking] source 2235 seq 23: ready 105 ms before start, 105 ms of it with an idle slot in group 65
- [cross_source_blocking] source 3461 seq 1: ready 145 ms before start, 145 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3425 seq 2: ready 129 ms before start, 129 ms of it with an idle slot in group 119
- [cross_source_blocking] source 295 seq 11: ready 123 ms before start, 123 ms of it with an idle slot in group 8
- [cross_source_blocking] source 1055 seq 5: ready 123 ms before start, 123 ms of it with an idle slot in group 32
- [cross_source_blocking] source 3459 seq 2: ready 116 ms before start, 116 ms of it with an idle slot in group 119
- [cross_source_blocking] source 2581 seq 5: ready 101 ms before start, 101 ms of it with an idle slot in group 80
- [cross_source_blocking] source 3725 seq 7: ready 100 ms before start, 100 ms of it with an idle slot in group 126
- [cross_source_blocking] source 14353 seq 2: ready 101 ms before start, 101 ms of it with an idle slot in group 458

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 384 | 384 | 0 | 1916.927 | 5931.007 | 6062.079 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 86 | 86 | 0 | 1679.359 | 4095.999 | 4292.607 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 81 | 81 | 0 | 2064.383 | 4063.231 | 4063.231 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 167 | 167 | 0 | 1769.471 | 3555.327 | 5111.807 |
| t-291 | 7 | 1 | 25.77 | 2.932 | 368 | 0 | 183 | 183 | 0 | 11.327 | 121.855 | 128.511 |
| t-440 | 6 | 1 | 12.273 | 1.261 | 154 | 0 | 103 | 103 | 0 | 12.415 | 121.343 | 122.879 |
| t-079 | 6 | 1 | 59.93 | 6.162 | 1522 | 0 | 407 | 407 | 0 | 11.199 | 110.079 | 125.951 |
| t-054 | 17 | 1 | 22.221 | 6.363 | 161 | 0 | 408 | 408 | 0 | 11.583 | 106.495 | 158.719 |
| t-123 | 16 | 1 | 41.638 | 10.35 | 4028 | 0 | 644 | 644 | 0 | 11.967 | 103.935 | 136.191 |
| t-463 | 12 | 1 | 12.01 | 2.433 | 2195 | 0 | 169 | 169 | 0 | 12.287 | 102.911 | 111.103 |
| t-407 | 30 | 1 | 17.733 | 8.56 | 222 | 0 | 578 | 578 | 0 | 11.711 | 102.399 | 119.807 |
| t-256 | 14 | 1 | 22.369 | 4.948 | 2927 | 0 | 339 | 339 | 0 | 12.543 | 101.887 | 138.239 |
| t-339 | 17 | 1 | 7.716 | 2.214 | 134 | 0 | 173 | 173 | 0 | 11.327 | 100.863 | 115.711 |
| t-405 | 6 | 1 | 15.154 | 1.503 | 874 | 0 | 117 | 117 | 0 | 11.647 | 100.863 | 122.367 |
| t-444 | 16 | 1 | 21.972 | 5.945 | 188 | 0 | 406 | 406 | 0 | 11.199 | 100.863 | 121.343 |
| t-151 | 6 | 1 | 14.775 | 1.546 | 577 | 0 | 125 | 125 | 0 | 11.775 | 100.351 | 118.783 |
| t-459 | 25 | 1 | 10.391 | 4.24 | 2174 | 0 | 336 | 336 | 0 | 12.223 | 100.351 | 125.951 |
| t-422 | 9 | 1 | 20.596 | 3.061 | 2679 | 0 | 232 | 232 | 0 | 11.007 | 99.327 | 111.615 |
| t-476 | 15 | 1 | 23.787 | 6.636 | 432 | 0 | 441 | 441 | 0 | 11.071 | 98.815 | 142.335 |
| t-007 | 17 | 1 | 31.606 | 8.692 | 140 | 0 | 560 | 560 | 0 | 11.583 | 97.279 | 119.295 |
| t-135 | 6 | 1 | 9.423 | 0.961 | 791 | 0 | 73 | 73 | 0 | 12.031 | 96.767 | 122.879 |
| t-118 | 15 | 1 | 14.944 | 3.781 | 138 | 0 | 280 | 280 | 0 | 11.903 | 96.255 | 134.143 |
| t-161 | 41 | 1 | 37.816 | 25.619 | 339 | 0 | 1689 | 1689 | 0 | 11.647 | 96.255 | 140.287 |
| t-414 | 21 | 1 | 20.375 | 7.128 | 347 | 0 | 479 | 479 | 0 | 11.391 | 96.255 | 130.559 |
| t-162 | 69 | 1 | 6.543 | 7.534 | 353 | 0 | 612 | 612 | 0 | 11.263 | 95.231 | 144.383 |
