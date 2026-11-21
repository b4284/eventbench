# W21-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.457s, closing 1.87s, drain 20.795s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 431005 | 431005 | 5300.739 | 3056.939 | 22086 | 0 | 3296 | 3296 | 263 | 2185 | 55422 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 262782 | 32754.971 | 39714.815 | 50167.807 | 56131.583 | 59670.527 | 62160.895 |
| e2e_from_send | 262782 | 25368.511 | 32964.607 | 49151.999 | 55017.471 | 58556.415 | 61505.535 |
| producer_lag | 263247 | 7377.147 | 1271.807 | 29540.351 | 31031.295 | 31195.135 | 31866.879 |
| publish_call | 431005 | 251.497 | 115.583 | 298.751 | 952.831 | 23740.415 | 24723.455 |
| queue_wait | 262782 | 25221.606 | 32849.919 | 49119.231 | 54951.935 | 58523.647 | 61505.535 |
| broker_dwell | 262782 | 25000.094 | 32325.631 | 48988.159 | 54853.631 | 58392.575 | 61177.855 |
| receive_call | 382724 | 26.907 | 6.331 | 62.111 | 359.935 | 484.095 | 549.887 |
| ctx_load | 262782 | 12.541 | 3.477 | 13.655 | 71.167 | 154.111 | 15015.935 |
| process | 262782 | 10.905 | 0.002 | 0.004 | 0.042 | 2631.679 | 3002.367 |
| ctx_commit | 262782 | 123.367 | 32.111 | 308.735 | 1161.215 | 1489.919 | 1585.151 |
| ack | 262741 | 0.008 | 0.004 | 0.007 | 0.033 | 0.452 | 51.103 |
| service | 262782 | 146.896 | 38.687 | 329.727 | 1363.967 | 2928.639 | 17793.023 |
| e2e_large | 163 | 10543.754 | 4345.855 | 32276.479 | 33816.575 | 34963.455 | 34963.455 |
| ready_wait | 262782 | 1537.279 | 19.519 | 1545.215 | 38830.079 | 47742.975 | 54198.271 |
| blocked | 262782 | 1032.931 | 14.975 | 424.959 | 28950.527 | 32260.095 | 34275.327 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3056.939 | 15030.9 |
| applied_total | 164313.163 | 222591 |
| bench_cpu_cores | 1.476 | 4.097 |
| bench_cpu_pct | 6.148 | 17.07 |
| bench_gc_gen0 | 0.959 | 3 |
| bench_gc_gen1 | 0.633 | 3 |
| bench_gc_gen2 | 0.286 | 1 |
| bench_gc_heap_mb | 1042.457 | 1393.1 |
| bench_mem_mb | 1687.357 | 1832.9 |
| bench_threadpool_queue | 10.449 | 189 |
| bench_threadpool_threads | 42.531 | 44 |
| broker_backlog | 143016.98 | 174633 |
| ctr_eb_dc1a_cpu_pct | 68.851 | 374.34 |
| ctr_eb_dc1a_mem_mb | 3828.265 | 4077.9 |
| ctr_eb_dc1b_cpu_pct | 92.957 | 982.56 |
| ctr_eb_dc1b_mem_mb | 7134.482 | 8790 |
| ctr_eb_dc2a_cpu_pct | 45.239 | 64.54 |
| ctr_eb_dc2a_mem_mb | 4125.914 | 4350.7 |
| ctr_eb_dc2b_cpu_pct | 664.529 | 806.87 |
| ctr_eb_dc2b_mem_mb | 8239.014 | 9026.5 |
| ctr_eb_etcd_dc1_cpu_pct | 1.39 | 3.04 |
| ctr_eb_etcd_dc1_mem_mb | 39.7 | 44.5 |
| ctr_eb_etcd_dc2_cpu_pct | 1.558 | 1.97 |
| ctr_eb_etcd_dc2_mem_mb | 32.5 | 33.4 |
| ctr_eb_etcd_w_cpu_pct | 1.47 | 2.58 |
| ctr_eb_etcd_w_mem_mb | 55.014 | 57 |
| ctr_eb_mosquitto_cpu_pct | 18.129 | 47.13 |
| ctr_eb_mosquitto_mem_mb | 7.155 | 30.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.816 | 8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.118 | 9.4 |
| ctr_eb_postgres_cpu_pct | 0.842 | 2.03 |
| ctr_eb_postgres_mem_mb | 1925.031 | 1926.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 67.2 | 1990.8 |
| host_cpu_pct | 34.422 | 86.85 |
| in_flight | 146196.878 | 172800 |
| nacks_per_s | 67.2 | 1990.8 |
| octopus_dispatcher_cores | 1.076 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 495587.592 | 676212 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 149521.816 | 197902 |
| octopus_mqtt_sent_total | 101059.163 | 150888 |
| octopus_publish_retries_total | 7206.041 | 18914 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.653 | 22 |
| pg_active_loads | 2.265 | 18 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 50.082 | 91 |
| pg_conn_idle | 218.592 | 266 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.49 | 6 |
| pg_conn_total | 268.673 | 269 |
| pg_db_size_mb | 1413.433 | 1558.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 27.287 | 121.26 |
| pg_replica_replay_lag_ms | 221.576 | 1181.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 10129.076 | 20204.3 |
| pg_tup_del_per_s | 15686.645 | 32582.5 |
| pg_tup_fetch_per_s | 398243.547 | 539354.5 |
| pg_tup_ins_per_s | 27805.357 | 58261.4 |
| pg_tup_upd_per_s | 10067.896 | 21771.3 |
| pg_wal_mb_per_s | 56.422 | 106.13 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 5300.739 | 16742.8 |
| produced_total | 310510.041 | 395391 |
| publish_mb_per_s | 20.42 | 75.11 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 71,968 records are Completed |
| record_counts | pass | 71,968 records found, 71,968 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 263 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 431,005 events applied by workers, 431,005 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 55,422 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 28951 ms, max 34262 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6357 seq 4: ready 294 ms before start, 294 ms of it with an idle slot in group 217
- [cross_source_blocking] source 1141 seq 10: ready 294 ms before start, 294 ms of it with an idle slot in group 38
- [cross_source_blocking] source 9533 seq 5: ready 260 ms before start, 260 ms of it with an idle slot in group 331
- [cross_source_blocking] source 1337 seq 7: ready 318 ms before start, 318 ms of it with an idle slot in group 42
- [cross_source_blocking] source 9685 seq 10: ready 318 ms before start, 318 ms of it with an idle slot in group 336
- [cross_source_blocking] source 5081 seq 6: ready 318 ms before start, 318 ms of it with an idle slot in group 177
- [cross_source_blocking] source 2357 seq 7: ready 293 ms before start, 293 ms of it with an idle slot in group 68
- [cross_source_blocking] source 4277 seq 10: ready 276 ms before start, 276 ms of it with an idle slot in group 145
- [cross_source_blocking] source 9498 seq 4: ready 204 ms before start, 204 ms of it with an idle slot in group 328
- [cross_source_blocking] source 605 seq 7: ready 176 ms before start, 176 ms of it with an idle slot in group 17
- [cross_source_blocking] source 14201 seq 22: ready 293 ms before start, 293 ms of it with an idle slot in group 454
- [cross_source_blocking] source 8917 seq 7: ready 293 ms before start, 293 ms of it with an idle slot in group 307
- [cross_source_blocking] source 861 seq 11: ready 276 ms before start, 276 ms of it with an idle slot in group 24
- [cross_source_blocking] source 2933 seq 7: ready 293 ms before start, 293 ms of it with an idle slot in group 94
- [cross_source_blocking] source 1785 seq 4: ready 293 ms before start, 293 ms of it with an idle slot in group 51
- [cross_source_blocking] source 12665 seq 8: ready 206 ms before start, 206 ms of it with an idle slot in group 398
- [cross_source_blocking] source 13669 seq 11: ready 276 ms before start, 276 ms of it with an idle slot in group 435
- [cross_source_blocking] source 5673 seq 17: ready 293 ms before start, 293 ms of it with an idle slot in group 195
- [cross_source_blocking] source 15253 seq 7: ready 293 ms before start, 293 ms of it with an idle slot in group 476
- [cross_source_blocking] source 6653 seq 4: ready 180 ms before start, 180 ms of it with an idle slot in group 224

