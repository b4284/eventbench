#!/usr/bin/env bash
# name role state lag (host) from the first Patroni REST API that answers
for a in 192.168.0.217:8008 192.168.0.217:8009 192.168.0.106:8008 192.168.0.106:8009; do
  c=$(curl -s --max-time 2 http://$a/cluster 2>/dev/null) && [[ -n $c ]] && {
    echo "$c" | python3 -c 'import json,sys; [print(m["name"], m["role"], m["state"], m.get("lag",""), m["host"]) for m in json.load(sys.stdin)["members"]]'; exit 0; }
done
exit 1
