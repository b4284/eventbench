#!/usr/bin/env bash
# The 2 + 2 Patroni cluster on per-DC container networks, so a DC can be cut off while it keeps running
# (backlog B27: a real partition between DCs). Rootless podman bridge networks with name resolution:
#   eb-b-dc1, eb-b-dc2: each DC's local network     eb-b-wan: the links between the DCs and the witness
#   dc1a, dc1b, etcd-dc1: on eb-b-dc1 + eb-b-wan     dc2a, dc2b, etcd-dc2: on eb-b-dc2 + eb-b-wan    etcd-w: eb-b-wan only
# Clients (host network) go through HAProxy containers with published ports:
#   127.0.0.1:6440  eb-b-haproxy-wan, on eb-b-wan: sees every node (clients outside DC1)
#   127.0.0.1:6441  eb-b-haproxy-dc1, on eb-b-dc1: sees DC1 only (clients inside DC1)
# Partition DC1: podman network disconnect eb-b-wan eb-b-dc1a eb-b-dc1b eb-b-etcd-dc1   (connect to heal)
set -euo pipefail
cd "$(dirname "$0")"
IMAGE=${PATRONI_IMAGE:-localhost/eb-patroni:latest}
STATE=~/.cache/eventbench/patroni-bridged
mkdir -p "$STATE"
for n in eb-b-dc1 eb-b-dc2 eb-b-wan; do podman network exists $n || podman network create $n >/dev/null; done
CLUSTER="eb-b-etcd-dc1=http://eb-b-etcd-dc1:2380,eb-b-etcd-dc2=http://eb-b-etcd-dc2:2380,eb-b-etcd-w=http://eb-b-etcd-w:2380"
for e in "etcd-dc1 eb-b-dc1" "etcd-dc2 eb-b-dc2" "etcd-w"; do
  set -- $e; n=eb-b-$1; nets="--network eb-b-wan"; [[ -n "${2:-}" ]] && nets="--network $2 $nets"
  podman container exists $n && continue
  podman run -d --name $n $nets quay.io/coreos/etcd:v3.5.17 /usr/local/bin/etcd --name $n --data-dir /tmp/etcd \
    --listen-client-urls http://0.0.0.0:2379 --advertise-client-urls http://$n:2379 \
    --listen-peer-urls http://0.0.0.0:2380 --initial-advertise-peer-urls http://$n:2380 \
    --initial-cluster "$CLUSTER" --initial-cluster-state new --initial-cluster-token eb-bridged >/dev/null
done
cat > "$STATE/post_bootstrap.sh" <<'SH'
#!/bin/sh
psql "$1" -v ON_ERROR_STOP=1 -c "CREATE ROLE bench LOGIN SUPERUSER PASSWORD 'bench'" -c "CREATE DATABASE bench OWNER bench"
SH
chmod +x "$STATE/post_bootstrap.sh"
i=0
for nd in dc1a:dc1 dc1b:dc1 dc2a:dc2 dc2b:dc2; do
  i=$((i + 1)); name=${nd%%:*}; dc=${nd##*:}
  sed -e "s/@NAME@/$name/g" -e "s/@DC@/$dc/g" patroni.yml.in > "$STATE/$name.yml"
  podman container exists eb-b-$name && continue
  podman run -d --name eb-b-$name --network eb-b-$dc --network eb-b-wan --shm-size=1g --user postgres \
    -v "$STATE:/etc/patroni:ro,Z" -v eb-b-$name-data:/var/lib/postgresql/patroni:U "$IMAGE" /etc/patroni/$name.yml >/dev/null
  [[ $i -eq 1 ]] && sleep 8
done
haproxy_cfg() {   # $1: servers
  cat <<CFG
global
    maxconn 8000
defaults
    mode tcp
    timeout connect 3s
    timeout client 30m
    timeout server 30m
listen primary
    bind 0.0.0.0:6432
    option httpchk GET /primary
    http-check expect status 200
    default-server inter 1s fall 2 rise 1 on-marked-down shutdown-sessions
CFG
  for s in $1; do echo "    server $s eb-b-$s:5432 check port 8008"; done
}
haproxy_cfg "dc1a dc1b dc2a dc2b" > "$STATE/haproxy-wan.cfg"
haproxy_cfg "dc1a dc1b" > "$STATE/haproxy-dc1.cfg"
podman container exists eb-b-haproxy-wan || podman run -d --name eb-b-haproxy-wan --network eb-b-wan -p 127.0.0.1:6440:6432 \
  -v "$STATE/haproxy-wan.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null
podman container exists eb-b-haproxy-dc1 || podman run -d --name eb-b-haproxy-dc1 --network eb-b-dc1 -p 127.0.0.1:6441:6432 \
  -v "$STATE/haproxy-dc1.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null
echo -n "waiting for a leader and 3 streaming standbys"
for _ in $(seq 1 180); do
  if ./status.sh 2>/dev/null | awk '$2=="leader"{l=1} $2!="leader" && $3=="streaming"{s++} END{exit !(l && s>=3)}'; then echo " ok"; break; fi
  echo -n "."; sleep 2
done
./status.sh
