# N24-332-s2 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.0.217:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 92.445s, closing 1.45s, drain 0.858s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 51259 | 51259 | 560.27 | 604.58 | 3348.9 | 0 | 0 | 0 | 72 | 294 | 1967 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 5158 | 42957.784 | 43581.439 | 49446.911 | 53673.983 | 54296.575 | 55934.975 |
| e2e_from_send | 5158 | 2392.423 | 2025.471 | 4378.623 | 5971.967 | 6905.855 | 12353.535 |
| producer_lag | 5158 | 40565.445 | 40402.943 | 45809.663 | 47841.279 | 51085.311 | 51347.455 |
| publish_call | 51259 | 2561.384 | 797.183 | 3936.255 | 19333.119 | 63668.223 | 63668.223 |
| queue_wait | 5158 | 2329.666 | 1947.647 | 4317.183 | 5619.711 | 6451.199 | 8151.039 |
| broker_dwell | 5158 | 409.466 | 149.247 | 1076.223 | 1686.527 | 3297.279 | 4640.767 |
| receive_call | 41374 | 104.98 | 47.903 | 333.823 | 483.839 | 498.687 | 510.719 |
| ctx_load | 5158 | 23.401 | 16.119 | 23.887 | 235.263 | 241.151 | 245.247 |
| process | 5158 | 12.734 | 0.003 | 0.006 | 0.017 | 2701.311 | 2949.119 |
| ctx_commit | 5158 | 26.582 | 21.375 | 39.167 | 70.143 | 750.591 | 2500.607 |
| ack | 5046 | 0.005 | 0.004 | 0.009 | 0.018 | 0.053 | 0.168 |
| service | 5158 | 62.768 | 37.823 | 61.887 | 279.295 | 2916.351 | 4878.335 |
| e2e_large | 9 | 47202.304 | 48070.655 | 51806.207 | 54689.791 | 54689.791 | 54689.791 |
| ready_wait | 5158 | 287.243 | 55.231 | 932.863 | 1314.815 | 5947.391 | 5951.487 |
| blocked | 5158 | 286.767 | 55.199 | 932.863 | 1314.815 | 5947.391 | 5951.487 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 604.58 | 1524.1 |
| applied_total | 29952.107 | 45882 |
| bench_cpu_cores | 1.172 | 1.53 |
| bench_cpu_pct | 14.654 | 19.12 |
| bench_gc_gen0 | 0.089 | 1 |
| bench_gc_gen1 | 0.089 | 1 |
| bench_gc_gen2 | 0.071 | 1 |
| bench_gc_heap_mb | 914.612 | 1371.6 |
| bench_mem_mb | 1513.823 | 1636.3 |
| bench_threadpool_queue | 0.357 | 19 |
| bench_threadpool_threads | 15.804 | 21 |
| broker_backlog | 6025.179 | 18270 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | -396.036 | 953 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 76782.679 | 118291 |
| octopus_missing_reports_total | 1084.411 | 1356 |
| octopus_mqtt_received_total | 55546.589 | 78903 |
| octopus_mqtt_sent_total | 33998.357 | 51014 |
| octopus_publish_retries_total | 5579.625 | 9547 |
| octopus_publish_stuck | 268.643 | 511 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.661 | 31 |
| pg_active_loads | 5.429 | 19 |
| pg_active_polls | 0.071 | 1 |
| pg_active_sends | 0.071 | 1 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 29.464 | 71 |
| pg_conn_idle | 233.786 | 263 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.25 | 42 |
| pg_conn_total | 263.25 | 264 |
| pg_db_size_mb | 3086.184 | 3308.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23968.611 | 27929.1 |
| pg_tup_del_per_s | 23712.675 | 32812.7 |
| pg_tup_fetch_per_s | 219872.248 | 276913.5 |
| pg_tup_ins_per_s | 28821.359 | 36976.8 |
| pg_tup_upd_per_s | 46791.991 | 55107.8 |
| pg_wal_mb_per_s | 79.774 | 105.1 |
| produced_per_s | 560.27 | 1762.2 |
| produced_total | 29556.071 | 45884 |
| publish_mb_per_s | 4.486 | 15.34 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 159,133 records are Completed |
| record_counts | pass | 159,133 records found, 9,856 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 172 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 51,259 events applied by workers, 51,259 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,967 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1315 ms, max 5950 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5996 seq 13: ready 207 ms before start, 207 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7235 seq 46: ready 161 ms before start, 161 ms of it with an idle slot in group 246
- [cross_source_blocking] source 5932 seq 17: ready 789 ms before start, 789 ms of it with an idle slot in group 206
- [cross_source_blocking] source 6028 seq 20: ready 789 ms before start, 789 ms of it with an idle slot in group 206
- [cross_source_blocking] source 2406 seq 14: ready 789 ms before start, 789 ms of it with an idle slot in group 70
- [cross_source_blocking] source 8021 seq 44: ready 789 ms before start, 789 ms of it with an idle slot in group 271
- [cross_source_blocking] source 5319 seq 29: ready 789 ms before start, 789 ms of it with an idle slot in group 182
- [cross_source_blocking] source 10653 seq 18: ready 804 ms before start, 804 ms of it with an idle slot in group 350
- [cross_source_blocking] source 9007 seq 7: ready 803 ms before start, 803 ms of it with an idle slot in group 311
- [cross_source_blocking] source 10068 seq 12: ready 803 ms before start, 803 ms of it with an idle slot in group 343
- [cross_source_blocking] source 14214 seq 19: ready 808 ms before start, 808 ms of it with an idle slot in group 455
- [cross_source_blocking] source 12824 seq 13: ready 1713 ms before start, 1713 ms of it with an idle slot in group 406
- [cross_source_blocking] source 15338 seq 9: ready 987 ms before start, 987 ms of it with an idle slot in group 479
- [cross_source_blocking] source 8021 seq 45: ready 912 ms before start, 912 ms of it with an idle slot in group 271
- [cross_source_blocking] source 5319 seq 30: ready 912 ms before start, 912 ms of it with an idle slot in group 182
- [cross_source_blocking] source 15641 seq 26: ready 996 ms before start, 996 ms of it with an idle slot in group 487
- [cross_source_blocking] source 5335 seq 22: ready 118 ms before start, 118 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5271 seq 21: ready 118 ms before start, 118 ms of it with an idle slot in group 182
- [cross_source_blocking] source 15778 seq 11: ready 118 ms before start, 118 ms of it with an idle slot in group 494
- [cross_source_blocking] source 3412 seq 6: ready 118 ms before start, 118 ms of it with an idle slot in group 118

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-054 | 17 | 1 | 66.663 | 19.09 | 161 | 0 | 179 | 179 | 0 | 41943.039 | 56098.815 | 56098.815 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 98 | 98 | 0 | 48234.495 | 54788.095 | 54788.095 |
| t-207 | 27 | 1 | 38.619 | 17.705 | 595 | 0 | 206 | 206 | 0 | 44564.479 | 54525.951 | 54525.951 |
| t-494 | 35 | 1 | 61.505 | 34.869 | 2036 | 0 | 358 | 358 | 0 | 44302.335 | 54525.951 | 54525.951 |
| t-022 | 63 | 2 | 56.223 | 59.231 | 162 | 0 | 621 | 621 | 0 | 43515.903 | 54263.807 | 54263.807 |
| t-038 | 9 | 1 | 82.676 | 12.428 | 2056 | 0 | 117 | 117 | 0 | 45875.199 | 54263.807 | 54263.807 |
| t-302 | 12 | 1 | 172.074 | 36.802 | 1189 | 0 | 316 | 316 | 0 | 41680.895 | 54263.807 | 54263.807 |
| t-366 | 55 | 1 | 32.483 | 30.213 | 2410 | 0 | 328 | 328 | 0 | 44302.335 | 54263.807 | 54263.807 |
| t-423 | 11 | 1 | 68.117 | 12.287 | 726 | 0 | 99 | 99 | 0 | 43253.759 | 54263.807 | 54263.807 |
| t-446 | 49 | 1 | 55.848 | 44.043 | 196 | 0 | 469 | 469 | 0 | 41418.751 | 54263.807 | 54263.807 |
| t-007 | 17 | 1 | 94.817 | 26.077 | 140 | 0 | 245 | 245 | 0 | 42991.615 | 54001.663 | 54001.663 |
| t-047 | 52 | 2 | 64.055 | 57.307 | 343 | 0 | 545 | 545 | 0 | 43515.903 | 54001.663 | 54263.807 |
| t-071 | 26 | 1 | 47.863 | 21.315 | 162 | 0 | 229 | 229 | 0 | 45613.055 | 54001.663 | 54001.663 |
| t-110 | 9 | 1 | 35.127 | 5.301 | 2746 | 0 | 68 | 68 | 0 | 45875.199 | 54001.663 | 54001.663 |
| t-134 | 21 | 1 | 36.452 | 12.488 | 3368 | 0 | 122 | 122 | 0 | 46399.487 | 54001.663 | 54001.663 |
| t-150 | 15 | 1 | 57.684 | 14.737 | 990 | 0 | 156 | 156 | 0 | 46399.487 | 54001.663 | 54001.663 |
| t-182 | 89 | 5 | 152.41 | 221.744 | 2406 | 0 | 1991 | 1991 | 0 | 43778.047 | 54001.663 | 54263.807 |
| t-318 | 7 | 1 | 59.085 | 7.206 | 686 | 0 | 60 | 60 | 0 | 43778.047 | 54001.663 | 54001.663 |
| t-334 | 19 | 1 | 115.991 | 37.119 | 391 | 0 | 327 | 327 | 0 | 43778.047 | 54001.663 | 54001.663 |
| t-358 | 22 | 1 | 50.133 | 18.037 | 310 | 0 | 202 | 202 | 0 | 44040.191 | 54001.663 | 54001.663 |
| t-415 | 14 | 1 | 128.891 | 29.793 | 2972 | 0 | 268 | 268 | 0 | 43778.047 | 54001.663 | 54001.663 |
| t-478 | 28 | 1 | 31.246 | 14.142 | 416 | 0 | 165 | 165 | 0 | 44302.335 | 54001.663 | 54001.663 |
| t-270 | 21 | 1 | 80.369 | 27.184 | 556 | 0 | 274 | 274 | 0 | 45613.055 | 53739.519 | 53739.519 |
| t-326 | 10 | 2 | 302.57 | 51.839 | 1837 | 0 | 408 | 408 | 0 | 45350.911 | 53739.519 | 53739.519 |
| t-014 | 34 | 1 | 58.61 | 31.844 | 214 | 0 | 292 | 292 | 0 | 40894.463 | 53215.231 | 53215.231 |
