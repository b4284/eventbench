# H01-dc-kill-leader-dc-haproxy — FAIL ❌

- Broker: `Octopus dispatchers via MQTT 127.0.0.1:1883 ns=octopus, schema octopus; one event per publish call (4 producer / 2 worker connections), commit through the apply hook`
- Store: `postgres @ 127.0.0.1:6440/bench (pool max 200), unlogged=False, fillfactor=70, group commit (max 32/tx, 16 executors)`
- Ack mode: `AfterCommit`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 601 workers, offered 12856.021 ev/s
- Phases: run 60.271s, closing 2.398s, drain 17.707s (drained: true)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 497448 | 497448 | 6655.127 | 3788.108 | 20016.7 | 0 | 2029 | 2029 | 283 | 2508 | 63149 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 328416 | 32900.676 | 37421.055 | 50331.647 | 57475.071 | 59965.439 | 62980.095 |
| e2e_from_send | 328416 | 23986.865 | 17055.743 | 49446.911 | 56328.191 | 58949.631 | 61964.287 |
| producer_lag | 328630 | 8909.022 | 1289.215 | 25739.263 | 27049.983 | 27361.279 | 27656.191 |
| publish_call | 497448 | 221.789 | 122.879 | 250.239 | 620.543 | 21561.343 | 22216.703 |
| queue_wait | 328416 | 23861.776 | 16957.439 | 49381.375 | 56295.423 | 58916.863 | 61898.751 |
| broker_dwell | 328416 | 23657.705 | 16588.799 | 49283.071 | 56164.351 | 58818.559 | 61800.447 |
| receive_call | 445958 | 25.893 | 7.139 | 58.591 | 347.647 | 477.439 | 561.151 |
| ctx_load | 328416 | 9.675 | 6.039 | 20.479 | 60.607 | 104.639 | 207.231 |
| process | 328416 | 10.735 | 0.002 | 0.004 | 0.058 | 2625.535 | 3006.463 |
| ctx_commit | 328416 | 104.61 | 45.023 | 247.551 | 1065.983 | 1470.463 | 1520.639 |
| ack | 328407 | 0.009 | 0.004 | 0.007 | 0.042 | 0.729 | 53.919 |
| service | 328416 | 125.098 | 55.231 | 264.959 | 1193.983 | 2732.031 | 4126.719 |
| e2e_large | 183 | 10380.929 | 4247.551 | 28459.007 | 30425.087 | 32456.703 | 32456.703 |
| ready_wait | 328416 | 1292.592 | 26.159 | 1161.215 | 39911.423 | 48234.495 | 56786.943 |
| blocked | 328416 | 793.37 | 18.623 | 349.695 | 25378.815 | 31162.367 | 34603.007 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 3788.108 | 12870.4 |
| applied_total | 179165.571 | 263008 |
| bench_cpu_cores | 1.745 | 3.815 |
| bench_cpu_pct | 7.269 | 15.9 |
| bench_gc_gen0 | 1.184 | 3 |
| bench_gc_gen1 | 0.755 | 3 |
| bench_gc_gen2 | 0.327 | 1 |
| bench_gc_heap_mb | 1132.561 | 1530.1 |
| bench_mem_mb | 1765.537 | 1888.4 |
| bench_threadpool_queue | 19.102 | 352 |
| bench_threadpool_threads | 44.531 | 45 |
| broker_backlog | 144220.796 | 188535 |
| ctr_eb_dc1a_cpu_pct | 107.642 | 1041.16 |
| ctr_eb_dc1a_mem_mb | 3453.188 | 5005.8 |
| ctr_eb_dc1b_cpu_pct | 26.103 | 81.89 |
| ctr_eb_dc1b_mem_mb | 8668.308 | 8761.4 |
| ctr_eb_dc2a_cpu_pct | 714.356 | 846.25 |
| ctr_eb_dc2a_mem_mb | 8313.724 | 9030.3 |
| ctr_eb_dc2b_cpu_pct | 50.436 | 60.64 |
| ctr_eb_dc2b_mem_mb | 5369.686 | 6331.4 |
| ctr_eb_etcd_dc1_cpu_pct | 1.367 | 2.21 |
| ctr_eb_etcd_dc1_mem_mb | 38.849 | 41.2 |
| ctr_eb_etcd_dc2_cpu_pct | 1.625 | 2.04 |
| ctr_eb_etcd_dc2_mem_mb | 37.214 | 37.7 |
| ctr_eb_etcd_w_cpu_pct | 1.483 | 2.77 |
| ctr_eb_etcd_w_mem_mb | 58.894 | 61.4 |
| ctr_eb_haproxy_cpu_pct | 52.392 | 114.63 |
| ctr_eb_haproxy_mem_mb | 21.812 | 24.2 |
| ctr_eb_mosquitto_cpu_pct | 21.974 | 46.93 |
| ctr_eb_mosquitto_mem_mb | 6.696 | 25.4 |
| ctr_eb_pgbouncer_2_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_2_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_3_cpu_pct | 0.018 | 0.03 |
| ctr_eb_pgbouncer_3_mem_mb | 7.831 | 8.3 |
| ctr_eb_pgbouncer_4_cpu_pct | 0.02 | 0.07 |
| ctr_eb_pgbouncer_4_mem_mb | 7.8 | 7.8 |
| ctr_eb_pgbouncer_cpu_pct | 0.019 | 0.04 |
| ctr_eb_pgbouncer_mem_mb | 9 | 9 |
| ctr_eb_postgres_cpu_pct | 0.727 | 2.2 |
| ctr_eb_postgres_mem_mb | 1857.8 | 1857.8 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 41.416 | 1769.2 |
| host_cpu_pct | 38.216 | 80.25 |
| in_flight | 147653.776 | 196616 |
| nacks_per_s | 41.416 | 1769.2 |
| octopus_dispatcher_cores | 0.761 | 1.49 |
| octopus_dispatchers_alive | 3 | 3 |
| octopus_items_sent_total | 525676.061 | 742307 |
| octopus_missing_reports_total | 0 | 0 |
| octopus_mqtt_received_total | 151503.898 | 212628 |
| octopus_mqtt_sent_total | 117879.694 | 172704 |
| octopus_publish_retries_total | 9399.571 | 23465 |
| pg_active_acks | 0 | 0 |
| pg_active_commits | 15.306 | 18 |
| pg_active_loads | 3.347 | 41 |
| pg_active_polls | 0 | 0 |
| pg_active_sends | 0 | 0 |
| pg_cache_hit_pct | 100 | 100 |
| pg_conn_active | 56.918 | 71 |
| pg_conn_idle | 210.857 | 244 |
| pg_conn_idle_in_tx | 0 | 0 |
| pg_conn_lock_wait | 0.939 | 15 |
| pg_conn_total | 267.776 | 268 |
| pg_db_size_mb | 1432.831 | 1583.9 |
| pg_deadlocks | 0 | 0 |
| pg_replica_lag_mb | 18.592 | 48 |
| pg_replica_replay_lag_ms | 171.865 | 427.9 |
| pg_replicas | 3 | 3 |
| pg_rollbacks_per_s | 0 | 0 |
| pg_temp_mb_per_s | 0 | 0 |
| pg_tps | 10552.769 | 19602.6 |
| pg_tup_del_per_s | 17046.122 | 33890.2 |
| pg_tup_fetch_per_s | 485044.394 | 550670.7 |
| pg_tup_ins_per_s | 31124.843 | 57364.1 |
| pg_tup_upd_per_s | 10861.427 | 23066.8 |
| pg_wal_mb_per_s | 67.456 | 103.42 |
| pgb_cl_active | 0 | 0 |
| pgb_cl_waiting | 0 | 0 |
| pgb_maxwait_ms | 0 | 0 |
| pgb_sv_active | 0 | 0 |
| pgb_sv_idle | 0 | 0 |
| pgb_sv_used | 0 | 0 |
| produced_per_s | 6655.127 | 20194.4 |
| produced_total | 326819.347 | 459624 |
| publish_mb_per_s | 24.283 | 76.01 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 80,115 records are Completed |
| record_counts | pass | 80,115 records found, 80,115 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 283 uploads verified, 0 sources mismatched |
| applied_equals_generated | pass | 497,448 events applied by workers, 497,448 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | pass | none observed |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 63,149 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 25379 ms, max 34603 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7307 seq 10: ready 248 ms before start, 248 ms of it with an idle slot in group 250
- [cross_source_blocking] source 1099 seq 5: ready 442 ms before start, 114 ms of it with an idle slot in group 35
- [cross_source_blocking] source 8603 seq 8: ready 141 ms before start, 141 ms of it with an idle slot in group 296
- [cross_source_blocking] source 15374 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 480
- [cross_source_blocking] source 9294 seq 15: ready 142 ms before start, 142 ms of it with an idle slot in group 320
- [cross_source_blocking] source 2979 seq 5: ready 132 ms before start, 132 ms of it with an idle slot in group 98
- [cross_source_blocking] source 8235 seq 17: ready 145 ms before start, 145 ms of it with an idle slot in group 278
- [cross_source_blocking] source 3406 seq 13: ready 142 ms before start, 142 ms of it with an idle slot in group 117
- [cross_source_blocking] source 5884 seq 4: ready 144 ms before start, 135 ms of it with an idle slot in group 204
- [cross_source_blocking] source 13731 seq 7: ready 131 ms before start, 131 ms of it with an idle slot in group 437
- [cross_source_blocking] source 12366 seq 7: ready 140 ms before start, 140 ms of it with an idle slot in group 392
- [cross_source_blocking] source 2181 seq 7: ready 153 ms before start, 153 ms of it with an idle slot in group 62
- [cross_source_blocking] source 590 seq 4: ready 142 ms before start, 142 ms of it with an idle slot in group 17
- [cross_source_blocking] source 1347 seq 7: ready 134 ms before start, 134 ms of it with an idle slot in group 43
- [cross_source_blocking] source 12043 seq 6: ready 144 ms before start, 144 ms of it with an idle slot in group 385
- [cross_source_blocking] source 15758 seq 17: ready 146 ms before start, 146 ms of it with an idle slot in group 493
- [cross_source_blocking] source 9198 seq 6: ready 143 ms before start, 143 ms of it with an idle slot in group 316
- [cross_source_blocking] source 5212 seq 8: ready 145 ms before start, 145 ms of it with an idle slot in group 180
- [cross_source_blocking] source 15260 seq 10: ready 146 ms before start, 146 ms of it with an idle slot in group 477
- [cross_source_blocking] source 11275 seq 5: ready 117 ms before start, 117 ms of it with an idle slot in group 367

