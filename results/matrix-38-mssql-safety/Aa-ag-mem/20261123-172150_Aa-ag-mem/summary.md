# Aa-ag-mem — FAIL ❌

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.214s, closing 1.908s, drain 1.352s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000209 | 1000209 | 16084.618 | 16087.637 | 22728.6 | 0 | 0 | 0 | 458 | 5161 | 7852 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796844 | 125.902 | 32.223 | 158.847 | 1651.711 | 2889.727 | 7999.487 |
| e2e_from_send | 796844 | 69.239 | 29.567 | 86.079 | 1342.463 | 2791.423 | 7995.391 |
| producer_lag | 796844 | 56.664 | 1.582 | 24.783 | 1293.311 | 1639.423 | 1973.247 |
| publish_call | 1000209 | 51.171 | 31.551 | 99.711 | 333.823 | 663.551 | 1391.615 |
| queue_wait | 796844 | 58.472 | 29.455 | 83.519 | 721.919 | 2332.671 | 5574.655 |
| broker_dwell | 796844 | 24.234 | 3.825 | 18.943 | 680.959 | 2301.951 | 5447.679 |
| receive_call | 858538 | 41.991 | 20.767 | 103.231 | 322.815 | 471.807 | 546.303 |
| ctx_load | 796844 | 0.003 | 0.002 | 0.003 | 0.012 | 0.074 | 59.519 |
| process | 796844 | 10.734 | 0.001 | 0.002 | 0.018 | 2619.391 | 3006.463 |
| ctx_commit | 796844 | 0.002 | 0.001 | 0.001 | 0.004 | 0.034 | 57.055 |
| ack | 795528 | 0.006 | 0.003 | 0.007 | 0.026 | 0.146 | 60.319 |
| service | 796844 | 10.767 | 0.013 | 0.039 | 0.284 | 2619.391 | 3006.463 |
| e2e_large | 356 | 2831.829 | 2723.839 | 3919.871 | 5664.767 | 7999.487 | 7999.487 |
| ready_wait | 796844 | 10.564 | 1.731 | 30.639 | 99.903 | 299.263 | 1514.495 |
| blocked | 796844 | 10.497 | 1.731 | 30.623 | 99.711 | 297.471 | 1361.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16087.637 | 18002.5 |
| applied_total | 577445.98 | 961273 |
| bench_cpu_cores | 4.55 | 5.23 |
| bench_cpu_pct | 18.958 | 21.79 |
| bench_gc_gen0 | 3 | 5 |
| bench_gc_gen1 | 1.347 | 3 |
| bench_gc_gen2 | 0.837 | 2 |
| bench_gc_heap_mb | 925.967 | 1215.7 |
| bench_mem_mb | 1653.349 | 1759.4 |
| bench_threadpool_queue | 42.816 | 171 |
| bench_threadpool_threads | 33.653 | 35 |
| broker_backlog | 3243.816 | 4876 |
| ctr_eb_ag1_cpu_pct | 741.648 | 917.97 |
| ctr_eb_ag1_mem_mb | 7203.282 | 7236.5 |
| ctr_eb_ag2_cpu_pct | 170.738 | 201.68 |
| ctr_eb_ag2_mem_mb | 6442.292 | 6481.2 |
| ctr_eb_ag_haproxy_cpu_pct | 27.334 | 30.34 |
| ctr_eb_ag_haproxy_mem_mb | 22.496 | 25 |
| ctr_eb_mysql_cpu_pct | 0.723 | 0.76 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.02 |
| ctr_eb_pgbouncer_mem_mb | 7.8 | 7.8 |
| ctr_eb_postgres_cpu_pct | 0.105 | 0.32 |
| ctr_eb_postgres_mem_mb | 2312.7 | 2312.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 69.64 | 79.07 |
| in_flight | 560.143 | 2176 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 2.338 | 2.58 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1150182.143 | 1927997 |
| octopus_missing_reports_total | 7347.245 | 12311 |
| octopus_mqtt_received_total | 145942.796 | 258936 |
| octopus_mqtt_sent_total | 317701.755 | 541894 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| produced_per_s | 16084.618 | 18065.5 |
| produced_total | 578006.122 | 961636 |
| publish_mb_per_s | 52.412 | 75.31 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,833 records are Completed |
| record_counts | pass | 138,833 records found, 138,833 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,209 events applied by workers, 1,000,209 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 7,852 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 100 ms, max 1362 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 10667 seq 14: ready 121 ms before start, 121 ms of it with an idle slot in group 350
- [cross_source_blocking] source 6507 seq 12: ready 120 ms before start, 120 ms of it with an idle slot in group 222
- [cross_source_blocking] source 15797 seq 9: ready 101 ms before start, 101 ms of it with an idle slot in group 494
- [cross_source_blocking] source 15666 seq 8: ready 104 ms before start, 104 ms of it with an idle slot in group 488
- [cross_source_blocking] source 4601 seq 13: ready 105 ms before start, 105 ms of it with an idle slot in group 160
- [cross_source_blocking] source 15773 seq 13: ready 103 ms before start, 103 ms of it with an idle slot in group 494
- [cross_source_blocking] source 14237 seq 31: ready 103 ms before start, 103 ms of it with an idle slot in group 456
- [cross_source_blocking] source 2457 seq 11: ready 101 ms before start, 101 ms of it with an idle slot in group 72
- [cross_source_blocking] source 15497 seq 9: ready 105 ms before start, 105 ms of it with an idle slot in group 483
- [cross_source_blocking] source 14578 seq 14: ready 107 ms before start, 107 ms of it with an idle slot in group 461
- [cross_source_blocking] source 3358 seq 9: ready 107 ms before start, 107 ms of it with an idle slot in group 116
- [cross_source_blocking] source 4306 seq 34: ready 107 ms before start, 107 ms of it with an idle slot in group 147
- [cross_source_blocking] source 7582 seq 13: ready 107 ms before start, 107 ms of it with an idle slot in group 261
- [cross_source_blocking] source 13422 seq 4: ready 107 ms before start, 107 ms of it with an idle slot in group 429
- [cross_source_blocking] source 222 seq 17: ready 107 ms before start, 107 ms of it with an idle slot in group 5
- [cross_source_blocking] source 3950 seq 17: ready 107 ms before start, 107 ms of it with an idle slot in group 131
- [cross_source_blocking] source 8466 seq 17: ready 107 ms before start, 107 ms of it with an idle slot in group 287
- [cross_source_blocking] source 5554 seq 20: ready 107 ms before start, 107 ms of it with an idle slot in group 190
- [cross_source_blocking] source 3102 seq 15: ready 107 ms before start, 107 ms of it with an idle slot in group 103
- [cross_source_blocking] source 8094 seq 14: ready 107 ms before start, 107 ms of it with an idle slot in group 273

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 183 | 183 | 0 | 2637.823 | 6848.511 | 8028.159 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2277.375 | 4554.751 | 5210.111 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 95 | 95 | 0 | 2023.423 | 4292.607 | 4653.055 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 86 | 86 | 0 | 2064.383 | 4161.535 | 5668.863 |
| t-195 | 9 | 1 | 91.88 | 14.658 | 2299 | 0 | 881 | 881 | 0 | 30.079 | 2916.351 | 3670.015 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 874 | 874 | 0 | 32.639 | 2654.207 | 4194.303 |
| t-381 | 11 | 1 | 58.31 | 10.961 | 134 | 0 | 688 | 688 | 0 | 34.559 | 2637.823 | 3653.631 |
| t-132 | 22 | 2 | 187.019 | 63.596 | 2676 | 0 | 3803 | 3803 | 0 | 34.815 | 2539.519 | 4489.215 |
| t-447 | 17 | 1 | 56.174 | 15.159 | 558 | 0 | 941 | 941 | 0 | 31.615 | 2457.599 | 3915.775 |
| t-082 | 42 | 1 | 22.21 | 15.677 | 150 | 0 | 1017 | 1017 | 0 | 31.103 | 2424.831 | 3866.623 |
| t-418 | 6 | 1 | 150.134 | 15.163 | 201 | 0 | 954 | 954 | 0 | 33.023 | 2424.831 | 3391.487 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 260 | 260 | 0 | 31.615 | 2408.447 | 2883.583 |
| t-484 | 6 | 1 | 92.928 | 9.407 | 232 | 0 | 615 | 615 | 0 | 32.767 | 2408.447 | 3620.863 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5536 | 5536 | 0 | 34.559 | 2310.143 | 3768.319 |
| t-102 | 18 | 1 | 44.475 | 13.531 | 339 | 0 | 853 | 853 | 0 | 31.231 | 2293.759 | 3964.927 |
| t-441 | 30 | 2 | 175.195 | 84.966 | 2203 | 0 | 5132 | 5132 | 0 | 34.303 | 2293.759 | 3670.015 |
| t-286 | 20 | 1 | 23.822 | 8.108 | 401 | 0 | 560 | 560 | 0 | 31.231 | 2277.375 | 2736.127 |
| t-427 | 16 | 1 | 137.834 | 36.586 | 1711 | 0 | 2259 | 2259 | 0 | 34.559 | 2277.375 | 3145.727 |
| t-499 | 36 | 1 | 70.369 | 41.476 | 1538 | 0 | 2630 | 2630 | 0 | 33.023 | 2260.991 | 2899.967 |
| t-462 | 10 | 1 | 31.81 | 5.162 | 2006 | 0 | 378 | 378 | 0 | 34.815 | 2244.607 | 2998.271 |
| t-105 | 33 | 2 | 91.21 | 52.442 | 851 | 0 | 3202 | 3202 | 0 | 33.023 | 2195.455 | 5013.503 |
| t-299 | 21 | 1 | 52.52 | 17.96 | 646 | 0 | 1132 | 1132 | 0 | 32.767 | 2195.455 | 3178.495 |
| t-054 | 17 | 1 | 44.442 | 12.727 | 161 | 0 | 825 | 825 | 0 | 32.383 | 2179.071 | 2965.503 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 911 | 911 | 0 | 33.023 | 2179.071 | 2703.359 |
| t-077 | 10 | 1 | 178.071 | 29.72 | 1566 | 0 | 1812 | 1812 | 0 | 33.535 | 2162.687 | 3473.407 |
