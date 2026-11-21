# T10-2-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.851s, closing 3.506s, drain 18.112s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 519324 | 519324 | 7318.722 | 4527.267 | 17503.5 | 0 | 1670 | 1670 | 292 | 2615 | 105317 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 350194 | 27636.053 | 33193.983 | 48922.623 | 56819.711 | 59932.671 | 63438.847 |
| e2e_from_send | 350194 | 21135.124 | 14262.271 | 47448.063 | 55279.615 | 58294.271 | 61702.143 |
| producer_lag | 350256 | 6500.025 | 1686.527 | 22577.151 | 24068.095 | 24575.999 | 25001.983 |
| publish_call | 519324 | 212.843 | 120.383 | 248.959 | 470.271 | 21315.583 | 21577.727 |
| queue_wait | 350194 | 21006.266 | 14180.351 | 47382.527 | 55246.847 | 58261.503 | 61439.999 |
| broker_dwell | 350194 | 20775.904 | 13901.823 | 47251.455 | 55115.775 | 58163.199 | 61177.855 |
| receive_call | 462717 | 28.78 | 8.147 | 70.335 | 364.287 | 479.487 | 570.879 |
| ctx_load | 350194 | 10.712 | 6.803 | 23.455 | 59.295 | 117.311 | 252.671 |
| process | 350194 | 10.686 | 0.002 | 0.005 | 0.062 | 2619.391 | 3006.463 |
| ctx_commit | 350194 | 107.374 | 71.295 | 239.103 | 605.183 | 905.727 | 937.471 |
| ack | 350177 | 0.009 | 0.004 | 0.007 | 0.043 | 0.675 | 68.031 |
| service | 350194 | 128.866 | 83.583 | 259.199 | 760.319 | 2754.559 | 3790.847 |
| e2e_large | 192 | 10413.195 | 5017.599 | 26509.311 | 28852.223 | 29753.343 | 29753.343 |
| ready_wait | 350194 | 1315.634 | 42.303 | 1502.207 | 38764.543 | 45088.767 | 53936.127 |
| blocked | 350194 | 826.558 | 27.247 | 623.615 | 26116.095 | 31752.191 | 34144.255 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4527.267 | 11323.4 |
| applied_total | 208565.041 | 299367 |
| bench_cpu_cores | 1.936 | 3.887 |
| bench_cpu_pct | 8.068 | 16.2 |
| bench_gc_gen0 | 1.265 | 4 |
| bench_gc_gen1 | 0.816 | 3 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 1200.345 | 1522.5 |
| bench_mem_mb | 1830.996 | 2132.6 |
| bench_threadpool_queue | 61.857 | 1029 |
| bench_threadpool_threads | 43.327 | 44 |
| broker_backlog | 139467.367 | 182326 |
| ctr_eb_dc1a_cpu_pct | 786.356 | 855.39 |
| ctr_eb_dc1a_mem_mb | 9134.886 | 9660.7 |
| ctr_eb_dc1b_cpu_pct | 53.54 | 68.08 |
| ctr_eb_dc1b_mem_mb | 2603.145 | 2639.8 |
| ctr_eb_dc2a_cpu_pct | 110.069 | 1095.61 |
| ctr_eb_dc2a_mem_mb | 9182.586 | 9461.4 |
| ctr_eb_dc2b_cpu_pct | 30.005 | 68.2 |
| ctr_eb_dc2b_mem_mb | 3511.153 | 3568.6 |
| ctr_eb_etcd_dc1_cpu_pct | 2.48 | 2.83 |
| ctr_eb_etcd_dc1_mem_mb | 40.409 | 40.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.808 | 3.55 |
| ctr_eb_etcd_dc2_mem_mb | 48.151 | 50.6 |
| ctr_eb_etcd_w_cpu_pct | 2.112 | 3.93 |
| ctr_eb_etcd_w_mem_mb | 53.32 | 54.5 |
| ctr_eb_haproxy_cpu_pct | 59.969 | 122.21 |
| ctr_eb_haproxy_mem_mb | 31.998 | 33.9 |
| ctr_eb_mosquitto_cpu_pct | 26.37 | 51.15 |
| ctr_eb_mosquitto_mem_mb | 6.529 | 30.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.022 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 7.7 | 7.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.6 | 7.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.821 | 2.18 |
| ctr_eb_postgres_mem_mb | 1683.902 | 1685.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 34.122 | 1487.7 |
| host_cpu_pct | 43.008 | 82.35 |
| in_flight | 142181.367 | 184593 |
| nacks_per_s | 34.122 | 1487.7 |
| octopus_dispatcher_cores | 0.861 | 1.5 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 577162.306 | 835211 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 150366.204 | 212687 |
| octopus_mqtt_sent_total | 131300.184 | 200412 |
| octopus_publish_retries_total | 9010.082 | 23257 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.408 | 21 |
| pg_active_loads | 1.633 | 14 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 55.143 | 65 |
| pg_conn_idle | 213 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.408 | 8 |
| pg_conn_total | 268.143 | 269 |
| pg_db_size_mb | 1463.594 | 1662.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 9.106 | 30.06 |
| pg_replica_replay_lag_ms | 74.669 | 174.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17618.124 | 20489.8 |
| pg_tup_del_per_s | 27516.578 | 35175.6 |
| pg_tup_fetch_per_s | 511841.861 | 627448.8 |
| pg_tup_ins_per_s | 47754.288 | 62880.5 |
| pg_tup_upd_per_s | 20181.214 | 23252.8 |
| pg_wal_mb_per_s | 84.786 | 111.9 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7318.722 | 18820.1 |
| produced_total | 350746.408 | 483960 |
| publish_mb_per_s | 26.292 | 73.69 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,764 records are Completed |
| record_counts | pass | 82,764 records found, 82,764 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 292 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 519,324 events applied by workers, 519,324 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 105,317 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26116 ms, max 34128 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4013 seq 3: ready 131 ms before start, 131 ms of it with an idle slot in group 134
- [cross_source_blocking] source 1989 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 58
- [cross_source_blocking] source 8301 seq 17: ready 227 ms before start, 119 ms of it with an idle slot in group 282
- [cross_source_blocking] source 8131 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 275
- [cross_source_blocking] source 3333 seq 9: ready 128 ms before start, 128 ms of it with an idle slot in group 114
- [cross_source_blocking] source 11501 seq 26: ready 274 ms before start, 180 ms of it with an idle slot in group 374
- [cross_source_blocking] source 2221 seq 21: ready 223 ms before start, 138 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2885 seq 7: ready 275 ms before start, 166 ms of it with an idle slot in group 92
- [cross_source_blocking] source 2189 seq 11: ready 384 ms before start, 270 ms of it with an idle slot in group 62
- [cross_source_blocking] source 13608 seq 8: ready 113 ms before start, 113 ms of it with an idle slot in group 431
- [cross_source_blocking] source 7394 seq 5: ready 135 ms before start, 135 ms of it with an idle slot in group 255
- [cross_source_blocking] source 3330 seq 6: ready 136 ms before start, 136 ms of it with an idle slot in group 113
- [cross_source_blocking] source 8581 seq 4: ready 208 ms before start, 208 ms of it with an idle slot in group 295
- [cross_source_blocking] source 709 seq 5: ready 208 ms before start, 208 ms of it with an idle slot in group 21
- [cross_source_blocking] source 13020 seq 9: ready 113 ms before start, 113 ms of it with an idle slot in group 414
- [cross_source_blocking] source 5550 seq 16: ready 254 ms before start, 254 ms of it with an idle slot in group 190
- [cross_source_blocking] source 6050 seq 3: ready 136 ms before start, 136 ms of it with an idle slot in group 207
- [cross_source_blocking] source 5421 seq 8: ready 208 ms before start, 208 ms of it with an idle slot in group 185
- [cross_source_blocking] source 605 seq 7: ready 209 ms before start, 209 ms of it with an idle slot in group 17
- [cross_source_blocking] source 7330 seq 3: ready 119 ms before start, 119 ms of it with an idle slot in group 252

