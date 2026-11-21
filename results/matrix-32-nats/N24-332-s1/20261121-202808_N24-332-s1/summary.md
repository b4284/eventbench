# N24-332-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.0.217:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 86.215s, closing 0.864s, drain 7.751s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 553539 | 553539 | 9283.684 | 9327.71 | 13807.2 | 0 | 0 | 0 | 100 | 2704 | 4789 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 445877 | 287.315 | 43.103 | 987.135 | 3217.407 | 4147.199 | 7282.687 |
| e2e_from_send | 445877 | 112.42 | 42.047 | 229.119 | 1629.183 | 2852.863 | 5169.151 |
| producer_lag | 445877 | 174.896 | 0.004 | 457.983 | 2891.775 | 3667.967 | 4001.791 |
| publish_call | 553539 | 81.64 | 21.775 | 223.871 | 696.319 | 1521.663 | 1983.487 |
| queue_wait | 445877 | 82.952 | 22.511 | 190.463 | 1236.991 | 2580.479 | 4747.263 |
| broker_dwell | 445877 | 37.442 | 0.001 | 39.999 | 1183.743 | 2529.279 | 4702.207 |
| receive_call | 522298 | 31.298 | 16.383 | 73.023 | 250.751 | 457.215 | 508.671 |
| ctx_load | 445877 | 6.037 | 4.103 | 11.991 | 28.143 | 113.279 | 396.031 |
| process | 445877 | 10.042 | 0.001 | 0.002 | 0.008 | 2592.767 | 3211.263 |
| ctx_commit | 445877 | 13.371 | 8.159 | 29.519 | 72.767 | 252.927 | 482.815 |
| ack | 369509 | 0.002 | 0.001 | 0.003 | 0.007 | 0.02 | 0.334 |
| service | 445877 | 29.468 | 14.143 | 39.455 | 111.103 | 2621.439 | 3368.959 |
| e2e_large | 77 | 2851.075 | 2519.039 | 3749.887 | 7196.671 | 7282.687 | 7282.687 |
| ready_wait | 445877 | 9.159 | 2.551 | 19.343 | 122.687 | 319.999 | 1913.855 |
| blocked | 445877 | 8.214 | 2.547 | 16.511 | 103.359 | 317.695 | 553.983 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9327.71 | 13807.2 |
| applied_total | 321147.224 | 537052 |
| bench_cpu_cores | 4.811 | 6.101 |
| bench_cpu_pct | 48.106 | 61.01 |
| bench_gc_gen0 | 0.286 | 2 |
| bench_gc_gen1 | 0.143 | 1 |
| bench_gc_gen2 | 0.102 | 1 |
| bench_gc_heap_mb | 1836.876 | 2922.3 |
| bench_mem_mb | 2926.351 | 3173.3 |
| bench_threadpool_queue | 3.265 | 97 |
| bench_threadpool_threads | 19.837 | 24 |
| broker_backlog | 5461.918 | 14589 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 708.878 | 2957 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 644006.694 | 1092664 |
| octopus_missing_reports_total | 19026.796 | 23980 |
| octopus_mqtt_received_total | 188552.122 | 334081 |
| octopus_mqtt_sent_total | 307473.98 | 536008 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.673 | 31 |
| pg_active_loads | 7.612 | 29 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 41.245 | 115 |
| pg_conn_idle | 221.98 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.408 | 6 |
| pg_conn_total | 263.224 | 264 |
| pg_db_size_mb | 3081.104 | 3289.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25431.451 | 27970.5 |
| pg_tup_del_per_s | 24680.547 | 30791.1 |
| pg_tup_fetch_per_s | 229465.449 | 263143.1 |
| pg_tup_ins_per_s | 30115.914 | 35760.3 |
| pg_tup_upd_per_s | 49232.388 | 54697.3 |
| pg_wal_mb_per_s | 82.649 | 111.91 |
| produced_per_s | 9283.684 | 13086.2 |
| produced_total | 321856.102 | 537606 |
| publish_mb_per_s | 23.027 | 33.47 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 159,133 records are Completed |
| record_counts | pass | 159,133 records found, 73,222 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 172 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 553,539 events applied by workers, 553,539 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 4,789 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 103 ms, max 554 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9206 seq 11: ready 113 ms before start, 113 ms of it with an idle slot in group 316
- [cross_source_blocking] source 5535 seq 11: ready 128 ms before start, 128 ms of it with an idle slot in group 189
- [cross_source_blocking] source 1110 seq 12: ready 104 ms before start, 104 ms of it with an idle slot in group 36
- [cross_source_blocking] source 12158 seq 7: ready 121 ms before start, 121 ms of it with an idle slot in group 388
- [cross_source_blocking] source 11715 seq 19: ready 139 ms before start, 139 ms of it with an idle slot in group 379
- [cross_source_blocking] source 5687 seq 34: ready 127 ms before start, 127 ms of it with an idle slot in group 196
- [cross_source_blocking] source 10412 seq 15: ready 208 ms before start, 208 ms of it with an idle slot in group 347
- [cross_source_blocking] source 662 seq 37: ready 152 ms before start, 152 ms of it with an idle slot in group 19
- [cross_source_blocking] source 4359 seq 63: ready 312 ms before start, 312 ms of it with an idle slot in group 147
- [cross_source_blocking] source 2995 seq 16: ready 141 ms before start, 141 ms of it with an idle slot in group 99
- [cross_source_blocking] source 887 seq 11: ready 279 ms before start, 278 ms of it with an idle slot in group 27
- [cross_source_blocking] source 5232 seq 57: ready 261 ms before start, 261 ms of it with an idle slot in group 181
- [cross_source_blocking] source 9648 seq 7: ready 316 ms before start, 314 ms of it with an idle slot in group 333
- [cross_source_blocking] source 15270 seq 16: ready 346 ms before start, 346 ms of it with an idle slot in group 477
- [cross_source_blocking] source 5686 seq 34: ready 227 ms before start, 227 ms of it with an idle slot in group 196
- [cross_source_blocking] source 5687 seq 35: ready 208 ms before start, 208 ms of it with an idle slot in group 196
- [cross_source_blocking] source 1109 seq 13: ready 107 ms before start, 107 ms of it with an idle slot in group 36
- [cross_source_blocking] source 13960 seq 21: ready 306 ms before start, 306 ms of it with an idle slot in group 443
- [cross_source_blocking] source 13959 seq 19: ready 107 ms before start, 107 ms of it with an idle slot in group 443
- [cross_source_blocking] source 14970 seq 24: ready 353 ms before start, 353 ms of it with an idle slot in group 469

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 88 | 88 | 0 | 2457.599 | 7307.263 | 7307.263 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 1818.623 | 4718.591 | 5767.167 |
| t-077 | 10 | 1 | 267.106 | 44.58 | 1566 | 0 | 2693 | 2693 | 0 | 46.591 | 3997.695 | 5963.775 |
| t-484 | 6 | 1 | 139.393 | 14.11 | 232 | 0 | 859 | 859 | 0 | 47.615 | 3981.311 | 4259.839 |
| t-251 | 9 | 1 | 70.446 | 10.583 | 1193 | 0 | 625 | 625 | 0 | 38.143 | 3833.855 | 4227.071 |
| t-492 | 6 | 1 | 48.414 | 4.411 | 3046 | 0 | 282 | 282 | 0 | 41.983 | 3833.855 | 4030.463 |
| t-099 | 13 | 1 | 92.021 | 21.193 | 419 | 0 | 1314 | 1314 | 0 | 41.215 | 3817.471 | 5472.255 |
| t-155 | 14 | 1 | 27.137 | 6.169 | 2759 | 0 | 397 | 397 | 0 | 40.447 | 3817.471 | 4227.071 |
| t-203 | 6 | 1 | 53.511 | 5.782 | 2258 | 0 | 373 | 373 | 0 | 41.727 | 3801.087 | 4095.999 |
| t-020 | 22 | 1 | 118.343 | 41.696 | 981 | 0 | 2546 | 2546 | 0 | 42.751 | 3768.319 | 5275.647 |
| t-412 | 6 | 1 | 52.89 | 4.896 | 468 | 0 | 285 | 285 | 0 | 41.471 | 3702.783 | 3850.239 |
| t-139 | 16 | 1 | 32.738 | 9.249 | 1367 | 0 | 612 | 612 | 0 | 41.471 | 3670.015 | 4046.847 |
| t-365 | 13 | 1 | 65.273 | 14.086 | 3428 | 0 | 928 | 928 | 0 | 42.495 | 3670.015 | 5177.343 |
| t-187 | 6 | 1 | 104.579 | 10.404 | 685 | 0 | 593 | 593 | 0 | 44.287 | 3620.863 | 4358.143 |
| t-427 | 16 | 2 | 206.752 | 54.879 | 1711 | 0 | 3370 | 3370 | 0 | 46.079 | 3620.863 | 5996.543 |
| t-028 | 27 | 1 | 38.33 | 17.031 | 454 | 0 | 1107 | 1107 | 0 | 41.983 | 3604.479 | 4456.447 |
| t-419 | 17 | 1 | 38.104 | 11.284 | 3214 | 0 | 706 | 706 | 0 | 43.775 | 3588.095 | 4259.839 |
| t-123 | 16 | 1 | 124.914 | 31.051 | 4028 | 0 | 1869 | 1869 | 0 | 43.263 | 3555.327 | 4259.839 |
| t-459 | 25 | 1 | 31.174 | 12.721 | 2174 | 0 | 832 | 832 | 0 | 40.191 | 3555.327 | 4079.615 |
| t-141 | 7 | 1 | 204.873 | 24.01 | 134 | 0 | 1493 | 1493 | 0 | 42.495 | 3538.943 | 4390.911 |
| t-292 | 16 | 1 | 83.138 | 23.979 | 916 | 0 | 1466 | 1466 | 0 | 41.727 | 3506.175 | 4521.983 |
| t-444 | 16 | 1 | 65.916 | 17.836 | 188 | 0 | 1109 | 1109 | 0 | 41.727 | 3457.023 | 4063.231 |
| t-235 | 54 | 3 | 147.636 | 132.899 | 527 | 0 | 7963 | 7963 | 0 | 42.239 | 3440.639 | 5701.631 |
| t-435 | 8 | 1 | 73.655 | 10.941 | 3763 | 0 | 663 | 663 | 0 | 45.311 | 3424.255 | 3883.007 |
| t-349 | 14 | 1 | 82.665 | 18.662 | 379 | 0 | 1168 | 1168 | 0 | 46.079 | 3407.871 | 5242.879 |
