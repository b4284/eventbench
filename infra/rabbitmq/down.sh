#!/usr/bin/env bash
# Stops and removes the RabbitMQ container (and its anonymous data volume). Leaves the Postgres pod alone.
set -euo pipefail
NAME=${RMQ_NAME:-eb-rabbitmq}
if podman container exists "$NAME"; then
  podman rm -f -v "$NAME" >/dev/null
  echo "removed $NAME"
else
  echo "container $NAME does not exist"
fi
