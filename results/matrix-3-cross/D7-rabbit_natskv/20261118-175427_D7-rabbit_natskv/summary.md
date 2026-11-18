# D7-rabbit_natskv — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.107s, closing 0.61s, drain 4.841s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814602 | 814602 | 12850.698 | 12849.837 | 18870.7 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642883 | 17.786 | 11.159 | 32.591 | 115.903 | 209.151 | 335.871 |
| e2e_from_send | 642883 | 13.906 | 8.703 | 25.503 | 87.871 | 163.327 | 326.655 |
| producer_lag | 642883 | 3.881 | 2.313 | 6.699 | 39.135 | 111.231 | 216.063 |
| publish_call | 318815 | 5.486 | 4.223 | 7.435 | 20.751 | 139.775 | 612.863 |
| queue_wait | 642883 | 7.651 | 4.635 | 13.471 | 53.343 | 101.887 | 273.663 |
| broker_dwell | 642883 | 7.632 | 4.627 | 13.447 | 52.735 | 101.823 | 272.895 |
| receive_call | 712032 | 33.371 | 19.167 | 88.831 | 178.431 | 198.911 | 268.799 |
| ctx_load | 642883 | 1.835 | 1.198 | 3.399 | 11.607 | 26.895 | 44.991 |
| process | 642883 | 0.002 | 0.001 | 0.002 | 0.005 | 0.111 | 10.591 |
| ctx_commit | 642883 | 3.362 | 2.291 | 6.443 | 19.519 | 42.591 | 99.263 |
| ack | 629926 | 0.002 | 0.002 | 0.003 | 0.006 | 0.076 | 2.541 |
| service | 642883 | 6.255 | 3.723 | 11.359 | 47.391 | 101.247 | 255.103 |
| e2e_large | 331 | 55.53 | 50.591 | 90.559 | 138.495 | 192.767 | 192.767 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12849.837 | 13339.4 |
| applied_total | 468729.102 | 777413 |
| bench_cpu_cores | 3.568 | 3.818 |
| bench_cpu_pct | 14.865 | 15.91 |
| bench_gc_gen0 | 11.122 | 14 |
| bench_gc_gen1 | 6.327 | 9 |
| bench_gc_gen2 | 0.347 | 2 |
| bench_gc_heap_mb | 507.624 | 630.3 |
| bench_mem_mb | 767.108 | 851.3 |
| bench_threadpool_queue | 10.755 | 108 |
| bench_threadpool_threads | 35.959 | 44 |
| broker_backlog | 185.714 | 330 |
| broker_consumers | 598 | 598 |
| broker_ready | 0 | 0 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 185.714 | 330 |
| ctr_eb_kafka_cpu_pct | 1.648 | 2.02 |
| ctr_eb_kafka_mem_mb | 1942.6 | 1942.6 |
| ctr_eb_mongo_cpu_pct | 0.937 | 1.21 |
| ctr_eb_mongo_mem_mb | 3298.602 | 3300.7 |
| ctr_eb_nats_cpu_pct | 212.749 | 235.14 |
| ctr_eb_nats_mem_mb | 2232.378 | 3211 |
| ctr_eb_pgbouncer_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.4 | 9.4 |
| ctr_eb_postgres_cpu_pct | 0.359 | 1.44 |
| ctr_eb_postgres_mem_mb | 4222.076 | 4224.8 |
| ctr_eb_rabbitmq_cpu_pct | 589.112 | 617.94 |
| ctr_eb_rabbitmq_mem_mb | 1183.059 | 1227.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 51.368 | 55.34 |
| in_flight | 79.327 | 411 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12850.698 | 13310.3 |
| produced_total | 468808.429 | 777514 |
| publish_mb_per_s | 44.327 | 70.36 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,590 records are Completed |
| record_counts | pass | 117,590 records found, 117,590 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,602 events applied by workers, 814,602 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-421 | 6 | 1 | 65.062 | 6.65 | 758 | 0 | 382 | 382 | 0 | 11.071 | 203.775 | 231.423 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 11.199 | 182.271 | 309.247 |
| t-060 | 7 | 1 | 47.726 | 5.892 | 1546 | 0 | 398 | 398 | 0 | 10.751 | 172.031 | 241.663 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 11.327 | 158.719 | 268.287 |
| t-286 | 20 | 1 | 19.211 | 6.539 | 401 | 0 | 449 | 449 | 0 | 10.559 | 156.671 | 221.183 |
| t-363 | 19 | 1 | 93.142 | 30.223 | 639 | 0 | 1855 | 1855 | 0 | 11.199 | 155.647 | 239.615 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2989 | 2989 | 0 | 11.391 | 150.527 | 280.575 |
| t-468 | 61 | 3 | 127.029 | 127.053 | 1645 | 0 | 7792 | 7792 | 0 | 11.263 | 147.455 | 321.535 |
| t-200 | 62 | 1 | 28.398 | 29.019 | 348 | 0 | 1901 | 1901 | 0 | 10.943 | 146.431 | 233.471 |
| t-488 | 26 | 1 | 57.68 | 25.54 | 1017 | 0 | 1556 | 1556 | 0 | 11.199 | 146.431 | 192.511 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9184 | 9184 | 0 | 11.455 | 145.407 | 257.023 |
| t-137 | 10 | 1 | 21.801 | 3.669 | 1569 | 0 | 288 | 288 | 0 | 10.623 | 144.383 | 214.015 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 11.199 | 144.383 | 248.831 |
| t-441 | 30 | 2 | 141.287 | 68.521 | 2203 | 0 | 4264 | 4264 | 0 | 11.007 | 144.383 | 236.543 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 2563 | 2563 | 0 | 11.263 | 140.287 | 212.991 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 8390 | 8390 | 0 | 11.327 | 140.287 | 261.119 |
| t-210 | 71 | 2 | 57.482 | 65.449 | 136 | 0 | 4177 | 4177 | 0 | 11.135 | 139.263 | 236.543 |
| t-386 | 56 | 1 | 12.283 | 11.312 | 3094 | 0 | 810 | 810 | 0 | 10.879 | 138.239 | 221.183 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 390 | 390 | 0 | 10.687 | 137.215 | 229.375 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 11.263 | 137.215 | 303.103 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 11.199 | 136.191 | 221.183 |
| t-235 | 54 | 2 | 79.374 | 71.451 | 527 | 0 | 4451 | 4451 | 0 | 11.135 | 136.191 | 219.135 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 7676 | 7676 | 0 | 11.391 | 136.191 | 278.527 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13790 | 13790 | 0 | 11.455 | 135.167 | 259.071 |
| t-119L | 64 | 2 | 4.456 | 4.863 | 156 | 1 | 412 | 412 | 0 | 42.751 | 135.167 | 160.767 |
