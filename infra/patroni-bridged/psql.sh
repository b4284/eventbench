#!/usr/bin/env bash
# psql against the leader (bench database) through the WAN-side HAProxy, run inside a node container.
for n in dc2a dc2b dc1a dc1b; do
  [[ "$(podman inspect -f '{{.State.Status}}' eb-b-$n 2>/dev/null)" == running ]] && \
    exec podman exec -i -e PGPASSWORD=bench eb-b-$n psql "host=eb-b-haproxy-wan port=6432 user=bench dbname=bench connect_timeout=3" "$@"
done
echo "no node running" >&2; exit 1
