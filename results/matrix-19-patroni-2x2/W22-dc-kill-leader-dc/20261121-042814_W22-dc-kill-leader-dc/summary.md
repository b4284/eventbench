# W22-dc-kill-leader-dc — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:5441,127.0.0.1:5442,127.0.0.1:5443,127.0.0.1:5444:5432/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.483s, closing 1.557s, drain 120.033s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 565563 | 555026 | 8012.259 | 3892.824 | 23198.2 | 0 | 3474 | 3474 | 309 | 2879 | 100595 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 387218 | 32251.886 | 37748.735 | 55574.527 | 66322.431 | 76218.367 | 166854.655 |
| e2e_from_send | 387218 | 26445.2 | 20938.751 | 55017.471 | 65896.447 | 75366.399 | 165806.079 |
| producer_lag | 395938 | 5989.371 | 379.647 | 20971.519 | 22249.471 | 22396.927 | 22773.759 |
| publish_call | 565563 | 180.01 | 88.319 | 199.167 | 453.375 | 19677.183 | 22544.383 |
| queue_wait | 387218 | 26272.323 | 20889.599 | 54919.167 | 65568.767 | 74186.751 | 105775.103 |
| broker_dwell | 387218 | 26169.159 | 20742.143 | 54820.863 | 65470.463 | 74121.215 | 105709.567 |
| receive_call | 514169 | 23.467 | 4.703 | 53.727 | 329.471 | 473.343 | 559.103 |
| ctx_load | 387218 | 75.707 | 2.129 | 10.063 | 48.063 | 15024.127 | 30015.487 |
| process | 387218 | 10.792 | 0.002 | 0.004 | 0.033 | 2635.775 | 3002.367 |
| ctx_commit | 387218 | 86.333 | 20.575 | 207.871 | 391.935 | 579.071 | 58720.255 |
| ack | 387180 | 0.007 | 0.004 | 0.007 | 0.031 | 0.34 | 60.607 |
| service | 387218 | 172.891 | 25.455 | 227.967 | 591.871 | 15065.087 | 88145.919 |
| e2e_large | 207 | 8225.245 | 3588.095 | 23822.335 | 27901.951 | 30687.231 | 30687.231 |
| ready_wait | 387218 | 1386.332 | 16.295 | 1571.839 | 43122.687 | 54624.255 | 91684.863 |
| blocked | 387218 | 779.996 | 11.303 | 516.863 | 26230.783 | 30425.087 | 33914.879 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3892.824 | 11009.1 |
| applied_total | 206856.49 | 268842 |
| bench_cpu_cores | 2.026 | 4.716 |
| bench_cpu_pct | 8.44 | 19.65 |
| bench_gc_gen0 | 1.184 | 4 |
| bench_gc_gen1 | 0.592 | 2 |
| bench_gc_gen2 | 0.327 | 2 |
| bench_gc_heap_mb | 1122.133 | 1502.2 |
| bench_mem_mb | 1805.978 | 1943.7 |
| bench_threadpool_queue | 23.102 | 268 |
| bench_threadpool_threads | 43.816 | 44 |
| broker_backlog | 150933.367 | 174039 |
| ctr_eb_dc1a_cpu_pct | 45.651 | 55.77 |
| ctr_eb_dc1a_mem_mb | 3075.955 | 3444.7 |
| ctr_eb_dc1b_cpu_pct | 742.774 | 794.12 |
| ctr_eb_dc1b_mem_mb | 10642.127 | 11110.3 |
| ctr_eb_dc2a_cpu_pct | 66.833 | 315.32 |
| ctr_eb_dc2a_mem_mb | 4099.224 | 4147.5 |
| ctr_eb_dc2b_cpu_pct | 115.166 | 612.66 |
| ctr_eb_dc2b_mem_mb | 6847.767 | 7714.3 |
| ctr_eb_etcd_dc1_cpu_pct | 1.687 | 1.9 |
| ctr_eb_etcd_dc1_mem_mb | 46 | 46.6 |
| ctr_eb_etcd_dc2_cpu_pct | 1.432 | 2.13 |
| ctr_eb_etcd_dc2_mem_mb | 25.847 | 28.6 |
| ctr_eb_etcd_w_cpu_pct | 1.498 | 2.18 |
| ctr_eb_etcd_w_mem_mb | 57.963 | 59.2 |
| ctr_eb_mosquitto_cpu_pct | 23.653 | 41.93 |
| ctr_eb_mosquitto_mem_mb | 7.471 | 25.2 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.015 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.816 | 8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_3_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.02 | 0.06 |
| ctr_eb_pgbouncer_mem_mb | 9.1 | 9.1 |
| ctr_eb_postgres_cpu_pct | 0.775 | 1.89 |
| ctr_eb_postgres_mem_mb | 1901.837 | 1904.5 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 71.437 | 1958.7 |
| host_cpu_pct | 40.29 | 88.32 |
| in_flight | 161966.327 | 259623 |
| nacks_per_s | 71.437 | 1958.7 |
| octopus_dispatcher_cores | 1.166 | 1.57 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 584794.531 | 718005 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 158876.265 | 193707 |
| octopus_mqtt_sent_total | 128857 | 164182 |
| octopus_publish_retries_total | 6606.837 | 16742 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 16.408 | 18 |
| pg_active_loads | 0.469 | 8 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 69.878 | 74 |
| pg_conn_idle | 198.306 | 224 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.878 | 13 |
| pg_conn_total | 268.184 | 269 |
| pg_db_size_mb | 1508.288 | 1689.8 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 7.851 | 14.95 |
| pg_replica_replay_lag_ms | 69.994 | 172 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 19376.084 | 21882.9 |
| pg_tup_del_per_s | 27837.047 | 33977 |
| pg_tup_fetch_per_s | 610002.631 | 707743.5 |
| pg_tup_ins_per_s | 47958.48 | 62677 |
| pg_tup_upd_per_s | 21442.41 | 24000.3 |
| pg_wal_mb_per_s | 84.181 | 111.45 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 8012.259 | 29968.8 |
| produced_total | 368822.816 | 528465 |
| publish_mb_per_s | 28.527 | 90.44 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | **FAIL** | 14988/16000 source cursors at the expected last sequence |
| records_complete | **FAIL** | 2,216 records not Completed |
| record_counts | **FAIL** | 87,432 records found, 88,353 expected; 1012 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 309 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 555,026 events applied by workers, 565,563 generated |
| broker_drained | **FAIL** | 10,537 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 100,595 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 26231 ms, max 33899 ms) |

