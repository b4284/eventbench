# C1-dc-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.535s, closing 2.424s, drain 14.064s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 469010 | 469010 | 6013.043 | 3940.796 | 20339.3 | 0 | 1727 | 1727 | 276 | 2370 | 97171 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 300080 | 25751.464 | 33505.279 | 47710.207 | 54099.967 | 56426.495 | 58949.631 |
| e2e_from_send | 300080 | 24215.486 | 32079.871 | 47415.295 | 53641.215 | 55836.671 | 58687.487 |
| producer_lag | 300572 | 1533.739 | 113.919 | 885.759 | 26574.847 | 27262.975 | 28229.631 |
| publish_call | 469010 | 221.785 | 96.191 | 207.999 | 493.823 | 27983.871 | 28131.327 |
| queue_wait | 300080 | 24114.256 | 31997.951 | 47349.759 | 53608.447 | 55803.903 | 58032.127 |
| broker_dwell | 300080 | 23873.418 | 31637.503 | 47251.455 | 53542.911 | 55705.599 | 57966.591 |
| receive_call | 417973 | 29.894 | 7.363 | 77.823 | 347.391 | 481.023 | 590.847 |
| ctx_load | 300080 | 10.087 | 6.511 | 21.279 | 60.703 | 97.919 | 169.983 |
| process | 300080 | 10.826 | 0.002 | 0.004 | 0.053 | 2635.775 | 3000.319 |
| ctx_commit | 300080 | 80.277 | 46.751 | 200.191 | 333.567 | 467.711 | 560.127 |
| ack | 300067 | 0.008 | 0.004 | 0.007 | 0.044 | 0.616 | 50.879 |
| service | 300080 | 101.258 | 57.311 | 218.751 | 385.791 | 2717.695 | 3327.999 |
| e2e_large | 175 | 6438.795 | 3422.207 | 29278.207 | 31916.031 | 35356.671 | 35356.671 |
| ready_wait | 300080 | 1570.874 | 46.143 | 1976.319 | 36601.855 | 41025.535 | 48955.391 |
| blocked | 300080 | 1057.344 | 27.631 | 821.759 | 28835.839 | 33849.343 | 35749.887 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3940.796 | 11035.8 |
| applied_total | 225870.51 | 270597 |
| bench_cpu_cores | 2.006 | 4.981 |
| bench_cpu_pct | 8.358 | 20.75 |
| bench_gc_gen0 | 1.102 | 3 |
| bench_gc_gen1 | 0.755 | 3 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 1116.802 | 1361.1 |
| bench_mem_mb | 1810.91 | 1908.9 |
| bench_threadpool_queue | 38.857 | 440 |
| bench_threadpool_threads | 43.163 | 44 |
| broker_backlog | 132866.49 | 158183 |
| ctr_eb_dc1a_cpu_pct | 31.594 | 221.9 |
| ctr_eb_dc1a_mem_mb | 4516.478 | 5455 |
| ctr_eb_dc1b_cpu_pct | 24.571 | 59.31 |
| ctr_eb_dc1b_mem_mb | 2554.633 | 2624.5 |
| ctr_eb_dc2a_cpu_pct | 48.134 | 56.33 |
| ctr_eb_dc2a_mem_mb | 3169.468 | 3310.2 |
| ctr_eb_dc2b_cpu_pct | 697.711 | 785.17 |
| ctr_eb_dc2b_mem_mb | 5237.077 | 5519.9 |
| ctr_eb_etcd_dc1_cpu_pct | 1.439 | 1.82 |
| ctr_eb_etcd_dc1_mem_mb | 20.355 | 24.7 |
| ctr_eb_etcd_dc2_cpu_pct | 2.827 | 3.19 |
| ctr_eb_etcd_dc2_mem_mb | 34.632 | 35.8 |
| ctr_eb_etcd_w_cpu_pct | 1.717 | 2.17 |
| ctr_eb_etcd_w_mem_mb | 53.59 | 54.9 |
| ctr_eb_haproxy_cpu_pct | 48.512 | 97.84 |
| ctr_eb_haproxy_mem_mb | 31.308 | 33.3 |
| ctr_eb_mosquitto_cpu_pct | 26.73 | 60.16 |
| ctr_eb_mosquitto_mem_mb | 4.982 | 14.6 |
| ctr_eb_mssql_cpu_pct | 2.925 | 12.55 |
| ctr_eb_mssql_mem_mb | 6084.141 | 6169.3 |
| ctr_eb_mysql_cpu_pct | 0.578 | 0.99 |
| ctr_eb_mysql_mem_mb | 7502.384 | 7597.9 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 4.714 | 4.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.08 |
| ctr_eb_pgbouncer_3_mem_mb | 4.022 | 4.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 4.6 | 4.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 4.222 | 4.3 |
| ctr_eb_postgres_cpu_pct | 1.095 | 3.07 |
| ctr_eb_postgres_mem_mb | 1785.351 | 1798.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 35.241 | 1343 |
| host_cpu_pct | 33.342 | 76.95 |
| in_flight | 136559.143 | 161776 |
| nacks_per_s | 35.241 | 1343 |
| octopus_dispatcher_cores | 0.671 | 1.39 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 604580 | 775031 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 159056.061 | 203712 |
| octopus_mqtt_sent_total | 143163.49 | 189916 |
| octopus_publish_retries_total | 6728.898 | 24781 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.224 | 18 |
| pg_active_loads | 0.653 | 4 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 61.286 | 90 |
| pg_conn_idle | 202.592 | 260 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.796 | 7 |
| pg_conn_total | 263.878 | 265 |
| pg_db_size_mb | 1379.676 | 1583.9 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 10.24 | 15.52 |
| pg_replica_replay_lag_ms | 82.463 | 125.5 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17770.069 | 22191.5 |
| pg_tup_del_per_s | 21642.194 | 31136.9 |
| pg_tup_fetch_per_s | 241812.629 | 287277.2 |
| pg_tup_ins_per_s | 37776.878 | 53911.1 |
| pg_tup_upd_per_s | 22968.773 | 26363.8 |
| pg_wal_mb_per_s | 85.704 | 116.43 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.102 | 1 |
| produced_per_s | 6013.043 | 15899.9 |
| produced_total | 362429.653 | 432373 |
| publish_mb_per_s | 22.422 | 71.59 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 76,665 records are Completed |
| record_counts | pass | 76,665 records found, 76,665 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 276 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 469,010 events applied by workers, 469,010 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 97,171 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 28836 ms, max 35736 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8130 seq 10: ready 136 ms before start, 136 ms of it with an idle slot in group 275
- [cross_source_blocking] source 7318 seq 9: ready 136 ms before start, 136 ms of it with an idle slot in group 251
- [cross_source_blocking] source 7401 seq 4: ready 236 ms before start, 236 ms of it with an idle slot in group 255
- [cross_source_blocking] source 11209 seq 9: ready 235 ms before start, 235 ms of it with an idle slot in group 365
- [cross_source_blocking] source 13014 seq 14: ready 135 ms before start, 135 ms of it with an idle slot in group 414
- [cross_source_blocking] source 7394 seq 5: ready 136 ms before start, 136 ms of it with an idle slot in group 255
- [cross_source_blocking] source 7273 seq 7: ready 235 ms before start, 235 ms of it with an idle slot in group 249
- [cross_source_blocking] source 9206 seq 3: ready 137 ms before start, 137 ms of it with an idle slot in group 316
- [cross_source_blocking] source 5058 seq 7: ready 119 ms before start, 119 ms of it with an idle slot in group 176
- [cross_source_blocking] source 14894 seq 6: ready 137 ms before start, 137 ms of it with an idle slot in group 467
- [cross_source_blocking] source 2357 seq 7: ready 113 ms before start, 113 ms of it with an idle slot in group 68
- [cross_source_blocking] source 4521 seq 6: ready 125 ms before start, 125 ms of it with an idle slot in group 157
- [cross_source_blocking] source 5346 seq 3: ready 137 ms before start, 137 ms of it with an idle slot in group 183
- [cross_source_blocking] source 1993 seq 6: ready 235 ms before start, 219 ms of it with an idle slot in group 58
- [cross_source_blocking] source 10921 seq 13: ready 124 ms before start, 124 ms of it with an idle slot in group 355
- [cross_source_blocking] source 3826 seq 5: ready 119 ms before start, 119 ms of it with an idle slot in group 130
- [cross_source_blocking] source 3401 seq 5: ready 161 ms before start, 118 ms of it with an idle slot in group 117
- [cross_source_blocking] source 5009 seq 9: ready 114 ms before start, 114 ms of it with an idle slot in group 173
- [cross_source_blocking] source 12977 seq 8: ready 132 ms before start, 132 ms of it with an idle slot in group 411
- [cross_source_blocking] source 12661 seq 10: ready 132 ms before start, 132 ms of it with an idle slot in group 398

