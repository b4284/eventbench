# T10-6-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.495s, closing 3s, drain 17.749s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 477225 | 477225 | 6449.951 | 4113 | 18914.6 | 0 | 1813 | 1813 | 280 | 2413 | 103598 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 308397 | 27383.876 | 34471.935 | 48988.159 | 55836.671 | 58851.327 | 63307.775 |
| e2e_from_send | 308397 | 21737.822 | 13500.415 | 46891.007 | 53936.127 | 56983.551 | 61767.679 |
| producer_lag | 308746 | 5641.937 | 2051.071 | 25133.055 | 26492.927 | 26771.455 | 27312.127 |
| publish_call | 477225 | 233.282 | 126.975 | 250.623 | 546.815 | 22708.223 | 23166.975 |
| queue_wait | 308397 | 21622.849 | 13426.687 | 46825.471 | 53903.359 | 56950.783 | 61472.767 |
| broker_dwell | 308397 | 21425.148 | 13123.583 | 46694.399 | 53772.287 | 56852.479 | 61374.463 |
| receive_call | 419761 | 31.059 | 9.423 | 76.735 | 349.951 | 479.999 | 556.031 |
| ctx_load | 308397 | 11.752 | 7.467 | 25.727 | 66.047 | 191.103 | 260.351 |
| process | 308397 | 10.805 | 0.002 | 0.005 | 0.069 | 2631.679 | 3006.463 |
| ctx_commit | 308397 | 92.287 | 66.879 | 206.463 | 356.351 | 460.543 | 553.983 |
| ack | 308382 | 0.009 | 0.004 | 0.007 | 0.048 | 0.825 | 59.135 |
| service | 308397 | 114.942 | 81.087 | 231.551 | 428.799 | 2727.935 | 3325.951 |
| e2e_large | 178 | 9455.403 | 4935.679 | 28393.471 | 31113.215 | 32309.247 | 32309.247 |
| ready_wait | 308397 | 1393.878 | 46.559 | 1512.447 | 38076.415 | 43188.223 | 50200.575 |
| blocked | 308397 | 943.129 | 31.743 | 697.343 | 27279.359 | 31637.503 | 33718.271 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4113 | 11778.1 |
| applied_total | 201544.245 | 270413 |
| bench_cpu_cores | 1.852 | 3.766 |
| bench_cpu_pct | 7.715 | 15.69 |
| bench_gc_gen0 | 1.204 | 4 |
| bench_gc_gen1 | 0.755 | 3 |
| bench_gc_gen2 | 0.347 | 2 |
| bench_gc_heap_mb | 1207.055 | 1533.4 |
| bench_mem_mb | 1856.039 | 2046.1 |
| bench_threadpool_queue | 48.184 | 708 |
| bench_threadpool_threads | 43.429 | 44 |
| broker_backlog | 134931.245 | 173111 |
| ctr_eb_dc1a_cpu_pct | 55.503 | 62.15 |
| ctr_eb_dc1a_mem_mb | 1480.123 | 1865.4 |
| ctr_eb_dc1b_cpu_pct | 804.669 | 865.31 |
| ctr_eb_dc1b_mem_mb | 4083.959 | 5024.9 |
| ctr_eb_dc2a_cpu_pct | 29.407 | 60.4 |
| ctr_eb_dc2a_mem_mb | 1747.12 | 2038 |
| ctr_eb_dc2b_cpu_pct | 64.361 | 408.85 |
| ctr_eb_dc2b_mem_mb | 1852.859 | 3171.9 |
| ctr_eb_etcd_dc1_cpu_pct | 2.258 | 2.48 |
| ctr_eb_etcd_dc1_mem_mb | 37.891 | 38.6 |
| ctr_eb_etcd_dc2_cpu_pct | 1.539 | 1.91 |
| ctr_eb_etcd_dc2_mem_mb | 30.569 | 32.8 |
| ctr_eb_etcd_w_cpu_pct | 2.23 | 2.93 |
| ctr_eb_etcd_w_mem_mb | 56.349 | 57.7 |
| ctr_eb_haproxy_cpu_pct | 58.063 | 107.76 |
| ctr_eb_haproxy_mem_mb | 36.592 | 39.6 |
| ctr_eb_mosquitto_cpu_pct | 25.278 | 51.84 |
| ctr_eb_mosquitto_mem_mb | 5.659 | 14.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.021 | 0.06 |
| ctr_eb_pgbouncer_2_mem_mb | 7.5 | 7.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.5 | 7.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 7.824 | 8.1 |
| ctr_eb_postgres_cpu_pct | 0.866 | 2.49 |
| ctr_eb_postgres_mem_mb | 1667.184 | 1668 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 37 | 1787 |
| host_cpu_pct | 41.707 | 81.96 |
| in_flight | 139184.939 | 171666 |
| nacks_per_s | 37 | 1787 |
| octopus_dispatcher_cores | 0.771 | 1.48 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 561728.612 | 749703 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 149146.653 | 196383 |
| octopus_mqtt_sent_total | 127786.878 | 177176 |
| octopus_publish_retries_total | 9464.306 | 23829 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.265 | 20 |
| pg_active_loads | 4.041 | 6 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 53.531 | 69 |
| pg_conn_idle | 214.061 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.367 | 2 |
| pg_conn_total | 267.592 | 268 |
| pg_db_size_mb | 1403.012 | 1587.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 12.168 | 16.18 |
| pg_replica_replay_lag_ms | 98.384 | 138.3 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19345.055 | 21167.6 |
| pg_tup_del_per_s | 26313.2 | 30767.6 |
| pg_tup_fetch_per_s | 483419.218 | 582053.1 |
| pg_tup_ins_per_s | 47870.229 | 53821 |
| pg_tup_upd_per_s | 22025.918 | 25611.5 |
| pg_wal_mb_per_s | 85.66 | 97.29 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6449.951 | 15462.2 |
| produced_total | 340729.184 | 442079 |
| publish_mb_per_s | 24.3 | 77.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 77,623 records are Completed |
| record_counts | pass | 77,623 records found, 77,623 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 280 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 477,225 events applied by workers, 477,225 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 103,598 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27279 ms, max 33688 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7164 seq 10: ready 105 ms before start, 105 ms of it with an idle slot in group 243
- [cross_source_blocking] source 1020 seq 12: ready 113 ms before start, 113 ms of it with an idle slot in group 31
- [cross_source_blocking] source 13020 seq 9: ready 128 ms before start, 128 ms of it with an idle slot in group 414
- [cross_source_blocking] source 7260 seq 6: ready 110 ms before start, 110 ms of it with an idle slot in group 248
- [cross_source_blocking] source 8765 seq 17: ready 101 ms before start, 101 ms of it with an idle slot in group 305
- [cross_source_blocking] source 10921 seq 13: ready 114 ms before start, 114 ms of it with an idle slot in group 355
- [cross_source_blocking] source 5884 seq 3: ready 660 ms before start, 138 ms of it with an idle slot in group 204
- [cross_source_blocking] source 4193 seq 10: ready 139 ms before start, 139 ms of it with an idle slot in group 138
- [cross_source_blocking] source 12705 seq 13: ready 232 ms before start, 232 ms of it with an idle slot in group 400
- [cross_source_blocking] source 1785 seq 4: ready 234 ms before start, 234 ms of it with an idle slot in group 51
- [cross_source_blocking] source 2357 seq 7: ready 224 ms before start, 224 ms of it with an idle slot in group 68
- [cross_source_blocking] source 5563 seq 10: ready 192 ms before start, 192 ms of it with an idle slot in group 191
- [cross_source_blocking] source 2933 seq 7: ready 225 ms before start, 225 ms of it with an idle slot in group 94
- [cross_source_blocking] source 15253 seq 7: ready 225 ms before start, 225 ms of it with an idle slot in group 476
- [cross_source_blocking] source 7389 seq 6: ready 233 ms before start, 233 ms of it with an idle slot in group 254
- [cross_source_blocking] source 4249 seq 7: ready 122 ms before start, 122 ms of it with an idle slot in group 143
- [cross_source_blocking] source 9289 seq 16: ready 233 ms before start, 172 ms of it with an idle slot in group 320
- [cross_source_blocking] source 12443 seq 10: ready 238 ms before start, 238 ms of it with an idle slot in group 396
- [cross_source_blocking] source 8917 seq 7: ready 225 ms before start, 225 ms of it with an idle slot in group 307
- [cross_source_blocking] source 10629 seq 6: ready 233 ms before start, 187 ms of it with an idle slot in group 349

