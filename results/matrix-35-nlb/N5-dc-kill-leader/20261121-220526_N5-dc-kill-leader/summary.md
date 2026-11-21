# N5-dc-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.25s, closing 2.674s, drain 14.565s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 465468 | 465468 | 5946.443 | 3807.3 | 21175.4 | 0 | 1645 | 1645 | 274 | 2355 | 111494 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 297115 | 24470.924 | 30457.855 | 48594.943 | 55181.311 | 58195.967 | 61177.855 |
| e2e_from_send | 297115 | 21930.046 | 11255.807 | 48267.263 | 54427.647 | 57442.303 | 60620.799 |
| producer_lag | 297116 | 2540.868 | 76.287 | 1289.215 | 27328.511 | 27525.119 | 28082.175 |
| publish_call | 465468 | 215.879 | 89.855 | 202.239 | 591.359 | 26836.991 | 27361.279 |
| queue_wait | 297115 | 21828.346 | 11141.119 | 48234.495 | 54394.879 | 57409.535 | 60620.799 |
| broker_dwell | 297115 | 21572.43 | 10805.247 | 48136.191 | 54296.575 | 57278.463 | 60424.191 |
| receive_call | 409691 | 30.441 | 7.731 | 75.711 | 370.943 | 487.167 | 556.031 |
| ctx_load | 297115 | 13.311 | 7.471 | 31.999 | 84.095 | 138.623 | 227.327 |
| process | 297115 | 10.844 | 0.002 | 0.005 | 0.069 | 2637.823 | 3020.799 |
| ctx_commit | 297115 | 77.429 | 59.839 | 162.303 | 351.999 | 496.127 | 584.703 |
| ack | 297104 | 0.01 | 0.004 | 0.007 | 0.049 | 0.874 | 57.567 |
| service | 297115 | 101.694 | 73.215 | 192.255 | 454.399 | 2734.079 | 3500.031 |
| e2e_large | 174 | 7653.247 | 3846.143 | 30031.871 | 33243.135 | 34177.023 | 34177.023 |
| ready_wait | 297115 | 1384.533 | 57.055 | 1456.127 | 38305.791 | 42500.095 | 47874.047 |
| blocked | 297115 | 916.493 | 36.255 | 778.751 | 25395.199 | 34635.775 | 36372.479 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3807.3 | 11538.7 |
| applied_total | 214239.204 | 263097 |
| bench_cpu_cores | 2.317 | 5.819 |
| bench_cpu_pct | 9.654 | 24.25 |
| bench_gc_gen0 | 1.531 | 5 |
| bench_gc_gen1 | 1.041 | 4 |
| bench_gc_gen2 | 0.714 | 3 |
| bench_gc_heap_mb | 575.692 | 1370.1 |
| bench_mem_mb | 1135.978 | 1778.1 |
| bench_threadpool_queue | 36.122 | 385 |
| bench_threadpool_threads | 43.816 | 44 |
| broker_backlog | 135086.551 | 163826 |
| ctr_eb_dc1a_cpu_pct | 723.413 | 771.62 |
| ctr_eb_dc1a_mem_mb | 5283.105 | 5668.6 |
| ctr_eb_dc1b_cpu_pct | 53.503 | 58.58 |
| ctr_eb_dc1b_mem_mb | 2831.082 | 2909.7 |
| ctr_eb_dc2a_cpu_pct | 26.1 | 58.23 |
| ctr_eb_dc2a_mem_mb | 4222.924 | 4627.2 |
| ctr_eb_dc2b_cpu_pct | 36.347 | 440.88 |
| ctr_eb_dc2b_mem_mb | 2493.604 | 3810.9 |
| ctr_eb_etcd_dc1_cpu_pct | 2.127 | 2.36 |
| ctr_eb_etcd_dc1_mem_mb | 29.691 | 30.7 |
| ctr_eb_etcd_dc2_cpu_pct | 1.763 | 2.37 |
| ctr_eb_etcd_dc2_mem_mb | 29.904 | 34.6 |
| ctr_eb_etcd_w_cpu_pct | 1.944 | 2.73 |
| ctr_eb_etcd_w_mem_mb | 52.745 | 55 |
| ctr_eb_haproxy_cpu_pct | 46.429 | 99.69 |
| ctr_eb_haproxy_mem_mb | 24.841 | 31.6 |
| ctr_eb_mosquitto_cpu_pct | 27.63 | 61.61 |
| ctr_eb_mosquitto_mem_mb | 7.245 | 26.9 |
| ctr_eb_mssql_cpu_pct | 2.391 | 4.92 |
| ctr_eb_mssql_mem_mb | 6426.198 | 6518.4 |
| ctr_eb_mysql_cpu_pct | 0.625 | 0.96 |
| ctr_eb_mysql_mem_mb | 7887.282 | 7986.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 5.006 | 5.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.06 |
| ctr_eb_pgbouncer_3_mem_mb | 4.306 | 4.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 4.8 | 4.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 4.545 | 4.6 |
| ctr_eb_postgres_cpu_pct | 1.046 | 4.28 |
| ctr_eb_postgres_mem_mb | 1833.712 | 1846.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 33.606 | 1113.6 |
| host_cpu_pct | 37.135 | 80.1 |
| in_flight | 140400.837 | 166310 |
| nacks_per_s | 33.606 | 1113.6 |
| octopus_dispatcher_cores | 0.677 | 1.34 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 585518.653 | 761584 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 160768.837 | 206652 |
| octopus_mqtt_sent_total | 131975.082 | 177241 |
| octopus_publish_retries_total | 6621.408 | 24063 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.306 | 17 |
| pg_active_loads | 4.408 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 64.776 | 97 |
| pg_conn_idle | 199.163 | 257 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.878 | 11 |
| pg_conn_total | 263.939 | 265 |
| pg_db_size_mb | 1413.053 | 1711.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 6.727 | 8.76 |
| pg_replica_replay_lag_ms | 75.786 | 103.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20065.59 | 24046.9 |
| pg_tup_del_per_s | 21370.9 | 28056.7 |
| pg_tup_fetch_per_s | 199693.006 | 216977 |
| pg_tup_ins_per_s | 36573.835 | 48830.4 |
| pg_tup_upd_per_s | 24760.165 | 29496 |
| pg_wal_mb_per_s | 87.015 | 107.62 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.735 | 1 |
| produced_per_s | 5946.443 | 19167.9 |
| produced_total | 354640.041 | 429407 |
| publish_mb_per_s | 22.439 | 75.91 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 76,244 records are Completed |
| record_counts | pass | 76,244 records found, 76,244 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 274 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 465,468 events applied by workers, 465,468 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 111,494 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 25395 ms, max 36371 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12022 seq 8: ready 115 ms before start, 115 ms of it with an idle slot in group 384
- [cross_source_blocking] source 11031 seq 12: ready 143 ms before start, 143 ms of it with an idle slot in group 360
- [cross_source_blocking] source 8228 seq 11: ready 151 ms before start, 151 ms of it with an idle slot in group 278
- [cross_source_blocking] source 404 seq 12: ready 143 ms before start, 143 ms of it with an idle slot in group 12
- [cross_source_blocking] source 13014 seq 14: ready 144 ms before start, 144 ms of it with an idle slot in group 414
- [cross_source_blocking] source 14862 seq 5: ready 161 ms before start, 161 ms of it with an idle slot in group 466
- [cross_source_blocking] source 12662 seq 8: ready 146 ms before start, 146 ms of it with an idle slot in group 398
- [cross_source_blocking] source 5081 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 177
- [cross_source_blocking] source 6398 seq 7: ready 116 ms before start, 116 ms of it with an idle slot in group 219
- [cross_source_blocking] source 15070 seq 5: ready 128 ms before start, 128 ms of it with an idle slot in group 470
- [cross_source_blocking] source 8766 seq 13: ready 126 ms before start, 126 ms of it with an idle slot in group 305
- [cross_source_blocking] source 873 seq 16: ready 122 ms before start, 122 ms of it with an idle slot in group 26
- [cross_source_blocking] source 13020 seq 9: ready 153 ms before start, 153 ms of it with an idle slot in group 414
- [cross_source_blocking] source 12894 seq 7: ready 118 ms before start, 118 ms of it with an idle slot in group 408
- [cross_source_blocking] source 5550 seq 16: ready 122 ms before start, 122 ms of it with an idle slot in group 190
- [cross_source_blocking] source 9498 seq 4: ready 117 ms before start, 117 ms of it with an idle slot in group 328
- [cross_source_blocking] source 7318 seq 9: ready 123 ms before start, 123 ms of it with an idle slot in group 251
- [cross_source_blocking] source 11300 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 368
- [cross_source_blocking] source 9206 seq 3: ready 141 ms before start, 141 ms of it with an idle slot in group 316
- [cross_source_blocking] source 13976 seq 8: ready 161 ms before start, 161 ms of it with an idle slot in group 444

