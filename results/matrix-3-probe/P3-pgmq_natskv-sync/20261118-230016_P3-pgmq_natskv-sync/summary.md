# P3-pgmq_natskv-sync — FAIL ❌

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 20.104s, closing 0.243s, drain 20.001s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 293400 | 20886 | 12881.929 | 408.179 | 873.8 | 0 | 0 | 0 | 195 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1442 | 22482.205 | 22953.983 | 31391.743 | 34045.951 | 34799.615 | 35160.063 |
| e2e_from_send | 1442 | 22479.557 | 22953.983 | 31358.975 | 34045.951 | 34799.615 | 35160.063 |
| producer_lag | 193162 | 2.31 | 0.846 | 2.789 | 49.311 | 102.655 | 144.511 |
| publish_call | 173542 | 2.131 | 1.642 | 2.629 | 8.439 | 91.327 | 228.607 |
| queue_wait | 1442 | 17943.239 | 18825.215 | 27705.343 | 31277.055 | 32931.839 | 33685.503 |
| broker_dwell | 1442 | 17941.723 | 18825.215 | 27705.343 | 31277.055 | 32931.839 | 33685.503 |
| receive_call | 3156 | 20.222 | 9.159 | 25.567 | 273.151 | 1035.775 | 1036.287 |
| ctx_load | 1442 | 209.998 | 181.247 | 364.031 | 492.031 | 682.495 | 851.455 |
| process | 1442 | 0.016 | 0.001 | 0.003 | 0.365 | 0.528 | 0.568 |
| ctx_commit | 1442 | 706.462 | 738.303 | 1058.815 | 1561.599 | 2033.663 | 2033.663 |
| ack | 1442 | 8.315 | 7.411 | 15.383 | 20.863 | 24.623 | 24.959 |
| service | 1442 | 4536.419 | 4313.087 | 8101.887 | 10452.991 | 11870.207 | 12697.599 |
| e2e_large | 80 | 18507.578 | 20201.471 | 28016.639 | 30556.159 | 31916.031 | 31916.031 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 408.179 | 772.2 |
| applied_total | 4902.286 | 7752 |
| bench_cpu_cores | 2.305 | 2.668 |
| bench_cpu_pct | 9.605 | 11.12 |
| bench_gc_gen0 | 1.214 | 2 |
| bench_gc_gen1 | 0.5 | 1 |
| bench_gc_gen2 | 0.5 | 1 |
| bench_gc_heap_mb | 882.043 | 1151.1 |
| bench_mem_mb | 1067.993 | 1236.9 |
| bench_threadpool_queue | 0.643 | 8 |
| bench_threadpool_threads | 43.714 | 44 |
| broker_backlog | 161818.429 | 246812 |
| broker_oldest_age_s | 12.143 | 19 |
| broker_visible | 158025.5 | 241868 |
| ctr_eb_kafka_cpu_pct | 1.67 | 2.15 |
| ctr_eb_kafka_mem_mb | 2838.457 | 3083.2 |
| ctr_eb_mongo_cpu_pct | 0.968 | 1.1 |
| ctr_eb_mongo_mem_mb | 2731.336 | 2757.1 |
| ctr_eb_nats_cpu_pct | 15.047 | 16.6 |
| ctr_eb_nats_mem_mb | 3143.814 | 3151.9 |
| ctr_eb_pgbouncer_cpu_pct | 32.338 | 36.31 |
| ctr_eb_pgbouncer_mem_mb | 11.5 | 12.5 |
| ctr_eb_postgres_cpu_pct | 477.993 | 573.4 |
| ctr_eb_postgres_mem_mb | 3910.086 | 4000.7 |
| ctr_eb_rabbitmq_cpu_pct | 11.469 | 12.53 |
| ctr_eb_rabbitmq_mem_mb | 731.336 | 739.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 30.121 | 34.44 |
| in_flight | 175096.214 | 255918 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12881.929 | 13126.6 |
| produced_total | 179998.5 | 263670 |
| publish_mb_per_s | 56.764 | 83.06 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 69/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 11,607 records not Completed |
| record_counts | **FAIL** | 12,666 records found, 54,353 expected; 15931 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | **FAIL** | 123 uploads verified, 54 sources mismatched |
| applied_equals_generated | **FAIL** | 20,886 events applied by workers, 293,400 generated |
| broker_drained | **FAIL** | 272,527 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

Sample mismatches:

