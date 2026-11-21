# T3b-mqtt-restart — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.156s, closing 0.981s, drain 90.019s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814640 | 588998 | 12948.351 | 5795.794 | 17898.3 | 680 | 0 | 0 | 431 | 4254 | 228895 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 437118 | 11541.106 | 4362.239 | 32194.559 | 54493.183 | 65077.247 | 70582.271 |
| e2e_from_send | 437118 | 11172.89 | 3887.103 | 31162.367 | 54394.879 | 64847.871 | 70582.271 |
| producer_lag | 642918 | 446.326 | 2.401 | 1740.799 | 4612.095 | 4911.103 | 5193.727 |
| publish_call | 814640 | 71.873 | 40.863 | 134.015 | 429.055 | 5070.847 | 5206.015 |
| queue_wait | 437118 | 11113.124 | 3801.087 | 31145.983 | 54394.879 | 64847.871 | 70582.271 |
| broker_dwell | 437118 | 11055.84 | 3727.359 | 31080.447 | 54362.111 | 64782.335 | 70516.735 |
| receive_call | 522386 | 29.303 | 6.575 | 86.399 | 332.799 | 473.599 | 546.815 |
| ctx_load | 437118 | 6.588 | 2.543 | 17.663 | 48.767 | 90.751 | 176.639 |
| process | 437118 | 11.371 | 0.002 | 0.005 | 0.051 | 2646.015 | 3018.751 |
| ctx_commit | 437118 | 41.762 | 21.151 | 101.951 | 208.255 | 332.287 | 879.103 |
| ack | 435551 | 0.007 | 0.003 | 0.006 | 0.028 | 0.318 | 53.215 |
| service | 437118 | 59.771 | 26.623 | 117.247 | 272.383 | 2689.023 | 3710.975 |
| e2e_large | 319 | 4824.399 | 3643.391 | 7426.047 | 26673.151 | 31817.727 | 31817.727 |
| ready_wait | 437118 | 617.925 | 151.807 | 1056.767 | 6410.239 | 31178.751 | 47677.439 |
| blocked | 437118 | 513.185 | 111.935 | 690.687 | 6406.143 | 30752.767 | 47677.439 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5795.794 | 14471.6 |
| applied_total | 317480.673 | 399916 |
| bench_cpu_cores | 3.567 | 4.695 |
| bench_cpu_pct | 14.864 | 19.56 |
| bench_gc_gen0 | 2.286 | 4 |
| bench_gc_gen1 | 1.939 | 4 |
| bench_gc_gen2 | 0.531 | 1 |
| bench_gc_heap_mb | 965.076 | 1282.5 |
| bench_mem_mb | 1517.106 | 1737.1 |
| bench_threadpool_queue | 39.612 | 491 |
| bench_threadpool_threads | 43.694 | 44 |
| broker_backlog | 136392.143 | 355629 |
| ctr_eb_etcd_cpu_pct | 0.833 | 1.23 |
| ctr_eb_etcd_mem_mb | 29.837 | 31.3 |
| ctr_eb_mosquitto_cpu_pct | 44.53 | 57.47 |
| ctr_eb_mosquitto_mem_mb | 8.445 | 24.9 |
| ctr_eb_pat1_cpu_pct | 57.501 | 86.7 |
| ctr_eb_pat1_mem_mb | 3375.704 | 3429.4 |
| ctr_eb_pat2_cpu_pct | 663.497 | 1065.95 |
| ctr_eb_pat2_mem_mb | 9260.21 | 9613 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.443 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 9.729 | 9.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.661 | 11.9 |
| ctr_eb_postgres_cpu_pct | 1.507 | 2.82 |
| ctr_eb_postgres_mem_mb | 2393.088 | 2393.7 |
| duplicates_per_s | 0.061 | 3 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 54.098 | 81.11 |
| in_flight | 144587.429 | 377432 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.376 | 1.72 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 773798.531 | 1175249 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 164789.327 | 254256 |
| octopus_mqtt_sent_total | 199260.388 | 273191 |
| octopus_publish_retries_total | 823.571 | 1832 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.959 | 19 |
| pg_active_loads | 0.918 | 6 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.979 | 100 |
| pg_conn_active | 34.265 | 82 |
| pg_conn_idle | 233.918 | 267 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.306 | 37 |
| pg_conn_total | 268.184 | 269 |
| pg_db_size_mb | 1602.584 | 2231.9 |
| pg_deadlocks | 2 | 2 |
| pg_replica_lag_mb | 14.004 | 200.11 |
| pg_replica_replay_lag_ms | 37.52 | 497.6 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 16085.547 | 29140.9 |
| pg_tup_del_per_s | 23230.206 | 42962.1 |
| pg_tup_fetch_per_s | 327086.986 | 530558.1 |
| pg_tup_ins_per_s | 44872.061 | 62939.6 |
| pg_tup_upd_per_s | 17297.461 | 33320.3 |
| pg_wal_mb_per_s | 89.403 | 330.09 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12948.351 | 36247.3 |
| produced_total | 462068.102 | 777348 |
| publish_mb_per_s | 44.897 | 118.61 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 7113/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 16,534 records not Completed |
| record_counts | **FAIL** | 92,210 records found, 117,597 expected; 8887 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | **FAIL** | 419 uploads verified, 10 sources mismatched |
| applied_equals_generated | **FAIL** | 588,998 events applied by workers, 814,640 generated |
| broker_drained | **FAIL** | 226,397 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 228,895 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 6406 ms, max 47655 ms) |

