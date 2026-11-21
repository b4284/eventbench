#!/usr/bin/env python3
import csv, sys, glob, statistics
# usage: outage.py <run-dir>...   longest stretch of the run phase with applied/s under 10% of its 90th percentile
for d in sys.argv[1:]:
    f = sorted(glob.glob(d + '/*/timeseries.csv'))
    if not f: continue
    rows = [r for r in csv.DictReader(open(f[-1])) if r['phase'] == 'run']
    a = [(float(r['t_s']), float(r['applied_per_s'] or 0)) for r in rows]
    vs = sorted(v for _, v in a); med = vs[int(0.9 * (len(vs) - 1))]
    best = cur = 0; start = None; at = None
    for t, v in a:
        if v < 0.1 * med:
            if start is None: start = t
            cur = t - start + 1
            if cur > best: best, at = cur, start
        else:
            start = None
    print(f"{d.rstrip('/').split('/')[-1]}: outage {best:.0f} s (from t={at}), p90 applied {med:.0f}/s")
