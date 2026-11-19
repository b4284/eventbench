#!/usr/bin/env bash
# sync.sh on  : eb_standby becomes the synchronous standby; the bench database commits with remote_write
#               (a commit returns once the standby has the WAL in memory; no fsync wait on either side).
# sync.sh off : async replication (synchronous_commit=off, best effort).
set -euo pipefail
case "${1:?on|off}" in
  on)  podman exec eb-postgres psql -U bench -d bench -q -c "ALTER SYSTEM SET synchronous_standby_names = 'eb_standby'" -c "SELECT pg_reload_conf()" >/dev/null
       podman exec eb-postgres psql -U bench -d postgres -qc "ALTER DATABASE bench SET synchronous_commit = remote_write" ;;
  off) podman exec eb-postgres psql -U bench -d bench -q -c "ALTER SYSTEM SET synchronous_standby_names = ''" -c "SELECT pg_reload_conf()" >/dev/null
       podman exec eb-postgres psql -U bench -d postgres -qc "ALTER DATABASE bench SET synchronous_commit = off" ;;
esac
podman exec eb-postgres psql -U bench -d bench -Atc "show synchronous_commit; select sync_state from pg_stat_replication"
