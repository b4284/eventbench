# C2d-redis3-wait1-KILL-PRIMARY — FAIL ❌

- Broker: `redis streams @ 127.0.0.1:26380,127.0.0.1:26381,127.0.0.1:26382,serviceName=ebmaster, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False, wait replicas=1`
- Store: `redis @ 127.0.0.1:26380,127.0.0.1:26381,127.0.0.1:26382,serviceName=ebmaster, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False, wait replicas=1`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 152.464s, closing 1.574s, drain 600.092s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 786613 | 785639 | 3493.962 | 220.396 | 19404.8 | 5980 | 7103 | 6228 | 264 | 3922 | 39282 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 503163 | 159628.439 | 161742.847 | 168034.303 | 174456.831 | 182583.295 | 188743.679 |
| e2e_from_send | 503163 | 159592.234 | 161611.775 | 168034.303 | 174456.831 | 182583.295 | 188612.607 |
| producer_lag | 503239 | 36.24 | 27.695 | 77.951 | 152.447 | 228.223 | 296.447 |
| publish_call | 26468 | 175.404 | 28.079 | 80.831 | 152.447 | 108462.079 | 121569.279 |
| queue_wait | 503163 | 158899.324 | 161087.487 | 167510.015 | 173670.399 | 182321.151 | 186515.455 |
| broker_dwell | 503163 | 158543.878 | 161087.487 | 167510.015 | 173670.399 | 182321.151 | 186515.455 |
| receive_call | 786251 | 17.423 | 0.338 | 0.982 | 42.207 | 7901.183 | 15056.895 |
| ctx_load | 503163 | 37.168 | 3.291 | 118.143 | 157.951 | 807.935 | 13942.783 |
| process | 503163 | 10.759 | 0.001 | 0.002 | 0.006 | 2609.151 | 13058.047 |
| ctx_commit | 503163 | 71.751 | 6.151 | 236.031 | 307.967 | 1404.927 | 14172.159 |
| service | 503163 | 692.898 | 10.127 | 905.727 | 13729.791 | 29409.279 | 140902.399 |
| e2e_large | 156 | 68264.481 | 13205.503 | 132579.327 | 140771.327 | 143261.695 | 143261.695 |
| ready_wait | 503163 | 612.201 | 141.311 | 1422.335 | 4657.151 | 15040.511 | 163184.639 |
| blocked | 503163 | 73.284 | 6.699 | 77.247 | 736.767 | 6819.839 | 121569.279 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 220.396 | 2320.8 |
| applied_total | 60399.486 | 62775 |
| bench_cpu_cores | 0.237 | 2.048 |
| bench_cpu_pct | 0.986 | 8.53 |
| bench_gc_gen0 | 0.203 | 2 |
| bench_gc_gen1 | 0.109 | 2 |
| bench_gc_gen2 | 0.051 | 1 |
| bench_gc_heap_mb | 1001.061 | 1145 |
| bench_mem_mb | 1286.567 | 1319.8 |
| bench_threadpool_queue | 3694.261 | 7054 |
| bench_threadpool_threads | 101.022 | 211 |
| broker_backlog | 639224.159 | 687339 |
| ctr_eb_redis_6380_cpu_pct | 180.369 | 233.05 |
| ctr_eb_redis_6380_mem_mb | 2259.059 | 2714.2 |
| ctr_eb_redis_6381_cpu_pct | 28.957 | 125.77 |
| ctr_eb_redis_6381_mem_mb | 2639.796 | 2784.7 |
| ctr_eb_redis_6382_cpu_pct | 31.429 | 111.49 |
| ctr_eb_redis_6382_mem_mb | 2648.449 | 2792.4 |
| ctr_eb_sentinel_26380_cpu_pct | 0.2 | 0.41 |
| ctr_eb_sentinel_26380_mem_mb | 4.556 | 4.8 |
| ctr_eb_sentinel_26381_cpu_pct | 0.182 | 0.39 |
| ctr_eb_sentinel_26381_mem_mb | 4.507 | 4.6 |
| ctr_eb_sentinel_26382_cpu_pct | 0.181 | 0.41 |
| ctr_eb_sentinel_26382_mem_mb | 4.435 | 4.5 |
| duplicates_per_s | 7.106 | 613.6 |
| errors_per_s | 24.509 | 953.1 |
| host_cpu_pct | 3.377 | 26.84 |
| in_flight | 653731.928 | 687364 |
| nacks_per_s | 20.691 | 922.1 |
| produced_per_s | 3493.962 | 26005.2 |
| produced_total | 714131.413 | 750139 |
| publish_mb_per_s | 9.848 | 89.86 |
| redis_aof_buffer_length | 859613.065 | 14982768 |
| redis_aof_current_size | 2140.995 | 2260.4 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.022 | 1 |
| redis_blocked_clients | 6.319 | 11 |
| redis_connected_clients | 38 | 38 |
| redis_instantaneous_ops_per_sec | 43434.37 | 61954 |
| redis_total_commands_processed_per_s | 43729.774 | 58897.2 |
| redis_total_net_input_bytes_per_s | 80081720.543 | 104330737.8 |
| redis_total_net_output_bytes_per_s | 191504584.233 | 238924735.3 |
| redis_used_cpu_sys_cores | 0.773 | 0.97 |
| redis_used_cpu_sys_main_thread_cores | 0.139 | 0.25 |
| redis_used_cpu_user_cores | 0.656 | 0.82 |
| redis_used_cpu_user_main_thread_cores | 0.225 | 0.27 |
| redis_used_memory | 2499.707 | 2716.3 |
| redisq_aof_buffer_length | 1652100.891 | 14785426 |
| redisq_aof_current_size | 2269.657 | 2384.6 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.022 | 1 |
| redisq_blocked_clients | 10.217 | 12 |
| redisq_connected_clients | 38 | 38 |
| redisq_instantaneous_ops_per_sec | 41962.116 | 68506 |
| redisq_total_commands_processed_per_s | 44763.41 | 64827.7 |
| redisq_total_net_input_bytes_per_s | 82181080.919 | 109833196.2 |
| redisq_total_net_output_bytes_per_s | 171803955.384 | 240095915.6 |
| redisq_used_cpu_sys_cores | 0.901 | 0.95 |
| redisq_used_cpu_sys_main_thread_cores | 0.154 | 0.27 |
| redisq_used_cpu_user_cores | 0.751 | 0.83 |
| redisq_used_cpu_user_main_thread_cores | 0.208 | 0.3 |
| redisq_used_memory | 2598.854 | 2691.6 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 114,383 records are Completed |
| record_counts | pass | 114,383 records found, 114,383 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 264 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 785,639 events applied by workers, 786,613 generated |
| broker_drained | pass | 0 messages left in the broker |
| runtime_ordering_gap | **FAIL** | 12 observed by workers |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 39,282 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 737 ms, max 121515 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 9730 seq 6: ready 1124 ms before start, 194 ms of it with an idle slot in group 338
- [cross_source_blocking] source 2845 seq 7: ready 1719 ms before start, 143 ms of it with an idle slot in group 89
- [cross_source_blocking] source 3998 seq 6: ready 1144 ms before start, 118 ms of it with an idle slot in group 133
- [cross_source_blocking] source 8933 seq 1: ready 104 ms before start, 104 ms of it with an idle slot in group 308
- [cross_source_blocking] source 2843 seq 5: ready 3310 ms before start, 230 ms of it with an idle slot in group 89
- [cross_source_blocking] source 5866 seq 9: ready 485 ms before start, 485 ms of it with an idle slot in group 203
- [cross_source_blocking] source 9728 seq 8: ready 2668 ms before start, 206 ms of it with an idle slot in group 338
- [cross_source_blocking] source 14188 seq 7: ready 2294 ms before start, 239 ms of it with an idle slot in group 452
- [cross_source_blocking] source 13603 seq 6: ready 254 ms before start, 136 ms of it with an idle slot in group 430
- [cross_source_blocking] source 12816 seq 9: ready 3542 ms before start, 111 ms of it with an idle slot in group 405
- [cross_source_blocking] source 13883 seq 7: ready 4318 ms before start, 218 ms of it with an idle slot in group 440
- [cross_source_blocking] source 13921 seq 4: ready 184 ms before start, 184 ms of it with an idle slot in group 442
- [cross_source_blocking] source 13923 seq 10: ready 5102 ms before start, 300 ms of it with an idle slot in group 442
- [cross_source_blocking] source 6357 seq 8: ready 4022 ms before start, 228 ms of it with an idle slot in group 217
- [cross_source_blocking] source 9731 seq 7: ready 4481 ms before start, 236 ms of it with an idle slot in group 338
- [cross_source_blocking] source 12959 seq 6: ready 2731 ms before start, 136 ms of it with an idle slot in group 410
- [cross_source_blocking] source 14189 seq 11: ready 1822 ms before start, 195 ms of it with an idle slot in group 452
- [cross_source_blocking] source 13619 seq 10: ready 365 ms before start, 131 ms of it with an idle slot in group 433
- [cross_source_blocking] source 13621 seq 8: ready 2501 ms before start, 471 ms of it with an idle slot in group 433
- [cross_source_blocking] source 5868 seq 9: ready 4606 ms before start, 629 ms of it with an idle slot in group 203