## Error samples

- t-199#0 src=5770 seq=5 op=Complete rec=24781961297921 ev=3: NpgsqlException: Exception while reading from stream
- t-267#0 src=7930 seq=39 op=Update rec=34059090657281 ev=12: NpgsqlException: Exception while reading from stream
- t-367#0 src=11279 seq=35 op=Get rec=48442936131588 ev=1: NpgsqlException: Exception while reading from stream
- t-041#0 src=1308 seq=56 op=Complete rec=5617817223172 ev=12: NpgsqlException: Exception while reading from stream
- t-249#0 src=7272 seq=16 op=Update rec=31233002176514 ev=5: NpgsqlException: Exception while reading from stream
- t-471#0 src=15096 seq=5 op=Complete rec=64836826300417 ev=4: NpgsqlException: Exception while reading from stream
- t-421#0 src=13210 seq=29 op=Get rec=56736517980164 ev=3: NpgsqlException: Exception while reading from stream
- t-344#2 src=10193 seq=18 op=Complete rec=43778601648134 ev=2: NpgsqlException: Exception while reading from stream
- t-293#0 src=8545 seq=14 op=Update rec=36700495544324 ev=3: NpgsqlException: Exception while reading from stream
- t-344#4 src=10173 seq=17 op=Update rec=43692702302209 ev=3: NpgsqlException: Exception while reading from stream
- t-301#0 src=8681 seq=30 op=Get rec=37284611096579 ev=3: NpgsqlException: Exception while reading from stream
- t-316#0 src=9204 seq=13 op=Update rec=39530878992387 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: Exception while reading from stream
- t-050#0 src=1738 seq=5 op=Update rec=7464653160449 ev=4: NpgsqlException: Exception while reading from stream
- t-132#0 src=3976 seq=52 op=Update rec=17076789968907 ev=1: NpgsqlException: Exception while reading from stream
- t-329#0 src=9512 seq=45 op=Get rec=40853728919559 ev=4: NpgsqlException: Exception while reading from stream
- t-425#2 src=13270 seq=17 op=Create rec=56994216017924 ev=0: NpgsqlException: Exception while reading from stream
- t-001#1 src=126 seq=13 op=Update rec=541165879298 ev=1: NpgsqlException: Exception while reading from stream
- t-190#0 src=5555 seq=45 op=Update rec=23858543329284 ev=18: NpgsqlException: Exception while reading from stream
- t-112#0 src=3321 seq=19 op=Update rec=14263586390019 ev=7: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1657 | 1657 | 0 | 45350.911 | 58195.967 | 58982.399 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3481 | 3481 | 0 | 44564.479 | 57409.535 | 58982.399 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1742 | 1742 | 0 | 44302.335 | 57409.535 | 58195.967 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3435 | 3435 | 0 | 44564.479 | 56885.247 | 57933.823 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1682 | 1682 | 0 | 43778.047 | 56623.103 | 57671.679 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3656 | 3656 | 0 | 43515.903 | 56623.103 | 57409.535 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1708 | 1708 | 0 | 44826.623 | 56360.959 | 57933.823 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1803 | 1803 | 0 | 44564.479 | 56360.959 | 56885.247 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5017 | 5017 | 0 | 43778.047 | 56360.959 | 57147.391 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1679 | 1679 | 0 | 42729.471 | 56098.815 | 56623.103 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3655 | 3655 | 0 | 43515.903 | 56098.815 | 56885.247 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1668 | 1668 | 0 | 43515.903 | 55836.671 | 56623.103 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1684 | 1684 | 0 | 43778.047 | 55836.671 | 56623.103 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6977 | 6977 | 0 | 43515.903 | 55836.671 | 57409.535 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6810 | 6810 | 0 | 43778.047 | 55574.527 | 57409.535 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4732 | 4732 | 0 | 42729.471 | 55574.527 | 56623.103 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3166 | 3166 | 0 | 43253.759 | 55574.527 | 56885.247 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1673 | 1673 | 0 | 43253.759 | 55312.383 | 56623.103 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1564 | 1564 | 0 | 42729.471 | 55050.239 | 56623.103 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5142 | 5142 | 0 | 42729.471 | 55050.239 | 56098.815 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 4610 | 4610 | 0 | 42205.183 | 55050.239 | 55836.671 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1619 | 1619 | 0 | 42991.615 | 54525.951 | 55050.239 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5042 | 5042 | 0 | 42729.471 | 54525.951 | 55836.671 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1576 | 1576 | 0 | 42991.615 | 54525.951 | 55312.383 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3415 | 3415 | 0 | 42205.183 | 54525.951 | 55574.527 |
