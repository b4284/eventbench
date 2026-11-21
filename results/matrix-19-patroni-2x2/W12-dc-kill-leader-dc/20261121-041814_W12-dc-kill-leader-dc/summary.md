# W12-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.381s, closing 2.58s, drain 120.004s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 538505 | 530982 | 7472.173 | 3647.696 | 22163.6 | 0 | 2522 | 2522 | 302 | 2742 | 76251 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 363049 | 36400.142 | 41418.751 | 58261.503 | 69074.943 | 78315.519 | 171311.103 |
| e2e_from_send | 363049 | 29885.923 | 32145.407 | 57147.391 | 68026.367 | 77398.015 | 169869.311 |
| producer_lag | 369223 | 6700.419 | 1023.999 | 23543.807 | 24756.223 | 25083.903 | 25657.343 |
| publish_call | 538505 | 203.76 | 103.039 | 220.543 | 463.871 | 24395.775 | 24444.927 |
| queue_wait | 363049 | 29706.716 | 31981.567 | 57049.087 | 67698.687 | 75956.223 | 109838.335 |
| broker_dwell | 363049 | 29483.249 | 31211.519 | 56918.015 | 67567.615 | 75890.687 | 109772.799 |
| receive_call | 496934 | 21.456 | 4.307 | 47.423 | 314.367 | 472.831 | 558.591 |
| ctx_load | 363049 | 85.528 | 1.733 | 9.871 | 50.527 | 15024.127 | 30015.487 |
| process | 363049 | 10.828 | 0.002 | 0.004 | 0.033 | 2629.631 | 3002.367 |
| ctx_commit | 363049 | 82.805 | 15.303 | 178.047 | 537.087 | 990.207 | 59047.935 |
| ack | 362410 | 0.006 | 0.003 | 0.006 | 0.025 | 0.296 | 37.727 |
| service | 363049 | 179.222 | 19.551 | 200.063 | 1466.367 | 15048.703 | 61898.751 |
| e2e_large | 202 | 9764.405 | 3923.967 | 27803.647 | 42139.647 | 43614.207 | 43614.207 |
| ready_wait | 363049 | 1428.915 | 12.767 | 1287.167 | 44335.103 | 55443.455 | 93192.191 |
| blocked | 363049 | 871.419 | 9.487 | 469.503 | 28606.463 | 33062.911 | 36667.391 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3647.696 | 12175.7 |
| applied_total | 185662.633 | 246143 |
| bench_cpu_cores | 1.709 | 4.277 |
| bench_cpu_pct | 7.119 | 17.82 |
| bench_gc_gen0 | 1.122 | 3 |
| bench_gc_gen1 | 0.857 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1080.671 | 1487.9 |
| bench_mem_mb | 1744.99 | 1958 |
| bench_threadpool_queue | 8.02 | 137 |
| bench_threadpool_threads | 43.837 | 44 |
| broker_backlog | 162565.49 | 186283 |
| ctr_eb_dc1a_cpu_pct | 44.241 | 52.98 |
| ctr_eb_dc1a_mem_mb | 3894.052 | 3998.8 |
| ctr_eb_dc1b_cpu_pct | 702.831 | 873.07 |
| ctr_eb_dc1b_mem_mb | 9012.452 | 9975.4 |
| ctr_eb_dc2a_cpu_pct | 49.844 | 241.57 |
| ctr_eb_dc2a_mem_mb | 6541.335 | 8243.6 |
| ctr_eb_dc2b_cpu_pct | 71.86 | 891.34 |
| ctr_eb_dc2b_mem_mb | 4037.486 | 5343.4 |
| ctr_eb_etcd_dc1_cpu_pct | 1.445 | 1.63 |
| ctr_eb_etcd_dc1_mem_mb | 31.205 | 32.8 |
| ctr_eb_etcd_dc2_cpu_pct | 1.311 | 1.98 |
| ctr_eb_etcd_dc2_mem_mb | 30.271 | 33.4 |
| ctr_eb_etcd_w_cpu_pct | 1.582 | 2.73 |
| ctr_eb_etcd_w_mem_mb | 52.555 | 54.9 |
| ctr_eb_mosquitto_cpu_pct | 20.544 | 48.79 |
| ctr_eb_mosquitto_mem_mb | 5.573 | 10.8 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.016 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.021 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.816 | 8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 9.2 | 9.2 |
| ctr_eb_postgres_cpu_pct | 0.81 | 2.72 |
| ctr_eb_postgres_mem_mb | 1953.645 | 1974.1 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 51.488 | 1605.5 |
| host_cpu_pct | 36.295 | 90.85 |
| in_flight | 169790.918 | 255501 |
| nacks_per_s | 51.488 | 1605.5 |
| octopus_dispatcher_cores | 1.064 | 1.65 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 551772.082 | 677952 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 150255.245 | 182493 |
| octopus_mqtt_sent_total | 113720.592 | 146051 |
| octopus_publish_retries_total | 6271.02 | 16560 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 17.224 | 20 |
| pg_active_loads | 16.265 | 26 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 62.102 | 73 |
| pg_conn_idle | 204.653 | 228 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 17.755 | 29 |
| pg_conn_total | 266.755 | 268 |
| pg_db_size_mb | 1433.927 | 1597.4 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 5.475 | 22.61 |
| pg_replica_replay_lag_ms | 64.282 | 306.4 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 21153.929 | 23997.7 |
| pg_tup_del_per_s | 31288.161 | 33879.4 |
| pg_tup_fetch_per_s | 599247.369 | 682413.9 |
| pg_tup_ins_per_s | 51866.539 | 58958.5 |
| pg_tup_upd_per_s | 24013.494 | 28084.4 |
| pg_wal_mb_per_s | 91.72 | 102.43 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 7472.173 | 32130.6 |
| produced_total | 355453.551 | 501644 |
| publish_mb_per_s | 26.554 | 76.54 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 15401/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 1,264 records not Completed |
| record_counts | **FAIL** | 84,382 records found, 85,083 expected; 599 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 302 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 530,982 events applied by workers, 538,505 generated |
| broker_drained | **FAIL** | 7,523 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 76,251 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 28606 ms, max 36664 ms) |