- source 10859: record 46639049867265 left in status Open (version 1)
- source 4611: record 19804094201857 left in status Open (version 1)
- source 5166: record 22187801051137 left in status Open (version 1)
- source 14808: record 63599875719169 left in status Open (version 1)
- source 13377: record 57453777518593 left in status Open (version 1)
- source 9978: record 42855183679489 left in status Open (version 1)
- source 14461: record 62109522067457 left in status Open (version 1)
- source 13720: record 58926951301121 left in status Open (version 1)
- source 2501: record 10741713207297 left in status Open (version 1)
- source 88: record 377957122049 left in status Open (version 1)
- source 10377: record 44568875630593 left in status Open (version 1)
- source 771: record 3311419785217 left in status Open (version 1)
- source 8452: record 36301063585793 left in status Open (version 1)
- source 5656: record 24292335026177 left in status Open (version 1)
- source 13321: record 57213259350017 left in status Open (version 1)
- source 3561: record 15294378541057 left in status Open (version 1)
- source 15165: record 65133179043841 left in status Open (version 1)
- source 1283: record 5510443040769 left in status Open (version 1)
- source 15765: record 67710159421441 left in status Open (version 1)
- source 13909: record 59738700120065 left in status Open (version 1)

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-006 | 14 | 1 | 25.112 | 5.603 | 1170 | 0 | 142 | 34 | 0 | 31195.135 | 35389.439 | 35389.439 |
| t-013 | 10 | 1 | 36.707 | 5.57 | 134 | 0 | 133 | 37 | 0 | 27000.831 | 34865.151 | 34865.151 |
| t-101 | 6 | 1 | 31.723 | 2.854 | 198 | 0 | 77 | 35 | 0 | 20054.015 | 34865.151 | 34865.151 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 136 | 36 | 0 | 26869.759 | 34865.151 | 34865.151 |
| t-370 | 12 | 1 | 20.006 | 4.067 | 568 | 0 | 97 | 33 | 0 | 27787.263 | 34865.151 | 34865.151 |
| t-012 | 6 | 1 | 61.836 | 6.408 | 556 | 0 | 150 | 36 | 0 | 30277.631 | 34603.007 | 34603.007 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 128 | 36 | 0 | 33816.575 | 34603.007 | 34603.007 |
| t-056 | 7 | 1 | 48.226 | 5.826 | 347 | 0 | 123 | 38 | 0 | 24248.319 | 34340.863 | 34340.863 |
| t-330 | 9 | 1 | 46.556 | 6.482 | 2607 | 0 | 156 | 35 | 0 | 27131.903 | 34340.863 | 34340.863 |
| t-484 | 6 | 1 | 74.942 | 7.586 | 232 | 0 | 160 | 36 | 0 | 29360.127 | 34340.863 | 34340.863 |
| t-485 | 12 | 1 | 32.031 | 6.197 | 880 | 0 | 147 | 40 | 0 | 33423.359 | 34340.863 | 34340.863 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 170 | 41 | 0 | 28573.695 | 34078.719 | 34078.719 |
| t-226 | 6 | 1 | 87.952 | 9.302 | 623 | 0 | 193 | 37 | 0 | 23199.743 | 34078.719 | 34078.719 |
| t-289 | 6 | 1 | 67.249 | 7.205 | 1711 | 0 | 150 | 37 | 0 | 29622.271 | 34078.719 | 34078.719 |
| t-324 | 15 | 1 | 22.024 | 5.117 | 131 | 0 | 124 | 35 | 0 | 29622.271 | 34078.719 | 34078.719 |
| t-463 | 12 | 1 | 19.371 | 3.924 | 2195 | 0 | 114 | 32 | 0 | 30277.631 | 34078.719 | 34078.719 |
| t-090 | 7 | 1 | 39.93 | 4.697 | 242 | 0 | 104 | 37 | 0 | 27787.263 | 33816.575 | 33816.575 |
| t-138 | 9 | 1 | 30.7 | 4.513 | 228 | 0 | 114 | 37 | 0 | 25034.751 | 33816.575 | 33816.575 |
| t-148 | 6 | 1 | 62.881 | 5.748 | 715 | 0 | 139 | 35 | 0 | 24248.319 | 33816.575 | 33816.575 |
| t-156 | 9 | 1 | 36.877 | 5.434 | 968 | 0 | 122 | 36 | 0 | 26869.759 | 33816.575 | 33816.575 |
| t-158 | 6 | 1 | 43.525 | 4.603 | 165 | 0 | 99 | 34 | 0 | 29360.127 | 33816.575 | 33816.575 |
| t-244 | 11 | 1 | 32 | 5.972 | 1151 | 0 | 125 | 35 | 0 | 26476.543 | 33816.575 | 33816.575 |
| t-384 | 12 | 1 | 22.317 | 4.426 | 844 | 0 | 109 | 35 | 0 | 28049.407 | 33816.575 | 33816.575 |
| t-345 | 13 | 1 | 16.657 | 3.763 | 1052 | 0 | 102 | 36 | 0 | 31195.135 | 33554.431 | 33554.431 |
| t-060 | 7 | 1 | 47.726 | 5.892 | 1546 | 0 | 135 | 36 | 0 | 32899.071 | 33423.359 | 33423.359 |
