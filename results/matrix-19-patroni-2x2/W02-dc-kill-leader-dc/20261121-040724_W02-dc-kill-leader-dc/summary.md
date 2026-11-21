# W02-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.311s, closing 1.122s, drain 120.086s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 558477 | 528460 | 7766.89 | 3985.284 | 22837 | 0 | 2730 | 2730 | 309 | 2846 | 95458 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 362173 | 29714.45 | 34635.775 | 53837.823 | 64520.191 | 144703.487 | 166985.727 |
| e2e_from_send | 362173 | 23990.513 | 16228.351 | 53608.447 | 64290.815 | 124256.255 | 166985.727 |
| producer_lag | 388950 | 6139.256 | 84.223 | 21348.351 | 22003.711 | 22331.391 | 25378.815 |
| publish_call | 558477 | 179.476 | 86.719 | 187.647 | 456.703 | 22102.015 | 22151.167 |
| queue_wait | 362173 | 23762.303 | 16138.239 | 53411.839 | 63832.063 | 123338.751 | 166854.655 |
| broker_dwell | 362173 | 23616.291 | 15974.399 | 53313.535 | 63733.759 | 123273.215 | 166854.655 |
| receive_call | 487127 | 24.551 | 4.855 | 61.599 | 330.495 | 473.855 | 558.079 |
| ctx_load | 362173 | 105.605 | 2.095 | 11.447 | 49.023 | 15024.127 | 30015.487 |
| process | 362173 | 10.878 | 0.002 | 0.004 | 0.034 | 2635.775 | 3031.039 |
| ctx_commit | 362173 | 111.652 | 20.335 | 196.991 | 437.759 | 16547.839 | 59965.439 |
| ack | 361371 | 0.005 | 0.003 | 0.006 | 0.026 | 0.237 | 28.847 |
| service | 362173 | 228.198 | 26.095 | 216.831 | 1871.871 | 45023.231 | 75038.719 |
| e2e_large | 209 | 9158.94 | 3428.351 | 24936.447 | 40501.247 | 43352.063 | 43352.063 |
| ready_wait | 362173 | 1502.619 | 15.663 | 1739.775 | 43220.991 | 55443.455 | 147455.999 |
| blocked | 362173 | 791.347 | 11.255 | 521.471 | 25214.975 | 29310.975 | 33161.215 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3985.284 | 10977.2 |
| applied_total | 227531.653 | 285789 |
| bench_cpu_cores | 2.136 | 4.703 |
| bench_cpu_pct | 8.899 | 19.6 |
| bench_gc_gen0 | 1.163 | 3 |
| bench_gc_gen1 | 0.429 | 2 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1241.537 | 1609.8 |
| bench_mem_mb | 1966.004 | 2234.3 |
| bench_threadpool_queue | 31.592 | 419 |
| bench_threadpool_threads | 43.469 | 44 |
| broker_backlog | 126373.082 | 145193 |
| ctr_eb_dc1a_cpu_pct | 793.173 | 839.72 |
| ctr_eb_dc1a_mem_mb | 13549.132 | 13799.7 |
| ctr_eb_dc1b_cpu_pct | 48.38 | 67.97 |
| ctr_eb_dc1b_mem_mb | 2852.686 | 2862.9 |
| ctr_eb_dc2a_cpu_pct | 116.383 | 663.09 |
| ctr_eb_dc2a_mem_mb | 2749.927 | 4202.8 |
| ctr_eb_dc2b_cpu_pct | 62.77 | 275.81 |
| ctr_eb_dc2b_mem_mb | 2437.494 | 2864.8 |
| ctr_eb_etcd_dc1_cpu_pct | 1.463 | 1.57 |
| ctr_eb_etcd_dc1_mem_mb | 59.023 | 59.9 |
| ctr_eb_etcd_dc2_cpu_pct | 1.356 | 2.21 |
| ctr_eb_etcd_dc2_mem_mb | 33.029 | 35.3 |
| ctr_eb_etcd_w_cpu_pct | 1.671 | 2.64 |
| ctr_eb_etcd_w_mem_mb | 48.955 | 50.8 |
| ctr_eb_mosquitto_cpu_pct | 25.878 | 54.42 |
| ctr_eb_mosquitto_mem_mb | 6.722 | 18.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.06 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 9.092 | 9.1 |
| ctr_eb_postgres_cpu_pct | 0.769 | 2.02 |
| ctr_eb_postgres_mem_mb | 2027.169 | 2027.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 55.673 | 1739.4 |
| host_cpu_pct | 41.996 | 91.21 |
| in_flight | 138079.347 | 235581 |
| nacks_per_s | 55.673 | 1739.4 |
| octopus_dispatcher_cores | 0.803 | 1.64 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 604341.898 | 725703 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 161036.327 | 196555 |
| octopus_mqtt_sent_total | 148640.49 | 181181 |
| octopus_publish_retries_total | 10048.449 | 20438 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.673 | 19 |
| pg_active_loads | 2.245 | 11 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 67.449 | 74 |
| pg_conn_idle | 200.816 | 254 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.02 | 1 |
| pg_conn_total | 268.265 | 269 |
| pg_db_size_mb | 1500.171 | 1659.7 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 8.763 | 14.18 |
| pg_replica_replay_lag_ms | 106.224 | 148 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22546.596 | 24435.9 |
| pg_tup_del_per_s | 28576.3 | 37378.2 |
| pg_tup_fetch_per_s | 642736.543 | 766690.4 |
| pg_tup_ins_per_s | 46613.965 | 62641.2 |
| pg_tup_upd_per_s | 24875.535 | 27089.4 |
| pg_wal_mb_per_s | 83.247 | 112.62 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7766.89 | 29754.9 |
| produced_total | 365611 | 521370 |
| publish_mb_per_s | 27.446 | 96.31 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 14394/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 3,364 records not Completed |
| record_counts | **FAIL** | 84,298 records found, 87,480 expected; 1606 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 309 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 528,460 events applied by workers, 558,477 generated |
| broker_drained | **FAIL** | 30,017 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 95,458 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 25215 ms, max 33154 ms) |

