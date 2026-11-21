# H07-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.296s, closing 2.564s, drain 18.643s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 520059 | 520059 | 7106.257 | 4283.078 | 20370.6 | 0 | 2142 | 2142 | 292 | 2618 | 60746 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 350530 | 32640.628 | 36208.639 | 49479.679 | 57049.087 | 60162.047 | 62783.487 |
| e2e_from_send | 350530 | 24342.316 | 25690.111 | 48005.119 | 56033.279 | 59244.543 | 61931.519 |
| producer_lag | 350947 | 8290.185 | 1800.191 | 24084.479 | 25034.751 | 25837.567 | 26361.855 |
| publish_call | 520059 | 214.581 | 126.975 | 233.471 | 456.447 | 21774.335 | 22118.399 |
| queue_wait | 350530 | 24221.051 | 25493.503 | 47939.583 | 56000.511 | 59211.775 | 61833.215 |
| broker_dwell | 350530 | 24059.69 | 24936.447 | 47808.511 | 55869.439 | 59146.239 | 61767.679 |
| receive_call | 471884 | 24.204 | 7.279 | 50.591 | 318.207 | 474.879 | 557.567 |
| ctx_load | 350530 | 9.187 | 6.087 | 19.215 | 51.135 | 92.479 | 204.543 |
| process | 350530 | 10.699 | 0.002 | 0.004 | 0.057 | 2625.535 | 3012.607 |
| ctx_commit | 350530 | 101.255 | 47.615 | 257.279 | 580.607 | 3397.631 | 3469.311 |
| ack | 350509 | 0.009 | 0.004 | 0.007 | 0.042 | 0.766 | 61.343 |
| service | 350530 | 121.237 | 58.271 | 283.135 | 756.223 | 3403.775 | 4202.495 |
| e2e_large | 192 | 10381.637 | 4689.919 | 26378.239 | 28262.399 | 31391.743 | 31391.743 |
| ready_wait | 350530 | 1112.485 | 24.479 | 1057.791 | 36503.551 | 45154.303 | 55705.599 |
| blocked | 350530 | 604.196 | 17.791 | 253.183 | 20643.839 | 26902.527 | 29327.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4283.078 | 15152.5 |
| applied_total | 173362.551 | 283731 |
| bench_cpu_cores | 1.791 | 3.778 |
| bench_cpu_pct | 7.462 | 15.74 |
| bench_gc_gen0 | 1.286 | 4 |
| bench_gc_gen1 | 0.898 | 3 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 1145.069 | 1406.1 |
| bench_mem_mb | 1760.541 | 1917.4 |
| bench_threadpool_queue | 41.245 | 872 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 161316.184 | 201891 |
| ctr_eb_dc1a_cpu_pct | 147.607 | 1092.25 |
| ctr_eb_dc1a_mem_mb | 7433.52 | 8357 |
| ctr_eb_dc1b_cpu_pct | 28.684 | 66.4 |
| ctr_eb_dc1b_mem_mb | 3718.071 | 3797.5 |
| ctr_eb_dc2a_cpu_pct | 642.297 | 716.22 |
| ctr_eb_dc2a_mem_mb | 7380.31 | 7854.5 |
| ctr_eb_dc2b_cpu_pct | 44.099 | 45.54 |
| ctr_eb_dc2b_mem_mb | 8002.62 | 8015.6 |
| ctr_eb_etcd_dc1_cpu_pct | 1.506 | 2.86 |
| ctr_eb_etcd_dc1_mem_mb | 32.906 | 36 |
| ctr_eb_etcd_dc2_cpu_pct | 1.434 | 1.82 |
| ctr_eb_etcd_dc2_mem_mb | 33.675 | 34 |
| ctr_eb_etcd_w_cpu_pct | 1.635 | 3.15 |
| ctr_eb_etcd_w_mem_mb | 68.076 | 70 |
| ctr_eb_haproxy_cpu_pct | 55.248 | 131.21 |
| ctr_eb_haproxy_mem_mb | 33.682 | 37.5 |
| ctr_eb_mosquitto_cpu_pct | 21.435 | 49.77 |
| ctr_eb_mosquitto_mem_mb | 5.031 | 9.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.07 |
| ctr_eb_pgbouncer_mem_mb | 8.9 | 8.9 |
| ctr_eb_postgres_cpu_pct | 0.76 | 2.1 |
| ctr_eb_postgres_mem_mb | 1809.069 | 1813.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 43.778 | 2138.1 |
| host_cpu_pct | 41.388 | 82.4 |
| in_flight | 164729.878 | 200238 |
| nacks_per_s | 43.778 | 2138.1 |
| octopus_dispatcher_cores | 0.806 | 1.47 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 532232.571 | 831001 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 152557.02 | 227922 |
| octopus_mqtt_sent_total | 106250.735 | 184948 |
| octopus_publish_retries_total | 9541.102 | 21996 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.939 | 21 |
| pg_active_loads | 11.02 | 18 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 66.673 | 77 |
| pg_conn_idle | 201.224 | 239 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 267.898 | 269 |
| pg_db_size_mb | 1404.241 | 1554.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 14.204 | 17.91 |
| pg_replica_replay_lag_ms | 133.7 | 184 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14134.09 | 17424.9 |
| pg_tup_del_per_s | 20832.402 | 29930.8 |
| pg_tup_fetch_per_s | 468815.251 | 530537.4 |
| pg_tup_ins_per_s | 44923.288 | 55000.4 |
| pg_tup_upd_per_s | 15024.727 | 19503.4 |
| pg_wal_mb_per_s | 79.808 | 98.01 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7106.257 | 21149.9 |
| produced_total | 338092.429 | 483969 |
| publish_mb_per_s | 25.224 | 62.61 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 82,871 records are Completed |
| record_counts | pass | 82,871 records found, 82,871 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 292 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 520,059 events applied by workers, 520,059 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 60,746 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 20644 ms, max 29315 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4282 seq 12: ready 125 ms before start, 125 ms of it with an idle slot in group 146
- [cross_source_blocking] source 4028 seq 2: ready 208 ms before start, 208 ms of it with an idle slot in group 135
- [cross_source_blocking] source 15802 seq 9: ready 211 ms before start, 211 ms of it with an idle slot in group 495
- [cross_source_blocking] source 9533 seq 5: ready 207 ms before start, 207 ms of it with an idle slot in group 331
- [cross_source_blocking] source 6396 seq 8: ready 208 ms before start, 208 ms of it with an idle slot in group 219
- [cross_source_blocking] source 14877 seq 7: ready 207 ms before start, 207 ms of it with an idle slot in group 467
- [cross_source_blocking] source 861 seq 11: ready 310 ms before start, 310 ms of it with an idle slot in group 24
- [cross_source_blocking] source 10717 seq 3: ready 310 ms before start, 310 ms of it with an idle slot in group 351
- [cross_source_blocking] source 15957 seq 6: ready 311 ms before start, 311 ms of it with an idle slot in group 498
- [cross_source_blocking] source 4235 seq 5: ready 270 ms before start, 270 ms of it with an idle slot in group 142
- [cross_source_blocking] source 14631 seq 5: ready 182 ms before start, 182 ms of it with an idle slot in group 463
- [cross_source_blocking] source 4244 seq 5: ready 176 ms before start, 176 ms of it with an idle slot in group 142
- [cross_source_blocking] source 8572 seq 14: ready 573 ms before start, 102 ms of it with an idle slot in group 295
- [cross_source_blocking] source 13984 seq 10: ready 176 ms before start, 102 ms of it with an idle slot in group 444
- [cross_source_blocking] source 2772 seq 7: ready 176 ms before start, 176 ms of it with an idle slot in group 85
- [cross_source_blocking] source 15296 seq 4: ready 176 ms before start, 176 ms of it with an idle slot in group 478
- [cross_source_blocking] source 4500 seq 8: ready 176 ms before start, 176 ms of it with an idle slot in group 156
- [cross_source_blocking] source 11528 seq 9: ready 176 ms before start, 176 ms of it with an idle slot in group 375
- [cross_source_blocking] source 8092 seq 24: ready 677 ms before start, 122 ms of it with an idle slot in group 273
- [cross_source_blocking] source 15752 seq 8: ready 176 ms before start, 176 ms of it with an idle slot in group 492

