# P2-nats-sync_natskv — FAIL ❌

- Broker: `nats jetstream @ nats://127.0.0.1:4222: work-queue stream/group (File), =workers partitions/group, 1 pull consumer/partition, ackWait=30s, maxAckPending=20, nack=pause+consumer rewind, msg-id dedup 120s, long-poll 1000ms, conns=4`
- Store: `natskv @ nats://127.0.0.1:4222: KV eb_cursor/eb_record + object stores eb_upload/eb_blob (File), commit = record CAS (idempotent, pre-image) + cursor CAS commit point, conns=4`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 1
- Topology: 500 groups, 16000 sources, 598 workers, offered 12856.021 ev/s
- Phases: run 20.105s, closing 1.479s, drain 20.018s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 293388 | 18636 | 13659.136 | 328.586 | 894 | 0 | 0 | 0 | 195 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1004 | 25191.919 | 25804.799 | 32931.839 | 35389.439 | 36143.103 | 36306.943 |
| e2e_from_send | 1004 | 24941.308 | 25640.959 | 32489.471 | 34996.223 | 36012.031 | 36175.871 |
| producer_lag | 193155 | 96.24 | 3.167 | 387.327 | 980.991 | 1200.127 | 1297.407 |
| publish_call | 82388 | 7.254 | 2.411 | 8.543 | 70.783 | 731.647 | 1294.335 |
| queue_wait | 1004 | 19783.828 | 20938.751 | 27410.431 | 33357.823 | 33718.271 | 33947.647 |
| broker_dwell | 1004 | 19774.515 | 20938.751 | 27394.047 | 33357.823 | 33685.503 | 33947.647 |
| receive_call | 2711 | 87.605 | 26.607 | 246.399 | 561.663 | 898.559 | 1045.503 |
| ctx_load | 1004 | 204.484 | 190.079 | 364.031 | 618.495 | 1248.255 | 1369.087 |
| process | 1004 | 0.017 | 0.001 | 0.003 | 0.316 | 0.474 | 0.54 |
| ctx_commit | 1004 | 725.389 | 784.895 | 1055.743 | 2030.591 | 2469.887 | 2469.887 |
| ack | 1004 | 0.029 | 0.002 | 0.051 | 0.235 | 2.587 | 7.527 |
| service | 1004 | 5156.983 | 4820.991 | 9314.303 | 13254.655 | 15441.919 | 17285.119 |
| e2e_large | 69 | 20524.284 | 22249.471 | 29917.183 | 32882.687 | 33226.751 | 33226.751 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 328.586 | 659.6 |
| applied_total | 3365.143 | 5730 |
| bench_cpu_cores | 1.329 | 1.709 |
| bench_cpu_pct | 5.538 | 7.12 |
| bench_gc_gen0 | 1.071 | 2 |
| bench_gc_gen1 | 0.357 | 1 |
| bench_gc_gen2 | 0.357 | 1 |
| bench_gc_heap_mb | 897.186 | 1055.8 |
| bench_mem_mb | 1151.607 | 1287 |
| bench_threadpool_queue | 0.286 | 1 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 165011.857 | 243448 |
| broker_backlog_mb | 648.222 | 954.503 |
| broker_consumer_resets | 0 | 0 |
| broker_delivery_gaps | 0 | 0 |
| ctr_eb_kafka_cpu_pct | 1.536 | 1.86 |
| ctr_eb_kafka_mem_mb | 3141.4 | 3141.4 |
| ctr_eb_mongo_cpu_pct | 1.004 | 1.57 |
| ctr_eb_mongo_mem_mb | 2766.6 | 2766.6 |
| ctr_eb_nats_cpu_pct | 285.642 | 319.36 |
| ctr_eb_nats_mem_mb | 2144.364 | 2300.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 9.3 | 9.3 |
| ctr_eb_postgres_cpu_pct | 0.939 | 3.68 |
| ctr_eb_postgres_mem_mb | 2821.9 | 2821.9 |
| ctr_eb_rabbitmq_cpu_pct | 9.333 | 9.96 |
| ctr_eb_rabbitmq_mem_mb | 744.8 | 746.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 19.119 | 21.64 |
| in_flight | 173809.786 | 257789 |
| nacks_per_s | 0 | 0 |
| produced_per_s | 13659.136 | 22322.6 |
| produced_total | 177174.929 | 263519 |
| publish_mb_per_s | 59.066 | 117.09 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 51/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 10,875 records not Completed |
| record_counts | **FAIL** | 11,739 records found, 54,348 expected; 15949 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | **FAIL** | 114 uploads verified, 58 sources mismatched |
| applied_equals_generated | **FAIL** | 18,636 events applied by workers, 293,388 generated |
| broker_drained | **FAIL** | 274,752 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |

Sample mismatches:

