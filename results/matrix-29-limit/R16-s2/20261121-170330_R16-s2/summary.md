# R16-s2 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 192.168.0.217:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 86.836s, closing 1.543s, drain 3.105s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 373336 | 373336 | 6029.161 | 6009.145 | 8289.4 | 0 | 0 | 0 | 291 | 2006 | 14502 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 298193 | 416.48 | 124.287 | 1073.151 | 3921.919 | 5144.575 | 8089.599 |
| e2e_from_send | 298193 | 187.057 | 109.567 | 379.135 | 1568.767 | 2961.407 | 7598.079 |
| producer_lag | 298193 | 229.423 | 0.157 | 562.687 | 3598.335 | 4788.223 | 6565.887 |
| publish_call | 373336 | 108.838 | 52.767 | 263.167 | 781.823 | 1108.991 | 1796.095 |
| queue_wait | 298193 | 146.747 | 79.103 | 323.839 | 1114.111 | 2553.855 | 5873.663 |
| broker_dwell | 298193 | 72.065 | 33.855 | 128.511 | 963.071 | 2469.887 | 5697.535 |
| receive_call | 337335 | 35.642 | 15.711 | 90.175 | 293.375 | 460.543 | 516.607 |
| ctx_load | 298193 | 9.599 | 6.171 | 16.527 | 115.071 | 128.127 | 378.879 |
| process | 298193 | 10.841 | 0.001 | 0.003 | 0.016 | 2588.671 | 3006.463 |
| ctx_commit | 298193 | 19.835 | 11.791 | 35.615 | 254.335 | 293.375 | 683.519 |
| ack | 280679 | 0.002 | 0.001 | 0.005 | 0.012 | 0.068 | 5.787 |
| service | 298193 | 40.311 | 20.303 | 51.167 | 276.991 | 2668.543 | 3422.207 |
| e2e_large | 224 | 3134.016 | 2936.831 | 4591.615 | 7258.111 | 7602.175 | 7602.175 |
| ready_wait | 298193 | 30.718 | 13.791 | 76.479 | 223.487 | 506.879 | 1431.551 |
| blocked | 298193 | 27.77 | 13.247 | 69.439 | 193.023 | 464.639 | 734.719 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6009.145 | 8289.4 |
| applied_total | 213050.224 | 355487 |
| bench_cpu_cores | 3.993 | 4.752 |
| bench_cpu_pct | 49.914 | 59.4 |
| bench_gc_gen0 | 0.592 | 2 |
| bench_gc_gen1 | 0.429 | 1 |
| bench_gc_gen2 | 0.429 | 1 |
| bench_gc_heap_mb | 1025.082 | 1593.6 |
| bench_mem_mb | 1702.351 | 1792.1 |
| bench_threadpool_queue | 0.694 | 6 |
| bench_threadpool_threads | 14 | 16 |
| broker_backlog | 3962.061 | 6325 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 520.367 | 1383 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 429308.776 | 710921 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 169449.245 | 282169 |
| octopus_mqtt_sent_total | 190268.796 | 323060 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 7.612 | 17 |
| pg_active_loads | 5.714 | 21 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 25.327 | 69 |
| pg_conn_idle | 236.551 | 262 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.122 | 1 |
| pg_conn_total | 261.878 | 263 |
| pg_db_size_mb | 3575.857 | 4030.3 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23192.135 | 26954.4 |
| pg_tup_del_per_s | 29009.439 | 39188.4 |
| pg_tup_fetch_per_s | 216105.382 | 262721.6 |
| pg_tup_ins_per_s | 40489.967 | 52049.5 |
| pg_tup_upd_per_s | 41566.914 | 49075.2 |
| pg_wal_mb_per_s | 108.016 | 182.97 |
| produced_per_s | 6029.161 | 8751.6 |
| produced_total | 213570.592 | 356800 |
| publish_mb_per_s | 27.028 | 41.9 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 5629/5629 source cursors at the expected last sequence |
| records_complete | pass | all 138,747 records are Completed |
| record_counts | pass | 138,747 records found, 51,204 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 460 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 373,336 events applied by workers, 373,336 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 14,502 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 193 ms, max 735 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 14615 seq 12: ready 107 ms before start, 102 ms of it with an idle slot in group 461
- [cross_source_blocking] source 10269 seq 15: ready 107 ms before start, 103 ms of it with an idle slot in group 344
- [cross_source_blocking] source 14595 seq 10: ready 109 ms before start, 104 ms of it with an idle slot in group 461
- [cross_source_blocking] source 10225 seq 17: ready 120 ms before start, 115 ms of it with an idle slot in group 344
- [cross_source_blocking] source 6007 seq 7: ready 119 ms before start, 107 ms of it with an idle slot in group 206
- [cross_source_blocking] source 3605 seq 12: ready 128 ms before start, 101 ms of it with an idle slot in group 122
- [cross_source_blocking] source 2591 seq 7: ready 109 ms before start, 109 ms of it with an idle slot in group 80
- [cross_source_blocking] source 2264 seq 28: ready 151 ms before start, 151 ms of it with an idle slot in group 65
- [cross_source_blocking] source 790 seq 8: ready 109 ms before start, 109 ms of it with an idle slot in group 23
- [cross_source_blocking] source 7132 seq 18: ready 112 ms before start, 112 ms of it with an idle slot in group 242
- [cross_source_blocking] source 5993 seq 9: ready 110 ms before start, 110 ms of it with an idle slot in group 206
- [cross_source_blocking] source 10400 seq 16: ready 114 ms before start, 114 ms of it with an idle slot in group 347
- [cross_source_blocking] source 7773 seq 16: ready 114 ms before start, 114 ms of it with an idle slot in group 263
- [cross_source_blocking] source 5830 seq 9: ready 698 ms before start, 246 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5825 seq 4: ready 575 ms before start, 359 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5829 seq 3: ready 625 ms before start, 358 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5785 seq 2: ready 688 ms before start, 358 ms of it with an idle slot in group 200
- [cross_source_blocking] source 4537 seq 19: ready 103 ms before start, 103 ms of it with an idle slot in group 158
- [cross_source_blocking] source 5777 seq 9: ready 451 ms before start, 295 ms of it with an idle slot in group 200
- [cross_source_blocking] source 5801 seq 11: ready 451 ms before start, 293 ms of it with an idle slot in group 200

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 398 | 398 | 0 | 2605.055 | 7077.887 | 7602.175 |
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 77 | 77 | 0 | 2129.919 | 5668.863 | 5668.863 |
| t-374 | 28 | 3 | 221.769 | 102.602 | 1811 | 0 | 6301 | 6301 | 0 | 130.559 | 5046.271 | 6750.207 |
| t-254 | 9 | 1 | 26.189 | 4.067 | 3623 | 0 | 278 | 278 | 0 | 99.327 | 4882.431 | 5079.039 |
| t-419 | 17 | 1 | 25.402 | 7.523 | 3214 | 0 | 498 | 498 | 0 | 108.543 | 4784.127 | 5111.807 |
| t-158 | 6 | 1 | 53.971 | 5.707 | 165 | 0 | 391 | 391 | 0 | 124.927 | 4751.359 | 5046.271 |
| t-173 | 12 | 1 | 52.259 | 11.008 | 3493 | 0 | 735 | 735 | 0 | 111.615 | 4521.983 | 5898.239 |
| t-335 | 6 | 1 | 35.932 | 3.38 | 450 | 0 | 214 | 214 | 0 | 110.079 | 4521.983 | 5144.575 |
| t-041 | 34 | 3 | 241.149 | 135.838 | 2732 | 0 | 8053 | 8053 | 0 | 149.503 | 4489.215 | 8093.695 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 17039 | 17039 | 0 | 152.575 | 4489.215 | 6782.975 |
| t-224 | 9 | 1 | 37.952 | 5.826 | 1098 | 0 | 378 | 378 | 0 | 94.719 | 4456.447 | 4751.359 |
| t-266 | 6 | 1 | 140.662 | 13.758 | 714 | 0 | 876 | 876 | 0 | 124.927 | 4292.607 | 5406.719 |
| t-326 | 10 | 1 | 201.714 | 34.56 | 1837 | 0 | 2120 | 2120 | 0 | 126.463 | 4292.607 | 5144.575 |
| t-029 | 28 | 2 | 177.66 | 82.383 | 1732 | 0 | 5020 | 5020 | 0 | 134.143 | 4259.839 | 7045.119 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5515 | 5515 | 0 | 160.767 | 4259.839 | 6225.919 |
| t-143 | 21 | 1 | 29.272 | 10.484 | 1094 | 0 | 660 | 660 | 0 | 94.719 | 4227.071 | 5144.575 |
| t-278 | 11 | 1 | 92.207 | 16.698 | 1899 | 0 | 1039 | 1039 | 0 | 109.567 | 4227.071 | 5079.039 |
| t-395 | 6 | 1 | 43.526 | 4.665 | 212 | 0 | 278 | 278 | 0 | 98.815 | 4161.535 | 4980.735 |
| t-476 | 15 | 1 | 47.573 | 13.273 | 432 | 0 | 852 | 852 | 0 | 96.255 | 4145.151 | 4390.911 |
| t-230 | 30 | 2 | 129.395 | 64.06 | 1033 | 0 | 3896 | 3896 | 0 | 119.295 | 4128.767 | 5472.255 |
| t-257 | 12 | 1 | 83.769 | 16.632 | 1267 | 0 | 1077 | 1077 | 0 | 115.199 | 4128.767 | 4751.359 |
| t-056 | 7 | 1 | 59.8 | 7.224 | 347 | 0 | 453 | 453 | 0 | 113.151 | 4112.383 | 4751.359 |
| t-128 | 11 | 1 | 125.96 | 23.38 | 260 | 0 | 1459 | 1459 | 0 | 118.271 | 4095.999 | 5668.863 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 5123 | 5123 | 0 | 135.167 | 4095.999 | 5210.111 |
| t-293 | 46 | 1 | 52.367 | 40.187 | 2741 | 0 | 2561 | 2561 | 0 | 128.511 | 4095.999 | 5013.503 |
