# P0b-epochs-none-3d-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.118s, closing 1.401s, drain 1.958s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000523 | 1000523 | 16139.835 | 16140.1 | 23209.9 | 0 | 0 | 0 | 458 | 5162 | 18713 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797156 | 117.635 | 35.679 | 167.679 | 1622.015 | 2883.583 | 6774.783 |
| e2e_from_send | 797156 | 79.763 | 33.535 | 116.479 | 1405.951 | 2803.711 | 5914.623 |
| producer_lag | 797156 | 37.873 | 1.523 | 8.967 | 1025.023 | 1537.023 | 2072.575 |
| publish_call | 1000523 | 36.685 | 17.631 | 88.063 | 259.327 | 513.279 | 702.463 |
| queue_wait | 797156 | 52.782 | 19.247 | 81.087 | 808.959 | 2371.583 | 4685.823 |
| broker_dwell | 797156 | 33.497 | 5.999 | 30.719 | 787.455 | 2355.199 | 4657.151 |
| receive_call | 906705 | 38.347 | 20.111 | 89.087 | 300.799 | 462.847 | 525.311 |
| ctx_load | 797156 | 6.03 | 4.555 | 11.775 | 26.591 | 46.463 | 75.007 |
| process | 797156 | 10.733 | 0.002 | 0.004 | 0.045 | 2619.391 | 3002.367 |
| ctx_commit | 797156 | 10.182 | 7.159 | 21.119 | 50.879 | 77.951 | 301.823 |
| ack | 796583 | 0.006 | 0.003 | 0.005 | 0.028 | 0.331 | 35.231 |
| service | 797156 | 26.982 | 12.663 | 31.647 | 74.111 | 2635.775 | 3155.967 |
| e2e_large | 356 | 2585.437 | 2553.855 | 3514.367 | 4739.071 | 6774.783 | 6774.783 |
| ready_wait | 797156 | 13.109 | 4.013 | 24.399 | 207.359 | 315.391 | 1558.527 |
| blocked | 797156 | 12.184 | 4.007 | 23.567 | 193.407 | 267.263 | 485.375 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16140.1 | 18782.4 |
| applied_total | 577710.612 | 961292 |
| bench_cpu_cores | 5.006 | 5.46 |
| bench_cpu_pct | 20.857 | 22.75 |
| bench_gc_gen0 | 3.265 | 5 |
| bench_gc_gen1 | 2.551 | 4 |
| bench_gc_gen2 | 0.571 | 2 |
| bench_gc_heap_mb | 981.027 | 1240.4 |
| bench_mem_mb | 1558.769 | 1700.7 |
| bench_threadpool_queue | 36.653 | 360 |
| bench_threadpool_threads | 39.796 | 44 |
| broker_backlog | 2651.735 | 5520 |
| ctr_eb_mosquitto_cpu_pct | 66.103 | 70.22 |
| ctr_eb_mosquitto_mem_mb | 8.461 | 18.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 19.582 | 21.77 |
| ctr_eb_pgbouncer_2_mem_mb | 15.098 | 15.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.71 | 21.88 |
| ctr_eb_pgbouncer_3_mem_mb | 15.349 | 15.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.539 | 22.85 |
| ctr_eb_pgbouncer_4_mem_mb | 15.416 | 16.5 |
| ctr_eb_pgbouncer_cpu_pct | 19.754 | 21.54 |
| ctr_eb_pgbouncer_mem_mb | 16.131 | 17.1 |
| ctr_eb_postgres_cpu_pct | 817.768 | 1004.17 |
| ctr_eb_postgres_mem_mb | 5395.7 | 5999.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 73.349 | 81.51 |
| in_flight | 861.796 | 1841 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.842 | 1.9 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1132808.122 | 1902627 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 176821.449 | 298780 |
| octopus_mqtt_sent_total | 372344.98 | 651660 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 3.347 | 14 |
| pg_active_loads | 4.061 | 19 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 16.286 | 52 |
| pg_conn_idle | 241.796 | 257 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.163 | 4 |
| pg_conn_total | 258.082 | 259 |
| pg_db_size_mb | 3502.816 | 3956.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21914.708 | 22666.7 |
| pg_tup_del_per_s | 44390.178 | 62174.6 |
| pg_tup_fetch_per_s | 471035.927 | 680719.6 |
| pg_tup_ins_per_s | 55800.265 | 72992.1 |
| pg_tup_upd_per_s | 42520.445 | 46852.3 |
| pg_wal_mb_per_s | 112.345 | 142.64 |
| pgb_cl_active | 239.082 | 272 |
| pgb_cl_waiting | 8.531 | 84 |
| pgb_maxwait_ms | 0.346 | 3.58 |
| pgb_sv_active | 14.939 | 48 |
| pgb_sv_idle | 33.061 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16139.835 | 18664.2 |
| produced_total | 578572.408 | 962085 |
| publish_mb_per_s | 52.691 | 79.97 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,523 events applied by workers, 1,000,523 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,713 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 193 ms, max 485 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1659 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 49
- [cross_source_blocking] source 4563 seq 12: ready 108 ms before start, 108 ms of it with an idle slot in group 160
- [cross_source_blocking] source 2219 seq 43: ready 108 ms before start, 108 ms of it with an idle slot in group 65
- [cross_source_blocking] source 6167 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 210
- [cross_source_blocking] source 5235 seq 32: ready 107 ms before start, 107 ms of it with an idle slot in group 181
- [cross_source_blocking] source 4299 seq 27: ready 105 ms before start, 105 ms of it with an idle slot in group 147
- [cross_source_blocking] source 8179 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 276
- [cross_source_blocking] source 13699 seq 8: ready 110 ms before start, 110 ms of it with an idle slot in group 436
- [cross_source_blocking] source 14239 seq 31: ready 109 ms before start, 109 ms of it with an idle slot in group 456
- [cross_source_blocking] source 14943 seq 27: ready 106 ms before start, 106 ms of it with an idle slot in group 468
- [cross_source_blocking] source 11580 seq 5: ready 108 ms before start, 108 ms of it with an idle slot in group 376
- [cross_source_blocking] source 8055 seq 29: ready 113 ms before start, 113 ms of it with an idle slot in group 271
- [cross_source_blocking] source 13867 seq 12: ready 105 ms before start, 105 ms of it with an idle slot in group 439
- [cross_source_blocking] source 11508 seq 37: ready 105 ms before start, 105 ms of it with an idle slot in group 374
- [cross_source_blocking] source 15636 seq 23: ready 105 ms before start, 105 ms of it with an idle slot in group 487
- [cross_source_blocking] source 1071 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 32
- [cross_source_blocking] source 6019 seq 18: ready 114 ms before start, 114 ms of it with an idle slot in group 206
- [cross_source_blocking] source 11031 seq 14: ready 109 ms before start, 109 ms of it with an idle slot in group 360
- [cross_source_blocking] source 4348 seq 25: ready 115 ms before start, 115 ms of it with an idle slot in group 147
- [cross_source_blocking] source 3799 seq 27: ready 121 ms before start, 121 ms of it with an idle slot in group 128

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2080.767 | 5636.095 | 6782.975 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2129.919 | 4358.143 | 5537.791 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 1794.047 | 4227.071 | 4390.911 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2162.687 | 3866.623 | 4554.751 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 35.327 | 2621.439 | 4227.071 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 911 | 911 | 0 | 35.583 | 2506.751 | 3096.575 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3805 | 3805 | 0 | 36.863 | 2392.063 | 4521.983 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 721 | 721 | 0 | 36.863 | 2392.063 | 4014.079 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 37.887 | 2392.063 | 2949.119 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 33.279 | 2359.295 | 3538.943 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 37.375 | 2359.295 | 3260.415 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 36.863 | 2310.143 | 2736.127 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 855 | 855 | 0 | 35.583 | 2293.759 | 3276.799 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 37.631 | 2293.759 | 3637.247 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 36.863 | 2293.759 | 2899.967 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 34.559 | 2293.759 | 2949.119 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 35.071 | 2277.375 | 2801.663 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 38.911 | 2277.375 | 2899.967 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 33.023 | 2260.991 | 2801.663 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2260 | 2260 | 0 | 37.887 | 2260.991 | 3162.111 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 37.375 | 2260.991 | 2981.887 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 35.839 | 2244.607 | 3227.647 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1473 | 1473 | 0 | 38.655 | 2179.071 | 3276.799 |
| t-437 | 9 | 1 | 71.893 | 10.794 | 1067 | 0 | 649 | 649 | 0 | 35.839 | 2179.071 | 2867.199 |
| t-114 | 14 | 1 | 60.752 | 14.008 | 1025 | 0 | 877 | 877 | 0 | 34.303 | 2162.687 | 2588.671 |
