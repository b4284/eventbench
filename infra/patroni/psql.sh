#!/usr/bin/env bash
# psql against the current Patroni leader (bench database), run inside whichever Patroni container is up.
STATE=~/.cache/eventbench/patroni
hosts=$(awk '{printf "127.0.0.1,"}' "$STATE/nodes" | sed 's/,$//'); ports=$(awk '{printf "%s,", $2}' "$STATE/nodes" | sed 's/,$//')
while read -r name pg api dc; do
  if [[ "$(podman inspect -f '{{.State.Status}}' eb-$name 2>/dev/null)" == running ]]; then
    exec podman exec -i -e PGPASSWORD=bench eb-$name psql \
      "host=$hosts port=$ports user=bench dbname=bench target_session_attrs=read-write connect_timeout=3" "$@"
  fi
done < "$STATE/nodes"
echo "no Patroni node running" >&2; exit 1
