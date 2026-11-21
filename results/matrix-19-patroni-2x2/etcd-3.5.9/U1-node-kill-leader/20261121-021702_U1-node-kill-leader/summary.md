# U1-node-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.258s, closing 2.1s, drain 79.849s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 599548 | 599548 | 8648.673 | 4954.776 | 18748 | 0 | 2782 | 2782 | 325 | 3029 | 154368 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 429360 | 32358.14 | 35520.511 | 53379.071 | 60981.247 | 77070.335 | 93388.799 |
| e2e_from_send | 429360 | 25004.348 | 23166.975 | 51609.599 | 59047.935 | 61439.999 | 75235.327 |
| producer_lag | 429666 | 7348.752 | 342.015 | 18202.623 | 18907.135 | 19283.967 | 19644.415 |
| publish_call | 599548 | 173.965 | 106.687 | 223.871 | 563.199 | 16474.111 | 18219.007 |
| queue_wait | 429360 | 24904.332 | 23134.207 | 51544.063 | 59015.167 | 61407.231 | 75169.791 |
| broker_dwell | 429360 | 24782.876 | 22970.367 | 51478.527 | 58949.631 | 61341.695 | 75169.791 |
| receive_call | 510349 | 29.17 | 6.167 | 95.423 | 351.743 | 493.311 | 563.711 |
| ctx_load | 429360 | 5.375 | 2.567 | 11.239 | 38.143 | 74.367 | 15007.743 |
| process | 429360 | 10.625 | 0.002 | 0.004 | 0.035 | 2625.535 | 3000.319 |
| ctx_commit | 429360 | 83.985 | 30.879 | 241.535 | 566.271 | 842.239 | 15015.935 |
| ack | 427313 | 0.007 | 0.003 | 0.005 | 0.024 | 0.313 | 61.983 |
| service | 429360 | 100.029 | 36.191 | 257.919 | 630.783 | 2760.703 | 17776.639 |
| e2e_large | 224 | 9161.246 | 3540.991 | 21086.207 | 22970.367 | 25067.519 | 25067.519 |
| ready_wait | 429360 | 1173.233 | 80.959 | 1767.423 | 34111.487 | 45121.535 | 59473.919 |
| blocked | 429360 | 823.195 | 42.623 | 900.607 | 25100.287 | 35815.423 | 51511.295 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4954.776 | 13479.6 |
| applied_total | 192157.388 | 309387 |
| bench_cpu_cores | 1.997 | 4.099 |
| bench_cpu_pct | 8.32 | 17.08 |
| bench_gc_gen0 | 1.837 | 5 |
| bench_gc_gen1 | 1.755 | 5 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 993.153 | 1294.8 |
| bench_mem_mb | 1537.271 | 1674.7 |
| bench_threadpool_queue | 37.857 | 788 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 169799.184 | 243743 |
| ctr_eb_dc1a_cpu_pct | 732.29 | 794.56 |
| ctr_eb_dc1a_mem_mb | 7575.124 | 8149.1 |
| ctr_eb_dc1b_cpu_pct | 72.298 | 290.44 |
| ctr_eb_dc1b_mem_mb | 3145.059 | 3280.6 |
| ctr_eb_dc2a_cpu_pct | 72.217 | 286.59 |
| ctr_eb_dc2a_mem_mb | 3168.808 | 3305.4 |
| ctr_eb_dc2b_cpu_pct | 236.356 | 976.83 |
| ctr_eb_dc2b_mem_mb | 3493.4 | 5467.4 |
| ctr_eb_etcd_dc1_cpu_pct | 1.571 | 3.02 |
| ctr_eb_etcd_dc1_mem_mb | 34.38 | 35.5 |
| ctr_eb_etcd_dc2_cpu_pct | 1.309 | 2.18 |
| ctr_eb_etcd_dc2_mem_mb | 29.569 | 31.5 |
| ctr_eb_etcd_w_cpu_pct | 1.289 | 2.36 |
| ctr_eb_etcd_w_mem_mb | 24.473 | 25.4 |
| ctr_eb_mosquitto_cpu_pct | 26.417 | 49.75 |
| ctr_eb_mosquitto_mem_mb | 6.661 | 12.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.026 | 0.07 |
| ctr_eb_pgbouncer_2_mem_mb | 9.3 | 9.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.022 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.756 | 2.63 |
| ctr_eb_postgres_mem_mb | 2213.112 | 2213.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 56.733 | 1734.6 |
| host_cpu_pct | 48.703 | 90.52 |
| in_flight | 173076.082 | 252420 |
| nacks_per_s | 56.733 | 1734.6 |
| octopus_dispatcher_cores | 1.015 | 1.6 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 579776.51 | 892138 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 164761.327 | 233271 |
| octopus_mqtt_sent_total | 124182.653 | 198955 |
| octopus_publish_retries_total | 10717.469 | 21751 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.918 | 21 |
| pg_active_loads | 1.041 | 9 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 56.429 | 76 |
| pg_conn_idle | 211.796 | 235 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 268.224 | 269 |
| pg_db_size_mb | 1490.763 | 1654.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 21.502 | 31.37 |
| pg_replica_replay_lag_ms | 141.367 | 198.7 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 17015.255 | 21379.6 |
| pg_tup_del_per_s | 26402.02 | 38506.5 |
| pg_tup_fetch_per_s | 583744.912 | 658498 |
| pg_tup_ins_per_s | 44054.682 | 67894.9 |
| pg_tup_upd_per_s | 18365.345 | 23287.5 |
| pg_wal_mb_per_s | 83.213 | 121.41 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 8648.673 | 20517.1 |
| produced_total | 365233.469 | 561807 |
| publish_mb_per_s | 30.321 | 78.58 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 92,402 records are Completed |
| record_counts | pass | 92,402 records found, 92,402 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 325 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 599,548 events applied by workers, 599,548 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 154,368 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 25100 ms, max 51486 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9468 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 327
- [cross_source_blocking] source 8216 seq 4: ready 113 ms before start, 113 ms of it with an idle slot in group 277
- [cross_source_blocking] source 5868 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 203
- [cross_source_blocking] source 4792 seq 2: ready 113 ms before start, 113 ms of it with an idle slot in group 166
- [cross_source_blocking] source 7404 seq 12: ready 111 ms before start, 111 ms of it with an idle slot in group 256
- [cross_source_blocking] source 7104 seq 6: ready 113 ms before start, 113 ms of it with an idle slot in group 241
- [cross_source_blocking] source 708 seq 3: ready 113 ms before start, 113 ms of it with an idle slot in group 21
- [cross_source_blocking] source 5848 seq 5: ready 113 ms before start, 113 ms of it with an idle slot in group 201
- [cross_source_blocking] source 6816 seq 10: ready 113 ms before start, 113 ms of it with an idle slot in group 232
- [cross_source_blocking] source 14156 seq 6: ready 112 ms before start, 112 ms of it with an idle slot in group 451
- [cross_source_blocking] source 8572 seq 14: ready 331 ms before start, 102 ms of it with an idle slot in group 295
- [cross_source_blocking] source 2432 seq 9: ready 481 ms before start, 102 ms of it with an idle slot in group 71
- [cross_source_blocking] source 12352 seq 5: ready 506 ms before start, 101 ms of it with an idle slot in group 392
- [cross_source_blocking] source 8917 seq 7: ready 192 ms before start, 190 ms of it with an idle slot in group 307
- [cross_source_blocking] source 2357 seq 7: ready 192 ms before start, 192 ms of it with an idle slot in group 68
- [cross_source_blocking] source 7252 seq 7: ready 121 ms before start, 121 ms of it with an idle slot in group 247
- [cross_source_blocking] source 3033 seq 3: ready 105 ms before start, 105 ms of it with an idle slot in group 101
- [cross_source_blocking] source 1612 seq 4: ready 127 ms before start, 127 ms of it with an idle slot in group 48
- [cross_source_blocking] source 2574 seq 3: ready 109 ms before start, 109 ms of it with an idle slot in group 78
- [cross_source_blocking] source 4012 seq 5: ready 121 ms before start, 121 ms of it with an idle slot in group 134

