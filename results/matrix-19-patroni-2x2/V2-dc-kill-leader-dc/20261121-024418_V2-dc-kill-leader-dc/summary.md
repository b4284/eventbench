# V2-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.635s, closing 2.449s, drain 120.106s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 485891 | 485883 | 6577.041 | 3775.769 | 17974.8 | 138 | 2824 | 2824 | 280 | 2455 | 140644 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 317243 | 30875.847 | 36470.783 | 55017.471 | 67764.223 | 78381.055 | 149422.079 |
| e2e_from_send | 317243 | 23701.65 | 14614.527 | 50724.863 | 64585.727 | 71958.527 | 125173.759 |
| producer_lag | 317248 | 7174.499 | 1890.303 | 25903.103 | 27312.127 | 27852.799 | 28409.855 |
| publish_call | 485891 | 227.998 | 116.735 | 241.663 | 547.839 | 24838.143 | 24887.295 |
| queue_wait | 317243 | 23579.992 | 14499.839 | 50692.095 | 64454.655 | 71958.527 | 125108.223 |
| broker_dwell | 317243 | 23351.322 | 14082.047 | 50561.023 | 64323.583 | 71892.991 | 125108.223 |
| receive_call | 408765 | 33.697 | 7.711 | 100.735 | 366.079 | 492.799 | 565.247 |
| ctx_load | 317243 | 20.003 | 3.349 | 16.159 | 49.631 | 103.359 | 15048.703 |
| process | 317243 | 10.782 | 0.002 | 0.005 | 0.053 | 2631.679 | 3000.319 |
| ctx_commit | 317243 | 90.834 | 33.439 | 226.175 | 598.015 | 1909.759 | 15015.935 |
| ack | 316047 | 0.006 | 0.003 | 0.006 | 0.031 | 0.395 | 31.567 |
| service | 317243 | 121.668 | 39.327 | 242.815 | 948.735 | 3334.143 | 17039.359 |
| e2e_large | 180 | 10214.753 | 4694.015 | 29114.367 | 31424.511 | 32473.087 | 32473.087 |
| ready_wait | 317243 | 1698.261 | 108.863 | 2087.935 | 38469.631 | 49381.375 | 69795.839 |
| blocked | 317243 | 1281.361 | 80.191 | 1016.319 | 30883.839 | 39059.455 | 60882.943 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3775.769 | 10632.7 |
| applied_total | 201358.673 | 263622 |
| bench_cpu_cores | 1.579 | 4.412 |
| bench_cpu_pct | 6.579 | 18.38 |
| bench_gc_gen0 | 1.367 | 4 |
| bench_gc_gen1 | 1.224 | 4 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 958.786 | 1304.1 |
| bench_mem_mb | 1540.051 | 1677.5 |
| bench_threadpool_queue | 27.265 | 708 |
| bench_threadpool_threads | 42.571 | 44 |
| broker_backlog | 125175.735 | 147163 |
| ctr_eb_dc1a_cpu_pct | 107.51 | 981.1 |
| ctr_eb_dc1a_mem_mb | 3251.212 | 4449.8 |
| ctr_eb_dc1b_cpu_pct | 62.878 | 246.35 |
| ctr_eb_dc1b_mem_mb | 3149.537 | 3241.5 |
| ctr_eb_dc2a_cpu_pct | 817.896 | 898.48 |
| ctr_eb_dc2a_mem_mb | 9212.286 | 9861 |
| ctr_eb_dc2b_cpu_pct | 57.372 | 67.66 |
| ctr_eb_dc2b_mem_mb | 9885.962 | 9899.1 |
| ctr_eb_etcd_dc1_cpu_pct | 1.691 | 2.74 |
| ctr_eb_etcd_dc1_mem_mb | 27.224 | 28.7 |
| ctr_eb_etcd_dc2_cpu_pct | 1.752 | 2.14 |
| ctr_eb_etcd_dc2_mem_mb | 27.581 | 28.3 |
| ctr_eb_etcd_w_cpu_pct | 1.803 | 2.81 |
| ctr_eb_etcd_w_mem_mb | 27.594 | 29.2 |
| ctr_eb_mosquitto_cpu_pct | 23.552 | 52.6 |
| ctr_eb_mosquitto_mem_mb | 5.669 | 9.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.024 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 9.5 | 9.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.024 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.023 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 1.068 | 2.92 |
| ctr_eb_postgres_mem_mb | 2149.655 | 2150.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 57.682 | 2048.1 |
| host_cpu_pct | 40.036 | 85.08 |
| in_flight | 132892.816 | 185774 |
| nacks_per_s | 57.682 | 2048.1 |
| octopus_dispatcher_cores | 1.137 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 540666.612 | 661123 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 142557.755 | 173833 |
| octopus_mqtt_sent_total | 118751.653 | 147573 |
| octopus_publish_retries_total | 3839.51 | 15678 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.673 | 20 |
| pg_active_loads | 16.714 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 65.327 | 79 |
| pg_conn_idle | 202.265 | 232 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.061 | 1 |
| pg_conn_total | 267.592 | 268 |
| pg_db_size_mb | 1392.543 | 1562.4 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 12.515 | 20.56 |
| pg_replica_replay_lag_ms | 112.429 | 155.5 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16799.302 | 21352.7 |
| pg_tup_del_per_s | 23832.522 | 35570.8 |
| pg_tup_fetch_per_s | 528328.584 | 628867.6 |
| pg_tup_ins_per_s | 43221.131 | 63330.6 |
| pg_tup_upd_per_s | 18346.902 | 25950.9 |
| pg_wal_mb_per_s | 90.66 | 115.93 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6577.041 | 18271.2 |
| produced_total | 334251.49 | 449396 |
| publish_mb_per_s | 24.034 | 70.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15999/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 3 records not Completed |
| record_counts | **FAIL** | 78,684 records found, 78,684 expected; 1 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 280 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 485,883 events applied by workers, 485,891 generated |
| broker_drained | **FAIL** | 8 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 140,644 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 30884 ms, max 60851 ms) |

