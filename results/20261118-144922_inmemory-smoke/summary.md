# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.101s, closing 0.001s, drain 0.096s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42152 | 42152 | 5005.75 | 5008.775 | 10837.9 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30059 | 2.884 | 2.559 | 5.867 | 9.999 | 13.239 | 14.775 |
| e2e_from_send | 30058 | 2.081 | 2.018 | 4.367 | 7.723 | 8.159 | 8.543 |
| producer_lag | 30059 | 0.803 | 0.002 | 2.549 | 5.595 | 6.743 | 7.287 |
| publish_call | 26195 | 0.015 | 0.002 | 0.01 | 0.038 | 1.554 | 33.407 |
| queue_wait | 30059 | 2.06 | 1.994 | 4.343 | 7.703 | 8.143 | 8.527 |
| broker_dwell | 30059 | 2.056 | 1.991 | 4.339 | 7.699 | 8.143 | 8.527 |
| receive_call | 8690 | 0.053 | 0.009 | 0.049 | 0.173 | 13.343 | 17.103 |
| ctx_load | 30047 | 0.001 | 0.001 | 0.001 | 0.002 | 0.005 | 0.029 |
| process | 30045 | 0.001 | 0.001 | 0.001 | 0.007 | 0.035 | 2.403 |
| ctx_commit | 30041 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.503 |
| ack | 3361 | 0.001 | 0.001 | 0.001 | 0.001 | 0.004 | 0.021 |
| service | 30059 | 0.021 | 0.017 | 0.038 | 0.069 | 0.455 | 2.421 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5008.775 | 5050.7 |
| applied_total | 29795.25 | 39807 |
| bench_cpu_cores | 1.091 | 1.166 |
| bench_cpu_pct | 4.545 | 4.86 |
| bench_gc_gen0 | 1 | 2 |
| bench_gc_gen1 | 0 | 0 |
| bench_gc_gen2 | 0 | 0 |
| bench_gc_heap_mb | 123.475 | 135.5 |
| bench_mem_mb | 225 | 231.2 |
| bench_threadpool_queue | 0.5 | 2 |
| bench_threadpool_threads | 22 | 22 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.148 | 2.48 |
| in_flight | 21.75 | 37 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5005.75 | 5078.6 |
| produced_total | 29817 | 39839 |
| publish_mb_per_s | 2.582 | 2.62 |

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
| hot-0 | 200 | 4 | 2000 | 16435 | 16435 | 0 | 2.455 | 9.631 | 14.679 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.457 | 9.543 | 14.775 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.047 | 10.367 | 14.551 |
| cold-1 | 200 | 4 | 500 | 4581 | 4581 | 0 | 3.081 | 10.583 | 13.999 |
