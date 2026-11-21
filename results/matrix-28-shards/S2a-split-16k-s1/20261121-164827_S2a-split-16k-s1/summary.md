# S2a-split-16k-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.495s, closing 1.076s, drain 3.687s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 503601 | 503601 | 8035.016 | 8032.916 | 12545 | 0 | 0 | 0 | 373 | 2688 | 17439 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 401075 | 103.104 | 48.351 | 183.295 | 1449.983 | 2893.823 | 9560.063 |
| e2e_from_send | 401075 | 99.88 | 47.071 | 172.543 | 1444.863 | 2891.775 | 9469.951 |
| producer_lag | 401075 | 3.224 | 0.004 | 2.903 | 84.223 | 287.487 | 585.215 |
| publish_call | 503601 | 38.23 | 20.479 | 81.087 | 299.263 | 560.127 | 648.191 |
| queue_wait | 401075 | 69.868 | 27.631 | 139.007 | 866.303 | 2390.015 | 6975.487 |
| broker_dwell | 401075 | 71.124 | 35.007 | 113.343 | 859.135 | 2396.159 | 6807.551 |
| receive_call | 474073 | 37.975 | 16.703 | 96.127 | 324.351 | 469.503 | 504.575 |
| ctx_load | 401075 | 7.205 | 5.347 | 14.903 | 29.199 | 43.423 | 134.527 |
| process | 401075 | 11.008 | 0.001 | 0.002 | 0.011 | 2613.247 | 3000.319 |
| ctx_commit | 401075 | 11.774 | 8.551 | 24.623 | 49.695 | 92.735 | 651.775 |
| ack | 340844 | 0.002 | 0.001 | 0.004 | 0.009 | 0.043 | 20.719 |
| service | 401075 | 30.012 | 15.799 | 36.607 | 73.023 | 2646.015 | 3194.879 |
| e2e_large | 282 | 3228.396 | 3096.575 | 4370.431 | 6389.759 | 9560.063 | 9560.063 |
| ready_wait | 401075 | 19.231 | 4.735 | 52.575 | 206.975 | 344.575 | 2207.743 |
| blocked | 401075 | 18.249 | 4.707 | 47.583 | 199.679 | 334.079 | 1506.303 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8032.916 | 9649.4 |
| applied_total | 289816.959 | 482361 |
| bench_cpu_cores | 5 | 5.502 |
| bench_cpu_pct | 49.998 | 55.02 |
| bench_gc_gen0 | 0.633 | 2 |
| bench_gc_gen1 | 0.408 | 1 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 1272.38 | 1978.6 |
| bench_mem_mb | 2150.359 | 2275.5 |
| bench_threadpool_queue | 1.102 | 21 |
| bench_threadpool_threads | 17 | 21 |
| broker_backlog | 3235.367 | 4569 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 576.082 | 1636 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 586170.061 | 979041 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 216403.163 | 357358 |
| octopus_mqtt_sent_total | 263391.796 | 444292 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.816 | 18 |
| pg_active_loads | 4.143 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 20.306 | 62 |
| pg_conn_idle | 240.347 | 259 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.388 | 18 |
| pg_conn_total | 260.653 | 262 |
| pg_db_size_mb | 3533.406 | 3874.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21562.941 | 24381 |
| pg_tup_del_per_s | 28089.255 | 36901.1 |
| pg_tup_fetch_per_s | 206453.667 | 230340.6 |
| pg_tup_ins_per_s | 38469.502 | 50837 |
| pg_tup_upd_per_s | 41203.908 | 44666.1 |
| pg_wal_mb_per_s | 98.505 | 123.33 |
| produced_per_s | 8035.016 | 9318.3 |
| produced_total | 290393.041 | 482804 |
| publish_mb_per_s | 33.589 | 61.42 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 138,580 records are Completed |
| record_counts | pass | 138,580 records found, 69,626 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 420 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 503,601 events applied by workers, 503,601 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 17,439 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 200 ms, max 1505 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12199 seq 8: ready 129 ms before start, 129 ms of it with an idle slot in group 389
- [cross_source_blocking] source 7211 seq 16: ready 195 ms before start, 195 ms of it with an idle slot in group 245
- [cross_source_blocking] source 3596 seq 21: ready 132 ms before start, 132 ms of it with an idle slot in group 121
- [cross_source_blocking] source 2949 seq 10: ready 132 ms before start, 132 ms of it with an idle slot in group 95
- [cross_source_blocking] source 1910 seq 26: ready 164 ms before start, 164 ms of it with an idle slot in group 55
- [cross_source_blocking] source 14251 seq 2: ready 196 ms before start, 196 ms of it with an idle slot in group 457
- [cross_source_blocking] source 3544 seq 17: ready 132 ms before start, 132 ms of it with an idle slot in group 121
- [cross_source_blocking] source 13914 seq 34: ready 132 ms before start, 132 ms of it with an idle slot in group 441
- [cross_source_blocking] source 3570 seq 20: ready 111 ms before start, 111 ms of it with an idle slot in group 121
- [cross_source_blocking] source 388 seq 14: ready 113 ms before start, 113 ms of it with an idle slot in group 11
- [cross_source_blocking] source 15643 seq 14: ready 131 ms before start, 131 ms of it with an idle slot in group 487
- [cross_source_blocking] source 5532 seq 16: ready 132 ms before start, 132 ms of it with an idle slot in group 189
- [cross_source_blocking] source 11611 seq 19: ready 132 ms before start, 132 ms of it with an idle slot in group 377
- [cross_source_blocking] source 14543 seq 10: ready 131 ms before start, 131 ms of it with an idle slot in group 461
- [cross_source_blocking] source 11003 seq 7: ready 132 ms before start, 132 ms of it with an idle slot in group 359
- [cross_source_blocking] source 6096 seq 6: ready 142 ms before start, 142 ms of it with an idle slot in group 209
- [cross_source_blocking] source 10917 seq 7: ready 111 ms before start, 111 ms of it with an idle slot in group 355
- [cross_source_blocking] source 7374 seq 15: ready 111 ms before start, 111 ms of it with an idle slot in group 253
- [cross_source_blocking] source 1503 seq 10: ready 136 ms before start, 136 ms of it with an idle slot in group 45
- [cross_source_blocking] source 10919 seq 6: ready 132 ms before start, 132 ms of it with an idle slot in group 355

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 2899.967 | 6389.759 | 9568.255 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2179.071 | 4816.895 | 6488.063 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2981.887 | 4521.983 | 4915.199 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 707 | 707 | 0 | 47.615 | 2899.967 | 4489.215 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 411 | 411 | 0 | 47.359 | 2457.599 | 3047.423 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 759 | 759 | 0 | 48.639 | 2408.447 | 2932.735 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2274 | 2274 | 0 | 50.687 | 2359.295 | 3227.647 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 443 | 443 | 0 | 46.335 | 2277.375 | 2981.887 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 979 | 979 | 0 | 49.919 | 2244.607 | 2998.271 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 370 | 370 | 0 | 44.799 | 2228.223 | 2818.047 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5215 | 5215 | 0 | 52.479 | 2211.839 | 3276.799 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2574 | 2574 | 0 | 54.527 | 2179.071 | 3211.263 |
| t-205 | 18 | 1 | 65.466 | 19.469 | 449 | 0 | 1257 | 1257 | 0 | 47.871 | 2146.303 | 3080.191 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 683 | 683 | 0 | 49.919 | 2146.303 | 2981.887 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3269 | 3269 | 0 | 49.663 | 2129.919 | 3145.727 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 370 | 370 | 0 | 48.639 | 2088.959 | 2490.367 |
| t-415 | 14 | 1 | 85.928 | 19.862 | 2972 | 0 | 1281 | 1281 | 0 | 49.919 | 2047.999 | 2719.743 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3552 | 3552 | 0 | 48.383 | 2015.231 | 2752.511 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2486 | 2486 | 0 | 50.943 | 2007.039 | 3047.423 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1008 | 1008 | 0 | 46.847 | 1974.271 | 2965.503 |
| t-019 | 38 | 2 | 109.921 | 69.438 | 208 | 0 | 4340 | 4340 | 0 | 49.151 | 1949.695 | 2867.199 |
| t-417 | 75 | 2 | 54.652 | 67.926 | 3449 | 0 | 4236 | 4236 | 0 | 48.639 | 1916.927 | 3014.655 |
| t-439 | 22 | 1 | 84.591 | 31.543 | 3339 | 0 | 1992 | 1992 | 0 | 48.127 | 1916.927 | 2916.351 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 927 | 927 | 0 | 46.079 | 1892.351 | 2981.887 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8359 | 8359 | 0 | 52.479 | 1884.159 | 3014.655 |
