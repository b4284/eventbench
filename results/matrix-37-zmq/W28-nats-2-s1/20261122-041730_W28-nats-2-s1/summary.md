# W28-nats-2-s1 — FAIL ❌

- Broker: `Octopus dispatchers via NATS 192.168.31.128:6440 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 192.168.31.128:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 856 workers, offered 27887.483 ev/s
- Phases: run 86.233s, closing 1.595s, drain 85.608s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 404952 | 404952 | 6595.743 | 6584.037 | 9273.7 | 0 | 0 | 0 | 68 | 1962 | 19077 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 282608 | 15549.817 | 15630.335 | 22478.847 | 25935.871 | 26673.151 | 29163.519 |
| e2e_from_send | 282608 | 497.552 | 431.871 | 830.463 | 1992.703 | 3332.095 | 6737.919 |
| producer_lag | 282608 | 15052.253 | 15163.391 | 22003.711 | 25526.271 | 26181.631 | 26460.159 |
| publish_call | 404952 | 307.375 | 265.727 | 602.111 | 1155.071 | 1979.391 | 2967.551 |
| queue_wait | 282608 | 415.551 | 355.327 | 727.551 | 1587.199 | 2992.127 | 5234.687 |
| broker_dwell | 282608 | 105.831 | 25.151 | 262.143 | 1234.943 | 2707.455 | 5169.151 |
| receive_call | 363315 | 43.056 | 17.727 | 115.583 | 323.071 | 476.927 | 509.951 |
| ctx_load | 282608 | 16.204 | 11.839 | 30.799 | 127.807 | 156.159 | 776.191 |
| process | 282608 | 10.167 | 0.001 | 0.002 | 0.007 | 2578.431 | 3002.367 |
| ctx_commit | 282608 | 55.598 | 38.079 | 106.495 | 341.503 | 571.391 | 869.887 |
| ack | 230952 | 0.002 | 0.001 | 0.003 | 0.008 | 0.025 | 0.411 |
| service | 282608 | 82.001 | 54.975 | 134.527 | 400.895 | 2664.447 | 3758.079 |
| e2e_large | 44 | 17125.748 | 16859.135 | 22839.295 | 28524.543 | 28524.543 | 28524.543 |
| ready_wait | 282608 | 42.929 | 13.911 | 118.719 | 340.991 | 675.839 | 1998.847 |
| blocked | 282608 | 31.909 | 13.287 | 79.679 | 262.399 | 652.287 | 1532.927 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 6584.037 | 9273.7 |
| applied_total | 226739.816 | 386942 |
| bench_cpu_cores | 1.513 | 2.565 |
| bench_cpu_pct | 15.126 | 25.65 |
| bench_gc_gen0 | 0.245 | 2 |
| bench_gc_gen1 | 0.143 | 1 |
| bench_gc_gen2 | 0.143 | 1 |
| bench_gc_heap_mb | 1595.694 | 2769.8 |
| bench_mem_mb | 2529.943 | 2894 |
| bench_threadpool_queue | 0.224 | 5 |
| bench_threadpool_threads | 18.796 | 22 |
| broker_backlog | 55924.837 | 104233 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| in_flight | 1253.265 | 3611 |
| nacks_per_s | 0 | 0 |
| octopus_items_sent_total | 459363.959 | 768527 |
| octopus_missing_reports_total | 18662.633 | 29220 |
| octopus_mqtt_received_total | 106201.143 | 164685 |
| octopus_mqtt_sent_total | 193510.776 | 318311 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 24.286 | 47 |
| pg_active_loads | 21.122 | 139 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.977 | 100 |
| pg_conn_active | 62.837 | 131 |
| pg_conn_idle | 200.857 | 247 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 9.265 | 51 |
| pg_conn_total | 263.694 | 264 |
| pg_db_size_mb | 3780.404 | 4323.4 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24398.398 | 28347.8 |
| pg_tup_del_per_s | 32416.241 | 40039.7 |
| pg_tup_fetch_per_s | 330905.484 | 412747 |
| pg_tup_ins_per_s | 44588.547 | 57811.4 |
| pg_tup_upd_per_s | 52544.867 | 59539.3 |
| pg_wal_mb_per_s | 121.46 | 202.14 |
| produced_per_s | 6595.743 | 9375.7 |
| produced_total | 227993.082 | 387970 |
| publish_mb_per_s | 16.193 | 24.49 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 6514/6514 source cursors at the expected last sequence |
| records_complete | pass | all 185,191 records are Completed |
| record_counts | pass | 185,191 records found, 56,339 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 364 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 404,952 events applied by workers, 404,952 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 19,077 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 262 ms, max 1533 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 15248 seq 18: ready 100 ms before start, 100 ms of it with an idle slot in group 476
- [cross_source_blocking] source 2754 seq 26: ready 100 ms before start, 100 ms of it with an idle slot in group 84
- [cross_source_blocking] source 9184 seq 20: ready 111 ms before start, 111 ms of it with an idle slot in group 315
- [cross_source_blocking] source 9152 seq 28: ready 111 ms before start, 111 ms of it with an idle slot in group 315
- [cross_source_blocking] source 4775 seq 32: ready 111 ms before start, 111 ms of it with an idle slot in group 165
- [cross_source_blocking] source 12758 seq 13: ready 111 ms before start, 111 ms of it with an idle slot in group 403
- [cross_source_blocking] source 5914 seq 20: ready 110 ms before start, 110 ms of it with an idle slot in group 205
- [cross_source_blocking] source 9964 seq 6: ready 111 ms before start, 111 ms of it with an idle slot in group 341
- [cross_source_blocking] source 11725 seq 28: ready 122 ms before start, 105 ms of it with an idle slot in group 379
- [cross_source_blocking] source 1490 seq 28: ready 123 ms before start, 123 ms of it with an idle slot in group 45
- [cross_source_blocking] source 8520 seq 15: ready 105 ms before start, 105 ms of it with an idle slot in group 293
- [cross_source_blocking] source 11163 seq 7: ready 116 ms before start, 116 ms of it with an idle slot in group 364
- [cross_source_blocking] source 4993 seq 44: ready 116 ms before start, 116 ms of it with an idle slot in group 172
- [cross_source_blocking] source 704 seq 14: ready 116 ms before start, 116 ms of it with an idle slot in group 21
- [cross_source_blocking] source 7171 seq 21: ready 148 ms before start, 148 ms of it with an idle slot in group 244
- [cross_source_blocking] source 11490 seq 35: ready 177 ms before start, 136 ms of it with an idle slot in group 373
- [cross_source_blocking] source 9367 seq 12: ready 176 ms before start, 109 ms of it with an idle slot in group 325
- [cross_source_blocking] source 9836 seq 13: ready 145 ms before start, 104 ms of it with an idle slot in group 340
- [cross_source_blocking] source 13399 seq 11: ready 212 ms before start, 176 ms of it with an idle slot in group 429
- [cross_source_blocking] source 15587 seq 23: ready 114 ms before start, 114 ms of it with an idle slot in group 483

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-308L | 26 | 1 | 1.861 | 0.85 | 201 | 1 | 65 | 65 | 0 | 18481.151 | 28573.695 | 28573.695 |
| t-093L | 20 | 1 | 2.903 | 0.952 | 711 | 1 | 68 | 68 | 0 | 15204.351 | 27262.975 | 27262.975 |
| t-164 | 7 | 1 | 221.485 | 23.812 | 236 | 0 | 894 | 894 | 0 | 15204.351 | 26738.687 | 26738.687 |
| t-045 | 47 | 3 | 179.274 | 139.699 | 1398 | 0 | 5370 | 5370 | 0 | 15400.959 | 26476.543 | 28180.479 |
| t-132 | 22 | 3 | 327.283 | 111.293 | 2676 | 0 | 4222 | 4222 | 0 | 15663.103 | 26476.543 | 28966.911 |
| t-205 | 18 | 1 | 114.565 | 34.071 | 449 | 0 | 1350 | 1350 | 0 | 14876.671 | 26476.543 | 26738.687 |
| t-227 | 17 | 1 | 115.847 | 31.596 | 850 | 0 | 1202 | 1202 | 0 | 15532.031 | 26476.543 | 26869.759 |
| t-357 | 21 | 1 | 116.69 | 40.486 | 2215 | 0 | 1574 | 1574 | 0 | 15663.103 | 26476.543 | 26869.759 |
| t-420 | 13 | 1 | 107.671 | 23.798 | 270 | 0 | 958 | 958 | 0 | 14548.991 | 26476.543 | 27918.335 |
| t-476 | 15 | 1 | 83.253 | 23.227 | 432 | 0 | 935 | 935 | 0 | 15663.103 | 26476.543 | 26869.759 |
| t-020 | 22 | 1 | 138.067 | 48.645 | 981 | 0 | 1761 | 1761 | 0 | 16056.319 | 26345.471 | 26738.687 |
| t-027 | 62 | 2 | 60.438 | 60.682 | 935 | 0 | 2373 | 2373 | 0 | 15204.351 | 26345.471 | 27262.975 |
| t-037 | 21 | 2 | 160.4 | 56.292 | 272 | 0 | 2229 | 2229 | 0 | 15335.423 | 26345.471 | 26869.759 |
| t-107 | 19 | 1 | 89.485 | 27.789 | 453 | 0 | 1034 | 1034 | 0 | 15466.495 | 26345.471 | 26738.687 |
| t-133 | 6 | 1 | 60.083 | 5.815 | 273 | 0 | 243 | 243 | 0 | 15728.639 | 26345.471 | 26476.543 |
| t-140 | 13 | 1 | 137.523 | 30.094 | 429 | 0 | 1179 | 1179 | 0 | 14483.455 | 26345.471 | 26869.759 |
| t-221 | 70 | 1 | 43.339 | 49.639 | 2834 | 0 | 2045 | 2045 | 0 | 15663.103 | 26345.471 | 27787.263 |
| t-285 | 43 | 2 | 113.527 | 80.919 | 233 | 0 | 3058 | 3058 | 0 | 15532.031 | 26345.471 | 26869.759 |
| t-421 | 6 | 1 | 141.184 | 14.431 | 758 | 0 | 532 | 532 | 0 | 15532.031 | 26345.471 | 26738.687 |
| t-436 | 58 | 2 | 77.019 | 74.806 | 1128 | 0 | 2922 | 2922 | 0 | 15597.567 | 26345.471 | 26738.687 |
| t-452 | 6 | 1 | 44.703 | 4.676 | 1797 | 0 | 167 | 167 | 0 | 15597.567 | 26345.471 | 26345.471 |
| t-459 | 25 | 1 | 36.37 | 14.841 | 2174 | 0 | 649 | 649 | 0 | 15204.351 | 26345.471 | 26738.687 |
| t-499 | 36 | 2 | 123.146 | 72.584 | 1538 | 0 | 2766 | 2766 | 0 | 15400.959 | 26345.471 | 27262.975 |
| t-068 | 21 | 1 | 42.794 | 14.696 | 3531 | 0 | 609 | 609 | 0 | 16449.535 | 26214.399 | 26607.615 |
| t-092 | 29 | 1 | 73.218 | 35.891 | 519 | 0 | 1430 | 1430 | 0 | 15335.423 | 26214.399 | 27394.047 |
