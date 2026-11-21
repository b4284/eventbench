#!/usr/bin/env bash
# A leader-following endpoint in front of the Patroni cluster, as production setups do: HAProxy on 127.0.0.1:6440
# health-checks every node's Patroni REST API (/primary answers 200 only on the leader) and sends connections to
# the leader; sessions to a node that stops being the leader are cut at once (shutdown-sessions).
# Clients then use one host: "host=127.0.0.1 port=6440".
set -euo pipefail
STATE=~/.cache/eventbench/patroni
{
  cat <<'CFG'
global
    maxconn 8000
defaults
    mode tcp
    timeout connect 3s
    timeout client 30m
    timeout server 30m
listen primary
    bind 127.0.0.1:6440
    option httpchk GET /primary
    http-check expect status 200
    default-server inter 1s fall 2 rise 2 on-marked-down shutdown-sessions
CFG
  while read -r name pg api dc; do echo "    server $name 127.0.0.1:$pg check port $api"; done < "$STATE/nodes"
} > "$STATE/haproxy.cfg"
podman rm -f eb-haproxy >/dev/null 2>&1 || true
podman run -d --name eb-haproxy --network host -v "$STATE/haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null
sleep 3
podman logs eb-haproxy 2>&1 | tail -3
