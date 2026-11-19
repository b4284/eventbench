#!/usr/bin/env bash
# Production-style NATS JetStream: 3 servers in one cluster (Raft), host network.
#   clients nats://127.0.0.1:4222,4223,4224   routes :6222-6224   monitoring :8222-8224
#   Streams created with Broker:Replicas=3 (R3): a publish is acked after a Raft quorum (2 of 3) has it.
# Durability: default sync_interval (2 min, best effort); replication covers single-node loss.
set -euo pipefail
IMAGE=${NATS_IMAGE:-docker.io/library/nats:2.12-alpine}
DIR="$HOME/.cache/eventbench/nats-cluster"; mkdir -p "$DIR"
ROUTES="nats-route://127.0.0.1:6222,nats-route://127.0.0.1:6223,nats-route://127.0.0.1:6224"
for i in 1 2 3; do
  name=eb-nats-$i
  podman container exists "$name" && { echo "$name exists (run infra/nats-cluster/down.sh first)"; exit 0; }
  cat > "$DIR/n$i.conf" <<CONF
server_name: n$i
listen: 127.0.0.1:$((4221 + i))
http: 127.0.0.1:$((8221 + i))
max_payload: 64MB
max_pending: 512MB
write_deadline: "30s"
cluster {
  name: eb
  listen: 127.0.0.1:$((6221 + i))
  routes: [ $(echo "$ROUTES" | sed 's/,/, /g') ]
}
jetstream {
  store_dir: "/data"
  max_file_store: 50GB
  max_memory_store: 2GB
}
CONF
  podman run -d --name "$name" --network host -v "$DIR/n$i.conf:/etc/nats/n.conf:ro,Z" "$IMAGE" -c /etc/nats/n.conf >/dev/null
done
echo -n "waiting for the cluster"
for _ in $(seq 1 60); do
  ok=0; for p in 8222 8223 8224; do curl -fsS "http://127.0.0.1:$p/healthz?js-enabled-only=true" >/dev/null 2>&1 && ok=$((ok+1)); done
  [[ $ok == 3 ]] && { echo " ok"; break; }; echo -n "."; sleep 1
done
curl -s http://127.0.0.1:8222/jsz | python3 -c 'import json,sys; d=json.load(sys.stdin); print("meta leader", d.get("meta_cluster",{}).get("leader"), "cluster size", d.get("meta_cluster",{}).get("cluster_size"))'
