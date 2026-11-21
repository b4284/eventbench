# U2-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.283s, closing 2.497s, drain 23.136s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 479312 | 479312 | 6095.8 | 3605.363 | 23160.1 | 0 | 3558 | 3558 | 278 | 2426 | 85460 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 310764 | 27989.155 | 35258.367 | 48300.031 | 54722.559 | 59408.383 | 67829.759 |
| e2e_from_send | 310764 | 24990.018 | 33030.143 | 47841.279 | 54460.415 | 59179.007 | 67829.759 |
| producer_lag | 310764 | 2999.147 | 396.543 | 25133.055 | 26460.159 | 27394.047 | 27721.727 |
| publish_call | 479312 | 223.006 | 109.311 | 222.335 | 439.551 | 25542.655 | 25591.807 |
| queue_wait | 310764 | 24859.653 | 32948.223 | 47775.743 | 54394.879 | 58884.095 | 67829.759 |
| broker_dwell | 310764 | 24622.429 | 32702.463 | 47644.671 | 54296.575 | 58818.559 | 67698.687 |
| receive_call | 431417 | 27.036 | 6.851 | 65.983 | 343.039 | 481.535 | 533.503 |
| ctx_load | 310764 | 14.076 | 3.557 | 14.023 | 52.767 | 285.183 | 15015.935 |
| process | 310764 | 10.818 | 0.002 | 0.004 | 0.046 | 2631.679 | 3002.367 |
| ctx_commit | 310764 | 105.413 | 41.759 | 261.631 | 839.679 | 1492.991 | 1538.047 |
| ack | 310160 | 0.007 | 0.003 | 0.005 | 0.027 | 0.362 | 57.119 |
| service | 310764 | 130.35 | 50.399 | 276.735 | 1032.191 | 2936.831 | 15015.935 |
| e2e_large | 178 | 7964.836 | 3631.103 | 28803.071 | 30949.375 | 32047.103 | 32047.103 |
| ready_wait | 310764 | 1604.359 | 19.599 | 2183.167 | 36012.031 | 42500.095 | 57114.623 |
| blocked | 310764 | 1113.746 | 15.207 | 657.407 | 28671.999 | 31916.031 | 36896.767 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3605.363 | 11995.9 |
| applied_total | 214782.347 | 265350 |
| bench_cpu_cores | 1.576 | 4.196 |
| bench_cpu_pct | 6.566 | 17.48 |
| bench_gc_gen0 | 1.429 | 4 |
| bench_gc_gen1 | 1.245 | 4 |
| bench_gc_gen2 | 0.347 | 1 |
| bench_gc_heap_mb | 989.822 | 1310.5 |
| bench_mem_mb | 1542.216 | 1749.2 |
| bench_threadpool_queue | 50.49 | 553 |
| bench_threadpool_threads | 43.02 | 44 |
| broker_backlog | 138561.429 | 178511 |
| ctr_eb_dc1a_cpu_pct | 89.351 | 770.34 |
| ctr_eb_dc1a_mem_mb | 9326.688 | 10156.6 |
| ctr_eb_dc1b_cpu_pct | 75.573 | 473.42 |
| ctr_eb_dc1b_mem_mb | 3169.273 | 3259.7 |
| ctr_eb_dc2a_cpu_pct | 55.703 | 76.54 |
| ctr_eb_dc2a_mem_mb | 3149.686 | 3180.5 |
| ctr_eb_dc2b_cpu_pct | 766.373 | 914.93 |
| ctr_eb_dc2b_mem_mb | 9419.573 | 10032.7 |
| ctr_eb_etcd_dc1_cpu_pct | 1.671 | 2.34 |
| ctr_eb_etcd_dc1_mem_mb | 34.398 | 36.2 |
| ctr_eb_etcd_dc2_cpu_pct | 1.589 | 1.9 |
| ctr_eb_etcd_dc2_mem_mb | 30.55 | 31.2 |
| ctr_eb_etcd_w_cpu_pct | 1.368 | 2.51 |
| ctr_eb_etcd_w_mem_mb | 26.573 | 28.9 |
| ctr_eb_mosquitto_cpu_pct | 22.523 | 54.05 |
| ctr_eb_mosquitto_mem_mb | 8.443 | 42.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.07 |
| ctr_eb_pgbouncer_2_mem_mb | 9.3 | 9.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.026 | 0.11 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.834 | 2.81 |
| ctr_eb_postgres_mem_mb | 2212.5 | 2212.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 72.731 | 1941.9 |
| host_cpu_pct | 38.471 | 86.64 |
| in_flight | 143506.143 | 179616 |
| nacks_per_s | 72.731 | 1941.9 |
| octopus_dispatcher_cores | 0.849 | 1.6 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 598139.245 | 764174 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 157083.776 | 202424 |
| octopus_mqtt_sent_total | 137508.224 | 177935 |
| octopus_publish_retries_total | 11402.816 | 29408 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.347 | 18 |
| pg_active_loads | 2.816 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 42.735 | 70 |
| pg_conn_idle | 224.286 | 242 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.918 | 9 |
| pg_conn_total | 267.02 | 268 |
| pg_db_size_mb | 1477.902 | 1634.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 9.444 | 46.36 |
| pg_replica_replay_lag_ms | 51.884 | 483.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14964.863 | 23140.2 |
| pg_tup_del_per_s | 23126.306 | 40231.3 |
| pg_tup_fetch_per_s | 516219.465 | 552205.6 |
| pg_tup_ins_per_s | 44386.537 | 64119.4 |
| pg_tup_upd_per_s | 15459.765 | 27760.6 |
| pg_wal_mb_per_s | 97.694 | 119.53 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6095.8 | 18468.7 |
| produced_total | 358288.49 | 444966 |
| publish_mb_per_s | 22.203 | 66.58 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 77,876 records are Completed |
| record_counts | pass | 77,876 records found, 77,876 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 278 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 479,312 events applied by workers, 479,312 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 85,460 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 28672 ms, max 36885 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15272 seq 6: ready 126 ms before start, 126 ms of it with an idle slot in group 477
- [cross_source_blocking] source 7316 seq 8: ready 113 ms before start, 113 ms of it with an idle slot in group 251
- [cross_source_blocking] source 2890 seq 7: ready 158 ms before start, 158 ms of it with an idle slot in group 92
- [cross_source_blocking] source 8386 seq 5: ready 152 ms before start, 152 ms of it with an idle slot in group 284
- [cross_source_blocking] source 9468 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 327
- [cross_source_blocking] source 404 seq 12: ready 115 ms before start, 115 ms of it with an idle slot in group 12
- [cross_source_blocking] source 8475 seq 16: ready 126 ms before start, 126 ms of it with an idle slot in group 289
- [cross_source_blocking] source 8234 seq 14: ready 134 ms before start, 134 ms of it with an idle slot in group 278
- [cross_source_blocking] source 9512 seq 14: ready 127 ms before start, 127 ms of it with an idle slot in group 329
- [cross_source_blocking] source 15758 seq 17: ready 134 ms before start, 134 ms of it with an idle slot in group 493
- [cross_source_blocking] source 2332 seq 18: ready 115 ms before start, 115 ms of it with an idle slot in group 67
- [cross_source_blocking] source 15638 seq 20: ready 145 ms before start, 145 ms of it with an idle slot in group 487
- [cross_source_blocking] source 7158 seq 10: ready 141 ms before start, 141 ms of it with an idle slot in group 243
- [cross_source_blocking] source 15602 seq 13: ready 153 ms before start, 153 ms of it with an idle slot in group 484
- [cross_source_blocking] source 1351 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 43
- [cross_source_blocking] source 7634 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 262
- [cross_source_blocking] source 14635 seq 6: ready 113 ms before start, 113 ms of it with an idle slot in group 463
- [cross_source_blocking] source 15130 seq 7: ready 135 ms before start, 135 ms of it with an idle slot in group 472
- [cross_source_blocking] source 2932 seq 4: ready 113 ms before start, 113 ms of it with an idle slot in group 94
- [cross_source_blocking] source 6812 seq 11: ready 120 ms before start, 120 ms of it with an idle slot in group 232

