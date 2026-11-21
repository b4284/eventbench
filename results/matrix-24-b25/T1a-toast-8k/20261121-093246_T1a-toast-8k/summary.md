# T1a-toast-8k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 647 workers, offered 15939.397 ev/s
- Phases: run 60.188s, closing 2.057s, drain 5.538s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1000281 | 1000281 | 15946.473 | 16014.831 | 23872.8 | 0 | 0 | 0 | 458 | 5159 | 434227 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 796909 | 2508.988 | 488.191 | 8175.615 | 16531.455 | 22216.703 | 32129.023 |
| e2e_from_send | 796909 | 2489.83 | 459.263 | 8142.847 | 16515.071 | 22200.319 | 32129.023 |
| producer_lag | 796909 | 19.159 | 3.067 | 41.951 | 297.983 | 577.023 | 957.951 |
| publish_call | 1000281 | 56.198 | 42.303 | 109.503 | 258.303 | 449.791 | 604.159 |
| queue_wait | 796909 | 2431.895 | 397.311 | 8089.599 | 16465.919 | 22151.167 | 32129.023 |
| broker_dwell | 796909 | 2399.584 | 361.983 | 8052.735 | 16441.343 | 22118.399 | 32096.255 |
| receive_call | 815743 | 37.695 | 15.407 | 93.439 | 334.335 | 474.367 | 684.031 |
| ctx_load | 796909 | 14.555 | 10.903 | 30.095 | 62.655 | 98.751 | 341.503 |
| process | 796909 | 10.754 | 0.002 | 0.004 | 0.066 | 2621.439 | 3104.767 |
| ctx_commit | 796909 | 32.373 | 27.039 | 62.879 | 111.679 | 187.647 | 427.263 |
| ack | 796889 | 0.012 | 0.004 | 0.007 | 0.048 | 1.238 | 84.927 |
| service | 796909 | 57.937 | 42.431 | 83.967 | 163.839 | 2676.735 | 3280.895 |
| e2e_large | 356 | 3041.753 | 2928.639 | 4145.151 | 6467.583 | 7565.311 | 7565.311 |
| ready_wait | 796909 | 293.616 | 159.871 | 739.327 | 1676.287 | 2310.143 | 2871.295 |
| blocked | 796909 | 241.489 | 132.863 | 614.399 | 1241.087 | 1789.951 | 2385.919 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 16014.831 | 18561.9 |
| applied_total | 538945.51 | 922730 |
| bench_cpu_cores | 5.714 | 7.05 |
| bench_cpu_pct | 23.809 | 29.38 |
| bench_gc_gen0 | 2.163 | 4 |
| bench_gc_gen1 | 0.653 | 2 |
| bench_gc_gen2 | 0.551 | 2 |
| bench_gc_heap_mb | 1455.127 | 1947.9 |
| bench_mem_mb | 2276.641 | 2674.7 |
| bench_threadpool_queue | 165.633 | 792 |
| bench_threadpool_threads | 42.327 | 44 |
| broker_backlog | 41948.653 | 48746 |
| ctr_eb_mosquitto_cpu_pct | 66.224 | 70.23 |
| ctr_eb_mosquitto_mem_mb | 7.455 | 15.1 |
| ctr_eb_pgbouncer_2_cpu_pct | 21.359 | 23.07 |
| ctr_eb_pgbouncer_2_mem_mb | 15.612 | 17.1 |
| ctr_eb_pgbouncer_3_cpu_pct | 19.79 | 21.64 |
| ctr_eb_pgbouncer_3_mem_mb | 15.233 | 16.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 21.597 | 24.08 |
| ctr_eb_pgbouncer_4_mem_mb | 15.516 | 16.8 |
| ctr_eb_pgbouncer_cpu_pct | 21.021 | 23.42 |
| ctr_eb_pgbouncer_mem_mb | 15.502 | 16.3 |
| ctr_eb_postgres_cpu_pct | 839.912 | 907.74 |
| ctr_eb_postgres_mem_mb | 5631.002 | 5803.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 77.43 | 83.1 |
| in_flight | 39456.347 | 48439 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.835 | 1.9 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 1093003.673 | 1879201 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 218074.694 | 361990 |
| octopus_mqtt_sent_total | 280457.286 | 502556 |
| octopus_publish_retries_total | 0 | 0 |
| octopus_publish_stuck | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5.898 | 16 |
| pg_active_loads | 3.082 | 12 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 99.998 | 100 |
| pg_conn_active | 21.163 | 69 |
| pg_conn_idle | 237.449 | 257 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 2.816 | 25 |
| pg_conn_total | 258.612 | 260 |
| pg_db_size_mb | 3602.506 | 4109.2 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19244.427 | 23759.4 |
| pg_tup_del_per_s | 29277.457 | 37131.4 |
| pg_tup_fetch_per_s | 381709.99 | 433268.4 |
| pg_tup_ins_per_s | 41418.341 | 54333.6 |
| pg_tup_upd_per_s | 42569.102 | 49577.5 |
| pg_wal_mb_per_s | 116.505 | 202.23 |
| pgb_cl_active | 247.306 | 263 |
| pgb_cl_waiting | 3.898 | 40 |
| pgb_maxwait_ms | 0.535 | 18.98 |
| pgb_sv_active | 13.857 | 48 |
| pgb_sv_idle | 34.143 | 48 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 15946.473 | 18178.1 |
| produced_total | 578401.857 | 961075 |
| publish_mb_per_s | 51.968 | 73.55 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 138,853 records are Completed |
| record_counts | pass | 138,853 records found, 138,853 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 458 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,000,281 events applied by workers, 1,000,281 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 434,227 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 1241 ms, max 2385 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 1988 seq 11: ready 138 ms before start, 138 ms of it with an idle slot in group 58
- [cross_source_blocking] source 2968 seq 13: ready 138 ms before start, 138 ms of it with an idle slot in group 97
- [cross_source_blocking] source 10 seq 10: ready 206 ms before start, 206 ms of it with an idle slot in group 0
- [cross_source_blocking] source 8569 seq 21: ready 266 ms before start, 266 ms of it with an idle slot in group 294
- [cross_source_blocking] source 14240 seq 39: ready 266 ms before start, 266 ms of it with an idle slot in group 456
- [cross_source_blocking] source 3172 seq 18: ready 275 ms before start, 275 ms of it with an idle slot in group 105
- [cross_source_blocking] source 15136 seq 5: ready 265 ms before start, 265 ms of it with an idle slot in group 472
- [cross_source_blocking] source 5480 seq 11: ready 265 ms before start, 265 ms of it with an idle slot in group 188
- [cross_source_blocking] source 13732 seq 18: ready 286 ms before start, 286 ms of it with an idle slot in group 437
- [cross_source_blocking] source 14228 seq 21: ready 266 ms before start, 266 ms of it with an idle slot in group 456
- [cross_source_blocking] source 3156 seq 24: ready 265 ms before start, 265 ms of it with an idle slot in group 105
- [cross_source_blocking] source 8212 seq 6: ready 265 ms before start, 265 ms of it with an idle slot in group 276
- [cross_source_blocking] source 548 seq 6: ready 320 ms before start, 320 ms of it with an idle slot in group 16
- [cross_source_blocking] source 15636 seq 23: ready 265 ms before start, 265 ms of it with an idle slot in group 487
- [cross_source_blocking] source 4992 seq 13: ready 266 ms before start, 266 ms of it with an idle slot in group 172
- [cross_source_blocking] source 15399 seq 14: ready 170 ms before start, 170 ms of it with an idle slot in group 482
- [cross_source_blocking] source 12416 seq 15: ready 285 ms before start, 285 ms of it with an idle slot in group 394
- [cross_source_blocking] source 7675 seq 7: ready 202 ms before start, 202 ms of it with an idle slot in group 262
- [cross_source_blocking] source 9349 seq 4: ready 180 ms before start, 180 ms of it with an idle slot in group 324
- [cross_source_blocking] source 3704 seq 11: ready 285 ms before start, 285 ms of it with an idle slot in group 126

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-168 | 71 | 1 | 42.924 | 49.841 | 155 | 0 | 3193 | 3193 | 0 | 7831.551 | 25034.751 | 32243.711 |
| t-121 | 76 | 3 | 121.292 | 148.793 | 149 | 0 | 9220 | 9220 | 0 | 8847.359 | 24772.607 | 27918.335 |
| t-362 | 47 | 1 | 59.227 | 45.382 | 3235 | 0 | 2849 | 2849 | 0 | 7208.959 | 23986.175 | 29229.055 |
| t-182 | 89 | 3 | 101.606 | 147.829 | 2406 | 0 | 9088 | 9088 | 0 | 8519.679 | 23199.743 | 29491.199 |
| t-350 | 85 | 2 | 66.469 | 91.357 | 285 | 0 | 5628 | 5628 | 0 | 6520.831 | 22020.095 | 29884.415 |
| t-450 | 63 | 2 | 94.289 | 97.643 | 165 | 0 | 5976 | 5976 | 0 | 6684.671 | 22020.095 | 26476.543 |
| t-340 | 184 | 2 | 30.383 | 93.585 | 141 | 0 | 6097 | 6097 | 0 | 5996.543 | 20840.447 | 26869.759 |
| t-285 | 43 | 1 | 64.873 | 46.239 | 233 | 0 | 2910 | 2910 | 0 | 5308.415 | 20709.375 | 22675.455 |
| t-416 | 57 | 1 | 48.216 | 46.188 | 3430 | 0 | 2960 | 2960 | 0 | 6160.383 | 20578.303 | 23461.887 |
| t-438 | 121 | 4 | 92.608 | 193.902 | 1839 | 0 | 11851 | 11851 | 0 | 7405.567 | 20578.303 | 24379.391 |
| t-343 | 63 | 1 | 48.018 | 48.591 | 518 | 0 | 3099 | 3099 | 0 | 6782.975 | 20447.231 | 23855.103 |
| t-496 | 81 | 5 | 180.568 | 245.466 | 278 | 0 | 15136 | 15136 | 0 | 7077.887 | 20185.087 | 25427.967 |
| t-344 | 195 | 7 | 101.58 | 334.276 | 571 | 0 | 20593 | 20593 | 0 | 6619.135 | 19922.943 | 26083.327 |
| t-147 | 79 | 5 | 180.465 | 242.333 | 1078 | 0 | 14675 | 14675 | 0 | 7929.855 | 19791.871 | 23461.887 |
| t-378 | 55 | 2 | 106.476 | 95.397 | 3659 | 0 | 5917 | 5917 | 0 | 7208.959 | 19791.871 | 23199.743 |
| t-235 | 54 | 2 | 98.424 | 88.599 | 527 | 0 | 5557 | 5557 | 0 | 6127.615 | 19660.799 | 23592.959 |
| t-265 | 75 | 1 | 37.432 | 46.672 | 250 | 0 | 3103 | 3103 | 0 | 6193.151 | 19398.655 | 23855.103 |
| t-372 | 65 | 1 | 40.869 | 44.199 | 875 | 0 | 2826 | 2826 | 0 | 4685.823 | 18874.367 | 22020.095 |
| t-106 | 24 | 2 | 220.788 | 89.77 | 824 | 0 | 5540 | 5540 | 0 | 7667.711 | 18612.223 | 20709.375 |
| t-465 | 66 | 2 | 87.718 | 94.646 | 667 | 0 | 5826 | 5826 | 0 | 5734.399 | 18612.223 | 24510.463 |
| t-222 | 138 | 4 | 76.871 | 178.909 | 2846 | 0 | 11129 | 11129 | 0 | 6422.527 | 18481.151 | 24903.679 |
| t-258 | 52 | 1 | 51.203 | 44.018 | 2470 | 0 | 2834 | 2834 | 0 | 5111.807 | 18219.007 | 20447.231 |
| t-239 | 39 | 2 | 130.401 | 82.547 | 225 | 0 | 4966 | 4966 | 0 | 2228.223 | 17956.863 | 19529.727 |
| t-383 | 200 | 7 | 96.136 | 319.746 | 2521 | 0 | 19762 | 19762 | 0 | 5373.951 | 17694.719 | 22937.599 |
| t-436 | 58 | 1 | 44.011 | 42.746 | 1128 | 0 | 2776 | 2776 | 0 | 4849.663 | 17563.647 | 19398.655 |
