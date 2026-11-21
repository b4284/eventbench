# T2-patroni-freeze-leader-30s — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 118.174s, closing 1.965s, drain 21.159s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 440590 | 440590 | 4095.692 | 4360.462 | 16967.7 | 122 | 18 | 18 | 266 | 2224 | 107615 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 272676 | 7273.739 | 326.399 | 6791.167 | 102891.519 | 109969.407 | 113180.671 |
| e2e_from_send | 272676 | 7165.377 | 286.719 | 6660.095 | 102891.519 | 109903.871 | 112984.063 |
| producer_lag | 272676 | 108.363 | 2.213 | 85.951 | 836.095 | 1224.703 | 86704.127 |
| publish_call | 440590 | 429.968 | 50.463 | 132.991 | 416.767 | 86638.591 | 86704.127 |
| queue_wait | 272676 | 6893.993 | 218.879 | 6377.471 | 102825.983 | 109838.335 | 112984.063 |
| broker_dwell | 272676 | 6523.79 | 162.047 | 6213.631 | 102825.983 | 109772.799 | 112918.527 |
| receive_call | 377043 | 37.461 | 11.319 | 102.399 | 361.471 | 479.487 | 581.631 |
| ctx_load | 272676 | 9.116 | 5.875 | 18.991 | 43.967 | 102.399 | 51511.295 |
| process | 272676 | 10.81 | 0.002 | 0.004 | 0.051 | 2627.583 | 3002.367 |
| ctx_commit | 272676 | 251.385 | 42.431 | 77.759 | 131.583 | 85524.479 | 85590.015 |
| ack | 272178 | 0.008 | 0.003 | 0.005 | 0.032 | 0.695 | 37.087 |
| service | 272676 | 271.382 | 51.231 | 91.199 | 199.679 | 85590.015 | 88145.919 |
| e2e_large | 166 | 14455.114 | 3280.895 | 89915.391 | 93323.263 | 94371.839 | 94371.839 |
| ready_wait | 272676 | 884.911 | 86.975 | 671.743 | 2682.879 | 91226.111 | 94699.519 |
| blocked | 272676 | 435.406 | 61.791 | 435.455 | 1611.775 | 39354.367 | 88211.455 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 4360.462 | 16967.7 |
| applied_total | 347428.391 | 389526 |
| bench_cpu_cores | 1.623 | 5.277 |
| bench_cpu_pct | 6.765 | 21.99 |
| bench_gc_gen0 | 1.031 | 5 |
| bench_gc_gen1 | 0.922 | 4 |
| bench_gc_gen2 | 0.188 | 2 |
| bench_gc_heap_mb | 989.478 | 1309.3 |
| bench_mem_mb | 1543.584 | 1650.8 |
| bench_threadpool_queue | 231.688 | 12901 |
| bench_threadpool_threads | 69.859 | 103 |
| broker_backlog | 23576.484 | 32789 |
| ctr_eb_etcd_cpu_pct | 0.602 | 1 |
| ctr_eb_etcd_mem_mb | 28.227 | 29 |
| ctr_eb_mosquitto_cpu_pct | 22.648 | 64.34 |
| ctr_eb_mosquitto_mem_mb | 5.85 | 10.1 |
| ctr_eb_pat1_cpu_pct | 447.965 | 1098.06 |
| ctr_eb_pat1_mem_mb | 10050.447 | 13818.7 |
| ctr_eb_pat2_cpu_pct | 34.63 | 67.61 |
| ctr_eb_pat2_mem_mb | 5791.788 | 11863.7 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 9.442 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 9.731 | 9.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 10.931 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.638 | 11.9 |
| ctr_eb_postgres_cpu_pct | 1.246 | 2.27 |
| ctr_eb_postgres_mem_mb | 2414.689 | 2417.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0.869 | 40.2 |
| host_cpu_pct | 31.478 | 94.01 |
| in_flight | 20354.344 | 31545 |
| nacks_per_s | 0.869 | 40.2 |
| octopus_dispatcher_cores | 0.697 | 1.75 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 760938.281 | 901335 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 148895.266 | 167868 |
| octopus_mqtt_sent_total | 214108.188 | 248492 |
| octopus_publish_retries_total | 3842.281 | 12852 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 12.938 | 28 |
| pg_active_loads | 5.125 | 27 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 51.844 | 72 |
| pg_conn_idle | 209.078 | 262 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.969 | 13 |
| pg_conn_total | 260.922 | 271 |
| pg_db_size_mb | 1515.327 | 1648 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 1.151 | 6.78 |
| pg_replica_replay_lag_ms | 7.417 | 56.8 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | -0.019 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | -8856.095 | 36289.5 |
| pg_tup_del_per_s | -7569.325 | 43697.3 |
| pg_tup_fetch_per_s | -424019.375 | 631422 |
| pg_tup_ins_per_s | -9778.139 | 58081.6 |
| pg_tup_upd_per_s | -8929.748 | 40931.5 |
| pg_wal_mb_per_s | -24.856 | 112.07 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 4095.692 | 14494.7 |
| produced_total | 367782.734 | 407778 |
| publish_mb_per_s | 15.421 | 76.9 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 73,179 records are Completed |
| record_counts | pass | 73,179 records found, 73,179 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 266 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 440,590 events applied by workers, 440,590 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 107,615 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1612 ms, max 88180 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 3546 seq 18: ready 161 ms before start, 115 ms of it with an idle slot in group 121
- [cross_source_blocking] source 12732 seq 6: ready 110 ms before start, 110 ms of it with an idle slot in group 402
- [cross_source_blocking] source 12947 seq 14: ready 102 ms before start, 102 ms of it with an idle slot in group 409
- [cross_source_blocking] source 398 seq 8: ready 155 ms before start, 109 ms of it with an idle slot in group 11
- [cross_source_blocking] source 14129 seq 12: ready 209 ms before start, 150 ms of it with an idle slot in group 450
- [cross_source_blocking] source 5441 seq 20: ready 133 ms before start, 102 ms of it with an idle slot in group 186
- [cross_source_blocking] source 2669 seq 2: ready 161 ms before start, 161 ms of it with an idle slot in group 82
- [cross_source_blocking] source 6710 seq 11: ready 152 ms before start, 152 ms of it with an idle slot in group 229
- [cross_source_blocking] source 11300 seq 4: ready 184 ms before start, 184 ms of it with an idle slot in group 368
- [cross_source_blocking] source 7979 seq 14: ready 192 ms before start, 109 ms of it with an idle slot in group 269
- [cross_source_blocking] source 15660 seq 8: ready 182 ms before start, 182 ms of it with an idle slot in group 488
- [cross_source_blocking] source 1957 seq 15: ready 228 ms before start, 162 ms of it with an idle slot in group 55
- [cross_source_blocking] source 2313 seq 11: ready 231 ms before start, 187 ms of it with an idle slot in group 66
- [cross_source_blocking] source 4598 seq 11: ready 152 ms before start, 106 ms of it with an idle slot in group 160
- [cross_source_blocking] source 1515 seq 15: ready 185 ms before start, 146 ms of it with an idle slot in group 45
- [cross_source_blocking] source 4996 seq 22: ready 184 ms before start, 184 ms of it with an idle slot in group 172
- [cross_source_blocking] source 1530 seq 17: ready 182 ms before start, 143 ms of it with an idle slot in group 45
- [cross_source_blocking] source 9476 seq 6: ready 169 ms before start, 169 ms of it with an idle slot in group 328
- [cross_source_blocking] source 5472 seq 8: ready 175 ms before start, 175 ms of it with an idle slot in group 188
- [cross_source_blocking] source 14200 seq 25: ready 182 ms before start, 182 ms of it with an idle slot in group 454
- [cross_source_blocking] source 9240 seq 9: ready 182 ms before start, 182 ms of it with an idle slot in group 317

