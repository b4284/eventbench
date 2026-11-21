# L2a-local-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.115s, closing 1.364s, drain 2.171s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000523 | 1000523 | 15955.818 | 16239.598 | 26321.9 | 0 | 0 | 0 | 458 | 5163 | 171653 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797158 | 302.828 | 111.039 | 702.463 | 3051.519 | 5255.167 | 8495.103 |
| e2e_from_send | 797158 | 294.61 | 104.703 | 687.103 | 3037.183 | 5234.687 | 8478.719 |
| producer_lag | 797158 | 8.219 | 2.087 | 17.535 | 123.775 | 256.767 | 672.767 |
| publish_call | 1000523 | 42.373 | 29.471 | 89.599 | 202.367 | 316.159 | 679.935 |
| queue_wait | 797158 | 245.986 | 56.767 | 605.695 | 2934.783 | 5169.151 | 8462.335 |
| broker_dwell | 797158 | 219.534 | 27.247 | 569.855 | 2899.967 | 5136.383 | 8454.143 |
| receive_call | 854891 | 37.831 | 16.719 | 91.967 | 326.911 | 470.783 | 552.447 |
| ctx_load | 797158 | 10.951 | 7.675 | 22.271 | 52.511 | 220.287 | 225.791 |
| process | 797158 | 10.749 | 0.002 | 0.004 | 0.057 | 2621.439 | 3147.775 |
| ctx_commit | 797158 | 26.762 | 19.599 | 58.943 | 108.991 | 164.863 | 291.071 |
| ack | 797137 | 0.009 | 0.004 | 0.006 | 0.04 | 0.685 | 41.439 |
| service | 797158 | 48.624 | 30.943 | 76.863 | 152.959 | 2660.351 | 3217.407 |
| e2e_large | 356 | 2580.181 | 2545.663 | 3508.223 | 4603.903 | 5812.223 | 5812.223 |
| ready_wait | 797158 | 85.954 | 22.607 | 262.143 | 688.639 | 1230.847 | 2168.831 |
| blocked | 797158 | 69.584 | 21.135 | 214.271 | 526.847 | 866.815 | 1599.487 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16239.598 | 19961 |
| applied_total | 574645.49 | 959195 |
| bench_cpu_cores | 5.822 | 6.68 |
| bench_cpu_pct | 24.258 | 27.83 |
| bench_gc_gen0 | 2.102 | 4 |
| bench_gc_gen1 | 0.633 | 2 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 1506.522 | 2016.2 |
| bench_mem_mb | 2216.118 | 2649.8 |
| bench_threadpool_queue | 123.898 | 853 |
| bench_threadpool_threads | 42.959 | 44 |
| broker_backlog | 8900.163 | 24743 |
| ctr_eb_mosquitto_cpu_pct | 65.709 | 72.2 |
| ctr_eb_mosquitto_mem_mb | 10.153 | 29.5 |
| ctr_eb_mssql_cpu_pct | 3.432 | 4.64 |
| ctr_eb_mssql_mem_mb | 7502.678 | 7504.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.132 | 23.13 |
| ctr_eb_pgbouncer_2_mem_mb | 15.653 | 16.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.619 | 21.98 |
| ctr_eb_pgbouncer_3_mem_mb | 15.447 | 16.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.732 | 25.02 |
| ctr_eb_pgbouncer_4_mem_mb | 16.029 | 16.9 |
| ctr_eb_pgbouncer_cpu_pct | 20.551 | 23.78 |
| ctr_eb_pgbouncer_mem_mb | 15.669 | 16.4 |
| ctr_eb_postgres_cpu_pct | 786.079 | 945.83 |
| ctr_eb_postgres_mem_mb | 5698.949 | 6120.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.373 | 80.62 |
| in_flight | 4161.286 | 15472 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.565 | 1.71 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1128044.408 | 1873442 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 221802.653 | 371507 |
| octopus_mqtt_sent_total | 323779.776 | 554040 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7 | 19 |
| pg_active_loads | 3.857 | 8 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.988 | 100 |
| pg_conn_active | 20.878 | 63 |
| pg_conn_idle | 234.837 | 252 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.163 | 6 |
| pg_conn_total | 255.714 | 258 |
| pg_db_size_mb | 3761.006 | 5103.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18620.31 | 21141.8 |
| pg_tup_del_per_s | 29550.496 | 39018.1 |
| pg_tup_fetch_per_s | 214655.541 | 262599.4 |
| pg_tup_ins_per_s | 41104.292 | 54140.6 |
| pg_tup_upd_per_s | 41822.345 | 46319.9 |
| pg_wal_mb_per_s | 108.038 | 158.5 |
| pgb_cl_active | 233.224 | 272 |
| pgb_cl_waiting | 13.306 | 108 |
| pgb_maxwait_ms | 0.646 | 3.92 |
| pgb_sv_active | 20.51 | 48 |
| pgb_sv_idle | 27.49 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15955.818 | 17784.5 |
| produced_total | 578806.776 | 961734 |
| publish_mb_per_s | 51.882 | 80.4 |

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
| runtime_cross_source_blocking | **FAIL** | 171,653 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 527 ms, max 1599 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11279 seq 11: ready 119 ms before start, 119 ms of it with an idle slot in group 367
- [cross_source_blocking] source 15728 seq 19: ready 118 ms before start, 107 ms of it with an idle slot in group 490
- [cross_source_blocking] source 5689 seq 20: ready 124 ms before start, 114 ms of it with an idle slot in group 196
- [cross_source_blocking] source 7239 seq 27: ready 122 ms before start, 107 ms of it with an idle slot in group 246
- [cross_source_blocking] source 10837 seq 12: ready 111 ms before start, 101 ms of it with an idle slot in group 354
- [cross_source_blocking] source 15375 seq 8: ready 122 ms before start, 122 ms of it with an idle slot in group 480
- [cross_source_blocking] source 2891 seq 10: ready 122 ms before start, 109 ms of it with an idle slot in group 92
- [cross_source_blocking] source 14223 seq 29: ready 123 ms before start, 123 ms of it with an idle slot in group 456
- [cross_source_blocking] source 5525 seq 12: ready 131 ms before start, 131 ms of it with an idle slot in group 189
- [cross_source_blocking] source 9001 seq 18: ready 131 ms before start, 131 ms of it with an idle slot in group 310
- [cross_source_blocking] source 7565 seq 14: ready 130 ms before start, 130 ms of it with an idle slot in group 260
- [cross_source_blocking] source 6349 seq 20: ready 131 ms before start, 119 ms of it with an idle slot in group 216
- [cross_source_blocking] source 3829 seq 11: ready 139 ms before start, 139 ms of it with an idle slot in group 130
- [cross_source_blocking] source 3183 seq 26: ready 136 ms before start, 121 ms of it with an idle slot in group 106
- [cross_source_blocking] source 11168 seq 3: ready 122 ms before start, 109 ms of it with an idle slot in group 364
- [cross_source_blocking] source 3087 seq 13: ready 127 ms before start, 127 ms of it with an idle slot in group 103
- [cross_source_blocking] source 7919 seq 22: ready 127 ms before start, 127 ms of it with an idle slot in group 267
- [cross_source_blocking] source 6057 seq 8: ready 130 ms before start, 130 ms of it with an idle slot in group 207
- [cross_source_blocking] source 3213 seq 9: ready 130 ms before start, 130 ms of it with an idle slot in group 107
- [cross_source_blocking] source 8261 seq 15: ready 131 ms before start, 131 ms of it with an idle slot in group 280

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 950.271 | 6619.135 | 8290.303 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 536.575 | 5865.471 | 7634.943 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9222 | 9222 | 0 | 385.023 | 5406.719 | 8388.607 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 468.991 | 5341.183 | 8454.143 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 305.151 | 5177.343 | 8519.679 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15142 | 15142 | 0 | 315.391 | 5144.575 | 6717.439 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3103 | 3103 | 0 | 610.303 | 5111.807 | 8355.839 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2015.231 | 5079.039 | 5832.703 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 164.863 | 4653.055 | 7438.335 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 342.015 | 4653.055 | 6520.831 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2228.223 | 4521.983 | 5603.327 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 352.255 | 4521.983 | 6586.367 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 261.119 | 4456.447 | 8290.303 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14680 | 14680 | 0 | 282.623 | 4390.911 | 6160.383 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2911 | 2911 | 0 | 311.295 | 4358.143 | 8454.143 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 233.471 | 4358.143 | 6946.815 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 243.711 | 4259.839 | 5636.095 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 244.735 | 4227.071 | 6455.295 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 303.103 | 4227.071 | 6225.919 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 286.719 | 4177.919 | 6815.743 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2675 | 2675 | 0 | 164.863 | 4128.767 | 6193.151 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20598 | 20598 | 0 | 278.527 | 4046.847 | 7110.655 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 220.159 | 4014.079 | 6520.831 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16729 | 16729 | 0 | 205.823 | 3932.159 | 5898.239 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2146.303 | 3932.159 | 4587.519 |
