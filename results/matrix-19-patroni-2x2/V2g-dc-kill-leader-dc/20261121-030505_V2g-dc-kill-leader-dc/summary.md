# V2g-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.272s, closing 2.487s, drain 46.074s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 514549 | 514549 | 6856.371 | 3984.453 | 20120.5 | 104 | 3133 | 3133 | 288 | 2592 | 125794 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 345516 | 32154.202 | 36569.087 | 52166.655 | 64454.655 | 68354.047 | 74514.431 |
| e2e_from_send | 345516 | 24460.846 | 19316.735 | 50331.647 | 61571.071 | 67567.615 | 72155.135 |
| producer_lag | 345516 | 7693.371 | 714.239 | 23609.343 | 24625.151 | 24952.831 | 25313.279 |
| publish_call | 514549 | 210.954 | 113.279 | 256.767 | 693.759 | 20152.319 | 21807.103 |
| queue_wait | 345516 | 24336.108 | 19185.663 | 50266.111 | 61439.999 | 67436.543 | 71958.527 |
| broker_dwell | 345516 | 24130.772 | 18415.615 | 50200.575 | 61341.695 | 67371.007 | 71761.919 |
| receive_call | 440738 | 30.369 | 6.539 | 89.087 | 351.999 | 488.447 | 563.199 |
| ctx_load | 345516 | 20.591 | 3.043 | 14.559 | 49.503 | 244.863 | 15024.127 |
| process | 345516 | 10.686 | 0.002 | 0.005 | 0.045 | 2619.391 | 3002.367 |
| ctx_commit | 345516 | 93.38 | 28.255 | 238.335 | 880.639 | 1033.727 | 1112.063 |
| ack | 344125 | 0.006 | 0.003 | 0.006 | 0.029 | 0.327 | 37.503 |
| service | 345516 | 124.726 | 34.111 | 256.127 | 1000.959 | 3469.311 | 17924.095 |
| e2e_large | 188 | 9265.092 | 3768.319 | 26460.159 | 27656.191 | 29556.735 | 29556.735 |
| ready_wait | 345516 | 1437.718 | 92.351 | 1945.599 | 36863.999 | 46071.807 | 63668.223 |
| blocked | 345516 | 997.779 | 60.703 | 822.271 | 27115.519 | 36012.031 | 53411.839 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3984.453 | 13165.3 |
| applied_total | 195447.776 | 277062 |
| bench_cpu_cores | 1.657 | 4.058 |
| bench_cpu_pct | 6.903 | 16.91 |
| bench_gc_gen0 | 1.449 | 4 |
| bench_gc_gen1 | 1.265 | 4 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 939.008 | 1292.9 |
| bench_mem_mb | 1487.327 | 1621.2 |
| bench_threadpool_queue | 20.857 | 271 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 142198.265 | 201055 |
| ctr_eb_dc1a_cpu_pct | 163.253 | 1149.72 |
| ctr_eb_dc1a_mem_mb | 4747.167 | 5868.9 |
| ctr_eb_dc1b_cpu_pct | 63.948 | 306.71 |
| ctr_eb_dc1b_mem_mb | 7347.429 | 7392.9 |
| ctr_eb_dc2a_cpu_pct | 53.57 | 62.99 |
| ctr_eb_dc2a_mem_mb | 4566.668 | 4957.2 |
| ctr_eb_dc2b_cpu_pct | 757.629 | 840.98 |
| ctr_eb_dc2b_mem_mb | 10849.355 | 11444.1 |
| ctr_eb_etcd_dc1_cpu_pct | 1.418 | 2.29 |
| ctr_eb_etcd_dc1_mem_mb | 22.843 | 25.9 |
| ctr_eb_etcd_dc2_cpu_pct | 1.572 | 2.03 |
| ctr_eb_etcd_dc2_mem_mb | 31.159 | 32.6 |
| ctr_eb_etcd_w_cpu_pct | 1.841 | 2.96 |
| ctr_eb_etcd_w_mem_mb | 55.461 | 56.9 |
| ctr_eb_mosquitto_cpu_pct | 24.292 | 50.69 |
| ctr_eb_mosquitto_mem_mb | 7.731 | 19.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.021 | 0.05 |
| ctr_eb_pgbouncer_2_mem_mb | 9.5 | 9.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.021 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.916 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.805 | 1.92 |
| ctr_eb_postgres_mem_mb | 2033.404 | 2034.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 63.894 | 2060.2 |
| host_cpu_pct | 41.505 | 85.58 |
| in_flight | 145111.735 | 200614 |
| nacks_per_s | 63.894 | 2060.2 |
| octopus_dispatcher_cores | 1.255 | 1.61 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 556222.98 | 807588 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 153776.551 | 216727 |
| octopus_mqtt_sent_total | 121237.306 | 183567 |
| octopus_publish_retries_total | 7474.898 | 17930 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.796 | 18 |
| pg_active_loads | 2.082 | 16 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 51.796 | 74 |
| pg_conn_idle | 215.816 | 230 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.245 | 3 |
| pg_conn_total | 267.612 | 268 |
| pg_db_size_mb | 1484.769 | 1642.6 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 14.729 | 30.95 |
| pg_replica_replay_lag_ms | 117.433 | 345.9 |
| pg_replicas | 2.408 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 12438.622 | 20451.4 |
| pg_tup_del_per_s | 19157.841 | 39489 |
| pg_tup_fetch_per_s | 518636.78 | 594430.7 |
| pg_tup_ins_per_s | 32983.888 | 61662.5 |
| pg_tup_upd_per_s | 13127.604 | 23261.8 |
| pg_wal_mb_per_s | 79.824 | 113.87 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6856.371 | 18050.5 |
| produced_total | 340559.51 | 477676 |
| publish_mb_per_s | 24.18 | 80.81 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,183 records are Completed |
| record_counts | pass | 82,183 records found, 82,183 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 288 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 514,549 events applied by workers, 514,549 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 125,794 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27116 ms, max 53391 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15252 seq 9: ready 236 ms before start, 236 ms of it with an idle slot in group 476
- [cross_source_blocking] source 7164 seq 10: ready 229 ms before start, 229 ms of it with an idle slot in group 243
- [cross_source_blocking] source 15118 seq 8: ready 247 ms before start, 247 ms of it with an idle slot in group 472
- [cross_source_blocking] source 15758 seq 17: ready 230 ms before start, 230 ms of it with an idle slot in group 493
- [cross_source_blocking] source 12706 seq 10: ready 257 ms before start, 257 ms of it with an idle slot in group 400
- [cross_source_blocking] source 12886 seq 7: ready 274 ms before start, 274 ms of it with an idle slot in group 408
- [cross_source_blocking] source 15130 seq 7: ready 176 ms before start, 176 ms of it with an idle slot in group 472
- [cross_source_blocking] source 15374 seq 4: ready 176 ms before start, 176 ms of it with an idle slot in group 480
- [cross_source_blocking] source 12894 seq 7: ready 274 ms before start, 274 ms of it with an idle slot in group 408
- [cross_source_blocking] source 9498 seq 4: ready 274 ms before start, 274 ms of it with an idle slot in group 328
- [cross_source_blocking] source 12350 seq 8: ready 248 ms before start, 248 ms of it with an idle slot in group 392
- [cross_source_blocking] source 2450 seq 5: ready 274 ms before start, 274 ms of it with an idle slot in group 72
- [cross_source_blocking] source 15628 seq 15: ready 235 ms before start, 235 ms of it with an idle slot in group 486
- [cross_source_blocking] source 8602 seq 5: ready 178 ms before start, 178 ms of it with an idle slot in group 296
- [cross_source_blocking] source 14862 seq 5: ready 281 ms before start, 281 ms of it with an idle slot in group 466
- [cross_source_blocking] source 12344 seq 4: ready 238 ms before start, 238 ms of it with an idle slot in group 392
- [cross_source_blocking] source 15352 seq 4: ready 179 ms before start, 179 ms of it with an idle slot in group 480
- [cross_source_blocking] source 314 seq 2: ready 281 ms before start, 281 ms of it with an idle slot in group 9
- [cross_source_blocking] source 15070 seq 5: ready 274 ms before start, 274 ms of it with an idle slot in group 470
- [cross_source_blocking] source 862 seq 9: ready 248 ms before start, 248 ms of it with an idle slot in group 24

