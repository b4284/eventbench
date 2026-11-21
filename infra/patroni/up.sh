#!/usr/bin/env bash
# Patroni-managed Postgres for failover drills (backlog B6, B28). No manual steps: promotion, fencing, rejoin.
#
# PATRONI_LAYOUT=1+1 (default): pat1, pat2; one etcd; strict synchronous mode (one standby must confirm).
# PATRONI_LAYOUT=2+2: dc1a, dc1b (DC1) and dc2a, dc2b (DC2); etcd members etcd-dc1, etcd-dc2, etcd-w (witness).
#   Quorum mode, synchronous_node_count 2: a commit waits for ANY 2 of the 3 standbys, so it always reaches the
#   other DC. After losing a node or a DC, Patroni lowers the quorum to the standbys still alive; writes go on.
#
# PATRONI_TTL / PATRONI_LOOP / PATRONI_RETRY: leader lease, loop and retry (default 20 / 5 / 5 s).
# Node i (1-based): Postgres on 5440+i, REST API on 8007+i. The node list is written to $STATE/nodes
# ("name pgport apiport dc" per line) for psql.sh, leader.sh, heal.sh and the drills.
# Clients: OCTOPUS_PG_CONN / configs/overlay-patroni*.json (multi-host, target_session_attrs=read-write).
set -euo pipefail
cd "$(dirname "$0")"
IMAGE=${PATRONI_IMAGE:-localhost/eb-patroni:latest}
LAYOUT=${PATRONI_LAYOUT:-1+1}
STATE=~/.cache/eventbench/patroni
mkdir -p "$STATE"
case $LAYOUT in
  1+1) NODES="pat1:dc1 pat2:dc2"; ETCD="eb-etcd:2379:2380"; SYNC_MODE=true; SYNC_COUNT=1 ;;
  2+2) NODES="dc1a:dc1 dc1b:dc1 dc2a:dc2 dc2b:dc2"; ETCD="etcd-dc1:2379:2380 etcd-dc2:2381:2382 etcd-w:2383:2384"; SYNC_MODE=quorum; SYNC_COUNT=2 ;;
  *) echo "unknown PATRONI_LAYOUT $LAYOUT" >&2; exit 2 ;;
esac
# etcd: one member, or three forming one cluster
CLUSTER=$(for e in $ETCD; do IFS=: read -r n c p <<<"$e"; printf '%s=http://127.0.0.1:%s,' "$n" "$p"; done | sed 's/,$//')
HOSTS=$(for e in $ETCD; do IFS=: read -r n c p <<<"$e"; printf '127.0.0.1:%s,' "$c"; done | sed 's/,$//')
for e in $ETCD; do
  IFS=: read -r n c p <<<"$e"
  podman container exists eb-$n && continue
  podman run -d --name eb-$n --network host quay.io/coreos/etcd:${ETCD_IMAGE_TAG:-v3.5.17} /usr/local/bin/etcd --name $n \
    --data-dir /tmp/etcd --listen-client-urls http://127.0.0.1:$c --advertise-client-urls http://127.0.0.1:$c \
    --listen-peer-urls http://127.0.0.1:$p --initial-advertise-peer-urls http://127.0.0.1:$p \
    --initial-cluster "$CLUSTER" --initial-cluster-state new --initial-cluster-token eb-$LAYOUT >/dev/null
done
cat > "$STATE/post_bootstrap.sh" <<'SH'
#!/bin/sh
psql "$1" -v ON_ERROR_STOP=1 -c "CREATE ROLE bench LOGIN SUPERUSER PASSWORD 'bench'" -c "CREATE DATABASE bench OWNER bench" \
  -c "CREATE EXTENSION IF NOT EXISTS pg_stat_statements"
psql "$(echo "$1" | sed 's/dbname=[^ ]*/dbname=bench/')" -c "CREATE EXTENSION IF NOT EXISTS pg_stat_statements" || true
SH
chmod +x "$STATE/post_bootstrap.sh"
: > "$STATE/nodes"
i=0
for nd in $NODES; do
  i=$((i + 1)); name=${nd%%:*}; dc=${nd##*:}; pg=$((5440 + i)); api=$((8007 + i))
  echo "$name $pg $api $dc" >> "$STATE/nodes"
  sed -e "s/@NAME@/$name/g" -e "s/@PG@/$pg/g" -e "s/@API@/$api/g" -e "s/@ETCD@/$HOSTS/g" -e "s/@DC@/$dc/g" \
      -e "s/@TTL@/${PATRONI_TTL:-20}/g" -e "s/@LOOP@/${PATRONI_LOOP:-5}/g" -e "s/@RETRY@/${PATRONI_RETRY:-5}/g" -e "s/@SYNC_MODE@/$SYNC_MODE/g" -e "s/@SYNC_COUNT@/$SYNC_COUNT/g" -e "s/@SCOPE@/octopus-lab-${LAYOUT/+/x}/g" \
      patroni.yml.in > "$STATE/$name.yml"
  podman container exists eb-$name && continue
  podman run -d --name eb-$name --network host --shm-size=1g --user postgres \
    -v "$STATE:/etc/patroni:ro,Z" -v eb-$name-data:/var/lib/postgresql/patroni:U "$IMAGE" /etc/patroni/$name.yml >/dev/null
  [[ $i -eq 1 ]] && sleep 8   # let the first one bootstrap the cluster
done
echo "$LAYOUT" > "$STATE/layout"
want=$(( $(wc -l < "$STATE/nodes") - 1 ))
echo -n "waiting for a leader and $want streaming standbys"
for _ in $(seq 1 180); do
  if ./status.sh 2>/dev/null | awk -v w="$want" '$2=="leader"{l=1} $2!="leader" && $3=="streaming"{s++} END{exit !(l && s>=w)}'; then echo " ok"; break; fi
  echo -n "."; sleep 2
done
./status.sh
