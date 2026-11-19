# C2c-redis3-async-KILL-PRIMARY — FAIL ❌

- Broker: `redis streams @ 127.0.0.1:26380,127.0.0.1:26381,127.0.0.1:26382,serviceName=ebmaster, =workers streams per group, single consumer per stream, 8 connections, XACK+XDEL on ack, waitaof=False, wait replicas=0`
- Store: `redis @ 127.0.0.1:26380,127.0.0.1:26381,127.0.0.1:26382,serviceName=ebmaster, 8 connections, commit = Lua script (cursor check + version-guarded record + upload + cursor), waitaof=False, wait replicas=0`
- Ack mode: `InTransaction`, batch 10, parallel/worker 4
- Topology: 500 groups, 16000 sources, 780 workers, offered 24033.262 ev/s
- Phases: run 152.476s, closing 0.748s, drain 600.103s (drained: false)

## Throughput

| produced | applied | steady produced/s | steady applied/s | peak applied/s | duplicates | errors | nacks | large events | slow events | blocking violations |
|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| 791229 | 791423 | 3566.701 | 2827.26 | 30762 | 55162 | 3790 | 5633 | 265 | 3952 | 281644 |

## Latency (ms, after warmup, excluding closing events)

| metric | count | mean | p50 | p90 | p99 | p99.9 | max |
|---|---:|---:|---:|---:|---:|---:|---:|
| e2e | 508031 | 50641.268 | 8871.935 | 132841.471 | 136708.095 | 139198.463 | 143654.911 |
| e2e_from_send | 508031 | 50609.312 | 8839.167 | 132841.471 | 136577.023 | 139067.391 | 143523.839 |
| producer_lag | 507837 | 31.918 | 25.615 | 66.175 | 127.103 | 188.415 | 248.575 |
| publish_call | 22736 | 211.047 | 38.399 | 80.959 | 153.471 | 121176.063 | 121307.135 |
| queue_wait | 508031 | 50141.68 | 8708.095 | 132710.399 | 136445.951 | 138805.247 | 141950.975 |
| broker_dwell | 508031 | 49891.345 | 8650.751 | 132710.399 | 136445.951 | 138805.247 | 141950.975 |
| receive_call | 599926 | 52.369 | 27.375 | 72.639 | 142.847 | 8970.239 | 19529.727 |
| ctx_load | 508031 | 63.598 | 18.239 | 36.543 | 70.079 | 18956.287 | 18956.287 |
| process | 508031 | 11.029 | 0.001 | 0.002 | 0.005 | 2625.535 | 17891.327 |
| ctx_commit | 508031 | 25.371 | 21.263 | 39.167 | 67.775 | 106.879 | 18956.287 |
| service | 508031 | 467.641 | 43.135 | 89.215 | 2181.119 | 122355.711 | 126943.231 |
| e2e_large | 165 | 16637.428 | 2699.263 | 123338.751 | 125501.439 | 129761.279 | 129761.279 |
| ready_wait | 508031 | 2861.686 | 139.903 | 1272.831 | 122683.391 | 126681.087 | 135921.663 |
| blocked | 508031 | 2831.277 | 135.295 | 1236.991 | 122552.319 | 126156.799 | 134610.943 |

## Resources (steady state mean / max)

