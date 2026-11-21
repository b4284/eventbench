# D24-sync-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.0.217:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.0.217:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 779 workers, offered 23904.788 ev/s
- Phases: run 86.114s, closing 1.057s, drain 8.488s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 732477 | 732477 | 12508.302 | 12305.86 | 15223.3 | 0 | 0 | 0 | 356 | 3802 | 191829 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 589220 | 3552.714 | 3799.039 | 6553.599 | 9093.119 | 12902.399 | 22036.479 |
| e2e_from_send | 589220 | 787.232 | 373.247 | 1930.239 | 5947.391 | 10747.903 | 17940.479 |
| producer_lag | 589220 | 2765.484 | 3041.279 | 5603.327 | 7032.831 | 7630.847 | 8040.447 |
| publish_call | 732477 | 148.377 | 113.343 | 310.015 | 625.151 | 974.847 | 1106.943 |
| queue_wait | 589220 | 718.338 | 310.271 | 1819.647 | 5885.951 | 10674.175 | 17842.175 |
| broker_dwell | 589220 | 583.705 | 137.343 | 1693.695 | 5767.167 | 10543.103 | 17743.871 |
| receive_call | 667370 | 25.692 | 9.239 | 68.735 | 241.023 | 431.359 | 523.007 |
| ctx_load | 589220 | 11.382 | 8.687 | 23.407 | 44.383 | 72.255 | 407.039 |
| process | 589220 | 10.672 | 0.001 | 0.002 | 0.008 | 2613.247 | 3346.431 |
| ctx_commit | 589220 | 46.808 | 43.135 | 76.863 | 118.143 | 405.503 | 731.647 |
| ack | 517067 | 0.002 | 0.001 | 0.003 | 0.007 | 0.026 | 3.607 |
| service | 589220 | 68.894 | 54.911 | 92.415 | 149.503 | 2680.831 | 3846.143 |
| e2e_large | 256 | 5870.826 | 5963.775 | 8699.903 | 10764.287 | 13172.735 | 13172.735 |
| ready_wait | 589220 | 128.455 | 70.719 | 328.703 | 684.031 | 1251.327 | 2111.487 |
| blocked | 589220 | 85.622 | 48.255 | 223.871 | 398.847 | 664.063 | 1003.007 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 12305.86 | 15223.3 |
| applied_total | 390161.14 | 702786 |
| bench_cpu_cores | 3.426 | 5.514 |
| bench_cpu_pct | 34.262 | 55.14 |
| bench_gc_gen0 | 0.5 | 2 |
| bench_gc_gen1 | 0.34 | 1 |
| bench_gc_gen2 | 0.32 | 1 |
| bench_gc_heap_mb | 1789.664 | 2794.9 |
| bench_mem_mb | 2734.77 | 2972.5 |
| bench_threadpool_queue | 2.08 | 65 |
| bench_threadpool_threads | 17.92 | 23 |
| broker_backlog | 47579.62 | 82026 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 7652.22 | 13194 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 785790.28 | 1407184 |
| octopus_missing_reports_total | 26391.28 | 43912 |
| octopus_mqtt_received_total | 150670.7 | 257531 |
| octopus_mqtt_sent_total | 300118.78 | 532286 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 23.14 | 37 |
| pg_active_loads | 16.92 | 147 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.972 | 100 |
| pg_conn_active | 60.04 | 122 |
| pg_conn_idle | 200.9 | 236 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.28 | 28 |
| pg_conn_total | 260.94 | 262 |
| pg_db_size_mb | 3771.226 | 4366.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 25119.486 | 28357.1 |
| pg_tup_del_per_s | 35447.994 | 45192.7 |
| pg_tup_fetch_per_s | 315085.452 | 390699.7 |
| pg_tup_ins_per_s | 48509.114 | 57437.1 |
| pg_tup_upd_per_s | 58652.348 | 67026.7 |
| pg_wal_mb_per_s | 133.928 | 182.36 |
| produced_per_s | 12508.302 | 19514.3 |
| produced_total | 397813.36 | 713991 |
| publish_mb_per_s | 42.448 | 60.3 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 8027/8027 source cursors at the expected last sequence |
| records_complete | pass | all 191,448 records are Completed |
| record_counts | pass | 191,448 records found, 95,637 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 402 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 732,477 events applied by workers, 732,477 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 191,829 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 399 ms, max 1003 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 11715 seq 15: ready 100 ms before start, 100 ms of it with an idle slot in group 379
- [cross_source_blocking] source 5720 seq 27: ready 102 ms before start, 102 ms of it with an idle slot in group 197
- [cross_source_blocking] source 6657 seq 7: ready 100 ms before start, 100 ms of it with an idle slot in group 225
- [cross_source_blocking] source 6907 seq 35: ready 144 ms before start, 120 ms of it with an idle slot in group 235
- [cross_source_blocking] source 9130 seq 21: ready 139 ms before start, 109 ms of it with an idle slot in group 315
- [cross_source_blocking] source 11683 seq 16: ready 150 ms before start, 118 ms of it with an idle slot in group 379
- [cross_source_blocking] source 13861 seq 15: ready 169 ms before start, 114 ms of it with an idle slot in group 439
- [cross_source_blocking] source 2288 seq 74: ready 163 ms before start, 163 ms of it with an idle slot in group 65
- [cross_source_blocking] source 13289 seq 25: ready 142 ms before start, 142 ms of it with an idle slot in group 425
- [cross_source_blocking] source 370 seq 15: ready 152 ms before start, 152 ms of it with an idle slot in group 11
- [cross_source_blocking] source 3805 seq 22: ready 110 ms before start, 110 ms of it with an idle slot in group 129
- [cross_source_blocking] source 15570 seq 15: ready 133 ms before start, 111 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10444 seq 24: ready 234 ms before start, 208 ms of it with an idle slot in group 347
- [cross_source_blocking] source 11370 seq 17: ready 168 ms before start, 168 ms of it with an idle slot in group 371
- [cross_source_blocking] source 2973 seq 17: ready 234 ms before start, 230 ms of it with an idle slot in group 97
- [cross_source_blocking] source 8531 seq 23: ready 188 ms before start, 188 ms of it with an idle slot in group 293
- [cross_source_blocking] source 13289 seq 29: ready 176 ms before start, 171 ms of it with an idle slot in group 425
- [cross_source_blocking] source 15457 seq 23: ready 115 ms before start, 102 ms of it with an idle slot in group 483
- [cross_source_blocking] source 10384 seq 37: ready 230 ms before start, 204 ms of it with an idle slot in group 347
- [cross_source_blocking] source 13285 seq 21: ready 163 ms before start, 159 ms of it with an idle slot in group 425

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-409 | 48 | 2 | 120.205 | 94.736 | 671 | 0 | 5779 | 5779 | 0 | 5308.415 | 17825.791 | 22151.167 |
| t-055 | 65 | 4 | 188.704 | 195.304 | 359 | 0 | 11537 | 11537 | 0 | 5177.343 | 15400.959 | 16777.215 |
| t-439 | 22 | 1 | 126.886 | 47.314 | 3339 | 0 | 2862 | 2862 | 0 | 5111.807 | 15073.279 | 16121.855 |
| t-175 | 27 | 1 | 107.829 | 49.991 | 184 | 0 | 2969 | 2969 | 0 | 5341.183 | 14286.847 | 17039.359 |
| t-109 | 33 | 1 | 88.394 | 49.274 | 1467 | 0 | 3063 | 3063 | 0 | 5865.471 | 13369.343 | 18350.079 |
| t-037 | 21 | 1 | 137.486 | 48.25 | 272 | 0 | 2903 | 2903 | 0 | 4325.375 | 13238.271 | 14614.527 |
| t-245 | 43 | 2 | 132.507 | 91.014 | 2232 | 0 | 5601 | 5601 | 0 | 4980.735 | 12320.767 | 14352.383 |
| t-261 | 51 | 2 | 110.543 | 94.419 | 2342 | 0 | 5824 | 5824 | 0 | 4882.431 | 11862.015 | 15335.423 |
| t-469 | 105 | 5 | 142.583 | 248.092 | 278 | 0 | 14744 | 14744 | 0 | 4751.359 | 11534.335 | 16187.391 |
| t-383 | 200 | 10 | 144.204 | 479.619 | 2521 | 0 | 28845 | 28845 | 0 | 4784.127 | 11206.655 | 13303.807 |
| t-319L | 29 | 2 | 4.084 | 1.953 | 1917 | 1 | 135 | 135 | 0 | 5079.039 | 11075.583 | 13172.735 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 77 | 77 | 0 | 4849.663 | 10747.903 | 10747.903 |
| t-465 | 66 | 3 | 131.577 | 141.969 | 667 | 0 | 8670 | 8670 | 0 | 4685.823 | 10616.831 | 15007.743 |
| t-119L | 64 | 4 | 4.456 | 4.863 | 156 | 1 | 373 | 373 | 0 | 5505.023 | 10223.615 | 10289.151 |
| t-333 | 79 | 1 | 36.943 | 47.58 | 192 | 0 | 3063 | 3063 | 0 | 4882.431 | 9895.935 | 12058.623 |
| t-077 | 10 | 1 | 267.106 | 44.58 | 1566 | 0 | 2564 | 2564 | 0 | 4980.735 | 9830.399 | 10354.687 |
| t-461 | 130 | 4 | 83.759 | 182.212 | 3025 | 0 | 11258 | 11258 | 0 | 4358.143 | 9633.791 | 13041.663 |
| t-221 | 70 | 1 | 37.148 | 42.548 | 2834 | 0 | 2731 | 2731 | 0 | 4161.535 | 9371.647 | 12058.623 |
| t-481 | 12 | 1 | 225.826 | 42.578 | 1993 | 0 | 2515 | 2515 | 0 | 3850.239 | 9240.575 | 10289.151 |
| t-005 | 47 | 2 | 112.643 | 87.54 | 2385 | 0 | 5223 | 5223 | 0 | 3719.167 | 9175.039 | 11206.655 |
| t-023 | 72 | 1 | 38.898 | 46.836 | 647 | 0 | 3016 | 3016 | 0 | 4521.983 | 9175.039 | 11665.407 |
| t-441 | 30 | 3 | 262.793 | 127.449 | 2203 | 0 | 7609 | 7609 | 0 | 4145.151 | 9175.039 | 12189.695 |
| t-313 | 35 | 1 | 65.222 | 39.055 | 510 | 0 | 2373 | 2373 | 0 | 3997.695 | 9109.503 | 10616.831 |
| t-413 | 19 | 1 | 129.315 | 42.734 | 3585 | 0 | 2578 | 2578 | 0 | 4030.463 | 9043.967 | 10616.831 |
| t-065 | 80 | 9 | 315.578 | 416.57 | 263 | 0 | 24589 | 24589 | 0 | 4292.607 | 8781.823 | 10551.295 |
