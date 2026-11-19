# D2-pgqueue-24k-sync-KILL-PRIMARY — FAIL ❌

- Broker: `pgqueue table ebq_msg (16 hash partitions) @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench; head reads without vt writes, notify=True, fallback 200 ms, 64 groups/fetch x 4, preload=True`
- Store: `postgres @ 127.0.0.1:5432,127.0.0.1:5433:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.451s, closing 0.322s, drain 13.419s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1480688 | 1480688 | 23950.284 | 21512.271 | 28873.3 | 0 | 648 | 648 | 422 | 7473 | 412423 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1195301 | 2683.039 | 712.191 | 8019.967 | 12902.399 | 13983.743 | 18907.135 |
| e2e_from_send | 1195301 | 2457.317 | 495.871 | 7872.511 | 12689.407 | 13811.711 | 18317.311 |
| producer_lag | 1195301 | 225.722 | 40.703 | 469.247 | 2820.095 | 3237.887 | 3405.823 |
| publish_call | 27256 | 70.959 | 47.967 | 105.791 | 408.063 | 3221.503 | 3405.823 |
| queue_wait | 1195301 | 2410.49 | 422.143 | 7806.975 | 12656.639 | 13754.367 | 18120.703 |
| broker_dwell | 1195301 | 2401.19 | 406.783 | 7802.879 | 12648.447 | 13746.175 | 18120.703 |
| receive_call | 939910 | 47.236 | 37.151 | 79.295 | 251.391 | 451.839 | 503.295 |
| ctx_load | 1195301 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 5.295 |
| process | 1195301 | 10.383 | 0.001 | 0.002 | 0.004 | 2613.247 | 3002.367 |
| ctx_commit | 1195301 | 23.468 | 11.575 | 57.471 | 148.351 | 429.055 | 470.015 |
| service | 1195301 | 46.83 | 26.863 | 76.863 | 224.127 | 2646.015 | 3221.503 |
| e2e_large | 321 | 2820.404 | 2615.295 | 4358.143 | 6410.239 | 8634.367 | 8634.367 |
| ready_wait | 1195301 | 267.488 | 43.231 | 783.359 | 2713.599 | 5062.655 | 12345.343 |
| blocked | 1195301 | 241.216 | 43.135 | 703.999 | 2367.487 | 4415.487 | 11018.239 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 21512.271 | 28828.8 |
| applied_total | 816501.408 | 1319816 |
| bench_cpu_cores | 1.668 | 2.047 |
| bench_cpu_pct | 6.951 | 8.53 |
| bench_gc_gen0 | 2.327 | 4 |
| bench_gc_gen1 | 2.041 | 4 |
| bench_gc_gen2 | 0.306 | 1 |
| bench_gc_heap_mb | 781.657 | 973.8 |
| bench_mem_mb | 1088.143 | 1182.5 |
| bench_threadpool_queue | 3.184 | 64 |
| bench_threadpool_threads | 36.143 | 38 |
| broker_backlog | 42876.224 | 120278 |
| ctr_eb_pg_standby_cpu_pct | 380.368 | 832.93 |
| ctr_eb_pg_standby_mem_mb | 2752.896 | 3509.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.014 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 10.9 | 10.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 9.3 | 9.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 11.8 | 11.8 |
| ctr_eb_postgres_cpu_pct | 799.395 | 826.7 |
| ctr_eb_postgres_mem_mb | 4571.635 | 4645.3 |
| dispatch_empty_reads_per_s | 3204.512 | 6307.9 |
| dispatch_fetch_calls_per_s | 293.396 | 424 |
| dispatch_group_reads_per_s | 12280.549 | 18024.3 |
| dispatch_in_flight | 857.878 | 2714 |
| dispatch_notifies_per_s | 18274.155 | 21861.9 |
| dispatch_preloaded_per_s | 21865.873 | 27818.5 |
| dispatch_rows_per_s | 21865.961 | 27738.3 |
| dispatch_waiting_receives | 714.939 | 780 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 13.216 | 511.7 |
| host_cpu_pct | 39.409 | 49.73 |
| in_flight | 44902.571 | 121164 |
| nacks_per_s | 13.216 | 511.7 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.898 | 9 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.994 | 100 |
| pg_conn_active | 40.816 | 45 |
| pg_conn_idle | 6.551 | 13 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 27.959 | 31 |
| pg_conn_total | 47.367 | 50 |
| pg_db_size_mb | 3396.733 | 5110 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 0.296 | 0.47 |
| pg_replica_replay_lag_ms | 1.788 | 3.8 |
| pg_replicas | 1 | 1 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 13.725 | 51.16 |
| pg_tps | 927.982 | 6850 |
| pg_tup_del_per_s | 22678.331 | 74696.8 |
| pg_tup_fetch_per_s | 1010582.998 | 2615161.1 |
| pg_tup_ins_per_s | 29106.053 | 116791.1 |
| pg_tup_upd_per_s | 16030.837 | 46396.1 |
| pg_wal_mb_per_s | 99.173 | 262.89 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 23950.284 | 86149.4 |
| produced_total | 861403.98 | 1440980 |
| publish_mb_per_s | 63.763 | 219.24 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,136 records are Completed |
| record_counts | pass | 193,136 records found, 193,136 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 422 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,480,688 events applied by workers, 1,480,688 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 412,423 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 2367 ms, max 11011 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8090 seq 40: ready 174 ms before start, 173 ms of it with an idle slot in group 272
- [cross_source_blocking] source 3119 seq 9: ready 177 ms before start, 155 ms of it with an idle slot in group 104
- [cross_source_blocking] source 8088 seq 49: ready 150 ms before start, 149 ms of it with an idle slot in group 272
- [cross_source_blocking] source 3126 seq 14: ready 183 ms before start, 161 ms of it with an idle slot in group 104
- [cross_source_blocking] source 3117 seq 8: ready 200 ms before start, 165 ms of it with an idle slot in group 104
- [cross_source_blocking] source 3136 seq 7: ready 219 ms before start, 184 ms of it with an idle slot in group 104
- [cross_source_blocking] source 3123 seq 12: ready 230 ms before start, 195 ms of it with an idle slot in group 104
- [cross_source_blocking] source 8080 seq 28: ready 114 ms before start, 103 ms of it with an idle slot in group 272
- [cross_source_blocking] source 14064 seq 11: ready 116 ms before start, 101 ms of it with an idle slot in group 447
- [cross_source_blocking] source 693 seq 26: ready 288 ms before start, 166 ms of it with an idle slot in group 20
- [cross_source_blocking] source 3137 seq 13: ready 134 ms before start, 121 ms of it with an idle slot in group 104
- [cross_source_blocking] source 14057 seq 18: ready 149 ms before start, 136 ms of it with an idle slot in group 447
- [cross_source_blocking] source 3127 seq 14: ready 150 ms before start, 138 ms of it with an idle slot in group 104
- [cross_source_blocking] source 3133 seq 8: ready 181 ms before start, 156 ms of it with an idle slot in group 104
- [cross_source_blocking] source 12926 seq 17: ready 110 ms before start, 100 ms of it with an idle slot in group 409
- [cross_source_blocking] source 6076 seq 23: ready 118 ms before start, 103 ms of it with an idle slot in group 208
- [cross_source_blocking] source 682 seq 14: ready 136 ms before start, 102 ms of it with an idle slot in group 20
- [cross_source_blocking] source 14063 seq 16: ready 164 ms before start, 154 ms of it with an idle slot in group 447
- [cross_source_blocking] source 3134 seq 15: ready 178 ms before start, 155 ms of it with an idle slot in group 104
- [cross_source_blocking] source 3118 seq 11: ready 175 ms before start, 153 ms of it with an idle slot in group 104

## Error samples

- publish p15 attempt 1: NpgsqlException: Exception while reading from stream
- publish p5 attempt 1: NpgsqlException: Exception while reading from stream
- publish p25 attempt 1: NpgsqlException: Exception while reading from stream
- publish p24 attempt 1: NpgsqlException: Exception while reading from stream
- publish p2 attempt 1: NpgsqlException: Exception while reading from stream
- publish p31 attempt 1: NpgsqlException: Exception while reading from stream
- publish p20 attempt 1: NpgsqlException: Exception while reading from stream
- publish p12 attempt 1: NpgsqlException: Exception while reading from stream
- publish p21 attempt 1: NpgsqlException: Exception while reading from stream
- publish p8 attempt 1: NpgsqlException: Exception while reading from stream
- t-045#0 src=1498 seq=99 op=Update rec=6433861009420 ev=1: NpgsqlException: No suitable host was found.
- t-119L#3 src=3485 seq=4 op=Update rec=14967961026561 ev=3: NpgsqlException: No suitable host was found.
- t-263#2 src=7705 seq=71 op=Update rec=33092723015688 ev=2: NpgsqlException: No suitable host was found.
- t-394#0 src=12399 seq=32 op=Update rec=53253299503109 ev=3: NpgsqlException: No suitable host was found.
- t-348#3 src=10613 seq=50 op=Update rec=45582487912457 ev=1: NpgsqlException: No suitable host was found.
- t-049#1 src=1641 seq=37 op=Get rec=7048041332740 ev=2: NpgsqlException: No suitable host was found.
- t-306#1 src=8846 seq=92 op=Update rec=37993280700430 ev=1: NpgsqlException: No suitable host was found.
- t-397#0 src=12639 seq=15 op=Update rec=54284091654146 ev=2: NpgsqlException: No suitable host was found.
- t-334#0 src=9653 seq=55 op=Update rec=41459319308297 ev=6: NpgsqlException: No suitable host was found.
- t-056#0 src=1967 seq=68 op=Get rec=8448200671237 ev=7: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-465 | 66 | 3 | 132.285 | 142.732 | 667 | 0 | 8794 | 8794 | 0 | 5570.559 | 14417.919 | 16187.391 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15021 | 15021 | 0 | 5406.719 | 14286.847 | 16646.143 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14260 | 14260 | 0 | 5275.647 | 13959.167 | 16777.215 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15164 | 15164 | 0 | 5013.503 | 13893.631 | 16383.999 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9059 | 9059 | 0 | 5603.327 | 13697.023 | 16449.535 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9079 | 9079 | 0 | 5406.719 | 13434.879 | 15859.711 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3172 | 3172 | 0 | 5308.415 | 13369.343 | 14614.527 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5495 | 5495 | 0 | 4784.127 | 13369.343 | 15663.103 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17922 | 17922 | 0 | 5144.575 | 13238.271 | 19005.439 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2915 | 2915 | 0 | 4784.127 | 13238.271 | 15204.351 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 30610 | 30610 | 0 | 5275.647 | 13172.735 | 15794.175 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29332 | 29332 | 0 | 5210.111 | 13041.663 | 15663.103 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 8469 | 8469 | 0 | 4521.983 | 12386.303 | 14876.671 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9089 | 9089 | 0 | 5079.039 | 12320.767 | 14811.135 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 14552 | 14552 | 0 | 4816.895 | 12320.767 | 14352.383 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22394 | 22394 | 0 | 4882.431 | 12320.767 | 15204.351 |
| t-354 | 90 | 2 | 65.114 | 97.451 | 157 | 0 | 6120 | 6120 | 0 | 4947.967 | 12255.231 | 14155.775 |
| t-461 | 130 | 4 | 84.209 | 183.191 | 3025 | 0 | 11214 | 11214 | 0 | 5177.343 | 12255.231 | 14024.703 |
| t-382 | 59 | 1 | 44.037 | 43.77 | 2368 | 0 | 2820 | 2820 | 0 | 5963.775 | 12124.159 | 13369.343 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 5835 | 5835 | 0 | 5144.575 | 12058.623 | 14155.775 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 19406 | 19406 | 0 | 4521.983 | 11993.087 | 14090.239 |
| t-460 | 71 | 1 | 38.387 | 45.799 | 141 | 0 | 3040 | 3040 | 0 | 5013.503 | 11665.407 | 11993.087 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5746 | 5746 | 0 | 4325.375 | 11534.335 | 13369.343 |
| t-439 | 22 | 1 | 127.568 | 47.568 | 3339 | 0 | 2805 | 2805 | 0 | 5603.327 | 11468.799 | 11730.943 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 14961 | 14961 | 0 | 4521.983 | 11403.263 | 14221.311 |
