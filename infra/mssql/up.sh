#!/usr/bin/env bash
# Starts SQL Server (Developer edition) for the benchmark.
#   container eb-mssql, 127.0.0.1:1433, login sa / $MSSQL_SA_PASSWORD, database "bench"
#   connection string: Server=127.0.0.1,1433;Database=bench;User Id=sa;Password=...;TrustServerCertificate=true
# The database is created with READ_COMMITTED_SNAPSHOT on (readers don't block on writers' row locks),
# SIMPLE recovery and pre-sized data/log files (no autogrowth stalls mid-run).
# Durability: MSSQL_DURABILITY=forced (default, best effort: delayed durable commits, like
# synchronous_commit=off) or disabled (every commit waits for the log flush). Switch later with
# infra/mssql/durability.sh.
# Tunables via env: MSSQL_IMAGE, MSSQL_PORT, MSSQL_MEMORY_MB, MSSQL_CPUS
set -euo pipefail

NAME=${MSSQL_CONTAINER:-eb-mssql}
IMAGE=${MSSQL_IMAGE:-mcr.microsoft.com/mssql/server:2025-latest}
PORT=${MSSQL_PORT:-1433}
PASSWORD=${MSSQL_SA_PASSWORD:-EventBench#2026}
MEMORY_MB=${MSSQL_MEMORY_MB:-4096}
MSSQL_CPUS=${MSSQL_CPUS:-}
DURABILITY=${MSSQL_DURABILITY:-forced}

limits=()
[[ -n "$MSSQL_CPUS" ]] && limits+=(--cpus "$MSSQL_CPUS")

if podman container exists "$NAME"; then
  echo "container $NAME already exists (run infra/mssql/down.sh first to recreate)"; exit 0
fi

# Networking: EB_NETWORK=host (default) runs on the host network. Rootless podman's published ports go
# through a single-threaded userspace proxy (pasta) that capped Postgres paths at ~19k ev/s. EB_NETWORK=pasta
# restores published ports.
NETMODE=${EB_NETWORK:-host}
ports=(-p "127.0.0.1:${PORT}:1433"); [[ "$NETMODE" == host ]] && ports=(--network host)
podman run -d --name "$NAME" "${limits[@]}" "${ports[@]}" \
  -e ACCEPT_EULA=Y -e MSSQL_PID=Developer -e "MSSQL_SA_PASSWORD=$PASSWORD" \
  -e "MSSQL_MEMORY_LIMIT_MB=$MEMORY_MB" \
  "$IMAGE" >/dev/null

sql() { podman exec "$NAME" /opt/mssql-tools18/bin/sqlcmd -C -S 127.0.0.1 -U sa -P "$PASSWORD" -b "$@"; }

echo -n "waiting for sqlservr"
for _ in $(seq 1 90); do
  if sql -Q "SELECT 1" >/dev/null 2>&1; then echo " ok"; break; fi
  echo -n "."; sleep 1
done

sql -Q "
IF DB_ID('bench') IS NULL
BEGIN
  CREATE DATABASE bench
    ON (NAME = bench_data, FILENAME = '/var/opt/mssql/data/bench.mdf', SIZE = 4GB, FILEGROWTH = 1GB)
    LOG ON (NAME = bench_log, FILENAME = '/var/opt/mssql/data/bench.ldf', SIZE = 4GB, FILEGROWTH = 1GB);
END;
ALTER DATABASE bench SET RECOVERY SIMPLE;
ALTER DATABASE bench SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
ALTER DATABASE bench SET ALLOW_SNAPSHOT_ISOLATION ON;
EXEC sp_configure 'show advanced options', 1; RECONFIGURE;
EXEC sp_configure 'max degree of parallelism', 1; RECONFIGURE;
" >/dev/null

"$(dirname "$0")/durability.sh" "$DURABILITY"
sql -h -1 -W -s ' ' -Q "SET NOCOUNT ON; SELECT 'sql server', CAST(SERVERPROPERTY('ProductVersion') AS nvarchar(64)), CAST(SERVERPROPERTY('Edition') AS nvarchar(128)), 'rcsi', is_read_committed_snapshot_on, 'delayed_durability', delayed_durability_desc, 'recovery', recovery_model_desc FROM sys.databases WHERE name = 'bench'"
