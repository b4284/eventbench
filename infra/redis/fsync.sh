#!/usr/bin/env bash
# Switches Redis AOF fsync policy at runtime: everysec (best effort) | always (fsync before reply) | no
set -euo pipefail
podman exec "${REDIS_CONTAINER:-eb-redis}" redis-cli config set appendfsync "${1:?everysec|always|no}" >/dev/null
echo "appendfsync: $(podman exec "${REDIS_CONTAINER:-eb-redis}" redis-cli config get appendfsync | tail -1)"
