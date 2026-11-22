# W28-nats-1-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.31.128:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.402s, closing 1.362s, drain 76.504s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 393870 | 393870 | 6293.122 | 6289.061 | 10353.1 | 0 | 0 | 0 | 68 | 1907 | 27916 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 271493 | 15511.869 | 15073.279 | 22986.751 | 27426.815 | 29081.599 | 31784.959 |
| e2e_from_send | 271493 | 556.296 | 467.711 | 956.927 | 2273.279 | 3475.455 | 6369.279 |
| producer_lag | 271493 | 14955.581 | 14409.727 | 22478.847 | 26804.223 | 28557.311 | 29245.439 |
| publish_call | 393870 | 316.933 | 264.703 | 621.055 | 1061.887 | 2203.647 | 2476.031 |
| queue_wait | 271493 | 468.442 | 383.231 | 860.671 | 1934.335 | 3170.303 | 6033.407 |
| broker_dwell | 271493 | 142.671 | 29.375 | 378.367 | 1652.735 | 2926.591 | 5816.319 |
| receive_call | 355215 | 42.596 | 17.727 | 111.871 | 329.727 | 477.695 | 510.719 |
| ctx_load | 271493 | 16.648 | 11.887 | 32.367 | 127.743 | 162.047 | 439.039 |
| process | 271493 | 10.189 | 0.001 | 0.002 | 0.007 | 2582.527 | 2996.223 |
| ctx_commit | 271493 | 60.991 | 41.279 | 126.591 | 344.063 | 573.951 | 1501.183 |
| ack | 223246 | 0.002 | 0.001 | 0.003 | 0.008 | 0.023 | 34.367 |
| service | 271493 | 87.855 | 58.367 | 154.879 | 408.831 | 2676.735 | 4349.951 |
| e2e_large | 44 | 17028.003 | 16179.199 | 24133.631 | 29458.431 | 29458.431 | 29458.431 |
| ready_wait | 271493 | 58.722 | 16.319 | 165.119 | 538.623 | 922.623 | 2562.047 |
| blocked | 271493 | 41.195 | 15.375 | 102.271 | 368.127 | 848.383 | 1733.631 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6289.061 | 10353.1 |
| applied_total | 225460.51 | 375056 |
| bench_cpu_cores | 1.485 | 2.24 |
| bench_cpu_pct | 14.848 | 22.4 |
| bench_gc_gen0 | 0.204 | 2 |
| bench_gc_gen1 | 0.102 | 1 |
| bench_gc_gen2 | 0.102 | 1 |
| bench_gc_heap_mb | 1656.2 | 3064.8 |
| bench_mem_mb | 2694.259 | 3017.7 |
| bench_threadpool_queue | 21 | 506 |
| bench_threadpool_threads | 22.163 | 30 |
| broker_backlog | 54508.245 | 123049 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1507.694 | 4693 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 458776.51 | 750861 |
| octopus_missing_reports_total | 21917.612 | 32388 |
| octopus_mqtt_received_total | 106706.98 | 164721 |
| octopus_mqtt_sent_total | 194078.878 | 312772 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 23.245 | 46 |
| pg_active_loads | 14.98 | 39 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.993 | 100 |
| pg_conn_active | 66.041 | 112 |
| pg_conn_idle | 197.551 | 236 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 10.02 | 29 |
| pg_conn_total | 263.592 | 265 |
| pg_db_size_mb | 3720.282 | 4207.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23282.451 | 32587.3 |
| pg_tup_del_per_s | 31745.465 | 46262.5 |
| pg_tup_fetch_per_s | 313073.945 | 377001.7 |
| pg_tup_ins_per_s | 43688.241 | 56022.6 |
| pg_tup_upd_per_s | 49940.245 | 65189.7 |
| pg_wal_mb_per_s | 121.753 | 201.8 |
| produced_per_s | 6293.122 | 9736 |
| produced_total | 226968.204 | 376130 |
| publish_mb_per_s | 15.656 | 34.83 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 179,883 records are Completed |
| record_counts | pass | 179,883 records found, 55,122 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 354 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 393,870 events applied by workers, 393,870 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 27,916 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 368 ms, max 1733 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9371 seq 13: ready 128 ms before start, 105 ms of it with an idle slot in group 325
- [cross_source_blocking] source 15049 seq 28: ready 122 ms before start, 107 ms of it with an idle slot in group 469
- [cross_source_blocking] source 10586 seq 12: ready 164 ms before start, 138 ms of it with an idle slot in group 348
- [cross_source_blocking] source 10618 seq 26: ready 164 ms before start, 124 ms of it with an idle slot in group 349
- [cross_source_blocking] source 10394 seq 25: ready 124 ms before start, 101 ms of it with an idle slot in group 347
- [cross_source_blocking] source 13435 seq 7: ready 183 ms before start, 145 ms of it with an idle slot in group 429
- [cross_source_blocking] source 13725 seq 15: ready 189 ms before start, 131 ms of it with an idle slot in group 436
- [cross_source_blocking] source 12497 seq 6: ready 182 ms before start, 105 ms of it with an idle slot in group 397
- [cross_source_blocking] source 13563 seq 8: ready 187 ms before start, 148 ms of it with an idle slot in group 429
- [cross_source_blocking] source 2122 seq 11: ready 213 ms before start, 130 ms of it with an idle slot in group 61
- [cross_source_blocking] source 6964 seq 15: ready 274 ms before start, 129 ms of it with an idle slot in group 237
- [cross_source_blocking] source 14605 seq 18: ready 124 ms before start, 124 ms of it with an idle slot in group 461
- [cross_source_blocking] source 12625 seq 4: ready 124 ms before start, 124 ms of it with an idle slot in group 397
- [cross_source_blocking] source 15049 seq 30: ready 107 ms before start, 107 ms of it with an idle slot in group 469
- [cross_source_blocking] source 11729 seq 31: ready 104 ms before start, 104 ms of it with an idle slot in group 380
- [cross_source_blocking] source 263 seq 21: ready 129 ms before start, 129 ms of it with an idle slot in group 5
- [cross_source_blocking] source 8950 seq 10: ready 117 ms before start, 106 ms of it with an idle slot in group 309
- [cross_source_blocking] source 3903 seq 23: ready 512 ms before start, 512 ms of it with an idle slot in group 131
- [cross_source_blocking] source 13003 seq 31: ready 514 ms before start, 464 ms of it with an idle slot in group 413
- [cross_source_blocking] source 8556 seq 16: ready 515 ms before start, 490 ms of it with an idle slot in group 293

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 68 | 68 | 0 | 14942.207 | 29491.199 | 29491.199 |
| t-452 | 6 | 1 | 44.703 | 4.676 | 1797 | 0 | 168 | 168 | 0 | 14155.775 | 28966.911 | 29360.127 |
| t-157 | 28 | 1 | 52.32 | 23.339 | 2458 | 0 | 893 | 893 | 0 | 15269.887 | 28704.767 | 29622.271 |
| t-172 | 20 | 2 | 159.63 | 56.096 | 2589 | 0 | 2078 | 2078 | 0 | 14876.671 | 28704.767 | 29491.199 |
| t-365 | 13 | 1 | 76.152 | 16.433 | 3428 | 0 | 620 | 620 | 0 | 15269.887 | 28704.767 | 29229.055 |
| t-141 | 7 | 1 | 239.018 | 28.012 | 134 | 0 | 1038 | 1038 | 0 | 15138.815 | 28573.695 | 29491.199 |
| t-275 | 9 | 1 | 153.153 | 22.802 | 166 | 0 | 829 | 829 | 0 | 15007.743 | 28442.623 | 29491.199 |
| t-036 | 6 | 1 | 64.114 | 6.442 | 284 | 0 | 235 | 235 | 0 | 14352.383 | 28311.551 | 28704.767 |
| t-028 | 27 | 1 | 44.718 | 19.869 | 454 | 0 | 821 | 821 | 0 | 15400.959 | 28049.407 | 29097.983 |
| t-051 | 31 | 1 | 91.094 | 45.647 | 200 | 0 | 1700 | 1700 | 0 | 15007.743 | 28049.407 | 29360.127 |
| t-117 | 28 | 1 | 51.968 | 24.357 | 294 | 0 | 933 | 933 | 0 | 14811.135 | 28049.407 | 30015.487 |
| t-059 | 8 | 1 | 138.471 | 18.307 | 1449 | 0 | 658 | 658 | 0 | 15073.279 | 27918.335 | 28704.767 |
| t-069 | 31 | 1 | 30.759 | 15.322 | 2002 | 0 | 663 | 663 | 0 | 14745.599 | 27918.335 | 29229.055 |
| t-084 | 23 | 2 | 177.1 | 64.243 | 204 | 0 | 2337 | 2337 | 0 | 14614.527 | 27918.335 | 29360.127 |
| t-091 | 27 | 1 | 82.038 | 35.127 | 952 | 0 | 1368 | 1368 | 0 | 15073.279 | 27918.335 | 29360.127 |
| t-139 | 16 | 1 | 38.195 | 10.791 | 1367 | 0 | 451 | 451 | 0 | 14942.207 | 27918.335 | 29229.055 |
| t-187 | 6 | 1 | 122.008 | 12.138 | 685 | 0 | 460 | 460 | 0 | 14286.847 | 27918.335 | 29229.055 |
| t-259 | 70 | 1 | 39.136 | 44.806 | 160 | 0 | 1761 | 1761 | 0 | 15204.351 | 27918.335 | 29491.199 |
| t-293 | 46 | 2 | 91.642 | 70.327 | 2741 | 0 | 2618 | 2618 | 0 | 14811.135 | 27918.335 | 29229.055 |
| t-396 | 17 | 1 | 89.956 | 25.946 | 210 | 0 | 921 | 921 | 0 | 15466.495 | 27918.335 | 29360.127 |
| t-403 | 41 | 1 | 41.613 | 28.214 | 304 | 0 | 1053 | 1053 | 0 | 14811.135 | 27918.335 | 29229.055 |
| t-045 | 47 | 3 | 179.274 | 139.699 | 1398 | 0 | 5214 | 5214 | 0 | 15007.743 | 27787.263 | 30015.487 |
| t-124 | 11 | 1 | 32.353 | 6.167 | 1576 | 0 | 225 | 225 | 0 | 15073.279 | 27787.263 | 29360.127 |
| t-125 | 20 | 1 | 140.299 | 48.983 | 1900 | 0 | 1938 | 1938 | 0 | 15728.639 | 27787.263 | 28966.911 |
| t-181 | 35 | 4 | 322.682 | 186.423 | 1367 | 0 | 6792 | 6792 | 0 | 15204.351 | 27787.263 | 29360.127 |
