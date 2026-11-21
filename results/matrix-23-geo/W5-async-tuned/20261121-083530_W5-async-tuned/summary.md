# W5-async-tuned — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.396s, closing 2.889s, drain 16.98s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 647328 | 647328 | 9563.545 | 8208.441 | 16127.8 | 0 | 0 | 0 | 348 | 3320 | 313191 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 476843 | 15931.719 | 12902.399 | 31293.439 | 40075.263 | 44957.695 | 52985.855 |
| e2e_from_send | 476843 | 8972.352 | 5521.407 | 23363.583 | 33619.967 | 40042.495 | 50069.503 |
| producer_lag | 476843 | 6959.366 | 7221.247 | 11263.999 | 14458.879 | 15089.663 | 15507.455 |
| publish_call | 647328 | 168.082 | 113.855 | 299.007 | 1150.975 | 2109.439 | 2590.719 |
| queue_wait | 476843 | 8834.856 | 5373.951 | 23265.279 | 33505.279 | 39944.191 | 50003.967 |
| broker_dwell | 476843 | 8663.205 | 5189.631 | 23085.055 | 33341.439 | 39747.583 | 50003.967 |
| receive_call | 565304 | 32.863 | 9.463 | 88.959 | 366.847 | 488.191 | 564.223 |
| ctx_load | 476843 | 14.408 | 8.807 | 31.599 | 75.519 | 208.895 | 1153.023 |
| process | 476843 | 10.808 | 0.002 | 0.005 | 0.055 | 2631.679 | 3028.991 |
| ctx_commit | 476843 | 112.171 | 64.127 | 150.655 | 1564.671 | 2254.847 | 3162.111 |
| ack | 476830 | 0.01 | 0.004 | 0.007 | 0.043 | 0.905 | 55.711 |
| service | 476843 | 137.491 | 79.039 | 176.639 | 1953.791 | 3162.111 | 5730.303 |
| e2e_large | 248 | 9397.566 | 9568.255 | 14655.487 | 17219.583 | 18022.399 | 18022.399 |
| ready_wait | 476843 | 766.526 | 372.479 | 2051.071 | 4657.151 | 7294.975 | 11280.383 |
| blocked | 476843 | 309.616 | 197.119 | 762.879 | 1456.127 | 2187.263 | 4857.855 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8208.441 | 14353.1 |
| applied_total | 301855.429 | 494620 |
| bench_cpu_cores | 2.797 | 4.567 |
| bench_cpu_pct | 11.655 | 19.03 |
| bench_gc_gen0 | 1.633 | 4 |
| bench_gc_gen1 | 0.612 | 2 |
| bench_gc_gen2 | 0.449 | 1 |
| bench_gc_heap_mb | 1247.763 | 1549.7 |
| bench_mem_mb | 1949.62 | 2223.4 |
| bench_threadpool_queue | 75.429 | 825 |
| bench_threadpool_threads | 43.98 | 44 |
| broker_backlog | 73768 | 107989 |
| ctr_eb_mosquitto_cpu_pct | 37.856 | 52.07 |
| ctr_eb_mosquitto_mem_mb | 5.247 | 8.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.032 | 0.07 |
| ctr_eb_pgbouncer_2_mem_mb | 7.767 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.028 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 8.3 | 8.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.026 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 7.724 | 8 |
| ctr_eb_pgbouncer_cpu_pct | 0.028 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 8.492 | 8.7 |
| ctr_eb_postgres_cpu_pct | 1.453 | 3.01 |
| ctr_eb_postgres_mem_mb | 2192.81 | 2196.3 |
| ctr_geo_dc1a_cpu_pct | 793.602 | 973.69 |
| ctr_geo_dc1a_mem_mb | 8805.924 | 9870.5 |
| ctr_geo_dc1b_cpu_pct | 55.147 | 74.17 |
| ctr_geo_dc1b_mem_mb | 3555.871 | 3926.3 |
| ctr_geo_etcd_dc1_cpu_pct | 2.12 | 2.67 |
| ctr_geo_etcd_dc1_mem_mb | 56.651 | 58 |
| ctr_geo_haproxy_cpu_pct | 124.496 | 151.3 |
| ctr_geo_haproxy_mem_mb | 32.645 | 37 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 61.609 | 81.88 |
| in_flight | 74213.755 | 113439 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.336 | 1.55 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 670108 | 1097119 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 168583.857 | 264140 |
| octopus_mqtt_sent_total | 183803.612 | 309704 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.429 | 29 |
| pg_active_loads | 4.918 | 60 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.962 | 100 |
| pg_conn_active | 50.184 | 72 |
| pg_conn_idle | 217.592 | 241 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.469 | 12 |
| pg_conn_total | 267.776 | 268 |
| pg_db_size_mb | 2146.137 | 3265.4 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 32765.956 | 34010.63 |
| pg_replica_replay_lag_ms | 97510.553 | 120244.8 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20501.89 | 26583.6 |
| pg_tup_del_per_s | 26590.91 | 37912.5 |
| pg_tup_fetch_per_s | 413912.031 | 625166.6 |
| pg_tup_ins_per_s | 37714.567 | 52815.4 |
| pg_tup_upd_per_s | 23673.098 | 31253.5 |
| pg_wal_mb_per_s | 68.618 | 96.3 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 9563.545 | 16665.9 |
| produced_total | 376069.184 | 608059 |
| publish_mb_per_s | 33.766 | 65.45 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 98,119 records are Completed |
| record_counts | pass | 98,119 records found, 98,119 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 348 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 647,328 events applied by workers, 647,328 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 313,191 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1456 ms, max 4855 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8228 seq 11: ready 101 ms before start, 101 ms of it with an idle slot in group 278
- [cross_source_blocking] source 7112 seq 2: ready 103 ms before start, 103 ms of it with an idle slot in group 241
- [cross_source_blocking] source 13014 seq 14: ready 169 ms before start, 169 ms of it with an idle slot in group 414
- [cross_source_blocking] source 2880 seq 4: ready 171 ms before start, 171 ms of it with an idle slot in group 91
- [cross_source_blocking] source 14894 seq 6: ready 136 ms before start, 136 ms of it with an idle slot in group 467
- [cross_source_blocking] source 7394 seq 5: ready 171 ms before start, 171 ms of it with an idle slot in group 255
- [cross_source_blocking] source 5346 seq 3: ready 137 ms before start, 137 ms of it with an idle slot in group 183
- [cross_source_blocking] source 13212 seq 13: ready 148 ms before start, 148 ms of it with an idle slot in group 421
- [cross_source_blocking] source 13984 seq 10: ready 168 ms before start, 168 ms of it with an idle slot in group 444
- [cross_source_blocking] source 9206 seq 3: ready 136 ms before start, 136 ms of it with an idle slot in group 316
- [cross_source_blocking] source 12120 seq 5: ready 141 ms before start, 141 ms of it with an idle slot in group 387
- [cross_source_blocking] source 8096 seq 18: ready 119 ms before start, 119 ms of it with an idle slot in group 273
- [cross_source_blocking] source 7316 seq 8: ready 170 ms before start, 170 ms of it with an idle slot in group 251
- [cross_source_blocking] source 4528 seq 5: ready 157 ms before start, 157 ms of it with an idle slot in group 157
- [cross_source_blocking] source 404 seq 12: ready 171 ms before start, 171 ms of it with an idle slot in group 12
- [cross_source_blocking] source 2332 seq 18: ready 171 ms before start, 171 ms of it with an idle slot in group 67
- [cross_source_blocking] source 11607 seq 8: ready 135 ms before start, 135 ms of it with an idle slot in group 377
- [cross_source_blocking] source 8130 seq 10: ready 136 ms before start, 136 ms of it with an idle slot in group 275
- [cross_source_blocking] source 8098 seq 25: ready 168 ms before start, 168 ms of it with an idle slot in group 273
- [cross_source_blocking] source 9468 seq 12: ready 171 ms before start, 171 ms of it with an idle slot in group 327

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 7217 | 7217 | 0 | 30146.559 | 47710.207 | 51380.223 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2381 | 2381 | 0 | 25427.967 | 47185.919 | 53215.231 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2319 | 2319 | 0 | 27394.047 | 46399.487 | 48758.783 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2310 | 2310 | 0 | 27918.335 | 46399.487 | 51380.223 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2228 | 2228 | 0 | 21889.023 | 45875.199 | 50331.647 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5042 | 5042 | 0 | 29884.415 | 45350.911 | 50069.503 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2293 | 2293 | 0 | 29229.055 | 45088.767 | 46137.343 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5077 | 5077 | 0 | 30277.631 | 44826.623 | 48758.783 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4734 | 4734 | 0 | 29229.055 | 44826.623 | 48496.639 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4679 | 4679 | 0 | 28180.479 | 44826.623 | 47972.351 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2253 | 2253 | 0 | 25296.895 | 44040.191 | 47448.063 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 4521 | 4521 | 0 | 26476.543 | 44040.191 | 47972.351 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2358 | 2358 | 0 | 26869.759 | 43778.047 | 46137.343 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 6861 | 6861 | 0 | 25952.255 | 43778.047 | 48496.639 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 7040 | 7040 | 0 | 27000.831 | 42991.615 | 48234.495 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2142 | 2142 | 0 | 23855.103 | 42991.615 | 46661.631 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4766 | 4766 | 0 | 29097.983 | 42991.615 | 44826.623 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2305 | 2305 | 0 | 25165.823 | 42991.615 | 48234.495 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2309 | 2309 | 0 | 25952.255 | 42991.615 | 44826.623 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2358 | 2358 | 0 | 28049.407 | 42991.615 | 45350.911 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 9647 | 9647 | 0 | 28704.767 | 42729.471 | 46137.343 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 9623 | 9623 | 0 | 30277.631 | 42467.327 | 44564.479 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2400 | 2400 | 0 | 30015.487 | 42467.327 | 44826.623 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2132 | 2132 | 0 | 24772.607 | 41943.039 | 43253.759 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 6179 | 6179 | 0 | 22151.167 | 41943.039 | 46661.631 |