## Error samples

- t-340#0 src=9923 seq=5 op=Update rec=42618960478210 ev=1: NpgsqlException: No suitable host was found.
- t-021#0 src=712 seq=6 op=Update rec=3058016714754 ev=3: NpgsqlException: No suitable host was found.
- t-033#0 src=1077 seq=47 op=Update rec=4625679777798 ev=5: NpgsqlException: No suitable host was found.
- t-226#0 src=6663 seq=33 op=Update rec=28617367093252 ev=5: NpgsqlException: No suitable host was found.
- t-396#0 src=12447 seq=11 op=Update rec=53459457933313 ev=8: NpgsqlException: No suitable host was found.
- t-197#1 src=5710 seq=30 op=Get rec=24524263260165 ev=1: NpgsqlException: No suitable host was found.
- t-417#0 src=13111 seq=12 op=Update rec=56311316217858 ev=2: NpgsqlException: No suitable host was found.
- t-065#2 src=2272 seq=47 op=Create rec=9758165696520 ev=0: NpgsqlException: No suitable host was found.
- t-160#1 src=4576 seq=15 op=Create rec=19653770346499 ev=0: NpgsqlException: No suitable host was found.
- t-026#0 src=872 seq=44 op=Get rec=3745211482120 ev=2: NpgsqlException: No suitable host was found.
- t-206#1 src=6015 seq=9 op=Complete rec=25834228285441 ev=8: NpgsqlException: No suitable host was found.
- t-208#0 src=6071 seq=21 op=Update rec=26074746454021 ev=2: NpgsqlException: No suitable host was found.
- t-037#0 src=1123 seq=22 op=Get rec=4823248273411 ev=2: NpgsqlException: No suitable host was found.
- t-314#1 src=9097 seq=18 op=Update rec=39071317491713 ev=12: NpgsqlException: No suitable host was found.
- t-290#0 src=8491 seq=22 op=Update rec=36468567310338 ev=2: NpgsqlException: No suitable host was found.
- t-048#0 src=1616 seq=9 op=Get rec=6940667150338 ev=3: NpgsqlException: No suitable host was found.
- t-413#0 src=13004 seq=18 op=Update rec=55851754717185 ev=4: NpgsqlException: No suitable host was found.
- t-214#0 src=6294 seq=9 op=Complete rec=27032524161025 ev=5: NpgsqlException: No suitable host was found.
- t-480#0 src=15349 seq=10 op=Update rec=65923453026306 ev=2: NpgsqlException: No suitable host was found.
- t-392#0 src=12353 seq=9 op=Update rec=53055731007490 ev=2: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1458 | 1458 | 0 | 51642.367 | 61079.551 | 61865.983 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3220 | 3220 | 0 | 48234.495 | 60293.119 | 61865.983 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1524 | 1524 | 0 | 48234.495 | 60293.119 | 61341.695 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7145 | 7145 | 0 | 44826.623 | 59768.831 | 61603.839 |
| t-200 | 62 | 1 | 28.398 | 29.019 | 348 | 0 | 979 | 979 | 0 | 48496.639 | 59768.831 | 62128.127 |
| t-074 | 30 | 1 | 59.845 | 28.959 | 1515 | 0 | 946 | 946 | 0 | 47185.919 | 59244.543 | 61603.839 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 980 | 980 | 0 | 46137.343 | 59244.543 | 60555.263 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 2378 | 2378 | 0 | 44040.191 | 59244.543 | 61603.839 |
| t-340 | 184 | 2 | 24.502 | 75.472 | 141 | 0 | 2680 | 2680 | 0 | 47185.919 | 59244.543 | 60293.119 |
| t-460 | 71 | 1 | 20.528 | 24.491 | 141 | 0 | 915 | 915 | 0 | 46661.631 | 59244.543 | 60030.975 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6357 | 6357 | 0 | 46661.631 | 59244.543 | 60817.407 |
| t-259 | 70 | 1 | 18.035 | 20.648 | 160 | 0 | 817 | 817 | 0 | 46399.487 | 58982.399 | 60030.975 |
| t-104 | 46 | 1 | 31.882 | 24.451 | 2163 | 0 | 830 | 830 | 0 | 46137.343 | 58720.255 | 60030.975 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4329 | 4329 | 0 | 46137.343 | 58458.111 | 60293.119 |
| t-409 | 48 | 2 | 64.626 | 50.933 | 671 | 0 | 1666 | 1666 | 0 | 38797.311 | 58458.111 | 60293.119 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1539 | 1539 | 0 | 45350.911 | 57933.823 | 58720.255 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1625 | 1625 | 0 | 45088.767 | 57933.823 | 59506.687 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 3897 | 3897 | 0 | 44826.623 | 57933.823 | 61603.839 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3323 | 3323 | 0 | 45088.767 | 57933.823 | 58458.111 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1518 | 1518 | 0 | 45875.199 | 57671.679 | 59506.687 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3363 | 3363 | 0 | 45613.055 | 57671.679 | 60817.407 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1463 | 1463 | 0 | 45088.767 | 57671.679 | 58195.967 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1587 | 1587 | 0 | 45875.199 | 57147.391 | 57671.679 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1551 | 1551 | 0 | 44564.479 | 56885.247 | 58195.967 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3146 | 3146 | 0 | 45088.767 | 56885.247 | 57933.823 |
