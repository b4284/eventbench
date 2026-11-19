#!/usr/bin/env bash
# Kills the current primary (simulated node crash). Sentinel promotes a replica within ~5-10 s.
set -euo pipefail
addr=$(podman exec eb-sentinel-26380 redis-cli -p 26380 sentinel get-master-addr-by-name ebmaster | tail -1 | tr -d '\r')
echo "killing primary on port $addr"
podman kill "eb-redis-$addr" >/dev/null
