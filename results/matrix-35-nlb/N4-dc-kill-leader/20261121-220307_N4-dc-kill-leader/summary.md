# N4-dc-kill-leader — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.355s, closing 2.447s, drain 11.636s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 453434 | 453434 | 5715.794 | 3667.653 | 19741.9 | 0 | 2069 | 2069 | 274 | 2291 | 104296 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 285269 | 23409.53 | 29949.951 | 47644.671 | 54198.271 | 57540.607 | 61374.463 |
| e2e_from_send | 285269 | 22054.327 | 12632.063 | 47546.367 | 54099.967 | 57278.463 | 60653.567 |
| producer_lag | 285270 | 1355.201 | 11.535 | 481.791 | 26886.143 | 28475.391 | 29130.751 |
| publish_call | 453434 | 215.165 | 83.711 | 190.975 | 465.919 | 28704.767 | 28737.535 |
| queue_wait | 285269 | 21942.08 | 12443.647 | 47513.599 | 54034.431 | 57212.927 | 60653.567 |
| broker_dwell | 285269 | 21710.397 | 11747.327 | 47415.295 | 54001.663 | 57147.391 | 60588.031 |
| receive_call | 403529 | 29.658 | 7.555 | 71.487 | 368.895 | 484.351 | 552.959 |
| ctx_load | 285269 | 10.514 | 6.531 | 24.383 | 60.351 | 86.207 | 164.351 |
| process | 285269 | 10.819 | 0.002 | 0.004 | 0.06 | 2629.631 | 3000.319 |
| ctx_commit | 285269 | 90.845 | 66.495 | 194.559 | 465.407 | 682.495 | 694.783 |
| ack | 285265 | 0.01 | 0.004 | 0.007 | 0.045 | 0.788 | 54.367 |
| service | 285269 | 112.263 | 78.335 | 217.599 | 554.495 | 2732.031 | 3575.807 |
| e2e_large | 173 | 6897.229 | 3264.511 | 29868.031 | 33210.367 | 34308.095 | 34308.095 |
| ready_wait | 285269 | 1621.009 | 56.223 | 1829.887 | 37978.111 | 41877.503 | 50298.879 |
| blocked | 285269 | 982.64 | 32.431 | 842.239 | 22527.999 | 25886.719 | 30769.151 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3667.653 | 11988.5 |
| applied_total | 220693.02 | 267029 |
| bench_cpu_cores | 2.327 | 5.764 |
| bench_cpu_pct | 9.695 | 24.02 |
| bench_gc_gen0 | 1.653 | 6 |
| bench_gc_gen1 | 1 | 4 |
| bench_gc_gen2 | 0.857 | 3 |
| bench_gc_heap_mb | 493.373 | 717.8 |
| bench_mem_mb | 1016.68 | 1336.2 |
| bench_threadpool_queue | 34.061 | 549 |
| bench_threadpool_threads | 43.98 | 44 |
| broker_backlog | 131463.939 | 156160 |
| ctr_eb_dc1a_cpu_pct | 31.144 | 280.75 |
| ctr_eb_dc1a_mem_mb | 2754.916 | 3761.3 |
| ctr_eb_dc1b_cpu_pct | 23.745 | 54.51 |
| ctr_eb_dc1b_mem_mb | 3597.71 | 4112.2 |
| ctr_eb_dc2a_cpu_pct | 662.08 | 817.79 |
| ctr_eb_dc2a_mem_mb | 5423.645 | 5748.7 |
| ctr_eb_dc2b_cpu_pct | 46.351 | 53.73 |
| ctr_eb_dc2b_mem_mb | 2579.736 | 2629.3 |
| ctr_eb_etcd_dc1_cpu_pct | 1.399 | 1.73 |
| ctr_eb_etcd_dc1_mem_mb | 23.384 | 26.7 |
| ctr_eb_etcd_dc2_cpu_pct | 2.05 | 2.38 |
| ctr_eb_etcd_dc2_mem_mb | 32.655 | 33.3 |
| ctr_eb_etcd_w_cpu_pct | 1.929 | 2.76 |
| ctr_eb_etcd_w_mem_mb | 48.145 | 51 |
| ctr_eb_haproxy_cpu_pct | 46.023 | 94.75 |
| ctr_eb_haproxy_mem_mb | 24.027 | 26.5 |
| ctr_eb_mosquitto_cpu_pct | 23.439 | 58.53 |
| ctr_eb_mosquitto_mem_mb | 7.243 | 30.3 |
| ctr_eb_mssql_cpu_pct | 2.205 | 4.91 |
| ctr_eb_mssql_mem_mb | 6723.792 | 6775.9 |
| ctr_eb_mysql_cpu_pct | 0.578 | 0.96 |
| ctr_eb_mysql_mem_mb | 8355.308 | 8482 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 5.3 | 5.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 4.522 | 4.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 4.994 | 5 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 4.8 | 4.8 |
| ctr_eb_postgres_cpu_pct | 0.982 | 2.94 |
| ctr_eb_postgres_mem_mb | 1899.841 | 1915 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 42.196 | 1127.3 |
| host_cpu_pct | 33.186 | 79.11 |
| in_flight | 134616.898 | 156765 |
| nacks_per_s | 42.196 | 1127.3 |
| octopus_dispatcher_cores | 0.583 | 1.36 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 596918.939 | 758814 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 160233.755 | 204290 |
| octopus_mqtt_sent_total | 141875.347 | 185407 |
| octopus_publish_retries_total | 6955.02 | 25190 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.163 | 17 |
| pg_active_loads | 3.204 | 38 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 52.224 | 86 |
| pg_conn_idle | 213.408 | 240 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.286 | 4 |
| pg_conn_total | 265.633 | 266 |
| pg_db_size_mb | 1555.902 | 1748 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 14.294 | 20.83 |
| pg_replica_replay_lag_ms | 159.543 | 238.6 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 18687.71 | 23446 |
| pg_tup_del_per_s | 20163.765 | 33629.6 |
| pg_tup_fetch_per_s | 298388.696 | 382445.6 |
| pg_tup_ins_per_s | 38006.427 | 54529 |
| pg_tup_upd_per_s | 23111.602 | 27936.7 |
| pg_wal_mb_per_s | 86.342 | 119.99 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.918 | 1 |
| produced_per_s | 5715.794 | 19229.7 |
| produced_total | 355309.918 | 418884 |
| publish_mb_per_s | 21.808 | 96.46 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,742 records are Completed |
| record_counts | pass | 74,742 records found, 74,742 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 274 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 453,434 events applied by workers, 453,434 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 104,296 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 22528 ms, max 30763 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12662 seq 8: ready 142 ms before start, 142 ms of it with an idle slot in group 398
- [cross_source_blocking] source 6710 seq 11: ready 132 ms before start, 132 ms of it with an idle slot in group 229
- [cross_source_blocking] source 5550 seq 16: ready 132 ms before start, 132 ms of it with an idle slot in group 190
- [cross_source_blocking] source 1020 seq 12: ready 184 ms before start, 135 ms of it with an idle slot in group 31
- [cross_source_blocking] source 13984 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 444
- [cross_source_blocking] source 5552 seq 13: ready 101 ms before start, 101 ms of it with an idle slot in group 190
- [cross_source_blocking] source 9198 seq 6: ready 100 ms before start, 100 ms of it with an idle slot in group 316
- [cross_source_blocking] source 13916 seq 16: ready 302 ms before start, 223 ms of it with an idle slot in group 441
- [cross_source_blocking] source 2234 seq 23: ready 155 ms before start, 106 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2252 seq 24: ready 183 ms before start, 132 ms of it with an idle slot in group 65
- [cross_source_blocking] source 12732 seq 6: ready 288 ms before start, 157 ms of it with an idle slot in group 402
- [cross_source_blocking] source 14938 seq 14: ready 198 ms before start, 117 ms of it with an idle slot in group 468
- [cross_source_blocking] source 2247 seq 30: ready 113 ms before start, 104 ms of it with an idle slot in group 65
- [cross_source_blocking] source 2455 seq 7: ready 102 ms before start, 102 ms of it with an idle slot in group 72
- [cross_source_blocking] source 11607 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 377
- [cross_source_blocking] source 1975 seq 11: ready 101 ms before start, 101 ms of it with an idle slot in group 57
- [cross_source_blocking] source 6647 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 224
- [cross_source_blocking] source 2935 seq 4: ready 101 ms before start, 101 ms of it with an idle slot in group 94
- [cross_source_blocking] source 4526 seq 6: ready 249 ms before start, 249 ms of it with an idle slot in group 157
- [cross_source_blocking] source 14936 seq 24: ready 193 ms before start, 112 ms of it with an idle slot in group 468

