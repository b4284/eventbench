# W4-worker-commit-per-event-fast-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.245s, closing 2.171s, drain 9.8s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1110687 | 1110687 | 18297.51 | 18045.99 | 20056.1 | 649 | 0 | 0 | 338 | 5592 | 263444 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 826119 | 11248.689 | 11272.191 | 15491.071 | 20627.455 | 27377.663 | 34701.311 |
| e2e_from_send | 826119 | 546.432 | 196.735 | 1087.487 | 6918.143 | 13893.631 | 22904.831 |
| producer_lag | 826119 | 10702.263 | 10870.783 | 14811.135 | 16482.303 | 17203.199 | 17678.335 |
| publish_call | 1110687 | 99.44 | 86.079 | 180.991 | 308.991 | 412.159 | 536.575 |
| queue_wait | 826119 | 496.079 | 154.367 | 984.575 | 6885.375 | 13860.863 | 22822.911 |
| broker_dwell | 826119 | 425.007 | 58.911 | 931.839 | 6836.223 | 13819.903 | 22740.991 |
| receive_call | 875390 | 35.757 | 6.515 | 101.311 | 328.703 | 474.111 | 559.615 |
| ctx_load | 826119 | 19.268 | 15.431 | 39.903 | 73.855 | 97.087 | 233.087 |
| process | 826119 | 10.458 | 0.001 | 0.003 | 0.026 | 2611.199 | 3006.463 |
| ctx_commit | 826119 | 20.553 | 15.975 | 43.039 | 79.039 | 138.111 | 317.951 |
| ack | 825853 | 0.011 | 0.003 | 0.007 | 0.037 | 0.765 | 42.975 |
| service | 826119 | 50.354 | 33.663 | 77.183 | 148.351 | 2656.255 | 3231.743 |
| e2e_large | 237 | 12677.872 | 12476.415 | 16924.671 | 18874.367 | 19021.823 | 19021.823 |
| ready_wait | 826119 | 111.546 | 59.807 | 276.223 | 771.583 | 1376.255 | 2273.279 |
| blocked | 826119 | 104.781 | 54.783 | 259.199 | 754.687 | 1340.415 | 2273.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 18045.99 | 19826.3 |
| applied_total | 615433.633 | 1054788 |
| bench_cpu_cores | 3.98 | 4.848 |
| bench_cpu_pct | 16.584 | 20.2 |
| bench_gc_gen0 | 3.673 | 5 |
| bench_gc_gen1 | 3.633 | 5 |
| bench_gc_gen2 | 0.673 | 2 |
| bench_gc_heap_mb | 988.649 | 1219.3 |
| bench_mem_mb | 1433.331 | 1532.3 |
| bench_threadpool_queue | 148.959 | 972 |
| bench_threadpool_threads | 42.02 | 44 |
| broker_backlog | 7779.02 | 15171 |
| ctr_eb_mosquitto_cpu_pct | 49.948 | 52.12 |
| ctr_eb_mosquitto_mem_mb | 7.531 | 15.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 36.724 | 38.5 |
| ctr_eb_pgbouncer_2_mem_mb | 15.806 | 16.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 35.83 | 37.9 |
| ctr_eb_pgbouncer_3_mem_mb | 15.686 | 16.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 36.016 | 38.42 |
| ctr_eb_pgbouncer_4_mem_mb | 15.806 | 16.8 |
| ctr_eb_pgbouncer_cpu_pct | 36.742 | 38.66 |
| ctr_eb_pgbouncer_mem_mb | 16.549 | 17.8 |
| ctr_eb_postgres_cpu_pct | 1123.145 | 1162.36 |
| ctr_eb_postgres_mem_mb | 5629.639 | 5703.9 |
| duplicates_per_s | 10.224 | 72 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 80.987 | 84.43 |
| in_flight | 7146.082 | 15564 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.099 | 1.16 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1222681.408 | 2145529 |
| octopus_mqtt_received_total | 100956.776 | 163756 |
| octopus_mqtt_sent_total | 31749.469 | 54592 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 38.429 | 146 |
| pg_active_loads | 10.51 | 30 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 66.51 | 163 |
| pg_conn_idle | 161 | 211 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.449 | 4 |
| pg_conn_total | 227.51 | 228 |
| pg_db_size_mb | 3367.32 | 3836.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 37109.443 | 42688 |
| pg_tup_del_per_s | 43886.614 | 51217.3 |
| pg_tup_fetch_per_s | 372391.7 | 399702.5 |
| pg_tup_ins_per_s | 54198.222 | 64801.1 |
| pg_tup_upd_per_s | 48623.206 | 53867.7 |
| pg_wal_mb_per_s | 106.707 | 156.99 |
| pgb_cl_active | 217.041 | 256 |
| pgb_cl_waiting | 35.327 | 173 |
| pgb_maxwait_ms | 2.718 | 12.16 |
| pgb_sv_active | 26.898 | 48 |
| pgb_sv_idle | 21.102 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 18297.51 | 24709.5 |
| produced_total | 622579.714 | 1069264 |
| publish_mb_per_s | 50.944 | 77.75 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 151,335 records are Completed |
| record_counts | pass | 151,335 records found, 151,335 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 338 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,110,687 events applied by workers, 1,110,687 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 263,444 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 755 ms, max 2271 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15965 seq 35: ready 141 ms before start, 141 ms of it with an idle slot in group 499
- [cross_source_blocking] source 9021 seq 7: ready 140 ms before start, 140 ms of it with an idle slot in group 312
- [cross_source_blocking] source 7421 seq 18: ready 155 ms before start, 155 ms of it with an idle slot in group 257
- [cross_source_blocking] source 11005 seq 5: ready 155 ms before start, 121 ms of it with an idle slot in group 359
- [cross_source_blocking] source 14717 seq 24: ready 141 ms before start, 141 ms of it with an idle slot in group 464
- [cross_source_blocking] source 10493 seq 22: ready 140 ms before start, 140 ms of it with an idle slot in group 348
- [cross_source_blocking] source 15037 seq 21: ready 155 ms before start, 155 ms of it with an idle slot in group 469
- [cross_source_blocking] source 5245 seq 58: ready 137 ms before start, 137 ms of it with an idle slot in group 181
- [cross_source_blocking] source 10077 seq 16: ready 198 ms before start, 198 ms of it with an idle slot in group 343
- [cross_source_blocking] source 15709 seq 28: ready 197 ms before start, 197 ms of it with an idle slot in group 490
- [cross_source_blocking] source 11261 seq 8: ready 199 ms before start, 199 ms of it with an idle slot in group 366
- [cross_source_blocking] source 3325 seq 7: ready 198 ms before start, 198 ms of it with an idle slot in group 113
- [cross_source_blocking] source 3229 seq 26: ready 196 ms before start, 196 ms of it with an idle slot in group 108
- [cross_source_blocking] source 6909 seq 23: ready 198 ms before start, 198 ms of it with an idle slot in group 235
- [cross_source_blocking] source 6301 seq 8: ready 199 ms before start, 199 ms of it with an idle slot in group 214
- [cross_source_blocking] source 3517 seq 33: ready 199 ms before start, 199 ms of it with an idle slot in group 120
- [cross_source_blocking] source 14621 seq 7: ready 199 ms before start, 199 ms of it with an idle slot in group 462
- [cross_source_blocking] source 3617 seq 31: ready 198 ms before start, 198 ms of it with an idle slot in group 122
- [cross_source_blocking] source 5025 seq 17: ready 199 ms before start, 199 ms of it with an idle slot in group 175
- [cross_source_blocking] source 669 seq 26: ready 200 ms before start, 200 ms of it with an idle slot in group 19

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 18856 | 18856 | 5 | 15859.711 | 31326.207 | 34865.151 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 9249 | 9249 | 2 | 14942.207 | 26607.615 | 28311.551 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 10199 | 10199 | 1 | 11927.551 | 25296.895 | 27787.263 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 16574 | 16574 | 4 | 12124.159 | 22675.455 | 24510.463 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 8384 | 8384 | 5 | 12189.695 | 21757.951 | 24379.391 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 13444 | 13444 | 13 | 11665.407 | 21626.879 | 25690.111 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 11202 | 11202 | 2 | 12058.623 | 21495.807 | 24510.463 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 6014 | 6014 | 7 | 12845.055 | 21364.735 | 22020.095 |
| t-121 | 76 | 5 | 182.916 | 224.389 | 149 | 0 | 10200 | 10200 | 7 | 11272.191 | 20447.231 | 22806.527 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 7325 | 7325 | 3 | 11468.799 | 20185.087 | 20840.447 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 4277 | 4277 | 1 | 11796.479 | 19660.799 | 20840.447 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 16659 | 16659 | 5 | 11927.551 | 19660.799 | 22020.095 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 6856 | 6856 | 2 | 12189.695 | 19529.727 | 20578.303 |
| t-461 | 130 | 4 | 84.209 | 183.191 | 3025 | 0 | 8422 | 8422 | 4 | 11534.335 | 19398.655 | 23461.887 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 73 | 73 | 0 | 12124.159 | 19005.439 | 19005.439 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 308 | 308 | 0 | 12517.375 | 18874.367 | 19136.511 |
| t-131 | 140 | 6 | 113.781 | 268.058 | 961 | 0 | 12269 | 12269 | 10 | 11468.799 | 18481.151 | 21364.735 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 14025 | 14025 | 7 | 11403.263 | 18481.151 | 21495.807 |
| t-310 | 6 | 1 | 131.802 | 13.811 | 1910 | 0 | 670 | 670 | 0 | 10944.511 | 18350.079 | 19267.583 |
| t-315 | 88 | 4 | 117.565 | 172.184 | 2927 | 0 | 7892 | 7892 | 8 | 11468.799 | 18350.079 | 21364.735 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 14500 | 14500 | 13 | 11599.871 | 18350.079 | 21233.663 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 5750 | 5750 | 2 | 11993.087 | 18087.935 | 19267.583 |
| t-483 | 200 | 6 | 76.95 | 259.099 | 2470 | 0 | 12005 | 12005 | 3 | 11599.871 | 18087.935 | 19791.871 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 1023 | 1023 | 0 | 10944.511 | 17956.863 | 18219.007 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 2125 | 2125 | 1 | 11534.335 | 17825.791 | 19398.655 |
