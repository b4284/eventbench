# U4b-dc-freeze-leader-dc-60s-etcd3.5.17 — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 116.642s, closing 3.583s, drain 58.963s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 451109 | 451109 | 2845.434 | 1952.677 | 16432.2 | 154 | 351 | 351 | 270 | 2282 | 151742 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 282982 | 49372.725 | 12468.223 | 109379.583 | 115671.039 | 118489.087 | 147324.927 |
| e2e_from_send | 282982 | 48888.572 | 11878.399 | 108986.367 | 115277.823 | 118226.943 | 147324.927 |
| producer_lag | 282982 | 484.169 | 350.719 | 1001.983 | 1503.231 | 1801.215 | 84672.511 |
| publish_call | 451109 | 460.023 | 104.703 | 206.335 | 441.855 | 83492.863 | 84803.583 |
| queue_wait | 282982 | 48381.554 | 11493.375 | 108920.831 | 115212.287 | 118226.943 | 147193.855 |
| broker_dwell | 282982 | 47745.629 | 11059.199 | 108789.759 | 115081.215 | 118161.407 | 146276.351 |
| receive_call | 380073 | 35.47 | 9.207 | 104.895 | 343.807 | 488.703 | 598.015 |
| ctx_load | 282982 | 62.452 | 4.871 | 24.367 | 66.815 | 252.543 | 82509.823 |
| process | 282982 | 10.836 | 0.002 | 0.005 | 0.065 | 2629.631 | 3000.319 |
| ctx_commit | 282982 | 433.686 | 71.679 | 179.711 | 413.183 | 83165.183 | 84475.903 |
| ack | 282362 | 0.008 | 0.003 | 0.006 | 0.037 | 0.569 | 48.447 |
| service | 282982 | 507.074 | 83.519 | 198.143 | 930.303 | 83230.719 | 85852.159 |
| e2e_large | 170 | 7903.274 | 3274.751 | 5595.135 | 87293.951 | 88670.207 | 88670.207 |
| ready_wait | 282982 | 3325.344 | 195.071 | 2030.591 | 94633.983 | 102432.767 | 117702.655 |
| blocked | 282982 | 1642.148 | 125.823 | 1120.255 | 68550.655 | 78708.735 | 90439.679 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 1952.677 | 10758.8 |
| applied_total | 254607.306 | 280136 |
| bench_cpu_cores | 1.024 | 7.868 |
| bench_cpu_pct | 4.269 | 32.79 |
| bench_gc_gen0 | 0.633 | 3 |
| bench_gc_gen1 | 0.592 | 3 |
| bench_gc_gen2 | 0.143 | 1 |
| bench_gc_heap_mb | 978.239 | 1308.3 |
| bench_mem_mb | 1525.232 | 1621.3 |
| bench_threadpool_queue | 619.306 | 11715 |
| bench_threadpool_threads | 55.459 | 62 |
| broker_backlog | 132661.939 | 143326 |
| ctr_eb_dc1a_cpu_pct | 883.829 | 958.64 |
| ctr_eb_dc1a_mem_mb | 4457.526 | 5530.4 |
| ctr_eb_dc1b_cpu_pct | 66.943 | 83.59 |
| ctr_eb_dc1b_mem_mb | 2054.235 | 2386.1 |
| ctr_eb_dc2a_cpu_pct | 19.039 | 83.13 |
| ctr_eb_dc2a_mem_mb | 2376.744 | 2534.9 |
| ctr_eb_dc2b_cpu_pct | 23.213 | 238.06 |
| ctr_eb_dc2b_mem_mb | 2419.233 | 3618.2 |
| ctr_eb_etcd_dc1_cpu_pct | 1.791 | 2.18 |
| ctr_eb_etcd_dc1_mem_mb | 19.991 | 24.9 |
| ctr_eb_etcd_dc2_cpu_pct | 1.049 | 2.2 |
| ctr_eb_etcd_dc2_mem_mb | 20.28 | 22.2 |
| ctr_eb_etcd_w_cpu_pct | 1.14 | 2.19 |
| ctr_eb_etcd_w_mem_mb | 22.636 | 24 |
| ctr_eb_mosquitto_cpu_pct | 14.164 | 53.55 |
| ctr_eb_mosquitto_mem_mb | 6.043 | 20.5 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.017 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 9.508 | 9.7 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.016 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 10 | 10 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.016 | 0.05 |
| ctr_eb_pgbouncer_4_mem_mb | 10.908 | 11.1 |
| ctr_eb_pgbouncer_cpu_pct | 0.016 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 11.2 | 11.2 |
| ctr_eb_postgres_cpu_pct | 0.603 | 2.7 |
| ctr_eb_postgres_mem_mb | 2154.463 | 2156.3 |
| duplicates_per_s | 1.367 | 120 |
| errors_per_s | 68.937 | 6755.8 |
| host_cpu_pct | 18.974 | 78.42 |
| in_flight | 133817.214 | 143182 |
| nacks_per_s | 68.937 | 6755.8 |
| octopus_dispatcher_cores | 1.306 | 1.76 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 669212.082 | 797508 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 161256.061 | 177665 |
| octopus_mqtt_sent_total | 157140.143 | 175227 |
| octopus_publish_retries_total | 3008.571 | 14742 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.01 | 21 |
| pg_active_loads | 4.684 | 17 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 77.041 | 105 |
| pg_conn_idle | 192.52 | 259 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.051 | 2 |
| pg_conn_total | 269.561 | 297 |
| pg_db_size_mb | 1611.079 | 1711.9 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 8.114 | 24.82 |
| pg_replica_replay_lag_ms | 22.88 | 283.1 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 23181.799 | 24651.9 |
| pg_tup_del_per_s | 29089.362 | 34944.6 |
| pg_tup_fetch_per_s | 541589.072 | 602105.6 |
| pg_tup_ins_per_s | 50483.602 | 60279.6 |
| pg_tup_upd_per_s | 26558.235 | 28130.1 |
| pg_wal_mb_per_s | 90.585 | 108.94 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 2845.434 | 15603.2 |
| produced_total | 388424.52 | 419264 |
| publish_mb_per_s | 10.749 | 69.34 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 74,482 records are Completed |
| record_counts | pass | 74,482 records found, 74,482 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 270 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 451,109 events applied by workers, 451,109 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 151,742 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 68551 ms, max 90408 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 12392 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 393
- [cross_source_blocking] source 11528 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 375
- [cross_source_blocking] source 5016 seq 9: ready 108 ms before start, 108 ms of it with an idle slot in group 173
- [cross_source_blocking] source 9000 seq 8: ready 135 ms before start, 135 ms of it with an idle slot in group 310
- [cross_source_blocking] source 5568 seq 2: ready 140 ms before start, 140 ms of it with an idle slot in group 192
- [cross_source_blocking] source 3216 seq 7: ready 134 ms before start, 134 ms of it with an idle slot in group 107
- [cross_source_blocking] source 5868 seq 4: ready 139 ms before start, 139 ms of it with an idle slot in group 203
- [cross_source_blocking] source 12744 seq 7: ready 209 ms before start, 120 ms of it with an idle slot in group 402
- [cross_source_blocking] source 5716 seq 12: ready 125 ms before start, 104 ms of it with an idle slot in group 197
- [cross_source_blocking] source 1993 seq 6: ready 173 ms before start, 173 ms of it with an idle slot in group 58
- [cross_source_blocking] source 2661 seq 3: ready 172 ms before start, 172 ms of it with an idle slot in group 82
- [cross_source_blocking] source 5917 seq 11: ready 173 ms before start, 173 ms of it with an idle slot in group 205
- [cross_source_blocking] source 13609 seq 7: ready 173 ms before start, 173 ms of it with an idle slot in group 431
- [cross_source_blocking] source 15117 seq 4: ready 100 ms before start, 100 ms of it with an idle slot in group 472
- [cross_source_blocking] source 14401 seq 5: ready 173 ms before start, 173 ms of it with an idle slot in group 459
- [cross_source_blocking] source 1989 seq 7: ready 173 ms before start, 173 ms of it with an idle slot in group 58
- [cross_source_blocking] source 13205 seq 9: ready 175 ms before start, 175 ms of it with an idle slot in group 420
- [cross_source_blocking] source 13669 seq 11: ready 172 ms before start, 172 ms of it with an idle slot in group 435
- [cross_source_blocking] source 6249 seq 6: ready 173 ms before start, 173 ms of it with an idle slot in group 213
- [cross_source_blocking] source 12701 seq 17: ready 167 ms before start, 167 ms of it with an idle slot in group 400
- [cross_source_blocking] source 1341 seq 4: ready 173 ms before start, 155 ms of it with an idle slot in group 42
- [cross_source_blocking] source 9029 seq 4: ready 174 ms before start, 174 ms of it with an idle slot in group 312

