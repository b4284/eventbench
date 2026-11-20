# O2-octopus-v11-2d-24k — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 8 worker connections), commit through the apply hook`
- Store: `bench tables via Postgres (postgres @ 127.0.0.1:6432/bench (pool max 1000), unlogged=False, fillfactor=70); commits through Octopus's apply hook`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 60.241s, closing 2.464s, drain 12.904s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 1012537 | 1012537 | 16116.547 | 15526.047 | 20805.5 | 0 | 0 | 0 | 314 | 5109 | 343587 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 728155 | 13773.668 | 13492.223 | 20070.399 | 30343.167 | 40468.479 | 45613.055 |
| e2e_from_send | 728155 | 1385.105 | 313.343 | 3710.975 | 15097.855 | 27967.487 | 36044.799 |
| producer_lag | 728155 | 12388.563 | 12492.799 | 18333.695 | 20004.863 | 20561.919 | 21053.439 |
| publish_call | 1012537 | 110.857 | 108.287 | 168.191 | 234.239 | 292.351 | 506.367 |
| queue_wait | 728155 | 1308.451 | 234.495 | 3633.151 | 15048.703 | 27934.719 | 36044.799 |
| broker_dwell | 728155 | 1260.493 | 180.607 | 3581.951 | 15007.743 | 27901.951 | 35979.263 |
| receive_call | 932820 | 28.81 | 0.015 | 94.591 | 329.215 | 489.471 | 541.183 |
| ctx_load | 728155 | 0.001 | 0.001 | 0.001 | 0.001 | 0.011 | 35.135 |
| process | 728155 | 10.537 | 0.001 | 0.002 | 0.014 | 2611.199 | 3026.943 |
| ctx_commit | 728155 | 66.084 | 63.135 | 107.071 | 151.039 | 227.199 | 463.103 |
| service | 728155 | 76.655 | 63.327 | 108.095 | 162.303 | 2684.927 | 3241.983 |
| e2e_large | 213 | 14116.989 | 14131.199 | 20479.999 | 21872.639 | 22331.391 | 22331.391 |
| ready_wait | 728155 | 182.978 | 97.919 | 495.871 | 950.271 | 1418.239 | 3033.087 |
| blocked | 728155 | 177.074 | 90.559 | 492.031 | 946.175 | 1406.975 | 3033.087 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 15526.047 | 18072.4 |
| applied_total | 565924.224 | 939098 |
| bench_cpu_cores | 3.662 | 4.229 |
| bench_cpu_pct | 15.257 | 17.62 |
| bench_gc_gen0 | 4.918 | 7 |
| bench_gc_gen1 | 4.878 | 6 |
| bench_gc_gen2 | 0.735 | 2 |
| bench_gc_heap_mb | 953.716 | 1224.2 |
| bench_mem_mb | 1468.606 | 1628.2 |
| bench_threadpool_queue | 34.531 | 1011 |
| bench_threadpool_threads | 43.082 | 44 |
| broker_backlog | 16883.714 | 32476 |
| ctr_eb_mosquitto_cpu_pct | 97.239 | 98.67 |
| ctr_eb_mosquitto_mem_mb | 12.888 | 30 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.767 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.886 | 8.2 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.741 | 8 |
| ctr_eb_pgbouncer_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_mem_mb | 8.543 | 8.9 |
| ctr_eb_postgres_cpu_pct | 1018.762 | 1045.28 |
| ctr_eb_postgres_mem_mb | 2226.288 | 2619.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 0 | 0 |
| host_cpu_pct | 74.44 | 76.19 |
| in_flight | 17514.469 | 34109 |
| nacks_per_s | 0 | 0 |
| octopus_dispatcher_cores | 1.901 | 1.94 |
| octopus_dispatchers_alive | 2 | 2 |
| octopus_items_sent_total | 1140057.286 | 1961915 |
| octopus_mqtt_received_total | 578117.939 | 1034775 |
| octopus_mqtt_sent_total | 1042934.633 | 1813344 |
| octopus_publish_retries_total | 0 | 0 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 0 | 0 |
| pg_active_loads | 0 | 0 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 11.633 | 22 |
| pg_conn_idle | 23.367 | 32 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.429 | 4 |
| pg_conn_total | 35 | 35 |
| pg_db_size_mb | 3252.345 | 3564.9 |
| pg_deadlocks | 0 | 0 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 7894.253 | 8971.9 |
| pg_tup_del_per_s | 38517.649 | 45255 |
| pg_tup_fetch_per_s | 982649.896 | 1098435.3 |
| pg_tup_ins_per_s | 48544.878 | 55226.9 |
| pg_tup_upd_per_s | 42131.806 | 47049.1 |
| pg_wal_mb_per_s | 94.53 | 131.4 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 16116.547 | 18210.1 |
| produced_total | 583438.694 | 971498 |
| publish_mb_per_s | 44.79 | 63.99 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 140,276 records are Completed |
| record_counts | pass | 140,276 records found, 140,276 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 314 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 1,012,537 events applied by workers, 1,012,537 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 343,587 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 946 ms, max 3033 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7260 seq 9: ready 168 ms before start, 168 ms of it with an idle slot in group 248
- [cross_source_blocking] source 4476 seq 9: ready 148 ms before start, 148 ms of it with an idle slot in group 154
- [cross_source_blocking] source 13212 seq 31: ready 109 ms before start, 109 ms of it with an idle slot in group 421
- [cross_source_blocking] source 9116 seq 17: ready 187 ms before start, 187 ms of it with an idle slot in group 315
- [cross_source_blocking] source 7132 seq 16: ready 148 ms before start, 148 ms of it with an idle slot in group 242
- [cross_source_blocking] source 12252 seq 7: ready 154 ms before start, 154 ms of it with an idle slot in group 389
- [cross_source_blocking] source 1020 seq 19: ready 146 ms before start, 146 ms of it with an idle slot in group 31
- [cross_source_blocking] source 2172 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 62
- [cross_source_blocking] source 8508 seq 21: ready 144 ms before start, 144 ms of it with an idle slot in group 292
- [cross_source_blocking] source 12892 seq 12: ready 142 ms before start, 142 ms of it with an idle slot in group 408
- [cross_source_blocking] source 15740 seq 10: ready 155 ms before start, 155 ms of it with an idle slot in group 491
- [cross_source_blocking] source 10684 seq 17: ready 192 ms before start, 192 ms of it with an idle slot in group 350
- [cross_source_blocking] source 9052 seq 7: ready 200 ms before start, 168 ms of it with an idle slot in group 313
- [cross_source_blocking] source 14492 seq 15: ready 119 ms before start, 119 ms of it with an idle slot in group 461
- [cross_source_blocking] source 6844 seq 6: ready 161 ms before start, 161 ms of it with an idle slot in group 233
- [cross_source_blocking] source 1052 seq 14: ready 150 ms before start, 150 ms of it with an idle slot in group 32
- [cross_source_blocking] source 13468 seq 8: ready 133 ms before start, 133 ms of it with an idle slot in group 429
- [cross_source_blocking] source 14588 seq 17: ready 239 ms before start, 239 ms of it with an idle slot in group 461
- [cross_source_blocking] source 11644 seq 20: ready 134 ms before start, 134 ms of it with an idle slot in group 378
- [cross_source_blocking] source 1532 seq 19: ready 235 ms before start, 235 ms of it with an idle slot in group 45

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 8381 | 8381 | 0 | 33292.287 | 43515.903 | 45613.055 |
| t-468 | 61 | 5 | 237.543 | 237.588 | 1645 | 0 | 9643 | 9643 | 0 | 25034.751 | 38273.023 | 40894.463 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 15092 | 15092 | 0 | 21757.951 | 34340.863 | 37224.447 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 6654 | 6654 | 0 | 17825.791 | 33816.575 | 35913.727 |
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 7617 | 7617 | 0 | 18481.151 | 32243.711 | 35127.295 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 15158 | 15158 | 0 | 19791.871 | 31326.207 | 33816.575 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 3753 | 3753 | 0 | 16449.535 | 30670.847 | 33554.431 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 6440 | 6440 | 0 | 17301.503 | 30539.775 | 31981.567 |
| t-409 | 48 | 2 | 120.851 | 95.245 | 671 | 0 | 4006 | 4006 | 0 | 16383.999 | 29884.415 | 33161.215 |
| t-245 | 43 | 2 | 133.219 | 91.504 | 2232 | 0 | 3733 | 3733 | 0 | 16187.391 | 29753.343 | 32374.783 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 5126 | 5126 | 0 | 15794.175 | 28835.839 | 30670.847 |
| t-186 | 34 | 3 | 194.316 | 109.602 | 1259 | 0 | 4370 | 4370 | 0 | 17432.575 | 28704.767 | 30277.631 |
| t-383 | 200 | 10 | 144.979 | 482.198 | 2521 | 0 | 20030 | 20030 | 0 | 16383.999 | 28704.767 | 32374.783 |
| t-052 | 62 | 2 | 82.347 | 86.403 | 1837 | 0 | 3661 | 3661 | 0 | 14942.207 | 28442.623 | 31326.207 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 10236 | 10236 | 0 | 15466.495 | 28442.623 | 31981.567 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 3956 | 3956 | 0 | 15007.743 | 28180.479 | 29884.415 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 17120 | 17120 | 0 | 15859.711 | 27918.335 | 29753.343 |
| t-344 | 195 | 11 | 153.189 | 504.11 | 571 | 0 | 20744 | 20744 | 0 | 15663.103 | 27787.263 | 30277.631 |
| t-371 | 51 | 3 | 118.987 | 104.407 | 3301 | 0 | 4237 | 4237 | 0 | 15138.815 | 27525.119 | 30801.919 |
| t-490 | 56 | 4 | 175.559 | 164.988 | 245 | 0 | 6673 | 6673 | 0 | 15269.887 | 26869.759 | 29097.983 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 9250 | 9250 | 0 | 14614.527 | 26738.687 | 28573.695 |
| t-450 | 63 | 3 | 142.193 | 147.252 | 165 | 0 | 6225 | 6225 | 0 | 14024.703 | 25690.111 | 27918.335 |
| t-271 | 59 | 5 | 230.794 | 226.355 | 334 | 0 | 9287 | 9287 | 0 | 13893.631 | 25427.967 | 28835.839 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 5733 | 5733 | 0 | 14548.991 | 24903.679 | 26083.327 |
| t-004 | 38 | 3 | 163.013 | 106.748 | 2178 | 0 | 4448 | 4448 | 0 | 13762.559 | 24117.247 | 25952.255 |
