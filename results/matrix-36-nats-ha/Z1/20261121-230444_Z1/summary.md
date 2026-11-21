# Z1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.104s, closing 0.623s, drain 2.9s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000520 | 1000520 | 15942.1 | 15905.59 | 40085.5 | 32 | 0 | 0 | 458 | 5162 | 140 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797153 | 74.114 | 6.935 | 12.127 | 1612.799 | 18104.319 | 30031.871 |
| e2e_from_send | 797153 | 71.875 | 6.247 | 10.191 | 1571.839 | 18104.319 | 30031.871 |
| producer_lag | 797153 | 2.239 | 0.786 | 2.693 | 6.015 | 31.583 | 5038.079 |
| publish_call | 1000520 | 8.233 | 6.015 | 11.263 | 64.351 | 203.647 | 5062.655 |
| queue_wait | 797153 | 61.137 | 6.219 | 9.887 | 978.431 | 18104.319 | 30031.871 |
| broker_dwell | 797153 | 54.575 | 0.32 | 1.552 | 963.583 | 18104.319 | 30031.871 |
| receive_call | 961211 | 35.888 | 14.647 | 90.559 | 318.207 | 468.735 | 506.111 |
| ctx_load | 797153 | 0.002 | 0.002 | 0.003 | 0.01 | 0.032 | 21.359 |
| process | 797153 | 10.723 | 0.001 | 0.002 | 0.013 | 2619.391 | 3002.367 |
| ctx_commit | 797153 | 0.002 | 0.001 | 0.001 | 0.003 | 0.019 | 21.711 |
| ack | 795191 | 0.003 | 0.002 | 0.004 | 0.013 | 0.042 | 21.359 |
| service | 797153 | 10.738 | 0.012 | 0.025 | 0.07 | 2619.391 | 3002.367 |
| e2e_large | 356 | 2266.279 | 2254.847 | 3031.039 | 4354.047 | 4767.743 | 4767.743 |
| ready_wait | 797153 | 2.36 | 0.525 | 5.559 | 11.695 | 66.879 | 30031.871 |
| blocked | 797153 | 2.287 | 0.525 | 5.559 | 11.663 | 63.551 | 30031.871 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15905.59 | 16311.2 |
| applied_total | 578258.306 | 959882 |
| bench_cpu_cores | 4.652 | 4.967 |
| bench_cpu_pct | 19.384 | 20.7 |
| bench_gc_gen0 | 2.327 | 4 |
| bench_gc_gen1 | 0.939 | 2 |
| bench_gc_gen2 | 0.469 | 2 |
| bench_gc_heap_mb | 1100.351 | 1341.9 |
| bench_mem_mb | 1706.345 | 1846.9 |
| bench_threadpool_queue | 1.673 | 26 |
| bench_threadpool_threads | 32.245 | 34 |
| broker_backlog | 2045.327 | 3427 |
| ctr_eb_mssql_cpu_pct | 1.962 | 2.18 |
| ctr_eb_mssql_mem_mb | 4900.265 | 4901.9 |
| ctr_eb_mysql_cpu_pct | 0.554 | 0.6 |
| ctr_eb_mysql_mem_mb | 5951.894 | 5952.8 |
| ctr_eb_nats_octopus_2_cpu_pct | 65.771 | 75.29 |
| ctr_eb_nats_octopus_2_mem_mb | 23.241 | 35.9 |
| ctr_eb_nats_octopus_3_cpu_pct | 16.076 | 29.98 |
| ctr_eb_nats_octopus_3_mem_mb | 13.614 | 23.2 |
| ctr_eb_nats_octopus_cpu_pct | 79.96 | 80.74 |
| ctr_eb_nats_octopus_mem_mb | 29.15 | 42.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 6.7 | 6.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.1 | 9.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 10.037 | 10.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 144.77 | 203.07 |
| ctr_eb_postgres_mem_mb | 3977.731 | 4059.8 |
| duplicates_per_s | 0.02 | 1 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 35.797 | 42.7 |
| in_flight | 1047.959 | 2265 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.18 | 1.26 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1146958.102 | 1920522 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 210592.347 | 353888 |
| octopus_mqtt_sent_total | 378450.143 | 641197 |
| octopus_publish_retries_total | 24 | 49 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.1 | 16278.2 |
| produced_total | 579306.265 | 962147 |
| publish_mb_per_s | 51.795 | 74.35 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,520 events applied by workers, 1,000,520 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 140 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 12 ms, max 30025 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2216 seq 38: ready 116 ms before start, 116 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8480 seq 19: ready 116 ms before start, 116 ms of it with an idle slot in group 290
- [cross_source_blocking] source 14724 seq 17: ready 116 ms before start, 116 ms of it with an idle slot in group 464
- [cross_source_blocking] source 14128 seq 20: ready 113 ms before start, 113 ms of it with an idle slot in group 450
- [cross_source_blocking] source 12864 seq 9: ready 116 ms before start, 116 ms of it with an idle slot in group 407
- [cross_source_blocking] source 5614 seq 22: ready 114 ms before start, 114 ms of it with an idle slot in group 194
- [cross_source_blocking] source 2985 seq 16: ready 125 ms before start, 125 ms of it with an idle slot in group 99
- [cross_source_blocking] source 8766 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 305
- [cross_source_blocking] source 1129 seq 14: ready 125 ms before start, 125 ms of it with an idle slot in group 37
- [cross_source_blocking] source 15053 seq 15: ready 125 ms before start, 125 ms of it with an idle slot in group 469
- [cross_source_blocking] source 13769 seq 26: ready 125 ms before start, 125 ms of it with an idle slot in group 438
- [cross_source_blocking] source 15970 seq 15: ready 109 ms before start, 109 ms of it with an idle slot in group 499
- [cross_source_blocking] source 3194 seq 36: ready 109 ms before start, 109 ms of it with an idle slot in group 106
- [cross_source_blocking] source 5641 seq 24: ready 125 ms before start, 125 ms of it with an idle slot in group 194
- [cross_source_blocking] source 4157 seq 13: ready 114 ms before start, 114 ms of it with an idle slot in group 136
- [cross_source_blocking] source 7365 seq 32: ready 104 ms before start, 104 ms of it with an idle slot in group 253
- [cross_source_blocking] source 3458 seq 1: ready 135 ms before start, 135 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3461 seq 1: ready 105 ms before start, 105 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3441 seq 2: ready 131 ms before start, 131 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 103 ms before start, 103 ms of it with an idle slot in group 119

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-203 | 6 | 1 | 35.674 | 3.855 | 2258 | 0 | 234 | 234 | 1 | 7.199 | 27131.903 | 28704.767 |
| t-138 | 9 | 1 | 38.068 | 5.596 | 228 | 0 | 379 | 379 | 1 | 7.519 | 24641.535 | 29491.199 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2400 | 2400 | 1 | 7.167 | 24248.319 | 29622.271 |
| t-253 | 26 | 1 | 91.3 | 40.306 | 1433 | 0 | 2465 | 2465 | 1 | 7.167 | 23724.031 | 30146.559 |
| t-180 | 19 | 1 | 58.767 | 17.705 | 152 | 0 | 1158 | 1158 | 0 | 7.071 | 23068.671 | 30146.559 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 1 | 7.519 | 22937.599 | 30146.559 |
| t-196 | 21 | 1 | 116.998 | 41.352 | 2244 | 0 | 2493 | 2493 | 1 | 7.231 | 22937.599 | 29884.415 |
| t-035 | 22 | 1 | 58.172 | 22.212 | 264 | 0 | 1406 | 1406 | 1 | 6.975 | 21889.023 | 29884.415 |
| t-274 | 29 | 1 | 69.982 | 33.783 | 1181 | 0 | 2116 | 2116 | 1 | 7.103 | 20054.015 | 30015.487 |
| t-007 | 17 | 1 | 63.212 | 17.385 | 140 | 0 | 1125 | 1125 | 1 | 7.071 | 19791.871 | 29884.415 |
| t-433 | 6 | 1 | 26.326 | 2.618 | 184 | 0 | 182 | 182 | 1 | 7.583 | 19791.871 | 27918.335 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 1 | 6.847 | 19529.727 | 29753.343 |
| t-384 | 12 | 1 | 27.672 | 5.489 | 844 | 0 | 359 | 359 | 1 | 6.847 | 19529.727 | 25952.255 |
| t-488 | 26 | 1 | 71.523 | 31.67 | 1017 | 0 | 1945 | 1945 | 1 | 7.039 | 18743.295 | 29884.415 |
| t-055 | 65 | 3 | 125.803 | 130.203 | 359 | 0 | 8049 | 8049 | 1 | 6.943 | 17694.719 | 30146.559 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5724 | 5724 | 1 | 7.199 | 17694.719 | 29753.343 |
| t-473 | 32 | 1 | 52.343 | 28.434 | 4074 | 0 | 1795 | 1795 | 1 | 7.167 | 16056.319 | 29753.343 |
| t-074 | 30 | 1 | 74.208 | 35.909 | 1515 | 0 | 2270 | 2270 | 1 | 7.039 | 15859.711 | 29622.271 |
| t-181 | 35 | 3 | 184.39 | 106.528 | 1367 | 0 | 6377 | 6377 | 1 | 7.167 | 14811.135 | 29753.343 |
| t-129 | 19 | 1 | 112.391 | 37.002 | 623 | 0 | 2223 | 2223 | 1 | 7.167 | 13238.271 | 29360.127 |
| t-476 | 15 | 1 | 47.573 | 13.273 | 432 | 0 | 828 | 828 | 1 | 6.975 | 11468.799 | 28704.767 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 2233 | 2233 | 0 | 7.167 | 11141.119 | 30146.559 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 7.295 | 9502.719 | 30146.559 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14679 | 14679 | 1 | 7.007 | 8847.359 | 30146.559 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 1 | 6.911 | 7995.391 | 29753.343 |
