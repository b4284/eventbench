# D4-morning-everything-pgrestart-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.256s, closing 2.816s, drain 7.065s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 991087 | 991087 | 16627.379 | 15312.696 | 19307.6 | 0 | 0 | 0 | 456 | 5126 | 485797 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 787792 | 3788.683 | 3166.207 | 7802.879 | 15294.463 | 20217.855 | 27246.591 |
| e2e_from_send | 787792 | 2082.454 | 571.391 | 6754.303 | 14245.887 | 18841.599 | 26263.551 |
| producer_lag | 787792 | 1706.226 | 1650.687 | 3545.087 | 4272.127 | 4538.367 | 5033.983 |
| publish_call | 991087 | 104.89 | 87.423 | 195.967 | 366.591 | 596.991 | 921.087 |
| queue_wait | 787792 | 2016.256 | 505.343 | 6692.863 | 14196.735 | 18792.447 | 26247.167 |
| broker_dwell | 787792 | 1936.688 | 418.047 | 6619.135 | 14131.199 | 18694.143 | 26198.015 |
| receive_call | 829667 | 36.999 | 15.695 | 90.047 | 329.215 | 489.727 | 575.487 |
| ctx_load | 787792 | 14.687 | 10.975 | 30.559 | 62.239 | 96.319 | 191.487 |
| process | 787792 | 10.784 | 0.002 | 0.005 | 0.087 | 2619.391 | 3033.087 |
| ctx_commit | 787792 | 40.607 | 34.783 | 77.951 | 135.807 | 196.479 | 707.583 |
| ack | 787150 | 0.012 | 0.003 | 0.006 | 0.043 | 1.368 | 106.431 |
| service | 787792 | 66.2 | 49.983 | 99.775 | 181.631 | 2682.879 | 3534.847 |
| e2e_large | 354 | 4647.894 | 4759.551 | 6672.383 | 7876.607 | 9256.959 | 9256.959 |
| ready_wait | 787792 | 319.573 | 206.079 | 773.631 | 1736.703 | 2719.743 | 3545.087 |
| blocked | 787792 | 258.726 | 169.727 | 638.975 | 1226.751 | 1993.727 | 2881.535 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15312.696 | 19057 |
| applied_total | 514418 | 872097 |
| bench_cpu_cores | 4.127 | 4.665 |
| bench_cpu_pct | 17.195 | 19.44 |
| bench_gc_gen0 | 3.062 | 4 |
| bench_gc_gen1 | 3.062 | 4 |
| bench_gc_gen2 | 0.667 | 2 |
| bench_gc_heap_mb | 1100.01 | 1377 |
| bench_mem_mb | 1655.119 | 1821.1 |
| bench_threadpool_queue | 128.667 | 726 |
| bench_threadpool_threads | 43.167 | 44 |
| broker_backlog | 30519.833 | 70178 |
| ctr_eb_mosquitto_cpu_pct | 67.792 | 71.62 |
| ctr_eb_mosquitto_mem_mb | 9.281 | 20.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 25.028 | 28.81 |
| ctr_eb_pgbouncer_2_mem_mb | 15.094 | 16.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 24.358 | 27.16 |
| ctr_eb_pgbouncer_3_mem_mb | 14.956 | 16 |
| ctr_eb_pgbouncer_4_cpu_pct | 25.736 | 28.21 |
| ctr_eb_pgbouncer_4_mem_mb | 15.435 | 16.7 |
| ctr_eb_pgbouncer_cpu_pct | 25.073 | 27.21 |
| ctr_eb_pgbouncer_mem_mb | 16.506 | 17.9 |
| ctr_eb_postgres_cpu_pct | 1014.149 | 1067.23 |
| ctr_eb_postgres_mem_mb | 4195.058 | 5291.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.476 | 82.58 |
| in_flight | 27289.062 | 63501 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.874 | 1.97 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1035970.021 | 1844400 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 152356.062 | 272124 |
| octopus_mqtt_sent_total | 294730.958 | 518263 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 9.271 | 15 |
| pg_active_loads | 14.667 | 94 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.999 | 100 |
| pg_conn_active | 32.688 | 77 |
| pg_conn_idle | 225.375 | 251 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.292 | 2 |
| pg_conn_total | 258.062 | 260 |
| pg_db_size_mb | 3538.892 | 4113.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18068.846 | 21300.7 |
| pg_tup_del_per_s | 42333.56 | 48745.5 |
| pg_tup_fetch_per_s | 523141.215 | 606394.8 |
| pg_tup_ins_per_s | 56199.796 | 66086.9 |
| pg_tup_upd_per_s | 40177.298 | 47342.4 |
| pg_wal_mb_per_s | 112.826 | 150.46 |
| pgb_cl_active | 240.938 | 262 |
| pgb_cl_waiting | 10.625 | 170 |
| pgb_maxwait_ms | 1.007 | 16.11 |
| pgb_sv_active | 8.938 | 48 |
| pgb_sv_idle | 39.062 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16627.379 | 21989.2 |
| produced_total | 541707.062 | 935598 |
| publish_mb_per_s | 54.69 | 86.39 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 137,765 records are Completed |
| record_counts | pass | 137,765 records found, 137,765 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 456 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 991,087 events applied by workers, 991,087 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 485,797 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1227 ms, max 2880 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4302 seq 38: ready 146 ms before start, 129 ms of it with an idle slot in group 147
- [cross_source_blocking] source 4334 seq 32: ready 145 ms before start, 129 ms of it with an idle slot in group 147
- [cross_source_blocking] source 5102 seq 7: ready 294 ms before start, 278 ms of it with an idle slot in group 178
- [cross_source_blocking] source 9358 seq 5: ready 293 ms before start, 293 ms of it with an idle slot in group 325
- [cross_source_blocking] source 5326 seq 29: ready 293 ms before start, 244 ms of it with an idle slot in group 182
- [cross_source_blocking] source 7278 seq 11: ready 166 ms before start, 166 ms of it with an idle slot in group 249
- [cross_source_blocking] source 14094 seq 19: ready 295 ms before start, 264 ms of it with an idle slot in group 450
- [cross_source_blocking] source 8686 seq 13: ready 118 ms before start, 118 ms of it with an idle slot in group 301
- [cross_source_blocking] source 2254 seq 29: ready 293 ms before start, 272 ms of it with an idle slot in group 65
- [cross_source_blocking] source 11406 seq 6: ready 295 ms before start, 229 ms of it with an idle slot in group 372
- [cross_source_blocking] source 3182 seq 22: ready 119 ms before start, 119 ms of it with an idle slot in group 105
- [cross_source_blocking] source 8046 seq 23: ready 167 ms before start, 167 ms of it with an idle slot in group 271
- [cross_source_blocking] source 10766 seq 12: ready 295 ms before start, 295 ms of it with an idle slot in group 353
- [cross_source_blocking] source 15854 seq 30: ready 171 ms before start, 171 ms of it with an idle slot in group 496
- [cross_source_blocking] source 2830 seq 15: ready 300 ms before start, 298 ms of it with an idle slot in group 88
- [cross_source_blocking] source 3950 seq 17: ready 165 ms before start, 165 ms of it with an idle slot in group 131
- [cross_source_blocking] source 11982 seq 13: ready 301 ms before start, 301 ms of it with an idle slot in group 383
- [cross_source_blocking] source 5038 seq 15: ready 297 ms before start, 290 ms of it with an idle slot in group 175
- [cross_source_blocking] source 10126 seq 20: ready 301 ms before start, 301 ms of it with an idle slot in group 344
- [cross_source_blocking] source 7182 seq 25: ready 161 ms before start, 161 ms of it with an idle slot in group 245

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3166 | 3166 | 0 | 7045.119 | 22806.527 | 27262.975 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9127 | 9127 | 0 | 6946.815 | 21626.879 | 27000.831 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9001 | 9001 | 0 | 6586.367 | 21495.807 | 27131.903 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5926 | 5926 | 0 | 6094.847 | 20447.231 | 24117.247 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6038 | 6038 | 0 | 5570.559 | 20185.087 | 25559.039 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2803 | 2803 | 0 | 5406.719 | 20054.015 | 22806.527 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5578 | 5578 | 0 | 5570.559 | 19660.799 | 22413.311 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3065 | 3065 | 0 | 4587.519 | 19398.655 | 21626.879 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2940 | 2940 | 0 | 6225.919 | 19398.655 | 23199.743 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11743 | 11743 | 0 | 5636.095 | 19398.655 | 22282.239 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 14984 | 14984 | 0 | 6225.919 | 19136.511 | 22937.599 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14562 | 14562 | 0 | 5996.543 | 19005.439 | 23724.031 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19577 | 19577 | 0 | 5275.647 | 18087.935 | 23855.103 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3081 | 3081 | 0 | 5505.023 | 17694.719 | 20185.087 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11054 | 11054 | 0 | 5242.879 | 17432.575 | 21233.663 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5779 | 5779 | 0 | 4718.591 | 17170.431 | 23724.031 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20416 | 20416 | 0 | 5177.343 | 17039.359 | 20840.447 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16544 | 16544 | 0 | 4980.735 | 16908.287 | 21626.879 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5511 | 5511 | 0 | 4816.895 | 16908.287 | 19398.655 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4912 | 4912 | 0 | 3768.319 | 16908.287 | 22282.239 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2799 | 2799 | 0 | 4685.823 | 16777.215 | 19660.799 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5480 | 5480 | 0 | 5341.183 | 16580.607 | 19005.439 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2695 | 2695 | 0 | 5242.879 | 16515.071 | 20185.087 |
| t-131 | 140 | 4 | 75.448 | 177.75 | 961 | 0 | 10856 | 10856 | 0 | 4751.359 | 16449.535 | 20185.087 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5850 | 5850 | 0 | 5865.471 | 16318.463 | 19791.871 |
