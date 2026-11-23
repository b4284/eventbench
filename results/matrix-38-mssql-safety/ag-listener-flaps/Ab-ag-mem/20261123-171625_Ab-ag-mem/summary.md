# Ab-ag-mem — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.309s, closing 2.471s, drain 0.604s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000075 | 1000075 | 16341.727 | 16347.241 | 19074.8 | 0 | 0 | 0 | 458 | 5160 | 42111 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796696 | 580.008 | 84.735 | 2072.575 | 2959.359 | 4124.671 | 8114.175 |
| e2e_from_send | 796696 | 106.005 | 56.895 | 172.031 | 1404.927 | 2846.719 | 7176.191 |
| producer_lag | 796696 | 474.003 | 5.643 | 1903.615 | 2736.127 | 3168.255 | 3569.663 |
| publish_call | 1000075 | 77.888 | 58.687 | 144.511 | 358.655 | 720.383 | 1657.855 |
| queue_wait | 796696 | 95.217 | 56.511 | 167.679 | 813.567 | 2400.255 | 5160.959 |
| broker_dwell | 796696 | 33.692 | 7.239 | 39.615 | 739.839 | 2328.575 | 5066.751 |
| receive_call | 807795 | 45.094 | 23.343 | 107.135 | 336.383 | 474.623 | 555.519 |
| ctx_load | 796696 | 0.003 | 0.002 | 0.003 | 0.012 | 0.097 | 52.543 |
| process | 796696 | 10.742 | 0.001 | 0.002 | 0.018 | 2619.391 | 3002.367 |
| ctx_commit | 796696 | 0.002 | 0.001 | 0.001 | 0.005 | 0.038 | 46.079 |
| ack | 793928 | 0.007 | 0.003 | 0.007 | 0.027 | 0.258 | 54.559 |
| service | 796696 | 10.789 | 0.014 | 0.046 | 0.853 | 2619.391 | 3016.703 |
| e2e_large | 356 | 3641.918 | 3407.871 | 5468.159 | 6832.127 | 8114.175 | 8114.175 |
| ready_wait | 796696 | 19.907 | 3.413 | 58.559 | 220.799 | 366.335 | 1780.735 |
| blocked | 796696 | 19.866 | 3.413 | 58.527 | 220.415 | 361.215 | 1780.735 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16347.241 | 19074.8 |
| applied_total | 570088.061 | 958953 |
| bench_cpu_cores | 3.886 | 4.485 |
| bench_cpu_pct | 16.194 | 18.69 |
| bench_gc_gen0 | 2.98 | 4 |
| bench_gc_gen1 | 1.408 | 2 |
| bench_gc_gen2 | 0.918 | 2 |
| bench_gc_heap_mb | 999.739 | 1265.2 |
| bench_mem_mb | 1690.224 | 1865.3 |
| bench_threadpool_queue | 40.796 | 458 |
| bench_threadpool_threads | 42.857 | 44 |
| broker_backlog | 4886.061 | 7623 |
| ctr_eb_ag1_cpu_pct | 791.979 | 867.43 |
| ctr_eb_ag1_mem_mb | 6285.943 | 6324.8 |
| ctr_eb_ag2_cpu_pct | 184.585 | 241.39 |
| ctr_eb_ag2_mem_mb | 5345.935 | 5376.8 |
| ctr_eb_ag_haproxy_cpu_pct | 26.202 | 28.76 |
| ctr_eb_ag_haproxy_mem_mb | 20.916 | 21.9 |
| ctr_eb_mysql_cpu_pct | 0.747 | 0.81 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.665 | 1.11 |
| ctr_eb_postgres_mem_mb | 2327.165 | 2327.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.319 | 79.11 |
| in_flight | 511.286 | 1129 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.239 | 2.47 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1133541.306 | 1954924 |
| octopus_missing_reports_total | 7178.837 | 12945 |
| octopus_mqtt_received_total | 127903.102 | 228056 |
| octopus_mqtt_sent_total | 301034.959 | 522476 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16341.727 | 19017.7 |
| produced_total | 570599.347 | 959498 |
| publish_mb_per_s | 53.69 | 93.43 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,823 records are Completed |
| record_counts | pass | 138,823 records found, 138,823 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,075 events applied by workers, 1,000,075 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 42,111 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 220 ms, max 1781 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6506 seq 17: ready 123 ms before start, 123 ms of it with an idle slot in group 222
- [cross_source_blocking] source 2826 seq 7: ready 114 ms before start, 114 ms of it with an idle slot in group 88
- [cross_source_blocking] source 11710 seq 13: ready 122 ms before start, 122 ms of it with an idle slot in group 379
- [cross_source_blocking] source 13950 seq 12: ready 122 ms before start, 122 ms of it with an idle slot in group 443
- [cross_source_blocking] source 4926 seq 4: ready 123 ms before start, 123 ms of it with an idle slot in group 170
- [cross_source_blocking] source 9566 seq 9: ready 123 ms before start, 123 ms of it with an idle slot in group 332
- [cross_source_blocking] source 11774 seq 8: ready 124 ms before start, 124 ms of it with an idle slot in group 382
- [cross_source_blocking] source 10014 seq 6: ready 124 ms before start, 124 ms of it with an idle slot in group 341
- [cross_source_blocking] source 9950 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 341
- [cross_source_blocking] source 15646 seq 13: ready 124 ms before start, 124 ms of it with an idle slot in group 488
- [cross_source_blocking] source 7774 seq 12: ready 124 ms before start, 124 ms of it with an idle slot in group 263
- [cross_source_blocking] source 6078 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 208
- [cross_source_blocking] source 3541 seq 21: ready 368 ms before start, 368 ms of it with an idle slot in group 121
- [cross_source_blocking] source 2261 seq 27: ready 331 ms before start, 331 ms of it with an idle slot in group 65
- [cross_source_blocking] source 13301 seq 24: ready 331 ms before start, 331 ms of it with an idle slot in group 425
- [cross_source_blocking] source 2533 seq 21: ready 361 ms before start, 361 ms of it with an idle slot in group 75
- [cross_source_blocking] source 11485 seq 17: ready 331 ms before start, 331 ms of it with an idle slot in group 373
- [cross_source_blocking] source 7868 seq 6: ready 101 ms before start, 101 ms of it with an idle slot in group 265
- [cross_source_blocking] source 924 seq 6: ready 101 ms before start, 101 ms of it with an idle slot in group 27
- [cross_source_blocking] source 7772 seq 19: ready 102 ms before start, 102 ms of it with an idle slot in group 263
- [cross_source_blocking] source 13404 seq 7: ready 102 ms before start, 102 ms of it with an idle slot in group 429

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 3014.655 | 7241.727 | 7667.711 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 3227.647 | 6586.367 | 8126.463 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2637.823 | 5767.167 | 6127.615 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2408.447 | 5406.719 | 6127.615 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 739 | 739 | 0 | 376.831 | 3883.007 | 5636.095 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 880 | 880 | 0 | 83.455 | 3866.623 | 5079.039 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 941 | 941 | 0 | 94.719 | 3588.095 | 5832.703 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1812 | 1812 | 0 | 110.591 | 3457.023 | 4751.359 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 181 | 181 | 0 | 63.743 | 3424.255 | 4489.215 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 77.823 | 3407.871 | 5013.503 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2257 | 2257 | 0 | 94.719 | 3407.871 | 5177.343 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 930 | 930 | 0 | 82.431 | 3375.103 | 3637.247 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 260 | 260 | 0 | 59.903 | 3325.951 | 3784.703 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 612 | 612 | 0 | 83.967 | 3309.567 | 5079.039 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 687 | 687 | 0 | 145.407 | 3293.183 | 5111.807 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 150.527 | 3276.799 | 5373.951 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 469 | 469 | 0 | 163.839 | 3276.799 | 4751.359 |
| t-164 | 7 | 1 | 126.563 | 13.607 | 236 | 0 | 816 | 816 | 0 | 109.567 | 3260.415 | 3473.407 |
| t-329 | 10 | 1 | 101.534 | 16.401 | 132 | 0 | 1030 | 1030 | 0 | 86.015 | 3260.415 | 4259.839 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 111.615 | 3244.031 | 4882.431 |
| t-099 | 13 | 1 | 61.347 | 14.129 | 419 | 0 | 910 | 910 | 0 | 147.455 | 3227.647 | 5701.631 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 63.743 | 3194.879 | 5079.039 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5536 | 5536 | 0 | 102.399 | 3194.879 | 5275.647 |
| t-203 | 6 | 1 | 35.674 | 3.855 | 2258 | 0 | 234 | 234 | 0 | 196.607 | 3194.879 | 3407.871 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7644 | 7644 | 0 | 104.447 | 3194.879 | 5668.863 |