Sample mismatches:

- source 14667: record 62994285330433 left in status Open (version 4)
- source 11935: record 51260434677761 left in status Open (version 10)
- source 10263: record 44079249358849 left in status Open (version 8)
- source 4776: record 20512763805697 left in status Open (version 8)
- source 4358: record 18717467475975 left in status Open (version 10)
- source 13059: record 56087977918465 left in status Open (version 8)
- source 1541: record 6618544603137 left in status Open (version 9)
- source 8041: record 34535832027137 left in status Open (version 7)
- source 1525: record 6549825126401 left in status Open (version 16)
- source 3148: record 13520557047809 left in status Open (version 3)
- source 14757: record 63380832387073 left in status Open (version 6)
- source 4775: record 20508468838401 left in status Open (version 12)
- source 3150: record 13529146982401 left in status Open (version 5)
- source 15008: record 64458869178369 left in status Open (version 9)
- source 14962: record 64261300682753 left in status Open (version 9)
- source 5682: record 24404004175873 left in status Open (version 7)
- source 14766: record 63419487092737 left in status Open (version 4)
- source 7747: record 33273111642113 left in status Open (version 5)
- source 7698: record 33062658244611 left in status Open (version 4)
- source 14032: record 60266981097473 left in status Open (version 4)

## Cross-source blocking samples

