# Ca-ag-mem — FAIL ❌

**Error:** cancelled

- Broker: `Octopus dispatchers via ZMQ 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `inmemory`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 0s, closing 0s, drain 0s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 420763 | 420722 | 305.336 | 305.866 | 19774.4 | 0 | 0 | 0 | 248 | 2268 | 16874 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 253561 | 3302.628 | 3356.671 | 5017.599 | 6672.383 | 7606.271 | 10452.991 |
| e2e_from_send | 253561 | 165.427 | 123.519 | 295.167 | 1304.575 | 2854.911 | 5230.591 |
| producer_lag | 253602 | 3137.443 | 3196.927 | 4825.087 | 6328.319 | 7262.207 | 7675.903 |
| publish_call | 420763 | 133.064 | 112.255 | 248.831 | 499.711 | 1067.007 | 1456.127 |
| queue_wait | 253561 | 154.475 | 123.007 | 292.607 | 630.271 | 2316.287 | 4780.031 |
| broker_dwell | 253561 | 26.102 | 5.495 | 34.303 | 402.687 | 2195.455 | 4714.495 |
| receive_call | 334726 | 53.912 | 25.679 | 140.927 | 356.863 | 484.351 | 544.255 |
| ctx_load | 253561 | 0.003 | 0.001 | 0.003 | 0.007 | 0.07 | 33.855 |
| process | 253561 | 10.917 | 0.001 | 0.002 | 0.016 | 2625.535 | 3000.319 |
| ctx_commit | 253561 | 0.002 | 0.001 | 0.001 | 0.004 | 0.029 | 16.623 |
| ack | 251971 | 0.007 | 0.003 | 0.006 | 0.023 | 0.139 | 55.615 |
| service | 253561 | 10.952 | 0.014 | 0.042 | 0.294 | 2625.535 | 3000.319 |
| e2e_large | 145 | 5671.103 | 5582.847 | 7610.367 | 9101.311 | 9666.559 | 9666.559 |
| ready_wait | 253561 | 19.062 | 2.793 | 56.959 | 276.223 | 458.495 | 1137.663 |
| blocked | 253561 | 18.996 | 2.793 | 56.831 | 275.455 | 453.119 | 553.471 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 305.866 | 18661.4 |
| applied_total | 417858.919 | 420722 |
| bench_cpu_cores | 0.542 | 3.511 |
| bench_cpu_pct | 2.257 | 14.63 |
| bench_gc_gen0 | 1.111 | 5 |
| bench_gc_gen1 | 0.408 | 3 |
| bench_gc_gen2 | 0.102 | 2 |
| bench_gc_heap_mb | 286.308 | 1107.4 |
| bench_mem_mb | 653.899 | 1624.5 |
| bench_threadpool_queue | 1.181 | 402 |
| bench_threadpool_threads | 20.189 | 38 |
| broker_backlog | 6189.05 | 8449 |
| ctr_eb_ag1_cpu_pct | 915.963 | 1082.23 |
| ctr_eb_ag1_mem_mb | 4954.435 | 7078.2 |
| ctr_eb_ag2_cpu_pct | 858.975 | 1072.56 |
| ctr_eb_ag2_mem_mb | 7111.256 | 7789.6 |
| ctr_eb_ag_haproxy_cpu_pct | 15.653 | 25.4 |
| ctr_eb_ag_haproxy_mem_mb | 33.595 | 36.5 |
| ctr_eb_mysql_cpu_pct | 0.457 | 0.72 |
| ctr_eb_mysql_mem_mb | 6126.4 | 6126.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.011 | 0.02 |
| ctr_eb_pgbouncer_2_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.01 | 0.02 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.011 | 0.06 |
| ctr_eb_pgbouncer_4_mem_mb | 7.3 | 7.3 |
| ctr_eb_pgbouncer_cpu_pct | 0.011 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 10.2 | 10.2 |
| ctr_eb_postgres_cpu_pct | 0.391 | 0.98 |
| ctr_eb_postgres_mem_mb | 2275.858 | 2277.4 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.346 | 96.22 |
| in_flight | 48.859 | 711 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 0.288 | 2.49 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1484606.238 | 2146931 |
| octopus_missing_reports_total | 8281.455 | 8360 |
| octopus_mqtt_received_total | 425682.856 | 849839 |
| octopus_mqtt_sent_total | 568716.563 | 978776 |
| octopus_publish_retries_total | 356507.45 | 727448 |
| octopus_publish_stuck | 1705.412 | 1812 |
| produced_per_s | 305.336 | 18728.4 |
| produced_total | 417907.777 | 420763 |
| publish_mb_per_s | 1.149 | 77.48 |

## Oracle

_not run_

## Cross-source blocking samples

- [cross_source_blocking] source 3132 seq 5: ready 138 ms before start, 138 ms of it with an idle slot in group 104
- [cross_source_blocking] source 4586 seq 11: ready 139 ms before start, 139 ms of it with an idle slot in group 160
- [cross_source_blocking] source 7434 seq 14: ready 139 ms before start, 139 ms of it with an idle slot in group 258
- [cross_source_blocking] source 202 seq 14: ready 139 ms before start, 139 ms of it with an idle slot in group 4
- [cross_source_blocking] source 6078 seq 15: ready 139 ms before start, 139 ms of it with an idle slot in group 208
- [cross_source_blocking] source 8702 seq 29: ready 147 ms before start, 147 ms of it with an idle slot in group 302
- [cross_source_blocking] source 8234 seq 21: ready 147 ms before start, 147 ms of it with an idle slot in group 278
- [cross_source_blocking] source 11882 seq 16: ready 148 ms before start, 148 ms of it with an idle slot in group 383
- [cross_source_blocking] source 14282 seq 6: ready 148 ms before start, 148 ms of it with an idle slot in group 458
- [cross_source_blocking] source 8746 seq 15: ready 149 ms before start, 149 ms of it with an idle slot in group 304
- [cross_source_blocking] source 10238 seq 26: ready 149 ms before start, 149 ms of it with an idle slot in group 344
- [cross_source_blocking] source 5404 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 184
- [cross_source_blocking] source 6300 seq 8: ready 109 ms before start, 109 ms of it with an idle slot in group 214
- [cross_source_blocking] source 3676 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 125
- [cross_source_blocking] source 15964 seq 22: ready 109 ms before start, 109 ms of it with an idle slot in group 499
- [cross_source_blocking] source 9066 seq 17: ready 119 ms before start, 119 ms of it with an idle slot in group 313
- [cross_source_blocking] source 2526 seq 24: ready 133 ms before start, 133 ms of it with an idle slot in group 75
- [cross_source_blocking] source 2072 seq 6: ready 135 ms before start, 135 ms of it with an idle slot in group 61
- [cross_source_blocking] source 6520 seq 9: ready 133 ms before start, 133 ms of it with an idle slot in group 222
- [cross_source_blocking] source 9886 seq 3: ready 223 ms before start, 223 ms of it with an idle slot in group 340

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 75 | 75 | 0 | 5210.111 | 9699.327 | 9699.327 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 163 | 162 | 0 | 5046.271 | 8847.359 | 9109.503 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 39 | 39 | 0 | 4980.735 | 8781.823 | 8781.823 |
| t-079 | 6 | 1 | 119.859 | 12.324 | 1522 | 0 | 317 | 316 | 0 | 3702.783 | 8585.215 | 10354.687 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 26 | 26 | 0 | 5603.327 | 7929.855 | 7929.855 |
| t-369 | 14 | 1 | 164.535 | 40.402 | 1313 | 0 | 961 | 961 | 0 | 3424.255 | 7831.551 | 8978.431 |
| t-017 | 19 | 1 | 36.957 | 11.447 | 830 | 0 | 337 | 337 | 0 | 3391.487 | 7733.247 | 8191.999 |
| t-142 | 15 | 1 | 33.381 | 8.13 | 413 | 0 | 212 | 212 | 0 | 3506.175 | 7634.943 | 7929.855 |
| t-481 | 12 | 1 | 150.551 | 28.385 | 1993 | 0 | 770 | 769 | 0 | 3588.095 | 7602.175 | 10485.759 |
| t-081 | 6 | 1 | 16.193 | 1.614 | 2170 | 0 | 46 | 46 | 0 | 4095.999 | 7569.407 | 7569.407 |
| t-251 | 9 | 1 | 46.964 | 7.056 | 1193 | 0 | 165 | 165 | 0 | 3997.695 | 7536.639 | 7536.639 |
| t-006 | 14 | 1 | 31.139 | 6.948 | 1170 | 0 | 203 | 203 | 0 | 3293.183 | 7503.871 | 7536.639 |
| t-038 | 9 | 1 | 55.117 | 8.286 | 2056 | 0 | 195 | 195 | 0 | 3948.543 | 7438.335 | 7471.103 |
| t-051 | 31 | 1 | 52.054 | 26.084 | 200 | 0 | 658 | 656 | 0 | 3325.951 | 7372.799 | 8519.679 |
| t-269 | 24 | 1 | 88.126 | 37.126 | 531 | 0 | 1033 | 1033 | 0 | 3407.871 | 7372.799 | 7733.247 |
| t-295 | 12 | 1 | 68.958 | 13.698 | 3396 | 0 | 363 | 363 | 0 | 3407.871 | 7372.799 | 8257.535 |
| t-424 | 8 | 1 | 94.679 | 12.424 | 1700 | 0 | 323 | 322 | 0 | 3670.015 | 7372.799 | 7503.871 |
| t-100 | 33 | 1 | 50.561 | 26.936 | 2626 | 0 | 722 | 722 | 0 | 3457.023 | 7340.031 | 7667.711 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 164 | 164 | 0 | 3424.255 | 7340.031 | 7438.335 |
| t-134 | 21 | 1 | 24.302 | 8.325 | 3368 | 0 | 225 | 225 | 0 | 3522.559 | 7307.263 | 7471.103 |
| t-145 | 6 | 1 | 63.772 | 6.425 | 517 | 0 | 169 | 169 | 0 | 3899.391 | 7307.263 | 7634.943 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 607 | 607 | 0 | 3670.015 | 7274.495 | 7831.551 |
| t-358 | 22 | 1 | 33.422 | 12.024 | 310 | 0 | 320 | 320 | 0 | 3375.103 | 7274.495 | 7700.479 |
| t-123 | 16 | 1 | 83.276 | 20.7 | 4028 | 0 | 520 | 520 | 0 | 3440.639 | 7110.655 | 7831.551 |
| t-247 | 8 | 1 | 30.815 | 4.104 | 506 | 0 | 109 | 109 | 0 | 3309.567 | 7110.655 | 7405.567 |
