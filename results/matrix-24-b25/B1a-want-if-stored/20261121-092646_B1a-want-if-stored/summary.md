# B1a-want-if-stored — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.172s, closing 1.803s, drain 4.737s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000503 | 1000503 | 16424.418 | 16255.829 | 20090.5 | 0 | 0 | 0 | 458 | 5162 | 432738 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 797138 | 2120.632 | 642.047 | 6483.967 | 14434.303 | 20365.311 | 31260.671 |
| e2e_from_send | 797138 | 1896.176 | 390.655 | 6209.535 | 14327.807 | 20332.543 | 31260.671 |
| producer_lag | 797138 | 224.455 | 4.795 | 1012.735 | 2111.487 | 2533.375 | 2871.295 |
| publish_call | 1000503 | 68.293 | 48.479 | 142.463 | 316.415 | 672.767 | 874.495 |
| queue_wait | 797138 | 1839.185 | 334.591 | 6152.191 | 14270.463 | 20283.391 | 31260.671 |
| broker_dwell | 797138 | 1797.518 | 284.159 | 6111.231 | 14237.695 | 20234.239 | 31244.287 |
| receive_call | 826985 | 36.411 | 14.271 | 91.647 | 331.007 | 475.903 | 606.719 |
| ctx_load | 797138 | 13.616 | 9.967 | 28.223 | 59.647 | 99.391 | 282.367 |
| process | 797138 | 10.754 | 0.002 | 0.004 | 0.062 | 2621.439 | 3112.959 |
| ctx_commit | 797138 | 32.415 | 27.775 | 61.343 | 107.007 | 259.327 | 786.431 |
| ack | 797118 | 0.012 | 0.004 | 0.006 | 0.044 | 1.046 | 226.175 |
| service | 797138 | 56.994 | 41.695 | 82.175 | 147.455 | 2664.447 | 3495.935 |
| e2e_large | 356 | 3038.614 | 2908.159 | 4612.095 | 5820.415 | 6987.775 | 6987.775 |
| ready_wait | 797138 | 272.654 | 152.191 | 693.247 | 1559.551 | 2232.319 | 3481.599 |
| blocked | 797138 | 226.646 | 127.167 | 583.167 | 1147.903 | 1754.111 | 2598.911 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16255.829 | 19358.3 |
| applied_total | 545206.245 | 935224 |
| bench_cpu_cores | 5.593 | 6.704 |
| bench_cpu_pct | 23.307 | 27.93 |
| bench_gc_gen0 | 2.224 | 5 |
| bench_gc_gen1 | 0.633 | 3 |
| bench_gc_gen2 | 0.551 | 2 |
| bench_gc_heap_mb | 1486.045 | 1876.5 |
| bench_mem_mb | 2219.361 | 2494.4 |
| bench_threadpool_queue | 120.347 | 519 |
| bench_threadpool_threads | 41.878 | 44 |
| broker_backlog | 32201.531 | 41075 |
| ctr_eb_mosquitto_cpu_pct | 64.858 | 70.83 |
| ctr_eb_mosquitto_mem_mb | 9.324 | 23.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 20.701 | 22.24 |
| ctr_eb_pgbouncer_2_mem_mb | 15.224 | 16.5 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.327 | 21.06 |
| ctr_eb_pgbouncer_3_mem_mb | 15.171 | 15.9 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.503 | 23.55 |
| ctr_eb_pgbouncer_4_mem_mb | 15.541 | 16.6 |
| ctr_eb_pgbouncer_cpu_pct | 20.781 | 22.08 |
| ctr_eb_pgbouncer_mem_mb | 15.812 | 19.6 |
| ctr_eb_postgres_cpu_pct | 902.479 | 1001.94 |
| ctr_eb_postgres_mem_mb | 4924.678 | 5252.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.159 | 82.19 |
| in_flight | 29785.041 | 41541 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.793 | 1.91 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1098161 | 1933914 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 212635.653 | 366428 |
| octopus_mqtt_sent_total | 298273.918 | 542735 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 6.531 | 16 |
| pg_active_loads | 8.898 | 60 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.994 | 100 |
| pg_conn_active | 26.755 | 51 |
| pg_conn_idle | 232.061 | 250 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 4.816 | 27 |
| pg_conn_total | 258.816 | 261 |
| pg_db_size_mb | 3701.237 | 4138.8 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19669.004 | 21884.6 |
| pg_tup_del_per_s | 44632.143 | 55870.3 |
| pg_tup_fetch_per_s | 466842.692 | 525312.9 |
| pg_tup_ins_per_s | 55992.131 | 74137.7 |
| pg_tup_upd_per_s | 43179.024 | 49392.3 |
| pg_wal_mb_per_s | 111.469 | 139.52 |
| pgb_cl_active | 250.898 | 262 |
| pgb_cl_waiting | 4.98 | 54 |
| pgb_maxwait_ms | 0.328 | 3.6 |
| pgb_sv_active | 11.286 | 48 |
| pgb_sv_idle | 36.714 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16424.418 | 22895.9 |
| produced_total | 574991.286 | 961214 |
| publish_mb_per_s | 53.905 | 103.66 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,872 records are Completed |
| record_counts | pass | 138,872 records found, 138,872 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,503 events applied by workers, 1,000,503 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 432,738 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1148 ms, max 2598 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 8682 seq 8: ready 145 ms before start, 145 ms of it with an idle slot in group 301
- [cross_source_blocking] source 10858 seq 12: ready 152 ms before start, 143 ms of it with an idle slot in group 354
- [cross_source_blocking] source 10 seq 10: ready 154 ms before start, 154 ms of it with an idle slot in group 0
- [cross_source_blocking] source 1034 seq 6: ready 153 ms before start, 153 ms of it with an idle slot in group 32
- [cross_source_blocking] source 4426 seq 7: ready 153 ms before start, 136 ms of it with an idle slot in group 152
- [cross_source_blocking] source 10922 seq 28: ready 122 ms before start, 122 ms of it with an idle slot in group 356
- [cross_source_blocking] source 5246 seq 22: ready 121 ms before start, 121 ms of it with an idle slot in group 181
- [cross_source_blocking] source 14206 seq 24: ready 124 ms before start, 124 ms of it with an idle slot in group 454
- [cross_source_blocking] source 11582 seq 4: ready 122 ms before start, 122 ms of it with an idle slot in group 376
- [cross_source_blocking] source 15762 seq 24: ready 122 ms before start, 122 ms of it with an idle slot in group 493
- [cross_source_blocking] source 11594 seq 5: ready 123 ms before start, 123 ms of it with an idle slot in group 376
- [cross_source_blocking] source 10890 seq 6: ready 122 ms before start, 122 ms of it with an idle slot in group 354
- [cross_source_blocking] source 8618 seq 6: ready 121 ms before start, 121 ms of it with an idle slot in group 297
- [cross_source_blocking] source 6962 seq 10: ready 121 ms before start, 121 ms of it with an idle slot in group 237
- [cross_source_blocking] source 4746 seq 21: ready 123 ms before start, 123 ms of it with an idle slot in group 164
- [cross_source_blocking] source 15634 seq 7: ready 121 ms before start, 121 ms of it with an idle slot in group 486
- [cross_source_blocking] source 2782 seq 10: ready 121 ms before start, 121 ms of it with an idle slot in group 85
- [cross_source_blocking] source 12350 seq 7: ready 123 ms before start, 123 ms of it with an idle slot in group 392
- [cross_source_blocking] source 1354 seq 12: ready 123 ms before start, 123 ms of it with an idle slot in group 43
- [cross_source_blocking] source 7914 seq 24: ready 121 ms before start, 121 ms of it with an idle slot in group 266

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9222 | 9222 | 0 | 7897.087 | 23592.959 | 26476.543 |
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3195 | 3195 | 0 | 7536.639 | 23592.959 | 31326.207 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2849 | 2849 | 0 | 5931.007 | 22020.095 | 24903.679 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9092 | 9092 | 0 | 7307.263 | 21233.663 | 27918.335 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 6062.079 | 20840.447 | 24903.679 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2962 | 2962 | 0 | 5472.255 | 19136.511 | 22806.527 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5631 | 5631 | 0 | 5177.343 | 19005.439 | 25165.823 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11853 | 11853 | 0 | 5931.007 | 18874.367 | 24117.247 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15140 | 15140 | 0 | 5734.399 | 18481.151 | 23592.959 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20597 | 20597 | 0 | 5472.255 | 18219.007 | 23330.815 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5918 | 5918 | 0 | 5898.239 | 17694.719 | 20709.375 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6099 | 6099 | 0 | 4947.967 | 17563.647 | 23461.887 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 4620.287 | 16908.287 | 20578.303 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3103 | 3103 | 0 | 5242.879 | 16908.287 | 20840.447 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2911 | 2911 | 0 | 3391.487 | 16908.287 | 18087.935 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14679 | 14679 | 0 | 5767.167 | 16580.607 | 20971.519 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 4456.447 | 16515.071 | 20054.015 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5829 | 5829 | 0 | 4882.431 | 16318.463 | 21233.663 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 3850.239 | 15990.783 | 19267.583 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11136 | 11136 | 0 | 4947.967 | 15663.103 | 21889.023 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5543 | 5543 | 0 | 5505.023 | 15466.495 | 17956.863 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19764 | 19764 | 0 | 3964.927 | 14942.207 | 20578.303 |
| t-314 | 24 | 2 | 232.61 | 92.686 | 525 | 0 | 5724 | 5724 | 0 | 4620.287 | 14745.599 | 15663.103 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2834 | 2834 | 0 | 3719.167 | 14352.383 | 15728.639 |
| t-065 | 80 | 6 | 210.386 | 277.713 | 263 | 0 | 16730 | 16730 | 0 | 4079.615 | 13959.167 | 16056.319 |
