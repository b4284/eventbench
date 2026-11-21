# W28-332-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.86s, closing 0.999s, drain 60.642s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 310746 | 310746 | 5023.798 | 5034.606 | 7544.4 | 0 | 0 | 0 | 287 | 1761 | 40521 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 237701 | 9863.558 | 9797.631 | 14082.047 | 18595.839 | 20447.231 | 22675.455 |
| e2e_from_send | 237701 | 662.613 | 539.647 | 1195.007 | 2705.407 | 4114.431 | 8232.959 |
| producer_lag | 237701 | 9200.942 | 9166.847 | 13336.575 | 17727.487 | 19890.175 | 20299.775 |
| publish_call | 310746 | 390.187 | 289.535 | 822.271 | 1643.519 | 2269.183 | 2531.327 |
| queue_wait | 237701 | 597.179 | 478.975 | 1120.255 | 2404.351 | 3866.623 | 6340.607 |
| broker_dwell | 237701 | 214.513 | 94.783 | 500.991 | 2051.071 | 3510.271 | 6062.079 |
| receive_call | 273592 | 36.245 | 14.111 | 94.911 | 318.975 | 466.687 | 523.007 |
| ctx_load | 237701 | 16.041 | 10.511 | 30.655 | 131.583 | 162.687 | 450.815 |
| process | 237701 | 11.668 | 0.001 | 0.003 | 0.014 | 2643.967 | 3205.119 |
| ctx_commit | 237701 | 37.695 | 23.679 | 68.991 | 302.079 | 530.943 | 1063.935 |
| ack | 220005 | 0.003 | 0.001 | 0.004 | 0.014 | 0.077 | 46.719 |
| service | 237701 | 65.435 | 38.943 | 97.855 | 357.887 | 2750.463 | 3586.047 |
| e2e_large | 207 | 12327.303 | 12533.759 | 16842.751 | 20529.151 | 21757.951 | 21757.951 |
| ready_wait | 237701 | 65.221 | 34.751 | 164.607 | 364.287 | 709.631 | 2020.351 |
| blocked | 237701 | 55.328 | 31.647 | 135.551 | 291.839 | 624.127 | 1924.095 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5034.606 | 7544.4 |
| applied_total | 168014.347 | 297439 |
| bench_cpu_cores | 1.906 | 2.814 |
| bench_cpu_pct | 23.824 | 35.17 |
| bench_gc_gen0 | 0.531 | 2 |
| bench_gc_gen1 | 0.429 | 1 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 985.363 | 1488 |
| bench_mem_mb | 1723.337 | 1826.8 |
| bench_threadpool_queue | 21.51 | 296 |
| bench_threadpool_threads | 13.306 | 14 |
| broker_backlog | 43840 | 81228 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1305.429 | 3718 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 341975.571 | 604567 |
| octopus_missing_reports_total | 13861.98 | 22942 |
| octopus_mqtt_received_total | 101609.673 | 159132 |
| octopus_mqtt_sent_total | 126504.041 | 217573 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.102 | 35 |
| pg_active_loads | 7.49 | 20 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.982 | 100 |
| pg_conn_active | 52.653 | 83 |
| pg_conn_idle | 210.898 | 242 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 7.061 | 32 |
| pg_conn_total | 263.551 | 264 |
| pg_db_size_mb | 3689.782 | 4170.1 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25640.843 | 30968.3 |
| pg_tup_del_per_s | 32916.182 | 41419.7 |
| pg_tup_fetch_per_s | 326823.816 | 381071.7 |
| pg_tup_ins_per_s | 44660.916 | 55566.2 |
| pg_tup_upd_per_s | 54985.196 | 66770.4 |
| pg_wal_mb_per_s | 131.337 | 276.31 |
| produced_per_s | 5023.798 | 8841 |
| produced_total | 169319.776 | 297874 |
| publish_mb_per_s | 25.182 | 43.74 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 3506/3506 source cursors at the expected last sequence |
| records_complete | pass | all 187,992 records are Completed |
| record_counts | pass | 187,992 records found, 40,766 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 360 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 310,746 events applied by workers, 310,746 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 40,521 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 292 ms, max 1923 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6020 seq 21: ready 108 ms before start, 108 ms of it with an idle slot in group 206
- [cross_source_blocking] source 5956 seq 23: ready 168 ms before start, 168 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7740 seq 23: ready 409 ms before start, 409 ms of it with an idle slot in group 263
- [cross_source_blocking] source 5308 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 182
- [cross_source_blocking] source 5988 seq 21: ready 112 ms before start, 112 ms of it with an idle slot in group 206
- [cross_source_blocking] source 6807 seq 25: ready 119 ms before start, 119 ms of it with an idle slot in group 231
- [cross_source_blocking] source 11976 seq 33: ready 120 ms before start, 114 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11848 seq 31: ready 120 ms before start, 114 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11912 seq 25: ready 120 ms before start, 114 ms of it with an idle slot in group 383
- [cross_source_blocking] source 6775 seq 29: ready 121 ms before start, 121 ms of it with an idle slot in group 231
- [cross_source_blocking] source 5956 seq 24: ready 120 ms before start, 113 ms of it with an idle slot in group 206
- [cross_source_blocking] source 7740 seq 25: ready 138 ms before start, 138 ms of it with an idle slot in group 263
- [cross_source_blocking] source 8466 seq 13: ready 127 ms before start, 127 ms of it with an idle slot in group 287
- [cross_source_blocking] source 3047 seq 19: ready 196 ms before start, 106 ms of it with an idle slot in group 102
- [cross_source_blocking] source 12880 seq 24: ready 209 ms before start, 172 ms of it with an idle slot in group 407
- [cross_source_blocking] source 2398 seq 21: ready 462 ms before start, 317 ms of it with an idle slot in group 70
- [cross_source_blocking] source 1533 seq 30: ready 217 ms before start, 217 ms of it with an idle slot in group 46
- [cross_source_blocking] source 11877 seq 18: ready 277 ms before start, 277 ms of it with an idle slot in group 383
- [cross_source_blocking] source 8740 seq 14: ready 410 ms before start, 349 ms of it with an idle slot in group 303
- [cross_source_blocking] source 13832 seq 35: ready 131 ms before start, 121 ms of it with an idle slot in group 438

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-079 | 6 | 1 | 209.753 | 21.568 | 1522 | 0 | 984 | 984 | 0 | 10944.511 | 20316.159 | 21626.879 |
| t-295 | 12 | 1 | 120.676 | 23.972 | 3396 | 0 | 1182 | 1182 | 0 | 9830.399 | 20316.159 | 22020.095 |
| t-462 | 10 | 1 | 55.668 | 9.033 | 2006 | 0 | 405 | 405 | 0 | 9961.471 | 20316.159 | 20447.231 |
| t-190 | 6 | 1 | 150.374 | 15.699 | 241 | 0 | 724 | 724 | 0 | 12517.375 | 20054.015 | 20578.303 |
| t-287 | 17 | 1 | 155.915 | 45.751 | 158 | 0 | 2173 | 2173 | 0 | 9764.863 | 19922.943 | 20578.303 |
| t-470 | 16 | 1 | 103.193 | 27.186 | 140 | 0 | 1377 | 1377 | 0 | 10616.831 | 19922.943 | 20709.375 |
| t-063 | 8 | 1 | 42.658 | 5.912 | 1296 | 0 | 291 | 291 | 0 | 9961.471 | 19791.871 | 20971.519 |
| t-118 | 15 | 1 | 52.306 | 13.232 | 138 | 0 | 647 | 647 | 0 | 9240.575 | 19791.871 | 20709.375 |
| t-223 | 13 | 1 | 233.448 | 49.378 | 2372 | 0 | 2379 | 2379 | 0 | 10158.079 | 19791.871 | 20840.447 |
| t-031 | 11 | 1 | 150.409 | 27.387 | 463 | 0 | 1325 | 1325 | 0 | 10092.543 | 19529.727 | 20709.375 |
| t-046 | 24 | 2 | 183.851 | 74.131 | 3235 | 0 | 3496 | 3496 | 0 | 10092.543 | 19529.727 | 20709.375 |
| t-239 | 39 | 3 | 228.201 | 144.456 | 225 | 0 | 7130 | 7130 | 0 | 10092.543 | 19529.727 | 21233.663 |
| t-334 | 19 | 1 | 135.323 | 43.306 | 391 | 0 | 2085 | 2085 | 0 | 9830.399 | 19529.727 | 20709.375 |
| t-486 | 17 | 1 | 116.681 | 31.091 | 1259 | 0 | 1537 | 1537 | 0 | 9306.111 | 19529.727 | 20840.447 |
| t-095 | 6 | 1 | 118.583 | 11.97 | 925 | 0 | 575 | 575 | 0 | 9764.863 | 19398.655 | 20578.303 |
| t-150 | 15 | 1 | 67.298 | 17.194 | 990 | 0 | 827 | 827 | 0 | 9175.039 | 19398.655 | 20316.159 |
| t-159 | 6 | 1 | 101.348 | 9.698 | 295 | 0 | 440 | 440 | 0 | 10813.439 | 19398.655 | 20185.087 |
| t-279 | 18 | 1 | 77.025 | 22.74 | 196 | 0 | 1098 | 1098 | 0 | 9830.399 | 19398.655 | 20840.447 |
| t-302 | 12 | 1 | 200.753 | 42.936 | 1189 | 0 | 2113 | 2113 | 0 | 8912.895 | 19398.655 | 20709.375 |
| t-086 | 6 | 1 | 94.295 | 10.369 | 464 | 0 | 548 | 548 | 0 | 8028.159 | 19267.583 | 20578.303 |
| t-103 | 50 | 2 | 105.66 | 87.689 | 1025 | 0 | 4316 | 4316 | 0 | 9830.399 | 19267.583 | 20709.375 |
| t-142 | 15 | 1 | 58.416 | 14.227 | 413 | 0 | 703 | 703 | 0 | 9633.791 | 19267.583 | 20447.231 |
| t-495 | 16 | 1 | 81.078 | 20.795 | 553 | 0 | 992 | 992 | 0 | 9830.399 | 19267.583 | 19660.799 |
| t-014 | 34 | 1 | 68.378 | 37.151 | 214 | 0 | 1829 | 1829 | 0 | 9633.791 | 19136.511 | 20578.303 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 340 | 340 | 0 | 12124.159 | 19136.511 | 21757.951 |
