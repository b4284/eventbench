# W28-nats-3-s2 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.31.128:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.452s, closing 1.008s, drain 7.376s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 343365 | 343365 | 5619.954 | 5572.974 | 8378.9 | 0 | 0 | 0 | 303 | 1942 | 23692 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 270280 | 7062.33 | 7102.463 | 10780.671 | 13836.287 | 14868.479 | 18087.935 |
| e2e_from_send | 270280 | 589.731 | 487.935 | 1025.535 | 2629.631 | 3856.383 | 9314.303 |
| producer_lag | 270280 | 6472.608 | 6524.927 | 10223.615 | 13271.039 | 14114.815 | 14712.831 |
| publish_call | 343365 | 347.332 | 283.903 | 678.399 | 1233.919 | 1855.487 | 2289.663 |
| queue_wait | 270280 | 521.116 | 425.983 | 939.519 | 2336.767 | 3565.567 | 7729.151 |
| broker_dwell | 270280 | 159.358 | 49.599 | 378.623 | 2012.159 | 3235.839 | 7462.911 |
| receive_call | 306949 | 31.482 | 11.055 | 84.351 | 291.071 | 460.031 | 500.479 |
| ctx_load | 270280 | 14.686 | 9.623 | 26.927 | 129.855 | 156.415 | 649.727 |
| process | 270280 | 11.588 | 0.001 | 0.002 | 0.012 | 2641.919 | 3000.319 |
| ctx_commit | 270280 | 42.308 | 26.063 | 79.039 | 313.599 | 552.447 | 1104.895 |
| ack | 253303 | 0.002 | 0.001 | 0.004 | 0.012 | 0.037 | 4.095 |
| service | 270280 | 68.615 | 40.287 | 108.223 | 368.895 | 2734.079 | 4128.767 |
| e2e_large | 223 | 9812.831 | 9797.631 | 13615.103 | 16072.703 | 18087.935 | 18087.935 |
| ready_wait | 270280 | 50.363 | 18.559 | 127.231 | 398.847 | 924.159 | 1666.047 |
| blocked | 270280 | 37.949 | 16.623 | 93.311 | 269.055 | 671.743 | 1367.039 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5572.974 | 8348.4 |
| applied_total | 183957.16 | 331120 |
| bench_cpu_cores | 1.83 | 2.278 |
| bench_cpu_pct | 22.873 | 28.47 |
| bench_gc_gen0 | 0.36 | 1 |
| bench_gc_gen1 | 0.26 | 1 |
| bench_gc_gen2 | 0.26 | 1 |
| bench_gc_heap_mb | 1333.94 | 2219.5 |
| bench_mem_mb | 2139.582 | 2285.2 |
| bench_threadpool_queue | 4.18 | 96 |
| bench_threadpool_threads | 15.82 | 21 |
| broker_backlog | 27447.18 | 49902 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1344.68 | 3245 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 372073.48 | 680192 |
| octopus_missing_reports_total | 16496.14 | 28040 |
| octopus_mqtt_received_total | 102507.72 | 167139 |
| octopus_mqtt_sent_total | 160051.22 | 284884 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.96 | 36 |
| pg_active_loads | 13.82 | 52 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 53.46 | 115 |
| pg_conn_idle | 209.68 | 256 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.74 | 42 |
| pg_conn_total | 263.14 | 264 |
| pg_db_size_mb | 3759.56 | 4269.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26441.776 | 30645.6 |
| pg_tup_del_per_s | 34735.08 | 42627.4 |
| pg_tup_fetch_per_s | 302240.998 | 350098 |
| pg_tup_ins_per_s | 46364.2 | 56304.7 |
| pg_tup_upd_per_s | 56692.22 | 63139.1 |
| pg_wal_mb_per_s | 130.013 | 221.46 |
| produced_per_s | 5619.954 | 9280 |
| produced_total | 185301.84 | 333735 |
| publish_mb_per_s | 26.82 | 39.05 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 190,019 records are Completed |
| record_counts | pass | 190,019 records found, 44,471 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 374 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 343,365 events applied by workers, 343,365 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 23,692 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 269 ms, max 1366 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13756 seq 29: ready 184 ms before start, 120 ms of it with an idle slot in group 438
- [cross_source_blocking] source 750 seq 13: ready 188 ms before start, 112 ms of it with an idle slot in group 22
- [cross_source_blocking] source 11278 seq 19: ready 272 ms before start, 139 ms of it with an idle slot in group 367
- [cross_source_blocking] source 12332 seq 19: ready 391 ms before start, 375 ms of it with an idle slot in group 391
- [cross_source_blocking] source 11824 seq 21: ready 115 ms before start, 115 ms of it with an idle slot in group 383
- [cross_source_blocking] source 6314 seq 16: ready 652 ms before start, 520 ms of it with an idle slot in group 215
- [cross_source_blocking] source 5748 seq 8: ready 152 ms before start, 152 ms of it with an idle slot in group 199
- [cross_source_blocking] source 13225 seq 7: ready 173 ms before start, 173 ms of it with an idle slot in group 423
- [cross_source_blocking] source 4401 seq 15: ready 173 ms before start, 173 ms of it with an idle slot in group 151
- [cross_source_blocking] source 15802 seq 16: ready 174 ms before start, 174 ms of it with an idle slot in group 495
- [cross_source_blocking] source 7387 seq 10: ready 213 ms before start, 213 ms of it with an idle slot in group 254
- [cross_source_blocking] source 11260 seq 9: ready 213 ms before start, 213 ms of it with an idle slot in group 366
- [cross_source_blocking] source 6494 seq 32: ready 116 ms before start, 116 ms of it with an idle slot in group 222
- [cross_source_blocking] source 3777 seq 32: ready 216 ms before start, 216 ms of it with an idle slot in group 127
- [cross_source_blocking] source 11533 seq 38: ready 231 ms before start, 231 ms of it with an idle slot in group 375
- [cross_source_blocking] source 10036 seq 77: ready 218 ms before start, 218 ms of it with an idle slot in group 342
- [cross_source_blocking] source 11824 seq 22: ready 192 ms before start, 192 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11224 seq 5: ready 119 ms before start, 119 ms of it with an idle slot in group 366
- [cross_source_blocking] source 1601 seq 14: ready 185 ms before start, 185 ms of it with an idle slot in group 47
- [cross_source_blocking] source 2402 seq 22: ready 213 ms before start, 213 ms of it with an idle slot in group 70

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 364 | 364 | 0 | 9371.647 | 15663.103 | 17563.647 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 825 | 825 | 0 | 8781.823 | 15138.815 | 15663.103 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 2335 | 2335 | 0 | 6782.975 | 14680.063 | 15728.639 |
| t-335 | 6 | 1 | 62.881 | 5.915 | 450 | 0 | 391 | 391 | 0 | 7274.495 | 14614.527 | 14745.599 |
| t-398 | 15 | 1 | 84.303 | 22.108 | 482 | 0 | 1198 | 1198 | 0 | 6914.047 | 14614.527 | 15138.815 |
| t-454 | 7 | 1 | 292.242 | 33.739 | 266 | 0 | 1837 | 1837 | 0 | 6848.511 | 14614.527 | 15007.743 |
| t-303 | 32 | 1 | 86.277 | 46.445 | 659 | 0 | 2489 | 2489 | 0 | 7143.423 | 14483.455 | 15663.103 |
| t-247 | 8 | 1 | 53.926 | 7.182 | 506 | 0 | 454 | 454 | 0 | 5898.239 | 14417.919 | 14745.599 |
| t-015 | 9 | 1 | 88.66 | 13.135 | 3462 | 0 | 708 | 708 | 0 | 7569.407 | 14286.847 | 15532.031 |
| t-463 | 12 | 1 | 42.034 | 8.514 | 2195 | 0 | 498 | 498 | 0 | 6815.743 | 14286.847 | 14548.991 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5246 | 5246 | 0 | 7012.351 | 14221.311 | 16449.535 |
| t-246 | 32 | 5 | 407.356 | 215.226 | 494 | 0 | 11245 | 11245 | 0 | 7405.567 | 14221.311 | 15204.351 |
| t-326 | 10 | 2 | 352.999 | 60.479 | 1837 | 0 | 3217 | 3217 | 0 | 7143.423 | 14221.311 | 15073.279 |
| t-007 | 17 | 1 | 110.62 | 30.423 | 140 | 0 | 1593 | 1593 | 0 | 7241.727 | 14155.775 | 14811.135 |
| t-038 | 9 | 1 | 96.455 | 14.5 | 2056 | 0 | 817 | 817 | 0 | 6782.975 | 14155.775 | 14614.527 |
| t-102 | 18 | 1 | 77.832 | 23.679 | 339 | 0 | 1338 | 1338 | 0 | 6717.439 | 14155.775 | 15073.279 |
| t-150 | 15 | 1 | 67.298 | 17.194 | 990 | 0 | 911 | 911 | 0 | 6651.903 | 14155.775 | 14942.207 |
| t-271 | 59 | 6 | 267.82 | 262.668 | 334 | 0 | 13928 | 13928 | 0 | 7176.191 | 14155.775 | 16121.855 |
| t-055 | 65 | 5 | 220.155 | 227.855 | 359 | 0 | 11964 | 11964 | 0 | 7208.959 | 14090.239 | 17039.359 |
| t-087 | 6 | 1 | 75.81 | 8.181 | 1614 | 0 | 481 | 481 | 0 | 6586.367 | 14090.239 | 14483.455 |
| t-358 | 22 | 1 | 58.489 | 21.043 | 310 | 0 | 1157 | 1157 | 0 | 6815.743 | 14090.239 | 14680.063 |
| t-415 | 14 | 1 | 150.373 | 34.759 | 2972 | 0 | 1867 | 1867 | 0 | 6848.511 | 14090.239 | 14548.991 |
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 1136 | 1136 | 0 | 8060.927 | 14024.703 | 14811.135 |
| t-110 | 9 | 1 | 40.981 | 6.184 | 2746 | 0 | 381 | 381 | 0 | 6488.063 | 14024.703 | 14614.527 |
| t-118 | 15 | 1 | 52.306 | 13.232 | 138 | 0 | 724 | 724 | 0 | 6717.439 | 14024.703 | 14548.991 |