Sample mismatches:

- source 544: record 2336462209025 left in status Open (version 6)
- source 74: record 317827579905 left in status Open (version 7)
- source 14529: record 62401579843585 left in status Open (version 7)
- source 7026: record 30176440221697 left in status Open (version 4)
- source 806: record 3461743640577 left in status Open (version 9)
- source 10901: record 46819438493697 left in status Open (version 4)
- source 11656: record 50062138802177 left in status Open (version 18)
- source 4520: record 19413252177921 left in status Open (version 10)
- source 11443: record 49147310768129 left in status Open (version 5)
- source 3711: record 15938623635457 left in status Open (version 4)
- source 12473: record 53571127083009 left in status Open (version 3)
- source 3131: record 13447542603777 left in status Open (version 8)
- source 3347: record 14375255539713 left in status Open (version 9)
- source 3921: record 16840566767617 left in status Open (version 10)
- source 11428: record 49082886258689 left in status Open (version 5)
- source 83: record 356482285569 left in status Open (version 14)
- source 3366: record 14456859918337 left in status Open (version 4)
- source 6094: record 26173530701825 left in status Open (version 5)
- source 7012: record 30116310679553 left in status Open (version 7)
- source 6432: record 27625229647873 left in status Open (version 5)

## Cross-source blocking samples

