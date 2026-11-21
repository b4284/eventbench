# matrix-36: a 3-node NATS cluster, broker-node kill drills (B31)

`EB_MQTT=nats EB_NATS_NODES=3 infra/octopus/up.sh`: three NATS 2.15.2 nodes routed into one cluster (clients on
6440–6442). The dispatchers get all three URLs (`OCTOPUS_NATS_URL`, comma-separated: Octopus 71fa1e8 made that
work), the benchmark's clients the first one and learn the others from the cluster. Local 16k, Octopus alone
(in-memory store), 3 dispatchers; `scripts/octopus-drill.sh ... nats-kill:<n>` SIGKILLs node n 30 s into the run,
not restarted. In these runs node 1 carried all six benchmark connections and one dispatcher.

| run | failure | correct | lowest applied/s (t 28–45 s) | e2e p99 / p99.9 / max | publish max |
|---|---|---|---:|---|---:|
| Z0 | none | pass | 15.8k | 1.29 / 2.7 / 4.9 s | 0.17 s |
| Z1 | node 1 killed (every benchmark client on it) | pass | 15.8k | 1.61 / 18.1 / 30.0 s | 5.1 s |
| Z2 | node 2 killed (no clients) | pass | 15.8k | 1.35 / 2.9 / 30.1 s | 0.16 s |
| Z3 | node 1 killed | pass | 15.7k | 1.62 / 18.6 / 30.0 s | 5.1 s |

**Verdict:** with a NATS cluster the broker is no longer a single point of failure. Losing the node every
benchmark client was connected to caused no measurable outage: clients and dispatchers reconnected to the other
nodes within milliseconds (d1 in Z1: 5 ms), throughput never dipped. The cost is in the tail: messages in flight on
the dead node are lost (core NATS delivers at most once) and come again through Octopus's own retries after their
timeouts (30 s delivery timeout: e2e p99.9 18 s, max 30 s). Nothing was lost or applied twice.

With Mosquitto (no clustering) the same failure stops everything until the broker is back.

Possible next steps: shorter delivery and publish timeouts to shorten that tail; JetStream (at-least-once in the
broker) is not needed for correctness.
