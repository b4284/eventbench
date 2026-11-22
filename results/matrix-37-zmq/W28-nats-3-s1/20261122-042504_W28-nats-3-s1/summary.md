# W28-nats-3-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.31.128:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.56s, closing 1.084s, drain 7.397s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 427006 | 427006 | 6911.476 | 6906.418 | 12011.9 | 0 | 0 | 0 | 71 | 2062 | 13769 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 304630 | 14894.343 | 14327.807 | 21954.559 | 25493.503 | 26591.231 | 28180.479 |
| e2e_from_send | 304630 | 454.003 | 388.863 | 739.839 | 1945.599 | 3258.367 | 6488.063 |
| producer_lag | 304630 | 14440.346 | 13901.823 | 21512.191 | 25051.135 | 26132.479 | 26853.375 |
| publish_call | 427006 | 291.094 | 250.239 | 561.151 | 1029.119 | 1563.647 | 2303.999 |
| queue_wait | 304630 | 377.613 | 320.255 | 660.991 | 1497.087 | 2947.071 | 6381.567 |
| broker_dwell | 304630 | 86.961 | 17.551 | 204.415 | 1226.751 | 2691.071 | 6082.559 |
| receive_call | 381362 | 41.596 | 17.647 | 108.607 | 321.023 | 468.479 | 504.831 |
| ctx_load | 304630 | 14.998 | 10.719 | 27.151 | 129.279 | 153.983 | 415.999 |
| process | 304630 | 10.055 | 0.001 | 0.002 | 0.007 | 2580.479 | 3006.463 |
| ctx_commit | 304630 | 51.31 | 35.231 | 94.847 | 322.303 | 504.319 | 1797.119 |
| ack | 249003 | 0.002 | 0.001 | 0.003 | 0.008 | 0.024 | 33.055 |
| service | 304630 | 76.391 | 50.015 | 122.367 | 378.623 | 2646.015 | 4179.967 |
| e2e_large | 47 | 16563.091 | 16318.463 | 22282.239 | 25477.119 | 25477.119 | 25477.119 |
| ready_wait | 304630 | 33.43 | 12.287 | 89.087 | 282.111 | 540.159 | 1196.031 |
| blocked | 304630 | 25.58 | 11.791 | 58.815 | 228.479 | 485.887 | 1196.031 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6906.418 | 9350.3 |
| applied_total | 233811.837 | 408071 |
| bench_cpu_cores | 1.57 | 2.407 |
| bench_cpu_pct | 15.703 | 24.07 |
| bench_gc_gen0 | 0.224 | 1 |
| bench_gc_gen1 | 0.122 | 1 |
| bench_gc_gen2 | 0.122 | 1 |
| bench_gc_heap_mb | 1556.806 | 2714.2 |
| bench_mem_mb | 2446.478 | 2713.9 |
| bench_threadpool_queue | 12.796 | 433 |
| bench_threadpool_threads | 19.469 | 27 |
| broker_backlog | 28130.245 | 51555 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1097.286 | 2910 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 474519.612 | 816744 |
| octopus_missing_reports_total | 18073.735 | 29721 |
| octopus_mqtt_received_total | 106391.347 | 166058 |
| octopus_mqtt_sent_total | 198631.429 | 334770 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 19.694 | 40 |
| pg_active_loads | 12.857 | 50 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 60.898 | 106 |
| pg_conn_idle | 202.286 | 233 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.714 | 41 |
| pg_conn_total | 263.184 | 264 |
| pg_db_size_mb | 3767.686 | 4239.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26666.714 | 31035.2 |
| pg_tup_del_per_s | 34291.198 | 45172.2 |
| pg_tup_fetch_per_s | 305312.298 | 333106 |
| pg_tup_ins_per_s | 46356.2 | 52526.1 |
| pg_tup_upd_per_s | 56954.373 | 64178.6 |
| pg_wal_mb_per_s | 133.11 | 244.64 |
| produced_per_s | 6911.476 | 10072.4 |
| produced_total | 234909.122 | 409036 |
| publish_mb_per_s | 16.791 | 24.86 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 190,019 records are Completed |
| record_counts | pass | 190,019 records found, 58,920 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 374 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 427,006 events applied by workers, 427,006 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 13,769 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 228 ms, max 1196 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10403 seq 25: ready 139 ms before start, 123 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10435 seq 20: ready 131 ms before start, 116 ms of it with an idle slot in group 347
- [cross_source_blocking] source 14550 seq 20: ready 215 ms before start, 215 ms of it with an idle slot in group 461
- [cross_source_blocking] source 240 seq 32: ready 194 ms before start, 194 ms of it with an idle slot in group 5
- [cross_source_blocking] source 15600 seq 35: ready 116 ms before start, 116 ms of it with an idle slot in group 484
- [cross_source_blocking] source 13951 seq 20: ready 389 ms before start, 389 ms of it with an idle slot in group 443
- [cross_source_blocking] source 5223 seq 58: ready 155 ms before start, 155 ms of it with an idle slot in group 181
- [cross_source_blocking] source 3922 seq 28: ready 101 ms before start, 101 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3838 seq 23: ready 100 ms before start, 100 ms of it with an idle slot in group 131
- [cross_source_blocking] source 2057 seq 8: ready 100 ms before start, 100 ms of it with an idle slot in group 61
- [cross_source_blocking] source 6899 seq 25: ready 123 ms before start, 123 ms of it with an idle slot in group 235
- [cross_source_blocking] source 5905 seq 15: ready 123 ms before start, 123 ms of it with an idle slot in group 204
- [cross_source_blocking] source 4513 seq 15: ready 113 ms before start, 113 ms of it with an idle slot in group 157
- [cross_source_blocking] source 7934 seq 15: ready 123 ms before start, 123 ms of it with an idle slot in group 268
- [cross_source_blocking] source 2567 seq 34: ready 112 ms before start, 112 ms of it with an idle slot in group 77
- [cross_source_blocking] source 10445 seq 17: ready 123 ms before start, 123 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3263 seq 16: ready 122 ms before start, 122 ms of it with an idle slot in group 109
- [cross_source_blocking] source 15756 seq 12: ready 123 ms before start, 123 ms of it with an idle slot in group 492
- [cross_source_blocking] source 262 seq 27: ready 123 ms before start, 123 ms of it with an idle slot in group 5
- [cross_source_blocking] source 7184 seq 34: ready 117 ms before start, 117 ms of it with an idle slot in group 245

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-452 | 6 | 1 | 44.703 | 4.676 | 1797 | 0 | 190 | 190 | 0 | 14614.527 | 26476.543 | 26476.543 |
| t-492 | 6 | 1 | 56.484 | 5.146 | 3046 | 0 | 243 | 243 | 0 | 13959.167 | 26476.543 | 26738.687 |
| t-077 | 10 | 2 | 311.624 | 52.01 | 1566 | 0 | 2064 | 2064 | 0 | 14352.383 | 26345.471 | 27131.903 |
| t-173 | 12 | 1 | 91.453 | 19.263 | 3493 | 0 | 878 | 878 | 0 | 13959.167 | 26214.399 | 27000.831 |
| t-484 | 6 | 1 | 162.625 | 16.461 | 232 | 0 | 666 | 666 | 0 | 14614.527 | 26214.399 | 27131.903 |
| t-164 | 7 | 1 | 221.485 | 23.812 | 236 | 0 | 966 | 966 | 0 | 14090.239 | 26083.327 | 26607.615 |
| t-179 | 25 | 1 | 51.101 | 21.881 | 989 | 0 | 931 | 931 | 0 | 15007.743 | 26083.327 | 27000.831 |
| t-251 | 9 | 1 | 82.187 | 12.347 | 1193 | 0 | 499 | 499 | 0 | 14286.847 | 26083.327 | 27000.831 |
| t-349 | 14 | 1 | 96.442 | 21.772 | 379 | 0 | 895 | 895 | 0 | 14221.311 | 26083.327 | 26607.615 |
| t-355 | 10 | 1 | 65.589 | 11.499 | 349 | 0 | 463 | 463 | 0 | 14155.775 | 26083.327 | 27000.831 |
| t-396 | 17 | 1 | 89.956 | 25.946 | 210 | 0 | 1011 | 1011 | 0 | 14352.383 | 26083.327 | 27131.903 |
| t-419 | 17 | 1 | 44.454 | 13.165 | 3214 | 0 | 541 | 541 | 0 | 14090.239 | 26083.327 | 27131.903 |
| t-444 | 16 | 1 | 76.901 | 20.808 | 188 | 0 | 859 | 859 | 0 | 14548.991 | 26083.327 | 27000.831 |
| t-485 | 12 | 1 | 69.507 | 13.448 | 880 | 0 | 562 | 562 | 0 | 13369.343 | 26083.327 | 26345.471 |
| t-181 | 35 | 4 | 322.682 | 186.423 | 1367 | 0 | 7373 | 7373 | 0 | 14352.383 | 25952.255 | 27131.903 |
| t-365 | 13 | 1 | 76.152 | 16.433 | 3428 | 0 | 672 | 672 | 0 | 14483.455 | 25952.255 | 26869.759 |
| t-380 | 22 | 2 | 168.026 | 60.126 | 132 | 0 | 2456 | 2456 | 0 | 14155.775 | 25952.255 | 26869.759 |
| t-413 | 19 | 1 | 150.868 | 49.856 | 3585 | 0 | 1978 | 1978 | 0 | 14090.239 | 25952.255 | 26738.687 |
| t-139 | 16 | 1 | 38.195 | 10.791 | 1367 | 0 | 491 | 491 | 0 | 14090.239 | 25821.183 | 27000.831 |
| t-204 | 37 | 1 | 39.588 | 24.436 | 1822 | 0 | 1060 | 1060 | 0 | 14548.991 | 25821.183 | 26607.615 |
| t-229 | 28 | 1 | 51.872 | 24.225 | 1675 | 0 | 1094 | 1094 | 0 | 14155.775 | 25821.183 | 27000.831 |
| t-243 | 16 | 1 | 128.845 | 34.514 | 3143 | 0 | 1417 | 1417 | 0 | 14155.775 | 25821.183 | 27000.831 |
| t-300 | 25 | 1 | 44.966 | 19.032 | 372 | 0 | 855 | 855 | 0 | 14024.703 | 25821.183 | 26607.615 |
| t-316 | 19 | 1 | 58.258 | 18.542 | 169 | 0 | 752 | 752 | 0 | 14090.239 | 25821.183 | 26345.471 |
| t-324 | 15 | 1 | 47.791 | 11.104 | 131 | 0 | 485 | 485 | 0 | 15400.959 | 25821.183 | 26476.543 |
