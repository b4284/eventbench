#!/usr/bin/env bash
# Switches synchronous_commit for database "bench" on the running eb-postgres, then restarts PgBouncer
# so pooled server connections pick it up. (infra/up.sh passes -c synchronous_commit=... on the command
# line, which outranks ALTER SYSTEM; a per-database setting outranks the command line.)
#   infra/pg-sync-commit.sh off   # best effort: commits return before the WAL fsync (flushed within ~3x wal_writer_delay)
#   infra/pg-sync-commit.sh on    # crash-durable commits
set -euo pipefail
v=${1:?on|off}
podman exec eb-postgres psql -U bench -d bench -qc "ALTER SYSTEM RESET synchronous_commit" -c "SELECT pg_reload_conf()" \
  -c "ALTER DATABASE bench SET synchronous_commit = '$v'" >/dev/null
for c in $(podman ps --format '{{.Names}}' | grep -E '^eb-pgbouncer(-[0-9]+)?$'); do podman restart "$c" >/dev/null; done
for _ in $(seq 1 30); do
  podman exec eb-postgres psql "host=127.0.0.1 port=6432 dbname=bench user=bench password=bench" -Atc "select 1" >/dev/null 2>&1 && break; sleep 1
done
echo "via pgbouncer: synchronous_commit = $(podman exec eb-postgres psql "host=127.0.0.1 port=6432 dbname=bench user=bench password=bench" -Atc "show synchronous_commit")"
