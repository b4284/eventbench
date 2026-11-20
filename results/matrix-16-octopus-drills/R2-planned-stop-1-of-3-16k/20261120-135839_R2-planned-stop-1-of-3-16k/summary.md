# R2-planned-stop-1-of-3-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.118s, closing 1.467s, drain 1.703s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000526 | 1000526 | 15952.024 | 15987.292 | 22249.2 | 0 | 0 | 0 | 458 | 5162 | 18813 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797160 | 119.901 | 30.591 | 100.671 | 2641.919 | 3805.183 | 6983.679 |
| e2e_from_send | 797160 | 116.43 | 28.655 | 94.271 | 2623.487 | 3747.839 | 6983.679 |
| producer_lag | 797160 | 3.471 | 1.291 | 5.023 | 22.943 | 99.519 | 5058.559 |
| publish_call | 1000526 | 29.698 | 15.519 | 65.407 | 208.767 | 402.943 | 5070.847 |
| queue_wait | 797160 | 92.234 | 17.039 | 63.743 | 2480.127 | 3667.967 | 6143.999 |
| broker_dwell | 797160 | 78.496 | 6.079 | 30.927 | 2467.839 | 3639.295 | 6135.807 |
| receive_call | 900526 | 38.523 | 19.503 | 90.303 | 309.759 | 462.847 | 520.447 |
| ctx_load | 797160 | 5.37 | 4.013 | 10.615 | 23.967 | 38.815 | 86.847 |
| process | 797160 | 10.732 | 0.002 | 0.004 | 0.041 | 2617.343 | 3002.367 |
| ctx_commit | 797160 | 8.061 | 5.691 | 16.831 | 38.367 | 59.071 | 324.351 |
| ack | 796399 | 0.006 | 0.003 | 0.005 | 0.027 | 0.296 | 24.303 |
| service | 797160 | 24.197 | 10.431 | 26.799 | 60.319 | 2631.679 | 3074.047 |
| e2e_large | 356 | 2493.755 | 2482.175 | 3327.999 | 4505.599 | 5537.791 | 5537.791 |
| ready_wait | 797160 | 26.238 | 3.437 | 21.663 | 215.679 | 3297.279 | 5070.847 |
| blocked | 797160 | 25.122 | 3.435 | 21.151 | 209.919 | 3141.631 | 5066.751 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15987.292 | 21109.1 |
| applied_total | 577391.796 | 961251 |
| bench_cpu_cores | 5.042 | 5.521 |
| bench_cpu_pct | 21.006 | 23 |
| bench_gc_gen0 | 3.184 | 4 |
| bench_gc_gen1 | 2.694 | 4 |
| bench_gc_gen2 | 0.551 | 1 |
| bench_gc_heap_mb | 1009.722 | 1292.1 |
| bench_mem_mb | 1532.437 | 1672.7 |
| bench_threadpool_queue | 44.449 | 245 |
| bench_threadpool_threads | 39.429 | 44 |
| broker_backlog | 3486.735 | 15476 |
| ctr_eb_mosquitto_cpu_pct | 61.84 | 68.66 |
| ctr_eb_mosquitto_mem_mb | 6.753 | 9.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.178 | 22.56 |
| ctr_eb_pgbouncer_2_mem_mb | 15.443 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.5 | 21.82 |
| ctr_eb_pgbouncer_3_mem_mb | 15.471 | 16.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 19.882 | 22.12 |
| ctr_eb_pgbouncer_4_mem_mb | 15.582 | 16.4 |
| ctr_eb_pgbouncer_cpu_pct | 19.521 | 21.41 |
| ctr_eb_pgbouncer_mem_mb | 15.843 | 16.9 |
| ctr_eb_postgres_cpu_pct | 775.518 | 903.36 |
| ctr_eb_postgres_mem_mb | 5992.857 | 6173.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 71.105 | 78.79 |
| in_flight | 1574.49 | 12628 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.719 | 1.87 |
| octopus_dispatchers_alive | 2.449 | 3 |
| octopus_items_sent_total | 1127251.041 | 1966722 |
| octopus_mqtt_received_total | 173363.714 | 280173 |
| octopus_mqtt_sent_total | 349234.265 | 627824 |
| octopus_publish_retries_total | 14.571 | 34 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 2.184 | 10 |
| pg_active_loads | 3.918 | 16 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 11.082 | 22 |
| pg_conn_idle | 235.245 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.306 | 5 |
| pg_conn_total | 246.327 | 259 |
| pg_db_size_mb | 3585.09 | 3932 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21117.541 | 23726.2 |
| pg_tup_del_per_s | 44261.576 | 51730.9 |
| pg_tup_fetch_per_s | 413948.678 | 607208.1 |
| pg_tup_ins_per_s | 55628.539 | 70914.7 |
| pg_tup_upd_per_s | 42319.706 | 49091.9 |
| pg_wal_mb_per_s | 108.981 | 137.46 |
| pgb_cl_active | 248.653 | 257 |
| pgb_cl_waiting | 0.551 | 27 |
| pgb_maxwait_ms | 0.017 | 0.81 |
| pgb_sv_active | 8.857 | 48 |
| pgb_sv_idle | 39.143 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15952.024 | 16598.4 |
| produced_total | 578966.286 | 961832 |
| publish_mb_per_s | 51.87 | 77.07 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,526 events applied by workers, 1,000,526 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,813 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 210 ms, max 5067 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4337 seq 32: ready 107 ms before start, 104 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4346 seq 24: ready 105 ms before start, 103 ms of it with an idle slot in group 147
- [cross_source_blocking] source 15445 seq 6: ready 125 ms before start, 108 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15491 seq 10: ready 132 ms before start, 115 ms of it with an idle slot in group 483
- [cross_source_blocking] source 4334 seq 32: ready 116 ms before start, 116 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4302 seq 38: ready 115 ms before start, 115 ms of it with an idle slot in group 147
- [cross_source_blocking] source 15537 seq 11: ready 127 ms before start, 115 ms of it with an idle slot in group 483
- [cross_source_blocking] source 4312 seq 27: ready 111 ms before start, 111 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4321 seq 25: ready 127 ms before start, 125 ms of it with an idle slot in group 147
- [cross_source_blocking] source 15428 seq 15: ready 138 ms before start, 122 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15423 seq 8: ready 123 ms before start, 115 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10204 seq 19: ready 105 ms before start, 105 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10234 seq 24: ready 105 ms before start, 105 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10117 seq 19: ready 104 ms before start, 104 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10253 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10265 seq 18: ready 127 ms before start, 127 ms of it with an idle slot in group 344
- [cross_source_blocking] source 15891 seq 24: ready 104 ms before start, 104 ms of it with an idle slot in group 496
- [cross_source_blocking] source 15882 seq 24: ready 103 ms before start, 103 ms of it with an idle slot in group 496
- [cross_source_blocking] source 15473 seq 17: ready 120 ms before start, 119 ms of it with an idle slot in group 483
- [cross_source_blocking] source 6542 seq 13: ready 192 ms before start, 182 ms of it with an idle slot in group 222

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1998.847 | 5373.951 | 5537.791 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1761.279 | 4390.911 | 4521.983 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 33.535 | 4325.375 | 5439.487 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2064.383 | 4292.607 | 5308.415 |
| t-304 | 19 | 1 | 129.878 | 40.323 | 238 | 0 | 2460 | 2460 | 0 | 34.815 | 4014.079 | 5275.647 |
| t-458 | 132 | 1 | 19.452 | 42.195 | 2384 | 0 | 2867 | 2867 | 0 | 32.383 | 4014.079 | 4423.679 |
| t-461 | 130 | 3 | 55.839 | 121.474 | 3025 | 0 | 7598 | 7598 | 0 | 31.871 | 3899.391 | 7012.351 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 33.279 | 3801.087 | 4718.591 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1966.079 | 3784.703 | 4521.983 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20598 | 20598 | 0 | 32.255 | 3686.399 | 6094.847 |
| t-428 | 37 | 1 | 74.414 | 46.006 | 159 | 0 | 2903 | 2903 | 0 | 32.639 | 3653.631 | 4259.839 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15141 | 15141 | 0 | 33.279 | 3538.943 | 5734.399 |
| t-044 | 113 | 1 | 18.942 | 34.8 | 243 | 0 | 2321 | 2321 | 0 | 31.743 | 3522.559 | 3670.015 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 33.279 | 3489.791 | 5079.039 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 2642 | 2642 | 0 | 35.327 | 3473.407 | 4882.431 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19764 | 19764 | 0 | 31.871 | 3457.023 | 6619.135 |
| t-040 | 119 | 2 | 32.511 | 65.473 | 1254 | 0 | 4259 | 4259 | 0 | 32.127 | 3440.639 | 3915.775 |
| t-131 | 140 | 4 | 75.448 | 177.75 | 961 | 0 | 10979 | 10979 | 0 | 32.255 | 3375.103 | 6258.687 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 32.511 | 3358.719 | 3866.623 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2911 | 2911 | 0 | 32.255 | 3325.951 | 6225.919 |
| t-261 | 51 | 2 | 73.695 | 62.946 | 2342 | 0 | 3934 | 3934 | 0 | 32.383 | 3309.567 | 5603.327 |
| t-322 | 27 | 1 | 80.689 | 35.397 | 2198 | 0 | 2220 | 2220 | 0 | 32.255 | 3309.567 | 3702.783 |
| t-391 | 61 | 1 | 39.352 | 39.5 | 225 | 0 | 2516 | 2516 | 0 | 30.591 | 3244.031 | 3588.095 |
| t-001 | 118 | 2 | 36.72 | 71.345 | 154 | 0 | 4629 | 4629 | 0 | 31.743 | 3227.647 | 3457.023 |
| t-016 | 132 | 2 | 23.498 | 51.087 | 137 | 0 | 3310 | 3310 | 0 | 30.975 | 3227.647 | 5079.039 |
