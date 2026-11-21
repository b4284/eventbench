# M6-bounded-delete — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 81.806s, closing 1.717s, drain 1.542s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 930196 | 930196 | 10195.61 | 10201.566 | 24874.9 | 0 | 0 | 0 | 433 | 4815 | 25253 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 727227 | 148.453 | 30.559 | 174.719 | 4489.215 | 4988.927 | 26525.695 |
| e2e_from_send | 727227 | 89.718 | 27.327 | 114.943 | 1656.831 | 4751.359 | 15024.127 |
| producer_lag | 727227 | 58.734 | 1.767 | 20.559 | 4263.935 | 4534.271 | 26509.311 |
| publish_call | 930196 | 53.62 | 23.455 | 84.671 | 390.911 | 4636.671 | 26574.847 |
| queue_wait | 727227 | 78.864 | 27.167 | 110.207 | 1048.575 | 4747.263 | 13680.639 |
| broker_dwell | 727227 | 33.628 | 5.815 | 39.167 | 684.031 | 2400.255 | 8708.095 |
| receive_call | 804587 | 41.499 | 18.447 | 103.679 | 338.431 | 475.135 | 544.255 |
| ctx_load | 727227 | 0.003 | 0.002 | 0.004 | 0.014 | 0.09 | 76.287 |
| process | 727227 | 10.817 | 0.001 | 0.003 | 0.021 | 2621.439 | 3028.991 |
| ctx_commit | 727227 | 0.002 | 0.001 | 0.001 | 0.005 | 0.039 | 31.439 |
| ack | 718588 | 0.005 | 0.002 | 0.005 | 0.021 | 0.137 | 52.255 |
| service | 727227 | 10.855 | 0.013 | 0.039 | 0.404 | 2621.439 | 3028.991 |
| e2e_large | 331 | 2878.275 | 2752.511 | 3885.055 | 7319.551 | 15032.319 | 15032.319 |
| ready_wait | 727227 | 19.221 | 3.135 | 32.287 | 213.247 | 636.415 | 13680.639 |
| blocked | 727227 | 19.14 | 3.135 | 32.271 | 213.119 | 622.591 | 13680.639 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10201.566 | 18087.1 |
| applied_total | 669801.871 | 894379 |
| bench_cpu_cores | 2.702 | 4.916 |
| bench_cpu_pct | 11.258 | 20.48 |
| bench_gc_gen0 | 1.8 | 5 |
| bench_gc_gen1 | 0.771 | 3 |
| bench_gc_gen2 | 0.386 | 2 |
| bench_gc_heap_mb | 1054.181 | 1331.7 |
| bench_mem_mb | 1852.201 | 2017.8 |
| bench_threadpool_queue | 20.771 | 150 |
| bench_threadpool_threads | 37.886 | 43 |
| broker_backlog | 43670.471 | 98921 |
| ctr_eb_mosquitto_cpu_pct | 41.495 | 65.59 |
| ctr_eb_mosquitto_mem_mb | 8.101 | 20.9 |
| ctr_eb_mssql_cpu_pct | 669.067 | 984.05 |
| ctr_eb_mssql_mem_mb | 7382.959 | 7391 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 7.5 | 7.5 |
| ctr_eb_postgres_cpu_pct | 0.632 | 1.85 |
| ctr_eb_postgres_mem_mb | 2513.19 | 2516.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 48.739 | 78.96 |
| in_flight | 373.029 | 2650 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.81 | 2.74 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1330306 | 1841684 |
| octopus_missing_reports_total | 2967.629 | 3159 |
| octopus_mqtt_received_total | 204389.9 | 295055 |
| octopus_mqtt_sent_total | 361606.029 | 516548 |
| octopus_publish_retries_total | 150.143 | 654 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10195.61 | 18120.1 |
| produced_total | 670174.9 | 894360 |
| publish_mb_per_s | 33.506 | 74.02 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 130,796 records are Completed |
| record_counts | pass | 130,796 records found, 130,796 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 433 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 930,196 events applied by workers, 930,196 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 25,253 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 213 ms, max 13678 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11665 seq 19: ready 188 ms before start, 188 ms of it with an idle slot in group 378
- [cross_source_blocking] source 6653 seq 9: ready 183 ms before start, 183 ms of it with an idle slot in group 224
- [cross_source_blocking] source 10489 seq 15: ready 183 ms before start, 183 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7361 seq 14: ready 186 ms before start, 186 ms of it with an idle slot in group 253
- [cross_source_blocking] source 9473 seq 4: ready 186 ms before start, 186 ms of it with an idle slot in group 328
- [cross_source_blocking] source 2237 seq 50: ready 183 ms before start, 183 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8349 seq 11: ready 183 ms before start, 183 ms of it with an idle slot in group 283
- [cross_source_blocking] source 15646 seq 14: ready 240 ms before start, 240 ms of it with an idle slot in group 488
- [cross_source_blocking] source 11782 seq 9: ready 240 ms before start, 240 ms of it with an idle slot in group 382
- [cross_source_blocking] source 10994 seq 10: ready 240 ms before start, 240 ms of it with an idle slot in group 359
- [cross_source_blocking] source 12007 seq 22: ready 389 ms before start, 389 ms of it with an idle slot in group 383
- [cross_source_blocking] source 3556 seq 23: ready 386 ms before start, 386 ms of it with an idle slot in group 121
- [cross_source_blocking] source 7804 seq 18: ready 385 ms before start, 385 ms of it with an idle slot in group 264
- [cross_source_blocking] source 4623 seq 14: ready 393 ms before start, 393 ms of it with an idle slot in group 161
- [cross_source_blocking] source 7207 seq 23: ready 389 ms before start, 389 ms of it with an idle slot in group 245
- [cross_source_blocking] source 47 seq 8: ready 389 ms before start, 389 ms of it with an idle slot in group 1
- [cross_source_blocking] source 4012 seq 7: ready 386 ms before start, 386 ms of it with an idle slot in group 134
- [cross_source_blocking] source 7403 seq 8: ready 394 ms before start, 394 ms of it with an idle slot in group 256
- [cross_source_blocking] source 628 seq 11: ready 386 ms before start, 386 ms of it with an idle slot in group 18
- [cross_source_blocking] source 2607 seq 9: ready 389 ms before start, 389 ms of it with an idle slot in group 80
- [cross_source_blocking] source 9119 seq 19: ready 391 ms before start, 391 ms of it with an idle slot in group 315
- [cross_source_blocking] source 12919 seq 14: ready 389 ms before start, 389 ms of it with an idle slot in group 409

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-232 | 6 | 1 | 70.326 | 7.149 | 805 | 0 | 409 | 409 | 0 | 30.719 | 13303.807 | 13631.487 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 83 | 83 | 0 | 2523.135 | 7536.639 | 15073.279 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 376 | 376 | 0 | 2375.679 | 7143.423 | 8847.359 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 171 | 171 | 0 | 2228.223 | 5734.399 | 6848.511 |
| t-095 | 6 | 1 | 67.761 | 6.84 | 925 | 0 | 376 | 376 | 0 | 34.559 | 5603.327 | 6193.151 |
| t-198 | 6 | 1 | 16.318 | 1.427 | 180 | 0 | 76 | 76 | 0 | 32.255 | 5242.879 | 13303.807 |
| t-226 | 6 | 1 | 109.061 | 11.534 | 623 | 0 | 656 | 656 | 0 | 30.335 | 5210.111 | 5963.775 |
| t-062 | 18 | 1 | 38.881 | 11.922 | 2061 | 0 | 716 | 716 | 0 | 31.615 | 4784.127 | 5242.879 |
| t-249 | 15 | 1 | 29.615 | 7.653 | 691 | 0 | 450 | 450 | 0 | 29.695 | 4718.591 | 4784.127 |
| t-118 | 15 | 1 | 29.889 | 7.561 | 138 | 0 | 457 | 457 | 0 | 29.695 | 4685.823 | 5144.575 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 577 | 577 | 0 | 31.871 | 4685.823 | 5931.007 |
| t-020 | 22 | 1 | 78.895 | 27.797 | 981 | 0 | 1577 | 1577 | 0 | 32.639 | 4620.287 | 13565.951 |
| t-304 | 19 | 1 | 129.878 | 40.323 | 238 | 0 | 2257 | 2257 | 0 | 32.127 | 4620.287 | 6291.455 |
| t-412 | 6 | 1 | 35.26 | 3.264 | 468 | 0 | 194 | 194 | 0 | 27.135 | 4620.287 | 4685.823 |
| t-006 | 14 | 1 | 31.139 | 6.948 | 1170 | 0 | 419 | 419 | 0 | 29.311 | 4587.519 | 4849.663 |
| t-026 | 6 | 1 | 149.86 | 15.03 | 1596 | 0 | 872 | 872 | 0 | 31.487 | 4587.519 | 20840.447 |
| t-034 | 7 | 1 | 32.67 | 3.86 | 2831 | 0 | 238 | 238 | 0 | 30.079 | 4587.519 | 5242.879 |
| t-148 | 6 | 1 | 77.973 | 7.127 | 715 | 0 | 435 | 435 | 0 | 33.535 | 4587.519 | 4653.055 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 860 | 860 | 0 | 29.951 | 4587.519 | 4849.663 |
| t-181 | 35 | 3 | 184.39 | 106.528 | 1367 | 0 | 5939 | 5939 | 0 | 32.255 | 4587.519 | 13565.951 |
| t-248 | 6 | 1 | 41.432 | 4.041 | 1190 | 0 | 213 | 213 | 0 | 34.559 | 4587.519 | 4718.591 |
| t-272 | 14 | 1 | 124.531 | 30.953 | 394 | 0 | 1817 | 1817 | 0 | 31.487 | 4587.519 | 5832.703 |
| t-323 | 6 | 1 | 36.585 | 3.673 | 3540 | 0 | 218 | 218 | 0 | 27.391 | 4587.519 | 5242.879 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 390 | 390 | 0 | 32.511 | 4587.519 | 17301.503 |
| t-435 | 8 | 1 | 49.103 | 7.294 | 3763 | 0 | 434 | 434 | 0 | 33.279 | 4587.519 | 4915.199 |
