# pool-128 — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 400)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 400), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 1
- Topology: 20 groups, 32000 sources, 240 workers, offered 8000 ev/s
- Phases: run 60.102s, closing 0.346s, drain 5.629s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 551679 | 551679 | 7989.165 | 7989.245 | 21510 | 0 | 0 | 0 | 12 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 399383 | 11.325 | 10.503 | 16.511 | 25.535 | 35.583 | 65.599 |
| e2e_from_send | 399383 | 10.203 | 9.263 | 15.351 | 24.415 | 34.431 | 65.023 |
| producer_lag | 399383 | 1.122 | 0.983 | 2.381 | 3.767 | 5.919 | 13.775 |
| publish_call | 295579 | 2.45 | 2.195 | 3.379 | 7.259 | 14.543 | 693.247 |
| queue_wait | 399383 | 4.905 | 4.631 | 6.835 | 9.751 | 14.007 | 40.255 |
| broker_dwell | 399383 | 3.828 | 3.561 | 5.679 | 8.447 | 12.599 | 38.559 |
| receive_call | 254970 | 4.192 | 2.453 | 4.055 | 11.023 | 468.735 | 1388.543 |
| ctx_load | 399383 | 0.781 | 0.636 | 1.423 | 2.493 | 3.913 | 12.703 |
| process | 399383 | 0.002 | 0.001 | 0.002 | 0.004 | 0.02 | 7.407 |
| ctx_commit | 399383 | 2.074 | 1.979 | 2.915 | 4.263 | 6.263 | 15.079 |
| service | 399383 | 5.298 | 4.005 | 10.247 | 19.247 | 28.831 | 58.719 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 7989.245 | 8242.3 |
| applied_total | 302610.714 | 494466 |
| bench_cpu_cores | 2.525 | 2.645 |
| bench_cpu_pct | 10.52 | 11.02 |
| bench_gc_gen0 | 3.571 | 4 |
| bench_gc_gen1 | 2.857 | 4 |
| bench_gc_gen2 | 0.02 | 1 |
| bench_gc_heap_mb | 956.753 | 1077.4 |
| bench_mem_mb | 738.671 | 897.2 |
| bench_threadpool_queue | 2.163 | 29 |
| bench_threadpool_threads | 25.327 | 27 |
| broker_backlog | 65.592 | 80 |
| broker_oldest_age_s | 0 | 0 |
| broker_visible | 9.633 | 25 |
| ctr_eb_pgbouncer_cpu_pct | 76.754 | 79.86 |
| ctr_eb_pgbouncer_mem_mb | 15.961 | 17.3 |
| ctr_eb_postgres_cpu_pct | 1043.72 | 1149.42 |
| ctr_eb_postgres_mem_mb | 1857.404 | 1867.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 55.887 | 63.19 |
| in_flight | 66.571 | 110 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 25.122 | 50 |
| pg_conn_idle | 104.102 | 121 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.061 | 1 |
| pg_conn_total | 129.224 | 130 |
| pg_db_size_mb | 312.524 | 418.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 36685.28 | 40738.4 |
| pg_tup_del_per_s | 8078.806 | 10546.1 |
| pg_tup_fetch_per_s | 189004.12 | 215288 |
| pg_tup_ins_per_s | 9429.192 | 14000.1 |
| pg_tup_upd_per_s | 21025.394 | 23100.2 |
| pg_wal_mb_per_s | 21.093 | 29.46 |
| pgb_cl_active | 419 | 419 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 40.327 | 82 |
| pgb_sv_idle | 86.653 | 108 |
| pgb_sv_used | 1.02 | 4 |
| produced_per_s | 7989.165 | 8251.3 |
| produced_total | 302677.286 | 494545 |
| publish_mb_per_s | 4.122 | 4.26 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 32000/32000 source cursors at the expected last sequence |
| records_complete | pass | all 105,164 records are Completed |
| record_counts | pass | 105,164 records found, 105,164 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 12 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 551,679 events applied by workers, 551,679 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| A-0 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40008 | 40008 | 0 | 10.623 | 26.367 | 52.735 |
| A-1 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40120 | 40120 | 0 | 10.623 | 26.879 | 55.551 |
| A-2 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39307 | 39307 | 0 | 10.559 | 26.239 | 48.127 |
| A-3 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 39875 | 39875 | 0 | 10.623 | 26.495 | 52.479 |
| A-4 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40172 | 40172 | 0 | 10.559 | 26.367 | 66.047 |
| A-5 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40079 | 40079 | 0 | 10.751 | 26.879 | 65.279 |
| A-6 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40015 | 40015 | 0 | 10.559 | 26.879 | 51.967 |
| A-7 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40065 | 40065 | 0 | 10.559 | 26.367 | 51.967 |
| A-8 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40127 | 40127 | 0 | 10.623 | 26.751 | 51.967 |
| A-9 | 1600 | 12 | 22.5 | 600 | 256 | 0 | 40434 | 40434 | 0 | 10.559 | 26.751 | 52.479 |
| B-0 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15052 | 15052 | 0 | 10.303 | 20.863 | 34.047 |
| B-1 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.367 | 20.607 | 34.047 |
| B-2 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15083 | 15083 | 0 | 10.303 | 20.735 | 40.703 |
| B-3 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15232 | 15232 | 0 | 10.431 | 20.991 | 41.727 |
| B-4 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15207 | 15207 | 0 | 10.367 | 21.375 | 35.327 |
| B-5 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15105 | 15105 | 0 | 10.303 | 19.967 | 33.535 |
| B-6 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15074 | 15074 | 0 | 10.367 | 20.863 | 41.983 |
| B-7 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15345 | 15345 | 0 | 10.239 | 20.991 | 37.119 |
| B-8 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15167 | 15167 | 0 | 10.303 | 20.863 | 47.871 |
| B-9 | 1600 | 12 | 7.5 | 200 | 256 | 0 | 15107 | 15107 | 0 | 10.303 | 21.631 | 33.279 |
