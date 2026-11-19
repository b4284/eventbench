# 04-pgmq_natskv — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.216s, closing 0.352s, drain 11.678s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814508 | 814508 | 12853.635 | 10910.029 | 17878.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642795 | 7906.175 | 7188.479 | 18776.063 | 24133.631 | 25804.799 | 26427.391 |
| e2e_from_send | 642795 | 7900.913 | 7180.287 | 18776.063 | 24117.247 | 25788.415 | 26427.391 |
| producer_lag | 642795 | 5.262 | 2.465 | 8.351 | 82.111 | 171.647 | 276.991 |
| publish_call | 308937 | 5.357 | 4.231 | 9.047 | 21.711 | 141.311 | 557.055 |
| queue_wait | 642795 | 7786.844 | 7008.255 | 18661.375 | 24018.943 | 25706.495 | 26394.623 |
| broker_dwell | 642795 | 7783.445 | 7004.159 | 18661.375 | 24018.943 | 25690.111 | 26394.623 |
| receive_call | 222370 | 6.796 | 4.995 | 10.135 | 24.591 | 133.119 | 1136.639 |
| ctx_load | 642795 | 8.307 | 7.399 | 14.823 | 26.447 | 45.503 | 100.735 |
| process | 642795 | 0.002 | 0.001 | 0.002 | 0.004 | 0.076 | 25.919 |
| ctx_commit | 642795 | 15.474 | 14.399 | 27.247 | 46.271 | 82.175 | 260.223 |
| ack | 642795 | 4.741 | 4.183 | 8.503 | 17.935 | 36.863 | 82.111 |
| service | 642795 | 114.06 | 75.391 | 270.847 | 382.463 | 466.175 | 629.247 |
| e2e_large | 331 | 363.395 | 355.071 | 517.375 | 640.511 | 750.591 | 750.591 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10910.029 | 12269.5 |
| applied_total | 378317.184 | 646105 |
| bench_cpu_cores | 3.881 | 4.452 |
| bench_cpu_pct | 16.171 | 18.55 |
| bench_gc_gen0 | 5.49 | 8 |
| bench_gc_gen1 | 5.265 | 8 |
| bench_gc_gen2 | 1.918 | 4 |
| bench_gc_heap_mb | 706.502 | 1417.8 |
| bench_mem_mb | 828.802 | 1325.4 |
| bench_threadpool_queue | 54.653 | 331 |
| bench_threadpool_threads | 42.082 | 44 |
| broker_backlog | 88513.204 | 129011 |
| broker_oldest_age_s | 16.122 | 25 |
| broker_visible | 86249.571 | 126306 |
| ctr_eb_kafka_cpu_pct | 1.901 | 2.51 |
| ctr_eb_kafka_mem_mb | 1819.851 | 1988.4 |
| ctr_eb_mongo_cpu_pct | 1.729 | 5.83 |
| ctr_eb_mongo_mem_mb | 3418.433 | 3701.2 |
| ctr_eb_nats_cpu_pct | 144.345 | 166.96 |
| ctr_eb_nats_mem_mb | 6176.751 | 9434.7 |
| ctr_eb_pgbouncer_cpu_pct | 83.246 | 88.05 |
| ctr_eb_pgbouncer_mem_mb | 15.7 | 16.9 |
| ctr_eb_postgres_cpu_pct | 1262.378 | 1363.43 |
| ctr_eb_postgres_mem_mb | 4365.012 | 4491.8 |
| ctr_eb_rabbitmq_cpu_pct | 15.507 | 16.16 |
| ctr_eb_rabbitmq_mem_mb | 611.296 | 675.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 84.493 | 87.92 |
| in_flight | 90521.143 | 131575 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12853.635 | 13234.1 |
| produced_total | 468838.327 | 777680 |
| publish_mb_per_s | 44.328 | 71.11 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,580 records are Completed |
| record_counts | pass | 117,580 records found, 117,580 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,508 events applied by workers, 814,508 generated |
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
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12253 | 12253 | 0 | 19660.799 | 26214.399 | 26476.543 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6336 | 6336 | 0 | 19922.943 | 26083.327 | 26083.327 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 19398.655 | 25690.111 | 25821.183 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6276 | 6276 | 0 | 19529.727 | 25690.111 | 25821.183 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12131 | 12131 | 0 | 19398.655 | 25427.967 | 25690.111 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3062 | 3062 | 0 | 19136.511 | 25165.823 | 25427.967 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3007 | 3007 | 0 | 18743.295 | 24903.679 | 25427.967 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9183 | 9183 | 0 | 18743.295 | 24772.607 | 24903.679 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5912 | 5912 | 0 | 18481.151 | 24379.391 | 24510.463 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3025 | 3025 | 0 | 18743.295 | 24248.319 | 24641.535 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5926 | 5926 | 0 | 18481.151 | 24117.247 | 24379.391 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2903 | 2903 | 0 | 17825.791 | 23592.959 | 23724.031 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8789 | 8789 | 0 | 17956.863 | 23592.959 | 23724.031 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 17432.575 | 23461.887 | 23592.959 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2894 | 2894 | 0 | 18087.935 | 23330.815 | 23592.959 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 17170.431 | 22937.599 | 23199.743 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 17563.647 | 22937.599 | 23330.815 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 17170.431 | 22544.383 | 22675.455 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2899 | 2899 | 0 | 17301.503 | 22282.239 | 22544.383 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16762 | 16762 | 0 | 16515.071 | 22151.167 | 22282.239 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 16580.607 | 22151.167 | 22282.239 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8602 | 8602 | 0 | 16908.287 | 22151.167 | 22282.239 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13789 | 13789 | 0 | 16515.071 | 22020.095 | 22282.239 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 2870 | 2870 | 0 | 16318.463 | 21757.951 | 21889.023 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5491 | 5491 | 0 | 16187.391 | 21495.807 | 21626.879 |
