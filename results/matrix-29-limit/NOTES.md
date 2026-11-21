# matrix-29: the ceiling of the current setup with the load split across machines

Postgres, PgBouncer, Mosquitto and 3 Octopus dispatchers on the lab laptop; producers and workers split into shards
(laptop, m4mini, m1mini; `scripts/shard-run.sh`). Standard 500-group workload; workers commit their own writes. Network
measured over the production window on the laptop's eth0 and the Macs' en0 (1 GbE each, ~117 MB/s each way).

## Ramp, 3 equal shards (R16..R32)

| offered | total produced/s | p50 laptop / m4mini / m1mini | laptop link in/out MB/s | Mosquitto | Postgres |
|---|---:|---:|---:|---:|---:|
| 16k | 16.0k | 56 ms / 68 ms / 124 ms | 77 / 55 | 93% | 9.5 cores |
| 20k | 19.0k | 81 ms / 104 ms / 8.4 s | 80 / 58 | 90% | 9.6 |
| 24k | 20.8k | 178 ms / 417 ms / 12.8 s | 74 / 56 | 90% | 10.2 |
| 28k | 22.6k | 235 ms / 5.5 s / 16.4 s | 73 / 55 | 86% | 10.2 |
| 32k | 23.5k | 2.4 s / 10.1 s / 21.9 s | 68 / 52 | 84% | 10.3 |

The slowest shard (m1mini) fell behind its own schedule: at 24k `e2e` p50 12.8 s but `e2e_from_send` 276 ms. Its
publish round trips were 175 ms (laptop 56 ms), and the benchmark's producer sends a source's next event only after
the previous one is acknowledged, so its busiest sources could not keep up.

## Variations at 24k

| run | change | total produced / applied | notes |
|---|---|---:|---|
| Q24-pipe4-mosq | up to 4 events per source in flight (`Workload:SourcePipeline=4`) | 21.6k / 21.4k | m1mini still lags (round trip 237 ms) |
| Q24-pipe4-nanomq | NanoMQ 0.25.7 instead of Mosquitto | ~7k / ~3.7k | NanoMQ at 3.9 cores, round trips 650 ms: unsuited to Octopus's traffic |
| T24-2shards | laptop + m4mini only | 23.1k / 22.0k | m4mini delivered 11.2k of 12k; the laptop shard lagged after sending |
| W24-332 | weights 3,3,2 (`Workload:ShardWeights`) | 21.5k / 21.4k | Mac round trips 231 / 283 ms |
| W28-332 | same at 28k | 23.6k / 22.5k | |
| C24-332-conn16 | 16 producer / 8 worker MQTT connections per process | 22.1k / **14.7k** | Mosquitto 97%; the laptop shard's round trip 54 -> 195 ms |

## Verdict

- **Ceiling ~22–23.5k ev/s** with the full application workload, however the load is split.
- **Mosquitto is the first hard limit:** 84–93% of its single thread in every run at 16k and above with remote
  clients (~57% all-local at 16k: remote TCP costs it more per message). More connections pushed it to 97%, and
  every shard slowed, including the laptop's own, which crosses no network.
- **Postgres is next:** ~10 of 24 cores at the ceiling; the laptop at ~82% overall, much of it on efficiency cores.
- **The network is not the limit:** at most ~77% of one direction (90 MB/s in) on the laptop; the Macs 20–73 MB/s.
  (vnstat on `lo` shows loopback, local traffic with no link limit.)
- Per machine as a load generator (with the server side not saturated): m1mini ~6k ev/s, m4mini ~11k.

**Not explored yet:**
- EMQX, a multi-core broker that also clusters, which would remove Mosquitto as the topology's single point of
  failure (B31);
- fewer MQTT messages per event inside Octopus (more batching of replies and done reports);
- Postgres's own ceiling once MQTT is out of the way.
