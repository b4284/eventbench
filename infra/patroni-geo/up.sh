#!/usr/bin/env bash
# The 2 + 2 Patroni cluster across three machines (backlog B5, B20): real network hops between the sites.
#   DC1  laptop 192.168.0.217: geo-dc1a (pg 5441, api 8008), geo-dc1b (5442, 8009), etcd-dc1   [podman, host network]
#   DC2  m4mini 192.168.0.106: dc2a (5441, 8008), dc2b (5442, 8009), etcd-dc2                   [native, ~/octolab]
#   witness m1mini 192.168.0.95: etcd-w                                                         [native, ~/octolab]
# Locale C everywhere: standbys on macOS replicate a Linux primary, and text sort order must not differ.
# Clients: HAProxy on the laptop, 127.0.0.1:6440 -> the leader, wherever it is.
set -euo pipefail
cd "$(dirname "$0")"
L=192.168.0.217; M4=192.168.0.106; M1=192.168.0.95
IMAGE=${PATRONI_IMAGE:-localhost/eb-patroni:latest}
STATE=~/.cache/eventbench/patroni-geo
mkdir -p "$STATE"
CLUSTER="etcd-dc1=http://$L:2380,etcd-dc2=http://$M4:2380,etcd-w=http://$M1:2380"
etcd_args() { echo "--name $1 --listen-client-urls http://0.0.0.0:2379 --advertise-client-urls http://$2:2379 --listen-peer-urls http://0.0.0.0:2380 --initial-advertise-peer-urls http://$2:2380 --initial-cluster $CLUSTER --initial-cluster-state new --initial-cluster-token eb-geo"; }
# etcd: DC1 in a container, DC2 and the witness as processes on the Macs
podman container exists geo-etcd-dc1 || podman run -d --name geo-etcd-dc1 --network host quay.io/coreos/etcd:v3.5.17 \
  /usr/local/bin/etcd --data-dir /tmp/etcd $(etcd_args etcd-dc1 $L) >/dev/null
for m in "m4mini etcd-dc2 $M4" "m1mini etcd-w $M1"; do set -- $m
  # (pid files, not pgrep -f: over ssh the pattern is part of the remote shell's own command line)
  ssh -o BatchMode=yes $1 "cd ~/octolab; if [ -f $2.pid ] && kill -0 \$(cat $2.pid) 2>/dev/null; then :; else mkdir -p data; nohup ~/octolab/etcd35/etcd-v3.5.17-darwin-arm64/etcd --data-dir ~/octolab/data/$2 $(etcd_args $2 $3) > $2.log 2>&1 & echo \$! > $2.pid; fi"
done
sleep 3
# Patroni: DC1 first (it bootstraps the cluster), then DC2
cat > "$STATE/post_bootstrap.sh" <<'SH'
#!/bin/sh
psql "$1" -v ON_ERROR_STOP=1 -c "CREATE ROLE bench LOGIN SUPERUSER PASSWORD 'bench'" -c "CREATE DATABASE bench OWNER bench"
SH
chmod +x "$STATE/post_bootstrap.sh"
render() {  # name ip pg api dc data bin post
  sed -e "s|@NAME@|$1|g; s|@IP@|$2|g; s|@PG@|$3|g; s|@API@|$4|g; s|@DC@|$5|g; s|@DATA@|$6|g; s|@BIN@|$7|g; s|@POST@|$8|g" patroni.yml.in
}
for n in "dc1a 5441 8008" "dc1b 5442 8009"; do set -- $n
  render geo-$1 $L $2 $3 dc1 /var/lib/postgresql/patroni/data /usr/lib/postgresql/18/bin /etc/patroni/post_bootstrap.sh > "$STATE/$1.yml"
  podman container exists geo-$1 || podman run -d --name geo-$1 --network host --shm-size=1g --user postgres \
    -v "$STATE:/etc/patroni:ro,Z" -v geo-$1-data:/var/lib/postgresql/patroni:U "$IMAGE" /etc/patroni/$1.yml >/dev/null
  [[ $1 == dc1a ]] && sleep 10
done
for n in "dc2a 5441 8008" "dc2b 5442 8009"; do set -- $n
  render $1 $M4 $2 $3 dc2 '~/octolab/data/'$1 '~/octolab/env/bin' /bin/true > "$STATE/$1.yml"
  scp -q "$STATE/$1.yml" m4mini:octolab/$1.yml
  ssh -o BatchMode=yes m4mini "cd ~/octolab; sed -i '' \"s|~/|\$HOME/|g\" $1.yml; mkdir -p data && chmod 700 data"
  # macOS 15 Local Network privacy: a process that outlives its ssh session gets "No route to host" for every other
  # LAN address. So Patroni (and the Postgres it starts) stays attached to an ssh session held open from here;
  # its log streams back to $STATE/<node>.log. Killing that ssh (or the laptop losing m4mini) ends the node.
  if [[ -f "$STATE/$1.ssh.pid" ]] && kill -0 "$(cat "$STATE/$1.ssh.pid")" 2>/dev/null; then :; else
    setsid nohup ssh -n -o BatchMode=yes -o ServerAliveInterval=5 -o ServerAliveCountMax=3 m4mini \
      "exec ~/octolab/env/bin/patroni ~/octolab/$1.yml" >> "$STATE/$1.log" 2>&1 < /dev/null &
    echo $! > "$STATE/$1.ssh.pid"
  fi
done
# HAProxy: the leader, wherever it is
{
  cat <<CFG
global
    maxconn 8000
defaults
    mode tcp
    timeout connect 3s
    timeout client 30m
    timeout server 30m
listen primary
    bind 127.0.0.1:6440
    option httpchk GET /primary
    http-check expect status 200
    default-server inter 1s fall 2 rise 1 on-marked-down shutdown-sessions
    server geo-dc1a $L:5441 check port 8008
    server geo-dc1b $L:5442 check port 8009
    server dc2a $M4:5441 check port 8008
    server dc2b $M4:5442 check port 8009
CFG
} > "$STATE/haproxy.cfg"
podman container exists geo-haproxy || podman run -d --name geo-haproxy --network host \
  -v "$STATE/haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg:ro,Z" docker.io/library/haproxy:2.9 >/dev/null
echo -n "waiting for a leader and 3 streaming standbys"
for _ in $(seq 1 240); do
  if ./status.sh 2>/dev/null | awk '$2=="leader"{l=1} $2!="leader" && $3=="streaming"{s++} END{exit !(l && s>=3)}'; then echo " ok"; break; fi
  echo -n "."; sleep 3
done
./status.sh
