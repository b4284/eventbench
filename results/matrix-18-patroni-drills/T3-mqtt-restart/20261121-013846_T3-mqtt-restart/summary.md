# T3-mqtt-restart — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.14s, closing 0.782s, drain 180.009s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814630 | 387628 | 12873.537 | 5352.024 | 15619.3 | 0 | 0 | 0 | 431 | 4254 | 109556 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 251171 | 1263.488 | 267.519 | 4214.783 | 8912.895 | 12533.759 | 18235.391 |
| e2e_from_send | 251171 | 1230.864 | 251.007 | 4063.231 | 8904.703 | 12533.759 | 18235.391 |
| producer_lag | 642906 | 354.555 | 1.628 | 1412.095 | 4653.055 | 4915.199 | 5222.399 |
| publish_call | 814630 | 55.045 | 29.743 | 97.215 | 240.639 | 5099.519 | 5259.263 |
| queue_wait | 251171 | 1160.415 | 176.895 | 3991.551 | 8830.975 | 12460.031 | 18219.007 |
| broker_dwell | 251171 | 1106.363 | 116.927 | 3897.343 | 8781.823 | 12410.879 | 18186.239 |
| receive_call | 336012 | 39.975 | 12.903 | 111.743 | 357.119 | 481.023 | 534.015 |
| ctx_load | 251171 | 8.664 | 5.967 | 18.591 | 43.871 | 68.223 | 108.799 |
| process | 251171 | 11.06 | 0.002 | 0.004 | 0.054 | 2639.871 | 3002.367 |
| ctx_commit | 251171 | 50.668 | 44.127 | 91.583 | 140.287 | 183.423 | 407.039 |
| ack | 250784 | 0.007 | 0.003 | 0.005 | 0.029 | 0.656 | 41.055 |
| service | 251171 | 70.451 | 53.663 | 102.399 | 169.599 | 2699.263 | 3143.679 |
| e2e_large | 180 | 2993.795 | 2848.767 | 4362.239 | 7278.591 | 7659.519 | 7659.519 |
| ready_wait | 251171 | 269.303 | 86.911 | 757.247 | 1690.623 | 2676.735 | 5140.479 |
| blocked | 251171 | 166.583 | 59.743 | 471.039 | 968.191 | 1446.911 | 5140.479 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5352.024 | 15619.3 |
| applied_total | 336546.082 | 387628 |
| bench_cpu_cores | 3.175 | 5.073 |
| bench_cpu_pct | 13.231 | 21.14 |
| bench_gc_gen0 | 2.204 | 4 |
| bench_gc_gen1 | 1.714 | 4 |
| bench_gc_gen2 | 0.408 | 1 |
| bench_gc_heap_mb | 975.82 | 1258.7 |
| bench_mem_mb | 1478.994 | 1670.3 |
| bench_threadpool_queue | 23.224 | 353 |
| bench_threadpool_threads | 43.143 | 44 |
| broker_backlog | 119209.327 | 387398 |
| ctr_eb_etcd_cpu_pct | 0.643 | 0.99 |
| ctr_eb_etcd_mem_mb | 29.157 | 29.9 |
| ctr_eb_mosquitto_cpu_pct | 37.411 | 62.14 |
| ctr_eb_mosquitto_mem_mb | 7.641 | 21 |
| ctr_eb_pat1_cpu_pct | 40.36 | 69.94 |
| ctr_eb_pat1_mem_mb | 6227.788 | 6528.9 |
| ctr_eb_pat2_cpu_pct | 548.028 | 1073.09 |
| ctr_eb_pat2_mem_mb | 13001.502 | 13313.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.437 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 9.712 | 9.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.025 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 10.982 | 11.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.655 | 11.9 |
| ctr_eb_postgres_cpu_pct | 1.347 | 2.37 |
| ctr_eb_postgres_mem_mb | 2413.078 | 2413.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 43.125 | 81.79 |
| in_flight | 126945.49 | 389577 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.074 | 1.71 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 795187.143 | 1183623 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 159028.816 | 224279 |
| octopus_mqtt_sent_total | 214628.633 | 272375 |
| octopus_publish_retries_total | 881.041 | 1877 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 4.837 | 17 |
| pg_active_loads | 3.673 | 34 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 26.041 | 76 |
| pg_conn_idle | 242.082 | 268 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.551 | 12 |
| pg_conn_total | 268.122 | 269 |
| pg_db_size_mb | 1572.431 | 2127.6 |
| pg_deadlocks | 2 | 2 |
| pg_replica_lag_mb | 3.303 | 27.56 |
| pg_replica_replay_lag_ms | 17.667 | 93.8 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14340.567 | 32770.3 |
| pg_tup_del_per_s | 19758.104 | 45984.7 |
| pg_tup_fetch_per_s | 300435.153 | 678596.3 |
| pg_tup_ins_per_s | 43219.384 | 66796.2 |
| pg_tup_upd_per_s | 16548.759 | 37990.8 |
| pg_wal_mb_per_s | 80.254 | 230.36 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12873.537 | 43065.1 |
| produced_total | 463491.571 | 777205 |
| publish_mb_per_s | 44.438 | 118.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 1/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 31,156 records not Completed |
| record_counts | **FAIL** | 70,560 records found, 117,596 expected; 15999 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | **FAIL** | 280 uploads verified, 84 sources mismatched |
| applied_equals_generated | **FAIL** | 387,628 events applied by workers, 814,630 generated |
| broker_drained | **FAIL** | 427,776 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 109,556 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 968 ms, max 5139 ms) |

