# V1-node-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.222s, closing 2.424s, drain 23.099s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 465825 | 465825 | 5977.743 | 3175.247 | 19762.7 | 0 | 2513 | 2513 | 272 | 2362 | 58176 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 297374 | 36733.933 | 40599.551 | 52920.319 | 59670.527 | 62226.431 | 65273.855 |
| e2e_from_send | 297374 | 26603.742 | 31588.351 | 49774.591 | 58359.807 | 61145.087 | 64782.335 |
| producer_lag | 297513 | 10127.134 | 4763.647 | 26132.479 | 27967.487 | 28655.615 | 29097.983 |
| publish_call | 465825 | 240.259 | 131.711 | 312.831 | 692.223 | 20463.615 | 20856.831 |
| queue_wait | 297374 | 26463.34 | 31358.975 | 49741.823 | 58327.039 | 61112.319 | 64782.335 |
| broker_dwell | 297374 | 26177.477 | 30605.311 | 49545.215 | 58195.967 | 60981.247 | 64651.263 |
| receive_call | 420929 | 24.588 | 7.071 | 52.319 | 345.087 | 477.439 | 545.791 |
| ctx_load | 297374 | 6.849 | 4.223 | 14.695 | 43.007 | 72.447 | 158.207 |
| process | 297374 | 10.888 | 0.002 | 0.005 | 0.064 | 2635.775 | 3002.367 |
| ctx_commit | 297374 | 122.613 | 42.975 | 305.663 | 1192.959 | 1648.639 | 1779.711 |
| ack | 296866 | 0.006 | 0.003 | 0.005 | 0.032 | 0.426 | 28.159 |
| service | 297374 | 140.396 | 50.559 | 320.511 | 1439.743 | 2789.375 | 4653.055 |
| e2e_large | 172 | 12850.426 | 7348.223 | 28540.927 | 31342.591 | 31916.031 | 31916.031 |
| ready_wait | 297374 | 1102.31 | 26.287 | 889.343 | 37322.751 | 44367.871 | 58982.399 |
| blocked | 297374 | 684.411 | 18.175 | 224.895 | 24690.687 | 30588.927 | 32817.151 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3175.247 | 9775.9 |
| applied_total | 150034.878 | 219637 |
| bench_cpu_cores | 1.466 | 3.355 |
| bench_cpu_pct | 6.111 | 13.98 |
| bench_gc_gen0 | 1.327 | 4 |
| bench_gc_gen1 | 1.102 | 4 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 913.461 | 1212 |
| bench_mem_mb | 1445.733 | 1564.7 |
| bench_threadpool_queue | 26.245 | 350 |
| bench_threadpool_threads | 43.429 | 44 |
| broker_backlog | 150348.755 | 203790 |
| ctr_eb_dc1a_cpu_pct | 70.759 | 395.1 |
| ctr_eb_dc1a_mem_mb | 3120.176 | 3227.2 |
| ctr_eb_dc1b_cpu_pct | 68.214 | 380.93 |
| ctr_eb_dc1b_mem_mb | 3103.192 | 3193.9 |
| ctr_eb_dc2a_cpu_pct | 173.392 | 884.4 |
| ctr_eb_dc2a_mem_mb | 3379.2 | 4780.8 |
| ctr_eb_dc2b_cpu_pct | 652.55 | 772.36 |
| ctr_eb_dc2b_mem_mb | 11441.482 | 11682.5 |
| ctr_eb_etcd_dc1_cpu_pct | 1.331 | 2.04 |
| ctr_eb_etcd_dc1_mem_mb | 26.41 | 27.4 |
| ctr_eb_etcd_dc2_cpu_pct | 1.428 | 2.53 |
| ctr_eb_etcd_dc2_mem_mb | 26.531 | 28.1 |
| ctr_eb_etcd_w_cpu_pct | 1.45 | 2.1 |
| ctr_eb_etcd_w_mem_mb | 26.992 | 28.3 |
| ctr_eb_mosquitto_cpu_pct | 22.677 | 47.41 |
| ctr_eb_mosquitto_mem_mb | 6.457 | 15.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.516 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.026 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.026 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.774 | 1.84 |
| ctr_eb_postgres_mem_mb | 2150.859 | 2152.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 51.233 | 1901.8 |
| host_cpu_pct | 42.074 | 91.25 |
| in_flight | 158554.571 | 208982 |
| nacks_per_s | 51.233 | 1901.8 |
| octopus_dispatcher_cores | 0.938 | 1.54 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 484358.98 | 695850 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 156671.878 | 223625 |
| octopus_mqtt_sent_total | 95692.653 | 145429 |
| octopus_publish_retries_total | 11889.51 | 26068 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.449 | 19 |
| pg_active_loads | 0.306 | 5 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 43.939 | 55 |
| pg_conn_idle | 221.061 | 224 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.327 | 4 |
| pg_conn_total | 265 | 265 |
| pg_db_size_mb | 1307.945 | 1408.9 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 8.273 | 39.58 |
| pg_replica_replay_lag_ms | 55.682 | 158.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14971.798 | 16607.1 |
| pg_tup_del_per_s | 21479.669 | 30849.2 |
| pg_tup_fetch_per_s | 480704.314 | 530563.8 |
| pg_tup_ins_per_s | 45885.951 | 59499.4 |
| pg_tup_upd_per_s | 16052.804 | 17827.2 |
| pg_wal_mb_per_s | 89.589 | 106 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 5977.743 | 15990.9 |
| produced_total | 308589.449 | 428619 |
| publish_mb_per_s | 22.165 | 65.47 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 76,228 records are Completed |
| record_counts | pass | 76,228 records found, 76,228 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 272 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 465,825 events applied by workers, 465,825 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 58,176 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 24691 ms, max 32809 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13212 seq 13: ready 102 ms before start, 102 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9468 seq 12: ready 104 ms before start, 104 ms of it with an idle slot in group 327
- [cross_source_blocking] source 7260 seq 6: ready 160 ms before start, 160 ms of it with an idle slot in group 248
- [cross_source_blocking] source 6812 seq 11: ready 162 ms before start, 162 ms of it with an idle slot in group 232
- [cross_source_blocking] source 4476 seq 7: ready 122 ms before start, 122 ms of it with an idle slot in group 154
- [cross_source_blocking] source 7100 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 241
- [cross_source_blocking] source 11612 seq 4: ready 122 ms before start, 122 ms of it with an idle slot in group 377
- [cross_source_blocking] source 13340 seq 5: ready 124 ms before start, 124 ms of it with an idle slot in group 426
- [cross_source_blocking] source 6658 seq 6: ready 190 ms before start, 190 ms of it with an idle slot in group 225
- [cross_source_blocking] source 7158 seq 10: ready 881 ms before start, 111 ms of it with an idle slot in group 243
- [cross_source_blocking] source 15734 seq 4: ready 166 ms before start, 166 ms of it with an idle slot in group 491
- [cross_source_blocking] source 15391 seq 12: ready 486 ms before start, 119 ms of it with an idle slot in group 482
- [cross_source_blocking] source 4374 seq 8: ready 135 ms before start, 135 ms of it with an idle slot in group 149
- [cross_source_blocking] source 5846 seq 8: ready 134 ms before start, 134 ms of it with an idle slot in group 201
- [cross_source_blocking] source 10715 seq 6: ready 304 ms before start, 304 ms of it with an idle slot in group 351
- [cross_source_blocking] source 11611 seq 6: ready 304 ms before start, 304 ms of it with an idle slot in group 377
- [cross_source_blocking] source 2462 seq 4: ready 167 ms before start, 167 ms of it with an idle slot in group 73
- [cross_source_blocking] source 15070 seq 5: ready 491 ms before start, 127 ms of it with an idle slot in group 470
- [cross_source_blocking] source 12963 seq 7: ready 149 ms before start, 149 ms of it with an idle slot in group 410
- [cross_source_blocking] source 7292 seq 4: ready 702 ms before start, 128 ms of it with an idle slot in group 250

