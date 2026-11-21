# Ki-mysql-minimal — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.11s, closing 1.299s, drain 2.053s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000511 | 1000511 | 16499.376 | 16504.453 | 28192.7 | 0 | 0 | 0 | 458 | 5163 | 26258 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797147 | 1121.196 | 250.239 | 3096.575 | 4059.135 | 5181.439 | 8585.215 |
| e2e_from_send | 797147 | 102.205 | 38.719 | 200.063 | 1405.951 | 2805.759 | 5378.047 |
| producer_lag | 797147 | 1018.992 | 78.399 | 2942.975 | 3860.479 | 4272.127 | 4694.015 |
| publish_call | 1000511 | 72.312 | 35.647 | 178.431 | 485.375 | 856.575 | 882.175 |
| queue_wait | 797147 | 91.455 | 38.175 | 194.559 | 884.223 | 2402.303 | 4755.455 |
| broker_dwell | 797147 | 30.313 | 4.435 | 28.447 | 733.695 | 2355.199 | 4726.783 |
| receive_call | 857151 | 40.531 | 14.055 | 111.231 | 343.807 | 481.535 | 515.327 |
| ctx_load | 797147 | 0.002 | 0.002 | 0.003 | 0.01 | 0.045 | 19.551 |
| process | 797147 | 10.729 | 0.001 | 0.002 | 0.014 | 2617.343 | 3002.367 |
| ctx_commit | 797147 | 0.002 | 0.001 | 0.001 | 0.004 | 0.024 | 28.111 |
| ack | 780912 | 0.003 | 0.002 | 0.004 | 0.015 | 0.06 | 30.191 |
| service | 797147 | 10.75 | 0.013 | 0.033 | 0.124 | 2617.343 | 3002.367 |
| e2e_large | 356 | 3578.92 | 3299.327 | 5746.687 | 7020.543 | 7380.991 | 7380.991 |
| ready_wait | 797147 | 13.069 | 2.365 | 25.263 | 198.783 | 432.639 | 1482.751 |
| blocked | 797147 | 13.013 | 2.365 | 25.247 | 197.503 | 425.215 | 731.135 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16504.453 | 26977 |
| applied_total | 561617.735 | 961614 |
| bench_cpu_cores | 3.221 | 4.854 |
| bench_cpu_pct | 13.421 | 20.23 |
| bench_gc_gen0 | 2.653 | 4 |
| bench_gc_gen1 | 1.143 | 2 |
| bench_gc_gen2 | 0.673 | 1 |
| bench_gc_heap_mb | 1050.343 | 1306.7 |
| bench_mem_mb | 1587.914 | 1687.1 |
| bench_threadpool_queue | 9.367 | 118 |
| bench_threadpool_threads | 43.612 | 44 |
| broker_backlog | 4753.776 | 7631 |
| ctr_eb_mosquitto_cpu_pct | 46.699 | 56.22 |
| ctr_eb_mosquitto_mem_mb | 11.096 | 28.5 |
| ctr_eb_mssql_cpu_pct | 2.217 | 5.09 |
| ctr_eb_mssql_mem_mb | 6993.053 | 6994.2 |
| ctr_eb_mysql_cpu_pct | 437.172 | 511.65 |
| ctr_eb_mysql_mem_mb | 9130.708 | 9428 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 14.912 | 15.1 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 15.118 | 15.4 |
| ctr_eb_pgbouncer_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.318 | 16.6 |
| ctr_eb_postgres_cpu_pct | 1.053 | 6.37 |
| ctr_eb_postgres_mem_mb | 4148.665 | 4188.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 41.547 | 54.28 |
| in_flight | 653.571 | 4641 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.358 | 1.69 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1108618.061 | 1916860 |
| octopus_missing_reports_total | 383.306 | 997 |
| octopus_mqtt_received_total | 131803.51 | 257347 |
| octopus_mqtt_sent_total | 266268.939 | 496371 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16499.376 | 26955 |
| produced_total | 562271.306 | 962027 |
| publish_mb_per_s | 53.905 | 110.63 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,872 records are Completed |
| record_counts | pass | 138,872 records found, 138,872 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,511 events applied by workers, 1,000,511 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 26,258 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 198 ms, max 731 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9691 seq 13: ready 133 ms before start, 133 ms of it with an idle slot in group 337
- [cross_source_blocking] source 13346 seq 22: ready 181 ms before start, 181 ms of it with an idle slot in group 427
- [cross_source_blocking] source 14594 seq 15: ready 181 ms before start, 181 ms of it with an idle slot in group 461
- [cross_source_blocking] source 2754 seq 10: ready 175 ms before start, 175 ms of it with an idle slot in group 84
- [cross_source_blocking] source 14402 seq 9: ready 181 ms before start, 181 ms of it with an idle slot in group 459
- [cross_source_blocking] source 13730 seq 15: ready 175 ms before start, 175 ms of it with an idle slot in group 437
- [cross_source_blocking] source 13986 seq 8: ready 182 ms before start, 182 ms of it with an idle slot in group 444
- [cross_source_blocking] source 4610 seq 21: ready 182 ms before start, 182 ms of it with an idle slot in group 161
- [cross_source_blocking] source 14242 seq 36: ready 182 ms before start, 182 ms of it with an idle slot in group 456
- [cross_source_blocking] source 2434 seq 9: ready 182 ms before start, 182 ms of it with an idle slot in group 71
- [cross_source_blocking] source 15490 seq 7: ready 182 ms before start, 182 ms of it with an idle slot in group 483
- [cross_source_blocking] source 4098 seq 8: ready 185 ms before start, 185 ms of it with an idle slot in group 136
- [cross_source_blocking] source 1538 seq 20: ready 186 ms before start, 186 ms of it with an idle slot in group 46
- [cross_source_blocking] source 11074 seq 9: ready 185 ms before start, 185 ms of it with an idle slot in group 362
- [cross_source_blocking] source 12130 seq 10: ready 182 ms before start, 182 ms of it with an idle slot in group 388
- [cross_source_blocking] source 8098 seq 28: ready 179 ms before start, 179 ms of it with an idle slot in group 273
- [cross_source_blocking] source 11522 seq 40: ready 181 ms before start, 181 ms of it with an idle slot in group 374
- [cross_source_blocking] source 7202 seq 18: ready 181 ms before start, 181 ms of it with an idle slot in group 245
- [cross_source_blocking] source 4450 seq 9: ready 182 ms before start, 182 ms of it with an idle slot in group 153
- [cross_source_blocking] source 10370 seq 26: ready 185 ms before start, 185 ms of it with an idle slot in group 347

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2801.663 | 6848.511 | 7077.887 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2834.431 | 6750.207 | 7405.567 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 3014.655 | 6651.903 | 7208.959 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2490.367 | 6324.223 | 7274.495 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 739 | 739 | 0 | 1064.959 | 6225.919 | 7962.623 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 253.951 | 4980.735 | 6127.615 |
| t-187 | 6 | 1 | 69.719 | 6.936 | 685 | 0 | 400 | 400 | 0 | 174.079 | 4849.663 | 5472.255 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 400 | 400 | 0 | 835.583 | 4620.287 | 5734.399 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 222.207 | 4554.751 | 6848.511 |
| t-191 | 11 | 1 | 47.373 | 8.424 | 3529 | 0 | 563 | 563 | 0 | 140.287 | 4554.751 | 5636.095 |
| t-360 | 14 | 1 | 82.459 | 19.32 | 274 | 0 | 1187 | 1187 | 0 | 196.607 | 4521.983 | 5636.095 |
| t-056 | 7 | 1 | 59.8 | 7.224 | 347 | 0 | 447 | 447 | 0 | 123.903 | 4489.215 | 6193.151 |
| t-424 | 8 | 1 | 94.679 | 12.424 | 1700 | 0 | 746 | 746 | 0 | 342.015 | 4489.215 | 6029.311 |
| t-339 | 17 | 1 | 15.431 | 4.427 | 134 | 0 | 323 | 323 | 0 | 190.463 | 4423.679 | 6553.599 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 943 | 943 | 0 | 211.967 | 4423.679 | 5439.487 |
| t-012 | 6 | 1 | 76.677 | 7.946 | 556 | 0 | 505 | 505 | 0 | 299.007 | 4390.911 | 5308.415 |
| t-017 | 19 | 1 | 36.957 | 11.447 | 830 | 0 | 776 | 776 | 0 | 252.927 | 4390.911 | 5799.935 |
| t-063 | 8 | 1 | 24.376 | 3.378 | 1296 | 0 | 217 | 217 | 0 | 370.687 | 4390.911 | 4423.679 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3206 | 3206 | 0 | 226.303 | 4390.911 | 6193.151 |
| t-336 | 11 | 1 | 50.302 | 9.444 | 1886 | 0 | 624 | 624 | 0 | 232.447 | 4390.911 | 5373.951 |
| t-002 | 17 | 1 | 102.561 | 29.203 | 377 | 0 | 1882 | 1882 | 0 | 380.927 | 4358.143 | 6094.847 |
| t-053 | 11 | 1 | 14.325 | 2.693 | 3589 | 0 | 234 | 234 | 0 | 303.103 | 4358.143 | 5275.647 |
| t-146 | 6 | 1 | 53.681 | 5.759 | 246 | 0 | 376 | 376 | 0 | 602.111 | 4358.143 | 5931.007 |
| t-190 | 6 | 1 | 85.928 | 8.971 | 241 | 0 | 551 | 551 | 0 | 325.631 | 4358.143 | 5079.039 |
| t-223 | 13 | 1 | 133.399 | 28.216 | 2372 | 0 | 1722 | 1722 | 0 | 360.447 | 4358.143 | 5570.559 |
