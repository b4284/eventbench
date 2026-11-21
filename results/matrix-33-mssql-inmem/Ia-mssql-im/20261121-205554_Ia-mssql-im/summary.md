# Ia-mssql-im — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.482s, closing 3.151s, drain 0.263s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 563574 | 563574 | 8837.937 | 8838.753 | 11534.5 | 0 | 0 | 0 | 291 | 2844 | 7103 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 363076 | 18399.529 | 18497.535 | 25788.415 | 29327.359 | 30507.007 | 32866.303 |
| e2e_from_send | 363076 | 222.679 | 217.855 | 342.271 | 1090.559 | 2861.055 | 5464.063 |
| producer_lag | 363076 | 18176.852 | 18268.159 | 25608.191 | 28983.295 | 30146.559 | 31326.207 |
| publish_call | 563574 | 196.422 | 202.879 | 337.919 | 543.743 | 1155.071 | 1695.743 |
| queue_wait | 363076 | 211.907 | 217.343 | 340.223 | 536.575 | 2193.407 | 4751.359 |
| broker_dwell | 363076 | 15.639 | 3.721 | 12.671 | 208.767 | 2015.231 | 4583.423 |
| receive_call | 460949 | 74.892 | 43.519 | 191.615 | 409.855 | 489.215 | 535.039 |
| ctx_load | 363076 | 0.002 | 0.001 | 0.003 | 0.007 | 0.036 | 22.319 |
| process | 363076 | 10.75 | 0.001 | 0.002 | 0.012 | 2613.247 | 3010.559 |
| ctx_commit | 363076 | 0.001 | 0.001 | 0.001 | 0.004 | 0.019 | 21.583 |
| ack | 353421 | 0.003 | 0.002 | 0.004 | 0.012 | 0.05 | 23.119 |
| service | 363076 | 10.772 | 0.012 | 0.033 | 0.119 | 2613.247 | 3010.559 |
| e2e_large | 189 | 19505.325 | 18579.455 | 28311.551 | 30719.999 | 31866.879 | 31866.879 |
| ready_wait | 363076 | 9.87 | 3.133 | 10.319 | 266.495 | 407.807 | 541.183 |
| blocked | 363076 | 9.87 | 3.133 | 10.319 | 266.495 | 407.807 | 541.183 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 8838.753 | 11534.5 |
| applied_total | 311675.673 | 526344 |
| bench_cpu_cores | 1.344 | 1.901 |
| bench_cpu_pct | 5.6 | 7.92 |
| bench_gc_gen0 | 1.857 | 4 |
| bench_gc_gen1 | 0.959 | 2 |
| bench_gc_gen2 | 0.633 | 1 |
| bench_gc_heap_mb | 875.014 | 1187.6 |
| bench_mem_mb | 1374.957 | 1577 |
| bench_threadpool_queue | 13.02 | 292 |
| bench_threadpool_threads | 44 | 44 |
| broker_backlog | 948.776 | 2077 |
| ctr_eb_mosquitto_cpu_pct | 28.292 | 36.19 |
| ctr_eb_mosquitto_mem_mb | 6.463 | 10.1 |
| ctr_eb_mssql_cpu_pct | 672.512 | 843.99 |
| ctr_eb_mssql_mem_mb | 7634.041 | 7972.7 |
| ctr_eb_mysql_cpu_pct | 0.649 | 0.78 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.019 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 15 | 15 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 15 | 15 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 15.2 | 15.2 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 16.3 | 16.3 |
| ctr_eb_postgres_cpu_pct | 1.264 | 2.93 |
| ctr_eb_postgres_mem_mb | 4606.729 | 4610.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 42.339 | 55.25 |
| in_flight | 219.816 | 730 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.159 | 1.7 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 625984.102 | 1067098 |
| octopus_missing_reports_total | 1286.796 | 2183 |
| octopus_mqtt_received_total | 67992.898 | 108842 |
| octopus_mqtt_sent_total | 155474.592 | 262730 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 8837.937 | 11571.4 |
| produced_total | 311895.49 | 526463 |
| publish_mb_per_s | 31.688 | 59.56 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 88,167 records are Completed |
| record_counts | pass | 88,167 records found, 88,167 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 291 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 563,574 events applied by workers, 563,574 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 7,103 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 266 ms, max 541 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 5724 seq 24: ready 183 ms before start, 183 ms of it with an idle slot in group 197
- [cross_source_blocking] source 1628 seq 10: ready 187 ms before start, 187 ms of it with an idle slot in group 49
- [cross_source_blocking] source 15260 seq 11: ready 187 ms before start, 187 ms of it with an idle slot in group 477
- [cross_source_blocking] source 7740 seq 11: ready 187 ms before start, 187 ms of it with an idle slot in group 263
- [cross_source_blocking] source 11676 seq 14: ready 187 ms before start, 187 ms of it with an idle slot in group 379
- [cross_source_blocking] source 4156 seq 16: ready 255 ms before start, 255 ms of it with an idle slot in group 136
- [cross_source_blocking] source 15996 seq 17: ready 255 ms before start, 255 ms of it with an idle slot in group 499
- [cross_source_blocking] source 6524 seq 21: ready 251 ms before start, 251 ms of it with an idle slot in group 222
- [cross_source_blocking] source 11260 seq 5: ready 255 ms before start, 255 ms of it with an idle slot in group 366
- [cross_source_blocking] source 13756 seq 20: ready 252 ms before start, 252 ms of it with an idle slot in group 438
- [cross_source_blocking] source 14492 seq 8: ready 255 ms before start, 255 ms of it with an idle slot in group 461
- [cross_source_blocking] source 4732 seq 6: ready 255 ms before start, 255 ms of it with an idle slot in group 163
- [cross_source_blocking] source 14812 seq 11: ready 256 ms before start, 256 ms of it with an idle slot in group 465
- [cross_source_blocking] source 11452 seq 5: ready 255 ms before start, 255 ms of it with an idle slot in group 372
- [cross_source_blocking] source 1308 seq 33: ready 256 ms before start, 256 ms of it with an idle slot in group 41
- [cross_source_blocking] source 10876 seq 14: ready 256 ms before start, 256 ms of it with an idle slot in group 354
- [cross_source_blocking] source 9564 seq 18: ready 252 ms before start, 252 ms of it with an idle slot in group 332
- [cross_source_blocking] source 7740 seq 14: ready 253 ms before start, 253 ms of it with an idle slot in group 263
- [cross_source_blocking] source 6908 seq 29: ready 256 ms before start, 256 ms of it with an idle slot in group 235
- [cross_source_blocking] source 6332 seq 18: ready 256 ms before start, 256 ms of it with an idle slot in group 215
- [cross_source_blocking] source 2588 seq 15: ready 255 ms before start, 255 ms of it with an idle slot in group 80

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 263 | 263 | 0 | 18481.151 | 31588.351 | 31981.567 |
| t-247 | 8 | 1 | 30.815 | 4.104 | 506 | 0 | 165 | 165 | 0 | 20054.015 | 30670.847 | 31195.135 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 61 | 61 | 0 | 19136.511 | 30408.703 | 30408.703 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 540 | 540 | 0 | 18219.007 | 30408.703 | 31326.207 |
| t-311 | 15 | 1 | 28.322 | 7.37 | 761 | 0 | 293 | 293 | 0 | 19005.439 | 30277.631 | 31326.207 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 255 | 255 | 0 | 19529.727 | 30146.559 | 30408.703 |
| t-190 | 6 | 1 | 85.928 | 8.971 | 241 | 0 | 288 | 288 | 0 | 21233.663 | 30146.559 | 30539.775 |
| t-216 | 19 | 1 | 75.625 | 24.458 | 3889 | 0 | 905 | 905 | 0 | 18087.935 | 30146.559 | 31064.063 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 1189 | 1189 | 0 | 21102.591 | 30146.559 | 30801.919 |
| t-360 | 14 | 1 | 82.459 | 19.32 | 274 | 0 | 648 | 648 | 0 | 19660.799 | 30146.559 | 30670.847 |
| t-426 | 14 | 1 | 38.666 | 8.907 | 830 | 0 | 350 | 350 | 0 | 19660.799 | 30146.559 | 30277.631 |
| t-482 | 10 | 1 | 83.28 | 14.168 | 2234 | 0 | 483 | 483 | 0 | 18612.223 | 30146.559 | 30801.919 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1054 | 1054 | 0 | 18612.223 | 30015.487 | 32767.999 |
| t-009 | 29 | 1 | 23.894 | 11.467 | 2407 | 0 | 435 | 435 | 0 | 17825.791 | 30015.487 | 30539.775 |
| t-029 | 28 | 2 | 177.66 | 82.383 | 1732 | 0 | 2807 | 2807 | 0 | 18743.295 | 30015.487 | 31195.135 |
| t-033 | 6 | 1 | 87.319 | 9.519 | 859 | 0 | 300 | 300 | 0 | 20316.159 | 30015.487 | 30408.703 |
| t-035 | 22 | 1 | 58.172 | 22.212 | 264 | 0 | 817 | 817 | 0 | 17694.719 | 30015.487 | 30801.919 |
| t-042 | 20 | 1 | 53.552 | 19.032 | 209 | 0 | 631 | 631 | 0 | 19398.655 | 30015.487 | 31195.135 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 2974 | 2974 | 0 | 18743.295 | 30015.487 | 31326.207 |
| t-116 | 24 | 1 | 65.702 | 25.166 | 3734 | 0 | 860 | 860 | 0 | 18087.935 | 30015.487 | 30408.703 |
| t-177 | 19 | 1 | 47.607 | 15.175 | 615 | 0 | 508 | 508 | 0 | 19660.799 | 30015.487 | 30277.631 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 1588 | 1588 | 0 | 18874.367 | 30015.487 | 31326.207 |
| t-261 | 51 | 2 | 73.695 | 62.946 | 2342 | 0 | 2198 | 2198 | 0 | 18481.151 | 30015.487 | 30670.847 |
| t-268 | 41 | 1 | 45.858 | 30.087 | 1430 | 0 | 1093 | 1093 | 0 | 18087.935 | 30015.487 | 30539.775 |
| t-336 | 11 | 1 | 50.302 | 9.444 | 1886 | 0 | 326 | 326 | 0 | 18874.367 | 30015.487 | 30670.847 |
