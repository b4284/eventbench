# V2b-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.271s, closing 2.936s, drain 16.276s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 538674 | 538674 | 7379.504 | 5552.218 | 22450.1 | 0 | 2784 | 2784 | 303 | 2742 | 146520 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 369337 | 21840.48 | 22413.311 | 42303.487 | 50200.575 | 53346.303 | 57475.071 |
| e2e_from_send | 369337 | 16621.614 | 9707.519 | 42106.879 | 49381.375 | 52592.639 | 56197.119 |
| producer_lag | 369407 | 5217.911 | 249.727 | 21725.183 | 22118.399 | 22265.855 | 22872.063 |
| publish_call | 538674 | 186.595 | 91.775 | 201.087 | 518.655 | 22036.479 | 22396.927 |
| queue_wait | 369337 | 16533.229 | 9633.791 | 42074.111 | 49348.607 | 52527.103 | 56164.351 |
| broker_dwell | 369337 | 16349.529 | 9453.567 | 41975.807 | 49250.303 | 52461.567 | 56033.279 |
| receive_call | 468146 | 32.446 | 8.051 | 90.047 | 362.239 | 483.071 | 566.271 |
| ctx_load | 369337 | 9.205 | 5.183 | 21.263 | 55.903 | 133.119 | 194.559 |
| process | 369337 | 10.792 | 0.002 | 0.004 | 0.051 | 2625.535 | 3008.511 |
| ctx_commit | 369337 | 68.326 | 53.567 | 133.631 | 465.151 | 851.967 | 971.263 |
| ack | 368610 | 0.008 | 0.003 | 0.005 | 0.03 | 0.661 | 68.287 |
| service | 369337 | 88.38 | 63.839 | 149.887 | 590.335 | 2723.839 | 3676.159 |
| e2e_large | 202 | 8264.045 | 3848.191 | 24854.527 | 26853.375 | 29409.279 | 29409.279 |
| ready_wait | 369337 | 1174.287 | 65.311 | 1393.663 | 34144.255 | 38862.847 | 43974.655 |
| blocked | 369337 | 892.472 | 42.239 | 811.007 | 27230.207 | 30932.991 | 32751.615 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5552.218 | 14106.7 |
| applied_total | 252922.102 | 356169 |
| bench_cpu_cores | 2.117 | 4.348 |
| bench_cpu_pct | 8.82 | 18.12 |
| bench_gc_gen0 | 1.735 | 5 |
| bench_gc_gen1 | 1.551 | 5 |
| bench_gc_gen2 | 0.367 | 2 |
| bench_gc_heap_mb | 979.276 | 1261.6 |
| bench_mem_mb | 1599.753 | 1726.4 |
| bench_threadpool_queue | 54.02 | 453 |
| bench_threadpool_threads | 42.082 | 44 |
| broker_backlog | 112416.714 | 144116 |
| ctr_eb_dc1a_cpu_pct | 931.777 | 986.08 |
| ctr_eb_dc1a_mem_mb | 10170.317 | 10461.8 |
| ctr_eb_dc1b_cpu_pct | 61.944 | 67.6 |
| ctr_eb_dc1b_mem_mb | 3008.496 | 3062.2 |
| ctr_eb_dc2a_cpu_pct | 142.509 | 1045.08 |
| ctr_eb_dc2a_mem_mb | 3419.561 | 4860.9 |
| ctr_eb_dc2b_cpu_pct | 81.178 | 397.07 |
| ctr_eb_dc2b_mem_mb | 3219.569 | 3646.9 |
| ctr_eb_etcd_dc1_cpu_pct | 1.813 | 2.02 |
| ctr_eb_etcd_dc1_mem_mb | 34.839 | 35.4 |
| ctr_eb_etcd_dc2_cpu_pct | 1.524 | 2.29 |
| ctr_eb_etcd_dc2_mem_mb | 30.218 | 33.7 |
| ctr_eb_etcd_w_cpu_pct | 1.964 | 2.96 |
| ctr_eb_etcd_w_mem_mb | 43.369 | 46.6 |
| ctr_eb_mosquitto_cpu_pct | 31.941 | 63 |
| ctr_eb_mosquitto_mem_mb | 10.918 | 34 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.5 | 9.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.024 | 0.05 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.024 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.916 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.06 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.901 | 2.4 |
| ctr_eb_postgres_mem_mb | 2106.414 | 2130.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 57.261 | 1519.4 |
| host_cpu_pct | 49.142 | 93.59 |
| in_flight | 113843.878 | 145962 |
| nacks_per_s | 57.261 | 1519.4 |
| octopus_dispatcher_cores | 0.992 | 1.65 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 639754.898 | 921398 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 164871.224 | 230312 |
| octopus_mqtt_sent_total | 159548.776 | 236976 |
| octopus_publish_retries_total | 11325.551 | 27749 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.776 | 24 |
| pg_active_loads | 22.571 | 33 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 73.633 | 78 |
| pg_conn_idle | 195.51 | 218 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.245 | 43 |
| pg_conn_total | 269.143 | 270 |
| pg_db_size_mb | 1474.8 | 1640.3 |
| pg_deadlocks | 1 | 1 |
| pg_replica_lag_mb | 5.866 | 52.83 |
| pg_replica_replay_lag_ms | 52.398 | 489.5 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24531.529 | 27124.3 |
| pg_tup_del_per_s | 35523.229 | 37759.6 |
| pg_tup_fetch_per_s | 469479.343 | 517706.6 |
| pg_tup_ins_per_s | 48796.445 | 58820.1 |
| pg_tup_upd_per_s | 28762.555 | 32007.9 |
| pg_wal_mb_per_s | 96.572 | 139.27 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7379.504 | 18154.5 |
| produced_total | 366765.98 | 502131 |
| publish_mb_per_s | 26.317 | 83.37 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 85,109 records are Completed |
| record_counts | pass | 85,109 records found, 85,109 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 303 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 538,674 events applied by workers, 538,674 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 146,520 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27230 ms, max 32745 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13213 seq 12: ready 206 ms before start, 206 ms of it with an idle slot in group 421
- [cross_source_blocking] source 14862 seq 5: ready 249 ms before start, 249 ms of it with an idle slot in group 466
- [cross_source_blocking] source 8670 seq 4: ready 189 ms before start, 189 ms of it with an idle slot in group 300
- [cross_source_blocking] source 2453 seq 8: ready 175 ms before start, 175 ms of it with an idle slot in group 72
- [cross_source_blocking] source 3641 seq 7: ready 160 ms before start, 160 ms of it with an idle slot in group 123
- [cross_source_blocking] source 3642 seq 9: ready 196 ms before start, 196 ms of it with an idle slot in group 123
- [cross_source_blocking] source 314 seq 2: ready 247 ms before start, 247 ms of it with an idle slot in group 9
- [cross_source_blocking] source 7997 seq 8: ready 164 ms before start, 164 ms of it with an idle slot in group 270
- [cross_source_blocking] source 15628 seq 15: ready 188 ms before start, 188 ms of it with an idle slot in group 486
- [cross_source_blocking] source 12382 seq 6: ready 167 ms before start, 167 ms of it with an idle slot in group 393
- [cross_source_blocking] source 1885 seq 4: ready 160 ms before start, 160 ms of it with an idle slot in group 54
- [cross_source_blocking] source 14894 seq 6: ready 249 ms before start, 249 ms of it with an idle slot in group 467
- [cross_source_blocking] source 1977 seq 15: ready 233 ms before start, 233 ms of it with an idle slot in group 57
- [cross_source_blocking] source 2885 seq 7: ready 102 ms before start, 102 ms of it with an idle slot in group 92
- [cross_source_blocking] source 9085 seq 3: ready 101 ms before start, 101 ms of it with an idle slot in group 313
- [cross_source_blocking] source 6357 seq 4: ready 111 ms before start, 111 ms of it with an idle slot in group 217
- [cross_source_blocking] source 1141 seq 10: ready 112 ms before start, 112 ms of it with an idle slot in group 38
- [cross_source_blocking] source 5009 seq 9: ready 252 ms before start, 252 ms of it with an idle slot in group 173
- [cross_source_blocking] source 10924 seq 14: ready 104 ms before start, 104 ms of it with an idle slot in group 356
- [cross_source_blocking] source 7909 seq 16: ready 192 ms before start, 192 ms of it with an idle slot in group 266

