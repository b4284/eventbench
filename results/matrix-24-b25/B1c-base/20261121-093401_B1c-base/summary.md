# B1c-base — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.189s, closing 2.258s, drain 6.096s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000376 | 1000376 | 16161.367 | 15754.825 | 19913.1 | 0 | 0 | 0 | 458 | 5163 | 458069 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797003 | 2534.138 | 767.487 | 7942.143 | 16433.151 | 22233.087 | 32210.943 |
| e2e_from_send | 797003 | 2398.203 | 514.047 | 7782.399 | 16343.039 | 22200.319 | 32210.943 |
| producer_lag | 797003 | 135.936 | 4.823 | 545.791 | 1476.607 | 2045.951 | 2750.463 |
| publish_call | 1000376 | 68.635 | 50.271 | 140.159 | 318.463 | 656.895 | 911.871 |
| queue_wait | 797003 | 2338.798 | 450.047 | 7729.151 | 16293.887 | 22134.783 | 32194.559 |
| broker_dwell | 797003 | 2297.092 | 403.967 | 7684.095 | 16261.119 | 22118.399 | 32194.559 |
| receive_call | 815136 | 38.097 | 15.311 | 94.975 | 339.199 | 499.199 | 619.519 |
| ctx_load | 797003 | 14.905 | 10.903 | 31.359 | 66.623 | 108.351 | 191.487 |
| process | 797003 | 10.76 | 0.002 | 0.004 | 0.067 | 2619.391 | 3063.807 |
| ctx_commit | 797003 | 33.525 | 27.727 | 65.791 | 120.127 | 176.511 | 539.135 |
| ack | 796975 | 0.011 | 0.004 | 0.007 | 0.048 | 1.377 | 68.159 |
| service | 797003 | 59.405 | 43.167 | 88.575 | 167.423 | 2670.591 | 3444.735 |
| e2e_large | 356 | 2917.698 | 2828.287 | 4104.191 | 5652.479 | 6545.407 | 6545.407 |
| ready_wait | 797003 | 309.773 | 180.095 | 773.119 | 1710.079 | 2404.351 | 3121.151 |
| blocked | 797003 | 257.469 | 152.959 | 653.823 | 1275.903 | 1910.783 | 2580.479 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15754.825 | 18777.9 |
| applied_total | 531939.771 | 903497 |
| bench_cpu_cores | 5.543 | 6.784 |
| bench_cpu_pct | 23.095 | 28.27 |
| bench_gc_gen0 | 2.146 | 5 |
| bench_gc_gen1 | 0.75 | 2 |
| bench_gc_gen2 | 0.562 | 2 |
| bench_gc_heap_mb | 1427.496 | 1733.3 |
| bench_mem_mb | 2202.119 | 2507.9 |
| bench_threadpool_queue | 161.396 | 862 |
| bench_threadpool_threads | 42.646 | 44 |
| broker_backlog | 39120.104 | 48238 |
| ctr_eb_mosquitto_cpu_pct | 64.972 | 70.5 |
| ctr_eb_mosquitto_mem_mb | 7.673 | 13.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.772 | 23.28 |
| ctr_eb_pgbouncer_2_mem_mb | 15.185 | 15.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 21.004 | 23.69 |
| ctr_eb_pgbouncer_3_mem_mb | 14.99 | 15.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 22.702 | 26.83 |
| ctr_eb_pgbouncer_4_mem_mb | 15.088 | 17.8 |
| ctr_eb_pgbouncer_cpu_pct | 20.846 | 23.95 |
| ctr_eb_pgbouncer_mem_mb | 15.308 | 16.4 |
| ctr_eb_postgres_cpu_pct | 876.005 | 960.91 |
| ctr_eb_postgres_mem_mb | 5769.219 | 5966.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.914 | 82.07 |
| in_flight | 36437.729 | 46175 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.811 | 1.85 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1080517.333 | 1857011 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 206456.125 | 349415 |
| octopus_mqtt_sent_total | 280485.458 | 500829 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.083 | 18 |
| pg_active_loads | 6.812 | 18 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 20.271 | 55 |
| pg_conn_idle | 239.354 | 253 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.958 | 13 |
| pg_conn_total | 259.625 | 261 |
| pg_db_size_mb | 3676.369 | 4127.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19074.248 | 22153.1 |
| pg_tup_del_per_s | 43310.417 | 51177.8 |
| pg_tup_fetch_per_s | 425437.508 | 485980.1 |
| pg_tup_ins_per_s | 55251.996 | 75638.3 |
| pg_tup_upd_per_s | 42147.74 | 47221.1 |
| pg_wal_mb_per_s | 111.319 | 171.91 |
| pgb_cl_active | 253.354 | 271 |
| pgb_cl_waiting | 0.312 | 5 |
| pgb_maxwait_ms | 0.03 | 0.48 |
| pgb_sv_active | 15.833 | 48 |
| pgb_sv_idle | 32.167 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16161.367 | 22869.2 |
| produced_total | 568377.5 | 943903 |
| publish_mb_per_s | 52.975 | 89.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,858 records are Completed |
| record_counts | pass | 138,858 records found, 138,858 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,376 events applied by workers, 1,000,376 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 458,069 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1276 ms, max 2580 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10324 seq 5: ready 211 ms before start, 211 ms of it with an idle slot in group 346
- [cross_source_blocking] source 3172 seq 18: ready 207 ms before start, 207 ms of it with an idle slot in group 105
- [cross_source_blocking] source 296 seq 20: ready 215 ms before start, 215 ms of it with an idle slot in group 8
- [cross_source_blocking] source 11516 seq 32: ready 121 ms before start, 121 ms of it with an idle slot in group 374
- [cross_source_blocking] source 14416 seq 5: ready 164 ms before start, 164 ms of it with an idle slot in group 459
- [cross_source_blocking] source 15636 seq 23: ready 217 ms before start, 217 ms of it with an idle slot in group 487
- [cross_source_blocking] source 15128 seq 5: ready 214 ms before start, 214 ms of it with an idle slot in group 472
- [cross_source_blocking] source 9436 seq 7: ready 214 ms before start, 214 ms of it with an idle slot in group 325
- [cross_source_blocking] source 14228 seq 21: ready 212 ms before start, 212 ms of it with an idle slot in group 456
- [cross_source_blocking] source 13868 seq 11: ready 164 ms before start, 164 ms of it with an idle slot in group 439
- [cross_source_blocking] source 14068 seq 9: ready 212 ms before start, 212 ms of it with an idle slot in group 447
- [cross_source_blocking] source 3260 seq 9: ready 127 ms before start, 104 ms of it with an idle slot in group 109
- [cross_source_blocking] source 6736 seq 9: ready 179 ms before start, 179 ms of it with an idle slot in group 229
- [cross_source_blocking] source 996 seq 5: ready 179 ms before start, 179 ms of it with an idle slot in group 30
- [cross_source_blocking] source 2332 seq 23: ready 103 ms before start, 103 ms of it with an idle slot in group 67
- [cross_source_blocking] source 4628 seq 14: ready 177 ms before start, 177 ms of it with an idle slot in group 161
- [cross_source_blocking] source 3156 seq 24: ready 175 ms before start, 175 ms of it with an idle slot in group 105
- [cross_source_blocking] source 3304 seq 11: ready 215 ms before start, 215 ms of it with an idle slot in group 111
- [cross_source_blocking] source 9392 seq 5: ready 183 ms before start, 183 ms of it with an idle slot in group 325
- [cross_source_blocking] source 8212 seq 6: ready 211 ms before start, 211 ms of it with an idle slot in group 276

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 6979.583 | 23986.175 | 32243.711 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9221 | 9221 | 0 | 8290.303 | 23855.103 | 28311.551 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 6651.903 | 23855.103 | 26345.471 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9091 | 9091 | 0 | 9109.503 | 23592.959 | 30932.991 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 6586.367 | 22544.383 | 28049.407 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 6848.511 | 22151.167 | 25165.823 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 7798.783 | 22020.095 | 25559.039 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15138 | 15138 | 0 | 7569.407 | 21495.807 | 26738.687 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11852 | 11852 | 0 | 7471.103 | 21102.591 | 25821.183 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6098 | 6098 | 0 | 6356.991 | 20578.303 | 27656.191 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 5767.167 | 20316.159 | 29360.127 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 7208.959 | 20316.159 | 23330.815 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14677 | 14677 | 0 | 7929.855 | 20185.087 | 25690.111 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20596 | 20596 | 0 | 6193.151 | 19660.799 | 26869.759 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 5996.543 | 19267.583 | 20578.303 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5540 | 5540 | 0 | 7307.263 | 19136.511 | 20709.375 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11133 | 11133 | 0 | 6488.063 | 18743.295 | 25559.039 |
| t-231 | 43 | 1 | 62.465 | 43.112 | 156 | 0 | 2725 | 2725 | 0 | 7438.335 | 18743.295 | 21495.807 |
| t-332 | 39 | 1 | 72.81 | 47.362 | 812 | 0 | 2933 | 2933 | 0 | 7307.263 | 18743.295 | 21757.951 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 5177.343 | 18743.295 | 21757.951 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 5570.559 | 18481.151 | 23330.815 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5827 | 5827 | 0 | 5898.239 | 18481.151 | 25034.751 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2850 | 2850 | 0 | 4112.383 | 17956.863 | 22151.167 |
| t-049 | 53 | 1 | 45.862 | 40.95 | 350 | 0 | 2675 | 2675 | 0 | 4292.607 | 17694.719 | 19660.799 |
| t-242 | 28 | 1 | 77.975 | 35.745 | 1662 | 0 | 2233 | 2233 | 0 | 1245.183 | 17563.647 | 19136.511 |
