# matrix-32: NATS as Octopus's transport (B32)

NATS 2.15.2 (`EB_MQTT=nats infra/octopus/up.sh`, container eb-nats-octopus, 64 MB payloads) in place of Mosquitto:
Octopus's fabric over NATS subjects and queue groups (Octopus 64f8903), the .NET client's NATS mode (NATS.Net
3.3.2), `--Broker:Transport=nats`. Everything else as matrix-29 (Postgres, PgBouncer, broker and 3 dispatchers on the
laptop; standard 500-group workload; workers commit their own writes; SourcePipeline 4).

The Macs reach the laptop only on the ports its Windows firewall allows; 4222 is not one, so NATS listens on 6440
(`EB_NATS_PORT=6440`, `--Broker:MqttPort=6440`), which the geo-lab rule opens.

## Local, 16k (Na-pg; Mosquitto: matrix-31 Lb-pg)

| | e2e p50 | e2e p99 | broker CPU | dispatchers | laptop CPU |
|---|---:|---:|---:|---:|---:|
| NATS | 6.8 ms | 1.29 s | 0.84 cores | 1.19 cores | 35.4% |
| Mosquitto | 7.1 ms | 1.29 s | 0.53 cores | 1.33 cores | 36.7% |

## The ceiling, laptop + m4mini (2 equal shards; T*)

| offered | NATS produced / applied | Mosquitto produced / applied | laptop shard e2e p50 NATS / Mosquitto | broker CPU NATS / Mosquitto |
|---|---:|---:|---:|---:|
| 24k | 23.6k / **23.6k** | 23.1k / 22.0k (matrix-29 T24-2shards) | 202 ms / — | 1.25 / — |
| 28k | 25.8k / **24.7k** | 25.1k / 23.3k | 963 ms / 2.3 s | 1.22 / 0.83 |
| 32k | 27.9k / **25.5k** | 27.8k / 24.6k | 4.6 s / 6.3 s | 1.16 / 0.84 |

Laptop CPU 82–83% in all of them (24 logical CPUs: Postgres ~10 cores, the local shard ~4, dispatchers ~2, the
broker 0.8–1.2, PgBouncer 0.3); laptop link ~72 MB/s in, ~54 out (of ~117).

**Verdict:** NATS lifts the ceiling, modestly: +4–6% applied (24.7k vs 23.3k at 28k; 25.5k vs 24.6k at 32k), and
the laptop shard's latency at the same load falls by more than half. Mosquitto's single thread sat at its limit
(~0.83 of a core, matrix-29); NATS spreads over threads and takes more CPU in total, but no longer queues. The
ceiling now is the laptop's total CPU, Postgres first (10 cores). **New best on this lab: 25.5k ev/s applied.**

## Repeats at 28k (V28-*, interleaved Mosquitto / NATS pairs)

| | run 1 | run 2 | run 3 | T28 | mean applied |
|---|---:|---:|---:|---:|---:|
| Mosquitto | 22.8k | 23.6k | 23.1k | 23.3k | **23.2k** (±0.3k) |
| NATS | 24.2k | 23.9k | 24.4k | 24.7k | **24.3k** (±0.4k) |

NATS ahead in every pair: **+4.7%** applied (produced ~25.2k vs ~25.5k). Mosquitto at 83% of its one core in all
four, NATS at 1.1–1.2 cores. The spread within a transport is small; P28-nats's low result (below) came with
pg_stat_statements sampling running.

## m1mini: a link problem, not NATS (N*-332)

The 3-shard ramp (weights 3,3,2) first looked like NATS failing on m1mini: its shard produced 0.5–0.9k ev/s instead
of ~6k, publish round trips p50 0.8 s and p99 19 s, 10 publish calls per event, even at 4k (probe/M8-n). Its en0 had
renegotiated to **100baseTX** (m4mini: 1000baseT) sometime between 01:45 (matrix-30: 46–52 MB/s out) and 04:29
(here: capped at 12 MB/s out): publishes queued behind the saturated link, timed out and were resent. The laptop
and m4mini shards in the same runs met their full shares (28k: 11.4k and 10.6k). Needs a cable or switch-port check;
`scripts/shard-run.sh` now refuses a Mac whose link is not at 1 GbE.

## ZeroMQ (B34): what is left for a brokerless transport here

The broker is ~6% of the laptop's work at the ceiling (1.2 of ~20 busy cores). Removing it entirely could free
that much at most; the dispatchers would take on part of it (a connection per client, fan-out of announcements).
So on this lab ZeroMQ's upper bound is a few percent of throughput, plus one hop less latency. In a production
layout the broker runs on its own hosts and costs the database host nothing. See B34 for the decision.

## Where the laptop's CPU goes at the ceiling (P28-nats, pg_stat_statements over 30 s of a 28k run)

~20 of 24 logical CPUs busy: Postgres ~10 cores, the local shard (producers and workers) ~4, dispatchers ~2, NATS
~1.2, PgBouncer 0.3, the rest system. Inside Postgres, Octopus's own work at 16k unsaturated (matrix-31 Lb-pg)
is ~2 cores (publish ~0.85, advance ~0.55, settle ~0.3): ~3–3.5 cores at 25k. The other ~6.5 are the
benchmark store's writes (`eb_commit_many` and its record/source updates), standing in for the application's own
database work. At saturation every statement's time inflates (publish 23 ms per call vs 1 ms at 16k), so per-call
times there are not costs. P28-nats itself (22.8k produced) ran below T28-nats (25.8k), with the statement sampling
running; plain repeats spread ±0.4k (V28-*).

What would lift it further: hardware (Postgres or the store on a host of its own, which a production layout has), or
less CPU per event in the store's writes (the application's, outside Octopus). Octopus's share is about a third of
Postgres and a sixth of the laptop.