| metric | mean | max |
|---|---:|---:|
| applied_per_s | 2827.26 | 24640.5 |
| applied_total | 543508.074 | 571985 |
| bench_cpu_cores | 0.472 | 6.812 |
| bench_cpu_pct | 1.966 | 28.38 |
| bench_gc_gen0 | 0.36 | 4 |
| bench_gc_gen1 | 0.36 | 4 |
| bench_gc_gen2 | 0.044 | 1 |
| bench_gc_heap_mb | 926.29 | 1339.9 |
| bench_mem_mb | 1567.447 | 1722 |
| bench_threadpool_queue | 2783.574 | 5136 |
| bench_threadpool_threads | 105.515 | 181 |
| broker_backlog | 170989.713 | 183424 |
| ctr_eb_redis_6380_cpu_pct | 438.515 | 476.79 |
| ctr_eb_redis_6380_mem_mb | 1717.658 | 2035.1 |
| ctr_eb_redis_6381_cpu_pct | 40.339 | 178.13 |
| ctr_eb_redis_6381_mem_mb | 1773.843 | 2019.9 |
| ctr_eb_redis_6382_cpu_pct | 33.283 | 218.23 |
| ctr_eb_redis_6382_mem_mb | 1784.815 | 1942.6 |
| ctr_eb_sentinel_26380_cpu_pct | 0.231 | 0.96 |
| ctr_eb_sentinel_26380_mem_mb | 4.457 | 4.5 |
| ctr_eb_sentinel_26381_cpu_pct | 0.222 | 0.98 |
| ctr_eb_sentinel_26381_mem_mb | 4.451 | 4.5 |
| ctr_eb_sentinel_26382_cpu_pct | 0.213 | 0.94 |
| ctr_eb_sentinel_26382_mem_mb | 4.496 | 4.7 |
| duplicates_per_s | 0 | 0 |
| errors_per_s | 17.723 | 1678.6 |
| host_cpu_pct | 6.492 | 42.34 |
| in_flight | 176307.86 | 182851 |
| nacks_per_s | 10.497 | 966.8 |
| produced_per_s | 3566.701 | 25516.6 |
| produced_total | 719815.934 | 754836 |
| publish_mb_per_s | 10.105 | 88.84 |
| redis_aof_buffer_length | 771498.86 | 9632076 |
| redis_aof_current_size | 1433.839 | 2198 |
| redis_aof_delayed_fsync_per_s | 0 | 0 |
| redis_aof_pending_bio_fsync | 0.015 | 1 |
| redis_blocked_clients | 0 | 0 |
| redis_connected_clients | 38 | 38 |
| redis_instantaneous_ops_per_sec | 261257.39 | 289341 |
| redis_total_commands_processed_per_s | 253485.912 | 289394.3 |
| redis_total_net_input_bytes_per_s | 109637604.215 | 144123717.9 |
| redis_total_net_output_bytes_per_s | 299554061.781 | 377233703.7 |
| redis_used_cpu_sys_cores | 2.147 | 2.31 |
| redis_used_cpu_sys_main_thread_cores | 0.155 | 0.27 |
| redis_used_cpu_user_cores | 1.79 | 1.8 |
| redis_used_cpu_user_main_thread_cores | 0.824 | 0.86 |
| redis_used_memory | 1716.326 | 1757.6 |
| redisq_aof_buffer_length | 1133103.941 | 7325900 |
| redisq_aof_current_size | 1415.616 | 2175.4 |
| redisq_aof_delayed_fsync_per_s | 0 | 0 |
| redisq_aof_pending_bio_fsync | 0.015 | 1 |
| redisq_blocked_clients | 0 | 0 |
| redisq_connected_clients | 38 | 38 |
| redisq_instantaneous_ops_per_sec | 260870.588 | 289341 |
| redisq_total_commands_processed_per_s | 250002.549 | 288359.6 |
| redisq_total_net_input_bytes_per_s | 108205098.066 | 142882840.7 |
| redisq_total_net_output_bytes_per_s | 300291782.106 | 381767758.5 |
| redisq_used_cpu_sys_cores | 2.13 | 2.29 |
| redisq_used_cpu_sys_main_thread_cores | 0.156 | 0.28 |
| redisq_used_cpu_user_cores | 1.764 | 1.8 |
| redisq_used_cpu_user_main_thread_cores | 0.815 | 0.86 |
| redisq_used_memory | 1720.482 | 1763.1 |

## Oracle

| check | result | detail |
|---|---|---|
| cursors | pass | 16000/16000 source cursors at the expected last sequence |
| records_complete | pass | all 114,932 records are Completed |
| record_counts | pass | 114,932 records found, 114,932 expected; 0 sources with count mismatch |
| record_content | pass | 0 sources with record content hash mismatch |
| uploads | pass | 265 uploads verified, 0 sources mismatched |
| applied_equals_generated | **FAIL** | 791,423 events applied by workers, 791,229 generated |
| broker_drained | **FAIL** | 5 messages left in the broker |
| runtime_ordering_gap | **FAIL** | 3,450 observed by workers |
| runtime_pre_state | pass | none observed |
| runtime_record_conflict | pass | none observed |
| runtime_payload_corrupt | pass | none observed |
| runtime_context_corrupt | pass | none observed |
| runtime_blob_missing | pass | none observed |
| runtime_cross_source_blocking | **FAIL** | 281,644 events waited > 100 ms while a worker slot of their group was idle (4 slot(s)/worker; blocked p99 122552 ms, max 134580 ms) |

## Cross-source blocking samples

