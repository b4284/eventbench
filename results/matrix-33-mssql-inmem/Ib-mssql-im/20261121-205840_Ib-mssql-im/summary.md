# Ib-mssql-im — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.232s, closing 1.539s, drain 1.651s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000522 | 1000522 | 16175.849 | 16178.743 | 23240.4 | 0 | 0 | 0 | 458 | 5162 | 8252 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797156 | 148.855 | 14.751 | 164.351 | 2570.239 | 3434.495 | 6758.399 |
| e2e_from_send | 797156 | 53.705 | 13.135 | 66.495 | 1329.151 | 2766.847 | 5369.855 |
| producer_lag | 797156 | 95.151 | 1.23 | 13.287 | 2355.199 | 3139.583 | 5169.151 |
| publish_call | 1000522 | 37.544 | 13.607 | 91.903 | 345.599 | 788.479 | 5378.047 |
| queue_wait | 797156 | 42.95 | 13.063 | 62.815 | 717.311 | 2316.287 | 4739.071 |
| broker_dwell | 797156 | 22.861 | 2.015 | 16.215 | 676.863 | 2295.807 | 4657.151 |
| receive_call | 899975 | 39.016 | 17.039 | 98.303 | 323.839 | 472.575 | 539.135 |
| ctx_load | 797156 | 0.003 | 0.002 | 0.004 | 0.012 | 0.054 | 44.607 |
| process | 797156 | 10.729 | 0.001 | 0.003 | 0.017 | 2617.343 | 3014.655 |
| ctx_commit | 797156 | 0.002 | 0.001 | 0.001 | 0.004 | 0.028 | 36.223 |
| ack | 790760 | 0.004 | 0.003 | 0.005 | 0.017 | 0.071 | 35.263 |
| service | 797156 | 10.754 | 0.014 | 0.033 | 0.135 | 2617.343 | 3014.655 |
| e2e_large | 356 | 2666.185 | 2574.335 | 3952.639 | 5251.071 | 6742.015 | 6742.015 |
| ready_wait | 797156 | 6.045 | 1.101 | 11.207 | 102.271 | 257.919 | 1529.855 |
| blocked | 797156 | 5.967 | 1.101 | 11.207 | 101.695 | 241.023 | 1406.975 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16178.743 | 18861.5 |
| applied_total | 577018.49 | 961569 |
| bench_cpu_cores | 4.563 | 5.083 |
| bench_cpu_pct | 19.013 | 21.18 |
| bench_gc_gen0 | 2.714 | 5 |
| bench_gc_gen1 | 1.102 | 2 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1031.843 | 1351.4 |
| bench_mem_mb | 1726.271 | 1885.9 |
| bench_threadpool_queue | 12.327 | 66 |
| bench_threadpool_threads | 38.776 | 44 |
| broker_backlog | 2489.408 | 6685 |
| ctr_eb_mosquitto_cpu_pct | 60.348 | 67.38 |
| ctr_eb_mosquitto_mem_mb | 6.965 | 12.4 |
| ctr_eb_mssql_cpu_pct | 741.298 | 1173.49 |
| ctr_eb_mssql_mem_mb | 7484.202 | 7677.1 |
| ctr_eb_mysql_cpu_pct | 0.764 | 0.89 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 1.123 | 2.05 |
| ctr_eb_postgres_mem_mb | 3119.633 | 3123.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 62.216 | 81.04 |
| in_flight | 491.204 | 1449 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.507 | 2.97 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1149836.939 | 1932249 |
| octopus_missing_reports_total | 1292.163 | 1718 |
| octopus_mqtt_received_total | 175772.837 | 323338 |
| octopus_mqtt_sent_total | 320564.388 | 567325 |
| octopus_publish_retries_total | 3.592 | 11 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16175.849 | 18458.7 |
| produced_total | 577509.694 | 961954 |
| publish_mb_per_s | 52.902 | 79.2 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,522 events applied by workers, 1,000,522 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 8,252 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 102 ms, max 1407 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7050 seq 14: ready 101 ms before start, 101 ms of it with an idle slot in group 239
- [cross_source_blocking] source 4977 seq 24: ready 115 ms before start, 115 ms of it with an idle slot in group 171
- [cross_source_blocking] source 3621 seq 15: ready 114 ms before start, 114 ms of it with an idle slot in group 122
- [cross_source_blocking] source 12661 seq 8: ready 116 ms before start, 116 ms of it with an idle slot in group 398
- [cross_source_blocking] source 11381 seq 15: ready 116 ms before start, 116 ms of it with an idle slot in group 371
- [cross_source_blocking] source 7618 seq 13: ready 100 ms before start, 100 ms of it with an idle slot in group 261
- [cross_source_blocking] source 469 seq 7: ready 123 ms before start, 123 ms of it with an idle slot in group 16
- [cross_source_blocking] source 8041 seq 19: ready 115 ms before start, 115 ms of it with an idle slot in group 271
- [cross_source_blocking] source 15919 seq 8: ready 190 ms before start, 190 ms of it with an idle slot in group 497
- [cross_source_blocking] source 2761 seq 18: ready 375 ms before start, 375 ms of it with an idle slot in group 84
- [cross_source_blocking] source 3949 seq 19: ready 367 ms before start, 367 ms of it with an idle slot in group 131
- [cross_source_blocking] source 1253 seq 9: ready 368 ms before start, 368 ms of it with an idle slot in group 40
- [cross_source_blocking] source 9317 seq 16: ready 368 ms before start, 368 ms of it with an idle slot in group 322
- [cross_source_blocking] source 3093 seq 16: ready 377 ms before start, 377 ms of it with an idle slot in group 103
- [cross_source_blocking] source 10089 seq 9: ready 376 ms before start, 376 ms of it with an idle slot in group 343
- [cross_source_blocking] source 8393 seq 8: ready 366 ms before start, 366 ms of it with an idle slot in group 285
- [cross_source_blocking] source 11437 seq 13: ready 368 ms before start, 368 ms of it with an idle slot in group 372
- [cross_source_blocking] source 3889 seq 18: ready 375 ms before start, 375 ms of it with an idle slot in group 131
- [cross_source_blocking] source 11453 seq 10: ready 375 ms before start, 375 ms of it with an idle slot in group 372
- [cross_source_blocking] source 10229 seq 24: ready 368 ms before start, 368 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2162.687 | 5734.399 | 6750.207 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2056.191 | 4947.967 | 5242.879 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2293.759 | 4915.199 | 5505.023 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1736.703 | 4358.143 | 4653.055 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 881 | 881 | 0 | 14.271 | 3375.103 | 5046.271 |
| t-280 | 12 | 1 | 50.839 | 9.951 | 2814 | 0 | 621 | 621 | 0 | 15.359 | 3260.415 | 4079.615 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 14.783 | 3244.031 | 5734.399 |
| t-269 | 24 | 1 | 88.126 | 37.126 | 531 | 0 | 2294 | 2294 | 0 | 15.551 | 3211.263 | 5177.343 |
| t-335 | 6 | 1 | 35.932 | 3.38 | 450 | 0 | 223 | 223 | 0 | 14.655 | 3211.263 | 3342.335 |
| t-298 | 6 | 1 | 79.466 | 8.739 | 3700 | 0 | 530 | 530 | 0 | 14.591 | 3194.879 | 3440.639 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 15.167 | 3194.879 | 5013.503 |
| t-345 | 13 | 1 | 20.655 | 4.666 | 1052 | 0 | 330 | 330 | 0 | 14.719 | 3162.111 | 3244.031 |
| t-087 | 6 | 1 | 43.32 | 4.675 | 1614 | 0 | 287 | 287 | 0 | 15.231 | 3112.959 | 3457.023 |
| t-395 | 6 | 1 | 43.526 | 4.665 | 212 | 0 | 310 | 310 | 0 | 15.871 | 3080.191 | 3227.647 |
| t-452 | 6 | 1 | 25.545 | 2.672 | 1797 | 0 | 139 | 139 | 0 | 13.567 | 3080.191 | 3227.647 |
| t-432 | 9 | 1 | 72.613 | 10.689 | 747 | 0 | 650 | 650 | 0 | 15.039 | 3063.807 | 5079.039 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 14.975 | 3031.039 | 5603.327 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4966 | 4966 | 0 | 14.911 | 3014.655 | 5079.039 |
| t-356 | 6 | 1 | 105.761 | 10.36 | 303 | 0 | 677 | 677 | 0 | 14.783 | 3014.655 | 3358.719 |
| t-330 | 9 | 1 | 57.729 | 8.037 | 2607 | 0 | 505 | 505 | 0 | 14.847 | 2998.271 | 3276.799 |
| t-415 | 14 | 1 | 85.928 | 19.862 | 2972 | 0 | 1235 | 1235 | 0 | 15.487 | 2998.271 | 3260.415 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 15.423 | 2998.271 | 3915.775 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 14.271 | 2981.887 | 3325.951 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 15.807 | 2981.887 | 4915.199 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 924 | 924 | 0 | 14.591 | 2981.887 | 3424.255 |
