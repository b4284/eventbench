# Y2-yb-nocbo-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.86s, closing 8.627s, drain 12.657s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 141507 | 141507 | 1742.312 | 1742.461 | 4877.4 | 0 | 0 | 0 | 64 | 628 | 212 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 10279 | 38852.15 | 41975.807 | 48103.423 | 49283.071 | 50397.183 | 51478.527 |
| e2e_from_send | 10279 | 418.315 | 388.607 | 664.063 | 2652.159 | 4198.399 | 5513.215 |
| producer_lag | 10279 | 38433.91 | 41811.967 | 47611.903 | 48824.319 | 49479.679 | 49741.823 |
| publish_call | 141507 | 791.46 | 445.183 | 2265.087 | 4145.151 | 6602.751 | 6610.943 |
| queue_wait | 10279 | 406.284 | 388.607 | 662.527 | 1723.391 | 4198.399 | 4227.071 |
| broker_dwell | 10279 | 8.655 | 2.913 | 6.175 | 47.199 | 1684.479 | 2357.247 |
| receive_call | 122458 | 158.194 | 121.983 | 373.247 | 483.327 | 498.431 | 556.031 |
| ctx_load | 10279 | 0.002 | 0.001 | 0.004 | 0.007 | 0.036 | 1.127 |
| process | 10279 | 12.015 | 0.001 | 0.003 | 0.018 | 2590.719 | 2908.159 |
| ctx_commit | 10279 | 0.001 | 0.001 | 0.002 | 0.006 | 0.023 | 0.424 |
| ack | 10167 | 0.003 | 0.002 | 0.006 | 0.018 | 0.054 | 0.199 |
| service | 10279 | 12.033 | 0.011 | 0.043 | 0.12 | 2590.719 | 2908.159 |
| e2e_large | 8 | 39680 | 43876.351 | 49479.679 | 51150.847 | 51150.847 | 51150.847 |
| ready_wait | 10279 | 14.554 | 1.828 | 5.099 | 342.527 | 2652.159 | 2654.207 |
| blocked | 10279 | 14.554 | 1.828 | 5.099 | 342.527 | 2652.159 | 2654.207 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 1742.461 | 2813.1 |
| applied_total | 74003.633 | 117537 |
| bench_cpu_cores | 0.605 | 0.86 |
| bench_cpu_pct | 2.522 | 3.58 |
| bench_gc_gen0 | 0.816 | 3 |
| bench_gc_gen1 | 0.408 | 2 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 464.78 | 647.5 |
| bench_mem_mb | 743.18 | 862.9 |
| bench_threadpool_queue | 0.102 | 3 |
| bench_threadpool_threads | 42.469 | 44 |
| broker_backlog | 64940.306 | 103951 |
| ctr_eb_mosquitto_cpu_pct | 10.67 | 17.03 |
| ctr_eb_mosquitto_mem_mb | 5.69 | 6.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.639 | 2.42 |
| ctr_eb_postgres_mem_mb | 2729.247 | 2732.3 |
| ctr_eb_yb_cpu_pct | 1163.374 | 1341.89 |
| ctr_eb_yb_mem_mb | 6618.688 | 7528.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 53.4 | 66.12 |
| in_flight | 22.551 | 49 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.347 | 0.52 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 168781.551 | 275624 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 49357.265 | 76173 |
| octopus_mqtt_sent_total | 65962.49 | 107347 |
| octopus_publish_retries_total | 277 | 277 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 1742.312 | 2822.1 |
| produced_total | 74026.184 | 117572 |
| publish_mb_per_s | 6.022 | 15.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 32,493 records are Completed |
| record_counts | pass | 32,493 records found, 32,493 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 64 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 141,507 events applied by workers, 141,507 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 212 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 343 ms, max 2653 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3877 seq 19: ready 125 ms before start, 125 ms of it with an idle slot in group 131
- [cross_source_blocking] source 10117 seq 30: ready 125 ms before start, 125 ms of it with an idle slot in group 344
- [cross_source_blocking] source 12805 seq 7: ready 2651 ms before start, 2651 ms of it with an idle slot in group 404
- [cross_source_blocking] source 6085 seq 20: ready 2646 ms before start, 2646 ms of it with an idle slot in group 208
- [cross_source_blocking] source 1957 seq 39: ready 2646 ms before start, 2646 ms of it with an idle slot in group 55
- [cross_source_blocking] source 5605 seq 33: ready 2645 ms before start, 2645 ms of it with an idle slot in group 194
- [cross_source_blocking] source 5733 seq 28: ready 2646 ms before start, 2646 ms of it with an idle slot in group 197
- [cross_source_blocking] source 5221 seq 43: ready 2652 ms before start, 2652 ms of it with an idle slot in group 181
- [cross_source_blocking] source 4933 seq 6: ready 2652 ms before start, 2652 ms of it with an idle slot in group 170
- [cross_source_blocking] source 15813 seq 9: ready 2652 ms before start, 2652 ms of it with an idle slot in group 495
- [cross_source_blocking] source 10565 seq 16: ready 2645 ms before start, 2645 ms of it with an idle slot in group 348
- [cross_source_blocking] source 8037 seq 25: ready 2652 ms before start, 2652 ms of it with an idle slot in group 271
- [cross_source_blocking] source 10821 seq 12: ready 2652 ms before start, 2652 ms of it with an idle slot in group 353
- [cross_source_blocking] source 15429 seq 17: ready 2652 ms before start, 2652 ms of it with an idle slot in group 483
- [cross_source_blocking] source 13733 seq 15: ready 2651 ms before start, 2651 ms of it with an idle slot in group 437
- [cross_source_blocking] source 2533 seq 25: ready 2645 ms before start, 2645 ms of it with an idle slot in group 75
- [cross_source_blocking] source 229 seq 13: ready 2652 ms before start, 2652 ms of it with an idle slot in group 5
- [cross_source_blocking] source 11205 seq 16: ready 2653 ms before start, 2653 ms of it with an idle slot in group 365
- [cross_source_blocking] source 11333 seq 31: ready 2651 ms before start, 2651 ms of it with an idle slot in group 369
- [cross_source_blocking] source 14917 seq 45: ready 2651 ms before start, 2651 ms of it with an idle slot in group 468

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-163 | 32 | 1 | 70.698 | 37.497 | 154 | 0 | 340 | 340 | 0 | 43253.759 | 51642.367 | 51642.367 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 69 | 69 | 0 | 47448.063 | 51380.223 | 51380.223 |
| t-261 | 51 | 2 | 73.695 | 62.946 | 2342 | 0 | 533 | 533 | 0 | 41943.039 | 51118.079 | 51118.079 |
| t-274 | 29 | 1 | 69.982 | 33.783 | 1181 | 0 | 293 | 293 | 0 | 38273.023 | 50855.935 | 50855.935 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 613 | 613 | 0 | 44302.335 | 50855.935 | 50855.935 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 372 | 372 | 0 | 44826.623 | 50593.791 | 50593.791 |
| t-379 | 57 | 2 | 58.935 | 55.77 | 591 | 0 | 486 | 486 | 0 | 41943.039 | 50069.503 | 50069.503 |
| t-004 | 38 | 2 | 108.094 | 70.785 | 2178 | 0 | 584 | 584 | 0 | 40108.031 | 49807.359 | 49807.359 |
| t-076 | 23 | 1 | 22.044 | 8.332 | 4045 | 0 | 102 | 102 | 0 | 49545.215 | 49807.359 | 49807.359 |
| t-083 | 58 | 1 | 28.993 | 28.657 | 417 | 0 | 294 | 294 | 0 | 40370.175 | 49807.359 | 49807.359 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 722 | 722 | 0 | 41156.607 | 49807.359 | 49807.359 |
| t-154 | 18 | 1 | 25.802 | 7.641 | 2158 | 0 | 77 | 77 | 0 | 44826.623 | 49807.359 | 49807.359 |
| t-186 | 34 | 2 | 128.851 | 72.677 | 1259 | 0 | 621 | 621 | 0 | 41156.607 | 49807.359 | 49807.359 |
| t-282 | 23 | 1 | 104.692 | 40.289 | 956 | 0 | 338 | 338 | 0 | 44564.479 | 49807.359 | 49807.359 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 60 | 60 | 0 | 44826.623 | 49807.359 | 49807.359 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 352 | 352 | 0 | 47710.207 | 49807.359 | 49807.359 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 788 | 788 | 0 | 42991.615 | 49807.359 | 49807.359 |
| t-398 | 15 | 1 | 48.173 | 12.633 | 482 | 0 | 105 | 105 | 0 | 48234.495 | 49807.359 | 49807.359 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 1550 | 1550 | 0 | 44564.479 | 49807.359 | 49807.359 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 457 | 457 | 0 | 44826.623 | 49807.359 | 49807.359 |
| t-017 | 19 | 1 | 36.957 | 11.447 | 830 | 0 | 106 | 106 | 0 | 47448.063 | 49545.215 | 49545.215 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 230 | 230 | 0 | 42991.615 | 49545.215 | 49545.215 |
| t-050 | 96 | 1 | 13.54 | 21.538 | 130 | 0 | 291 | 291 | 0 | 43253.759 | 49545.215 | 49545.215 |
| t-059 | 8 | 1 | 79.126 | 10.461 | 1449 | 0 | 85 | 85 | 0 | 48234.495 | 49545.215 | 49545.215 |
| t-061 | 148 | 2 | 27.838 | 68.109 | 1274 | 0 | 720 | 720 | 0 | 41680.895 | 49545.215 | 49545.215 |
