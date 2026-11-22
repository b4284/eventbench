# W28-mosq-2-s1 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.31.128:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 89.259s, closing 2.146s, drain 66.107s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 416405 | 416405 | 6795.008 | 6770.672 | 10103.7 | 0 | 0 | 0 | 70 | 2007 | 70802 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 293996 | 15965.793 | 16343.039 | 21381.119 | 24461.311 | 26460.159 | 28835.839 |
| e2e_from_send | 293996 | 716.349 | 501.503 | 1441.791 | 3803.135 | 5926.911 | 9650.175 |
| producer_lag | 293996 | 15249.459 | 15589.375 | 20594.687 | 23674.879 | 25772.031 | 26345.471 |
| publish_call | 416405 | 302.205 | 254.463 | 586.751 | 987.135 | 1335.295 | 1857.535 |
| queue_wait | 293996 | 635.399 | 427.263 | 1308.671 | 3688.447 | 5816.319 | 9576.447 |
| broker_dwell | 293996 | 349.765 | 77.183 | 1056.767 | 3469.311 | 5591.039 | 9560.063 |
| receive_call | 375694 | 42.149 | 20.975 | 104.703 | 315.903 | 465.663 | 524.799 |
| ctx_load | 293996 | 16.925 | 11.903 | 33.087 | 131.839 | 165.887 | 702.463 |
| process | 293996 | 10.091 | 0.001 | 0.002 | 0.007 | 2584.575 | 3002.367 |
| ctx_commit | 293996 | 53.911 | 36.799 | 105.215 | 324.863 | 498.687 | 1506.303 |
| ack | 229426 | 0.002 | 0.001 | 0.003 | 0.008 | 0.024 | 30.991 |
| service | 293996 | 80.95 | 54.207 | 135.423 | 380.927 | 2662.399 | 4395.007 |
| e2e_large | 46 | 17416.014 | 17121.279 | 21544.959 | 23953.407 | 23953.407 | 23953.407 |
| ready_wait | 293996 | 107.402 | 33.407 | 323.583 | 840.191 | 1239.039 | 2044.927 |
| blocked | 293996 | 84.11 | 30.095 | 245.503 | 623.615 | 899.071 | 1173.503 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6770.672 | 10103.7 |
| applied_total | 224133.02 | 397441 |
| bench_cpu_cores | 1.617 | 2.169 |
| bench_cpu_pct | 16.175 | 21.69 |
| bench_gc_gen0 | 0.3 | 2 |
| bench_gc_gen1 | 0.18 | 1 |
| bench_gc_gen2 | 0.18 | 1 |
| bench_gc_heap_mb | 1352.684 | 2762.7 |
| bench_mem_mb | 2212.862 | 2778.6 |
| bench_threadpool_queue | 0.78 | 11 |
| bench_threadpool_threads | 17.54 | 19 |
| broker_backlog | 55316.66 | 115695 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 2653.18 | 13031 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 458632.68 | 804773 |
| octopus_missing_reports_total | 20179 | 34602 |
| octopus_mqtt_received_total | 113077.26 | 189065 |
| octopus_mqtt_sent_total | 167172 | 289238 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 20.6 | 47 |
| pg_active_loads | 12.7 | 39 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.986 | 100 |
| pg_conn_active | 54.2 | 92 |
| pg_conn_idle | 209.48 | 248 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 5.26 | 45 |
| pg_conn_total | 263.68 | 264 |
| pg_db_size_mb | 3666.472 | 4245.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23982.208 | 29797.8 |
| pg_tup_del_per_s | 31252.932 | 42444.1 |
| pg_tup_fetch_per_s | 316527.856 | 410633.8 |
| pg_tup_ins_per_s | 43055.912 | 53035.9 |
| pg_tup_upd_per_s | 50897.296 | 60930.9 |
| pg_wal_mb_per_s | 116.691 | 173.39 |
| produced_per_s | 6795.008 | 9973.2 |
| produced_total | 226786.2 | 400304 |
| publish_mb_per_s | 16.573 | 27.46 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 180,618 records are Completed |
| record_counts | pass | 180,618 records found, 57,686 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 348 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 416,405 events applied by workers, 416,405 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 70,802 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 624 ms, max 1173 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7968 seq 14: ready 300 ms before start, 300 ms of it with an idle slot in group 268
- [cross_source_blocking] source 14900 seq 36: ready 109 ms before start, 104 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14940 seq 45: ready 109 ms before start, 104 ms of it with an idle slot in group 468
- [cross_source_blocking] source 11369 seq 33: ready 112 ms before start, 103 ms of it with an idle slot in group 371
- [cross_source_blocking] source 10491 seq 20: ready 109 ms before start, 109 ms of it with an idle slot in group 348
- [cross_source_blocking] source 11364 seq 19: ready 112 ms before start, 103 ms of it with an idle slot in group 371
- [cross_source_blocking] source 11389 seq 27: ready 113 ms before start, 104 ms of it with an idle slot in group 371
- [cross_source_blocking] source 12155 seq 14: ready 116 ms before start, 106 ms of it with an idle slot in group 388
- [cross_source_blocking] source 13593 seq 4: ready 129 ms before start, 129 ms of it with an idle slot in group 429
- [cross_source_blocking] source 3854 seq 15: ready 127 ms before start, 126 ms of it with an idle slot in group 131
- [cross_source_blocking] source 11712 seq 19: ready 121 ms before start, 121 ms of it with an idle slot in group 379
- [cross_source_blocking] source 7619 seq 29: ready 126 ms before start, 105 ms of it with an idle slot in group 261
- [cross_source_blocking] source 12612 seq 5: ready 132 ms before start, 125 ms of it with an idle slot in group 397
- [cross_source_blocking] source 3878 seq 24: ready 126 ms before start, 124 ms of it with an idle slot in group 131
- [cross_source_blocking] source 4976 seq 35: ready 131 ms before start, 103 ms of it with an idle slot in group 171
- [cross_source_blocking] source 15494 seq 19: ready 137 ms before start, 137 ms of it with an idle slot in group 483
- [cross_source_blocking] source 5515 seq 18: ready 117 ms before start, 117 ms of it with an idle slot in group 189
- [cross_source_blocking] source 3938 seq 17: ready 120 ms before start, 119 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3954 seq 21: ready 117 ms before start, 116 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3918 seq 22: ready 120 ms before start, 119 ms of it with an idle slot in group 131

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-100 | 33 | 1 | 88.482 | 47.138 | 2626 | 0 | 1925 | 1925 | 0 | 16908.287 | 27787.263 | 28835.839 |
| t-291 | 7 | 1 | 90.195 | 10.263 | 368 | 0 | 406 | 406 | 0 | 16449.535 | 26214.399 | 26345.471 |
| t-379 | 57 | 2 | 103.137 | 97.597 | 591 | 0 | 3979 | 3979 | 0 | 16777.215 | 26214.399 | 27262.975 |
| t-171 | 14 | 1 | 209.284 | 49.641 | 142 | 0 | 1941 | 1941 | 0 | 16383.999 | 25821.183 | 26607.615 |
| t-315 | 88 | 4 | 136.426 | 199.807 | 2927 | 0 | 7984 | 7984 | 0 | 16711.679 | 25690.111 | 27000.831 |
| t-349 | 14 | 1 | 96.442 | 21.772 | 379 | 0 | 888 | 888 | 0 | 16318.463 | 25690.111 | 26607.615 |
| t-189 | 59 | 2 | 97.115 | 94.014 | 1601 | 0 | 3874 | 3874 | 0 | 16777.215 | 25559.039 | 27131.903 |
| t-012 | 6 | 1 | 134.185 | 13.905 | 556 | 0 | 559 | 559 | 0 | 16515.071 | 25427.967 | 26476.543 |
| t-484 | 6 | 1 | 162.625 | 16.461 | 232 | 0 | 656 | 656 | 0 | 16711.679 | 25427.967 | 25559.039 |
| t-339 | 17 | 1 | 27.005 | 7.748 | 134 | 0 | 343 | 343 | 0 | 15597.567 | 25296.895 | 26607.615 |
| t-357 | 21 | 1 | 116.69 | 40.486 | 2215 | 0 | 1610 | 1610 | 0 | 16580.607 | 25165.823 | 26738.687 |
| t-069 | 31 | 1 | 30.759 | 15.322 | 2002 | 0 | 679 | 679 | 0 | 15597.567 | 25034.751 | 26476.543 |
| t-125 | 20 | 1 | 140.299 | 48.983 | 1900 | 0 | 2041 | 2041 | 0 | 17432.575 | 25034.751 | 27000.831 |
| t-179 | 25 | 1 | 51.101 | 21.881 | 989 | 0 | 908 | 908 | 0 | 16252.927 | 25034.751 | 26738.687 |
| t-181 | 35 | 4 | 322.682 | 186.423 | 1367 | 0 | 7194 | 7194 | 0 | 16777.215 | 25034.751 | 26869.759 |
| t-195 | 9 | 1 | 160.79 | 25.652 | 2299 | 0 | 984 | 984 | 0 | 16580.607 | 25034.751 | 26607.615 |
| t-324 | 15 | 1 | 47.791 | 11.104 | 131 | 0 | 453 | 453 | 0 | 16646.143 | 25034.751 | 26083.327 |
| t-421 | 6 | 1 | 141.184 | 14.431 | 758 | 0 | 540 | 540 | 0 | 16056.319 | 25034.751 | 26607.615 |
| t-155 | 14 | 1 | 31.66 | 7.197 | 2759 | 0 | 294 | 294 | 0 | 15728.639 | 24903.679 | 25296.895 |
| t-212 | 29 | 2 | 109.791 | 53.116 | 296 | 0 | 2152 | 2152 | 0 | 16056.319 | 24903.679 | 26214.399 |
| t-219 | 13 | 1 | 71.086 | 15.025 | 272 | 0 | 576 | 576 | 0 | 16449.535 | 24903.679 | 26476.543 |
| t-317 | 32 | 1 | 85.87 | 46.88 | 1288 | 0 | 1901 | 1901 | 0 | 16318.463 | 24903.679 | 26738.687 |
| t-027 | 62 | 2 | 60.438 | 60.682 | 935 | 0 | 2431 | 2431 | 0 | 15925.247 | 24772.607 | 26869.759 |
| t-028 | 27 | 1 | 44.718 | 19.869 | 454 | 0 | 863 | 863 | 0 | 16121.855 | 24772.607 | 26476.543 |
| t-045 | 47 | 3 | 179.274 | 139.699 | 1398 | 0 | 5554 | 5554 | 0 | 16515.071 | 24772.607 | 27131.903 |
