# W10-lazy-cursor-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.165s, closing 1.627s, drain 31.451s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1487375 | 1487375 | 25054.34 | 24078.348 | 27353.9 | 0 | 0 | 0 | 425 | 7514 | 332652 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1201950 | 2170.845 | 1673.215 | 3385.343 | 19316.735 | 33652.735 | 38109.183 |
| e2e_from_send | 1201950 | 1078.252 | 155.263 | 2252.799 | 18677.759 | 33193.983 | 38010.879 |
| producer_lag | 1201950 | 1092.591 | 1159.167 | 2240.511 | 2918.399 | 3127.295 | 3424.255 |
| publish_call | 1487375 | 67.101 | 58.655 | 121.599 | 213.119 | 314.367 | 474.367 |
| queue_wait | 1201950 | 1039.122 | 120.511 | 2142.207 | 18644.991 | 33177.599 | 38010.879 |
| broker_dwell | 1201950 | 988.299 | 50.687 | 2094.079 | 18595.839 | 33161.215 | 38010.879 |
| receive_call | 1381170 | 21.829 | 0.018 | 62.047 | 275.455 | 457.215 | 534.527 |
| ctx_load | 1201950 | 8.483 | 6.299 | 17.471 | 39.103 | 55.583 | 90.303 |
| process | 1201950 | 10.404 | 0.001 | 0.002 | 0.024 | 2615.295 | 3014.655 |
| ctx_commit | 1201950 | 20.196 | 16.799 | 40.031 | 70.335 | 97.599 | 225.919 |
| ack | 1201289 | 0.009 | 0.003 | 0.007 | 0.038 | 0.765 | 71.359 |
| service | 1201950 | 39.132 | 25.311 | 53.823 | 95.487 | 2643.967 | 3160.063 |
| e2e_large | 324 | 3816.22 | 3803.135 | 5447.679 | 7319.551 | 9265.151 | 9265.151 |
| ready_wait | 1201950 | 107.84 | 39.103 | 280.319 | 921.087 | 1448.959 | 2668.543 |
| blocked | 1201950 | 105.045 | 35.903 | 277.247 | 920.063 | 1438.719 | 2668.543 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 24078.348 | 27353.9 |
| applied_total | 810133.083 | 1374405 |
| bench_cpu_cores | 4.937 | 6.258 |
| bench_cpu_pct | 20.569 | 26.08 |
| bench_gc_gen0 | 4.104 | 6 |
| bench_gc_gen1 | 4.062 | 6 |
| bench_gc_gen2 | 0.75 | 2 |
| bench_gc_heap_mb | 1026.379 | 1282.5 |
| bench_mem_mb | 1503.46 | 1635.2 |
| bench_threadpool_queue | 167.062 | 789 |
| bench_threadpool_threads | 42.917 | 44 |
| broker_backlog | 24607.042 | 49845 |
| ctr_eb_mosquitto_cpu_pct | 66.095 | 68.94 |
| ctr_eb_mosquitto_mem_mb | 9.246 | 20.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 25.215 | 27.63 |
| ctr_eb_pgbouncer_2_mem_mb | 15.735 | 16.2 |
| ctr_eb_pgbouncer_3_cpu_pct | 24.78 | 26.78 |
| ctr_eb_pgbouncer_3_mem_mb | 15.894 | 16.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 26.654 | 28.36 |
| ctr_eb_pgbouncer_4_mem_mb | 15.952 | 16.7 |
| ctr_eb_pgbouncer_cpu_pct | 25.68 | 27.87 |
| ctr_eb_pgbouncer_mem_mb | 16.188 | 17.1 |
| ctr_eb_postgres_cpu_pct | 949.411 | 1028.24 |
| ctr_eb_postgres_mem_mb | 4803.819 | 5176.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.178 | 82.79 |
| in_flight | 19132.167 | 47546 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.585 | 1.65 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1598710.417 | 2823771 |
| octopus_mqtt_received_total | 103755.979 | 181475 |
| octopus_mqtt_sent_total | 345805.271 | 606857 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 8.083 | 16 |
| pg_active_loads | 8.583 | 40 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.99 | 100 |
| pg_conn_active | 24.479 | 36 |
| pg_conn_idle | 211.479 | 227 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.708 | 7 |
| pg_conn_total | 235.958 | 237 |
| pg_db_size_mb | 3744.367 | 4261.5 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 26872.267 | 29152 |
| pg_tup_del_per_s | 57585.169 | 67522 |
| pg_tup_fetch_per_s | 446904.54 | 591047.7 |
| pg_tup_ins_per_s | 71226.89 | 78092.4 |
| pg_tup_upd_per_s | 59699.502 | 63403.2 |
| pg_wal_mb_per_s | 140.928 | 246.76 |
| pgb_cl_active | 253.958 | 265 |
| pgb_cl_waiting | 1.062 | 17 |
| pgb_maxwait_ms | 0.107 | 1.71 |
| pgb_sv_active | 12.896 | 48 |
| pgb_sv_idle | 35.104 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 25054.34 | 28866 |
| produced_total | 829265.25 | 1421951 |
| publish_mb_per_s | 66.829 | 102.02 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,896 records are Completed |
| record_counts | pass | 193,896 records found, 193,896 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 425 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,487,375 events applied by workers, 1,487,375 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 332,652 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 920 ms, max 2668 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7260 seq 9: ready 109 ms before start, 109 ms of it with an idle slot in group 248
- [cross_source_blocking] source 9052 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 313
- [cross_source_blocking] source 1020 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 31
- [cross_source_blocking] source 10684 seq 17: ready 109 ms before start, 109 ms of it with an idle slot in group 350
- [cross_source_blocking] source 988 seq 45: ready 103 ms before start, 103 ms of it with an idle slot in group 29
- [cross_source_blocking] source 4341 seq 58: ready 132 ms before start, 132 ms of it with an idle slot in group 147
- [cross_source_blocking] source 12892 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 408
- [cross_source_blocking] source 4476 seq 9: ready 103 ms before start, 103 ms of it with an idle slot in group 154
- [cross_source_blocking] source 5916 seq 17: ready 103 ms before start, 103 ms of it with an idle slot in group 205
- [cross_source_blocking] source 4875 seq 18: ready 151 ms before start, 142 ms of it with an idle slot in group 169
- [cross_source_blocking] source 14923 seq 43: ready 177 ms before start, 177 ms of it with an idle slot in group 468
- [cross_source_blocking] source 14603 seq 13: ready 177 ms before start, 177 ms of it with an idle slot in group 461
- [cross_source_blocking] source 7307 seq 10: ready 177 ms before start, 177 ms of it with an idle slot in group 250
- [cross_source_blocking] source 6443 seq 7: ready 177 ms before start, 177 ms of it with an idle slot in group 221
- [cross_source_blocking] source 15691 seq 23: ready 177 ms before start, 177 ms of it with an idle slot in group 490
- [cross_source_blocking] source 5355 seq 4: ready 177 ms before start, 177 ms of it with an idle slot in group 183
- [cross_source_blocking] source 6891 seq 21: ready 177 ms before start, 177 ms of it with an idle slot in group 234
- [cross_source_blocking] source 11865 seq 30: ready 103 ms before start, 103 ms of it with an idle slot in group 383
- [cross_source_blocking] source 6748 seq 37: ready 155 ms before start, 155 ms of it with an idle slot in group 230
- [cross_source_blocking] source 14236 seq 42: ready 155 ms before start, 155 ms of it with an idle slot in group 456

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25414 | 25414 | 0 | 17432.575 | 36438.015 | 38273.023 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22481 | 22481 | 0 | 8912.895 | 26476.543 | 31326.207 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17982 | 17982 | 0 | 3604.479 | 18350.079 | 23855.103 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30756 | 30756 | 0 | 2785.279 | 15597.567 | 21757.951 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 10880 | 10880 | 0 | 2916.351 | 15204.351 | 21102.591 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 12405 | 12405 | 0 | 4980.735 | 14614.527 | 17563.647 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 19027 | 19027 | 0 | 2752.511 | 14155.775 | 20971.519 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 22210 | 22210 | 0 | 2785.279 | 13565.951 | 16908.287 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29456 | 29456 | 0 | 2768.895 | 13303.807 | 19922.943 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 19487 | 19487 | 0 | 2867.199 | 12845.055 | 17694.719 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 7772 | 7772 | 0 | 2883.583 | 11993.087 | 14745.599 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 14630 | 14630 | 0 | 2293.759 | 10616.831 | 16777.215 |
| t-315 | 88 | 4 | 117.565 | 172.184 | 2927 | 0 | 10536 | 10536 | 0 | 2408.447 | 9961.471 | 14876.671 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 13827 | 13827 | 0 | 2408.447 | 9699.327 | 13762.559 |
| t-121 | 76 | 5 | 182.916 | 224.389 | 149 | 0 | 13754 | 13754 | 0 | 2162.687 | 9502.719 | 13762.559 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 9826 | 9826 | 0 | 2195.455 | 8781.823 | 10944.511 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9129 | 9129 | 0 | 2408.447 | 8585.215 | 11796.479 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9092 | 9092 | 0 | 2195.455 | 7962.623 | 10354.687 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 85 | 85 | 0 | 3637.247 | 7700.479 | 7700.479 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15092 | 15092 | 0 | 2015.231 | 7503.871 | 14286.847 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 379 | 379 | 0 | 3227.647 | 7340.031 | 9306.111 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 8521 | 8521 | 0 | 2277.375 | 7274.495 | 8716.287 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 95 | 95 | 0 | 2883.583 | 6029.311 | 6520.831 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 158 | 158 | 0 | 3178.495 | 6029.311 | 6848.511 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15225 | 15225 | 0 | 1933.311 | 5996.543 | 9502.719 |
