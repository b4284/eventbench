# Qa-pg — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.101s, closing 0.627s, drain 2.685s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000521 | 1000521 | 15934.461 | 15936.935 | 38692.7 | 0 | 0 | 0 | 458 | 5162 | 42 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797154 | 35.924 | 7.447 | 16.279 | 1290.239 | 2727.935 | 4915.199 |
| e2e_from_send | 797154 | 34.772 | 6.695 | 14.559 | 1289.215 | 2725.887 | 4915.199 |
| producer_lag | 797154 | 1.153 | 0.792 | 2.689 | 5.803 | 20.223 | 112.191 |
| publish_call | 1000521 | 8.537 | 6.579 | 15.455 | 47.903 | 98.815 | 159.487 |
| queue_wait | 797154 | 24.031 | 6.667 | 13.927 | 633.855 | 2269.183 | 4673.535 |
| broker_dwell | 797154 | 17.134 | 0.362 | 1.608 | 627.711 | 2263.039 | 4673.535 |
| receive_call | 949664 | 36.993 | 15.127 | 94.655 | 319.999 | 470.271 | 500.735 |
| ctx_load | 797154 | 0.002 | 0.002 | 0.003 | 0.01 | 0.03 | 9.103 |
| process | 797154 | 10.725 | 0.001 | 0.002 | 0.012 | 2617.343 | 3002.367 |
| ctx_commit | 797154 | 0.002 | 0.001 | 0.001 | 0.003 | 0.017 | 18.495 |
| ack | 796681 | 0.004 | 0.003 | 0.007 | 0.018 | 0.05 | 24.383 |
| service | 797154 | 10.741 | 0.013 | 0.028 | 0.073 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2256.968 | 2240.511 | 3018.751 | 4333.567 | 4694.015 | 4694.015 |
| ready_wait | 797154 | 1.758 | 0.373 | 3.317 | 31.647 | 70.591 | 1871.871 |
| blocked | 797154 | 1.685 | 0.373 | 3.315 | 31.391 | 70.143 | 125.439 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15936.935 | 16409.7 |
| applied_total | 578849.878 | 961452 |
| bench_cpu_cores | 5.179 | 5.584 |
| bench_cpu_pct | 21.579 | 23.27 |
| bench_gc_gen0 | 2.959 | 6 |
| bench_gc_gen1 | 1.286 | 3 |
| bench_gc_gen2 | 0.735 | 2 |
| bench_gc_heap_mb | 945.569 | 1187.7 |
| bench_mem_mb | 1554.676 | 1728.4 |
| bench_threadpool_queue | 6.837 | 118 |
| bench_threadpool_threads | 33.755 | 44 |
| broker_backlog | 1577.673 | 2514 |
| ctr_eb_mssql_cpu_pct | 1.798 | 2 |
| ctr_eb_mssql_mem_mb | 6251.592 | 6253.2 |
| ctr_eb_mysql_cpu_pct | 0.53 | 0.55 |
| ctr_eb_mysql_mem_mb | 6150.2 | 6150.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 151.097 | 223.38 |
| ctr_eb_postgres_mem_mb | 3321.716 | 3397 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 36.086 | 39.18 |
| in_flight | 446.592 | 781 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.258 | 1.34 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1148901.163 | 1924003 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 205851.694 | 348542 |
| octopus_mqtt_sent_total | 353078.429 | 595585 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15934.461 | 16263.9 |
| produced_total | 579296.469 | 961800 |
| publish_mb_per_s | 51.78 | 74.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,521 events applied by workers, 1,000,521 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 42 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 31 ms, max 125 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3461 seq 1: ready 100 ms before start, 100 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 106 ms before start, 106 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 2: ready 106 ms before start, 106 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 120 ms before start, 120 ms of it with an idle slot in group 119
- [cross_source_blocking] source 10849 seq 17: ready 109 ms before start, 109 ms of it with an idle slot in group 354
- [cross_source_blocking] source 3458 seq 4: ready 107 ms before start, 107 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 4: ready 125 ms before start, 125 ms of it with an idle slot in group 119
- [cross_source_blocking] source 14688 seq 24: ready 100 ms before start, 100 ms of it with an idle slot in group 464
- [cross_source_blocking] source 9827 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 340
- [cross_source_blocking] source 1836 seq 30: ready 101 ms before start, 101 ms of it with an idle slot in group 52
- [cross_source_blocking] source 9932 seq 16: ready 102 ms before start, 102 ms of it with an idle slot in group 340
- [cross_source_blocking] source 3039 seq 33: ready 101 ms before start, 101 ms of it with an idle slot in group 102
- [cross_source_blocking] source 5424 seq 68: ready 102 ms before start, 102 ms of it with an idle slot in group 186
- [cross_source_blocking] source 14438 seq 18: ready 106 ms before start, 106 ms of it with an idle slot in group 460
- [cross_source_blocking] source 6628 seq 44: ready 103 ms before start, 103 ms of it with an idle slot in group 222
- [cross_source_blocking] source 15675 seq 104: ready 101 ms before start, 101 ms of it with an idle slot in group 489
- [cross_source_blocking] source 9588 seq 10: ready 106 ms before start, 106 ms of it with an idle slot in group 333
- [cross_source_blocking] source 10694 seq 43: ready 100 ms before start, 100 ms of it with an idle slot in group 350
- [cross_source_blocking] source 6596 seq 39: ready 106 ms before start, 106 ms of it with an idle slot in group 222
- [cross_source_blocking] source 184 seq 67: ready 106 ms before start, 106 ms of it with an idle slot in group 4

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1859.583 | 4489.215 | 4685.823 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1867.775 | 4128.767 | 4718.591 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1769.471 | 3506.175 | 4063.231 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1490.943 | 3031.039 | 4095.999 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 7.359 | 2588.671 | 4177.919 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 7.711 | 2342.911 | 4423.679 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 7.615 | 2342.911 | 2916.351 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.135 | 2244.607 | 2719.743 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 7.679 | 2244.607 | 3112.959 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 7.487 | 2244.607 | 2916.351 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 7.103 | 2244.607 | 2965.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 7.679 | 2228.223 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 7.711 | 2211.839 | 3440.639 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.359 | 2211.839 | 2867.199 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 7.455 | 2162.687 | 2867.199 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 7.583 | 2129.919 | 2932.735 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 7.199 | 2129.919 | 2850.815 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 7.583 | 2113.535 | 3129.343 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 7.647 | 2097.151 | 2752.511 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 7.551 | 2072.575 | 2752.511 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 7.263 | 2064.383 | 2719.743 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 7.327 | 2056.191 | 2949.119 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 7.487 | 2056.191 | 2965.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 7.423 | 2056.191 | 2555.903 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 7.647 | 1998.847 | 2899.967 |
