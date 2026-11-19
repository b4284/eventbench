# P3-ebhead-hot-linger — FAIL ❌

- Broker: `pgmq fifo via eb_read_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 45.15s, closing 0.832s, drain 25.452s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 619106 | 619106 | 12853.965 | 8208.891 | 12476.3 | 0 | 0 | 0 | 332 | 3147 | 46369 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 448920 | 15647.326 | 16941.055 | 29573.119 | 36831.231 | 42893.311 | 48758.783 |
| e2e_from_send | 448920 | 15596.217 | 16891.903 | 29507.583 | 36765.695 | 42827.775 | 48660.479 |
| producer_lag | 448920 | 51.111 | 40.287 | 98.047 | 275.967 | 473.599 | 828.415 |
| publish_call | 17210 | 84.796 | 67.263 | 116.223 | 391.423 | 936.447 | 2189.311 |
| queue_wait | 448920 | 15489.385 | 16793.599 | 29458.431 | 36700.159 | 42795.007 | 48627.711 |
| broker_dwell | 448920 | 15459.107 | 16752.639 | 29425.663 | 36667.391 | 42762.239 | 48594.943 |
| receive_call | 140011 | 55.017 | 55.519 | 69.503 | 100.863 | 1356.799 | 2471.935 |
| ctx_load | 448920 | 47.634 | 54.303 | 78.207 | 104.959 | 131.583 | 226.687 |
| process | 448920 | 10.697 | 0.001 | 0.002 | 0.007 | 2631.679 | 3000.319 |
| ctx_commit | 448920 | 47.872 | 54.399 | 78.399 | 106.303 | 139.391 | 688.127 |
| service | 448920 | 106.84 | 111.743 | 158.207 | 206.591 | 2742.271 | 3780.607 |
| e2e_large | 232 | 2949.478 | 2918.399 | 4126.719 | 5640.191 | 6475.775 | 6475.775 |
| ready_wait | 448920 | 898.799 | 46.399 | 108.031 | 25133.055 | 40435.711 | 48562.175 |
| blocked | 448920 | 787.95 | 46.399 | 107.711 | 21921.791 | 35553.279 | 41615.359 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8208.891 | 9446.9 |
| applied_total | 225548.176 | 364246 |
| bench_cpu_cores | 2.691 | 3.221 |
| bench_cpu_pct | 11.211 | 13.42 |
| bench_gc_gen0 | 1.441 | 3 |
| bench_gc_gen1 | 0.5 | 1 |
| bench_gc_gen2 | 0.353 | 1 |
| bench_gc_heap_mb | 1483.262 | 1765.9 |
| bench_mem_mb | 1916.924 | 2109 |
| bench_threadpool_queue | 20.118 | 231 |
| bench_threadpool_threads | 37.324 | 44 |
| broker_backlog | 135603.412 | 205098 |
| broker_oldest_age_s | 23.941 | 37 |
| broker_visible | 132097.971 | 201845 |
| ctr_eb_pgbouncer_2_cpu_pct | 29.061 | 30.41 |
| ctr_eb_pgbouncer_2_mem_mb | 14.621 | 15 |
| ctr_eb_pgbouncer_3_cpu_pct | 28.125 | 29.87 |
| ctr_eb_pgbouncer_3_mem_mb | 14.921 | 19 |
| ctr_eb_pgbouncer_4_cpu_pct | 28.501 | 29.42 |
| ctr_eb_pgbouncer_4_mem_mb | 18.268 | 20.4 |
| ctr_eb_pgbouncer_cpu_pct | 28.525 | 29.83 |
| ctr_eb_pgbouncer_mem_mb | 14.885 | 19 |
| ctr_eb_postgres_cpu_pct | 1437.364 | 1539.88 |
| ctr_eb_postgres_mem_mb | 9863.785 | 10032.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 81.842 | 88.71 |
| in_flight | 145204.265 | 218991 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.777 | 100 |
| pg_conn_active | 35.559 | 205 |
| pg_conn_idle | 170.088 | 200 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.088 | 1 |
| pg_conn_total | 205.647 | 206 |
| pg_db_size_mb | 2381.885 | 2925.7 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 30051.165 | 34482.1 |
| pg_tup_del_per_s | 30322.515 | 39022.9 |
| pg_tup_fetch_per_s | 480085.803 | 665292.5 |
| pg_tup_ins_per_s | 50495.859 | 65453.9 |
| pg_tup_upd_per_s | 22348.479 | 25676 |
| pg_wal_mb_per_s | 98.146 | 128.96 |
| pgb_cl_active | 486.676 | 499 |
| pgb_cl_waiting | 8.824 | 37 |
| pgb_maxwait_ms | 2.898 | 15.63 |
| pgb_sv_active | 29.529 | 52 |
| pgb_sv_idle | 22.029 | 49 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 12853.965 | 15379.9 |
| produced_total | 370752.441 | 583237 |
| publish_mb_per_s | 44.424 | 66.17 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 94,744 records are Completed |
| record_counts | pass | 94,744 records found, 94,744 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 332 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 619,106 events applied by workers, 619,106 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 46,369 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 21922 ms, max 41597 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 13173 seq 22: ready 115 ms before start, 115 ms of it with an idle slot in group 418
- [cross_source_blocking] source 15391 seq 12: ready 147 ms before start, 147 ms of it with an idle slot in group 482
- [cross_source_blocking] source 8095 seq 13: ready 163 ms before start, 142 ms of it with an idle slot in group 273
- [cross_source_blocking] source 12350 seq 8: ready 171 ms before start, 171 ms of it with an idle slot in group 392
- [cross_source_blocking] source 13175 seq 22: ready 137 ms before start, 137 ms of it with an idle slot in group 418
- [cross_source_blocking] source 12344 seq 4: ready 160 ms before start, 160 ms of it with an idle slot in group 392
- [cross_source_blocking] source 15272 seq 6: ready 187 ms before start, 187 ms of it with an idle slot in group 477
- [cross_source_blocking] source 13014 seq 14: ready 201 ms before start, 198 ms of it with an idle slot in group 414
- [cross_source_blocking] source 15070 seq 5: ready 188 ms before start, 188 ms of it with an idle slot in group 470
- [cross_source_blocking] source 2181 seq 7: ready 112 ms before start, 112 ms of it with an idle slot in group 62
- [cross_source_blocking] source 8276 seq 4: ready 139 ms before start, 139 ms of it with an idle slot in group 281
- [cross_source_blocking] source 15620 seq 7: ready 151 ms before start, 128 ms of it with an idle slot in group 486
- [cross_source_blocking] source 947 seq 4: ready 138 ms before start, 138 ms of it with an idle slot in group 28
- [cross_source_blocking] source 8659 seq 6: ready 137 ms before start, 125 ms of it with an idle slot in group 300
- [cross_source_blocking] source 7158 seq 10: ready 146 ms before start, 146 ms of it with an idle slot in group 243
- [cross_source_blocking] source 7105 seq 3: ready 133 ms before start, 126 ms of it with an idle slot in group 241
- [cross_source_blocking] source 12383 seq 10: ready 270 ms before start, 270 ms of it with an idle slot in group 393
- [cross_source_blocking] source 14395 seq 3: ready 227 ms before start, 144 ms of it with an idle slot in group 459
- [cross_source_blocking] source 15674 seq 13: ready 145 ms before start, 145 ms of it with an idle slot in group 489
- [cross_source_blocking] source 278 seq 12: ready 146 ms before start, 146 ms of it with an idle slot in group 7

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2291 | 2291 | 0 | 26869.759 | 48234.495 | 48758.783 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2143 | 2143 | 0 | 26476.543 | 45613.055 | 46661.631 |
| t-016 | 132 | 1 | 18.95 | 41.199 | 137 | 0 | 2117 | 2117 | 0 | 24510.463 | 45350.911 | 45875.199 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2197 | 2197 | 0 | 25427.967 | 45088.767 | 46137.343 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2045 | 2045 | 0 | 24641.535 | 43515.903 | 44564.479 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2211 | 2211 | 0 | 26607.615 | 43515.903 | 44302.335 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4878 | 4878 | 0 | 28311.551 | 42991.615 | 45088.767 |
| t-388 | 78 | 1 | 32.916 | 41.779 | 1205 | 0 | 2102 | 2102 | 0 | 24772.607 | 41680.895 | 43253.759 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4805 | 4805 | 0 | 27918.335 | 41156.607 | 44040.191 |
| t-340 | 184 | 2 | 24.502 | 75.472 | 141 | 0 | 3816 | 3816 | 0 | 22675.455 | 40894.463 | 41418.751 |
| t-458 | 132 | 1 | 15.687 | 34.029 | 2384 | 0 | 1817 | 1817 | 0 | 22544.383 | 40632.319 | 41943.039 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 6610 | 6610 | 0 | 26345.471 | 40632.319 | 41418.751 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4562 | 4562 | 0 | 27656.191 | 40108.031 | 41680.895 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2251 | 2251 | 0 | 26476.543 | 39321.599 | 40370.175 |
| t-168 | 71 | 1 | 34.616 | 40.194 | 155 | 0 | 1912 | 1912 | 0 | 22675.455 | 39321.599 | 40632.319 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2205 | 2205 | 0 | 26607.615 | 38797.311 | 40108.031 |
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 1999 | 1999 | 0 | 23592.959 | 38535.167 | 39321.599 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2204 | 2204 | 0 | 27262.975 | 38010.879 | 39845.887 |
| t-341 | 96 | 1 | 20.205 | 32.47 | 2651 | 0 | 1653 | 1653 | 0 | 20840.447 | 38010.879 | 39845.887 |
| t-283 | 74 | 1 | 27.256 | 32.897 | 240 | 0 | 1662 | 1662 | 0 | 20840.447 | 37748.735 | 39321.599 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6832 | 6832 | 0 | 27918.335 | 37486.591 | 39321.599 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2323 | 2323 | 0 | 27787.263 | 37486.591 | 38797.311 |
| t-372 | 65 | 1 | 32.959 | 35.645 | 875 | 0 | 1789 | 1789 | 0 | 21102.591 | 37486.591 | 38535.167 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 2049 | 2049 | 0 | 27000.831 | 37224.447 | 37486.591 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4525 | 4525 | 0 | 27787.263 | 37224.447 | 38010.879 |
