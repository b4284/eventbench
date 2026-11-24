# pg-mem-16k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.106s, closing 0.588s, drain 2.201s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000512 | 1000512 | 15945.531 | 15945.696 | 38409.3 | 0 | 0 | 0 | 458 | 458 | 659 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797145 | 19.464 | 8.279 | 32.511 | 259.711 | 493.055 | 6365.183 |
| e2e_from_send | 797145 | 15.26 | 7.191 | 29.215 | 130.175 | 317.183 | 6365.183 |
| producer_lag | 797145 | 4.204 | 0.853 | 2.897 | 131.583 | 311.551 | 599.551 |
| publish_call | 1000512 | 13.715 | 7.043 | 27.919 | 110.015 | 257.535 | 388.607 |
| queue_wait | 797145 | 14.35 | 7.175 | 29.199 | 128.255 | 294.655 | 3721.215 |
| broker_dwell | 797145 | 1.709 | 0.006 | 2.213 | 44.319 | 78.527 | 3713.023 |
| receive_call | 928389 | 37.878 | 15.543 | 98.303 | 319.231 | 469.503 | 505.855 |
| ctx_load | 797145 | 0.002 | 0.002 | 0.003 | 0.01 | 0.03 | 21.343 |
| process | 797145 | 0.893 | 0.001 | 0.002 | 0.006 | 0.036 | 3000.319 |
| ctx_commit | 797145 | 0.002 | 0.001 | 0.001 | 0.003 | 0.018 | 18.175 |
| ack | 796413 | 0.004 | 0.003 | 0.007 | 0.018 | 0.05 | 18.767 |
| service | 797145 | 0.91 | 0.012 | 0.028 | 0.063 | 0.27 | 3000.319 |
| e2e_large | 356 | 2230.864 | 2162.687 | 3014.655 | 4411.391 | 6365.183 | 6365.183 |
| ready_wait | 797145 | 2.196 | 0.408 | 3.817 | 43.103 | 97.215 | 1300.479 |
| blocked | 797145 | 2.189 | 0.408 | 3.817 | 43.071 | 97.087 | 316.671 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15945.696 | 20165.9 |
| applied_total | 578947.796 | 962086 |
| bench_cpu_cores | 4.97 | 5.493 |
| bench_cpu_pct | 20.71 | 22.89 |
| bench_gc_gen0 | 2.939 | 5 |
| bench_gc_gen1 | 1.286 | 2 |
| bench_gc_gen2 | 0.735 | 2 |
| bench_gc_heap_mb | 954.429 | 1267.6 |
| bench_mem_mb | 1545.698 | 1717 |
| bench_threadpool_queue | 4.592 | 30 |
| bench_threadpool_threads | 33.898 | 40 |
| broker_backlog | 1429.143 | 5158 |
| ctr_eb_mssql_cpu_pct | 20.84 | 111.75 |
| ctr_eb_mssql_mem_mb | 7095.01 | 7410 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.01 | 0.01 |
| ctr_eb_pgbouncer_mem_mb | 11.3 | 11.3 |
| ctr_eb_postgres_cpu_pct | 151.169 | 209.1 |
| ctr_eb_postgres_mem_mb | 3589.627 | 3675.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 34.739 | 45.15 |
| in_flight | 71.286 | 871 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.174 | 1.39 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1145059.571 | 1915797 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 179669.224 | 306686 |
| octopus_mqtt_sent_total | 331392.245 | 562738 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15945.531 | 20810.5 |
| produced_total | 579019.082 | 962100 |
| publish_mb_per_s | 51.964 | 72.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,512 events applied by workers, 1,000,512 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 659 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 43 ms, max 317 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6591 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 222
- [cross_source_blocking] source 304 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 8
- [cross_source_blocking] source 2396 seq 11: ready 107 ms before start, 107 ms of it with an idle slot in group 70
- [cross_source_blocking] source 352 seq 10: ready 107 ms before start, 107 ms of it with an idle slot in group 10
- [cross_source_blocking] source 15400 seq 11: ready 103 ms before start, 103 ms of it with an idle slot in group 483
- [cross_source_blocking] source 9129 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 315
- [cross_source_blocking] source 11391 seq 13: ready 135 ms before start, 135 ms of it with an idle slot in group 371
- [cross_source_blocking] source 15649 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 488
- [cross_source_blocking] source 6359 seq 3: ready 105 ms before start, 105 ms of it with an idle slot in group 217
- [cross_source_blocking] source 6036 seq 8: ready 107 ms before start, 107 ms of it with an idle slot in group 206
- [cross_source_blocking] source 4309 seq 34: ready 105 ms before start, 105 ms of it with an idle slot in group 147
- [cross_source_blocking] source 7919 seq 23: ready 138 ms before start, 138 ms of it with an idle slot in group 267
- [cross_source_blocking] source 7220 seq 44: ready 106 ms before start, 106 ms of it with an idle slot in group 246
- [cross_source_blocking] source 10361 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10359 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 347
- [cross_source_blocking] source 6191 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 211
- [cross_source_blocking] source 4783 seq 12: ready 106 ms before start, 106 ms of it with an idle slot in group 165
- [cross_source_blocking] source 11691 seq 13: ready 109 ms before start, 109 ms of it with an idle slot in group 379
- [cross_source_blocking] source 10059 seq 8: ready 106 ms before start, 106 ms of it with an idle slot in group 343
- [cross_source_blocking] source 2825 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 88

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1810.431 | 4423.679 | 6389.759 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 1875.967 | 3883.007 | 4751.359 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1654.783 | 3309.567 | 3440.639 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1638.399 | 3047.423 | 4587.519 |
| t-318 | 7 | 1 | 39.39 | 4.804 | 686 | 0 | 313 | 313 | 0 | 8.383 | 348.159 | 380.927 |
| t-078 | 6 | 1 | 21.855 | 2.121 | 386 | 0 | 141 | 141 | 0 | 9.343 | 342.015 | 362.495 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 438 | 438 | 0 | 8.703 | 342.015 | 374.783 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 7.967 | 342.015 | 434.175 |
| t-386 | 56 | 1 | 15.231 | 14.027 | 3094 | 0 | 995 | 995 | 0 | 7.999 | 342.015 | 438.271 |
| t-134 | 21 | 1 | 24.302 | 8.325 | 3368 | 0 | 579 | 579 | 0 | 8.511 | 339.967 | 473.087 |
| t-485 | 12 | 1 | 39.718 | 7.685 | 880 | 0 | 470 | 470 | 0 | 7.999 | 339.967 | 438.271 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 411 | 411 | 0 | 8.575 | 331.775 | 485.375 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 911 | 911 | 0 | 8.191 | 321.535 | 675.839 |
| t-116 | 24 | 1 | 65.702 | 25.166 | 3734 | 0 | 1566 | 1566 | 0 | 8.063 | 319.487 | 503.807 |
| t-185 | 12 | 1 | 38.22 | 7.517 | 438 | 0 | 483 | 483 | 0 | 8.575 | 319.487 | 528.383 |
| t-324 | 15 | 1 | 27.309 | 6.345 | 131 | 0 | 416 | 416 | 0 | 8.383 | 319.487 | 395.263 |
| t-199 | 24 | 1 | 15.247 | 6.464 | 251 | 0 | 468 | 468 | 0 | 8.031 | 317.439 | 473.087 |
| t-238 | 13 | 1 | 70.713 | 15.247 | 2335 | 0 | 929 | 929 | 0 | 8.575 | 317.439 | 499.711 |
| t-352 | 16 | 1 | 129.471 | 34.033 | 2088 | 0 | 2153 | 2153 | 0 | 8.447 | 317.439 | 499.711 |
| t-240 | 8 | 1 | 80.78 | 11.925 | 361 | 0 | 736 | 736 | 0 | 8.063 | 315.391 | 540.671 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 8.319 | 313.343 | 430.079 |
| t-297 | 14 | 1 | 47.725 | 10.711 | 1702 | 0 | 669 | 669 | 0 | 8.319 | 313.343 | 540.671 |
| t-309 | 56 | 1 | 27.034 | 24.933 | 3528 | 0 | 1696 | 1696 | 0 | 8.255 | 311.295 | 569.343 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 6148 | 6148 | 0 | 8.319 | 311.295 | 565.247 |
| t-489 | 6 | 1 | 173.223 | 17.681 | 1030 | 0 | 1052 | 1052 | 0 | 8.255 | 311.295 | 585.727 |
