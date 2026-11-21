# Z3 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.105s, closing 0.548s, drain 9.499s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000539 | 1000539 | 15942.969 | 15905.049 | 40004.1 | 47 | 0 | 0 | 458 | 5163 | 59 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797174 | 75.516 | 7.243 | 14.055 | 1616.895 | 18595.839 | 30031.871 |
| e2e_from_send | 797174 | 72.95 | 6.335 | 11.807 | 1571.839 | 18595.839 | 30031.871 |
| producer_lag | 797174 | 2.566 | 0.92 | 3.159 | 7.039 | 48.415 | 5029.887 |
| publish_call | 1000539 | 7.71 | 5.959 | 11.943 | 54.431 | 103.679 | 5058.559 |
| queue_wait | 797174 | 62.21 | 6.307 | 11.575 | 976.895 | 18563.071 | 30031.871 |
| broker_dwell | 797174 | 55.994 | 0.949 | 2.187 | 958.975 | 18563.071 | 30015.487 |
| receive_call | 960256 | 36.117 | 15.055 | 90.751 | 317.695 | 468.991 | 504.831 |
| ctx_load | 797174 | 0.002 | 0.002 | 0.003 | 0.01 | 0.03 | 13.639 |
| process | 797174 | 10.725 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797174 | 0.002 | 0.001 | 0.001 | 0.003 | 0.018 | 20.607 |
| ack | 794862 | 0.003 | 0.002 | 0.004 | 0.014 | 0.039 | 13.295 |
| service | 797174 | 10.74 | 0.012 | 0.025 | 0.069 | 2617.343 | 3002.367 |
| e2e_large | 356 | 2264.895 | 2246.655 | 3026.943 | 4349.951 | 4775.935 | 4775.935 |
| ready_wait | 797174 | 1.479 | 0.681 | 2.919 | 11.135 | 50.175 | 5058.559 |
| blocked | 797174 | 1.406 | 0.681 | 2.917 | 11.119 | 47.295 | 5058.559 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15905.049 | 16309.9 |
| applied_total | 578235.939 | 959871 |
| bench_cpu_cores | 4.513 | 5.016 |
| bench_cpu_pct | 18.805 | 20.9 |
| bench_gc_gen0 | 2.306 | 4 |
| bench_gc_gen1 | 0.939 | 2 |
| bench_gc_gen2 | 0.469 | 1 |
| bench_gc_heap_mb | 1150.42 | 1539.6 |
| bench_mem_mb | 1781.859 | 1994.1 |
| bench_threadpool_queue | 4.02 | 33 |
| bench_threadpool_threads | 38.612 | 44 |
| broker_backlog | 2080.143 | 3342 |
| ctr_eb_mssql_cpu_pct | 1.935 | 2.1 |
| ctr_eb_mssql_mem_mb | 4892.569 | 4894.3 |
| ctr_eb_mysql_cpu_pct | 0.557 | 0.58 |
| ctr_eb_mysql_mem_mb | 5944.553 | 5946.2 |
| ctr_eb_nats_octopus_2_cpu_pct | 37.019 | 69.94 |
| ctr_eb_nats_octopus_2_mem_mb | 18.002 | 36.8 |
| ctr_eb_nats_octopus_3_cpu_pct | 57.777 | 65.64 |
| ctr_eb_nats_octopus_3_mem_mb | 21.024 | 30.6 |
| ctr_eb_nats_octopus_cpu_pct | 70.584 | 74.89 |
| ctr_eb_nats_octopus_mem_mb | 25.35 | 32.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 6.7 | 6.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 9.1 | 9.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 10.4 | 10.4 |
| ctr_eb_postgres_cpu_pct | 134.358 | 169.45 |
| ctr_eb_postgres_mem_mb | 3690.849 | 3788.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 36.274 | 40.98 |
| in_flight | 1046.939 | 2293 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.132 | 1.2 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1145485.429 | 1918135 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 190882.796 | 334629 |
| octopus_mqtt_sent_total | 363260.551 | 625615 |
| octopus_publish_retries_total | 25.469 | 52 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 15942.969 | 16324.9 |
| produced_total | 579282.878 | 962164 |
| publish_mb_per_s | 51.799 | 74.31 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,874 records are Completed |
| record_counts | pass | 138,874 records found, 138,874 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,539 events applied by workers, 1,000,539 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 59 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 11 ms, max 5058 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9694 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 337
- [cross_source_blocking] source 3666 seq 13: ready 103 ms before start, 103 ms of it with an idle slot in group 125
- [cross_source_blocking] source 3605 seq 18: ready 117 ms before start, 117 ms of it with an idle slot in group 122
- [cross_source_blocking] source 11441 seq 6: ready 116 ms before start, 116 ms of it with an idle slot in group 372
- [cross_source_blocking] source 4401 seq 5: ready 113 ms before start, 113 ms of it with an idle slot in group 151
- [cross_source_blocking] source 4085 seq 11: ready 117 ms before start, 117 ms of it with an idle slot in group 136
- [cross_source_blocking] source 673 seq 20: ready 117 ms before start, 117 ms of it with an idle slot in group 20
- [cross_source_blocking] source 10429 seq 16: ready 117 ms before start, 117 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3441 seq 2: ready 107 ms before start, 107 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 2: ready 103 ms before start, 103 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3434 seq 1: ready 120 ms before start, 120 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3484 seq 2: ready 104 ms before start, 104 ms of it with an idle slot in group 119
- [cross_source_blocking] source 3435 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 119
- [cross_source_blocking] source 15081 seq 45: ready 435 ms before start, 435 ms of it with an idle slot in group 470
- [cross_source_blocking] source 15070 seq 29: ready 400 ms before start, 400 ms of it with an idle slot in group 470
- [cross_source_blocking] source 15076 seq 29: ready 397 ms before start, 397 ms of it with an idle slot in group 470
- [cross_source_blocking] source 15071 seq 25: ready 238 ms before start, 238 ms of it with an idle slot in group 470
- [cross_source_blocking] source 15066 seq 36: ready 109 ms before start, 109 ms of it with an idle slot in group 470
- [cross_source_blocking] source 15080 seq 35: ready 112 ms before start, 112 ms of it with an idle slot in group 470
- [cross_source_blocking] source 3496 seq 69: ready 5055 ms before start, 5055 ms of it with an idle slot in group 120

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-123 | 16 | 1 | 83.276 | 20.7 | 4028 | 0 | 1278 | 1278 | 2 | 7.615 | 25559.039 | 29884.415 |
| t-064 | 11 | 1 | 57.27 | 10.782 | 263 | 0 | 728 | 728 | 1 | 7.455 | 25034.751 | 30146.559 |
| t-223 | 13 | 1 | 133.399 | 28.216 | 2372 | 0 | 1722 | 1722 | 1 | 7.679 | 24379.391 | 29884.415 |
| t-305 | 8 | 1 | 77.153 | 10.084 | 436 | 0 | 607 | 607 | 1 | 7.647 | 23986.175 | 29884.415 |
| t-470 | 16 | 1 | 58.967 | 15.535 | 140 | 0 | 996 | 996 | 1 | 7.327 | 23855.103 | 29229.055 |
| t-390 | 10 | 1 | 44.751 | 7.306 | 167 | 0 | 462 | 462 | 1 | 7.359 | 23068.671 | 30015.487 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1688 | 1688 | 2 | 7.775 | 22806.527 | 30015.487 |
| t-304 | 19 | 1 | 129.878 | 40.323 | 238 | 0 | 2460 | 2460 | 1 | 7.487 | 22282.239 | 29884.415 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8301 | 8301 | 2 | 7.679 | 21889.023 | 29884.415 |
| t-296 | 22 | 1 | 50.816 | 18.334 | 970 | 0 | 1165 | 1165 | 1 | 7.135 | 21364.735 | 29884.415 |
| t-339 | 17 | 1 | 15.431 | 4.427 | 134 | 0 | 323 | 323 | 1 | 7.327 | 20447.231 | 29097.983 |
| t-212 | 29 | 1 | 62.738 | 30.352 | 296 | 0 | 1930 | 1930 | 1 | 7.359 | 19791.871 | 29753.343 |
| t-399 | 34 | 1 | 41.726 | 23.034 | 397 | 0 | 1485 | 1485 | 2 | 7.007 | 19791.871 | 30015.487 |
| t-181 | 35 | 3 | 184.39 | 106.528 | 1367 | 0 | 6379 | 6379 | 1 | 7.487 | 19660.799 | 30015.487 |
| t-396 | 17 | 1 | 51.403 | 14.826 | 210 | 0 | 888 | 888 | 1 | 6.943 | 19398.655 | 28704.767 |
| t-358 | 22 | 1 | 33.422 | 12.024 | 310 | 0 | 799 | 799 | 1 | 7.199 | 18874.367 | 27787.263 |
| t-004 | 38 | 2 | 108.094 | 70.785 | 2178 | 0 | 4347 | 4347 | 1 | 7.391 | 18743.295 | 29753.343 |
| t-373 | 31 | 1 | 71.355 | 37.966 | 2878 | 0 | 2310 | 2310 | 1 | 7.391 | 18087.935 | 29491.199 |
| t-281 | 10 | 1 | 43.191 | 7.593 | 2160 | 0 | 456 | 456 | 1 | 7.519 | 17694.719 | 29884.415 |
| t-030 | 19 | 1 | 30.981 | 9.922 | 748 | 0 | 670 | 670 | 1 | 7.551 | 16908.287 | 29622.271 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 6149 | 6149 | 1 | 7.583 | 16121.855 | 30015.487 |
| t-428 | 37 | 1 | 74.414 | 46.006 | 159 | 0 | 2903 | 2903 | 1 | 7.263 | 16121.855 | 28573.695 |
| t-497 | 57 | 1 | 28.238 | 26.176 | 912 | 0 | 1728 | 1728 | 1 | 7.231 | 13500.415 | 29753.343 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3533 | 3533 | 1 | 7.231 | 12517.375 | 30015.487 |
| t-189 | 59 | 2 | 55.494 | 53.722 | 1601 | 0 | 3428 | 3428 | 1 | 7.327 | 10616.831 | 30015.487 |