## Error samples

- t-223#0 src=6632 seq=33 op=Update rec=28484223107073 ev=11: NpgsqlException: Exception while reading from stream
- t-047#0 src=1607 seq=8 op=Get rec=6902012444675 ev=1: NpgsqlException: Exception while reading from stream
- t-048#0 src=1618 seq=12 op=Update rec=6949257084930 ev=6: NpgsqlException: Exception while reading from stream
- t-015#0 src=453 seq=25 op=Update rec=1945620185093 ev=3: NpgsqlException: Exception while reading from stream
- t-347#3 src=10471 seq=11 op=Get rec=44972602556420 ev=4: NpgsqlException: Exception while reading from stream
- t-014#0 src=424 seq=12 op=Update rec=1821066133507 ev=1: NpgsqlException: Exception while reading from stream
- t-432#0 src=13614 seq=23 op=Update rec=58471684767748 ev=5: NpgsqlException: Exception while reading from stream
- t-004#0 src=190 seq=23 op=Update rec=816043786242 ev=12: NpgsqlException: Exception while reading from stream
- t-019#0 src=641 seq=23 op=Update rec=2753074036738 ev=7: NpgsqlException: Exception while reading from stream
- t-286#0 src=8449 seq=5 op=Update rec=36288178683905 ev=3: NpgsqlException: Exception while reading from stream
- t-020#0 src=675 seq=18 op=Complete rec=2899102924803 ev=2: NpgsqlException: Exception while reading from stream
- t-313#0 src=9073 seq=14 op=Get rec=38968238276610 ev=6: NpgsqlException: Exception while reading from stream
- t-332#0 src=9564 seq=12 op=Update rec=41077067218949 ev=1: NpgsqlException: Exception while reading from stream
- t-286#0 src=8447 seq=19 op=Complete rec=36279588749313 ev=12: NpgsqlException: Exception while reading from stream
- t-222#0 src=6518 seq=10 op=Update rec=27994596835329 ev=6: NpgsqlException: Exception while reading from stream
- t-018#0 src=613 seq=18 op=Complete rec=2632814952452 ev=5: NpgsqlException: Exception while reading from stream
- t-022#0 src=729 seq=7 op=Get rec=3131031158785 ev=4: NpgsqlException: Exception while reading from stream
- t-088#0 src=2809 seq=8 op=Update rec=12064563134465 ev=4: NpgsqlException: Exception while reading from stream
- t-282#0 src=8290 seq=20 op=Update rec=35605278883843 ev=8: NpgsqlException: Exception while reading from stream
- t-350#0 src=10660 seq=11 op=Update rec=45784351375361 ev=4: NpgsqlException: Exception while reading from stream

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-075 | 37 | 1 | 75.485 | 46.624 | 232 | 0 | 1777 | 1777 | 0 | 45088.767 | 61603.839 | 63176.703 |
| t-245 | 43 | 1 | 71.24 | 48.932 | 2232 | 0 | 1844 | 1844 | 0 | 43778.047 | 61079.551 | 62128.127 |
| t-136 | 146 | 2 | 40.495 | 97.61 | 450 | 0 | 3893 | 3893 | 0 | 45088.767 | 60555.263 | 62390.271 |
| t-461 | 130 | 2 | 45.032 | 97.963 | 3025 | 0 | 3885 | 3885 | 0 | 44302.335 | 60555.263 | 61865.983 |
| t-206 | 118 | 2 | 48.629 | 93.697 | 1969 | 0 | 3680 | 3680 | 0 | 44826.623 | 60030.975 | 61865.983 |
| t-222 | 138 | 3 | 61.993 | 144.282 | 2846 | 0 | 5499 | 5499 | 0 | 44826.623 | 60030.975 | 61865.983 |
| t-263 | 93 | 2 | 59.766 | 94.005 | 374 | 0 | 3626 | 3626 | 0 | 43778.047 | 60030.975 | 62128.127 |
| t-496 | 81 | 4 | 145.619 | 197.957 | 278 | 0 | 7404 | 7404 | 0 | 43778.047 | 60030.975 | 61603.839 |
| t-005 | 47 | 1 | 60.561 | 47.065 | 2385 | 0 | 1779 | 1779 | 0 | 44040.191 | 59768.831 | 61079.551 |
| t-126 | 93 | 1 | 27.815 | 43.54 | 816 | 0 | 1760 | 1760 | 0 | 43778.047 | 59506.687 | 60030.975 |
| t-237 | 74 | 1 | 36.026 | 45.137 | 2944 | 0 | 1803 | 1803 | 0 | 45088.767 | 59506.687 | 60555.263 |
| t-315 | 88 | 2 | 62.869 | 92.077 | 2927 | 0 | 3451 | 3451 | 0 | 43778.047 | 59506.687 | 61341.695 |
| t-483 | 200 | 3 | 41.15 | 138.556 | 2470 | 0 | 5280 | 5280 | 0 | 44040.191 | 59506.687 | 60817.407 |
| t-131 | 140 | 3 | 60.845 | 143.347 | 961 | 0 | 5351 | 5351 | 0 | 44040.191 | 59244.543 | 60293.119 |
| t-264 | 46 | 1 | 60.311 | 46.436 | 3058 | 0 | 1770 | 1770 | 0 | 43778.047 | 59244.543 | 60030.975 |
| t-443 | 49 | 1 | 57.19 | 47.514 | 261 | 0 | 1831 | 1831 | 0 | 43778.047 | 59244.543 | 59768.831 |
| t-052 | 62 | 1 | 44.036 | 46.205 | 1837 | 0 | 1795 | 1795 | 0 | 44040.191 | 58982.399 | 60030.975 |
| t-178 | 90 | 1 | 27.367 | 41.476 | 131 | 0 | 1653 | 1653 | 0 | 44040.191 | 58982.399 | 59506.687 |
| t-379 | 57 | 1 | 47.528 | 44.975 | 591 | 0 | 1772 | 1772 | 0 | 43778.047 | 58982.399 | 60293.119 |
| t-397 | 200 | 1 | 12.561 | 41.495 | 426 | 0 | 1903 | 1903 | 0 | 45088.767 | 58982.399 | 59768.831 |
| t-147 | 79 | 4 | 145.536 | 195.43 | 1078 | 0 | 7260 | 7260 | 0 | 44040.191 | 58720.255 | 60030.975 |
| t-161 | 41 | 1 | 60.994 | 41.321 | 339 | 0 | 1669 | 1669 | 0 | 43778.047 | 58720.255 | 59768.831 |
| t-246 | 32 | 2 | 187.722 | 99.182 | 494 | 0 | 3638 | 3638 | 0 | 44826.623 | 58720.255 | 60030.975 |
| t-325 | 98 | 1 | 26.904 | 43.432 | 3394 | 0 | 1778 | 1778 | 0 | 43778.047 | 58720.255 | 59506.687 |
| t-348 | 125 | 3 | 63.004 | 132.054 | 1340 | 0 | 5024 | 5024 | 0 | 43253.759 | 58720.255 | 59506.687 |