Sample mismatches:

- source 1297: record 5570572582921 left in status Open (version 19)
- source 1297: record 5570572582924 left in status Open (version 5)
- source 1297: record 5570572582925 left in status Open (version 4)
- source 1297: cursor at seq 140, expected 148 (8 events not applied)
- source 1297: 13 records (10 completed), expected 13 (13 completed)

## Cross-source blocking samples

- [cross_source_blocking] source 8476 seq 13: ready 132 ms before start, 132 ms of it with an idle slot in group 289
- [cross_source_blocking] source 13212 seq 13: ready 134 ms before start, 134 ms of it with an idle slot in group 421
- [cross_source_blocking] source 7164 seq 10: ready 136 ms before start, 136 ms of it with an idle slot in group 243
- [cross_source_blocking] source 13984 seq 10: ready 158 ms before start, 158 ms of it with an idle slot in group 444
- [cross_source_blocking] source 7260 seq 6: ready 159 ms before start, 159 ms of it with an idle slot in group 248
- [cross_source_blocking] source 1020 seq 12: ready 160 ms before start, 160 ms of it with an idle slot in group 31
- [cross_source_blocking] source 12108 seq 8: ready 158 ms before start, 158 ms of it with an idle slot in group 387
- [cross_source_blocking] source 1964 seq 10: ready 158 ms before start, 158 ms of it with an idle slot in group 56
- [cross_source_blocking] source 2880 seq 4: ready 158 ms before start, 149 ms of it with an idle slot in group 91
- [cross_source_blocking] source 6812 seq 11: ready 159 ms before start, 159 ms of it with an idle slot in group 232
- [cross_source_blocking] source 12412 seq 4: ready 247 ms before start, 180 ms of it with an idle slot in group 394
- [cross_source_blocking] source 5900 seq 4: ready 205 ms before start, 205 ms of it with an idle slot in group 204
- [cross_source_blocking] source 12892 seq 5: ready 247 ms before start, 247 ms of it with an idle slot in group 408
- [cross_source_blocking] source 7164 seq 11: ready 166 ms before start, 166 ms of it with an idle slot in group 243
- [cross_source_blocking] source 2204 seq 13: ready 247 ms before start, 247 ms of it with an idle slot in group 64
- [cross_source_blocking] source 7292 seq 4: ready 257 ms before start, 257 ms of it with an idle slot in group 250
- [cross_source_blocking] source 7104 seq 6: ready 256 ms before start, 256 ms of it with an idle slot in group 241
- [cross_source_blocking] source 10924 seq 14: ready 256 ms before start, 256 ms of it with an idle slot in group 356
- [cross_source_blocking] source 15296 seq 4: ready 257 ms before start, 257 ms of it with an idle slot in group 478
- [cross_source_blocking] source 7404 seq 12: ready 256 ms before start, 256 ms of it with an idle slot in group 256

## Error samples

