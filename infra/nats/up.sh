#!/usr/bin/env bash
# Starts a single NATS server with JetStream (file store in a container volume).
#   client     : 127.0.0.1:4222
#   monitoring : http://127.0.0.1:8222  (/varz, /jsz?streams=true&consumers=true)
# Tunables via env: NATS_IMAGE, NATS_SYNC_INTERVAL (2m = server default, "always" = fsync per write),
#                   NATS_CPUS, NATS_MEMORY
set -euo pipefail
cd "$(dirname "$0")"

NAME=${NATS_CONTAINER:-eb-nats}
VOLUME=${NATS_VOLUME:-eb-nats-data}
NATS_IMAGE=${NATS_IMAGE:-docker.io/library/nats:2.12-alpine}
NATS_SYNC_INTERVAL=${NATS_SYNC_INTERVAL:-2m}
NATS_CPUS=${NATS_CPUS:-}
NATS_MEMORY=${NATS_MEMORY:-}

limits=()
[[ -n "$NATS_CPUS" ]] && limits+=(--cpus "$NATS_CPUS")
[[ -n "$NATS_MEMORY" ]] && limits+=(--memory "$NATS_MEMORY")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/nats/down.sh first to recreate)"; exit 0
fi

podman volume exists "$VOLUME" || podman volume create "$VOLUME" >/dev/null

# Render the config outside the repo (a quoted duration: an unquoted 2m would parse as "2 million").
CONF="${TMPDIR:-/tmp}/eb-nats-$(id -u).conf"
sed "s|__SYNC_INTERVAL__|$NATS_SYNC_INTERVAL|" nats-server.conf > "$CONF"

podman run -d --name "$NAME" "${limits[@]}" \
  -p 4222:4222 -p 8222:8222 \
  -v "$CONF:/etc/nats/eventbench.conf:ro,Z" \
  -v "$VOLUME:/data" \
  "$NATS_IMAGE" -js -c /etc/nats/eventbench.conf >/dev/null

echo -n "waiting for nats"
for _ in $(seq 1 30); do
  if curl -fsS "http://127.0.0.1:8222/healthz?js-enabled-only=true" >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done
curl -fsS http://127.0.0.1:8222/varz | python3 -c \
  'import json,sys; v=json.load(sys.stdin); print("nats-server", v["version"], "max_payload", v["max_payload"], "jetstream", "yes" if v.get("jetstream") else "no", "sync", "always" if v.get("jetstream",{}).get("config",{}).get("sync_always") else str(v.get("jetstream",{}).get("config",{}).get("sync_interval",0)//10**9)+"s")' \
  || true
podman ps --filter name="^$NAME\$" --format "table {{.Names}}\t{{.Status}}\t{{.Image}}"
