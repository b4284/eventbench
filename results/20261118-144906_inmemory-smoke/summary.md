# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.101s, closing 0.002s, drain 0.101s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42152 | 42152 | 5024.56 | 5024.18 | 10045.1 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30059 | 2.966 | 2.601 | 6.131 | 10.247 | 13.639 | 14.879 |
| e2e_from_send | 30059 | 2.111 | 1.949 | 4.511 | 7.915 | 8.303 | 8.439 |
| producer_lag | 30059 | 0.856 | 0.001 | 2.691 | 5.863 | 6.783 | 7.235 |
| publish_call | 26230 | 0.014 | 0.002 | 0.009 | 0.037 | 1.172 | 32.191 |
| queue_wait | 30059 | 2.092 | 1.923 | 4.487 | 7.899 | 8.287 | 8.439 |
| broker_dwell | 30059 | 2.088 | 1.922 | 4.483 | 7.895 | 8.287 | 8.431 |
| receive_call | 8707 | 0.061 | 0.009 | 0.04 | 0.171 | 15.783 | 20.687 |
| ctx_load | 30046 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 1.284 |
| process | 30051 | 0.001 | 0.001 | 0.001 | 0.003 | 0.005 | 2.271 |
| ctx_commit | 30051 | 0.001 | 0.001 | 0.001 | 0.001 | 0.001 | 0.82 |
| ack | 3277 | 0.001 | 0.001 | 0.001 | 0.001 | 0.005 | 0.281 |
| service | 30059 | 0.02 | 0.017 | 0.035 | 0.05 | 0.303 | 2.311 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5024.18 | 5080.7 |
| applied_total | 29820.6 | 39839 |
| bench_cpu_cores | 1.064 | 1.1 |
| bench_cpu_pct | 4.43 | 4.58 |
| bench_gc_gen0 | 1.4 | 2 |
| bench_gc_gen1 | 0.6 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 170.74 | 199.8 |
| bench_mem_mb | 249.42 | 260.3 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.18 | 2.39 |
| in_flight | 6.6 | 22 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5024.56 | 5095.3 |
| produced_total | 29827.2 | 39842 |
| publish_mb_per_s | 2.594 | 2.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 6,048 records are Completed |
| record_counts | pass | 6,048 records found, 6,048 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 20 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 42,152 events applied by workers, 42,152 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Groups

| group | sources | workers | offered/s | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| hot-0 | 200 | 4 | 2000 | 16435 | 16435 | 0 | 2.481 | 10.031 | 14.599 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.505 | 9.791 | 14.391 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.079 | 10.911 | 14.879 |
| cold-1 | 200 | 4 | 500 | 4581 | 4581 | 0 | 3.149 | 10.831 | 14.455 |
