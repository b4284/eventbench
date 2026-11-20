# D3-scale-out-2-to-3 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.117s, closing 1.644s, drain 1.505s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000525 | 1000525 | 15943.827 | 15945.554 | 21029.8 | 0 | 0 | 0 | 458 | 5163 | 13873 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797160 | 67.632 | 25.103 | 61.407 | 1500.159 | 2840.575 | 6885.375 |
| e2e_from_send | 797160 | 65.706 | 23.391 | 57.695 | 1499.135 | 2838.527 | 6881.279 |
| producer_lag | 797160 | 1.927 | 1.139 | 4.271 | 14.623 | 41.215 | 218.495 |
| publish_call | 1000525 | 23.253 | 12.503 | 52.383 | 164.863 | 334.079 | 540.159 |
| queue_wait | 797160 | 45.085 | 14.247 | 40.063 | 1057.791 | 2529.279 | 5308.415 |
| broker_dwell | 797160 | 34.538 | 4.687 | 21.407 | 1047.551 | 2516.991 | 5251.071 |
| receive_call | 926467 | 33.597 | 13.375 | 85.567 | 309.247 | 467.455 | 530.431 |
| ctx_load | 797160 | 4.091 | 3.149 | 7.719 | 17.583 | 29.279 | 55.295 |
| process | 797160 | 10.731 | 0.001 | 0.002 | 0.024 | 2619.391 | 3002.367 |
| ctx_commit | 797160 | 5.774 | 4.387 | 11.047 | 26.783 | 41.535 | 174.975 |
| ack | 795344 | 0.005 | 0.003 | 0.007 | 0.031 | 0.264 | 17.871 |
| service | 797160 | 20.621 | 8.147 | 17.967 | 43.263 | 2629.631 | 3037.183 |
| e2e_large | 356 | 2518.204 | 2459.647 | 3356.671 | 5574.655 | 6885.375 | 6885.375 |
| ready_wait | 797160 | 11.569 | 2.897 | 16.623 | 193.151 | 1042.943 | 3274.751 |
| blocked | 797160 | 11.242 | 2.863 | 16.375 | 189.055 | 1015.295 | 3213.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15945.554 | 17924.2 |
| applied_total | 570446.833 | 945727 |
| bench_cpu_cores | 5.36 | 5.787 |
| bench_cpu_pct | 22.333 | 24.11 |
| bench_gc_gen0 | 3.25 | 5 |
| bench_gc_gen1 | 2.292 | 4 |
| bench_gc_gen2 | 0.583 | 2 |
| bench_gc_heap_mb | 989.979 | 1295.4 |
| bench_mem_mb | 1545.754 | 1710.1 |
| bench_threadpool_queue | 33.958 | 262 |
| bench_threadpool_threads | 39.771 | 44 |
| broker_backlog | 2359 | 5193 |
| ctr_eb_mosquitto_cpu_pct | 62.896 | 66.23 |
| ctr_eb_mosquitto_mem_mb | 7.592 | 11.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.118 | 19.76 |
| ctr_eb_pgbouncer_2_mem_mb | 15.731 | 16.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.235 | 20.01 |
| ctr_eb_pgbouncer_3_mem_mb | 15.831 | 16.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.137 | 21.62 |
| ctr_eb_pgbouncer_4_mem_mb | 16.033 | 17.2 |
| ctr_eb_pgbouncer_cpu_pct | 19.525 | 20.29 |
| ctr_eb_pgbouncer_mem_mb | 16.644 | 17.6 |
| ctr_eb_postgres_cpu_pct | 679.63 | 718.3 |
| ctr_eb_postgres_mem_mb | 5472.806 | 5659.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 68.844 | 73.49 |
| in_flight | 826.625 | 2691 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.593 | 1.83 |
| octopus_dispatchers_alive | 2.562 | 3 |
| octopus_items_sent_total | 1122038.562 | 1849813 |
| octopus_mqtt_received_total | 147120 | 260603 |
| octopus_mqtt_sent_total | 330292.375 | 585239 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.292 | 8 |
| pg_active_loads | 2.438 | 12 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 9.958 | 16 |
| pg_conn_idle | 236.479 | 250 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.646 | 8 |
| pg_conn_total | 246.438 | 255 |
| pg_db_size_mb | 3489.671 | 3870.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20714.91 | 22467.8 |
| pg_tup_del_per_s | 43529.569 | 50602.2 |
| pg_tup_fetch_per_s | 269218.852 | 371516.2 |
| pg_tup_ins_per_s | 55254.346 | 68559 |
| pg_tup_upd_per_s | 41654.446 | 43686.8 |
| pg_wal_mb_per_s | 111.927 | 246.87 |
| pgb_cl_active | 243.562 | 272 |
| pgb_cl_waiting | 0.938 | 15 |
| pgb_maxwait_ms | 0.161 | 2.58 |
| pgb_sv_active | 7.667 | 48 |
| pgb_sv_idle | 40.333 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15943.827 | 16334.9 |
| produced_total | 571273.458 | 946240 |
| publish_mb_per_s | 51.956 | 74.04 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,525 events applied by workers, 1,000,525 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 13,873 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 189 ms, max 3212 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13877 seq 10: ready 107 ms before start, 102 ms of it with an idle slot in group 439
- [cross_source_blocking] source 10757 seq 7: ready 122 ms before start, 122 ms of it with an idle slot in group 353
- [cross_source_blocking] source 2993 seq 14: ready 124 ms before start, 124 ms of it with an idle slot in group 99
- [cross_source_blocking] source 5453 seq 25: ready 122 ms before start, 122 ms of it with an idle slot in group 186
- [cross_source_blocking] source 1125 seq 19: ready 122 ms before start, 122 ms of it with an idle slot in group 37
- [cross_source_blocking] source 1613 seq 7: ready 124 ms before start, 124 ms of it with an idle slot in group 48
- [cross_source_blocking] source 2245 seq 26: ready 122 ms before start, 122 ms of it with an idle slot in group 65
- [cross_source_blocking] source 3281 seq 14: ready 124 ms before start, 124 ms of it with an idle slot in group 109
- [cross_source_blocking] source 5449 seq 27: ready 124 ms before start, 124 ms of it with an idle slot in group 186
- [cross_source_blocking] source 4329 seq 28: ready 124 ms before start, 124 ms of it with an idle slot in group 147
- [cross_source_blocking] source 865 seq 11: ready 124 ms before start, 124 ms of it with an idle slot in group 25
- [cross_source_blocking] source 4285 seq 42: ready 120 ms before start, 120 ms of it with an idle slot in group 147
- [cross_source_blocking] source 2605 seq 12: ready 123 ms before start, 123 ms of it with an idle slot in group 80
- [cross_source_blocking] source 8565 seq 22: ready 124 ms before start, 124 ms of it with an idle slot in group 294
- [cross_source_blocking] source 7885 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 265
- [cross_source_blocking] source 3301 seq 20: ready 122 ms before start, 122 ms of it with an idle slot in group 111
- [cross_source_blocking] source 15661 seq 9: ready 125 ms before start, 125 ms of it with an idle slot in group 488
- [cross_source_blocking] source 5621 seq 24: ready 124 ms before start, 124 ms of it with an idle slot in group 194
- [cross_source_blocking] source 325 seq 5: ready 122 ms before start, 122 ms of it with an idle slot in group 9
- [cross_source_blocking] source 6117 seq 6: ready 122 ms before start, 122 ms of it with an idle slot in group 210

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2113.535 | 5636.095 | 6914.047 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 1957.887 | 5079.039 | 5537.791 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 1867.775 | 3686.399 | 4358.143 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1589.247 | 3407.871 | 4390.911 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 25.727 | 2981.887 | 4685.823 |
| t-045 | 47 | 2 | 102.442 | 79.828 | 1398 | 0 | 4933 | 4933 | 0 | 25.471 | 2850.815 | 3145.727 |
| t-100 | 33 | 1 | 50.561 | 26.936 | 2626 | 0 | 1730 | 1730 | 0 | 26.239 | 2818.047 | 2998.271 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 26.367 | 2621.439 | 4227.071 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 24.831 | 2539.519 | 4095.999 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7644 | 7644 | 0 | 26.751 | 2392.063 | 5570.559 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 24.831 | 2375.679 | 2932.735 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 25.599 | 2359.295 | 4489.215 |
| t-364 | 66 | 1 | 22.212 | 25.071 | 436 | 0 | 1657 | 1657 | 0 | 25.983 | 2293.759 | 2473.983 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 25.087 | 2277.375 | 2932.735 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 25.343 | 2277.375 | 3178.495 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 26.239 | 2260.991 | 3522.559 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 24.831 | 2260.991 | 2752.511 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 24.447 | 2244.607 | 2981.887 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 25.983 | 2228.223 | 2899.967 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 23.423 | 2211.839 | 2768.895 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 23.551 | 2179.071 | 3178.495 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 25.343 | 2179.071 | 2883.583 |
| t-355 | 10 | 1 | 37.479 | 6.571 | 349 | 0 | 417 | 417 | 0 | 24.959 | 2146.303 | 2965.503 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 25.471 | 2146.303 | 3112.959 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 24.063 | 2129.919 | 2949.119 |
