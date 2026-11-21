# G2-dc-freeze:leader:60 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.951s, closing 2.349s, drain 10.202s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 454698 | 454698 | 6724.929 | 5652.929 | 20417 | 0 | 1683 | 1683 | 271 | 2304 | 125627 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 286544 | 15078.561 | 5255.167 | 42369.023 | 51019.775 | 54493.183 | 58294.271 |
| e2e_from_send | 286544 | 14659.735 | 4931.583 | 42336.255 | 50954.239 | 54427.647 | 58294.271 |
| producer_lag | 286544 | 418.823 | 2.457 | 71.807 | 26935.295 | 28311.551 | 28819.455 |
| publish_call | 454698 | 194.32 | 64.671 | 141.183 | 363.263 | 28655.615 | 28917.759 |
| queue_wait | 286544 | 14568.394 | 4825.087 | 42303.487 | 50888.703 | 54362.111 | 58261.503 |
| broker_dwell | 286544 | 14323.54 | 4632.575 | 42270.719 | 50790.399 | 54231.039 | 58195.967 |
| receive_call | 403512 | 31.972 | 8.783 | 86.783 | 342.271 | 479.999 | 540.671 |
| ctx_load | 286544 | 11.904 | 7.691 | 26.671 | 57.951 | 141.695 | 361.727 |
| process | 286544 | 10.872 | 0.002 | 0.004 | 0.059 | 2633.727 | 3000.319 |
| ctx_commit | 286544 | 68.482 | 64.159 | 123.775 | 245.503 | 441.855 | 534.015 |
| ack | 286537 | 0.009 | 0.004 | 0.007 | 0.047 | 0.847 | 45.855 |
| service | 286544 | 91.351 | 77.503 | 143.743 | 346.111 | 2715.647 | 3397.631 |
| e2e_large | 171 | 5100.43 | 2936.831 | 5631.999 | 32112.639 | 34668.543 | 34668.543 |
| ready_wait | 286544 | 1184.755 | 99.711 | 1375.231 | 34308.095 | 37322.751 | 42270.719 |
| blocked | 286544 | 494.393 | 61.375 | 726.527 | 10936.319 | 13271.039 | 28671.999 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5652.929 | 12905.4 |
| applied_total | 260414.366 | 323142 |
| bench_cpu_cores | 2.676 | 5.456 |
| bench_cpu_pct | 11.148 | 22.74 |
| bench_gc_gen0 | 1.317 | 4 |
| bench_gc_gen1 | 0.415 | 2 |
| bench_gc_gen2 | 0.317 | 1 |
| bench_gc_heap_mb | 1190.366 | 1425 |
| bench_mem_mb | 1920.295 | 2035.8 |
| bench_threadpool_queue | 159.073 | 5331 |
| bench_threadpool_threads | 52.951 | 71 |
| broker_backlog | 87691.561 | 101112 |
| ctr_eb_dc1a_cpu_pct | 778.094 | 821.38 |
| ctr_eb_dc1a_mem_mb | 8310.532 | 9918.2 |
| ctr_eb_dc1b_cpu_pct | 47.93 | 52.74 |
| ctr_eb_dc1b_mem_mb | 2175.241 | 2222.1 |
| ctr_eb_dc2a_cpu_pct | 30.193 | 53.73 |
| ctr_eb_dc2a_mem_mb | 3081.059 | 3726 |
| ctr_eb_dc2b_cpu_pct | 26.805 | 49.91 |
| ctr_eb_dc2b_mem_mb | 3440.395 | 4617.7 |
| ctr_eb_etcd_dc1_cpu_pct | 1.95 | 2.26 |
| ctr_eb_etcd_dc1_mem_mb | 32.886 | 34.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.272 | 1.78 |
| ctr_eb_etcd_dc2_mem_mb | 29.41 | 30.5 |
| ctr_eb_etcd_w_cpu_pct | 1.907 | 2.68 |
| ctr_eb_etcd_w_mem_mb | 55.663 | 57.5 |
| ctr_eb_haproxy_cpu_pct | 62.506 | 108.17 |
| ctr_eb_haproxy_mem_mb | 28.993 | 30.4 |
| ctr_eb_mosquitto_cpu_pct | 31.218 | 56.7 |
| ctr_eb_mosquitto_mem_mb | 4.234 | 7.2 |
| ctr_eb_mssql_cpu_pct | 2.406 | 3.7 |
| ctr_eb_mssql_mem_mb | 4751.378 | 4767.4 |
| ctr_eb_mysql_cpu_pct | 0.666 | 0.93 |
| ctr_eb_mysql_mem_mb | 6018.6 | 6018.6 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 3 | 3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 2.4 | 2.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 3 | 3 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 3 | 3 |
| ctr_eb_postgres_cpu_pct | 0.98 | 2.24 |
| ctr_eb_postgres_mem_mb | 1528.654 | 1529.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 41.051 | 1683.1 |
| host_cpu_pct | 41.497 | 79.64 |
| in_flight | 90544.951 | 99494 |
| nacks_per_s | 41.051 | 1683.1 |
| octopus_dispatcher_cores | 1.079 | 1.43 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 596641.341 | 808525 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 158321.439 | 209575 |
| octopus_mqtt_sent_total | 155981.537 | 212878 |
| octopus_publish_retries_total | 2293.39 | 15965 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.39 | 18 |
| pg_active_loads | 5.537 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 62.122 | 86 |
| pg_conn_idle | 204.61 | 232 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 266.732 | 268 |
| pg_db_size_mb | 1372.432 | 1630.8 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 5.256 | 7.93 |
| pg_replica_replay_lag_ms | 28.137 | 36.3 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24201.749 | 29638.7 |
| pg_tup_del_per_s | 24935.327 | 29610.1 |
| pg_tup_fetch_per_s | 190215.534 | 199658.6 |
| pg_tup_ins_per_s | 37312.51 | 50004.8 |
| pg_tup_upd_per_s | 29945.761 | 35876.2 |
| pg_wal_mb_per_s | 90.449 | 113.78 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0.317 | 1 |
| produced_per_s | 6724.929 | 14282.2 |
| produced_total | 350959.317 | 420917 |
| publish_mb_per_s | 25.199 | 71.05 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,925 records are Completed |
| record_counts | pass | 74,925 records found, 74,925 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 271 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 454,698 events applied by workers, 454,698 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 125,627 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 10936 ms, max 28670 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13020 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 414
- [cross_source_blocking] source 2179 seq 4: ready 164 ms before start, 164 ms of it with an idle slot in group 62
- [cross_source_blocking] source 6345 seq 14: ready 173 ms before start, 173 ms of it with an idle slot in group 216
- [cross_source_blocking] source 6091 seq 6: ready 148 ms before start, 148 ms of it with an idle slot in group 209
- [cross_source_blocking] source 1020 seq 12: ready 113 ms before start, 113 ms of it with an idle slot in group 31
- [cross_source_blocking] source 7164 seq 10: ready 164 ms before start, 164 ms of it with an idle slot in group 243
- [cross_source_blocking] source 13976 seq 8: ready 110 ms before start, 110 ms of it with an idle slot in group 444
- [cross_source_blocking] source 8663 seq 4: ready 109 ms before start, 109 ms of it with an idle slot in group 300
- [cross_source_blocking] source 5175 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 179
- [cross_source_blocking] source 2979 seq 5: ready 107 ms before start, 107 ms of it with an idle slot in group 98
- [cross_source_blocking] source 12655 seq 14: ready 181 ms before start, 181 ms of it with an idle slot in group 398
- [cross_source_blocking] source 3406 seq 13: ready 116 ms before start, 116 ms of it with an idle slot in group 117
- [cross_source_blocking] source 5670 seq 11: ready 113 ms before start, 113 ms of it with an idle slot in group 195
- [cross_source_blocking] source 12811 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 404
- [cross_source_blocking] source 12023 seq 5: ready 108 ms before start, 108 ms of it with an idle slot in group 384
- [cross_source_blocking] source 2202 seq 7: ready 116 ms before start, 116 ms of it with an idle slot in group 64
- [cross_source_blocking] source 2935 seq 4: ready 172 ms before start, 172 ms of it with an idle slot in group 94
- [cross_source_blocking] source 2173 seq 9: ready 149 ms before start, 149 ms of it with an idle slot in group 62
- [cross_source_blocking] source 8233 seq 13: ready 134 ms before start, 134 ms of it with an idle slot in group 278
- [cross_source_blocking] source 15759 seq 19: ready 112 ms before start, 112 ms of it with an idle slot in group 493

