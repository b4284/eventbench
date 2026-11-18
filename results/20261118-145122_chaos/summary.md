# chaos — FAIL ❌

- Broker: `inmemory (reorder=0, duplicate=0, drop=0.0005)`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 4 groups, 800 sources, 16 workers, offered 5000 ev/s
- Phases: run 5.101s, closing 0.001s, drain 8.09s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 27009 | 26954 | 5029.75 | 5014.3 | 5077 | 0 | 0 | 106 | 3 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 14996 | 2.869 | 2.551 | 5.871 | 9.775 | 12.631 | 14.607 |
| e2e_from_send | 14996 | 2.074 | 1.998 | 4.427 | 7.779 | 8.163 | 8.687 |
| producer_lag | 15041 | 0.795 | 0.001 | 2.499 | 5.579 | 6.783 | 7.147 |
| publish_call | 16816 | 0.015 | 0.002 | 0.01 | 0.04 | 0.484 | 31.679 |
| queue_wait | 14996 | 2.053 | 1.973 | 4.403 | 7.767 | 8.127 | 8.671 |
| broker_dwell | 14996 | 2.049 | 1.971 | 4.399 | 7.763 | 8.123 | 8.671 |
| receive_call | 5698 | 0.057 | 0.009 | 0.067 | 0.185 | 10.391 | 10.999 |
| ctx_load | 14996 | 0.001 | 0.001 | 0.001 | 0.002 | 0.003 | 0.013 |
| process | 14996 | 0.001 | 0.001 | 0.001 | 0.003 | 0.008 | 1.508 |
| ctx_commit | 14996 | 0.001 | 0.001 | 0.001 | 0.001 | 0.002 | 0.417 |
| ack | 1591 | 0.001 | 0.001 | 0.001 | 0.001 | 0.003 | 0.004 |
| service | 14996 | 0.022 | 0.016 | 0.033 | 0.054 | 1.645 | 1.941 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 5014.3 | 5077 |
| applied_total | 22245.5 | 24730 |
| bench_cpu_cores | 1.098 | 1.111 |
| bench_cpu_pct | 4.575 | 4.63 |
| bench_gc_gen0 | 2.5 | 3 |
| bench_gc_gen1 | 0.5 | 1 |
| bench_gc_gen2 | 0.5 | 1 |
| bench_gc_heap_mb | 63.35 | 78.9 |
| bench_mem_mb | 141.2 | 141.2 |
| bench_threadpool_queue | 2 | 4 |
| bench_threadpool_threads | 21 | 21 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 2.375 | 2.39 |
| in_flight | 32 | 45 |
| nacks_per_s | 7.5 | 8 |
| produced_per_s | 5029.75 | 5082 |
| produced_total | 22277.5 | 24775 |
| publish_mb_per_s | 2.595 | 2.62 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 796/800 source cursors at the expected last sequence |
| records_complete | **FAIL** | 10 records not Completed |
| record_counts | **FAIL** | 4,250 records found, 4,252 expected; 4 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 3 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 26,954 events applied by workers, 27,009 generated |
| broker_drained | **FAIL** | 51 messages left in the broker |
| runtime_ordering_gap | **FAIL** | 106 observed by workers |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

Sample mismatches:

- source 54: record 231928233986 left in status Open (version 12)
- source 77: record 330712481796 left in status Open (version 2)
- source 77: record 330712481793 left in status Open (version 10)
- source 77: record 330712481795 left in status Open (version 3)
- source 77: record 330712481794 left in status Open (version 5)
- source 205: record 880468295685 left in status Open (version 2)
- source 205: record 880468295687 left in status Open (version 2)
- source 205: record 880468295683 left in status Open (version 13)
- source 216: record 927712935938 left in status Open (version 11)
- source 216: record 927712935939 left in status Open (version 5)
- source 54: cursor at seq 58, expected 59 (1 events not applied)
- source 54: 7 records (6 completed), expected 7 (7 completed)
- source 77: cursor at seq 24, expected 42 (18 events not applied)
- source 77: 4 records (0 completed), expected 4 (4 completed)
- source 205: cursor at seq 34, expected 68 (34 events not applied)
- source 205: 7 records (4 completed), expected 9 (9 completed)
- source 216: cursor at seq 38, expected 40 (2 events not applied)
- source 216: 5 records (3 completed), expected 5 (5 completed)

## Runtime violation samples

- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 216: received seq 40 while store cursor is 38
- [ordering_gap] source 77: received seq 26 while store cursor is 24
- [ordering_gap] source 205: received seq 36 while store cursor is 34
- [ordering_gap] source 216: received seq 40 while store cursor is 38

## Groups

| group | sources | workers | offered/s | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| hot-0 | 200 | 4 | 2000 | 10459 | 10440 | 0 | 2.503 | 9.551 | 14.607 |
| hot-1 | 200 | 4 | 2000 | 10555 | 10519 | 0 | 2.395 | 8.527 | 11.639 |
| cold-0 | 200 | 4 | 500 | 3002 | 3002 | 0 | 2.981 | 10.623 | 14.359 |
| cold-1 | 200 | 4 | 500 | 2993 | 2993 | 0 | 3.059 | 10.743 | 14.143 |
