# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.103s, closing 0.001s, drain 0.1s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42155 | 42155 | 5023.74 | 5021.18 | 10225.6 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30061 | 2.896 | 2.561 | 5.935 | 9.783 | 13.215 | 14.967 |
| e2e_from_send | 30061 | 2.074 | 1.934 | 4.427 | 7.695 | 8.303 | 11.215 |
| producer_lag | 30061 | 0.823 | 0.001 | 2.571 | 5.599 | 6.691 | 7.111 |
| publish_call | 26505 | 0.014 | 0.002 | 0.009 | 0.034 | 0.72 | 32.991 |
| queue_wait | 30061 | 2.052 | 1.907 | 4.403 | 7.675 | 8.279 | 11.207 |
| broker_dwell | 30061 | 2.049 | 1.906 | 4.399 | 7.671 | 8.279 | 11.207 |
| receive_call | 8773 | 0.052 | 0.008 | 0.045 | 0.156 | 12.415 | 19.615 |
| ctx_load | 30061 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.773 |
| process | 30061 | 0.001 | 0.001 | 0.001 | 0.003 | 0.006 | 3.731 |
| ctx_commit | 30061 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.341 |
| ack | 3127 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.538 |
| service | 30061 | 0.021 | 0.016 | 0.034 | 0.053 | 1.336 | 3.785 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5021.18 | 5096.5 |
| applied_total | 29823.6 | 39833 |
| bench_cpu_cores | 1.078 | 1.123 |
| bench_cpu_pct | 4.488 | 4.68 |
| bench_gc_gen0 | 1.2 | 2 |
| bench_gc_gen1 | 0.4 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 160.1 | 175.7 |
| bench_mem_mb | 269.5 | 273.6 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 23 | 23 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.436 | 3.09 |
| in_flight | 9 | 23 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5023.74 | 5086.5 |
| produced_total | 29832.6 | 39856 |
| publish_mb_per_s | 2.59 | 2.62 |

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
| hot-0 | 200 | 4 | 2000 | 16437 | 16437 | 0 | 2.499 | 8.775 | 14.127 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.431 | 9.119 | 14.967 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 3.053 | 11.135 | 14.727 |
| cold-1 | 200 | 4 | 500 | 4582 | 4582 | 0 | 3.051 | 10.391 | 13.975 |
