# L1-db-freeze-10min — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 635.737s, closing 2.433s, drain 9.582s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 372969 | 372969 | 359.425 | 282.759 | 16402.8 | 0 | 1432 | 1432 | 231 | 1886 | 93408 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 205935 | 265286.257 | 11034.623 | 621281.279 | 625999.871 | 628097.023 | 630718.463 |
| e2e_from_send | 205935 | 260260.845 | 7155.711 | 617086.975 | 621805.567 | 623902.719 | 627048.447 |
| producer_lag | 206448 | 5024.429 | 4153.343 | 5693.439 | 7127.039 | 607649.791 | 611844.095 |
| publish_call | 372969 | 3074.514 | 135.167 | 273.919 | 690.687 | 604504.063 | 605028.351 |
| queue_wait | 205935 | 260142.143 | 6983.679 | 616562.687 | 621805.567 | 623902.719 | 627048.447 |
| broker_dwell | 205935 | 260009.062 | 6836.223 | 616562.687 | 621805.567 | 623902.719 | 626524.159 |
| receive_call | 327003 | 37.29 | 10.567 | 104.319 | 380.159 | 486.655 | 586.751 |
| ctx_load | 205935 | 15.958 | 9.127 | 37.983 | 95.231 | 143.487 | 191.359 |
| process | 205935 | 10.99 | 0.002 | 0.005 | 0.092 | 2629.631 | 3020.799 |
| ctx_commit | 205935 | 91.368 | 82.431 | 174.463 | 286.975 | 439.551 | 584.703 |
| ack | 205922 | 0.014 | 0.004 | 0.008 | 0.063 | 1.488 | 64.671 |
| service | 205935 | 118.451 | 99.775 | 204.415 | 396.031 | 2736.127 | 3373.055 |
| e2e_large | 127 | 59503.285 | 7229.439 | 10772.479 | 613941.247 | 616562.687 | 616562.687 |
| ready_wait | 205935 | 28563.97 | 89.279 | 2197.503 | 610795.519 | 614989.823 | 620756.991 |
| blocked | 205935 | 28096.818 | 59.967 | 1242.111 | 603455.487 | 606076.927 | 608174.079 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 282.759 | 13688.1 |
| applied_total | 252069.543 | 268237 |
| bench_cpu_cores | 0.354 | 4.418 |
| bench_cpu_pct | 1.477 | 18.41 |
| bench_gc_gen0 | 1.21 | 6 |
| bench_gc_gen1 | 0.402 | 3 |
| bench_gc_gen2 | 0.066 | 2 |
| bench_gc_heap_mb | 371.987 | 1594.7 |
| bench_mem_mb | 915.535 | 1970.3 |
| bench_threadpool_queue | 4.029 | 632 |
| bench_threadpool_threads | 24.788 | 44 |
| broker_backlog | 76188.627 | 77142 |
| ctr_eb_dc1a_cpu_pct | 60.354 | 91.54 |
| ctr_eb_dc1a_mem_mb | 2334.135 | 2374.6 |
| ctr_eb_dc1b_cpu_pct | 60.993 | 90.6 |
| ctr_eb_dc1b_mem_mb | 2319.95 | 2364.2 |
| ctr_eb_dc2a_cpu_pct | 767.906 | 934.09 |
| ctr_eb_dc2a_mem_mb | 4757.862 | 5373 |
| ctr_eb_dc2b_cpu_pct | 61.878 | 93.59 |
| ctr_eb_dc2b_mem_mb | 2360.715 | 2419.5 |
| ctr_eb_etcd_dc1_cpu_pct | 0.645 | 2.44 |
| ctr_eb_etcd_dc1_mem_mb | 30.766 | 33.3 |
| ctr_eb_etcd_dc2_cpu_pct | 0.63 | 2.26 |
| ctr_eb_etcd_dc2_mem_mb | 27.115 | 29.5 |
| ctr_eb_etcd_w_cpu_pct | 0.74 | 2.73 |
| ctr_eb_etcd_w_mem_mb | 41.858 | 43.4 |
| ctr_eb_haproxy_cpu_pct | 17.512 | 113.03 |
| ctr_eb_haproxy_mem_mb | 27.27 | 29.1 |
| ctr_eb_mosquitto_cpu_pct | 4.692 | 53.11 |
| ctr_eb_mosquitto_mem_mb | 3.963 | 15.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.7 | 7.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.011 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.012 | 0.06 |
| ctr_eb_pgbouncer_4_mem_mb | 7.6 | 7.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.09 |
| ctr_eb_pgbouncer_mem_mb | 7.802 | 8.1 |
| ctr_eb_postgres_cpu_pct | 0.22 | 3.16 |
| ctr_eb_postgres_mem_mb | 1681.319 | 1683.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 2.297 | 1433.2 |
| host_cpu_pct | 4.47 | 82.07 |
| in_flight | 85136.26 | 86690 |
| nacks_per_s | 2.297 | 1433.2 |
| octopus_dispatcher_cores | 0.142 | 1.37 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1053066.425 | 1527333 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 392810.067 | 712150 |
| octopus_mqtt_sent_total | 369009.732 | 652725 |
| octopus_publish_retries_total | 267272.048 | 547102 |
| octopus_publish_stuck | 1653.667 | 1804 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.207 | 17 |
| pg_active_loads | 0.125 | 14 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 28.237 | 52 |
| pg_conn_idle | 238.768 | 244 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.117 | 21 |
| pg_conn_total | 267.005 | 268 |
| pg_db_size_mb | 1381.32 | 1391.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 1.378 | 20.61 |
| pg_replica_replay_lag_ms | 12.881 | 236.3 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18264.067 | 21283.5 |
| pg_tup_del_per_s | 23526.463 | 32541.4 |
| pg_tup_fetch_per_s | 352252.443 | 353930.5 |
| pg_tup_ins_per_s | 40374.956 | 51799.5 |
| pg_tup_upd_per_s | 21740.134 | 24135.8 |
| pg_wal_mb_per_s | 76.158 | 94.4 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 359.425 | 13629.8 |
| produced_total | 337205.803 | 342322 |
| publish_mb_per_s | 1.42 | 66.81 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 64,615 records are Completed |
| record_counts | pass | 64,615 records found, 64,615 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 231 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 372,969 events applied by workers, 372,969 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 93,408 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 603455 ms, max 607900 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7948 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 268
- [cross_source_blocking] source 5676 seq 11: ready 129 ms before start, 129 ms of it with an idle slot in group 195
- [cross_source_blocking] source 15340 seq 7: ready 277 ms before start, 277 ms of it with an idle slot in group 479
- [cross_source_blocking] source 2380 seq 6: ready 122 ms before start, 122 ms of it with an idle slot in group 69
- [cross_source_blocking] source 12396 seq 6: ready 357 ms before start, 322 ms of it with an idle slot in group 393
- [cross_source_blocking] source 5868 seq 5: ready 122 ms before start, 122 ms of it with an idle slot in group 203
- [cross_source_blocking] source 9068 seq 5: ready 119 ms before start, 119 ms of it with an idle slot in group 313
- [cross_source_blocking] source 5036 seq 13: ready 361 ms before start, 361 ms of it with an idle slot in group 175
- [cross_source_blocking] source 2412 seq 10: ready 353 ms before start, 343 ms of it with an idle slot in group 70
- [cross_source_blocking] source 9228 seq 8: ready 113 ms before start, 113 ms of it with an idle slot in group 317
- [cross_source_blocking] source 10796 seq 7: ready 552 ms before start, 360 ms of it with an idle slot in group 353
- [cross_source_blocking] source 364 seq 5: ready 428 ms before start, 183 ms of it with an idle slot in group 11
- [cross_source_blocking] source 5900 seq 5: ready 151 ms before start, 151 ms of it with an idle slot in group 204
- [cross_source_blocking] source 9468 seq 12: ready 212 ms before start, 212 ms of it with an idle slot in group 327
- [cross_source_blocking] source 7916 seq 16: ready 171 ms before start, 171 ms of it with an idle slot in group 267
- [cross_source_blocking] source 11020 seq 7: ready 250 ms before start, 232 ms of it with an idle slot in group 359
- [cross_source_blocking] source 2732 seq 7: ready 212 ms before start, 212 ms of it with an idle slot in group 83
- [cross_source_blocking] source 4588 seq 11: ready 288 ms before start, 288 ms of it with an idle slot in group 160
- [cross_source_blocking] source 2476 seq 12: ready 656 ms before start, 329 ms of it with an idle slot in group 74
- [cross_source_blocking] source 6284 seq 3: ready 803 ms before start, 386 ms of it with an idle slot in group 214

