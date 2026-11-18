# D6-pgmq_natskv — PASS ✅

- Broker: `pgmq fifo via read_grouped_head, index=Btree, vt=30s, unlogged=False, delete on ack @ 127.0.0.1:6432/bench (pool max 1000)`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.195s, closing 0.373s, drain 14.967s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814336 | 814336 | 12856.747 | 10631.439 | 15253.3 | 0 | 0 | 0 | 431 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642618 | 8934.197 | 8142.847 | 21069.823 | 26542.079 | 28229.631 | 28639.231 |
| e2e_from_send | 642618 | 8922.775 | 8134.655 | 21053.439 | 26525.695 | 28229.631 | 28639.231 |
| producer_lag | 642618 | 11.418 | 8.879 | 21.119 | 69.183 | 139.391 | 228.607 |
| publish_call | 107963 | 17.773 | 16.223 | 26.767 | 57.471 | 154.111 | 250.623 |
| queue_wait | 642618 | 8801.805 | 7954.431 | 20938.751 | 26411.007 | 28114.943 | 28606.463 |
| broker_dwell | 642618 | 8788.138 | 7942.143 | 20922.367 | 26411.007 | 28098.559 | 28606.463 |
| receive_call | 202835 | 17.401 | 16.591 | 26.719 | 50.751 | 84.863 | 1057.791 |
| ctx_load | 642618 | 5.212 | 3.999 | 9.911 | 23.439 | 38.847 | 114.623 |
| process | 642618 | 0.002 | 0.001 | 0.002 | 0.004 | 0.062 | 14.719 |
| ctx_commit | 642618 | 9.527 | 7.567 | 18.687 | 36.863 | 57.887 | 179.071 |
| ack | 642618 | 14.115 | 14.247 | 23.663 | 42.911 | 74.431 | 106.303 |
| service | 642618 | 120.967 | 84.671 | 279.551 | 386.303 | 470.015 | 651.263 |
| e2e_large | 331 | 288.708 | 280.831 | 397.567 | 551.935 | 595.967 | 595.967 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10631.439 | 12263.4 |
| applied_total | 371335.143 | 627900 |
| bench_cpu_cores | 3.541 | 4.371 |
| bench_cpu_pct | 14.754 | 18.21 |
| bench_gc_gen0 | 5.102 | 8 |
| bench_gc_gen1 | 4.796 | 8 |
| bench_gc_gen2 | 1.735 | 4 |
| bench_gc_heap_mb | 704.686 | 1410.6 |
| bench_mem_mb | 856.861 | 1357.5 |
| bench_threadpool_queue | 50.898 | 331 |
| bench_threadpool_threads | 41.163 | 44 |
| broker_backlog | 95180.694 | 149581 |
| broker_oldest_age_s | 16.898 | 27 |
| broker_visible | 93002.02 | 147354 |
| ctr_eb_kafka_cpu_pct | 1.932 | 2.25 |
| ctr_eb_kafka_mem_mb | 1830.29 | 1876.8 |
| ctr_eb_mongo_cpu_pct | 1.236 | 2.01 |
| ctr_eb_mongo_mem_mb | 3283.439 | 3397 |
| ctr_eb_nats_cpu_pct | 157.718 | 173.64 |
| ctr_eb_nats_mem_mb | 6011.08 | 9055.1 |
| ctr_eb_pgbouncer_cpu_pct | 76.27 | 81.77 |
| ctr_eb_pgbouncer_mem_mb | 15.484 | 17.7 |
| ctr_eb_postgres_cpu_pct | 1107.708 | 1148.79 |
| ctr_eb_postgres_mem_mb | 4319.327 | 4466.1 |
| ctr_eb_rabbitmq_cpu_pct | 14.337 | 15.1 |
| ctr_eb_rabbitmq_mem_mb | 639.016 | 657.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 76.196 | 80.03 |
| in_flight | 97298.082 | 149463 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12856.747 | 13484.6 |
| produced_total | 468633.224 | 777363 |
| publish_mb_per_s | 44.341 | 70.27 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,566 records are Completed |
| record_counts | pass | 117,566 records found, 117,566 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 431 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,336 events applied by workers, 814,336 generated |
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
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 12251 | 12251 | 0 | 21889.023 | 28573.695 | 28704.767 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6334 | 6334 | 0 | 21889.023 | 28442.623 | 28573.695 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 6272 | 6272 | 0 | 21757.951 | 28180.479 | 28311.551 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 6076 | 6076 | 0 | 21495.807 | 27918.335 | 28180.479 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 12126 | 12126 | 0 | 21757.951 | 27787.263 | 27918.335 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 3061 | 3061 | 0 | 21233.663 | 27394.047 | 28049.407 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 3006 | 3006 | 0 | 20578.303 | 27000.831 | 27394.047 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 9182 | 9182 | 0 | 20578.303 | 26738.687 | 27000.831 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3024 | 3024 | 0 | 20709.375 | 26738.687 | 26869.759 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5909 | 5909 | 0 | 20185.087 | 26476.543 | 26738.687 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 5925 | 5925 | 0 | 20185.087 | 26345.471 | 26476.543 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2902 | 2902 | 0 | 19660.799 | 25952.255 | 26345.471 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 8786 | 8786 | 0 | 20054.015 | 25952.255 | 26083.327 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2910 | 2910 | 0 | 19267.583 | 25821.183 | 25952.255 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2987 | 2987 | 0 | 19267.583 | 25559.039 | 26476.543 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 2876 | 2876 | 0 | 19660.799 | 25559.039 | 25690.111 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 19529.727 | 25296.895 | 25690.111 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2896 | 2896 | 0 | 19267.583 | 25034.751 | 25296.895 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 8404 | 8404 | 0 | 18350.079 | 24772.607 | 25034.751 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 5702 | 5702 | 0 | 18743.295 | 24641.535 | 24641.535 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 8601 | 8601 | 0 | 18743.295 | 24641.535 | 24772.607 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 13786 | 13786 | 0 | 18481.151 | 24510.463 | 24641.535 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 16755 | 16755 | 0 | 18481.151 | 24379.391 | 24510.463 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2977 | 2977 | 0 | 18219.007 | 24379.391 | 24641.535 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 5490 | 5490 | 0 | 18219.007 | 24379.391 | 24641.535 |
