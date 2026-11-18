#!/usr/bin/env bash
# Stops and removes the NATS container and its JetStream volume (data is not persisted between runs).
set -euo pipefail
NAME=${NATS_CONTAINER:-eb-nats}
VOLUME=${NATS_VOLUME:-eb-nats-data}
podman rm -f "$NAME" >/dev/null 2>&1 && echo "removed container $NAME" || echo "container $NAME not found"
podman volume rm -f "$VOLUME" >/dev/null 2>&1 && echo "removed volume $VOLUME" || true
