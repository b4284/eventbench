# C4-dc-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 62.712s, closing 2.481s, drain 10.402s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 451084 | 451084 | 5299.92 | 4389.188 | 16261.6 | 0 | 1792 | 1792 | 270 | 2282 | 121457 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 282452 | 20054.77 | 11173.887 | 45875.199 | 52133.887 | 54689.791 | 58425.343 |
| e2e_from_send | 282452 | 19957.817 | 11001.855 | 45809.663 | 51904.511 | 54493.183 | 58425.343 |
| producer_lag | 282961 | 96.878 | 2.815 | 262.655 | 860.159 | 1179.647 | 30670.847 |
| publish_call | 451084 | 213.472 | 70.527 | 164.095 | 472.831 | 30523.391 | 30752.767 |
| queue_wait | 282452 | 19867.685 | 10895.359 | 45776.895 | 51871.743 | 54427.647 | 58392.575 |
| broker_dwell | 282452 | 19694.403 | 10649.599 | 45711.359 | 51806.207 | 54394.879 | 58359.807 |
| receive_call | 400045 | 31.256 | 8.359 | 84.479 | 353.023 | 481.791 | 521.215 |
| ctx_load | 282452 | 11.682 | 7.463 | 26.015 | 65.791 | 102.271 | 163.711 |
| process | 282452 | 10.803 | 0.002 | 0.005 | 0.061 | 2629.631 | 3004.415 |
| ctx_commit | 282452 | 67.548 | 59.903 | 130.367 | 230.783 | 339.711 | 431.871 |
| ack | 282448 | 0.009 | 0.004 | 0.007 | 0.053 | 0.855 | 49.855 |
| service | 282452 | 90.117 | 71.423 | 150.655 | 293.631 | 2723.839 | 3207.167 |
| e2e_large | 170 | 6711.13 | 3379.199 | 31997.951 | 35651.583 | 36798.463 | 36798.463 |
| ready_wait | 282452 | 1557.991 | 82.943 | 1604.607 | 37191.679 | 40861.695 | 45252.607 |
| blocked | 282452 | 978.144 | 50.687 | 801.279 | 23461.887 | 26148.863 | 30588.927 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4389.188 | 13566 |
| applied_total | 257378.824 | 311595 |
| bench_cpu_cores | 2.14 | 5.165 |
| bench_cpu_pct | 8.917 | 21.52 |
| bench_gc_gen0 | 1.118 | 4 |
| bench_gc_gen1 | 0.588 | 4 |
| bench_gc_gen2 | 0.275 | 1 |
| bench_gc_heap_mb | 1199.855 | 1433.1 |
| bench_mem_mb | 1906.62 | 2040.4 |
| bench_threadpool_queue | 56.294 | 521 |
| bench_threadpool_threads | 40.49 | 44 |
| broker_backlog | 106608.02 | 116814 |
| ctr_eb_dc1a_cpu_pct | 49.008 | 55.75 |
| ctr_eb_dc1a_mem_mb | 2616.191 | 2738 |
| ctr_eb_dc1b_cpu_pct | 761.384 | 800.89 |
| ctr_eb_dc1b_mem_mb | 7529.082 | 8057.6 |
| ctr_eb_dc2a_cpu_pct | 24.261 | 56.91 |
| ctr_eb_dc2a_mem_mb | 2671.702 | 2793.3 |
| ctr_eb_dc2b_cpu_pct | 24.804 | 52.82 |
| ctr_eb_dc2b_mem_mb | 4564.075 | 5569.5 |
| ctr_eb_etcd_dc1_cpu_pct | 1.744 | 1.92 |
| ctr_eb_etcd_dc1_mem_mb | 25.364 | 25.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.545 | 2.03 |
| ctr_eb_etcd_dc2_mem_mb | 24.341 | 27.4 |
| ctr_eb_etcd_w_cpu_pct | 1.971 | 2.95 |
| ctr_eb_etcd_w_mem_mb | 57.6 | 59.5 |
| ctr_eb_haproxy_cpu_pct | 51.167 | 108.14 |
| ctr_eb_haproxy_mem_mb | 30.047 | 33 |
| ctr_eb_mosquitto_cpu_pct | 26.016 | 57.87 |
| ctr_eb_mosquitto_mem_mb | 5.245 | 24.1 |
| ctr_eb_mssql_cpu_pct | 2.137 | 3.56 |
| ctr_eb_mssql_mem_mb | 4976.596 | 5073.5 |
| ctr_eb_mysql_cpu_pct | 0.571 | 0.9 |
| ctr_eb_mysql_mem_mb | 6228.688 | 6335.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 3.306 | 3.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 2.645 | 3.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 3.306 | 3.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 3.206 | 3.3 |
| ctr_eb_postgres_cpu_pct | 1.096 | 3.76 |
| ctr_eb_postgres_mem_mb | 1601.306 | 1619.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 35.149 | 1759.6 |
| host_cpu_pct | 34.439 | 78.82 |
| in_flight | 106343.569 | 114773 |
| nacks_per_s | 35.149 | 1759.6 |
| octopus_dispatcher_cores | 0.646 | 1.42 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 640245.49 | 793649 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 169075.392 | 211586 |
| octopus_mqtt_sent_total | 162033.745 | 208865 |
| octopus_publish_retries_total | 7790.549 | 27271 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.706 | 17 |
| pg_active_loads | 2.706 | 23 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 61.706 | 90 |
| pg_conn_idle | 205.471 | 250 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.824 | 12 |
| pg_conn_total | 267.176 | 268 |
| pg_db_size_mb | 1583.559 | 1779.1 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 20.915 | 34.19 |
| pg_replica_replay_lag_ms | 258.375 | 428 |
| pg_replicas | 2.412 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22302.806 | 28821.8 |
| pg_tup_del_per_s | 22538.482 | 30384.9 |
| pg_tup_fetch_per_s | 284453.431 | 352594.1 |
| pg_tup_ins_per_s | 36085.806 | 50323.6 |
| pg_tup_upd_per_s | 28409.951 | 34065.7 |
| pg_wal_mb_per_s | 83.994 | 113.91 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.196 | 1 |
| produced_per_s | 5299.92 | 19363.9 |
| produced_total | 363722.392 | 417742 |
| publish_mb_per_s | 19.816 | 75.85 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,479 records are Completed |
| record_counts | pass | 74,479 records found, 74,479 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 270 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 451,084 events applied by workers, 451,084 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 121,457 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 23462 ms, max 30585 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9519 seq 14: ready 146 ms before start, 146 ms of it with an idle slot in group 330
- [cross_source_blocking] source 2401 seq 6: ready 108 ms before start, 101 ms of it with an idle slot in group 70
- [cross_source_blocking] source 5559 seq 9: ready 139 ms before start, 139 ms of it with an idle slot in group 191
- [cross_source_blocking] source 15348 seq 5: ready 127 ms before start, 127 ms of it with an idle slot in group 480
- [cross_source_blocking] source 139 seq 12: ready 195 ms before start, 100 ms of it with an idle slot in group 2
- [cross_source_blocking] source 5183 seq 3: ready 138 ms before start, 138 ms of it with an idle slot in group 179
- [cross_source_blocking] source 3351 seq 9: ready 140 ms before start, 140 ms of it with an idle slot in group 115
- [cross_source_blocking] source 2204 seq 13: ready 116 ms before start, 116 ms of it with an idle slot in group 64
- [cross_source_blocking] source 5049 seq 10: ready 201 ms before start, 142 ms of it with an idle slot in group 175
- [cross_source_blocking] source 5044 seq 12: ready 198 ms before start, 138 ms of it with an idle slot in group 175
- [cross_source_blocking] source 12739 seq 6: ready 195 ms before start, 128 ms of it with an idle slot in group 402
- [cross_source_blocking] source 9085 seq 3: ready 202 ms before start, 105 ms of it with an idle slot in group 313
- [cross_source_blocking] source 4235 seq 5: ready 224 ms before start, 224 ms of it with an idle slot in group 142
- [cross_source_blocking] source 8475 seq 16: ready 224 ms before start, 224 ms of it with an idle slot in group 289
- [cross_source_blocking] source 1885 seq 4: ready 233 ms before start, 233 ms of it with an idle slot in group 54
- [cross_source_blocking] source 8269 seq 3: ready 226 ms before start, 226 ms of it with an idle slot in group 281
- [cross_source_blocking] source 4241 seq 3: ready 227 ms before start, 227 ms of it with an idle slot in group 142
- [cross_source_blocking] source 11212 seq 8: ready 232 ms before start, 232 ms of it with an idle slot in group 365
- [cross_source_blocking] source 13212 seq 13: ready 223 ms before start, 223 ms of it with an idle slot in group 421
- [cross_source_blocking] source 7316 seq 8: ready 224 ms before start, 224 ms of it with an idle slot in group 251