Sample mismatches:

- source 1702: record 7310034337793 left in status Open (version 3)
- source 1541: record 6618544603137 left in status Open (version 10)
- source 217: record 932007903233 left in status Open (version 6)
- source 1096: record 4707284156417 left in status Open (version 6)
- source 808: record 3470333575169 left in status Open (version 4)
- source 2333: record 10020158701569 left in status Open (version 10)
- source 1624: record 6975026888705 left in status Open (version 5)
- source 12151: record 52188147613700 left in status Open (version 1)
- source 1729: record 7425998454785 left in status Open (version 3)
- source 2984: record 12816182411268 left in status Open (version 7)
- source 2319: record 9960029159425 left in status Open (version 7)
- source 9672: record 41540923686914 left in status Open (version 3)
- source 1143: record 4909147619329 left in status Open (version 3)
- source 1041: record 4471060955138 left in status Open (version 6)
- source 10416: record 44736379355142 left in status Open (version 1)
- source 441: record 1894080577537 left in status Open (version 5)
- source 4701: record 20190641258497 left in status Open (version 4)
- source 357: record 1533303324673 left in status Open (version 4)
- source 6361: record 27320286969857 left in status Open (version 3)
- source 3364: record 14448269983748 left in status Open (version 3)

## Cross-source blocking samples

- [cross_source_blocking] source 13213 seq 12: ready 112 ms before start, 112 ms of it with an idle slot in group 421
- [cross_source_blocking] source 7401 seq 4: ready 113 ms before start, 113 ms of it with an idle slot in group 255
- [cross_source_blocking] source 2857 seq 9: ready 113 ms before start, 113 ms of it with an idle slot in group 91
- [cross_source_blocking] source 7394 seq 5: ready 114 ms before start, 114 ms of it with an idle slot in group 255
- [cross_source_blocking] source 4528 seq 5: ready 111 ms before start, 111 ms of it with an idle slot in group 157
- [cross_source_blocking] source 12383 seq 10: ready 114 ms before start, 114 ms of it with an idle slot in group 393
- [cross_source_blocking] source 2119 seq 4: ready 114 ms before start, 114 ms of it with an idle slot in group 61
- [cross_source_blocking] source 14401 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 459
- [cross_source_blocking] source 11528 seq 9: ready 112 ms before start, 112 ms of it with an idle slot in group 375
- [cross_source_blocking] source 13212 seq 13: ready 115 ms before start, 115 ms of it with an idle slot in group 421
- [cross_source_blocking] source 15272 seq 6: ready 111 ms before start, 111 ms of it with an idle slot in group 477
- [cross_source_blocking] source 1141 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 38
- [cross_source_blocking] source 15791 seq 3: ready 129 ms before start, 129 ms of it with an idle slot in group 494
- [cross_source_blocking] source 14045 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 446
- [cross_source_blocking] source 4707 seq 3: ready 134 ms before start, 134 ms of it with an idle slot in group 162
- [cross_source_blocking] source 1932 seq 18: ready 190 ms before start, 120 ms of it with an idle slot in group 55
- [cross_source_blocking] source 1880 seq 7: ready 136 ms before start, 136 ms of it with an idle slot in group 54
- [cross_source_blocking] source 708 seq 3: ready 134 ms before start, 134 ms of it with an idle slot in group 21
- [cross_source_blocking] source 1912 seq 23: ready 174 ms before start, 115 ms of it with an idle slot in group 55
- [cross_source_blocking] source 13541 seq 3: ready 164 ms before start, 107 ms of it with an idle slot in group 429

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 1065 | 0 | 5013.503 | 16121.855 | 18087.935 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 1124 | 0 | 4947.967 | 14483.455 | 17039.359 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9186 | 3595 | 0 | 3653.631 | 13697.023 | 18350.079 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 2498 | 0 | 5013.503 | 13500.415 | 17039.359 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2679 | 1068 | 0 | 3817.471 | 13172.735 | 15532.031 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5928 | 2438 | 0 | 4161.535 | 12976.127 | 14942.207 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 2501 | 0 | 3768.319 | 12845.055 | 15007.743 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12133 | 4845 | 0 | 5046.271 | 12320.767 | 14155.775 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2833 | 1135 | 0 | 3358.719 | 12189.695 | 14614.527 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6274 | 2565 | 0 | 3883.007 | 12058.623 | 16121.855 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12254 | 5100 | 0 | 4079.615 | 11599.871 | 14286.847 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 2492 | 0 | 3948.543 | 11403.263 | 13434.879 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2978 | 1189 | 0 | 3244.031 | 11337.727 | 14614.527 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 1309 | 0 | 4685.823 | 11206.655 | 12976.127 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 1254 | 0 | 3031.039 | 11075.583 | 12910.591 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 3649 | 0 | 2916.351 | 10747.903 | 13172.735 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13791 | 6253 | 0 | 2752.511 | 10485.759 | 12058.623 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 3595 | 0 | 3162.111 | 10485.759 | 11599.871 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 1142 | 0 | 3424.255 | 10289.151 | 14221.311 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 1318 | 0 | 2654.207 | 10289.151 | 12713.983 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16765 | 7464 | 0 | 2801.663 | 10289.151 | 13238.271 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5703 | 2463 | 0 | 2867.199 | 10158.079 | 11272.191 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5490 | 2431 | 0 | 2998.271 | 9764.863 | 11010.047 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 1181 | 0 | 3162.111 | 9699.327 | 11534.335 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2766 | 1228 | 0 | 2113.535 | 9568.255 | 13303.807 |
