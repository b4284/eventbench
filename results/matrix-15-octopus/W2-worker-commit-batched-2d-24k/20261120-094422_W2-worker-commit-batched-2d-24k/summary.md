# W2-worker-commit-batched-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70, group commit (max 64/tx, 8 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.265s, closing 1.637s, drain 11.099s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1357393 | 1357393 | 22840.669 | 21751.955 | 26750.7 | 0 | 0 | 0 | 385 | 6835 | 517465 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 1072260 | 6055.436 | 5357.567 | 8749.055 | 19415.039 | 26476.543 | 32817.151 |
| e2e_from_send | 1072260 | 1223.82 | 278.015 | 3360.767 | 14008.319 | 21135.359 | 27181.055 |
| producer_lag | 1072260 | 4831.616 | 4694.015 | 6287.359 | 7139.327 | 7643.135 | 7950.335 |
| publish_call | 1357393 | 80.817 | 71.423 | 142.079 | 258.687 | 357.887 | 429.823 |
| queue_wait | 1072260 | 1183.588 | 240.895 | 3317.759 | 13991.935 | 21118.975 | 27164.671 |
| broker_dwell | 1072260 | 1133.261 | 184.959 | 3270.655 | 13942.783 | 21053.439 | 27115.519 |
| receive_call | 1170036 | 26.582 | 0.007 | 85.375 | 294.911 | 460.031 | 538.111 |
| ctx_load | 1072260 | 7.412 | 4.947 | 15.631 | 42.943 | 68.735 | 173.183 |
| process | 1072260 | 10.396 | 0.001 | 0.003 | 0.025 | 2613.247 | 3035.135 |
| ctx_commit | 1072260 | 22.377 | 18.367 | 44.319 | 83.775 | 123.711 | 258.943 |
| ack | 1068964 | 0.008 | 0.003 | 0.006 | 0.033 | 0.614 | 59.711 |
| service | 1072260 | 40.233 | 24.975 | 58.623 | 111.103 | 2643.967 | 3145.727 |
| e2e_large | 284 | 7184.204 | 7094.271 | 8921.087 | 9830.399 | 11902.975 | 11902.975 |
| ready_wait | 1072260 | 163.124 | 101.631 | 366.591 | 966.655 | 1728.511 | 2605.055 |
| blocked | 1072260 | 157.922 | 95.167 | 363.519 | 964.095 | 1726.463 | 2605.055 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 21751.955 | 25322.2 |
| applied_total | 731779.367 | 1261125 |
| bench_cpu_cores | 3.875 | 4.442 |
| bench_cpu_pct | 16.147 | 18.51 |
| bench_gc_gen0 | 3.633 | 5 |
| bench_gc_gen1 | 3.633 | 5 |
| bench_gc_gen2 | 0.776 | 1 |
| bench_gc_heap_mb | 973.516 | 1220.2 |
| bench_mem_mb | 1507.814 | 1598.1 |
| bench_threadpool_queue | 155.306 | 1267 |
| bench_threadpool_threads | 40.898 | 44 |
| broker_backlog | 21121.184 | 52133 |
| ctr_eb_mosquitto_cpu_pct | 48.738 | 52.62 |
| ctr_eb_mosquitto_mem_mb | 9.067 | 23.3 |
| ctr_eb_pgbouncer_2_cpu_pct | 22.89 | 24.12 |
| ctr_eb_pgbouncer_2_mem_mb | 15.855 | 16.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 22.082 | 23.76 |
| ctr_eb_pgbouncer_3_mem_mb | 15.735 | 16.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 24.4 | 26.79 |
| ctr_eb_pgbouncer_4_mem_mb | 16.033 | 16.6 |
| ctr_eb_pgbouncer_cpu_pct | 21.966 | 23.34 |
| ctr_eb_pgbouncer_mem_mb | 16.155 | 17 |
| ctr_eb_postgres_cpu_pct | 1145.673 | 1180.85 |
| ctr_eb_postgres_mem_mb | 5297.78 | 5524.6 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 78.577 | 81.36 |
| in_flight | 21955.816 | 54989 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.029 | 1.08 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1459310.143 | 2520515 |
| octopus_mqtt_received_total | 80745 | 131290 |
| octopus_mqtt_sent_total | 38837.429 | 65410 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 5 | 10 |
| pg_active_loads | 7.367 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 23.082 | 34 |
| pg_conn_idle | 204.061 | 215 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 1.122 | 5 |
| pg_conn_total | 227.143 | 228 |
| pg_db_size_mb | 3528.792 | 4023.6 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 24124.498 | 27365.2 |
| pg_tup_del_per_s | 52226.894 | 63395.4 |
| pg_tup_fetch_per_s | 830098.439 | 935105.5 |
| pg_tup_ins_per_s | 64940.941 | 76231.9 |
| pg_tup_upd_per_s | 58244.859 | 64897.4 |
| pg_wal_mb_per_s | 125.318 | 236.29 |
| pgb_cl_active | 254.143 | 276 |
| pgb_cl_waiting | 0.163 | 4 |
| pgb_maxwait_ms | 0.021 | 0.51 |
| pgb_sv_active | 8.673 | 48 |
| pgb_sv_idle | 39.327 | 47 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 22840.669 | 27009.8 |
| produced_total | 753735.184 | 1316114 |
| publish_mb_per_s | 61.038 | 83.93 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 179,319 records are Completed |
| record_counts | pass | 179,319 records found, 179,319 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 385 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,357,393 events applied by workers, 1,357,393 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 517,465 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 964 ms, max 2603 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9142 seq 14: ready 122 ms before start, 122 ms of it with an idle slot in group 315
- [cross_source_blocking] source 6542 seq 28: ready 101 ms before start, 101 ms of it with an idle slot in group 222
- [cross_source_blocking] source 5614 seq 45: ready 102 ms before start, 102 ms of it with an idle slot in group 194
- [cross_source_blocking] source 6030 seq 10: ready 101 ms before start, 101 ms of it with an idle slot in group 206
- [cross_source_blocking] source 8846 seq 24: ready 126 ms before start, 126 ms of it with an idle slot in group 306
- [cross_source_blocking] source 10350 seq 17: ready 149 ms before start, 149 ms of it with an idle slot in group 347
- [cross_source_blocking] source 15022 seq 27: ready 102 ms before start, 102 ms of it with an idle slot in group 469
- [cross_source_blocking] source 6038 seq 14: ready 101 ms before start, 101 ms of it with an idle slot in group 206
- [cross_source_blocking] source 9910 seq 12: ready 100 ms before start, 100 ms of it with an idle slot in group 340
- [cross_source_blocking] source 7766 seq 18: ready 104 ms before start, 104 ms of it with an idle slot in group 263
- [cross_source_blocking] source 10734 seq 46: ready 120 ms before start, 120 ms of it with an idle slot in group 352
- [cross_source_blocking] source 4086 seq 20: ready 155 ms before start, 155 ms of it with an idle slot in group 136
- [cross_source_blocking] source 4046 seq 11: ready 129 ms before start, 129 ms of it with an idle slot in group 136
- [cross_source_blocking] source 11950 seq 28: ready 209 ms before start, 209 ms of it with an idle slot in group 383
- [cross_source_blocking] source 7734 seq 22: ready 105 ms before start, 105 ms of it with an idle slot in group 263
- [cross_source_blocking] source 11374 seq 22: ready 101 ms before start, 101 ms of it with an idle slot in group 371
- [cross_source_blocking] source 9370 seq 6: ready 101 ms before start, 101 ms of it with an idle slot in group 325
- [cross_source_blocking] source 7566 seq 30: ready 109 ms before start, 109 ms of it with an idle slot in group 260
- [cross_source_blocking] source 7574 seq 21: ready 103 ms before start, 103 ms of it with an idle slot in group 260
- [cross_source_blocking] source 10254 seq 35: ready 101 ms before start, 101 ms of it with an idle slot in group 344

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 20298 | 20298 | 0 | 15532.031 | 29622.271 | 32899.071 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 23193 | 23193 | 0 | 13107.199 | 25165.823 | 29097.983 |
| t-194 | 78 | 5 | 183.847 | 245.382 | 1184 | 0 | 13721 | 13721 | 0 | 10616.831 | 23855.103 | 27000.831 |
| t-347 | 162 | 7 | 116.152 | 315.598 | 833 | 0 | 17773 | 17773 | 0 | 7667.711 | 21233.663 | 24510.463 |
| t-464 | 153 | 5 | 93.129 | 234.299 | 352 | 0 | 13367 | 13367 | 0 | 7569.407 | 20971.519 | 25427.967 |
| t-222 | 138 | 6 | 115.927 | 269.807 | 2846 | 0 | 14988 | 14988 | 0 | 7340.031 | 20185.087 | 25690.111 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 13032 | 13032 | 0 | 7208.959 | 18743.295 | 21626.879 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 7376 | 7376 | 0 | 9043.967 | 18481.151 | 19267.583 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 8671 | 8671 | 0 | 8323.071 | 18481.151 | 22020.095 |
| t-121 | 76 | 5 | 182.916 | 224.389 | 149 | 0 | 12539 | 12539 | 0 | 6979.583 | 17301.503 | 19267.583 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 11307 | 11307 | 0 | 7569.407 | 16908.287 | 20840.447 |
| t-306 | 134 | 7 | 140.526 | 314.244 | 2978 | 0 | 17296 | 17296 | 0 | 6160.383 | 16515.071 | 22020.095 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 28051 | 28051 | 0 | 5701.631 | 16252.927 | 19791.871 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 7076 | 7076 | 0 | 7307.263 | 15728.639 | 18087.935 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 6914 | 6914 | 0 | 7012.351 | 15269.887 | 17825.791 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 13766 | 13766 | 0 | 6062.079 | 15073.279 | 22544.383 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 8936 | 8936 | 0 | 6324.223 | 14811.135 | 17825.791 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 12561 | 12561 | 0 | 6455.295 | 14811.135 | 23855.103 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 5066 | 5066 | 0 | 6127.615 | 14155.775 | 15925.247 |
| t-483 | 200 | 6 | 76.95 | 259.099 | 2470 | 0 | 14678 | 14678 | 0 | 6356.991 | 14090.239 | 17039.359 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 10902 | 10902 | 0 | 6291.455 | 13893.631 | 16580.607 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 10281 | 10281 | 0 | 6488.063 | 13893.631 | 18087.935 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 8319 | 8319 | 0 | 5931.007 | 13828.095 | 16187.391 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 20477 | 20477 | 0 | 6062.079 | 13828.095 | 17956.863 |
| t-263 | 93 | 4 | 111.763 | 175.79 | 374 | 0 | 9788 | 9788 | 0 | 6291.455 | 13762.559 | 16711.679 |
