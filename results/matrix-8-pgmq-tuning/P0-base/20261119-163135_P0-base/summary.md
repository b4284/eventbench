# P0-base — FAIL ❌

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 46.708s, closing 2.66s, drain 18.421s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 564187 | 564187 | 10792.031 | 8426.691 | 10867.1 | 0 | 0 | 0 | 311 | 2871 | 209497 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 394598 | 12629.954 | 11214.847 | 26460.159 | 30310.399 | 32751.615 | 34865.151 |
| e2e_from_send | 394598 | 11844.751 | 10649.599 | 25673.727 | 29638.655 | 32227.327 | 34734.079 |
| producer_lag | 394598 | 785.205 | 351.487 | 2215.935 | 5152.767 | 6402.047 | 6975.487 |
| publish_call | 4983 | 306.789 | 97.599 | 809.471 | 2473.983 | 4175.871 | 5767.167 |
| queue_wait | 394598 | 11746.69 | 10534.911 | 25608.191 | 29573.119 | 32194.559 | 34701.311 |
| broker_dwell | 394598 | 11709.337 | 10502.143 | 25575.423 | 29540.351 | 32161.791 | 34701.311 |
| receive_call | 444083 | 44.159 | 41.951 | 63.007 | 115.967 | 1266.687 | 3033.087 |
| ctx_load | 394598 | 41.652 | 39.199 | 63.967 | 105.727 | 402.687 | 2752.511 |
| process | 394598 | 10.757 | 0.002 | 0.003 | 0.015 | 2623.487 | 3002.367 |
| ctx_commit | 394598 | 44.543 | 39.615 | 65.791 | 125.183 | 1941.503 | 8044.543 |
| service | 394598 | 98.057 | 82.239 | 129.279 | 285.183 | 2850.815 | 10289.151 |
| e2e_large | 211 | 7320.683 | 6672.383 | 11993.087 | 15171.583 | 17317.887 | 17317.887 |
| ready_wait | 394598 | 982.593 | 173.567 | 2828.287 | 8806.399 | 16457.727 | 27754.495 |
| blocked | 394598 | 930.579 | 164.735 | 2684.927 | 8273.919 | 15147.007 | 27328.511 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8426.691 | 10223.4 |
| applied_total | 262757 | 392757 |
| bench_cpu_cores | 2.206 | 2.741 |
| bench_cpu_pct | 9.191 | 11.42 |
| bench_gc_gen0 | 1.457 | 3 |
| bench_gc_gen1 | 0.829 | 3 |
| bench_gc_gen2 | 0.257 | 1 |
| bench_gc_heap_mb | 1636.686 | 2173.3 |
| bench_mem_mb | 1867.04 | 2167.2 |
| bench_threadpool_queue | 6.343 | 41 |
| bench_threadpool_threads | 41.2 | 44 |
| broker_backlog | 82985.2 | 111202 |
| broker_oldest_age_s | 13.829 | 20 |
| broker_visible | 76927.886 | 100328 |
| ctr_eb_pgbouncer_2_cpu_pct | 30.305 | 33.54 |
| ctr_eb_pgbouncer_2_mem_mb | 13.9 | 14.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.772 | 34.7 |
| ctr_eb_pgbouncer_3_mem_mb | 14.149 | 14.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 31.451 | 34.48 |
| ctr_eb_pgbouncer_4_mem_mb | 17.583 | 21.3 |
| ctr_eb_pgbouncer_cpu_pct | 29.78 | 34.65 |
| ctr_eb_pgbouncer_mem_mb | 13.986 | 15.4 |
| ctr_eb_postgres_cpu_pct | 1591.388 | 1710.28 |
| ctr_eb_postgres_mem_mb | 9318.846 | 9870.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 84.769 | 92.37 |
| in_flight | 91544.143 | 129798 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 99.979 | 100 |
| pg_conn_active | 129.829 | 188 |
| pg_conn_idle | 65.343 | 172 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 13 | 28 |
| pg_conn_total | 195.171 | 196 |
| pg_db_size_mb | 2430.994 | 3079.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 30426.24 | 35097.2 |
| pg_tup_del_per_s | 30074.289 | 37847.5 |
| pg_tup_fetch_per_s | 595722.529 | 673043 |
| pg_tup_ins_per_s | 46621.9 | 73006.9 |
| pg_tup_upd_per_s | 25000.643 | 27268.7 |
| pg_wal_mb_per_s | 89.329 | 135.04 |
| pgb_cl_active | 280.229 | 430 |
| pgb_cl_waiting | 125.029 | 358 |
| pgb_maxwait_ms | 16.889 | 46.3 |
| pgb_sv_active | 42.571 | 50 |
| pgb_sv_idle | 5.714 | 24 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 10792.031 | 16386 |
| produced_total | 354301.143 | 522555 |
| publish_mb_per_s | 37.818 | 62.98 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 88,160 records are Completed |
| record_counts | pass | 88,160 records found, 88,160 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 311 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 564,187 events applied by workers, 564,187 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 209,497 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 8274 ms, max 27318 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12043 seq 5: ready 112 ms before start, 112 ms of it with an idle slot in group 385
- [cross_source_blocking] source 12039 seq 12: ready 116 ms before start, 116 ms of it with an idle slot in group 385
- [cross_source_blocking] source 12377 seq 4: ready 132 ms before start, 120 ms of it with an idle slot in group 393
- [cross_source_blocking] source 1977 seq 15: ready 211 ms before start, 211 ms of it with an idle slot in group 57
- [cross_source_blocking] source 6854 seq 4: ready 216 ms before start, 183 ms of it with an idle slot in group 233
- [cross_source_blocking] source 6345 seq 14: ready 204 ms before start, 204 ms of it with an idle slot in group 216
- [cross_source_blocking] source 1974 seq 10: ready 140 ms before start, 140 ms of it with an idle slot in group 57
- [cross_source_blocking] source 12382 seq 6: ready 136 ms before start, 124 ms of it with an idle slot in group 393
- [cross_source_blocking] source 1979 seq 14: ready 108 ms before start, 108 ms of it with an idle slot in group 57
- [cross_source_blocking] source 12358 seq 8: ready 126 ms before start, 105 ms of it with an idle slot in group 392
- [cross_source_blocking] source 2675 seq 4: ready 242 ms before start, 235 ms of it with an idle slot in group 82
- [cross_source_blocking] source 6731 seq 3: ready 172 ms before start, 119 ms of it with an idle slot in group 229
- [cross_source_blocking] source 12395 seq 5: ready 180 ms before start, 163 ms of it with an idle slot in group 393
- [cross_source_blocking] source 2180 seq 6: ready 109 ms before start, 102 ms of it with an idle slot in group 62
- [cross_source_blocking] source 12379 seq 3: ready 164 ms before start, 159 ms of it with an idle slot in group 393
- [cross_source_blocking] source 2669 seq 2: ready 237 ms before start, 231 ms of it with an idle slot in group 82
- [cross_source_blocking] source 10945 seq 6: ready 301 ms before start, 268 ms of it with an idle slot in group 357
- [cross_source_blocking] source 2661 seq 3: ready 161 ms before start, 161 ms of it with an idle slot in group 82
- [cross_source_blocking] source 1982 seq 12: ready 138 ms before start, 110 ms of it with an idle slot in group 57
- [cross_source_blocking] source 1103 seq 9: ready 219 ms before start, 203 ms of it with an idle slot in group 35

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 8281 | 8281 | 0 | 29753.343 | 33816.575 | 34865.151 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 8367 | 8367 | 0 | 27918.335 | 31850.495 | 33816.575 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 4149 | 4149 | 0 | 27787.263 | 31588.351 | 33816.575 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 6063 | 6063 | 0 | 26869.759 | 30539.775 | 31850.495 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 4397 | 4397 | 0 | 27394.047 | 30539.775 | 32374.783 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 11566 | 11566 | 0 | 26738.687 | 30277.631 | 32243.711 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 4434 | 4434 | 0 | 27000.831 | 30146.559 | 31195.135 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 6209 | 6209 | 0 | 26345.471 | 30146.559 | 32243.711 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 4133 | 4133 | 0 | 25952.255 | 29622.271 | 31457.279 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 2094 | 2094 | 0 | 26083.327 | 29491.199 | 30146.559 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 9421 | 9421 | 0 | 25690.111 | 29360.127 | 31195.135 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 2049 | 2049 | 0 | 26083.327 | 29229.055 | 30670.847 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2022 | 2022 | 0 | 25427.967 | 29097.983 | 29753.343 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2014 | 2014 | 0 | 24903.679 | 29097.983 | 29884.415 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2015 | 2015 | 0 | 25296.895 | 28835.839 | 29360.127 |
| t-383 | 200 | 6 | 77.529 | 257.86 | 2521 | 0 | 11178 | 11178 | 0 | 25034.751 | 28704.767 | 30539.775 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4151 | 4151 | 0 | 25165.823 | 28573.695 | 29229.055 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5780 | 5780 | 0 | 24510.463 | 28442.623 | 30146.559 |
| t-194 | 78 | 3 | 98.314 | 131.221 | 1184 | 0 | 5469 | 5469 | 0 | 24510.463 | 28311.551 | 30539.775 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2027 | 2027 | 0 | 23855.103 | 28180.479 | 28966.911 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2008 | 2008 | 0 | 24772.607 | 28180.479 | 29229.055 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2123 | 2123 | 0 | 24510.463 | 28180.479 | 29097.983 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2093 | 2093 | 0 | 23986.175 | 28180.479 | 28573.695 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 6061 | 6061 | 0 | 24379.391 | 28180.479 | 29753.343 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3813 | 3813 | 0 | 24248.319 | 28180.479 | 29884.415 |
