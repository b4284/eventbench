#!/usr/bin/env bash
# One line per member: name role state lag (asks the first REST API that answers).
STATE=~/.cache/eventbench/patroni
while read -r name pg api dc; do
  c=$(curl -s --max-time 2 127.0.0.1:$api/cluster 2>/dev/null) && [[ -n $c ]] && {
    echo "$c" | python3 -c 'import json,sys; [print(m["name"], m["role"], m["state"], m.get("lag", "")) for m in json.load(sys.stdin)["members"]]'
    exit 0; }
done < "$STATE/nodes"
exit 1