## Error samples

- t-319L#1 src=9271 seq=4 op=Create rec=39818641801218 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3445 seq=4 op=Update rec=14796162334721 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3482 seq=5 op=Update rec=14955076124673 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3432 seq=3 op=Update rec=14740327759873 ev=2: NpgsqlException: Exception while reading from stream
- t-130#0 src=3829 seq=15 op=Complete rec=16445429776385 ev=8: NpgsqlException: Exception while reading from stream
- t-396#0 src=12447 seq=16 op=Update rec=53459457933314 ev=2: NpgsqlException: Exception while reading from stream
- t-015#0 src=449 seq=18 op=Update rec=1928440315907 ev=6: NpgsqlException: Exception while reading from stream
- t-406#0 src=12845 seq=11 op=Create rec=55168854917123 ev=0: NpgsqlException: Exception while reading from stream
- t-223#0 src=6637 seq=31 op=Update rec=28505697943556 ev=5: NpgsqlException: Exception while reading from stream
- t-156#0 src=4502 seq=16 op=Create rec=19335942766596 ev=0: NpgsqlException: Exception while reading from stream
- t-327#0 src=9467 seq=35 op=Update rec=40660455391233 ev=8: NpgsqlException: Exception while reading from stream
- t-160#0 src=4569 seq=16 op=Get rec=19623705575426 ev=5: NpgsqlException: Exception while reading from stream
- t-115#0 src=3349 seq=16 op=Get rec=14383845474305 ev=5: NpgsqlException: Exception while reading from stream
- t-012#0 src=404 seq=35 op=Update rec=1735166787589 ev=2: NpgsqlException: Exception while reading from stream
- t-072#0 src=2455 seq=15 op=Update rec=10544144711682 ev=2: NpgsqlException: Exception while reading from stream
- t-422#0 src=13215 seq=25 op=Create rec=56757992816645 ev=0: NpgsqlException: Exception while reading from stream
- t-000#0 src=1 seq=30 op=Update rec=4294967299 ev=4: NpgsqlException: Exception while reading from stream
- t-317#0 src=9223 seq=16 op=Get rec=39612483371012 ev=1: NpgsqlException: Exception while reading from stream
- t-176#0 src=5052 seq=18 op=Create rec=21698174779396 ev=0: NpgsqlException: Exception while reading from stream
- t-054#0 src=1895 seq=18 op=Create rec=8138963025926 ev=0: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4067 | 4067 | 0 | 43778.047 | 61341.695 | 62390.271 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1937 | 1937 | 0 | 42729.471 | 61341.695 | 62914.559 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4074 | 4074 | 0 | 42729.471 | 61079.551 | 62390.271 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1831 | 1831 | 0 | 43253.759 | 60817.407 | 62390.271 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3841 | 3841 | 0 | 43778.047 | 60555.263 | 62652.415 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5745 | 5745 | 0 | 43515.903 | 60555.263 | 62652.415 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1881 | 1881 | 0 | 43515.903 | 60030.975 | 60817.407 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1857 | 1857 | 0 | 42729.471 | 60030.975 | 61079.551 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1872 | 1872 | 0 | 42467.327 | 59768.831 | 61079.551 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3791 | 3791 | 0 | 42467.327 | 59768.831 | 60817.407 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3616 | 3616 | 0 | 42729.471 | 59768.831 | 61079.551 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1881 | 1881 | 0 | 42467.327 | 59768.831 | 61341.695 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5559 | 5559 | 0 | 42729.471 | 59768.831 | 60817.407 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5626 | 5626 | 0 | 42991.615 | 59506.687 | 60817.407 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1843 | 1843 | 0 | 42467.327 | 59244.543 | 60030.975 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1871 | 1871 | 0 | 42729.471 | 58982.399 | 60293.119 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1720 | 1720 | 0 | 43253.759 | 58982.399 | 59768.831 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3792 | 3792 | 0 | 43515.903 | 58982.399 | 60817.407 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1918 | 1918 | 0 | 42991.615 | 58982.399 | 60293.119 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1743 | 1743 | 0 | 42467.327 | 58720.255 | 60555.263 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1859 | 1859 | 0 | 42467.327 | 58720.255 | 59244.543 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1973 | 1973 | 0 | 43515.903 | 58720.255 | 59244.543 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5352 | 5352 | 0 | 42205.183 | 58720.255 | 61865.983 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1835 | 1835 | 0 | 42467.327 | 58458.111 | 59506.687 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7621 | 7621 | 0 | 42467.327 | 58458.111 | 60555.263 |
