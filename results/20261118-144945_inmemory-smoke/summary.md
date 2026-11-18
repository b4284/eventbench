# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.1s, closing 0.001s, drain 0.096s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42155 | 42155 | 5026.55 | 5027.3 | 10712.7 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30061 | 2.92 | 2.571 | 6.023 | 9.895 | 13.119 | 14.935 |
| e2e_from_send | 30061 | 2.083 | 1.986 | 4.411 | 7.831 | 8.191 | 8.447 |
| producer_lag | 30061 | 0.838 | 0.002 | 2.625 | 5.687 | 6.727 | 7.075 |
| publish_call | 26166 | 0.016 | 0.002 | 0.009 | 0.037 | 0.758 | 34.367 |
| queue_wait | 30061 | 2.061 | 1.951 | 4.391 | 7.807 | 8.175 | 8.431 |
| broker_dwell | 30061 | 2.057 | 1.949 | 4.387 | 7.803 | 8.171 | 8.431 |
| receive_call | 8748 | 0.052 | 0.009 | 0.047 | 0.16 | 13.559 | 16.399 |
| ctx_load | 30052 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.028 |
| process | 30040 | 0.001 | 0.001 | 0.001 | 0.004 | 0.025 | 2.181 |
| ctx_commit | 30048 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 2.505 |
| ack | 3379 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.009 |
| service | 30061 | 0.022 | 0.016 | 0.036 | 0.061 | 1.864 | 2.529 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5027.3 | 5087.2 |
| applied_total | 27305.75 | 34811 |
| bench_cpu_cores | 1.078 | 1.105 |
| bench_cpu_pct | 4.49 | 4.6 |
| bench_gc_gen0 | 1 | 1 |
| bench_gc_gen1 | 0.25 | 1 |
| bench_gc_gen2 | 0 | 0 |
| bench_gc_heap_mb | 141.35 | 146.7 |
| bench_mem_mb | 250.725 | 255.6 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.11 | 2.48 |
| in_flight | 13 | 26 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5026.55 | 5094.2 |
| produced_total | 27318.75 | 34818 |
| publish_mb_per_s | 2.595 | 2.63 |

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
| hot-0 | 200 | 4 | 2000 | 16437 | 16437 | 0 | 2.453 | 9.391 | 14.935 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.467 | 9.519 | 14.887 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.065 | 10.567 | 14.735 |
| cold-1 | 200 | 4 | 500 | 4582 | 4582 | 0 | 3.105 | 10.495 | 14.663 |
