# D4c-pg-primary-kill-traced — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.236s, closing 2.654s, drain 180.047s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 997606 | 997287 | 15968.414 | 12246.761 | 23341.5 | 0 | 11559 | 11559 | 457 | 5154 | 94169 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 793996 | 9922.657 | 6488.063 | 24231.935 | 38273.023 | 121700.351 | 189923.327 |
| e2e_from_send | 793996 | 9556.283 | 6139.903 | 23740.415 | 38043.647 | 121634.815 | 189399.039 |
| producer_lag | 794238 | 366.336 | 68.799 | 1161.215 | 1811.455 | 2134.015 | 2596.863 |
| publish_call | 997606 | 89.93 | 72.127 | 155.647 | 357.887 | 1233.919 | 2058.239 |
| queue_wait | 793996 | 9432.269 | 6008.831 | 23674.879 | 37945.343 | 120913.919 | 189399.039 |
| broker_dwell | 793996 | 9355.533 | 5931.007 | 23592.959 | 37879.807 | 120913.919 | 189399.039 |
| receive_call | 986338 | 15.082 | 0.005 | 37.695 | 285.439 | 470.271 | 536.575 |
| ctx_load | 793996 | 10.963 | 4.259 | 14.447 | 40.607 | 70.527 | 15007.743 |
| process | 793996 | 10.755 | 0.002 | 0.004 | 0.046 | 2619.391 | 3014.655 |
| ctx_commit | 793996 | 102.269 | 94.719 | 181.119 | 340.479 | 731.647 | 15015.935 |
| ack | 792577 | 0.007 | 0.003 | 0.005 | 0.033 | 0.685 | 49.279 |
| service | 793996 | 124.013 | 102.591 | 192.639 | 624.127 | 2852.863 | 17907.711 |
| e2e_large | 355 | 3448.698 | 3155.967 | 5033.983 | 9805.823 | 12296.191 | 12296.191 |
| ready_wait | 793996 | 589.523 | 338.175 | 1473.535 | 3071.999 | 6303.743 | 37584.895 |
| blocked | 793996 | 84.151 | 2.641 | 130.047 | 1624.063 | 3039.231 | 16105.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12246.761 | 17242.8 |
| applied_total | 456469.449 | 738504 |
| bench_cpu_cores | 3.937 | 5.273 |
| bench_cpu_pct | 16.405 | 21.97 |
| bench_gc_gen0 | 2.878 | 4 |
| bench_gc_gen1 | 2.204 | 4 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1084.71 | 1357.5 |
| bench_mem_mb | 1639.127 | 1843 |
| bench_threadpool_queue | 57.837 | 743 |
| bench_threadpool_threads | 43.224 | 44 |
| broker_backlog | 81061.429 | 214606 |
| ctr_eb_mosquitto_cpu_pct | 53.552 | 69.36 |
| ctr_eb_mosquitto_mem_mb | 10.131 | 20.8 |
| ctr_eb_pg_standby_cpu_pct | 527.483 | 962.58 |
| ctr_eb_pg_standby_mem_mb | 3782.235 | 5652.4 |
| ctr_eb_postgres_cpu_pct | 985.027 | 1040.67 |
| ctr_eb_postgres_mem_mb | 4336.836 | 4646.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 235.933 | 8146.8 |
| host_cpu_pct | 69.038 | 79.39 |
| in_flight | 115466 | 219592 |
| nacks_per_s | 235.933 | 8146.8 |
| octopus_dispatcher_cores | 1.5 | 1.59 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 726158.388 | 1716816 |
| octopus_mqtt_received_total | 114295.959 | 255069 |
| octopus_mqtt_sent_total | 184659.245 | 434119 |
| octopus_publish_retries_total | 4049.633 | 7632 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 11.655 | 18 |
| pg_active_loads | 4.138 | 21 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 58.448 | 123 |
| pg_conn_idle | 206.448 | 268 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.138 | 2 |
| pg_conn_total | 264.897 | 295 |
| pg_db_size_mb | 4470.234 | 5693.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 0 | 0 |
| pg_replica_replay_lag_ms | 1.7 | 1.7 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 29.203 | 50.43 |
| pg_tps | 25392.031 | 36290.8 |
| pg_tup_del_per_s | 32333.854 | 45452 |
| pg_tup_fetch_per_s | 418138.431 | 534466.8 |
| pg_tup_ins_per_s | 48884.042 | 68755.8 |
| pg_tup_upd_per_s | 29788.6 | 38338.3 |
| pg_wal_mb_per_s | 177.841 | 229.56 |
| produced_per_s | 15968.414 | 23304.6 |
| produced_total | 571935.449 | 958096 |
| publish_mb_per_s | 51.99 | 84.25 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15964/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 71 records not Completed |
| record_counts | **FAIL** | 138,534 records found, 138,562 expected; 36 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 457 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 997,287 events applied by workers, 997,606 generated |
| broker_drained | **FAIL** | 319 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 94,169 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1624 ms, max 16105 ms) |

