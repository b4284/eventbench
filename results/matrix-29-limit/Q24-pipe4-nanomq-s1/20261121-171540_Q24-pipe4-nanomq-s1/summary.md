# Q24-pipe4-nanomq-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 90.611s, closing 3.695s, drain 32.102s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 161537 | 161537 | 2363.341 | 1125.851 | 3423.1 | 0 | 0 | 0 | 44 | 786 | 67452 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 74999 | 58444.463 | 60850.175 | 76021.759 | 80084.991 | 82116.607 | 84148.223 |
| e2e_from_send | 74999 | 30132.476 | 31916.031 | 50855.935 | 59768.831 | 63635.455 | 67960.831 |
| producer_lag | 74999 | 28311.861 | 28262.399 | 37355.519 | 40173.567 | 41353.215 | 42369.023 |
| publish_call | 161537 | 790.273 | 641.023 | 1446.911 | 1562.623 | 1618.943 | 1708.031 |
| queue_wait | 74999 | 30119.328 | 31899.647 | 50855.935 | 59736.063 | 63635.455 | 67960.831 |
| broker_dwell | 74999 | 30129.094 | 31916.031 | 50855.935 | 59768.831 | 63569.919 | 67960.831 |
| receive_call | 157087 | 95.543 | 47.231 | 276.991 | 461.055 | 495.871 | 500.735 |
| ctx_load | 74999 | 1.127 | 0.91 | 1.397 | 4.503 | 8.423 | 130.367 |
| process | 74999 | 10.772 | 0.001 | 0.001 | 0.005 | 2611.199 | 3000.319 |
| ctx_commit | 74999 | 1.252 | 1.026 | 1.537 | 4.671 | 8.271 | 301.567 |
| ack | 50959 | 0.001 | 0.001 | 0.002 | 0.008 | 0.017 | 0.053 |
| service | 74999 | 13.159 | 2.031 | 2.985 | 10.959 | 2613.247 | 3241.983 |
| e2e_large | 17 | 30422.679 | 29081.599 | 35061.759 | 41517.055 | 41517.055 | 41517.055 |
| ready_wait | 74999 | 1245.873 | 647.167 | 2934.783 | 8347.647 | 15245.311 | 19136.511 |
| blocked | 74999 | 1245.797 | 647.167 | 2934.783 | 8347.647 | 15245.311 | 19136.511 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 1125.851 | 1389.2 |
| applied_total | 44380.686 | 71978 |
| bench_cpu_cores | 0.942 | 1.148 |
| bench_cpu_pct | 9.418 | 11.48 |
| bench_gc_gen0 | 0.157 | 1 |
| bench_gc_gen1 | 0.118 | 1 |
| bench_gc_gen2 | 0.098 | 1 |
| bench_gc_heap_mb | 773.331 | 1498.9 |
| bench_mem_mb | 1540.894 | 1823 |
| bench_threadpool_queue | 0.314 | 10 |
| bench_threadpool_threads | 16.02 | 22 |
| broker_backlog | 131761.176 | 211850 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 48614.039 | 78630 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 151527.373 | 244335 |
| octopus_missing_reports_total | 904.98 | 1269 |
| octopus_mqtt_received_total | 150520.882 | 249473 |
| octopus_mqtt_sent_total | 77613.137 | 126613 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.804 | 4 |
| pg_active_loads | 0.294 | 2 |
| pg_active_polls | 0.157 | 1 |
| pg_active_sends | 0.157 | 1 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 2.529 | 6 |
| pg_conn_idle | 256.51 | 259 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 259.039 | 260 |
| pg_db_size_mb | 2774.206 | 3113.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 8838.302 | 11432.1 |
| pg_tup_del_per_s | 8942.422 | 13093.1 |
| pg_tup_fetch_per_s | 179432.596 | 389822.6 |
| pg_tup_ins_per_s | 16504.21 | 21587.5 |
| pg_tup_upd_per_s | 10868.308 | 13065.4 |
| pg_wal_mb_per_s | 38.275 | 55.12 |
| produced_per_s | 2363.341 | 2930.5 |
| produced_total | 92994.725 | 150608 |
| publish_mb_per_s | 5.441 | 14.62 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5359/5359 source cursors at the expected last sequence |
| records_complete | pass | all 78,057 records are Completed |
| record_counts | pass | 78,057 records found, 26,204 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 174 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 161,537 events applied by workers, 161,537 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 67,452 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 8348 ms, max 19131 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14254 seq 12: ready 187 ms before start, 187 ms of it with an idle slot in group 457
- [cross_source_blocking] source 2550 seq 5: ready 422 ms before start, 422 ms of it with an idle slot in group 76
- [cross_source_blocking] source 2776 seq 7: ready 576 ms before start, 576 ms of it with an idle slot in group 85
- [cross_source_blocking] source 14864 seq 12: ready 399 ms before start, 399 ms of it with an idle slot in group 466
- [cross_source_blocking] source 1086 seq 10: ready 919 ms before start, 919 ms of it with an idle slot in group 34
- [cross_source_blocking] source 5586 seq 5: ready 1079 ms before start, 1079 ms of it with an idle slot in group 193
- [cross_source_blocking] source 5551 seq 25: ready 600 ms before start, 600 ms of it with an idle slot in group 190
- [cross_source_blocking] source 1366 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 43
- [cross_source_blocking] source 3354 seq 11: ready 431 ms before start, 431 ms of it with an idle slot in group 115
- [cross_source_blocking] source 14250 seq 7: ready 361 ms before start, 361 ms of it with an idle slot in group 457
- [cross_source_blocking] source 411 seq 11: ready 606 ms before start, 606 ms of it with an idle slot in group 13
- [cross_source_blocking] source 2204 seq 17: ready 621 ms before start, 621 ms of it with an idle slot in group 64
- [cross_source_blocking] source 4239 seq 8: ready 453 ms before start, 453 ms of it with an idle slot in group 142
- [cross_source_blocking] source 3323 seq 18: ready 797 ms before start, 797 ms of it with an idle slot in group 112
- [cross_source_blocking] source 8578 seq 19: ready 883 ms before start, 883 ms of it with an idle slot in group 295
- [cross_source_blocking] source 3822 seq 6: ready 170 ms before start, 170 ms of it with an idle slot in group 130
- [cross_source_blocking] source 2557 seq 4: ready 1082 ms before start, 1082 ms of it with an idle slot in group 76
- [cross_source_blocking] source 13996 seq 4: ready 940 ms before start, 940 ms of it with an idle slot in group 445
- [cross_source_blocking] source 7408 seq 13: ready 1329 ms before start, 1329 ms of it with an idle slot in group 256
- [cross_source_blocking] source 7103 seq 3: ready 296 ms before start, 296 ms of it with an idle slot in group 241

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-037 | 21 | 1 | 137.486 | 48.25 | 272 | 0 | 1036 | 1036 | 0 | 77070.335 | 82837.503 | 82837.503 |
| t-109 | 33 | 1 | 88.394 | 49.274 | 1467 | 0 | 1164 | 1164 | 0 | 77594.623 | 82313.215 | 82837.503 |
| t-136 | 146 | 4 | 75.321 | 181.554 | 450 | 0 | 4055 | 4055 | 0 | 75497.471 | 82313.215 | 83361.791 |
| t-313 | 35 | 1 | 65.222 | 39.055 | 510 | 0 | 922 | 922 | 0 | 74973.183 | 82313.215 | 82313.215 |
| t-406 | 36 | 1 | 76.647 | 43.103 | 183 | 0 | 969 | 969 | 0 | 74973.183 | 82313.215 | 82313.215 |
| t-439 | 22 | 1 | 126.886 | 47.314 | 3339 | 0 | 1021 | 1021 | 0 | 75497.471 | 82313.215 | 83361.791 |
| t-469 | 105 | 5 | 142.583 | 248.092 | 278 | 0 | 5225 | 5225 | 0 | 76546.047 | 82313.215 | 83361.791 |
| t-382 | 59 | 1 | 43.802 | 43.536 | 2368 | 0 | 1015 | 1015 | 0 | 76021.759 | 81788.927 | 84410.367 |
| t-055 | 65 | 4 | 188.704 | 195.304 | 359 | 0 | 4084 | 4084 | 0 | 76546.047 | 81264.639 | 83361.791 |
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 2757 | 2757 | 0 | 76021.759 | 81264.639 | 81788.927 |
| t-208 | 21 | 1 | 109.072 | 39.748 | 915 | 0 | 931 | 931 | 0 | 75497.471 | 81264.639 | 81788.927 |
| t-379 | 57 | 2 | 88.403 | 83.654 | 591 | 0 | 1926 | 1926 | 0 | 74973.183 | 81264.639 | 84410.367 |
| t-388 | 78 | 2 | 61.224 | 77.71 | 1205 | 0 | 1837 | 1837 | 0 | 74448.895 | 81264.639 | 82313.215 |
| t-178 | 90 | 2 | 50.902 | 77.145 | 131 | 0 | 1785 | 1785 | 0 | 73924.607 | 80740.351 | 81788.927 |
| t-235 | 54 | 3 | 147.636 | 132.899 | 527 | 0 | 2874 | 2874 | 0 | 75497.471 | 80740.351 | 82837.503 |
| t-301 | 26 | 1 | 82.855 | 37.185 | 3339 | 0 | 840 | 840 | 0 | 72876.031 | 80740.351 | 81264.639 |
| t-223 | 13 | 1 | 200.098 | 42.324 | 2372 | 0 | 927 | 927 | 0 | 74973.183 | 79691.775 | 80216.063 |
| t-265 | 75 | 2 | 56.148 | 70.008 | 250 | 0 | 1624 | 1624 | 0 | 72351.743 | 79691.775 | 81264.639 |
| t-181 | 35 | 4 | 276.585 | 159.791 | 1367 | 0 | 3387 | 3387 | 0 | 73924.607 | 79167.487 | 79691.775 |
| t-481 | 12 | 1 | 225.826 | 42.578 | 1993 | 0 | 870 | 870 | 0 | 74448.895 | 79167.487 | 79691.775 |
| t-343 | 63 | 2 | 72.028 | 72.887 | 518 | 0 | 1618 | 1618 | 0 | 72876.031 | 78643.199 | 80216.063 |
| t-436 | 58 | 2 | 66.017 | 64.119 | 1128 | 0 | 1496 | 1496 | 0 | 71827.455 | 78643.199 | 79691.775 |
| t-019 | 38 | 3 | 164.882 | 104.157 | 208 | 0 | 2230 | 2230 | 0 | 72351.743 | 78118.911 | 78643.199 |
| t-049 | 53 | 2 | 68.793 | 61.425 | 350 | 0 | 1362 | 1362 | 0 | 70254.591 | 78118.911 | 79167.487 |
| t-214 | 52 | 2 | 70.276 | 62.98 | 688 | 0 | 1405 | 1405 | 0 | 70778.879 | 77594.623 | 78118.911 |