## Error samples

- t-119L#0 src=3470 seq=4 op=Create rec=14903536517122 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3485 seq=3 op=Update rec=14967961026561 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3439 seq=5 op=Update rec=14770392530946 ev=1: NpgsqlException: Exception while reading from stream
- t-319L#1 src=9259 seq=4 op=Update rec=39767102193665 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3463 seq=7 op=Update rec=14873471746051 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3482 seq=5 op=Update rec=14955076124673 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3444 seq=3 op=Create rec=14791867367426 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3460 seq=4 op=Update rec=14860586844161 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3474 seq=3 op=Update rec=14920716386305 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3458 seq=2 op=Update rec=14851996909569 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3455 seq=3 op=Update rec=14839112007681 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3436 seq=4 op=Update rec=14757507629057 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3429 seq=4 op=Update rec=14727442857985 ev=3: NpgsqlException: Exception while reading from stream
- t-319L#0 src=9283 seq=3 op=Update rec=39870181408769 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3432 seq=4 op=Update rec=14740327759873 ev=3: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8930 seq=1 op=Create rec=38354057953281 ev=0: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3464 seq=4 op=Update rec=14877766713345 ev=2: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6401 | 6401 | 2 | 802.815 | 111673.343 | 112721.919 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3230 | 3230 | 2 | 3063.807 | 111149.055 | 113246.207 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3439 | 3439 | 0 | 2359.295 | 109576.191 | 112197.631 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 4263 | 4263 | 4 | 499.711 | 109576.191 | 113246.207 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3229 | 3229 | 1 | 1613.823 | 109576.191 | 110624.767 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1546 | 1546 | 1 | 5963.775 | 109051.903 | 111149.055 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 4860 | 4860 | 2 | 2719.743 | 109051.903 | 111149.055 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3070 | 3070 | 2 | 999.423 | 109051.903 | 112197.631 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4526 | 4526 | 2 | 1220.607 | 109051.903 | 111673.343 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 2968 | 2968 | 2 | 450.559 | 109051.903 | 110100.479 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1583 | 1583 | 3 | 2883.583 | 108527.615 | 110624.767 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1393 | 1393 | 2 | 1695.743 | 108003.327 | 110100.479 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1645 | 1645 | 0 | 1638.399 | 108003.327 | 110100.479 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4711 | 4711 | 4 | 3227.647 | 108003.327 | 110624.767 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3290 | 3290 | 1 | 4095.999 | 107479.039 | 111149.055 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1573 | 1573 | 3 | 3620.863 | 107479.039 | 110624.767 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 5627 | 5627 | 3 | 505.855 | 106954.751 | 110100.479 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7302 | 7302 | 7 | 589.823 | 106430.463 | 110624.767 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 2840 | 2840 | 4 | 794.623 | 106430.463 | 110624.767 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4439 | 4439 | 4 | 1130.495 | 106430.463 | 110100.479 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1632 | 1632 | 0 | 913.407 | 106430.463 | 109576.191 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1559 | 1559 | 0 | 1228.799 | 106430.463 | 109576.191 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 5642 | 5642 | 2 | 464.895 | 105906.175 | 111673.343 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 4127 | 4127 | 6 | 1228.799 | 105381.887 | 110100.479 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1485 | 1485 | 4 | 7241.727 | 104333.311 | 109051.903 |