Sample mismatches:

- source 15872: record 68169720922136 left in status Open (version 19)
- source 2242: record 9629316677651 left in status Open (version 9)
- source 2225: record 9556302233619 left in status Open (version 7)
- source 15864: record 68135361183765 left in status Open (version 9)
- source 2225: record 9556302233620 left in status Open (version 9)
- source 2288: record 9826885173278 left in status Open (version 4)
- source 10298: record 44229573214231 left in status Open (version 6)
- source 2260: record 9706626088983 left in status Open (version 8)
- source 2288: record 9826885173279 left in status Open (version 6)
- source 2222: record 9543417331735 left in status Open (version 10)
- source 15864: record 68135361183766 left in status Open (version 4)
- source 15862: record 68126771249173 left in status Open (version 5)
- source 2253: record 9676561317910 left in status Open (version 13)
- source 15862: record 68126771249174 left in status Open (version 3)
- source 2248: record 9655086481433 left in status Open (version 5)
- source 2236: record 9603546873882 left in status Open (version 4)
- source 10298: record 44229573214232 left in status Open (version 1)
- source 2236: record 9603546873883 left in status Open (version 6)
- source 2279: record 9788230467607 left in status Open (version 7)
- source 2265: record 9728100925461 left in status Open (version 7)

## Cross-source blocking samples

- [cross_source_blocking] source 8763 seq 17: ready 129 ms before start, 129 ms of it with an idle slot in group 305
- [cross_source_blocking] source 7277 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 249
- [cross_source_blocking] source 2773 seq 7: ready 113 ms before start, 113 ms of it with an idle slot in group 85
- [cross_source_blocking] source 1813 seq 9: ready 212 ms before start, 212 ms of it with an idle slot in group 52
- [cross_source_blocking] source 15605 seq 10: ready 186 ms before start, 186 ms of it with an idle slot in group 484
- [cross_source_blocking] source 1865 seq 12: ready 212 ms before start, 212 ms of it with an idle slot in group 52
- [cross_source_blocking] source 573 seq 9: ready 132 ms before start, 132 ms of it with an idle slot in group 16
- [cross_source_blocking] source 2973 seq 14: ready 332 ms before start, 309 ms of it with an idle slot in group 97
- [cross_source_blocking] source 5533 seq 17: ready 323 ms before start, 157 ms of it with an idle slot in group 189
- [cross_source_blocking] source 5085 seq 10: ready 292 ms before start, 292 ms of it with an idle slot in group 178
- [cross_source_blocking] source 11673 seq 10: ready 300 ms before start, 217 ms of it with an idle slot in group 379
- [cross_source_blocking] source 6057 seq 9: ready 212 ms before start, 201 ms of it with an idle slot in group 207
- [cross_source_blocking] source 7397 seq 7: ready 255 ms before start, 255 ms of it with an idle slot in group 255
- [cross_source_blocking] source 1613 seq 7: ready 322 ms before start, 322 ms of it with an idle slot in group 48
- [cross_source_blocking] source 15357 seq 7: ready 306 ms before start, 306 ms of it with an idle slot in group 480
- [cross_source_blocking] source 13227 seq 6: ready 151 ms before start, 107 ms of it with an idle slot in group 423
- [cross_source_blocking] source 11708 seq 12: ready 125 ms before start, 125 ms of it with an idle slot in group 379
- [cross_source_blocking] source 2201 seq 14: ready 102 ms before start, 102 ms of it with an idle slot in group 64
- [cross_source_blocking] source 1861 seq 11: ready 260 ms before start, 260 ms of it with an idle slot in group 52
- [cross_source_blocking] source 14036 seq 4: ready 429 ms before start, 116 ms of it with an idle slot in group 446

## Error samples

