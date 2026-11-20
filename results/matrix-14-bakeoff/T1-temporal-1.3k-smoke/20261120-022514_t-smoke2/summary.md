# t-smoke2 — FAIL ❌

- Broker: `Temporal 127.0.0.1:7233/default: workflow per source (signal-with-start), activity per event on a task queue per group, retry 250 ms..60 s for ever, continue-as-new after 500`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 504 workers, offered 1293.357 ev/s
- Phases: run 20.153s, closing 2.963s, drain 69.292s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 47753 | 47753 | 1604.679 | 275.021 | 1001.3 | 0 | 0 | 0 | 188 | 365 | 21988 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 24255 | 38194.98 | 40042.495 | 64192.511 | 70057.983 | 70516.735 | 81919.999 |
| e2e_from_send | 24255 | 38193.098 | 40042.495 | 64192.511 | 70057.983 | 70516.735 | 81919.999 |
| producer_lag | 24255 | 1.928 | 1.215 | 3.325 | 17.551 | 85.055 | 184.575 |
| publish_call | 47753 | 132.677 | 66.687 | 368.127 | 751.615 | 1051.647 | 1771.519 |
| queue_wait | 24255 | 38166.837 | 40042.495 | 64159.743 | 69992.447 | 70451.199 | 81919.999 |
| broker_dwell | 24255 | 36.012 | 31.103 | 59.263 | 151.039 | 745.983 | 1427.455 |
| receive_call | 47702 | 160.965 | 116.159 | 385.023 | 487.935 | 498.943 | 536.063 |
| ctx_load | 24255 | 1.177 | 0.639 | 2.531 | 6.915 | 28.047 | 58.847 |
| process | 24255 | 23.283 | 0.002 | 0.005 | 1304.575 | 2836.479 | 2998.271 |
| ctx_commit | 24255 | 1.524 | 0.866 | 3.169 | 8.511 | 33.503 | 62.943 |
| ack | 22729 | 0.002 | 0.001 | 0.002 | 0.007 | 0.125 | 0.964 |
| service | 24255 | 26.23 | 1.986 | 5.823 | 1312.767 | 2838.527 | 3026.943 |
| e2e_large | 147 | 52820.922 | 55345.151 | 69009.407 | 73138.175 | 73203.711 | 73203.711 |
| ready_wait | 24255 | 19076.028 | 993.279 | 62849.023 | 69926.911 | 70385.663 | 81919.999 |
| blocked | 24255 | 19076.023 | 993.279 | 62849.023 | 69926.911 | 70385.663 | 81919.999 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 275.021 | 498.8 |
| applied_total | 1394.5 | 3850 |
| bench_cpu_cores | 2.609 | 3.273 |
| bench_cpu_pct | 10.871 | 13.64 |
| bench_gc_gen0 | 1.857 | 2 |
| bench_gc_gen1 | 0.929 | 2 |
| bench_gc_gen2 | 0.929 | 2 |
| bench_gc_heap_mb | 377.829 | 444.9 |
| bench_mem_mb | 964.3 | 1198.4 |
| bench_threadpool_queue | 5.286 | 43 |
| bench_threadpool_threads | 39.143 | 43 |
| broker_backlog | 18913.5 | 26454 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.989 | 2.32 |
| ctr_eb_pgbouncer_2_mem_mb | 3.971 | 4 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.446 | 1.05 |
| ctr_eb_pgbouncer_3_mem_mb | 4.1 | 4.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.619 | 1.42 |
| ctr_eb_pgbouncer_4_mem_mb | 5.3 | 5.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.538 | 1.24 |
| ctr_eb_pgbouncer_mem_mb | 4.3 | 4.3 |
| ctr_eb_postgres_cpu_pct | 871.885 | 937.81 |
| ctr_eb_postgres_mem_mb | 12325.564 | 12369.2 |
| ctr_eb_temporal_cpu_pct | 669.615 | 797.66 |
| ctr_eb_temporal_mem_mb | 1712.993 | 1964.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 79.867 | 89.32 |
| in_flight | 20898.214 | 28415 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0.214 | 1 |
| pg_active_loads | 0.571 | 2 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.985 | 100 |
| pg_conn_active | 0 | 0 |
| pg_conn_idle | 192 | 192 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 192 | 192 |
| pg_db_size_mb | 1980.536 | 1983.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 348.393 | 823 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 1091.557 | 1842.2 |
| pg_tup_ins_per_s | 72.971 | 171.8 |
| pg_tup_upd_per_s | 223.264 | 615.9 |
| pg_wal_mb_per_s | 51.106 | 60.83 |
| pgb_cl_active | 5.929 | 15 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 5 | 14 |
| pgb_sv_used | 43 | 48 |
| produced_per_s | 1604.679 | 1886.4 |
| produced_total | 22292.714 | 32265 |
| publish_mb_per_s | 18.605 | 30.19 |
| temporal_published_total | 19935.857 | 29499 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 17,107 records are Completed |
| record_counts | pass | 17,107 records found, 17,107 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 188 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 47,753 events applied by workers, 47,753 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 21,988 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 69927 ms, max 81863 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2055 seq 2: ready 191 ms before start, 191 ms of it with an idle slot in group 61
- [cross_source_blocking] source 8559 seq 2: ready 193 ms before start, 193 ms of it with an idle slot in group 293
- [cross_source_blocking] source 8753 seq 2: ready 161 ms before start, 161 ms of it with an idle slot in group 304
- [cross_source_blocking] source 7229 seq 2: ready 216 ms before start, 216 ms of it with an idle slot in group 246
- [cross_source_blocking] source 1548 seq 2: ready 279 ms before start, 279 ms of it with an idle slot in group 46
- [cross_source_blocking] source 9869 seq 2: ready 230 ms before start, 230 ms of it with an idle slot in group 340
- [cross_source_blocking] source 3254 seq 3: ready 244 ms before start, 244 ms of it with an idle slot in group 108
- [cross_source_blocking] source 15469 seq 2: ready 264 ms before start, 264 ms of it with an idle slot in group 483
- [cross_source_blocking] source 2250 seq 3: ready 275 ms before start, 275 ms of it with an idle slot in group 65
- [cross_source_blocking] source 9115 seq 2: ready 191 ms before start, 191 ms of it with an idle slot in group 315
- [cross_source_blocking] source 3831 seq 2: ready 265 ms before start, 265 ms of it with an idle slot in group 130
- [cross_source_blocking] source 7229 seq 3: ready 248 ms before start, 248 ms of it with an idle slot in group 246
- [cross_source_blocking] source 95 seq 3: ready 310 ms before start, 310 ms of it with an idle slot in group 1
- [cross_source_blocking] source 1548 seq 3: ready 280 ms before start, 280 ms of it with an idle slot in group 46
- [cross_source_blocking] source 13937 seq 3: ready 348 ms before start, 348 ms of it with an idle slot in group 443
- [cross_source_blocking] source 13853 seq 2: ready 422 ms before start, 422 ms of it with an idle slot in group 438
- [cross_source_blocking] source 8559 seq 3: ready 189 ms before start, 189 ms of it with an idle slot in group 293
- [cross_source_blocking] source 15015 seq 3: ready 348 ms before start, 348 ms of it with an idle slot in group 469
- [cross_source_blocking] source 3254 seq 4: ready 180 ms before start, 180 ms of it with an idle slot in group 108
- [cross_source_blocking] source 4315 seq 2: ready 105 ms before start, 105 ms of it with an idle slot in group 147

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 45 | 45 | 0 | 62652.415 | 73400.319 | 73400.319 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 31 | 31 | 0 | 66846.719 | 73400.319 | 73400.319 |
| t-359 | 52 | 1 | 2.53 | 2.173 | 132 | 0 | 104 | 104 | 0 | 58195.967 | 71827.455 | 71827.455 |
| t-368 | 36 | 1 | 1.85 | 1.097 | 2666 | 0 | 32 | 32 | 0 | 68681.727 | 71827.455 | 71827.455 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 196 | 196 | 0 | 51904.511 | 71303.167 | 71303.167 |
| t-006 | 14 | 1 | 2.511 | 0.56 | 1170 | 0 | 30 | 30 | 0 | 67108.863 | 70778.879 | 70778.879 |
| t-009 | 29 | 1 | 1.927 | 0.925 | 2407 | 0 | 65 | 65 | 0 | 60555.263 | 70778.879 | 70778.879 |
| t-014 | 34 | 1 | 3.151 | 1.712 | 214 | 0 | 64 | 64 | 0 | 56885.247 | 70778.879 | 70778.879 |
| t-016 | 132 | 1 | 1.895 | 4.12 | 137 | 0 | 191 | 191 | 0 | 52953.087 | 70778.879 | 70778.879 |
| t-021 | 19 | 1 | 1.626 | 0.541 | 3252 | 0 | 27 | 27 | 0 | 66060.287 | 70778.879 | 70778.879 |
| t-022 | 63 | 1 | 3.023 | 3.184 | 162 | 0 | 150 | 150 | 0 | 53477.375 | 70778.879 | 70778.879 |
| t-030 | 19 | 1 | 2.498 | 0.8 | 748 | 0 | 34 | 34 | 0 | 58195.967 | 70778.879 | 70778.879 |
| t-039 | 28 | 1 | 1.831 | 0.856 | 1459 | 0 | 56 | 56 | 0 | 54263.807 | 70778.879 | 70778.879 |
| t-040 | 119 | 1 | 2.622 | 5.28 | 1254 | 0 | 243 | 243 | 0 | 57933.823 | 70778.879 | 70778.879 |
| t-050 | 96 | 1 | 1.092 | 1.737 | 130 | 0 | 76 | 76 | 0 | 62390.271 | 70778.879 | 70778.879 |
| t-058 | 23 | 1 | 3.384 | 1.279 | 768 | 0 | 62 | 62 | 0 | 47185.919 | 70778.879 | 70778.879 |
| t-061 | 148 | 1 | 2.245 | 5.493 | 1274 | 0 | 235 | 235 | 0 | 57147.391 | 70778.879 | 70778.879 |
| t-062 | 18 | 1 | 3.136 | 0.961 | 2061 | 0 | 44 | 44 | 0 | 59506.687 | 70778.879 | 70778.879 |
| t-068 | 21 | 1 | 1.972 | 0.677 | 3531 | 0 | 42 | 42 | 0 | 50331.647 | 70778.879 | 70778.879 |
| t-076 | 23 | 1 | 1.778 | 0.672 | 4045 | 0 | 35 | 35 | 0 | 65798.143 | 70778.879 | 70778.879 |
| t-080 | 56 | 1 | 3.299 | 3.108 | 822 | 0 | 160 | 160 | 0 | 52953.087 | 70778.879 | 70778.879 |
| t-082 | 42 | 1 | 1.791 | 1.264 | 150 | 0 | 69 | 69 | 0 | 52690.943 | 70778.879 | 70778.879 |
| t-083 | 58 | 1 | 2.338 | 2.311 | 417 | 0 | 116 | 116 | 0 | 59768.831 | 70778.879 | 70778.879 |
| t-094 | 13 | 1 | 3.221 | 0.695 | 1443 | 0 | 30 | 30 | 0 | 47185.919 | 70778.879 | 70778.879 |
| t-098 | 6 | 1 | 2.735 | 0.253 | 192 | 0 | 8 | 8 | 0 | 64225.279 | 70778.879 | 70778.879 |
