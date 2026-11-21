# T24-2shards-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.356s, closing 0.927s, drain 7.774s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 733291 | 733291 | 11919.22 | 10780.165 | 22623.3 | 0 | 0 | 0 | 46 | 3558 | 340709 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 591978 | 3427.98 | 1262.591 | 10043.391 | 17874.943 | 23412.735 | 30621.695 |
| e2e_from_send | 591978 | 3387.524 | 1180.671 | 10027.007 | 17858.559 | 23396.351 | 30605.311 |
| producer_lag | 591978 | 40.456 | 3.701 | 78.911 | 698.879 | 1388.543 | 1711.103 |
| publish_call | 733291 | 105.32 | 78.527 | 219.263 | 446.719 | 671.231 | 1013.759 |
| queue_wait | 591978 | 3320.302 | 1094.655 | 9969.663 | 17793.023 | 23314.431 | 30572.543 |
| broker_dwell | 591978 | 3254.253 | 1020.927 | 9912.319 | 17743.871 | 23248.895 | 30457.855 |
| receive_call | 616726 | 28.962 | 14.183 | 70.847 | 230.527 | 450.815 | 609.791 |
| ctx_load | 591978 | 17.143 | 11.887 | 37.183 | 87.103 | 124.095 | 239.743 |
| process | 591978 | 10.046 | 0.002 | 0.004 | 0.077 | 2611.199 | 3061.759 |
| ctx_commit | 591978 | 39.765 | 31.007 | 85.119 | 155.391 | 235.263 | 423.423 |
| ack | 591933 | 0.014 | 0.004 | 0.007 | 0.059 | 1.647 | 94.143 |
| service | 591978 | 67.223 | 49.087 | 112.575 | 213.119 | 2670.591 | 3289.087 |
| e2e_large | 39 | 3434.338 | 3262.463 | 4878.335 | 6598.655 | 6598.655 | 6598.655 |
| ready_wait | 591978 | 287.667 | 170.111 | 712.703 | 1503.231 | 2018.303 | 2832.383 |
| blocked | 591978 | 232.185 | 141.183 | 577.535 | 1121.279 | 1449.983 | 1691.647 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10780.165 | 14422.2 |
| applied_total | 388995.449 | 647097 |
| bench_cpu_cores | 4.339 | 5.538 |
| bench_cpu_pct | 18.081 | 23.07 |
| bench_gc_gen0 | 1.673 | 4 |
| bench_gc_gen1 | 0.633 | 2 |
| bench_gc_gen2 | 0.449 | 2 |
| bench_gc_heap_mb | 790.778 | 1255.1 |
| bench_mem_mb | 1221.722 | 1561.7 |
| bench_threadpool_queue | 125.367 | 656 |
| bench_threadpool_threads | 42.143 | 44 |
| broker_backlog | 46572.939 | 72887 |
| ctr_eb_mosquitto_cpu_pct | 83.823 | 88.1 |
| ctr_eb_mosquitto_mem_mb | 9.588 | 17.8 |
| ctr_eb_mssql_cpu_pct | 4.271 | 5.2 |
| ctr_eb_mssql_mem_mb | 7444.4 | 7444.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.667 | 32.09 |
| ctr_eb_pgbouncer_2_mem_mb | 20.818 | 21.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 31.168 | 32.51 |
| ctr_eb_pgbouncer_3_mem_mb | 20.445 | 21.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 31.856 | 34.18 |
| ctr_eb_pgbouncer_4_mem_mb | 20.976 | 21.8 |
| ctr_eb_pgbouncer_cpu_pct | 31.85 | 34.14 |
| ctr_eb_pgbouncer_mem_mb | 20.447 | 20.8 |
| ctr_eb_postgres_cpu_pct | 1002.895 | 1041.17 |
| ctr_eb_postgres_mem_mb | 5883.892 | 6104.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.533 | 86.1 |
| in_flight | 36567.878 | 63237 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.072 | 2.25 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 806561.857 | 1359555 |
| octopus_missing_reports_total | 33727.082 | 50624 |
| octopus_mqtt_received_total | 186124.531 | 306475 |
| octopus_mqtt_sent_total | 274580.918 | 454386 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 13 | 24 |
| pg_active_loads | 9.714 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.988 | 100 |
| pg_conn_active | 49.633 | 80 |
| pg_conn_idle | 211.673 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.837 | 9 |
| pg_conn_total | 261.306 | 262 |
| pg_db_size_mb | 3667.051 | 4140.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24668.331 | 28498.5 |
| pg_tup_del_per_s | 33224.527 | 40027 |
| pg_tup_fetch_per_s | 291186.451 | 355967.1 |
| pg_tup_ins_per_s | 44888.814 | 54385.8 |
| pg_tup_upd_per_s | 54464.276 | 61196.2 |
| pg_wal_mb_per_s | 124.319 | 168.23 |
| pgb_cl_active | 444.265 | 522 |
| pgb_cl_waiting | 43.245 | 248 |
| pgb_maxwait_ms | 5.35 | 33.86 |
| pgb_sv_active | 23.122 | 48 |
| pgb_sv_idle | 24.878 | 46 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11919.22 | 13986 |
| produced_total | 425563.327 | 710334 |
| publish_mb_per_s | 22.786 | 39.59 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 185,182 records are Completed |
| record_counts | pass | 185,182 records found, 95,820 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 383 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 733,291 events applied by workers, 733,291 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 340,709 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1121 ms, max 1692 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9464 seq 63: ready 120 ms before start, 120 ms of it with an idle slot in group 326
- [cross_source_blocking] source 3829 seq 12: ready 117 ms before start, 117 ms of it with an idle slot in group 130
- [cross_source_blocking] source 3830 seq 18: ready 121 ms before start, 121 ms of it with an idle slot in group 130
- [cross_source_blocking] source 2198 seq 14: ready 112 ms before start, 112 ms of it with an idle slot in group 64
- [cross_source_blocking] source 2000 seq 15: ready 147 ms before start, 147 ms of it with an idle slot in group 58
- [cross_source_blocking] source 11343 seq 9: ready 124 ms before start, 124 ms of it with an idle slot in group 370
- [cross_source_blocking] source 10318 seq 12: ready 155 ms before start, 151 ms of it with an idle slot in group 346
- [cross_source_blocking] source 9030 seq 9: ready 154 ms before start, 154 ms of it with an idle slot in group 312
- [cross_source_blocking] source 5433 seq 35: ready 158 ms before start, 158 ms of it with an idle slot in group 186
- [cross_source_blocking] source 1616 seq 7: ready 123 ms before start, 123 ms of it with an idle slot in group 48
- [cross_source_blocking] source 8623 seq 31: ready 122 ms before start, 122 ms of it with an idle slot in group 298
- [cross_source_blocking] source 7800 seq 13: ready 189 ms before start, 102 ms of it with an idle slot in group 264
- [cross_source_blocking] source 2756 seq 31: ready 128 ms before start, 128 ms of it with an idle slot in group 84
- [cross_source_blocking] source 6114 seq 15: ready 195 ms before start, 131 ms of it with an idle slot in group 210
- [cross_source_blocking] source 13018 seq 8: ready 222 ms before start, 106 ms of it with an idle slot in group 414
- [cross_source_blocking] source 10911 seq 13: ready 222 ms before start, 146 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10167 seq 14: ready 176 ms before start, 110 ms of it with an idle slot in group 344
- [cross_source_blocking] source 8741 seq 37: ready 175 ms before start, 104 ms of it with an idle slot in group 304
- [cross_source_blocking] source 3204 seq 65: ready 110 ms before start, 109 ms of it with an idle slot in group 106
- [cross_source_blocking] source 5212 seq 13: ready 102 ms before start, 100 ms of it with an idle slot in group 180

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-450 | 63 | 3 | 141.433 | 146.465 | 165 | 0 | 8988 | 8988 | 0 | 6946.815 | 25165.823 | 28311.551 |
| t-348 | 125 | 5 | 117.187 | 245.621 | 1340 | 0 | 15155 | 15155 | 0 | 8126.463 | 24117.247 | 30670.847 |
| t-066 | 38 | 1 | 77.996 | 49.178 | 277 | 0 | 3060 | 3060 | 0 | 8781.823 | 23986.175 | 27918.335 |
| t-132 | 22 | 2 | 280.528 | 95.394 | 2676 | 0 | 5739 | 5739 | 0 | 9109.503 | 23330.815 | 27131.903 |
| t-152 | 43 | 1 | 70.072 | 49.467 | 882 | 0 | 3090 | 3090 | 0 | 8585.215 | 22806.527 | 25165.823 |
| t-104 | 46 | 1 | 59.3 | 45.479 | 2163 | 0 | 2825 | 2825 | 0 | 6225.919 | 22675.455 | 29097.983 |
| t-184 | 20 | 1 | 147.97 | 48.078 | 161 | 0 | 2932 | 2932 | 0 | 8191.999 | 22282.239 | 25296.895 |
| t-040 | 119 | 2 | 48.767 | 98.209 | 1254 | 0 | 6218 | 6218 | 0 | 7438.335 | 22151.167 | 26345.471 |
| t-120 | 37 | 2 | 154.992 | 94.103 | 1412 | 0 | 5765 | 5765 | 0 | 6619.135 | 22020.095 | 25427.967 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5557 | 5557 | 0 | 5603.327 | 21889.023 | 24379.391 |
| t-172 | 20 | 1 | 136.825 | 48.083 | 2589 | 0 | 2948 | 2948 | 0 | 7176.191 | 21495.807 | 23330.815 |
| t-438 | 121 | 6 | 138.912 | 290.853 | 1839 | 0 | 17705 | 17705 | 0 | 6619.135 | 21495.807 | 26214.399 |
| t-350 | 85 | 3 | 99.704 | 137.035 | 285 | 0 | 8592 | 8592 | 0 | 5832.703 | 20840.447 | 25952.255 |
| t-464 | 153 | 5 | 92.631 | 233.046 | 352 | 0 | 14436 | 14436 | 0 | 4816.895 | 20316.159 | 26083.327 |
| t-230 | 30 | 2 | 194.092 | 96.091 | 1033 | 0 | 5847 | 5847 | 0 | 6553.599 | 20185.087 | 23986.175 |
| t-460 | 71 | 1 | 38.182 | 45.554 | 141 | 0 | 2967 | 2967 | 0 | 6553.599 | 20185.087 | 25296.895 |
| t-376 | 65 | 1 | 43.345 | 48.199 | 566 | 0 | 3087 | 3087 | 0 | 4849.663 | 19529.727 | 26607.615 |
| t-032 | 52 | 1 | 53.698 | 45.402 | 1041 | 0 | 2918 | 2918 | 0 | 4521.983 | 19267.583 | 24903.679 |
| t-194 | 78 | 5 | 182.864 | 244.07 | 1184 | 0 | 14816 | 14816 | 0 | 6750.207 | 19136.511 | 23724.031 |
| t-340 | 184 | 3 | 45.575 | 140.377 | 141 | 0 | 8825 | 8825 | 0 | 5373.951 | 19005.439 | 25296.895 |
| t-246 | 32 | 4 | 349.163 | 184.479 | 494 | 0 | 11413 | 11413 | 0 | 6455.295 | 18874.367 | 20447.231 |
| t-446 | 49 | 1 | 55.848 | 44.043 | 196 | 0 | 2864 | 2864 | 0 | 5439.487 | 18874.367 | 21889.023 |
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8066 | 8066 | 0 | 5701.631 | 18743.295 | 19791.871 |
| t-378 | 55 | 3 | 159.715 | 143.096 | 3659 | 0 | 8705 | 8705 | 0 | 5308.415 | 18612.223 | 23986.175 |
| t-488 | 26 | 1 | 107.285 | 47.505 | 1017 | 0 | 2899 | 2899 | 0 | 5537.791 | 18612.223 | 19922.943 |
