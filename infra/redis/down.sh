#!/usr/bin/env bash
set -euo pipefail
podman rm -f "${REDIS_CONTAINER:-eb-redis}" >/dev/null 2>&1 && echo "removed eb-redis" || echo "eb-redis not running"
