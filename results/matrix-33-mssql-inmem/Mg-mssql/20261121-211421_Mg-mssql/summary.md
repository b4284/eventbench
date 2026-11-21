# Mg-mssql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 81.613s, closing 1.708s, drain 0.825s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 866853 | 866853 | 9296.864 | 9315.434 | 27679 | 0 | 0 | 0 | 402 | 4459 | 6813 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 664229 | 147.645 | 21.391 | 82.303 | 4784.127 | 9994.239 | 29802.495 |
| e2e_from_send | 664229 | 94.338 | 19.247 | 67.007 | 2037.759 | 5480.447 | 22134.783 |
| producer_lag | 664229 | 53.308 | 1.303 | 7.295 | 460.287 | 4853.759 | 29786.111 |
| publish_call | 866853 | 63.296 | 18.111 | 71.039 | 391.679 | 5095.423 | 25165.823 |
| queue_wait | 664229 | 83.549 | 19.135 | 64.575 | 1411.071 | 5382.143 | 22134.783 |
| broker_dwell | 664229 | 25.398 | 3.043 | 21.263 | 641.023 | 2396.159 | 7311.359 |
| receive_call | 774060 | 39.983 | 17.663 | 100.607 | 325.631 | 471.295 | 525.311 |
| ctx_load | 664229 | 0.003 | 0.002 | 0.004 | 0.013 | 0.07 | 49.247 |
| process | 664229 | 10.761 | 0.001 | 0.002 | 0.019 | 2617.343 | 3002.367 |
| ctx_commit | 664229 | 0.002 | 0.001 | 0.002 | 0.005 | 0.037 | 32.895 |
| ack | 657419 | 0.004 | 0.002 | 0.005 | 0.02 | 0.099 | 32.447 |
| service | 664229 | 10.79 | 0.013 | 0.035 | 0.195 | 2617.343 | 3002.367 |
| e2e_large | 300 | 2734.513 | 2619.391 | 3510.271 | 7553.023 | 11304.959 | 11304.959 |
| ready_wait | 664229 | 22.669 | 1.845 | 15.079 | 103.935 | 5050.367 | 22134.783 |
| blocked | 664229 | 22.576 | 1.845 | 15.071 | 102.143 | 5050.367 | 22134.783 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9315.434 | 16863 |
| applied_total | 644917.114 | 831382 |
| bench_cpu_cores | 2.648 | 4.958 |
| bench_cpu_pct | 11.032 | 20.66 |
| bench_gc_gen0 | 1.757 | 4 |
| bench_gc_gen1 | 0.729 | 2 |
| bench_gc_gen2 | 0.414 | 1 |
| bench_gc_heap_mb | 874.757 | 1223.5 |
| bench_mem_mb | 1586.797 | 1750.8 |
| bench_threadpool_queue | 12.886 | 180 |
| bench_threadpool_threads | 35.129 | 40 |
| broker_backlog | 44725.271 | 121269 |
| ctr_eb_mosquitto_cpu_pct | 37.212 | 62.5 |
| ctr_eb_mosquitto_mem_mb | 7.053 | 22 |
| ctr_eb_mssql_cpu_pct | 579.607 | 941.97 |
| ctr_eb_mssql_mem_mb | 7389.45 | 7418.6 |
| ctr_eb_mysql_cpu_pct | 0.589 | 0.8 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.015 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.808 | 1.76 |
| ctr_eb_postgres_mem_mb | 3121.47 | 3126.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.67 | 75.7 |
| in_flight | 308.929 | 1067 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.811 | 2.69 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1261278.657 | 1716129 |
| octopus_missing_reports_total | 2964.971 | 3466 |
| octopus_mqtt_received_total | 204436.529 | 294076 |
| octopus_mqtt_sent_total | 357675.557 | 503816 |
| octopus_publish_retries_total | 375.743 | 1895 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 9296.864 | 16683.3 |
| produced_total | 645226.043 | 831227 |
| publish_mb_per_s | 30.495 | 79.14 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 123,535 records are Completed |
| record_counts | pass | 123,535 records found, 123,535 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 402 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 866,853 events applied by workers, 866,853 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 6,813 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 102 ms, max 22131 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 379 seq 15: ready 371 ms before start, 371 ms of it with an idle slot in group 11
- [cross_source_blocking] source 135 seq 17: ready 376 ms before start, 376 ms of it with an idle slot in group 2
- [cross_source_blocking] source 3403 seq 7: ready 377 ms before start, 377 ms of it with an idle slot in group 117
- [cross_source_blocking] source 3791 seq 18: ready 378 ms before start, 378 ms of it with an idle slot in group 127
- [cross_source_blocking] source 3891 seq 7: ready 377 ms before start, 377 ms of it with an idle slot in group 131
- [cross_source_blocking] source 1939 seq 17: ready 377 ms before start, 377 ms of it with an idle slot in group 55
- [cross_source_blocking] source 12191 seq 7: ready 378 ms before start, 378 ms of it with an idle slot in group 388
- [cross_source_blocking] source 10207 seq 18: ready 378 ms before start, 378 ms of it with an idle slot in group 344
- [cross_source_blocking] source 15007 seq 15: ready 378 ms before start, 378 ms of it with an idle slot in group 469
- [cross_source_blocking] source 7187 seq 26: ready 378 ms before start, 378 ms of it with an idle slot in group 245
- [cross_source_blocking] source 3279 seq 12: ready 372 ms before start, 372 ms of it with an idle slot in group 109
- [cross_source_blocking] source 15934 seq 2: ready 225 ms before start, 225 ms of it with an idle slot in group 497
- [cross_source_blocking] source 13785 seq 20: ready 217 ms before start, 217 ms of it with an idle slot in group 438
- [cross_source_blocking] source 15989 seq 13: ready 217 ms before start, 217 ms of it with an idle slot in group 499
- [cross_source_blocking] source 8048 seq 37: ready 188 ms before start, 188 ms of it with an idle slot in group 271
- [cross_source_blocking] source 5940 seq 10: ready 187 ms before start, 187 ms of it with an idle slot in group 206
- [cross_source_blocking] source 6812 seq 21: ready 189 ms before start, 189 ms of it with an idle slot in group 232
- [cross_source_blocking] source 9232 seq 8: ready 189 ms before start, 189 ms of it with an idle slot in group 317
- [cross_source_blocking] source 228 seq 12: ready 188 ms before start, 188 ms of it with an idle slot in group 5
- [cross_source_blocking] source 9604 seq 6: ready 188 ms before start, 188 ms of it with an idle slot in group 333

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-012 | 6 | 1 | 76.677 | 7.946 | 556 | 0 | 421 | 421 | 0 | 20.991 | 9633.791 | 9961.471 |
| t-048 | 18 | 1 | 29.153 | 9.129 | 136 | 0 | 505 | 505 | 0 | 22.143 | 9568.255 | 19529.727 |
| t-199 | 24 | 1 | 15.247 | 6.464 | 251 | 0 | 414 | 414 | 0 | 21.631 | 9502.719 | 19529.727 |
| t-224 | 9 | 1 | 37.952 | 5.826 | 1098 | 0 | 304 | 304 | 0 | 23.423 | 9437.183 | 9764.863 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 354 | 354 | 0 | 2326.527 | 7733.247 | 11337.727 |
| t-394 | 31 | 1 | 37.161 | 19.94 | 570 | 0 | 1207 | 1207 | 0 | 21.119 | 5668.863 | 19660.799 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 79 | 79 | 0 | 1875.967 | 5373.951 | 5373.951 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 157 | 157 | 0 | 2097.151 | 5144.575 | 6881.279 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 363 | 363 | 0 | 21.503 | 5079.039 | 9502.719 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 350 | 350 | 0 | 22.399 | 5013.503 | 9699.327 |
| t-164 | 7 | 1 | 126.563 | 13.607 | 236 | 0 | 698 | 698 | 0 | 20.735 | 4980.735 | 9699.327 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 638 | 638 | 0 | 23.167 | 4947.967 | 10092.543 |
| t-081 | 6 | 1 | 16.193 | 1.614 | 2170 | 0 | 107 | 107 | 0 | 20.479 | 4947.967 | 9895.935 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 4755 | 4755 | 0 | 22.655 | 4947.967 | 10223.615 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 761 | 761 | 0 | 21.631 | 4947.967 | 9371.647 |
| t-144 | 6 | 1 | 30.474 | 3.167 | 816 | 0 | 183 | 183 | 0 | 21.119 | 4947.967 | 9895.935 |
| t-198 | 6 | 1 | 16.318 | 1.427 | 180 | 0 | 70 | 70 | 0 | 19.455 | 4947.967 | 4947.967 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 610 | 610 | 0 | 20.351 | 4947.967 | 9633.791 |
| t-444 | 16 | 1 | 43.944 | 11.89 | 188 | 0 | 622 | 622 | 0 | 19.839 | 4947.967 | 26738.687 |
| t-452 | 6 | 1 | 25.545 | 2.672 | 1797 | 0 | 130 | 130 | 0 | 22.527 | 4947.967 | 9764.863 |
| t-040 | 119 | 2 | 32.511 | 65.473 | 1254 | 0 | 3682 | 3682 | 0 | 21.247 | 4915.199 | 26738.687 |
| t-127 | 19 | 1 | 97.741 | 29.425 | 2026 | 0 | 1581 | 1581 | 0 | 21.631 | 4915.199 | 19529.727 |
| t-137 | 10 | 1 | 27.033 | 4.549 | 1569 | 0 | 277 | 277 | 0 | 20.607 | 4915.199 | 19529.727 |
| t-141 | 7 | 1 | 136.582 | 16.007 | 134 | 0 | 801 | 801 | 0 | 21.119 | 4915.199 | 19660.799 |
| t-149 | 13 | 1 | 42.874 | 8.748 | 3034 | 0 | 448 | 448 | 0 | 22.399 | 4915.199 | 9764.863 |
