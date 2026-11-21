# V0-none — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.416s, closing 3.187s, drain 41.22s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 764364 | 764364 | 12076.496 | 7477.786 | 21268.1 | 0 | 0 | 0 | 407 | 3950 | 235136 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 592804 | 20508.339 | 20004.863 | 40730.623 | 51052.543 | 56033.279 | 60948.479 |
| e2e_from_send | 592804 | 19343.711 | 18628.607 | 39780.351 | 50069.503 | 54820.863 | 59080.703 |
| producer_lag | 592804 | 1164.633 | 1116.159 | 2379.775 | 3971.071 | 4620.287 | 5496.831 |
| publish_call | 764364 | 138.759 | 111.871 | 230.655 | 589.823 | 2480.127 | 2537.471 |
| queue_wait | 592804 | 19206.194 | 18497.535 | 39714.815 | 50036.735 | 54755.327 | 59080.703 |
| broker_dwell | 592804 | 19085.448 | 18366.463 | 39649.279 | 49938.431 | 54689.791 | 59047.935 |
| receive_call | 673926 | 27.871 | 7.919 | 71.615 | 321.535 | 474.111 | 570.879 |
| ctx_load | 592804 | 8.675 | 4.399 | 18.863 | 59.615 | 116.479 | 2136.063 |
| process | 592804 | 10.812 | 0.002 | 0.005 | 0.061 | 2623.487 | 3028.991 |
| ctx_commit | 592804 | 117.974 | 69.503 | 239.359 | 887.295 | 2600.959 | 2623.487 |
| ack | 591554 | 0.007 | 0.003 | 0.006 | 0.034 | 0.488 | 48.575 |
| service | 592804 | 137.515 | 81.023 | 256.383 | 1311.743 | 2799.615 | 5476.351 |
| e2e_large | 307 | 4184.453 | 4020.223 | 6057.983 | 7520.255 | 9060.351 | 9060.351 |
| ready_wait | 592804 | 908.23 | 50.271 | 2158.591 | 15269.887 | 27017.215 | 37584.895 |
| blocked | 592804 | 346.452 | 32.031 | 865.791 | 4882.431 | 11395.071 | 15400.959 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7477.786 | 11960.7 |
| applied_total | 277118.061 | 434999 |
| bench_cpu_cores | 2.655 | 3.961 |
| bench_cpu_pct | 11.061 | 16.5 |
| bench_gc_gen0 | 2.347 | 4 |
| bench_gc_gen1 | 2.224 | 4 |
| bench_gc_gen2 | 0.551 | 1 |
| bench_gc_heap_mb | 1067.102 | 1264 |
| bench_mem_mb | 1595.661 | 1718.8 |
| bench_threadpool_queue | 88.224 | 555 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 168896.735 | 279890 |
| ctr_eb_dc1a_cpu_pct | 54.047 | 67.92 |
| ctr_eb_dc1a_mem_mb | 2662.137 | 2944 |
| ctr_eb_dc1b_cpu_pct | 51.949 | 68.66 |
| ctr_eb_dc1b_mem_mb | 2656.245 | 2927.8 |
| ctr_eb_dc2a_cpu_pct | 54.305 | 70.69 |
| ctr_eb_dc2a_mem_mb | 2831.661 | 3075.6 |
| ctr_eb_dc2b_cpu_pct | 806.722 | 947.45 |
| ctr_eb_dc2b_mem_mb | 10071.004 | 11653.9 |
| ctr_eb_etcd_dc1_cpu_pct | 1.627 | 2.2 |
| ctr_eb_etcd_dc1_mem_mb | 25.914 | 27 |
| ctr_eb_etcd_dc2_cpu_pct | 1.619 | 2.01 |
| ctr_eb_etcd_dc2_mem_mb | 24.969 | 25.9 |
| ctr_eb_etcd_w_cpu_pct | 1.788 | 2.31 |
| ctr_eb_etcd_w_mem_mb | 25.384 | 26.3 |
| ctr_eb_mosquitto_cpu_pct | 41.237 | 55.58 |
| ctr_eb_mosquitto_mem_mb | 9.082 | 39.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.029 | 0.09 |
| ctr_eb_pgbouncer_2_mem_mb | 9.516 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.031 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 10.018 | 10.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.03 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.916 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.027 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 1.655 | 2.79 |
| ctr_eb_postgres_mem_mb | 2153.231 | 2155.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 63.829 | 78.64 |
| in_flight | 173706.612 | 290250 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.484 | 1.66 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 723008.163 | 1167705 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 185401.837 | 295328 |
| octopus_mqtt_sent_total | 166022.653 | 271293 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.408 | 20 |
| pg_active_loads | 5.061 | 21 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 52 | 77 |
| pg_conn_idle | 215.837 | 241 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.469 | 16 |
| pg_conn_total | 267.837 | 269 |
| pg_db_size_mb | 1599.159 | 2156.8 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 9.885 | 48.42 |
| pg_replica_replay_lag_ms | 92.618 | 609.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19202.782 | 27177 |
| pg_tup_del_per_s | 27270.537 | 38332.7 |
| pg_tup_fetch_per_s | 501978.078 | 681620.7 |
| pg_tup_ins_per_s | 46630.447 | 60868.7 |
| pg_tup_upd_per_s | 21876.396 | 31543.5 |
| pg_wal_mb_per_s | 97.797 | 204.17 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12076.496 | 19543.8 |
| produced_total | 450824.673 | 725249 |
| publish_mb_per_s | 42.091 | 73.99 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 111,861 records are Completed |
| record_counts | pass | 111,861 records found, 111,861 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 407 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 764,364 events applied by workers, 764,364 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 235,136 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 4882 ms, max 15394 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4022 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 134
- [cross_source_blocking] source 6710 seq 11: ready 106 ms before start, 106 ms of it with an idle slot in group 229
- [cross_source_blocking] source 12886 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 408
- [cross_source_blocking] source 12662 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 398
- [cross_source_blocking] source 7318 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 251
- [cross_source_blocking] source 12022 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 384
- [cross_source_blocking] source 4214 seq 8: ready 239 ms before start, 176 ms of it with an idle slot in group 140
- [cross_source_blocking] source 4210 seq 3: ready 361 ms before start, 361 ms of it with an idle slot in group 139
- [cross_source_blocking] source 8598 seq 5: ready 353 ms before start, 109 ms of it with an idle slot in group 296
- [cross_source_blocking] source 6812 seq 11: ready 392 ms before start, 392 ms of it with an idle slot in group 232
- [cross_source_blocking] source 7260 seq 6: ready 392 ms before start, 392 ms of it with an idle slot in group 248
- [cross_source_blocking] source 12892 seq 5: ready 391 ms before start, 391 ms of it with an idle slot in group 408
- [cross_source_blocking] source 12 seq 9: ready 116 ms before start, 116 ms of it with an idle slot in group 0
- [cross_source_blocking] source 2357 seq 7: ready 120 ms before start, 120 ms of it with an idle slot in group 68
- [cross_source_blocking] source 2970 seq 14: ready 143 ms before start, 143 ms of it with an idle slot in group 97
- [cross_source_blocking] source 1880 seq 7: ready 116 ms before start, 116 ms of it with an idle slot in group 54
- [cross_source_blocking] source 7292 seq 4: ready 116 ms before start, 116 ms of it with an idle slot in group 250
- [cross_source_blocking] source 10924 seq 14: ready 116 ms before start, 116 ms of it with an idle slot in group 356
- [cross_source_blocking] source 11212 seq 8: ready 117 ms before start, 117 ms of it with an idle slot in group 365
- [cross_source_blocking] source 7404 seq 12: ready 116 ms before start, 116 ms of it with an idle slot in group 256

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2691 | 2691 | 0 | 34078.719 | 59244.543 | 61079.551 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2861 | 2861 | 0 | 34340.863 | 58195.967 | 60030.975 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5948 | 5948 | 0 | 34603.007 | 57933.823 | 60030.975 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5556 | 5556 | 0 | 33554.431 | 56623.103 | 58720.255 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8581 | 8581 | 0 | 33423.359 | 56623.103 | 58720.255 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2801 | 2801 | 0 | 32636.927 | 56098.815 | 58195.967 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5921 | 5921 | 0 | 33816.575 | 56098.815 | 57933.823 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5661 | 5661 | 0 | 34078.719 | 55574.527 | 58458.111 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2708 | 2708 | 0 | 32505.855 | 55312.383 | 56885.247 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5547 | 5547 | 0 | 32767.999 | 55050.239 | 56623.103 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2803 | 2803 | 0 | 32767.999 | 54788.095 | 56885.247 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2828 | 2828 | 0 | 32505.855 | 54788.095 | 56360.959 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5340 | 5340 | 0 | 31719.423 | 54525.951 | 56360.959 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2729 | 2729 | 0 | 31588.351 | 54525.951 | 56623.103 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5151 | 5151 | 0 | 31064.063 | 54001.663 | 55836.671 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2681 | 2681 | 0 | 32112.639 | 53739.519 | 54788.095 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2724 | 2724 | 0 | 31326.207 | 53739.519 | 55574.527 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2724 | 2724 | 0 | 31064.063 | 53739.519 | 55574.527 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2533 | 2533 | 0 | 31457.279 | 53215.231 | 54525.951 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2795 | 2795 | 0 | 31850.495 | 53215.231 | 54525.951 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2462 | 2462 | 0 | 30670.847 | 52953.087 | 55574.527 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2731 | 2731 | 0 | 30932.991 | 52428.799 | 54525.951 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8111 | 8111 | 0 | 30932.991 | 52428.799 | 53739.519 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 4991 | 4991 | 0 | 30146.559 | 52166.655 | 54263.807 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8285 | 8285 | 0 | 31064.063 | 51904.511 | 54525.951 |
