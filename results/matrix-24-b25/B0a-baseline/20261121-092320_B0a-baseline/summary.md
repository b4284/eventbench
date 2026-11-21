# B0a-baseline — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.15s, closing 2.381s, drain 6.56s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 999792 | 999792 | 16387.483 | 15287.079 | 22942.4 | 0 | 0 | 0 | 458 | 5158 | 473016 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796421 | 2735.897 | 1683.455 | 7143.423 | 14909.439 | 19595.263 | 26787.839 |
| e2e_from_send | 796421 | 2139.012 | 546.815 | 6856.703 | 14589.951 | 19251.199 | 24887.295 |
| producer_lag | 796421 | 596.882 | 167.679 | 1697.791 | 2758.655 | 3414.015 | 4050.943 |
| publish_call | 999792 | 91.554 | 70.911 | 182.399 | 387.071 | 718.335 | 948.223 |
| queue_wait | 796421 | 2075.66 | 479.487 | 6799.359 | 14540.799 | 19218.431 | 24870.911 |
| broker_dwell | 796421 | 2011.667 | 407.039 | 6746.111 | 14491.647 | 19152.895 | 24756.223 |
| receive_call | 827004 | 36.908 | 14.079 | 96.127 | 333.823 | 478.207 | 650.239 |
| ctx_load | 796421 | 14.119 | 9.967 | 29.807 | 66.943 | 107.839 | 299.263 |
| process | 796421 | 10.758 | 0.002 | 0.004 | 0.069 | 2623.487 | 3047.423 |
| ctx_commit | 796421 | 38.261 | 31.343 | 76.863 | 134.143 | 226.303 | 1040.383 |
| ack | 796394 | 0.011 | 0.004 | 0.007 | 0.047 | 0.954 | 210.175 |
| service | 796421 | 63.352 | 46.143 | 98.879 | 181.119 | 2682.879 | 3977.215 |
| e2e_large | 356 | 3575.889 | 3356.671 | 5181.439 | 7426.047 | 8355.839 | 8355.839 |
| ready_wait | 796421 | 316.053 | 190.207 | 775.167 | 1728.511 | 2584.575 | 4044.799 |
| blocked | 796421 | 254.816 | 156.799 | 642.047 | 1201.151 | 1914.879 | 2689.023 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15287.079 | 20131.1 |
| applied_total | 530515.417 | 886654 |
| bench_cpu_cores | 4.937 | 6.901 |
| bench_cpu_pct | 20.571 | 28.75 |
| bench_gc_gen0 | 2.146 | 4 |
| bench_gc_gen1 | 0.688 | 2 |
| bench_gc_gen2 | 0.583 | 1 |
| bench_gc_heap_mb | 1442.402 | 1847.8 |
| bench_mem_mb | 2204.823 | 2452.7 |
| bench_threadpool_queue | 134 | 1245 |
| bench_threadpool_threads | 41.375 | 44 |
| broker_backlog | 31916.604 | 62388 |
| ctr_eb_mosquitto_cpu_pct | 65.614 | 69.62 |
| ctr_eb_mosquitto_mem_mb | 7.804 | 17.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.699 | 25.43 |
| ctr_eb_pgbouncer_2_mem_mb | 15.617 | 16 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.103 | 21.34 |
| ctr_eb_pgbouncer_3_mem_mb | 15.492 | 17.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.781 | 23.6 |
| ctr_eb_pgbouncer_4_mem_mb | 15.648 | 16.4 |
| ctr_eb_pgbouncer_cpu_pct | 21.148 | 22.62 |
| ctr_eb_pgbouncer_mem_mb | 15.773 | 16.4 |
| ctr_eb_postgres_cpu_pct | 959.176 | 1037.67 |
| ctr_eb_postgres_mem_mb | 4883.708 | 5216.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.157 | 80.89 |
| in_flight | 30033.188 | 59696 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.755 | 1.83 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1070624.5 | 1814544 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 186091.854 | 320257 |
| octopus_mqtt_sent_total | 297726.75 | 500083 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.417 | 15 |
| pg_active_loads | 6.292 | 36 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 36.312 | 73 |
| pg_conn_idle | 222.208 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.146 | 17 |
| pg_conn_total | 258.521 | 260 |
| pg_db_size_mb | 3621.288 | 4145.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18742.821 | 21436.7 |
| pg_tup_del_per_s | 42841.265 | 51448.1 |
| pg_tup_fetch_per_s | 451665.438 | 544384.3 |
| pg_tup_ins_per_s | 57053.535 | 70525.1 |
| pg_tup_upd_per_s | 40872.108 | 47879.7 |
| pg_wal_mb_per_s | 119.71 | 212.07 |
| pgb_cl_active | 247.688 | 270 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 14.375 | 39 |
| pgb_sv_idle | 33.625 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16387.483 | 24179.1 |
| produced_total | 560548.604 | 945935 |
| publish_mb_per_s | 54.029 | 86.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,799 records are Completed |
| record_counts | pass | 138,799 records found, 138,799 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 999,792 events applied by workers, 999,792 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 473,016 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1201 ms, max 2687 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11036 seq 14: ready 245 ms before start, 245 ms of it with an idle slot in group 360
- [cross_source_blocking] source 13372 seq 12: ready 247 ms before start, 231 ms of it with an idle slot in group 428
- [cross_source_blocking] source 14620 seq 7: ready 246 ms before start, 246 ms of it with an idle slot in group 462
- [cross_source_blocking] source 2012 seq 12: ready 249 ms before start, 249 ms of it with an idle slot in group 59
- [cross_source_blocking] source 13180 seq 6: ready 217 ms before start, 217 ms of it with an idle slot in group 419
- [cross_source_blocking] source 2332 seq 21: ready 246 ms before start, 246 ms of it with an idle slot in group 67
- [cross_source_blocking] source 2940 seq 9: ready 247 ms before start, 247 ms of it with an idle slot in group 94
- [cross_source_blocking] source 7900 seq 9: ready 194 ms before start, 157 ms of it with an idle slot in group 265
- [cross_source_blocking] source 10620 seq 13: ready 175 ms before start, 175 ms of it with an idle slot in group 349
- [cross_source_blocking] source 9660 seq 16: ready 248 ms before start, 212 ms of it with an idle slot in group 334
- [cross_source_blocking] source 2396 seq 10: ready 251 ms before start, 225 ms of it with an idle slot in group 70
- [cross_source_blocking] source 11580 seq 5: ready 252 ms before start, 252 ms of it with an idle slot in group 376
- [cross_source_blocking] source 8700 seq 24: ready 247 ms before start, 247 ms of it with an idle slot in group 302
- [cross_source_blocking] source 9532 seq 8: ready 247 ms before start, 247 ms of it with an idle slot in group 331
- [cross_source_blocking] source 5948 seq 5: ready 205 ms before start, 190 ms of it with an idle slot in group 206
- [cross_source_blocking] source 5181 seq 8: ready 168 ms before start, 168 ms of it with an idle slot in group 179
- [cross_source_blocking] source 8221 seq 6: ready 171 ms before start, 171 ms of it with an idle slot in group 277
- [cross_source_blocking] source 7990 seq 21: ready 135 ms before start, 100 ms of it with an idle slot in group 269
- [cross_source_blocking] source 1948 seq 22: ready 117 ms before start, 105 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1309 seq 43: ready 126 ms before start, 126 ms of it with an idle slot in group 41

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3193 | 3193 | 0 | 5242.879 | 21495.807 | 25690.111 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9091 | 9091 | 0 | 5865.471 | 20578.303 | 26869.759 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9216 | 9216 | 0 | 5734.399 | 20316.159 | 26083.327 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2960 | 2960 | 0 | 5603.327 | 19922.943 | 24510.463 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15129 | 15129 | 0 | 5996.543 | 19922.943 | 24379.391 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5825 | 5825 | 0 | 4325.375 | 19398.655 | 24510.463 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5627 | 5627 | 0 | 4718.591 | 19267.583 | 22806.527 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5973 | 5973 | 0 | 5177.343 | 19267.583 | 22413.311 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6090 | 6090 | 0 | 4718.591 | 18874.367 | 24772.607 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11843 | 11843 | 0 | 4915.199 | 18743.295 | 22413.311 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3096 | 3096 | 0 | 5046.271 | 18350.079 | 19791.871 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14666 | 14666 | 0 | 5144.575 | 17956.863 | 22020.095 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2834 | 2834 | 0 | 3932.159 | 17825.791 | 20578.303 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5914 | 5914 | 0 | 5701.631 | 17694.719 | 21495.807 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5533 | 5533 | 0 | 4849.663 | 17563.647 | 19005.439 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19752 | 19752 | 0 | 4259.839 | 17301.503 | 22282.239 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 4456.447 | 17170.431 | 19791.871 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11125 | 11125 | 0 | 4718.591 | 17039.359 | 22020.095 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20582 | 20582 | 0 | 4423.679 | 17039.359 | 20185.087 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2848 | 2848 | 0 | 3883.007 | 16777.215 | 21233.663 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5552 | 5552 | 0 | 4014.079 | 16711.679 | 19529.727 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8296 | 8296 | 0 | 4112.383 | 16515.071 | 18481.151 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3099 | 3099 | 0 | 4128.767 | 16515.071 | 19005.439 |
| t-055 | 65 | 3 | 125.803 | 130.203 | 359 | 0 | 8042 | 8042 | 0 | 3932.159 | 16252.927 | 19791.871 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2908 | 2908 | 0 | 3489.791 | 15990.783 | 18219.007 |
