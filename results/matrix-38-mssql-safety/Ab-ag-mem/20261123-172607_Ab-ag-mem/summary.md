# Ab-ag-mem — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.239s, closing 1.861s, drain 1.206s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000529 | 1000529 | 15968.171 | 15967.527 | 19860.1 | 0 | 0 | 0 | 458 | 5163 | 5737 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797164 | 74.197 | 33.567 | 87.295 | 1345.535 | 2805.759 | 6803.455 |
| e2e_from_send | 797164 | 67.519 | 30.959 | 73.471 | 1336.319 | 2797.567 | 6799.359 |
| producer_lag | 797164 | 6.679 | 1.422 | 9.799 | 98.367 | 592.383 | 1089.535 |
| publish_call | 1000529 | 46.702 | 32.111 | 84.159 | 263.935 | 566.271 | 851.455 |
| queue_wait | 797164 | 56.75 | 30.847 | 71.679 | 715.775 | 2328.575 | 4931.583 |
| broker_dwell | 797164 | 23.987 | 3.599 | 19.711 | 676.351 | 2297.855 | 4739.071 |
| receive_call | 855914 | 42.474 | 21.183 | 103.551 | 324.863 | 472.063 | 544.767 |
| ctx_load | 797164 | 0.003 | 0.002 | 0.003 | 0.012 | 0.073 | 45.823 |
| process | 797164 | 10.735 | 0.001 | 0.002 | 0.018 | 2619.391 | 3024.895 |
| ctx_commit | 797164 | 0.004 | 0.001 | 0.001 | 0.004 | 0.036 | 72.831 |
| ack | 796004 | 0.006 | 0.003 | 0.007 | 0.026 | 0.153 | 45.503 |
| service | 797164 | 10.768 | 0.014 | 0.04 | 0.285 | 2619.391 | 3024.895 |
| e2e_large | 356 | 2867.649 | 2826.239 | 3846.143 | 5697.535 | 6803.455 | 6803.455 |
| ready_wait | 797164 | 10.685 | 1.783 | 32.751 | 88.831 | 314.111 | 1831.935 |
| blocked | 797164 | 10.605 | 1.783 | 32.735 | 88.639 | 304.383 | 1577.983 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15967.527 | 16766.8 |
| applied_total | 578170.857 | 960985 |
| bench_cpu_cores | 4.697 | 5.104 |
| bench_cpu_pct | 19.572 | 21.27 |
| bench_gc_gen0 | 3.02 | 4 |
| bench_gc_gen1 | 1.408 | 2 |
| bench_gc_gen2 | 0.857 | 2 |
| bench_gc_heap_mb | 943.71 | 1209.2 |
| bench_mem_mb | 1695.7 | 1833.6 |
| bench_threadpool_queue | 60.939 | 448 |
| bench_threadpool_threads | 40.776 | 44 |
| broker_backlog | 4158.592 | 5952 |
| ctr_eb_ag1_cpu_pct | 740.647 | 842.62 |
| ctr_eb_ag1_mem_mb | 7034.653 | 7048.6 |
| ctr_eb_ag2_cpu_pct | 166.268 | 183.48 |
| ctr_eb_ag2_mem_mb | 7008.635 | 7030.5 |
| ctr_eb_ag_haproxy_cpu_pct | 26.847 | 29.17 |
| ctr_eb_ag_haproxy_mem_mb | 28.527 | 32.3 |
| ctr_eb_mysql_cpu_pct | 0.715 | 0.74 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.6 | 7.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 8.1 | 8.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.096 | 0.35 |
| ctr_eb_postgres_mem_mb | 2283.1 | 2283.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 70.22 | 78.75 |
| in_flight | 482.531 | 733 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.323 | 2.53 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1149933.245 | 1945956 |
| octopus_missing_reports_total | 7116.265 | 12399 |
| octopus_mqtt_received_total | 150253.571 | 263363 |
| octopus_mqtt_sent_total | 312091.204 | 537232 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15968.171 | 16844.5 |
| produced_total | 578653.388 | 961451 |
| publish_mb_per_s | 51.981 | 85.38 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,529 events applied by workers, 1,000,529 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 5,737 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 89 ms, max 1577 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14353 seq 4: ready 109 ms before start, 109 ms of it with an idle slot in group 458
- [cross_source_blocking] source 13080 seq 7: ready 132 ms before start, 132 ms of it with an idle slot in group 416
- [cross_source_blocking] source 5298 seq 26: ready 114 ms before start, 114 ms of it with an idle slot in group 182
- [cross_source_blocking] source 789 seq 4: ready 113 ms before start, 113 ms of it with an idle slot in group 23
- [cross_source_blocking] source 10499 seq 15: ready 108 ms before start, 108 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10504 seq 13: ready 113 ms before start, 113 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10593 seq 12: ready 113 ms before start, 113 ms of it with an idle slot in group 348
- [cross_source_blocking] source 12745 seq 11: ready 113 ms before start, 113 ms of it with an idle slot in group 402
- [cross_source_blocking] source 12741 seq 8: ready 120 ms before start, 120 ms of it with an idle slot in group 402
- [cross_source_blocking] source 10531 seq 14: ready 118 ms before start, 118 ms of it with an idle slot in group 348
- [cross_source_blocking] source 3171 seq 14: ready 111 ms before start, 111 ms of it with an idle slot in group 105
- [cross_source_blocking] source 10612 seq 21: ready 135 ms before start, 135 ms of it with an idle slot in group 348
- [cross_source_blocking] source 8812 seq 18: ready 144 ms before start, 144 ms of it with an idle slot in group 306
- [cross_source_blocking] source 3995 seq 40: ready 102 ms before start, 102 ms of it with an idle slot in group 132
- [cross_source_blocking] source 3990 seq 30: ready 114 ms before start, 114 ms of it with an idle slot in group 132
- [cross_source_blocking] source 297 seq 16: ready 102 ms before start, 102 ms of it with an idle slot in group 8
- [cross_source_blocking] source 8577 seq 20: ready 102 ms before start, 102 ms of it with an idle slot in group 295
- [cross_source_blocking] source 6752 seq 32: ready 135 ms before start, 135 ms of it with an idle slot in group 230
- [cross_source_blocking] source 9092 seq 55: ready 114 ms before start, 114 ms of it with an idle slot in group 314
- [cross_source_blocking] source 13298 seq 22: ready 114 ms before start, 114 ms of it with an idle slot in group 425

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2752.511 | 5767.167 | 6815.743 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2072.575 | 4751.359 | 5177.343 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2047.999 | 3751.935 | 4358.143 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1785.855 | 3358.719 | 4489.215 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 34.559 | 2637.823 | 4292.607 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 35.071 | 2408.447 | 4521.983 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 35.839 | 2375.679 | 2949.119 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 31.999 | 2310.143 | 2801.663 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 33.535 | 2293.759 | 3063.807 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 35.839 | 2277.375 | 3489.791 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 35.071 | 2277.375 | 3112.959 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 34.559 | 2277.375 | 2932.735 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 33.535 | 2260.991 | 2883.583 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 34.815 | 2260.991 | 3260.415 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 34.559 | 2244.607 | 2916.351 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 911 | 911 | 0 | 34.047 | 2211.839 | 2670.591 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 33.023 | 2195.455 | 3178.495 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 32.511 | 2162.687 | 2883.583 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 33.791 | 2146.303 | 2752.511 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 32.511 | 2146.303 | 2768.895 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 33.023 | 2146.303 | 2752.511 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 34.303 | 2146.303 | 2949.119 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 739 | 739 | 0 | 34.047 | 2113.535 | 3768.319 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 33.023 | 2088.959 | 2965.503 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 34.047 | 2072.575 | 3768.319 |
