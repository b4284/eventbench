# V2c-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.664s, closing 2.388s, drain 21.677s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 471123 | 471123 | 6247.731 | 3047.598 | 22181.4 | 0 | 3062 | 3062 | 276 | 2381 | 70233 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 302620 | 33615.403 | 40108.031 | 51773.439 | 58294.271 | 61734.911 | 65634.303 |
| e2e_from_send | 302620 | 27597.195 | 36339.711 | 50364.415 | 56983.551 | 60588.031 | 64159.743 |
| producer_lag | 302704 | 6016.982 | 1635.327 | 26591.231 | 28082.175 | 28491.775 | 28835.839 |
| publish_call | 471123 | 237.797 | 129.087 | 234.495 | 806.399 | 24477.695 | 24592.383 |
| queue_wait | 302620 | 27481.865 | 36274.175 | 50331.647 | 56950.783 | 60522.495 | 64159.743 |
| broker_dwell | 302620 | 27318.766 | 36110.335 | 50200.575 | 56819.711 | 60391.423 | 64126.975 |
| receive_call | 430189 | 24.812 | 5.999 | 55.039 | 343.551 | 479.999 | 542.207 |
| ctx_load | 302620 | 11.502 | 3.101 | 10.447 | 32.047 | 707.071 | 15040.511 |
| process | 302620 | 10.815 | 0.002 | 0.004 | 0.046 | 2633.727 | 3002.367 |
| ctx_commit | 302620 | 92.979 | 28.767 | 292.863 | 519.167 | 657.919 | 15015.935 |
| ack | 301914 | 0.006 | 0.003 | 0.005 | 0.026 | 0.404 | 25.183 |
| service | 302620 | 115.331 | 34.207 | 310.015 | 573.951 | 2873.343 | 16826.367 |
| e2e_large | 175 | 9166.637 | 4464.639 | 29474.815 | 31326.207 | 33505.279 | 33505.279 |
| ready_wait | 302620 | 1422.714 | 19.711 | 1480.703 | 38895.615 | 44531.711 | 54657.023 |
| blocked | 302620 | 1043.249 | 14.439 | 459.519 | 31178.751 | 34570.239 | 36175.871 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3047.598 | 10238.5 |
| applied_total | 166177.469 | 211057 |
| bench_cpu_cores | 1.302 | 3.354 |
| bench_cpu_pct | 5.423 | 13.97 |
| bench_gc_gen0 | 1.245 | 5 |
| bench_gc_gen1 | 1.061 | 4 |
| bench_gc_gen2 | 0.347 | 2 |
| bench_gc_heap_mb | 972.751 | 1196.7 |
| bench_mem_mb | 1480.324 | 1604.3 |
| bench_threadpool_queue | 36.429 | 501 |
| bench_threadpool_threads | 44.082 | 46 |
| broker_backlog | 160786.551 | 185392 |
| ctr_eb_dc1a_cpu_pct | 56.122 | 248.02 |
| ctr_eb_dc1a_mem_mb | 5246.937 | 5961.4 |
| ctr_eb_dc1b_cpu_pct | 71.59 | 584.76 |
| ctr_eb_dc1b_mem_mb | 7297.551 | 8289.3 |
| ctr_eb_dc2a_cpu_pct | 714.811 | 785.34 |
| ctr_eb_dc2a_mem_mb | 10758.79 | 11119.8 |
| ctr_eb_dc2b_cpu_pct | 47.475 | 53.02 |
| ctr_eb_dc2b_mem_mb | 2944.119 | 3028.9 |
| ctr_eb_etcd_dc1_cpu_pct | 1.303 | 1.78 |
| ctr_eb_etcd_dc1_mem_mb | 24.363 | 27.2 |
| ctr_eb_etcd_dc2_cpu_pct | 1.365 | 1.72 |
| ctr_eb_etcd_dc2_mem_mb | 32.152 | 32.6 |
| ctr_eb_etcd_w_cpu_pct | 1.739 | 2.42 |
| ctr_eb_etcd_w_mem_mb | 50.982 | 52.1 |
| ctr_eb_mosquitto_cpu_pct | 20.323 | 43.59 |
| ctr_eb_mosquitto_mem_mb | 6.659 | 14.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.5 | 9.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.11 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.799 | 2.31 |
| ctr_eb_postgres_mem_mb | 2056.988 | 2075.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 62.535 | 1975.8 |
| host_cpu_pct | 34.592 | 85.11 |
| in_flight | 169626.755 | 224176 |
| nacks_per_s | 62.535 | 1975.8 |
| octopus_dispatcher_cores | 1.026 | 1.5 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 520007.469 | 631126 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 147727.122 | 177390 |
| octopus_mqtt_sent_total | 96021.184 | 122184 |
| octopus_publish_retries_total | 6937.837 | 16679 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17 | 18 |
| pg_active_loads | 0.122 | 2 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 46.061 | 53 |
| pg_conn_idle | 220.327 | 225 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 266.388 | 267 |
| pg_db_size_mb | 1459.08 | 1636 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 11.105 | 21.28 |
| pg_replica_replay_lag_ms | 42.461 | 188 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17008.131 | 20283.5 |
| pg_tup_del_per_s | 23687.72 | 35482 |
| pg_tup_fetch_per_s | 606235.618 | 718051.7 |
| pg_tup_ins_per_s | 47060.204 | 61713.8 |
| pg_tup_upd_per_s | 18098.331 | 22384.7 |
| pg_wal_mb_per_s | 81.517 | 109.15 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6247.731 | 19024.1 |
| produced_total | 335804.224 | 435233 |
| publish_mb_per_s | 23.299 | 68.72 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 76,931 records are Completed |
| record_counts | pass | 76,931 records found, 76,931 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 276 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 471,123 events applied by workers, 471,123 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 70,233 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 31179 ms, max 36151 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14862 seq 5: ready 198 ms before start, 198 ms of it with an idle slot in group 466
- [cross_source_blocking] source 13605 seq 7: ready 196 ms before start, 196 ms of it with an idle slot in group 431
- [cross_source_blocking] source 14894 seq 6: ready 265 ms before start, 265 ms of it with an idle slot in group 467
- [cross_source_blocking] source 9468 seq 12: ready 164 ms before start, 164 ms of it with an idle slot in group 327
- [cross_source_blocking] source 13212 seq 13: ready 152 ms before start, 152 ms of it with an idle slot in group 421
- [cross_source_blocking] source 5550 seq 16: ready 157 ms before start, 157 ms of it with an idle slot in group 190
- [cross_source_blocking] source 4389 seq 11: ready 216 ms before start, 216 ms of it with an idle slot in group 150
- [cross_source_blocking] source 7909 seq 16: ready 217 ms before start, 217 ms of it with an idle slot in group 266
- [cross_source_blocking] source 5573 seq 4: ready 217 ms before start, 217 ms of it with an idle slot in group 193
- [cross_source_blocking] source 13989 seq 5: ready 219 ms before start, 219 ms of it with an idle slot in group 445
- [cross_source_blocking] source 13669 seq 11: ready 219 ms before start, 219 ms of it with an idle slot in group 435
- [cross_source_blocking] source 15391 seq 12: ready 125 ms before start, 125 ms of it with an idle slot in group 482
- [cross_source_blocking] source 7260 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 248
- [cross_source_blocking] source 3326 seq 4: ready 117 ms before start, 117 ms of it with an idle slot in group 113
- [cross_source_blocking] source 862 seq 9: ready 117 ms before start, 117 ms of it with an idle slot in group 24
- [cross_source_blocking] source 12022 seq 8: ready 131 ms before start, 131 ms of it with an idle slot in group 384
- [cross_source_blocking] source 1020 seq 12: ready 126 ms before start, 126 ms of it with an idle slot in group 31
- [cross_source_blocking] source 4022 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 134
- [cross_source_blocking] source 590 seq 4: ready 210 ms before start, 133 ms of it with an idle slot in group 17
- [cross_source_blocking] source 12886 seq 7: ready 127 ms before start, 127 ms of it with an idle slot in group 408

