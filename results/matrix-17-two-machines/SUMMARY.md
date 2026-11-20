# Matrix 17: two machines

- **Lab host** (24-core Core Ultra 9 275HX, WSL2, mirrored networking): Postgres, 4 PgBouncers, Mosquitto,
  3 Octopus dispatchers.
- **Mac mini M1** (4 performance + 4 efficiency cores, 8 GB, macOS 15.2): the benchmark process (producers and
  workers), a self-contained osx-arm64 build. `scripts/remote-run.sh`, `configs/overlay-remote-app.json`.
- **Link:** 1 Gbit/s Ethernet, 0.77 ms average round trip, measured 112-113 MiB/s each way.
- Workers commit their own writes (`overlay-octopus-worker.json`), one event per publish, async commits, no standby.
- Lab-side CPU from /proc per process name (`scripts/local-cpu-summary.py`, middle 60 % of the run).

| run | load | produced / applied | e2e p50 / p99 | blocking violations | Mac (bench) | lab: Postgres / dispatchers / PgBouncer / Mosquitto | lab network in / out |
|---|---|---|---|---|---|---|---|
| M1 (1st) | 12.9k | all | 47 ms / 3.0 s | 35.6k | 6.1 cores | (sampling broken) | — |
| M1 | 12.9k | all | 26 ms / 2.9 s | 5.4k | 6.4 cores | 7.6 / 2.3 / 0.8 / 0.7 cores | — |
| M2 (1st) | 16k | **693k of 1.0M produced; 9.4k/s** | 6.1 s / 41 s | 65k | 2.9 cores | 4.3 / 1.1 / 0.5 / 0.3 cores | (not sampled) |
| M2 | 16k | all; 16.1k/s | 143 ms / 3.6 s | 12.2k | 5.5 cores | 8.5 / 2.2 / 0.9 / 0.7 cores | **97 / 72 MiB/s** |

All runs pass the oracle except the blocking check.

## Findings
- **The link is the limit, not either machine.** At 16k the lab receives ~100 MiB/s of the 113 available: inline
  large events (1-4 MB) cross it twice towards the lab (publish; the worker's commit with its upload) and once
  back (delivery). The first 16k attempt went over the edge and stalled (both machines idle, p99 41 s). 24k cannot
  run on this link. For production sizing: the database/broker hosts need 10 GbE at this payload mix.
- **Every hop now costs a LAN round trip** (publish, delivery, the worker's context read and commit, done): at 12.9k
  the median is 26-47 ms against 16 ms with everything on one host, with more blocking.
- The Mac runs the benchmark process at 5.5-6.4 of its 8 cores (half of them efficiency cores): near its limit.
- Database CPU at 16k (8.5 cores) is what the single-host runs showed: separating the application did not change
  the database's own cost.