## Error samples

- t-194#0 src=5615 seq=29 op=Update rec=24116241367045 ev=4: NpgsqlException: Exception while reading from stream
- t-205#0 src=5915 seq=28 op=Get rec=25404731555845 ev=6: NpgsqlException: Exception while reading from stream
- t-122#0 src=3627 seq=23 op=Update rec=15577846382594 ev=8: NpgsqlException: Exception while reading from stream
- t-055#1 src=1924 seq=33 op=Update rec=8263517077511 ev=1: NpgsqlException: Exception while reading from stream
- t-337#0 src=9689 seq=14 op=Update rec=41613938130945 ev=3: NpgsqlException: Exception while reading from stream
- t-040#1 src=1239 seq=9 op=Update rec=5321464479745 ev=5: NpgsqlException: Exception while reading from stream
- t-029#0 src=973 seq=60 op=Update rec=4179003179017 ev=4: NpgsqlException: Exception while reading from stream
- t-468#1 src=14907 seq=47 op=Complete rec=64025077481479 ev=4: NpgsqlException: Exception while reading from stream
- t-360#0 src=11029 seq=39 op=Update rec=47369194307587 ev=10: NpgsqlException: Exception while reading from stream
- t-196#0 src=5681 seq=27 op=Complete rec=24399709208580 ev=2: NpgsqlException: Exception while reading from stream
- t-216#0 src=6349 seq=29 op=Update rec=27268747362307 ev=6: NpgsqlException: Exception while reading from stream
- t-018#0 src=628 seq=18 op=Update rec=2697239461891 ev=2: NpgsqlException: Exception while reading from stream
- t-299#0 src=8626 seq=18 op=Create rec=37048387895302 ev=0: NpgsqlException: Exception while reading from stream
- t-040#0 src=1282 seq=10 op=Update rec=5506148073475 ev=2: NpgsqlException: Exception while reading from stream
- t-136#1 src=4158 seq=7 op=Update rec=17858474016770 ev=1: NpgsqlException: Exception while reading from stream
- t-305#0 src=8760 seq=28 op=Update rec=37623913512964 ev=6: NpgsqlException: Exception while reading from stream
- t-103#0 src=3100 seq=12 op=Get rec=13314398617602 ev=1: NpgsqlException: Exception while reading from stream
- t-382#0 src=11816 seq=9 op=Get rec=50749333569538 ev=1: NpgsqlException: Exception while reading from stream
- t-041#0 src=1307 seq=68 op=Update rec=5613522255883 ev=1: NpgsqlException: Exception while reading from stream
- t-265#0 src=7897 seq=9 op=Update rec=33917356736515 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1926 | 1926 | 0 | 44302.335 | 61341.695 | 63438.847 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1837 | 1837 | 0 | 45613.055 | 61079.551 | 62128.127 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4055 | 4055 | 0 | 44302.335 | 60293.119 | 61341.695 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3837 | 3837 | 0 | 45088.767 | 60293.119 | 62128.127 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4053 | 4053 | 0 | 44302.335 | 60293.119 | 61603.839 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1867 | 1867 | 0 | 42991.615 | 60030.975 | 63438.847 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7618 | 7618 | 0 | 42729.471 | 59506.687 | 62914.559 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1876 | 1876 | 0 | 44826.623 | 59244.543 | 62128.127 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1855 | 1855 | 0 | 42205.183 | 59244.543 | 60293.119 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5541 | 5541 | 0 | 44040.191 | 59244.543 | 60293.119 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7743 | 7743 | 0 | 42467.327 | 59244.543 | 61603.839 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1874 | 1874 | 0 | 42991.615 | 58982.399 | 59768.831 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1871 | 1871 | 0 | 44302.335 | 58982.399 | 60293.119 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1830 | 1830 | 0 | 44302.335 | 58982.399 | 59506.687 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1721 | 1721 | 0 | 43778.047 | 58982.399 | 60030.975 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3796 | 3796 | 0 | 43778.047 | 58720.255 | 59506.687 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1860 | 1860 | 0 | 43253.759 | 58720.255 | 59244.543 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1972 | 1972 | 0 | 45350.911 | 58720.255 | 59244.543 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3381 | 3381 | 0 | 40894.463 | 58458.111 | 62390.271 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5758 | 5758 | 0 | 43515.903 | 58458.111 | 59506.687 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4944 | 4944 | 0 | 39583.743 | 58458.111 | 61079.551 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3510 | 3510 | 0 | 41680.895 | 58458.111 | 60555.263 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 8665 | 8665 | 0 | 42729.471 | 58195.967 | 61341.695 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1738 | 1738 | 0 | 42991.615 | 58195.967 | 58982.399 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5263 | 5263 | 0 | 42991.615 | 58195.967 | 59244.543 |
