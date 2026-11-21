#!/usr/bin/env python3
"""Sum the last 'stats' line of each dispatcher log (ANSI colours stripped). usage: octopus-stats.py <log>..."""
import re, sys
tot = {}
for f in sys.argv[1:]:
    last = None
    for line in open(f, errors='replace'):
        line = re.sub(r'\x1b\[[0-9;]*m', '', line)
        if ' stats ' in line:
            last = line
    if last:
        for k, v in re.findall(r'(\w+)=(\d+)', last):
            tot[k] = tot.get(k, 0) + int(v)
keys = ['published', 'applied', 'offered_hint', 'offered_pending', 'offered_read', 'offered_key', 'fetches', 'reads_dirty', 'found_dirty', 'reads_fallback', 'found_fallback',
        'reads_probe', 'found_probe', 'key_fetches', 'key_found']
print(' '.join(f'{k}={tot.get(k, 0)}' for k in keys))
for r in ('dirty', 'fallback', 'probe'):
    n = tot.get(f'reads_{r}', 0)
    if n: print(f'  {r}: {n} group reads, {100 * tot.get(f"found_{r}", 0) / n:.0f}% found an event')
if tot.get('key_fetches'): print(f"  key lookups: {tot['key_fetches']}, {100 * tot['key_found'] / tot['key_fetches']:.0f}% found")
