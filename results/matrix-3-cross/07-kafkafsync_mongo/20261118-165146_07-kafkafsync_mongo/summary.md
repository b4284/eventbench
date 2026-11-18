# 07-kafkafsync_mongo — PASS ✅

- Broker: `kafka 500 topics 'eb.g*' (=workers partitions/group, rf=1, flush.messages=1), acks=all idempotent, linger=2ms, compression=none, max.message=64MB, producers=per publisher (max 32), manual assign + contiguous commits every 100ms @ 127.0.0.1:9092`
- Store: `mongo @ 127.0.0.1:27017/eventbench prefix=eb, mode=transactions (readConcern snapshot), w=majority j=true, load readConcern majority, chunk=1024KiB, pool max 1000`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 61.56s, closing 6.429s, drain 0.185s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 242862 | 242862 | 3539.802 | 3685.968 | 4240.2 | 0 | 0 | 0 | 165 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 79855 | 37392.539 | 37453.823 | 43745.279 | 45809.663 | 46333.951 | 46727.167 |
| e2e_from_send | 79855 | 2180.504 | 2166.783 | 3887.103 | 4411.391 | 4628.479 | 4784.127 |
| producer_lag | 79855 | 35212.094 | 35258.367 | 41582.591 | 42893.311 | 43548.671 | 43745.279 |
| publish_call | 569 | 3786.584 | 4268.031 | 4685.823 | 5468.159 | 5767.167 | 5877.759 |
| queue_wait | 79855 | 2162.058 | 2146.303 | 3868.671 | 4395.007 | 4612.095 | 4739.071 |
| broker_dwell | 79855 | 28.893 | 26.751 | 52.127 | 66.943 | 129.599 | 200.063 |
| receive_call | 105986 | 0.038 | 0.022 | 0.038 | 0.09 | 4.283 | 14.967 |
| ctx_load | 79855 | 0.728 | 0.633 | 1.063 | 2.265 | 4.743 | 22.927 |
| process | 79855 | 0.002 | 0.001 | 0.002 | 0.004 | 0.072 | 3.681 |
| ctx_commit | 79855 | 7.295 | 6.959 | 8.943 | 13.479 | 48.287 | 80.191 |
| ack | 79711 | 0.008 | 0.006 | 0.012 | 0.025 | 0.063 | 5.471 |
| service | 79855 | 18.438 | 14.559 | 36.959 | 69.183 | 104.895 | 179.711 |
| e2e_large | 65 | 35568.782 | 35389.439 | 42795.007 | 43778.047 | 44302.335 | 44302.335 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3685.968 | 4060.2 |
| applied_total | 122060.56 | 212640 |
| bench_cpu_cores | 3.33 | 3.587 |
| bench_cpu_pct | 13.875 | 14.95 |
| bench_gc_gen0 | 9.46 | 16 |
| bench_gc_gen1 | 0.98 | 2 |
| bench_gc_gen2 | 0.32 | 1 |
| bench_gc_heap_mb | 521.396 | 725.2 |
| bench_mem_mb | 1340.86 | 1502.3 |
| bench_threadpool_queue | 0.74 | 22 |
| bench_threadpool_threads | 28.08 | 29 |
| broker_backlog | 0 | 0 |
| broker_commit_errors | 0 | 0 |
| broker_committed_offsets | 81068.08 | 152259 |
| broker_discarded_msgs | 0 | 0 |
| broker_end_offsets | 47731.28 | 112581 |
| broker_rewinds | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 124.72 | 133.99 |
| ctr_eb_kafka_mem_mb | 2083.252 | 2136.2 |
| ctr_eb_mongo_cpu_pct | 481.425 | 507.19 |
| ctr_eb_mongo_mem_mb | 4034.832 | 4055 |
| ctr_eb_nats_cpu_pct | 0.531 | 2.12 |
| ctr_eb_nats_mem_mb | 171.716 | 172.5 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.187 | 0.29 |
| ctr_eb_postgres_mem_mb | 4987.296 | 5005.8 |
| ctr_eb_rabbitmq_cpu_pct | 12.128 | 12.87 |
| ctr_eb_rabbitmq_mem_mb | 1056.166 | 1088.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 41.094 | 45.05 |
| in_flight | -7968.56 | -2982 |
| mongo_conn_current | 98.68 | 107 |
| mongo_op_command_per_s | 3423.472 | 3742.2 |
| mongo_op_delete_per_s | 0 | 0 |
| mongo_op_getmore_per_s | 0 | 0 |
| mongo_op_insert_per_s | 855.313 | 1276.7 |
| mongo_op_query_per_s | 6843.902 | 7483.6 |
| mongo_op_update_per_s | 5345.348 | 6045.2 |
| mongo_store_commit_retries_per_s | 0 | 0 |
| mongo_store_embedded_retries_per_s | 0 | 0 |
| mongo_store_torn_loads_per_s | 0 | 0 |
| mongo_store_txn_retries_per_s | 0 | 0 |
| mongo_txn_aborted_per_s | 0 | 0 |
| mongo_txn_active | 0.64 | 2 |
| mongo_txn_committed_per_s | 3420.707 | 3741.8 |
| mongo_wt_cache_mb | 1616.64 | 1637.7 |
| mongo_wt_dirty_mb | 88.454 | 115.2 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 3539.802 | 11500.3 |
| produced_total | 114092 | 201312 |
| publish_mb_per_s | 15.688 | 60.8 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 47,442 records are Completed |
| record_counts | pass | 47,442 records found, 47,442 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 165 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 242,862 events applied by workers, 242,862 generated |
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
| t-387 | 13 | 1 | 26.447 | 5.945 | 138 | 0 | 124 | 124 | 0 | 36962.303 | 46923.775 | 46923.775 |
| t-067 | 11 | 1 | 86.994 | 16.001 | 3490 | 0 | 280 | 280 | 0 | 36962.303 | 46661.631 | 46661.631 |
| t-130 | 13 | 1 | 31.643 | 7.058 | 136 | 0 | 135 | 135 | 0 | 40370.175 | 46661.631 | 46661.631 |
| t-012 | 6 | 1 | 61.836 | 6.408 | 556 | 0 | 121 | 121 | 0 | 41943.039 | 46399.487 | 46399.487 |
| t-024 | 14 | 1 | 33.379 | 7.62 | 1743 | 0 | 159 | 159 | 0 | 34865.151 | 46399.487 | 46399.487 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 137 | 137 | 0 | 41156.607 | 46399.487 | 46399.487 |
| t-050 | 96 | 1 | 10.919 | 17.369 | 130 | 0 | 459 | 459 | 0 | 37748.735 | 46399.487 | 46661.631 |
| t-085 | 19 | 1 | 28.267 | 8.788 | 1157 | 0 | 205 | 205 | 0 | 38010.879 | 46399.487 | 46661.631 |
| t-106 | 24 | 2 | 178.055 | 72.395 | 824 | 0 | 1308 | 1308 | 0 | 37748.735 | 46399.487 | 46661.631 |
| t-158 | 6 | 1 | 43.525 | 4.603 | 165 | 0 | 81 | 81 | 0 | 38010.879 | 46399.487 | 46399.487 |
| t-165 | 36 | 1 | 55.043 | 32.844 | 373 | 0 | 630 | 630 | 0 | 37486.591 | 46399.487 | 46399.487 |
| t-171 | 14 | 1 | 96.444 | 22.876 | 142 | 0 | 414 | 414 | 0 | 38273.023 | 46399.487 | 46661.631 |
| t-212 | 29 | 1 | 50.595 | 24.477 | 296 | 0 | 470 | 470 | 0 | 37224.447 | 46399.487 | 46661.631 |
| t-243 | 16 | 1 | 59.376 | 15.905 | 3143 | 0 | 301 | 301 | 0 | 37224.447 | 46399.487 | 46399.487 |
| t-288 | 6 | 1 | 58.058 | 5.873 | 214 | 0 | 104 | 104 | 0 | 39059.455 | 46399.487 | 46399.487 |
| t-301 | 26 | 1 | 44.546 | 19.992 | 3339 | 0 | 356 | 356 | 0 | 38535.167 | 46399.487 | 46923.775 |
| t-326 | 10 | 1 | 162.672 | 27.871 | 1837 | 0 | 495 | 495 | 0 | 38535.167 | 46399.487 | 46661.631 |
| t-332 | 39 | 1 | 58.718 | 38.195 | 812 | 0 | 704 | 704 | 0 | 36700.159 | 46399.487 | 46661.631 |
| t-341 | 96 | 1 | 20.205 | 32.47 | 2651 | 0 | 681 | 681 | 0 | 37748.735 | 46399.487 | 46661.631 |
| t-353 | 77 | 2 | 42.158 | 54.021 | 977 | 0 | 1067 | 1067 | 0 | 38273.023 | 46399.487 | 46399.487 |
| t-382 | 59 | 1 | 23.549 | 23.407 | 2368 | 0 | 488 | 488 | 0 | 37224.447 | 46399.487 | 46661.631 |
| t-394 | 31 | 1 | 29.969 | 16.081 | 570 | 0 | 322 | 322 | 0 | 38273.023 | 46399.487 | 46661.631 |
| t-399 | 34 | 1 | 33.65 | 18.576 | 397 | 0 | 396 | 396 | 0 | 38797.311 | 46399.487 | 46399.487 |
| t-417 | 75 | 2 | 44.074 | 54.779 | 3449 | 0 | 1039 | 1039 | 0 | 37224.447 | 46399.487 | 46399.487 |
| t-433 | 6 | 1 | 21.23 | 2.112 | 184 | 0 | 45 | 45 | 0 | 38797.311 | 46399.487 | 46399.487 |