Sample mismatches:

- source 10121: record 43469364002823 left in status Open (version 1)
- source 6585: record 28282359644161 left in status Open (version 9)
- source 12473: record 53571127083009 left in status Open (version 2)
- source 12308: record 52862457479169 left in status Open (version 3)
- source 15853: record 68088116543489 left in status Open (version 24)
- source 3150: record 13529146982401 left in status Open (version 10)
- source 10107: record 43409234460673 left in status Open (version 2)
- source 10059: record 43203076030465 left in status Open (version 6)
- source 7851: record 33719788240897 left in status Open (version 6)
- source 10300: record 44238163148805 left in status Open (version 1)
- source 10246: record 44006234914822 left in status Open (version 2)
- source 7899: record 33925946671105 left in status Open (version 5)
- source 2225: record 9556302233609 left in status Open (version 1)
- source 2232: record 9586367004679 left in status Open (version 1)
- source 12328: record 52948356825089 left in status Open (version 4)
- source 10263: record 44079249358849 left in status Open (version 8)
- source 10114: record 43439299231745 left in status Open (version 7)
- source 15889: record 68242735366152 left in status Open (version 3)
- source 10301: record 44242458116107 left in status Open (version 3)
- source 10062: record 43215960932353 left in status Open (version 6)

## Cross-source blocking samples

