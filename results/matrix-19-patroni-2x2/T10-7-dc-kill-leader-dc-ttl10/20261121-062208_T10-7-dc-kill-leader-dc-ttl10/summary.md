# T10-7-dc-kill-leader-dc-ttl10 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.385s, closing 3.423s, drain 22.235s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 535324 | 535324 | 7379.447 | 4464.143 | 18254.4 | 0 | 1664 | 1664 | 300 | 2719 | 102590 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 365702 | 29548.951 | 34930.687 | 49577.983 | 57606.143 | 60817.407 | 65273.855 |
| e2e_from_send | 365702 | 24312.82 | 20463.615 | 48955.391 | 56786.943 | 59932.671 | 64454.655 |
| producer_lag | 366102 | 5230.975 | 561.663 | 20758.527 | 22380.543 | 22888.447 | 23576.575 |
| publish_call | 535324 | 200.786 | 111.999 | 228.991 | 505.343 | 20725.759 | 20938.751 |
| queue_wait | 365702 | 24190.44 | 20430.847 | 48922.623 | 56754.175 | 59899.903 | 64421.887 |
| broker_dwell | 365702 | 24021.001 | 20070.399 | 48791.551 | 56655.871 | 59801.599 | 64356.351 |
| receive_call | 473593 | 29.188 | 8.719 | 67.007 | 351.999 | 478.975 | 538.111 |
| ctx_load | 365702 | 10.943 | 7.271 | 23.743 | 59.967 | 96.511 | 381.439 |
| process | 365702 | 10.783 | 0.002 | 0.005 | 0.069 | 2623.487 | 3014.655 |
| ctx_commit | 365702 | 100.545 | 58.623 | 242.431 | 459.263 | 579.071 | 699.903 |
| ack | 365679 | 0.01 | 0.004 | 0.007 | 0.051 | 0.79 | 70.847 |
| service | 365702 | 122.389 | 72.191 | 262.399 | 521.471 | 2725.887 | 3504.127 |
| e2e_large | 200 | 7981.391 | 3887.103 | 24264.703 | 27574.271 | 28459.007 | 28459.007 |
| ready_wait | 365702 | 1260.708 | 37.471 | 1505.279 | 38010.879 | 45121.535 | 54525.951 |
| blocked | 365702 | 801.717 | 25.375 | 571.391 | 26722.303 | 33177.599 | 36077.567 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4464.143 | 11915.5 |
| applied_total | 197218.98 | 290699 |
| bench_cpu_cores | 2.165 | 4.125 |
| bench_cpu_pct | 9.022 | 17.19 |
| bench_gc_gen0 | 1.265 | 3 |
| bench_gc_gen1 | 0.694 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1165.724 | 1542.7 |
| bench_mem_mb | 1843.227 | 1991.7 |
| bench_threadpool_queue | 34.857 | 529 |
| bench_threadpool_threads | 43.184 | 44 |
| broker_backlog | 163937.306 | 213836 |
| ctr_eb_dc1a_cpu_pct | 103.306 | 1078.36 |
| ctr_eb_dc1a_mem_mb | 3674.631 | 5038.3 |
| ctr_eb_dc1b_cpu_pct | 27.481 | 57.47 |
| ctr_eb_dc1b_mem_mb | 4537.647 | 4612.9 |
| ctr_eb_dc2a_cpu_pct | 48.527 | 56.95 |
| ctr_eb_dc2a_mem_mb | 2320.686 | 2363.2 |
| ctr_eb_dc2b_cpu_pct | 736.155 | 812.79 |
| ctr_eb_dc2b_mem_mb | 7104.627 | 7540.7 |
| ctr_eb_etcd_dc1_cpu_pct | 1.722 | 2.95 |
| ctr_eb_etcd_dc1_mem_mb | 31.429 | 33.4 |
| ctr_eb_etcd_dc2_cpu_pct | 2.123 | 2.23 |
| ctr_eb_etcd_dc2_mem_mb | 38.859 | 40 |
| ctr_eb_etcd_w_cpu_pct | 2.313 | 3.71 |
| ctr_eb_etcd_w_mem_mb | 55.745 | 56.6 |
| ctr_eb_haproxy_cpu_pct | 58.05 | 118.92 |
| ctr_eb_haproxy_mem_mb | 35.159 | 37.7 |
| ctr_eb_mosquitto_cpu_pct | 26.797 | 52.95 |
| ctr_eb_mosquitto_mem_mb | 7.451 | 39.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.022 | 0.06 |
| ctr_eb_pgbouncer_2_mem_mb | 7.316 | 7.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 7.4 | 7.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.08 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 7.7 | 7.7 |
| ctr_eb_postgres_cpu_pct | 0.853 | 2.06 |
| ctr_eb_postgres_mem_mb | 1635.976 | 1638.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 33.992 | 1477.6 |
| host_cpu_pct | 44.154 | 82.84 |
| in_flight | 168180.286 | 211070 |
| nacks_per_s | 33.992 | 1477.6 |
| octopus_dispatcher_cores | 0.875 | 1.51 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 578749.592 | 851658 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 162285.959 | 231325 |
| octopus_mqtt_sent_total | 126512.612 | 198935 |
| octopus_publish_retries_total | 9222.531 | 23803 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.306 | 21 |
| pg_active_loads | 11.347 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 56.082 | 76 |
| pg_conn_idle | 211.98 | 228 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.347 | 4 |
| pg_conn_total | 268.061 | 269 |
| pg_db_size_mb | 1520.059 | 1703.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 11.94 | 83.15 |
| pg_replica_replay_lag_ms | 153.761 | 885.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16417.367 | 20992.6 |
| pg_tup_del_per_s | 25579.922 | 34330.2 |
| pg_tup_fetch_per_s | 538376.053 | 682065.3 |
| pg_tup_ins_per_s | 48708.739 | 58124.7 |
| pg_tup_upd_per_s | 18022.076 | 23393.3 |
| pg_wal_mb_per_s | 86.127 | 110.86 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7379.447 | 16054.8 |
| produced_total | 365399.265 | 499184 |
| publish_mb_per_s | 26.098 | 75.89 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 84,686 records are Completed |
| record_counts | pass | 84,686 records found, 84,686 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 300 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 535,324 events applied by workers, 535,324 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 102,590 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26722 ms, max 36050 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13212 seq 13: ready 211 ms before start, 211 ms of it with an idle slot in group 421
- [cross_source_blocking] source 873 seq 16: ready 241 ms before start, 241 ms of it with an idle slot in group 26
- [cross_source_blocking] source 13204 seq 13: ready 240 ms before start, 240 ms of it with an idle slot in group 420
- [cross_source_blocking] source 11300 seq 4: ready 239 ms before start, 239 ms of it with an idle slot in group 368
- [cross_source_blocking] source 5081 seq 6: ready 271 ms before start, 271 ms of it with an idle slot in group 177
- [cross_source_blocking] source 9516 seq 8: ready 213 ms before start, 213 ms of it with an idle slot in group 330
- [cross_source_blocking] source 9685 seq 10: ready 210 ms before start, 210 ms of it with an idle slot in group 336
- [cross_source_blocking] source 4389 seq 11: ready 194 ms before start, 194 ms of it with an idle slot in group 150
- [cross_source_blocking] source 5673 seq 17: ready 192 ms before start, 192 ms of it with an idle slot in group 195
- [cross_source_blocking] source 12909 seq 3: ready 454 ms before start, 454 ms of it with an idle slot in group 408
- [cross_source_blocking] source 4013 seq 3: ready 449 ms before start, 449 ms of it with an idle slot in group 134
- [cross_source_blocking] source 7260 seq 6: ready 384 ms before start, 384 ms of it with an idle slot in group 248
- [cross_source_blocking] source 13173 seq 22: ready 449 ms before start, 449 ms of it with an idle slot in group 418
- [cross_source_blocking] source 13989 seq 5: ready 383 ms before start, 383 ms of it with an idle slot in group 445
- [cross_source_blocking] source 7112 seq 2: ready 467 ms before start, 467 ms of it with an idle slot in group 241
- [cross_source_blocking] source 1020 seq 12: ready 396 ms before start, 396 ms of it with an idle slot in group 31
- [cross_source_blocking] source 7404 seq 12: ready 384 ms before start, 384 ms of it with an idle slot in group 256
- [cross_source_blocking] source 4249 seq 7: ready 388 ms before start, 388 ms of it with an idle slot in group 143
- [cross_source_blocking] source 15296 seq 4: ready 387 ms before start, 387 ms of it with an idle slot in group 478
- [cross_source_blocking] source 10924 seq 14: ready 387 ms before start, 387 ms of it with an idle slot in group 356

