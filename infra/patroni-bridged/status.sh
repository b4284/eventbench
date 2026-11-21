#!/usr/bin/env bash
# name role state lag, asked from whichever node answers (inside its container).
for n in dc2a dc2b dc1a dc1b; do
  c=$(podman exec eb-b-$n curl -s --max-time 2 localhost:8008/cluster 2>/dev/null) && [[ -n $c ]] && {
    echo "$c" | python3 -c 'import json,sys; [print(m["name"], m["role"], m["state"], m.get("lag","")) for m in json.load(sys.stdin)["members"]]'; exit 0; }
done
exit 1
