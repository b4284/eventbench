# Ka-mssql-durable — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.207s, closing 1.553s, drain 2.302s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 684168 | 684168 | 9470.386 | 9460.184 | 28403.7 | 0 | 0 | 0 | 340 | 3465 | 5247 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 482620 | 6882.777 | 23.679 | 22527.999 | 23625.727 | 24690.687 | 28049.407 |
| e2e_from_send | 482620 | 141.668 | 19.871 | 90.815 | 1889.279 | 23838.719 | 26836.991 |
| producer_lag | 482620 | 6741.11 | 2.301 | 22396.927 | 23494.655 | 23773.183 | 24051.711 |
| publish_call | 684168 | 108.67 | 23.343 | 96.639 | 305.663 | 23740.415 | 24199.167 |
| queue_wait | 482620 | 130.964 | 19.743 | 89.151 | 1366.015 | 23838.719 | 25296.895 |
| broker_dwell | 482620 | 25.671 | 2.171 | 14.471 | 838.655 | 2379.775 | 20217.855 |
| receive_call | 592167 | 37.704 | 17.167 | 93.055 | 315.647 | 468.991 | 518.655 |
| ctx_load | 482620 | 0.002 | 0.002 | 0.003 | 0.01 | 0.048 | 36.895 |
| process | 482620 | 10.678 | 0.001 | 0.002 | 0.014 | 2605.055 | 3000.319 |
| ctx_commit | 482620 | 0.001 | 0.001 | 0.001 | 0.004 | 0.023 | 24.479 |
| ack | 481650 | 0.006 | 0.003 | 0.008 | 0.022 | 0.086 | 49.023 |
| service | 482620 | 10.704 | 0.014 | 0.037 | 0.132 | 2605.055 | 3000.319 |
| e2e_large | 238 | 8544.404 | 3194.879 | 24788.991 | 26836.991 | 28049.407 | 28049.407 |
| ready_wait | 482620 | 48.211 | 1.18 | 16.815 | 104.127 | 16941.055 | 24068.095 |
| blocked | 482620 | 48.114 | 1.18 | 16.815 | 103.551 | 16941.055 | 24068.095 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9460.184 | 28403.7 |
| applied_total | 445752.776 | 643710 |
| bench_cpu_cores | 2.809 | 5.7 |
| bench_cpu_pct | 11.706 | 23.75 |
| bench_gc_gen0 | 2 | 5 |
| bench_gc_gen1 | 0.959 | 3 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 874.333 | 1171.3 |
| bench_mem_mb | 1557.718 | 1752.3 |
| bench_threadpool_queue | 20.612 | 209 |
| bench_threadpool_threads | 32.49 | 40 |
| broker_backlog | 3351.878 | 5554 |
| ctr_eb_mssql_cpu_pct | 521.128 | 733.71 |
| ctr_eb_mssql_mem_mb | 4862.879 | 6732.9 |
| ctr_eb_mysql_cpu_pct | 0.502 | 0.67 |
| ctr_eb_mysql_mem_mb | 6156.763 | 6159.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.323 | 0.66 |
| ctr_eb_postgres_mem_mb | 2438.643 | 2439.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 37.258 | 67.43 |
| in_flight | 170.469 | 1312 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.609 | 2.77 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 900261.408 | 1310488 |
| octopus_missing_reports_total | 2700.653 | 3897 |
| octopus_mqtt_received_total | 134277.571 | 181985 |
| octopus_mqtt_sent_total | 254906.49 | 361691 |
| octopus_publish_retries_total | 8910.898 | 20317 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 9470.386 | 28455.8 |
| produced_total | 445923.245 | 644672 |
| publish_mb_per_s | 32.081 | 86.64 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 102,403 records are Completed |
| record_counts | pass | 102,403 records found, 102,403 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 340 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 684,168 events applied by workers, 684,168 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 5,247 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 104 ms, max 24052 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6015 seq 10: ready 164 ms before start, 164 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7931 seq 13: ready 164 ms before start, 164 ms of it with an idle slot in group 267
- [cross_source_blocking] source 4351 seq 41: ready 163 ms before start, 163 ms of it with an idle slot in group 147
- [cross_source_blocking] source 283 seq 16: ready 164 ms before start, 164 ms of it with an idle slot in group 7
- [cross_source_blocking] source 11611 seq 8: ready 164 ms before start, 164 ms of it with an idle slot in group 377
- [cross_source_blocking] source 1971 seq 12: ready 168 ms before start, 168 ms of it with an idle slot in group 57
- [cross_source_blocking] source 647 seq 19: ready 163 ms before start, 163 ms of it with an idle slot in group 19
- [cross_source_blocking] source 11391 seq 13: ready 163 ms before start, 163 ms of it with an idle slot in group 371
- [cross_source_blocking] source 4427 seq 10: ready 164 ms before start, 164 ms of it with an idle slot in group 152
- [cross_source_blocking] source 2215 seq 35: ready 168 ms before start, 168 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4763 seq 11: ready 164 ms before start, 164 ms of it with an idle slot in group 165
- [cross_source_blocking] source 14875 seq 8: ready 165 ms before start, 165 ms of it with an idle slot in group 467
- [cross_source_blocking] source 14987 seq 18: ready 163 ms before start, 163 ms of it with an idle slot in group 469
- [cross_source_blocking] source 11451 seq 8: ready 164 ms before start, 164 ms of it with an idle slot in group 372
- [cross_source_blocking] source 14867 seq 6: ready 168 ms before start, 168 ms of it with an idle slot in group 466
- [cross_source_blocking] source 1555 seq 21: ready 165 ms before start, 165 ms of it with an idle slot in group 46
- [cross_source_blocking] source 2515 seq 12: ready 168 ms before start, 168 ms of it with an idle slot in group 75
- [cross_source_blocking] source 9323 seq 10: ready 164 ms before start, 164 ms of it with an idle slot in group 322
- [cross_source_blocking] source 14163 seq 5: ready 167 ms before start, 167 ms of it with an idle slot in group 451
- [cross_source_blocking] source 7475 seq 10: ready 165 ms before start, 165 ms of it with an idle slot in group 258

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 296 | 296 | 0 | 3325.951 | 26869.759 | 28049.407 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 67 | 67 | 0 | 2621.439 | 26214.399 | 26214.399 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 77 | 77 | 0 | 2490.367 | 25952.255 | 25952.255 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 141 | 141 | 0 | 2883.583 | 25690.111 | 25952.255 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 545 | 545 | 0 | 24.319 | 25296.895 | 25821.183 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 684 | 684 | 0 | 24.575 | 24903.679 | 26214.399 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 995 | 995 | 0 | 28.671 | 24903.679 | 26083.327 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 626 | 626 | 0 | 30.079 | 24903.679 | 25821.183 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 455 | 455 | 0 | 23.167 | 24641.535 | 25559.039 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 636 | 636 | 0 | 24.191 | 24510.463 | 26607.615 |
| t-038 | 9 | 1 | 55.117 | 8.286 | 2056 | 0 | 340 | 340 | 0 | 23.807 | 24510.463 | 25559.039 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 260 | 260 | 0 | 24.191 | 24510.463 | 25427.967 |
| t-294 | 7 | 1 | 133.744 | 15.435 | 667 | 0 | 680 | 680 | 0 | 23.935 | 24510.463 | 24903.679 |
| t-470 | 16 | 1 | 58.967 | 15.535 | 140 | 0 | 680 | 680 | 0 | 23.551 | 24379.391 | 25559.039 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 281 | 281 | 0 | 20.863 | 24117.247 | 25034.751 |
| t-240 | 8 | 1 | 80.78 | 11.925 | 361 | 0 | 485 | 485 | 0 | 25.343 | 24117.247 | 24510.463 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 719 | 719 | 0 | 23.807 | 24117.247 | 24903.679 |
| t-316 | 19 | 1 | 33.29 | 10.596 | 169 | 0 | 495 | 495 | 0 | 21.631 | 24117.247 | 25165.823 |
| t-493 | 8 | 1 | 106.163 | 15.177 | 1419 | 0 | 673 | 673 | 0 | 25.087 | 24117.247 | 25034.751 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 5558 | 5558 | 0 | 25.599 | 23986.175 | 27131.903 |
| t-108 | 32 | 1 | 72.27 | 39.424 | 694 | 0 | 1648 | 1648 | 0 | 24.191 | 23986.175 | 26607.615 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 295 | 295 | 0 | 21.119 | 23986.175 | 24248.319 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 5210 | 5210 | 0 | 26.623 | 23986.175 | 26869.759 |
| t-247 | 8 | 1 | 30.815 | 4.104 | 506 | 0 | 202 | 202 | 0 | 19.711 | 23986.175 | 23986.175 |
| t-292 | 16 | 1 | 55.425 | 15.986 | 916 | 0 | 681 | 681 | 0 | 21.759 | 23986.175 | 26476.543 |
