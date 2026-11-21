# C0b-before-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.344s, closing 2.066s, drain 2.522s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000521 | 1000521 | 15980.612 | 16199.916 | 19595.2 | 0 | 0 | 0 | 458 | 5163 | 344661 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797153 | 1219.563 | 231.807 | 3907.583 | 10723.327 | 15261.695 | 24657.919 |
| e2e_from_send | 797153 | 1181.249 | 206.079 | 3846.143 | 10665.983 | 15228.927 | 24657.919 |
| producer_lag | 797153 | 38.314 | 3.161 | 54.911 | 690.687 | 1138.687 | 1806.335 |
| publish_call | 1000521 | 59.206 | 42.911 | 120.191 | 285.183 | 491.263 | 855.551 |
| queue_wait | 797153 | 1126.217 | 150.783 | 3786.751 | 10608.639 | 15155.199 | 24608.767 |
| broker_dwell | 797153 | 1091.514 | 104.831 | 3745.791 | 10575.871 | 15106.047 | 24592.383 |
| receive_call | 820953 | 38.05 | 16.447 | 92.735 | 325.375 | 470.271 | 577.023 |
| ctx_load | 797153 | 13.763 | 10.143 | 28.815 | 60.127 | 81.535 | 177.279 |
| process | 797153 | 10.754 | 0.002 | 0.004 | 0.058 | 2621.439 | 3026.943 |
| ctx_commit | 797153 | 30.253 | 25.023 | 58.751 | 105.727 | 158.463 | 346.367 |
| ack | 797122 | 0.01 | 0.004 | 0.007 | 0.042 | 0.877 | 55.551 |
| service | 797153 | 55.032 | 39.231 | 79.615 | 145.791 | 2666.495 | 3229.695 |
| e2e_large | 356 | 2734.706 | 2691.071 | 3792.895 | 4812.799 | 6361.087 | 6361.087 |
| ready_wait | 797153 | 203.96 | 73.343 | 563.199 | 1340.415 | 1950.719 | 2951.167 |
| blocked | 797153 | 170.278 | 64.799 | 475.903 | 1018.367 | 1504.255 | 2004.991 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16199.916 | 19595.2 |
| applied_total | 559544.837 | 947775 |
| bench_cpu_cores | 5.412 | 6.046 |
| bench_cpu_pct | 22.548 | 25.19 |
| bench_gc_gen0 | 1.98 | 4 |
| bench_gc_gen1 | 0.612 | 2 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 1561.684 | 1861.2 |
| bench_mem_mb | 2368.896 | 2781.8 |
| bench_threadpool_queue | 156.286 | 712 |
| bench_threadpool_threads | 41.286 | 44 |
| broker_backlog | 22078.98 | 35121 |
| ctr_eb_mosquitto_cpu_pct | 64.767 | 70.12 |
| ctr_eb_mosquitto_mem_mb | 7.514 | 17.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.626 | 22.15 |
| ctr_eb_pgbouncer_2_mem_mb | 15.431 | 16 |
| ctr_eb_pgbouncer_3_cpu_pct | 20.695 | 22.62 |
| ctr_eb_pgbouncer_3_mem_mb | 15.424 | 16.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 20.472 | 21.3 |
| ctr_eb_pgbouncer_4_mem_mb | 15.463 | 16.3 |
| ctr_eb_pgbouncer_cpu_pct | 20.694 | 22.61 |
| ctr_eb_pgbouncer_mem_mb | 15.322 | 16.2 |
| ctr_eb_postgres_cpu_pct | 884.486 | 921.61 |
| ctr_eb_postgres_mem_mb | 5792.102 | 5888 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.099 | 81.94 |
| in_flight | 18703.857 | 32687 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.774 | 1.86 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1115198.755 | 1949016 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 208149.939 | 354000 |
| octopus_mqtt_sent_total | 305210 | 550870 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.143 | 12 |
| pg_active_loads | 9.878 | 46 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 27.306 | 48 |
| pg_conn_idle | 231.939 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.531 | 15 |
| pg_conn_total | 259.245 | 260 |
| pg_db_size_mb | 3628.508 | 4051.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20239.173 | 23709.3 |
| pg_tup_del_per_s | 44292.145 | 55642.8 |
| pg_tup_fetch_per_s | 575964.896 | 685078.1 |
| pg_tup_ins_per_s | 56056.202 | 75782.4 |
| pg_tup_upd_per_s | 43459.416 | 50224.4 |
| pg_wal_mb_per_s | 117.622 | 225.51 |
| pgb_cl_active | 247.408 | 252 |
| pgb_cl_waiting | 2.449 | 40 |
| pgb_maxwait_ms | 0.094 | 1.54 |
| pgb_sv_active | 9.755 | 48 |
| pgb_sv_idle | 38.245 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15980.612 | 19151.6 |
| produced_total | 578248.694 | 960643 |
| publish_mb_per_s | 51.895 | 75.87 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,873 records are Completed |
| record_counts | pass | 138,873 records found, 138,873 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,521 events applied by workers, 1,000,521 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 344,661 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1018 ms, max 2004 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5923 seq 12: ready 122 ms before start, 108 ms of it with an idle slot in group 205
- [cross_source_blocking] source 14403 seq 4: ready 118 ms before start, 118 ms of it with an idle slot in group 459
- [cross_source_blocking] source 3327 seq 11: ready 102 ms before start, 102 ms of it with an idle slot in group 113
- [cross_source_blocking] source 9675 seq 4: ready 122 ms before start, 122 ms of it with an idle slot in group 335
- [cross_source_blocking] source 5863 seq 8: ready 121 ms before start, 121 ms of it with an idle slot in group 203
- [cross_source_blocking] source 13867 seq 12: ready 138 ms before start, 108 ms of it with an idle slot in group 439
- [cross_source_blocking] source 13335 seq 7: ready 107 ms before start, 107 ms of it with an idle slot in group 426
- [cross_source_blocking] source 12715 seq 7: ready 123 ms before start, 123 ms of it with an idle slot in group 401
- [cross_source_blocking] source 3207 seq 13: ready 121 ms before start, 121 ms of it with an idle slot in group 107
- [cross_source_blocking] source 11501 seq 33: ready 105 ms before start, 103 ms of it with an idle slot in group 374
- [cross_source_blocking] source 3171 seq 14: ready 123 ms before start, 123 ms of it with an idle slot in group 105
- [cross_source_blocking] source 2859 seq 11: ready 117 ms before start, 117 ms of it with an idle slot in group 91
- [cross_source_blocking] source 7239 seq 27: ready 124 ms before start, 123 ms of it with an idle slot in group 246
- [cross_source_blocking] source 9532 seq 8: ready 123 ms before start, 123 ms of it with an idle slot in group 331
- [cross_source_blocking] source 14470 seq 4: ready 168 ms before start, 122 ms of it with an idle slot in group 460
- [cross_source_blocking] source 7523 seq 4: ready 184 ms before start, 125 ms of it with an idle slot in group 259
- [cross_source_blocking] source 11381 seq 15: ready 128 ms before start, 128 ms of it with an idle slot in group 371
- [cross_source_blocking] source 14425 seq 5: ready 155 ms before start, 137 ms of it with an idle slot in group 460
- [cross_source_blocking] source 11525 seq 37: ready 135 ms before start, 109 ms of it with an idle slot in group 374
- [cross_source_blocking] source 5244 seq 27: ready 146 ms before start, 110 ms of it with an idle slot in group 181

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 5308.415 | 18219.007 | 24117.247 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 4521.983 | 17956.863 | 24772.607 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9221 | 9221 | 0 | 4128.767 | 17563.647 | 19529.727 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 1900.543 | 16252.927 | 17432.575 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 2998.271 | 15466.495 | 19267.583 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 2850.815 | 14483.455 | 18874.367 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2911 | 2911 | 0 | 2228.223 | 14155.775 | 16318.463 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 2457.599 | 14090.239 | 16515.071 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 3604.479 | 14024.703 | 17694.719 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 3358.719 | 13893.631 | 16580.607 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15142 | 15142 | 0 | 3997.695 | 13828.095 | 19529.727 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 3080.191 | 13762.559 | 15728.639 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 2007.039 | 13434.879 | 15728.639 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14679 | 14679 | 0 | 4390.911 | 13369.343 | 15859.711 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11136 | 11136 | 0 | 2916.351 | 13107.199 | 17039.359 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20598 | 20598 | 0 | 2113.535 | 12713.983 | 16121.855 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 4128.767 | 12386.303 | 13631.487 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 2039.807 | 12124.159 | 14614.527 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5829 | 5829 | 0 | 1835.007 | 12124.159 | 13631.487 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2675 | 2675 | 0 | 921.599 | 11993.087 | 15335.423 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6099 | 6099 | 0 | 2424.831 | 11927.551 | 15728.639 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2724 | 2724 | 0 | 2228.223 | 11403.263 | 14614.527 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 1957.887 | 11206.655 | 14286.847 |
| t-022 | 63 | 1 | 37.482 | 39.487 | 162 | 0 | 2576 | 2576 | 0 | 864.255 | 11010.047 | 13434.879 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 2408.447 | 10944.511 | 13631.487 |
