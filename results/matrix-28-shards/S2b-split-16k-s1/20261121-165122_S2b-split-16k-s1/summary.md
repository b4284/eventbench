# S2b-split-16k-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.362s, closing 1.288s, drain 3.142s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 503642 | 503642 | 8028.784 | 8027.8 | 11504.8 | 0 | 0 | 0 | 373 | 2688 | 2803 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 401126 | 89.38 | 41.151 | 129.471 | 1420.287 | 2818.047 | 9084.927 |
| e2e_from_send | 401126 | 83.415 | 40.063 | 116.543 | 1415.167 | 2813.951 | 9084.927 |
| producer_lag | 401126 | 5.965 | 0.004 | 3.073 | 205.695 | 505.343 | 875.007 |
| publish_call | 503642 | 40.232 | 18.831 | 93.247 | 319.487 | 604.159 | 886.783 |
| queue_wait | 401126 | 55.517 | 22.991 | 88.639 | 810.495 | 2361.343 | 6373.375 |
| broker_dwell | 401126 | 52.931 | 27.343 | 64.927 | 787.967 | 2359.295 | 6275.071 |
| receive_call | 475879 | 38.149 | 18.287 | 94.207 | 307.967 | 467.711 | 622.079 |
| ctx_load | 401126 | 6.573 | 5.019 | 12.831 | 25.055 | 41.215 | 414.719 |
| process | 401126 | 11.016 | 0.001 | 0.002 | 0.011 | 2615.295 | 3057.663 |
| ctx_commit | 401126 | 10.289 | 7.471 | 20.991 | 47.295 | 94.719 | 404.479 |
| ack | 340660 | 0.002 | 0.001 | 0.004 | 0.009 | 0.042 | 2.711 |
| service | 401126 | 27.898 | 14.071 | 31.599 | 66.943 | 2637.823 | 3153.919 |
| e2e_large | 282 | 2760.433 | 2699.263 | 3663.871 | 6311.935 | 9084.927 | 9084.927 |
| ready_wait | 401126 | 10.892 | 3.957 | 28.863 | 94.591 | 212.479 | 1468.415 |
| blocked | 401126 | 10.424 | 3.945 | 27.807 | 87.487 | 197.119 | 1260.543 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8027.8 | 10467.6 |
| applied_total | 290481.796 | 482909 |
| bench_cpu_cores | 5.04 | 6.138 |
| bench_cpu_pct | 50.398 | 61.38 |
| bench_gc_gen0 | 0.653 | 2 |
| bench_gc_gen1 | 0.429 | 2 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 1281.769 | 1945.9 |
| bench_mem_mb | 2221.186 | 2299.5 |
| bench_threadpool_queue | 3.918 | 48 |
| bench_threadpool_threads | 16.143 | 18 |
| broker_backlog | 2789.245 | 5545 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 428.776 | 945 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 587409.755 | 988217 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 216713.694 | 360770 |
| octopus_mqtt_sent_total | 263626.041 | 448972 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.551 | 9 |
| pg_active_loads | 4.408 | 16 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 20.02 | 62 |
| pg_conn_idle | 241.163 | 259 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.082 | 2 |
| pg_conn_total | 261.184 | 262 |
| pg_db_size_mb | 3534.884 | 3900.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21994.265 | 23915.1 |
| pg_tup_del_per_s | 28312.643 | 40874.5 |
| pg_tup_fetch_per_s | 205398.255 | 236172.3 |
| pg_tup_ins_per_s | 38432.069 | 60627.1 |
| pg_tup_upd_per_s | 41280.686 | 43850.5 |
| pg_wal_mb_per_s | 101.821 | 136.68 |
| produced_per_s | 8028.784 | 10372.5 |
| produced_total | 290910.571 | 483278 |
| publish_mb_per_s | 33.464 | 56.73 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 138,563 records are Completed |
| record_counts | pass | 138,563 records found, 69,626 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 420 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 503,642 events applied by workers, 503,642 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 2,803 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 87 ms, max 1260 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6808 seq 11: ready 130 ms before start, 129 ms of it with an idle slot in group 231
- [cross_source_blocking] source 1116 seq 18: ready 146 ms before start, 146 ms of it with an idle slot in group 37
- [cross_source_blocking] source 13113 seq 8: ready 125 ms before start, 125 ms of it with an idle slot in group 417
- [cross_source_blocking] source 1308 seq 29: ready 105 ms before start, 105 ms of it with an idle slot in group 41
- [cross_source_blocking] source 6946 seq 17: ready 125 ms before start, 125 ms of it with an idle slot in group 235
- [cross_source_blocking] source 7873 seq 13: ready 124 ms before start, 112 ms of it with an idle slot in group 265
- [cross_source_blocking] source 7497 seq 4: ready 119 ms before start, 107 ms of it with an idle slot in group 259
- [cross_source_blocking] source 6773 seq 13: ready 156 ms before start, 155 ms of it with an idle slot in group 231
- [cross_source_blocking] source 12992 seq 15: ready 156 ms before start, 144 ms of it with an idle slot in group 413
- [cross_source_blocking] source 8721 seq 13: ready 147 ms before start, 134 ms of it with an idle slot in group 303
- [cross_source_blocking] source 4794 seq 4: ready 163 ms before start, 163 ms of it with an idle slot in group 167
- [cross_source_blocking] source 15576 seq 10: ready 103 ms before start, 101 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11680 seq 13: ready 110 ms before start, 110 ms of it with an idle slot in group 379
- [cross_source_blocking] source 3579 seq 16: ready 109 ms before start, 109 ms of it with an idle slot in group 121
- [cross_source_blocking] source 7699 seq 14: ready 108 ms before start, 108 ms of it with an idle slot in group 263
- [cross_source_blocking] source 6240 seq 7: ready 137 ms before start, 114 ms of it with an idle slot in group 213
- [cross_source_blocking] source 15578 seq 10: ready 103 ms before start, 103 ms of it with an idle slot in group 483
- [cross_source_blocking] source 2731 seq 11: ready 110 ms before start, 110 ms of it with an idle slot in group 83
- [cross_source_blocking] source 11721 seq 11: ready 108 ms before start, 108 ms of it with an idle slot in group 379
- [cross_source_blocking] source 9192 seq 16: ready 103 ms before start, 103 ms of it with an idle slot in group 315

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2818.047 | 6520.831 | 9109.503 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 2179.071 | 4784.127 | 6586.367 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 91 | 91 | 0 | 2310.143 | 3522.559 | 4112.383 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 707 | 707 | 0 | 39.935 | 2703.359 | 4390.911 |
| t-349 | 14 | 1 | 55.11 | 12.441 | 379 | 0 | 759 | 759 | 0 | 41.727 | 2375.679 | 2916.351 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 411 | 411 | 0 | 40.191 | 2359.295 | 3047.423 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2274 | 2274 | 0 | 41.727 | 2342.911 | 2949.119 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 370 | 370 | 0 | 44.543 | 2293.759 | 2818.047 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 979 | 979 | 0 | 43.007 | 2211.839 | 3014.655 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 443 | 443 | 0 | 41.983 | 2162.687 | 2965.503 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5215 | 5215 | 0 | 43.263 | 2129.919 | 3129.343 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 370 | 370 | 0 | 40.447 | 2113.535 | 2523.135 |
| t-205 | 18 | 1 | 65.466 | 19.469 | 449 | 0 | 1257 | 1257 | 0 | 40.959 | 2113.535 | 3031.039 |
| t-075 | 37 | 2 | 93.601 | 57.814 | 232 | 0 | 3552 | 3552 | 0 | 41.471 | 2072.575 | 3080.191 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2574 | 2574 | 0 | 44.799 | 2047.999 | 3309.567 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 1008 | 1008 | 0 | 40.959 | 2023.423 | 2949.119 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3271 | 3271 | 0 | 43.263 | 2015.231 | 3096.575 |
| t-365 | 13 | 1 | 43.515 | 9.39 | 3428 | 0 | 683 | 683 | 0 | 38.399 | 1966.079 | 2932.735 |
| t-415 | 14 | 1 | 85.928 | 19.862 | 2972 | 0 | 1281 | 1281 | 0 | 43.263 | 1966.079 | 2949.119 |
| t-153 | 17 | 1 | 51.807 | 14.803 | 965 | 0 | 927 | 927 | 0 | 40.703 | 1933.311 | 3063.807 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2486 | 2486 | 0 | 43.775 | 1925.119 | 2998.271 |
| t-417 | 75 | 2 | 54.652 | 67.926 | 3449 | 0 | 4236 | 4236 | 0 | 40.447 | 1925.119 | 3080.191 |
| t-225 | 6 | 1 | 39.542 | 3.897 | 3030 | 0 | 243 | 243 | 0 | 48.895 | 1884.159 | 2883.583 |
| t-019 | 38 | 2 | 109.921 | 69.438 | 208 | 0 | 4340 | 4340 | 0 | 41.215 | 1867.775 | 2867.199 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8359 | 8359 | 0 | 43.519 | 1859.583 | 2965.503 |