- [cross_source_blocking] source 12360 seq 4: ready 108 ms before start, 108 ms of it with an idle slot in group 392
- [cross_source_blocking] source 13904 seq 25: ready 108 ms before start, 108 ms of it with an idle slot in group 441
- [cross_source_blocking] source 7164 seq 10: ready 128 ms before start, 128 ms of it with an idle slot in group 243
- [cross_source_blocking] source 9516 seq 8: ready 128 ms before start, 128 ms of it with an idle slot in group 330
- [cross_source_blocking] source 15611 seq 8: ready 199 ms before start, 199 ms of it with an idle slot in group 485
- [cross_source_blocking] source 1740 seq 3: ready 164 ms before start, 164 ms of it with an idle slot in group 50
- [cross_source_blocking] source 7316 seq 8: ready 198 ms before start, 198 ms of it with an idle slot in group 251
- [cross_source_blocking] source 12377 seq 4: ready 133 ms before start, 133 ms of it with an idle slot in group 393
- [cross_source_blocking] source 12392 seq 6: ready 135 ms before start, 135 ms of it with an idle slot in group 393
- [cross_source_blocking] source 12779 seq 4: ready 152 ms before start, 152 ms of it with an idle slot in group 403
- [cross_source_blocking] source 4528 seq 5: ready 156 ms before start, 156 ms of it with an idle slot in group 157
- [cross_source_blocking] source 1269 seq 5: ready 185 ms before start, 152 ms of it with an idle slot in group 40
- [cross_source_blocking] source 5917 seq 11: ready 165 ms before start, 165 ms of it with an idle slot in group 205
- [cross_source_blocking] source 8131 seq 10: ready 197 ms before start, 197 ms of it with an idle slot in group 275
- [cross_source_blocking] source 12383 seq 10: ready 199 ms before start, 199 ms of it with an idle slot in group 393
- [cross_source_blocking] source 8475 seq 16: ready 201 ms before start, 201 ms of it with an idle slot in group 289
- [cross_source_blocking] source 14877 seq 7: ready 140 ms before start, 140 ms of it with an idle slot in group 467
- [cross_source_blocking] source 13213 seq 12: ready 143 ms before start, 143 ms of it with an idle slot in group 421
- [cross_source_blocking] source 2332 seq 18: ready 199 ms before start, 199 ms of it with an idle slot in group 67
- [cross_source_blocking] source 3339 seq 10: ready 152 ms before start, 152 ms of it with an idle slot in group 114

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 12133 | 16 | 37486.591 | 66584.575 | 67633.151 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 5928 | 8 | 28180.479 | 65011.711 | 70778.879 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 9186 | 10 | 21889.023 | 64225.279 | 69730.303 |
| t-182 | 89 | 3 | 81.941 | 119.217 | 2406 | 0 | 7382 | 7382 | 12 | 23592.959 | 63963.135 | 68157.439 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 8 | 26738.687 | 62914.559 | 69206.015 |
| t-121 | 76 | 3 | 97.816 | 119.994 | 149 | 0 | 7236 | 7236 | 12 | 20971.519 | 60293.119 | 67633.151 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 8 | 21757.951 | 60030.975 | 63700.991 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 4980 | 4980 | 8 | 20578.303 | 59244.543 | 63438.847 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 8 | 25034.751 | 58720.255 | 65273.855 |
| t-350 | 85 | 2 | 53.604 | 73.675 | 285 | 0 | 4675 | 4675 | 8 | 20316.159 | 58720.255 | 65535.999 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 10478 | 10478 | 16 | 27131.903 | 57409.535 | 61341.695 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 8037 | 8037 | 11 | 20840.447 | 57147.391 | 61341.695 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 8 | 17039.359 | 57147.391 | 61865.983 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8391 | 8391 | 12 | 24903.679 | 56360.959 | 61603.839 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 2537 | 2537 | 4 | 18481.151 | 56098.815 | 58195.967 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 6 | 15728.639 | 55574.527 | 60817.407 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 4365 | 4365 | 8 | 16056.319 | 55312.383 | 60817.407 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 11 | 13238.271 | 55312.383 | 58982.399 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 5703 | 6 | 9371.647 | 55050.239 | 62652.415 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13792 | 13792 | 20 | 34603.007 | 54525.951 | 57409.535 |
| t-340 | 184 | 2 | 24.502 | 75.472 | 141 | 0 | 4973 | 4973 | 8 | 20840.447 | 54263.807 | 65011.711 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 4 | 22675.455 | 54001.663 | 60817.407 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 8031 | 8031 | 12 | 20578.303 | 54001.663 | 61341.695 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 12 | 24903.679 | 54001.663 | 60817.407 |
| t-210 | 71 | 2 | 57.482 | 65.449 | 136 | 0 | 4177 | 4177 | 7 | 15007.743 | 53739.519 | 61079.551 |