Sample mismatches:

- source 10368: record 44530220924929 left in status Open (version 13)
- source 9151: record 39303245725697 left in status Open (version 8)
- source 13059: record 56087977918465 left in status Open (version 8)
- source 10448: record 44873818308609 left in status Open (version 14)
- source 660: record 2834678415361 left in status Open (version 7)
- source 5166: record 22187801051137 left in status Open (version 4)
- source 13965: record 59979218288641 left in status Open (version 7)
- source 5084: record 21835613732865 left in status Open (version 2)
- source 13085: record 56199647068161 left in status Open (version 11)
- source 4763: record 20456929230849 left in status Open (version 19)
- source 3629: record 15586436317189 left in status Open (version 2)
- source 14262: record 61254823575553 left in status Open (version 3)
- source 11849: record 50891067490305 left in status Open (version 19)
- source 5097: record 21891448307713 left in status Open (version 5)
- source 5103: record 21917218111489 left in status Open (version 5)
- source 13079: record 56173877264385 left in status Open (version 7)
- source 11705: record 50272592199682 left in status Open (version 5)
- source 10263: record 44079249358849 left in status Open (version 8)
- source 11948: record 51316269252609 left in status Open (version 10)
- source 11687: record 50195282788353 left in status Open (version 6)

## Cross-source blocking samples

