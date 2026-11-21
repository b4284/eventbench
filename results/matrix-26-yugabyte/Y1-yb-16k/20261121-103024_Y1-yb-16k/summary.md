# Y1-yb-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 63.909s, closing 10.862s, drain 0.169s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 152262 | 152262 | 2007.077 | 2214.438 | 12267.4 | 0 | 0 | 0 | 73 | 691 | 307 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 3280 | 45255.475 | 45678.591 | 48922.623 | 51183.615 | 53116.927 | 53247.999 |
| e2e_from_send | 3280 | 497.293 | 212.863 | 1713.151 | 2228.223 | 2957.311 | 3796.991 |
| producer_lag | 3280 | 44757.671 | 45350.911 | 48070.655 | 50102.271 | 52953.087 | 53215.231 |
| publish_call | 152262 | 733.81 | 378.111 | 1539.071 | 3637.247 | 36569.087 | 36569.087 |
| queue_wait | 3280 | 485.068 | 212.735 | 1712.127 | 2201.599 | 2443.263 | 2574.335 |
| broker_dwell | 3280 | 41.227 | 4.087 | 109.119 | 499.199 | 2312.191 | 2551.807 |
| receive_call | 108839 | 144.839 | 100.031 | 384.511 | 488.191 | 500.223 | 504.319 |
| ctx_load | 3280 | 0.003 | 0.002 | 0.005 | 0.01 | 0.079 | 0.549 |
| process | 3280 | 12.201 | 0.001 | 0.004 | 0.021 | 2664.447 | 2869.247 |
| ctx_commit | 3280 | 0.002 | 0.001 | 0.004 | 0.01 | 0.034 | 0.181 |
| ack | 3246 | 0.004 | 0.003 | 0.008 | 0.03 | 0.145 | 0.184 |
| service | 3280 | 12.225 | 0.014 | 0.058 | 0.201 | 2664.447 | 2869.247 |
| ready_wait | 3280 | 32.794 | 3.041 | 93.375 | 402.687 | 1086.463 | 1086.463 |
| blocked | 3280 | 32.794 | 3.041 | 93.375 | 402.687 | 1086.463 | 1086.463 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2214.438 | 12267.4 |
| applied_total | 72130.038 | 129111 |
| bench_cpu_cores | 0.684 | 1.189 |
| bench_cpu_pct | 2.852 | 4.95 |
| bench_gc_gen0 | 0.923 | 3 |
| bench_gc_gen1 | 0.5 | 2 |
| bench_gc_gen2 | 0.365 | 2 |
| bench_gc_heap_mb | 474.627 | 670.7 |
| bench_mem_mb | 778.996 | 944.4 |
| bench_threadpool_queue | 3.173 | 152 |
| bench_threadpool_threads | 33.75 | 44 |
| broker_backlog | 19514.558 | 30961 |
| ctr_eb_mosquitto_cpu_pct | 12.982 | 21.49 |
| ctr_eb_mosquitto_mem_mb | 5.762 | 6.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.3 | 7.3 |
| ctr_eb_postgres_cpu_pct | 0.863 | 2.58 |
| ctr_eb_postgres_mem_mb | 2737.467 | 2742.8 |
| ctr_eb_yb_cpu_pct | 1527.092 | 1597.2 |
| ctr_eb_yb_mem_mb | 6824.652 | 8475.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 69.328 | 75.81 |
| in_flight | 4708.154 | 17769 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.319 | 0.54 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 170982 | 301134 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 42980.885 | 70064 |
| octopus_mqtt_sent_total | 59839.519 | 109498 |
| octopus_publish_retries_total | 1716.462 | 2542 |
| octopus_publish_stuck | 39.904 | 415 |
| produced_per_s | 2007.077 | 3447.4 |
| produced_total | 76838.192 | 129112 |
| publish_mb_per_s | 7.183 | 17.5 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 34,273 records are Completed |
| record_counts | pass | 34,273 records found, 34,273 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 73 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 152,262 events applied by workers, 152,262 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 307 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 403 ms, max 1086 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1688 seq 4: ready 135 ms before start, 135 ms of it with an idle slot in group 50
- [cross_source_blocking] source 8856 seq 19: ready 165 ms before start, 165 ms of it with an idle slot in group 306
- [cross_source_blocking] source 4216 seq 14: ready 290 ms before start, 290 ms of it with an idle slot in group 140
- [cross_source_blocking] source 3000 seq 13: ready 232 ms before start, 232 ms of it with an idle slot in group 100
- [cross_source_blocking] source 11480 seq 7: ready 156 ms before start, 156 ms of it with an idle slot in group 373
- [cross_source_blocking] source 2456 seq 14: ready 119 ms before start, 119 ms of it with an idle slot in group 72
- [cross_source_blocking] source 15128 seq 5: ready 244 ms before start, 244 ms of it with an idle slot in group 472
- [cross_source_blocking] source 15480 seq 13: ready 120 ms before start, 120 ms of it with an idle slot in group 483
- [cross_source_blocking] source 14680 seq 5: ready 117 ms before start, 117 ms of it with an idle slot in group 464
- [cross_source_blocking] source 8024 seq 29: ready 135 ms before start, 135 ms of it with an idle slot in group 271
- [cross_source_blocking] source 2488 seq 13: ready 201 ms before start, 201 ms of it with an idle slot in group 74
- [cross_source_blocking] source 8056 seq 26: ready 217 ms before start, 217 ms of it with an idle slot in group 271
- [cross_source_blocking] source 3256 seq 8: ready 167 ms before start, 167 ms of it with an idle slot in group 108
- [cross_source_blocking] source 12856 seq 3: ready 215 ms before start, 215 ms of it with an idle slot in group 407
- [cross_source_blocking] source 1816 seq 9: ready 165 ms before start, 165 ms of it with an idle slot in group 52
- [cross_source_blocking] source 3576 seq 24: ready 244 ms before start, 244 ms of it with an idle slot in group 121
- [cross_source_blocking] source 15768 seq 7: ready 149 ms before start, 149 ms of it with an idle slot in group 494
- [cross_source_blocking] source 13304 seq 19: ready 194 ms before start, 194 ms of it with an idle slot in group 425
- [cross_source_blocking] source 12824 seq 9: ready 298 ms before start, 298 ms of it with an idle slot in group 406
- [cross_source_blocking] source 15064 seq 16: ready 220 ms before start, 220 ms of it with an idle slot in group 469

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 2285 | 2285 | 0 | 47972.351 | 53477.375 | 53477.375 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 1180 | 1180 | 0 | 47448.063 | 53215.231 | 53215.231 |
| t-468 | 61 | 4 | 157.515 | 157.545 | 1645 | 0 | 1415 | 1415 | 0 | 47448.063 | 53215.231 | 53215.231 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 1067 | 1067 | 0 | 46399.487 | 52428.799 | 52428.799 |
| t-261 | 51 | 2 | 73.695 | 62.946 | 2342 | 0 | 558 | 558 | 0 | 46399.487 | 51904.511 | 51904.511 |
| t-197 | 42 | 2 | 128.441 | 87.485 | 725 | 0 | 732 | 732 | 0 | 45613.055 | 51642.367 | 51642.367 |
| t-237 | 74 | 2 | 44.672 | 55.97 | 2944 | 0 | 600 | 600 | 0 | 44040.191 | 51642.367 | 51642.367 |
| t-270 | 21 | 1 | 53.579 | 18.123 | 556 | 0 | 167 | 167 | 0 | 51642.367 | 51642.367 | 51642.367 |
| t-275 | 9 | 1 | 87.516 | 13.03 | 166 | 0 | 126 | 126 | 0 | 51642.367 | 51642.367 | 51642.367 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 455 | 455 | 0 | 42729.471 | 51380.223 | 51380.223 |
| t-055 | 65 | 3 | 125.803 | 130.203 | 359 | 0 | 1128 | 1128 | 0 | 45875.199 | 51380.223 | 51380.223 |
| t-123 | 16 | 1 | 83.276 | 20.7 | 4028 | 0 | 169 | 169 | 0 | 45875.199 | 51380.223 | 51380.223 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 209 | 209 | 0 | 43778.047 | 51380.223 | 51380.223 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 509 | 509 | 0 | 45613.055 | 51380.223 | 51380.223 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 1993 | 1993 | 0 | 45875.199 | 51380.223 | 51380.223 |
| t-160 | 57 | 2 | 71.075 | 67.452 | 548 | 0 | 633 | 633 | 0 | 46661.631 | 51380.223 | 51380.223 |
| t-184 | 20 | 1 | 98.647 | 32.052 | 161 | 0 | 308 | 308 | 0 | 43515.903 | 51380.223 | 51380.223 |
| t-221 | 70 | 1 | 24.765 | 28.365 | 2834 | 0 | 340 | 340 | 0 | 47710.207 | 51380.223 | 51380.223 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 1675 | 1675 | 0 | 45613.055 | 51380.223 | 51380.223 |
| t-269 | 24 | 1 | 88.126 | 37.126 | 531 | 0 | 341 | 341 | 0 | 44040.191 | 51380.223 | 51380.223 |
| t-279 | 18 | 1 | 44.014 | 12.994 | 196 | 0 | 137 | 137 | 0 | 47972.351 | 51380.223 | 51380.223 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 71 | 71 | 0 | 41418.751 | 51380.223 | 51380.223 |
| t-321 | 10 | 1 | 79.218 | 14.205 | 204 | 0 | 117 | 117 | 0 | 46923.775 | 51380.223 | 51380.223 |
| t-337 | 40 | 1 | 60.247 | 39.019 | 195 | 0 | 414 | 414 | 0 | 45613.055 | 51380.223 | 51380.223 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 2940 | 2940 | 0 | 45875.199 | 51380.223 | 51380.223 |
