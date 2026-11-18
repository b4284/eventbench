#!/usr/bin/env python3
"""Aggregate repeated runs: median (and min-max) per variant directory.

usage: scripts/aggregate.py <matrix-dir>
Expects <matrix-dir>/<variant>/<stamp>_<variant>/summary.json (layout produced by run-matrix.sh).
"""
import json, os, statistics, sys
from pathlib import Path

root = Path(sys.argv[1])
METRICS = [
    ("verdicts", None),
    ("applied/s", lambda j: j["throughput"]["steadyAppliedPerSec"]),
    ("e2e p50", lambda j: lat(j, "e2e", "p50")),
    ("e2e p99", lambda j: lat(j, "e2e", "p99")),
    ("e2e p99.9", lambda j: lat(j, "e2e", "p99_9")),
    ("e2e max", lambda j: lat(j, "e2e", "max")),
    ("qwait p99", lambda j: lat(j, "queue_wait", "p99")),
    ("commit p99", lambda j: lat(j, "ctx_commit", "p99")),
    # Cores used by everything except the benchmark process (≈ Postgres + PgBouncer); valid for all runs.
    ("db-side cores", lambda j: non_bench_cores(j)),
    # Container CPU from podman; only valid for runs after the cpu_time-delta fix (2026-11-18 ~00:00).
    ("pg cpu%", lambda j: res(j, "ctr_eb_postgres_cpu_pct")),
    ("pgb cpu%", lambda j: res(j, "ctr_eb_pgbouncer_cpu_pct")),
    ("pg tps", lambda j: res(j, "pg_tps")),
    ("wal MB/s", lambda j: res(j, "pg_wal_mb_per_s")),
    ("pgb waiting", lambda j: res(j, "pgb_cl_waiting")),
    ("pgb maxwait ms", lambda j: res(j, "pgb_maxwait_ms", "max")),
    ("sv active", lambda j: res(j, "pgb_sv_active")),
]

def non_bench_cores(j):
    host, bench = res(j, "host_cpu_pct"), res(j, "bench_cpu_cores")
    if host is None or bench is None:
        return None
    return host / 100 * (os.cpu_count() or 1) - bench

def lat(j, metric, p):
    for l in j["latencyMs"]:
        if l["metric"] == metric:
            return l[p]
    return None

def res(j, key, stat="mean"):
    v = j["resourcesSteadyState"].get(key)
    return v[stat] if v else None

rows = []
for vdir in sorted(p for p in root.iterdir() if p.is_dir()):
    runs = [json.loads(f.read_text()) for f in sorted(vdir.glob("*/summary.json"))]
    if not runs:
        continue
    row = {"variant": vdir.name, "n": len(runs)}
    for name, fn in METRICS:
        if fn is None:
            row[name] = "".join("P" if r["verdict"] == "PASS" else "F" for r in runs)
            continue
        vals = [v for v in (fn(r) for r in runs) if v is not None]
        row[name] = (statistics.median(vals), min(vals), max(vals)) if vals else None
    rows.append(row)

def fmt(v):
    if v is None: return "-"
    if isinstance(v, str): return v
    med, lo, hi = v
    return f"{med:.1f}" if lo == hi else f"{med:.1f} ({lo:.1f}-{hi:.1f})"

cols = ["variant", "n"] + [m for m, _ in METRICS]
print("| " + " | ".join(cols) + " |")
print("|" + "|".join("---" for _ in cols) + "|")
for r in rows:
    print("| " + " | ".join(str(r[c]) if c in ("variant", "n") else fmt(r[c]) for c in cols) + " |")
print("\nvalues: median (min-max) across repetitions; latencies in ms")