## Error samples

- t-093L#0 src=2917 seq=1 op=Create rec=12528419602433 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3481 seq=5 op=Create rec=14950781157378 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3447 seq=5 op=Update rec=14804752269313 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3450 seq=2 op=Create rec=14817637171202 ev=0: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8940 seq=1 op=Create rec=38397007626241 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3467 seq=3 op=Update rec=14890651615233 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3461 seq=3 op=Update rec=14864881811458 ev=1: NpgsqlException: Exception while reading from stream
- t-319L#1 src=9272 seq=3 op=Update rec=39822936768513 ev=2: NpgsqlException: Exception while reading from stream
- t-093L#0 src=2916 seq=2 op=Update rec=12524124635137 ev=1: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8926 seq=1 op=Create rec=38336878084097 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3436 seq=3 op=Update rec=14757507629057 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3452 seq=4 op=Create rec=14826227105794 ev=0: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8927 seq=1 op=Create rec=38341173051393 ev=0: NpgsqlException: Exception while reading from stream
- t-116#0 src=3371 seq=25 op=Get rec=14478334754819 ev=1: NpgsqlException: Exception while reading from stream
- t-258#0 src=7431 seq=12 op=Update rec=31915901976578 ev=2: NpgsqlException: Exception while reading from stream
- t-393#0 src=12389 seq=14 op=Get rec=53210349830146 ev=3: NpgsqlException: Exception while reading from stream
- t-245#0 src=7203 seq=18 op=Update rec=30936649433090 ev=3: NpgsqlException: Exception while reading from stream
- t-283#0 src=8302 seq=10 op=Update rec=35656818491396 ev=1: NpgsqlException: Exception while reading from stream
- t-019#1 src=659 seq=41 op=Update rec=2830383448071 ev=2: NpgsqlException: Exception while reading from stream
- t-181#0 src=5249 seq=38 op=Update rec=22544283336711 ev=4: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1347 | 1347 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1324 | 1324 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1321 | 1321 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 6153 | 6153 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1324 | 1324 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1184 | 1184 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1203 | 1203 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1331 | 1331 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 2913 | 2913 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 5438 | 5438 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1277 | 1277 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 2798 | 2798 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4068 | 4068 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1396 | 1396 | 0 | 620756.991 | 629145.599 | 633339.903 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1400 | 1400 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 2710 | 2710 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1197 | 1197 | 0 | 616562.687 | 629145.599 | 629145.599 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 2728 | 2728 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1325 | 1325 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 2604 | 2604 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1326 | 1326 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 7557 | 7557 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 3680 | 3680 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1479 | 1479 | 0 | 620756.991 | 629145.599 | 629145.599 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 3562 | 3562 | 0 | 620756.991 | 629145.599 | 629145.599 |