## Error samples

- t-104#0 src=3117 seq=13 op=Update rec=13387413061633 ev=2: NpgsqlException: No suitable host was found.
- t-082#0 src=2651 seq=6 op=Update rec=11385958301698 ev=1: NpgsqlException: No suitable host was found.
- t-233#0 src=6861 seq=6 op=Create rec=29467770617859 ev=0: NpgsqlException: No suitable host was found.
- t-428#0 src=13390 seq=14 op=Create rec=57509612093444 ev=0: NpgsqlException: No suitable host was found.
- t-218#0 src=6387 seq=13 op=Create rec=27431956119555 ev=0: NpgsqlException: No suitable host was found.
- t-096#0 src=2957 seq=26 op=Create rec=12700218294277 ev=0: NpgsqlException: No suitable host was found.
- t-384#0 src=12025 seq=9 op=Get rec=51646981734401 ev=8: NpgsqlException: No suitable host was found.
- t-174#0 src=5024 seq=18 op=Update rec=21577915695108 ev=4: NpgsqlException: No suitable host was found.
- t-008#0 src=304 seq=28 op=Get rec=1305670057990 ev=1: NpgsqlException: No suitable host was found.
- t-453#0 src=14196 seq=25 op=Update rec=60971355734018 ev=6: NpgsqlException: No suitable host was found.
- t-467#0 src=14892 seq=13 op=Update rec=63960652972036 ev=3: NpgsqlException: No suitable host was found.
- t-498#0 src=15955 seq=26 op=Get rec=68526203207683 ev=4: NpgsqlException: No suitable host was found.
- t-232#0 src=6813 seq=20 op=Update rec=29261612187650 ev=5: NpgsqlException: No suitable host was found.
- t-421#0 src=13211 seq=35 op=Update rec=56740812947459 ev=7: NpgsqlException: No suitable host was found.
- t-435#0 src=13667 seq=22 op=Complete rec=58699318034434 ev=8: NpgsqlException: No suitable host was found.
- t-021#0 src=712 seq=8 op=Update rec=3058016714754 ev=5: NpgsqlException: No suitable host was found.
- t-485#0 src=15607 seq=16 op=Update rec=67031554588677 ev=3: NpgsqlException: No suitable host was found.
- t-401#0 src=12717 seq=15 op=Create rec=54619099103236 ev=0: NpgsqlException: No suitable host was found.
- t-398#0 src=12653 seq=23 op=Get rec=54344221196290 ev=3: NpgsqlException: No suitable host was found.
- t-133#0 src=3997 seq=12 op=Update rec=17166984282116 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1839 | 1839 | 0 | 66846.719 | 70254.591 | 72351.743 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1668 | 1668 | 3 | 49283.071 | 69730.303 | 70778.879 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1853 | 1853 | 0 | 48234.495 | 69206.015 | 72351.743 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4898 | 4898 | 0 | 41156.607 | 68681.727 | 74973.183 |
| t-129 | 19 | 1 | 90.638 | 29.84 | 623 | 0 | 1155 | 1155 | 0 | 51904.511 | 68157.439 | 69206.015 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1608 | 1608 | 0 | 65273.855 | 68157.439 | 69206.015 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4016 | 4016 | 0 | 62914.559 | 67633.151 | 69730.303 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7670 | 7670 | 8 | 61341.695 | 66846.719 | 74448.895 |
| t-458 | 132 | 1 | 15.687 | 34.029 | 2384 | 0 | 1520 | 1520 | 0 | 55574.527 | 64749.567 | 67108.863 |
| t-231 | 43 | 1 | 50.375 | 34.767 | 156 | 0 | 1367 | 1367 | 0 | 37224.447 | 64487.423 | 65798.143 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 3116 | 3116 | 0 | 55050.239 | 64225.279 | 66584.575 |
| t-049 | 53 | 1 | 36.985 | 33.024 | 350 | 0 | 1369 | 1369 | 4 | 52953.087 | 63700.991 | 69206.015 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3340 | 3340 | 2 | 45350.911 | 62390.271 | 68157.439 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1961 | 1961 | 0 | 56885.247 | 62390.271 | 69730.303 |
| t-002 | 17 | 1 | 82.711 | 23.551 | 377 | 0 | 919 | 919 | 4 | 41156.607 | 60817.407 | 62128.127 |
| t-037 | 21 | 1 | 73.917 | 25.941 | 272 | 0 | 1021 | 1021 | 3 | 40108.031 | 60817.407 | 64487.423 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1914 | 1914 | 0 | 41418.751 | 60293.119 | 63438.847 |
| t-044 | 113 | 1 | 15.276 | 28.064 | 243 | 0 | 1317 | 1317 | 0 | 44826.623 | 60030.975 | 60555.263 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1814 | 1814 | 0 | 42467.327 | 59768.831 | 63963.135 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 4980 | 4980 | 4 | 56885.247 | 59768.831 | 61079.551 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1814 | 1814 | 0 | 56098.815 | 59506.687 | 60293.119 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1698 | 1698 | 0 | 56623.103 | 59244.543 | 61341.695 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5695 | 5695 | 4 | 45350.911 | 59244.543 | 68681.727 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3755 | 3755 | 0 | 42467.327 | 59244.543 | 64749.567 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1860 | 1860 | 0 | 55836.671 | 58982.399 | 59244.543 |
