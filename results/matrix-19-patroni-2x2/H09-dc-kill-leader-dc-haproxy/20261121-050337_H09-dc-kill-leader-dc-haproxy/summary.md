# H09-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.21s, closing 2.59s, drain 24.587s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 553633 | 553633 | 7752.902 | 4512.784 | 19281.2 | 0 | 1940 | 1940 | 307 | 2824 | 86817 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 384197 | 29874.284 | 33882.111 | 48988.159 | 57212.927 | 60424.191 | 64847.871 |
| e2e_from_send | 384197 | 23789.9 | 20758.527 | 48037.887 | 56131.583 | 59441.151 | 63897.599 |
| producer_lag | 384197 | 6084.381 | 1104.895 | 20545.535 | 21413.887 | 21757.951 | 22118.399 |
| publish_call | 553633 | 195.395 | 115.775 | 223.359 | 378.879 | 20201.471 | 20381.695 |
| queue_wait | 384197 | 23675.361 | 20692.991 | 48005.119 | 56098.815 | 59375.615 | 63602.687 |
| broker_dwell | 384197 | 23464.764 | 20316.159 | 47874.047 | 56000.511 | 59277.311 | 63438.847 |
| receive_call | 499749 | 25.893 | 7.691 | 60.063 | 328.703 | 472.831 | 547.327 |
| ctx_load | 384197 | 9.838 | 6.307 | 21.327 | 59.775 | 96.959 | 187.135 |
| process | 384197 | 10.8 | 0.002 | 0.004 | 0.055 | 2625.535 | 3002.367 |
| ctx_commit | 384197 | 93.824 | 57.247 | 237.695 | 450.559 | 593.919 | 644.607 |
| ack | 384167 | 0.009 | 0.004 | 0.007 | 0.042 | 0.645 | 50.431 |
| service | 384197 | 114.549 | 67.711 | 254.591 | 525.823 | 2729.983 | 3414.015 |
| e2e_large | 207 | 8979.018 | 4206.591 | 23789.567 | 26820.607 | 29917.183 | 29917.183 |
| ready_wait | 384197 | 1225.196 | 29.839 | 1455.103 | 37093.375 | 44662.783 | 54067.199 |
| blocked | 384197 | 753.408 | 20.431 | 536.575 | 23986.175 | 29458.431 | 32129.023 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4512.784 | 13338 |
| applied_total | 201980.184 | 301317 |
| bench_cpu_cores | 2.008 | 3.759 |
| bench_cpu_pct | 8.365 | 15.66 |
| bench_gc_gen0 | 1.327 | 3 |
| bench_gc_gen1 | 0.898 | 3 |
| bench_gc_gen2 | 0.388 | 1 |
| bench_gc_heap_mb | 1187.963 | 1486 |
| bench_mem_mb | 1830.788 | 1960.6 |
| bench_threadpool_queue | 22.429 | 343 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 156446.02 | 208700 |
| ctr_eb_dc1a_cpu_pct | 121.203 | 1104.3 |
| ctr_eb_dc1a_mem_mb | 3336.018 | 4488 |
| ctr_eb_dc1b_cpu_pct | 31.522 | 78.67 |
| ctr_eb_dc1b_mem_mb | 2874.706 | 2954.5 |
| ctr_eb_dc2a_cpu_pct | 51.031 | 57.76 |
| ctr_eb_dc2a_mem_mb | 5799.236 | 5831.7 |
| ctr_eb_dc2b_cpu_pct | 745.59 | 816.62 |
| ctr_eb_dc2b_mem_mb | 9065.659 | 9574.9 |
| ctr_eb_etcd_dc1_cpu_pct | 1.427 | 2.66 |
| ctr_eb_etcd_dc1_mem_mb | 31.853 | 33.1 |
| ctr_eb_etcd_dc2_cpu_pct | 1.311 | 1.44 |
| ctr_eb_etcd_dc2_mem_mb | 38.336 | 38.7 |
| ctr_eb_etcd_w_cpu_pct | 1.721 | 3.88 |
| ctr_eb_etcd_w_mem_mb | 66.355 | 67.9 |
| ctr_eb_haproxy_cpu_pct | 59.411 | 127.35 |
| ctr_eb_haproxy_mem_mb | 35.106 | 37 |
| ctr_eb_mosquitto_cpu_pct | 25.251 | 45.5 |
| ctr_eb_mosquitto_mem_mb | 6.527 | 12.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 8.8 | 8.8 |
| ctr_eb_postgres_cpu_pct | 0.894 | 2.26 |
| ctr_eb_postgres_mem_mb | 1780.255 | 1781.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 39.606 | 1926.5 |
| host_cpu_pct | 43.143 | 81.9 |
| in_flight | 162495.796 | 215161 |
| nacks_per_s | 39.606 | 1926.5 |
| octopus_dispatcher_cores | 0.836 | 1.49 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 584663.245 | 850526 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 156477.041 | 223550 |
| octopus_mqtt_sent_total | 130442.204 | 198214 |
| octopus_publish_retries_total | 9067.837 | 21852 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.857 | 19 |
| pg_active_loads | 1.898 | 15 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 60.571 | 73 |
| pg_conn_idle | 208.327 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.041 | 1 |
| pg_conn_total | 268.898 | 270 |
| pg_db_size_mb | 1451.398 | 1611 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 2.408 | 13.65 |
| pg_replica_replay_lag_ms | 51.345 | 136.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18045.912 | 19036 |
| pg_tup_del_per_s | 26422.739 | 31313.2 |
| pg_tup_fetch_per_s | 543567.031 | 640846.4 |
| pg_tup_ins_per_s | 50494.976 | 54831.8 |
| pg_tup_upd_per_s | 20823.284 | 22241.6 |
| pg_wal_mb_per_s | 88.656 | 100.18 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7752.902 | 16979.1 |
| produced_total | 364475.98 | 516478 |
| publish_mb_per_s | 27.681 | 67.5 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 86,907 records are Completed |
| record_counts | pass | 86,907 records found, 86,907 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 307 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 553,633 events applied by workers, 553,633 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 86,817 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 23986 ms, max 32115 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10926 seq 16: ready 129 ms before start, 129 ms of it with an idle slot in group 356
- [cross_source_blocking] source 5582 seq 3: ready 129 ms before start, 129 ms of it with an idle slot in group 193
- [cross_source_blocking] source 4526 seq 6: ready 131 ms before start, 131 ms of it with an idle slot in group 157
- [cross_source_blocking] source 14062 seq 7: ready 130 ms before start, 130 ms of it with an idle slot in group 447
- [cross_source_blocking] source 3330 seq 6: ready 176 ms before start, 176 ms of it with an idle slot in group 113
- [cross_source_blocking] source 6357 seq 4: ready 316 ms before start, 316 ms of it with an idle slot in group 217
- [cross_source_blocking] source 6050 seq 3: ready 176 ms before start, 176 ms of it with an idle slot in group 207
- [cross_source_blocking] source 7909 seq 16: ready 177 ms before start, 177 ms of it with an idle slot in group 266
- [cross_source_blocking] source 5174 seq 6: ready 226 ms before start, 226 ms of it with an idle slot in group 179
- [cross_source_blocking] source 1989 seq 7: ready 316 ms before start, 316 ms of it with an idle slot in group 58
- [cross_source_blocking] source 4709 seq 3: ready 328 ms before start, 328 ms of it with an idle slot in group 162
- [cross_source_blocking] source 12022 seq 8: ready 257 ms before start, 257 ms of it with an idle slot in group 384
- [cross_source_blocking] source 1141 seq 10: ready 315 ms before start, 315 ms of it with an idle slot in group 38
- [cross_source_blocking] source 9486 seq 8: ready 177 ms before start, 177 ms of it with an idle slot in group 328
- [cross_source_blocking] source 6710 seq 11: ready 261 ms before start, 261 ms of it with an idle slot in group 229
- [cross_source_blocking] source 9468 seq 12: ready 221 ms before start, 221 ms of it with an idle slot in group 327
- [cross_source_blocking] source 11212 seq 8: ready 223 ms before start, 223 ms of it with an idle slot in group 365
- [cross_source_blocking] source 15391 seq 12: ready 243 ms before start, 243 ms of it with an idle slot in group 482
- [cross_source_blocking] source 12779 seq 4: ready 243 ms before start, 243 ms of it with an idle slot in group 403
- [cross_source_blocking] source 1020 seq 12: ready 136 ms before start, 136 ms of it with an idle slot in group 31

