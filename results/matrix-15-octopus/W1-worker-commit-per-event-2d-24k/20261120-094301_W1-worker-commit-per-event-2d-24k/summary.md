# W1-worker-commit-per-event-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.313s, closing 2.309s, drain 5.201s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1010024 | 1010024 | 16533.006 | 16268.312 | 19426.1 | 0 | 0 | 0 | 312 | 5111 | 367089 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 725700 | 13941.646 | 13910.015 | 19464.191 | 24281.087 | 28196.863 | 32997.375 |
| e2e_from_send | 725700 | 582.028 | 274.431 | 1060.863 | 6438.911 | 11059.199 | 17907.711 |
| producer_lag | 725700 | 13359.618 | 13492.223 | 18677.759 | 20611.071 | 21135.359 | 21856.255 |
| publish_call | 1010024 | 109.344 | 94.335 | 201.727 | 363.007 | 489.471 | 816.639 |
| queue_wait | 725700 | 541.668 | 241.535 | 974.847 | 6410.239 | 11034.623 | 17907.711 |
| broker_dwell | 725700 | 467.83 | 156.287 | 907.775 | 6348.799 | 10960.895 | 17858.559 |
| receive_call | 722485 | 44.907 | 2.351 | 133.503 | 370.431 | 489.215 | 551.423 |
| ctx_load | 725700 | 14.198 | 10.007 | 31.247 | 64.479 | 84.479 | 216.703 |
| process | 725700 | 10.596 | 0.001 | 0.003 | 0.029 | 2615.295 | 3024.895 |
| ctx_commit | 725700 | 15.501 | 10.319 | 34.431 | 73.215 | 138.367 | 416.255 |
| ack | 724771 | 0.011 | 0.003 | 0.007 | 0.037 | 0.913 | 73.471 |
| service | 725700 | 40.36 | 22.383 | 63.711 | 135.551 | 2648.063 | 3160.063 |
| e2e_large | 211 | 15059.187 | 15155.199 | 20398.079 | 22544.383 | 23298.047 | 23298.047 |
| ready_wait | 725700 | 144.007 | 105.151 | 293.631 | 780.287 | 1425.407 | 2091.007 |
| blocked | 725700 | 140.102 | 101.375 | 287.743 | 774.143 | 1423.359 | 2091.007 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16268.312 | 18093.8 |
| applied_total | 559347.898 | 954042 |
| bench_cpu_cores | 3.545 | 4.091 |
| bench_cpu_pct | 14.772 | 17.05 |
| bench_gc_gen0 | 3.204 | 5 |
| bench_gc_gen1 | 3.163 | 5 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 976.4 | 1282 |
| bench_mem_mb | 1509.376 | 1630.6 |
| bench_threadpool_queue | 71.694 | 488 |
| bench_threadpool_threads | 42.571 | 44 |
| broker_backlog | 6764.02 | 14921 |
| ctr_eb_mosquitto_cpu_pct | 40.522 | 42.88 |
| ctr_eb_mosquitto_mem_mb | 8.457 | 25.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 33.505 | 36.74 |
| ctr_eb_pgbouncer_2_mem_mb | 15.886 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 31.288 | 34.54 |
| ctr_eb_pgbouncer_3_mem_mb | 15.637 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 32.482 | 35.78 |
| ctr_eb_pgbouncer_4_mem_mb | 15.902 | 17 |
| ctr_eb_pgbouncer_cpu_pct | 31.318 | 34.03 |
| ctr_eb_pgbouncer_mem_mb | 16.535 | 17.1 |
| ctr_eb_postgres_cpu_pct | 1195.804 | 1280.33 |
| ctr_eb_postgres_mem_mb | 4343.849 | 4893.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 80.191 | 83.77 |
| in_flight | 6834.918 | 17203 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.88 | 0.93 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1108568.143 | 1949645 |
| octopus_mqtt_received_total | 68208.306 | 112769 |
| octopus_mqtt_sent_total | 33941.735 | 58373 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.571 | 49 |
| pg_active_loads | 5.02 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 43.612 | 106 |
| pg_conn_idle | 183.837 | 217 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.469 | 16 |
| pg_conn_total | 227.449 | 229 |
| pg_db_size_mb | 3224.39 | 3600 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 33899.422 | 36562.6 |
| pg_tup_del_per_s | 40014.553 | 44874.2 |
| pg_tup_fetch_per_s | 644599.988 | 741188.8 |
| pg_tup_ins_per_s | 49790.6 | 59373.4 |
| pg_tup_upd_per_s | 44038.076 | 46857.5 |
| pg_wal_mb_per_s | 95.04 | 133.49 |
| pgb_cl_active | 222.286 | 269 |
| pgb_cl_waiting | 28.714 | 199 |
| pgb_maxwait_ms | 3.026 | 18.8 |
| pgb_sv_active | 19.776 | 48 |
| pgb_sv_idle | 28.224 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16533.006 | 21304.7 |
| produced_total | 566182.816 | 969619 |
| publish_mb_per_s | 46.324 | 64.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 139,950 records are Completed |
| record_counts | pass | 139,950 records found, 139,950 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 312 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,010,024 events applied by workers, 1,010,024 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 367,089 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 774 ms, max 2090 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15740 seq 10: ready 147 ms before start, 147 ms of it with an idle slot in group 491
- [cross_source_blocking] source 9116 seq 17: ready 141 ms before start, 141 ms of it with an idle slot in group 315
- [cross_source_blocking] source 4476 seq 9: ready 145 ms before start, 145 ms of it with an idle slot in group 154
- [cross_source_blocking] source 7260 seq 9: ready 143 ms before start, 143 ms of it with an idle slot in group 248
- [cross_source_blocking] source 12252 seq 7: ready 147 ms before start, 147 ms of it with an idle slot in group 389
- [cross_source_blocking] source 14588 seq 17: ready 116 ms before start, 116 ms of it with an idle slot in group 461
- [cross_source_blocking] source 7132 seq 16: ready 114 ms before start, 114 ms of it with an idle slot in group 242
- [cross_source_blocking] source 15580 seq 20: ready 102 ms before start, 102 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15548 seq 19: ready 102 ms before start, 102 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10236 seq 18: ready 156 ms before start, 156 ms of it with an idle slot in group 344
- [cross_source_blocking] source 1948 seq 20: ready 129 ms before start, 129 ms of it with an idle slot in group 55
- [cross_source_blocking] source 14236 seq 41: ready 150 ms before start, 150 ms of it with an idle slot in group 456
- [cross_source_blocking] source 12636 seq 6: ready 161 ms before start, 109 ms of it with an idle slot in group 397
- [cross_source_blocking] source 8509 seq 15: ready 106 ms before start, 106 ms of it with an idle slot in group 292
- [cross_source_blocking] source 14652 seq 21: ready 104 ms before start, 104 ms of it with an idle slot in group 464
- [cross_source_blocking] source 6749 seq 28: ready 136 ms before start, 136 ms of it with an idle slot in group 230
- [cross_source_blocking] source 1532 seq 19: ready 145 ms before start, 126 ms of it with an idle slot in group 45
- [cross_source_blocking] source 9093 seq 63: ready 136 ms before start, 136 ms of it with an idle slot in group 314
- [cross_source_blocking] source 2277 seq 44: ready 146 ms before start, 146 ms of it with an idle slot in group 65
- [cross_source_blocking] source 3589 seq 24: ready 147 ms before start, 140 ms of it with an idle slot in group 121

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 19989 | 19989 | 0 | 15925.247 | 29622.271 | 33030.143 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 8358 | 8358 | 0 | 16646.143 | 28835.839 | 30146.559 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 7599 | 7599 | 0 | 16908.287 | 27918.335 | 28835.839 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 17088 | 17088 | 0 | 14876.671 | 27525.119 | 29753.343 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 9258 | 9258 | 0 | 14221.311 | 26869.759 | 29097.983 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 12205 | 12205 | 0 | 14221.311 | 25690.111 | 27918.335 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 15058 | 15058 | 0 | 14221.311 | 25559.039 | 27262.975 |
| t-425 | 89 | 5 | 155.428 | 227.21 | 2911 | 0 | 9278 | 9278 | 0 | 14090.239 | 24641.535 | 25559.039 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 9602 | 9602 | 0 | 14352.383 | 24510.463 | 26869.759 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 5705 | 5705 | 0 | 14614.527 | 24117.247 | 25296.895 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 20711 | 20711 | 0 | 14221.311 | 23986.175 | 27394.047 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 6417 | 6417 | 0 | 14417.919 | 23986.175 | 24772.607 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 5403 | 5403 | 0 | 14417.919 | 23068.671 | 24379.391 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 287 | 287 | 0 | 15269.887 | 23068.671 | 23330.815 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 1864 | 1864 | 0 | 14024.703 | 22937.599 | 23592.959 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 3902 | 3902 | 0 | 14155.775 | 22937.599 | 24117.247 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 10233 | 10233 | 0 | 14090.239 | 22675.455 | 24379.391 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 5256 | 5256 | 0 | 14221.311 | 22544.383 | 23461.887 |
| t-282 | 23 | 2 | 157.882 | 60.759 | 956 | 0 | 2526 | 2526 | 0 | 13762.559 | 22282.239 | 24641.535 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 10193 | 10193 | 0 | 14090.239 | 22282.239 | 23855.103 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 6207 | 6207 | 0 | 14024.703 | 22282.239 | 23330.815 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 7428 | 7428 | 0 | 13959.167 | 22151.167 | 23855.103 |
| t-418 | 6 | 1 | 226.411 | 22.866 | 201 | 0 | 932 | 932 | 0 | 13697.023 | 22151.167 | 22282.239 |
| t-483 | 200 | 6 | 76.95 | 259.099 | 2470 | 0 | 10922 | 10922 | 0 | 13959.167 | 22151.167 | 24248.319 |
| t-484 | 6 | 1 | 140.142 | 14.186 | 232 | 0 | 584 | 584 | 0 | 14221.311 | 22020.095 | 22544.383 |
