# W6-sync-tuned — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.296s, closing 4.7s, drain 23.851s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 732732 | 732732 | 11248.467 | 9366.182 | 17161.3 | 0 | 0 | 0 | 393 | 3799 | 390357 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 561543 | 13106.692 | 9281.535 | 30031.871 | 40206.335 | 45875.199 | 55738.367 |
| e2e_from_send | 561543 | 10096.331 | 6365.183 | 26312.703 | 37945.343 | 43941.887 | 55607.295 |
| producer_lag | 561543 | 3010.364 | 2478.079 | 6295.551 | 9568.255 | 10788.863 | 11485.183 |
| publish_call | 732732 | 148.332 | 108.351 | 269.823 | 1100.799 | 1338.367 | 1678.335 |
| queue_wait | 561543 | 9978.637 | 6250.495 | 26214.399 | 37879.807 | 43843.583 | 55607.295 |
| broker_dwell | 561543 | 9838.789 | 6119.423 | 26034.175 | 37748.735 | 43712.511 | 55541.759 |
| receive_call | 634285 | 33.44 | 9.495 | 92.223 | 361.471 | 491.519 | 565.247 |
| ctx_load | 561543 | 14.266 | 9.023 | 32.639 | 72.767 | 142.079 | 516.863 |
| process | 561543 | 10.886 | 0.002 | 0.005 | 0.057 | 2627.583 | 3026.943 |
| ctx_commit | 561543 | 92.442 | 62.719 | 138.879 | 1231.871 | 1832.959 | 1867.775 |
| ack | 561515 | 0.011 | 0.004 | 0.007 | 0.043 | 0.97 | 59.839 |
| service | 561543 | 117.7 | 77.439 | 167.167 | 1315.839 | 2873.343 | 4665.343 |
| e2e_large | 293 | 5968.353 | 5443.583 | 9461.759 | 12410.879 | 12853.247 | 12853.247 |
| ready_wait | 561543 | 756.463 | 402.175 | 2030.591 | 4132.863 | 7544.831 | 25165.823 |
| blocked | 561543 | 350.093 | 217.983 | 857.087 | 1669.119 | 2742.271 | 13656.063 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 9366.182 | 13282.9 |
| applied_total | 337361.694 | 552714 |
| bench_cpu_cores | 3.13 | 4.534 |
| bench_cpu_pct | 13.04 | 18.89 |
| bench_gc_gen0 | 1.796 | 4 |
| bench_gc_gen1 | 0.837 | 2 |
| bench_gc_gen2 | 0.49 | 2 |
| bench_gc_heap_mb | 1302.708 | 1735.7 |
| bench_mem_mb | 1938.586 | 2231.2 |
| bench_threadpool_queue | 86.306 | 803 |
| bench_threadpool_threads | 43.776 | 44 |
| broker_backlog | 90487 | 137633 |
| ctr_eb_mosquitto_cpu_pct | 42.135 | 51.15 |
| ctr_eb_mosquitto_mem_mb | 8.096 | 26.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.033 | 0.07 |
| ctr_eb_pgbouncer_2_mem_mb | 7.773 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.023 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 8.3 | 8.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.029 | 0.07 |
| ctr_eb_pgbouncer_4_mem_mb | 7.7 | 7.7 |
| ctr_eb_pgbouncer_cpu_pct | 0.023 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 8.443 | 8.7 |
| ctr_eb_postgres_cpu_pct | 1.616 | 2.54 |
| ctr_eb_postgres_mem_mb | 2192.635 | 2194.4 |
| ctr_geo_dc1a_cpu_pct | 886.8 | 1036.7 |
| ctr_geo_dc1a_mem_mb | 11823.996 | 12779.2 |
| ctr_geo_dc1b_cpu_pct | 63.39 | 72.84 |
| ctr_geo_dc1b_mem_mb | 3736.441 | 4193.3 |
| ctr_geo_etcd_dc1_cpu_pct | 2.364 | 3.13 |
| ctr_geo_etcd_dc1_mem_mb | 56.898 | 57.9 |
| ctr_geo_haproxy_cpu_pct | 133.606 | 151.75 |
| ctr_geo_haproxy_mem_mb | 32.545 | 34.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 70.468 | 80.29 |
| in_flight | 88811.837 | 139300 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.441 | 1.58 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 759946.449 | 1285508 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 182363.878 | 300779 |
| octopus_mqtt_sent_total | 206936.224 | 352936 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 14.204 | 19 |
| pg_active_loads | 7.286 | 22 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.924 | 100 |
| pg_conn_active | 52.735 | 74 |
| pg_conn_idle | 216.469 | 250 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 8.02 | 21 |
| pg_conn_total | 269.204 | 270 |
| pg_db_size_mb | 2439.004 | 3660.8 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 36912.762 | 38231.81 |
| pg_replica_replay_lag_ms | 210640.439 | 234419.5 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22042.088 | 27430.1 |
| pg_tup_del_per_s | 29041.188 | 38845 |
| pg_tup_fetch_per_s | 393910.514 | 497036.8 |
| pg_tup_ins_per_s | 41585.437 | 58702.2 |
| pg_tup_upd_per_s | 25864.378 | 30678.1 |
| pg_wal_mb_per_s | 75.493 | 105.4 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 11248.467 | 18527.7 |
| produced_total | 426173.531 | 692014 |
| publish_mb_per_s | 39.196 | 81.96 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 108,094 records are Completed |
| record_counts | pass | 108,094 records found, 108,094 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 393 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 732,732 events applied by workers, 732,732 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 390,357 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1669 ms, max 13655 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13213 seq 12: ready 112 ms before start, 112 ms of it with an idle slot in group 421
- [cross_source_blocking] source 14877 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 467
- [cross_source_blocking] source 13005 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 414
- [cross_source_blocking] source 7401 seq 4: ready 113 ms before start, 113 ms of it with an idle slot in group 255
- [cross_source_blocking] source 341 seq 14: ready 149 ms before start, 130 ms of it with an idle slot in group 10
- [cross_source_blocking] source 2857 seq 9: ready 113 ms before start, 113 ms of it with an idle slot in group 91
- [cross_source_blocking] source 6889 seq 9: ready 136 ms before start, 136 ms of it with an idle slot in group 234
- [cross_source_blocking] source 1989 seq 7: ready 119 ms before start, 119 ms of it with an idle slot in group 58
- [cross_source_blocking] source 1141 seq 10: ready 119 ms before start, 119 ms of it with an idle slot in group 38
- [cross_source_blocking] source 8096 seq 18: ready 118 ms before start, 118 ms of it with an idle slot in group 273
- [cross_source_blocking] source 6357 seq 4: ready 119 ms before start, 119 ms of it with an idle slot in group 217
- [cross_source_blocking] source 7273 seq 7: ready 129 ms before start, 129 ms of it with an idle slot in group 249
- [cross_source_blocking] source 1745 seq 2: ready 104 ms before start, 104 ms of it with an idle slot in group 50
- [cross_source_blocking] source 4709 seq 3: ready 104 ms before start, 104 ms of it with an idle slot in group 162
- [cross_source_blocking] source 2333 seq 15: ready 110 ms before start, 110 ms of it with an idle slot in group 67
- [cross_source_blocking] source 10945 seq 7: ready 119 ms before start, 119 ms of it with an idle slot in group 357
- [cross_source_blocking] source 12377 seq 4: ready 107 ms before start, 107 ms of it with an idle slot in group 393
- [cross_source_blocking] source 2560 seq 20: ready 132 ms before start, 132 ms of it with an idle slot in group 77
- [cross_source_blocking] source 8640 seq 8: ready 115 ms before start, 115 ms of it with an idle slot in group 299
- [cross_source_blocking] source 2993 seq 7: ready 114 ms before start, 114 ms of it with an idle slot in group 99
- [cross_source_blocking] source 1885 seq 4: ready 107 ms before start, 107 ms of it with an idle slot in group 54

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2601 | 2601 | 0 | 25821.183 | 47972.351 | 54001.663 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 8221 | 8221 | 0 | 27787.263 | 47710.207 | 51642.367 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2678 | 2678 | 0 | 22675.455 | 47448.063 | 55836.671 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5326 | 5326 | 0 | 27131.903 | 46399.487 | 49283.071 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2428 | 2428 | 0 | 21889.023 | 46137.343 | 49020.927 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2494 | 2494 | 0 | 19660.799 | 45875.199 | 52428.799 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 5702 | 5702 | 0 | 27918.335 | 45875.199 | 50331.647 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 5684 | 5684 | 0 | 27000.831 | 45875.199 | 51118.079 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2612 | 2612 | 0 | 24379.391 | 45613.055 | 49807.359 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2581 | 2581 | 0 | 27394.047 | 45613.055 | 47185.919 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5104 | 5104 | 0 | 25034.751 | 45613.055 | 49545.215 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5304 | 5304 | 0 | 25296.895 | 45088.767 | 49807.359 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2678 | 2678 | 0 | 26083.327 | 44826.623 | 50069.503 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2554 | 2554 | 0 | 23199.743 | 44564.479 | 47972.351 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 5425 | 5425 | 0 | 26869.759 | 43778.047 | 45350.911 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 7933 | 7933 | 0 | 24903.679 | 43515.903 | 49283.071 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 7789 | 7789 | 0 | 23855.103 | 43515.903 | 49545.215 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2734 | 2734 | 0 | 27131.903 | 43253.759 | 45350.911 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2692 | 2692 | 0 | 26214.399 | 43253.759 | 45350.911 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2614 | 2614 | 0 | 23461.887 | 42991.615 | 45875.199 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 11006 | 11006 | 0 | 27656.191 | 42991.615 | 46661.631 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 10946 | 10946 | 0 | 27918.335 | 42467.327 | 46399.487 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 7527 | 7527 | 0 | 22806.527 | 42467.327 | 46661.631 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 9433 | 9433 | 0 | 20316.159 | 41680.895 | 47710.207 |
| t-350 | 85 | 2 | 53.604 | 73.675 | 285 | 0 | 4250 | 4250 | 0 | 16121.855 | 41680.895 | 49020.927 |