- t-371#0 src=11385 seq=21 op=Create rec=48898202664967 ev=0: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-394#0 src=12425 seq=22 op=Update rec=53364968652805 ev=3: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-432#0 src=13611 seq=24 op=Update rec=58458799865858 ev=8: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8923 seq=2 op=Update rec=38323993182209 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-261#0 src=7629 seq=14 op=Get rec=32766305501187 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-344#0 src=10150 seq=21 op=Update rec=43593918054403 ev=3: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-190#0 src=5552 seq=30 op=Complete rec=23845658427393 ev=16: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-427#0 src=13352 seq=27 op=Update rec=57346403336195 ev=8: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-469#1 src=15017 seq=18 op=Update rec=64497523884034 ev=1: NpgsqlException: No suitable host was found.
- t-106#0 src=3196 seq=63 op=Complete rec=13726715478027 ev=1: NpgsqlException: No suitable host was found.
- t-432#0 src=13618 seq=28 op=Update rec=58488864636932 ev=7: NpgsqlException: Exception while reading from stream
- t-064#0 src=2208 seq=25 op=Get rec=9483287789571 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-085#0 src=2777 seq=12 op=Update rec=11927124180994 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-388#0 src=12149 seq=7 op=Update rec=52179557679105 ev=2: NpgsqlException: No suitable host was found.
- t-168#0 src=4871 seq=8 op=Complete rec=20920785698818 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-019#0 src=647 seq=28 op=Complete rec=2778843840518 ev=3: NpgsqlException: No suitable host was found.
- t-491#0 src=15743 seq=8 op=Create rec=67615670140930 ev=0: NpgsqlException: No suitable host was found.
- t-494#0 src=15766 seq=13 op=Get rec=67714454388739 ev=2: NpgsqlException: No suitable host was found.
- t-150#0 src=4388 seq=16 op=Update rec=18846316494851 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-204#0 src=5902 seq=9 op=Update rec=25348896980993 ev=4: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-041 | 34 | 3 | 194.475 | 109.547 | 2732 | 0 | 3907 | 3899 | 1 | 38797.311 | 126353.407 | 149946.367 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 2696 | 2696 | 0 | 51118.079 | 117440.511 | 120061.951 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3545 | 3545 | 2 | 47710.207 | 89653.247 | 108003.327 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1745 | 1745 | 0 | 69730.303 | 73924.607 | 74448.895 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 8060 | 8060 | 0 | 61865.983 | 72351.743 | 74448.895 |
| t-230 | 30 | 2 | 104.35 | 51.662 | 1033 | 0 | 1811 | 1811 | 4 | 8028.159 | 71827.455 | 75497.471 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3269 | 3269 | 0 | 68681.727 | 71827.455 | 72351.743 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5178 | 5178 | 0 | 65011.711 | 69730.303 | 71303.167 |
| t-242 | 28 | 1 | 62.883 | 28.827 | 1662 | 0 | 1043 | 1043 | 4 | 54263.807 | 67633.151 | 69206.015 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 4402 | 4402 | 0 | 60817.407 | 67633.151 | 70254.591 |
| t-170 | 66 | 1 | 27.342 | 30.077 | 3243 | 0 | 1152 | 1152 | 0 | 47185.919 | 67108.863 | 68157.439 |
| t-215 | 31 | 1 | 55.636 | 27.918 | 2427 | 0 | 1044 | 1044 | 4 | 53739.519 | 67108.863 | 70778.879 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 6210 | 6210 | 0 | 59768.831 | 67108.863 | 69730.303 |
| t-182 | 89 | 3 | 81.941 | 119.217 | 2406 | 0 | 4344 | 4344 | 0 | 61865.983 | 66584.575 | 67633.151 |
| t-121 | 76 | 3 | 97.816 | 119.994 | 149 | 0 | 4320 | 4320 | 0 | 54525.951 | 64225.279 | 66584.575 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1635 | 1635 | 0 | 57933.823 | 63700.991 | 69206.015 |
| t-293 | 46 | 1 | 42.231 | 32.409 | 2741 | 0 | 1263 | 1263 | 0 | 48758.783 | 63700.991 | 63963.135 |
| t-253 | 26 | 1 | 73.629 | 32.505 | 1433 | 0 | 1185 | 1185 | 0 | 47448.063 | 63438.847 | 64487.423 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4917 | 4917 | 0 | 57671.679 | 63176.703 | 69730.303 |
| t-045 | 47 | 2 | 82.615 | 64.377 | 1398 | 0 | 2392 | 2392 | 0 | 49545.215 | 62914.559 | 64749.567 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1722 | 1722 | 0 | 58720.255 | 62652.415 | 63176.703 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1722 | 1722 | 4 | 50069.503 | 62652.415 | 64487.423 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1764 | 1764 | 4 | 51904.511 | 62652.415 | 73924.607 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1817 | 1817 | 0 | 44040.191 | 62652.415 | 70778.879 |
| t-108 | 32 | 1 | 58.283 | 31.794 | 694 | 0 | 1207 | 1207 | 0 | 47972.351 | 61865.983 | 63176.703 |
