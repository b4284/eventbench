#!/usr/bin/env bash
# Streaming standby for the eb-postgres primary (production-style HA pair), host network, port 5433.
#   Built with pg_basebackup (-R writes primary_conninfo); hot standby, read-only queries allowed.
#   Async by default (best effort: commits do not wait for the standby). infra/pg-standby/sync.sh switches
#   the bench database to synchronous_commit=remote_write with the standby as synchronous standby.
# No replication slot: wal_keep_size on the primary retains WAL, so a stopped standby cannot fill the disk.
set -euo pipefail
NAME=eb-pg-standby
IMAGE=${PG_IMAGE:-ghcr.io/pgmq/pg18-pgmq:latest}
podman container exists "$NAME" && { echo "$NAME exists (run infra/pg-standby/down.sh first)"; exit 0; }
podman exec eb-postgres psql -U bench -d bench -q -c "ALTER SYSTEM SET wal_keep_size = '8GB'" -c "SELECT pg_reload_conf()" >/dev/null
podman run -d --name "$NAME" --network host --user postgres --shm-size=1g --entrypoint bash "$IMAGE" -c '
  set -e
  rm -rf /tmp/standby
  PGPASSWORD=bench pg_basebackup -h 127.0.0.1 -p 5432 -U bench -D /tmp/standby -R -X stream --checkpoint=fast \
    -d "application_name=eb_standby"
  echo "primary_conninfo = '"'"'host=127.0.0.1 port=5432 user=bench password=bench application_name=eb_standby'"'"'" >> /tmp/standby/postgresql.auto.conf
  exec postgres -D /tmp/standby -p 5433 -c shared_buffers=2GB -c hot_standby=on -c max_connections=300 \
    -c hot_standby_feedback=on -c wal_receiver_status_interval=1s' >/dev/null
echo -n "waiting for standby"
for _ in $(seq 1 120); do
  st=$(podman exec eb-postgres psql -U bench -d bench -Atc "select state from pg_stat_replication where application_name='eb_standby'" 2>/dev/null)
  [[ "$st" == streaming ]] && { echo " streaming"; break; }
  echo -n "."; sleep 1
done
podman exec eb-postgres psql -U bench -d bench -Atc "select application_name, state, sync_state from pg_stat_replication"
