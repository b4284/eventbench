# matrix-25: peak capacity of one dispatcher (B21)

Workers commit to the benchmark's in-memory store (`--Store:Type=inmemory`), so Postgres carries only Octopus's
own statements (publish, cursor writes, reads). Same workload otherwise (500 groups, slow events, inline large
events), Octopus `257cc83`. Script: `scripts/dispatcher-peak.sh`.

| run | applied/s | e2e p50 / p99 | dispatcher cores | Postgres cores | Mosquitto (1 core = 100%) |
|---|---:|---:|---:|---:|---:|
| 1 dispatcher, 16k | 15,947 | 8 ms / 1.3 s | 0.92 | 1.6 | 35% |
| 1 dispatcher, 24k | 23,915 | 15 ms / 1.6 s | 1.25 | 2.1 | 44% |
| 1 dispatcher, 32k | 31,046 | **1.5 s** / 4.0 s | 1.31 | 2.1 | 43% |
| 3 dispatchers, 32k | 31,861 | 15 ms / 1.7 s | 2.05 | 4.0 | 74% |

(p99 includes the workload's deliberately slow events, 1–3 s.)

- **One dispatcher carries 24k ev/s at a 15 ms median**, using 1.25 cores. Between 24k and 32k it saturates:
  it still moves 31k/s, but the median rises to 1.5 s while it uses only 1.3 cores. Something serial limits it,
  not CPU; possibly the core state behind one mutex, or the publish/flush round trips. Not profiled yet.
- **Three dispatchers carry 32k at 15 ms.** Scaling out works.
- **Octopus's own database cost is small:** ~0.1 Postgres core per 1k ev/s (1.6 cores at 16k, 2.1 at 24k with one
  dispatcher; 4.0 at 32k with three, whose batches are smaller). In the full runs (matrix-24) Postgres used
  ~8 cores at 16k, so ~80% of it is the application's own worker writes (`eb_commit_many`, record lookups and
  updates), not Octopus.
- **Mosquitto is single-threaded:** 74% of one core at 32k. On this host it would cap somewhere around 40k ev/s.
  Beyond that: a multi-threaded broker (EMQX, VerneMQ), or several brokers.

**Not explored yet:** what serializes one dispatcher near 30k (profile it); Mosquitto's actual ceiling; a run with
2 dispatchers.
