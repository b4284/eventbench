#!/usr/bin/env bash
# psql against the current Patroni leader (bench database), run inside whichever Patroni container is up.
STATE=~/.cache/eventbench/patroni
hosts=$(awk '{printf "127.0.0.1,"}' "$STATE/nodes" | sed 's/,$//'); ports=$(awk '{printf "%s,", $2}' "$STATE/nodes" | sed 's/,$//')
# the registry is read on fd 3: psql must keep this script's stdin (SQL may be piped in)
while read -r name pg api dc <&3; do
  if [[ "$(podman inspect -f '{{.State.Status}}' eb-$name 2>/dev/null)" == running ]]; then
    exec podman exec -i -e PGPASSWORD=bench eb-$name psql \
      "host=$hosts port=$ports user=bench dbname=bench target_session_attrs=read-write connect_timeout=3" "$@"
  fi
done 3< "$STATE/nodes"
echo "no Patroni node running" >&2; exit 1