- t-119L#2 src=3426 seq=2 op=Create rec=14714557956098 ev=0: NpgsqlException: Exception while writing to stream
- t-119L#3 src=3472 seq=2 op=Update rec=14912126451713 ev=1: NpgsqlException: No suitable host was found.
- t-093L#0 src=2916 seq=3 op=Create rec=12524124635138 ev=0: NpgsqlException: No suitable host was found.
- t-119L#3 src=3435 seq=4 op=Update rec=14753212661761 ev=3: PostgresException: 57P01: terminating connection due to unexpected postmaster exit
- t-119L#3 src=3479 seq=4 op=Update rec=14942191222785 ev=3: NpgsqlException: No suitable host was found.
- t-063#0 src=2195 seq=10 op=Complete rec=9427453214721 ev=2: NpgsqlException: No suitable host was found.
- t-306#1 src=8771 seq=40 op=Update rec=37671158153224 ev=5: NpgsqlException: No suitable host was found.
- t-460#0 src=14461 seq=16 op=Update rec=62109522067459 ev=4: NpgsqlException: No suitable host was found.
- t-306#1 src=8787 seq=37 op=Create rec=37739877629959 ev=0: NpgsqlException: No suitable host was found.
- t-306#1 src=8816 seq=36 op=Update rec=37864431681542 ev=3: NpgsqlException: No suitable host was found.
- t-460#0 src=14428 seq=18 op=Create rec=61967788146694 ev=0: NpgsqlException: No suitable host was found.
- t-438#3 src=13820 seq=34 op=Update rec=59356448030723 ev=8: NpgsqlException: No suitable host was found.
- t-306#1 src=8810 seq=38 op=Get rec=37838661877762 ev=12: NpgsqlException: No suitable host was found.
- t-306#1 src=8880 seq=40 op=Create rec=38139309588486 ev=0: NpgsqlException: No suitable host was found.
- t-306#1 src=8865 seq=42 op=Complete rec=38074885079042 ev=7: NpgsqlException: No suitable host was found.
- t-306#1 src=8855 seq=40 op=Update rec=38031935406088 ev=3: NpgsqlException: No suitable host was found.
- t-303#0 src=8739 seq=31 op=Update rec=37533719199749 ev=2: NpgsqlException: No suitable host was found.
- t-460#0 src=14443 seq=7 op=Get rec=62032212656129 ev=3: NpgsqlException: No suitable host was found.
- t-306#1 src=8854 seq=36 op=Complete rec=38027640438791 ev=1: NpgsqlException: No suitable host was found.
- t-301#0 src=8685 seq=37 op=Update rec=37301790965766 ev=6: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16688 | 16461 | 0 | 23986.175 | 170917.887 | 187695.103 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15088 | 15003 | 0 | 25690.111 | 154140.671 | 190840.831 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20539 | 20532 | 0 | 22020.095 | 68157.439 | 185597.951 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5905 | 5905 | 0 | 18087.935 | 51904.511 | 55836.671 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19710 | 19710 | 0 | 20578.303 | 50331.647 | 116916.223 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5810 | 5810 | 0 | 14942.207 | 50069.503 | 54001.663 |
| t-342 | 15 | 1 | 173.079 | 43.282 | 146 | 0 | 2633 | 2633 | 0 | 15007.743 | 47448.063 | 50855.935 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2903 | 2903 | 0 | 15466.495 | 46923.775 | 51642.367 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2844 | 2844 | 0 | 18743.295 | 46661.631 | 49283.071 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2929 | 2929 | 0 | 17039.359 | 46399.487 | 49807.359 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2618 | 2618 | 0 | 14286.847 | 45088.767 | 48758.783 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3181 | 3181 | 0 | 19922.943 | 44302.335 | 51904.511 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11815 | 11815 | 0 | 19005.439 | 44302.335 | 49020.927 |
| t-084 | 23 | 1 | 101.2 | 36.71 | 204 | 0 | 2289 | 2289 | 0 | 9437.183 | 42205.183 | 44826.623 |
| t-170 | 66 | 1 | 33.904 | 37.296 | 3243 | 0 | 2363 | 2363 | 0 | 8585.215 | 41680.895 | 47710.207 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2720 | 2720 | 0 | 16318.463 | 41418.751 | 52953.087 |
| t-001 | 118 | 2 | 36.72 | 71.345 | 154 | 0 | 4615 | 4615 | 0 | 7340.031 | 41156.607 | 50331.647 |
| t-347 | 162 | 5 | 77.021 | 209.274 | 833 | 0 | 13179 | 13179 | 0 | 13762.559 | 41156.607 | 52166.655 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5520 | 5520 | 0 | 21102.591 | 40894.463 | 43253.759 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5960 | 5960 | 0 | 17563.647 | 40894.463 | 47972.351 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9188 | 9188 | 0 | 20316.159 | 40108.031 | 45613.055 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2668 | 2668 | 0 | 14221.311 | 39845.887 | 52953.087 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9071 | 9071 | 0 | 18350.079 | 39845.887 | 47972.351 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6080 | 6080 | 0 | 16056.319 | 39845.887 | 53477.375 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11090 | 11090 | 0 | 15859.711 | 39321.599 | 48234.495 |
