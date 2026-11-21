# E24-332-s0 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 89.141s, closing 1.044s, drain 36.01s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 589313 | 589313 | 10117.759 | 8710.647 | 19231.1 | 0 | 0 | 0 | 0 | 2814 | 287119 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 476508 | 5523.293 | 4595.711 | 12328.959 | 19775.487 | 24346.623 | 34340.863 |
| e2e_from_send | 476508 | 4207.577 | 1928.191 | 11722.751 | 19185.663 | 23511.039 | 34340.863 |
| producer_lag | 476508 | 1315.722 | 268.287 | 4182.015 | 5447.679 | 5918.719 | 6492.159 |
| publish_call | 589313 | 167.873 | 115.135 | 365.055 | 784.895 | 1704.959 | 2934.783 |
| queue_wait | 476508 | 4130.67 | 1826.815 | 11657.215 | 19136.511 | 23461.887 | 34340.863 |
| broker_dwell | 476508 | 4005.318 | 1698.815 | 11526.143 | 19054.591 | 23363.583 | 34308.095 |
| receive_call | 517696 | 25.155 | 11.247 | 61.855 | 231.551 | 467.711 | 883.199 |
| ctx_load | 476508 | 16.805 | 11.679 | 35.551 | 88.319 | 138.239 | 481.791 |
| process | 476508 | 9.939 | 0.002 | 0.004 | 0.075 | 2619.391 | 3110.911 |
| ctx_commit | 476508 | 49.832 | 38.335 | 105.279 | 208.767 | 468.991 | 630.783 |
| ack | 476462 | 0.019 | 0.004 | 0.008 | 0.068 | 2.937 | 441.087 |
| service | 476508 | 76.903 | 56.799 | 131.455 | 273.407 | 2680.831 | 3176.447 |
| ready_wait | 476508 | 286.314 | 189.567 | 672.767 | 1427.455 | 2052.095 | 11452.415 |
| blocked | 476508 | 211.002 | 147.711 | 483.327 | 976.895 | 1400.831 | 10420.223 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8710.647 | 10988.4 |
| applied_total | 296315.571 | 504098 |
| bench_cpu_cores | 3.148 | 4.09 |
| bench_cpu_pct | 13.117 | 17.04 |
| bench_gc_gen0 | 1.306 | 3 |
| bench_gc_gen1 | 0.388 | 1 |
| bench_gc_gen2 | 0.204 | 1 |
| bench_gc_heap_mb | 455.169 | 760.3 |
| bench_mem_mb | 837.924 | 1041.6 |
| bench_threadpool_queue | 147.694 | 1232 |
| bench_threadpool_threads | 39.98 | 45 |
| broker_backlog | 48295.265 | 91705 |
| ctr_eb_emqx_cpu_pct | 269.478 | 288.19 |
| ctr_eb_emqx_mem_mb | 597.092 | 621.2 |
| ctr_eb_mssql_cpu_pct | 4.229 | 5.07 |
| ctr_eb_mssql_mem_mb | 7447.2 | 7447.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.241 | 33.61 |
| ctr_eb_pgbouncer_2_mem_mb | 21.467 | 22.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 31.362 | 34.11 |
| ctr_eb_pgbouncer_3_mem_mb | 21.439 | 22.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 30.111 | 33.55 |
| ctr_eb_pgbouncer_4_mem_mb | 21.553 | 22 |
| ctr_eb_pgbouncer_cpu_pct | 30.399 | 32.71 |
| ctr_eb_pgbouncer_mem_mb | 21.761 | 22.8 |
| ctr_eb_postgres_cpu_pct | 985.913 | 1054.4 |
| ctr_eb_postgres_mem_mb | 6067.029 | 6425.9 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 84.405 | 86.45 |
| in_flight | 35037.408 | 68830 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.101 | 2.18 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 620451.653 | 1096552 |
| octopus_missing_reports_total | 43872.612 | 73204 |
| octopus_mqtt_received_total | 153612.469 | 267881 |
| octopus_mqtt_sent_total | 228944.735 | 392368 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 18.694 | 33 |
| pg_active_loads | 12.592 | 24 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.961 | 100 |
| pg_conn_active | 64.857 | 106 |
| pg_conn_idle | 199.816 | 230 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 6.122 | 37 |
| pg_conn_total | 264.673 | 265 |
| pg_db_size_mb | 3713.71 | 4383.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 22564.169 | 25415.1 |
| pg_tup_del_per_s | 30798.712 | 34463 |
| pg_tup_fetch_per_s | 280415.922 | 335630.7 |
| pg_tup_ins_per_s | 43328.753 | 48525.5 |
| pg_tup_upd_per_s | 46978.739 | 52787.3 |
| pg_wal_mb_per_s | 126.514 | 230.12 |
| pgb_cl_active | 622.122 | 653 |
| pgb_cl_waiting | 8.653 | 41 |
| pgb_maxwait_ms | 0.458 | 2.52 |
| pgb_sv_active | 31.041 | 48 |
| pgb_sv_idle | 16.959 | 46 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 10117.759 | 18272.9 |
| produced_total | 331352.98 | 572928 |
| publish_mb_per_s | 15.945 | 28.78 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5980/5980 source cursors at the expected last sequence |
| records_complete | pass | all 167,902 records are Completed |
| record_counts | pass | 167,902 records found, 76,037 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 405 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 589,313 events applied by workers, 589,313 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 287,119 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 977 ms, max 10416 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13267 seq 20: ready 394 ms before start, 393 ms of it with an idle slot in group 425
- [cross_source_blocking] source 4154 seq 8: ready 119 ms before start, 119 ms of it with an idle slot in group 136
- [cross_source_blocking] source 9072 seq 10: ready 120 ms before start, 120 ms of it with an idle slot in group 313
- [cross_source_blocking] source 10328 seq 12: ready 133 ms before start, 133 ms of it with an idle slot in group 346
- [cross_source_blocking] source 8292 seq 39: ready 210 ms before start, 210 ms of it with an idle slot in group 282
- [cross_source_blocking] source 2818 seq 15: ready 226 ms before start, 217 ms of it with an idle slot in group 88
- [cross_source_blocking] source 13288 seq 30: ready 214 ms before start, 196 ms of it with an idle slot in group 425
- [cross_source_blocking] source 5119 seq 6: ready 210 ms before start, 182 ms of it with an idle slot in group 178
- [cross_source_blocking] source 14077 seq 12: ready 225 ms before start, 225 ms of it with an idle slot in group 449
- [cross_source_blocking] source 7281 seq 9: ready 174 ms before start, 174 ms of it with an idle slot in group 250
- [cross_source_blocking] source 11668 seq 32: ready 177 ms before start, 151 ms of it with an idle slot in group 378
- [cross_source_blocking] source 11636 seq 30: ready 178 ms before start, 151 ms of it with an idle slot in group 378
- [cross_source_blocking] source 3796 seq 27: ready 183 ms before start, 183 ms of it with an idle slot in group 128
- [cross_source_blocking] source 9308 seq 27: ready 133 ms before start, 133 ms of it with an idle slot in group 321
- [cross_source_blocking] source 1297 seq 75: ready 120 ms before start, 120 ms of it with an idle slot in group 41
- [cross_source_blocking] source 11028 seq 24: ready 172 ms before start, 172 ms of it with an idle slot in group 360
- [cross_source_blocking] source 8854 seq 19: ready 177 ms before start, 177 ms of it with an idle slot in group 306
- [cross_source_blocking] source 2223 seq 46: ready 107 ms before start, 107 ms of it with an idle slot in group 65
- [cross_source_blocking] source 4880 seq 19: ready 178 ms before start, 143 ms of it with an idle slot in group 169
- [cross_source_blocking] source 10261 seq 27: ready 178 ms before start, 175 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-040 | 119 | 2 | 48.767 | 98.209 | 1254 | 0 | 6225 | 6225 | 0 | 8585.215 | 24641.535 | 29097.983 |
| t-314 | 24 | 3 | 348.915 | 139.029 | 525 | 0 | 8388 | 8388 | 0 | 7634.943 | 24641.535 | 27131.903 |
| t-152 | 43 | 1 | 70.072 | 49.467 | 882 | 0 | 3033 | 3033 | 0 | 8716.287 | 23986.175 | 28049.407 |
| t-376 | 65 | 1 | 43.345 | 48.199 | 566 | 0 | 3232 | 3232 | 0 | 8912.895 | 23855.103 | 27394.047 |
| t-450 | 63 | 3 | 141.433 | 146.465 | 165 | 0 | 9021 | 9021 | 0 | 8454.143 | 23199.743 | 26083.327 |
| t-002 | 17 | 1 | 153.842 | 43.804 | 377 | 0 | 2679 | 2679 | 0 | 8716.287 | 23068.671 | 25559.039 |
| t-104 | 46 | 1 | 59.3 | 45.479 | 2163 | 0 | 2844 | 2844 | 0 | 8060.927 | 22937.599 | 27656.191 |
| t-354 | 90 | 2 | 64.766 | 96.929 | 157 | 0 | 6114 | 6114 | 0 | 7471.103 | 22937.599 | 28573.695 |
| t-065 | 80 | 9 | 315.578 | 416.57 | 263 | 0 | 25168 | 25168 | 0 | 9240.575 | 22806.527 | 34340.863 |
| t-194 | 78 | 5 | 182.864 | 244.07 | 1184 | 0 | 14798 | 14798 | 0 | 8847.359 | 22675.455 | 26083.327 |
| t-378 | 55 | 3 | 159.715 | 143.096 | 3659 | 0 | 8782 | 8782 | 0 | 7700.479 | 22675.455 | 26083.327 |
| t-456 | 21 | 2 | 267.561 | 91.118 | 512 | 0 | 5349 | 5349 | 0 | 6586.367 | 22544.383 | 25034.751 |
| t-032 | 52 | 1 | 53.698 | 45.402 | 1041 | 0 | 2803 | 2803 | 0 | 6815.743 | 22020.095 | 24510.463 |
| t-488 | 26 | 1 | 107.285 | 47.505 | 1017 | 0 | 2915 | 2915 | 0 | 8159.231 | 22020.095 | 25427.967 |
| t-409 | 48 | 2 | 120.205 | 94.736 | 671 | 0 | 5781 | 5781 | 0 | 8454.143 | 21889.023 | 25952.255 |
| t-465 | 66 | 3 | 131.577 | 141.969 | 667 | 0 | 8852 | 8852 | 0 | 8323.071 | 21626.879 | 24379.391 |
| t-106 | 24 | 3 | 331.183 | 134.655 | 824 | 0 | 8132 | 8132 | 0 | 7700.479 | 21495.807 | 23461.887 |
| t-464 | 153 | 5 | 92.631 | 233.046 | 352 | 0 | 14503 | 14503 | 0 | 6651.903 | 21364.735 | 26607.615 |
| t-120 | 37 | 2 | 154.992 | 94.103 | 1412 | 0 | 5814 | 5814 | 0 | 8454.143 | 21102.591 | 24379.391 |
| t-184 | 20 | 1 | 147.97 | 48.078 | 161 | 0 | 2977 | 2977 | 0 | 7143.423 | 21102.591 | 22544.383 |
| t-136 | 146 | 4 | 75.321 | 181.554 | 450 | 0 | 11241 | 11241 | 0 | 6520.831 | 20578.303 | 26214.399 |
| t-066 | 38 | 1 | 77.996 | 49.178 | 277 | 0 | 3029 | 3029 | 0 | 7667.711 | 20316.159 | 22151.167 |
| t-272 | 14 | 1 | 186.797 | 46.429 | 394 | 0 | 2804 | 2804 | 0 | 8159.231 | 20316.159 | 21757.951 |
| t-306 | 134 | 7 | 139.774 | 312.564 | 2978 | 0 | 19312 | 19312 | 0 | 6848.511 | 20054.015 | 23724.031 |
| t-169 | 33 | 1 | 87.477 | 48.432 | 260 | 0 | 3002 | 3002 | 0 | 6914.047 | 19922.943 | 24379.391 |
