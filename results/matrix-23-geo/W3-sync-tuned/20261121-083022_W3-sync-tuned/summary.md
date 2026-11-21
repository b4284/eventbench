# W3-sync-tuned — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.446s, closing 2.602s, drain 29.943s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 726150 | 726150 | 11160.741 | 7560.427 | 19730.9 | 0 | 0 | 0 | 381 | 3744 | 391213 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 554894 | 17179.672 | 16138.239 | 34930.687 | 43352.063 | 48136.191 | 55345.151 |
| e2e_from_send | 554894 | 14840.828 | 13213.695 | 32964.607 | 42565.631 | 47677.439 | 55083.007 |
| producer_lag | 554894 | 2338.846 | 1545.215 | 5799.935 | 8257.535 | 9961.471 | 10584.063 |
| publish_call | 726150 | 143.939 | 101.439 | 236.415 | 1068.031 | 1668.095 | 1833.983 |
| queue_wait | 554894 | 14706.066 | 13074.431 | 32866.303 | 42500.095 | 47611.903 | 54591.487 |
| broker_dwell | 554894 | 14566.212 | 12926.975 | 32735.231 | 42369.023 | 47513.599 | 54558.719 |
| receive_call | 628590 | 30.112 | 8.287 | 80.319 | 336.383 | 478.463 | 562.175 |
| ctx_load | 554894 | 12.828 | 7.747 | 30.223 | 67.967 | 102.655 | 976.895 |
| process | 554894 | 10.805 | 0.002 | 0.004 | 0.05 | 2625.535 | 3002.367 |
| ctx_commit | 554894 | 111.035 | 61.823 | 184.319 | 1220.607 | 1625.087 | 2320.383 |
| ack | 554839 | 0.009 | 0.004 | 0.007 | 0.036 | 0.561 | 49.471 |
| service | 554894 | 134.764 | 75.775 | 209.279 | 1443.839 | 2842.623 | 4886.527 |
| e2e_large | 281 | 5048.025 | 4521.983 | 8519.679 | 10911.743 | 12214.271 | 12214.271 |
| ready_wait | 554894 | 912.763 | 438.271 | 2418.687 | 5849.087 | 9543.679 | 30883.839 |
| blocked | 554894 | 368.498 | 222.335 | 900.607 | 1993.727 | 3969.023 | 15736.831 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7560.427 | 13520.7 |
| applied_total | 301728.714 | 462207 |
| bench_cpu_cores | 2.763 | 4.239 |
| bench_cpu_pct | 11.512 | 17.66 |
| bench_gc_gen0 | 1.694 | 4 |
| bench_gc_gen1 | 0.673 | 2 |
| bench_gc_gen2 | 0.49 | 2 |
| bench_gc_heap_mb | 1199.11 | 1530.2 |
| bench_mem_mb | 1948.5 | 2090.2 |
| bench_threadpool_queue | 79.878 | 1504 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 128455.082 | 215883 |
| ctr_eb_mosquitto_cpu_pct | 37.423 | 53.43 |
| ctr_eb_mosquitto_mem_mb | 6.616 | 18.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.023 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.767 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.027 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 8.316 | 8.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 7.743 | 8 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 8.4 | 8.4 |
| ctr_eb_postgres_cpu_pct | 1.439 | 2.6 |
| ctr_eb_postgres_mem_mb | 2192.561 | 2193.5 |
| ctr_geo_dc1a_cpu_pct | 743.032 | 972.16 |
| ctr_geo_dc1a_mem_mb | 7910.637 | 8860.6 |
| ctr_geo_dc1b_cpu_pct | 58.484 | 79.24 |
| ctr_geo_dc1b_mem_mb | 3441.79 | 4410.7 |
| ctr_geo_etcd_dc1_cpu_pct | 2.077 | 2.65 |
| ctr_geo_etcd_dc1_mem_mb | 57.192 | 58.1 |
| ctr_geo_haproxy_cpu_pct | 114.032 | 146.8 |
| ctr_geo_haproxy_mem_mb | 32.524 | 38.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 60.286 | 78.02 |
| in_flight | 132066.143 | 226744 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.341 | 1.63 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 731709.449 | 1155542 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 181630.939 | 293952 |
| octopus_mqtt_sent_total | 181894.469 | 284943 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.776 | 20 |
| pg_active_loads | 3.878 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.816 | 100 |
| pg_conn_active | 49.796 | 78 |
| pg_conn_idle | 220.633 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.98 | 8 |
| pg_conn_total | 270.429 | 271 |
| pg_db_size_mb | 2392.106 | 3592.3 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 13.063 | 34.76 |
| pg_replica_replay_lag_ms | 106.353 | 338.8 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18845.973 | 26779.4 |
| pg_tup_del_per_s | 25918.847 | 37803.9 |
| pg_tup_fetch_per_s | 413647.039 | 536272.7 |
| pg_tup_ins_per_s | 41131.635 | 59022 |
| pg_tup_upd_per_s | 21529.888 | 29758.3 |
| pg_wal_mb_per_s | 73.095 | 108.54 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11160.741 | 22271 |
| produced_total | 433794.857 | 688951 |
| publish_mb_per_s | 38.378 | 68.43 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 107,423 records are Completed |
| record_counts | pass | 107,423 records found, 107,423 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 381 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 726,150 events applied by workers, 726,150 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 391,213 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1994 ms, max 15731 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7394 seq 5: ready 220 ms before start, 220 ms of it with an idle slot in group 255
- [cross_source_blocking] source 12382 seq 6: ready 219 ms before start, 219 ms of it with an idle slot in group 393
- [cross_source_blocking] source 5062 seq 9: ready 198 ms before start, 198 ms of it with an idle slot in group 176
- [cross_source_blocking] source 12392 seq 6: ready 220 ms before start, 220 ms of it with an idle slot in group 393
- [cross_source_blocking] source 8130 seq 10: ready 219 ms before start, 219 ms of it with an idle slot in group 275
- [cross_source_blocking] source 12386 seq 5: ready 152 ms before start, 152 ms of it with an idle slot in group 393
- [cross_source_blocking] source 13212 seq 13: ready 221 ms before start, 221 ms of it with an idle slot in group 421
- [cross_source_blocking] source 6398 seq 7: ready 220 ms before start, 220 ms of it with an idle slot in group 219
- [cross_source_blocking] source 12039 seq 12: ready 147 ms before start, 147 ms of it with an idle slot in group 385
- [cross_source_blocking] source 1964 seq 10: ready 202 ms before start, 202 ms of it with an idle slot in group 56
- [cross_source_blocking] source 12120 seq 5: ready 202 ms before start, 202 ms of it with an idle slot in group 387
- [cross_source_blocking] source 14398 seq 5: ready 151 ms before start, 151 ms of it with an idle slot in group 459
- [cross_source_blocking] source 278 seq 12: ready 205 ms before start, 205 ms of it with an idle slot in group 7
- [cross_source_blocking] source 3050 seq 8: ready 223 ms before start, 223 ms of it with an idle slot in group 102
- [cross_source_blocking] source 8602 seq 5: ready 205 ms before start, 205 ms of it with an idle slot in group 296
- [cross_source_blocking] source 5214 seq 11: ready 151 ms before start, 151 ms of it with an idle slot in group 180
- [cross_source_blocking] source 871 seq 16: ready 207 ms before start, 207 ms of it with an idle slot in group 26
- [cross_source_blocking] source 4455 seq 6: ready 204 ms before start, 204 ms of it with an idle slot in group 153
- [cross_source_blocking] source 2880 seq 4: ready 220 ms before start, 220 ms of it with an idle slot in group 91
- [cross_source_blocking] source 7318 seq 9: ready 220 ms before start, 220 ms of it with an idle slot in group 251

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2652 | 2652 | 0 | 28180.479 | 51642.367 | 55050.239 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2482 | 2482 | 0 | 26607.615 | 50331.647 | 55574.527 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8114 | 8114 | 0 | 31064.063 | 49807.359 | 54788.095 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2591 | 2591 | 0 | 28311.551 | 48758.783 | 51380.223 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2586 | 2586 | 0 | 30539.775 | 48496.639 | 53739.519 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2569 | 2569 | 0 | 31326.207 | 48234.495 | 50069.503 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2536 | 2536 | 0 | 28704.767 | 48234.495 | 51380.223 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2408 | 2408 | 0 | 27394.047 | 47972.351 | 52166.655 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5288 | 5288 | 0 | 30801.919 | 47972.351 | 50331.647 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5264 | 5264 | 0 | 29753.343 | 47972.351 | 50855.935 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5644 | 5644 | 0 | 31457.279 | 47972.351 | 53215.231 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5651 | 5651 | 0 | 32112.639 | 47710.207 | 51642.367 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 7693 | 7693 | 0 | 28573.695 | 47448.063 | 50855.935 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2653 | 2653 | 0 | 28835.839 | 46923.775 | 49807.359 |
| t-350 | 85 | 2 | 53.604 | 73.675 | 285 | 0 | 4226 | 4226 | 0 | 23855.103 | 46923.775 | 51380.223 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 2312 | 2312 | 0 | 26083.327 | 46399.487 | 47972.351 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2573 | 2573 | 0 | 28573.695 | 46399.487 | 49283.071 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2446 | 2446 | 0 | 27394.047 | 46399.487 | 48758.783 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5062 | 5062 | 0 | 28704.767 | 46137.343 | 49545.215 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5344 | 5344 | 0 | 30932.991 | 45875.199 | 47972.351 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 7461 | 7461 | 0 | 27525.119 | 45875.199 | 50069.503 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2594 | 2594 | 0 | 28442.623 | 45875.199 | 48758.783 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 7877 | 7877 | 0 | 29229.055 | 45613.055 | 48234.495 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2364 | 2364 | 0 | 27131.903 | 45613.055 | 47448.063 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2667 | 2667 | 0 | 29360.127 | 45350.911 | 47448.063 |