- [cross_source_blocking] source 7415 seq 12: ready 103 ms before start, 103 ms of it with an idle slot in group 256
- [cross_source_blocking] source 8656 seq 13: ready 108 ms before start, 108 ms of it with an idle slot in group 300
- [cross_source_blocking] source 7313 seq 19: ready 111 ms before start, 111 ms of it with an idle slot in group 251
- [cross_source_blocking] source 1881 seq 11: ready 119 ms before start, 119 ms of it with an idle slot in group 54
- [cross_source_blocking] source 4746 seq 28: ready 102 ms before start, 102 ms of it with an idle slot in group 164
- [cross_source_blocking] source 7984 seq 23: ready 131 ms before start, 131 ms of it with an idle slot in group 269
- [cross_source_blocking] source 8608 seq 15: ready 132 ms before start, 132 ms of it with an idle slot in group 297
- [cross_source_blocking] source 12723 seq 10: ready 109 ms before start, 109 ms of it with an idle slot in group 401
- [cross_source_blocking] source 2419 seq 11: ready 109 ms before start, 109 ms of it with an idle slot in group 71
- [cross_source_blocking] source 8573 seq 18: ready 159 ms before start, 159 ms of it with an idle slot in group 295
- [cross_source_blocking] source 9677 seq 9: ready 133 ms before start, 133 ms of it with an idle slot in group 336
- [cross_source_blocking] source 10312 seq 4: ready 112 ms before start, 112 ms of it with an idle slot in group 345
- [cross_source_blocking] source 4027 seq 5: ready 111 ms before start, 111 ms of it with an idle slot in group 135
- [cross_source_blocking] source 7132 seq 16: ready 161 ms before start, 161 ms of it with an idle slot in group 242
- [cross_source_blocking] source 8225 seq 11: ready 145 ms before start, 145 ms of it with an idle slot in group 277
- [cross_source_blocking] source 4180 seq 8: ready 145 ms before start, 145 ms of it with an idle slot in group 137
- [cross_source_blocking] source 13173 seq 39: ready 120 ms before start, 120 ms of it with an idle slot in group 418
- [cross_source_blocking] source 7402 seq 18: ready 142 ms before start, 142 ms of it with an idle slot in group 255
- [cross_source_blocking] source 8378 seq 12: ready 142 ms before start, 142 ms of it with an idle slot in group 284
- [cross_source_blocking] source 6394 seq 13: ready 149 ms before start, 149 ms of it with an idle slot in group 219

## Runtime violation samples

- [ordering_gap] source 6146: received seq 48 while store cursor is 46
- [ordering_gap] source 10122: received seq 64 while store cursor is 62
- [ordering_gap] source 3595: received seq 67 while store cursor is 65
- [ordering_gap] source 11587: received seq 15 while store cursor is 13
- [ordering_gap] source 3618: received seq 75 while store cursor is 73
- [ordering_gap] source 10042: received seq 105 while store cursor is 102
- [ordering_gap] source 15266: received seq 32 while store cursor is 30
- [ordering_gap] source 3258: received seq 21 while store cursor is 19
- [ordering_gap] source 3194: received seq 99 while store cursor is 97
- [ordering_gap] source 2514: received seq 44 while store cursor is 42
- [ordering_gap] source 15706: received seq 68 while store cursor is 66
- [ordering_gap] source 5607: received seq 76 while store cursor is 74
- [ordering_gap] source 7223: received seq 125 while store cursor is 123
- [ordering_gap] source 5611: received seq 59 while store cursor is 57
- [ordering_gap] source 3211: received seq 62 while store cursor is 60
- [ordering_gap] source 12707: received seq 45 while store cursor is 43
- [ordering_gap] source 7072: received seq 72 while store cursor is 70
- [ordering_gap] source 6146: received seq 48 while store cursor is 46
- [ordering_gap] source 5712: received seq 71 while store cursor is 69
- [ordering_gap] source 8131: received seq 73 while store cursor is 71

## Error samples

- t-308L#0 src=8930 seq=1 op=Create rec=38354057953281 ev=0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 93, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-456#1 src=14224 seq=81 op=Update rec=61091614818317 ev=1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 118, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-343#1 src=10097 seq=22 op=Update rec=43366284787717 ev=3: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 110, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-197#1 src=5734 seq=43 op=Create rec=24627342475272 ev=0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 133, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-348#1 src=10547 seq=35 op=Create rec=45299020070920 ev=0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 107, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-119L#1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-041#3 src=1300 seq=107 op=Update rec=5583457484810 ev=8: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 141, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-319L#1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-430#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-110#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-286#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-262#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-318#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-054#0: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- receive t-391#1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p9 attempt 1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p1 attempt 1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p25 attempt 1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- t-260#0 src=7557 seq=63 op=Update rec=32457067855880 ev=6: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: EVAL, origin: ReadAllAsync, outstanding: 111, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 0, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751
- publish p17 attempt 1: RedisConnectionException: SocketFailure on 127.0.0.1:6380/Interactive, Idle/Faulted, last: XREADGROUP, origin: ReadAllAsync, outstanding: 294, last-read: 0s ago, last-write: 0s ago, unanswered-write: 0s ago, keep-alive: 60s, state: ConnectedEstablished, in: 0, in-pipe: 4, last-heartbeat: 0s ago, last-mbeat: 0s ago, global: 0s ago, v: 3.4.35.20751

