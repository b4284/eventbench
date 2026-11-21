# D3-morning-everything-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.14s, closing 2.082s, drain 3.002s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000317 | 1000317 | 16664.088 | 16489.056 | 20403.9 | 0 | 0 | 0 | 458 | 5161 | 373558 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796950 | 1876.271 | 1441.791 | 4173.823 | 9650.175 | 13688.831 | 20398.079 |
| e2e_from_send | 796950 | 1102.627 | 270.591 | 3405.823 | 9379.839 | 13516.799 | 19841.023 |
| producer_lag | 796950 | 773.643 | 20.367 | 2420.735 | 3395.583 | 3782.655 | 4554.751 |
| publish_call | 1000317 | 81.938 | 62.559 | 165.119 | 331.007 | 658.943 | 1219.583 |
| queue_wait | 796950 | 1046.779 | 219.519 | 3342.335 | 9322.495 | 13459.455 | 19808.255 |
| broker_dwell | 796950 | 989.678 | 137.727 | 3289.087 | 9281.535 | 13418.495 | 19775.487 |
| receive_call | 830455 | 37.895 | 16.943 | 91.711 | 327.679 | 492.287 | 593.919 |
| ctx_load | 796950 | 13.556 | 10.367 | 27.295 | 56.607 | 81.023 | 189.183 |
| process | 796950 | 10.748 | 0.002 | 0.004 | 0.074 | 2619.391 | 3067.903 |
| ctx_commit | 796950 | 31.432 | 26.303 | 61.727 | 105.151 | 147.455 | 586.751 |
| ack | 796220 | 0.01 | 0.003 | 0.005 | 0.036 | 1.103 | 52.863 |
| service | 796950 | 55.849 | 40.127 | 81.919 | 143.487 | 2674.687 | 3389.439 |
| e2e_large | 356 | 3820.97 | 3557.375 | 5844.991 | 8196.095 | 9707.519 | 9707.519 |
| ready_wait | 796950 | 207.413 | 98.495 | 555.519 | 1205.247 | 1843.199 | 2496.511 |
| blocked | 796950 | 172.374 | 84.159 | 465.151 | 894.463 | 1417.215 | 2063.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16489.056 | 19659.9 |
| applied_total | 540779.667 | 930375 |
| bench_cpu_cores | 4.519 | 5.507 |
| bench_cpu_pct | 18.83 | 22.95 |
| bench_gc_gen0 | 3.167 | 5 |
| bench_gc_gen1 | 3.104 | 4 |
| bench_gc_gen2 | 0.646 | 2 |
| bench_gc_heap_mb | 1035.012 | 1349.7 |
| bench_mem_mb | 1582.754 | 1787.5 |
| bench_threadpool_queue | 64.562 | 592 |
| bench_threadpool_threads | 41.438 | 44 |
| broker_backlog | 20462.104 | 31757 |
| ctr_eb_mosquitto_cpu_pct | 68.976 | 70.94 |
| ctr_eb_mosquitto_mem_mb | 7.048 | 12.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.86 | 24.84 |
| ctr_eb_pgbouncer_2_mem_mb | 15.402 | 17.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 22.727 | 24.7 |
| ctr_eb_pgbouncer_3_mem_mb | 15.385 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 23.896 | 26.18 |
| ctr_eb_pgbouncer_4_mem_mb | 15.608 | 16.6 |
| ctr_eb_pgbouncer_cpu_pct | 23.876 | 25.67 |
| ctr_eb_pgbouncer_mem_mb | 16.781 | 18.6 |
| ctr_eb_postgres_cpu_pct | 983.864 | 1034.34 |
| ctr_eb_postgres_mem_mb | 5810.765 | 5923.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.149 | 82.9 |
| in_flight | 16604.938 | 30217 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.854 | 1.95 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1074402.083 | 1861787 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 157637.979 | 272480 |
| octopus_mqtt_sent_total | 299721.125 | 537225 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.292 | 17 |
| pg_active_loads | 7.042 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 34.021 | 114 |
| pg_conn_idle | 224.375 | 256 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.25 | 33 |
| pg_conn_total | 258.396 | 261 |
| pg_db_size_mb | 3574.61 | 4162.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19382.169 | 22275.8 |
| pg_tup_del_per_s | 44289.898 | 49871.5 |
| pg_tup_fetch_per_s | 560402.506 | 640918.1 |
| pg_tup_ins_per_s | 57012.869 | 69534.7 |
| pg_tup_upd_per_s | 42509.271 | 49502.1 |
| pg_wal_mb_per_s | 116.373 | 218.32 |
| pgb_cl_active | 247.146 | 260 |
| pgb_cl_waiting | 4.521 | 34 |
| pgb_maxwait_ms | 1.945 | 16.51 |
| pgb_sv_active | 15.5 | 48 |
| pgb_sv_idle | 32.5 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16664.088 | 20838 |
| produced_total | 557384.604 | 945854 |
| publish_mb_per_s | 54.643 | 74.5 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,848 records are Completed |
| record_counts | pass | 138,848 records found, 138,848 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,317 events applied by workers, 1,000,317 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 373,558 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 894 ms, max 2063 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2789 seq 13: ready 135 ms before start, 135 ms of it with an idle slot in group 86
- [cross_source_blocking] source 9349 seq 4: ready 135 ms before start, 135 ms of it with an idle slot in group 324
- [cross_source_blocking] source 325 seq 5: ready 133 ms before start, 133 ms of it with an idle slot in group 9
- [cross_source_blocking] source 12741 seq 8: ready 176 ms before start, 110 ms of it with an idle slot in group 402
- [cross_source_blocking] source 4581 seq 10: ready 136 ms before start, 131 ms of it with an idle slot in group 160
- [cross_source_blocking] source 8773 seq 12: ready 228 ms before start, 205 ms of it with an idle slot in group 306
- [cross_source_blocking] source 8549 seq 8: ready 207 ms before start, 112 ms of it with an idle slot in group 293
- [cross_source_blocking] source 562 seq 3: ready 103 ms before start, 103 ms of it with an idle slot in group 16
- [cross_source_blocking] source 11293 seq 6: ready 137 ms before start, 137 ms of it with an idle slot in group 368
- [cross_source_blocking] source 11773 seq 4: ready 138 ms before start, 129 ms of it with an idle slot in group 382
- [cross_source_blocking] source 13981 seq 5: ready 135 ms before start, 135 ms of it with an idle slot in group 444
- [cross_source_blocking] source 2973 seq 14: ready 140 ms before start, 140 ms of it with an idle slot in group 97
- [cross_source_blocking] source 5853 seq 4: ready 135 ms before start, 135 ms of it with an idle slot in group 201
- [cross_source_blocking] source 4253 seq 5: ready 139 ms before start, 139 ms of it with an idle slot in group 143
- [cross_source_blocking] source 7909 seq 30: ready 140 ms before start, 140 ms of it with an idle slot in group 266
- [cross_source_blocking] source 13925 seq 16: ready 138 ms before start, 135 ms of it with an idle slot in group 443
- [cross_source_blocking] source 10405 seq 12: ready 225 ms before start, 166 ms of it with an idle slot in group 347
- [cross_source_blocking] source 15357 seq 7: ready 138 ms before start, 138 ms of it with an idle slot in group 480
- [cross_source_blocking] source 3045 seq 14: ready 138 ms before start, 138 ms of it with an idle slot in group 102
- [cross_source_blocking] source 14909 seq 25: ready 138 ms before start, 131 ms of it with an idle slot in group 468

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 4489.215 | 14942.207 | 20447.231 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15137 | 15137 | 0 | 4292.607 | 14876.671 | 19529.727 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 3440.639 | 13762.559 | 17170.431 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14673 | 14673 | 0 | 3653.631 | 13434.879 | 15532.031 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 3719.167 | 13434.879 | 15597.567 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9220 | 9220 | 0 | 3670.015 | 13238.271 | 17694.719 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11852 | 11852 | 0 | 3489.791 | 13041.663 | 17039.359 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5630 | 5630 | 0 | 3358.719 | 12648.447 | 16908.287 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 3178.495 | 12517.375 | 14548.991 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3194 | 3194 | 0 | 3407.871 | 12386.303 | 14942.207 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5917 | 5917 | 0 | 3964.927 | 12386.303 | 14811.135 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5536 | 5536 | 0 | 3588.095 | 12058.623 | 14811.135 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6097 | 6097 | 0 | 3211.263 | 11862.015 | 16515.071 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 3309.567 | 11599.871 | 13893.631 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8299 | 8299 | 0 | 2768.895 | 11534.335 | 12845.055 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11133 | 11133 | 0 | 3276.799 | 11468.799 | 16908.287 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 3293.183 | 11272.191 | 12582.911 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19760 | 19760 | 0 | 2932.735 | 11010.047 | 15859.711 |
| t-122 | 33 | 1 | 82.365 | 44.385 | 550 | 0 | 2756 | 2756 | 0 | 2785.279 | 10878.975 | 12582.911 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5828 | 5828 | 0 | 2785.279 | 10813.439 | 14090.239 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2724 | 2724 | 0 | 3555.327 | 10682.367 | 11665.407 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20591 | 20591 | 0 | 3014.655 | 10682.367 | 13434.879 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5723 | 5723 | 0 | 3342.335 | 10616.831 | 12255.231 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 2834.431 | 10551.295 | 15269.887 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 2867.199 | 10551.295 | 11927.551 |