## Error samples

- t-137#0 src=4180 seq=22 op=Update rec=17952963297286 ev=1: NpgsqlException: Exception while reading from stream
- t-112#0 src=3319 seq=28 op=Get rec=14254996455429 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3485 seq=3 op=Update rec=14967961026561 ev=2: NpgsqlException: Exception while reading from stream
- t-059#0 src=2015 seq=27 op=Get rec=8654359101445 ev=1: NpgsqlException: Exception while reading from stream
- t-351#0 src=10726 seq=10 op=Update rec=46067819216897 ev=8: NpgsqlException: Exception while reading from stream
- t-478#0 src=15290 seq=14 op=Update rec=65670049955843 ev=3: NpgsqlException: Exception while reading from stream
- t-191#0 src=5562 seq=21 op=Update rec=23888608100357 ev=1: NpgsqlException: Exception while reading from stream
- t-232#0 src=6815 seq=38 op=Update rec=29270202122249 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: Exception while reading from stream
- t-274#0 src=8125 seq=18 op=Update rec=34896609280003 ev=2: NpgsqlException: Exception while reading from stream
- t-180#0 src=5207 seq=24 op=Complete rec=22363894710275 ev=6: NpgsqlException: Exception while reading from stream
- t-319L#1 src=9258 seq=3 op=Update rec=39762807226369 ev=2: NpgsqlException: Exception while reading from stream
- t-135#0 src=4025 seq=9 op=Get rec=17287243366403 ev=2: NpgsqlException: Exception while reading from stream
- t-184#0 src=5404 seq=23 op=Complete rec=23210003267586 ev=4: NpgsqlException: Exception while reading from stream
- t-438#1 src=13846 seq=18 op=Create rec=59468117180420 ev=0: NpgsqlException: Exception while reading from stream
- t-086#0 src=2790 seq=32 op=Get rec=11982958755845 ev=1: NpgsqlException: Exception while reading from stream
- t-170#0 src=4917 seq=7 op=Complete rec=21118354194434 ev=1: NpgsqlException: Exception while reading from stream
- t-453#0 src=14196 seq=28 op=Get rec=60971355734018 ev=8: NpgsqlException: Exception while reading from stream
- t-399#0 src=12685 seq=18 op=Update rec=54481660149762 ev=10: NpgsqlException: Exception while reading from stream
- t-406#0 src=12820 seq=16 op=Complete rec=55061480734725 ev=3: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1942 | 1942 | 0 | 43515.903 | 61865.983 | 64225.279 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4328 | 4328 | 0 | 43253.759 | 61603.839 | 64487.423 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4312 | 4312 | 0 | 42991.615 | 61603.839 | 62652.415 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2060 | 2060 | 0 | 42205.183 | 61079.551 | 62652.415 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4064 | 4064 | 0 | 43515.903 | 60555.263 | 61865.983 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6142 | 6142 | 0 | 42729.471 | 60555.263 | 62390.271 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4032 | 4032 | 0 | 42991.615 | 60555.263 | 65011.711 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1994 | 1994 | 0 | 41680.895 | 60293.119 | 61341.695 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2046 | 2046 | 0 | 41680.895 | 60030.975 | 60817.407 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1853 | 1853 | 0 | 41943.039 | 59768.831 | 60555.263 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1994 | 1994 | 0 | 43515.903 | 59768.831 | 60555.263 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4038 | 4038 | 0 | 41943.039 | 59768.831 | 62914.559 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3850 | 3850 | 0 | 41418.751 | 59768.831 | 61079.551 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5993 | 5993 | 0 | 41943.039 | 59244.543 | 60030.975 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1782 | 1782 | 0 | 40370.175 | 59244.543 | 60293.119 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1970 | 1970 | 0 | 41680.895 | 59244.543 | 60030.975 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5899 | 5899 | 0 | 42205.183 | 58982.399 | 60030.975 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2009 | 2009 | 0 | 41680.895 | 58720.255 | 59244.543 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1949 | 1949 | 0 | 41680.895 | 58720.255 | 59768.831 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2085 | 2085 | 0 | 42991.615 | 58720.255 | 58982.399 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2012 | 2012 | 0 | 41680.895 | 58720.255 | 60555.263 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5673 | 5673 | 0 | 41680.895 | 58720.255 | 62390.271 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1973 | 1973 | 0 | 42205.183 | 58458.111 | 59244.543 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1944 | 1944 | 0 | 42205.183 | 58195.967 | 58982.399 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1828 | 1828 | 0 | 41680.895 | 58195.967 | 60030.975 |