## Error samples

- t-165#0 src=4772 seq=24 op=Update rec=20495583936518 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-081#0 src=2640 seq=12 op=Complete rec=11338713661441 ev=6: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-043#0 src=1357 seq=18 op=Get rec=5828270620675 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-353#1 src=10775 seq=18 op=Update rec=46278272614402 ev=7: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-315#1 src=9140 seq=18 op=Update rec=39256001085443 ev=4: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-387#0 src=12116 seq=13 op=Update rec=52037823758338 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-118#0 src=3415 seq=21 op=Update rec=14667313315844 ev=3: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-380#0 src=11741 seq=36 op=Complete rec=50427211022340 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-393#0 src=12377 seq=17 op=Get rec=53158810222597 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-069#0 src=2372 seq=10 op=Update rec=10187662426115 ev=1: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-384#0 src=12024 seq=17 op=Get rec=51642686767107 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-300#0 src=8655 seq=12 op=Update rec=37172941946882 ev=6: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-183#0 src=5357 seq=10 op=Update rec=23008139804674 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-200#0 src=5793 seq=11 op=Update rec=24880745545730 ev=3: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-479#0 src=15325 seq=19 op=Update rec=65820373811202 ev=7: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-276#0 src=8142 seq=11 op=Update rec=34969623724033 ev=6: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-334#0 src=9662 seq=36 op=Update rec=41497974013955 ev=7: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-108#0 src=3239 seq=25 op=Complete rec=13911399071747 ev=2: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-493#0 src=15758 seq=49 op=Get rec=67680094650375 ev=3: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.
- t-340#0 src=9829 seq=7 op=Update rec=42215233552386 ev=4: NpgsqlException: Unable to connect to a suitable host. Check inner exception for more details.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-230 | 30 | 2 | 104.35 | 51.662 | 1033 | 0 | 1652 | 1652 | 1 | 1957.887 | 140509.183 | 147849.215 |
| t-120 | 37 | 2 | 83.329 | 50.593 | 1412 | 0 | 1700 | 1700 | 0 | 2408.447 | 119537.663 | 122683.391 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3301 | 3301 | 0 | 114819.071 | 119537.663 | 122683.391 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3360 | 3360 | 0 | 113246.207 | 119013.375 | 121110.527 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1590 | 1590 | 0 | 114294.783 | 119013.375 | 120061.951 |
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1579 | 1579 | 0 | 113770.495 | 118489.087 | 119013.375 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1684 | 1684 | 0 | 112721.919 | 118489.087 | 120586.239 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1627 | 1627 | 0 | 112197.631 | 117964.799 | 120061.951 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 7488 | 7488 | 0 | 112197.631 | 116916.223 | 120061.951 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 4832 | 4832 | 0 | 111673.343 | 116391.935 | 117964.799 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3055 | 3055 | 0 | 112197.631 | 116391.935 | 116391.935 |
| t-314 | 24 | 2 | 187.589 | 74.747 | 525 | 0 | 2500 | 2500 | 0 | 104857.599 | 115867.647 | 116916.223 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1618 | 1618 | 0 | 111149.055 | 115343.359 | 115343.359 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1523 | 1523 | 0 | 110624.767 | 114819.071 | 115343.359 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 6539 | 6539 | 0 | 98041.855 | 114294.783 | 116916.223 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 4548 | 4548 | 0 | 110100.479 | 114294.783 | 116916.223 |
| t-186 | 34 | 2 | 103.912 | 58.611 | 1259 | 0 | 1973 | 1973 | 0 | 6389.759 | 113770.495 | 115343.359 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 6697 | 6697 | 0 | 98041.855 | 113770.495 | 116916.223 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3510 | 3510 | 0 | 100663.295 | 113246.207 | 115867.647 |
| t-271 | 59 | 3 | 123.419 | 121.045 | 334 | 0 | 4076 | 4076 | 1 | 108527.615 | 113246.207 | 114819.071 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 2417 | 2417 | 0 | 5308.415 | 112721.919 | 115343.359 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1649 | 1649 | 0 | 101187.583 | 112721.919 | 114294.783 |
| t-306 | 134 | 4 | 75.147 | 168.045 | 2978 | 0 | 5763 | 5763 | 0 | 110100.479 | 112721.919 | 113770.495 |
| t-450 | 63 | 2 | 76.039 | 78.744 | 165 | 0 | 2724 | 2724 | 0 | 108003.327 | 112721.919 | 114819.071 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3516 | 3516 | 0 | 100663.295 | 112721.919 | 113770.495 |
