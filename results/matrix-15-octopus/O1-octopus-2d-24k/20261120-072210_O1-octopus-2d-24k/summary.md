# O1-octopus-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 8 worker connections), commit through the apply hook`
- Store: `bench tables via Postgres (postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70); commits through Octopus's apply hook`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.325s, closing 3.205s, drain 12.105s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 854079 | 854079 | 13502.882 | 10816.876 | 19187.4 | 0 | 0 | 0 | 285 | 4297 | 383770 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 570441 | 26409.281 | 25739.263 | 38862.847 | 42074.111 | 42958.847 | 45547.519 |
| e2e_from_send | 570441 | 9145.16 | 9814.015 | 18546.687 | 23216.127 | 24625.151 | 27148.287 |
| producer_lag | 570441 | 17264.135 | 17580.031 | 24641.535 | 26394.623 | 26902.527 | 27443.199 |
| publish_call | 854079 | 132.917 | 130.367 | 172.671 | 250.623 | 346.367 | 524.031 |
| queue_wait | 570441 | 9032.555 | 9707.519 | 18464.767 | 23117.823 | 24543.231 | 26345.471 |
| broker_dwell | 570441 | 9020.02 | 9691.135 | 18448.383 | 23101.439 | 24526.847 | 26329.087 |
| receive_call | 748903 | 52.933 | 30.063 | 128.831 | 334.847 | 473.087 | 514.303 |
| ctx_load | 570441 | 0.001 | 0.001 | 0.001 | 0.001 | 0.001 | 0.504 |
| process | 570441 | 10.564 | 0.001 | 0.002 | 0.005 | 2613.247 | 3014.655 |
| ctx_commit | 570441 | 102.025 | 114.431 | 155.135 | 191.103 | 212.991 | 534.015 |
| service | 570441 | 112.604 | 114.687 | 156.031 | 199.167 | 2715.647 | 3385.343 |
| e2e_large | 184 | 19496.826 | 19398.655 | 27508.735 | 29048.831 | 30932.991 | 30932.991 |
| ready_wait | 570441 | 721.799 | 204.543 | 1975.295 | 6414.335 | 11739.135 | 21823.487 |
| blocked | 570441 | 633.669 | 188.287 | 1738.751 | 5378.047 | 9732.095 | 20348.927 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10816.876 | 12360 |
| applied_total | 391355.02 | 651915 |
| bench_cpu_cores | 2.583 | 3.164 |
| bench_cpu_pct | 10.764 | 13.18 |
| bench_gc_gen0 | 4.143 | 6 |
| bench_gc_gen1 | 4.122 | 6 |
| bench_gc_gen2 | 0.633 | 2 |
| bench_gc_heap_mb | 895.953 | 1121.1 |
| bench_mem_mb | 1368.571 | 1522.5 |
| bench_threadpool_queue | 31.857 | 1068 |
| bench_threadpool_threads | 41.204 | 44 |
| broker_backlog | 90884.02 | 153697 |
| ctr_eb_mosquitto_cpu_pct | 94.768 | 95.42 |
| ctr_eb_mosquitto_mem_mb | 12.065 | 21.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 1.6 | 1.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 1.6 | 1.6 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 2.035 | 2.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 1.906 | 2 |
| ctr_eb_postgres_cpu_pct | 964.344 | 997.35 |
| ctr_eb_postgres_mem_mb | 2324.498 | 2778.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 65.892 | 70.06 |
| in_flight | 94221.143 | 161205 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.294 | 2.41 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_mqtt_received_total | 1164528.143 | 1927679 |
| octopus_mqtt_sent_total | 1221765.102 | 2022743 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0 | 0 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 9.143 | 15 |
| pg_conn_idle | 25.898 | 35 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 35.041 | 36 |
| pg_db_size_mb | 3127.812 | 3568 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 14388.247 | 16408.3 |
| pg_tup_del_per_s | 28679.782 | 34331.7 |
| pg_tup_fetch_per_s | 1006967.463 | 1133174.9 |
| pg_tup_ins_per_s | 41595.151 | 49638.3 |
| pg_tup_upd_per_s | 29842.969 | 31974.4 |
| pg_wal_mb_per_s | 74.385 | 91.16 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 13502.882 | 16094.6 |
| produced_total | 485576.163 | 813120 |
| publish_mb_per_s | 39.269 | 57.2 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 122,038 records are Completed |
| record_counts | pass | 122,038 records found, 122,038 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 285 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 854,079 events applied by workers, 854,079 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 383,770 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 5378 ms, max 20337 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 6332 seq 21: ready 163 ms before start, 163 ms of it with an idle slot in group 215
- [cross_source_blocking] source 13212 seq 31: ready 103 ms before start, 103 ms of it with an idle slot in group 421
- [cross_source_blocking] source 15612 seq 7: ready 145 ms before start, 145 ms of it with an idle slot in group 485
- [cross_source_blocking] source 4796 seq 13: ready 119 ms before start, 119 ms of it with an idle slot in group 167
- [cross_source_blocking] source 15356 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 480
- [cross_source_blocking] source 9212 seq 6: ready 141 ms before start, 141 ms of it with an idle slot in group 316
- [cross_source_blocking] source 2332 seq 27: ready 122 ms before start, 122 ms of it with an idle slot in group 67
- [cross_source_blocking] source 12252 seq 8: ready 116 ms before start, 116 ms of it with an idle slot in group 389
- [cross_source_blocking] source 9308 seq 26: ready 118 ms before start, 118 ms of it with an idle slot in group 321
- [cross_source_blocking] source 8572 seq 27: ready 161 ms before start, 100 ms of it with an idle slot in group 295
- [cross_source_blocking] source 284 seq 24: ready 217 ms before start, 163 ms of it with an idle slot in group 7
- [cross_source_blocking] source 14204 seq 51: ready 121 ms before start, 121 ms of it with an idle slot in group 454
- [cross_source_blocking] source 6844 seq 6: ready 173 ms before start, 173 ms of it with an idle slot in group 233
- [cross_source_blocking] source 6684 seq 17: ready 147 ms before start, 125 ms of it with an idle slot in group 228
- [cross_source_blocking] source 2620 seq 8: ready 217 ms before start, 217 ms of it with an idle slot in group 80
- [cross_source_blocking] source 12444 seq 14: ready 212 ms before start, 158 ms of it with an idle slot in group 396
- [cross_source_blocking] source 348 seq 13: ready 273 ms before start, 175 ms of it with an idle slot in group 10
- [cross_source_blocking] source 15804 seq 9: ready 229 ms before start, 229 ms of it with an idle slot in group 495
- [cross_source_blocking] source 12860 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 407
- [cross_source_blocking] source 7420 seq 25: ready 118 ms before start, 118 ms of it with an idle slot in group 257

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-066 | 38 | 1 | 78.415 | 49.442 | 277 | 0 | 1836 | 1836 | 0 | 39845.887 | 43253.759 | 44826.623 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 8568 | 8568 | 0 | 39321.599 | 43253.759 | 45088.767 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 10338 | 10338 | 0 | 39845.887 | 43253.759 | 44826.623 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 5233 | 5233 | 0 | 39845.887 | 43253.759 | 45613.055 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 8698 | 8698 | 0 | 39845.887 | 43253.759 | 45613.055 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 6786 | 6786 | 0 | 38797.311 | 42729.471 | 44302.335 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 8581 | 8581 | 0 | 39321.599 | 42729.471 | 45088.767 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 4901 | 4901 | 0 | 38797.311 | 42467.327 | 44302.335 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 1780 | 1780 | 0 | 38797.311 | 42205.183 | 42729.471 |
| t-439 | 22 | 1 | 127.568 | 47.568 | 3339 | 0 | 1616 | 1616 | 0 | 38273.023 | 42205.183 | 43778.047 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 1644 | 1644 | 0 | 39321.599 | 42205.183 | 43778.047 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 1706 | 1706 | 0 | 38797.311 | 41943.039 | 44564.479 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 5304 | 5304 | 0 | 39059.455 | 41943.039 | 44040.191 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 4554 | 4554 | 0 | 36175.871 | 41418.751 | 42467.327 |
| t-184 | 20 | 1 | 148.766 | 48.337 | 161 | 0 | 1615 | 1615 | 0 | 38535.167 | 41418.751 | 41680.895 |
| t-272 | 14 | 1 | 187.801 | 46.679 | 394 | 0 | 1633 | 1633 | 0 | 38535.167 | 41418.751 | 43253.759 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 8396 | 8396 | 0 | 38273.023 | 41418.751 | 43515.903 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 3735 | 3735 | 0 | 37748.735 | 41156.607 | 43515.903 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 14294 | 14294 | 0 | 37224.447 | 41156.607 | 42991.615 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 1548 | 1548 | 0 | 36175.871 | 41156.607 | 41680.895 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 1752 | 1752 | 0 | 38535.167 | 41156.607 | 42991.615 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 3272 | 3272 | 0 | 36700.159 | 41156.607 | 42467.327 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 12721 | 12721 | 0 | 37486.591 | 41156.607 | 42729.471 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 1755 | 1755 | 0 | 36962.303 | 41156.607 | 43515.903 |
| t-446 | 49 | 1 | 56.149 | 44.28 | 196 | 0 | 1602 | 1602 | 0 | 37486.591 | 41156.607 | 41943.039 |
