# W8-minthreads256-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.172s, closing 1.749s, drain 28.601s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1480870 | 1480870 | 24748.314 | 24156.676 | 28093.2 | 0 | 0 | 0 | 425 | 7487 | 280193 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1195443 | 1968.597 | 1445.887 | 3086.335 | 20086.783 | 34832.383 | 44826.623 |
| e2e_from_send | 1195443 | 984.251 | 136.831 | 1797.119 | 19185.663 | 34045.951 | 42762.239 |
| producer_lag | 1195443 | 984.344 | 939.519 | 2209.791 | 2795.519 | 3065.855 | 3434.495 |
| publish_call | 1480870 | 67.007 | 59.359 | 119.103 | 200.831 | 297.471 | 546.303 |
| queue_wait | 1195443 | 947.48 | 107.391 | 1674.239 | 19169.279 | 34045.951 | 42729.471 |
| broker_dwell | 1195443 | 894.729 | 36.159 | 1623.039 | 19120.127 | 34013.183 | 42663.935 |
| receive_call | 1369567 | 22.313 | 0.035 | 61.695 | 272.639 | 470.015 | 564.735 |
| ctx_load | 1195443 | 8.406 | 6.375 | 17.311 | 33.983 | 51.167 | 105.343 |
| process | 1195443 | 10.412 | 0.001 | 0.002 | 0.024 | 2615.295 | 3010.559 |
| ctx_commit | 1195443 | 17.902 | 14.863 | 35.263 | 61.215 | 86.335 | 354.303 |
| ack | 1194592 | 0.009 | 0.003 | 0.006 | 0.034 | 0.801 | 52.639 |
| service | 1195443 | 36.772 | 23.359 | 48.479 | 86.591 | 2641.919 | 3336.191 |
| e2e_large | 324 | 3525.836 | 3444.735 | 5079.039 | 7167.999 | 8286.207 | 8286.207 |
| ready_wait | 1195443 | 94.991 | 31.247 | 246.143 | 893.439 | 1547.263 | 2228.223 |
| blocked | 1195443 | 92.594 | 28.815 | 242.303 | 891.903 | 1546.239 | 2228.223 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24156.676 | 28093.2 |
| applied_total | 825878.755 | 1404340 |
| bench_cpu_cores | 4.942 | 5.808 |
| bench_cpu_pct | 20.591 | 24.2 |
| bench_gc_gen0 | 3.857 | 6 |
| bench_gc_gen1 | 3.816 | 5 |
| bench_gc_gen2 | 0.531 | 2 |
| bench_gc_heap_mb | 1546.592 | 1955 |
| bench_mem_mb | 2057.696 | 2309.6 |
| bench_threadpool_queue | 106.939 | 508 |
| bench_threadpool_threads | 130.184 | 257 |
| broker_backlog | 18451.653 | 29989 |
| ctr_eb_mosquitto_cpu_pct | 67.246 | 68.94 |
| ctr_eb_mosquitto_mem_mb | 10.002 | 30.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 24.897 | 27.02 |
| ctr_eb_pgbouncer_2_mem_mb | 15.529 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 26.927 | 30.11 |
| ctr_eb_pgbouncer_3_mem_mb | 16.476 | 17.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.908 | 29.04 |
| ctr_eb_pgbouncer_4_mem_mb | 16.031 | 17.2 |
| ctr_eb_pgbouncer_cpu_pct | 25.86 | 28.23 |
| ctr_eb_pgbouncer_mem_mb | 15.927 | 17.2 |
| ctr_eb_postgres_cpu_pct | 950.139 | 989.13 |
| ctr_eb_postgres_mem_mb | 5534.796 | 5667.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.631 | 82.28 |
| in_flight | 18757.571 | 34998 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.599 | 1.69 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1621812.694 | 2760884 |
| octopus_mqtt_received_total | 158420.02 | 262651 |
| octopus_mqtt_sent_total | 334414.959 | 575876 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.816 | 15 |
| pg_active_loads | 9.939 | 31 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.976 | 100 |
| pg_conn_active | 29.184 | 57 |
| pg_conn_idle | 198.163 | 212 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 3.306 | 20 |
| pg_conn_total | 227.347 | 228 |
| pg_db_size_mb | 3731.171 | 4208.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 27594.929 | 32329.8 |
| pg_tup_del_per_s | 57932.867 | 68511.2 |
| pg_tup_fetch_per_s | 372304.51 | 436141.1 |
| pg_tup_ins_per_s | 70980.339 | 90725.3 |
| pg_tup_upd_per_s | 64922.022 | 70421.2 |
| pg_wal_mb_per_s | 140.608 | 238.14 |
| pgb_cl_active | 240.122 | 268 |
| pgb_cl_waiting | 9.714 | 80 |
| pgb_maxwait_ms | 1.278 | 12.6 |
| pgb_sv_active | 21.837 | 48 |
| pgb_sv_idle | 26.163 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24748.314 | 28096.1 |
| produced_total | 844636.327 | 1439338 |
| publish_mb_per_s | 66.3 | 86.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,150 records are Completed |
| record_counts | pass | 193,150 records found, 193,150 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,480,870 events applied by workers, 1,480,870 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 280,193 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 892 ms, max 2227 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9116 seq 17: ready 161 ms before start, 161 ms of it with an idle slot in group 315
- [cross_source_blocking] source 12956 seq 18: ready 208 ms before start, 208 ms of it with an idle slot in group 409
- [cross_source_blocking] source 12636 seq 6: ready 204 ms before start, 204 ms of it with an idle slot in group 397
- [cross_source_blocking] source 7036 seq 15: ready 126 ms before start, 123 ms of it with an idle slot in group 238
- [cross_source_blocking] source 5916 seq 17: ready 209 ms before start, 205 ms of it with an idle slot in group 205
- [cross_source_blocking] source 4796 seq 13: ready 204 ms before start, 204 ms of it with an idle slot in group 167
- [cross_source_blocking] source 4476 seq 9: ready 205 ms before start, 205 ms of it with an idle slot in group 154
- [cross_source_blocking] source 11612 seq 13: ready 125 ms before start, 125 ms of it with an idle slot in group 377
- [cross_source_blocking] source 6332 seq 21: ready 207 ms before start, 207 ms of it with an idle slot in group 215
- [cross_source_blocking] source 10236 seq 18: ready 205 ms before start, 205 ms of it with an idle slot in group 344
- [cross_source_blocking] source 12892 seq 12: ready 205 ms before start, 205 ms of it with an idle slot in group 408
- [cross_source_blocking] source 12252 seq 7: ready 206 ms before start, 206 ms of it with an idle slot in group 389
- [cross_source_blocking] source 5052 seq 13: ready 211 ms before start, 211 ms of it with an idle slot in group 176
- [cross_source_blocking] source 7932 seq 13: ready 206 ms before start, 142 ms of it with an idle slot in group 268
- [cross_source_blocking] source 6844 seq 6: ready 163 ms before start, 146 ms of it with an idle slot in group 233
- [cross_source_blocking] source 15324 seq 16: ready 169 ms before start, 125 ms of it with an idle slot in group 479
- [cross_source_blocking] source 8668 seq 8: ready 214 ms before start, 185 ms of it with an idle slot in group 300
- [cross_source_blocking] source 15740 seq 10: ready 206 ms before start, 206 ms of it with an idle slot in group 491
- [cross_source_blocking] source 15388 seq 30: ready 214 ms before start, 177 ms of it with an idle slot in group 481
- [cross_source_blocking] source 11996 seq 22: ready 142 ms before start, 142 ms of it with an idle slot in group 383

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25288 | 25288 | 0 | 21757.951 | 38797.311 | 44826.623 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 18940 | 18940 | 0 | 6225.919 | 20316.159 | 29229.055 |
| t-315 | 88 | 4 | 117.565 | 172.184 | 2927 | 0 | 10485 | 10485 | 0 | 5341.183 | 20054.015 | 23724.031 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17908 | 17908 | 0 | 4751.359 | 19791.871 | 26476.543 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9089 | 9089 | 0 | 3833.855 | 16121.855 | 18219.007 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 11254 | 11254 | 0 | 4489.215 | 14352.383 | 18874.367 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30605 | 30605 | 0 | 3227.647 | 14352.383 | 21626.879 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29328 | 29328 | 0 | 2686.975 | 13893.631 | 27000.831 |
| t-131 | 140 | 6 | 113.781 | 268.058 | 961 | 0 | 16473 | 16473 | 0 | 2785.279 | 12451.839 | 19922.943 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 3080.191 | 8224.767 | 8224.767 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 22120 | 22120 | 0 | 2015.231 | 7798.783 | 10551.295 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 9441 | 9441 | 0 | 2072.575 | 7077.887 | 8912.895 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 2965.503 | 6553.599 | 8290.303 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15031 | 15031 | 0 | 1810.431 | 6291.455 | 9633.791 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 2801.663 | 6160.383 | 6193.151 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 8048 | 8048 | 0 | 1744.895 | 5996.543 | 7602.175 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 2736.127 | 5799.935 | 6193.151 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22399 | 22399 | 0 | 1859.583 | 5636.095 | 7438.335 |
| t-239 | 39 | 3 | 196.652 | 124.486 | 225 | 0 | 7542 | 7542 | 0 | 1638.399 | 5210.111 | 9109.503 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11893 | 11893 | 0 | 1638.399 | 5177.343 | 9961.471 |
| t-417 | 75 | 3 | 82.418 | 102.437 | 3449 | 0 | 6167 | 6167 | 0 | 1540.095 | 5144.575 | 7405.567 |
| t-222 | 138 | 6 | 115.927 | 269.807 | 2846 | 0 | 16345 | 16345 | 0 | 1654.783 | 5046.271 | 8847.359 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5725 | 5725 | 0 | 1630.207 | 4849.663 | 6225.919 |
| t-197 | 42 | 3 | 193.698 | 131.933 | 725 | 0 | 7848 | 7848 | 0 | 1507.327 | 4653.055 | 6651.903 |
| t-079 | 6 | 1 | 180.755 | 18.586 | 1522 | 0 | 1118 | 1118 | 0 | 1564.671 | 4554.751 | 6160.383 |
