# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.1s, closing 0.001s, drain 0.1s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42155 | 42155 | 5005.925 | 5010.175 | 10575.7 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30061 | 2.955 | 2.615 | 6.067 | 9.879 | 13.087 | 14.935 |
| e2e_from_send | 30061 | 2.099 | 1.924 | 4.491 | 7.815 | 8.279 | 11.487 |
| producer_lag | 30061 | 0.857 | 0.001 | 2.695 | 5.779 | 6.695 | 7.151 |
| publish_call | 26199 | 0.015 | 0.002 | 0.009 | 0.032 | 1.423 | 33.343 |
| queue_wait | 30061 | 2.078 | 1.897 | 4.475 | 7.803 | 8.271 | 11.463 |
| broker_dwell | 30061 | 2.075 | 1.894 | 4.471 | 7.799 | 8.263 | 11.463 |
| receive_call | 8660 | 0.047 | 0.008 | 0.033 | 0.164 | 11.831 | 18.703 |
| ctx_load | 30047 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.277 |
| process | 30041 | 0.001 | 0.001 | 0.001 | 0.003 | 0.005 | 2.143 |
| ctx_commit | 30043 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.329 |
| ack | 3406 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.004 |
| service | 30061 | 0.02 | 0.016 | 0.034 | 0.05 | 0.346 | 2.223 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5010.175 | 5080.5 |
| applied_total | 29799 | 39827 |
| bench_cpu_cores | 1.069 | 1.11 |
| bench_cpu_pct | 4.458 | 4.63 |
| bench_gc_gen0 | 2 | 4 |
| bench_gc_gen1 | 0.5 | 2 |
| bench_gc_gen2 | 0.25 | 1 |
| bench_gc_heap_mb | 138.4 | 147.7 |
| bench_mem_mb | 267.825 | 274.3 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 24 | 24 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.228 | 2.68 |
| in_flight | 8.75 | 15 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5005.925 | 5083.5 |
| produced_total | 29807.75 | 39834 |
| publish_mb_per_s | 2.582 | 2.62 |

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
| hot-0 | 200 | 4 | 2000 | 16437 | 16437 | 0 | 2.495 | 8.991 | 14.935 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.507 | 9.607 | 14.759 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.207 | 10.527 | 14.359 |
| cold-1 | 200 | 4 | 500 | 4582 | 4582 | 0 | 3.089 | 10.823 | 14.679 |