## Error samples

- t-374#0 src=11525 seq=64 op=Complete rec=49499498086405 ev=2: NpgsqlException: Exception while reading from stream
- t-397#0 src=12610 seq=3 op=Update rec=54159537602562 ev=1: NpgsqlException: Exception while reading from stream
- t-367#0 src=11288 seq=29 op=Update rec=48481590837250 ev=6: NpgsqlException: Exception while reading from stream
- t-344#4 src=10136 seq=20 op=Create rec=43533788512262 ev=0: NpgsqlException: Exception while reading from stream
- t-060#0 src=2019 seq=33 op=Update rec=8671538970628 ev=17: NpgsqlException: Exception while reading from stream
- t-378#1 src=11617 seq=22 op=Update rec=49894635077636 ev=1: NpgsqlException: Exception while reading from stream
- t-287#0 src=8458 seq=30 op=Update rec=36326833389571 ev=7: NpgsqlException: Exception while reading from stream
- t-171#0 src=4985 seq=36 op=Update rec=21410411970563 ev=12: NpgsqlException: Exception while reading from stream
- t-369#0 src=11339 seq=44 op=Complete rec=48700634169350 ev=2: NpgsqlException: Exception while reading from stream
- t-468#1 src=14949 seq=43 op=Get rec=64205466107913 ev=1: NpgsqlException: Exception while reading from stream
- t-164#0 src=4749 seq=59 op=Get rec=20396799688711 ev=2: NpgsqlException: Exception while reading from stream
- t-125#0 src=3672 seq=32 op=Update rec=15771119910915 ev=2: NpgsqlException: Exception while reading from stream
- t-361#0 src=11056 seq=12 op=Update rec=47485158424578 ev=4: NpgsqlException: Exception while reading from stream
- t-257#0 src=7417 seq=43 op=Update rec=31855772434438 ev=4: NpgsqlException: Exception while reading from stream
- t-242#0 src=7133 seq=21 op=Update rec=30636001722371 ev=3: NpgsqlException: Exception while reading from stream
- t-065#0 src=2219 seq=59 op=Update rec=9530532429833 ev=3: NpgsqlException: Exception while reading from stream
- t-369#0 src=11331 seq=43 op=Update rec=48666274430979 ev=9: NpgsqlException: Exception while reading from stream
- t-380#0 src=11739 seq=24 op=Update rec=50418621087750 ev=1: NpgsqlException: Exception while reading from stream
- t-314#0 src=9096 seq=59 op=Update rec=39067022524422 ev=2: NpgsqlException: Exception while reading from stream
- t-461#0 src=14555 seq=10 op=Update rec=62513248993282 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3893 | 3893 | 0 | 42467.327 | 62128.127 | 65273.855 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1997 | 1997 | 0 | 44040.191 | 61865.983 | 62390.271 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4188 | 4188 | 0 | 44302.335 | 61865.983 | 64749.567 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4183 | 4183 | 0 | 45088.767 | 61603.839 | 63963.135 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3945 | 3945 | 0 | 45350.911 | 61341.695 | 63438.847 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1880 | 1880 | 0 | 44826.623 | 61079.551 | 62390.271 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3900 | 3900 | 0 | 44040.191 | 60817.407 | 62128.127 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1940 | 1940 | 0 | 43253.759 | 60555.263 | 61865.983 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1884 | 1884 | 0 | 44302.335 | 60555.263 | 61341.695 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1940 | 1940 | 0 | 45613.055 | 60555.263 | 61865.983 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2026 | 2026 | 0 | 45350.911 | 60555.263 | 61079.551 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1972 | 1972 | 0 | 43778.047 | 60555.263 | 62390.271 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1920 | 1920 | 0 | 43253.759 | 60293.119 | 61341.695 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1910 | 1910 | 0 | 43778.047 | 60293.119 | 60817.407 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5924 | 5924 | 0 | 44040.191 | 60030.975 | 61603.839 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1763 | 1763 | 0 | 43778.047 | 59768.831 | 60817.407 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5706 | 5706 | 0 | 43778.047 | 59768.831 | 61079.551 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1939 | 1939 | 0 | 42205.183 | 59506.687 | 60817.407 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1795 | 1795 | 0 | 43253.759 | 59244.543 | 60293.119 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3721 | 3721 | 0 | 42991.615 | 59244.543 | 60817.407 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1933 | 1933 | 0 | 44040.191 | 58982.399 | 60293.119 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5773 | 5773 | 0 | 43515.903 | 58982.399 | 59768.831 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1880 | 1880 | 0 | 43515.903 | 58982.399 | 60293.119 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5431 | 5431 | 0 | 42729.471 | 58720.255 | 60555.263 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1838 | 1838 | 0 | 43778.047 | 58458.111 | 59244.543 |
