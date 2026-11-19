#!/usr/bin/env bash
# Starts Redis 8 for the benchmark (broker: Redis Streams; store: keys + Lua).
#   container eb-redis, 127.0.0.1:6379, no auth
# Persistence: AOF only (no RDB fork snapshots). REDIS_APPENDFSYNC=everysec (default, best effort:
# fsync once a second, lose <= ~1 s on an OS crash) | always (fsync before replying) | no.
# Tunables via env: REDIS_IMAGE, REDIS_PORT, REDIS_IO_THREADS, REDIS_CPUS
set -euo pipefail
NAME=${REDIS_CONTAINER:-eb-redis}
IMAGE=${REDIS_IMAGE:-docker.io/library/redis:8}
PORT=${REDIS_PORT:-6379}
FSYNC=${REDIS_APPENDFSYNC:-everysec}
IO_THREADS=${REDIS_IO_THREADS:-4}
REDIS_CPUS=${REDIS_CPUS:-}
limits=(); [[ -n "$REDIS_CPUS" ]] && limits+=(--cpus "$REDIS_CPUS")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/redis/down.sh first to recreate)"; exit 0
fi
# Networking: EB_NETWORK=host (default) runs on the host network. Rootless podman's published ports go
# through a single-threaded userspace proxy (pasta) that capped Postgres paths at ~19k ev/s. EB_NETWORK=pasta
# restores published ports.
NETMODE=${EB_NETWORK:-host}
ports=(-p "127.0.0.1:${PORT}:6379"); [[ "$NETMODE" == host ]] && ports=(--network host)
podman run -d --name "$NAME" "${limits[@]}" "${ports[@]}" "$IMAGE" redis-server --port 6379 \
  --appendonly yes --appendfsync "$FSYNC" --save "" --io-threads "$IO_THREADS" \
  --maxclients 20000 --protected-mode no --lazyfree-lazy-user-del yes >/dev/null
for _ in $(seq 1 30); do
  [[ "$(podman exec "$NAME" redis-cli ping 2>/dev/null)" == "PONG" ]] && break; sleep 1
done
podman exec "$NAME" redis-cli info server | grep -E "^redis_version" | tr -d '\r'
echo "appendfsync: $(podman exec "$NAME" redis-cli config get appendfsync | tail -1)"
