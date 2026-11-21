# E32-332-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 926 workers, offered 31870.178 ev/s
- Phases: run 86.95s, closing 1.659s, drain 9.804s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 417488 | 417488 | 6755.818 | 6720.535 | 9293.5 | 0 | 0 | 0 | 71 | 1999 | 14220 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 280307 | 19325.611 | 19251.199 | 25837.567 | 28262.399 | 29130.751 | 31735.807 |
| e2e_from_send | 280307 | 490.417 | 415.487 | 840.191 | 2127.871 | 3444.735 | 5431.295 |
| producer_lag | 280307 | 18835.196 | 18808.831 | 25329.663 | 27787.263 | 28655.615 | 28917.759 |
| publish_call | 417488 | 299.325 | 238.975 | 600.575 | 1243.135 | 1741.823 | 1867.775 |
| queue_wait | 280307 | 399.899 | 334.847 | 728.063 | 1741.823 | 3155.967 | 5144.575 |
| broker_dwell | 280307 | 135.205 | 41.375 | 325.119 | 1406.975 | 2951.167 | 5087.231 |
| receive_call | 381873 | 45.162 | 20.831 | 116.351 | 326.143 | 465.663 | 508.671 |
| ctx_load | 280307 | 18.148 | 12.831 | 35.391 | 134.655 | 166.143 | 692.735 |
| process | 280307 | 10.103 | 0.001 | 0.002 | 0.006 | 2615.295 | 3028.991 |
| ctx_commit | 280307 | 62.245 | 46.079 | 117.119 | 350.719 | 566.271 | 1000.959 |
| ack | 222593 | 0.002 | 0.001 | 0.003 | 0.01 | 0.031 | 33.567 |
| service | 280307 | 90.519 | 64.287 | 146.431 | 438.015 | 2703.359 | 3538.943 |
| e2e_large | 47 | 21072.874 | 21512.191 | 27459.583 | 30965.759 | 30965.759 | 30965.759 |
| ready_wait | 280307 | 36.094 | 11.719 | 97.407 | 326.911 | 656.895 | 1743.871 |
| blocked | 280307 | 27.183 | 11.423 | 63.327 | 240.511 | 617.471 | 1742.847 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6720.535 | 9293.5 |
| applied_total | 223081.469 | 393676 |
| bench_cpu_cores | 1.62 | 3.317 |
| bench_cpu_pct | 16.199 | 33.17 |
| bench_gc_gen0 | 0.347 | 1 |
| bench_gc_gen1 | 0.245 | 1 |
| bench_gc_gen2 | 0.224 | 1 |
| bench_gc_heap_mb | 1159.892 | 2282.4 |
| bench_mem_mb | 2055.284 | 2331.4 |
| bench_threadpool_queue | 3.204 | 143 |
| bench_threadpool_threads | 19.959 | 24 |
| broker_backlog | 34682.531 | 80677 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1402.265 | 4660 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 455881.469 | 817695 |
| octopus_missing_reports_total | 22129.633 | 40361 |
| octopus_mqtt_received_total | 114864.408 | 190877 |
| octopus_mqtt_sent_total | 172450.837 | 304086 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 20.449 | 33 |
| pg_active_loads | 9.041 | 18 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 71.714 | 183 |
| pg_conn_idle | 193.429 | 243 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.449 | 45 |
| pg_conn_total | 265.143 | 266 |
| pg_db_size_mb | 3586.1 | 4149.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24865.414 | 31971.7 |
| pg_tup_del_per_s | 31334.043 | 42564.1 |
| pg_tup_fetch_per_s | 292304.529 | 408316 |
| pg_tup_ins_per_s | 42200.827 | 50436.2 |
| pg_tup_upd_per_s | 52722.437 | 67718 |
| pg_wal_mb_per_s | 111.249 | 172.68 |
| produced_per_s | 6755.818 | 10167.3 |
| produced_total | 224483.735 | 396298 |
| publish_mb_per_s | 16.779 | 28.76 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 182,600 records are Completed |
| record_counts | pass | 182,600 records found, 57,819 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 327 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 417,488 events applied by workers, 417,488 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 14,220 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 241 ms, max 1742 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12484 seq 5: ready 162 ms before start, 145 ms of it with an idle slot in group 397
- [cross_source_blocking] source 15763 seq 40: ready 533 ms before start, 533 ms of it with an idle slot in group 493
- [cross_source_blocking] source 9528 seq 14: ready 534 ms before start, 534 ms of it with an idle slot in group 331
- [cross_source_blocking] source 4335 seq 72: ready 138 ms before start, 138 ms of it with an idle slot in group 147
- [cross_source_blocking] source 2720 seq 20: ready 229 ms before start, 229 ms of it with an idle slot in group 83
- [cross_source_blocking] source 6392 seq 12: ready 203 ms before start, 203 ms of it with an idle slot in group 219
- [cross_source_blocking] source 15059 seq 34: ready 229 ms before start, 229 ms of it with an idle slot in group 469
- [cross_source_blocking] source 9118 seq 20: ready 229 ms before start, 198 ms of it with an idle slot in group 315
- [cross_source_blocking] source 12134 seq 14: ready 230 ms before start, 230 ms of it with an idle slot in group 388
- [cross_source_blocking] source 15996 seq 28: ready 203 ms before start, 203 ms of it with an idle slot in group 499
- [cross_source_blocking] source 7566 seq 28: ready 228 ms before start, 228 ms of it with an idle slot in group 260
- [cross_source_blocking] source 6454 seq 10: ready 229 ms before start, 202 ms of it with an idle slot in group 221
- [cross_source_blocking] source 5529 seq 32: ready 109 ms before start, 109 ms of it with an idle slot in group 189
- [cross_source_blocking] source 1424 seq 5: ready 229 ms before start, 229 ms of it with an idle slot in group 44
- [cross_source_blocking] source 8166 seq 16: ready 204 ms before start, 204 ms of it with an idle slot in group 276
- [cross_source_blocking] source 5479 seq 24: ready 229 ms before start, 229 ms of it with an idle slot in group 188
- [cross_source_blocking] source 14496 seq 21: ready 554 ms before start, 515 ms of it with an idle slot in group 461
- [cross_source_blocking] source 13196 seq 16: ready 203 ms before start, 203 ms of it with an idle slot in group 420
- [cross_source_blocking] source 12814 seq 12: ready 203 ms before start, 203 ms of it with an idle slot in group 405
- [cross_source_blocking] source 15737 seq 14: ready 204 ms before start, 204 ms of it with an idle slot in group 491

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 70 | 70 | 0 | 21757.951 | 31064.063 | 31064.063 |
| t-291 | 7 | 1 | 103.081 | 11.729 | 368 | 0 | 416 | 416 | 0 | 19660.799 | 28966.911 | 30539.775 |
| t-453 | 6 | 1 | 111.459 | 10.979 | 874 | 0 | 392 | 392 | 0 | 19136.511 | 28966.911 | 29229.055 |
| t-059 | 8 | 1 | 158.253 | 20.923 | 1449 | 0 | 721 | 721 | 0 | 18612.223 | 28835.839 | 28966.911 |
| t-099 | 13 | 1 | 122.695 | 28.258 | 419 | 0 | 1049 | 1049 | 0 | 19922.943 | 28835.839 | 29360.127 |
| t-107 | 19 | 1 | 102.269 | 31.759 | 453 | 0 | 1045 | 1045 | 0 | 19005.439 | 28835.839 | 29360.127 |
| t-115 | 10 | 1 | 82.634 | 13.216 | 3658 | 0 | 506 | 506 | 0 | 20185.087 | 28835.839 | 28966.911 |
| t-253 | 26 | 2 | 182.599 | 80.612 | 1433 | 0 | 2756 | 2756 | 0 | 19529.727 | 28835.839 | 31195.135 |
| t-396 | 17 | 1 | 102.807 | 29.653 | 210 | 0 | 1063 | 1063 | 0 | 19398.655 | 28835.839 | 29097.983 |
| t-492 | 6 | 1 | 64.553 | 5.882 | 3046 | 0 | 220 | 220 | 0 | 19529.727 | 28835.839 | 29097.983 |
| t-012 | 6 | 1 | 153.354 | 15.891 | 556 | 0 | 532 | 532 | 0 | 18219.007 | 28704.767 | 31719.423 |
| t-013 | 10 | 1 | 91.033 | 13.812 | 134 | 0 | 491 | 491 | 0 | 18743.295 | 28704.767 | 28966.911 |
| t-035 | 22 | 1 | 116.343 | 44.424 | 264 | 0 | 1507 | 1507 | 0 | 19660.799 | 28704.767 | 29229.055 |
| t-077 | 10 | 2 | 356.142 | 59.44 | 1566 | 0 | 2029 | 2029 | 0 | 19267.583 | 28704.767 | 29229.055 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 64 | 64 | 0 | 22151.167 | 28704.767 | 28704.767 |
| t-132 | 22 | 3 | 374.037 | 127.192 | 2676 | 0 | 4404 | 4404 | 0 | 19267.583 | 28704.767 | 30932.991 |
| t-147 | 79 | 10 | 360.93 | 484.666 | 1078 | 0 | 16752 | 16752 | 0 | 19398.655 | 28704.767 | 29491.199 |
| t-172 | 20 | 2 | 182.434 | 64.11 | 2589 | 0 | 2251 | 2251 | 0 | 19398.655 | 28704.767 | 29229.055 |
| t-181 | 35 | 5 | 368.78 | 213.055 | 1367 | 0 | 7204 | 7204 | 0 | 19791.871 | 28704.767 | 30015.487 |
| t-251 | 9 | 1 | 93.928 | 14.111 | 1193 | 0 | 467 | 467 | 0 | 19529.727 | 28704.767 | 29097.983 |
| t-324 | 15 | 1 | 54.619 | 12.69 | 131 | 0 | 475 | 475 | 0 | 19136.511 | 28704.767 | 29097.983 |
| t-380 | 22 | 2 | 192.03 | 68.716 | 132 | 0 | 2346 | 2346 | 0 | 19398.655 | 28704.767 | 29491.199 |
| t-419 | 17 | 1 | 50.805 | 15.046 | 3214 | 0 | 505 | 505 | 0 | 20316.159 | 28704.767 | 28835.839 |
| t-020 | 22 | 2 | 157.791 | 55.595 | 981 | 0 | 1846 | 1846 | 0 | 19529.727 | 28573.695 | 29229.055 |
| t-029 | 28 | 4 | 355.32 | 164.765 | 1732 | 0 | 5636 | 5636 | 0 | 19136.511 | 28573.695 | 29097.983 |
