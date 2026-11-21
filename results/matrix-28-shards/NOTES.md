# matrix-28: producers and workers split across machines (sharded runs)

The benchmark process (producers, workers, checker) is split by groups: shard 0 on the lab laptop, shard 1 on m4mini
(Apple M4, 10 cores, 1 GbE). Postgres, PgBouncer, Mosquitto and the 3 Octopus dispatchers stay on the laptop. Each
shard takes the groups whose id % 2 equals its index (250 groups, ~8k sources, ~8k ev/s each), starts at the same
wall-clock instant, and verifies its own sources. Shard 0 sets up the shared store. Workers commit their own writes
to Postgres through PgBouncer (shard 1 over the LAN). 16k ev/s total, the standard 500-group workload with slow and
large events.

Scripts: `scripts/shard-deploy.sh` (self-contained osx-arm64 build to the Macs), `scripts/shard-run.sh`
(`EB_SHARD_HOSTS="m4mini"`). Benchmark options: `Workload:ShardCount`, `ShardIndex`, `StartAtUnixMs`.

| run | applied/s (total) | e2e p50 | e2e p99 | laptop: benchmark | laptop: Postgres | laptop: dispatchers | laptop CPU |
|---|---:|---:|---:|---:|---:|---:|---:|
| S2a split | 15,921 | 52 / 48 ms (shard 0 / 1) | 1.45 s | 3.6 cores | 8.6 cores | 2.2 cores | 75% |
| L2a all local | 16,240 | 111 ms | 3.05 s | 5.8 | 7.9 | 1.6 | 74% |
| S2b split | 15,902 | 43 / 41 ms | 1.42 s | 3.5 | 8.4 | 2.3 | 74% |
| L2b all local | 16,299 | 96 ms | 3.75 s | 5.9 | 7.8 | 1.6 | 74% |

(The local runs apply slightly faster than 16k: they catch up a backlog during the drain. p99 includes the
workload's slow events, 1–3 s.)

- **Correct:** every shard's sources at the expected last sequence (7,973 + 8,027), all checks pass except the
  usual cross-source blocking (not enforced).
- **Latency halves:** p50 41–52 ms vs 96–111 ms, p99 1.4 s vs 3.0–3.7 s. The laptop no longer runs the benchmark's
  ~2.3 cores of producers and workers for half the load. m4mini's shard used ~5 cores of its own.
- **The laptop's total CPU stays ~74%:** the freed cores went to Postgres (+0.6 cores) and the dispatchers (+0.6).
  The system keeps up instead of queueing: same work, less waiting. The laptop is still the limit for going
  higher, now mostly through Postgres.
- First attempt (S1): Mosquitto stayed bound to 127.0.0.1 because it was already running (up.sh now recreates it when
  the requested bind address differs), and the shard's steady-state window counted the wait for the common start
  (fixed).

**Not explored yet:** m1mini as a third shard; 20k and 24k with the load split; MQTT, delivery and DB-commit
latency over the LAN measured separately; the SQL Server backend with split load.
