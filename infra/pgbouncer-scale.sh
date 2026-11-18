#!/usr/bin/env bash
# Runs N PgBouncer processes on the same port (so_reuseport) inside the eventbench pod.
# PgBouncer is single-threaded; this spreads client connections across N cores.
#
# usage: infra/pgbouncer-scale.sh <processes> [default_pool_size per process]
#   infra/pgbouncer-scale.sh 4 48    # 4 processes x 48 server connections
#   infra/pgbouncer-scale.sh 1       # back to the single stock instance (infra/pgbouncer/pgbouncer.ini)
#
# Each process keeps its own pools, so the server-side total is N x (pool + reserve); it must stay
# below Postgres max_connections (300 by default, minus admin/stat connections).
# Note: the SHOW POOLS stats command reaches only one (random) process.
set -euo pipefail
cd "$(dirname "$0")"
N=${1:?processes}; POOL=${2:-}
POD=${POD:-eventbench}
PGB_IMAGE=${PGB_IMAGE:-docker.io/edoburu/pgbouncer:latest}
MAX_CONN=$(podman exec eb-postgres psql -U bench -d bench -Atc "show max_connections")

for c in $(podman ps -a --format '{{.Names}}' | grep -E '^eb-pgbouncer(-[0-9]+)?$'); do podman rm -f "$c" >/dev/null; done

if [[ "$N" == 1 && -z "$POOL" ]]; then
  CONF_DIR="$PWD/pgbouncer"
else
  CONF_DIR="$PWD/pgbouncer/scaled"
  mkdir -p "$CONF_DIR"
  cp pgbouncer/userlist.txt "$CONF_DIR/"
  sed -E -e "s/^default_pool_size = .*/default_pool_size = ${POOL:-64}/" pgbouncer/pgbouncer.ini > "$CONF_DIR/pgbouncer.ini"
  echo "so_reuseport = 1" >> "$CONF_DIR/pgbouncer.ini"
fi
pool=$(grep -E '^default_pool_size' "$CONF_DIR/pgbouncer.ini" | awk -F= '{gsub(/ /,"",$2);print $2}')
reserve=$(grep -E '^reserve_pool_size' "$CONF_DIR/pgbouncer.ini" | awk -F= '{gsub(/ /,"",$2);print $2}')
total=$(( N * (pool + reserve) ))
if (( total > MAX_CONN - 20 )); then
  echo "refusing: $N x ($pool + $reserve) = $total server connections > max_connections ($MAX_CONN) - 20" >&2; exit 1
fi

for i in $(seq 1 "$N"); do
  name=eb-pgbouncer; [[ $i -gt 1 ]] && name="eb-pgbouncer-$i"
  podman run -d --pod "$POD" --name "$name" -v "$CONF_DIR:/etc/pgbouncer:ro,Z" \
    --entrypoint /usr/bin/pgbouncer "$PGB_IMAGE" /etc/pgbouncer/pgbouncer.ini >/dev/null
done
for _ in $(seq 1 30); do
  podman exec eb-postgres psql "host=127.0.0.1 port=6432 dbname=bench user=bench password=bench" -Atc "select 1" >/dev/null 2>&1 && break; sleep 1
done
echo "pgbouncer: $N process(es), default_pool_size=$pool, reserve=$reserve -> up to $total server connections (max_connections $MAX_CONN)"
