# S2b-split-16k-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 89.297s, closing 1.239s, drain 3.33s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 494713 | 494713 | 7871.365 | 7873.992 | 10996.3 | 0 | 0 | 0 | 47 | 2423 | 1111 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 393733 | 81.679 | 43.039 | 95.231 | 1421.311 | 2801.663 | 6074.367 |
| e2e_from_send | 393733 | 76.352 | 40.319 | 88.127 | 1415.167 | 2797.567 | 6066.175 |
| producer_lag | 393733 | 5.327 | 1.76 | 7.435 | 97.407 | 322.815 | 571.903 |
| publish_call | 494713 | 34.279 | 20.559 | 67.327 | 249.087 | 420.095 | 1028.095 |
| queue_wait | 393733 | 48.783 | 23.727 | 59.103 | 843.775 | 2428.927 | 4673.535 |
| broker_dwell | 393733 | 33.536 | 10.519 | 32.367 | 830.463 | 2416.639 | 4661.247 |
| receive_call | 447820 | 39.237 | 21.663 | 90.367 | 303.103 | 465.919 | 548.351 |
| ctx_load | 393733 | 6.775 | 4.959 | 13.727 | 28.063 | 46.015 | 363.007 |
| process | 393733 | 10.213 | 0.002 | 0.005 | 0.067 | 2609.151 | 3094.527 |
| ctx_commit | 393733 | 10.467 | 7.475 | 21.791 | 47.295 | 84.607 | 363.519 |
| ack | 393716 | 0.01 | 0.004 | 0.009 | 0.056 | 0.696 | 30.815 |
| service | 393733 | 27.57 | 14.215 | 32.959 | 73.087 | 2623.487 | 3102.719 |
| e2e_large | 39 | 2554.512 | 2621.439 | 3291.135 | 6074.367 | 6074.367 | 6074.367 |
| ready_wait | 393733 | 10.582 | 5.531 | 25.151 | 70.783 | 184.191 | 1392.639 |
| blocked | 393733 | 10.191 | 5.511 | 24.463 | 66.495 | 145.791 | 431.615 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7873.992 | 10052.9 |
| applied_total | 285537.49 | 473965 |
| bench_cpu_cores | 3.458 | 4.768 |
| bench_cpu_pct | 14.408 | 19.87 |
| bench_gc_gen0 | 1.49 | 3 |
| bench_gc_gen1 | 0.265 | 2 |
| bench_gc_gen2 | 0.224 | 1 |
| bench_gc_heap_mb | 660.712 | 948.4 |
| bench_mem_mb | 1045.218 | 1330.5 |
| bench_threadpool_queue | 34.714 | 140 |
| bench_threadpool_threads | 38.959 | 44 |
| broker_backlog | 2654.694 | 4913 |
| ctr_eb_mosquitto_cpu_pct | 83.759 | 88.2 |
| ctr_eb_mosquitto_mem_mb | 15.886 | 26.2 |
| ctr_eb_mssql_cpu_pct | 3.887 | 4.52 |
| ctr_eb_mssql_mem_mb | 7497.147 | 7497.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 26.825 | 28.8 |
| ctr_eb_pgbouncer_2_mem_mb | 17.686 | 18.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 26.48 | 28.27 |
| ctr_eb_pgbouncer_3_mem_mb | 17.353 | 18.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.237 | 28.2 |
| ctr_eb_pgbouncer_4_mem_mb | 17.661 | 18.6 |
| ctr_eb_pgbouncer_cpu_pct | 26.95 | 29.25 |
| ctr_eb_pgbouncer_mem_mb | 17.376 | 18.5 |
| ctr_eb_postgres_cpu_pct | 838.671 | 921.68 |
| ctr_eb_postgres_mem_mb | 5681.471 | 5843.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.451 | 78.84 |
| in_flight | 407.122 | 1434 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.272 | 2.35 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 570385.551 | 940624 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 177164.286 | 282662 |
| octopus_mqtt_sent_total | 252379.469 | 418127 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.286 | 18 |
| pg_active_loads | 4.673 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 15.51 | 36 |
| pg_conn_idle | 245.673 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.122 | 2 |
| pg_conn_total | 261.184 | 262 |
| pg_db_size_mb | 3495.535 | 3873.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22124.929 | 23818.6 |
| pg_tup_del_per_s | 28862.855 | 40853.1 |
| pg_tup_fetch_per_s | 207369.135 | 242655.4 |
| pg_tup_ins_per_s | 39789.549 | 56464 |
| pg_tup_upd_per_s | 41480.761 | 44557.1 |
| pg_wal_mb_per_s | 102.561 | 142.35 |
| pgb_cl_active | 441.918 | 467 |
| pgb_cl_waiting | 0.306 | 5 |
| pgb_maxwait_ms | 0.074 | 1.21 |
| pgb_sv_active | 9.755 | 48 |
| pgb_sv_idle | 38.245 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7871.365 | 8986.7 |
| produced_total | 285944.612 | 474319 |
| publish_mb_per_s | 15.815 | 26.74 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 7973/7973 source cursors at the expected last sequence |
| records_complete | pass | all 138,563 records are Completed |
| record_counts | pass | 138,563 records found, 68,937 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 420 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 494,713 events applied by workers, 494,713 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 1,111 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 66 ms, max 431 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4823 seq 5: ready 249 ms before start, 191 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4828 seq 6: ready 269 ms before start, 205 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4862 seq 7: ready 266 ms before start, 201 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4822 seq 7: ready 252 ms before start, 167 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4856 seq 9: ready 245 ms before start, 168 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4814 seq 6: ready 233 ms before start, 147 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4806 seq 12: ready 249 ms before start, 162 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4835 seq 6: ready 241 ms before start, 149 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4855 seq 13: ready 220 ms before start, 135 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4812 seq 13: ready 212 ms before start, 123 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4870 seq 7: ready 199 ms before start, 108 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4818 seq 8: ready 210 ms before start, 112 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4867 seq 6: ready 202 ms before start, 104 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4805 seq 7: ready 211 ms before start, 113 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4841 seq 11: ready 176 ms before start, 105 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4849 seq 9: ready 179 ms before start, 105 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4822 seq 8: ready 184 ms before start, 112 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4820 seq 12: ready 167 ms before start, 113 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4810 seq 10: ready 157 ms before start, 110 ms of it with an idle slot in group 168
- [cross_source_blocking] source 4863 seq 12: ready 170 ms before start, 131 ms of it with an idle slot in group 168

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 88 | 88 | 0 | 1941.503 | 3309.567 | 6094.847 |
| t-346 | 12 | 1 | 34.767 | 6.737 | 1247 | 0 | 404 | 404 | 0 | 40.703 | 2539.519 | 2949.119 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 152 | 152 | 0 | 43.519 | 2490.367 | 2572.287 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3931 | 3931 | 0 | 45.567 | 2408.447 | 3801.087 |
| t-320 | 14 | 1 | 106.83 | 23.68 | 574 | 0 | 1432 | 1432 | 0 | 43.775 | 2375.679 | 2916.351 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5542 | 5542 | 0 | 45.823 | 2342.911 | 3342.335 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 904 | 904 | 0 | 43.519 | 2293.759 | 2932.735 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5630 | 5630 | 0 | 45.311 | 2195.455 | 3063.807 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 919 | 919 | 0 | 42.751 | 2179.071 | 3063.807 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1054 | 1054 | 0 | 45.055 | 2080.767 | 2736.127 |
| t-056 | 7 | 1 | 59.8 | 7.224 | 347 | 0 | 466 | 466 | 0 | 41.215 | 2072.575 | 2490.367 |
| t-288 | 6 | 1 | 71.992 | 7.282 | 214 | 0 | 446 | 446 | 0 | 41.471 | 2056.191 | 3031.039 |
| t-018 | 26 | 1 | 63.129 | 27.754 | 315 | 0 | 1709 | 1709 | 0 | 43.775 | 2015.231 | 3096.575 |
| t-090 | 7 | 1 | 49.513 | 5.825 | 242 | 0 | 386 | 386 | 0 | 42.751 | 1990.655 | 3063.807 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 382 | 382 | 0 | 45.823 | 1982.463 | 2686.975 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 749 | 749 | 0 | 44.799 | 1982.463 | 2785.279 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7472 | 7472 | 0 | 46.335 | 1974.271 | 4685.823 |
| t-482 | 10 | 1 | 83.28 | 14.168 | 2234 | 0 | 877 | 877 | 0 | 41.471 | 1974.271 | 2949.119 |
| t-260 | 30 | 2 | 103.347 | 53.5 | 499 | 0 | 3302 | 3302 | 0 | 42.751 | 1941.503 | 4390.911 |
| t-456 | 21 | 2 | 178.374 | 60.745 | 512 | 0 | 3869 | 3869 | 0 | 44.543 | 1925.119 | 2949.119 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 2618 | 2618 | 0 | 43.263 | 1875.967 | 2867.199 |
| t-380 | 22 | 1 | 96.015 | 34.358 | 132 | 0 | 2098 | 2098 | 0 | 43.775 | 1859.583 | 2916.351 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 6210 | 6210 | 0 | 43.775 | 1851.391 | 2998.271 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 575 | 575 | 0 | 41.983 | 1851.391 | 2834.431 |
| t-160 | 57 | 2 | 71.075 | 67.452 | 548 | 0 | 4335 | 4335 | 0 | 43.263 | 1826.815 | 3112.959 |