- [cross_source_blocking] source 404 seq 12: ready 165 ms before start, 165 ms of it with an idle slot in group 12
- [cross_source_blocking] source 14213 seq 5: ready 200 ms before start, 100 ms of it with an idle slot in group 455
- [cross_source_blocking] source 15357 seq 5: ready 325 ms before start, 102 ms of it with an idle slot in group 480
- [cross_source_blocking] source 5568 seq 2: ready 121 ms before start, 121 ms of it with an idle slot in group 192
- [cross_source_blocking] source 1880 seq 7: ready 217 ms before start, 116 ms of it with an idle slot in group 54
- [cross_source_blocking] source 7041 seq 16: ready 281 ms before start, 108 ms of it with an idle slot in group 238
- [cross_source_blocking] source 10629 seq 6: ready 193 ms before start, 139 ms of it with an idle slot in group 349
- [cross_source_blocking] source 7292 seq 4: ready 292 ms before start, 130 ms of it with an idle slot in group 250
- [cross_source_blocking] source 7036 seq 8: ready 225 ms before start, 108 ms of it with an idle slot in group 238
- [cross_source_blocking] source 12345 seq 6: ready 272 ms before start, 119 ms of it with an idle slot in group 392
- [cross_source_blocking] source 1341 seq 4: ready 721 ms before start, 111 ms of it with an idle slot in group 42
- [cross_source_blocking] source 13608 seq 8: ready 142 ms before start, 142 ms of it with an idle slot in group 431
- [cross_source_blocking] source 3331 seq 5: ready 131 ms before start, 131 ms of it with an idle slot in group 113
- [cross_source_blocking] source 14188 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 452
- [cross_source_blocking] source 12341 seq 6: ready 283 ms before start, 119 ms of it with an idle slot in group 392
- [cross_source_blocking] source 10620 seq 9: ready 201 ms before start, 142 ms of it with an idle slot in group 349
- [cross_source_blocking] source 14205 seq 17: ready 194 ms before start, 140 ms of it with an idle slot in group 454
- [cross_source_blocking] source 9204 seq 9: ready 143 ms before start, 143 ms of it with an idle slot in group 316
- [cross_source_blocking] source 2444 seq 9: ready 228 ms before start, 133 ms of it with an idle slot in group 72
- [cross_source_blocking] source 7112 seq 2: ready 143 ms before start, 143 ms of it with an idle slot in group 241

## Error samples