- [cross_source_blocking] source 1977 seq 15: ready 126 ms before start, 126 ms of it with an idle slot in group 57
- [cross_source_blocking] source 4709 seq 3: ready 118 ms before start, 118 ms of it with an idle slot in group 162
- [cross_source_blocking] source 10945 seq 6: ready 107 ms before start, 107 ms of it with an idle slot in group 357
- [cross_source_blocking] source 13976 seq 8: ready 114 ms before start, 114 ms of it with an idle slot in group 444
- [cross_source_blocking] source 6687 seq 8: ready 114 ms before start, 114 ms of it with an idle slot in group 228
- [cross_source_blocking] source 15957 seq 6: ready 101 ms before start, 101 ms of it with an idle slot in group 498
- [cross_source_blocking] source 6091 seq 6: ready 116 ms before start, 116 ms of it with an idle slot in group 209
- [cross_source_blocking] source 13230 seq 9: ready 132 ms before start, 132 ms of it with an idle slot in group 423
- [cross_source_blocking] source 15130 seq 7: ready 129 ms before start, 129 ms of it with an idle slot in group 472
- [cross_source_blocking] source 14631 seq 5: ready 110 ms before start, 110 ms of it with an idle slot in group 463
- [cross_source_blocking] source 8619 seq 13: ready 117 ms before start, 117 ms of it with an idle slot in group 298
- [cross_source_blocking] source 12779 seq 4: ready 116 ms before start, 116 ms of it with an idle slot in group 403
- [cross_source_blocking] source 8131 seq 10: ready 111 ms before start, 111 ms of it with an idle slot in group 275
- [cross_source_blocking] source 6731 seq 3: ready 116 ms before start, 116 ms of it with an idle slot in group 229
- [cross_source_blocking] source 4235 seq 5: ready 116 ms before start, 116 ms of it with an idle slot in group 142
- [cross_source_blocking] source 8376 seq 10: ready 159 ms before start, 159 ms of it with an idle slot in group 284
- [cross_source_blocking] source 12811 seq 4: ready 116 ms before start, 116 ms of it with an idle slot in group 404
- [cross_source_blocking] source 15342 seq 5: ready 134 ms before start, 134 ms of it with an idle slot in group 479
- [cross_source_blocking] source 12352 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 392
- [cross_source_blocking] source 15072 seq 5: ready 109 ms before start, 109 ms of it with an idle slot in group 470

## Error samples

