# V28-mosq-1-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.468s, closing 1.099s, drain 44.328s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 839627 | 839627 | 14375.602 | 12036.441 | 27759.7 | 0 | 0 | 0 | 51 | 4053 | 406364 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 678652 | 6552.584 | 5001.215 | 13295.615 | 20496.383 | 28983.295 | 48791.551 |
| e2e_from_send | 678652 | 3911.522 | 1635.327 | 11255.807 | 18300.927 | 27705.343 | 45711.359 |
| producer_lag | 678652 | 2641.06 | 2848.767 | 4521.983 | 5697.535 | 6184.959 | 6443.007 |
| publish_call | 839627 | 143.487 | 116.991 | 271.871 | 511.487 | 1069.055 | 1604.607 |
| queue_wait | 678652 | 3841.43 | 1541.119 | 11198.463 | 18268.159 | 27672.575 | 45711.359 |
| broker_dwell | 678652 | 3737.879 | 1441.791 | 11108.351 | 18186.239 | 27623.423 | 45678.591 |
| receive_call | 717259 | 27.858 | 12.879 | 66.751 | 232.831 | 486.655 | 560.639 |
| ctx_load | 678652 | 15.789 | 10.287 | 35.359 | 83.903 | 164.095 | 354.303 |
| process | 678652 | 9.951 | 0.002 | 0.004 | 0.069 | 2611.199 | 3248.127 |
| ctx_commit | 678652 | 44.084 | 34.975 | 91.775 | 180.351 | 329.983 | 635.903 |
| ack | 678456 | 0.012 | 0.004 | 0.007 | 0.057 | 1.189 | 268.543 |
| service | 678652 | 70.094 | 52.063 | 118.079 | 258.175 | 2668.543 | 3371.007 |
| e2e_large | 44 | 5848.029 | 5742.591 | 8142.847 | 8724.479 | 8724.479 | 8724.479 |
| ready_wait | 678652 | 289.522 | 176.639 | 693.759 | 1557.503 | 2611.199 | 10461.183 |
| blocked | 678652 | 237.859 | 147.199 | 571.391 | 1197.055 | 1831.935 | 9994.239 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12036.441 | 15741.2 |
| applied_total | 414021.143 | 699056 |
| bench_cpu_cores | 4.002 | 5.057 |
| bench_cpu_pct | 16.673 | 21.07 |
| bench_gc_gen0 | 1.98 | 3 |
| bench_gc_gen1 | 0.694 | 2 |
| bench_gc_gen2 | 0.571 | 1 |
| bench_gc_heap_mb | 744.941 | 1230 |
| bench_mem_mb | 1246.012 | 1629.4 |
| bench_threadpool_queue | 191.837 | 1795 |
| bench_threadpool_threads | 41.449 | 44 |
| broker_backlog | 50968.612 | 123278 |
| ctr_eb_mosquitto_cpu_pct | 82.525 | 86.74 |
| ctr_eb_mosquitto_mem_mb | 11.345 | 23.1 |
| ctr_eb_mssql_cpu_pct | 6.841 | 25.36 |
| ctr_eb_mssql_mem_mb | 4926.682 | 4951.5 |
| ctr_eb_mysql_cpu_pct | 1.074 | 1.19 |
| ctr_eb_mysql_mem_mb | 6059.027 | 6073 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.692 | 32.82 |
| ctr_eb_pgbouncer_2_mem_mb | 12.706 | 13.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 31.57 | 34.26 |
| ctr_eb_pgbouncer_3_mem_mb | 14.433 | 16.5 |
| ctr_eb_pgbouncer_4_cpu_pct | 30.648 | 33.28 |
| ctr_eb_pgbouncer_4_mem_mb | 13.182 | 15.7 |
| ctr_eb_pgbouncer_cpu_pct | 31.114 | 34.65 |
| ctr_eb_pgbouncer_mem_mb | 13.273 | 14.5 |
| ctr_eb_postgres_cpu_pct | 1025.382 | 1076.61 |
| ctr_eb_postgres_mem_mb | 4661.973 | 5291.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 82.415 | 85.65 |
| in_flight | 42439.449 | 115535 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.007 | 2.11 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 861064.204 | 1537074 |
| octopus_missing_reports_total | 46333.816 | 76838 |
| octopus_mqtt_received_total | 174092.367 | 301351 |
| octopus_mqtt_sent_total | 290045.49 | 485018 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.857 | 32 |
| pg_active_loads | 10.837 | 39 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.995 | 100 |
| pg_conn_active | 61.816 | 103 |
| pg_conn_idle | 198.531 | 232 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.429 | 27 |
| pg_conn_total | 260.347 | 262 |
| pg_db_size_mb | 3566.335 | 4232.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25451.447 | 27614.8 |
| pg_tup_del_per_s | 33015.624 | 39842.9 |
| pg_tup_fetch_per_s | 307024.712 | 383101.2 |
| pg_tup_ins_per_s | 45727.053 | 51838.4 |
| pg_tup_upd_per_s | 55704.773 | 62460 |
| pg_wal_mb_per_s | 122.601 | 162.42 |
| pgb_cl_active | 485.265 | 518 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 13.408 | 24 |
| pgb_sv_idle | 34.592 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 14375.602 | 21105 |
| produced_total | 456460.592 | 814591 |
| publish_mb_per_s | 27.246 | 39.78 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 191,954 records are Completed |
| record_counts | pass | 191,954 records found, 107,655 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 347 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 839,627 events applied by workers, 839,627 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 406,364 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1197 ms, max 9993 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4148 seq 13: ready 320 ms before start, 300 ms of it with an idle slot in group 136
- [cross_source_blocking] source 10614 seq 25: ready 277 ms before start, 268 ms of it with an idle slot in group 348
- [cross_source_blocking] source 13333 seq 8: ready 275 ms before start, 275 ms of it with an idle slot in group 426
- [cross_source_blocking] source 9679 seq 16: ready 322 ms before start, 322 ms of it with an idle slot in group 336
- [cross_source_blocking] source 9864 seq 13: ready 277 ms before start, 277 ms of it with an idle slot in group 340
- [cross_source_blocking] source 9768 seq 7: ready 320 ms before start, 320 ms of it with an idle slot in group 340
- [cross_source_blocking] source 10550 seq 23: ready 274 ms before start, 266 ms of it with an idle slot in group 348
- [cross_source_blocking] source 4116 seq 21: ready 273 ms before start, 253 ms of it with an idle slot in group 136
- [cross_source_blocking] source 11433 seq 17: ready 322 ms before start, 322 ms of it with an idle slot in group 372
- [cross_source_blocking] source 11254 seq 5: ready 322 ms before start, 322 ms of it with an idle slot in group 366
- [cross_source_blocking] source 2316 seq 22: ready 322 ms before start, 322 ms of it with an idle slot in group 66
- [cross_source_blocking] source 5295 seq 33: ready 321 ms before start, 321 ms of it with an idle slot in group 182
- [cross_source_blocking] source 14465 seq 9: ready 277 ms before start, 277 ms of it with an idle slot in group 460
- [cross_source_blocking] source 8081 seq 31: ready 237 ms before start, 237 ms of it with an idle slot in group 272
- [cross_source_blocking] source 10518 seq 16: ready 229 ms before start, 221 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10151 seq 34: ready 321 ms before start, 321 ms of it with an idle slot in group 344
- [cross_source_blocking] source 13071 seq 10: ready 321 ms before start, 291 ms of it with an idle slot in group 416
- [cross_source_blocking] source 15646 seq 23: ready 270 ms before start, 270 ms of it with an idle slot in group 488
- [cross_source_blocking] source 14141 seq 22: ready 276 ms before start, 276 ms of it with an idle slot in group 450
- [cross_source_blocking] source 2636 seq 20: ready 269 ms before start, 269 ms of it with an idle slot in group 80

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-496 | 81 | 9 | 315.994 | 429.566 | 278 | 0 | 25546 | 25546 | 0 | 10158.079 | 33423.359 | 49020.927 |
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 2897 | 2897 | 0 | 10813.439 | 25034.751 | 27000.831 |
| t-018 | 26 | 1 | 110.475 | 48.57 | 315 | 0 | 2844 | 2844 | 0 | 10092.543 | 24510.463 | 26214.399 |
| t-344 | 195 | 12 | 177.764 | 584.983 | 571 | 0 | 35081 | 35081 | 0 | 10878.975 | 24510.463 | 35389.439 |
| t-348 | 125 | 6 | 136.718 | 286.558 | 1340 | 0 | 17427 | 17427 | 0 | 8519.679 | 23986.175 | 28835.839 |
| t-474 | 63 | 1 | 39.975 | 41.235 | 375 | 0 | 2606 | 2606 | 0 | 7733.247 | 23461.887 | 27394.047 |
| t-210 | 71 | 3 | 124.735 | 142.024 | 136 | 0 | 8681 | 8681 | 0 | 8585.215 | 22937.599 | 27918.335 |
| t-260 | 30 | 2 | 180.858 | 93.625 | 499 | 0 | 5536 | 5536 | 0 | 8716.287 | 22413.311 | 27787.263 |
| t-194 | 78 | 6 | 213.341 | 284.749 | 1184 | 0 | 17049 | 17049 | 0 | 8716.287 | 22151.167 | 27131.903 |
| t-234 | 21 | 1 | 127.636 | 43.478 | 141 | 0 | 2697 | 2697 | 0 | 6979.583 | 22020.095 | 23724.031 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 2942 | 2942 | 0 | 7372.799 | 21889.023 | 25296.895 |
| t-070 | 24 | 1 | 113.313 | 45.458 | 2303 | 0 | 2798 | 2798 | 0 | 8585.215 | 21757.951 | 24510.463 |
| t-126 | 93 | 2 | 60.358 | 94.482 | 816 | 0 | 5850 | 5850 | 0 | 7667.711 | 21626.879 | 27131.903 |
| t-468 | 61 | 6 | 275.652 | 275.704 | 1645 | 0 | 16350 | 16350 | 0 | 8126.463 | 21626.879 | 26214.399 |
| t-208 | 21 | 1 | 127.251 | 46.372 | 915 | 0 | 2804 | 2804 | 0 | 7864.319 | 21495.807 | 22544.383 |
| t-438 | 121 | 7 | 162.064 | 339.328 | 1839 | 0 | 20587 | 20587 | 0 | 8781.823 | 21102.591 | 26345.471 |
| t-016 | 132 | 2 | 41.122 | 89.402 | 137 | 0 | 5649 | 5649 | 0 | 7110.655 | 20840.447 | 26869.759 |
| t-222 | 138 | 7 | 134.525 | 313.091 | 2846 | 0 | 18839 | 18839 | 0 | 7569.407 | 20840.447 | 25690.111 |
| t-464 | 153 | 6 | 108.069 | 271.887 | 352 | 0 | 16584 | 16584 | 0 | 7471.103 | 20840.447 | 24248.319 |
| t-490 | 56 | 4 | 203.724 | 191.456 | 245 | 0 | 11395 | 11395 | 0 | 8781.823 | 20840.447 | 22937.599 |
| t-276 | 78 | 1 | 37.161 | 47.91 | 423 | 0 | 3032 | 3032 | 0 | 6914.047 | 20709.375 | 23724.031 |
| t-306 | 134 | 8 | 163.07 | 364.658 | 2978 | 0 | 22107 | 22107 | 0 | 8650.751 | 20709.375 | 24117.247 |
| t-364 | 66 | 1 | 38.872 | 43.874 | 436 | 0 | 2824 | 2824 | 0 | 7012.351 | 20709.375 | 26214.399 |
| t-168 | 71 | 2 | 75.117 | 87.222 | 155 | 0 | 5509 | 5509 | 0 | 6914.047 | 20578.303 | 23855.103 |
| t-388 | 78 | 2 | 71.428 | 90.661 | 1205 | 0 | 5670 | 5670 | 0 | 7503.871 | 20578.303 | 24772.607 |
