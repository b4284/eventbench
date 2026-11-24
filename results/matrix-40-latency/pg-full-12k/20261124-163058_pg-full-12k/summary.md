# pg-full-12k — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 591 workers, offered 11956.702 ev/s
- Phases: run 60.168s, closing 1.296s, drain 1.66s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 759850 | 759850 | 11949.829 | 11951.796 | 19988 | 0 | 0 | 0 | 445 | 445 | 24526 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 597302 | 112.873 | 29.647 | 261.503 | 1503.231 | 2342.911 | 5627.903 |
| e2e_from_send | 597302 | 108.206 | 27.839 | 253.567 | 1458.175 | 2316.287 | 5627.903 |
| producer_lag | 597302 | 4.667 | 1.217 | 5.075 | 100.095 | 273.919 | 731.647 |
| publish_call | 759850 | 29.896 | 16.007 | 68.159 | 185.087 | 311.295 | 423.935 |
| queue_wait | 597302 | 79.265 | 14.743 | 147.455 | 1345.535 | 2179.071 | 3887.103 |
| broker_dwell | 597302 | 55.751 | 2.827 | 71.679 | 1234.943 | 2082.815 | 3876.863 |
| receive_call | 694257 | 43.384 | 19.967 | 109.439 | 342.783 | 475.647 | 540.671 |
| ctx_load | 597302 | 3.73 | 2.429 | 7.095 | 23.679 | 70.399 | 109.887 |
| process | 597302 | 1.183 | 0.002 | 0.004 | 0.021 | 0.365 | 2996.223 |
| ctx_commit | 597302 | 23.964 | 8.879 | 53.887 | 242.559 | 400.895 | 489.215 |
| ack | 597299 | 0.008 | 0.005 | 0.012 | 0.049 | 0.325 | 20.143 |
| service | 597302 | 28.942 | 12.135 | 61.247 | 272.639 | 430.335 | 3155.967 |
| e2e_large | 345 | 2392.518 | 2334.719 | 3186.687 | 5115.903 | 5627.903 | 5627.903 |
| ready_wait | 597302 | 33.393 | 1.922 | 60.351 | 612.863 | 1311.743 | 1857.535 |
| blocked | 597302 | 15.208 | 1.921 | 36.575 | 212.863 | 340.479 | 464.383 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 11951.796 | 19988 |
| applied_total | 435106.061 | 723218 |
| bench_cpu_cores | 5.497 | 6.568 |
| bench_cpu_pct | 22.904 | 27.36 |
| bench_gc_gen0 | 2.02 | 4 |
| bench_gc_gen1 | 0.735 | 2 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 1400.318 | 1760 |
| bench_mem_mb | 2098.78 | 2404.8 |
| bench_threadpool_queue | 37.204 | 287 |
| bench_threadpool_threads | 43.327 | 44 |
| broker_backlog | 2009.429 | 8836 |
| ctr_eb_mssql_cpu_pct | 8.48 | 32.03 |
| ctr_eb_mssql_mem_mb | 6905.667 | 6958 |
| ctr_eb_pgbouncer_2_cpu_pct | 15.094 | 16.95 |
| ctr_eb_pgbouncer_2_mem_mb | 15.6 | 16.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 15.049 | 17.35 |
| ctr_eb_pgbouncer_3_mem_mb | 15.488 | 16.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 15.499 | 17.7 |
| ctr_eb_pgbouncer_4_mem_mb | 15.622 | 16.4 |
| ctr_eb_pgbouncer_cpu_pct | 15.164 | 17.25 |
| ctr_eb_pgbouncer_mem_mb | 15.7 | 16.8 |
| ctr_eb_postgres_cpu_pct | 506.894 | 613.16 |
| ctr_eb_postgres_mem_mb | 4887.071 | 5310.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 57.28 | 66.04 |
| in_flight | 1004.367 | 13355 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.35 | 1.51 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 862211.327 | 1474939 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 175496.898 | 298197 |
| octopus_mqtt_sent_total | 309188.918 | 535896 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.796 | 18 |
| pg_active_loads | 8.837 | 77 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 30.918 | 112 |
| pg_conn_idle | 225.755 | 245 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.061 | 1 |
| pg_conn_total | 256.673 | 259 |
| pg_db_size_mb | 3349.71 | 3725 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14876.445 | 17157.9 |
| pg_tup_del_per_s | 24716.031 | 34909.9 |
| pg_tup_fetch_per_s | 169432.014 | 202587.1 |
| pg_tup_ins_per_s | 35616.341 | 50402.3 |
| pg_tup_upd_per_s | 31991.49 | 39130.8 |
| pg_wal_mb_per_s | 90.521 | 154.04 |
| pgb_cl_active | 250.429 | 255 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 6.122 | 15 |
| pgb_sv_idle | 41.878 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11949.829 | 12964.8 |
| produced_total | 436110.429 | 723267 |
| publish_mb_per_s | 44.045 | 69.33 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 111,260 records are Completed |
| record_counts | pass | 111,260 records found, 111,260 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 445 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 759,850 events applied by workers, 759,850 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 24,526 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 213 ms, max 464 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13629 seq 10: ready 140 ms before start, 140 ms of it with an idle slot in group 434
- [cross_source_blocking] source 1917 seq 17: ready 122 ms before start, 122 ms of it with an idle slot in group 55
- [cross_source_blocking] source 8857 seq 17: ready 111 ms before start, 111 ms of it with an idle slot in group 306
- [cross_source_blocking] source 2261 seq 30: ready 111 ms before start, 111 ms of it with an idle slot in group 65
- [cross_source_blocking] source 5941 seq 8: ready 111 ms before start, 111 ms of it with an idle slot in group 206
- [cross_source_blocking] source 9089 seq 29: ready 115 ms before start, 115 ms of it with an idle slot in group 314
- [cross_source_blocking] source 15894 seq 27: ready 128 ms before start, 128 ms of it with an idle slot in group 496
- [cross_source_blocking] source 12830 seq 6: ready 128 ms before start, 128 ms of it with an idle slot in group 406
- [cross_source_blocking] source 2274 seq 31: ready 129 ms before start, 129 ms of it with an idle slot in group 65
- [cross_source_blocking] source 8246 seq 7: ready 125 ms before start, 125 ms of it with an idle slot in group 279
- [cross_source_blocking] source 1314 seq 24: ready 129 ms before start, 129 ms of it with an idle slot in group 41
- [cross_source_blocking] source 13334 seq 8: ready 128 ms before start, 128 ms of it with an idle slot in group 426
- [cross_source_blocking] source 10690 seq 7: ready 129 ms before start, 129 ms of it with an idle slot in group 350
- [cross_source_blocking] source 5314 seq 12: ready 129 ms before start, 129 ms of it with an idle slot in group 182
- [cross_source_blocking] source 11338 seq 30: ready 128 ms before start, 128 ms of it with an idle slot in group 369
- [cross_source_blocking] source 15042 seq 9: ready 128 ms before start, 128 ms of it with an idle slot in group 469
- [cross_source_blocking] source 14014 seq 11: ready 128 ms before start, 128 ms of it with an idle slot in group 446
- [cross_source_blocking] source 4546 seq 15: ready 129 ms before start, 129 ms of it with an idle slot in group 160
- [cross_source_blocking] source 14342 seq 4: ready 128 ms before start, 128 ms of it with an idle slot in group 458
- [cross_source_blocking] source 4962 seq 7: ready 129 ms before start, 129 ms of it with an idle slot in group 170

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 403 | 403 | 0 | 1990.655 | 4751.359 | 5636.095 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 174 | 174 | 0 | 1990.655 | 4227.071 | 5308.415 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 76 | 76 | 0 | 2244.607 | 4014.079 | 4014.079 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 80 | 80 | 0 | 1974.271 | 3096.575 | 3096.575 |
| t-230 | 30 | 1 | 97.046 | 48.045 | 1033 | 0 | 2975 | 2975 | 0 | 31.231 | 2260.991 | 2949.119 |
| t-132 | 22 | 1 | 140.264 | 47.697 | 2676 | 0 | 2921 | 2921 | 0 | 30.463 | 2129.919 | 2818.047 |
| t-354 | 90 | 1 | 32.383 | 48.465 | 157 | 0 | 3180 | 3180 | 0 | 30.335 | 2072.575 | 3293.183 |
| t-040 | 119 | 1 | 24.383 | 49.105 | 1254 | 0 | 3122 | 3122 | 0 | 29.695 | 2047.999 | 2768.895 |
| t-260 | 30 | 1 | 77.51 | 40.125 | 499 | 0 | 2468 | 2468 | 0 | 29.567 | 2039.807 | 3014.655 |
| t-343 | 63 | 1 | 36.014 | 36.444 | 518 | 0 | 2351 | 2351 | 0 | 30.335 | 2039.807 | 2850.815 |
| t-461 | 130 | 2 | 41.879 | 91.106 | 3025 | 0 | 5951 | 5951 | 0 | 30.463 | 2039.807 | 3080.191 |
| t-438 | 121 | 3 | 69.456 | 145.426 | 1839 | 0 | 9167 | 9167 | 0 | 29.951 | 2015.231 | 2899.967 |
| t-161 | 41 | 1 | 56.725 | 38.428 | 339 | 0 | 2465 | 2465 | 0 | 28.543 | 1990.655 | 2670.591 |
| t-456 | 21 | 1 | 133.78 | 45.559 | 512 | 0 | 2746 | 2746 | 0 | 29.183 | 1966.079 | 2588.671 |
| t-315 | 88 | 2 | 58.468 | 85.632 | 2927 | 0 | 5421 | 5421 | 0 | 29.695 | 1957.887 | 2686.975 |
| t-055 | 65 | 2 | 94.352 | 97.652 | 359 | 0 | 5947 | 5947 | 0 | 30.591 | 1949.695 | 2965.503 |
| t-246 | 32 | 2 | 174.581 | 92.24 | 494 | 0 | 5589 | 5589 | 0 | 30.591 | 1949.695 | 2932.735 |
| t-147 | 79 | 4 | 135.349 | 181.75 | 1078 | 0 | 11102 | 11102 | 0 | 30.207 | 1933.311 | 2834.431 |
| t-261 | 51 | 1 | 55.271 | 47.21 | 2342 | 0 | 2956 | 2956 | 0 | 28.031 | 1908.735 | 2785.279 |
| t-222 | 138 | 3 | 57.654 | 134.182 | 2846 | 0 | 8410 | 8410 | 0 | 30.079 | 1900.543 | 2998.271 |
| t-383 | 200 | 5 | 72.102 | 239.81 | 2521 | 0 | 14962 | 14962 | 0 | 29.567 | 1900.543 | 3227.647 |
| t-136 | 146 | 2 | 37.66 | 90.777 | 450 | 0 | 5924 | 5924 | 0 | 28.671 | 1892.351 | 3129.343 |
| t-245 | 43 | 1 | 66.253 | 45.507 | 2232 | 0 | 2811 | 2811 | 0 | 30.591 | 1892.351 | 2523.135 |
| t-065 | 80 | 5 | 157.789 | 208.285 | 263 | 0 | 12696 | 12696 | 0 | 29.951 | 1859.583 | 2654.207 |
| t-120 | 37 | 1 | 77.496 | 47.051 | 1412 | 0 | 2942 | 2942 | 0 | 29.183 | 1859.583 | 2752.511 |
