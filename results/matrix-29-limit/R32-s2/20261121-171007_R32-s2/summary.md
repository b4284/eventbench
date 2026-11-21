# R32-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 926 workers, offered 31870.178 ev/s
- Phases: run 89.754s, closing 1.609s, drain 7.835s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 374856 | 374856 | 6054.712 | 6052.541 | 8879.8 | 0 | 0 | 0 | 173 | 1917 | 7934 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 239809 | 21970.502 | 21889.023 | 29376.511 | 32473.087 | 34144.255 | 36831.231 |
| e2e_from_send | 239809 | 327.046 | 265.727 | 516.351 | 1812.479 | 3086.335 | 6115.327 |
| producer_lag | 239809 | 21643.456 | 21577.727 | 29032.447 | 32178.175 | 33882.111 | 34963.455 |
| publish_call | 374856 | 218.233 | 187.647 | 403.967 | 731.135 | 1070.079 | 1471.487 |
| queue_wait | 239809 | 258.367 | 205.823 | 431.871 | 1349.631 | 2709.503 | 4739.071 |
| broker_dwell | 239809 | 61.936 | 23.231 | 90.047 | 1101.823 | 2547.711 | 4718.591 |
| receive_call | 320396 | 56.402 | 30.063 | 144.639 | 341.503 | 481.535 | 568.831 |
| ctx_load | 239809 | 16.809 | 11.783 | 32.143 | 129.343 | 166.783 | 663.551 |
| process | 239809 | 10.665 | 0.001 | 0.003 | 0.014 | 2578.431 | 3217.407 |
| ctx_commit | 239809 | 41.164 | 28.031 | 78.271 | 295.935 | 529.919 | 759.295 |
| ack | 225304 | 0.003 | 0.001 | 0.005 | 0.014 | 0.066 | 44.287 |
| service | 239809 | 68.68 | 44.895 | 105.535 | 354.047 | 2650.111 | 3547.135 |
| e2e_large | 99 | 23487.374 | 23019.519 | 30949.375 | 33947.647 | 36831.231 | 36831.231 |
| ready_wait | 239809 | 27.463 | 13.799 | 59.135 | 255.359 | 477.951 | 1074.175 |
| blocked | 239809 | 25.801 | 13.687 | 53.439 | 249.983 | 462.847 | 971.263 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6052.541 | 8879.8 |
| applied_total | 203330.449 | 355170 |
| bench_cpu_cores | 1.984 | 4.153 |
| bench_cpu_pct | 24.8 | 51.91 |
| bench_gc_gen0 | 0.449 | 2 |
| bench_gc_gen1 | 0.347 | 1 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1058.908 | 1882.7 |
| bench_mem_mb | 1880.388 | 1997.8 |
| bench_threadpool_queue | 7.224 | 200 |
| bench_threadpool_threads | 15.551 | 18 |
| broker_backlog | 26804.265 | 54573 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 662.714 | 1331 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 418316.306 | 735357 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 86754.959 | 135878 |
| octopus_mqtt_sent_total | 140105.959 | 240960 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.551 | 42 |
| pg_active_loads | 11.02 | 41 |
| pg_active_polls | 0.082 | 1 |
| pg_active_sends | 0.082 | 1 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 48.02 | 86 |
| pg_conn_idle | 215.49 | 241 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.408 | 24 |
| pg_conn_total | 263.51 | 264 |
| pg_db_size_mb | 3551.498 | 4081.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26428.822 | 31435 |
| pg_tup_del_per_s | 31483.427 | 40383.8 |
| pg_tup_fetch_per_s | 299424.878 | 332352.7 |
| pg_tup_ins_per_s | 41648.867 | 50788 |
| pg_tup_upd_per_s | 57132.569 | 65342.6 |
| pg_wal_mb_per_s | 121.202 | 236.07 |
| produced_per_s | 6054.712 | 8480.7 |
| produced_total | 203993.163 | 355844 |
| publish_mb_per_s | 20.638 | 33.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5629/5629 source cursors at the expected last sequence |
| records_complete | pass | all 187,233 records are Completed |
| record_counts | pass | 187,233 records found, 51,272 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 307 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 374,856 events applied by workers, 374,856 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 7,934 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 250 ms, max 971 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1584 seq 16: ready 151 ms before start, 132 ms of it with an idle slot in group 47
- [cross_source_blocking] source 11371 seq 31: ready 478 ms before start, 478 ms of it with an idle slot in group 371
- [cross_source_blocking] source 12662 seq 21: ready 641 ms before start, 641 ms of it with an idle slot in group 398
- [cross_source_blocking] source 8590 seq 16: ready 172 ms before start, 172 ms of it with an idle slot in group 296
- [cross_source_blocking] source 15775 seq 14: ready 172 ms before start, 172 ms of it with an idle slot in group 494
- [cross_source_blocking] source 3452 seq 2: ready 131 ms before start, 131 ms of it with an idle slot in group 119
- [cross_source_blocking] source 8456 seq 26: ready 391 ms before start, 391 ms of it with an idle slot in group 287
- [cross_source_blocking] source 3903 seq 27: ready 101 ms before start, 101 ms of it with an idle slot in group 131
- [cross_source_blocking] source 14578 seq 24: ready 103 ms before start, 103 ms of it with an idle slot in group 461
- [cross_source_blocking] source 10704 seq 26: ready 104 ms before start, 104 ms of it with an idle slot in group 350
- [cross_source_blocking] source 14526 seq 27: ready 103 ms before start, 103 ms of it with an idle slot in group 461
- [cross_source_blocking] source 14502 seq 18: ready 103 ms before start, 103 ms of it with an idle slot in group 461
- [cross_source_blocking] source 10640 seq 12: ready 102 ms before start, 102 ms of it with an idle slot in group 350
- [cross_source_blocking] source 1313 seq 95: ready 102 ms before start, 102 ms of it with an idle slot in group 41
- [cross_source_blocking] source 14537 seq 26: ready 115 ms before start, 115 ms of it with an idle slot in group 461
- [cross_source_blocking] source 7176 seq 39: ready 102 ms before start, 100 ms of it with an idle slot in group 245
- [cross_source_blocking] source 1301 seq 80: ready 102 ms before start, 102 ms of it with an idle slot in group 41
- [cross_source_blocking] source 1055 seq 13: ready 371 ms before start, 371 ms of it with an idle slot in group 32
- [cross_source_blocking] source 5473 seq 32: ready 250 ms before start, 250 ms of it with an idle slot in group 188
- [cross_source_blocking] source 385 seq 11: ready 371 ms before start, 355 ms of it with an idle slot in group 11

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-314 | 24 | 4 | 465.22 | 185.373 | 525 | 0 | 5764 | 5764 | 0 | 22937.599 | 34603.007 | 36962.303 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 252 | 252 | 0 | 22806.527 | 34078.719 | 36962.303 |
| t-146 | 6 | 1 | 107.361 | 11.519 | 246 | 0 | 358 | 358 | 0 | 20185.087 | 34078.719 | 34078.719 |
| t-020 | 22 | 2 | 157.791 | 55.595 | 981 | 0 | 1712 | 1712 | 0 | 23068.671 | 33816.575 | 34340.863 |
| t-041 | 34 | 6 | 482.299 | 271.676 | 2732 | 0 | 8200 | 8200 | 0 | 22675.455 | 33816.575 | 35127.295 |
| t-266 | 6 | 1 | 281.323 | 27.515 | 714 | 0 | 810 | 810 | 0 | 22937.599 | 33816.575 | 34078.719 |
| t-398 | 15 | 1 | 96.346 | 25.266 | 482 | 0 | 861 | 861 | 0 | 22282.239 | 33816.575 | 34340.863 |
| t-407 | 30 | 1 | 70.93 | 34.241 | 222 | 0 | 1094 | 1094 | 0 | 22544.383 | 33816.575 | 34078.719 |
| t-476 | 15 | 1 | 95.147 | 26.545 | 432 | 0 | 760 | 760 | 0 | 22413.311 | 33816.575 | 34603.007 |
| t-038 | 9 | 1 | 110.235 | 16.571 | 2056 | 0 | 487 | 487 | 0 | 22151.167 | 33554.431 | 33816.575 |
| t-125 | 20 | 2 | 160.342 | 55.98 | 1900 | 0 | 1735 | 1735 | 0 | 22020.095 | 33554.431 | 34340.863 |
| t-185 | 12 | 1 | 76.441 | 15.034 | 438 | 0 | 446 | 446 | 0 | 22282.239 | 33554.431 | 34078.719 |
| t-056 | 7 | 1 | 119.601 | 14.449 | 347 | 0 | 392 | 392 | 0 | 22675.455 | 33423.359 | 33816.575 |
| t-299 | 21 | 1 | 105.04 | 35.921 | 646 | 0 | 1158 | 1158 | 0 | 22020.095 | 33423.359 | 34340.863 |
| t-356 | 6 | 1 | 211.523 | 20.72 | 303 | 0 | 590 | 590 | 0 | 22020.095 | 33423.359 | 34340.863 |
| t-374 | 28 | 5 | 443.538 | 205.203 | 1811 | 0 | 6275 | 6275 | 0 | 22937.599 | 33423.359 | 34865.151 |
| t-326 | 10 | 2 | 403.427 | 69.119 | 1837 | 0 | 2116 | 2116 | 0 | 23592.959 | 33292.287 | 34078.719 |
| t-077 | 10 | 2 | 356.142 | 59.44 | 1566 | 0 | 1740 | 1740 | 0 | 23330.815 | 33161.215 | 34078.719 |
| t-380 | 22 | 2 | 192.03 | 68.716 | 132 | 0 | 2142 | 2142 | 0 | 21233.663 | 33161.215 | 34340.863 |
| t-176 | 11 | 1 | 102.407 | 17.77 | 1581 | 0 | 516 | 516 | 0 | 22544.383 | 33030.143 | 34078.719 |
| t-029 | 28 | 4 | 355.32 | 164.765 | 1732 | 0 | 4903 | 4903 | 0 | 22020.095 | 32899.071 | 35913.727 |
| t-134 | 21 | 1 | 48.603 | 16.651 | 3368 | 0 | 582 | 582 | 0 | 21102.591 | 32767.999 | 34078.719 |
| t-215 | 31 | 2 | 137.977 | 69.236 | 2427 | 0 | 2118 | 2118 | 0 | 21889.023 | 32767.999 | 34078.719 |
| t-287 | 17 | 2 | 178.188 | 52.287 | 158 | 0 | 1562 | 1562 | 0 | 22675.455 | 32767.999 | 34078.719 |
| t-005 | 47 | 3 | 150.191 | 116.72 | 2385 | 0 | 3632 | 3632 | 0 | 22020.095 | 32636.927 | 34603.007 |
