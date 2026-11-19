#!/usr/bin/env bash
# Starts MongoDB 8.0 as a single-node replica set (rs0) — transactions need a replica set.
#   container eb-mongo, 127.0.0.1:27017
#   connection string: mongodb://127.0.0.1:27017/?directConnection=true   (or ?replicaSet=rs0)
# Journaling is always on in MongoDB 8; the store writes with w:majority, j:true.
# Tunables via env: MONGO_IMAGE, MONGO_PORT, MONGO_CACHE_GB, MONGO_CPUS, MONGO_MEMORY
set -euo pipefail

NAME=${MONGO_CONTAINER:-eb-mongo}
IMAGE=${MONGO_IMAGE:-docker.io/library/mongo:8.0}
PORT=${MONGO_PORT:-27017}
CACHE_GB=${MONGO_CACHE_GB:-2}
MONGO_CPUS=${MONGO_CPUS:-}       # e.g. 8  -> --cpus=8
MONGO_MEMORY=${MONGO_MEMORY:-}   # e.g. 8g -> --memory=8g

limits=()
[[ -n "$MONGO_CPUS" ]] && limits+=(--cpus "$MONGO_CPUS")
[[ -n "$MONGO_MEMORY" ]] && limits+=(--memory "$MONGO_MEMORY")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/mongo/down.sh first to recreate)"; exit 0
fi

# Networking: EB_NETWORK=host (default) runs on the host network. Rootless podman's published ports go
# through a single-threaded userspace proxy (pasta) that capped Postgres paths at ~19k ev/s. EB_NETWORK=pasta
# restores published ports.
NETMODE=${EB_NETWORK:-host}
ports=(-p "127.0.0.1:${PORT}:27017"); [[ "$NETMODE" == host ]] && ports=(--network host)
podman run -d --name "$NAME" "${limits[@]}" "${ports[@]}" \
  "$IMAGE" mongod --replSet rs0 --bind_ip_all --wiredTigerCacheSizeGB "$CACHE_GB" >/dev/null

echo -n "waiting for mongod"
for _ in $(seq 1 60); do
  if podman exec "$NAME" mongosh --quiet --eval 'db.adminCommand({ping: 1}).ok' >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done

# Member host is 127.0.0.1:27017 (valid inside the container; from the host use directConnection=true).
podman exec "$NAME" mongosh --quiet --eval '
  try { rs.status().ok } catch (e) {
    rs.initiate({ _id: "rs0", members: [{ _id: 0, host: "127.0.0.1:27017" }] }).ok
  }' >/dev/null

echo -n "waiting for primary"
for _ in $(seq 1 60); do
  if [[ "$(podman exec "$NAME" mongosh --quiet --eval 'db.hello().isWritablePrimary' 2>/dev/null)" == "true" ]]; then echo " ok"; break; fi
  echo -n "."; sleep 1
done
podman exec "$NAME" mongosh --quiet --eval 'print("mongodb " + db.version() + ", replica set " + rs.status().set + ", primary " + db.hello().isWritablePrimary)'
