# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.1s, closing 0.001s, drain 0.1s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42155 | 42155 | 5023.28 | 5024.1 | 10179.8 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30061 | 2.906 | 2.549 | 5.975 | 10.159 | 13.943 | 14.919 |
| e2e_from_send | 30061 | 2.094 | 1.972 | 4.451 | 7.883 | 8.343 | 11.343 |
| producer_lag | 30061 | 0.812 | 0.001 | 2.593 | 5.659 | 6.687 | 7.439 |
| publish_call | 26377 | 0.015 | 0.002 | 0.009 | 0.036 | 1.652 | 32.895 |
| queue_wait | 30061 | 2.073 | 1.95 | 4.431 | 7.867 | 8.319 | 11.311 |
| broker_dwell | 30061 | 2.069 | 1.947 | 4.423 | 7.859 | 8.319 | 11.303 |
| receive_call | 8678 | 0.053 | 0.009 | 0.039 | 0.16 | 13.535 | 17.503 |
| ctx_load | 30061 | 0.001 | 0.001 | 0.001 | 0.002 | 0.004 | 0.044 |
| process | 30061 | 0.001 | 0.001 | 0.001 | 0.004 | 0.028 | 1.692 |
| ctx_commit | 30061 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.303 |
| ack | 3672 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.012 |
| service | 30061 | 0.021 | 0.017 | 0.038 | 0.059 | 0.336 | 1.944 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5024.1 | 5087.9 |
| applied_total | 29812 | 39826 |
| bench_cpu_cores | 1.092 | 1.141 |
| bench_cpu_pct | 4.552 | 4.75 |
| bench_gc_gen0 | 1.6 | 3 |
| bench_gc_gen1 | 0.4 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 144.72 | 157.7 |
| bench_mem_mb | 261.56 | 271.5 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.268 | 2.48 |
| in_flight | 10 | 14 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5023.28 | 5095.9 |
| produced_total | 29822 | 39836 |
| publish_mb_per_s | 2.592 | 2.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 6,049 records are Completed |
| record_counts | pass | 6,049 records found, 6,049 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 20 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 42,155 events applied by workers, 42,155 generated |
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
| hot-0 | 200 | 4 | 2000 | 16437 | 16437 | 0 | 2.411 | 9.423 | 14.519 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.493 | 9.927 | 14.911 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 2.989 | 11.199 | 14.919 |
| cold-1 | 200 | 4 | 500 | 4582 | 4582 | 0 | 3.101 | 10.727 | 14.439 |
