#!/usr/bin/env bash
# Starts a single-node RabbitMQ 4.x (with the management plugin) as a standalone podman container.
#   AMQP       : 127.0.0.1:5672   (user bench / bench)
#   Management : http://127.0.0.1:15672
# Independent of the Postgres pod (infra/up.sh); use both for the rabbitmq-postgres config.
# Tunables via env: RMQ_IMAGE, RMQ_NAME, RMQ_BIND, RMQ_CPUS, RMQ_MEMORY
set -euo pipefail
cd "$(dirname "$0")"

NAME=${RMQ_NAME:-eb-rabbitmq}
IMAGE=${RMQ_IMAGE:-docker.io/library/rabbitmq:4.4.1-management}
BIND=${RMQ_BIND:-127.0.0.1}
RMQ_CPUS=${RMQ_CPUS:-}       # e.g. 8  -> --cpus=8
RMQ_MEMORY=${RMQ_MEMORY:-}   # e.g. 8g -> --memory=8g

limits=()
[[ -n "$RMQ_CPUS" ]] && limits+=(--cpus "$RMQ_CPUS")
[[ -n "$RMQ_MEMORY" ]] && limits+=(--memory "$RMQ_MEMORY")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/rabbitmq/down.sh first to recreate)"; exit 0
fi

# A fixed hostname keeps the node name (rabbit@eb-rabbitmq) stable. CLI calls run as user rabbitmq:
# as root (HOME=/var/lib/rabbitmq) they would create .erlang.cookie with the wrong owner and crash the node.
podman run -d --name "$NAME" --hostname eb-rabbitmq "${limits[@]}" \
  -p "$BIND:5672:5672" -p "$BIND:15672:15672" \
  -v "$PWD/rabbitmq.conf:/etc/rabbitmq/conf.d/90-eventbench.conf:ro,Z" \
  -e RABBITMQ_SERVER_ADDITIONAL_ERL_ARGS="-rabbit quorum_tick_interval 1000" \
  "$IMAGE" >/dev/null

echo -n "waiting for rabbitmq"
for _ in $(seq 1 90); do
  if podman exec --user rabbitmq "$NAME" rabbitmq-diagnostics -q check_port_connectivity >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done
podman exec --user rabbitmq "$NAME" rabbitmq-diagnostics -q check_port_connectivity >/dev/null
echo "rabbitmq $(podman exec --user rabbitmq "$NAME" rabbitmq-diagnostics -q server_version)"
podman ps --filter name="$NAME" --format "table {{.Names}}\t{{.Status}}\t{{.Image}}"
