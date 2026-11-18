#!/usr/bin/env bash
# Stops and removes the eb-mongo container (data is not persisted between runs).
set -euo pipefail
NAME=${MONGO_CONTAINER:-eb-mongo}
podman rm -f "$NAME" >/dev/null 2>&1 && echo "removed container $NAME" || echo "container $NAME not found"