- [cross_source_blocking] source 7260 seq 6: ready 127 ms before start, 127 ms of it with an idle slot in group 248
- [cross_source_blocking] source 6812 seq 11: ready 126 ms before start, 126 ms of it with an idle slot in group 232
- [cross_source_blocking] source 15752 seq 8: ready 109 ms before start, 109 ms of it with an idle slot in group 492
- [cross_source_blocking] source 2792 seq 6: ready 109 ms before start, 109 ms of it with an idle slot in group 87
- [cross_source_blocking] source 11528 seq 9: ready 109 ms before start, 109 ms of it with an idle slot in group 375
- [cross_source_blocking] source 404 seq 12: ready 109 ms before start, 109 ms of it with an idle slot in group 12
- [cross_source_blocking] source 1614 seq 3: ready 134 ms before start, 134 ms of it with an idle slot in group 48
- [cross_source_blocking] source 15811 seq 7: ready 137 ms before start, 137 ms of it with an idle slot in group 495
- [cross_source_blocking] source 10324 seq 6: ready 129 ms before start, 129 ms of it with an idle slot in group 346
- [cross_source_blocking] source 298 seq 5: ready 134 ms before start, 134 ms of it with an idle slot in group 8
- [cross_source_blocking] source 13012 seq 6: ready 129 ms before start, 129 ms of it with an idle slot in group 414
- [cross_source_blocking] source 4238 seq 7: ready 226 ms before start, 226 ms of it with an idle slot in group 142
- [cross_source_blocking] source 2378 seq 2: ready 146 ms before start, 146 ms of it with an idle slot in group 69
- [cross_source_blocking] source 9026 seq 3: ready 145 ms before start, 145 ms of it with an idle slot in group 312
- [cross_source_blocking] source 14062 seq 7: ready 758 ms before start, 111 ms of it with an idle slot in group 447
- [cross_source_blocking] source 10618 seq 13: ready 212 ms before start, 133 ms of it with an idle slot in group 349
- [cross_source_blocking] source 2787 seq 9: ready 205 ms before start, 205 ms of it with an idle slot in group 86
- [cross_source_blocking] source 2795 seq 6: ready 216 ms before start, 216 ms of it with an idle slot in group 87
- [cross_source_blocking] source 8234 seq 14: ready 327 ms before start, 105 ms of it with an idle slot in group 278
- [cross_source_blocking] source 10912 seq 7: ready 150 ms before start, 150 ms of it with an idle slot in group 355
- [cross_source_blocking] source 11606 seq 8: ready 146 ms before start, 146 ms of it with an idle slot in group 377

## Error samples

