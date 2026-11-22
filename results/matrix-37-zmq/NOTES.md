# matrix-37: ZeroMQ, no broker (B34)

Octopus 8d0b0d6/8bb1fb6: the fabric over ZeroMQ (Rust `zeromq` 0.7.0, .NET NetMQ 4.0.4.5). Every dispatcher binds a
ROUTER and a PUB; every client and dispatcher holds a DEALER and a SUB to every dispatcher; requests and replies
travel one hop. `EB_MQTT=zmq infra/octopus/up.sh` (no broker), `--Broker:Transport=zmq --Broker:ZmqPeers=...`.
The Macs reach the dispatchers on port pairs the laptop's firewall already allows: `EB_ZMQ_PORTS="2379 5441 8008"`.

From this matrix on the wired lab is 192.168.31.0/24 (laptop .128, m4mini .36, m1mini .105; both Macs at 1 GbE).

## Local 16k (Za-pg; as matrix-31 Lb-pg and matrix-32 Na-pg)

| transport | e2e p50 | e2e p99 | broker CPU | dispatchers | benchmark process | laptop CPU |
|---|---:|---:|---:|---:|---:|---:|
| ZeroMQ | 6.7 ms | 1.29 s | — | 1.31 | 5.4 cores | 34.9% |
| NATS | 6.8 ms | 1.29 s | 0.84 | 1.19 | 4.7 | 35.4% |
| Mosquitto | 7.1 ms | 1.29 s | 0.53 | 1.33 | 4.7 | 36.7% |

## The ceiling: laptop + m4mini + m1mini (weights 3,3,2), 28k offered, interleaved rounds (W28-*)

| round | ZeroMQ applied | NATS applied | Mosquitto applied |
|---|---:|---:|---:|
| 1 | 22.2k | 20.7k | 20.6k |
| 2 | 23.3k | 21.9k | 20.9k |
| 3 | 25.0k | 23.5k | 22.9k |
| **mean** | **23.5k** | **22.0k** | **21.5k** |

ZeroMQ first in every round: **+6.6% over NATS, +9.5% over Mosquitto** (NATS over Mosquitto +2.7% here, +4.7% in
the two-machine repeats of matrix-32). Every round rose over the session (the host warming up; interleaving keeps
the comparison fair). Publish round trips p50 on the Mac shards: ZeroMQ 175–237 ms, NATS 250–312, Mosquitto
229–341: one hop less matters most for the remote clients. Laptop CPU 82–84% in all; Postgres ~10.5 cores. All 15
shard results correct (cursors, applied = generated, drained; cross-source blocking fails under overload, as always).

**Verdict:** on this lab ZeroMQ lifts the ceiling more than NATS did: the broker's CPU is gone and every message
saves a hop, which shows most for clients on other machines. The price: no broker means no discovery (the
dispatcher list is configuration: adding a dispatcher means a new list everywhere), every client connected to every
dispatcher, a dispatcher that dies drops what was queued to it (Octopus retries), and no central place to watch the
traffic. NATS keeps discovery, clustering (matrix-36) and monitoring for a smaller gain. Production choice: ZeroMQ
where the dispatcher set is static and the last percent counts; NATS otherwise.

## Not done yet

- ZeroMQ failure drills: a dispatcher killed (its queued messages dropped, clients route around it after the
  connection drops) and added (needs the new list).
- Dynamic membership: dispatcher endpoints in their member announcements, so clients learn new ones.
