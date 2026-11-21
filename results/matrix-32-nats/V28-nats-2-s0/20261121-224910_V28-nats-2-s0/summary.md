# V28-nats-2-s0 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 127.0.0.1:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 85.937s, closing 0.938s, drain 12.175s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 849797 | 849797 | 14250.118 | 12868.253 | 22408.7 | 0 | 0 | 0 | 51 | 4103 | 358574 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 688837 | 3511.77 | 2069.503 | 9101.311 | 16097.279 | 20889.599 | 27164.671 |
| e2e_from_send | 688837 | 3083.452 | 1319.935 | 8945.663 | 16039.935 | 20840.447 | 27148.287 |
| producer_lag | 688837 | 428.318 | 55.999 | 1473.535 | 2598.911 | 3358.719 | 3778.559 |
| publish_call | 849797 | 118.247 | 87.423 | 251.135 | 505.855 | 819.199 | 1342.463 |
| queue_wait | 688837 | 3006.469 | 1219.583 | 8880.127 | 15982.591 | 20774.911 | 27131.903 |
| broker_dwell | 688837 | 2916.575 | 1128.447 | 8806.399 | 15900.671 | 20709.375 | 27115.519 |
| receive_call | 743068 | 23.433 | 10.815 | 56.511 | 216.447 | 412.415 | 615.423 |
| ctx_load | 688837 | 16.946 | 11.511 | 36.063 | 87.807 | 229.375 | 350.719 |
| process | 688837 | 9.953 | 0.002 | 0.004 | 0.072 | 2611.199 | 3174.399 |
| ctx_commit | 688837 | 49.8 | 37.183 | 108.031 | 214.399 | 321.279 | 427.775 |
| ack | 688797 | 0.014 | 0.004 | 0.007 | 0.063 | 1.909 | 317.183 |
| service | 688837 | 76.986 | 55.871 | 134.271 | 272.383 | 2678.783 | 3244.031 |
| e2e_large | 44 | 3202.129 | 2838.527 | 4829.183 | 6209.535 | 6209.535 | 6209.535 |
| ready_wait | 688837 | 251.176 | 150.015 | 610.815 | 1404.927 | 2174.975 | 11091.967 |
| blocked | 688837 | 179.675 | 109.695 | 440.063 | 915.455 | 1354.751 | 8962.047 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12868.253 | 18792.4 |
| applied_total | 444779.796 | 750991 |
| bench_cpu_cores | 4.263 | 5.423 |
| bench_cpu_pct | 17.762 | 22.6 |
| bench_gc_gen0 | 1.633 | 4 |
| bench_gc_gen1 | 0.408 | 2 |
| bench_gc_gen2 | 0.327 | 2 |
| bench_gc_heap_mb | 1087.069 | 1548.9 |
| bench_mem_mb | 1634.231 | 2067.4 |
| bench_threadpool_queue | 212.469 | 2014 |
| bench_threadpool_threads | 42.061 | 44 |
| broker_backlog | 46121 | 80713 |
| ctr_eb_mssql_cpu_pct | 4.047 | 4.66 |
| ctr_eb_mssql_mem_mb | 4906.829 | 4909.5 |
| ctr_eb_mysql_cpu_pct | 1.087 | 1.29 |
| ctr_eb_mysql_mem_mb | 5997.7 | 6000.5 |
| ctr_eb_nats_octopus_cpu_pct | 114.65 | 118.45 |
| ctr_eb_nats_octopus_mem_mb | 37.061 | 65.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.593 | 32.66 |
| ctr_eb_pgbouncer_2_mem_mb | 13.055 | 14.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.665 | 32.76 |
| ctr_eb_pgbouncer_3_mem_mb | 15.482 | 16.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 30.017 | 32.57 |
| ctr_eb_pgbouncer_4_mem_mb | 16.029 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 31.423 | 33.46 |
| ctr_eb_pgbouncer_mem_mb | 14.106 | 17.1 |
| ctr_eb_postgres_cpu_pct | 987.585 | 1045.57 |
| ctr_eb_postgres_mem_mb | 5429.733 | 5624.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.424 | 86.77 |
| in_flight | 37743.714 | 70132 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.962 | 2.04 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 912664.551 | 1532590 |
| octopus_missing_reports_total | 46168.878 | 73087 |
| octopus_mqtt_received_total | 176738.286 | 294042 |
| octopus_mqtt_sent_total | 312196.224 | 510033 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.102 | 32 |
| pg_active_loads | 34.918 | 160 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.983 | 100 |
| pg_conn_active | 78.388 | 233 |
| pg_conn_idle | 182.429 | 240 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.51 | 28 |
| pg_conn_total | 260.816 | 262 |
| pg_db_size_mb | 3610.245 | 4218.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26490.524 | 31908.4 |
| pg_tup_del_per_s | 34351.735 | 39799.1 |
| pg_tup_fetch_per_s | 315056.335 | 374315 |
| pg_tup_ins_per_s | 46641.269 | 52938.4 |
| pg_tup_upd_per_s | 58929.188 | 67597.8 |
| pg_wal_mb_per_s | 140.079 | 275.45 |
| pgb_cl_active | 481.939 | 514 |
| pgb_cl_waiting | 19.898 | 139 |
| pgb_maxwait_ms | 2.657 | 30.33 |
| pgb_sv_active | 24.776 | 48 |
| pgb_sv_idle | 23.224 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14250.118 | 20319.1 |
| produced_total | 482523.51 | 821123 |
| publish_mb_per_s | 27.029 | 42.73 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 196,921 records are Completed |
| record_counts | pass | 196,921 records found, 108,796 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 362 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 849,797 events applied by workers, 849,797 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 358,574 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 915 ms, max 8957 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6816 seq 14: ready 190 ms before start, 190 ms of it with an idle slot in group 232
- [cross_source_blocking] source 8887 seq 26: ready 107 ms before start, 106 ms of it with an idle slot in group 306
- [cross_source_blocking] source 2758 seq 33: ready 102 ms before start, 102 ms of it with an idle slot in group 84
- [cross_source_blocking] source 4965 seq 13: ready 139 ms before start, 139 ms of it with an idle slot in group 170
- [cross_source_blocking] source 8293 seq 34: ready 137 ms before start, 137 ms of it with an idle slot in group 282
- [cross_source_blocking] source 9556 seq 22: ready 105 ms before start, 105 ms of it with an idle slot in group 332
- [cross_source_blocking] source 8751 seq 44: ready 191 ms before start, 191 ms of it with an idle slot in group 304
- [cross_source_blocking] source 5640 seq 32: ready 149 ms before start, 148 ms of it with an idle slot in group 194
- [cross_source_blocking] source 9858 seq 7: ready 128 ms before start, 127 ms of it with an idle slot in group 340
- [cross_source_blocking] source 4142 seq 20: ready 124 ms before start, 123 ms of it with an idle slot in group 136
- [cross_source_blocking] source 12166 seq 15: ready 224 ms before start, 200 ms of it with an idle slot in group 388
- [cross_source_blocking] source 12198 seq 11: ready 136 ms before start, 104 ms of it with an idle slot in group 388
- [cross_source_blocking] source 9356 seq 8: ready 153 ms before start, 153 ms of it with an idle slot in group 324
- [cross_source_blocking] source 15839 seq 47: ready 112 ms before start, 112 ms of it with an idle slot in group 496
- [cross_source_blocking] source 11028 seq 23: ready 222 ms before start, 184 ms of it with an idle slot in group 360
- [cross_source_blocking] source 10173 seq 26: ready 222 ms before start, 214 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10145 seq 28: ready 167 ms before start, 165 ms of it with an idle slot in group 344
- [cross_source_blocking] source 11352 seq 9: ready 223 ms before start, 221 ms of it with an idle slot in group 370
- [cross_source_blocking] source 7040 seq 29: ready 197 ms before start, 174 ms of it with an idle slot in group 238
- [cross_source_blocking] source 13804 seq 22: ready 111 ms before start, 111 ms of it with an idle slot in group 438
- [cross_source_blocking] source 14661 seq 24: ready 224 ms before start, 206 ms of it with an idle slot in group 464
- [cross_source_blocking] source 4436 seq 17: ready 223 ms before start, 187 ms of it with an idle slot in group 152

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 2957 | 2957 | 0 | 6914.047 | 22675.455 | 25952.255 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2893 | 2893 | 0 | 7405.567 | 21364.735 | 22675.455 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8789 | 8789 | 0 | 6356.991 | 21364.735 | 25034.751 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17212 | 17212 | 0 | 6062.079 | 21102.591 | 26607.615 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2924 | 2924 | 0 | 6684.671 | 20971.519 | 23986.175 |
| t-260 | 30 | 2 | 180.858 | 93.625 | 499 | 0 | 5606 | 5606 | 0 | 6782.975 | 20840.447 | 24117.247 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2848 | 2848 | 0 | 6193.151 | 20316.159 | 22806.527 |
| t-348 | 125 | 6 | 136.718 | 286.558 | 1340 | 0 | 17626 | 17626 | 0 | 5439.487 | 19922.943 | 24117.247 |
| t-388 | 78 | 2 | 71.428 | 90.661 | 1205 | 0 | 5725 | 5725 | 0 | 5505.023 | 19922.943 | 24772.607 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 20833 | 20833 | 0 | 6193.151 | 19660.799 | 27262.975 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 19068 | 19068 | 0 | 4980.735 | 18481.151 | 24510.463 |
| t-178 | 90 | 2 | 59.386 | 90.002 | 131 | 0 | 5617 | 5617 | 0 | 5505.023 | 18350.079 | 23724.031 |
| t-364 | 66 | 1 | 38.872 | 43.874 | 436 | 0 | 2847 | 2847 | 0 | 3964.927 | 18219.007 | 23855.103 |
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 25854 | 25854 | 0 | 5832.703 | 18219.007 | 22413.311 |
| t-276 | 78 | 1 | 37.161 | 47.91 | 423 | 0 | 3070 | 3070 | 0 | 4423.679 | 17956.863 | 21626.879 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 16550 | 16550 | 0 | 4751.359 | 17694.719 | 20709.375 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5912 | 5912 | 0 | 4816.895 | 17563.647 | 21889.023 |
| t-320 | 14 | 1 | 186.953 | 41.44 | 574 | 0 | 2619 | 2619 | 0 | 5373.951 | 17170.431 | 19267.583 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11532 | 11532 | 0 | 5275.647 | 17170.431 | 19660.799 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 22388 | 22388 | 0 | 4620.287 | 17039.359 | 21495.807 |
| t-474 | 63 | 1 | 39.975 | 41.235 | 375 | 0 | 2636 | 2636 | 0 | 3702.783 | 17039.359 | 20054.015 |
| t-216 | 19 | 1 | 132.344 | 42.802 | 3889 | 0 | 2625 | 2625 | 0 | 5341.183 | 16908.287 | 18743.295 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5717 | 5717 | 0 | 4456.447 | 16777.215 | 20578.303 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2837 | 2837 | 0 | 4816.895 | 16777.215 | 19660.799 |
| t-494 | 35 | 1 | 71.756 | 40.681 | 2036 | 0 | 2545 | 2545 | 0 | 3801.087 | 16580.607 | 20971.519 |
