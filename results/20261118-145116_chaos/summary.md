# chaos — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0.05, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 5.101s, closing 0.005s, drain 0.001s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 27009 | 27009 | 5039.2 | 5035.2 | 17023.3 | 1342 | 0 | 0 | 3 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 15041 | 2.836 | 2.513 | 5.779 | 9.687 | 13.887 | 14.991 |
| e2e_from_send | 15041 | 2.062 | 2.03 | 4.359 | 7.747 | 8.099 | 8.423 |
| producer_lag | 15041 | 0.775 | 0.001 | 2.477 | 5.767 | 6.835 | 7.075 |
| publish_call | 16778 | 0.016 | 0.002 | 0.011 | 0.038 | 0.424 | 32.767 |
| queue_wait | 15041 | 2.038 | 2.002 | 4.335 | 7.735 | 8.091 | 8.407 |
| broker_dwell | 15041 | 2.034 | 2.001 | 4.335 | 7.731 | 8.087 | 8.407 |
| receive_call | 6520 | 0.053 | 0.009 | 0.068 | 0.171 | 10.775 | 11.311 |
| ctx_load | 15041 | 0.001 | 0.001 | 0.001 | 0.002 | 0.005 | 0.051 |
| process | 15041 | 0.002 | 0.001 | 0.001 | 0.008 | 0.04 | 3.055 |
| ctx_commit | 15041 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.522 |
| ack | 1758 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.004 |
| service | 15041 | 0.024 | 0.018 | 0.04 | 0.076 | 0.533 | 3.089 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5035.2 | 5078.9 |
| applied_total | 22282.5 | 24778 |
| bench_cpu_cores | 1.154 | 1.157 |
| bench_cpu_pct | 4.805 | 4.82 |
| bench_gc_gen0 | 1.5 | 3 |
| bench_gc_gen1 | 0.5 | 1 |
| bench_gc_gen2 | 0.5 | 1 |
| bench_gc_heap_mb | 72.15 | 84.2 |
| bench_mem_mb | 154.3 | 161.4 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 261 | 267 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.565 | 2.78 |
| in_flight | 14 | 20 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5039.2 | 5098.9 |
| produced_total | 22296.5 | 24786 |
| publish_mb_per_s | 2.6 | 2.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 4,252 records are Completed |
| record_counts | pass | 4,252 records found, 4,252 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 3 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 27,009 events applied by workers, 27,009 generated |
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
| hot-0 | 200 | 4 | 2000 | 10459 | 10459 | 525 | 2.381 | 8.735 | 14.863 |
| hot-1 | 200 | 4 | 2000 | 10555 | 10555 | 529 | 2.477 | 9.767 | 14.639 |
| cold-0 | 200 | 4 | 500 | 3002 | 3002 | 145 | 3.025 | 9.975 | 14.991 |
| cold-1 | 200 | 4 | 500 | 2993 | 2993 | 143 | 2.893 | 9.911 | 13.279 |
