# D3-32k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 926 workers, offered 31870.178 ev/s
- Phases: run 60.154s, closing 0.752s, drain 3.712s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1957863 | 1957863 | 31873.28 | 31860.798 | 51505 | 0 | 0 | 0 | 453 | 9862 | 18792 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1593691 | 67.465 | 14.919 | 59.711 | 1748.991 | 2861.055 | 7614.463 |
| e2e_from_send | 1593691 | 65.027 | 13.399 | 51.935 | 1745.919 | 2856.959 | 7614.463 |
| producer_lag | 1593691 | 2.439 | 0.788 | 4.727 | 29.471 | 70.271 | 258.431 |
| publish_call | 1957863 | 16.753 | 11.191 | 34.975 | 80.447 | 135.807 | 221.695 |
| queue_wait | 1593691 | 54.715 | 13.327 | 50.047 | 1437.695 | 2633.727 | 6705.151 |
| broker_dwell | 1593691 | 43.424 | 3.235 | 27.135 | 1425.407 | 2621.439 | 6696.959 |
| receive_call | 1756041 | 28.71 | 11.759 | 66.495 | 277.503 | 458.495 | 543.743 |
| ctx_load | 1593691 | 0.003 | 0.002 | 0.004 | 0.014 | 0.068 | 35.167 |
| process | 1593691 | 10.29 | 0.001 | 0.002 | 0.015 | 2607.103 | 3026.943 |
| ctx_commit | 1593691 | 0.003 | 0.001 | 0.001 | 0.004 | 0.032 | 55.615 |
| ack | 1579218 | 0.003 | 0.002 | 0.004 | 0.016 | 0.077 | 42.367 |
| service | 1593691 | 10.312 | 0.012 | 0.029 | 0.136 | 2607.103 | 3026.943 |
| e2e_large | 345 | 2419.685 | 2371.583 | 3135.487 | 6307.839 | 7614.463 | 7614.463 |
| ready_wait | 1593691 | 7.195 | 2.083 | 14.031 | 108.671 | 200.319 | 1442.815 |
| blocked | 1593691 | 7.185 | 2.083 | 14.031 | 108.543 | 199.935 | 279.295 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 31860.798 | 34635.2 |
| applied_total | 1148738.245 | 1913182 |
| bench_cpu_cores | 8.473 | 9.142 |
| bench_cpu_pct | 35.305 | 38.09 |
| bench_gc_gen0 | 3.857 | 6 |
| bench_gc_gen1 | 1.388 | 3 |
| bench_gc_gen2 | 0.612 | 2 |
| bench_gc_heap_mb | 1185.078 | 1451.6 |
| bench_mem_mb | 1937.722 | 2163.2 |
| bench_threadpool_queue | 21.143 | 180 |
| bench_threadpool_threads | 39.98 | 44 |
| broker_backlog | 10333.592 | 34678 |
| ctr_eb_mosquitto_cpu_pct | 74.317 | 81.09 |
| ctr_eb_mosquitto_mem_mb | 7.28 | 15 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.021 | 0.04 |
| ctr_eb_pgbouncer_2_mem_mb | 11.3 | 11.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.02 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 9.7 | 9.7 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 11.3 | 11.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.021 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 9.7 | 9.7 |
| ctr_eb_postgres_cpu_pct | 400.287 | 627.48 |
| ctr_eb_postgres_mem_mb | 5857.839 | 6181.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 64.505 | 72.99 |
| in_flight | 1582.98 | 3683 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.053 | 2.24 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 2272491.245 | 3811647 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 243323.02 | 404410 |
| octopus_mqtt_sent_total | 464769.776 | 812967 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 31873.28 | 34076.2 |
| produced_total | 1150321.224 | 1915111 |
| publish_mb_per_s | 79.29 | 111.14 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 246,446 records are Completed |
| record_counts | pass | 246,446 records found, 246,446 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 453 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,957,863 events applied by workers, 1,957,863 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 18,792 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 109 ms, max 279 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 4849 seq 22: ready 148 ms before start, 148 ms of it with an idle slot in group 168
- [cross_source_blocking] source 97 seq 14: ready 148 ms before start, 148 ms of it with an idle slot in group 1
- [cross_source_blocking] source 14937 seq 44: ready 148 ms before start, 148 ms of it with an idle slot in group 468
- [cross_source_blocking] source 7305 seq 8: ready 147 ms before start, 147 ms of it with an idle slot in group 250
- [cross_source_blocking] source 10293 seq 29: ready 148 ms before start, 148 ms of it with an idle slot in group 344
- [cross_source_blocking] source 3753 seq 8: ready 147 ms before start, 147 ms of it with an idle slot in group 126
- [cross_source_blocking] source 8457 seq 41: ready 147 ms before start, 147 ms of it with an idle slot in group 287
- [cross_source_blocking] source 11853 seq 30: ready 148 ms before start, 148 ms of it with an idle slot in group 383
- [cross_source_blocking] source 11885 seq 37: ready 148 ms before start, 148 ms of it with an idle slot in group 383
- [cross_source_blocking] source 4853 seq 14: ready 148 ms before start, 148 ms of it with an idle slot in group 168
- [cross_source_blocking] source 1297 seq 117: ready 148 ms before start, 148 ms of it with an idle slot in group 41
- [cross_source_blocking] source 3853 seq 22: ready 148 ms before start, 148 ms of it with an idle slot in group 131
- [cross_source_blocking] source 9301 seq 39: ready 148 ms before start, 148 ms of it with an idle slot in group 321
- [cross_source_blocking] source 11505 seq 74: ready 148 ms before start, 148 ms of it with an idle slot in group 374
- [cross_source_blocking] source 10681 seq 30: ready 139 ms before start, 139 ms of it with an idle slot in group 350
- [cross_source_blocking] source 15625 seq 32: ready 138 ms before start, 138 ms of it with an idle slot in group 486
- [cross_source_blocking] source 12697 seq 20: ready 138 ms before start, 138 ms of it with an idle slot in group 399
- [cross_source_blocking] source 5977 seq 29: ready 139 ms before start, 139 ms of it with an idle slot in group 206
- [cross_source_blocking] source 8693 seq 24: ready 140 ms before start, 140 ms of it with an idle slot in group 301
- [cross_source_blocking] source 15033 seq 22: ready 138 ms before start, 138 ms of it with an idle slot in group 469

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 169 | 169 | 0 | 1900.543 | 6717.439 | 7143.423 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 411 | 411 | 0 | 1908.735 | 5079.039 | 7634.943 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 86 | 86 | 0 | 1990.655 | 3604.479 | 3751.935 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 88 | 88 | 0 | 1540.095 | 3162.111 | 3244.031 |
| t-352 | 16 | 2 | 258.942 | 68.066 | 2088 | 0 | 4093 | 4093 | 0 | 15.423 | 2850.815 | 5308.415 |
| t-484 | 6 | 1 | 185.857 | 18.813 | 232 | 0 | 1114 | 1114 | 0 | 14.975 | 2818.047 | 4292.607 |
| t-437 | 9 | 1 | 143.786 | 21.588 | 1067 | 0 | 1336 | 1336 | 0 | 15.231 | 2768.895 | 4292.607 |
| t-295 | 12 | 1 | 137.916 | 27.397 | 3396 | 0 | 1679 | 1679 | 0 | 14.847 | 2654.207 | 3260.415 |
| t-106 | 24 | 4 | 441.577 | 179.54 | 824 | 0 | 10797 | 10797 | 0 | 16.767 | 2621.439 | 4980.735 |
| t-299 | 21 | 1 | 105.04 | 35.921 | 646 | 0 | 2177 | 2177 | 0 | 15.167 | 2424.831 | 4259.839 |
| t-132 | 22 | 3 | 374.037 | 127.192 | 2676 | 0 | 7578 | 7578 | 0 | 16.063 | 2408.447 | 4259.839 |
| t-355 | 10 | 1 | 74.959 | 13.142 | 349 | 0 | 818 | 818 | 0 | 15.167 | 2392.063 | 2932.735 |
| t-441 | 30 | 4 | 350.391 | 169.932 | 2203 | 0 | 10222 | 10222 | 0 | 15.807 | 2392.063 | 4849.663 |
| t-079 | 6 | 1 | 239.718 | 24.649 | 1522 | 0 | 1452 | 1452 | 0 | 15.039 | 2375.679 | 3309.567 |
| t-244 | 11 | 1 | 79.359 | 14.812 | 1151 | 0 | 891 | 891 | 0 | 14.079 | 2375.679 | 2899.967 |
| t-041 | 34 | 6 | 482.299 | 271.676 | 2732 | 0 | 16418 | 16418 | 0 | 16.319 | 2359.295 | 3964.927 |
| t-320 | 14 | 1 | 213.66 | 47.36 | 574 | 0 | 2862 | 2862 | 0 | 15.935 | 2359.295 | 3047.423 |
| t-456 | 21 | 3 | 356.748 | 121.49 | 512 | 0 | 7210 | 7210 | 0 | 15.743 | 2326.527 | 3457.023 |
| t-246 | 32 | 5 | 465.55 | 245.973 | 494 | 0 | 15011 | 15011 | 0 | 16.191 | 2293.759 | 4784.127 |
| t-077 | 10 | 2 | 356.142 | 59.44 | 1566 | 0 | 3495 | 3495 | 0 | 16.063 | 2260.991 | 3014.655 |
| t-314 | 24 | 4 | 465.22 | 185.373 | 525 | 0 | 11219 | 11219 | 0 | 15.935 | 2260.991 | 4292.607 |
| t-374 | 28 | 5 | 443.538 | 205.203 | 1811 | 0 | 12402 | 12402 | 0 | 16.063 | 2260.991 | 3932.159 |
| t-288 | 6 | 1 | 143.984 | 14.564 | 214 | 0 | 895 | 895 | 0 | 15.487 | 2228.223 | 2981.887 |
| t-427 | 16 | 2 | 275.669 | 73.172 | 1711 | 0 | 4361 | 4361 | 0 | 15.615 | 2228.223 | 3751.935 |
| t-322 | 27 | 2 | 161.378 | 70.793 | 2198 | 0 | 4474 | 4474 | 0 | 15.039 | 2211.839 | 3014.655 |
