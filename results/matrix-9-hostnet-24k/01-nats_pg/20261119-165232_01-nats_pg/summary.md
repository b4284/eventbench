# 01-nats_pg — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.286s, closing 1.841s, drain 11.421s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1484215 | 1484215 | 24575.165 | 21548.457 | 35055.7 | 0 | 0 | 0 | 424 | 7498 | 759429 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1198822 | 6822.23 | 7041.023 | 14319.615 | 19087.359 | 22167.551 | 26296.319 |
| e2e_from_send | 1198822 | 6527.414 | 6639.615 | 14098.431 | 18907.135 | 21905.407 | 26181.631 |
| producer_lag | 1198822 | 294.814 | 141.567 | 674.815 | 2791.423 | 3631.103 | 4036.607 |
| publish_call | 6871 | 286.508 | 230.527 | 543.231 | 884.223 | 1104.895 | 1464.319 |
| queue_wait | 1198822 | 6489.659 | 6610.943 | 14049.279 | 18825.215 | 21725.183 | 24068.095 |
| broker_dwell | 1198822 | 6407.962 | 6512.639 | 13967.359 | 18743.295 | 21626.879 | 23986.175 |
| receive_call | 846586 | 57.981 | 49.311 | 86.591 | 287.743 | 705.023 | 2715.647 |
| ctx_load | 1198822 | 1.2 | 0.634 | 2.615 | 8.751 | 20.319 | 55.967 |
| process | 1198822 | 10.394 | 0.001 | 0.002 | 0.006 | 2613.247 | 3002.367 |
| ctx_commit | 1198822 | 1.235 | 0.639 | 2.593 | 9.367 | 24.143 | 72.767 |
| ack | 1165498 | 0.142 | 0.002 | 0.009 | 2.127 | 25.455 | 195.455 |
| service | 1198822 | 37.755 | 1.713 | 9.575 | 1505.279 | 2775.039 | 8019.967 |
| e2e_large | 323 | 2713.494 | 2603.007 | 3835.903 | 5935.103 | 7741.439 | 7741.439 |
| ready_wait | 1198822 | 409.661 | 141.695 | 1062.911 | 3414.015 | 6483.967 | 17743.871 |
| blocked | 1198822 | 409.466 | 141.567 | 1061.887 | 3414.015 | 6483.967 | 17743.871 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 21548.457 | 23477.7 |
| applied_total | 716224.776 | 1241698 |
| bench_cpu_cores | 4.841 | 5.473 |
| bench_cpu_pct | 20.169 | 22.81 |
| bench_gc_gen0 | 3.122 | 4 |
| bench_gc_gen1 | 2.878 | 4 |
| bench_gc_gen2 | 0.245 | 1 |
| bench_gc_heap_mb | 1215.065 | 1489.1 |
| bench_mem_mb | 1844.886 | 1966.5 |
| bench_threadpool_queue | 21.367 | 443 |
| bench_threadpool_threads | 41.878 | 44 |
| broker_backlog | 142273.796 | 201975 |
| broker_backlog_mb | 322.328 | 445.656 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_nats_cpu_pct | 411.615 | 429.42 |
| ctr_eb_nats_mem_mb | 4125.922 | 5459.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 31.663 | 33.95 |
| ctr_eb_pgbouncer_2_mem_mb | 13.353 | 14.4 |
| ctr_eb_pgbouncer_3_cpu_pct | 30.927 | 32.78 |
| ctr_eb_pgbouncer_3_mem_mb | 12.818 | 13.4 |
| ctr_eb_pgbouncer_4_cpu_pct | 32.084 | 33.62 |
| ctr_eb_pgbouncer_4_mem_mb | 13.171 | 15 |
| ctr_eb_pgbouncer_cpu_pct | 29.991 | 31.99 |
| ctr_eb_pgbouncer_mem_mb | 12.941 | 14.8 |
| ctr_eb_postgres_cpu_pct | 501.811 | 527.4 |
| ctr_eb_postgres_mem_mb | 2270.341 | 3164.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 64.922 | 69.37 |
| in_flight | 139460.714 | 197672 |
| nacks_per_s | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 6.082 | 35 |
| pg_conn_idle | 185.918 | 192 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0 | 0 |
| pg_conn_total | 192 | 192 |
| pg_db_size_mb | 837.733 | 1284 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 42473.92 | 46273.3 |
| pg_tup_del_per_s | 0 | 0 |
| pg_tup_fetch_per_s | 98845.053 | 109851.8 |
| pg_tup_ins_per_s | 11880.527 | 18378.7 |
| pg_tup_upd_per_s | 35193.929 | 38497 |
| pg_wal_mb_per_s | 42.33 | 54.62 |
| pgb_cl_active | 247.041 | 259 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 5.061 | 32 |
| pgb_sv_idle | 42.939 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 24575.165 | 27427.5 |
| produced_total | 855685.49 | 1439370 |
| publish_mb_per_s | 65.634 | 89.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 193,537 records are Completed |
| record_counts | pass | 193,537 records found, 193,537 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 424 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,484,215 events applied by workers, 1,484,215 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 759,429 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 3414 ms, max 17729 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 2172 seq 9: ready 104 ms before start, 104 ms of it with an idle slot in group 62
- [cross_source_blocking] source 14076 seq 18: ready 105 ms before start, 105 ms of it with an idle slot in group 449
- [cross_source_blocking] source 7260 seq 9: ready 124 ms before start, 124 ms of it with an idle slot in group 248
- [cross_source_blocking] source 15740 seq 10: ready 139 ms before start, 139 ms of it with an idle slot in group 491
- [cross_source_blocking] source 4476 seq 9: ready 149 ms before start, 149 ms of it with an idle slot in group 154
- [cross_source_blocking] source 946 seq 5: ready 111 ms before start, 111 ms of it with an idle slot in group 28
- [cross_source_blocking] source 8914 seq 10: ready 137 ms before start, 137 ms of it with an idle slot in group 307
- [cross_source_blocking] source 5025 seq 17: ready 145 ms before start, 145 ms of it with an idle slot in group 175
- [cross_source_blocking] source 5763 seq 6: ready 107 ms before start, 107 ms of it with an idle slot in group 199
- [cross_source_blocking] source 5761 seq 4: ready 159 ms before start, 159 ms of it with an idle slot in group 199
- [cross_source_blocking] source 4796 seq 13: ready 192 ms before start, 192 ms of it with an idle slot in group 167
- [cross_source_blocking] source 12892 seq 12: ready 193 ms before start, 193 ms of it with an idle slot in group 408
- [cross_source_blocking] source 7090 seq 24: ready 166 ms before start, 166 ms of it with an idle slot in group 240
- [cross_source_blocking] source 12082 seq 7: ready 166 ms before start, 166 ms of it with an idle slot in group 386
- [cross_source_blocking] source 2937 seq 14: ready 104 ms before start, 104 ms of it with an idle slot in group 94
- [cross_source_blocking] source 2745 seq 24: ready 104 ms before start, 104 ms of it with an idle slot in group 84
- [cross_source_blocking] source 11609 seq 13: ready 101 ms before start, 101 ms of it with an idle slot in group 377
- [cross_source_blocking] source 11289 seq 26: ready 109 ms before start, 109 ms of it with an idle slot in group 367
- [cross_source_blocking] source 2649 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 82
- [cross_source_blocking] source 1881 seq 11: ready 114 ms before start, 114 ms of it with an idle slot in group 54

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 15066 | 15066 | 0 | 12713.983 | 23855.103 | 26345.471 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 6472 | 6472 | 0 | 16318.463 | 23592.959 | 26345.471 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 11914 | 11914 | 0 | 12582.911 | 23330.815 | 24379.391 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 5754 | 5754 | 0 | 13631.487 | 21495.807 | 23461.887 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 15192 | 15192 | 0 | 13959.167 | 21364.735 | 23855.103 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 9073 | 9073 | 0 | 13697.023 | 21102.591 | 23330.815 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 22439 | 22439 | 0 | 10878.975 | 21102.591 | 22675.455 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 3048 | 3048 | 0 | 15728.639 | 20971.519 | 21626.879 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 5862 | 5862 | 0 | 11862.015 | 20971.519 | 22413.311 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 25360 | 25360 | 0 | 10551.295 | 20840.447 | 24248.319 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 9107 | 9107 | 0 | 13893.631 | 20840.447 | 22937.599 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 17960 | 17960 | 0 | 13369.343 | 20709.375 | 22937.599 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 3089 | 3089 | 0 | 15073.279 | 20578.303 | 22020.095 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 29402 | 29402 | 0 | 12976.127 | 20447.231 | 22544.383 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 14995 | 14995 | 0 | 13041.663 | 20316.159 | 22151.167 |
| t-340 | 184 | 3 | 45.82 | 141.132 | 141 | 0 | 9111 | 9111 | 0 | 13041.663 | 19660.799 | 20578.303 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 3056 | 3056 | 0 | 13303.807 | 19529.727 | 20840.447 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5508 | 5508 | 0 | 10289.151 | 19529.727 | 21495.807 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 14304 | 14304 | 0 | 11337.727 | 19267.583 | 20971.519 |
| t-488 | 26 | 1 | 107.861 | 47.761 | 1017 | 0 | 2926 | 2926 | 0 | 14352.383 | 19267.583 | 20840.447 |
| t-376 | 65 | 1 | 43.578 | 48.459 | 566 | 0 | 3182 | 3182 | 0 | 13172.735 | 19005.439 | 20971.519 |
| t-235 | 54 | 3 | 148.429 | 133.613 | 527 | 0 | 8021 | 8021 | 0 | 9109.503 | 18743.295 | 19267.583 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 22164 | 22164 | 0 | 10813.439 | 18612.223 | 19791.871 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 5849 | 5849 | 0 | 13172.735 | 18612.223 | 20709.375 |
| t-425 | 89 | 5 | 155.428 | 227.21 | 2911 | 0 | 13668 | 13668 | 0 | 9961.471 | 18612.223 | 21102.591 |
