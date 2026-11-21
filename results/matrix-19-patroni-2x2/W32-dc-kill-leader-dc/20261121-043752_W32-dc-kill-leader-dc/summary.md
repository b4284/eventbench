# W32-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.871s, closing 2.615s, drain 120.083s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 559121 | 546659 | 7900.02 | 3200.998 | 21280 | 0 | 2858 | 2858 | 309 | 2856 | 65836 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 379280 | 39863.622 | 43253.759 | 60653.567 | 110297.087 | 143392.767 | 159121.407 |
| e2e_from_send | 379280 | 31886.644 | 31686.655 | 58589.183 | 91029.503 | 140640.255 | 157286.399 |
| producer_lag | 389568 | 8222.811 | 2727.935 | 21184.511 | 22953.983 | 23511.039 | 23920.639 |
| publish_call | 559121 | 199.992 | 110.911 | 234.623 | 605.695 | 20430.847 | 21446.655 |
| queue_wait | 379280 | 31687.107 | 31506.431 | 58458.111 | 89587.711 | 140509.183 | 152436.735 |
| broker_dwell | 379280 | 31469.775 | 31113.215 | 58327.039 | 89522.175 | 140378.111 | 152174.591 |
| receive_call | 515767 | 21.467 | 3.659 | 46.495 | 321.023 | 476.415 | 537.087 |
| ctx_load | 379280 | 94.834 | 1.44 | 6.635 | 43.743 | 15024.127 | 30015.487 |
| process | 379280 | 10.856 | 0.002 | 0.004 | 0.031 | 2633.727 | 3002.367 |
| ctx_commit | 379280 | 93.779 | 10.983 | 222.975 | 651.263 | 15015.935 | 43253.759 |
| ack | 379234 | 0.007 | 0.004 | 0.007 | 0.029 | 0.237 | 46.367 |
| service | 379280 | 199.519 | 13.159 | 251.263 | 1686.527 | 15147.007 | 60030.975 |
| e2e_large | 209 | 11005.171 | 5439.487 | 24739.839 | 27721.727 | 39649.279 | 39649.279 |
| ready_wait | 379280 | 1371.919 | 10.687 | 1150.975 | 45416.447 | 60850.175 | 136577.023 |
| blocked | 379280 | 709.739 | 6.655 | 357.631 | 24690.687 | 30408.703 | 33783.807 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3200.998 | 9338.5 |
| applied_total | 170609.061 | 229582 |
| bench_cpu_cores | 1.716 | 4.02 |
| bench_cpu_pct | 7.148 | 16.75 |
| bench_gc_gen0 | 1.163 | 3 |
| bench_gc_gen1 | 0.857 | 3 |
| bench_gc_gen2 | 0.367 | 1 |
| bench_gc_heap_mb | 1193.406 | 1545.4 |
| bench_mem_mb | 1796.629 | 1944.9 |
| bench_threadpool_queue | 11.755 | 210 |
| bench_threadpool_threads | 43.755 | 44 |
| broker_backlog | 157730.571 | 185169 |
| ctr_eb_dc1a_cpu_pct | 39.608 | 45.83 |
| ctr_eb_dc1a_mem_mb | 2972.438 | 3047.9 |
| ctr_eb_dc1b_cpu_pct | 647.487 | 743.73 |
| ctr_eb_dc1b_mem_mb | 10496.771 | 11577.6 |
| ctr_eb_dc2a_cpu_pct | 134.715 | 1015.15 |
| ctr_eb_dc2a_mem_mb | 3762.649 | 5482.7 |
| ctr_eb_dc2b_cpu_pct | 51.732 | 199.16 |
| ctr_eb_dc2b_mem_mb | 7126.545 | 8519.2 |
| ctr_eb_etcd_dc1_cpu_pct | 1.452 | 1.57 |
| ctr_eb_etcd_dc1_mem_mb | 28.88 | 35.2 |
| ctr_eb_etcd_dc2_cpu_pct | 1.524 | 2.61 |
| ctr_eb_etcd_dc2_mem_mb | 31.988 | 35.1 |
| ctr_eb_etcd_w_cpu_pct | 1.663 | 2.78 |
| ctr_eb_etcd_w_mem_mb | 63.892 | 66 |
| ctr_eb_mosquitto_cpu_pct | 22.444 | 50.72 |
| ctr_eb_mosquitto_mem_mb | 6.453 | 16.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.05 |
| ctr_eb_pgbouncer_mem_mb | 9 | 9 |
| ctr_eb_postgres_cpu_pct | 0.815 | 1.93 |
| ctr_eb_postgres_mem_mb | 1864.329 | 1886.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 58.351 | 2244.3 |
| host_cpu_pct | 37.849 | 85.71 |
| in_flight | 178319.122 | 294136 |
| nacks_per_s | 58.351 | 2244.3 |
| octopus_dispatcher_cores | 1.062 | 1.49 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 517795.204 | 639445 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 147246.816 | 181008 |
| octopus_mqtt_sent_total | 101343.184 | 128358 |
| octopus_publish_retries_total | 4243.306 | 14890 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.388 | 19 |
| pg_active_loads | 0.857 | 11 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 44.592 | 70 |
| pg_conn_idle | 223.041 | 230 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.02 | 1 |
| pg_conn_total | 267.633 | 268 |
| pg_db_size_mb | 1400.269 | 1583 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 14.676 | 55.33 |
| pg_replica_replay_lag_ms | 144.422 | 681.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18602.388 | 21165.6 |
| pg_tup_del_per_s | 26940.063 | 29183.1 |
| pg_tup_fetch_per_s | 508881.902 | 556256 |
| pg_tup_ins_per_s | 52752.976 | 56307.3 |
| pg_tup_upd_per_s | 19895.271 | 23079.7 |
| pg_wal_mb_per_s | 91.57 | 100.55 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7900.02 | 25365.8 |
| produced_total | 348928.184 | 523718 |
| publish_mb_per_s | 27.761 | 83.04 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15072/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 2,043 records not Completed |
| record_counts | **FAIL** | 86,398 records found, 87,568 expected; 928 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 309 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 546,659 events applied by workers, 559,121 generated |
| broker_drained | **FAIL** | 12,462 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 65,836 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 24691 ms, max 33778 ms) |

