#!/usr/bin/env python3
"""Averages of the lab-side CPU samples written by scripts/remote-run.sh (middle 60% of the run)."""
import sys, collections
rows = [l.rstrip('\n').split('\t') for l in open(sys.argv[1]) if l.count('\t') == 2]
ts = sorted({int(r[0]) for r in rows})
if len(ts) < 5:
    sys.exit("too few samples")
lo, hi = ts[len(ts) // 5], ts[len(ts) * 4 // 5]
by = collections.defaultdict(list)
for t, n, v in rows:
    if lo <= int(t) <= hi:
        try: by[n].append((int(t), float(v)))
        except ValueError: pass
out = {}
for n, vs in by.items():
    if n.endswith('_bytes'):
        vs.sort(); out[n[:-6] + ' (MiB/s)'] = (vs[-1][1] - vs[0][1]) / 2**20 / max(1, vs[-1][0] - vs[0][0])
    elif n.endswith('_ticks'):
        vs.sort(); out[n[:-6] + ' (cores)'] = (vs[-1][1] - vs[0][1]) / 100 / max(1, vs[-1][0] - vs[0][0])
    elif n == 'host_pct':
        out['host cpu %'] = sum(v for _, v in vs) / len(vs)
    else:
        out[n + ' (cores)'] = sum(v for _, v in vs) / len(vs) / 100
for k in sorted(out):
    print(f"{k:28} {out[k]:6.2f}")
