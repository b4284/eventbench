#!/usr/bin/env bash
# Prints the current leader's name (pat1 or pat2), asking any node's REST API.
for p in 8008 8009; do
  l=$(curl -s --max-time 2 127.0.0.1:$p/cluster | python3 -c 'import json,sys; print(next(m["name"] for m in json.load(sys.stdin)["members"] if m["role"]=="leader"))' 2>/dev/null) && [[ -n "$l" ]] && { echo "$l"; exit 0; }
done
exit 1
