# D7-rabbit_natskv — PASS ✅

- Broker: `rabbitmq quorum queues, =workers partitions/group (598 queues), prefetch 40, SAC=True, delivery-limit=-1, persistent + publisher confirms, client-side ordered retry, 4+2 conns @ 127.0.0.1:5672 vhost /`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 60.118s, closing 1.197s, drain 3.062s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 814169 | 814169 | 12835.598 | 12810.814 | 20573.8 | 0 | 0 | 0 | 430 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 642450 | 23.019 | 11.887 | 43.967 | 203.391 | 301.823 | 494.079 |
| e2e_from_send | 642450 | 17.206 | 9.279 | 35.231 | 140.671 | 248.959 | 373.759 |
| producer_lag | 642450 | 5.813 | 2.465 | 8.351 | 89.983 | 181.247 | 217.343 |
| publish_call | 291806 | 6.126 | 4.343 | 7.811 | 25.023 | 215.807 | 2039.807 |
| queue_wait | 642450 | 10.076 | 4.903 | 17.615 | 99.967 | 185.599 | 315.903 |
| broker_dwell | 642450 | 10.04 | 4.899 | 17.535 | 99.391 | 183.679 | 315.647 |
| receive_call | 683560 | 33.748 | 19.199 | 90.367 | 179.711 | 199.039 | 265.983 |
| ctx_load | 642450 | 1.966 | 1.274 | 3.737 | 11.791 | 26.367 | 42.463 |
| process | 642450 | 0.002 | 0.001 | 0.002 | 0.005 | 0.089 | 7.327 |
| ctx_commit | 642450 | 3.613 | 2.459 | 7.203 | 20.479 | 36.351 | 98.239 |
| ack | 626330 | 0.002 | 0.002 | 0.003 | 0.006 | 0.086 | 1.512 |
| service | 642450 | 7.13 | 4.009 | 13.399 | 55.743 | 136.703 | 256.639 |
| e2e_large | 330 | 62.511 | 52.767 | 109.503 | 182.783 | 319.743 | 319.743 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12810.814 | 14067.3 |
| applied_total | 468643.857 | 775392 |
| bench_cpu_cores | 3.451 | 3.712 |
| bench_cpu_pct | 14.379 | 15.47 |
| bench_gc_gen0 | 9.224 | 14 |
| bench_gc_gen1 | 4.102 | 7 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 552.643 | 717.6 |
| bench_mem_mb | 837.131 | 919.4 |
| bench_threadpool_queue | 8.163 | 84 |
| bench_threadpool_threads | 35.796 | 42 |
| broker_backlog | 235.551 | 623 |
| broker_consumers | 598 | 598 |
| broker_ready | 0 | 0 |
| broker_test_nacks | 0 | 0 |
| broker_unacked | 235.551 | 623 |
| ctr_eb_kafka_cpu_pct | 1.696 | 2.17 |
| ctr_eb_kafka_mem_mb | 1747.1 | 1747.1 |
| ctr_eb_mongo_cpu_pct | 1.168 | 2.59 |
| ctr_eb_mongo_mem_mb | 3242.051 | 3243.4 |
| ctr_eb_nats_cpu_pct | 203.528 | 215.54 |
| ctr_eb_nats_mem_mb | 2189.253 | 3058.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 12.524 | 12.6 |
| ctr_eb_postgres_cpu_pct | 0.259 | 0.49 |
| ctr_eb_postgres_mem_mb | 4503.52 | 4504.2 |
| ctr_eb_rabbitmq_cpu_pct | 582.122 | 611.99 |
| ctr_eb_rabbitmq_mem_mb | 1158.186 | 1208.3 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 49.997 | 53.05 |
| in_flight | 96.898 | 1364 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 12835.598 | 14458.7 |
| produced_total | 468740.755 | 776756 |
| publish_mb_per_s | 44.301 | 71.12 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 117,544 records are Completed |
| record_counts | pass | 117,544 records found, 117,544 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 430 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 814,169 events applied by workers, 814,169 generated |
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
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 2678 | 2678 | 0 | 12.031 | 272.383 | 460.799 |
| t-389 | 64 | 1 | 28.067 | 30.041 | 281 | 0 | 2008 | 2008 | 0 | 11.775 | 262.143 | 352.255 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 2682 | 2682 | 0 | 11.711 | 258.047 | 434.175 |
| t-416 | 57 | 1 | 38.884 | 37.248 | 3430 | 0 | 2351 | 2351 | 0 | 12.159 | 257.023 | 417.791 |
| t-304 | 19 | 1 | 104.74 | 32.518 | 238 | 0 | 1940 | 1940 | 0 | 11.839 | 253.951 | 378.879 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 2833 | 2833 | 0 | 11.967 | 244.735 | 401.407 |
| t-231 | 43 | 1 | 50.375 | 34.767 | 156 | 0 | 2213 | 2213 | 0 | 12.031 | 243.711 | 315.391 |
| t-342 | 15 | 1 | 139.58 | 34.905 | 146 | 0 | 2098 | 2098 | 0 | 11.775 | 242.687 | 401.407 |
| t-064 | 11 | 1 | 46.186 | 8.695 | 263 | 0 | 535 | 535 | 0 | 11.199 | 240.639 | 286.719 |
| t-372 | 65 | 1 | 32.959 | 35.645 | 875 | 0 | 2355 | 2355 | 0 | 11.903 | 239.615 | 440.319 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 82 | 82 | 0 | 46.591 | 238.591 | 238.591 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 5908 | 5908 | 0 | 12.223 | 238.591 | 346.111 |
| t-054 | 17 | 1 | 35.84 | 10.264 | 161 | 0 | 682 | 682 | 0 | 12.031 | 237.567 | 286.719 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 6334 | 6334 | 0 | 12.095 | 237.567 | 356.351 |
| t-332 | 39 | 1 | 58.718 | 38.195 | 812 | 0 | 2412 | 2412 | 0 | 11.775 | 237.567 | 405.503 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 2895 | 2895 | 0 | 12.031 | 237.567 | 352.255 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 3024 | 3024 | 0 | 11.775 | 235.519 | 335.871 |
| t-492 | 6 | 1 | 26.029 | 2.372 | 3046 | 0 | 146 | 146 | 0 | 11.775 | 231.423 | 234.495 |
| t-242 | 28 | 1 | 62.883 | 28.827 | 1662 | 0 | 1779 | 1779 | 0 | 12.095 | 230.399 | 313.343 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 2893 | 2893 | 0 | 12.223 | 229.375 | 407.551 |
| t-322 | 27 | 1 | 65.072 | 28.546 | 2198 | 0 | 1848 | 1848 | 0 | 12.415 | 229.375 | 378.879 |
| t-347 | 162 | 4 | 62.113 | 168.769 | 833 | 0 | 10466 | 10466 | 0 | 11.967 | 229.375 | 391.167 |
| t-419 | 17 | 1 | 20.486 | 6.067 | 3214 | 0 | 426 | 426 | 0 | 11.775 | 229.375 | 258.047 |
| t-029 | 28 | 2 | 143.274 | 66.438 | 1732 | 0 | 4238 | 4238 | 0 | 11.775 | 228.351 | 405.503 |
| t-043 | 27 | 1 | 27.497 | 12.287 | 491 | 0 | 859 | 859 | 0 | 11.711 | 228.351 | 346.111 |
