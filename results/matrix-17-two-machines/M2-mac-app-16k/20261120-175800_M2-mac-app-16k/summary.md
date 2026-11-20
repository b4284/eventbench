# M2-mac-app-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.248s, closing 1.367s, drain 16.447s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 692905 | 692905 | 9742.2 | 9413.639 | 21713.6 | 0 | 0 | 0 | 335 | 3551 | 64615 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 491510 | 7650.673 | 6062.079 | 19169.279 | 37322.751 | 41287.679 | 45842.431 |
| e2e_from_send | 491510 | 1099.303 | 125.567 | 1585.151 | 18530.303 | 35192.831 | 39223.295 |
| producer_lag | 491510 | 6551.372 | 5328.895 | 17842.175 | 26427.391 | 26853.375 | 27295.743 |
| publish_call | 692905 | 146.191 | 59.135 | 197.887 | 512.255 | 20791.295 | 20807.679 |
| queue_wait | 491510 | 1026.803 | 100.287 | 1404.927 | 18497.535 | 35192.831 | 39190.527 |
| broker_dwell | 491510 | 654.45 | 0.001 | 0.001 | 16392.191 | 33161.215 | 37158.911 |
| receive_call | 618885 | 35.406 | 12.567 | 93.247 | 313.343 | 467.967 | 503.295 |
| ctx_load | 491510 | 15.999 | 4.947 | 11.615 | 18.735 | 113.215 | 13885.439 |
| process | 491510 | 10.812 | 0.001 | 0.003 | 0.019 | 2598.911 | 3000.319 |
| ctx_commit | 491510 | 45.677 | 10.439 | 37.983 | 93.823 | 13541.375 | 15949.823 |
| ack | 415032 | 0.002 | 0.001 | 0.003 | 0.011 | 0.048 | 7.995 |
| service | 491510 | 72.499 | 17.183 | 45.951 | 275.967 | 13828.095 | 16744.447 |
| e2e_large | 233 | 9423.611 | 7901.183 | 21004.287 | 29212.671 | 43646.975 | 43646.975 |
| ready_wait | 491510 | 175.294 | 9.615 | 198.271 | 1772.543 | 19742.719 | 35749.887 |
| blocked | 491510 | 136.807 | 9.319 | 135.423 | 1342.463 | 19742.719 | 35454.975 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9413.639 | 21490 |
| applied_total | 436973 | 634642 |
| bench_cpu_cores | 2.854 | 5.38 |
| bench_cpu_pct | 35.672 | 67.25 |
| bench_gc_gen0 | 0.694 | 2 |
| bench_gc_gen1 | 0.612 | 2 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1071.824 | 1894.1 |
| bench_mem_mb | 2108.953 | 2419.8 |
| bench_threadpool_queue | 0.122 | 1 |
| bench_threadpool_threads | 20.041 | 28 |
| broker_backlog | 6589.265 | 20825 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 5305.98 | 18268 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 882188.102 | 1338811 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 131348.898 | 194513 |
| octopus_mqtt_sent_total | 222528.122 | 330793 |
| octopus_publish_retries_total | 1937 | 8167 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.469 | 16 |
| pg_active_loads | 3.633 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 9.102 | 23 |
| pg_conn_idle | 238.98 | 248 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 248.082 | 249 |
| pg_db_size_mb | 3428.135 | 3693.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19685.967 | 26210 |
| pg_tup_del_per_s | 39346.943 | 53007.1 |
| pg_tup_fetch_per_s | 527310.347 | 711608.6 |
| pg_tup_ins_per_s | 53529.506 | 64288.4 |
| pg_tup_upd_per_s | 38804.243 | 54589.8 |
| pg_wal_mb_per_s | 105.788 | 216.28 |
| produced_per_s | 9742.2 | 22545 |
| produced_total | 442278.98 | 652910 |
| publish_mb_per_s | 32.674 | 74.39 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 103,190 records are Completed |
| record_counts | pass | 103,190 records found, 103,190 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 335 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 692,905 events applied by workers, 692,905 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 64,615 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1342 ms, max 35446 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 121 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 1
- [cross_source_blocking] source 11949 seq 19: ready 105 ms before start, 105 ms of it with an idle slot in group 383
- [cross_source_blocking] source 669 seq 21: ready 105 ms before start, 105 ms of it with an idle slot in group 19
- [cross_source_blocking] source 15393 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 482
- [cross_source_blocking] source 7673 seq 2: ready 105 ms before start, 105 ms of it with an idle slot in group 262
- [cross_source_blocking] source 10653 seq 18: ready 105 ms before start, 105 ms of it with an idle slot in group 350
- [cross_source_blocking] source 4245 seq 5: ready 105 ms before start, 105 ms of it with an idle slot in group 142
- [cross_source_blocking] source 10281 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 344
- [cross_source_blocking] source 2233 seq 37: ready 106 ms before start, 106 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4857 seq 10: ready 105 ms before start, 105 ms of it with an idle slot in group 168
- [cross_source_blocking] source 9001 seq 18: ready 105 ms before start, 105 ms of it with an idle slot in group 310
- [cross_source_blocking] source 4417 seq 7: ready 105 ms before start, 105 ms of it with an idle slot in group 152
- [cross_source_blocking] source 11957 seq 25: ready 105 ms before start, 105 ms of it with an idle slot in group 383
- [cross_source_blocking] source 3213 seq 9: ready 105 ms before start, 105 ms of it with an idle slot in group 107
- [cross_source_blocking] source 11645 seq 10: ready 107 ms before start, 107 ms of it with an idle slot in group 378
- [cross_source_blocking] source 11033 seq 22: ready 105 ms before start, 105 ms of it with an idle slot in group 360
- [cross_source_blocking] source 13361 seq 11: ready 105 ms before start, 105 ms of it with an idle slot in group 428
- [cross_source_blocking] source 9293 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 320
- [cross_source_blocking] source 5181 seq 8: ready 105 ms before start, 105 ms of it with an idle slot in group 179
- [cross_source_blocking] source 6221 seq 9: ready 105 ms before start, 105 ms of it with an idle slot in group 212

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-377 | 17 | 1 | 51.208 | 13.841 | 200 | 0 | 594 | 594 | 0 | 5505.023 | 41680.895 | 42991.615 |
| t-001 | 118 | 2 | 36.72 | 71.345 | 154 | 0 | 3220 | 3220 | 0 | 5865.471 | 41418.751 | 42467.327 |
| t-043 | 27 | 1 | 34.096 | 15.236 | 491 | 0 | 734 | 734 | 0 | 5701.631 | 41418.751 | 41943.039 |
| t-111 | 18 | 1 | 98.15 | 29.411 | 3401 | 0 | 1263 | 1263 | 0 | 6225.919 | 41418.751 | 41943.039 |
| t-155 | 14 | 1 | 18.091 | 4.112 | 2759 | 0 | 195 | 195 | 0 | 1351.679 | 41418.751 | 41680.895 |
| t-307 | 16 | 1 | 32.741 | 8.834 | 200 | 0 | 397 | 397 | 0 | 6193.151 | 41418.751 | 41943.039 |
| t-402 | 31 | 1 | 42.399 | 22.299 | 1454 | 0 | 984 | 984 | 0 | 6094.847 | 41418.751 | 43778.047 |
| t-027 | 62 | 1 | 34.536 | 34.675 | 935 | 0 | 1505 | 1505 | 0 | 5963.775 | 41156.607 | 42729.471 |
| t-037 | 21 | 1 | 91.657 | 32.167 | 272 | 0 | 1403 | 1403 | 0 | 5832.703 | 41156.607 | 41680.895 |
| t-068 | 21 | 1 | 24.454 | 8.398 | 3531 | 0 | 389 | 389 | 0 | 6062.079 | 41156.607 | 41418.751 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 510 | 510 | 0 | 6389.759 | 41156.607 | 41418.751 |
| t-165 | 36 | 1 | 68.253 | 40.726 | 373 | 0 | 1785 | 1785 | 0 | 6193.151 | 41156.607 | 43515.903 |
| t-176 | 11 | 1 | 51.203 | 8.885 | 1581 | 0 | 407 | 407 | 0 | 6127.615 | 41156.607 | 41943.039 |
| t-188 | 27 | 1 | 49.246 | 22.747 | 599 | 0 | 1015 | 1015 | 0 | 6127.615 | 41156.607 | 41680.895 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 595 | 595 | 0 | 5767.167 | 41156.607 | 41943.039 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 754 | 754 | 0 | 6225.919 | 41156.607 | 41418.751 |
| t-268 | 41 | 1 | 45.858 | 30.087 | 1430 | 0 | 1359 | 1359 | 0 | 5963.775 | 41156.607 | 41943.039 |
| t-279 | 18 | 1 | 44.014 | 12.994 | 196 | 0 | 578 | 578 | 0 | 4882.431 | 41156.607 | 41680.895 |
| t-294 | 7 | 1 | 133.744 | 15.435 | 667 | 0 | 688 | 688 | 0 | 5996.543 | 41156.607 | 41943.039 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2059 | 2059 | 0 | 6029.311 | 41156.607 | 41943.039 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 136 | 136 | 0 | 5799.935 | 41156.607 | 41156.607 |
| t-359 | 52 | 1 | 31.374 | 26.945 | 132 | 0 | 1248 | 1248 | 0 | 5636.095 | 41156.607 | 42205.183 |
| t-366 | 55 | 1 | 21.655 | 20.142 | 2410 | 0 | 957 | 957 | 0 | 5701.631 | 41156.607 | 41943.039 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 3523 | 3523 | 0 | 6094.847 | 41156.607 | 42205.183 |
| t-444 | 16 | 1 | 43.944 | 11.89 | 188 | 0 | 496 | 496 | 0 | 5832.703 | 41156.607 | 41418.751 |