- t-131#1 src=3954 seq=11 op=Update rec=16982300688387 ev=1: NpgsqlException: No suitable host was found.
- t-029#0 src=979 seq=34 op=Get rec=4204772982792 ev=2: NpgsqlException: No suitable host was found.
- t-347#0 src=10434 seq=12 op=Update rec=44813688766466 ev=3: NpgsqlException: No suitable host was found.
- t-196#0 src=5693 seq=24 op=Update rec=24451248816133 ev=1: NpgsqlException: No suitable host was found.
- t-055#1 src=1942 seq=24 op=Update rec=8340826488837 ev=2: NpgsqlException: No suitable host was found.
- t-441#0 src=13889 seq=33 op=Get rec=59652800774148 ev=6: NpgsqlException: No suitable host was found.
- t-429#0 src=13405 seq=6 op=Update rec=57574036602881 ev=3: NpgsqlException: No suitable host was found.
- t-446#0 src=14041 seq=11 op=Update rec=60305635803139 ev=1: NpgsqlException: No suitable host was found.
- t-221#0 src=6481 seq=8 op=Update rec=27835683045378 ev=4: NpgsqlException: No suitable host was found.
- t-374#0 src=11515 seq=35 op=Update rec=49456548413445 ev=3: NpgsqlException: No suitable host was found.
- t-427#0 src=13355 seq=27 op=Update rec=57359288238083 ev=1: NpgsqlException: No suitable host was found.
- t-496#0 src=15881 seq=25 op=Update rec=68208375627777 ev=10: NpgsqlException: No suitable host was found.
- t-372#0 src=11406 seq=8 op=Create rec=48988396978178 ev=0: NpgsqlException: No suitable host was found.
- t-383#5 src=11853 seq=14 op=Create rec=50908247359492 ev=0: NpgsqlException: No suitable host was found.
- t-383#1 src=11857 seq=15 op=Update rec=50925427228676 ev=2: NpgsqlException: No suitable host was found.
- t-041#0 src=1295 seq=43 op=Update rec=5561982648327 ev=1: NpgsqlException: No suitable host was found.
- t-027#0 src=924 seq=8 op=Update rec=3968549781505 ev=4: NpgsqlException: No suitable host was found.
- t-041#2 src=1306 seq=43 op=Update rec=5609227288582 ev=3: NpgsqlException: No suitable host was found.
- t-369#0 src=11332 seq=30 op=Update rec=48670569398274 ev=13: NpgsqlException: No suitable host was found.
- t-481#0 src=15388 seq=38 op=Update rec=66090956750854 ev=1: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-343 | 63 | 1 | 38.725 | 39.187 | 518 | 0 | 1731 | 1279 | 0 | 56360.959 | 81788.927 | 171966.463 |
| t-265 | 75 | 1 | 30.187 | 37.639 | 250 | 0 | 1593 | 1255 | 0 | 57671.679 | 81264.639 | 135266.303 |
| t-456 | 21 | 1 | 143.85 | 48.988 | 512 | 0 | 1939 | 1404 | 0 | 58458.111 | 80740.351 | 133169.151 |
| t-362 | 47 | 1 | 47.764 | 36.599 | 3235 | 0 | 1545 | 1384 | 0 | 56885.247 | 79691.775 | 131071.999 |
| t-283 | 74 | 1 | 27.256 | 32.897 | 240 | 0 | 1439 | 1230 | 0 | 55836.671 | 78643.199 | 152043.519 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1996 | 1978 | 0 | 52428.799 | 77594.623 | 126353.407 |
| t-196 | 21 | 1 | 94.353 | 33.348 | 2244 | 0 | 1341 | 1310 | 0 | 53215.231 | 76546.047 | 127401.983 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3923 | 3898 | 0 | 51642.367 | 76546.047 | 120061.951 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 9033 | 7636 | 0 | 56098.815 | 76021.759 | 124780.543 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 2025 | 1812 | 0 | 57671.679 | 76021.759 | 163577.855 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 8018 | 7257 | 0 | 52953.087 | 76021.759 | 121634.815 |
| t-105 | 33 | 1 | 73.557 | 42.292 | 851 | 0 | 1700 | 1673 | 0 | 54525.951 | 75497.471 | 125829.119 |
| t-181 | 35 | 2 | 148.702 | 85.909 | 1367 | 0 | 3517 | 3475 | 0 | 45875.199 | 75497.471 | 127401.983 |
| t-253 | 26 | 1 | 73.629 | 32.505 | 1433 | 0 | 1324 | 1324 | 0 | 55312.383 | 75497.471 | 77070.335 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1903 | 1893 | 0 | 57147.391 | 75497.471 | 124256.255 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5533 | 5445 | 0 | 56098.815 | 75497.471 | 164626.431 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5958 | 5782 | 0 | 54788.095 | 74973.183 | 133693.439 |
| t-391 | 61 | 1 | 31.736 | 31.855 | 225 | 0 | 1402 | 1277 | 0 | 52953.087 | 74973.183 | 161480.703 |
| t-163 | 32 | 1 | 57.015 | 30.239 | 154 | 0 | 1221 | 1211 | 0 | 50069.503 | 73924.607 | 114294.783 |
| t-425 | 89 | 3 | 83.116 | 121.502 | 2911 | 0 | 5075 | 5011 | 0 | 50069.503 | 73924.607 | 162529.279 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3630 | 3585 | 0 | 51904.511 | 72876.031 | 135266.303 |
| t-282 | 23 | 1 | 84.429 | 32.491 | 956 | 0 | 1248 | 1248 | 0 | 53477.375 | 72351.743 | 73924.607 |
| t-029 | 28 | 2 | 143.274 | 66.438 | 1732 | 0 | 2758 | 2729 | 0 | 50331.647 | 71827.455 | 155189.247 |
| t-103 | 50 | 1 | 48.691 | 40.41 | 1025 | 0 | 1725 | 1712 | 0 | 55312.383 | 71827.455 | 127401.983 |
| t-182 | 89 | 3 | 81.941 | 119.217 | 2406 | 0 | 4823 | 4823 | 0 | 49020.927 | 71827.455 | 79167.487 |