Sample mismatches:

- source 10246: record 44006234914819 left in status Open (version 15)
- source 13965: record 59979218288641 left in status Open (version 4)
- source 10292: record 44203803410438 left in status Open (version 4)
- source 2515: record 10801842749441 left in status Open (version 9)
- source 12202: record 52407190945793 left in status Open (version 4)
- source 10263: record 44079249358849 left in status Open (version 8)
- source 11935: record 51260434677761 left in status Open (version 10)
- source 14103: record 60571923775489 left in status Open (version 8)
- source 8793: record 37765647433729 left in status Open (version 14)
- source 12257: record 52643414147073 left in status Open (version 6)
- source 11965: record 51389283696641 left in status Open (version 14)
- source 10114: record 43439299231745 left in status Open (version 7)
- source 13951: record 59919088746497 left in status Open (version 5)
- source 11948: record 51316269252609 left in status Open (version 9)
- source 10182: record 43731357007880 left in status Open (version 1)
- source 6553: record 28144920690689 left in status Open (version 13)
- source 10245: record 44001939947521 left in status Open (version 5)
- source 12243: record 52583284604929 left in status Open (version 3)
- source 10293: record 44208098377734 left in status Open (version 2)
- source 15821: record 67950677590036 left in status Open (version 6)

## Cross-source blocking samples

