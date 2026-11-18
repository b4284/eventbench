# 07-kafkafsync_mongo — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=1), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 61.973s, closing 6.258s, drain 0.297s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 243634 | 243634 | 3539.149 | 3633.71 | 4630.6 | 0 | 0 | 0 | 165 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 80594 | 37555.842 | 37486.591 | 44072.959 | 46104.575 | 46727.167 | 47218.687 |
| e2e_from_send | 80594 | 2219.628 | 2222.079 | 3940.351 | 4481.023 | 4673.535 | 4833.279 |
| producer_lag | 80594 | 35336.263 | 35454.975 | 41910.271 | 43057.151 | 43581.439 | 43679.743 |
| publish_call | 569 | 3803.421 | 4333.567 | 4665.343 | 5165.055 | 5447.679 | 5582.847 |
| queue_wait | 80594 | 2200.751 | 2203.647 | 3919.871 | 4464.639 | 4657.151 | 4800.511 |
| broker_dwell | 80594 | 28.193 | 26.095 | 51.423 | 67.135 | 131.327 | 218.623 |
| receive_call | 106643 | 0.038 | 0.021 | 0.037 | 0.09 | 3.639 | 16.479 |
| ctx_load | 80594 | 0.754 | 0.655 | 1.103 | 2.385 | 5.303 | 19.967 |
| process | 80594 | 0.002 | 0.001 | 0.002 | 0.004 | 0.048 | 1.684 |
| ctx_commit | 80594 | 7.463 | 7.023 | 9.095 | 14.887 | 47.615 | 100.991 |
| ack | 80456 | 0.007 | 0.006 | 0.012 | 0.025 | 0.071 | 2.311 |
| service | 80594 | 18.878 | 14.807 | 37.535 | 71.103 | 124.607 | 244.991 |
| e2e_large | 65 | 36033.205 | 36372.479 | 42565.631 | 44171.263 | 44597.247 | 44597.247 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3633.71 | 3947.2 |
| applied_total | 124505.902 | 215513 |
| bench_cpu_cores | 3.352 | 3.606 |
| bench_cpu_pct | 13.965 | 15.03 |
| bench_gc_gen0 | 9.216 | 16 |
| bench_gc_gen1 | 1.137 | 2 |
| bench_gc_gen2 | 0.314 | 1 |
| bench_gc_heap_mb | 493.384 | 654.6 |
| bench_mem_mb | 1315.465 | 1447.4 |
| bench_threadpool_queue | 1.941 | 59 |
| bench_threadpool_threads | 25.412 | 30 |
| broker_backlog | 0 | 0 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 83005.098 | 151933 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 48996.02 | 112160 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 126.71 | 138.22 |
| ctr_eb_kafka_mem_mb | 2141.641 | 2203 |
| ctr_eb_mongo_cpu_pct | 505.132 | 526.11 |
| ctr_eb_mongo_mem_mb | 4191.68 | 4218.1 |
| ctr_eb_nats_cpu_pct | 0.68 | 3.33 |
| ctr_eb_nats_mem_mb | 311.143 | 317.6 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 12.159 | 12.2 |
| ctr_eb_postgres_cpu_pct | 0.297 | 1.61 |
| ctr_eb_postgres_mem_mb | 4885.22 | 4923.8 |
| ctr_eb_rabbitmq_cpu_pct | 11.214 | 12.47 |
| ctr_eb_rabbitmq_mem_mb | 998.514 | 1046.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 42.471 | 48.07 |
| in_flight | -7974.51 | -3911 |
| mongo_conn_current | 87.471 | 97 |
| mongo_op_command_per_s | 3410.1 | 3648.1 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 869.383 | 1332.7 |
| mongo_op_query_per_s | 6817.798 | 7294.4 |
| mongo_op_update_per_s | 5312.102 | 5954.1 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 4.412 | 10 |
| mongo_txn_committed_per_s | 3407.523 | 3647.4 |
| mongo_wt_cache_mb | 1628.637 | 1643 |
| mongo_wt_dirty_mb | 95.224 | 102.1 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 3539.149 | 10995.1 |
| produced_total | 116531.392 | 205551 |
| publish_mb_per_s | 15.505 | 50.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 47,543 records are Completed |
| record_counts | pass | 47,543 records found, 47,543 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 165 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 243,634 events applied by workers, 243,634 generated |
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
| t-028 | 27 | 1 | 20.607 | 9.156 | 454 | 0 | 205 | 205 | 0 | 36962.303 | 47185.919 | 47185.919 |
| t-031 | 11 | 1 | 69.313 | 12.621 | 463 | 0 | 214 | 214 | 0 | 39845.887 | 46923.775 | 47185.919 |
| t-050 | 96 | 1 | 10.919 | 17.369 | 130 | 0 | 460 | 460 | 0 | 37748.735 | 46923.775 | 47185.919 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 75 | 75 | 0 | 38797.311 | 46923.775 | 46923.775 |
| t-150 | 15 | 1 | 31.013 | 7.923 | 990 | 0 | 147 | 147 | 0 | 36700.159 | 46923.775 | 46923.775 |
| t-226 | 6 | 1 | 87.952 | 9.302 | 623 | 0 | 152 | 152 | 0 | 37748.735 | 46923.775 | 46923.775 |
| t-298 | 6 | 1 | 64.085 | 7.048 | 3700 | 0 | 121 | 121 | 0 | 39059.455 | 46923.775 | 46923.775 |
| t-317 | 32 | 1 | 39.571 | 21.604 | 1288 | 0 | 411 | 411 | 0 | 36700.159 | 46923.775 | 47185.919 |
| t-399 | 34 | 1 | 33.65 | 18.576 | 397 | 0 | 398 | 398 | 0 | 38797.311 | 46923.775 | 46923.775 |
| t-460 | 71 | 1 | 20.528 | 24.491 | 141 | 0 | 537 | 537 | 0 | 38010.879 | 46923.775 | 46923.775 |
| t-466 | 11 | 1 | 39.527 | 6.561 | 449 | 0 | 129 | 129 | 0 | 35913.727 | 46923.775 | 46923.775 |
| t-010 | 26 | 1 | 42.887 | 18.144 | 183 | 0 | 365 | 365 | 0 | 38010.879 | 46661.631 | 46923.775 |
| t-018 | 26 | 1 | 50.91 | 22.382 | 315 | 0 | 426 | 426 | 0 | 38535.167 | 46661.631 | 46661.631 |
| t-020 | 22 | 1 | 63.625 | 22.417 | 981 | 0 | 400 | 400 | 0 | 38273.023 | 46661.631 | 46661.631 |
| t-022 | 63 | 1 | 30.227 | 31.845 | 162 | 0 | 679 | 679 | 0 | 37486.591 | 46661.631 | 46661.631 |
| t-030 | 19 | 1 | 24.985 | 8.002 | 748 | 0 | 157 | 157 | 0 | 37486.591 | 46661.631 | 46661.631 |
| t-057 | 17 | 1 | 64.483 | 19.109 | 290 | 0 | 338 | 338 | 0 | 36700.159 | 46661.631 | 46923.775 |
| t-061 | 148 | 2 | 22.45 | 54.927 | 1274 | 0 | 1217 | 1217 | 0 | 37224.447 | 46661.631 | 46923.775 |
| t-080 | 56 | 1 | 32.985 | 31.08 | 822 | 0 | 680 | 680 | 0 | 37748.735 | 46661.631 | 47448.063 |
| t-081 | 6 | 1 | 13.059 | 1.301 | 2170 | 0 | 31 | 31 | 0 | 40108.031 | 46661.631 | 46661.631 |
| t-085 | 19 | 1 | 28.267 | 8.788 | 1157 | 0 | 205 | 205 | 0 | 38273.023 | 46661.631 | 46923.775 |
| t-088 | 46 | 1 | 43.405 | 32.75 | 203 | 0 | 688 | 688 | 0 | 37486.591 | 46661.631 | 47185.919 |
| t-100 | 33 | 1 | 40.775 | 21.722 | 2626 | 0 | 424 | 424 | 0 | 37486.591 | 46661.631 | 46661.631 |
| t-125 | 20 | 1 | 64.654 | 22.573 | 1900 | 0 | 411 | 411 | 0 | 38273.023 | 46661.631 | 46661.631 |
| t-146 | 6 | 1 | 43.291 | 4.645 | 246 | 0 | 89 | 89 | 0 | 38010.879 | 46661.631 | 46661.631 |
