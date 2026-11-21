# Id-mssql-im — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.325s, closing 3.28s, drain 0.261s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 662611 | 662611 | 10236.429 | 10236.012 | 16957.1 | 0 | 0 | 0 | 328 | 3365 | 13022 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 461203 | 12948.426 | 12673.023 | 20463.615 | 23429.119 | 25329.663 | 27492.351 |
| e2e_from_send | 461203 | 201.978 | 191.231 | 317.439 | 1205.247 | 2852.863 | 5324.799 |
| producer_lag | 461203 | 12746.448 | 12460.031 | 20250.623 | 23216.127 | 24821.759 | 26558.463 |
| publish_call | 662611 | 167.152 | 174.975 | 290.815 | 456.959 | 933.375 | 1290.239 |
| queue_wait | 461203 | 191.216 | 190.847 | 313.599 | 551.935 | 2275.327 | 4743.167 |
| broker_dwell | 461203 | 18.089 | 3.849 | 13.287 | 207.103 | 2123.775 | 4710.399 |
| receive_call | 545433 | 64.542 | 36.895 | 161.407 | 386.047 | 485.119 | 508.671 |
| ctx_load | 461203 | 0.002 | 0.002 | 0.003 | 0.007 | 0.036 | 33.247 |
| process | 461203 | 10.74 | 0.001 | 0.002 | 0.011 | 2607.103 | 3000.319 |
| ctx_commit | 461203 | 0.001 | 0.001 | 0.001 | 0.004 | 0.019 | 23.983 |
| ack | 448593 | 0.004 | 0.002 | 0.004 | 0.012 | 0.052 | 37.983 |
| service | 461203 | 10.762 | 0.013 | 0.035 | 0.115 | 2607.103 | 3000.319 |
| e2e_large | 226 | 14351.238 | 13328.383 | 21446.655 | 26460.159 | 27459.583 | 27459.583 |
| ready_wait | 461203 | 11.125 | 3.073 | 11.311 | 230.527 | 354.047 | 629.759 |
| blocked | 461203 | 11.123 | 3.073 | 11.311 | 230.527 | 353.791 | 629.759 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 10236.012 | 13323.1 |
| applied_total | 380130.592 | 624586 |
| bench_cpu_cores | 1.585 | 2.073 |
| bench_cpu_pct | 6.604 | 8.64 |
| bench_gc_gen0 | 1.98 | 3 |
| bench_gc_gen1 | 0.959 | 2 |
| bench_gc_gen2 | 0.653 | 2 |
| bench_gc_heap_mb | 926.645 | 1191.1 |
| bench_mem_mb | 1522.255 | 1648.1 |
| bench_threadpool_queue | 5.755 | 133 |
| bench_threadpool_threads | 43.531 | 44 |
| broker_backlog | 1428.714 | 2321 |
| ctr_eb_mosquitto_cpu_pct | 29.521 | 37.41 |
| ctr_eb_mosquitto_mem_mb | 6.684 | 13.3 |
| ctr_eb_mssql_cpu_pct | 699.791 | 873.29 |
| ctr_eb_mssql_mem_mb | 7481.892 | 7494 |
| ctr_eb_mysql_cpu_pct | 0.653 | 0.71 |
| ctr_eb_mysql_mem_mb | 10557.2 | 10557.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.014 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 14.6 | 14.6 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.013 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 14.7 | 14.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.015 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 14.9 | 14.9 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 16.1 | 16.1 |
| ctr_eb_postgres_cpu_pct | 0.791 | 1.9 |
| ctr_eb_postgres_mem_mb | 3120.89 | 3124.2 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 45.961 | 63.01 |
| in_flight | 291.408 | 1183 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.315 | 1.86 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 761163.041 | 1254246 |
| octopus_missing_reports_total | 1696.612 | 2883 |
| octopus_mqtt_received_total | 82242.02 | 126101 |
| octopus_mqtt_sent_total | 192417.857 | 312759 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 10236.429 | 12617.3 |
| produced_total | 380422 | 624830 |
| publish_mb_per_s | 35.244 | 65.16 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 99,906 records are Completed |
| record_counts | pass | 99,906 records found, 99,906 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 328 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 662,611 events applied by workers, 662,611 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 13,022 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 231 ms, max 630 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14364 seq 3: ready 212 ms before start, 212 ms of it with an idle slot in group 458
- [cross_source_blocking] source 11100 seq 13: ready 214 ms before start, 214 ms of it with an idle slot in group 362
- [cross_source_blocking] source 10396 seq 13: ready 214 ms before start, 214 ms of it with an idle slot in group 347
- [cross_source_blocking] source 10684 seq 12: ready 214 ms before start, 214 ms of it with an idle slot in group 350
- [cross_source_blocking] source 1916 seq 25: ready 215 ms before start, 215 ms of it with an idle slot in group 55
- [cross_source_blocking] source 7132 seq 16: ready 185 ms before start, 185 ms of it with an idle slot in group 242
- [cross_source_blocking] source 5788 seq 6: ready 212 ms before start, 212 ms of it with an idle slot in group 200
- [cross_source_blocking] source 10332 seq 16: ready 214 ms before start, 214 ms of it with an idle slot in group 347
- [cross_source_blocking] source 2940 seq 9: ready 185 ms before start, 185 ms of it with an idle slot in group 94
- [cross_source_blocking] source 2332 seq 21: ready 214 ms before start, 214 ms of it with an idle slot in group 67
- [cross_source_blocking] source 988 seq 29: ready 214 ms before start, 214 ms of it with an idle slot in group 29
- [cross_source_blocking] source 11132 seq 15: ready 212 ms before start, 212 ms of it with an idle slot in group 363
- [cross_source_blocking] source 15132 seq 6: ready 185 ms before start, 185 ms of it with an idle slot in group 472
- [cross_source_blocking] source 2012 seq 12: ready 185 ms before start, 185 ms of it with an idle slot in group 59
- [cross_source_blocking] source 3228 seq 18: ready 212 ms before start, 212 ms of it with an idle slot in group 108
- [cross_source_blocking] source 8572 seq 16: ready 185 ms before start, 185 ms of it with an idle slot in group 295
- [cross_source_blocking] source 15644 seq 14: ready 214 ms before start, 214 ms of it with an idle slot in group 488
- [cross_source_blocking] source 10204 seq 19: ready 185 ms before start, 185 ms of it with an idle slot in group 344
- [cross_source_blocking] source 12252 seq 4: ready 212 ms before start, 212 ms of it with an idle slot in group 389
- [cross_source_blocking] source 9660 seq 16: ready 184 ms before start, 184 ms of it with an idle slot in group 334

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-038 | 9 | 1 | 55.117 | 8.286 | 2056 | 0 | 320 | 320 | 0 | 14680.063 | 26607.615 | 27525.119 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 65 | 65 | 0 | 15204.351 | 26345.471 | 26345.471 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 297 | 297 | 0 | 12779.519 | 25559.039 | 27525.119 |
| t-177 | 19 | 1 | 47.607 | 15.175 | 615 | 0 | 619 | 619 | 0 | 14155.775 | 25165.823 | 26476.543 |
| t-489 | 6 | 1 | 173.223 | 17.681 | 1030 | 0 | 672 | 672 | 0 | 14155.775 | 25165.823 | 26476.543 |
| t-498 | 10 | 1 | 42.063 | 6.745 | 292 | 0 | 290 | 290 | 0 | 14352.383 | 25165.823 | 25559.039 |
| t-057 | 17 | 1 | 79.959 | 23.695 | 290 | 0 | 907 | 907 | 0 | 13631.487 | 25034.751 | 25952.255 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 135 | 135 | 0 | 13697.023 | 25034.751 | 26607.615 |
| t-064 | 11 | 1 | 57.27 | 10.782 | 263 | 0 | 468 | 468 | 0 | 12582.911 | 24772.607 | 26476.543 |
| t-115 | 10 | 1 | 41.317 | 6.608 | 3658 | 0 | 278 | 278 | 0 | 13500.415 | 24772.607 | 25034.751 |
| t-196 | 21 | 1 | 116.998 | 41.352 | 2244 | 0 | 1653 | 1653 | 0 | 13434.879 | 24772.607 | 26476.543 |
| t-024 | 14 | 1 | 41.39 | 9.449 | 1743 | 0 | 394 | 394 | 0 | 14417.919 | 24641.535 | 26607.615 |
| t-118 | 15 | 1 | 29.889 | 7.561 | 138 | 0 | 321 | 321 | 0 | 13893.631 | 24641.535 | 25821.183 |
| t-159 | 6 | 1 | 57.913 | 5.542 | 295 | 0 | 267 | 267 | 0 | 11141.119 | 24641.535 | 24903.679 |
| t-191 | 11 | 1 | 47.373 | 8.424 | 3529 | 0 | 351 | 351 | 0 | 13434.879 | 24641.535 | 25034.751 |
| t-017 | 19 | 1 | 36.957 | 11.447 | 830 | 0 | 511 | 511 | 0 | 12386.303 | 24510.463 | 26476.543 |
| t-033 | 6 | 1 | 87.319 | 9.519 | 859 | 0 | 357 | 357 | 0 | 14811.135 | 24510.463 | 25821.183 |
| t-034 | 7 | 1 | 32.67 | 3.86 | 2831 | 0 | 172 | 172 | 0 | 11665.407 | 24510.463 | 24641.535 |
| t-081 | 6 | 1 | 16.193 | 1.614 | 2170 | 0 | 76 | 76 | 0 | 14876.671 | 24510.463 | 24510.463 |
| t-112 | 6 | 1 | 57.527 | 5.908 | 774 | 0 | 227 | 227 | 0 | 14811.135 | 24510.463 | 26607.615 |
| t-184 | 20 | 1 | 98.647 | 32.052 | 161 | 0 | 1256 | 1256 | 0 | 13434.879 | 24510.463 | 24772.607 |
| t-289 | 6 | 1 | 83.389 | 8.934 | 1711 | 0 | 395 | 395 | 0 | 11141.119 | 24510.463 | 24772.607 |
| t-487 | 9 | 1 | 120.825 | 17.666 | 755 | 0 | 705 | 705 | 0 | 14221.311 | 24510.463 | 26345.471 |
| t-031 | 11 | 1 | 85.948 | 15.65 | 463 | 0 | 648 | 648 | 0 | 14155.775 | 24379.391 | 26476.543 |
| t-149 | 13 | 1 | 42.874 | 8.748 | 3034 | 0 | 326 | 326 | 0 | 14417.919 | 24379.391 | 24641.535 |
