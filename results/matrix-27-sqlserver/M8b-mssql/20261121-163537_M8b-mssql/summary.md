# M8b-mssql — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 72.572s, closing 4.312s, drain 14.635s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 923182 | 923182 | 11597.556 | 11610.208 | 21639.9 | 0 | 0 | 0 | 429 | 4784 | 8875 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 720313 | 1236.087 | 35.967 | 4259.839 | 7700.479 | 8921.087 | 55672.831 |
| e2e_from_send | 720313 | 99.555 | 27.167 | 115.839 | 1990.655 | 4493.311 | 55672.831 |
| producer_lag | 720313 | 1136.533 | 2.899 | 4120.575 | 7544.831 | 8454.143 | 31096.831 |
| publish_call | 923182 | 71.415 | 28.191 | 112.575 | 492.287 | 4452.351 | 31178.751 |
| queue_wait | 720313 | 88.706 | 26.943 | 113.471 | 1413.119 | 4489.215 | 54591.487 |
| broker_dwell | 720313 | 29.236 | 4.187 | 21.887 | 781.823 | 2533.375 | 54296.575 |
| receive_call | 793873 | 42.094 | 19.903 | 103.551 | 332.287 | 472.831 | 511.743 |
| ctx_load | 720313 | 0.003 | 0.002 | 0.003 | 0.011 | 0.064 | 48.063 |
| process | 720313 | 10.819 | 0.001 | 0.002 | 0.015 | 2617.343 | 3002.367 |
| ctx_commit | 720313 | 0.002 | 0.001 | 0.001 | 0.004 | 0.03 | 27.135 |
| ack | 707366 | 0.004 | 0.002 | 0.005 | 0.017 | 0.095 | 48.095 |
| service | 720313 | 10.848 | 0.013 | 0.035 | 0.207 | 2617.343 | 3002.367 |
| e2e_large | 327 | 7435.882 | 3500.031 | 17809.407 | 44695.551 | 55672.831 | 55672.831 |
| ready_wait | 720313 | 11.594 | 2.571 | 16.751 | 111.551 | 564.735 | 26886.143 |
| blocked | 720313 | 11.489 | 2.571 | 16.735 | 110.527 | 544.255 | 26886.143 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11610.208 | 20286.5 |
| applied_total | 621071.098 | 887290 |
| bench_cpu_cores | 2.629 | 4.568 |
| bench_cpu_pct | 10.955 | 19.04 |
| bench_gc_gen0 | 1.984 | 5 |
| bench_gc_gen1 | 0.836 | 2 |
| bench_gc_gen2 | 0.459 | 2 |
| bench_gc_heap_mb | 1114.502 | 1403.6 |
| bench_mem_mb | 1829.102 | 2003 |
| bench_threadpool_queue | 5.344 | 72 |
| bench_threadpool_threads | 41.967 | 44 |
| broker_backlog | 74476.689 | 189910 |
| ctr_eb_mosquitto_cpu_pct | 41.952 | 64.31 |
| ctr_eb_mosquitto_mem_mb | 7.402 | 10.8 |
| ctr_eb_mssql_cpu_pct | 640.887 | 947.41 |
| ctr_eb_mssql_mem_mb | 7509.48 | 7520.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.62 | 1.99 |
| ctr_eb_postgres_mem_mb | 2514.615 | 2517.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 47.262 | 73.59 |
| in_flight | 404.754 | 1256 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.997 | 2.69 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1221462.672 | 1821277 |
| octopus_missing_reports_total | 3042.672 | 3264 |
| octopus_mqtt_received_total | 173955.754 | 254690 |
| octopus_mqtt_sent_total | 324691.689 | 489796 |
| octopus_publish_retries_total | 28.852 | 126 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 11597.556 | 20548.5 |
| produced_total | 621475.852 | 887313 |
| publish_mb_per_s | 38.089 | 75.72 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 129,898 records are Completed |
| record_counts | pass | 129,898 records found, 129,898 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 429 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 923,182 events applied by workers, 923,182 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 8,875 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 111 ms, max 26882 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3575 seq 29: ready 208 ms before start, 208 ms of it with an idle slot in group 121
- [cross_source_blocking] source 3989 seq 24: ready 349 ms before start, 349 ms of it with an idle slot in group 132
- [cross_source_blocking] source 4901 seq 12: ready 348 ms before start, 348 ms of it with an idle slot in group 169
- [cross_source_blocking] source 7781 seq 16: ready 348 ms before start, 348 ms of it with an idle slot in group 263
- [cross_source_blocking] source 6493 seq 4: ready 347 ms before start, 347 ms of it with an idle slot in group 221
- [cross_source_blocking] source 3653 seq 4: ready 349 ms before start, 349 ms of it with an idle slot in group 124
- [cross_source_blocking] source 8349 seq 11: ready 347 ms before start, 347 ms of it with an idle slot in group 283
- [cross_source_blocking] source 3333 seq 13: ready 349 ms before start, 349 ms of it with an idle slot in group 114
- [cross_source_blocking] source 13725 seq 6: ready 347 ms before start, 347 ms of it with an idle slot in group 436
- [cross_source_blocking] source 11861 seq 9: ready 349 ms before start, 349 ms of it with an idle slot in group 383
- [cross_source_blocking] source 10229 seq 24: ready 349 ms before start, 349 ms of it with an idle slot in group 344
- [cross_source_blocking] source 2021 seq 15: ready 349 ms before start, 349 ms of it with an idle slot in group 60
- [cross_source_blocking] source 1285 seq 6: ready 349 ms before start, 349 ms of it with an idle slot in group 40
- [cross_source_blocking] source 11445 seq 11: ready 349 ms before start, 349 ms of it with an idle slot in group 372
- [cross_source_blocking] source 2453 seq 13: ready 349 ms before start, 349 ms of it with an idle slot in group 72
- [cross_source_blocking] source 12565 seq 2: ready 349 ms before start, 349 ms of it with an idle slot in group 397
- [cross_source_blocking] source 5653 seq 16: ready 349 ms before start, 349 ms of it with an idle slot in group 194
- [cross_source_blocking] source 3541 seq 21: ready 349 ms before start, 349 ms of it with an idle slot in group 121
- [cross_source_blocking] source 11733 seq 15: ready 349 ms before start, 349 ms of it with an idle slot in group 380
- [cross_source_blocking] source 9557 seq 20: ready 348 ms before start, 348 ms of it with an idle slot in group 332
- [cross_source_blocking] source 2237 seq 50: ready 347 ms before start, 347 ms of it with an idle slot in group 65

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 372 | 372 | 0 | 3948.543 | 45613.055 | 55836.671 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 565 | 565 | 0 | 44.031 | 28835.839 | 30146.559 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 644 | 644 | 0 | 34.559 | 26869.759 | 29753.343 |
| t-413 | 19 | 1 | 86.21 | 28.489 | 3585 | 0 | 1647 | 1647 | 0 | 40.703 | 24510.463 | 30670.847 |
| t-415 | 14 | 1 | 85.928 | 19.862 | 2972 | 0 | 1134 | 1134 | 0 | 40.959 | 22806.527 | 31195.135 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1353 | 1353 | 0 | 43.519 | 22413.311 | 30801.919 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 83 | 83 | 0 | 2736.127 | 10092.543 | 10092.543 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2523.135 | 9764.863 | 13041.663 |
| t-124 | 11 | 1 | 18.487 | 3.524 | 1576 | 0 | 213 | 213 | 0 | 32.639 | 8978.431 | 9109.503 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 839 | 839 | 0 | 41.215 | 8585.215 | 9240.575 |
| t-318 | 7 | 1 | 39.39 | 4.804 | 686 | 0 | 274 | 274 | 0 | 31.615 | 8519.679 | 8585.215 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 5611 | 5611 | 0 | 38.911 | 8519.679 | 31195.135 |
| t-390 | 10 | 1 | 44.751 | 7.306 | 167 | 0 | 437 | 437 | 0 | 28.031 | 8519.679 | 9175.039 |
| t-008 | 11 | 1 | 91.742 | 16.542 | 279 | 0 | 901 | 901 | 0 | 37.631 | 8454.143 | 11206.655 |
| t-289 | 6 | 1 | 83.389 | 8.934 | 1711 | 0 | 533 | 533 | 0 | 36.607 | 8454.143 | 9240.575 |
| t-062 | 18 | 1 | 38.881 | 11.922 | 2061 | 0 | 715 | 715 | 0 | 36.095 | 8355.839 | 10354.687 |
| t-430 | 6 | 1 | 29.555 | 3.204 | 1163 | 0 | 189 | 189 | 0 | 29.055 | 8355.839 | 8454.143 |
| t-015 | 9 | 1 | 50.663 | 7.506 | 3462 | 0 | 447 | 447 | 0 | 36.607 | 8323.071 | 8519.679 |
| t-133 | 6 | 1 | 34.333 | 3.323 | 273 | 0 | 192 | 192 | 0 | 37.887 | 8323.071 | 8388.607 |
| t-174 | 6 | 1 | 43.117 | 4.836 | 3366 | 0 | 277 | 277 | 0 | 39.935 | 8323.071 | 8454.143 |
| t-303 | 32 | 1 | 49.301 | 26.54 | 659 | 0 | 1554 | 1554 | 0 | 36.607 | 8323.071 | 30277.631 |
| t-442 | 6 | 1 | 22.328 | 2.338 | 577 | 0 | 138 | 138 | 0 | 35.839 | 8323.071 | 9109.503 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 3437 | 3437 | 0 | 40.959 | 8323.071 | 10485.759 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 845 | 845 | 0 | 35.839 | 8290.303 | 8978.431 |
| t-234 | 21 | 1 | 72.935 | 24.845 | 141 | 0 | 1389 | 1389 | 0 | 30.975 | 8257.535 | 9240.575 |
