# N28-332-s2 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.0.217:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 93.136s, closing 2.008s, drain 0.303s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 60891 | 60891 | 686.275 | 743.638 | 3047.6 | 0 | 0 | 0 | 70 | 334 | 455 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797 | 47527.394 | 45940.735 | 53116.927 | 56459.263 | 56983.551 | 57081.855 |
| e2e_from_send | 797 | 5093.72 | 3291.135 | 10461.183 | 10747.903 | 13131.775 | 13213.695 |
| producer_lag | 797 | 42433.841 | 41910.271 | 44597.247 | 56033.279 | 56950.783 | 56950.783 |
| publish_call | 60891 | 2178.62 | 719.359 | 4526.079 | 16769.023 | 29343.743 | 29360.127 |
| queue_wait | 797 | 5023.187 | 3207.167 | 10420.223 | 10665.983 | 11231.231 | 11436.031 |
| broker_dwell | 797 | 383.661 | 94.975 | 939.007 | 2654.207 | 10608.639 | 10649.599 |
| receive_call | 51326 | 90.513 | 46.847 | 270.847 | 476.671 | 497.151 | 505.343 |
| ctx_load | 797 | 20.761 | 15.151 | 27.503 | 226.431 | 234.111 | 239.103 |
| process | 797 | 21.14 | 0.003 | 0.007 | 0.059 | 2496.511 | 2738.175 |
| ctx_commit | 797 | 28.542 | 20.399 | 47.135 | 83.263 | 1157.119 | 1189.887 |
| ack | 775 | 0.005 | 0.004 | 0.01 | 0.02 | 0.027 | 0.043 |
| service | 797 | 70.494 | 38.911 | 72.959 | 258.431 | 2729.983 | 3907.583 |
| e2e_large | 2 | 55017.472 | 54984.703 | 55083.007 | 55083.007 | 55083.007 | 55083.007 |
| ready_wait | 797 | 2931.508 | 598.527 | 9838.591 | 10452.991 | 10665.983 | 10674.175 |
| blocked | 797 | 2930.637 | 598.015 | 9830.399 | 10452.991 | 10665.983 | 10674.175 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 743.638 | 2715.9 |
| applied_total | 40257.982 | 54829 |
| bench_cpu_cores | 1.206 | 1.778 |
| bench_cpu_pct | 15.074 | 22.22 |
| bench_gc_gen0 | 0.107 | 1 |
| bench_gc_gen1 | 0.107 | 1 |
| bench_gc_gen2 | 0.089 | 1 |
| bench_gc_heap_mb | 901.404 | 1256.8 |
| bench_mem_mb | 1576.377 | 1720 |
| bench_threadpool_queue | 0.321 | 14 |
| bench_threadpool_threads | 14.786 | 19 |
| broker_backlog | 10657.75 | 16722 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 142.857 | 3191 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 97632.589 | 137255 |
| octopus_missing_reports_total | 1599.482 | 2145 |
| octopus_mqtt_received_total | 67234.946 | 92947 |
| octopus_mqtt_sent_total | 43557.714 | 60636 |
| octopus_publish_retries_total | 3989.946 | 9111 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.036 | 41 |
| pg_active_loads | 8.946 | 25 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 44.429 | 82 |
| pg_conn_idle | 220.161 | 264 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.821 | 25 |
| pg_conn_total | 264.625 | 265 |
| pg_db_size_mb | 3264.516 | 3543.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25140.482 | 32279.5 |
| pg_tup_del_per_s | 26593.766 | 33665.1 |
| pg_tup_fetch_per_s | 262146.746 | 309120.2 |
| pg_tup_ins_per_s | 31717.673 | 39351 |
| pg_tup_upd_per_s | 52860.957 | 63355 |
| pg_wal_mb_per_s | 90.191 | 142.77 |
| produced_per_s | 686.275 | 2249.3 |
| produced_total | 40400.839 | 54401 |
| publish_mb_per_s | 4.659 | 13.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 181,685 records are Completed |
| record_counts | pass | 181,685 records found, 11,408 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 169 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 60,891 events applied by workers, 60,891 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 455 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 10453 ms, max 10671 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3742 seq 11: ready 125 ms before start, 125 ms of it with an idle slot in group 126
- [cross_source_blocking] source 3738 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 126
- [cross_source_blocking] source 5953 seq 15: ready 104 ms before start, 104 ms of it with an idle slot in group 206
- [cross_source_blocking] source 13791 seq 26: ready 135 ms before start, 135 ms of it with an idle slot in group 438
- [cross_source_blocking] source 13759 seq 26: ready 135 ms before start, 135 ms of it with an idle slot in group 438
- [cross_source_blocking] source 4017 seq 10: ready 736 ms before start, 736 ms of it with an idle slot in group 134
- [cross_source_blocking] source 7773 seq 20: ready 758 ms before start, 758 ms of it with an idle slot in group 263
- [cross_source_blocking] source 1534 seq 22: ready 869 ms before start, 869 ms of it with an idle slot in group 46
- [cross_source_blocking] source 12849 seq 15: ready 755 ms before start, 755 ms of it with an idle slot in group 406
- [cross_source_blocking] source 2792 seq 15: ready 735 ms before start, 735 ms of it with an idle slot in group 87
- [cross_source_blocking] source 4013 seq 8: ready 736 ms before start, 736 ms of it with an idle slot in group 134
- [cross_source_blocking] source 12288 seq 11: ready 187 ms before start, 187 ms of it with an idle slot in group 391
- [cross_source_blocking] source 6021 seq 17: ready 318 ms before start, 318 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7059 seq 40: ready 456 ms before start, 456 ms of it with an idle slot in group 239
- [cross_source_blocking] source 8038 seq 40: ready 189 ms before start, 189 ms of it with an idle slot in group 271
- [cross_source_blocking] source 11845 seq 27: ready 187 ms before start, 187 ms of it with an idle slot in group 383
- [cross_source_blocking] source 3076 seq 23: ready 187 ms before start, 187 ms of it with an idle slot in group 103
- [cross_source_blocking] source 3766 seq 8: ready 318 ms before start, 318 ms of it with an idle slot in group 126
- [cross_source_blocking] source 7737 seq 21: ready 195 ms before start, 195 ms of it with an idle slot in group 263
- [cross_source_blocking] source 8229 seq 26: ready 527 ms before start, 527 ms of it with an idle slot in group 278
- [cross_source_blocking] source 717 seq 13: ready 194 ms before start, 194 ms of it with an idle slot in group 22

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-095 | 6 | 1 | 118.583 | 11.97 | 925 | 0 | 109 | 109 | 0 | 57147.391 | 57147.391 | 57147.391 |
| t-246 | 32 | 5 | 407.356 | 215.226 | 494 | 0 | 1816 | 1816 | 0 | 52690.943 | 57147.391 | 57147.391 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 419 | 419 | 0 | 53477.375 | 57147.391 | 57147.391 |
| t-342 | 15 | 2 | 302.888 | 75.744 | 146 | 0 | 741 | 741 | 0 | 52953.087 | 57147.391 | 57147.391 |
| t-399 | 34 | 1 | 73.021 | 40.31 | 397 | 0 | 410 | 410 | 0 | 46661.631 | 57147.391 | 57147.391 |
| t-102 | 18 | 1 | 77.832 | 23.679 | 339 | 0 | 241 | 241 | 0 | 42991.615 | 56623.103 | 56623.103 |
| t-150 | 15 | 1 | 67.298 | 17.194 | 990 | 0 | 181 | 181 | 0 | 56360.959 | 56360.959 | 56360.959 |
| t-326 | 10 | 2 | 352.999 | 60.479 | 1837 | 0 | 515 | 515 | 0 | 44826.623 | 56360.959 | 56360.959 |
| t-343 | 63 | 2 | 84.032 | 85.035 | 518 | 0 | 833 | 833 | 0 | 52166.655 | 56360.959 | 56360.959 |
| t-182 | 89 | 6 | 177.811 | 258.701 | 2406 | 0 | 2341 | 2341 | 0 | 49807.359 | 56098.815 | 56098.815 |
| t-263 | 93 | 5 | 129.692 | 203.992 | 374 | 0 | 1990 | 1990 | 0 | 42991.615 | 56098.815 | 56098.815 |
| t-374 | 28 | 4 | 388.096 | 179.553 | 1811 | 0 | 1561 | 1561 | 0 | 44564.479 | 55836.671 | 55836.671 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 101 | 101 | 0 | 55050.239 | 55312.383 | 55312.383 |
| t-023 | 72 | 2 | 45.381 | 54.642 | 647 | 0 | 623 | 623 | 0 | 43515.903 | 53477.375 | 53477.375 |
| t-046 | 24 | 2 | 183.851 | 74.131 | 3235 | 0 | 646 | 646 | 0 | 51642.367 | 53477.375 | 53477.375 |
| t-054 | 17 | 1 | 77.773 | 22.272 | 161 | 0 | 215 | 215 | 0 | 45613.055 | 53477.375 | 53477.375 |
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 184 | 184 | 0 | 53477.375 | 53477.375 | 53477.375 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 967 | 967 | 0 | 42729.471 | 53477.375 | 53477.375 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 146 | 146 | 0 | 53477.375 | 53477.375 | 53477.375 |
| t-206 | 118 | 5 | 105.526 | 203.323 | 1969 | 0 | 1945 | 1945 | 0 | 42991.615 | 53477.375 | 53477.375 |
| t-214 | 52 | 2 | 81.989 | 73.477 | 688 | 0 | 759 | 759 | 0 | 42729.471 | 53477.375 | 53477.375 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 2949 | 2949 | 0 | 42991.615 | 53477.375 | 53477.375 |
| t-238 | 13 | 1 | 123.749 | 26.683 | 2335 | 0 | 250 | 250 | 0 | 50593.791 | 53477.375 | 53477.375 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 2416 | 2416 | 0 | 46661.631 | 53477.375 | 53477.375 |
| t-350 | 85 | 4 | 116.322 | 159.874 | 285 | 0 | 1578 | 1578 | 0 | 43253.759 | 53477.375 | 53477.375 |
