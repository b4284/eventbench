# Z2 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 0.601s, drain 2.777s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000502 | 1000502 | 15942.859 | 15937.924 | 38799.6 | 10 | 0 | 0 | 458 | 5162 | 214 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797135 | 42.522 | 7.191 | 12.911 | 1348.607 | 2861.055 | 30064.639 |
| e2e_from_send | 797135 | 41.339 | 6.447 | 10.991 | 1347.583 | 2859.007 | 30048.255 |
| producer_lag | 797135 | 1.183 | 0.81 | 2.735 | 6.099 | 20.799 | 104.511 |
| publish_call | 1000502 | 7.327 | 6.075 | 11.015 | 52.351 | 92.415 | 161.023 |
| queue_wait | 797135 | 30.599 | 6.419 | 10.655 | 721.407 | 2486.271 | 30048.255 |
| broker_dwell | 797135 | 24.856 | 1.085 | 2.593 | 714.239 | 2482.175 | 29999.103 |
| receive_call | 960965 | 36.026 | 14.839 | 90.687 | 316.415 | 468.735 | 509.183 |
| ctx_load | 797135 | 0.002 | 0.002 | 0.003 | 0.01 | 0.033 | 9.751 |
| process | 797135 | 10.724 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797135 | 0.002 | 0.001 | 0.001 | 0.004 | 0.02 | 21.263 |
| ack | 794760 | 0.003 | 0.002 | 0.004 | 0.014 | 0.042 | 11.551 |
| service | 797135 | 10.74 | 0.013 | 0.026 | 0.072 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2271.005 | 2260.991 | 3028.991 | 4349.951 | 4771.839 | 4771.839 |
| ready_wait | 797135 | 1.607 | 0.628 | 3.001 | 11.855 | 50.751 | 2799.615 |
| blocked | 797135 | 1.546 | 0.628 | 3.001 | 11.831 | 47.167 | 2799.615 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15937.924 | 16327.5 |
| applied_total | 578718.653 | 961495 |
| bench_cpu_cores | 4.678 | 5.054 |
| bench_cpu_pct | 19.494 | 21.06 |
| bench_gc_gen0 | 2.367 | 4 |
| bench_gc_gen1 | 0.918 | 2 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1038.916 | 1382.7 |
| bench_mem_mb | 1649.004 | 1803.3 |
| bench_threadpool_queue | 2.776 | 24 |
| bench_threadpool_threads | 30.531 | 33 |
| broker_backlog | 1541.633 | 2189 |
| ctr_eb_mssql_cpu_pct | 2.011 | 2.32 |
| ctr_eb_mssql_mem_mb | 4896.157 | 4899 |
| ctr_eb_mysql_cpu_pct | 0.583 | 0.66 |
| ctr_eb_mysql_mem_mb | 5947.865 | 5950 |
| ctr_eb_nats_octopus_2_cpu_pct | 37.604 | 39.72 |
| ctr_eb_nats_octopus_2_mem_mb | 16.84 | 21.2 |
| ctr_eb_nats_octopus_3_cpu_pct | 56.372 | 58.71 |
| ctr_eb_nats_octopus_3_mem_mb | 20.467 | 30.2 |
| ctr_eb_nats_octopus_cpu_pct | 76.918 | 80.92 |
| ctr_eb_nats_octopus_mem_mb | 23.935 | 35.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 6.7 | 6.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.1 | 9.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 140.363 | 177.31 |
| ctr_eb_postgres_mem_mb | 3542.733 | 3588.7 |
| duplicates_per_s | 0.02 | 1 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 38.615 | 44.86 |
| in_flight | 560.98 | 828 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.18 | 1.26 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1149224.163 | 1922387 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 213157.531 | 355768 |
| octopus_mqtt_sent_total | 379964.714 | 638875 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.859 | 16333.5 |
| produced_total | 579279.633 | 962175 |
| publish_mb_per_s | 51.798 | 74.28 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,502 events applied by workers, 1,000,502 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 214 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 12 ms, max 2798 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9279 seq 1: ready 111 ms before start, 111 ms of it with an idle slot in group 319
- [cross_source_blocking] source 3461 seq 1: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 120 ms before start, 120 ms of it with an idle slot in group 119
- [cross_source_blocking] source 8796 seq 20: ready 102 ms before start, 102 ms of it with an idle slot in group 306
- [cross_source_blocking] source 15256 seq 18: ready 102 ms before start, 102 ms of it with an idle slot in group 476
- [cross_source_blocking] source 10876 seq 19: ready 102 ms before start, 102 ms of it with an idle slot in group 354
- [cross_source_blocking] source 3435 seq 2: ready 108 ms before start, 108 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 119 ms before start, 119 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3480 seq 1: ready 105 ms before start, 105 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3484 seq 2: ready 117 ms before start, 117 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3458 seq 4: ready 106 ms before start, 106 ms of it with an idle slot in group 119
- [cross_source_blocking] source 2129 seq 14: ready 104 ms before start, 104 ms of it with an idle slot in group 61
- [cross_source_blocking] source 2125 seq 17: ready 104 ms before start, 104 ms of it with an idle slot in group 61
- [cross_source_blocking] source 7875 seq 32: ready 224 ms before start, 224 ms of it with an idle slot in group 265
- [cross_source_blocking] source 11791 seq 23: ready 268 ms before start, 268 ms of it with an idle slot in group 382
- [cross_source_blocking] source 3435 seq 4: ready 137 ms before start, 137 ms of it with an idle slot in group 119
- [cross_source_blocking] source 6580 seq 47: ready 447 ms before start, 447 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3207 seq 32: ready 447 ms before start, 447 ms of it with an idle slot in group 107
- [cross_source_blocking] source 9019 seq 13: ready 443 ms before start, 443 ms of it with an idle slot in group 311
- [cross_source_blocking] source 9056 seq 28: ready 447 ms before start, 447 ms of it with an idle slot in group 313

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-431 | 6 | 1 | 46.594 | 4.642 | 596 | 0 | 330 | 330 | 1 | 7.775 | 27656.191 | 30015.487 |
| t-337 | 40 | 1 | 60.247 | 39.019 | 195 | 0 | 2501 | 2501 | 2 | 7.679 | 24248.319 | 30146.559 |
| t-154 | 18 | 1 | 25.802 | 7.641 | 2158 | 0 | 500 | 500 | 1 | 6.975 | 21495.807 | 29622.271 |
| t-029 | 28 | 2 | 177.66 | 82.383 | 1732 | 0 | 5024 | 5024 | 1 | 7.295 | 10092.543 | 30015.487 |
| t-214 | 52 | 1 | 46.851 | 41.987 | 688 | 0 | 2773 | 2773 | 2 | 7.359 | 9175.039 | 30015.487 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1859.583 | 4489.215 | 4784.127 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1875.967 | 4145.151 | 4718.591 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1769.471 | 3522.559 | 4079.615 |
| t-055 | 65 | 3 | 125.803 | 130.203 | 359 | 0 | 8048 | 8048 | 1 | 7.167 | 3457.023 | 30015.487 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1507.327 | 3047.423 | 4112.383 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.391 | 2588.671 | 4161.535 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.775 | 2359.295 | 2916.351 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.487 | 2342.911 | 4423.679 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.359 | 2260.991 | 2719.743 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.231 | 2260.991 | 2899.967 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.519 | 2228.223 | 2916.351 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.295 | 2228.223 | 3080.191 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.263 | 2228.223 | 2965.503 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.487 | 2211.839 | 3457.023 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.167 | 2211.839 | 2883.583 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.295 | 2162.687 | 2867.199 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.167 | 2129.919 | 2916.351 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.743 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.167 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.359 | 2113.535 | 2752.511 |
