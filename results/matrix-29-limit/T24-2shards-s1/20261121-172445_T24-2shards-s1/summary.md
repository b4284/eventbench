# T24-2shards-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.481s, closing 1.005s, drain 7.256s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 676138 | 676138 | 11225.188 | 11220.344 | 17185.2 | 0 | 0 | 0 | 337 | 3523 | 132756 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 532897 | 6033.491 | 5681.151 | 9633.791 | 11059.199 | 12468.223 | 17350.655 |
| e2e_from_send | 532897 | 466.844 | 336.127 | 860.159 | 2699.263 | 4349.951 | 10641.407 |
| producer_lag | 532897 | 5566.643 | 5226.495 | 9117.695 | 10354.687 | 11124.735 | 11444.223 |
| publish_call | 676138 | 181.301 | 141.695 | 359.935 | 693.759 | 931.839 | 1157.119 |
| queue_wait | 532897 | 407.525 | 285.183 | 786.943 | 2480.127 | 4214.783 | 8077.311 |
| broker_dwell | 532897 | 264.439 | 113.471 | 638.463 | 2355.199 | 4098.047 | 7806.975 |
| receive_call | 607105 | 31.696 | 12.879 | 81.535 | 269.567 | 444.159 | 536.063 |
| ctx_load | 532897 | 14.337 | 10.607 | 29.791 | 56.927 | 94.015 | 428.543 |
| process | 532897 | 10.761 | 0.001 | 0.002 | 0.009 | 2611.199 | 3020.799 |
| ctx_commit | 532897 | 34.192 | 26.831 | 68.479 | 139.007 | 220.671 | 737.279 |
| ack | 453678 | 0.002 | 0.001 | 0.003 | 0.01 | 0.039 | 3.313 |
| service | 532897 | 59.319 | 41.951 | 89.983 | 184.575 | 2676.735 | 3493.887 |
| e2e_large | 237 | 8817.081 | 8519.679 | 12500.991 | 14934.015 | 17350.655 | 17350.655 |
| ready_wait | 532897 | 87.354 | 46.687 | 222.719 | 524.799 | 881.663 | 1900.543 |
| blocked | 532897 | 71.144 | 40.159 | 180.735 | 392.447 | 652.799 | 1900.543 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11220.344 | 17185.2 |
| applied_total | 361245.6 | 651540 |
| bench_cpu_cores | 2.749 | 5.027 |
| bench_cpu_pct | 27.486 | 50.27 |
| bench_gc_gen0 | 0.64 | 2 |
| bench_gc_gen1 | 0.48 | 2 |
| bench_gc_gen2 | 0.46 | 1 |
| bench_gc_heap_mb | 1508.674 | 2383 |
| bench_mem_mb | 2498.852 | 2868 |
| bench_threadpool_queue | 3.98 | 158 |
| bench_threadpool_threads | 17.08 | 19 |
| broker_backlog | 46833.7 | 75192 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 3154.24 | 8227 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 728382.78 | 1318871 |
| octopus_missing_reports_total | 27165.74 | 46544 |
| octopus_mqtt_received_total | 151336.68 | 252879 |
| octopus_mqtt_sent_total | 241211.2 | 417603 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14 | 32 |
| pg_active_loads | 10.12 | 25 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.989 | 100 |
| pg_conn_active | 41.78 | 74 |
| pg_conn_idle | 219.64 | 241 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.54 | 25 |
| pg_conn_total | 261.42 | 262 |
| pg_db_size_mb | 3709.226 | 4153.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25052.708 | 33708.5 |
| pg_tup_del_per_s | 33069.838 | 38370.5 |
| pg_tup_fetch_per_s | 295598.502 | 360975.8 |
| pg_tup_ins_per_s | 44750.156 | 60598.4 |
| pg_tup_upd_per_s | 55497.398 | 70076.8 |
| pg_wal_mb_per_s | 123.164 | 196.07 |
| produced_per_s | 11225.188 | 15711.4 |
| produced_total | 364399.84 | 655593 |
| publish_mb_per_s | 38.674 | 59.96 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 185,182 records are Completed |
| record_counts | pass | 185,182 records found, 89,362 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 383 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 676,138 events applied by workers, 676,138 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 132,756 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 392 ms, max 1900 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14155 seq 4: ready 107 ms before start, 107 ms of it with an idle slot in group 451
- [cross_source_blocking] source 11133 seq 34: ready 150 ms before start, 150 ms of it with an idle slot in group 363
- [cross_source_blocking] source 5700 seq 30: ready 107 ms before start, 107 ms of it with an idle slot in group 197
- [cross_source_blocking] source 7076 seq 40: ready 108 ms before start, 108 ms of it with an idle slot in group 239
- [cross_source_blocking] source 10460 seq 28: ready 107 ms before start, 107 ms of it with an idle slot in group 347
- [cross_source_blocking] source 6927 seq 31: ready 107 ms before start, 107 ms of it with an idle slot in group 235
- [cross_source_blocking] source 5740 seq 36: ready 107 ms before start, 107 ms of it with an idle slot in group 197
- [cross_source_blocking] source 4778 seq 21: ready 107 ms before start, 107 ms of it with an idle slot in group 165
- [cross_source_blocking] source 12008 seq 32: ready 111 ms before start, 111 ms of it with an idle slot in group 383
- [cross_source_blocking] source 1958 seq 25: ready 177 ms before start, 177 ms of it with an idle slot in group 55
- [cross_source_blocking] source 8032 seq 58: ready 112 ms before start, 112 ms of it with an idle slot in group 271
- [cross_source_blocking] source 11949 seq 46: ready 131 ms before start, 122 ms of it with an idle slot in group 383
- [cross_source_blocking] source 13273 seq 28: ready 108 ms before start, 108 ms of it with an idle slot in group 425
- [cross_source_blocking] source 7085 seq 32: ready 110 ms before start, 110 ms of it with an idle slot in group 239
- [cross_source_blocking] source 3585 seq 37: ready 107 ms before start, 100 ms of it with an idle slot in group 121
- [cross_source_blocking] source 15403 seq 17: ready 139 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11981 seq 37: ready 131 ms before start, 122 ms of it with an idle slot in group 383
- [cross_source_blocking] source 65 seq 8: ready 138 ms before start, 107 ms of it with an idle slot in group 1
- [cross_source_blocking] source 7537 seq 8: ready 138 ms before start, 102 ms of it with an idle slot in group 259
- [cross_source_blocking] source 8724 seq 8: ready 166 ms before start, 123 ms of it with an idle slot in group 303

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 122 | 122 | 0 | 8650.751 | 14942.207 | 17432.575 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 356 | 356 | 0 | 8224.767 | 13828.095 | 15138.815 |
| t-473 | 32 | 1 | 78.515 | 42.651 | 4074 | 0 | 2438 | 2438 | 0 | 5865.471 | 13238.271 | 15269.887 |
| t-481 | 12 | 1 | 225.826 | 42.578 | 1993 | 0 | 2270 | 2270 | 0 | 6389.759 | 13041.663 | 14352.383 |
| t-109 | 33 | 1 | 88.394 | 49.274 | 1467 | 0 | 2847 | 2847 | 0 | 6848.511 | 12910.591 | 14745.599 |
| t-245 | 43 | 2 | 132.507 | 91.014 | 2232 | 0 | 5145 | 5145 | 0 | 6488.063 | 12451.839 | 13238.271 |
| t-489 | 6 | 1 | 259.835 | 26.522 | 1030 | 0 | 1441 | 1441 | 0 | 3063.807 | 12386.303 | 14221.311 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 77 | 77 | 0 | 6750.207 | 12320.767 | 12320.767 |
| t-355 | 10 | 1 | 56.219 | 9.856 | 349 | 0 | 522 | 522 | 0 | 4194.303 | 12255.231 | 12779.519 |
| t-223 | 13 | 1 | 200.098 | 42.324 | 2372 | 0 | 2363 | 2363 | 0 | 6127.615 | 11730.943 | 12124.159 |
| t-303 | 32 | 1 | 73.952 | 39.81 | 659 | 0 | 2259 | 2259 | 0 | 5505.023 | 11730.943 | 12648.447 |
| t-075 | 37 | 2 | 140.402 | 86.721 | 232 | 0 | 4878 | 4878 | 0 | 6029.311 | 11665.407 | 13303.807 |
| t-205 | 18 | 1 | 98.199 | 29.204 | 449 | 0 | 1615 | 1615 | 0 | 6094.847 | 11665.407 | 12976.127 |
| t-055 | 65 | 4 | 188.704 | 195.304 | 359 | 0 | 10607 | 10607 | 0 | 6029.311 | 11599.871 | 13631.487 |
| t-261 | 51 | 2 | 110.543 | 94.419 | 2342 | 0 | 5372 | 5372 | 0 | 6225.919 | 11599.871 | 12779.519 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 6980 | 6980 | 0 | 5865.471 | 11599.871 | 13238.271 |
| t-065 | 80 | 9 | 315.578 | 416.57 | 263 | 0 | 22707 | 22707 | 0 | 5832.703 | 11534.335 | 14286.847 |
| t-409 | 48 | 2 | 120.205 | 94.736 | 671 | 0 | 5280 | 5280 | 0 | 6127.615 | 11534.335 | 13238.271 |
| t-465 | 66 | 3 | 131.577 | 141.969 | 667 | 0 | 8053 | 8053 | 0 | 5898.239 | 11534.335 | 13500.415 |
| t-105 | 33 | 2 | 136.816 | 78.663 | 851 | 0 | 4487 | 4487 | 0 | 5767.167 | 11468.799 | 14548.991 |
| t-197 | 42 | 3 | 192.662 | 131.227 | 725 | 0 | 7446 | 7446 | 0 | 6127.615 | 11468.799 | 12910.591 |
| t-287 | 17 | 1 | 133.641 | 39.215 | 158 | 0 | 2216 | 2216 | 0 | 4718.591 | 11468.799 | 11927.551 |
| t-453 | 6 | 1 | 83.594 | 8.234 | 874 | 0 | 455 | 455 | 0 | 6651.903 | 11468.799 | 12058.623 |
| t-469 | 105 | 5 | 142.583 | 248.092 | 278 | 0 | 13626 | 13626 | 0 | 5767.167 | 11468.799 | 13303.807 |
| t-023 | 72 | 1 | 38.898 | 46.836 | 647 | 0 | 2795 | 2795 | 0 | 5701.631 | 11403.263 | 13631.487 |