## Groups (500; worst 25 by e2e p99 — all groups in summary.json)

| group | sources | workers | base ev/min/src | offered/s | payload B | large frac | produced | applied | dup | e2e p50 | e2e p99 | e2e max |
|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| t-246 | 32 | 4 | 351.04 | 185.471 | 494 | 0 | 5892 | 5893 | 1580 | 128450.559 | 142606.335 | 143654.911 |
| t-147 | 79 | 8 | 272.153 | 365.454 | 1078 | 0 | 11767 | 11771 | 3353 | 127401.983 | 139460.607 | 140509.183 |
| t-181 | 35 | 4 | 278.072 | 160.65 | 1367 | 0 | 5143 | 5144 | 1198 | 10944.511 | 139460.607 | 141557.759 |
| t-212 | 29 | 1 | 94.612 | 45.773 | 296 | 0 | 1469 | 1469 | 976 | 131071.999 | 139460.607 | 140509.183 |
| t-260 | 30 | 2 | 155.854 | 80.682 | 499 | 0 | 2671 | 2671 | 623 | 10092.543 | 139460.607 | 139460.607 |
| t-029 | 28 | 3 | 267.923 | 124.238 | 1732 | 0 | 3986 | 3986 | 388 | 126877.695 | 138412.031 | 140509.183 |
| t-065 | 80 | 9 | 317.275 | 418.809 | 263 | 0 | 13244 | 13248 | 1348 | 127926.271 | 138412.031 | 139460.607 |
| t-109 | 33 | 1 | 88.869 | 49.539 | 1467 | 0 | 1607 | 1607 | 160 | 132120.575 | 138412.031 | 141557.759 |
| t-132 | 22 | 2 | 282.036 | 95.907 | 2676 | 0 | 3026 | 3026 | 406 | 132644.863 | 138412.031 | 140509.183 |
| t-152 | 43 | 1 | 70.449 | 49.733 | 882 | 0 | 1633 | 1633 | 244 | 132644.863 | 138412.031 | 139460.607 |
| t-261 | 51 | 2 | 111.137 | 94.927 | 2342 | 0 | 3185 | 3185 | 38 | 131596.287 | 138412.031 | 139460.607 |
| t-333 | 79 | 1 | 37.141 | 47.836 | 192 | 0 | 1642 | 1642 | 2 | 133693.439 | 138412.031 | 139460.607 |
| t-348 | 125 | 5 | 117.817 | 246.942 | 1340 | 0 | 7909 | 7909 | 14 | 130023.423 | 138412.031 | 140509.183 |
| t-350 | 85 | 3 | 100.24 | 137.772 | 285 | 0 | 4539 | 4539 | 354 | 131596.287 | 138412.031 | 139460.607 |
| t-378 | 55 | 3 | 160.573 | 143.865 | 3659 | 0 | 4866 | 4867 | 170 | 131071.999 | 138412.031 | 139460.607 |
| t-438 | 121 | 6 | 139.659 | 292.416 | 1839 | 0 | 9561 | 9561 | 394 | 130023.423 | 138412.031 | 139460.607 |
| t-469 | 105 | 5 | 143.35 | 249.426 | 278 | 0 | 8066 | 8068 | 82 | 131596.287 | 138412.031 | 140509.183 |
| t-037 | 21 | 1 | 138.225 | 48.51 | 272 | 0 | 1575 | 1575 | 0 | 132644.863 | 137363.455 | 137363.455 |
| t-040 | 119 | 2 | 49.029 | 98.737 | 1254 | 0 | 3492 | 3492 | 129 | 131596.287 | 137363.455 | 139460.607 |
| t-041 | 34 | 5 | 363.669 | 204.852 | 2732 | 0 | 6531 | 6531 | 772 | 7766.015 | 137363.455 | 138412.031 |
| t-077 | 10 | 1 | 268.542 | 44.819 | 1566 | 0 | 1430 | 1430 | 1 | 131071.999 | 137363.455 | 138412.031 |
| t-105 | 33 | 2 | 137.551 | 79.086 | 851 | 0 | 2599 | 2600 | 437 | 10420.223 | 137363.455 | 138412.031 |
| t-121 | 76 | 5 | 182.916 | 224.389 | 149 | 0 | 7279 | 7281 | 429 | 127401.983 | 137363.455 | 137363.455 |
| t-182 | 89 | 5 | 153.229 | 222.936 | 2406 | 0 | 7254 | 7255 | 435 | 127401.983 | 137363.455 | 139460.607 |
| t-245 | 43 | 2 | 133.219 | 91.504 | 2232 | 0 | 2925 | 2926 | 11 | 130023.423 | 137363.455 | 138412.031 |
