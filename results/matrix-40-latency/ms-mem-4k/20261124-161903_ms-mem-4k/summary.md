# ms-mem-4k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 511 workers, offered 3991.312 ev/s
- Phases: run 60.134s, closing 1.042s, drain 4.402s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 275209 | 275209 | 4003.043 | 4000.941 | 23294.6 | 0 | 0 | 0 | 457 | 457 | 232 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 200192 | 21.961 | 9.479 | 20.271 | 372.735 | 2105.343 | 5894.143 |
| e2e_from_send | 200192 | 20.06 | 8.175 | 18.367 | 354.303 | 2103.295 | 5890.047 |
| producer_lag | 200192 | 1.902 | 1.055 | 2.829 | 8.887 | 186.495 | 758.783 |
| publish_call | 275209 | 31.151 | 8.583 | 47.231 | 449.023 | 1866.751 | 2014.207 |
| queue_wait | 200192 | 16.476 | 8.155 | 18.335 | 155.775 | 695.807 | 4517.887 |
| broker_dwell | 200192 | 2.527 | 0.664 | 1.384 | 52.287 | 110.655 | 4468.735 |
| receive_call | 254473 | 82.201 | 40.031 | 231.935 | 450.815 | 494.335 | 536.575 |
| ctx_load | 200192 | 0.002 | 0.002 | 0.003 | 0.011 | 0.033 | 33.439 |
| process | 200192 | 3.558 | 0.001 | 0.003 | 0.011 | 1881.087 | 3008.511 |
| ctx_commit | 200192 | 0.002 | 0.001 | 0.002 | 0.004 | 0.024 | 14.351 |
| ack | 200099 | 0.007 | 0.005 | 0.01 | 0.024 | 0.065 | 34.175 |
| service | 200192 | 3.584 | 0.02 | 0.037 | 0.08 | 1881.087 | 3010.559 |
| e2e_large | 352 | 2315.812 | 2252.799 | 3094.527 | 5132.287 | 5894.143 | 5894.143 |
| ready_wait | 200192 | 2.531 | 0.297 | 6.883 | 51.295 | 106.879 | 2183.167 |
| blocked | 200192 | 2.469 | 0.297 | 6.883 | 51.007 | 103.295 | 652.799 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4000.941 | 5015.2 |
| applied_total | 150956.49 | 246844 |
| bench_cpu_cores | 2.011 | 2.446 |
| bench_cpu_pct | 8.377 | 10.19 |
| bench_gc_gen0 | 1.918 | 4 |
| bench_gc_gen1 | 1.02 | 2 |
| bench_gc_gen2 | 0.796 | 2 |
| bench_gc_heap_mb | 781.671 | 945.6 |
| bench_mem_mb | 1173.965 | 1276.8 |
| bench_threadpool_queue | 0.49 | 19 |
| bench_threadpool_threads | 29.082 | 32 |
| broker_backlog | 420.918 | 2274 |
| ctr_eb_mssql_cpu_pct | 243.194 | 329.79 |
| ctr_eb_mssql_mem_mb | 6867.322 | 7030.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.173 | 0.3 |
| ctr_eb_postgres_mem_mb | 2482.876 | 2508.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 27.443 | 30.78 |
| in_flight | 49.143 | 371 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.992 | 1.2 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 311154.02 | 512968 |
| octopus_missing_reports_total | 109.612 | 198 |
| octopus_mqtt_received_total | 137061.653 | 228298 |
| octopus_mqtt_sent_total | 153640.673 | 255971 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 4003.043 | 5276.1 |
| produced_total | 151005.633 | 246978 |
| publish_mb_per_s | 30.911 | 50.65 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 51,849 records are Completed |
| record_counts | pass | 51,849 records found, 51,849 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 275,209 events applied by workers, 275,209 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 232 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 51 ms, max 653 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4651 seq 1: ready 111 ms before start, 111 ms of it with an idle slot in group 162
- [cross_source_blocking] source 12815 seq 2: ready 111 ms before start, 111 ms of it with an idle slot in group 405
- [cross_source_blocking] source 14717 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 464
- [cross_source_blocking] source 9279 seq 1: ready 105 ms before start, 105 ms of it with an idle slot in group 319
- [cross_source_blocking] source 10642 seq 4: ready 105 ms before start, 105 ms of it with an idle slot in group 350
- [cross_source_blocking] source 3477 seq 1: ready 121 ms before start, 121 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3424 seq 3: ready 129 ms before start, 129 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 156 ms before start, 156 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3463 seq 4: ready 121 ms before start, 121 ms of it with an idle slot in group 119
- [cross_source_blocking] source 5323 seq 13: ready 100 ms before start, 100 ms of it with an idle slot in group 182
- [cross_source_blocking] source 6409 seq 5: ready 104 ms before start, 104 ms of it with an idle slot in group 220
- [cross_source_blocking] source 10274 seq 14: ready 134 ms before start, 134 ms of it with an idle slot in group 344
- [cross_source_blocking] source 12414 seq 5: ready 138 ms before start, 138 ms of it with an idle slot in group 394
- [cross_source_blocking] source 6118 seq 8: ready 129 ms before start, 129 ms of it with an idle slot in group 210
- [cross_source_blocking] source 15609 seq 5: ready 132 ms before start, 132 ms of it with an idle slot in group 485
- [cross_source_blocking] source 6268 seq 11: ready 141 ms before start, 141 ms of it with an idle slot in group 214
- [cross_source_blocking] source 10737 seq 17: ready 121 ms before start, 121 ms of it with an idle slot in group 352
- [cross_source_blocking] source 6102 seq 4: ready 126 ms before start, 126 ms of it with an idle slot in group 209
- [cross_source_blocking] source 2973 seq 10: ready 113 ms before start, 113 ms of it with an idle slot in group 97
- [cross_source_blocking] source 12090 seq 4: ready 126 ms before start, 126 ms of it with an idle slot in group 386

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 413 | 413 | 0 | 1974.271 | 5341.183 | 5898.239 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 182 | 182 | 0 | 1744.895 | 3948.543 | 3981.311 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 83 | 83 | 0 | 1630.207 | 3538.943 | 3604.479 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 1540.095 | 3194.879 | 3194.879 |
| t-142 | 15 | 1 | 8.345 | 2.032 | 413 | 0 | 124 | 124 | 0 | 8.319 | 663.551 | 733.183 |
| t-217 | 8 | 1 | 5.492 | 0.681 | 1936 | 0 | 43 | 43 | 0 | 10.239 | 651.263 | 651.263 |
| t-307 | 16 | 1 | 8.185 | 2.208 | 200 | 0 | 159 | 159 | 0 | 9.791 | 647.167 | 786.431 |
| t-207 | 27 | 1 | 6.436 | 2.951 | 595 | 0 | 231 | 231 | 0 | 9.087 | 638.975 | 651.263 |
| t-411 | 15 | 1 | 15.697 | 3.6 | 2644 | 0 | 250 | 250 | 0 | 9.407 | 638.975 | 790.527 |
| t-494 | 35 | 1 | 10.251 | 5.812 | 2036 | 0 | 425 | 425 | 0 | 9.535 | 622.591 | 786.431 |
| t-072 | 22 | 1 | 11.761 | 4.252 | 388 | 0 | 307 | 307 | 0 | 9.215 | 614.399 | 630.783 |
| t-292 | 16 | 1 | 13.856 | 3.996 | 916 | 0 | 296 | 296 | 0 | 9.663 | 610.303 | 749.567 |
| t-298 | 6 | 1 | 19.866 | 2.185 | 3700 | 0 | 144 | 144 | 0 | 9.663 | 606.207 | 729.087 |
| t-323 | 6 | 1 | 9.146 | 0.918 | 3540 | 0 | 59 | 59 | 0 | 8.895 | 598.015 | 598.015 |
| t-498 | 10 | 1 | 10.516 | 1.686 | 292 | 0 | 127 | 127 | 0 | 9.663 | 598.015 | 614.399 |
| t-228 | 27 | 1 | 11.395 | 5.011 | 2869 | 0 | 343 | 343 | 0 | 9.855 | 593.919 | 688.127 |
| t-312 | 30 | 1 | 6.841 | 3.388 | 213 | 0 | 252 | 252 | 0 | 9.855 | 589.823 | 794.623 |
| t-255 | 11 | 1 | 11.005 | 1.988 | 2029 | 0 | 131 | 131 | 0 | 9.919 | 585.727 | 663.551 |
| t-054 | 17 | 1 | 11.11 | 3.182 | 161 | 0 | 224 | 224 | 0 | 9.599 | 577.535 | 745.471 |
| t-384 | 12 | 1 | 6.918 | 1.372 | 844 | 0 | 119 | 119 | 0 | 9.599 | 577.535 | 733.183 |
| t-430 | 6 | 1 | 7.389 | 0.801 | 1163 | 0 | 61 | 61 | 0 | 9.407 | 561.151 | 561.151 |
| t-363 | 19 | 1 | 28.874 | 9.369 | 639 | 0 | 640 | 640 | 0 | 9.279 | 557.055 | 790.527 |
| t-110 | 9 | 1 | 5.854 | 0.883 | 2746 | 0 | 68 | 68 | 0 | 8.895 | 552.959 | 552.959 |
| t-035 | 22 | 1 | 14.543 | 5.553 | 264 | 0 | 371 | 371 | 0 | 9.599 | 548.863 | 630.783 |
| t-471 | 28 | 1 | 3.254 | 1.543 | 330 | 0 | 145 | 145 | 0 | 9.663 | 548.863 | 630.783 |