## Error samples

- t-186#1 src=5435 seq=29 op=Get rec=23343147253765 ev=2: NpgsqlException: Exception while reading from stream
- t-019#1 src=657 seq=25 op=Update rec=2821793513477 ev=2: NpgsqlException: Exception while reading from stream
- t-464#2 src=14760 seq=10 op=Update rec=63393717288962 ev=4: NpgsqlException: Exception while reading from stream
- t-052#0 src=1859 seq=8 op=Get rec=7984344203266 ev=1: NpgsqlException: Exception while reading from stream
- t-019#1 src=647 seq=26 op=Update rec=2778843840518 ev=2: NpgsqlException: Exception while reading from stream
- t-194#2 src=5617 seq=22 op=Update rec=24124831301633 ev=14: NpgsqlException: Exception while reading from stream
- t-180#0 src=5211 seq=21 op=Update rec=22381074579458 ev=10: NpgsqlException: Exception while reading from stream
- t-128#0 src=3797 seq=40 op=Complete rec=16307990822920 ev=1: NpgsqlException: Exception while reading from stream
- t-121#1 src=3535 seq=20 op=Update rec=15182709391363 ev=6: NpgsqlException: Exception while reading from stream
- t-263#1 src=7697 seq=10 op=Get rec=33058363277316 ev=1: NpgsqlException: Exception while reading from stream
- t-263#1 src=7777 seq=10 op=Get rec=33401960660994 ev=2: NpgsqlException: Exception while reading from stream
- t-019#1 src=669 seq=27 op=Complete rec=2873333121028 ev=4: NpgsqlException: Exception while reading from stream
- t-210#0 src=6144 seq=14 op=Update rec=26388279066628 ev=1: NpgsqlException: No suitable host was found.
- t-270#0 src=8011 seq=27 op=Update rec=34406983008261 ev=3: NpgsqlException: No suitable host was found.
- t-344#4 src=10179 seq=16 op=Complete rec=43718472105988 ev=5: NpgsqlException: No suitable host was found.
- t-035#0 src=1088 seq=21 op=Update rec=4672924418051 ev=2: NpgsqlException: No suitable host was found.
- t-191#0 src=5558 seq=32 op=Update rec=23871428231172 ev=5: NpgsqlException: No suitable host was found.
- t-476#0 src=15250 seq=22 op=Update rec=65498251264004 ev=2: NpgsqlException: No suitable host was found.
- t-437#0 src=13734 seq=23 op=Update rec=58987080843268 ev=1: NpgsqlException: No suitable host was found.
- t-173#0 src=5010 seq=20 op=Update rec=21517786152963 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 6551 | 6551 | 0 | 57933.823 | 78643.199 | 80216.063 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 8924 | 8924 | 0 | 59768.831 | 78643.199 | 93847.551 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2238 | 2238 | 0 | 56885.247 | 62128.127 | 70778.879 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 6140 | 6140 | 0 | 57409.535 | 61865.983 | 80216.063 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2231 | 2231 | 0 | 39321.599 | 60817.407 | 62652.415 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2242 | 2242 | 0 | 55574.527 | 60817.407 | 62128.127 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2141 | 2141 | 0 | 56098.815 | 60555.263 | 60817.407 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2122 | 2122 | 0 | 39583.743 | 60555.263 | 63700.991 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3949 | 3949 | 0 | 55836.671 | 60293.119 | 70254.591 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1971 | 1971 | 0 | 55574.527 | 60030.975 | 60817.407 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4403 | 4403 | 0 | 39845.887 | 59768.831 | 61603.839 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6679 | 6679 | 0 | 54525.951 | 59768.831 | 61865.983 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4675 | 4675 | 0 | 40108.031 | 59506.687 | 61603.839 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4693 | 4693 | 0 | 40370.175 | 59244.543 | 61341.695 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2004 | 2004 | 0 | 38797.311 | 58720.255 | 61603.839 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4343 | 4343 | 0 | 39845.887 | 58720.255 | 61341.695 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2163 | 2163 | 0 | 38273.023 | 58458.111 | 59244.543 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2154 | 2154 | 0 | 40632.319 | 58195.967 | 59244.543 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1941 | 1941 | 0 | 54788.095 | 58195.967 | 59244.543 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2149 | 2149 | 0 | 38273.023 | 58195.967 | 58982.399 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 6362 | 6362 | 0 | 38797.311 | 58195.967 | 60030.975 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 12301 | 12301 | 0 | 53215.231 | 57933.823 | 60030.975 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 1922 | 1922 | 0 | 53739.519 | 57671.679 | 58720.255 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 3618 | 3618 | 0 | 53215.231 | 57671.679 | 58458.111 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 11893 | 11893 | 0 | 52953.087 | 57671.679 | 59244.543 |
