# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.1s, closing 0.002s, drain 0.097s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42152 | 42152 | 5041 | 5036.75 | 10627.8 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30059 | 2.889 | 2.559 | 5.903 | 9.887 | 13.239 | 20.079 |
| e2e_from_send | 30059 | 2.058 | 1.891 | 4.431 | 7.707 | 8.303 | 19.295 |
| producer_lag | 30059 | 0.832 | 0.002 | 2.629 | 5.647 | 6.579 | 7.171 |
| publish_call | 26131 | 0.014 | 0.002 | 0.009 | 0.034 | 1.22 | 32.063 |
| queue_wait | 30059 | 2.037 | 1.867 | 4.415 | 7.675 | 8.263 | 19.279 |
| broker_dwell | 30059 | 2.033 | 1.864 | 4.407 | 7.671 | 8.263 | 19.263 |
| receive_call | 8795 | 0.053 | 0.009 | 0.055 | 0.164 | 12.679 | 17.055 |
| ctx_load | 30041 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 1.719 |
| process | 30050 | 0.001 | 0.001 | 0.001 | 0.003 | 0.011 | 1.754 |
| ctx_commit | 30044 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 3.555 |
| ack | 3426 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.011 |
| service | 30059 | 0.02 | 0.016 | 0.035 | 0.053 | 0.269 | 3.567 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5036.75 | 5100 |
| applied_total | 28575 | 39828 |
| bench_cpu_cores | 1.085 | 1.191 |
| bench_cpu_pct | 4.52 | 4.96 |
| bench_gc_gen0 | 1.75 | 3 |
| bench_gc_gen1 | 0.75 | 2 |
| bench_gc_gen2 | 0.25 | 1 |
| bench_gc_heap_mb | 152.625 | 159.1 |
| bench_mem_mb | 282.2 | 290.6 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.318 | 2.57 |
| in_flight | 8 | 15 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5041 | 5088 |
| produced_total | 28583 | 39842 |
| publish_mb_per_s | 2.602 | 2.63 |

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
| hot-0 | 200 | 4 | 2000 | 16435 | 16435 | 0 | 2.445 | 8.959 | 13.943 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.469 | 9.223 | 14.647 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.159 | 10.751 | 20.079 |
| cold-1 | 200 | 4 | 500 | 4581 | 4581 | 0 | 2.955 | 10.631 | 14.319 |