- [cross_source_blocking] source 5868 seq 4: ready 168 ms before start, 168 ms of it with an idle slot in group 203
- [cross_source_blocking] source 7260 seq 6: ready 131 ms before start, 131 ms of it with an idle slot in group 248
- [cross_source_blocking] source 7392 seq 9: ready 184 ms before start, 184 ms of it with an idle slot in group 255
- [cross_source_blocking] source 4476 seq 7: ready 183 ms before start, 183 ms of it with an idle slot in group 154
- [cross_source_blocking] source 14168 seq 4: ready 184 ms before start, 184 ms of it with an idle slot in group 451
- [cross_source_blocking] source 2935 seq 4: ready 178 ms before start, 178 ms of it with an idle slot in group 94
- [cross_source_blocking] source 14634 seq 3: ready 188 ms before start, 188 ms of it with an idle slot in group 463
- [cross_source_blocking] source 9498 seq 4: ready 190 ms before start, 190 ms of it with an idle slot in group 328
- [cross_source_blocking] source 3351 seq 9: ready 308 ms before start, 308 ms of it with an idle slot in group 115
- [cross_source_blocking] source 6661 seq 12: ready 153 ms before start, 153 ms of it with an idle slot in group 226
- [cross_source_blocking] source 5559 seq 9: ready 312 ms before start, 312 ms of it with an idle slot in group 191
- [cross_source_blocking] source 4235 seq 5: ready 312 ms before start, 312 ms of it with an idle slot in group 142
- [cross_source_blocking] source 12023 seq 5: ready 312 ms before start, 242 ms of it with an idle slot in group 384
- [cross_source_blocking] source 12811 seq 4: ready 312 ms before start, 312 ms of it with an idle slot in group 404
- [cross_source_blocking] source 4500 seq 8: ready 155 ms before start, 155 ms of it with an idle slot in group 156
- [cross_source_blocking] source 2200 seq 4: ready 155 ms before start, 155 ms of it with an idle slot in group 64
- [cross_source_blocking] source 15130 seq 7: ready 153 ms before start, 153 ms of it with an idle slot in group 472
- [cross_source_blocking] source 2546 seq 4: ready 153 ms before start, 153 ms of it with an idle slot in group 76
- [cross_source_blocking] source 8265 seq 7: ready 154 ms before start, 154 ms of it with an idle slot in group 280
- [cross_source_blocking] source 15744 seq 8: ready 155 ms before start, 155 ms of it with an idle slot in group 491

## Error samples