## Error samples

- t-333#0 src=9575 seq=4 op=Create rec=41124311859203 ev=0: NpgsqlException: No suitable host was found.
- t-138#0 src=4189 seq=11 op=Update rec=17991618002946 ev=4: NpgsqlException: No suitable host was found.
- t-308L#0 src=8918 seq=1 op=Create rec=38302518345729 ev=0: NpgsqlException: No suitable host was found.
- t-299#0 src=8635 seq=16 op=Update rec=37087042600963 ev=2: NpgsqlException: No suitable host was found.
- t-251#0 src=7313 seq=19 op=Update rec=31409095835651 ev=3: NpgsqlException: No suitable host was found.
- t-289#0 src=8477 seq=38 op=Update rec=36408437768197 ev=5: NpgsqlException: No suitable host was found.
- t-393#0 src=12383 seq=21 op=Get rec=53184580026371 ev=1: NpgsqlException: No suitable host was found.
- t-077#0 src=2565 seq=33 op=Update rec=11016591114244 ev=3: NpgsqlException: No suitable host was found.
- t-403#0 src=12779 seq=12 op=Update rec=54885387075587 ev=2: NpgsqlException: No suitable host was found.
- t-436#0 src=13683 seq=7 op=Update rec=58768037511170 ev=1: NpgsqlException: No suitable host was found.
- t-438#0 src=13820 seq=12 op=Get rec=59356448030722 ev=2: NpgsqlException: No suitable host was found.
- t-416#0 src=13054 seq=6 op=Update rec=56066503081985 ev=3: NpgsqlException: No suitable host was found.
- t-295#0 src=8581 seq=15 op=Get rec=36855114366977 ev=7: NpgsqlException: No suitable host was found.
- t-215#0 src=6321 seq=11 op=Update rec=27148488278018 ev=3: NpgsqlException: No suitable host was found.
- t-147#3 src=4300 seq=19 op=Update rec=18468359372804 ev=4: NpgsqlException: No suitable host was found.
- t-316#0 src=9215 seq=17 op=Update rec=39578123632642 ev=2: NpgsqlException: No suitable host was found.
- t-469#0 src=15064 seq=10 op=Create rec=64699387346946 ev=0: NpgsqlException: No suitable host was found.
- t-469#2 src=15013 seq=11 op=Get rec=64480344014852 ev=1: NpgsqlException: No suitable host was found.
- t-014#0 src=429 seq=11 op=Get rec=1842540969986 ev=2: NpgsqlException: No suitable host was found.
- t-347#2 src=10352 seq=9 op=Create rec=44461501448195 ev=0: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1645 | 1645 | 0 | 46923.775 | 62652.415 | 63438.847 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3646 | 3646 | 0 | 46923.775 | 62652.415 | 63700.991 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3626 | 3626 | 0 | 47185.919 | 62390.271 | 63438.847 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3453 | 3453 | 0 | 47448.063 | 62390.271 | 65273.855 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1735 | 1735 | 0 | 46399.487 | 62390.271 | 65273.855 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3409 | 3409 | 0 | 47448.063 | 62390.271 | 63176.703 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1585 | 1585 | 0 | 45613.055 | 62128.127 | 62652.415 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5126 | 5126 | 0 | 46661.631 | 62128.127 | 63438.847 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3409 | 3409 | 0 | 46137.343 | 62128.127 | 63438.847 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4978 | 4978 | 0 | 46923.775 | 62128.127 | 62914.559 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1675 | 1675 | 0 | 47448.063 | 61865.983 | 62914.559 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6751 | 6751 | 0 | 46923.775 | 61865.983 | 63176.703 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1700 | 1700 | 0 | 47185.919 | 61865.983 | 63176.703 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1673 | 1673 | 0 | 46661.631 | 61603.839 | 62390.271 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5003 | 5003 | 0 | 45875.199 | 61603.839 | 62652.415 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1547 | 1547 | 0 | 46923.775 | 61603.839 | 62390.271 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3237 | 3237 | 0 | 45350.911 | 61603.839 | 63176.703 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4795 | 4795 | 0 | 45350.911 | 61603.839 | 63176.703 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1659 | 1659 | 0 | 46661.631 | 61341.695 | 62390.271 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4705 | 4705 | 0 | 45613.055 | 61341.695 | 62652.415 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1676 | 1676 | 0 | 46399.487 | 61341.695 | 62390.271 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1713 | 1713 | 0 | 46661.631 | 61341.695 | 62128.127 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1658 | 1658 | 0 | 46399.487 | 61341.695 | 62652.415 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1672 | 1672 | 0 | 46661.631 | 61079.551 | 61603.839 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3138 | 3138 | 0 | 45875.199 | 61079.551 | 62390.271 |