- t-119L#3 src=3474 seq=3 op=Update rec=14920716386305 ev=1: NpgsqlException: No suitable host was found.
- t-119L#2 src=3436 seq=4 op=Update rec=14757507629057 ev=2: NpgsqlException: Exception while reading from stream
- t-212#0 src=6217 seq=21 op=Update rec=26701811679236 ev=3: NpgsqlException: No suitable host was found.
- t-419#0 src=13180 seq=8 op=Get rec=56607668961283 ev=1: NpgsqlException: No suitable host was found.
- t-348#1 src=10542 seq=16 op=Update rec=45277545234434 ev=5: NpgsqlException: No suitable host was found.
- t-084#0 src=2755 seq=24 op=Create rec=11832634900486 ev=0: NpgsqlException: No suitable host was found.
- t-235#1 src=6922 seq=20 op=Get rec=29729763622916 ev=1: NpgsqlException: No suitable host was found.
- t-050#0 src=1706 seq=8 op=Get rec=7327214206977 ev=7: NpgsqlException: No suitable host was found.
- t-218#0 src=6370 seq=19 op=Complete rec=27358941675523 ev=7: NpgsqlException: No suitable host was found.
- t-305#0 src=8764 seq=24 op=Update rec=37641093382149 ev=2: NpgsqlException: No suitable host was found.
- t-322#0 src=9332 seq=20 op=Get rec=40080634806275 ev=1: NpgsqlException: No suitable host was found.
- t-186#0 src=5447 seq=45 op=Get rec=23394686861319 ev=2: NpgsqlException: No suitable host was found.
- t-467#0 src=14895 seq=11 op=Update rec=63973537873921 ev=5: NpgsqlException: No suitable host was found.
- t-161#0 src=4604 seq=17 op=Update rec=19774029430786 ev=1: NpgsqlException: No suitable host was found.
- t-257#0 src=7417 seq=45 op=Get rec=31855772434439 ev=1: NpgsqlException: No suitable host was found.
- t-417#0 src=13162 seq=14 op=Update rec=56530359549955 ev=2: NpgsqlException: No suitable host was found.
- t-342#0 src=10037 seq=36 op=Get rec=43108586749955 ev=15: NpgsqlException: No suitable host was found.
- t-182#0 src=5272 seq=24 op=Get rec=22643067584517 ev=3: NpgsqlException: No suitable host was found.
- t-119L#3 src=3439 seq=5 op=Update rec=14770392530946 ev=1: NpgsqlException: Exception while reading from stream
- t-157#0 src=4508 seq=20 op=Update rec=19361712570371 ev=6: NpgsqlException: No suitable host was found.

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1863 | 1346 | 0 | 52690.943 | 79691.775 | 147849.215 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 2100 | 1532 | 0 | 56623.103 | 79167.487 | 167772.159 |
| t-416 | 57 | 1 | 38.884 | 37.248 | 3430 | 0 | 1672 | 1223 | 0 | 51904.511 | 78118.911 | 125829.119 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1997 | 1477 | 0 | 56885.247 | 77070.335 | 164626.431 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3922 | 3168 | 0 | 52166.655 | 76546.047 | 165675.007 |
| t-458 | 132 | 1 | 15.687 | 34.029 | 2384 | 0 | 1656 | 1239 | 0 | 53215.231 | 76021.759 | 132120.575 |
| t-189 | 59 | 1 | 44.753 | 43.324 | 1601 | 0 | 1914 | 1691 | 0 | 55312.383 | 74973.183 | 161480.703 |
| t-122 | 33 | 1 | 66.424 | 35.794 | 550 | 0 | 1537 | 1378 | 0 | 51118.079 | 73924.607 | 130023.423 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 2016 | 1998 | 0 | 52953.087 | 73400.319 | 152043.519 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 2031 | 2031 | 0 | 52166.655 | 72351.743 | 73924.607 |
| t-490 | 56 | 2 | 93.882 | 88.229 | 245 | 0 | 3809 | 3809 | 0 | 49020.927 | 72351.743 | 79167.487 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 2033 | 2016 | 0 | 54263.807 | 71827.455 | 158334.975 |
| t-260 | 30 | 1 | 83.345 | 43.145 | 499 | 0 | 1821 | 1821 | 0 | 45613.055 | 71827.455 | 78643.199 |
| t-344 | 195 | 6 | 81.919 | 269.578 | 571 | 0 | 11602 | 8763 | 0 | 53215.231 | 71303.167 | 147849.215 |
| t-469 | 105 | 3 | 76.658 | 133.383 | 278 | 0 | 5776 | 5671 | 0 | 54525.951 | 70778.879 | 144703.487 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 4098 | 4088 | 0 | 53477.375 | 70254.591 | 147849.215 |
| t-182 | 89 | 3 | 81.941 | 119.217 | 2406 | 0 | 5062 | 5062 | 0 | 46661.631 | 69730.303 | 78643.199 |
| t-438 | 121 | 4 | 74.684 | 156.372 | 1839 | 0 | 6603 | 6577 | 0 | 47972.351 | 69730.303 | 122683.391 |
| t-084 | 23 | 1 | 81.613 | 29.605 | 204 | 0 | 1271 | 1271 | 0 | 49020.927 | 69206.015 | 70778.879 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1993 | 1966 | 0 | 52428.799 | 69206.015 | 136314.879 |
| t-165 | 36 | 1 | 55.043 | 32.844 | 373 | 0 | 1425 | 1419 | 0 | 51118.079 | 69206.015 | 114294.783 |
| t-065 | 80 | 5 | 169.666 | 223.962 | 263 | 0 | 9494 | 9352 | 0 | 44564.479 | 68681.727 | 153092.095 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1885 | 1885 | 0 | 51380.223 | 67633.151 | 75497.471 |
| t-197 | 42 | 2 | 103.582 | 70.552 | 725 | 0 | 3030 | 3025 | 0 | 46661.631 | 67633.151 | 110624.767 |
| t-342 | 15 | 1 | 139.58 | 34.905 | 146 | 0 | 1474 | 1474 | 0 | 49807.359 | 67633.151 | 69730.303 |
