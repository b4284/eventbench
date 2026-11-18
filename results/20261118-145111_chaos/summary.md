# chaos — FAIL ❌

- Broker: `inmemory (reorder=0.01, duplicate=0, drop=0)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 5.101s, closing 0.001s, drain 0.301s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 26998 | 26998 | 5030.4 | 5026.9 | 5191.1 | 0 | 0 | 19 | 3 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 15030 | 3.101 | 2.553 | 5.875 | 9.927 | 27.391 | 259.071 |
| e2e_from_send | 15030 | 2.319 | 2.071 | 4.427 | 7.827 | 27.391 | 256.127 |
| producer_lag | 15030 | 0.783 | 0.002 | 2.487 | 5.523 | 6.607 | 7.191 |
| publish_call | 16569 | 0.016 | 0.003 | 0.013 | 0.043 | 0.329 | 32.607 |
| queue_wait | 15030 | 2.293 | 2.033 | 4.403 | 7.795 | 27.391 | 255.999 |
| broker_dwell | 15030 | 2.288 | 2.032 | 4.399 | 7.791 | 27.391 | 255.999 |
| receive_call | 5777 | 0.063 | 0.012 | 0.077 | 0.162 | 11.655 | 11.927 |
| ctx_load | 15030 | 0.001 | 0.001 | 0.001 | 0.002 | 0.007 | 1.531 |
| process | 15030 | 0.002 | 0.001 | 0.002 | 0.008 | 0.039 | 2.695 |
| ctx_commit | 15030 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 2.559 |
| ack | 1938 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 |
| service | 15030 | 0.026 | 0.019 | 0.042 | 0.074 | 1.576 | 2.741 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5026.9 | 5068.3 |
| applied_total | 22287 | 24781 |
| bench_cpu_cores | 1.112 | 1.152 |
| bench_cpu_pct | 4.63 | 4.8 |
| bench_gc_gen0 | 1 | 1 |
| bench_gc_gen1 | 0 | 0 |
| bench_gc_gen2 | 0 | 0 |
| bench_gc_heap_mb | 62.8 | 63.2 |
| bench_mem_mb | 143.35 | 150.3 |
| bench_threadpool_queue | 0 | 0 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.655 | 2.88 |
| in_flight | 13 | 19 |
| nacks_per_s | 1 | 2 |
| produced_per_s | 5030.4 | 5087.3 |
| produced_total | 22300 | 24788 |
| publish_mb_per_s | 2.595 | 2.62 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 800/800 source cursors at the expected last sequence |
| records_complete | pass | all 4,251 records are Completed |
| record_counts | pass | 4,251 records found, 4,251 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 3 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 26,998 events applied by workers, 26,998 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | **FAIL** | 19 observed by workers |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

## Runtime violation samples

- [ordering_gap] source 150: received seq 2 while store cursor is 0
- [ordering_gap] source 279: received seq 2 while store cursor is 0
- [ordering_gap] source 92: received seq 7 while store cursor is 5
- [ordering_gap] source 279: received seq 5 while store cursor is 3
- [ordering_gap] source 33: received seq 22 while store cursor is 20
- [ordering_gap] source 282: received seq 28 while store cursor is 26
- [ordering_gap] source 735: received seq 11 while store cursor is 9
- [ordering_gap] source 172: received seq 52 while store cursor is 50
- [ordering_gap] source 204: received seq 50 while store cursor is 48
- [ordering_gap] source 405: received seq 17 while store cursor is 15
- [ordering_gap] source 39: received seq 49 while store cursor is 47
- [ordering_gap] source 243: received seq 47 while store cursor is 45
- [ordering_gap] source 705: received seq 16 while store cursor is 14
- [ordering_gap] source 543: received seq 9 while store cursor is 7
- [ordering_gap] source 193: received seq 56 while store cursor is 54
- [ordering_gap] source 374: received seq 60 while store cursor is 58
- [ordering_gap] source 397: received seq 39 while store cursor is 37
- [ordering_gap] source 46: received seq 48 while store cursor is 46
- [ordering_gap] source 704: received seq 23 while store cursor is 21

## Groups

| group | sources | workers | offered/s | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| hot-0 | 200 | 4 | 2000 | 10455 | 10455 | 0 | 2.453 | 9.999 | 258.303 |
| hot-1 | 200 | 4 | 2000 | 10551 | 10551 | 0 | 2.501 | 9.479 | 259.071 |
| cold-0 | 200 | 4 | 500 | 3000 | 3000 | 0 | 3.013 | 10.583 | 14.935 |
| cold-1 | 200 | 4 | 500 | 2992 | 2992 | 0 | 2.959 | 10.247 | 254.975 |