## Error samples

- t-152#0 src=4425 seq=13 op=Update rec=19005230284803 ev=1: NpgsqlException: Exception while reading from stream
- t-272#0 src=8081 seq=52 op=Update rec=34707630718980 ev=12: NpgsqlException: Exception while reading from stream
- t-044#0 src=1379 seq=5 op=Update rec=5922759901185 ev=2: NpgsqlException: Exception while reading from stream
- t-347#1 src=10367 seq=19 op=Create rec=44525925957638 ev=0: NpgsqlException: Exception while reading from stream
- t-261#1 src=7593 seq=27 op=Update rec=32611686678531 ev=4: NpgsqlException: Exception while reading from stream
- t-461#1 src=14603 seq=12 op=Update rec=62719407423490 ev=1: NpgsqlException: Exception while reading from stream
- t-350#1 src=10711 seq=16 op=Update rec=46003394707458 ev=2: NpgsqlException: Exception while reading from stream
- t-163#0 src=4742 seq=27 op=Get rec=20366734917636 ev=6: NpgsqlException: Exception while reading from stream
- t-005#0 src=224 seq=16 op=Create rec=962072674306 ev=0: NpgsqlException: Exception while reading from stream
- t-371#1 src=11363 seq=27 op=Update rec=48803713384450 ev=4: NpgsqlException: Exception while reading from stream
- t-292#0 src=8504 seq=28 op=Update rec=36524401885191 ev=1: NpgsqlException: Exception while reading from stream
- t-035#0 src=1102 seq=18 op=Get rec=4733053960196 ev=1: NpgsqlException: Exception while reading from stream
- t-438#3 src=13857 seq=29 op=Create rec=59515361820678 ev=0: NpgsqlException: Exception while reading from stream
- t-020#0 src=690 seq=28 op=Complete rec=2963527434247 ev=2: NpgsqlException: Exception while reading from stream
- t-147#2 src=4330 seq=53 op=Get rec=18597208391689 ev=2: NpgsqlException: Exception while reading from stream
- t-468#0 src=14956 seq=52 op=Update rec=64235530878982 ev=12: NpgsqlException: Exception while reading from stream
- t-001#0 src=31 seq=12 op=Create rec=133143986180 ev=0: NpgsqlException: Exception while reading from stream
- t-344#5 src=10183 seq=23 op=Get rec=43735651975169 ev=10: NpgsqlException: Exception while reading from stream
- t-334#0 src=9655 seq=36 op=Update rec=41467909242886 ev=1: NpgsqlException: Exception while reading from stream
- t-383#2 src=11856 seq=26 op=Update rec=50921132261380 ev=4: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3401 | 3401 | 0 | 42205.183 | 60030.975 | 61079.551 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6915 | 6915 | 0 | 41156.607 | 59244.543 | 61341.695 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6749 | 6749 | 0 | 41943.039 | 58195.967 | 60817.407 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1648 | 1648 | 0 | 40632.319 | 58195.967 | 59506.687 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1639 | 1639 | 0 | 44564.479 | 57933.823 | 59506.687 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4400 | 4400 | 0 | 37748.735 | 57933.823 | 60030.975 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3018 | 3018 | 0 | 38273.023 | 57671.679 | 60030.975 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7722 | 7722 | 0 | 40370.175 | 57409.535 | 60293.119 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1800 | 1800 | 0 | 44564.479 | 57409.535 | 57671.679 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3635 | 3635 | 0 | 42991.615 | 57147.391 | 60555.263 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3453 | 3453 | 0 | 44040.191 | 57147.391 | 58720.255 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1689 | 1689 | 0 | 45350.911 | 57147.391 | 59244.543 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3639 | 3639 | 0 | 43778.047 | 57147.391 | 57671.679 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3145 | 3145 | 0 | 40894.463 | 57147.391 | 58720.255 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1658 | 1658 | 0 | 44040.191 | 56885.247 | 58458.111 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1674 | 1674 | 0 | 42991.615 | 56623.103 | 56885.247 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1730 | 1730 | 0 | 44302.335 | 56623.103 | 58195.967 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1667 | 1667 | 0 | 43253.759 | 56623.103 | 56885.247 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1672 | 1672 | 0 | 43253.759 | 56360.959 | 57933.823 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5117 | 5117 | 0 | 42467.327 | 56360.959 | 58195.967 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3397 | 3397 | 0 | 42729.471 | 56360.959 | 57933.823 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1651 | 1651 | 0 | 42991.615 | 56360.959 | 57409.535 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1466 | 1466 | 0 | 40108.031 | 56098.815 | 58458.111 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 2818 | 2818 | 0 | 38797.311 | 56098.815 | 59768.831 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 9243 | 9243 | 0 | 40894.463 | 56098.815 | 58195.967 |