## Error samples

- t-184#0 src=5402 seq=45 op=Create rec=23201413332999 ev=0: NpgsqlException: No suitable host was found.
- t-401#0 src=12720 seq=14 op=Update rec=54631984005122 ev=4: NpgsqlException: No suitable host was found.
- t-085#0 src=2766 seq=24 op=Get rec=11879879540738 ev=4: NpgsqlException: No suitable host was found.
- t-184#0 src=5394 seq=42 op=Update rec=23167053594633 ev=1: NpgsqlException: No suitable host was found.
- t-455#0 src=14211 seq=27 op=Update rec=61035780243459 ev=6: NpgsqlException: No suitable host was found.
- t-487#0 src=15642 seq=55 op=Update rec=67181878444038 ev=4: NpgsqlException: No suitable host was found.
- t-171#0 src=4978 seq=58 op=Get rec=21380347199494 ev=3: NpgsqlException: Exception while reading from stream
- t-184#0 src=5397 seq=42 op=Update rec=23179938496516 ev=5: NpgsqlException: No suitable host was found.
- t-074#0 src=2483 seq=26 op=Create rec=10664403795975 ev=0: NpgsqlException: Exception while reading from stream
- t-245#0 src=7213 seq=18 op=Update rec=30979599106050 ev=6: NpgsqlException: Exception while reading from stream
- t-450#1 src=14124 seq=25 op=Complete rec=60662118088709 ev=1: NpgsqlException: Exception while reading from stream
- t-112#0 src=3322 seq=33 op=Get rec=14267881357317 ev=3: NpgsqlException: No suitable host was found.
- t-085#0 src=2769 seq=22 op=Create rec=11892764442631 ev=0: NpgsqlException: No suitable host was found.
- t-467#0 src=14898 seq=11 op=Update rec=63986422775810 ev=2: NpgsqlException: No suitable host was found.
- t-188#0 src=5484 seq=17 op=Create rec=23553600651268 ev=0: NpgsqlException: No suitable host was found.
- t-467#0 src=14878 seq=7 op=Get rec=63900523429890 ev=1: NpgsqlException: No suitable host was found.
- t-106#1 src=3197 seq=62 op=Update rec=13731010445318 ev=7: NpgsqlException: No suitable host was found.
- t-188#0 src=5465 seq=29 op=Update rec=23471996272642 ev=10: NpgsqlException: No suitable host was found.
- t-058#0 src=2004 seq=20 op=Update rec=8607114461189 ev=4: NpgsqlException: No suitable host was found.
- t-106#1 src=3199 seq=61 op=Update rec=13739600379915 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1889 | 1889 | 0 | 40632.319 | 55836.671 | 57671.679 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1944 | 1944 | 0 | 37486.591 | 55050.239 | 56623.103 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1778 | 1778 | 0 | 38273.023 | 54001.663 | 55574.527 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1945 | 1945 | 0 | 39059.455 | 54001.663 | 55050.239 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4209 | 4209 | 0 | 38010.879 | 53739.519 | 54788.095 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2026 | 2026 | 0 | 38535.167 | 53739.519 | 55050.239 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3969 | 3969 | 0 | 38273.023 | 53215.231 | 55050.239 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3920 | 3920 | 0 | 38535.167 | 53215.231 | 55574.527 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1919 | 1919 | 0 | 37486.591 | 53215.231 | 54525.951 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4206 | 4206 | 0 | 38010.879 | 52953.087 | 54525.951 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1897 | 1897 | 0 | 38010.879 | 52690.943 | 54001.663 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5957 | 5957 | 0 | 37224.447 | 52690.943 | 54263.807 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5447 | 5447 | 0 | 36700.159 | 52690.943 | 54788.095 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1699 | 1699 | 0 | 37224.447 | 52428.799 | 54525.951 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1845 | 1845 | 0 | 36962.303 | 52166.655 | 53477.375 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2010 | 2010 | 0 | 36700.159 | 52166.655 | 53477.375 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3745 | 3745 | 0 | 35913.727 | 52166.655 | 53477.375 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5740 | 5740 | 0 | 37224.447 | 52166.655 | 53739.519 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7922 | 7922 | 0 | 37748.735 | 51904.511 | 53739.519 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3928 | 3928 | 0 | 37224.447 | 51904.511 | 52953.087 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1688 | 1688 | 0 | 35127.295 | 51380.223 | 53215.231 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 8017 | 8017 | 0 | 37486.591 | 51380.223 | 52953.087 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1730 | 1730 | 0 | 36438.015 | 51118.079 | 53215.231 |
| t-283 | 74 | 1 | 27.256 | 32.897 | 240 | 0 | 1442 | 1442 | 0 | 32505.855 | 50855.935 | 52428.799 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5539 | 5539 | 0 | 36700.159 | 50855.935 | 53739.519 |
