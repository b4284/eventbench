#!/usr/bin/env bash
# Stops and removes the single-node Kafka container (and its data). Leaves the `eventbench` pod alone.
set -euo pipefail
NAME=${KAFKA_CONTAINER:-eb-kafka}
if podman container exists "$NAME"; then
  podman rm -f "$NAME" >/dev/null && echo "removed $NAME"
else
  echo "container $NAME does not exist"
fi
