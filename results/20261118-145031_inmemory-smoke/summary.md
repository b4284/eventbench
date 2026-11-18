# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.102s, closing 0.001s, drain 0.097s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42124 | 42124 | 5020.96 | 5024.78 | 10069 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30030 | 2.881 | 2.537 | 5.863 | 9.871 | 13.727 | 15.191 |
| e2e_from_send | 30030 | 2.069 | 1.957 | 4.391 | 7.819 | 8.391 | 11.791 |
| producer_lag | 30030 | 0.813 | 0.002 | 2.519 | 5.695 | 6.683 | 7.059 |
| publish_call | 26191 | 0.016 | 0.002 | 0.01 | 0.039 | 0.902 | 34.015 |
| queue_wait | 30030 | 2.048 | 1.934 | 4.371 | 7.799 | 8.375 | 11.775 |
| broker_dwell | 30030 | 2.044 | 1.933 | 4.367 | 7.795 | 8.375 | 11.767 |
| receive_call | 8763 | 0.055 | 0.008 | 0.044 | 0.163 | 13.815 | 21.695 |
| ctx_load | 30030 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.012 |
| process | 30030 | 0.001 | 0.001 | 0.001 | 0.005 | 0.015 | 2.351 |
| ctx_commit | 30030 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 2.131 |
| ack | 3472 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.06 |
| service | 30030 | 0.02 | 0.017 | 0.036 | 0.057 | 0.093 | 2.371 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5024.78 | 5096 |
| applied_total | 29812.8 | 39818 |
| bench_cpu_cores | 1.069 | 1.14 |
| bench_cpu_pct | 4.456 | 4.75 |
| bench_gc_gen0 | 0.8 | 1 |
| bench_gc_gen1 | 0 | 0 |
| bench_gc_gen2 | 0 | 0 |
| bench_gc_heap_mb | 155.72 | 166.3 |
| bench_mem_mb | 250.6 | 258.1 |
| bench_threadpool_queue | 0.6 | 3 |
| bench_threadpool_threads | 22 | 22 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.218 | 2.48 |
| in_flight | 13.8 | 29 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5020.96 | 5089.1 |
| produced_total | 29826.6 | 39820 |
| publish_mb_per_s | 2.59 | 2.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 6,045 records are Completed |
| record_counts | pass | 6,045 records found, 6,045 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 20 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 42,124 events applied by workers, 42,124 generated |
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
| hot-0 | 200 | 4 | 2000 | 16423 | 16423 | 0 | 2.419 | 9.079 | 15.191 |
| hot-1 | 200 | 4 | 2000 | 16546 | 16546 | 0 | 2.493 | 9.623 | 14.791 |
| cold-0 | 200 | 4 | 500 | 4576 | 4576 | 0 | 2.881 | 10.319 | 14.551 |
| cold-1 | 200 | 4 | 500 | 4579 | 4579 | 0 | 3.019 | 11.151 | 14.535 |
