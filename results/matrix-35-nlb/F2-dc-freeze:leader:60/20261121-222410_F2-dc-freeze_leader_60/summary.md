# F2-dc-freeze:leader:60 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 118.075s, closing 2.73s, drain 7.321s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 447409 | 447409 | 2693.735 | 2492.185 | 17810.4 | 0 | 1750 | 1750 | 268 | 2270 | 116224 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 277825 | 37592.252 | 7512.063 | 98566.143 | 104726.527 | 107282.431 | 109903.871 |
| e2e_from_send | 277825 | 37515.19 | 7467.007 | 98566.143 | 104660.991 | 107282.431 | 109903.871 |
| producer_lag | 279368 | 76.721 | 2.006 | 30.207 | 182.911 | 433.919 | 86441.983 |
| publish_call | 447409 | 431.05 | 67.135 | 141.695 | 349.695 | 84213.759 | 86441.983 |
| queue_wait | 277825 | 37417.24 | 7348.223 | 98500.607 | 104660.991 | 107282.431 | 109641.727 |
| broker_dwell | 277825 | 36798.726 | 7081.983 | 98500.607 | 104595.455 | 107216.895 | 109641.727 |
| receive_call | 403698 | 30.61 | 8.255 | 79.295 | 354.047 | 485.887 | 538.111 |
| ctx_load | 277825 | 11.544 | 7.587 | 25.007 | 59.231 | 140.927 | 223.231 |
| process | 277825 | 10.303 | 0.002 | 0.004 | 0.057 | 2596.863 | 3002.367 |
| ctx_commit | 277825 | 76.01 | 65.919 | 151.679 | 248.959 | 351.999 | 404.479 |
| ack | 277821 | 0.009 | 0.004 | 0.007 | 0.05 | 0.972 | 42.911 |
| service | 277825 | 97.923 | 79.231 | 170.495 | 306.943 | 2705.407 | 3282.943 |
| e2e_large | 168 | 9902.534 | 2867.199 | 4747.263 | 88735.743 | 91095.039 | 91095.039 |
| ready_wait | 277825 | 3210.279 | 69.567 | 1567.743 | 91291.647 | 95092.735 | 99876.863 |
| blocked | 277825 | 1071.672 | 45.311 | 721.407 | 27525.119 | 30113.791 | 84410.367 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2492.185 | 17810.4 |
| applied_total | 290838.85 | 343847 |
| bench_cpu_cores | 1.239 | 5.132 |
| bench_cpu_pct | 5.163 | 21.38 |
| bench_gc_gen0 | 0.59 | 4 |
| bench_gc_gen1 | 0.21 | 2 |
| bench_gc_gen2 | 0.15 | 1 |
| bench_gc_heap_mb | 1164.805 | 1457.2 |
| bench_mem_mb | 1931.964 | 2018.6 |
| bench_threadpool_queue | 42.82 | 3226 |
| bench_threadpool_threads | 49.56 | 66 |
| broker_backlog | 86740.49 | 90577 |
| ctr_eb_dc1a_cpu_pct | 19.444 | 667.18 |
| ctr_eb_dc1a_mem_mb | 2899.267 | 4197.1 |
| ctr_eb_dc1b_cpu_pct | 11.664 | 53.78 |
| ctr_eb_dc1b_mem_mb | 3257.315 | 4666.3 |
| ctr_eb_dc2a_cpu_pct | 45.703 | 56.28 |
| ctr_eb_dc2a_mem_mb | 2226.617 | 2308.8 |
| ctr_eb_dc2b_cpu_pct | 739.103 | 809.03 |
| ctr_eb_dc2b_mem_mb | 7589.313 | 7908.8 |
| ctr_eb_etcd_dc1_cpu_pct | 0.956 | 2.12 |
| ctr_eb_etcd_dc1_mem_mb | 33.038 | 34.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.6 | 2.02 |
| ctr_eb_etcd_dc2_mem_mb | 23.957 | 24.5 |
| ctr_eb_etcd_w_cpu_pct | 1.196 | 3.13 |
| ctr_eb_etcd_w_mem_mb | 56.533 | 58.4 |
| ctr_eb_haproxy_cpu_pct | 23.801 | 106.11 |
| ctr_eb_haproxy_mem_mb | 30.091 | 33.6 |
| ctr_eb_mosquitto_cpu_pct | 13.445 | 58.8 |
| ctr_eb_mosquitto_mem_mb | 3.57 | 18.4 |
| ctr_eb_mssql_cpu_pct | 1.645 | 4.92 |
| ctr_eb_mssql_mem_mb | 4777.132 | 4799.8 |
| ctr_eb_mysql_cpu_pct | 0.434 | 0.95 |
| ctr_eb_mysql_mem_mb | 6023.4 | 6023.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.013 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 3 | 3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 2.4 | 2.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.014 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 3 | 3 |
| ctr_eb_pgbouncer_cpu_pct | 0.013 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 3 | 3 |
| ctr_eb_postgres_cpu_pct | 0.592 | 2.01 |
| ctr_eb_postgres_mem_mb | 1537.158 | 1538.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 17.497 | 1549.8 |
| host_cpu_pct | 18.883 | 79 |
| in_flight | 96520.46 | 102604 |
| nacks_per_s | 17.497 | 1549.8 |
| octopus_dispatcher_cores | 0.636 | 1.39 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 700937.4 | 887226 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 179777.08 | 211227 |
| octopus_mqtt_sent_total | 184626.71 | 230803 |
| octopus_publish_retries_total | 4492.62 | 22506 |
| octopus_publish_stuck | 315.01 | 592 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.07 | 25 |
| pg_active_loads | 14.46 | 36 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 43.02 | 86 |
| pg_conn_idle | 218.15 | 249 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.45 | 11 |
| pg_conn_total | 261.17 | 266 |
| pg_db_size_mb | 1407.29 | 1525.2 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 1.902 | 7.92 |
| pg_replica_replay_lag_ms | 27.293 | 35.3 |
| pg_replicas | 2.94 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21022.397 | 27793.9 |
| pg_tup_del_per_s | 21480.534 | 33754.4 |
| pg_tup_fetch_per_s | 191891.079 | 204350.2 |
| pg_tup_ins_per_s | 33796.112 | 50157.9 |
| pg_tup_upd_per_s | 26359.919 | 32395.4 |
| pg_wal_mb_per_s | 79.901 | 112.22 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.14 | 1 |
| produced_per_s | 2693.735 | 13647.7 |
| produced_total | 387359.31 | 415663 |
| publish_mb_per_s | 10.035 | 69.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,044 records are Completed |
| record_counts | pass | 74,044 records found, 74,044 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 268 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 447,409 events applied by workers, 447,409 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 116,224 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 27525 ms, max 84404 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13605 seq 7: ready 149 ms before start, 149 ms of it with an idle slot in group 431
- [cross_source_blocking] source 5474 seq 8: ready 160 ms before start, 140 ms of it with an idle slot in group 188
- [cross_source_blocking] source 13976 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 444
- [cross_source_blocking] source 8269 seq 3: ready 130 ms before start, 130 ms of it with an idle slot in group 281
- [cross_source_blocking] source 13014 seq 14: ready 145 ms before start, 145 ms of it with an idle slot in group 414
- [cross_source_blocking] source 6398 seq 7: ready 120 ms before start, 120 ms of it with an idle slot in group 219
- [cross_source_blocking] source 1002 seq 6: ready 136 ms before start, 136 ms of it with an idle slot in group 30
- [cross_source_blocking] source 8130 seq 10: ready 145 ms before start, 145 ms of it with an idle slot in group 275
- [cross_source_blocking] source 12723 seq 7: ready 120 ms before start, 120 ms of it with an idle slot in group 401
- [cross_source_blocking] source 353 seq 4: ready 104 ms before start, 104 ms of it with an idle slot in group 10
- [cross_source_blocking] source 4455 seq 6: ready 138 ms before start, 138 ms of it with an idle slot in group 153
- [cross_source_blocking] source 15611 seq 8: ready 137 ms before start, 137 ms of it with an idle slot in group 485
- [cross_source_blocking] source 7279 seq 5: ready 100 ms before start, 100 ms of it with an idle slot in group 249
- [cross_source_blocking] source 9512 seq 14: ready 105 ms before start, 105 ms of it with an idle slot in group 329
- [cross_source_blocking] source 12392 seq 6: ready 105 ms before start, 105 ms of it with an idle slot in group 393
- [cross_source_blocking] source 2857 seq 9: ready 103 ms before start, 103 ms of it with an idle slot in group 91
- [cross_source_blocking] source 5917 seq 11: ready 137 ms before start, 137 ms of it with an idle slot in group 205
- [cross_source_blocking] source 2895 seq 10: ready 148 ms before start, 148 ms of it with an idle slot in group 92
- [cross_source_blocking] source 9206 seq 3: ready 136 ms before start, 136 ms of it with an idle slot in group 316
- [cross_source_blocking] source 8254 seq 8: ready 101 ms before start, 101 ms of it with an idle slot in group 279

