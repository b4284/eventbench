# Ic-mssql-im — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.331s, closing 3.397s, drain 0.258s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 617566 | 617566 | 9672.004 | 9672.394 | 13224.3 | 0 | 0 | 0 | 310 | 3145 | 13194 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 416508 | 15682.536 | 15638.527 | 22216.703 | 26034.175 | 27869.183 | 30441.471 |
| e2e_from_send | 416508 | 208.238 | 204.415 | 304.383 | 1174.527 | 2859.007 | 4841.471 |
| producer_lag | 416508 | 15474.305 | 15417.343 | 21987.327 | 25755.647 | 27656.191 | 28327.935 |
| publish_call | 617566 | 179.198 | 192.383 | 297.215 | 478.463 | 1573.887 | 1741.823 |
| queue_wait | 416508 | 197.381 | 204.031 | 302.335 | 527.359 | 2258.943 | 4493.311 |
| broker_dwell | 416508 | 17.345 | 4.623 | 12.663 | 169.343 | 2103.295 | 4276.223 |
| receive_call | 500753 | 69.735 | 41.823 | 172.799 | 400.639 | 486.399 | 541.695 |
| ctx_load | 416508 | 0.002 | 0.002 | 0.003 | 0.007 | 0.035 | 30.079 |
| process | 416508 | 10.832 | 0.001 | 0.002 | 0.011 | 2611.199 | 3002.367 |
| ctx_commit | 416508 | 0.001 | 0.001 | 0.001 | 0.004 | 0.02 | 27.471 |
| ack | 405322 | 0.004 | 0.002 | 0.004 | 0.012 | 0.048 | 42.175 |
| service | 416508 | 10.857 | 0.013 | 0.034 | 0.117 | 2611.199 | 3002.367 |
| e2e_large | 208 | 17069.588 | 16637.951 | 23658.495 | 27656.191 | 28065.791 | 28065.791 |
| ready_wait | 416508 | 12.159 | 3.147 | 10.559 | 248.831 | 400.895 | 540.159 |
| blocked | 416508 | 12.158 | 3.147 | 10.559 | 248.831 | 400.895 | 540.159 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9672.394 | 13099.9 |
| applied_total | 346343.347 | 579984 |
| bench_cpu_cores | 1.615 | 2.323 |
| bench_cpu_pct | 6.728 | 9.68 |
| bench_gc_gen0 | 1.959 | 4 |
| bench_gc_gen1 | 0.959 | 2 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 933.361 | 1163.9 |
| bench_mem_mb | 1484.927 | 1667.9 |
| bench_threadpool_queue | 0.184 | 4 |
| bench_threadpool_threads | 43.184 | 44 |
| broker_backlog | 1124.939 | 1901 |
| ctr_eb_mosquitto_cpu_pct | 27.854 | 33.54 |
| ctr_eb_mosquitto_mem_mb | 6.449 | 8.8 |
| ctr_eb_mssql_cpu_pct | 676.493 | 792.02 |
| ctr_eb_mssql_mem_mb | 7508.943 | 7672.3 |
| ctr_eb_mysql_cpu_pct | 0.63 | 0.7 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.752 | 1.43 |
| ctr_eb_postgres_mem_mb | 3120.439 | 3123.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.649 | 59.8 |
| in_flight | 276.633 | 852 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.243 | 1.61 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 693398.776 | 1170701 |
| octopus_missing_reports_total | 1228.612 | 2423 |
| octopus_mqtt_received_total | 76853.531 | 119478 |
| octopus_mqtt_sent_total | 171818.755 | 287724 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 9672.004 | 13231.8 |
| produced_total | 346619.98 | 580180 |
| publish_mb_per_s | 34.021 | 54.9 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 94,648 records are Completed |
| record_counts | pass | 94,648 records found, 94,648 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 310 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 617,566 events applied by workers, 617,566 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 13,194 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 249 ms, max 540 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11060 seq 9: ready 194 ms before start, 194 ms of it with an idle slot in group 361
- [cross_source_blocking] source 13428 seq 9: ready 193 ms before start, 193 ms of it with an idle slot in group 429
- [cross_source_blocking] source 10132 seq 15: ready 194 ms before start, 194 ms of it with an idle slot in group 344
- [cross_source_blocking] source 3732 seq 11: ready 194 ms before start, 194 ms of it with an idle slot in group 126
- [cross_source_blocking] source 14996 seq 23: ready 193 ms before start, 193 ms of it with an idle slot in group 469
- [cross_source_blocking] source 1012 seq 16: ready 192 ms before start, 192 ms of it with an idle slot in group 31
- [cross_source_blocking] source 10836 seq 5: ready 193 ms before start, 193 ms of it with an idle slot in group 354
- [cross_source_blocking] source 9652 seq 12: ready 195 ms before start, 195 ms of it with an idle slot in group 334
- [cross_source_blocking] source 15636 seq 24: ready 192 ms before start, 192 ms of it with an idle slot in group 487
- [cross_source_blocking] source 12724 seq 12: ready 196 ms before start, 196 ms of it with an idle slot in group 401
- [cross_source_blocking] source 8884 seq 13: ready 196 ms before start, 196 ms of it with an idle slot in group 306
- [cross_source_blocking] source 3252 seq 14: ready 196 ms before start, 196 ms of it with an idle slot in group 108
- [cross_source_blocking] source 7988 seq 15: ready 196 ms before start, 196 ms of it with an idle slot in group 269
- [cross_source_blocking] source 4116 seq 7: ready 196 ms before start, 196 ms of it with an idle slot in group 136
- [cross_source_blocking] source 3796 seq 14: ready 196 ms before start, 196 ms of it with an idle slot in group 128
- [cross_source_blocking] source 14036 seq 4: ready 196 ms before start, 196 ms of it with an idle slot in group 446
- [cross_source_blocking] source 6580 seq 14: ready 196 ms before start, 196 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3220 seq 9: ready 196 ms before start, 196 ms of it with an idle slot in group 107
- [cross_source_blocking] source 11988 seq 17: ready 189 ms before start, 189 ms of it with an idle slot in group 383
- [cross_source_blocking] source 14900 seq 15: ready 194 ms before start, 194 ms of it with an idle slot in group 468

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 73 | 73 | 0 | 16056.319 | 28049.407 | 28049.407 |
| t-151 | 6 | 1 | 29.549 | 3.093 | 577 | 0 | 118 | 118 | 0 | 14024.703 | 28049.407 | 28180.479 |
| t-021 | 19 | 1 | 20.159 | 6.705 | 3252 | 0 | 249 | 249 | 0 | 17694.719 | 27787.263 | 28049.407 |
| t-190 | 6 | 1 | 85.928 | 8.971 | 241 | 0 | 326 | 326 | 0 | 15990.783 | 27787.263 | 28442.623 |
| t-240 | 8 | 1 | 80.78 | 11.925 | 361 | 0 | 433 | 433 | 0 | 16318.463 | 27656.191 | 28311.551 |
| t-475 | 6 | 1 | 16.127 | 1.527 | 3771 | 0 | 60 | 60 | 0 | 15073.279 | 27656.191 | 27656.191 |
| t-024 | 14 | 1 | 41.39 | 9.449 | 1743 | 0 | 380 | 380 | 0 | 15859.711 | 27525.119 | 28049.407 |
| t-193 | 19 | 1 | 15.713 | 5.079 | 1805 | 0 | 217 | 217 | 0 | 16121.855 | 27525.119 | 27525.119 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 126 | 126 | 0 | 15400.959 | 27525.119 | 27656.191 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 465 | 465 | 0 | 15532.031 | 27394.047 | 27918.335 |
| t-100 | 33 | 1 | 50.561 | 26.936 | 2626 | 0 | 1106 | 1106 | 0 | 16252.927 | 27394.047 | 28311.551 |
| t-220 | 19 | 1 | 52.897 | 17.707 | 506 | 0 | 679 | 679 | 0 | 15597.567 | 27394.047 | 28311.551 |
| t-015 | 9 | 1 | 50.663 | 7.506 | 3462 | 0 | 283 | 283 | 0 | 16252.927 | 27262.975 | 27787.263 |
| t-137 | 10 | 1 | 27.033 | 4.549 | 1569 | 0 | 203 | 203 | 0 | 15335.423 | 27262.975 | 27787.263 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 252 | 252 | 0 | 15532.031 | 27262.975 | 27918.335 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 526 | 526 | 0 | 16187.391 | 27262.975 | 27787.263 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1146 | 1146 | 0 | 15400.959 | 27131.903 | 27787.263 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1046 | 1046 | 0 | 16252.927 | 27131.903 | 27918.335 |
| t-068 | 21 | 1 | 24.454 | 8.398 | 3531 | 0 | 350 | 350 | 0 | 15597.567 | 27131.903 | 27787.263 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 883 | 883 | 0 | 15925.247 | 27131.903 | 28049.407 |
| t-139 | 16 | 1 | 21.826 | 6.166 | 1367 | 0 | 286 | 286 | 0 | 15466.495 | 27131.903 | 27918.335 |
| t-141 | 7 | 1 | 136.582 | 16.007 | 134 | 0 | 557 | 557 | 0 | 16908.287 | 27131.903 | 28049.407 |
| t-149 | 13 | 1 | 42.874 | 8.748 | 3034 | 0 | 318 | 318 | 0 | 16515.071 | 27131.903 | 27787.263 |
| t-171 | 14 | 1 | 119.591 | 28.366 | 142 | 0 | 1018 | 1018 | 0 | 16056.319 | 27131.903 | 28049.407 |
| t-173 | 12 | 1 | 52.259 | 11.008 | 3493 | 0 | 444 | 444 | 0 | 16187.391 | 27131.903 | 27918.335 |
