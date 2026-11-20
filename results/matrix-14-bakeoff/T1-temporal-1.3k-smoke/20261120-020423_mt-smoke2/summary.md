# mt-smoke2 — FAIL ❌

- Broker: `MassTransit SQL transport (Postgres 127.0.0.1:5432/bench, schema transport), queue per group, partition key = source, PartitionedOrdered limit 1, connection limit 50, unlock delay 250 ms, max delivery 1000000`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 511 workers, offered 3862.838 ev/s
- Phases: run 20.245s, closing 5.184s, drain 5.903s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 77253 | 77253 | 2901.421 | 2893.4 | 5233 | 0 | 0 | 0 | 149 | 428 | 3941 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 33912 | 6077.53 | 6008.831 | 7196.671 | 9535.487 | 11321.343 | 21069.823 |
| e2e_from_send | 33912 | 211.855 | 22.191 | 501.247 | 3168.255 | 4866.047 | 14548.991 |
| producer_lag | 33912 | 5865.67 | 5906.431 | 6860.799 | 7548.927 | 7938.047 | 7999.487 |
| publish_call | 77253 | 10.387 | 7.535 | 12.191 | 60.543 | 748.543 | 1950.719 |
| queue_wait | 33912 | 191.044 | 17.839 | 410.879 | 2977.791 | 4464.639 | 12410.879 |
| broker_dwell | 33912 | 190.371 | 17.183 | 410.111 | 2975.743 | 4464.639 | 12410.879 |
| receive_call | 73289 | 77.459 | 29.231 | 233.599 | 454.911 | 496.127 | 2060.287 |
| ctx_load | 33912 | 1.535 | 1.239 | 2.905 | 6.143 | 10.935 | 62.303 |
| process | 33912 | 15.173 | 0.001 | 0.003 | 0.026 | 2766.847 | 3657.727 |
| ctx_commit | 33912 | 3.761 | 2.117 | 4.935 | 36.511 | 231.551 | 866.815 |
| ack | 27030 | 0.002 | 0.001 | 0.002 | 0.004 | 0.044 | 1.54 |
| service | 33912 | 20.81 | 3.745 | 7.695 | 108.479 | 2791.423 | 3692.543 |
| e2e_large | 105 | 9842.454 | 9478.143 | 12967.935 | 16211.967 | 21069.823 | 21069.823 |
| ready_wait | 33912 | 141.343 | 9.295 | 187.775 | 2621.439 | 4171.775 | 6672.383 |
| blocked | 33912 | 141.207 | 9.295 | 187.391 | 2617.343 | 4171.775 | 6672.383 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2893.4 | 3831.2 |
| applied_total | 36951.929 | 57837 |
| bench_cpu_cores | 2.943 | 4.139 |
| bench_cpu_pct | 12.26 | 17.25 |
| bench_gc_gen0 | 2.357 | 4 |
| bench_gc_gen1 | 0.643 | 1 |
| bench_gc_gen2 | 0.643 | 1 |
| bench_gc_heap_mb | 1394.7 | 1819.6 |
| bench_mem_mb | 1869.221 | 2237 |
| bench_threadpool_queue | 175.071 | 1098 |
| bench_threadpool_threads | 46.429 | 47 |
| broker_backlog | 652.643 | 796 |
| ctr_eb_pgbouncer_2_cpu_pct | 4.297 | 5.29 |
| ctr_eb_pgbouncer_2_mem_mb | 1.686 | 1.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 7.688 | 9.29 |
| ctr_eb_pgbouncer_3_mem_mb | 1.786 | 2 |
| ctr_eb_pgbouncer_4_cpu_pct | 5.896 | 7.3 |
| ctr_eb_pgbouncer_4_mem_mb | 1.829 | 2.2 |
| ctr_eb_pgbouncer_cpu_pct | 6.76 | 8.3 |
| ctr_eb_pgbouncer_mem_mb | 2 | 2 |
| ctr_eb_postgres_cpu_pct | 1162.1 | 1403.47 |
| ctr_eb_postgres_mem_mb | 2380.45 | 2440.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 66.866 | 83.43 |
| in_flight | 635 | 764 |
| mt_error_or_dead_letter_rows | 0 | 0 |
| mt_faulted_total | 0 | 0 |
| nacks_per_s | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 1.357 | 7 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 18.714 | 28 |
| pg_conn_idle | 87.786 | 91 |
| pg_conn_idle_in_tx | 15.786 | 19 |
| pg_conn_lock_wait | 0.857 | 6 |
| pg_conn_total | 122.286 | 123 |
| pg_db_size_mb | 2588.85 | 2895 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 28703.379 | 38227.5 |
| pg_tup_del_per_s | 22897.693 | 37103.4 |
| pg_tup_fetch_per_s | 107008.529 | 162093.7 |
| pg_tup_ins_per_s | 36028.686 | 52998.1 |
| pg_tup_upd_per_s | 7132.936 | 8824.6 |
| pg_wal_mb_per_s | 62.976 | 96.19 |
| pgb_cl_active | 14.643 | 21 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 1 | 4 |
| pgb_sv_idle | 11.571 | 17 |
| pgb_sv_used | 3.929 | 7 |
| produced_per_s | 2901.421 | 3894.2 |
| produced_total | 37586.929 | 58543 |
| publish_mb_per_s | 31.796 | 55.17 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 22,683 records are Completed |
| record_counts | pass | 22,683 records found, 22,683 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 149 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 77,253 events applied by workers, 77,253 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 3,941 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2617 ms, max 6671 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5628 seq 2: ready 1951 ms before start, 1951 ms of it with an idle slot in group 194
- [cross_source_blocking] source 3100 seq 2: ready 1953 ms before start, 1953 ms of it with an idle slot in group 103
- [cross_source_blocking] source 9596 seq 1: ready 126 ms before start, 126 ms of it with an idle slot in group 333
- [cross_source_blocking] source 15455 seq 3: ready 408 ms before start, 408 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15560 seq 2: ready 274 ms before start, 271 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15587 seq 4: ready 164 ms before start, 160 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15580 seq 2: ready 134 ms before start, 131 ms of it with an idle slot in group 483
- [cross_source_blocking] source 15338 seq 2: ready 142 ms before start, 142 ms of it with an idle slot in group 479
- [cross_source_blocking] source 10179 seq 5: ready 234 ms before start, 234 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10188 seq 5: ready 190 ms before start, 190 ms of it with an idle slot in group 344
- [cross_source_blocking] source 5243 seq 4: ready 165 ms before start, 165 ms of it with an idle slot in group 181
- [cross_source_blocking] source 10299 seq 4: ready 281 ms before start, 281 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10152 seq 2: ready 289 ms before start, 289 ms of it with an idle slot in group 344
- [cross_source_blocking] source 10248 seq 3: ready 389 ms before start, 389 ms of it with an idle slot in group 344
- [cross_source_blocking] source 3888 seq 2: ready 105 ms before start, 105 ms of it with an idle slot in group 131
- [cross_source_blocking] source 3881 seq 2: ready 135 ms before start, 135 ms of it with an idle slot in group 131
- [cross_source_blocking] source 4073 seq 2: ready 297 ms before start, 297 ms of it with an idle slot in group 136
- [cross_source_blocking] source 10120 seq 2: ready 488 ms before start, 488 ms of it with an idle slot in group 344
- [cross_source_blocking] source 14984 seq 3: ready 594 ms before start, 588 ms of it with an idle slot in group 469
- [cross_source_blocking] source 15007 seq 2: ready 807 ms before start, 801 ms of it with an idle slot in group 469

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 156 | 156 | 0 | 10223.615 | 19005.439 | 21102.591 |
| t-425 | 89 | 1 | 24.935 | 36.451 | 2911 | 0 | 690 | 690 | 0 | 8290.303 | 11796.479 | 11927.551 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 67 | 67 | 0 | 8323.071 | 11468.799 | 11468.799 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 34 | 34 | 0 | 8912.895 | 11403.263 | 11403.263 |
| t-131 | 140 | 1 | 18.254 | 43.004 | 961 | 0 | 781 | 781 | 0 | 7503.871 | 10813.439 | 11075.583 |
| t-222 | 138 | 1 | 18.598 | 43.285 | 2846 | 0 | 773 | 773 | 0 | 8323.071 | 10682.367 | 11337.727 |
| t-466 | 11 | 1 | 11.858 | 1.968 | 449 | 0 | 45 | 45 | 0 | 6225.919 | 10682.367 | 10682.367 |
| t-306 | 134 | 2 | 22.544 | 50.414 | 2978 | 0 | 876 | 876 | 0 | 7766.015 | 10551.295 | 11927.551 |
| t-162 | 69 | 1 | 3.166 | 3.645 | 353 | 0 | 138 | 138 | 0 | 6225.919 | 10354.687 | 10354.687 |
| t-348 | 125 | 1 | 18.901 | 39.616 | 1340 | 0 | 704 | 704 | 0 | 5898.239 | 10289.151 | 10682.367 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 18 | 18 | 0 | 7864.319 | 10158.079 | 10158.079 |
| t-293 | 46 | 1 | 12.669 | 9.723 | 2741 | 0 | 206 | 206 | 0 | 5996.543 | 10027.007 | 10092.543 |
| t-468 | 61 | 1 | 38.109 | 38.116 | 1645 | 0 | 648 | 648 | 0 | 5931.007 | 9895.935 | 10616.831 |
| t-186 | 34 | 1 | 31.174 | 17.583 | 1259 | 0 | 299 | 299 | 0 | 6553.599 | 9699.327 | 9764.863 |
| t-302 | 12 | 1 | 27.754 | 5.936 | 1189 | 0 | 100 | 100 | 0 | 6062.079 | 9568.255 | 9568.255 |
| t-344 | 195 | 2 | 24.576 | 80.873 | 571 | 0 | 1418 | 1418 | 0 | 6291.455 | 9568.255 | 10223.615 |
| t-152 | 43 | 1 | 11.302 | 7.978 | 882 | 0 | 174 | 174 | 0 | 6062.079 | 9502.719 | 9699.327 |
| t-350 | 85 | 1 | 16.081 | 22.102 | 285 | 0 | 425 | 425 | 0 | 5931.007 | 9371.647 | 9764.863 |
| t-461 | 130 | 1 | 13.509 | 29.389 | 3025 | 0 | 595 | 595 | 0 | 6455.295 | 9371.647 | 9830.399 |
| t-259 | 70 | 1 | 5.41 | 6.194 | 160 | 0 | 196 | 196 | 0 | 5931.007 | 9240.575 | 9240.575 |
| t-386 | 56 | 1 | 3.685 | 3.394 | 3094 | 0 | 113 | 113 | 0 | 6258.687 | 8912.895 | 8912.895 |
| t-398 | 15 | 1 | 11.655 | 3.056 | 482 | 0 | 69 | 69 | 0 | 6127.615 | 8912.895 | 8912.895 |
| t-417 | 75 | 1 | 13.222 | 16.434 | 3449 | 0 | 343 | 343 | 0 | 6062.079 | 8912.895 | 8978.431 |
| t-427 | 16 | 1 | 33.347 | 8.852 | 1711 | 0 | 121 | 121 | 0 | 6062.079 | 8912.895 | 9437.183 |
| t-121 | 76 | 1 | 29.345 | 35.998 | 149 | 0 | 600 | 600 | 0 | 6094.847 | 8847.359 | 9109.503 |
