# E28-332-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 88.932s, closing 1.711s, drain 46.906s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 385963 | 385963 | 6182.827 | 6185.524 | 9014.5 | 0 | 0 | 0 | 67 | 1880 | 9614 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 263619 | 16858.631 | 16711.679 | 22986.751 | 29589.503 | 31719.423 | 33783.807 |
| e2e_from_send | 263619 | 488.611 | 423.167 | 820.735 | 1956.863 | 3315.711 | 5750.783 |
| producer_lag | 263619 | 16370.022 | 16261.119 | 22396.927 | 29114.367 | 31096.831 | 31637.503 |
| publish_call | 385963 | 323.557 | 277.247 | 637.951 | 1169.407 | 1761.279 | 2480.127 |
| queue_wait | 263619 | 409.758 | 353.535 | 731.135 | 1468.415 | 2971.647 | 5730.303 |
| broker_dwell | 263619 | 107.749 | 37.023 | 234.495 | 1173.503 | 2709.503 | 5500.927 |
| receive_call | 344609 | 47.3 | 21.647 | 123.519 | 336.127 | 468.735 | 506.623 |
| ctx_load | 263619 | 17.44 | 12.703 | 32.223 | 134.783 | 306.687 | 436.735 |
| process | 263619 | 10.301 | 0.001 | 0.002 | 0.007 | 2600.959 | 3004.415 |
| ctx_commit | 263619 | 51.086 | 35.647 | 92.479 | 328.191 | 535.039 | 911.871 |
| ack | 205798 | 0.002 | 0.001 | 0.003 | 0.008 | 0.023 | 0.164 |
| service | 263619 | 78.854 | 53.599 | 123.455 | 381.695 | 2684.927 | 3844.095 |
| e2e_large | 43 | 18280.257 | 18235.391 | 23347.199 | 31227.903 | 31227.903 | 31227.903 |
| ready_wait | 263619 | 31.498 | 11.135 | 79.551 | 327.679 | 676.863 | 1393.663 |
| blocked | 263619 | 25.039 | 10.871 | 53.087 | 281.855 | 648.191 | 1139.711 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6185.524 | 8482.1 |
| applied_total | 211490.531 | 363909 |
| bench_cpu_cores | 1.6 | 3.953 |
| bench_cpu_pct | 16.004 | 39.53 |
| bench_gc_gen0 | 0.306 | 2 |
| bench_gc_gen1 | 0.204 | 1 |
| bench_gc_gen2 | 0.184 | 1 |
| bench_gc_heap_mb | 1168.347 | 1953.2 |
| bench_mem_mb | 2135.045 | 2316.5 |
| bench_threadpool_queue | 0.51 | 25 |
| bench_threadpool_threads | 22.694 | 30 |
| broker_backlog | 41712.551 | 85093 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 989.184 | 2723 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 431915.469 | 747174 |
| octopus_missing_reports_total | 20228.245 | 34040 |
| octopus_mqtt_received_total | 112287.306 | 177634 |
| octopus_mqtt_sent_total | 162288.51 | 274483 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.857 | 44 |
| pg_active_loads | 11.837 | 41 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.994 | 100 |
| pg_conn_active | 67.041 | 129 |
| pg_conn_idle | 197.204 | 228 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.612 | 30 |
| pg_conn_total | 264.245 | 266 |
| pg_db_size_mb | 3723.308 | 4215.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24691.855 | 29378 |
| pg_tup_del_per_s | 32931.429 | 40460.7 |
| pg_tup_fetch_per_s | 305087.433 | 361739.9 |
| pg_tup_ins_per_s | 44609.024 | 52323.5 |
| pg_tup_upd_per_s | 52054.314 | 59247.1 |
| pg_wal_mb_per_s | 122.938 | 189.11 |
| produced_per_s | 6182.827 | 8467.7 |
| produced_total | 212479.714 | 364724 |
| publish_mb_per_s | 15.37 | 26.58 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 181,747 records are Completed |
| record_counts | pass | 181,747 records found, 54,258 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 363 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 385,963 events applied by workers, 385,963 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 9,614 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 282 ms, max 1139 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 984 seq 41: ready 111 ms before start, 111 ms of it with an idle slot in group 29
- [cross_source_blocking] source 7381 seq 37: ready 129 ms before start, 129 ms of it with an idle slot in group 253
- [cross_source_blocking] source 9827 seq 12: ready 130 ms before start, 130 ms of it with an idle slot in group 340
- [cross_source_blocking] source 9795 seq 11: ready 130 ms before start, 130 ms of it with an idle slot in group 340
- [cross_source_blocking] source 9859 seq 9: ready 130 ms before start, 130 ms of it with an idle slot in group 340
- [cross_source_blocking] source 15514 seq 15: ready 130 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15546 seq 15: ready 130 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11122 seq 26: ready 130 ms before start, 130 ms of it with an idle slot in group 363
- [cross_source_blocking] source 15482 seq 24: ready 130 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15418 seq 21: ready 130 ms before start, 130 ms of it with an idle slot in group 483
- [cross_source_blocking] source 11748 seq 27: ready 130 ms before start, 130 ms of it with an idle slot in group 380
- [cross_source_blocking] source 9454 seq 14: ready 130 ms before start, 130 ms of it with an idle slot in group 325
- [cross_source_blocking] source 8905 seq 17: ready 130 ms before start, 130 ms of it with an idle slot in group 307
- [cross_source_blocking] source 10573 seq 30: ready 130 ms before start, 130 ms of it with an idle slot in group 348
- [cross_source_blocking] source 1519 seq 31: ready 335 ms before start, 335 ms of it with an idle slot in group 45
- [cross_source_blocking] source 3388 seq 11: ready 424 ms before start, 398 ms of it with an idle slot in group 117
- [cross_source_blocking] source 3960 seq 11: ready 409 ms before start, 409 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3944 seq 17: ready 409 ms before start, 409 ms of it with an idle slot in group 131
- [cross_source_blocking] source 15756 seq 15: ready 222 ms before start, 222 ms of it with an idle slot in group 492
- [cross_source_blocking] source 6418 seq 22: ready 409 ms before start, 409 ms of it with an idle slot in group 220
- [cross_source_blocking] source 6406 seq 17: ready 410 ms before start, 410 ms of it with an idle slot in group 220

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-187 | 6 | 1 | 122.008 | 12.138 | 685 | 0 | 466 | 466 | 0 | 16187.391 | 31326.207 | 31719.423 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 62 | 62 | 0 | 19267.583 | 31326.207 | 31326.207 |
| t-251 | 9 | 1 | 82.187 | 12.347 | 1193 | 0 | 454 | 454 | 0 | 17563.647 | 31064.063 | 32112.639 |
| t-316 | 19 | 1 | 58.258 | 18.542 | 169 | 0 | 681 | 681 | 0 | 16646.143 | 31064.063 | 32636.927 |
| t-395 | 6 | 1 | 76.171 | 8.163 | 212 | 0 | 274 | 274 | 0 | 15532.031 | 31064.063 | 31850.495 |
| t-484 | 6 | 1 | 162.625 | 16.461 | 232 | 0 | 583 | 583 | 0 | 17432.575 | 30801.919 | 31850.495 |
| t-013 | 10 | 1 | 79.654 | 12.086 | 134 | 0 | 428 | 428 | 0 | 17039.359 | 30670.847 | 31850.495 |
| t-060 | 7 | 1 | 103.566 | 12.785 | 1546 | 0 | 476 | 476 | 0 | 16515.071 | 30539.775 | 31326.207 |
| t-092 | 29 | 1 | 73.218 | 35.891 | 519 | 0 | 1376 | 1376 | 0 | 16646.143 | 30277.631 | 31719.423 |
| t-212 | 29 | 2 | 109.791 | 53.116 | 296 | 0 | 1999 | 1999 | 0 | 16646.143 | 30277.631 | 31981.567 |
| t-405 | 6 | 1 | 53.039 | 5.259 | 874 | 0 | 198 | 198 | 0 | 16515.071 | 30277.631 | 30670.847 |
| t-493 | 8 | 1 | 185.785 | 26.56 | 1419 | 0 | 971 | 971 | 0 | 16711.679 | 30277.631 | 30932.991 |
| t-021 | 19 | 1 | 35.278 | 11.734 | 3252 | 0 | 449 | 449 | 0 | 16646.143 | 30146.559 | 31719.423 |
| t-284 | 12 | 1 | 60.548 | 11.512 | 217 | 0 | 442 | 442 | 0 | 17563.647 | 30146.559 | 31719.423 |
| t-339 | 17 | 1 | 27.005 | 7.748 | 134 | 0 | 323 | 323 | 0 | 16056.319 | 30146.559 | 31719.423 |
| t-355 | 10 | 1 | 65.589 | 11.499 | 349 | 0 | 406 | 406 | 0 | 18087.935 | 30146.559 | 31064.063 |
| t-381 | 11 | 1 | 102.042 | 19.181 | 134 | 0 | 710 | 710 | 0 | 16449.535 | 30146.559 | 31850.495 |
| t-173 | 12 | 1 | 91.453 | 19.263 | 3493 | 0 | 782 | 782 | 0 | 17301.503 | 30015.487 | 31850.495 |
| t-180 | 19 | 1 | 102.843 | 30.984 | 152 | 0 | 1121 | 1121 | 0 | 16711.679 | 30015.487 | 31850.495 |
| t-235 | 54 | 4 | 172.241 | 155.048 | 527 | 0 | 5561 | 5561 | 0 | 16777.215 | 30015.487 | 31850.495 |
| t-323 | 6 | 1 | 64.024 | 6.427 | 3540 | 0 | 242 | 242 | 0 | 16252.927 | 30015.487 | 31850.495 |
| t-411 | 15 | 1 | 109.881 | 25.197 | 2644 | 0 | 936 | 936 | 0 | 16580.607 | 30015.487 | 31850.495 |
| t-445 | 15 | 1 | 41.842 | 10.067 | 896 | 0 | 369 | 369 | 0 | 16056.319 | 30015.487 | 30670.847 |
| t-451 | 35 | 1 | 22.558 | 13 | 815 | 0 | 517 | 517 | 0 | 16449.535 | 30015.487 | 31719.423 |
| t-492 | 6 | 1 | 56.484 | 5.146 | 3046 | 0 | 205 | 205 | 0 | 17956.863 | 30015.487 | 30277.631 |