- t-312#0 src=9031 seq=18 op=Update rec=38787849650178 ev=4: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3423 seq=4 op=Update rec=14701673054209 ev=2: NpgsqlException: No suitable host was found.
- t-339#0 src=9746 seq=9 op=Update rec=41858751266818 ev=2: NpgsqlException: Exception while reading from stream
- t-348#1 src=10525 seq=10 op=Update rec=45204530790402 ev=4: NpgsqlException: Exception while reading from stream
- t-405#0 src=12816 seq=14 op=Get rec=55044300865537 ev=12: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3442 seq=2 op=Create rec=14783277432834 ev=0: NpgsqlException: Exception while reading from stream
- t-372#0 src=11445 seq=6 op=Update rec=49155900702722 ev=1: NpgsqlException: Exception while reading from stream
- t-344#0 src=10148 seq=13 op=Update rec=43585328119811 ev=4: NpgsqlException: Exception while reading from stream
- t-374#0 src=11509 seq=28 op=Complete rec=49430778609667 ev=6: NpgsqlException: Exception while reading from stream
- t-231#0 src=6794 seq=8 op=Update rec=29180007809025 ev=7: NpgsqlException: Exception while reading from stream
- t-206#1 src=5989 seq=7 op=Get rec=25722559135746 ev=2: NpgsqlException: Exception while reading from stream
- t-026#0 src=872 seq=50 op=Update rec=3745211482117 ev=2: NpgsqlException: Exception while reading from stream
- t-066#0 src=2325 seq=13 op=Get rec=9985798963203 ev=3: NpgsqlException: Exception while reading from stream
- t-425#1 src=13256 seq=12 op=Update rec=56934086475777 ev=7: NpgsqlException: Exception while reading from stream
- t-257#0 src=7423 seq=35 op=Get rec=31881542238214 ev=4: NpgsqlException: Exception while reading from stream
- t-262#0 src=7673 seq=5 op=Create rec=32955284062210 ev=0: NpgsqlException: Exception while reading from stream
- t-057#0 src=1983 seq=23 op=Get rec=8516920147972 ev=7: NpgsqlException: Exception while reading from stream
- t-383#4 src=11824 seq=13 op=Update rec=50783693307906 ev=3: NpgsqlException: Exception while reading from stream
- t-425#2 src=13295 seq=14 op=Get rec=57101590200322 ev=5: NpgsqlException: Exception while reading from stream
- t-040#1 src=1186 seq=8 op=Get rec=5093831213058 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2085 | 1619 | 0 | 63176.703 | 152043.519 | 153092.095 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1751 | 1726 | 0 | 71827.455 | 149946.367 | 150994.943 |
| t-389 | 64 | 1 | 28.067 | 30.041 | 281 | 0 | 1425 | 1196 | 0 | 60293.119 | 148897.791 | 157286.399 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4347 | 4188 | 0 | 61865.983 | 148897.791 | 157286.399 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 3422 | 3307 | 0 | 54525.951 | 145752.063 | 148897.791 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4060 | 3958 | 0 | 60030.975 | 142606.335 | 158334.975 |
| t-274 | 29 | 1 | 56.437 | 27.244 | 1181 | 0 | 1164 | 1158 | 0 | 60293.119 | 141557.759 | 146800.639 |
| t-023 | 72 | 1 | 20.913 | 25.181 | 647 | 0 | 1184 | 1166 | 0 | 53739.519 | 140509.183 | 141557.759 |
| t-276 | 78 | 1 | 17.125 | 22.078 | 423 | 0 | 1018 | 1015 | 0 | 51118.079 | 139460.607 | 141557.759 |
| t-428 | 37 | 1 | 60.011 | 37.101 | 159 | 0 | 1599 | 1569 | 0 | 52166.655 | 139460.607 | 144703.487 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1955 | 1880 | 0 | 59244.543 | 135266.303 | 147849.215 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2015 | 1997 | 0 | 54263.807 | 134217.727 | 148897.791 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1792 | 1747 | 0 | 53215.231 | 133693.439 | 140509.183 |
| t-046 | 24 | 1 | 84.724 | 34.162 | 3235 | 0 | 1506 | 1487 | 0 | 54001.663 | 132120.575 | 140509.183 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 1909 | 1898 | 0 | 54788.095 | 131596.287 | 149946.367 |
| t-066 | 38 | 1 | 41.933 | 26.44 | 277 | 0 | 1164 | 1135 | 0 | 56098.815 | 129499.135 | 159383.551 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4088 | 3950 | 0 | 52690.943 | 128450.559 | 134217.727 |
| t-027 | 62 | 1 | 27.852 | 27.964 | 935 | 0 | 1283 | 1216 | 0 | 60293.119 | 127926.271 | 150994.943 |
| t-499 | 36 | 1 | 56.749 | 33.449 | 1538 | 0 | 1456 | 1430 | 0 | 55050.239 | 127401.983 | 130023.423 |
| t-374 | 28 | 2 | 178.846 | 82.743 | 1811 | 0 | 3388 | 3388 | 0 | 50855.935 | 126353.407 | 132644.863 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2025 | 2025 | 0 | 52690.943 | 124780.543 | 144703.487 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 3130 | 3130 | 0 | 41943.039 | 124256.255 | 130547.711 |
| t-182 | 89 | 3 | 81.941 | 119.217 | 2406 | 0 | 4997 | 4961 | 0 | 50855.935 | 123207.679 | 144703.487 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 11489 | 7935 | 0 | 60817.407 | 121110.527 | 147849.215 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 9388 | 7396 | 0 | 49545.215 | 119537.663 | 146800.639 |