## Error samples

- t-121#1 src=3532 seq=22 op=Create rec=15169824489477 ev=0: NpgsqlException: No suitable host was found.
- t-329#0 src=9508 seq=39 op=Create rec=40836549050376 ev=0: NpgsqlException: No suitable host was found.
- t-443#0 src=13971 seq=11 op=Get rec=60004988092417 ev=7: NpgsqlException: No suitable host was found.
- t-480#0 src=15356 seq=11 op=Create rec=65953517797378 ev=0: NpgsqlException: No suitable host was found.
- t-065#4 src=2243 seq=45 op=Get rec=9633611644936 ev=3: NpgsqlException: No suitable host was found.
- t-409#1 src=12912 seq=20 op=Get rec=55456617725954 ev=9: NpgsqlException: No suitable host was found.
- t-156#0 src=4496 seq=14 op=Update rec=19310172962818 ev=4: NpgsqlException: No suitable host was found.
- t-132#0 src=3980 seq=48 op=Complete rec=17093969838084 ev=9: NpgsqlException: No suitable host was found.
- t-123#0 src=3645 seq=25 op=Update rec=15655155793922 ev=15: NpgsqlException: No suitable host was found.
- t-407#0 src=12872 seq=16 op=Update rec=55284819034116 ev=1: NpgsqlException: No suitable host was found.
- t-469#1 src=14962 seq=16 op=Update rec=64261300682753 ev=7: NpgsqlException: No suitable host was found.
- t-017#0 src=599 seq=13 op=Update rec=2572685410307 ev=2: NpgsqlException: No suitable host was found.
- t-005#0 src=247 seq=11 op=Update rec=1060856922113 ev=6: NpgsqlException: No suitable host was found.
- t-437#0 src=13733 seq=30 op=Update rec=58982785875972 ev=7: NpgsqlException: No suitable host was found.
- t-184#0 src=5403 seq=22 op=Get rec=23205708300294 ev=2: NpgsqlException: No suitable host was found.
- t-389#0 src=12206 seq=7 op=Update rec=52424370814978 ev=2: NpgsqlException: No suitable host was found.
- t-352#0 src=10729 seq=37 op=Update rec=46080704118790 ev=1: NpgsqlException: No suitable host was found.
- t-271#2 src=8051 seq=28 op=Update rec=34578781700103 ev=1: NpgsqlException: No suitable host was found.
- t-243#0 src=7156 seq=27 op=Create rec=30734785970182 ev=0: NpgsqlException: No suitable host was found.
- t-231#0 src=6777 seq=12 op=Update rec=29106993364993 ev=8: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1698 | 1698 | 0 | 47972.351 | 63176.703 | 65798.143 |
| t-268 | 41 | 1 | 36.983 | 24.264 | 1430 | 0 | 965 | 965 | 0 | 43778.047 | 62652.415 | 63176.703 |
| t-184 | 20 | 1 | 79.554 | 25.849 | 161 | 0 | 925 | 925 | 0 | 42205.183 | 62390.271 | 64487.423 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1756 | 1756 | 0 | 47448.063 | 62128.127 | 62914.559 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7825 | 7825 | 0 | 46399.487 | 61865.983 | 64749.567 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 2853 | 2853 | 0 | 44564.479 | 61341.695 | 63438.847 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 2615 | 2615 | 0 | 42205.183 | 61079.551 | 63176.703 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1692 | 1692 | 0 | 47448.063 | 61079.551 | 62652.415 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4752 | 4752 | 0 | 46661.631 | 61079.551 | 62914.559 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1665 | 1665 | 0 | 43515.903 | 61079.551 | 63176.703 |
| t-186 | 34 | 2 | 103.912 | 58.611 | 1259 | 0 | 2055 | 2055 | 0 | 40894.463 | 60817.407 | 63700.991 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3507 | 3507 | 0 | 47448.063 | 60817.407 | 62390.271 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3438 | 3438 | 0 | 46661.631 | 60817.407 | 63700.991 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1677 | 1677 | 0 | 45875.199 | 60817.407 | 62652.415 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3672 | 3672 | 0 | 46661.631 | 60817.407 | 61603.839 |
| t-293 | 46 | 1 | 42.231 | 32.409 | 2741 | 0 | 1220 | 1220 | 0 | 45613.055 | 60555.263 | 62652.415 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5040 | 5040 | 0 | 46923.775 | 60555.263 | 63700.991 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1667 | 1667 | 0 | 45875.199 | 60293.119 | 61603.839 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 2585 | 2585 | 0 | 43778.047 | 60293.119 | 61603.839 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3677 | 3677 | 0 | 46399.487 | 60293.119 | 63438.847 |
| t-274 | 29 | 1 | 56.437 | 27.244 | 1181 | 0 | 1005 | 1005 | 0 | 40370.175 | 60030.975 | 63438.847 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1020 | 1020 | 0 | 42205.183 | 60030.975 | 62390.271 |
| t-340 | 184 | 2 | 24.502 | 75.472 | 141 | 0 | 2927 | 2927 | 0 | 46661.631 | 60030.975 | 61865.983 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1674 | 1674 | 0 | 46137.343 | 59768.831 | 60555.263 |
| t-216 | 19 | 1 | 60.988 | 19.724 | 3889 | 0 | 723 | 723 | 0 | 31850.495 | 59768.831 | 61865.983 |
