# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 6.102s, closing 0.004s, drain 0.001s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 32021 | 32021 | 5049.1 | 5049.767 | 15054.3 | 0 | 0 | 0 | 3 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 20059 | 2.809 | 2.495 | 5.707 | 9.199 | 11.919 | 14.679 |
| e2e_from_send | 20058 | 2.047 | 1.992 | 4.307 | 7.627 | 8.043 | 8.263 |
| producer_lag | 20059 | 0.763 | 0.001 | 2.405 | 5.363 | 6.711 | 7.059 |
| publish_call | 20194 | 0.015 | 0.003 | 0.011 | 0.043 | 0.366 | 33.247 |
| queue_wait | 20059 | 2.021 | 1.966 | 4.275 | 7.603 | 8.015 | 8.175 |
| broker_dwell | 20059 | 2.016 | 1.963 | 4.271 | 7.603 | 8.011 | 8.175 |
| receive_call | 6655 | 0.051 | 0.009 | 0.056 | 0.167 | 10.991 | 11.567 |
| ctx_load | 20053 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.121 |
| process | 20057 | 0.001 | 0.001 | 0.001 | 0.004 | 0.025 | 1.692 |
| ctx_commit | 20051 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 3.075 |
| ack | 2136 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.007 |
| service | 20059 | 0.026 | 0.017 | 0.036 | 0.07 | 2.177 | 4.255 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5049.767 | 5100.1 |
| applied_total | 24817.667 | 29872 |
| bench_cpu_cores | 1.102 | 1.162 |
| bench_cpu_pct | 4.59 | 4.84 |
| bench_gc_gen0 | 2 | 2 |
| bench_gc_gen1 | 0 | 0 |
| bench_gc_gen2 | 0 | 0 |
| bench_gc_heap_mb | 83.767 | 86.1 |
| bench_mem_mb | 138.667 | 140.6 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.423 | 2.74 |
| in_flight | 14.333 | 21 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5049.1 | 5087.1 |
| produced_total | 24832 | 29880 |
| publish_mb_per_s | 2.603 | 2.62 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 4,824 records are Completed |
| record_counts | pass | 4,824 records found, 4,824 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 3 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 32,021 events applied by workers, 32,021 generated |
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
| hot-0 | 200 | 4 | 2000 | 12435 | 12435 | 0 | 2.395 | 8.231 | 14.663 |
| hot-1 | 200 | 4 | 2000 | 12514 | 12514 | 0 | 2.419 | 8.647 | 14.047 |
| cold-0 | 200 | 4 | 500 | 3537 | 3537 | 0 | 3.035 | 10.599 | 14.391 |
| cold-1 | 200 | 4 | 500 | 3535 | 3535 | 0 | 2.999 | 10.383 | 14.679 |
