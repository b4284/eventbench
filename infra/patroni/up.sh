#!/usr/bin/env bash
# Patroni-managed Postgres pair for failover drills (backlog B6): automatic promotion, fencing, no manual steps.
#   etcd (one node, standing in for the witness site): 127.0.0.1:2379
#   pat1 / pat2: Postgres on 5441 / 5442, Patroni REST API on 8008 / 8009
#   strict synchronous mode: a primary commits only with its synchronous standby (no acknowledged write can be
#   lost by a failover, and an isolated old primary cannot acknowledge writes)
#   leader lease (ttl) 20 s, loop 5 s: production-like detection time
# Clients: "host=127.0.0.1,127.0.0.1 port=5441,5442 ... target_session_attrs=read-write"
# Status: curl -s 127.0.0.1:8008/cluster
set -euo pipefail
cd "$(dirname "$0")"
IMAGE=${PATRONI_IMAGE:-localhost/eb-patroni:latest}
STATE=~/.cache/eventbench/patroni
mkdir -p "$STATE"
if ! podman container exists eb-etcd; then
  podman run -d --name eb-etcd --network host quay.io/coreos/etcd:v3.5.9 /usr/local/bin/etcd \
    --data-dir /tmp/etcd --listen-client-urls http://127.0.0.1:2379 --advertise-client-urls http://127.0.0.1:2379 \
    --listen-peer-urls http://127.0.0.1:2380 >/dev/null
fi
cat > "$STATE/post_bootstrap.sh" <<'SH'
#!/bin/sh
psql "$1" -v ON_ERROR_STOP=1 -c "CREATE ROLE bench LOGIN SUPERUSER PASSWORD 'bench'" -c "CREATE DATABASE bench OWNER bench"
SH
chmod +x "$STATE/post_bootstrap.sh"
for i in 1 2; do
  name=pat$i; pg=$((5440 + i)); api=$((8007 + i))
  sed -e "s/@NAME@/$name/g" -e "s/@PG@/$pg/g" -e "s/@API@/$api/g" patroni.yml.in > "$STATE/$name.yml"
  if ! podman container exists eb-$name; then
    podman run -d --name eb-$name --network host --shm-size=1g --user postgres \
      -v "$STATE:/etc/patroni:ro,Z" -v eb-$name-data:/var/lib/postgresql/patroni:U \
      "$IMAGE" /etc/patroni/$name.yml >/dev/null
  fi
done
echo -n "waiting for a leader and a synchronous standby"
for _ in $(seq 1 120); do
  c=$(curl -s 127.0.0.1:8008/cluster 2>/dev/null || true)
  if echo "$c" | grep -q '"role": *"leader"' && echo "$c" | grep -q '"role": *"sync_standby"' && echo "$c" | grep -q '"state": *"streaming"'; then echo " ok"; break; fi
  echo -n "."; sleep 2
done
curl -s 127.0.0.1:8008/cluster | python3 -c 'import json,sys; [print(m["name"], m["role"], m["state"], m.get("lag","")) for m in json.load(sys.stdin)["members"]]'
