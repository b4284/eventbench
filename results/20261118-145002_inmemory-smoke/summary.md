# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.1s, closing 0.001s, drain 0.099s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42155 | 42155 | 5037.025 | 5025.8 | 10666.7 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30061 | 2.972 | 2.619 | 6.107 | 10.031 | 13.535 | 16.959 |
| e2e_from_send | 30061 | 2.128 | 2.038 | 4.487 | 7.839 | 8.223 | 11.639 |
| producer_lag | 30061 | 0.845 | 0.001 | 2.669 | 5.667 | 6.747 | 7.567 |
| publish_call | 26367 | 0.014 | 0.002 | 0.009 | 0.033 | 1.497 | 32.703 |
| queue_wait | 30061 | 2.108 | 2.017 | 4.463 | 7.823 | 8.183 | 11.607 |
| broker_dwell | 30061 | 2.104 | 2.016 | 4.463 | 7.819 | 8.175 | 11.607 |
| receive_call | 8614 | 0.048 | 0.008 | 0.04 | 0.155 | 11.935 | 17.167 |
| ctx_load | 30054 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.01 |
| process | 30047 | 0.001 | 0.001 | 0.001 | 0.003 | 0.006 | 1.425 |
| ctx_commit | 30041 | 0.001 | 0.001 | 0.001 | 0.001 | 0.001 | 2.283 |
| ack | 3318 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.005 |
| service | 30061 | 0.02 | 0.016 | 0.034 | 0.05 | 0.342 | 2.305 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5025.8 | 5087.9 |
| applied_total | 31064 | 39799 |
| bench_cpu_cores | 1.07 | 1.135 |
| bench_cpu_pct | 4.458 | 4.73 |
| bench_gc_gen0 | 1.75 | 2 |
| bench_gc_gen1 | 0.5 | 1 |
| bench_gc_gen2 | 0.25 | 1 |
| bench_gc_heap_mb | 146.15 | 156.6 |
| bench_mem_mb | 248.925 | 251.7 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.188 | 2.38 |
| in_flight | 20.5 | 39 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5037.025 | 5090.9 |
| produced_total | 31084.5 | 39838 |
| publish_mb_per_s | 2.6 | 2.63 |

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
| hot-0 | 200 | 4 | 2000 | 16437 | 16437 | 0 | 2.521 | 9.775 | 14.991 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.513 | 9.327 | 11.743 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.075 | 10.543 | 16.959 |
| cold-1 | 200 | 4 | 500 | 4582 | 4582 | 0 | 3.119 | 11.247 | 14.695 |
