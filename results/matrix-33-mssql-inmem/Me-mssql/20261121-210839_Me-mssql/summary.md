# Me-mssql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 100.041s, closing 1.483s, drain 0.322s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 630903 | 630903 | 4671.183 | 4679.68 | 22827.7 | 0 | 0 | 0 | 317 | 3202 | 6870 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 429743 | 916.407 | 20.015 | 405.247 | 14245.887 | 43089.919 | 63242.239 |
| e2e_from_send | 429743 | 169.407 | 17.599 | 75.647 | 4673.535 | 21102.591 | 42598.399 |
| producer_lag | 429743 | 746.994 | 1.51 | 154.111 | 14139.391 | 39747.583 | 63242.239 |
| publish_call | 630903 | 131.727 | 18.815 | 105.727 | 620.543 | 21118.975 | 61341.695 |
| queue_wait | 429743 | 158.631 | 17.519 | 72.063 | 4669.439 | 21102.591 | 42598.399 |
| broker_dwell | 429743 | 26.453 | 3.527 | 18.991 | 596.479 | 2537.471 | 10534.911 |
| receive_call | 556785 | 39.778 | 17.487 | 99.583 | 331.007 | 476.927 | 525.823 |
| ctx_load | 429743 | 0.002 | 0.002 | 0.003 | 0.011 | 0.062 | 21.583 |
| process | 429743 | 10.752 | 0.001 | 0.002 | 0.018 | 2605.055 | 3024.895 |
| ctx_commit | 429743 | 0.002 | 0.001 | 0.001 | 0.004 | 0.03 | 13.255 |
| ack | 424564 | 0.004 | 0.002 | 0.005 | 0.018 | 0.091 | 37.631 |
| service | 429743 | 10.775 | 0.013 | 0.033 | 0.161 | 2605.055 | 3024.895 |
| e2e_large | 215 | 3210.23 | 2750.463 | 3883.007 | 16539.647 | 17137.663 | 17137.663 |
| ready_wait | 429743 | 67.915 | 1.734 | 13.903 | 157.823 | 21102.591 | 42598.399 |
| blocked | 429743 | 67.783 | 1.734 | 13.895 | 157.055 | 21102.591 | 42598.399 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4679.68 | 20843.2 |
| applied_total | 533755.247 | 596887 |
| bench_cpu_cores | 1.401 | 4.851 |
| bench_cpu_pct | 5.838 | 20.21 |
| bench_gc_gen0 | 1.124 | 6 |
| bench_gc_gen1 | 0.427 | 3 |
| bench_gc_gen2 | 0.191 | 2 |
| bench_gc_heap_mb | 900.917 | 1268 |
| bench_mem_mb | 1689.785 | 1841 |
| bench_threadpool_queue | 3.562 | 47 |
| bench_threadpool_threads | 31.315 | 44 |
| broker_backlog | 17315.079 | 51965 |
| ctr_eb_mosquitto_cpu_pct | 19.825 | 63.85 |
| ctr_eb_mosquitto_mem_mb | 5.615 | 10.4 |
| ctr_eb_mssql_cpu_pct | 308.048 | 904.8 |
| ctr_eb_mssql_mem_mb | 7495.639 | 7533.1 |
| ctr_eb_mysql_cpu_pct | 0.436 | 0.78 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.64 | 1.77 |
| ctr_eb_postgres_mem_mb | 3120.522 | 3125.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 22.629 | 75.22 |
| in_flight | 46.719 | 717 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.171 | 2.91 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1064122.528 | 1264593 |
| octopus_missing_reports_total | 3642.191 | 3810 |
| octopus_mqtt_received_total | 172813.933 | 225777 |
| octopus_mqtt_sent_total | 306114.697 | 379946 |
| octopus_publish_retries_total | 2780.719 | 6681 |
| octopus_publish_stuck | 61.933 | 302 |
| produced_per_s | 4671.183 | 21035.7 |
| produced_total | 533801.966 | 596649 |
| publish_mb_per_s | 16.003 | 79.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 96,199 records are Completed |
| record_counts | pass | 96,199 records found, 96,199 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 317 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 630,903 events applied by workers, 630,903 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 6,870 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 157 ms, max 42587 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6117 seq 6: ready 111 ms before start, 111 ms of it with an idle slot in group 210
- [cross_source_blocking] source 8857 seq 13: ready 110 ms before start, 110 ms of it with an idle slot in group 306
- [cross_source_blocking] source 15529 seq 7: ready 113 ms before start, 113 ms of it with an idle slot in group 483
- [cross_source_blocking] source 1325 seq 43: ready 109 ms before start, 109 ms of it with an idle slot in group 41
- [cross_source_blocking] source 9093 seq 47: ready 112 ms before start, 112 ms of it with an idle slot in group 314
- [cross_source_blocking] source 1613 seq 7: ready 107 ms before start, 107 ms of it with an idle slot in group 48
- [cross_source_blocking] source 3281 seq 14: ready 111 ms before start, 111 ms of it with an idle slot in group 109
- [cross_source_blocking] source 9317 seq 16: ready 106 ms before start, 106 ms of it with an idle slot in group 322
- [cross_source_blocking] source 13317 seq 23: ready 111 ms before start, 111 ms of it with an idle slot in group 425
- [cross_source_blocking] source 4557 seq 11: ready 110 ms before start, 110 ms of it with an idle slot in group 160
- [cross_source_blocking] source 325 seq 5: ready 111 ms before start, 111 ms of it with an idle slot in group 9
- [cross_source_blocking] source 3301 seq 20: ready 107 ms before start, 107 ms of it with an idle slot in group 111
- [cross_source_blocking] source 865 seq 11: ready 115 ms before start, 115 ms of it with an idle slot in group 25
- [cross_source_blocking] source 15357 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 480
- [cross_source_blocking] source 2245 seq 26: ready 111 ms before start, 111 ms of it with an idle slot in group 65
- [cross_source_blocking] source 15876 seq 33: ready 147 ms before start, 147 ms of it with an idle slot in group 496
- [cross_source_blocking] source 10116 seq 15: ready 144 ms before start, 144 ms of it with an idle slot in group 344
- [cross_source_blocking] source 7028 seq 15: ready 147 ms before start, 147 ms of it with an idle slot in group 237
- [cross_source_blocking] source 8541 seq 13: ready 101 ms before start, 101 ms of it with an idle slot in group 293
- [cross_source_blocking] source 3489 seq 21: ready 101 ms before start, 101 ms of it with an idle slot in group 120
- [cross_source_blocking] source 14241 seq 41: ready 101 ms before start, 101 ms of it with an idle slot in group 456

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 474 | 474 | 0 | 19.455 | 43515.903 | 44302.335 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 255 | 255 | 0 | 19.199 | 42729.471 | 42991.615 |
| t-207 | 27 | 1 | 25.746 | 11.803 | 595 | 0 | 484 | 484 | 0 | 19.711 | 41943.039 | 60555.263 |
| t-141 | 7 | 1 | 136.582 | 16.007 | 134 | 0 | 571 | 571 | 0 | 20.607 | 40108.031 | 60817.407 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 1428 | 1428 | 0 | 22.015 | 39583.743 | 60817.407 |
| t-063 | 8 | 1 | 24.376 | 3.378 | 1296 | 0 | 136 | 136 | 0 | 21.887 | 36962.303 | 60293.119 |
| t-067 | 11 | 1 | 107.873 | 19.841 | 3490 | 0 | 786 | 786 | 0 | 22.399 | 36962.303 | 52166.655 |
| t-339 | 17 | 1 | 15.431 | 4.427 | 134 | 0 | 209 | 209 | 0 | 19.071 | 34078.719 | 41680.895 |
| t-232 | 6 | 1 | 70.326 | 7.149 | 805 | 0 | 267 | 267 | 0 | 20.479 | 31588.351 | 36438.015 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 4773 | 4773 | 0 | 21.375 | 27262.975 | 61341.695 |
| t-134 | 21 | 1 | 24.302 | 8.325 | 3368 | 0 | 368 | 368 | 0 | 18.047 | 25034.751 | 30801.919 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 395 | 395 | 0 | 23.167 | 25034.751 | 60555.263 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 3524 | 3524 | 0 | 20.607 | 24772.607 | 62652.415 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 909 | 909 | 0 | 19.583 | 24510.463 | 39845.887 |
| t-243 | 16 | 1 | 73.626 | 19.722 | 3143 | 0 | 748 | 748 | 0 | 19.455 | 24510.463 | 43515.903 |
| t-037 | 21 | 1 | 91.657 | 32.167 | 272 | 0 | 1280 | 1280 | 0 | 20.479 | 24248.319 | 44040.191 |
| t-202 | 8 | 1 | 56.524 | 7.268 | 1480 | 0 | 308 | 308 | 0 | 18.815 | 24117.247 | 38797.311 |
| t-303 | 32 | 1 | 49.301 | 26.54 | 659 | 0 | 1076 | 1076 | 0 | 20.479 | 24117.247 | 60817.407 |
| t-469 | 105 | 4 | 95.056 | 165.395 | 278 | 0 | 6358 | 6358 | 0 | 19.967 | 23855.103 | 62914.559 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 673 | 673 | 0 | 19.327 | 23461.887 | 39321.599 |
| t-275 | 9 | 1 | 87.516 | 13.03 | 166 | 0 | 538 | 538 | 0 | 20.351 | 23461.887 | 37748.735 |
| t-313 | 35 | 1 | 43.481 | 26.036 | 510 | 0 | 1010 | 1010 | 0 | 18.815 | 23461.887 | 40108.031 |
| t-029 | 28 | 2 | 177.66 | 82.383 | 1732 | 0 | 3156 | 3156 | 0 | 20.607 | 23068.671 | 61079.551 |
| t-375 | 6 | 1 | 92.935 | 8.861 | 275 | 0 | 328 | 328 | 0 | 20.223 | 22282.239 | 24248.319 |
| t-042 | 20 | 1 | 53.552 | 19.032 | 209 | 0 | 713 | 713 | 0 | 19.327 | 22151.167 | 40108.031 |