## Error samples

- t-119L#2 src=3470 seq=4 op=Create rec=14903536517122 ev=0: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8930 seq=1 op=Create rec=38354057953281 ev=0: NpgsqlException: Exception while reading from stream
- t-333#0 src=9623 seq=7 op=Update rec=41330470289409 ev=4: NpgsqlException: Exception while reading from stream
- t-312#0 src=9042 seq=15 op=Get rec=38835094290436 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3444 seq=3 op=Create rec=14791867367426 ev=0: NpgsqlException: Exception while reading from stream
- t-045#1 src=1493 seq=30 op=Update rec=6412386172930 ev=13: NpgsqlException: Exception while reading from stream
- t-355#0 src=10917 seq=19 op=Get rec=46888157970434 ev=2: NpgsqlException: Exception while reading from stream
- t-333#0 src=9599 seq=8 op=Update rec=41227391074307 ev=3: NpgsqlException: Exception while reading from stream
- t-174#0 src=5022 seq=21 op=Complete rec=21569325760513 ev=5: NpgsqlException: Exception while reading from stream
- t-361#0 src=11042 seq=9 op=Get rec=47425028882433 ev=2: NpgsqlException: Exception while reading from stream
- t-082#0 src=2684 seq=15 op=Get rec=11527692222466 ev=8: NpgsqlException: Exception while reading from stream
- t-041#2 src=1321 seq=63 op=Complete rec=5673651798023 ev=11: NpgsqlException: Exception while reading from stream
- t-176#0 src=5062 seq=20 op=Complete rec=21741124452356 ev=2: NpgsqlException: Exception while reading from stream
- t-311#0 src=9011 seq=13 op=Create rec=38701950304258 ev=0: NpgsqlException: Exception while reading from stream
- t-265#0 src=7843 seq=9 op=Update rec=33685428502530 ev=3: NpgsqlException: Exception while reading from stream
- t-391#0 src=12324 seq=11 op=Get rec=52931176955907 ev=5: NpgsqlException: Exception while reading from stream
- t-136#0 src=4158 seq=7 op=Update rec=17858474016770 ev=1: NpgsqlException: Exception while reading from stream
- t-219#0 src=6392 seq=17 op=Update rec=27453430956037 ev=1: NpgsqlException: Exception while reading from stream
- t-065#4 src=2273 seq=79 op=Update rec=9762460663820 ev=4: NpgsqlException: Exception while reading from stream
- t-049#0 src=1676 seq=14 op=Update rec=7198365188098 ev=1: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6587 | 6587 | 0 | 41156.607 | 59244.543 | 61603.839 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6719 | 6719 | 0 | 40632.319 | 58195.967 | 60555.263 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1598 | 1598 | 0 | 43253.759 | 57933.823 | 59768.831 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3308 | 3308 | 0 | 40632.319 | 57671.679 | 60817.407 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7536 | 7536 | 0 | 38797.311 | 57409.535 | 60555.263 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3541 | 3541 | 0 | 43515.903 | 56885.247 | 58982.399 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1600 | 1600 | 0 | 40894.463 | 56098.815 | 57933.823 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3365 | 3365 | 0 | 43778.047 | 55836.671 | 56885.247 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3320 | 3320 | 0 | 42991.615 | 55836.671 | 57409.535 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3517 | 3517 | 0 | 43253.759 | 55836.671 | 56623.103 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 4257 | 4257 | 0 | 37224.447 | 55836.671 | 57671.679 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3052 | 3052 | 0 | 40370.175 | 55836.671 | 57147.391 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1527 | 1527 | 0 | 42991.615 | 55574.527 | 56623.103 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1599 | 1599 | 0 | 42729.471 | 55312.383 | 56098.815 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4989 | 4989 | 0 | 42991.615 | 55312.383 | 57671.679 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1703 | 1703 | 0 | 43515.903 | 55312.383 | 55574.527 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1763 | 1763 | 0 | 43515.903 | 55312.383 | 55574.527 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1682 | 1682 | 0 | 42467.327 | 55312.383 | 56098.815 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 2490 | 2490 | 0 | 38273.023 | 55050.239 | 57409.535 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1633 | 1633 | 0 | 42729.471 | 55050.239 | 56098.815 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 2931 | 2931 | 0 | 39583.743 | 55050.239 | 56885.247 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1654 | 1654 | 0 | 44040.191 | 55050.239 | 56623.103 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1624 | 1624 | 0 | 42467.327 | 55050.239 | 55836.671 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 9023 | 9023 | 0 | 39845.887 | 55050.239 | 56885.247 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4681 | 4681 | 0 | 41156.607 | 55050.239 | 56885.247 |
