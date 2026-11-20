# M2-mac-app-16k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.179s, closing 1.728s, drain 1.404s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000190 | 1000190 | 16098.584 | 16116.869 | 20911.3 | 0 | 0 | 0 | 458 | 5161 | 12216 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796820 | 496.695 | 142.975 | 1519.615 | 2316.287 | 3635.199 | 6238.207 |
| e2e_from_send | 796820 | 125.254 | 73.919 | 241.151 | 1440.767 | 2861.055 | 5500.927 |
| producer_lag | 796820 | 371.441 | 1.06 | 1311.743 | 2022.399 | 2289.663 | 2650.111 |
| publish_call | 1000190 | 66.802 | 38.687 | 158.207 | 462.591 | 628.223 | 831.487 |
| queue_wait | 796820 | 97.506 | 54.719 | 207.231 | 834.047 | 2430.975 | 4706.303 |
| broker_dwell | 796820 | 0.986 | 0.001 | 0.001 | 0.001 | 398.847 | 2678.783 |
| receive_call | 905266 | 39.356 | 16.943 | 100.223 | 308.479 | 466.687 | 519.423 |
| ctx_load | 796820 | 6.355 | 5.359 | 11.751 | 17.039 | 90.879 | 219.135 |
| process | 796820 | 10.726 | 0.001 | 0.003 | 0.015 | 2617.343 | 3002.367 |
| ctx_commit | 796820 | 10.66 | 8.607 | 19.167 | 34.079 | 265.727 | 797.183 |
| ack | 651273 | 0.002 | 0.001 | 0.003 | 0.009 | 0.04 | 13.095 |
| service | 796820 | 27.749 | 14.719 | 29.951 | 60.351 | 2641.919 | 3508.223 |
| e2e_large | 356 | 3134.074 | 3033.087 | 4521.983 | 6041.599 | 6238.207 | 6238.207 |
| ready_wait | 796820 | 15.06 | 4.763 | 46.015 | 118.399 | 209.663 | 1033.215 |
| blocked | 796820 | 14.235 | 4.747 | 43.807 | 112.447 | 180.479 | 504.063 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16116.869 | 20433.7 |
| applied_total | 571419.816 | 960669 |
| bench_cpu_cores | 5.532 | 7.211 |
| bench_cpu_pct | 69.155 | 90.14 |
| bench_gc_gen0 | 1.408 | 3 |
| bench_gc_gen1 | 1 | 3 |
| bench_gc_gen2 | 0.51 | 1 |
| bench_gc_heap_mb | 1036.951 | 1807.6 |
| bench_mem_mb | 2029.59 | 2545.5 |
| bench_threadpool_queue | 8.776 | 190 |
| bench_threadpool_threads | 21.429 | 28 |
| broker_backlog | 2462.816 | 3895 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1064.265 | 2559 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 1137808.429 | 1960289 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 160545.918 | 325347 |
| octopus_mqtt_sent_total | 305126.776 | 585454 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 2.735 | 8 |
| pg_active_loads | 3.571 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 10.265 | 51 |
| pg_conn_idle | 244.796 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 255.061 | 256 |
| pg_db_size_mb | 3522.541 | 3958.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22407.369 | 24907.4 |
| pg_tup_del_per_s | 43850.343 | 52385.7 |
| pg_tup_fetch_per_s | 516012.273 | 718471.8 |
| pg_tup_ins_per_s | 55370.406 | 64653.1 |
| pg_tup_upd_per_s | 42224.514 | 48745.7 |
| pg_wal_mb_per_s | 108.471 | 153.68 |
| produced_per_s | 16098.584 | 21980.6 |
| produced_total | 572484.082 | 961094 |
| publish_mb_per_s | 52.511 | 70.04 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,837 records are Completed |
| record_counts | pass | 138,837 records found, 138,837 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,190 events applied by workers, 1,000,190 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 12,216 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 112 ms, max 504 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6273 seq 9: ready 127 ms before start, 109 ms of it with an idle slot in group 214
- [cross_source_blocking] source 3201 seq 29: ready 127 ms before start, 126 ms of it with an idle slot in group 106
- [cross_source_blocking] source 5614 seq 19: ready 101 ms before start, 101 ms of it with an idle slot in group 194
- [cross_source_blocking] source 3618 seq 16: ready 101 ms before start, 101 ms of it with an idle slot in group 122
- [cross_source_blocking] source 4593 seq 11: ready 127 ms before start, 126 ms of it with an idle slot in group 160
- [cross_source_blocking] source 14594 seq 15: ready 100 ms before start, 100 ms of it with an idle slot in group 461
- [cross_source_blocking] source 8462 seq 20: ready 101 ms before start, 101 ms of it with an idle slot in group 287
- [cross_source_blocking] source 7494 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 259
- [cross_source_blocking] source 10274 seq 17: ready 100 ms before start, 100 ms of it with an idle slot in group 344
- [cross_source_blocking] source 3886 seq 12: ready 100 ms before start, 100 ms of it with an idle slot in group 131
- [cross_source_blocking] source 8098 seq 28: ready 100 ms before start, 100 ms of it with an idle slot in group 273
- [cross_source_blocking] source 8710 seq 14: ready 101 ms before start, 101 ms of it with an idle slot in group 303
- [cross_source_blocking] source 15482 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15022 seq 24: ready 101 ms before start, 101 ms of it with an idle slot in group 469
- [cross_source_blocking] source 2234 seq 32: ready 101 ms before start, 101 ms of it with an idle slot in group 65
- [cross_source_blocking] source 10390 seq 17: ready 101 ms before start, 101 ms of it with an idle slot in group 347
- [cross_source_blocking] source 3962 seq 9: ready 100 ms before start, 100 ms of it with an idle slot in group 131
- [cross_source_blocking] source 10874 seq 12: ready 100 ms before start, 100 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10158 seq 16: ready 100 ms before start, 100 ms of it with an idle slot in group 344
- [cross_source_blocking] source 14242 seq 36: ready 101 ms before start, 101 ms of it with an idle slot in group 456

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2572.287 | 6193.151 | 6193.151 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2572.287 | 5505.023 | 6094.847 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2555.903 | 5439.487 | 6258.687 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2375.679 | 5177.343 | 5537.791 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 162.815 | 4079.615 | 4423.679 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 739 | 739 | 0 | 203.775 | 3866.623 | 5636.095 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 144.383 | 3751.935 | 5472.255 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 260 | 260 | 0 | 129.023 | 3489.791 | 3932.159 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 133.119 | 3211.263 | 4849.663 |
| t-338 | 6 | 1 | 26.945 | 2.753 | 156 | 0 | 181 | 181 | 0 | 159.743 | 3194.879 | 4227.071 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1811 | 1811 | 0 | 150.527 | 3112.959 | 4521.983 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 2398 | 2398 | 0 | 160.767 | 2965.503 | 5046.271 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 616 | 616 | 0 | 247.807 | 2932.735 | 3686.399 |
| t-360 | 14 | 1 | 82.459 | 19.32 | 274 | 0 | 1186 | 1186 | 0 | 122.879 | 2916.351 | 4489.215 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 942 | 942 | 0 | 157.695 | 2883.583 | 3670.015 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 134.143 | 2867.199 | 3784.703 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3804 | 3804 | 0 | 205.823 | 2850.815 | 4653.055 |
| t-339 | 17 | 1 | 15.431 | 4.427 | 134 | 0 | 323 | 323 | 0 | 138.239 | 2834.431 | 4128.767 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1016 | 1016 | 0 | 150.527 | 2785.279 | 4325.375 |
| t-300 | 25 | 1 | 25.695 | 10.875 | 372 | 0 | 720 | 720 | 0 | 215.039 | 2785.279 | 3801.087 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4961 | 4961 | 0 | 153.599 | 2703.359 | 5079.039 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 826 | 826 | 0 | 95.231 | 2686.975 | 4145.151 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5540 | 5540 | 0 | 182.271 | 2670.591 | 4177.919 |
| t-246 | 32 | 3 | 232.775 | 122.986 | 494 | 0 | 7644 | 7644 | 0 | 179.199 | 2670.591 | 5537.791 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 132.095 | 2670.591 | 2981.887 |
