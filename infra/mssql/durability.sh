#!/usr/bin/env bash
# Sets SQL Server commit durability for database "bench".
#   forced   = best effort: commits return before the log flush (lose < ~60 KB / a few ms of commits on crash)
#   disabled = fully durable: every commit waits for its log flush (group commit still applies)
set -euo pipefail
MODE=${1:?usage: durability.sh forced|disabled}
NAME=${MSSQL_CONTAINER:-eb-mssql}
PASSWORD=${MSSQL_SA_PASSWORD:-EventBench#2026}
case "$MODE" in forced|FORCED) M=FORCED ;; disabled|DISABLED) M=DISABLED ;; *) echo "forced|disabled" >&2; exit 1 ;; esac
podman exec "$NAME" /opt/mssql-tools18/bin/sqlcmd -C -S 127.0.0.1 -U sa -P "$PASSWORD" -b -h -1 -Q \
  "SET NOCOUNT ON; ALTER DATABASE bench SET DELAYED_DURABILITY = $M WITH NO_WAIT; SELECT 'bench delayed_durability = ' + delayed_durability_desc FROM sys.databases WHERE name = 'bench'"
