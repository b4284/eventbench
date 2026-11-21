# E24-332-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.488s, closing 1.524s, drain 35.192s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 380848 | 380848 | 6003.218 | 5997.424 | 9394.5 | 0 | 0 | 0 | 74 | 1843 | 33015 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 273666 | 13237.443 | 13860.863 | 19185.663 | 23117.823 | 24690.687 | 27082.751 |
| e2e_from_send | 273666 | 616.452 | 509.695 | 1061.887 | 2859.007 | 4198.399 | 6479.871 |
| producer_lag | 273666 | 12620.996 | 13336.575 | 18563.071 | 22511.615 | 24199.167 | 24526.847 |
| publish_call | 380848 | 327.621 | 279.295 | 669.183 | 1168.383 | 1886.207 | 2924.543 |
| queue_wait | 273666 | 533.309 | 431.359 | 954.367 | 2633.727 | 4042.751 | 6295.551 |
| broker_dwell | 273666 | 214.663 | 57.215 | 509.183 | 2387.967 | 3842.047 | 6217.727 |
| receive_call | 343530 | 39.974 | 15.591 | 105.535 | 326.399 | 466.687 | 529.919 |
| ctx_load | 273666 | 16.439 | 11.799 | 31.103 | 130.111 | 162.175 | 636.415 |
| process | 273666 | 10.158 | 0.001 | 0.002 | 0.007 | 2621.439 | 2998.271 |
| ctx_commit | 273666 | 56.515 | 38.079 | 111.231 | 330.751 | 555.007 | 2291.711 |
| ack | 227140 | 0.002 | 0.001 | 0.003 | 0.012 | 0.036 | 33.247 |
| service | 273666 | 83.143 | 54.783 | 139.647 | 379.135 | 2711.551 | 4530.175 |
| e2e_large | 51 | 15672.701 | 15974.399 | 21725.183 | 23560.191 | 26001.407 | 26001.407 |
| ready_wait | 273666 | 67.3 | 16.415 | 189.439 | 620.031 | 938.495 | 2232.319 |
| blocked | 273666 | 43.92 | 15.319 | 116.479 | 371.199 | 625.151 | 1628.159 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5997.424 | 8194.6 |
| applied_total | 211026.22 | 363329 |
| bench_cpu_cores | 1.522 | 2.315 |
| bench_cpu_pct | 15.219 | 23.15 |
| bench_gc_gen0 | 0.3 | 2 |
| bench_gc_gen1 | 0.2 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 1189.576 | 2037.4 |
| bench_mem_mb | 2094.388 | 2400.2 |
| bench_threadpool_queue | 1.6 | 49 |
| bench_threadpool_threads | 17.74 | 19 |
| broker_backlog | 47638.4 | 85336 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1616.14 | 7399 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 430190.98 | 752717 |
| octopus_missing_reports_total | 21895.92 | 33922 |
| octopus_mqtt_received_total | 118660.8 | 196526 |
| octopus_mqtt_sent_total | 167635.8 | 286642 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 25.56 | 47 |
| pg_active_loads | 18.94 | 179 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.951 | 100 |
| pg_conn_active | 76.86 | 206 |
| pg_conn_idle | 187.96 | 223 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 8.94 | 45 |
| pg_conn_total | 264.82 | 267 |
| pg_db_size_mb | 3747.318 | 4403 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22313.148 | 24473.7 |
| pg_tup_del_per_s | 31262.344 | 37888.3 |
| pg_tup_fetch_per_s | 281910.158 | 365731.2 |
| pg_tup_ins_per_s | 43001.63 | 50450.3 |
| pg_tup_upd_per_s | 46810.846 | 51998 |
| pg_wal_mb_per_s | 129.582 | 256.34 |
| produced_per_s | 6003.218 | 9808.3 |
| produced_total | 212642.36 | 364735 |
| publish_mb_per_s | 15.134 | 26.28 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 167,902 records are Completed |
| record_counts | pass | 167,902 records found, 53,583 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 405 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 380,848 events applied by workers, 380,848 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 33,015 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 371 ms, max 1628 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9169 seq 23: ready 155 ms before start, 111 ms of it with an idle slot in group 315
- [cross_source_blocking] source 5530 seq 22: ready 129 ms before start, 119 ms of it with an idle slot in group 189
- [cross_source_blocking] source 10567 seq 15: ready 150 ms before start, 134 ms of it with an idle slot in group 348
- [cross_source_blocking] source 9853 seq 8: ready 122 ms before start, 109 ms of it with an idle slot in group 340
- [cross_source_blocking] source 6977 seq 22: ready 196 ms before start, 139 ms of it with an idle slot in group 237
- [cross_source_blocking] source 9757 seq 11: ready 129 ms before start, 119 ms of it with an idle slot in group 340
- [cross_source_blocking] source 14966 seq 21: ready 169 ms before start, 147 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15030 seq 30: ready 172 ms before start, 150 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15540 seq 14: ready 128 ms before start, 128 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10479 seq 18: ready 231 ms before start, 178 ms of it with an idle slot in group 347
- [cross_source_blocking] source 13955 seq 21: ready 245 ms before start, 124 ms of it with an idle slot in group 443
- [cross_source_blocking] source 14962 seq 25: ready 193 ms before start, 112 ms of it with an idle slot in group 469
- [cross_source_blocking] source 10435 seq 19: ready 195 ms before start, 123 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10415 seq 10: ready 195 ms before start, 123 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10343 seq 21: ready 195 ms before start, 123 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10347 seq 25: ready 212 ms before start, 129 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10567 seq 16: ready 272 ms before start, 181 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10375 seq 21: ready 210 ms before start, 126 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10447 seq 24: ready 209 ms before start, 137 ms of it with an idle slot in group 347
- [cross_source_blocking] source 15006 seq 20: ready 195 ms before start, 114 ms of it with an idle slot in group 469

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 70 | 70 | 0 | 16646.143 | 26083.327 | 26083.327 |
| t-355 | 10 | 1 | 56.219 | 9.856 | 349 | 0 | 447 | 447 | 0 | 13959.167 | 24772.607 | 24903.679 |
| t-099 | 13 | 1 | 92.021 | 21.193 | 419 | 0 | 892 | 892 | 0 | 14221.311 | 24641.535 | 25034.751 |
| t-196 | 21 | 2 | 175.497 | 62.027 | 2244 | 0 | 2671 | 2671 | 0 | 13959.167 | 24641.535 | 25165.823 |
| t-059 | 8 | 1 | 118.69 | 15.692 | 1449 | 0 | 618 | 618 | 0 | 13762.559 | 24379.391 | 24772.607 |
| t-419 | 17 | 1 | 38.104 | 11.284 | 3214 | 0 | 518 | 518 | 0 | 13893.631 | 24379.391 | 24772.607 |
| t-013 | 10 | 1 | 68.275 | 10.359 | 134 | 0 | 423 | 423 | 0 | 14090.239 | 24248.319 | 24772.607 |
| t-492 | 6 | 1 | 48.414 | 4.411 | 3046 | 0 | 199 | 199 | 0 | 13434.879 | 24248.319 | 24772.607 |
| t-171 | 14 | 1 | 179.386 | 42.55 | 142 | 0 | 1773 | 1773 | 0 | 14221.311 | 24117.247 | 24903.679 |
| t-292 | 16 | 1 | 83.138 | 23.979 | 916 | 0 | 1018 | 1018 | 0 | 13631.487 | 24117.247 | 25034.751 |
| t-028 | 27 | 1 | 38.33 | 17.031 | 454 | 0 | 785 | 785 | 0 | 13434.879 | 23986.175 | 24772.607 |
| t-077 | 10 | 1 | 267.106 | 44.58 | 1566 | 0 | 1856 | 1856 | 0 | 14286.847 | 23986.175 | 25034.751 |
| t-236 | 12 | 1 | 39.847 | 7.706 | 131 | 0 | 325 | 325 | 0 | 13500.415 | 23986.175 | 24510.463 |
| t-251 | 9 | 1 | 70.446 | 10.583 | 1193 | 0 | 429 | 429 | 0 | 13828.095 | 23986.175 | 24641.535 |
| t-427 | 16 | 2 | 206.752 | 54.879 | 1711 | 0 | 2280 | 2280 | 0 | 14352.383 | 23986.175 | 24903.679 |
| t-060 | 7 | 1 | 88.771 | 10.959 | 1546 | 0 | 483 | 483 | 0 | 13631.487 | 23855.103 | 24117.247 |
| t-180 | 19 | 1 | 88.151 | 26.557 | 152 | 0 | 1127 | 1127 | 0 | 13959.167 | 23855.103 | 24641.535 |
| t-229 | 28 | 1 | 44.461 | 20.764 | 1675 | 0 | 966 | 966 | 0 | 13828.095 | 23855.103 | 24903.679 |
| t-267 | 17 | 1 | 121.795 | 35.028 | 1525 | 0 | 1450 | 1450 | 0 | 13893.631 | 23855.103 | 24510.463 |
| t-307 | 16 | 1 | 49.111 | 13.25 | 200 | 0 | 570 | 570 | 0 | 13762.559 | 23855.103 | 24641.535 |
| t-357 | 21 | 1 | 100.02 | 34.702 | 2215 | 0 | 1429 | 1429 | 0 | 13959.167 | 23855.103 | 25034.751 |
| t-403 | 41 | 1 | 35.668 | 24.183 | 304 | 0 | 1069 | 1069 | 0 | 13959.167 | 23855.103 | 25034.751 |
| t-443 | 49 | 2 | 106.374 | 88.376 | 261 | 0 | 3750 | 3750 | 0 | 13959.167 | 23855.103 | 25427.967 |
| t-004 | 38 | 3 | 162.141 | 106.177 | 2178 | 0 | 4331 | 4331 | 0 | 13893.631 | 23724.031 | 24903.679 |
| t-108 | 32 | 2 | 108.406 | 59.137 | 694 | 0 | 2447 | 2447 | 0 | 13697.023 | 23724.031 | 25034.751 |