## Error samples

- t-119L#0 src=3455 seq=3 op=Update rec=14839112007681 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3482 seq=5 op=Update rec=14955076124673 ev=3: NpgsqlException: Exception while reading from stream
- t-002#0 src=141 seq=37 op=Complete rec=605590388740 ev=5: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3485 seq=3 op=Update rec=14967961026561 ev=2: NpgsqlException: Exception while reading from stream
- t-129#0 src=3818 seq=50 op=Get rec=16398185136133 ev=9: NpgsqlException: Exception while reading from stream
- t-175#0 src=5044 seq=30 op=Update rec=21663815041027 ev=9: NpgsqlException: Exception while reading from stream
- t-304#0 src=8746 seq=35 op=Update rec=37563783970822 ev=6: NpgsqlException: Exception while reading from stream
- t-140#0 src=4214 seq=27 op=Update rec=18098992185345 ev=4: NpgsqlException: Exception while reading from stream
- t-031#0 src=1018 seq=42 op=Get rec=4372276707337 ev=5: NpgsqlException: Exception while reading from stream
- t-188#0 src=5478 seq=22 op=Update rec=23527830847490 ev=6: NpgsqlException: Exception while reading from stream
- t-329#0 src=9509 seq=31 op=Get rec=40840844017666 ev=10: NpgsqlException: Exception while reading from stream
- t-245#0 src=7181 seq=14 op=Update rec=30842160152579 ev=2: NpgsqlException: Exception while reading from stream
- t-474#0 src=15206 seq=13 op=Update rec=65309272702979 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3429 seq=4 op=Update rec=14727442857985 ev=3: NpgsqlException: Exception while reading from stream
- t-493#0 src=15760 seq=40 op=Update rec=67688684584968 ev=6: NpgsqlException: Exception while reading from stream
- t-319L#1 src=9258 seq=3 op=Update rec=39762807226369 ev=2: NpgsqlException: Exception while reading from stream
- t-052#0 src=1830 seq=11 op=Complete rec=7859790151681 ev=7: NpgsqlException: Exception while reading from stream
- t-031#0 src=1016 seq=46 op=Update rec=4363686772740 ev=8: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3464 seq=4 op=Update rec=14877766713345 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3436 seq=4 op=Update rec=14757507629057 ev=2: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1775 | 1775 | 0 | 45088.767 | 60030.975 | 62914.559 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6949 | 6949 | 0 | 42991.615 | 59506.687 | 63438.847 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3491 | 3491 | 0 | 44302.335 | 59506.687 | 62128.127 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1693 | 1693 | 0 | 45350.911 | 58982.399 | 60555.263 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3729 | 3729 | 0 | 44826.623 | 58982.399 | 60555.263 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3525 | 3525 | 0 | 45350.911 | 58982.399 | 62914.559 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1718 | 1718 | 0 | 44564.479 | 58720.255 | 60817.407 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1700 | 1700 | 0 | 42205.183 | 58720.255 | 60555.263 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3475 | 3475 | 0 | 44040.191 | 58195.967 | 59768.831 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1719 | 1719 | 0 | 44302.335 | 58195.967 | 59768.831 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3720 | 3720 | 0 | 44826.623 | 58195.967 | 62390.271 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7935 | 7935 | 0 | 41943.039 | 57933.823 | 61079.551 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1764 | 1764 | 0 | 44040.191 | 57933.823 | 59506.687 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5088 | 5088 | 0 | 44302.335 | 57933.823 | 59506.687 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1685 | 1685 | 0 | 44302.335 | 57671.679 | 59768.831 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1742 | 1742 | 0 | 45088.767 | 57671.679 | 59244.543 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1839 | 1839 | 0 | 45613.055 | 57671.679 | 58982.399 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7091 | 7091 | 0 | 42991.615 | 57671.679 | 60817.407 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1601 | 1601 | 0 | 44302.335 | 57409.535 | 59244.543 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1589 | 1589 | 0 | 44302.335 | 57409.535 | 58720.255 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1704 | 1704 | 0 | 44302.335 | 57147.391 | 58720.255 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4508 | 4508 | 0 | 40632.319 | 57147.391 | 59768.831 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1507 | 1507 | 0 | 41943.039 | 56885.247 | 58458.111 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5244 | 5244 | 0 | 44302.335 | 56885.247 | 59768.831 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1527 | 1527 | 0 | 41156.607 | 56885.247 | 58982.399 |