## Runtime violation samples

- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 7240: received seq 26 while store cursor is 24
- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 7240: received seq 26 while store cursor is 24
- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 7240: received seq 26 while store cursor is 24
- [ordering_gap] source 414: received seq 12 while store cursor is 10
- [ordering_gap] source 7240: received seq 26 while store cursor is 24
- [ordering_gap] source 7240: received seq 26 while store cursor is 24
- [ordering_gap] source 7240: received seq 26 while store cursor is 24

## Error samples

- t-361#0 src=11067 seq=4 op=Get rec=47532403064834 ev=1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: EVAL, origin: ReadAllAsync, outstanding: 39, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p27 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 140, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p3 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 140, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p19 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 140, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p11 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 140, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-293#1 src=8555 seq=5 op=Complete rec=36743445217281 ev=3: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: EVAL, origin: ReadAllAsync, outstanding: 39, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-435#0 src=13667 seq=13 op=Get rec=58699318034433 ev=6: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: EVAL, origin: ReadAllAsync, outstanding: 39, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-496#3 src=15843 seq=18 op=Update rec=68045166870531 ev=3: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: EVAL, origin: ReadAllAsync, outstanding: 39, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-152#0 src=4443 seq=2 op=Get rec=19082539696129 ev=1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: EVAL, origin: ReadAllAsync, outstanding: 39, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p22 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 188, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-121#4: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-401#0: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 188, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-246#1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-258#1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 188, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-429#0: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p26 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-457#0: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 188, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p10 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-385#0: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: UNKNOWN, origin: ReadAllAsync, outstanding: 188, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p2 attempt 1: RedisConnectionException: SocketClosed on 127.0.0.1:6380/Interactive, Idle/RanToCompletion, last: XREADGROUP, origin: ReadAllAsync, outstanding: 237, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 5859 | 5854 | 785 | 161480.703 | 186646.527 | 188743.679 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 11692 | 11686 | 47 | 153092.095 | 183500.799 | 183500.799 |
| t-496 | 81 | 8 | 272.308 | 370.179 | 278 | 0 | 11779 | 11769 | 67 | 153092.095 | 183500.799 | 184549.375 |
| t-055 | 65 | 4 | 189.718 | 196.354 | 359 | 0 | 6237 | 6233 | 29 | 166723.583 | 178257.919 | 178257.919 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 6501 | 6492 | 36 | 154140.671 | 176160.767 | 177209.343 |
| t-314 | 24 | 3 | 350.791 | 139.777 | 525 | 0 | 4444 | 4442 | 26 | 160432.127 | 176160.767 | 177209.343 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 13149 | 13141 | 65 | 160432.127 | 175112.191 | 177209.343 |
| t-106 | 24 | 3 | 332.963 | 135.379 | 824 | 0 | 4168 | 4166 | 22 | 158334.975 | 175112.191 | 176160.767 |
| t-206 | 118 | 4 | 90.937 | 175.214 | 1969 | 0 | 5835 | 5832 | 27 | 166723.583 | 175112.191 | 178257.919 |
| t-263 | 93 | 4 | 111.763 | 175.79 | 374 | 0 | 5579 | 5573 | 37 | 164626.431 | 175112.191 | 176160.767 |
| t-374 | 28 | 4 | 334.442 | 154.73 | 1811 | 0 | 4944 | 4938 | 33 | 157286.399 | 175112.191 | 176160.767 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 1422 | 1420 | 7 | 166723.583 | 174063.615 | 174063.615 |
| t-136 | 146 | 4 | 75.726 | 182.53 | 450 | 0 | 6016 | 6014 | 23 | 162529.279 | 174063.615 | 175112.191 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 5108 | 5103 | 29 | 166723.583 | 174063.615 | 176160.767 |
| t-315 | 88 | 4 | 117.565 | 172.184 | 2927 | 0 | 5544 | 5538 | 22 | 160432.127 | 174063.615 | 177209.343 |
| t-461 | 130 | 4 | 84.209 | 183.191 | 3025 | 0 | 5974 | 5967 | 33 | 163577.855 | 174063.615 | 176160.767 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 3963 | 3961 | 22 | 158334.975 | 173015.039 | 174063.615 |
| t-456 | 21 | 2 | 268.999 | 91.607 | 512 | 0 | 2891 | 2887 | 21 | 164626.431 | 173015.039 | 173015.039 |
| t-490 | 56 | 4 | 175.559 | 164.988 | 245 | 0 | 5176 | 5170 | 33 | 164626.431 | 173015.039 | 175112.191 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 3475 | 3473 | 8 | 168820.735 | 171966.463 | 175112.191 |
| t-120 | 37 | 2 | 155.825 | 94.609 | 1412 | 0 | 3066 | 3064 | 22 | 168820.735 | 171966.463 | 174063.615 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 3173 | 3172 | 22 | 166723.583 | 171966.463 | 173015.039 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 7858 | 7850 | 35 | 166723.583 | 171966.463 | 171966.463 |
| t-441 | 30 | 3 | 264.206 | 128.135 | 2203 | 0 | 4061 | 4058 | 36 | 162529.279 | 171966.463 | 171966.463 |
| t-023 | 72 | 1 | 39.107 | 47.088 | 647 | 0 | 1673 | 1671 | 8 | 168820.735 | 170917.887 | 171966.463 |
