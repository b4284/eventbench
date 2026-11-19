#!/usr/bin/env bash
# Failover drill: kill the primary (eb-postgres, port 5432) and promote the standby (port 5433).
# Clients using "Host=127.0.0.1:5432,127.0.0.1:5433;Target Session Attributes=read-write" follow it.
set -euo pipefail
echo "$(date +%T.%3N) killing primary"
podman kill eb-postgres >/dev/null
podman exec eb-pg-standby pg_ctl promote -D /tmp/standby >/dev/null
for _ in $(seq 1 30); do
  [[ "$(podman exec eb-pg-standby psql -U bench -d bench -p 5433 -Atc 'select pg_is_in_recovery()' 2>/dev/null)" == f ]] && break; sleep 0.2
done
echo "$(date +%T.%3N) standby promoted: $(podman exec eb-pg-standby psql -U bench -d bench -p 5433 -Atc 'select not pg_is_in_recovery()')"