## Error samples

- t-308L#0 src=8921 seq=1 op=Create rec=38315403247617 ev=0: NpgsqlException: Exception while reading from stream
- t-308L#0 src=8930 seq=2 op=Update rec=38354057953281 ev=1: NpgsqlException: Exception while reading from stream
- t-319L#0 src=9256 seq=2 op=Update rec=39754217291777 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3427 seq=5 op=Update rec=14718852923393 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3439 seq=5 op=Update rec=14770392530946 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3474 seq=3 op=Update rec=14920716386305 ev=1: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3433 seq=5 op=Update rec=14744622727169 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#1 src=3436 seq=4 op=Update rec=14757507629057 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3463 seq=7 op=Update rec=14873471746051 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#2 src=3482 seq=5 op=Update rec=14955076124673 ev=3: NpgsqlException: Exception while reading from stream
- t-119L#0 src=3467 seq=4 op=Update rec=14890651615233 ev=2: NpgsqlException: Exception while reading from stream
- t-119L#3 src=3465 seq=4 op=Update rec=14882061680641 ev=1: NpgsqlException: Exception while reading from stream
- t-259#0 src=7531 seq=11 op=Update rec=32345398706178 ev=3: NpgsqlException: Exception while reading from stream
- t-347#2 src=10384 seq=24 op=Update rec=44598940401666 ev=5: NpgsqlException: Exception while reading from stream
- t-498#0 src=15960 seq=15 op=Get rec=68547678044163 ev=2: NpgsqlException: Exception while reading from stream
- t-065#4 src=2220 seq=85 op=Update rec=9534827397130 ev=7: NpgsqlException: Exception while reading from stream
- t-423#0 src=13228 seq=20 op=Create rec=56813827391492 ev=0: NpgsqlException: Exception while reading from stream
- t-468#1 src=14914 seq=44 op=Update rec=64055142252551 ev=6: NpgsqlException: Exception while reading from stream
- t-342#0 src=10039 seq=52 op=Complete rec=43117176684553 ev=4: NpgsqlException: Exception while reading from stream
- t-374#1 src=11503 seq=63 op=Update rec=49405008805896 ev=2: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6591 | 6591 | 0 | 28049.407 | 56098.815 | 57933.823 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3325 | 3325 | 0 | 35389.439 | 55574.527 | 58458.111 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7530 | 7530 | 0 | 32112.639 | 54001.663 | 56885.247 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 4406 | 4406 | 0 | 8028.159 | 54001.663 | 56098.815 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 2751 | 2751 | 0 | 6029.311 | 54001.663 | 56098.815 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1598 | 1598 | 0 | 39583.743 | 53739.519 | 55050.239 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 2937 | 2937 | 0 | 8912.895 | 53739.519 | 56098.815 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1611 | 1611 | 0 | 6684.671 | 53739.519 | 55836.671 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3077 | 3077 | 0 | 34865.151 | 53739.519 | 56098.815 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1447 | 1447 | 0 | 4784.127 | 53215.231 | 54788.095 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 4694 | 4694 | 0 | 35127.295 | 53215.231 | 55836.671 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 2500 | 2500 | 0 | 8978.431 | 52953.087 | 54001.663 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3534 | 3534 | 0 | 37224.447 | 52953.087 | 56098.815 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5002 | 5002 | 0 | 36962.303 | 52953.087 | 55574.527 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 5820 | 5820 | 0 | 33554.431 | 52953.087 | 55312.383 |
| t-438 | 121 | 4 | 74.684 | 156.372 | 1839 | 0 | 5258 | 5258 | 0 | 7536.639 | 52953.087 | 55836.671 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 4098 | 4098 | 0 | 29360.127 | 52690.943 | 56623.103 |
| t-121 | 76 | 3 | 97.816 | 119.994 | 149 | 0 | 4064 | 4064 | 0 | 8224.767 | 52428.799 | 54788.095 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3387 | 3387 | 0 | 38273.023 | 52428.799 | 54001.663 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 2516 | 2516 | 0 | 4620.287 | 52428.799 | 53477.375 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1437 | 1437 | 0 | 35913.727 | 52166.655 | 54525.951 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1520 | 1520 | 0 | 38273.023 | 52166.655 | 53477.375 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3160 | 3160 | 0 | 35389.439 | 52166.655 | 54525.951 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 5835 | 5835 | 0 | 28704.767 | 52166.655 | 54263.807 |
| t-362 | 47 | 1 | 47.764 | 36.599 | 3235 | 0 | 1298 | 1298 | 0 | 3997.695 | 52166.655 | 56623.103 |
