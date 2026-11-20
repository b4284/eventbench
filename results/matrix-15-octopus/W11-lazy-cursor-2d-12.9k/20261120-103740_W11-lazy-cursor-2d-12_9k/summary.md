# W11-lazy-cursor-2d-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.128s, closing 1.319s, drain 2.356s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814589 | 814589 | 12853.516 | 12854.904 | 22670.2 | 0 | 0 | 0 | 431 | 4254 | 954 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642863 | 43.317 | 15.695 | 33.567 | 1202.175 | 2748.415 | 6021.119 |
| e2e_from_send | 642863 | 41.811 | 14.239 | 31.599 | 1200.127 | 2746.367 | 5996.543 |
| producer_lag | 642863 | 1.507 | 1.012 | 3.445 | 8.639 | 21.007 | 135.423 |
| publish_call | 814589 | 13.392 | 8.591 | 26.831 | 82.815 | 167.423 | 406.527 |
| queue_wait | 642863 | 25.537 | 9.487 | 22.031 | 389.631 | 2236.415 | 4057.087 |
| broker_dwell | 642863 | 17.681 | 2.435 | 9.511 | 381.183 | 2230.271 | 4052.991 |
| receive_call | 766512 | 39.964 | 16.767 | 101.951 | 344.063 | 490.751 | 566.783 |
| ctx_load | 642863 | 2.325 | 1.783 | 4.371 | 9.079 | 18.975 | 38.079 |
| process | 642863 | 10.903 | 0.001 | 0.002 | 0.022 | 2625.535 | 3002.367 |
| ctx_commit | 642863 | 3.027 | 2.313 | 5.635 | 12.975 | 24.975 | 118.143 |
| ack | 641127 | 0.005 | 0.003 | 0.007 | 0.03 | 0.169 | 22.287 |
| service | 642863 | 16.274 | 4.427 | 9.575 | 23.887 | 2627.583 | 3037.183 |
| e2e_large | 331 | 2406.975 | 2437.119 | 3215.359 | 4640.767 | 6021.119 | 6021.119 |
| ready_wait | 642863 | 3.561 | 1.398 | 8.439 | 25.967 | 158.975 | 1924.095 |
| blocked | 642863 | 3.51 | 1.395 | 8.407 | 25.823 | 148.991 | 1797.119 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12854.904 | 13470.4 |
| applied_total | 468299.49 | 777185 |
| bench_cpu_cores | 4.792 | 5.18 |
| bench_cpu_pct | 19.965 | 21.58 |
| bench_gc_gen0 | 2.816 | 4 |
| bench_gc_gen1 | 1.857 | 4 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 972.28 | 1242.9 |
| bench_mem_mb | 1476.241 | 1652.5 |
| bench_threadpool_queue | 16.347 | 120 |
| bench_threadpool_threads | 39.735 | 44 |
| broker_backlog | 1484.102 | 2243 |
| ctr_eb_mosquitto_cpu_pct | 53.257 | 57.48 |
| ctr_eb_mosquitto_mem_mb | 8.937 | 20.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 18.054 | 18.72 |
| ctr_eb_pgbouncer_2_mem_mb | 15.388 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 17.446 | 18.12 |
| ctr_eb_pgbouncer_3_mem_mb | 15.357 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 18.754 | 19.89 |
| ctr_eb_pgbouncer_4_mem_mb | 15.514 | 16.6 |
| ctr_eb_pgbouncer_cpu_pct | 18.857 | 19.34 |
| ctr_eb_pgbouncer_mem_mb | 15.89 | 17.1 |
| ctr_eb_postgres_cpu_pct | 575.674 | 632.43 |
| ctr_eb_postgres_mem_mb | 5231.804 | 5426.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 61.916 | 67 |
| in_flight | 402.857 | 552 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.41 | 1.46 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 923641.184 | 1580829 |
| octopus_mqtt_received_total | 143384.612 | 243449 |
| octopus_mqtt_sent_total | 300136.429 | 529513 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 1.653 | 6 |
| pg_active_loads | 2.041 | 7 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 6.061 | 13 |
| pg_conn_idle | 230.061 | 236 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.041 | 1 |
| pg_conn_total | 236.122 | 237 |
| pg_db_size_mb | 3323.453 | 3752.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18682.982 | 19724.1 |
| pg_tup_del_per_s | 36382.894 | 46954.8 |
| pg_tup_fetch_per_s | 232667.171 | 277424.7 |
| pg_tup_ins_per_s | 46996.139 | 59800.9 |
| pg_tup_upd_per_s | 34153.01 | 36049.7 |
| pg_wal_mb_per_s | 95.541 | 172.61 |
| pgb_cl_active | 246.347 | 263 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 3.184 | 12 |
| pgb_sv_idle | 44.816 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.516 | 13281.2 |
| produced_total | 468702.347 | 777526 |
| publish_mb_per_s | 44.332 | 71.23 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,590 records are Completed |
| record_counts | pass | 117,590 records found, 117,590 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,589 events applied by workers, 814,589 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 954 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26 ms, max 1797 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1317 seq 34: ready 134 ms before start, 134 ms of it with an idle slot in group 41
- [cross_source_blocking] source 3937 seq 15: ready 134 ms before start, 134 ms of it with an idle slot in group 131
- [cross_source_blocking] source 241 seq 15: ready 138 ms before start, 138 ms of it with an idle slot in group 5
- [cross_source_blocking] source 4346 seq 27: ready 588 ms before start, 588 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4331 seq 28: ready 589 ms before start, 589 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4294 seq 28: ready 578 ms before start, 578 ms of it with an idle slot in group 147
- [cross_source_blocking] source 11856 seq 12: ready 280 ms before start, 280 ms of it with an idle slot in group 383
- [cross_source_blocking] source 4348 seq 22: ready 549 ms before start, 549 ms of it with an idle slot in group 147
- [cross_source_blocking] source 11926 seq 10: ready 280 ms before start, 280 ms of it with an idle slot in group 383
- [cross_source_blocking] source 4306 seq 29: ready 198 ms before start, 198 ms of it with an idle slot in group 147
- [cross_source_blocking] source 11860 seq 13: ready 261 ms before start, 261 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11952 seq 19: ready 267 ms before start, 267 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11900 seq 14: ready 239 ms before start, 239 ms of it with an idle slot in group 383
- [cross_source_blocking] source 3463 seq 4: ready 116 ms before start, 116 ms of it with an idle slot in group 119
- [cross_source_blocking] source 4308 seq 33: ready 178 ms before start, 178 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4359 seq 42: ready 172 ms before start, 172 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4313 seq 35: ready 160 ms before start, 160 ms of it with an idle slot in group 147
- [cross_source_blocking] source 49 seq 10: ready 108 ms before start, 108 ms of it with an idle slot in group 1
- [cross_source_blocking] source 14573 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 461
- [cross_source_blocking] source 8553 seq 8: ready 108 ms before start, 108 ms of it with an idle slot in group 293

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 1966.079 | 4620.287 | 5406.719 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 1744.895 | 4521.983 | 6029.311 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 2146.303 | 4358.143 | 4358.143 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 1458.175 | 3194.879 | 3719.167 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 15.423 | 2850.815 | 3883.007 |
| t-418 | 6 | 1 | 121.075 | 12.228 | 201 | 0 | 761 | 761 | 0 | 15.871 | 2588.671 | 2932.735 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 16.127 | 2555.903 | 3768.319 |
| t-447 | 17 | 1 | 45.301 | 12.225 | 558 | 0 | 750 | 750 | 0 | 15.295 | 2506.751 | 2899.967 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 15.551 | 2490.367 | 2752.511 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 15.039 | 2375.679 | 2801.663 |
| t-349 | 14 | 1 | 44.443 | 10.033 | 379 | 0 | 603 | 603 | 0 | 14.847 | 2375.679 | 2867.199 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1790 | 1790 | 0 | 15.487 | 2359.295 | 2834.431 |
| t-299 | 21 | 1 | 42.355 | 14.484 | 646 | 0 | 907 | 907 | 0 | 16.319 | 2260.991 | 2981.887 |
| t-381 | 11 | 1 | 47.024 | 8.839 | 134 | 0 | 603 | 603 | 0 | 15.871 | 2228.223 | 2834.431 |
| t-320 | 14 | 1 | 86.153 | 19.097 | 574 | 0 | 1206 | 1206 | 0 | 16.191 | 2211.839 | 2899.967 |
| t-338 | 6 | 1 | 21.73 | 2.22 | 156 | 0 | 140 | 140 | 0 | 14.847 | 2179.071 | 2555.903 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 15.295 | 2162.687 | 2883.583 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4563 | 4563 | 0 | 16.767 | 2162.687 | 3997.695 |
| t-420 | 13 | 1 | 49.618 | 10.967 | 270 | 0 | 712 | 712 | 0 | 16.127 | 2162.687 | 2932.735 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 15.295 | 2146.303 | 2752.511 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 16.383 | 2097.151 | 2441.215 |
| t-481 | 12 | 1 | 121.412 | 22.891 | 1993 | 0 | 1428 | 1428 | 0 | 16.255 | 2072.575 | 2801.663 |
| t-243 | 16 | 1 | 59.376 | 15.905 | 3143 | 0 | 984 | 984 | 0 | 15.295 | 2031.615 | 3014.655 |
| t-054 | 17 | 1 | 35.84 | 10.264 | 161 | 0 | 682 | 682 | 0 | 14.527 | 2023.423 | 2949.119 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 2110 | 2110 | 0 | 15.999 | 2015.231 | 2883.583 |
