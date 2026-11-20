# M1-mac-app-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.255s, closing 1.631s, drain 3.109s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814647 | 814647 | 12895.739 | 12907.041 | 24377.5 | 0 | 0 | 0 | 431 | 4254 | 35604 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642923 | 166.525 | 46.591 | 384.767 | 1711.103 | 2963.455 | 10182.655 |
| e2e_from_send | 642923 | 108.413 | 42.911 | 233.983 | 1306.623 | 2877.439 | 10166.271 |
| producer_lag | 642923 | 58.112 | 0.006 | 136.959 | 1243.135 | 1565.695 | 2055.167 |
| publish_call | 814647 | 51.45 | 18.719 | 130.623 | 431.615 | 734.207 | 913.919 |
| queue_wait | 642923 | 81.237 | 24.319 | 199.679 | 724.479 | 2361.343 | 7614.463 |
| broker_dwell | 642923 | 0.845 | 0.001 | 0.001 | 0.001 | 341.247 | 5570.559 |
| receive_call | 758849 | 42.146 | 15.287 | 114.943 | 348.671 | 475.903 | 504.063 |
| ctx_load | 642923 | 5.95 | 3.603 | 13.311 | 31.455 | 70.591 | 100.415 |
| process | 642923 | 10.899 | 0.001 | 0.003 | 0.013 | 2623.487 | 3000.319 |
| ctx_commit | 642923 | 10.321 | 6.535 | 21.727 | 65.503 | 106.175 | 474.623 |
| ack | 521542 | 0.002 | 0.001 | 0.003 | 0.008 | 0.035 | 8.855 |
| service | 642923 | 27.177 | 11.183 | 34.975 | 97.279 | 2652.159 | 3264.511 |
| e2e_large | 331 | 3377.505 | 3219.455 | 4648.959 | 7004.159 | 10182.655 | 10182.655 |
| ready_wait | 642923 | 20.403 | 3.123 | 65.855 | 205.823 | 305.919 | 2275.327 |
| blocked | 642923 | 18.947 | 3.117 | 58.111 | 198.783 | 288.255 | 1518.591 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12907.041 | 16083.9 |
| applied_total | 466503.041 | 777040 |
| bench_cpu_cores | 6.075 | 6.774 |
| bench_cpu_pct | 75.939 | 84.68 |
| bench_gc_gen0 | 1.347 | 3 |
| bench_gc_gen1 | 0.714 | 2 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 1000.178 | 1806.4 |
| bench_mem_mb | 1826.786 | 2174.7 |
| bench_threadpool_queue | 4.265 | 36 |
| bench_threadpool_threads | 20.796 | 28 |
| broker_backlog | 2378.082 | 4937 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 852.98 | 2830 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 932431.184 | 1585815 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 207372.408 | 371732 |
| octopus_mqtt_sent_total | 320387.102 | 573680 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.918 | 16 |
| pg_active_loads | 9.878 | 176 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 23.143 | 246 |
| pg_conn_idle | 233.429 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.449 | 7 |
| pg_conn_total | 256.571 | 259 |
| pg_db_size_mb | 3366.318 | 3811 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19602.596 | 22760.1 |
| pg_tup_del_per_s | 36471.394 | 48435.6 |
| pg_tup_fetch_per_s | 416229.245 | 530826.9 |
| pg_tup_ins_per_s | 47022.647 | 60722.9 |
| pg_tup_upd_per_s | 34135.806 | 38119 |
| pg_wal_mb_per_s | 94.951 | 152.4 |
| produced_per_s | 12895.739 | 15823 |
| produced_total | 467356.02 | 777438 |
| publish_mb_per_s | 44.531 | 69.14 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,599 records are Completed |
| record_counts | pass | 117,599 records found, 117,599 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,647 events applied by workers, 814,647 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 35,604 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 199 ms, max 1518 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9140 seq 11: ready 100 ms before start, 100 ms of it with an idle slot in group 315
- [cross_source_blocking] source 14008 seq 3: ready 100 ms before start, 100 ms of it with an idle slot in group 446
- [cross_source_blocking] source 8680 seq 11: ready 100 ms before start, 100 ms of it with an idle slot in group 301
- [cross_source_blocking] source 10148 seq 14: ready 107 ms before start, 107 ms of it with an idle slot in group 344
- [cross_source_blocking] source 9096 seq 39: ready 101 ms before start, 101 ms of it with an idle slot in group 314
- [cross_source_blocking] source 5508 seq 11: ready 101 ms before start, 101 ms of it with an idle slot in group 189
- [cross_source_blocking] source 968 seq 36: ready 101 ms before start, 101 ms of it with an idle slot in group 29
- [cross_source_blocking] source 2280 seq 20: ready 107 ms before start, 107 ms of it with an idle slot in group 65
- [cross_source_blocking] source 5644 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 194
- [cross_source_blocking] source 8288 seq 15: ready 112 ms before start, 112 ms of it with an idle slot in group 282
- [cross_source_blocking] source 7480 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 258
- [cross_source_blocking] source 7248 seq 35: ready 101 ms before start, 101 ms of it with an idle slot in group 246
- [cross_source_blocking] source 5696 seq 18: ready 120 ms before start, 120 ms of it with an idle slot in group 196
- [cross_source_blocking] source 4832 seq 4: ready 143 ms before start, 133 ms of it with an idle slot in group 168
- [cross_source_blocking] source 2724 seq 6: ready 119 ms before start, 119 ms of it with an idle slot in group 83
- [cross_source_blocking] source 15676 seq 18: ready 131 ms before start, 131 ms of it with an idle slot in group 490
- [cross_source_blocking] source 5456 seq 17: ready 120 ms before start, 120 ms of it with an idle slot in group 186
- [cross_source_blocking] source 6764 seq 27: ready 120 ms before start, 120 ms of it with an idle slot in group 230
- [cross_source_blocking] source 8064 seq 27: ready 140 ms before start, 140 ms of it with an idle slot in group 271
- [cross_source_blocking] source 2232 seq 33: ready 115 ms before start, 115 ms of it with an idle slot in group 65

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 2965.503 | 6881.279 | 10223.615 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2523.135 | 6848.511 | 7438.335 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 2588.671 | 6062.079 | 6062.079 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 1933.311 | 4063.231 | 5210.111 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 41.471 | 3063.807 | 3964.927 |
| t-418 | 6 | 1 | 121.075 | 12.228 | 201 | 0 | 761 | 761 | 0 | 46.079 | 2785.279 | 3784.703 |
| t-447 | 17 | 1 | 45.301 | 12.225 | 558 | 0 | 750 | 750 | 0 | 44.031 | 2637.823 | 4259.839 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 48.127 | 2621.439 | 2949.119 |
| t-338 | 6 | 1 | 21.73 | 2.22 | 156 | 0 | 140 | 140 | 0 | 34.815 | 2588.671 | 2949.119 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 55.807 | 2572.287 | 3768.319 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 41.215 | 2490.367 | 2834.431 |
| t-299 | 21 | 1 | 42.355 | 14.484 | 646 | 0 | 907 | 907 | 0 | 46.847 | 2441.215 | 2998.271 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1790 | 1790 | 0 | 48.895 | 2441.215 | 3391.487 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 51.455 | 2424.831 | 5210.111 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 32.383 | 2392.063 | 2801.663 |
| t-349 | 14 | 1 | 44.443 | 10.033 | 379 | 0 | 603 | 603 | 0 | 42.495 | 2392.063 | 2867.199 |
| t-420 | 13 | 1 | 49.618 | 10.967 | 270 | 0 | 712 | 712 | 0 | 44.799 | 2392.063 | 3063.807 |
| t-152 | 43 | 1 | 37.673 | 26.595 | 882 | 0 | 1701 | 1701 | 0 | 46.591 | 2326.527 | 3784.703 |
| t-320 | 14 | 1 | 86.153 | 19.097 | 574 | 0 | 1206 | 1206 | 0 | 50.687 | 2293.759 | 2932.735 |
| t-381 | 11 | 1 | 47.024 | 8.839 | 134 | 0 | 603 | 603 | 0 | 39.679 | 2277.375 | 2883.583 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 40.959 | 2228.223 | 2899.967 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 41.727 | 2228.223 | 2768.895 |
| t-294 | 7 | 1 | 107.858 | 12.447 | 667 | 0 | 765 | 765 | 0 | 43.007 | 2228.223 | 3112.959 |
| t-455 | 16 | 1 | 50.441 | 13.579 | 3551 | 0 | 843 | 843 | 0 | 46.335 | 2228.223 | 3489.791 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 34.047 | 2195.455 | 2867.199 |
