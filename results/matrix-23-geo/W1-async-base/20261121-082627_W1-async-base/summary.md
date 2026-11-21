# W1-async-base — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.508s, closing 4.368s, drain 17.302s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 737720 | 737720 | 11569.616 | 9937.161 | 16033.9 | 0 | 0 | 0 | 391 | 3810 | 365897 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 566456 | 9844.135 | 5689.343 | 23986.175 | 35520.511 | 41451.519 | 49741.823 |
| e2e_from_send | 566456 | 6642.229 | 2174.975 | 20152.319 | 32112.639 | 38469.631 | 47546.367 |
| producer_lag | 566456 | 3201.914 | 3078.143 | 4911.103 | 6455.295 | 7241.727 | 8298.495 |
| publish_call | 737720 | 148.406 | 117.695 | 283.391 | 608.255 | 887.807 | 1016.319 |
| queue_wait | 566456 | 6530.269 | 2028.543 | 20070.399 | 32047.103 | 38404.095 | 47546.367 |
| broker_dwell | 566456 | 6391.346 | 1890.303 | 19939.327 | 31916.031 | 38273.023 | 47382.527 |
| receive_call | 634209 | 35.077 | 9.615 | 100.223 | 372.223 | 485.119 | 612.351 |
| ctx_load | 566456 | 14.248 | 8.647 | 33.215 | 76.031 | 207.103 | 447.231 |
| process | 566456 | 10.821 | 0.002 | 0.005 | 0.058 | 2627.583 | 3059.711 |
| ctx_commit | 566456 | 86.757 | 62.175 | 147.199 | 604.671 | 1126.399 | 1211.391 |
| ack | 566434 | 0.01 | 0.004 | 0.007 | 0.041 | 0.791 | 56.479 |
| service | 566456 | 111.958 | 76.735 | 176.511 | 883.199 | 2768.895 | 4177.919 |
| e2e_large | 291 | 6431.86 | 6430.719 | 8445.951 | 10649.599 | 13041.663 | 13041.663 |
| ready_wait | 566456 | 643.368 | 355.071 | 1648.639 | 3772.415 | 6594.559 | 14614.527 |
| blocked | 566456 | 316.947 | 193.407 | 801.791 | 1594.367 | 2539.519 | 7135.231 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9937.161 | 13732.2 |
| applied_total | 366857.571 | 589416 |
| bench_cpu_cores | 3.244 | 4.375 |
| bench_cpu_pct | 13.516 | 18.23 |
| bench_gc_gen0 | 1.857 | 5 |
| bench_gc_gen1 | 0.653 | 3 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 1327.649 | 1631.3 |
| bench_mem_mb | 2022.278 | 2362.4 |
| bench_threadpool_queue | 80.796 | 832 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 61028.694 | 113169 |
| ctr_eb_mosquitto_cpu_pct | 43.211 | 52.64 |
| ctr_eb_mosquitto_mem_mb | 6.182 | 17.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.028 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.724 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.025 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 8.3 | 8.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.7 | 7.7 |
| ctr_eb_pgbouncer_cpu_pct | 0.03 | 0.06 |
| ctr_eb_pgbouncer_mem_mb | 8.449 | 8.7 |
| ctr_eb_postgres_cpu_pct | 1.663 | 2.9 |
| ctr_eb_postgres_mem_mb | 2192.582 | 2193.5 |
| ctr_geo_dc1a_cpu_pct | 943.729 | 1068.44 |
| ctr_geo_dc1a_mem_mb | 7849.067 | 8230.2 |
| ctr_geo_dc1b_cpu_pct | 60.438 | 71.35 |
| ctr_geo_dc1b_mem_mb | 2446.731 | 2672.2 |
| ctr_geo_etcd_dc1_cpu_pct | 2.219 | 2.5 |
| ctr_geo_etcd_dc1_mem_mb | 56.486 | 57.6 |
| ctr_geo_haproxy_cpu_pct | 142.864 | 158.38 |
| ctr_geo_haproxy_mem_mb | 32.488 | 34.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 72.543 | 79.57 |
| in_flight | 58090.082 | 110003 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.422 | 1.53 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 791841.082 | 1330045 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 173681.408 | 288397 |
| octopus_mqtt_sent_total | 224179.224 | 378135 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 12.653 | 17 |
| pg_active_loads | 6.531 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.957 | 100 |
| pg_conn_active | 47.367 | 94 |
| pg_conn_idle | 220.633 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.755 | 28 |
| pg_conn_total | 268 | 268 |
| pg_db_size_mb | 2476.996 | 3908.5 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 4.894 | 30.44 |
| pg_replica_replay_lag_ms | 58.529 | 452.1 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23593.243 | 28917 |
| pg_tup_del_per_s | 30073.818 | 40185.1 |
| pg_tup_fetch_per_s | 441641.918 | 555808.1 |
| pg_tup_ins_per_s | 42933.524 | 50319 |
| pg_tup_upd_per_s | 27498.441 | 34354.2 |
| pg_wal_mb_per_s | 92.041 | 129.55 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11569.616 | 19536.7 |
| produced_total | 424947.653 | 699419 |
| publish_mb_per_s | 40.253 | 84.88 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 108,781 records are Completed |
| record_counts | pass | 108,781 records found, 108,781 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 391 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 737,720 events applied by workers, 737,720 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 365,897 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1594 ms, max 7132 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9294 seq 15: ready 107 ms before start, 107 ms of it with an idle slot in group 320
- [cross_source_blocking] source 6710 seq 11: ready 100 ms before start, 100 ms of it with an idle slot in group 229
- [cross_source_blocking] source 12886 seq 7: ready 239 ms before start, 239 ms of it with an idle slot in group 408
- [cross_source_blocking] source 4022 seq 6: ready 244 ms before start, 244 ms of it with an idle slot in group 134
- [cross_source_blocking] source 15662 seq 6: ready 205 ms before start, 101 ms of it with an idle slot in group 488
- [cross_source_blocking] source 4598 seq 11: ready 273 ms before start, 156 ms of it with an idle slot in group 160
- [cross_source_blocking] source 398 seq 8: ready 243 ms before start, 110 ms of it with an idle slot in group 11
- [cross_source_blocking] source 4566 seq 7: ready 313 ms before start, 197 ms of it with an idle slot in group 160
- [cross_source_blocking] source 10038 seq 15: ready 313 ms before start, 112 ms of it with an idle slot in group 342
- [cross_source_blocking] source 12830 seq 3: ready 281 ms before start, 202 ms of it with an idle slot in group 406
- [cross_source_blocking] source 10878 seq 7: ready 278 ms before start, 274 ms of it with an idle slot in group 354
- [cross_source_blocking] source 12894 seq 7: ready 282 ms before start, 282 ms of it with an idle slot in group 408
- [cross_source_blocking] source 9230 seq 9: ready 276 ms before start, 276 ms of it with an idle slot in group 317
- [cross_source_blocking] source 11158 seq 8: ready 255 ms before start, 255 ms of it with an idle slot in group 364
- [cross_source_blocking] source 9246 seq 4: ready 279 ms before start, 279 ms of it with an idle slot in group 317
- [cross_source_blocking] source 3286 seq 11: ready 349 ms before start, 349 ms of it with an idle slot in group 109
- [cross_source_blocking] source 12958 seq 9: ready 281 ms before start, 215 ms of it with an idle slot in group 409
- [cross_source_blocking] source 15070 seq 5: ready 282 ms before start, 282 ms of it with an idle slot in group 470
- [cross_source_blocking] source 4886 seq 5: ready 449 ms before start, 223 ms of it with an idle slot in group 169
- [cross_source_blocking] source 9198 seq 6: ready 362 ms before start, 362 ms of it with an idle slot in group 316

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2614 | 2614 | 0 | 21889.023 | 44040.191 | 49283.071 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2598 | 2598 | 0 | 23724.031 | 43253.759 | 45088.767 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2452 | 2452 | 0 | 18350.079 | 42467.327 | 45350.911 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5365 | 5365 | 0 | 22020.095 | 42205.183 | 45088.767 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8268 | 8268 | 0 | 21757.951 | 42205.183 | 46923.775 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2688 | 2688 | 0 | 22413.311 | 41418.751 | 46661.631 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5759 | 5759 | 0 | 22020.095 | 41418.751 | 45613.055 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5745 | 5745 | 0 | 21233.663 | 41156.607 | 46923.775 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5350 | 5350 | 0 | 19267.583 | 40894.463 | 46137.343 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2692 | 2692 | 0 | 18481.151 | 40894.463 | 49807.359 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2521 | 2521 | 0 | 15073.279 | 40632.319 | 47972.351 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2573 | 2573 | 0 | 19398.655 | 40370.175 | 44302.335 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5451 | 5451 | 0 | 22282.239 | 40370.175 | 41680.895 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 7825 | 7825 | 0 | 19267.583 | 40370.175 | 46661.631 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5157 | 5157 | 0 | 16908.287 | 40108.031 | 44302.335 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 11068 | 11068 | 0 | 21233.663 | 39583.743 | 44040.191 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2626 | 2626 | 0 | 19398.655 | 38797.311 | 48496.639 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 2351 | 2351 | 0 | 15466.495 | 38273.023 | 39845.887 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 11017 | 11017 | 0 | 21889.023 | 38010.879 | 41418.751 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2751 | 2751 | 0 | 19791.871 | 38010.879 | 40894.463 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 7603 | 7603 | 0 | 17825.791 | 38010.879 | 42729.471 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2713 | 2713 | 0 | 18087.935 | 38010.879 | 39845.887 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 12477 | 12477 | 0 | 18481.151 | 37748.735 | 40370.175 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2310 | 2310 | 0 | 18743.295 | 37224.447 | 39059.455 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 7993 | 7993 | 0 | 16711.679 | 37224.447 | 43778.047 |