## Error samples

- t-453#0 src=14196 seq=30 op=Update rec=60971355734019 ev=4: TimeoutException: commit of src=14196 seq=30 op=Update rec=60971355734019 ev=4 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-222#1 src=6496 seq=18 op=Get rec=27900107554820 ev=1: TimeoutException: commit of src=6496 seq=18 op=Get rec=27900107554820 ev=1 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-282#0 src=8286 seq=34 op=Update rec=35588099014662 ev=5: TimeoutException: commit of src=8286 seq=34 op=Update rec=35588099014662 ev=5 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-341#0 src=10002 seq=8 op=Update rec=42958262894594 ev=2: TimeoutException: commit of src=10002 seq=8 op=Update rec=42958262894594 ev=2 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-029#1 src=969 seq=53 op=Update rec=4161823309830 ev=8: TimeoutException: commit of src=969 seq=53 op=Update rec=4161823309830 ev=8 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-235#1 src=6926 seq=27 op=Create rec=29746943492102 ev=0: TimeoutException: commit of src=6926 seq=27 op=Create rec=29746943492102 ev=0 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-147#0 src=4347 seq=40 op=Get rec=18670222835716 ev=4: TimeoutException: commit of src=4347 seq=40 op=Get rec=18670222835716 ev=4 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-322#0 src=9323 seq=25 op=Get rec=40041980100611 ev=8: TimeoutException: commit of src=9323 seq=25 op=Get rec=40041980100611 ev=8 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-259#0 src=7484 seq=9 op=Complete rec=32143535243265 ev=4: TimeoutException: commit of src=7484 seq=9 op=Complete rec=32143535243265 ev=4 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-314#0 src=9095 seq=69 op=Update rec=39062727557128 ev=6: TimeoutException: commit of src=9095 seq=69 op=Update rec=39062727557128 ev=6 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-051#0 src=1802 seq=29 op=Get rec=7739531067396 ev=3: TimeoutException: commit of src=1802 seq=29 op=Get rec=7739531067396 ev=3 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-250#0 src=7281 seq=11 op=Update rec=31271656882178 ev=1: TimeoutException: commit of src=7281 seq=11 op=Update rec=31271656882178 ev=1 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-461#1 src=14502 seq=13 op=Update rec=62285615726594 ev=3: TimeoutException: commit of src=14502 seq=13 op=Update rec=62285615726594 ev=3 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-282#0 src=8289 seq=37 op=Get rec=35600983916547 ev=2: TimeoutException: commit of src=8289 seq=37 op=Get rec=35600983916547 ev=2 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-013#0 src=409 seq=20 op=Get rec=1756641624065 ev=7: TimeoutException: commit of src=409 seq=20 op=Get rec=1756641624065 ev=7 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-334#0 src=9652 seq=41 op=Update rec=41455024340999 ev=1: TimeoutException: commit of src=9652 seq=41 op=Update rec=41455024340999 ev=1 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-090#0 src=2850 seq=19 op=Complete rec=12240656793604 ev=1: TimeoutException: commit of src=2850 seq=19 op=Complete rec=12240656793604 ev=1 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-235#0 src=6894 seq=30 op=Update rec=29609504538629 ev=1: TimeoutException: commit of src=6894 seq=30 op=Update rec=29609504538629 ev=1 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-189#0 src=5536 seq=14 op=Complete rec=23776938950657 ev=9: TimeoutException: commit of src=5536 seq=14 op=Complete rec=23776938950657 ev=9 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt
- t-158#0 src=4538 seq=18 op=Update rec=19490561589251 ev=8: TimeoutException: commit of src=4538 seq=18 op=Update rec=19490561589251 ev=8 not finished after 60000 ms: abandoned (not cancelled), outcome in doubt

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1570 | 1570 | 0 | 97517.567 | 108527.615 | 110100.479 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1639 | 1639 | 0 | 96468.991 | 108527.615 | 109051.903 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1615 | 1615 | 0 | 95420.415 | 108003.327 | 108003.327 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3481 | 3481 | 0 | 96468.991 | 108003.327 | 108527.615 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6494 | 6494 | 0 | 96468.991 | 107479.039 | 109576.191 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1669 | 1669 | 0 | 95420.415 | 107479.039 | 108003.327 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3488 | 3488 | 0 | 95420.415 | 107479.039 | 108003.327 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1607 | 1607 | 0 | 95420.415 | 106954.751 | 108527.615 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3341 | 3341 | 0 | 95944.703 | 106954.751 | 109051.903 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4924 | 4924 | 0 | 95420.415 | 106954.751 | 108527.615 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3277 | 3277 | 0 | 95944.703 | 106954.751 | 109051.903 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3288 | 3288 | 0 | 95420.415 | 106954.751 | 106954.751 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1727 | 1727 | 0 | 95420.415 | 106954.751 | 107479.039 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1432 | 1432 | 0 | 92798.975 | 106430.463 | 106954.751 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1608 | 1608 | 0 | 94371.839 | 106430.463 | 106954.751 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1505 | 1505 | 0 | 94371.839 | 105906.175 | 106430.463 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4790 | 4790 | 0 | 94371.839 | 105906.175 | 106430.463 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6648 | 6648 | 0 | 94896.127 | 105906.175 | 106954.751 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1589 | 1589 | 0 | 93323.263 | 105381.887 | 105906.175 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3113 | 3113 | 0 | 93847.551 | 105381.887 | 106430.463 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4518 | 4518 | 0 | 93847.551 | 105381.887 | 106430.463 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1652 | 1652 | 0 | 93847.551 | 105381.887 | 105906.175 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 1547 | 1547 | 0 | 93323.263 | 104857.599 | 106430.463 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1416 | 1416 | 0 | 92798.975 | 104857.599 | 105381.887 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 4818 | 4818 | 0 | 93847.551 | 104857.599 | 108527.615 |