## Error samples

- t-392#0 src=12352 seq=12 op=Update rec=53051436040196 ev=3: NpgsqlException: No suitable host was found.
- t-354#1 src=10884 seq=12 op=Update rec=46746424049667 ev=1: NpgsqlException: No suitable host was found.
- t-476#0 src=15248 seq=28 op=Complete rec=65489661329414 ev=2: NpgsqlException: No suitable host was found.
- t-150#0 src=4396 seq=19 op=Update rec=18880676233219 ev=1: NpgsqlException: No suitable host was found.
- t-326#0 src=9461 seq=57 op=Update rec=40634685587462 ev=14: NpgsqlException: No suitable host was found.
- t-119L#0 src=3465 seq=4 op=Update rec=14882061680641 ev=1: NpgsqlException: No suitable host was found.
- t-200#0 src=5784 seq=11 op=Update rec=24842090840067 ev=1: NpgsqlException: No suitable host was found.
- t-246#1 src=7220 seq=35 op=Update rec=31009663877124 ev=2: NpgsqlException: No suitable host was found.
- t-376#0 src=11551 seq=11 op=Get rec=49611167236097 ev=3: NpgsqlException: No suitable host was found.
- t-242#0 src=7122 seq=19 op=Get rec=30588757082115 ev=1: NpgsqlException: Exception while reading from stream
- t-066#0 src=2315 seq=15 op=Update rec=9942849290244 ev=2: NpgsqlException: No suitable host was found.
- t-107#0 src=3219 seq=26 op=Get rec=13825499725827 ev=10: NpgsqlException: No suitable host was found.
- t-425#1 src=13254 seq=19 op=Update rec=56925496541187 ev=5: NpgsqlException: No suitable host was found.
- t-309#0 src=8962 seq=7 op=Create rec=38491496906754 ev=0: NpgsqlException: No suitable host was found.
- t-226#0 src=6662 seq=64 op=Get rec=28613072125963 ev=3: NpgsqlException: No suitable host was found.
- t-383#2 src=11995 seq=18 op=Get rec=51518132715524 ev=1: NpgsqlException: No suitable host was found.
- t-194#1 src=5601 seq=22 op=Update rec=24056111824898 ev=1: NpgsqlException: No suitable host was found.
- t-348#1 src=10554 seq=14 op=Create rec=45329084841989 ev=0: NpgsqlException: No suitable host was found.
- t-147#0 src=4307 seq=28 op=Update rec=18498424143876 ev=2: NpgsqlException: No suitable host was found.
- t-160#0 src=4570 seq=21 op=Update rec=19628000542722 ev=7: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3503 | 3503 | 0 | 46661.631 | 62390.271 | 66584.575 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1788 | 1788 | 0 | 47448.063 | 61603.839 | 67108.863 |
| t-231 | 43 | 1 | 50.375 | 34.767 | 156 | 0 | 1284 | 1284 | 0 | 39583.743 | 58982.399 | 62128.127 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1844 | 1844 | 0 | 47972.351 | 58982.399 | 60293.119 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3548 | 3548 | 0 | 45350.911 | 58195.967 | 59768.831 |
| t-372 | 65 | 1 | 32.959 | 35.645 | 875 | 0 | 1396 | 1396 | 0 | 45088.767 | 57671.679 | 59506.687 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3755 | 3755 | 0 | 44040.191 | 57409.535 | 60293.119 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 1530 | 1530 | 0 | 45088.767 | 57409.535 | 63963.135 |
| t-189 | 59 | 1 | 44.753 | 43.324 | 1601 | 0 | 1608 | 1608 | 0 | 40370.175 | 57147.391 | 66846.719 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1714 | 1714 | 0 | 44302.335 | 57147.391 | 68157.439 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3737 | 3737 | 0 | 43515.903 | 56885.247 | 58982.399 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1704 | 1704 | 0 | 43515.903 | 56623.103 | 59768.831 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3225 | 3225 | 0 | 44040.191 | 56623.103 | 57933.823 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1717 | 1717 | 0 | 44040.191 | 56360.959 | 58195.967 |
| t-129 | 19 | 1 | 90.638 | 29.84 | 623 | 0 | 1057 | 1057 | 0 | 34078.719 | 56098.815 | 57933.823 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5267 | 5267 | 0 | 43515.903 | 56098.815 | 68157.439 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3503 | 3503 | 0 | 42991.615 | 56098.815 | 56623.103 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4526 | 4526 | 0 | 40894.463 | 56098.815 | 62914.559 |
| t-184 | 20 | 1 | 79.554 | 25.849 | 161 | 0 | 943 | 943 | 0 | 32112.639 | 55836.671 | 58458.111 |
| t-253 | 26 | 1 | 73.629 | 32.505 | 1433 | 0 | 1172 | 1172 | 0 | 37486.591 | 55836.671 | 59768.831 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3334 | 3334 | 0 | 43515.903 | 55836.671 | 56623.103 |
| t-378 | 55 | 2 | 85.868 | 76.933 | 3659 | 0 | 2742 | 2742 | 0 | 40894.463 | 55836.671 | 63700.991 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 2579 | 2579 | 0 | 39059.455 | 55574.527 | 63963.135 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1747 | 1747 | 0 | 43778.047 | 55574.527 | 56623.103 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1719 | 1719 | 0 | 42467.327 | 55312.383 | 56360.959 |
