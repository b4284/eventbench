#!/usr/bin/env bash
# Production-style fail-safe Redis: 1 primary + 2 replicas (async replication) + 3 Sentinels (quorum 2,
# automatic failover). Host networking so the addresses Sentinel advertises are reachable from the bench.
#   primary 127.0.0.1:6380, replicas :6381 :6382, sentinels :26380 :26381 :26382, master name "ebmaster"
#   bench connection string: "127.0.0.1:26380,127.0.0.1:26381,127.0.0.1:26382,serviceName=ebmaster"
# Every node: AOF everysec (best effort), no RDB. min-replicas-to-write 1 / max-lag 10: the primary refuses
# writes when no replica is connected (bounds what a failover can lose to ~10 s of replication lag; the
# client-side WAIT option bounds it to 0 acked writes).
# Tunables: REDIS_IMAGE, REDIS_IO_THREADS, REDIS_MIN_REPLICAS (0 disables the write fence)
set -euo pipefail
IMAGE=${REDIS_IMAGE:-docker.io/library/redis:8}
IO_THREADS=${REDIS_IO_THREADS:-4}
MIN_REPLICAS=${REDIS_MIN_REPLICAS:-1}
common=(--appendonly yes --appendfsync everysec --save "" --io-threads "$IO_THREADS" --maxclients 20000
        --protected-mode no --lazyfree-lazy-user-del yes --repl-backlog-size 512mb
        --client-output-buffer-limit "replica 2gb 1gb 60" --replica-announce-ip 127.0.0.1
        --min-replicas-to-write "$MIN_REPLICAS" --min-replicas-max-lag 10)

for port in 6380 6381 6382; do
  name=eb-redis-$port
  podman container exists "$name" && { echo "$name exists (run infra/redis-ha/down.sh first)"; exit 0; }
  extra=(); [[ $port != 6380 ]] && extra=(--replicaof 127.0.0.1 6380)
  podman run -d --name "$name" --network host "$IMAGE" redis-server --port "$port" "${common[@]}" "${extra[@]}" >/dev/null
done
for port in 6380 6381 6382; do
  for _ in $(seq 1 30); do [[ "$(podman exec eb-redis-$port redis-cli -p $port ping 2>/dev/null)" == PONG ]] && break; sleep 1; done
done

for port in 26380 26381 26382; do
  name=eb-sentinel-$port
  podman run -d --name "$name" --network host --entrypoint sh "$IMAGE" -c "
    printf 'port $port\nsentinel announce-ip 127.0.0.1\nsentinel resolve-hostnames yes\nsentinel monitor ebmaster 127.0.0.1 6380 2\nsentinel down-after-milliseconds ebmaster 5000\nsentinel failover-timeout ebmaster 30000\nsentinel parallel-syncs ebmaster 1\n' > /tmp/s.conf
    exec redis-sentinel /tmp/s.conf" >/dev/null
done
sleep 3
podman exec eb-redis-6380 redis-cli -p 6380 info replication | grep -E "^(role|connected_slaves|slave[0-9])" | tr -d '\r'
podman exec eb-sentinel-26380 redis-cli -p 26380 sentinel get-master-addr-by-name ebmaster | tr '\n' ' '; echo
