# matrix-30: EMQX in place of Mosquitto (B31)

Same setup as matrix-29 (weighted shards 3,3,2 over the laptop, m4mini and m1mini; SourcePipeline 4; Postgres,
dispatchers and the broker on the laptop). EMQX 6.3.2 (single node: free under its Community License; clustering
needs a commercial license from 5.9 on), with Octopus-friendly settings: 64 MB packets, deep queues, no forced
shutdown of busy clients (`EB_MQTT=emqx` in infra/octopus/up.sh).

| offered | EMQX produced / applied | Mosquitto produced / applied (matrix-29) | broker CPU | laptop CPU | laptop shard publish p50 |
|---|---:|---:|---:|---:|---:|
| 24k | 20.9k / 19.4k | 21.5k / 21.4k | 2.7 cores (Mosquitto ~0.9) | 84% | 115 ms (Mosquitto 54 ms) |
| 28k | 22.9k / 21.7k | 23.6k / 22.5k | 2.6 | 84% | 139 ms |
| 32k | 22.9k / 21.8k | — | 2.6 | 85% | 149 ms |

**Verdict:** a multi-core broker on the same machine does not lift the ceiling; it takes ~3x Mosquitto's CPU from
the cores Postgres needs, and round trips get longer. The ceiling (~22–23.5k ev/s) is the laptop's total CPU
(~84%; what remains are efficiency cores), shared by Postgres (~10 cores), the broker, 3 dispatchers and the local
shard. Mosquitto's single thread is the first part to run out, but replacing it alone moves nothing here.

What would move it: the broker on its own machine, or a production layout with separate hosts for database,
broker and dispatchers. NATS (B32) is still interesting for high availability (free clustering), less for throughput
on this lab.
