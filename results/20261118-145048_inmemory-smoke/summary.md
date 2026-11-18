# inmemory-smoke — PASS ✅

- Broker: `inmemory (reorder=0, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 8.101s, closing 0.001s, drain 0.1s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 42152 | 42152 | 5022.34 | 5026.34 | 10074.9 | 0 | 0 | 0 | 20 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 30059 | 2.925 | 2.587 | 5.987 | 10.031 | 13.583 | 15.487 |
| e2e_from_send | 30059 | 2.092 | 1.925 | 4.447 | 7.811 | 8.191 | 11.231 |
| producer_lag | 30059 | 0.834 | 0.002 | 2.617 | 5.631 | 6.731 | 7.859 |
| publish_call | 26036 | 0.014 | 0.002 | 0.009 | 0.033 | 1.257 | 32.799 |
| queue_wait | 30059 | 2.071 | 1.894 | 4.427 | 7.799 | 8.163 | 11.223 |
| broker_dwell | 30059 | 2.067 | 1.893 | 4.423 | 7.795 | 8.151 | 11.223 |
| receive_call | 8687 | 0.051 | 0.008 | 0.043 | 0.158 | 12.703 | 19.615 |
| ctx_load | 30059 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.068 |
| process | 30059 | 0.001 | 0.001 | 0.001 | 0.003 | 0.006 | 1.872 |
| ctx_commit | 30059 | 0.001 | 0.001 | 0.001 | 0.001 | 0.001 | 2.513 |
| ack | 3338 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.163 |
| service | 30059 | 0.021 | 0.017 | 0.036 | 0.054 | 1.237 | 2.555 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5026.34 | 5103.9 |
| applied_total | 29814.2 | 39811 |
| bench_cpu_cores | 1.062 | 1.116 |
| bench_cpu_pct | 4.424 | 4.65 |
| bench_gc_gen0 | 1.2 | 2 |
| bench_gc_gen1 | 0.4 | 1 |
| bench_gc_gen2 | 0.2 | 1 |
| bench_gc_heap_mb | 151 | 162.4 |
| bench_mem_mb | 246.84 | 249.7 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.222 | 2.48 |
| in_flight | 9.8 | 13 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 5022.34 | 5081 |
| produced_total | 29824 | 39822 |
| publish_mb_per_s | 2.592 | 2.62 |

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
| hot-0 | 200 | 4 | 2000 | 16435 | 16435 | 0 | 2.479 | 10.239 | 14.815 |
| hot-1 | 200 | 4 | 2000 | 16559 | 16559 | 0 | 2.493 | 9.327 | 14.367 |
| cold-0 | 200 | 4 | 500 | 4577 | 4577 | 0 | 2.981 | 10.543 | 14.807 |
| cold-1 | 200 | 4 | 500 | 4581 | 4581 | 0 | 3.115 | 10.711 | 15.487 |