- t-154#0 src=4479 seq=9 op=Create rec=19237158518787 ev=0: NpgsqlException: No suitable host was found.
- t-386#0 src=12101 seq=8 op=Get rec=51973399248897 ev=3: NpgsqlException: No suitable host was found.
- t-450#1 src=14133 seq=21 op=Update rec=60700772794373 ev=2: NpgsqlException: No suitable host was found.
- t-393#0 src=12377 seq=16 op=Create rec=53158810222597 ev=0: NpgsqlException: No suitable host was found.
- t-042#0 src=1327 seq=16 op=Update rec=5699421601794 ev=4: NpgsqlException: No suitable host was found.
- t-450#1 src=14137 seq=20 op=Update rec=60717952663553 ev=11: NpgsqlException: No suitable host was found.
- t-400#0 src=12707 seq=29 op=Update rec=54576149430277 ev=2: NpgsqlException: No suitable host was found.
- t-280#0 src=8260 seq=31 op=Update rec=35476429864964 ev=7: NpgsqlException: No suitable host was found.
- t-214#0 src=6268 seq=14 op=Get rec=26920855011330 ev=2: NpgsqlException: No suitable host was found.
- t-082#0 src=2677 seq=5 op=Create rec=11497627451394 ev=0: NpgsqlException: No suitable host was found.
- t-042#0 src=1345 seq=23 op=Create rec=5776731013124 ev=0: NpgsqlException: No suitable host was found.
- t-142#0 src=4245 seq=14 op=Complete rec=18232136171522 ev=3: NpgsqlException: No suitable host was found.
- t-110#0 src=3292 seq=6 op=Create rec=14139032338436 ev=0: NpgsqlException: No suitable host was found.
- t-182#2 src=5269 seq=22 op=Get rec=22630182682626 ev=7: NpgsqlException: No suitable host was found.
- t-186#0 src=5455 seq=33 op=Create rec=23429046599688 ev=0: NpgsqlException: No suitable host was found.
- t-383#4 src=11933 seq=19 op=Get rec=51251844743170 ev=1: NpgsqlException: No suitable host was found.
- t-209#0 src=6111 seq=16 op=Update rec=26246545145858 ev=4: NpgsqlException: No suitable host was found.
- t-314#1 src=9105 seq=52 op=Get rec=39105677230088 ev=2: NpgsqlException: No suitable host was found.
- t-466#0 src=14864 seq=18 op=Create rec=63840393887748 ev=0: NpgsqlException: No suitable host was found.
- t-013#0 src=409 seq=18 op=Get rec=1756641624066 ev=4: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2019 | 975 | 0 | 49807.359 | 164626.431 | 167772.159 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4060 | 1848 | 0 | 49807.359 | 160432.127 | 165675.007 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 5050 | 3733 | 0 | 51380.223 | 155189.247 | 162529.279 |
| t-045 | 47 | 2 | 82.615 | 64.377 | 1398 | 0 | 2760 | 1889 | 0 | 41418.751 | 150994.943 | 160432.127 |
| t-169 | 33 | 1 | 47.03 | 26.039 | 260 | 0 | 1141 | 1034 | 0 | 36700.159 | 147849.215 | 150994.943 |
| t-184 | 20 | 1 | 79.554 | 25.849 | 161 | 0 | 1112 | 966 | 0 | 33554.431 | 147849.215 | 152043.519 |
| t-111 | 18 | 1 | 79.153 | 23.718 | 3401 | 0 | 1019 | 1006 | 0 | 31457.279 | 145752.063 | 148897.791 |
| t-326 | 10 | 1 | 162.672 | 27.871 | 1837 | 0 | 1172 | 1172 | 0 | 38535.167 | 144703.487 | 146800.639 |
| t-055 | 65 | 3 | 101.454 | 105.002 | 359 | 0 | 4358 | 4358 | 0 | 37486.591 | 143654.911 | 152043.519 |
| t-041 | 34 | 3 | 194.475 | 109.547 | 2732 | 0 | 4490 | 4393 | 0 | 34078.719 | 139460.607 | 149946.367 |
| t-427 | 16 | 1 | 111.157 | 29.505 | 1711 | 0 | 1195 | 949 | 0 | 42205.183 | 139460.607 | 155189.247 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 9369 | 7306 | 0 | 53739.519 | 130023.423 | 158334.975 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 11471 | 9362 | 0 | 46661.631 | 113246.207 | 144703.487 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2076 | 864 | 0 | 49020.927 | 96468.991 | 136314.879 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4081 | 1849 | 0 | 56623.103 | 81264.639 | 143654.911 |
| t-464 | 153 | 3 | 49.802 | 125.294 | 352 | 0 | 5484 | 2845 | 0 | 48496.639 | 79167.487 | 150994.943 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 8262 | 7813 | 0 | 47972.351 | 74448.895 | 148897.791 |
| t-416 | 57 | 1 | 38.884 | 37.248 | 3430 | 0 | 1651 | 941 | 0 | 44826.623 | 73924.607 | 146800.639 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5704 | 3720 | 0 | 50331.647 | 73400.319 | 145752.063 |
| t-046 | 24 | 1 | 84.724 | 34.162 | 3235 | 0 | 1505 | 862 | 0 | 50069.503 | 72351.743 | 131071.999 |
| t-165 | 36 | 1 | 55.043 | 32.844 | 373 | 0 | 1414 | 936 | 0 | 35913.727 | 71827.455 | 115867.647 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2014 | 955 | 0 | 55836.671 | 71827.455 | 131596.287 |
| t-428 | 37 | 1 | 60.011 | 37.101 | 159 | 0 | 1601 | 1029 | 0 | 44040.191 | 69730.303 | 127926.271 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1776 | 1542 | 0 | 49020.927 | 69206.015 | 141557.759 |
| t-152 | 43 | 1 | 37.673 | 26.595 | 882 | 0 | 1180 | 934 | 0 | 35389.439 | 69206.015 | 140509.183 |
