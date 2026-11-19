#!/usr/bin/env bash
# Starts Postgres 18 + pgmq and PgBouncer in one podman pod (shared localhost).
#   Postgres  : 127.0.0.1:5432  (direct; used for DDL, oracle scans, server stats)
#   PgBouncer : 127.0.0.1:6432  (transaction pooling; used by producers and workers)
# Tunables via env: PG_IMAGE, PGB_IMAGE, PG_SHARED_BUFFERS, PG_MAX_CONNECTIONS, PG_SYNC_COMMIT, PG_CPUS, PG_MEMORY
set -euo pipefail
cd "$(dirname "$0")"
# Networking: EB_NETWORK=host (default) runs on the host network. Rootless podman's published ports go
# through a single-threaded userspace proxy (pasta) that capped Postgres paths at ~19k ev/s. EB_NETWORK=pasta
# restores published ports.
NETMODE=${EB_NETWORK:-host}

POD=${POD:-eventbench}
PG_IMAGE=${PG_IMAGE:-ghcr.io/pgmq/pg18-pgmq:latest}
PGB_IMAGE=${PGB_IMAGE:-docker.io/edoburu/pgbouncer:latest}
PG_SHARED_BUFFERS=${PG_SHARED_BUFFERS:-2GB}
PG_MAX_CONNECTIONS=${PG_MAX_CONNECTIONS:-300}
PG_SYNC_COMMIT=${PG_SYNC_COMMIT:-on}
PG_CPUS=${PG_CPUS:-}       # e.g. 8  -> --cpus=8
PG_MEMORY=${PG_MEMORY:-}   # e.g. 8g -> --memory=8g

limits=()
[[ -n "$PG_CPUS" ]] && limits+=(--cpus "$PG_CPUS")
[[ -n "$PG_MEMORY" ]] && limits+=(--memory "$PG_MEMORY")

if podman pod exists "$POD"; then
  echo "pod $POD already exists (run infra/down.sh first to recreate)"; exit 0
fi

if [[ "$NETMODE" == host ]]; then podman pod create --name "$POD" --network host >/dev/null
else podman pod create --name "$POD" -p 5432:5432 -p 6432:6432 >/dev/null; fi

podman run -d --pod "$POD" --name eb-postgres "${limits[@]}" \
  --shm-size=1g \
  -e POSTGRES_USER=bench -e POSTGRES_PASSWORD=bench -e POSTGRES_DB=bench \
  "$PG_IMAGE" \
  postgres \
    -c max_connections="$PG_MAX_CONNECTIONS" \
    -c shared_buffers="$PG_SHARED_BUFFERS" \
    -c effective_cache_size=6GB \
    -c work_mem=16MB \
    -c maintenance_work_mem=512MB \
    -c synchronous_commit="$PG_SYNC_COMMIT" \
    -c wal_level=replica \
    -c wal_compression=lz4 \
    -c max_wal_size=8GB \
    -c min_wal_size=1GB \
    -c checkpoint_timeout=15min \
    -c checkpoint_completion_target=0.9 \
    -c random_page_cost=1.1 \
    -c autovacuum_naptime=10s \
    -c autovacuum_vacuum_scale_factor=0.05 \
    -c autovacuum_vacuum_cost_limit=2000 \
    -c shared_preload_libraries=pg_stat_statements \
    -c track_io_timing=on >/dev/null

echo -n "waiting for postgres"
for _ in $(seq 1 60); do
  if podman exec eb-postgres pg_isready -U bench -d bench -h 127.0.0.1 >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done
podman exec eb-postgres psql -U bench -d bench -qc "CREATE EXTENSION IF NOT EXISTS pgmq; CREATE EXTENSION IF NOT EXISTS pg_stat_statements;"
podman exec eb-postgres psql -U bench -d bench -Atc "SELECT 'postgres ' || current_setting('server_version') || ', pgmq ' || extversion FROM pg_extension WHERE extname='pgmq'"

podman run -d --pod "$POD" --name eb-pgbouncer \
  -v "$PWD/pgbouncer:/etc/pgbouncer:ro,Z" \
  --entrypoint /usr/bin/pgbouncer \
  "$PGB_IMAGE" /etc/pgbouncer/pgbouncer.ini >/dev/null

echo -n "waiting for pgbouncer"
for _ in $(seq 1 30); do
  if podman exec eb-postgres psql "host=127.0.0.1 port=6432 dbname=bench user=bench password=bench" -Atc "select 1" >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done
podman ps --pod --filter pod="$POD" --format "table {{.Names}}\t{{.Status}}\t{{.Image}}"
