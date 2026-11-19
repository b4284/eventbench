#!/usr/bin/env bash
# After a failover drill: drop the promoted standby, restart the original primary, rebuild a fresh standby.
set -euo pipefail
cd "$(dirname "$0")"
podman rm -f eb-pg-standby >/dev/null 2>&1 || true
podman start eb-postgres >/dev/null
for _ in $(seq 1 60); do podman exec eb-postgres pg_isready -U bench -d bench -h 127.0.0.1 >/dev/null 2>&1 && break; sleep 1; done
./sync.sh off >/dev/null
./up.sh | tail -1