- source 1357: record 5828270620673 left in status Open (version 1)
- source 14547: record 62478889254913 left in status Open (version 1)
- source 4652: record 19980187860993 left in status Open (version 1)
- source 8332: record 35785667510273 left in status Open (version 1)
- source 2884: record 12386685681665 left in status Open (version 1)
- source 8452: record 36301063585793 left in status Open (version 1)
- source 7564: record 32487132626945 left in status Open (version 1)
- source 10730: record 46084999086081 left in status Open (version 1)
- source 1548: record 6648609374209 left in status Open (version 1)
- source 8952: record 38448547233793 left in status Open (version 1)
- source 8183: record 35145717383169 left in status Open (version 1)
- source 2455: record 10544144711681 left in status Open (version 1)
- source 6492: record 27882927685633 left in status Open (version 1)
- source 3600: record 15461882265601 left in status Open (version 1)
- source 5166: record 22187801051137 left in status Open (version 1)
- source 9978: record 42855183679489 left in status Open (version 1)
- source 12208: record 52432960749569 left in status Open (version 1)
- source 7851: record 33719788240897 left in status Open (version 1)
- source 8666: record 37220186587137 left in status Open (version 1)
- source 10748: record 46162308497409 left in status Open (version 1)

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-118 | 15 | 1 | 24.104 | 6.098 | 138 | 0 | 162 | 35 | 0 | 36438.015 | 36438.015 | 36438.015 |
| t-038 | 9 | 1 | 44.449 | 6.682 | 2056 | 0 | 170 | 34 | 0 | 36175.871 | 36175.871 | 36175.871 |
| t-090 | 7 | 1 | 39.93 | 4.697 | 242 | 0 | 104 | 33 | 0 | 34078.719 | 36175.871 | 36175.871 |
| t-422 | 9 | 1 | 33.219 | 4.937 | 2679 | 0 | 123 | 31 | 0 | 36175.871 | 36175.871 | 36175.871 |
| t-244 | 11 | 1 | 32 | 5.972 | 1151 | 0 | 125 | 30 | 0 | 32899.071 | 35913.727 | 35913.727 |
| t-146 | 6 | 1 | 43.291 | 4.645 | 246 | 0 | 106 | 33 | 0 | 35389.439 | 35651.583 | 35651.583 |
| t-311 | 15 | 1 | 22.841 | 5.944 | 761 | 0 | 134 | 34 | 0 | 35651.583 | 35651.583 | 35651.583 |
| t-346 | 12 | 1 | 28.038 | 5.433 | 1247 | 0 | 136 | 35 | 0 | 33816.575 | 35651.583 | 35651.583 |
| t-401 | 11 | 1 | 30.173 | 5.668 | 360 | 0 | 151 | 34 | 0 | 35651.583 | 35651.583 | 35651.583 |
| t-056 | 7 | 1 | 48.226 | 5.826 | 347 | 0 | 123 | 32 | 0 | 33161.215 | 35389.439 | 35389.439 |
| t-193 | 19 | 1 | 12.672 | 4.096 | 1805 | 0 | 128 | 31 | 0 | 33423.359 | 35389.439 | 35389.439 |
| t-224 | 9 | 1 | 30.606 | 4.698 | 1098 | 0 | 119 | 32 | 0 | 32374.783 | 35389.439 | 35389.439 |
| t-232 | 6 | 1 | 56.715 | 5.765 | 805 | 0 | 128 | 34 | 0 | 34865.151 | 35389.439 | 35389.439 |
| t-435 | 8 | 1 | 39.599 | 5.882 | 3763 | 0 | 132 | 33 | 0 | 29884.415 | 35389.439 | 35389.439 |
| t-013 | 10 | 1 | 36.707 | 5.57 | 134 | 0 | 133 | 33 | 0 | 34603.007 | 35127.295 | 35127.295 |
| t-148 | 6 | 1 | 62.881 | 5.748 | 715 | 0 | 139 | 33 | 0 | 31588.351 | 35127.295 | 35127.295 |
| t-158 | 6 | 1 | 43.525 | 4.603 | 165 | 0 | 99 | 30 | 0 | 31588.351 | 35127.295 | 35127.295 |
| t-307 | 16 | 1 | 26.404 | 7.124 | 200 | 0 | 159 | 35 | 0 | 35127.295 | 35127.295 | 35127.295 |
| t-310 | 6 | 1 | 70.482 | 7.386 | 1910 | 0 | 149 | 37 | 0 | 32243.711 | 35127.295 | 35127.295 |
| t-324 | 15 | 1 | 22.024 | 5.117 | 131 | 0 | 124 | 33 | 0 | 30932.991 | 35127.295 | 35127.295 |
| t-159 | 6 | 1 | 46.704 | 4.469 | 295 | 0 | 99 | 35 | 0 | 33816.575 | 34865.151 | 34865.151 |
| t-339 | 17 | 1 | 12.445 | 3.571 | 134 | 0 | 93 | 33 | 0 | 32767.999 | 34865.151 | 34865.151 |
| t-025 | 6 | 1 | 48.625 | 5.14 | 954 | 0 | 112 | 36 | 0 | 30670.847 | 34603.007 | 34603.007 |
| t-086 | 6 | 1 | 43.454 | 4.778 | 464 | 0 | 101 | 32 | 0 | 31064.063 | 34603.007 | 34603.007 |
| t-112 | 6 | 1 | 46.393 | 4.765 | 774 | 0 | 103 | 30 | 0 | 29884.415 | 34603.007 | 34603.007 |
