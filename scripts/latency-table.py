#!/usr/bin/env python3
"""Plain latency table from a matrix directory: one row per run (newest result of each name).
usage: scripts/latency-table.py <matrix dir> [name-prefix ...]
"Kept up": applied at least 98% of the offered rate in the steady window and e2e p50 under 500 ms (no backlog)."""
import glob, os, re, sys

d = sys.argv[1]
prefixes = sys.argv[2:]

def cell(rows, name, col):
    r = rows.get(name)
    return r[col] if r and len(r) > col else ""

out = []
for run in sorted(os.listdir(d)):
    p = os.path.join(d, run)
    if not os.path.isdir(p) or (prefixes and not any(run.startswith(x) for x in prefixes)):
        continue
    sums = sorted(glob.glob(os.path.join(p, "*", "summary.md")))
    if not sums:
        continue
    text = open(sums[-1]).read()
    m = re.search(r"offered ([\d.]+) ev/s", text)
    offered = float(m.group(1)) if m else 0
    rows = {}
    for line in text.splitlines():
        if line.startswith("| ") and line.count("|") >= 3:
            cells = [c.strip() for c in line.strip("|").split("|")]
            rows.setdefault(cells[0], cells)
    th = text.split("## Throughput", 1)[1].splitlines()[4] if "## Throughput" in text else ""
    tc = [c.strip() for c in th.strip("|").split("|")]
    applied = float(tc[3]) if len(tc) > 3 and tc[3] else 0
    e = rows.get("e2e", [])
    def ms(i):
        try:
            v = float(e[i]); return f"{v/1000:.1f} s" if v >= 1000 else f"{v:.0f} ms" if v >= 10 else f"{v:.1f} ms"
        except (IndexError, ValueError):
            return ""
    p50 = float(e[3]) if len(e) > 3 else 1e9
    kept = applied >= 0.98 * offered and p50 < 500
    db = ""
    for k in ("ctr_eb_postgres_cpu_pct", "ctr_eb_mssql_cpu_pct"):
        v = cell(rows, k, 1)
        if v and float(v) > 50:
            db = f"{float(v)/100:.1f}"
    fails = re.findall(r"^\| (\w+) \| fail \|", text, re.M)
    fails = [f for f in fails if f != "runtime_cross_source_blocking"]
    out.append((run, f"{offered/1000:.1f}k", f"{applied/1000:.1f}k", "yes" if kept else "**no**",
                ms(3), ms(4), ms(5), ms(6), db, f"{float(cell(rows, 'host_cpu_pct', 1) or 0):.0f}%",
                "correct" if not fails else "FAIL: " + ",".join(fails)))

print("| run | offered | applied | kept up | p50 | p90 | p99 | p99.9 | database cores | laptop CPU | oracle |")
print("|---|---:|---:|---|---:|---:|---:|---:|---:|---:|---|")
out.sort(key=lambda r: (r[0].rsplit("-", 1)[0], float(r[1][:-1])))
for r in out:
    print("| " + " | ".join(r) + " |")
