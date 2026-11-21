# M3-mssql-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.384s, closing 1.758s, drain 4.975s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 878859 | 878859 | 13431.347 | 13435.62 | 25339.1 | 0 | 0 | 0 | 413 | 4524 | 18091 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 676194 | 1154.435 | 33.727 | 4227.071 | 8396.799 | 9076.735 | 11976.703 |
| e2e_from_send | 676194 | 107.662 | 27.615 | 121.727 | 2199.551 | 5074.943 | 7925.759 |
| producer_lag | 676194 | 1046.773 | 2.637 | 4069.375 | 8224.767 | 8863.743 | 9273.343 |
| publish_call | 878859 | 71.733 | 26.319 | 110.399 | 482.559 | 4960.255 | 5132.287 |
| queue_wait | 676194 | 96.869 | 27.375 | 117.311 | 1593.343 | 5074.943 | 7557.119 |
| broker_dwell | 676194 | 33.626 | 5.503 | 33.471 | 717.311 | 3422.207 | 7483.391 |
| receive_call | 750344 | 40.95 | 19.231 | 100.351 | 329.215 | 471.807 | 530.431 |
| ctx_load | 676194 | 0.003 | 0.002 | 0.004 | 0.012 | 0.08 | 45.119 |
| process | 676194 | 10.76 | 0.001 | 0.002 | 0.018 | 2615.295 | 3006.463 |
| ctx_commit | 676194 | 0.002 | 0.001 | 0.002 | 0.005 | 0.044 | 47.935 |
| ack | 664650 | 0.005 | 0.002 | 0.005 | 0.02 | 0.129 | 51.967 |
| service | 676194 | 10.793 | 0.014 | 0.039 | 0.287 | 2615.295 | 3049.471 |
| e2e_large | 311 | 3647.124 | 2895.871 | 6635.519 | 10878.975 | 11788.287 | 11788.287 |
| ready_wait | 676194 | 17.984 | 3.037 | 29.999 | 158.079 | 3905.535 | 5124.095 |
| blocked | 676194 | 17.874 | 3.037 | 29.967 | 157.567 | 3905.535 | 5124.095 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 13435.62 | 19781.1 |
| applied_total | 552933.939 | 838273 |
| bench_cpu_cores | 3.312 | 4.825 |
| bench_cpu_pct | 13.801 | 20.1 |
| bench_gc_gen0 | 2.286 | 4 |
| bench_gc_gen1 | 0.959 | 2 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 1070.796 | 1372.5 |
| bench_mem_mb | 1829.686 | 1987.2 |
| bench_threadpool_queue | 36.367 | 703 |
| bench_threadpool_threads | 40.571 | 44 |
| broker_backlog | 18017.633 | 62456 |
| ctr_eb_mosquitto_cpu_pct | 49.89 | 66.48 |
| ctr_eb_mosquitto_mem_mb | 10.245 | 25.7 |
| ctr_eb_mssql_cpu_pct | 706.951 | 921.1 |
| ctr_eb_mssql_mem_mb | 7583.641 | 7595.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.633 | 1.82 |
| ctr_eb_postgres_mem_mb | 2497.484 | 2500.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 55.008 | 80.51 |
| in_flight | 553.286 | 2496 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.349 | 2.83 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1065534.204 | 1665285 |
| octopus_missing_reports_total | 1316.714 | 1761 |
| octopus_mqtt_received_total | 146526.714 | 222880 |
| octopus_mqtt_sent_total | 275896.286 | 431129 |
| octopus_publish_retries_total | 18.898 | 463 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 13431.347 | 19938.9 |
| produced_total | 553487.224 | 838629 |
| publish_mb_per_s | 44.46 | 80.81 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 124,945 records are Completed |
| record_counts | pass | 124,945 records found, 124,945 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 413 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 878,859 events applied by workers, 878,859 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,091 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 158 ms, max 5121 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12565 seq 2: ready 328 ms before start, 328 ms of it with an idle slot in group 397
- [cross_source_blocking] source 9086 seq 51: ready 223 ms before start, 223 ms of it with an idle slot in group 314
- [cross_source_blocking] source 5337 seq 20: ready 209 ms before start, 209 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5801 seq 5: ready 210 ms before start, 210 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5315 seq 17: ready 225 ms before start, 225 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5432 seq 29: ready 211 ms before start, 211 ms of it with an idle slot in group 186
- [cross_source_blocking] source 5784 seq 10: ready 223 ms before start, 223 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5448 seq 24: ready 223 ms before start, 223 ms of it with an idle slot in group 186
- [cross_source_blocking] source 14054 seq 13: ready 223 ms before start, 223 ms of it with an idle slot in group 447
- [cross_source_blocking] source 9959 seq 3: ready 210 ms before start, 210 ms of it with an idle slot in group 341
- [cross_source_blocking] source 8024 seq 29: ready 223 ms before start, 223 ms of it with an idle slot in group 271
- [cross_source_blocking] source 14971 seq 27: ready 210 ms before start, 210 ms of it with an idle slot in group 469
- [cross_source_blocking] source 7137 seq 13: ready 211 ms before start, 211 ms of it with an idle slot in group 242
- [cross_source_blocking] source 10204 seq 20: ready 211 ms before start, 211 ms of it with an idle slot in group 344
- [cross_source_blocking] source 9669 seq 12: ready 211 ms before start, 211 ms of it with an idle slot in group 334
- [cross_source_blocking] source 11336 seq 25: ready 211 ms before start, 211 ms of it with an idle slot in group 369
- [cross_source_blocking] source 10272 seq 14: ready 211 ms before start, 211 ms of it with an idle slot in group 344
- [cross_source_blocking] source 11884 seq 27: ready 211 ms before start, 211 ms of it with an idle slot in group 383
- [cross_source_blocking] source 2303 seq 8: ready 225 ms before start, 225 ms of it with an idle slot in group 66
- [cross_source_blocking] source 2024 seq 7: ready 211 ms before start, 211 ms of it with an idle slot in group 61

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 80 | 80 | 0 | 2621.439 | 11796.479 | 11796.479 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 361 | 361 | 0 | 2801.663 | 10878.975 | 11534.335 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 161 | 161 | 0 | 2506.751 | 10682.367 | 10682.367 |
| t-112 | 6 | 1 | 57.527 | 5.908 | 774 | 0 | 321 | 321 | 0 | 35.327 | 9502.719 | 10092.543 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 90 | 90 | 0 | 2703.359 | 9109.503 | 9633.791 |
| t-198 | 6 | 1 | 16.318 | 1.427 | 180 | 0 | 70 | 70 | 0 | 25.087 | 9043.967 | 9043.967 |
| t-063 | 8 | 1 | 24.376 | 3.378 | 1296 | 0 | 188 | 188 | 0 | 36.863 | 8978.431 | 8978.431 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3372 | 3372 | 0 | 37.375 | 8912.895 | 10944.511 |
| t-363 | 19 | 1 | 115.496 | 37.476 | 639 | 0 | 1955 | 1955 | 0 | 37.119 | 8781.823 | 10682.367 |
| t-058 | 23 | 1 | 41.959 | 15.86 | 768 | 0 | 878 | 878 | 0 | 32.511 | 8716.287 | 9109.503 |
| t-141 | 7 | 1 | 136.582 | 16.007 | 134 | 0 | 814 | 814 | 0 | 34.303 | 8716.287 | 11010.047 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 794 | 794 | 0 | 35.583 | 8716.287 | 10158.079 |
| t-278 | 11 | 1 | 92.207 | 16.698 | 1899 | 0 | 890 | 890 | 0 | 32.639 | 8716.287 | 9043.967 |
| t-401 | 11 | 1 | 37.414 | 7.028 | 360 | 0 | 403 | 403 | 0 | 31.231 | 8716.287 | 9830.399 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1473 | 1473 | 0 | 33.535 | 8650.751 | 10420.223 |
| t-046 | 24 | 1 | 105.058 | 42.361 | 3235 | 0 | 2279 | 2279 | 0 | 34.303 | 8650.751 | 10158.079 |
| t-086 | 6 | 1 | 53.883 | 5.925 | 464 | 0 | 334 | 334 | 0 | 35.583 | 8650.751 | 11993.087 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 352 | 352 | 0 | 33.023 | 8650.751 | 9633.791 |
| t-098 | 6 | 1 | 33.911 | 3.131 | 192 | 0 | 195 | 195 | 0 | 36.863 | 8650.751 | 8912.895 |
| t-107 | 19 | 1 | 51.135 | 15.879 | 453 | 0 | 834 | 834 | 0 | 34.303 | 8650.751 | 9109.503 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 766 | 766 | 0 | 31.999 | 8650.751 | 11075.583 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 762 | 762 | 0 | 36.863 | 8650.751 | 9175.039 |
| t-202 | 8 | 1 | 56.524 | 7.268 | 1480 | 0 | 410 | 410 | 0 | 33.535 | 8650.751 | 9043.967 |
| t-244 | 11 | 1 | 39.68 | 7.406 | 1151 | 0 | 415 | 415 | 0 | 30.335 | 8650.751 | 9043.967 |
| t-249 | 15 | 1 | 29.615 | 7.653 | 691 | 0 | 420 | 420 | 0 | 30.207 | 8650.751 | 9240.575 |
