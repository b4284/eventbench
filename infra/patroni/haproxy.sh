#!/usr/bin/env bash
# A leader-following endpoint in front of the Patroni cluster, as production setups do: HAProxy on 127.0.0.1:6440
# health-checks every node's Patroni REST API (/primary answers 200 only on the leader) and sends connections to
# the leader; sessions to a node that stops being the leader are cut at once (shutdown-sessions).
# Clients then use one host: "host=127.0.0.1 port=6440".
# Like a cloud network load balancer (B30): HAPROXY_INTER=5s HAPROXY_FALL=2 HAPROXY_CUT=0 (checks every 5 s, and
# established sessions to a node that stops being the leader are left open, as NLBs do by default).
set -euo pipefail
STATE=~/.cache/eventbench/patroni
{
  cat <<CFG
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
    default-server inter ${HAPROXY_INTER:-1s} fall ${HAPROXY_FALL:-2} rise ${HAPROXY_RISE:-2}$([[ ${HAPROXY_CUT:-1} == 1 ]] && echo ' on-marked-down shutdown-sessions')
CFG
  while read -r name pg api dc; do echo "    server $name 127.0.0.1:$pg check port $api"; done < "$STATE/nodes"
} > "$STATE/haproxy.cfg"
podman rm -f eb-haproxy >/dev/null 2>&1 || true
podman run -d --name eb-haproxy --network host -v "$STATE/haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null
sleep 3
podman logs eb-haproxy 2>&1 | tail -3