## Error samples

- t-119L#1 src=3464 seq=4 op=Update rec=14877766713345 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3429 seq=4 op=Update rec=14727442857985 ev=3: NpgsqlException: Exception while reading from stream
- t-046#0 src=1549 seq=28 op=Create rec=6652904341511 ev=0: NpgsqlException: Exception while reading from stream
- t-239#1 src=7075 seq=34 op=Update rec=30386893619208 ev=6: NpgsqlException: Exception while reading from stream
- t-282#0 src=8285 seq=37 op=Get rec=35583804047363 ev=9: NpgsqlException: Exception while reading from stream
- t-227#0 src=6671 seq=26 op=Update rec=28651726831619 ev=9: NpgsqlException: Exception while reading from stream
- t-468#0 src=14904 seq=39 op=Create rec=64012192579590 ev=0: NpgsqlException: Exception while reading from stream
- t-083#0 src=2719 seq=17 op=Update rec=11678016077829 ev=1: NpgsqlException: Exception while reading from stream
- t-091#0 src=2880 seq=17 op=Complete rec=12369505812481 ev=10: NpgsqlException: Exception while reading from stream
- t-132#0 src=3987 seq=67 op=Update rec=17124034609164 ev=1: NpgsqlException: Exception while reading from stream
- t-231#0 src=6772 seq=21 op=Update rec=29085518528515 ev=2: NpgsqlException: Exception while reading from stream
- t-396#0 src=12436 seq=37 op=Complete rec=53412213293062 ev=2: NpgsqlException: Exception while reading from stream
- t-270#0 src=7998 seq=17 op=Update rec=34351148433411 ev=1: NpgsqlException: Exception while reading from stream
- t-208#0 src=6071 seq=36 op=Update rec=26074746454021 ev=8: NpgsqlException: Exception while reading from stream
- t-080#0 src=2635 seq=17 op=Update rec=11317238824962 ev=3: NpgsqlException: Exception while reading from stream
- t-040#0 src=1228 seq=15 op=Create rec=5274219839492 ev=0: NpgsqlException: Exception while reading from stream
- t-142#0 src=4245 seq=15 op=Update rec=18232136171521 ev=7: NpgsqlException: Exception while reading from stream
- t-467#0 src=14895 seq=11 op=Update rec=63973537873921 ev=5: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3485 seq=3 op=Update rec=14967961026561 ev=2: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1581 | 1581 | 0 | 45350.911 | 57147.391 | 58458.111 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1649 | 1649 | 0 | 44302.335 | 55574.527 | 56623.103 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1627 | 1627 | 0 | 42729.471 | 55312.383 | 55574.527 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1736 | 1736 | 0 | 42991.615 | 55312.383 | 55836.671 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3510 | 3510 | 0 | 43253.759 | 55050.239 | 55574.527 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1618 | 1618 | 0 | 42205.183 | 54788.095 | 55836.671 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3361 | 3361 | 0 | 42991.615 | 54788.095 | 55312.383 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1515 | 1515 | 0 | 42729.471 | 54525.951 | 56623.103 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1429 | 1429 | 0 | 41680.895 | 54263.807 | 54788.095 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6539 | 6539 | 0 | 42991.615 | 54263.807 | 56623.103 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3302 | 3302 | 0 | 42991.615 | 54263.807 | 57409.535 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1618 | 1618 | 0 | 42467.327 | 54263.807 | 54788.095 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3514 | 3514 | 0 | 42729.471 | 54263.807 | 54788.095 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4549 | 4549 | 0 | 41418.751 | 53739.519 | 54525.951 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4830 | 4830 | 0 | 41943.039 | 53739.519 | 54263.807 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4971 | 4971 | 0 | 41943.039 | 53477.375 | 55836.671 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6697 | 6697 | 0 | 41943.039 | 53477.375 | 55574.527 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1559 | 1559 | 0 | 41680.895 | 53215.231 | 53739.519 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3312 | 3312 | 0 | 41680.895 | 53215.231 | 53739.519 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1444 | 1444 | 0 | 40370.175 | 52953.087 | 53739.519 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4650 | 4650 | 0 | 41418.751 | 52953.087 | 53739.519 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1681 | 1681 | 0 | 41156.607 | 52690.943 | 53739.519 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3056 | 3056 | 0 | 41156.607 | 52690.943 | 53477.375 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 4441 | 4441 | 0 | 40108.031 | 52428.799 | 52953.087 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1400 | 1400 | 0 | 39583.743 | 52166.655 | 52953.087 |
