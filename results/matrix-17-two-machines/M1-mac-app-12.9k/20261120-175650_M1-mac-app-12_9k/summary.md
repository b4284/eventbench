# M1-mac-app-12.9k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.25s, closing 1.578s, drain 1.959s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814638 | 814638 | 12978.994 | 12978.763 | 21016.7 | 0 | 0 | 0 | 431 | 4254 | 5444 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642915 | 125.433 | 25.711 | 288.511 | 1558.527 | 2887.679 | 6250.495 |
| e2e_from_send | 642915 | 78.698 | 25.071 | 145.663 | 1254.399 | 2809.855 | 6201.343 |
| producer_lag | 642915 | 46.736 | 0.005 | 57.087 | 1051.647 | 1505.279 | 1884.159 |
| publish_call | 814638 | 36.157 | 11.455 | 89.791 | 350.719 | 757.759 | 899.583 |
| queue_wait | 642915 | 56.922 | 13.303 | 121.535 | 715.263 | 2308.095 | 4100.095 |
| broker_dwell | 642915 | 0.73 | 0.001 | 0.001 | 0.001 | 301.823 | 2111.487 |
| receive_call | 762110 | 42.401 | 17.407 | 111.743 | 336.895 | 474.623 | 510.207 |
| ctx_load | 642915 | 4.501 | 3.179 | 9.127 | 14.583 | 21.935 | 212.735 |
| process | 642915 | 10.899 | 0.001 | 0.003 | 0.012 | 2623.487 | 3002.367 |
| ctx_commit | 642915 | 6.369 | 4.471 | 12.799 | 24.959 | 83.135 | 797.183 |
| ack | 526423 | 0.002 | 0.001 | 0.003 | 0.008 | 0.032 | 20.959 |
| service | 642915 | 21.777 | 8.207 | 21.903 | 42.111 | 2641.919 | 3481.599 |
| e2e_large | 331 | 2735.557 | 2662.399 | 3907.583 | 5267.455 | 6250.495 | 6250.495 |
| ready_wait | 642915 | 10.296 | 2.013 | 30.671 | 100.415 | 296.447 | 2529.279 |
| blocked | 642915 | 9.872 | 2.012 | 29.327 | 92.351 | 288.767 | 885.247 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12978.763 | 17622.9 |
| applied_total | 467483.408 | 777277 |
| bench_cpu_cores | 6.357 | 7.044 |
| bench_cpu_pct | 79.46 | 88.05 |
| bench_gc_gen0 | 1.633 | 3 |
| bench_gc_gen1 | 0.898 | 2 |
| bench_gc_gen2 | 0.49 | 1 |
| bench_gc_heap_mb | 870.894 | 1290.2 |
| bench_mem_mb | 1588.357 | 2119 |
| bench_threadpool_queue | 6.184 | 93 |
| bench_threadpool_threads | 16.776 | 22 |
| broker_backlog | 1585.265 | 4538 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 639.959 | 2644 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 934538.122 | 1574243 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 225600.245 | 402081 |
| octopus_mqtt_sent_total | 329608.184 | 586717 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 2.816 | 11 |
| pg_active_loads | 1.51 | 7 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 8.592 | 25 |
| pg_conn_idle | 246.673 | 255 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.163 | 4 |
| pg_conn_total | 255.265 | 257 |
| pg_db_size_mb | 3373.769 | 3762.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 20298.588 | 22688.5 |
| pg_tup_del_per_s | 36795.512 | 48199.2 |
| pg_tup_fetch_per_s | 375469.129 | 472392.1 |
| pg_tup_ins_per_s | 47100.678 | 62137.1 |
| pg_tup_upd_per_s | 34070.041 | 36159.8 |
| pg_wal_mb_per_s | 92.255 | 144.89 |
| produced_per_s | 12978.994 | 17540.9 |
| produced_total | 468123.367 | 777635 |
| publish_mb_per_s | 44.809 | 70.13 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,598 records are Completed |
| record_counts | pass | 117,598 records found, 117,598 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,638 events applied by workers, 814,638 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 5,444 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 92 ms, max 885 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6384 seq 8: ready 179 ms before start, 179 ms of it with an idle slot in group 218
- [cross_source_blocking] source 1940 seq 9: ready 194 ms before start, 194 ms of it with an idle slot in group 55
- [cross_source_blocking] source 14924 seq 20: ready 186 ms before start, 186 ms of it with an idle slot in group 468
- [cross_source_blocking] source 6224 seq 9: ready 194 ms before start, 194 ms of it with an idle slot in group 212
- [cross_source_blocking] source 4312 seq 17: ready 211 ms before start, 211 ms of it with an idle slot in group 147
- [cross_source_blocking] source 10692 seq 9: ready 175 ms before start, 175 ms of it with an idle slot in group 350
- [cross_source_blocking] source 4904 seq 11: ready 175 ms before start, 175 ms of it with an idle slot in group 169
- [cross_source_blocking] source 5596 seq 14: ready 194 ms before start, 194 ms of it with an idle slot in group 194
- [cross_source_blocking] source 10336 seq 8: ready 197 ms before start, 197 ms of it with an idle slot in group 347
- [cross_source_blocking] source 14312 seq 4: ready 194 ms before start, 194 ms of it with an idle slot in group 458
- [cross_source_blocking] source 8236 seq 17: ready 197 ms before start, 197 ms of it with an idle slot in group 278
- [cross_source_blocking] source 10896 seq 8: ready 184 ms before start, 184 ms of it with an idle slot in group 354
- [cross_source_blocking] source 14860 seq 11: ready 197 ms before start, 197 ms of it with an idle slot in group 465
- [cross_source_blocking] source 13364 seq 11: ready 186 ms before start, 186 ms of it with an idle slot in group 428
- [cross_source_blocking] source 15680 seq 20: ready 182 ms before start, 182 ms of it with an idle slot in group 490
- [cross_source_blocking] source 3840 seq 17: ready 194 ms before start, 194 ms of it with an idle slot in group 131
- [cross_source_blocking] source 15468 seq 10: ready 194 ms before start, 194 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15376 seq 8: ready 182 ms before start, 182 ms of it with an idle slot in group 480
- [cross_source_blocking] source 744 seq 4: ready 194 ms before start, 194 ms of it with an idle slot in group 22
- [cross_source_blocking] source 10168 seq 11: ready 187 ms before start, 187 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 2555.903 | 6094.847 | 6094.847 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 2211.839 | 5079.039 | 5570.559 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 154 | 154 | 0 | 2064.383 | 4685.823 | 6258.687 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 82 | 82 | 0 | 1744.895 | 4079.615 | 4128.767 |
| t-437 | 9 | 1 | 57.978 | 8.705 | 1067 | 0 | 542 | 542 | 0 | 27.135 | 2850.815 | 3866.623 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1790 | 1790 | 0 | 26.495 | 2605.055 | 3604.479 |
| t-418 | 6 | 1 | 121.075 | 12.228 | 201 | 0 | 761 | 761 | 0 | 26.751 | 2588.671 | 2965.503 |
| t-295 | 12 | 1 | 55.611 | 11.047 | 3396 | 0 | 684 | 684 | 0 | 27.007 | 2555.903 | 3784.703 |
| t-082 | 42 | 1 | 17.911 | 12.643 | 150 | 0 | 864 | 864 | 0 | 25.855 | 2523.135 | 2834.431 |
| t-447 | 17 | 1 | 45.301 | 12.225 | 558 | 0 | 750 | 750 | 0 | 27.647 | 2506.751 | 3620.863 |
| t-299 | 21 | 1 | 42.355 | 14.484 | 646 | 0 | 907 | 907 | 0 | 27.135 | 2490.367 | 2998.271 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 444 | 444 | 0 | 26.879 | 2392.063 | 2834.431 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 4562 | 4562 | 0 | 27.775 | 2375.679 | 4456.447 |
| t-349 | 14 | 1 | 44.443 | 10.033 | 379 | 0 | 603 | 603 | 0 | 26.367 | 2375.679 | 2965.503 |
| t-294 | 7 | 1 | 107.858 | 12.447 | 667 | 0 | 765 | 765 | 0 | 25.343 | 2326.527 | 3047.423 |
| t-381 | 11 | 1 | 47.024 | 8.839 | 134 | 0 | 603 | 603 | 0 | 24.063 | 2293.759 | 2883.583 |
| t-205 | 18 | 1 | 52.795 | 15.701 | 449 | 0 | 967 | 967 | 0 | 27.647 | 2277.375 | 3981.311 |
| t-320 | 14 | 1 | 86.153 | 19.097 | 574 | 0 | 1206 | 1206 | 0 | 26.367 | 2260.991 | 2899.967 |
| t-338 | 6 | 1 | 21.73 | 2.22 | 156 | 0 | 140 | 140 | 0 | 29.055 | 2211.839 | 2572.287 |
| t-017 | 19 | 1 | 29.804 | 9.231 | 830 | 0 | 628 | 628 | 0 | 27.647 | 2195.455 | 2867.199 |
| t-187 | 6 | 1 | 56.225 | 5.593 | 685 | 0 | 354 | 354 | 0 | 24.319 | 2179.071 | 2768.895 |
| t-420 | 13 | 1 | 49.618 | 10.967 | 270 | 0 | 712 | 712 | 0 | 25.215 | 2179.071 | 2932.735 |
| t-281 | 10 | 1 | 34.831 | 6.124 | 2160 | 0 | 395 | 395 | 0 | 26.367 | 2162.687 | 2916.351 |
| t-318 | 7 | 1 | 31.766 | 3.874 | 686 | 0 | 252 | 252 | 0 | 28.927 | 2146.303 | 2506.751 |
| t-369 | 14 | 1 | 132.689 | 32.582 | 1313 | 0 | 1943 | 1943 | 0 | 28.159 | 2080.767 | 2981.887 |
