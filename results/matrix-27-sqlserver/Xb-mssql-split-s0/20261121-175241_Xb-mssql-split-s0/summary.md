# Xb-mssql-split-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.592s, closing 1.227s, drain 12.832s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 494903 | 494903 | 7876.555 | 7876.088 | 13576.5 | 0 | 0 | 0 | 47 | 2424 | 1490 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 393923 | 103.356 | 32.831 | 126.399 | 1887.231 | 4028.415 | 7274.495 |
| e2e_from_send | 393923 | 75.105 | 29.503 | 97.919 | 1402.879 | 2863.103 | 5705.727 |
| producer_lag | 393923 | 28.251 | 1.77 | 7.971 | 847.871 | 3178.495 | 6561.791 |
| publish_call | 494903 | 55.118 | 26.095 | 105.663 | 396.031 | 2418.687 | 6295.551 |
| queue_wait | 393923 | 64.868 | 29.343 | 95.487 | 932.351 | 2510.847 | 5107.711 |
| broker_dwell | 393923 | 27.134 | 6.987 | 21.071 | 761.855 | 2373.631 | 4734.975 |
| receive_call | 438499 | 40.58 | 20.207 | 98.047 | 315.647 | 469.247 | 513.535 |
| ctx_load | 393923 | 0.004 | 0.002 | 0.004 | 0.017 | 0.116 | 25.967 |
| process | 393923 | 10.199 | 0.001 | 0.003 | 0.024 | 2609.151 | 3002.367 |
| ctx_commit | 393923 | 0.002 | 0.001 | 0.002 | 0.006 | 0.056 | 23.327 |
| ack | 388302 | 0.006 | 0.003 | 0.006 | 0.028 | 0.214 | 24.175 |
| service | 393923 | 10.237 | 0.014 | 0.048 | 0.572 | 2609.151 | 3002.367 |
| e2e_large | 39 | 2794.194 | 2871.295 | 3516.415 | 5709.823 | 5709.823 | 5709.823 |
| ready_wait | 393923 | 7.741 | 3.377 | 15.447 | 58.623 | 224.767 | 5107.711 |
| blocked | 393923 | 7.701 | 3.377 | 15.447 | 58.495 | 218.495 | 5107.711 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7876.088 | 9307.8 |
| applied_total | 283079.837 | 472053 |
| bench_cpu_cores | 2.068 | 2.353 |
| bench_cpu_pct | 8.617 | 9.8 |
| bench_gc_gen0 | 1.551 | 4 |
| bench_gc_gen1 | 0.551 | 2 |
| bench_gc_gen2 | 0.265 | 2 |
| bench_gc_heap_mb | 483.978 | 692.4 |
| bench_mem_mb | 820.071 | 995.1 |
| bench_threadpool_queue | 16.143 | 99 |
| bench_threadpool_threads | 35.347 | 44 |
| broker_backlog | 98910.816 | 235307 |
| ctr_eb_mosquitto_cpu_pct | 81.413 | 86.63 |
| ctr_eb_mosquitto_mem_mb | 16.612 | 29.4 |
| ctr_eb_mssql_cpu_pct | 1130.87 | 1180.56 |
| ctr_eb_mssql_mem_mb | 7479.918 | 7488.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.028 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 15.008 | 15.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.027 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.027 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.03 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 15.016 | 15.2 |
| ctr_eb_postgres_cpu_pct | 1.055 | 8.04 |
| ctr_eb_postgres_mem_mb | 4491.31 | 4529 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.087 | 81.57 |
| in_flight | 250.714 | 394 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 3.161 | 3.51 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 548214.857 | 927810 |
| octopus_missing_reports_total | 1354 | 1380 |
| octopus_mqtt_received_total | 151994.49 | 247571 |
| octopus_mqtt_sent_total | 229254.408 | 388747 |
| octopus_publish_retries_total | 41.143 | 84 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 7876.555 | 9463.9 |
| produced_total | 283330.551 | 472355 |
| publish_mb_per_s | 15.856 | 30.43 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 68,954 records are Completed |
| record_counts | pass | 68,954 records found, 68,954 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 47 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 494,903 events applied by workers, 494,903 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,490 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 58 ms, max 5104 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1816 seq 15: ready 122 ms before start, 122 ms of it with an idle slot in group 52
- [cross_source_blocking] source 7230 seq 61: ready 378 ms before start, 378 ms of it with an idle slot in group 246
- [cross_source_blocking] source 14794 seq 7: ready 374 ms before start, 374 ms of it with an idle slot in group 464
- [cross_source_blocking] source 1763 seq 3: ready 374 ms before start, 374 ms of it with an idle slot in group 50
- [cross_source_blocking] source 4659 seq 4: ready 374 ms before start, 374 ms of it with an idle slot in group 162
- [cross_source_blocking] source 10497 seq 22: ready 378 ms before start, 378 ms of it with an idle slot in group 348
- [cross_source_blocking] source 1283 seq 10: ready 186 ms before start, 186 ms of it with an idle slot in group 40
- [cross_source_blocking] source 14921 seq 32: ready 186 ms before start, 186 ms of it with an idle slot in group 468
- [cross_source_blocking] source 10599 seq 23: ready 175 ms before start, 175 ms of it with an idle slot in group 348
- [cross_source_blocking] source 4644 seq 7: ready 368 ms before start, 368 ms of it with an idle slot in group 162
- [cross_source_blocking] source 15856 seq 40: ready 398 ms before start, 398 ms of it with an idle slot in group 496
- [cross_source_blocking] source 11439 seq 14: ready 107 ms before start, 107 ms of it with an idle slot in group 372
- [cross_source_blocking] source 11440 seq 11: ready 119 ms before start, 119 ms of it with an idle slot in group 372
- [cross_source_blocking] source 11661 seq 34: ready 122 ms before start, 122 ms of it with an idle slot in group 378
- [cross_source_blocking] source 1285 seq 11: ready 107 ms before start, 107 ms of it with an idle slot in group 40
- [cross_source_blocking] source 8237 seq 24: ready 130 ms before start, 130 ms of it with an idle slot in group 278
- [cross_source_blocking] source 10040 seq 59: ready 115 ms before start, 115 ms of it with an idle slot in group 342
- [cross_source_blocking] source 7249 seq 62: ready 107 ms before start, 107 ms of it with an idle slot in group 246
- [cross_source_blocking] source 10557 seq 30: ready 129 ms before start, 129 ms of it with an idle slot in group 348
- [cross_source_blocking] source 9109 seq 57: ready 130 ms before start, 130 ms of it with an idle slot in group 314

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-232 | 6 | 1 | 70.326 | 7.149 | 805 | 0 | 412 | 412 | 0 | 31.743 | 4456.447 | 6029.311 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3931 | 3931 | 0 | 35.327 | 4325.375 | 7274.495 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5544 | 5544 | 0 | 36.095 | 4227.071 | 6553.599 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 88 | 88 | 0 | 2129.919 | 4112.383 | 5734.399 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 446 | 446 | 0 | 30.847 | 4046.847 | 4784.127 |
| t-190 | 6 | 1 | 85.928 | 8.971 | 241 | 0 | 555 | 555 | 0 | 36.095 | 3932.159 | 6520.831 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 2056 | 2056 | 0 | 34.559 | 3702.783 | 6586.367 |
| t-226 | 6 | 1 | 109.061 | 11.534 | 623 | 0 | 687 | 687 | 0 | 34.303 | 3440.639 | 5111.807 |
| t-176 | 11 | 1 | 51.203 | 8.885 | 1581 | 0 | 572 | 572 | 0 | 34.815 | 3276.799 | 5373.951 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 404 | 404 | 0 | 31.487 | 2932.735 | 5931.007 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 842 | 842 | 0 | 33.279 | 2654.207 | 4751.359 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 3869 | 3869 | 0 | 35.327 | 2654.207 | 4751.359 |
| t-074 | 30 | 1 | 74.208 | 35.909 | 1515 | 0 | 2268 | 2268 | 0 | 32.383 | 2572.287 | 6062.079 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 382 | 382 | 0 | 33.279 | 2572.287 | 2981.887 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 901 | 901 | 0 | 33.279 | 2539.519 | 5472.255 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 152 | 152 | 0 | 33.791 | 2523.135 | 2555.903 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14937 | 14937 | 0 | 34.303 | 2523.135 | 6488.063 |
| t-160 | 57 | 2 | 71.075 | 67.452 | 548 | 0 | 4338 | 4338 | 0 | 32.767 | 2506.751 | 6324.223 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 575 | 575 | 0 | 35.583 | 2490.367 | 3014.655 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1828 | 1828 | 0 | 31.999 | 2473.983 | 5111.807 |
| t-454 | 7 | 1 | 166.996 | 19.279 | 266 | 0 | 1190 | 1190 | 0 | 34.559 | 2441.215 | 6029.311 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 919 | 919 | 0 | 31.871 | 2392.063 | 2981.887 |
| t-368 | 36 | 1 | 22.946 | 13.605 | 2666 | 0 | 914 | 914 | 0 | 31.999 | 2392.063 | 5275.647 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1057 | 1057 | 0 | 31.103 | 2359.295 | 2998.271 |
| t-398 | 15 | 1 | 48.173 | 12.633 | 482 | 0 | 794 | 794 | 0 | 31.487 | 2359.295 | 6520.831 |
